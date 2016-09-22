using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UnicornStore.Models.UnicornStore;

namespace UnicornStore.Controllers
{
    public class RecallsController : Controller
    {
        private UnicornStoreContext db;

        public RecallsController(UnicornStoreContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var recalls = db.Recalls
                .Include(r => r.Product)
                .ToList();

            return View(recalls);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Recall recall)
        {
            if (ModelState.IsValid)
            {
                db.Recalls.Add(recall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recall);
        }
    }
}
