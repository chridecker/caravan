using chd.CaraVan.Devices.Contracts.Dtos.Pi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Contracts.Interfaces
{
   public interface IPiManager
    {
        Task<PiSettings> GetSettings(CancellationToken cancellationToken = default);
        Task<bool> Read(int pin);
        void Start();
        void Stop();
        Task Write(int pin, bool val);
    }
}
