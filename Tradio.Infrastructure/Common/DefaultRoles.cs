using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
