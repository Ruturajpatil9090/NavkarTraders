using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class Report_rptVasuliRegisterNonZero : System.Web.UI.Page
{
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet d2;
    DataTable binddata;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDate = Request.QueryString["FromDT"].ToString();
        toDate = Request.QueryString["ToDt"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
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
            qry = "Select distinct transport,transportname as Ac_Name_E,SUM(vasuli_amount1) as vasuli_amount from qrydohead where " +
                "company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " "
            + " and  doc_date between '" + fromDate + "' and '" + toDate + "' and purc_no!=0 group by transport,transportname order by transportname asc ";


            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);

            qry = "Select doc_no,doc_dateConverted as doc_date,millshortname,quantal,voucherbyname,truck_no,vasuli_amount1 as vasuli_amount,transport from qrydohead " +
                     " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and  doc_date between '" + fromDate + "' and '" + toDate + "' and purc_no!=0";
            d2 = clsDAL.SimpleQuery(qry);

            binddata = d2.Tables[0];

            if (ds != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("Transport_Code", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Transport_Name", typeof(string)));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        string transcode = ds.Tables[0].Rows[i]["transport"].ToString();
                        dr["transport_Code"] = transcode;
                        //string transport_Name = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_code=" + transcode + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='T'");
                        dr["Transport_Name"] = ds.Tables[0].Rows[i]["Ac_Name_E"].ToString();
                        double vasuli = Convert.ToDouble(ds.Tables[0].Rows[i]["vasuli_amount"].ToString());
                        if (vasuli != 0)
                        {
                            dt.Rows.Add(dr);
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {

                        lblTotalVasuli.Text = clsCommon.getString("select SUM(vasuli_amount1) as VA from qrydohead where  transport!=0 and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and doc_date between '" + fromDate + "' and '" + toDate + "'");


                        dtl.DataSource = dt;
                        dtl.DataBind();
                    }
                    else
                    {
                        dtl.DataSource = null;
                        dtl.DataBind();
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

    protected void DataList_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        //using (clsDataProvider obj = new clsDataProvider())
        //{
        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        Label lblTransportcode = (Label)e.Item.FindControl("lblTrasportCode");
        string trans_code = lblTransportcode.Text.ToString();

        if (trans_code != string.Empty)
        {
            DataView view1 = new DataView(binddata, "transport='" + lblTransportcode.Text + "'", "transport", DataViewRowState.CurrentRows);

            DataTable dtAcData = view1.ToTable(true, "doc_no", "doc_date", "millshortname", "quantal", "voucherbyname", "truck_no", "vasuli_amount", "transport");

            if (dtAcData.Rows.Count > 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("doc_no", typeof(string)));
                dt.Columns.Add(new DataColumn("doc_date", typeof(string)));
                dt.Columns.Add(new DataColumn("millShortName", typeof(string)));
                dt.Columns.Add(new DataColumn("quantal", typeof(double)));
                dt.Columns.Add(new DataColumn("PartyName", typeof(string)));
                dt.Columns.Add(new DataColumn("truck_no", typeof(string)));
                dt.Columns.Add(new DataColumn("vasuli_amount", typeof(double)));

                if (dtAcData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAcData.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["doc_no"] = dtAcData.Rows[i]["doc_no"].ToString();
                        dr["doc_date"] = dtAcData.Rows[i]["doc_date"].ToString();
                        dr["millShortName"] = dtAcData.Rows[i]["millShortName"].ToString();
                        dr["quantal"] = dtAcData.Rows[i]["quantal"].ToString();
                        dr["PartyName"] = dtAcData.Rows[i]["voucherbyname"].ToString();
                        dr["truck_no"] = dtAcData.Rows[i]["truck_no"].ToString();
                        double vasuliamt = Convert.ToDouble(dtAcData.Rows[i]["vasuli_amount"].ToString());
                        dr["vasuli_amount"] = vasuliamt;
                        if (vasuliamt != 0)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                    Label lblVasuliTotal = (Label)e.Item.FindControl("lblVasuliTotal");

                    if (dt.Rows.Count > 0)
                    {
                        lblVasuliTotal.Text = Convert.ToString(dt.Compute("SUM(vasuli_amount)", string.Empty));
                        dtlDetails.DataSource = dt;
                        dtlDetails.DataBind();
                    }
                    else
                    {
                        dtlDetails.DataSource = null;
                        dtlDetails.DataBind();
                    }
                }
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