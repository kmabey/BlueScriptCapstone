using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blue_Script.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}