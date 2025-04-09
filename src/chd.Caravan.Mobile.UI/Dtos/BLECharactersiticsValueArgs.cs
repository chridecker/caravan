using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class BLECharactersiticsValueArgs : EventArgs
    {
        public Guid CharacteristicId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid DeviceId { get; set; }
        public string UID { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public byte[] Data { get; set; }
    }
}
