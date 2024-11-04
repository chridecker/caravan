using chd.CaraVan.Contracts.Interfaces;
using chd.Hub.Base.Client;

namespace chd.CaraVan.UI.Interfaces
{
    public interface IDataHubClient : IBaseHubClient<IDataHub>
    {
        event EventHandler VotronicDataReceived;
        event EventHandler RuuviTagDeviceDataReceived;
        event EventHandler<bool> AesStateSwitched;
    }
}
