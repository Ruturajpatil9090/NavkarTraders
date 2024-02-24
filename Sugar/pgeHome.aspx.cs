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

public partial class pgeHome : System.Web.UI.Page
{
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


    string clientipN = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        clsMAC mac = new clsMAC();
        pcmac = mac.GetMACAddress();
        pcusername = mac.HostName();
        pcbiossrno = HardwareInfo.GetBIOSserNo();
        //string Configecurity = ConfigurationManager.AppSettings["Configecurity"].ToString();

        //if (Configecurity == "Y")
        //{
        //    hdnfconfirm.Value = "Y";
           

        //}

        #region
        //String MyIPAddress = "";
        //WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        //using (WebResponse response = request.GetResponse())
        //using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        //{
        //    MyIPAddress = stream.ReadToEnd();
        //}
        //int first = MyIPAddress.IndexOf("Address: ") + 9;
        //int last = MyIPAddress.LastIndexOf("</body>");
        //MyIPAddress = MyIPAddress.Substring(first, last - first);

        //var request1 = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");
        //request1.UserAgent = "curl"; // this will tell the server to return the information as if the request was made by the linux "curl" command
        //string publicIPAddress;
        //request1.Method = "GET";
        //using (WebResponse response1 = request1.GetResponse())
        //{
        //    using (var reader = new System.IO.StreamReader(response1.GetResponseStream()))
        //    {
        //        publicIPAddress = reader.ReadToEnd();
        //    }
        //}

        //string externalIP;
        //externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
        //externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
        //             .Matches(externalIP)[0].ToString();

       
     


        //string aa = lblIPAdd.Text;

        //Label1.Text = aa;


        //lblIPAdd.Text = MyIPAddress + publicIPAddress + " " + externalIP;
        //if (Configecurity == "Y")
        //{
        //    string IPAddress1 = Session["IPAddress1"].ToString();
        //    string IPAddress2 = Session["IPAddress2"].ToString();
        //    string hostName = Dns.GetHostName();
        //    string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        //    if (IPAddress1 == MyIPAddress || IPAddress2 == MyIPAddress)
        //    {
        //    }
        //    else
        //    {
        //        if (Session["Confirm"] == "Y")
        //        {

        //        }
        //        else
        //        {
        //            ModalPopupMsg.Show();
        //        }
        //    }

        //}

        #endregion

        //string ipAddress = IpAddress();
        ////string hostName = Dns.GetHostByAddress(ipAddress).HostName;
        //string strHostName = System.Net.Dns.GetHostName();
        //string clientIPAddressN = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        //clientipN = clientIPAddressN.ToString();

        //dbmac = clsCommon.getString("Select MAC from tblSecurityCertificate where MAC='" + pcmac + "'");
        //dbbiossrno = clsCommon.getString("Select IPAddress from tblSecurityCertificate Where MAC='" + dbmac + "'");
        //HttpCookie _Certificate = Request.Cookies["Certificate"];
        //if (_Certificate != null)
        //{
        //    if (pcmac == dbmac)
        //    {
        //        if (pcbiossrno == dbbiossrno)
        //        {
        //            if (!IsPostBack)
        //            {
        //                //base.LogActivity("Visiting the home page..", true);
        //                Companyname();
        //            }
        //        }
        //        else
        //        {
        //            ModalPopupMsg.Show();
        //        }
        //    }
        //    else
        //    {
        //        ModalPopupMsg.Show();
        //    }
        //}
        //else
        //{
        //    ModalPopupMsg.Show();
        //}
    }

    private void Companyname()
    {
        #region set company name
        try
        {
            string companyCode = Session["Company_Code"].ToString();

            string qry = "";


            string Company_Name_E = clsCommon.getString("select Company_Name_E from Company where Company_Code='" + companyCode + "'");

            Label lbl = (Label)Master.FindControl("lblCompanyName");
            lbl.Text = Company_Name_E;
        #endregion

            #region generate company wise table prefix

            string companyPrefix = string.Empty;

            if (Company_Name_E != string.Empty)
            {
                string[] initials = Company_Name_E.Split(' ');
                for (int i = 0; i < initials.Length; i++)
                {
                    companyPrefix = companyPrefix + initials[i].Substring(0, 1);
                }
                companyPrefix = companyPrefix + "_" + companyCode + "_";

                Session["tblPrefix"] = companyPrefix;
                Session["tblPrefix"] = "NT_1_";
                //generate company wise view
                clsCommon.generateView(companyCode);
            }
            #endregion
        }
        catch
        {
            //company not selected  Go to startup page to view the list of all companies
        }
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
    protected void btnAll_Click(object sender, EventArgs e)
    {
        string Datefrm = DateTime.Parse(clsGV.To_date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "D", "javascript:DO()", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "E", "javascript:TP()", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "F", "javascript:GL()", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "G", "javascript:MP('" + Datefrm + "','" + Datefrm + "','')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "B", "javascript:Ac('" + fromDT + "','" + toDT + "','')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "H", "javascript:DD('" + Datefrm + "','" + Datefrm + "','" + Branch_Code + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "A", "javascript:report('" + fromDT + "','" + toDT + "','')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "C", "javascript:Account()", true);

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
            }
            else
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
            if (Session["adminmobile"] != null)
            {
                lblResendOtp.Text = "OTP is Sent To Registered Selected Admin";
            }
            else
            {
                lblResendOtp.Text = "OTP is Sent To Registered " + through;
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
                       // string API = clsGV.msgAPI;
                        string msg = "your Verification OTP is: ";
                        //string API = Session["smsApi"].ToString();
                        //string senderid = Session["Sender_id"].ToString();
                        //string accusage = Session["Accusage"].ToString();
                        string API = clsCommon.getString("select smsApi from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string senderid = clsCommon.getString("select Sender_id from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string accusage = clsCommon.getString("select Accusage from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        string Url = API + "mobile=" + admin_mobile + "&message=" + msg + PIN + "&senderid=NAVKAR&accusage=1";

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
    private string IpAddress()
    {
        string strIpAddress;
        strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (strIpAddress == null)
            strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        return strIpAddress;
    }

    protected void btnMB_Click(object sender, EventArgs e)
    {
        MB.Show("Ankush");
    }

    protected void btnBWSPA_Click(object sender, EventArgs e)
    {
        try
        {
            string fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            string toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:bwlpa('" + fromDT + "','" + toDT + "','')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btndatabasebackup_Click(object sender, EventArgs e)
    {
        #region
        //string conn = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
        //SqlConnection sqlconn = new SqlConnection(conn);
        //SqlCommand sqlcmd = new SqlCommand();
        //SqlDataAdapter da = new SqlDataAdapter();
        //DataTable dt = new DataTable();



        //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn);

        //string database = builder.InitialCatalog;
        //// Backup destination
        //string backupDestination = "D:\\SQLBackUpFolder";
        //// check if backup folder exist, otherwise create it.
        //if (!System.IO.Directory.Exists(backupDestination))
        //{
        //    System.IO.Directory.CreateDirectory("D:\\SQLBackUpFolder");
        //}
        //try
        //{
        //    sqlconn.Open();
        //    sqlcmd = new SqlCommand("backup database " + database + " to disk='" + backupDestination + "\\" + "Sugarian" + DateTime.Now.ToString("ddMMyyyy_HHmmss_") + user + ".Bak'", sqlconn);
        //    sqlcmd.ExecuteNonQuery();
        //    //Close connection
        //    sqlconn.Close();
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Backup Database');", true);
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
        #endregion
        string conn = ConfigurationManager.ConnectionStrings["sqlconnection"].ToString();
        SqlConnection sqlconn = new SqlConnection(conn);
        SqlCommand sqlcmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();



        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn);

        string database = builder.InitialCatalog;
        // Backup destination
        string backupDrive = clsCommon.getString("select dbbackup from tblvoucherheadaddress where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ");
        //string backupDestination = backupDrive+":\\SQLBackUpFolder";
        string backupDestination = backupDrive;
        // check if backup folder exist, otherwise create it.
        if (!System.IO.Directory.Exists(backupDestination))
        {
            System.IO.Directory.CreateDirectory(backupDrive);
        }
        try
        {
            sqlconn.Open();
            sqlcmd = new SqlCommand("backup database " + database + " to disk='" + backupDestination + "\\" + "Sugarian" + DateTime.Now.ToString("ddMMyyyy_HHmmss_") + user + ".Bak'", sqlconn);
            sqlcmd.ExecuteNonQuery();
            //Close connection
            sqlconn.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Backup Database');", true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
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


}