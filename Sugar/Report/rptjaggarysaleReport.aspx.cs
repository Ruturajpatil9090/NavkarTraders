using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

public partial class Report_rptjaggarysaleReport : System.Web.UI.Page
{
    string Mill_Code = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    DataSet ds = null;
    DataTable dt = null;
    double grandNetQntl = 0.00;
    double grandSubtotal = 0.00;
    double grandExtraexpenses = 0.00;
    double grandfright = 0.00;
    double grandotheramt = 0.00;
    double grandBillAmount = 0.00;
    double grandcgstamnt = 0.00;
    double grandsgstamnt = 0.00;
    double grandigstamnt = 0.00;
    double grandRoundoff = 0.00;
    protected void Page_Load(object sender, EventArgs e)
    {

        tblPrefix = Session["tblPrefix"].ToString();
        Mill_Code = Request.QueryString["AcCode"].ToString();
        fromDT = Request.QueryString["FromDt"].ToString();
        toDT = Request.QueryString["ToDt"].ToString();
        if (!Page.IsPostBack)
        {
            lblCompanyName.Text = Session["Company_Name"].ToString();
            this.BindList();
        }
    }

    private void BindList()
    {
        try
        {
            string from = Convert.ToDateTime(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string to = Convert.ToDateTime(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (Mill_Code != string.Empty)
            {
                qry = "Select DISTINCT(Convert(varchar(10),doc_date,103)) as Date,doc_date as DDate from " + tblPrefix
                    + "JSaleHead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                    " doc_date between '" + from + "' and '" + to + "' and Cust_Code=" + Mill_Code + " order by DDate";
            }
            else
            {
                qry = "Select DISTINCT(Convert(varchar(10),doc_date,103)) as Date,doc_date as DDate from " + tblPrefix + "JSaleHead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                   " doc_date between '" + from + "' and '" + to + "'  order by DDate";
            }
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dtl.DataSource = dt;
                    dtl.DataBind();

                    lblGrandNetQntl.Text = Convert.ToDecimal(grandNetQntl).ToString("0.00");
                    lblGrandSubTotal.Text = Convert.ToDecimal(grandSubtotal).ToString("0.00");
                    //lblGrandExtraExp.Text = grandExtraexpenses.ToString();
                    //lblGrandfright.Text = grandfright.ToString();
                    //lblGrandotheramt.Text = grandotheramt.ToString();

                    lblGrandBillAmount.Text = Convert.ToDecimal(grandBillAmount).ToString("0.00");
                    lblgrndcgstamnt.Text = Convert.ToDecimal(grandcgstamnt).ToString("0.00");
                    lblgrndigstamnt.Text = Convert.ToDecimal(grandigstamnt).ToString("0.00");
                    lblgrndsgstamnt.Text = Convert.ToDecimal(grandsgstamnt).ToString("0.00");
                    lblgrandRoundoff.Text = Convert.ToDecimal(grandRoundoff).ToString("0.00");
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void dtl_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label lbldtlDetailsNetQntl = (Label)e.Item.FindControl("lbldtlDetailsNetQntl");
            Label lbldtlDetailsSubTotal = (Label)e.Item.FindControl("lbldtlDetailsSubTotal");
            Label lbldtlDetailsExtraExp = (Label)e.Item.FindControl("lbldtlDetailsExtraExp");
            Label lbldtlDetailfrieght = (Label)e.Item.FindControl("lbldtlDetailfrieght");
            Label lbldtlDetailotheramt = (Label)e.Item.FindControl("lbldtlDetailotheramt");
            Label lbldtlDetailsBillAmount = (Label)e.Item.FindControl("lbldtlDetailsBillAmount");
            Label lbldtlDetailsroundoff = (Label)e.Item.FindControl("lbldtlDetailsroundoff");

            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            string Date = DateTime.Parse(lblDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            if (Mill_Code != string.Empty)
            {
                //qry = "SELECT 'J-'+convert(varchar(50),dbo.NT_1_JSaleHead.doc_no) as doc_no, Convert(varchar(10),dbo.NT_1_JSaleHead.doc_date,103) as doc_date, dbo.NT_1_JSaleHead.Cust_Code,dbo.NT_1_AccountMaster.Ac_Name_E, " +
                //      "dbo.NT_1_AccountMaster.Gst_No,SUM(dbo.qryjaggaryewaybillunion.Qty) AS Net_Wt, dbo.NT_1_JSaleHead.TaxableAmount, " +
                //      "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount,dbo.NT_1_JSaleHead.BillAmt,dbo.NT_1_JSaleHead.roundoff  " +
                //      "FROM dbo.NT_1_JSaleHead LEFT OUTER JOIN dbo.NT_1_AccountMaster LEFT OUTER JOIN " +
                //      "dbo.GSTStateMaster ON dbo.NT_1_AccountMaster.GSTStateCode = dbo.GSTStateMaster.State_Code LEFT OUTER JOIN " +
                //      "dbo.NT_1_CityMaster ON dbo.NT_1_AccountMaster.Company_Code = dbo.NT_1_CityMaster.company_code AND dbo.NT_1_AccountMaster.City_Code = dbo.NT_1_CityMaster.city_code ON " +
                //      "dbo.NT_1_JSaleHead.Cust_Code = dbo.NT_1_AccountMaster.Ac_Code AND dbo.NT_1_JSaleHead.Company_Code = dbo.NT_1_AccountMaster.Company_Code LEFT OUTER JOIN " +
                //      "dbo.qryjaggaryewaybillunion ON dbo.NT_1_JSaleHead.Year_Code = dbo.qryjaggaryewaybillunion.Year_Code AND " +
                //      "dbo.NT_1_JSaleHead.Company_Code = dbo.qryjaggaryewaybillunion.Company_Code AND dbo.NT_1_JSaleHead.doc_no = dbo.qryjaggaryewaybillunion.Bill_No  where dbo.NT_1_JSaleHead.Company_Code="
                //      + Convert.ToInt32(Session["Company_Code"].ToString()) + " and dbo.NT_1_JSaleHead.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                //      " dbo.NT_1_JSaleHead.doc_date='" + Date + "'  and dbo.NT_1_JSaleHead.Cust_Code=" + Mill_Code + " " +
                //      "GROUP BY dbo.NT_1_JSaleHead.doc_no, dbo.NT_1_JSaleHead.doc_date, dbo.NT_1_JSaleHead.Cust_Code, dbo.NT_1_JSaleHead.Company_Code, dbo.NT_1_JSaleHead.Year_Code, " +
                //      "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount, dbo.NT_1_JSaleHead.CGST_Rate, dbo.NT_1_JSaleHead.SGST_Rate, " +
                //      "dbo.NT_1_JSaleHead.GST_RateCode, dbo.NT_1_JSaleHead.TaxableAmount, dbo.NT_1_JSaleHead.BillAmt, dbo.NT_1_AccountMaster.Ac_Name_E, dbo.NT_1_AccountMaster.Gst_No, " +
                //      "dbo.NT_1_JSaleHead.IGST_Rate,dbo.NT_1_JSaleHead.roundoff";
                qry = "SELECT 'J-' + CONVERT(varchar(50), dbo.NT_1_JSaleHead.doc_no) AS doc_no, CONVERT(varchar(10), dbo.NT_1_JSaleHead.doc_date, 103) AS doc_date, dbo.NT_1_JSaleHead.Cust_Code, " +
                    "dbo.NT_1_AccountMaster.Ac_Name_E, dbo.NT_1_AccountMaster.Gst_No, SUM(dbo.qryjaggaryewaybillunion.Qty) AS Net_Wt, dbo.NT_1_JSaleHead.TaxableAmount, " +
                    "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount, dbo.NT_1_JSaleHead.BillAmt, dbo.NT_1_JSaleHead.roundoff " +
                    "FROM dbo.NT_1_JSaleHead LEFT OUTER JOIN dbo.NT_1_GSTRateMaster ON dbo.NT_1_JSaleHead.Company_Code = dbo.NT_1_GSTRateMaster.Company_Code AND " +
                    "dbo.NT_1_JSaleHead.GST_RateCode = dbo.NT_1_GSTRateMaster.Doc_no LEFT OUTER JOIN dbo.NT_1_AccountMaster LEFT OUTER JOIN " +
                    "dbo.GSTStateMaster ON dbo.NT_1_AccountMaster.GSTStateCode = dbo.GSTStateMaster.State_Code LEFT OUTER JOIN " +
                    "dbo.NT_1_CityMaster ON dbo.NT_1_AccountMaster.Company_Code = dbo.NT_1_CityMaster.company_code AND " +
                    "dbo.NT_1_AccountMaster.City_Code = dbo.NT_1_CityMaster.city_code ON dbo.NT_1_JSaleHead.Cust_Code = dbo.NT_1_AccountMaster.Ac_Code AND " +
                    "dbo.NT_1_JSaleHead.Company_Code = dbo.NT_1_AccountMaster.Company_Code LEFT OUTER JOIN " +
                    "dbo.qryjaggaryewaybillunion ON dbo.NT_1_JSaleHead.Year_Code = dbo.qryjaggaryewaybillunion.Year_Code AND " +
                    "dbo.NT_1_JSaleHead.Company_Code = dbo.qryjaggaryewaybillunion.Company_Code AND dbo.NT_1_JSaleHead.doc_no = dbo.qryjaggaryewaybillunion.Bill_No " +
                    "WHERE dbo.NT_1_JSaleHead.Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND dbo.NT_1_JSaleHead.Year_Code = " + Convert.ToInt32(Session["year"].ToString()) + " AND dbo.NT_1_JSaleHead.doc_date = '" + Date + "' and dbo.NT_1_JSaleHead.Cust_Code=" + Mill_Code + " and dbo.NT_1_GSTRateMaster.Rate!=0 " +
                    "GROUP BY dbo.NT_1_JSaleHead.doc_no, dbo.NT_1_JSaleHead.doc_date, dbo.NT_1_JSaleHead.Cust_Code, dbo.NT_1_JSaleHead.Company_Code, dbo.NT_1_JSaleHead.Year_Code, " +
                    "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount, dbo.NT_1_JSaleHead.CGST_Rate, dbo.NT_1_JSaleHead.SGST_Rate, " +
                    "dbo.NT_1_JSaleHead.GST_RateCode, dbo.NT_1_JSaleHead.TaxableAmount, dbo.NT_1_JSaleHead.BillAmt, dbo.NT_1_AccountMaster.Ac_Name_E, dbo.NT_1_AccountMaster.Gst_No, " +
                    "dbo.NT_1_JSaleHead.IGST_Rate, dbo.NT_1_JSaleHead.roundoff";
             

            }
            else
            {


                qry = "SELECT 'J-' + CONVERT(varchar(50), dbo.NT_1_JSaleHead.doc_no) AS doc_no, CONVERT(varchar(10), dbo.NT_1_JSaleHead.doc_date, 103) AS doc_date, dbo.NT_1_JSaleHead.Cust_Code, " +
                      "dbo.NT_1_AccountMaster.Ac_Name_E, dbo.NT_1_AccountMaster.Gst_No, SUM(dbo.qryjaggaryewaybillunion.Qty) AS Net_Wt, dbo.NT_1_JSaleHead.TaxableAmount, " +
                      "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount, dbo.NT_1_JSaleHead.BillAmt, dbo.NT_1_JSaleHead.roundoff " +
                      "FROM dbo.NT_1_JSaleHead LEFT OUTER JOIN dbo.NT_1_GSTRateMaster ON dbo.NT_1_JSaleHead.Company_Code = dbo.NT_1_GSTRateMaster.Company_Code AND " +
                      "dbo.NT_1_JSaleHead.GST_RateCode = dbo.NT_1_GSTRateMaster.Doc_no LEFT OUTER JOIN dbo.NT_1_AccountMaster LEFT OUTER JOIN " +
                      "dbo.GSTStateMaster ON dbo.NT_1_AccountMaster.GSTStateCode = dbo.GSTStateMaster.State_Code LEFT OUTER JOIN " +
                      "dbo.NT_1_CityMaster ON dbo.NT_1_AccountMaster.Company_Code = dbo.NT_1_CityMaster.company_code AND " +
                      "dbo.NT_1_AccountMaster.City_Code = dbo.NT_1_CityMaster.city_code ON dbo.NT_1_JSaleHead.Cust_Code = dbo.NT_1_AccountMaster.Ac_Code AND " +
                      "dbo.NT_1_JSaleHead.Company_Code = dbo.NT_1_AccountMaster.Company_Code LEFT OUTER JOIN " +
                      "dbo.qryjaggaryewaybillunion ON dbo.NT_1_JSaleHead.Year_Code = dbo.qryjaggaryewaybillunion.Year_Code AND " +
                      "dbo.NT_1_JSaleHead.Company_Code = dbo.qryjaggaryewaybillunion.Company_Code AND dbo.NT_1_JSaleHead.doc_no = dbo.qryjaggaryewaybillunion.Bill_No " +
                      "WHERE dbo.NT_1_JSaleHead.Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND dbo.NT_1_JSaleHead.Year_Code = " + Convert.ToInt32(Session["year"].ToString()) + " AND dbo.NT_1_JSaleHead.doc_date = '" + Date + "' and dbo.NT_1_GSTRateMaster.Rate!=0 " +
                      "GROUP BY dbo.NT_1_JSaleHead.doc_no, dbo.NT_1_JSaleHead.doc_date, dbo.NT_1_JSaleHead.Cust_Code, dbo.NT_1_JSaleHead.Company_Code, dbo.NT_1_JSaleHead.Year_Code, " +
                      "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount, dbo.NT_1_JSaleHead.CGST_Rate, dbo.NT_1_JSaleHead.SGST_Rate, " +
                      "dbo.NT_1_JSaleHead.GST_RateCode, dbo.NT_1_JSaleHead.TaxableAmount, dbo.NT_1_JSaleHead.BillAmt, dbo.NT_1_AccountMaster.Ac_Name_E, dbo.NT_1_AccountMaster.Gst_No, " +
                      "dbo.NT_1_JSaleHead.IGST_Rate, dbo.NT_1_JSaleHead.roundoff";
                //qry = "SELECT 'J-'+convert(varchar(50),dbo.NT_1_JSaleHead.doc_no) as doc_no, Convert(varchar(10),dbo.NT_1_JSaleHead.doc_date,103) as doc_date, dbo.NT_1_JSaleHead.Cust_Code,dbo.NT_1_AccountMaster.Ac_Name_E, " +
                //      "dbo.NT_1_AccountMaster.Gst_No,SUM(dbo.qryjaggaryewaybillunion.Qty) AS Net_Wt, dbo.NT_1_JSaleHead.TaxableAmount, " +
                //      "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount,dbo.NT_1_JSaleHead.BillAmt,dbo.NT_1_JSaleHead.roundoff  " +
                //      "FROM dbo.NT_1_JSaleHead LEFT OUTER JOIN dbo.NT_1_AccountMaster LEFT OUTER JOIN " +
                //      "dbo.GSTStateMaster ON dbo.NT_1_AccountMaster.GSTStateCode = dbo.GSTStateMaster.State_Code LEFT OUTER JOIN " +
                //      "dbo.NT_1_CityMaster ON dbo.NT_1_AccountMaster.Company_Code = dbo.NT_1_CityMaster.company_code AND dbo.NT_1_AccountMaster.City_Code = dbo.NT_1_CityMaster.city_code ON " +
                //      "dbo.NT_1_JSaleHead.Cust_Code = dbo.NT_1_AccountMaster.Ac_Code AND dbo.NT_1_JSaleHead.Company_Code = dbo.NT_1_AccountMaster.Company_Code LEFT OUTER JOIN " +
                //      "dbo.qryjaggaryewaybillunion ON dbo.NT_1_JSaleHead.Year_Code = dbo.qryjaggaryewaybillunion.Year_Code AND " +
                //      "dbo.NT_1_JSaleHead.Company_Code = dbo.qryjaggaryewaybillunion.Company_Code AND dbo.NT_1_JSaleHead.doc_no = dbo.qryjaggaryewaybillunion.Bill_No  where dbo.NT_1_JSaleHead.Company_Code="
                //      + Convert.ToInt32(Session["Company_Code"].ToString()) + " and dbo.NT_1_JSaleHead.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                //      " dbo.NT_1_JSaleHead.doc_date='" + Date + "'" +
                //      " GROUP BY dbo.NT_1_JSaleHead.doc_no, dbo.NT_1_JSaleHead.doc_date, dbo.NT_1_JSaleHead.Cust_Code, dbo.NT_1_JSaleHead.Company_Code, dbo.NT_1_JSaleHead.Year_Code, " +
                //      "dbo.NT_1_JSaleHead.IGST_Amount, dbo.NT_1_JSaleHead.CGST_Amount, dbo.NT_1_JSaleHead.SGST_Amount, dbo.NT_1_JSaleHead.CGST_Rate, dbo.NT_1_JSaleHead.SGST_Rate, " +
                //      "dbo.NT_1_JSaleHead.GST_RateCode, dbo.NT_1_JSaleHead.TaxableAmount, dbo.NT_1_JSaleHead.BillAmt, dbo.NT_1_AccountMaster.Ac_Name_E, dbo.NT_1_AccountMaster.Gst_No, " +
                //      "dbo.NT_1_JSaleHead.IGST_Rate ,dbo.NT_1_JSaleHead.roundoff";
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                dt.Columns.Add(new DataColumn("doc_no", typeof(string)));
                dt.Columns.Add(new DataColumn("doc_date", typeof(string)));
                dt.Columns.Add(new DataColumn("Ac_Name_E", typeof(string)));
                dt.Columns.Add(new DataColumn("Gst_No", typeof(string)));
                dt.Columns.Add(new DataColumn("Net_Wt", typeof(string)));
                dt.Columns.Add(new DataColumn("Net_Wt1", typeof(double)));
                dt.Columns.Add(new DataColumn("TaxableAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxableAmount1", typeof(double)));
                dt.Columns.Add(new DataColumn("CGST_Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("CGST_Amount1", typeof(double)));
                dt.Columns.Add(new DataColumn("SGST_Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("SGST_Amount1", typeof(double)));
                dt.Columns.Add(new DataColumn("IGST_Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("IGST_Amount1", typeof(double)));
                dt.Columns.Add(new DataColumn("roundoff", typeof(double)));
                dt.Columns.Add(new DataColumn("roundoff1", typeof(double)));
                dt.Columns.Add(new DataColumn("BillAmt", typeof(string)));
                dt.Columns.Add(new DataColumn("BillAmt1", typeof(double)));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["doc_no"] = ds.Tables[0].Rows[i]["doc_no"].ToString();
                        dr["doc_date"] = ds.Tables[0].Rows[i]["doc_date"].ToString();

                        dr["Net_Wt"] = ds.Tables[0].Rows[i]["Net_Wt"].ToString();
                        dr["Net_Wt1"] = ds.Tables[0].Rows[i]["Net_Wt"].ToString();

                        dr["Ac_Name_E"] = ds.Tables[0].Rows[i]["Ac_Name_E"].ToString();

                        dr["TaxableAmount"] = ds.Tables[0].Rows[i]["TaxableAmount"].ToString();
                        dr["TaxableAmount1"] = ds.Tables[0].Rows[i]["TaxableAmount"].ToString();

                        dr["BillAmt"] = ds.Tables[0].Rows[i]["BillAmt"].ToString();
                        dr["BillAmt1"] = ds.Tables[0].Rows[i]["BillAmt"].ToString();


                        dr["Gst_No"] = ds.Tables[0].Rows[i]["Gst_No"].ToString();
                        dr["CGST_Amount"] = ds.Tables[0].Rows[i]["CGST_Amount"].ToString();
                        dr["CGST_Amount1"] = ds.Tables[0].Rows[i]["CGST_Amount"].ToString();

                        dr["SGST_Amount"] = ds.Tables[0].Rows[i]["SGST_Amount"].ToString();
                        dr["SGST_Amount1"] = ds.Tables[0].Rows[i]["SGST_Amount"].ToString();

                        dr["IGST_Amount"] = ds.Tables[0].Rows[i]["IGST_Amount"].ToString();
                        dr["IGST_Amount1"] = ds.Tables[0].Rows[i]["IGST_Amount"].ToString();

                        dr["roundoff"] = ds.Tables[0].Rows[i]["roundoff"].ToString();
                        dr["roundoff1"] = ds.Tables[0].Rows[i]["roundoff"].ToString();



                        dt.Rows.Add(dr);
                    }
                    double qntl = Convert.ToDouble(dt.Compute("SUM(Net_Wt1)", string.Empty));
                    double Subtotal = Convert.ToDouble(dt.Compute("SUM(TaxableAmount1)", string.Empty));

                    double BillAmount = Math.Round(Convert.ToDouble(dt.Compute("SUM(BillAmt1)", string.Empty)), 2);

                    double CGSTAmount = Convert.ToDouble(dt.Compute("SUM(CGST_Amount1)", string.Empty));
                    double SGSTAmount = Convert.ToDouble(dt.Compute("SUM(SGST_Amount1)", string.Empty));
                    double IGSTAmount = Math.Round(Convert.ToDouble(dt.Compute("SUM(IGST_Amount1)", string.Empty)), 2);
                    double roundoff = Math.Round(Convert.ToDouble(dt.Compute("SUM(roundoff1)", string.Empty)), 2);

                    lbldtlDetailsNetQntl.Text = Convert.ToString(qntl);
                    lbldtlDetailsSubTotal.Text = Convert.ToString(Subtotal);
                   
                    lbldtlDetailsBillAmount.Text = Convert.ToString(BillAmount);
                    lbldtlDetailsroundoff.Text = Convert.ToString(roundoff);
                    grandNetQntl += qntl;
                    grandSubtotal += Subtotal;
                    
                    grandBillAmount += BillAmount;

                    grandcgstamnt += CGSTAmount;
                    grandsgstamnt += SGSTAmount;
                    grandigstamnt += IGSTAmount;
                    grandRoundoff += roundoff;

                    if (dt.Rows.Count > 0)
                    {
                        if (Mill_Code != string.Empty)
                        {
                            lblBrokerName.Text = "Mill Name :" + " " + clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + Mill_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        }
                        else
                        {
                            lblBrokerName.Text = "All Mills";
                        }
                        lblDate1.Text = "Sale List From :" + fromDT + " To " + toDT;
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
}