using chd.CaraVan.Contracts.Settings;
using chd.CaraVan.Devices.Contracts.Dtos.Pi;
using chd.CaraVan.Devices.Contracts.Interfaces;
using chd.CaraVan.Devices.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDeviceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<DeviceSettings>(configuration.GetSection(nameof(DeviceSettings)));
            services.Configure<AesSettings>(configuration.GetSection(nameof(AesSettings)));
            services.Configure<PiSettings>(configuration.GetSection(nameof(PiSettings)));

            services.AddSingleton<GpioController>();
            services.AddSingleton<BLEManager>();

            services.AddSingleton<IPiManager, PiManager>();
            services.AddSingleton<ISystemManager, SystemManager>();

            services.AddSingleton<IRuuviTagDataService, RuuviTagDataService>();
            services.AddSingleton<IVotronicDataService, VotronicDataService>();
            services.AddSingleton<IAESManager, AESManager>();


            return services;
        }
    }
}
