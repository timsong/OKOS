using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFS.WebSite4.Areas.AccountManager.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /AccountManager/Dashboard/

        public ActionResult Index()
        {
            return View();
        }

    }
}
