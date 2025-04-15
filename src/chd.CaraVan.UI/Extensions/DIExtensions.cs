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
        public static IServiceCollection AddUi(this IServiceCollection services, IConfiguration configuration, ServiceLifetime profileServiceLifeTime = ServiceLifetime.Singleton)
        {
            services.AddAuthorizationCore();
            services.AddUtilities<chdProfileService, int, int, HandleUserIdLogin, SettingManager, ISettingManager, UiHandler, IBaseUIComponentHandler, UpdateService>(profileServiceLifeTime);
            services.AddScoped<IDataHubClient, DataHubClient>();
            services.AddSingleton<ISystemControlService, SystemControlService>();
            services.AddSingleton<IKeyHandler, KeyHandler>();

            return services;
        }
    }
}
