using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;

public partial class Report_rptDayBook : System.Web.UI.Page
{
    string f = "../GSReports/DayBook_" + clsGV.user + ".htm";
    string f_Main = "../Report/rptDayBook";
    string email = string.Empty;
    DataSet dsCredit = new DataSet();
    DataSet dsDebit = new DataSet();
    DataTable dtCreditdetail = null;
    DataTable dtDebitdetail = null;
    double daycredittotal = 0.00;
    double daydebittotal = 0.00;
    double totalcredit = 0.00;
    double closingdebit = 0.00;
    double GrandCredit = 0.00;
    double GrandDebit = 0.00;
    double DayOpBal = 0.00;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string tblPrefix = string.Empty;
    string user = string.Empty;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        FromDt = Request.QueryString["FromDt"].ToString();
        ToDt = Request.QueryString["ToDt"].ToString();

        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Company_Name"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                dtCreditdetail = new DataTable();
                dtCreditdetail.Columns.Add("amt", typeof(double));
                dtCreditdetail.Columns.Add("narration", typeof(string));

                dtDebitdetail = new DataTable();
                dtDebitdetail.Columns.Add("amt", typeof(double));
                dtDebitdetail.Columns.Add("narration", typeof(string));
                this.bindDataMain();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }


    private void bindDataMain()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string dt1 = DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string dt2 = DateTime.Parse(ToDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string qry = "select sum(AMOUNT) as AMOUNT,DOC_DATE as DOC_DATE  from qrygledger where " +
                         " COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' group by DOC_DATE order by DOC_DATE asc";

            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add(new DataColumn("AMOUNT", typeof(double)));
                    dtNew.Columns.Add(new DataColumn("DOC_DATE", typeof(string)));
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow drNew = dtNew.NewRow();
                            string tranDate = dt.Rows[i]["DOC_DATE"].ToString();
                            tranDate = DateTime.Parse(tranDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                            //string opBalance = clsCommon.getString("select ISNULL(SUM(case DRCR when 'D' then AMOUNT ELSE -AMOUNT END),0) as AMOUNT from qryGledgernew where AC_CODE=1 
                            //AND COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [DOC_DATE]<'" + tranDate.Replace('.', '-') + "'");
                            string opBalance = clsCommon.getString("select ISNULL(SUM(case DRCR when 'D' then AMOUNT ELSE -AMOUNT END),0) as AMOUNT from qrygledger where AC_CODE=1 " +
                                          " AND COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<'" + tranDate.Replace('/', '-') + "'");
                            drNew[0] = Convert.ToDouble(opBalance);
                            drNew[1] = tranDate;
                            dtNew.Rows.Add(drNew);
                        }
                        dtlMain.DataSource = dtNew;
                        dtlMain.DataBind();
                    }
                }
            }
        }
        catch (Exception e1)
        {
            Response.Write(e1.Message);
        }
    }

    protected void dtlMain_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblOpBal = (Label)e.Item.FindControl("lblOpBal");

        DataList dtl_Credit = (DataList)e.Item.FindControl("dtl_Credit");
        Label lblDate = (Label)e.Item.FindControl("lblDate");

        DayOpBal = Convert.ToDouble(lblOpBal.Text);

        Label lblGrandDebit = (Label)e.Item.FindControl("lblGrandDebit");
        Label lblGrandCredit = (Label)e.Item.FindControl("lblGrandCredit");

        this.bindData(dtl_Credit, lblDate.Text);

        Label lblTotalDayCredit = (Label)e.Item.FindControl("lblTotalDayCredit");

        Label lblDayCreditTotal = (Label)e.Item.FindControl("lblDayCreditTotal");
        lblDayCreditTotal.Text = daycredittotal.ToString();
        totalcredit = DayOpBal + daycredittotal;
        lblTotalDayCredit.Text = totalcredit.ToString();

        DataList dtl_debit = (DataList)e.Item.FindControl("dtl_debit");

        this.bindData_1(dtl_debit, lblDate.Text);

        Label lblClosingDebit = (Label)e.Item.FindControl("lblClosingDebit");

        Label lblDayDebitTotal = (Label)e.Item.FindControl("lblDayDebitTotal");
        lblDayDebitTotal.Text = daydebittotal.ToString();

        closingdebit = totalcredit - daydebittotal;
        lblClosingDebit.Text = Math.Round(closingdebit, 2).ToString();

        lblGrandCredit.Text = totalcredit.ToString();
        GrandDebit = closingdebit + daydebittotal;
        lblGrandDebit.Text = GrandDebit.ToString();

        string dt = lblDate.Text;

        lblDate.Text = DateTime.Parse(lblDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        lblDate.Text = lblDate.Text + "   " + clsCommon.getString("select DATENAME(WEEKDAY, '" + dt + "')");
    }

    private void bindData(DataList dtl_Credit, string date)
    {
        try
        {
            double cTotal = 0.00;
            daycredittotal = 0.00;
            DataTable dtCredit = new DataTable();
            dtCredit.Columns.Add("amt", typeof(double));
            dtCredit.Columns.Add("acname", typeof(string));

            string accode = "";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //string qry = "select * from [dbo].[qryGledgernew] where [COMPANY_CODE]=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //    + " and [DRCR]='C' and [DOC_DATE]='" + date + "' and AC_CODE<>1 order by [DOC_DATE],[AC_CODE]";
            string qry = "select * from qrygledger where COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
              + " and DRCR='C' and DOC_DATE='" + date + "' and AC_CODE<>1 order by DOC_DATE,AC_CODE";
            ds = clsDAL.SimpleQuery(qry);

            dsCredit = new DataSet();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        accode = dt.Rows[0]["AC_CODE"].ToString();
                        DataTable dtUniqueAcCode = dt.DefaultView.ToTable(true, "AC_CODE");
                        for (int k = 0; k < dtUniqueAcCode.Rows.Count; k++)
                        {
                            string uniqueAcCode = dtUniqueAcCode.Rows[k]["AC_CODE"].ToString();
                            DataRow dr = dtCredit.NewRow();
                            dr["amt"] = dt.Compute("SUM(AMOUNT)", "AC_CODE=" + uniqueAcCode);
                            dr["acname"] = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + uniqueAcCode);
                            dtCredit.Rows.Add(dr);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string dtDate = DateTime.Parse(dt.Rows[i]["DOC_DATE"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy.MM.dd");

                                //if (dtDate == date)
                                if (dt.Rows[i]["AC_CODE"].ToString() == uniqueAcCode)
                                {
                                    DataRow dr2 = dtCreditdetail.NewRow();
                                    dr2["amt"] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                                    dr2["narration"] = dt.Rows[i]["NARRATION"].ToString();
                                    dtCreditdetail.Rows.Add(dr2);
                                    cTotal = Convert.ToDouble(dr2["amt"].ToString());
                                    daycredittotal = daycredittotal + cTotal;
                                }
                                //else
                                //{
                                //    accode = dt.Rows[i]["AC_CODE"].ToString();
                                //    DataTable dtTemp = new DataTable();
                                //    //      dtTemp = dtCreditdetail;

                                //    //     dtCreditdetail.TableName = "tbl" + i;

                                //    dsCredit.Tables.Add(dtCreditdetail);
                                //    //   dsCredit.Tables[0].TableName = "tbl" + 0;

                                //    //     dtCreditdetail.Rows.Clear();
                                //    dtCreditdetail = new DataTable();
                                //    dtCreditdetail.Columns.Add("amt", typeof(double));
                                //    dtCreditdetail.Columns.Add("narration", typeof(string));

                                //    DataRow dr = dtCredit.NewRow();
                                //    dr["amt"] = cTotal;
                                //    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                //    dtCredit.Rows.Add(dr);

                                //    daycredittotal = daycredittotal + cTotal;

                                //    cTotal = 0.00;
                                //    i = i - 1;
                                //}
                            }
                            //dsCredit = new DataSet();
                            dsCredit.Tables.Add(dtCreditdetail);
                            dtCreditdetail = new DataTable();
                            dtCreditdetail.Columns.Add("amt", typeof(double));
                            dtCreditdetail.Columns.Add("narration", typeof(string));
                        }
                        dtl_Credit.DataSource = dtCredit;
                        dtl_Credit.DataBind();
                    }
                }
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    private void bindData_1(DataList dtl_debit, string date)
    {
        try
        {
            double dTotal = 0.00;
            daydebittotal = 0.00;
            DataTable dtDebit = new DataTable();
            dtDebit.Columns.Add("amt", typeof(double));
            dtDebit.Columns.Add("acname", typeof(string));
            string accode = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select * from qrygledger where COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and DRCR='D' and DOC_DATE='" + date + "' and AC_CODE<>1 order by DOC_DATE,AC_CODE";
            ds = clsDAL.SimpleQuery(qry);
            dsDebit = new DataSet();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        accode = dt.Rows[0]["AC_CODE"].ToString();
                        DataTable dtUniqueAcCode = dt.DefaultView.ToTable(true, "AC_CODE");
                        for (int k = 0; k < dtUniqueAcCode.Rows.Count; k++)
                        {
                            string uniqueAcCode = dtUniqueAcCode.Rows[k]["AC_CODE"].ToString();
                            DataRow dr = dtDebit.NewRow();
                            dr["amt"] = dt.Compute("SUM(AMOUNT)", "AC_CODE=" + uniqueAcCode);
                            dr["acname"] = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + uniqueAcCode);
                            dtDebit.Rows.Add(dr);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string dtDate = DateTime.Parse(dt.Rows[i]["DOC_DATE"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy.MM.dd");
                                if (dt.Rows[i]["AC_CODE"].ToString() == uniqueAcCode)
                                {
                                    DataRow dr2 = dtDebitdetail.NewRow();
                                    dr2["amt"] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                                    dr2["narration"] = dt.Rows[i]["NARRATION"].ToString();
                                    dtDebitdetail.Rows.Add(dr2);
                                    dTotal = Convert.ToDouble(dr2["amt"].ToString());
                                    daydebittotal = daydebittotal + dTotal;
                                }
                            }

                            dsDebit.Tables.Add(dtDebitdetail);
                            dtDebitdetail = new DataTable();
                            dtDebitdetail.Columns.Add("amt", typeof(double));
                            dtDebitdetail.Columns.Add("narration", typeof(string));
                        }

                        //accode = dt.Rows[0]["AC_CODE"].ToString();
                        //DataTable dtUniqueAcCode = dt.DefaultView.ToTable(true, "AC_CODE");
                        //for (int k = 0; k < dtUniqueAcCode.Rows.Count; k++)
                        //{
                        //    string uniqueAcCode = dtUniqueAcCode.Rows[k]["AC_CODE"].ToString();
                        //    DataRow dr2 = dtDebit.NewRow();
                        //    dr2["amt"] = dt.Compute("SUM(AMOUNT)", "AC_CODE=" + uniqueAcCode);
                        //    dr2["acname"] = clsCommon.getString("select Ac_Name_E from NT_1_AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + uniqueAcCode);
                        //    dtDebit.Rows.Add(dr2);

                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        string dtDate = DateTime.Parse(dt.Rows[i]["DOC_DATE"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy.MM.dd");

                        //        if (dt.Rows[i]["AC_CODE"].ToString() == uniqueAcCode)
                        //        {
                        //            DataRow dr = dtDebitdetail.NewRow();
                        //            dr["amt"] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        //            dr["narration"] = dt.Rows[i]["NARRATION"].ToString();

                        //            dtDebitdetail.Rows.Add(dr);

                        //            dTotal = dTotal + Convert.ToDouble(dr["amt"].ToString());
                        //        }
                        //        //else
                        //        //{
                        //        //    accode = dt.Rows[i]["AC_CODE"].ToString();
                        //        //    DataTable dtTemp = new DataTable();
                        //        //    //      dtTemp = dtCreditdetail;

                        //        //    //     dtCreditdetail.TableName = "tbl" + i;

                        //        //    dsDebit.Tables.Add(dtDebitdetail);

                        //        //    //   dsCredit.Tables[0].TableName = "tbl" + 0;

                        //        //    //     dtCreditdetail.Rows.Clear();
                        //        //    dtDebitdetail = new DataTable();
                        //        //    dtDebitdetail.Columns.Add("amt", typeof(double));
                        //        //    dtDebitdetail.Columns.Add("narration", typeof(string));
                        //        //    DataRow dr = dtDebit.NewRow();
                        //        //    dr["amt"] = dTotal;
                        //        //    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                        //        //    dtDebit.Rows.Add(dr);
                        //        //    daydebittotal = daydebittotal + dTotal;
                        //        //    dTotal = 0.00;
                        //        //    i = i - 1;
                        //        //}
                        //    }
                        //    dsDebit.Tables.Add(dtDebitdetail);
                        //    dtDebitdetail = new DataTable();
                        //    dtDebitdetail.Columns.Add("amt", typeof(double));
                        //    dtDebitdetail.Columns.Add("narration", typeof(string));
                        //}

                        dtl_debit.DataSource = dtDebit;
                        dtl_debit.DataBind();
                    }
                }
            }
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
    }

    protected void dtl_Credit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        int itemindex = e.Item.ItemIndex;
        DataTable dt = new DataTable();
        // dt=dsCredit.Tables[0];
        // dt = dsCredit.Tables[1];
        DataList dtl_CreditDetail = (DataList)e.Item.FindControl("dtl_CreditDetail");

        this.bindCreditDetails(dtl_CreditDetail, itemindex);
    }

    private void bindCreditDetails(DataList dtl_CreditDetail, int itemindex)
    {
        try
        {
            dtl_CreditDetail.DataSource = dsCredit.Tables[itemindex];
            dtl_CreditDetail.DataBind();
        }
        catch
        {

        }
    }

    protected void dtl_debit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        int itemindex = e.Item.ItemIndex;
        DataTable dt = new DataTable();
        // dt=dsCredit.Tables[0];
        // dt = dsCredit.Tables[1];
        DataList dtl_DebitDetail = (DataList)e.Item.FindControl("dtl_DebitDetail");

        this.bindDebitDetails(dtl_DebitDetail, itemindex);
    }

    private void bindDebitDetails(DataList dtl_DebitDetail, int itemindex)
    {
        try
        {
            dtl_DebitDetail.DataSource = dsDebit.Tables[itemindex];
            dtl_DebitDetail.DataBind();
        }
        catch
        {

        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            email = txtEmail.Text.ToString();
            WebClient client = new WebClient();
            Stream data = client.OpenRead(HttpContext.Current.Request.Url.AbsoluteUri);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
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
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "Day Book";
            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            msg.Subject = "Day Book" + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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

}