using Nuxiba.TestArch.Business;
using Nuxiba.TestArch.Business.Views;
using Nuxiba.TestArch.Domain.Services;
using Nuxiba.TestArch.Entities;
using Nuxiba.TestArch.Tools.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Nuxiba.TestArch.Web.Controllers.RESTful
{
    //[AuthorizeApiSession]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class UsuarioController : ApiController
    {
        IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public HttpResponseMessage Lista()
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                resultado = Request.CreateResponse(HttpStatusCode.OK, VWUsuarioBL.Obtener());
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }

        [HttpPost]
        public HttpResponseMessage Guardar(Usuario entidad)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                if (entidad == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El modelo no puede ser nulo" });

                if (entidad.Id > 0)
                {
                    var entidadTemp = _usuarioService.GetById(entidad.Id);
                    if (entidadTemp != null)
                    {
                        entidadTemp.Username = entidad.Username;
                        entidadTemp.CorreoElectronico = entidad.CorreoElectronico;
                        entidadTemp.Nombre = entidad.Nombre;
                        entidadTemp.Password = Tools.Helpers.Cryptography.Encrypt(entidad.Password);
                        entidadTemp.Sexo = entidad.Sexo;

                        _usuarioService.Update(entidadTemp);

                        resultado = Request.CreateResponse(HttpStatusCode.OK, entidad.Id);
                    }
                    else
                    {
                        resultado = Request.CreateResponse(HttpStatusCode.OK, -1);
                    }
                }
                else
                {
                    entidad.Password = Cryptography.Encrypt(entidad.Password);
                    resultado = Request.CreateResponse(HttpStatusCode.OK, UsuarioBL.Guardar(entidad));
                }
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }

        [HttpGet]
        public HttpResponseMessage ObtenerPorId(int id)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                resultado = Request.CreateResponse(HttpStatusCode.OK, _usuarioService.GetById(id));
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }

        [HttpGet]
        public HttpResponseMessage ObtenerPorUsernameId(string username, int? id = null)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                resultado = Request.CreateResponse(HttpStatusCode.OK, _usuarioService.GetByUsernameId(username, id));
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }


        [HttpPost]
        public HttpResponseMessage Eliminar(Usuario entidad)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                if (entidad == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El modelo no puede ser nulo" });
                if (entidad.Id < 1)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El código debe ser amyor a 0" });

                entidad = _usuarioService.GetById(entidad.Id);

                if (entidad != null)
                {
                    entidad.Estatus = false;
                    _usuarioService.Update(entidad);

                    resultado = Request.CreateResponse(HttpStatusCode.OK, 1);
                }
                else
                    resultado = Request.CreateResponse(HttpStatusCode.OK, -1);
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }
    }
}