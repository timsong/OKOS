using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    
    public class ActivateVendorFoodOptionRequest
    {
        public int VendorFoodOptionId { get; set; }

        public bool IsActive { get; set; }
    }

    public class ActivateVendorFoodOptionResponse : Result<bool>
    {

    }
}
