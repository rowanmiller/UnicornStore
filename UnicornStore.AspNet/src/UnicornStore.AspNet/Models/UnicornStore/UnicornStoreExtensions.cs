using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public static class UnicornStoreExtensions
    {
        public static void EnsureSampleData(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<UnicornStoreContext>();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { DisplayName = "Unicorn Coffee Mug (Blue)", MSRP = 12.95M, CurrentPrice = 12.95M, ImageUrl= "/images/products/CoffeeMug_Blue.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                    new Product { DisplayName = "Unicorn Coffee Mug (Green)", MSRP = 12.95M, CurrentPrice = 12.95M, ImageUrl = "/images/products/CoffeeMug_Green.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                    new Product { DisplayName = "Unicorn Coffee Mug (Pink)", MSRP = 12.95M, CurrentPrice = 12.95M, ImageUrl = "/images/products/CoffeeMug_Pink.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                    new Product { DisplayName = "Unicorn Coffee Mug (White)", MSRP = 12.95M, CurrentPrice = 12.95M, ImageUrl = "/images/products/CoffeeMug_White.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                    new Product { DisplayName = "Mens Unicorn Tee (Blue)", MSRP = 19.95M, CurrentPrice = 19.95M, ImageUrl = "/images/products/MensTee_Blue.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." },
                    new Product { DisplayName = "Mens Unicorn Tee (Grey)", MSRP = 19.95M, CurrentPrice = 19.95M, ImageUrl = "/images/products/MensTee_Grey.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." },
                    new Product { DisplayName = "Mens Unicorn Tee (Red/Black Stripe)", MSRP = 19.95M, CurrentPrice = 19.95M, ImageUrl = "/images/products/MensTee_RedBlackStripe.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." });

                context.SaveChanges();
            }
        }
    }
}