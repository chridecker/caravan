using chd.UI.Base.Contracts.Interfaces.Services.Base;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface ISettingManager : IBaseClientSettingManager
    {
        Task<string> MainUrl { get; }
        Task UpdateMainUrl(string url);

        T? GetNativSetting<T>(string key) where T : class;
        void SetNativSetting<T>(string key, T value) where T : class;
    }
}
