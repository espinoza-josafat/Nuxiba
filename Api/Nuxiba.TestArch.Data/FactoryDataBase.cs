using Nuxiba.TestArch.Data.Interfaces;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Nuxiba.TestArch.Data
{
    public static class FactoryDataBase
    {
        public static IDataBase Create(SGBDType SGBDType)
        {
            return Create("ConnectionString", SGBDType);
        }

        public static IDataBase Create(string connectionStringName = "ConnectionString", SGBDType SGBDType = SGBDType.SqlServer)
        {
            var resultado = (IDataBase)null;

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new NullReferenceException("connectionString");

            if (SGBDType == SGBDType.Configuration)
            {
                var providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

                if (string.IsNullOrWhiteSpace(providerName))
                    throw new NullReferenceException("providerName");

                providerName = providerName.ToLower().Trim();
                
                if (providerName.Contains("system.data.sqlclient"))
                    resultado = new SqlDataBase(connectionString, true);
                else if (providerName.Contains("oracle.manageddataaccess"))
                    resultado = new OracleDataBase(connectionString, true);
                else
                    throw new NotImplementedException("Proveedor de datos no identificado");
            }
            else if (SGBDType == SGBDType.SqlServer)
                resultado = new SqlDataBase(connectionString, true);
            else if (SGBDType == SGBDType.Oracle)
                resultado = new OracleDataBase(connectionString, true);

            return resultado;
        }
        
        public static async Task<IDataBase> CreateAsync(SGBDType SGBDType)
        {
            return await CreateAsync("ConnectionString", SGBDType);
        }

        public static async Task<IDataBase> CreateAsync(string connectionStringName = "ConnectionString", SGBDType SGBDType = SGBDType.SqlServer)
        {
            var resultado = (IDataBase)null;

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new NullReferenceException("connectionString");

            if (SGBDType == SGBDType.Configuration)
            {
                var providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

                if (string.IsNullOrWhiteSpace(providerName))
                    throw new NullReferenceException("providerName");

                providerName = providerName.ToLower().Trim();

                if (providerName.Contains("system.data.sqlclient"))
                {
                    resultado = new SqlDataBase(connectionString, false);

                    await resultado.OpenConnectionAsync();
                }
                else if (providerName.Contains("oracle.manageddataaccess"))
                {
                    resultado = new OracleDataBase(connectionString, false);

                    await resultado.OpenConnectionAsync();
                }
                else
                    throw new NotImplementedException("Proveedor de datos no identificado");
            }
            else if (SGBDType == SGBDType.SqlServer)
            {
                resultado = new SqlDataBase(connectionString, false);

                await resultado.OpenConnectionAsync();
            }
            else if (SGBDType == SGBDType.Oracle)
            {
                resultado = new OracleDataBase(connectionString, false);

                await resultado.OpenConnectionAsync();
            }

            return resultado;
        }
    }
}