using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Concrete
{
    public class AppUserManager : UserManager<User, int>
    {
        public AppUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            DbContext db = context.Get<DbContext>();
            AppUserManager manager = new AppUserManager(new UserStore(db));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            manager.UserValidator = new CustomUserValidator(manager)
            {
                RequireUniqueEmail = false
            };

            return manager;
        }
    }
} 

