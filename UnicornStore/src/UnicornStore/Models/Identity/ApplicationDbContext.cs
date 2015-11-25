using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace UnicornStore.Models.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAddress>().ToTable("AspNetUserAddresses");
            builder.Entity<UserAddress>().ConfigureAddress();
        }
    }
}
