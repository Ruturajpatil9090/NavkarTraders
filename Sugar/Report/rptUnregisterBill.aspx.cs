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
public partial class Sugar_Report_rptUnregisterBill : System.Web.UI.Page
{
    string Company_Name = string.Empty;
    string Address1 = string.Empty;
    string Address2 = string.Empty;
    string Address3 = string.Empty;
    string Address4 = string.Empty;
    string Inword = string.Empty;
    string Doc_No = string.Empty;
    string other = string.Empty;
    Int32 Company_Code;
    Int32 Year_Code;
    ReportDocument rprt1 = new ReportDocument();
    string mail = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Doc_No = Request.QueryString["Doc_No"];
            Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            Year_Code = Convert.ToInt32(Session["year"].ToString());
            Company_Name = Session["Company_Name"].ToString();
            string qry = "select * from tblvoucherheadaddress where Company_Code='" + Session["Company_Code"].ToString() + "'";
            DataSet ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    Address1 = dt1.Rows[0]["AL1"].ToString();
                    Address2 = dt1.Rows[0]["AL2"].ToString();
                    Address3 = dt1.Rows[0]["AL3"].ToString();
                    Address4 = dt1.Rows[0]["AL4"].ToString();
                    other = dt1.Rows[0]["Other"].ToString();
                }
            }
          

            DataTable dt = GetData();
            SqlDataAdapter da = new SqlDataAdapter();
            string TotalAmount = dt.Rows[0]["amount"].ToString();
            string inwordsnew = TotalAmount.Replace("-", "");
            string inWords = clsNoToWord.ctgword(inwordsnew);
            rprt1.Load(Server.MapPath("cryUnregisterBill.rpt"));
            rprt1.SetDataSource(dt);
            cryUnregisterBill.ReportSource = rprt1;

            rprt1.DataDefinition.FormulaFields["Company_Name"].Text = "\"" + Company_Name + "\"";
            rprt1.DataDefinition.FormulaFields["other"].Text = "\"" + other + "\"";
            rprt1.DataDefinition.FormulaFields["Address1"].Text = "\"" + Address1 + "\"";
            rprt1.DataDefinition.FormulaFields["Address2"].Text = "\"" + Address2 + "\"";
            rprt1.DataDefinition.FormulaFields["Address3"].Text = "\"" + Address3 + "\"";
            rprt1.DataDefinition.FormulaFields["Address4"].Text = "\"" + Address4 + "\"";
            rprt1.DataDefinition.FormulaFields["inword"].Text = "\"" + inWords + "\"";

            string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
             + Session["Company_Code"].ToString() + "'");
            String path = Server.MapPath("") + "\\" + imagepath;

            imagepath = path.Replace("Report", "Images");

            string logo = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='S' and Company_Code='"
           + Session["Company_Code"].ToString() + "'");
            String logopath = Server.MapPath("") + "\\" + logo;

          
            logo = logopath.Replace("Report", "Images");

            rprt1.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";
            rprt1.DataDefinition.FormulaFields["logo"].Text = "\"" + logo + "\"";
           



            cryUnregisterBill.RefreshReport();
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

            SqlCommand cmd = new SqlCommand("select * from qryOtherInvoice where  doc_no=" + Doc_No +
                "  and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and "
             + "  year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "", con);

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
            // string filepath=@"D:\pdffiles\cryChequePrinting.pdf";
            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename = filepath + "\\UnregisterBill" + Company_Code + "_" + Year_Code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
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
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\UnregisterBill_" + Company_Code + "_" + Year_Code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty && txtEmail.Text != "0")
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                


                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "UnregisterBill";
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "UnregisterBill";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                //msg.Subject = "DOC.No:";
                msg.Subject = "No:" + Doc_No;

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
        this.cryUnregisterBill.ReportSource = null;

        cryUnregisterBill.Dispose();

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