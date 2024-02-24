using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;

public partial class Report_rptSaudaBalance : System.Web.UI.Page
{

    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    string Broker_Code = string.Empty;
    DataSet ds;
    DataTable dt;
    string from = "";
    string to = "";
    string tblPrefix = string.Empty;
    string f = "../GSReports/BrokerWiseShortPay_.htm";
    string f_Main = "../Report/BrokerWiseShortPay_";

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Broker_Code = Request.QueryString["accode"].ToString();
        if (!Page.IsPostBack)
        {
            lblCompanyName.Text = Session["Company_Name"].ToString();
            BindList();
        }
    }

    private void BindList()
    {
        try
        {
            from = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            to = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            lblDate.Text = "From: <b>" + from + "</b> To:<b>" + to + "</b>";
            if (Broker_Code != string.Empty)
            {
                qry = "select CONVERT(varchar(10),Tender_Date,103) as dt,Tender_No,tenderdetailid as ID,buyername as Buyer,Buyer_Quantal as Quintal," +
                    " Short_Name as Mill,AMT as Amount,received as Recieved,BALANCE as Balance from qrysaudabalancesummarymain where buyerid=" + Broker_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Sauda_Date between '" + from + "' and '" + to + "' and balance!=0";
                lblBrokerName.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "accountmaster Where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + Broker_Code) + " Detailed Report";
            }
            else
            {
                qry = "select CONVERT(varchar(10),Tender_Date,103) as dt,Tender_No,tenderdetailid as ID,buyername as Buyer,Buyer_Quantal as Quintal," +
                     " Short_Name as Mill,AMT as Amount,received as Recieved,BALANCE as Balance from qrysaudabalancesummarymain where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Sauda_Date between '" + from + "' and '" + to + "' and balance!=0";
                lblBrokerName.Text = "All Sauda Balance Report";
            }
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlDetails.DataSource = ds;
                dtlDetails.DataBind();
            }
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
            DataList detailsDtl = (DataList)e.Item.FindControl("detailsDtl");
            Label lblTenderNo = (Label)e.Item.FindControl("lblTenderNo");
            Label lblTenderId = (Label)e.Item.FindControl("lblTenderId");

            string Tender_No = lblTenderNo.Text;
            string Tender_Id = lblTenderId.Text;
            qry = "SELECT doc_no AS #, Tran_Type AS Ptype, CONVERT(VARCHAR(10), doc_date, 103) AS tran_date, amount AS PayAmt, narration FROM nt_1_transactdetail" +
                 " WHERE Tran_Type IN ('BR', 'CR') and Tender_No=" + Tender_No + " and tenderdetailid=" + Tender_Id + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
            DataSet ds2 = new DataSet();
            ds2 = clsDAL.SimpleQuery(qry);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds2.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //double voucAmount = Convert.ToDouble(lblAmount1.Text);
                    //double totalrecieved = Convert.ToDouble(dt.Compute("SUM(PayAmt)", string.Empty));
                    //lblRecieved1.Text = totalrecieved.ToString();
                    //lblShort1.Text = Convert.ToString(Math.Round((voucAmount - totalrecieved), 2));
                    detailsDtl.DataSource = dt;
                    detailsDtl.DataBind();
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
        try
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
        catch (Exception)
        {
            throw;
        }
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
                SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(email);
                msg.Body = "Broker Wise Short Payment With Details Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Broker Wise Short Payment With Details " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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