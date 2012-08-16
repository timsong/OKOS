using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands.Vendor
{

    public class ActivateVendorFoodCategoryCommand : ICommand<bool>
    {
        private readonly int _vendorId;

        private readonly int _vendorFoodCategoryId;

        private readonly bool _isActive;

        public ActivateVendorFoodCategoryCommand(int vendorId, int vendorFoodCategoryId, bool isActive)
        {
            _vendorId = vendorId;

            _vendorFoodCategoryId = vendorFoodCategoryId;

            _isActive = isActive;
        }

        public IResult<bool> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

            var response = new ActivateFoodCategoryResponse();

            response.Status = Status.Success;

            try
            {
                var foodCategory = context.VendorFoodCategories.FirstOrDefault(x => x.OrganizationID.Equals(_vendorId) && x.VendorFoodCategoryId == _vendorFoodCategoryId);
                foodCategory.IsActive = _isActive;
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
