using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class Report_rptWithPaymentDiff : System.Web.UI.Page
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

            //qry = "select Tender_DateConverted as tdate,Tender_No as tno,isnull(millshortname,millname) as mill,voucherbyname as party,Broker" +
            // " ,Quantal as quantal,Mill_Rate as millrate,Purc_Rate as purcrate,Voucher_No as voucher from qrytenderhead  where Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and " +
            // "  type='W' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ";

            qry = "select Tender_DateConverted as tdate,Tender_No as tno,isnull(millshortname,millname) as mill,voucherbyname as party,Broker" +
            " ,Quantal as quantal,Mill_Rate as millrate,Purc_Rate as purcrate,Voucher_No as voucher from qrytenderhead  where Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and " +
            "  type='W' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ";



            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("tdate", typeof(string)));
                dt.Columns.Add(new DataColumn("tno", typeof(string)));
                dt.Columns.Add(new DataColumn("mill", typeof(string)));
                dt.Columns.Add(new DataColumn("party", typeof(string)));
                dt.Columns.Add(new DataColumn("Broker", typeof(string)));
                dt.Columns.Add(new DataColumn("quantal", typeof(string)));
                dt.Columns.Add(new DataColumn("millrate", typeof(string)));
                dt.Columns.Add(new DataColumn("purcrate", typeof(string)));
                dt.Columns.Add(new DataColumn("amount", typeof(double)));
                dt.Columns.Add(new DataColumn("voucher", typeof(string)));
                dt.Columns.Add(new DataColumn("finalAmt", typeof(double)));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["tdate"] = ds.Tables[0].Rows[i]["tdate"].ToString();
                        dr["tno"] = ds.Tables[0].Rows[i]["tno"].ToString();
                        dr["mill"] = ds.Tables[0].Rows[i]["mill"].ToString();
                        dr["party"] = ds.Tables[0].Rows[i]["party"].ToString();
                        string voucno = ds.Tables[0].Rows[i]["voucher"].ToString();
                        if (voucno != "0")
                        {
                            dr["voucher"] = voucno + " LV";
                        }
                        else
                        {
                            dr["voucher"] = "";
                        }

                        string Broker_Code = ds.Tables[0].Rows[i]["Broker"].ToString();
                        if (Broker_Code != "2")
                        {
                            dr["Broker"] = clsCommon.getString("Select Short_Name from " + tblPrefix + "AccountMaster where Ac_Code=" + Broker_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        }
                        else
                        {
                            dr["Broker"] = "";
                        }
                        double qntl = Convert.ToDouble(ds.Tables[0].Rows[i]["quantal"].ToString());
                        dr["quantal"] = qntl;
                        double millrate = Convert.ToDouble(ds.Tables[0].Rows[i]["millrate"].ToString());
                        double purcrate = Convert.ToDouble(ds.Tables[0].Rows[i]["purcrate"].ToString());
                        dr["millrate"] = millrate;
                        dr["purcrate"] = purcrate;
                        double amount = ((millrate - purcrate) * qntl);
                        double finalAmt = qntl * millrate;
                        dr["finalAmt"] = finalAmt;
                        if (amount < 0)
                        {
                            dr["amount"] = Math.Abs(amount);
                            dt.Rows.Add(dr);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        lblFinalAmount.Text = Convert.ToString(dt.Compute("SUM(finalAmt)", string.Empty));
                        lblDiffAmount.Text = Convert.ToString(dt.Compute("SUM(amount)", string.Empty));
                        datalist.DataSource = dt;
                        datalist.DataBind();
                    }
                    else
                    {
                        datalist.DataSource = null;
                        datalist.DataBind();
                    }
                }
                else
                {
                    datalist.DataSource = null;
                    datalist.DataBind();
                }
            }
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
    protected void lnkLV_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkLV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkLV.NamingContainer;
            string no = lnkLV.Text.Replace("LV", "").Trim();
            Session["LV_NO"] = no;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:DebitNote();", true);
            lnkLV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
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
        string strFile = "WithPaymentDifference_Report.xls";
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