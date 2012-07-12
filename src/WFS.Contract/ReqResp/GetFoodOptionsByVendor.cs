using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetFoodOptionsByVendorRequest
    {
        public int VendorId { get; set; }
    }

    public class GetFoodOptionsByVendorResponse : BaseResponse
    {
        public GetFoodOptionsByVendorResponse()
        {
            FoodOptions = new List<FoodOption>();
        }

        public List<FoodOption> FoodOptions { get; set; }
    }
}
