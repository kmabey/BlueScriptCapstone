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
    
    public partial class Character
    {
        public Character()
        {
            this.Scenes = new HashSet<Scene>();
        }
    
        public int CharacterID { get; set; }
        public string FullName { get; set; }
        public string Notes { get; set; }
    
        public virtual ICollection<Scene> Scenes { get; set; }
    }
}
