using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BootstrapExample.Models
{
    public class Autor
    {
        [JsonProperty(PropertyName="id")]
        public int Id { get; set; }
        [Required(ErrorMessage="El Primer Nombre es requerido")] // Más Data Annotations: https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx
        [JsonProperty(PropertyName = "primerNombre")]
        public string PrimerNombre { get; set; }
        [Required(ErrorMessage="El Segundo Nombre es requerido")]
        [JsonProperty(PropertyName = "segundoNombre")]
        public string SegundoNombre { get; set; }
        [JsonProperty(PropertyName = "biografia")]
        public string Biografia { get; set; }
        [JsonProperty(PropertyName = "libros")]
        public virtual ICollection<Libro> Libros { get; set; } // lista de libros del autor en cuestión

    }
}
