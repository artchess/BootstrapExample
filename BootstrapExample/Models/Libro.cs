using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapExample.Models
{
    public class Libro
    {
        public int Id { get; set; } // por convención Id se reconoce como una llave primaria, las convenciones pueden modificarse en OnModelCreating
        public int AutorId { get; set; } // por convención Clase + "Id" se reconoce como una llave foranea
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public string Synopsis { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public virtual Autor Autor { get; set; } // virtual nos provee de la habilidad de crear propiedades de navegación, es decir, de acceder al modelo de Autor directamente del modelo Libro.
    }
}