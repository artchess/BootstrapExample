using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapExample.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Biografia { get; set; }
        public virtual ICollection<Libro> Libros { get; set; } // lista de libros del autor en cuestión

    }
}
