using chd.Caravan.Mobile.UI.Constants;
using chd.Caravan.Mobile.UI.Dtos;
using chd.Caravan.Mobile.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.WPF.Services
{
    public class BLEManager : IBLEManager
    {
        public bool IsRunning => true;

        public bool IsAvailable => true;

        public IEnumerable<BLEDevice> ConnectedDevices => [];

        public event EventHandler<BLEDeviceFoundArgs> DeviceDiscoverd;
        public event EventHandler<BLEDevice> DeviceConnected;
        public event EventHandler<BLECharactersiticsValueArgs> CharacteristicValueUpdated;

        public async Task<bool> ConnectDeviceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            this.DeviceConnected?.Invoke(this, new()
            {
                Id = id,
                Name = "Test",
                UID = "3456789"
            });
            return true;
        }

        public Task<bool> DisconnectDeviceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<BLEService>> GetDeviceServices(Guid id, CancellationToken cancellationToken = default)
        {
            return [new BLEService {
                Id = BLEConstants.HEART_REATE,
                Name = "HearRate"
                } ];
        }

        public async Task<IEnumerable<BLECharacteristic>> GetServiceCharactersitics(Guid deviceId, Guid serviceId, CancellationToken cancellationToken = default)
        {
            return [new BLECharacteristic{
                Id = BLEConstants.HR_MEASURMENT,
                Name = "HR",
                CanUpdate = true,
                }];
        }

        public async Task<byte[]> ReadValue(Guid deviceId, Guid serviceId, BLECharacteristic characteristic, CancellationToken cancellationToken = default)
        {
            return [];
        }

        public async Task<bool> StartScanAsync(CancellationToken cancellationToken = default)
        {
            this.DeviceDiscoverd?.Invoke(this, new BLEDeviceFoundArgs(new()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                UID = "3456789"
            }));
            return true;
        }

        public async Task<bool> StopScanAsync(CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> SubscribeForServiceCharacteristicAsync(Guid deviceId, Guid serviceId, Guid characteristicId, CancellationToken cancellationToken = default)
        {
            this.CharacteristicValueUpdated.Invoke(this, new()
            {
                CharacteristicId = characteristicId,
                ServiceId = serviceId,
                DeviceId = deviceId,
                Time = DateTime.Now,
                UID = "3456789",
                Data = [12, 63, 25, 12, 36, 51, 25, 36, 56, 125, 230, 12, 58, 94, 58, 25]
            });
            return true;
        }
    }
}
