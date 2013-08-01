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
            return View(db.Scenes);
        }

        [HttpPost]
        public ActionResult Delete(Scene eve)
        {
            try
            {
                db.Entry(eve).State = System.Data.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("MyBlueScript");
            }
            catch
            {
                return View();
            }
        }
    }
}
