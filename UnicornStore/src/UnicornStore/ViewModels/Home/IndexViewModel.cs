using System;
using System.Collections.Generic;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Category> TopLevelCategories { get; set; }
        public List<WebsiteAd> CurrentAds { get; set; }
    }
}