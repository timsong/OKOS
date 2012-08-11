using System.Collections.Generic;
using WFS.Contract.Enums;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetFoodItemsByVendorIdRequest
    {
        public int VendorId { get; set; }
     
		public ActiveDataRequestEnum ActiveDataRequest { get; set; }
    }

    public class GetFoodItemsByVendorIdResponse : BaseResponse
    {
        public GetFoodItemsByVendorIdResponse()
        {
            FoodItems = new List<FoodItem>();
        }

        public List<FoodItem> FoodItems { get; set; }
    }
}

