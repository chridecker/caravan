using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Enums;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.Contracts.Settings;
using chd.CaraVan.Devices.Contracts.Dtos.RuvviTag;
using chd.CaraVan.Devices.Contracts.Dtos.Votronic;
using chd.CaraVan.Devices.Implementations;
using chd.CaraVan.Web.Hub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace chd.CaraVan.Web.Services
{
    public class DeviceWorker : BackgroundService
    {
        private readonly IHubContext<DataHub, IDataHub> _hub;
        private readonly IPiManager _piManager;
        private readonly IOptionsMonitor<DeviceSettings> _optionsMonitor;
        private readonly IRuuviTagDataService _dataService;
        private readonly IVotronicDataService _votronicDataService;
        private readonly BLEManager _bleManager;

        public DeviceWorker(BLEManager bLEManager,
             IHubContext<DataHub, IDataHub> hub,
             IPiManager piManager,
             IOptionsMonitor<DeviceSettings> optionsMonitor,
             IRuuviTagDataService dataService, IVotronicDataService votronicDataService)
        {
            this._hub = hub;
            this._piManager = piManager;
            this._bleManager = bLEManager;
            this._optionsMonitor = optionsMonitor;
            this._dataService = dataService;
            this._votronicDataService = votronicDataService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.StartDevices(cancellationToken);

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await this._bleManager?.DisconnectAsync();
            await this._piManager.Stop(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task StartDevices(CancellationToken cancellationToken)
        {
            this._bleManager.RuuviTagDataReceived += this.RuuviTag_DataReceived;
            this._bleManager.VotronicDataReceived += this._tag_VotronicDataReceived;

            if (OperatingSystem.IsLinux())
            {
                await this._piManager.Start(cancellationToken);
                await this._bleManager.ConnectAsync(cancellationToken);
            }
        }

        private async void _tag_VotronicDataReceived(object? sender, VotronicEventArgs e)
        {
            if (e.BatteryData is not null)
            {
                await this._votronicDataService.AddData(e.BatteryData);
                await this._hub.Clients.All.VotronicData();
            }
            if (e.SolarData is not null)
            {
                await this._votronicDataService.AddData(e.SolarData);
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
