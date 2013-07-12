using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProject01.Models
{
    public class InformationModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string input { get; set; }

        public int number { get; set; }
    }
}