using System;
using System.Data.Entity;
using System.Linq;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class SaveWfsUserAccountCreditCommand : ICommand<C.WFSUser>
    {
        private readonly int _userId;
        private readonly decimal _credits;

        public SaveWfsUserAccountCreditCommand(int userId, decimal accountCredit)
        {
            _userId = userId;
            _credits = accountCredit;
        }

        public IResult<C.WFSUser> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new Result<C.WFSUser>();

            try
            {
                var user = context.WFSUsers.FirstOrDefault(x => x.UserId.Equals(_userId));
                user.AvailableCredit = _credits;

                dbContext.SaveChanges();
                result.Status = Status.Success;
                result.Value = user.ToContract();
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
