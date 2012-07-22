using System.Web.Mvc;

namespace WFS.WebSite4.Areas.Vendor
{
	public class VendorAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Vendor";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("vendor.dashboard", "Vendor/Dashboard", new { controller = "Dashboard", action = "Index" });

			context.MapRoute(
				"Vendor_default",
				"Vendor/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
