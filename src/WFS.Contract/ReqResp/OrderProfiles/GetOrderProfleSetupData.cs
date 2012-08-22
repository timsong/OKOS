using System.Collections.Generic;
using WFS.Repository;

namespace WFS.Contract.ReqResp
{
    public class GetOrderProfleSetupDataRequest
    {
        public int SchoolId { get; set; }
    }


    public class GetOrderProfleSetupDataResponse : ListResult<LunchPeriod>
    {
        public GetOrderProfleSetupDataResponse()
        {
            Teachers = new List<Teacher>();
            LunchPeriods = new List<LunchPeriod>();
            Grades = new List<SchoolGrade>();
        }

        public List<Teacher> Teachers { get; set; }
        public List<LunchPeriod> LunchPeriods { get; set; }
        public List<SchoolGrade> Grades { get; set; }
    }
}
