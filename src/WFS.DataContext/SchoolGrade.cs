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
    public partial class SchoolGrade
    {
        public int SchoolGradeId { get; set; }
        public int SchoolId { get; set; }
        public int GradeId { get; set; }
    
        public virtual Grade Grade { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual School School { get; set; }
    }
    
}
