using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Employees.Service
{
    public class DataAccess : IDisposable
    {
        private System.ComponentModel.Component components = new System.ComponentModel.Component();
        private bool disposedValue = false; // To detect redundant calls

        MySqlDataAdapter da;
        MySqlTransaction trans;

        public MySqlConnection conn;
        public MySqlCommand cmd;

        string server = string.Empty,
            db = string.Empty,
            user = string.Empty,
            password = string.Empty,
            connectionString = string.Empty;

        public DataAccess()
        {
            try
            {
                server = Properties.Settings.Default.DB_SERVER;
                db = Properties.Settings.Default.DB;
                user = Properties.Settings.Default.USER;
                password = Properties.Settings.Default.PASSWORD;

                connectionString = $"Server={server}; Database={db}; Username={user}; Password={password}";

                conn = new MySqlConnection(connectionString);

            }
            catch
            {

                throw;
            }
        }

        public void OpenConnection()
        {
            try
            {
                conn.Open();
            }
            catch
            {

                throw;
            }
        }

        public void ExecuteTSQL(string query)
        {
            try
            {
                cmd = new MySqlCommand(query, conn);
            }
            catch
            {

                throw;
            }
        }

        public void CloseConnection()
        {
            try
            {
                conn.Close();
            }
            catch
            {

                throw;
            }
        }

        //T-SQL
        public void BeginTransaction()
        {
            try
            {
                if(conn.State != ConnectionState.Open)
                {
                    CloseConnection();
                    OpenConnection();
                }

                trans = conn.BeginTransaction(IsolationLevel.Serializable);
            }
            catch
            {

                throw;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                trans.Commit();
            }
            catch
            {

                throw;
            }
        }

        public void RollbackTransaction()
        {
            trans.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                try
                {
                    cmd.Dispose();
                    conn.Dispose();

                    if (trans != null)
                        trans.Dispose();
                }
                catch
                {

                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                if (conn != null)
                    MySqlConnection.ClearPool(conn);

                components.Dispose();
            }

            disposedValue = true;
        }

        ~DataAccess()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
    }
}
