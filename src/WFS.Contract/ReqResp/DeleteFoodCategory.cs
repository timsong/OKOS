using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
	public class DeleteFoodCategoryRequest
	{
        public int FoodCategoryId { get; set; }
	}

	public class DeleteFoodCategoryResponse : Result<bool>
    {

    }
}
