using System;
using System.Collections.Generic;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<Category> TopLevelCategories { get; set; }
        public List<WebsiteAd> CurrentAds { get; set; }
    }
}