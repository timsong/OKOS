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
    public partial class SchoolDaysOff
    {
        public int SchoolDaysOffId { get; set; }
        public int SchoolId { get; set; }
        public int DaysOffId { get; set; }
    
        public virtual DaysOff DaysOff { get; set; }
        public virtual Organization Organization { get; set; }
    }
    
}