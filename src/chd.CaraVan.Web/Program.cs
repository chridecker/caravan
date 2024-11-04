using System.Diagnostics;
using NLog.Extensions.Logging;
using chd.CaraVan.Web.Endpoints;
using chd.CaraVan.Web.Extensions;
using chd.CaraVan.Web.Hub;
using chd.Api.Base.Extensions;

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

builder.Services.AddBaseApi("chdCaraVanAPI");
builder.Services.AddServer(builder.Configuration);
builder.Services.AddSignalR();

builder.Host.UseSystemd();

var app = builder.Build();
app.UseBaseApi();

app.MapEndpoints();
app.MapHub<DataHub>("data-hub");


app.Run();
