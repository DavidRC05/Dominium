﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DominiumAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DominiumEntities1 : DbContext
    {
        public DominiumEntities1()
            : base("name=DominiumEntities1")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TUsers> TUsers { get; set; }
        public virtual DbSet<TPropiedades> TPropiedades { get; set; }
        public virtual DbSet<TProvincias> TProvincias { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Visitas> Visitas { get; set; }
    
        public virtual int RegisterUsers(string firstName, string lastName, string email, string phoneNumber, string password, Nullable<int> rol)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegisterUsers", firstNameParameter, lastNameParameter, emailParameter, phoneNumberParameter, passwordParameter, rolParameter);
        }
    
        public virtual ObjectResult<ObtenerPropiedadConVendedor_Result> ObtenerPropiedadConVendedor(Nullable<int> propiedadID)
        {
            var propiedadIDParameter = propiedadID.HasValue ?
                new ObjectParameter("PropiedadID", propiedadID) :
                new ObjectParameter("PropiedadID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerPropiedadConVendedor_Result>("ObtenerPropiedadConVendedor", propiedadIDParameter);
        }
    }
}
