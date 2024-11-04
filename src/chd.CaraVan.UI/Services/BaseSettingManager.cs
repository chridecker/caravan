using chd.Api.Base.Client.Extensions;
using chd.CaraVan.Contracts.Contants;
using chd.CaraVan.Contracts.Interfaces;
using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace chd.CaraVan.UI.Services
{
    public abstract class BaseSettingManager : BaseClientSettingManager<int, int>, ISettingManager
    {
        protected string _mainUrl;
        private readonly IConfiguration _configuration;

        public event EventHandler<string> AutoRedirectToChanged;

        public BaseSettingManager(ILogger<BaseSettingManager> logger, IConfiguration configuration,
            IProtecedLocalStorageHandler protecedLocalStorageHandler,
            NavigationManager navigationManager) : base(logger, protecedLocalStorageHandler, navigationManager)
        {
            this._configuration = configuration;
        }
        public Task<string> MainUrl => Task.Run(async () =>
        {
            if (string.IsNullOrWhiteSpace(this._mainUrl))
            {
                this._mainUrl = await this.GetSettingLocal<string>(SettingConstants.BaseAddress) ??
                this._configuration.GetApiKey("chdCaravanApi").ToString();
            }
            return this._mainUrl;
        });



        public async Task UpdateMainUrl(string url)
        {
            this._mainUrl = url;
            await this.StoreSettingLocal<string>(SettingConstants.BaseAddress, url);
        }

        public abstract T? GetNativSetting<T>(string key) where T : class;

        public abstract void SetNativSetting<T>(string key, T value) where T : class;

    }
}
