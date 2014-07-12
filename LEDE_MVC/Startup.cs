using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LEDE_MVC.Startup))]
namespace LEDE_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
