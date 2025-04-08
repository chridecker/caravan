using chd.Caravan.Mobile.UI.Interfaces;
using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.Services
{
    public class SettingManager : BaseClientSettingManager<int, int>, ISettingManager
    {
        public SettingManager(ILogger<SettingManager> logger, IProtecedLocalStorageHandler protecedLocalStorageHandler,
            NavigationManager navigationManager) : base(logger,  protecedLocalStorageHandler, navigationManager)
        {
        }

        public T? GetNativSetting<T>(string key) where T : class
        {
            if (Preferences.ContainsKey(key))
            {
                return Preferences.Default.Get<T>(key, default(T));
            }
            return default(T);
        }

        public void SetNativSetting<T>(string key, T value) where T : class
        {
            Preferences.Default.Set<T>(key, value);
        }
    }
}
