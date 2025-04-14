using chd.CaraVan.Contracts.Settings;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.Devices.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Gpio;

namespace chd.CaraVan.Devices.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDeviceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<DeviceSettings>(configuration.GetSection(nameof(DeviceSettings)));

            services.AddSingleton<GpioController>();
            services.AddSingleton<BLEManager>();

            services.AddSingleton<ISystemManager, SystemManager>();

            services.AddSingleton<IRuuviTagDataService, RuuviTagDataService>();
            services.AddSingleton<IVotronicDataService, VotronicDataService>();


            return services;
        }
    }
}
