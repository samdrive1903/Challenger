using Microsoft.Data.SqlClient;
using System.Data;

namespace ChatAppWebRepository
{
    public class SqlConnectionHelper : IDbConnection
    {
        public SqlConnection conn;

        public SqlConnectionHelper(string connectionString)
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
        }
        public string ConnectionString
        {
            get => ((IDbConnection)conn).ConnectionString;
            set => ((IDbConnection)conn).ConnectionString = value;
        }

        public int ConnectionTimeout => ((IDbConnection)conn).ConnectionTimeout;

        public string Database
        {
            get => ((IDbConnection)conn).Database;
        }

        public ConnectionState State
        {
            get => ((IDbConnection)conn).State;
        }

        public IDbTransaction BeginTransaction() => ((IDbConnection)conn).BeginTransaction();

        public IDbTransaction BeginTransaction(IsolationLevel il) => ((IDbConnection)conn).BeginTransaction(il);

        public void ChangeDatabase(string databaseName) => ((IDbConnection)conn).ChangeDatabase(databaseName);

        public void Close()
        {
            ((IDbConnection)conn).Close();
        }

        public IDbCommand CreateCommand()
        {
            return ((IDbConnection)conn).CreateCommand();
        }

        public void Dispose()
        {
            conn.Dispose();
        }

        public void Open()
        {
            ((IDbConnection)conn).Open();
        }
    }
}
