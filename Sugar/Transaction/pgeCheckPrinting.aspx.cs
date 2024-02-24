using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;

public partial class Sugar_Transaction_pgeCheckPrinting : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string qryAccountList = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string Action = string.Empty;
    int Detail_ID = 2;
    int Ac_Code = 3;
    int ChqCaption = 5;
    int Amount = 6;
    int Narration = 7;
    int Bank_Date = 8;
    int Mark = 9;
    int Cheque_No = 10;
    int Berror = 11;
    int Mobile_No = 12;
    int Check_Detail_Id = 12;
    int acid = 10;
    int Doc_No = 0;
    int Check_Id = 0;

    DataTable Maindt = null; 
    DataRow dr = null;
    int Rowaction = 13;
    int Srno = 14;

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    string cs = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        #region [Page Load]
        //try
        //{
        //    tblPrefix = Session["tblPrefix"].ToString();
        //    tblHead = "Checkprinting_Head";
        //    tblDetails = "Checkprinting_Detail";
        //    qryCommon = "qryCheckPrinting";
        //    qryAccountList = "qrymstaccountmaster";
        //    user = Session["user"].ToString();
        //    pnlPopup.Style["display"] = "none";

        //    string Doc = Request.QueryString["Doc_No"];
        //    string Action = Request.QueryString["Action"];
        //    string types = Request.QueryString["Tran_Type"];
        //    if (Action == "1")
        //    {
        //        if (Doc == string.Empty)
        //        {
        //            Doc = null;
        //            types = null;

        //        }
        //        Session["CQ_NO"] = Doc;
        //        Session["Tran_TYPE"] = types;
        //    }

        //    if (!Page.IsPostBack)
        //    {
        //        hdnfyearcode.Value = Session["year"].ToString();
        //        hdnfcompanycode.Value = Session["Company_Code"].ToString();
        //        isAuthenticate = Security.Authenticate(tblPrefix, user);
        //        string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
        //        if (isAuthenticate == "1" || User_Type == "A")
        //        {
        //            pnlPopup.Style["display"] = "none";
        //            ViewState["currentTable"] = null;
        //            clsButtonNavigation.enableDisable("N");
        //            this.makeEmptyForm("N");
        //            ViewState["mode"] = "I";
        //            if (Session["CQ_NO"] != null)
        //            {
        //                hdnf.Value = Session["CQ_NO"].ToString();
        //                //--trntype = Session["RP_TYPE"].ToString();

        //                //--drpTrnType.SelectedValue = Session["RP_TYPE"].ToString();
        //                qry = getDisplayQuery();
        //                this.fetchRecord(qry);
        //                this.enableDisableNavigateButtons();
        //                if (Session["Allow"] == "Allow")
        //                {
        //                    btnEdit.Enabled = false;
        //                    btnAdd.Enabled = false;
        //                    btnAuthentication.Visible = true;
        //                }

        //                else
        //                {
        //                    btnAuthentication.Visible = false;
        //                }

        //                Session["CQ_NO"] = null;
        //                Session["Tran_TYPE"] = null;
        //                Session["Allow"] = null;
        //            }
        //            else
        //            {
        //                btnAdd_Click(this, new EventArgs());
        //                //this.showLastRecord();
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
        //        }
        //        if (objAsp != null)
        //            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
        //        if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
        //        {
        //            pnlPopup.Style["display"] = "none";
        //        }
        //        else
        //        {
        //            pnlPopup.Style["display"] = "block";
        //            objAsp = btnSearch;
        //        }
        //    }
        //}
          #endregion
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead =   "Checkprinting_Head";
            tblDetails = "Checkprinting_Detail";
            qryCommon = "qryCheckPrinting"; 
            qryAccountList = "qrymstaccountmaster"; 
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();
            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {

                        hdnfCheckDoc_No.Value = Request.QueryString["Doc_No"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");

                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        this.enableDisableNavigateButtons();
                        setFocusControl(btnEdit);

                        //hdnf.Value = Request.QueryString["saleid"];
                        //pnlPopup.Style["display"] = "none";
                        //ViewState["currentTable"] = null;
                        //clsButtonNavigation.enableDisable("N");
                        //this.makeEmptyForm("N");
                        //ViewState["mode"] = "I";
                        //this.showLastRecord();
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber(); 
                        
                        //setFocusControl(txtAC_CODE);
                        // PurcSaleDo_CoomonFields purc = new PurcSaleDo_CoomonFields();

                    }
                    #region oldcode comment
                    //pnlPopup.Style["display"] = "none";
                    //ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    //this.makeEmptyForm("N");
                    //ViewState["mode"] = "I";
                    //if (Session["SB_NO"] != null)
                    //{
                    //    hdnf.Value = Session["SB_NO"].ToString();
                    //    qry = getDisplayQuery();
                    //    this.fetchRecord(qry);

                    //    Session["SB_NO"] = null;
                    //}
                    //else
                    //{
                    //    this.showLastRecord();
                    //}
                    #endregion
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
            //txtGStno.Text = "27AABHJ9303C1ZM";
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
            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "'"));
            if (counts == 0)
            {
                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(Check_Id) as Check_Id from " + tblHead + " "));
            if (counts == 0)
            {
                lblDoc_No.Text = "1";
                Check_Id = 1;
            }
            else
            {
                Check_Id = Convert.ToInt32(clsCommon.getString("SELECT max(Check_Id) as Check_Id from " + tblHead)) + 1;
                lblDoc_No.Text = Check_Id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
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
                obj.tableName = tblHead + " where ";
                obj.code = "Doc_no";
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
                                    txtDoc_No.Text = ds.Tables[0].Rows[0][0].ToString();
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
    private void makeEmptyForm(string dAction)
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
                pnlPopup.Style["display"] = "none";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;

                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtCash_Bank.Enabled = false;
                btntxtCash_Bank.Enabled = false;
                txtAuthorised.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtValue.Enabled = false;
                txtNarration.Enabled = false;
                txtBank_Date.Enabled = false;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtMark.Enabled = false;
                txtCheque_No.Enabled = false;
                txtBerror.Enabled = false;
                txtMobile_No.Enabled = false;
                btntxtpaymentNo.Enabled = false;
                setFocusControl(btnAdd);
                btnCheck.Enabled = false;
                btnBack.Enabled = true;

            }
            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();

                txtAc_Code.Enabled = true;
                lblAcname.Text = string.Empty;
                lbltxtpayment.Text = string.Empty;
                btntxtAc_Code.Enabled = true;
                txtValue.Enabled = true;
                txtNarration.Enabled = true;
                txtBank_Date.Enabled = true;
                txtBank_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtBank_Date.Enabled = true;
                txtMark.Enabled = true;
                txtCheque_No.Enabled = true;
                txtBerror.Enabled = true;
                txtMobile_No.Enabled = true;

                txtCash_Bank.Enabled = true;
                lblCash_Bank.Text = string.Empty;
                btntxtCash_Bank.Enabled = true;
                txtAuthorised.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                btntxtpaymentNo.Enabled = true;
                //txtpaymentNo.Enabled = false;
                btnCheck.Enabled = false;
                btnBack.Enabled = false;
                lblDoc_No.Text = string.Empty;
                #region set Business logic for save
                #endregion
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;

                txtCash_Bank.Enabled = false;
                btntxtCash_Bank.Enabled = false;
                txtAuthorised.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtValue.Enabled = false;
                txtNarration.Enabled = false;
                txtBank_Date.Enabled = false;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtMark.Enabled = false;
                txtCheque_No.Enabled = false;
                txtBerror.Enabled = false;
                txtMobile_No.Enabled = false;

                txtAc_Code.Text = string.Empty;
                btntxtAc_Code.Enabled = false;
                txtValue.Text = string.Empty;
                txtNarration.Text = string.Empty;
                txtBank_Date.Text = string.Empty;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtMark.Text = string.Empty;
                txtCheque_No.Text = string.Empty;
                txtBerror.Text = string.Empty;
                txtMobile_No.Text = string.Empty;
                btntxtpaymentNo.Enabled = false;
                txtCqucaption.Text = string.Empty;

                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnCheck.Enabled = false;
                btnBack.Enabled = true;
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;

                txtCash_Bank.Enabled = true;
                btntxtCash_Bank.Enabled = true;
                txtAuthorised.Enabled = true;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                txtValue.Enabled = true;
                txtNarration.Enabled = true;
                txtBank_Date.Enabled = true;
                CalendarExtenderDatetxtBank_Date.Enabled = true;
                txtBank_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                txtMark.Enabled = true;
                txtCheque_No.Enabled = true;
                txtBerror.Enabled = true;
                txtMobile_No.Enabled = true;

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btntxtpaymentNo.Enabled = false;
                txtpaymentNo.Enabled = false;
                txtCqucaption.Text = string.Empty;
                btnCheck.Enabled = true;
                btnBack.Enabled = false;
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion
    #region [showLastRecord]
    private void showLastRecord()
    {
        //try
        //{
        //    string qry = string.Empty;
        //    qry = "select max(Doc_No) as Doc_No from " + tblHead +
        //        " where Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    ds = clsDAL.SimpleQuery(qry);
        //    if (ds != null)
        //    {
        //        if (ds.Tables.Count > 0)
        //        {
        //            dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                hdnf.Value = dt.Rows[0]["Doc_No"].ToString();
        //                qry = getDisplayQuery();
        //                bool recordExist = this.fetchRecord(qry);
        //                if (recordExist == true)
        //                {
        //                    btnAdd.Focus();
        //                }
        //                else                     //new code
        //                {
        //                    btnEdit.Enabled = false;
        //                    btnDelete.Enabled = false;
        //                }
        //            }
        //        }
        //    }
        //}
        try
        {
            string qry = string.Empty;
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnAdd.Focus();
            }
            else                     //new code
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
        }
    }
    #endregion
    //#region [enableDisableNavigateButtons]
    //private void enableDisableNavigateButtons()
    //{
        
    //    #region enable disable previous next buttons
    //    int RecordCount = 0;
    //    string query = "";
    //    // query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) " and Sub_Type='" + drpSub_Type.SelectedValue + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString());

    //    query = "select count(*) from " + tblHead + " where  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' ";
    //    string cnt = clsCommon.getString(query);
    //    if (cnt != string.Empty)
    //    {
    //        RecordCount = Convert.ToInt32(cnt);
    //    }

    //    if (RecordCount != 0 && RecordCount == 1)
    //    {
    //        btnFirst.Enabled = true;
    //        btnPrevious.Enabled = false;
    //        btnNext.Enabled = false;
    //        btnLast.Enabled = false;
    //    }
    //    else if (RecordCount != 0 && RecordCount > 1)
    //    {
    //        btnFirst.Enabled = true;
    //        btnPrevious.Enabled = false;
    //        btnNext.Enabled = false;
    //        btnLast.Enabled = true;
    //    }
    //    if (txtDoc_No.Text != string.Empty)
    //    {
    //        #region check for next or previous record exist or not

    //        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnfCheckDoc_No.Value) +
    //            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
    //            "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc  ";
    //        string strDoc_No = clsCommon.getString(query);
    //        if (strDoc_No != string.Empty)
    //        {
    //            //next record exist
    //            btnNext.Enabled = true;
    //            btnLast.Enabled = true;
    //        }
    //        else
    //        {
    //            //next record does not exist
    //            btnNext.Enabled = false;
    //            btnLast.Enabled = false;
    //        }


    //        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + int.Parse(hdnfCheckDoc_No.Value) +
    //            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
    //             "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No desc  ";
    //        strDoc_No = clsCommon.getString(query);
    //        if (strDoc_No != string.Empty)
    //        {
    //            //previous record exist
    //            btnPrevious.Enabled = true;
    //            btnFirst.Enabled = true;
    //        }
    //        else
    //        {
    //            btnPrevious.Enabled = false;
    //            btnFirst.Enabled = false;
    //        }
    //        #endregion
    //    }
    //    #endregion
    //}
    //#endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
        }
        if (txtDoc_No.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [Doc_No] from " + tblHead +
                    " where Doc_No>" + Convert.ToInt32(hdnfCheckDoc_No.Value) +
                    " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                        }
                    }
                }
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnfCheckDoc_No.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY doc_no asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnPrevious.Enabled = true;
                            btnFirst.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                            btnFirst.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        #endregion
    }
    #endregion
    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where   Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
            hdnfCheckDoc_No.Value = clsCommon.getString(query);
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
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnfCheckDoc_No.Value) + " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
                hdnfCheckDoc_No.Value = clsCommon.getString(query);
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
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnfCheckDoc_No.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' ORDER BY Doc_No asc  ";
                hdnfCheckDoc_No.Value = clsCommon.getString(query);
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + " where Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
            hdnfCheckDoc_No.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion
    #region [btnCheck_Click]
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (btnCheck.Text == "Check All")
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                CheckBox chkMsg = (CheckBox)grdDetail.Rows[i].Cells[15].FindControl("chkMsg");
                chkMsg.Checked = true;
            }
            btnCheck.Text = "Unckecked All";
        }
        else
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                CheckBox chkMsg = (CheckBox)grdDetail.Rows[i].Cells[15].FindControl("chkMsg");
                chkMsg.Checked = false;
            }
            btnCheck.Text = "Check All";
        }
    }
    #endregion
    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string check = txtEditDoc_No.Text;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
            setFocusControl(txtDoc_No);
            pnlPopupDetails.Style["display"] = "none";
            pnlgrdDetail.Enabled = true;
            Int32 Doc_No = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from " + tblHead + "  where Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Tran_Type='CQ'"));

            if (Doc_No != 0)
            {
                int doc_no = Doc_No + 1;
                Doc_No = doc_no;
            }
            else
            {
                Doc_No = 1;
            }
            txtDoc_No.Text = Convert.ToString(Doc_No);
            setFocusControl(txtDoc_Date);
        }
        else
        {
            btnCancel_Click(this, new EventArgs());
        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_No);
    }
    #endregion

    //#region [btnDelete_Click]
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (hdconfirm.Value == "Yes")
    //        {
    //            int userid = Convert.ToInt32(Session["User_Id"].ToString());
    //            string pagevalidation = clsCommon.getString("Select Permission from tbluserdetail where Tran_Type ='CQ' and User_Id=" + userid + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString());
    //            if (pagevalidation == "Y")
    //            {

    //            }
    //            else
    //            {
    //                //setFocusControl(txtDoc_Date);
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You are Not Authorize Person to Delete Record please Contact Admistrator!!!!!');", true);
    //                return;
    //            }
    //            string str = string.Empty;
    //            DataSet xml_ds = new DataSet();
    //            if (str == string.Empty)
    //            {
    //                #region
    //                //string currentDoc_No = txtDoc_No.Text;
    //                //DataSet ds = new DataSet();
    //                //string strrev = "";
    //                //qry = "delete from " + tblDetails + " where doc_no=" + currentDoc_No + "  and Company_Code="
    //                //   + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
    //                //ds = clsDAL.SimpleQuery(qry);
    //                //using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
    //                //{
    //                //    obj.flag = 3;
    //                //    obj.tableName = tblHead;
    //                //    obj.columnNm = "Doc_No=" + currentDoc_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
    //                //    obj.values = "none";
    //                //    ds = obj.insertAccountMaster(ref strrev);
    //                //}
    //                //string query = "";
    //                //if (strrev == "-3")
    //                //{
    //                //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(currentDoc_No) + " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
    //                //    hdnf.Value = clsCommon.getString(query);
    //                //    if (hdnf.Value == string.Empty)
    //                //    {
    //                //        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(currentDoc_No) + " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
    //                //        hdnf.Value = clsCommon.getString(query);
    //                //    }
    //                //    if (hdnf.Value != string.Empty)
    //                //    {
    //                //        query = getDisplayQuery();
    //                //        bool recordExist = this.fetchRecord(query);
    //                //        this.makeEmptyForm("S");
    //                //        clsButtonNavigation.enableDisable("S");
    //                //    }
    //                //    else
    //                //    {
    //                //        this.makeEmptyForm("N");
    //                //        clsButtonNavigation.enableDisable("N");
    //                //        btnEdit.Enabled = false;
    //                //        btnDelete.Enabled = false;
    //                //    }
    //                //}
    //                //this.enableDisableNavigateButtons();
    //                #endregion

    //                string currentDoc_No = txtDoc_No.Text;
    //                DataSet ds = new DataSet();
    //                string strrev = "";
    //                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
    //                {
    //                    XElement root = new XElement("ROOT");
    //                    XElement child1 = new XElement("Head");
    //                    int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
    //                    int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
    //                    string strRev = string.Empty;
    //                    child1.SetAttributeValue("Doc_No", txtDoc_No.Text);
    //                    child1.SetAttributeValue("Company_Code", Company_Code);
    //                    child1.SetAttributeValue("Year_Code", Year_Code);
    //                    //child1.SetAttributeValue("Tran_Type", "");
    //                    child1.SetAttributeValue("Authentication", user + "Deleted");
    //                    root.Add(child1);
    //                    string XMLReport = root.ToString();
    //                    XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
    //                    XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
    //                    string spname = "SP_Checkprinting_Head";
    //                    string xmlfile = XMLReport;
    //                    string op = "";
    //                    string returnmaxno = "";
    //                    int flag = 10;
    //                    xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
    //                    strrev = op;
    //                }
    //                string query = "";
    //                if (strrev == "-3")
    //                {


    //                    DataTable dt_dalete = xml_ds.Tables[0];
    //                    string HDNF_VALUE = "";
    //                    if (dt_dalete.Rows.Count > 0)
    //                    {
    //                        HDNF_VALUE = dt_dalete.Rows[0]["Doc_No"].ToString();
    //                    }
    //                    hdnf.Value = HDNF_VALUE;

    //                    if (hdnf.Value == string.Empty)
    //                    {
    //                        DataTable dt_dalete_two = xml_ds.Tables[1];
    //                        if (dt_dalete_two.Rows.Count > 0)
    //                        {
    //                            hdnf.Value = dt_dalete_two.Rows[0]["Doc_No"].ToString();
    //                        }
    //                    }
    //                    if (hdnf.Value != string.Empty)
    //                    {
    //                        query = getDisplayQuery();
    //                        bool recordExist = this.fetchRecord(query);
    //                        this.makeEmptyForm("S");
    //                        clsButtonNavigation.enableDisable("S");
    //                    }
    //                    else
    //                    {
    //                        this.makeEmptyForm("N");
    //                        clsButtonNavigation.enableDisable("N");
    //                        btnEdit.Enabled = false;
    //                        btnDelete.Enabled = false;
    //                    }
    //                }
    //                this.enableDisableNavigateButtons();
    //            }
    //            else
    //            {
    //                lblMsg.Text = "Cannot delete this Group , it is in use";
    //                lblMsg.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
            }
        }
        catch
        {

        }
    }
    #endregion
    protected void btnCheck_Printing_Click(object sender, EventArgs e)
    {
        try
        {
            //string fromdt = txtFromDt.Text;
            //string todt = txtToDt.Text;
            //fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            //string fromdt1 = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //string todt1 = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //string unit = drpFilter.SelectedValue;


            if (txtDoc_No.Text != string.Empty)
            {
                string Doc_No = txtDoc_No.Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kys", "javascript:CheckPrinting('" + Doc_No + "')", true);

            }
        }
        catch (Exception eex)
        {

        }

    }
    //protected void btnmultipleprint_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //string fromdt = txtFromDt.Text;
    //        //string todt = txtToDt.Text;
    //        //fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
    //        //todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


    //        //string fromdt1 = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
    //        //string todt1 = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
    //        //string unit = drpFilter.SelectedValue;


    //        if (txtDoc_No.Text != string.Empty)
    //        {
    //            string Doc_No = txtDoc_No.Text;

    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kys", "javascript:MP('" + Doc_No + "')", true);

    //        }
    //    }
    //    catch (Exception eex)
    //    {

    //    }

    //}
    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (hdnf.Value != string.Empty)
        {
            string query = getDisplayQuery();
            bool recordExist = this.fetchRecord(query);
        }
        else
        {
            this.showLastRecord();
        }
        string qry = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        if (qry != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            this.enableDisableNavigateButtons();
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");
            this.enableDisableNavigateButtons();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
     
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
                        Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        if (lblCreatedDate != null)
                        {
                            if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblCreatedDate.Text = "Created Date" + dt.Rows[0]["Created_Date"].ToString();
                            }
                        }
                        if (lblModifiedDate != null)
                        {
                            if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblModifiedDate.Text = "Modified Date" + dt.Rows[0]["Modified_Date"].ToString();
                            }
                        }

                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnfCheckDoc_No.Value = txtDoc_No.Text;
                        hdnf.Value = dt.Rows[0]["Check_Id"].ToString();
                        lblDoc_No.Text = hdnf.Value;
                        hdnfCheck_id.Value = lblDoc_No.Text;
                        txtCash_Bank.Text = dt.Rows[0]["Cash_Bank"].ToString();
                        //lblCash_Bank.Text = dt.Rows[0]["CBAcName"].ToString();
                        txtAuthorised.Text = dt.Rows[0]["Authorised"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_Date"].ToString();
                        txtpaymentNo.Text = dt.Rows[0]["Payment_No"].ToString();
                        lbltxtpayment.Text = dt.Rows[0]["Payment_Name"].ToString();
                        lblCash_Bank.Text = dt.Rows[0]["Cash_BankName"].ToString();

                        hdnfcbcode.Value = dt.Rows[0]["cbid"].ToString();
                        hdnfpaymentcode.Value = dt.Rows[0]["pid"].ToString();

                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select Detail_ID,Ac_Code,ChqCaption,Amount,Narration,Bank_Dateconvert as Bank_Date,Mark,Cheque_No,Berror,Mobile_No,Check_Detail_Id,acid,Ac_Code_Name from " + qryCommon + "  where Detail_ID is not null and Company_Code='"
                            + Session["Company_Code"].ToString() + "'And Doc_No='" + txtDoc_No.Text
                            + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
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
                        double subtotal = 0.00;
                        #region calculate subtotal
                        if (grdDetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < grdDetail.Rows.Count; i++)
                            {
                                if (grdDetail.Rows[i].Cells[14].Text != "D")
                                {
                                    double Amt = Convert.ToDouble(grdDetail.Rows[i].Cells[6].Text.Trim());
                                    subtotal = subtotal + Amt;
                                    lblChqAmountBalance.Text = subtotal.ToString();
                                }
                            }
                        }
                        #endregion
                        #endregion
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            hdnf.Value = txtDoc_No.Text;
            this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }

            if (strTextBox == "txtCash_Bank")
            {
                //string acname = "";
                //if (txtCash_Bank.Text != string.Empty)
                //{
                //    bool a = clsCommon.isStringIsNumeric(txtCash_Bank.Text);
                //    if (a == false)
                //    {
                //        btntxtCash_Bank_Click(this, new EventArgs());
                //    }
                //    else
                //    {

                //        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtCash_Bank.Text
                //            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                //        if (acname != string.Empty)
                //        {
                //            lblCash_Bank.Text = acname;

                //            setFocusControl(txtAc_Code);

                //        }
                //        else
                //        {
                //            txtCash_Bank.Text = string.Empty;
                //            lblAcname.Text = acname;
                //            setFocusControl(txtCash_Bank);
                //        }
                //    }
                //}

                string cbname = string.Empty;
                if (txtCash_Bank.Text != string.Empty)
                {
                    searchString = txtCash_Bank.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtCash_Bank_Click(this, new EventArgs());
                    }
                    else
                    {
                        string qry = "";
                        hdnfcbcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtCash_Bank.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtCash_Bank.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        cbname = clsCommon.getString(qry);

                        if (cbname != string.Empty)
                        {
                            lblCash_Bank.Text = cbname;
                            setFocusControl(txtAc_Code);
                        }
                        else
                        {
                            lblCash_Bank.Text = string.Empty;
                            txtCash_Bank.Text = string.Empty;
                            setFocusControl(txtCash_Bank);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }
            }
            if (strTextBox == "txtAuthorised")
            {
                setFocusControl(txtAuthorised);
            }
            //if (strTextBox == "txtpaymentNo")
            //{
            //    string acname = "";
            //    if (txtpaymentNo.Text != string.Empty)
            //    {
            //        bool a = clsCommon.isStringIsNumeric(txtpaymentNo.Text);
            //        if (a == false)
            //        {
            //            btntxtpaymentNo_Click(this, new EventArgs());

            //        }
            //        else
            //        {
            //            acname = "select 0 + ROW_NUMBER() over  ( order by Ac_Name_E) as  Detail_ID,Ac_Code as Ac_Code,Ac_Name_E as Ac_Code_Name,ChqCaption, " +
            //                "Payment as Amount,'' as Narration,Doc_Date as Bank_Date,'' as Mark,'' as Cheque_No," +
            //                "'N' as Berror,Ac_Mobile_No_1 as Mobile_No,'A'as rowAction, 0 + ROW_NUMBER() over  ( order by AC_CODE) as SrNo " +
            //                "from  qrySupplierPayment_Detail where Payment <>0 and Doc_No not  in(select [Payment_No] from  Checkprinting_Head where Payment_No is not null) and Company_Code="
            //                + Convert.ToInt32(Session["Company_Code"].ToString())
            //                + " and [Doc_No]=" + txtpaymentNo.Text
            //                 + "order by Ac_Code_Name";
            //            DataSet ds = clsDAL.SimpleQuery(acname);
            //            if (ds.Tables.Count > 0)
            //            {
            //                DataTable dt = ds.Tables[0];
            //                if (dt.Rows.Count > 0)
            //                {

            //                    grdDetail.DataSource = dt;
            //                    grdDetail.DataBind();
            //                    ViewState["currentTable"] = dt;
            //                    setFocusControl(txtCash_Bank);
            //                    pnlgrdDetail.Enabled = true;
            //                }
            //                else
            //                {


            //                    grdDetail.DataSource = null;
            //                    grdDetail.DataBind();
            //                    ViewState["currentTable"] = null;
            //                    setFocusControl(txtCash_Bank);
            //                }

            //            }
            //            else
            //            {


            //                grdDetail.DataSource = null;
            //                grdDetail.DataBind();
            //                ViewState["currentTable"] = null;
            //                setFocusControl(txtCash_Bank);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        setFocusControl(txtCash_Bank);
            //    }
            //}
            if (strTextBox == "txtpaymentNo")
            {
                string partyName = string.Empty;
                if (txtpaymentNo.Text != string.Empty)
                {
                    searchString = txtpaymentNo.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtpaymentNo_Click(this, new EventArgs());
                    }
                    else
                    {
                        string qry = "";
                        hdnfpaymentcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtpaymentNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtpaymentNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        partyName = clsCommon.getString(qry);

                        if (partyName != string.Empty)
                        {
                            lbltxtpayment.Text = partyName;
                            setFocusControl(txtCash_Bank);
                        }
                        else
                        {
                            lbltxtpayment.Text = string.Empty;
                            txtpaymentNo.Text = string.Empty;
                            setFocusControl(txtpaymentNo);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtCash_Bank);
                }
            }
            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    {
                        setFocusControl(txtpaymentNo);
                    }
                    else
                    {
                        // txtDoc_Date.Text = "";
                        setFocusControl(txtDoc_Date);
                    }
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtAc_Code")
            {
               
                //if (txtAc_Code.Text != string.Empty)
                //{
                //    bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                //    if (a == false)
                //    {
                //        btntxtAc_Code_Click(this, new EventArgs());
                //    }
                //    else
                //    {
                //        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                //            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                //        string chqcaption = clsCommon.getString("select ChqCaption from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                //       + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //        mobileno = clsCommon.getString("select Mobile_No from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                //           + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //        if (acname != string.Empty)
                //        {
                //            DateTime DateNow = DateTime.Now;
                //            string date = DateNow.ToString("yyyy/MM/dd");
                //            double balance = Convert.ToDouble(clsCommon.getString("select isnull(sum(balance),0) from  qryGLegderamountOnChq where Ac_Code=" + txtAc_Code.Text
                //       + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //       " and DOC_DATE<='" + date + "'"));

                //            lblAcname.Text = acname;
                //            txtMobile_No.Text = mobileno;
                //            txtCqucaption.Text = chqcaption;
                //            lblChqAmountBalance.Text = Math.Abs(balance).ToString();
                //            if (balance < 0)
                //            {
                //                lblDeb_Credit.Text = "Dr";
                //            }
                //            else if (balance == 0)
                //            {
                //                lblDeb_Credit.Text = string.Empty;
                //            }
                //            else
                //            {
                //                lblDeb_Credit.Text = "Cr";

                //            }
                //            setFocusControl(txtCqucaption);

                //        }
                //        else
                //        {
                //            txtAc_Code.Text = string.Empty;
                //            lblAcname.Text = acname;
                //            setFocusControl(txtAc_Code);
                //        }
                //    }
                //}
                string acname = string.Empty;
                if (txtAc_Code.Text != string.Empty)
                {
                    searchString = txtAc_Code.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        string qry = "";
                        hdnfaccode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtAc_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtAc_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                        string chqcaption = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                       + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); 

                        acname = clsCommon.getString(qry);
                        if (acname != string.Empty)
                        {
                            lblAcname.Text = acname;
                            //txtCqucaption.Text = chqcaption;
                            setFocusControl(txtCqucaption);
                        }
                        else
                        {
                            lblAcname.Text = string.Empty;
                            txtAc_Code.Text = string.Empty;
                            setFocusControl(txtAc_Code);
                        }
                    }
                } 
                else
                {
                    setFocusControl(txtAc_Code);
                }


            }
            if (strTextBox == "txtValue")
            {
                setFocusControl(txtValue);
            }
            if (strTextBox == "txtNarration")
            {
                setFocusControl(txtNarration);
            }
            if (strTextBox == "txtBank_Date")
            {
                setFocusControl(txtBank_Date);
            }
            if (strTextBox == "txtMark")
            {
                setFocusControl(txtMark);
            }
            if (strTextBox == "txtCheque_No")
            {
                setFocusControl(txtCheque_No);
            }
            if (strTextBox == "txtBerror")
            {
                setFocusControl(txtBerror);
            }
            if (strTextBox == "txtMobile_No")
            {
                setFocusControl(btnAdddetails);
            }


        }
        catch
        {
        }
    }
    #endregion
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        //try
        //{
        //    string qryDisplay = " select * from " + qryCommon
        //        + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
        //        + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Doc_No=" + hdnf.Value;
        //    //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        //    return qryDisplay;
        //}
        try
        {
            //string qryDisplay = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and Tran_Type='" + trntype + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            string qryDisplay = "select * from " + qryCommon + " where Doc_No=" + hdnfCheckDoc_No.Value + " ";
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
                txtDoc_No.Text = hdnf.Value;
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
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        double amount;
        double.TryParse(txtValue.Text, out amount);
        if (amount == 0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct amount! ');", true);

            setFocusControl(txtValue);
            return;
        }
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
                if (dt.Rows[0]["Detail_ID"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString());
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
                            rowIndex = maxIndex;          //1txtf
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["Detail_ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_ID from " + tblDetails + " where Detail_ID='" + lblID.Text +
                            "' and Doc_No=" + txtDoc_No.Text
                            + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
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
                    dt.Columns.Add((new DataColumn("Detail_ID", typeof(int))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Ac_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Ac_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("ChqCaption", typeof(string))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("Bank_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Mark", typeof(string))));
                    dt.Columns.Add((new DataColumn("Cheque_No", typeof(string))));
                    dt.Columns.Add((new DataColumn("Berror", typeof(string))));
                    dt.Columns.Add((new DataColumn("Mobile_No", typeof(string))));
                    dt.Columns.Add((new DataColumn("Check_Detail_Id", typeof(int))));
                    dt.Columns.Add((new DataColumn("acid", typeof(int))));

                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["Detail_ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_ID", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Ac_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Ac_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("ChqCaption", typeof(string))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                dt.Columns.Add((new DataColumn("Bank_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("Mark", typeof(string))));
                dt.Columns.Add((new DataColumn("Cheque_No", typeof(string))));
                dt.Columns.Add((new DataColumn("Berror", typeof(string))));
                dt.Columns.Add((new DataColumn("Mobile_No", typeof(string))));
                dt.Columns.Add((new DataColumn("Check_Detail_Id", typeof(int))));
                dt.Columns.Add((new DataColumn("acid", typeof(int))));

                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            if (txtAc_Code.Text != string.Empty)
            {
                dr["Ac_Code"] = txtAc_Code.Text;
            }
            else
            {
                setFocusControl(txtAc_Code);
                return;
            }
            dr["Ac_Code_Name"] = lblAcname.Text;
            dr["Amount"] = txtValue.Text;
            dr["Narration"] = Server.HtmlDecode(txtNarration.Text);
            dr["acid"] = hdnfaccode.Value;

            if (txtBank_Date.Text != string.Empty)
            {
                dr["Bank_Date"] = DateTime.Parse(txtBank_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                //dr["Bank_Date"] = txtBankDate.Text;
            }
            else
            {
                //    setFocusControl(txtBankDate);
                dr["Bank_Date"] = txtBank_Date.Text;
                //    return;
                // Anamat_DetailID
            }

            if (txtCqucaption.Text != string.Empty)
            {
                dr["ChqCaption"] = txtCqucaption.Text;

                //dr["Bank_Date"] = txtBankDate.Text;
            }
            else
            {
                //    setFocusControl(txtBankDate);
                dr["ChqCaption"] = "";
                //    return;
                // Anamat_DetailID
            }
            dr["Mark"] = Server.HtmlDecode(txtMark.Text);
            dr["Cheque_No"] = txtCheque_No.Text;
            dr["Berror"] = txtBerror.Text;
            dr["Mobile_No"] = Server.HtmlDecode(txtMobile_No.Text);

            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["Check_Detail_Id"] = 0;
                dt.Rows.Add(dr);
            }
            //if (btnAdddetails.Text == "ADD")
            //{
            //    dt.Rows.Add(dr);
            //}
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
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtAc_Code);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }


            double subtotal = 0.00;
            #region calculate subtotal
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[14].Text != "D")
                    {
                        double Amt = Convert.ToDouble(grdDetail.Rows[i].Cells[6].Text.Trim());
                        subtotal = subtotal + Amt;
                        lblChqAmountBalance.Text = subtotal.ToString();
                    }
                } 
            }
            #endregion
            // Empty Code->
            txtAc_Code.Text = string.Empty;
            lblAcname.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtNarration.Text = string.Empty;
            //txtBank_Date.Text = string.Empty;
            txtMark.Text = string.Empty;
            txtCheque_No.Text = string.Empty;
            txtBerror.Text = string.Empty;
            txtMobile_No.Text = string.Empty;
            txtCqucaption.Text = string.Empty; 
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
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;
        txtAc_Code.Text = string.Empty;
        txtValue.Text = string.Empty;
        txtNarration.Text = string.Empty;
        txtBank_Date.Text = string.Empty;
        txtMark.Text = string.Empty;
        txtCheque_No.Text = string.Empty;
        txtBerror.Text = string.Empty;
        txtMobile_No.Text = string.Empty;
        lblAcname.Text = string.Empty;
        txtCqucaption.Text = string.Empty;

        btnAdddetails.Text = "ADD";
        setFocusControl(txtAc_Code);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[15].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtAc_Code.Text = Server.HtmlDecode(gvrow.Cells[Ac_Code].Text);
        lblAcname.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtValue.Text = Server.HtmlDecode(gvrow.Cells[6].Text);
        txtNarration.Text = Server.HtmlDecode(gvrow.Cells[Narration].Text);
        txtBank_Date.Text = Server.HtmlDecode(gvrow.Cells[Bank_Date].Text);
        txtMark.Text = Server.HtmlDecode(gvrow.Cells[Mark].Text);
        txtCheque_No.Text = Server.HtmlDecode(gvrow.Cells[Cheque_No].Text);
        txtBerror.Text = Server.HtmlDecode(gvrow.Cells[Berror].Text);
        txtMobile_No.Text = Server.HtmlDecode(gvrow.Cells[Mobile_No].Text);
        txtCqucaption.Text = Server.HtmlDecode(gvrow.Cells[ChqCaption].Text);
        hdnfaccode.Value = Server.HtmlDecode(gvrow.Cells[16].Text); 

        //txtTran_Type.Text = Server.HtmlDecode(gvrow.Cells[Tran_Type].Text);
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_ID"].ToString());
                string IDExisting = clsCommon.getString("select Detail_ID from " + tblDetails + " where Doc_No='" + hdnf.Value
                    + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "N";// Add rowaction id
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
                        grdDetail.Rows[rowIndex].Cells[14].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "A";
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

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            //e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            //e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            //e.Row.Cells[2].Style["overflow"] = "hidden";
            //e.Row.Cells[4].Style["overflow"] = "hidden";

            //e.Row.Cells[2].Width = new Unit("20px");
            //e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            //e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            //e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            //e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            //e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            ////--------------------------------------------------
            //e.Row.Cells[Ac_Code].ControlStyle.Width = new Unit("50px");
            //e.Row.Cells[Ac_Code].Style["overflow"] = "hidden";
            //e.Row.Cells[Ac_Code].HorizontalAlign = HorizontalAlign.Left;

            //e.Row.Cells[4].ControlStyle.Width = new Unit("350px");
            //e.Row.Cells[4].Style["overflow"] = "hidden";
            //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;

            //e.Row.Cells[ChqCaption].ControlStyle.Width = new Unit("200px");
            //e.Row.Cells[ChqCaption].Style["overflow"] = "hidden";
            //e.Row.Cells[ChqCaption].HorizontalAlign = HorizontalAlign.Left;
            ////--------------------------------------------------
            //e.Row.Cells[Amount].ControlStyle.Width = new Unit("100px");
            //e.Row.Cells[Amount].Style["overflow"] = "hidden";
            //e.Row.Cells[Amount].HorizontalAlign = HorizontalAlign.Right;
            ////--------------------------------------------------
            //e.Row.Cells[Narration].ControlStyle.Width = new Unit("70px");
            //e.Row.Cells[Narration].Style["overflow"] = "hidden";
            //e.Row.Cells[Narration].HorizontalAlign = HorizontalAlign.Left;
            ////--------------------------------------------------
            //e.Row.Cells[Bank_Date].ControlStyle.Width = new Unit("100px");
            //e.Row.Cells[Bank_Date].Style["overflow"] = "hidden";
            //e.Row.Cells[Bank_Date].HorizontalAlign = HorizontalAlign.Left;
            ////--------------------------------------------------
            //e.Row.Cells[Mark].ControlStyle.Width = new Unit("60px");
            //e.Row.Cells[Mark].Style["overflow"] = "hidden";
            //e.Row.Cells[Mark].HorizontalAlign = HorizontalAlign.Left;

            ////--------------------------------------------------
            //e.Row.Cells[Cheque_No].ControlStyle.Width = new Unit("70px");
            //e.Row.Cells[Cheque_No].Style["overflow"] = "hidden";
            //e.Row.Cells[Cheque_No].HorizontalAlign = HorizontalAlign.Left;
            ////--------------------------------------------------
            //e.Row.Cells[Berror].ControlStyle.Width = new Unit("50px");
            //e.Row.Cells[Berror].Style["overflow"] = "hidden";
            //e.Row.Cells[Berror].HorizontalAlign = HorizontalAlign.Left;
            ////--------------------------------------------------
            //e.Row.Cells[Mobile_No].ControlStyle.Width = new Unit("120px");
            //e.Row.Cells[Mobile_No].Style["overflow"] = "hidden";
            //e.Row.Cells[Mobile_No].HorizontalAlign = HorizontalAlign.Left;
            ////--------------------------------------------------
            //e.Row.Cells[Check_Detail_Id].ControlStyle.Width = new Unit("150px");
            //e.Row.Cells[Check_Detail_Id].Style["overflow"] = "hidden";
            //e.Row.Cells[Check_Detail_Id].HorizontalAlign = HorizontalAlign.Left;

            //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[0].Style["overflow"] = "hiden";
            //e.Row.Cells[0].Visible = true; 
            //e.Row.Cells[Rowaction].Visible = true;
            //e.Row.Cells[Srno].Visible = true;

            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Detail_ID].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Ac_Code].ControlStyle.Width = new Unit("70px");
            //e.Row.Cells[Ac_Code].Visible = false;
            e.Row.Cells[4].ControlStyle.Width = new Unit("200px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("200px");
            e.Row.Cells[5].Style["overflow"] = "hiden";
            e.Row.Cells[6].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[17].ControlStyle.Width = new Unit("50px");






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
            e.Row.Cells[0].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtpaymentNo")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            }
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;
        }
    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
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
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
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
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[Rowaction].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "UPDATE";
                            setFocusControl(txtAc_Code);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
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
    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtEditDoc_No.Text;
        //strTextBox = "txtEditDoc_No";
        //csCalculations();
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                searchString = txtEditDoc_No.Text;
                strTextBox = "txtEditDoc_No";
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry1 = getDisplayQuery();
                fetchRecord(qry1);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtDoc_No.Text;
        //strTextBox = "txtDoc_No";
        //csCalculations();
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtDoc_No.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtDoc_No.Text != string.Empty)
                {
                    txtValue = txtDoc_No.Text;

                    string qry = "select * from " + tblHead + " where  Doc_No='" + txtValue + "' " +
                        "  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'";

                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //Record Found
                                hdnf.Value = dt.Rows[0]["Doc_No"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtCash_Bank);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtDoc_No.Enabled = false;
                                            setFocusControl(txtCash_Bank);

                                            hdnf.Value = txtDoc_No.Text;
                                            txtEditDoc_No.Text = string.Empty;
                                        }
                                    }
                                }
                            }
                            else   //Record Not Found
                            {
                                if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                {
                                    lblMsg.Text = "";
                                    setFocusControl(txtCash_Bank);
                                    txtDoc_No.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtDoc_No.Text = string.Empty;
                                    setFocusControl(txtDoc_No);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtDoc_No);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtDoc_No.Text = string.Empty;
                setFocusControl(txtDoc_No);
            }
        }
        catch
        {

        }
        #endregion
    }
    #endregion
    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtCash_Bank_TextChanged]
    protected void txtCash_Bank_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCash_Bank.Text;
        strTextBox = "txtCash_Bank";
        csCalculations();
    }
    #endregion
    #region [btntxtCash_Bank_Click]
    protected void btntxtCash_Bank_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCash_Bank";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtAuthorised_TextChanged]
    protected void txtAuthorised_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAuthorised.Text;
        strTextBox = "txtAuthorised";
        csCalculations();
    }
    #endregion
    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt = new DataTable();
        dt = (DataTable)ViewState["currentTable"];
        int myrows = grdDetail.Rows.Count;
        long mycheckNo;
        mycheckNo = Convert.ToInt32(txtfrom.Text);
        for (int i = 0; i < myrows; i++)
        {
            dr = (DataRow)dt.Rows[i];
            dr[8] = mycheckNo;
            mycheckNo = mycheckNo + 1;

        }
        grdDetail.DataSource = dt;
        grdDetail.DataBind();
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
    }


    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
    }
    #endregion

    #region [txtAc_Code_TextChanged]
    protected void txtpaymentNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtpaymentNo.Text;
        strTextBox = "txtpaymentNo";
        csCalculations();
    }
    #endregion

    #region [btntxtAc_Code_Click]
    protected void btntxtAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAc_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtAc_Code_Click]
    protected void btntxtpaymentNo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtpaymentNo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtValue_TextChanged]
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtValue.Text;
        strTextBox = "txtValue";
        csCalculations();
    }
    #endregion


    #region [txtNarration_TextChanged]
    protected void txtNarration_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration.Text;
        strTextBox = "txtNarration";
        csCalculations();
    }
    #endregion


    #region [txtBank_Date_TextChanged]
    protected void txtBank_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBank_Date.Text;
        strTextBox = "txtBank_Date";
        csCalculations();
    }
    #endregion


    #region [txtMark_TextChanged]
    protected void txtMark_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMark.Text;
        strTextBox = "txtMark";
        csCalculations();
    }
    #endregion


    #region [txtCheque_No_TextChanged]
    protected void txtCheque_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheque_No.Text.Trim();
        strTextBox = "txtCheque_No";
        csCalculations();
    }
    #endregion


    #region [txtBerror_TextChanged]
    protected void txtBerror_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBerror.Text.Trim();
        strTextBox = "txtBerror";
        setFocusControl(txtMobile_No);
        //csCalculations();
    }
    #endregion


    #region [txtMobile_No_TextChanged]
    protected void txtMobile_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtMobile_No.Text.Trim();
        //searchString = searchString.Replace("@", string.Empty);
        //txtMobile_No.Text = searchString;
        strTextBox = "txtMobile_No";
        //setFocusControl(btnAdddetails);
        // csCalculations();
        //   modalCity.Style["display"] = "none";



    }
    #endregion


    //#region [txtTran_Type_TextChanged]
    //protected void txtTran_Type_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = txtTran_Type.Text;
    //    strTextBox = "txtTran_Type";
    //    csCalculations();
    //}
    //#endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string searchtxt = "";
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = "";
            string[] split = null;
            string name = string.Empty;
            if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
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
                if (btntxtDoc_No.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDoc_No.Text = string.Empty;
                    txtDoc_No.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtDoc_No);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtDoc_No.Text == "Choose No")
                {
                    searchString = txtEditDoc_No.Text;
                    strTextBox = "txtEditDoc_No";
                    pnlPopup.Style["display"] = "block";
                    if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
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
                        name += "( Doc_No Like '%" + aa + "%' or Ac_Name_E Like '%" + aa + "%'   ) and";
                        //name += "( Part_No like '%" + aa + "%' or Part_Type like '%" + aa + "%' or Part_Name_E like '%" + aa + "%' or Sub_Group_Code like '%" + aa + "%' or Rate_Kg like '%" + aa + "%' or Rate_Qty like '%" + aa + "%' or Weight like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%'  ) and";
                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select doc no--";
                    string qry = "  select Doc_No,Doc_Date,Ac_Name_E,Amount,Narration,Cheque_No from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and (" + name + ") order by Doc_No desc";
                    // string qry = " select Part_No,Part_Type,Part_Name_E,Sub_Group_Code,Rate_Kg,Rate_Qty,Weight,Ac_Name_E from  qrypartmasternewreportA where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Part_No";
                    this.showPopup(qry);
                }
            }


            if (hdnfClosePopup.Value == "txtCash_Bank")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();


                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or CityName like '%" + aa + "%' ) and";


                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Branch Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from  qrymstaccountmaster   where Ac_Type='B' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + "  and " + name + " order by Ac_Name_E";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtpaymentNo")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or CityName like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Branch Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from  qrymstaccountmaster   where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + "  and " + name + " order by Ac_Name_E";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                lblPopupHead.Text = "--Select AC Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
                    " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' ) ORDER BY Ac_Name_E asc";
                this.showPopup(qry);
                //string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                //     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or city_name_e  like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";

                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMobile_No")
            {
                pnlPopup.Style["display"] = "Block";
                foreach (var s in split)
                {

                    string aa = s.ToString();


                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' ) and";


                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Branch Code--";
                string qry = "select Ac_Code,Ac_Name_E,Ac_Mobile_No_1 from  Account_Master   where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                 + " and  " + name + " order by Ac_Name_E";

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
            setFocusControl(txtSearchText);
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
                strTextBox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion


    protected void btnSMS_Click(object sender, CommandEventArgs e)
    {
        //string script = "$(document).ready(function () { $('[id*=btnSMS]').click(); });";
        //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
        //  System.Threading.Thread.Sleep(500);
        //thread  System.Threading.Thread.CurrentThread.Abort();

        // this.showLastRecord();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Processed!');", true);
        //Response.AddHeader("Refresh", "3");
        //for (int i = 0; i < grdDetail.Rows.Count; i++)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowProgress()", true);
        //}
        //System.Threading.Thread.Sleep(5000);
        // Response.Redirect(Request.RawUrl);
        //this.Page_Load(this,new EventArgs ());
        modalCity.Show();

        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {

            string msg = string.Empty;
            string MsgforMail = string.Empty;

            //msg = "Party Name: " + PartyName + " ,Invoice Date :" + date + " ,Invoice No : " + invoiceno + " ,Quantity :" + qty + " ,Bill Amount :" + amount;
            //  MsgforMail = "Banbr/>k Details Of Party <br/>" + PartyName + " <br/>" + BankName + " <br/>" + BankAc_number + " <br/>" + BankIFSCode;
            CheckBox chkMsg = (CheckBox)grdDetail.Rows[i].Cells[15].FindControl("chkMsg");
            if (chkMsg.Checked == true)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                string code = grdDetail.Rows[i].Cells[Mobile_No].Text;
                //                string subject = "Please Collect Cheque From Office Cheq.no" + grdDetail.Rows[i].Cells[Cheque_No].Text + " Amount: " + grdDetail.Rows[i].Cells[Amount].Text + " From Bhavani Ind.pvt.Ltd.Kolhapur " + Session["User"];
                //string subject = "Please Collect Cheque From Office Cheq.no" + grdDetail.Rows[i].Cells[Cheque_No].Text + " Amount: " + grdDetail.Rows[i].Cells[Amount].Text + " From Bhavani Ind.pvt.Ltd.Kolhapur " + Session["User"];
                string subject = "Please%20Collect%20Cheque%20From%20office%20%20cheq.no" + grdDetail.Rows[i].Cells[Cheque_No].Text + " Amount " + grdDetail.Rows[i].Cells[Amount].Text + " from%20Bhavani%20Iron%20Ind.%20pvt.Ltd.%20Kolhapur";
                if (code != "")
                {
                    msg = subject;

                    foreach (string singleSubject in code.Split(','))
                    {
                        if (!dict.ContainsKey(singleSubject))
                        {
                            string partymob = string.Empty;
                            partymob = singleSubject;
                            if (e.CommandName == "sms")
                            {
                                if (!string.IsNullOrWhiteSpace(partymob))
                                {
                                    //string msgAPI = clsGV.msgAPI;
                                    ////                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=BHVANI&accusage=1";
                                    ////string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=BHVANI&accusage=1";
                                    //string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=LATASW&accusage=1";
                                    //clsCommon.apicall(URL);
                                    //clsCommon.SendMySMS(partymob, msg, "");

                                }
                            }

                            //txtPartyMobno.Text = singleSubject;

                            // dict.Add(subject, new List<string> { code });
                        }
                        else
                        {
                            dict[subject].Add(code);
                        }
                    }
                }
                lblsucess.Text = "Sucessfully processed";

            }
            else
            {

            }

        }


    }


    protected void imgClose_Click(object sender, EventArgs e)
    {
    }

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hdnfyearcode.Value != Session["year"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
            return;
            // Response.Redirect("~/Sugar/pgeHome.aspx", false);
        }
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Purchase", "javascript:pagevalidation();", true);

    }
    #endregion

    #region [btnAuthentication_Click]
    protected void btnAuthentication_Click(object sender, EventArgs e)
    {
        if (hdconfirm.Value == "Yes")
        {

            #region -Head part declearation

            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));

            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string retValue = string.Empty;
            string strRev = string.Empty;
            #endregion-End of Head part declearation


            #region save Head Master
            #region --------------------  Details --------------------
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                string CheckingFlag = string.Empty;
                XElement child2 = new XElement("Head");
                Int32 Detail_ID = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);

                child2.SetAttributeValue("Cash_Bank", txtCash_Bank.Text != string.Empty ? txtCash_Bank.Text : "0");
                child2.SetAttributeValue("Doc_No", txtDoc_No.Text);
                child2.SetAttributeValue("Company_Code", Company_Code);
                child2.SetAttributeValue("Year_Code", Year_Code);
                child2.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));


                child2.SetAttributeValue("Tran_Type", "CQ");
                // txtAc_Code.Text != string.Empty ? txtAc_Code.Text : "0"
                child2.SetAttributeValue("Amount", grdDetail.Rows[i].Cells[Amount].Text);
                child2.SetAttributeValue("Ac_Code", grdDetail.Rows[i].Cells[Ac_Code].Text);
                child2.SetAttributeValue("Narration", grdDetail.Rows[i].Cells[Narration].Text + " Chq.no " + grdDetail.Rows[i].Cells[Cheque_No].Text);
                child2.SetAttributeValue("Authentication", Session["user"].ToString());
                child2.SetAttributeValue("Modify_Date", DateTime.Parse(DateTime.Now.ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));



                child2.SetAttributeValue("Detail_ID", grdDetail.Rows[i].Cells[2].Text);
                string s = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bank_Date].Text);
                string bankdate = s;
                DateTime bdate;
                string dateba = "";
                string BankDT = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bank_Date].Text);
                BankDT = BankDT.Trim();
                if (s == string.Empty || s == "01/01/1900")
                {
                    bdate = DateTime.Parse(txtDoc_Date.Text);
                    bdate = bdate.AddDays(2);
                    child2.SetAttributeValue("Bank_Date", bdate.ToString("yyyy/MM/dd"));
                }
                else
                {

                    child2.SetAttributeValue("Bank_Date", DateTime.Parse(Server.HtmlDecode(BankDT), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
                }
                //string BankDT = Server.HtmlDecode(grdDetail.Rows[i].Cells[Bank_Date].Text);
                //BankDT = BankDT.Trim();

                //if (BankDT != "")
                //{
                //    child2.SetAttributeValue("Bank_Date", DateTime.Parse(Server.HtmlDecode(BankDT), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
                //    //child2.SetAttributeValue("Bank_Date", DateTime.Parse(grdDetail.Rows[i].Cells[Bank_Date].Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
                //}
                //else
                //{
                //    child2.SetAttributeValue("Bank_Date", Server.HtmlDecode(BankDT));

                //}

                root.Add(child2);
            }
            #endregion



            string XMLReport = root.ToString();
            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            DataSet xml_ds = new DataSet();
            string spname = "SP_Checkprinting_Head";
            string xmlfile = XMLReport;
            string op = "";
            string returnmaxno = "";
            int flag;

            #region[Insert]
            flag = 7;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion


            //txtDoc_No.Text = returnmaxno;
            //hdnf.Value = txtDoc_No.Text;
            // retValue = op;
            #endregion

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CQ", "javascript:auth();", true);
        }
        else
        {
            setFocusControl(btnAuthentication);
            return;

        }

    }
    #endregion

    private void saveinacessdata(int docno)
    {
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Access/chqprinting.mdb");
        //  string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\project\sqlacess\bhavani\chqprintin.mdb";
        string strSQL = "delete FROM chqprinting where companycode=" + Session["Company_Code"] +
            " and yearcode=" + Session["year"] + " and doc_no=" + docno;

        // Create a connection  
        DataSet ds = new DataSet();

        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            // Create a command and set its connection  
            OleDbCommand command = new OleDbCommand(strSQL, connection);
            // Open the connection and execute the select command.  
            try
            {
                // Open connecton  
                connection.Open();
                OleDbDataAdapter adp;
                adp = new OleDbDataAdapter(command);
                adp.Fill(ds);
                string insertrecord = "";
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    string doc_date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    string bank_date = DateTime.Parse(grdDetail.Rows[i].Cells[Bank_Date].Text.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                    insertrecord = " insert into chqprinting (doc_no,doc_date,cashbank,ac_code,amount,chqno,berror,narration,authorised," +
                        "companycode,created,modify,yearcode,ordercode,tran_type,mark,inword,mobile,bankdate) " +
                        "values (" + docno + ",#" + doc_date + "#," + txtCash_Bank.Text + "," + grdDetail.Rows[i].Cells[Ac_Code].Text + ","
                        + grdDetail.Rows[i].Cells[Amount].Text + ",'" + grdDetail.Rows[i].Cells[Cheque_No].Text + "','" + grdDetail.Rows[i].Cells[Berror].Text +
                        "','" + grdDetail.Rows[i].Cells[Narration].Text.Trim() + "','" + "'," + Session["Company_Code"] + ",'" + Session["user"] + "','" + Session["user"] + "',"
                     + Session["year"] + "," + grdDetail.Rows[i].Cells[Detail_ID].Text + "," + "'CQ','" + grdDetail.Rows[i].Cells[Mark].Text.Trim() + "',"
                        + "'','" + grdDetail.Rows[i].Cells[Mobile_No].Text + "',#" + bank_date + "#); ";
                    // insertrecord.Replace("&nbsp;", "");
                    string qry = insertrecord.Replace("&nbsp;", "");
                    command = new OleDbCommand(qry, connection);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
            }
        }
    }
}