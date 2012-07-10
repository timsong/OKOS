using System.Web.Mvc;
using System.Web.Routing;

namespace WFS_WebSite
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

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);

            routes.MapRoute(
                "Default", // Route name
                "", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "About", // Route name
                "about", // URL with parameters
                new { controller = "Home", action = "About", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Login", // Route name
                "login", // URL with parameters
                new { controller = "Account", action = "Logon", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "LogOff", // Route name
                "logoff", // URL with parameters
                new { controller = "Account", action = "Logoff", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Register", // Route name
                "register", // URL with parameters
                new { controller = "Account", action = "Register", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "ChangePassword", // Route name
                "changepassword", // URL with parameters
                new { controller = "Account", action = "ChangePassword", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "ChangePasswordSuccess", // Route name
                "changepassword/success", // URL with parameters
                new { controller = "Account", action = "ChangePasswordSuccess", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}