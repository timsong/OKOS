using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class FoodOptionConversion
    {
		static void Map(this C.FoodOption foodOption, VendorFoodOption existing)
		{
			existing.Cost = foodOption.Cost;

			existing.Description = foodOption.Description;

			existing.Name = foodOption.Name;

			existing.OrganizationId = foodOption.VendorId;

			existing.Price = foodOption.Price;

			existing.VendorFoodOptionId = foodOption.FoodOptionId;
		}

		public static void ForUpdate(this VendorFoodOption existing, C.FoodOption FoodOption)
		{
			FoodOption.Map(existing);
		}

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

		public static VendorFoodOption ToDataModel (this C.FoodOption domain)
		{
			var model = new VendorFoodOption { };

			domain.Map(model);

			return model;
		}
	}
}

