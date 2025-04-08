using chd.Caravan.Mobile.Platforms.Android;
using chd.Caravan.Mobile.Services;
using chd.Caravan.Mobile.UI.Extensions;
using chd.Caravan.Mobile.UI.Interfaces;
using Microsoft.Extensions.Configuration;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBluetoothLE>(CrossBluetoothLE.Current);
            services.AddSingleton<IAdapter>(CrossBluetoothLE.Current.Adapter);

            services.AddSingleton<INotificationManagerService,NotificationManagerService>();

            services.AddUi<SettingManager, BLEManager>(configuration);

            return services;
        }
    }
}
