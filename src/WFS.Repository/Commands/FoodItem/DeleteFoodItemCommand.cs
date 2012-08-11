using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C = WFS.Contract;
using WFS.Contract.ReqResp;
using WFS.Repository.Conversions;
using System.Data.Objects;
using System.Data.Entity;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands
{
	public class DeleteFoodItemCommand : ICommand<bool>
	{
		private readonly int _foodItemId;

		public DeleteFoodItemCommand(int foodItemId)
		{
			_foodItemId = foodItemId;
		}

		public IResult<bool> Execute(DbContext dbContext)
		{
			var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new DeleteFoodItemResponse ();

			try
			{
				var foodItem = context.FoodItems.FirstOrDefault(x => x.FoodItemId.Equals(_foodItemId));

				context.SaveChanges();

				result.Status = Status.Success;

				result.Messages.Add(new Message { Code = "SUCCEEDEDDELETEFoodItem", Text = "FoodItem was deleted successfully" });
			}
			catch (Exception ex)
			{
				result.Status = Status.Error;

				result.Messages.Add(new Message { Code = "FAILEDTODELETEFoodItem", Text = ex.ToString() });
			}

			return result;
		}
	}
}
