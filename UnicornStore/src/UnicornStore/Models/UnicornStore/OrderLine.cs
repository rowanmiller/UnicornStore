using System;

namespace UnicornStore.Models.UnicornStore
{
    public class OrderLine : ILineItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}