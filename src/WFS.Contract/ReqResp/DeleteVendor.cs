using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
	public class DeleteVendorRequest
	{
        public int VendorId { get; set; }
	}

	public class DeleteVendorResponse : Result<bool>
    {

    }
}
