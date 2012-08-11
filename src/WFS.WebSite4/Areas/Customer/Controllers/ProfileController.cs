using System;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
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
            var resp = _wfsUserMgr.GetWfsUserInfoByMembershipId(new GetWfsUserInfoByMembershipIdRequest() { MembershipId = membershipId });

            var m = new OrderProfileViewModel()
            {
                MembershipId = membershipId,
                UserId = resp.Value.UserId
            };

            return View(m);
        }
        public ActionResult GetList(int userId)
        {
            var resp = _profManager.GetListOfProfiles(new GetOrderProfileListRequest() { UserId = userId });
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

        public ActionResult AddProfile(int userId)
        {
            var model = new OrderProfileAddEditModel();
            model.Profile.UserId = userId;

            //populate data here

            var uiresult = new UIResponse<OrderProfileAddEditModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("AddProfile", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditProfile(int profileId)
        {
            var model = new OrderProfileAddEditModel();
            var resp = _profManager.GetOrderProfileById(new GetOrderProfileByIdRequest() { ProfileId = profileId });

            model.Profile = resp.Value;

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
            if (model.IsSchool.Value)
            {
                model.Schools.Clear();

                model.Schools.Add(new SelectListItem() { Value = "0", Text = "Please Select a School", Selected = true });
                model.Schools.Add(new SelectListItem() { Value = "1", Text = "University Park Elelmentary" });
                model.Schools.Add(new SelectListItem() { Value = "2", Text = "El Camino Real High School" });
                model.Schools.Add(new SelectListItem() { Value = "3", Text = "St. Andrews Academy" });
                model.Schools.Add(new SelectListItem() { Value = "4", Text = "South High School" });
                model.Schools.Add(new SelectListItem() { Value = "5", Text = "Lompoc Correctional School" });
                model.Schools.Add(new SelectListItem() { Value = "6", Text = "ITT Tech" });
            }


            var uiresult = new UIResponse<OrderProfileAddEditModel>();
            uiresult.Subject = model;

            uiresult.HtmlResult = RenderPartialViewToString(model.IsSchool.Value ? "SchoolInfo" : "EmployeeInfo", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetSchoolInfo(OrderProfileAddEditModel model)
        {
            var uiresult = new UIResponse<OrderProfileAddEditModel>();
            uiresult.Subject = model;

            uiresult.HtmlResult = RenderPartialViewToString("StudentInfo", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProfile(OrderProfileAddEditModel model)
        {
            var resp = _profManager.SaveOrderProfile(new SaveOrderProfileRequest() { Profile = model.Profile });

            if (resp.Status == Status.Success)
            {
                var uiResp = resp.ToUIResult<OrderProfileAddEditModel, OrderProfile>(x => new OrderProfileAddEditModel(resp.Value), x => string.Empty);
                return Json(uiResp);
            }
            else
            {
                var uiResp = resp.ToUIResult<OrderProfileAddEditModel, OrderProfile>(x => new OrderProfileAddEditModel(resp.Value), x =>
                    {
                        x.Merge(resp);
                        return RenderPartialViewToString("AddProfile", model);
                    });
                return Json(uiResp);
            }
        }


    }
}
