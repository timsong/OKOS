
using WFS.Contract.Enums;
using WFS.DataContext;
using C = WFS.Contract;
using System;

namespace WFS.Repository.Conversions
{
    public static class OrderProfileConversions
    {
        public static C.OrderProfile ToContract(this UserOrderProfile data)
        {
            if (data == null)
                return null;

            var model = new C.OrderProfile()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    GradeId = data.SchoolGradeId,
                    LunchPeriodId = data.LunchPeriodId,
                    TeacherId = data.TeacherId,
                    Title = data.Title,
                    UserId = data.UserId,
                    OrderProfileId = data.OrderProfileId,
                    SchoolId = data.SchoolId,
                    OrganizationName = data.School.Organization.Name,
                    OrganizationType = (OrganizationTypeEnum)Enum.Parse(typeof(OrganizationTypeEnum), data.School.Organization.OrganizationType)
                };


            return model;
        }

        public static UserOrderProfile ToDataModel(this C.OrderProfile model)
        {
            if (model == null) return null;

            var data = new UserOrderProfile()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                LunchPeriodId = model.LunchPeriodId,
                SchoolGradeId = model.GradeId,
                TeacherId = model.TeacherId,
                Title = model.Title,
                UserId = model.UserId,
                IsDeleted = false,
                SchoolId = model.SchoolId
            };

            return data;
        }


        public static void ForUpdate(this UserOrderProfile existing, C.OrderProfile model)
        {
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.LunchPeriodId = model.LunchPeriodId;
            existing.SchoolGradeId = model.GradeId;
            existing.TeacherId = model.TeacherId;
            existing.Title = model.Title;
        }
    }

}
