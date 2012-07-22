using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C = WFS.Contract;
using WFS.Repository.Conversions;

namespace WFS.Repository.Commands
{
    public class ChangeOrganizationActivateStatusCommand: ICommand<C.Organization>
    {
        private readonly  int _orgId;
        private readonly bool _isActive;

        public ChangeOrganizationActivateStatusCommand(int organizationId, bool isActive)
        {
            _orgId = organizationId;
            _isActive = isActive;
        }
        #region ICommand<Vendor> Members

        public IResult<C.Organization> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;
            var data = (from x in ent.Organizations
                        where x.OrganizationId == _orgId
                        select x).FirstOrDefault();

            data.IsActive = _isActive;
            dbContext.SaveChanges();

            var result = new Result<C.Organization>(Status.Success, data.ToContract());
            return result;
        }

        #endregion

    }
}
