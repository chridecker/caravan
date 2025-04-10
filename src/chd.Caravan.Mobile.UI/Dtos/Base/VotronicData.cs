﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos.Base
{
   public abstract class VotronicData
    {
        protected readonly byte[] _data;
        public decimal Voltage => this.GetData(0,2,100m);

        public byte[] Data => this._data;

        protected VotronicData(byte[] data)
        {
            this._data = data;
        }
       
        
        protected decimal GetData(int start, int bytes, decimal divisor)
        {
            var hex = Convert.ToHexString(this._data.Skip(start).Take(bytes).Reverse().ToArray());
            var val = Convert.ToInt16(hex, 16);
            return Math.Round(val / divisor, 2);
        }

    }
}
