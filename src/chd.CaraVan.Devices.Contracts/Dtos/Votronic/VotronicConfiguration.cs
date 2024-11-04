namespace chd.CaraVan.Devices.Contracts.Dtos.Votronic
{
    public class VotronicConfiguration
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string DeviceAddress { get; set; }
        public int BatteryAH { get; set; }
    }
}
