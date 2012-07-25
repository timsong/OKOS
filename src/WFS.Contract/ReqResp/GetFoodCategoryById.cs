using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class GetFoodCategoryByIdRequest
    {
        public int FoodCategoryID { get; set; }
    }

    public class GetFoodCategoryByIdResponse : BaseResponse
    {
        public FoodCategory FoodCategory { get; set; }
    }
}
