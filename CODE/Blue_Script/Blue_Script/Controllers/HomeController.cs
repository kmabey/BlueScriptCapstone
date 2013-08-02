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
        public ActionResult MyBlueScript(Scene scene)
        {
            if(ModelState.IsValid)
            {
                db.Entry(scene).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = scene.Name + " has been saved";
                return RedirectToAction("MyBlueScript");
            }
            else
            {
                return View(db.Scenes);
            }
        }

        public ActionResult SettingPartial(int? sID)
        {
            Setting setting = db.Settings.FirstOrDefault(s => s.LocationID == sID);
            return PartialView("SettingPartial", setting);
        }

        [HttpPost]
        public ActionResult SettingPartial(Setting setting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setting).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = setting.Name + " has been saved";
                return RedirectToAction("MyBlueScript");
            }
            else
            {
                return View(db.Scenes);
            }
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
