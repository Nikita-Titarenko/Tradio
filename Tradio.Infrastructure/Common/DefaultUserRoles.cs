using Microsoft.AspNetCore.Identity;

namespace Tradio.Infrastructure.Common
{
    public class DefaultUserRoles
    {
        public readonly static IdentityUserRole<string>[] UserRoles = new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = DefaultRoles.AdminRole.Id,
                UserId = DefaultUsers.Users[0].Id
            }
        };
    }
}
