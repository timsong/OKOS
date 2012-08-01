using System.Web.Mvc;

namespace WFS.WebSite4.Controllers
{
    public class HomeController : DirectorBaseController
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}
