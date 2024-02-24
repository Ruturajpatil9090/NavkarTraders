using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;


public partial class Report_rptRetailSellPrintForGST : System.Web.UI.Page
{
    string f = "../GSReports/SalesBill_.htm";
    string f_Main = "../Report/rptsalebill";
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string cityMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string email = string.Empty;
    string billno = string.Empty;
    string type = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime time = DateTime.Now;
        lblTime.Text = time.ToString();

        billno = Request.QueryString["billno"];
        type = Request.QueryString["type"];
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        cityMasterTable = tblPrefix + "CityMaster";
        qryCommon = tblPrefix + "qrySugarRetailSellList";
        if (!Page.IsPostBack)
        {
            BindList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showprint1fdf", "javascript:PrintPanel();", true);

        }

    }

    private void BindList()
    {
        try
        {
            if (billno != string.Empty && type != string.Empty)
            {
                using (clsDataProvider obj = new clsDataProvider())
                {

                    qry = "select  Party_Name,cityname,Gst_No as partygst,doc_no as billno,doc_dateConverted," +
               "Due_Days as duedays,Vehical_No as lorry,Narration,Delivered,(case CashRecive when 'True' then '' when 'False' then 'Not Paid' end) as cashnotrecive,CGSTRate,CGSTAmount,SGSTRate," +
             "SGSTAmount,IGSTRate,IGSTAmount,Total,Grand_Total,Round_Off,Party_Name,brokername,HamaliAmount,Party_Name_New,TCS_Rate,TCS_Amt,TCS_Net_Payable from qryretailsellheaddetail " +
             " where Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString() + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Doc_No=" + billno + " and Tran_Type='" + type + "' limit 1 ";



                    //qry = "select top(1) partyname,partycity,partytin,partygst,doc_no as billno,Convert(varchar(10),doc_date,103) as date,Due_Days as duedays,Vehical_No as lorry,Delivered,(case CashRecive when 'True' then '' when 'False' then 'Not Paid' end) as cashnotrecive," +
                    //    "CGSTRate,CGSTAmount,SGSTRate,SGSTAmount,IGSTRate,IGSTAmount,Total,Grand_Total,Round_Off,Party_Name,BrokerName,HamaliAmount,New_PartyNM from " + qryCommon + " where Company_Code="
                    //    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    //    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Doc_No=" + billno + " and Tran_Type='" + type + "'";



                    DataSet ds = new DataSet();
                    ds = obj.GetDataSet(qry);

                    string partyno = clsCommon.getString("select Party_Code from qryretailsellheaddetail where Doc_no='" + billno + "' and Company_Code="
                       + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));


                    //string PartyName = clsCommon.getString("select Party_Name from " + qryCommon + " where Doc_no='" + billno + "' and Company_Code="
                    //   + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                    //string Party_Name = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code='" + part + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                    string emailid = clsCommon.getString("select Email_Id from qrymstaccountmaster where Ac_Code='" + partyno + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    txtEmail.Text = emailid;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Columns.Add(new DataColumn("CompanyGST", typeof(string)));
                        string companyGst = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");


                        ds.Tables[0].Rows[0]["CompanyGST"] = companyGst;
                        ds.Tables[0].Columns.Add(new DataColumn("isdel", typeof(string)));
                        string del = ds.Tables[0].Rows[0]["Delivered"].ToString();
                        if (del == "False")
                        {
                            ds.Tables[0].Rows[0]["isdel"] = "";
                        }
                        else
                        {
                            ds.Tables[0].Rows[0]["isdel"] = "Delivered";
                        }

                        if (ds.Tables[0].Rows[0]["partygst"].ToString().Trim() != string.Empty)
                        {
                            ds.Tables[0].Rows[0]["partygst"] = "GST : " + ds.Tables[0].Rows[0]["partygst"].ToString();
                        }
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        dtlist.DataSource = dt;
                        dtlist.DataBind();
                        //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showprint1fdf", "javascript:PrintPanel();", true);

                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlist_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {

            Label lblBillType = (Label)e.Item.FindControl("lblBillType");
            //if (type == "CS" || type == "CS1")
            //{
            //    lblBillType.Text = "Cash ";
            //}
            //else
            //{
            //    lblBillType.Text = "Credit ";
            //}

            Label lblCompany = (Label)e.Item.FindControl("lblCompany");
            lblCompany.Text = "M/S. " + Session["Company_Name"].ToString();
            Label lblAl1 = (Label)e.Item.FindControl("lblAl1");
            Label lblAl2 = (Label)e.Item.FindControl("lblAl2");
            Label lblAl3 = (Label)e.Item.FindControl("lblAl3");
            Label lblAl4 = (Label)e.Item.FindControl("lblAl4");
            Label lblOtherDetails = (Label)e.Item.FindControl("lblOtherDetails");
            Label lblPartyName = (Label)e.Item.FindControl("lblPartyName");


            //string PartyName = clsCommon.getString("select Party_Name from " + qryCommon + " where Doc_no='" + billno + "' and Company_Code="
            //         + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            //string name = clsCommon.getString("select Ac_Name_E from NT_1_AccountMaster where Ac_Code='" + PartyName + "' and Ac_type='CR' and Company_Code="
            //         + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            //lblPartyName.Text = name;
            //Label lblbankdetail = (Label)e.Item.FindControl("lblbankdetails");
            //lblbankdetail.Text = clsCommon.getString(" select Bank_Name from NT_1_InvoiceUtility where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //    + " and Branch_Code=" + Convert.ToInt32(Session["Branch_Code"].ToString()) + "");
            #region Address
            //string qry = "Select * from tblVoucherHeadAddress where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            //DataSet ds = new DataSet();
            //ds = clsDAL.SimpleQuery(qry);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("LeftAddress", typeof(string)));
            //    dt.Columns.Add(new DataColumn("MiddlePart", typeof(string)));
            //    dt.Columns.Add(new DataColumn("RightAddress", typeof(string)));

            //    string AL1 = ds.Tables[0].Rows[0]["AL1"].ToString();
            //    string AL2 = ds.Tables[0].Rows[0]["AL2"].ToString();
            //    string AL3 = ds.Tables[0].Rows[0]["AL3"].ToString();
            //    string AL4 = ds.Tables[0].Rows[0]["AL4"].ToString();
            //    string OtherDetails = ds.Tables[0].Rows[0]["Other"].ToString();

            //    string rnl = AL1.Replace("\n", "<br/>");
            //    var TabSpace = new String(' ', 4);
            //    string ab = rnl.Replace("\t", TabSpace);
            //    string la = ab.Replace(" ", "&nbsp;");
            //    lblAl1.Text = la;


            //    string rnl1 = AL2.Replace("\n", "<br/>");
            //    var TabSpace1 = new String(' ', 4);
            //    string ab1 = rnl1.Replace("\t", TabSpace1);
            //    string la1 = ab1.Replace(" ", "&nbsp;");
            //    lblAl2.Text = la1;

            //    string rnl2 = AL3.Replace("\n", "<br/>");
            //    var TabSpace2 = new String(' ', 4);
            //    string ab2 = rnl2.Replace("\t", TabSpace2);
            //    string la2 = ab2.Replace(" ", "&nbsp;");
            //    lblAl3.Text = la2;

            //    string rnl3 = AL4.Replace("\n", "<br/>");
            //    var TabSpace3 = new String(' ', 4);
            //    string ab3 = rnl3.Replace("\t", TabSpace2);
            //    string la3 = ab3.Replace(" ", "&nbsp;");
            //    lblAl4.Text = la3;

            //    string rnl4 = OtherDetails.Replace("\n", "<br/>");
            //    var TabSpace4 = new String(' ', 4);
            //    string ab4 = rnl4.Replace("\t", TabSpace2);
            //    string la4 = ab4.Replace(" ", "&nbsp;");
            //    lblOtherDetails.Text = la4;

            // }
            #endregion

            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblBillNo = (Label)e.Item.FindControl("lblBillNo");
            string billno1 = lblBillNo.Text.ToString();
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            DataSet dsDetails = new DataSet();
            string qryDetails = " select itemname as itemname,KgPerKatta,(KgPerKatta * Quantity) as Kg,'' as HSN,Quantity as qty,Rate,Rate as GSTRate,vat_percent,vat_amount,Gross,millshortname as Mill_Name,grade from "
                + " qryretailsellheaddetail  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Doc_No=" + billno1 + " and Tran_Type='" + type + "'";
            //dsDetails = obj.GetDataSet(qryDetails);


            dsDetails = clsDAL.SimpleQuery(qryDetails);
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
                {
                    double qty = Convert.ToDouble(dsDetails.Tables[0].Rows[i]["qty"].ToString());
                    double Rate = Convert.ToDouble(dsDetails.Tables[0].Rows[i]["Rate"].ToString());
                    double kgperkatta = Convert.ToDouble(dsDetails.Tables[0].Rows[i]["KgPerKatta"].ToString());
                    double saleRate = Math.Round(Rate / 105 * 100, 2);
                    dsDetails.Tables[0].Rows[i]["Rate"] = saleRate;

                    double rateSale = 0.0;
                    if (kgperkatta == 50)
                    {
                        rateSale = Math.Round((saleRate / 2) * qty, 2);
                    }
                    else
                    {
                        rateSale = Math.Round((saleRate / 4) * qty, 2);
                    }
                    //dsDetails.Tables[0].Rows[i]["Value"] = rateSale;
                    dsDetails.Tables[0].Rows[i]["Gross"] = rateSale;

                }
                DataTable dt = new DataTable();
                dt = dsDetails.Tables[0];
                dtlDetails.DataSource = dt;
                dtlDetails.DataBind();


                double totalQty = Convert.ToDouble(dt.Compute("SUM(qty)", string.Empty));
                //double totalValue = Convert.ToDouble(dt.Compute("SUM(Value)", string.Empty));
                //double totalVatamount = Convert.ToDouble(dt.Compute("SUM(vat_amount)", string.Empty));
                double totalGross = Convert.ToDouble(dt.Compute("SUM(Gross)", string.Empty));

                Label lblTotalQty = (Label)e.Item.FindControl("lblTotalQty");
                Label lblTotalValue = (Label)e.Item.FindControl("lblTotalValue");
                Label lblTotalVatAmount = (Label)e.Item.FindControl("lblTotalVatAmount");
                Label lblTotalGross = (Label)e.Item.FindControl("lblTotalGross");
                Label lblNotPaid = (Label)e.Item.FindControl("lblNotPaid");


                lblTotalQty.Text = totalQty.ToString();
                //lblTotalValue.Text = totalValue.ToString();
                //lblTotalVatAmount.Text = totalVatamount.ToString();
                lblTotalGross.Text = totalGross.ToString();
                //lblTaxableAmount.Text = totalGross.ToString();
                Label lblBillAmountInWords = (Label)e.Item.FindControl("lblBillAmountInWords");
                //  Label lblBillAmount = (Label)e.Item.FindControl("lblBillAmount");
                //lblBillAmountInWords.Text = clsNoToWord.ctgword(lblTotalGross.Text);

                Label lblBillAmount = (Label)e.Item.FindControl("Label22");
                lblBillAmountInWords.Text = clsNoToWord.ctgword(lblBillAmount.Text);
                //lblBillAmount.Text = lblTotalGross.Text;

                Label lblCompanyName = (Label)e.Item.FindControl("lblCompanyName");
                lblCompanyName.Text = "M/S. " + Session["Company_Name"].ToString();
                System.Web.UI.WebControls.Image imgSign = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSign");
                string imgurl = clsCommon.getString("Select ImagePath from tblSign where ImageOrLogo='S' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string img = "~/Sugar/Images/" + imgurl;
                imgSign.ImageUrl = img;

            }
            //}
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            email = txtEmail.Text.ToString();
            Label lblSB_No = (Label)dtlist.Items[0].FindControl("lblSB_No");

            //StringWriter sw = new StringWriter();
            //HtmlTextWriter tw = new HtmlTextWriter(sw);
            //pnlMain.RenderControl(tw);
            //string s = sw.ToString();
            //s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");

            using (MemoryStream ms = new MemoryStream())
            {
                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                pnlMain.RenderControl(tw);
                string s = sw.ToString();
                s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");
                byte[] array = Encoding.UTF8.GetBytes(s);
                ms.Write(array, 0, array.Length);
                ms.Seek(0, SeekOrigin.Begin);
                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Octet;
                contentType.Name = "SaleBill.htm";
                Attachment attachment = new Attachment(ms, contentType);

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(email);
                msg.Body = "Sale Bill";
                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;

                msg.Subject = "Bill No:" + billno;
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
            //try
            //{
            //    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
            //    {
            //        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
            //        {
            //            w.WriteLine(s);
            //        }
            //    }
            //}
            //catch (Exception ee)
            //{
            //    f = f_Main + ".htm";
            //    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
            //    {
            //        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
            //        {
            //            w.WriteLine(s);
            //        }
            //    }
            //}
            //string mailFrom = Session["EmailId"].ToString();
            //string smtpPort = "587";
            //string emailPassword = Session["EmailPassword"].ToString();
            //MailMessage msg = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
            //SmtpServer.Host = clsGV.Email_Address;
            //msg.From = new MailAddress(mailFrom);
            //msg.To.Add(email);
            //msg.Body = "Sales Bill";
            //msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            //msg.IsBodyHtml = true;
            ////msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            //msg.Subject = "Sales Bill Report" + DateTime.Now.ToString("dd/MM/yyyy");
            //msg.IsBodyHtml = true;
            //msg.IsBodyHtml = true;
            //if (smtpPort != string.Empty)
            //{
            //    SmtpServer.Port = Convert.ToInt32(smtpPort);
            //}
            //                    SmtpServer.EnableSsl = true;
            //SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
            //SmtpServer.Send(msg);
        }
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");
    }

    protected void btnPriPrinted_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception)
        {
            throw;
        }
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{

    //}
}