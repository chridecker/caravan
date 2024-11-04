namespace chd.CaraVan.Contracts.Settings
{
    public class AesSettings
    {
        public bool IsActive { get; set; }
        public decimal? BatteryLimit { get; set; }
        public decimal? SolarAmpLimit { get; set; }
        public TimeSpan? AesTimeout { get; set; }
    }
}
