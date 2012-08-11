using System;
using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Customer.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Customer.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly WFSUserManager _wfsUserMgr;
        private readonly OrderProfileManager _profManager;

        public ProfileController(WFSUserManager wfsUserMgr, OrderProfileManager profManager)
        {
            this._wfsUserMgr = wfsUserMgr;
            this._profManager = profManager;
        }
        //
        // GET: /SupportTicket/
        public ActionResult Index(Guid membershipId)
        {
            var m = new OrderProfileViewModel();
            return View(m);
        }
        public ActionResult GetList(Guid membershipId)
        {
            var resp = _profManager.GetListOfProfiles(new GetOrderProfileListRequest() { MembershipId = membershipId });
            var model = new OrderProfileViewModel()
            {
                Profiles = resp.Values
            };

            var uiresult = new UIResponse<OrderProfileViewModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("ProfileList", model);
            uiresult.Status = resp.Status;

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayProfile(Guid membershipId, int profileId)
        {
            return null;
        }

        public ActionResult AddProfile()
        {
            var model = new OrderProfileAddEditModel();

            //populate data here

            var uiresult = new UIResponse<OrderProfileAddEditModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("AddProfile", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetInfo(OrderProfileAddEditModel model)
        {
            //populate data here

            var uiresult = new UIResponse<OrderProfileAddEditModel>();
            uiresult.Subject = model;

            uiresult.HtmlResult = RenderPartialViewToString(model.IsSchool.Value ? "StudentInfo" : "EmployeeInfo", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

    }
}
