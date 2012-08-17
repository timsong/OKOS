using System;
using System.Data.Entity;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class SaveWfsBillingAddressCommand : ICommand<C.WFSUser>
    {
        private readonly C.WFSUser _userInfo;

        public SaveWfsBillingAddressCommand(C.WFSUser userInfo)
        {
            _userInfo = userInfo;
        }

        public IResult<C.WFSUser> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new Result<C.WFSUser>();
            result.Value = _userInfo;

            try
            {
                var userAddy = context.WFSUserAddresses.FirstOrDefault(x => x.UserID.Equals(_userInfo.UserId));

                if (userAddy == null)
                {
                    var address = _userInfo.BillingAddress.ToDataModel(_userInfo.UserId);
                    context.WFSUserAddresses.Add(address);
                }
                else
                {
                    userAddy.ForUpdate(_userInfo.BillingAddress);
                }

                dbContext.SaveChanges();
                result.Status = Status.Success;
            }
            catch (Exception ex)
            {

                result.Status = Status.Error;
                result.Messages.Add(new Message { Code = "DIE!", Text = ex.ToString() });
            }

            return result;
        }
    }
}
