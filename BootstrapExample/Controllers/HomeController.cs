using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapExample.DAL;
using BootstrapExample.Models;
using BootstrapExample.ViewModels;

namespace BootstrapExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LibreriaContext context = new LibreriaContext();

            var libros = context.Libros.ToList();

            context.Dispose();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult Advanced()
        {
            Persona persona = new Persona()
            {
                PrimerNombre = "Arturo",
                SegundoNombre = "Ferreiro"
            };

            return View(persona);
        }
    }
}