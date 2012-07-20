using System.Web.Mvc;

namespace WFS.WebSite4.Areas.Admin
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
                "Admin_Vendor_List",
                "Admin/Vendors/GetList",
                new { controller = "Vendor", action = "List" }
            );

            context.MapRoute(
                "Admin_Vendor_EditVendor",
                "Admin/Vendors/EditVendor/{vendorID}",
                new { controller = "Vendor", action = "EditVendor" }
            );
        }
    }
}
