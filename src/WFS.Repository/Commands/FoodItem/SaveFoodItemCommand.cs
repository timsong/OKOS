using System.Linq;
using System.Data.Objects;
using System.Data.Entity;
using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands
{
    public class SaveFoodItemCommand : ICommand<C.FoodItem>
    {
        private readonly C.FoodItem _foodItem;

        public SaveFoodItemCommand(C.FoodItem foodItem)
        {
			_foodItem = foodItem;
        }

        public IResult<C.FoodItem> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new SaveFoodItemResponse();

			result.Value = _foodItem;

            try
            {
				if (_foodItem.FoodItemId <= 0)
				{
					var foodItem = context.FoodItems.Add(_foodItem.ToDataModel());

					dbContext.SaveChanges();

					result.Value = foodItem.ToContract();

					result.Messages.Add(new Message { Text = "Food Item saved successfully." });
				}
				else
				{
					var foodItem = context.FoodItems.FirstOrDefault(x => x.FoodItemId.Equals(_foodItem.FoodItemId));

					foodItem.ForUpdate(_foodItem);

					dbContext.SaveChanges();

					result.Messages.Add(new Message { Text = "Food Item saved successfully." });
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
