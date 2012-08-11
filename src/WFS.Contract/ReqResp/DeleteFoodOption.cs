using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
	public class DeleteFoodOptionRequest
	{
        public int FoodOptionId { get; set; }
	}

	public class DeleteFoodOptionResponse : Result<bool>
    {

    }
}
