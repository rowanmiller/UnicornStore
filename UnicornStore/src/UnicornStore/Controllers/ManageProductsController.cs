using System.Linq;
using Microsoft.EntityFrameworkCore;
using UnicornStore.Models.UnicornStore;
using UnicornStore.ViewModels.ManageProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnicornStore.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace UnicornStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageProductsController : Controller
    {
        private UnicornStoreContext db;
        CategoryCache categoryCache;

        public ManageProductsController(UnicornStoreContext context, CategoryCache cache)
        {
            db = context;
            categoryCache = cache;
        }

        // GET: Products
        public IActionResult Index(int? categoryId)
        {
            if (categoryId != null)
            {
                var ids = categoryCache.GetThisAndChildIds(categoryId.Value);
                return View(db.Products.Where(p => ids.Contains(p.CategoryId)));
            }

            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public IActionResult Details(System.Int32? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(404);
            }

            Product product = db.Products.Single(m => m.ProductId == id);
            if (product == null)
            {
                return new StatusCodeResult(404);
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "DisplayName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(System.Int32? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(404);
            }

            Product product = db.Products.Single(m => m.ProductId == id);
            if (product == null)
            {
                return new StatusCodeResult(404);
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "DisplayName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(System.Int32? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(404);
            }

            Product product = db.Products.Single(m => m.ProductId == id);
            if (product == null)
            {
                return new StatusCodeResult(404);
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(System.Int32 id)
        {
            Product product = db.Products.Single(m => m.ProductId == id);
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BulkPriceReduction()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "DisplayName");
            return View(new BulkPriceReductionViewModel { PercentageOffMSRP = 10 });
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BulkPriceReduction(BulkPriceReductionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ids = categoryCache.GetThisAndChildIds(model.CategoryId);
                var products = db.Products.Where(p => ids.Contains(p.CategoryId));

                foreach (var product in products)
                {
                    var discount = product.MSRP * model.PercentageOffMSRP / 100;
                    product.CurrentPrice = product.MSRP - discount;
                }

                db.SaveChanges();

                return RedirectToAction("Index", new { categoryId = model.CategoryId });
            }

            return View(model);
        }
    }
}
