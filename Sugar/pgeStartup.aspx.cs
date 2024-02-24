using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Threading;
using System.Web.Security;
using System.Net;
using System.IO;
public partial class pgeStartup : System.Web.UI.Page
{
    //data
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ///API 
        //http://localhost:3455/AccoWeb/pgeStartup.aspx?GroupCode=#GroupCode#
        try
        {
            ViewState["Group_Code"] = Request.QueryString["Group_Code"];
            Session["Group_Code"] = Request.QueryString["GC"];
            ViewState["uname"] = Request.QueryString["nm"];
            ViewState["pass"] = Request.QueryString["pd"];
            Session["tblPrefix"] = "NT_1_";
            //Session["tblPrefix"] = Session["tblPrefix"].ToString();
            Session["uname"] = ViewState["uname"].ToString();
            Session["pass"] = ViewState["pass"].ToString();
            string uname = ViewState["uname"].ToString();
            string pass = ViewState["pass"].ToString();
            Session["user"] = uname;
            UserSecurity();
            //  ViewState["GroupCode"] = "1";
            if (!Page.IsPostBack)
            {
                string qry = "select count(*) from Company where Group_Code=" + Session["Group_Code"].ToString() + " ";
                var str = clsCommon.getString(qry);
                int cnt = Convert.ToInt32(str);
                if (cnt > 0)
                {
                    this.fillGrid();
                    this.fillDropdownYear();
                   
                }
                else
                {
                    #region insert into user table admin user entry
                    qry = "insert into tblUser ( User_Name,Password,User_Type) "
                       + " values('" + uname + "','" + pass + "','A')";
                    clsDAL.SimpleQuery(qry);
                    #endregion
                    Response.Redirect("pgeCreateNewCompany.aspx", true);
                }
            }
        }
        catch
        {
            //  Response.Redirect("http://localhost:2316/Sugar/HomePage/pgeloginForm.aspx");
        }
    }



    private void fillDropdownYear()
    {
        ListItem liDefault = new ListItem("Default", "0");
        try
        {
            drpYear.Items.Clear();
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListItem li = new ListItem("---Select---", "0");
            qry = "select * from AccountingYear where Company_Code=" + Session["Company_Code"].ToString() + " order by yearCode desc";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpYear.DataSource = dt;
                        drpYear.DataValueField = dt.Columns[0].ToString();
                        drpYear.DataTextField = dt.Columns[1].ToString();

                        drpYear.DataBind();
                    }
                }
            }

            //drpYear.Items.Insert(0, li);

            drpBranch.Items.Clear();


            qry = "select Branch_Id,Branch from BranchMaster where Company_Code='" + Session["Company_Code"].ToString() + "'";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpBranch.DataSource = dt;
                        drpBranch.DataTextField = "Branch";
                        drpBranch.DataValueField = "Branch_Id";
                        drpBranch.DataBind();
                    }
                }
            }
            //drpBranch.Items.Insert(0, liDefault);
        }
        catch
        {
            //drpBranch.Items.Insert(0, liDefault);
        }
    }

    private void fillGrid()
    {
        try
        {
            string qry = "select Company_Name_E,Company_Code from Company where Group_Code='" + Session["Group_Code"].ToString() + "' order by Company_Code asc";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdCompany.DataSource = dt;
                        grdCompany.DataBind();

                        LinkButton lnkCompany = grdCompany.Rows[0].Cells[1].FindControl("lnkCompany") as LinkButton;
                        lnkCompany.Focus();
                    }
                }
            }
        }
        catch
        {

        }
    }


    protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            Int32 companyCode = 0;
            companyCode = Convert.ToInt32(grdCompany.Rows[rowindex].Cells[0].Text);
            Session["Company_Code"] = companyCode;

            clsGV.Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            clsGV.CompanyName = clsCommon.getString("select Company_Name_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_Name"] = clsCommon.getString("select Company_Name_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            clsGV.CompanyAddress = clsCommon.getString("select Address_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["address"] = clsCommon.getString("select Address_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            clsGV.CompanyPhone = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Session["Company_Code"].ToString());

            Session["CompanyPan_No"] = clsCommon.getString("select Pan_No from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_CST"] = clsCommon.getString("select CST from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_TIN"] = clsCommon.getString("select TIN from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["CompanyFSSAI_No"] = clsCommon.getString("select FSSAI_No from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_GST"] = clsCommon.getString("select GST from Company where Company_Code=" + Session["Company_Code"].ToString());
            clsGV.CompanyAddress = clsCommon.getString("select Address_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_Address"] = clsCommon.getString("select Address_E from Company where Company_Code=" + Session["Company_Code"].ToString());

            clsGV.CompanyPhone = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_Phone"] = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Session["Company_Code"].ToString());

            clsGV.CompanyOurGSTNo = clsCommon.getString("select GST from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_OurGSTNo"] = clsCommon.getString("select GST from Company where Company_Code=" + Session["Company_Code"].ToString());

            clsGV.mystatename = clsCommon.getString("select State_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["mystatename"] = clsCommon.getString("select State_E from Company where Company_Code=" + Session["Company_Code"].ToString());

            Session["Company_City"] = clsCommon.getString("select City_E from Company where Company_Code=" + Session["Company_Code"].ToString());
            Session["Company_Pincode"] = clsCommon.getString("select PIN from Company where Company_Code=" + Session["Company_Code"].ToString());



            this.fillDropdownYear();

            modalPoppLogin.Show();
            txtLoginName.Text = string.Empty;
            txtLoginName.Focus();
            lblLoginFailedMsg.Visible = false;
            txtPassword.Attributes.Add("onmousedown", "return noCopyMouse(event);");
            txtPassword.Attributes.Add("onkeydown", "return noCopyKey(event);");
        }
        catch
        {

        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = string.Empty;
            string companyCode = Convert.ToString(Session["Company_Code"].ToString());


            EncryptPass enc = new EncryptPass();
            // msg = AuthenticateUser(txtLoginName.Text, txtPassword.Text, msg);
            //if (msg == "1")
            //{
            //  EncryptPass enc = new EncryptPass();
            //string de = string.Empty;
            //de = enc.Decrypt(de);
            qry = "select * from tblUser where User_Name ='" + txtLoginName.Text + "' and Password = '" + enc.Encrypt(txtPassword.Text) + "' and User_Type!='C'";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Session["user"] = txtLoginName.Text;
                        Session["User_Id"] = dt.Rows[0]["User_Id"].ToString();
                        Session["year"] = drpYear.SelectedValue;
                        Session["Branch_Code"] = "1";
                        Session["User_Type"] = dt.Rows[0]["User_Type"].ToString();
                        Session["User_Security"] ="Y";

                        Session["printinsalebill"] = clsCommon.getString("select printinsalebill from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + " and yearCode=" + Session["year"].ToString());
                        
                        string printinsalebill = Session["printinsalebill"].ToString();

                        if (printinsalebill == "Y")
                        {
                            Session["selectedyear"] = clsCommon.getString("select year from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + " and yearCode=" + Session["year"].ToString());
                        }
                        else
                        {
                            Session["selectedyear"] = string.Empty;
                        }
                        #region for email sending
                        Session["EmailId"] = dt.Rows[0]["EmailId"].ToString();
                        //Session["EmailPassword"] = enc.Decrypt(dt.Rows[0]["EmailPassword"].ToString());
                        Session["EmailPassword"] = dt.Rows[0]["EmailPassword"].ToString();
                        Session["smtpServerPort"] = dt.Rows[0]["smtpServerPort"].ToString();

                        clsGV.EmailId = dt.Rows[0]["EmailId"].ToString();
                        clsGV.EmailPassword = dt.Rows[0]["EmailPassword"].ToString();
                        clsGV.smtpServerPort = dt.Rows[0]["smtpServerPort"].ToString();
                        #endregion
                        #region whatsappapi
                        qry = "select * from tblWhatsAppURL where Company_Code=" + Session["Company_Code"].ToString();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    //Session["Instance_Id"] = dt.Rows[0]["Instance_Id"].ToString();
                                    //Session["Access_token"] = dt.Rows[0]["Access_token"].ToString();
                                    //Session["Mobile_NoWa"] = dt.Rows[0]["Mobile_NoWa"].ToString();
                                    Session["OtpEmail"] = dt.Rows[0]["OtpEmail"].ToString();
                                    Session["OtpPassword"] = dt.Rows[0]["OtpPassword"].ToString();
                                    //Session["gitauthKey"] = dt.Rows[0]["gitauthKey"].ToString();
                                    //Session["gitAuthToken"] = dt.Rows[0]["gitAuthToken"].ToString();
                                    //Session["gitRepo"] = dt.Rows[0]["gitRepo"].ToString();
                                    //Session["WaTitle"] = dt.Rows[0]["WaTitle"].ToString();
                                    //Session["Mobile_No"] = dt.Rows[0]["Mobile_No"].ToString();

                                }
                            }
                        }
                        #endregion
                        string DateFormat = "%d/%m/%Y";
                        // qry = "select DATE_FORMAT(Start_Date, '" + DateFormat + "' ) as Start_Date,DATE_FORMAT(End_Date, '" + DateFormat + "') as End_Date from AccountingYear where yearCode=" + Session["year"].ToString();
                        qry = "select Convert(varchar(10),Start_Date,103 ) as Start_Date,Convert(varchar(10),End_Date,103) as End_Date ,CONVERT(varchar(10),DATEADD(DAY,-1,Start_Date),103) as Start_Date_op from AccountingYear where yearCode=" + Session["year"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString();

                        ds = clsDAL.SimpleQuery(qry);
                        dt = ds.Tables[0];
                        string sd = dt.Rows[0]["Start_Date"].ToString();
                        string ed = dt.Rows[0]["End_Date"].ToString();
                        string sd1 = dt.Rows[0]["Start_Date_op"].ToString();

                        Session["Start_Date"] = sd;
                        Session["End_Date"] = ed;
                        Session["start_date_op"] = sd1;


                        Session["accountingYear"] = sd + "-" + ed;

                        #region global variable declaration
                        clsGV.Start_Date = Session["Start_Date"].ToString();
                        clsGV.End_Date = Session["End_Date"].ToString();

                        DateTime dStart = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                        clsGV.Start_Date = dStart.ToString("dd/MM/yyyy");
                        DateTime dEnd = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                        //                        string todayDate = clsCommon.getString("select date_format(current_date(),'%d/%m/%Y') as todayDt");
                        string todayDate = clsCommon.getString("select Convert(varchar(10),getdate(),103)as todayDt");
                        string companypan = clsCommon.getString("select Pan_No from company");
                        Session["ComapnyPan"] = companypan;
                        todayDate = DateTime.Parse(todayDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                        DateTime dToday = Convert.ToDateTime(todayDate);

                        string dtdate = "";
                        // DateTime Post_Date = DateTime.Now.Subtract(21).ToString();
                        DateTime Post_Date = DateTime.Now + TimeSpan.FromDays(-21);

                        dtdate = Post_Date.ToString("dd/MM/yyyy");

                        Session["Post_Date"] = dtdate;
                        //Session["Outword_Date"] = clsCommon.getString("select date_format(Outword_Date,'%d/%m/%Y') as Outword from Post_Date where Company_Code=" + companyCode);
                        Session["Outword_Date"] = clsCommon.getString("select Convert(varchar(10),Outword_Date,103)as Outword from Post_Date where Company_Code=" + companyCode + " and Year_Code=" + Session["year"].ToString());
                        Session["Inword_Date"] = clsCommon.getString("select Convert(varchar(10),Inword_Date,103)as Inword_Date from Post_Date where Company_Code=" + companyCode + " and Year_Code=" + Session["year"].ToString());

                        // Session["Inword_Date"] = clsCommon.getString("select date_format(Inword_Date,'%d/%m/%Y') as Outword from Post_Date where Company_Code=" + companyCode);

                        //DateTime.Parse(todayDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                        //dToday = Convert.ToDateTime(dToday.ToString("dd/mm/yyyy"));
                        if (dToday < dEnd)
                        {
                            clsGV.To_date = dToday.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            clsGV.To_date = dEnd.ToString("dd/MM/yyyy");
                        }

                        clsGV.Year_Code = Convert.ToInt32(Session["year"].ToString());
                        clsGV.Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
                        Session["Company_Code"] = companyCode;
                        clsGV.user = Session["user"].ToString();
                        clsGV.User_Id = Convert.ToInt32(Session["User_Id"].ToString());
                        string computerName = System.Net.Dns.GetHostName();
                        string VisitorsIPAddr = string.Empty;
                        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        {
                            VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                        }
                        else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                        {
                            VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
                        }
                        clsGV.userInfo = clsGV.User_Id.ToString() + "-" + clsGV.user + "-" + computerName + "-" + VisitorsIPAddr + "-";
                        //userInfo-> clsGV.userInfo
                        ViewState["tblprefix"] = Session["tblPrefix"].ToString();
                        string tblPrefix = ViewState["tblprefix"].ToString(); // Session["tblPrefix"].ToString(); //"NT_1_"; 
                        qry = "select * from " + tblPrefix + "CompanyParameters where Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    Session["DODate"] = clsCommon.getString("select Convert(varchar(10),DODate,103)as Inword_Date from " + tblPrefix +
                                        "CompanyParameters where Company_Code=" + companyCode + " and Year_Code=" + Session["year"].ToString());

                                    //  Session["DODate"] = dt.Rows[0]["DODate"].ToString();
                                    Session["DOPages"] = dt.Rows[0]["DOPages"].ToString();
                                    Session["CGST_RCM_Ac"] = dt.Rows[0]["CGST_RCM_Ac"].ToString();
                                    Session["CGST_RCM_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["CGST_RCM_Ac"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SGST_RCM_Ac"] = dt.Rows[0]["SGST_RCM_Ac"].ToString();
                                    Session["SGST_RCM_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SGST_RCM_Ac"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["IGST_RCM_Ac"] = dt.Rows[0]["IGST_RCM_Ac"].ToString();
                                    Session["IGST_RCM_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["IGST_RCM_Ac"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["ReturnSaleCGST"] = dt.Rows[0]["ReturnSaleCGST"].ToString();
                                    Session["Return_Sale_CGST_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["ReturnSaleCGST"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["ReturnSaleSGST"] = dt.Rows[0]["ReturnSaleSGST"].ToString();
                                    Session["Return_Sale_SGST_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["ReturnSaleSGST"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["ReturnSaleIGST"] = dt.Rows[0]["ReturnSaleIGST"].ToString();
                                    Session["Return_Sale_IGST_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["ReturnSaleIGST"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["ReturnPurchaseCGST"] = dt.Rows[0]["ReturnPurchaseCGST"].ToString();
                                    Session["Return_Purchase_CGST_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["ReturnPurchaseCGST"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["ReturnPurchaseSGST"] = dt.Rows[0]["ReturnPurchaseSGST"].ToString();
                                    Session["Return_Purchase_SGST_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["ReturnPurchaseSGST"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["ReturnPurchaseIGST"] = dt.Rows[0]["ReturnPurchaseIGST"].ToString();
                                    Session["Return_Purchase_IGST_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["ReturnPurchaseIGST"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["Despatch_Type"] = dt.Rows[0]["dispatchType"].ToString();
                                    Session["millpaymentdate"] = dt.Rows[0]["Mill_Payment_date"].ToString();
                                    //  Session["BROKRAGE_AC"] = dt.Rows[0]["BROKRAGE_AC"].ToString();
                                    //   Session["SERVICE_CHARGE_AC"] = dt.Rows[0]["SERVICE_CHARGE_AC"].ToString();
                                    Session["COMMISSION_AC"] = dt.Rows[0]["COMMISSION_AC"].ToString();
                                    Session["commissionid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["COMMISSION_AC"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["INTEREST_AC"] = dt.Rows[0]["INTEREST_AC"].ToString();
                                    Session["INTEREST_ACid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["INTEREST_AC"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["TRANSPORT_AC"] = dt.Rows[0]["TRANSPORT_AC"].ToString();
                                    Session["TRANSPORT_ACid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["TRANSPORT_AC"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["POSTAGE_AC"] = dt.Rows[0]["POSTAGE_AC"].ToString();
                                    Session["POSTAGE_ACid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["POSTAGE_AC"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SELF_AC"] = dt.Rows[0]["SELF_AC"].ToString();
                                    Session["AUTO_VOUCHER"] = dt.Rows[0]["AutoVoucher"].ToString();

                                    string companygstStateCode = dt.Rows[0]["GSTStateCode"].ToString();
                                    Session["CompanyGSTStateCode"] = companygstStateCode;
                                    string statename = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + companygstStateCode + "");
                                    Session["CompanyState"] = statename;

                                    Session["SaleCGSTAc"] = dt.Rows[0]["CGSTAc"].ToString();
                                    Session["SaleCGSTid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SaleCGSTAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SaleSGSTAc"] = dt.Rows[0]["SGSTAc"].ToString();
                                    Session["SaleSGSTid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SaleSGSTAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SaleIGSTAc"] = dt.Rows[0]["IGSTAc"].ToString();
                                    Session["SaleIGSTid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SaleIGSTAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["PurchaseCGSTAc"] = dt.Rows[0]["PurchaseCGSTAc"].ToString();
                                    Session["PurchaseCGSTid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseCGSTAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["PurchaseSGSTAc"] = dt.Rows[0]["PurchaseSGSTAc"].ToString();
                                    Session["PurchaseSGSTid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseSGSTAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["PurchaseIGSTAc"] = dt.Rows[0]["PurchaseIGSTAc"].ToString();
                                    Session["PurchaseIGSTid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseIGSTAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["TCSRate"] = dt.Rows[0]["TCS"].ToString();
                                    Session["PurchaseTCSAc"] = dt.Rows[0]["PurchaseTCSAc"].ToString();
                                    Session["PurchaseTCSAcid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseTCSAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SaleTCSAc"] = dt.Rows[0]["SaleTCSAc"].ToString();
                                    Session["SaleTCSAcid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SaleTCSAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["Edit_Sale_Rate"] = dt.Rows[0]["Edit_Sale_Rate"].ToString();
                                  
                                    Session["customisesb"] = dt.Rows[0]["customisesb"].ToString();
                                    Session["customisedo"] = dt.Rows[0]["customisedo"].ToString();

                                    if (dt.Rows[0]["Freight_Ac"].ToString() != string.Empty)
                                    {
                                        Session["Freight_Ac"] = dt.Rows[0]["Freight_Ac"].ToString();
                                    }
                                    else
                                    {
                                        Session["Freight_Ac"] = "0";
                                    }
                                    Session["Freight_Acid"] = clsCommon.getString("select isnull(accoid,0) from qrymstaccountmaster where Ac_Code='" + Session["Freight_Ac"].ToString() + "' and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    if (dt.Rows[0]["RoundOff"].ToString() != string.Empty)
                                    {
                                        Session["RoundOff"] = dt.Rows[0]["RoundOff"].ToString();
                                    }
                                    else
                                    {
                                        Session["RoundOff"] = "0";
                                    }
                                    Session["RoundOffid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["RoundOff"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["OTHER_Ac"] = dt.Rows[0]["OTHER_AMOUNT_AC"].ToString();
                                    Session["OTHER_Acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["OTHER_Ac"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["MarketSase"] = dt.Rows[0]["MarketSase"].ToString();
                                    Session["MarketSaseid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["MarketSase"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SuperCost"] = dt.Rows[0]["SuperCost"].ToString();
                                    Session["SuperCostid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SuperCost"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["Packing"] = dt.Rows[0]["Packing"].ToString();
                                    Session["Packingid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["Packing"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["Hamali"] = dt.Rows[0]["Hamali"].ToString();
                                    Session["Hamaliid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["Hamali"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["CASHID"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=1 and Company_Code=" + Session["Company_Code"].ToString() + "");
                                    Session["SELFID"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=2 and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["TransportTDS_Ac"] = dt.Rows[0]["TransportTDS_Ac"].ToString();
                                    Session["transportTdsid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["TransportTDS_Ac"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["TransportTDS_AcCut"] = dt.Rows[0]["TransportTDS_AcCut"].ToString();
                                    Session["transportTdsCutid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["TransportTDS_AcCut"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");


                                    Session["TransportTDS_Ac"] = dt.Rows[0]["TransportTDS_Ac"].ToString();
                                    Session["transportTdsid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["TransportTDS_Ac"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["TransportTDS_AcCut"] = dt.Rows[0]["TransportTDS_AcCut"].ToString();
                                    Session["transportTdsCutid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["TransportTDS_AcCut"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["SaleTDSAc"] = dt.Rows[0]["SaleTDSAc"].ToString();
                                    Session["SaleTDSRate"] = dt.Rows[0]["SaleTDSRate"].ToString();
                                    Session["SaleTDSacid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["SaleTDSAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["PurchaseTDSAc"] = dt.Rows[0]["PurchaseTDSAc"].ToString();
                                    Session["PurchaseTDSRate"] = dt.Rows[0]["PurchaseTDSRate"].ToString();
                                    Session["PurchaseTDSacid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseTDSAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                                    Session["BalanceLimit"] = dt.Rows[0]["BalanceLimit"].ToString();
                                    Session["RateDiffAc"] = dt.Rows[0]["RateDiffAc"].ToString();

                                    Session["RateDiffAcid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["RateDiffAc"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                    Session["Jaggary_TCSRate"] = dt.Rows[0]["Jaggary_TCS"].ToString();
                                    //Session["Jaggary_PurchaseTCSAc"] = dt.Rows[0]["Jaggary_PurchaseTCSAc"].ToString();
                                    //Session["Jaggary_SaleTCSAc"] = dt.Rows[0]["Jaggary_SaleTCSAc"].ToString();
                                    //Session["Jaggary_SaleTDSAc"] = dt.Rows[0]["Jaggary_SaleTDSAc"].ToString();
                                    //Session["Jaggary_SaleTDSRate"] = dt.Rows[0]["Jaggary_SaleTDSRate"].ToString();
                                    //Session["Jaggary_GSTCode"] = dt.Rows[0]["Jaggary_GSTCode"].ToString();
                                }
                            }
                        }
                        qry = "select * from NT_1_JaggryCompanyParameters where Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    Session["Jaggary_TCSRate"] = dt.Rows[0]["Jaggary_TCS"].ToString();
                                    Session["Jaggary_PurchaseTCSAc"] = dt.Rows[0]["Jaggary_PurchaseTCSAc"].ToString();
                                    Session["Jaggary_SaleTCSAc"] = dt.Rows[0]["Jaggary_SaleTCSAc"].ToString();
                                    Session["Jaggary_SaleTDSAc"] = dt.Rows[0]["Jaggary_SaleTDSAc"].ToString();
                                    Session["Jaggary_SaleTDSRate"] = dt.Rows[0]["Jaggary_SaleTDSRate"].ToString();
                                    Session["Jaggary_GSTCode"] = dt.Rows[0]["Jaggary_GSTCode"].ToString();
                                }
                            }
                        }

                        //qry = "select * from UserSecurity where Company_Code='" + Session["Company_Code"].ToString() + "'";
                        //ds = clsDAL.SimpleQuery(qry);
                        //if (ds != null)
                        //{
                        //    if (ds.Tables.Count > 0)
                        //    {
                        //        dt = ds.Tables[0];
                        //        if (dt.Rows.Count > 0)
                        //        {
                        //            Session["IPAddress1"] = dt.Rows[0]["IPAddress1"].ToString();
                        //            Session["IPAddress2"] = dt.Rows[0]["IPAddress2"].ToString();
                        //        }
                        //    }
                        //}
                        #region smsapi
                        qry = "select smsApi,Sender_id,Accusage from eway_bill where Company_Code=" + Session["Company_Code"].ToString();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    Session["smsApi"] = dt.Rows[0]["smsApi"].ToString();
                                    Session["Sender_id"] = dt.Rows[0]["Sender_id"].ToString();
                                    Session["Accusage"] = dt.Rows[0]["Accusage"].ToString();
                                }
                            }
                        }
                        #endregion
                        #endregion
                        modalPoppLogin.Hide();
                        txtLoginName.Text = "";
                        txtPassword.Text = "";
                        Response.Redirect("~/Sugar/pgeUserSecurity.aspx", false);
                    }
                    else
                    {
                        //Session["Company_Code"] = null;
                        //Session["user"] = null;
                        //Session["year"] = null;
                        modalPoppLogin.Show();
                        txtLoginName.Focus();
                        lblLoginFailedMsg.Visible = true;
                    }
                }
                else
                {
                    //Session["Company_Code"] = null;
                    //Session["user"] = null;
                    //Session["year"] = null;
                    modalPoppLogin.Show();
                    txtLoginName.Focus();
                    lblLoginFailedMsg.Visible = true;
                }
            }
            else
            {
                //Session["Company_Code"] = null;
                //Session["user"] = null;
                //Session["year"] = null;
                modalPoppLogin.Show();
                txtLoginName.Focus();
                lblLoginFailedMsg.Visible = true;
            }
            //}
            //else
            //{
            //    lblLoginFailedMsg.Text = msg;
            //    modalPoppLogin.Show();
            //    txtLoginName.Focus();
            //    lblLoginFailedMsg.Visible = true;
            //}
        }
        catch
        {
            Response.Redirect("~/Sugar/pgeUserSecurity.aspx", false);
        }
    }

    public static string AuthenticateUser(string Username, string Password, string msg)
    {
        msg = clsDAL.AuthenticateUser("spAuthenticateUser", Username, Password, msg);
        return msg;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        modalPoppLogin.Hide();
    }

    protected void lnkCreateCompany_Click(object sender, EventArgs e)
    {
        Session["Company_Code"] = null;
        if (Session["User_Type"] != null)
        {
            string uType = Session["User_Type"].ToString();
            if (uType == "A")
            {
                Response.Redirect("pgeCreateNewCompany.aspx");
            }
            else
            {
                modalPoppLogin.Show();
            }
        }
        else
        {
            modalPoppLogin.Show();
        }
    }
    protected void lnkNewUser_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Company_Code"] = null;
        }
        catch
        {

        }
    }

    protected void lnkCreateBranch_Click(object sender, EventArgs e)
    {
        Session["Company_Code"] = null;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = false;

            string qry = "";
            qry = "";
            string User_Id = "0";

            #region validation
            if (txtOldPassword.Text != string.Empty)
            {
                string str = clsCommon.getString("select User_Id from tblUser where User_Name='" + txtCUserName.Text + "' and Password='" + txtOldPassword.Text + "'");
                if (str != string.Empty)
                {
                    User_Id = str;
                    isValidated = true;
                    lblerrChangePassword.Visible = false;
                }
                else
                {
                    isValidated = false;
                    txtOldPassword.Text = string.Empty;
                    txtOldPassword.Focus();
                    lblerrChangePassword.Visible = true;
                    return;
                }
            }
            else
            {
                lblerrChangePassword.Visible = false;
                isValidated = false;
                txtOldPassword.Focus();
                return;
            }
            if (txtNewPassword.Text != string.Empty)
            {
                if (txtnewCPassword.Text == txtnewCPassword.Text)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    txtnewCPassword.Text = string.Empty;
                    txtnewCPassword.Focus();
                    return;
                }
            }
            else
            {
                isValidated = false;
                txtNewPassword.Focus();
                return;
            }

            #endregion

            #region update password
            if (isValidated == true)
            {
                try
                {
                    qry = "";
                    DataSet ds = new DataSet();
                    qry = "update tblUser set Password='" + txtNewPassword.Text + "' where User_Id='" + User_Id + "'";
                    ds = clsDAL.SimpleQuery(qry);

                    txtCUserName.Text = string.Empty;
                    txtOldPassword.Text = string.Empty;
                    txtnewCPassword.Text = string.Empty;
                    txtNewPassword.Text = string.Empty;
                    popupChangePassword.Hide();
                    modalPoppLogin.Show();
                    //document.getElementById("iframe_id").contentWindow.location.href
                }
                catch
                {

                }
            }
            #endregion
        }
        catch
        {

        }
    }

    protected void btnCancelNewPass_Click(object sender, EventArgs e)
    {
        try
        {
            txtCUserName.Text = string.Empty;
            txtOldPassword.Text = string.Empty;
            txtnewCPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            popupChangePassword.Hide();
            modalPoppLogin.Show();
        }
        catch
        {

        }
    }
    protected void lnkChangePass_Click(object sender, EventArgs e)
    {
        try
        {
            modalPoppLogin.Hide();
            popupChangePassword.Show();
            txtCUserName.Focus();
        }
        catch
        {

        }
    }
    protected void lnkForgotPass_Click(object sender, EventArgs e)
    {
        mpForgetPass.Show();
    }
    protected void btnSendForgetedPass_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserNameForgot.Text != string.Empty)
            {
                string username = txtUserNameForgot.Text.Trim();
                string UserPassword = clsCommon.getString("Select Password from tblUser where User_Name='" + username + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                if (UserPassword == string.Empty)
                {
                    lblWrongUserName.Text = "User Name is InCorrect!";
                    mpForgetPass.Show();
                }
                else
                {
                    string mobile = clsCommon.getString("Select Mobile from tblUser where User_Name='" + username + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (mobile == string.Empty)
                    {
                        lblWrongUserName.Text = "Mobile Number is not Present Please Contact Admin!";
                        mpForgetPass.Show();
                    }
                    else
                    {
                        string API = clsGV.msgAPI;
                        string Url = API + "mobile=" + mobile + "&message=" + "Hello " + username + ",Your Password is : " + UserPassword + " Please Dont Forget again.." + "&senderid=NAVKAR&accusage=1";
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        StreamReader reader = new StreamReader(myResp.GetResponseStream());
                        string respString = reader.ReadToEnd();
                        reader.Close();
                        myResp.Close();
                        modalPoppLogin.Show();
                    }
                }

            }
            else
            {
                lblWrongUserName.Text = "Please Enter User Name!";
                mpForgetPass.Show();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        modalPoppLogin.Show();
    }
    private void UserSecurity()
    {
        try
        {
            string qry = "select * from UserSecurity where Company_Code='" + Session["Company_Code"].ToString() + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Session["IPAddress1"] = dt.Rows[0]["IPAddress1"].ToString();
                        Session["IPAddress2"] = dt.Rows[0]["IPAddress2"].ToString();
                    }
                }
            }
        }
        catch
        {

        }
    }
}