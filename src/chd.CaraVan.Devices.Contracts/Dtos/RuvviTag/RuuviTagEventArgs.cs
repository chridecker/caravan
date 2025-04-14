using chd.CaraVan.Shared.UI.Dtos;

namespace chd.CaraVan.Devices.Contracts.Dtos.RuvviTag
{
    public class RuuviTagEventArgs
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public RuuviTagData Data { get; set; }
    }
}
