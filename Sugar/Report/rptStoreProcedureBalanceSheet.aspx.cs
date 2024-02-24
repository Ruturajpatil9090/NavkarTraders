using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;

public partial class Report_rptStoreProcedureBalanceSheet : System.Web.UI.Page
{
    string f = "../GSReports/BalanceSheet_" + clsGV.user + ".htm";
    string f_Main = "../Report/rptBalanceSheet";
    string email = string.Empty;
    string qryCommon = string.Empty;
    string tblPrefix = string.Empty;
    string cityMasterTable = string.Empty;
    string tblHead = string.Empty;
    double netDebit = 0.00;
    double netCredit = 0.00;
    double netProfit = 0.00;
    double netLoss = 0.00;
    double totalDebit = 0.00;
    double totalCredit = 0.00;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    DataSet ds = null;
    // DataTable dt = null;
    // DataSet ds_Left = null;
    DataTable dt_Left = null;
    // DataSet ds_Right = null;
    DataTable dt_Right = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        qryCommon = "qryGledgernew";
        tblHead = tblPrefix + "BSGroupMaster";
        cityMasterTable = tblPrefix + "CityMaster";
        ViewState["VNO"] = Request.QueryString["VNO"];
        ViewState["mailID"] = Request.QueryString["mailID"];
        ViewState["pageBreak"] = Request.QueryString["pageBreak"];

        ViewState["BalDate"] = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                this.bindData();
                lblCompany.Text = Session["Company_Name"].ToString();
                lblUpto.Text = "Balance Sheet upto " + DateTime.Parse(ViewState["BalDate"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                lblCompanyAddr.Text = clsGV.CompanyAddress;
                lblCompanyMobile.Text = clsGV.CompanyPhone;
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }

    }

    private void bindData()
    {
        try
        {
            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
            string Cwhere = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            child1.SetAttributeValue("Company_Code", Company_Code);
            child1.SetAttributeValue("year", Year_Code);
            child1.SetAttributeValue("to_date", Cwhere);

            root.Add(child1);
            string XMLReport = root.ToString();
            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            string xmlfile = XMLReport;

            ds = clsDAL.xmlExecuteDMLQryReport1("Report_BalanceSheet", xmlfile);

            DataTable dtLeft = new DataTable();
            DataTable dtRight = new DataTable();

            dtLeft.Columns.Add("Group_Code", typeof(string));
            dtLeft.Columns.Add("groupname", typeof(string));
            dtLeft.Columns.Add("groupamount", typeof(double));
            dtLeft.Columns.Add("summary", typeof(string));

            dtRight.Columns.Add("Group_Code", typeof(string));
            dtRight.Columns.Add("groupname", typeof(string));
            dtRight.Columns.Add("groupamount", typeof(double));
            dtRight.Columns.Add("summary", typeof(string));

            #region[left side]

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt_Left = ds.Tables[0];
                    if (dt_Left.Rows.Count > 0)
                    {
                        Int32 grpcode = 0;
                        string groupname = "";
                        string groupsummary = "";
                        double grpbal = 0.00;
                        for (int i = 0; i < dt_Left.Rows.Count; i++)
                        {
                            if (grpcode == Convert.ToInt32(dt_Left.Rows[i]["Group_Code"].ToString()))
                            {
                                grpbal += Convert.ToDouble(dt_Left.Rows[i]["Balance"].ToString());
                            }
                            else
                            {
                                if (grpbal != 0)
                                {
                                    DataRow dr = dtLeft.NewRow();
                                    dr["Group_Code"] = grpcode;
                                    dr["groupname"] = groupname;
                                    dr["groupamount"] = Math.Abs(grpbal);
                                    dr["summary"] = groupsummary;
                                    dtLeft.Rows.Add(dr);

                                    netCredit += Math.Abs(grpbal);
                                    // grpbal = 0;
                                    grpbal = Convert.ToDouble(dt_Left.Rows[i]["Balance"].ToString());
                                }
                                else
                                {
                                    grpbal += Convert.ToDouble(dt_Left.Rows[i]["Balance"].ToString());
                                }
                                grpcode = Convert.ToInt32(dt_Left.Rows[i]["Group_Code"].ToString());
                                groupname = dt_Left.Rows[i]["groupname"].ToString();
                                groupsummary = dt_Left.Rows[i]["summary"].ToString();

                            }
                        }
                        DataRow dr1 = dtLeft.NewRow();
                        dr1["Group_Code"] = grpcode;
                        dr1["groupname"] = groupname;
                        dr1["groupamount"] = Math.Abs(grpbal);
                        dr1["summary"] = groupsummary;
                        dtLeft.Rows.Add(dr1);
                        netCredit += Math.Abs(grpbal);

                        //'-----------------------------

                        dtl_Left.DataSource = dtLeft;
                        dtl_Left.DataBind();

                        //------right 
            #endregion

                        #region[right side]
                        //fill dtl_Left 

                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt_Right = ds.Tables[1];
                                if (dt_Right.Rows.Count > 0)
                                {
                                    grpcode = 0;
                                    groupname = "";
                                    groupsummary = "";
                                    grpbal = 0.00;
                                    for (int i = 0; i < dt_Right.Rows.Count; i++)
                                    {
                                        if (grpcode == Convert.ToInt32(dt_Right.Rows[i]["Group_Code"].ToString()))
                                        {
                                            grpbal += Convert.ToDouble(dt_Right.Rows[i]["Balance"].ToString());
                                        }
                                        else
                                        {
                                            if (grpbal != 0)
                                            {
                                                DataRow dr = dtRight.NewRow();
                                                dr["Group_Code"] = grpcode;
                                                dr["groupname"] = groupname;
                                                dr["groupamount"] = Math.Abs(grpbal);
                                                dr["summary"] = groupsummary;
                                                dtRight.Rows.Add(dr);

                                                netDebit += Math.Abs(grpbal);
                                                // grpbal = 0;
                                                grpbal = Convert.ToDouble(dt_Right.Rows[i]["Balance"].ToString());
                                            }
                                            else
                                            {
                                                grpbal += Convert.ToDouble(dt_Right.Rows[i]["Balance"].ToString());
                                            }
                                            grpcode = Convert.ToInt32(dt_Right.Rows[i]["Group_Code"].ToString());
                                            groupname = dt_Right.Rows[i]["groupname"].ToString();
                                            groupsummary = dt_Right.Rows[i]["summary"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                        DataRow dr2 = dtRight.NewRow();
                        dr2["Group_Code"] = grpcode;
                        dr2["groupname"] = groupname;
                        dr2["groupamount"] = Math.Abs(grpbal);
                        dr2["summary"] = groupsummary;
                        dtRight.Rows.Add(dr2);
                        netDebit += Math.Abs(grpbal);

                        //'-----------------------------

                        dtl_Right.DataSource = dtRight;
                        dtl_Right.DataBind();

                        //------right 
                        #endregion

                        lblNetDebit.Text = netDebit.ToString();
                        lblNetCredit.Text = netCredit.ToString();

                        if (netDebit - netCredit > 0)
                        {
                            netProfit = (netDebit - netCredit);
                            lblnetProfit.Text = Math.Round(netProfit, 2).ToString();
                        }
                        else
                        {
                            netLoss = (netCredit - netDebit);
                            lblnetLoss.Text = Math.Round(netLoss, 2).ToString();
                        }

                        if (netProfit == 0)
                        {
                            lblnetprofithead.Text = "";
                            lblnetProfit.Text = "";
                        }
                        if (netLoss == 0)
                        {
                            lblnetLoss.Text = "";
                            lblnetlosshead.Text = "";

                        }

                        lbltotalCredit.Text = Math.Round((netCredit + netProfit), 2).ToString();
                        lbltotalDebit.Text = Math.Round((netLoss + netDebit), 2).ToString();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void dtLeft_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtLeftInner = (DataList)e.Item.FindControl("dtLeftInner");

            Label lblsummaryL = (Label)e.Item.FindControl("lblsummaryL");
            Label lblGroupCodeL = (Label)e.Item.FindControl("lblGroupCodeL");

            DataTable dtT = new DataTable();
            dtT.Columns.Add("AC_CODE", typeof(string));
            dtT.Columns.Add("Ac_Name_E", typeof(string));
            dtT.Columns.Add("acamount", typeof(string));

            string sortOrder = "AC_CODE,Ac_Name_E asc";

            if (lblsummaryL.Text == "Y")
            {

            }
            else
            {
                DataRow[] results = dt_Left.Select("Group_Code=" + lblGroupCodeL.Text + " and Balance <> 0 ", sortOrder);

                foreach (DataRow row in results)
                {
                    double bal = Convert.ToDouble(row[6]);

                    if (bal < 0)         //  >0  right side asset
                    {
                        row["acamount"] = Math.Abs(bal).ToString("0.00");
                        dtT.ImportRow(row);
                    }

                }
            }

            dtLeftInner.DataSource = dtT;
            dtLeftInner.DataBind();

        }
        catch
        {
        }
    }

    protected void dtl_Right_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtRightInner = (DataList)e.Item.FindControl("dtRightInner");

            Label lblsummaryR = (Label)e.Item.FindControl("lblsummaryR");
            Label lblGroupCodeR = (Label)e.Item.FindControl("lblGroupCodeR");

            DataTable dtT = new DataTable();
            dtT.Columns.Add("AC_CODE", typeof(string));
            dtT.Columns.Add("Ac_Name_E", typeof(string));
            dtT.Columns.Add("acamount", typeof(string));
            string sortOrder = "AC_CODE,Ac_Name_E asc";


            if (lblsummaryR.Text == "Y")
            {

            }
            else
            {

                DataRow[] results = dt_Right.Select("Group_Code=" + lblGroupCodeR.Text + " and Balance <> 0 ", sortOrder);

                foreach (DataRow row in results)
                {
                    double bal = Convert.ToDouble(row[6]);

                    if (bal > 0)         //  >0  right side asset
                    {
                        row["acamount"] = Math.Abs(bal).ToString("0.00");
                        dtT.ImportRow(row);
                    }

                }
            }
            dtRightInner.DataSource = dtT;
            dtRightInner.DataBind();

        }
        catch
        {
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        email = txtEmail.Text.ToString();
        if (email != string.Empty)
        {
            try
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
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(email);
                msg.Body = "Balance Sheet";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                msg.Subject = "Balance Sheet ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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

            catch (Exception e1)
            {
                Response.Write("mail err:" + e1);
                //Response.Write("<script>alert('Error sending Mail');</script>");
                return;
            }
            Response.Write("<script>alert('Mail sent successfully');</script>");
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }

    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;

            Label lblaccode = (Label)item.FindControl("lblGroupCodeL");
            // TextBox txt = (TextBox)item.FindControl("tb_cmt");
            //DataList dtLeftInner = (DataList)FindControl("dtLeftInner");
            //Label lblaccode = (Label)dtLeftInner.FindControl("lblaccode");
            //string sal = ((Label)FindControl("lblaccode")).Text;
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;

            string fromDt = DateTime.Parse(fromdt1, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string dt = DateTime.Parse(todt1, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string accode = lblaccode.Text;
            string group_type = "B";
            string ac_type = "";
            // Session["VOUC_NO"] = no;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "rptTBS", "javascript:TBS('" + dt + "','" + accode
               + "','" + fromDt + "','" + dt + "','" + ac_type + "','" + group_type + "')", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void lnkDO1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;

            Label lblaccode = (Label)item.FindControl("lblGroupCodeR");
            // TextBox txt = (TextBox)item.FindControl("tb_cmt");
            //DataList dtLeftInner = (DataList)FindControl("dtLeftInner");
            //Label lblaccode = (Label)dtLeftInner.FindControl("lblaccode");
            //string sal = ((Label)FindControl("lblaccode")).Text;
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;

            string fromDt = DateTime.Parse(fromdt1, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string dt = DateTime.Parse(todt1, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string accode = lblaccode.Text;
            string group_type = "B";
            string ac_type = "";
            // Session["VOUC_NO"] = no;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "rptTBS", "javascript:TBS('" + dt + "','" + accode
               + "','" + fromDt + "','" + dt + "','" + ac_type + "','" + group_type + "')", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
}