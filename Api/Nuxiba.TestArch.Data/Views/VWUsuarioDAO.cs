using Nuxiba.TestArch.Data.Helpers;
using Nuxiba.TestArch.Entities.Views;
using System.Collections.Generic;

namespace Nuxiba.TestArch.Data.Views
{
    public class VWUsuarioDAO
    {
        public static List<VWUsuario> Select()
        {
            var resultado = new List<VWUsuario>();

            using (var conexion = FactoryDataBase.Create())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"SELECT [Id]
                                              ,[Username]
                                              ,[CorreoElectronico]
                                              ,[Sexo]
                                              ,[Estatus]
                                              ,[Nombre]
                                          FROM [dbo].[vwUsuario]";

                using (var lector = comando.ExecuteReader())
                    while (lector.Read())
                        resultado.Add(MappingDAOs.MapToClass<VWUsuario>(lector));
            }

            return resultado;
        }
    }
}