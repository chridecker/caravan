using chd.CaraVan.Contracts.Enums;

namespace chd.CaraVan.Contracts.Dtos
{
    public class GpioPinDto
    {
        public int Pin { get; set; }
        public string Name { get; set; }
        public GpioType Type { get; set; }
    }
}
