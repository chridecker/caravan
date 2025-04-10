﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Constants
{
    public class BLEContants
    {
        public static Guid NORDIC_UART_SVC = Guid.Parse("6E400001-B5A3-F393-E0A9-E50E24DCCA9E");
        public static Guid TX_CHARACTERISTIC = Guid.Parse("6E400003-B5A3-F393-E0A9-E50E24DCCA9E");

        public static Guid VOTRONIC_SVC = Guid.Parse("d0cb6aa7-8548-46d0-99f8-2d02611e5270");

        public static Guid BATTERY_CHARACTERISTIC = Guid.Parse("9a082a4e-5bcc-4b1d-9958-a97cfccfa5ec");
        public static Guid SOLAR_CHARACTERISTIC = Guid.Parse("971ccec2-521d-42fd-b570-cf46fe5ceb65");
    }
}
