using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ContosoUniversity.DAL;
using System.Data.Entity.Infrastructure.Interception;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());

            // Only run migrations if the setting is true
            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                var configuration = new ContosoUniversity.Migrations.Configuration();
                //configuration.TargetDatabase = new DbConnectionInfo(
                //"Server=tcp:g74xl0join.database.windows.net,1433;Database=DevNet;User ID=OrahK613@g74xl0join;Password=OKeter613;Trusted_Connection=False;",
                //"System.Data.SqlClient");
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }


        }
    }
}
