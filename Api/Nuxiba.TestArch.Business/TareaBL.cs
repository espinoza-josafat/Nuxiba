using Nuxiba.TestArch.Data;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Business
{
    public class TareaBL
    {
        public static int Guardar(Tarea entidad)
        {
            return TareaDAO.Insert(entidad);
        }
    }
}
