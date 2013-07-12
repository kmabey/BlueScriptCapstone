using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniProject01.Models;

namespace MiniProject01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult GetStats()
        {
            return View();
        }
    }
}
