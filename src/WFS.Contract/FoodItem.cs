using System.Collections.Generic;
using WFS.Contract.Enums;

namespace WFS.Contract
{
    public class FoodItem
    {
        public FoodItem()
        {
            Options = new List<FoodOption>();
        }

        public int FoodItemId { get; set; }
        public int FoodCategoryId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public List<FoodOption> Options { get; set; }

        public string Category { get; set; }
        public FoodCategoryTypeEnum CategoryType { get; set; }
    }
}
