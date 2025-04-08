using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Services
{
    public class UserIdLogInService : BaseUserIdLoginService<int>
    {
        public UserIdLogInService(IProtecedLocalStorageHandler protecedLocalStorageHandler) : base(protecedLocalStorageHandler)
        {
        }

        public override Task<bool> IsEnabled => Task.FromResult(false);
    }
}
