using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blue_Script.Models;

namespace Blue_Script.Controllers
{
    public class HomeController : Controller
    {
        BlueScriptEntities db = new BlueScriptEntities();

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyBlueScript()
        {
            ViewBag.Events = db.Events;
            return View();
        }
    }
}
