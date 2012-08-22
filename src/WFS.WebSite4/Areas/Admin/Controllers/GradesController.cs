using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    public class GradesController : Controller
    {
        public ActionResult List(int schoolId)
        {
            return View(GetList(schoolId));
        }

        private IView GetList(int schoolId)
        {
            throw new NotImplementedException();
        }

    }
}
