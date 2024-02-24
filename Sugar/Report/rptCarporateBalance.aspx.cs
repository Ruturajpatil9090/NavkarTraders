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

public partial class Report_rptCarporateBalance : System.Web.UI.Page
{
    DataSet ds = null;
    DataTable dt = null;
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    string Branch_Code = string.Empty;
    string fromDt = string.Empty;
    string toDt = string.Empty;
    string f = "../GSReports/CarporateBalance_.htm";
    string f_Main = "../Report/CarporateBalance_";
    double grandBalTotal = 0.00;
    double grandQntlTotal = 0.00;
    string PDS = string.Empty;
    string acCode = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        Branch_Code = Convert.ToString(Request.QueryString["Branch_Code"]);
        fromDt = Request.QueryString["fromDt"];
        toDt = Request.QueryString["toDt"];
        PDS = Request.QueryString["PDS"];
        acCode = Request.QueryString["ac_code"];
        if (!IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                BindList();

                lblCompany.Text = Session["Company_Name"].ToString();
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
            if (acCode != string.Empty && acCode != null)
            {

                qry = "select distinct carporatepartyaccountname as Party,ac_code as Party_Code  from qrycarporatedobalance " +
                      " where ac_code=" + acCode + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Doc_Date between '" + fromDt + "' and '" + toDt + "' order by carporatepartyaccountname";

            }
            else
            {

                qry = "select distinct carporatepartyaccountname as Party,ac_code as Party_Code  from qrycarporatedobalance " +
                      " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Doc_Date between '" + fromDt + "' and '" + toDt + "' order by carporatepartyaccountname";

            }

            //using (clsDataProvider obj = new clsDataProvider())
            //{
            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add(new DataColumn("IsPresent", typeof(string)));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (acCode != string.Empty && acCode != null)
                        {

                            qry = "select  DISTINCT doc_no ,doc_dateConverted as Doc_Date,carporatepartyunitname as Unit,sell_rate as Sale_Rate,pono as podetail,quantal as Qntl ,dispatched as desp FROM " +
                            " qrycarporatedobalance where Ac_Code=" + acCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Doc_Date between '" + fromDt + "' and '" + toDt + "'";

                        }
                        else
                        {

                            qry = "select DISTINCT doc_no,doc_dateConverted as Doc_Date,carporatepartyunitname as Unit,sell_rate as Sale_Rate, pono as podetail, quantal as Qntl, dispatched as desp FROM qrycarporatedobalance"
              + " where Ac_Code=" + ds.Tables[0].Rows[i]["Party_Code"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " "
              + "  and Doc_Date between '" + fromDt + "' and '" + toDt + "'";

                        }
                        DataSet ds2 = new DataSet();
                        //ds2 = obj.GetDataSet(qry);
                        ds2 = clsDAL.SimpleQuery(qry);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt2 = new DataTable();
                            dt2 = ds2.Tables[0];
                            double qntl = Convert.ToDouble(dt2.Compute("SUM(Qntl)", string.Empty));
                            double desp = Convert.ToDouble(dt2.Compute("SUM(desp)", string.Empty));
                            double bal = qntl - desp;
                            if (bal == 0)
                            {
                                ds.Tables[0].Rows[i]["IsPresent"] = "0";
                            }
                            else
                            {
                                ds.Tables[0].Rows[i]["IsPresent"] = "1";
                            }
                        }
                    }

                    dt = new DataTable();
                    dt = ds.Tables[0];
                    DataTable dtset = dt.Clone();
                    foreach (DataRow row in dt.Rows)
                    {
                        string hasBal = row["IsPresent"].ToString();
                        if (hasBal == "1")
                        {
                            dtset.Rows.Add(row.ItemArray);
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        dtlist.DataSource = dtset;
                        dtlist.DataBind();
                        lblGrandBalTotal.Text = grandBalTotal.ToString();
                        lblGrandQntlTotal.Text = grandQntlTotal.ToString();
                    }
                    else
                    {
                        dtlist.DataSource = null;
                        dtlist.DataBind();
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
    protected void dtlist_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtlDetails = e.Item.FindControl("dtlDetails") as DataList;
            Label lblPartyCode = e.Item.FindControl("lblPartyCode") as Label;
            Label lblQuatalTotal = e.Item.FindControl("lblQuatalTotal") as Label;
            Label lblBalanceTotal = e.Item.FindControl("lblBalanceTotal") as Label;
            string partycode = lblPartyCode.Text;
            ds = new DataSet();

            qry = "select DISTINCT doc_no,doc_dateConverted as Doc_Date,doc_date as Schedule_Date,carporatepartyunitname as Unit,sell_rate as Sale_Rate,pono as podetail,quantal as Qntl, dispatched as desp FROM qrycarporatedobalance" +
                  " where Ac_Code=" + partycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "   and Doc_Date between '" + fromDt + "' and '" + toDt + "'";

            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("Doc_No", typeof(string)));
                dt.Columns.Add(new DataColumn("Doc_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Schedule_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit", typeof(string)));
                dt.Columns.Add(new DataColumn("Sale_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Qntl", typeof(double)));
                dt.Columns.Add(new DataColumn("desp", typeof(double)));
                dt.Columns.Add(new DataColumn("balance", typeof(double)));
                dt.Columns.Add(new DataColumn("podetail", typeof(string)));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Doc_No"] = ds.Tables[0].Rows[i]["Doc_No"].ToString();
                        dr["Doc_Date"] = ds.Tables[0].Rows[i]["Doc_Date"].ToString();
                        dr["Schedule_Date"] = ds.Tables[0].Rows[i]["Schedule_Date"].ToString();
                        dr["Unit"] = ds.Tables[0].Rows[i]["Unit"].ToString();
                        dr["Sale_Rate"] = ds.Tables[0].Rows[i]["Sale_Rate"].ToString();
                        double Qntl = Convert.ToDouble(ds.Tables[0].Rows[i]["Qntl"].ToString());
                        dr["Qntl"] = Qntl;
                        double desp = Convert.ToDouble(ds.Tables[0].Rows[i]["desp"].ToString());
                        dr["desp"] = desp;
                        dr["podetail"] = ds.Tables[0].Rows[i]["podetail"].ToString();
                        double balance = Qntl - desp;
                        dr["balance"] = balance;
                        if (balance != 0)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        double qntl = Convert.ToDouble(dt.Compute("SUM(Qntl)", string.Empty));
                        lblQuatalTotal.Text = qntl.ToString();
                        double balance = Convert.ToDouble(dt.Compute("SUM(balance)", string.Empty));
                        lblBalanceTotal.Text = balance.ToString();
                        if (balance > 0)
                        {
                            grandBalTotal += balance;
                        }
                        grandQntlTotal += qntl;
                        dtlDetails.DataSource = dt;
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
                msg.Body = "Carporate Balance";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Carporate Balance " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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