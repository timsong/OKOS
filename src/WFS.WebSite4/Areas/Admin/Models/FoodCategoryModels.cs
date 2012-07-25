using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFS.Contract;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class FoodCategoryListViewModel
    {
        public FoodCategoryListViewModel()
        {
            Categories = new List<FoodCategory>();
        }

        public List<FoodCategory> Categories { get; set; }
    }

    public class FoodCategoryEditModel
    {

    }
}