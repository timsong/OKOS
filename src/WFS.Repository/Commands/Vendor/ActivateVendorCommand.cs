using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Contract.ReqResp;

namespace WFS.Repository.Commands.Vendor
{
    public class ActivateVendorCommand : ICommand<bool>
    {
        private readonly int _vendorId;

        private readonly bool _isActive;

        public ActivateVendorCommand(int vendorId, bool isActive)
        {
            _vendorId = vendorId;

            _isActive = isActive;
        }

        public IResult<bool> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

            var response = new ActivateVendorResponse();

            response.Status = Status.Success;

            try
            {
                var vendor = context.Organizations.FirstOrDefault(x => x.OrganizationId.Equals(_vendorId));
                vendor.IsActive = _isActive;
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
