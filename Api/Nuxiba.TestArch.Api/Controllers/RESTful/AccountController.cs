using Nuxiba.TestArch.Business.Process;
using Nuxiba.TestArch.Domain.Services;
using Nuxiba.TestArch.Tools.Exceptions;
using Nuxiba.TestArch.Web.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Nuxiba.TestArch.Web.Controllers.RESTful
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class AccountController : ApiController
    {
        IUsuarioService _usuarioService;

        public AccountController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public HttpResponseMessage Login(UserLoginModel modelo)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                if (modelo == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El modelo no puede ser nulo" });
                if (string.IsNullOrWhiteSpace(modelo.UserName))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El usuario no puede ser nulo o vacío" });
                if (string.IsNullOrWhiteSpace(modelo.Password))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "La contraseña no puede ser nula o vacía" });
                
                var usuario = new ProcesoAutenticarUsuario().Ejecutar(modelo.UserName, Tools.Helpers.Cryptography.Encrypt(modelo.Password));

                resultado = Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (BusinessLogicException businessLogicException)
            {
                resultado = Request.CreateResponse(HttpStatusCode.NotFound, new { message = businessLogicException.Message });
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }
    }
}