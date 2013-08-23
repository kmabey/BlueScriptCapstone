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
		public ActionResult Chapters()
		{
			return View();
		}

		public ActionResult AddEditCharacter(int id)
		{
			Character character = db.Characters.Find(id);
			return PartialView("EditCharacter", character);
		}

		public ActionResult AddEditSetting(int id)
		{
			Setting setting = db.Settings.Find(id);
			return PartialView("EditSetting", setting);
		}

		public ActionResult ScenePartial(int id)
		{
			Scene scene = db.Scenes.Find(id);
			PopulateSettingsDropDownList(scene.SettingID);
			PopulateAssignedCharacters(scene);
			return PartialView(scene);
		}

		[HttpPost]
		public ActionResult EditScene(int id, FormCollection formCollection, string[] selectedCharacters)
		{
			var sceneToUpdate = db.Scenes.Find(id);
			if (TryUpdateModel(sceneToUpdate, "",
			   new string[] { "Name", "Notes", "SettingID" }))
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

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AddEditCharacter(int id, FormCollection formCollection)
		{
			var characterToUpdate = db.Characters.Find(id);
			
				try
				{
					UpdateModel(characterToUpdate, formCollection);
					db.Entry(characterToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return PartialView("EditCharacter", db.Characters.Find(id));

				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			return View("MyBlueScript", characterToUpdate);
		}

		[HttpPost]
		public ActionResult AddEditSetting(int id, FormCollection formCollection)
		{
			var settingToUpdate = db.Settings.Find(id);
			if (TryUpdateModel(settingToUpdate, "",
			   new string[] { "Name", "Notes"}))
			{
				try
				{
					db.Entry(settingToUpdate).State = EntityState.Modified;
					db.SaveChanges();

					return RedirectToAction("MyBlueScript");
				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View("MyBlueScript", settingToUpdate);
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

		public ActionResult CreateCharacter()
		{
			var newCharacter = new Character
			{
				FullName = "Character Name",
				Notes = "Notes go here."
			};
			db.Characters.Add(newCharacter);
			db.SaveChanges();
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult CreateSetting()
		{
			var newSetting = new Setting
			{
				Name = "Setting Name",
				Notes = "Notes go here."
			};
			db.Settings.Add(newSetting);
			db.SaveChanges();
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult CreateScene()
		{
			var newScene = new Scene {Name = "Scene Name", Characters = new List<Character>(),
			Setting = null, Notes = "Notes go here."};
			PopulateSettingsDropDownList();
			PopulateAssignedCharacters(newScene);
			db.Scenes.Add(newScene);
			db.SaveChanges();
			return RedirectToAction("MyBlueScript");
		}

		

		private void PopulateSettingsDropDownList(object selectedSetting = null)
		{
			var q = from s in db.Settings
								   orderby s.Name
								   select s;
			ViewBag.PossibleSettings = new SelectList(q.AsEnumerable(), "ID", "Name", db.Settings.Find(selectedSetting));
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
