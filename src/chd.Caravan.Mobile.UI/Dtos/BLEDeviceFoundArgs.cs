using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class BLEDeviceFoundArgs : EventArgs
    {
        public BLEDevice Device { get; set; }

        public BLEDeviceFoundArgs(BLEDevice device)
        {
            this.Device = device;
        }
    }
}
