using chd.CaraVan.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Dtos
{
    public class GpioPinDto
    {
        public int Pin { get; set; }
        public string Name { get; set; }
        public GpioType Type { get; set; }
    }
}
