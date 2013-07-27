using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blue_Script.Models
{
    public class Character
    {
        public int CharacterID { get; set; }
        public string FullName { get; set; }
        public string Notes { get; set; }
        public char Gender { get; set;}
    }
}