using WFS.Repository.Conversions;
using WFS.Contract.Enums;
using WFS.Contract;

namespace WFS.Repository.Commands
{
    public class CreateOrganizationCommand : ICommand<Organization>
    {
        private readonly PhoneAddress _addressInfo;
        private readonly string _name;
        private readonly int _userId;
        private readonly int? _parentId;
        private readonly OrganizationTypeEnum _type;

        public CreateOrganizationCommand(PhoneAddress addressInfo, string name, int userId, int? parentId, OrganizationTypeEnum type)
        {
            this._addressInfo = addressInfo;
            this._name = name;
            this._userId = userId;
            this._parentId = parentId;
            this._type = type;
        }
        #region ICommand<Vendor> Members

        public IResult<Organization> Execute(System.Data.Entity.DbContext dbContext)
        {
            var organization = new WFS.DataContext.Organization()
            {
                Address1 = this._addressInfo.Address1,
                Address2 = this._addressInfo.Address2,
                City = this._addressInfo.City,
                Name = this._name,
                State = this._addressInfo.State,
                PhoneNumber = this._addressInfo.PhoneNumber,
                PhoneExt = this._addressInfo.PhoneExt,
                ZipCode = this._addressInfo.ZipCode,
                IsActive = true,
                ParentOrgId = this._parentId,
                OrganizationType = this._type.ToString(),
                UserId = this._userId
            };

            dbContext.Set<WFS.DataContext.Organization>().Add(organization);
            dbContext.SaveChanges();

            var result = new Result<Organization>(Status.Success, organization.ToContract());
            return result;
        }

        #endregion

    }
}
