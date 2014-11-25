using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Concrete
{
    public class AppRoleManager : RoleManager<Role, int>, IDisposable 
    {
        public AppRoleManager(IRoleStore<Role, int> store)
            : base(store)
        {
        }

        public static AppRoleManager Create(
                IdentityFactoryOptions<AppRoleManager> options,
                IOwinContext context)
        {
            return new AppRoleManager(new
                RoleStore(context.Get<DbContext>()));
        }
    }
}