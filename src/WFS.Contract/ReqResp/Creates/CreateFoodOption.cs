using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class CreateFoodOptionRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int VendorId { get; set; }
    }

    public class CreateFoodOptionResponse : BaseResponse
    {
        public FoodOption FoodOption { get; set; }
    }
}
