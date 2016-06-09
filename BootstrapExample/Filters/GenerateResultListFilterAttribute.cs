using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapExample.ViewModels;

namespace BootstrapExample.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenerateResultListFilterAttribute : FilterAttribute, IResultFilter
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;

        public GenerateResultListFilterAttribute(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }

        // Este método se ejecuta antes de que la vista sea generada
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // en este método uso reflection para dinamicamente instanciar el ResultList al destinationType y llenar los resultados al ejecutar automapper.

            var model = filterContext.Controller.ViewData.Model;

            var resultListGeneticType = typeof(ResultList<>)
                                        .MakeGenericType(new Type[] { _destinationType });

            var srcGenericType = typeof(List<>).MakeGenericType(new Type[] { _sourceType });

            var destGenericType = typeof(List<>).MakeGenericType(new Type[] { _destinationType });

            AutoMapper.Mapper.CreateMap(_sourceType, _destinationType);
            var viewModel = AutoMapper.Mapper.Map(model, srcGenericType, destGenericType);

            var queryOptions = filterContext.Controller.ViewData.ContainsKey("QueryOptions") ? filterContext.Controller.ViewData["QueryOptions"] : new QueryOptions();

            var resultList = Activator.CreateInstance(resultListGeneticType, viewModel, queryOptions);

            filterContext.Controller.ViewData.Model = resultList;
        }

        // Este método es llamado despues de que la vista es generada y lista para ser retornada del servidor.
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}