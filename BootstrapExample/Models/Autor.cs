using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BootstrapExample.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage="El Primer Nombre es requerido")] // Más Data Annotations: https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx
        public string PrimerNombre { get; set; }
        [Required(ErrorMessage="El Segundo Nombre es requerido")]
        public string SegundoNombre { get; set; }
        public string Biografia { get; set; }
        public virtual ICollection<Libro> Libros { get; set; } // lista de libros del autor en cuestión

    }
}
