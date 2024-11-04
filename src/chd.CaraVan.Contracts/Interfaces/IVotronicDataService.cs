using chd.CaraVan.Contracts.Dtos;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface IVotronicDataService
    {
        Task AddData(VotronicBatteryData votronicBatteryData, CancellationToken cancellationToken = default);
        Task AddData(VotronicSolarData votronicSolarData, CancellationToken cancellationToken = default);
        Task<VotronicBatteryData> GetBatteryData(CancellationToken cancellationToken = default);
        Task<VotronicSolarData> GetSolarData(CancellationToken cancellationToken = default);
    }
}
