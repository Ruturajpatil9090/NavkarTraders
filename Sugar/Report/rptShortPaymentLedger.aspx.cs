using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class Report_rptShortPaymentLedger : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    string Broker_Code = string.Empty;
    DataSet ds;
    DataTable dt;
    string from = "";
    string to = "";
    string tblPrefix = string.Empty;
    string f = "../GSReports/BrokerWiseShortPay_.htm";
    string f_Main = "../Report/BrokerWiseShortPay_";

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Broker_Code = Request.QueryString["Broker_Code"].ToString();
        if (!Page.IsPostBack)
        {
            lblCompanyName.Text = clsCommon.getString("Select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
            BindList();
        }
    }

    private void BindList()
    {
        try
        {
            //DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            //Label lblBrokerCode = (Label)e.Item.FindControl("lblBrokerCode");
            //string Broker = lblBrokerCode.Text;
            //Label lblAmountTotal = (Label)e.Item.FindControl("lblAmountTotal");
            //Label lblRecievedTotal = (Label)e.Item.FindControl("lblRecievedTotal");
            //Label lblShortTotal = (Label)e.Item.FindControl("lblShortTotal");
            //Label lblQntlTotal = (Label)e.Item.FindControl("lblQntlTotal");
            from = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            to = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            lblDate.Text = "From: <b>" + from + "</b> To:<b>" + to + "</b>";
            if (Broker_Code != string.Empty)
            {
                //qry = "select Convert(varchar(10),u.doc_date,103) as dt,u.Tran_Type as ttype,u.doc_no as #,u.Unit_Name as Party,u.lorry as lorry,u.NETQNTL as Qntl,a.Short_Name as Mill,u.Bill_Amount as VocAmount," +
                //    " ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS''RR') and t.Tran_Type=u.Tran_Type and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0) as Recieved," +
                //    " (u.Bill_Amount-ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS','RR') and t.Tran_Type=u.Tran_Type and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0)) as Short,u.BROKER,u.ASN from " + tblPrefix + "qryVoucherSaleUnion1 u" +
                //    " left outer join " + tblPrefix + "AccountMaster a on u.mill_code=a.Ac_Code and u.Company_Code=a.Company_Code where u.Ac_Code="
                //    + Broker_Code + " and u.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and u.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and u.doc_date between '" + from + "' and '" + to + 
                //    "' order by u.doc_date";
                //lblBrokerName.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + 
                //    "AccountMaster Where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + 
                //    " and Ac_Code=" + Broker_Code) + " Detailed Report";

                qry = "select Convert(varchar(10),u.doc_date,103) as dt,u.Tran_Type as ttype,u.doc_no as #,u.Unit_Name as Party,u.lorry as lorry,u.NETQNTL as Qntl,a.Short_Name as Mill,u.Bill_Amount as VocAmount,Year_Code," +
                  " ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS''RR') and t.Tran_Type=u.Tran_Type and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0) as Recieved," +
                  " (u.Bill_Amount-ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS','RR') and t.Tran_Type=u.Tran_Type and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0)) as Short,u.BROKER,u.ASN from " + tblPrefix + "qryVoucherSaleUnion1 u" +
                  " left outer join " + tblPrefix + "AccountMaster a on u.mill_code=a.Ac_Code and u.Company_Code=a.Company_Code where  u.Ac_Code="
                  + Broker_Code + " and u.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                  " and u.doc_date between '" + from + "' and '" + to +
                  "' order by u.doc_date";
                lblBrokerName.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix +
                    "AccountMaster Where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Ac_Code=" + Broker_Code) + " Detailed Report";
            }
            else
            {
                //qry = "select Convert(varchar(10),u.doc_date,103) as dt,u.Tran_Type as ttype,u.doc_no as #,u.Unit_Name as Party,u.lorry as lorry,u.NETQNTL as Qntl,a.Short_Name as Mill,u.Bill_Amount as VocAmount," +
                //    " ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS','RR') and t.Tran_Type=u.Tran_Type  and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0) as Recieved," +
                //    " (u.Bill_Amount-ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS','RR') and t.Tran_Type=u.Tran_Type  and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0)) as Short,u.BROKER,u.ASN from " + tblPrefix + "qryVoucherSaleUnion1 u" +
                //    " left outer join " + tblPrefix + "AccountMaster a on u.mill_code=a.Ac_Code and u.Company_Code=a.Company_Code where u.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) 
                //    + " and u.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and u.doc_date between '" + from + "' and '" + to + 
                //    "' order by u.doc_date";
                //lblBrokerName.Text = "All Short Payment Details Detailed Report";
                qry = "select Convert(varchar(10),u.doc_date,103) as dt,u.Tran_Type as ttype,u.doc_no as #,u.Unit_Name as Party,u.lorry as lorry,u.NETQNTL as Qntl,a.Short_Name as Mill,u.Bill_Amount as VocAmount,Year_Code," +
                    " ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS','RR') and t.Tran_Type=u.Tran_Type  and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0) as Recieved," +
                    " (u.Bill_Amount-ISNULL((Select SUM(t.Value) as Recieved from multiple_receipt_detail  t where t.Tran_Type IN('BR','CR','AB','CN','DN','RS','RR') and t.Tran_Type=u.Tran_Type  and t.Bill_No = u.doc_no and t.Bill_Auto_Id = u.saleid and t.Year_Code=u.Year_Code),0)) as Short,u.BROKER,u.ASN from " + tblPrefix + "qryVoucherSaleUnion1 u" +
                    " left outer join " + tblPrefix + "AccountMaster a on u.mill_code=a.Ac_Code and u.Company_Code=a.Company_Code where u.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + "  and u.doc_date between '" + from + "' and '" + to +
                    "' order by u.doc_date";
                lblBrokerName.Text = "All Short Payment Details Detailed Report";
            }
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("dt", typeof(string)));
                    dt.Columns.Add(new DataColumn("ttype", typeof(string)));
                    dt.Columns.Add(new DataColumn("#", typeof(string)));
                    dt.Columns.Add(new DataColumn("Party", typeof(string)));
                    dt.Columns.Add(new DataColumn("lorry", typeof(string)));
                    dt.Columns.Add(new DataColumn("Qntl", typeof(double)));
                    dt.Columns.Add(new DataColumn("Mill", typeof(string)));
                    dt.Columns.Add(new DataColumn("VocAmount", typeof(double)));
                    dt.Columns.Add(new DataColumn("Recieved", typeof(double)));
                    dt.Columns.Add(new DataColumn("Short", typeof(double)));
                    dt.Columns.Add(new DataColumn("ASN", typeof(string)));
                    dt.Columns.Add(new DataColumn("PODetails", typeof(string)));
                    dt.Columns.Add(new DataColumn("Year_Code", typeof(string)));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["dt"] = ds.Tables[0].Rows[i]["dt"].ToString();
                        string tran_type = ds.Tables[0].Rows[i]["ttype"].ToString();
                        string doc_no = ds.Tables[0].Rows[i]["#"].ToString();
                        dr["#"] = doc_no;
                        if (tran_type == "AB")
                        {
                            dr["ttype"] = "RS";
                            tran_type = "RS";

                        }
                        else
                        {
                            dr["ttype"] = tran_type;

                        }
                        if (tran_type == "OV")
                        {
                            string PODetails = clsCommon.getString("select c.pono as podetail from " + tblPrefix + "deliveryorder d left outer join " +
                                           " carporatehead c on d.Carporate_Sale_No=c.Doc_No and d.company_code=c.Company_Code  where d.Carporate_Sale_No!=0 and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and d.voucher_type='OV' and d.voucher_no=" + doc_no + "");
                            dr["PODetails"] = PODetails;
                        }
                        if (tran_type == "SB")
                        {
                            string PODetails = clsCommon.getString("select c.pono as podetail from " + tblPrefix + "deliveryorder d left outer join " +
                                           " carporatehead c on d.Carporate_Sale_No=c.Doc_No and d.company_code=c.Company_Code  where d.Carporate_Sale_No!=0 and d.company_code=" 
                                           + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.SB_No=" + doc_no + " and d.Year_Code=" 
                                           + Convert.ToInt32(Session["year"].ToString()));
                            dr["PODetails"] = PODetails;
                        }
                        if (tran_type == "RS")
                        {
                            //string PODetails = clsCommon.getString("select PO_Details from nt_1_sugarsalereturn where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            //    + " and Year_code=" + Session["year"].ToString() + " and doc_no=" + doc_no + "");
                            //dr["PODetails"] = PODetails;
                            string PODetails = clsCommon.getString("select PO_Details from nt_1_sugarsalereturn where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                               + " and doc_no=" + doc_no + "");
                            dr["PODetails"] = PODetails;
                        }
                        dr["Party"] = ds.Tables[0].Rows[i]["Party"].ToString();
                        dr["lorry"] = ds.Tables[0].Rows[i]["lorry"].ToString();
                        dr["Qntl"] = ds.Tables[0].Rows[i]["Qntl"].ToString();
                        dr["Mill"] = ds.Tables[0].Rows[i]["Mill"].ToString();
                        dr["VocAmount"] = ds.Tables[0].Rows[i]["VocAmount"].ToString();
                        dr["Recieved"] = ds.Tables[0].Rows[i]["Recieved"].ToString();
                        double shortpayment = Convert.ToDouble(ds.Tables[0].Rows[i]["Short"].ToString());
                        dr["Short"] = shortpayment;
                        dr["ASN"] = ds.Tables[0].Rows[i]["ASN"].ToString();
                        dr["Year_Code"] = ds.Tables[0].Rows[i]["Year_Code"].ToString();
                        if (shortpayment != 0)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        //lblQntlTotal.Text = Convert.ToString(dt.Compute("SUM(Qntl)", string.Empty));
                        //lblAmountTotal.Text = Convert.ToString(dt.Compute("SUM(VocAmount)", string.Empty));
                        //lblRecievedTotal.Text = Convert.ToString(dt.Compute("SUM(Recieved)", string.Empty));
                        //lblShortTotal.Text = Convert.ToString(dt.Compute("SUM(Short)", string.Empty));
                        dtlDetails.DataSource = dt;
                        dtlDetails.DataBind();
                    }
                }
            }
            else
            {
                dtlDetails.DataSource = null;
                dtlDetails.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void dtlDetails_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList detailsDtl = (DataList)e.Item.FindControl("detailsDtl");
            Label lblTtype = (Label)e.Item.FindControl("lblTtype");
            Label lblno = (Label)e.Item.FindControl("lblno");
            Label lblAmount1 = (Label)e.Item.FindControl("lblAmount1");
            Label lblRecieved1 = (Label)e.Item.FindControl("lblRecieved1");
            Label lblShort1 = (Label)e.Item.FindControl("lblShort1");
            Label lblYear_Code = (Label)e.Item.FindControl("lblYear_Code1");
            string Vouc_Type = lblTtype.Text;
            string Vouc_No = lblno.Text;
            string yearCode = lblYear_Code.Text;
            qry = " select #,Ptype,tran_date,PayAmt,narration from " + tblPrefix + "qryLedgerShortPayment where Voucher_No=" + Vouc_No + " and Voucher_Type='" + Vouc_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + yearCode + "";
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    double voucAmount = Convert.ToDouble(lblAmount1.Text);
                    double totalrecieved = Convert.ToDouble(dt.Compute("SUM(PayAmt)", string.Empty));
                    lblRecieved1.Text = totalrecieved.ToString();
                    lblShort1.Text = Convert.ToString(Math.Round((voucAmount - totalrecieved), 2));
                    detailsDtl.DataSource = dt;
                    detailsDtl.DataBind();
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
        try
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
        catch (Exception)
        {
            throw;
        }
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
                msg.Body = "Broker Wise Short Payment With Details Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Broker Wise Short Payment With Details " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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