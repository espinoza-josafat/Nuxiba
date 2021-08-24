using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Data
{
    public class TareaDAO
    {
        public static int Insert(Tarea entidad)
        {
            var resultado = -1;

            using (var conexion = FactoryDataBase.Create())
            using (var comando = conexion.CreateCommand("spInsertTarea", System.Data.CommandType.StoredProcedure))
            {
                conexion.AddParameter(comando, "Nombre", entidad.Nombre);
                conexion.AddParameter(comando, "Descripcion", entidad.Descripcion);
                conexion.AddParameter(comando, "FechaCreacion", entidad.FechaCreacion);
                conexion.AddParameter(comando, "Estatus", entidad.Estatus);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }
    }
}
