using Nuxiba.TestArch.Data.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Nuxiba.TestArch.Data
{
    public class SqlDataBase : Database, IDataBase, IDisposable
    {
        SqlConnection _connection;
        string _connectionString;

        public override string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public override IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public SqlDataBase()
            : this(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, true)
        { }

        public SqlDataBase(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            _connectionString = connectionString;

            CreateConnection();
        }

        public SqlDataBase(string connectionString, bool openConnection)
            : this(connectionString)
        {
            if (openConnection)
                OpenConnection();
        }

        public override void CreateConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);
        }
        
        public override void CreateOpenConnection()
        {
            CreateConnection();

            OpenConnection();
        }

        public override async Task CreateOpenConnectionAsync()
        {
            CreateConnection();

            await OpenConnectionAsync();
        }

        public void Open()
        {
            OpenConnection();
        }

        public async Task OpenAsync()
        {
            await OpenConnectionAsync();
        }

        public override void OpenConnection()
        {
            ValidateConnection();

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        public override async Task OpenConnectionAsync()
        {
            ValidateConnection();

            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();
        }

        public void Close()
        {
            CloseConnection();
        }

        public override void CloseConnection()
        {
            ValidateConnection();

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public override IDbCommand CreateCommand()
        {
            ValidateConnection();
            
            return _connection.CreateCommand();
        }

        public override IDbCommand CreateCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var command = CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }
        
        public override IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            ValidateParameter(parameterName, parameterValue);

            return new SqlParameter(parameterName, parameterValue);
        }
        
        public override void AddParameter(IDbCommand command, string parameterName, object parameterValue)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            ValidateParameter(parameterName, parameterValue);

            ((SqlCommand)command).Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }

        public override IDbTransaction BeginTransaction()
        {
            ValidateConnection();

            if (_connection.State != ConnectionState.Open)
                throw new DataException("El \"State\" de la conexión no es: \"Open\"");

            return _connection.BeginTransaction();
        }

        public override void Dispose()
        {
            if (_connection != null)
            {
                CloseConnection();

                _connection.Dispose();
                _connection = null;
            }
        }

        private void ValidateConnection()
        {
            if (_connection == null)
                throw new NullReferenceException("_connection");
        }

        private void ValidateParameter(string parameterName, object parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentNullException("parameterName");
            if (parameterValue == null)
                throw new ArgumentNullException("parameterValue");
        }
    }
}