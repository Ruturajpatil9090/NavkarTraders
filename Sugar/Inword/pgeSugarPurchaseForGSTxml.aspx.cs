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

public partial class Sugar_pgeSugarPurchaseForGSTxml : System.Web.UI.Page
{

    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string AccountMasterView = string.Empty;
    string SystemMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string qryHead = string.Empty;
    string qryDetail = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string trntype = "PS";
    int defaultAccountCode = 0;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    int Doc_No = 0;
    int purchase_id = 0;
    #endregion
    #region Head Part Declare
    int doc_no = 0;

    string Tran_Type = string.Empty;
    int PURCNO = 0;
    string doc_date = string.Empty;
    int Ac_Code = 0;
    int Unit_Code = 0;
    int mill_code = 0;
    string FROM_STATION = string.Empty;
    string TO_STATION = string.Empty;
    string LORRYNO = string.Empty;
    int BROKER = 0;
    string wearhouse = string.Empty;
    double subTotal = 0.00;
    double LESS_FRT_RATE = 0.00;
    double freight = 0.00;
    double cash_advance = 0.00;
    double bank_commission = 0.00;
    double OTHER_AMT = 0.00;
    double Bill_Amount = 0.00;
    int Due_Days = 0;

    double NETQNTL = 0.00;
    double CGSTRate = 0.00;
    double CGSTAmount = 0.00;
    double SGSTRate = 0.00;
    double SGSTAmount = 0.00;
    double IGSTRate = 0.00;
    double IGSTAmount = 0.00;
    int GstRateCode = 0;
    int Company_Code = 0;
    int Year_Code = 0;
    int Branch_Code = 0;
    string Created_By = string.Empty;
    string Bill_No = string.Empty;
    string EWay_Bill_No = string.Empty;
    #endregion

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    string cs = string.Empty;
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "SugarPurchase";
            tblDetails = tblPrefix + "SugarPurchaseDetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = "qrypurchaseheaddetail";
            qryHead = "qrypurchasehead";
            qryDetail = "qrypurchasedetail";
            qryAccountList = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
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
                        hdnf.Value = Request.QueryString["purchaseid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        this.enableDisableNavigateButtons();
                        setFocusControl(btnEdit);
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtAC_CODE);
                        // PurcSaleDo_CoomonFields purc = new PurcSaleDo_CoomonFields();

                    }
                    //pnlPopup.Style["display"] = "none";
                    //ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    //this.makeEmptyForm("N");
                    //ViewState["mode"] = "I";

                    //if (Session["PURC_NO"] != null)
                    //{
                    //    hdnf.Value = Session["PURC_NO"].ToString();
                    //    qry = getDisplayQuery();
                    //    this.fetchRecord(qry);
                    //    this.enableDisableNavigateButtons();
                    //    Session["PURC_NO"] = null;
                    //}
                    //else
                    //{
                    //    this.showLastRecord();
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
        catch
        {
        }
    }
    #endregion

    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Enter Only Numbers!')", true);
            }
            else
            {
                int Purc_id = Convert.ToInt32(clsCommon.getString("select purchaseid from nt_1_sugarpurchase where doc_no=" + txtEditDoc_No.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " and " +
                    " Year_Code=" + Session["year"].ToString() + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:PUrchaseOPen('" + Purc_id + "')", true);

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                obj.code = "doc_no";
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
                                    txtDOC_NO.Text = ds.Tables[0].Rows[0][0].ToString();
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
                txtEditDoc_No.Enabled = true;
                pnlPopup.Style["display"] = "none";
                // btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                #region logic
                LblPartyname.Text = "";
                lblUnitName.Text = "";
                LBLMILLNAME.Text = "";
                LBLBROKERNAME.Text = "";
                lblGSTRateName.Text = "";
                btntxtAC_CODE.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtBROKER.Enabled = false;
                btntxtdoc_no.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btntxtPURCNO.Enabled = false;
                calenderExtenderDate.Enabled = false;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                btntxtUnitcode.Enabled = false;
                txtEway_Bill_No.Enabled = false;

                txtITEM_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
                txtQUANTAL.Enabled = false;
                txtPACKING.Enabled = false;
                txtBAGS.Enabled = false;
                txtRATE.Enabled = false;
                txtITEMAMOUNT.Enabled = false;
                txtITEM_NARRATION.Enabled = false;
                #endregion

                txtITEM_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
                txtQUANTAL.Enabled = false;
                txtPACKING.Enabled = false;
                txtBAGS.Enabled = false;
                txtRATE.Enabled = false;
                txtITEMAMOUNT.Enabled = false;
                txtITEM_NARRATION.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtmill_inv_date.Enabled = false;
                CalendarExtenderMill_Inv.Enabled = false;
                txtTCSRate.Enabled = false;
                txtTCSAmt.Enabled = false;
                txtTCSNet_Payable.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                drpreatilstock.Enabled = false;

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
                txtEditDoc_No.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = true;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                #region set Business logic for save
                LblPartyname.Text = "";
                lblUnitName.Text = "";
                LBLMILLNAME.Text = "";
                LBLBROKERNAME.Text = "";
                lblGSTRateName.Text = "";
                btntxtAC_CODE.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtBROKER.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtPURCNO.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtEway_Bill_No.Enabled = true;
                // btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                btntxtUnitcode.Enabled = true;
                txtEway_Bill_No.Enabled = true;
                txtPURCNO.Enabled = true;
                btntxtPURCNO.Enabled = true;
                txtDOC_NO.Enabled = false;
                btntxtdoc_no.Enabled = false;
                txtPACKING.Text = "50";
                txtDUE_DAYS.Text = "1";
                #endregion

                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
                txtQUANTAL.Enabled = true;
                txtPACKING.Enabled = true;
                txtBAGS.Enabled = true;
                txtRATE.Enabled = true;
                txtITEMAMOUNT.Enabled = true;
                txtITEM_NARRATION.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtmill_inv_date.Enabled = true;
                CalendarExtenderMill_Inv.Enabled = true;
                txtmill_inv_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtTCSRate.Enabled = true;
                txtTCSAmt.Enabled = true;
                txtTCSNet_Payable.Enabled = true;
                //txtTCSRate.Text = Session["TCSRate"].ToString();
                string docdate = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

                if (dEnd >= ss)
                {
                    txtTCSRate.Text = "0.00";
                    txtTDS.Text = Session["PurchaseTDSRate"].ToString(); ;

                }
                else
                {
                    txtTCSRate.Text = Session["TCSRate"].ToString();
                    txtTDS.Text = "0.00";

                }

                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                drpreatilstock.Enabled = true;

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
                txtEditDoc_No.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                // btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                btntxtUnitcode.Enabled = false;
                #region logic
                btntxtAC_CODE.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtBROKER.Enabled = false;
                calenderExtenderDate.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btntxtPURCNO.Enabled = false;
                #endregion

                txtITEM_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
                txtQUANTAL.Enabled = false;
                txtPACKING.Enabled = false;
                txtBAGS.Enabled = false;
                txtRATE.Enabled = false;
                txtITEMAMOUNT.Enabled = false;
                txtITEM_NARRATION.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtmill_inv_date.Enabled = false;
                CalendarExtenderMill_Inv.Enabled = false;
                txtTCSRate.Enabled = false;
                txtTCSAmt.Enabled = false;
                txtTCSNet_Payable.Enabled = false;

                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                drpreatilstock.Enabled = false;
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
                txtEditDoc_No.Enabled = false;
                txtDOC_NO.Enabled = false;
                btntxtdoc_no.Enabled = false;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                // btnOpenDetailsPopup.Enabled = true;
                pnlgrdDetail.Enabled = true;
                btntxtUnitcode.Enabled = true;
                txtEway_Bill_No.Enabled = true;
                #region logic
                btntxtAC_CODE.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtBROKER.Enabled = true;
                calenderExtenderDate.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtPURCNO.Enabled = true;
                txtPURCNO.Enabled = false;
                btntxtPURCNO.Enabled = false;
                if (txtPURCNO.Text != "0")
                {

                    txtDOC_DATE.Enabled = false;
                    calenderExtenderDate.Enabled = false;
                }

                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
                txtQUANTAL.Enabled = true;
                txtPACKING.Enabled = true;
                txtBAGS.Enabled = true;
                txtRATE.Enabled = true;
                txtITEMAMOUNT.Enabled = true;
                txtITEM_NARRATION.Enabled = true;
                #endregion

                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
                txtQUANTAL.Enabled = true;
                txtPACKING.Enabled = true;
                txtBAGS.Enabled = true;
                txtRATE.Enabled = true;
                txtITEMAMOUNT.Enabled = true;
                txtITEM_NARRATION.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtmill_inv_date.Enabled = true;
                CalendarExtenderMill_Inv.Enabled = true;
                txtTCSRate.Enabled = true;
                txtTCSAmt.Enabled = true;
                txtTCSNet_Payable.Enabled = true;

                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                drpreatilstock.Enabled = true;
            }
            if (dAction == "A" || dAction == "N")
            {
                drpreatilstock.SelectedValue = "Y";
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [showLastRecord]
    private void showLastRecord()
    {
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


    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select purchaseid from " + tblHead + " where purchaseid=(select MIN(purchaseid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
            if (txtDOC_NO.Text != string.Empty)
            {
                string query = "SELECT top 1 [purchaseid] from " + tblHead + " where purchaseid<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY purchaseid DESC  ";
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
            if (txtDOC_NO.Text != string.Empty)
            {
                string query = "SELECT top 1 [purchaseid] from " + tblHead + " where purchaseid>" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY purchaseid asc  ";
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
            query = "select purchaseid from " + tblHead + " where purchaseid=(select MAX(purchaseid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
        if (txtDOC_NO.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [doc_no] from " + tblHead +
                    " where purchaseid>" + Convert.ToInt32(hdnf.Value) +
                    " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY doc_no asc  ";
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where purchaseid<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
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

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.NextNumber();
        txtGSTRateCode.Text = "1";
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        lblGSTRateName.Text = gstname;
        setFocusControl(txtPURCNO);
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
        txtDOC_NO.Enabled = false;
        btntxtdoc_no.Enabled = false;
        if (txtPURCNO.Text != string.Empty && txtPURCNO.Text != "0")
        {
            grdDetail.Enabled = false;
            btnAdddetails.Enabled = false;
        }
        setFocusControl(txtITEM_CODE);
    }
    #endregion

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

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnf.Value = Request.QueryString["purchaseid"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(purchaseid),0) as purchaseid from nt_1_sugarpurchase "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgeSugarPurchaseForGSTxml.aspx?purchaseid=" + hdnf.Value + "&Action=" + 1);
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
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string yearcode = Session["year"].ToString();
                        if (hdnfyearcode.Value != yearcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same !')", true);
                            return false;
                        }
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();

                        txtDOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        hdnfpurchasedoc.Value = txtDOC_NO.Text;
                        lblPurchase_Id.Text = hdnf.Value;
                        hdnfpurchaseid.Value = lblPurchase_Id.Text;
                        txtPURCNO.Text = dt.Rows[0]["PURCNO"].ToString();
                        lblSelfBal.Text = dt.Rows[0]["SelfBal"].ToString();
                        // txtgrade.Text = clsCommon.getString("select grade from " + tblPrefix + "deliveryorder where doc_no=" + txtPURCNO.Text +
                        // " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        txtmill_inv_date.Text = dt.Rows[0]["mill_inv_dateConverted"].ToString();
                        txtgrade.Text = dt.Rows[0]["grade"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["AC_CODE"].ToString();
                        LblPartyname.Text = dt.Rows[0]["suppliername"].ToString();
                        txtUnit_Code.Text = dt.Rows[0]["Unit_Code"].ToString();
                        lblUnitName.Text = dt.Rows[0]["unitname"].ToString();
                        txtMILL_CODE.Text = dt.Rows[0]["MILL_CODE"].ToString();
                        LBLMILLNAME.Text = dt.Rows[0]["millname"].ToString();
                        txtFROM_STATION.Text = dt.Rows[0]["FROM_STATION"].ToString();
                        txtTO_STATION.Text = dt.Rows[0]["TO_STATION"].ToString();
                        txtLORRYNO.Text = dt.Rows[0]["LORRYNO"].ToString();
                        txtBROKER.Text = dt.Rows[0]["BROKER"].ToString();
                        LBLBROKERNAME.Text = dt.Rows[0]["brokername"].ToString();
                        txtWEARHOUSE.Text = dt.Rows[0]["WEARHOUSE"].ToString();
                        txtSUBTOTAL.Text = dt.Rows[0]["SUBTOTAL"].ToString();
                        txtLESS_FRT_RATE.Text = dt.Rows[0]["LESS_FRT_RATE"].ToString();
                        txtFREIGHT.Text = dt.Rows[0]["FREIGHT"].ToString();
                        txtCASH_ADVANCE.Text = dt.Rows[0]["CASH_ADVANCE"].ToString();
                        txtBANK_COMMISSION.Text = dt.Rows[0]["BANK_COMMISSION"].ToString();
                        txtOTHER_AMT.Text = dt.Rows[0]["OTHER_AMT"].ToString();
                        txtBILL_AMOUNT.Text = dt.Rows[0]["BILL_AMOUNT"].ToString();
                        txtDUE_DAYS.Text = dt.Rows[0]["DUE_DAYS"].ToString();
                        txtNETQNTL.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtbillNo.Text = dt.Rows[0]["Bill_No"].ToString();
                        txtGSTRateCode.Text = dt.Rows[0]["GstRateCode"].ToString();
                        lblGSTRateName.Text = dt.Rows[0]["GST_Name"].ToString();

                        txtEway_Bill_No.Text = dt.Rows[0]["EWay_Bill_No"].ToString();

                        txtCGSTRate.Text = dt.Rows[0]["CGSTRate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["SGSTRate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["IGSTRate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["IGSTAmount"].ToString();

                        hdnfmc.Value = dt.Rows[0]["mc"].ToString();
                        hdnfac.Value = dt.Rows[0]["ac"].ToString();
                        hdnfuc.Value = dt.Rows[0]["uc"].ToString();
                        hdnfbk.Value = dt.Rows[0]["bk"].ToString();

                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();

                        txtTDS.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();
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
                        drpreatilstock.SelectedValue = dt.Rows[0]["Retail_Stock"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";

                        hdnfMillshort.Value = dt.Rows[0]["millshortname"].ToString(); ;
                        hdnfBrokerShort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + txtBROKER.Text + " and company_code=" + Session["Company_Code"].ToString() + "");


                        #region  Details
                        qry = "select detail_id as ID,item_code,itemname as item_Name,Brand_Code,Brand_Name,narration,Quantal,packing,bags,rate,item_Amount,purchasedetailid," +
                            " '' as rowAction,'' as SrNo,ic from " + qryDetail + " where purchaseid=" + hdnf.Value + " ";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    //dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    //dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
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
                        #endregion

                        var item = "";
                        if (grdDetail.Rows[0].Cells[3].Text == "")
                        {
                            item = "0";
                        }
                        else
                        {
                            item = grdDetail.Rows[0].Cells[3].Text;
                        }
                        hdnfPurchaseAc.Value = clsCommon.getString("select Purchase_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        hdnfPurchaseAcid.Value = clsCommon.getString("select PurcAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }

            if (txtPURCNO.Text != string.Empty && txtPURCNO.Text != "0")
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                pnlgrdDetail.Enabled = false;




            }
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + qryHead + " where purchaseid=" + hdnf.Value + " ";
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
                txtDOC_NO.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                // this.enableDisableNavigateButtons();
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
        // pnlPopupDetails.Style["display"] = "block";
        txtPACKING.Text = "50";
        txtITEM_CODE.Text = "";
        txtQUANTAL.Text = "";
        txtBAGS.Text = "";
        txtRATE.Text = "";
        txtITEMAMOUNT.Text = "";
        txtITEM_NARRATION.Text = "";
        LBLITEMNAME.Text = "";
        setFocusControl(txtITEM_CODE);
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtRATE.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtRATE);
                return;
            }

            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["ID"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
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
                        dr["ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        // update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + txtDOC_NO.Text + " and Detail_ID=" + lblID.Text + " And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        if (id != "0")
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
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));

                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("item_code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("Quantal", typeof(double))));
                    dt.Columns.Add((new DataColumn("packing", typeof(double))));
                    dt.Columns.Add((new DataColumn("bags", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("purchasedetailid", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add(new DataColumn("ic", typeof(string)));
                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(int))));

                #region [Write here columns]
                dt.Columns.Add((new DataColumn("item_code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("narration", typeof(string))));
                dt.Columns.Add((new DataColumn("Quantal", typeof(double))));
                dt.Columns.Add((new DataColumn("packing", typeof(double))));
                dt.Columns.Add((new DataColumn("bags", typeof(Int32))));
                dt.Columns.Add((new DataColumn("rate", typeof(double))));
                dt.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("purchasedetailid", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add(new DataColumn("ic", typeof(string)));

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["item_code"] = txtITEM_CODE.Text;
            dr["item_Name"] = LBLITEMNAME.Text;
            dr["Brand_Code"] = txtBrand_Code.Text;
            dr["Brand_Name"] = lblBrandname.Text;

            dr["narration"] = txtITEM_NARRATION.Text;
            dr["ic"] = clsCommon.getString("select systemid from qrymstitem where System_Code=" + txtITEM_CODE.Text + " and Company_Code=" + Session["company_Code"].ToString() + "");
            if (txtQUANTAL.Text != string.Empty)
            {
                dr["Quantal"] = txtQUANTAL.Text;
            }
            else
            {
                setFocusControl(txtQUANTAL);
            }
            if (txtPACKING.Text != string.Empty)
            {
                dr["packing"] = txtPACKING.Text;
            }
            else
            {
                setFocusControl(txtPACKING);
            }

            dr["bags"] = txtBAGS.Text;
            if (txtRATE.Text != string.Empty)
            {
                dr["rate"] = txtRATE.Text;
            }
            else
            {
                setFocusControl(txtRATE);
            }
            if (txtITEMAMOUNT.Text != string.Empty)
            {
                dr["item_Amount"] = txtITEMAMOUNT.Text;
            }
            else
            {
                setFocusControl(txtITEMAMOUNT);
            }
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["purchasedetailid"] = 0;
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
                // pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtITEM_CODE);
            }
            else
            {
                // pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtITEM_CODE);
                // btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            var item = "";
            if (grdDetail.Rows[0].Cells[3].Text == "")
            {
                item = "0";
            }
            else
            {
                item = grdDetail.Rows[0].Cells[3].Text;
            }
            hdnfPurchaseAc.Value = clsCommon.getString("select Purchase_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
            hdnfPurchaseAcid.Value = clsCommon.getString("select PurcAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
            txtITEM_CODE.Text = "";
            LBLITEMNAME.Text = string.Empty;
            txtITEM_NARRATION.Text = "";
            txtQUANTAL.Text = "";
            txtRATE.Text = "";
            txtITEMAMOUNT.Text = "";
            txtBrand_Code.Text = "";
            lblBrandname.Text = "";

            txtBAGS.Text = "";
            csCalculations();
            setFocusControl(txtITEM_CODE);
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
        txtITEM_CODE.Text = string.Empty;
        LBLITEMNAME.Text = string.Empty;
        txtBrand_Code.Text = string.Empty;
        lblBrandname.Text = string.Empty;
        txtQUANTAL.Text = string.Empty;
        txtPACKING.Text = string.Empty;
        txtBAGS.Text = string.Empty;
        txtRATE.Text = string.Empty;
        txtITEMAMOUNT.Text = string.Empty;
        txtITEM_NARRATION.Text = string.Empty;
        btnAdddetails.Text = string.Empty;
        btnClosedetails.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        btnClosedetails.Text = "Close";
        setFocusControl(txtITEM_CODE);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[15].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);
        txtITEM_CODE.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        LBLITEMNAME.Text = clsCommon.getString("Select System_Name_E from " + tblPrefix + "SystemMaster where System_Code=" + Server.HtmlDecode(gvrow.Cells[3].Text.ToString()) + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        txtBrand_Code.Text = Server.HtmlDecode(gvrow.Cells[5].Text);
        lblBrandname.Text = Server.HtmlDecode(gvrow.Cells[6].Text.ToString());
        txtITEM_NARRATION.Text = Server.HtmlDecode(gvrow.Cells[7].Text);
        txtITEM_NARRATION.Text = txtITEM_NARRATION.Text.Trim();
        txtQUANTAL.Text = Server.HtmlDecode(gvrow.Cells[8].Text);
        txtPACKING.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        txtBAGS.Text = Server.HtmlDecode(gvrow.Cells[10].Text);
        txtRATE.Text = Server.HtmlDecode(gvrow.Cells[11].Text);
        txtITEMAMOUNT.Text = Server.HtmlDecode(gvrow.Cells[12].Text);
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["purchasedetailid"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where purchasedetailid=" + ID + " ");
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[14].Text = "R";       //R=Only remove fro grid
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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;

        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtAC_CODE" || v == "txtUnit_Code" || v == "txtMILL_CODE" || v == "txtBROKER")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("300px");
                e.Row.Cells[2].Width = new Unit("100px");

            }
            if (v == "txtITEM_CODE")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("200px");
            }
            if (v == "txtGSTRateCode")
            {
                e.Row.Cells[0].Width = new Unit("90px");
                e.Row.Cells[1].Width = new Unit("150px");
                e.Row.Cells[2].Width = new Unit("90px");
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
                        if (grdDetail.Rows[rowindex].Cells[12].Text != "D" && grdDetail.Rows[rowindex].Cells[12].Text != "R")
                        {
                            // pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
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
        csCalculations();
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("100px");
            //e.Row.Cells[10].Visible = false;

            e.Row.Cells[12].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("60px");
            int i = 1;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                i++;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string s = cell.Text.ToString();
                    if (cell.Text.Length > 20)
                    {
                        cell.Text = cell.Text.Substring(0, 31) + "..";
                        cell.ToolTip = s;
                    }
                }
            }
            //}
        }
        catch
        {
        }
    }
    #endregion

    #region [txtDOC_NO_TextChanged]
    protected void txtDOC_NO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_NO.Text;
        strTextBox = "txtDOC_NO";
        csCalculations();
    }
    #endregion

    #region [btntxtDOC_NO_Click]
    protected void btntxtDOC_NO_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDOC_NO";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtPURCNO_TextChanged]
    protected void txtPURCNO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPURCNO.Text;
        strTextBox = "txtPURCNO";
        csCalculations();
    }
    #endregion

    #region [btntxtPURCNO_Click]
    protected void btntxtPURCNO_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPURCNO";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtDOC_DATE_TextChanged]
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_DATE.Text;
        strTextBox = "txtDOC_DATE";
        csCalculations();
    }
    #endregion

    #region [txtAC_CODE_TextChanged]
    protected void txtAC_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAC_CODE.Text;
        strTextBox = "txtAC_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtAC_CODE_Click]
    protected void btntxtAC_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAC_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtMILL_CODE_TextChanged]
    protected void txtMILL_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMILL_CODE.Text;
        strTextBox = "txtMILL_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtMILL_CODE_Click]
    protected void btntxtMILL_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMILL_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtFROM_STATION_TextChanged]
    protected void txtFROM_STATION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFROM_STATION.Text;
        strTextBox = "txtFROM_STATION";
        csCalculations();
    }
    #endregion

    #region [txtTO_STATION_TextChanged]
    protected void txtTO_STATION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTO_STATION.Text;
        strTextBox = "txtTO_STATION";
        csCalculations();
    }
    #endregion

    #region [txtLORRYNO_TextChanged]
    protected void txtLORRYNO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLORRYNO.Text;
        strTextBox = "txtLORRYNO";
        csCalculations();
    }
    #endregion

    #region [txtBROKER_TextChanged]
    protected void txtBROKER_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBROKER.Text;
        strTextBox = "txtBROKER";
        csCalculations();
    }
    #endregion

    #region [btntxtBROKER_Click]
    protected void btntxtBROKER_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBROKER";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtWEARHOUSE_TextChanged]
    protected void txtWEARHOUSE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWEARHOUSE.Text;
        strTextBox = "txtWEARHOUSE";
        csCalculations();
    }
    #endregion

    #region [txtSUBTOTAL_TextChanged]
    protected void txtSUBTOTAL_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSUBTOTAL.Text;
        strTextBox = "txtSUBTOTAL";
        csCalculations();
    }
    #endregion

    #region [txtLESS_FRT_RATE_TextChanged]
    protected void txtLESS_FRT_RATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLESS_FRT_RATE.Text;
        strTextBox = "txtLESS_FRT_RATE";
        csCalculations();
    }
    #endregion

    #region [txtFREIGHT_TextChanged]
    protected void txtFREIGHT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFREIGHT.Text;
        strTextBox = "txtFREIGHT";
        csCalculations();
    }
    #endregion

    #region [txtCASH_ADVANCE_TextChanged]
    protected void txtCASH_ADVANCE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCASH_ADVANCE.Text;
        strTextBox = "txtCASH_ADVANCE";
        csCalculations();
    }
    #endregion

    #region [txtBANK_COMMISSION_TextChanged]
    protected void txtBANK_COMMISSION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBANK_COMMISSION.Text;
        strTextBox = "txtBANK_COMMISSION";
        csCalculations();
    }
    #endregion

    #region [txtOTHER_AMT_TextChanged]
    protected void txtOTHER_AMT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOTHER_AMT.Text;
        strTextBox = "txtOTHER_AMT";
        csCalculations();
    }
    #endregion

    #region [txtBILL_AMOUNT_TextChanged]
    protected void txtBILL_AMOUNT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBILL_AMOUNT.Text;
        strTextBox = "txtBILL_AMOUNT";
        csCalculations();
    }
    #endregion

    #region [txtDUE_DAYS_TextChanged]
    protected void txtDUE_DAYS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDUE_DAYS.Text;
        strTextBox = "txtDUE_DAYS";
        csCalculations();
    }
    #endregion

    #region [txtNETQNTL_TextChanged]
    protected void txtNETQNTL_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNETQNTL.Text;
        strTextBox = "txtNETQNTL";
        csCalculations();
    }
    #endregion

    #region [txtITEM_CODE_TextChanged]
    protected void txtITEM_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtITEM_CODE.Text;
        strTextBox = "txtITEM_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtITEM_CODE_Click]
    protected void btntxtITEM_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtITEM_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtITEM_NARRATION_TextChanged]
    protected void txtITEM_NARRATION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtITEM_NARRATION.Text;
        strTextBox = "txtITEM_NARRATION";
        csCalculations();
    }
    #endregion

    #region [txtQUANTAL_TextChanged]
    protected void txtQUANTAL_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQUANTAL.Text;
        strTextBox = "txtQUANTAL";
        csCalculations();
    }
    #endregion

    #region [txtPACKING_TextChanged]
    protected void txtPACKING_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPACKING.Text;
        strTextBox = "txtPACKING";
        csCalculations();
    }
    #endregion

    #region [txtBAGS_TextChanged]
    protected void txtBAGS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBAGS.Text;
        strTextBox = "txtBAGS";
        csCalculations();
    }
    #endregion

    #region [txtRATE_TextChanged]
    protected void txtRATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRATE.Text;
        strTextBox = "txtRATE";
        csCalculations();
    }
    #endregion

    #region [txtITEMAMOUNT_TextChanged]
    protected void txtITEMAMOUNT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtITEMAMOUNT.Text;
        strTextBox = "txtITEMAMOUNT";
        csCalculations();
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
                        pnlPopup.Style["display"] = "block";
                        txtSearchText.Text = string.Empty;
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Account Master Popup Button Code]
    protected void showPopupAccountMaster(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.myaccountmaster(qry);

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
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
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
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                setFocusControl(txtMILL_CODE);
            }
            if (hdnfClosePopup.Value == "txtBROKER")
            {
                setFocusControl(txtBROKER);
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
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Purchase", "javascript:pagevalidation();", true);

        }
        catch
        {

        }

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
            if (searchString != string.Empty)
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

            if (hdnfClosePopup.Value == "txtDOC_NO" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtdoc_no.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDOC_NO.Text = string.Empty;
                    txtDOC_NO.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtDOC_NO);
                    hdnfClosePopup.Value = "Close";
                }


                if (btntxtdoc_no.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,PartyShortname,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or PartyShortname like '%" + txtSearchText.Text + "%' or PartyCity like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }
            if (hdnfClosePopup.Value == "txtPURCNO")
            {
                lblPopupHead.Text = "--Select Purchase No--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                lblPopupHead.Text = "--Select Party Code--";
                if (txtAC_CODE.Text != string.Empty)
                {
                    split = txtAC_CODE.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);

                //string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                //    " and Locked=0 and " + name + " order by Ac_Name_E";
                //this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and (" + name + ") order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtUnit_Code")
            {
                if (txtAC_CODE.Text != string.Empty)
                {
                    string iscarporate = clsCommon.getString("select carporate_party from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + " and Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (iscarporate == "Y")
                    {
                        lblMsg.Text = "";
                        lblPopupHead.Text = "--Select Unit--";
                        if (txtUnit_Code.Text != string.Empty)
                        {
                            split = txtUnit_Code.Text.Split(delimiter);
                        }
                        foreach (var s in split)
                        {
                            string aa = s.ToString();
                            //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                            name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                        }
                        name = name.Remove(name.Length - 3);
                        //string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                        //    " and Locked=0 and " + name + " order by Ac_Name_E";
                        //this.showPopup(qry);

                        string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                 + " and (" + name + ") order by Ac_Name_E ";

                        this.showPopupAccountMaster(qry);
                    }
                    else
                    {
                        lblMsg.Text = "";
                        lblPopupHead.Text = "--Select Unit--";
                        if (txtUnit_Code.Text != string.Empty)
                        {
                            split = txtUnit_Code.Text.Split(delimiter);
                        }
                        foreach (var s in split)
                        {
                            string aa = s.ToString();
                            //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                            name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                        }
                        name = name.Remove(name.Length - 3);
                        //string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                        //    " and Locked=0 and " + name + " order by Ac_Name_E";
                        //this.showPopup(qry);
                        string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                 + " and (" + name + ") order by Ac_Name_E desc";

                        this.showPopupAccountMaster(qry);
                    }
                }
                else
                {
                    lblMsg.Text = "Please Enter Ac_Code First!";
                    setFocusControl(txtAC_CODE);
                }
            }
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                lblPopupHead.Text = "--Select Mill--";
                string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " + qryAccountList + ".Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and " + qryAccountList + ".Ac_type='M' " +
                    " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBROKER")
            {
                lblPopupHead.Text = "--Select Broker--";
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " + qryAccountList + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and Locked=0 and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               + " and (" + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtITEM_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                if (txtITEM_CODE.Text != string.Empty)
                {
                    split = txtITEM_CODE.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( System_Code like '%" + aa + "%' or System_Name_E like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                string qry = "select System_Code,System_Name_E as Item_Name,KgPerKatta from " + SystemMasterTable + " where System_Type='I' and " +
                    " " + name + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Code Like '%" + aa + "%'or Marka like '%" + aa + "%' or Wt_Per like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = " select Code,Marka,Wt_Per from Brand_Master where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + "and Mal_Code=" + txtITEM_CODE.Text + "and (" + name + ")";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGSTRateCode")
            {
                lblPopupHead.Text = "--Select Item--";
                if (txtITEM_CODE.Text != string.Empty)
                {
                    split = txtITEM_CODE.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Doc_no like '%" + aa + "%' or GST_Name like '%" + aa + "%' or Rate like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where " + name + "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Code AS Brand_code,English_Name as Brand_Name from Brand_Master where ( Code like '%" + txtSearchText.Text + "%' or English_Name like '%" + txtSearchText.Text + "%' ) and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                this.showPopup(qry);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtDOC_NO")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtDOC_NO.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtDOC_NO.Text != string.Empty)
                        {
                            txtValue = txtDOC_NO.Text;

                            string qry = "select * from " + tblHead + " where  doc_no='" + txtValue + "' " +
                                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Record Found
                                        hdnf.Value = dt.Rows[0]["doc_no"].ToString();
                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                txtDOC_NO.Enabled = false;
                                                hdnf.Value = txtDOC_NO.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                setFocusControl(txtPURCNO);
                                            }
                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    pnlgrdDetail.Enabled = true;
                                                    txtDOC_NO.Enabled = false;
                                                    setFocusControl(txtPURCNO);
                                                    hdnf.Value = txtDOC_NO.Text;
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtPURCNO);
                                            txtDOC_NO.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtDOC_NO.Text = string.Empty;
                                            setFocusControl(txtDOC_NO);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtDOC_NO);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtDOC_NO.Text = string.Empty;
                        setFocusControl(txtDOC_NO);
                    }
                }
                catch
                {

                }
                #endregion
            }
            if (strTextBox == "txtPURCNO")
            {
                // string millgrade;

                hdnfpurcid.Value = clsCommon.getString("select doid from nt_1_deliveryorder where doc_no=" + txtPURCNO.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");

                setFocusControl(txtDOC_DATE);
            }
            if (strTextBox == "txtBrand_Code")
            {
                string acname = "";
                if (txtBrand_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBrand_Code.Text);
                    if (a == false)
                    {
                        btntxtBrand_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = clsDAL.SimpleQuery("select Marka,Wt_Per from Brand_Master where Code=" + txtBrand_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                lblBrandname.Text = dt.Rows[0]["Marka"].ToString();
                                setFocusControl(txtQUANTAL);
                                // txtWtper.Text = dt.Rows[0]["Wt_Per"].ToString();
                            }
                            else
                            {
                                lblBrandname.Text = "";
                                setFocusControl(txtQUANTAL);

                            }
                        }
                        else
                        {
                            lblBrandname.Text = "";
                            setFocusControl(txtQUANTAL);
                        }


                    }
                }
                else
                {
                    setFocusControl(txtQUANTAL);
                }
            }
            if (strTextBox == "txtDOC_DATE")
            {
                try
                {
                    string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //DateTime startDate = DateTime.Parse(clsGV.Start_Date);
                    //TimeSpan oneDay = new TimeSpan(864000000000);
                    //DateTime dayBeforeStartDate = startDate.Subtract(oneDay);
                    //if (clsCommon.isValidDate(dt) == true || dt == "31/03/2017")
                    //{
                    //    setFocusControl(txtAC_CODE);
                        txtmill_inv_date.Text = txtDOC_DATE.Text;
                    //}
                    //else
                    //{
                    //    txtDOC_DATE.Text = "";
                    //    setFocusControl(txtDOC_DATE);
                    //}
                }
                catch
                {
                    txtDOC_DATE.Text = "";
                    setFocusControl(txtDOC_DATE);
                }
            }
            if (strTextBox == "txtAC_CODE")
            {
                string acname = "";
                if (txtAC_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAC_CODE.Text);
                    if (a == false)
                    {
                        btntxtAC_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfac.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (acname != string.Empty)
                        {

                            LblPartyname.Text = acname;
                            lblUnitName.Text = acname;
                            txtUnit_Code.Text = txtAC_CODE.Text;
                            setFocusControl(txtUnit_Code);

                            txtTO_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        }
                        else
                        {
                            txtAC_CODE.Text = string.Empty;
                            LblPartyname.Text = acname;
                            setFocusControl(txtAC_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAC_CODE);
                }
            }
            if (strTextBox == "txtUnit_Code")
            {
                string acname = "";
                if (txtUnit_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtUnit_Code.Text);
                    if (a == false)
                    {
                        btntxtUnitcode_Click(this, new EventArgs());
                    }
                    else
                    {
                        string iscarporate = clsCommon.getString("select carporate_party from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (iscarporate == "Y")
                        {
                            lblMsg.Text = "";
                            lblPopupHead.Text = "--Select Unit--";
                            //string qry = "select UnitName from " + tblPrefix + "qryPartyUnitlist where Ac_Code=" + txtAC_CODE.Text +
                            //    " and Unit_name=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                            acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfuc.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (acname != string.Empty)
                            {
                                lblUnitName.Text = acname;
                                setFocusControl(txtMILL_CODE);
                                //txtTo_Place.Text = clsCommon.getString("select CityName from " + tblPrefix + "qryAccountsList where Ac_Code=" + txtAC_CODE.Text);
                            }
                            else
                            {
                                txtUnit_Code.Text = string.Empty;
                                lblUnitName.Text = acname;
                                setFocusControl(txtUnit_Code);
                            }
                        }
                        else
                        {
                            acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfuc.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (acname != string.Empty)
                            {

                                lblUnitName.Text = acname;
                                setFocusControl(txtMILL_CODE);
                                // txtTO_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            }
                            else
                            {
                                txtUnit_Code.Text = string.Empty;
                                lblUnitName.Text = acname;
                                setFocusControl(txtUnit_Code);
                            }
                        }
                    }
                }
                else
                {
                    setFocusControl(txtUnit_Code);
                }
            }
            if (strTextBox == "txtMILL_CODE")
            {
                string millName = "";

                if (txtMILL_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMILL_CODE.Text);
                    if (a == false)
                    {
                        btntxtMILL_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        millName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type='M' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfmc.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (millName != string.Empty && millName != "0")
                        {
                            hdnfMillshort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + txtMILL_CODE.Text + " and company_code=" + Session["Company_Code"].ToString() + "");

                            LBLMILLNAME.Text = millName;
                            setFocusControl(txtFROM_STATION);
                            txtFROM_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        }
                        else
                        {
                            txtMILL_CODE.Text = string.Empty;
                            LBLMILLNAME.Text = millName;
                            setFocusControl(txtMILL_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtMILL_CODE);
                }
            }
            if (strTextBox == "txtFROM_STATION")
            {
                setFocusControl(txtTO_STATION);
            }
            if (strTextBox == "txtTO_STATION")
            {
                setFocusControl(txtLORRYNO);
            }
            if (strTextBox == "txtLORRYNO")
            {
                setFocusControl(txtWEARHOUSE);
            }
            if (strTextBox == "txtWEARHOUSE")
            {
                setFocusControl(txtBROKER);
            }
            if (strTextBox == "txtBROKER")
            {
                string brokername = "";
                if (txtBROKER.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBROKER.Text);
                    if (a == false)
                    {
                        btntxtBROKER_Click(this, new EventArgs());
                    }
                    else
                    {
                        brokername = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtBROKER.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfbk.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtBROKER.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (brokername != string.Empty)
                        {
                            hdnfBrokerShort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + txtBROKER.Text + " and company_code=" + Session["Company_Code"].ToString() + "");

                            LBLBROKERNAME.Text = brokername;
                            setFocusControl(txtGSTRateCode);
                        }
                        else
                        {
                            txtBROKER.Text = "0";
                            LBLBROKERNAME.Text = brokername;
                            setFocusControl(txtGSTRateCode);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBROKER);
                }
            }

            if (strTextBox == "txtGSTRateCode")
            {
                string gstname = "";
                if (txtGSTRateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGSTRateCode.Text);
                    if (a == false)
                    {
                        btntxtGSTRateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGSTRateCode.Text + " ");
                        if (gstname != string.Empty && gstname != "0")
                        {
                            lblGSTRateName.Text = gstname;
                            setFocusControl(txtbillNo);
                        }
                        else
                        {
                            txtGSTRateCode.Text = string.Empty;
                            lblGSTRateName.Text = gstname;
                            setFocusControl(txtGSTRateCode);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBROKER);
                }
            }
            if (strTextBox == "txtbillNo")
            {
                // setFocusControl(btnOpenDetailsPopup);
            }
            if (strTextBox == "txtCASH_ADVANCE")
            {
                setFocusControl(txtDUE_DAYS);
            }
            if (strTextBox == "txtBANK_COMMISSION")
            {
                setFocusControl(txtOTHER_AMT);
            }
            if (strTextBox == "txtLESS_FRT_RATE")
            {
                setFocusControl(txtBANK_COMMISSION);
            }
            //if (strTextBox == "txtFREIGHT")
            //{
            //    setFocusControl(txtDUE_DAYS);
            //}
            if (strTextBox == "txtOTHER_AMT")
            {
                double txtBILL_AMOUNT_Convert = txtBILL_AMOUNT.Text != string.Empty ? Convert.ToDouble(txtBILL_AMOUNT.Text) : 0.00;
                double txtOTHER_AMT_Convert = txtOTHER_AMT.Text != string.Empty ? Convert.ToDouble(txtOTHER_AMT.Text) : 0.00;

                txtBILL_AMOUNT_Convert = (txtBILL_AMOUNT_Convert) + (txtOTHER_AMT_Convert);
                txtBILL_AMOUNT.Text = Convert.ToString(txtBILL_AMOUNT_Convert);

                setFocusControl(txtCASH_ADVANCE);
            }
            if (strTextBox == "txtBILL_AMOUNT")
            {
                setFocusControl(btnSave);
            }
            if (strTextBox == "txtDUE_DAYS")
            {
                setFocusControl(txtBILL_AMOUNT);
            }
            //if (strTextBox == "txtOTHER_AMT")
            //{
            //    setFocusControl(txtBILL_AMOUNT);
            //}
            if (strTextBox == "txtITEM_CODE")
            {
                string itemname = "";
                string itepacking = "";
                if (txtITEM_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtITEM_CODE.Text);
                    if (a == false)
                    {
                        btntxtITEM_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        itepacking = clsCommon.getString("select KgPerKatta from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (itemname != string.Empty || itepacking != string.Empty)
                        {

                            if (itepacking == string.Empty)
                            {
                                txtPACKING.Text = "50";
                            }
                            else
                            {
                                txtPACKING.Text = itepacking;
                            }
                            LBLITEMNAME.Text = itemname;
                            setFocusControl(txtBrand_Code);

                        }
                        else
                        {
                            txtPACKING.Text = "50";
                            txtITEM_CODE.Text = string.Empty;
                            LBLITEMNAME.Text = itemname;
                            setFocusControl(txtITEM_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtITEM_CODE);
                }
            }
            if (strTextBox == "txtITEM_NARRATION")
            {
                setFocusControl(btnAdddetails);
            }
            if (strTextBox == "txtQUANTAL")
            {
                setFocusControl(txtRATE);
            }
            if (strTextBox == "txtPACKING")
            {
                setFocusControl(txtRATE);
            }

            if (strTextBox == "txtRATE")
            {
                setFocusControl(txtITEMAMOUNT);
            }
            if (strTextBox == "txtITEMAMOUNT")
            {
                setFocusControl(txtITEM_NARRATION);
            }
            if (strTextBox == "txtCGSTRate")
            {
                this.CSTCalculation();
                setFocusControl(txtSGSTRate);
            }

            if (strTextBox == "txtIGSTRate")
            {
                this.LIGSTCalculation();
                setFocusControl(txtLESS_FRT_RATE);
            }

            if (strTextBox == "txtSGSTRate")
            {
                this.LSGSTCalculation();
                setFocusControl(txtIGSTRate);
            }
            if (strTextBox == "txtTCSRate")
            {
                setFocusControl(txtTCSNet_Payable);
            }
            if (strTextBox == "txtTCSNet_Payable")
            {
                setFocusControl(btnSave);
            }

            #region calculation part
            double qtl = Convert.ToDouble(txtQUANTAL.Text != string.Empty ? txtQUANTAL.Text : "0") + 0;
            double packing = Convert.ToDouble(txtPACKING.Text != string.Empty ? txtPACKING.Text : "0") + 0;
            Int32 bags = 0;

            double lessfreight = Convert.ToDouble(txtLESS_FRT_RATE.Text != string.Empty ? txtLESS_FRT_RATE.Text : "0") + 0;
            double freight = 0.00;

            double netQntl = 0.00;
            double subtotal = 0.00;
            double cashAdv = Convert.ToDouble(txtCASH_ADVANCE.Text != string.Empty ? txtCASH_ADVANCE.Text : "0") + 0;
            double bankComm = Convert.ToDouble(txtBANK_COMMISSION.Text != string.Empty ? txtBANK_COMMISSION.Text : "0");
            double other = Convert.ToDouble(txtOTHER_AMT.Text != string.Empty ? txtOTHER_AMT.Text : "0") + 0;
            double billAmt = 0.00;

            double item_Amount = 0.00;
            double rate = Convert.ToDouble(txtRATE.Text != string.Empty ? txtRATE.Text : "0");
            double RCST = Convert.ToDouble(txtCGSTAmount.Text != string.Empty ? txtCGSTAmount.Text : "0");
            double RIGST = Convert.ToDouble(txtIGSTAmount.Text != string.Empty ? txtIGSTAmount.Text : "0");
            double RSGST = Convert.ToDouble(txtSGSTAmount.Text != string.Empty ? txtSGSTAmount.Text : "0");

            if (qtl != 0 && packing != 0)
            {
                bags = Convert.ToInt32((qtl / packing) * 100);
                txtBAGS.Text = bags.ToString();
            }
            else
            {
                txtBAGS.Text = bags.ToString();
            }
            item_Amount = Math.Round((qtl * rate), 2);
            txtITEMAMOUNT.Text = item_Amount.ToString();

            #region calculate subtotal
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[14].Text != "D")
                    {
                        double item_Amt = Convert.ToDouble(grdDetail.Rows[i].Cells[12].Text.Trim());
                        subtotal = subtotal + item_Amt;
                    }
                }
                txtSUBTOTAL.Text = subtotal.ToString();
            }
            #endregion

            #region calculate net Quantal
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[14].Text != "D")
                    {
                        double qntl = Convert.ToDouble(grdDetail.Rows[i].Cells[8].Text.Trim());
                        netQntl = netQntl + qntl;
                    }
                }
                txtNETQNTL.Text = netQntl.ToString();
            }
            #endregion
            string aaa = "";
            if (txtAC_CODE.Text.Trim() != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAC_CODE.Text);
                if (a == true)
                {
                    aaa = clsCommon.getString("select isnull(GSTStateCode,0) from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAC_CODE.Text + "");
                }
            }
            int partygstStateCode = 0;
            if (aaa.Trim().ToString() != "")
            {
                partygstStateCode = Convert.ToInt32(aaa);
            }

            if (Session["CompanyGSTStateCode"] == null || Session["CompanyGSTStateCode"] == string.Empty)
            {
                Session["CompanyGSTStateCode"] = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                    "  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            }
            int companyGstStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
            string GSTRateCode = txtGSTRateCode.Text;
            double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + ""));
            double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + ""));

            double CGSTAmountForPS = 0.0;
            double SGSTAmountForPS = 0.0;
            double IGSTAmountForPS = 0.0;

            double CGSTRateForPS = 0.00;
            double SGSTRateForPS = 0.00;
            double IGSTRateForPS = 0.00;
            double billAmount = 0.00;

            if (companyGstStateCode == partygstStateCode)
            {
                CGSTRateForPS = cgstrate;
                double millamount = subtotal;
                double cgsttaxAmountOnMR = Math.Round((millamount * cgstrate / 100), 2);
                //double cgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + cgsttaxAmountOnMR) * mill_rate)), 2);
                //double cgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - cgstExMillRate), 2);
                CGSTAmountForPS = Math.Round(cgsttaxAmountOnMR, 2);

                SGSTRateForPS = sgstrate;
                double sgsttaxAmountOnMR = Math.Round((millamount * sgstrate / 100), 2);
                //double sgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + sgsttaxAmountOnMR) * mill_rate)), 2);
                //double sgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - sgstExMillRate), 2);
                SGSTAmountForPS = Math.Round(sgsttaxAmountOnMR, 2);
            }
            else
            {
                IGSTRateForPS = igstrate;
                double igsttaxAmountOnMR = ((subtotal) * igstrate / 100);
                //double igstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + igsttaxAmountOnMR) * mill_rate)), 2);
                //double igstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - igstExMillRate), 2);
                IGSTAmountForPS = Math.Round(igsttaxAmountOnMR, 2);
            }

            txtCGSTRate.Text = CGSTRateForPS.ToString();
            txtCGSTAmount.Text = CGSTAmountForPS.ToString();
            txtSGSTRate.Text = SGSTRateForPS.ToString();
            txtSGSTAmount.Text = SGSTAmountForPS.ToString();
            txtIGSTRate.Text = IGSTRateForPS.ToString();
            txtIGSTAmount.Text = IGSTAmountForPS.ToString();

            freight = Math.Round((lessfreight * netQntl), 2);
            txtFREIGHT.Text = freight.ToString();

            billAmount = Math.Round(((subtotal + bankComm + cashAdv + other + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS) - freight), 2);
            billAmt = Math.Round(billAmount);
            txtBILL_AMOUNT.Text = billAmt.ToString();

            if (btnSave.Text == "Save")
            {
                AmtCalculation();
            }

            #region TDS Calculation
            double TDS_Rate = 0.000;
            double TDS_Amt = 0.00;
            double TDS_Amount = 0.00;
            double Bill_Amount = 0.00;
            //TDS_Rate = Convert.ToDouble(txtTDS.Text);
            ////TDS_Rate = txtTDS.Text != string.Empty ? Convert.ToDouble(txtTDS.Text) : 0.00;
            ////Bill_Amount = Convert.ToDouble(txtBILL_AMOUNT.Text);
            ////TDS_Amt = Math.Round(((subtotal * TDS_Rate) / 100), 2);
            ////txtTDSAmt.Text = TDS_Amt.ToString();

            if (txtTDSAmt.Text == string.Empty || txtTDSAmt.Text == "0")
            {
                txtTDSAmt.Text = "0";
            }
            else
            {
                TDS_Amt = Convert.ToDouble(txtTDSAmt.Text);
            }
            TDS_Rate = Convert.ToDouble(txtTDS.Text);
           
            if (TDS_Amt == 0)
            {
                TDS_Amt = Math.Round(((subtotal * TDS_Rate) / 100), 2);
            }
            TDS_Amount = Math.Round(TDS_Amt);
            txtTDSAmt.Text = TDS_Amount.ToString();
            #endregion



            #region TCS Calculation
            double TCS_Rate = 0.000;
            double TCS_Amt = 0.00;
            double Bill_Amt = 0.00;
            double Net_Payable_Amt = 0.00;
            double TCS_Amount = 0.00;
            double Net_Payable_Amtount = 0.00;
            if (txtTCSAmt.Text == string.Empty || txtTCSAmt.Text == "0")
            {
                txtTCSAmt.Text = "0";
            }
            else
            {
                TCS_Amt = Convert.ToDouble(txtTCSAmt.Text);
            }
            TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
            Bill_Amt = Convert.ToDouble(txtBILL_AMOUNT.Text);
            if (TCS_Amt == 0)
            {
                TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
            }

            Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt - TDS_Amt), 2);

            TCS_Amount = Math.Round(TCS_Amt);
            txtTCSAmt.Text = TCS_Amount.ToString();

            Net_Payable_Amtount = Math.Round(Net_Payable_Amt);
            txtTCSNet_Payable.Text = Net_Payable_Amtount.ToString();
            #endregion
            //freight = Math.Round((lessfreight * netQntl), 2);
            //txtFREIGHT.Text = freight.ToString();
            //billAmt = (subtotal + bankComm + cashAdv + other + RCST + RIGST + RSGST) - freight;
            //txtBILL_AMOUNT.Text = billAmt.ToString();

            #endregion
        }
        catch
        {
        }
    }
    #endregion

    #region[CSTCalculation]
    private void CSTCalculation()
    {
        double Totalamt = txtSUBTOTAL.Text != string.Empty ? Convert.ToDouble(txtSUBTOTAL.Text) : 0.00;
        double CSTtax = txtCGSTRate.Text != string.Empty ? Convert.ToDouble(txtCGSTRate.Text) : 0.00;
        double result = 0.00;

        CSTtax = CSTtax / 100;
        result = Totalamt * CSTtax;
        txtCGSTAmount.Text = result.ToString();

    }

    #endregion

    #region[LSGSTCalculation]
    private void LSGSTCalculation()
    {
        double Totalamt = txtSUBTOTAL.Text != string.Empty ? Convert.ToDouble(txtSUBTOTAL.Text) : 0;
        double LSTtax = txtSGSTRate.Text != string.Empty ? Convert.ToDouble(txtSGSTRate.Text) : 0;
        double result = 0;

        LSTtax = LSTtax / 100;
        result = Totalamt * LSTtax;
        txtSGSTAmount.Text = result.ToString();

    }

    #endregion

    #region[LIGSTCalculation]
    private void LIGSTCalculation()
    {
        double Totalamt = txtSUBTOTAL.Text != string.Empty ? Convert.ToDouble(txtSUBTOTAL.Text) : 0;
        double LITtax = txtIGSTRate.Text != string.Empty ? Convert.ToDouble(txtIGSTRate.Text) : 0;
        double result = 0;

        LITtax = LITtax / 100;
        result = Totalamt * LITtax;
        txtIGSTAmount.Text = result.ToString();

    }

    #endregion


    protected void txtbillNo_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtbillNo.Text;
        //strTextBox = "txtbillNo";
        //csCalculations();
    }
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    protected void txtUnit_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtUnit_Code.Text;
        strTextBox = "txtUnit_Code";
        csCalculations();
    }
    protected void btntxtUnitcode_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtUnit_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtCGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGSTRate.Text;
        strTextBox = "txtCGSTRate";
        csCalculations();

    }
    protected void txtSGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGSTRate.Text;
        strTextBox = "txtSGSTRate";
        csCalculations();

    }
    protected void txtIGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGSTRate.Text;
        strTextBox = "txtIGSTRate";
        csCalculations();

    }
    protected void txtGSTRateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGSTRateCode.Text;
        strTextBox = "txtGSTRateCode";
        csCalculations();

    }
    protected void btntxtGSTRateCode_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGSTRateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtEWayBillNo_TextChanged(object sender, EventArgs e)
    {

    }
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "'"));
            if (counts == 0)
            {
                txtDOC_NO.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                txtDOC_NO.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(purchaseid),0) as purchaseid from " + tblHead + " "));
            if (counts == 0)
            {
                lblPurchase_Id.Text = "1";
                purchase_id = 1;
            }
            else
            {
                purchase_id = Convert.ToInt32(clsCommon.getString("SELECT max(purchaseid) as purchaseid from " + tblHead)) + 1;
                lblPurchase_Id.Text = purchase_id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
    protected void txtmill_inv_date_TextChanged(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string NewInsert(string PurchaseInsertUpdate, string PurchaseDetail_Insert, string PurchaseDetail_Update, string PurchaseDetail_Delete, string Gledger_Delete, string Gledger_Insert, string status)
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
            cmd.Parameters.AddWithValue("QryInsertAndUpdate", PurchaseInsertUpdate);
            cmd.Parameters["QryInsertAndUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Insert", PurchaseDetail_Insert);
            cmd.Parameters["QryDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Update", PurchaseDetail_Update);
            cmd.Parameters["QryDetail_Update"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Delete", PurchaseDetail_Delete);
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
    protected void txtTCSNet_Payable_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSNet_Payable.Text;
        strTextBox = "txtTCSNet_Payable";
        csCalculations();
    }
    protected void txtTCSRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSRate.Text;
        strTextBox = "txtTCSRate";

        txtTDS.Text = "0.00";
        txtTDSAmt.Text = "0.00";
        csCalculations();
    }
    protected void txtTCSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSAmt.Text;
        strTextBox = "txtTCSAmt";
        csCalculations();
    }

    protected void txtTDS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS.Text;
        strTextBox = "txtTDS";

        txtTCSRate.Text = "0.00";
        txtTCSAmt.Text = "0.00";
        csCalculations();
    }
    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAmt.Text;
        strTextBox = "txtTDSAmt";
        csCalculations();
    }
    #region [txtBrand_Code_TextChanged]
    protected void txtBrand_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBrand_Code.Text;
        strTextBox = "txtBrand_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtBrand_Code_Click]
    protected void btntxtBrand_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBrand_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    private void AmtCalculation()
    {
        string Amt = string.Empty;
        double Amount = 0.00;
        double Amountf = 0.00;
        double PSBalAmt = 0.00;
        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
        PSBalAmt = Convert.ToDouble("0" + txtSUBTOTAL.Text);
        //Amt = clsCommon.getString("select isnull(sum(AMOUNT),0) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + txtAC_CODE.Text
        //             + " and DRCR='D' and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        DataTable dtpan = new DataTable();
        DataSet dspan = new DataSet();
        #region[balancelimit]
        string panno = clsCommon.getString("select CompanyPan from qrymstaccountmaster where Ac_Code=" + txtAC_CODE.Text
           + " and Company_Code=" + Session["Company_Code"].ToString() + "");

        dspan = clsDAL.SimpleQuery("select Ac_Code from qrymstaccountmaster where CompanyPan='" + panno
           + "' and Company_Code=" + Session["Company_Code"].ToString() + "");
        dtpan = dspan.Tables[0];

        //PurchaseTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtAC_CODE
        //   + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        ////double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
        double Amt2 = 0.00;
        double sbamt2 = 0.00;
        if (panno != string.Empty)
        {
            if (dtpan.Rows.Count > 0)
            {
                for (int i = 0; i < dtpan.Rows.Count; i++)
                {
                    double Amt1 = 0.00;
                    //int accode = 0;
                    // accode=dtpan.Rows
                    //Amt1 = convert.todouble(clscommon.getstring("select sum(amount) as amount from nt_1_gledger where  ac_code=" + dtpan.rows[0]["ac_code"].tostring()
                    // + " and drcr='c' and  tran_type in('ps','pr','rp') and year_code='" + convert.toint32(session["year"].tostring()) +
                    // "' and company_code=" + convert.toint32(session["company_code"].tostring()));
                    //Amt1 = Convert.ToDouble("select sum(isnull(AMOUNT,0)) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpan.Rows[i]["Ac_Code"].ToString()
                    //+ " and DRCR='C' and TRAN_TYPE in('PS','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
                    // "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    Amt1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(AMOUNT),0) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpan.Rows[i]["Ac_Code"].ToString()
                   + " and DRCR='C' and TRAN_TYPE in('PS','PR','RP') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                    Amt2 += Amt1;
                }

                Amt = Amt2.ToString();
            }
        #endregion
        }
        if (Amt == string.Empty || Amt == "")
        {
            Amt = "0.00";
        }
        Amountf = Convert.ToDouble(Amt);
        if (btnSave.Text == "Save")
        {
            Amount = Amountf + PSBalAmt;
        }
        else
        {
            Amount = Amountf;
        }
        if (Amount > balancelimit)
        {
            txtTDS.Text = Session["SaleTDSRate"].ToString();
            txtTCSRate.Text = "0.00";
        }
        else
        {
            txtTDS.Text = "0.00";
            txtTCSRate.Text = "0.00";
        }
    }
    protected void drpreatilstock_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}