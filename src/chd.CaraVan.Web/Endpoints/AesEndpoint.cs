using chd.CaraVan.Contracts.Interfaces;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Aes;

namespace chd.CaraVan.Web.Endpoints
{
    public static class AesEndpoint
    {
        public static IEndpointRouteBuilder AddAes(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(ROOT).WithTags(ROOT);

            group.MapGet(IS_ACTIVE, async (IAESManager manager) => await manager.IsActive);
            group.MapGet(AES_OFF_SINCE, async (IAESManager manager) => await manager.SolarAesOffSince);
            group.MapGet(BATTERY_LIMIT, async (IAESManager manager) => await manager.BatteryLimit);
            group.MapGet(AES_TIMEOUT, async (IAESManager manager) => await manager.AesTimeout);
            group.MapGet(AES_LIMIT, async (IAESManager manager) => await manager.SolarAmpLimit);

            group.MapPost(SET_ACTIVE, async (IAESManager manager, CancellationToken cancellationToken) => await manager.SetActive(cancellationToken));
            group.MapPost(CHECK_ACTIVE, async (IAESManager manager, CancellationToken cancellationToken) => await manager.CheckForActive(cancellationToken));
            group.MapPost(AES_OFF, async (IAESManager manager, CancellationToken cancellationToken) => await manager.Off(cancellationToken));
            return app;
        }
    }
}
