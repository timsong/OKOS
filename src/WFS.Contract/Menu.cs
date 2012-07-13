using System.Collections.Generic;

namespace WFS.Contract
{
    public class Menu
    {
        public Menu()
        {
            FoodItems = new List<FoodItem>();
        }

        public int MenuId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }


        public List<FoodItem> FoodItems { get; set; }
    }
}
