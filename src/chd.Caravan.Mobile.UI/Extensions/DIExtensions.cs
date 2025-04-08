using chd.Caravan.Mobile.UI.Interfaces;
using chd.Caravan.Mobile.UI.Services;
using chd.UI.Base.Client.Extensions;
using chd.UI.Base.Client.Implementations.Services;
using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
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
        public static IServiceCollection AddUi<TSettingManager>(this IServiceCollection services, IConfiguration configuration)
            where TSettingManager : BaseClientSettingManager<int,int>, ISettingManager
        {
            services.AddAuthorizationCore();
            services.AddUtilities<chdProfileService, int, int, BaseUserIdLoginService<int>, TSettingManager, ISettingManager, BaseUIComponentHandler, IBaseUIComponentHandler, BaseUpdateService>(ServiceLifetime.Singleton);
            services.AddMauiModalHandler();
            services.AddSingleton<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();
            services.AddScoped<INavigationHandler, NavigationHandler>();

            services.AddSingleton<IAppInfoService, AppInfoService>();
            return services;
        }
    }
}
