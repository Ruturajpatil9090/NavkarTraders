using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Transaction_pgeReceipt_Data : System.Web.UI.Page
{
    static WebControl objAsp = null;
    int Doc_No = 0;
    int cashbank = 0;
    string tran_type = string.Empty;
    int tranid = 0;
    string html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        setFocusControl(drpTrnType);
    }
    public string getData()
    {
        // string cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = clsDAL.SimpleQuery("select Doc_No,cashbank,tran_type,doc_date,tranid  from nt_1_transacthead where Company_code=" + Session["Company_Code"].ToString() + " " +
                " and Year_Code=" + Session["Year"].ToString() + "  order by doc_no desc ");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Doc_No = Convert.ToInt32(dt.Rows[i]["doc_no"].ToString());
                cashbank = Convert.ToInt32(dt.Rows[i]["cashbank"].ToString());
                tran_type = dt.Rows[i]["tran_type"].ToString();
                tranid = Convert.ToInt32(dt.Rows[i]["tranid"].ToString());
                DateTime dtime = Convert.ToDateTime(dt.Rows[i]["doc_date"].ToString());
                html += "<tr><td visible='false'>" + i + "</td><td>" + Doc_No + "</td><td>" + cashbank + "</td><td>" + tran_type + "</td><td>" + dtime + "</td><td>" + tranid + "</td></tr>";


            }

            return html;
        }
        catch
        {
            return "";
        }
    }

    protected void lnkGo_Click(object sender, EventArgs e)
    {

    }
    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
}