using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using static chd.CaraVan.Contracts.Contants.EndpointContants.Pi;
namespace chd.CaraVan.Web.Endpoints
{
    public static class PiEndpoint
    {
        public static IEndpointRouteBuilder AddPi(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(ROOT).WithTags(ROOT);

            group.MapGet(GET_PIN_STATE, GetPinState);
            group.MapGet(GET_SETTING, GetGpios);
            group.MapPost(POST_WRITE_PIN, WritePinState);
            return app;
        }
        private static async Task<bool> GetPinState(int pin, IPiManager manager, CancellationToken cancellationToken) => await manager.Read(pin, cancellationToken);
        private static async Task<IEnumerable<GpioPinDto>> GetGpios(IPiManager manager, CancellationToken cancellationToken) => await manager.GetGpioPins(cancellationToken);

        private static async Task WritePinState(PinWriteDto dto, IPiManager manager, CancellationToken cancellationToken) => await manager.Write(dto, cancellationToken);

    }
}
