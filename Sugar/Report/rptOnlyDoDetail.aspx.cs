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

public partial class Report_rptOnlyDoDetail : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet ds2 = new DataSet();
    DataTable dtlistbind = new DataTable();
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    string f = "../GSReports/DispatchSummary_.htm";
    string f_Main = "../Report/DispatchSummary_";

    protected void Page_Load(object sender, EventArgs e)
    {
        Branch_Code = Request.QueryString["Branch_Code"];
        tblPrefix = Session["tblPrefix"].ToString();

        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        if (!Page.IsPostBack)
        {
            //BindList();
            qry = " select distinct doc_dateConverted as doc_date from qrydohead where desp_type='DO' and doc_date between  '" + fromDT + "' and '" + toDT
                + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            
            ds = clsDAL.SimpleQuery(qry);
            qry = "select doc_no,millshortname,getpassname as GetPassName,voucherbyname as VoucherByname,grade,quantal,mill_rate,sale_rate,Tender_Commission," +
                  " truck_no,transportname,voucher_no,FreightPerQtl,MM_Rate,vasuli_rate1,desp_type,doc_date from qrydohead  WHERE desp_type = 'DO' " +
                  " and doc_date between  '" + fromDT + "' and '" + toDT + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())+" order by  doc_no,millName asc;";

            ds2 = clsDAL.SimpleQuery(qry);

            dtlistbind = ds2.Tables[0];
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
        }
       
    }

    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            if (string.IsNullOrEmpty(Branch_Code))
            {//Distinct(Convert(varchar(10),d.doc_date,103)) as do_date
                //qry = "SELECT Distinct(Convert(varchar(10),doc_date,103)) as doc_date "
                //+ " FROM NT_1_qryDeliveryOrderList"
                //+ " WHERE (desp_type = 'DO') and doc_date between '" + fromDT + "' and '" + toDT + "' and company_code="
                //  + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by doc_date asc";

                qry = "SELECT Distinct doc_dateConverted as doc_date "
                + " FROM qrydohead"
                + " WHERE (desp_type = 'DO') and doc_date between '" + fromDT + "' and '" + toDT + "' and company_code="
                  + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            }
            else
            {
                qry = "SELECT Distinct doc_dateConverted as doc_date "
                + " FROM qrydohead"
                + " WHERE (desp_type = 'DO') and doc_date between '" + fromDT + "' and '" + toDT + "' and company_code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                + " and Branch_Code=" + Branch_Code;


            }

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
        Label lbldodate = (Label)e.Item.FindControl("lbldodate");
        Label lblTotalQntl = (Label)e.Item.FindControl("lblTotalQntl");
        string do_date = lbldodate.Text;
        string date = DateTime.Parse(do_date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
       
        DataView view1=new DataView(dtlistbind,"doc_date='"+lbldodate.Text+"'", "doc_date", DataViewRowState.CurrentRows);
        DataTable dtAcData = view1.ToTable(true, "doc_no", "voucher_no", "desp_type", "millShortName", "GetPassName", "VoucherByname", "grade", "quantal",
            "mill_rate", "sale_rate", "Tender_Commission", "truck_no", "TransportName", "FreightPerQtl", "MM_Rate", "vasuli_rate1", "doc_date");


        if (dtAcData.Rows.Count > 0)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("doc_no", typeof(string)));
            dt.Columns.Add(new DataColumn("voucher_no", typeof(string)));
            dt.Columns.Add(new DataColumn("desp_type", typeof(string)));
            dt.Columns.Add(new DataColumn("millName", typeof(string)));
            dt.Columns.Add(new DataColumn("GetPassName", typeof(string)));
            dt.Columns.Add(new DataColumn("VoucherByname", typeof(string)));
            dt.Columns.Add(new DataColumn("grade", typeof(string)));
            dt.Columns.Add(new DataColumn("quantal", typeof(string)));
            dt.Columns.Add(new DataColumn("mill_rate", typeof(string)));
            dt.Columns.Add(new DataColumn("sale_rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Tender_Commission", typeof(string)));
            dt.Columns.Add(new DataColumn("truck_no", typeof(string)));
            dt.Columns.Add(new DataColumn("TransportName", typeof(string)));
            dt.Columns.Add(new DataColumn("FreightPerQtl", typeof(string)));
            dt.Columns.Add(new DataColumn("MM_Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("vasuli_rate1", typeof(string)));
            for (int i = 0; i < dtAcData.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["doc_no"] = dtAcData.Rows[i]["doc_no"].ToString();
                    dr["voucher_no"] = dtAcData.Rows[i]["voucher_no"].ToString();
                    dr["desp_type"] = dtAcData.Rows[i]["desp_type"].ToString();

                    dr["millName"] = dtAcData.Rows[i]["millShortName"].ToString();
                    dr["GetPassName"] = dtAcData.Rows[i]["GetPassName"].ToString();
                    dr["VoucherByname"] = dtAcData.Rows[i]["VoucherByname"].ToString();
                    dr["grade"] = dtAcData.Rows[i]["grade"].ToString();
                    dr["quantal"] = dtAcData.Rows[i]["quantal"].ToString();
                    dr["mill_rate"] = dtAcData.Rows[i]["mill_rate"].ToString();
                    dr["sale_rate"] = dtAcData.Rows[i]["sale_rate"].ToString();
                    dr["Tender_Commission"] = dtAcData.Rows[i]["Tender_Commission"].ToString();
                    dr["truck_no"] = dtAcData.Rows[i]["truck_no"].ToString();
                    dr["TransportName"] = dtAcData.Rows[i]["TransportName"].ToString();
                    dr["FreightPerQtl"] = dtAcData.Rows[i]["FreightPerQtl"].ToString();
                    dr["MM_Rate"] = dtAcData.Rows[i]["MM_Rate"].ToString();
                    dr["vasuli_rate1"] = dtAcData.Rows[i]["vasuli_rate1"].ToString();
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    //lblTotalQntl.Text = Convert.ToString(dt.Compute("SUM(quantal)", string.Empty));
                    //lblToRecieveTotal.Text = Convert.ToString(dt.Compute("SUM(torecieve)", string.Empty));
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
        
        //}
    }

    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkDO = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkDO.NamingContainer;
            string ps = lnkDO.Text;
            Session["DO_NO"] = ps;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kdjsd", "javascript:DO();", true);
            lnkDO.Focus();
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

    //protected void lnkVO_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnkVO = (LinkButton)sender;
    //        DataListItem item = (DataListItem)lnkVO.NamingContainer;
    //        string ps = lnkVO.Text;
    //        Session["Doc_No"] = ps;
    //        string TranType = "LV";
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:LV();", true);
    //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "lvb", "javascript:LV();", true);
    //        lnkVO.Focus();
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
}