using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    
    public class ActivateFoodCategoryRequest
    {
        public int VendorId { get; set; }

        public int VendorFoodCategoryId { get; set; }

        public bool IsActive { get; set; }
    }

    public class ActivateFoodCategoryResponse : Result<bool>
    {

    }
}
