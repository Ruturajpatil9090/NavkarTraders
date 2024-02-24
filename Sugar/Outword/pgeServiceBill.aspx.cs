using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;

public partial class Sugar_pgeServiceBill : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string SystemMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string trntype = "RB";
    string user = string.Empty;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    #endregion

    #region Head Fields
    Int32 DOC_NO = 0;
    string DOC_DATE = string.Empty;
    Int32 AC_CODE = 0;
    Int32 GSTRateCode = 0;
    double SUBTOTAL = 0.00;
    double CGSTRate = 0.00;
    double CGSTAmount = 0.00;
    double IGSTRate = 0.00;
    double IGSTAmount = 0.00;
    double SGSTRate = 0.00;
    double SGSTAmount = 0.00;
    double TOTAL = 0;
    double ROUND_OFF = 0.00;
    double BILL_AMOUNT = 0.00;
    string isTDS = string.Empty;
    Int32 TDS_AC = 0;
    double TDSPer = 0.00;
    double TDSApplicableAmt = 0.00;
    double TDS = 0.00;
    string retValue = string.Empty;
    string strRev = string.Empty;
    int Company_Code = 0;
    int Year_Code = 0;
    int Branch_Code = 0;
    string billNo = string.Empty;

    int acid = 0;
    int itemid = 0;
    int doc_id = 0;
    int tdsac_id = 0;
    //  double GSTRate =0;
    //double cgstrate =0;
    //double sgstrate =0;
    //double igstrate =0;
    //double CGSTAmountForRR =0;
    //double SGSTAmountForRR =0;
    //double IGSTAmountForRR =0;
    int SaleCGSTAc = 0;
    int SaleSGSTAc = 0;
    int SaleIGSTAc = 0;
    #endregion

    #region detail
    string i_d = string.Empty;
    Int32 item_code = 0;
    string description = string.Empty;
    double amount = 0.00;
    int rbdid = 0;
    string Detail_Insert = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;
    #endregion

    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    string Head_Insert = string.Empty;
    StringBuilder Head_Update = null;
    string Head_Delete = string.Empty;
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    string msg = string.Empty;
    string Action = string.Empty;
    int flag = 0;
    string cs = string.Empty;
    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "RentBillHead";
            tblDetails = tblPrefix + "RentBillDetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            //cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "qryRentBill";
            qryAccountList = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();

            Head_Fields = new StringBuilder();
            Head_Values = new StringBuilder();
            Head_Update = new StringBuilder();
            Detail_Update = new StringBuilder();
            Detail_Delete = new StringBuilder();
            Detail_Fields = new StringBuilder();
            Detail_Values = new StringBuilder();

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
                        hdnf.Value = Request.QueryString["rbid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtDOC_DATE);
                    }
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
            //bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            //if (a == false)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Only Numbers Allow!')", true);
            //}
            //else
            //{
            //    hdnf.Value = txtEditDoc_No.Text;
            //    //string qry = "select *,CONVERT(varchar(10),doc_date,103) as doc_date1 from " + qryCommon
            //    //    + " where doc_no='" + hdnf.Value + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
            //    //    + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Tran_Type='" + trntype + "'";
            //    string qry = "select * from " + qryCommon + " where doc_no='" + hdnf.Value
            //  + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ";
            //    this.fetchRecord(qry);
            //    setFocusControl(txtEditDoc_No);
            //    //setFocusControl(txtSearchText);
            //}

            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Enter Only Numbers!')", true);
            }
            else
            {
                int do_id = Convert.ToInt32(clsCommon.getString("select rbid from " + tblHead + " where doc_no=" + txtEditDoc_No.Text +
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
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {

                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }

                }

                drpTDS_Applicable.Enabled = false;
                btntxtTDS_AC.Enabled = false;
                pnlPopup.Style["display"] = "none";
                btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                btnBack.Enabled = true;
                #region logic
                LblPartyname.Text = "";
                //LBLMILLNAME.Text = "";
                //LBLBROKERNAME.Text = "";
                btntxtAC_CODE.Enabled = false;
                //btntxtMILL_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
                btntxtdoc_no.Enabled = false;

                calenderExtenderDate.Enabled = false;

                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;

                btnPrintSaleBill.Enabled = true;
                //lblUnitName.Text = "";
                txtEditDoc_No.Enabled = true;
                btntxtGSTRateCode.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                #endregion

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
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {

                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }

                }
                drpTDS_Applicable.Enabled = true;
                btntxtTDS_AC.Enabled = true;
                txtEditDoc_No.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = false;
                btnBack.Enabled = false;
                #region set Business logic for save
                LblPartyname.Text = "";
                lblstatename.Text = "";


                //LBLMILLNAME.Text = "";
                //LBLBROKERNAME.Text = "";
                btntxtAC_CODE.Enabled = true;
                btntxtGSTRateCode.Enabled = true;

                //lblTransportName.Text = "";
                btntxtITEM_CODE.Enabled = true;

                calenderExtenderDate.Enabled = true;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                //btntxtUnitcode.Enabled = true;
                //lblUnitName.Text = "";
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                #endregion

                txtTCSRate.Text = Session["TCSRate"].ToString();
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
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {

                    if (c is System.Web.UI.WebControls.TextBox)
                    {

                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }

                }
                drpTDS_Applicable.Enabled = false;
                btntxtTDS_AC.Enabled = false;
                txtEditDoc_No.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                btnPrintSaleBill.Enabled = true;

                txtDescription.Text = string.Empty;
                btnBack.Enabled = true;
                //btntxtUnitcode.Enabled = false;
                #region logic
                btntxtAC_CODE.Enabled = false;
                btntxtGSTRateCode.Enabled = false;

                btntxtITEM_CODE.Enabled = false;
                calenderExtenderDate.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                LBLITEMNAME.Text = "";
                txtITEM_CODE.Text = "";
                txtAmount.Text = string.Empty;
                #endregion
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
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {

                    if (c is System.Web.UI.WebControls.TextBox)
                    {

                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }

                }
                drpTDS_Applicable.Enabled = true;
                btntxtTDS_AC.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDOC_NO.Enabled = false;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                btntxtGSTRateCode.Enabled = true;
                lblMsg.Text = string.Empty;
                btnOpenDetailsPopup.Enabled = true;
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                btnBack.Enabled = false;
                //btntxtUnitcode.Enabled = true;
                #region logic
                btntxtAC_CODE.Enabled = true;

                btntxtITEM_CODE.Enabled = true;
                calenderExtenderDate.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;

                #endregion
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
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnEdit.Focus();
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
        query = "select count(*) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    " ORDER BY doc_no asc  ";
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    " ORDER BY doc_no asc  ";
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")" +
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
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY doc_no DESC  ";
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
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY doc_no asc  ";
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
            query = "select DOC_NO from " + tblHead + " where DOC_NO=(select MAX(DOC_NO) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
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
       // this.getMaxCode();
        this.NextNumber();
        txtDOC_NO.Enabled = false;
        pnlPopupDetails.Style["display"] = "none";
        txtGSTRateCode.Text = "1";
        lblGSTRateName.Text = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGSTRateCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        setFocusControl(txtAC_CODE);
        //lblDONo.Text = "0";
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
        //txtITEM_CODE.Text = "1";
        //if (txtITEM_CODE.Text == "1")
        //{
        //    searchString = txtITEM_CODE.Text;
        //    strTextBox = "txtITEM_CODE";
        //    csCalculations();
        //}
        foreach (GridViewRow gr in grdDetail.Rows)
        {
            gr.Cells[8].Text = "U";
        }
        btnGenEinvoice.Enabled = false;
        setFocusControl(txtAC_CODE);
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
        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(rbid),0) as doid from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
        hdnf.Value = Convert.ToString(maxno);

        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
    }
    #endregion

    protected void drpCashCredit_SelectedIndexChanged(object sender, EventArgs e)
    {
        showLastRecord();
    }

    protected void drpDelivered_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(btnSave);
    }

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
                        hdnf.Value = dt.Rows[0]["rbid"].ToString();
                        txtDOC_NO.Text = dt.Rows[0]["Doc_No"].ToString();
                        servicebilldoc.Value = txtDOC_NO.Text;
                        lbldocid.Text = dt.Rows[0]["rbid"].ToString();
                        servicebillid.Value = lbldocid.Text;
                        txtDOC_DATE.Text = dt.Rows[0]["DateConverted"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["Customer_Code"].ToString();
                        LblPartyname.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        // double CGSTRate = txtCGSTRate.Text != string.Empty ? Convert.ToDouble(txtCGSTRate.Text) : 0.00;
                        txtstate_code.Text = dt.Rows[0]["GSTStateCode"].ToString();// != string.Empty ? txtstate_code.Text : "0";
                        if (txtstate_code.Text == string.Empty)
                        {
                            txtstate_code.Text = "0";
                        }
                        //lblstatename.Text = dt.Rows[0]["CustomerStateName"].ToString();
                        txtGSTRateCode.Text = dt.Rows[0]["GstRateCode"].ToString();
                        lblGSTRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtSubtotal.Text = dt.Rows[0]["Subtotal"].ToString();
                        txtCGSTRate.Text = dt.Rows[0]["CGSTRate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["SGSTRate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["IGSTRate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtTotal.Text = dt.Rows[0]["Total"].ToString();
                        txtRoundOff.Text = dt.Rows[0]["Round_Off"].ToString();
                        txtBILL_AMOUNT.Text = dt.Rows[0]["Final_Amount"].ToString();
                        txtbillNo.Text = dt.Rows[0]["billno"].ToString();
                        drpTDS_Applicable.SelectedValue = dt.Rows[0]["IsTDS"].ToString();
                        txtTDS_AC.Text = dt.Rows[0]["TDS_Ac"].ToString();
                        txtTDS_Perc.Text = dt.Rows[0]["TDS_Per"].ToString();
                        txtTDSApplicalbeAmount.Text = dt.Rows[0]["TDSAmount"].ToString();
                        txtTDS_Amount.Text = dt.Rows[0]["TDS"].ToString();
                        lblTDS_AC_Code.Text = dt.Rows[0]["tdsname"].ToString();
                        hdnfac.Value = dt.Rows[0]["cc"].ToString();
                        hdnftds.Value = dt.Rows[0]["ta"].ToString();

                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        string IsDeleted = dt.Rows[0]["IsDeleted"].ToString();

                        if (IsDeleted == "0")
                        {
                            lblMsg.Text = "delete";
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            btnCancel.Enabled = false;
                            btnGenEinvoice.Enabled = false;

                        }

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

                        if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txteinvoiceno.Text == "0" && txtackno.Text == "0")
                        {
                            btnGenEinvoice.Enabled = true;
                        }
                        else
                        {
                            btnGenEinvoice.Enabled = false;
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
                       // lblMsg.Text = "";

                        #region  Details
                        qry = "select Detail_Id as ID,Item_Code as item_code,ItemName as itemName,Description,Amount,rbdid,'' as rowAction,'' as SrNo,ic,saleac,ac  from qryrentbilldetail where rbid=" + hdnf.Value;
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    //dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    // dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
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
            string qryDisplay = "select * from qryrentbillhead where rbid=" + hdnf.Value;
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

        pnlPopupDetails.Style["display"] = "block";
        //txtPACKING.Text = "50";
        txtITEM_CODE.Text = "";
        txtDescription.Text = "";
        //txtBAGS.Text = "";
        txtAmount.Text = "";

        //txtITEM_NARRATION.Text = "";
        LBLITEMNAME.Text = "";
        setFocusControl(txtAC_CODE);
        txtITEM_CODE.Text = "1";
        string itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
        ItemSelection(itemname, "1");
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
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + txtDOC_NO.Text + " and Detail_ID=" + lblID.Text + " And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
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
                    dt.Columns.Add((new DataColumn("itemName", typeof(string))));
                    dt.Columns.Add((new DataColumn("Description", typeof(string))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("rbdid", typeof(double))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add(new DataColumn("ic", typeof(string)));
                    dt.Columns.Add(new DataColumn("saleac", typeof(string)));
                    dt.Columns.Add(new DataColumn("ac", typeof(string)));

                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(Int32))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("item_code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("itemName", typeof(string))));
                dt.Columns.Add((new DataColumn("Description", typeof(string))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("rbdid", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add(new DataColumn("ic", typeof(string)));
                dt.Columns.Add(new DataColumn("saleac", typeof(string)));
                dt.Columns.Add(new DataColumn("ac", typeof(string)));
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]


            if (txtITEM_CODE.Text != string.Empty)
            {
                dr["item_code"] = txtITEM_CODE.Text;
            }
            else
            {
                setFocusControl(txtITEM_CODE);
            }

            dr["itemName"] = LBLITEMNAME.Text;
            //dr["narration"] = Server.HtmlDecode(txtITEM_NARRATION.Text);
            DataSet dsa = clsDAL.SimpleQuery("select systemid,Sale_AC,SaleAcid from qrymstitem where System_Code=" + txtITEM_CODE.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
            if (dsa != null)
            {
                if (dsa.Tables[0].Rows.Count > 0)
                {
                    dr["ic"] = dsa.Tables[0].Rows[0]["systemid"].ToString();
                    dr["saleac"] = dsa.Tables[0].Rows[0]["Sale_AC"].ToString();

                    dr["ac"] = dsa.Tables[0].Rows[0]["SaleAcid"].ToString();

                }
            }
            //dr["ic"] = clsCommon.getString("select systemid from qrymstitem where System_Code=" + txtITEM_CODE.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
            dr["Description"] = txtDescription.Text;



            if (txtAmount.Text != string.Empty)
            {
                dr["Amount"] = txtAmount.Text;
            }
            else
            {
                setFocusControl(txtAmount);
            }
            double amnt = Convert.ToDouble(txtAmount.Text);
            if (amnt <= 0.00)
            {
                setFocusControl(txtAmount);
                return;
            }

            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["rbdid"] = 1;
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
             //   pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtITEM_CODE);
            }
            else
            {
               // pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->

            txtITEM_CODE.Text = string.Empty;
            LBLITEMNAME.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDescription.Text = string.Empty;
          //  pnlPopupDetails.Style["display"] = "none";
            Calculation();
            csCalculations();
          //  btnAdddetails.Text = "ADD";
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

        txtITEM_CODE.Text = "1";
        LBLITEMNAME.Text = clsCommon.getString("Select System_Name_E from " + tblPrefix + "SystemMaster where System_Code=" + LBLITEMNAME.Text + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //txtITEM_NARRATION.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtAmount.Text = "";

        txtDescription.Text = "";

        //vat ac name

        // pnlPopupDetails.Style["display"] = "none";
        if (txtITEM_CODE.Text == "1")
        {
            searchString = txtITEM_CODE.Text;
            strTextBox = "txtITEM_CODE";
            csCalculations();
        }
        btnAdddetails.Text = "ADD";
        setFocusControl(txtAC_CODE);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);

        txtITEM_CODE.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        LBLITEMNAME.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        //LBLITEMNAME.Text = clsCommon.getString("Select System_Name_E from " + tblPrefix + "SystemMaster where System_Code=" + Server.HtmlDecode(gvrow.Cells[3].Text.ToString()) + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //txtITEM_NARRATION.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtDescription.Text = Server.HtmlDecode(gvrow.Cells[5].Text);

        txtAmount.Text = Server.HtmlDecode(gvrow.Cells[6].Text);

        //vat ac name

        pnlPopupDetails.Style["display"] = "none";
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["ID"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[8].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[8].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[8].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[8].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
                Calculation();
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
            if (v == "txtAC_CODE")
            {
                e.Row.Cells[0].Width = new Unit("90px");
                e.Row.Cells[1].Width = new Unit("400px");
                e.Row.Cells[2].Width = new Unit("200px");
            }
            if (v == "txtGSTRateCode")
            {
                e.Row.Cells[0].Width = new Unit("90px");
                e.Row.Cells[1].Width = new Unit("400px");
                e.Row.Cells[2].Width = new Unit("200px");
            }
            if (v == "txtITEM_CODE")
            {
                e.Row.Cells[0].Width = new Unit("100px");
                e.Row.Cells[1].Width = new Unit("300px");

            }
            if (v == "txtTDS_AC")
            {
                e.Row.Cells[0].Width = new Unit("90px");
                e.Row.Cells[1].Width = new Unit("400px");
                e.Row.Cells[2].Width = new Unit("90px");
                e.Row.Cells[3].Width = new Unit("400px");
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
                        if (grdDetail.Rows[rowindex].Cells[8].Text != "D" && grdDetail.Rows[rowindex].Cells[8].Text != "R")
                        {
                            pnlPopupDetails.Style["display"] = "block";
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
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            // {

            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("300px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("10px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("40px");


            //e.Row.Cells[7].Visible = false;
            // e.Row.Cells[9].Visible = false;


            //int i = 1;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    i++;
            //    foreach (TableCell cell in e.Row.Cells)
            //    {
            //        string s = cell.Text.ToString();
            //        if (cell.Text.Length > 20)
            //        {
            //            cell.Text = cell.Text.Substring(0, 26) + "..";
            //            cell.ToolTip = s;
            //        }
            //    }

            //}
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
            hdnfClosePopup.Value = "txtEditDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region[bill no text changed]
    protected void txtbillNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtbillNo.Text;
        strTextBox = "txtbillNo";
        csCalculations();
    }
    #endregion

    #region [Amount textchanged]
    protected void txtAmount_extChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtAmount.Text;
            strTextBox = "txtAmount";
            csCalculations();
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


    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDescription.Text;
        strTextBox = "txtDescription";
        csCalculations();
    }
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

    #region [drpTDS_Applicable_TextChanged]
    protected void drpTDS_Applicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        searchString = drpTDS_Applicable.SelectedValue;
        strTextBox = "drpTDS_Applicable";
        csCalculations();
    }
    #endregion

    #region [txtTDS_AC_TextChanged]
    protected void txtTDS_AC_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS_AC.Text;
        strTextBox = "txtTDS_AC";
        csCalculations();
    }
    #endregion

    #region [btntxtTDS_AC_Click]
    protected void btntxtTDS_AC_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTDS_AC";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtTDS_Perc_TextChanged]
    protected void txtTDS_Perc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS_Perc.Text;
        strTextBox = "txtTDS_Perc";
        csCalculations();
    }
    #endregion

    #region [txtTDSApplicalbeAmount_TextChanged]
    protected void txtTDSApplicalbeAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSApplicalbeAmount.Text;
        strTextBox = "txtTDSApplicalbeAmount";
        csCalculations();
    }
    #endregion

    #region [txtTDS_Amount_TextChanged]
    protected void txtTDS_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS_Amount.Text;
        strTextBox = "txtTDS_Amount";
        csCalculations();
    }
    #endregion

    #region [txtTotal_TextChanged]
    protected void txtTotal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotal.Text;
        strTextBox = "txtTotal";
        csCalculations();
    }
    #endregion




    #region [txtRoundOff_TextChanged]
    protected void txtRoundOff_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRoundOff.Text;
        strTextBox = "txtRoundOff";
        csCalculations();
    }
    #endregion



    #region [txtSubtotal_TextChanged]
    protected void txtSubtotal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSubtotal.Text;
        strTextBox = "txtSubtotal";
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
            if (hdnff1.Value == "0")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtITEM_CODE";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }
    #endregion




    #region [btntxtPurcNo_Click]
    protected void btntxtPurcNo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPurcNo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion



    #region [btntxtMillCode_Click]
    protected void btntxtMillCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMillCode";
            btnSearch_Click(sender, e);
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
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                setFocusControl(txtAC_CODE);
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
        try
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServiceBill", "javascript:pagevalidation();", true);

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
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtdoc_no.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDOC_NO.Text = string.Empty;
                    txtDOC_NO.Enabled = true;

                    btnSave.Enabled = false;

                    hdnfClosePopup.Value = "Close";
                    setFocusControl(txtDOC_NO);
                    return;
                }

                if (btntxtdoc_no.Text == "Choose No")
                {
                    if (searchString != string.Empty)
                    {
                        txtSearchText.Text = txtSearchText.Text;
                    }
                    else
                    {
                        txtSearchText.Text = txtSearchText.Text;
                    }

                    lblPopupHead.Text = "--Select Account--";
                    string qry = "select distinct Doc_No,Customer_Code,CustomerName  from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and (Customer_Code like '%" + txtSearchText.Text + "%' or CustomerName like '%" + txtSearchText.Text + "%' ) order by CustomerName asc";
                    this.showPopup(qry);
                    setFocusControl(txtSearchText);
                    return;
                }
            }

            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtTDS_AC")
            {
                string qry = string.Empty;

                lblPopupHead.Text = "--Select--";
                qry = " select Ac_Code,Ac_Name_E,Ac_Type,Address_E from qrymstaccountmaster where Company_Code="
                   + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E asc";
                this.showPopup(qry);
            }


            if (hdnfClosePopup.Value == "txtITEM_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' ) ";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGSTRateCode")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where  ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name";
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
                                                //setFocusControl(txtPURCNO);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtDOC_NO.Enabled = false;
                                                    //setFocusControl(txtPURCNO);
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
                                            //setFocusControl(txtPURCNO);
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
            if (strTextBox == "txtAmount")
            {
                double amnt = Convert.ToDouble(txtAmount.Text);
                if (amnt <= 0)
                {
                    setFocusControl(txtAmount);
                    return;
                }
                else
                {
                    setFocusControl(btnAdddetails);
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
                string statecode = "";
                string statename = "";
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
                        statecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        statename = clsCommon.getString("select State_Name from GSTStateMaster where State_Code='" + statecode + "'");
                        if (acname != string.Empty && acname!="0")
                        {
                            hdnfac.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            LblPartyname.Text = acname;
                            txtstate_code.Text = statecode != string.Empty ? statecode : "0";
                            lblstatename.Text = statename;
                            // string address = clsCommon.getString("select Address_E from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            // string city = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            //lblPartyAddress.Text = address + " <br/>" + city;
                            setFocusControl(txtGSTRateCode);
                        }
                        else
                        {
                            txtAC_CODE.Text = string.Empty;
                            LblPartyname.Text = acname;
                            txtstate_code.Text = statecode;
                            lblstatename.Text = statename;
                            setFocusControl(txtAC_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAC_CODE);
                }
            }

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
                        if (itemname != string.Empty && itemname!="0")
                        {
                            hdnff1.Value = null;
                            ItemSelection(itemname, txtITEM_CODE.Text);

                            setFocusControl(txtDescription);
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
                        gstname = clsCommon.getString("select GST_Name from nt_1_gstratemaster where Doc_no=" + txtGSTRateCode.Text + "  ");
                        if (gstname != string.Empty && gstname!="0")
                        {
                            lblGSTRateName.Text = gstname;
                            // string trntype = "C";
                            //GSTCalc();
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
                    setFocusControl(txtGSTRateCode);
                }
            }
            txtTDSApplicalbeAmount.Text = txtSubtotal.Text;
            if (strTextBox == "drpTDS_Applicable")
            {
                if (drpTDS_Applicable.SelectedValue == "Y")
                {
                    txtTDS_Amount.Enabled = true;
                    txtTDS_Perc.Enabled = true;
                    txtTDSApplicalbeAmount.Enabled = true;
                    txtTDS_AC.Enabled = true;
                    btntxtTDS_AC.Enabled = true;
                    txtTDSApplicalbeAmount.Text = txtSubtotal.Text;

                    setFocusControl(txtTDS_AC);


                }
                else
                {
                    txtTDSApplicalbeAmount.Enabled = false;
                    txtTDS_Perc.Enabled = false;
                    txtTDS_Amount.Enabled = false;
                    txtTDS_AC.Enabled = false;
                    btntxtTDS_AC.Enabled = false;
                    lblTDS_AC_Code.Text = string.Empty;
                    txtTDSApplicalbeAmount.Text = "";
                    txtTDS_AC.Text = "";
                    txtTDS_Perc.Text = "";
                    txtTDS_Amount.Text = "";
                    setFocusControl(btnSave);
                }
            }
            if (strTextBox == "txtTDS_AC")
            {
                string bname = "";
                if (txtTDS_AC.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtTDS_AC.Text);
                    if (a == false)
                    {
                        btntxtTDS_AC_Click(this, new EventArgs());
                    }
                    else
                    {
                        bname = clsCommon.getString("select Ac_Name_E from  " + qryAccountList + "  where Ac_Code=" + txtTDS_AC.Text
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (bname != string.Empty && bname!="0")
                        {
                            hdnftds.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtTDS_AC.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            lblTDS_AC_Code.Text = bname;
                            setFocusControl(txtTDS_Perc);

                        }
                        else
                        {
                            txtTDS_AC.Text = string.Empty;
                            lblTDS_AC_Code.Text = bname;
                            setFocusControl(txtTDS_AC);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtTDS_AC);
                }
            }
            if (strTextBox == "txtLCST")
            {
                //  this.CSTCalculation();
                setFocusControl(txtSGSTRate);
            }

            if (strTextBox == "txtIGSTRate")
            {
                //this.IGSTRateCalculation();
                setFocusControl(txtIGSTRate);
            }

            if (strTextBox == "txtSGSTRate")
            {
                //this.SGSTRateCalculation();
                setFocusControl(txtIGSTRate);
            }
            if (strTextBox == "txtRoundOff")
            {
                setFocusControl(btnSave);
            }
            if (strTextBox == "txtbillNo")
            {
                setFocusControl(txtITEM_CODE);
            }
            if (strTextBox == "txtSubtotal")
            {
                txtTDSApplicalbeAmount.Text = txtSubtotal.Text;
                //setFocusControl(txtITEM_CODE);
            }
            if (strTextBox == "txtTDS_Perc")
            {

                setFocusControl(txtTDSApplicalbeAmount);
            }
            if (strTextBox == "txtTCSRate")
            {
                setFocusControl(txtTCSNet_Payable);
            }
            if (strTextBox == "txtTCSNet_Payable")
            {
                setFocusControl(btnSave);
            }
            double TDS_Final_Amount = 0.00;
            if (drpTDS_Applicable.SelectedValue == "Y")
            {
                double tds_amnt = txtTDSApplicalbeAmount.Text != string.Empty ? Convert.ToDouble(txtTDSApplicalbeAmount.Text) : 0;
                double TDS_perc = txtTDS_Perc.Text != string.Empty ? Convert.ToDouble(txtTDS_Perc.Text) : 0;
                TDS_Final_Amount = Math.Round((tds_amnt * TDS_perc / 100), 2);
                txtTDS_Amount.Text = Math.Round(TDS_Final_Amount, 2).ToString();
            }
            txtTDS_Amount.Text = Math.Round(TDS_Final_Amount, 2).ToString();

            Calculation();
        }
        catch
        {
        }
    }

    private void Calculation()
    {
        #region calculation part
        double subtotal = 0.00;

        double CGSTAmount = Convert.ToDouble(txtCGSTAmount.Text != string.Empty ? txtCGSTAmount.Text : "0.00");
        double SGSTAmount = Convert.ToDouble(txtSGSTAmount.Text != string.Empty ? txtSGSTAmount.Text : "0.00");
        double IGSTAmount = Convert.ToDouble(txtIGSTAmount.Text != string.Empty ? txtIGSTAmount.Text : "0.00");


        //double roundoff = Convert.ToDouble("0" + txtRoundOff.Text);
        double roundoff = txtRoundOff.Text != string.Empty ? Convert.ToDouble(txtRoundOff.Text) : 0.00;
        double billamount = Convert.ToDouble("0" + txtBILL_AMOUNT.Text);

        //string itemName = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
        //if (itemName != string.Empty)
        //{
        //    string kgPerKatta = clsCommon.getString("select ISNULL(KgPerKatta,100) as KgPerKatta from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
        //    kgperk = Convert.ToDouble(kgPerKatta);
        //}

        //   double kg = qtl * kgperk;
        // double quintal = qtl;

        //item_Amount = Math.Round((quintal * (rate)), 2);



        //  txtVatTotal.Text = vatTotal.ToString();

        //        txtITEMAMOUNT.Text = item_Amount.ToString();

        //      txtGross.Text = (item_Amount + vatTotal).ToString();
        #region calculate subtotal
        if (grdDetail.Rows.Count > 0)
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[8].Text != "R" && grdDetail.Rows[i].Cells[8].Text != "D")
                {
                    double item_Amt = Convert.ToDouble(grdDetail.Rows[i].Cells[6].Text.Trim());
                    subtotal = subtotal + item_Amt;
                }
                //double gross = Convert.ToDouble(grdDetail.Rows[i].Cells[15].Text.Trim());
                //grossAmount = grossAmount + gross;
            }

            txtSubtotal.Text = subtotal.ToString();
            //txtVatAmount.Text = vatAmount.ToString();
            //txtBILL_AMOUNT.Text = grossAmount.ToString();
        }
        #endregion

        double cgstRate = txtCGSTRate.Text != string.Empty ? Convert.ToDouble(txtCGSTRate.Text) : 0.0;
        double sgstRate = txtSGSTRate.Text != string.Empty ? Convert.ToDouble(txtSGSTRate.Text) : 0.0;
        double igstRate = txtIGSTRate.Text != string.Empty ? Convert.ToDouble(txtIGSTRate.Text) : 0.0;
        string GSTRateCode = txtGSTRateCode.Text;

        double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
        double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
        double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
        double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));

        int partystatecode = txtstate_code.Text != string.Empty ? Convert.ToInt32(txtstate_code.Text) : 0;
        double cgstAmountOnBillAmount = 0;
        double sgstAmountOnBillAmount = 0;
        double igstAmountOnBillAmount = 0;
        //if (drpCashCredit.SelectedValue == "CR")
        //{
        #region gstcalculation
        if (txtAC_CODE.Text != string.Empty)
        {
            bool asd = clsCommon.isStringIsNumeric(txtAC_CODE.Text);
            if (asd)
            {
                if (txtAC_CODE.Text != "1")
                {
                    int CompanyGSTStateCode = Convert.ToInt32(clsCommon.getString("select GSTStateCode from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));

                    //int partystatecode = Convert.ToInt32(clsCommon.getString("select ISNULL(GSTStateCode,0) from " + tblPrefix + "AccountMaster where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
                    if (partystatecode != 0)
                    {
                        if (CompanyGSTStateCode == partystatecode)
                        {
                            txtCGSTRate.Text = cgstrate.ToString();
                            txtSGSTRate.Text = sgstrate.ToString();
                            txtIGSTRate.Text = "0";
                            cgstAmountOnBillAmount = Math.Round(((subtotal * cgstrate) / 100), 2);
                            sgstAmountOnBillAmount = Math.Round(((subtotal * sgstrate) / 100), 2);
                        }
                        else
                        {
                            igstAmountOnBillAmount = Math.Round(((subtotal * igstrate) / 100), 2);
                            txtCGSTRate.Text = "0";
                            txtSGSTRate.Text = "0";
                            txtIGSTRate.Text = igstrate.ToString();
                        }
                    }
                    else
                    {
                        txtCGSTRate.Text = "0";
                        txtSGSTRate.Text = "0";
                        txtIGSTRate.Text = igstrate.ToString();
                        igstAmountOnBillAmount = Math.Round(((subtotal * igstrate) / 100), 2);
                    }
                }
                else
                {
                    txtCGSTRate.Text = cgstrate.ToString();
                    txtSGSTRate.Text = sgstrate.ToString();
                    txtIGSTRate.Text = "0";
                    cgstAmountOnBillAmount = Math.Round(((subtotal * cgstrate) / 100), 2);
                    sgstAmountOnBillAmount = Math.Round(((subtotal * sgstrate) / 100), 2);
                }
            }
        }
        #endregion
        //}
        txtCGSTAmount.Text = cgstAmountOnBillAmount.ToString();
        txtSGSTAmount.Text = sgstAmountOnBillAmount.ToString();
        txtIGSTAmount.Text = igstAmountOnBillAmount.ToString();
        txtTotal.Text = Math.Round(subtotal + cgstAmountOnBillAmount + sgstAmountOnBillAmount + igstAmountOnBillAmount, 2).ToString();
        double total = Convert.ToDouble(txtTotal.Text);
        txtBILL_AMOUNT.Text = Math.Round(total + roundoff, 2).ToString();

        #region TCS Calculation
        //double TCS_Rate = 0.000;
        //double TCS_Amt = 0.00;
        //double Bill_Amt = 0.00;
        //double Net_Payable_Amt = 0.00;
        //if (txtTCSAmt.Text == string.Empty || txtTCSAmt.Text == "0")
        //{
        //    txtTCSAmt.Text = "0";
        //}
        //else
        //{
        //    TCS_Amt = Convert.ToDouble(txtTCSAmt.Text);
        //}
        //TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
        //Bill_Amt = Convert.ToDouble(txtBILL_AMOUNT.Text);
        //if (TCS_Amt == 0)
        //{
        //    TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
        //}
        //Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt), 2);


        //txtTCSAmt.Text = TCS_Amt.ToString();
        //txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
        #endregion

        #region TCS Calculation
        double TCS_Rate = 0.000;
        double TCS_Amt = 0.00;
        double Bill_Amt = 0.00;
        double Net_Payable_Amt = 0.00;
        TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
        Bill_Amt = Convert.ToDouble(txtBILL_AMOUNT.Text);
        TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
        Net_Payable_Amt = Bill_Amt + TCS_Amt;


        txtTCSAmt.Text = TCS_Amt.ToString();
        txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
        #endregion
        //subtotal = Math.Round(grossAmount + cgstAmountOnBillAmount + sgstAmountOnBillAmount + igstAmountOnBillAmount, 2);
        //txtSubtotal.Text = subtotal.ToString();
        //freight = Math.Round((lessfreight * netQntl), 2);
        //txtBILL_AMOUNT.Text = subtotal.ToString();

        #endregion
    }

    private void ItemSelection(string itemname, string itemCode)
    {
        LBLITEMNAME.Text = itemname;
        //string qryGetVatAc = "select Vat_AC from " + tblPrefix + "SystemMaster where System_Code=" + itemCode + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
        //string vatAc_Code = clsCommon.getString(qryGetVatAc);
        //if (vatAc_Code != "0")
        //{
        //    txtVatAc.Text = vatAc_Code;
        //    string qryVatAcDetail = "select Ac_Name_E from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + vatAc_Code + "";
        //    string qryVatRate = "select Ac_rate from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + vatAc_Code + "";
        //    txtVatPercent.Text = clsCommon.getString(qryVatRate);
        //    lblVatAcName.Text = clsCommon.getString(qryVatAcDetail);
        //}
    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }



    protected void txtCGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGSTRate.Text;
        strTextBox = "txtLCST";
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
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGSTRateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void btnPrintSaleBill_Click(object sender, EventArgs e)
    {

    }

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select isnull(max(Doc_No),0) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["Year"].ToString() + "'")) + 1;
            //if (counts == 0)
            //{
            txtDOC_NO.Text = counts.ToString();
            DOC_NO = counts;
            //}


            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(rbid),0) as rbid from " + tblHead + " ")) + 1;
            //if (counts == 0)
            //{
            lbldocid.Text = counts.ToString();
            doc_id = counts;
            //}



        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion

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
                            qry = "update nt_1_sugarsalereturn set ackno='NA',einvoiceno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and doc_no=" + txtDOC_NO.Text;
                            ds = clsDAL.SimpleQuery(qry);

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
}