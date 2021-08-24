using Nuxiba.TestArch.Web.Attributes;
using System.Web.Mvc;

namespace Nuxiba.TestArch.Web.Controllers
{
    [AuthorizeSession]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}