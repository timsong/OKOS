using System.Web.Mvc;

namespace WFS.WebSite4.Areas.SchoolAdmin
{
    public class SchoolAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SchoolAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SchoolAdmin_default",
                "SchoolAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
