using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    [ContextType(typeof(UnicornStoreContext))]
    partial class UnicornStoreContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Identity");
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.CartItem", b =>
                    {
                        b.Property<int>("CartItemId")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<DateTime>("PriceCalculated")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<decimal>("PricePerUnit")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<int>("ProductId")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("Quantity")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("Username")
                            .Annotation("OriginalValueIndex", 6);
                        b.Key("CartItemId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Category", b =>
                    {
                        b.Property<int>("CategoryId")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("DisplayName")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int?>("ParentCategoryId")
                            .Annotation("OriginalValueIndex", 2);
                        b.Key("CategoryId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Order", b =>
                    {
                        b.Property<DateTime>("CheckoutBegan")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("OrderId")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<DateTime?>("OrderPlaced")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("State")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<decimal>("Total")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<string>("Username")
                            .Annotation("OriginalValueIndex", 5);
                        b.Key("OrderId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderLine", b =>
                    {
                        b.Property<int>("OrderId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<decimal>("PricePerUnit")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("ProductId")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("Quantity")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("OrderId", "ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderShippingDetails", b =>
                    {
                        b.Property<string>("Addressee")
                            .Required()
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("CityOrTown")
                            .Required()
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Country")
                            .Required()
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("LineOne")
                            .Required()
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("LineTwo")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<int>("OrderId")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("StateOrProvince")
                            .Required()
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("ZipOrPostalCode")
                            .Required()
                            .Annotation("OriginalValueIndex", 7);
                        b.Key("OrderId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                    {
                        b.Property<int>("CategoryId")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<decimal>("CurrentPrice")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Description")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("DisplayName")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("ImageUrl")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<decimal>("MSRP")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<int>("ProductId")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Key("ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.WebsiteAd", b =>
                    {
                        b.Property<string>("Details")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<DateTime?>("End")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("ImageUrl")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<DateTime?>("Start")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("TagLine")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<string>("Url")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<int>("WebsiteAdId")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 6)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Key("WebsiteAdId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.CartItem", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Product", "ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Category", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Category", "ParentCategoryId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderLine", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Order", "OrderId");
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Product", "ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderShippingDetails", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Order", "OrderId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Category", "CategoryId");
                    });
                
                return builder.Model;
            }
        }
    }
}
