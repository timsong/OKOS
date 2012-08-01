using System;
using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework.Extensions;
using WFS.WebSite4.Models;

namespace WFS.WebSite4.Controllers
{
    [AllowAnonymous]
    public class SupportTicketController : BaseController
    {
        private readonly WFSUserManager _wfsUserMgr;

        public SupportTicketController(WFSUserManager wfsUserMgr)
        {
            this._wfsUserMgr = wfsUserMgr;
        }
        //
        // GET: /SupportTicket/

        public ActionResult NewSupportTicket()
        {
            if (User.Identity.IsAuthenticated && !String.IsNullOrEmpty(User.Identity.Name))
            {
                var userResp = _wfsUserMgr.GetWfsUserInfoByUserName(new GetWfsUserInfoByUserNameRequest() { UserName = User.Identity.Name });

                if (userResp.Status == Status.Success && userResp.UserInfo.UserId > 0)
                {
                    var uiresult = userResp.ToUIResult<SupportTicketNewModel>(() => new SupportTicketNewModel()
                    {
                        FirstName = userResp.UserInfo.FirstName,
                        LastName = userResp.UserInfo.LastName,
                        EmailAddress = userResp.UserInfo.EmailAddress,
                        UserId = userResp.UserInfo.UserId
                    }, (m) => RenderPartialViewToString("NewSupportTicket", m));

                    return Json(uiresult, JsonRequestBehavior.AllowGet);
                }
            }

            var noUserResult = new GetWfsUserInfoByUserNameResponse().ToUIResult<SupportTicketNewModel>(() => new SupportTicketNewModel() { },
                    (m) => RenderPartialViewToString("NewSupportTicket", m));

            return Json(noUserResult, JsonRequestBehavior.AllowGet);
        }

    }
}
