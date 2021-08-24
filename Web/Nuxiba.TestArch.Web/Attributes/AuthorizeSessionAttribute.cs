using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Nuxiba.TestArch.Web.Attributes
{
    public class AuthorizeSessionAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null || httpContext.Session == null || httpContext.Session["UserName"] == null)
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();

                    HttpContext.Current.User = null;
                }

                Thread.CurrentPrincipal = null;

                FormsAuthentication.SignOut();

                return false;
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}