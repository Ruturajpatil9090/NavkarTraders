using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
    public Security()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string Authenticate(string tblPrefix, string user)
    {
        string isAuthenticate = string.Empty;

        string qry = string.Empty;
        DataSet ds = new DataSet();
        //string user = clsGV.user;
        string userid = clsCommon.getString("Select User_id from  tblUser  WHERE User_Name='" + user + "'");
        string pagename = HttpContext.Current.Request.Url.AbsolutePath;
        int fromStr = pagename.LastIndexOf(@"/");

        string trim = pagename.Substring(1, fromStr - 1);
        int len = trim.LastIndexOf(@"/");
        string folderName = trim.Substring(len + 1);
        string ext = pagename.Substring(fromStr + 1);
        int toStr = ext.IndexOf(@".");
        string pgeName = ext.Substring(0, toStr);

        string pageid = clsCommon.getString("Select Doc_No from program_master where Program_Name='" + pgeName + "'");

        if (pageid == "0")
        {
            string pagetypenew = "R";
            if (folderName != "Report")
            {
                pagetypenew = "P";
            }
            string maxcount = clsCommon.getString("select max(Doc_No)+1 from program_master");
            string namereturn = clsCommon.getString("insert into program_master (Tran_Type,Program_Name,Doc_No,Page_Type) values ('','" + pgeName +
                "'," + maxcount + ",'" + pagetypenew + "')");
            pageid = "1";
        }

        int pgid = pageid != string.Empty ? Convert.ToInt32(pageid) : 0;

        if (pgid == 0)
        {

        }
        else
        {
            if (folderName != "Report")
            {
                qry = "Select * from tbluserdetail where Program_Name='" + pgeName + "' AND User_Id=" + userid + "";
            }
            else
            {
                qry = "Select * from tbluserdetail_report where Program_Name='" + pgeName + "' AND User_Id=" + userid + "";

            }
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();
            dt.Columns.Add("IsAuthenticate", typeof(string));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["IsAuthenticate"] = ds.Tables[0].Rows[i]["Permission"].ToString();
                        dt.Rows.Add(dr);
                        isAuthenticate = dt.Rows[i]["IsAuthenticate"].ToString();

                    }

                    if (isAuthenticate == "Y")
                    {
                        isAuthenticate = "1";
                    }
                    else
                    {
                        isAuthenticate = "0";
                    }
                }
                else
                {
                    isAuthenticate = "0";
                }
            }

        }
        return isAuthenticate;
    }
}