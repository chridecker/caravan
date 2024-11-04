using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Devices.Contracts.Interfaces;
using Microsoft.Win32.SafeHandles;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Ruuvi


namespace chd.CaraVan.Web.Endpoints
{
    public static class RuuviEndpoint
    {
        public static IEndpointRouteBuilder AddRuuvi(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(ROOT).WithTags(ROOT);
            group.MapGet(GET_DATA, GetData);
            group.MapGet(ALL, GetAll);
            return app;
        }
        private static async Task GetAll(IRuuviTagDataService svc) => await svc.Devices;
        private static async Task<RuuviSensorDataDto> GetData(int id, IRuuviTagDataService svc, CancellationToken cancellationToken) => await svc.GetData(id, cancellationToken);
    }
}
