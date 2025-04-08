using chd.Caravan.Mobile.UI.Dtos;
using chd.Caravan.Mobile.UI.Interfaces;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.Services
{
    public class BLEManager : IBLEManager
    {
        private readonly IBluetoothLE _bluetoothLE;
        private readonly IAdapter _adapter;

        public bool IsRunning => this._bluetoothLE.IsOn;
        public bool IsAvailable => this._bluetoothLE.IsAvailable;

        public EventHandler<BLEDeviceFoundArgs> DeviceDiscoverd { get; set; }

        public BLEManager(IBluetoothLE bluetoothLE, IAdapter adapter)
        {
            this._bluetoothLE = bluetoothLE;
            this._adapter = adapter;
            this._adapter.DeviceDiscovered += this._adapter_DeviceDiscovered;
        }

        public async Task<bool> StartAsync(CancellationToken cancellationToken = default)
        {
            if (this._bluetoothLE.State is not BluetoothState.On or BluetoothState.TurningOn)
            {
                await this._bluetoothLE.TrySetStateAsync(true);
            }
            await this._adapter.StartScanningForDevicesAsync(cancellationToken: cancellationToken);
            return this.IsRunning;
        }

        public async Task<bool> ConnectDeviceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (this._adapter.ConnectedDevices.Any(a => a.Id == id)) { return true; }

            try
            {
                _ = await this._adapter.ConnectToKnownDeviceAsync(id, cancellationToken: cancellationToken);
                return true;
            }
            catch (DeviceConnectionException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public async Task<bool> SubscribeForServiceCharacteristicAsync(Guid deviceId, Guid serviceId, Guid characteristicId, CancellationToken cancellationToken = default)
        {
            var device = this._adapter.ConnectedDevices.FirstOrDefault(x => x.Id == deviceId);
            if (device == null) { return false; }

            var service = await device.GetServiceAsync(serviceId, cancellationToken: cancellationToken);
            if (service == null) { return false; }

            var characteristic = await service.GetCharacteristicAsync(characteristicId, cancellationToken);
            if (characteristic == null) { return false; }

            characteristic.ValueUpdated += this.Characteristic_ValueUpdated;
            await characteristic.StartUpdatesAsync(cancellationToken);
            return true;
        }

        private void Characteristic_ValueUpdated(object? sender, CharacteristicUpdatedEventArgs e)
        {
            var device = e.Characteristic.Service.Device;
            var data = e.Characteristic.Value;

        }

        private void _adapter_DeviceDiscovered(object? sender, DeviceEventArgs e)
        {
            var device = e.Device;
            this.DeviceDiscoverd?.Invoke(this, new BLEDeviceFoundArgs(
                new()
                {
                    Id = device.Id,
                    Name = device.Name
                }));
        }

    }
}
