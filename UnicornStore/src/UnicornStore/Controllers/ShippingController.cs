using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Controllers
{
    public class ShippingController : Controller
    {
        private UnicornStoreContext db;

        public ShippingController(UnicornStoreContext context)
        {
            db = context;
        }

        public IEnumerable<Order> PendingOrders()
        {
            var orders = db.Orders
                .Include(o => o.Lines).ThenInclude(l => l.Product)
                .Include(o => o.ShippingDetails)
                .Where(o => o.State == OrderState.Placed)
                .ToList();

            var dtos = orders.Select(o => new Order
            {
                OrderId = o.OrderId,
                Addressee = o.ShippingDetails.Addressee,
                LineOne = o.ShippingDetails.LineOne,
                LineTwo = o.ShippingDetails.LineTwo,
                CityOrTown = o.ShippingDetails.CityOrTown,
                StateOrProvince = o.ShippingDetails.StateOrProvince,
                ZipOrPostalCode = o.ShippingDetails.ZipOrPostalCode,
                Country = o.ShippingDetails.Country,
                Lines = o.Lines.Select(l => new OrderLine
                {
                    OrderId = l.OrderId,
                    ProductId = l.ProductId,
                    ProductName = l.Product.DisplayName,
                    Quantity = l.Quantity
                }).ToList()
            });

            return dtos;
        }

        public void Packing(int id)
        {
            var order = db.Orders.Single(o => o.OrderId == id);
            order.State = OrderState.Filling;
            db.SaveChanges();
        }

        public void Shipped(int id)
        {
            var order = db.Orders.Single(o => o.OrderId == id);
            order.State = OrderState.Shipped;
            db.SaveChanges();
        }

        public class Order
        {
            public int OrderId { get; set; }
            public string Addressee { get; set; }
            public string LineOne { get; set; }
            public string LineTwo { get; set; }
            public string CityOrTown { get; set; }
            public string StateOrProvince { get; set; }
            public string ZipOrPostalCode { get; set; }
            public string Country { get; set; }

            public List<OrderLine> Lines { get; set; }
        }

        public class OrderLine
        {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
        }
    }
}
