using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class Report_rptMillPaymentForGST : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    double GrandTotalQuintal = 0.00;
    double GrandTotalAmount = 0.00;
    double GrandTotalTCSAmt = 0.00;
    double GrandTotalTDSAmt = 0.00;
    double GrandTotalNteTCSAmt = 0.00;
    string Mill_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Mill_Code = Request.QueryString["Mill_Code"].ToString();
        //Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {
            BindList();
        }
    }

    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            if (Mill_Code == string.Empty || Mill_Code=="0")
            {
                //qry = "SELECT DISTINCT(d.Payment_To) as millcode,d.paymentshortname as millname FROM  qrydohead AS d " +
                //    "WHERE d.purc_no!=0 and  d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and d.Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Payment_To<>'' ";
                qry = "SELECT DISTINCT dbo.nt_1_tender.Payment_To as millcode, dbo.nt_1_accountmaster.Short_Name as millname FROM " +
                      "dbo.nt_1_accountmaster RIGHT OUTER JOIN dbo.nt_1_tender ON dbo.nt_1_accountmaster.accoid = dbo.nt_1_tender.pt RIGHT OUTER JOIN " +
                      "dbo.nt_1_deliveryorder ON dbo.nt_1_tender.Company_Code = dbo.nt_1_deliveryorder.company_code AND dbo.nt_1_tender.Tender_No = dbo.nt_1_deliveryorder.purc_no " +
                      "WHERE dbo.nt_1_deliveryorder.purc_no!=0 and  dbo.nt_1_deliveryorder.doc_date between '" + fromDT + "' and '" + toDT + "' " +
                      "and dbo.nt_1_deliveryorder.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and dbo.nt_1_deliveryorder.Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " and dbo.nt_1_tender.Payment_To<>''"; 
            }
            else
            {
                //qry = "SELECT DISTINCT(d.Payment_To) as millcode,d.paymentshortname as millname FROM  qrydohead AS d " +
                //  "WHERE d.purc_no!=0 and  d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //  " and d.Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Payment_To<>'' and Payment_To='" + Mill_Code + "'";
                qry = "SELECT DISTINCT dbo.nt_1_tender.Payment_To as millcode, dbo.nt_1_accountmaster.Short_Name as millname FROM " +
                     "dbo.nt_1_accountmaster RIGHT OUTER JOIN dbo.nt_1_tender ON dbo.nt_1_accountmaster.accoid = dbo.nt_1_tender.pt RIGHT OUTER JOIN " +
                     "dbo.nt_1_deliveryorder ON dbo.nt_1_tender.Company_Code = dbo.nt_1_deliveryorder.company_code AND dbo.nt_1_tender.Tender_No = dbo.nt_1_deliveryorder.purc_no " +
                     "WHERE dbo.nt_1_deliveryorder.purc_no!=0 and  dbo.nt_1_deliveryorder.doc_date between '" + fromDT + "' and '" + toDT + "' " +
                     "and dbo.nt_1_deliveryorder.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and dbo.nt_1_deliveryorder.Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " and dbo.nt_1_tender.Payment_To<>'' and Payment_To='" + Mill_Code + "'";

            }
            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add(new DataColumn("Balance", typeof(string)));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string millCode = ds.Tables[0].Rows[i]["millcode"].ToString();
                    string balance = clsCommon.getString("select SUM(case DRCR when 'D' then AMOUNT when 'C' then -AMOUNT end) as Balance from " + tblPrefix + "GLEDGER" +
                                         " where AC_CODE=" + millCode + " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                                         " and YEAR_CODE<=" + Convert.ToInt32(Session["year"].ToString()) + " group by AC_CODE");
                    ds.Tables[0].Rows[i]["Balance"] = balance;
                }
                dtlList.DataSource = ds;
                dtlList.DataBind();
                lblGrandTotalQuintal.Text = GrandTotalQuintal.ToString();
                lblGrandTotalAmount.Text = GrandTotalAmount.ToString();
                lblGrandTotalTCSAmt.Text = GrandTotalTCSAmt.ToString();
                lblGrandTotalTDSAmt.Text = GrandTotalTDSAmt.ToString();
                lblGrandTotalNteTCSAmt.Text = GrandTotalNteTCSAmt.ToString();
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

    protected void dtlList_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        //using (clsDataProvider obj = new clsDataProvider())
        //{
        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        Label lblMillCode = (Label)e.Item.FindControl("lblMillCode");
        Label lblTotalQuintal = (Label)e.Item.FindControl("lblTotalQuintal");
        Label lblTotalAmount = (Label)e.Item.FindControl("lblTotalAmount");
        Label lblTotalNteTCSAmt = (Label)e.Item.FindControl("lblTotalNteTCSAmt");
        Label lblTotalTCSAmt = (Label)e.Item.FindControl("lblTotalTCSAmt");
        Label lblTotalTDSAmt = (Label)e.Item.FindControl("lblTotalTDSAmt");
        string millCode = lblMillCode.Text;
        //string qryDetails = "select SUM(qtl) as qtl,mill_rate,excise_rate as gst_rate,(mill_rate + excise_rate) as totalmillrate,CAST((SUM(qtl) * (mill_rate + excise_rate)) as NUMERIC(18,2)) as millamount,TCS_Rate,(CAST((SUM(qtl) * (mill_rate + excise_rate)) as NUMERIC(18,2))*TCS_Rate/100) as TCSAmt,0.00 as TCSNetPayable from " 
        //    + tblPrefix + "qryMillPayment2 where doc_date between '" + fromDT + "' and '" + toDT + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) 
        //    + " and mill_Code=" + millCode + " group by mill_rate,excise_rate,TCS_Rate";


        //string qryDetails = "select SUM(quantal) as qtl,mill_rate,excise_rate as gst_rate,(mill_rate + excise_rate) as totalmillrate," +
        //                     " CAST((SUM(quantal) * (mill_rate + excise_rate)) as NUMERIC(18,2)) as millamount,isnull(TCS_Rate,0) as TCS_Rate ,(CAST((SUM(quantal) * (mill_rate + excise_rate)) as NUMERIC(18,2))*isnull(TCS_Rate,0)/100) as TCSAmt " +
        //                     ",isnull(PurchaseTDSRate,0) as TDSRate,(CAST((SUM(quantal) * (mill_rate)) as NUMERIC(18,2))*isnull(PurchaseTDSRate,0)/100) as TDSAmt " +
        //                      " ,(CAST((SUM(quantal) * (mill_rate + excise_rate)) as NUMERIC(18,2))+(CAST((SUM(quantal) * (mill_rate + excise_rate)) as NUMERIC(18,2))*isnull(TCS_Rate,0)/100)-(CAST((SUM(quantal) * (mill_rate)) as NUMERIC(18,2))*isnull(PurchaseTDSRate,0)/100)) as TCSNetPayable ,doc_no as dono" +
        //                      " from qrydohead where  doc_date between '" + fromDT + "' and '" + toDT + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
        //                     + " and Payment_To=" + millCode + " group by mill_rate,excise_rate,TCS_Rate,PurchaseTDSRate,amount,TDSAmt,doc_no order by doc_no";

        string qryDetails = "select SUM(dbo.nt_1_deliveryorder.quantal) as qtl,dbo.nt_1_deliveryorder.mill_rate,dbo.nt_1_deliveryorder.excise_rate as gst_rate,(dbo.nt_1_deliveryorder.mill_rate + dbo.nt_1_deliveryorder.excise_rate) as totalmillrate, " +
                            "CAST((SUM(dbo.nt_1_deliveryorder.quantal) * (dbo.nt_1_deliveryorder.mill_rate + dbo.nt_1_deliveryorder.excise_rate)) as NUMERIC(18,2)) as millamount,isnull(dbo.nt_1_deliveryorder.TCS_Rate,0) as TCS_Rate ," +
                            "(CAST((SUM(dbo.nt_1_deliveryorder.quantal) * (dbo.nt_1_deliveryorder.mill_rate + dbo.nt_1_deliveryorder.excise_rate)) as NUMERIC(18,2))*isnull(dbo.nt_1_deliveryorder.TCS_Rate,0)/100) as TCSAmt ," +
                            "isnull(PurchaseTDSRate,0) as TDSRate,(CAST((SUM(dbo.nt_1_deliveryorder.quantal) * (dbo.nt_1_deliveryorder.mill_rate)) as NUMERIC(18,2))*isnull(PurchaseTDSRate,0)/100) as TDSAmt  ," +
                            "(CAST((SUM(dbo.nt_1_deliveryorder.quantal) * (dbo.nt_1_deliveryorder.mill_rate + dbo.nt_1_deliveryorder.excise_rate)) as NUMERIC(18,2))+(CAST((SUM(dbo.nt_1_deliveryorder.quantal) * (dbo.nt_1_deliveryorder.mill_rate + dbo.nt_1_deliveryorder.excise_rate)) as NUMERIC(18,2))*isnull(dbo.nt_1_deliveryorder.TCS_Rate,0)/100)-(CAST((SUM(dbo.nt_1_deliveryorder.quantal) * (dbo.nt_1_deliveryorder.mill_rate)) as NUMERIC(18,2))*isnull(PurchaseTDSRate,0)/100)) as TCSNetPayable ,doc_no as dono " +
                            "FROM dbo.nt_1_accountmaster RIGHT OUTER JOIN dbo.nt_1_tender ON dbo.nt_1_accountmaster.accoid = dbo.nt_1_tender.pt RIGHT OUTER JOIN " +
                            "dbo.nt_1_deliveryorder ON dbo.nt_1_tender.Company_Code = dbo.nt_1_deliveryorder.company_code AND dbo.nt_1_tender.Tender_No = dbo.nt_1_deliveryorder.purc_no " +
                            "where  doc_date between '" + fromDT + "' and '" + toDT + "' and dbo.nt_1_deliveryorder.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and dbo.nt_1_deliveryorder.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and dbo.nt_1_tender.Payment_To=" + millCode + " group by dbo.nt_1_deliveryorder.mill_rate,dbo.nt_1_deliveryorder.excise_rate,dbo.nt_1_deliveryorder.TCS_Rate,PurchaseTDSRate,amount,TDSAmt,doc_no order by doc_no";

        DataSet dsDetails = new DataSet();
        dt = new DataTable();
        //dsDetails = obj.GetDataSet(qryDetails);
        dsDetails = clsDAL.SimpleQuery(qryDetails);
        if (dsDetails.Tables[0].Rows.Count > 0)
        {
            dt = dsDetails.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double Quintal = Convert.ToDouble(dt.Rows[i]["qtl"].ToString());
                double Amount = Convert.ToDouble(dt.Rows[i]["millamount"].ToString());
                double TCSAmt = Convert.ToDouble(dt.Rows[i]["TCSAmt"].ToString());
                double TDSAmt = Convert.ToDouble(dt.Rows[i]["TDSAmt"].ToString());
                double NetPayable = Convert.ToDouble(dt.Rows[i]["TCSNetPayable"].ToString());
                TCSAmt = Math.Round(TCSAmt, 2);
                NetPayable = Math.Round(NetPayable, 2);
                dt.Rows[i]["TCSAmt"] = TCSAmt;
                dt.Rows[i]["TDSAmt"] = TDSAmt;
                dt.Rows[i]["TCSNetPayable"] = NetPayable;

                GrandTotalQuintal += Quintal;
                GrandTotalAmount += Amount;
                GrandTotalTCSAmt += TCSAmt;
                GrandTotalTDSAmt += TDSAmt;
                GrandTotalNteTCSAmt += NetPayable;
            }


            dtlDetails.DataSource = dt;
            dtlDetails.DataBind();
            lblTotalQuintal.Text = dt.Compute("SUM(qtl)", string.Empty).ToString();
            lblTotalAmount.Text = dt.Compute("SUM(millamount)", string.Empty).ToString();
            lblTotalTCSAmt.Text = dt.Compute("SUM(TCSAmt)", string.Empty).ToString();
            lblTotalTDSAmt.Text = dt.Compute("SUM(TDSAmt)", string.Empty).ToString();
            lblTotalNteTCSAmt.Text = dt.Compute("SUM(TCSNetPayable)", string.Empty).ToString();
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