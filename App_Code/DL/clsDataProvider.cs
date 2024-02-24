using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsAdmin ok
/// </summary>
public class clsDataProvider : IDisposable
{
    #region Public Variable Declaration
    //public SqlConnection ConnMain = new SqlConnection();
    public SqlConnection ConnMain = new SqlConnection();
    private static string ConnetionStringName = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
    #endregion




    public String Get_Single_FieldValue_fromDR(string strSQlQuery)
    {
        //Get Single String Value 
        //SqlCommand cmd1 = new SqlCommand();
        SqlCommand cmd1 = new SqlCommand();
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());

        // SqlDataReader dr1;
        SqlDataReader dr1;
        try
        {
            cmd1.Connection = con1;
            cmd1.CommandText = strSQlQuery;
            cmd1.CommandType = CommandType.Text;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows == true)
            {
                dr1.Read();
                return dr1[0].ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            throw;
        }
        finally
        {
            con1.Close();
            con1.Dispose();
            cmd1.Dispose();
        }
    }


    #region Constructor
    public clsDataProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    ~clsDataProvider()
    {
        this.Dispose();
    }
    public void Dispose()
    {
        //Dispose(true);
        System.GC.SuppressFinalize(this);
    }
    //protected virtual void Dispose(bool disposing)
    //{
    //    try
    //    {
    //        if (disposing == true)
    //        {
    //            ConnMain.Close(); // call close here to close connection
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}


    #endregion

    #region Private Connection
    protected bool OpenConnection()
    {
        try
        {
            if (ConnMain.State != ConnectionState.Open)
            {
                //String Str;
                //Str = 
                ConnMain.ConnectionString = ConnetionStringName;
                ConnMain.Open();
            }

            return true;
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            return false;
        }
    }
    public void CloseConnection()
    {
        try
        {
            if (ConnMain.State == ConnectionState.Open)
            {
                ConnMain.Close();
                ConnMain.Dispose();
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            String str;
            str = e.Message;
        }
    }
    #endregion

    #region Public Sql Methods,Functions
    #region Function

    //public DataSet GetDataSet(String[] SqlStr)
    //{
    //    SqlDataAdapter da;
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        if (OpenConnection())
    //        {
    //            for (int i = 0; i <= SqlStr.Length - 1; i++)
    //            {
    //                SqlCommand cmd = new SqlCommand();
    //                cmd.Connection = ConnMain;
    //                cmd.CommandType = CommandType.Text;
    //                cmd.CommandText = SqlStr.GetValue(i).ToString();
    //                da = new SqlDataAdapter(cmd);
    //                da.Fill(ds, "Tables" + "[" + i + "]");
    //            }
    //            return ds;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        clsLog.Publish(e);
    //        return null;
    //    }
    //    finally
    //    {
    //        CloseConnection();
    //    }
    //}

    public DataSet GetDataSet(string SqlStr)
    {
        // SqlDataAdapter da;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        // SqlCommand cmd = new SqlCommand();
        try
        {
            if (OpenConnection())
            {
                cmd.Connection = ConnMain;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlStr;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            return null;
        }
        finally
        {
            cmd.Dispose();
            ds.Dispose();
            //CloseConnection();
        }
    }

    public DataTable GetDataTable(String SqlStr)
    {
        // SqlDataAdapter da;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlStr;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }

    public DataRow GetDataRow(String SqlStr)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlStr;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0].Rows[0];
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }

    public Int64 GetReturnValuefromqry(String SqlStr)
    {
        Int64 retvalue;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlStr;
                retvalue = Convert.ToInt64(cmd.ExecuteScalar());
                return retvalue;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            string str;
            str = e.Message;
            return 0;
        }
        finally
        {
            CloseConnection();
        }
    }

    public Int64 GetIntValue(String SqlStr, SqlParameter[] param)
    {
        Int64 retvalue;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = SqlStr;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }
                    cmd.Parameters.Add(lclparam);
                }

                retvalue = Convert.ToInt64(cmd.ExecuteScalar());

                return retvalue;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return 0;
        }
        finally
        {
            CloseConnection();
        }
    }

    public string GetString(String SqlStr, SqlParameter[] param)
    {
        string retvalue;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = SqlStr;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }
                    cmd.Parameters.Add(lclparam);
                }

                retvalue = Convert.ToString(cmd.ExecuteScalar());

                return retvalue;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return "";
        }
        finally
        {
            CloseConnection();
        }
    }

    public bool ExecuteDatabyStr(String StrSql)
    {
        //This function return True when sql query excuted ok otherwise false when error or 0 record affected.
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = StrSql;
                cmd.CommandType = CommandType.Text;
                Int32 i;
                i = (Int32)cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }

    public void UpdateAccountStateCodes(string procName, SqlParameter[] param)
    {
        SqlParameter tempparam = new SqlParameter();
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }
                    cmd.Parameters.Add(lclparam);
                }
                //Execute cmd
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            //clsLog.Publish(e);
            string str;
            str = e.ToString();
        }
        finally
        {
            CloseConnection();
        }
    }

    /// <summary>
    /// This function will executes the stored proc and returns you the output parameter value
    /// </summary>
    /// <param name="StrProcName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public long ExecuteStoredProc(String StrProcName, SqlParameter[] param)
    {
        SqlParameter tempparam = new SqlParameter();
        long RetID = 0;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = StrProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }
                    cmd.Parameters.Add(lclparam);
                }

                //Execute cmd
                Int16 j;
                j = (Int16)cmd.ExecuteNonQuery();
                if (j != 0)
                {
                    //Get return value of ouput parameter
                    for (Int32 i = 0; i < cmd.Parameters.Count; i++)
                    {
                        if (cmd.Parameters[i].Direction == ParameterDirection.InputOutput)
                        {
                            tempparam.ParameterName = cmd.Parameters[i].ParameterName;
                            tempparam.Value = cmd.Parameters[i].Value;
                            break;
                        }
                    }

                    if (tempparam.Value != null)
                    {
                        long.TryParse(tempparam.Value.ToString(), out RetID);
                    }
                }
                else
                {
                    RetID = 0;
                }
            }
            else
            {
                RetID = 0;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            string str;
            str = e.ToString();
            //throw;
            RetID = 0;
        }
        finally
        {
            CloseConnection();
        }
        return RetID;
    }



    /// <summary>
    /// Added by Sandeep
    /// </summary>
    /// <param name="PassUserID"></param>
    /// <returns></returns>
    public Int64 ExecuteNonQuery(string str)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        try
        {
            cmd.Connection = con;
            cmd.CommandText = str;
            cmd.CommandType = CommandType.Text;
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            throw;
        }
    }

    public String SetUserUniqueId(Int64 PassUserID)
    {
        if (PassUserID > 0)//When Saving Record
        {
            try
            {
                DataTable dt;
                String GenerateUser;
                String str = "Select ClientID,RoleID,UserId from UserMaster Where UserId =" + PassUserID + "";
                dt = this.GetDataTable(str);
                if (!(dt == null))
                {
                    if (dt.Rows.Count > 0)
                    {
                        GenerateUser = dt.Rows[0].ItemArray.GetValue(0).ToString() + dt.Rows[0].ItemArray.GetValue(1).ToString() + dt.Rows[0].ItemArray.GetValue(2).ToString();
                        str = "Update UserMaster Set UseUniqueCode =" + GenerateUser + " Where  UserId =" + PassUserID + "";
                        if (this.ExecuteDatabyStr(str))
                        {
                            return GenerateUser;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                clsLog.Publish(e);
                return "";
            }
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// This Function is able to call stored procedure and returns he data in data table
    /// </summary>
    /// <param name="StrProcName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public DataTable CallStoredProc(String StrProcName, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        SqlParameter tempparam = new SqlParameter();
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = StrProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }

                    cmd.Parameters.Add(lclparam);
                }

                //Start to make the data table
                SqlDataAdapter adptr = new SqlDataAdapter();
                adptr.SelectCommand = cmd;
                adptr.SelectCommand.Connection = ConnMain;
                DataSet ds = new DataSet();
                adptr.Fill(ds);
                dt = ds.Tables[0];

                ds.Dispose();
                cmd.Dispose();
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            string str;
            str = e.Message;
            return dt;
        }
        finally
        {
            CloseConnection();
        }

        return dt;

    }

    /// <summary>
    /// This function Function will return Single field value(0 index field) only.
    /// </summary>
    /// <param name="StrProcName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public string CallStoredProc_for_SingleField(String StrProcName, SqlParameter[] param)
    {
        string strData = "";
        SqlParameter tempparam = new SqlParameter();
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = StrProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }

                    cmd.Parameters.Add(lclparam);
                }

                //Start to make the data table

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    dr.Read();
                    strData = dr[0].ToString();
                }
                else
                {
                    strData = "";
                }

            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            string str;
            str = e.Message;
            return strData;
        }
        finally
        {
            CloseConnection();
        }

        return strData;

    }

    /*public DataRow GetStoredProcDataRow(string cmdText, object[,] cmdParms)
    {
        DataSet ds =null;
        if (OpenConnection())
        {
            SqlCommand cmd = new SqlCommand(cmdText, ConnMain);
            for (int i = 0; i < cmdParams.Length / 2; i++)
            {
                oParameter = cmd.CreateParameter();
                oParameter.ParameterName = cmdParms[i, 0].ToString();
                oParameter.Value = cmdParms[i, 1];
                cmd.Parameters.Add(oParameter);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            else
                return null;
        }
    }*/

    public DataRow GetDataRowFromSpWithoutParam(String SqlStr)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = SqlStr;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }
    public DataRow GetDataRowFromSpWithParam(String StrProcName, SqlParameter[] param)
    {
        SqlParameter tempparam = new SqlParameter();
        DataRow drow = null;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = StrProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }

                    cmd.Parameters.Add(lclparam);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        drow = ds.Tables[0].Rows[0];
                    }
                    else
                    {
                        drow = null;
                    }
                }
                else
                {
                    drow = null;
                }

                ds.Dispose();



            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            drow = null;
        }
        finally
        {
            CloseConnection();
        }
        return drow;
    }

    public DataTable GetDataTableFromSpWithParam(String StrProcName, SqlParameter[] param)
    {
        SqlParameter tempparam = new SqlParameter();
        DataTable dTable = null;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = StrProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters in Command objects
                for (Int32 i = 0; i < param.Length - 1; i++)
                {
                    SqlParameter lclparam = new SqlParameter();
                    lclparam.Direction = param[i].Direction;
                    lclparam.ParameterName = param[i].ParameterName;
                    lclparam.Value = param[i].Value;
                    if (param[i].Direction == ParameterDirection.InputOutput)
                    {
                        lclparam.SqlDbType = param[i].SqlDbType;
                    }
                    cmd.Parameters.Add(lclparam);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    dTable = ds.Tables[0];
                }
                else
                {
                    dTable = null;
                }
                ds.Dispose();
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            dTable = null;
        }
        finally
        {
            CloseConnection();
        }
        return dTable;
    }

    public DataTable GetDataTableFromSpWithoutParam(String StrProcName)
    {
        SqlParameter tempparam = new SqlParameter();
        DataTable dTable = null;
        try
        {
            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ConnMain;
                cmd.CommandText = StrProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    dTable = ds.Tables[0];
                }
                else
                {
                    dTable = null;
                }
                ds.Dispose();
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            dTable = null;
        }
        finally
        {
            CloseConnection();
        }
        return dTable;
    }

    public DataTable SortDataTable(DataTable dtTable, int sortOrder, string columnKey)
    {
        if (dtTable == null || dtTable.Rows.Count == 0)
        {
            return dtTable;
        }

        DataSet dsSorted = new DataSet();
        string sortDirection = "";

        switch (sortOrder)
        {

            case 0:

                sortDirection = "ASC";

                break;

            case 1:

                sortDirection = "DESC";

                break;

            default:

                sortDirection = "ASC";

                break;

        }

        DataView dv = dtTable.DefaultView;
        dv.Sort = columnKey + "  " + sortDirection;
        dtTable = null;
        return dtTable = dv.ToTable();

    }

    #endregion

    #endregion
}


