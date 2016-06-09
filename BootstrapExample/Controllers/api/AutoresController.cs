using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BootstrapExample.DAL;
using BootstrapExample.Models;
using BootstrapExample.ViewModels;

namespace BootstrapExample.Controllers.api
{
    public class AutoresController : ApiController
    {
        private LibreriaContext db = new LibreriaContext();


        /*** HTTP Status Codes
         * aplicaciones RESTful depende mucho de los HTTP Status Codes 
         * para proveer el feedback del API request. Los tres principales niveles que son comunmente usados son:
         * Successful 2xx
         *      200 OK, 201 Created, 204 No Content. Cualquier respuesta 
         *      con código 200s es usado para identificar que el API request fue correcto
         * Client Error 400
         *      400 Bad Request (los datos no son validos), 401 Unauthorized, 404 Not Found, 405 Method Not Allowed. 
         *      Cualquier respuesta en 400s es usada para identificar que el ejecutador del API hizo halgo incorrectamente. 
         *      Es comun que en el cuerpo de la respuesta contenga un mensaje de error de ayuda para resolver el problema para que se puede hacer re submit al mismo request.
         * Server Error 5xx
         *     500 Internal Server Error, 501 Not Implemented, 503 Service Unavailable (comunmente usado para limitar el numero de request al API). Cualquier request que regrese 500s es usado para identificar un error de servidor y el executador del API debería reintentar el request de nueva cuenta. 
         *     También es comun para el body de la respuesta contener un mensaje de error de ayuda para identificar cual fue el problema.
         *     
        ***/

        //GET: api/Autores
        public ResultList<AutorViewModel> Get([FromUri]QueryOptions queryOptions)
        {
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            var autores = db.Autor.OrderBy(queryOptions.Sort) // aqui uso LINQ Dynamic
                                .Skip(start)
                                .Take(queryOptions.PageSize);

            queryOptions.TotalPages = (int)Math.Ceiling((double)db.Autor.Count() / queryOptions.PageSize);

            AutoMapper.Mapper.CreateMap<Autor, AutorViewModel>();

            //return new ResultList<AutorViewModel>
            //{
            //    QueryOptions = queryOptions,
            //    Results = AutoMapper.Mapper.Map<List<AutorViewModel>>(autores.ToList())
            //};

            return new ResultList<AutorViewModel>(AutoMapper.Mapper.Map<List<Autor>, List<AutorViewModel>>(autores.ToList()), queryOptions);
        }

        [ResponseType(typeof(AutorViewModel))]
        public IHttpActionResult Get(int? id)
        {
            if(id == null)
            {
                return BadRequest("No se ha especificado el id del Autor");
            }

            Autor autor = db.Autor.Find(id);
            if(autor == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.CreateMap<Autor, AutorViewModel>();
            return Ok(AutoMapper.Mapper.Map<AutorViewModel>(autor));
        }

        // PUT: api/autores
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(AutorViewModel autor)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoMapper.Mapper.CreateMap<AutorViewModel, Autor>();
            db.Entry(AutoMapper.Mapper.Map<AutorViewModel, Autor>(autor)).State = EntityState.Modified;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/autores
        [ResponseType(typeof(AutorViewModel))]
        public IHttpActionResult Post(AutorViewModel autor)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoMapper.Mapper.CreateMap<AutorViewModel, Autor>();
            db.Autor.Add(AutoMapper.Mapper.Map<AutorViewModel, Autor>(autor));
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autor.Id }, autor);
        }


        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
