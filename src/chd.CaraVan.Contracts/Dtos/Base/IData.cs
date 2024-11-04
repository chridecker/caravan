using chd.CaraVan.Contracts.Enums;

namespace chd.CaraVan.Contracts.Dtos.Base
{
    public interface IData
    {
        DateTime RecordDateTime { get; }
        EDataType Type { get; }
        decimal Value { get; }
    }
}
