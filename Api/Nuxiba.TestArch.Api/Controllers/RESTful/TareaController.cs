using Nuxiba.TestArch.Business;
using Nuxiba.TestArch.Business.Views;
using Nuxiba.TestArch.Domain.Services;
using Nuxiba.TestArch.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Nuxiba.TestArch.Api.Controllers.RESTful
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class TareaController : ApiController
    {
        ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        public HttpResponseMessage Lista()
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                resultado = Request.CreateResponse(HttpStatusCode.OK, _tareaService.Get());
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }

        [HttpPost]
        public HttpResponseMessage Guardar(Tarea entidad)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                if (entidad == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El modelo no puede ser nulo" });

                if (entidad.Id > 0)
                {
                    var entidadTemp = _tareaService.GetById(entidad.Id);
                    if (entidadTemp != null)
                    {
                        entidadTemp.Nombre = entidad.Nombre;
                        entidadTemp.Descripcion = entidad.Descripcion;
                        entidadTemp.Estatus = entidad.Estatus;

                        _tareaService.Update(entidadTemp);

                        resultado = Request.CreateResponse(HttpStatusCode.OK, entidad.Id);
                    }
                    else
                    {
                        resultado = Request.CreateResponse(HttpStatusCode.OK, -1);
                    }
                }
                else
                {
                    resultado = Request.CreateResponse(HttpStatusCode.OK, TareaBL.Guardar(entidad));
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
                resultado = Request.CreateResponse(HttpStatusCode.OK, _tareaService.GetById(id));
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }

        [HttpPost]
        public HttpResponseMessage Eliminar(Tarea entidad)
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                if (entidad == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El modelo no puede ser nulo" });
                if (entidad.Id < 1)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "El código debe ser amyor a 0" });

                entidad = _tareaService.GetById(entidad.Id);

                if (entidad != null)
                {
                    _tareaService.Delete(entidad);

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