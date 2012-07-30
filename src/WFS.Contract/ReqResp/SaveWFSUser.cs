using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
	public class SaveWFSUserRequest
	{
		public WFSUser Subject { get; set; }
	}

	public class SaveWFSUserResponse : Result<WFSUser>
	{
		public WFSUser Subject { get; set; }
	}
}
