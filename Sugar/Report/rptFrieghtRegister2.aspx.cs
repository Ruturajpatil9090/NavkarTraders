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

public partial class Report_rptFrieghtRegister2 : System.Web.UI.Page
{
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    string f = "../GSReports/FrieghtRegister_.htm";
    string f_Main = "../Report/FrieghtRegister_";
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {
            lblCompanyName.Text = Session["Company_Name"].ToString();
            BindList();
        }
    }
    protected void BindList()
    {
        if (string.IsNullOrEmpty(Branch_Code))
        {
            qry = "select distinct(Convert(varchar(10),doc_date,103)) As Memo_Date,doc_date as date from " + tblPrefix + "deliveryorder where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_date between '" + fromDT + "' and '" + toDT + "' order by date";
        }
        else
        {
            qry = "select distinct(Convert(varchar(10),doc_date,103)) As Memo_Date,doc_date as date from " + tblPrefix + "deliveryorder where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Branch_Code=" + Branch_Code + " and tran_type='DO' and doc_date between '" + fromDT + "' and '" + toDT + "' order by date";
        }
        ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dtl.DataSource = dt;
                    dtl.DataBind();
                }
            }
        }
    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        #region
        //DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        //Label lblMemoDate = e.Item.FindControl("lblDate") as Label;
        //string dte = lblMemoDate.Text;
        //string date = DateTime.Parse(dte, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //if (string.IsNullOrEmpty(Branch_Code))
        //{
        //    qry = "select d.doc_no as Memo_No,d.vasuli_amount,d.GETPASSCODE,ISNULL(d.SB_No,0) as SB,a.Ac_Name_E +' <br/>'+ a.Mobile_No as Party,b.Short_Name as mill,d.quantal,d.truck_no +' <br/>'+ d.driver_no as Veh_No,Freight_Amount as frieghtAmt,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.FreightPerQtl as frieght,(c.Short_Name + ' (' + CAST(c.Ac_Code as NVARCHAR(max))+')') +'<br/>'+c.Mobile_No  as Transport" +
        //    " from " + tblPrefix + "deliveryorder d  left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code" +
        //    " left outer join " + tblPrefix + "AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code left outer join " + tblPrefix + "AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code  where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and d.doc_date='" + date + "'";
        //}
        //else
        //{
        //    qry = "select d.doc_no as Memo_No,d.vasuli_amount,d.GETPASSCODE,ISNULL(d.SB_No,0) as SB,a.Ac_Name_E +' <br/>'+ a.Mobile_No as Party,b.Short_Name as mill,d.quantal,d.truck_no +' <br/>'+ d.driver_no as Veh_No,Freight_Amount as frieghtAmt,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.FreightPerQtl as frieght,(c.Short_Name + ' (' + CAST(c.Ac_Code as NVARCHAR(max))+')') +'<br/>'+c.Mobile_No as Transport" +
        //    " from " + tblPrefix + "deliveryorder d  left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code" +
        //    " left outer join " + tblPrefix + "AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code left outer join " + tblPrefix + "AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code  where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and tran_type='DO' and d.doc_date='" + date + "'";
        //}
        //ds = new DataSet();
        //ds = clsDAL.SimpleQuery(qry);
        //if (ds != null)
        //{
        //    dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("Memo_No", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Mill", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Party", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Transport", typeof(string)));
        //    dt.Columns.Add(new DataColumn("quantal", typeof(double)));
        //    dt.Columns.Add(new DataColumn("Veh_No", typeof(string)));
        //    dt.Columns.Add(new DataColumn("frieght", typeof(double)));
        //    dt.Columns.Add(new DataColumn("vasuli_amount", typeof(double)));
        //    dt.Columns.Add(new DataColumn("frieghtAmt", typeof(double)));
        //    dt.Columns.Add(new DataColumn("Advance", typeof(double)));
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["Memo_No"] = ds.Tables[0].Rows[i]["Memo_No"].ToString();
        //            dr["Mill"] = ds.Tables[0].Rows[i]["Mill"].ToString();
        //            string self_ac = clsCommon.getString("select SELF_AC from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
        //            if (ds.Tables[0].Rows[i]["GETPASSCODE"].ToString() == self_ac)
        //            {
        //                string sb_no = ds.Tables[0].Rows[i]["SB"].ToString();
        //                if (sb_no != "0")
        //                {
        //                    string partyname = clsCommon.getString(" select a.Ac_Name_E+' <br>'+a.Mobile_No as Party from " + tblPrefix + "SugarSale s left outer join " + tblPrefix + "AccountMaster a ON s.Unit_Code=a.Ac_Code and s.Company_Code=a.Company_Code" +
        //                                                            " where s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and s.doc_no=" + sb_no + "");

        //                    dr["Party"] = partyname;
        //                }
        //                else
        //                {
        //                    dr["Party"] = "";
        //                }
        //            }
        //            else
        //            {
        //                dr["Party"] = ds.Tables[0].Rows[i]["Party"].ToString();
        //            }

        //            dr["Transport"] = ds.Tables[0].Rows[i]["Transport"].ToString();
        //            string qtl = ds.Tables[0].Rows[i]["quantal"].ToString();
        //            dr["quantal"] = Convert.ToString(double.Parse(qtl));
        //            dr["Veh_No"] = ds.Tables[0].Rows[i]["Veh_No"].ToString();
        //            dr["frieght"] = ds.Tables[0].Rows[i]["frieght"].ToString();
        //            dr["vasuli_amount"] = ds.Tables[0].Rows[i]["vasuli_amount"].ToString();
        //            dr["frieghtAmt"] = ds.Tables[0].Rows[i]["frieghtAmt"].ToString();
        //            double advance = Convert.ToDouble(ds.Tables[0].Rows[i]["Advance"].ToString());
        //            dr["Advance"] = advance;
        //            if (advance != 0)
        //            {
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //        Label lblqtltotal = (Label)e.Item.FindControl("lblqtltotal");
        //        Label lblfrighttotal = (Label)e.Item.FindControl("lblfrieghttotal");
        //        Label lblfrightAmttotal = (Label)e.Item.FindControl("lblfrightAmttotal");
        //        Label lblfrightpaidtotal = (Label)e.Item.FindControl("lblfrightpaidtotal");
        //        Label lbladvtotal = (Label)e.Item.FindControl("lbladvtotal");
        //        lblqtltotal.Text = Convert.ToString(dt.Compute("SUM(quantal)", string.Empty));
        //        lblfrighttotal.Text = Convert.ToString(dt.Compute("SUM(frieght)", string.Empty));
        //        lblfrightAmttotal.Text = Convert.ToString(dt.Compute("SUM(frieghtAmt)", string.Empty));
        //        lblfrightpaidtotal.Text = Convert.ToString(dt.Compute("SUM(vasuli_amount)", string.Empty));
        //        lbladvtotal.Text = Convert.ToString(dt.Compute("SUM(Advance)", string.Empty));
        //        if (dt.Rows.Count > 0)
        //        {
        //            dtlDetails.DataSource = dt;
        //            dtlDetails.DataBind();
        //        }
        //        else
        //        {
        //            dtlDetails.DataSource = null;
        //            dtlDetails.DataBind();
        //        }
        //    }
        //}
        #endregion

        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        Label lblMemoDate = e.Item.FindControl("lblDate") as Label;
        string dte = lblMemoDate.Text;
        string date = DateTime.Parse(dte, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        if (string.IsNullOrEmpty(Branch_Code))
        {
            //qry = "select d.doc_no as Memo_No,d.vasuli_amount,d.GETPASSCODE,ISNULL(d.SB_No,0) as SB,a.Ac_Name_E +' <br/>'+ a.Mobile_No as Party,b.Short_Name as mill,d.quantal,d.truck_no +' <br/>'+ d.driver_no as Veh_No,Freight_Amount as frieghtAmt,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.FreightPerQtl as frieght,(c.Short_Name + ' (' + CAST(c.Ac_Code as NVARCHAR(max))+')') +'<br/>'+c.Mobile_No  as Transport" +
            //" from " + tblPrefix + "deliveryorder d  left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code" +
            //" left outer join " + tblPrefix + "AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code left outer join " + tblPrefix + "AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code  where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and d.doc_date='" + date + "'";

            qry = "select (Convert(varchar(10),d.doc_date,103)) as date,d.doc_no as Memo_No,d.vasuli_amount,d.GETPASSCODE,ISNULL(d.SB_No,0) as SB,a.Ac_Name_E +' <br/>'+ a.Mobile_No as Party,b.Short_Name as mill,d.quantal," +
                " d.truck_no  as Veh_No,Freight_Amount as frieghtAmt,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.FreightPerQtl as frieght," +
                " (c.Short_Name + ' (' + CAST(c.Ac_Code as NVARCHAR(max))+')') as Transport,c.Mobile_No as TransportMob,d.narration4,v.Ac_Name_E as SHIP_TO,d.driver_no,'0.00' as Balance,d.TDSAmt,d.Pan_No from NT_1_deliveryorder d " +
                " left outer join NT_1_AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code left outer join NT_1_AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code " +
                " left outer join NT_1_AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code left outer join NT_1_AccountMaster v on d.voucher_by=v.Ac_Code AND d.company_code=c.Company_Code " +
                " where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and d.doc_date='" + date + "' and d.purc_no!=0";
        }
        else
        {
            //qry = "select d.doc_no as Memo_No,d.vasuli_amount,d.GETPASSCODE,ISNULL(d.SB_No,0) as SB,a.Ac_Name_E +' <br/>'+ a.Mobile_No as Party,b.Short_Name as mill,d.quantal,d.truck_no +' <br/>'+ d.driver_no as Veh_No,Freight_Amount as frieghtAmt,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.FreightPerQtl as frieght,(c.Short_Name + ' (' + CAST(c.Ac_Code as NVARCHAR(max))+')') +'<br/>'+c.Mobile_No as Transport" +
            //" from " + tblPrefix + "deliveryorder d  left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code" +
            //" left outer join " + tblPrefix + "AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code left outer join " + tblPrefix + "AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code  where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and tran_type='DO' and d.doc_date='" + date + "'";

            qry = "select (Convert(varchar(10),d.doc_date,103)) as date,d.doc_no as Memo_No,d.vasuli_amount,d.GETPASSCODE,ISNULL(d.SB_No,0) as SB,a.Ac_Name_E +' <br/>'+ a.Mobile_No as Party,b.Short_Name as mill,d.quantal," +
                  " d.truck_no  as Veh_No,Freight_Amount as frieghtAmt,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.FreightPerQtl as frieght," +
                  " (c.Short_Name + ' (' + CAST(c.Ac_Code as NVARCHAR(max))+')') as Transport,c.Mobile_No as TransportMob,d.narration4,v.Ac_Name_E as SHIP_TO,d.driver_no,'0.00' as Balance,d.TDSAmt,d.Pan_No from NT_1_deliveryorder d " +
                  " left outer join NT_1_AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code left outer join NT_1_AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code " +
                  " left outer join NT_1_AccountMaster c on d.transport=c.Ac_Code AND d.company_code=c.Company_Code left outer join NT_1_AccountMaster v on d.voucher_by=v.Ac_Code AND d.company_code=c.Company_Code " +
                  " where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and tran_type='DO' and d.doc_date='" + date + "' and d.purc_no!=0";
        }
        ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("date", typeof(string)));
            dt.Columns.Add(new DataColumn("Memo_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Mill", typeof(string)));
            //dt.Columns.Add(new DataColumn("Party", typeof(string)));
            dt.Columns.Add(new DataColumn("SB", typeof(string)));
            dt.Columns.Add(new DataColumn("narration4", typeof(string)));
            dt.Columns.Add(new DataColumn("SHIP_TO", typeof(string)));
            dt.Columns.Add(new DataColumn("Transport", typeof(string)));
            dt.Columns.Add(new DataColumn("TransportMob", typeof(string)));
            dt.Columns.Add(new DataColumn("Veh_No", typeof(string)));
            dt.Columns.Add(new DataColumn("driver_no", typeof(string)));
            dt.Columns.Add(new DataColumn("quantal", typeof(double)));

            dt.Columns.Add(new DataColumn("frieght", typeof(double)));
            dt.Columns.Add(new DataColumn("vasuli_amount", typeof(double)));
            dt.Columns.Add(new DataColumn("frieghtAmt", typeof(double)));
            dt.Columns.Add(new DataColumn("Advance", typeof(double)));
            dt.Columns.Add(new DataColumn("Balance", typeof(double)));
            dt.Columns.Add(new DataColumn("TDSAmt", typeof(double)));
            dt.Columns.Add(new DataColumn("Pan_No", typeof(string)));

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["date"] = ds.Tables[0].Rows[i]["date"].ToString();
                    dr["Memo_No"] = ds.Tables[0].Rows[i]["Memo_No"].ToString();
                    dr["Mill"] = ds.Tables[0].Rows[i]["Mill"].ToString();
                    dr["SB"] = ds.Tables[0].Rows[i]["SB"].ToString();
                    dr["narration4"] = ds.Tables[0].Rows[i]["narration4"].ToString();
                    dr["SHIP_TO"] = ds.Tables[0].Rows[i]["SHIP_TO"].ToString();
                    dr["Transport"] = ds.Tables[0].Rows[i]["Transport"].ToString();
                    dr["TransportMob"] = ds.Tables[0].Rows[i]["TransportMob"].ToString();
                    dr["Veh_No"] = ds.Tables[0].Rows[i]["Veh_No"].ToString();
                    dr["driver_no"] = ds.Tables[0].Rows[i]["driver_no"].ToString();
                    string qtl = ds.Tables[0].Rows[i]["quantal"].ToString();
                    dr["quantal"] = Convert.ToString(double.Parse(qtl));

                    dr["frieght"] = ds.Tables[0].Rows[i]["frieght"].ToString();
                    dr["vasuli_amount"] = ds.Tables[0].Rows[i]["vasuli_amount"].ToString();
                    dr["frieghtAmt"] = ds.Tables[0].Rows[i]["frieghtAmt"].ToString();
                    double frightamt = Convert.ToDouble(ds.Tables[0].Rows[i]["frieghtAmt"].ToString());
                    double advance = Convert.ToDouble(ds.Tables[0].Rows[i]["Advance"].ToString());
                    double bal = frightamt - advance;
                    dr["Balance"] = bal;
                    dr["Advance"] = advance;
                    dr["TDSAmt"] = ds.Tables[0].Rows[i]["TDSAmt"].ToString();
                    dr["Pan_No"] = ds.Tables[0].Rows[i]["Pan_No"].ToString();

                    if (advance != 0)
                    {
                        dt.Rows.Add(dr);
                    }

                }
                Label lblqtltotal = (Label)e.Item.FindControl("lblqtltotal");
                Label lblfrighttotal = (Label)e.Item.FindControl("lblfrieghttotal");
                Label lblfrightAmttotal = (Label)e.Item.FindControl("lblfrightAmttotal");
                Label lblfrightpaidtotal = (Label)e.Item.FindControl("lblfrightpaidtotal");
                Label lbladvtotal = (Label)e.Item.FindControl("lbladvtotal");
                Label lblbaltotal = (Label)e.Item.FindControl("lblbaltotal");
                lblqtltotal.Text = Convert.ToString(dt.Compute("SUM(quantal)", string.Empty));
                lblfrighttotal.Text = Convert.ToString(dt.Compute("SUM(frieght)", string.Empty));
                lblfrightAmttotal.Text = Convert.ToString(dt.Compute("SUM(frieghtAmt)", string.Empty));
                lblfrightpaidtotal.Text = Convert.ToString(dt.Compute("SUM(vasuli_amount)", string.Empty));
                lbladvtotal.Text = Convert.ToString(dt.Compute("SUM(Advance)", string.Empty));
                lblbaltotal.Text = Convert.ToString(dt.Compute("SUM(Advance)", string.Empty));
                if (dt.Rows.Count > 0)
                {
                    dtlDetails.DataSource = dt;
                    dtlDetails.DataBind();
                }
                else
                {
                    dtlDetails.DataSource = null;
                    dtlDetails.DataBind();
                }
            }
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
                msg.Body = "Frieght Register Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Frieght Register Report " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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
    protected void dtl_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}