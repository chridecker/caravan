using chd.CaraVan.Contracts.Dtos;

namespace chd.CaraVan.Devices.Contracts.Interfaces
{
   public interface IRuuviTagDataService
    {
        Task<IEnumerable<RuuviDeviceDto>> Devices { get; }

        Task AddData(int id, RuuviTagDeviceData data, CancellationToken cancellationToken = default);
        Task<RuuviSensorDataDto> GetData(int id, CancellationToken cancellationToken = default);
    }
}
