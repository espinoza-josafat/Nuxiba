using Nuxiba.TestArch.Data.Views;
using Nuxiba.TestArch.Entities.Views;
using System.Collections.Generic;

namespace Nuxiba.TestArch.Business.Views
{
    public class VWUsuarioBL
    {
        public static List<VWUsuario> Obtener()
        {
            return VWUsuarioDAO.Select();
        }
    }
}