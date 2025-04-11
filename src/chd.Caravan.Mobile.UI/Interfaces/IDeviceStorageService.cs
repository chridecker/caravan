using chd.Caravan.Mobile.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Interfaces
{
    public interface IDeviceStorageService
    {
        Task AddDevice(SavedDevice dto);
        Task<List<SavedDevice>> GetDevices();
        Task RemoveDevice(SavedDevice dto);
    }
}
