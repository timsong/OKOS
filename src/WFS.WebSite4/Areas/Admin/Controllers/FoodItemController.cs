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
    public class FoodItemController : BaseController
    {
        private readonly VendorManager _vendorMgr;

        public FoodItemController(VendorManager vendorMgr)
        {
            this._vendorMgr = vendorMgr;
        }

		FoodItemsListViewModel GetList(int vendorId)
		{
			var resp = _vendorMgr.GetFoodItemsByVendorId(new GetFoodItemsByVendorIdRequest() {
				VendorId = vendorId
			});

			var m = new FoodItemsListViewModel() {
				Items = resp.FoodItems
				, VendorId = vendorId
			};

			return m;
		}

        public ActionResult List(int vendorID)
        {
            return View(GetList(vendorID));
        }

        public ActionResult EditFoodItem(int foodItemId, int vendorId)
        {
			var resp = _vendorMgr.GetFoodItemById(new GetFoodItemByIdRequest { FoodItemId = foodItemId });

			var uiresult = resp.ToUIResult(() => {
				var model = new FoodItemEditModel(resp.FoodItem, vendorId);

				model.Categories = _vendorMgr.GetFoodCategoriesByVendor(new GetFoodCategoriesByVendorRequest { VendorId = vendorId }).FoodCategories;

				return model;
				}
				, (vm) => RenderPartialViewToString("AddEdit", vm));

			return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
        public ActionResult Save(FoodItem model, int vendorId)
        {
			SaveFoodItemResponse resp = _vendorMgr.SaveFoodItem(new SaveFoodItemRequest { Subject = model });

			if (resp.Status == Status.Success)
			{
				var uiresponse = resp.ToUIResult<FoodItemsListViewModel
					, FoodItem>((Options) => GetList(vendorId)
					, (vm) => RenderPartialViewToString("FoodItemList", vm));

				return Json(uiresponse);
			}
			else
			{
				var uiresponse = resp.ToUIResult<FoodItemEditModel
					, FoodItem>((Options) => new FoodItemEditModel(Options)
					, (vm) => {
						vm.Merge(resp);

						vm.Categories = _vendorMgr.GetFoodCategoriesByVendor(new GetFoodCategoriesByVendorRequest { VendorId = vendorId }).FoodCategories;

						return RenderPartialViewToString("AddEdit", vm);
					});
				return Json(uiresponse);
			}
        }

		[HttpPost]
		public ActionResult DeleteFoodItem(int vendorId, int FoodItemId)
		{
			UIResponse<bool> response = _vendorMgr.DeleteFoodItem(new DeleteFoodItemRequest { FoodItemId = FoodItemId })
				.ToUIResult<bool, bool>((x) => true, (x) => RenderPartialViewToString("FoodItemList", GetList(vendorId)));

			return Json(response);
		}

		public ActionResult AddFoodItem(int vendorId)
		{
			var m = new FoodItemEditModel();

			var uiresult = new UIResponse<FoodItemEditModel>();

			m.Categories = _vendorMgr.GetFoodCategoriesByVendor(new GetFoodCategoriesByVendorRequest { VendorId = vendorId }).FoodCategories;

			uiresult.Subject = m;

			uiresult.HtmlResult = RenderPartialViewToString("AddEdit", m);

			return Json(uiresult, JsonRequestBehavior.AllowGet);
		}
	}
}