﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlobalCalendar
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CalendarEntities : DbContext
    {
        public CalendarEntities()
            : base("name=CalendarEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<AllCours> AllCourses { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
    }
}
