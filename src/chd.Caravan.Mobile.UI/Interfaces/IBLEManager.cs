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
        event EventHandler<BLEDeviceFoundArgs> DeviceDiscoverd;
        event EventHandler<BLEDevice> DeviceConnected;
        event EventHandler<BLECharactersiticsValueArgs> CharacteristicValueUpdated;

        bool IsRunning { get; }
        bool IsAvailable { get; }

        Task<bool> StartScanAsync(CancellationToken cancellationToken = default);
        Task<bool> ConnectDeviceAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> StopScanAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<BLEService>> GetDeviceServices(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BLECharacteristic>> GetServiceCharactersitics(Guid deviceId, Guid serviceId, CancellationToken cancellationToken = default);
        Task<byte[]> ReadValue(Guid deviceId, Guid serviceId, BLECharacteristic characteristic, CancellationToken cancellationToken = default);
        Task<bool> DisconnectDeviceAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SubscribeForServiceCharacteristicAsync(Guid deviceId, Guid serviceId, Guid characteristicId, CancellationToken cancellationToken = default);
    }
}
