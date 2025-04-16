using chd.CaraVan.Contracts.Contants;

namespace chd.CaraVan.Web.Endpoints
{
    public static class ComposeEndpoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(EndpointContants.ROOT);
            group.AddSystem();

            return app;
        }
    }
}
