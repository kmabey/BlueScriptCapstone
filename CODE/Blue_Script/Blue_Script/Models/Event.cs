using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blue_Script.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Location> Locations { get; set; }

        public Event()
        {
            this.Characters = new List<Character>();
            this.Locations = new List<Location>();
        }
    }
}