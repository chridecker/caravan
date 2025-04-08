using chd.Caravan.Mobile.UI.Interfaces;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
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
        public bool isAvailable => this._bluetoothLE.IsAvailable;

        public BLEManager(IBluetoothLE bluetoothLE, IAdapter adapter)
        {
            this._bluetoothLE = bluetoothLE;
            this._adapter = adapter;

            this._bluetoothLE.StateChanged += this._bluetoothLE_StateChanged;
        }

        public async Task<bool> StartAsync(CancellationToken cancellationToken = default)
        {
            if (this._bluetoothLE.State is not BluetoothState.On or BluetoothState.TurningOn)
            {
                return await this._bluetoothLE.TrySetStateAsync(true);
            }
            return this.IsRunning;
        }

        private void _bluetoothLE_StateChanged(object? sender, BluetoothStateChangedArgs e)
        {
        }
    }
}
