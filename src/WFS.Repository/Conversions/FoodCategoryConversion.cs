using System;
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class FoodCategoryConversion
    {
        public static C.FoodCategory ToContract(this FoodCategory data)
        {
            if (data == null)
                return null;

            var model = new C.FoodCategory()
            {
                FoodCategoryId = data.FoodCategoryId,
                Name = data.Name,
                VendorId = data.VendorID,
                CategoryType = (FoodCategoryTypeEnum)Enum.Parse(typeof(FoodCategoryTypeEnum), data.CategoryType),
            };

            return model;
        }
    }
}

