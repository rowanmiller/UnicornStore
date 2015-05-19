using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    [ContextType(typeof(UnicornStoreContext))]
    partial class CartItems
    {
        public override string Id
        {
            get { return "20150421225541_CartItems"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12914"; }
        }
        
        public override IModel Target
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
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<decimal>("PricePerUnit")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<int>("ProductId")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<int>("Quantity")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<string>("Username")
                            .Annotation("OriginalValueIndex", 5);
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
