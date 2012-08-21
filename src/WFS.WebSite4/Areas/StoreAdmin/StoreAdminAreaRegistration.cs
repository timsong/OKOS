using System.Web.Mvc;

namespace WFS.WebSite4.Areas.StoreAdmin
{
    public class StoreAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "StoreAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("storeadmin.dashboard", "Store/Dashboard", new { controller = "Dashboard", action = "Index" });

            context.MapRoute(
                "StoreAdmin_default",
                "StoreAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
