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

			context.MapRoute("admin.vendor.categories.list", "Admin/Vendor/Categories", new { controller = "FoodCategory", action = "Index" });

			#region food category
            context.MapRoute("Admin_FoodCategory_List", "Admin/FoodCategory/GetList/{vendorID}", new { controller = "FoodCategory", action = "List" });

			context.MapRoute("Admin_FoodCategory_EditFoodCategory", "Admin/FoodCategory/EditFoodCategory/{FoodCategoryID}", new { controller = "FoodCategory", action = "EditFoodCategory" } );

            context.MapRoute("Admin_FoodCategory_Save", "Admin/FoodCategory/Save", new { controller = "FoodCategory", action = "Save" });

            context.MapRoute("Admin_FoodCategory_DeleteFoodCategory","Admin/FoodCategory/DeleteFoodCategory/{FoodCategoryID}",new { controller = "FoodCategory", action = "DeleteFoodCategory" });

            context.MapRoute("Admin_FoodCategory_CreateFoodCategory", "Admin/FoodCategory/AddFoodCategory/{vendorId}", new { controller = "FoodCategory", action = "AddFoodCategory" });
            #endregion

			#region food option
			context.MapRoute("admin.foodoption.list", "Admin/FoodOption/GetList/{vendorID}", new { controller = "FoodOption", action = "List" });

			context.MapRoute("admin.foodoption.editfoodoption", "Admin/FoodOption/EditFoodOption/{foodOptionID}", new { controller = "FoodOption", action = "EditFoodOption" });

			context.MapRoute("admin.foodoption.save", "Admin/FoodOption/Save", new { controller = "FoodOption", action = "Save" });

			context.MapRoute("admin.foodoption.deletefoodoption", "Admin/FoodOption/DeleteFoodOption/{FoodOptionID}", new { controller = "FoodOption", action = "DeleteFoodOption" });

			context.MapRoute("admin.foodoption.createfoodoption", "Admin/FoodOption/AddFoodOption/{vendorId}", new { controller = "FoodOption", action = "AddFoodOption" });
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
			context.MapRoute("admin.vendor.add", "Admin/Vendors/Add", new { controller = "Vendor", action = "AddVendor" });
 
			context.MapRoute("admin.vendor.display", "Admin/Vendors/DisplayVendor/{vendorID}", new { controller = "Vendor", action = "DisplayVendor" });

			context.MapRoute("admin.vendor.delete", "Admin/Vendors/Delete/{vendorId}", new { controller = "Vendor", action = "DeleteVendor" });

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
                "admin/schools",
                new { controller = "School", action = "Schools" }
            );

            context.MapRoute(
                "admin.school.create",
                "admin/schools/create",
                new { controller = "School", action = "Create" }
            );

            context.MapRoute(
                "admin.school.edit",
                "admin/schools/edit/{schoolId}",
                new { controller = "School", action = "Edit" }
            );

            context.MapRoute(
                "admin.school.view",
                "admin/schools/{schoolid}",
                new { controller = "School", action = "School" }
            );
        }
    }
}
