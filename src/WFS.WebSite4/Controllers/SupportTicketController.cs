using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.WebSite4.Models;
using WFS.Repository;
using System;

namespace WFS.WebSite4.Controllers
{
    [AllowAnonymous]
    public class SupportTicketController : BaseController
    {
        private readonly WFSUserManager _wfsUserMgr;
        private readonly SupportTicketManager _tickManager;

        public SupportTicketController(WFSUserManager wfsUserMgr, SupportTicketManager tickManager)
        {
            this._wfsUserMgr = wfsUserMgr;
            this._tickManager = tickManager;
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
        [HttpPost]
        public ActionResult NewSupportTicket(SupportTicketNewModel model)
        {
            var newTick = new SupportTicket()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                ContactPhone = model.ContactPhone,
                IssueText = model.IssueText,
                SupportCategory = (SupportCategoryEnum)Enum.Parse(typeof(SupportCategoryEnum), model.SelectedCategory),
            };

            var resp = _tickManager.SaveSupportTicket(new SaveSupportTicketRequest()
                {
                    Ticket = newTick
                });


            if (resp.Status == Status.Success)
            {
                var uiresponse = resp.ToUIResult<SupportTicketNewModel>(() => new SupportTicketNewModel(resp.Ticket), (vm) => string.Empty);
                return Json(uiresponse);
            }
            else
            {
                var uiresponse = new Result<SupportTicketNewModel>(Status.Error);
                uiresponse.Messages.AddRange(resp.Messages);

                return Json(uiresponse);
            }
        }
    }
}
