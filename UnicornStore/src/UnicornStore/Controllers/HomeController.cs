using System;
using System.Linq;
using UnicornStore.Models.UnicornStore;
using UnicornStore.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace UnicornStore.Controllers
{
    public class HomeController : Controller
    {
        private UnicornStoreContext db;
        CategoryCache categoryCache;

        public HomeController(UnicornStoreContext context, CategoryCache cache)
        {
            db = context;
            categoryCache = cache;
        }

        public IActionResult Index() 
        {
            var ads = db.WebsiteAds
                .Where(a => a.Start == null || a.Start <= DateTime.Now.ToUniversalTime())
                .Where(a => a.End == null || a.End >= DateTime.Now.ToUniversalTime());

            return View(new IndexViewModel
            {
                TopLevelCategories = categoryCache.TopLevel(),
                CurrentAds = ads.ToList()
            });
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}