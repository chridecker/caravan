using chd.CaraVan.Contracts.Settings;
using chd.CaraVan.Devices;
using chd.CaraVan.Devices.Contracts.Dtos.Pi;
using chd.CaraVan.Devices.Contracts.Interfaces;
using chd.CaraVan.UI.Hubs.Clients;
using chd.CaraVan.UI.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.UI.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddUi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataHubClient, DataHubClient>();

            return services;
        }
    }
}
