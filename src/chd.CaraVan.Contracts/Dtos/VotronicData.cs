namespace chd.CaraVan.Contracts.Dtos
{
    public abstract class VotronicData
    {
        public byte[] Data {get;set; }

        public DateTime DateTime { get; set; }
        public decimal Voltage { get; set; }
        public decimal AmpereH { get; set; }
        public decimal Ampere { get; set; }
    }
}
