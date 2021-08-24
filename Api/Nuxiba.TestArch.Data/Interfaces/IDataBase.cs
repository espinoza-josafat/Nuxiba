using System;
using System.Data;
using System.Threading.Tasks;

namespace Nuxiba.TestArch.Data.Interfaces
{
    public interface IDataBase : IDisposable
    {
        string ConnectionString { get; }

        IDbConnection Connection { get; }

        void CreateConnection();

        Task CreateOpenConnectionAsync();

        void CreateOpenConnection();

        void OpenConnection();

        Task OpenConnectionAsync();

        void CloseConnection();

        IDbCommand CreateCommand();

        //IDbCommand CreateCommand(string commandText);

        IDbCommand CreateCommand(string commandText, CommandType commandType = CommandType.Text);

        IDataParameter CreateParameter(string parameterName, object parameterValue);

        void AddParameter(IDbCommand command, string parameterName, object parameterValue);

        IDbTransaction BeginTransaction();
    }
}