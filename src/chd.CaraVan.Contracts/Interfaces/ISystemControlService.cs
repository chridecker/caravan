using chd.CaraVan.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface ISystemControlService
    {
        event EventHandler SettingsChanged;
        Task<HomeSettingDto> GetCurrentSettingAsync(CancellationToken cancellationToken  = default); 

        Task SetSettingsAsync(HomeSettingDto dto, CancellationToken cancellationToken= default);
    }
}
