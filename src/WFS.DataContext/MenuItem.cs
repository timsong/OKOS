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
    public partial class MenuItem
    {
        public int MenuItemId { get; set; }
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual FoodItem FoodItem { get; set; }
    }
    
}
