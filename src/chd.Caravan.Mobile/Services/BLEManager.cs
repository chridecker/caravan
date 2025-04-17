using Android.Bluetooth;
using Android.DeviceLock;
using chd.Caravan.Mobile.UI.Dtos;
using chd.Caravan.Mobile.UI.Interfaces;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.Services
{
    public class BLEManager : IBLEManager
    {
        private IBluetoothLE _bluetoothLE => CrossBluetoothLE.Current;
        private IAdapter _adapter => this._bluetoothLE.Adapter;

        public bool IsRunning => this._bluetoothLE.IsOn;
        public bool IsAvailable => this._bluetoothLE.IsAvailable;

        public IEnumerable<BLEDevice> ConnectedDevices
        {
            get
            {
                var lst = new List<BLEDevice>();
                foreach (var conn in this._adapter.ConnectedDevices)
                {
                    if (conn.NativeDevice is BluetoothDevice bt)
                    {
                        lst.Add(new()
                        {
                            Id = conn.Id,
                            Name = conn.Name,
                            UID = bt.Address
                        });
                    }
                }
                return lst;
            }
        }
        public event EventHandler<BLEDeviceFoundArgs> DeviceDiscoverd;
        public event EventHandler<BLECharactersiticsValueArgs> CharacteristicValueUpdated;
        public event EventHandler<BLEDevice> DeviceConnected;

        public BLEManager()
        {
            this._adapter.DeviceDiscovered += this._adapter_DeviceDiscovered;
            this._adapter.DeviceConnected += this._adapter_DeviceConnected;
        }

        public async Task<bool> StartScanAsync(CancellationToken cancellationToken = default)
        {
            if (this._bluetoothLE.State is not BluetoothState.On or BluetoothState.TurningOn)
            {
                await this._bluetoothLE.TrySetStateAsync(true);
            }
            if (!this._adapter.IsScanning)
            {
                await this._adapter.StartScanningForDevicesAsync(cancellationToken: cancellationToken);
            }
            return this._adapter.IsScanning;
        }

        public async Task<bool> StopScanAsync(CancellationToken cancellationToken = default)
        {
            if (this._adapter.IsScanning)
            {
                await this._adapter.StopScanningForDevicesAsync();
            }
            return !this._adapter.IsScanning;
        }



        public async Task<bool> ConnectDeviceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (this._adapter.ConnectedDevices.Any(a => a.Id == id)) { return true; }
            {
                try
                {
                    if (this._bluetoothLE.State is not BluetoothState.On or BluetoothState.TurningOn)
                    {
                        await this._bluetoothLE.TrySetStateAsync(true);
                    }
                    _ = await this._adapter.ConnectToKnownDeviceAsync(id, cancellationToken: cancellationToken);
                    return true;
                }
                catch (DeviceConnectionException ex)
                {

                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }

        public async Task<bool> DisconnectDeviceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (!this._adapter.ConnectedDevices.Any(a => a.Id == id)) { return true; }

            {
                try
                {
                    await this._adapter.DisconnectDeviceAsync(this._adapter.ConnectedDevices.FirstOrDefault(a => a.Id == id), cancellationToken: cancellationToken);
                    return true;
                }
                catch (DeviceConnectionException ex)
                {

                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }

        private void _adapter_DeviceConnected(object? sender, DeviceEventArgs e)
        {
            var device = e.Device;
            if (device.NativeDevice is BluetoothDevice navtiveDevive
                && !string.IsNullOrWhiteSpace(navtiveDevive.Address))
            {
                this.DeviceConnected?.Invoke(this, new BLEDevice
                {
                    Id = device.Id,
                    UID = navtiveDevive.Address,
                    Name = device.Name,
                });
            }
        }

        public async Task<IEnumerable<BLEService>> GetDeviceServices(Guid id, CancellationToken cancellationToken = default)
        {
            var device = this._adapter.ConnectedDevices.FirstOrDefault(x => x.Id == id);
            if (device == null) { return []; }
            var services = await device.GetServicesAsync(cancellationToken);
            return services.Select(s => new BLEService
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        public async Task<byte[]> ReadValue(Guid deviceId, Guid serviceId, BLECharacteristic characteristic, CancellationToken cancellationToken = default)
        {
            var device = this._adapter.ConnectedDevices.FirstOrDefault(x => x.Id == deviceId);
            if (device == null) { return []; }

            var service = await device.GetServiceAsync(serviceId, cancellationToken);
            if (service is null) { return []; }
            var c = await service.GetCharacteristicAsync(characteristic.Id, cancellationToken);
            var data = await c.ReadAsync(cancellationToken);
            return data.data ?? [];
        }

        public async Task<IEnumerable<BLECharacteristic>> GetServiceCharactersitics(Guid deviceId, Guid serviceId, CancellationToken cancellationToken = default)
        {
            var device = this._adapter.ConnectedDevices.FirstOrDefault(x => x.Id == deviceId);
            if (device == null) { return []; }

            var service = await device.GetServiceAsync(serviceId, cancellationToken);
            if (service is null) { return []; }
            var characteristivs = await service.GetCharacteristicsAsync(cancellationToken);

            var ret =  characteristivs.Select(s => new BLECharacteristic
            {
                Id = s.Id,
                Name = s.Name,
                CanRead = s.CanRead,
                CanUpdate = s.CanUpdate,
                CanWrite = s.CanWrite,
            });

            return ret;
        }

        public async Task<bool> SubscribeForServiceCharacteristicAsync(Guid deviceId, Guid serviceId, Guid characteristicId, CancellationToken cancellationToken = default)
        {
            var device = this._adapter.ConnectedDevices.FirstOrDefault(x => x.Id == deviceId);
            if (device == null) { return false; }

            var service = await device.GetServiceAsync(serviceId, cancellationToken: cancellationToken);
            if (service == null) { return false; }

            var characteristic = await service.GetCharacteristicAsync(characteristicId, cancellationToken);
            if (characteristic == null || !characteristic.CanUpdate) { return false; }

            characteristic.ValueUpdated += this.Characteristic_ValueUpdated;
            await characteristic.StartUpdatesAsync(cancellationToken);
            return true;
        }

        private void Characteristic_ValueUpdated(object? sender, CharacteristicUpdatedEventArgs e)
        {
            var data = e.Characteristic.Value;
            var service = e.Characteristic.Service;
            var device = service.Device;
            if (device.NativeDevice is BluetoothDevice nativeDevice
                && !string.IsNullOrWhiteSpace(nativeDevice.Address))
            {
                this.CharacteristicValueUpdated?.Invoke(this, new BLECharactersiticsValueArgs
                {
                    CharacteristicId = e.Characteristic.Id,
                    Data = data,
                    DeviceId = e.Characteristic.Service.Device.Id,
                    ServiceId = e.Characteristic.Service.Id,
                    Time = DateTime.Now,
                    UID = nativeDevice.Address
                });
            }

        }

        private void _adapter_DeviceDiscovered(object? sender, DeviceEventArgs e)
        {
            var device = e.Device;
            if (device.NativeDevice is BluetoothDevice bDevice
                && !string.IsNullOrWhiteSpace(bDevice.Address))
            {
                this.DeviceDiscoverd?.Invoke(this, new BLEDeviceFoundArgs(
                    new()
                    {
                        Id = device.Id,
                        UID = bDevice.Address,
                        Name = device.Name,
                    }));
            }
        }

    }
}
