
using WFS.Contract.Enums;
namespace WFS.Contract
{
    public class OrderProfile
    {
        public int OrderProfileId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }
        public int? TeacherId { get; set; }
        public int? GradeId { get; set; }
        public int? LunchPeriodId { get; set; }
        public int SchoolId { get; set; }

        public string TeacherName { get; set; }
        public string GradeName { get; set; }
        public string LunchPeriodName { get; set; }
        public string OrganizationName { get; set; }
        public OrganizationTypeEnum OrganizationType { get; set; }

    }
}
