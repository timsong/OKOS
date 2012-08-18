
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WFS.Contract;
using WFS.Framework;

namespace WFS.WebSite4.Areas.Admin.Models
{
    public class UserSearchModel
    {
        public UserSearchModel()
        {
            Roles = new List<SelectListItem>();

            Roles.Add(new SelectListItem() { Value = "All", Text = "All", Selected = true });
            Roles.Add(new SelectListItem() { Value = "Admin", Text = "Admin", Selected = true });
            Roles.Add(new SelectListItem() { Value = "AccountManager", Text = "Account Manager", Selected = true });
            Roles.Add(new SelectListItem() { Value = "VendorAdmin", Text = "Vendor Admin", Selected = true });
            Roles.Add(new SelectListItem() { Value = "StoreAdmin", Text = "Store Admin", Selected = true });
            Roles.Add(new SelectListItem() { Value = "SchoolAdmin", Text = "School Admin", Selected = true });
            Roles.Add(new SelectListItem() { Value = "Customer", Text = "Customer", Selected = true });

        }

        public string SearchProperty { get; set; }
        public string SelectedRole { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }

    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            Results = new List<SearchResult>();
        }

        public List<SearchResult> Results { get; set; }
    }

    public class SearchResult
    {
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string AccountType { get; set; }

        public int UserId { get; set; }
        public Guid MembershipId { get; set; }
    }

    public class UserEditModel : EditModelBase<WFSUser>
    {
        public WFSUser UserInfo { get; set; }
    }
}