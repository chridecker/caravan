using chd.UI.Base.Client.Implementations.Authorization;
using chd.UI.Base.Contracts.Dtos.Authentication;

namespace chd.CaraVan.UI.Services
{
    public class chdProfileService : ProfileService<int, int>
    {
        protected override async Task<UserPermissionDto<int>> GetPermissions(UserDto<int, int> dto, CancellationToken cancellationToken = default)
        {
            return new UserPermissionDto<int>();
        }

        protected override async Task<UserDto<int, int>> GetUser(LoginDto<int> dto, CancellationToken cancellationToken = default)
        {
            return new UserDto<int, int>
            {
                FirstName = "Christoph",
                LastName = "Decker",
                Id = 1,
            };
        }
    }
}
