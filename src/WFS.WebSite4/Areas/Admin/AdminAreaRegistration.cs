﻿using System.Web.Mvc;

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
            MapUserRoutes(context);

            context.MapRoute("admin.companies", "Admin/Companies", new { controller = "Company", action = "Index" });
            context.MapRoute("admin.reports", "Admin/Reports", new { controller = "Report", action = "Index" });

            #region food category
            context.MapRoute("Admin_FoodCategory_List", "Admin/FoodCategory/GetList/{vendorID}", new { controller = "FoodCategory", action = "List" });
            context.MapRoute("Admin_FoodCategory_EditFoodCategory", "Admin/FoodCategory/EditFoodCategory/{FoodCategoryID}", new { controller = "FoodCategory", action = "EditFoodCategory" });
            context.MapRoute("Admin_FoodCategory_Save", "Admin/FoodCategory/Save", new { controller = "FoodCategory", action = "Save" });
            context.MapRoute("Admin_FoodCategory_DeleteFoodCategory", "Admin/FoodCategory/Delete/{vendorId}/{foodCategoryID}", new { controller = "FoodCategory", action = "DeleteFoodCategory" });
            context.MapRoute("Admin_FoodCategory_CreateFoodCategory", "Admin/FoodCategory/AddFoodCategory/{vendorId}", new { controller = "FoodCategory", action = "AddFoodCategory" });
            #endregion

            #region food option
            context.MapRoute("admin.foodoption.list", "Admin/FoodOption/GetList/{vendorID}", new { controller = "FoodOption", action = "List" });
            context.MapRoute("admin.foodoption.editfoodoption", "Admin/FoodOption/EditFoodOption/{foodOptionID}", new { controller = "FoodOption", action = "EditFoodOption" });
            context.MapRoute("admin.foodoption.save", "Admin/FoodOption/Save", new { controller = "FoodOption", action = "Save" });
            context.MapRoute("admin.foodoption.deletefoodoption", "Admin/FoodOption/Delete/{vendorId}/{foodOptionID}", new { controller = "FoodOption", action = "DeleteFoodOption" });
            context.MapRoute("admin.foodoption.createfoodoption", "Admin/FoodOption/AddFoodOption/{vendorId}", new { controller = "FoodOption", action = "AddFoodOption" });
            #endregion

            #region food item
            context.MapRoute("Admin_FoodItem_List", "Admin/FoodItem/GetList/{vendorID}", new { controller = "FoodItem", action = "List" });
            context.MapRoute("Admin_FoodItem_EditFoodItem", "Admin/FoodItem/EditFoodItem/{FoodItemID}", new { controller = "FoodItem", action = "EditFoodItem" });
            context.MapRoute("Admin_FoodItem_Save", "Admin/FoodItem/Save", new { controller = "FoodItem", action = "Save" });
            context.MapRoute("Admin_FoodItem_DeleteFoodItem", "Admin/FoodItem/Delete/{vendorId}/{foodItemID}", new { controller = "FoodItem", action = "DeleteFoodItem" });
            context.MapRoute("Admin_FoodItem_CreateFoodItem", "Admin/FoodItem/AddFoodItem/{vendorId}", new { controller = "FoodItem", action = "AddFoodItem" });
            context.MapRoute("Admin_FoodItem_SetFoodOption", "Admin/FoodItem/SetFoodOption/{foodItemId}/{foodOptionId}/{selected}", new { controller = "FoodItem", action = "SetFoodOption" });
            #endregion

            #region Vendor Routes
            context.MapRoute("admin.vendor.list", "Admin/Vendors/GetList", new { controller = "Vendor", action = "List" });
            context.MapRoute("admin.vendor.save", "Admin/Vendor/Save", new { controller = "Vendor", action = "Save" });
            context.MapRoute("admin.vendor.categories.list", "Admin/Vendor/Categories", new { controller = "FoodCategory", action = "Index" });
            context.MapRoute("admin.vendor.editVendor", "Admin/Vendors/EditVendor/{vendorID}", new { controller = "Vendor", action = "EditVendor" });
            context.MapRoute("admin.vendor.add", "Admin/Vendors/Add", new { controller = "Vendor", action = "AddVendor" });
            context.MapRoute("admin.vendor.display", "Admin/Vendors/DisplayVendor/{vendorID}", new { controller = "Vendor", action = "DisplayVendor" });
            context.MapRoute("admin.vendor.delete", "Admin/Vendors/Delete/{vendorId}", new { controller = "Vendor", action = "DeleteVendor" });
            context.MapRoute("admin.vendor.createVendor", "Admin/Vendors/AddVendor", new { controller = "Vendor", action = "AddVendor" });
            #endregion

            context.MapRoute("admin.LoginAs", "Admin/LoginAs/{userId}", new { controller = "LoginAs", action = "LoginAs" });
        }

        private void MapSchoolRoutes(AreaRegistrationContext context)
        {
            context.MapRoute("admin.schools", "admin/schools", new { controller = "School", action = "Schools" });
            context.MapRoute("admin.schools.list", "Admin/schools/GetList", new { controller = "School", action = "List" });
            context.MapRoute("admin.school.create", "admin/schools/create", new { controller = "School", action = "Create" });
            context.MapRoute("admin.school.edit", "admin/schools/edit/{schoolId}", new { controller = "School", action = "Edit" });
            context.MapRoute("admin.school.view", "admin/schools/{schoolid}", new { controller = "School", action = "School" });

            context.MapRoute("admin.grades.list", "Admin/Grades/GetList/{schoolId}", new { controller = "Grades", action = "List" });

        }
        private void MapUserRoutes(AreaRegistrationContext context)
        {
            context.MapRoute("admin.users",
                             "Admin/Users", new { controller = "User", action = "Index" });

            context.MapRoute("admin.users.search",
                              "Admin/Users/Search/{searchText}/{filter}", new { controller = "User", action = "PerformSearch" });

            context.MapRoute("admin.users.UserInfo",
                             "Admin/Users/UserInfo/{userId}", new { controller = "User", action = "GetUserInfo" });

            context.MapRoute("admin.users.UpdateUserInfo",
                             "Admin/Users/UpdateUserInfo", new { controller = "User", action = "UpdateUserInfo" });

            context.MapRoute("admin.users.UpdateUserBalance",
                             "Admin/Users/UpdateUserBalance", new { controller = "User", action = "UpdateUserBalance" });
        }
    }
}
