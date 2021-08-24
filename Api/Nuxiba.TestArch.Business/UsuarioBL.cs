using Nuxiba.TestArch.Data;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Business
{
    public class UsuarioBL
    {
        public static Usuario ObtenerPorUsername(string username)
        {
            return UsuarioDAO.SelectByUserName(username);
        }

        public static int Guardar(Usuario entidad)
        {
            return UsuarioDAO.Insert(entidad);
        }
    }
}