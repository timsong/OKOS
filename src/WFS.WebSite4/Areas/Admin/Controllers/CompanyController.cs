using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFS.WebSite4.Controllers;
using WFS.Contract.Enums;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(WFSRoleEnum.Admin)]
	public class CompanyController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
