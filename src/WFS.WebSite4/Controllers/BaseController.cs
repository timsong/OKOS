using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;

namespace WFS.WebSite4.Controllers
{
    public class BaseController : Controller
    {
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public int AuthenticatedUserId
        {
            get
            {
                var cookieName = "WHSUserId";
                var myCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];

                if (myCookie == null)
                {
                    FormsAuthentication.SignOut();
                    RedirectToAction("Index", "Home");
                    return 0;
                }
                else
                {
                    var i = Convert.ToInt32(myCookie.Values["UserId"]);
                    return i;
                }
            }
        }
        public Guid AuthenticatedMembershipId
        {
            get
            {
                var cookieName = "WHSUserId";
                var myCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];

                if (myCookie == null)
                    RedirectToRoute("LogOff");

                var g = new Guid(myCookie.Values["MembershipId"]);

                return g;
            }
        }
    }
}
