namespace chd.CaraVan.Contracts.Interfaces
{
    public interface IDataHub
    {
        Task AesStateSwitched(bool state);
        Task RuuviTagData();
        Task VotronicData();
    }
}
