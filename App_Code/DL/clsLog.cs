using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
public static class clsLog
{
    public static void Publish(Exception ex)
    {
        try
        {
            String strMessage = ex.Message;
            String strInnerException = Convert.ToString(ex.InnerException);
            String strExceptionId = "0";
            String PageName = clsAdvanceUtility.GetCurrentPageName();
            string cs = string.Empty;
            SqlConnection con = null;
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            using (clsDataProvider objDataProvider = new clsDataProvider())
            {
                con.Open();

                SqlCommand sqlcmd = new SqlCommand("SP_Exception", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("ExceptionId", strExceptionId);
                sqlcmd.Parameters.AddWithValue("ExceptionName", strInnerException);
                sqlcmd.Parameters.AddWithValue("ExceptionDetails", strMessage);
                sqlcmd.Parameters.AddWithValue("PageName", PageName);
                sqlcmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch
        {
        }
    }
}