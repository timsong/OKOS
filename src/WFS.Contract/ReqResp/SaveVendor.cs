using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
	public class SaveVendorRequest
	{
		public Vendor Subject { get; set; }
	}

	public class SaveVendorResponse : Result<Vendor>
	{
		public Vendor Subject { get; set; }
	}
}
