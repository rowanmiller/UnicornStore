using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Shop
{
    public class CategoryViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Category Category { get; set; }
        public IEnumerable<CategoryInfo> ParentHierarchy { get; set; }
        public IEnumerable<CategoryInfo> Children { get; set; }
    }
}