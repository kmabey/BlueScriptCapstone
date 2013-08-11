using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blue_Script.Models
{
	public class AssignedCharacters
	{
		[HiddenInput(DisplayValue = false)]
		public int CharacterID { get; set; }

		public string FullName { get; set; }
		public bool Assigned { get; set; }
	}
}