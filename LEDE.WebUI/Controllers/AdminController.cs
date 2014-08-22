using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Linq; 
using LEDE.Domain.Abstract;
using System.Collections.Generic; 

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles="ECSEL Admin, LEDE Admin, Super Admin")]
    public class AdminController : Controller
    {
        private IUserRepository users;

        public AdminController(IUserRepository repo)
        {
            this.users = repo; 
        }

        public ActionResult Index(int? id)
        {
            IEnumerable<IndexModel> model = users.GetUsersWithRole();             

            return View(model);
        }

        public ActionResult Create()
        {
            CreateModel model = new CreateModel(){Roles = new SelectList(RoleManager.Roles, "Name", "Name")};
         
            return View(model); 
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {                
                User user = new User { UserName = model.UserName, FirstName = model.FirstName, 
                    LastName = model.LastName, UniversityID = model.UniversityID};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.SelectedRole); 
                    return RedirectToAction("Index");
                }                    
                else
                    AddErrorsFromResult(result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View("Error", result.Errors);
            }
            else
                return View("Error", new string[] { "User Not Found" });
        }

        public ActionResult Edit(int UserID)
        {
            EditModel model = new EditModel();
            model.User = UserManager.FindById(UserID); 
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel post)
        {
            EditModel editModel = post;
            User user = await UserManager.FindByIdAsync(editModel.User.Id);
            if (user != null)
            {
                users.Edit(editModel.User);      
                IdentityResult validPass = null;
                if (editModel.Password != string.Empty && editModel.Password != null)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(editModel.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(editModel.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validPass == null) || (editModel.Password != string.Empty && validPass.Succeeded))
                {
                    return RedirectToAction("Index"); 
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
                       
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}