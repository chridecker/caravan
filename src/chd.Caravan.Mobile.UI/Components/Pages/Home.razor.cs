using Blazored.Modal;
using Blazored.Modal.Services;
using chd.Caravan.Mobile.UI.Components.Shared;
using chd.Caravan.Mobile.UI.Constants;
using chd.Caravan.Mobile.UI.Dtos;
using chd.Caravan.Mobile.UI.Interfaces;
using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Shared.UI.Dtos;
using chd.UI.Base.Contracts.Extensions;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace chd.Caravan.Mobile.UI.Components.Pages
{
    public partial class Home
    {
        [Inject] IDeviceStorageService deviceStorageService { get; set; }
        [Inject] IModalService modal { get; set; }
        [Inject] IBLEManager bLEManager { get; set; }
        [Inject] INotificationManagerService _notificationManagerService { get; set; }

        List<SavedDevice> _savedDevices = [];
        List<BLEDevice> connectedDevices = [];
        BLEDevice _currentSelectedDevice = null;
        Dictionary<Guid, RuuviTagData> _ruuviData = [];
        VotronicSolarData _solarData = null;
        VotronicBatteryData _batteryData = null;
        HeartRateData _heartRateData = null;

        protected override async Task OnInitializedAsync()
        {
            this._savedDevices = (await deviceStorageService.GetDevices()) ?? [];
            connectedDevices.AddRange(bLEManager.ConnectedDevices);
            foreach (var device in connectedDevices)
            {
                if (this.IsSaved(device))
                {
                    await this.StartDevice(device);
                }
            }

            bLEManager.DeviceConnected += this.BleDevice_Connected;
            bLEManager.CharacteristicValueUpdated += this.CharacUpdate;

            var x = new byte[]{1,2,3,4};
            BitConverter.ToInt16(x);

            foreach (var savedDevice in _savedDevices)
            {
                await bLEManager.ConnectDeviceAsync(savedDevice.Id);
            }

            await base.OnInitializedAsync();
        }

        private async Task SelectDevice(BLEDevice device)
        {
            this._currentSelectedDevice = device;
            await this.InvokeAsync(this.StateHasChanged);
        }

        private async Task DisconnectDevice()
        {
            await bLEManager.DisconnectDeviceAsync(this._currentSelectedDevice.Id);
            this._currentSelectedDevice.IsStarted = false;
            this.connectedDevices.Remove(this._currentSelectedDevice);
            this._currentSelectedDevice = null;
            await this.InvokeAsync(this.StateHasChanged);
        }

        private async Task ScanServices()
        {
            var modalParam = new ModalParameters()
        {
            { nameof(BLEServiceComponent.Device),this._currentSelectedDevice }
        };
            await this.modal.Show<BLEServiceComponent>("Services", modalParam).Result;
        }

        private async Task StartDevice(BLEDevice device)
        {

            var svcs = await this.bLEManager.GetDeviceServices(device.Id);
            if (svcs.Any(a => a.Id == BLEConstants.BATTERY_SVC))
            {
                var batterySvc = svcs.FirstOrDefault(x => x.Id == BLEConstants.BATTERY_SVC);
                var levelCharac = (await this.bLEManager.GetServiceCharactersitics(device.Id, batterySvc.Id)).First(x => x.Id == BLEConstants.BATTERY_LEVEL);
                if (levelCharac != null && levelCharac.CanUpdate)
                {
                    await this.bLEManager.SubscribeForServiceCharacteristicAsync(device.Id, batterySvc.Id, levelCharac.Id);
                    device.IsStarted = true;
                }
            }

            if (device.Type is Enums.EDeviceType.Ruuvi)
            {
                await this.bLEManager.SubscribeForServiceCharacteristicAsync(device.Id, BLEConstants.NORDIC_UART_SVC, BLEConstants.TX_CHARACTERISTIC);
                device.IsStarted = true;
            }
            else if (device.Type is Enums.EDeviceType.Votronic)
            {
                await this.bLEManager.SubscribeForServiceCharacteristicAsync(device.Id, BLEConstants.VOTRONIC_SVC, BLEConstants.BATTERY_CHARACTERISTIC);
                await this.bLEManager.SubscribeForServiceCharacteristicAsync(device.Id, BLEConstants.VOTRONIC_SVC, BLEConstants.SOLAR_CHARACTERISTIC);
                device.IsStarted = true;
            }
            else if (device.Type is Enums.EDeviceType.HeartRate)
            {
                await this.bLEManager.SubscribeForServiceCharacteristicAsync(device.Id, BLEConstants.HEART_REATE, BLEConstants.HR_MEASURMENT);
                device.IsStarted = true;
            }
            await this.InvokeAsync(this.StateHasChanged);
        }

        private async Task SaveDevice()
        {
            if (this._currentSelectedDevice.Type is Enums.EDeviceType.Default) { return; }

            var saved = new SavedDevice()
            {
                Id = this._currentSelectedDevice.Id,
                Name = string.IsNullOrWhiteSpace(_currentSelectedDevice.DisplayName) ? _currentSelectedDevice.Name : _currentSelectedDevice.DisplayName,
                Type = this._currentSelectedDevice.Type,
                UID = this._currentSelectedDevice.UID
            };
            await this.deviceStorageService.AddDevice(saved);
            this._savedDevices = await this.deviceStorageService.GetDevices();
        }
        private async Task DeleteDevice()
        {
            var saved = this._savedDevices.FirstOrDefault(x => x.Id == this._currentSelectedDevice.Id);

            await this.deviceStorageService.RemoveDevice(saved);
            this._savedDevices = await this.deviceStorageService.GetDevices();
        }


        private async Task OpenScanModal()
        {
            var res = await this.modal.Show<ScanDevicesComponent>
        ("Scan Devices").Result;
            await bLEManager.StopScanAsync();
            if (!res.Cancelled && res.Data is BLEDevice device)
            {
                await this.AddDevice(device);
            }
        }

        private async Task OpenDeviceSettings()
        {
            var param = new ModalParameters { { nameof(DeviceSettingComponent.Device), this._currentSelectedDevice } };
            var modalResult = await this.modal.Show<DeviceSettingComponent>
                ("Device Settings", param).Result;
            await this.InvokeAsync(this.StateHasChanged);
        }

        private async void CharacUpdate(object? sender, BLECharactersiticsValueArgs args)
        {
            if (!connectedDevices.Any(a => a.Id == args.DeviceId)) { return; }

            var device = this.connectedDevices.FirstOrDefault(x => x.Id == args.DeviceId);

            if (args.ServiceId == BLEConstants.BATTERY_SVC
                && args.CharacteristicId == BLEConstants.BATTERY_LEVEL)
            {
                device.BatteryLevel = args.Data;
            }

            if (device.Type == Enums.EDeviceType.Ruuvi
            && args.ServiceId == BLEConstants.NORDIC_UART_SVC
            && args.CharacteristicId == BLEConstants.TX_CHARACTERISTIC)
            {
                this._ruuviData[device.Id] = new RuuviTagData(args.Data);
            }
            else if (device.Type == Enums.EDeviceType.Votronic
            && args.ServiceId == BLEConstants.VOTRONIC_SVC)
            {
                if (args.CharacteristicId == BLEConstants.SOLAR_CHARACTERISTIC)
                {
                    var oldAes = this._solarData?.AES ?? false;
                    this._solarData = new(args.Data);
                    if (oldAes != this._solarData.AES)
                    {
                        this._notificationManagerService.SendNotification($"Votronic Solar", $"AES [{(this._solarData.AES ? "ON" : "OFF")}]");
                    }
                }
                else if (args.CharacteristicId == BLEConstants.BATTERY_CHARACTERISTIC)
                {
                    this._batteryData = new(args.Data, 200);
                }
            }
            else if (device.Type == Enums.EDeviceType.HeartRate
            && args.ServiceId == BLEConstants.HEART_REATE
            && args.CharacteristicId == BLEConstants.HR_MEASURMENT)
            {
                this._heartRateData = new HeartRateData(args.Data);
                this._heartRateData.Name = !string.IsNullOrWhiteSpace(device.DisplayName) ? device.DisplayName : device.Name;
            }
            await this.InvokeAsync(this.StateHasChanged);
        }


        private async void BleDevice_Connected(object? sender, BLEDevice device)
        {
            if (connectedDevices.Any(a => a.Id == device.Id)) { return; }
            connectedDevices.Add(device);
            if (this.IsSaved(device))
            {
                await this.StartDevice(device);
            }
            await this.InvokeAsync(this.StateHasChanged);
        }

        private bool IsSaved(BLEDevice device)
        {
            var startAuto = this._savedDevices.Any(a => a.Id == device.Id);
            if (startAuto)
            {
                var sDev = _savedDevices.FirstOrDefault(a => a.Id == device.Id);
                device.DisplayName = sDev.Name;
                device.Type = sDev.Type;
            }
            return startAuto;
        }

        private async Task AddDevice(BLEDevice device)
        {
            await this.bLEManager.ConnectDeviceAsync(device.Id);
            await this.InvokeAsync(this.StateHasChanged);
        }
    }
}