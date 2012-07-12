using System;
using System.Linq;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class FoodItemConversion
    {
        public static C.FoodItem ToContract(this FoodItem data)
        {
            if (data == null)
                return null;

            var model = new C.FoodItem()
            {
                Name = data.Name,
                Description = data.Description,
                FoodCategoryId = data.FoodCategoryID,
                FoodItemId = data.FoodItemId,
                IsActive = data.IsActive,
                Cost = data.Cost,
                Price = data.Price,
                Category = data.FoodCategory.Name,
                CategoryType = (FoodCategoryTypeEnum)Enum.Parse(typeof(FoodCategoryTypeEnum), data.FoodCategory.CategoryType)
            };


            if (data.FoodItemOptions.Count > 0)
                model.Options.AddRange(data.FoodItemOptions.Select(x => x.FoodOption.ToContract()).ToList());

            return model;
        }
    }
}

