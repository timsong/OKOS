using System;
using System.Linq;
using System.Web.Mvc;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SystemAdmin")]
    public class FoodCategoryController : BaseController
    {
        private readonly VendorManager _vendorMgr;

        public FoodCategoryController(VendorManager vendorMgr)
        {
            this._vendorMgr = vendorMgr;
        }


        public ActionResult Index(int vendorID)
        {
            var resp = _vendorMgr.GetFoodCategoriesByVendor(new GetFoodCategoriesByVendorRequest()
                {
                    VendorId = vendorID,
                    ActiveDataRequest = ActiveDataRequestEnum.All
                });

            var m = new FoodCategoryListViewModel()
            {
                Categories = resp.FoodCategories
            };

            return View(m);
        }

        public ActionResult EditFoodCategory(int foodCategoryId)
        {
            var resp = _vendorMgr.GetFoodCategoryById(new GetFoodCategoryByIdRequest() { FoodCategoryID = foodCategoryId });

            var list = Enum.GetValues(typeof(FoodCategoryTypeEnum));
            var cateTypes = from FoodCategoryTypeEnum s in list
                            select new { ID = s.ToString(), Name = s.ToString() };

            ViewData["categoryTypes"] = new SelectList(cateTypes, "ID", "Name", resp.FoodCategory.CategoryType);

            var m = new FoodCategoryEditModel()
                {
                    Name = resp.FoodCategory.Name,
                    CategoryType = resp.FoodCategory.CategoryType.ToString(),
                    FoodCategoryId = resp.FoodCategory.FoodCategoryId,
                };

            var retString = RenderPartialViewToString("AddEdit", m);
            return Json(retString, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditFoodCategory(FoodCategoryEditModel model)
        {
            return null;
        }


        public ActionResult DeleteFoodCategory(int vendorID)
        {
            return null;
        }

        public ActionResult AddFoodCategory()
        {
            var m = new FoodCategoryEditModel();
            return View("AddEdit", m);
        }

        [HttpPost]
        public ActionResult AddFoodCategory(FoodCategoryEditModel model)
        {
            return null;
        }
    }
}