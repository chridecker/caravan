using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Interfaces;
using chd.UI.Base.Components.Base;
using Microsoft.AspNetCore.Components;

namespace chd.CaraVan.UI.Components.Pages
{
    public partial class Home : PageComponentBase<int, int>, IDisposable
    {
        [Inject] private IVotronicDataService _votronicData { get; set; }
        [Inject] private IRuuviTagDataService _ruuviTagDataService { get; set; }
        [Inject] private IDataHubClient _dataHubClient { get; set; }

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private VotronicBatteryData VotronicBatteryData;
        private VotronicSolarData VotronicSolarData;

        private IDictionary<int, RuuviSensorDataDto> _valueDict = new Dictionary<int, RuuviSensorDataDto>();

        private DateTime? RuuviTime(RuuviDeviceDto dto) => this._valueDict.TryGetValue(dto.Id, out var val) ? val?.Record : null;
        private decimal? RuuviValue(RuuviDeviceDto dto) => this._valueDict.TryGetValue(dto.Id, out var val) ? val?.Value : null;
        private (decimal?, decimal?) MinMax(RuuviDeviceDto dto) => this._valueDict.TryGetValue(dto.Id, out var val) ? (val.Min, val.Max) : (null, null);

        private IEnumerable<RuuviDeviceDto> _devices = [];

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

            await base.OnInitializedAsync();
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