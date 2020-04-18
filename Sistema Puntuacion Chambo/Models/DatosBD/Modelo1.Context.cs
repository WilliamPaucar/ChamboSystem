﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistema_Puntuacion_Chambo.Models.DatosBD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DatosEntities : DbContext
    {
        public DatosEntities()
            : base("name=DatosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_candidatas> tbl_candidatas { get; set; }
        public virtual DbSet<tbl_presentacion_cuatro> tbl_presentacion_cuatro { get; set; }
        public virtual DbSet<tbl_presentacion_dos> tbl_presentacion_dos { get; set; }
        public virtual DbSet<tbl_presentacion_tres> tbl_presentacion_tres { get; set; }
        public virtual DbSet<tbl_presentacion_uno> tbl_presentacion_uno { get; set; }
        public virtual DbSet<tbl_users_jueces> tbl_users_jueces { get; set; }
    
        public virtual ObjectResult<Nullable<byte>> PromediarNotas(Nullable<int> usuario)
        {
            var usuarioParameter = usuario.HasValue ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<byte>>("PromediarNotas", usuarioParameter);
        }
    }
}
