using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace UnicornStore.AspNet.Models.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<UserAddress> Addresses { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PreApproval> PreApprovals { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PreApproval>().ForRelational().Table("AspNetPreApprovals");
            builder.Entity<PreApproval>().Key(p => new { p.UserEmail, p.Role });

            builder.Entity<UserAddress>().ForRelational().Table("AspNetUserAddresses");
            builder.Entity<UserAddress>().ConfigureAddress();
        }
    }
}