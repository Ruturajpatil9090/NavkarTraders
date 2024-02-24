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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Net.Mime;

public partial class Report_rptCarporateSaleDetail : System.Web.UI.Page
{
    DataSet ds = null;
    DataTable dt = null;
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    string Branch_Code = string.Empty;
    string fromDt = string.Empty;
    string toDt = string.Empty;
    string f = "../GSReports/CarporateSaleDetail_.htm";
    string f_Main = "../Report/CarporateSaleDetail_";
    string PDS = string.Empty;
    string acCode = string.Empty;
    string lot_no = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    DataTable dtabind;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Branch_Code"])))
        {
            Branch_Code = "";
        }
        else
        {
            Branch_Code = Convert.ToString(Request.QueryString["Branch_Code"]);
        }
        acCode = Request.QueryString["ac_code"];
        fromDt = Request.QueryString["fromDt"];
        toDt = Request.QueryString["toDt"];
        PDS = Request.QueryString["PDS"];
        lot_no = Request.QueryString["lotno"];
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

    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkDO = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkDO.NamingContainer;
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;
            string accode = lnkDO.Text;

            Int16 Action = 1;
            Int32 counts = Convert.ToInt32(clsCommon.getString("SELECT doid  from nt_1_deliveryorder where doc_no=" + accode + "  "));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + counts + "','" + Action + "')", true);
            lnkDO.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void BindList()
    {
        try
        {
            if (acCode != string.Empty && acCode != null)
            {


                if (string.IsNullOrEmpty(lot_no))
                {
                    qry = "select distinct doc_no  as CSNO,carporatepartyaccountname as CSName,doc_dateConverted as CSDate ,carporatepartyunitname as CSUnitName,"
                       + " sell_rate as CSSaleRate,quantal as CSQntl,  isnull(sum(doqntl),0) as CSDesp,pono as CSPodetails FROM qrycarporatedodetail "
                         + " where Ac_Code=" + acCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " "
                        + " and Doc_Date between '" + fromDt + "' and '" +
                        toDt + "' and selling_type='" + PDS + "' group by doc_no,carporatepartyaccountname,doc_dateConverted,carporatepartyunitname,sell_rate,quantal,pono order by CSName asc";
                }
                else
                {
                    qry = "select distinct doc_no  as CSNO,carporatepartyaccountname as CSName,doc_dateConverted as CSDate ,carporatepartyunitname as CSUnitName,"
                       + " sell_rate as CSSaleRate,quantal as CSQntl, isnull(sum(doqntl),0) as CSDesp ,pono as CSPodetails FROM qrycarporatedodetail " +
                          " where Ac_Code=" + acCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " And Doc_Date between '" + fromDt + "' and '" + toDt + "' and selling_type='" + PDS + "'  and Doc_No='" + lot_no + "'  group by doc_no,carporatepartyaccountname,doc_dateConverted,carporatepartyunitname,sell_rate,quantal,pono order by CSName asc";
                }


            }
            else
            {

                if (string.IsNullOrEmpty(lot_no))
                {
                    qry = "select distinct doc_no  as CSNO,carporatepartyaccountname as CSName,doc_dateConverted as CSDate ,carporatepartyunitname as CSUnitName,"
                        + " sell_rate as CSSaleRate,quantal as CSQntl, isnull(sum(doqntl),0) as CSDesp,pono as CSPodetails FROM qrycarporatedodetail "
                          + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " "
                         + " and Doc_Date between '" + fromDt + "' and '" + toDt + "' and selling_type='" + PDS + "' group by doc_no,carporatepartyaccountname,doc_dateConverted,carporatepartyunitname,sell_rate,quantal,pono order by CSName asc";
                }
                else
                {
                    qry = "select distinct doc_no  as CSNO,carporatepartyaccountname as CSName,doc_dateConverted as CSDate ,carporatepartyunitname as CSUnitName,"
                      + " isnull(sell_rate) as CSSaleRate,quantal as CSQntl, isnull(sum(doqntl),0) as CSDesp,pono as CSPodetails FROM qrycarporatedodetail " +
                         " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                         " And Doc_Date between '" + fromDt + "' and '" + toDt + "' and selling_type='" + PDS +
                         "' and Doc_No='" + lot_no + "' group by doc_no,carporatepartyaccountname,doc_dateConverted,carporatepartyunitname,sell_rate,quantal,pono order by CSName asc,sale_rate IS NULL";
                }


            }

     //       string qry1 = " SELECT distinct doc_no AS DONo, convert(varchar(30),dodate,103) AS DODate, doqntl AS DODesp, millshortname AS DOMill, remark, carporatepartyaccountname, "
     //+ " truck_no AS DOLorryNo, Freight_Amount AS DOFrt, transportname AS DOTransport, salebillname AS DOGetpass," +
     //"voucher_no AS VN,  voucher_type AS VT,  dono AS MM, SB_No AS SB, shiptoname AS shiptoshortname, vasuli_amount + FreightPerQtl AS Addition,carpid,dono as dispatchno,ASN_No " +
     // "FROM qrycarporatedodetail  where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_date between '" + fromDt + "' and '" + toDt +
     // "' and Carporate_Sale_No!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
           
            string qry1 = " SELECT distinct doc_no AS DONo, convert(varchar(30),dodate,103) AS DODate, doqntl AS DODesp, millshortname AS DOMill, remark, carporatepartyaccountname, "
   + " truck_no AS DOLorryNo, Freight_Amount AS DOFrt, transportname AS DOTransport, salebillname AS DOGetpass," +
   "voucher_no AS VN,  voucher_type AS VT,  dono AS MM, SB_No AS SB, shiptoname AS shiptoshortname, vasuli_amount + FreightPerQtl AS Addition,carpid,dono as dispatchno,ASN_No " +
    "FROM qrycarporatedodetail  where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_date between '" + fromDt + "' and '" + toDt +
    "' and Carporate_Sale_No!=0 ";
            DataSet ds1 = clsDAL.SimpleQuery(qry1);
            dtabind = ds1.Tables[0];


            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("CSNO", typeof(string)));
                dt.Columns.Add(new DataColumn("CSName", typeof(string)));
                dt.Columns.Add(new DataColumn("CSDate", typeof(string)));
                dt.Columns.Add(new DataColumn("CSUnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("CSSaleRate", typeof(string)));
                dt.Columns.Add(new DataColumn("CSQntl", typeof(double)));
                dt.Columns.Add(new DataColumn("CSDesp", typeof(double)));
                dt.Columns.Add(new DataColumn("CSBalance", typeof(double)));
                dt.Columns.Add(new DataColumn("CSPodetails", typeof(string)));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["CSNO"] = ds.Tables[0].Rows[i]["CSNO"].ToString();
                        dr["CSName"] = ds.Tables[0].Rows[i]["CSName"].ToString();
                        dr["CSDate"] = ds.Tables[0].Rows[i]["CSDate"].ToString();
                        dr["CSUnitName"] = ds.Tables[0].Rows[i]["CSUnitName"].ToString();
                        dr["CSSaleRate"] = ds.Tables[0].Rows[i]["CSSaleRate"].ToString();
                        double Qntl = Convert.ToDouble(ds.Tables[0].Rows[i]["CSQntl"].ToString());
                        dr["CSQntl"] = Qntl;
                        double desp = Convert.ToDouble(ds.Tables[0].Rows[i]["CSDesp"].ToString());
                        dr["CSDesp"] = desp;
                        double balance = Qntl - desp;
                        dr["CSBalance"] = balance;
                        dr["CSPodetails"] = ds.Tables[0].Rows[i]["CSPodetails"].ToString();
                        //if (balance != 0)
                        //{
                        dt.Rows.Add(dr);
                        //}
                    }
                    if (dt.Rows.Count > 0)
                    {
                        dtlist.DataSource = dt;
                        dtlist.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void dtlist_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtlDetails = e.Item.FindControl("dtlDetails") as DataList;
            Label lblCSNo = e.Item.FindControl("lblCSNo") as Label;
            string carporatesaleno = lblCSNo.Text.ToString();

            DataView view1 = new DataView(dtabind, "DONo='" + lblCSNo.Text + "'", "DONo", DataViewRowState.CurrentRows);

            DataTable dtAcData = view1.ToTable(true, "DONo", "DODate", "DODesp", "DOMill", "carporatepartyaccountname", "DOLorryNo", "DOFrt", "DOTransport", "DOGetpass",
                "VN", "VT", "MM", "SB", "shiptoshortname", "Addition", "carpid", "dispatchno", "ASN_No");




            if (dtAcData.Rows.Count > 0)
            {

                dtAcData.Columns.Add(new DataColumn("PS", typeof(string)));
                dtAcData.Columns.Add(new DataColumn("VO", typeof(string)));
                for (int i = 0; i < dtAcData.Rows.Count; i++)
                {
                    string VN = dtAcData.Rows[i]["VN"].ToString();
                    string VT = dtAcData.Rows[i]["VT"].ToString();
                    string SB = dtAcData.Rows[i]["SB"].ToString();

                    if (VT == "PS")
                    {
                        dtAcData.Rows[i]["PS"] = VN;
                    }
                    else
                    {
                        dtAcData.Rows[i]["VO"] = VN;

                    }
                    if (!string.IsNullOrEmpty(SB))
                    {
                        dtAcData.Rows[i]["DOGetpass"] = dtAcData.Rows[i]["carporatepartyaccountname"].ToString();
                        dtAcData.Rows[i]["SB"] = SB;

                    }
                }
                dt = new DataTable();
                dt = dtAcData;
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
    protected void lnkPS_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkPS = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkPS.NamingContainer;
            Label Label8 = (Label)item.FindControl("Label8");
            string ps = lnkPS.Text;
            Session["PURC_NO"] = ps;
            Int32 counts;
            counts = Convert.ToInt32(clsCommon.getString("SELECT purchaseid as purchaseid from  nt_1_sugarpurchase where doc_no=" + ps + "  "));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:sugarpurchase('" + counts + "','1');", true);
            lnkPS.Focus();
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
            Int32 counts;
            counts = Convert.ToInt32(clsCommon.getString("SELECT saleid as saleid from  nt_1_sugarsale where doc_no=" + no + "  "));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:salebill('" + counts + "','1');", true);
            lnkSB.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    //protected void lnkOV_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnkOV = (LinkButton)sender;
    //        DataListItem item = (DataListItem)lnkOV.NamingContainer;
    //        string no = lnkOV.Text;
    //        Session["VOUC_NO"] = no;
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:loadingvoucher();", true);
    //        lnkOV.Focus();
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
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
                msg.Body = "Carporate Sell Details";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Carporate Sell Details " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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

    protected void btnPDf_Click(object sender, EventArgs e)
    {
        //#region[Pdf comment]
        ////try
        ////{
        ////    Response.ContentType = "application/pdf";
        ////    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        ////    Response.Cache.SetCacheability(HttpCacheability.NoCache);

        ////    StringWriter sw = new StringWriter();
        ////    HtmlTextWriter hw = new HtmlTextWriter(sw);
        ////    tblmn.RenderControl(hw);
        ////    string s1 = sw.ToString().Replace("font-size: medium", "font-size: xx-small");
        ////    //StringReader sr = new StringReader(s1.ToString());
        ////    StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

        ////    Document pdfDoc = new Document(iTextSharp.text.PageSize.A4);
        ////    var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        ////    pdfDoc.Open();
        ////    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        ////    pdfDoc.Close();
        ////    Response.Write(pdfDoc);
        ////    Response.End();
        ////}
        ////catch
        ////{
        ////}
        //#endregion

        //string filepath = "";
        //Attachment attachment = null;
        //try
        //{
        //    string email = txtEmail.Text.ToString();

        //    //if (txttransportemilid.Text != string.Empty)
        //    //{
        //    //    email = txtEmail.Text.ToString() + "," + txttransportemilid.Text.ToString();
        //    //    email = email.TrimEnd(',');
        //    //    //email = email.Trim(',');
        //    //}
        //    //else
        //    //{
        //    //    email = txtEmail.Text.ToString();
        //    //    email = email.TrimEnd(',');
        //    //}
        //    Label lblSB_No = (Label)dtlist.Items[0].FindControl("lblSB_No");
        //    string fileName = "SellBill_" + lblSB_No.Text + ".pdf";
        //    filepath = "~/PAN/" + fileName;
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    StringWriter sw1 = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw1);
        //    pnlMain.RenderControl(hw);
        //    string s1 = sw1.ToString().Replace("font-size: medium", "font-size: xx-small");
        //    StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));
        //    Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 15f, 15f, 15f, 250f);
        //    var writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath(filepath), FileMode.Create));
        //    Font tblfont = new Font();
        //    pdfDoc.Open();
        //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //    pdfDoc.Close();

        //    ContentType contentType = new ContentType();
        //    contentType.MediaType = MediaTypeNames.Application.Pdf;
        //    contentType.Name = fileName;
        //    attachment = new Attachment(Server.MapPath(filepath), contentType);

        //    string mailFrom = Session["EmailId"].ToString();
        //    string smtpPort = "587";
        //    string emailPassword = Session["EmailPassword"].ToString();
        //    MailMessage msg = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
        //    SmtpServer.Host = clsGV.Email_Address;
        //    msg.From = new MailAddress(mailFrom);
        //    msg.To.Add(email);
        //    msg.Body = "Sale Bill";
        //    msg.Attachments.Add(attachment);
        //    msg.IsBodyHtml = true;
        //    string lorry = string.Empty;
        //    string millshort = string.Empty;
        //    string party = string.Empty;
        //    if (ViewState["lorry"] != null)
        //    {
        //        lorry = "Lorry:" + ViewState["lorry"].ToString();
        //    }
        //    if (ViewState["millshort"] != null)
        //    {
        //        millshort = "Mill:" + ViewState["millshort"].ToString();
        //    }
        //    if (ViewState["Party_Name"] != null)
        //    {
        //        party = "Getpass:" + ViewState["Party_Name"].ToString();
        //    }
        //    msg.Subject = "Bill No:" + lblSB_No.Text + " " + lorry + " " + millshort + " " + party;
        //    msg.IsBodyHtml = true;
        //    if (smtpPort != string.Empty)
        //    {
        //        SmtpServer.Port = Convert.ToInt32(smtpPort);
        //    }
        //    SmtpServer.EnableSsl = true;
        //    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    SmtpServer.UseDefaultCredentials = false;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
        //    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
        //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
        //        System.Security.Cryptography.X509Certificates.X509Chain chain,
        //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
        //    {
        //        return true;
        //    };
        //    SmtpServer.Send(msg);
        //    attachment.Dispose();
        //    if (File.Exists(Server.MapPath(filepath)))
        //    {
        //        File.Delete(Server.MapPath(filepath));
        //    }
        //}
        //catch (Exception e1)
        //{
        //    //Response.Write("mail err:" + e1);
        //    Response.Write("<script>alert('Error sending Mail');</script>");
        //    return;
        //}
        //finally
        //{
        //    attachment.Dispose();
        //    if (File.Exists(Server.MapPath(filepath)))
        //    {
        //        File.Delete(Server.MapPath(filepath));
        //    }
        //}
        //Response.Write("<script>alert('Mail sent successfully');</script>");


    }


    protected void btnPDfDownload_Click(object sender, EventArgs e)
    {
        try
        {
            //string lorry = string.Empty;
            //if (ViewState["lorry"] != null)
            //{
            //    lorry = "Lorry:" + ViewState["lorry"].ToString();
            //}
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=SB_" + lorry + ".pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            pnlMain.RenderControl(hw);
            string s1 = sw.ToString().Replace("font-size: medium", "font-size: xx-small");
            //StringReader sr = new StringReader(s1.ToString());
            StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

            Document pdfDoc = new Document(iTextSharp.text.PageSize.LEDGER);
            var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            Response.Write(pdfDoc);

            Response.End();
        }
        catch
        {
        }
    }
}