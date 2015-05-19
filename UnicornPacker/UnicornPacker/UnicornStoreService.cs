using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnicornPacker.Models;

namespace UnicornPacker
{
    class UnicornStoreService
    {
        private static Uri serviceUri = new Uri("http://localhost:5000/");

        public static async Task<IEnumerable<Order>> GetPendingOrders()
        {
            using (var client = new HttpClient { BaseAddress = serviceUri })
            {
                var response = await client.GetStringAsync("Shipping/PendingOrders");
                return JsonConvert.DeserializeObject<IEnumerable<Order>>(response);
            }
        }

        public static async Task PackingOrders(IEnumerable<int> orderIds)
        {
            using (var client = new HttpClient { BaseAddress = serviceUri })
            {
                foreach (var id in orderIds)
                {
                    var response = await client.GetAsync("Shipping/Packing/" + id);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public static async Task ShippedOrder(int orderId)
        {
            using (var client = new HttpClient { BaseAddress = serviceUri })
            {
                var response = await client.GetAsync("Shipping/Shipped/" + orderId);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
