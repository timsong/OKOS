using WFS.Contract.Enums;
using WFS.Repository;
namespace WFS.Contract
{
    public class GetGradesRequest
    {
        public GetGradesRequest()
        {
            DataRequest = ActiveDataRequestEnum.All;
        }
        public int SchoolId { get; set; }
        public ActiveDataRequestEnum DataRequest { get; set; }
    }

    public class GetGradesResponse : ListResult<Grade>
    {
    }
}
