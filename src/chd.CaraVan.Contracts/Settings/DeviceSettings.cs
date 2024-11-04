using chd.CaraVan.Contracts.Dtos;

namespace chd.CaraVan.Contracts.Settings
{
    public class DeviceSettings
    {
        public IEnumerable<RuuviDeviceDto> RuuviTags { get; set; }
        public VotronicDto Votronic{ get; set; }
    }
}
