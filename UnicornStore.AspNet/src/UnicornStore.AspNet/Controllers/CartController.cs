using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using UnicornStore.AspNet.Models.UnicornStore;
using UnicornStore.AspNet.ViewModels.Cart;

namespace UnicornStore.AspNet.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UnicornStoreContext db;

        public CartController(UnicornStoreContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var items = db.CartItems
                .Where(i => i.Username == User.GetUserName())
                .Include(i => i.Product);

            return View(new IndexViewModel
            {
                CartItems = items,
                TopLevelCategories = ShopController.GetTopLevelCategories(db)
            });
        }

        public IActionResult Add(int productId, int quantity = 1)
        {
            var product = db.Products
                .SingleOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var item = db.CartItems
                .SingleOrDefault(i => i.ProductId == product.ProductId
                                      && i.Username == User.GetUserName());

            if (item != null)
            {
                item.Quantity += quantity;
                item.PricePerUnit = product.CurrentPrice;
                item.PriceCalculated = DateTime.Now.ToUniversalTime();
            }
            else
            {
                item = db.CartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    PricePerUnit = product.CurrentPrice,
                    Quantity = quantity,
                    Username = User.GetUserName(),
                    PriceCalculated = DateTime.Now.ToUniversalTime()
                }).Entity;
            }

            db.SaveChanges();

            return View(new AddViewModel
            {
                CartItem = item,
                TopLevelCategories = ShopController.GetTopLevelCategories(db)
            });
        }

        public IActionResult Remove(int productId)
        {
            var item = db.CartItems
                .SingleOrDefault(i => i.ProductId == productId
                                      && i.Username == User.GetUserName());

            if (item == null)
            {
                return new HttpStatusCodeResult(404);
            }

            db.CartItems.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
