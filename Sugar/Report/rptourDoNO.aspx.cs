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

public partial class Sugar_Report_rptourDoNO : System.Web.UI.Page
{
    string fromdt = string.Empty;
    string todt = string.Empty;
    string mail = string.Empty;
    int company_code;
    int year_code;
    string unitno = string.Empty;
    string Accode = string.Empty;
    string voutchername = string.Empty;
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
    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
    string bankdetail = string.Empty;
    string FssaiNO = string.Empty;
    string TinNo = string.Empty;
    string GSTNO = string.Empty;
    string panno = string.Empty;
    string inWords = string.Empty;
    string billfoter = string.Empty;
    string TotalAmount;
    string docno = string.Empty;
    string tdsrate = string.Empty;
    string mill_rate = string.Empty;
    string quantal = string.Empty;
    double basicamt ;
    double tdsamount;
    string amount;
    // string TotalAmount = string.Empty;
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
                string companystate = Session["CompanyState"].ToString();
                string gststatecode = Session["CompanyGSTStateCode"].ToString();
                year_code = Convert.ToInt32(Session["year"].ToString());
                fromdt = Request.QueryString["fromdt"];
                todt = Request.QueryString["todt"];
                FssaiNO = Session["CompanyFSSAI_No"].ToString();
                TinNo = Session["Company_TIN"].ToString();
                GSTNO = Session["Company_GST"].ToString();
                panno = Session["CompanyPan_No"].ToString();
                pagehead = "Freight Register from " + fromdt + " To " + todt;
                voutchername = clsCommon.getString("select Ac_Name_E from nt_1_accountmaster where Ac_Code=" + Accode);

                datefrom = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                dateto = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                DataTable dt = GetData(fromdt, todt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        TotalAmount = dt.Rows[i]["Mill_AmtWO_TCS"].ToString();
                        tdsrate = dt.Rows[i]["PurchaseTDSRate"].ToString();
                        mill_rate = dt.Rows[i]["mill_rate"].ToString();
                        quantal = dt.Rows[i]["quantal"].ToString();
                        basicamt = Convert.ToDouble(mill_rate) * Convert.ToDouble(quantal);
                        tdsamount = basicamt * Convert.ToDouble(tdsrate) / 100;
                        amount = Convert.ToString(Convert.ToDouble(TotalAmount) - tdsamount);


                        //TotalAmount = dt.Rows[i]["amount"].ToString();

                        inWords = clsNoToWord.ctgword(amount);
                        dt.Rows[i]["WordinAmount"] = inWords;
                        docno = dt.Rows[i]["doc_no"].ToString();

                        string utrnarration = clsCommon.getString("select Narration from qrydodetail where  doid='" + docno + "' and Company_Code='" + Session["Company_Code"] + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'");
                        dt.Rows[i]["utrnarration"] = utrnarration.ToString();

                    }
                }

                SqlDataAdapter da = new SqlDataAdapter();

                rprt1.Load(Server.MapPath("CryMultipleourdono.rpt"));
                rprt1.SetDataSource(dt);
                CryMultipleourdono.ReportSource = rprt1;

                company_name = Session["Company_Name"].ToString();
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

                rprt1.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
                rprt1.DataDefinition.FormulaFields["AL1"].Text = "\"" + AL1 + "\"";
                rprt1.DataDefinition.FormulaFields["AL2"].Text = "\"" + AL2 + "\"";
                rprt1.DataDefinition.FormulaFields["AL3"].Text = "\"" + AL3 + "\"";
                rprt1.DataDefinition.FormulaFields["Al4"].Text = "\"" + AL4 + "\"";
                rprt1.DataDefinition.FormulaFields["note"].Text = "\"" + billfoter + "\"";
                //    rprt1.DataDefinition.FormulaFields["companystate"].Text = "\"" + companystate + "\"";
                //   rprt1.DataDefinition.FormulaFields["gststatecode"].Text = "\"" + gststatecode + "\"";


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
    private DataTable GetData(string fromdt, string todt)
    {
        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            if (Accode == string.Empty)
            {
                SqlCommand cmd = new SqlCommand("select * from qrydohead where  doc_date between '" + datefrom + "' and '" + dateto + "'and company_code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code = " + Convert.ToInt32(Session["year"].ToString()) + "", con);

                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from qrydohead where mill_code = " + Accode + " and  doc_date between '" + datefrom + "' and '" + dateto + "'and company_code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code = " + Convert.ToInt32(Session["year"].ToString()) + "", con);

                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                //   dt.Columns.Add((new DataColumn("Amount", typeof(double))));

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
                bankdetail = dt1.Rows[0]["bankdetail"].ToString();
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
            string filename = filepath + "\\SupplierPurchaseOrder" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
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
            string filename = filepath + "\\SupplierPurchaseOrder_" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
            //rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "SupplierPurchaseOrder";
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
                msg.Body = "SupplierPurchaseOrder";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                msg.Subject = "No:";
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
        this.CryMultipleourdono.ReportSource = null;

        CryMultipleourdono.Dispose();

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