using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using BootstrapExample.Models;

namespace BootstrapExample.DAL
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext() : base("LibreriaContext") // llamo el contructor de la clase base pasandole el parámetro "LibreriaContext" este es el nombre de mi conexión.
        {

        }

        public DbSet<Libro> Libros { get; set; } // un DbSet esta relacionado a una tabla en la base de datos, un modelo representa un registro de la tabla.
        public DbSet<Autor> Autor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // lo que hago aqui es una sobre carga del metodo OnModelCreating, cuando crea el modelo ejecuto esta función
        {
            // especifico como queiro que mis tablas y columnas sean creadas.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // remuevo la convención de Pluralización en nombres de tablas.

            base.OnModelCreating(modelBuilder);
        }
    }
}