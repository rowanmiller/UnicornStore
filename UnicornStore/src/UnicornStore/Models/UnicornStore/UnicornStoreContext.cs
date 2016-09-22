using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnicornStore.Models.UnicornStore
{
    public class UnicornStoreContext : DbContext
    {
        public UnicornStoreContext(DbContextOptions<UnicornStoreContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WebsiteAd> WebsiteAds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Recall> Recalls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentCategoryId);

            builder.Entity<OrderLine>()
                .HasKey(ol => new { ol.OrderId, ol.ProductId });

            builder.Entity<OrderShippingDetails>()
                .HasKey(d => d.OrderId);

            builder.Entity<Order>()
                .HasOne(o => o.ShippingDetails)
                .WithOne()
                .HasForeignKey<OrderShippingDetails>(d => d.OrderId);

            builder.Entity<Order>()
                .Ignore(o => o.DisplayId);

            builder.Entity<OrderShippingDetails>().ConfigureAddress();

            builder.Entity<Product>()
                .HasAlternateKey(p => p.SKU);

            builder.Entity<Product>() 
                .Ignore(p => p.Savings);

            builder.Entity<CartItem>().Property<DateTime>("LastUpdated");

            builder.Entity<Recall>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductSKU)
                .HasPrincipalKey(p => p.SKU)
                .IsRequired();
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            var entries = this.ChangeTracker.Entries<CartItem>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}