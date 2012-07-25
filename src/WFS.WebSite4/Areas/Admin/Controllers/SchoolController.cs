using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFS.WebSite4.Controllers;
using WFS.Domain.Managers;
using WFS.Contract.ReqResp;
using WFS.Contract.Enums;
using WFS.WebSite4.Areas.Admin.Models;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,SystemAdmin")]
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
    }
}
