using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UnicornStore.Data;

namespace UnicornStore.Data.Migrations.UnicornStore
{
    [DbContext(typeof(UnicornStoreContext))]
    partial class UnicornStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
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

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckoutBegan");

                    b.Property<DateTime?>("OrderPlaced");

                    b.Property<int>("State");

                    b.Property<decimal>("Total");

                    b.Property<string>("Username");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderLine", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("PricePerUnit");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLine");
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

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("OrderShippingDetails");
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

                    b.Property<string>("SKU")
                        .IsRequired();

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Recall", b =>
                {
                    b.Property<int>("RecallId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details");

                    b.Property<string>("ProductSKU")
                        .IsRequired();

                    b.HasKey("RecallId");

                    b.HasIndex("ProductSKU");

                    b.ToTable("Recalls");
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

                    b.ToTable("WebsiteAds");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.CartItem", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Category", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Category", "ParentCategory")
                        .WithMany("Children")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderLine", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Order", "Order")
                        .WithMany("Lines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UnicornStore.Models.UnicornStore.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.OrderShippingDetails", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Order")
                        .WithOne("ShippingDetails")
                        .HasForeignKey("UnicornStore.Models.UnicornStore.OrderShippingDetails", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Product", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UnicornStore.Models.UnicornStore.Recall", b =>
                {
                    b.HasOne("UnicornStore.Models.UnicornStore.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductSKU")
                        .HasPrincipalKey("SKU")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
