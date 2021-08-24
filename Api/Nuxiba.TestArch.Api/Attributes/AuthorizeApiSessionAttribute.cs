using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace Nuxiba.TestArch.Web.Attributes
{
    public class AuthorizeApiSessionAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["UserName"] == null)
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
            }
            else
                return true;

            return base.IsAuthorized(actionContext);
        }
    }
}