using Management.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.Seeds
{
    public static class RoleSeeding
    {
        public static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role(){Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp=Guid.NewGuid().ToString()},
                    new Role(){Id = Guid.NewGuid(), Name = "SuperAdmin", NormalizedName = "SUPERADMIN", ConcurrencyStamp=Guid.NewGuid().ToString()},
                    new Role(){Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER", ConcurrencyStamp=Guid.NewGuid().ToString()},
                };
            }
        }
    }
}
