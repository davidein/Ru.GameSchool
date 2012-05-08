using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ru.GameSchool.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.IgnoreRoute( "elmah.axd" );
            
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "AddUserToCourse", // Route name
                "JSon/AddUserToCourse/{userId}/{courseId}", // URL with parameters
                new { controller = "JSon", action = "AddUserToCourse", userId = UrlParameter.Optional, courseId = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //BundleTable.Bundles.RegisterTemplateBundles();

            #if DEBUG
            var javascriptLibraries = new Bundle("~/lib");
            javascriptLibraries.AddDirectory("~/Scripts/lib", "*.js", false);
            BundleTable.Bundles.Add(javascriptLibraries);

            var javascriptApp = new Bundle("~/app");
            javascriptApp.AddDirectory("~/Scripts/app", "*.js", false);
            BundleTable.Bundles.Add(javascriptApp);
            #else
            var javascriptLibraries = new Bundle("~/lib", new JsMinify());
            javascriptLibraries.AddDirectory("~/Scripts/lib", "*.js", false);
            BundleTable.Bundles.Add(javascriptLibraries);

            var javascriptApp = new Bundle("~/app", new JsMinify());
            javascriptApp.AddDirectory("~/Scripts/app", "*.js", false);
            BundleTable.Bundles.Add(javascriptApp);
            #endif
        }
    }
}