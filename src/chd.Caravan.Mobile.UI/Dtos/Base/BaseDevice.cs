using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos.Base
{
    public abstract class BaseDevice
    {
        public Guid Id { get; set; }
        public string DeviceAddress { get; set; }
        public string Name { get; set; }
    }
}
