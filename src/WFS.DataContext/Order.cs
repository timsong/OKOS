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
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string TransactionId { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
    
        public virtual WFSUser WFSUser { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
    
}
