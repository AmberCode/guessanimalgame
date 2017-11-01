using System;
using System.Data.SqlClient;

namespace Shared
{
    public class SqlDbManager
    {
        private string _connectionString = string.Empty;

        public SqlConnection SqlConnection { get; private set; }

        public SqlDbManager(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void OpenConnection()
        {
            if (this.SqlConnection == null)
            {
                this.SqlConnection = new SqlConnection(this._connectionString);
            }

            if (this.SqlConnection.State != System.Data.ConnectionState.Open)
            {

                try
                {
                    this.SqlConnection.Open();
                }
                catch (SqlException ex)
                {
                    //TODO: log ex
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void CloseConnection()
        {
            try
            {
                this.SqlConnection.Close();
            }
            catch (SqlException ex)
            {
                //TODO: log err
                Console.WriteLine(ex.Message);
            }
        }

        public string ConnectionString { get { return this._connectionString; } }
    }
}
