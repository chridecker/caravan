using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Dtos
{
    public class BLECharacteristic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanWrite { get; set; }
        public byte[] Value { get; set; }
    }
}
