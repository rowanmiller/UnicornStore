using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    [ContextType(typeof(UnicornStoreContext))]
    partial class Products
    {
        public override string Id
        {
            get { return "20150312195159_Products"; }
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
                
                builder.Entity("UnicornStore.AspNet.Models.UnicornStore.Product", b =>
                    {
                        b.Property<decimal>("CurrentPrice");
                        b.Property<string>("Description");
                        b.Property<string>("DisplayName");
                        b.Property<decimal>("MSRP");
                        b.Property<int>("ProductId")
                            .GenerateValueOnAdd();
                        b.Key("ProductId");
                    });
                
                return builder.Model;
            }
        }
    }
}
