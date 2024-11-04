using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Contracts.Interfaces
{
    public interface ISystemManager
    {
        Task Reboot(CancellationToken cancellationToken = default);
        void ChangeStateInTime(string service, TimeSpan span, CancellationToken cancellationToken = default);
        Task<DateTime?> IsServiceRunning(string service, CancellationToken cancellationToken = default);
        Task<bool> StartService(string service, CancellationToken cancellationToken = default);
        Task<bool> StopService(string service, CancellationToken cancellationToken = default);
    }
}
