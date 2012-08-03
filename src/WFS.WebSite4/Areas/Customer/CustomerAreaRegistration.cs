using System.Web.Mvc;

namespace WFS.WebSite4.Areas.Customer
{
    public class CustomerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Customer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Customer_Profile_List",
                "Customer/Profile/GetList",
                new { controller = "Profile", action = "Index" }
            );

        }
    }
}
