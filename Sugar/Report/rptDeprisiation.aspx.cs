using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

public partial class Report_rptDeprisiation : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    string qrycommon = string.Empty;
    DataSet ds = null;
    DataTable dt = null;
    int Company_Code = 0;
    int year_code = 0;
    double outerOpBal = 0.00;
    double outerAdditionalamnt = 0.00;
    double outerDelection = 0.00;
    double outerClosingBalance = 0.00;
    double outerDepriciation = 0.00;
    double outerbalance = 0.00;
    string from = string.Empty;
    string to = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        fromDT = Request.QueryString["FromDt"].ToString();
        toDT = Request.QueryString["ToDt"].ToString();
        qrycommon = "NT_1_Dipriciation";
        Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
        year_code = Convert.ToInt32(Session["year"].ToString());
        from = Convert.ToDateTime(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        to = Convert.ToDateTime(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
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
            //using (clsDataProvider obj = new clsDataProvider())
            //{


            qry = "Select distinct([Ac_Code]),[Ac_Name_E] from " + qrycommon + " where Ac_type='F' and [Company_Code]=" + Company_Code + "";
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            //  ds = obj.GetDataSet(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dtl.DataSource = dt;
                    dtl.DataBind();

                    lblouterOpBal.Text = Convert.ToDecimal(Convert.ToString(outerOpBal)).ToString("0.00");
                    lblouterAdditionalamnt.Text = Convert.ToDecimal(Convert.ToString(outerAdditionalamnt)).ToString("0.00");
                    lblouterDelection.Text = Convert.ToDecimal(Convert.ToString(outerDelection)).ToString("0.00");
                    lblouterClosingBalance.Text = Convert.ToDecimal(Convert.ToString(outerClosingBalance)).ToString("0.00");
                    lblouterDepriciation.Text = Convert.ToDecimal(Convert.ToString(outerDepriciation)).ToString("0.00");
                    lblouterbalance.Text = Convert.ToDecimal(Convert.ToString(outerbalance)).ToString("0.00");
                }
            }
            // }
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
            //using (clsDataProvider obj = new clsDataProvider())
            //{


            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblaccode = (Label)e.Item.FindControl("lblaccode");
            string accode = lblaccode.Text;
            string to1 = Convert.ToDateTime(to, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy") + "/03/30";

            Label lnlinneropbal = (Label)e.Item.FindControl("lnlinneropbal");
            Label lblinnerAdditionalamnt = (Label)e.Item.FindControl("lblinnerAdditionalamnt");
            Label lblinnerDelection = (Label)e.Item.FindControl("lblinnerDelection");
            Label lblinnerClosingBalance = (Label)e.Item.FindControl("lblinnerClosingBalance");
            Label lblinnerDepriciation = (Label)e.Item.FindControl("lblinnerDepriciation");
            Label lblinnerbalance = (Label)e.Item.FindControl("lblinnerbalance");

            //Label lblaccode = (Label)e.Item.FindControl("lblaccode");
            double InnerDepriciation = 0.00;
            double Innerbalance = 0.00;
            double InnerClosingbalance = 0.00;
            string halftdate = Convert.ToDateTime(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy") + "/09/30";
            string Lastdate = Convert.ToDateTime(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy") + "/03/30";

            #region[comment]
            //qry = "select [AC_CODE],[Ac_Name_E],ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal," +
            //    " ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal1," +
            //    " ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon + " where year_code=" + year_code + " and [AC_CODE]='" + accode
            //    + "' group by [AC_CODE]),0) as Additionalamnt,ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon
            //    + " where year_code=" + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Additionalamnt1,"

            //    + "ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where year_code=" + year_code + " and [AC_CODE]='"
            //    + accode + "' group by [AC_CODE]),0) as Delection, ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where year_code="
            //    + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Delection1,"
            //    + " ISNULL((select SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) from " + qrycommon + " where [AC_CODE]='" + accode
            //    + "' group by [AC_CODE]),0) as ClosingBalance,ISNULL((select SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) from " + qrycommon
            //    + " where [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as ClosingBalance1,ISNULL(Ac_rate,0) as Ac_rate, ISNULL(Ac_rate,0) as Ac_rate1 from "
            //    + qrycommon + " where Ac_code='"
            //    + accode + "' and DOC_DATE <'" + from
            //    + "' group by [AC_CODE],[Ac_Name_E],[Ac_rate]";


            //qry = "select [AC_CODE],[Ac_Name_E],ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal," +
            //    " ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal1," +
            //    " ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon + " where COMPANY_CODE=" + Company_Code + " and year_code=" + year_code + " and [AC_CODE]='" + accode
            //    + "' group by [AC_CODE]),0) as Additionalamnt,ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon
            //    + " where COMPANY_CODE=" + Company_Code + " and year_code=" + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Additionalamnt1,"

            //    + "ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where COMPANY_CODE=" + Company_Code + " and year_code=" + year_code + " and [AC_CODE]='"
            //    + accode + "' group by [AC_CODE]),0) as Delection, ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where COMPANY_CODE=" + Company_Code + " and year_code="
            //    + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Delection1,"
            //    + " ISNULL((select SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) from " + qrycommon + " where COMPANY_CODE=" + Company_Code + " and [AC_CODE]='" + accode
            //    + "' group by [AC_CODE]),0) as ClosingBalance,ISNULL((select SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) from " + qrycommon
            //    + " where COMPANY_CODE=" + Company_Code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as ClosingBalance1,ISNULL(Ac_rate,0) as Ac_rate, ISNULL(Ac_rate,0) as Ac_rate1 from "
            //    + qrycommon + " where  YEAR_CODE<= " + year_code + " and COMPANY_CODE=" + Company_Code + " and Ac_code='"
            //    + accode + "' and DOC_DATE <'" + from
            //    + "' group by [AC_CODE],[Ac_Name_E],[Ac_rate]";
            #endregion

            //qry = "select [AC_CODE],[Ac_Name_E],ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal," +
            //  " ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal1," +
            //  " ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon + " where DOC_DATE >'" + from + "' and  COMPANY_CODE=" + Company_Code + " and year_code="
            //  + year_code + " and [AC_CODE]='" + accode
            //  + "' group by [AC_CODE]),0) as Additionalamnt,ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon
            //  + " where DOC_DATE >'" + from + "' and COMPANY_CODE=" + Company_Code + " and year_code=" + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Additionalamnt1,"

            //  + "ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where DOC_DATE >'" + from + "' and COMPANY_CODE=" + Company_Code + " and year_code=" + year_code + " and [AC_CODE]='"
            //  + accode + "' group by [AC_CODE]),0) as Delection, ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where DOC_DATE >'" + from + "' and COMPANY_CODE=" + Company_Code + " and year_code="
            //  + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Delection1,ISNULL(Ac_rate,0) as Ac_rate, ISNULL(Ac_rate,0) as Ac_rate1 from "
            //  + qrycommon + " where  YEAR_CODE<= " + year_code + " and COMPANY_CODE=" + Company_Code + " and Ac_code='"
            //  + accode + "' and DOC_DATE <'" + from + "' group by [AC_CODE],[Ac_Name_E],[Ac_rate]";

            qry = "select [AC_CODE],[Ac_Name_E],ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal," +
             " ISNULL(SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end),0) as  OpBal1," +
             " ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon + " where DOC_DATE >'" + from + "' and  COMPANY_CODE=" + Company_Code + " and year_code="
             + year_code + " and [AC_CODE]='" + accode
             + "' group by [AC_CODE]),0) as Additionalamnt,ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon
             + " where DOC_DATE >'" + from + "' and COMPANY_CODE=" + Company_Code + " and year_code=" + year_code + " and [AC_CODE]='" + accode
             + "' group by [AC_CODE]),0) as Additionalamnt1,"

             + "ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where DOC_DATE <'" + to1 + "' and DOC_DATE >'" + from + "' and COMPANY_CODE=" + Company_Code
             + " and year_code=" + year_code + " and [AC_CODE]='"
             + accode + "' group by [AC_CODE]),0) as Delection,        ISNULL((select SUM(case drcr when 'C' then AMOUNT end) from " + qrycommon + " where DOC_DATE <'" + to1 + "' and DOC_DATE >'" + from
             + "' and COMPANY_CODE=" + Company_Code + " and year_code="
             + year_code + " and [AC_CODE]='" + accode + "' group by [AC_CODE]),0) as Delection1,       ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from "
             + qrycommon + " where DOC_DATE >='"
             + from + "' and DOC_DATE <='" + halftdate + "' and  COMPANY_CODE=" + Company_Code + " and year_code="
             + year_code + " and [AC_CODE]='" + accode
             + "' group by [AC_CODE]),0) as fulladdition,ISNULL((select SUM(case drcr when 'D' then AMOUNT end) from " + qrycommon + " where DOC_DATE >='"
             + halftdate + "' and DOC_DATE <='" + Lastdate + "' and  COMPANY_CODE=" + Company_Code + " and year_code="
             + year_code + " and [AC_CODE]='" + accode
             + "' group by [AC_CODE]),0) as halfaddition," +
             "ISNULL(Ac_rate,0) as Ac_rate, ISNULL(Ac_rate,0) as Ac_rate1 from "
             + qrycommon + " where  YEAR_CODE<= " + year_code + " and COMPANY_CODE=" + Company_Code + " and Ac_code='"
             + accode + "' and DOC_DATE < '" + from + "' group by [AC_CODE],[Ac_Name_E],[Ac_rate]";



            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            //  ds = obj.GetDataSet(qry);
            if (ds != null)
            {
                dt.Columns.Add(new DataColumn("AC_CODE", typeof(string)));
                dt.Columns.Add(new DataColumn("Ac_Name_E", typeof(string)));

                dt.Columns.Add(new DataColumn("OpBal", typeof(string)));
                dt.Columns.Add(new DataColumn("OpBal1", typeof(double)));

                dt.Columns.Add(new DataColumn("Additionalamnt", typeof(string)));
                dt.Columns.Add(new DataColumn("Additionalamnt1", typeof(double)));

                dt.Columns.Add(new DataColumn("Delection", typeof(string)));
                dt.Columns.Add(new DataColumn("Delection1", typeof(double)));

                dt.Columns.Add(new DataColumn("ClosingBalance", typeof(double)));
                //dt.Columns.Add(new DataColumn("ClosingBalance1", typeof(double)));

                dt.Columns.Add(new DataColumn("Ac_rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Ac_rate1", typeof(double)));

                dt.Columns.Add(new DataColumn("Depriciation", typeof(double)));
                dt.Columns.Add(new DataColumn("balance", typeof(double)));

                dt.Columns.Add(new DataColumn("fulladdition", typeof(double)));
                dt.Columns.Add(new DataColumn("halfaddition", typeof(double)));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AC_CODE"] = ds.Tables[0].Rows[i]["AC_CODE"].ToString();
                        dr["Ac_Name_E"] = ds.Tables[0].Rows[i]["Ac_Name_E"].ToString();
                        dr["OpBal"] = ds.Tables[0].Rows[i]["OpBal"].ToString();
                        dr["Additionalamnt"] = ds.Tables[0].Rows[i]["Additionalamnt"].ToString();
                        dr["Delection"] = ds.Tables[0].Rows[i]["Delection"].ToString();
                        //dr["ClosingBalance"] = ds.Tables[0].Rows[i]["ClosingBalance"].ToString();
                        dr["Ac_rate"] = ds.Tables[0].Rows[i]["Ac_rate"].ToString();

                        dr["OpBal1"] = ds.Tables[0].Rows[i]["OpBal1"].ToString();

                        dr["Additionalamnt1"] = ds.Tables[0].Rows[i]["Additionalamnt1"].ToString();
                        dr["Delection1"] = ds.Tables[0].Rows[i]["Delection1"].ToString();
                        //dr["ClosingBalance1"] = ds.Tables[0].Rows[i]["ClosingBalance1"].ToString();
                        dr["Ac_rate1"] = ds.Tables[0].Rows[i]["Ac_rate1"].ToString();

                        double Additionalamnt1 = Convert.ToDouble(ds.Tables[0].Rows[i]["Additionalamnt1"].ToString());
                        double Delection1 = Convert.ToDouble(ds.Tables[0].Rows[i]["Delection1"].ToString());
                        // double ClosingBalance1 = Convert.ToDouble(ds.Tables[0].Rows[i]["ClosingBalance1"].ToString());
                        double Ac_rate1 = Convert.ToDouble(ds.Tables[0].Rows[i]["Ac_rate1"].ToString());
                        double OpBal1 = Convert.ToDouble(ds.Tables[0].Rows[i]["OpBal1"].ToString());

                        double ClosingBalance1 = OpBal1 + (Additionalamnt1 - Delection1);

                        double fulladdition = Convert.ToDouble(ds.Tables[0].Rows[i]["fulladdition"].ToString());
                        double halfaddition = Convert.ToDouble(ds.Tables[0].Rows[i]["halfaddition"].ToString());

                        double Depriciationopbal = Math.Round((OpBal1 * Ac_rate1) / 100, 0);
                        double Despriciation_Full = Math.Round(((fulladdition * Ac_rate1) / 100), 0);
                        double Despriciation_half = Math.Round(((halfaddition * Ac_rate1) / 100) / 2, 0);

                        double Depriciation = Depriciationopbal + Despriciation_Full + Despriciation_half;


                        double balance = ClosingBalance1 - Depriciation;
                        dr["Depriciation"] = Depriciation.ToString();
                        dr["balance"] = balance.ToString();
                        dr["ClosingBalance"] = ClosingBalance1.ToString();
                        InnerDepriciation += Depriciation;
                        Innerbalance += balance;
                        InnerClosingbalance += ClosingBalance1;

                        outerOpBal += OpBal1;
                        outerAdditionalamnt += Additionalamnt1;
                        outerDelection += Delection1;
                        outerClosingBalance += ClosingBalance1;
                        outerDepriciation += Depriciation;
                        outerbalance += balance;

                        dt.Rows.Add(dr);
                    }
                    double InnerOpBal = Convert.ToDouble(dt.Compute("SUM(OpBal1)", string.Empty));
                    double InnerAdditionalamnt = Convert.ToDouble(dt.Compute("SUM(Additionalamnt1)", string.Empty));
                    double InnerDelection = Convert.ToDouble(dt.Compute("SUM(Delection1)", string.Empty));
                    // double InnerClosingBalance = Convert.ToDouble(dt.Compute("SUM(ClosingBalance1)", string.Empty));
                    lnlinneropbal.Text = Convert.ToString(InnerOpBal);
                    lblinnerAdditionalamnt.Text = Convert.ToString(InnerAdditionalamnt);
                    lblinnerDelection.Text = Convert.ToString(InnerDelection);
                    lblinnerClosingBalance.Text = Convert.ToString(Innerbalance);
                    lblinnerDepriciation.Text = Convert.ToString(InnerDepriciation);
                    lblinnerbalance.Text = Convert.ToString(Innerbalance);
                    if (dt.Rows.Count > 0)
                    {

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
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlDetails_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{


            DataList dtlDetails1 = (DataList)e.Item.FindControl("dtlDetails1");
            Label lbldtlNo = (Label)e.Item.FindControl("lbldtlNo");
            string accode = lbldtlNo.Text;
            // from1 = Convert.ToDateTime(from, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy");
            string to1 = Convert.ToDateTime(to, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy") + "/03/30";

            string halftdate = Convert.ToDateTime(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy") + "/09/30";
            string Lastdate = Convert.ToDateTime(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy") + "/03/30";
            qry = "Select [DRCR],[AMOUNT],convert(varchar(10),[DOC_DATE],103) as Date,AMOUNT as DebitAmount,AMOUNT as CreditAmount from " + qrycommon + " where ac_code='"
                + accode + "' and DOC_DATE >= '" + from
                + "' and DOC_DATE <= '" + to1 + "' order by DOC_DATE";


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            //  ds = obj.GetDataSet(qry);
            if (ds != null)
            {
                dt.Columns.Add(new DataColumn("DebitAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("CreditAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("Date", typeof(string)));



                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        string drcr = ds.Tables[0].Rows[i]["DRCR"].ToString();
                        if (drcr == "D")
                        {
                            dr["DebitAmount"] = ds.Tables[0].Rows[i]["AMOUNT"].ToString();
                        }
                        else
                        {
                            dr["CreditAmount"] = ds.Tables[0].Rows[i]["AMOUNT"].ToString();
                        }

                        dr["Date"] = ds.Tables[0].Rows[i]["Date"].ToString();




                        dt.Rows.Add(dr);
                    }


                    if (dt.Rows.Count > 0)
                    {

                        dtlDetails1.DataSource = dt;
                        dtlDetails1.DataBind();
                    }
                    else
                    {
                        dtlDetails1.DataSource = null;
                        dtlDetails1.DataBind();
                    }
                }
                else
                {
                    dtlDetails1.DataSource = null;
                    dtlDetails1.DataBind();
                }
            }
            //}
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string Name = "Depriciation";

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
        Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();

      
    }
    public override void VerifyRenderingInServerForm(Control control) { }
}