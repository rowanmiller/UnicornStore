using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using UnicornStore.AspNet.Models.UnicornStore;
using UnicornStore.AspNet.ViewModels.Orders;

namespace UnicornStore.AspNet.Controllers
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
                .Where(o => o.Username == User.GetUserName())
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
                return new HttpStatusCodeResult(404);
            }

            if (order.Username != User.GetUserName())
            {
                return new HttpStatusCodeResult(403);
            }

            if (order.State == OrderState.CheckingOut)
            {
                return new HttpStatusCodeResult(400);
            }

            return View(new DetailsViewModel
            {
                Order = order,
                ShowConfirmation = showConfirmation
            });
        }
    }
}
