using chd.Api.Base.Client;
using chd.Api.Base.Client.Extensions;
using chd.CaraVan.Contracts.Contants;
using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using Microsoft.Extensions.Logging;

namespace chd.CaraVan.WebClient.Clients
{
    public class RuuviClient : BaseApiService, IRuuviTagDataService
    {
        public RuuviClient(ILogger<RuuviClient> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
        {
        }

        public Task<IEnumerable<RuuviDeviceDto>> Devices => this.Get<IEnumerable<RuuviDeviceDto>>(EndpointContants.Ruuvi.ALL, CancellationToken.None);

        public async Task<RuuviSensorDataDto> GetData(int id, CancellationToken cancellationToken = default)
          => await this.Get<RuuviSensorDataDto>(EndpointContants.Ruuvi.GET_DATA.SetUrlParameters((nameof(id), id)), cancellationToken);

        public Task AddData(int id, RuuviTagDeviceData data, CancellationToken cancellationToken = default)
        => Task.CompletedTask;


    }
}
