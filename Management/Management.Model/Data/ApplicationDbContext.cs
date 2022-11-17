using Management.Model.DBModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid,
        UserClaim, UserRole, UserLogin, Roleclaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        
        }

        public DbSet<DBModel.Action> Actions { get; set; }
        public DbSet<UserAuditLog> UserAuditLogs { get; set; }

    }
}