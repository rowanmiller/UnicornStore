using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnicornPacker.Models;
using Windows.Storage;
using Windows.UI.Xaml;

namespace UnicornPacker
{
    class UnicornStoreService
    {
        public static async Task< IEnumerable<Order>> GetPendingOrders()
        {
            await SimulateNetworkHit();

            var list = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    Addressee = "Jane Doe",
                    LineOne = "One Microsoft Way",
                    CityOrTown = "Redmond",
                    StateOrProvince = "WA",
                    ZipOrPostalCode = "98052",
                    Country = "United States",
                    Lines = new List<OrderLine>
                    {
                        new OrderLine { OrderId = 1, ProductId = 1, ProductName = "Red T-Shirt", Quantity = 2 },
                        new OrderLine { OrderId = 1, ProductId = 2, ProductName = "Blue T-Shirt", Quantity = 2 }
                    }
                },
                new Order
                {
                    OrderId = 2,
                    Addressee = "John Citizen",
                    LineOne = "One Microsoft Way",
                    CityOrTown = "Redmond",
                    StateOrProvince = "WA",
                    ZipOrPostalCode = "98052",
                    Country = "United States",
                    Lines = new List<OrderLine>
                    {
                        new OrderLine { OrderId = 2, ProductId = 1, ProductName = "Orange T-Shirt", Quantity = 2 },
                        new OrderLine { OrderId = 2, ProductId = 2, ProductName = "Coffee Mug", Quantity = 12 }
                    }
                }
            };

            using (var db = new OrdersContext())
            {
                var orders = db.Orders.Select(o => o.OrderId).ToList();
                return list.Where(o => !orders.Contains(o.OrderId));
            }
        }
      
        public static async Task AssignOrders(string employee, IEnumerable<int> orderIds)
        {
            await SimulateNetworkHit();
        }

        public static async Task RecordOrderShipped(int orderId)
        {
            await SimulateNetworkHit();
        }

        public static async Task RecordOrdersShipped(IEnumerable<int> orderIds)
        {
            await SimulateNetworkHit();
        }

        private static async Task SimulateNetworkHit()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://bing.com");
                var text = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
