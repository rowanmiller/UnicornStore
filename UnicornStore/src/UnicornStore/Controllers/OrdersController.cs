using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnicornStore.Models.UnicornStore;
using UnicornStore.ViewModels.Orders;
using UnicornStore.Data;

namespace UnicornStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private UnicornStoreContext db;

        public OrdersController(UnicornStoreContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var orders = db.Orders
                .Include(o => o.Lines).ThenInclude(l => l.Product)
                .Where(o => o.Username == User.Identity.Name)
                .Where(o => o.State != OrderState.CheckingOut);

            return View(orders);
        }

        public IActionResult Details(int orderId, bool showConfirmation = false)
        {
            var order = db.Orders
                .Include(o => o.Lines).ThenInclude(ol => ol.Product)
                .Include(o => o.ShippingDetails)
                .SingleOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return new StatusCodeResult(404);
            }

            if (order.Username != User.Identity.Name)
            {
                return new StatusCodeResult(403);
            }

            if (order.State == OrderState.CheckingOut)
            {
                return new StatusCodeResult(400);
            }

            return View(new DetailsViewModel
            {
                Order = order,
                ShowConfirmation = showConfirmation
            });
        }
    }
}
