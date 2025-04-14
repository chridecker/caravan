using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Interfaces;
using chd.CaraVan.Shared.UI.Dtos;
using chd.UI.Base.Components.Base;
using Microsoft.AspNetCore.Components;
using chd.UI.Base.Contracts.Enum;
using Blazorise;
using Blazored.Modal;
using chd.CaraVan.UI.Dtos;

namespace chd.CaraVan.UI.Components.Pages
{
    public partial class Home : PageComponentBase<int, int>, IDisposable
    {
        [Inject] private Blazored.Modal.Services.IModalService _modal { get; set; }
        [Inject] private IVotronicDataService _votronicData { get; set; }
        [Inject] private IRuuviTagDataService _ruuviTagDataService { get; set; }
        [Inject] private IDataHubClient _dataHubClient { get; set; }

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private VotronicBatteryData VotronicBatteryData;
        private VotronicSolarData VotronicSolarData;

        private IDictionary<int, RuuviSensorDataDto> _valueDict = new Dictionary<int, RuuviSensorDataDto>();

        private decimal? RuuviValue(RuuviDeviceDto dto) => this._valueDict.TryGetValue(dto.Id, out var val) ? val?.Value : null;

        private IEnumerable<RuuviDeviceDto> _devices = [];

        private string _selectedSlide = "Time";
        private Carousel _carousel;

        private HomeSettingDto _settings = new();

        protected override async Task OnInitializedAsync()
        {
            this.Title = "Home";
            try
            {
                this._devices = await this._ruuviTagDataService.Devices;
            }
            catch { }
            await this.Reload();
            if (!this._dataHubClient.IsConnected)
            {
                this.StartHub();
            }
            this._dataHubClient.VotronicDataReceived += this._dataHubClient_VotronicDataReceived;
            this._dataHubClient.RuuviTagDeviceDataReceived += this._dataHubClient_RuuviTagDeviceDataReceived;

            this.VotronicSolarData = await this._votronicData.GetSolarData();
            this.VotronicBatteryData = await this._votronicData.GetBatteryData();

            this.VotronicBatteryData = new([25, 34, 25, 58, 58, 15, 15, 35, 58, 28, 253, 214, 123, 25, 35, 45], 200);
            this.VotronicSolarData = new([25, 34, 25, 58, 251, 15, 15, 35, 58, 28, 253, 2, 123, 25, 35, 45]);


            this._valueDict[1] = new RuuviSensorDataDto()
            {
                Record = DateTime.Now,
                Min = -9.5m,
                Max = 11.5m,
                Value = 27.8m
            };

            this._valueDict[2] = new RuuviSensorDataDto()
            {
                Record = DateTime.Now,
                Min = -9.5m,
                Max = 11.5m,
                Value = 27.8m
            };

            this._valueDict[3] = new RuuviSensorDataDto()
            {
                Record = DateTime.Now,
                Min = -9.5m,
                Max = 11.5m,
                Value = 27.8m
            }; this._valueDict[5] = new RuuviSensorDataDto()
            {
                Record = DateTime.Now,
                Min = -9.5m,
                Max = 11.5m,
                Value = 27.8m
            };

            await base.OnInitializedAsync();
        }


        private Task OnSwipe(ESwipeDirection direction)
            => direction switch
            {
                ESwipeDirection.TopToBottom => this.ShowSettingModal(),
                ESwipeDirection.LeftToRight => this._carousel.SelectPrevious(),
                ESwipeDirection.RightToLeft => this._carousel.SelectNext(),
                ESwipeDirection.BottomToTop => this._carousel.Select("Sensors"),
                _ => Task.CompletedTask
            };

        private async Task ShowSettingModal()
        {
            var parameter = new ModalParameters
            {
                {nameof(Settings.HomeSetting),this._settings }
            };
            _ = await this._modal.Show<Settings>("Settings", parameter).Result;
        }

        private async Task Reload()
        {
            foreach (var device in this._devices)
            {
                var data = await this._ruuviTagDataService.GetData(device.Id);
                if (data is not null)
                {
                    this._valueDict[device.Id] = data;
                }
            }
        }

        private async void _dataHubClient_RuuviTagDeviceDataReceived(object sender, EventArgs e)
        {
            await this.Reload();
            await this.InvokeAsync(this.StateHasChanged);
        }

        private async void _dataHubClient_VotronicDataReceived(object sender, EventArgs e)
        {
            this.VotronicSolarData = await this._votronicData.GetSolarData();
            this.VotronicBatteryData = await this._votronicData.GetBatteryData();
            await this.InvokeAsync(this.StateHasChanged);
        }

        private void StartHub() => Task.Run(async () => await this._dataHubClient.StartAsync(this._cts.Token));
        public void Dispose()
        {
            this._dataHubClient.RuuviTagDeviceDataReceived -= this._dataHubClient_RuuviTagDeviceDataReceived;
            this._dataHubClient.VotronicDataReceived -= this._dataHubClient_VotronicDataReceived;

            this._cts.Cancel();
            this._cts.Dispose();

        }
    }
}