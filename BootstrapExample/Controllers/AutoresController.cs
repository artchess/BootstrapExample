using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BootstrapExample.DAL;
using BootstrapExample.Models;

namespace BootstrapExample.Controllers
{
    public class AutoresController : Controller
    {
        //esta variable se va a instanciar al inicio de cada request del browser
        private LibreriaContext db = new LibreriaContext();

        // GET: Autores
        public ActionResult Index()
        {
            // al acceso al DbSet Autor le sigue la funcion ToList, este es un importante concepto al momento de trabajar con entity framework. Cuando estamos interactuando con un DbSet, EF no executa ningun query a la base de datos hasta que los datos son accesados en el código. Al momento en que llamo a ToList, esto le dice a EF que execute un query y llene la lista de autores en una lista. Si pusieramos un Where por ejemplo, antes del ToList lo que va a hacer EF es modificar el query que va a ejecutar al llamar a ToList. Una vez que los datos han sido obtenidos de la base de datos, cualquier manipulación  posterior al resultado se hará estrictamente en el objeto en la memoria y no contra la base de datos. (solamente si esta habilitado lazy loading y accedemos a propiedades de navegación, se ejecutará un query contra la base de datos para traer lo que solicitamos)     

            //var autor = db.Autor.Where(a => a.Id == 1).Where(a => a.PrimerNombre == "Jamie").ToList();

            //var prueba = "";

            //var libros = autor.First().Libros;

            return View("IndexConKnockout", db.Autor.ToList());
        }

        // GET: Autores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // GET: Autores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PrimerNombre,SegundoNombre,Biografia")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Autor.Add(autor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autor);
        }

        // GET: Autores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PrimerNombre,SegundoNombre,Biografia")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autor autor = db.Autor.Find(id);
            db.Autor.Remove(autor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // este dispose es importante porque se hace una desconexión de la base de datos con db.Dispose(), esto cierra todas las conexiones abiertas para prevenir perdida de memoria. Aqui se sobreescribe el metodo Dispose de la clase base Controller. Esta función es llamada por MVC automáticamente al término de cada request.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
