using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeSelectYear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string qry = "select yearCode,year as Year from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + "";
            DataSet ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    ViewState["currentTable"] = dt;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ViewState["currentTable"] = null;
                }
            }
        }
    }

    protected void lnkYear_Click(object sender, EventArgs e)
    {
        LinkButton lnkYear = (LinkButton)sender;
        GridViewRow rw = (GridViewRow)lnkYear.NamingContainer;
        int index = rw.RowIndex;

        Session["year"] = rw.Cells[0].Text.ToString();
        string qry = "select convert(varchar(10),Start_Date,103) as Start_Date,convert(varchar(10),End_Date,103) as End_Date from AccountingYear where " +
            "Company_Code=" + Session["Company_Code"].ToString() + " and yearCode=" + Session["year"].ToString();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];
        string sd = dt.Rows[0]["Start_Date"].ToString();
        string ed = dt.Rows[0]["End_Date"].ToString();
        Session["Start_Date"] = sd;
        Session["End_Date"] = ed;
        Session["accountingYear"] = sd + "-" + ed;

        Session["printinsalebill"] = clsCommon.getString("select printinsalebill from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + " and yearCode=" + Session["year"].ToString());

        string printinsalebill = Session["printinsalebill"].ToString();

        if (printinsalebill == "Y")
        {
            Session["selectedyear"] = clsCommon.getString("select year from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + " and yearCode=" + Session["year"].ToString());
        }
        else
        {
            Session["selectedyear"] = string.Empty;
        }
        Label lblYear = (Label)Master.FindControl("lblYear");
        lblYear.Text = sd + "-" + ed;

        clsGV.Start_Date = Session["Start_Date"].ToString();
        clsGV.End_Date = Session["End_Date"].ToString();

        DateTime dStart = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        clsGV.Start_Date = dStart.ToString("dd/MM/yyyy");
        DateTime dEnd = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        string todayDate = clsCommon.getString("select convert(varchar(10),GetDate(),103) as todayDt");
        DateTime dToday = DateTime.Parse(todayDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        if (dToday < dEnd)
        {
            clsGV.To_date = dToday.ToString("dd/MM/yyyy");
        }
        else
        {
            clsGV.To_date = dEnd.ToString("dd/MM/yyyy");
        }

        Response.Redirect("~/Sugar/pgeHome.aspx", false);
    }

}