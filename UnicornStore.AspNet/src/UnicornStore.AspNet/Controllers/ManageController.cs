using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Security;
using UnicornStore.AspNet.Models;

namespace UnicornStore.AspNet.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        public ManageController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public SignInManager<ApplicationUser> SignInManager { get; private set; }

        public async Task<ActionResult> Index(ManageMessageId? message = null)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var user = await UserManager.FindByIdAsync(Context.User.Identity.GetUserId());
            var model = new IndexViewModel
            {
                Logins = await UserManager.GetLoginsAsync(user),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await UserManager.FindByIdAsync(Context.User.Identity.GetUserId());
            if (user != null)
            {
                var result = await UserManager.RemoveLoginAsync(user, loginProvider, providerKey);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(Context.User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(user);
            var otherLogins = SignInManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, User.Identity.GetUserId());
            return new ChallengeResult(provider, properties);
        }

        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await UserManager.FindByIdAsync(Context.User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var loginInfo = await SignInManager.GetExternalLoginInfoAsync(User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }

            var result = await UserManager.AddLoginAsync(user, loginInfo);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        public enum ManageMessageId
        {
            AddLoginSuccess,
            RemoveLoginSuccess,
            Error
        }
    }
}