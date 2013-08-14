using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blue_Script.Models;
using System.Data;

namespace Blue_Script.Controllers
{
    public class HomeController : Controller
    {
        BSEntities db = new BSEntities();

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult MyStats()
		{
			ViewBag.Scenes = new List<Scene>(db.Scenes);
			ViewBag.Characters = new List<Character>(db.Characters);
			ViewBag.Settings = new List<Setting>(db.Settings);
			ViewBag.TotalScenes = ViewBag.Scenes.Count;
			ViewBag.TotalCharacters = ViewBag.Characters.Count;
			ViewBag.TotalSettings = ViewBag.Settings.Count;
			return View();
		}

        public ActionResult MyBlueScript()
        {
			var query = db.Settings.Select(c => new { c.ID, c.Name });
			ViewBag.PossibleSettings = new SelectList(query.AsEnumerable(), "ID", "Name");
			var query2 = db.Characters.Select(c => new { c.CharacterID, c.FullName });
			var myChars = new List<Character>();
			foreach(var item in db.Characters)
			{
				myChars.Add(item);
			}
			ViewBag.myCharacters = myChars;
			ViewBag.Settings = new List<Setting>(db.Settings);
            
			return View(db.Scenes);
        }

		[HttpGet]
		public ActionResult AddEditCharacter(int? id)
		{
			if (Request.IsAjaxRequest())
			{
				if (id != null)
				{
					ViewBag.IsUpdate = true;
					Character character = db.Characters.Find(id);
					return PartialView("EditCharacter", character);
				}
				ViewBag.IsUpdate = false;
				return PartialView("EditCharacter");
			}
			else
			{
				if (id != null)
				{
					ViewBag.IsUpdate = true;
					Character character = db.Characters.Find(id);
					return PartialView("EditCharacter", character);
				}
				ViewBag.IsUpdate = false;
				return View("EditCharacter");
			}
		}

		[HttpGet]
		public ActionResult AddEditSetting(int? id)
		{
			if (Request.IsAjaxRequest())
			{
				if (id != null)
				{
					ViewBag.IsUpdate = true;
					Setting setting = db.Settings.Where(m => m.ID == id).FirstOrDefault();
					return PartialView("EditSetting", setting);
				}
				ViewBag.IsUpdate = false;
				return PartialView("EditSetting");
			}
			else
			{
				if (id != null)
				{
					ViewBag.IsUpdate = true;
					Setting setting = db.Settings.Where(m => m.ID == id).FirstOrDefault();
					return PartialView("EditSetting", setting);
				}
				ViewBag.IsUpdate = false;
				return View("EditSetting");
			}
		}

		[HttpPost]
		public ActionResult AddEditCharacter(Character character, string cmd)
		{
			if (ModelState.IsValid)
			{
				if (cmd == "Save")
				{
					try
					{
						db.Characters.Add(character);
						db.SaveChanges();
						return RedirectToAction("MyBlueScript");
					}
					catch { }
				}
				else
				{
					try
					{
						Character chara = db.Characters.Where(m => m.CharacterID == character.CharacterID).FirstOrDefault();
						if (chara != null)
						{
							chara.FullName = character.FullName;
							chara.Notes = character.Notes;
							db.SaveChanges();
						}
						return RedirectToAction("MyBlueScript");
					}
					catch { }
				}
			}

			if (Request.IsAjaxRequest())
			{
				return PartialView("EditCharacter", character);
			}
			else
			{
				return View("EditCharacter", character);
			}
		}

		[HttpPost]
		public ActionResult AddEditSetting(Setting setting, string cmd)
		{
			if (ModelState.IsValid)
			{
				if (cmd == "Save")
				{
					try
					{
						db.Settings.Add(setting);
						db.SaveChanges();
						return RedirectToAction("MyBlueScript");
					}
					catch { }
				}
				else
				{
					try
					{
						Setting sett = db.Settings.Where(m => m.ID == setting.ID).FirstOrDefault();
						if (sett != null)
						{
							sett.Name = setting.Name;
							sett.Notes = setting.Notes;
							db.SaveChanges();
						}
						return RedirectToAction("MyBlueScript");
					}
					catch { }
				}
			}

			if (Request.IsAjaxRequest())
			{
				return PartialView("EditSetting", setting);
			}
			else
			{
				return View("EditSetting", setting);
			}
		}

		public ActionResult DeleteCharacter(int id)
		{
			Character character = new Character() { CharacterID = id };
			if (character != null)
			{
					db.Entry(character).State = EntityState.Deleted;
					db.SaveChanges();
			}
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult DeleteSetting(int id)
		{
			Setting setting = new Setting() {ID = id };
			if (setting != null)
			{
					db.Entry(setting).State = EntityState.Deleted;
					db.SaveChanges();
			}
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult DeleteScene(int id)
		{
			Scene scene = new Scene() { ID = id };
			db.Entry(scene).State = System.Data.EntityState.Deleted;
			db.SaveChanges();
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult CreateScene()
		{
			var newScene = new Scene {Name = "SceneName", Characters = new List<Character>(),
			Setting = null, Notes = "Notes go here."};
			PopulateSettingsDropDownList();
			PopulateAssignedCharacters(newScene);
			db.Scenes.Add(newScene);
			db.SaveChanges();
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult ScenePartial(int id)
		{
			Scene scene = db.Scenes.Find(id);
			PopulateSettingsDropDownList(scene.Setting);
			PopulateAssignedCharacters(scene);
			return PartialView(scene);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditScene(int id, FormCollection formCollection, string[] selectedCharacters)
		{
			var sceneToUpdate = db.Scenes.Find(id);
			if (TryUpdateModel(sceneToUpdate, "",
			   new string[] { "Name", "Notes" }))
			{
				try
				{

					UpdateSceneCharacters(selectedCharacters, sceneToUpdate);

					db.Entry(sceneToUpdate).State = EntityState.Modified;
					db.SaveChanges();

					return RedirectToAction("MyBlueScript");
				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			PopulateAssignedCharacters(sceneToUpdate);
			return View("MyBlueScript", sceneToUpdate);
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
			var sceneCharacters = new HashSet<int>(scene.Characters.Select(c => c.CharacterID));
			var viewModel = new List<AssignedCharacters>();
			foreach (var cha in allCharacters)
			{
				viewModel.Add(new AssignedCharacters
				{
					CharacterID = cha.CharacterID,
					FullName = cha.FullName,
					Assigned = sceneCharacters.Contains(cha.CharacterID)
				});
			}
			ViewBag.Characters = viewModel;
		}

		private void UpdateSceneCharacters(string[] selectedCharacters, Scene sceneToUpdate)
		{
			if (selectedCharacters == null)
			{
				sceneToUpdate.Characters = new List<Character>();
				return;
			}

			var selectedCharactersHS = new HashSet<string>(selectedCharacters);
			var sceneCharacters = new HashSet<int>
				(sceneToUpdate.Characters.Select(c => c.CharacterID));
			foreach (var character in db.Characters)
			{
				if (selectedCharactersHS.Contains(character.CharacterID.ToString()))
				{
					if (!sceneCharacters.Contains(character.CharacterID))
					{
						sceneToUpdate.Characters.Add(character);
					}
				}
				else
				{
					if (sceneCharacters.Contains(character.CharacterID))
					{
						sceneToUpdate.Characters.Remove(character);
					}
				}
			}
		}
    }
}
