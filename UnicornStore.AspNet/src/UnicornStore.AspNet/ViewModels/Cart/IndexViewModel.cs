using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Cart
{
    public class IndexViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
    }
}