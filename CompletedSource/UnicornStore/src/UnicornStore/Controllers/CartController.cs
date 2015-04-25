using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using UnicornStore.AspNet.Models;
using UnicornStore.AspNet.Models.Identity;
using UnicornStore.AspNet.Models.UnicornStore;
using UnicornStore.AspNet.ViewModels.Cart;

namespace UnicornStore.AspNet.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UnicornStoreContext db;
        private ApplicationDbContext identityDb;

        public CartController(UnicornStoreContext context, ApplicationDbContext identityContext)
        {
            db = context;
            identityDb = identityContext;
        }

        public IActionResult Index(IndexMessage message = IndexMessage.None)
        {
            var items = db.CartItems
                .Where(i => i.Username == User.GetUserName())
                .Include(i => i.Product);

            return View(new IndexViewModel
            {
                CartItems = items,
                TopLevelCategories = ShopController.GetTopLevelCategories(db),
                Message = message
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
                item = new CartItem
                {
                    ProductId = product.ProductId,
                    PricePerUnit = product.CurrentPrice,
                    Quantity = quantity,
                    Username = User.GetUserName(),
                    PriceCalculated = DateTime.Now.ToUniversalTime()
                };
                db.CartItems.Add(item);
            }

            db.SaveChanges();

            return RedirectToAction("Index", new { message = IndexMessage.ItemAdded });
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

            return RedirectToAction("Index", new { message = IndexMessage.ItemRemoved });
        }

        public IActionResult Checkout()
        {
            var items = db.CartItems
                .Where(i => i.Username == User.GetUserName())
                .Include(i => i.Product)
                .ToList();

            var order = new Order
            {
                CheckoutBegan = DateTime.Now.ToUniversalTime(),
                Username = User.GetUserName(),
                Total = items.Sum(i => i.PricePerUnit * i.Quantity),
                State = OrderState.CheckingOut,
                Lines = items.Select(i => new OrderLine
                {
                    ProductId = i.ProductId,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    PricePerUnit = i.PricePerUnit,
                }).ToList()
            };

            db.ChangeTracker.TrackGraph(order, e => e.State = EntityState.Added);
            db.SaveChanges();

            // TODO workaround for https://github.com/aspnet/EntityFramework/issues/1449
            var orderFixed = db.Orders
                .AsNoTracking()
                .Include(o => o.Lines).ThenInclude(ol => ol.Product)
                .Single(o => o.OrderId == order.OrderId);

            orderFixed.ShippingDetails = new OrderShippingDetails();

            var addresses = identityDb.UserAddresses
                .Where(a => a.UserId == User.GetUserId())
                .ToList();

            return View(new CheckoutViewModel
            {
                Order = orderFixed,
                UserAddresses = addresses
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutViewModel formOrder)
        {
            var order = db.Orders
                .Include(o => o.Lines)
                .SingleOrDefault(o => o.OrderId == formOrder.Order.OrderId);

            if (order == null)
            {
                return new HttpStatusCodeResult(404);
            }

            if (order.Username != User.GetUserName())
            {
                return new HttpStatusCodeResult(403);
            }

            if (order.State != OrderState.CheckingOut)
            {
                return new HttpStatusCodeResult(400);
            }

            // Place order
            order.ShippingDetails = formOrder.Order.ShippingDetails.CloneTo<OrderShippingDetails>();
            order.State = OrderState.Placed;
            order.OrderPlaced = DateTime.Now.ToUniversalTime();
            db.SaveChanges();

            if(formOrder.RememberAddress)
            {
                var address = formOrder.Order.ShippingDetails.CloneTo<UserAddress>();
                address.UserId = User.GetUserId();
                identityDb.UserAddresses.Add(address);
                identityDb.SaveChanges();
            }

            // Remove items from cart
            var cartItems = db.CartItems
                .Where(i => i.Username == User.GetUserName())
                .ToList();

            foreach (var item in cartItems)
            {
                if (order.Lines.Any(l => l.ProductId == item.ProductId))
                {
                    db.CartItems.Remove(item);
                }
            }

            db.SaveChanges();

            return RedirectToAction("Details", "Orders", new { orderId = order.OrderId, showConfirmation = true });
        }
    }
}
