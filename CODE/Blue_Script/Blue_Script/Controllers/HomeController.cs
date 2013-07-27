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
        private ScriptContext repo = new ScriptContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyBlueScript()
        {
            return View(repo.Events);
        }
    }
}
