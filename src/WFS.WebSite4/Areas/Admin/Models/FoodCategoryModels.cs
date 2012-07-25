using System.Collections.Generic;
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
        public int FoodCategoryId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string CategoryType { get; set; }
    }
}