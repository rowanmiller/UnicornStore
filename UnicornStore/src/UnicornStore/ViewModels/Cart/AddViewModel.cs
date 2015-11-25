using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Cart
{
    public class AddViewModel
    {
        public CartItem CartItem { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
    }
}