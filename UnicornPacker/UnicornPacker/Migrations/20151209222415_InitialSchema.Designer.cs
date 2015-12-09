using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using UnicornPacker.Models;

namespace UnicornPacker.Migrations
{
    [DbContext(typeof(OrdersContext))]
    [Migration("20151209222415_InitialSchema")]
    partial class InitialSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("UnicornPacker.Models.Order", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<string>("Addressee");

                    b.Property<string>("CityOrTown");

                    b.Property<string>("Country");

                    b.Property<bool>("IsShipped");

                    b.Property<bool>("IsShippingSynced");

                    b.Property<string>("LineOne");

                    b.Property<string>("LineTwo");

                    b.Property<string>("StateOrProvince");

                    b.Property<string>("ZipOrPostalCode");

                    b.HasKey("OrderId");
                });

            modelBuilder.Entity("UnicornPacker.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<bool>("IsPacked");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "ProductId");
                });

            modelBuilder.Entity("UnicornPacker.Models.OrderLine", b =>
                {
                    b.HasOne("UnicornPacker.Models.Order")
                        .WithMany()
                        .HasForeignKey("OrderId");
                });
        }
    }
}
