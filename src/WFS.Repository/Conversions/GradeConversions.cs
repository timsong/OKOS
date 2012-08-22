using WFS.DataContext;
using C = WFS.Contract;

namespace WFS.Repository.Conversions
{
    public static class GradeConversions
    {
        public static C.Grade ToContract(this Grade data)
        {
            if (data == null)
                return null;

            var model = new C.Grade()
            {
                Name = data.Name,
                GradeId = data.GradeId,
                Order = data.Order
            };

            return model;
        }

    }

    public static class SchoolGradeConversions
    {
        public static C.SchoolGrade ToContract(this SchoolGrade data)
        {
            if (data == null)
                return null;

            var model = new C.SchoolGrade()
            {
                Name = data.Grade.Name,
                SchoolId = data.SchoolId,
                SchoolGradeId = data.SchoolGradeId,
                GradeId = data.GradeId,
            };

            return model;
        }

    }
}
