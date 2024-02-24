using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Transaction_pgeUtr_Data : System.Web.UI.Page
{
    static WebControl objAsp = null;
    int Doc_No = 0;
    int Mill_Code = 0;
    string Utrno = string.Empty;
    string html = string.Empty;
    int LotNo = 0;
    string utr_id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        setFocusControl(btnAdd);
    }
    public string getData()
    {
        // string cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = clsDAL.SimpleQuery("select doc_no,Mill_Code,utr_no,isnull(lot_no,0) as Lott_No,utrid  from qryutrheaddetail where Company_code=" + Session["Company_Code"].ToString() + " " +
                " and Year_Code=" + Session["Year"].ToString() + " order by doc_no desc ");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Doc_No = Convert.ToInt32(dt.Rows[i]["doc_no"].ToString());
                Mill_Code = Convert.ToInt32(dt.Rows[i]["Mill_Code"].ToString());
                Utrno = dt.Rows[i]["utr_no"].ToString();
                LotNo = Convert.ToInt32(dt.Rows[i]["Lott_No"].ToString());
                utr_id = dt.Rows[i]["utrid"].ToString();
                html += "<tr><td visible='false'>" + i + "</td><td>" + Doc_No + "</td><td>" + Mill_Code + "</td><td>" + Utrno + "</td><td>" + LotNo + "</td><td>" + utr_id + "</td></tr>";


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