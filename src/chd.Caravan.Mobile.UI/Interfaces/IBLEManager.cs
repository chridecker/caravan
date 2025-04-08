using chd.Caravan.Mobile.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Interfaces
{
    public interface IBLEManager
    {
        EventHandler<BLEDeviceFoundArgs> DeviceDiscoverd { get; set; }
        bool IsRunning { get; }
        bool IsAvailable { get; }

        Task<bool> StartAsync(CancellationToken cancellationToken = default);
    }
}
