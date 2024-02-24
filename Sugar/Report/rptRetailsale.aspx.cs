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

public partial class Report_rptRetailsale : System.Web.UI.Page
{
    string billno = string.Empty;
    string mail = string.Empty;
    int company_code;
    int year_code;
    string partycode = string.Empty;
    string Doc_Date = string.Empty;
    string company_name = string.Empty;
    string Address = string.Empty;
    string Company_OurGSTNo = string.Empty;
    string mystatename = string.Empty;
    string bankdetail = string.Empty;
    string mystatecode = string.Empty;

    ReportDocument rprt1 = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            billno = Request.QueryString["billno"];
            company_code = Convert.ToInt32(Session["Company_Code"].ToString());
            year_code = Convert.ToInt32(Session["year"].ToString());
            partycode = Request.QueryString["partycode"];
            DataTable dt = GetData(int.Parse(billno), company_code);
            SqlDataAdapter da = new SqlDataAdapter();
          

            string TotalAmount = dt.Rows[0]["NetPayble"].ToString();
            string party = dt.Rows[0]["Party_Code"].ToString();
            string inWords = clsNoToWord.ctgword(TotalAmount);

            if (!IsPostBack)
            {
                txtEmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + party + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
               // txtEmail.Text = EmailId;

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Inword"] = inWords;
                    //rprt1.DataDefinition.FormulaFields["ourgstno"].Text = "\"" + Company_OurGSTNo + "\"";

                }


            }
            string printyear = Session["printinsalebill"].ToString();
            if (printyear == "Y")
            {
                billno = Session["selectedyear"].ToString() + "-" + billno;
            }
            string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
               + Session["Company_Code"].ToString() + "'");
            String path = Server.MapPath("") + "\\" + imagepath;

            imagepath = path.Replace("Report", "Images");


            string imagepath1 = imagepath.Replace("~/Images", "");
            string imgnew = imagepath1.Replace("/", "");



            string logo = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
             + Session["Company_Code"].ToString() + "'");
            String logopath = Server.MapPath("") + "\\" + logo;


            logo = logopath.Replace("Report", "Images");

            string path1 = logo.Replace("~/Images", "");
            string img = path1.Replace("/", "");

            rprt1.Load(Server.MapPath("cryRetailSale.rpt"));
            rprt1.SetDataSource(dt);
            cryRetailsale.ReportSource = rprt1;

            company_name = Session["Company_Name"].ToString();
            Address = Session["Company_Address"].ToString();
            Company_OurGSTNo = Session["Company_OurGSTNo"].ToString();
            mystatename = Session["mystatename"].ToString();
            mystatecode = Session["CompanyGSTStateCode"].ToString();

            cryRetailsale.ReportSource = rprt1; 
            rprt1.DataDefinition.FormulaFields["docno"].Text = "\"" + "RR" + billno + "\"";
            rprt1.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";

            cryRetailsale.ReportSource = rprt1;
            rprt1.DataDefinition.FormulaFields["ourgstno"].Text = "\"" + Company_OurGSTNo + "\"";
            cryRetailsale.ReportSource = rprt1;
            rprt1.DataDefinition.FormulaFields["statename"].Text = "\"" + mystatename + "\"";
            rprt1.DataDefinition.FormulaFields["Mystatecode"].Text = "\"" + mystatecode + "\"";



            rprt1.DataDefinition.FormulaFields["logo"].Text = "\"" + imgnew + "\"";
            rprt1.DataDefinition.FormulaFields["sign"].Text = "\"" + img + "\"";
            #region Address
            string qry = "Select * from tblVoucherHeadAddress where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add(new DataColumn("LeftAddress", typeof(string)));
                dt1.Columns.Add(new DataColumn("MiddlePart", typeof(string)));
                dt1.Columns.Add(new DataColumn("RightAddress", typeof(string)));

                string AL1 = ds.Tables[0].Rows[0]["AL1"].ToString();
                string AL2 = ds.Tables[0].Rows[0]["AL2"].ToString();
                string AL3 = ds.Tables[0].Rows[0]["AL3"].ToString();
                string AL4 = ds.Tables[0].Rows[0]["AL4"].ToString();

                bankdetail = ds.Tables[0].Rows[0]["bankdetail"].ToString();
                //string Terms_condition = ds.Tables[0].Rows[0]["Terms_condition"].ToString();

                string rnl = AL1.Replace("\n", "<br/>");
                var TabSpace = new String(' ', 4);
                string ab = rnl.Replace("\t", TabSpace);
                string la = ab.Replace(" ", "&nbsp;");
                rprt1.DataDefinition.FormulaFields["add1"].Text = "\"" + AL1 + "\"";

                string rnl1 = AL2.Replace("\n", "<br/>");
                var TabSpace1 = new String(' ', 4);
                string ab1 = rnl1.Replace("\t", TabSpace1);
                string la1 = ab1.Replace(" ", "&nbsp;");
                rprt1.DataDefinition.FormulaFields["add2"].Text = "\"" + AL2 + "\"";

                string rnl2 = AL3.Replace("\n", "<br/>");
                var TabSpace2 = new String(' ', 4);
                string ab2 = rnl2.Replace("\t", TabSpace2);
                string la2 = ab2.Replace(" ", "&nbsp;");
                rprt1.DataDefinition.FormulaFields["add3"].Text = "\"" + AL3 + "\"";

                string rnl3 = AL4.Replace("\n", "<br/>");
                var TabSpace3 = new String(' ', 4);
                string ab3 = rnl3.Replace("\t", TabSpace3);
                string la3 = ab3.Replace(" ", "&nbsp;");
                rprt1.DataDefinition.FormulaFields["add4"].Text = "\"" + AL4 + "\"";

                //string rnl4 = Terms_condition.Replace("\n", "<br/>");
                //var TabSpace4 = new String(' ', 4);
                //string ab4 = rnl4.Replace("\t", TabSpace4);
                //string la4 = ab4.Replace(" ", "&nbsp;");
                //rprt1.DataDefinition.FormulaFields["Terms"].Text = "\"" + TabSpace4 + "\"";

                rprt1.DataDefinition.FormulaFields["bankdetail"].Text = "\"" + bankdetail + "\"";


            }
            #endregion


            cryRetailsale.RefreshReport();


        }
        catch (Exception)
        {

            throw;
        }
    }

    private DataTable GetData(int bill_no, int company_code)
    {
        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            SqlCommand cmd = new SqlCommand("select * from qryRetailSale where Doc_No=" + billno +
                " and Company_Code=" + company_code+ " and Year_Code="+year_code, con);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Doc_Date = dt.Rows[0]["Doc_Date"].ToString();
            }
            
        }
        return dt;
    }

    protected void Print(object sender, EventArgs e)
    {
        DataTable dt = GetData(int.Parse(billno), company_code);

        SqlDataAdapter da = new SqlDataAdapter();
        string pname = GetDefaultPrinter();

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
            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\Retailsale" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);

            //open PDF File

            //System.Diagnostics.Process.Start(filename);
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
        //   Response.Write("<script>alert('PDF successfully Generated');</script>");

    }
    protected void btnMail_Click(object sender, EventArgs e)
    {

        try
        {
            // string filepath = @"D:\ashwini\bhavani10012019\accowebBhavaniNew\PAN\cryChequePrinting.pdf";
            //string filepath = @"E:\Lata Software Backup\accowebnavkar\PAN\Saudapending.pdf";
            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\Retailsale_" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
            //rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);


            //string fileName = "Saudapending.pdf";
            //string filepath1 = "~/PAN/" + fileName;

            mail = txtEmail.Text;

            ContentType contentType = new ContentType();
            contentType.MediaType = MediaTypeNames.Application.Pdf;
            contentType.Name = "Retailsale";
            // Attachment attachment = new Attachment(Server.MapPath(filename), contentType);
            Attachment attachment = new Attachment(filename);
            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            //EncryptPass enc = new EncryptPass();
            //emailPassword = enc.Decrypt(emailPassword);
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(mail);
            //msg.Body = "SupplierPurchaseOrder";
            msg.Body = "Retailsale";
            msg.Attachments.Add(attachment);
            msg.IsBodyHtml = true;
            //msg.Subject = "No:" + billno;
            msg.Subject = "Retail sale No." + billno + "Date :" + Doc_Date + " From " + company_name;

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
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            Response.Write("<script>alert('Mail Send successfully');</script>");


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
        this.cryRetailsale.ReportSource = null;

        cryRetailsale.Dispose();

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