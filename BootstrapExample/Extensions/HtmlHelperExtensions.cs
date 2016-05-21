using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


//Extensión de HtmlHelper que convierte un Modelo en string JSON, no le pongo namespace para usarlo en cualquier vista sin necesidad de utilizar un @using
public static class HtmlHelperExtensions
{
    public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        return new HtmlString(JsonConvert.SerializeObject(model, settings));
    }
}
