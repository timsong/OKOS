using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateFoodItemCommand : ICommand<C.FoodItem>
    {
        private readonly string _name;
        private readonly string _description;
        private readonly int _foodCatId;
        private readonly bool _isActive;
        private readonly decimal _cost;
        private readonly decimal _price;

        public CreateFoodItemCommand(string name, string description, int foodCatId, bool isActive, decimal cost, decimal price)
        {
            _name = name;
            _description = description;
            _foodCatId = foodCatId;
            _isActive = isActive;
            _cost = cost;
            _price = price;
        }

        public IResult<C.FoodItem> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var fc = new WFS.DataContext.FoodItem()
            {
                Name = _name,
                Description = _description,
                FoodCategoryID = _foodCatId,
                IsActive = _isActive,
                Cost = _cost,
                Price = _price,
            };

            try
            {
                ent.FoodItems.Add(fc);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var fail = new Result<C.FoodItem>(Status.Error);
                fail.Messages.Add(new Message() { Text = ex.Message });
                return fail;
            }

            var result = new Result<C.FoodItem>(Status.Success, fc.ToContract());
            return result;
        }


    }
}
