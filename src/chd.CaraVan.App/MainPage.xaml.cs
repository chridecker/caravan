#if ANDROID
using AndroidX.Activity;
using chd.CaraVan.App.Platforms.Android;
#endif
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Maui.Platform;


namespace chd.CaraVan.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            try
            {
                this.blazorWebView.BlazorWebViewInitializing += this.BlazorWebViewInitializing;
                this.blazorWebView.BlazorWebViewInitialized += this.BlazorWebViewInitialized;
            }
            catch (Exception ex)
            {
                // do something if error appears
            }
        }

        private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e);
        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e);

#if WINDOWS
        private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
        {
        }

        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
        {

        }
#endif


#if ANDROID

        // To manage Android permissions, update AndroidManifest.xml to include the permissions and
        // features required by your app. You may have to perform additional configuration to enable
        // use of those APIs from the WebView, as is done below. A custom WebChromeClient is needed
        // to define what happens when the WebView requests a set of permissions. See
        // PermissionManagingBlazorWebChromeClient.cs to explore the approach taken in this example.

        private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
        {
        }

        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
        {
            try
            {
                if (e.WebView.Context?.GetActivity() is not ComponentActivity activity)
                {
                    throw new InvalidOperationException($"The permission-managing WebChromeClient requires that the current activity be a '{nameof(ComponentActivity)}'.");
                }

                e.WebView.Settings.JavaScriptEnabled = true;
                e.WebView.Settings.AllowFileAccess = true;
                e.WebView.Settings.MediaPlaybackRequiresUserGesture = false;
                var webChromeClient = new PermissionManagingBlazorWebChromeClient(e.WebView.WebChromeClient!, activity);
                e.WebView.SetWebChromeClient(webChromeClient);
            }
            catch (Exception)
            {
                // do something if error appears
            }

        }
#endif
    }
}
