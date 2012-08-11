using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
	public class DeleteFoodItemRequest
	{
        public int FoodItemId { get; set; }
	}

	public class DeleteFoodItemResponse : Result<bool>
    {

    }
}
