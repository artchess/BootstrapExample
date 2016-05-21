using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BootstrapExample.Models;

namespace BootstrapExample.DAL
{
    // existe  DropCreateDatabaseIfModelChanges (para desarrollo), CreateDatabaseIfNotExists (este se puede usar en producción) y DropCreateDatabaseAlways
    public class LibreriaDBInicializador : DropCreateDatabaseIfModelChanges<LibreriaContext>
    {
        protected override void Seed(LibreriaContext context)
        {
            var autor = new Autor
            {
                Biografia = "...",
                PrimerNombre = "Jamie",
                SegundoNombre = "Munro"
            };

            var libros = new List<Libro>
            {
                new Libro
                {
                 Autor = autor,
                 Descripcion = "...",
                 UrlImagen = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                 Isbn = "1491914319",
                 Synopsis = "...",
                 Titulo = "Knockout.js: Building Dynamic Client-Side Web Applications"
                },
                new Libro
                {
                 Autor = autor,
                 Descripcion = "...",
                 UrlImagen = "http://ecx.images-amazon.com/images/I/51AkFkNeUxL._AA160_.jpg",
                 Isbn = "1449319548",
                 Synopsis = "...",
                 Titulo = "20 Recipes for Programming PhoneGap: Cross-Plataform Movile Development"
                },
                new Libro
                {
                 Autor = autor,
                 Descripcion = "...",
                 UrlImagen = "http://ecx.images-amazon.com/images/I/51LpqnDq8-L._AA160_.jpg",
                 Isbn = "1449309860",
                 Synopsis = "...",
                 Titulo = "20 Recipes for Programming MVC 3: Faster, Smarter Web Development"
                },
                new Libro
                {
                 Autor = autor,
                 Descripcion = "...",
                 UrlImagen = "http://ecx.images-amazon.com/images/I/41JC54HEroL._AA160_.jpg",
                 Isbn = "1460954394",
                 Synopsis = "...",
                 Titulo = "Rapid Application Development With CakePHP"
                }
            };

            // Si nos damos cuenta, no estamos agregando Autor en su DbSet solo los las instancias de Libro. Esta es la magia de EF. Automaticamente sabe que necesita crear el Autor antes de salvar el Libro porque el modelo de Libro fue inicializado con una referencia al Autor. Magia! :)
            libros.ForEach(b => context.Libros.Add(b));
            context.SaveChanges();
        }
    }
}