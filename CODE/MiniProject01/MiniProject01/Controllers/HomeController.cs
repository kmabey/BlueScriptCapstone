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

        [HttpGet]
        public ActionResult GetStats()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStats(InformationModel info)
        {
            //TODO: add regex stuff here.
            return View("GetStats", info);
        }
    }
}
