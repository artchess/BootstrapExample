using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BootstrapExample.DAL;
using BootstrapExample.Models;

namespace BootstrapExample.Controllers.api
{
    public class Autors2Controller : ApiController
    {
        private LibreriaContext db = new LibreriaContext();

        // GET: api/Autors2
        public IQueryable<Autor> GetAutor()
        {
            return db.Autor;
        }

        // GET: api/Autors2/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult GetAutor(int id)
        {
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        // PUT: api/Autors2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutor(int id, Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autor.Id)
            {
                return BadRequest();
            }

            db.Entry(autor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Autors2
        [ResponseType(typeof(Autor))]
        public IHttpActionResult PostAutor(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autor.Add(autor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autor.Id }, autor);
        }

        // DELETE: api/Autors2/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult DeleteAutor(int id)
        {
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            db.Autor.Remove(autor);
            db.SaveChanges();

            return Ok(autor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutorExists(int id)
        {
            return db.Autor.Count(e => e.Id == id) > 0;
        }
    }
}