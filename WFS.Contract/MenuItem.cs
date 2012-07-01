
namespace WFS.Contract
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public int FoodItemId { get; set; }
        public int MenuId { get; set; }

        public bool IsActive { get; set; }
    }
}
