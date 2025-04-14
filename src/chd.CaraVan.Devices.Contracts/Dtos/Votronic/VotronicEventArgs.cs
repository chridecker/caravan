using chd.CaraVan.Contracts.Dtos;

namespace chd.CaraVan.Devices.Contracts.Dtos.Votronic
{
    public class VotronicEventArgs
    {
        public DateTime DateTime { get; set; }
        public VotronicBatteryData? BatteryData { get; set; }
        public VotronicSolarData? SolarData { get; set; }
    }
}
