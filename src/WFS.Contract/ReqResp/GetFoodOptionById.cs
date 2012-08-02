using System.Collections.Generic;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetFoodOptionByIdRequest
    {
        public int FoodOptionId { get; set; }
    }

    public class GetFoodOptionByIdResponse : BaseResponse
    {
        public GetFoodOptionByIdResponse()
        {
            FoodOption = new FoodOption();
        }

        public FoodOption FoodOption { get; set; }
    }
}
