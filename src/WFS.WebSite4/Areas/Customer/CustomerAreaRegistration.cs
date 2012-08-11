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
            context.MapRoute("Customer.Profile.GetList", "Customer/Profile/GetList/{userId}", new { controller = "Profile", action = "GetList" });

            context.MapRoute("Customer.Profile.Add", "Customer/Profile/Add/{userId}", new { controller = "Profile", action = "AddProfile" });
            context.MapRoute("Customer.Profile.Add", "Customer/Profile/Edit/{profileIdId}", new { controller = "Profile", action = "EditProfile" });
            context.MapRoute("Customer.Profile.SetInfoScreen", "Customer/Profile/SetInfo", new { controller = "Profile", action = "SetInfo" });
            context.MapRoute("Customer.Profile.SetSchoolInfoScreen", "Customer/Profile/SetSchoolInfo", new { controller = "Profile", action = "SetSchoolInfo" });
            context.MapRoute("Customer.Profile.Save", "Customer/Profile/Save", new { controller = "Profile", action = "SaveProfile" });

            #endregion

        }
    }
}
