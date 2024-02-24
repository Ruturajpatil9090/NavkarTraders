using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for clsGetMaxCode
/// </summary>
public class clsGetMaxCode : IDisposable
{
    //public clsGetMaxCode()
    //{
    //}

    public void Dispose()
    {
        if (ds != null)
        {
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
 
        }
        //clsDAL._connection.Close();
    }

    #region variable
    public DataSet ds = null;
    public DataTable dt = null;
    Hashtable hash = null;
    public string tableName { get; set; }
    public string code { get; set; }
    #endregion

    public DataSet getMaxCode()
    {
        try
        {
            hash = new Hashtable();
            hash.Add("@tableName", tableName);
            hash.Add("@Code", code);

            ds = new DataSet();
            ds = clsDAL.FillDataSet("SP_GetMaxCode", hash);
            return ds;
        }
        catch
        {
            return null;
        }
    }
}