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

public partial class Sugar_Report_pgeLetterprint1 : System.Web.UI.Page
{
    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
    string bankdetail = string.Empty;
    string doc_no = string.Empty;
    ReportDocument rpt = new ReportDocument();
    string company_name = string.Empty;
    string mail = string.Empty;
    string Ac_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        doc_no = Request.QueryString["doc_no"];
        company_name = Session["Company_Name"].ToString();
        DataTable dt = GetData();

        Ac_Code = dt.Rows[0]["AC_CODE"].ToString();
        if (!IsPostBack)
        {

            txtEmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + Ac_Code + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        }

        rpt.Load(Server.MapPath("cryLetterprint.rpt"));
        rpt.SetDataSource(dt);

        string docno = doc_no;
        docno = docno.PadLeft(6, '0');
        string docdate = clsCommon.getString("select doc_dateConverted from qrysaleheaddetail where  saleid=" + docno);

        cryletterprint.ReportSource = rpt;

        rpt.DataDefinition.FormulaFields["printhead"].Text = "\"N\"";

        rpt.DataDefinition.FormulaFields["AL1"].Text = "\"" + AL1 + "\"";
        rpt.DataDefinition.FormulaFields["AL2"].Text = "\"" + AL2 + "\"";
        rpt.DataDefinition.FormulaFields["AL3"].Text = "\"" + AL3 + "\"";
        rpt.DataDefinition.FormulaFields["Al4"].Text = "\"" + AL4 + "\"";
        rpt.DataDefinition.FormulaFields["other"].Text = "\"" + other + "\"";


        string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
                + Session["Company_Code"].ToString() + "'");
        String path = Server.MapPath("") + "\\" + imagepath;

        imagepath = path.Replace("Report", "Images");

        string logo = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
         + Session["Company_Code"].ToString() + "'");
        String logopath = Server.MapPath("") + "\\" + logo;

        //string logopath1 = path.Replace("Sugar", "Images");
        logo = logopath.Replace("Report", "Images");

        rpt.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";
        rpt.DataDefinition.FormulaFields["logo"].Text = "\"" + logo + "\"";
        rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";


        // rprt.DataDefinition.FormulaFields["todate"].Text = datefrom;
        //rprt.DataDefinition.FormulaFields["Date"].Text = "\"Reverse Charge Report From " + datefrom + " To " + dateto + "\"";
        cryletterprint.RefreshReport();

    }

    private DataTable GetData()
    {
        try
        {
            DataTable dt = new DataTable();
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("select * from NT_1_Letter where  DOC_NO in(" + doc_no + ")", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            string qry = "select * from tblvoucherheadaddress where Company_Code='" + Session["Company_Code"].ToString() + "'";
            DataSet ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    AL1 = dt1.Rows[0]["AL1"].ToString();
                    AL2 = dt1.Rows[0]["AL2"].ToString();
                    AL3 = dt1.Rows[0]["AL3"].ToString();
                    AL4 = dt1.Rows[0]["AL4"].ToString();
                    other = dt1.Rows[0]["Other"].ToString();
                    bankdetail = dt1.Rows[0]["bankdetail"].ToString();
                }
            }


            return dt;






        }
        catch
        {
            return null;
        }
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
            string pdfname = filepath + "\\Letter" + doc_no + ".pdf";
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
            string pdfname = filepath + "\\Letter" + doc_no + ".pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty)
            {
                mail = txtEmail.Text;
                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "Letterprint:" + doc_no;
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "Salebill";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                //msg.Subject = "DOC.No:";
                msg.Subject = "Letter No:" + doc_no;

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
        this.cryletterprint.ReportSource = null;

        cryletterprint.Dispose();

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