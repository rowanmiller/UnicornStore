using System;
using System.ComponentModel.DataAnnotations;

namespace UnicornStore.Models.UnicornStore
{
    public class OrderShippingDetails : IAddress
    {
        public int OrderId { get; set; }

        [Display(Name = "Addressee")]
        public string Addressee { get; set; }
        [Display(Name = "Line One")]
        public string LineOne { get; set; }
        [Display(Name = "Line Two")]
        public string LineTwo { get; set; }
        [Display(Name = "City/Town")]
        public string CityOrTown { get; set; }
        [Display(Name = "State/Province")]
        public string StateOrProvince { get; set; }
        [Display(Name = "Zip/Postal Code")]
        public string ZipOrPostalCode { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}