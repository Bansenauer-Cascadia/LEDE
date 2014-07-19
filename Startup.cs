using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECSEL.Startup))]
namespace ECSEL
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
