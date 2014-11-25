using System.Threading.Tasks;
using System.Web.Mvc;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LEDE.WebUI.Infrastructure;
using System.Web;

namespace LEDE.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LoginRedirect");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        public ActionResult LoginRedirect()
        {
            if (HttpContext.User.IsInRole("Candidate"))
                return RedirectToAction("Index", "Candidate");
            else if (HttpContext.User.IsInRole("Faculty"))
                return RedirectToAction("Summary", "Faculty");
            else if (HttpContext.User.IsInRole("ECSEL Admin") || HttpContext.User.IsInRole("LEDE Admin")
                     || HttpContext.User.IsInRole("Super Admin"))
                return RedirectToAction("Index", "Admin");
            else
                return RedirectToAction("Logout"); 
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(details.Name,
                    details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    if (returnUrl != null && returnUrl != string.Empty)
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("LoginRedirect");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}