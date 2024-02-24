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

public partial class Sugar_Report_rptTransactionTrialBalance : System.Web.UI.Page
{
    string f = "../GSReports/TrialBalance_.htm";
    string f_Main = "../Report/rptTrialBalance";
    string email = string.Empty;
    double netdebit = 0.00; double netcredit = 0.00;
    string From_Date = string.Empty;
    string To_Date = string.Empty;
    double netdifference = 0.0;
    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Cwhere"] = Request.QueryString["Cwhere"];
        ViewState["Doc_Date"] = Request.QueryString["Doc_Date"];
        From_Date = Request.QueryString["FromDt"].ToString();
        To_Date = Request.QueryString["ToDt"].ToString();


        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                lblCompanyName.Text = Session["Company_Name"].ToString();
                Bindata();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void Bindata()
    {
        try
        {

            string accqry = "select Ac_Code,Group_Code,group_Name_E as BSGroupName,group_Type,accoid,Company_Code from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + "";
            DataSet dsacc = new DataSet();
            dsacc = clsDAL.SimpleQuery(accqry);
            DataTable dtAc = dsacc.Tables[0];

            DataSet ds = new DataSet();
            // string qry = "select sum(case drcr when 'D' then amount else -amount end) as balance,AC_CODE,ac,COMPANY_CODE,'' as Group_Code,'' as BSGroupName,'' as group_Type from nt_1_gledger " +
            //     " where doc_date between '" + From_Date + "' and '" + To_Date + "' group by AC_CODE,ac,COMPANY_CODE,Group_Code,BSGroupName,group_Type having sum(case drcr when 'D' then amount else -amount end) <> 0 ";

            // string qry = "select sum(case drcr when 'D' then amount else -amount end) as balance,AC_CODE,ac,COMPANY_CODE,'' as Group_Code,'' as BSGroupName,'' as group_Type from nt_1_gledger " ;

            string qry = "select a.Group_code,a.group_name_e as BSGroupName,a.group_Type," +
"SUM(case g.drcr when 'D' then g.Amount when 'C' then -Amount end)as Balance " +
"from qrymstaccountmaster as a,qrycashtransactionTrialbalance as g ";
            if (ViewState["Cwhere"] != null)
            {
                if (ViewState["Cwhere"].ToString() != string.Empty)
                {
                    string whr = ViewState["Cwhere"].ToString();
                    whr = whr.Replace('+', ' ');
                    whr = whr.Replace("-", "'");
                    string doc_date = ViewState["Doc_Date"].ToString();
                    doc_date.Replace('+', ' ');
                    doc_date.Replace("-", "'");
                    int index = doc_date.IndexOf('=');
                    int endindex = doc_date.Length - index;
                    string d = doc_date.Substring(index + 1, endindex - 1);
                    d = d.Trim('-');
                    doc_date = "Where DOC_DATE<='" + d + "'";
                    qry = qry + " " + doc_date + whr;
                }
            }
            if (ViewState["Cwhere"] != null)
            {
                if (ViewState["Cwhere"].ToString() != string.Empty)
                {
                    qry = qry + " and a.COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and a.accoid=g.ac group by a.Group_Code,a.group_Name_E,a.group_Type order by a.group_name_e,a.Group_Code   ";
                }
                else
                {
                    qry = qry + "  where a.accoid=g.ac and a.COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " group by a.Group_Code,a.group_Name_E,a.group_Type order by a.group_name_e,a.Group_Code   ";
                }
            }

            ds = clsDAL.SimpleQuery(qry);

            DataTable dt = new DataTable();
            dt.Columns.Add("Group_Code", typeof(string));
            dt.Columns.Add("BSGroupName", typeof(string));
            dt.Columns.Add("group_Type", typeof(string));
            dt.Columns.Add("Debit", typeof(string));
            dt.Columns.Add("Credit", typeof(string));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // string Accode = ds.Tables[0].Rows[i]["AC_CODE"].ToString();
                        //DataRow[] dr1 = dtAc.Select("Ac_Code = " + Accode + " and Company_Code=" + Session["Company_Code"].ToString());
                        //if (dr1.Length > 0)
                        //{
                        //    ds.Tables[0].Rows[i]["Group_Code"] = dr1[0]["Group_Code"].ToString();
                        //    ds.Tables[0].Rows[i]["BSGroupName"] = dr1[0]["BSGroupName"].ToString();
                        //    ds.Tables[0].Rows[i]["group_Type"] = dr1[0]["group_Type"].ToString();
                        //}
                        DataRow dr = dt.NewRow();

                        dr["Group_Code"] = ds.Tables[0].Rows[i]["Group_Code"].ToString();
                        dr["BSGroupName"] = ds.Tables[0].Rows[i]["BSGroupName"].ToString();
                        dr["group_Type"] = ds.Tables[0].Rows[i]["group_Type"].ToString();
                        //dr["Debit"] = ds.Tables[0].Rows[i]["Debit"].ToString();
                        //dr["Credit"] = ds.Tables[0].Rows[i]["Credit"].ToString();
                        //double bal = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString());
                        //if (bal > 0)
                        //{
                        //    dr["Debit"] = bal.ToString();
                        //    dr["Credit"] = "";
                        //}
                        //else
                        //{
                        //    dr["Debit"] = "";
                        //    dr["Credit"] = bal.ToString();
                        //}

                        dt.Rows.Add(dr);
                    }
                }

                dtl.DataSource = dt;
                dtl.DataBind();
                lblnetDebit.Text = netdebit.ToString();
                lblnetCredit.Text = netcredit.ToString();
                double totaldiffrenceabs = Math.Abs(Math.Round(netdifference, 2));
                lblTotalDifference.Text = Convert.ToString(totaldiffrenceabs);
            }

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;
            string accode = lnkOV.Text;
            // Session["VOUC_NO"] = no;
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt1 + "','" + todt1 + "','DrCr')", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            double groupdebitamt = 0.00; double groupcreditamt = 0.00;
            double bal = 0.00;
            DataList dtl1 = (DataList)e.Item.FindControl("dtlDetails");
            Label lblgroupCode = (Label)e.Item.FindControl("lblGroup_Code");
            Label lblGroupType = (Label)e.Item.FindControl("lblGroupType");
            string whr = string.Empty;
            if (ViewState["Cwhere"] != null)
            {
                if (ViewState["Cwhere"].ToString() != string.Empty)
                {
                    whr = ViewState["Cwhere"].ToString();
                    whr = whr.Replace('+', ' ');
                    whr = whr.Replace("-", "'");
                    //qry = qry + " " + whr;
                    string doc_date = ViewState["Doc_Date"].ToString();
                    doc_date.Replace('+', ' ');
                    doc_date.Replace("-", "'");
                    int index = doc_date.IndexOf('=');
                    int endindex = doc_date.Length - index;
                    string d = doc_date.Substring(index + 1, endindex - 1);
                    d = d.Trim('-');
                    doc_date = "Where  DOC_DATE<='" + d + "'";
                    whr = doc_date + " " + whr;
                }
            }
            DataSet ds = new DataSet();
            string qry = "";
            if (ViewState["Cwhere"] != null)
            {
                if (ViewState["Cwhere"].ToString() != string.Empty)
                {
                    if (lblGroupType.Text == "B")
                    {
                        qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                       " from qrycashtransactionTrialbalance " + whr + " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Group_Code="
                       + lblgroupCode.Text + " and DOC_DATE<='" + To_Date.ToString()
                       + "'  group by AC_CODE,Ac_Name_E,CityName having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by CityName,Ac_Name_E";
                    }
                    else
                    {
                        qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                      " from qrycashtransactionTrialbalance " + whr + " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_CODE=" + Convert.ToInt32(Session["year"].ToString()) + " and Group_Code="
                      + lblgroupCode.Text + " and DOC_DATE between '" + From_Date.ToString() + "' and '" + To_Date.ToString()
                      + "'  group by AC_CODE,Ac_Name_E,CityName having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by CityName,Ac_Name_E";
                    }

                }
                else
                {
                    if (lblGroupType.Text == "B")
                    {
                        qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                           " from qrycashtransactionTrialbalance where COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Group_Code="
                           + lblgroupCode.Text + " and DOC_DATE<='" + To_Date.ToString()
                           + "'  group by AC_CODE,Ac_Name_E,CityName having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by CityName,Ac_Name_E";
                    }
                    else
                    {
                        qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                          " from qrycashtransactionTrialbalance where COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_CODE=" + Convert.ToInt32(Session["year"].ToString()) + "  and Group_Code="
                          + lblgroupCode.Text + " and DOC_DATE between '" + From_Date.ToString() + "' and '" + To_Date.ToString()
                          + "'  group by AC_CODE,Ac_Name_E,CityName having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by CityName,Ac_Name_E";
                    }
                }
            }
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AC_CODE", typeof(string));
                dt.Columns.Add("Ac_Name_E", typeof(string));
                dt.Columns.Add("CityName", typeof(string));
                dt.Columns.Add("DebitD", typeof(string));
                dt.Columns.Add("CreditD", typeof(string));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();

                            dr["AC_CODE"] = ds.Tables[0].Rows[i]["AC_CODE"].ToString();
                            dr["Ac_Name_E"] = ds.Tables[0].Rows[i]["Ac_Name_E"].ToString();
                            dr["CityName"] = ds.Tables[0].Rows[i]["CityName"].ToString();


                            bal = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString());
                            if (bal > 0)
                            {
                                groupdebitamt += bal;
                                //double balDebit = bal;
                                dr["DebitD"] = bal.ToString();
                                dr["CreditD"] = "";
                            }
                            else
                            {
                                double balminus = Math.Abs(bal);
                                groupcreditamt += balminus;
                                dr["DebitD"] = "";
                                dr["CreditD"] = balminus.ToString();
                            }
                            dt.Rows.Add(dr);
                        }

                        Label lblDebitTotal = (Label)e.Item.FindControl("lblDebitTotal");
                        lblDebitTotal.Text = groupdebitamt.ToString();


                        Label lblCreditTotal = (Label)e.Item.FindControl("lblCreditTotal");
                        lblCreditTotal.Text = groupcreditamt.ToString();
                        Label lblDiff = (Label)e.Item.FindControl("lblDiff");
                        lblDiff.Text = "Diff:" + (Convert.ToDouble(lblDebitTotal.Text) - Convert.ToDouble(lblCreditTotal.Text)).ToString();

                        netdebit += groupdebitamt;
                        netcredit += groupcreditamt;
                        netdifference = netdebit - netcredit;

                    }
                }
                dtl1.DataSource = dt;
                dtl1.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            email = txtEmail.Text.ToString();
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);
            pnlMain.RenderControl(tw);
            string s = sw.ToString();
            //WebClient client = new WebClient();
            //Stream data = client.OpenRead(HttpContext.Current.Request.Url.AbsoluteUri);
            //StreamReader reader = new StreamReader(data);
            //string s = reader.ReadToEnd();
            //data.Close();
            //reader.Close();
            // string s = HttpContext.Current.Request.Url.AbsoluteUri;
            //GetHtml(s);
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
            msg.Body = "Trial Balnace";
            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            msg.Subject = "Trial Balance Report  " + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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
    public static string GetHtml(string urlAddr)
    {
        if (urlAddr == null || string.IsNullOrEmpty(urlAddr))
        {
            throw new ArgumentNullException("urlAddr");
        }
        else
        {
            string result;

            //1.Create the request object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddr);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = 2000;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.PreAuthenticate = false;
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.Proxy = new WebProxy(urlAddr, true);
            request.UseDefaultCredentials = true;

            //2.Add the container with the active 
            CookieContainer cc = new CookieContainer();
            var s = request.RequestUri;

            //3.Must assing a cookie container for the request to pull the cookies
            request.CookieContainer = cc;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Stream sd = response.GetResponseStream();

            //Encoding enc = Encoding.GetEncoding(1252);
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                //Close and clean up the StreamReader
                sr.Close();
            }
            return result;

        }
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
        pnlMain.RenderControl(tw);
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
}