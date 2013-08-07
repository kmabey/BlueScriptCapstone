using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blue_Script.Models
{
	public class AssignedCharacters
	{
		public int CharacterID { get; set; }
		public string FullName { get; set; }
		public bool Assigned { get; set; }
	}
}