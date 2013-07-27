using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blue_Script.Models;

namespace Blue_Script.Controllers
{ 
    public class CharacterController : Controller
    {
        private ScriptContext db = new ScriptContext();

        //
        // GET: /Character/

        public ViewResult Index()
        {
            return View(db.Characters.ToList());
        }

        //
        // GET: /Character/Details/5

        public ViewResult Details(int id)
        {
            Character character = db.Characters.Find(id);
            return View(character);
        }

        //
        // GET: /Character/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Character/Create

        [HttpPost]
        public ActionResult Create(Character character)
        {
            if (ModelState.IsValid)
            {
                db.Characters.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(character);
        }
        
        //
        // GET: /Character/Edit/5
 
        public ActionResult Edit(int id)
        {
            Character character = db.Characters.Find(id);
            return View(character);
        }

        //
        // POST: /Character/Edit/5

        [HttpPost]
        public ActionResult Edit(Character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        //
        // GET: /Character/Delete/5
 
        public ActionResult Delete(int id)
        {
            Character character = db.Characters.Find(id);
            return View(character);
        }

        //
        // POST: /Character/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}