using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetFoodItemByIdRequest
    {
        public int FoodItemId { get; set; }
    }

    public class GetFoodItemByIdResponse : BaseResponse
    {
        public GetFoodItemByIdResponse()
        {
            FoodItem = new FoodItem();
        }

        public FoodItem FoodItem { get; set; }
    }
}
