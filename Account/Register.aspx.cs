using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ECSEL.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using System.Data.Entity.Validation;

namespace ECSEL.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = new User()
            {
                UserName = Username.Text,
                UniversityID = UniversityID.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Email = Username.Text + "@null.com"
            };

            IdentityResult UserResult =  manager.Create(user, Password.Text);
            if (UserResult.Succeeded)
            {
                manager.AddToRole(user.Id, Role.SelectedItem.Text);                 

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = UserResult.Errors.FirstOrDefault();
            }
        }
    }
}