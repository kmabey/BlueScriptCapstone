using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blue_Script.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Blue_Script.Controllers
{
	public class HomeController : Controller
	{
		BlueScriptEntities db = new BlueScriptEntities();
		int projectNum;

		public HomeController()
		{
			projectNum = 1;
		}

		public ActionResult Index()
		{
			Project project = db.Projects.Find(projectNum);
			var chapters = db.Chapters.Where(x => x.ProjectID == projectNum);
			PopulateChaptersDropDownList(projectNum);
			return View(db.Chapters.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult Nav()
		{
			return PartialView();
		}

		public ActionResult Projects()
		{
			return PartialView(db.Projects);
		}

		public ActionResult MyStats()
		{
			ViewBag.Scenes = new List<Scene>(db.Scenes.Where(x => x.ProjectID == projectNum));
			ViewBag.Characters = new List<Character>(db.Characters.Where(x => x.ProjectID == projectNum));
			ViewBag.Settings = new List<Setting>(db.Settings.Where(x => x.ProjectID == projectNum));
			ViewBag.TotalScenes = ViewBag.Scenes.Count;
			ViewBag.TotalCharacters = ViewBag.Characters.Count;
			ViewBag.TotalSettings = ViewBag.Settings.Count;
			return View();
		}

		public ActionResult MyBlueScript()
		{
			return View(db.Scenes.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult Scenes()
		{
			var set = db.Settings.Where(x => x.ProjectID == projectNum);
			var query = set.Select(c => new { c.ID, c.Name });
			ViewBag.PossibleSettings = new SelectList(query.AsEnumerable(), "ID", "Name");
			var query2 = db.Characters.Select(c => new { c.CharacterID, c.FullName });
			var myChars = new List<Character>();
			foreach (var item in db.Characters.Where(x => x.ProjectID == projectNum))
			{
				myChars.Add(item);
			}
			ViewBag.myCharacters = myChars;
			ViewBag.Settings = new List<Setting>(db.Settings);

			return PartialView(db.Scenes);
		}

		public ActionResult Characters()
		{
			var myChars = new List<Character>();
			foreach (var item in db.Characters.Where(x => x.ProjectID == projectNum))
			{
				myChars.Add(item);
			}
			ViewBag.myCharacters = myChars;
			return PartialView(db.Characters);
		}

		public ActionResult Settings()
		{
			ViewBag.Settings = new List<Setting>(db.Settings.Where(x => x.ProjectID == projectNum));
			return PartialView(db.Settings);
		}

		public ActionResult Chapters()
		{
			return View();
		}

		public ActionResult EditChapter(int id)
		{
			Chapter chapter = db.Chapters.Find(id);
			return PartialView("EditChapter", chapter);
		}

		public ActionResult EditCharacter(int id)
		{
			Character character = db.Characters.Find(id);
			return PartialView("EditCharacter", character);
		}

		public ActionResult EditSetting(int id)
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
		public ActionResult EditChapter(int id, FormCollection formCollection)
		{
			var chapterToUpdate = db.Chapters.Find(id);
			if (TryUpdateModel(chapterToUpdate, "",
			   new string[] { "Name", "Body" }))
			{
				try
				{
					db.Entry(chapterToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					FindMatches(formCollection["Body"]);
					return Json(new { ID = id, Name = chapterToUpdate.Name, Body = chapterToUpdate.Body });

				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View("MyBlueScript", chapterToUpdate);
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
					return Json(new { ID = id });

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

		[HttpPost]
		public ActionResult EditCharacter(int id, FormCollection formCollection)
		{
			var characterToUpdate = db.Characters.Find(id);
			if (TryUpdateModel(characterToUpdate, "",
			   new string[] { "FullName", "Notes" }))
			{
				try
				{
					db.Entry(characterToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return Json(new { ID = id, FullName = characterToUpdate.FullName, Notes = characterToUpdate.Notes });

				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View("MyBlueScript", characterToUpdate);
		}

		[HttpPost]
		public ActionResult EditSetting(int id, FormCollection formCollection)
		{
			var settingToUpdate = db.Settings.Find(id);
			if (TryUpdateModel(settingToUpdate, "",
			   new string[] { "Name", "Notes" }))
			{
				try
				{
					db.Entry(settingToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return Json(new { ID = id });
				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View("MyBlueScript", settingToUpdate);
		}

		public ActionResult DeleteChapter(int id)
		{
			Chapter chapter = new Chapter() { ID = id };
			if (chapter != null)
			{
				db.Entry(chapter).State = EntityState.Deleted;
				db.SaveChanges();
			}
			return RedirectToAction("Index");
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
			Setting setting = new Setting() { ID = id };
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
			var newScene = new Scene
			{
				Name = "Scene Name",
				Characters = new List<Character>(),
				Setting = null,
				Notes = "Notes go here."
			};
			PopulateSettingsDropDownList();
			PopulateAssignedCharacters(newScene);
			db.Scenes.Add(newScene);
			db.SaveChanges();
			return RedirectToAction("MyBlueScript");
		}

		public ActionResult CreateChapter()
		{
			var newChapter = new Chapter
			{
				ProjectID = projectNum,
				Name = "New Chapter",
				Body = "Chapter goes here."
			};
			db.Chapters.Add(newChapter);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		private void PopulateChaptersDropDownList(object selectedchapter = null)
		{
			var q = from s in db.Chapters.Where(x => x.ProjectID == projectNum)
					orderby s.ID
					select s;
			ViewBag.AllChapters = new SelectList(q.AsEnumerable(), "ID", "Name", db.Chapters.Find(selectedchapter));
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
		

		private void FindMatches(string body)
		{
			MatchCollection matchCollection = Regex.Matches(body, @"[^.:?!\d\n\v\e\r\s""']\s([A-Z]+[a-z]+)[,.:;?!\s]");

			List<string> matches = matchCollection
			.Cast<Match>()
			.Select(m => m.Groups[1].Value)
			.Distinct()
			.ToList();

			foreach (String s in matches)
			{
				bool alreadyExists = false;
				foreach (Character c in db.Characters.Where(x => x.ProjectID == projectNum))
				{
					if(c.FullName.Equals(s))
					{
						alreadyExists = true;
					}
				}
				foreach (Setting set in db.Settings.Where(x => x.ProjectID == projectNum))
				{
					if (set.Name.Equals(s))
					{
						alreadyExists = true;
					}
				}
				if(!alreadyExists)
				{
					//create character or setting
				}
				Trace.WriteLine("Found: " + s);
			}

		}
	}
}
