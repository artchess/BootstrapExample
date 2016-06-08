using System.Web;
using System.Web.Mvc;
using BootstrapExample.Models;
using BootstrapExample.ViewModels;
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

    //No es necesario crear un método HtmlHelper extension para implementar sorting en las vistas, sin embargo esta manera de hacerlo provee 2 buenos beneficios:
    // 1. No se hace una revoltura de HTML y Razor 2. Es reusable y se ahorra código.

    public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField, QueryOptions queryOptions)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        var isCurrentSortField = queryOptions.SortField == sortField;

        return new MvcHtmlString(string.Format("<a href=\"{0}\">{1} {2}</>",
            urlHelper.Action(actionName, new
            {
                SortField = sortField,
                SortOrder = (isCurrentSortField && queryOptions.SortOrder == SortOrder.ASC.ToString()) ? SortOrder.DESC : SortOrder.ASC
            }),
            fieldName,
            BuildSortIcon(isCurrentSortField, queryOptions)));
    }

    //version knokout del método anterior
    public static MvcHtmlString BuildKnockoutSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        return new MvcHtmlString(string.Format("<a href=\"{0}\" data-bind=\"click: pagingService.sortEntitiesBy\" data-sort-field=\"{1}\">{2}<span data-bind=\"css: pagingService.buildSortIcon('{1}')\"></span></a>",
            urlHelper.Action(actionName),
            sortField,
            fieldName));
    }

    private static string BuildSortIcon(bool isCurrentSortField, QueryOptions queryOptions)
    {
        string sortIcon = "sort";

        if(isCurrentSortField)
        {
            sortIcon += "-by-alphabet";
            if (queryOptions.SortOrder == SortOrder.DESC.ToString())
                sortIcon += "-alt";
        }

        return string.Format("<span class=\"{0} {1}{2}\"></span>", "glyphicon", "glyphicon-", sortIcon);
    }

    public static MvcHtmlString BuildPaggingLinks(this HtmlHelper htmlHelper, QueryOptions queryOptions, string actionName)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

        return new MvcHtmlString(string.Format(
            "<nav>" +
            "   <ul class=\"pager\">" +
            "       <li class=\"previous {0}\">{1}</li>" +
            "       <li class=\"next {2}\">{3}</li>" +
            "   </ul>" +
            "</nav>",
            IsPreviousDisabled(queryOptions),
            BuildPreviousLink(urlHelper, queryOptions, actionName),
            IsNextDisabled(queryOptions),
            BuildNextLink(urlHelper, queryOptions, actionName)
            ));  
    }

    //version knockout del metodo anterior
    public static MvcHtmlString BuildKnockoutPaggingLinks(this HtmlHelper htmlHelper, string actionName)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
        
        return new MvcHtmlString(string.Format(
            "<nav>" +
            "   <ul class=\"pager\">" +
            "       <li data-bind=\"css: pagingService.buildPreviousClass()\">" +
            "           <a href=\"{0}\" data-bind=\"click: pagingService.previousPage\">Anterior</a>" +
            "       </li>" +
            "       <li data-bind=\"css: pagingService.buildNextClass()\">" +
            "           <a href=\"{0}\" data-bind=\"click: pagingService.nextPage\">Siguiente</a>" +
            "       </li>" +
            "   </ul>" +
            "</nav>",
            @urlHelper.Action(actionName)
            ));
    }

    private static string IsPreviousDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == 1) ? "disabled" : string.Empty;
    }

    private static string IsNextDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == queryOptions.TotalPages) ? "disabled" : string.Empty;
    }

    private static string BuildPreviousLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
    {
        return string.Format(
            "<a href=\"{0}\"><span aria-hidden=\"true\">&larr;</span> Anterior</a>",
            (queryOptions.CurrentPage == 1) ? "#" : urlHelper.Action(actionName, new
            {
                SortOrder = queryOptions.SortOrder,
                SortField = queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage - 1,
                PageSize = queryOptions.PageSize
            }));
    }

    private static object BuildNextLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
    {
        return string.Format(
            "<a href=\"{0}\">Siguiente <span aria-hidden=\"true\">&rarr;</span></a>",
            (queryOptions.CurrentPage == queryOptions.TotalPages) ? "#" : urlHelper.Action(actionName, new
            {
                SortOrder = queryOptions.SortOrder,
                SortField = queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage + 1,
                PageSize = queryOptions.PageSize
            }));
    }

}
