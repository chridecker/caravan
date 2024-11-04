using chd.CaraVan.Contracts.Dtos;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface IPiManager
    {
        Task<IEnumerable<GpioPinDto>> GetGpioPins(CancellationToken cancellationToken = default);
        Task<bool> Read(int pin, CancellationToken cancellationToken = default);
        Task Start(CancellationToken cancellationToken = default);
        Task Stop(CancellationToken cancellationToken = default);
        Task Write(PinWriteDto dto, CancellationToken cancellationToken = default);
    }
}
