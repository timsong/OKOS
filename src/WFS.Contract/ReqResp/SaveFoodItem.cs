using WFS.Contract.Enums;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SaveFoodItemRequest
    {
		public FoodItem Subject { get; set; }
	}

	public class SaveFoodItemResponse : Result<FoodItem>
    {
        public FoodItem Subject { get; set; }
    }
}
