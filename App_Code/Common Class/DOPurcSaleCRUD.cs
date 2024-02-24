using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Summary description for DOPurcSaleCRUD
/// </summary>
public class DOPurcSaleCRUD
{
    public static string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
    public static SqlConnection _connection = new SqlConnection();
    private static SqlDataReader _reader = null;

    private static SqlDataAdapter _adapter = null;
    private static SqlCommand _sqlCmd = null;
    private static SqlTransaction _transaction = null;
    public static String timervalue = "";

    public static string DOPurcSaleCRUD_OP(int flag, DataTable dt)
    {
        string msg = string.Empty;
        string qry = string.Empty;
        Stopwatch timer;
        try
        {
            string timerset = "";
            if (OpenConnection())
            {
                timer = new Stopwatch();
                timer.Start();
                _transaction = _connection.BeginTransaction();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //try
                    //{

                    qry = dt.Rows[i]["Querys"].ToString();
                    _sqlCmd = new SqlCommand(qry, _connection, _transaction);
                    _sqlCmd.CommandTimeout = 100;
                    _sqlCmd.ExecuteScalar();
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogError(ex);
                    //}
                }

                _transaction.Commit();

                timervalue = timerset;
                timer.Stop();
                TimeSpan ts = timer.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                // timerset += elapsedTime;
                timerset = elapsedTime;

                if (flag == 1)
                {
                    msg = "Insert";
                }
                else if (flag == 2)
                {
                    msg = "Update";
                }
                else
                {
                    msg = "Delete";
                }

            }
            return msg;
        }
        catch
        {
            _transaction.Rollback();
            return null;
        }
        finally
        {
            _sqlCmd.Dispose();
            _connection.Close();
        }
    }

    public static bool OpenConnection()
    {
        try
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.ConnectionString = strConnectionString;
                _connection.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
          //  clsLog.Publish(ex);
            String strException = ex.Message;
            return false;
        }
    }

    public static void LogError(Exception ex)
    {
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);

        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        string path = @"E:\NewError.txt";
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
        }
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }
    }
}