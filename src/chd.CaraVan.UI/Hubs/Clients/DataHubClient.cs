using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Interfaces;
using chd.Hub.Base.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace chd.CaraVan.UI.Hubs.Clients
{
    public class DataHubClient : BaseHubClient<IDataHub>, IDataHubClient
    {
        private readonly ISettingManager _settingManager;

        public event EventHandler VotronicDataReceived;
        public event EventHandler RuuviTagDeviceDataReceived;
        public event EventHandler<bool> AesStateSwitched;

        public DataHubClient(ILogger<DataHubClient> logger, ISettingManager settingManager) : base(logger)
        {
            this._settingManager = settingManager;
        }

        protected override Uri LoadUri() => new UriBuilder($"http://localhost/data-hub").Uri;
        protected override Task<bool> ShouldInitialize(CancellationToken cancellationToken) => Task.FromResult(true);


        protected override Task DoInvokations(HubConnection connection, CancellationToken cancellationToken) => Task.CompletedTask;

        protected override void HookIncomingCalls(HubConnection connection)
        {
            connection.On(nameof(IDataHub.VotronicData), () =>
            {
                this.VotronicDataReceived?.Invoke(this, EventArgs.Empty);
            });

            connection.On(nameof(IDataHub.RuuviTagData), () =>
            {
                this.RuuviTagDeviceDataReceived?.Invoke(this, EventArgs.Empty);
            });

            connection.On(nameof(IDataHub.AesStateSwitched), (bool state) =>
            {
                this.AesStateSwitched?.Invoke(this, state);
            });
        }

        protected override void SpecificReinitialize(HubConnection connection)
        {
            connection.Remove(nameof(IDataHub.VotronicData));
            connection.Remove(nameof(IDataHub.RuuviTagData));
        }
    }

}
