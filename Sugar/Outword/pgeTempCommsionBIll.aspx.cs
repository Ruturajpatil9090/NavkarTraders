using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Outword_pgeTempCommsionBIll : System.Web.UI.Page
{

    #region data section
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string qryAccountList = string.Empty;
    string millShortName = string.Empty;
    string GLedgerTable = string.Empty;
    string Tran_Type = "LN";             //Local Voucher
    static WebControl objAsp = null;
    string user = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;
    int Doc_No = 0;
    public static SqlConnection conn = null;
    public static SqlCommand cmdm = null;
    int LV_Id = 0;
    string SystemMasterTable = string.Empty;



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
            user = Session["user"].ToString();
            tblHead = "commission_bill";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            qryAccountList = "qrymstaccountmaster";
            qryCommon = "qrycommissionbill";
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
                        hdnf.Value = Request.QueryString["commissionid"];
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
                        setFocusControl(txtAC_CODE);
                    }
                    //if (Session["LV_NO"] != null)
                    //{
                    //    hdnf.Value = Session["LV_NO"].ToString();
                    //    qry = getDisplayQuery();
                    //    this.fetchRecord(qry);
                    //    this.enableDisableNavigateButtons();
                    //    Session["LV_NO"] = null;
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Only Numbers Allow!')", true);
            }
            else
            {
                //string qry = "select * from " + qryHead + " where doc_no='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code="+Session["year"].ToString()+"";
                //this.fetchRecord(qry);
                //setFocusControl(txtEditDoc_No);
                string qry = "select * from " + qryCommon + " where Tran_Type='LV' and doc_no='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Session["year"].ToString() + "";
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);

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
                obj.tableName = tblHead + " where Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                obj.code = "Doc_No";

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
                                    txtdoc_no.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtdoc_no.Enabled = false;
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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                lblMsg.Text = string.Empty;

                #region set Business logic
                lblAc_name.Text = string.Empty;
                lblUnitName.Text = string.Empty;
                lblMill_name.Text = string.Empty;
                lblBroker_name.Text = string.Empty;
                lblDiff.Text = string.Empty;
                txtEditDoc_No.Enabled = true;
                btntxtDONO.Enabled = false;
                btntxtAC_CODE.Enabled = false;
                btntxtBroker_CODE.Enabled = false;
                btntxtGRADE.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtUnitcode.Enabled = false;
                btntxtNarration1.Enabled = false;
                btntxtNarration2.Enabled = false;
                btntxtNarration3.Enabled = false;
                btntxtNarration4.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btntxtTRANSPORT_CODE.Enabled = false;
                calenderExtenderDate.Enabled = false;
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
                btnSave.Text = "Save";
                btntxtDOC_NO.Text = "Change No";
                btntxtDOC_NO.Enabled = true;
                txtdoc_no.Enabled = false;
                lblMsg.Text = "";
                lblTenderNo.Text = "";
                txtEditDoc_No.Enabled = false;
                btntxtDOC_NO.Enabled = false;
                #region set Business logic for save
                lblAc_name.Text = string.Empty;
                lblUnitName.Text = string.Empty;
                lblMill_name.Text = string.Empty;
                lblBroker_name.Text = string.Empty;
                lblDiff.Text = string.Empty;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btntxtDONO.Enabled = true;
                btntxtAC_CODE.Enabled = true;
                btntxtBroker_CODE.Enabled = true;
                btntxtGRADE.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtNarration1.Enabled = true;
                btntxtNarration2.Enabled = true;
                btntxtNarration3.Enabled = true;
                btntxtNarration4.Enabled = true;
                btntxtUnitcode.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtTRANSPORT_CODE.Enabled = true;
                calenderExtenderDate.Enabled = true;
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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                txtEditDoc_No.Enabled = true;

                #region set Business logic for save


                btntxtDONO.Enabled = false;
                btntxtAC_CODE.Enabled = false;
                btntxtBroker_CODE.Enabled = false;
                btntxtGRADE.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtNarration1.Enabled = false;
                btntxtNarration2.Enabled = false;
                btntxtNarration3.Enabled = false;
                btntxtNarration4.Enabled = false;
                btntxtUnitcode.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btntxtTRANSPORT_CODE.Enabled = false;
                calenderExtenderDate.Enabled = false;
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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = true;
                lblMsg.Text = string.Empty;
                #region logic
                btntxtDONO.Enabled = true;
                btntxtAC_CODE.Enabled = true;
                btntxtBroker_CODE.Enabled = true;
                btntxtGRADE.Enabled = true;
                btntxtUnitcode.Enabled = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtNarration1.Enabled = true;
                btntxtNarration2.Enabled = true;
                btntxtNarration3.Enabled = true;
                btntxtNarration4.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                btntxtTRANSPORT_CODE.Enabled = true;
                calenderExtenderDate.Enabled = true;
                #endregion
                txtEditDoc_No.Enabled = false;
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


            //qry = "select Suffix from " + tblHead + " where doc_no=" + hdnf.Value +
            //    " and Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            //    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            // hdnfSuffix.Value = clsCommon.getString(qry);
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnEdit.Focus();
            }
            else                            //new code
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
        //int RecordCount = 0;
        //string query = "";
        //query = "   select count(*) from " + tblHead + " where  Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        //               " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());


        //string cnt = clsCommon.getString(query);
        //if (cnt != string.Empty)
        //{
        //    RecordCount = Convert.ToInt32(cnt);
        //}

        //if (RecordCount != 0 && RecordCount == 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = false;
        //}
        //else if (RecordCount != 0 && RecordCount > 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = true;
        //}
        //if (txtdoc_no.Text != string.Empty)
        //{
        //    if (hdnf.Value != string.Empty)
        //    {
        //        #region check for next or previous record exist or not

        //        query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
        //            " and Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
        //            " ORDER BY doc_no asc  ";
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


        //        query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
        //            " and Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
        //            " ORDER BY doc_no asc  ";
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
        //}
        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                "  and Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
            if (txtdoc_no.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                    " and  Tran_Type='" + Tran_Type + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
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
            if (txtdoc_no.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                    " and  Tran_Type='" + Tran_Type + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MAX(doc_no) from " + tblHead + " where Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")  " +
                " and  Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
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
                txtdoc_no.Text = hdnf.Value;
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

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.NextNumber();
        setFocusControl(txtDONO);
        txtGSTRateCode.Text = "2";
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        lblGSTRateName.Text = gstname;
        pnlPopupDetails.Style["display"] = "none";
        txtTCSRate.Text = Session["TCSRate"].ToString();
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
        if (lblTenderNo.Text != string.Empty)
        {
            txtDONO.Enabled = false;
            btntxtDONO.Enabled = false;
            btnEdit.Enabled = false;
        }
        else
        {
            txtDONO.Enabled = true;
            btntxtDONO.Enabled = true;
        }
        txtdoc_no.Enabled = false;

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
        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(commissionid),0) as id from commission_bill " +
            "where Tran_Type='LV' and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));
        hdnf.Value = Convert.ToString(maxno);
        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
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
                        txtdoc_no.Text = dt.Rows[0]["DOC_NO"].ToString();
                        hdnflvdoc.Value = txtdoc_no.Text;
                        lblLV_Id.Text = dt.Rows[0]["commissionid"].ToString();
                        hdnflvid.Value = lblLV_Id.Text;
                        hdnf.Value = dt.Rows[0]["commissionid"].ToString();
                        //txtSUFFIX.Text = dt.Rows[0]["SUFFIX"].ToString();
                        // txtDONO.Text = dt.Rows[0]["DO_No"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["DOC_DATE"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["AC_CODE"].ToString();
                        lblAc_name.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        txtUnit_Code.Text = dt.Rows[0]["Unit_Code"].ToString();
                        lblUnitName.Text = dt.Rows[0]["unitname"].ToString();
                        txtBroker_CODE.Text = dt.Rows[0]["BROKER_CODE"].ToString();
                        lblBroker_name.Text = dt.Rows[0]["brokername"].ToString();
                        txtTRANSPORT_CODE.Text = dt.Rows[0]["TRANSPORT_CODE"].ToString();
                        LBLTRANSPORT_NAME.Text = dt.Rows[0]["transportname"].ToString();

                        //if (transportcode != string.Empty)
                        //{
                        //    LBLTRANSPORT_NAME.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + transportcode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        //}
                        txtITEM_CODE.Text = dt.Rows[0]["item_code"].ToString();
                        LBLITEMNAME.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtQNTL.Text = dt.Rows[0]["qntl"].ToString();
                        txtPACKING.Text = dt.Rows[0]["packing"].ToString();
                        txtBAGS.Text = dt.Rows[0]["BAGS"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        txtGRADE.Text = dt.Rows[0]["GRADE"].ToString();
                        txtMILL_CODE.Text = dt.Rows[0]["MILL_CODE"].ToString();
                        lblMill_name.Text = dt.Rows[0]["millname"].ToString();
                        txtMILL_RATE.Text = dt.Rows[0]["MILL_RATE"].ToString();
                        txtSALE_RATE.Text = dt.Rows[0]["SALE_RATE"].ToString();


                        //   lblDiff.Text = dt.Rows[0]["commission_amount"].ToString();
                        txtRDiffTender.Text = dt.Rows[0]["commission_amount"].ToString();
                        txtPURCHASE_RATE.Text = dt.Rows[0]["purc_rate"].ToString();
                        // txtRDiffTender.Text = dt.Rows[0]["RDIFFTENDER"].ToString();
                        txtNarration1.Text = dt.Rows[0]["NARRATION1"].ToString();
                        // txtPostage.Text = dt.Rows[0]["misc_amount"].ToString();
                        txtNarration2.Text = dt.Rows[0]["NARRATION2"].ToString();
                        txtCommissionPerQntl.Text = dt.Rows[0]["resale_rate"].ToString();
                        txtResale_Commisson.Text = dt.Rows[0]["resale_commission"].ToString();
                        txtNarration3.Text = dt.Rows[0]["NARRATION3"].ToString();
                        //// txtBANK_COMMISSION.Text = dt.Rows[0]["commission_amount"].ToString();
                        txtNarration4.Text = dt.Rows[0]["NARRATION4"].ToString();
                        // txtFREIGHT.Text = dt.Rows[0]["FREIGHT"].ToString();
                        txtOTHER_Expenses.Text = dt.Rows[0]["misc_amount"].ToString();
                        txtVoucher_Amount.Text = dt.Rows[0]["bill_amount"].ToString();
                        txtGSTRateCode.Text = dt.Rows[0]["gst_code"].ToString();
                        lblGSTRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtTaxableAmount.Text = dt.Rows[0]["texable_amount"].ToString();
                        txtCGSTRate.Text = dt.Rows[0]["cgst_rate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["cgst_amount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["sgst_rate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["sgst_amount"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["igst_rate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["igst_amount"].ToString();

                        txtBANK_COMMISSION.Text = dt.Rows[0]["BANK_COMMISSION"].ToString();
                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        // millShortName = dt.Rows[0]["millshortname"].ToString();
                        txtVoucher_Amount.Text = dt.Rows[0]["bill_amount"].ToString();
                        hdnfmillcode.Value = dt.Rows[0]["mc"].ToString();
                        hdnfAcode.Value = dt.Rows[0]["ac"].ToString();
                        hdnfunitcode.Value = dt.Rows[0]["uc"].ToString();
                        hdnfBrokercode.Value = dt.Rows[0]["bc"].ToString();
                        hdnftransportcode.Value = dt.Rows[0]["tc"].ToString();
                        hdnfItemid.Value = dt.Rows[0]["ic"].ToString();

                        //     lblDiff.Text = dt.Rows[0]["Diff_Amount"].ToString();
                        //txtNarration1.Text = dt.Rows[0]["Narration1"].ToString();
                        //lblTenderNo.Text = dt.Rows[0]["Tender_No"].ToString();
                        //txtDueDays.Text = dt.Rows[0]["Due_Days"].ToString();
                        //Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        //Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                        //if (lblCreated != null)
                        //{
                        //    lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        //}
                        //if (lblModified != null)
                        //{
                        //    lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        //}
                        //Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        //Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        //if (lblCreatedDate != null)
                        //{
                        //    if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                        //    {
                        //        lblCreatedDate.Text = "";
                        //    }
                        //    else
                        //    {
                        //        lblCreatedDate.Text = "Created Date" + dt.Rows[0]["Created_Date"].ToString();
                        //    }
                        //}
                        //if (lblModifiedDate != null)
                        //{
                        //    if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                        //    {
                        //        lblModifiedDate.Text = "";
                        //    }
                        //    else
                        //    {
                        //        lblModifiedDate.Text = "Modified Date" + dt.Rows[0]["Modified_Date"].ToString();
                        //    }
                        //}
                        //lblVoucherBy.Text = dt.Rows[0]["Ac_Name_E"].ToString()

                        // hdnfdiffqulity.Value = clsCommon.getString("select QUALITY_DIFF_AC from NT_1_CompanyParameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "");
                        if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txteinvoiceno.Text == "0" && txtackno.Text == "0")
                        {
                            btnGenEinvoice.Enabled = true;
                        }
                        else
                        {
                            btnGenEinvoice.Enabled = false;
                        }
                        hdnf.Value = txtdoc_no.Text;
                        recordExist = true;
                        lblMsg.Text = "";
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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtParty" || v == "txtMILL_CODE" || v == "txtBANK_CODE")
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(60);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(30);
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[0].Style["overflow"] = "hidden";
                e.Row.Cells[1].Style["overflow"] = "hidden";
                e.Row.Cells[2].Style["overflow"] = "hidden";
            }
            if (v == "txtGSTRateCode")
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(10);
            }
            if (v == "txtdoc_no")
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(50);
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtDONO")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("250px");
                e.Row.Cells[2].Width = new Unit("50px");
                e.Row.Cells[3].Width = new Unit("50px");
                e.Row.Cells[4].Width = new Unit("50px");
                e.Row.Cells[7].Width = new Unit("40px");
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


    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = "select * from " + qryCommon +
            //    " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
            //    " and Tran_Type='" + Tran_Type + "' and Doc_No=" + hdnf.Value + " and Suffix='" + hdnfSuffix.Value.Trim() + "'";
            string qryDisplay = "select * from " + qryCommon + " where Tran_Type='LV' and  commissionid=" + hdnf.Value + "";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();
    }
    #endregion

    #region [txtSUFFIX_TextChanged]
    protected void txtSUFFIX_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSUFFIX.Text;
        strTextBox = "txtSUFFIX";
        csCalculations();
    }
    #endregion

    #region [btntxtDOC_NO_Click]
    protected void btntxtDOC_NO_Click(object sender, EventArgs e)
    {
        try
        {
            if (btntxtDOC_NO.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtdoc_no.Text = string.Empty;
                txtdoc_no.Enabled = true;
                btnSave.Enabled = false;
                setFocusControl(txtdoc_no);
            }
            if (btntxtDOC_NO.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtdoc_no";
                btnSearch_Click(sender, e);

            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtSUFFIX_Click]
    protected void btntxtSUFFIX_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSUFFIX";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtDONO_TextChanged]
    protected void txtDONO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDONO.Text;
        strTextBox = "txtDONO";
        csCalculations();
    }
    #endregion

    #region [btntxtDONO_Click]
    protected void btntxtDONO_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtdoc_no";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion


    #region [txtTRANSPORT_CODE_TextChanged]
    protected void txtTRANSPORT_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTRANSPORT_CODE.Text;
        strTextBox = "txtTRANSPORT_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtTRANSPORT_CODE_Click]
    protected void btntxtTRANSPORT_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTRANSPORT_CODE";
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
        strTextBox = "txtAC_CODE";
        searchString = txtAC_CODE.Text;
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


    #region [txtBroker_CODE_TextChanged]
    protected void txtBroker_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBroker_CODE.Text;
        strTextBox = "txtBroker_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtBroker_CODE_Click]
    protected void btntxtBroker_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtQNTL_TextChanged]
    protected void txtQNTL_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQNTL.Text;
        strTextBox = "txtQNTL";
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

        csCalculations();
    }
    #endregion

    #region [txtHSN_TextChanged]
    protected void txtHSN_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHSN.Text;

        csCalculations();
    }
    #endregion

    #region [txtGRADE_TextChanged]
    protected void txtGRADE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGRADE.Text;
        if (txtGRADE.Text != string.Empty)
        {
            bool a = true;
            if (txtGRADE.Text.Length < 8)
            {
                a = clsCommon.isStringIsNumeric(txtGRADE.Text);
            }
            if (a == false)
            {
                btntxtGRADE_Click(this, new EventArgs());
            }
            else
            {
                pnlPopup.Style["display"] = "none";
                setFocusControl(txtMILL_CODE);
            }
        }
        //searchString = txtGRADE.Text;
        //strTextBox = "txtGRADE";
        //csCalculations();
    }
    #endregion

    #region [btntxtGRADE_Click]
    protected void btntxtGRADE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGRADE";
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

    #region [txtMILL_RATE_TextChanged]
    protected void txtMILL_RATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMILL_RATE.Text;
        strTextBox = "txtMILL_RATE";
        csCalculations();
    }
    #endregion

    #region [txtSALE_RATE_TextChanged]
    protected void txtSALE_RATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSALE_RATE.Text;
        strTextBox = "txtSALE_RATE";
        csCalculations();
    }
    #endregion

    #region [txtPURCHASE_RATE_TextChanged]
    protected void txtPURCHASE_RATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPURCHASE_RATE.Text;
        strTextBox = "txtPURCHASE_RATE";
        txtSALE_RATE.Text = string.Empty;
        csCalculations();
    }
    #endregion

    #region [txtRDiffTender_TextChanged]
    protected void txtRDiffTender_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRDiffTender.Text;
        strTextBox = "txtRDiffTender";
        csCalculations();
    }
    #endregion

    #region [txtNarration1_TextChanged]
    protected void txtNarration1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration1.Text;
        strTextBox = "txtNarration1";
        csCalculations();
    }
    #endregion

    #region [btntxtNarration1_Click]
    protected void btntxtNarration1_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNarration1";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtPostage_TextChanged]
    protected void txtPostage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPostage.Text;
        strTextBox = "txtPostage";
        csCalculations();
    }
    #endregion

    #region [txtNarration2_TextChanged]
    protected void txtNarration2_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration2.Text;
        strTextBox = "txtNarration2";
        csCalculations();
    }
    #endregion

    #region [btntxtNarration2_Click]
    protected void btntxtNarration2_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNarration2";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtResale_Commisson_TextChanged]
    protected void txtResale_Commisson_TextChanged(object sender, EventArgs e)
    {
        searchString = txtResale_Commisson.Text;
        strTextBox = "txtResale_Commisson";
        csCalculations();
    }
    #endregion

    #region [txtNarration3_TextChanged]
    protected void txtNarration3_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration3.Text;
        strTextBox = "txtNarration3";
        csCalculations();
    }
    #endregion

    #region [btntxtNarration3_Click]
    protected void btntxtNarration3_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNarration3";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
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

    #region [txtNarration4_TextChanged]
    protected void txtNarration4_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration4.Text;
        strTextBox = "txtNarration3";
        csCalculations();
    }
    #endregion

    #region [btntxtNarration4_Click]
    protected void btntxtNarration4_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNarration4";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtFREIGHT_TextChanged]
    protected void txtFREIGHT_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtFREIGHT.Text;
        strTextBox = "txtFREIGHT";
        csCalculations();
    }
    #endregion

    #region [txtOTHER_Expenses_TextChanged]
    protected void txtOTHER_Expenses_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOTHER_Expenses.Text;
        strTextBox = "txtOTHER_Expenses";
        csCalculations();
    }
    #endregion

    #region [txtVoucher_Amount_TextChanged]
    protected void txtVoucher_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVoucher_Amount.Text;
        csCalculations();
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
                searchString = txtSearchText.Text;
            }

            if (hdnfClosePopup.Value == "txtdoc_no")
            {
                lblPopupHead.Text = "--Select Voucher--";
                string qry = "SELECT  " + tblHead + ".Doc_No, " + tblHead + ".Suffix, " + tblHead + ".DO_No, Convert(varchar(10)," + tblHead + ".Doc_Date,103) as Doc_Date, " +
                " Party.Ac_Name_E AS PartyName, " + tblHead + ".Quantal FROM  " + AccountMasterTable + " AS Party left outer JOIN " +
                " " + tblHead + " ON Party.Ac_Code = " + tblHead + ".Ac_Code and Party.Company_Code = " + tblHead + ".Company_Code where " + tblHead + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and " + tblHead + ".Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    " and " + tblHead + ".Tran_Type='" + Tran_Type + "' and (Party.Ac_Name_E like '%" + searchString + "%' or " + tblHead + ".Doc_No like '%" + searchString + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtDONO")
            {
                lblPopupHead.Text = "--Select DO--";
                string qry = "select doc_no,VoucherByname as Party,quantal,mill_rate,sale_rate,millShortName as mill,truck_no,BrokerName from " + tblPrefix + "qryDeliveryOrderList where voucher_no=0 and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO'" +
                    " and (doc_no like '%" + txtSearchText.Text + "%' or VoucherByname like '%" + txtSearchText.Text + "%' or quantal like '%" + txtSearchText.Text + "%' or millShortName like '%" + txtSearchText.Text + "%' or truck_no like '%" + txtSearchText.Text + "%' or BrokerName like '%" + txtSearchText.Text + "%') " +
                    " group by doc_no,VoucherByname,quantal,mill_rate,sale_rate,millShortName,truck_no,BrokerName";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {

                lblPopupHead.Text = "--Select Party--";
                txtSearchText.Text = txtAC_CODE.Text;
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0 and " +
               " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtTRANSPORT_CODE")
            {
                lblPopupHead.Text = "--Select Transport--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0 and Ac_Type='T' and " +
               " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
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
                        txtSearchText.Text = txtUnit_Code.Text;
                        //string qry = "select Unit_name,UnitName,unitCity from " + tblPrefix + "qryPartyUnitlist where Ac_Code=" + txtAC_CODE.Text +
                        //    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (Unit_name like '%" + txtSearchText.Text + "%' or UnitName like '%" + txtSearchText.Text + "%' or unitCity like '%" + txtSearchText.Text + "%') order by UnitName";
                        string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                        this.showPopup(qry);
                    }
                    else
                    {
                        lblPopupHead.Text = "--Select Unit Code--";
                        txtSearchText.Text = txtUnit_Code.Text;
                        string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                        this.showPopup(qry);
                    }
                }
                else
                {
                    lblMsg.Text = "Please Enter Ac_Code First!";
                    setFocusControl(txtAC_CODE);
                }
            }
            if (hdnfClosePopup.Value == "txtBroker_CODE")
            {

                lblPopupHead.Text = "--Select Broker--";
                txtSearchText.Text = txtBroker_CODE.Text;

                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0 and " +
               " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGRADE")
            {
                lblPopupHead.Text = "--Select Grade--";
                if (txtGRADE.Text != string.Empty)
                {
                    txtSearchText.Text = txtGRADE.Text;
                }
                string qry = "select  System_Name_E from " + tblPrefix + "SystemMaster where System_Type='S' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and System_Name_E like '%" + txtSearchText.Text + "%' ";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                lblPopupHead.Text = "--Select Mill--";
                txtSearchText.Text = txtMILL_CODE.Text;

                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0 and Ac_type='M' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNarration1")
            {
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E from nt_1_systemmaster where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNarration2")
            {
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E from nt_1_systemmaster where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNarration3")
            {
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E from nt_1_systemmaster where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNarration4")
            {
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E from nt_1_systemmaster where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGSTRateCode")
            {
                lblPopupHead.Text = "--Select Narration--";
                txtSearchText.Text = txtGSTRateCode.Text;

                string qry = "select Doc_No,GST_Name,Rate from nt_1_gstratemaster where ( Doc_No like '%" + txtSearchText.Text + "%' or Gst_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtITEM_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:pagevalidation();", true);

    }
    #endregion
    [WebMethod]
    public static string NewInsert(string localvocherInsertUpdate, string Gledger_Delete, string Gledger_Insert, string status)
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
            cmd.Parameters.AddWithValue("QryInsertAndUpdate", localvocherInsertUpdate);
            cmd.Parameters["QryInsertAndUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Insert", "");
            cmd.Parameters["QryDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Update", "");
            cmd.Parameters["QryDetail_Update"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Delete", "");
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

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtAC_CODE")
            {
                string partyName = string.Empty;
                if (txtAC_CODE.Text != string.Empty)
                {
                    searchString = txtAC_CODE.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtAC_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        string qry = "";
                        hdnfAcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtAC_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        partyName = clsCommon.getString(qry);

                        if (partyName != string.Empty)
                        {
                            lblAc_name.Text = partyName;
                            setFocusControl(txtUnit_Code);
                        }
                        else
                        {
                            lblAc_name.Text = string.Empty;
                            txtAC_CODE.Text = string.Empty;
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
                    if (!clsCommon.isStringIsNumeric(txtUnit_Code.Text))
                    {
                        btntxtUnitcode_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfunitcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtUnit_Code.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        string iscarporate = clsCommon.getString("select carporate_party from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (iscarporate == "Y")
                        {
                            lblMsg.Text = "";
                            lblPopupHead.Text = "--Select Unit--";

                            acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (acname != string.Empty)
                            {
                                lblUnitName.Text = acname;
                                setFocusControl(txtBroker_CODE);
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

                                lblUnitName.Text = acname;
                                setFocusControl(txtBroker_CODE);
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
            if (strTextBox == "txtTRANSPORT_CODE")
            {
                string qry = string.Empty;
                string partyName = string.Empty;
                if (txtTRANSPORT_CODE.Text != string.Empty)
                {
                    searchString = txtTRANSPORT_CODE.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtTRANSPORT_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnftransportcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtTRANSPORT_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtTRANSPORT_CODE.Text + " and Ac_type='T' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        partyName = clsCommon.getString(qry);

                        if (partyName != string.Empty)
                        {
                            LBLTRANSPORT_NAME.Text = partyName;
                            setFocusControl(txtMILL_CODE);
                        }
                        else
                        {
                            LBLTRANSPORT_NAME.Text = string.Empty;
                            txtTRANSPORT_CODE.Text = string.Empty;
                            setFocusControl(txtTRANSPORT_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtTRANSPORT_CODE);
                }
            }
            if (strTextBox == "txtBroker_CODE")
            {
                string brokername = string.Empty;
                if (txtBroker_CODE.Text != string.Empty)
                {
                    searchString = txtBroker_CODE.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtBroker_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        string qry = "";
                        hdnfBrokercode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtBroker_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtBroker_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        brokername = clsCommon.getString(qry);

                        if (brokername != string.Empty)
                        {
                            lblBroker_name.Text = brokername;
                            setFocusControl(txtITEM_CODE);
                        }
                        else
                        {
                            lblBroker_name.Text = string.Empty;
                            txtBroker_CODE.Text = string.Empty;
                            setFocusControl(txtBroker_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBroker_CODE);
                }
            }

            if (strTextBox == "txtMILL_CODE")
            {
                string millName = string.Empty;

                if (txtMILL_CODE.Text != string.Empty)
                {
                    searchString = txtMILL_CODE.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtMILL_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfmillcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtMILL_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        string qry = "";
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='M'";
                        millName = clsCommon.getString(qry);
                        //get shortname
                        qry = "select Short_Name from " + qryAccountList + " where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='M'";
                        millShortName = clsCommon.getString(qry);

                        if (millName != string.Empty)
                        {
                            lblMill_name.Text = millName;

                            setFocusControl(txtMILL_RATE);
                        }
                        else
                        {
                            lblMill_name.Text = string.Empty;
                            txtMILL_CODE.Text = string.Empty;
                            setFocusControl(txtMILL_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtMILL_CODE);
                }

            }

            if (strTextBox == "txtdoc_no" || strTextBox == "txtSUFFIX")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtdoc_no.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtdoc_no.Text != string.Empty)
                        {
                            txtValue = txtdoc_no.Text;

                            string qry = "select * from " + tblHead + " where Tran_Type='" + Tran_Type + "' and  Doc_No='" + txtValue + "' " +
                                " and Suffix='" + txtSUFFIX.Text.Trim() + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and " +
                                " Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' ";
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
                                        hdnfSuffix.Value = dt.Rows[0]["Suffix"].ToString();
                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                //this.getMaxCode();
                                                txtdoc_no.Enabled = false;
                                                btnSave.Enabled = true;   //IMP
                                                txtSUFFIX.Text = string.Empty;
                                                setFocusControl(txtSUFFIX);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and Suffix='" + hdnfSuffix.Value + "'" +
                                                   " and Tran_Type='" + Tran_Type + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                                                  " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    setFocusControl(txtDONO);
                                                    hdnf.Value = txtdoc_no.Text;
                                                    hdnfSuffix.Value = txtSUFFIX.Text.Trim();
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtDONO);
                                            txtdoc_no.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtdoc_no.Text = string.Empty;
                                            setFocusControl(txtdoc_no);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtdoc_no);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtdoc_no.Text = string.Empty;
                        setFocusControl(txtdoc_no);
                    }
                }
                catch
                {

                }
                #endregion
            }
            if (strTextBox == "txtDONO")
            {
                if (txtDONO.Text != string.Empty)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    string qry = "";
                    qry = "select * from " + tblPrefix + "qryDeliveryOrderList where doc_no=" + txtDONO.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and voucher_no=0 and tran_type='DO'";
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                txtAC_CODE.Text = dt.Rows[0]["Ac_Code"].ToString();
                                lblAc_name.Text = dt.Rows[0]["PartyName"].ToString();
                                txtBroker_CODE.Text = dt.Rows[0]["broker"].ToString();
                                lblBroker_name.Text = dt.Rows[0]["BrokerName"].ToString();
                                txtQNTL.Text = dt.Rows[0]["quantal"].ToString();
                                txtPACKING.Text = dt.Rows[0]["packing"].ToString();
                                txtBAGS.Text = dt.Rows[0]["bags"].ToString();
                                txtGRADE.Text = dt.Rows[0]["grade"].ToString();
                                txtMILL_CODE.Text = dt.Rows[0]["mill_code"].ToString();
                                lblMill_name.Text = dt.Rows[0]["millName"].ToString();
                                txtMILL_RATE.Text = dt.Rows[0]["mill_rate"].ToString();
                                txtSALE_RATE.Text = dt.Rows[0]["sale_rate"].ToString();

                                setFocusControl(txtDOC_DATE);
                            }
                            else
                            {
                                setFocusControl(txtDONO);
                            }
                        }
                        else
                        {
                            setFocusControl(txtDONO);
                        }
                    }
                    else
                    {
                        setFocusControl(txtDONO);
                    }
                }
                else
                {
                    setFocusControl(txtDONO);
                }
            }

            if (strTextBox == "txtDOC_DATE")
            {
                setFocusControl(txtAC_CODE);
            }
            if (strTextBox == "txtQNTL")
            {
                txtPACKING.Text = "50";
                setFocusControl(txtPACKING);
            }
            if (strTextBox == "txtPACKING")
            {
                setFocusControl(txtHSN);
            }
            if (strTextBox == "txtHSN")
            {
                setFocusControl(txtGRADE);
            }
            if (strTextBox == "txtGRADE")
            {
                setFocusControl(txtMILL_CODE);
            }
            if (strTextBox == "txtMILL_RATE")
            {
                setFocusControl(txtSALE_RATE);
            }
            if (strTextBox == "txtSALE_RATE")
            {
                setFocusControl(txtPURCHASE_RATE);
            }
            if (strTextBox == "txtPURCHASE_RATE")
            {
                setFocusControl(txtGSTRateCode);
            }

            if (strTextBox == "txtRDiffTender")
            {
                setFocusControl(txtPostage);
            }
            if (strTextBox == "txtPostage")
            {
                setFocusControl(txtCommissionPerQntl);
            }
            if (strTextBox == "txtCommissionPerQntl")
            {
                setFocusControl(txtResale_Commisson);
            }
            if (strTextBox == "txtResale_Commisson")
            {
                setFocusControl(txtBANK_COMMISSION);
            }
            if (strTextBox == "txtBANK_COMMISSION")
            {
                setFocusControl(txtCGSTRate);
            }
            if (strTextBox == "txtFREIGHT")
            {
                setFocusControl(txtTRANSPORT_CODE);
            }
            //if (strTextBox == "txtTRANSPORT_CODE")
            //{
            //    setFocusControl(txtOTHER_Expenses);
            //}
            if (strTextBox == "txtNarration1")
            {
                setFocusControl(txtNarration2);
            }
            if (strTextBox == "txtNarration2")
            {
                setFocusControl(txtNarration3);
            }
            if (strTextBox == "txtNarration3")
            {
                setFocusControl(txtNarration4);
            }
            if (strTextBox == "txtNarration4")
            {
                setFocusControl(btnSave);
            }
            if (strTextBox == "txtDueDays")
            {
                setFocusControl(txtNarration1);
            }
            if (strTextBox == "txtTCSRate")
            {
                setFocusControl(txtTCSNet_Payable);
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
                        gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGSTRateCode.Text + "  ");
                        if (gstname != string.Empty && gstname != "0")
                        {
                            lblGSTRateName.Text = gstname;
                            setFocusControl(txtRDiffTender);
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
                        hdnfItemid.Value = clsCommon.getString("select systemid from qrymstitem where system_code=" + txtITEM_CODE.Text + " and company_code=" + Session["company_code"].ToString() + "");
                        itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (itemname != string.Empty && itemname != "0")
                        {
                            LBLITEMNAME.Text = itemname;
                            setFocusControl(txtQNTL);
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
            if (strTextBox == "txtOTHER_Expenses")
            {
                setFocusControl(txtIGSTRate);
            }

            #region [Calculation Part]

            double qtl = Convert.ToDouble(txtQNTL.Text != string.Empty ? txtQNTL.Text : "0");
            Int32 packing = Convert.ToInt32(txtPACKING.Text != string.Empty ? txtPACKING.Text : "0");
            double bags = 0.00;
            double saleRate = Convert.ToDouble(txtSALE_RATE.Text != string.Empty ? txtSALE_RATE.Text : "0");
            double millRate = Convert.ToDouble(txtMILL_RATE.Text != string.Empty ? txtMILL_RATE.Text : "0");
            double purcRate = Convert.ToDouble(txtPURCHASE_RATE.Text != string.Empty ? txtPURCHASE_RATE.Text : "0");
            double rDiffTender = Convert.ToDouble(txtRDiffTender.Text != string.Empty ? txtRDiffTender.Text : "0");
            double postage = Convert.ToDouble(txtPostage.Text != string.Empty ? txtPostage.Text : "0");
            double resale_comm = Convert.ToDouble(txtResale_Commisson.Text != string.Empty ? txtResale_Commisson.Text : "0");
            double bank_comm = Convert.ToDouble(txtBANK_COMMISSION.Text != string.Empty ? txtBANK_COMMISSION.Text : "0");
            // double freight = Convert.ToDouble("0" + txtFREIGHT.Text);
            double other_expense = Convert.ToDouble(txtOTHER_Expenses.Text != string.Empty ? txtOTHER_Expenses.Text : "0");
            double voucher_Amt = 0.00;
            double diffAmt = 0.00;
            double diff = 0.00;

            if (qtl != 0 && packing != 0)
            {
                bags = (100 / packing) * qtl;
                txtBAGS.Text = bags.ToString();
            }
            else
            {
                txtBAGS.Text = bags.ToString();
            }

            if (saleRate == 0 && purcRate == 0)
            {
                lblDiff.Text = "0";
                lblDiff.Text = "0";
            }
            else
            {
                if (saleRate != 0)
                {
                    diff = saleRate - millRate;
                    diffAmt = Math.Round(diff * qtl, 2);
                    txtPURCHASE_RATE.Text = "";
                }
                else
                {
                    diff = millRate - purcRate;
                    diffAmt = Math.Round(diff * qtl, 2);
                    txtSALE_RATE.Text = "";
                }
            }
            // lblDiff.Text = diff.ToString();
            lblDiff.Text = diffAmt.ToString();
            txtRDiffTender.Text = diffAmt.ToString();

            string aaa = "";
            if (txtAC_CODE.Text.Trim() != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAC_CODE.Text);
                if (a == true)
                {
                    aaa = clsCommon.getString("select isnull(GSTStateCode,0) from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAC_CODE.Text + "");
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
            double resalecomm = 0.00;
            double taxable = 0.00;
            taxable = diffAmt + resale_comm;

            if (companyGstStateCode == partygstStateCode)
            {
                CGSTRateForPS = cgstrate;
                double cgsttaxAmountOnMR = Math.Round((taxable * cgstrate / 100), 2);
                //double cgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + cgsttaxAmountOnMR) * mill_rate)), 2);
                //double cgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - cgstExMillRate), 2);
                CGSTAmountForPS = Math.Round(cgsttaxAmountOnMR, 2);

                SGSTRateForPS = sgstrate;
                double sgsttaxAmountOnMR = Math.Round((taxable * sgstrate / 100), 2);
                //double sgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + sgsttaxAmountOnMR) * mill_rate)), 2);
                //double sgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - sgstExMillRate), 2);
                SGSTAmountForPS = Math.Round(sgsttaxAmountOnMR, 2);
            }
            else
            {
                IGSTRateForPS = igstrate;
                double igsttaxAmountOnMR = ((taxable) * igstrate / 100);
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

            if (taxable >= 0)
            {
                txtTaxableAmount.Text = taxable.ToString();
                //voucher_Amt = Math.Round(diffAmt + rDiffTender + postage + resale_comm + bank_comm + freight + other_expense + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS, 2);
                //txtVoucher_Amount.Text = voucher_Amt.ToString();
            }
            else
            {
                resalecomm = Convert.ToDouble(txtResale_Commisson.Text);
                // taxable = taxable + resalecomm;
                taxable = diffAmt + resalecomm;
                txtTaxableAmount.Text = taxable.ToString();
                //double tamnt = Convert.ToDouble(txtTaxableAmount.Text);
                //voucher_Amt = Math.Round(taxable + rDiffTender + postage + resale_comm + bank_comm + freight + other_expense + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS, 2);
                //txtVoucher_Amount.Text = voucher_Amt.ToString();
            }
            voucher_Amt = Math.Round(diffAmt + postage + resale_comm + bank_comm + 0 + other_expense + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS, 2);
            txtVoucher_Amount.Text = voucher_Amt.ToString();

            #endregion
            #region TCS
            double TCS_Rate = 0.000;
            double TCS_Amt = 0.00;
            double Bill_Amt = 0.00;
            double Net_Payable_Amt = 0.00;
            //if (txtTCSAmt.Text == string.Empty || txtTCSAmt.Text == "0")
            //{
            //    txtTCSAmt.Text = "0";
            //}
            //else
            //{
            //    TCS_Amt = Convert.ToDouble(txtTCSAmt.Text);
            //}
            TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
            Bill_Amt = Convert.ToDouble(txtVoucher_Amount.Text);
            if (TCS_Amt == 0)
            {
                TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
            }
            Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt), 2);


            txtTCSAmt.Text = TCS_Amt.ToString();
            txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
            #endregion



        }

        catch
        {
        }
    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:p('" + txtdoc_no.Text + "','" + "LN" + "')", true);
    }
    protected void txtDueDays_TextChanged(object sender, EventArgs e)
    {
        strTextBox = "txtDueDays";
        csCalculations();
    }
    protected void txtCommissionPerQntl_TextChanged(object sender, EventArgs e)
    {
        double commision = txtCommissionPerQntl.Text != string.Empty ? Convert.ToDouble(txtCommissionPerQntl.Text) : 0;
        double commamt = commision * Convert.ToDouble(txtQNTL.Text != string.Empty ? txtQNTL.Text : "0");
        txtResale_Commisson.Text = Convert.ToString(commamt);
        strTextBox = "txtCommissionPerQntl";
        csCalculations();
        setFocusControl(txtResale_Commisson);
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
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGSTRateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void onbeforeunload(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms1", "javascript:confirmExit('Email Sent Successfully!')", true);
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
                txtdoc_no.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                txtdoc_no.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(commissionid) as commissionid from " + tblHead + " "));
            if (counts == 0)
            {
                lblLV_Id.Text = "1";
                LV_Id = 1;
            }
            else
            {
                LV_Id = Convert.ToInt32(clsCommon.getString("SELECT max(commissionid) as commissionid from " + tblHead)) + 1;
                lblLV_Id.Text = LV_Id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);

        }
    }
    #endregion

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
}