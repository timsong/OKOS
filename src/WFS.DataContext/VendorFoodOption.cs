//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace WFS.DataContext
{
    public partial class VendorFoodOption
    {
        public VendorFoodOption()
        {
            this.FoodItemOptions = new HashSet<FoodItemOption>();
            this.OrderItemOptions = new HashSet<OrderItemOption>();
        }
    
        public int VendorFoodOptionId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<FoodItemOption> FoodItemOptions { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<OrderItemOption> OrderItemOptions { get; set; }
    }
    
}
