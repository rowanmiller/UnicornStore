using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnicornStore.AspNet.Models.Identity;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Cart
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<UserAddress> UserAddresses { get; set; }

        [Display(Name ="Remember Address")]
        public bool RememberAddress { get; set; }
    }
}