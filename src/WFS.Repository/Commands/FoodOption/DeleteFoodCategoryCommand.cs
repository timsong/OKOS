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
	public class DeleteFoodOptionCommand : ICommand<bool>
	{
		private readonly int _foodOptionId;

		public DeleteFoodOptionCommand(int foodOptionId)
		{
			_foodOptionId = foodOptionId;
		}

		public IResult<bool> Execute(DbContext dbContext)
		{
			var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new DeleteFoodOptionResponse ();

			try
			{
				var foodOption = context.VendorFoodOptions.FirstOrDefault(x => x.VendorFoodOptionId.Equals(_foodOptionId));

				foodOption.IsDeleted = true;

				context.SaveChanges();

				result.Status = Status.Success;

				result.Messages.Add(new Message { Code = "SUCCEEDEDDELETEFoodOption", Text = "FoodOption was deleted successfully" });
			}
			catch (Exception ex)
			{
				result.Status = Status.Error;

				result.Messages.Add(new Message { Code = "FAILEDTODELETEFoodOption", Text = ex.ToString() });
			}

			return result;
		}
	}
}
