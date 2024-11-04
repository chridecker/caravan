using chd.CaraVan.Contracts.Enums;
using System.Device.Gpio;

namespace chd.CaraVan.Devices.Contracts.Dtos.Pi
{
    public class Gpio
    {
        public int Pin { get; set; }
        public PinMode Mode { get; set; }
        public PinValue Default { get; set; }
        public string Name { get; set; }
        public GpioType Type { get; set; }

    }
}
