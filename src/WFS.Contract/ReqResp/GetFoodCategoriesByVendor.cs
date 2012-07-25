using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Framework;
using WFS.Contract.Enums;

namespace WFS.Contract.ReqResp
{
    public class GetFoodCategoriesByVendorRequest
    {
        public int VendorId { get; set; }
        public ActiveDataRequestEnum ActiveDataRequest { get; set; }
    }

    public class GetFoodCategoriesByVendorResponse : BaseResponse
    {
        public GetFoodCategoriesByVendorResponse()
        {
            FoodCategories = new List<FoodCategory>();
        }

        public List<FoodCategory> FoodCategories { get; set; }
    }
}
