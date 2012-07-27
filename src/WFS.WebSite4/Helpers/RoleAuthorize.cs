using System.Web.Mvc;
using WFS.Contract.Enums;

namespace WFS.WebSite4
{
    public class RoleAuthorize : AuthorizeAttribute
    {
        public RoleAuthorize(params WFSRoleEnum[] DomainRoles)
        {
            foreach (var domainRole in DomainRoles)
            {
                var role = domainRole.ToString().Replace("_", " ");
                Roles += ", " + role;
            }
            Roles = Roles.Substring(2);
        }
    }
}