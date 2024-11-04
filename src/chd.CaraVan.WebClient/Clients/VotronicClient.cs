using chd.Api.Base.Client;
using chd.CaraVan.Contracts.Contants;
using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using Microsoft.Extensions.Logging;

namespace chd.CaraVan.WebClient.Clients
{
    public class VotronicClient : BaseApiService, IVotronicDataService
    {
        public VotronicClient(ILogger<VotronicClient> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
        {
        }

        public async Task<VotronicBatteryData> GetBatteryData(CancellationToken cancellationToken = default)
       => await this.Get<VotronicBatteryData>(EndpointContants.Votronic.Battery, cancellationToken);

        public async Task<VotronicSolarData> GetSolarData(CancellationToken cancellationToken = default)
         => await this.Get<VotronicSolarData>(EndpointContants.Votronic.Solar, cancellationToken);

        public Task AddData(VotronicBatteryData votronicBatteryData, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

        public Task AddData(VotronicSolarData votronicSolarData, CancellationToken cancellationToken = default)
         => Task.CompletedTask;

    }
}
