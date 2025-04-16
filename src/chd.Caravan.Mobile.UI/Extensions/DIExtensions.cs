using chd.Caravan.Mobile.UI.Interfaces;
using chd.Caravan.Mobile.UI.Services;
using chd.UI.Base.Client.Extensions;
using chd.UI.Base.Client.Implementations.Services;
using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace chd.Caravan.Mobile.UI.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddUi<TSettingManager, TBLEManager>(this IServiceCollection services, IConfiguration configuration)
            where TSettingManager : BaseClientSettingManager<int, int>, ISettingManager
            where TBLEManager : class, IBLEManager
        {

            services.AddAuthorizationCore();
            services.Add(new ServiceDescriptor(typeof(IBLEManager), typeof(TBLEManager), ServiceLifetime.Singleton));

            services.AddUtilities<chdProfileService, int, int, UserIdLogInService, TSettingManager, ISettingManager, UIComponentHandler, IBaseUIComponentHandler, UpdateService>(ServiceLifetime.Singleton);
            services.AddMauiModalHandler();
            services.AddScoped<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();
            services.AddScoped<IDeviceStorageService, DeviceStorageService>();
            services.AddScoped<INavigationHandler, NavigationHandler>();

            services.AddSingleton<IAppInfoService, AppInfoService>();
            return services;
        }
    }
}
