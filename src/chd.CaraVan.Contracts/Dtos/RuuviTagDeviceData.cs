using chd.CaraVan.Contracts.Dtos.Base;
using chd.CaraVan.Contracts.Enums;

namespace chd.CaraVan.Contracts.Dtos
{
    public class RuuviTagDeviceData : DataBase
    {
        public int DeviceId { get; set; }
        public RuuviTagDeviceData(DateTime time, EDataType type, decimal val) : base(time, type, val)
        {
        }
    }
}
