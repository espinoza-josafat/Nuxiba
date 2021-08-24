using Nuxiba.TestArch.Data.Helpers;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Data
{
    public class UsuarioDAO
    {
        public static Usuario SelectByUserName(string username)
        {
            var resultado = (Usuario)null;

            using (var conexion = FactoryDataBase.Create())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"SELECT [Id]
                                              ,[CorreoElectronico]
                                              ,[Username]
                                              ,[Password]
                                              ,[Estatus]
                                              ,[Sexo]
                                              ,[FechaCreacion]
                                              ,[Nombre]
                                          FROM [dbo].[Usuario]
                                         WHERE [Username]=@Username";

                conexion.AddParameter(comando, "Username", username);

                using (var lector = comando.ExecuteReader())
                    if (lector.Read())
                        resultado = MappingDAOs.MapToClass<Usuario>(lector);
            }

            return resultado;
        }
        
        public static int Insert(Usuario entidad)
        {
            var resultado = -1;

            using (var conexion = FactoryDataBase.Create())
            using (var comando = conexion.CreateCommand("spInsertUsuario", System.Data.CommandType.StoredProcedure))
            {
                conexion.AddParameter(comando, "CorreoElectronico", entidad.CorreoElectronico);
                conexion.AddParameter(comando, "Username", entidad.Username);
                conexion.AddParameter(comando, "Nombre", entidad.Nombre);
                conexion.AddParameter(comando, "Password", entidad.Password);
                conexion.AddParameter(comando, "Estatus", entidad.Estatus);
                conexion.AddParameter(comando, "Sexo", entidad.Sexo);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }
    }
}