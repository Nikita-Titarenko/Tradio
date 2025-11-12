using Microsoft.AspNetCore.Identity;

namespace Tradio.Application.Common
{
    public class DefaultRoles
    {
        public static readonly IdentityRole UserRole = new IdentityRole
        {
            Id = "1",
            Name = "User",
            NormalizedName = "USER"
        };

        public static readonly IdentityRole AdminRole = new IdentityRole
        {
            Id = "2",
            Name = "Admin",
            NormalizedName = "ADMIN"
        };
    }
}
