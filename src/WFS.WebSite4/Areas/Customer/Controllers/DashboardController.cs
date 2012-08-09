using System;
using System.Web.Mvc;

namespace WFS.WebSite4.Areas.Customer.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index(Guid membershipId)
        {
            return View();
        }

    }
}
