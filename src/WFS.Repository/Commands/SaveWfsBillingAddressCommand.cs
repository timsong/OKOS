using System;
using System.Data.Entity;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class SaveWfsBillingAddressCommand : ICommand<C.CustomerAccount>
    {
        private readonly C.CustomerAccount _acct;

        public SaveWfsBillingAddressCommand(C.CustomerAccount account)
        {
            _acct = account;
        }

        public IResult<C.CustomerAccount> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new Result<C.CustomerAccount>();
            result.Value = _acct;

            try
            {
                var userAddy = context.WFSUserAddresses.FirstOrDefault(x => x.UserID.Equals(_acct.User.UserId));

                if (userAddy == null)
                {
                    var address = _acct.AddressInfo.ToDataModel(_acct.User.UserId);
                    context.WFSUserAddresses.Add(address);
                }
                else
                {
                    userAddy.ForUpdate(_acct.AddressInfo);
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
