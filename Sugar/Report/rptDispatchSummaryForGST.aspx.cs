using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Net.Mail;

public partial class Report_rptDispatchSummaryForGST : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string qrycommon = string.Empty; 
    string Branch_Code = string.Empty;
    string f = "../GSReports/DispatchSummary_.htm";
    string f_Main = "../Report/DispatchSummary_";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Branch_Code = Request.QueryString["Branch_Code"];
        Branch_Code = Session["Branch_Code"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        qrycommon = "qrydohead";
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        if (!Page.IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        if (string.IsNullOrEmpty(Branch_Code))
        {
            qry = "select Distinct(Convert(varchar(10),d.doc_date,103)) as do_date,doc_date as date from " + tblPrefix + "deliveryorder d " +
              " where d.tran_type NOT IN('LV','MM') and d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.purc_no!=0 order by date";
        }
        else
        {
            qry = "select Distinct(Convert(varchar(10),d.doc_date,103)) as do_date,doc_date as date from " + tblPrefix + "deliveryorder d " +
                 " where d.tran_type NOT IN('LV','MM') and d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and d.purc_no!=0  order by date";
        }

        ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
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
    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        Label lbldodate = (Label)e.Item.FindControl("lbldodate");
        Label lblTotalQntl = (Label)e.Item.FindControl("lblTotalQntl");
        string do_date = lbldodate.Text;
        string date = DateTime.Parse(do_date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        if (string.IsNullOrEmpty(Branch_Code))
        {
            qry = "select d.doc_no as no,ISNULL(a.Short_Name,a.Ac_Name_E) as mill, d.mill_rate as millrate,(d.mill_rate+d.excise_rate) as GSTRate,ISNULL(d.SaleBillTo,0) as SaleBillTo,d.quantal as quantal,b.Ac_Name_E as getpass,d.truck_no as truck,d.voucher_no as VN,d.voucher_type as VT,d.memo_no as MM,d.SB_No as SB" +
                 " ,ISNULL(c.Short_Name,c.Ac_Name_E) as transport,ISNULL(e.Short_Name,e.Ac_Name_E) as do,d.purc_no as purcno,d.purc_order as purcorder,d.driver_no,d.Eway_Bill_No from " + tblPrefix + "deliveryorder d " +
                 " left outer join " + tblPrefix + "AccountMaster a on d.mill_code=a.Ac_Code AND d.company_code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on d.GETPASSCODE=b.Ac_Code AND d.company_code=b.Company_Code" +
                 " left outer join " + tblPrefix + "AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code left outer join " + tblPrefix + "AccountMaster e on d.DO=e.Ac_Code AND d.company_code=e.Company_Code" +
                 " where d.doc_date='" + date + "' and tran_type NOT IN('LV','MM') and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.purc_no!=0 order by mill asc";
        }
        else
        {
            qry = "select d.doc_no as no,ISNULL(a.Short_Name,a.Ac_Name_E) as mill, d.mill_rate as millrate,(d.mill_rate+d.excise_rate) as GSTRate,ISNULL(d.SaleBillTo,0) as SaleBillTo,d.quantal as quantal,b.Ac_Name_E as getpass,d.truck_no as truck,d.voucher_no as VN,d.voucher_type as VT,d.memo_no as MM,d.SB_No as SB" +
                 " ,ISNULL(c.Short_Name,c.Ac_Name_E) as transport,ISNULL(e.Short_Name,e.Ac_Name_E) as do,d.purc_no as purcno,d.purc_order as purcorder,d.driver_no,d.Eway_Bill_No from " + tblPrefix + "deliveryorder d " +
                 " left outer join " + tblPrefix + "AccountMaster a on d.mill_code=a.Ac_Code AND d.company_code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on d.GETPASSCODE=b.Ac_Code AND d.company_code=b.Company_Code" +
                 " left outer join " + tblPrefix + "AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code left outer join " + tblPrefix + "AccountMaster e on d.DO=e.Ac_Code AND d.company_code=e.Company_Code" +
                 " where d.doc_date='" + date + "' and tran_type NOT IN('LV','MM') and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and d.purc_no!=0 order by mill asc";
        }

        ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("no", typeof(string)));
            dt.Columns.Add(new DataColumn("mill", typeof(string)));
            dt.Columns.Add(new DataColumn("millrate", typeof(string)));
            dt.Columns.Add(new DataColumn("GSTRate", typeof(string)));
            dt.Columns.Add(new DataColumn("quantal", typeof(double)));
            dt.Columns.Add(new DataColumn("getpass", typeof(string)));
            dt.Columns.Add(new DataColumn("truck", typeof(string)));
            dt.Columns.Add(new DataColumn("transport", typeof(string)));
            dt.Columns.Add(new DataColumn("do", typeof(string)));
            dt.Columns.Add(new DataColumn("driver_no", typeof(string)));
            dt.Columns.Add(new DataColumn("Eway_Bill_No", typeof(string)));

            dt.Columns.Add(new DataColumn("purcorder", typeof(string)));
            dt.Columns.Add(new DataColumn("PS", typeof(string)));
            dt.Columns.Add(new DataColumn("VO", typeof(string)));
            dt.Columns.Add(new DataColumn("SB", typeof(string)));
            dt.Columns.Add(new DataColumn("MM", typeof(string)));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    string VN = ds.Tables[0].Rows[i]["VN"].ToString();
                    string VT = ds.Tables[0].Rows[i]["VT"].ToString();
                    string SB = ds.Tables[0].Rows[i]["SB"].ToString();

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
                    dr["no"] = ds.Tables[0].Rows[i]["no"].ToString();
                    dr["MM"] = ds.Tables[0].Rows[i]["MM"].ToString();
                    dr["mill"] = ds.Tables[0].Rows[i]["mill"].ToString();
                    double millrate = double.Parse(ds.Tables[0].Rows[i]["millrate"].ToString());
                    dr["millrate"] = Math.Abs(millrate);
                    dr["quantal"] = ds.Tables[0].Rows[i]["quantal"].ToString();
                    dr["GSTRate"] = ds.Tables[0].Rows[i]["GSTRate"].ToString();

                    Int32 salebilltoCode = ds.Tables[0].Rows[i]["SaleBillTo"].ToString().Trim() != string.Empty ? Convert.ToInt32(ds.Tables[0].Rows[i]["SaleBillTo"].ToString().Trim()) : 0;
                    if (salebilltoCode != 0)
                    {
                        string saleBillToName = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + salebilltoCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        dr["getpass"] = saleBillToName;
                    }
                    else
                    {
                        dr["getpass"] = ds.Tables[0].Rows[i]["getpass"].ToString();
                    }

                    dr["truck"] = ds.Tables[0].Rows[i]["truck"].ToString();
                    dr["transport"] = ds.Tables[0].Rows[i]["transport"].ToString();
                    dr["do"] = ds.Tables[0].Rows[i]["do"].ToString();
                    //dr["purcno"] = ds.Tables[0].Rows[i]["purcno"].ToString();
                    dr["driver_no"] = ds.Tables[0].Rows[i]["driver_no"].ToString();
                    dr["Eway_Bill_No"] = ds.Tables[0].Rows[i]["Eway_Bill_No"].ToString();

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
    }
    protected void lnkPS_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkPS = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkPS.NamingContainer;
            Label Label8 = (Label)item.FindControl("Label8");
            string ps = lnkPS.Text;
            Session["PURC_NO"] = ps;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:sugarpurchase();", true);
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
            Int32 dono = Convert.ToInt16(clsCommon.getString("SELECT doid  from nt_1_deliveryorder where doc_no=" + ps + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  "));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kdjsd", "javascript:Donew('" + dono + "','" + Action + "');", true);
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:salebill();", true);
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
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
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
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
