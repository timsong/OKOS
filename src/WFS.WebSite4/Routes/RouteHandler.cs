using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WFS.WebSite4
{
    public class RouteHandler
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            MapSupportRoutes(routes);

            routes.MapRoute(
                name: "home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "AboutUs",
                url: "about-us",
                defaults: new { controller = "General", action = "AboutUs" }
            );

            routes.MapRoute(
                name: "ContactUs",
                url: "contact-us",
                defaults: new { controller = "General", action = "ContactUs" }
            );

            routes.MapRoute(
                name: "Why",
                url: "why",
                defaults: new { controller = "General", action = "Why" }
            );

            routes.MapRoute(
                name: "HeathAndNutrition",
                url: "heath-and-nutrition",
                defaults: new { controller = "General", action = "HealthAndNutrition" }
            );

            routes.MapRoute(
                name: "Testimonials",
                url: "testimonials",
                defaults: new { controller = "General", action = "Testimonials" }
            );

            routes.MapRoute(
                name: "Support",
                url: "support",
                defaults: new { controller = "General", action = "Support" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "logoff",
                defaults: new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "ChangePassword",
                url: "change-password",
                defaults: new { controller = "Account", action = "ChangePassword" }
            );

            routes.MapRoute(
                name: "ChangePasswordSuccess",
                url: "change-password-success",
                defaults: new { controller = "Account", action = "ChangePasswordSuccess" }
            );
        }

        private static void MapSupportRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Support_NewSupportTicket",
                url: "Support/NewSupportTicket",
                defaults: new { controller = "SupportTicket", action = "NewSupportTicket" }
            );

            routes.MapRoute(
                name: "Support_GetTicketById",
                url: "Support/Tickets/Get/{ticketId}",
                defaults: new { controller = "SupportTicket", action = "GetTicket" }
            );

            routes.MapRoute(
                name: "Support_GetUnresolvedTickets",
                url: "Support/Tickets/GetList",
                defaults: new { controller = "SupportTicket", action = "GetUnresolvedTickets" }
            );


        }

    }
}