using System;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.Repository;
using WFS.WebSite4.Models;

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

                if (userResp.Status == Status.Success && userResp.Value.UserId > 0)
                {
                    var uiresult = userResp.ToUIResult<SupportTicketNewModel, WFSUser>(x => new SupportTicketNewModel()
                    {
                        FirstName = userResp.Value.FirstName,
                        LastName = userResp.Value.LastName,
                        EmailAddress = userResp.Value.EmailAddress,
                        UserId = userResp.Value.UserId,
                    }, (m) => RenderPartialViewToString("NewSupportTicket", m));

                    return Json(uiresult, JsonRequestBehavior.AllowGet);
                }
            }

            var noUserResult = new GetWfsUserInfoResponse().ToUIResult<SupportTicketNewModel, WFSUser>(x => new SupportTicketNewModel() { },
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

        public ActionResult GetUnresolvedTickets()
        {
            var resp = _tickManager.GetUnresolvedSupportTickets(new GetUnResolvedSupportTicketRequest());

            var model = new SupportTicketListViewModel()
            {
                Tickets = resp.Tickets
            };

            var uiresult = new UIResponse<SupportTicketListViewModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("UnresolvedTicketList", model);
            uiresult.Status = resp.Status;

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTicket(int ticketId)
        {
            var resp = _tickManager.GetSupportTicketById(new GetSupportTicketByIDRequest() { TicketID = ticketId });
            var model = new SupportTicketEditModel(resp.Ticket);

            var uiresult = new UIResponse<SupportTicketEditModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("ResolveSupportTicket", model);
            uiresult.Status = resp.Status;

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RoleAuthorize(WFSRoleEnum.Admin, WFSRoleEnum.SystemAdmin, WFSRoleEnum.AccountManager)]
        public ActionResult ResolveTicket(SupportTicketEditModel model)
        {
            var userResp = _wfsUserMgr.GetWfsUserInfoByUserName(new GetWfsUserInfoByUserNameRequest() { UserName = User.Identity.Name });

            model.Ticket.ResolvedByUserID = userResp.Value.UserId;

            var resp = _tickManager.SaveSupportTicket(new SaveSupportTicketRequest()
                {
                    Ticket = model.Ticket
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
