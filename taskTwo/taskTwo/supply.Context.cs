﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace taskTwo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestdbEntities : DbContext
    {
        public TestdbEntities()
            : base("name=TestdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<region> regions { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
