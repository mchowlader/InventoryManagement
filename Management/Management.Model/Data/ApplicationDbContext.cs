using Management.Common.Configuration;
using Management.Model.DBModel;
using Management.Model.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid,
        UserClaim, UserRole, UserLogin, Roleclaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .HasData(RoleSeeding.Roles);

            base.OnModelCreating(builder);
        }
        public DbSet<Management.Model.DBModel.Action> Actions { get; set; }
        public DbSet<UserAuditLog> UserAuditLogs { get; set; }
    }
}