using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BootstrapExample.ViewModels
{
    public class AutorViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Primer Nombre es requerido")] // Más Data Annotations: https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx
        [JsonProperty(PropertyName = "primerNombre")]
        public string PrimerNombre { get; set; }

        [Required(ErrorMessage = "El Segundo Nombre es requerido")]
        [JsonProperty(PropertyName = "segundoNombre")]
        public string SegundoNombre { get; set; }

        [JsonProperty(PropertyName = "biografia")]
        public string Biografia { get; set; }

    }
}