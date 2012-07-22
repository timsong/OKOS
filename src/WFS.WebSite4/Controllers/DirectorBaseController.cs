using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

namespace WFS.WebSite4.Controllers
{
	public class DirectorBaseController : BaseController
	{
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);

			if (this.HttpContext.Request.IsAuthenticated)
			{
				var roles = Roles.GetRolesForUser(User.Identity.Name);

				string routeName = string.Format("{0}.dashboard", roles.First());

				var context = new RequestContext(filterContext.HttpContext, filterContext.RouteData);

				var url = string.Format("/{0}/Dashboard", roles.First());

				context.HttpContext.Response.Redirect(url, true);

			}
		}
	}
}
