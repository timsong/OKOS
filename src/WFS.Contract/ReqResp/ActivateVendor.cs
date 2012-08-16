using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class ActivateVendorRequest
    {
        public int VendorId { get; set; }

        public bool IsActive { get; set; }
    }

    public class ActivateVendorResponse : Result<bool>
    {

    }
}
