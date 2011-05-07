using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Services;
using SocialFreeks.Database;
using SocialFreeks.Entities;

namespace SocialFreeks
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Main", action = "Index", id = UrlParameter.Optional },
                new []{"SocialFreeks.Controllers"}
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            Settings.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            DatabaseMigrator.MigrateDatabase();
            ServiceFactory.Bootstrap();
        }

        protected void Application_End()
        {
            ServiceFactory.DisposeSessionManager();
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            ServiceFactory.BindSession();
        }

        public void Application_EndRequest(object sender, EventArgs e)
        {
            ServiceFactory.UnBindSession();
        }
    }
}