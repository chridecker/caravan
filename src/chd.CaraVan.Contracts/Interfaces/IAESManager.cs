namespace chd.CaraVan.Contracts.Interfaces
{
    public interface IAESManager
    {
        Task Off(CancellationToken cancellationToken = default);
        Task CheckForActive(CancellationToken cancellationToken = default);
        Task SetActive(CancellationToken cancellation = default);
        Task<bool> IsActive { get; }
        Task<DateTime?> SolarAesOffSince { get; }
        Task<decimal?> BatteryLimit { get; }
        Task<decimal?> SolarAmpLimit { get; }
        Task<TimeSpan?> AesTimeout { get; }

        event EventHandler<bool> StateSwitched;
    }
}
