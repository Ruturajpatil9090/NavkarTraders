//RAHUL 8APR21:connection lifetime=120 connection discoonect lifetime in sec.
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;


/// <summary>
/// Summary description for clsDAL
/// </summary>
public class clsDAL : IDisposable
{
    #region --------------------------------- Declaration ---------------------------------

    public static string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

    // public static SqlConnection _connection = new SqlConnection();
    private static DataSet _ds = null;
    private static DataTable _dt = null;
    //private static SqlDataReader _reader = null;

    //private static SqlDataAdapter _adapter = null;
    //private static SqlCommand _sqlCmd = null;
    //private static SqlTransaction _transaction = null;
    private static string _getQuery = string.Empty;
    public static bool _transCheck = false;
    public static string _Query = string.Empty;
    private static string Head_Insert = string.Empty;
    private static string Fields = string.Empty;
    private static string Detail_Fields = string.Empty;
    private static string Detail_Values = string.Empty;
    private static string Detail_Insert = string.Empty;
    private static int PurchaseDetailId = 0;

    #region Purchase Detail Fields
    private static Int32 item_code = 0;
    private static string narration = "";
    private static double Quantal = 0.00;
    private static int packing = 0;
    private static int bags = 0;
    private static double rate = 0.00;
    private static double item_Amount = 0.00;
    private static string i_d = string.Empty;
    #endregion
    //mysql command
    public static SqlConnection _connection = new SqlConnection();
    private static SqlDataReader _reader = null;

    private static SqlDataAdapter _adapter = null;
    private static SqlCommand _sqlCmd = null;
    private static SqlTransaction _transaction = null;

    #endregion

    #region --------------------------------- Constructor ------------------------------------------

    public clsDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    ~clsDAL()
    {
        this.Dispose();
    }

    public void Dispose()
    {
        //  Dispose(true);
        GC.WaitForPendingFinalizers();
        System.GC.SuppressFinalize(this);
    }

    //protected virtual void Dispose(bool disposing)
    //{
    //    try
    //    {
    //        if (disposing == true)
    //        {
    //            _connection.Close(); // call close here to close connection
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}

    #endregion

    #region ------------------------------- Private Connection -------------------------------------

    /// <summary>
    /// OPEN THE SPECIFIED CONNECTION
    /// </summary>
    /// <returns></returns>
    /// 

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
            // DOPurcSaleCRUD.LogError(ex);
            //clsNoToWord n = new clsNoToWord();
            //n.WriteToFile(ex.ToString());
            // return "";
            //clsLog.Publish(ex);
            String strException = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// CLOSE THE CONNECTION IF OPENED
    /// </summary>
    public static void CloseConnection()
    {
        try
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        catch (Exception e)
        {
            String str;
            str = e.Message;
            //clsException.Publish(e);
        }
    }

    #endregion

    #region --------------------------------- Private Methods ---------------------------------

    /// <summary>
    /// Fill Data Set
    /// </summary>
    /// <param name="strProcedureName">Procudure Name</param>
    /// <returns>Data Set</returns>
    public static DataSet FillDataSet(string strProcedureName)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(_sqlCmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            _sqlCmd.Dispose();
            _adapter.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
        return _ds;
    }
    public static DataSet xmlExecuteDMLQryReport1(string strProcedureName, string Xmlfile)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;

                //_sqlCmd.Parameters.AddWithValue("@company_code", company_code).ToString();
                //_sqlCmd.Parameters.AddWithValue("@year_Code", year_Code).ToString();
                //_sqlCmd.Parameters.AddWithValue("@From_Date", From_Date).ToString();
                //_sqlCmd.Parameters.AddWithValue("@To_Date", To_Date).ToString();
                //SqlParameter outParameter = new SqlParameter("@Msg", SqlDbType.VarChar, 255); //New Update
                //outParameter.Direction = ParameterDirection.InputOutput;
                //outParameter.Value = "";
                //_sqlCmd.Parameters.Add(outParameter);
                _sqlCmd.Parameters.AddWithValue("@xmlDocument", Xmlfile).ToString();
                _adapter = new SqlDataAdapter(_sqlCmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                //Return_Dataset = _sqlCmd.Parameters["@Return_Dataset"].Value.ToString();
                return _ds;

                //_sqlCmd = new SqlCommand(str, _connection);
                //_adapter = new SqlDataAdapter(_sqlCmd);
                //_ds = new DataSet();
                //_adapter.Fill(_ds);
                //return _ds;


            }
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            //output = _sqlCmd.Parameters["@Msg"].Value.ToString();
            throw;
            //return null;
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return _ds;
    }
    public static string AuthenticateUser(string strProcedure, string Username, string Password, string msg)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedure, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramUsername = new SqlParameter("@User_name", Username);
                SqlParameter paramPassword = new SqlParameter("@Password", Password);
                _sqlCmd.Parameters.Add(paramUsername);
                _sqlCmd.Parameters.Add(paramPassword);

                SqlDataReader rdr = _sqlCmd.ExecuteReader();
                while (rdr.Read())
                {
                    int RetryAttempts = Convert.ToInt32(rdr["RetryAttempts"]);
                    if (Convert.ToBoolean(rdr["AccountLocked"]))
                    {
                        msg = "Account has locked. Please contact administrator";
                    }
                    else if (RetryAttempts > 0)
                    {
                        int AttemptsLeft = (4 - RetryAttempts);
                        msg = "Invalid user name and/or password. " +
                            AttemptsLeft.ToString() + "attempt(s) left";
                    }
                    else if (Convert.ToBoolean(rdr["Authenticated"]))
                    {
                        msg = "1";
                    }
                }
                rdr.Close();
            }
        }
        catch
        {
        }
        return msg;
    }


    #region[Database's simple Query]
    public static DataSet SimpleQuery1(string str)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(str, _connection);
                _sqlCmd.CommandTimeout = 100;
                _adapter = new SqlDataAdapter(_sqlCmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                return _ds;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return null;
        }
        finally
        {
            _ds.Dispose();
            _sqlCmd.Dispose();
            _adapter.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
    #endregion

    #region[Database's simple Query]
    public static DataSet SimpleQuery(string str)
    {
        try
        {
            using (SqlConnection _connection = new SqlConnection(strConnectionString))
            {
                SqlCommand _sqlCmd = new SqlCommand(str, _connection);
                _sqlCmd.CommandTimeout = 100;
                SqlDataAdapter _adapter = new SqlDataAdapter(_sqlCmd);
                DataSet _ds = new DataSet();
                _adapter.Fill(_ds);
                return _ds;
            }
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return null;
        }
        finally
        {
            ////_ds.Dispose();
            // _sqlCmd.Dispose();
            // _adapter.Dispose();
            //  _connection.Close();
            // _connection.Dispose();
        }
    }
    #endregion

    public String Get_Single_FieldValue_fromDR(string strSQlQuery)
    {


        try
        {
            SqlDataReader dr1;

            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strSQlQuery, _connection);

                dr1 = _sqlCmd.ExecuteReader();
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
            //_connection.Close();
            //_connection.Dispose();
            //_connection.Dispose();

            //_ds.Dispose();
            //_sqlCmd.Dispose();
            //_adapter.Dispose();


            //_sqlCmd.Dispose();


            //con1.Close();
            //con1.Dispose();
            //cmd1.Dispose();
        }
    }


    #region single string

    public static string GetString(string qry)
    {
        string returnString = string.Empty;
        try
        {

            if (OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = qry;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = _connection;
                var retValue = cmd.ExecuteScalar();
                returnString = Convert.ToString(retValue);
            }
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            throw;
        }

        return returnString;
    }

    #endregion


    /// <summary>
    /// Fill Data Set
    /// </summary>
    /// <param name="strProcedureName"></param>
    /// <param name="hshtblCollection"></param>
    /// <returns>Data Set</returns>
    /// 
    public static DataSet FillDataSet(string strProcedureName, Hashtable hshtblCollection)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                IDictionaryEnumerator _enumerator = hshtblCollection.GetEnumerator();
                while (_enumerator.MoveNext())
                {
                    _sqlCmd.Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                }
                //SqlParameter outParameter = new SqlParameter("@vchrMsg", SqlDbType.VarChar, 255); //New Update
                // outParameter.Direction = ParameterDirection.Output;
                // outParameter.Value = "";
                // _sqlCmd.Parameters.Add(outParameter);
                _adapter = new SqlDataAdapter(_sqlCmd);
                _ds = new DataSet();
                if (_adapter != null)
                {
                    _adapter.Fill(_ds);
                }

                return _ds;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            _ds.Dispose();
            _sqlCmd.Dispose();
            _adapter.Dispose();
            CloseConnection();
        }

    }

    /// <summary>
    /// Fill Data Table 
    /// </summary> 
    /// <param name="strProcedureName">Procedure Name</param>
    /// <returns>Data Table</returns>
    /// 
    public static DataTable FillDataTable(string strProcedureName)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(_sqlCmd);
                _dt = new DataTable();
                _adapter.Fill(_dt);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        finally
        {
            _sqlCmd.Dispose();
            _adapter.Dispose();
            CloseConnection();
        }
        return _dt;
    }
    /// <summary>
    /// Fill Data Table 
    /// </summary> 
    /// <param name="strProcedureName">Procedure Name</param>
    /// <param name="hshtblCollection">Hash Table</param>
    /// <returns>Data Table</returns>

    public static DataTable FillDataTable(string strProcedureName, Hashtable hshtblCollection)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);

                _sqlCmd.CommandType = CommandType.StoredProcedure;
                IDictionaryEnumerator _enumerator = hshtblCollection.GetEnumerator();
                while (_enumerator.MoveNext())
                {
                    _sqlCmd.Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                }
                //SqlParameter outParameter = new SqlParameter("@vchrMsg", SqlDbType.VarChar, 200); //New Update
                //outParameter.Direction = ParameterDirection.Output;
                //outParameter.Value = "";
                //_sqlCmd.Parameters.Add(outParameter);
                //_sqlCmd.ExecuteNonQuery();
                _adapter = new SqlDataAdapter(_sqlCmd);
                _dt = new DataTable();
                _adapter.Fill(_dt);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            _sqlCmd.Dispose();
            _adapter.Dispose();
            CloseConnection();
        }
        return _dt;
    }

    /// <summary>
    /// It Executes The Insert/Update/Delete Query.
    /// </summary>
    /// <param name="strProcedureName">Procedure Name</param>
    /// <returns>It Returns Flase for The Exception Else true </returns>

    public static bool ExecuteDMLQuery(string strProcedureName)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.Transaction = _transaction;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(_sqlCmd);
                //  _sqlCmd.ExecuteNonQuery();

            }
        }
        catch (Exception ex)
        {

            throw;
            return false;
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return true;
    }

    /// <summary>
    /// It Executes The Insert/Update/Delete Query.
    /// </summary>
    /// <param name="strProcedureName"> Procedure Name</param>
    /// <param name="hshtblCollection">Hashtable</param>
    /// <returns>It Returns Flase for The Exception Else true </returns>

    public static bool ExecuteDMLQuery(string strProcedureName, Hashtable hshtblCollection)
    {
        try
        {

            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);

                _sqlCmd.CommandType = CommandType.StoredProcedure;
                IDictionaryEnumerator _enumerator = hshtblCollection.GetEnumerator();
                while (_enumerator.MoveNext())
                {
                    _sqlCmd.Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                }
                _adapter = new SqlDataAdapter(_sqlCmd);
                //  _sqlCmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw;
            return false;
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return true;
    }

    /// <summary>
    /// It Executes The Insert/Update/Delete Query.
    /// </summary>
    /// <param name="strProcedureName"> Procedure Name</param>
    /// <param name="hshtblCollection">Hashtable</param>
    /// <returns>It Returns Object</returns>

    public static object ExecuteDMLQuery(Hashtable hshtblCollection, string strProcedureName)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                IDictionaryEnumerator _enumerator = hshtblCollection.GetEnumerator();
                while (_enumerator.MoveNext())
                {
                    _sqlCmd.Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                }
                SqlParameter outParameter = new SqlParameter("@vchrMsg", SqlDbType.VarChar, 255); //New Update
                outParameter.Direction = ParameterDirection.InputOutput;
                outParameter.Value = "";
                _sqlCmd.Parameters.Add(outParameter);
                _adapter = new SqlDataAdapter(_sqlCmd);
                //  _sqlCmd.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            throw;
            return _sqlCmd.Parameters["@vchrMsg"].Value;
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return _sqlCmd.Parameters["@vchrMsg"].Value;
    }

    /// <summary>
    /// It Executes The Insert/Update/Delete Query.
    /// </summary>
    /// <param name="strProcedureName"> Procedure Name</param>
    /// <param name="hshtblCollection">Hashtable</param>
    /// <returns>It Returns Object</returns>


    //output return

    public static DataSet xmlExecuteDMLQry(string strProcedureName, string Xmlfile, ref string output, int flag, ref string returnmaxno)
    {
        DataSet _ds = null;
        try
        {
            using (SqlConnection _connection = new SqlConnection(strConnectionString))
            {
                using (SqlCommand _sqlCmd = new SqlCommand(strProcedureName, _connection))
                {
                    //_sqlCmd = new SqlCommand(strProcedureName, _connection);
                    _sqlCmd.CommandType = CommandType.StoredProcedure;
                    _sqlCmd.CommandTimeout = 40;

                    _sqlCmd.Parameters.AddWithValue("@xmlDocument", Xmlfile).ToString();
                    _sqlCmd.Parameters.AddWithValue("@flag", flag);

                    SqlParameter outParameter = new SqlParameter("@Msg", SqlDbType.VarChar, 255); //New Update

                    SqlParameter outParameter1 = new SqlParameter("@returnmax", SqlDbType.VarChar, 255);
                    outParameter.Direction = ParameterDirection.InputOutput;
                    outParameter.Value = "";
                    _sqlCmd.Parameters.Add(outParameter);

                    outParameter1.Direction = ParameterDirection.InputOutput;
                    outParameter1.Value = "";
                    _sqlCmd.Parameters.Add(outParameter1);
                    using (SqlDataAdapter _adapter = new SqlDataAdapter(_sqlCmd))
                    {
                        //_adapter = new SqlDataAdapter(_sqlCmd);
                        _ds = new DataSet();
                        _adapter.Fill(_ds);
                    }
                    output = _sqlCmd.Parameters["@Msg"].Value.ToString();
                    returnmaxno = _sqlCmd.Parameters["@returnmax"].Value.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // DOPurcSaleCRUD.LogError(ex);
            output = _sqlCmd.Parameters["@Msg"].Value.ToString();
            throw;
            // return null;
        }
        finally
        {
            //_sqlCmd.Dispose();
            //CloseConnection();
        }
        return _ds;
    }

    public static DataSet ExecuteDMLQuery(Hashtable hshtblCollection, string strProcedureName, ref string output)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                IDictionaryEnumerator _enumerator = hshtblCollection.GetEnumerator();
                while (_enumerator.MoveNext())
                {
                    _sqlCmd.Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                }
                SqlParameter outParameter = new SqlParameter("@Msg", SqlDbType.VarChar, 255); //New Update
                outParameter.Direction = ParameterDirection.InputOutput;
                outParameter.Value = "";
                _sqlCmd.Parameters.Add(outParameter);
                _adapter = new SqlDataAdapter(_sqlCmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                output = _sqlCmd.Parameters["@Msg"].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            output = _sqlCmd.Parameters["@Msg"].Value.ToString();
            throw;
            return null;
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return _ds;
    }

    public static DataSet ExecuteDMLQuery_Date(string frm_date, string to_date, string strProcedureName, ref string output)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(strProcedureName, _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;

                _sqlCmd.Parameters.AddWithValue("@FromDate", frm_date);
                _sqlCmd.Parameters.AddWithValue("@ToDate", to_date);

                SqlParameter outParameter = new SqlParameter("@Msg", SqlDbType.VarChar, 255); //New Update
                outParameter.Direction = ParameterDirection.InputOutput;
                outParameter.Value = "";
                _sqlCmd.Parameters.Add(outParameter);
                _adapter = new SqlDataAdapter(_sqlCmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                output = _sqlCmd.Parameters["@Msg"].Value.ToString();
                return _ds;
            }
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            output = _sqlCmd.Parameters["@Msg"].Value.ToString();
            throw;
            return null;
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return _ds;
    }

    //public static DataSet xmlExecuteDMLQry(string strProcedureName, string Xmlfile, ref string output, int flag, ref string returnmaxno)
    //{
    //    try
    //    {
    //        if (OpenConnection())
    //        {
    //            _sqlCmd = new SqlCommand(strProcedureName, _connection);
    //            _sqlCmd.CommandType = CommandType.StoredProcedure;
    //            _sqlCmd.CommandTimeout = 100;
    //            _sqlCmd.Parameters.AddWithValue("@xmlDocument", Xmlfile).ToString();
    //            _sqlCmd.Parameters.AddWithValue("@flag", flag);

    //            SqlParameter outParameter = new SqlParameter("@Msg", SqlDbType.VarChar, 255); //New Update

    //            SqlParameter outParameter1 = new SqlParameter("@returnmax", SqlDbType.VarChar, 255);
    //            outParameter.Direction = ParameterDirection.InputOutput;
    //            outParameter.Value = "";
    //            _sqlCmd.Parameters.Add(outParameter);

    //            outParameter1.Direction = ParameterDirection.InputOutput;
    //            outParameter1.Value = "";
    //            _sqlCmd.Parameters.Add(outParameter1);
    //            _adapter = new SqlDataAdapter(_sqlCmd);
    //            _ds = new DataSet();
    //            _adapter.Fill(_ds);
    //            output = _sqlCmd.Parameters["@Msg"].Value.ToString();
    //            returnmaxno = _sqlCmd.Parameters["@returnmax"].Value.ToString();
    //            return _ds;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        clsNoToWord n = new clsNoToWord();
    //        n.WriteToFile(ex.ToString());
    //        output = _sqlCmd.Parameters["@Msg"].Value.ToString();
    //        throw;
    //        return null;
    //    }
    //    finally
    //    {
    //        _sqlCmd.Dispose();
    //        CloseConnection();
    //    }
    //    return _ds;
    //}


    /// <summary>
    /// Transaction with sql command and bulk copy of datatable
    /// </summary>
    /// <param name="TableList">Array Of DataTable </param>
    /// <param name="strProcedureName">Array Of Procedure Name</param>
    /// /// <param name="hshtblCollection">Array Of Hash Table</param>
    /// <returns>boolean </returns>

    public static object _objTransCheck = null;

    public static object ExecuteTransact(DataTable[] TableList, string[] strProcedureName, Hashtable[] hshtblCollection)
    {
        SqlCommand[] sqlCommand = new SqlCommand[strProcedureName.Length];
        try
        {
            if (OpenConnection())
            {
                //Start a local transaction.
                _transaction = _connection.BeginTransaction();
                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                //  SqlBulkCopy blkCpy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.Default, _transaction);

                for (int i = 0; i < strProcedureName.Length; i++)
                {
                    if (strProcedureName[i] != null && strProcedureName[i] != "")
                    {
                        sqlCommand[i] = new SqlCommand(strProcedureName[i], _connection);
                        sqlCommand[i].Transaction = _transaction;
                        sqlCommand[i].CommandType = CommandType.StoredProcedure;
                        if (hshtblCollection[i] != null)
                        {
                            IDictionaryEnumerator _enumerator = hshtblCollection[i].GetEnumerator();
                            while (_enumerator.MoveNext())
                            {
                                sqlCommand[i].Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                            }
                        }
                        SqlParameter outParameter = new SqlParameter("@vchrMsg", SqlDbType.VarChar, 200); //New Update
                        outParameter.Direction = ParameterDirection.Output;
                        outParameter.Value = "";
                        sqlCommand[i].Parameters.Add(outParameter);
                        sqlCommand[i].ExecuteNonQuery();
                        _objTransCheck = sqlCommand[i].Parameters["@vchrMsg"].Value;
                        //sqlCommand[i].ExecuteNonQuery();
                    }
                }
                if (TableList != null && _objTransCheck.ToString().Equals("SUCCESS"))//string.IsNullOrEmpty(_objTransCheck.ToString())
                    for (int t = 0; t < TableList.Length; t++)
                    {
                        if (TableList[t] != null && TableList[t].Rows.Count >= 1)
                        {
                            //blkCpy.DestinationTableName = TableList[t].TableName;
                            //blkCpy.WriteToServer(TableList[t]);
                        }
                    }
                //if (string.IsNullOrEmpty(_objTransCheck.ToString())) _transaction.Commit();
                if (_objTransCheck.ToString().Equals("SUCCESS")) _transaction.Commit();
            }
        }
        catch (Exception ex)
        {
            _transaction.Rollback();
            throw;
            return _objTransCheck;
        }
        finally
        {
            CloseConnection();
        }
        return _objTransCheck;
    }

    public static string ExcecuteAll(List<object> tableList, string strProc, string output)
    {
        //try
        //{
        SqlTransaction tran = null;
        if (OpenConnection())
        {
            tran = _connection.BeginTransaction();
            try
            {
                for (int i = 0; i < tableList.Count; i++)
                {
                    Hashtable hash = (Hashtable)tableList[i];
                    _sqlCmd = new SqlCommand(strProc, _connection);
                    _sqlCmd.Transaction = tran;
                    _sqlCmd.CommandType = CommandType.StoredProcedure;
                    IDictionaryEnumerator _enumerator = hash.GetEnumerator();
                    while (_enumerator.MoveNext())
                    {
                        _sqlCmd.Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                    }
                    SqlParameter outParameter = new SqlParameter("@Msg", SqlDbType.VarChar, 255); //New Update
                    outParameter.Direction = ParameterDirection.InputOutput;
                    outParameter.Value = "";
                    _sqlCmd.Parameters.Add(outParameter);
                    _adapter = new SqlDataAdapter(_sqlCmd);
                    _ds = new DataSet();
                    _adapter.Fill(_ds);
                    output = _sqlCmd.Parameters["@Msg"].Value.ToString();
                }
                tran.Commit();
                output = "Success";
            }
            catch (Exception)
            {
                output = "Error";
                tran.Rollback();
            }
            finally
            {
                _connection.Close();
            }
        }
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        return output;
    }


    /// <summary>
    /// Transaction with sql command and bulk copy of datatable
    /// </summary>
    /// <param name="TableList">Array Of DataTable </param>
    /// <param name="strProcedureName">Array Of Procedure Name</param>
    /// /// <param name="hshtblCollection">Array Of Hash Table</param>
    /// <returns>boolean </returns>

    public static bool ExecuteTransact(string[] strProcedureName, Hashtable[] hshtblCollection, DataTable[] TableList)
    {
        SqlCommand[] sqlCommand = new SqlCommand[strProcedureName.Length];
        try
        {
            if (OpenConnection())
            {
                // Start a local transaction.
                _transaction = _connection.BeginTransaction();
                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                //  SqlBulkCopy blkCpy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.Default, _transaction);


                for (int i = 0; i < strProcedureName.Length; i++)
                {
                    if (strProcedureName[i] != null && strProcedureName[i] != "")
                    {
                        sqlCommand[i] = new SqlCommand(strProcedureName[i], _connection);
                        sqlCommand[i].Transaction = _transaction;
                        sqlCommand[i].CommandType = CommandType.StoredProcedure;
                        if (hshtblCollection[i] != null)
                        {
                            IDictionaryEnumerator _enumerator = hshtblCollection[i].GetEnumerator();
                            while (_enumerator.MoveNext())
                            {
                                sqlCommand[i].Parameters.AddWithValue(_enumerator.Key.ToString(), _enumerator.Value);
                            }
                        }
                        sqlCommand[i].ExecuteNonQuery();
                    }
                }
                if (TableList != null)
                    for (int t = 0; t < TableList.Length; t++)
                    {
                        if (TableList[t] != null && TableList[t].Rows.Count >= 1)
                        {
                            // blkCpy.DestinationTableName = TableList[t].TableName;
                            // blkCpy.WriteToServer(TableList[t]);
                        }
                    }
                _transaction.Commit();
                _transCheck = true;
            }
        }
        catch (Exception ex)
        {
            _transaction.Rollback();
            throw;
            _transCheck = false;
        }
        finally
        {
            CloseConnection();
        }
        return _transCheck;
    }

    public static string ExecuteDelete(string query)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(query, _connection);
                _sqlCmd.CommandType = CommandType.Text;
                if (OpenConnection())
                {
                    var i = _sqlCmd.ExecuteNonQuery();
                }
                return "S";
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {
            return "F";
        }
        finally
        {
            CloseConnection();
        }
    }


    public static string ExecuteSP(string TableName, string ColumnName, int OldAcCode, int NewAcCode, string WhereCondition)
    {
        string result = string.Empty;
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand("spClubAccount", _connection);
                _sqlCmd.Transaction = _transaction;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.AddWithValue("@TableName", TableName);
                string columnNames = ColumnName + "=" + NewAcCode + " where " + ColumnName + "=" + OldAcCode + " and " + WhereCondition;
                _sqlCmd.Parameters.AddWithValue("@ColumnName", columnNames);
                _sqlCmd.ExecuteNonQuery();
                result = "1";
            }
        }
        catch (Exception ex)
        {
            result = "0";
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
        return result;
    }

    public static void ExecuteMergedAccountsSP(string RightAccountCode, string WrongAccountCode, string Created_By, string RightAccountFilePath, string WrongAccountFilePath, string Company_Code, string Year_Code)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand("spInsertMergedAccount", _connection);
                _sqlCmd.Transaction = _transaction;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.AddWithValue("@RightAccountCode", RightAccountCode);
                _sqlCmd.Parameters.AddWithValue("@WrongAccountCode", WrongAccountCode);
                _sqlCmd.Parameters.AddWithValue("@Created_By", Created_By);
                _sqlCmd.Parameters.AddWithValue("@RightAccountFilePath", RightAccountFilePath);
                _sqlCmd.Parameters.AddWithValue("@WrongAccountFilePath", WrongAccountFilePath);
                _sqlCmd.Parameters.AddWithValue("@Company_Code", Company_Code);
                _sqlCmd.Parameters.AddWithValue("@Year_Code", Year_Code);
                _sqlCmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            _sqlCmd.Dispose();
            CloseConnection();
        }
    }
    #endregion



    public static int Read(string qry)
    {
        int Message = 0;
        try
        {

            _transaction = _connection.BeginTransaction();
            //cmd.CommandText = qry;
            //cmd.Connection = con;
            //cmd.Transaction = _transaction;
            _sqlCmd = new SqlCommand(_Query, _connection, _transaction);
            _sqlCmd.ExecuteNonQuery();
            Thread.Sleep(100);
            _transaction.Commit();
            Message = 1;


            return Message;
        }
        catch
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);

            }
            return Message;

        }
        finally
        {
            _connection.Close();
        }
    }
    public static SqlDataReader ReadData(string str)
    {
        try
        {
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand(str, _connection);
                _sqlCmd.CommandTimeout = 100;
                _reader = _sqlCmd.ExecuteReader();
                //_ds = new DataSet();
                //_adapter.Fill(_ds);
                return _reader;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return null;
        }
        finally
        {
            //_ds.Dispose();
            _sqlCmd.Dispose();
            // _adapter.Dispose();
            //_connection.Close();
            //_connection.Dispose();
        }
    }

    public static SqlConnection NewOpenConnection()
    {
        try
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.ConnectionString = strConnectionString;
                _connection.Open();
            }
            return _connection;
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            String strException = ex.Message;
            return null;
        }
    }


    //public static string CommonInsert(int flag, PurchaseFields purchase, string Type, DataTable dt)
    //{
    //    string Msg = string.Empty;
    //    try
    //    {
    //        if (flag == 1)
    //        {
    //            #region Head Table Column
    //            Fields = "doc_no,";
    //            Fields = Fields + "Tran_Type,";
    //            Fields = Fields + "PURCNO,";
    //            Fields = Fields + " doc_date,";
    //            Fields = Fields + " Ac_Code ,";
    //            Fields = Fields + " Unit_Code,";
    //            Fields = Fields + " mill_code,";
    //            Fields = Fields + " FROM_STATION,";
    //            Fields = Fields + " TO_STATION,";
    //            Fields = Fields + " LORRYNO,";
    //            Fields = Fields + "BROKER,";
    //            Fields = Fields + " wearhouse,";
    //            Fields = Fields + " subTotal,";
    //            Fields = Fields + "LESS_FRT_RATE,";
    //            Fields = Fields + "freight,";
    //            Fields = Fields + "cash_advance,";
    //            Fields = Fields + "bank_commission,";
    //            Fields = Fields + " OTHER_AMT,";
    //            Fields = Fields + "Bill_Amount,";
    //            Fields = Fields + "Due_Days,";

    //            Fields = Fields + " NETQNTL,";
    //            Fields = Fields + "CGSTRate,";
    //            Fields = Fields + "CGSTAmount,";
    //            Fields = Fields + "SGSTRate,";
    //            Fields = Fields + "SGSTAmount,";
    //            Fields = Fields + "IGSTRate,";
    //            Fields = Fields + "IGSTAmount,";
    //            Fields = Fields + " GstRateCode,";
    //            Fields = Fields + "Company_Code,";
    //            Fields = Fields + "Year_Code,";
    //            Fields = Fields + " Branch_Code,";
    //            Fields = Fields + " Created_By,";
    //            Fields = Fields + "Bill_No,";
    //            Fields = Fields + "EWay_Bill_No,";
    //            Fields = Fields + " purchaseid,";
    //            Fields = Fields + " ac,";
    //            Fields = Fields + " uc,";
    //            Fields = Fields + " mc,";
    //            Fields = Fields + "bk";
    //            #endregion
    //            #region Detail Table Column
    //            Detail_Fields = Detail_Fields + "doc_no,";
    //            Detail_Fields = Detail_Fields + "detail_id,";
    //            Detail_Fields = Detail_Fields + "Tran_Type,";
    //            Detail_Fields = Detail_Fields + "item_code,";
    //            Detail_Fields = Detail_Fields + "narration,";
    //            Detail_Fields = Detail_Fields + "Quantal,";
    //            Detail_Fields = Detail_Fields + "packing,";
    //            Detail_Fields = Detail_Fields + "bags,";
    //            Detail_Fields = Detail_Fields + "rate,";
    //            Detail_Fields = Detail_Fields + "item_Amount,";
    //            Detail_Fields = Detail_Fields + "Company_Code,";
    //            Detail_Fields = Detail_Fields + "Year_Code,";

    //            Detail_Fields = Detail_Fields + "Branch_Code,";
    //            Detail_Fields = Detail_Fields + "Created_By,";
    //            Detail_Fields = Detail_Fields + "purchasedetailid,";
    //            Detail_Fields = Detail_Fields + "purchaseid,";
    //            Detail_Fields = Detail_Fields + "ic";
    //            #endregion

    //            #region Head Insert Qry
    //            //Access Common Class Fields And Vallues
    //            Head_Insert = "insert into nt_1_sugarpurchase(" + Fields + ") values(" + purchase.PS_doc_no + ",'" + purchase.PS_Tran_Type + "'," + purchase.PS_PURCNO + ",'" + purchase.PS_doc_date + "'," + purchase.PS_Ac_Code + "," +
    //            " " + purchase.PS_Unit_Code + "," + purchase.PS_mill_code + ",'" + purchase.PS_FROM_STATION + "','" + purchase.PS_TO_STATION + "','" + purchase.PS_LORRYNO + "'," +
    //            " " + purchase.PS_BROKER + ",'" + purchase.PS_wearhouse + "'," + purchase.PS_subTotal + "," + purchase.PS_LESS_FRT_RATE + "," + purchase.PS_freight + ",'" + purchase.PS_cash_advance + "'," +
    //            " '" + purchase.PS_bank_commission + "','" + purchase.PS_OTHER_AMT + "'," + purchase.PS_Bill_Amount + ",'" + purchase.PS_Due_Days + "','" + purchase.PS_NETQNTL + "'," +
    //            " '" + purchase.PS_CGSTRate + "','" + purchase.PS_CGSTAmount + "','" + purchase.PS_SGSTRate + "','" + purchase.PS_SGSTAmount + "','" + purchase.PS_IGSTRate + "','" + purchase.PS_IGSTAmount + "'," +
    //            " '" + purchase.PS_GstRateCode + "','" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "'," +
    //            " '" + purchase.PS_Branch_Code + "','" + purchase.PS_Created_By + "','" + purchase.PS_Bill_No + "'," + purchase.PS_EWay_Bill_No + "," + purchase.PS_purchase_Id + "," + purchase.PS_ac + "," + purchase.PS_uc + "," + purchase.PS_mc + "," + purchase.PS_bk + ") ";
    //            #endregion
    //            #region Detail Insert Qry
    //            PurchaseDetailId = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(purchasedetailid),0) as purchasedetailid from nt_1_sugarpurchasedetails"));
    //            if (PurchaseDetailId == 0)
    //            {
    //                PurchaseDetailId = 0;
    //            }
    //            else
    //            {
    //                PurchaseDetailId = Convert.ToInt32(clsCommon.getString("select max(purchasedetailid) as purchasedetailid from nt_1_sugarpurchasedetails"));
    //            }
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                PurchaseDetailId = PurchaseDetailId + 1;
    //                item_code = Convert.ToInt32(dt.Rows[i]["item_code"].ToString());
    //                narration = dt.Rows[i]["narration"].ToString();
    //                Quantal = Convert.ToDouble(dt.Rows[i]["Quantal"].ToString());
    //                packing = Convert.ToInt32(dt.Rows[i]["packing"].ToString());
    //                bags = Convert.ToInt32(dt.Rows[i]["bags"].ToString());
    //                rate = Convert.ToDouble(dt.Rows[i]["rate"].ToString());
    //                item_Amount = Convert.ToDouble(dt.Rows[i]["item_Amount"].ToString());
    //                i_d = dt.Rows[i]["ID"].ToString();
    //                int ic = Convert.ToInt32(clsCommon.getString("select systemid from nt_1_systemmaster where Company_Code='" + purchase.PS_Company_Code + "' " +
    //                    " and Year_Code='" + purchase.PS_Year_Code + "'"));

    //                Detail_Values = Detail_Values + "('" + purchase.PS_doc_no + "','" + i_d + "','" + purchase.PS_Tran_Type + "','" + item_code + "','" + narration + "','" + Quantal + "'," +
    //                    " '" + packing + "','" + bags + "','" + rate + "','" + item_Amount + "','" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "'," +
    //                    " '" + purchase.PS_Branch_Code + "','" + purchase.PS_Created_By + "','" + PurchaseDetailId + "','" + purchase.PS_purchase_Id + "','" + ic + "'),";
    //            }
    //            if (Detail_Values.Length > 0)
    //            {
    //                Detail_Values = Detail_Values.Remove(Detail_Values.Length - 1);
    //                Detail_Insert = "insert into nt_1_sugarpurchasedetails (" + Detail_Fields + ") values " + Detail_Values + "";

    //            }
    //            #endregion
    //            if (OpenConnection())
    //            {
    //                _transaction = _connection.BeginTransaction();

    //                _sqlCmd = new SqlCommand(Head_Insert, _connection, _transaction);
    //                _sqlCmd.ExecuteNonQuery();
    //                _sqlCmd = new SqlCommand(Detail_Insert, _connection, _transaction);
    //                _sqlCmd.ExecuteNonQuery();
    //                _transaction.Commit();
    //                Msg = "Insert";
    //                Detail_Fields = string.Empty;
    //                Detail_Values = string.Empty;
    //                Detail_Insert = string.Empty;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        return Msg;

    //    }
    //    catch (Exception ex)
    //    {
    //        clsLog.Publish(ex);
    //        return null;
    //    }
    //    finally
    //    {
    //        //_ds.Dispose();
    //        _sqlCmd.Dispose();
    //        // _adapter.Dispose();
    //        //_connection.Close();
    //        //_connection.Dispose();
    //    }
    //}

    public static string DataStore(string Head_Insert, string Head_Update, string Head_Delete, string Detail_Insert, string Detail_Update, string Detail_Delete, string GLEDGER_Insert, string GLEDGER_Delete, int flag)
    {
        string msg = string.Empty;
        try
        {
            if (OpenConnection())
            {
                _transaction = _connection.BeginTransaction();
                if (flag == 1)
                {
                    if (GLEDGER_Delete != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(GLEDGER_Delete, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }

                    _sqlCmd = new SqlCommand(Head_Insert, _connection, _transaction);
                    _sqlCmd.ExecuteNonQuery();
                    if (Detail_Insert != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(Detail_Insert, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    if (GLEDGER_Insert != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(GLEDGER_Insert, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    _transaction.Commit();
                    msg = "Insert";
                }
                else if (flag == 2)
                {
                    if (GLEDGER_Delete != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(GLEDGER_Delete, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    if (Detail_Delete != "")
                    {
                        _sqlCmd = new SqlCommand(Detail_Delete, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    if (Detail_Insert != "")
                    {
                        _sqlCmd = new SqlCommand(Detail_Insert, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }

                    if (Head_Update != "")
                    {
                        _sqlCmd = new SqlCommand(Head_Update, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    if (Detail_Update != "")
                    {
                        _sqlCmd = new SqlCommand(Detail_Update, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }

                    if (GLEDGER_Insert != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(GLEDGER_Insert, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    _transaction.Commit();
                    msg = "Update";

                }
                else
                {
                    if (GLEDGER_Delete != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(GLEDGER_Delete, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    if (Detail_Delete != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(Detail_Delete, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }
                    _sqlCmd = new SqlCommand(Head_Delete, _connection, _transaction);
                    _sqlCmd.ExecuteNonQuery();
                    if (Head_Update != string.Empty)
                    {
                        _sqlCmd = new SqlCommand(Head_Update, _connection, _transaction);
                        _sqlCmd.ExecuteNonQuery();
                    }

                    _transaction.Commit();
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

    public static int AccountID(string Ac_Code, int Company_Code)
    {
        int id = 0;
        try
        {
            SqlDataReader dr1;
            if (OpenConnection())
            {
                _sqlCmd = new SqlCommand("Sp_AccountMaster", _connection);
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.AddWithValue("Ac", Ac_Code);
                _sqlCmd.Parameters.AddWithValue("Company", Company_Code);

                dr1 = _sqlCmd.ExecuteReader();
                if (dr1.HasRows == true)
                {
                    dr1.Read();
                    id = Convert.ToInt32(dr1[0].ToString());

                    return id;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        catch (Exception e)
        {
            clsLog.Publish(e);
            throw;
        }
        finally
        {
            _connection.Close();
            _connection.Dispose();
            _connection.Dispose();

            _ds.Dispose();
            _sqlCmd.Dispose();
            _adapter.Dispose();


            _sqlCmd.Dispose();


            //con1.Close();
            //con1.Dispose();
            //cmd1.Dispose();
        }
    }
    public static DataSet myaccountmaster(string searchstring)
    {
        string mystring = "";
        using (SqlConnection _connection = new SqlConnection(strConnectionString))
        {

            mystring = "SELECT dbo.nt_1_accountmaster.Ac_Code, dbo.nt_1_accountmaster.Ac_Name_E, dbo.nt_1_accountmaster.Ac_type, dbo.nt_1_citymaster.city_name_e as cityname, dbo.nt_1_accountmaster.Address_E "
                            + " FROM dbo.nt_1_accountmaster LEFT OUTER JOIN " +
                               " dbo.nt_1_citymaster ON dbo.nt_1_accountmaster.City_Code = dbo.nt_1_citymaster.city_code AND dbo.nt_1_accountmaster.company_code = dbo.nt_1_citymaster.company_code" +
                              " where " + searchstring; 

            SqlCommand _sqlCmd = new SqlCommand(mystring, _connection);
            _sqlCmd.CommandTimeout = 100;
            SqlDataAdapter _adapter = new SqlDataAdapter(_sqlCmd);
            DataSet _ds = new DataSet();
            _adapter.Fill(_ds);
            return _ds;


        }

    }
}
