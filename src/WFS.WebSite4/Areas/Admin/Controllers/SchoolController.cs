using System;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(WFSUserTypeEnum.Admin)]
    public class SchoolController : BaseController
    {
        private readonly SchoolManager _schoolMgr;

        public SchoolController(SchoolManager schoolMgr)
        {
            this._schoolMgr = schoolMgr;
        }

        public ActionResult Schools()
        {
            return View();
        }
        public ActionResult List()
        {
            var uiresult = GetSchools();
            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        private UIResponse<SchoolsViewModel> GetSchools()
        {
            var response = this._schoolMgr.GetSchoolList(new GetSchoolsRequest());
            var model = new SchoolsViewModel()
            {
                Schools = response.Schools
            };

            var uiresult = new UIResponse<SchoolsViewModel>();
            uiresult.Subject = model;
            uiresult.HtmlResult = RenderPartialViewToString("SchoolList", model);
            uiresult.Status = response.Status;
            return uiresult;
        }

        public ActionResult School(int schoolID)
        {
            var response = this._schoolMgr.GetSchool(new GetSchoolRequest() { SchoolID = schoolID });

            var model = new SchoolViewModel()
            {
                School = response.Value
            };

            return View(model);
        }

        public ActionResult Create()
        {
            var viewModel = new SchoolAddEditModel();
            var uiresult = new UIResponse<SchoolAddEditModel>();

            uiresult.Subject = viewModel;
            uiresult.HtmlResult = RenderPartialViewToString("CreateEditSchool", viewModel);

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int schoolId)
        {
            var school = this._schoolMgr.GetSchool(new GetSchoolRequest() { SchoolID = schoolId });

            var viewModel = new SchoolAddEditModel(school.Value);
            var uiresult = new UIResponse<SchoolAddEditModel>();

            uiresult.Subject = viewModel;
            uiresult.HtmlResult = RenderPartialViewToString("createeditschool", viewModel);

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(SchoolAddEditModel model)
        {
            model.School.User.UserType = WFSUserTypeEnum.SchoolAdmin;
            model.School.User.Password = model.Password;

            var dt = DateTime.ParseExact(model.DeliveryTime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            model.School.DeliveryTime = dt.TimeOfDay;
            var resp = this._schoolMgr.CreateSchool(new SaveSchoolRequest() { Subject = model.School });

            if (resp.Status == Status.Success)
            {
                var uiResp = resp.ToUIResult<SchoolAddEditModel, School>(x => new SchoolAddEditModel(resp.Value), x => string.Empty);
                return Json(uiResp);
            }
            else
            {
                var uiResp = resp.ToUIResult<SchoolAddEditModel, School>(x => new SchoolAddEditModel(model.School), x =>
                {
                    x.Merge(resp);
                    return (RenderPartialViewToString("createeditschool", new SchoolAddEditModel(model.School)));
                });
                return Json(uiResp);
            }
        }

    }
}
