using chd.Api.Base.Client;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Control;

namespace chd.CaraVan.WebClient.Clients
{
    public class ContolClient : BaseApiService, ISystemControlService
    {
        public event EventHandler SettingsChanged;

        public ContolClient(ILogger<ContolClient> logger, IHttpClientFactory httpClientFactory) : base(logger, httpClientFactory)
        {
        }


        public async Task<HomeSettingDto> GetCurrentSettingAsync(CancellationToken cancellationToken = default)
        => await this.Get<HomeSettingDto>(GET_SETTINGS, cancellationToken);

        public async Task SetSettingsAsync(HomeSettingDto dto, CancellationToken cancellationToken)
        => await this.Post(POST_SETTINGS, dto, cancellationToken);
    }
}
