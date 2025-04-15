using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Components.Pages.Cards;
using chd.CaraVan.UI.Dtos;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Control;
namespace chd.CaraVan.Web.Endpoints
{
    public static class ControlEndpoint
    {
        public static IEndpointRouteBuilder AddControl(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(ROOT).WithTags(ROOT);

            group.MapGet(GET_SETTINGS, async (ISystemControlService svc, CancellationToken cancellationToken) => await svc.GetCurrentSettingAsync(cancellationToken));
            group.MapPost(POST_SETTINGS, async (HomeSettingDto dto, ISystemControlService svc, CancellationToken cancellationToken) => await svc.SetSettingsAsync(dto, cancellationToken));
            return app;
        }
    }
}
