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
	public class DeleteFoodCategoryCommand : ICommand<bool>
	{
		private readonly int _foodCategoryId;

		public DeleteFoodCategoryCommand(int foodCategoryId)
		{
			_foodCategoryId = foodCategoryId;
		}

		public IResult<bool> Execute(DbContext dbContext)
		{
			var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new DeleteFoodCategoryResponse ();

			try
			{

				var foodCategory = context.VendorFoodCategories.FirstOrDefault(x => x.VendorFoodCategoryId.Equals(_foodCategoryId));

				foodCategory.IsDeleted = true;

				context.SaveChanges();

				result.Status = Status.Success;

				result.Messages.Add(new Message { Code = "SUCCEEDEDDELETEFoodCategory", Text = "FoodCategory was deleted successfully" });
			}
			catch (Exception ex)
			{
				result.Status = Status.Error;

				result.Messages.Add(new Message { Code = "FAILEDTODELETEFoodCategory", Text = ex.ToString() });
			}

			return result;
		}
	}
}
