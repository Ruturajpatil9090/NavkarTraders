using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Sugar_Inword_pgeSugarPurchaseReturnForGST_DataUtility : System.Web.UI.Page
{
    int doc_no = 0;
    string doc_date = string.Empty;
    string AcName = string.Empty;
    double NetQtl = 0.00;
    double BillAmt = 0.00;
    int Srid = 0;
    string html = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }

    }
    public string getData()
    {
        // string cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = clsDAL.SimpleQuery("select distinct doc_no,doc_dateConverted,Ac_Name_E,NETQNTL,Bill_Amount,prid from qrysugarpurchasereturnhead where Company_Code=" + Session["Company_Code"].ToString() + " " +
                " and Year_Code=" + Session["Year"].ToString() + "  order by doc_no desc ");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                doc_no = Convert.ToInt32(dt.Rows[i]["doc_no"].ToString());
                doc_date = dt.Rows[i]["doc_dateConverted"].ToString();
                AcName = dt.Rows[i]["Ac_Name_E"].ToString();
                NetQtl = Convert.ToDouble(dt.Rows[i]["NETQNTL"].ToString());
                BillAmt = Convert.ToDouble(dt.Rows[i]["Bill_Amount"].ToString());
                Srid = Convert.ToInt32(dt.Rows[i]["prid"].ToString());


                html += "<tr><td>" + i + "</td><td>" + doc_no + "</td><td>" + doc_date + "</td><td>" + AcName + "</td><td>" + NetQtl + "</td><td>" + BillAmt + "</td><td>" + Srid + "</td></tr>";


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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("pgeSugarPurchaseReturnForGST_DataUtility.aspx");
    }
    protected void txtf1_Click(object sender, EventArgs e)
    {

    }
}