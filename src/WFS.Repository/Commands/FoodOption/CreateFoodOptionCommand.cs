using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateFoodOptionCommand : ICommand<C.FoodOption>
    {
        private readonly string _name;
        private readonly int _vendorId;
        private readonly string _description;
        private readonly decimal _cost;
        private readonly decimal _price;

        public CreateFoodOptionCommand(string name, int vendorId, string description, decimal cost, decimal price)
        {
            _vendorId = vendorId;
            _name = name;
            _description = description;
            _cost = cost;
            _price = price;
        }

        public IResult<C.FoodOption> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var fo = new WFS.DataContext.VendorFoodOption()
            {
                Name = _name,
                OrganizationId = _vendorId,
                Description = _description,
                Cost = _cost,
                Price = _price,
            };

            try
            {
                ent.VendorFoodOptions.Add(fo);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var fail = new Result<C.FoodOption>(Status.Error);
                fail.Messages.Add(new Message() { Text = ex.Message });
                return fail;
            }

            var result = new Result<C.FoodOption>(Status.Success, fo.ToContract());
            return result;
        }


    }
}
