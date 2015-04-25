using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Shop
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
        public IEnumerable<CategoryInfo> CategoryHierarchy { get; set; }
    }
}