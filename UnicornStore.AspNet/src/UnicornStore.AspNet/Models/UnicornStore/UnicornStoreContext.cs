using System;
using Microsoft.Data.Entity;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public class UnicornStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WebsiteAd> WebsiteAds { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Children)
                .ForeignKey(c => c.ParentCategoryId);

            modelBuilder.Entity<OrderLine>()
                .Key(ol => new { ol.OrderId, ol.ProductId });
        }
    }
}