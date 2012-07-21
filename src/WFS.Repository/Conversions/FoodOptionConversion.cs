using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class FoodOptionConversion
    {
        public static C.FoodOption ToContract(this VendorFoodOption data)
        {
            if (data == null)
                return null;

            var model = new C.FoodOption()
            {
                Name = data.Name,
                Description = data.Description,
                FoodOptionId = data.VendorFoodOptionId,
                Cost = data.Cost,
                Price = data.Price,
                VendorId = data.OrganizationId
            };

            return model;
        }
    }
}

