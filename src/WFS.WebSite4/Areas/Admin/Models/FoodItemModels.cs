using System.Collections.Generic;
using WFS.Contract;
using WFS.Framework;
using System.Web.Mvc;


namespace WFS.WebSite4.Areas.Admin.Models
{
    public class FoodItemsListViewModel
    {
        public FoodItemsListViewModel()
        {
            Items = new List<FoodItem>();
        }

		public List<FoodItem> Items { get; set; }

		public int VendorId { get; set; }

		public List<FoodOption> Options { get; set; }
	}

	public class FoodItemEditModel : EditModelBase<FoodItem>
    {
		public FoodItemEditModel()
		{
			Subject = new FoodItem();
		}

		public FoodItemEditModel(FoodItem Item)
		{
			Subject = Item;
		}

		public FoodItemEditModel(FoodItem Item, int vendorid)
		{
			Subject = Item;

			VendorId = vendorid;
		}

		public List<FoodCategory> Categories { get; set; }

		public SelectList CategoriesList
		{
			get
			{
				return new SelectList(Categories, "FoodCategoryId", "Name", Subject.FoodCategoryId);
			}
		}

		public FoodItem Subject { get; set; }

		public FoodItemsListViewModel Parent { get; set; }

		public FoodItemEditModel SetParent(FoodItemsListViewModel listModel)
		{
			Parent = listModel;

			return this;
		}

		public int VendorId { get; set; }
	}
}