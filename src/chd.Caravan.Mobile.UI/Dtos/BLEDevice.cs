using chd.Caravan.Mobile.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class BLEDevice
    {
        public Guid Id { get; set; }
        public string UID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public EDeviceType Type { get; set; }

        public bool IsStarted { get; set; }

        public byte[] BatteryLevel { get; set; }
    }
}
