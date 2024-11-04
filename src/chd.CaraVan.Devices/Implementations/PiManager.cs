using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.Devices.Contracts.Dtos.Pi;
using Microsoft.Extensions.Options;
using System.Device.Gpio;

namespace chd.CaraVan.Devices.Implementations
{
    public class PiManager : IPiManager
    {
        private readonly IOptionsMonitor<PiSettings> _optionsMonitor;

        private GpioController _controller;

        public PiManager(IOptionsMonitor<PiSettings> optionsMonitor, GpioController gpioController)
        {
            this._optionsMonitor = optionsMonitor;
            this._controller = gpioController;
        }

        public Task<IEnumerable<GpioPinDto>> GetGpioPins(CancellationToken cancellationToken = default) => Task.Run(() =>
        {
            return this._optionsMonitor.CurrentValue.Gpios.Select(s => new GpioPinDto
            {
                Pin = s.Pin,
                Name = s.Name,
                Type = s.Type
            });
        }, cancellationToken);

        public Task Start(CancellationToken cancellationToken) => Task.Run(() =>
        {
            foreach (var g in this._optionsMonitor.CurrentValue.Gpios)
            {
                if (this._controller.IsPinOpen(g.Pin))
                {
                    this._controller.ClosePin(g.Pin);
                }
                this._controller.OpenPin(g.Pin, g.Mode, g.Default);
            }
        }, cancellationToken);

        public Task Stop(CancellationToken cancellationToken = default) => Task.Run(() =>
        {
            foreach (var g in this._optionsMonitor.CurrentValue.Gpios)
            {
                if (this._controller.IsPinOpen(g.Pin))
                {
                    this._controller.ClosePin(g.Pin);
                }
            }
            this._controller?.Dispose();
        }, cancellationToken);

        public Task Write(PinWriteDto dto, CancellationToken cancellationToken = default) => Task.Run(() => this.WriteToPin(dto.Pin, dto.Value), cancellationToken);
        public Task<bool> Read(int pin, CancellationToken cancellationToken = default) => Task.FromResult(this._controller?.Read(pin) == PinValue.High);
        private void WriteToPin(int pin, bool val) => this._controller?.Write(pin, val ? PinValue.High : PinValue.Low);
    }

}
