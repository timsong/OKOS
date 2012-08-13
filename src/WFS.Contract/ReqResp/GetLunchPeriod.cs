using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFS.Repository;
using WFS.Contract.Enums;

namespace WFS.Contract.ReqResp
{
    public class GetLunchPeriodsRequest
    {
        public GetLunchPeriodsRequest()
        {
            DataRequest = ActiveDataRequestEnum.All;
        }
        public int SchoolId { get; set; }
        public ActiveDataRequestEnum DataRequest { get; set; }
    }

    public class GetLunchPeriodsResponse : ListResult<LunchPeriod>
    {

    }
}
