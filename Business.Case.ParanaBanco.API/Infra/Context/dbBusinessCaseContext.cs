using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Business.Case.ParanaBanco.API.Domain.Static;

namespace Business.Case.ParanaBanco.API.Infra.Context
{
    public class dbBusinessCaseContext
    {
        //public IDbConnection Connection => new SqlConnection(RuntimeConfig.ConnectStringSqlServer);
        private readonly IDbConnection connection;
        public IDbConnection Connection => connection;
        public dbBusinessCaseContext(IDbConnection connection)
        {
            this.connection = connection;
            OpenConnectionService(connection);
        }


        private IDbConnection OpenConnectionService(IDbConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return conn;
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
