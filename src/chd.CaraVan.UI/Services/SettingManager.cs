using chd.Api.Base.Client.Extensions;
using chd.CaraVan.Contracts.Contants;
using chd.CaraVan.Contracts.Interfaces;
using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace chd.CaraVan.UI.Services
{
    public class SettingManager : BaseClientSettingManager<int, int>, ISettingManager
    {

        public SettingManager(ILogger<SettingManager> logger, IProtecedLocalStorageHandler protecedLocalStorageHandler,
            NavigationManager navigationManager) : base(logger, protecedLocalStorageHandler, navigationManager)
        {
        }
    }
}
