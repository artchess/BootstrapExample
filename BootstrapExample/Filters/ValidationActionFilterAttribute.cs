using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BootstrapExample.Filters
{
    /**
     * Este Action filter hereda de ActionFilterAttribute, que es 
     * una clase abstracta que contiene cuatro funciones virtuales que pueden ser opcionalmente sobrecargadas en tu clase Action filter:
     * 
     * OnActionExecuting:
     *      Es llamada justo antes de ejecutar el código del metodo del controlador. 
     *      Si el ModelState es invalido, la respuesta es inmediatamente terminada y un 400 Bad Request es retornado del servidor. Esto asegura que la data es valida cuando el metodo del controlador es ejecutada en base a la validacion de las reglas de ese modelo.
     * 
     * OnActionExecutingAsync
     *      Es identica a OnActionExecuting con la excepción de que trabaja con controladores asincronos
     *      
     * OnActionExecuted:
     *      Es llamada despues que el metodo del controlador ha finalizado de ejecutarse, pero es extremadamente importante que es disparada antes de la respuesta siendo contruida y enviada de vuelta al servidor.
     *      
     * OnActionExecutedAsync:
     *      Es identica a OnActionExecuted con la excepción de que trabaja con controlladores asincronos.
     *      
     * Este Action filter puede implementarse globalmente. Global Web API filters son definicos en la calse WebApiConfig
     *      
     **/

    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, modelState); 

                //actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}