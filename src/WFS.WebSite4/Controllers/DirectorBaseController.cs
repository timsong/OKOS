using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using WFS.Contract.Enums;

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
                var mem = Membership.GetUser(User.Identity.Name);

				var role = roles.FirstOrDefault();

				if (role != null)
				{

					string routeName = string.Format("{0}.dashboard", role);

					var context = new RequestContext(filterContext.HttpContext, filterContext.RouteData);

					var url = string.Format("/{0}/Dashboard", roles.First());

                    if (role == WFSRoleEnum.Customer.ToString())
                        url += "/" + mem.ProviderUserKey.ToString();

					context.HttpContext.Response.Redirect(url, true);
				}

			}
		}
	}
}
