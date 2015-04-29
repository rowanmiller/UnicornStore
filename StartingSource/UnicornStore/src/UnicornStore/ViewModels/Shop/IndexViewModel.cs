using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Shop
{
    public class IndexViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<Category> TopLevelCategories { get; set; }
    }
}