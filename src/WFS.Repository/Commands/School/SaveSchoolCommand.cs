﻿using System;
using System.Data.Entity;
using System.Linq;
using WFS.Contract.ReqResp;
using WFS.Repository.Conversions;
using C = WFS.Contract;

namespace WFS.Repository.Commands.School
{
    public class SaveSchoolCommand : ICommand<C.School>
    {
        private readonly C.School _School;

        public SaveSchoolCommand(C.School School)
        {
            _School = School;
        }

        public IResult<C.School> Execute(DbContext dbContext)
        {
            var context = (WFS.DataContext.WFSEntities)dbContext;
            var result = new SaveSchoolResponse();

            try
            {
                if (_School.OrganizationId > 0)
                {
                    var org = context.Organizations.FirstOrDefault(x => x.OrganizationId.Equals(_School.OrganizationId));

                    org.ForUpdate(_School);

                    dbContext.SaveChanges();

                    result.Value = (C.School)org.ToContract();
                }
                else
                {
                    var org = _School.ToDataModel();
                    org.School = new DataContext.School();

                    context.Organizations.Add(org);
                    dbContext.SaveChanges();

                    result.Value = (C.School)org.ToContract();
                }
            }
            catch (Exception ex)
            {
                result.Status = Status.Error;

                result.Messages.Add(new Message { Code = "DIE!", Text = ex.ToString() });
            }

            return result;
        }
    }
}
