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
public partial class Sugar_Report_rptpurchasebill : System.Web.UI.Page
{
    string billno = string.Empty;
    string type = string.Empty;
    string mail = string.Empty;
    string blank = string.Empty;
    string partycode = string.Empty;
    int branchcode = 0;
    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
    string company_name = string.Empty;
    //   string GSTNO = string.Empty;
    string Address1 = string.Empty;
    string Address2 = string.Empty;
    string Doc_Date = string.Empty;
    string FssaiNO = string.Empty;
    string TinNo = string.Empty;
    string GSTNO = string.Empty;
    string panno = string.Empty;
    int company_code;
    string billfoter = string.Empty;
    int year_code;
    ReportDocument rprt1 = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string companystate = Session["CompanyState"].ToString();
            string gststatecode = Session["CompanyGSTStateCode"].ToString();
            billno = Request.QueryString["billno"];
            type = Request.QueryString["type"];
            company_code = Convert.ToInt32(Session["Company_Code"].ToString());
            year_code = Convert.ToInt32(Session["year"].ToString());
            company_name = Session["Company_Name"].ToString();
            GSTNO = Session["Company_GST"].ToString();
            FssaiNO = Session["CompanyFSSAI_No"].ToString();
            TinNo = Session["Company_TIN"].ToString();
            GSTNO = Session["Company_GST"].ToString();
            panno = Session["CompanyPan_No"].ToString();
            partycode = Request.QueryString["partycode"];
            //  branchcode = Convert.ToInt32(Request.QueryString["branchcode"].ToString());
            partycode = Request.QueryString["partycode"];
            Address1 = Session["address"].ToString();
            string monumber = Session["Company_Phone"].ToString();

            string EmailId = "";
            if (!IsPostBack)
            {
                EmailId = Convert.ToString(clsCommon.getString("select Email_Id from nt_1_accountmaster where Ac_Code='" + partycode + "' and Company_Code='" + company_code + "'"));
                txtEmail.Text = EmailId;
            }

            DataTable dt = GetData(int.Parse(billno), type, year_code, company_code);

            SqlDataAdapter da = new SqlDataAdapter();

            string TotalAmount = dt.Rows[0]["TCS_Net_Payable"].ToString();

            string inWords = clsNoToWord.ctgword(TotalAmount);


            string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
                 + Session["Company_Code"].ToString() + "'");
            String path = Server.MapPath("") + "\\" + imagepath;

            imagepath = path.Replace("Report", "Images");

            string logo = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
             + Session["Company_Code"].ToString() + "'");
            String logopath = Server.MapPath("") + "\\" + logo;

            //string logopath1 = path.Replace("Sugar", "Images");
            logo = logopath.Replace("Report", "Images");


            rprt1.Load(Server.MapPath("Crypurchasebill.rpt"));
            rprt1.DataDefinition.FormulaFields["companystate"].Text = "\"" + companystate + "\"";

            rprt1.DataDefinition.FormulaFields["inword"].Text = "\"" + inWords + "\"";
            rprt1.DataDefinition.FormulaFields["gststatecode"].Text = "\"" + gststatecode + "\"";
            rprt1.DataDefinition.FormulaFields["Company_Name"].Text = "\"" + company_name + "\"";

            rprt1.DataDefinition.FormulaFields["note"].Text = "\"" + billfoter + "\"";
            rprt1.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";
            rprt1.DataDefinition.FormulaFields["logo"].Text = "\"" + logo + "\"";
            rprt1.DataDefinition.FormulaFields["AL1"].Text = "\"" + AL1 + "\"";
            rprt1.DataDefinition.FormulaFields["AL2"].Text = "\"" + AL2 + "\"";
            rprt1.DataDefinition.FormulaFields["AL3"].Text = "\"" + AL3 + "\"";
            rprt1.DataDefinition.FormulaFields["Al4"].Text = "\"" + AL4 + "\"";
            rprt1.DataDefinition.FormulaFields["other"].Text = "\"" + other + "\"";
            rprt1.DataDefinition.FormulaFields["fssai"].Text = "\"" + FssaiNO + "\"";
            rprt1.DataDefinition.FormulaFields["tin"].Text = "\"" + TinNo + "\"";
            rprt1.DataDefinition.FormulaFields["gstno"].Text = "\"" + GSTNO + "\"";
            rprt1.DataDefinition.FormulaFields["panno"].Text = "\"" + panno + "\"";

            rprt1.SetDataSource(dt);
            Crypurchasebill.ReportSource = rprt1;

            Crypurchasebill.RefreshReport();



        }
        catch (Exception)
        {

            throw;
        }

    }
    private DataTable GetData(int bill_no, string type, int year_code, int company_code)
    {
        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            SqlCommand cmd = new SqlCommand("select * from qrypurchaseheaddetail where doc_no=" + billno + " and Year_Code=" + year_code + " and Company_Code=" + company_code, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Doc_Date = dt.Rows[0]["Doc_Date"].ToString();
            }
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
                billfoter = dt1.Rows[0]["BillFooter"].ToString();
            }
        }
        return dt;
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
            string filename = filepath + "\\cryChequePrinting_" + billno + "_" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
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
            string filepath = @"C:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\pdffiles");
            }
            string pdfname = filepath + "\\DebitCreditNote" + billno + "_" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;




                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "DebitCreditNote";
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
                //  msg.Subject = "Bill No:" + sbno + " " + lorryno + " " + millname + " " + partyname;
                msg.Subject = "Dr. Cr. No. " + billno + " Date :" + Doc_Date + "";
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
        this.Crypurchasebill.ReportSource = null;

        Crypurchasebill.Dispose();

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