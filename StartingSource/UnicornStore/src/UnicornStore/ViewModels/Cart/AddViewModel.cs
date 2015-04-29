using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Cart
{
    public class AddViewModel
    {
        public CartItem CartItem { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
    }
}