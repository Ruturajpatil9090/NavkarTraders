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

public partial class Sugar_Report_rptddreport : System.Web.UI.Page
{
    int billno;
    int company_code;
    int year_code;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string doc_date;
    string AcType = string.Empty;
    string mail = string.Empty;
    ReportDocument rprt1 = new ReportDocument();
    ReportDocument rprt2 = new ReportDocument();
    ReportDocument rpt = new ReportDocument();
    string company_name = string.Empty;
    string GSTNO = string.Empty;
    string Address = string.Empty;
    string Mobile = string.Empty;
    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string frmdt = string.Empty;
    string pagehead = string.Empty;
    string fromdate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            frmdt = Request.QueryString["dt"];

            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                int company_code = Convert.ToInt32(Session["Company_Code"].ToString());
               
               // fromdate  = DateTime.Parse(frmdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                pagehead = "Daily Mill wise despatch report for the date  " + frmdt  ;
                
                company_name = Session["Company_Name"].ToString();
              
                DataTable dt = GetData();
                SqlDataAdapter da = new SqlDataAdapter();


                rpt.Load(Server.MapPath("Cryddreport.rpt"));
                rpt.SetDataSource(dt);

                Cryddreport.ReportSource = rpt;

                rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
                rpt.DataDefinition.FormulaFields["pagehead"].Text = "\"" + pagehead + "\"";
              
               
                Cryddreport.RefreshReport();

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
    private DataTable GetData()
    {

        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            string sql = string.Empty;
//            sql = "SELECT     paymentto.Ac_Name_E AS paymenttoname, salebillto.Ac_Name_E AS partyname, dbo.nt_1_citymaster.city_name_e AS partyplace, nt_1_citymaster_1.city_name_e AS paymenttocity, "+
//                   "   dbo.nt_1_deliveryorder.quantal, dbo.nt_1_deliveryorder.sale_rate AS rate, dbo.nt_1_deliveryorder.PurchaseRate AS dorate, dbo.nt_1_gstratemaster.Rate AS gstrate, "+
//                   "   dbo.nt_1_deliveryorder.sale_rate * dbo.nt_1_gstratemaster.Rate / 100 + dbo.nt_1_deliveryorder.sale_rate AS srgst, "+
//                   "   dbo.nt_1_deliveryorder.PurchaseRate * dbo.nt_1_gstratemaster.Rate / 100 + dbo.nt_1_deliveryorder.PurchaseRate AS prgst, dbo.nt_1_sugarsale.RoundOff AS saleroundoff, "+
//                   "   dbo.nt_1_sugarpurchase.OTHER_AMT AS purcroundoff, dbo.nt_1_sugarpurchase.TCS_Amt AS tcsaspurc, dbo.nt_1_sugarsale.TCS_Amt AS tcsassale, "+
//                   "   dbo.nt_1_sugarpurchase.Bill_Amount AS purcamt, dbo.nt_1_sugarsale.Bill_Amount AS saleamt, 0 AS recdamt, dbo.nt_1_tender.Tender_No "+
//"FROM         dbo.nt_1_citymaster AS nt_1_citymaster_1 RIGHT OUTER JOIN "+
// "                     dbo.nt_1_deliveryorder INNER JOIN "+
// "                     dbo.nt_1_gstratemaster ON dbo.nt_1_deliveryorder.GstRateCode = dbo.nt_1_gstratemaster.Doc_no INNER JOIN "+
// "                     dbo.nt_1_sugarsale ON dbo.nt_1_deliveryorder.saleid = dbo.nt_1_sugarsale.saleidnew INNER JOIN "+
// "                     dbo.nt_1_sugarpurchase ON dbo.nt_1_deliveryorder.voucher_no = dbo.nt_1_sugarpurchase.doc_no AND dbo.nt_1_deliveryorder.Year_Code = dbo.nt_1_sugarpurchase.Year_Code INNER JOIN "+
// "                     dbo.nt_1_accountmaster AS paymentto INNER JOIN "+
// "                     dbo.nt_1_tender ON paymentto.accoid = dbo.nt_1_tender.pt ON dbo.nt_1_deliveryorder.purc_no = dbo.nt_1_tender.Tender_No AND "+
// "                     dbo.nt_1_deliveryorder.company_code = dbo.nt_1_tender.Company_Code INNER JOIN "+
// "                     dbo.nt_1_accountmaster AS salebillto ON dbo.nt_1_deliveryorder.sb = salebillto.accoid LEFT OUTER JOIN "+
// "                     dbo.nt_1_citymaster ON salebillto.cityid = dbo.nt_1_citymaster.cityid ON nt_1_citymaster_1.cityid = paymentto.cityid "+
//" WHERE (dbo.nt_1_deliveryorder.purc_no <> 0) and dbo.nt_1_deliveryorder.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
//" and dbo.nt_1_deliveryorder.doc_date='" + frmdt ;
           // sql = "qrydd1";
            SqlCommand cmd = new SqlCommand("select * from qrydd1 where docdate='"+frmdt+"' ", con);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

        }
        return dt;
    }

    protected void Print(object sender, EventArgs e)
    {


        rpt.Refresh();
        rpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

        // Set Paper Size.
        rpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

        rpt.PrintOptions.PrinterName = GetDefaultPrinter();
        rpt.PrintToPrinter(1, false, 0, 0);

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
        this.Cryddreport.ReportSource = null;

        Cryddreport.Dispose();

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
