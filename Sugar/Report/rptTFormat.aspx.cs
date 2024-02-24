using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using iTextSharp.tool.xml;


public partial class Report_rptTFormat : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string tblHead = "";
    string qryCommon = "";
    string email = string.Empty;
    string f = "../GSReports/TFormat_.htm";
    string f_Main = "../Report/rptTFormat";
    IFormatProvider ifrDT = CultureInfo.CreateSpecificCulture("en-GB");
    string prefix = string.Empty;
    string Ac_Type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qryGledgernew";

        if (Session["tblPrefix"] != null)
        {
            tblPrefix = Session["tblPrefix"].ToString();
        }
        else
        {
            prefix = clsCommon.getString("Select tblPrefix from tblPrefix");
            tblPrefix = prefix.ToString();
        }

        // ViewState["fromDt"] = Request.QueryString["fromDt"];
        ViewState["ToDt"] = Request.QueryString["ToDt"];
        ViewState["whr1"] = "";
        Ac_Type = Request.QueryString["Ac_Type"].ToString();
        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Company_Name"].ToString();
            this.bindData("Ac_Name", "ASC");
        }
    }

    private void bindData(string sortBy, string ascordesc)
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                DataTable dtCredit = new DataTable();
                DataTable dtDebit = new DataTable();

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataSet dstemp = new DataSet();
                string qry = "";
                dtCredit.Columns.Add("AC_Code", typeof(Int32));
                dtCredit.Columns.Add("Ac_Name", typeof(string));
                dtCredit.Columns.Add("Balance", typeof(double));
                dtCredit.Columns.Add("CityName", typeof(string));

                dtDebit.Columns.Add("AC_Code", typeof(Int32));
                dtDebit.Columns.Add("Ac_Name", typeof(string));
                dtDebit.Columns.Add("Balance", typeof(double));
                dtDebit.Columns.Add("CityName", typeof(string));


                if (ViewState["ToDt"] != null)
                {
                    string ToDt = DateTime.Parse(ViewState["ToDt"].ToString(), ifrDT).ToString("yyyy/MM/dd");


                    if (Ac_Type != "All")
                    {

                        if (Ac_Type == "Q")
                        {

                            qry = "select AC_CODE,Ac_Name_E,CityName,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance "
                                  + " from qryGledgernew  "
                                  + " where group_type='B' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + ToDt + "'"
                                  + " group by AC_CODE,Ac_Name_E,CityName  having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end)!=0 ";
                            dstemp = clsDAL.SimpleQuery(qry);

                            qry = "select AC_CODE,Ac_Name_E,CityName,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance "
                                  + " from qryGledgernew  "
                                  + " where group_type<>'B'  and year_code=" + Convert.ToInt32(Session["Year"].ToString()) + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + ToDt + "'"
                                  + " group by AC_CODE,Ac_Name_E,CityName  having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end)!=0 ";
                            ds = clsDAL.SimpleQuery(qry);
                            ds.Merge(dstemp);
                        }
                        else
                        {
                            qry = "select AC_CODE,Ac_Name_E,CityName,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance "
                                  + " from qryGledgernew  "
                                  + " where Ac_type='" + Ac_Type + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + ToDt + "'"
                                  + " group by AC_CODE,Ac_Name_E,CityName  having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end)!=0 ";

                            ds = clsDAL.SimpleQuery(qry);

                        }

                    }
                    else
                    {

                        qry = "select AC_CODE,Ac_Name_E,CityName,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance "
                               + " from qryGledgernew  "
                               + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + ToDt + "'"
                               + " group by AC_CODE,Ac_Name_E,CityName  having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end)!=0 ";
                        ds = clsDAL.SimpleQuery(qry);
                    }



                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    double bal = 0.00;
                                    if (dt.Rows[i]["Balance"].ToString() != string.Empty)
                                    {
                                        bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString());
                                    }

                                    if (bal > 0)
                                    {
                                        DataRow dr = dtDebit.NewRow();
                                        dr["AC_Code"] = dt.Rows[i]["AC_CODE"].ToString();
                                        dr["Ac_Name"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                        dr["Balance"] = dt.Rows[i]["Balance"].ToString();
                                        dr["CityName"] = dt.Rows[i]["CityName"].ToString();
                                        dtDebit.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        DataRow dr = dtCredit.NewRow();
                                        dr["AC_Code"] = dt.Rows[i]["AC_CODE"].ToString();
                                        dr["Ac_Name"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                        double balance = Convert.ToDouble(dt.Rows[i]["Balance"].ToString());
                                        dr["Balance"] = Math.Abs(balance);
                                        dr["CityName"] = dt.Rows[i]["CityName"].ToString();

                                        dtCredit.Rows.Add(dr);
                                    }
                                }

                            }
                        }
                    }


                    if (dt.Rows.Count > 0)
                    {
                        DataView dvDebit = dtDebit.DefaultView;
                        dvDebit.Sort = sortBy + " " + ascordesc;
                        dtDebit = dvDebit.ToTable();
                        dtl_Debit.DataSource = dtDebit;
                        dtl_Debit.DataBind();
                        DataView dvCredit = dtCredit.DefaultView;
                        dvCredit.Sort = sortBy + " " + ascordesc;
                        dtCredit = dvCredit.ToTable();
                        dtl_Credit.DataSource = dtCredit;
                        dtl_Credit.DataBind();



                        double creditTotal = Convert.ToDouble(dtCredit.Compute("SUM(Balance)", string.Empty));
                        lblCreditTotal.Text = creditTotal.ToString();


                        double debitTotal = Convert.ToDouble(dtCredit.Compute("SUM(Balance)", string.Empty));
                        lblDebitTotal.Text = debitTotal.ToString();
                    }
                }
            }
        }
        catch (Exception eex)
        {
            Response.Write(eex.Message);
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            email = txtEmail.Text.ToString();
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);
            PrintPanel.RenderControl(tw);
            string s = sw.ToString();
            try
            {
                using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(s);
                    }
                }
            }
            catch (Exception ee)
            {
                f = f_Main + ".htm";
                using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(s);
                    }
                }
            }
            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "T-Fomat Report";
            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            msg.Subject = "T-Format Balance Report " + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.IsBodyHtml = true;
            if (smtpPort != string.Empty)
            {
                SmtpServer.Port = Convert.ToInt32(smtpPort);
            }
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
            SmtpServer.Send(msg);
        }

        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        PrintPanel.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        string strFile = "report.xls";
        string strcontentType = "application/excel";
        Response.ClearContent();
        Response.ClearHeaders();
        Response.BufferOutput = true;
        Response.ContentType = strcontentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        Response.Write(StrExport.ToString());
        Response.Flush();
        Response.Close();
        Response.End();
    }

    protected void btnSortByBalance_Click(object sender, EventArgs e)
    {
        if (btnSortByBalance.Text == "Sort By Balance ASC")
        {
            this.bindData("Balance", "ASC");
            btnSortByBalance.Text = "Sort By Balance DESC";
        }
        else
        {
            this.bindData("Balance", "DESC");
            btnSortByBalance.Text = "Sort By Balance ASC";
        }
    }

    protected void btnSortByName_Click(object sender, EventArgs e)
    {
        if (btnSortByName.Text == "Sort By Name ASC")
        {
            this.bindData("Ac_Name", "ASC");
            btnSortByName.Text = "Sort By Name DESC";
        }
        else
        {
            this.bindData("Ac_Name", "DESC");
            btnSortByName.Text = "Sort By Name ASC";
        }
    }

    protected void btnPDfDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            PrintPanel.RenderControl(hw);
            //  string s1 = sw.ToString().Replace("font-size: medium", "font-size: xx-small");
            //StringReader sr = new StringReader(s1.ToString());
            StringReader sr = new StringReader(sw.ToString().Replace("../Images", Server.MapPath("~/Images")));

            Document pdfDoc = new Document(iTextSharp.text.PageSize.A4);
            var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch
        {
        }
    }

    protected void btnPDf_Click(object sender, EventArgs e)
    {
        #region[Pdf comment]
        //try
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    tblmn.RenderControl(hw);
        //    string s1 = sw.ToString().Replace("font-size: medium", "font-size: xx-small");
        //    //StringReader sr = new StringReader(s1.ToString());
        //    StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

        //    Document pdfDoc = new Document(iTextSharp.text.PageSize.A4);
        //    var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        //    pdfDoc.Open();
        //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}
        //catch
        //{
        //}
        #endregion

        string filepath = "";
        Attachment attachment = null;
        try
        {
            email = txtEmail.Text.ToString();
            //if (txttransportemilid.Text != string.Empty)
            //{
            //    email = txtEmail.Text.ToString() + "," + txttransportemilid.Text.ToString();
            //    email = email.TrimEnd(',');
            //    //email = email.Trim(',');
            //}
            //else
            //{
            //    email = txtEmail.Text.ToString();
            //    email = email.TrimEnd(',');
            //}

            string fileName = "Trial_balnace" + ".pdf";
            filepath = "~/PAN/" + fileName;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw1 = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw1);
            PrintPanel.RenderControl(hw);
            string s1 = sw1.ToString().Replace("font-size: medium", "font-size: xx-small");
            StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));
            Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 250f);
            var writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath(filepath), FileMode.Create));
            Font tblfont = new Font();
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            ContentType contentType = new ContentType();
            contentType.MediaType = MediaTypeNames.Application.Pdf;
            contentType.Name = fileName;
            attachment = new Attachment(Server.MapPath(filepath), contentType);

            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "Trial Balance";
            msg.Attachments.Add(attachment);
            msg.IsBodyHtml = true;
            string lorry = string.Empty;
            string millshort = string.Empty;
            string party = string.Empty;

            msg.Subject = "Trial Balance Report";
            msg.IsBodyHtml = true;
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
            if (File.Exists(Server.MapPath(filepath)))
            {
                File.Delete(Server.MapPath(filepath));
            }
        }
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        finally
        {
            attachment.Dispose();
            if (File.Exists(Server.MapPath(filepath)))
            {
                File.Delete(Server.MapPath(filepath));
            }
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");


    }

}