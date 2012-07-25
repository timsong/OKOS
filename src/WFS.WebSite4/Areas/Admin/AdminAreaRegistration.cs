using System.Web.Mvc;

namespace WFS.WebSite4.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("admin.dashboard", "Admin/Dashboard", new { controller = "Dashboard", action = "Index" });


            MapSchoolRoutes(context);
            

            context.MapRoute("admin.companies", "Admin/Companies", new { controller = "Company", action = "Index" });

            context.MapRoute("admin.users", "Admin/Users", new { controller = "User", action = "Index" });

            context.MapRoute("admin.reports", "Admin/Reports", new { controller = "Report", action = "Index" });

			context.MapRoute("admin.vendor.save", "Admin/Vendor/Save", new { controller = "Vendor", action = "Save" });

            #region food category
            context.MapRoute(
                "Admin_FoodCategory_List",
                "Admin/FoodCategorys/GetList",
                new { controller = "FoodCategory", action = "Index" }
            );

            context.MapRoute(
                "Admin_FoodCategory_EditFoodCategory",
                "Admin/FoodCategorys/EditFoodCategory/{FoodCategoryID}",
                new { controller = "FoodCategory", action = "EditFoodCategory" }
            );

            context.MapRoute(
                "Admin_FoodCategory_CreateFoodCategory",
                "Admin/FoodCategorys/AddFoodCategory",
                new { controller = "FoodCategory", action = "AddFoodCategory" }
            );
            #endregion


            context.MapRoute(
                "Admin_Vendor_List",
                "Admin/Vendors/GetList",
                new { controller = "Vendor", action = "List" }
            );

            context.MapRoute(
                "Admin_Vendor_EditVendor",
                "Admin/Vendors/EditVendor/{vendorID}",
                new { controller = "Vendor", action = "EditVendor" }
            );

            context.MapRoute(
                "Admin_Vendor_CreateVendor",
                "Admin/Vendors/AddVendor",
                new { controller = "Vendor", action = "AddVendor" }
            );
        }

        private void MapSchoolRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "admin.schools",
                "Admin/Schools",
                new { controller = "School", action = "Schools" }
            );
            
            context.MapRoute(
                "admin.school.view",
                "Admin/Schools/{schoolid}",
                new { controller = "School", action = "School" }
            );
        }
    }
}
