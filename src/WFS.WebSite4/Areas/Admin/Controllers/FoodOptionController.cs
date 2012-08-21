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
    [RoleAuthorize(WFSRoleEnum.Admin)]
    public class FoodOptionController : BaseController
    {
        private readonly VendorManager _vendorMgr;

        public FoodOptionController(VendorManager vendorMgr)
        {
            this._vendorMgr = vendorMgr;
        }

		FoodOptionsListViewModel GetList(int vendorId)
		{
			var resp = _vendorMgr.GetFoodOptionByVendorId(new GetFoodOptionsByVendorRequest() {
				VendorId = vendorId
			});

			var m = new FoodOptionsListViewModel() {
				Options = resp.FoodOptions
				, VendorId = vendorId
			};

			return m;
		}

        public ActionResult List(int vendorID)
        {
            return View(GetList(vendorID));
        }

        public ActionResult EditFoodOption(int foodOptionId)
        {
			var resp = _vendorMgr.GetFoodOptionById(new GetFoodOptionByIdRequest { FoodOptionId = foodOptionId });

			var uiresult = resp.ToUIResult(() => new FoodOptionEditModel(resp.FoodOption)
				, (vm) => RenderPartialViewToString("AddEdit", vm));
			
			return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
        public ActionResult Save(FoodOption model)
        {
			SaveFoodOptionResponse resp = _vendorMgr.SaveFoodOption(new SaveFoodOptionRequest { Subject = model });

			if (resp.Status == Status.Success)
			{
				var uiresponse = resp.ToUIResult<FoodOptionsListViewModel
					, FoodOption>((Options) => GetList(model.VendorId)
					, (vm) => RenderPartialViewToString("FoodOptionList", vm));

				return Json(uiresponse);
			}
			else
			{
				var uiresponse = resp.ToUIResult<FoodOptionEditModel
					, FoodOption>((Options) => new FoodOptionEditModel(Options)
					, (vm) => {
						vm.Merge(resp);

						return RenderPartialViewToString("AddEdit", vm);
					});
				return Json(uiresponse);
			}
        }

		[HttpPost]
		public ActionResult DeleteFoodOption(int vendorId, int foodOptionId)
		{
			UIResponse<bool> response = _vendorMgr.DeleteFoodOption(new DeleteFoodOptionRequest { FoodOptionId = foodOptionId })
				.ToUIResult<bool, bool>((x) => true, (x) => RenderPartialViewToString("FoodOptionList", GetList(vendorId)));

			return Json(response);
		}

		public ActionResult AddFoodOption(int vendorId)
		{
			var m = new FoodOptionEditModel();

			m.Subject.VendorId = vendorId;

			var uiresult = new UIResponse<FoodOptionEditModel>();

			uiresult.Subject = m;

			uiresult.HtmlResult = RenderPartialViewToString("AddEdit", m);

			return Json(uiresult, JsonRequestBehavior.AllowGet);
		}
	}
}