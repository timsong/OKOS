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
    public partial class OrderItemOption
    {
        public int OrderItemOptionId { get; set; }
        public int OrderItemId { get; set; }
        public int FoodOptionId { get; set; }
    
        public virtual OrderItem OrderItem { get; set; }
        public virtual VendorFoodOption VendorFoodOption { get; set; }
    }
    
}