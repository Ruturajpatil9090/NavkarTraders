using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class Report_rptWithPaymentDifference : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        try
        {
          
           
                qry = "select DISTINCT Voucher_By as partycode,voucherbyname as party from qrytenderhead  where " +
                    " type='W' and Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  order by party";
            

            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        datalist.DataSource = dt;
                        datalist.DataBind();
                    }
                    else
                    {
                        datalist.DataSource = null;
                        datalist.DataBind();
                    }
                }
            }
            //}
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            clsDAL.CloseConnection();
        }
    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //using (clsDataProvider obj = new clsDataProvider())
        //{
        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        Label lblPartyCode = (Label)e.Item.FindControl("lblPartyCode");
        string voucherby = lblPartyCode.Text;
        Label lblToPayTotal = (Label)e.Item.FindControl("lblToPayTotal");
        Label lblToRecieveTotal = (Label)e.Item.FindControl("lblToRecieveTotal");

        //qry = "select Tender_DateConverted as tdate,Tender_No as tno,isnull(millshortname,millname) as mill" +
        //       " ,Quantal as quantal,Mill_Rate as millrate,Purc_Rate as purcrate from qrytenderhead  where Voucher_By=" + voucherby + "" +
        //       " and type='W' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

        qry = "select Tender_DateConverted as tdate,Tender_No as tno,isnull(millshortname,millname) as mill" +
              " ,Quantal as quantal,Mill_Rate as millrate,Purc_Rate as purcrate from qrytenderhead  where Voucher_By=" + voucherby + "" +
              " and type='W' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";

          
        ds = new DataSet();
        //ds = obj.GetDataSet(qry);
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("tdate", typeof(string)));
            dt.Columns.Add(new DataColumn("tno", typeof(string)));
            dt.Columns.Add(new DataColumn("mill", typeof(string)));
            dt.Columns.Add(new DataColumn("quantal", typeof(string)));
            dt.Columns.Add(new DataColumn("millrate", typeof(string)));
            dt.Columns.Add(new DataColumn("purcrate", typeof(string)));
            dt.Columns.Add(new DataColumn("topay", typeof(double)));
            dt.Columns.Add(new DataColumn("torecieve", typeof(double)));
            dt.Columns.Add(new DataColumn("topayrate", typeof(double)));
            dt.Columns.Add(new DataColumn("torecieverate", typeof(double)));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["tdate"] = ds.Tables[0].Rows[i]["tdate"].ToString();
                    dr["tno"] = ds.Tables[0].Rows[i]["tno"].ToString();
                    dr["mill"] = ds.Tables[0].Rows[i]["mill"].ToString();
                    double quintal = Convert.ToDouble(ds.Tables[0].Rows[i]["quantal"].ToString());
                    dr["quantal"] = quintal;
                    double millrate = Convert.ToDouble(ds.Tables[0].Rows[i]["millrate"].ToString());
                    double purcrate = Convert.ToDouble(ds.Tables[0].Rows[i]["purcrate"].ToString());
                    dr["millrate"] = millrate;
                    dr["purcrate"] = purcrate;
                    double rate = millrate - purcrate;
                    double amount = rate * quintal;
                    if (rate <= 0)
                    {
                        dr["topayrate"] = Math.Abs(rate);
                    }
                    else
                    {
                        dr["torecieverate"] = Math.Abs(rate);
                    }
                    if (amount <= 0)
                    {
                        dr["topay"] = Math.Abs(amount);
                    }
                    else
                    {
                        dr["torecieve"] = Math.Abs(amount);
                    }
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    lblToPayTotal.Text = Convert.ToString(dt.Compute("SUM(topay)", string.Empty));
                    lblToRecieveTotal.Text = Convert.ToString(dt.Compute("SUM(torecieve)", string.Empty));
                    dtlDetails.DataSource = dt;
                    dtlDetails.DataBind();
                }
                else
                {
                    dtlDetails.DataSource = null;
                    dtlDetails.DataBind();
                }
            }
            else
            {
                dtlDetails.DataSource = null;
                dtlDetails.DataBind();
            }
        }
        //}
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        pnlMain.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        string strFile = "WithPaymentDifference_report.xls";
        string strcontentType = "application/excel";
        Response.ClearContent();
        Response.ClearHeaders();
        Response.BufferOutput = true;
        Response.ContentType = strcontentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        Response.Write(StrExport.ToString());
        Response.Flush();
        Response.Close();
        Response.End();
    }
}