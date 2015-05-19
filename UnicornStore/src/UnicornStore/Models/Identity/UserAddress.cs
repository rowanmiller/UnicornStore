using System;
using System.ComponentModel.DataAnnotations;

namespace UnicornStore.AspNet.Models.Identity
{
    public class UserAddress : IAddress
    {
        public int UserAddressId { get; set; }

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
        public string Country { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}