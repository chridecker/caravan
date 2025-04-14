namespace chd.CaraVan.Contracts.Dtos
{
    public class VotronicBatteryData : VotronicData
    {
        private readonly int _batteryAmpH;

        public VotronicBatteryData(byte[] data, int batteryAmpH) : base(data)
        {
            this._batteryAmpH = batteryAmpH;
        }

        public decimal AmpereH => this._batteryAmpH;
        public decimal Ampere => this.GetData(10, 2, 1000m);
        public decimal Percent => this.GetData(8, 1, 1m);
        public decimal LeftAH => this._batteryAmpH * this.Percent / 100;
    }
}
