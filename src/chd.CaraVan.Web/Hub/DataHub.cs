using chd.CaraVan.Contracts.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace chd.CaraVan.Web.Hub
{
     public class DataHub : Hub<IDataHub>
    {
        private readonly ILogger<DataHub> _logger;

        public DataHub(ILogger<DataHub>logger)
        {
            this._logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            this._logger?.LogDebug($"Connected Client on Hub");
            return base.OnConnectedAsync();
        }
    }

   
}
