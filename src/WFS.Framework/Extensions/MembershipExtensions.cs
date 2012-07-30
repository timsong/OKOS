using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace WFS.Framework.Extensions
{
	public static class MembershipExtensions
	{
		public static string TranslateMessage(this MembershipCreateStatus memStat)
		{
			string msg = string.Empty;

			switch (memStat)
			{
				case MembershipCreateStatus.DuplicateEmail:
					msg = "An account with this email already exists";
					break;
				case MembershipCreateStatus.DuplicateProviderUserKey:
					break;
				case MembershipCreateStatus.DuplicateUserName:
					msg = "An account with this email already exists";
					break;
				case MembershipCreateStatus.InvalidEmail:
					msg = "Your email is not a vaild email address";
					break;
				case MembershipCreateStatus.InvalidPassword:
					msg = "The password was not long enough or contained invalid characters";
					break;
				case MembershipCreateStatus.InvalidProviderUserKey:
					break;
				case MembershipCreateStatus.InvalidUserName:
					msg = "Your email is not a vaild email address";
					break;
				default:
					msg = "An error occurred please try again";
					break;
			}

			return msg;
		}
	}
}
