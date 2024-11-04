using chd.CaraVan.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Contracts.Interfaces
{
    public interface IVotronicDataService
    {
        Task AddData(VotronicBatteryData votronicBatteryData, CancellationToken cancellationToken = default);
        Task AddData(VotronicSolarData votronicSolarData, CancellationToken cancellationToken = default);
        Task<VotronicBatteryData> GetBatteryData(CancellationToken cancellationToken = default);
        Task<VotronicSolarData> GetSolarData(CancellationToken cancellationToken = default);
    }
}
