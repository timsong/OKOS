using System.Web.Mvc;
using WFS.Domain.Managers;
using WFS.Contract.ReqResp;

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
    }
}
