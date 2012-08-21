using System.Web.Mvc;

namespace WFS.WebSite4.Areas.AccountManager
{
    public class AccountManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AccountManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("accountmanager.dashboard", "AccountManager/Dashboard", new { controller = "Dashboard", action = "Index" });
            context.MapRoute(
                "AccountManager_default",
                "AccountManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
