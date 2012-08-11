using System.Linq;
using System.Data.Objects;
using System.Data.Entity;
using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands
{
    public class SaveFoodCategoryCommand : ICommand<C.FoodCategory>
    {
        private readonly C.FoodCategory _foodCategory;

        public SaveFoodCategoryCommand(C.FoodCategory foodCategory)
        {
			_foodCategory = foodCategory;
        }

        public IResult<C.FoodCategory> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new SaveFoodCategoryResponse();

			result.Value = _foodCategory;

            try
            {
				if (_foodCategory.FoodCategoryId <= 0)
				{
					var foodCategory = context.VendorFoodCategories.Add(_foodCategory.ToDataModel());

					dbContext.SaveChanges();

					result.Value = foodCategory.ToContract();

					result.Messages.Add(new Message { Text = "Food Category saved successfully." });
				}
				else
				{
					var foodCategory = context.VendorFoodCategories.FirstOrDefault(x => x.VendorFoodCategoryId.Equals(_foodCategory.FoodCategoryId));

					foodCategory.ForUpdate(_foodCategory);

					dbContext.SaveChanges();

					result.Messages.Add(new Message { Text = "Food Category saved successfully." });
				}
            }
            catch (Exception ex)
            {
				result.Status = Status.Error;

				result.Messages.Add(new Message { Code = "DIE!", Text = ex.ToString() });
			}

            return result;
        }


    }
}
