using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    [ContextType(typeof(UnicornStoreContext))]
    partial class Orders
    {
        public override string Id
        {
            get { return "20150320215931_Orders"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12528"; }
        }
        
        public override IModel Target
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.CartItem", b =>
                    {
                        b.Property<int>("CartItemId")
                            .GenerateValueOnAdd();
                        b.Property<DateTime>("PriceCalculated");
                        b.Property<decimal>("PricePerUnit");
                        b.Property<int>("ProductId");
                        b.Property<int>("Quantity");
                        b.Property<string>("Username");
                        b.Key("CartItemId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Category", b =>
                    {
                        b.Property<int>("CategoryId")
                            .GenerateValueOnAdd();
                        b.Property<string>("DisplayName");
                        b.Property<int?>("ParentCategoryId");
                        b.Key("CategoryId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Order", b =>
                    {
                        b.Property<DateTime>("CheckoutBegan");
                        b.Property<int>("OrderId")
                            .GenerateValueOnAdd();
                        b.Property<DateTime?>("OrderPlaced");
                        b.Property<string>("ShippingAddressLineOne");
                        b.Property<string>("ShippingAddressLineTwo");
                        b.Property<string>("ShippingAddressee");
                        b.Property<string>("ShippingCityOrTown");
                        b.Property<string>("ShippingCountry");
                        b.Property<string>("ShippingStateOrProvince");
                        b.Property<string>("ShippingZipOrPostalCode");
                        b.Property<int>("State");
                        b.Property<decimal>("Total");
                        b.Property<string>("Username");
                        b.Key("OrderId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.OrderLine", b =>
                    {
                        b.Property<int>("OrderId");
                        b.Property<decimal>("PricePerUnit");
                        b.Property<int>("ProductId");
                        b.Property<int>("Quantity");
                        b.Key("OrderId", "ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                    {
                        b.Property<int>("CategoryId");
                        b.Property<decimal>("CurrentPrice");
                        b.Property<string>("Description");
                        b.Property<string>("DisplayName");
                        b.Property<string>("ImageUrl");
                        b.Property<decimal>("MSRP");
                        b.Property<int>("ProductId")
                            .GenerateValueOnAdd();
                        b.Key("ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.WebsiteAd", b =>
                    {
                        b.Property<string>("Details");
                        b.Property<DateTime?>("End");
                        b.Property<string>("ImageUrl");
                        b.Property<DateTime?>("Start");
                        b.Property<string>("TagLine");
                        b.Property<string>("Url");
                        b.Property<int>("WebsiteAdId")
                            .GenerateValueOnAdd();
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
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Category", "CategoryId");
                    });
                
                return builder.Model;
            }
        }
    }
}
