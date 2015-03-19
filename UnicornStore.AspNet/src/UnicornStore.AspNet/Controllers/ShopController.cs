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
        private UnicornStoreContext db = new UnicornStoreContext();

        public ShopController(UnicornStoreContext context)
        {
            db = context;
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
                TopLevelCategories = GetTopLevelCategories(db)
            });
        }

        public IActionResult Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var category = db.Categories
                .AsNoTracking()
                .SingleOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var children = db.Categories
                .Where(c => c.ParentCategoryId == category.CategoryId)
                .Select(c => new CategoryInfo
                {
                    CategoryId = c.CategoryId,
                    DisplayName = c.DisplayName
                });
            
            return View(new CategoryViewModel
            {
                Category = category,
                Products = GetProducts(category),
                ParentHierarchy = GetCategoryHierarchy(category.ParentCategoryId),
                Children = children
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
                CategoryHierarchy = GetCategoryHierarchy(product.CategoryId),
                TopLevelCategories = GetTopLevelCategories(db)
            });
        }

        private IEnumerable<Product> GetProducts(Category category)
        {
            // TODO Look at moving this to a stored procedure or similar when raw SQL is available
            var childTree = db.Categories
                .AsNoTracking()
                .Include(c => c.Children)
                    .ThenInclude(c => c.Children)
                    .ThenInclude(c => c.Children)
                    .ThenInclude(c => c.Children)
                .Where(c => c.ParentCategoryId == category.CategoryId);

            var categoryIds = new int[] { category.CategoryId }
                .Union(GetAllCategoryIdsIncludingChildren(childTree));

            return db.Products
                .Where(p => categoryIds.Contains(p.CategoryId));
        }

        internal static IEnumerable<Category> GetTopLevelCategories(UnicornStoreContext db)
        {
            return db.Categories
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.DisplayName)
                .Include(c => c.Children);
        }

        private List<CategoryInfo> GetCategoryHierarchy(int? categoryId)
        {
            var parentTree = new List<CategoryInfo>();
            if (categoryId != null)
            {
                // TODO Look at moving this to a stored procedure or similar when raw SQL is available
                var parent = db.Categories
                    .AsNoTracking()
                    .Include(c => c.ParentCategory
                        .ParentCategory
                        .ParentCategory
                        .ParentCategory)
                    .SingleOrDefault(c => c.CategoryId == categoryId);

                for (var cat = parent; cat != null; cat = cat.ParentCategory)
                {
                    parentTree.Insert(0, new CategoryInfo
                    {
                        CategoryId = cat.CategoryId,
                        DisplayName = cat.DisplayName
                    });
                }
            }

            return parentTree;
        }

        private static IEnumerable<int> GetAllCategoryIdsIncludingChildren(IEnumerable<Category> categories)
        {
            return categories
                .Select(c => c.CategoryId)
                .Union(categories
                    .Where(c => c.Children != null)
                    .SelectMany(c => GetAllCategoryIdsIncludingChildren(c.Children)));
        }
    }
}
