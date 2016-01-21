using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity.Migrations;
using System.Web.Configuration;


namespace LEDE.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if(bool.Parse(WebConfigurationManager.AppSettings.Get("MigrateDatabaseToLatestVersion"))){
                var configuration = new LEDE.Domain.Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }

        }
    }
}
