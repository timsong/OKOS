using WFS.Contract.Enums;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SaveFoodOptionRequest
    {
		public FoodOption Subject { get; set; }
	}

	public class SaveFoodOptionResponse : Result<FoodOption>
    {
        public FoodOption Subject { get; set; }
    }
}
