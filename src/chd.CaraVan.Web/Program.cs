using System.Diagnostics;
using NLog.Extensions.Logging;
using chd.CaraVan.Web.Endpoints;
using chd.CaraVan.Web.Extensions;
using chd.CaraVan.Web.Hub;
using chd.Api.Base.Extensions;
using chd.Api.Base;
using chd.CaraVan.Web.Components;
using chd.CaraVan.UI.Components;

if (!(Debugger.IsAttached || args.Contains("--console")))
{
    var pathToExe = Process.GetCurrentProcess()?.MainModule?.FileName ?? string.Empty;
    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
    if (!string.IsNullOrWhiteSpace(pathToContentRoot))
    {
        Directory.SetCurrentDirectory(pathToContentRoot);
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddNLog();

builder.Services.AddBaseApi();
builder.Services.AddWeb(builder.Configuration);
builder.Services.AddSignalR();


builder.Services.AddRazorComponents(options => { options.DetailedErrors = true; })
.AddInteractiveServerComponents();

builder.Services.AddExceptionHandler<APIExceptionHandler>();

builder.Host.UseSystemd();

var app = builder.Build();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseBaseApi();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Routes).Assembly);

app.MapBaseApi("chdCaravan");

app.MapEndpoints();
app.MapHub<DataHub>("data-hub");


app.Run();
