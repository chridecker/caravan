using Blazored.Modal;
using chd.CaraVan.App.Extensions;
using chd.CaraVan.Contracts.Contants;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;
namespace chd.CaraVan.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Configuration.AddConfiguration(GetLocalSetting());
            builder.AddServices();
            return builder.Build();
        }
        private static IConfiguration GetLocalSetting()
        {
            var dict = new Dictionary<string, string>();
            dict[$"ApiKeys:chdScoringApi"] = "http://localhost:8080/";
            if (Preferences.ContainsKey(SettingConstants.BaseAddress))
            {
                var pref = Preferences.Default.Get<string>(SettingConstants.BaseAddress, string.Empty);
                dict[$"ApiKeys:chdScoringApi"] = pref;
            }
            return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
        }

        private static void AddServices(this MauiAppBuilder builder)
        {
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddAppUi(builder.Configuration);
        }
    }
}
