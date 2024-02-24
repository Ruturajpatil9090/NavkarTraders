using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Text;
using System.Web.Services;
public partial class pgeCarporatesell : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string accountmasterlist = string.Empty;
    string partyunitTable = string.Empty;
    int defaultAccountCode = 0;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string Action = string.Empty;
    string cs = string.Empty;
    int id = 0;
    int flag = 0;
    int count = 0;
    int Detail_Id = 2;
    int Schedul_date = 3;
    int Schedul_Quantal = 4;
    int Transit_daye = 5;
    int Reminde_date = 6;
    int GrDetali_id = 7;
    int Rowaction = 8;
    int Srno = 9;
    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    #endregion
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    #region Head Declaration Field
    string DeliveryType = string.Empty;
    int Company_Code = 0;
    int Year_Code = 0;
    int Branch_Code = 0;
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    Int32 DOC_NO = 0;
    Int32 Cor_Id = 0;
    string DOC_DATE = string.Empty;
    Int32 AC_CODE = 0;
    Int32 UNIT_CODE = 0;
    string PODETAIL = string.Empty;
    double QUANTAL = 0.00;
    double SELL_RATE = 0.00;
    string ASN_NO = string.Empty;
    string Selling_Type = string.Empty;
    int Broker = 0;
    int Ac_id = 0;
    int Unit_id = 0;
    int broker_id = 0;
    int Bill_To = 0;
    int bt = 0;
    string Head_Insert = string.Empty;
    StringBuilder Head_Update = null;
    string Head_Delete = string.Empty;
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    #endregion

    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    #region Detail part Declaration
    string GrdSheduledate = string.Empty;
    double GrdQuntal = 0.00;
    int GrdTransitDay = 0;
    string Reminddate = string.Empty;
    int ID = 0;
    string Detail_Insert = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "carporatehead";
            tblDetails = "carporatedetail";
            AccountMasterTable = tblPrefix + "AccountMaster";
            qryCommon = "qrycarporateheaddetail";
            pnlPopup.Style["display"] = "none";
            accountmasterlist = tblPrefix + "qryAccountsList";
            partyunitTable = tblPrefix + "PartyUnit";
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
                        hdnf.Value = Request.QueryString["carpid"];
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


                        //double qntl = 0;
                        //double mainqntl = Convert.ToDouble(txtquantal.Text);
                        //lblQntlDiff.Text = Convert.ToString(QntlDiff(qntl, mainqntl));
                        setFocusControl(txtdoc_date);
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
            counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            if (counts == 0)
            {
                txtdoc_no.Text = "1";
                DOC_NO = 1;

            }
            else
            {
                DOC_NO = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as carpid from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
                txtdoc_no.Text = DOC_NO.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(carpid),0) as carpid from " + tblHead + " "));
            if (counts == 0)
            {
                id = 1;
                lblCor_Id.Text = "1";

            }
            else
            {
                id = Convert.ToInt32(clsCommon.getString("SELECT max(carpid) as carpid from " + tblHead)) + 1;
                lblCor_Id.Text = id.ToString();
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
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                //btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                txtEditDoc_No.Enabled = true;
                #region set Business logic for save
                setFocusControl(txtdoc_date);
                lblParty_name.Text = string.Empty;
                lblUnit_name.Text = string.Empty;
                btntxtac_code.Enabled = true;
                btntxtunit_code.Enabled = false;
                calenderExtendertxtdoc_date.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                drpSellingType.Enabled = false;
                btntxtBroker.Enabled = false;
                txtBill_To.Enabled = false;
                btntxtbill_To.Enabled = false;
                btntxtac_code.Enabled = false;
                txtSCDate.Enabled = false;
                txtSCQuantal.Enabled = false;
                txtTransitDays.Enabled = false;
                txtRemindDate.Enabled = false;
                #endregion

                txtSCDate.Enabled = false;
                txtSCQuantal.Enabled = false;
                txtTransitDays.Enabled = false;
                txtRemindDate.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                drpDeliveryType.Enabled = false;
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
                lblMsg.Text = string.Empty;
                txtEditDoc_No.Enabled = false;
                #region set Business logic for save
                setFocusControl(txtdoc_date);
                lblParty_name.Text = string.Empty;
                lblUnit_name.Text = string.Empty;
                lblBroker.Text = string.Empty;
                txtdoc_no.Enabled = false;
                btntxtac_code.Enabled = true;
                btntxtunit_code.Enabled = true;
                calenderExtendertxtdoc_date.Enabled = true;
                txtdoc_date.Text = DateTime.Now.ToString("dd/MM/yyyy");// clsCommon.getString("select Convert(varchar(10),getDate(),103) as doc_date");
                btnOpenDetailsPopup.Enabled = true;
                btntxtBroker.Enabled = true;
                pnlgrdDetail.Enabled = true;
                drpSellingType.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                txtBill_To.Enabled = true;
                btntxtbill_To.Enabled = true;

                txtSCDate.Enabled = true;
                txtSCQuantal.Enabled = true;
                txtTransitDays.Enabled = true;
                txtRemindDate.Enabled = true;
                #endregion

                txtSCDate.Enabled = true;
                txtSCQuantal.Enabled = true;
                txtTransitDays.Enabled = true;
                txtRemindDate.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                lblBill_To.Text = string.Empty;
                drpDeliveryType.Enabled = true;
                txtBroker.Text = "2";
                hdnfbk.Value = Session["SELFID"].ToString();

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
                #region logic
                btntxtac_code.Enabled = false;
                btntxtunit_code.Enabled = false;
                calenderExtendertxtdoc_date.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                drpSellingType.Enabled = false;
                btntxtBroker.Enabled = false;
                txtBill_To.Enabled = false;
                btntxtbill_To.Enabled = false;

                txtSCDate.Enabled = false;
                txtSCQuantal.Enabled = false;
                txtTransitDays.Enabled = false;
                txtRemindDate.Enabled = false;
                #endregion

                txtSCDate.Enabled = false;
                txtSCQuantal.Enabled = false;
                txtTransitDays.Enabled = false;
                txtRemindDate.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                drpDeliveryType.Enabled = false;
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
                txtdoc_no.Enabled = false;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                // txtEditDoc_No.Enabled = false;
                #region logic
                btntxtac_code.Enabled = true;
                btntxtunit_code.Enabled = true;
                btntxtBroker.Enabled = true;
                calenderExtendertxtdoc_date.Enabled = true;
                btnOpenDetailsPopup.Enabled = true;
                pnlgrdDetail.Enabled = true;
                drpSellingType.Enabled = true;
                txtBill_To.Enabled = true;
                btntxtbill_To.Enabled = true;

                txtSCDate.Enabled = true;
                txtSCQuantal.Enabled = true;
                txtTransitDays.Enabled = true;
                txtRemindDate.Enabled = true;
                #endregion

                txtSCDate.Enabled = true;
                txtSCQuantal.Enabled = true;
                txtTransitDays.Enabled = true;
                txtRemindDate.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                drpDeliveryType.Enabled = true;
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
        if (txtdoc_no.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY doc_no asc  ";
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY doc_no asc  ";
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                string query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) +
                    "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    "   ORDER BY Doc_No DESC  ";
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
                string query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) +
                   "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                   "    ORDER BY Doc_No asc  ";
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MAX(doc_no) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
        lblQntlDiff.Text = "0";
        this.NextNumber();
        //   pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");

        this.makeEmptyForm("E");
        //string docNo = txtdoc_no.Text;
        //string CSNo = clsCommon.getString("Select Carporate_Sale_No from " + tblPrefix + "deliveryorder where Carporate_Sale_No=" + docNo + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
        //if (docNo == CSNo)
        //{
        //    drpSellingType.Enabled = false;
        //}

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
        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(carpid),0) as id from " + tblHead + " where Company_Code=" + Session["Company_Code"].ToString() + " "));

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
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }


                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();

                        hdnf.Value = dt.Rows[0]["carpid"].ToString();
                        txtdoc_no.Text = dt.Rows[0]["DOC_NO"].ToString();
                        hdnfcarpdoc.Value = txtdoc_no.Text;
                        lblCor_Id.Text = hdnf.Value;
                        hdnfcarpid.Value = lblCor_Id.Text;
                        txtdoc_date.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtac_code.Text = dt.Rows[0]["AC_CODE"].ToString();
                        lblParty_name.Text = dt.Rows[0]["carporatepartyaccountname"].ToString();
                        txtBill_To.Text = dt.Rows[0]["bill_to"].ToString();
                        lblBill_To.Text = dt.Rows[0]["carporatebilltoname"].ToString();
                        lblPartyCommission.Text = "Comm.Rate: " + dt.Rows[0]["Commission"].ToString();
                        txtunit_code.Text = dt.Rows[0]["unit_code"].ToString();
                        lblUnit_name.Text = dt.Rows[0]["carporatepartyunitname"].ToString();
                        txtpodetail.Text = dt.Rows[0]["pono"].ToString();
                        txtquantal.Text = dt.Rows[0]["quantal"].ToString();
                        txtsell_rate.Text = dt.Rows[0]["sell_rate"].ToString();
                        txtCommision.Text = dt.Rows[0]["CommissionRate"].ToString();
                        txtasn_no.Text = dt.Rows[0]["remark"].ToString();
                        txtBroker.Text = dt.Rows[0]["broker"].ToString();
                        lblBroker.Text = dt.Rows[0]["carporatepartybrokername"].ToString();
                        drpSellingType.SelectedValue = dt.Rows[0]["selling_type"].ToString();
                        drpDeliveryType.SelectedValue = dt.Rows[0]["DeliveryType"].ToString();
                        hdnfac.Value = dt.Rows[0]["ac"].ToString();
                        hdnfuc.Value = dt.Rows[0]["uc"].ToString();
                        hdnfbk.Value = dt.Rows[0]["br"].ToString();
                        hdnfbt.Value = dt.Rows[0]["bt"].ToString();

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
                        //  hdnf.Value = txtdoc_no.Text;
                        recordExist = true;
                        lblMsg.Text = "";
                        //pnlgrdDetail.Enabled = false;
                    }
                }
            }

            #region Grid Details
            qry = "select detail_Id as ID,  schedule_dateConverted as Schedule_Date  ,scheduale_qntl as Schedule_Quantal,transit_days as Transit_Days,convert(varchar(10), DATEADD(day, -transit_days, Schedule_Date ),103)  as Remind_Date, carpdetailid as autoIncrementDeId from qrycarporateheaddetail where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and carpid=" + hdnf.Value;
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string idValue = dt.Rows[0]["ID"].ToString();
                        if (idValue == "")  //blank Row
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        else
                        {
                            dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                            dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["rowAction"] = "N";
                                dt.Rows[i]["SrNo"] = i + 1;
                            }
                            //dt.Columns["ID"].SetOrdinal(8);
                            //dt.Columns["SrNo"].SetOrdinal(2);
                            grdDetail.DataSource = dt;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = dt;

                            double diffAmmt = 0.00;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                diffAmmt = diffAmmt + Convert.ToDouble(dt.Rows[i]["Schedule_Quantal"].ToString());
                            }
                            diffAmmt = Convert.ToDouble(txtquantal.Text) - diffAmmt;
                            lblQntlDiff.Text = diffAmmt.ToString();
                        }
                    }
                }
            }
            #endregion

            //  this.enableDisableNavigateButtons();

            return recordExist;
        }
        catch
        {
            return false;
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
                int Carp_id = Convert.ToInt32(clsCommon.getString("select carpid from carporatehead where doc_no=" + txtEditDoc_No.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " "));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:CarporateOPen('" + Carp_id + "')", true);

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + qryCommon + " where carpid=" + hdnf.Value + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
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
                double qntl = 0;
                double mainqntl = Convert.ToDouble(txtquantal.Text);
                lblQntlDiff.Text = Convert.ToString(QntlDiff(qntl, mainqntl));
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

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int i = 0;
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(6);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(7);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(12);
                e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[7].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[9].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].Visible = true;

                i++;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string s = cell.Text.ToString();
                    if (cell.Text.Length > 31)
                    {
                        cell.Text = cell.Text.Substring(0, 33) + "...";
                        cell.ToolTip = s;
                    }
                }
            }
        }
        catch
        {
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
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[7].Text != "R")
                        {
                            //  pnlPopupDetails.Style["display"] = "block";
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
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        // pnlPopupDetails.Style["display"] = "block";
        setFocusControl(txtSCDate);
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(clsCommon.getString("select COALESCE(MAX(detail_Id),0)+1 from " + tblDetails + " where carpid=" + lblCor_Id.Text + " "));
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            bool isValidated = true;
            if (ViewState["currentTable"] != null)
            {
                if (txtSCDate.Text != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtSCDate);
                    return;
                }
                if (txtSCQuantal.Text != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtSCQuantal);
                    return;
                }
                if (txtTransitDays.Text != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtTransitDays);
                    return;
                }
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
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;

                        #region decide whether actual row is updating or virtual [rowAction]
                        //  string id = clsCommon.getString("select carpdetailid from " + tblDetails + " where  carpdetailid='" + hdnf.Value + "'");
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where detail_Id=" + rowIndex + " and carpid=" + lblCor_Id.Text + "");
                        if (id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }

                        //if (id == "1" && ViewState["mode"].ToString() == "I")
                        //{
                        //    temp = "1";
                        //}
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Schedule_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Schedule_Quantal", typeof(double))));
                    dt.Columns.Add((new DataColumn("Transit_Days", typeof(int))));
                    dt.Columns.Add((new DataColumn("Remind_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("autoIncrementDeId", typeof(Int32))));


                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
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
                dt.Columns.Add((new DataColumn("Schedule_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("Schedule_Quantal", typeof(double))));
                dt.Columns.Add((new DataColumn("Transit_Days", typeof(int))));
                dt.Columns.Add((new DataColumn("Remind_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("autoIncrementDeId", typeof(Int32))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            if (txtSCDate.Text != string.Empty)
            {
                try
                {
                    dr["Schedule_Date"] = DateTime.Parse(txtSCDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                }
                catch
                {
                    txtSCDate.Text = "";
                    setFocusControl(txtSCDate);
                }
            }
            else
            {
                setFocusControl(txtSCDate);
            }

            if (txtSCQuantal.Text != string.Empty)
            {
                dr["Schedule_Quantal"] = txtSCQuantal.Text;
            }
            else
            {
                setFocusControl(txtSCQuantal);
            }

            if (txtTransitDays.Text != string.Empty)
            {
                dr["Transit_Days"] = txtTransitDays.Text;
            }
            else
            {
                setFocusControl(txtTransitDays);
            }
            dr["Remind_Date"] = txtRemindDate.Text;
            if (btnAdddetails.Text == "ADD")
            {
                dr["autoIncrementDeId"] = 0;
                dt.Rows.Add(dr);
            }
            #endregion
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
                // pnlgrdDetail.Style["display"] = "block";
                setFocusControl(txtSCDate);
            }
            else
            {
                // pnlgrdDetail.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            btnAdddetails.Text = "ADD";
            // Empty Code->
            txtSCDate.Text = string.Empty;
            txtSCQuantal.Text = string.Empty;
            txtTransitDays.Text = string.Empty;
            txtRemindDate.Text = string.Empty;
            double qntl = 0;
            double mainqntl = Convert.ToDouble(txtquantal.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["rowAction"].ToString() != "D")
                {
                    qntl = qntl + Convert.ToDouble(dt.Rows[i]["Schedule_Quantal"].ToString());
                }
            }
            // qntl = QntlDiff(qntl, mainqntl);
            qntl = mainqntl - qntl;
            lblQntlDiff.Text = Convert.ToString(qntl);
            setFocusControl(txtSCDate);
        }
        catch
        {
        }
    }

    private double QntlDiff(double qntl, double mainqntl)
    {
        double diff = 0.00;
        if (grdDetail.Rows.Count > 0)
        {
            for (int k = 0; k < grdDetail.Rows.Count; k++)
            {
                double a = Convert.ToDouble(Server.HtmlDecode(grdDetail.Rows[k].Cells[4].Text.ToString()));
                qntl += a;
                diff = mainqntl - qntl;
            }
        }
        return diff;
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        txtSCDate.Text = string.Empty;
        txtSCQuantal.Text = string.Empty;
        txtTransitDays.Text = string.Empty;
        txtRemindDate.Text = string.Empty;
        btnAdddetails.Text = "ADD";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gridViewRow)
    {

        lblNo.Text = Server.HtmlDecode(gridViewRow.Cells[9].Text);
        lblID.Text = Server.HtmlDecode(gridViewRow.Cells[2].Text);
        txtSCDate.Text = Server.HtmlDecode(gridViewRow.Cells[3].Text);
        txtSCQuantal.Text = Server.HtmlDecode(gridViewRow.Cells[4].Text);
        txtTransitDays.Text = Server.HtmlDecode(gridViewRow.Cells[5].Text);
        txtRemindDate.Text = Server.HtmlDecode(gridViewRow.Cells[6].Text);
        setFocusControl(txtSCDate);
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
                string IDExisting = clsCommon.getString("select detail_Id from carporatedetail where detail_Id=" + ID + " and carpid=" + hdnf.Value + "");
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
                        grdDetail.Rows[rowIndex].Cells[8].Text = "U";
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
            if (v == "txtdoc_no")
            {
                e.Row.Cells[0].Width = new Unit("80px");
                e.Row.Cells[1].Width = new Unit("150px");
                e.Row.Cells[2].Width = new Unit("300px");
                e.Row.Cells[3].Width = new Unit("300px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                e.Row.Cells[0].Width = new Unit("80px");
                e.Row.Cells[1].Width = new Unit("400px");
                e.Row.Cells[2].Width = new Unit("250px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
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


    #region [txtSCDate_TextChanged]
    protected void txtSCDate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSCDate.Text;
        strTextBox = "txtSCDate";
        csCalculations();
    }
    #endregion

    #region [txtSCQuantal_TextChanged]
    protected void txtSCQuantal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSCQuantal.Text;
        strTextBox = "txtSCQuantal";
        csCalculations();
    }
    #endregion

    #region [txtTransitDays_TextChanged]
    protected void txtTransitDays_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransitDays.Text;
        strTextBox = "txtTransitDays";
        csCalculations();
    }
    #endregion

    #region [txtRemindDate_TextChanged]
    protected void txtRemindDate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransitDays.Text;
        strTextBox = "txtTransitDays";
        csCalculations();
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

    #region [btntxtdoc_no_Click]
    protected void btntxtdoc_no_Click(object sender, EventArgs e)
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

    #region [txtdoc_date_TextChanged]
    protected void txtdoc_date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_date.Text;
        strTextBox = "txtdoc_date";
        csCalculations();
    }
    #endregion

    #region [btntxtdoc_date_Click]
    protected void btntxtdoc_date_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtdoc_date";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtac_code_TextChanged]
    protected void txtac_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtac_code.Text;
        strTextBox = "txtac_code";
        csCalculations();
    }
    #endregion

    #region [btntxtac_code_Click]
    protected void btntxtac_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtac_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtunit_code_TextChanged]
    protected void txtunit_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtunit_code.Text;
        strTextBox = "txtunit_code";
        csCalculations();
    }
    #endregion

    #region [btntxtunit_code_Click]
    protected void btntxtunit_code_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtac_code.Text != string.Empty)
            //{
            //    lblMsg.Text = "";
            //    pnlPopup.Style["display"] = "block";
            //    hdnfClosePopup.Value = "txtunit_code";
            //    btnSearch_Click(sender, e);
            //}
            //else
            //{
            //    lblMsg.Text = "Please enter party code first";

            //    setFocusControl(txtac_code);
            //}
            lblMsg.Text = "";
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtunit_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtpodetail_TextChanged]
    protected void txtpodetail_TextChanged(object sender, EventArgs e)
    {
        searchString = txtpodetail.Text;
        strTextBox = "txtpodetail";
        csCalculations();
    }
    #endregion

    #region [txtquantal_TextChanged]
    protected void txtquantal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtquantal.Text;
        strTextBox = "txtquantal";
        csCalculations();
    }
    #endregion

    #region [txtsell_rate_TextChanged]
    protected void txtsell_rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtsell_rate.Text;
        strTextBox = "txtsell_rate";
        csCalculations();
    }
    #endregion
    #region [txtCommision]
    protected void txtCommision_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCommision.Text;
        strTextBox = "txtCommision";
        csCalculations();
    }
    #endregion

    #region [txtasn_no_TextChanged]
    protected void txtasn_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtasn_no.Text;
        strTextBox = "txtasn_no";
        csCalculations();
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
            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtdoc_no.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtdoc_no.Text = string.Empty;
                    txtdoc_no.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtdoc_no);
                    hdnfClosePopup.Value = "Close";
                }

                if (btntxtdoc_no.Text == "Choose No" || hdnfClosePopup.Value == "txtEditDoc_No")
                {
                    lblPopupHead.Text = "--Select Doc No--";
                    string qry = "select Doc_No,Doc_Date,partyName,UnitName from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (Doc_No like '%" + txtSearchText.Text + "%' or Doc_Date like '%" + txtSearchText.Text + "%' or partyName like '%" + txtSearchText.Text + "%' or Unit_name like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtac_code")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or CityName '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Bank--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from  qrymstaccountmaster  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and Carporate_Party='Y' and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);
                string qry = " Locked=0  and Carporate_Party='Y' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                      + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtBroker")
            {
                lblPopupHead.Text = "--Select Party--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);
                string qry = " Locked=0  and Carporate_Party='Y' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                   + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            
            }

            if (hdnfClosePopup.Value == "txtBill_To")
            {
                lblPopupHead.Text = "--Select Party--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    "  and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);
                string qry = " Locked=0  and Carporate_Party='Y' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            
            }
            if (hdnfClosePopup.Value == "txtunit_code")
            {
                //if (txtac_code.Text != string.Empty)
                //{
                //    lblMsg.Text = "";
                //    lblPopupHead.Text = "--Select Unit--";
                //    string qry = "select  Unit_name as Unit_Code,unitname,Ac_Code from qrypartyunit where Ac_Code=" + txtac_code.Text +
                //        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (unit_code like '%" + txtSearchText.Text +
                //        "%' or unitname like '%" + txtSearchText.Text + "%' ) order by unitname";
                //    this.showPopup(qry);
                //}
                //else
                //{
                //    lblMsg.Text = "Please enter party code first";
                //    setFocusControl(txtac_code);
                //}

                lblPopupHead.Text = "--Select Unit--";
                //string qry = "select  Unit_name as Unit_Code,unitname,Ac_Code from qrypartyunit where Ac_Code=" + txtac_code.Text +
                //    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (unit_code like '%" + txtSearchText.Text +
                //   "%' or unitname like '%" + txtSearchText.Text + "%' ) order by unitname";
                //this.showPopup(qry);
                string qry = " Locked=0  and Carporate_Party='Y' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                      + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
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
                        setFocusControl(txtSearchText);
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
            if (hdnfClosePopup.Value == "txtac_code")
            {
                setFocusControl(txtac_code);
            }
            if (hdnfClosePopup.Value == "txtunit_code")
            {
                setFocusControl(txtunit_code);
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
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "carporateSell", "javascript:pagevalidation();", true);


    }
    #endregion


    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }


    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtdoc_no")
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

                            string qry = "select * from " + tblHead + " where  Doc_No='" + txtValue + "' " +
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
                                        //   hdnf.Value = dt.Rows[0]["Doc_No"].ToString();

                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** unit_code (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                //txtdoc_no.Enabled = false;
                                                //  hdnf.Value = txtdoc_no.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                setFocusControl(txtac_code);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                hdnf.Value = txtdoc_no.Text;
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = true;
                                                    setFocusControl(txtac_code);
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtac_code);
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
            if (strTextBox == "txtdoc_date")
            {
                //if (txtdoc_date.Text != string.Empty)
                //{
                //    string dt = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                //    if (clsCommon.isValidDate(dt) == true)
                //    {
                //        setFocusControl(txtac_code);
                //    }
                //    else
                //    {
                //        txtdoc_date.Text = string.Empty;
                //        setFocusControl(txtdoc_date);
                //    }
                //}
                //else
                //{
                //    setFocusControl(txtdoc_date);
                //}
                setFocusControl(txtac_code);
            }
            if (strTextBox == "txtac_code")
            {
                string acname = "";
                if (txtac_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtac_code.Text);
                    if (a == false)
                    {
                        btntxtac_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster  where Ac_Code=" + txtac_code.Text + " and Carporate_Party='Y'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnfac.Value = clsCommon.getString("select accoid from qrymstaccountmaster  where Ac_Code=" + txtac_code.Text + " and Carporate_Party='Y'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (drpSellingType.SelectedValue == "PS")
                            {
                                txtBill_To.Text = txtac_code.Text;
                                txtunit_code.Text = txtac_code.Text;
                                lblBill_To.Text = acname;
                                lblUnit_name.Text = acname;
                            }
                            lblParty_name.Text = acname;
                            if (drpDeliveryType.SelectedValue == "C")
                            {
                                lblPartyCommission.Text = "Comm.Rate: " + clsCommon.getString("select Commission from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + " and Carporate_Party='Y'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                txtCommision.Text = clsCommon.getString("select Commission from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + " and Carporate_Party='Y'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            }
                            setFocusControl(txtBill_To);
                        }
                        else
                        {
                            txtac_code.Text = string.Empty;
                            lblParty_name.Text = acname;
                            setFocusControl(txtac_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtac_code);
                }
            }
            if (strTextBox == "txtBill_To")
            {
                if (txtBill_To.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBill_To.Text);
                    if (a == false)
                    {
                        // btntxtBill_To_Click(this, new EventArgs());
                        btntxtbill_To_Click(this, new EventArgs());
                    }
                    else
                    {
                        string billto = "";
                        if (txtBill_To.Text != string.Empty)
                        {
                            billto = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Ac_Code=" + txtBill_To.Text + "");
                            if (billto != string.Empty && billto != "0")
                            {
                                hdnfbt.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Ac_Code=" + txtBill_To.Text + "");

                                lblBill_To.Text = billto;
                                setFocusControl(txtunit_code);
                            }
                            else
                            {
                                lblBill_To.Text = string.Empty;
                                txtBill_To.Text = string.Empty;
                                setFocusControl(txtBill_To);
                            }
                        }
                        else
                        {
                            setFocusControl(txtBill_To);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBill_To);
                }
            }

            if (strTextBox == "txtunit_code")
            {
                if (txtunit_code.Text != string.Empty)
                {
                    string unitname = "";
                    if (txtunit_code.Text != string.Empty)
                    {
                        bool a = clsCommon.isStringIsNumeric(txtunit_code.Text);
                        if (a == false)
                        {
                            btntxtunit_code_Click(this, new EventArgs());
                        }
                        else
                        {
                            unitname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtunit_code.Text + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (unitname != string.Empty && unitname != "0")
                            {
                                hdnfuc.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtunit_code.Text + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                lblUnit_name.Text = unitname;
                                setFocusControl(txtBroker);
                            }
                            else
                            {
                                txtunit_code.Text = string.Empty;
                                lblUnit_name.Text = unitname;
                                setFocusControl(txtunit_code);
                            }
                        }
                    }
                    else
                    {
                        setFocusControl(txtunit_code);
                    }
                }
                else
                {
                    lblMsg.Text = "Please enter party code first";
                    setFocusControl(txtac_code);
                }
            }
            if (strTextBox == "txtBroker")
            {
                if (txtBroker.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBroker.Text);
                    if (a == false)
                    {
                        btntxtBroker_Click(this, new EventArgs());
                    }
                    else
                    {
                        string broker = "";
                        if (txtBroker.Text != string.Empty)
                        {
                            broker = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtBroker.Text + "");
                            if (broker != string.Empty && broker != "0")
                            {
                                hdnfbk.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtBroker.Text + "");

                                lblBroker.Text = broker;
                                setFocusControl(txtpodetail);
                            }
                        }
                        else
                        {
                            setFocusControl(txtpodetail);
                        }
                    }
                }
                else
                {
                    setFocusControl(drpSellingType);
                }
            }
            if (strTextBox == "txtpodetail")
            {
                setFocusControl(txtquantal);
            }
            if (strTextBox == "txtquantal")
            {
                setFocusControl(txtCommision);
            }
            if (strTextBox == "txtCommision")
            {
                setFocusControl(txtsell_rate);
            }
            if (strTextBox == "txtsell_rate")
            {
                setFocusControl(txtasn_no);
            }
            if (strTextBox == "txtasn_no")
            {
                setFocusControl(txtSCDate);
            }
            if (strTextBox == "txtSCDate")
            {
                if (txtSCDate.Text != string.Empty)
                {
                    try
                    {
                        string scdate = DateTime.Parse(txtSCDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                        DateTime d = DateTime.Parse(txtSCDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                        if (txtTransitDays.Text != string.Empty)
                        {
                            d = d.AddDays(-(Convert.ToInt32(txtTransitDays.Text)));
                            txtRemindDate.Text = d.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            setFocusControl(txtSCQuantal);
                        }
                    }
                    catch
                    {
                        txtSCDate.Text = string.Empty;
                    }
                }
                else
                {

                }
            }
            if (strTextBox == "txtSCQuantal")
            {
                setFocusControl(txtTransitDays);
            }
            if (strTextBox == "txtTransitDays")
            {
                DateTime d = DateTime.Parse(txtSCDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                d = d.AddDays(-(Convert.ToInt32(txtTransitDays.Text)));
                txtRemindDate.Text = d.ToString("dd/MM/yyyy");
                setFocusControl(btnAdddetails);
            }
            //if (strTextBox == "txtdt1")
            //{
            //    try
            //    {
            //        if (txtdt1.Text != string.Empty)
            //        {
            //            DateTime d = DateTime.Parse(txtdt1.Text);
            //            DateTime docdate = DateTime.Parse(txtdoc_date.Text);

            //            string dt1 = DateTime.Parse(txtdt1.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //            if (clsCommon.isValidDate(dt1) && d > docdate)
            //            {
            //                setFocusControl(txtqtl1);
            //            }
            //            else
            //            {
            //                txtdt1.Text = string.Empty;
            //                setFocusControl(txtdt1);
            //            }

            //        }
            //        else
            //        {
            //            setFocusControl(txtdt1);
            //        }
            //    }
            //    catch
            //    {
            //        txtdt1.Text = string.Empty;
            //        setFocusControl(txtdt1);
            //    }
            //}

            //if (strTextBox == "txtdt2")
            //{
            //    try
            //    {
            //        if (txtdt2.Text != string.Empty)
            //        {
            //            DateTime d = DateTime.Parse(txtdt2.Text);
            //            DateTime docdate = DateTime.Parse(txtdoc_date.Text);

            //            string dt2 = DateTime.Parse(txtdt2.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //            if (clsCommon.isValidDate(dt2) && d > docdate)
            //            {
            //                setFocusControl(txtqtl2);
            //            }
            //            else
            //            {
            //                txtdt2.Text = string.Empty;
            //                setFocusControl(txtdt2);
            //            }

            //        }
            //        else
            //        {
            //            setFocusControl(txtdt2);
            //        }
            //    }
            //    catch
            //    {
            //        txtdt2.Text = string.Empty;
            //        setFocusControl(txtdt2);
            //    }
            //}
            //if (strTextBox == "txtdt3")
            //{
            //    try
            //    {
            //        if (txtdt3.Text != string.Empty)
            //        {
            //            DateTime d = DateTime.Parse(txtdt3.Text);
            //            DateTime docdate = DateTime.Parse(txtdoc_date.Text);

            //            string dt3 = DateTime.Parse(txtdt3.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //            if (clsCommon.isValidDate(dt3) && d > docdate)
            //            {
            //                setFocusControl(txtqtl3);
            //            }
            //            else
            //            {
            //                txtdt3.Text = string.Empty;
            //                setFocusControl(txtdt3);
            //            }

            //        }
            //        else
            //        {
            //            setFocusControl(txtdt3);
            //        }
            //    }
            //    catch
            //    {
            //        txtdt3.Text = string.Empty;
            //        setFocusControl(txtdt3);
            //    }
            //}

            //if (strTextBox == "txtdt4")
            //{
            //    try
            //    {
            //        if (txtdt4.Text != string.Empty)
            //        {
            //            DateTime d = DateTime.Parse(txtdt4.Text);
            //            DateTime docdate = DateTime.Parse(txtdoc_date.Text);

            //            string dt4 = DateTime.Parse(txtdt4.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //            if (clsCommon.isValidDate(dt4) && d > docdate)
            //            {
            //                setFocusControl(txtqtl4);
            //            }
            //            else
            //            {
            //                txtdt4.Text = string.Empty;
            //                setFocusControl(txtdt4);
            //            }

            //        }
            //        else
            //        {
            //            setFocusControl(txtdt4);
            //        }
            //    }
            //    catch
            //    {
            //        txtdt4.Text = string.Empty;
            //        setFocusControl(txtdt4);
            //    }
            //}

            //if (strTextBox == "txtdt5")
            //{
            //    try
            //    {
            //        if (txtdt5.Text != string.Empty)
            //        {
            //            DateTime d = DateTime.Parse(txtdt5.Text);
            //            DateTime docdate = DateTime.Parse(txtdoc_date.Text);

            //            string dt5 = DateTime.Parse(txtdt5.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //            if (clsCommon.isValidDate(dt5) && d > docdate)
            //            {
            //                setFocusControl(txtqtl5);
            //            }
            //            else
            //            {
            //                txtdt5.Text = string.Empty;
            //                setFocusControl(txtdt5);
            //            }

            //        }
            //        else
            //        {
            //            setFocusControl(txtdt5);
            //        }
            //    }
            //    catch
            //    {
            //        txtdt5.Text = string.Empty;
            //        setFocusControl(txtdt5);
            //    }
            //}

            //if (strTextBox == "txtqtl1")
            //{
            //    setFocusControl(txtdt2);
            //}
            //if (strTextBox == "txtqtl2")
            //{
            //    setFocusControl(txtdt3);
            //}
            //if (strTextBox == "txtqtl3")
            //{
            //    setFocusControl(txtdt4);
            //}
            //if (strTextBox == "txtqtl4")
            //{
            //    setFocusControl(txtdt5);
            //}
            //if (strTextBox == "txtqtl5")
            //{
            //    setFocusControl(btnSave);
            //}

            #region Calculation Part

            //double qtlTotal = 0.00;
            //double qtl1 = txtqtl1.Text != string.Empty ? Convert.ToDouble(txtqtl1.Text) : 0.00;
            //double qtl2 = txtqtl2.Text != string.Empty ? Convert.ToDouble(txtqtl2.Text) : 0.00;
            //double qtl3 = txtqtl3.Text != string.Empty ? Convert.ToDouble(txtqtl3.Text) : 0.00;
            //double qtl4 = txtqtl4.Text != string.Empty ? Convert.ToDouble(txtqtl4.Text) : 0.00;
            //double qtl5 = txtqtl5.Text != string.Empty ? Convert.ToDouble(txtqtl5.Text) : 0.00;

            //qtlTotal = Math.Round(qtl1 + qtl2 + qtl3 + qtl4 + qtl5, 2);

            //txtqtlTotal.Text = qtlTotal.ToString();

            #endregion

        }
        catch
        {
        }
    }
    #endregion



    protected void drpSellingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(drpSellingType);
    }
    protected void txtBroker_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBroker.Text;
        strTextBox = "txtBroker";
        csCalculations();
    }
    protected void btntxtBroker_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtBroker.Text != string.Empty)
            //{
            lblMsg.Text = "";
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
    }
    protected void txtBill_To_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To.Text;
        strTextBox = "txtBill_To";
        csCalculations();
    }
    protected void btntxtbill_To_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtBroker.Text != string.Empty)
            //{
            lblMsg.Text = "";
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBill_To";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
    }
    protected void drpDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDeliveryType.SelectedValue == "C")
        {
            txtCommision.Enabled = true;
        }
        else
        {
            txtCommision.Enabled = false;
        }
        setFocusControl(drpDeliveryType);
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

}
