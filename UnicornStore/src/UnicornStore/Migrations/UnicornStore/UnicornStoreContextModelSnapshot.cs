using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using UnicornStore.AspNet.Models.UnicornStore;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace UnicornStore.Migrations.UnicornStore
{
    [DbContext(typeof(UnicornStoreContext))]
    partial class UnicornStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdated");

                    b.Property<DateTime>("PriceCalculated");

                    b.Property<decimal>("PricePerUnit");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<string>("Username");

                    b.Key("CartItemId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<int?>("ParentCategoryId");

                    b.Key("CategoryId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckoutBegan");

                    b.Property<DateTime?>("OrderPlaced");

                    b.Property<int>("State");

                    b.Property<decimal>("Total");

                    b.Property<string>("Username");

                    b.Key("OrderId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderLine", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("PricePerUnit");

                    b.Property<int>("Quantity");

                    b.Key("OrderId", "ProductId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderShippingDetails", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<string>("Addressee")
                        .Required();

                    b.Property<string>("CityOrTown")
                        .Required();

                    b.Property<string>("Country")
                        .Required();

                    b.Property<string>("LineOne")
                        .Required();

                    b.Property<string>("LineTwo");

                    b.Property<string>("StateOrProvince")
                        .Required();

                    b.Property<string>("ZipOrPostalCode")
                        .Required();

                    b.Key("OrderId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<decimal>("CurrentPrice");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("ImageUrl");

                    b.Property<decimal>("MSRP");

                    b.Key("ProductId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.WebsiteAd", b =>
                {
                    b.Property<int>("WebsiteAdId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details");

                    b.Property<DateTime?>("End");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime?>("Start");

                    b.Property<string>("TagLine");

                    b.Property<string>("Url");

                    b.Key("WebsiteAdId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.CartItem", b =>
                {
                    b.Reference("UnicornStore.AspNet.Models.UnicornStore.Product")
                        .InverseCollection()
                        .ForeignKey("ProductId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.Category", b =>
                {
                    b.Reference("UnicornStore.AspNet.Models.UnicornStore.Category")
                        .InverseCollection()
                        .ForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderLine", b =>
                {
                    b.Reference("UnicornStore.AspNet.Models.UnicornStore.Order")
                        .InverseCollection()
                        .ForeignKey("OrderId");

                    b.Reference("UnicornStore.AspNet.Models.UnicornStore.Product")
                        .InverseCollection()
                        .ForeignKey("ProductId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderShippingDetails", b =>
                {
                    b.Reference("UnicornStore.AspNet.Models.UnicornStore.Order")
                        .InverseReference()
                        .ForeignKey("UnicornStore.AspNet.Models.UnicornStore.OrderShippingDetails", "OrderId");
                });

            modelBuilder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                {
                    b.Reference("UnicornStore.AspNet.Models.UnicornStore.Category")
                        .InverseCollection()
                        .ForeignKey("CategoryId");
                });
        }
    }
}
