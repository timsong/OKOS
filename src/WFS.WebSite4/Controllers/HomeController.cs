using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFS.WebSite4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Why()
        {
            return View();
        }

        public ActionResult HealthAndNutrition()
        {
            return View();
        }

        public ActionResult Testimonials()
        {
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }
    }
}
