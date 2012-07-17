using WFS.Framework;

namespace WFS.Contract.ReqResp
{
    public class CreateFoodItemRequest
    {
        public CreateFoodItemRequest()
        {
            IsActive = true;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int FoodCatId { get; set; }
        public bool IsActive { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateFoodItemResponse : BaseResponse
    {
        public FoodItem FoodItem { get; set; }
    }
}
