using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;
using WFS.Framework.Responses;
using WFS.Framework.Extensions;
using C = WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SystemAdmin")]
    public class VendorController : BaseController
	{
		#region cstor const managers
		private readonly VendorManager _vendorMgr;

		public VendorController(VendorManager vendorMgr)
		{
			this._vendorMgr = vendorMgr;
		}
		#endregion

		#region get some stuff
		VendorListViewModel GetVendors()
		{
			var resp = _vendorMgr.GetVendorList(new GetOrganizationByTypeListRequest());

			var m = new VendorListViewModel() {
				Vendors = resp.Organizations
			};

			return m;
		}

		#endregion

		#region actions
		public ActionResult List()
        {
			var m = GetVendors();

            return View(m);
        }


		public ActionResult DisplayVendor(int vendorId)
		{
			var resp = _vendorMgr.GetVendorById(new GetOrganizationByIdRequest { OrganizationID = vendorId });

			var viewModel = new VendorEditModel((C.Vendor)resp.Organization);

			return View("DisplayVendor", viewModel);
		}


        public ActionResult EditVendor(int vendorID)
        {
            var resp = _vendorMgr.GetVendorById(new GetOrganizationByIdRequest { OrganizationID = vendorID });

			var viewModel = new VendorEditModel ((C.Vendor)resp.Organization);

			return View("EditVendor",  viewModel);
        }

		[HttpPost]
		public ActionResult Save(C.Vendor model)
		{
			var resp = _vendorMgr.SaveVendor(new C.ReqResp.Creates.SaveVendorRequest() { Subject = model });

			if (resp.Status == Status.Success){
				var uiresponse = resp.ToUIResult<VendorListViewModel, C.Vendor>((vendor) => GetVendors(), (vm) => RenderPartialViewToString("VendorList", vm));

				return Json(uiresponse);
			}
			else {
				var uiresponse = resp.ToUIResult<VendorEditModel, C.Vendor>((vendor) => new VendorEditModel(vendor)
					, (vm) => RenderPartialViewToString("EditVendor", vm));

				return Json(uiresponse);
			}
		}
		#endregion
	}
}
