﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ВПФки
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StroyMatEntities : DbContext
    {
        public StroyMatEntities()
            : base("name=StroyMatEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProOrd> ProOrd { get; set; }
        public virtual DbSet<ProWare> ProWare { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserExtraData> UserExtraData { get; set; }
        public virtual DbSet<WareHouse> WareHouse { get; set; }
    }
}