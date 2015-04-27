using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using UnicornStore.AspNet.Models.Identity;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public static class UnicornStoreExtensions
    {
        public static void EnsureMigrationsApplied(this IApplicationBuilder app)
        {
            DbContext context = app.ApplicationServices.GetService<UnicornStoreContext>();
            context.Database.AsRelational().ApplyMigrations();

            context = app.ApplicationServices.GetService<ApplicationDbContext>();
            context.Database.AsRelational().ApplyMigrations();
        }

        public static void EnsureSampleData(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<UnicornStoreContext>();
            if (context.AllMigrationsApplied())
            {
                if (!context.Products.Any())
                {
                    var clothing = context.Categories.Add(new Category { DisplayName = "Clothing" }).Entity;
                    var mensClothing = context.Categories.Add(new Category { DisplayName = "Mens Clothing", ParentCategory = clothing }).Entity;
                    var mensShirts = context.Categories.Add(new Category { DisplayName = "Mens Shirts", ParentCategory = mensClothing }).Entity;

                    var homeAndGarden = context.Categories.Add(new Category { DisplayName = "Home & Garden" }).Entity;
                    var kitchenAndDining = context.Categories.Add(new Category { DisplayName = "Kitchen & Dining", ParentCategory = homeAndGarden }).Entity;

                    context.Products.AddRange(
                        new Product { DisplayName = "Unicorn Coffee Mug (Blue)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_Blue.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                        new Product { DisplayName = "Unicorn Coffee Mug (Green)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_Green.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                        new Product { DisplayName = "Unicorn Coffee Mug (Pink)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_Pink.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                        new Product { DisplayName = "Unicorn Coffee Mug (White)", MSRP = 12.95M, CurrentPrice = 12.95M, Category = kitchenAndDining, ImageUrl = "/images/products/CoffeeMug_White.png", Description = "Coffee and unicorns... what else could you need! Our flagship unicorn printed on a high quality coffee mug." },
                        new Product { DisplayName = "Mens Unicorn Tee (Blue)", MSRP = 19.95M, CurrentPrice = 19.95M, Category = mensShirts, ImageUrl = "/images/products/MensTee_Blue.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." },
                        new Product { DisplayName = "Mens Unicorn Tee (Grey)", MSRP = 19.95M, CurrentPrice = 19.95M, Category = mensShirts, ImageUrl = "/images/products/MensTee_Grey.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." },
                        new Product { DisplayName = "Mens Unicorn Tee (Red/Black Stripe)", MSRP = 19.95M, CurrentPrice = 19.95M, Category = mensShirts, ImageUrl = "/images/products/MensTee_RedBlackStripe.png", Description = "Share your love of unicorns with the world. Quality cotton t-shirt with a long lasting print." });

                    context.SaveChanges();
                }

                if (!context.WebsiteAds.Any())
                {
                    var kitchenAndDining = context.Categories.Single(c => c.DisplayName == "Kitchen & Dining");
                    var clothing = context.Categories.Single(c => c.DisplayName == "Clothing");

                    context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/DemoApplication.png", TagLine = "Unicorn Store is a demo application for Entity Framework 7", Details = "See github.com/rowanmiller/UnicornStore for details", Url = "http://github.com/rowanmiller/UnicornStore" });
                    context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/GotUnicorn.png", TagLine = "We've got you covered... literally", Details = "Checkout our range of clothing sporting our flagship unicorn", Url = "/Shop/Category/" + clothing.CategoryId.ToString() });
                    context.WebsiteAds.Add(new WebsiteAd { ImageUrl = "/images/banners/JavaHeart.png", TagLine = "Drink and eat in style with our range of unicorn wares", Url = "/Shop/Category/" + kitchenAndDining.CategoryId.ToString() });
                    context.SaveChanges();
                }
            }
        }
    }
}