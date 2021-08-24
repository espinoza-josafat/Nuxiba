using System.Web.Http;

namespace Nuxiba.TestArch.Web
{
    public static class WebApiConfig
    {
        public const string UrlPrefixApiWebRelative = "api";

        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ApiWebWithAction",
                routeTemplate: UrlPrefixApiWebRelative + "/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: UrlPrefixApiWebRelative + "/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}