using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateFoodCategoryCommand : ICommand<C.FoodCategory>
    {
        private readonly string _name;
        private readonly int _vendorId;
        private readonly string _catType;

        public CreateFoodCategoryCommand(string name, int vendorId, string catType)
        {
            _vendorId = vendorId;
            _name = name;
            _catType = catType;
        }

        public IResult<C.FoodCategory> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var fc = new WFS.DataContext.VendorFoodCategory()
            {
                Name = _name,
                OrganizationID = _vendorId,
                CategoryType = _catType,
            };

            try
            {
                ent.VendorFoodCategories.Add(fc);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var fail = new Result<C.FoodCategory>(Status.Error);
                fail.Messages.Add(new Message() { Text = ex.Message });
                return fail;
            }

            var result = new Result<C.FoodCategory>(Status.Success, fc.ToContract());
            return result;
        }


    }
}
