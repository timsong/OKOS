using System.Web.Mvc;

namespace WFS.WebSite4.Areas.VendorAdmin
{
    public class VendorAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VendorAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("VendorAdmin.dashboard", "VendorAdmin_default/Dashboard", new { controller = "Dashboard", action = "Index" });

            context.MapRoute(
                "VendorAdmin.default",
                "VendorAdmin.default/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
