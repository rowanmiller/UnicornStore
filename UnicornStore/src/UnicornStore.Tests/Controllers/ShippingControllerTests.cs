using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UnicornStore.Controllers;
using UnicornStore.Models.UnicornStore;
using Xunit;

namespace UnicornStore.Tests.Controllers
{
    public class ShippingControllerTests
    {
        [Fact]
        public void GetPendingOrders()
        {
            var builder = new DbContextOptionsBuilder<UnicornStoreContext>();
            builder.UseInMemoryDatabase();
            var options = builder.Options;

            using (var context = new UnicornStoreContext(options))
            {
                var orders = new List<Order>
                {
                    new Order { State = OrderState.CheckingOut, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Placed, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Filling, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.ReadyToShip, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Shipped, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Delivered, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Cancelled, ShippingDetails = new OrderShippingDetails() },
                };

                context.AddRange(orders);
                context.AddRange(orders.Select(o => o.ShippingDetails));
                context.SaveChanges();
            }

            using (var context = new UnicornStoreContext(options))
            {
                var controller = new ShippingController(context);
                var orders = controller.PendingOrders();
                Assert.Equal(1, orders.Count());
            }
        }
    }
}
