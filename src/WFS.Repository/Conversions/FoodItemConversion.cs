using System;
using System.Linq;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class FoodItemConversion
    {
		static void Map(this C.FoodItem foodItem, FoodItem existing)
		{
			existing.Name = foodItem.Name;

			existing.Description = foodItem.Description;

			existing.VendorFoodCategoryID = foodItem.FoodCategoryId;

			existing.IsActive = foodItem.IsActive;

			existing.Cost = foodItem.Cost;

			existing.Price = foodItem.Price;
		}

		public static void ForUpdate(this FoodItem existing, C.FoodItem FoodItem)
		{
			FoodItem.Map(existing);
		}


        public static C.FoodItem ToContract(this FoodItem data)
        {
            if (data == null)
                return null;

            var model = new C.FoodItem()
            {
                Name = data.Name,
                Description = data.Description,
                FoodCategoryId = data.VendorFoodCategoryID,
                FoodItemId = data.FoodItemId,
                IsActive = data.IsActive,
				Category = data.VendorFoodCategory != null ? data.VendorFoodCategory.Name : string.Empty,
                Cost = data.Cost,
				Options = data.FoodItemOptions.Select(x => x.VendorFoodOption.ToContract()).ToList(),
                Price = data.Price
            };

            return model;
        }

		public static FoodItem ToDataModel(this C.FoodItem domain)
		{
			var model = new FoodItem { };

			domain.Map(model);

			return model;
		}
	}
}

