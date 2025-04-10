using chd.Caravan.Mobile.UI.Extensions;
using chd.Caravan.Mobile.UI.Interfaces;
using chd.Caravan.Mobile.WPF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.WPF.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddChdCaravanApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUi<SettingManager,BLEManager>(configuration);

            services.AddSingleton<INotificationManagerService, NotificationManagerService>();
            return services;
        }
    }
}
