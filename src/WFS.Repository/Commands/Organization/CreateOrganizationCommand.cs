using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C = WFS.Contract;
using WFS.Repository.Conversions;

namespace WFS.Repository.Commands.Organization
{
    public class CreateOrganizationCommand : ICommand<C.Organization>
    {
        private readonly C.PhoneAddress _addInfo;
        private readonly string _name;
        private readonly int _userId;
        private readonly int? _parentVendorId;
        private readonly C.Enums.OrganizationTypeEnum _type;

        public CreateOrganizationCommand(C.PhoneAddress addInfo, string name, int userId, int? parentVendorId, C.Enums.OrganizationTypeEnum type)
        {
            _addInfo = addInfo;
            _name = name;
            _userId = userId;
            _parentVendorId = parentVendorId;
            _type = type;
        }
        #region ICommand<Vendor> Members

        public IResult<C.Organization> Execute(System.Data.Entity.DbContext dbContext)
        {
            var ent = (WFS.DataContext.WFSEntities)dbContext;

            var vend = new WFS.DataContext.Organization()
            {
                Address1 = _addInfo.Address1,
                Address2 = _addInfo.Address2,
                City = _addInfo.City,
                Name = _name,
                State = _addInfo.State,
                PhoneNumber = _addInfo.PhoneNumber,
                PhoneExt = _addInfo.PhoneExt,
                ZipCode = _addInfo.ZipCode,
                IsActive = true,
                ParentOrgId = _parentVendorId,
                OrganizationType = _type.ToString(),
                UserId = _userId
            };

            var result = new Result<C.Organization>(Status.Success, vend.ToContract());
            return result;
        }

        #endregion

    }
}
