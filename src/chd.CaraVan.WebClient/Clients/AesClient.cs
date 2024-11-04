using chd.Api.Base.Client;
using chd.CaraVan.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Aes;

namespace chd.CaraVan.WebClient.Clients
{
    public class AesClient : BaseApiService, IAESManager
    {
        public AesClient(ILogger<AesClient> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
        {
        }

        public Task<bool> IsActive => this.Get<bool>(IS_ACTIVE);

        public Task<DateTime?> SolarAesOffSince => this.Get<DateTime?>(AES_OFF_SINCE);

        public Task<decimal?> BatteryLimit => this.Get<decimal?>(BATTERY_LIMIT);

        public Task<decimal?> SolarAmpLimit => this.Get<decimal?>(AES_LIMIT);

        public Task<TimeSpan?> AesTimeout => this.Get<TimeSpan?>(AES_TIMEOUT);

        public event EventHandler<bool> StateSwitched;

        public async Task CheckForActive(CancellationToken cancellationToken = default) => await this.Post(CHECK_ACTIVE, cancellationToken);

        public async Task Off(CancellationToken cancellationToken = default) => await this.Post(AES_OFF, cancellationToken);

        public async Task SetActive(CancellationToken cancellationToken = default) => await this.Post(SET_ACTIVE, cancellationToken);
    }
}
