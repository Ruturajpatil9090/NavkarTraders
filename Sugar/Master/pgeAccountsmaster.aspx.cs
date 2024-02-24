using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Services;
using System.Web.Script.Serialization;
public partial class pgeAccountsmaster : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string GroupMasterTable = string.Empty;
    string AcGroupsTable = string.Empty;
    string SystemMastertable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextbox = string.Empty;
    string qryDisplay = string.Empty;
    string docno = string.Empty;

    string strTextBox = string.Empty;
    int defaultAccountCode = 0;
    string GLedgerTable = string.Empty;
    string TranTyp = "OP";
    string qry = string.Empty;
    string Debit = string.Empty;
    string Credit = string.Empty;
    string DRCRDiff = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
    string f_pan = "";
    string f_Main = "~/PAN/" + clsGV.user;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;
    string AccountNo = string.Empty;
    string cs = string.Empty;
    int count = 0;
    int Doc_No = 0;
    int Acid = 0;
    int bsGroup_Id = 0;
    int cityid = 0;
    string Head_Insert = string.Empty;
    string Head_Update = string.Empty;
    string Head_Delete = string.Empty;
    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    string Detail_Insert = string.Empty;
    string Detail_Update = string.Empty;
    string Detail_Delete = string.Empty;

    string Group_Insert = string.Empty;
    string Group_Update = string.Empty;
    string Group_Delete = string.Empty;

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;

    #endregion

    #region -Head part declearation
    Int32 AC_CODE = 0;
    string Limit_By = string.Empty;
    string AC_TYPE = string.Empty;
    double AC_RATE = 0.00;
    string AC_NAME_E = string.Empty;
    string AC_NAME_R = string.Empty;
    double COMMISSION = 0.00;
    string SHORT_NAME = string.Empty;
    string ADDRESS_E = string.Empty;
    string ADDRESS_R = string.Empty;
    Int32 CITY_CODE = 0;
    Int32 PINCODE = 0;
    double OPENING_BALANCE = 0.00;
    double DISTANCE = 0.00;
    int GSTStateCode = 0;
    double Branch1OB = 0.00;
    string Branch1Drcr = string.Empty;
    double Branch2OB = 0.00;
    string Branch2Drcr = string.Empty;
    Int32 GROUP_CODE = 0;
    string LOCAL_LIC_NO = string.Empty;
    string BANK_NAME = string.Empty;
    string TIN_NO = string.Empty;
    string BANK_AC_NO = string.Empty;
    string CST_NO = string.Empty;
    string EMAIL_ID = string.Empty;
    string GST_NO = string.Empty;
    string EMAIL_ID_CC = string.Empty;
    string ADHARNO = string.Empty;
    string OTHER_NARRATIOM = string.Empty;
    string ECC_NO = string.Empty;
    string MOBILE = string.Empty;
    string IFSC = string.Empty;
    string FSSAI = string.Empty;
    string TANNO = string.Empty;
    double BANK_OPENING = 0.00;
    string BANK_OP_DRCR = string.Empty;
    string carporate_party = string.Empty;
    string WHATSUPNO = string.Empty;
    int UnregisterGST = 0;

    int locked = 0;

    string referBy = string.Empty;
    string OffPhone = string.Empty;
    string Fax = string.Empty;
    string PanLink = string.Empty;
    string CompanyPan = string.Empty;

    string AC_Pan = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    int Company_Code = 0;
    int Year_Code = 0;
    int year_Code = 0;
    int Branch_Code = 0;
    string Branch1DrCr = string.Empty;
    string Branch2DrCr = string.Empty;

    string GlegerNarration = string.Empty;
    string Created_By = string.Empty;
    string Modified_By = string.Empty;

    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    string op = string.Empty;
    string returnmaxno = string.Empty;
    int flag = 0;
    string DRCR = string.Empty;
    string msg = string.Empty;
    #endregion-End of Head part declearation

    #region Detail Declaration
    Int32 personId = 0;
    string personName = "";
    string mobile = "";
    string email = "";
    string pan = "";
    string other = "";
    string i_d = "";
    string DOC_DATE = null;

    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;

    int personid_No = 2;
    int personName_E = 3;
    int mobile_No = 4;
    int email_Id = 5;
    int pan_No = 6;
    int other_Info = 7;
    int id = 8;
    int rowAction = 9;
    int SrNo = 10;

    #endregion

    #region Group Detail_Values
    StringBuilder Group_Fields = null;
    StringBuilder Group_Values = null;
    string Group_Code = string.Empty;

    #endregion
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;
    string customercode = string.Empty;

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "AccountMaster";
            tblDetails = tblPrefix + "AcContacts";
            GroupMasterTable = tblPrefix + "BSGroupMaster";
            AcGroupsTable = tblPrefix + "AcGroups";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMastertable = tblPrefix + "SystemMaster";
            qryCommon = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();

            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);

            Group_Fields = new StringBuilder();
            Group_Values = new StringBuilder();
            Head_Fields = new StringBuilder();
            Head_Values = new StringBuilder();
            Detail_Fields = new StringBuilder();
            Detail_Values = new StringBuilder();
            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            customercode = Request.QueryString["CustomerCode"];
            // hdnfcustomercode.Value = customercode;
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                hdnfcustomercode.Value = customercode;

                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["Ac_Code"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");

                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        //this.DebitCreditDiff();
                        this.fillGroupsGrid();
                        this.showLastRecord();
                        this.enableDisableNavigateButtons();
                        setFocusControl(btnEdit);
                    }
                    else
                    //{
                    //    if (customercode != string.Empty)
                    //    {

                    //        hdnf.Value = Request.QueryString["Ac_Code"];
                    //        pnlPopup.Style["display"] = "none";
                    //        ViewState["currentTable"] = null;
                    //        //clsButtonNavigation.enableDisable("N");

                    //        this.makeEmptyForm("A");
                    //        ViewState["mode"] = "I";
                    //        //this.DebitCreditDiff();
                    //        this.fillGroupsGrid();
                    //        this.showLastRecord();

                    //        setFocusControl(drpType);
                    //    }
                    //    else
                        {
                            //condition
                            clsButtonNavigation.enableDisable("A");
                            ViewState["mode"] = null;
                            ViewState["mode"] = "I";
                            this.makeEmptyForm("A");
                            this.NextNumber();
                            this.fillGroupsGrid();
                            btntxtAC_CODE.Enabled = false;
                            //txtGROUP_CODE.Text = "24";
                            //lblGROUPNAME.Text = clsCommon.getString("Select group_Name_E from " + tblPrefix + "BSGroupMaster where group_Code=24 and Company_Code="
                            //    + Convert.ToInt32(Session["Company_Code"].ToString()));
                            pnlPopupDetails.Style["display"] = "none";
                            drpType_SelectedIndexChanged(this, new EventArgs());

                        }
                        // drpDrCr.Enabled = true;
                        // disableOpening();
                    //}

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            if (objAsp != null)
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
            {
                pnlPopup.Style["display"] = "none";
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                objAsp = btnSearch;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void DebitCreditDiff()
    {
        try
        {
            Debit = Convert.ToString(clsCommon.getString("select SUM(AMOUNT) from " + tblPrefix + "GLEDGER where COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Drcr='D' and Tran_Type='OP'"));
            if (Debit == string.Empty)
            {
                Debit = "0";
            }
            Credit = Convert.ToString(clsCommon.getString("select SUM(AMOUNT) from " + tblPrefix + "GLEDGER where COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Drcr='C' and Tran_Type='OP'"));
            if (Credit == string.Empty)
            {
                Credit = "0";
            }
            DRCRDiff = Convert.ToString(Convert.ToDouble(Debit) - Convert.ToDouble(Credit));
            double value = 0;
            double diffn = double.Parse(DRCRDiff);
            if (diffn < 0)
            {
                value = Math.Abs(Math.Round(diffn, 2));
                lblDRCRDiff.Text = Convert.ToString(value);
                lblDRCRDiff.ForeColor = Color.Red;
            }
            else
            {
                value = Math.Abs(Math.Round(diffn, 2));
                lblDRCRDiff.Text = Convert.ToString(value);
                lblDRCRDiff.ForeColor = Color.Yellow;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void fillGroupsGrid()
    {
        try
        {
            string qry = "select System_Code,System_Name_E,'' as a from " + SystemMastertable + " where System_Type='G' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            grdGroup.DataSource = ds;
            grdGroup.DataBind();
        }
        catch
        {

        }

    }
    #endregion

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code>100";
                obj.code = "Ac_Code";
                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    if (ds.Tables[0].Rows[0][0].ToString() != "1")
                                    {
                                        txtAC_CODE.Text = ds.Tables[0].Rows[0][0].ToString();
                                        txtAC_CODE.Enabled = false;
                                    }
                                    else
                                    {
                                        txtAC_CODE.Text = "101";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [makeEmptyForm]
    private void 
        makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                btntxtGstStateCode.Enabled = false;
                txtEditDoc_No.Enabled = true;
                pnlPopup.Style["display"] = "none";
                btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                btntxtAC_CODE.Text = "Choose No";
                btntxtAC_CODE.Enabled = true;
                btnAddCity.Enabled = false;
                btnAddGroup.Enabled = false;
                txtAC_CODE.Enabled = false;
                lblMsg.Text = string.Empty;

                txtSendingAcCode.Enabled = true;
                #region logic
                btntxtCITY_CODE.Enabled = false;
                btntxtGROUP_CODE.Enabled = false;
                drpType.Enabled = false;
                drpDrCr.Enabled = false;
                lblCITYNAME.Text = string.Empty;
                lblGROUPNAME.Text = string.Empty;
                pnlGroup.Enabled = false;
                btntxtAC_CODE.Enabled = false;
                chkCarporate.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                #endregion
                txtADDRESS_R.Enabled = false;
                txtBANK_OPENING.Enabled = false;
                drpBankDrCr.Enabled = false;
                drpLimit.Enabled = false;

                chkUnregisterGST.Enabled = false;
                chkLocked.Enabled = false;
            }
            if (dAction == "A")
            {
                // drpType.SelectedIndex = 0;
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {

                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        //((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btntxtGstStateCode.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lbltxtGstStateName.Text = "";
                btnSave.Text = "Save";
                btntxtAC_CODE.Text = "Change No";
                txtAC_CODE.Enabled = false;
                btnOpenDetailsPopup.Enabled = true;
                txtSendingAcCode.Enabled = true;
                #region set Business logic for add
                setFocusControl(drpType);
                btntxtCITY_CODE.Enabled = true;
                btntxtGROUP_CODE.Enabled = true;
                lblCITYNAME.Text = string.Empty;
                lblGROUPNAME.Text = string.Empty;
                drpType.Enabled = true;
                drpDrCr.Enabled = true;

                pnlGroup.Enabled = true;
                chkCarporate.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                btnOpenDetailsPopup.Enabled = true;
                chkCarporate.Checked = false;
                btnAddCity.Enabled = true;
                btnAddGroup.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                for (int i = 0; i < grdGroup.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdGroup.Rows[i].Cells[2].FindControl("chk");
                    chk.Checked = false;
                }
                #endregion
                txtADDRESS_R.Enabled = true;

                txtOPENING_BALANCE.Enabled = true;
                drpDrCr.Enabled = true;
                btntxtAC_CODE.Enabled = false;

                txtBANK_OPENING.Enabled = false;
                drpBankDrCr.Enabled = false;
                drpLimit.Enabled = true;
                btntxtCITY_CODE.Enabled = true;


                txtAC_RATE.Text = string.Empty;

                txtCOMMISSION.Text = string.Empty;
                txtInsurance.Text = string.Empty;
                txtADDRESS_E.Text = string.Empty;
                txtADDRESS_R.Text = string.Empty;
                txtCITY_CODE.Text = string.Empty;

                txtPINCODE.Text = string.Empty;
                txtGstStateCode.Text = string.Empty;
                btntxtGstStateCode.Text = string.Empty;
                txtDistance.Text = string.Empty;
                chkCarporate.Text = string.Empty;
                txtLOCAL_LIC_NO.Text = string.Empty;
                txtRefBy.Text = string.Empty;
                txtcompanyPan.Text = string.Empty;
                txtFssaiNo.Text = string.Empty;
                txtGST_NO.Text = string.Empty;
                txtAdhar_No.Text = string.Empty;
                chkUnregisterGST.Text = string.Empty;
                txtIfsc.Text = string.Empty;
                txtBANK_AC_NO.Text = string.Empty;
                txtBANK_NAME.Text = string.Empty;
                txtOTHER_NARRATION.Text = string.Empty;
                chkLocked.Text = string.Empty;
                txtEMAIL_ID.Text = string.Empty;
                txtEMAIL_ID_CC.Text = string.Empty;
                txtOffPhone.Text = string.Empty;
                txtfax.Text = string.Empty;
                txtPanLink.Text = string.Empty;
                txtMOBILE.Text = string.Empty;
                txtwhatsup_No.Text = string.Empty;
                chkUnregisterGST.Text = string.Empty;
                chkLocked.Text = string.Empty;
                txtSendingEmail.Text = string.Empty;
                txtSendingMobile.Text = string.Empty;
                txtGROUP_CODE.Text = string.Empty;
                txtAC_NAME_E.Text = string.Empty;
                txtAC_NAME_R.Text = string.Empty;
                txtSHORT_NAME.Text = string.Empty;
                hdnfCity.Value = "";
                chkUnregisterGST.Checked = false;
                chkLocked.Checked = false;
                txtTan_No.Text = string.Empty;

            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtGstStateCode.Enabled = false;
                txtEditDoc_No.Enabled = true;
                btntxtAC_CODE.Text = "Choose No";
                txtAC_CODE.Enabled = false;
                btntxtAC_CODE.Enabled = false;
                btnAddCity.Enabled = false;
                btnAddGroup.Enabled = false;
                txtADDRESS_R.Enabled = false;
                txtSendingAcCode.Enabled = true;
                #region logic
                btntxtCITY_CODE.Enabled = false;
                btntxtGROUP_CODE.Enabled = false;
                drpType.Enabled = false;
                drpDrCr.Enabled = false;
                //lblCITYNAME.Text = string.Empty;
                //lblGROUPNAME.Text = string.Empty;
                pnlGroup.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                chkCarporate.Enabled = false;
                #endregion

                txtBANK_OPENING.Enabled = false;
                drpBankDrCr.Enabled = false;
                drpLimit.Enabled = false;
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {

                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;

                    }
                }
                btntxtGstStateCode.Enabled = true;
                txtEditDoc_No.Enabled = false;
                btntxtAC_CODE.Text = "Choose No";
                txtAC_CODE.Enabled = false;
                lblMsg.Text = string.Empty;
                txtAC_NAME_R.Enabled = true;

                txtSHORT_NAME.Enabled = true;
                btnAddCity.Enabled = true;
                btnAddGroup.Enabled = true;
                txtSendingAcCode.Enabled = true;
                #region logic
                drpDrCr.Enabled = true;
                btntxtGROUP_CODE.Enabled = true;
                drpType.Enabled = true;


                #endregion
                txtGST_NO.Enabled = true;
                txtLOCAL_LIC_NO.Enabled = true;

                txtBANK_OPENING.Enabled = false;
                drpBankDrCr.Enabled = false;
                drpLimit.Enabled = true;
            }
            string s_item = drpType.SelectedValue;
            checkTextBox(dAction, s_item);

            #region always check
            //if (dAction == "A" || dAction == "E")
            //{
            //    string s_item = drpType.SelectedValue;
            //    if (s_item == "I")
            //    {
            //        setFocusControl(txtAC_RATE);
            //        //    txtCOMMISSION.Text = "";
            //        txtCOMMISSION.Enabled = false;
            //        //    txtADDRESS_E.Text = "";
            //        txtADDRESS_E.Enabled = false;
            //        //     txtADDRESS_R.Text = "";
            //        // txtADDRESS_R.Enabled = false;
            //        //     txtCITY_CODE.Text = "";
            //        //      lblCITYNAME.Text = "";
            //        txtCITY_CODE.Enabled = false;
            //        //     txtPINCODE.Text = "";
            //        txtPINCODE.Enabled = false;
            //        //     txtOPENING_BALANCE.Text = "";
            //        txtOPENING_BALANCE.Enabled = false;
            //        drpDrCr.Enabled = false;
            //        //    txtLOCAL_LIC_NO.Text = "";
            //        txtLOCAL_LIC_NO.Enabled = false;
            //        //   txtTIN_NO.Text = "";

            //        //    txtGST_NO.Text = "";
            //        txtGST_NO.Enabled = false;
            //        //    txtBANK_OPENING.Text = "";
            //        // txtBANK_OPENING.Enabled = false;
            //        //drpBankDrCr.Enabled = false;
            //    }
            //    else if (drpType.SelectedValue != "F")
            //    {
            //        //  txtCOMMISSION.Text = "";
            //        txtCOMMISSION.Enabled = true;
            //        //    txtADDRESS_E.Text = "";
            //        txtADDRESS_E.Enabled = true;
            //        //    txtADDRESS_R.Text = "";
            //        // txtADDRESS_R.Enabled = true;
            //        //    txtCITY_CODE.Text = "";
            //        //    lblCITYNAME.Text = "";
            //        txtCITY_CODE.Enabled = true;
            //        //     txtPINCODE.Text = "";
            //        txtPINCODE.Enabled = true;
            //        //     txtOPENING_BALANCE.Text = "";
            //        txtOPENING_BALANCE.Enabled = true;
            //        drpDrCr.Enabled = true;
            //        //   txtLOCAL_LIC_NO.Text = "";
            //        txtLOCAL_LIC_NO.Enabled = true;
            //        //   txtTIN_NO.Text = "";

            //        //    txtGST_NO.Text = "";
            //        txtGST_NO.Enabled = true;
            //        //    //     txtBANK_OPENING.Text = "";
            //        //  txtBANK_OPENING.Enabled = true;
            //        //  drpBankDrCr.Enabled = true;
            //        grdGroup.Enabled = true;
            //        btntxtAC_CODE.Enabled = true;
            //    }
            //    if (s_item == "B")
            //    {
            //        //   txtBANK_OPENING.Text = "";
            //        //  txtBANK_OPENING.Enabled = true;
            //        // drpBankDrCr.Enabled = true;
            //    }
            //    else
            //    {
            //        //   txtBANK_OPENING.Text = "";
            //        //txtBANK_OPENING.Enabled = false;
            //        // drpBankDrCr.Enabled = false;
            //    }

            //    if (s_item == "F" || s_item == "I")
            //    {
            //        btntxtAC_CODE.Enabled = true;

            //        setFocusControl(txtAC_RATE);
            //        //  txtAC_RATE.Text = "";
            //        txtAC_RATE.Enabled = true;
            //    }
            //    else
            //    {
            //        // txtAC_RATE.Text = "";
            //        txtAC_RATE.Enabled = false;
            //    }
            //    if (s_item == "O" || s_item == "E")
            //    {
            //        btntxtAC_CODE.Enabled = true;

            //        TradingAndExpensesControls();
            //    }
            //}

            #endregion
        }
        catch
        {
        }
    }

    private void checkTextBox(string dAction, string s_item)
    {
        try
        {
            if (dAction == "A" || dAction == "E" || dAction == "")
            {
                if (Session["year"].ToString() != "1")
                {
                    txtOPENING_BALANCE.Enabled = false;
                    drpDrCr.Enabled = false;
                    // txtGROUP_CODE.Enabled = false;
                    //btntxtGROUP_CODE.Enabled = false;
                }

                if (s_item == "P" || s_item == "S")
                {
                    btntxtCITY_CODE.Enabled = true;
                    txtSendingMobile.Enabled = true;
                    txtSendingEmail.Enabled = true;
                    chkUnregisterGST.Enabled = true;
                    chkLocked.Enabled = true;
                    chkCarporate.Enabled = true;
                    // drpDrCr.Enabled = false;
                    txtSHORT_NAME.Enabled = true;
                }
                if (s_item == "B")
                {
                    drpLimit.Enabled = false;
                    txtCOMMISSION.Enabled = false;
                    txtInsurance.Enabled = false;
                    txtBANK_OPENING.Enabled = true;
                    drpBankDrCr.Enabled = true;

                    chkCarporate.Enabled = false;
                    txtLOCAL_LIC_NO.Enabled = false;
                    txtFssaiNo.Enabled = false;
                    btntxtCITY_CODE.Enabled = true;
                    chkUnregisterGST.Enabled = true;
                    chkLocked.Enabled = true;
                    txtSHORT_NAME.Enabled = true;
                    txtTan_No.Enabled = false;
                    // chkCarporate.Enabled = true;
                }
                if (s_item == "C" || s_item == "F")
                {
                    drpLimit.Enabled = false;
                    if (s_item == "C")
                    {
                        txtAC_RATE.Enabled = false;
                    }
                    else
                    {
                        txtOPENING_BALANCE.Enabled = false;
                        drpDrCr.Enabled = false;
                    }
                    txtCOMMISSION.Enabled = false;
                    txtInsurance.Enabled = false;
                    txtADDRESS_E.Enabled = false;
                    txtADDRESS_R.Enabled = false;
                    txtCITY_CODE.Enabled = false;
                    btnAddCity.Enabled = false;
                    btntxtCITY_CODE.Enabled = false;
                    txtPINCODE.Enabled = false;
                    txtGstStateCode.Enabled = false;
                    btntxtGstStateCode.Enabled = false;
                    txtDistance.Enabled = false;
                    chkCarporate.Enabled = false;
                    txtLOCAL_LIC_NO.Enabled = false;
                    txtRefBy.Enabled = false;
                    txtcompanyPan.Enabled = false;
                    txtFssaiNo.Enabled = false;
                    txtGST_NO.Enabled = false;
                    txtAdhar_No.Enabled = false;
                    chkUnregisterGST.Enabled = false;
                    txtIfsc.Enabled = false;
                    txtBANK_AC_NO.Enabled = false;
                    txtBANK_NAME.Enabled = false;
                    txtOTHER_NARRATION.Enabled = false;
                    chkLocked.Enabled = false;
                    txtEMAIL_ID.Enabled = false;
                    txtEMAIL_ID_CC.Enabled = false;
                    txtOffPhone.Enabled = false;
                    txtfax.Enabled = false;
                    txtPanLink.Enabled = false;
                    txtMOBILE.Enabled = false;
                    txtwhatsup_No.Enabled = false;
                    pnlGroup.Enabled = false;
                    pnlgrdDetail.Enabled = false;
                    grdDetail.Enabled = false;
                    btnOpenDetailsPopup.Enabled = false;

                    txtSendingEmail.Enabled = false;
                    txtSendingMobile.Enabled = false;
                    //txtGROUP_CODE.Enabled = false;
                    //btntxtGROUP_CODE.Enabled = false;
                    btnAddGroup.Enabled = false;
                    txtSHORT_NAME.Enabled = true;
                    //btntxtCITY_CODE.Enabled = true;
                    txtTan_No.Enabled = false;
                }
                if (s_item == "R")
                {
                    drpLimit.Enabled = false;
                    txtCOMMISSION.Enabled = false;
                    txtInsurance.Enabled = false;
                    btntxtCITY_CODE.Enabled = true;
                    chkUnregisterGST.Enabled = true;
                    chkLocked.Enabled = true;
                    chkCarporate.Enabled = true;
                    txtSHORT_NAME.Enabled = true;
                }
                if (s_item == "I" || s_item == "M" || s_item == "BR" || s_item == "T")
                {
                    if (s_item == "I" || s_item == "T")
                    {
                        drpLimit.Enabled = false;
                    }
                    else
                    {
                        drpLimit.Enabled = true;
                    }
                    if (s_item == "T")
                    {
                        txtGST_NO.Enabled = true;
                        txtLOCAL_LIC_NO.Enabled = true;
                        btntxtCITY_CODE.Enabled = true;
                    }
                    btntxtCITY_CODE.Enabled = true;
                    chkUnregisterGST.Enabled = true;
                    chkLocked.Enabled = true;
                    chkCarporate.Enabled = true;
                    txtSHORT_NAME.Enabled = true;
                }
                if (s_item == "E" || s_item == "O")
                {
                    drpLimit.Enabled = false;
                    txtAC_RATE.Enabled = false;

                    txtOPENING_BALANCE.Enabled = false;
                    drpDrCr.Enabled = false;
                    txtCOMMISSION.Enabled = true;
                    txtInsurance.Enabled = true;
                    txtADDRESS_E.Enabled = false;
                    txtADDRESS_R.Enabled = false;
                    txtCITY_CODE.Enabled = false;
                    btntxtCITY_CODE.Enabled = false;
                    btnAddCity.Enabled = false;
                    txtPINCODE.Enabled = false;
                    txtGstStateCode.Enabled = false;
                    btntxtGstStateCode.Enabled = false;
                    txtDistance.Enabled = false;
                    chkCarporate.Enabled = false;
                    txtLOCAL_LIC_NO.Enabled = false;
                    txtRefBy.Enabled = false;
                    txtcompanyPan.Enabled = false;
                    txtFssaiNo.Enabled = false;
                    txtGST_NO.Enabled = false;
                    txtAdhar_No.Enabled = false;
                    chkUnregisterGST.Enabled = false;
                    txtIfsc.Enabled = false;
                    txtBANK_AC_NO.Enabled = false;
                    txtBANK_NAME.Enabled = false;
                    txtOTHER_NARRATION.Enabled = false;
                    chkLocked.Enabled = false;
                    txtEMAIL_ID.Enabled = false;
                    txtEMAIL_ID_CC.Enabled = false;
                    txtOffPhone.Enabled = false;
                    txtfax.Enabled = false;
                    txtPanLink.Enabled = false;
                    txtMOBILE.Enabled = false;
                    txtwhatsup_No.Enabled = false;
                    pnlGroup.Enabled = false;
                    pnlgrdDetail.Enabled = false;

                    txtSendingEmail.Enabled = false;
                    txtSendingMobile.Enabled = false;
                    //txtGROUP_CODE.Enabled = false;
                    // btntxtGROUP_CODE.Enabled = false;
                    btnAddGroup.Enabled = false;

                    txtSHORT_NAME.Enabled = false;
                    txtTan_No.Enabled = false;
                }



            }
        }
        catch
        {
        }
    }
    private void TradingAndExpensesControls()
    {
        txtCOMMISSION.Enabled = false;
        txtInsurance.Enabled = false;
        txtADDRESS_E.Enabled = true;
        // txtADDRESS_R.Enabled = false;
        txtCITY_CODE.Enabled = false;
        txtPINCODE.Enabled = false;
        txtOPENING_BALANCE.Enabled = false;
        drpDrCr.Enabled = false;
        txtLOCAL_LIC_NO.Enabled = false;

        txtGST_NO.Enabled = false;
        txtBANK_OPENING.Enabled = false;
        drpBankDrCr.Enabled = false;
        chkCarporate.Enabled = false;
        txtBANK_AC_NO.Enabled = false;
        txtBANK_NAME.Enabled = false;
        txtEMAIL_ID.Enabled = false;
        txtEMAIL_ID_CC.Enabled = false;

        txtRefBy.Enabled = false;
        txtOffPhone.Enabled = false;
        txtcompanyPan.Enabled = false;
        txtfax.Enabled = false;
        txtPanLink.Enabled = false;
        btnOpenDetailsPopup.Enabled = false;
        grdGroup.Enabled = false;
        txtAC_RATE.Enabled = false;
        txtOTHER_NARRATION.Enabled = false;
        txtAdhar_No.Enabled = false;
    }
    #endregion

    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            bool recordExist = true; ;
            if (customercode != null)
            {
                qry = "select * from ClientRegistration where Client_Code=" + customercode;
                recordExist = this.fetchRecordCustomer(qry);
                if (recordExist == true)
                {
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else                     //new code
                {



                }

            }
            else
            {
                qry = getDisplayQuery();
                recordExist = this.fetchRecord(qry);
                if (recordExist == true)
                {
                    this.enableDisableNavigateButtons();
                    btnAdd.Focus();
                }
                else                     //new code
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }

            }





        }
        catch
        {
        }
    }
    #endregion

    private bool fetchRecordCustomer(string qry)
    {
        bool recordExist = false;
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
                    txtAC_CODE.Text = dt.Rows[0]["Client_Code"].ToString();
                    txtAC_NAME_E.Text = dt.Rows[0]["Client_Name"].ToString();

                    txtADDRESS_E.Text = dt.Rows[0]["Address"].ToString();
                    txtGstStateCode.Text = dt.Rows[0]["Gst_state_Code"].ToString();
                    txtPINCODE.Text = dt.Rows[0]["Pin_Code"].ToString();
                    txtGST_NO.Text = dt.Rows[0]["Gst_No"].ToString();
                    txtcompanyPan.Text = dt.Rows[0]["Pan_No"].ToString();
                    txtMOBILE.Text = dt.Rows[0]["Mobile1"].ToString();
                    txtEMAIL_ID.Text = dt.Rows[0]["Email1"].ToString();
                    txtBANK_NAME.Text = dt.Rows[0]["Bank_Name1"].ToString();
                    txtBANK_AC_NO.Text = dt.Rows[0]["Ac_No1"].ToString();
                    txtIfsc.Text = dt.Rows[0]["Ifsc1"].ToString();

                }
            }
        }

        recordExist = true;

        return recordExist;
    }




    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select accoid from " + tblHead + " where accoid=(select MIN(accoid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAC_CODE.Text != string.Empty)
            {
                string query = "SELECT top 1 [accoid] from " + tblHead + " where accoid<" + Convert.ToInt32(hdnf.Value) +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by accoid desc";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Next]
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAC_CODE.Text != string.Empty)
            {
                string query = "SELECT top 1 [accoid] from " + tblHead + " where accoid>" + Convert.ToInt32(hdnf.Value) +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by accoid asc";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Last]
    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select accoid from " + tblHead + " where accoid=(select MAX(accoid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons

        //if (ViewState["mode"].ToString() == "U")
        //{

        int RecordCount = 0;

        string query = "";
        query = "select count(*) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(query);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RecordCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
        }

        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
            //  btnLast.Focus();
        }

        if (txtAC_CODE.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [accoid] from " + tblHead +
                    " where accoid>" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY accoid asc  ";
                //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY accoid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnLast.Enabled = true;
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnLast.Enabled = false;
                            btnNext.Enabled = false;
                        }
                    }
                }

                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [accoid] from " + tblHead + " where accoid<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY accoid asc  ";
                //    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY accoid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnFirst.Enabled = true;
                            btnPrevious.Enabled = true;
                        }
                        else
                        {
                            btnFirst.Enabled = false;
                            btnPrevious.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        // }

        #endregion
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        txtOPENING_BALANCE.Text = "0"; 
        this.makeEmptyForm("A");
        this.NextNumber();
        setFocusControl(drpType);
        this.fillGroupsGrid();
        interest();
        //txtGROUP_CODE.Text = "24";
        //lblGROUPNAME.Text = clsCommon.getString("Select group_Name_E from " + tblPrefix + "BSGroupMaster where group_Code=24 and Company_Code="
        //    + Convert.ToInt32(Session["Company_Code"].ToString()));
        //disableOpening();
        drpType_SelectedIndexChanged(this, new EventArgs());
    }

    private void disableOpening()
    {
        int yearCode = Convert.ToInt32(Session["year"].ToString());
        if (yearCode > 1)
        {
            txtOPENING_BALANCE.Enabled = false;
            drpDrCr.Enabled = false;

        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        txtOPENING_BALANCE.Enabled = true;
        drpDrCr.Enabled = true;
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        if (Session["year"].ToString() != "1")
        {
            btntxtGROUP_CODE.Enabled = false;
            txtGROUP_CODE.Enabled = false;
        }
        interest();
        setFocusControl(txtAC_NAME_E);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string currentDoc_No = txtAC_CODE.Text;
            if (hdconfirm.Value == "Yes")
            {
                string gledgeraccode = string.Empty;
                string tenderheadaccode = string.Empty;
                string tenderdetailaccode = string.Empty;
                string doaccode = string.Empty;
                string carporateaccode = string.Empty;
                string concat = string.Empty;
                gledgeraccode = clsCommon.getString("select top(1) doc_no from nt_1_gledger where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (gledgeraccode != string.Empty && gledgeraccode != "0")
                {
                    concat = concat + "Gledger DocNo: " + gledgeraccode + "";
                }
                tenderheadaccode = clsCommon.getString("select top(1) Tender_No from nt_1_tender where Mill_Code=" + txtAC_CODE.Text + " or Payment_To=" + txtAC_CODE.Text + " or Tender_From=" + txtAC_CODE.Text + " or Tender_DO=" + txtAC_CODE.Text + " or Voucher_By=" + txtAC_CODE.Text + " or " +
                              " Broker=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (tenderheadaccode != string.Empty && tenderheadaccode != "0")
                {
                    concat = concat + "Tender DocNo: " + tenderheadaccode + "";
                }
                tenderdetailaccode = clsCommon.getString("select top(1) Tender_No from nt_1_tenderdetails where Buyer=" + txtAC_CODE.Text + " or Buyer_Party=" + txtAC_CODE.Text + " or sub_broker=" + txtAC_CODE.Text + " or ShipTo=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (tenderdetailaccode != string.Empty && tenderdetailaccode != "0")
                {
                    concat = concat + "Tender DocNo: " + tenderdetailaccode + "";
                }
                doaccode = clsCommon.getString("select top(1) doc_no from nt_1_deliveryorder where TRANSPORT=" + txtAC_CODE.Text + " or BROKER=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (doaccode != string.Empty && doaccode != "0")
                {
                    concat = concat + "Delivery Order No: " + doaccode + "";
                }
                carporateaccode = clsCommon.getString("select top(1) doc_no from carporatehead where ac_code==" + txtAC_CODE.Text + " or bill_to==" + txtAC_CODE.Text + " or unit_code==" + txtAC_CODE.Text + " or broker==" + txtAC_CODE.Text + " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (carporateaccode != string.Empty && carporateaccode != "0")
                {
                    concat = concat + "Carporate DocNo: " + carporateaccode + "";
                }
                if (gledgeraccode == "0" && tenderheadaccode == "0" && tenderdetailaccode == "0" && doaccode == "0" && carporateaccode == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Record Use In " + concat + "')", true);
                    return;
                }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);


            }
            else
            {
                hdnf.Value = lblAc_Code.Text;
                showLastRecord();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ClearSendingSmsTextboxes();
        //hdnf.Value = Request.QueryString["Ac_Code"];
        //if (hdnf.Value == "0" || hdnf.Value == "")
        //{
        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(accoid),0) as Accode from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " "));
        hdnf.Value = maxno.ToString();
        //}

        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
        hdnfcustomercode.Value = "0";

    }
    #endregion



    #region [fetchrecord]
    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;
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


                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }


                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["AC_CODE"].ToString();
                        hdnfacdoc.Value = txtAC_CODE.Text;
                        hdnfaccid.Value = dt.Rows[0]["accoid"].ToString();
                        hdnf.Value = hdnfaccid.Value;
                        txtAC_NAME_E.Text = dt.Rows[0]["AC_NAME_E"].ToString();
                        txtAC_RATE.Text = dt.Rows[0]["AC_RATE"].ToString();
                        txtAC_NAME_R.Text = dt.Rows[0]["AC_NAME_R"].ToString();
                        txtCOMMISSION.Text = dt.Rows[0]["COMMISSION"].ToString();
                        txtSHORT_NAME.Text = dt.Rows[0]["SHORT_NAME"].ToString();
                        txtADDRESS_E.Text = dt.Rows[0]["ADDRESS_E"].ToString();
                        txtADDRESS_R.Text = dt.Rows[0]["ADDRESS_R"].ToString();
                        txtCITY_CODE.Text = dt.Rows[0]["CITY_CODE"].ToString();
                        lblCITYNAME.Text = dt.Rows[0]["CityName"].ToString();
                        txtGstStateCode.Text = dt.Rows[0]["GSTStateCode"].ToString();
                        lbltxtGstStateName.Text = dt.Rows[0]["State_Name"].ToString();
                        txtPINCODE.Text = dt.Rows[0]["PINCODE"].ToString();
                        txtOPENING_BALANCE.Text = dt.Rows[0]["OPENING_BALANCE"].ToString();
                        txtGROUP_CODE.Text = dt.Rows[0]["GROUP_CODE"].ToString();
                        drpDrCr.SelectedValue = dt.Rows[0]["DRCR"].ToString();
                        txtwhatsup_No.Text = dt.Rows[0]["whatsup_no"].ToString();
                        drpLimit.SelectedValue = dt.Rows[0]["Limit_By"].ToString();
                        string Branch1DrCr = dt.Rows[0]["Branch1Drcr"].ToString();

                        lblGROUPNAME.Text = dt.Rows[0]["Group_Name_E"].ToString();

                        txtLOCAL_LIC_NO.Text = dt.Rows[0]["LOCAL_LIC_NO"].ToString();
                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                          
                        if (lblCreated != null)
                        {
                            lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        }
                        //lblCreated_Date.Text = dt.Rows[0]["Created_Date"].ToString();
                        //lblModified.Text = dt.Rows[0]["Modified_By"].ToString();
                        //lblModified_Date.Text = dt.Rows[0]["Modified_Date"].ToString();
                        txtBANK_NAME.Text = dt.Rows[0]["BANK_NAME"].ToString();
                        txtIfsc.Text = dt.Rows[0]["IFSC"].ToString();

                        txtBANK_AC_NO.Text = dt.Rows[0]["BANK_AC_NO"].ToString();

                        txtEMAIL_ID.Text = dt.Rows[0]["EMAIL_ID"].ToString();
                        txtGST_NO.Text = dt.Rows[0]["GST_NO"].ToString();
                        txtEMAIL_ID_CC.Text = dt.Rows[0]["EMAIL_ID_CC"].ToString();

                        txtAdhar_No.Text = dt.Rows[0]["adhar_no"].ToString();
                        txtOTHER_NARRATION.Text = dt.Rows[0]["OTHER_NARRATION"].ToString();
                        txtTan_No.Text = dt.Rows[0]["Tan_no"].ToString();
                        txtFssaiNo.Text = dt.Rows[0]["FSSAI"].ToString();
                        txtcompanyPan.Text = dt.Rows[0]["CompanyPan"].ToString();
                        txtBANK_OPENING.Text = dt.Rows[0]["BANK_OPENING"].ToString();
                        drpBankDrCr.SelectedValue = dt.Rows[0]["BANK_OP_DRCR"].ToString();
                        txtMOBILE.Text = dt.Rows[0]["Mobile_No"].ToString();
                        txtOffPhone.Text = dt.Rows[0]["OffPhone"].ToString();
                        txtRefBy.Text = dt.Rows[0]["referBy"].ToString();
                        txtfax.Text = dt.Rows[0]["Fax"].ToString();
                        txtPanLink.Text = dt.Rows[0]["PanLink"].ToString();
                        drpType.SelectedValue = dt.Rows[0]["AC_TYPE"].ToString();
                        txtDistance.Text = dt.Rows[0]["Distance"].ToString();
                        Acid = Convert.ToInt32(dt.Rows[0]["accoid"].ToString());
                        lblAc_Code.Text = dt.Rows[0]["accoid"].ToString();
                        lblgroupid.Text = dt.Rows[0]["bsid"].ToString();
                        lblcityid.Text = dt.Rows[0]["cityid"].ToString();
                        lblAc_Code.Text = dt.Rows[0]["accoid"].ToString();
                        drpTdsApplicable.SelectedValue = dt.Rows[0]["TDSApplicable"].ToString();
                        if (dt.Rows[0]["Carporate_Party"].ToString() == "Y")
                        {
                            chkCarporate.Checked = true;
                        }
                        else
                        {
                            chkCarporate.Checked = false;
                        }

                        txtInsurance.Text = dt.Rows[0]["Insurance"].ToString();
                        string abcd = dt.Rows[0]["UnregisterGST"].ToString();
                        chkUnregisterGST.Checked = Convert.ToBoolean(abcd);

                        string locked_check = dt.Rows[0]["Locked"].ToString();
                        chkLocked.Checked = Convert.ToBoolean(locked_check);
                        hdnfCity.Value = dt.Rows[0]["cityid"].ToString();
                        hdnfgid.Value = dt.Rows[0]["bsid"].ToString();

                        recordExist = true;
                        hdnf.Value = Acid.ToString();
                        lblMsg.Text = "";

                        string branch1code = clsCommon.getString("select Branch1 from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        // lblBranch1.Text = clsCommon.getString("select Branch from BranchMaster where Branch_Id=" + branch1code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string branch2code = clsCommon.getString("select Branch2 from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //  lblBranch2.Text = clsCommon.getString("select Branch from BranchMaster where Branch_Id=" + branch2code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        HyperLink1.NavigateUrl = txtfax.Text;

                        interest();
                        #region Account Details
                        qry = "SELECT  PersonId as personid_No, Person_Name, Person_Mobile as Mobile, Person_Email as Email, Person_Pan as Pan,Other,accoid as id " +
                            " FROM   " + tblPrefix + "AcContacts where accoid=" + hdnf.Value;
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    string v = dt.Rows[0]["personid_No"].ToString();
                                    if (v != "")
                                    {
                                        dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                        dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            dt.Rows[i]["rowAction"] = "N";
                                            dt.Rows[i]["SrNo"] = i + 1;
                                        }
                                        grdDetail.DataSource = dt;
                                        grdDetail.DataBind();
                                        ViewState["currentTable"] = dt;
                                    }
                                    else
                                    {
                                        grdDetail.DataSource = null;
                                        grdDetail.DataBind();
                                        ViewState["currentTable"] = null;
                                    }
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion

                        pnlgrdDetail.Enabled = false;

                        #region Show Groups
                        DataTable dtTemp = new DataTable();
                        string strChecked = "";
                        qry = "select Group_Code from " + AcGroupsTable + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and accoid=" + hdnf.Value;
                        ds = new DataSet();
                        dt = new DataTable();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        strChecked = strChecked + dt.Rows[i]["Group_Code"].ToString() + ",";
                                    }
                                    strChecked = strChecked.Substring(0, strChecked.Length - 1);
                                }
                                //grdGroup.DataSource = dt;
                                //grdGroup.DataBind();
                            }
                        }

                        if (strChecked != string.Empty)
                        {
                            qry = "select System_Code,System_Name_E from " + SystemMastertable + " where System_Type='G' and Company_Code=" +
                                Convert.ToInt32(Session["Company_Code"].ToString()) + " ";

                            ds = ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        dtTemp = dt;
                                        //if (ds.Tables[1].Rows.Count > 0)
                                        //{
                                        //    dtTemp.Merge(ds.Tables[1]);
                                        //}
                                    }
                                    //else if (ds.Tables[1].Rows.Count > 0)
                                    //{
                                    //    dtTemp = ds.Tables[1];
                                    //}

                                }
                            }
                            grdGroup.DataSource = dtTemp;
                            grdGroup.DataBind();
                            for (int i = 0; i < grdGroup.Rows.Count; i++)
                            {
                                CheckBox chk = (CheckBox)grdGroup.Rows[i].Cells[2].FindControl("chk");
                                if (strChecked.Contains(grdGroup.Rows[i].Cells[0].Text) == true)
                                {
                                    chk.Checked = true;
                                }
                                else
                                {
                                    chk.Checked = false;
                                }
                            }
                        }
                        else
                        {
                            qry = "select System_Code,System_Name_E from " + SystemMastertable + " where System_Type='G' and Company_Code=" +
                                Convert.ToInt32(Session["Company_Code"].ToString());
                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        grdGroup.DataSource = dt;
                                        grdGroup.DataBind();
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            //this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion


    #region getDisplayQuery
    protected string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + qryCommon + " where accoid=" + hdnf.Value + " ";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtAC_CODE.Text = hdnf.Value;
                hdnfSuffix.Value = "";
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        pnlPopupDetails.Style["display"] = "block";
        //pnlMain.Enabled = false;
        setFocusControl(txtPERSON_NAME);
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];

                if (dt.Rows[0]["personid_No"].ToString().Trim() != "")
                {

                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();

                        #region calculate rowindex

                        int maxIndex = 0;

                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["personid_No"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion

                        //     rowIndex = dt.Rows.Count + 1;
                        dr["personid_No"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblID.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["personid_No"] = rowIndex;
                        dr["SrNo"] = 0;

                        #region decide whether actual row is updating or virtual [rowAction]

                        string id = clsCommon.getString("select PersonId from " + tblDetails +
                        " where Ac_Code='" + txtAC_CODE.Text + "' and PersonId=" + lblID.Text +
                        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (id != string.Empty)
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row                        
                        }

                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("personid_No", typeof(int))));
                    dt.Columns.Add((new DataColumn("Person_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Mobile", typeof(string))));
                    dt.Columns.Add((new DataColumn("Email", typeof(string))));
                    dt.Columns.Add((new DataColumn("Pan", typeof(double))));
                    dt.Columns.Add((new DataColumn("Other", typeof(double))));
                    dt.Columns.Add((new DataColumn("id", typeof(int))));
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));

                    dr = dt.NewRow();
                    dr["personid_No"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("personid_No", typeof(int))));
                dt.Columns.Add((new DataColumn("Person_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Mobile", typeof(string))));
                dt.Columns.Add((new DataColumn("Email", typeof(string))));
                dt.Columns.Add((new DataColumn("Pan", typeof(string))));
                dt.Columns.Add((new DataColumn("Other", typeof(string))));
                dt.Columns.Add((new DataColumn("id", typeof(int))));
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));

                dr = dt.NewRow();
                dr["personid_No"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;

            }
            dr["Person_Name"] = txtPERSON_NAME.Text.ToUpper();
            if (txtPERSON_MOBILE.Text != string.Empty)
            {
                dr["Mobile"] = txtPERSON_MOBILE.Text;
            }
            else
            {
                setFocusControl(txtPERSON_MOBILE);
                return;
            }
            dr["Email"] = txtPERSON_EMAIL.Text;
            dr["Pan"] = txtPerson_PAN.Text;
            dr["Other"] = txtPERSON_OTHER.Text;
            dr["id"] = lblAc_Code.Text;
            if (btnAdddetails.Text == "ADD")
            {
                dt.Rows.Add(dr);
            }

            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion

            grdDetail.DataSource = dt;
            grdDetail.DataBind();

            ViewState["currentTable"] = dt;

            if (btnAdddetails.Text == "ADD")
            {
                pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtPERSON_NAME);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            txtPERSON_NAME.Text = string.Empty;
            txtPERSON_MOBILE.Text = string.Empty;
            txtPERSON_EMAIL.Text = string.Empty;
            txtPerson_PAN.Text = string.Empty;
            txtPERSON_OTHER.Text = string.Empty;
            btnAdddetails.Text = "ADD";
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        pnlPopupDetails.Style["display"] = "none";
        setFocusControl(btnSave);
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(4);
            e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(5);
            e.Row.Cells[personid_No].ControlStyle.Width = Unit.Percentage(7);
            e.Row.Cells[personName_E].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[mobile_No].ControlStyle.Width = Unit.Percentage(8);
            e.Row.Cells[email_Id].ControlStyle.Width = Unit.Percentage(15);
            e.Row.Cells[pan_No].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[other_Info].ControlStyle.Width = Unit.Percentage(15);
            e.Row.Cells[id].ControlStyle.Width = Unit.Percentage(15);
            e.Row.Cells[rowAction].ControlStyle.Width = Unit.Percentage(15);
            e.Row.Cells[SrNo].ControlStyle.Width = Unit.Percentage(15);

            e.Row.Cells[0].Style["overflow"] = "hidden";
            e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[personid_No].Style["overflow"] = "hidden";
            e.Row.Cells[personName_E].Style["overflow"] = "hidden";
            e.Row.Cells[mobile_No].Style["overflow"] = "hidden";
            e.Row.Cells[email_Id].Style["overflow"] = "hidden";
            e.Row.Cells[pan_No].Style["overflow"] = "hidden";
            e.Row.Cells[other_Info].Style["overflow"] = "hidden";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[personid_No].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[personName_E].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[mobile_No].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[email_Id].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[pan_No].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[other_Info].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[id].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[rowAction].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[SrNo].HorizontalAlign = HorizontalAlign.Left;


            }
            //e.Row.Cells[id].Visible = false;
            //e.Row.Cells[rowAction].Visible = false;
        }
        catch
        {
        }
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtGstStateCode" || v == "txtstatecode")
            {
                e.Row.Cells[0].Width = new Unit("100px");
                e.Row.Cells[1].Width = new Unit("400px");
            }
            else if (v == "txtSendingAcCode")
            {
                e.Row.Cells[0].Width = new Unit("100px");
                e.Row.Cells[1].Width = new Unit("400px");
            }
            else
            {
                e.Row.Cells[0].Width = new Unit("100px");
                e.Row.Cells[1].Width = new Unit("400px");
                e.Row.Cells[2].Width = new Unit("200px");
            }
        }


    }
    #endregion

    #region [grdPopup_RowCreated]
    protected void grdPopup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
           e.Row.RowState == DataControlRowState.Alternate))
            {


                e.Row.TabIndex = -1;


                e.Row.TabIndex = -1;
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";

            }
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region [RowCommand]
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[8].Text != "D" && grdDetail.Rows[rowindex].Cells[8].Text != "R")
                        {
                            pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            //Making Changes by ankush
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);
                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gridViewRow)
    {


        lblID.Text = Server.HtmlDecode(gridViewRow.Cells[personid_No].Text.Trim());
        lblNo.Text = Server.HtmlDecode(gridViewRow.Cells[rowAction].Text.Trim());
        txtPERSON_NAME.Text = Server.HtmlDecode(gridViewRow.Cells[personName_E].Text.Trim());
        txtPERSON_MOBILE.Text = Server.HtmlDecode(gridViewRow.Cells[mobile_No].Text.Trim());
        txtPERSON_EMAIL.Text = Server.HtmlDecode(gridViewRow.Cells[email_Id].Text.Trim());
        txtPerson_PAN.Text = Server.HtmlDecode(gridViewRow.Cells[pan_No].Text.Trim());
        txtPERSON_OTHER.Text = Server.HtmlDecode(gridViewRow.Cells[other_Info].Text.Trim());
        Acid = Convert.ToInt32(gridViewRow.Cells[id].Text);
        btnAdddetails.Text = "Update";
        setFocusControl(txtPERSON_NAME);
    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;

            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["personid_No"].ToString());

                string IDExisting = clsCommon.getString("select PersonId from " + tblDetails +
                    " where  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and " +
                    " Ac_Code=" + hdnf.Value + " and PersonId=" + ID);

                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "D";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table

                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "N";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";

                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "R";       //R=Only remove fro grid        
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "A";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;

            }

        }
        catch
        {

        }
    }
    #endregion



    #region [txtAC_CODE_TextChanged]
    protected void txtAC_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAC_CODE.Text;
        strTextbox = "txtAC_CODE";
        csCalculations();
        setFocusControl(txtAC_NAME_E);
    }
    #endregion

    protected void txtDistance_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDistance.Text;
        strTextbox = "txtDistance";
        csCalculations();
        setFocusControl(txtDistance);
    }


    #region [btntxtAC_CODE_Click]
    protected void btntxtAC_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtEditDoc_No";
            btnSearch_Click(sender, e);
            setFocusControl(txtSearchText);
        }
        catch
        {
        }
    }
    #endregion


    #region [txtAC_RATE_TextChanged]
    protected void txtAC_RATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAC_RATE.Text;
        strTextbox = "txtAC_RATE";
        csCalculations();
    }
    #endregion

   

    #region [txtAC_NAME_E_TextChanged]
    protected void txtAC_NAME_E_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAC_NAME_E.Text;
        strTextbox = "txtAC_NAME_E";
        csCalculations();
    }
    #endregion

    #region [txtwhatsup_No_TextChanged]
    protected void txtwhatsup_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtwhatsup_No.Text;
        strTextbox = "txtwhatsup_No";
        csCalculations();
    }
    #endregion

    #region [txtAC_NAME_R_TextChanged]
    protected void txtAC_NAME_R_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAC_NAME_R.Text;
        strTextbox = "txtAC_NAME_R";
        csCalculations();
    }
    #endregion

    #region [txtCOMMISSION_TextChanged]
    protected void txtCOMMISSION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCOMMISSION.Text;
        strTextbox = "txtCOMMISSION";
        csCalculations();
    }
    #endregion

    #region [txtSHORT_NAME_TextChanged]
    protected void txtSHORT_NAME_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSHORT_NAME.Text;
        strTextbox = "txtSHORT_NAME";
        csCalculations();
    }
    #endregion

    #region [txtADDRESS_E_TextChanged]
    protected void txtADDRESS_E_TextChanged(object sender, EventArgs e)
    {
        searchString = txtADDRESS_E.Text;
        strTextbox = "txtADDRESS_E";
        csCalculations();
    }
    #endregion

    #region [txtADDRESS_R_TextChanged]
    protected void txtADDRESS_R_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtADDRESS_R.Text;
        //strTextbox = "txtADDRESS_R";
        //csCalculations();
    }
    #endregion

    #region [txtCITY_CODE_TextChanged]
    protected void txtCITY_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCITY_CODE.Text;
        strTextbox = "txtCITY_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtCITY_CODE_Click]
    protected void btntxtCITY_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCITY_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtPINCODE_TextChanged]
    protected void txtPINCODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPINCODE.Text;
        strTextbox = "txtPINCODE";
        csCalculations();
    }
    #endregion

    #region [txtOPENING_BALANCE_TextChanged]
    protected void txtOPENING_BALANCE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOPENING_BALANCE.Text;
        strTextbox = "txtOPENING_BALANCE";
        csCalculations();
    }
    #endregion

    #region [drpDrCr_SelectedIndexChanged]
    protected void drpDrCr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = drpDrCr.SelectedValue;
            strTextbox = "drpDrCr";
            csCalculations();
        }
        catch
        {

        }
    }
    #endregion

    #region [txtGROUP_CODE_TextChanged]
    protected void txtGROUP_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGROUP_CODE.Text;
        strTextbox = "txtGROUP_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtGROUP_CODE_Click]
    protected void btntxtGROUP_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGROUP_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtLOCAL_LIC_NO_TextChanged]
    protected void txtLOCAL_LIC_NO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLOCAL_LIC_NO.Text;
        strTextbox = "txtLOCAL_LIC_NO";
        csCalculations();
    }
    #endregion

    #region [txtBANK_NAME_TextChanged]
    protected void txtBANK_NAME_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBANK_NAME.Text;
        strTextbox = "txtBANK_NAME";
        csCalculations();
    }
    #endregion

    #region [txtBANK_AC_NO_TextChanged]
    protected void txtBANK_AC_NO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBANK_AC_NO.Text;
        strTextbox = "txtBANK_AC_NO";
        csCalculations();
    }
    #endregion

    #region [txtCST_NO_TextChanged]

    #endregion

    #region [txtEMAIL_ID_TextChanged]
    protected void txtEMAIL_ID_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEMAIL_ID.Text;
        strTextbox = "txtEMAIL_ID";
        csCalculations();
    }
    #endregion

    #region [txtGST_NO_TextChanged]
    protected void txtGST_NO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_NO.Text;
        strTextbox = "txtGST_NO";
        csCalculations();
    }
    #endregion

    #region [txtEMAIL_ID_CC_TextChanged]
    protected void txtEMAIL_ID_CC_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEMAIL_ID_CC.Text;
        strTextbox = "txtEMAIL_ID_CC";
        csCalculations();
    }
    #endregion

    #region [txtAdhar_No_TextChanged]
    protected void txtAdhar_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAdhar_No.Text;
        strTextbox = "txtAdhar_No";
        csCalculations();
    }
    #endregion

    #region [txtOTHER_NARRATION_TextChanged]
    protected void txtOTHER_NARRATION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOTHER_NARRATION.Text;
        strTextbox = "txtOTHER_NARRATION";
        csCalculations();
    }
    #endregion

    #region [txtBANK_OPENING_TextChanged]
    protected void txtBANK_OPENING_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBANK_OPENING.Text;
        strTextbox = "txtBANK_OPENING";
        csCalculations();
    }
    #endregion

    #region [drpBankDrCr_SelectedIndexChanged]
    protected void drpBankDrCr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = drpBankDrCr.SelectedValue;
            strTextbox = "drpBankDrCr";
            csCalculations();
        }
        catch
        {

        }
    }
    #endregion

    #region [txtPERSON_NAME_TextChanged]
    protected void txtPERSON_NAME_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPERSON_NAME.Text;
        strTextbox = "txtPERSON_NAME";
        csCalculations();
    }
    #endregion

    #region [txtPERSON_MOBILE_TextChanged]
    protected void txtPERSON_MOBILE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPERSON_MOBILE.Text;
        strTextbox = "txtPERSON_MOBILE";
        csCalculations();
    }
    #endregion

    #region [txtPERSON_EMAIL_TextChanged]
    protected void txtPERSON_EMAIL_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPERSON_EMAIL.Text;
        strTextbox = "txtPERSON_EMAIL";
        csCalculations();
    }
    #endregion

    #region [txtPerson_PAN_TextChanged]
    protected void txtPerson_PAN_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPerson_PAN.Text;
        strTextbox = "txtPerson_PAN";
        csCalculations();
    }
    #endregion

    #region [txtPERSON_OTHER_TextChanged]
    protected void txtPERSON_OTHER_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPERSON_OTHER.Text;
        strTextbox = "txtPERSON_OTHER";
        csCalculations();
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;
            if (searchString != string.Empty && strTextbox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchString;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_No")
            {

                if (btntxtAC_CODE.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtAC_CODE.Text = string.Empty;
                    txtAC_CODE.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtAC_CODE);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtAC_CODE.Text == "Choose No")
                {
                    searchString = txtEditDoc_No.Text;
                    strTextbox = "txtEditDoc_No";
                    pnlPopup.Style["display"] = "block";
                    if (searchString != string.Empty && strTextbox == hdnfClosePopup.Value)
                    {
                        txtSearchText.Text = searchString;
                        searchtxt = txtSearchText.Text;
                        words = txtSearchText.Text;
                        split = words.Split(delimiter);
                    }
                    else
                    {
                        txtSearchText.Text = txtSearchText.Text;
                        searchtxt = txtSearchText.Text;
                        words = txtSearchText.Text;
                        split = words.Split(delimiter);
                    }

                    foreach (var s in split)
                    {
                        string aa = s.ToString();
                        //name += " Doc_No Like '%" + aa + "%'or Supplier_name Like '%" + aa
                        //    + "%'or Part_No_Name Like '%" + aa + "%'or TDS_name Like '%" + aa + "%'or";
                        name += "( Doc_No like '%" + aa + "%' or Supplier_name like '%" + aa + "%' or Part_No_Name like '%" + aa + "%' or TDS_name like '%" + aa + "%' ) and";

                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select distinct Doc_No,Doc_Date,Supplier_name,Bill_Amount,Bill_No,Branch_Code from " + qryCommon
                        + " where Tran_type='PB' and  Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and (" + name + ") order by Doc_No desc, Doc_Date desc";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtSendingAcCode")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code,Ac_Name_E from " + tblHead + " where (" + name + ") order by Ac_Name_E asc";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtCITY_CODE")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( city_code like '%" + aa + "%' or city_name_e like '%" + aa + "%' or state like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select City--";
                string qry = "select city_code,city_name_e,city_name_r,state,cityid from " + cityMasterTable + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and (" + name + ") order by city_name_e";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtGstStateCode")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( State_Code like '%" + aa + "%' or State_Name like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "Select State_Code,State_Name from GSTStateMaster where (" + name + ")";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtstatecode")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( State_Code like '%" + aa + "%' or State_Name like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "Select State_Code,State_Name from GSTStateMaster where (" + name + ")";
                // string qry = "Select State_Code,State_Name from GSTStateMaster where State_Code like'%" + txtSearchText.Text + "%' or State_Name like'%" + txtSearchText.Text + "%'";
                this.showPopup(qry);
            }


            if (hdnfClosePopup.Value == "txtGROUP_CODE")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Name_R like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select BS group--";
                string qry = "select group_Code,group_Name_E,group_Name_R,bsid from " + GroupMasterTable + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                " and (" + name + ") order by group_Name_E";
                this.showPopup(qry);
            }


           
        }
        catch
        {
        }
    }
    #endregion

    #region [Popup Button Code]
    protected void showPopup(string qry)
    {
        try
        {
            using (clsDataProvider objDataProvider = new clsDataProvider())
            {
                setFocusControl(txtSearchText);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = objDataProvider.GetDataSet(qry);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            grdPopup.DataSource = dt;
                            grdPopup.DataBind();

                            hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                        }
                        else
                        {
                            grdPopup.DataSource = null;
                            grdPopup.DataBind();

                            hdHelpPageCount.Value = "0";
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                setFocusControl(txtAC_CODE);
            }
            if (hdnfClosePopup.Value == "txtCITY_CODE")
            {
                setFocusControl(txtCITY_CODE);
            }
            if (hdnfClosePopup.Value == "txtGROUP_CODE")
            {
                setFocusControl(txtGROUP_CODE);
            }
            hdnfClosePopup.Value = "Close";
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {
        }
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    #region [txtSearchText_TextChanged]
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "Close")
            {
                txtSearchText.Text = string.Empty;
                pnlPopup.Style["display"] = "none";
                grdPopup.DataSource = null;
                grdPopup.DataBind();
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            }
            else
            {
                pnlPopup.Style["display"] = "block";

                searchString = txtSearchText.Text;
                strTextbox = hdnfClosePopup.Value;

                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        int gstno;
        if (txtGST_NO.Text != string.Empty)
        {
            gstno = Convert.ToInt32(clsCommon.getString("select COUNT(*) from " + tblHead + " where company_code=" + Session["Company_Code"].ToString() +
              " and Gst_No='" + txtGST_NO.Text + "'"));
            if (gstno > 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('GST No is already Exists!')", true);
            }
        }
        int[] arrEmail = new int[grdGroup.Rows.Count];
        for (int i = 0; i < grdGroup.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdGroup.Rows[i].Cells[2].FindControl("chk");
            if (chk.Checked == true)
            {
                arrEmail[i] = Convert.ToInt32(grdGroup.Rows[i].Cells[0].Text);
            }
        }


        string serializedLat = (new JavaScriptSerializer()).Serialize(arrEmail);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Account", "javascript:pagevalidation('" + serializedLat + "');", true);



    }
    #endregion

    #region [saveDocuments]
    private void saveDocuments()
    {
        //try
        //{
        //    if (FileUpload_PAN.HasFile)
        //    {
        //        try
        //        {
        //            string filename = Path.GetFileName(FileUpload_PAN.FileName);
        //            FileUpload_PAN.SaveAs(Server.MapPath("~/PAN/PAN_" + clsGV.user + "") + filename);
        //            //
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //            // StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        //        }
        //    }
        //    //using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
        //    //{
        //    //    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
        //    //    {
        //    //        w.WriteLine(s);
        //    //    }
        //    //}
        //}
        //catch
        //{
        //}
    }
    #endregion

    #region DataInsert
    private int DataStore(int flag)
    {
        int count = 0;
        try
        {
            //Connection open
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            ///Execution
            myTran = con.BeginTransaction();
            //cmd.CommandText = qry;
            //cmd.Connection = con;
            //cmd.Transaction = myTran;
            if (flag == 1)
            {
                cmd = new SqlCommand(Head_Insert, con, myTran);
                cmd.ExecuteNonQuery();

                if (Detail_Insert != "")
                {
                    cmd = new SqlCommand(Detail_Insert, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                if (Group_Insert != "")
                {
                    cmd = new SqlCommand(Group_Insert, con, myTran);
                    cmd.ExecuteNonQuery();
                }

                myTran.Commit();
                Thread.Sleep(100);

                count = 1;
            }

            if (flag == 2)
            {

                cmd = new SqlCommand(Head_Update, con, myTran);
                cmd.ExecuteNonQuery();

                if (Detail_Update != "")
                {
                    cmd = new SqlCommand(Detail_Update, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                if (Group_Update != "")
                {
                    cmd = new SqlCommand(Group_Update, con, myTran);
                    cmd.ExecuteNonQuery();
                }

                myTran.Commit();
                Thread.Sleep(100);

                count = 2;
            }
            if (flag == 3)
            {
                cmd = new SqlCommand(Detail_Delete, con, myTran);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(Group_Delete, con, myTran);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(Head_Delete, con, myTran);
                cmd.ExecuteNonQuery();


                myTran.Commit();
                Thread.Sleep(100);

                count = 3;
            }

            return count;
        }
        catch
        {
            if (myTran != null)
            {
                myTran.Rollback();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);

            }
            return count;

        }
        finally
        {
            con.Close();
        }

    }
    #endregion

    private void interest()
    {
        lblinterestrate.Text = "Interest Rate:";

        if (drpType.SelectedValue == "BR")
        {
            lblinterestrate.Text = "Brokarage";

        }
        if (drpType.SelectedValue == "F")
        {
            lblinterestrate.Text = "Depreciation:";

        }
    }

    #region [drpType_SelectedIndexChanged]
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        interest();
        #region always check
        setFocusControl(drpType);
        string val = drpType.SelectedValue.ToString();
        string aa = "";
        //drpType.Attributes.Add("onChange", "javascript:EnableDisable();");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "a", "javascript:EnableDisable('" + val + "');", true);
        if (btnAdd.Enabled == true)
        {
            aa = "A";
        }
        else
        {
            aa = "E";
        }
        this.makeEmptyForm(aa);
       // lblinterestrate.Text = "Interest Rate:";
        if (drpType.SelectedValue == "T")
        {
            txtGST_NO.Enabled = true;
        }
        if (drpType.SelectedValue == "P")
        {
            qry = "select party as party,pa as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

         //  hdnfgid.Value = clsCommon.getString("select pa from groupSetting where party and companyCode=" + Session["Company_Code"].ToString() + " ");

        else if (drpType.SelectedValue == "S")
        {
            qry = "select supplier  as party,sa as pa  from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "B")
        {
            //lblinterestrate.Text = "Brokarage:";
            qry = "select bank as party,ba as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "C")
        {
            qry = "select cash as party,ca as pa  from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "R")
        {
            qry = "select relatives as party,re as pa groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "F")
        {
           // lblinterestrate.Text = "Depreciation:";
            qry = "select fixedAssets as party,re as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "I")
        {
            qry = "select interestParty as party,fi as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "E")
        {
            qry = "select incomeExpenses as party,inte aspa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "O")
        {
            qry = "select trading as party,inc as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "M")
        {
            qry = "select mill as party,mi as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }

        else if (drpType.SelectedValue == "T")
        {
            qry = "select transport as party,tr as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }



        else if (drpType.SelectedValue == "RP")
        {
            qry = "select retailparty as party,trn as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }


        else if (drpType.SelectedValue == "CR")
        {
            qry = "select cashRetailParty as party,caret as pa from groupSetting where  companyCode=" + Session["Company_Code"].ToString() + "";
        }


     
        DataSet ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtGROUP_CODE.Text = dt.Rows[0]["party"].ToString();
                    //lblGROUPNAME.Text = dt.Rows[0]["party"].ToString();
                    hdnfgid.Value = dt.Rows[0]["pa"].ToString();
                }
            }
        }
        string Group_Name_E = clsCommon.getString("select  group_Name_E from nt_1_bsgroupmaster where group_Code="
                               + txtGROUP_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        lblGROUPNAME.Text = Group_Name_E;

      
        //checkTextBox("",val);
        #endregion
    }

    private void FixedAssetsControls()
    {
        txtAC_NAME_E.Enabled = true;
        txtOPENING_BALANCE.Enabled = true;
        drpDrCr.SelectedValue = "D";
        txtGROUP_CODE.Enabled = true;
        drpDrCr.Enabled = false;
        txtCOMMISSION.Enabled = false;
        txtADDRESS_E.Enabled = false;
        // txtADDRESS_R.Enabled = false;
        txtCITY_CODE.Enabled = false;
        txtPINCODE.Enabled = false;
        txtLOCAL_LIC_NO.Enabled = false;

        txtGST_NO.Enabled = false;
        txtBANK_AC_NO.Enabled = false;
        txtBANK_NAME.Enabled = false;
        txtEMAIL_ID.Enabled = false;
        txtEMAIL_ID_CC.Enabled = false;
        txtAdhar_No.Enabled = false;

        txtRefBy.Enabled = false;
        txtOffPhone.Enabled = false;
        txtcompanyPan.Enabled = false;
        txtfax.Enabled = false;
        txtPanLink.Enabled = false;
        btnOpenDetailsPopup.Enabled = false;
        grdGroup.Enabled = false;
        txtAC_RATE.Enabled = false;
        txtOTHER_NARRATION.Enabled = false;
        chkCarporate.Enabled = false;
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            #region  txtAC_CODE
            if (strTextbox == "txtAC_CODE")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtAC_CODE.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtAC_CODE.Text != string.Empty)
                        {
                            txtValue = txtAC_CODE.Text;

                            string qry = "select * from " + tblHead + " where  Ac_Code='" + txtValue + "' " +
                                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Record Found
                                        hdnf.Value = dt.Rows[0]["Ac_Code"].ToString();
                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Ac_Code (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                btnSave.Enabled = true;   //IMP
                                                this.getMaxCode();
                                                setFocusControl(drpType);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = this.getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtAC_CODE.Enabled = false;
                                                    setFocusControl(drpType);
                                                    hdnf.Value = txtAC_CODE.Text;
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtAC_CODE);
                                            txtAC_CODE.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtAC_CODE.Text = string.Empty;
                                            setFocusControl(txtAC_CODE);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtAC_CODE);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Ac code is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtAC_CODE.Text = string.Empty;
                        setFocusControl(txtAC_CODE);
                    }
                }
                catch
                {

                }
                #endregion
            }
            #endregion



            try
            {
                if (drpType.SelectedValue != "F" || drpType.SelectedValue != "E" || drpType.SelectedValue != "O")
                {
                    if (strTextbox == "txtAC_NAME_E")
                    {
                        setFocusControl(txtAC_NAME_R);
                    }
                    if (strTextbox == "txtAC_NAME_R")
                    {
                        if (drpType.SelectedValue == "F" || drpType.SelectedValue == "O" || drpType.SelectedValue == "E")
                        {
                            setFocusControl(txtSHORT_NAME);
                        }
                        else
                        {
                            setFocusControl(txtCOMMISSION);
                        }
                    }
                    if (strTextbox == "txtAC_RATE")
                    {
                        setFocusControl(txtAC_NAME_E);
                    }
                    if (strTextbox == "txtCOMMISSION")
                    {
                        setFocusControl(txtInsurance);
                    }
                    if (strTextbox == "txtInsurance")
                    {
                        setFocusControl(txtSHORT_NAME);
                    }
                    if (strTextbox == "txtSendingAcCode")
                    {
                        bool a = clsCommon.isStringIsNumeric(txtSendingAcCode.Text);
                        if (a == false)
                        {
                            btntxtSendingAcCode_Click(this, new EventArgs());
                        }
                        else
                        {
                            string SendingAcCode = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + txtSendingAcCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (SendingAcCode != string.Empty)
                            {
                                txtSendingEmail.Text = clsCommon.getString("select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=" + txtSendingAcCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                txtSendingMobile.Text = clsCommon.getString("select Mobile_No from " + tblPrefix + "AccountMaster where Ac_Code=" + txtSendingAcCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                if (SendingAcCode.Length >= 10)
                                {
                                    lblSendingAcCode.ToolTip = SendingAcCode;
                                    SendingAcCode = SendingAcCode.Substring(0, 10);
                                }
                                lblSendingAcCode.Text = SendingAcCode;
                                setFocusControl(btnSendSMS);
                            }
                            else
                            {
                                txtSendingAcCode.Text = string.Empty;
                                lblSendingAcCode.Text = SendingAcCode;
                                setFocusControl(txtSendingAcCode);
                            }
                        }
                    }
                    //else
                    //{
                    //    lblSendingAcCode.Text = "";
                    //    setFocusControl(txtSendingAcCode);
                    //}
                    if (strTextbox == "txtSHORT_NAME")
                    {
                        if (drpType.SelectedValue == "E" || drpType.SelectedValue == "O")
                        {
                            setFocusControl(txtGROUP_CODE);
                        }
                        else if (drpType.SelectedValue == "F")
                        {
                            setFocusControl(txtOPENING_BALANCE);
                        }
                        else
                        {
                            setFocusControl(txtADDRESS_E);
                        }
                    }
                    if (strTextbox == "txtADDRESS_E")
                    {
                        //setFocusControl(txtADDRESS_R);
                    }
                    if (strTextbox == "txtADDRESS_R")
                    {
                        setFocusControl(txtCITY_CODE);
                    }
                    if (strTextbox == "txtCITY_CODE")
                    {
                        if (txtCITY_CODE.Text == "0")
                        {
                            txtCITY_CODE.Text = string.Empty;
                        }
                        if (txtCITY_CODE.Text != string.Empty)
                        {
                            bool a = clsCommon.isStringIsNumeric(txtCITY_CODE.Text);
                            if (a == false)
                            {
                                btntxtCITY_CODE_Click(this, new EventArgs());
                            }
                            else
                            {
                                string str = clsCommon.getString("select city_name_e from " + cityMasterTable + " where city_code="
                                    + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                string pincode = clsCommon.getString("select isnull(pincode,'') from " + cityMasterTable + " where city_code="
                                    + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                string distance = clsCommon.getString("select isnull(Distance,'') from " + cityMasterTable + " where city_code="
                                  + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                string subarea = clsCommon.getString("select isnull(Sub_Area,'') from " + cityMasterTable + " where city_code="
                                 + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                                string gststatecode = clsCommon.getString("select isnull(GstStateCode,0) from " + cityMasterTable + " where city_code="
                                + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                cityid = Convert.ToInt32(clsCommon.getString("select isnull(cityid,0) from " + cityMasterTable + " where city_code="
                                + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                                lblcityid.Text = cityid.ToString();
                                hdnfCity.Value = cityid.ToString();
                                string statename = clsCommon.getString("select gststatename from qrycitymaster where city_code="
                               + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                string SubAreaCity = clsCommon.getString("select Sub_Area from qrycitymaster where city_code="
                               + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                txtADDRESS_R.Text = txtADDRESS_R.Text + " " + SubAreaCity;
                                if (str != string.Empty && str != "0")
                                {
                                    if (str.Length > 14)
                                    {
                                        str = str.Substring(1, 14);
                                    }
                                    else if (str.Length > 12)
                                    {
                                        str = str.Substring(1, 12);
                                    }
                                    else if (str.Length > 8)
                                    {
                                        str = str.Substring(1, 8);
                                    }

                                    lblCITYNAME.Text = str;
                                    txtPINCODE.Text = pincode;
                                    txtDistance.Text = distance;
                                    // txtADDRESS_R.Text = subarea;
                                    txtGstStateCode.Text = gststatecode;
                                    lbltxtGstStateName.Text = statename;

                                    setFocusControl(txtPINCODE);
                                }
                                else
                                {
                                    lblCITYNAME.Text = str;
                                    txtCITY_CODE.Text = string.Empty;
                                    setFocusControl(txtCITY_CODE);
                                }
                            }
                        }
                        else
                        {
                            setFocusControl(txtPINCODE);
                        }
                    }
                    if (strTextbox == "txtGstStateCode")
                    {
                        if (txtGstStateCode.Text != string.Empty)
                        {
                            bool a = clsCommon.isStringIsNumeric(txtGstStateCode.Text);
                            if (a == false)
                            {
                                btntxtGstStateCode_Click(this, new EventArgs());
                            }
                            else
                            {
                                string str = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + txtGstStateCode.Text + "");
                                if (str != string.Empty && str != "0")
                                {
                                    lbltxtGstStateName.Text = str;
                                    setFocusControl(txtDistance);
                                }
                                else
                                {
                                    lbltxtGstStateName.Text = str;
                                    txtGstStateCode.Text = string.Empty;
                                    setFocusControl(txtGstStateCode);
                                }
                            }
                        }
                        else
                        {
                            setFocusControl(txtGstStateCode);
                        }
                    }
                    if (strTextbox == "txtstatecode")
                    {
                        if (txtstatecode.Text != string.Empty)
                        {

                            bool a = clsCommon.isStringIsNumeric(txtstatecode.Text);
                            if (a == false)
                            {
                                btntxtGstStateCode_Click(this, new EventArgs());
                            }
                            else
                            {
                                string str = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + txtstatecode.Text + "");
                                if (str != string.Empty && str != "0")
                                {
                                    lblGstStateName.Text = str;
                                    setFocusControl(btnSaveCity);

                                }
                                else
                                {
                                    lblGstStateName.Text = str;
                                    txtstatecode.Text = string.Empty;
                                    setFocusControl(txtstatecode);
                                }
                            }
                        }
                        else
                        {
                            setFocusControl(txtstatecode);
                        }
                    }
                    if (strTextbox == "txtPINCODE")
                    {
                        setFocusControl(txtGstStateCode);
                    }
                    if (strTextbox == "txtopening_balance")
                    {
                        if (drpType.SelectedValue == "f")
                        {
                            setFocusControl(txtGROUP_CODE);

                        }
                        else
                        {

                            setFocusControl(drpDrCr);
                        }
                    }
                    if (strTextbox == "drpDrCr")
                    {
                        setFocusControl(txtGROUP_CODE);
                    }

                    if (strTextbox == "txtLOCAL_LIC_NO")
                    {

                    }
                    if (strTextbox == "txtTIN_NO")
                    {

                    }
                    if (strTextbox == "txtCST_NO")
                    {
                        setFocusControl(txtGST_NO);
                    }
                    if (strTextbox == "txtGST_NO")
                    {
                        string gstno = txtGST_NO.Text;
                        string mystr = gstno.Substring(2, gstno.Length - 5);
                        txtcompanyPan.Text = mystr.ToUpper();
                        setFocusControl(txtcompanyPan);
                    }
                    if (strTextbox == "txtOTHER_NARRATION")
                    {
                        setFocusControl(txtRefBy);
                    }
                    if (strTextbox == "txtBANK_NAME")
                    {
                        setFocusControl(txtIfsc);
                    }
                    if (strTextbox == "txtIfsc")
                    {
                        setFocusControl(txtBANK_AC_NO);
                    }

                    if (strTextbox == "txtBANK_AC_NO")
                    {
                        setFocusControl(txtEMAIL_ID);
                    }
                    if (strTextbox == "txtEMAIL_ID")
                    {
                        setFocusControl(txtEMAIL_ID_CC);
                    }
                    if (strTextbox == "txtEMAIL_ID_CC")
                    {

                    }
                    if (strTextbox == "txtECC_NO")
                    {
                        setFocusControl(txtFssaiNo);
                    }
                    if (strTextbox == "txtFssaiNo")
                    {
                        setFocusControl(txtBANK_NAME);
                    }
                    if (strTextbox == "txtRefBy")
                    {
                        setFocusControl(txtOffPhone);
                    }
                    if (strTextbox == "txtOffPhone")
                    {
                        setFocusControl(txtcompanyPan);
                    }
                    if (strTextbox == "txtcompanyPan")
                    {
                        setFocusControl(txtfax);
                    }
                    if (strTextbox == "txtPERSON_NAME")
                    {
                        setFocusControl(txtPERSON_MOBILE);
                    }
                    if (strTextbox == "txtPERSON_MOBILE")
                    {
                        setFocusControl(txtPERSON_EMAIL);
                    }
                    if (strTextbox == "txtPERSON_EMAIL")
                    {
                        setFocusControl(txtPerson_PAN);
                    }
                    if (strTextbox == "txtPerson_PAN")
                    {
                        setFocusControl(txtPERSON_OTHER);
                    }
                    if (strTextbox == "txtPERSON_OTHER")
                    {
                        setFocusControl(btnAdddetails);
                    }
                    if (strTextbox == "txtRefBy")
                    {
                        setFocusControl(txtOffPhone);
                    }
                    if (strTextbox == "txtOffPhone")
                    {
                        setFocusControl(txtcompanyPan);
                    }
                    if (strTextbox == "txtcompanyPan")
                    {
                        setFocusControl(txtfax);
                    }
                    if (strTextbox == "txtfax")
                    {
                        setFocusControl(txtPanLink);
                    }
                    if (strTextbox == "txtPanLink")
                    {
                        setFocusControl(txtMOBILE);
                    }
                    if (strTextbox == "txtMOBILE")
                    {
                        setFocusControl(btnOpenDetailsPopup);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (strTextbox == "txtGROUP_CODE")
            {
                if (txtGROUP_CODE.Text != string.Empty)
                {
                    string s = string.Empty;
                    bool a = clsCommon.isStringIsNumeric(txtGROUP_CODE.Text);
                    if (a == false)
                    {
                        btntxtGROUP_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        qry = "select group_Name_E,bsid from " + GroupMasterTable + " where group_Code=" + txtGROUP_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        DataSet ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                DataTable dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    s = dt.Rows[0]["group_Name_E"].ToString();
                                    bsGroup_Id = Convert.ToInt32(dt.Rows[0]["bsid"].ToString());
                                    lblgroupid.Text = bsGroup_Id.ToString();
                                    hdnfgid.Value = bsGroup_Id.ToString();
                                }
                            }
                        }



                        if (s != string.Empty)
                        {
                            if (s.Length > 14)
                            {
                                s = s.Substring(0, 14);
                            }
                            else if (s.Length > 13)
                            {
                                s = s.Substring(0, 13);
                            }
                            else if (s.Length > 12)
                            {
                                s = s.Substring(0, 12);
                            }
                            else if (s.Length > 11)
                            {
                                s = s.Substring(0, 11);
                            }
                            else if (s.Length > 10)
                            {
                                s = s.Substring(0, 10);
                            }
                            else if (s.Length > 9)
                            {
                                s = s.Substring(0, 9);
                            }
                            else if (s.Length > 5)
                            {
                                s = s.Substring(0, 5);
                            }
                            lblGROUPNAME.Text = s;

                            if (txtLOCAL_LIC_NO.Enabled == true)
                            {
                                if (txtBANK_OPENING.Enabled == true)
                                {
                                    setFocusControl(txtBANK_OPENING);
                                }
                                else
                                {
                                    setFocusControl(txtLOCAL_LIC_NO);
                                }
                            }
                            else
                            {
                                setFocusControl(btnSave);
                            }
                        }
                        else
                        {
                            lblGROUPNAME.Text = string.Empty;
                            txtGROUP_CODE.Text = string.Empty;
                            setFocusControl(txtGROUP_CODE);
                        }
                    }
                }
                else
                {
                    lblGROUPNAME.Text = string.Empty;
                    txtGROUP_CODE.Text = string.Empty;
                    setFocusControl(txtGROUP_CODE);
                }
            }
            #region always check
            //if (btnSave.Enabled == true)
            //{
            //    string s_item = drpType.SelectedValue;
            //    if (s_item == "T")
            //    {
            //        //    txtCOMMISSION.Text = "";
            //        txtCOMMISSION.Enabled = true;
            //        //    txtADDRESS_E.Text = "";
            //        txtADDRESS_E.Enabled = true;
            //        //     txtADDRESS_R.Text = "";
            //        // txtADDRESS_R.Enabled = true;
            //        //     txtCITY_CODE.Text = "";
            //        //      lblCITYNAME.Text = "";
            //        txtCITY_CODE.Enabled = true;
            //        //     txtPINCODE.Text = "";
            //        txtPINCODE.Enabled = true;
            //        //     txtOPENING_BALANCE.Text = "";
            //        if (Session["year"].ToString() != "1")
            //        {
            //            txtOPENING_BALANCE.Enabled = false;
            //        }
            //        else
            //        {
            //            txtOPENING_BALANCE.Enabled = true;
            //        }
            //        drpDrCr.Enabled = true;
            //        //    txtLOCAL_LIC_NO.Text = "";
            //        txtLOCAL_LIC_NO.Enabled = true;
            //        //   txtTIN_NO.Text = "";

            //        //    txtGST_NO.Text = "";
            //        txtGST_NO.Enabled = true;
            //        //    txtBANK_OPENING.Text = "";
            //        txtBANK_OPENING.Enabled = true;
            //        drpBankDrCr.Enabled = true;
            //    }
            //    else if (drpType.SelectedValue == "F" || drpType.SelectedValue == "O" || drpType.SelectedValue == "E")
            //    {
            //        //  txtCOMMISSION.Text = "";
            //        txtCOMMISSION.Enabled = false;
            //        //    txtADDRESS_E.Text = "";
            //        txtADDRESS_E.Enabled = false;
            //        //    txtADDRESS_R.Text = "";
            //        // txtADDRESS_R.Enabled = false;
            //        //    txtCITY_CODE.Text = "";
            //        //    lblCITYNAME.Text = "";
            //        txtCITY_CODE.Enabled = false;
            //        //     txtPINCODE.Text = "";
            //        txtPINCODE.Enabled = false;
            //        //     txtOPENING_BALANCE.Text = "";
            //        //txtOPENING_BALANCE.Enabled = false;
            //        drpDrCr.Enabled = false;
            //        //   txtLOCAL_LIC_NO.Text = "";
            //        txtLOCAL_LIC_NO.Enabled = false;
            //        //   txtTIN_NO.Text = "";

            //        //    txtGST_NO.Text = "";
            //        txtGST_NO.Enabled = false;
            //        //     txtBANK_OPENING.Text = "";
            //        txtBANK_OPENING.Enabled = false;
            //        drpBankDrCr.Enabled = true;
            //    }
            //    else
            //    {
            //        //  txtCOMMISSION.Text = "";
            //        txtCOMMISSION.Enabled = true;
            //        //    txtADDRESS_E.Text = "";
            //        txtADDRESS_E.Enabled = true;
            //        //    txtADDRESS_R.Text = "";
            //        //txtADDRESS_R.Enabled = true;
            //        //    txtCITY_CODE.Text = "";
            //        //    lblCITYNAME.Text = "";
            //        txtCITY_CODE.Enabled = true;
            //        //     txtPINCODE.Text = "";
            //        txtPINCODE.Enabled = true;
            //        //     txtOPENING_BALANCE.Text = "";
            //        if (Session["year"].ToString() != "1")
            //        {
            //            txtOPENING_BALANCE.Enabled = false;
            //        }
            //        else
            //        {
            //            txtOPENING_BALANCE.Enabled = true;
            //        }
            //        drpDrCr.Enabled = true;
            //        //   txtLOCAL_LIC_NO.Text = "";
            //        txtLOCAL_LIC_NO.Enabled = true;
            //        //   txtTIN_NO.Text = "";

            //        //    txtGST_NO.Text = "";
            //        txtGST_NO.Enabled = true;
            //        //     txtBANK_OPENING.Text = "";
            //        txtBANK_OPENING.Enabled = true;
            //        drpBankDrCr.Enabled = true;
            //    }
            //    if (s_item == "B")
            //    {
            //        //   txtBANK_OPENING.Text = "";
            //        txtBANK_OPENING.Enabled = true;
            //        drpBankDrCr.Enabled = true;
            //    }
            //    else
            //    {
            //        //   txtBANK_OPENING.Text = "";
            //        txtBANK_OPENING.Enabled = false;
            //        drpBankDrCr.Enabled = false;
            //    }

            //    if (s_item == "F" || s_item == "I")
            //    {
            //        //  txtAC_RATE.Text = "";
            //        txtAC_RATE.Enabled = true;
            //    }
            //    else
            //    {
            //        // txtAC_RATE.Text = "";
            //        txtAC_RATE.Enabled = false;
            //    }
            //}


            #endregion
        }
        catch
        {
        }
    }

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
        pnlPopup.Style["display"] = "block";
    }
    #endregion

    protected void FileUpload_PAN_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        //string filePath = "~/PAN/" + clsGV.user + "_" + e.FileName;
        //FileUpload_PAN.SaveAs(Server.MapPath(filePath));
        //f_pan = filePath;
    }
    protected void grdDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }
    protected void txtcompanyPan_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcompanyPan.Text;
        strTextbox = "txtcompanyPan";
        csCalculations();
    }
    protected void txtTan_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTan_No.Text;
        strTextbox = "txtTan_No";
        csCalculations();
    }
    protected void txtMOBILE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMOBILE.Text;
        strTextbox = "txtMOBILE";
        csCalculations();

    }
    protected void txtfax_TextChanged(object sender, EventArgs e)
    {
        searchString = txtfax.Text;
        strTextbox = "txtfax";
        csCalculations();
    }
    protected void txtPanLink_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPanLink.Text;
        strTextbox = "txtPanLink";
        csCalculations();
    }
    protected void txtOffPhone_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOffPhone.Text;
        strTextbox = "txtOffPhone";
        csCalculations();

    }
    protected void txtRefBy_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRefBy.Text;
        strTextbox = "txtRefBy";
        csCalculations();
    }
    protected void txtFssaiNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFssaiNo.Text;
        strTextbox = "txtFssaiNo";
        csCalculations();
    }
    protected void txtIfsc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIfsc.Text;
        strTextbox = "txtIfsc";
        csCalculations();
    }
    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void imgClose_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnSaveCity_Click(object sender, EventArgs e)
    {
        try
        {
            string citycode = txtCityCode.Text;
            string cityname = txtCityName.Text;
            string citynamer = txtRegionalName.Text;
            string state = txtState.Text;
            string pincode1 = txtpincodecity.Text;
            string subarea = txtSubArea.Text;
            string statecode = txtstatecode.Text;
            string distance = txtdist.Text;
            bool isValidated = true;
            if (!string.IsNullOrEmpty(txtCityName.Text))
            {
                string str = clsCommon.getString("select city_code from " + tblHead + " where  city_code='" + txtCityCode.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    lblErr.Text = "Doc No " + txtCityCode.Text + " already exist";
                    string maxcity = clsCommon.getString("Select ISNULL(MAX(city_code+1),1) from  " + tblPrefix + "CityMaster where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    txtCityCode.Text = maxcity;
                    modalCity.Show();
                    isValidated = true;
                    return;
                }
                else
                {
                    isValidated = true;
                }
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string strRev = "";
                    obj.flag = 1;
                    obj.tableName = tblPrefix + "CityMaster";
                    obj.columnNm = "city_code,city_name_e,city_name_r,company_code,state,Created_By,pincode,Sub_Area,GstStateCode,Distance";
                    obj.values = "'" + citycode + "','" + cityname + "','" + citynamer + "','" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + "','" + state + "','" + user + "','" + pincode1 + "','" + subarea + "','" + statecode + "','" + distance + "'";
                    DataSet ds = new DataSet();
                    ds = obj.insertAccountMaster(ref strRev);
                    txtCITY_CODE.Text = citycode;
                    lblCITYNAME.Text = cityname;
                    //txtpincodecity.Text = pincode1;
                    // txtGstStateCode.Text = statecode;
                }
            }
            else
            {
                lblErr.Text = "City Name Is Reuired!";
                setFocusControl(txtCityName);
                modalCity.Show();
            }
            txtpincodecity.Text = "";
            txtSubArea.Text = "";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void drpgroupSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpGroupSummary_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAddGroup_Click1(object sender, EventArgs e)
    {
        try
        {
            string GroupCode = clsCommon.getString("Select ISNULL(MAX(group_Code+1),1) from  " + tblPrefix + "BSGroupMaster where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            txtGroupCode.Text = GroupCode;
            ViewState["group"] = "S";
            txtGroupName.Text = string.Empty;
            setFocusControl(txtGroupName);
            ModalGroupMaster.Show();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtGroupCode.Text != string.Empty)
            {
                if (ViewState["group"].ToString() == "S")
                {
                    string str = clsCommon.getString("select group_Name_E from " + tblPrefix + "BSGroupMaster where group_Code=" + txtGroupCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblGropCodeexist.Text = "Code " + txtGroupCode.Text + " already exist";
                        isValidated = false;
                        return;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }


            }
            string GroupCod = txtGroupCode.Text;
            string GroupName = txtGroupName.Text;
            string group_type = drpgroupSection.SelectedValue;
            string group_Summary = drpGroupSummary.SelectedValue;
            Int32 group_order = txtGroupOrder.Text != string.Empty ? Convert.ToInt32(txtGroupOrder.Text) : 0;
            string user = Session["user"].ToString();

            if (isValidated == true)
            {
                if (!string.IsNullOrEmpty(txtGroupName.Text))
                {
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        string strRev = "";
                        DataSet ds = new DataSet();
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "BSGroupMaster";
                        obj.columnNm = "group_Code,group_Name_E,group_Type,group_Summary,group_Order,Company_Code,Created_By";
                        obj.values = "'" + GroupCod + "','" + GroupName + "','" + group_type + "','" + group_Summary + "','" + group_order + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + user + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        txtGROUP_CODE.Text = GroupCod;
                        lblGROUPNAME.Text = GroupName;
                    }
                }
                else
                {
                    lblGrr.Text = "Required!";
                    ModalGroupMaster.Show();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSendSMS_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string PartyName = txtAC_NAME_E.Text;
            string PartyAddress = txtADDRESS_E.Text;
            string CityName = clsCommon.getString("Select city_name_e from " + tblPrefix + "CityMaster where city_code=" + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
            CityName = CityName == string.Empty ? Convert.ToString(CityName) : " City: " + CityName;
            string State = clsCommon.getString("Select state from " + tblPrefix + "CityMaster where city_code=" + txtCITY_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
            State = State == string.Empty ? Convert.ToString(State) : " State: " + State;
            string PINCODE = txtPINCODE.Text == string.Empty ? Convert.ToString(txtPINCODE.Text) : " PINCODE: " + txtPINCODE.Text.Trim();
            string LOCAL_LIC_NO = txtLOCAL_LIC_NO.Text == string.Empty ? Convert.ToString(txtLOCAL_LIC_NO.Text) : "LIC: " + txtLOCAL_LIC_NO.Text + Environment.NewLine + ",";

            string GST_NO = txtGST_NO.Text.Trim() == string.Empty ? Convert.ToString(txtGST_NO.Text) : "GST: " + txtGST_NO.Text + Environment.NewLine + ",";

            string FSSAI = txtFssaiNo.Text.Trim() == string.Empty ? Convert.ToString(txtFssaiNo.Text) : "FSSAI: " + txtFssaiNo.Text + Environment.NewLine + ",";
            string PAN = txtcompanyPan.Text.Trim() == string.Empty ? Convert.ToString(txtcompanyPan.Text) : "PAN: " + txtcompanyPan.Text + Environment.NewLine + ",";
            string MOBILE = txtSendingMobile.Text;
            string EMAIL_ID = txtSendingEmail.Text;
            string ADHARNO = txtAdhar_No.Text;
            string TANNO = txtTan_No.Text.Trim() == string.Empty ? Convert.ToString(txtTan_No.Text) : "TIN_NO: " + txtTan_No.Text + Environment.NewLine + ",";
            string BankName = txtBANK_NAME.Text == string.Empty ? Convert.ToString(txtBANK_NAME.Text) : "Bank Name: " + txtBANK_NAME.Text + ",";
            string BankAc_number = txtBANK_AC_NO.Text == string.Empty ? Convert.ToString(txtBANK_AC_NO.Text) : "Bank A/c Number: " + txtBANK_AC_NO.Text + ",";
            string BankIFSCode = txtIfsc.Text == string.Empty ? Convert.ToString(txtIfsc.Text) : "IFSC: " + txtIfsc.Text + ",";
            string msg = string.Empty;
            string MsgforMail = string.Empty;
            if (chkBankDetails.Checked == true)
            {
                msg = "Bank Details Of Party <br/>" + PartyName + " " + CityName + " " + PINCODE + " " + State + BankName + " " + BankAc_number + " " + BankIFSCode;
                MsgforMail = "Bank Details Of Party <br/>" + PartyName + " <br/>" + BankName + " <br/>" + BankAc_number + " <br/>" + BankIFSCode;
            }
            if (chkAddressDetails.Checked == true)
            {
                msg = PartyName + " Address:" + PartyAddress + " " + CityName + " " + State + Environment.NewLine + LOCAL_LIC_NO + TIN_NO +
                    CST_NO + GST_NO + ECC_NO + FSSAI + PAN;
                MsgforMail = PartyName + "<br/> Address:" + PartyAddress + " <br/>" + CityName + " <br/>" + State + " <br/>" + PINCODE + " <br/>" + LOCAL_LIC_NO + " <br/>" + TIN_NO + " <br/>" + CST_NO + " <br/>" + GST_NO + " <br/>" + ECC_NO + " <br/>" + FSSAI + " <br/>" + PAN + " <br/>";
            }
            if (e.CommandName == "sms")
            {
                if (!string.IsNullOrWhiteSpace(MOBILE))
                {
                    string msgAPI = clsGV.msgAPI;
                    string URL = msgAPI + "mobile=" + MOBILE + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
                    clsCommon.apicall(URL);
                    //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                    //HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                    //StreamReader reader = new StreamReader(response.GetResponseStream());
                    //string read = reader.ReadToEnd();
                    //reader.Close();
                    //response.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert('SMS  Sent Successfully!')", true);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(EMAIL_ID))
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            string mailFrom = Session["EmailId"].ToString();
                            string smtpPort = "587";
                            string emailPassword = Session["EmailPassword"].ToString();
                            MailMessage msgs = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                            SmtpServer.Host = clsGV.Email_Address;
                            msgs.From = new MailAddress(mailFrom);
                            msgs.To.Add(EMAIL_ID);
                            msgs.Body = MsgforMail;
                            //msgs.Attachments.Add(attachment);
                            msgs.IsBodyHtml = true;
                            msgs.Subject = "Account details...";
                            msgs.IsBodyHtml = true;
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
                            SmtpServer.Send(msgs);
                        }
                    }
                    catch (Exception e1)
                    {
                        Response.Write("mail err:" + e1);
                        return;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert('Email Sent Successfully!')", true);
                }
            }

            //ClearSendingSmsTextboxes();

        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ClearSendingSmsTextboxes()
    {
        txtSendingAcCode.Text = string.Empty;
        txtSendingMobile.Text = string.Empty;
        txtSendingEmail.Text = string.Empty;
        lblSendingAcCode.Text = string.Empty;
        chkAddressDetails.Checked = false;
        chkBankDetails.Checked = false;
    }

    protected void txtSendingAcCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSendingAcCode.Text;
        strTextbox = "txtSendingAcCode";
        csCalculations();

    }
    protected void btntxtSendingAcCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSendingAcCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void chkAddressDetails_CheckedChanged(object sender, EventArgs e)
    {
        chkBankDetails.Checked = false;
    }
    protected void chkBankDetails_CheckedChanged(object sender, EventArgs e)
    {
        chkAddressDetails.Checked = false;
    }

    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                txtSearchText.Text = txtEditDoc_No.Text.ToString();
                strTextbox = "txtAC_CODE";
                btntxtAC_CODE_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry = "select * from " + qryCommon + " where Ac_Code='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region [txtGstStateCode_TextChanged]
    protected void txtGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGstStateCode.Text;
        strTextbox = "txtGstStateCode";
        csCalculations();
    }
    #endregion

    #region [txtstatecode_TextChanged]
    protected void txtstatecode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtstatecode.Text;
        strTextbox = "txtstatecode";
        csCalculations();
        modalCity.Show();
    }
    #endregion


    #region [btntxtGstStateCode_Click]
    protected void btntxtGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [btngststatecode_Click]
    protected void btngststatecode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtstatecode";
            strTextbox = "txtstatecode";
            btnSearch_Click(sender, e);
            modalCity.Show();

        }
        catch
        {
        }
    }
    #endregion

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(Ac_Code) as Ac_Code from " + tblHead + " "));
            if (counts == 0)
            {
                txtAC_CODE.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(Ac_Code),0) as Doc_No from " + qryCommon + " where Company_Code=" + Session["Company_Code"].ToString() + " ")) + 1;
                txtAC_CODE.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(accoid) as accoid from " + tblHead + " "));
            if (counts == 0)
            {
                lblAc_Code.Text = "1";
                Acid = 1;
            }
            else
            {
                Acid = Convert.ToInt32(clsCommon.getString("SELECT max(accoid) as accoid from " + tblHead)) + 1;
                lblAc_Code.Text = Acid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion


    protected void TextBankOpning_Bal_TextChanged(object sender, EventArgs e)
    {

    }
    protected void drpBankDrCr_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string NewInsert(string HeadInsertUpdate, string Detail_Insert, string Detail_Update, string Detail_Delete, string Gledger_Delete, string Gledger_Insert, string status)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "HeadDetail_CrudOpration";
            cmd.CommandType = CommandType.StoredProcedure;
            //an out parameter


            //an in parameter
            cmd.Parameters.AddWithValue("QryInsertAndUpdate", HeadInsertUpdate);
            cmd.Parameters["QryInsertAndUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Insert", Detail_Insert);
            cmd.Parameters["QryDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Update", Detail_Update);
            cmd.Parameters["QryDetail_Update"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Delete", Detail_Delete);
            cmd.Parameters["QryDetail_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryGledger_Delete", Gledger_Delete);
            cmd.Parameters["QryGledger_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryGledger_Insert", Gledger_Insert);
            cmd.Parameters["QryGledger_Insert"].Direction = ParameterDirection.Input;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            _adapter.Fill(ds);
            string msgReturn = "";
            if (status == "Save")
            {
                msgReturn = "Record Successfully Added";
            }
            else if (status == "Update")
            {
                msgReturn = "Record Successfully Update";
            }
            return msgReturn;

        }
        catch
        {
            con.Close();
            return "";
        }
        finally
        {
            con.Close();
        }
    }

    #region [txtInsurance_TextChanged]
    protected void txtInsurance_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInsurance.Text;
        strTextbox = "txtInsurance";
        csCalculations();
    }
    #endregion

}

