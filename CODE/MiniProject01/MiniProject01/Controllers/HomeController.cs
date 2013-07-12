using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniProject01.Models;
using System.Text.RegularExpressions;

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
            Regex regex = new Regex("" + info.name);
            MatchCollection matches = regex.Matches(info.input);
            info.number = matches.Count;
            return View("GetStats", info);
        }
    }
}
