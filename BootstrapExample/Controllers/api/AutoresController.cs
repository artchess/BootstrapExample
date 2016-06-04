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

        //GET: api/Autores
        //public ResultList<AutorViewModel> Get([FromUri]QueryOptions queryOptions)
        //{
        //    var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

        //    var autores = db.Autor.OrderBy(queryOptions.Sort) // aqui uso LINQ Dynamic
        //                        .Skip(start)
        //                        .Take(queryOptions.PageSize);

        //    queryOptions.TotalPages = (int)Math.Ceiling((double)db.Autor.Count() / queryOptions.PageSize);

        //    AutoMapper.Mapper.CreateMap<Autor, AutorViewModel>();

        //    return new ResultList<AutorViewModel>
        //    { 
        //        QueryOptions = queryOptions,
        //        Results = AutoMapper.Mapper.Map<List<AutorViewModel>>(autores.ToList())
        //    }
        //}

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
