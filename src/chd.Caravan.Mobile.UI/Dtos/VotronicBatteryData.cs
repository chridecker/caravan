using chd.Caravan.Mobile.UI.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class VotronicBatteryData : VotronicData
    {
        private readonly int _batteryAmpH;

        public VotronicBatteryData(byte[] data, int batteryAmpH) : base(data)
        {
            this._batteryAmpH = batteryAmpH;
        }

        public decimal Ampere => this.GetData(10, 2, 1000m);
        public decimal Percent => this.GetData(8, 1, 1m);
        public decimal LeftAH => this._batteryAmpH * this.Percent / 100;
        public decimal AmpereH => this._batteryAmpH;
    }
}
