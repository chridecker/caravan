﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Dtos
{
    public class PinChangedEventArgs
    {
        public bool Rising { get; set; }
        public int Pin { get; set; }
    }
}
