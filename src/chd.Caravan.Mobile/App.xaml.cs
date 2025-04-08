using chd.UI.Base.Contracts.Enum;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Platform = Microsoft.Maui.ApplicationModel.Platform;
namespace chd.Caravan.Mobile
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
                Platform.CurrentActivity.Window.SetNavigationBarColor(Color.FromRgba("#181B1F").ToAndroid());

                var mainWindow = base.CreateWindow(activationState);

                mainWindow.Deactivated += (sender, args) => this._appInfoService.AppLifeCycleChanged?.Invoke(this, EAppLifeCycle.OnSleep);
                mainWindow.Resumed += (sender, args) => this._appInfoService.AppLifeCycleChanged?.Invoke(this, EAppLifeCycle.OnResume);

                return mainWindow;
            }
        }
    }
}
