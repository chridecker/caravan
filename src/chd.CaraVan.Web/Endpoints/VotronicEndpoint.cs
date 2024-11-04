using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Devices.Contracts.Interfaces;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Votronic;
namespace chd.CaraVan.Web.Endpoints
{
    public static class VotronicEndpoint
    {
        public static IEndpointRouteBuilder AddVotronic(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(ROOT).WithTags(ROOT);

            group.MapGet(Battery,GetBatteryData);
            group.MapGet(Solar,GetSolarData);
            return app;
        }
        private static async Task<VotronicBatteryData> GetBatteryData(IVotronicDataService svc, CancellationToken cancellationToken)=> await svc.GetBatteryData(cancellationToken);
        private static async Task<VotronicSolarData> GetSolarData(IVotronicDataService svc, CancellationToken cancellationToken)=> await svc.GetSolarData(cancellationToken);

    }
}
