using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using WFS.Contract;
using System.Linq;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands
{
    public class SaveFoodOptionCommand : ICommand<C.FoodOption>
    {
        private readonly FoodOption _foodOption;

        public SaveFoodOptionCommand(FoodOption foodOption)
        {
			_foodOption = foodOption;
        }

        public IResult<C.FoodOption> Execute(System.Data.Entity.DbContext dbContext)
        {
			SaveFoodOptionResponse result = new SaveFoodOptionResponse();

			result.Status = Status.Success;

            var ent = (WFS.DataContext.WFSEntities)dbContext;

            try
            {
				if (_foodOption.FoodOptionId <= 0)
				{

					var newFo = ent.VendorFoodOptions.Add(_foodOption.ToDataModel());

					dbContext.SaveChanges();

					result.Messages.Add(new Message { Text = "Food Option saved successfully." });
				}
				else
				{
					var existing = ent.VendorFoodOptions.FirstOrDefault(x => x.VendorFoodOptionId.Equals(_foodOption.FoodOptionId));

					existing.ForUpdate(_foodOption);

					dbContext.SaveChanges();

					result.Value = (C.FoodOption)existing.ToContract();

					result.Messages.Add(new Message { Text = "Food Option saved successfully." });
				}
            }
            catch (Exception ex)
            {
                var fail = new Result<C.FoodOption>(Status.Error);
                
				fail.Messages.Add(new Message() { Text = ex.Message });
                
				return fail;
            }

			return result;
        }


    }
}
