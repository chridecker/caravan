using chd.CaraVan.App.Services;
using chd.CaraVan.UI.Extensions;
using chd.CaraVan.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.App.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddAppUi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUi<SettingManager>(configuration);
#if ANDROID
            services.ConfigureHttpClientDefaults(builder => builder.ConfigurePrimaryHttpMessageHandler(chd.CaraVan.App.Platforms.Android.HttpsClientHandlerService.GetPlatformMessageHandler));
#endif
            return services;
        }
    }
}
