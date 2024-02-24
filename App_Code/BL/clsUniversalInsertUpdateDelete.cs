using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

public class clsUniversalInsertUpdateDelete : IDisposable
{
    #region[Contructore and destructore]
    public clsUniversalInsertUpdateDelete()
    {
    }
    ~clsUniversalInsertUpdateDelete()
    {
        Dispose();
    }
    #endregion

    #region[variables]
    DataSet ds;
    Hashtable hash;
    public string tableName { get; set; }
    public int flag { get; set; }
    public string columnNm { get; set; }
    public string values { get; set; }
    #endregion
    public List<object> allHash; //= new List<object>();


    string str = string.Empty;
    public DataSet insertAccountMaster(ref string str)
    {
        try
        {
            //using (clsDAL obj = new clsDAL())
            //{
            hash = new Hashtable();
            ds = new DataSet();
            hash.Add("@flag", flag);
            hash.Add("@tableName", tableName);
            hash.Add("@columnNm", columnNm);
            hash.Add("@values", values);
            //allHash.Add(hash);
            ds = clsDAL.ExecuteDMLQuery(hash, "Universal_IUD", ref str);
            return ds;
            //}
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            return null;
        }
    }

    public string Exceute()
    {
        string bcs = string.Empty;
        string a = clsDAL.ExcecuteAll(allHash, "Universal_IUD", bcs);
        allHash = null;
        return a;
    }

    public string insertDO(ref string str)
    {
        try
        {
            //using (clsDAL obj = new clsDAL())
            //{
            hash = new Hashtable();
            ds = new DataSet();
            hash.Add("@flag", flag);
            hash.Add("@tableName", tableName);
            hash.Add("@columnNm", columnNm);
            hash.Add("@values", values);
            //allHash.Add(hash);
            ds = clsDAL.ExecuteDMLQuery(hash, "Universal_IUD", ref str);
            return "";
            //}
        }
        catch (Exception ex)
        {
            return ex.Message + " " + columnNm + " " + values;
        }
    }
    #region IDisposable Members
    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }
    #endregion
}