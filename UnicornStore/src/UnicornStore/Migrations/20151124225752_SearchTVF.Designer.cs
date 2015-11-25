using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.Migrations
{
    [DbContext(typeof(UnicornStoreContext))]
    [Migration("20151124225752_SearchTVF")]
    partial class SearchTVF
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16305")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdated");

                    b.Property<DateTime>("PriceCalculated");

                    b.Property<decimal>("PricePerUnit");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<string>("Username");

                    b.HasKey("CartItemId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("CategoryId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckoutBegan");

                    b.Property<string>("DisplayId");

                    b.Property<DateTime?>("OrderPlaced");

                    b.Property<int>("State");

                    b.Property<decimal>("Total");

                    b.Property<string>("Username");

                    b.HasKey("OrderId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderLine", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("PricePerUnit");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "ProductId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderShippingDetails", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<string>("Addressee")
                        .IsRequired();

                    b.Property<string>("CityOrTown")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("LineOne")
                        .IsRequired();

                    b.Property<string>("LineTwo");

                    b.Property<string>("StateOrProvince")
                        .IsRequired();

                    b.Property<string>("ZipOrPostalCode")
                        .IsRequired();

                    b.HasKey("OrderId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<decimal>("CurrentPrice");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("ImageUrl");

                    b.Property<decimal>("MSRP");

                    b.Property<string>("SKU");

                    b.HasKey("ProductId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Recall", b =>
                {
                    b.Property<int>("RecallId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details");

                    b.Property<string>("ProductSKU")
                        .IsRequired();

                    b.HasKey("RecallId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.WebsiteAd", b =>
                {
                    b.Property<int>("WebsiteAdId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details");

                    b.Property<DateTime?>("End");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime?>("Start");

                    b.Property<string>("TagLine");

                    b.Property<string>("Url");

                    b.HasKey("WebsiteAdId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.CartItem", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Category", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Category")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderLine", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("UnicornStore.Models.UnicornStore.Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderShippingDetails", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Order")
                        .WithOne()
                        .HasForeignKey("UnicornStore.Models.UnicornStore.OrderShippingDetails", "OrderId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Product", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Recall", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Product")
                        .WithMany()
                        .HasForeignKey("ProductSKU")
                        .HasPrincipalKey("SKU");
                });
        }
    }
}
