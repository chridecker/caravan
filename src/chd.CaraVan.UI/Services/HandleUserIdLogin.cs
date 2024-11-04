using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;

namespace chd.CaraVan.UI.Services
{
    public class HandleUserIdLogin : BaseUserIdLoginService<int>
    {
        public HandleUserIdLogin(IProtecedLocalStorageHandler protecedLocalStorageHandler) : base(protecedLocalStorageHandler)
        {
        }

        public override Task<bool> IsEnabled => Task.FromResult(true);
    }
}
