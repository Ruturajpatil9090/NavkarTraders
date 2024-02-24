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

public partial class Sugar_Report_rptReturnSaleBillForGST : System.Web.UI.Page
{

    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
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
    string FssaiNO = string.Empty;
    string TinNo = string.Empty;
    string GSTNO = string.Empty;
    string panno = string.Empty;
    string eway = string.Empty;
    string Inv = string.Empty;
    string company_name = string.Empty;
    string imagepath = string.Empty;
    string datefrom;
    string dateto;
    string datefrom1;
    string dateto1;
    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string lorry = string.Empty;
    string millshort = string.Empty;
    string party = string.Empty;
    string qty = string.Empty;

    string salebillparty = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string companystate = Session["CompanyState"].ToString();
            string gststatecode = Session["CompanyGSTStateCode"].ToString();
            user = Session["user"].ToString();
            eway = Request.QueryString["eway"];
            Inv = Request.QueryString["Inv"];
            FssaiNO = Session["CompanyFSSAI_No"].ToString();
            TinNo = Session["Company_TIN"].ToString();
            GSTNO = Session["Company_GST"].ToString();
            panno = Session["CompanyPan_No"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {


                //billno = Convert.ToInt32(Request.QueryString["billno"]);
                FromDt = Request.QueryString["FromDt"];
                ToDt = Request.QueryString["ToDt"];

                ac_code = Request.QueryString["billno"];
                string CompanyMail = Session["EmailId"].ToString();

                int company_code = Convert.ToInt32(Session["Company_Code"].ToString());
                int year_code = Convert.ToInt32(Session["year"].ToString());

                //DataTable dt = new DataTable();
                company_name = Session["Company_Name"].ToString();
                DataTable dt = GetData(datefrom, dateto);
                salebillparty = dt.Rows[0]["FromAc"].ToString();
                if (!IsPostBack)
                {

                    txtEmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + salebillparty + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                }

                string printyear = Session["printinsalebill"].ToString();
                if (printyear == "Y")
                {
                    ac_code = Session["selectedyear"].ToString() + "-" + ac_code;
                }
                string TotalAmount = dt.Rows[0]["item_Amount"].ToString();
                string inWords = clsNoToWord.ctgword(TotalAmount);
                SqlDataAdapter da = new SqlDataAdapter();

                rpt.Load(Server.MapPath("CryReturnSaleBillForGST.rpt"));
                rpt.SetDataSource(dt);

                CryReturnSaleBillForGST.ReportSource = rpt;

                rpt.DataDefinition.FormulaFields["fromto"].Text = "\"Broker List From " + datefrom1 + " To " + dateto1 + "\"";
                rpt.DataDefinition.FormulaFields["word"].Text = "\"" + inWords + "\"";
                rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
                rpt.DataDefinition.FormulaFields["AL1"].Text = "\"" + AL1 + "\"";
                rpt.DataDefinition.FormulaFields["AL2"].Text = "\"" + AL2 + "\"";
                rpt.DataDefinition.FormulaFields["AL3"].Text = "\"" + AL3 + "\"";
                rpt.DataDefinition.FormulaFields["Al4"].Text = "\"" + AL4 + "\"";
                rpt.DataDefinition.FormulaFields["Newdocno"].Text = "\"" + "RS" + ac_code + "\"";

                rpt.DataDefinition.FormulaFields["companystate"].Text = "\"" + companystate + "\"";
                rpt.DataDefinition.FormulaFields["gststatecode"].Text = "\"" + gststatecode + "\"";
                rpt.DataDefinition.FormulaFields["fssai"].Text = "\"" + FssaiNO + "\"";
                rpt.DataDefinition.FormulaFields["tin"].Text = "\"" + TinNo + "\"";
                rpt.DataDefinition.FormulaFields["gstno"].Text = "\"" + GSTNO + "\"";
                rpt.DataDefinition.FormulaFields["panno"].Text = "\"" + panno + "\"";
                //     rpt.DataDefinition.FormulaFields["Cmail"].Text = "\"" + CompanyMail + "\"";
                rpt.DataDefinition.FormulaFields["other"].Text = "\"" + other + "\"";

                imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
            + Session["Company_Code"].ToString() + "'");
                String path = Server.MapPath("") + "\\" + imagepath;

                // string imagepath1 = path.Replace("Sugar", "Images");
                imagepath = path.Replace("Report", "Images");

                string sign = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
                  + Session["Company_Code"].ToString() + "'");
                String logopath = Server.MapPath("") + "\\" + sign;

                // string logopath1 = path.Replace("Sugar", "Images");
                sign = logopath.Replace("Report", "Images");

                rpt.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";
                rpt.DataDefinition.FormulaFields["sign"].Text = "\"" + sign + "\"";

                CryReturnSaleBillForGST.RefreshReport();


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


            SqlCommand cmd = new SqlCommand("select * from qrysugarsalereturnheaddetail where doc_no='" + ac_code +
                "' and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString(), con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

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
                }
            }
        }
        millshort = dt.Rows[0]["millshortname"].ToString();
        lorry = dt.Rows[0]["LORRYNO"].ToString();
        party = dt.Rows[0]["FromAcName"].ToString();
        qty = dt.Rows[0]["Quantal"].ToString();
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
            string filepath = @"C:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\pdffiles");
            }
            string pdfname = filepath + "\\ReturnSale.pdf";

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
            string filepath = @"C:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\pdffiles");
            }
            string pdfname = filepath + "\\Returnsale.pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                if (lorry != string.Empty)
                {
                    lorry = "Lorry:" + lorry;
                }
                if (millshort != string.Empty)
                {
                    millshort = "Mill:" + millshort;
                }
                if (party != string.Empty)
                {
                    party = "Getpass:" + party;
                }
                if (qty != string.Empty)
                {
                    qty = "Qty:" + qty;
                }

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "SaleBill";
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "Return Sale";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "Bill No:RS" + ac_code + " " + lorry + " " + millshort + " " + party + " " + qty;
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
        this.CryReturnSaleBillForGST.ReportSource = null;

        CryReturnSaleBillForGST.Dispose();

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