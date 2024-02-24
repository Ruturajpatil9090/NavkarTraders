using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web.Security;
using System.Xml.Linq;
using System.Drawing.Printing;
using System.Net;
using System.Text;
using System.Net.Mail;

public partial class Report_rptNewProfitLoss : System.Web.UI.Page
{
    string f = "../GSReports/SugarStock.htm";
    string f_Main = "../Report/rptSugarBalanceStocks";
    string email = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    int pos;
    PagedDataSource adsource;
    string tblPrefix = string.Empty;
    double buyerGrandTotal = 0.0;
    double SaleRate = 0.0;
    double totSaleRate = 0.0;
    double profit = 0.0;
    double totprofit = 0.0;
    double qtl = 0.0;
    double totQtl = 0.0;
    string millcode = string.Empty;
    string lotno = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            if (!Page.IsPostBack)
            {
                lblCompanyName.Text = Session["Company_Name"].ToString();
                millcode = Request.QueryString["Mill_Code"];
                lotno = Request.QueryString["lotno"];
                fromDT = Request.QueryString["fromDT"].ToString();
                toDT = Request.QueryString["toDT"].ToString();

                Bindata();
            }




        }
        catch (NullReferenceException ne)
        {

        }

    }
    public override void VerifyRenderingInServerForm(Control control) { }
    private void Bindata()
    {
        try
        {
            DataSet ds = new DataSet();
            if (millcode != string.Empty && lotno != string.Empty)
            {
                qry = "select distinct(Tender_No), Tender_Date,Mill_Code,Quantal,Mill_Rate,Grade ,Short_Name" +
                   " from qrytenderprofit  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                   + Convert.ToInt32(Session["year"].ToString()) + " and Mill_Code=" + millcode + " and Tender_Date between '" + fromDT + "' and '" + toDT + "'  and Tender_No=" + lotno
                   + " order by Short_Name,Tender_No";



            }
            else
            {
                qry = "select distinct(Tender_No), Tender_Date,Mill_Code,Quantal,Mill_Rate,Grade ,Short_Name" +
          " from qrytenderprofit  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
          + Convert.ToInt32(Session["year"].ToString()) + "  and Tender_Date between '" + fromDT + "' and '" + toDT + "'  and Mill_Code=" + millcode
          + "  order by Short_Name,Tender_No";

            }
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                }
            }
        }
        catch
        {
        }
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dtl = (DataList)e.Item.FindControl("dtl");
        LinkButton lnktenderNo = (LinkButton)e.Item.FindControl("lbkTenderNo");
        Label lblDispatch1 = (Label)e.Item.FindControl("lblDispatch1");
        Label lblProfitTot = (Label)e.Item.FindControl("lblProfitTot");
        Label Label4 = (Label)e.Item.FindControl("Label4");
        Label lblqtl = (Label)e.Item.FindControl("lblqtl");
        Label lblDoqntal = (Label)e.Item.FindControl("doqntal");
        Label lblMillLot = (Label)e.Item.FindControl("lblMillLot");
        Label lblBalance1 = (Label)e.Item.FindControl("lblBalance1");
       // Label lblQntlGrandTotal = (Label)e.Item.FindControl("lblQntaltotal");
        DataSet ds = new DataSet();
        double totaldes = 0.00;
        string qry = "select distinct dono,  dodate,doqntl,billno, billdate,billto,billtoname,isnull(saleqty,0) as saleqty ,isnull(salerate,0) as salerate,isnull( profit,0) as profit  from qrytenderprofit where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            + " and purc_no=" + lnktenderNo.Text + " and billno!=0 and dodate between '" + fromDT + "' and '" + toDT + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  order by dono";
        //  string qry = "select dono,dodate,doqntl,billno,billdate,billto,billtoname,saleqty,salerate,profit from qryProfitLoss where  purc_no=" + lnktenderNo.Text + "  order by dono";
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("dono", typeof(string)));
            dt.Columns.Add(new DataColumn("dodate", typeof(string)));
            dt.Columns.Add(new DataColumn("doqntl", typeof(double)));
            dt.Columns.Add(new DataColumn("billno", typeof(string)));
            dt.Columns.Add(new DataColumn("billdate", typeof(string)));
            dt.Columns.Add(new DataColumn("billto", typeof(string)));
            dt.Columns.Add(new DataColumn("billtoname", typeof(string)));
            dt.Columns.Add(new DataColumn("saleqty", typeof(double)));
            dt.Columns.Add(new DataColumn("salerate", typeof(double)));
            dt.Columns.Add(new DataColumn("profit", typeof(double)));


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["dono"] = ds.Tables[0].Rows[i]["dono"].ToString();
                    dr["dodate"] = ds.Tables[0].Rows[i]["dodate"].ToString();

                    double buyerQntl = Convert.ToDouble(ds.Tables[0].Rows[i]["doqntl"].ToString());
                    dr["doqntl"] = buyerQntl;
                    dr["billno"] = ds.Tables[0].Rows[i]["billno"].ToString();
                    dr["billdate"] = ds.Tables[0].Rows[i]["billdate"].ToString();
                    dr["billto"] = ds.Tables[0].Rows[i]["billto"].ToString();
                    dr["billtoname"] = ds.Tables[0].Rows[i]["billtoname"].ToString();

                    //double despqty = Convert.ToDouble(ds.Tables[0].Rows[i]["saleqty"].ToString());
                    //dr["saleqty"] = despqty;
                    qtl = Convert.ToDouble(ds.Tables[0].Rows[i]["saleqty"].ToString());
                    dr["saleqty"] = qtl;
                    totQtl += qtl;
                    SaleRate = Convert.ToDouble(ds.Tables[0].Rows[i]["salerate"].ToString());
                    dr["salerate"] =SaleRate;
                    totSaleRate +=SaleRate;
                    profit = Convert.ToDouble(ds.Tables[0].Rows[i]["profit"].ToString());
                    dr["profit"] = profit;
                    totprofit += profit;
                    dt.Rows.Add(dr);


                }
                if (dt.Rows.Count > 0)
                {
                    //lblQntlGrandTotal.Text = buyerGrandTotal.ToString(); //clsCommon.getString("select SUM(Buyer_Quantal) from " + tblPrefix + "qryTenderList where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    //lblBalance1.Text = Convert.ToString(dt.Compute("SUM(balance)", string.Empty));
                   // lblDispatch1.Text = totaldes.ToString();// Convert.ToString(dt.Compute("SUM(despatchqty)", string.Empty));
                    lblDispatch1.Text = Convert.ToString(dt.Compute("SUM(salerate)", string.Empty));
                    lblProfitTot.Text = Convert.ToString(dt.Compute("SUM(profit)", string.Empty));
                    lblqtl.Text = Convert.ToString(dt.Compute("SUM(saleqty)", string.Empty));
                    lblDoqntal.Text = Convert.ToString(dt.Compute("SUM(doqntl)", string.Empty));
                    lbldespatchqty.Text = totSaleRate.ToString();
                    lblbalance.Text = totprofit.ToString();
                    lblQntaltotal.Text = totQtl.ToString();
                    double qntl = Convert.ToDouble(Label4.Text);
                    double millrate = Convert.ToDouble(lblMillLot.Text);
                    double bal = qntl * millrate;
                    lblBalance1.Text = bal.ToString();
                    dtl.DataSource = dt;
                    dtl.DataBind();
                }

            }
        }
    }
    protected void btnMail_Click(object sender, EventArgs e)
    {

        #region pdf code success
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=pankaj.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);

        //pnlMain.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 0, 0, 0, 0);
        //pnlMain.Style.Add("font-size", "10px");



        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();
        #endregion
        try
        {
            email = txtEmail.Text.ToString();
            CreateHtml();
            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "Sugar Stock";
            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            msg.Subject = "Sugar Stock Report  " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.IsBodyHtml = true;
            if (smtpPort != string.Empty)
            {
                SmtpServer.Port = Convert.ToInt32(smtpPort);
            }
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
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

    private void CreateHtml()
    {
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
        catch (Exception ee)
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
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //using (WebClient client = new WebClient())
        //{
        //    client.Headers[HttpRequestHeader.Cookie] =
        //        System.Web.HttpContext.Current.Request.Headers["Cookie"];

        //    string htmlCode = client.DownloadString(HttpContext.Current.Request.Url.AbsoluteUri);
        //   // client.DownloadFile(HttpContext.Current.Request.Url.AbsoluteUri, @"C:\\localfile.html");
        //}

        //WebRequest req = HttpWebRequest.Create("http://google.com");
        //req.Method = "GET";

        //string source;
        //using (StreamReader reader = new StreamReader(req.GetRequestStream())) 
        //{
        //    source = reader.ReadToEnd();
        //}


        WebClient client = new WebClient();

        Stream data = client.OpenRead(HttpContext.Current.Request.Url.AbsoluteUri);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();

        using (FileStream fs = new FileStream("D:\\grid.htm", FileMode.Create))
        {
            using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
            {
                w.WriteLine(s);
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void btnfirst_Click(object sender, EventArgs e)
    //{
    //    pos = 0;
    //    Bindata();
    //}

    //protected void btnprevious_Click(object sender, EventArgs e)
    //{
    //    pos = (int)this.ViewState["vs"];
    //    pos -= 1;
    //    this.ViewState["vs"] = pos;
    //    Bindata();
    //}
    //protected void btnnext_Click(object sender, EventArgs e)
    //{
    //    pos = (int)this.ViewState["vs"];
    //    pos += 1;
    //    this.ViewState["vs"] = pos;
    //    Bindata();
    //}
    //protected void btnlast_Click(object sender, EventArgs e)
    //{
    //    pos = adsource.PageCount - 1;
    //    Bindata();
    //}
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        //StringBuilder StrHtmlGenerate = new StringBuilder();
        //StringBuilder StrExport = new StringBuilder();
        //StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        //StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        //StrExport.Append("<DIV  style='font-size:12px;'>");
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter tw = new HtmlTextWriter(sw);
        //pnlMain.RenderControl(tw);
        //string sim = sw.ToString();
        //StrExport.Append(sim);
        //StrExport.Append("</div></body></html>");
        //string strFile = "report.xls";
        //string strcontentType = "application/excel";
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.BufferOutput = true;
        //Response.ContentType = strcontentType;
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        //Response.Write(StrExport.ToString());
        //Response.Flush();
        //Response.Close();
        //Response.End();



        string Name = "Report";

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
        Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();

    }

    protected void lnkTenderNo_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkTenderNo = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkTenderNo.NamingContainer;
            string No = lnkTenderNo.Text;
            string accode = lnkTenderNo.Text;
            Int16 Action = 1;
            Session["TN_NO"] = No;
            Int32 counts = Convert.ToInt32(clsCommon.getString("SELECT tenderid  from nt_1_tender where tenderid=" + accode + "  "));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tnjhj", "javascript:TN('" + counts + "','" + Action + "');", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
}