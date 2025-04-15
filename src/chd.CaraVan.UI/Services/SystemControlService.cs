using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.UI.Services
{
    public class SystemControlService : ISystemControlService
    {
        private HomeSettingDto _setting = new();

        public event EventHandler SettingsChanged;

        
        public Task<HomeSettingDto> GetCurrentSettingAsync(CancellationToken cancellationToken = default) => Task.FromResult(this._setting);

        public  Task SetSettingsAsync(HomeSettingDto dto, CancellationToken cancellationToken)
        {
            this._setting = dto;
            this.OnSettingChanged();
            return Task.CompletedTask;
        }

        private void OnSettingChanged()
        {
            this.SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
