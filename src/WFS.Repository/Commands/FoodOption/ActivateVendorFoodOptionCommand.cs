using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands
{
    public class ActivateVendorFoodOptionCommand: ICommand<bool>
    {
        private readonly int _vendorFoodOptionId;

        private readonly bool _isActive;

        public ActivateVendorFoodOptionCommand(int vendorFoodOptionId, bool isActive)
        {
            _vendorFoodOptionId = vendorFoodOptionId;

            _isActive = isActive;
        }

        public IResult<bool> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

            var response = new ActivateVendorFoodOptionResponse();

            response.Status = Status.Success;

            try
            {
                var fooOption = context.VendorFoodOptions.FirstOrDefault(x => x.VendorFoodOptionId.Equals(_vendorFoodOptionId));
                fooOption.IsActive = _isActive;
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
