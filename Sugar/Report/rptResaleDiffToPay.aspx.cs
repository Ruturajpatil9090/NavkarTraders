using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class Report_rptResaleDiffToPay : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                BindList();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    private void BindList()
    {
        try
        {
            //if (string.IsNullOrEmpty(Branch_Code))
            //{
            //    qry = "select CONVERT(varchar(10),t.Tender_Date,103) as tdate,t.Tender_No as tno,ISNULL(a.Short_Name,a.Ac_Name_E) as mill,b.Ac_Name_E as party,t.Broker" +
            // " ,t.Quantal as quantal,t.Mill_Rate as millrate,t.Purc_Rate as purcrate,t.Voucher_No as voucher from " + tblPrefix + "Tender t" +
            // " left outer join " + tblPrefix + "AccountMaster a on t.Mill_Code=a.Ac_Code AND t.Company_Code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on t.Voucher_By=b.Ac_Code AND t.Company_Code=b.Company_Code where t.Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and " +
            // "  t.type='R' and t.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and t.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
            //}
            //else
            //{
            //    qry = "select CONVERT(varchar(10),t.Tender_Date,103) as tdate,t.Tender_No as tno,ISNULL(a.Short_Name,a.Ac_Name_E) as mill,b.Ac_Name_E as party,t.Broker" +
            // " ,t.Quantal as quantal,t.Mill_Rate as millrate,t.Purc_Rate as purcrate,t.Voucher_No as voucher from " + tblPrefix + "Tender t" +
            // " left outer join " + tblPrefix + "AccountMaster a on t.Mill_Code=a.Ac_Code AND t.Company_Code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on t.Voucher_By=b.Ac_Code AND t.Company_Code=b.Company_Code  where t.Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and " +
            // "  t.type='R' and t.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and t.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and t.Branch_Id=" + Branch_Code + "";
            //}
            //qry = "select Tender_DateConverted,Tender_No ,millshortname, tenderdoname,Broker ,Quantal,Mill_Rate,Purc_Rate,Voucher_No from qrytenderhead "
            //    + " where Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and  type='R' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            qry = "select Tender_DateConverted,Tender_No ,millshortname, tenderdoname,Broker ,Quantal,Mill_Rate,Purc_Rate,Voucher_No from qrytenderhead "
               + " where Tender_Date BETWEEN '" + fromDT + "' and '" + toDT + "' and  type='R' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";

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
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["tdate"] = ds.Tables[0].Rows[i]["Tender_DateConverted"].ToString();
                        dr["tno"] = ds.Tables[0].Rows[i]["Tender_No"].ToString();
                        dr["mill"] = ds.Tables[0].Rows[i]["millshortname"].ToString();
                        dr["party"] = ds.Tables[0].Rows[i]["tenderdoname"].ToString();
                        string voucno = ds.Tables[0].Rows[i]["Voucher_No"].ToString();
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
                        double millrate = Convert.ToDouble(ds.Tables[0].Rows[i]["Mill_Rate"].ToString());
                        double purcrate = Convert.ToDouble(ds.Tables[0].Rows[i]["Purc_Rate"].ToString());
                        dr["millrate"] = millrate;
                        dr["purcrate"] = purcrate;
                        double amount = ((millrate - purcrate) * qntl);
                        if (amount < 0)
                        {
                            dr["amount"] = Math.Abs(amount);
                            dt.Rows.Add(dr);
                        }

                    }
                    if (dt.Rows.Count > 0)
                    {
                        //lblToPayTotal.Text = Convert.ToString(dt.Compute("SUM(topay)", string.Empty));
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
        catch
        {
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
            no = clsCommon.getString("select commissionid from commission_bill where Company_Code="+Session["Company_Code"].ToString()+" and Year_Code="+Session["year"].ToString()+"");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:LV('"+no+"');", true);
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
        string strFile = "ResaleDiffernceToPay_Report.xls";
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