using System;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Customer.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Customer.Controllers
{
    [RoleAuthorize(WFSRoleEnum.Customer, WFSRoleEnum.Admin, WFSRoleEnum.AccountManager)]
    public class ProfileController : BaseController
    {
        private readonly WFSUserManager _wfsUserMgr;
        private readonly OrderProfileManager _profManager;
        private readonly SchoolManager _schoolMgr;

        public ProfileController(WFSUserManager wfsUserMgr, OrderProfileManager profManager, SchoolManager schoolMgr)
        {
            this._wfsUserMgr = wfsUserMgr;
            this._profManager = profManager;
            this._schoolMgr = schoolMgr;
        }

        public ActionResult Index()
        {
            var i = AuthenticatedUserId;
            var resp = _wfsUserMgr.GetWfsUserInfoById(new GetWfsUserInfoByIdRequest() { MembershipId = AuthenticatedMembershipId });

            var m = new OrderProfileViewModel()
            {
                MembershipId = AuthenticatedMembershipId,
                UserId = resp.Value.UserId
            };

            return View(m);
        }
        public ActionResult GetList()
        {
            var uiresult = GetListOfProfiles();

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        private UIResponse<OrderProfileViewModel> GetListOfProfiles()
        {
            var resp = _profManager.GetListOfProfiles(new GetOrderProfileListRequest() { UserId = AuthenticatedUserId });
            var model = new OrderProfileViewModel()
            {
                Profiles = resp.Values
            };

            var uiresult = new UIResponse<OrderProfileViewModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("ProfileList", model);
            uiresult.Status = resp.Status;
            return uiresult;
        }

        public ActionResult AddProfile()
        {
            var model = new OrderProfileAddEditModel();
            model.Profile.UserId = AuthenticatedUserId;

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
            uiresult.HtmlResult = RenderPartialViewToString("EditProfile", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SetInfo(OrderProfileAddEditModel model)
        {
            //populate data here
            if (model.IsSchool.Value)
            {
                var resp = _schoolMgr.GetSchoolList(new GetSchoolsRequest() { DataRequest = ActiveDataRequestEnum.ActiveOnly });

                model.Schools.Clear();

                model.Schools.Add(new SelectListItem() { Value = "0", Text = "Please Select a School", Selected = true });
                foreach (var x in resp.Schools)
                    model.Schools.Add(new SelectListItem() { Value = x.OrganizationId.ToString(), Text = x.Name });
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

            var resp = _profManager.GetOrderProfleSetupDataBySchool(new GetOrderProfleSetupDataRequest() { SchoolId = model.Profile.OrganizationId });

            model.Grades.Clear();
            model.LunchPeriods.Clear();
            model.Teachers.Clear();

            resp.Grades.ForEach(x => model.Grades.Add(new SelectListItem() { Value = x.SchoolGradeId.ToString(), Text = x.Name }));
            resp.LunchPeriods.ForEach(x => model.LunchPeriods.Add(new SelectListItem() { Value = x.LunchPeriodId.ToString(), Text = String.Format("{0} - {1}", x.StartTime.ToShortTimeString(), x.EndTime.ToShortTimeString()) }));
            resp.Teachers.ForEach(x => model.Teachers.Add(new SelectListItem() { Value = x.TeacherId.ToString(), Text = String.Format("{0}, {1}", x.LastName, x.FirstName) }));

            uiresult.HtmlResult = RenderPartialViewToString("StudentInfo", model);
            uiresult.Status = Status.Success;
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProfile(OrderProfileAddEditModel model)
        {
            if (!String.IsNullOrEmpty(model.SelectedLunchPeriod))
            {
                int id = Convert.ToInt32(model.SelectedLunchPeriod);
                model.Profile.LunchPeriodId = (id > 0) ? id : (int?)null;
            }

            if (!String.IsNullOrEmpty(model.SelectedGrade))
            {
                int id = Convert.ToInt32(model.SelectedGrade);
                model.Profile.GradeId = (id > 0) ? id : (int?)null;
            }

            if (!String.IsNullOrEmpty(model.SelectedTeacher))
            {
                int id = Convert.ToInt32(model.SelectedTeacher);
                model.Profile.TeacherId = (id > 0) ? id : (int?)null;
            }


            var resp = _profManager.SaveOrderProfile(new SaveOrderProfileRequest() { Profile = model.Profile });

            if (resp.Status == Status.Success)
            {
                var uiResp = resp.ToUIResult<OrderProfileAddEditModel, OrderProfile>(x => new OrderProfileAddEditModel(resp.Value), x => string.Empty);
                return Json(uiResp);
            }
            else
            {
                var uiResp = resp.ToUIResult<OrderProfileAddEditModel, OrderProfile>(x => new OrderProfileAddEditModel(model.Profile), x =>
                    {
                        x.Merge(resp);
                        return (x.Profile.OrderProfileId > 0) ? RenderPartialViewToString("EditProfile", model) : RenderPartialViewToString("AddProfile", model);
                    });
                return Json(uiResp);
            }
        }

        [HttpPost]
        public ActionResult DeleteProfile(int profileId)
        {
            UIResponse<bool> response = _profManager.DeleteOrderProfile(new DeleteOrderProfileRequest() { ProfileId = profileId })
                .ToUIResult<bool, bool>((x) => true, (x) => RenderPartialViewToString("ProfileList", GetListOfProfiles().Subject));

            return Json(response);
        }


    }
}
