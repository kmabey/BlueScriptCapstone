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
			var query = db.Settings.Select(c => new { c.ID, c.Name });
			ViewBag.PossibleSettings = new SelectList(query.AsEnumerable(), "ID", "Name");
			var query2 = db.Characters.Select(c => new { c.CharacterID, c.FullName });
			ViewData["myCharacters"] = new SelectList(query2.AsEnumerable(), "CharacterID", "FullName");
            
			return View(db.Scenes);
        }

		public ActionResult CreateCharacter()
		{
			return PartialView();
		}

		public ActionResult EditCharacter()
		{
			return PartialView();
		}

		public ActionResult CreateSetting()
		{
			return PartialView(new Setting());
		}

		public ActionResult EditSetting()
		{
			return PartialView();
		}

		public ActionResult CreateScene()
		{
			PopulateSettingsDropDownList();
			return PartialView(new Scene());
		}

		public ActionResult ScenePartial(int id)
		{
			Scene scene = db.Scenes.Find(id);
			PopulateSettingsDropDownList(scene.Setting);
			return PartialView(scene);
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

        [HttpPost]
        public ActionResult Setting(Setting setting)
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
                return View("MyBlueScript", db.Scenes);
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

		private void PopulateSettingsDropDownList(object selectedSetting = null)
		{
			var q = from s in db.Settings
								   orderby s.Name
								   select s;
			ViewBag.PossibleSettings = new SelectList(q.AsEnumerable(), "ID", "Name", selectedSetting);
		}

		private void PopulateAssignedCharacters(Scene scene)
		{
			var allCharacters = db.Characters;
			var instructorCourses = new HashSet<int>(scene.Characters.Select(c => c.CharacterID));
			var viewModel = new List<Character>();
			foreach (var cha in allCharacters)
			{
				viewModel.Add(new Character
				{
					CharacterID = cha.CharacterID,
					FullName = cha.FullName
				});
			}
			ViewBag.Characters = viewModel;
		}
    }
}
