using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(WFSUserTypeEnum.Admin, WFSUserTypeEnum.AccountManager)]
    public class LoginAsController : BaseController
    {
        private readonly WFSUserManager _userManager;

        public LoginAsController(WFSUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult LoginAs(int userId)
        {
            var resp = _userManager.GetWfsUserInfoById(new GetWfsUserInfoByIdRequest() { UserId = userId });

            // set the logout parameters
            var authCookieName = "adminAuthCookie";
            var authCookie = System.Web.HttpContext.Current.Request.Cookies[authCookieName] ?? new HttpCookie(authCookieName);

            authCookie.Values["UserId"] = AuthenticatedUserId.ToString();
            authCookie.Values["MembershipId"] = AuthenticatedMembershipId.ToString();

            //get the current login cookie
            var cookieName = "WHSUserId";
            var myCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            myCookie.Values["UserId"] = resp.Value.UserId.ToString();
            myCookie.Values["MembershipId"] = resp.Value.MembershipGuid.ToString();

            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);

            FormsAuthentication.SetAuthCookie(resp.Value.EmailAddress, false);

            return RedirectToRoute("Home");
        }

    }
}
