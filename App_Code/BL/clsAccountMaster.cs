using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
/// <summary>
/// Summary description for clsAccountMaster
/// </summary>
public class clsAccountMaster : IDisposable
{
    #region [Variables]

    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    Hashtable hash;

    public int flag { get; set; }
    public int AC_Code { get; set; }
    public string AC_Name_E { get; set; }
    public string AC_Name_R { get; set; }
    public string AC_Type { get; set; }
    public string Address_E { get; set; }
    public string Address_R { get; set; }
    public string City_E { get; set; }
    public string City_R { get; set; }
    public string pincode { get; set; }
    public string State_E { get; set; }
    public string State_R { get; set; }
    public string persone1 { get; set; }
    public string mob1 { get; set; }
    public string persone2 { get; set; }
    public string mob2 { get; set; }
    public string persone3 { get; set; }
    public string mob3 { get; set; }
    public string LocalLIC { get; set; }
    public string TIN { get; set; }
    public string GST { get; set; }
    public string Email { get; set; }
    public string BankNm { get; set; }
    public string ACNo { get; set; }
    public string IFS { get; set; }
    public string Branch { get; set; }
    public string openingBal { get; set; }
    public string DRCR { get; set; }
    public int GroupCode { get; set; }
    public int CompanyCode { get; set; }
    public string CreatedBy { get; set; }
    public string ModifyBy { get; set; }

    #endregion





    public clsAccountMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet insertAccountMaster(ref string str)
    {
        try
        {
            //using (clsDAL obj = new clsDAL())
            //{
            ds = new DataSet();
            hash = new Hashtable();
            hash.Add("@Flag", flag);
            hash.Add("@AC_Code", AC_Code);
            hash.Add("@AC_Name_E", AC_Name_E);
            hash.Add("@AC_Name_R", AC_Name_R);
            hash.Add("@AC_Type", AC_Type);
            hash.Add("@Address_E", Address_E);
            hash.Add("@Address_R", Address_R);
            hash.Add("@City_E", City_E);
            hash.Add("@City_R", City_R);
            hash.Add("@pincode", pincode);
            hash.Add("@State_E", State_E);
            hash.Add("@State_R", State_R);
            hash.Add("@persone1", persone1);
            hash.Add("@mob1", mob1);
            hash.Add("@persone2", persone2);
            hash.Add("@mob2", mob2);
            hash.Add("@persone3", persone3);
            hash.Add("@mob3", mob3);
            hash.Add("@LocalLIC ", LocalLIC);
            hash.Add("@TIN ", TIN);
            hash.Add("@GST", GST);
            hash.Add("@Email", Email);
            hash.Add("@BankNm", BankNm);
            hash.Add("@ACNo", ACNo);
            hash.Add("@IFS ", IFS);
            hash.Add("@Branch", Branch);
            hash.Add("@openingBal", openingBal);
            hash.Add("@DRCR", DRCR);
            hash.Add("@GroupCode", GroupCode);
            hash.Add("@CompanyCode", CompanyCode);
            hash.Add("@CreatedBy", CreatedBy);
            hash.Add("@ModifyBy", ModifyBy);
            ds = clsDAL.ExecuteDMLQuery(hash, "SP_IU_AccountMaster", ref str);
            return ds;
            //}
        }
        catch
        {
            return null;
        }
    }



    #region IDisposable Members

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }

    #endregion
}