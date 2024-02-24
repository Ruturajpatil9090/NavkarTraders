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

public partial class Sugar_pgrSugarsaleForGSTxml : System.Web.UI.Page
{

    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string SystemMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string qryHead = string.Empty;
    string qryDetail = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string trntype = "SB";
    string user = string.Empty;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    int Doc_No = 0;
    int Sale_id = 0;

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    string cs = string.Empty;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "SugarSale";
            tblDetails = tblPrefix + "sugarsaleDetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "qrysaleheaddetail";
            qryHead = "qrysalehead";
            qryDetail = "qrysaledetail";
            qryAccountList = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
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

                        hdnf.Value = Request.QueryString["saleid"];
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
                        txtGStno.Text = Session["Company_GST"].ToString();
                        txtGSTRateCode.Text = "1";
                        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        lblGSTRateName.Text = gstname;
                        setFocusControl(txtAC_CODE);
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
                int do_id = Convert.ToInt32(clsCommon.getString("select saleid from " + tblHead + " where doc_no=" + txtEditDoc_No.Text +
                    " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                    " " + Session["year"].ToString() + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SBPen('" + do_id + "')", true);

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
                pnlPopup.Style["display"] = "none";

                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                #region logic
                LblPartyname.Text = "";
                LBLMILLNAME.Text = "";
                LBLBROKERNAME.Text = "";
                btntxtAC_CODE.Enabled = false;
                btntxtBill_To.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtBROKER.Enabled = false;
                btntxtdoc_no.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btntxtPURCNO.Enabled = false;
                btnTransport.Enabled = false;
                calenderExtenderDate.Enabled = false;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                btntxtUnitcode.Enabled = false;
                btnPrintSaleBill.Enabled = true;
                lblUnitName.Text = "";
                lblchkEWayBill.Text = "";
                chkEWayBill.Enabled = false;

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
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;

                drpInsured.Enabled = false;
                CalendarExtender4.Enabled = false;

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
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = false;
                txtDOC_NO.Enabled = false;
                txtEditDoc_No.Enabled = false;
                #region set Business logic for save
                LblPartyname.Text = "";
                LBLMILLNAME.Text = "";
                LBLBROKERNAME.Text = "";
                btntxtAC_CODE.Enabled = true;
                btntxtBill_To.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtPURCNO.Enabled = true;
                lblTransportName.Text = "";
                lblGSTRateName.Text = "";
                btntxtBROKER.Enabled = true;
                btnTransport.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtEwayBill_ValidDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtnewsbdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                btntxtUnitcode.Enabled = true;
                lblUnitName.Text = "";
                lblchkEWayBill.Text = string.Empty;
                chkEWayBill.Enabled = true;
                chkEWayBill.Checked = false;
                lblchkEWayBill.Text = "";


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

                txtTCSRate.Text = Session["TCSRate"].ToString();
                txtTDS.Text = Session["SaleTDSRate"].ToString();
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                CalendarExtender4.Enabled = true;
                drpInsured.Enabled = true;

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
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                txtEditDoc_No.Enabled = true;

                pnlgrdDetail.Enabled = false;
                btnPrintSaleBill.Enabled = true;
                btntxtUnitcode.Enabled = false;
                #region logic
                btntxtAC_CODE.Enabled = false;
                btntxtBill_To.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtBROKER.Enabled = false;
                btntxtPURCNO.Enabled = false;
                calenderExtenderDate.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btnTransport.Enabled = false;
                chkEWayBill.Enabled = false;

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
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                CalendarExtender4.Enabled = false;
                drpInsured.Enabled = false;
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
                txtDOC_NO.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;

                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                btntxtUnitcode.Enabled = true;
                chkEWayBill.Enabled = true;
                //if (lblDONo.Text != "0")
                //{
                //    txtEway_Bill_No.Enabled = false;
                //}
                #region logic
                btntxtAC_CODE.Enabled = true;
                btntxtBill_To.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtBROKER.Enabled = true;
                calenderExtenderDate.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtPURCNO.Enabled = true;
                btnTransport.Enabled = true;

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
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                CalendarExtender4.Enabled = true;
                drpInsured.Enabled = true;
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

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) ;
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
                    " where saleid>" + Convert.ToInt32(hdnf.Value) +
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where saleid<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) 
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
            query = "select saleid from " + tblHead + " where saleid=(select MIN(saleid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
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
                string query = "SELECT top 1 [saleid] from " + tblHead + " where saleid<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY saleid DESC  ";
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
                string query = "SELECT top 1 [saleid] from " + tblHead + " where saleid>" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY saleid asc  ";
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
            query = "select saleid from " + tblHead + " where saleid=(select MAX(saleid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.getMaxCode();
        txtDOC_NO.Enabled = false;
        // pnlPopupDetails.Style["display"] = "none";
        txtGSTRateCode.Text = "1";
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        lblGSTRateName.Text = gstname;
        setFocusControl(txtPURCNO);
        lblDONo.Text = "0";
        txtGStno.Text = Session["Company_GST"].ToString();
        drpInsured.SelectedValue = "N";

        this.NextNumber();
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
        //if (lblDONo.Text != string.Empty && lblDONo.Text != "0")
        //{
        //    grdDetail.Enabled = false;
        //    btnAdddetails.Enabled = false;
        //}
        btnGenEinvoice.Enabled = false;
        btnGentare_EWayBill.Enabled = false;
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

        //int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(saleid),0) as saleid from nt_1_sugarsale where  Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));


        //hdnf.Value = maxno.ToString();
        //clsButtonNavigation.enableDisable("S");
        //this.makeEmptyForm("S");
        //qry = getDisplayQuery();
        //this.fetchRecord(qry);

        hdnf.Value = Request.QueryString["saleid"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(saleid),0) as saleid from nt_1_sugarsale "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgrSugarsaleForGSTxml.aspx?saleid=" + hdnf.Value + "&Action=" + 1);
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
                        hdnf.Value = dt.Rows[0]["saleid"].ToString();
                        hdnfsaledoc.Value = txtDOC_NO.Text;
                        lblSale_Id.Text = hdnf.Value;
                        hdnfsaleid.Value = lblSale_Id.Text;
                        txtPURCNO.Text = dt.Rows[0]["PURCNO"].ToString();
                        lblDONo.Text = dt.Rows[0]["DO_No"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["AC_CODE"].ToString();
                        LblPartyname.Text = dt.Rows[0]["billtoname"].ToString();
                        txtBill_To.Text = dt.Rows[0]["Bill_To"].ToString();
                        lblBill_To.Text = dt.Rows[0]["CarporateBillTo_Name"].ToString();
                        txtUnit_Code.Text = dt.Rows[0]["Unit_Code"].ToString();
                        lblUnitName.Text = dt.Rows[0]["shiptoname"].ToString();
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
                        txtBankCommRate.Text = dt.Rows[0]["RateDiff"].ToString();
                        txtBANK_COMMISSION.Text = dt.Rows[0]["BANK_COMMISSION"].ToString();
                        txtOTHER_AMT.Text = dt.Rows[0]["OTHER_AMT"].ToString();
                        txtBILL_AMOUNT.Text = dt.Rows[0]["BILL_AMOUNT"].ToString();
                        txtASNGRNNo.Text = dt.Rows[0]["ASN_No"].ToString();
                        txtDUE_DAYS.Text = dt.Rows[0]["DUE_DAYS"].ToString();
                        txtNETQNTL.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtTransportCode.Text = dt.Rows[0]["Transport_Code"].ToString();
                        lblTransportName.Text = dt.Rows[0]["transportname"].ToString();
                        txtGSTRateCode.Text = dt.Rows[0]["GstRateCode"].ToString();
                        lblGSTRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtCGSTRate.Text = dt.Rows[0]["CGSTRate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["SGSTRate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["IGSTRate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtTaxableAmount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtEway_Bill_No.Text = dt.Rows[0]["EWay_Bill_No"].ToString();
                        txtMillInvoiceno.Text = dt.Rows[0]["MillInvoiceNo"].ToString();
                        txtRoundOff.Text = dt.Rows[0]["RoundOff"].ToString();
                        txtEwayBill_ValidDate.Text = dt.Rows[0]["EwayBillValidDateConverted"].ToString();

                        txtPartyMobno.Text = clsCommon.getString("select Mobile_No from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        txtTransportMobno.Text = clsCommon.getString("select Mobile_No from " + qryAccountList + " where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                        txtDriverMobno.Text = clsCommon.getString("select driver_no from " + tblPrefix + "deliveryorder where doc_no=" + lblDONo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

                        string unit_code = clsCommon.getString("select voucher_by from " + tblPrefix + "deliveryorder where doc_no=" + lblDONo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        txtunit.Text = clsCommon.getString("select Mobile_No from " + qryAccountList + " where Ac_Code=" + unit_code + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        hdnfac.Value = dt.Rows[0]["ac"].ToString();
                        hdnfuc.Value = dt.Rows[0]["uc"].ToString();
                        hdnfmc.Value = dt.Rows[0]["mc"].ToString();
                        hdnfbk.Value = dt.Rows[0]["bk"].ToString();
                        hdnfpurcid.Value = dt.Rows[0]["Purcid"].ToString();
                        hdnfAcShort.Value = dt.Rows[0]["billtoShortname"].ToString();
                        hdnfMillShort.Value = dt.Rows[0]["millshortname"].ToString();
                        hdnnfTransportshort.Value = dt.Rows[0]["transportShort_Name"].ToString();
                        hdnfUnitshort.Value = dt.Rows[0]["shiptocityname"].ToString();
                        hdnfbt.Value = dt.Rows[0]["bt"].ToString();
                        string ischecked = dt.Rows[0]["EWayBill_Chk"].ToString();
                        string IsDeleted = dt.Rows[0]["IsDeleted"].ToString();
                        // chkEWayBill.Checked = dt.Rows[0]["EWayBill_Chk"].ToString();

                        txtTDS.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        if (lblDONo.Text != string.Empty && lblDONo.Text != "0")
                        {

                            string salebillto = clsCommon.getString("select salebillto from nt_1_deliveryorder where doc_no=" + lblDONo.Text +
           " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

                            if (salebillto == Session["SELF_AC"].ToString())
                            {

                                btnEdit.Enabled = true;
                                btnDelete.Enabled = true;
                                pnlgrdDetail.Enabled = true;
                            }
                            else
                            {
                                btnEdit.Enabled = true;
                                btnDelete.Enabled = false;
                                pnlgrdDetail.Enabled = false;
                            }

                            btnEdit.Enabled = true;
                            btnDelete.Enabled = false;
                            pnlgrdDetail.Enabled = false;
                        }
                        if (IsDeleted == "0")
                        {
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            pnlgrdDetail.Enabled = false;
                            btnGenEinvoice.Enabled = false;
                            btnGentare_EWayBill.Enabled = false;
                            btnCancleEinvoice.Enabled = false;
                        }
                        if (ischecked == "Y")
                        {
                            chkEWayBill.Checked = true;
                            lblchkEWayBill.Text = LBLMILLNAME.Text;
                            string gstno = clsCommon.getString("select Gst_No from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " " +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            txtGStno.Text = gstno;
                        }
                        else
                        {
                            chkEWayBill.Checked = false;
                            txtGStno.Text = Session["Company_GST"].ToString();
                            lblchkEWayBill.Text = "";
                        }

                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();

                        txtnewsbno.Text = dt.Rows[0]["newsbno"].ToString();
                        txtnewsbdate.Text = dt.Rows[0]["newsbdate"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        txtsbnarration.Text = dt.Rows[0]["SBNARRATION"].ToString();
                        drpInsured.SelectedValue = dt.Rows[0]["Insured"].ToString();

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

                        string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                        if (User_Type == "A")
                        {
                            if (txteinvoiceno.Text != string.Empty && txtackno.Text != string.Empty)
                            {
                                btnCancleEinvoice.Enabled = true;
                            }
                            else
                            {
                                btnCancleEinvoice.Enabled = false;
                            }
                        }
                        else
                        {
                            btnCancleEinvoice.Enabled = false;
                        }

                        recordExist = true;
                        lblMsg.Text = "";

                        #region  Details
                        qry = "select detail_id as ID,item_code,itemname as item_Name,Brand_Code,Brand_Name,narration,Quantal,packing,bags,rate,item_Amount,saledetailid,'' as rowAction,'' as SrNo,ic from " + qryDetail + " where saleid=" + hdnf.Value;
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
                        hdnfSaleAc.Value = clsCommon.getString("select Sale_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        hdnfSaleAcid.Value = clsCommon.getString("select SaleAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        pnlgrdDetail.Enabled = false;
                    }
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
                    }
                }

            }
            // txtAC_CODE_TextChanged(this, new EventArgs());
            //txtTransportCode_TextChanged(this, new EventArgs());


            // csCalculations();
            //txtAC_CODE_TextChanged(this, new EventArgs());

            return recordExist;

        }
        catch
        {
            return false;
        }
        //csCalculations();
    }
    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and Tran_Type='" + trntype + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            string qryDisplay = "select * from " + qryHead + " where saleid=" + hdnf.Value + " ";
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
                //txtdoc_no.Text = hdnf.Value;
                hdnfSuffix.Value = "";
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
    //protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    //{
    //    btnAdddetails.Text = "ADD";
    //    pnlPopupDetails.Style["display"] = "block";
    //    txtPACKING.Text = "50";
    //    txtITEM_CODE.Text = "";
    //    txtQUANTAL.Text = "";
    //    txtBAGS.Text = "";
    //    txtRATE.Text = "";
    //    txtITEMAMOUNT.Text = "";
    //    txtITEM_NARRATION.Text = "";
    //    LBLITEMNAME.Text = "";
    //    setFocusControl(txtITEM_CODE);
    //}
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
                        //rowIndex = dt.Rows.Count + 1;
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
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + txtDOC_NO.Text + " and Detail_ID=" + lblID.Text + "  And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        if (id != string.Empty && id != "0")
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
                    dt.Columns.Add((new DataColumn("packing", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("bags", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("saledetailid", typeof(int))));
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
                dt.Columns.Add((new DataColumn("packing", typeof(Int32))));
                dt.Columns.Add((new DataColumn("bags", typeof(Int32))));
                dt.Columns.Add((new DataColumn("rate", typeof(double))));
                dt.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("saledetailid", typeof(int))));
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
            dr["ic"] = clsCommon.getString("select systemid from qrymstitem where system_code=" + txtITEM_CODE.Text + " and company_code=" + Session["company_code"].ToString() + "");
            dr["narration"] = Server.HtmlDecode(txtITEM_NARRATION.Text);
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
                dr["saledetailid"] = 0;
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
                //pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtITEM_CODE);
            }
            else
            {
                //pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                //btnOpenDetailsPopup.Focus();
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
            hdnfSaleAc.Value = clsCommon.getString("select Sale_Ac from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
            hdnfSaleAcid.Value = clsCommon.getString("select SaleAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");

            txtITEM_CODE.Text = "";
            LBLITEMNAME.Text = "";
            txtITEM_NARRATION.Text = "";
            txtQUANTAL.Text = "";
            txtRATE.Text = "";
            txtITEMAMOUNT.Text = "";
            txtPACKING.Text = "50";
            txtBrand_Code.Text = string.Empty;
            lblBrandname.Text = string.Empty;
            txtBAGS.Text = "";
            btnAdddetails.Text = "ADD";

            csCalculations();
            btnAdddetails.Enabled = true;
            grdDetail.Enabled = true;
            setFocusControl(txtITEM_CODE);
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
        btntxtITEM_CODE.Text = string.Empty;
        txtQUANTAL.Text = string.Empty;
        txtPACKING.Text = string.Empty;
        txtBAGS.Text = string.Empty;
        txtRATE.Text = string.Empty;
        txtITEMAMOUNT.Text = string.Empty;
        txtITEM_NARRATION.Text = string.Empty;
        btnAdddetails.Text = string.Empty;
        btnClosedetails.Text = string.Empty;
        btnAdddetails.Text = "ADD";

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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["saledetailid"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where saledetailid=" + ID + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (IDExisting != string.Empty && IDExisting != "0")
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtPURCNO")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("100px");
                e.Row.Cells[2].Width = new Unit("300px");
                e.Row.Cells[3].Width = new Unit("300px");
                e.Row.Cells[4].Width = new Unit("120px");
                e.Row.Cells[5].Width = new Unit("120px");

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }

            if (v == "txtGSTRateCode")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("300px");
                e.Row.Cells[2].Width = new Unit("100px");

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }



            if (v == "txtAC_CODE" || v == "txtUnit_Code" || v == "txtMILL_CODE" || v == "txtBROKER" || v == "txtTransportCode")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("450px");
                e.Row.Cells[2].Width = new Unit("150px");

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }
            if (v == "txtITEM_CODE")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("100px");


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

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
                        if (grdDetail.Rows[rowindex].Cells[14].Text != "D" && grdDetail.Rows[rowindex].Cells[14].Text != "R")
                        {
                            //pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
                            setFocusControl(txtITEM_CODE);
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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            // {
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("250px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("40px");
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

    #region [txtrondoff_TextChanged]
    protected void txtRoundOff_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRoundOff.Text;
        strTextBox = "txtRoundOff";
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
            setFocusControl(btnSave);
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Purchase", "javascript:pagevalidation();", true);

    }

    //}
    //#endregion



    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
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
                    string qry = "select doc_no,doc_date,PartyName,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or PartyName like '%" + txtSearchText.Text + "%' or PartyCity like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }
            if (hdnfClosePopup.Value == "txtPURCNO")
            {
                lblPopupHead.Text = "--Select Purchase No--";
                //string qry = "Select s.doc_no as PurcNo,a.Ac_Name_E as mill,b.Ac_Name_E as broker,Convert(varchar(10),s.doc_date,103) as doc_date,s.NETQNTL from " + tblPrefix + "SugarPurchase s" +
                //            " left outer join " + tblPrefix + "AccountMaster a on a.Ac_Code=s.mill_code and a.Company_Code=s.Company_Code left outer join " + tblPrefix + "AccountMaster b on b.Ac_Code=s.BROKER and b.Company_Code=s.Company_Code" +
                //            " where s.doc_no NOT IN (Select k.PURCNO from " + tblPrefix + "SugarSale k where k.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and k.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and ( PURCNO like '%" + txtSearchText.Text + "%')";

                //string qry = "select doc_no as PurcNo,date_format(doc_date,'%d/%m/%Y') as doc_date ,millname,supplier,NETQNTL,balance from qrysugarpurchasebalance  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Balance!=0 and (doc_no like '%" + txtSearchText.Text + "%' or millname like '%" + txtSearchText.Text + "%' or supplier like '%" + txtSearchText.Text + "%')";
                string qry = "select doc_no as PurcNo,PURCNO as DO_No,doc_date,MillName,PartyName,NETQNTL,Balance from " + tblPrefix + "qrySugarPurchaseBalance  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Balance!=0 and (doc_no like '%" + txtSearchText.Text + "%' or MillName like '%" + txtSearchText.Text + "%' or PartyName like '%" + txtSearchText.Text + "%') order by doc_no";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBill_To")
            {
                lblPopupHead.Text = "--Select Bill To Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
                setFocusControl(txtAC_CODE);
            }
            if (hdnfClosePopup.Value == "txtUnit_Code")
            {
                if (txtAC_CODE.Text != string.Empty)
                {
                    string iscarporate = clsCommon.getString("select carporate_party from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (iscarporate == "Y")
                    {
                        lblMsg.Text = "";
                        lblPopupHead.Text = "--Select Unit--";
                        string qry = "select Unit_name,UnitName,unitCity from " + tblPrefix + "qryPartyUnitlist where Ac_Code=" + txtAC_CODE.Text +
                            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (Unit_name like '%" + txtSearchText.Text + "%' or UnitName like '%" + txtSearchText.Text + "%' or unitCity like '%" + txtSearchText.Text + "%') order by UnitName";
                        this.showPopup(qry);
                    }
                    else
                    {
                        lblPopupHead.Text = "--Select Unit Code--";
                        string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                        this.showPopup(qry);
                    }
                }
                else
                {
                    lblMsg.Text = "Please Enter Ac_Code First!";
                    setFocusControl(txtAC_CODE);
                }
            }
            if (hdnfClosePopup.Value == "txtTransportCode")
            {
                lblPopupHead.Text = "--Select Transport Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                lblPopupHead.Text = "--Select Mill--";


                //string qry = "select Ac_Code as [Account Code], Ac_Name_E as [Account Name],Short_Name as [Short Name] from " + AccountMasterTable + " where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Ac_type='M' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                string qry = "select Ac_Code as Account_Code, Ac_Name_E as Account_Name,Short_Name as Short_Name from " + qryAccountList + " where Locked=0 and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Ac_type='M' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBROKER")
            {
                lblPopupHead.Text = "--Select Broker--";
                //string qry = "select " + AccountMasterTable + ".Ac_Code," + AccountMasterTable + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + AccountMasterTable +
                //    " Left Outer join " + cityMasterTable + " on " + AccountMasterTable + ".City_Code=" + cityMasterTable + ".city_code and " + AccountMasterTable + ".Company_Code=" + cityMasterTable + ".company_code where " + AccountMasterTable + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and (" + AccountMasterTable + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + AccountMasterTable + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + AccountMasterTable + ".Ac_Name_E";

                //    " and (" + AccountMasterTable + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + AccountMasterTable + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + AccountMasterTable + ".Ac_Name_E";
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryAccountList + "  where Locked=0 and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%')  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtITEM_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGSTRateCode")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name"; ;
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
                                                //txtDoc_no.Enabled = false;
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
                                                    txtDOC_NO.Enabled = false;
                                                    setFocusControl(txtPURCNO);
                                                    pnlgrdDetail.Enabled = true;
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
            //if (strTextBox == "txtPURCNO")
            //{
            //    setFocusControl(txtDOC_DATE);
            //}
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
                            setFocusControl(txtBrand_Code);
                        }


                    }
                }
                else
                {
                    setFocusControl(txtBrand_Code);
                }
            }
            if (strTextBox == "txtDOC_DATE")
            {
                try
                {
                    string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDate(dt) == true)
                    {
                        setFocusControl(txtAC_CODE);
                    }
                    else
                    {
                        txtDOC_DATE.Text = "";
                        setFocusControl(txtDOC_DATE);
                    }
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
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnfac.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfAcShort.Value = clsCommon.getString("select short_Name from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            LblPartyname.Text = acname;
                            setFocusControl(txtBill_To);
                            txtTO_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            txtPartyMobno.Text = clsCommon.getString("select Mobile_No from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            AmtCalculation();
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
            if (strTextBox == "txtBill_To")
            {
                string acname = "";
                if (txtBill_To.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBill_To.Text);
                    if (a == false)
                    {
                        btntxtBill_To_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtBill_To.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnfbt.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtBill_To.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            //hdnfAcShort.Value = clsCommon.getString("select short_Name from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            lblBill_To.Text = acname;
                            setFocusControl(txtUnit_Code);

                        }
                        else
                        {
                            txtBill_To.Text = string.Empty;
                            lblBill_To.Text = acname;
                            setFocusControl(txtBill_To);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBill_To);
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
                            string qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                            acname = clsCommon.getString(qry);
                            if (acname != string.Empty)
                            {
                                hdnfuc.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                hdnfUnitshort.Value = clsCommon.getString("select cityname from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

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
                            if (acname != string.Empty)
                            {
                                hdnfuc.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                hdnfUnitshort.Value = clsCommon.getString("select cityname from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

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
            if (strTextBox == "txtTransportCode")
            {
                string acname = "";
                if (txtTransportCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtTransportCode.Text);
                    if (a == false)
                    {
                        btnTransport_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnftc.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnnfTransportshort.Value = clsCommon.getString("select Short_Name from " + qryAccountList + " where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            lblTransportName.Text = acname;
                            setFocusControl(txtEway_Bill_No);
                            txtTransportMobno.Text = clsCommon.getString("select Mobile_No from " + qryAccountList + " where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        }
                        else
                        {
                            txtTransportCode.Text = string.Empty;
                            lblTransportName.Text = acname;
                            setFocusControl(txtCASH_ADVANCE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtTransportCode);
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
                        if (millName != string.Empty && millName != "0")
                        {
                            hdnfmc.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type='M' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfMillShort.Value = clsCommon.getString("select Short_Name from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type='M' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            LBLMILLNAME.Text = millName;
                            setFocusControl(txtFROM_STATION);
                            txtFROM_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
            if (strTextBox == "txtPURCNO")
            {
                if (txtPURCNO.Text != string.Empty)
                {
                    qry = "select doc_no,PURCNO,Convert(varchar(10),doc_date,103) as doc_date,Ac_Code,PartyName as Party,Unit_Code,Unit_Name,mill_code,MillName,FROM_STATION,TO_STATION,LORRYNO,BROKER,brokerName as broker_name,wearhouse,subTotal,LESS_FRT_RATE,freight,cash_advance,bank_commission,OTHER_AMT" +
                            ",Bill_Amount,Due_Days,NETQNTL,Balance,TCS_Rate,TDS_Rate from " + tblPrefix + "qrySugarPurchaseBalance  where doc_no=" + txtPURCNO.Text + "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string doc_no = ds.Tables[0].Rows[0]["doc_no"].ToString();
                            //txtAC_CODE.Text = ds.Tables[0].Rows[0]["Ac_Code"].ToString();
                            //LblPartyname.Text = ds.Tables[0].Rows[0]["Party"].ToString();
                            //txtUnit_Code.Text = ds.Tables[0].Rows[0]["Unit_Code"].ToString();
                            //lblUnitName.Text = ds.Tables[0].Rows[0]["Unit_Name"].ToString();
                            lblDONo.Text = ds.Tables[0].Rows[0]["PURCNO"].ToString();

                            txtMILL_CODE.Text = ds.Tables[0].Rows[0]["mill_code"].ToString();
                            LBLMILLNAME.Text = ds.Tables[0].Rows[0]["MillName"].ToString();
                            txtFROM_STATION.Text = ds.Tables[0].Rows[0]["FROM_STATION"].ToString();
                            txtTO_STATION.Text = ds.Tables[0].Rows[0]["TO_STATION"].ToString();
                            txtDOC_DATE.Text = ds.Tables[0].Rows[0]["doc_date"].ToString();
                            txtLORRYNO.Text = ds.Tables[0].Rows[0]["LORRYNO"].ToString();
                            txtWEARHOUSE.Text = ds.Tables[0].Rows[0]["wearhouse"].ToString();
                            txtBROKER.Text = ds.Tables[0].Rows[0]["BROKER"].ToString();
                            LBLBROKERNAME.Text = ds.Tables[0].Rows[0]["broker_name"].ToString();
                            txtNETQNTL.Text = ds.Tables[0].Rows[0]["Balance"].ToString();
                            txtSUBTOTAL.Text = ds.Tables[0].Rows[0]["subTotal"].ToString();
                            txtFREIGHT.Text = ds.Tables[0].Rows[0]["freight"].ToString();
                            txtLESS_FRT_RATE.Text = ds.Tables[0].Rows[0]["LESS_FRT_RATE"].ToString();
                            txtDUE_DAYS.Text = ds.Tables[0].Rows[0]["Due_Days"].ToString();
                            txtCASH_ADVANCE.Text = ds.Tables[0].Rows[0]["cash_advance"].ToString();
                            //txtBANK_COMMISSION.Text = ds.Tables[0].Rows[0]["bank_commission"].ToString();
                            txtOTHER_AMT.Text = ds.Tables[0].Rows[0]["OTHER_AMT"].ToString();
                            txtBILL_AMOUNT.Text = ds.Tables[0].Rows[0]["Bill_Amount"].ToString();
                            txtTaxableAmount.Text = txtSUBTOTAL.Text;
                            txtTCSRate.Text = ds.Tables[0].Rows[0]["TCS_Rate"].ToString();
                            txtTDS.Text = ds.Tables[0].Rows[0]["TDS_Rate"].ToString();
                            qry = "select detail_id as ID,item_code,itemname as item_Name,ic,Brand_Code,Brand_Name,narration,Quantal,packing,bags,rate,item_Amount,Balance,0 as saledetailid from " + tblPrefix + "qrySugarPurchaseBalance where doc_no=" + doc_no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by detail_id";
                            ds = new DataSet();
                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                dt = new DataTable();
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("ID", typeof(string)));
                                    dt.Columns.Add(new DataColumn("item_code", typeof(string)));
                                    dt.Columns.Add(new DataColumn("item_Name", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Brand_Code", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Brand_Name", typeof(string)));
                                    dt.Columns.Add(new DataColumn("narration", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Quantal", typeof(string)));
                                    dt.Columns.Add(new DataColumn("packing", typeof(string)));
                                    dt.Columns.Add(new DataColumn("bags", typeof(string)));
                                    dt.Columns.Add(new DataColumn("rate", typeof(string)));
                                    dt.Columns.Add(new DataColumn("item_Amount", typeof(string)));
                                    dt.Columns.Add((new DataColumn("saledetailid", typeof(int))));
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(string)));
                                    dt.Columns.Add(new DataColumn("ic", typeof(string)));
                                    int srno = 1;
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["ID"] = ds.Tables[0].Rows[i]["ID"].ToString();
                                        dr["item_code"] = ds.Tables[0].Rows[i]["item_code"].ToString();
                                        dr["item_Name"] = ds.Tables[0].Rows[i]["item_Name"].ToString();
                                        dr["Brand_Code"] = ds.Tables[0].Rows[i]["Brand_Code"].ToString();
                                        dr["Brand_Name"] = ds.Tables[0].Rows[i]["Brand_Name"].ToString();
                                        dr["narration"] = ds.Tables[0].Rows[i]["narration"].ToString();
                                        dr["Quantal"] = ds.Tables[0].Rows[i]["Balance"].ToString();
                                        dr["packing"] = ds.Tables[0].Rows[i]["packing"].ToString();
                                        dr["bags"] = ds.Tables[0].Rows[i]["bags"].ToString();
                                        dr["rate"] = ds.Tables[0].Rows[i]["rate"].ToString();
                                        dr["item_Amount"] = ds.Tables[0].Rows[i]["item_Amount"].ToString();
                                        dr["saledetailid"] = ds.Tables[0].Rows[i]["saledetailid"].ToString();
                                        dr["rowAction"] = "A";
                                        dr["SrNo"] = srno++;
                                        dr["ic"] = ds.Tables[0].Rows[i]["ic"].ToString();
                                        dt.Rows.Add(dr);
                                    }
                                    if (dt.Rows.Count > 0)
                                    {
                                        ViewState["currentTable"] = dt;
                                        grdDetail.DataSource = dt;
                                        grdDetail.DataBind();
                                    }
                                    else
                                    {
                                        grdDetail.DataSource = null;
                                        grdDetail.DataBind();
                                    }
                                }
                            }
                        }
                    }
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
                        if (brokername != string.Empty && brokername != "0")
                        {
                            hdnfbk.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtBROKER.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            LBLBROKERNAME.Text = brokername;
                            setFocusControl(txtGSTRateCode);
                        }
                        else
                        {
                            txtBROKER.Text = string.Empty;
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
                            setFocusControl(txtITEM_CODE);
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
                    setFocusControl(txtGSTRateCode);
                }
            }
            if (strTextBox == "txtDUE_DAYS")
            {
                setFocusControl(txtASNGRNNo);
            }
            if (strTextBox == "txtCASH_ADVANCE")
            {
                setFocusControl(txtBILL_AMOUNT);
            }
            if (strTextBox == "txtBANK_COMMISSION")
            {
                setFocusControl(txtOTHER_AMT);
            }
            if (strTextBox == "txtLESS_FRT_RATE")
            {
                setFocusControl(txtBankCommRate);
            }
            if (strTextBox == "txtOTHER_AMT")
            {
                double txtBILL_AMOUNT_Convert = txtBILL_AMOUNT.Text != string.Empty ? Convert.ToDouble(txtBILL_AMOUNT.Text) : 0.00;
                double txtOTHER_AMT_Convert = txtOTHER_AMT.Text != string.Empty ? Convert.ToDouble(txtOTHER_AMT.Text) : 0.00;

                txtBILL_AMOUNT_Convert = (txtBILL_AMOUNT_Convert) + (txtOTHER_AMT_Convert);
                txtBILL_AMOUNT.Text = Convert.ToString(txtBILL_AMOUNT_Convert);
                setFocusControl(txtTransportCode);
            }
            //if (strTextBox == "txtTransportCode")
            //{
            //    setFocusControl(txtCASH_ADVANCE);
            //}
            if (strTextBox == "txtITEM_CODE")
            {
                string itemname = "";
                if (txtITEM_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtITEM_CODE.Text);
                    if (a == false)
                    {
                        btntxtITEM_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (itemname != string.Empty && itemname != "0")
                        {
                            LBLITEMNAME.Text = itemname;
                            setFocusControl(txtBrand_Code);
                        }
                        else
                        {
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
            if (strTextBox == "txtQUANTAL")
            {

                txtPACKING.Text = "50";

                setFocusControl(txtPACKING);
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
            if (strTextBox == "txtITEM_NARRATION")
            {
                setFocusControl(btnAdddetails);
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


            txtDriverMobno.Text = clsCommon.getString("select driver_no from " + tblPrefix + "deliveryorder where doc_no=" + txtDOC_NO + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
            if (strTextBox == "txtTCSRate")
            {
                setFocusControl(txtTCSNet_Payable);
            }
            if (strTextBox == "txtTCSNet_Payable")
            {
                setFocusControl(btnSave);
            }
            //  txtDriverMobno.Text = driverno.ToString();

            #region calculation part
            double qtl = Convert.ToDouble("0" + txtQUANTAL.Text);
            Int32 packing = Convert.ToInt32("0" + txtPACKING.Text);
            Int32 bags = 0;

            double lessfreight = Convert.ToDouble("0" + txtLESS_FRT_RATE.Text);
            double freight = 0.00;

            double netQntl = 0.00;
            double subtotal = 0.00;
            double cashAdv = Convert.ToDouble("0" + txtCASH_ADVANCE.Text);
            double bankComm = Convert.ToDouble("0" + txtBANK_COMMISSION.Text);

            double other = txtOTHER_AMT.Text != string.Empty ? Convert.ToDouble(txtOTHER_AMT.Text) : 0.0;
            double billAmt = 0.00;

            double item_Amount = 0.00;
            double rate = Convert.ToDouble("0" + txtRATE.Text);

            double RCST = Convert.ToDouble("0" + txtCGSTAmount.Text);
            double RIGST = Convert.ToDouble("0" + txtIGSTAmount.Text);
            double RSGST = Convert.ToDouble("0" + txtSGSTAmount.Text);
            double Roundoffamnt = txtRoundOff.Text != string.Empty ? Convert.ToDouble(txtRoundOff.Text) : 0.00;
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
                txtTaxableAmount.Text = subtotal.ToString();
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
                    aaa = clsCommon.getString("select isnull(GSTStateCode,0) from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAC_CODE.Text + "");
                }
            }
            int partygstStateCode = 0;
            if (aaa.Trim().ToString() != "")
            {
                partygstStateCode = Convert.ToInt32(aaa);
            }

            if (Session["CompanyGSTStateCode"] == null || Session["CompanyGSTStateCode"].ToString() == string.Empty)
            {
                Session["CompanyGSTStateCode"] = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                    "  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            }

            int companyGstStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
            string GSTRateCode = txtGSTRateCode.Text;
            double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));

            double CGSTAmountForPS = 0.0;
            double SGSTAmountForPS = 0.0;
            double IGSTAmountForPS = 0.0;

            double CGSTRateForPS = 0.00;
            double SGSTRateForPS = 0.00;
            double IGSTRateForPS = 0.00;
            double taxmillamt = Convert.ToDouble(txtTaxableAmount.Text);
            if (companyGstStateCode == partygstStateCode)
            {
                CGSTRateForPS = cgstrate;
                double millamount = subtotal;


                double cgsttaxAmountOnMR = Math.Round((taxmillamt * cgstrate / 100), 2);
                //double cgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + cgsttaxAmountOnMR) * mill_rate)), 2);
                //double cgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - cgstExMillRate), 2);
                CGSTAmountForPS = Math.Round(cgsttaxAmountOnMR, 2);

                SGSTRateForPS = sgstrate;
                double sgsttaxAmountOnMR = Math.Round((taxmillamt * sgstrate / 100), 2);
                //double sgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + sgsttaxAmountOnMR) * mill_rate)), 2);
                //double sgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - sgstExMillRate), 2);
                SGSTAmountForPS = Math.Round(sgsttaxAmountOnMR, 2);
            }
            else
            {
                IGSTRateForPS = igstrate;
                double igsttaxAmountOnMR = ((taxmillamt) * igstrate / 100);
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
            double taxableAmount = txtTaxableAmount.Text != string.Empty ? Convert.ToDouble(txtTaxableAmount.Text) : 0.0;
            double cashadvance = txtCASH_ADVANCE.Text != string.Empty ? Convert.ToDouble(txtCASH_ADVANCE.Text) : 0.0;

            double billAmountForCalculation = taxableAmount + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS + bankComm;
            billAmountForCalculation = billAmountForCalculation + other + Roundoffamnt;

            billAmt = Math.Round(billAmountForCalculation, 0);
            txtBILL_AMOUNT.Text = billAmt.ToString();

            #region TDS Calculation
            double TDS_Rate = 0.000;
            double TDS_Amt = 0.00;
            double Bill_Amount = 0.00;
            double TDS_Amount = 0.00;
            TDS_Rate = Convert.ToDouble(txtTDS.Text);
            Bill_Amount = Convert.ToDouble(txtBILL_AMOUNT.Text);
            TDS_Amt = Math.Round(((taxableAmount * TDS_Rate) / 100), 2);

            TDS_Amount = Math.Round(TDS_Amt);
            txtTDSAmt.Text = TDS_Amount.ToString();
            #endregion


            #region TCS Calculation
            double TCS_Rate = 0.000;
            double TCS_Amount = 0.00;
            double TCS_Amt = 0.00;
            double Bill_Amt = 0.00;
            double Net_Payable_Amt = 0.00;
            TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
            Bill_Amt = Convert.ToDouble(txtBILL_AMOUNT.Text);
            TCS_Amount = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);

            TCS_Amt = Math.Round(TCS_Amount);
            Net_Payable_Amt = Bill_Amt + TCS_Amt;


            txtTCSAmt.Text = TCS_Amt.ToString();
            txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
            #endregion
            //txtTaxableAmount.Text = Math.Round((subtotal + freight), 2).ToString();
            #endregion
            //if (btnSave.Text == "Save")
            //{
            //    grdDetail.Enabled = true;
            //    btnAdddetails.Enabled = true;
            //}
            //else
            //{
            //    grdDetail.Enabled = false;
            //    btnAdddetails.Enabled = false;
            //}

            if (lblDONo.Text == "0")
            {
                grdDetail.Enabled = true;
                btnAdddetails.Enabled = true;
            }
            else
            {
                string salebillto = clsCommon.getString("select salebillto from nt_1_deliveryorder where doc_no=" + lblDONo.Text +
           " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

                if (salebillto == Session["SELF_AC"].ToString())
                {

                    grdDetail.Enabled = true;
                    btnAdddetails.Enabled = true;
                }
                else
                {
                    grdDetail.Enabled = true;
                    btnAdddetails.Enabled = true;
                }
            }

        }
        catch
        {
        }
    }
    #endregion

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

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

    }
    protected void txtTransportCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransportCode.Text;
        strTextBox = "txtTransportCode";
        csCalculations();
    }
    protected void btnTransport_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTransportCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
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
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtUnit_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void btntxtGSTRateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGSTRateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }


    protected void lnkDo_Click(object sender, EventArgs e)
    {
        Int32 sbno = lblDONo.Text != string.Empty ? Convert.ToInt32(lblDONo.Text) : 0;

        if (sbno != 0)
        {
            string DO = clsCommon.getString("select doid from nt_1_deliveryorder where doc_no=" + sbno + " and Company_Code=" + Session["Company_Code"].ToString() + " and " +
                "  Year_Code=" + Session["year"].ToString() + "");
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:DO();", true);
            string Action = "1";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DO('" + Action + "','" + DO + "')", true);
        }
    }

    protected void txtBankCommRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double netQntl = txtNETQNTL.Text != string.Empty ? Convert.ToDouble(txtNETQNTL.Text) : 0.00;
            double rateDiff = txtBankCommRate.Text != string.Empty ? Convert.ToDouble(txtBankCommRate.Text) : 0.00;
            double rateDiffAmount = rateDiff * netQntl;
            txtBANK_COMMISSION.Text = rateDiffAmount.ToString();
            setFocusControl(txtOTHER_AMT);
            csCalculations();
        }
        catch (Exception)
        {
            throw;
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


    protected void btnSendSMS_Click(object sender, CommandEventArgs e)
    {
        try
        {
            if (txtEway_Bill_No.Text == "0")
            {
                string NUMBERS = txtPartyMobno.Text + "," + txtTransportMobno.Text + "," + txtDriverMobno.Text;

                setFocusControl(txtEway_Bill_No);
                return;
            }




            string PartyName = LblPartyname.Text;
            string date = txtDOC_DATE.Text;
            string invoiceno = txtDOC_NO.Text;
            string ewaybillno = txtEway_Bill_No.Text == string.Empty ? Convert.ToString(txtEway_Bill_No.Text) : " EWay Bill: " + txtEway_Bill_No.Text.Trim();
            string gstno = txtGStno.Text;
            string truckno = clsCommon.getString("select TRUCK_NO from " + tblPrefix + "deliveryorder where doc_no=" + lblDONo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
            //string gstno = txtGStno.Text == string.Empty ? Convert.ToString(txtGStno.Text) : "LIC: " + txtGStno.Text + Environment.NewLine + ",";
            //string TIN_NO = txtTIN_NO.Text.Trim() == string.Empty ? Convert.ToString(txtTIN_NO.Text) : "TIN: " + txtTIN_NO.Text + Environment.NewLine + ",";
            //string CST_NO = txtCST_NO.Text.Trim() == string.Empty ? Convert.ToString(txtCST_NO.Text) : "CST: " + txtCST_NO.Text + Environment.NewLine + ",";
            //string GST_NO = txtGST_NO.Text.Trim() == string.Empty ? Convert.ToString(txtGST_NO.Text) : "GST: " + txtGST_NO.Text + Environment.NewLine + ",";
            //string ECC_NO = txtECC_NO.Text.Trim() == string.Empty ? Convert.ToString(txtECC_NO.Text) : "ECC: " + txtECC_NO.Text + Environment.NewLine + ",";
            //string FSSAI = txtFssaiNo.Text.Trim() == string.Empty ? Convert.ToString(txtFssaiNo.Text) : "FSSAI: " + txtFssaiNo.Text + Environment.NewLine + ",";
            //string PAN = txtcompanyPan.Text.Trim() == string.Empty ? Convert.ToString(txtcompanyPan.Text) : "PAN: " + txtcompanyPan.Text + Environment.NewLine + ",";
            //string MOBILE = txtSendingMobile.Text;
            //string EMAIL_ID = txtSendingEmail.Text;

            //string BankName = txtBANK_NAME.Text == string.Empty ? Convert.ToString(txtBANK_NAME.Text) : "Bank Name: " + txtBANK_NAME.Text + ",";
            // string BankAc_number = txtBANK_AC_NO.Text == string.Empty ? Convert.ToString(txtBANK_AC_NO.Text) : "Bank A/c Number: " + txtBANK_AC_NO.Text + ",";
            //string BankIFSCode = txtIfsc.Text == string.Empty ? Convert.ToString(txtIfsc.Text) : "IFSC: " + txtIfsc.Text + ",";
            string msg = string.Empty;
            string MsgforMail = string.Empty;

            msg = "" + ewaybillno + " ,Invoice Date :" + date + " ,Truck No:" + truckno + " ,Generated By :" + gstno + " ,Invoice No :" + invoiceno;
            //  MsgforMail = "Banbr/>k Details Of Party <br/>" + PartyName + " <br/>" + BankName + " <br/>" + BankAc_number + " <br/>" + BankIFSCode;

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            if (txtPartyMobno.Text != "")
            {
                string code = txtPartyMobno.Text;
                string subject = txtPartyMobno.Text;
                foreach (string singleSubject in subject.Split(','))
                {
                    if (!dict.ContainsKey(singleSubject))
                    {
                        string partymob = string.Empty;
                        partymob = singleSubject;
                        if (e.CommandName == "sms")
                        {
                            if (!string.IsNullOrWhiteSpace(partymob))
                            {
                                string msgAPI = clsGV.msgAPI;
                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
                                clsCommon.apicall(URL);

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


            if (txtTransportMobno.Text != "")
            {
                string code = txtTransportMobno.Text;
                string subject = txtTransportMobno.Text;
                foreach (string singleSubject in subject.Split(','))
                {
                    if (!dict.ContainsKey(singleSubject))
                    {
                        string partymob = string.Empty;
                        partymob = singleSubject;
                        if (e.CommandName == "sms")
                        {
                            if (!string.IsNullOrWhiteSpace(partymob))
                            {
                                string msgAPI = clsGV.msgAPI;
                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
                                clsCommon.apicall(URL);

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

            if (txtDriverMobno.Text != "")
            {
                string code = txtDriverMobno.Text;
                string subject = txtDriverMobno.Text;
                foreach (string singleSubject in subject.Split(','))
                {
                    if (!dict.ContainsKey(singleSubject))
                    {
                        string partymob = string.Empty;
                        partymob = singleSubject;
                        if (e.CommandName == "sms")
                        {
                            if (!string.IsNullOrWhiteSpace(partymob))
                            {
                                string msgAPI = clsGV.msgAPI;
                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
                                clsCommon.apicall(URL);

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

            if (txtunit.Text != "")
            {
                string code = txtunit.Text;
                string subject = txtunit.Text;
                foreach (string singleSubject in subject.Split(','))
                {
                    if (!dict.ContainsKey(singleSubject))
                    {
                        string partymob = string.Empty;
                        partymob = singleSubject;
                        if (e.CommandName == "sms")
                        {
                            if (!string.IsNullOrWhiteSpace(partymob))
                            {
                                string msgAPI = clsGV.msgAPI;
                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
                                clsCommon.apicall(URL);

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
            //if (txtPartyMobno.Text != "")
            //{
            //    string partymob = string.Empty;
            //    partymob = txtPartyMobno.Text;
            //    if (e.CommandName == "sms")
            //    {
            //        if (!string.IsNullOrWhiteSpace(partymob))
            //        {
            //            string msgAPI = clsGV.msgAPI;
            //            string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
            //            clsCommon.apicall(URL);

            //        }
            //    }
            //}

            //if (txtTransportMobno.Text != "")
            //{
            //    string transportmob = string.Empty;
            //    transportmob = txtTransportMobno.Text;
            //    if (e.CommandName == "sms")
            //    {
            //        if (!string.IsNullOrWhiteSpace(transportmob))
            //        {
            //            string msgAPI = clsGV.msgAPI;
            //            string URL = msgAPI + "mobile=" + transportmob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
            //            clsCommon.apicall(URL);

            //        }
            //    }
            //}


            //if (txtDriverMobno.Text != "")
            //{
            //    string drivermob = string.Empty;
            //    drivermob = txtDriverMobno.Text;
            //    if (e.CommandName == "sms")
            //    {
            //        if (!string.IsNullOrWhiteSpace(drivermob))
            //        {
            //            string msgAPI = clsGV.msgAPI;
            //            string URL = msgAPI + "mobile=" + drivermob + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
            //            clsCommon.apicall(URL);

            //        }
            //    }
            //}
            //ClearSendingSmsTextboxes();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert('SMS  Sent Successfully!')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void chkEWayBill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEWayBill.Checked == true)
        {
            lblchkEWayBill.Text = LBLMILLNAME.Text;
            string gstno = clsCommon.getString("select Gst_No from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            txtGStno.Text = gstno;
        }
        else
        {
            lblchkEWayBill.Text = "";
            txtGStno.Text = Session["Company_GST"].ToString();

        }

    }

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
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

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(saleid) as saleid from " + tblHead + " "));
            if (counts == 0)
            {
                lblSale_Id.Text = "1";
                Sale_id = 1;
            }
            else
            {
                Sale_id = Convert.ToInt32(clsCommon.getString("SELECT max(saleid) as saleid from " + tblHead)) + 1;
                lblSale_Id.Text = Sale_id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
    protected void btnPrintSaleBill_Click(object sender, EventArgs e)
    {
        string billto = clsCommon.getString("select isnull(carporate_ac,0) as id from qrydohead where doc_no='"
            + lblDONo.Text + "' and  Company_Code='" + Session["Company_code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "'");

        string corporatenumber = clsCommon.getString("select Carporate_Sale_No as id from qrydohead where doc_no='"
            + lblDONo.Text + "' and  Company_Code='" + Session["Company_code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "'");
        string sellingtype = clsCommon.getString("select selling_type from carporatehead where doc_no='" + lblDONo.Text +
            "' and Company_Code='" + Session["Company_code"].ToString() + "'");
        string corporate = "0";

        if (corporatenumber !="0" && corporatenumber != string.Empty)
        {
            if (sellingtype == "P")
            {
                string DeliveryType = clsCommon.getString("select DeliveryType from carporatehead where doc_no='" + corporatenumber +
               "' and Company_Code='" + Session["Company_code"].ToString() + "'");
                if (DeliveryType == "C")
                {
                    corporate = "0";
                }
                else
                {
                    corporate = "1";
                }

            }
            else
            {
                corporate = "1";
            }
        }
        else
        {
            corporate = "0";
        }





        string saleid = lblSale_Id.Text;
        string do_no = lblDONo.Text;
        if (hdconfirm.Value == "Yes")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid +
                "','" + billto + "','" + do_no + "','" + corporatenumber
                + "','" + corporate + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid +
               "','" + billto + "','" + do_no + "','" + corporatenumber
               + "','" + corporate + "')", true);
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','"
        //    + billto + "','"+do_no+"')", true);
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
            cmd.CommandTimeout = 100;
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
        csCalculations();
    }
    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAmt.Text;
        strTextBox = "txtTDSAmt";
        csCalculations();
    }

    #region [txtnewsbdate_TextChanged]
    protected void txtnewsbdate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnewsbdate.Text;
        strTextBox = "txtnewsbdate";
        csCalculations();
    }
    #endregion
    #region [txtnewsbno_TextChanged]
    protected void txtnewsbno_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnewsbno.Text;
        strTextBox = "txtnewsbno";
        csCalculations();

    }
    #endregion

    protected void btnCancleEInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                bool isValidated = true;
                if (txteinvoiceno.Text != string.Empty || txteinvoiceno.Text != "NA")
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Can't Cancle this Einvoice!!!!!');", true);
                    return;
                }
                string Irn = txteinvoiceno.Text;
                string Cnlrsn = "1";
                string Cnlrem = "Wrong entry";

                string json = "{\"Irn\":\"" + Irn + "\"," +
                               "\"Cnlrsn\":\"" + Cnlrsn + "\"," +
                               "\"Cnlrem\":\"" + Cnlrem + "\"}";

                string username = clsCommon.getString("select E_UserName from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string password = clsCommon.getString("select E_UserPassword from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string USERNAME = clsCommon.getString("select E_UserName_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string PASSWORD = clsCommon.getString("select E_UserPassword_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string gstin = clsCommon.getString("select E_Company_GSTno from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                ////string urlAddress = "https://gsp.adaequare.com/gsp/authenticate?grant_type=token";
                ////string urlAddress1 = "https://gsp.adaequare.com/enriched/ei/api/invoice/cancel";

                string urlAddress = clsCommon.getString("select E_UrlAddress_Token from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string urlAddress1 = clsCommon.getString("select EInvoiceCancle from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string DDate = DateTime.Now.ToString();
                string SBNo = txtDOC_NO.Text;
                string requestid = DDate + SBNo + "SS";
                string token = xmlExecuteDMLQry.GetAuthToken(urlAddress, username, password);
                string Ewaybullno = xmlExecuteDMLQry.GenrateEInvoice(urlAddress1, USERNAME, PASSWORD, gstin, requestid, token, json);
                string str = Ewaybullno;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);


                str = str.Replace("{", "");
                str = str.Replace("}", "");
                str = str.Replace(":", "");
                str = str.Replace(",", "");
                str = str.Replace("\"", "");
                string sub2 = "true";
                bool b = str.Contains(sub2);

                string sub4 = "false";
                bool s = str.Contains(sub4);
                string dist = "distance";

                string sub3 = "WARNING";
                bool n = str.Contains(sub3);

                string qry = "";
                string qry1 = "";
                DataSet ds = new DataSet();
                if (b)
                {
                    if (n)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);
                    }
                    else
                    {
                        int index = str.IndexOf(sub2);
                        if (index > 0)
                        {
                            qry1 = "update nt_1_sugarsale set ackno='NA',einvoiceno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + txtDOC_NO.Text;
                            ds = clsDAL.SimpleQuery(qry1);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice Canclled Successfully !');", true);
                        }

                    }
                }
                else if (s)
                {
                    int index = str.IndexOf(dist);
                    if (index > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Alert1", "alert('" + str + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);
                }
            }
            else
            {
                lblMsg.Text = "Cannot Cancle this EInvoice !";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        catch
        {
        }
    }
    #region [txtBill_To_TextChanged]
    protected void txtBill_To_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To.Text;
        strTextBox = "txtBill_To";
        csCalculations();
    }
    #endregion

    #region [btntxtBill_To_Click]
    protected void btntxtBill_To_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBill_To";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion


    private void AmtCalculation()
    {

        string docdate = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        double Amount = 0.00;
        double Amountf = 0.00;
        string SaleBillTo = txtAC_CODE.Text;
        string Amt = string.Empty;
        double SBBalAmt = 0.00;

        double gstRate = Convert.ToDouble("0" + txtIGSTRate.Text);
        double saleRate = 0.00;
        //double actualSaleRate = Convert.ToDouble("0" + txtSALE_RATE.Text);
        // double commision = Convert.ToDouble("0" + txtCommission.Text);
        //double qt = Convert.ToString(Math.Abs(Convert.ToDouble(txtquantal.Text))) != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0.00;
        string SaleTDS = "";
        string PurchaseTDS = "";


        SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtAC_CODE.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() + "");

        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);

        if (SaleTDS == "Y")
        {
            string tsd = Convert.ToString(txtTDS.Text);
            if (tsd == hdnfSaleTDS.Value)
            {
                hdnfSaleTDS.Value = Session["SaleTDSRate"].ToString();
            }
            else
            {
                hdnfSaleTDS.Value = txtTDS.Text;
            }


        }
        else
        {
            if (SaleTDS == "N")
            {
                hdnfSaleTDS.Value = "0.00";
                string tcssale = Convert.ToString(txtTCSRate.Text);
                if (tcssale == hdnfTCSRate.Value)
                {
                    hdnfTCSRate.Value = Session["TCSRate"].ToString();
                }
                else
                {
                    hdnfTCSRate.Value = txtTCSRate.Text;
                }
            }
            else
            {
                hdnfTCSRate.Value = "0.00";
                txtTCSRate.Text = "0.00";
                hdnfSaleTDS.Value = "0.00";
                txtTDS.Text = "0.00";
            }
        }

        //saleRate = actualSaleRate + commision;

        //SBBalAmt = (saleRate * gstRate) / 100 + saleRate * qt;
        SBBalAmt = Convert.ToDouble("0" + txtTaxableAmount.Text);



        //Amt = clsCommon.getString("select sum(isnull(AMOUNT,0)) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + txtAC_CODE.Text
        //              + " and DRCR='D' and TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
        //              "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
       
        
            //PSTDS
        if (panno != string.Empty)
            {
                if (dtpan.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpan.Rows.Count; i++)
                    {
                        double Amt1 = 0.00;
                        //int accode = 0;
                        // accode=dtpan.Rows
                        //psamt1 = Convert.ToDouble(clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where  AC_CODE=" + dtpan.Rows[0]["Ac_Code"].ToString()
                        // + " and DRCR='C' and  TRAN_TYPE in('PS','PR','RP') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
                        // "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                        Amt1 = Convert.ToDouble(clsCommon.getString("select sum(isnull(AMOUNT,0)) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpan.Rows[i]["Ac_Code"].ToString()
                        + " and DRCR='D' and TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
                         "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
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
            Amount = Amountf + SBBalAmt;
        }
        else
        {
            Amount = Amountf;
        }
        DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        if (SaleTDS == "Y" && dEnd >= ss)
        {
            txtTCSRate.Text = "0.00";

            hdnfTCSRate.Value = "0.00";
            txtTDS.Text = hdnfSaleTDS.Value;
            #region[Balance]
            if (Amount > balancelimit)
            {

                txtTDS.Text = Session["SaleTDSRate"].ToString();
            }
            else
            {

                if (hdnfTCSRate.Value != string.Empty)
                {
                    txtTDS.Text = hdnfSaleTDS.Value;
                }
                else
                {
                    txtTDS.Text = "0.000";
                }

            }
            #endregion
        }
        else
        {
            if (SaleTDS == "N" && dEnd >= ss)
            {
                txtTDS.Text = "0.00";
                hdnfSaleTDS.Value = "0.00";
                txtTCSRate.Text = hdnfTCSRate.Value;
                #region[Balance]
                if (Amount > balancelimit)
                {

                    txtTCSRate.Text = Session["TCSRate"].ToString();
                }
                else
                {

                    if (hdnfTCSRate.Value != string.Empty)
                    {
                        txtTCSRate.Text = hdnfTCSRate.Value;
                    }
                    else
                    {
                        txtTCSRate.Text = "0.000";
                    }

                }
                #endregion

            }

            else
            {
                hdnfTCSRate.Value = "0.00";
                txtTCSRate.Text = "0.00";
                hdnfSaleTDS.Value = "0.00";
                txtTDS.Text = "0.00";
            }
        }


    }

    
}