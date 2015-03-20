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

        [Display(Name ="Addressee")]
        public string ShippingAddressee { get; set; }
        [Display(Name ="Line One")]
        public string ShippingAddressLineOne { get; set; }
        [Display(Name ="Line Two")]
        public string ShippingAddressLineTwo { get; set; }
        [Display(Name = "City/Town")]
        public string ShippingCityOrTown { get; set; }
        [Display(Name ="State/Province")]
        public string ShippingStateOrProvince { get; set; }
        [Display(Name ="Zip/Postal Code")]
        public string ShippingZipOrPostalCode { get; set; }
        [Display(Name ="Country")]
        public string ShippingCountry { get; set; }

        [Display(Name ="Order #")]
        public string DisplayId
        {
            get { return OrderId.ToString().PadLeft(10, '0'); }
        }

        public List<OrderLine> Lines { get; set; }
    }
}