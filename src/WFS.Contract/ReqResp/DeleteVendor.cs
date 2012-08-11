using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
	public class DeleteVendorRequest
	{
        public int VendorId { get; set; }
	}

    public class DeleteVendorResponse : BaseResponse
    {

    }
}
