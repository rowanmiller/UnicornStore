using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using UnicornPacker.Models;

namespace UnicornPackerMigrations
{
    [ContextType(typeof(OrdersContext))]
    partial class InitialSchema
    {
        public override string Id
        {
            get { return "20150727220657_InitialSchema"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("UnicornPacker.Models.Order", b =>
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

            builder.Entity("UnicornPacker.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<bool>("IsPacked");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.Key("OrderId", "ProductId");
                });

            builder.Entity("UnicornPacker.Models.OrderLine", b =>
                {
                    b.Reference("UnicornPacker.Models.Order")
                        .InverseCollection()
                        .ForeignKey("OrderId");
                });
        }
    }
}
