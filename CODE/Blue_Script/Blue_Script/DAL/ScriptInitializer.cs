using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Blue_Script.Models;

namespace Blue_Script.DAL
{
    public class ScriptInitializer : DropCreateDatabaseIfModelChanges<ScriptContext>
    {
        protected override void Seed(ScriptContext context)
        {
            var characters = new List<Character>
            {
                new Character { FullName = "Jane Doe",   Notes = "Young, sweet, kind-hearted", Gender = 'f' },
                new Character { FullName = "John Doe",   Notes = "ghost", Gender = 'm' },
                new Character { FullName = "Jack Doe",   Notes = "wise", Gender = 'm' },
                new Character { FullName = "The Killer",   Notes = "main villian", Gender = 'm' }
            };
            characters.ForEach(s => context.Characters.Add(s));
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location { Name = "Downtown",      Notes = "" },
                new Location { Name = "Doe Household",     Notes = "" }
            };
            locations.ForEach(s => context.Locations.Add(s));
            context.SaveChanges();

            var events = new List<Event>
            {
                new Event { Name = "Killer Strikes", Notes = "Chapter One of novel" },
                new Event { Name = "Friend Found", Notes = "Chapter Two of novel" },
                new Event { Name = "Find out who killer is", Notes = "Chapter Three of novel" },
                new Event { Name = "Killer incounter", Notes = "Chapter Four of novel" }
            };
            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();

            events[0].Characters.Add(characters[2]);
            events[0].Characters.Add(characters[3]);
            events[1].Characters.Add(characters[0]);
            events[1].Characters.Add(characters[1]);
            events[2].Characters.Add(characters[0]);
            events[2].Characters.Add(characters[1]);
            events[3].Characters.Add(characters[0]);
            events[3].Characters.Add(characters[3]);
           
            events[0].Locations.Add(locations[1]);
            events[1].Locations.Add(locations[0]);
            events[2].Locations.Add(locations[1]);
            events[3].Locations.Add(locations[0]);
            context.SaveChanges();
        }
    }
}