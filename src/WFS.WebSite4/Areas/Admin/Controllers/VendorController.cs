using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFS.Domain.Managers;
using WFS.Contract.ReqResp;
using WFS.WebSite4.Areas.Admin.Models;

namespace WFS.WebSite4.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
        private readonly VendorManager _vendorMgr;

        public VendorController(VendorManager vendorMgr)
        {
            this._vendorMgr = vendorMgr;
        }

        public ActionResult List()
        {
            var resp = _vendorMgr.GetVendorList(new GetVendorListRequest());

            var m = new VendorListViewModel()
            {
                Vendors = resp.Vendors
            };

            return View(m);
        }
        public ActionResult EditVendor(int vendorID)
        {
            var resp = _vendorMgr.GetVendorList(new GetVendorListRequest());

            var m = new VendorListViewModel()
            {
                Vendors = resp.Vendors
            };

            return View(m);
        }

    }
}
