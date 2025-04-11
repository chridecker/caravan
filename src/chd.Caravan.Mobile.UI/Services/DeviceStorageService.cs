using chd.Caravan.Mobile.UI.Dtos;
using chd.Caravan.Mobile.UI.Interfaces;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Services
{
    public class DeviceStorageService : IDeviceStorageService
    {
        private const string SAVED_DEVICES_KEY = "SAVED_DEVICES";

        private readonly IBaseClientSettingManager _baseClientSettingManager;

        private List<SavedDevice> _savedDevices;

        public DeviceStorageService(IBaseClientSettingManager baseClientSettingManager)
        {
            this._baseClientSettingManager = baseClientSettingManager;
        }

        public async Task AddDevice(SavedDevice dto)
        {
            if (this._savedDevices is null) { this._savedDevices = []; }
            this._savedDevices.Add(dto);

            await this._baseClientSettingManager.StoreSettingLocal(SAVED_DEVICES_KEY, this._savedDevices);
        }
        public async Task RemoveDevice(SavedDevice dto)
        {
            if (!this._savedDevices.Any(a => a.Id == dto.Id)) { return; }
            this._savedDevices.Remove(dto);
            await this._baseClientSettingManager.StoreSettingLocal(SAVED_DEVICES_KEY, this._savedDevices);
        }
        public async Task<List<SavedDevice>> GetDevices()
        {
            if (this._savedDevices is null)
            {
                this._savedDevices = await this._baseClientSettingManager.GetSettingLocal<List<SavedDevice>>(SAVED_DEVICES_KEY);
            }
            return this._savedDevices;
        }

    }
}
