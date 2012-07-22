using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin,SystemAdmin")]
	public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
