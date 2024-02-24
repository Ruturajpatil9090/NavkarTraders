using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class Report_rptSaleregisterWithQty : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;

    DataSet ds;
    DataTable dt;
    string stritemcode = "1";
    string fromDTnew;
    string toDTnew;
    double total = 0.00;
    double shubamnt = 0.00;
    Int32 totalqty = 0;
    double ppolamnt = 0.00;
    double postphone = 0.00;
    double roundoff = 0.00;
    double billamnt = 0.00;
    Int32 Small = 0;
    Int32 big = 0;
    Int32 box = 0;
    Int32 Fivekg = 0;
    Int32 TenKg = 0;
    Int32 thirtyKg = 0;
    Int32 FivekgBox = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!IsPostBack)
        {
            fromDT = Request.QueryString["Fromdate"];
            toDT = Request.QueryString["Todate"];
            lblCompanyName.Text = Session["Company_Name"].ToString();
            fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            lblfrotodate.Text = "From " + fromDT + " To " + toDT;

            this.BindList();
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
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=JaggarySaleRegister.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();
    }
    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            //qry = "Select distinct convert(varchar(10),[doc_date],103) as doc_date  from " + tblPrefix + "qryJsaleReport where doc_date between'" + fromDTnew
            //    + "' and '" + toDTnew + "' and Company_Code="
            //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by doc_date";

            qry = "Select distinct convert(varchar(10),[doc_date],102) as doc_date  from " + tblPrefix + "qryForSaleRegisterQty where doc_date between'" + fromDTnew
                + "' and '" + toDTnew + "' and Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by doc_date";

            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            //ds = obj.GetDataSet(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    dtl.DataSource = dt;
                    dtl.DataBind();
                    lbltotal.Text = Convert.ToString(total);
                    lblshubamnt.Text = Convert.ToString(shubamnt);
                    lblppolamnttotal.Text = Convert.ToString(ppolamnt);
                    lblpostphtotl.Text = Convert.ToString(postphone);
                    lblroundoftotal.Text = Convert.ToString(roundoff);
                    lblbillamnttotal.Text = Convert.ToString(billamnt);
                    //  lblsmalltotal.Text = Convert.ToString(Small);
                    // lblBigTotal.Text = Convert.ToString(big);
                    lblBoxTotal.Text = Convert.ToString(box);
                    lblFiveKGTotal.Text = Convert.ToString(Fivekg);
                    lblTenKGtotal.Text = Convert.ToString(TenKg);
                    lblThirtyKGTotal.Text = Convert.ToString(thirtyKg);
                    lblfivebox.Text = Convert.ToString(FivekgBox);
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

    protected void dtl_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                Label item_code = (Label)e.Item.FindControl("lblItemCode");
                stritemcode = item_code.Text;

                string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                //string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                //stritemcode = item_code.Text.ToString("dd/MM/yyyy");
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
                //qry = "select distinct doc_no,Supplier,netqty,purchasevalue,MARKETSES,supercost,levihead,adat,tdsamount,AMOUNT,"
                //    + " convert(DECIMAL(10,2),round((AMOUNT)-(tdsamount),2)) as payable from NT_1_qryAwakPurchase where Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                //    + " and DOC_DATE='" + date + "'";

                qry = "select distinct [doc_no],convert(varchar(10),[doc_date],103) as doc_date,[Ac_Name_E],[Total],[Khajarat],[Shub_Amount],[P_Pol_Amount],[PostPhone],[roundoff],[BillAmt],isnull(ThirtyKg,0) as thirteenkg,isnull(TenKg,0) as tenkg,isnull(Fivekg,0) as KG,isnull(Box,0) as Box,isnull(Fivekgbox,0) as Fivekgbox"
                  + " from " + tblPrefix + "qryForSaleRegisterQty where Company_Code="
                  + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                  + " and DOC_DATE='" + date + "'";

                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);
                //dsMill = obj.GetDataSet(qry);

                //double adat = 0.0;
                //double tdsamount = 0.0;


                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];

                        //Label lblbillamount = (Label)e.Item.FindControl("lblbillamount");
                        //Label lblpayable = (Label)e.Item.FindControl("lblpayable");
                        //lblInward.Text = netInward.ToString();
                        //lblNetInwardValue.Text = NetInwardValue.ToString();
                        //lblOutward.Text = lblOutward.ToString();
                        //lblBalance.Text = bal.ToString();

                        double tot = Convert.ToDouble(dtMill.Compute("SUM(Total)", string.Empty));
                        total += tot;

                        double shub = Convert.ToDouble(dtMill.Compute("SUM(Shub_Amount)", string.Empty));
                        shubamnt += Math.Round(shub, 2);

                        //Int32 qtys = Convert.ToInt32(dtMill.Compute("SUM(Qtys)", string.Empty));
                        //totalqty += qtys;

                        double ppol = Convert.ToDouble(dtMill.Compute("SUM(P_Pol_Amount)", string.Empty));
                        ppolamnt += ppol;

                        double postp = Convert.ToDouble(dtMill.Compute("SUM(PostPhone)", string.Empty));
                        postphone += postp;

                        double round = Convert.ToDouble(dtMill.Compute("SUM(roundoff)", string.Empty));
                        roundoff += round;

                        double bilamnt = Convert.ToDouble(dtMill.Compute("SUM(BillAmt)", string.Empty));
                        billamnt += bilamnt;

                        //Int32 smallqty = Convert.ToInt32(dtMill.Compute("SUM(Small)", string.Empty));
                        //Small += smallqty;
                        Int32 thirtyqty = Convert.ToInt32(dtMill.Compute("SUM(thirteenkg)", string.Empty));
                        thirtyKg += thirtyqty;

                        Int32 tenqty = Convert.ToInt32(dtMill.Compute("SUM(tenkg)", string.Empty));
                        TenKg += tenqty;

                        Int32 fiveqty = Convert.ToInt32(dtMill.Compute("SUM(KG)", string.Empty));
                        Fivekg += fiveqty;
                       // Int32 Bigqty = Convert.ToInt32(dtMill.Compute("SUM(Big)", string.Empty));
                       // big += Bigqty;
                        Int32 Boxqty = Convert.ToInt32(dtMill.Compute("SUM(Box)", string.Empty));
                        box += Boxqty;

                        Int32 fiveBoxqty = Convert.ToInt32(dtMill.Compute("SUM(Fivekgbox)", string.Empty));
                        FivekgBox += fiveBoxqty; 

                     

                       

                       

                        dtlDetails.DataSource = dtMill;
                        dtlDetails.DataBind();

                    }
                }
                //}
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}