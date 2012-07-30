using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class FoodCategoryConversion
    {
		static void Map(this C.FoodCategory foodCategory, VendorFoodCategory existing)
		{
			existing.Name = foodCategory.Name;

			existing.CategoryType = foodCategory.CategoryType.ToString();

			existing.OrganizationID = foodCategory.VendorId;
		}

		public static VendorFoodCategory ToDataModel(this C.FoodCategory domain)
		{
			if (domain == null) return null;

			var data = new VendorFoodCategory ();

			domain.Map(data);

			return data;
		}

        public static C.FoodCategory ToContract(this VendorFoodCategory data)
        {
            if (data == null)
                return null;

            var model = new C.FoodCategory()
            {
                FoodCategoryId = data.VendorFoodCategoryId,
                Name = data.Name,
                VendorId = data.OrganizationID,
                CategoryType = (FoodCategoryTypeEnum)Enum.Parse(typeof(FoodCategoryTypeEnum), data.CategoryType),
            };

            return model;
        }

		public static void ForUpdate(this VendorFoodCategory existing, C.FoodCategory foodCategory)
		{
			foodCategory.Map(existing);
		}
    }
}

