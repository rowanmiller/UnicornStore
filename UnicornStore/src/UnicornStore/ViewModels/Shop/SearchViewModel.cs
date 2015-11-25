using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Shop
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}