using chd.Api.Base.Client;
using chd.Api.Base.Client.Extensions;
using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Pi;

namespace chd.CaraVan.WebClient.Clients
{
    public class PiClient : BaseApiService, IPiManager
    {
        public PiClient(ILogger<PiClient> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
        {
        }

        public async Task<IEnumerable<GpioPinDto>> GetGpioPins(CancellationToken cancellationToken = default) => await this.Get<IEnumerable<GpioPinDto>>(GET_SETTING, cancellationToken);

        public async Task<bool> Read(int pin, CancellationToken cancellationToken = default)
        => await this.Get<bool>(GET_PIN_STATE.SetUrlParameters((nameof(pin), pin)), cancellationToken);

        public async Task Write(PinWriteDto dto, CancellationToken cancellationToken = default) => await this.Post(POST_WRITE_PIN, dto, cancellationToken);

        public Task Start(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task Stop(CancellationToken cancellationToken = default) => Task.CompletedTask;


    }
}
