using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Devices.Contracts.Interfaces;
using System.Xml;
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
        private static async Task<bool> GetPinState(int pin, IPiManager manager) => await manager.Read(pin);
        private static IEnumerable<GpioPinDto> GetGpios(IPiManager manager) => manager.GetGpioPins();

        private static async Task WritePinState(PinWriteDto dto, IPiManager manager, CancellationToken cancellationToken) => await manager.Write(dto.Pin, dto.Value);

    }
}
