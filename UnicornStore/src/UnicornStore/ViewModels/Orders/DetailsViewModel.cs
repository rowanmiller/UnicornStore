using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Orders
{
    public class DetailsViewModel
    {
        public Order Order { get; set; }
        public bool ShowConfirmation { get; set; }
    }
}