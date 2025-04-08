using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Activity;
using AndroidX.Core.View;
using chd.UI.Base.Contracts.Interfaces.Services;

namespace chd.Caravan.Mobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private readonly IAppInfoService _appInfoService;

        public MainActivity()
        {
            this._appInfoService = IPlatformApplication.Current.Services.GetService<IAppInfoService>();
        }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.OnBackPressedDispatcher.AddCallback(this, new BackPress(this._appInfoService));

            this.Window?.AddFlags(WindowManagerFlags.Fullscreen);

            WindowCompat.SetDecorFitsSystemWindows(this.Window, false);
            WindowInsetsControllerCompat windowInsetsController = new WindowInsetsControllerCompat(this.Window, this.Window.DecorView);
            // Hide system bars
            windowInsetsController.Hide(WindowInsetsCompat.Type.SystemBars());
            windowInsetsController.SystemBarsBehavior = WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe;
        }

        class BackPress : OnBackPressedCallback
        {
            private readonly IAppInfoService _appInfoService;

            public BackPress(IAppInfoService appInfoService) : base(true)
            {
                this._appInfoService = appInfoService;
            }

            public override void HandleOnBackPressed()
            {
                var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
                if (navigation is not null && navigation.ModalStack.Count > 0)
                {
                    Task.Run(navigation.PopModalAsync);
                }
                else
                {

                    this._appInfoService.BackButtonPressed?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
