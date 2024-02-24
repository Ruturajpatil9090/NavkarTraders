using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class Sugar_Report_rptJaggerryDetailSalebill : System.Web.UI.Page
{
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string billno = string.Empty;
    double billamntfinal = 0.00;
    double insuranceamnttotal = 0.00;
    double hamaliamnttotal = 0.00;
    double packingamnttotal = 0.00;
    double levitotal = 0.00;
    double salevaluetotal = 0.00;
    double qtytotal = 0.00;
    double totalnetwt = 0.00;

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        billno = Request.QueryString["FromBill"];
        if (!Page.IsPostBack)
        {
            BindList();
        }

    }

    private void BindList()
    {
        try
        {


            //qry = "  select distinct([Item_Code]),doc_no,CONVERT(varchar(10),Doc_Date,103) as Doc_Date,PartyName " +
            //     " from [NT_1_JawakSaleBillPrint]   where doc_no='" + billno + "' and Company_Code= "
            //     + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by doc_no";

            qry = "  select distinct(doc_no),CONVERT(varchar(10),Doc_Date,103) as Doc_Date,PartyName " +
                " from [NT_1_JawakSaleBillPrint]   where doc_no='" + billno + "' and Company_Code= "
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by doc_no";


            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //lblCmpName.Text = clsCommon.getString("Select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    //lblTransportName.Text = "Balance Stock As On <b>" + System.DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss tt") + "</b>";
                    dtlist.DataSource = dt;
                    dtlist.DataBind();
                }
                else
                {
                    dtlist.DataSource = null;
                    dtlist.DataBind();
                }
            }
            else
            {
                dtlist.DataSource = null;
                dtlist.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlist_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            //Label lblitemcode = (Label)e.Item.FindControl("lblitemcode");
            Label lbldocno = (Label)e.Item.FindControl("lbldocno");

            //Label lblDispTotal = (Label)e.Item.FindControl("lblDispTotal");
            //Label lblBalTotal = (Label)e.Item.FindControl("lblBalTotal");
            //Label lblqtltotal = (Label)e.Item.FindControl("lblqtltotal");

            //Label lblsaleamnttotal = (Label)e.Item.FindControl("lblsaleamnttotal");


            // string itemcode = lblitemcode.Text;
            string docno = lbldocno.Text;



            //qry = "select Itemname,CONVERT(varchar(10),Doc_Date,103) as Doc_Date,Qty,Net_Wt,Net_Wt as Net_Wt1," +
            //    " Sale_Rate,Sale_Rate as Sale_Rate1,Comm_Rate,Comm_Rate as Comm_Rate1,Levi_Amt,Levi_Amt as Levi_Amt1,convert(decimal(10,2),Packing_Rate) as Packing_Rate ," +
            //    " Packing_Amnt,Packing_Amnt as Packing_Amnt1,convert(decimal(10,2),Hamali_Rate) as Hamali_Rate,Hamali_Amnt,Hamali_Amnt as Hamali_Amnt1," +
            //    " Insurance_Amt as Insurance_Amt1,Insurance_Amt,Panjar_Amnt as Panjar_Amnt1,Panjar_Amnt from NT_1_JawakSaleBillPrint " +
            //    " where Item_Code='" + itemcode + "' and doc_no='" + docno + "' and Company_Code= " + Convert.ToInt32(Session["Company_Code"].ToString());

            qry = "select Itemname,CONVERT(varchar(10),Doc_Date,103) as Doc_Date,Qty,Net_Wt,Net_Wt as Net_Wt1," +
                " Sale_Rate,Sale_Rate as Sale_Rate1,Comm_Rate,Comm_Rate as Comm_Rate1,Levi_Amt,Levi_Amt as Levi_Amt1,convert(decimal(10,2),Packing_Rate) as Packing_Rate ," +
                " Packing_Amnt,Packing_Amnt as Packing_Amnt1,convert(decimal(10,2),Hamali_Rate) as Hamali_Rate,Hamali_Amnt,Hamali_Amnt as Hamali_Amnt1," +
                " Insurance_Amt as Insurance_Amt1,Insurance_Amt,Panjar_Amnt as Panjar_Amnt1,Panjar_Amnt from NT_1_JawakSaleBillPrint " +
                " where  doc_no='" + docno + "' and Company_Code= " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by Itemname desc";




            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                //dt.Columns.Add(new DataColumn("Tender_No", typeof(string)));
                dt.Columns.Add(new DataColumn("Itemname", typeof(string)));
                //dt.Columns.Add(new DataColumn("Doc_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(double)));
                dt.Columns.Add(new DataColumn("Net_Wt", typeof(string)));
                dt.Columns.Add(new DataColumn("Net_Wt1", typeof(double)));


                dt.Columns.Add(new DataColumn("Sale_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Sale_Rate1", typeof(double)));

                dt.Columns.Add(new DataColumn("Comm_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Comm_Rate1", typeof(double)));

                dt.Columns.Add(new DataColumn("Levi_Amt", typeof(string)));
                dt.Columns.Add(new DataColumn("Levi_Amt1", typeof(double)));


                dt.Columns.Add(new DataColumn("Packing_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Packing_Amnt", typeof(string)));
                dt.Columns.Add(new DataColumn("Packing_Amnt1", typeof(double)));

                dt.Columns.Add(new DataColumn("Hamali_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Hamali_Amnt", typeof(string)));
                dt.Columns.Add(new DataColumn("Hamali_Amnt1", typeof(double)));

                dt.Columns.Add(new DataColumn("Insurance_Amt", typeof(string)));
                dt.Columns.Add(new DataColumn("Insurance_Amt1", typeof(double)));

                dt.Columns.Add(new DataColumn("Panjar_Amnt", typeof(string)));
                dt.Columns.Add(new DataColumn("Panjar_Amnt1", typeof(double)));

                dt.Columns.Add(new DataColumn("Gross_Amnt", typeof(double)));
                dt.Columns.Add(new DataColumn("com_sr_rate", typeof(double)));
                dt.Columns.Add(new DataColumn("salevalue", typeof(double)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    //dr["Tender_No"] = ds.Tables[0].Rows[i]["Tender_No"].ToString();
                    dr["Itemname"] = ds.Tables[0].Rows[i]["Itemname"].ToString();
                    // dr["Doc_Date"] = ds.Tables[0].Rows[i]["Doc_Date"].ToString();
                    dr["Qty"] = ds.Tables[0].Rows[i]["Qty"].ToString();
                    double qty1 = Convert.ToDouble(ds.Tables[0].Rows[i]["Qty"].ToString());
                    dr["Net_Wt"] = ds.Tables[0].Rows[i]["Net_Wt"].ToString();

                    dr["Sale_Rate"] = ds.Tables[0].Rows[i]["Sale_Rate"].ToString();

                    dr["Comm_Rate"] = ds.Tables[0].Rows[i]["Comm_Rate"].ToString();
                    dr["Comm_Rate1"] = ds.Tables[0].Rows[i]["Comm_Rate1"].ToString();

                    dr["Levi_Amt"] = ds.Tables[0].Rows[i]["Levi_Amt"].ToString();
                    dr["Packing_Rate"] = ds.Tables[0].Rows[i]["Packing_Rate"].ToString();
                    dr["Packing_Amnt"] = ds.Tables[0].Rows[i]["Packing_Amnt"].ToString();
                    dr["Hamali_Rate"] = ds.Tables[0].Rows[i]["Hamali_Rate"].ToString();
                    dr["Hamali_Amnt"] = ds.Tables[0].Rows[i]["Hamali_Amnt"].ToString();
                    dr["Insurance_Amt"] = ds.Tables[0].Rows[i]["Insurance_Amt"].ToString();
                    // dr["Insurance_Amt1"] = ds.Tables[0].Rows[i]["Insurance_Amt1"].ToString();

                    dr["Panjar_Amnt"] = ds.Tables[0].Rows[i]["Panjar_Amnt"].ToString();

                    dr["Panjar_Amnt1"] = ds.Tables[0].Rows[i]["Panjar_Amnt1"].ToString();
                    // dr["Gross_Amnt"] = ds.Tables[0].Rows[i]["Gross_Amnt"].ToString();

                    double salerate = Convert.ToDouble(ds.Tables[0].Rows[i]["Sale_Rate1"].ToString());
                    double commrate = Convert.ToDouble(ds.Tables[0].Rows[i]["Comm_Rate1"].ToString());


                    double com_sr_rate = ((salerate * commrate / 100) + salerate);
                    double com_sr_rate_convert = Math.Round(com_sr_rate, 2);
                    dr["com_sr_rate"] = com_sr_rate_convert;

                    double netwt = Convert.ToDouble(ds.Tables[0].Rows[i]["Net_Wt1"].ToString());
                    double salevalue = (com_sr_rate_convert * netwt) / 100;
                    double salevalue_convert = Math.Round(salevalue, 2);
                    dr["salevalue"] = salevalue_convert;

                    double levi = Convert.ToDouble(ds.Tables[0].Rows[i]["Levi_Amt1"].ToString());
                    double Packing_Amnt = Convert.ToDouble(ds.Tables[0].Rows[i]["Packing_Amnt1"].ToString());
                    double Hamali_Amnt = Convert.ToDouble(ds.Tables[0].Rows[i]["Hamali_Amnt1"].ToString());
                    double Panjar_Amnt = Convert.ToDouble(ds.Tables[0].Rows[i]["Panjar_Amnt1"].ToString());
                    double insurance = Convert.ToDouble(ds.Tables[0].Rows[i]["Insurance_Amt1"].ToString());

                    double gross = salevalue_convert + levi + Packing_Amnt + Hamali_Amnt + insurance;
                    double gross_convert = Math.Round(gross, 2);
                    dr["Gross_Amnt"] = gross_convert;

                    billamntfinal += gross_convert;
                    insuranceamnttotal += insurance;
                    hamaliamnttotal += Hamali_Amnt;
                    packingamnttotal += Packing_Amnt;
                    levitotal += levi;
                    salevaluetotal += salevalue_convert;
                    qtytotal += qty1;
                    totalnetwt += netwt;
                    //double Balance = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString());
                    //dr["Balance"] = Balance;
                    //if (Balance != 0)
                    //{
                    dt.Rows.Add(dr);
                    // }

                }


                //double insuranceamnt = Convert.ToDouble(dt.Compute("SUM(Insurance_Amt1)", string.Empty));
                //lbliamnttotal.Text = insuranceamnt.ToString();

                if (dt.Rows.Count > 0)
                {
                    //lblQntlGrandTotal.Text = clsCommon.getString("select SUM(Buyer_Quantal) from " + tblPrefix + "qryTenderList where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                    //lblBalTotal.Text = Convert.ToString(dt.Compute("SUM(Balance)", string.Empty));
                    //lblDispTotal.Text = Convert.ToString(dt.Compute("SUM(Disp)", string.Empty));
                    //lblqtltotal.Text = Convert.ToString(dt.Compute("SUM(Qntl)", string.Empty));
                    lblsaleamnttotal.Text = billamntfinal.ToString();
                    lbliamnttotal.Text = insuranceamnttotal.ToString();
                    lblhamnttotal.Text = hamaliamnttotal.ToString();
                    lblpamnttotal.Text = packingamnttotal.ToString();
                    lbllevitotal.Text = levitotal.ToString();
                    lblsvaluetotak.Text = salevaluetotal.ToString();
                    lbltotalqty.Text = qtytotal.ToString();
                    lblnetwttotal.Text = totalnetwt.ToString();

                    dtlDetails.DataSource = dt;
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
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();
    }

    protected void lnkTenderNo_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkTenderNo = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkTenderNo.NamingContainer;
            string No = lnkTenderNo.Text;
            Session["TN_NO"] = No;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tnjhj", "javascript:TN();", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
}