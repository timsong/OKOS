using System.Web.Mvc;
using WFS.WebSite4.Areas.Customer.Models;

namespace WFS.WebSite4.Areas.Customer.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Index(int userId)
        {
            var m = new OrderProfileViewModel();

            return View(m);
        }
        public ActionResult GetList(int userId)
        {
            return null;
        }

        public ActionResult DisplayProfile(int userId, int profileId)
        {
            return null;
        }
        public ActionResult DisplayProfile(int userId)
        {
            return null;
        }


    }
}
