using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using chd.CaraVan.Contracts.Settings;
using chd.CaraVan.Devices.Contracts.Dtos;
using chd.CaraVan.Devices.Contracts.Dtos.Pi;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Implementations
{
    public class PiManager : IPiManager
    {
        private readonly GpioController _controller;
        private readonly IOptionsMonitor<PiSettings> _optionsMonitor;

        public event EventHandler<PinChangedEventArgs> PinChanged;

        public PiManager(GpioController gpioController, IOptionsMonitor<PiSettings> optionsMonitor)
        {
            this._controller = new();
            this._optionsMonitor = optionsMonitor;
        }

        public Task Start(CancellationToken cancellationToken) => Task.Run(() =>
        {
            foreach (var g in this._optionsMonitor.CurrentValue.KeyPins)
            {
                if (this._controller.IsPinOpen(g))
                {

                    this._controller.ClosePin(g);
                }
                this._controller.OpenPin(g, PinMode.Input);
                this._controller.RegisterCallbackForPinValueChangedEvent(g, PinEventTypes.Rising, OnPinChanged);
                this._controller.RegisterCallbackForPinValueChangedEvent(g, PinEventTypes.Falling, OnPinChanged);
            }
        }, cancellationToken);



        public Task Stop(CancellationToken cancellationToken = default) => Task.Run(() =>
        {
            foreach (var g in this._optionsMonitor.CurrentValue.KeyPins)
            {
                if (this._controller.IsPinOpen(g))
                {
                    this._controller.ClosePin(g);
                }
            }
            this._controller?.Dispose();
        }, cancellationToken);

        public Task Write(PinWriteDto dto, CancellationToken cancellationToken = default) => Task.Run(() => this.WriteToPin(dto.Pin, dto.Value), cancellationToken);
        public Task<bool> Read(int pin, CancellationToken cancellationToken = default) => Task.FromResult(this._controller?.Read(pin) == PinValue.High);
        private void WriteToPin(int pin, bool val) => this._controller?.Write(pin, val ? PinValue.High : PinValue.Low);

        private void OnPinChanged(object sender, PinValueChangedEventArgs e)
        {
            this.PinChanged?.Invoke(this, new()
            {
                Pin = e.PinNumber,
                Rising = e.ChangeType == PinEventTypes.Rising
            });
        }
    }
}
