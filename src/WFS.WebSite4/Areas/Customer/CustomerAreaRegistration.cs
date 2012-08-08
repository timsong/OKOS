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
            context.MapRoute("Customer.Dashboard", "Customer/Dashboard", new { controller = "Dashboard", action = "Index" });

            #region Profiles
            context.MapRoute("Customer.Profile.Index", "Customer/Profile/Index/{userId}", new { controller = "Profile", action = "Index" });
            context.MapRoute("Customer.Profile.GetList", "Customer/Profile/GetList/{userId}", new { controller = "Profile", action = "GetList" });

            context.MapRoute("Customer.Profile.Display", "Customer/Profile/DisplayProfile/{userId}/{profileId}", new { controller = "Profile", action = "DisplayProfile" });
            context.MapRoute("Customer.Profile.Add", "Customer/Profile/Add/{userId}", new { controller = "Profile", action = "AddProfile" });

            #endregion

        }
    }
}
