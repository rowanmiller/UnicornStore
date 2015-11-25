using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Orders
{
    public class DetailsViewModel
    {
        public Order Order { get; set; }
        public bool ShowConfirmation { get; set; }
    }
}