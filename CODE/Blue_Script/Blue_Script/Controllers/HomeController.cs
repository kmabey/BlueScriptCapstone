using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blue_Script.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Web.SessionState;
using System.Collections.Specialized;

namespace Blue_Script.Controllers
{
	public class HomeController : Controller
	{
		BlueScriptEntities db = new BlueScriptEntities();
		public int projectNum;
		//public HttpSessionState Session { get; set; }

		public HomeController()
		{
		}

		public ActionResult Index()
		{
			if (HttpContext.Session["Number"] == null)
			{
				Session["Number"] = 1;
			}
			projectNum = (int)(HttpContext.Session["Number"]);

			Project project = db.Projects.Find(projectNum);
			ViewBag.projectName = project.Name;
			var chapters = db.Chapters.Where(x => x.ProjectID == projectNum);
			PopulateChaptersDropDownList(projectNum);
			return View(db.Chapters.Where(x => x.ProjectID == projectNum));
		}

		[HttpPost]
		public string Link()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			NameValueCollection q = System.Web.HttpUtility.ParseQueryString(string.Empty);
			var chapters = db.Chapters.Where(x => x.ProjectID == projectNum);
			q["id"] = "" + projectNum;
			foreach(Chapter c in chapters)
			{
				q["" + c.Name] = "" + c.Body;
			}
			string theuri = (Request.Url.AbsoluteUri).Replace("Link/", "LinkTo/?");
			return (theuri)+(q.ToString());
		}
		[HttpGet]
		public ActionResult LinkTo(string s)
		{
			string result = "";
			NameValueCollection n = Request.QueryString;
			projectNum = Convert.ToInt32(Request.QueryString["id"]);
			foreach (String key in Request.QueryString.AllKeys)
			{
				if(!key.Equals("id"))
				Response.Write(key + ": \n" + Request.QueryString[key] + "\n");
			}
			return Content(result);
		}

		public ActionResult ChangeProject(int id)
		{
			HttpContext.Session["Number"] = id;
			projectNum = id;
			Project project = db.Projects.Find(projectNum);
			ViewBag.projectName = project.Name;
			var chapters = db.Chapters.Where(x => x.ProjectID == projectNum);
			PopulateChaptersDropDownList(projectNum);
			checkElements(id);
			return View("Index", db.Chapters.Where(x => x.ProjectID == projectNum));
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
			projectNum = (int)(HttpContext.Session["Number"]);
			ViewBag.Scenes = new List<Scene>(db.Scenes.Where(x => x.ProjectID == projectNum));
			ViewBag.Characters = new List<Character>(db.Characters.Where(x => x.ProjectID == projectNum));
			ViewBag.Settings = new List<Setting>(db.Settings.Where(x => x.ProjectID == projectNum));
			ViewBag.TotalScenes = ViewBag.Scenes.Count;
			ViewBag.TotalCharacters = ViewBag.Characters.Count;
			ViewBag.TotalSettings = ViewBag.Settings.Count;
			return PartialView();
		}

		public ActionResult MyBlueScript()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			return View(db.Scenes.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult Scenes()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
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

			return PartialView(db.Scenes.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult Characters()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var myChars = new List<Character>();
			foreach (var item in db.Characters.Where(x => x.ProjectID == projectNum))
			{
				myChars.Add(item);
			}
			ViewBag.myCharacters = myChars;
			return PartialView(db.Characters.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult Settings()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			ViewBag.Settings = new List<Setting>(db.Settings.Where(x => x.ProjectID == projectNum));
			return PartialView(ViewBag.Settings);
		}

		public ActionResult Chapters()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			return PartialView(db.Chapters.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult Unsorted()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			return PartialView(db.Unsorteds.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult AddToC(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Unsorted un = db.Unsorteds.Find(id);
			var newCharacter = new Character
			{
				ProjectID = projectNum,
				FullName = un.Name,
				Notes = "Added"
			};

			db.Characters.Add(newCharacter);
			db.Entry(un).State = EntityState.Deleted;
			db.SaveChanges();
			return PartialView("Unsorted", db.Unsorteds.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult AddToS(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Unsorted un = db.Unsorteds.Find(id);
			var newSetting = new Setting
			{
				ProjectID = projectNum,
				Name = un.Name,
				Notes = "Added"
			};

			db.Settings.Add(newSetting);
			db.Entry(un).State = EntityState.Deleted;
			db.SaveChanges();
			return PartialView("Unsorted", db.Unsorteds.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult AddToI(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Unsorted un = db.Unsorteds.Find(id);
			var newForget = new Forget
			{
				ProjectID = projectNum,
				Name = un.Name
			};

			db.Forgets.Add(newForget);
			db.Entry(un).State = EntityState.Deleted;
			db.SaveChanges();
			return PartialView("Unsorted", db.Unsorteds.Where(x => x.ProjectID == projectNum));
		}

		public ActionResult EditChapter(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Chapter chapter = db.Chapters.Find(id);
			return PartialView("EditChapter", chapter);
		}

		public ActionResult EditCharacter(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Character character = db.Characters.Find(id);
			return PartialView("EditCharacter", character);
		}

		public ActionResult EditSetting(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Setting setting = db.Settings.Find(id);
			return PartialView("EditSetting", setting);
		}

		public ActionResult ScenePartial(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Scene scene = db.Scenes.Find(id);
			PopulateAssignedCharacters(scene);
			return PartialView(scene);
		}

		[HttpPost]
		public ActionResult EditChapter(int id, FormCollection formCollection)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
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
			projectNum = (int)(HttpContext.Session["Number"]);
			var sceneToUpdate = db.Scenes.Find(id);
			if (TryUpdateModel(sceneToUpdate, "",
			   new string[] { "Name", "Notes", "SettingID" }))
			{
				try
				{

					UpdateSceneCharacters(selectedCharacters, sceneToUpdate);

					db.Entry(sceneToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return PartialView("Scenes", db.Scenes.Where(x => x.ProjectID == projectNum));

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
			projectNum = (int)(HttpContext.Session["Number"]);
			var characterToUpdate = db.Characters.Find(id);
			if (TryUpdateModel(characterToUpdate, "",
			   new string[] { "FullName", "Notes" }))
			{
				try
				{
					db.Entry(characterToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return PartialView("Characters", db.Characters.Where(x => x.ProjectID == projectNum));

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
			projectNum = (int)(HttpContext.Session["Number"]);
			var settingToUpdate = db.Settings.Find(id);
			if (TryUpdateModel(settingToUpdate, "",
			   new string[] { "Name", "Notes" }))
			{
				try
				{
					db.Entry(settingToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return PartialView("Settings", db.Settings.Where(x => x.ProjectID == projectNum));
				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View("MyBlueScript", settingToUpdate);
		}

		[HttpPost]
		public ActionResult EditProject(int id, FormCollection formCollection)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var projectToUpdate = db.Projects.Find(id);
			if (TryUpdateModel(projectToUpdate, "",
			   new string[] { "Name" }))
			{
				try
				{
					db.Entry(projectToUpdate).State = EntityState.Modified;
					db.SaveChanges();
					return Json(new { ID = id });
				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View("MyBlueScript", projectToUpdate);
		}

		public ActionResult DeleteChapter(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Chapter chapter = new Chapter() { ID = id };
			if (chapter != null)
			{
				db.Entry(chapter).State = EntityState.Deleted;
				db.SaveChanges();
			}
			return RedirectToAction("Index", db.Chapters.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult DeleteCharacter(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Character character = new Character() { CharacterID = id };
			if (character != null)
			{
				db.Entry(character).State = EntityState.Deleted;
				db.SaveChanges();
			}
			return PartialView("Characters", db.Characters.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult DeleteSetting(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Setting setting = new Setting() { ID = id };
			if (setting != null)
			{
				db.Entry(setting).State = EntityState.Deleted;
				db.SaveChanges();
			}
			return PartialView("Settings", db.Settings.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult DeleteScene(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Scene scene = new Scene() { ID = id };
			db.Entry(scene).State = System.Data.EntityState.Deleted;
			db.SaveChanges();
			return PartialView("Scenes", db.Scenes.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult DeleteProject(int id)
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			Project project = new Project() { ID = id };
			db.Entry(project).State = System.Data.EntityState.Deleted;
			db.SaveChanges();
			return PartialView("Projects", db.Projects);
		}

		public ActionResult CreateCharacter()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var newCharacter = new Character
			{
				ProjectID = projectNum,
				FullName = "Character Name",
				Notes = "Notes go here."
			};
			db.Characters.Add(newCharacter);
			db.SaveChanges();
			return PartialView("Characters", db.Characters.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult CreateSetting()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var newSetting = new Setting
			{
				ProjectID = projectNum,
				Name = "Setting Name",
				Notes = "Notes go here."
			};
			db.Settings.Add(newSetting);
			db.SaveChanges();
			return PartialView("Settings", db.Settings.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult CreateScene()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var newScene = new Scene
			{
				ProjectID = projectNum,
				Name = "Scene Name",
				Characters = new List<Character>(),
				Notes = "Notes go here."
			};
			PopulateSettingsDropDownList();
			PopulateAssignedCharacters(newScene);
			db.Scenes.Add(newScene);
			db.SaveChanges();
			return PartialView("Scenes", db.Scenes.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult CreateChapter()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var newChapter = new Chapter
			{
				ProjectID = projectNum,
				Name = "New Chapter",
				Body = "Chapter goes here."
			};
			db.Chapters.Add(newChapter);
			db.SaveChanges();
			return RedirectToAction("Index", db.Chapters.Where(s => s.ProjectID == projectNum));
		}

		public ActionResult CreateProject()
		{
			projectNum = (int)(HttpContext.Session["Number"]);
			var newProject = new Project 
			{
 				Name = "New Project"
			};
			db.Projects.Add(newProject);
			db.SaveChanges();
			return PartialView("Projects", db.Projects);
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
			var allCharacters = db.Characters.Where(x => x.ProjectID == projectNum);
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
				foreach (Unsorted un in db.Unsorteds.Where(x => x.ProjectID == projectNum))
				{
					if (un.Name.Equals(s))
					{
						alreadyExists = true;
					}
				}
				foreach (Forget forget in db.Forgets.Where(x => x.ProjectID == projectNum))
				{
					if (forget.Name.Equals(s))
					{
						alreadyExists = true;
					}
				}
				if(!alreadyExists)
				{
					//add to unsorted
					var newUnsorted = new Unsorted
					{
						ProjectID = projectNum,
						Name = s
					};
					db.Unsorteds.Add(newUnsorted);
					db.SaveChanges();
				}
				Trace.WriteLine("Found: " + s);
			}

		}
	
		private void checkElements(int id)
		{
			var chaptersCount = (db.Chapters.Where(x => x.ProjectID == id)).Count();
			var scenesCount = (db.Scenes.Where(x => x.ProjectID == id)).Count();
			var charactersCount = (db.Characters.Where(x => x.ProjectID == id)).Count();
			var settingsCount = (db.Settings.Where(x => x.ProjectID == id)).Count();
			if(chaptersCount < 1)
			{
				var newChapter = new Chapter
				{
					ProjectID = id,
					Name = "Chapter One",
					Body = "auto generated chapter"
				};
				db.Chapters.Add(newChapter);
				db.SaveChanges();
			}
			if (scenesCount < 1)
			{
				var newScene = new Scene
				{
					ProjectID = id,
					Name = "Scene Name",
					Characters = new List<Character>(),
					Notes = "Notes go here."
				};
				PopulateSettingsDropDownList();
				PopulateAssignedCharacters(newScene);
				db.Scenes.Add(newScene);
				db.SaveChanges();
			}
			if(charactersCount < 1)
			{
				var newCharacter = new Character
				{
					ProjectID = id,
					FullName = "Character Name",
					Notes = "Notes go here."
				};
				db.Characters.Add(newCharacter);
				db.SaveChanges();
			}
			if(settingsCount < 1)
			{
				var newSetting = new Setting
				{
					ProjectID = id,
					Name = "Setting Name",
					Notes = "Notes go here."
				};
				db.Settings.Add(newSetting);
				db.SaveChanges();
			}
		}
	}
}
