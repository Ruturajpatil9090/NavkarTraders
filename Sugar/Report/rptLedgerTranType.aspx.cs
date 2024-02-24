using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.Web;
using System.Net.Mime;

public partial class Report_rptLedgerTranType : System.Web.UI.Page
{
    #region data section
    string f = "../GSReports/Ledger_.htm";
    string f_Main = "../Report/rptLedger";
    double netdebit = 0.00; double netcredit = 0.00;

    string tblPrefix = string.Empty;
    string tblGLEDGER = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string TranTyp = string.Empty;
    int defaultAccountCode = 0;
    int tempcounter = 0;
    string email = string.Empty;
    static WebControl objAsp = null;
    string prefix = string.Empty;
    string accode = string.Empty;
    string fromdt = string.Empty;
    string todt = string.Empty;
    string DrCr = string.Empty;
    string trantype = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        accode = Request.QueryString["accode"];
        fromdt = Request.QueryString["fromdt"];
        todt = Request.QueryString["todt"];
        DrCr = Request.QueryString["DrCr"];
        trantype = Request.QueryString["Tran_type"];
        if (Session["tblPrefix"] != null)
        {
            tblPrefix = Session["tblPrefix"].ToString();
        }
        else
        {
            prefix = clsCommon.getString("Select tblPrefix from tblPrefix");
            tblPrefix = prefix.ToString();
        }
        tblGLEDGER = tblPrefix + "GLEDGER";
        tblDetails = tblPrefix + "VoucherDetails";
        AccountMasterTable = tblPrefix + "AccountMaster";
        cityMasterTable = tblPrefix + "CityMaster";
        email = txtEmail.Text;
        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Company_Name"].ToString();
            if (DrCr != "DrCr")
            {
                this.OnlyCrORDr();
            }
            else
            {
                this.bindData();
            }
        }
    }

    private void OnlyCrORDr()
    {
        try
        {
            string mail = "";
            // pnlPopup.Style["display"] = "none";
            if (accode != string.Empty)
            {
                string ccmail = clsCommon.getString("Select Email_Id_cc from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                mail = ccmail + "," + clsCommon.getString("Select Email_Id from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            }
            if (mail != string.Empty)
            {
                txtEmail.Text = mail;
            }
            else
            {
                email = txtEmail.Text.ToString();
            }
            lblParty.Text = " (" + accode + ")&nbsp;" + clsCommon.getString("select Ac_name_e from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string filter = string.Empty;
            if (DrCr == "Dr")
            {
                filter = "D";
            }
            else
            {
                filter = "C";
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '"
                + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and drcr='" + filter + "' group by AC_CODE ";
            ds = clsDAL.SimpleQuery(qry);
            double opBal = 0.0;

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ////opBal = Convert.ToDouble(dt.Rows[0][1].ToString());
                        opBal = 0.0;
                    }
                }
            }
            qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from "
                + tblGLEDGER +
                    " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and drcr='" + filter + "' order by TRAN_TYPE,DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";
            ds = clsDAL.SimpleQuery(qry);

            DataTable dtT = new DataTable();
            //dtT = null;
            dtT.Columns.Add("TranType", typeof(string));
            dtT.Columns.Add("DocNo", typeof(Int32));
            dtT.Columns.Add("Date", typeof(string));
            dtT.Columns.Add("Narration", typeof(string));
            dtT.Columns.Add("Debit", typeof(double));
            dtT.Columns.Add("Credit", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("DrCr", typeof(string));

            //if (dt.Rows.Count > 0)
            //{
            dt = ds.Tables[0];

            DataRow dr = dtT.NewRow();
            //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
            dr[0] = "OP";
            dr[1] = 0.00;
            dr[2] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            dr[3] = "Opening Balance ";

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
            dtT.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtT.NewRow();

                    dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                    dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                    if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                    {
                        string s = dt.Rows[i]["DOC_DATE"].ToString();
                        dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    }
                    string SORT_TYPE = dt.Rows[i]["SORT_TYPE"].ToString();
                    string SORT_NO = dt.Rows[i]["SORT_NO"].ToString();
                    dr[3] = Server.HtmlDecode(dt.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");

                    if (dt.Rows[i]["DRCR"].ToString() == "D")
                    {
                        opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                        dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        dr[5] = 0.00;
                        netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }
                    else
                    {
                        opBal = opBal - Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        netcredit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                        dr[4] = 0.00;
                        dr[5] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }
                    if (DrCr == "Dr")
                    {
                        if (opBal > 0)
                        {
                            dr[6] = Math.Round(Convert.ToDouble(opBal), 2);
                            dr[7] = "Dr";
                            dtT.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        if (opBal < 0)
                        {
                            dr[6] = 0 - Math.Round(opBal, 2);
                            dr[7] = "Cr";
                            dtT.Rows.Add(dr);
                        }
                    }

                }
            }
            grdDetail.DataSource = dtT;
            grdDetail.DataBind();
            grdDetail.FooterRow.Cells[3].Text = "Total";
            if (DrCr == "Dr")
            {
                grdDetail.FooterRow.Cells[4].Text = netdebit.ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[5].Text = netcredit.ToString();
            }
            if (netdebit - netcredit != 0)
            {
                double balance = netdebit - netcredit;
                if (balance > 0)
                {
                    grdDetail.FooterRow.Cells[7].Text = "Dr";
                }
                if (balance < 0)
                {
                    grdDetail.FooterRow.Cells[7].Text = "Cr";
                }
                grdDetail.FooterRow.Cells[6].Text = Math.Abs(balance).ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[6].Text = "Nil";
            }
            grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    private void bindData()
    {
        try
        {
            string mail = "";
            // pnlPopup.Style["display"] = "none";
            if (accode != string.Empty)
            {
                string ccmail = clsCommon.getString("Select Email_Id_cc from " + AccountMasterTable + "  where  Ac_Code=" + accode
                    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                mail = ccmail + "," + clsCommon.getString("Select Email_Id from " + AccountMasterTable + "  where  Ac_Code=" + accode
                    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            }
            if (mail != string.Empty)
            {
                txtEmail.Text = mail;
            }
            else
            {
                email = txtEmail.Text.ToString();
            }
            lblParty.Text = " (" + accode + ")&nbsp;" + clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER
                + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
            ds = clsDAL.SimpleQuery(qry);
            double opBal = 0.0;
            string nar = string.Empty;
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ////opBal = Convert.ToDouble(dt.Rows[0][1].ToString());
                        opBal = 0.0;
                        //nar = dt.Rows[0][2].ToString();
                        //if (!string.IsNullOrEmpty(nar.Trim().ToString()))
                        //{
                        //    nar = nar.Remove(0, 15);
                        //}
                    }
                }
            }
            //qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from " 
            //    + tblGLEDGER +
            //        " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //        + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";

            if (trantype == "All")
            {

                qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from "
                + tblGLEDGER +
                    " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " order by TRAN_TYPE asc,DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";
            }
            else
            {
                qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from "
                 + tblGLEDGER +
                     " where TRAN_TYPE='" + trantype + "' and AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";
            }
            ds = clsDAL.SimpleQuery(qry);


            DataTable dtT = new DataTable();
            //dtT = null;
            dtT.Columns.Add("TranType", typeof(string));
            dtT.Columns.Add("DocNo", typeof(Int32));
            dtT.Columns.Add("Date", typeof(string));
            dtT.Columns.Add("Narration", typeof(string));
            dtT.Columns.Add("Debit", typeof(double));
            dtT.Columns.Add("Credit", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("DrCr", typeof(string));

            //if (dt.Rows.Count > 0)
            //{
            dt = ds.Tables[0];

            DataRow dr = dtT.NewRow();
            //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
            dr[0] = "OP";
            dr[1] = 0.00;
            dr[2] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
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
            dtT.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtT.NewRow();

                    dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                    dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                    if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                    {
                        string s = dt.Rows[i]["DOC_DATE"].ToString();
                        dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    }
                    string SORT_TYPE = dt.Rows[i]["SORT_TYPE"].ToString();
                    string SORT_NO = dt.Rows[i]["SORT_NO"].ToString();
                    dr[3] = Server.HtmlDecode(dt.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");

                    if (dt.Rows[i]["DRCR"].ToString() == "D")
                    {
                        opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                        dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        dr[5] = 0.00;
                        netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }
                    else
                    {
                        opBal = opBal - Math.Abs(Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        netcredit += Math.Abs(Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));

                        dr[4] = 0.00;
                        dr[5] = Math.Abs(Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
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
                    dtT.Rows.Add(dr);
                }
            }
            grdDetail.DataSource = dtT;
            grdDetail.DataBind();
            grdDetail.FooterRow.Cells[3].Text = "Total";
            grdDetail.FooterRow.Cells[4].Text = netdebit.ToString();
            grdDetail.FooterRow.Cells[5].Text = netcredit.ToString();
            if (netdebit - netcredit != 0)
            {
                double balance = netdebit - netcredit;
                if (balance > 0)
                {
                    grdDetail.FooterRow.Cells[7].Text = "Dr";
                }
                if (balance < 0)
                {
                    grdDetail.FooterRow.Cells[7].Text = "Cr";
                }
                grdDetail.FooterRow.Cells[6].Text = Math.Abs(Math.Round(balance, 2)).ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[6].Text = "Nil";
            }
            grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            //if (tempcounter == 10)
            //{
            //    e.Row.Attributes.Add("style", "page-break-after: always;");
            //    tempcounter = 0;
            //}
            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "";
            }
            if (e.Row.Cells[5].Text == "0")
            {
                e.Row.Cells[5].Text = "";
            }
            if (e.Row.Cells[6].Text == "0")
            {
                e.Row.Cells[6].Text = "Nil";
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string mail = txtEmail.Text;
            if (txtEmail.Text != string.Empty)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter tw = new HtmlTextWriter(sw);
                    PrintPanel.RenderControl(tw);
                    string s = sw.ToString();
                    s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");
                    byte[] array = Encoding.UTF8.GetBytes(s);
                    ms.Write(array, 0, array.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    ContentType contentType = new ContentType();
                    contentType.MediaType = MediaTypeNames.Application.Octet;
                    contentType.Name = "Ledger.htm";
                    Attachment attachment = new Attachment(ms, contentType);

                    string mailFrom = Session["EmailId"].ToString();
                    string smtpPort = "587";
                    string emailPassword = Session["EmailPassword"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                    SmtpServer.Host = clsGV.Email_Address;
                    msg.From = new MailAddress(mailFrom);
                    msg.To.Add(mail);
                    msg.Body = "Ledger";
                    msg.Attachments.Add(attachment);
                    msg.IsBodyHtml = true;
                    msg.Subject = "Ledger Report";
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
                }
            }
        }
        catch (Exception e1)
        {
            Response.Write("mail err:" + e1);
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");
    }

    private void CreateHtml()
    {
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
    }

    

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
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
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btpGrdprint_Click(object sender, EventArgs e)
    {
        grdDetail.AllowPaging = false;
        grdDetail.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grdDetail.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        grdDetail.AllowPaging = true;
        grdDetail.DataBind();
    }

    protected void lnkGo_Click(object sender, EventArgs e)
    {
        LinkButton lnkGo = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkGo.NamingContainer;
        int rowIndex = row.RowIndex;
        string TranType = grdDetail.Rows[rowIndex].Cells[0].Text;

        string No = lnkGo.Text;
        // DateTime dt = DateTime.Parse(grdDetail.Rows[rowIndex].Cells[2].Text);
        // DateTime dt1 = DateTime.Parse("01/7/2017");
        string sessionName = GetSessionName(TranType);
        string reciptPayment = string.Empty;

        string dtt = "01/07/2017";
        string dt11 = grdDetail.Rows[rowIndex].Cells[2].Text.Trim();
        DateTime dt = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt1 = DateTime.ParseExact(dt11, "dd/MM/yyyy", CultureInfo.InvariantCulture);


        if (No != "0")
        {
            if (TranType.Contains("BP") || TranType.Contains("CR") || TranType.Contains("CP") || TranType.Contains("BR"))
            {
                Session["RP_NO"] = No;
                Session["RP_TYPE"] = sessionName;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ReciptPayment", "javascript:RP();", true);
            }
            else if (TranType.Contains("JS"))
            {
                Session["RP_NO"] = No;
                Session["RP_TYPE"] = sessionName;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JaggarySale", "javascript:JS();", true);
            }
            else if (TranType.Contains("AW"))
            {
                Session["RP_NO"] = No;
                Session["RP_TYPE"] = sessionName;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JaggaryAwak", "javascript:AW();", true);
            }
            else if (TranType.Contains("CQ"))
            {
                Session["RP_NO"] = No;
                Session["RP_TYPE"] = sessionName;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cq", "javascript:Cheq();", true);
            }
            else
            {

                if (dt.Date < dt1.Date)
                {
                    Session[sessionName] = No;

                    if (sessionName == "SB_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:SBOld();", true);
                    }
                    else if (sessionName == "LV_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:LVOld();", true);

                    }
                    else if (sessionName == "DO_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:DOOld();", true);
                    }
                    else if (sessionName == "PURC_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:PSOld();", true);
                    }

                    else if (sessionName == "RS_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:RSOld();", true);

                    }
                    else
                    {
                        Session[sessionName] = No;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:" + TranType + "();", true);
                    }


                }
                else
                {
                    Session[sessionName] = No;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:" + TranType + "();", true);
                }
            }
        }
        lnkGo.Focus();
    }

    private static string GetSessionName(string TranType)
    {
        string SessionName = string.Empty;
        switch (TranType)
        {
            case "DO":
                SessionName = "DO_NO";
                break;
            case "LV":
                SessionName = "LV_NO";
                break;
            case "OV":
                SessionName = "VOUC_NO";
                break;
            case "SB":

                SessionName = "SB_NO";
                break;
            case "PS":
                SessionName = "PURC_NO";
                break;
            case "RS":
                SessionName = "RS_NO";
                break;
            case "PR":
                SessionName = "PR_NO";
                break;
            case "UT":
                SessionName = "UT_NO";
                break;
            case "JV":
                SessionName = "JV_NO";
                break;
            case "OP":
                SessionName = "UT_NO";
                break;
            case "CR":
                SessionName = "CR";
                break;
            case "CP":
                SessionName = "CP";
                break;
            case "BR":
                SessionName = "BR";
                break;
            case "BP":
                SessionName = "BP";
                break;

            default:
                SessionName = "";
                break;
        }
        return SessionName;
    }


    protected void btnCreateb2b_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = "select LTRIM(RTRIM(PartyGST)) as [GSTIN/UIN of Recipient],doc_no as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
                         "Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,PartyStateCode),2) +'-'+ LTRIM(RTRIM(PartyState))) as [Place Of Supply],'N' as [Reverse Charge],'Regular' as [Invoice Type]," +
                         "'' as [E-Commerce GSTIN],5 as Rate,TaxableAmount as [Taxable Value],'' as [Cess Amount] from NT_1_qrySugarSaleForGSTReturn where doc_date>='2017-07-01' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
                         " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    grdCsv.DataSource = dt;
                    grdCsv.DataBind();
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = "LEN([GSTIN/UIN of Recipient]) <> 15 ";

                    DataView dvStateCode = new DataView(dt);
                    dvStateCode.RowFilter = "LEN(TRIM([Place Of Supply]))<2";

                    string strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    //Regex reg = new Regex(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                    //string gstno = dt.Rows[0][0].ToString();
                    //bool istrue = reg.IsMatch(gstno);
                    //Exporting to CSV.

                    string fileName = "~/PAN/b2b.csv";
                    File.WriteAllText(Server.MapPath(fileName), strForCSV);

                    //string path = Server.MapPath(fileName);
                    //System.IO.FileInfo file = new System.IO.FileInfo(path);
                    //string Outgoingfile = "b2b.csv";
                    //Response.Clear();
                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "application/text";
                    //Response.WriteFile(file.FullName);
                    //Response.Flush();
                    //Response.Close();


                    //Response.ClearContent();
                    //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.csv"));
                    //Response.ContentType = "application/text";
                    //grdCsv.AllowPaging = false;
                    //grdCsv.DataBind();
                    //StringBuilder strbldr = new StringBuilder();
                    //for (int i = 0; i < grdCsv.Columns.Count; i++)
                    //{
                    //    //separting header columns text with comma operator
                    //    strbldr.Append(grdCsv.Columns[i].HeaderText + ',');
                    //}
                    ////appending new line for gridview header row
                    //strbldr.Append("\n");
                    //for (int j = 0; j < grdCsv.Rows.Count; j++)
                    //{
                    //    for (int k = 0; k < grdCsv.Columns.Count; k++)
                    //    {
                    //        //separating gridview columns with comma
                    //        strbldr.Append(grdCsv.Rows[j].Cells[k].Text + ',');
                    //    }
                    //    //appending new line for gridview rows
                    //    strbldr.Append("\n");
                    //}
                    //Response.Write(strbldr.ToString());
                    //Response.End();


                    // set the resulting file attachment name to the name of the report...
                    //string fileName = "test";

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    // Get the header row text form the sortable columns
                    LinkButton headerLink = new LinkButton();
                    string headerText = string.Empty;

                    for (int k = 0; k < grdCsv.HeaderRow.Cells.Count; k++)
                    {
                        //add separator
                        //headerLink = grdCsv.HeaderRow.Cells[k].Text.Controls[0] as LinkButton;
                        headerText = grdCsv.HeaderRow.Cells[k].Text;
                        sb.Append(headerText + ",");
                    }
                    //append new line
                    sb.Append("\r\n");
                    for (int i = 0; i < grdCsv.Rows.Count; i++)
                    {
                        for (int k = 0; k < grdCsv.HeaderRow.Cells.Count; k++)
                        {
                            //add separator and strip "," values from returned content...

                            sb.Append(grdCsv.Rows[i].Cells[k].Text.Replace(",", "") + ",");
                        }
                        //append new line
                        sb.Append("\r\n");
                    }
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}