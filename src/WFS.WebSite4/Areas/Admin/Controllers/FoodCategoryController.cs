using System;
using System.Linq;
using System.Web.Mvc;
using WFS.Contract.Enums;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;
using WFS.Framework.Responses;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(WFSRoleEnum.Admin, WFSRoleEnum.SystemAdmin)]
    public class FoodCategoryController : BaseController
    {
        private readonly VendorManager _vendorMgr;

        public FoodCategoryController(VendorManager vendorMgr)
        {
            this._vendorMgr = vendorMgr;
        }

		FoodCategoryListViewModel GetList(int vendorId)
		{
			var resp = _vendorMgr.GetFoodCategoriesByVendor(new GetFoodCategoriesByVendorRequest() {
				VendorId = vendorId,
				ActiveDataRequest = ActiveDataRequestEnum.All
			});

			var m = new FoodCategoryListViewModel() {
				Categories = resp.FoodCategories
				, VendorId = vendorId
			};

			return m;
		}


        public ActionResult List(int vendorID)
        {
            return View(GetList(vendorID));
        }

		void SetCategoryTypes(FoodCategoryTypeEnum categoryType)
		{
			var list = Enum.GetValues(typeof(FoodCategoryTypeEnum));
			var cateTypes = from FoodCategoryTypeEnum s in list
							select new { ID = s.ToString(), Name = s.ToString() };

			ViewData["categoryTypes"] = new SelectList(cateTypes, "ID", "Name", categoryType);
		}

        public ActionResult EditFoodCategory(int foodCategoryId)
        {
            var resp = _vendorMgr.GetFoodCategoryById(new GetFoodCategoryByIdRequest() { FoodCategoryID = foodCategoryId });

			SetCategoryTypes(resp.FoodCategory.CategoryType);

			var uiresult = resp.ToUIResult(() => new FoodCategoryEditModel(resp.FoodCategory)
				, (vm) => RenderPartialViewToString("AddEdit", vm));
			
			return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
        public ActionResult Save(FoodCategory model)
        {
			SaveFoodCategoryResponse resp = _vendorMgr.SaveFoodCategory(new SaveFoodCategoryRequest { Subject = model });

			if (resp.Status == Status.Success)
			{
				var uiresponse = resp.ToUIResult<FoodCategoryListViewModel
					, FoodCategory>((foodCategory) => GetList(model.VendorId)
					, (vm) => RenderPartialViewToString("FoodCategoryList", vm));

				return Json(uiresponse);
			}
			else
			{
				var uiresponse = resp.ToUIResult<FoodCategoryEditModel
					, FoodCategory>((foodCategory) => new FoodCategoryEditModel(foodCategory)
					, (vm) => {
						vm.Merge(resp);

						return RenderPartialViewToString("AddEdit", vm);
					});
				return Json(uiresponse);
			}
        }

        public ActionResult DeleteFoodCategory(int foodCategoryId)
        {
			//var resp = _vendorMgr.DeleteFoodCategory(new DeleteFoodCategory{ FoodCategoryId 
			throw new NotImplementedException();
		}

        public ActionResult AddFoodCategory(int vendorId)
        {
            var m = new FoodCategoryEditModel();

			m.Subject.VendorId = vendorId;

			SetCategoryTypes(FoodCategoryTypeEnum.Entree);

			var uiresult = new UIResponse<FoodCategoryEditModel>();

			uiresult.Subject = m;

			uiresult.HtmlResult = RenderPartialViewToString("AddEdit", m);

			return Json(uiresult, JsonRequestBehavior.AllowGet);
        }
    }
}