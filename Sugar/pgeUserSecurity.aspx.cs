using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;
using System.Data;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Device.Location;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;

public partial class Sugar_pgeUserSecurity : System.Web.UI.Page
{
     
    string mobileNumber = string.Empty;
    string user = string.Empty;
    string User_Type = string.Empty;
    string pcbiossrno = string.Empty;
    string pcusername = string.Empty;
    string pcmac = string.Empty;
    string dbbiossrno = string.Empty;
    string dbusername = string.Empty;
    string dbmac = string.Empty;
    DataTable tempOTP = null;
    DataRow dr;
    string way = "";
    string throughWa = string.Empty;
    string EmailAdmin = string.Empty;
    string ipAddress = string.Empty;
    string clientipN = string.Empty;

    string ipAddress1 = string.Empty;
    string ipAddress2 = string.Empty;
    string UserIPAddress = string.Empty;
    string User_Security = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        User_Security = Session["User_Security"].ToString();
        string Configecurity = ConfigurationManager.AppSettings["Configecurity"].ToString();
        if (Configecurity == "Y" && User_Security == "Y")
        {

            string url = "https://icanhazip.com";
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            string str = responseRead;
            UserIPAddress = str;
            ipAddress1 = Session["IPAddress1"].ToString();
            ipAddress2 = Session["IPAddress2"].ToString();
            ipAddress1 = ipAddress1 + '\n';
            if (ipAddress1 == UserIPAddress || ipAddress2 + '\n' == UserIPAddress)
            {
                Session["ConfirmSecurity"] = "Y";
                Response.Redirect("~/Sugar/pgeHome.aspx", false);

            }
            else
            {
                ModalPopupMsg.Show();
            }

            responseStream.Close();
            responseStream.Dispose();

        }
        else
        {
            Session["ConfirmSecurity"] = "Y";
            Response.Redirect("~/Sugar/pgeHome.aspx", false);
        }





        string Security = string.Empty;


        user = Session["user"].ToString();
        clsMAC mac = new clsMAC();
        pcmac = mac.GetMACAddress();
        pcusername = mac.HostName();
        pcbiossrno = HardwareInfo.GetBIOSserNo();

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (User_Type == "A")
            {
                tempOTP = new DataTable();
                tempOTP.Columns.Add(new DataColumn("User_Name", typeof(string)));
                tempOTP.Columns.Add(new DataColumn("Company_Code", typeof(string)));
                tempOTP.Columns.Add(new DataColumn("OTP", typeof(string)));
                dr = tempOTP.NewRow();
                dr["User_Name"] = user;
                dr["Company_Code"] = Convert.ToInt32(Session["Company_Code"].ToString());
                tempOTP.Rows.Add(dr);
                ViewState["tempOTP"] = tempOTP;
                ModalPopupOTP.Show();
                ModalPopupMsg.Hide();
            }
            else
            {
                tempOTP = new DataTable();
                tempOTP.Columns.Add(new DataColumn("User_Name", typeof(string)));
                tempOTP.Columns.Add(new DataColumn("Company_Code", typeof(string)));
                tempOTP.Columns.Add(new DataColumn("OTP", typeof(string)));
                dr = tempOTP.NewRow();
                dr["User_Name"] = user;
                dr["Company_Code"] = Convert.ToInt32(Session["Company_Code"].ToString());
                tempOTP.Rows.Add(dr);
                ViewState["tempOTP"] = tempOTP;

                usermodalpopup.Show();
                ModalPopupOTP.Hide();
                ModalPopupMsg.Hide();
                string qry = "Select User_Name,Mobile as mobile from tblUser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and User_Type='A'";
                DataSet ds = new DataSet();
                ds = clsDAL.SimpleQuery(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        AdminGrid.DataSource = dt;
                        AdminGrid.DataBind();
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected string HideMobileNumber()
    {
        mobileNumber = clsCommon.getString("select Mobile_NoWa from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        if (!string.IsNullOrEmpty(mobileNumber) && mobileNumber.Length >= 2)
        {
            string lastTwoDigits = mobileNumber.Substring(mobileNumber.Length - 2);
            string hiddenDigits = new string('*', mobileNumber.Length - 2);
            mobileNumber = hiddenDigits + lastTwoDigits;
            return mobileNumber;
        }
        return mobileNumber;


    }

    protected string HideEmailId()
    {
        EmailAdmin = clsCommon.getString("Select EmailId from tblUser where User_Name='" + user.Trim() + "'");

        //if (!string.IsNullOrEmpty(EmailAdmin) && EmailAdmin.Length >= 2)
        //{
        //    string lastTwoDigits = EmailAdmin.Substring(EmailAdmin.Length - 2);
        //    string hiddenDigits = new string('*', EmailAdmin.Length - 2); 
        //    EmailAdmin =  hiddenDigits + lastTwoDigits;
        //    return EmailAdmin;
        //}

        int atIndex = EmailAdmin.IndexOf('@');
        if (atIndex <= 1)
        {
            return EmailAdmin;
        }

        string maskedPart = new string('*', atIndex - 1);
        string domain = EmailAdmin.Substring(atIndex);
        string newdomain = EmailAdmin.Substring(atIndex - 2);
        EmailAdmin = EmailAdmin[atIndex - 16] + maskedPart + newdomain;

        return EmailAdmin;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/login1.aspx", false);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {

        hdnfway.Value = rblist.SelectedValue.ToString();
        way = hdnfway.Value;
        usermodalpopup.Show();
        ModalPopupOTP.Hide();
        ModalPopupMsg.Hide();
        string qry = "Select User_Name,EmailId,Mobile as mobile from tblUser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and User_Security='Y'";
        DataSet ds = new DataSet();
        ds = clsDAL.SimpleQuery(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                AdminGrid.DataSource = dt;
                AdminGrid.DataBind();
            }
        }

    }

  

    protected void btnCancelOtp_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/login1.aspx", false);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string dtm = System.DateTime.Now.ToString("yyyy/MM/dd");
            string createddate = DateTime.Parse(dtm, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string Pin = txtOtpVerification.Text;
            tempOTP = (DataTable)ViewState["tempOTP"];

            string dtUserName = tempOTP.Rows[0]["User_Name"].ToString();
            string dtCompany_Code = tempOTP.Rows[0]["Company_Code"].ToString();
            string dtOtp = tempOTP.Rows[0]["OTP"].ToString();

            if (dtOtp == Pin)
            {
                if (dtUserName == user)
                {
                    if (dtCompany_Code == Convert.ToString(Convert.ToInt32(Session["Company_Code"].ToString())))
                    {
                        HttpCookie _Certificate = new HttpCookie("Certificate");
                        _Certificate["Certi"] = "Yes";
                        _Certificate.Expires = DateTime.Now.AddYears(10);
                        Response.Cookies.Add(_Certificate);

                        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                        {
                            string retRev = "";
                            obj.flag = 1;
                            obj.tableName = "tblSecurityCertificate";
                            obj.columnNm = "MAC,IPAddress,Computer_User,Created_Date";
                            obj.values = "'" + pcmac + "','" + pcbiossrno + "','" + pcusername + "','" + dtm + "'";
                            DataSet ds = new DataSet();
                            ds = obj.insertAccountMaster(ref retRev);
                        }
                        ViewState["tempOTP"] = null;
                        tempOTP = null;
                        Session["Confirm"] = "Y";
                        Session["ConfirmSecurity"] = "Y";
                        Response.Redirect("../Sugar/pgeHome.aspx", false);

                    }
                    else
                    {
                        ModalPopupMsg.Hide();
                        ModalPopupOTP.Hide();
                        ModalPopupVerification.Show();
                        lblWrongOtp.Text = "Wrong OTP Code.Please Enter Correct OTP!";
                        lblResendOtp.Text = "";
                    }
                }
                else
                {
                    ModalPopupMsg.Hide();
                    ModalPopupOTP.Hide();
                    ModalPopupVerification.Show();
                    lblWrongOtp.Text = "Wrong OTP Code.Please Enter Correct OTP!";
                    lblResendOtp.Text = "";
                }
            }
            else
            {
                ModalPopupMsg.Hide();
                ModalPopupOTP.Hide();
                ModalPopupVerification.Show();
                lblWrongOtp.Text = "Wrong OTP Code.Please Enter Correct OTP!";
                lblResendOtp.Text = "";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void resendlnk_Click(object sender, EventArgs e)
    {
        try
        {
            string Admin_Mobile = clsCommon.getString("Select Mobile from tblUser where User_Name='" + user + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
            string Admin_Mail = clsCommon.getString("Select EmailId from tblUser where User_Name='" + user + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
            lblWrongOtp.Text = "";
            txtOtpVerification.Text = "";
            string through = "";
            //way = rblist.SelectedValue.ToString();

            //hdnfway.Value = rblist.SelectedValue.ToString();
            way = hdnfway.Value;
            OTP otp = new OTP();
            string PIN = otp.OTPassword();

            tempOTP = (DataTable)ViewState["tempOTP"];
            DataRow dr = tempOTP.Rows[0];
            dr["OTP"] = PIN;

            if (way == "S")
            {
                string API = clsGV.msgAPI;
                string msg = "your Verification OTP is:";
                string Url = "";
                if (Session["adminmobile"] != null)
                {
                    Url = API + "mobile=" + Session["adminmobile"].ToString() + "&message=" + "Hello Admin The Certificate Generation Request From " + user + "  Please Inform OTP:(" + PIN + ") To This User";
                }
                else
                {
                    Url = API + "mobile=" + Admin_Mobile + "&message=" + msg + PIN;
                }
                try
                {
                    HttpWebRequest myreq = (HttpWebRequest)WebRequest.Create(Url);
                    HttpWebResponse myResponse = (HttpWebResponse)myreq.GetResponse();
                    StreamReader respReader = new StreamReader(myResponse.GetResponseStream());
                    string responseString = respReader.ReadToEnd();
                    respReader.Close();
                    myResponse.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                through = "Mobile Number";

            }
            else
            {
                try
                {
                    string mailFrom = Session["OtpEmail"].ToString();
                    string smtpPort = "587";
                    string emailPassword = Session["OtpPassword"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                    SmtpServer.Host = clsGV.Email_Address;
                    msg.From = new MailAddress(mailFrom);
                    msg.To.Add(Admin_Mail);
                    msg.Body = "Your One Time Password is:" + PIN;
                    msg.IsBodyHtml = true;
                    msg.Subject = "One Time Password";
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
                through = "Email";

                //string instanceid = clsCommon.getString("select Instance_Id from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //string accesstoken = clsCommon.getString("select Access_token from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                //string respString = string.Empty;
                //string Moblie_Number = Session["Mobile_NoWa"].ToString();
                //string msgwa = "your Verification OTP is: ";
                //string Url = "https://wawatext.com/api/send.php?number=91" + Moblie_Number + "&type=text&message=" + msgwa + PIN + "&instance_id=" + instanceid + "&access_token=" + accesstoken + "";
                ////string Url = "https://wawatext.com/api/send.php?number=91";
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                //HttpWebResponse resp = (HttpWebResponse)myReq.GetResponse();
                //StreamReader reder = new StreamReader(resp.GetResponseStream());
                //respString = reder.ReadToEnd();
                //reder.Close();
                //resp.Close();

                //string str = respString;
                //str = str.Replace("{", "");
                //str = str.Replace("}", "");
                //str = str.Replace(":", "");
                //str = str.Replace(",", "");
                //str = str.Replace("\"", "");
                //string sub2 = "success";
                //bool b = str.Contains(sub2);

                //string sub4 = "error";
                //bool s = str.Contains(sub4);
                //throughWa = "WhatsApp";
            }




            if (Session["adminmobile"] != null)
            {
                lblResendOtp.Text = "OTP is Sent To Registered Selected Admin";
            }
            else
            {
                lblResendOtp.Text = "OTP is Sent To Registered " + through + " or " + throughWa;
            }
            lblWrongOtp.Text = "";
            ModalPopupMsg.Hide();
            ModalPopupOTP.Hide();
            ModalPopupVerification.Show();
        }
        catch (Exception)
        {
            throw;
        }
    }


    protected void btnSendOtptoAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < AdminGrid.Rows.Count; i++)
            {


                CheckBox chk = AdminGrid.Rows[i].Cells[3].FindControl("grdCB") as CheckBox;
                if (chk.Checked == true)
                {

                    string admin_mobile = AdminGrid.Rows[i].Cells[2].Text.ToString();
                    string Admin_Mail = AdminGrid.Rows[i].Cells[1].Text.ToString();
                    //hdnfway.Value = rblist.SelectedValue.ToString();
                    way = hdnfway.Value;
                    OTP otp = new OTP();
                    string PIN = otp.OTPassword();
                    tempOTP = (DataTable)ViewState["tempOTP"];
                    DataRow dr = tempOTP.Rows[0];
                    dr["OTP"] = PIN;
                    if (way == "E")
                    {
                        try
                        {
                            string mailFrom = Session["OtpEmail"].ToString();
                            string smtpPort = "587";
                            string emailPassword = Session["OtpPassword"].ToString();
                            MailMessage msg = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                            SmtpServer.Host = clsGV.Email_Address;
                            msg.From = new MailAddress(mailFrom);
                            msg.To.Add(Admin_Mail);
                            msg.Body = "Your One Time Password is:  " + "<b>" + PIN + "</b>";
                            msg.IsBodyHtml = true;
                            msg.Subject = "One Time Password";
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
                    }
                    //if (way == "W")
                    //{
                    //    string instanceid = clsCommon.getString("select Instance_Id from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    //    string accesstoken = clsCommon.getString("select Access_token from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                    //    string respString = string.Empty;
                    //    string Moblie_Number = "9326079789";
                    //    string msg = "your Verification OTP is: ";
                    //    string Url = "https://wawatext.com/api/send.php?number=91" + Moblie_Number + "&type=text&message=" + msg + PIN + "&instance_id=" + instanceid + "&access_token=" + accesstoken + "";
                    //    //string Url = "https://wawatext.com/api/send.php?number=91";
                    //    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                    //    HttpWebResponse resp = (HttpWebResponse)myReq.GetResponse();
                    //    StreamReader reder = new StreamReader(resp.GetResponseStream());
                    //    respString = reder.ReadToEnd();
                    //    reder.Close();
                    //    resp.Close();

                    //    string str = respString;
                    //    str = str.Replace("{", "");
                    //    str = str.Replace("}", "");
                    //    str = str.Replace(":", "");
                    //    str = str.Replace(",", "");
                    //    str = str.Replace("\"", "");
                    //    string sub2 = "success";
                    //    bool b = str.Contains(sub2);

                    //    string sub4 = "error";
                    //    bool s = str.Contains(sub4);

                    //    if (b)
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Message Successfully Sent!');", true);
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Message Cloud Not Sent!');", true);
                    //    }
                    //    //Response.Write("mail err:" + e1);
                    //    //Response.Write("<script>alert('Error sending Mail');</script>");
                    //    //return;

                    //}
                    //else
                    //{
                    //    // string API = clsGV.msgAPI;
                    //    string msg = "your Verification OTP is: ";
                    //    //string API = Session["smsApi"].ToString();
                    //    //string senderid = Session["Sender_id"].ToString();
                    //    //string accusage = Session["Accusage"].ToString();
                    //    string API = clsCommon.getString("select smsApi from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    //    string senderid = clsCommon.getString("select Sender_id from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    //    string accusage = clsCommon.getString("select Accusage from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                    //    string Url = API + "mobile=" + admin_mobile + "&message=" + msg + PIN + "&senderid=NAVKAR&accusage=1";

                    //    try
                    //    {
                    //        HttpWebRequest myreq = (HttpWebRequest)WebRequest.Create(Url);
                    //        HttpWebResponse myResponse = (HttpWebResponse)myreq.GetResponse();
                    //        StreamReader respReader = new StreamReader(myResponse.GetResponseStream());
                    //        string responseString = respReader.ReadToEnd();
                    //        respReader.Close();
                    //        myResponse.Close();
                    //    }
                    //    catch (Exception)
                    //    {
                    //        throw;
                    //    }
                    //}
                }
            }

            ModalPopupMsg.Hide();
            ModalPopupOTP.Hide();
            usermodalpopup.Hide();
            ModalPopupVerification.Show();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/login1.aspx", false);
    }

    private string GetClientIPAddress()
    {
        
        try
        {
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        catch (Exception ex)
        {
            // Handle any exception that may occur while retrieving the IP address
            // You can log the exception or perform other error handling tasks here
        }

        return ipAddress;
    }

    protected void rblist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            SetFocus(txtOtpVerification);
            string Admin_Mobile = clsCommon.getString("Select Mobile from tblUser where User_Name='" + user.Trim() + "'");
            string Admin_Mail = clsCommon.getString("Select EmailId from tblUser where User_Name='" + user.Trim() + "'");
            //  way = rblist.SelectedValue.ToString();

            hdnfway.Value = rblist.SelectedValue.ToString();
            way = hdnfway.Value;
            OTP otp = new OTP();
            string PIN = otp.OTPassword();
            tempOTP = (DataTable)ViewState["tempOTP"];
            DataRow dr = tempOTP.Rows[0];
            dr["OTP"] = PIN;
            if (way == "E")
            {
                try
                {
                    string mailFrom = Session["EmailId"].ToString();
                    string smtpPort = "587";
                    string emailPassword = Session["EmailPassword"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                    SmtpServer.Host = clsGV.Email_Address;
                    msg.From = new MailAddress(mailFrom);
                    msg.To.Add(Admin_Mail);
                    msg.Body = "Your One Time Password is:  " + "<b>" + PIN + "</b>";
                    msg.IsBodyHtml = true;
                    msg.Subject = "One Time Password";
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
            }
            else
            {
                //string API = clsGV.msgAPI;
                string msg = "your Verification OTP is: ";
                string API = Session["smsApi"].ToString();
                string senderid = Session["Sender_id"].ToString();
                string accusage = Session["Accusage"].ToString();

                string Url = API + "mobile=" + Admin_Mobile + "&message=" + msg + PIN + "&senderid=" + senderid + "&accusage=" + accusage + "";

                try
                {
                    HttpWebRequest myreq = (HttpWebRequest)WebRequest.Create(Url);
                    HttpWebResponse myResponse = (HttpWebResponse)myreq.GetResponse();
                    StreamReader respReader = new StreamReader(myResponse.GetResponseStream());
                    string responseString = respReader.ReadToEnd();
                    respReader.Close();
                    myResponse.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            ModalPopupVerification.Show();
            ModalPopupMsg.Hide();
        }
        catch (Exception)
        {
            throw;
        }
    }
}