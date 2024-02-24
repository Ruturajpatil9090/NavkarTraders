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

public partial class Report_rptDispDiffToPay : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    string f = "../GSReports/DispDiffToPay_.htm";
    string f_Main = "../Report/DispDiffToPay_";
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        string isAuthenticate = string.Empty;
        string user = string.Empty;

        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                BindList();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }

    private void BindList()
    {
        try
        {
            fromDT = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy-MM-dd");
            toDT = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy-MM-dd");
            //if (string.IsNullOrEmpty(Branch_Code))
            //{
            //    qry = "select CONVERT(varchar(10),d.doc_date,103) as tdate,d.doc_no as tno,b.Ac_Name_E as getpass,ISNULL(a.Short_Name,a.Ac_Name_E) as mill,d.voucher_no,d.voucher_type," +
            //     " d.Quantal as quantal,d.mill_rate as millrate,d.sale_rate as salerate,ISNULL(c.Short_Name,c.Ac_Name_E) as broker from " + tblPrefix + "deliveryorder d " +
            //     " left outer join " + tblPrefix + "AccountMaster a on d.mill_code=a.Ac_Code AND d.company_code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on d.GETPASSCODE=b.Ac_Code AND d.company_code=b.Company_Code" +
            //     " left outer join " + tblPrefix + "AccountMaster c on d.broker=c.Ac_Code AND d.company_code=c.Company_Code where d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.tran_type='DO' and d.desp_type='DO' and d.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
            //}
            //else
            //{
            //    qry = "select CONVERT(varchar(10),d.doc_date,103) as tdate,d.doc_no as tno,b.Ac_Name_E as getpass,ISNULL(a.Short_Name,a.Ac_Name_E) as mill,d.voucher_no,d.voucher_type ," +
            //     " d.Quantal as quantal,d.mill_rate as millrate,d.sale_rate as salerate,ISNULL(c.Short_Name,c.Ac_Name_E) as broker from " + tblPrefix + "deliveryorder d " +
            //     " left outer join " + tblPrefix + "AccountMaster a on d.mill_code=a.Ac_Code AND d.company_code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on d.GETPASSCODE=b.Ac_Code AND d.company_code=b.Company_Code" +
            //      " left outer join " + tblPrefix + "AccountMaster c on d.broker=c.Ac_Code AND d.company_code=c.Company_Code where d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.tran_type='DO' and d.desp_type='DO' and d.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + "";
            //}
            //qry = "select CONVERT(varchar(10),d.doc_date,103) as tdate,d.doc_no as tno,b.Ac_Name_E as getpass,ISNULL(a.Short_Name,a.Ac_Name_E) as mill,d.voucher_no,d.voucher_type ," +
            //     " d.Quantal as quantal,d.mill_rate as millrate,d.sale_rate as salerate,ISNULL(c.Short_Name,c.Ac_Name_E) as broker from " + tblPrefix + "deliveryorder d " +
            //     " left outer join " + tblPrefix + "AccountMaster a on d.mill_code=a.Ac_Code AND d.company_code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on d.GETPASSCODE=b.Ac_Code AND d.company_code=b.Company_Code" +
            //     " left outer join " + tblPrefix + "AccountMaster c on d.broker=c.Ac_Code AND d.company_code=c.Company_Code where d.doc_date between '" + fromDT + "' and '" + toDT + "' and d.tran_type='DO' and d.desp_type='DO' and d.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";

            qry = " select doc_dateConverted as tdate,doc_no as tno,getpassname as getpass,millname as mill,voucher_no,voucher_type," +
                " quantal as quantal,mill_rate as millrate,sale_rate as salerate,brokername as broker from  qrydohead  where doc_date between '" + fromDT + "' and '" + toDT + "' and tran_type='DO' " +
                " and desp_type='DO'and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and purc_no!=0  ";

            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("tdate", typeof(string)));
                dt.Columns.Add(new DataColumn("tno", typeof(string)));
                dt.Columns.Add(new DataColumn("getpass", typeof(string)));
                dt.Columns.Add(new DataColumn("mill", typeof(string)));
                dt.Columns.Add(new DataColumn("quantal", typeof(double)));
                dt.Columns.Add(new DataColumn("millrate", typeof(double)));
                dt.Columns.Add(new DataColumn("salerate", typeof(double)));
                dt.Columns.Add(new DataColumn("broker", typeof(string)));
                dt.Columns.Add(new DataColumn("amount", typeof(double)));
                dt.Columns.Add(new DataColumn("voucher", typeof(string)));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["tdate"] = ds.Tables[0].Rows[i]["tdate"].ToString();
                        dr["tno"] = ds.Tables[0].Rows[i]["tno"].ToString();
                        dr["getpass"] = ds.Tables[0].Rows[i]["getpass"].ToString();
                        dr["mill"] = ds.Tables[0].Rows[i]["mill"].ToString();
                        string voucherno = ds.Tables[0].Rows[i]["voucher_no"].ToString();
                        string vouchertype = ds.Tables[0].Rows[i]["voucher_type"].ToString();
                        dr["voucher"] = voucherno + " " + vouchertype;
                        double qntl = Convert.ToDouble(ds.Tables[0].Rows[i]["quantal"].ToString());
                        dr["quantal"] = qntl;
                        double millrate = Convert.ToDouble(ds.Tables[0].Rows[i]["millrate"].ToString());
                        double salerate = Convert.ToDouble(ds.Tables[0].Rows[i]["salerate"].ToString());
                        dr["millrate"] = millrate;
                        dr["salerate"] = salerate;
                        dr["broker"] = ds.Tables[0].Rows[i]["broker"].ToString();
                        double amount = ((salerate - millrate) * qntl);
                        if (amount < 0)
                        {
                            dr["amount"] = Math.Abs(amount);
                            dt.Rows.Add(dr);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        //lblToPayTotal.Text = Convert.ToString(dt.Compute("SUM(topay)", string.Empty));
                        //lblDiffAmount.Text = Convert.ToString(dt.Compute("SUM(amount)", string.Empty));
                        datalist.DataSource = dt;
                        datalist.DataBind();
                    }
                    else
                    {
                        datalist.DataSource = null;
                        datalist.DataBind();
                    }
                }
                else
                {
                    datalist.DataSource = null;
                    datalist.DataBind();
                }
            }
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
    protected void lnkLV_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkLV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkLV.NamingContainer;
            string no = lnkLV.Text.Replace("LV", "").Trim();
            Session["LV_NO"] = no;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:DebitNote();", true);
            lnkLV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
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
        string strFile = "DispDiffToPayreport.xls";
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
                msg.Body = "Dispatch Diff To Pay Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Dispatch Diff To Pay " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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