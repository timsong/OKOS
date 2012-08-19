using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;

namespace WFS.WebSite4.Controllers
{
    public class DirectorBaseController : BaseController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (this.HttpContext.Request.IsAuthenticated)
            {

                WFSUserManager _userManager = new WFSUserManager(new WFS.Repository.WFSRepository((DbContext)new WFS.DataContext.WFSEntities()));
                var resp = _userManager.GetWfsUserInfoById(new GetWfsUserInfoByIdRequest() { UserId = AuthenticatedUserId });

                var roles = Roles.GetRolesForUser(resp.Value.EmailAddress);

                var role = roles.FirstOrDefault();

                if (role != null)
                {

                    string routeName = string.Format("{0}.dashboard", role);

                    var context = new RequestContext(filterContext.HttpContext, filterContext.RouteData);

                    var url = string.Format("/{0}/Dashboard", roles.First());

                    context.HttpContext.Response.Redirect(url, true);
                }

            }
        }
    }
}
