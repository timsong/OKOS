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
    public partial class SchoolLunchPeriod
    {
        public int SchoolLunchPeriodId { get; set; }
        public int SchoolId { get; set; }
        public int LunchPeriodId { get; set; }
    
        public virtual LunchPeriod LunchPeriod { get; set; }
        public virtual Organization Organization { get; set; }
    }
    
}
