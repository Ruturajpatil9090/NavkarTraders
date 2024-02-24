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



public partial class Sugar_Report_pgeSaleBill_Print : System.Web.UI.Page
{
    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
    string bankdetail = string.Empty;
    string companyColor = string.Empty;
    string AddressColor = string.Empty;

    string podetail = string.Empty;
    string no = string.Empty;
    string grade = string.Empty;

    string grade1 = "";
    string deliverytype = string.Empty;
    string salerate = string.Empty;
    string salerate1 = string.Empty;
    string commission = string.Empty;
    string Sale_Rate = string.Empty;
    string Sale_Rate1 = "";

    string billto = string.Empty;
    int company_code;
    int year_code;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string ac_code;
    string utr_no;
    string salebillparty;
    string AcType = string.Empty;
    string mail = string.Empty;
    string mailnew = string.Empty;
    string doc_no = string.Empty;
    string season = string.Empty;
    string FssaiNO = string.Empty;
    string TinNo = string.Empty;
    string GSTNO = string.Empty;
    string panno = string.Empty;
    string eway = string.Empty;
    string Inv = string.Empty;
    string sbno = string.Empty;
    string lorryno = string.Empty;
    string cornumber = string.Empty;
    string purno = string.Empty;
    string chkwaybillno = string.Empty;
    string transoprt;
    string millinvoice = string.Empty;
    string netqntl = string.Empty;
    string dono = string.Empty;

    string cornumberChecking = string.Empty;
    ReportDocument rprt1 = new ReportDocument();
    ReportDocument rprt2 = new ReportDocument();
    ReportDocument rpt = new ReportDocument();
    string company_name = string.Empty;

    string singleitem = "1";
    string millname = string.Empty;
    string partyname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string companystate = Session["CompanyState"].ToString();
            string gststatecode = Session["CompanyGSTStateCode"].ToString();
            string corporate = Request.QueryString["corporate"].ToString();
            no = Request.QueryString["docnumber"];
            cornumberChecking = Request.QueryString["corporatenumber"];
            if (cornumberChecking != string.Empty)
            {
                cornumber = Request.QueryString["corporatenumber"];
            }
            else
            {
                cornumber = "0";
            }

            Title = "TAX INVOICE";
            company_name = Session["Company_Name"].ToString();
            doc_no = Request.QueryString["doc_no"];
            billto = Request.QueryString["billto"];
            eway = Request.QueryString["eway"];
            Inv = Request.QueryString["Inv"];
            FssaiNO = Session["CompanyFSSAI_No"].ToString();
            TinNo = Session["Company_TIN"].ToString();
            GSTNO = Session["Company_GST"].ToString();
            panno = Session["CompanyPan_No"].ToString();
            //  string sid = salebillparty;
            string DriverMobno = clsCommon.getString("select driver_no from nt_1_deliveryorder where doc_no=" + no + 
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
           
            DataTable dt = GetData();
            string TotalAmount = dt.Rows[0]["TCS_Net_Payable"].ToString();
            string tenderdetailid = clsCommon.getString("select tenderdetailid from nt_1_deliveryorder where doc_no=" + no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
            string salepartynew = clsCommon.getString("select Buyer from nt_1_tenderdetails where tenderdetailid=" + tenderdetailid +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string inWords = clsNoToWord.ctgword(TotalAmount);
            salebillparty = dt.Rows[0]["Ac_Code"].ToString();
            transoprt = dt.Rows[0]["Transport_Code"].ToString();
            string salepartyname = clsCommon.getString("Select Short_Name from qrymstaccountmaster where  Ac_Code='" + salepartynew + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            if (!IsPostBack)
            {

                txtEmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + salebillparty + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                txtTransportmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + transoprt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                txtpurmail.Text = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + salepartynew + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            }
            SqlDataAdapter da = new SqlDataAdapter();
            rpt.Load(Server.MapPath("crySaleBill_Print.rpt"));
            rpt.SetDataSource(dt);

            string docno = doc_no;
            docno = docno.PadLeft(6, '0');
            string docdate = clsCommon.getString("select doc_dateConverted from qrysaleheaddetail where  saleid=" + docno);

            crySaleBill_Print.ReportSource = rpt;
            rpt.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
            rpt.DataDefinition.FormulaFields["printhead"].Text = "\"Y\"";
            //  string printhead = rpt.DataDefinition.FormulaFields["printhead"].Text;
            sbno = clsCommon.getString("select doc_no from qrysaleheaddetail where  saleid=" + docno);
            string printyear = Session["printinsalebill"].ToString();
            if (printyear == "Y")
            {
                sbno = Session["selectedyear"].ToString() + "-" + sbno;
            }
            lorryno = clsCommon.getString("select LORRYNO from qrysaleheaddetail where  saleid=" + docno);
            rpt.DataDefinition.FormulaFields["word"].Text = "\"" + inWords + "\"";
            rpt.DataDefinition.FormulaFields["companystate"].Text = "\"" + companystate + "\"";
            rpt.DataDefinition.FormulaFields["gststatecode"].Text = "\"" + gststatecode + "\"";
            rpt.DataDefinition.FormulaFields["docdate"].Text = "\"" + docdate + "\"";
            rpt.DataDefinition.FormulaFields["saleparty"].Text = "\"" + salepartyname + "\"";
            rpt.DataDefinition.FormulaFields["AL1"].Text = "\"" + AL1 + "\"";
            rpt.DataDefinition.FormulaFields["AL2"].Text = "\"" + AL2 + "\"";
            rpt.DataDefinition.FormulaFields["AL3"].Text = "\"" + AL3 + "\"";
            rpt.DataDefinition.FormulaFields["Al4"].Text = "\"" + AL4 + "\"";
            rpt.DataDefinition.FormulaFields["other"].Text = "\"" + other + "\"";
            rpt.DataDefinition.FormulaFields["fssai"].Text = "\"" + FssaiNO + "\"";
            rpt.DataDefinition.FormulaFields["tin"].Text = "\"" + TinNo + "\"";
            rpt.DataDefinition.FormulaFields["gstno"].Text = "\"" + GSTNO + "\"";
            rpt.DataDefinition.FormulaFields["panno"].Text = "\"" + panno + "\"";
            rpt.DataDefinition.FormulaFields["docno"].Text = "\"" + "SB" + sbno + "\"";
            rpt.DataDefinition.FormulaFields["grade"].Text = "\"" + grade + "\"";
            rpt.DataDefinition.FormulaFields["grade1"].Text = "\"" + grade1 + "\"";
            rpt.DataDefinition.FormulaFields["companyColor"].Text = companyColor;
            rpt.DataDefinition.FormulaFields["AddressColor"].Text = AddressColor;
            rpt.DataDefinition.FormulaFields["Title"].Text = "\"" + Title + "\"";
            rpt.DataDefinition.FormulaFields["podetail"].Text = "\"" + podetail + "\"";

            rpt.DataDefinition.FormulaFields["chkwayno"].Text = "\"" + chkwaybillno + "\"";

            rpt.DataDefinition.FormulaFields["millinvoiceno"].Text = "\"" + millinvoice + "\"";

            rpt.DataDefinition.FormulaFields["frt"].Text = "\"" + corporate + "\"";
            if (DriverMobno != "0")
            {
                rpt.DataDefinition.FormulaFields["Mobileno"].Text = "\"" + DriverMobno + "\"";
            }
            else
            {
                rpt.DataDefinition.FormulaFields["Mobileno"].Text = "\"\"";
            }

            rpt.DataDefinition.FormulaFields["bankdetail"].Text = "\"" + bankdetail + "\"";
            if (season != "0")
            {
                rpt.DataDefinition.FormulaFields["tenderno"].Text = "\"" + season + "\"";
            }
            else
            {
                rpt.DataDefinition.FormulaFields["tenderno"].Text = "\"\"";
            }

            rpt.DataDefinition.FormulaFields["eway"].Text = "\"" + eway + "\"";
            rpt.DataDefinition.FormulaFields["Inv"].Text = "\"" + Inv + "\"";
            rpt.DataDefinition.FormulaFields["SaleRate"].Text = "\"" + Sale_Rate + "\"";
            rpt.DataDefinition.FormulaFields["SaleRate1"].Text = "\"" + Sale_Rate1 + "\"";
          
            rpt.DataDefinition.FormulaFields["singleitem"].Text = "\"" + singleitem + "\""; 


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
            // rprt.DataDefinition.FormulaFields["todate"].Text = datefrom;
            //rprt.DataDefinition.FormulaFields["Date"].Text = "\"Reverse Charge Report From " + datefrom + " To " + dateto + "\"";
            crySaleBill_Print.RefreshReport();
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
                SqlCommand cmd = new SqlCommand("select * from qrysaleheaddetail where  saleid in(" + doc_no + ")", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                singleitem = dt.Rows.Count.ToString();
            }

            if (billto != string.Empty && billto != "0")
            {
                DataSet dsbillto = clsDAL.SimpleQuery("select Ac_Code,Ac_Name_E,Address_E,cityname,citystate,isnull(citygststatecode,0) as citygststatecode " +
                    " ,Gst_No,AC_Pan from qrymstaccountmaster where Ac_Code='" + billto + "' and Company_Code='" + Session["Company_Code"].ToString() + "'");
                if (dsbillto != null)
                {
                    DataTable dtbillto = dsbillto.Tables[0];
                    if (dtbillto.Rows.Count > 0)
                    {
                        dt.Rows[0]["billtoname"] = dtbillto.Rows[0]["Ac_Name_E"].ToString();
                        dt.Rows[0]["billtoaddress"] = dtbillto.Rows[0]["Address_E"].ToString();
                        dt.Rows[0]["billtopin"] = dtbillto.Rows[0]["cityname"].ToString();
                        dt.Rows[0]["billtocitystate"] = dtbillto.Rows[0]["citystate"].ToString();

                        dt.Rows[0]["billtogstno"] = dtbillto.Rows[0]["Gst_No"].ToString();
                        dt.Rows[0]["billtogststatecode"] = dtbillto.Rows[0]["citygststatecode"].ToString();
                        dt.Rows[0]["billtopanno"] = dtbillto.Rows[0]["AC_Pan"].ToString();
                    }
                }
            }

            millname = dt.Rows[0]["millshortname"].ToString();
            partyname = dt.Rows[0]["billtoname"].ToString();

            string qry2 = "select * from carporatehead where Company_Code='" + Session["Company_Code"].ToString() + "' and doc_no='" + cornumber + "'";
            DataSet dsnew = clsDAL.SimpleQuery(qry2);
            if (dsnew != null)
            {
                DataTable dt1 = dsnew.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    podetail = dt1.Rows[0]["pono"].ToString();
                }

            }
            string qry3 = "select * from nt_1_deliveryorder where Company_Code='" + Session["Company_Code"].ToString() + "' and doc_no='" + no +
                "' and Year_Code=" + Session["year"].ToString();
            DataSet dsnew1 = clsDAL.SimpleQuery(qry3);
            if (dsnew != null)
            {
                DataTable dt1 = dsnew1.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    purno = dt1.Rows[0]["purc_no"].ToString();
                    grade = dt1.Rows[0]["grade"].ToString();
                    grade1 = dt1.Rows[0]["grade1"].ToString();

                    chkwaybillno = dt1.Rows[0]["EWayBillChk"].ToString();
                    millinvoice = dt1.Rows[0]["MillInvoiceNo"].ToString();
                    deliverytype = dt1.Rows[0]["Delivery_Type"].ToString();
                    salerate = dt1.Rows[0]["sale_rate"].ToString();
                    salerate1 = dt1.Rows[0]["sale_rate1"].ToString();
                    Sale_Rate1 = salerate1;
                    commission = dt1.Rows[0]["Tender_Commission"].ToString();
                    //     salebillparty = dt1.Rows[0]["Ac_Code"].ToString();
                    if (deliverytype == "C")
                    {
                        Sale_Rate = salerate + " + " + commission + " Commission";
                    }
                    if (deliverytype == "N")
                    {
                        Sale_Rate = salerate + " F.O.R. with GST";
                    }
                    if (deliverytype == "A")
                    {
                        Sale_Rate = salerate + " F.O.R without GST";
                    }
                }

            }
            season = clsCommon.getString("select season from nt_1_tender where Tender_No=" + purno +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); 
                //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));


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
                    companyColor = dt1.Rows[0]["CompanyNameColor"].ToString();
                    AddressColor = dt1.Rows[0]["AddressColor"].ToString();
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
            string pdfname = filepath + "\\Salebill" + sbno + "-" + lorryno + ".pdf";
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
            string pdfname = filepath + "\\SaleBillNO" + sbno + "" + lorryno + ".pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty && txtEmail.Text != "0")
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                if (lorryno != string.Empty)
                {
                    lorryno = "Lorry:" + lorryno;
                }
                if (netqntl != string.Empty)
                {
                    netqntl = "NETQNTL:" + netqntl;
                }
                if (partyname != string.Empty)
                {
                    partyname = "billtoname:" + partyname;
                }
                if (dono != string.Empty)
                {
                    dono = "DO_No:" + dono;
                }


                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "Salebill_DocNo:" + sbno + "-" + lorryno;
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
                msg.Subject = "Bill No:" + sbno + " " + lorryno + " " + netqntl + " " + partyname + " " + dono;

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
    protected void btnallmail_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath = @"D:\ashwini\bhavani10012019\accowebBhavaniNew\PAN\cryChequePrinting.pdf";
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\SaleBillNO" + sbno + "" + lorryno + ".pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtEmail.Text != string.Empty && txtEmail.Text != "0" || txtTransportmail.Text != string.Empty && txtTransportmail.Text != "0" || txtpurmail.Text != string.Empty && txtpurmail.Text != "0")
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                if (lorryno != string.Empty)
                {
                    lorryno = "Lorry:" + lorryno;
                }
                if (millname != string.Empty)
                {
                    millname = "Mill:" + millname;
                }
                if (partyname != string.Empty)
                {
                    partyname = "Getpass:" + partyname;
                }
                if (txtTransportmail.Text != string.Empty && txtTransportmail.Text != "0")
                {
                    mail = "" + mail + " " + txtTransportmail.Text + "";
                }
                if (txtpurmail.Text != string.Empty && txtpurmail.Text != "0")
                {
                    mail = "" + mail + " " + txtpurmail.Text + "";
                }

                mailnew = "" + mail + "";

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "Salebill_DocNo:" + sbno + "-" + lorryno;
                Attachment attachment = new Attachment(pdfname, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailnew);
                msg.To.Add(mail);
                msg.Body = "Salebill";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                //msg.Subject = "DOC.No:";
                msg.Subject = "Bill No:" + sbno + " " + lorryno + " " + millname + " " + partyname;
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

    protected void btnmilltransport_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath = @"D:\ashwini\bhavani10012019\accowebBhavaniNew\PAN\cryChequePrinting.pdf";
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\SaleBillNO" + sbno + "" + lorryno + ".pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtTransportmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtTransportmail.Text;

                if (lorryno != string.Empty)
                {
                    lorryno = "Lorry:" + lorryno;
                }
                if (millname != string.Empty)
                {
                    millname = "Mill:" + millname;
                }
                if (partyname != string.Empty)
                {
                    partyname = "Getpass:" + partyname;
                }

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "Salebill_DocNo:" + sbno + "-" + lorryno;
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
                msg.Subject = "Bill No:" + sbno + " " + lorryno + " " + millname + " " + partyname;

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

    protected void btnpurchmail_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath = @"D:\ashwini\bhavani10012019\accowebBhavaniNew\PAN\cryChequePrinting.pdf";
            string filepath = @"D:\pdffiles";
            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\pdffiles");
            }
            string pdfname = filepath + "\\SaleBillNO" + sbno + "" + lorryno + ".pdf";

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfname);

            if (txtpurmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtpurmail.Text;
                if (lorryno != string.Empty)
                {
                    lorryno = "Lorry:" + lorryno;
                }
                if (millname != string.Empty)
                {
                    millname = "Mill:" + millname;
                }
                if (partyname != string.Empty)
                {
                    partyname = "Getpass:" + partyname;
                }

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "Salebill_DocNo:" + sbno + "-" + lorryno;
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
                msg.Subject = "Bill No:" + sbno + " " + lorryno + " " + millname + " " + partyname;
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
        this.crySaleBill_Print.ReportSource = null;

        crySaleBill_Print.Dispose();

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