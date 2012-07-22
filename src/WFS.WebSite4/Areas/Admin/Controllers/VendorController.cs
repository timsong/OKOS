using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;
using WFS.WebSite4.Areas.Admin.Models;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SystemAdmin")]
    public class VendorController : BaseController
    {
        private readonly VendorManager _vendorMgr;

        public VendorController(VendorManager vendorMgr)
        {
            this._vendorMgr = vendorMgr;
        }

        public ActionResult List()
        {
            var resp = _vendorMgr.GetVendorList(new GetOrganizationByTypeListRequest());

            var m = new VendorListViewModel()
            {
                Vendors = resp.Organizations
            };

            return View(m);
        }
        public ActionResult EditVendor(int vendorID)
        {
            var resp = _vendorMgr.GetVendorList(new GetOrganizationByTypeListRequest());

            var m = new VendorListViewModel()
            {
                Vendors = resp.Organizations
            };

            return View(m);
        }
        public ActionResult AddVendor()
        {
            var model = new VendorEditModel()
            {
                IsNew = false,
            };

            model.Vendor.Name = "temp";
            model.Vendor.AddressInfo.Address2 = "456 Main St";

            return View("EditVendor", model);
        }

        [HttpPost]
        public ActionResult AddVendor(VendorEditModel model)
        {
            return View("EditVendor", new VendorEditModel());
        }
    }
}
