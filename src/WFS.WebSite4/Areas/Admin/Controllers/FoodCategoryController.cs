using System.Web.Mvc;
using WFS.Domain.Managers;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    public class FoodCategoryController : BaseController
    {
        private readonly FoodItemManager _foodItemMgr;

        public FoodCategoryController(FoodItemManager foodItemMgr)
        {
            this._foodItemMgr = foodItemMgr;
        }

        public ActionResult Index()
        {
            return null;
        }
        public ActionResult EditVendor(int vendorID)
        {
            return null;
        }
        public ActionResult AddVendor()
        {
            return null;
        }

        [HttpPost]
        public ActionResult AddVendor(FoodCategoryEditModel model)
        {
            return null;
        }
    }
}