using System.Collections.Generic;
using WFS.Contract;
using WFS.Framework;


namespace WFS.WebSite4.Areas.Admin.Models
{
    public class FoodOptionsListViewModel
    {
        public FoodOptionsListViewModel()
        {
            Options = new List<FoodOption>();
        }

		public List<FoodOption> Options { get; set; }

		public int VendorId { get; set; }
    }

	public class FoodOptionEditModel : EditModelBase<FoodOption>
    {
		public FoodOptionEditModel()
		{
			Subject = new FoodOption();
		}

		public FoodOptionEditModel(FoodOption itemOption)
		{
			Subject = itemOption;
		}

		public FoodOption Subject { get; set; }
	}
}