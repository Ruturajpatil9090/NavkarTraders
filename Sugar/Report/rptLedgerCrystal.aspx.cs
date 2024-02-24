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
using System.Xml.Linq;
public partial class Foundman_Report_rptLedgerCrystal : System.Web.UI.Page
{
    string mail = string.Empty;
    int company_code;
    int year_code;
    string type = string.Empty;
    string Fromdt = string.Empty;
    string Todt = string.Empty;
    string accode;
    string company_name = string.Empty;
    string City = string.Empty;
    string State = string.Empty;
    string Address = string.Empty;
    ReportDocument rprt1 = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        type = Request.QueryString["ac_type"];
        Fromdt = Request.QueryString["fromdt"];
        Todt = Request.QueryString["todt"];
        company_code = Convert.ToInt32(Session["Company_Code"].ToString());
        year_code = Convert.ToInt32(Session["year"].ToString());
        accode = Request.QueryString["accode"].ToString();
        DataTable dt = GetData(accode, company_code);
        SqlDataAdapter da = new SqlDataAdapter();
        string datefrom = DateTime.Parse(Fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        string dateto = DateTime.Parse(Todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //using (rprt1 = new ReportDocument())
        //{

        rprt1.Load(Server.MapPath("cryCrystalledger.rpt"));
        rprt1.SetDataSource(dt);
        cryLedgerCrystal.ReportSource = rprt1;
        company_name = Session["Company_Name"].ToString();

       // State = Session["state"].ToString();
        Address = Session["Company_Address"].ToString();
        ///City = Session["city"].ToString();

        rprt1.DataDefinition.FormulaFields["date"].Text = "\"Ledger Report From " + datefrom + " To " + dateto + "\"";

        rprt1.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
        rprt1.DataDefinition.FormulaFields["address"].Text = "\"" + Address + "\"";
       // rprt1.DataDefinition.FormulaFields["city"].Text = "\"" + City + "\"";
        rprt1.DataDefinition.FormulaFields["state"].Text = "\"" + State + "\"";
        rprt1.Refresh();

     //   rprt1.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, Page.Title);
       
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        //rprt1.Close();
        //rprt1.Clone();
        //rprt1.Dispose();
        //GC.Collect();
        this.cryLedgerCrystal.ReportSource = null;

        cryLedgerCrystal.Dispose();

        if (rprt1 != null)
        {

            rprt1.Close();

            rprt1.Dispose();

            rprt1 = null;

        }

        GC.Collect();

        GC.WaitForPendingFinalizers();
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
            string filepath = "C:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("C:\\PDFFiles");
            }
            string filename1 = filepath + "\\Legder" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename1);


            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(filename1);
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

    private DataTable GetData(string accode, int company_Code)
    {
        double netdebit = 0.00; double netcredit = 0.00;
        DataTable dt = new DataTable();
        DataTable dtreturn = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            Fromdt = DateTime.Parse(Fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            Todt = DateTime.Parse(Todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            SqlCommand cmd;
            //if (accode != string.Empty)
            //{
            //    cmd = new SqlCommand("select * from qryLedgerCrystal where AC_CODE in (" + accode + ") and DOC_DATE between '" + Fromdt + "' and '" + Todt + "' and YEAR_CODE=" + year_code +
            //       " and Company_Code='" + company_code + "' order by DOC_DATE asc,ORDER_CODE", con);
            //}
            //else
            //{
            //    cmd = new SqlCommand("select * from qryLedgerCrystal where DOC_DATE between '" + Fromdt + "' and '" + Todt + "' and YEAR_CODE=" + year_code +
            //    " and Company_Code='" + company_code + "' order by DOC_DATE asc,ORDER_CODE", con);

            //}
            string frmdt = Session["Start_Date"].ToString();
            //string Ac_Code = grdDetail.Rows[i].Cells[3].Text;
            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            child1.SetAttributeValue("Company_Code", Convert.ToInt32(Session["Company_Code"].ToString()));
            child1.SetAttributeValue("PartyCode", accode);
            child1.SetAttributeValue("FromDate", Fromdt);
            child1.SetAttributeValue("Year_Code", Convert.ToInt32(Session["year"].ToString()));
            child1.SetAttributeValue("StartDate", frmdt);
            child1.SetAttributeValue("ToDate", Todt);


            root.Add(child1);

            string XMLReport = root.ToString();
            XDocument XDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = XDoc.ToString(SaveOptions.DisableFormatting);
            DataSet xml_ds = new DataSet();
            string spname = "LedgerCrystal";
            string xmlfile = XMLReport;
            string op = "";
            string returnmaxno = "";
            int flag;

            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);


            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            dt = xml_ds.Tables[0];
            DataTable dtopinng = new DataTable();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows.Count > 0)
                {
                    dtreturn.Columns.Add("TRAN_TYPE", typeof(string));
                    dtreturn.Columns.Add("DOC_NO", typeof(Int32));
                    dtreturn.Columns.Add("DOC_DATE", typeof(string));
                    dtreturn.Columns.Add("NARRATION", typeof(string));
                    dtreturn.Columns.Add("Debit", typeof(double));
                    dtreturn.Columns.Add("Credit", typeof(double));
                    dtreturn.Columns.Add("Balance", typeof(double));
                    dtreturn.Columns.Add("DRCRNew", typeof(string));
                    dtreturn.Columns.Add("AC_CODE", typeof(int));
                    dtreturn.Columns.Add("Ac_Name_E", typeof(string));
                    dtreturn.Columns.Add("DO_no", typeof(string));
                    dt.DefaultView.Sort = "DOC_DATE ,DOC_NO asc,ORDER_CODE";
                    dt = dt.DefaultView.ToTable();
                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "AC_CODE");
                    //for selecting All accode
                  
                    for (int j = 0; j < distinctValues.Rows.Count; j++)
                    {

                        DataView view1 = new DataView(dt, "AC_CODE='" + distinctValues.Rows[j]["AC_CODE"].ToString() + "'", "AC_CODE", DataViewRowState.CurrentRows);
                        DataTable dtAcData = view1.ToTable(true, "TRAN_TYPE", "DOC_NO", "DOC_DATE", "AC_CODE", "NARRATION", "AMOUNT", "DRCR", "Credit", "Debit", "Balance", "DRCRNew", "Ac_Name_E", "group_Type", "ORDER_CODE");

                        string groputype = dtAcData.Rows[0]["group_Type"].ToString();
                        string qry = "";
                        DataView view11;
                        DataTable newdt;
                        if (groputype == "B")
                        {
                            dtopinng = xml_ds.Tables[1];
                             view11 = new DataView(dtopinng, "AC_CODE='" + distinctValues.Rows[j]["AC_CODE"].ToString() + "'", "AC_CODE", DataViewRowState.CurrentRows);
                             newdt = view11.ToTable(true, "AC_CODE", "OpBal");


                            
                            //qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from GLEDGER " +
                            //    " where DOC_DATE < '" + Fromdt + "' and Ac_code=" + distinctValues.Rows[j]["AC_CODE"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                        }
                        else
                        {
                            dtopinng = xml_ds.Tables[2];
                            view11 = new DataView(dtopinng, "AC_CODE='" + distinctValues.Rows[j]["AC_CODE"].ToString() + "'", "AC_CODE", DataViewRowState.CurrentRows);
                            newdt = view11.ToTable(true, "AC_CODE", "OpBal");
                            //qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from GLEDGER"
                            //   + " where DOC_DATE >= '" + frmdt + "' and DOC_DATE < '" + Fromdt + "' and Ac_code=" + distinctValues.Rows[j]["AC_CODE"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                        }

                        //for All data add opening balance
                        //  = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from GLEDGER where DOC_DATE >= '" + frmdt + "' and DOC_DATE < '" + Fromdt + "' and Ac_code=" + distinctValues.Rows[j]["AC_CODE"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                        DataSet ds = new DataSet();
                            ds.Tables.Add(newdt);
                        double opBal = 0.0;


                        //dtopinng = xml_ds.Tables[1];
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                newdt = ds.Tables[0];
                                if (newdt.Rows.Count > 0)
                                {
                                    opBal = Convert.ToDouble(newdt.Rows[0][1].ToString());
                                    //nar = dt.Rows[0][2].ToString();
                                    //if (!string.IsNullOrEmpty(nar.Trim().ToString()))
                                    //{
                                    //    nar = nar.Remove(0, 15);
                                    //}
                                }
                            }
                        }


                        if (groputype != "B")
                        {
                            opBal = 0;
                        }
                        DataRow dr = dtreturn.NewRow();
                        dr[0] = "OP";
                        dr[1] = 0.00;
                        dr[2] = DateTime.Parse(Fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        dr[3] = "Opening Balance";
                        if (opBal > 0)
                        {
                            dr[4] = Math.Round(opBal, 2);
                            dr[5] = 0.00;
                            dr[6] = Math.Round(opBal, 2);
                            dr[7] = "Dr";
                            netdebit += opBal;
                        }
                        else
                        {
                            dr[4] = 0.00;
                            dr[5] = Math.Round(-opBal, 2);
                            dr[6] = dr[5];
                            dr[7] = "Cr";
                            netcredit += -opBal;
                        }
                        dr[8] = distinctValues.Rows[j]["AC_CODE"].ToString();
                        dr[9] = dtAcData.Rows[0]["Ac_Name_E"].ToString();
                        dtreturn.Rows.Add(dr);
                        //Adding records
                        for (int i = 0; i < dtAcData.Rows.Count; i++)
                        {
                            dr = dtreturn.NewRow();
                            dr[0] = dtAcData.Rows[i]["TRAN_TYPE"].ToString();
                            dr[1] = dtAcData.Rows[i]["DOC_NO"].ToString();

                            if (dtAcData.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                            {
                                string s = dtAcData.Rows[i]["DOC_DATE"].ToString();
                                dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                            }

                            string SORT_TYPE = dtAcData.Rows[i]["TRAN_TYPE"].ToString();
                            string SORT_NO = dtAcData.Rows[i]["DOC_NO"].ToString();
                            dr[3] = Server.HtmlDecode(dtAcData.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");

                            if (dtAcData.Rows[i]["DRCR"].ToString() == "D")
                            {
                                opBal = opBal + Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString());

                                dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));
                                dr[5] = 0.00;
                                netdebit += Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString());
                            }
                            else
                            {
                                opBal = opBal - Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));
                                netcredit += Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));

                                dr[4] = 0.00;
                                dr[5] = Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));
                            }
                            if (opBal > 0)
                            {
                                dr[6] = Math.Round(Convert.ToDouble(opBal), 2);
                                dr[7] = "Dr";
                            }
                            else
                            {
                                dr[6] = 0 - Math.Round(opBal, 2);
                                dr[7] = "Cr";
                            }
                            dr[8] = distinctValues.Rows[j]["AC_CODE"].ToString();
                            dr[9] = dtAcData.Rows[i]["Ac_Name_E"].ToString();
                           // dr[10] = dtAcData.Rows[i]["Ac_Name_E"].ToString();
                            if (dt.Rows[i]["TRAN_TYPE"].ToString() == "SB")
                            {
                                string sbno = dt.Rows[i]["DOC_NO"].ToString();
                                if (sbno == "0")
                                {
                                    dr[10] = "0";
                                }
                                else
                                {
                                    string do_no = clsCommon.getString("select DO_No from NT_1_SugarSale where doc_no='" + dt.Rows[i]["DOC_NO"].ToString() + "' and  Company_Code="
                                       + Convert.ToInt32(Session["Company_Code"].ToString())
                                       + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                                    dr[10] = do_no;
                                }
                            }
                            if (dt.Rows[i]["TRAN_TYPE"].ToString() == "PS")
                            {
                                string do_no = clsCommon.getString("select PURCNO from NT_1_SugarPurchase where doc_no='" + dt.Rows[i]["DOC_NO"].ToString() + "' and  Company_Code="
                                    + Convert.ToInt32(Session["Company_Code"].ToString())
                                    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                                dr[10] = do_no;
                            }
                            dtreturn.Rows.Add(dr);


                        }


                    }

                }


            }


        }


        return dtreturn;
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
            string filename = filepath + "\\ledger" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
            //rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

            if (txtEmail.Text != string.Empty)
            {
                //string fileName = "Saudapending.pdf";
                //string filepath1 = "~/PAN/" + fileName;

                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "ledger";
                // Attachment attachment = new Attachment(Server.MapPath(filename), contentType);
                Attachment attachment = new Attachment(filename);
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
           //     EncryptPass enc = new EncryptPass();
              //  emailPassword = enc.Decrypt(emailPassword);
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                msg.Body = "ledger";
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ExportOptions exportOpts = new ExportOptions();
        ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
        DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
        exportOpts = rprt1.ExportOptions;

        // Set the excel format options.
        excelFormatOpts.ExcelUseConstantColumnWidth = true;
        excelFormatOpts.ExcelTabHasColumnHeadings = true;
        exportOpts.ExportFormatType = ExportFormatType.Excel;
        exportOpts.FormatOptions = excelFormatOpts;
        string filepath = "C:\\EXCELFiles";

        if (!System.IO.Directory.Exists(filepath))
        {
            System.IO.Directory.CreateDirectory("C:\\EXCELFiles");
        }
        string filename = filepath + "\\Ledger" + accode + "_" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
        // Set the disk file options and export.
        exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
        diskOpts.DiskFileName = filename;
        exportOpts.DestinationOptions = diskOpts;

        rprt1.Export();
    }
}