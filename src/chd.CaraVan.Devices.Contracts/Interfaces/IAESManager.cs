using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Contracts.Interfaces
{
    public interface IAESManager
    {
        Task Off();
        Task CheckForActive();
        Task SetActive();
        Task<bool> IsActive { get; }
        Task<DateTime?> SolarAesOffSince { get; }
        Task<decimal?> BatteryLimit { get; }
        Task<decimal?> SolarAmpLimit { get; }
        Task<TimeSpan?> AesTimeout { get; }

        event EventHandler<bool> StateSwitched;
    }
}
