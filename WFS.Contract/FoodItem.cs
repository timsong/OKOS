using System.Collections.Generic;

namespace WFS.Contract
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }
        public int Name { get; set; }
        public int FoodCategoryId { get; set; }
        public int Description { get; set; }
        public List<FoodOption> Options { get; set; }

        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}
