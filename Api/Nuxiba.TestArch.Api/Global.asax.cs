using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Nuxiba.TestArch.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Bootstrapper.Run();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsApiWebRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsApiWebRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower().StartsWith(WebApiConfig.UrlPrefixApiWebRelative.StartsWith("~/") ? WebApiConfig.UrlPrefixApiWebRelative.ToLower() : string.Format("~/{0}", WebApiConfig.UrlPrefixApiWebRelative.ToLower()));
        }
    }
}