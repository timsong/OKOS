using WFS.Contract.Enums;

namespace WFS.Contract
{
    public class FoodCategory
    {
        public int FoodCategoryId { get; set; }
        public string Name { get; set; }
        public int VendorId { get; set; }
        public FoodCategoryTypeEnum CategoryType { get; set; }
    }
}
