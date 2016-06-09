using System.Web;
using System.Web.Mvc;

namespace BootstrapExample
{
    /** 
     * Los filtros globales habilita el aplicar un comportamiento consistente
     * atraves de todos los request a nuestra aplicacion web al registrar los filtros via aplication stastup en el global asax. 
     * Los filtros podrían además ser aplicados específicamente a acciones o a un controlador entero añadiendo atributos a la accion o al controlador respectivamente.
     * Cinco diferentes tipos de filtros pueden ser creados.
     * Al inicio de cada web request, cualquier filtro que es definico es ejecutado en el siguiente orden-La exepcion a la regla es el Exception filter debido a que estos filtros solo son llamados cuando un error ocurre-:
     * Authentication filters -- para saber si el usuario es autentico (nuevo en MVC 5) (los web api regresan un 401 Unauthorized request)
     * Authorization filters -- para saber si el usuario autentificado tiene privilegios de acceso o autorización de acceder a cierto recurso.Si la autorización falla MVC comunmente regresa una pagina de error. Un controllador Web API comunmente regresa un 403 Forbidden request. Alternativamente, puede regresar un error 404 Not Found e informar al usuario que el recurso accesado no existe aunque realmente exista.
     * Action filters -- Provee la habilidad de executar código en dos tiempos diferentes. Cuando se define un Action filter, tu puedes opcionalmente implementar una funcion que se ejecuta antes de la accion que fue ejecutada, u opcionalmente despues de que la accion haya finalizado de ejecutarse, pero antes de generar los resultados finales para completar el request.
     * Result filters -- Provee dos diferentes funciones que pueden ser opcionalmente implementados. El primero es cuando el resultado ha acabado de ejecutarse; por ejemplo, en un controllador de MVC una vez que la vista ha sido completamente rendereada y esta lista para ser retornada desde el servidor. La segunda es cuando el resultado es ejecutado. Esta funcion no tiene acceso al contenido final. 
     * Exception filters -- En cualquier momento durante el request siendo manejado por MVC, cualquier Exception filter puede ser ejecutado. Filtros para controladores MVC comunmente retornan un custom error page que puede retornar un mensaje de error específico o genérico. Por ejemplo, Si el modelo es invalido, un 400 Bad Request podía ser retornado. Si una excepción no conocida ocurre, un error 500 podría ser más apropiado para indicar que ocurrio un error de servidor.
     * 
     * **/

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
