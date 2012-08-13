
using System;
namespace WFS.Contract
{
    public class LunchPeriod
    {
        public int LunchPeriodId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SchoolID { get; set; }

    }
}
