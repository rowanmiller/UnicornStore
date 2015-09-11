using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using UnicornPacker.Models;

namespace UnicornPacker.Migrations
{
    [DbContext(typeof(OrdersContext))]
    partial class OrdersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540");

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

                    b.Key("OrderId");
                });

            modelBuilder.Entity("UnicornPacker.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<bool>("IsPacked");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.Key("OrderId", "ProductId");
                });

            modelBuilder.Entity("UnicornPacker.Models.OrderLine", b =>
                {
                    b.Reference("UnicornPacker.Models.Order")
                        .InverseCollection()
                        .ForeignKey("OrderId");
                });
        }
    }
}
