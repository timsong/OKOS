using System.Collections.Generic;
using WFS.Contract.Enums;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class SetFoodOptionForFoodItemRequest
    {
        public int FoodItemId { get; set; }

		public int FoodOptionId { get; set; }

		public bool Selected { get; set; }
	}

    public class SetFoodOptionForFoodItemResponse : Result<bool>
    {

    }
}

