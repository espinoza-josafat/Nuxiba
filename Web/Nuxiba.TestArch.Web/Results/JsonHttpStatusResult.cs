using System.Net;
using System.Web.Mvc;

namespace Nuxiba.TestArch.Web.Results
{
    public class JsonHttpStatusResult : JsonResult
    {
        readonly HttpStatusCode _httpStatus;

        public JsonHttpStatusResult(HttpStatusCode httpStatus, object data)
        {
            Data = data;
            _httpStatus = httpStatus;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.RequestContext.HttpContext.Response.StatusCode = (int)_httpStatus;
            base.ExecuteResult(context);
        }
    }
}