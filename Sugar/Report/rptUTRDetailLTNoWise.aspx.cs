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
//using System.Printing;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Data.SqlClient;


public partial class Report_rptUTRDetailLTNoWise : System.Web.UI.Page
{
    int billno;
    int company_code;
    int year_code;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string ac_code;
    string utr_no;
    string AcType = string.Empty;
    string mail = string.Empty;
    ReportDocument rprt1 = new ReportDocument();
    ReportDocument rprt2 = new ReportDocument();
    ReportDocument rpt = new ReportDocument();
    string company_name = string.Empty;
    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                billno = Convert.ToInt32(Request.QueryString["billno"]);
                FromDt = Request.QueryString["FromDt"];
                ToDt = Request.QueryString["ToDt"];
                ac_code = Request.QueryString["accode"];
                AcType = Request.QueryString["AcType"];
                utr_no = Request.QueryString["utr_no"];


                string datefrom = DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                string dateto = DateTime.Parse(ToDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                int company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                int year_code = Convert.ToInt32(Session["year"].ToString());
                company_name = Session["Company_Name"].ToString();
                //DataTable dt = new DataTable();

                DataTable dt = GetData(datefrom, dateto);
                SqlDataAdapter da = new SqlDataAdapter();

                rpt.Load(Server.MapPath("cryUTRDetailReportOnLTNO.rpt"));
                rpt.SetDataSource(dt);

                cryUTRDetailReportOnLTNO.ReportSource = rpt;
                rpt.DataDefinition.FormulaFields["fromto"].Text = "\"UTR report From " + datefrom + " To " + dateto + "\"";
                rpt.DataDefinition.FormulaFields["summary"].Text = "\"N" + "\"";
                rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";

                cryUTRDetailReportOnLTNO.RefreshReport();


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
    private DataTable GetData(string from, string to)
    {

        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {

            //SqlCommand cmd = new SqlCommand("select * from NT_1_qrySaudaBalance where Tender_Date between '" + datefrom.ToString() +
            //    "' and '" + dateto.ToString() + "'", con);
            if (utr_no != "0")
            {
                SqlCommand cmd = new SqlCommand("select * from qryutrdodetail where LTNo is not null and UtrDate between '" + FromDt +
                "' and '" + ToDt + "' and UTRNo='" + utr_no + "' and Year_Code='"
                + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            else if (ac_code != "0")
            {
                SqlCommand cmd = new SqlCommand("select * from qryutrdodetail where Lott_No is not null and doc_date between '" + FromDt +
                "' and '" + ToDt + "' and mill_code='" + ac_code + "' and Year_Code='"
                + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            else if (ac_code != "0" && utr_no != "0")
            {
                SqlCommand cmd = new SqlCommand("select * from qryutrdodetail where Lott_No is not null and doc_date between '" + FromDt +
                    "' and '" + ToDt + "' and UTRNo='" + utr_no + "' and mill_code='" + ac_code + "' and Year_Code='"
                    + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from qryutrdodetail where Lott_No is not null and doc_date between '" + FromDt +
                    "' and '" + ToDt + "' and Year_Code='"
                    + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }
        return dt;
    }

    protected void Print(object sender, EventArgs e)
    {

        // Refresh Report.
        rpt.Refresh();
        //PrintDialog myPrintDialog = new PrintDialog();
        rpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

        // Set Paper Size.
        rpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
        // CrystalDecisions.Shared.ExportFormatType to change the format i.e. Excel, Word, PDF
        //crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "CustomerDetails");
        //PrintDialog dialog1 = new PrintDialog();


        rpt.PrintOptions.PrinterName = GetDefaultPrinter();
        //CrystalDecisions.Shared.PageMargins objPageMargins;
        //objPageMargins = rprt1.PrintOptions.PageMargins;
        //objPageMargins.bottomMargin = 100;
        //objPageMargins.leftMargin = 100;
        //objPageMargins.rightMargin = 100;
        //objPageMargins.topMargin = 100;
        //rprt1.PrintOptions.ApplyPageMargins(objPageMargins);
        //rprt1.PrintOptions.PrinterName = GetDefaultPrinter(); ;

        rpt.PrintToPrinter(1, false, 0, 0);

        //rprt1.PrintToPrinter(1, true, 0, 0);

    }


    private string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();

        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
            {
                return printer;
            }
        }
        return string.Empty;
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath=@"D:\pdffiles\cryChequePrinting.pdf";
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\UTR.pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            //open PDF File
            //System.Diagnostics.Process.Start(pdfname);
            // string FilePath = Server.MapPath("javascript1-sample.pdf");

            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(pdfname);

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
        //   Response.Write("<script>alert('PDF successfully Generated');</script>");

    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath = @"D:\ashwini\bhavani10012019\accowebBhavaniNew\PAN\cryChequePrinting.pdf";
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\UTR.pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "UTR";
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "UTR";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "DOC.No:";
                //msg.IsBodyHtml = true;
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
                if (File.Exists(pdfname))
                {
                    File.Delete(pdfname);
                }
            }


        }
        catch (Exception e1)
        {
            Response.Write("Mail err:" + e1);
            return;
        }
        Response.Write("<script>alert('Mail Send successfully');</script>");

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        //rprt1.Close();
        //rprt1.Clone();
        //rprt1.Dispose();
        //GC.Collect();
        this.cryUTRDetailReportOnLTNO.ReportSource = null;

        cryUTRDetailReportOnLTNO.Dispose();

        if (rpt != null)
        {

            rpt.Close();

            rpt.Dispose();

            rpt = null;

        }

        GC.Collect();

        GC.WaitForPendingFinalizers();
    }
}