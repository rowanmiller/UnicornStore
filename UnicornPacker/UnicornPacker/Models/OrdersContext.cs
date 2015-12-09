using Microsoft.Data.Entity;
using System;
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
            optionsBuilder.UseSqlite($"Data source={GetLocalDatabaseFile()}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Order>()
                .Ignore(o => o.DisplayId);

            modelBuilder.Entity<Order>()
                .Ignore(o => o.LineTwoVisibility);

            modelBuilder.Entity<OrderLine>()
                .HasKey(l => new { l.OrderId, l.ProductId });

            modelBuilder.Entity<OrderLine>()
                .Ignore(o => o.ProductDisplayId);
        }

        private static string GetLocalDatabaseFile()
        {
            string localDirectory = string.Empty;
            try
            {
                localDirectory = ApplicationData.Current.LocalFolder.Path;
            }
            catch (InvalidOperationException)
            { }

            return Path.Combine(localDirectory, "Orders.db");
        }

    }
}
