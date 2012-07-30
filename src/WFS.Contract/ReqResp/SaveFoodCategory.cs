using WFS.Contract.Enums;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SaveFoodCategoryRequest
    {
		public FoodCategory Subject { get; set; }
	}

	public class SaveFoodCategoryResponse : Result<FoodCategory>
    {
        public FoodCategory Subject { get; set; }
    }
}
