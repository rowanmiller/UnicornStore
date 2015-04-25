using Microsoft.Data.Entity;
using System.IO;
using Windows.Storage;

namespace UnicornPacker.Models
{
    public class OrdersContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var file = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Orders.db");
            optionsBuilder.UseSqlite("Data source=" + file);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLine>()
                .Key(l => new { l.OrderId, l.ProductId });
        }
    }
}
