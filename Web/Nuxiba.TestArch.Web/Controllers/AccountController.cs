using Nuxiba.TestArch.Entities;
using Nuxiba.TestArch.Tools.Http;
using Nuxiba.TestArch.Web.Exceptions;
using Nuxiba.TestArch.Web.Models;
using Nuxiba.TestArch.Web.Results;
using System;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Nuxiba.TestArch.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginModel modelo)
        {
            var resultado = (JsonHttpStatusResult)null;

            try
            {
                if (modelo == null)
                    return new JsonHttpStatusResult(HttpStatusCode.BadRequest, new { message = "El modelo no puede ser nulo" });
                if (string.IsNullOrWhiteSpace(modelo.UserName))
                    return new JsonHttpStatusResult(HttpStatusCode.BadRequest, new { message = "El usuario no puede ser nulo o vacío" });
                if (string.IsNullOrWhiteSpace(modelo.Password))
                    return new JsonHttpStatusResult(HttpStatusCode.BadRequest, new { message = "La contraseña no puede ser nula o vacía" });

                Usuario usuario = await RequestHttp.PostJsonWithErrorAsync<Usuario>(ConfigurationManager.AppSettings["api"] + "api/account/login", modelo);
                
                IPrincipal principalObj = new Providers.PrincipalProvider().CreatePrincipal(usuario.Username);
                Thread.CurrentPrincipal = principalObj;
                HttpContext.User = principalObj;
                HttpContext.Session["UserName"] = usuario.Username;
                HttpContext.Session["User"] = usuario;

                FormsAuthentication.SetAuthCookie(usuario.Username, false);

                resultado = new JsonHttpStatusResult(HttpStatusCode.OK, 1);
            }
            catch (BusinessLogicException businessLogicException)
            {
                resultado = new JsonHttpStatusResult(HttpStatusCode.NotFound, new { message = businessLogicException.Message });
            }
            catch (Exception exception)
            {
                resultado = new JsonHttpStatusResult(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            var resultado = (JsonHttpStatusResult)null;

            try
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Abandon();

                HttpContext.User = null;

                Thread.CurrentPrincipal = null;


                FormsAuthentication.SignOut();

                resultado = new JsonHttpStatusResult(HttpStatusCode.OK, 1);
            }
            catch (Exception exception)
            {
                resultado = new JsonHttpStatusResult(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }
    }
}