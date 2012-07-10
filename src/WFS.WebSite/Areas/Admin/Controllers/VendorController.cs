using System.Web.Mvc;
using WFS.Contract.ReqResp;
using WFS.Domain.Managers;

namespace WFS_WebSite.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
        private VendorManager _venMan;

        public VendorController(VendorManager vendorManager)
        {
            _venMan = vendorManager;
        }
        public ActionResult List()
        {
            var data = _venMan.GetVendorList(new GetVendorListRequest());
            return View(data.Vendors);
        }

        public ActionResult Edit(int vendorId)
        {
            return View("AddEdit");
        }

        public ActionResult Delete(int vendorId)
        {
            return View("AddEdit");
        }
    }
}
