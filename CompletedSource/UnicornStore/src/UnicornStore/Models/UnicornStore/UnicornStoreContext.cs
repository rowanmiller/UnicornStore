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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ForSqlServer().UseIdentity();

            builder.Entity<Category>()
                .Reference(c => c.ParentCategory)
                .InverseCollection(c => c.Children)
                .ForeignKey(c => c.ParentCategoryId);

            builder.Entity<OrderLine>()
                .Key(ol => new { ol.OrderId, ol.ProductId });

            builder.Entity<OrderShippingDetails>()
                .Key(d => d.OrderId);

            builder.Entity<Order>()
                .Reference(o => o.ShippingDetails)
                .InverseReference()
                .ForeignKey<OrderShippingDetails>(d => d.OrderId);

            builder.Entity<OrderShippingDetails>().ConfigureAddress();
        }
    }
}