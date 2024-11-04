using chd.Api.Base.Client;
using chd.Api.Base.Client.Extensions;
using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chd.CaraVan.Contracts.Contants.EndpointContants.System;

namespace chd.CaraVan.WebClient.Clients
{
    public class SystemClient : BaseApiService, ISystemManager
    {
        public SystemClient(ILogger<SystemClient> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
        {
        }

        public async Task ChangeStateInTime(ServiceControlDto dto, CancellationToken cancellationToken = default)
        => await this.Post(CHANGE_STATE_IN_TIME, dto, cancellationToken);

        public async Task<DateTime?> IsServiceRunning(string service, CancellationToken cancellationToken = default)
        => await this.Get<DateTime?>(RUNNING_SINCE.SetUrlParameters((nameof(service), service)), cancellationToken);

        public async Task Reboot(CancellationToken cancellationToken = default)
        => await this.Post(REBOOT, cancellationToken);


        public async Task<bool> StartService(ServiceControlDto dto, CancellationToken cancellationToken = default)
        => await this.Post<bool>(START_SERVICE, dto, cancellationToken);

        public async Task<bool> StopService(ServiceControlDto dto, CancellationToken cancellationToken = default)
        => await this.Post<bool>(STOP_SERVICE, dto, cancellationToken);
    }
}
