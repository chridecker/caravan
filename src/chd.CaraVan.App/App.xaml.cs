using chd.UI.Base.Contracts.Enum;
using chd.UI.Base.Contracts.Interfaces.Services;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif
using Platform = Microsoft.Maui.ApplicationModel.Platform;

namespace chd.CaraVan.App
{
    public partial class App : Application
    {
        private readonly IAppInfoService _appInfoService;

        public App(IAppInfoService appInfoService)
        {
            InitializeComponent();

            MainPage = new MainPage();
            this._appInfoService = appInfoService;
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            {
#if ANDROID
                Platform.CurrentActivity.Window.SetNavigationBarColor(Color.FromRgba("#181B1F").ToAndroid());
#endif

                var mainWindow = base.CreateWindow(activationState);

                mainWindow.Deactivated += (sender, args) => this._appInfoService.AppLifeCycleChanged?.Invoke(this, EAppLifeCycle.OnSleep);
                mainWindow.Resumed += (sender, args) => this._appInfoService.AppLifeCycleChanged?.Invoke(this, EAppLifeCycle.OnResume);

                return mainWindow;
            }
        }
    }
}
