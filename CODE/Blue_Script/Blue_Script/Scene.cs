//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blue_Script
{
    using System;
    using System.Collections.Generic;
    
    public partial class Scene
    {
        public Scene()
        {
            this.Characters = new HashSet<Character>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int ProjectID { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
