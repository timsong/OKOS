using System.Web.Mvc;
using WFS.Domain.Managers;

namespace WFS.WebSite4.Controllers
{
    public class HomeController : DirectorBaseController
    {
        private readonly WFSUserManager _userManager;

        public HomeController(WFSUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
