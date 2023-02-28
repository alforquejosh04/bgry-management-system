using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Database
{
    public class database : IDisposable
    {
        public SqlConnection conn = new SqlConnection();
       string MyConn = ConfigurationManager.ConnectionStrings["MyConn"].ToString();
      // String MyConn = System.Configuration.ConfigurationManager.AppSettings["MyConn"];

        public database()
        {
          
            conn = new SqlConnection(MyConn);
            conn.Open();
        }

        public void close()
        {
            conn.Dispose();
            conn.Close();
        }
        public DataSet executeStatement(String sql)
        {
            try
            {
                DataSet data = new DataSet();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(data);
                command.Dispose();
                adapter.Dispose();
                return data;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error running query: " + sql, ex);
            }
        }

        public DataSet executeStatement(String sql, Hashtable parameters)
        {
            try
            {
                DataSet data = new DataSet();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter adpt = new SqlDataAdapter();
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
                command.CommandTimeout = 3000;
                adpt.SelectCommand = command;
                adpt.Fill(data);
                command.Dispose();
                adpt.Dispose();

                return data;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error running query: " + sql, ex);
            }
  

        }
        public string ExecuteNonQueryWRollbackReader(String sCmd, Hashtable parameters)
        {
          
                 SqlConnection myConnection = conn;
                 SqlCommand command = new SqlCommand();           
                 SqlTransaction myTrans;
                 myConnection.Open();
                 command.Connection = myConnection;
                 myTrans = myConnection.BeginTransaction();
                 String _msg = "";
            try
            {
        
      
                command.Transaction = myTrans;
                command.CommandTimeout = 2000;
                command.CommandText = sCmd;
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }

                command.ExecuteNonQuery();
                myTrans.Commit();


            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                //_msg = ex.Message.ToString();
                _msg = "Error";
            }
            finally
            {
                command.Dispose();
                myTrans.Dispose();
                myConnection.Close();
            }
            return _msg;
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}