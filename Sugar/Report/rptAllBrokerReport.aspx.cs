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

public partial class Report_rptAllBrokerReport : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string AccountMasterTable = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string fromDT = string.Empty;
    string toDt = string.Empty;
    string f = "../GSReports/AllBroker_.htm";
    string f_Main = "../GSReports/AllBroker_";
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        fromDT = Request.QueryString["fromDT"].ToString();
        toDt = Request.QueryString["toDt"].ToString();
        if (!Page.IsPostBack)
        {
            lblCompanyName.Text = Session["Company_Name"].ToString();
            this.BindList();
        }
    }

    private void BindList()
    {
        //qry = "select qr.brok,qr.brok_name,SUM(qr.qntl) as brok_total from " + tblPrefix + "qryAllBrokerDetail qr" +
        //    " where qr.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qr.Year_Code="
        //    + Convert.ToInt32(Session["year"].ToString()) + " and qr.Doc_Date between '" + fromDT + "' and '" + toDt
        //    + "' group by qr.brok,qr.brok_name order by qr.brok_name asc";

        qry = "SELECT dbo.NT_1_AccountMaster.Ac_Code as brok, dbo.NT_1_AccountMaster.Ac_Name_E as brok_name, sum(d.quantal) as brok_total " +
              "  FROM   dbo.NT_1_AccountMaster AS B RIGHT OUTER JOIN " +
              "       dbo.NT_1_deliveryorder AS d LEFT OUTER JOIN " +
              "       dbo.NT_1_AccountMaster ON d.broker = dbo.NT_1_AccountMaster.Ac_Code AND d.company_code = dbo.NT_1_AccountMaster.Company_Code LEFT OUTER JOIN " +
              "       dbo.NT_1_AccountMaster AS A ON d.GETPASSCODE = A.Ac_Code AND d.company_code = A.Company_Code ON B.Ac_Code = d.mill_code AND B.Company_Code = d.company_code " +
              " where d.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code="
           + Convert.ToInt32(Session["year"].ToString()) + " and d.Doc_Date between '" + fromDT + "' and '" + toDt +
            "'  and  dbo.NT_1_AccountMaster.Ac_Code > 100 group by dbo.NT_1_AccountMaster.Ac_Code, dbo.NT_1_AccountMaster.Ac_Name_E ORDER BY  NT_1_AccountMaster.Ac_Name_E";


        DataSet ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("brok", typeof(string)));
            dt.Columns.Add(new DataColumn("brok_name", typeof(string)));
            dt.Columns.Add(new DataColumn("brok_total", typeof(double)));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["brok"] = ds.Tables[0].Rows[i]["brok"].ToString();
                    dr["brok_name"] = ds.Tables[0].Rows[i]["brok_name"].ToString();
                    dr["brok_total"] = ds.Tables[0].Rows[i]["brok_total"].ToString();
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    lblDate.Text = "Broker List From " + DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy") + " To " + DateTime.Parse(toDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    lblBrokerQntlTotal.Text = Convert.ToString(dt.Compute("SUM(brok_total)", string.Empty));
                    dtl.DataSource = dt;
                    dtl.DataBind();
                }
            }
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
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        if (txtEmail.Text != string.Empty)
        {
            try
            {
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
                msg.Body = "All Broker Details";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "All Broker Details " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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
                Response.Write("<script>alert('Error sending Mail');</script>");
                return;
            }
            Response.Write("<script>alert('Mail sent successfully');</script>");
        }
    }
}