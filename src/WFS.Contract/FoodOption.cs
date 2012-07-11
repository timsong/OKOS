
namespace WFS.Contract
{
    public class FoodOption
    {
        public int FoodOptionId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}
