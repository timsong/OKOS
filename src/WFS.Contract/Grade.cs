
namespace WFS.Contract
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }

    public class SchoolGrade
    {
        public int SchoolGradeId { get; set; }
        public int SchoolId { get; set; }
        public int GradeId { get; set; }
        public string Name { get; set; }
    }
}

