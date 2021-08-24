using System.Web;

namespace Nuxiba.TestArch.Web.Helpers
{
    public class UsuarioAutenticado
    {
        public static string ObtenerNombreCompleto()
        {
            var resultado = string.Empty;

            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["User"] != null)
                resultado = HttpContext.Current.Session["UserName"].ToString();

            return resultado;
        }
    }
}