using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Enums;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.Contracts.Settings;
using chd.CaraVan.Devices.Contracts.Dtos.Pi;
using chd.CaraVan.Devices.Contracts.Dtos.RuvviTag;
using chd.CaraVan.Devices.Contracts.Dtos.Votronic;
using chd.CaraVan.Devices.Contracts.Interfaces;
using chd.CaraVan.Devices.Implementations;
using chd.CaraVan.Web.Hub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace chd.CaraVan.Web.Services
{
    public class DeviceWorker : BackgroundService
    {
        private readonly ILogger<DeviceWorker> _logger;
        private readonly IHubContext<DataHub, IDataHub> _hub;
        private readonly IOptionsMonitor<AesSettings> _optionsMonitorAes;
        private readonly IOptionsMonitor<PiSettings> _optionsMonitorPi;
        private readonly IEmailService _emailService;
        private readonly IOptionsMonitor<DeviceSettings> _optionsMonitor;
        private readonly IRuuviTagDataService _dataService;
        private readonly IVotronicDataService _votronicDataService;
        private readonly BLEManager _bleManager;
        private readonly IPiManager _pi;
        private readonly IAESManager _aesManager;

        public DeviceWorker(ILogger<DeviceWorker> logger, BLEManager bLEManager,
             IHubContext<DataHub, IDataHub> hub, IOptionsMonitor<AesSettings> optionsMonitorAes,
             IOptionsMonitor<PiSettings> optionsMonitorPi, IEmailService emailService,
             IPiManager piManager, IAESManager aesManager, IOptionsMonitor<DeviceSettings> optionsMonitor,
             IRuuviTagDataService dataService, IVotronicDataService votronicDataService)
        {
            this._logger = logger;
            this._hub = hub;
            this._bleManager = bLEManager;
            this._optionsMonitorAes = optionsMonitorAes;
            this._optionsMonitorPi = optionsMonitorPi;
            this._emailService = emailService;
            this._pi = piManager;
            this._aesManager = aesManager;
            this._optionsMonitor = optionsMonitor;
            this._dataService = dataService;
            this._votronicDataService = votronicDataService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            this.StartPi();
            await this.StartDevices(cancellationToken);
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            this._pi.Stop();
            this._aesManager.StateSwitched -= this._aesManager_StateSwitched;
            await this._bleManager?.DisconnectAsync();
            await base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this._aesManager.CheckForActive();
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
        private void StartPi()
        {
            this._aesManager.StateSwitched += this._aesManager_StateSwitched;
            if (OperatingSystem.IsLinux())
            {
                this._pi.Start();
            }
        }

        private async void _aesManager_StateSwitched(object? sender, bool e)
        {
            await this._hub.Clients.All.AesStateSwitched(e);
            foreach (var pin in this._optionsMonitorPi.CurrentValue.Gpios.Where(x => x.Type == GpioType.Aes))
            {
                await this._emailService.SendEmail($"AES switched to {(e ? "ON" : "OFF")}", "AES");
                await this._pi.Write(new PinWriteDto
                {
                    Pin = pin.Pin,
                    Value = this._optionsMonitorAes.CurrentValue.IsActive && e
                });
            }
        }

        private async Task StartDevices(CancellationToken cancellationToken)
        {
            this._bleManager.RuuviTagDataReceived += this.RuuviTag_DataReceived;
            this._bleManager.VotronicDataReceived += this._tag_VotronicDataReceived;

            if (OperatingSystem.IsLinux())
            {
                await this._bleManager.ConnectAsync(cancellationToken);
            }
        }

        private async void _tag_VotronicDataReceived(object? sender, VotronicEventArgs e)
        {
            if (e.BatteryData is not null)
            {
                var data = new Contracts.Dtos.VotronicBatteryData()
                {
                    DateTime = e.DateTime,
                    Ampere = e.BatteryData.Ampere,
                    AmpereH = e.BatteryData.LeftAH,
                    Voltage = e.BatteryData.Voltage,
                    Percent = e.BatteryData.Percent,
                    Data = e.BatteryData.Data
                };
                await this._votronicDataService.AddData(data);
                await this._hub.Clients.All.VotronicData();
            }
            if (e.SolarData is not null)
            {
                var data = new Contracts.Dtos.VotronicSolarData()
                {
                    DateTime = e.DateTime,
                    Ampere = e.SolarData.Ampere,
                    WattH = e.SolarData.WattH,
                    AmpereH = e.SolarData.AH,
                    State = e.SolarData.State,
                    Voltage = e.SolarData.Voltage,
                    VoltageSolar = e.SolarData.VoltageSolar,
                    LoadingPhase = e.SolarData.LoadingPhase,
                    Data = e.SolarData.Data
                };
                await this._votronicDataService.AddData(data);
                await this._hub.Clients.All.VotronicData();
            }
        }

        private async void RuuviTag_DataReceived(object? sender, RuuviTagEventArgs e)
        {
            var device = this._optionsMonitor.CurrentValue.RuuviTags.FirstOrDefault(x => x.Id == e.Id);
            var data = new RuuviTagDeviceData(e.DateTime, EDataType.Temperature, e.Data.Temperature ?? 0)
            {
                DeviceId = device.Id
            };
            await this._dataService.AddData(device.Id, data);
            await this._hub.Clients.All.RuuviTagData();
        }
    }
}
