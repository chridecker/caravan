using chd.CaraVan.Devices.Contracts.Interfaces;
using chd.CaraVan.Devices.Extensions;
using chd.CaraVan.Web.Services;

namespace chd.CaraVan.Web.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddServer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IEmailService, EmailService>();
            services.AddDeviceServices(configuration);

            if (OperatingSystem.IsLinux())
            {
                services.AddHostedService<DeviceWorker>();
            }

            return services;
        }
    }
}
