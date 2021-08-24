using Nuxiba.TestArch.Domain.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Nuxiba.TestArch.Web.Controllers.RESTful
{
    //[AuthorizeApiSession]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class SexoController : ApiController
    {
        ISexoService _sexoService;

        public SexoController(ISexoService sexoService)
        {
            _sexoService = sexoService;
        }

        [HttpGet]
        public HttpResponseMessage Obtener()
        {
            var resultado = (HttpResponseMessage)null;

            try
            {
                resultado = Request.CreateResponse(HttpStatusCode.OK, _sexoService.Get());
            }
            catch (Exception exception)
            {
                resultado = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = exception.Message });
            }

            return resultado;
        }
    }
}