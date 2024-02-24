using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;

public partial class Sugar_Report_rptdd1summeryreport : System.Web.UI.Page
{
    string date = string.Empty;

    string Mill_Code = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string f = "../GSReports/DispMillWise_.htm";
    string f_Main = "../Report/DispMillWise_";
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    DataSet dsbind = new DataSet();
    DataSet dsitembind = new DataSet();
    DataTable ddate = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        date = Request.QueryString["dt"].ToString();
        string isAuthenticate = string.Empty;
        string user = string.Empty;
        user = Session["user"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                BindList();
            }
        }

    }
    private void BindList()
    {
        try
        {

            qry = "select  Payment_To,paymentto_name,0 as openingbal,SUM(((quantal*(((PurchaseRate*gst_rate)/100)+PurchaseRate))+psroundoff+pstcs_amt)) as amount,0 as payment,0 as balance,(SUM(((quantal*(((sale_rate*gst_rate)/100)+sale_rate))+sbroundoff+saletcs_amt))-SUM(((quantal*(((PurchaseRate*gst_rate)/100)+PurchaseRate))+psroundoff+pstcs_amt))) as DDDiff," +
                " SUM(((quantal*(((sale_rate*gst_rate)/100)+sale_rate))+sbroundoff+saletcs_amt)) as sale,sum(quantal) as bags from qrydd " +
                " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_date='" + date
                + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 group by Payment_To,paymentto_name order by paymentto_name ";
        
            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Payment_To=dt.Rows[i]["Payment_To"].ToString();

                            double openbal = Convert.ToDouble(clsCommon.getString("select SUM(case when DRCR='D' then AMOUNT else -amount end ) as balance from nt_1_gledger  where AC_CODE="+Payment_To
                                + " and COMPANY_CODE =" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE <'" + date + "'"));

                            double payment = Convert.ToDouble(clsCommon.getString("select SUM(case when DRCR='D' then AMOUNT else 0 end ) as balance from nt_1_gledger  where AC_CODE=" + Payment_To
                                + " and COMPANY_CODE =" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE ='" + date + "'"));

                            double amt = Convert.ToDouble(dt.Rows[i]["amount"]);
                            double finlamt = openbal + amt;

                            double balance = finlamt - payment;
                            dt.Rows[i]["openingbal"] = openbal;
                            dt.Rows[i]["amount"] = finlamt;
                            dt.Rows[i]["payment"] = payment;
                            dt.Rows[i]["balance"] = balance;

                        }
                        if (dt.Rows.Count > 0)
                        {
                            lblopeningbalTot.Text = Convert.ToString(dt.Compute("SUM(openingbal)", string.Empty));
                            lblAmountTot.Text = Convert.ToString(dt.Compute("SUM(amount)", string.Empty));
                            lblPaymentTot.Text = Convert.ToString(dt.Compute("SUM(payment)", string.Empty));
                            lblBalanceTot.Text = Convert.ToString(dt.Compute("SUM(balance)", string.Empty));
                            lblDDDiffTot.Text = Convert.ToString(dt.Compute("SUM(DDDiff)", string.Empty));
                            lblSaleTot.Text = Convert.ToString(dt.Compute("SUM(sale)", string.Empty));
                            lblBagsTot.Text = Convert.ToString(dt.Compute("SUM(bags)", string.Empty));
                        }
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
            // }
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
                msg.Body = "DD1 Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "DD1 Report " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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