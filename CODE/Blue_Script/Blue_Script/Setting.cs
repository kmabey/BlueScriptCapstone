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
    
    public partial class Setting
    {
        public Setting()
        {
            this.Scenes = new HashSet<Scene>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    
        public virtual ICollection<Scene> Scenes { get; set; }
    }
}
