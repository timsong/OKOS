using System;
using System.Linq;
using System.Data.Entity;
using WFS.Repository.Conversions;
using C = WFS.Contract;
using WFS.DataContext;
using WFS.Framework.Extensions;
using WFS.Contract.ReqResp;
using S = System.Web.Security;
namespace WFS.Repository.Commands
{
    public class SaveWFSUserCommand : ICommand<C.WFSUser>
    {
        private readonly C.WFSUser _user;

        public SaveWFSUserCommand(C.WFSUser user)
        {
			_user = user;
        }
        #region ICommand<Vendor> Members

		bool NewMembershipUser(SaveWFSUserResponse response)
		{
			S.MembershipCreateStatus status;

			var membership = System.Web.Security.Membership.CreateUser(
				_user.EmailAddress
				, _user.Password
				, _user.EmailAddress, null, null, true, out status);

			if (status != S.MembershipCreateStatus.Success)
			{
				response.Status = Status.Error;

				response.Messages.Add(new Message("CREATEUSERERROR", status.TranslateMessage()));

				return false;
			}

			_user.MembershipGuid = (Guid)membership.ProviderUserKey;
			
			return true;
		}

		bool UpdateMembershipUser(SaveWFSUserResponse response, WFS.DataContext.WFSEntities context)
		{
			var wfsUser = context.WFSUsers.FirstOrDefault(x => x.UserId.Equals(_user.UserId));

			int matchingUsers = context.Users.Count(x => !x.UserId.Equals(_user.MembershipGuid) && x.UserName.Equals(_user.EmailAddress));

			// TODO REFACTOR INTO ITS OWN VALIDATION COMMAND
			if (matchingUsers > 0)
			{
				response.Status = Status.Error;

				response.Messages.Add(new Message ("UNIQUEVALIDATION", "Email address is not unique"));

				return false;
			}

			var mUser = context.Users.FirstOrDefault(x => x.UserId.Equals(wfsUser.MembershipGuid));

			mUser.UserName = _user.EmailAddress;

			context.SaveChanges();

			return true;
		}

        public IResult<C.WFSUser> Execute(System.Data.Entity.DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;

			var result = new SaveWFSUserResponse();

			result.Value = _user;

            try
            {
				result.Status = Status.Success;

				if (_user.UserId <= 0)
				{
					if (NewMembershipUser(result))
					{
						context.WFSUsers.Add(_user.ToDataModel());

						context.SaveChanges();
					}
					else
					{
						return result;
					}
				}
				else
				{
					if (UpdateMembershipUser(result, context))
					{
						var wfsUser = context.WFSUsers.FirstOrDefault(x => x.UserId.Equals(_user.UserId));

						_user.ToDataModel().ForUpdate(wfsUser);

						context.SaveChanges();
					}
					else 
					{
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				result.Status = Status.Error;

				result.Messages.Add(new Message ("ERROR", ex.ToString()));
			}

            return result;
        }

        #endregion

    }
}
