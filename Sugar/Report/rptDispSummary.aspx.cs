using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Net.Mail;

public partial class Report_rptDispSummary : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet bindqry;
    DataTable d2;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    string f = "../GSReports/DispatchSummary_.htm";
    string f_Main = "../Report/DispatchSummary_";
    protected void Page_Load(object sender, EventArgs e)
    {
        Branch_Code = Request.QueryString["Branch_Code"];
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        if (!Page.IsPostBack)
        {
            BindList();
        }
    }

    private void BindList()
    {
        try
        {

            qry = "select Distinct doc_dateConverted as do_date,doc_date as date from qrydohead " +
                     " where tran_type NOT IN('LV','MM') and doc_date between '" + fromDT + "' and '" + toDT + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 order by date";


            ds = new DataSet();

            ds = clsDAL.SimpleQuery(qry);

            qry = "select doc_no as no,isnull(millshortname,millname) as mill, mill_rate as millrate,quantal as quantal," +
      " getpassname as getpass,truck_no as truck,voucher_no as VN,voucher_type as VT,memo_no as MM,SB_No as SB ," +
       " isnull(transportshortname,transportname) as transport,isnull(doshortname,doname) as do,purc_no as purcno," +
       "purc_order as purcorder,isnull(billtoshortname,billtoshortname) as billto,isnull(shiptoshortname,shiptoname) as shifto,doc_date "
        + "  from qrydohead where doc_date between '" + fromDT + "' and '" + toDT + "'and tran_type NOT IN('LV','MM') and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                  + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0  order by mill asc";
            bindqry = clsDAL.SimpleQuery(qry);
            d2 = bindqry.Tables[0];

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        datalist.DataSource = dt;
                        datalist.DataBind();
                    }
                    else
                    {
                        datalist.DataSource = null;
                        datalist.DataBind();
                    }
                }
            }
            //}
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            clsDAL.CloseConnection();
        }
    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //using (clsDataProvider obj = new clsDataProvider())
        //{
        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        Label lbldodate = (Label)e.Item.FindControl("lbldodate");
        Label lblTotalQntl = (Label)e.Item.FindControl("lblTotalQntl");
        string do_date = lbldodate.Text;
        string date = DateTime.Parse(do_date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        DataView view = new DataView(d2, "doc_date='" + lbldodate.Text + "'", "doc_date", DataViewRowState.CurrentRows);
        DataTable selectedvalue = view.ToTable(true, "no", "mill", "millrate", "quantal", "getpass", "truck", "VN", "VT", "MM", "SB", "transport", "do", "purcno", "purcorder",
            "billto", "shifto", "doc_date");
        if (selectedvalue.Rows.Count > 0)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("no", typeof(string)));
            dt.Columns.Add(new DataColumn("mill", typeof(string)));
            dt.Columns.Add(new DataColumn("millrate", typeof(string)));
            dt.Columns.Add(new DataColumn("quantal", typeof(double)));
            dt.Columns.Add(new DataColumn("getpass", typeof(string)));
            dt.Columns.Add(new DataColumn("truck", typeof(string)));
            dt.Columns.Add(new DataColumn("transport", typeof(string)));
            dt.Columns.Add(new DataColumn("do", typeof(string)));
            dt.Columns.Add(new DataColumn("purcno", typeof(string)));
            dt.Columns.Add(new DataColumn("purcorder", typeof(string)));
            dt.Columns.Add(new DataColumn("PS", typeof(string)));
            dt.Columns.Add(new DataColumn("VO", typeof(string)));
            dt.Columns.Add(new DataColumn("SB", typeof(string)));
            dt.Columns.Add(new DataColumn("MM", typeof(string)));
            dt.Columns.Add(new DataColumn("billto", typeof(string)));
            dt.Columns.Add(new DataColumn("shifto", typeof(string)));
            if (selectedvalue.Rows.Count > 0)
            {
                for (int i = 0; i < selectedvalue.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    string VN = selectedvalue.Rows[i]["VN"].ToString();
                    string VT = selectedvalue.Rows[i]["VT"].ToString();
                    string SB = selectedvalue.Rows[i]["SB"].ToString();

                    if (VT == "PS")
                    {
                        dr["PS"] = VN;
                    }
                    else
                    {
                        dr["VO"] = VN;

                    }
                    if (!string.IsNullOrEmpty(SB))
                    {
                        dr["SB"] = SB;
                    }
                    dr["billto"] = selectedvalue.Rows[i]["billto"].ToString();
                    dr["shifto"] = selectedvalue.Rows[i]["shifto"].ToString();
                    dr["no"] = selectedvalue.Rows[i]["no"].ToString();
                    dr["MM"] = selectedvalue.Rows[i]["MM"].ToString();
                    dr["mill"] = selectedvalue.Rows[i]["mill"].ToString();
                    double millrate = double.Parse(selectedvalue.Rows[i]["millrate"].ToString());
                    dr["millrate"] = Math.Abs(millrate);
                    dr["quantal"] = selectedvalue.Rows[i]["quantal"].ToString();
                    dr["getpass"] = selectedvalue.Rows[i]["getpass"].ToString();
                    dr["truck"] = selectedvalue.Rows[i]["truck"].ToString();
                    dr["transport"] = selectedvalue.Rows[i]["transport"].ToString();
                    dr["do"] = selectedvalue.Rows[i]["do"].ToString();
                    dr["purcno"] = selectedvalue.Rows[i]["purcno"].ToString();
                    dr["purcorder"] = selectedvalue.Rows[i]["purcorder"].ToString();
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    lblTotalQntl.Text = Convert.ToString(dt.Compute("SUM(quantal)", string.Empty));
                    //lblToRecieveTotal.Text = Convert.ToString(dt.Compute("SUM(torecieve)", string.Empty));
                    dtlDetails.DataSource = dt;
                    dtlDetails.DataBind();
                }
                else
                {
                    dtlDetails.DataSource = null;
                    dtlDetails.DataBind();
                }
            }
            else
            {
                dtlDetails.DataSource = null;
                dtlDetails.DataBind();
            }
        }
        //}
    }
    protected void lnkPS_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkPS = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkPS.NamingContainer;
            Label Label8 = (Label)item.FindControl("Label8");
            string ps = lnkPS.Text;
            Int32 Action = 1;
            Int32 count = Convert.ToInt16(clsCommon.getString("SELECT purchaseid  from nt_1_sugarpurchase where doc_no=" + ps + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " "));
            Session["PURC_NO"] = ps;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:sugarpurchase('" + count + "','" + Action + "');", true);
            lnkPS.Focus();
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
            LinkButton lnkDO = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkDO.NamingContainer;
            string ps = lnkDO.Text;
            Session["DO_NO"] = ps;
            Int32 Action = 1;
            Int32 count = Convert.ToInt16(clsCommon.getString("SELECT doid  from nt_1_deliveryorder where doc_no=" + ps + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  "));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kdjsd", "javascript:DO('" + count + "','" + Action + "');", true);
            lnkDO.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void lnkSB_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkSB = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkSB.NamingContainer;
            string no = lnkSB.Text;
            Session["SB_NO"] = no;
            Int32 Action = 1;
            Int32 count = Convert.ToInt16(clsCommon.getString("SELECT saleid  from nt_1_sugarsale where doc_no=" + no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  "));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:salebill('" + count + "','" + Action + "');", true);
            lnkSB.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void lnkOV_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;
            string no = lnkOV.Text;
            Session["VOUC_NO"] = no;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:loadingvoucher();", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void lnkMM_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkMM = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkMM.NamingContainer;
            string no = lnkMM.Text;
            Session["MEMO_NO"] = no;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjs", "javascript:memo();", true);
            lnkMM.Focus();
        }
        catch (Exception)
        {
            throw;
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
        pnlMain.RenderControl(tw);
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
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != string.Empty)
        {
            try
            {
                string email = txtEmail.Text;
                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                pnlMain.RenderControl(tw);
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
                catch (Exception)
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
                msg.Body = "Dispatch Summary Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Dispatch Summary Report " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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
                //Response.Write("mail err:" + e1);
                Response.Write("<script>alert('Error sending Mail');</script>");
                return;
            }
            Response.Write("<script>alert('Mail sent successfully');</script>");
        }
    }
}
