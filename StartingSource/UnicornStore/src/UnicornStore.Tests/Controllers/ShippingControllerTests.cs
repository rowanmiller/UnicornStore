using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using UnicornStore.AspNet.Controllers;
using UnicornStore.AspNet.Models.UnicornStore;
using Xunit;

namespace UnicornStore.Tests.Controllers
{
    public class ShippingControllerTests
    {
        [Fact]
        public void GetPendingOrders()
        {
            // Arrange
            using (var context = new UnicornStoreContext())
            {
                var testData = new List<Order>
                {
                    new Order { State = OrderState.CheckingOut, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Placed, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Filling, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.ReadyToShip, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Shipped, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Delivered, ShippingDetails = new OrderShippingDetails() },
                    new Order { State = OrderState.Cancelled, ShippingDetails = new OrderShippingDetails() },
                };

                context.AddRange(testData);
                context.AddRange(testData.Select(p => p.ShippingDetails));
                context.SaveChanges();
            }

            using (var context = new UnicornStoreContext())
            {
                // TODO Act

                // TODO Assert
            }
        }
    }
}
