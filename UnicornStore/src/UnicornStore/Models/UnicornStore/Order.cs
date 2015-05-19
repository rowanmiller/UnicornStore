using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public DateTime CheckoutBegan { get; set; }
        [Display(Name ="Order Placed")]
        public DateTime? OrderPlaced { get; set; }
        public decimal Total { get; set; }
        [Display(Name ="Status")]
        public OrderState State { get; set; }

        [Display(Name ="Order #")]
        public string DisplayId
        {
            get { return OrderId.ToString().PadLeft(10, '0'); }
        }

        public List<OrderLine> Lines { get; set; }

        public OrderShippingDetails ShippingDetails { get; set; }
    }
}