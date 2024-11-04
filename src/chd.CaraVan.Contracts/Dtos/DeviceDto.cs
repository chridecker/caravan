namespace chd.CaraVan.Contracts.Dtos
{
    public abstract class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UID { get; set; }
    }
}
