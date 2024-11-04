using chd.CaraVan.UI.Services;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace chd.CaraVan.App.Services
{
    public class SettingManager : BaseSettingManager
    {
        private string _mainUrl;
        private readonly IConfiguration _configuration;

        public event EventHandler<string> AutoRedirectToChanged;

        public SettingManager(ILogger<SettingManager> logger, IConfiguration configuration,
            IProtecedLocalStorageHandler protecedLocalStorageHandler,
            NavigationManager navigationManager) : base(logger, configuration, protecedLocalStorageHandler, navigationManager)
        {
            this._configuration = configuration;
        }

        public override T? GetNativSetting<T>(string key) where T : class
        {
            if (Preferences.ContainsKey(key))
            {
                return Preferences.Default.Get<T>(key, default(T));
            }
            return default(T);
        }

        public override void SetNativSetting<T>(string key, T value) where T : class
        {
            Preferences.Default.Set<T>(key, value);
        }

    }
}
