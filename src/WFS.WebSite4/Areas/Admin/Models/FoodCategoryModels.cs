using System.Collections.Generic;
using WFS.Contract;
using WFS.Framework;


namespace WFS.WebSite4.Areas.Admin.Models
{
    public class FoodCategoryListViewModel
    {
        public FoodCategoryListViewModel()
        {
            Categories = new List<FoodCategory>();
        }

        public List<FoodCategory> Categories { get; set; }

		public int VendorId { get; set; }
    }

	public class FoodCategoryEditModel : EditModelBase<FoodCategory>
    {
		public FoodCategoryEditModel()
		{
			Subject = new FoodCategory();
		}

		public FoodCategoryEditModel(FoodCategory category)
		{
			Subject = category;
		}

		public FoodCategory Subject { get; set; }
	}
}