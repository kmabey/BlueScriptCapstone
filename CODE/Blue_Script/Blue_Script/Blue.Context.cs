﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BlueScriptEntities : DbContext
    {
        public BlueScriptEntities()
            : base("name=BlueScriptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Unsorted> Unsorteds { get; set; }
        public DbSet<Forget> Forgets { get; set; }
    }
}
