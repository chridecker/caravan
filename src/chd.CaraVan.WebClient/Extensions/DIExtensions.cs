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
            services.AddHttpClient<IRuuviTagDataService>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.Ruuvi.ROOT));
            services.AddTransient<IRuuviTagDataService, RuuviClient>();

            services.AddHttpClient<IVotronicDataService>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.Votronic.ROOT));
            services.AddTransient<IVotronicDataService, VotronicClient>();
            
            services.AddHttpClient<IAESManager>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.Aes.ROOT));
            services.AddTransient<IAESManager, AesClient>();
            
            services.AddHttpClient<IPiManager>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.Pi.ROOT));
            services.AddTransient<IPiManager, PiClient>();
            
            services.AddHttpClient<ISystemManager>(sp => func.Invoke(sp).Append(ROOT).Append(EndpointContants.System.ROOT));
            services.AddTransient<ISystemManager, SystemClient>();
            return services;
        }
    }
}
