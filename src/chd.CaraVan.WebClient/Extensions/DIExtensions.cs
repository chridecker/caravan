using chd.CaraVan.Contracts.Contants;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.WebClient.Clients;
using chd.Api.Base.Client.Extensions;
using Microsoft.Extensions.DependencyInjection;
using static chd.CaraVan.Contracts.Contants.EndpointContants;

namespace chd.CaraVan.WebClient.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddChdCaravanClient(this IServiceCollection services, Func<IServiceProvider, Uri> func)
        {
            services.AddHttpClient<SystemClient>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.System.ROOT));
            services.AddTransient<ISystemManager, SystemClient>();
            
            services.AddHttpClient<ContolClient>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.Control.ROOT));
            services.AddTransient<ISystemControlService, ContolClient>();
            return services;
        }
    }
}
