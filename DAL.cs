using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace Ettermek
{
    public class DAL : IDisposable
    {
        private bool isConnected = false;
        protected SqlConnection sqlConnection;

        // Connection string 
        protected string strSqlConn = @"Data Source=DESKTOP-D2UVI8U;Initial Catalog=Ettermek;Integrated Security=SSPI;TrustServerCertificate=True";

        // Kapcsolat létrehozása
        protected void CreateConnection()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(strSqlConn);
            }
        }

        // Kapcsolat tesztelése
        public bool TestConnection(ref string errorMessage)
        {
            try
            {
                CreateConnection();
                sqlConnection.Open();
                sqlConnection.Close();
                errorMessage = "OK";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        // Kapcsolat megnyitása
        protected void OpenConnection(ref string errMess)
        {
            try
            {
                CreateConnection();
                if (!isConnected)
                {
                    sqlConnection.Open();
                    isConnected = true;
                }
                errMess = "OK";
            }
            catch (SqlException ex)
            {
                errMess = ex.Message;
            }
        }

        // Kapcsolat bezárása
        protected void CloseConnection()
        {
            try
            {
                if (isConnected && sqlConnection != null)
                {
                    sqlConnection.Close();
                    isConnected = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        // DataSet lekérdezés - egyszerű SELECT query
        protected DataSet ExecuteDS(string query, ref string errMess)
        {
            DataSet dataSet = new DataSet();
            try
            {
                OpenConnection(ref errMess);
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection))
                {
                    dataAdapter.Fill(dataSet);
                    errMess = "OK";
                }
            }
            catch (SqlException e)
            {
                errMess = e.Message;
            }
            finally
            {
                CloseConnection();
            }
            return dataSet;
        }

        // DataSet lekérdezés - paraméterezett SELECT query
        protected DataSet ExecuteDS(SqlCommand command, ref string errMess)
        {
            DataSet dataSet = new DataSet();
            try
            {
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataSet);
                    errMess = "OK";
                }
            }
            catch (SqlException e)
            {
                errMess = e.Message;
            }
            finally
            {
                CloseConnection();
            }
            return dataSet;
        }

        // Adatmódosítás - INSERT, UPDATE, DELETE
        protected int ExecuteNonQuery(SqlCommand command, ref string errMess)
        {
            int affectedRows = 0;
            try
            {
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                affectedRows = command.ExecuteNonQuery();
                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;
            }
            finally
            {
                CloseConnection();
            }
            return affectedRows;
        }

        // Adatolvasás - SELECT (SqlDataReader használat)
        protected SqlDataReader ExecuteReader(SqlCommand command, ref string errMess)
        {
            SqlDataReader reader = null;
            try
            {
                OpenConnection(ref errMess);
                command.Connection = sqlConnection;
                reader = command.ExecuteReader();
                errMess = "OK";
            }
            catch (SqlException e)
            {
                errMess = e.Message;
                CloseConnection();
            }
            return reader;
        }

        // Dispose - kapcsolat lezárása és felszabadítása
        public void Dispose()
        {
            if (sqlConnection != null)
            {
                try
                {
                    if (sqlConnection.State != ConnectionState.Closed)
                        sqlConnection.Close();
                }
                catch { }
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }
    }
}