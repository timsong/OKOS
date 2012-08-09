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
            context.MapRoute("Customer.Dashboard", "Customer/Dashboard/{membershipId}", new { controller = "Dashboard", action = "Index" });

            #region Profiles
            context.MapRoute("Customer.Profile.Index", "Customer/Profile/Index/{membershipId}", new { controller = "Profile", action = "Index" });
            context.MapRoute("Customer.Profile.GetList", "Customer/Profile/GetList/{membershipId}", new { controller = "Profile", action = "GetList" });

            context.MapRoute("Customer.Profile.Display", "Customer/Profile/DisplayProfile/{membershipId}/{profileId}", new { controller = "Profile", action = "DisplayProfile" });
            context.MapRoute("Customer.Profile.Add", "Customer/Profile/Add", new { controller = "Profile", action = "AddProfile" });

            #endregion

        }
    }
}
