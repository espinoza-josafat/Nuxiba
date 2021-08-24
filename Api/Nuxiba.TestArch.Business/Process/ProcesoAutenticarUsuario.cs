using Nuxiba.TestArch.Data;
using Nuxiba.TestArch.Entities;
using Nuxiba.TestArch.Tools.Exceptions;

namespace Nuxiba.TestArch.Business.Process
{
    public class ProcesoAutenticarUsuario
    {
        public Usuario Ejecutar(string username, string password)
        {
            var resultado = (Usuario)null;

            resultado = UsuarioDAO.SelectByUserName(username);

            if (resultado == null)
                throw new BusinessLogicException("El usuario no existe");
            if (!resultado.Estatus)
                throw new BusinessLogicException("Este usuario ya no esta activo");

            if (!password.Equals(resultado.Password))
            {
                throw new BusinessLogicException("No es correcta la contraseña");
            }
            
            return resultado;
        }
    }
}