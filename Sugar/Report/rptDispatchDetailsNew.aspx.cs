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

public partial class Report_rptDispatchDetailsNew : System.Web.UI.Page
{
    string Mill_Code = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string Lot_No = string.Empty;
    string Sr_No = string.Empty;
    string qry = string.Empty;
    string Tender_No = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet databind;
    DataTable SecoundQry;
    DataTable selectedvalue;
    string tblPrefix = string.Empty;
    string f = "../GSReports/DispatchDetails_.htm";
    string f_Main = "../Report/rptDispatchDetails";
    string Branch_Code = string.Empty;
    // string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        Mill_Code = Request.QueryString["Mill_Code"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Lot_No = Request.QueryString["Lot_No"].ToString();
        Sr_No = Request.QueryString["Sr_No"].ToString();
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
            string fromdate = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy-MM-dd");
            string todate = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy-MM-dd");
            string Dispatched = "";
            if (Sr_No != string.Empty)
            {
                qry = " select doc_no as detail_id,doc_dateConverted as DI_Date,salebillname as Getpass,shiptoshortname as ShippedTo,truck_no, " +
            " quantal as DI_Qty,doshortname as DI_DO,purc_no from qrydohead where company_code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
            + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromdate +
            "' and '" + todate + "' and purc_order=" + Sr_No + " and tran_type='DO' order by doc_date";

            }
            else
            {
                qry = " select doc_no as detail_id,doc_dateConverted as DI_Date,salebillname as Getpass,shiptoshortname as ShippedTo,truck_no, " +
            " quantal as DI_Qty,doshortname as DI_DO,purc_no from qrydohead where company_code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
            + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromdate + "' and '" + todate + "' and tran_type='DO' order by doc_date";

            }
           
            databind = clsDAL.SimpleQuery(qry);
            SecoundQry = databind.Tables[0];

            if (Mill_Code != string.Empty)
            {

                if (Mill_Code != string.Empty && Lot_No != string.Empty && Sr_No != string.Empty)
                {
                    //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                    //              " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                    //              " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                    //           + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                    //              " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'   and Tender_No=" + Lot_No + " and ID=" + Sr_No + " Order By  Tender_Date";

                    qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                                " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                             + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  AND " +
                                " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'   and Tender_No=" + Lot_No + " and ID=" + Sr_No + " Order By  Tender_Date";


                }
                else if (Mill_Code != string.Empty && Lot_No != string.Empty && Sr_No == string.Empty)
                {



                //    qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                //                   " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                //                   " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                //                + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND " +
                //                   " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate +
                //                   "' and Mill_Code=" + Mill_Code + "  and Tender_No=" + Lot_No + " Order By  Tender_Date";

                    qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                                 " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                 " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                              + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND " +
                                 " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate +
                                 "' and Mill_Code=" + Mill_Code + "  and Tender_No=" + Lot_No + " Order By  Tender_Date";
                
                }
                else
                {


                    //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                    //              " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                    //              " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                    //           + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                    //              " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code + "  Order By  Tender_Date";

                    qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                                  " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                  " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                               + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  AND " +
                                  " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code + "  Order By  Tender_Date";


                }
            }
            if (Mill_Code == string.Empty)
            {
                if (Mill_Code == string.Empty && Lot_No == string.Empty)
                {


                    //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                    //              " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                    //              " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                    //           + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND " +
                    //              " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' Order By  Tender_Date";
                   
                    qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                                 " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                 " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                              + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND " +
                                 " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' Order By  Tender_Date";
                }

                if (Mill_Code == string.Empty && Lot_No != string.Empty)
                {
                    if (Lot_No != string.Empty && Sr_No == string.Empty)
                    {

                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                        //           " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //           " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //        + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                        //           " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'   and Tender_No=" + Lot_No + " Order By  Tender_Date";

                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                                  " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                  " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                               + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  AND " +
                                  " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'   and Tender_No=" + Lot_No + " Order By  Tender_Date";

                    }
                    else
                    {
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                        //          " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //          " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //       + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                        //          " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'   and Tender_No=" + Lot_No +
                        //          " and ID=" + Sr_No + " Order By  Tender_Date";

                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill ,Mill_Code " +
                                 " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                 " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                              + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  AND " +
                                 " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'   and Tender_No=" + Lot_No +
                                 " and ID=" + Sr_No + " Order By  Tender_Date";


                    }
                }
            }

            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn disp = new DataColumn("Dispatched", typeof(double));
                    ds.Tables[0].Columns.Add(disp);

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string tenderid = ds.Tables[0].Rows[j]["Tender_No"].ToString();

                        Dispatched = clsCommon.getString("Select isnull( SUM(quantal),0 ) as TD_Dispatch from " + tblPrefix + "deliveryorder WHERE company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and purc_no=" + tenderid + " AND tran_type='DO'");

                        //string tdid = ds.Tables[0].Rows[j]["ID"].ToString();
                        double dispatc = Dispatched != string.Empty ? double.Parse(Dispatched) : 0;
                        ds.Tables[0].Rows[j]["Dispatched"] = dispatc;
                    }
                    dt = ds.Tables[0];
                    if (Lot_No != string.Empty)
                    {
                        string millCode = dt.Rows[0]["Mill_Code"].ToString();
                        string millMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code=" + millCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        txtEmail.Text = millMail;
                    }

                    dtl.DataSource = dt;
                    dtl.DataBind();

                }
                else
                {
                    dtl.DataSource = null;
                    dtl.DataBind();
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
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dtlDispatch = (DataList)e.Item.FindControl("dtlDispatch");
        Label tenderno = (Label)e.Item.FindControl("lblTenderNo");
        string Purc_No = tenderno.Text;


        //qry = " select do.doc_no as detail_id,Convert(varchar(10),do.doc_date,103) as DI_Date,(A.Ac_Name_E +' , '+A.CityName) as Getpass,(c.Ac_Name_E +' , '+c.CityName) as ShippedTo,truck_no, " +
        //               " quantal as DI_Qty,B.Short_Name as DI_DO from " + tblPrefix + "deliveryorder do left outer  join " + tblPrefix + "qryAccountsList A on do.GETPASSCODE=A.Ac_Code AND do.company_code=A.Company_Code " +
        //     " left outer  join " + tblPrefix + "AccountMaster B on B.Ac_Code=do.DO AND do.company_code=B.Company_Code " + "left outer  join " + tblPrefix + "qryAccountsList c on do.voucher_by=c.Ac_Code AND do.company_code=A.Company_Code where do.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and do.Branch_Code=" + Branch_Code + " and purc_no=" + Purc_No + "  and tran_type='DO' order by do.doc_date";
       
        
        DataView view = new DataView(SecoundQry, "purc_no='" + tenderno.Text + "' ", "purc_no", DataViewRowState.CurrentRows);
        selectedvalue = view.ToTable(true, "detail_id", "DI_Date", "Getpass", "ShippedTo", "truck_no", "DI_Qty", "DI_DO", "purc_no");


        if (selectedvalue.Rows.Count > 0)
        {
            //dt = new DataTable();
            //dt = selectedvalue.Rows
            dtlDispatch.DataSource = selectedvalue;
            dtlDispatch.DataBind();
        }
        else
        {
            dtlDispatch.DataSource = null;
            dtlDispatch.DataBind();
        }

    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string email = txtEmail.Text;
            if (txtEmail.Text != string.Empty)
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
                msg.Body = "Dispatch Details";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Dispatch Details Report" + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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
        }
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");

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
}