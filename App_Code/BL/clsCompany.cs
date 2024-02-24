using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for clsCompany
/// </summary>
public class clsCompany : IDisposable
{
    public clsCompany()
    {
      
    }
    public void Dispose()
    {

    }

    #region variables
    public DataSet ds = null;
    public DataTable dt = null;
    Hashtable hash = null;

    public int flag { get; set; }
    public int Company_Code { get; set; }
    public string Company_Name_E { get; set; }
    public string Company_Name_R { get; set; }
    public string Address_E { get; set; }
    public string Address_R { get; set; }
    public string City_E { get; set; }
    public string City_R { get; set; }
    public string State_E { get; set; }
    public string State_R { get; set; }
    public string PIN { get; set; }
    public string Mobile_No { get; set; }
    public string Created_By { get; set; }
    public string Modified_By { get; set; }
    public string Pan_No { get; set; }
    public string FSSAI_No { get; set; }
    public string CST { get; set; }
    public string TIN { get; set; }
    public string GST { get; set; }
    public string PHONE { get; set; }
    public string Group_Code { get; set; }
    #endregion

    public DataSet saveCompany(ref string str)
    {
        try
        {
            //using (clsDAL obj=new clsDAL())
            //{
                ds = new DataSet();
                hash = new Hashtable();
                hash.Add("@flag", flag);
                hash.Add("@Company_Code", Company_Code);
                hash.Add("@Company_Name_E", Company_Name_E);
                hash.Add("@Company_Name_R", Company_Name_R);
                hash.Add("@Address_E", Address_E);
                hash.Add("@Address_R", Address_R);
                hash.Add("@City_E", City_E);
                hash.Add("@City_R", City_R);
                hash.Add("@State_E", State_E);
                hash.Add("@State_R", State_R);
                hash.Add("@PIN", PIN);
                hash.Add("@Mobile_No", Mobile_No);
                hash.Add("@Created_By", Created_By);
                hash.Add("@Modified_By", Modified_By);
                hash.Add("@Pan_No", Pan_No);
                hash.Add("@FSSAI_No", FSSAI_No);
                hash.Add("@CST", CST);
                hash.Add("@TIN", TIN);
                hash.Add("@GST", GST);
                hash.Add("@PHONE", PHONE);
                hash.Add("@Group_Code", Group_Code);
                string qry = "select * from Company";
                ds = clsDAL.SimpleQuery(qry);
                ds = clsDAL.ExecuteDMLQuery(hash, "SP_IUD_Company", ref str);
                return ds; 
            //}
        }
        catch
        {
            return null;
        }
    }
}