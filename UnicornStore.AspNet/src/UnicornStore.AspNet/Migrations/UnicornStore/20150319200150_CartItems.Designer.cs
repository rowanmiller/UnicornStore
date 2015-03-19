using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    [ContextType(typeof(UnicornStoreContext))]
    partial class CartItems
    {
        public override string Id
        {
            get { return "20150319200150_CartItems"; }
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
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.CartItem", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Product", "ProductId");
                    });
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Category", b =>
                    {
                        b.ForeignKey("UnicornStore.AspNet.Models.UnicornStore.Category", "ParentCategoryId");
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
