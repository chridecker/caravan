using chd.CaraVan.Contracts.Dtos;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface ISystemManager
    {
        Task Reboot(CancellationToken cancellationToken = default);
        Task ChangeStateInTime(ServiceControlDto dto, CancellationToken cancellationToken = default);
        Task<DateTime?> IsServiceRunning(string service, CancellationToken cancellationToken = default);
        Task<bool> StartService(ServiceControlDto dto, CancellationToken cancellationToken = default);
        Task<bool> StopService(ServiceControlDto dto, CancellationToken cancellationToken = default);
    }
}
