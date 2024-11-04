using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using static chd.CaraVan.Contracts.Contants.EndpointContants.System;
namespace chd.CaraVan.Web.Endpoints
{
    public static class SystemEndpoint
    {
        public static IEndpointRouteBuilder AddSystem(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(ROOT).WithTags(ROOT);

            group.MapGet(RUNNING_SINCE, async (string service, ISystemManager manager, CancellationToken cancellationToken) => await manager.IsServiceRunning(service, cancellationToken));

            group.MapPost(START_SERVICE, async (ServiceControlDto dto, ISystemManager manager, CancellationToken cancellationToken) => await manager.StartService(dto, cancellationToken));
            group.MapPost(STOP_SERVICE, async (ServiceControlDto dto, ISystemManager manager, CancellationToken cancellationToken) => await manager.StopService(dto, cancellationToken));
            group.MapPost(CHANGE_STATE_IN_TIME, async (ServiceControlDto dto, ISystemManager manager, CancellationToken cancellationToken) => await manager.ChangeStateInTime(dto, cancellationToken));
            group.MapPost(REBOOT, async (ISystemManager manager, CancellationToken cancellationToken) => await manager.Reboot(cancellationToken));

            return app;
        }
    }
}
