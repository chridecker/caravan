using chd.CaraVan.Web.Components;
using System.Diagnostics;
using NLog.Extensions.Logging;
using chd.CaraVan.Web.Endpoints;
using chd.CaraVan.Web.Extensions;
using chd.CaraVan.Web.Hub;

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

builder.Services.AddSignalR();
builder.Services.AddServer(builder.Configuration);
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddCircuitOptions(opt =>
{
    opt.DetailedErrors = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSystemd();

var app = builder.Build();

app.UseExceptionHandler("/Error", createScopeForErrors: true);

app.UseStaticFiles();
app.UseAntiforgery();

app.MapEndpoints();

app.MapHub<DataHub>("data-hub");

app.Run();
