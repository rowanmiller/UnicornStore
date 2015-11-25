using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Shop
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
        public IEnumerable<Category> CategoryHierarchy { get; set; }
    }
}