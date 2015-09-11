using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using UnicornStore.AspNet.Models.UnicornStore;
using UnicornStore.AspNet.ViewModels.Shop;

namespace UnicornStore.AspNet.Controllers
{
    public class ShopController : Controller
    {
        private UnicornStoreContext db;
        CategoryCache categoryCache;

        public ShopController(UnicornStoreContext context, CategoryCache cache)
        {
            db = context;
            categoryCache = cache;
        }

        public IActionResult Index()
        {
            // TODO ToList() is a workaround for:
            //        - https://github.com/aspnet/EntityFramework/issues/1851
            //        - https://github.com/aspnet/EntityFramework/issues/1852

            var products = db.Products
                .ToList()
                .OrderByDescending(p => p.MSRP - p.CurrentPrice)
                .Take(4);

            return View(new IndexViewModel
            {
                FeaturedProducts = products,
                TopLevelCategories = categoryCache.TopLevel()
            });
        }

        public IActionResult Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var category = categoryCache.FromKey(id.Value);

            if (category == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var ids = categoryCache.GetThisAndChildIds(id.Value);
            return View(new CategoryViewModel
            {
                Category = category,
                Products = db.Products.Where(p => ids.Contains(p.CategoryId)),
                ParentHierarchy = categoryCache.GetHierarchy(category.CategoryId),
                Children = category.Children
            });
        }

        public IActionResult Product(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var product = db.Products
                .Include(p => p.Category)
                .SingleOrDefault(m => m.ProductId == id);

            if (product == null)
            {
                return new HttpStatusCodeResult(404);
            }

            return View(new ProductViewModel
            {
                Product = product,
                CategoryHierarchy = categoryCache.GetHierarchy(product.CategoryId),
                TopLevelCategories = categoryCache.TopLevel()
            });
        }

        public IActionResult Search(string term)
        {
            var products = db.Products
                .FromSql("SELECT * FROM [dbo].[SearchProducts] (@p0)", term)
                .OrderByDescending(p => p.Savings)
                .ToList();

            return View(new SearchViewModel
            {
                SearchTerm = term,
                Products = products
            });
        }
    }
}
