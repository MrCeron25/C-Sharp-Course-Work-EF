﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class course_work_EFEntities1 : DbContext
    {
        public course_work_EFEntities1()
            : base("name=course_work_EFEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<buyer> buyer { get; set; }
        public virtual DbSet<order_details> order_details { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<properties> properties { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<system> system { get; set; }
    }
}
