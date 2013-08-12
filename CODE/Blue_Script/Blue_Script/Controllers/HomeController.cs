﻿using System;
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

		public ActionResult CreateCharacter()
		{
			return PartialView();
		}

		public ActionResult EditCharacter(int id)
		{
			Character character = db.Characters.Find(id);
			if(Request.IsAjaxRequest())
			{
				return PartialView(character);
			}
			else
			{
				return View();
			}
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
