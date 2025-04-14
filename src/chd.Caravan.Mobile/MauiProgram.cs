using Blazored.Modal;
using chd.Caravan.Mobile.Extensions;
using chd.Caravan.Mobile.Services;
using chd.Caravan.Mobile.UI.Constants;
using chd.Caravan.Mobile.UI.Extensions;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace chd.Caravan.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            
            builder.Configuration.AddConfiguration(GetLocalSetting());

            builder.AddServices();
            return builder.Build();
        }
        private static void AddServices(this MauiAppBuilder builder)
        {
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddAppServices(builder.Configuration);
        }

         private static IConfiguration GetLocalSetting()
        {
            if (Preferences.ContainsKey(SettingConstants.BaseAddress))
            {
                var pref = Preferences.Default.Get<string>(SettingConstants.BaseAddress, "http://localhost:8081");
                var dict = new Dictionary<string, string>()
                {
                    {$"ApiKeys:chdCaravanApi",pref }
                };
                return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
            }
            return new ConfigurationBuilder().Build();
        }
    }
}
