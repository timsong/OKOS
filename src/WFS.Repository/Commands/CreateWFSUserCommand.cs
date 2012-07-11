using System;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateWFSUserCommand : ICommand<C.WFSUser>
    {
        private readonly Guid _membershipGuid;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _userType;

        public CreateWFSUserCommand(Guid membershipGuid, string firstName, string lastName, string userType)
        {
            _membershipGuid = membershipGuid;
            _firstName = firstName;
            _lastName = lastName;
            _userType = userType;
        }
        #region ICommand<Vendor> Members

        public IResult<C.WFSUser> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var user = new WFS.DataContext.WFSUser();

            try
            {
                user.LastName = _lastName;
                user.FirstName = _firstName;
                user.MembershipGuid = _membershipGuid;
                user.UserType = _userType;
                user.AvailableCredit = 0.00M;

                ent.WFSUsers.Add(user);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var fail = new Result<C.WFSUser>(Status.Error);
                fail.Messages.Add(new Message() { Text = ex.Message });
                return fail;
            }

            var result = new Result<C.WFSUser>(Status.Success, user.ToContract());
            return result;
        }

        #endregion

    }
}
