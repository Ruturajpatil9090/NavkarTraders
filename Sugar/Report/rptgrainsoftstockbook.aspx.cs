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
using System.Xml.Linq;


public partial class Sugar_Report_rptgrainsoftstockbook : System.Web.UI.Page
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
    ReportDocument rprt1 = new ReportDocument();
    string datefrom = "";
    string dateto = "";
    string pagehead = string.Empty;
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

                //Accode = Request.QueryString["Ac_Code"];
                company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                year_code = Convert.ToInt32(Session["year"].ToString());
                //fromdt = Request.QueryString["fromdt"];
                //todt = Request.QueryString["todt"];
                dateto = DateTime.Parse(DateTime.Now.ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd-MM-yyyy");

                pagehead = "Stock Book Upto Date:"+dateto;

                //datefrom = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                DataTable dt = GetData();
                SqlDataAdapter da = new SqlDataAdapter();

                rprt1.Load(Server.MapPath("crygrainstockBook.rpt"));
                rprt1.SetDataSource(dt);
                cryGrainsoftstockbook.ReportSource = rprt1;

                company_name = Session["Company_Name"].ToString();
                // State = Session["state"].ToString();
                //Address = Session["address"].ToString();
                rprt1.DataDefinition.FormulaFields["company_name"].Text = "\"" + company_name + "\"";
                rprt1.DataDefinition.FormulaFields["pagehead"].Text = "\"" + pagehead + "\"";
              
                
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private DataTable GetData()
    {
        try
        {
            DataTable dt = new DataTable();
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("select * from qrygrainstockbalance where balance!=0", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
          

           
            DataTable dtnew = new DataTable();
            string uptodate = DateTime.Parse(DateTime.Now.ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd1 = new SqlCommand("grainstock", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                string XML = "<ROOT><Head Company_Code='" + Session["Company_Code"] + "' year='" + Session["year"] + "' upto_date='" + uptodate+ "'></Head></ROOT>";
                XDocument XDoc = XDocument.Parse(XML, LoadOptions.None);
                XML = XDoc.ToString(SaveOptions.DisableFormatting);


                cmd1.Parameters.Add("@xmlDocument", SqlDbType.NVarChar, 500).Value = XML;
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                sda.Fill(dtnew);
            }

            if (dtnew.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable results = dtnew.Select("Brand_Code =" + dt.Rows[i]["Brand_Code"]).CopyToDataTable();
                    double rate = Convert.ToDouble(results.Rows[0]["Rate"].ToString());
                    dt.Rows[i]["Rate"] = rate;
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
            string filepath = @"C:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string pdfname = filepath + "\\rptGrainsoftstockbook.pdf";

            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

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
            string filepath = @"C:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string pdfname = filepath + "\\D.O.pdf";

            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);
           
            if (txtEmail.Text != string.Empty && txtEmail.Text != "0")
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "D.O";
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "D.O";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "";
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
                Response.Write("<script>alert('Mail Send successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Enter Email Id');</script>");
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
        //rprt1.Close();
        //rprt1.Clone();
        //rprt1.Dispose();
        //GC.Collect();
        this.cryGrainsoftstockbook.ReportSource = null;

        cryGrainsoftstockbook.Dispose();

        if (rprt1 != null)
        {

            rprt1.Close();

            rprt1.Dispose();

            rprt1 = null;

        }

        GC.Collect();

        GC.WaitForPendingFinalizers();
    }
    
}