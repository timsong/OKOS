using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.Framework;
using WFS.Framework.Extensions;
using WFS.Framework.Responses;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;
using C = WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [RoleAuthorize(C.Enums.WFSUserTypeEnum.Admin)]
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

            var m = new VendorListViewModel()
            {
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

        public ActionResult AddVendor()
        {
            var viewModel = new VendorEditModel(new C.Vendor());

            var uiresult = new UIResponse<VendorEditModel>();

            uiresult.Subject = viewModel;

            uiresult.HtmlResult = RenderPartialViewToString("EditVendor", viewModel);

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult DeleteVendor(int vendorId)
		{
			UIResponse<bool> response = _vendorMgr.DeleteVendor(new DeleteVendorRequest { VendorId = vendorId })
				.ToUIResult<bool, bool>((x) => true, (x) => RenderPartialViewToString("VendorList", GetVendors()));

			return Json(response);
		}

        public ActionResult EditVendor(int vendorID)
        {
            var resp = _vendorMgr.GetVendorById(new GetOrganizationByIdRequest { OrganizationID = vendorID });

            var uiresult = resp.ToUIResult<VendorEditModel>(() => new VendorEditModel((C.Vendor)resp.Organization)
                , (vm) => RenderPartialViewToString("EditVendor", vm));

            return Json(uiresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(C.Vendor model)
        {
            model.User.UserType = C.Enums.WFSUserTypeEnum.VendorAdmin;
            var resp = _vendorMgr.SaveVendor(new C.ReqResp.SaveVendorRequest() { Subject = model });

            if (resp.Status == Status.Success)
            {
                var uiresponse = resp.ToUIResult<VendorEditModel, C.Vendor>((vendor) => new VendorEditModel(resp.Value)
                    , (vm) => string.Empty);

                uiresponse.AdditionalPayload = new
                {
                    addressHtml = RenderPartialViewToString("DisplayAddress", uiresponse.Subject.Vendor)
                    ,
					contactHtml = RenderPartialViewToString("DisplayContact", uiresponse.Subject.Vendor)
                };

                return Json(uiresponse);
            }
            else
            {
                var uiresponse = resp.ToUIResult<VendorEditModel, C.Vendor>((vendor) => new VendorEditModel(vendor)
                    , (vm) =>
                    {
                        vm.Merge(resp);

                        return RenderPartialViewToString("EditVendor", vm);
                    });
                return Json(uiresponse);
            }
        }
        #endregion
    }
}
