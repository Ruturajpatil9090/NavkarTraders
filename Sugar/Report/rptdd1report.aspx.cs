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
public partial class Sugar_Report_rptdd1report : System.Web.UI.Page
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

                qry = "select distinct Payment_To,paymentto_name from qrydd where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and doc_date='" + date + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 order by paymentto_name ";

                dsbind = clsDAL.SimpleQuery(qry);


                //qry = "select SB_No,salebillto_name,salebillto_city,quantal,sale_rate,PurchaseRate,(((sale_rate*gst_rate)/100)+sale_rate) as ratewithGST," +
                //    " (((PurchaseRate*gst_rate)/100)+PurchaseRate) as dowithGST,sbroundoff,psroundoff,pstcs_amt,saletcs_amt," +
                //    " ((quantal*(((PurchaseRate*gst_rate)/100)+PurchaseRate))+psroundoff+pstcs_amt) as PSAmt,((quantal*(((sale_rate*gst_rate)/100)+sale_rate))+sbroundoff+saletcs_amt) as SBAmt," +
                //    " 0 as RecdAmt,0 as Bal,Payment_To from qrydd where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_date='" + date + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 ";

                qry = "select doc_no,SB_No,salebillto_name,salebillto_city,quantal,sale_rate,PurchaseRate,(((sale_rate*gst_rate)/100)+sale_rate) as ratewithGST," +
                    " (((PurchaseRate*gst_rate)/100)+PurchaseRate) as dowithGST,sbroundoff,psroundoff,pstcs_amt,saletcs_amt," +
                    " ((quantal*(((PurchaseRate*gst_rate)/100)+PurchaseRate))) as PSAmt,((quantal*(((sale_rate*gst_rate)/100)+sale_rate))+sbroundoff+saletcs_amt) as SBAmt," +
                    " 0 as RecdAmt,0 as Bal,Payment_To from qrydd where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_date='" + date + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 ";


                dsitembind = clsDAL.SimpleQuery(qry);
                if (dsbind != null)
                {
                    ddate = dsbind.Tables[0];
                    if (ddate.Rows.Count > 0)
                    {
                        datalist.DataSource = ddate;
                        datalist.DataBind();

                        //  lblgrandqntltotal.Text = grandqntlTotal.ToString();
                        //lblgrandamounttotal.Text = grandamounttotal.ToString();
                        //lblgrandtotalTDSAmt.Text = grandtotalTDSAmt.ToString();
                        //lblgrandtotalCGSTAmt.Text = grandtotalCGSTAmt.ToString();
                        //lblgrandtotalSGSTAmt.Text = grandtotalSGSTAmt.ToString();
                        //lblgrandtotalIGSTAmt.Text = grandtotalIGSTAmt.ToString();
                        //lblgrandtotalBillAmt.Text = grandtotalBillAmt.ToString();
                        //lblgrandtotalTCSAmt.Text = grandtotalTCSAmt.ToString();
                        //lblgrandtotalTCSNetpayAmt.Text = grandtotalTCSNetpayAmt.ToString();

                    }
                }
            }
        }

    }
    private void BindList()
    {
        try
        {

            //if (!string.IsNullOrEmpty(Request.QueryString["Mill_Code"]))
            //{
            //    string millCode = Request.QueryString["Mill_Code"].ToString();
            //    txtEmail.Text = clsCommon.getString("Select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=" + millCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
            //}

            qry = "select distinct doc_no,Payment_To,paymentto_name from qrydd where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                   + " and doc_date='" + date + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 order by paymentto_name ";
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
        try
        {
            DataTable dtlistbind = new DataTable();
            dtlistbind = dsitembind.Tables[0];

            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblPayment_To = (Label)e.Item.FindControl("lblPayment_To");

            Label lblPSAmtTotal = (Label)e.Item.FindControl("lblPSAmtTotal");
            Label lblSBAmtTotal = (Label)e.Item.FindControl("lblSBAmtTotal");
            Label lblDiff = (Label)e.Item.FindControl("lblDiff");

            string millcode = lblPayment_To.Text;

            DataView view1 = new DataView(dtlistbind, "Payment_To='" + lblPayment_To.Text + "'", "Payment_To", DataViewRowState.CurrentRows);


            DataTable dtAcData = view1.ToTable( false  , "SB_No", "salebillto_name", "salebillto_city", "quantal", "sale_rate", "PurchaseRate", "ratewithGST", "dowithGST", "sbroundoff", "psroundoff", "pstcs_amt", "saletcs_amt", "PSAmt", "SBAmt", "RecdAmt", "Bal");


            if (dsitembind != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("SB_No", typeof(string)));
                dt.Columns.Add(new DataColumn("salebillto_name", typeof(string)));
                dt.Columns.Add(new DataColumn("salebillto_city", typeof(string)));
                dt.Columns.Add(new DataColumn("quantal", typeof(double)));
                dt.Columns.Add(new DataColumn("sale_rate", typeof(double)));
                dt.Columns.Add(new DataColumn("PurchaseRate", typeof(double)));
                dt.Columns.Add(new DataColumn("ratewithGST", typeof(double)));
                dt.Columns.Add(new DataColumn("dowithGST", typeof(double)));
                dt.Columns.Add(new DataColumn("sbroundoff", typeof(double)));
                dt.Columns.Add(new DataColumn("psroundoff", typeof(double)));
                dt.Columns.Add(new DataColumn("pstcs_amt", typeof(double)));
                dt.Columns.Add(new DataColumn("saletcs_amt", typeof(double)));
                dt.Columns.Add(new DataColumn("PSAmt", typeof(double)));
                dt.Columns.Add(new DataColumn("SBAmt", typeof(double)));
                dt.Columns.Add(new DataColumn("RecdAmt", typeof(double)));
                dt.Columns.Add(new DataColumn("Bal", typeof(double)));
                if (dsitembind.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dtAcData.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["SB_No"] = dtAcData.Rows[i]["SB_No"].ToString();
                        dr["salebillto_name"] = dtAcData.Rows[i]["salebillto_name"].ToString();
                        dr["salebillto_city"] = dtAcData.Rows[i]["salebillto_city"].ToString();
                        dr["quantal"] = dtAcData.Rows[i]["quantal"].ToString();
                        dr["sale_rate"] = dtAcData.Rows[i]["sale_rate"].ToString();
                        dr["PurchaseRate"] = dtAcData.Rows[i]["PurchaseRate"].ToString();
                        dr["ratewithGST"] = dtAcData.Rows[i]["ratewithGST"].ToString();
                        dr["dowithGST"] = dtAcData.Rows[i]["dowithGST"].ToString();
                        dr["sbroundoff"] = dtAcData.Rows[i]["sbroundoff"].ToString();
                        dr["psroundoff"] = 0; //dtAcData.Rows[i]["psroundoff"].ToString();
                        dr["pstcs_amt"] = 0;// dtAcData.Rows[i]["pstcs_amt"].ToString();
                        dr["saletcs_amt"] = dtAcData.Rows[i]["saletcs_amt"].ToString();
                        dr["PSAmt"] =  dtAcData.Rows[i]["PSAmt"].ToString();
                        dr["SBAmt"] = dtAcData.Rows[i]["SBAmt"].ToString();
                        dr["RecdAmt"] = dtAcData.Rows[i]["RecdAmt"].ToString();
                        dr["Bal"] = dtAcData.Rows[i]["Bal"].ToString();
                        

                        dt.Rows.Add(dr);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        lblPSAmtTotal.Text = Convert.ToString(dt.Compute("SUM(PSAmt)", string.Empty));
                        //double TCS_Amt = Convert.ToDouble(dt.Compute("SUM(TCS_Amt)", string.Empty));
                        //grandtotalTCSAmt = grandtotalTCSAmt + TCS_Amt;


                        lblSBAmtTotal.Text = Convert.ToString(dt.Compute("SUM(SBAmt)", string.Empty));
                        //double TCS_Net_Payable = Convert.ToDouble(dt.Compute("SUM(TCS_Net_Payable)", string.Empty));
                        //grandtotalTCSNetpayAmt = grandtotalTCSNetpayAmt + TCS_Net_Payable;

                        double psamttotal = Convert.ToDouble(lblPSAmtTotal.Text);
                        double sbamttotal = Convert.ToDouble(lblSBAmtTotal.Text);
                        double diff = sbamttotal - psamttotal;
                        lblDiff.Text = diff.ToString();
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
            // }
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