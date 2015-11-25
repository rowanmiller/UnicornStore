using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Shop
{
    public class IndexViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
    }
}