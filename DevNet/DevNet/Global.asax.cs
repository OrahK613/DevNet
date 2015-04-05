using DevNet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DevNet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, DevNet.Migrations.Configuration>());


            // Only run migrations if the setting is true
            //if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaeToLatestVersion"]))
            //{
                //var configuration = new DevNet.Migrations.Configuration();
                //configuration.TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo("DefaultConnection_DatabasePublish");
                //var migrator = new DbMigrator(configuration);
                //migrator.Update();
            //}
        }
    }
}
