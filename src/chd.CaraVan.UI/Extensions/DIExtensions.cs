using chd.Api.Base.Client.Extensions;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Hubs.Clients;
using chd.CaraVan.UI.Interfaces;
using chd.CaraVan.UI.Services;
using chd.CaraVan.WebClient.Extensions;
using chd.UI.Base.Client.Extensions;
using chd.UI.Base.Client.Implementations.Services;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chd.CaraVan.UI.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddUi<TSettingManager>(this IServiceCollection services, IConfiguration configuration, ServiceLifetime profileServiceLifeTime = ServiceLifetime.Singleton)
             where TSettingManager : BaseSettingManager, ISettingManager
        {
            services.AddAuthorizationCore();
            services.AddUtilities<chdProfileService, int, int, HandleUserIdLogin, TSettingManager, ISettingManager, UiHandler, IBaseUIComponentHandler, UpdateService>(profileServiceLifeTime);
             services.AddMauiModalHandler();
            services.AddSingleton<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();
            services.AddScoped<INavigationHandler, NavigationHandler>();

            services.AddChdCaravanClient(sp => configuration.GetApiKey("chdCaravanApi"));
            services.AddSingleton<IAppInfoService, AppInfoService>();

            services.AddScoped<IDataHubClient, DataHubClient>();

            return services;
        }
    }
}
