using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateVendorMenuCommand : ICommand<C.Menu>
    {
        private readonly string _name;
        private readonly int _vendorId;
        private readonly string _description;
        private readonly bool _isActive;

        public CreateVendorMenuCommand(string name, int vendorId, string description, bool isActive)
        {
            _vendorId = vendorId;
            _name = name;
            _description = description;
            _isActive = isActive;
        }

        public IResult<C.Menu> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var fo = new WFS.DataContext.Menu()
            {
                Name = _name,
                VendorId = _vendorId,
                Description = _description,
                IsActive = _isActive
            };

            try
            {
                ent.Menus.Add(fo);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var fail = new Result<C.Menu>(Status.Error);
                fail.Messages.Add(new Message() { Text = ex.Message });
                return fail;
            }

            var result = new Result<C.Menu>(Status.Success, fo.ToContract());
            return result;
        }


    }
}
