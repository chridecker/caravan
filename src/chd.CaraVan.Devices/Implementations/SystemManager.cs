using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using System.Diagnostics;

namespace chd.CaraVan.Devices.Implementations
{
    public class SystemManager : ISystemManager
    {

        public SystemManager()
        {
        }

        public Task ChangeStateInTime(ServiceControlDto dto, CancellationToken cancellationToken) => _ = StopAfterTime(dto, cancellationToken);

        private Task StopAfterTime(ServiceControlDto dto, CancellationToken cancellationToken) => Task.Run(async () =>
        {
            using var timer = new PeriodicTimer(dto.Time);
            if (await timer.WaitForNextTickAsync(cancellationToken))
            {
                if ((await this.IsServiceRunning(dto.Service)).HasValue)
                {
                    await this.StopService(dto);
                }
                else
                {

                    await this.StartService(dto);
                }
            }
        }, cancellationToken);

        public async Task<bool> StartService(ServiceControlDto dto, CancellationToken cancellationToken = default)
        {
            if (!(await this.IsServiceRunning(dto.Service, cancellationToken)).HasValue)
            {
                await CommandService(dto.Service, "start", cancellationToken);
                return true;
            }
            return false;
        }
        public async Task<bool> StopService(ServiceControlDto dto, CancellationToken cancellationToken = default)
        {
            if ((await this.IsServiceRunning(dto.Service, cancellationToken)).HasValue)
            {
                await CommandService(dto.Service, "stop", cancellationToken);
                return true;
            }
            return false;
        }

        public Task Reboot(CancellationToken cancellationToken = default) => this.RunProcess("reboot", "", cancellationToken);

        public async Task<DateTime?> IsServiceRunning(string service, CancellationToken cancellationToken = default)
        {
            try
            {
                var activeRunnigString = "Active: ";
                var sinceString = " since";
                var isActiveText = "active (running)";
                var output = await CommandService(service, "status");
                var startIndex = output.IndexOf(activeRunnigString);
                var endOfLine = output.Substring(startIndex + activeRunnigString.Length).IndexOf(';');

                var activeStateIndex = output.Substring(startIndex + activeRunnigString.Length).IndexOf(sinceString);

                var activeText = output.Substring(startIndex + activeRunnigString.Length, activeStateIndex);
                var runningSincetext = output.Substring(startIndex + activeRunnigString.Length + activeStateIndex + sinceString.Length + 1 + 3, endOfLine - activeStateIndex - sinceString.Length - 1 - 8);
                var isActive = activeText.Contains(isActiveText);
                if (isActive &&
                    DateTime.TryParse(runningSincetext, out var running))
                {
                    return running;
                }
            }
            catch { }
            return null;
        }

        private Task<string> CommandService(string service, string command, CancellationToken cancellationToken = default)
            => this.RunProcess("service", $"{service} {command}", cancellationToken);

        private async Task<string> RunProcess(string filename, string args, CancellationToken cancellationToken)
        {
            try
            {
                var info = new ProcessStartInfo(filename, args)
                {
                    RedirectStandardOutput = true,
                };
                var proc = Process.Start(info);
                proc.Start();
                await proc.WaitForExitAsync(cancellationToken);
                return await proc.StandardOutput.ReadToEndAsync();
            }
            catch
            {
                return string.Empty;
            }
        }
    }

}
