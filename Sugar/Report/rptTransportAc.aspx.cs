using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Net.Mail;
public partial class Report_rptTransportAc : System.Web.UI.Page
{
    string qryCommon = string.Empty;
    string tblPrefix = string.Empty;
    string cityMasterTable = string.Empty;
    string prefix = string.Empty;
    string vtype = string.Empty;
    string f = "../GSReports/TransportBalance_.htm";
    string f_Main = "../Report/TransportBalance";
    string qry = string.Empty;
    string Branch_Code = string.Empty;
    string fromDt = string.Empty;
    string toDt = string.Empty;
    string Transport_Ac = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet bind;
    DataTable secondqry;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tblPrefix = Session["tblPrefix"].ToString();
            qryCommon = tblPrefix + "qryVoucherList";
            cityMasterTable = tblPrefix + "CityMaster";
            fromDt = Request.QueryString["fromDT"];
            toDt = Request.QueryString["toDT"];
            Transport_Ac = Request.QueryString["Transport"];
            Branch_Code = Request.QueryString["Branch_Code"];
            lblCompanyName.Text = Session["Company_Name"].ToString();
            this.bindData();
        }
    }
    private void bindData()
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                qry = "select doc_no ,doc_dateConverted as dt,voucherbyshortname as VoucherBy,millshortname as MillShort,truck_no as lorry,quantal as Qntl,Freight_RateMM as Rate," +
                                " Freight_AmountMM as Freight,Paid_Amount1 as Paid1,Paid_Amount2 as Paid2,Paid_Amount3 as Paid3,transport from qrydohead where tran_type='DO' and transport!=0 " +
                                "  and doc_date between '" + fromDt + "' and '" + toDt + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 ";


                bind = clsDAL.SimpleQuery(qry);
                secondqry = bind.Tables[0];

                if (Transport_Ac == string.Empty)
                {
                    qry = "select distinct(transport) as TransportCode,TransportName from qrydohead where tran_type='DO' and transport!=0 and doc_date between '" + fromDt + "' and '" + toDt + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 order by TransportName";
                }
                else
                {
                    qry = "select distinct(transport) as TransportCode,TransportName from qrydohead where tran_type='DO' and transport!=0 and doc_date between '" + fromDt + "' and '" + toDt + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and transport=" + Transport_Ac + " and purc_no!=0 order by TransportName";
                }




                ds = new DataSet();
                ds = obj.GetDataSet(qry);
                //ds = clsDAL.SimpleQuery(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        lblCompanyName.Text = clsCommon.getString("Select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        dtlist.DataSource = dt;
                        dtlist.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlist_OnitemDatabound(object sender, DataListItemEventArgs e)
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
                Label lblTransportCode = (Label)e.Item.FindControl("lblTransportCode");
                string transport = lblTransportCode.Text;
                Label lblTotalBalance = (Label)e.Item.FindControl("lblTotalBalance");

                DataView view = new DataView(secondqry, "transport='" + lblTransportCode.Text + "'", "transport", DataViewRowState.CurrentRows);
                DataTable selectedvalue = view.ToTable(true, "doc_no", "dt", "VoucherBy", "MillShort", "lorry", "Qntl", "Rate", "Freight", "Paid1", "Paid2", "Paid3", "transport");



                //ds = clsDAL.SimpleQuery(qry);
                if (selectedvalue.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("#", typeof(string)));
                    dt.Columns.Add(new DataColumn("dt", typeof(string)));
                    dt.Columns.Add(new DataColumn("VoucherBy", typeof(string)));
                    dt.Columns.Add(new DataColumn("MillShort", typeof(string)));
                    dt.Columns.Add(new DataColumn("lorry", typeof(string)));
                    dt.Columns.Add(new DataColumn("Qntl", typeof(string)));
                    dt.Columns.Add(new DataColumn("Rate", typeof(string)));
                    dt.Columns.Add(new DataColumn("Freight", typeof(string)));
                    dt.Columns.Add(new DataColumn("Paid1", typeof(string)));
                    dt.Columns.Add(new DataColumn("Paid2", typeof(string)));
                    dt.Columns.Add(new DataColumn("Paid3", typeof(string)));
                    dt.Columns.Add(new DataColumn("balance", typeof(double)));
                    for (int i = 0; i < selectedvalue.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["#"] = selectedvalue.Rows[i]["doc_no"].ToString();
                        dr["dt"] = selectedvalue.Rows[i]["dt"].ToString();
                        dr["VoucherBy"] = selectedvalue.Rows[i]["VoucherBy"].ToString();
                        dr["MillShort"] = selectedvalue.Rows[i]["MillShort"].ToString();
                        dr["lorry"] = selectedvalue.Rows[i]["lorry"].ToString();
                        dr["Qntl"] = selectedvalue.Rows[i]["Qntl"].ToString();
                        dr["Rate"] = selectedvalue.Rows[i]["Rate"].ToString();
                        string frtAmt = selectedvalue.Rows[i]["Freight"].ToString();
                        double freightAmt = frtAmt != string.Empty ? Convert.ToDouble(frtAmt) : 0.00;
                        dr["Freight"] = freightAmt;
                        string paid1 = selectedvalue.Rows[i]["Paid1"].ToString();
                        if (paid1 == "0.00")
                        {
                            dr["Paid1"] = "";
                        }
                        else
                        {
                            dr["Paid1"] = paid1;
                        }
                        string paid2 = selectedvalue.Rows[i]["Paid2"].ToString();
                        if (paid2 == "0.00")
                        {
                            dr["Paid2"] = "";
                        }
                        else
                        {
                            dr["Paid2"] = paid2;
                        }
                        string paid3 = selectedvalue.Rows[i]["Paid3"].ToString();
                        if (paid3 == "0.00")
                        {
                            dr["Paid3"] = "";
                        }
                        else
                        {
                            dr["Paid3"] = paid3;
                        }
                        double p1 = paid1 != string.Empty ? Convert.ToDouble(paid1) : 0.00;
                        double p2 = paid2 != string.Empty ? Convert.ToDouble(paid2) : 0.00;
                        double p3 = paid3 != string.Empty ? Convert.ToDouble(paid3) : 0.00;

                        double balance = freightAmt - (p1 + p2 + p3);
                        dr["balance"] = balance;
                        dt.Rows.Add(dr);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        lblTotalBalance.Text = Convert.ToString(dt.Compute("SUM(balance)", string.Empty));
                        dtlDetails.DataSource = dt;
                        dtlDetails.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
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
        string strFile = "report.xls";
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
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {

    }
}