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
using System.Web.UI.HtmlControls;
using System.Net.Mime;
public partial class Report_rptLetterPrint : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    string doc_no = string.Empty;
    DataSet ds;
    DataTable dt;
    string f = "../GSReports/Letter_.htm";
    string f_Main = "../Report/Letter";
    string matter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        doc_no = Request.QueryString["doc_no"].ToString();
        if (!Page.IsPostBack)
        {
            #region Address
            string qry = "Select * from tblVoucherHeadAddress where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCompany.Text = Session["Company_Name"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LeftAddress", typeof(string)));
                dt.Columns.Add(new DataColumn("MiddlePart", typeof(string)));
                dt.Columns.Add(new DataColumn("RightAddress", typeof(string)));

                string AL1 = ds.Tables[0].Rows[0]["AL1"].ToString();
                string AL2 = ds.Tables[0].Rows[0]["AL2"].ToString();
                string AL3 = ds.Tables[0].Rows[0]["AL3"].ToString();
                string AL4 = ds.Tables[0].Rows[0]["AL4"].ToString();
                string OtherDetails = ds.Tables[0].Rows[0]["Other"].ToString();

                string rnl = AL1.Replace("\n", "<br/>");
                var TabSpace = new String(' ', 4);
                string ab = rnl.Replace("\t", TabSpace);
                string la = ab.Replace(" ", "&nbsp;");
                lblAl1.Text = la;


                string rnl1 = AL2.Replace("\n", "<br/>");
                var TabSpace1 = new String(' ', 4);
                string ab1 = rnl1.Replace("\t", TabSpace1);
                string la1 = ab1.Replace(" ", "&nbsp;");
                lblAl2.Text = la1;

                string rnl2 = AL3.Replace("\n", "<br/>");
                var TabSpace2 = new String(' ', 4);
                string ab2 = rnl2.Replace("\t", TabSpace2);
                string la2 = ab2.Replace(" ", "&nbsp;");
                lblAl3.Text = la2;

                string rnl3 = AL4.Replace("\n", "<br/>");
                var TabSpace3 = new String(' ', 4);
                string ab3 = rnl3.Replace("\t", TabSpace2);
                string la3 = ab3.Replace(" ", "&nbsp;");
                lblAl4.Text = la3;

                string rnl4 = OtherDetails.Replace("\n", "<br/>");
                var TabSpace4 = new String(' ', 4);
                string ab4 = rnl4.Replace("\t", TabSpace2);
                string la4 = ab4.Replace(" ", "&nbsp;");
                lblOtherDetails.Text = la4;

            }
            #endregion
            this.BindList();
        }
    }

    private void BindList()
    {
        try
        {
            if (doc_no != string.Empty)
            {
                qry = "Select AC_CODE,DOC_NO as RefNo,CONVERT(varchar(10),DOC_DATE,103) as LetterDate,AC_NAME as Party,ADDRESS address,CITY,PINCODE,KIND_ATT as kind_att,SUBJECT as Subject,REF_NO as Your_Refno,CONVERT(varchar(10),REF_DT,103) as Dated,MATTER as Matter from " + tblPrefix + "Letter" +
                    " WHERE Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and DOC_NO=" + doc_no + "";
                ds = new DataSet();
                ds = clsDAL.SimpleQuery(qry);
                DataTable dt2 = new DataTable();
                if (ds != null)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("RefNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("LetterDate", typeof(string)));
                    dt.Columns.Add(new DataColumn("Party", typeof(string)));
                    dt.Columns.Add(new DataColumn("address", typeof(string)));
                    dt.Columns.Add(new DataColumn("kind_att", typeof(string)));
                    dt.Columns.Add(new DataColumn("Subject", typeof(string)));
                    dt.Columns.Add(new DataColumn("Your_Refno", typeof(string)));
                    dt.Columns.Add(new DataColumn("Dated", typeof(string)));
                    dt.Columns.Add(new DataColumn("Matter", typeof(string)));
                    dt.Columns.Add(new DataColumn("CmpName", typeof(string)));
                    dt.Columns.Add(new DataColumn("City", typeof(string)));
                    dt.Columns.Add(new DataColumn("Pincode", typeof(string)));
                    //dt.Columns.Add(new DataColumn("", typeof(string)));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            DataRow dr = dt.NewRow();
                            string ac_code = ds.Tables[0].Rows[i]["AC_CODE"].ToString();
                            string email = clsCommon.getString("Select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=" + ac_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            txtEmail.Text = clsCommon.getString("Select Email_Id_cc from " + tblPrefix + "AccountMaster where Ac_Code=" + ac_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "") + "," + email;
                            dr["RefNo"] = ds.Tables[0].Rows[i]["RefNo"].ToString();
                            dr["LetterDate"] = ds.Tables[0].Rows[i]["LetterDate"].ToString();
                            dr["Party"] = ds.Tables[0].Rows[i]["Party"].ToString();
                            dr["address"] = ds.Tables[0].Rows[i]["address"].ToString();
                            dr["kind_att"] = ds.Tables[0].Rows[i]["kind_att"].ToString();
                            dr["Subject"] = ds.Tables[0].Rows[i]["Subject"].ToString();
                            dr["Your_Refno"] = ds.Tables[0].Rows[i]["Your_Refno"].ToString();
                            dr["Dated"] = ds.Tables[0].Rows[i]["Dated"].ToString();
                            string s = ds.Tables[0].Rows[i]["Matter"].ToString();
                            string rnl = s.Replace("\n", "<br/>");
                            var TabSpace = new String(' ', 4);
                            string ab = rnl.Replace("\t", TabSpace);
                            string rs = ab.Replace(" ", "&nbsp;");
                            dr["Matter"] = rs;
                            dr["CmpName"] = Session["Company_Name"].ToString();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["CITY"].ToString()))
                            {
                                dr["City"] = ds.Tables[0].Rows[i]["CITY"].ToString();
                            }
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["PINCODE"].ToString()))
                            {
                                dr["Pincode"] = "Pincode: " + ds.Tables[0].Rows[i]["PINCODE"].ToString();
                            }
                            dt.Rows.Add(dr);


                        }
                        dt2 = dt;
                        if (dt.Rows.Count > 0)
                        {

                            //lblCompanyAddr.Text = clsGV.CompanyAddress;
                            //lblCompanyMobile.Text = "Mobile: " + clsGV.CompanyPhone;
                            dtl.DataSource = dt;
                            dtl.DataBind();
                            dtlist2.DataSource = dt2;
                            dtlist2.DataBind();
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        System.Web.UI.WebControls.Image imgSign = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSign");
        string imgurl = clsCommon.getString("Select ImagePath from tblSign where ImageOrLogo='S' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        imgSign.ImageUrl = imgurl;


    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            Label lblSubject = (Label)dtl.Items[0].FindControl("lblSubject");
            string MailSubject = Convert.ToString(lblSubject.Text);
            if (txtEmail.Text != string.Empty)
            {
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
                    contentType.Name = "Letter.htm";
                    Attachment attachment = new Attachment(ms, contentType);

                    string mailFrom = Session["EmailId"].ToString();
                    string smtpPort = "587";
                    string emailPassword = Session["EmailPassword"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                    SmtpServer.Host = clsGV.Email_Address;
                    msg.From = new MailAddress(mailFrom);
                    msg.To.Add(txtEmail.Text);
                    msg.Body = "DO";
                    msg.Attachments.Add(attachment);
                    msg.IsBodyHtml = true;
                    //msg.Subject = "Letter";
                    msg.Subject = MailSubject;
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