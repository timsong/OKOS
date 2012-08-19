﻿using System.Web.Mvc;
using WFS.WebSite4.Controllers;
using WFS.Contract.Enums;

namespace WFS.WebSite4.Areas.Customer.Controllers
{
    [RoleAuthorize(WFSRoleEnum.Customer, WFSRoleEnum.Admin, WFSRoleEnum.AccountManager, WFSRoleEnum.SystemAdmin)]
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            var i = AuthenticatedUserId;
            return View();
        }

    }
}
