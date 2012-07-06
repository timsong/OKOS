using System.Web.Mvc;

namespace WFS_WebSite.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                 "admin.vendor.getlist",
                 "Admin/Vendor/GetList",
                 new {controller = "Vendor", action = "List"});

        }
    }
}
