using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Net.Mail;

public partial class Report_rptBankBook : System.Web.UI.Page
{
    #region data section
    string f = "../GSReports/Ledger_.htm";
    string f_Main = "../Report/rptLedger";
    double netdebit = 0.00;
    double netcredit = 0.00;
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
    string email = string.Empty;
    static WebControl objAsp = null;
    string prefix = string.Empty;
    string accode = string.Empty;
    string fromdt = string.Empty;
    string todt = string.Empty;
    string qry = string.Empty;
    double netDebitTotal = 0.00;
    double netCreditTotal = 0.00;
    double netBalanceTotal = 0.00;
    string netDrCrTotal = "";
    double netBalance = 0.00;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        accode = Request.QueryString["accode"];
        fromdt = Request.QueryString["fromdt"];
        todt = Request.QueryString["todt"];
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
            this.bindData();
        }
    }

    private void bindData()
    {
        try
        {
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            string Bank_Name = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string fromDate = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string toDate = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            lblBankAndDate.Text = "<b>" + Bank_Name + "</b> <br/> From: <b>" + fromDate + "</b>  To:  <b>" + toDate + "</b>";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  group by AC_CODE ";
            ds = clsDAL.SimpleQuery(qry);
            double opBal = 0.0;

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt.Columns.Add(new DataColumn("Date", typeof(string)));
                    dt.Columns.Add(new DataColumn("OpDebit", typeof(string)));
                    dt.Columns.Add(new DataColumn("OpCredit", typeof(string)));
                    dt.Columns.Add(new DataColumn("OpBalance", typeof(string)));
                    dt.Columns.Add(new DataColumn("OpDrCr", typeof(string)));

                    DataRow dr = dt.NewRow();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        opBal = Convert.ToDouble(ds.Tables[0].Rows[0][1].ToString());
                        netBalance = opBal;
                    }
                    ViewState["opBal"] = opBal;
                    dr["Date"] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (opBal > 0)
                    {
                        dr["OpCredit"] = 0.00;
                        dr["OpDrCr"] = "Dr";
                        dr["OpDebit"] = Math.Round(opBal, 2);
                        dr["OpBalance"] = Math.Round(opBal, 2);
                        netdebit += opBal;
                    }
                    else
                    {
                        dr["OpCredit"] = Math.Round(-opBal, 2);
                        dr["OpDrCr"] = "Cr";
                        dr["OpDebit"] = 0.00;
                        dr["OpBalance"] = dr["OpCredit"];
                        netcredit += -opBal;
                    }
                    dt.Rows.Add(dr);
                    if (dt.Rows.Count > 0)
                    {
                        dtlist.DataSource = dt;
                        dtlist.DataBind();
                    }
                    else
                    {
                        dtlist.DataSource = null;
                        dtlist.DataBind();
                    }
                }
                else
                {
                    dtlist.DataSource = null;
                    dtlist.DataBind();

                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlist_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtlListdate = (DataList)e.Item.FindControl("dtlListdate");
            Label lblNetDebitTotal = (Label)e.Item.FindControl("lblNetDebitTotal");
            Label lblNetCreditTotal = (Label)e.Item.FindControl("lblNetCreditTotal");
            Label lblNetBalanceTotal = (Label)e.Item.FindControl("lblNetBalanceTotal");
            Label lblNetDrCr = (Label)e.Item.FindControl("lblNetDrCr");

            qry = "Select Distinct(Convert(varchar(10),DOC_DATE,103)) as Doc_Date,DOC_DATE as DO_Date from " + tblGLEDGER + " where DRCR_HEAD=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' " +
                " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  order by DO_Date asc";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dtlListdate.DataSource = dt;
                    dtlListdate.DataBind();

                    lblNetDebitTotal.Text = Convert.ToString(Math.Abs(Math.Round(netDebitTotal, 2)));
                    lblNetCreditTotal.Text = Convert.ToString(Math.Abs(Math.Round(netCreditTotal, 2)));
                    lblNetBalanceTotal.Text = Convert.ToString(Math.Abs(Math.Round(netBalanceTotal, 2)));
                    lblNetDrCr.Text = netDrCrTotal;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void dtlListdate_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");


            Label lblDebitTotal = (Label)e.Item.FindControl("lblDebitTotal");
            Label lblCreditTotal = (Label)e.Item.FindControl("lblCreditTotal");
            Label lblBalanceTotal = (Label)e.Item.FindControl("lblBalanceTotal");
            Label lblDrCr = (Label)e.Item.FindControl("lblDrCr");

            Label lblDocDate = (Label)e.Item.FindControl("lblDocDate");
            string date = DateTime.Parse(lblDocDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            qry = "select AC_CODE,TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from " + tblGLEDGER +
                  " where DRCR_HEAD=" + accode + " and DOC_DATE='" + date + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();

            double opBal = Convert.ToDouble(ViewState["opBal"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];

                DataTable dtT = new DataTable();
                dtT.Columns.Add("TranType", typeof(string));
                dtT.Columns.Add("DocNo", typeof(Int32));
                dtT.Columns.Add("Date", typeof(string));
                dtT.Columns.Add("Narration", typeof(string));
                dtT.Columns.Add("Debit", typeof(double));
                dtT.Columns.Add("Credit", typeof(double));
                dtT.Columns.Add("Balance", typeof(double));
                dtT.Columns.Add("DrCr", typeof(string));

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dtT.NewRow();
                        dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                        dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                        if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                        {
                            string s = dt.Rows[i]["DOC_DATE"].ToString();

                            dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        }

                        Int32 Ac_Code = dt.Rows[i]["AC_CODE"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["AC_CODE"].ToString()) : 0;
                        string Ac_Name = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + Ac_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string SORT_TYPE = dt.Rows[i]["SORT_TYPE"].ToString();
                        string SORT_NO = dt.Rows[i]["SORT_NO"].ToString();
                        dr[3] = Server.HtmlDecode("<b>" + Ac_Name + "</b> <br/>" + dt.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");

                        if (dt.Rows[i]["DRCR"].ToString() == "D")
                        {
                            opBal = opBal - Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                            netBalance -= Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                            dr[5] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                            dr[4] = 0.00;
                            netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        }
                        else
                        {
                            netBalance += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                            opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                            netcredit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                            dr[5] = 0.00;
                            dr[4] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        }

                        if (netBalance > 0)
                        {
                            dr[6] = Math.Abs(Convert.ToDouble(Math.Round(netBalance, 2)));
                            dr[7] = "Dr";
                        }
                        else
                        {
                            dr[6] = Math.Abs(0 - Math.Abs(Math.Round(netBalance, 2)));
                            dr[7] = "Cr";
                        }
                        dtT.Rows.Add(dr);
                    }
                    if (dtT.Rows.Count > 0)
                    {
                        double DebitTotal = Convert.ToDouble(dtT.Compute("SUM(Debit)", string.Empty));
                        double CreditTotal = Convert.ToDouble(dtT.Compute("SUM(Credit)", string.Empty));
                        lblDebitTotal.Text = DebitTotal.ToString();
                        lblCreditTotal.Text = CreditTotal.ToString();

                        double balanceTotal = Math.Abs(Math.Round(netBalance, 2));

                        netDebitTotal += DebitTotal;
                        netCreditTotal += CreditTotal;

                        netBalanceTotal = netBalance;

                        if (netBalanceTotal > 0)
                        {
                            netDrCrTotal = "Dr";
                        }
                        else
                        {
                            netDrCrTotal = "Cr";
                        }

                        lblBalanceTotal.Text = Convert.ToString(balanceTotal);
                        if (balanceTotal > 0)
                        {
                            lblDrCr.Text = "Dr";
                        }
                        else
                        {
                            lblDrCr.Text = "Cr";
                        }
                        dtlDetails.DataSource = dtT;
                        dtlDetails.DataBind();
                    }
                }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            email = txtEmail.Text.ToString();
            CreateHtml();

            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();

            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;

            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            //mail.To.Add("toaddress2@gmail.com");
            msg.Body = "Bank Book Report";

            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            msg.Subject = "Bank Book Report  " + DateTime.Now.ToString("dd/MM/yyyy");

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
}