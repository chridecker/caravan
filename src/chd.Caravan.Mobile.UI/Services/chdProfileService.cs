using chd.Caravan.Mobile.UI.Dtos;
using chd.UI.Base.Client.Implementations.Authorization;
using chd.UI.Base.Contracts.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Services
{
    public class chdProfileService : ProfileService<int, int>
    {
        protected override async Task<UserPermissionDto<int>> GetPermissions(UserDto<int, int> dto, CancellationToken cancellationToken = default)
        {
            return new UserPermissionDto<int>();
        }

        protected override async Task<UserDto<int, int>> GetUser(LoginDto<int> dto, CancellationToken cancellationToken = default)
        {
            return new chdUser
            {
                FirstName = "Christoph",
                LastName = "Decker",
                Id = 1,
            };
        }
    }
}
