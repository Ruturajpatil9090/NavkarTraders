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
using System.Globalization;

public partial class Report_rptDoWiseDispatch : System.Web.UI.Page
{
    string Mill_Code = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    string Tender_No = string.Empty;
    DataSet ds;
    DataTable dt;
    string tblPrefix = string.Empty;
    string f = "../GSReports/DOWiseDisp_.htm";
    string f_Main = "../Report/DOWiseDisp_";
    string Branch_Code = string.Empty;
    string qntltotal = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                lblCompanyName.Text = Session["Company_Name"].ToString();
                this.BindData();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }

    private void BindData()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{

       //     qry = "select doc_no ,doc_dateConverted as dodate,millShortName as millShortName,voucherbyshortname as VocBy,getpassshortname as GetPass,driver_no,SB_No,isnull(SaleBillTo,0) as SaleBillTo" +
       //             ",mill_rate as MR,quantal as Qntl,sale_rate as SR,Tender_Commission as Tender_Commission,truck_no as lorry,FreightPerQtl as frt,vasuli_rate1 AS vasuli,vasuli_amount1 as vasuliamount,transportname as transport,vasuliacname as Vasuli_Ac_Name," +
        //            " doname as do,purc_no as tender,LoadingSms from  qrydohead " +
        //            " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromDT + "' and '" + toDT + "' and  tran_type='DO' and purc_no!=0 order by doc_no asc";
            qry = "SELECT     dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103)  AS dodate, mill.Short_Name AS millShortName, vb.Short_Name AS VocBy, gp.Short_Name AS GetPass, dbo.nt_1_deliveryorder.driver_no, dbo.nt_1_deliveryorder.SB_No,isnull( dbo.nt_1_deliveryorder.SaleBillTo,0) as SaleBillTo"  
                        + ", dbo.nt_1_deliveryorder.mill_rate AS MR, dbo.nt_1_deliveryorder.quantal AS Qntl, dbo.nt_1_deliveryorder.sale_rate AS SR, Tender_Commission as Tender_Commission,dbo.nt_1_deliveryorder.truck_no AS lorry, "
                        + " dbo.nt_1_deliveryorder.FreightPerQtl AS frt,dbo.nt_1_deliveryorder.MM_Rate, dbo.nt_1_deliveryorder.vasuli_rate1 AS vasuli, tc.Short_Name AS transport, nt_1_accountmaster_1.Short_Name AS do, "
                        + " dbo.nt_1_deliveryorder.purc_no AS tender, vasuli_amount1 as vasuliamount,vasuli.Ac_Name_E AS  Vasuli_Ac_Name, dbo.nt_1_deliveryorder.LoadingSms, transport as trancode  "
                        + " FROM         dbo.nt_1_deliveryorder LEFT OUTER JOIN "
                        + " dbo.nt_1_accountmaster AS vasuli ON dbo.nt_1_deliveryorder.va = vasuli.accoid LEFT OUTER JOIN "
                        + " dbo.nt_1_accountmaster AS nt_1_accountmaster_1 ON dbo.nt_1_deliveryorder.docd = nt_1_accountmaster_1.accoid LEFT OUTER JOIN "
                        + " dbo.nt_1_accountmaster AS tc ON dbo.nt_1_deliveryorder.tc = tc.accoid LEFT OUTER JOIN "
                        + " dbo.nt_1_accountmaster AS gp ON dbo.nt_1_deliveryorder.gp = gp.accoid LEFT OUTER JOIN "
                        + " dbo.nt_1_accountmaster AS vb ON dbo.nt_1_deliveryorder.vb = vb.accoid LEFT OUTER JOIN "
                        + " dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid "
                        + " where dbo.nt_1_deliveryorder.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and dbo.nt_1_deliveryorder.doc_date between '" + fromDT + "' and '" + toDT +
                        "' and dbo.nt_1_deliveryorder.purc_no!=0 and  dbo.nt_1_deliveryorder.tran_type='DO'  order by dbo.nt_1_deliveryorder.doc_no asc";

     
            ds = new DataSet();
            //ds = obj.GetDataSet(qry);
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("#", typeof(string)));
                dt.Columns.Add(new DataColumn("dodate", typeof(string)));
                dt.Columns.Add(new DataColumn("millShortName", typeof(string)));
                dt.Columns.Add(new DataColumn("VocBy", typeof(string)));
                dt.Columns.Add(new DataColumn("GetPass", typeof(string)));
                dt.Columns.Add(new DataColumn("BillToName", typeof(string)));
                dt.Columns.Add(new DataColumn("driver_no", typeof(string)));
                dt.Columns.Add(new DataColumn("SB_No", typeof(string)));

                dt.Columns.Add(new DataColumn("MR", typeof(string)));
                dt.Columns.Add(new DataColumn("Qntl", typeof(double)));
                dt.Columns.Add(new DataColumn("SR", typeof(string)));
                dt.Columns.Add(new DataColumn("Tender_Commission", typeof(double)));
                dt.Columns.Add(new DataColumn("lorry", typeof(string)));
                dt.Columns.Add(new DataColumn("frt", typeof(string)));
                dt.Columns.Add(new DataColumn("MM_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("vasuli", typeof(string)));
                //dt.Columns.Add(new DataColumn("Vasuli_Ac_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("transport", typeof(string)));
                dt.Columns.Add(new DataColumn("do", typeof(string)));
                dt.Columns.Add(new DataColumn("tender", typeof(string)));
                dt.Columns.Add(new DataColumn("LoadingSms", typeof(string)));
                dt.Columns.Add(new DataColumn("vasuliamount", typeof(double)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["#"] = ds.Tables[0].Rows[i]["doc_no"].ToString();
                    dr["dodate"] = ds.Tables[0].Rows[i]["dodate"].ToString();
                    dr["millShortName"] = ds.Tables[0].Rows[i]["millShortName"].ToString();
                    dr["VocBy"] = ds.Tables[0].Rows[i]["VocBy"].ToString();
                    dr["GetPass"] = ds.Tables[0].Rows[i]["GetPass"].ToString();
                    string SaleBillTo = ds.Tables[0].Rows[i]["SaleBillTo"].ToString();
                    if (SaleBillTo != "0")
                    {
                        dr["BillToName"] = clsCommon.getString("select Short_Name from " + tblPrefix + "AccountMaster where Ac_Code=" + SaleBillTo + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    }
                    else
                    {
                        dr["BillToName"] = "";
                    }

                    dr["driver_no"] = ds.Tables[0].Rows[i]["driver_no"].ToString();
                    dr["SB_No"] = ds.Tables[0].Rows[i]["SB_No"].ToString();
                    dr["MR"] = ds.Tables[0].Rows[i]["MR"].ToString();
                    dr["Qntl"] = ds.Tables[0].Rows[i]["Qntl"].ToString();
                    dr["SR"] = ds.Tables[0].Rows[i]["SR"].ToString();
                    dr["lorry"] = ds.Tables[0].Rows[i]["lorry"].ToString();
                    dr["frt"] = ds.Tables[0].Rows[i]["frt"].ToString();
                    dr["MM_Rate"] = ds.Tables[0].Rows[i]["MM_Rate"].ToString();
                    dr["vasuli"] = ds.Tables[0].Rows[i]["vasuli"].ToString();
                    //dr["Vasuli_Ac_Name"] = ds.Tables[0].Rows[i]["Vasuli_Ac_Name"].ToString();
                    dr["transport"] = ds.Tables[0].Rows[i]["transport"].ToString();
                    dr["do"] = ds.Tables[0].Rows[i]["do"].ToString();
                    dr["tender"] = ds.Tables[0].Rows[i]["tender"].ToString();
                    dr["LoadingSms"] = ds.Tables[0].Rows[i]["LoadingSms"].ToString();
                    dr["vasuliamount"] = ds.Tables[0].Rows[i]["vasuliamount"].ToString();
                    dr["Tender_Commission"] = ds.Tables[0].Rows[i]["Tender_Commission"].ToString();
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    lblfromtodate.Text = "DO Wise Dispatch Register From  <b>" + fromDT + "</b>  To  <b>" + toDT + "</b>";
                    lblQntlTotala.Text = Convert.ToString(dt.Compute("SUM(Qntl)", string.Empty));
                  
                    lblTotalVasuliAmount.Text = Convert.ToString(dt.Compute("SUM(vasuliamount)", string.Empty));
                    Datalist1.DataSource = dt;
                    Datalist1.DataBind();
                }
                else
                {
                    Datalist1.DataSource = null;
                    Datalist1.DataBind();
                }
            }
            else
            {
                Datalist1.DataSource = null;
                Datalist1.DataBind();
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
    protected void Datalist1_OnItemDataBound(object sender, DataListItemEventArgs e)
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
                msg.Body = "Do Wise Dispatch Report";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Do Wise Dispatch Report " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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

    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkDO = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkDO.NamingContainer;
            string ps = lnkDO.Text;
            Session["DO_NO"] = ps;
            Int16 Action = 1;
            string count = clsDAL.GetString("select doid from nt_1_deliveryorder where doc_no=" + ps + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt16(Session["year"].ToString()) + "");
            string dtt = "01/07/2017";
            string dt11 = "01/07/2017";
           // string dt11 = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-BG")).ToString("dd/MM/yyyy");
            DateTime dt = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.ParseExact(dt11, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (dt1 < dt)//for old links
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kdjsd", "javascript:DO();", true);
            }
            else
            {
                Session["DO_NO"] = ps;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "lll", "javascript:Donew('" + count + "','" + Action + "');", true);

            }
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

            Int16 Action = 1;
            string count = clsDAL.GetString("select saleid from nt_1_sugarsale where doc_no=" + no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt16(Session["year"].ToString()) + "");

            string dtt = "01/07/2017";
            string dt11 = "01/07/2017";
            //string dt11 = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-BG")).ToString("dd/MM/yyyy");
            DateTime dt = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.ParseExact(dt11, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string dt11 = grdDetail.Rows[rowIndex].Cells[2].Text.Trim();
            //DateTime dt = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dt1 = DateTime.ParseExact(dt11, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (dt1 < dt)//for old links
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:salebill();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:salebillnew('" + count + "','" + Action + "');", true);
            }

            lnkSB.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }

}