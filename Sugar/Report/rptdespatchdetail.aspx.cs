using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;
using System.Configuration;
using System.Drawing.Printing;

using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Data.SqlClient;

public partial class Sugar_Report_rptdespatchdetail : System.Web.UI.Page
{
    string fromdt = string.Empty;
    string todt = string.Empty;
    string mail = string.Empty;
    int company_code;
    int year_code;
    string unitno = string.Empty;
    string Accode = string.Empty;
    string company_name = string.Empty;
    string Email = string.Empty;
    string State = string.Empty;
    string Address = string.Empty;
    ReportDocument rptDespatchdetail = new ReportDocument();
    string datefrom = "";
    string dateto = "";
    string pagehead = string.Empty;
    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string qry = string.Empty;
    string TinNo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                Accode = Request.QueryString["accode"];
                company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                year_code = Convert.ToInt32(Session["year"].ToString());
                fromdt = Request.QueryString["fromdt"];
                todt = Request.QueryString["todt"];
               
                pagehead = "despatchdetail from " + fromdt + " To " + todt;
               
                datefrom = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                dateto = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                DataTable dt = GetData(fromdt, todt, Accode);
                SqlDataAdapter da = new SqlDataAdapter();
                company_name = Session["Company_Name"].ToString();
                rptDespatchdetail.Load(Server.MapPath("../Report/CryDespatchdetail.rpt"));
                rptDespatchdetail.DataDefinition.FormulaFields["company_Name"].Text = "\"" + company_name + "\"";
                rptDespatchdetail.DataDefinition.FormulaFields["pagehead"].Text = "\"" + pagehead + "\"";

               
                rptDespatchdetail.SetDataSource(dt);
                CryDespatchdetail.ReportSource = rptDespatchdetail;

              
                              
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }

        catch (Exception)
        {
            throw;
        }
    }
    private DataTable GetData(string fromdt, string todt, string accode)
    {

        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            SqlCommand cmd;
            if (Accode == "")
            {
                cmd = new SqlCommand("  select * from qryDespatchdetail where  Sauda_Date between '" + datefrom + "' and '" + dateto + "' and  Company_Code=" + company_code + " and  Year_Code=" + year_code, con); 
            }

            else
            {
                cmd = new SqlCommand(" select * from qryDespatchdetail where Sauda_Date between '" + datefrom + "' and '" + dateto + "' and Company_Code=" + company_code +
                    " and  Year_Code=" + year_code + " and buyer= " + accode, con);
            }

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
        }
        return dt;
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {

            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\ledger" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rptDespatchdetail.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);


            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(filename);

            if (FileBuffer != null)
            {

                Response.ContentType = "application/pdf";

                Response.AddHeader("content-length", FileBuffer.Length.ToString());

                Response.BinaryWrite(FileBuffer);

            }
        }
        catch (Exception e1)
        {
            Response.Write("PDF err:" + e1);
            return;
        }
        ;

    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {

            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\ledger" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rptDespatchdetail.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);


            if (txtEmail.Text != string.Empty)
            {

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "ledger";

                Attachment attachment = new Attachment(filename);
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "ledger";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "No:";

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
                attachment.Dispose();
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                Response.Write("<script>alert('Mail Send successfully');</script>");
            }


        }
        catch (Exception e1)
        {
            Response.Write("Mail err:" + e1);
            return;
        }


    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        this.CryDespatchdetail.ReportSource = null;

        CryDespatchdetail.Dispose();

        if (rptDespatchdetail != null)
        {

            rptDespatchdetail.Close();

            rptDespatchdetail.Dispose();

            rptDespatchdetail = null;

        }

        GC.Collect();

        GC.WaitForPendingFinalizers();
    }
}