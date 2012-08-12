using System;
using WFS.Repository.Conversions;
using System.Linq;
using C = WFS.Contract;
using WFS.Contract.ReqResp;
using WFS.Framework;

namespace WFS.Repository.Commands
{
    public class SetFoodOptionForFoodItemCommand : ICommand<bool>
    {
        private readonly int _foodOptionId;
	
		private readonly int _foodItemId;

		private readonly bool _selected;

		public SetFoodOptionForFoodItemCommand(int foodOptionId, int foodItemId, bool selected)
        {
			_foodOptionId = foodOptionId;

			_foodItemId = foodItemId;

			_selected = selected;
        }

		public IResult<bool> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

			var response = new SetFoodOptionForFoodItemResponse();

			response.Status = Status.Success;

            try
            {
				var foodOption = context.FoodItemOptions.FirstOrDefault(x => x.FoodItemId.Equals(_foodItemId) && x.VendorFoodOptionId.Equals(_foodOptionId));

				if (foodOption != null && !_selected)
				{
					context.FoodItemOptions.Remove(foodOption);
				}
				else if (foodOption == null && _selected)
				{
					context.FoodItemOptions.Add(new DataContext.FoodItemOption { FoodItemId = _foodItemId, VendorFoodOptionId = _foodOptionId });
				}

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
				response.Status = Status.Error;

				response.Messages.Add(new Message() { Text = ex.Message });

				return response;
            }

            return response;
        }

	}
}
