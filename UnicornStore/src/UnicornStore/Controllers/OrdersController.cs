using System.Linq;
using Microsoft.EntityFrameworkCore;
using UnicornStore.Models.UnicornStore;
using UnicornStore.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UnicornStore.Models.Identity;

namespace UnicornStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private UnicornStoreContext db;

        public OrdersController(UserManager<ApplicationUser> manager, UnicornStoreContext context)
        {
            userManager = manager;
            db = context;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);

            var orders = db.Orders
                .Include(o => o.Lines).ThenInclude(l => l.Product)
                .Where(o => o.UserId == userId)
                .Where(o => o.State != OrderState.CheckingOut);

            return View(orders);
        }

        public IActionResult Details(int orderId, bool showConfirmation = false)
        {
            var userId = userManager.GetUserId(User);

            var order = db.Orders
                .Include(o => o.Lines).ThenInclude(ol => ol.Product)
                .Include(o => o.ShippingDetails)
                .SingleOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return new StatusCodeResult(404);
            }

            if (order.UserId != userId)
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
