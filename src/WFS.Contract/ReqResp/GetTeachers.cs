using WFS.Contract.Enums;
using WFS.Repository;

namespace WFS.Contract
{
    public class GetTeachersRequest
    {
        public GetTeachersRequest()
        {
            DataRequest = ActiveDataRequestEnum.All;
        }
        public int SchoolId { get; set; }
        public ActiveDataRequestEnum DataRequest { get; set; }
    }

    public class GetTeachersResponse : ListResult<Teacher>
    {
    }
}
