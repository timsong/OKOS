﻿using System;
using System.Web.Mvc;
using WFS.WebSite4.Controllers;

namespace WFS.WebSite4.Areas.Customer.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index(Guid membershipId)
        {
            var i = AuthenticatedUserId;
            return View();
        }

    }
}
