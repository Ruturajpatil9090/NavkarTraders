using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Text;
using System.Web.Services;

public partial class Sugar_pgeSugarsaleReturnForGST : System.Web.UI.Page
{

    #region data section
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

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

    string user = string.Empty;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;

    string Action = string.Empty;
    string cs = string.Empty;
    Int32 DOC_NO = 0;
    int id = 0;
    int flag = 0;
    int count = 0;

    DataTable dt;
    #endregion
    #region Head Declaration Field
    string Head_Insert = string.Empty;
    StringBuilder Head_Update = null;
    string Head_Delete = string.Empty;
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    string msg = string.Empty;

    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;

    Int32 FromAc = 0;
    Int32 fa = 0;
    Int32 PURCNO = 0;
    string DOC_DATE = string.Empty;
    Int32 AC_CODE = 0;
    Int32 Unit_Code = 0;
    Int32 MILL_CODE = 0;
    Int32 TRANSPORT_CODE = 0;
    string FROM_STATION = string.Empty;
    string TO_STATION = string.Empty;
    string LORRYNO = string.Empty;
    Int32 BROKER = 0;
    string WEARHOUSE = string.Empty;
    double SUBTOTAL = 0.00;
    double LESS_FRT_RATE = 0.00;
    double FREIGHT = 0.00;
    double CASH_ADVANCE = 0.00;
    double BANK_COMMISSION = 0.00;
    double OTHER_AMT = 0.00;
    double BILL_AMOUNT = 0.00;
    Int32 DUE_DAYS = 0;
    double NETQNTL = 0.00;
    double CGSTRate = 0;
    double CGSTAmount = 0;
    double IGSTRate = 0;
    double IGSTAmount = 0;
    double SGSTRate = 0;
    double SGSTAmount = 0;
    Int32 GstRateCode = 0;
    Int32 purcyearcode = 0;
    int Unit_Id = 0;
    int mill_id = 0;
    int Ac_id = 0;
    int Broker_id = 0;
    int gst_id = 0;
    string trntype = "RS";
    int tc = 0;
    #endregion

    #region Detail part Declaration
    string Detail_Insert = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;
    string detail_id = "";
    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    Int32 item_code = 0;
    int item__Name = 0;
    string narration = "";
    double Quantal = 0.00;
    int packing = 0;
    int bags = 0;
    double rate = 0.00;
    double item_Amount = 0.00;

    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "SugarSaleReturn";
            tblDetails = tblPrefix + "sugarsaleDetailsReturn";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = "qrysugarsalereturndetail";
            qryAccountList = tblPrefix + "qryAccountsList";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);

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
                        hdnf.Value = Request.QueryString["srid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
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
                        txtBROKER.Text = "2";
                        txtGSTRateCode.Text = "1";
                        txtPACKING.Text = "50";
                        setFocusControl(txtPURCNO);
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

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["Year"].ToString() + "'"));
            if (counts == 0)
            {

                txtDOC_NO.Text = "1";
                DOC_NO = 1;

            }
            else
            {
                DOC_NO = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as srid from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["Year"].ToString() + "' ")) + 1;
                txtDOC_NO.Text = DOC_NO.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(srid),0) as srid from " + tblHead + " "));
            if (counts == 0)
            {
                lblDoc_Id.Text = "1";

            }
            else
            {
                id = Convert.ToInt32(clsCommon.getString("SELECT max(srid) as srid from " + tblHead)) + 1;
                lblDoc_Id.Text = id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
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
                int do_id = Convert.ToInt32(clsCommon.getString("select srid from " + tblHead + " where doc_no=" + txtEditDoc_No.Text +
                    " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                    " " + Session["year"].ToString() + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:RSBPen('" + do_id + "')", true);

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
                btnOpenDetailsPopup.Enabled = false;
                txtEditDoc_No.Enabled = true;
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
                lblTransportName.Text = "";
                btntxtGSTRateCode.Enabled = false;
                btntxtPURCNO.Enabled = false;
                btntxtAC_CODE.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtBROKER.Enabled = false;
                btntxtdoc_no.Enabled = false;
                btnTransport.Enabled = false;
                txtITEM_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
                txtQUANTAL.Enabled = false;
                txtPACKING.Enabled = false;
                txtBAGS.Enabled = false;
                txtRATE.Enabled = false;
                txtITEMAMOUNT.Enabled = false;
                txtITEM_NARRATION.Enabled = false;
                calenderExtenderDate.Enabled = false;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                btnPrintSaleBill.Enabled = true;
                btntxtUnitcode.Enabled = false;
                #endregion
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtFromAc.Enabled = false;
                btnFromAc.Enabled = false;
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
                btntxtdoc_no.Enabled = true;
                txtEditDoc_No.Enabled = false;
                 lblMsg.Text = "";
                #region set Business logic for save
                LblPartyname.Text = "";
                lblUnitName.Text = "";
                LBLMILLNAME.Text = "";
                LBLBROKERNAME.Text = "";
                lblGSTRateName.Text = "";
                lblTransportName.Text = "";
                btntxtAC_CODE.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtBROKER.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtPURCNO.Enabled = true;
                calenderExtenderDate.Enabled = true;
                btnTransport.Enabled = true;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                btntxtUnitcode.Enabled = true;
                btntxtdoc_no.Enabled = false;
                #endregion

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtFromAc.Enabled = true;
                btnFromAc.Enabled = true;

                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
                txtQUANTAL.Enabled = true;
                txtPACKING.Enabled = true;
                txtBAGS.Enabled = true;
                txtRATE.Enabled = true;
                txtITEMAMOUNT.Enabled = true;
                txtITEM_NARRATION.Enabled = true;
                txtTCSRate.Text = Session["TCSRate"].ToString();
                txtTDS.Text = Session["SaleTDSRate"].ToString();

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
                btntxtUnitcode.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                btnPrintSaleBill.Enabled = true;
                txtEditDoc_No.Enabled = false;
                #region logic
                btntxtAC_CODE.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtBROKER.Enabled = false;
                calenderExtenderDate.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btntxtPURCNO.Enabled = false;
                btnTransport.Enabled = false;
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
                txtFromAc.Enabled = false;
                btnFromAc.Enabled = false;
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
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                btnOpenDetailsPopup.Enabled = true;
                btntxtUnitcode.Enabled = true;
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                #region logic
                btntxtAC_CODE.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtBROKER.Enabled = true;
                calenderExtenderDate.Enabled = true;
                btntxtPURCNO.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btnTransport.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
                txtQUANTAL.Enabled = true;
                txtPACKING.Enabled = true;
                txtBAGS.Enabled = true;
                txtRATE.Enabled = true;
                txtITEMAMOUNT.Enabled = true;
                txtITEM_NARRATION.Enabled = true;

                #endregion

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtFromAc.Enabled = true;
                btnFromAc.Enabled = true;
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
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
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
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
            query = "select DOC_NO from " + tblHead + " where DOC_NO=(select MAX(DOC_NO) from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
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
        txtBROKER.Text = "2";
        txtGSTRateCode.Text = "1";
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        lblGSTRateName.Text = gstname;
        setFocusControl(txtPURCNO);
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (lblMsg.Text != "Delete")
        {
            ViewState["mode"] = null;
            ViewState["mode"] = "U";
            clsButtonNavigation.enableDisable("E");
            pnlgrdDetail.Enabled = true;
            this.makeEmptyForm("E");
            txtDOC_NO.Enabled = false;
            btnGentare_EWayBill.Enabled = false;
            btnGenEinvoice.Enabled = false;
            setFocusControl(txtITEM_CODE);
        }
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Do No EInvoice is generated so it cannot be Deleted')", true);
                return;
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
        //int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(srid),0) as doid from " + tblHead + " where Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));

        //hdnf.Value = Convert.ToString(maxno);
        //clsButtonNavigation.enableDisable("S");
        //this.makeEmptyForm("S");
        //qry = getDisplayQuery();
        //this.fetchRecord(qry);

        hdnf.Value = Request.QueryString["srid"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            string max = clsCommon.getString("select max(srid) as id from " + tblHead + " where Company_Code=" + Session["Company_Code"].ToString() + " " +
               " and Year_Code=" + Session["year"].ToString() + "");
            hdnf.Value = max;
        }

        Response.Redirect("pgeSugarsaleReturnForGST.aspx?srid=" + hdnf.Value + "&Action=" + 1);
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
                        //if (hdnfyearcode.Value != yearcode)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same !')", true);
                        //    return false;
                        //}
                        //string comcode = Session["Company_Code"].ToString();
                        //if (hdnfcompanycode.Value != comcode)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                        //    return false;
                        //}

                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        hdnf.Value = dt.Rows[0]["srid"].ToString();
                        txtDOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        hdnfreturndoc.Value = txtDOC_NO.Text;
                        txtPURCNO.Text = dt.Rows[0]["PURCNO"].ToString();
                        lblDONo.Text = dt.Rows[0]["DO_No"].ToString();
                        lblDoc_Id.Text = hdnf.Value;
                        hdnfreturnid.Value = lblDoc_Id.Text;
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["AC_CODE"].ToString();
                        LblPartyname.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        txtFromAc.Text = dt.Rows[0]["FromAc"].ToString();
                        lblFrom_Ac.Text = dt.Rows[0]["FromAcName"].ToString();
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
                        txtTransportCode.Text = dt.Rows[0]["Transport_Code"].ToString();
                        lblTransportName.Text = dt.Rows[0]["transportname"].ToString();

                        txtCGSTRate.Text = dt.Rows[0]["CGSTRate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["SGSTRate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["IGSTRate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        lblyearcode.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtGSTRateCode.Text = dt.Rows[0]["GstRateCode"].ToString();
                        lblGSTRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        lblyearcode.Text = dt.Rows[0]["purcyearcode"].ToString();

                        txtTDS.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        hdnfacode.Value = dt.Rows[0]["ac"].ToString();
                        hdnfmc.Value = dt.Rows[0]["mc"].ToString();
                        hdnfunitcode.Value = dt.Rows[0]["uc"].ToString();
                        hdnfbROKERCODE.Value = dt.Rows[0]["bc"].ToString();
                        hdnfGstCode.Value = dt.Rows[0]["gc"].ToString();
                        hdnftransportcode.Value = dt.Rows[0]["tc"].ToString();
                        hdnffromac.Value = dt.Rows[0]["fa"].ToString();

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
                        txtASNGRNNo.Text = dt.Rows[0]["ASN_No"].ToString();
                        txtPO_Details.Text = dt.Rows[0]["PO_Details"].ToString();
                        txtEwaybill.Text = dt.Rows[0]["Eway_Bill_No"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();

                        hdnfMillShort.Value = dt.Rows[0]["millshortname"].ToString();
                        hdnnfTransportshort.Value = dt.Rows[0]["transportShort_Name"].ToString();
                        hdnfPartyshort.Value = dt.Rows[0]["PartyShortName"].ToString();
                        hdnfBrokershort.Value = dt.Rows[0]["BrokerShortName"].ToString();
                        string IsDeleted = dt.Rows[0]["IsDeleted"].ToString();
                        if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty
                                   && txtEwaybill.Text == string.Empty)
                        {
                            btnGenEInvoiceEwaybill.Enabled = true;
                            btnGentare_EWayBill.Enabled = false;
                            btnGenEinvoice.Enabled = false;

                        }
                        else
                        {
                            btnGenEInvoiceEwaybill.Enabled = false;
                            if (txtEwaybill.Text == string.Empty || txtEwaybill.Text == "0")
                            {
                                btnGentare_EWayBill.Enabled = true;
                            }
                            else
                            {
                                btnGentare_EWayBill.Enabled = false;
                            }
                            if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txteinvoiceno.Text == "0" && txtackno.Text == "0")
                            {
                                btnGenEinvoice.Enabled = true;
                            }
                            else
                            {
                                btnGenEinvoice.Enabled = false;
                            }
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

                        if (IsDeleted == "0")
                        {
                            if (IsDeleted == "0")
                            {
                                lblMsg.Text = "Delete";
                                btnEdit.Enabled = false;
                                btnDelete.Enabled = false;
                                btnCancel.Enabled = false;
                            }
                        }
                        #region  Details
                        qry = "select detail_id as ID,item_code,itemname as item_Name,narration,Quantal,packing,bags,rate,item_Amount,srdtid as sellAutoid,'' as rowAction,'' as SrNo,ic as Ic from " + qryCommon + " where srid=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {

                                    dt.Rows[0]["SrNo"] = 1;                           //self row
                                    dt.Rows[0]["rowAction"] = "U";
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
                        hdnfsaleAc.Value = clsCommon.getString("select Sale_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        hdnfsaleAcid.Value = clsCommon.getString("select SaleAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
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
            string qryDisplay = "select * from qrysugarsalereturnhead  where srid=" + hdnf.Value + " and Tran_Type='" + trntype + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                    dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("Quantal", typeof(double))));
                    dt.Columns.Add((new DataColumn("packing", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("bags", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("sellAutoid", typeof(double))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("Ic", typeof(double))));
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
                dt.Columns.Add((new DataColumn("narration", typeof(string))));
                dt.Columns.Add((new DataColumn("Quantal", typeof(double))));
                dt.Columns.Add((new DataColumn("packing", typeof(Int32))));
                dt.Columns.Add((new DataColumn("bags", typeof(Int32))));
                dt.Columns.Add((new DataColumn("rate", typeof(double))));
                dt.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("sellAutoid", typeof(double))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add((new DataColumn("Ic", typeof(double))));
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["item_code"] = txtITEM_CODE.Text;
            dr["item_Name"] = LBLITEMNAME.Text;
            dr["Ic"] = clsCommon.getString("select systemid from qrymstitem where System_Code= '" + txtITEM_CODE.Text + "' and " +
                  "company_Code=" + Session["Company_Code"].ToString() + "");
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
                dr["sellAutoid"] = 1;
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
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
                btnAdddetails.Text = "ADD";
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
            hdnfsaleAc.Value = clsCommon.getString("select Sale_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
            hdnfsaleAcid.Value = clsCommon.getString("select SaleAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
            txtITEM_CODE.Text = string.Empty; ;
            LBLITEMNAME.Text = string.Empty;
            txtITEM_NARRATION.Text = string.Empty;
            txtQUANTAL.Text = string.Empty;
            txtRATE.Text = string.Empty;
            txtITEMAMOUNT.Text = string.Empty;
            txtPACKING.Text = "50";
            txtBAGS.Text = string.Empty;
            LBLITEMNAME.Text = string.Empty; ;
            csCalculations();
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
        // pnlPopupDetails.Style["display"] = "none";
        setFocusControl(txtCGSTRate);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[13].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);
        txtITEM_CODE.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        LBLITEMNAME.Text = clsCommon.getString("Select System_Name_E from " + tblPrefix + "SystemMaster where System_Code=" + Server.HtmlDecode(gvrow.Cells[3].Text.ToString()) + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        txtITEM_NARRATION.Text = Server.HtmlDecode(gvrow.Cells[5].Text);
        txtQUANTAL.Text = Server.HtmlDecode(gvrow.Cells[6].Text);
        txtPACKING.Text = Server.HtmlDecode(gvrow.Cells[7].Text);
        txtBAGS.Text = Server.HtmlDecode(gvrow.Cells[8].Text);
        txtRATE.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        txtITEMAMOUNT.Text = Server.HtmlDecode(gvrow.Cells[10].Text);
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["sellAutoid"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where srdtid=" + ID);
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[12].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[12].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[12].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[12].Text = "A";
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
            e.Row.Cells[0].Width = new Unit("50px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtPURCNO")
            {
                e.Row.Cells[0].Visible = true;
            }
            else
            {
                e.Row.Cells[0].Visible = false;
            }
            if (v == "txtPURCNO")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("50px");
                e.Row.Cells[2].Width = new Unit("80px");
                e.Row.Cells[3].Width = new Unit("400px");
                e.Row.Cells[4].Width = new Unit("400px");
                e.Row.Cells[5].Width = new Unit("80px");
                e.Row.Cells[6].Width = new Unit("80px");
                e.Row.Cells[7].Width = new Unit("50px");
                e.Row.Cells[8].Width = new Unit("50px");
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            if (v == "txtAC_CODE" || v == "txtBillTo" || v == "txtUnit_Code" || v == "txtMILL_CODE" || v == "txtBROKER" || v == "txtGSTRateCode" || v == "txtTransportCode")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("300px");
                e.Row.Cells[2].Width = new Unit("200px");
            }
            if (v == "txtITEM_CODE")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("300px");
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
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("250px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("60px");
            //e.Row.Cells[9].ControlStyle.Width = new Unit("100px");
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
        //if (hdnfyearcode.Value != Session["year"].ToString())
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
        //    return;
        //    // Response.Redirect("~/Sugar/pgeHome.aspx", false);
        //}
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saleReturn", "javascript:pagevalidation();", true);

    }
    #endregion

    [WebMethod]
    public static string NewInsert(string SaleInsertUpdate, string saleDetail_Insert, string saleDetail_Update, string saleDetail_Delete, string Gledger_Delete, string Gledger_Insert, string status)
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
            cmd.Parameters.AddWithValue("QryInsertAndUpdate", SaleInsertUpdate);
            cmd.Parameters["QryInsertAndUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Insert", saleDetail_Insert);
            cmd.Parameters["QryDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Update", saleDetail_Update);
            cmd.Parameters["QryDetail_Update"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Delete", saleDetail_Delete);
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
    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "txtPURCNO")
            {
                grdPopup.AllowPaging = false;
                btnSelectVoucers.Enabled = true;
            }
            else
            {
                grdPopup.AllowPaging = true;
                btnSelectVoucers.Enabled = false;
            }

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
                    string qry = "select doc_no,doc_date,PartyShortname,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or PartyShortname like '%" + txtSearchText.Text + "%' or PartyCity like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }
            if (hdnfClosePopup.Value == "txtPURCNO")
            {
                lblPopupHead.Text = "--Select Purchase No--";
                //string qry = "Select s.doc_no as PurcNo,a.Ac_Name_E as mill,b.Ac_Name_E as broker,Convert(varchar(10),s.doc_date,103) as doc_date,s.NETQNTL from " + tblPrefix + "SugarPurchaseReturn s" +
                //            " left outer join " + tblPrefix + "AccountMaster a on a.Ac_Code=s.mill_code and a.Company_Code=s.Company_Code left outer join " + tblPrefix + "AccountMaster b on b.Ac_Code=s.BROKER and b.Company_Code=s.Company_Code" +
                //            " where s.doc_no NOT IN (Select k.PURCNO from " + tblPrefix + "SugarSale k where k.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and k.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and ( PURCNO like '%" + txtSearchText.Text + "%')";

                //string qry = "Select doc_no,doc_dateConverted as doc_date,millshortname as MillName,billtoname as PartyName, Bill_Amount, Quantal," +
                //             " Year_Code,prid from  qrysugarpurchasereturnbalance where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                //             + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Quantal!=0 and " +
                //             " (billtoname like '%%' or millshortname like '%%' or doc_no like '%%' )";

                string qry = "Select doc_no,doc_dateConverted as doc_date,millshortname as MillName,billtoname as PartyName, Bill_Amount, Quantal," +
                             " Year_Code,prid from  qrysugarpurchasereturnbalance where IsDeleted!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                             + " and Quantal!=0 and " +
                             " (billtoname like '%%' or millshortname like '%%' or doc_no like '%%' )";



                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtFromAc")
            {
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
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
                        string qry = "select Unit_name,UnitName,unitCity from " + tblPrefix + "qryPartyUnitlist where Ac_Code=" + txtAC_CODE.Text +
                            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and (Unit_name like '%" + txtSearchText.Text + "%' or UnitName like '%" + txtSearchText.Text + "%' or unitCity like '%" + txtSearchText.Text + "%') order by UnitName";
                        this.showPopup(qry);
                    }
                    else
                    {
                        lblPopupHead.Text = "--Select Unit Code--";
                        string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
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
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and Ac_Type='T' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                lblPopupHead.Text = "--Select Mill--";
                //string qry = "select " + AccountMasterTable + ".Ac_Code," + AccountMasterTable + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + AccountMasterTable +
                //    " inner join " + cityMasterTable + " on " + AccountMasterTable + ".City_Code=" + cityMasterTable + ".city_code where " + AccountMasterTable + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + AccountMasterTable + ".Ac_type='M' " +
                //    " and (" + AccountMasterTable + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + AccountMasterTable + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + AccountMasterTable + ".Ac_Name_E";

                string qry = "select Ac_Code , Ac_Name_E ,Short_Name  from nt_1_accountmaster where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Locked=0 and Ac_type='M'";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBROKER")
            {
                lblPopupHead.Text = "--Select Broker--";
                //string qry = "select " + AccountMasterTable + ".Ac_Code," + AccountMasterTable + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + AccountMasterTable +
                //    " Left Outer join " + cityMasterTable + " on " + AccountMasterTable + ".City_Code=" + cityMasterTable + ".city_code and " + AccountMasterTable + ".Company_Code=" + cityMasterTable + ".company_code where " + AccountMasterTable + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and (" + AccountMasterTable + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + AccountMasterTable + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + AccountMasterTable + ".Ac_Name_E";

                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
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
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where  ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name"; ;
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

                        hdnfacode.Value = clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + " ");
                        hdnfPartyshort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                        if (acname != string.Empty && acname != "0")
                        {
                            LblPartyname.Text = acname;
                            setFocusControl(txtFromAc);
                            txtTO_STATION.Text = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
            if (strTextBox == "txtFromAc")
            {
                string acname = "";
                if (txtFromAc.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtFromAc.Text);
                    if (a == false)
                    {
                        btnFromAc_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnffromac.Value = clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + txtFromAc.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtFromAc.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        if (acname != string.Empty && acname != "0")
                        {
                            lblFrom_Ac.Text = acname;
                            setFocusControl(txtUnit_Code);
                            // txtTO_STATION.Text = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + txtFromAc.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        }
                        else
                        {
                            txtFromAc.Text = string.Empty;
                            lblFrom_Ac.Text = acname;
                            setFocusControl(txtFromAc);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtFromAc);
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
                        hdnfunitcode.Value = clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        string iscarporate = clsCommon.getString("select carporate_party from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + "");
                        if (iscarporate == "Y")
                        {
                            lblMsg.Text = "";
                            lblPopupHead.Text = "--Select Unit--";
                            string qry = "select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                            acname = clsCommon.getString(qry);
                            if (acname != string.Empty && acname != "0")
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
                            // string qry = "select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                            acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            if (acname != string.Empty && acname != "0")
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
                        hdnftransportcode.Value = clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + txtTransportCode.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Type='T' and Ac_Code=" + txtTransportCode.Text + " ");
                        hdnnfTransportshort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Type='T' and Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (acname != string.Empty && acname != "0")
                        {
                            lblTransportName.Text = acname;
                            setFocusControl(txtCASH_ADVANCE);
                        }
                        else
                        {
                            txtTransportCode.Text = string.Empty;
                            lblTransportName.Text = acname;
                            setFocusControl(txtTransportCode);
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
                        hdnfmc.Value = clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        millName = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type='M'");
                        hdnfMillShort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Type='M' and Ac_Code=" + txtMILL_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (millName != string.Empty && millName != "0")
                        {

                            LBLMILLNAME.Text = millName;
                            setFocusControl(txtFROM_STATION);
                            txtFROM_STATION.Text = clsCommon.getString("select CityName from qrymstaccountmaster where Ac_Code=" + txtMILL_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
                    //hdnf.Value
                    // hdnf.Value = txtEditDoc_No.Text;
                    // Int32 purcyrcode = Convert.ToInt32(hdnfyearcode.Value);

                    //hdnfyearcode.Value = lblyearcode.Text;

                    //qry = "select doc_no,PURCNO,Convert(varchar(10),doc_date,103) as doc_date,Ac_Code,PartyName as Party,Unit_Code,Unit_Name,mill_code," +
                    //    "MillName,FROM_STATION,TO_STATION,LORRYNO,BROKER,brokerName as broker_name,wearhouse,subTotal,LESS_FRT_RATE,freight,cash_advance,bank_commission,OTHER_AMT" +
                    //        ",Bill_Amount,Due_Days,NETQNTL,Balance,Year_Code from " + tblPrefix + "qrySugarPurchaseReturnBalance  where doc_no="
                    //        + txtPURCNO.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                    //        + Convert.ToInt32(Session["year"].ToString());

                    //qry = "select doc_no,PURCNO,Convert(varchar(10),doc_date,103) as doc_date,Ac_Code,PartyName as Party,Unit_Code,Unit_Name,mill_code," +
                    //    "MillName,FROM_STATION,TO_STATION,LORRYNO,BROKER,brokerName as broker_name,wearhouse,subTotal,LESS_FRT_RATE,freight,cash_advance,bank_commission,OTHER_AMT" +
                    //        ",Bill_Amount,Due_Days,NETQNTL,Balance,Year_Code from " + tblPrefix + "qrySugarPurchaseReturnBalance  where doc_no="
                    //        + txtPURCNO.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                    //        + hdnfyearcode.Value;

                    qry = "select doc_no,Tran_Type,PURCNO,Convert(varchar(10),doc_date,103) as doc_date,Ac_Code,Ac_Name_E as Party,Unit_Code,unitname AS Unit_Name," +
                        "mill_code,MillName,FROM_STATION,TO_STATION,LORRYNO,BROKER,brokerName as broker_name,wearhouse,subTotal,LESS_FRT_RATE," +
                        "freight,cash_advance,bank_commission,OTHER_AMT,Bill_Amount,Due_Days,NETQNTL,Year_Code,Bill_To,billtoname,tcs_rate,TDS_Rate from " +
                        "qrysugarpurchasereturnhead  where doc_no=" + txtPURCNO.Text +
                        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + hdnfyearcode.Value;


                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {


                            string doc_no = ds.Tables[0].Rows[0]["doc_no"].ToString();
                            string Tran_Type = ds.Tables[0].Rows[0]["Tran_Type"].ToString();


                            string SB_No = clsCommon.getString("Select PURCNO from " + tblPrefix + "SugarSale where doc_no=" + doc_no + " and Company_Code="
                           + Convert.ToInt32(Session["Company_Code"].ToString()) + " ");
                           //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ");

                            string DO_no = clsCommon.getString("Select DO_No from " + tblPrefix + "SugarSale where doc_no=" + doc_no + " and Company_Code="
                           + Convert.ToInt32(Session["Company_Code"].ToString()) + " ");
                           //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ");

                            string carporateNo = clsCommon.getString("Select Carporate_Sale_No from " + tblPrefix + "deliveryorder where doc_no=" + DO_no
                           + " and tran_type='DO' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ");
                           //+ " and Year_Code="
                           //+ Convert.ToInt32(Session["year"].ToString()) + " ");


                            ViewState["carporateNo"] = carporateNo;



                            txtAC_CODE.Text = ds.Tables[0].Rows[0]["Ac_Code"].ToString();
                            LblPartyname.Text = ds.Tables[0].Rows[0]["Party"].ToString();
                            txtUnit_Code.Text = ds.Tables[0].Rows[0]["Unit_Code"].ToString();
                            lblUnitName.Text = ds.Tables[0].Rows[0]["Unit_Name"].ToString();
                            txtMILL_CODE.Text = ds.Tables[0].Rows[0]["mill_code"].ToString();
                            LBLMILLNAME.Text = ds.Tables[0].Rows[0]["MillName"].ToString();
                            txtFROM_STATION.Text = ds.Tables[0].Rows[0]["FROM_STATION"].ToString();
                            txtTO_STATION.Text = ds.Tables[0].Rows[0]["TO_STATION"].ToString();
                            // txtDOC_DATE.Text = ds.Tables[0].Rows[0]["doc_date"].ToString();
                            txtLORRYNO.Text = ds.Tables[0].Rows[0]["LORRYNO"].ToString();
                            txtWEARHOUSE.Text = ds.Tables[0].Rows[0]["wearhouse"].ToString();
                            txtBROKER.Text = ds.Tables[0].Rows[0]["BROKER"].ToString();
                            LBLBROKERNAME.Text = ds.Tables[0].Rows[0]["broker_name"].ToString();
                            //txtNETQNTL.Text = ds.Tables[0].Rows[0]["NETQNTL"].ToString();
                            txtNETQNTL.Text = hdnfQty.Value;
                            txtSUBTOTAL.Text = ds.Tables[0].Rows[0]["subTotal"].ToString();
                            txtFREIGHT.Text = ds.Tables[0].Rows[0]["freight"].ToString();
                            txtLESS_FRT_RATE.Text = ds.Tables[0].Rows[0]["LESS_FRT_RATE"].ToString();
                            txtDUE_DAYS.Text = ds.Tables[0].Rows[0]["Due_Days"].ToString();
                            txtCASH_ADVANCE.Text = ds.Tables[0].Rows[0]["cash_advance"].ToString();
                            txtBANK_COMMISSION.Text = ds.Tables[0].Rows[0]["bank_commission"].ToString();
                            txtOTHER_AMT.Text = ds.Tables[0].Rows[0]["OTHER_AMT"].ToString();
                            txtBILL_AMOUNT.Text = ds.Tables[0].Rows[0]["Bill_Amount"].ToString();
                            lblyearcode.Text = ds.Tables[0].Rows[0]["Year_Code"].ToString();
                            //txtTCSRate.Text = ds.Tables[0].Rows[0]["tcs_rate"].ToString();
                            //txtTDS.Text = ds.Tables[0].Rows[0]["TDS_Rate"].ToString();

                            txtTCSRate.Text = "0";
                            txtTDS.Text = "0";

                            hdnfMillShort.Value = clsCommon.getString("select  Short_Name from qrymstaccountmaster  where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            hdnfBrokershort.Value = clsCommon.getString("select  Short_Name from qrymstaccountmaster  where Ac_Code=" + txtBROKER.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            hdnfPartyshort.Value = clsCommon.getString("select  Short_Name from qrymstaccountmaster  where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                            if (!string.IsNullOrEmpty(carporateNo))
                            {
                                if (carporateNo != "0")
                                {

                                    string billto = clsCommon.getString("select ISNULL(Bill_To,0) from NT_1_CarporateSale where  Company_Code=" 
                                        + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_NO=" + carporateNo + "");


                                    if (billto != "0")
                                    {
                                        string BillToName = clsCommon.getString("select Ac_Name_E from NT_1_AccountMaster where Ac_Code=" + billto + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                                        string BillToAddress = clsCommon.getString("select Address_E from NT_1_AccountMaster where Ac_Code=" + billto + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");


                                        txtFromAc.Text = billto.ToString();
                                        lblFrom_Ac.Text = BillToName;
                                    }
                                }
                                else
                                {

                                    //txtFromAc.Text = ds.Tables[0].Rows[0]["Ac_Code"].ToString();
                                    //lblFrom_Ac.Text = ds.Tables[0].Rows[0]["Party"].ToString();
                                    txtFromAc.Text = ds.Tables[0].Rows[0]["Bill_To"].ToString();
                                    lblFrom_Ac.Text = ds.Tables[0].Rows[0]["billtoname"].ToString();
                                }

                            }
                            else
                            {
                            }


                            hdnfacode.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            hdnfunitcode.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            hdnfmc.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            hdnffromac.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtFromAc.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            hdnfbROKERCODE.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtBROKER.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");



                            //qry = "select detail_id as ID,item_code,narration,Quantal,packing,bags,rate,item_Amount,Balance from " + tblPrefix
                            //    + "qrySugarPurchaseReturnBalance where doc_no=" + doc_no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())                                 
                            //    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by detail_id";

                            qry = "select detail_id as ID,item_code,itemname as item_Name,narration,Quantal,packing,bags,rate,item_Amount,Quantal from " +
                                " qrysugarpurchasereturndetail where doc_no=" + doc_no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                + " and Year_Code=" + hdnfyearcode.Value + " order by detail_id";
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
                                    dt.Columns.Add(new DataColumn("narration", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Quantal", typeof(string)));
                                    dt.Columns.Add(new DataColumn("packing", typeof(string)));
                                    dt.Columns.Add(new DataColumn("bags", typeof(string)));
                                    dt.Columns.Add(new DataColumn("rate", typeof(string)));
                                    dt.Columns.Add(new DataColumn("item_Amount", typeof(string)));
                                    dt.Columns.Add(new DataColumn("srid", typeof(int)));
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
                                        dr["narration"] = ds.Tables[0].Rows[i]["narration"].ToString();
                                        //dr["Quantal"] = ds.Tables[0].Rows[i]["Quantal"].ToString();
                                        dr["Quantal"] = hdnfQty.Value;
                                        dr["packing"] = ds.Tables[0].Rows[i]["packing"].ToString();
                                        dr["bags"] = ds.Tables[0].Rows[i]["bags"].ToString();
                                        dr["rate"] = ds.Tables[0].Rows[i]["rate"].ToString();
                                        dr["item_Amount"] = ds.Tables[0].Rows[i]["item_Amount"].ToString();
                                        dr["srid"] = 0;
                                        dr["rowAction"] = "A";
                                        dr["SrNo"] = srno++;
                                        dr["ic"] = clsCommon.getString("select systemid from qrymstitem where System_Code=" + ds.Tables[0].Rows[i]["item_code"].ToString() + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                                        dt.Rows.Add(dr);

                                        var item = ds.Tables[0].Rows[i]["item_code"].ToString();
                                        hdnfsaleAc.Value = clsCommon.getString("select Sale_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                                        hdnfsaleAcid.Value = clsCommon.getString("select SaleAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
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
                setFocusControl(txtDOC_DATE);
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
                        hdnfGstCode.Value = clsCommon.getString("select Doc_no from nt_1_gstratemaster  where Doc_no=" + txtGSTRateCode.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
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
                    setFocusControl(txtBROKER);
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
                        hdnfbROKERCODE.Value = clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + txtBROKER.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        brokername = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtBROKER.Text + " ");
                        hdnfBrokershort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where  Ac_Code=" + txtBROKER.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (brokername != string.Empty && brokername != "0")
                        {

                            LBLBROKERNAME.Text = brokername;
                            setFocusControl(txtGSTRateCode);
                        }
                        else
                        {
                            txtBROKER.Text = string.Empty;
                            LBLBROKERNAME.Text = brokername;
                            setFocusControl(txtBROKER);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBROKER);
                }
            }
            if (strTextBox == "txtDUE_DAYS")
            {
                setFocusControl(txtLESS_FRT_RATE);
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
                setFocusControl(txtBANK_COMMISSION);
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
                            setFocusControl(txtQUANTAL);
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

            if (strTextBox == "txtLCST")
            {
                this.CSTCalculation();
                setFocusControl(txtSGSTRate);
            }

            if (strTextBox == "txtLIGST")
            {
                this.LIGSTCalculation();
                setFocusControl(txtLESS_FRT_RATE);
            }

            if (strTextBox == "txtLSGST")
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
            double qtl = Convert.ToDouble("0" + txtQUANTAL.Text);
            Int32 packing = Convert.ToInt32("0" + txtPACKING.Text);
            Int32 bags = 0;

            double lessfreight = Convert.ToDouble("0" + txtLESS_FRT_RATE.Text);
            double freight = 0.00;

            double netQntl = 0.00;
            double subtotal = 0.00;
            double cashAdv = Convert.ToDouble("0" + txtCASH_ADVANCE.Text);
            double bankComm = Convert.ToDouble("0" + txtBANK_COMMISSION.Text);
            double other = Convert.ToDouble("0" + txtOTHER_AMT.Text);
            double billAmt = 0.00;

            double item_Amount = 0.00;
            double rate = Convert.ToDouble("0" + txtRATE.Text);
            double RCST = Convert.ToDouble("0" + txtCGSTAmount.Text);
            double RIGST = Convert.ToDouble("0" + txtIGSTAmount.Text);
            double RSGST = Convert.ToDouble("0" + txtSGSTAmount.Text);

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

                    if (grdDetail.Rows[i].Cells[12].Text != "D")
                    {
                        double item_Amt = Convert.ToDouble(grdDetail.Rows[i].Cells[10].Text.Trim());
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
                    if (grdDetail.Rows[i].Cells[12].Text != "D")
                    {
                        double qntl = Convert.ToDouble(grdDetail.Rows[i].Cells[6].Text.Trim());
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
                    aaa = clsCommon.getString("select isnull(GSTStateCode,0) from " + tblPrefix + "AccountMaster where  Ac_Code=" + txtFromAc.Text + "");
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
            double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));

            double CGSTAmountForPS = 0.0;
            double SGSTAmountForPS = 0.0;
            double IGSTAmountForPS = 0.0;

            double CGSTRateForPS = 0.00;
            double SGSTRateForPS = 0.00;
            double IGSTRateForPS = 0.00;

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

            billAmt = Math.Round((subtotal + bankComm + cashAdv + other + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS) - freight, 2);
            txtBILL_AMOUNT.Text = billAmt.ToString();
            // txtTaxableAmount.Text = Math.Round((subtotal + freight), 2).ToString();

            //freight = Math.Round((lessfreight * netQntl), 2);
            //txtFREIGHT.Text = freight.ToString();



            //billAmt = (subtotal + bankComm + cashAdv + other + RCST + RIGST + RSGST) - freight;
            //txtBILL_AMOUNT.Text = billAmt.ToString();
            //// AmtCalculation();
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
            Net_Payable_Amt = Bill_Amt + TCS_Amt;


            txtTCSAmt.Text = TCS_Amt.ToString();
            txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();

            double TDS_Rate = 0.000;
            double TDS_Amt = 0.00;
            double subtotl = 0.00;

            if (txtTDSAmt.Text == string.Empty || txtTDSAmt.Text == "0")
            {
                txtTDSAmt.Text = "0";
            }
            else
            {
                TDS_Amt = Convert.ToDouble(txtTDSAmt.Text);
            }

            TDS_Rate = txtTDS.Text != string.Empty ? Convert.ToDouble(txtTDS.Text) : 0.000;
            subtotl = txtSUBTOTAL.Text != string.Empty ? Convert.ToDouble(txtSUBTOTAL.Text) : 0.000;
            if (TDS_Amt == 0)
            {
                TDS_Amt = Math.Round(((subtotl * TDS_Rate) / 100), 2);
            }
            txtTDSAmt.Text = TDS_Amt.ToString();
            #endregion
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


    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //savechkdvls(); 
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

        //if (ViewState["pages"] != null)
        //{
        //    if (dt.Rows.Count>0)
        //    {
        //        dt = (DataTable)ViewState["pages"];    
        //    }
        //}
        //else
        //{
        //    dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("pageIndex", typeof(int)));
        //    dt.Columns.Add(new DataColumn("rowIndex", typeof(string)));
        //    DataColumn[] keyColumn = new DataColumn[1];
        //    keyColumn[0] = dt.Columns["pageIndex"];
        //    dt.PrimaryKey = keyColumn;
        //}
        //int previousPageIndex = grdPopup.PageIndex;
        //int rowCount = grdPopup.Rows.Count;
        //if (dt.Rows.Contains(previousPageIndex))
        //{
        //    foreach (DataRow pageindex in dt.Rows)
        //    {
        //        if (Convert.ToInt32(pageindex["pageIndex"].ToString()) == previousPageIndex)
        //        {
        //            string[] rowIndexesArr = pageindex["rowIndex"].ToString().Split(',').ToArray();

        //            foreach (var item in rowIndexesArr)
        //            {
        //                CheckBox chkSelect = (CheckBox)grdPopup.Rows[Convert.ToInt32(item)].FindControl("chkSelect");
        //                chkSelect.Checked = true;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (dt.Rows.Contains(e.NewPageIndex))
        //    {
        //        foreach (DataRow pageindex in dt.Rows)
        //        {
        //            if (Convert.ToInt32(pageindex["pageIndex"].ToString()) == e.NewPageIndex)
        //            {
        //                string[] rowIndexesArr = pageindex["rowIndex"].ToString().Split(',').ToArray();

        //                foreach (var item in rowIndexesArr)
        //                {
        //                    CheckBox chkSelect = (CheckBox)grdPopup.Rows[Convert.ToInt32(item)].FindControl("chkSelect");
        //                    chkSelect.Checked = true;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        List<string> rowsList = new List<string>();
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            CheckBox chkSelect = (CheckBox)grdPopup.Rows[i].FindControl("chkSelect");

        //            if (chkSelect.Checked)
        //            {
        //                rowsList.Add(i.ToString());
        //            }
        //        }
        //        if (rowsList.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["pageIndex"] = previousPageIndex;
        //            dr["rowIndex"] = String.Join(",", rowsList.ToArray());
        //            dt.Rows.Add(dr);
        //        }
        //    }
        //}



        //ViewState["pages"] = dt;
        //chkdvaluesp();
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
    protected void btnSelectVoucers_Click(object sender, EventArgs e)
    {
        try
        {
            List<int> lstvouchers = new List<int>();
            List<double> lstQuintal = new List<double>();
            double totalQuintal = 0.0;
            DataTable dtVouchers;
            if (grdPopup.Rows.Count > 0)
            {
                for (int i = 0; i < grdPopup.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)grdPopup.Rows[i].Cells[0].FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        int voucherNo = Convert.ToInt32(grdPopup.Rows[i].Cells[1].Text.ToString());
                        double qntl = Convert.ToDouble(grdPopup.Rows[i].Cells[6].Text.ToString());
                        lstvouchers.Add(voucherNo);
                        lstQuintal.Add(qntl);
                        totalQuintal += qntl;

                    }
                }

                dtVouchers = new DataTable();

                dtVouchers.Columns.Add(new DataColumn("ID", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("item_code", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("item_Name", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("narration", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("Quantal", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("packing", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("bags", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("rate", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("item_Amount", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("sellAutoid", typeof(int)));
                dtVouchers.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("SrNo", typeof(string)));
                dtVouchers.Columns.Add(new DataColumn("Ic", typeof(string)));

                DataRow dr = dtVouchers.NewRow();
                dr["ID"] = 1;
                dr["item_code"] = 1;
                dr["item_Name"] = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                dr["narration"] = "From Vouchers:" + String.Join(",", lstvouchers.ToArray());
                dr["Quantal"] = totalQuintal;
                dr["packing"] = 50;
                dr["bags"] = totalQuintal * 50;
                dr["rate"] = 0.0;
                dr["item_Amount"] = 0.0;
                dr["sellAutoid"] = 1;
                dr["rowAction"] = "A";
                dr["SrNo"] = 1;
                dr["Ic"] = clsCommon.getString("select systemid from qrymstitem where System_Code=1 and " +
                  "company_Code=" + Session["Company_Code"].ToString() + "");
                dtVouchers.Rows.Add(dr);

                if (dtVouchers.Rows.Count > 0)
                {
                    ViewState["currentTable"] = dtVouchers;
                    grdDetail.DataSource = dtVouchers;
                    grdDetail.DataBind();
                    ViewState["lstVouchers"] = lstvouchers;
                    ViewState["lstQuintal"] = lstQuintal;
                    txtNETQNTL.Text = totalQuintal.ToString();
                    txtSUBTOTAL.Text = "0.0";
                    txtFREIGHT.Text = "0.0";
                    txtLESS_FRT_RATE.Text = "0.0";
                    txtDUE_DAYS.Text = "0";
                    txtCASH_ADVANCE.Text = "0.0";
                    txtBANK_COMMISSION.Text = "0.0";
                    txtOTHER_AMT.Text = "0.0";
                    txtBILL_AMOUNT.Text = "0.0";
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void chkdvaluesp()
    {

        ArrayList usercontent = (ArrayList)Session["chkditems"];

        if (usercontent != null && usercontent.Count > 0)
        {

            foreach (GridViewRow gvrow in grdPopup.Rows)
            {

                int index = Convert.ToInt32(gvrow.RowIndex.ToString());

                if (usercontent.Contains(index))
                {

                    CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkSelect");

                    myCheckBox.Checked = true;

                }

            }

        }

    }
    private void savechkdvls()
    {

        ArrayList usercontent = new ArrayList();

        int index = -1;

        foreach (GridViewRow gvrow in grdPopup.Rows)
        {
            //grdPopup.DataKeys[gvrow.RowIndex].Value.ToString();
            index = Convert.ToInt32(gvrow.RowIndex.ToString());

            bool result = ((CheckBox)gvrow.FindControl("chkSelect")).Checked;



            // Check in the Session

            if (Session["chkditems"] != null)

                usercontent = (ArrayList)Session["chkditems"];

            if (result)
            {
                if (!usercontent.Contains(index))
                    usercontent.Add(index);
            }
            else
                usercontent.Remove(index);

        }

        if (usercontent != null && usercontent.Count > 0)
            Session["chkditems"] = usercontent;

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
        strTextBox = "txtLSGST";
        csCalculations();

    }
    protected void txtIGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGSTRate.Text;
        strTextBox = "txtLIGST";
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
    protected void txtFromAc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFromAc.Text;
        strTextBox = "txtFromAc";
        csCalculations();
    }
    protected void btnFromAc_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtFromAc";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #region [txtTCSRate_TextChanged]
    protected void txtTCSRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSRate.Text;
        strTextBox = "txtTCSRate";
        hdnfTCSRate.Value = txtTCSRate.Text;
        csCalculations();
    }
    #endregion

    #region [txtTCSAmt_TextChanged]
    protected void txtTCSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSAmt.Text;
        strTextBox = "txtTCSAmt";
        csCalculations();
    }
    #endregion

    #region [txtTCSNet_Payable_TextChanged]
    protected void txtTCSNet_Payable_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSNet_Payable.Text;
        strTextBox = "txtTCSNet_Payable";
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
    private void AmtCalculation()
    {
        #region OldCode
        //string docdate = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        //double Amount = 0.00;
        //double Amountf = 0.00;
        //string SaleBillTo = txtAC_CODE.Text;
        //string Amt = string.Empty;
        //double SBBalAmt = 0.00;

        //double gstRate = Convert.ToDouble("0" + txtIGSTRate.Text);
        //double saleRate = 0.00;
        //string SaleTDS = "";
        //string PurchaseTDS = "";


        //SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtFromAc.Text
        //    + " and Company_Code=" + Session["Company_Code"].ToString() + "");

        //double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);

        //if (SaleTDS == "Y")
        //{
        //    string tsd = Convert.ToString(txtTDS.Text);
        //    if (tsd == hdnfSaleTDS.Value)
        //    {
        //        hdnfSaleTDS.Value = Session["SaleTDSRate"].ToString();
        //    }
        //    else
        //    {
        //        hdnfSaleTDS.Value = txtTDS.Text;
        //    }


        //}
        //else
        //{
        //    hdnfSaleTDS.Value = "0.00";
        //    string tcssale = Convert.ToString(txtTCSRate.Text);
        //    if (tcssale == hdnfTCSRate.Value)
        //    {
        //        hdnfTCSRate.Value = Session["TCSRate"].ToString();
        //    }
        //    else
        //    {
        //        hdnfTCSRate.Value = txtTCSRate.Text;
        //    }

        //}

        //DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        //if (SaleTDS == "Y" && dEnd >= ss)
        //{
        //    txtTCSRate.Text = "0.00";

        //    hdnfTCSRate.Value = "0.00";
        //    txtTDS.Text = hdnfSaleTDS.Value;
        //    #region[Balance]


        //    if (hdnfTCSRate.Value != string.Empty)
        //    {
        //        txtTDS.Text = hdnfSaleTDS.Value;
        //    }
        //    else
        //    {
        //        txtTDS.Text = "0.000";
        //    }


        //    #endregion
        //}
        //else
        //{
        //    txtTDS.Text = "0.00";
        //    #region[Balance]


        //    if (hdnfTCSRate.Value != string.Empty)
        //    {
        //        txtTCSRate.Text = hdnfTCSRate.Value;
        //    }
        //    else
        //    {
        //        txtTCSRate.Text = "0.000";
        //    }


        //    #endregion
        //}
        #endregion

        string SaleTDS = "";
        double SBBalAmt = 0.00;
        double Amount = 0.00;
        double Amountf = 0.00;
        string Amt = string.Empty;
        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);

        SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtFromAc.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        SBBalAmt = Convert.ToDouble(txtSUBTOTAL.Text);
        Amt = clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + txtFromAc.Text
     + " and DRCR='D' and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        if (Amt == string.Empty || Amt == "")
        {
            Amt = "0.00";
        }
        Amountf = Convert.ToDouble(Amt);
        Amount = Amountf + SBBalAmt;

        if (Amount >= balancelimit)
        {
            if (SaleTDS == "Y")
            {
                txtTDS.Text = Session["SaleTDSRate"].ToString();
                txtTCSRate.Text = "0.00";
            }
            else
            {
                txtTDS.Text = "0.00";
                txtTCSRate.Text = Session["TCSRate"].ToString();
            }

        }
        else
        {
            txtTDS.Text = "0.00";
            txtTCSRate.Text = "0.00";
        }


    }
}



