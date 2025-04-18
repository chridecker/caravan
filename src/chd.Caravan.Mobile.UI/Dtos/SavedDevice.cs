﻿using chd.Caravan.Mobile.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class SavedDevice
    {
        public Guid Id { get; set; }
        public string UID { get; set; }
        public string Name { get; set; }
        public EDeviceType Type { get; set; }
    }
}
