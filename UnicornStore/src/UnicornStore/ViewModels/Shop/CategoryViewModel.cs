using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Shop
{
    public class CategoryViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Category> ParentHierarchy { get; set; }
        public IEnumerable<Category> Children { get; set; }
    }
}