using chd.CaraVan.Devices.Extensions;
using chd.CaraVan.UI.Extensions;
using chd.CaraVan.Web.Services;

namespace chd.CaraVan.Web.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUi(configuration);

            services.AddDeviceServices(configuration);

            if (OperatingSystem.IsLinux())
            {
                services.AddHostedService<DeviceWorker>();
            }

            return services;
        }
    }
}
