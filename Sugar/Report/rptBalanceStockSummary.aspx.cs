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

public partial class Report_rptBalanceStockSummary : System.Web.UI.Page
{
    string Mill_Code = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string f = "../GSReports/BalanceStockSummary_.htm";
    string f_Main = "../Report/BalanceStockSummary_";
    string Branch_Code = string.Empty;
    string Tender_No = string.Empty;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!Page.IsPostBack)
        {
            FromDt = Request.QueryString["fromDT"];
            ToDt = Request.QueryString["toDT"];
            Branch_Code = Request.QueryString["Branch_Code"];
            Mill_Code = Request.QueryString["Mill_Code"];
            this.BindData();
            lblCompanyName.Text = Session["Company_Name"].ToString();
        }
    }

    private void BindData()
    {
        try
        {
            string Dispatched = "";
            if (Branch_Code == "0")
            {
                if (Mill_Code == "0")
                {

                //    qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                //          " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                //          " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                //          " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                //          " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND qt.Buyer=2 ";
                    qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                             " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                             " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                             " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                             " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND qt.Buyer=2 ";
                
                }
                else
                {
                    //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                    //      " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                    //      " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                    //      " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                    //      " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND qt.Buyer=2 AND qt.Mill_Code=" + Mill_Code + "";

                    qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                         " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                         " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                         " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                         " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND qt.Buyer=2 AND qt.Mill_Code=" + Mill_Code + "";

                }
            }
            else
            {
                if (Mill_Code == "0")
                {
                    //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                    //  " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                    //  " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                    //  " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                    //  " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND qt.Buyer=2  and qt.Branch_Code=" + Branch_Code + "";

                    qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                      " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                      " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                      " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                      " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND qt.Buyer=2  and qt.Branch_Code=" + Branch_Code + "";
               
                }
                else
                {
                  //  qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                  //" ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                  //" CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                  //" left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                  //" left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND qt.Buyer=2  and qt.Branch_Code=" + Branch_Code + " and qt.Mill_Code=" + Mill_Code + "";

                    qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from qrytenderheaddetail qt " +
                " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Tender_Date BETWEEN '" + FromDt + "' AND '" + ToDt + "' and qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND qt.Buyer=2  and qt.Branch_Code=" + Branch_Code + " and qt.Mill_Code=" + Mill_Code + "";
              
                }

            }
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn disp = new DataColumn("Dispatched", typeof(double));
                    ds.Tables[0].Columns.Add(disp);

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string tenderid = ds.Tables[0].Rows[j]["Tender_No"].ToString();
                        Dispatched = clsCommon.getString("Select SUM(quantal) as TD_Dispatch from " + tblPrefix + "deliveryorder WHERE company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no=" + tenderid + " AND tran_type='DO' AND desp_type='DI'");
                        //string tdid = ds.Tables[0].Rows[j]["ID"].ToString();
                        double dispatc = Dispatched != string.Empty ? double.Parse(Dispatched) : 0;
                        ds.Tables[0].Rows[j]["Dispatched"] = dispatc;
                    }
                    dt = ds.Tables[0];
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
        catch (Exception)
        {
            throw;
        }
    }

    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList DatalistTenderDetails = (DataList)e.Item.FindControl("DatalistTenderDetails");
        Label lblTenderNo = (Label)e.Item.FindControl("lblTenderNo");
        Label lblTenderDispatched = (Label)e.Item.FindControl("lblTenderDispatched");
        Label lblTenderNetQntl = (Label)e.Item.FindControl("lblTenderNetQntl");
        Label lblTenderBalLeft = (Label)e.Item.FindControl("lblTenderBalLeft");
        double NetQntl = Convert.ToDouble(lblTenderNetQntl.Text);
        double NetDisp = Convert.ToDouble(lblTenderDispatched.Text);
        Tender_No = lblTenderNo.Text;
        //qry = "select td.ID as TDetailId,a.Ac_Name_E as TDetailMill,(td.Sale_Rate+td.Commission_Rate) as TDeatailSaleRate," +
        //      " td.Buyer_Quantal as TDeatailQntl from qrytenderheaddetail td left outer join " + tblPrefix + "AccountMaster a" +
        //      " on td.Buyer=a.Ac_Code and td.Company_Code=a.Company_Code where td.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and td.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and td.Tender_No=" + Tender_No + "";

        qry = "select td.ID as TDetailId,a.Ac_Name_E as TDetailMill,(td.Sale_Rate+td.Commission_Rate) as TDeatailSaleRate," +
             " td.Buyer_Quantal as TDeatailQntl from qrytenderheaddetail td left outer join " + tblPrefix + "AccountMaster a" +
             " on td.Buyer=a.Ac_Code and td.Company_Code=a.Company_Code where td.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and td.Tender_No=" + Tender_No + "";
       
        ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblTenderBalLeft.Text = Convert.ToString(NetQntl - NetDisp);
                DatalistTenderDetails.DataSource = dt;
                DatalistTenderDetails.DataBind();
            }
        }
    }

    protected void DatalistTenderDetails_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList DtDispDetails = (DataList)e.Item.FindControl("DtDispDetails");
            Label lblTenderDispatchedd = (Label)e.Item.FindControl("lblTenderDispatchedd");
            Label lblTenderBalLeftd = (Label)e.Item.FindControl("lblTenderBalLeftd");
            Label lblTenderQntyd = (Label)e.Item.FindControl("lblTenderQntyd");
            Label lblTenderNod = (Label)e.Item.FindControl("lblTenderNod");
            string Purc_Order = lblTenderNod.Text.ToString();
            string Purc_No = Tender_No;
            qry = "select d.narration4 as DIGetPass,d.doc_no as DINo,d.sale_rate as DISaleRate,d.quantal as DIQntl,CONVERT(varchar(10),d.doc_date,103) as DIDocDate,truck_no as DITruckNo from " + tblPrefix + "deliveryorder d" +
                " left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code and d.company_code=a.Company_Code where d.tran_type='DO' and d.desp_type='DI'" +
                " and d.purc_no=" + Purc_No + " and d.purc_order=" + Purc_Order + " and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.purc_no!=0";
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("DIGetPass", typeof(string)));
                dt.Columns.Add(new DataColumn("DINo", typeof(string)));
                dt.Columns.Add(new DataColumn("DISaleRate", typeof(string)));
                dt.Columns.Add(new DataColumn("DIQntl", typeof(double)));
                dt.Columns.Add(new DataColumn("DIDocDate", typeof(string)));
                dt.Columns.Add(new DataColumn("DITruckNo", typeof(string)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["DIGetPass"] = ds.Tables[0].Rows[i]["DIGetPass"].ToString();
                    dr["DINo"] = ds.Tables[0].Rows[i]["DINo"].ToString();
                    dr["DISaleRate"] = ds.Tables[0].Rows[i]["DISaleRate"].ToString();
                    string diqntl = ds.Tables[0].Rows[i]["DIQntl"].ToString();
                    dr["DIQntl"] = diqntl;
                    dr["DIDocDate"] = ds.Tables[0].Rows[i]["DIDocDate"].ToString();
                    dr["DITruckNo"] = ds.Tables[0].Rows[i]["DITruckNo"].ToString();
                 
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    lblTenderDispatchedd.Text = Convert.ToString(dt.Compute("SUM(DIQntl)", string.Empty));
                    double bal = 0;
                    double tdqnty = Convert.ToDouble(lblTenderQntyd.Text);
                    double diqnty = Convert.ToDouble(lblTenderDispatchedd.Text);
                    bal = tdqnty - diqnty;
                    lblTenderBalLeftd.Text = Convert.ToString(bal);
                    DtDispDetails.DataSource = dt;
                    DtDispDetails.DataBind();
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
        //StringBuilder StrHtmlGenerate = new StringBuilder();
        //StringBuilder StrExport = new StringBuilder();
        //StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        //StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        //StrExport.Append("<DIV  style='font-size:12px;'>");
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter tw = new HtmlTextWriter(sw);
        //pnlMain.RenderControl(tw);
        //string sim = sw.ToString();
        //StrExport.Append(sim);
        //StrExport.Append("</div></body></html>");
        //string strFile = "report.xls";
        //string strcontentType = "application/excel";
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.BufferOutput = true;
        //Response.ContentType = strcontentType;
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        //Response.Write(StrExport.ToString());
        //Response.Flush();
        //Response.Close();
        //Response.End();


        string Name = "Report";

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
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != string.Empty)
        {
            try
            {
                string email = txtEmail.Text;

                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                pnlMain.RenderControl(tw);
                string s = sw.ToString();

                try
                {
                    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(s);
                        }
                    }
                }
                catch (Exception)
                {
                    f = f_Main + ".htm";
                    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(s);
                        }
                    }
                }

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(email);
                msg.Body = "Balance Stock Summary";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Balance Stock Summary " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
                msg.IsBodyHtml = true;
                if (smtpPort != string.Empty)
                {
                    SmtpServer.Port = Convert.ToInt32(smtpPort);
                }
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                SmtpServer.Send(msg);
            }
            catch (Exception e1)
            {
                //Response.Write("mail err:" + e1);
                Response.Write("<script>alert('Error sending Mail');</script>");
                return;
            }
            Response.Write("<script>alert('Mail sent successfully');</script>");
        }
    }
}