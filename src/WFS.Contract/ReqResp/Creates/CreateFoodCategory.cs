using WFS.Contract.Enums;
using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class CreateFoodCategoryRequest
    {
        public string Name { get; set; }
        public int VendorID { get; set; }
        public FoodCategoryTypeEnum CategoryType { get; set; }
    }

    public class CreateFoodCategoryResponse : BaseResponse
    {
        public FoodCategory FoodCategory { get; set; }
    }
}
