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

public partial class Sugar_Report_rptColdStorageBill : System.Web.UI.Page
{
    int billno;
    int company_code;
    int year_code;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string doc_no;
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
    string FssaiNO = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();

            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                doc_no = Request.QueryString["billno"];
               
                int company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                int year_code = Convert.ToInt32(Session["year"].ToString());
                FssaiNO = Session["CompanyFSSAI_No"].ToString();
                //DataTable dt = new DataTable();
                GSTNO = Session["Company_GST"].ToString();
                company_name = Session["Company_Name"].ToString();
                Address = Session["address"].ToString();
                Mobile = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Session["Company_Code"].ToString());
                DataTable dt = GetData();
                SqlDataAdapter da = new SqlDataAdapter();

                string TotalAmount = dt.Rows[0]["Final_Amount"].ToString();
                string inWords = clsNoToWord.ctgword(TotalAmount);

                rpt.Load(Server.MapPath("CryColdStorageBill.rpt"));
                rpt.SetDataSource(dt);

                CryColdStorageBill.ReportSource = rpt;        
                rpt.DataDefinition.FormulaFields["Gstin"].Text = "\"" + GSTNO + "\"";
                rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
                rpt.DataDefinition.FormulaFields["address"].Text = "\"" + Address + "\"";
                rpt.DataDefinition.FormulaFields["Mobileno"].Text = "\"" + Mobile + "\"";
                rpt.DataDefinition.FormulaFields["Inword"].Text = "\"" + inWords + "\"";

                string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
                 + Session["Company_Code"].ToString() + "'");
                String path = Server.MapPath("") + "\\" + imagepath;
                imagepath = path.Replace("Report", "Images");
                rpt.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";

                string logo = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
             + Session["Company_Code"].ToString() + "'");
                String logopath = Server.MapPath("") + "\\" + logo;
                logo = logopath.Replace("Report", "Images");
                rpt.DataDefinition.FormulaFields["logo"].Text = "\"" + logo + "\"";
                rpt.DataDefinition.FormulaFields["fssai"].Text = "\"" + FssaiNO + "\"";
                CryColdStorageBill.RefreshReport();

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
                SqlCommand cmd = new SqlCommand("select * from qryColdStorageHeadDetailBill where Doc_No="+doc_no+" and Company_Code="+ Session["Company_Code"].ToString() +" and Year_Code="+Session["year"].ToString() +" ", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
           
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
        this.CryColdStorageBill.ReportSource = null;

        CryColdStorageBill.Dispose();

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