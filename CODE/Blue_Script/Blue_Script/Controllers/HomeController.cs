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
        BlueScriptEntities db;

        public HomeController()
        {
            db = new BlueScriptEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyBlueScript()
        {
            ViewData.Model = db.Events.ToList();
            return View();
        }
    }
}
