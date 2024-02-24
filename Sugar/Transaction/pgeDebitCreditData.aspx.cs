using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Transaction_pgeDebitCreditData : System.Web.UI.Page
{
    static WebControl objAsp = null;
    int entryno = 0;
    int Mill_Code = 0;
    string Tran_Type = string.Empty;
    string html = string.Empty;
    int expaccode = 0;
    string entryid = string.Empty;
    string date = string.Empty;
    string expacname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        setFocusControl(drpSub_Type);
    }
    public string getData()
    {
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = clsDAL.SimpleQuery("select distinct doc_no,doc_dateConverted,tran_type,ac_code,Ac_Name_E,dcid from qrydebitnoteheaddetail order by doc_no desc ");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                entryno = Convert.ToInt32(dt.Rows[i]["doc_no"].ToString());
                date = dt.Rows[i]["doc_dateConverted"].ToString();
                Tran_Type = dt.Rows[i]["tran_type"].ToString();
                expaccode = Convert.ToInt32(dt.Rows[i]["ac_code"].ToString());
                expacname = dt.Rows[i]["Ac_Name_E"].ToString();
                entryid = dt.Rows[i]["dcid"].ToString();

                html += "<tr><td>" + i + "</td><td>" + entryno + "</td><td>" + date + "</td><td>" + Tran_Type + "</td><td>" + expaccode + "</td><td> " + expacname + "</td><td>" + entryid + "</td></tr>";


            }
            return html;
        }
        catch
        {
            return "";
        }
    }
    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
}