using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class HeartRateData
    {
        private readonly byte[] _data;

        public byte BPM => this._data[1];

        public HeartRateData(byte[] data)
        {
            this._data = data;
        }

    }
}
