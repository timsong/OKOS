using WFS.Contract.Enums;
using WFS.Repository;
namespace WFS.Contract
{
    public class GetSchoolGradesRequest
    {
        public GetSchoolGradesRequest()
        {
        }
        public int SchoolId { get; set; }
    }

    public class GetSchoolGradesResponse : ListResult<SchoolGrade>
    {
    }
}
