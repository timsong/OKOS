using System.Web.Mvc;
using WFS.Contract;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(WFSRoleEnum.Admin, WFSRoleEnum.SystemAdmin)]
	public class SchoolController : BaseController
    {
        private readonly SchoolManager _schoolMgr;

        public SchoolController(SchoolManager schoolMgr)
        {
            this._schoolMgr = schoolMgr;
        }

        public ActionResult Schools()
        {
            var response = this._schoolMgr.GetSchoolList(new GetSchoolsRequest());

            var model = new SchoolsViewModel()
            {
                Schools = response.Schools
            };

            return View(model);
        }

        public ActionResult School(int schoolID)
        {
            var response = this._schoolMgr.GetSchool(new GetSchoolRequest() { SchoolID = schoolID });

            var model = new SchoolViewModel()
            {
                School = response.School
            };

            return View(model);
        }

        public ActionResult Create()
        {
            var viewModel = new SchoolAddModel(new School());
            var uiresult = new UIResponse<SchoolAddModel>();

            uiresult.Subject = viewModel;
            uiresult.HtmlResult = RenderPartialViewToString("createschool", viewModel);

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(School model)
        {
            var response = this._schoolMgr.CreateSchool(new CreateSchoolRequest() { School = model });

            if (response.Status == Status.Success)
            {
                return RedirectToRoute("admin.school.view", new { schoolId = response.School.OrganizationId });
            }
            else
            {
                var viewModel = new SchoolAddModel(model);
                var uiresult = new UIResponse<SchoolAddModel>();

                viewModel.Merge(response);
                uiresult.Subject = viewModel;
                uiresult.HtmlResult = RenderPartialViewToString("createschool", viewModel);

                return Json(uiresult);
            }
        }
    }
}
