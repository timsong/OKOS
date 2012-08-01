using C = WFS.Contract;

namespace WFS.Repository.Conversions.ExtendOrganization
{
    public interface IExtendedOrganization
    {
        C.Organization Extend(WFS.DataContext.Organization data);
    }

    public class OrganizationExtensionFactory
    {
        public static IExtendedOrganization GetExtensionClass(C.Enums.OrganizationTypeEnum type)
        {
            switch (type)
            {
                case WFS.Contract.Enums.OrganizationTypeEnum.Vendor:
                    return new ExtendVendor();
                case WFS.Contract.Enums.OrganizationTypeEnum.School:
                    return new ExtendSchool();
                default:
                    break;
            }

            return null;
        }
    }
}
