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
public partial class pgeDebitCreditNote : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string sub_type = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    int Detail_Id = 2;
    int Ac_Code = 3;
    int AC_codeName = 4;

    //  int value = 5;
    int Rowaction = 11;
    //   int Srno = 7;
    int dcDetail_id = 10;

    string fornotsaverecord;
    #endregion

    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    string cs = string.Empty;
    string Action = string.Empty;
    //int Tender_No = 0;
    int id = 0;
    int flag = 0;
    int count = 0;

    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    string msg = string.Empty;
    string SystemMasterTable = string.Empty;

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;

    #region Head Declaration Field
    Int32 Doc_No = 0;
    Int32 Entry_no = 0;
    Int32 EntryId = 0;
    string type = string.Empty;
    string Entry_date = string.Empty;
    Int32 Ac_code = 0;
    string Ac_name = string.Empty;
    Int32 Bill_No = 0;
    string Bill_date = string.Empty;
    Int32 Bill_Id = 0;
    Int32 Gst_Code = 0;
    string GstName = string.Empty;
    string Ac_Id = string.Empty;
    float Gst_Rate = 0;
    double TaxableAmount = 0.00;
    double CgstRate = 0.00;
    double CgstAmount = 0.00;
    double SgstRate = 0.00;
    double SgstAmount = 0.00;
    double IgstRate = 0.00;
    double IgstAmount = 0.00;
    double MiscAmount = 0.00;
    double FinalAmount = 0.00;

    string Head_Insert = string.Empty;
    StringBuilder Head_Update = null;
    string Head_Delete = string.Empty;
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    #endregion
    #region Detail Declare Field
    string Detail_Insert = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;
    Int32 ExpAccode = 0;
    Int32 DetailId = 0;
    string Expname = string.Empty;
    double value = 0;


    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "debitnotehead";
            tblDetails = "debitnotedetail";
            qryCommon = "qrydebitnoteheaddetail";
            SystemMasterTable = tblPrefix + "SystemMaster";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            sub_type = drpSub_Type.SelectedValue;
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
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    hdnfTran_type.Value = Request.QueryString["tran_type"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["dcid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        pnlgrdDetail.Enabled = true;
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        //this.DebitCreditDiff();
                        this.showLastRecord();
                        setFocusControl(drpSub_Type);
                        
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        txtGST_Rate_Code.Text = "1";
                        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        lblGST_Rate_Code.Text = gstname;
                        setFocusControl(txtDoc_Date);
                    }

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            //if (objAsp != null)
            //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);

            if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
            {
                pnlPopup.Style["display"] = "none";
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                objAsp = btnSearch;
                //if (objAsp != null)
            }
        }
        catch
        {
            //  Response.Redirect("http://localhost:3994/HomePage/pgeloginForm.aspx");
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

                txtBillNo.Enabled = false;
                pnlPopup.Style["display"] = "none";
                ViewState["currentTable"] = null;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                btntxtBillNo.Enabled = false;
                pnlgrdDetail.Enabled = false;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;

                txtDoc_Date.Enabled = false;

                txtRef_Date.Enabled = false;
                txtEwayBillno.Enabled = false;
                txtAsnNo.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                btntxtMill_Code.Enabled = false;
                btntxtShipTo.Enabled = false;
                txtTaxable_Amount.Enabled = false;
                txtGST_Rate_Code.Enabled = false;
                btntxtGST_Rate_Code.Enabled = false;
                txtGST.Enabled = false;
                txtGross_Value.Enabled = false;

                txtCGST_Rate.Enabled = false;
                txtCGST_Amount.Enabled = false;
                txtSGST_Rate.Enabled = false;
                txtSGST_Amount.Enabled = false;
                txtIGST_Rate.Enabled = false;
                txtIGST_Amount.Enabled = false;
                txtfinalAmount.Enabled = false;
                txtMISC.Enabled = false;
                txtMISC.Text = string.Empty;
                txtnarration.Enabled = false;
                //txtTran_Type.Enabled = false;
                drpSub_Type.Enabled = true;
                // txtSub_Type.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_CodeDetails.Enabled = false;
                txtvalue.Enabled = false;
                ViewState["currentTable"] = null;
                txtCGST_Rate.Text = string.Empty;
                txtCGST_Amount.Text = string.Empty;
                txtSGST_Rate.Text = string.Empty;
                txtSGST_Amount.Text = string.Empty;
                txtIGST_Rate.Text = string.Empty;
                txtIGST_Amount.Text = string.Empty;

                txtGross_Value.Text = string.Empty;
                txtGST_Rate_Code.Text = string.Empty;
                lblGST_Rate_Code.Text = string.Empty;
                txtGST.Text = string.Empty;

                txtTaxable_Amount.Text = string.Empty;
                txtackno.Text = string.Empty;
                txtackno.Enabled = false;

                btntxtUnit.Enabled = false;

                txtTCSRate.Enabled = false;
                txtTCSAmt.Enabled = false;
                txtTCSNet_Payable.Enabled = false;

                txtTDS.Enabled = false;
                txtTDSAmt.Enabled = false;

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
                ViewState["currentTable"] = null;
                pnlgrdDetail.Enabled = true;
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btntxtBillNo.Enabled = true;
                grdDetail.DataSource = null; grdDetail.DataBind();
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtRef_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); ;
                //  txtSub_Type.Enabled = true;
                txtAc_Code.Enabled = true;
                lblAc_CodeDetails.Text = string.Empty;
                btntxtAc_CodeDetails.Enabled = true;
                txtvalue.Enabled = true;

                txtDoc_Date.Enabled = true;
                txtAsnNo.Enabled = true;
                txtEwayBillno.Enabled = true;
                txtBillNo.Enabled = false;
                txtnarration.Enabled = true;
                txtAc_Code.Enabled = true;
                lblAc_code.Text = string.Empty;
                btntxtAc_Code.Enabled = true;
                btntxtMill_Code.Enabled = true;
                btntxtShipTo.Enabled = true;
                txtnarration.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtGST_Rate_Code.Enabled = true;
                lblGST_Rate_Code.Text = string.Empty;
                btntxtGST_Rate_Code.Enabled = true;
                txtGST.Enabled = true;
                txtGross_Value.Enabled = true;

                txtCGST_Rate.Enabled = true;
                txtCGST_Amount.Enabled = true;
                txtSGST_Rate.Enabled = true;
                txtSGST_Amount.Enabled = true;
                txtIGST_Rate.Enabled = true;
                txtIGST_Amount.Enabled = true;
                txtMISC.Enabled = true;
                txtMISC.Text = string.Empty;
                //txtTran_Type.Enabled = true;
                drpSub_Type.Enabled = false;
                ViewState["currentTable"] = null;

                txtTaxable_Amount.Text = string.Empty;
                txtCGST_Rate.Text = string.Empty;
                txtCGST_Amount.Text = string.Empty;
                txtSGST_Rate.Text = string.Empty;
                txtSGST_Amount.Text = string.Empty;
                txtIGST_Rate.Text = string.Empty;
                txtIGST_Amount.Text = string.Empty;
                txtEwayBillno.Text = string.Empty;
                txtAsnNo.Text = string.Empty;
                txtGross_Value.Text = string.Empty;
                txtGST_Rate_Code.Text = string.Empty;
                lblGST_Rate_Code.Text = string.Empty;
                txtGST.Text = string.Empty;
                drpSub_Type.Text = hdnfTran_type.Value;
                txtackno.Text = string.Empty;
                txtackno.Enabled = true;
                // lblHSN_Code.Text = string.Empty;
                btntxtUnit.Enabled = true;
                lblUnit.Text = string.Empty;
                lblMill_Code.Text = string.Empty;
                lblShipTo.Text = string.Empty;
                lblBillid.Text = string.Empty;

                hdnfbillid.Value = string.Empty;
                hdnfbillno.Value = string.Empty; ;
                txtnarration.Text = string.Empty;

                txtTCSRate.Enabled = true;
                txtTCSAmt.Enabled = true;
                txtTCSNet_Payable.Enabled = true;
                txtTCSRate.Text = "0.000";
                txtfinalAmount.Text = string.Empty;

                txtTCSRate.Text = string.Empty;
                txtTCSAmt.Text = string.Empty;
                txtTCSNet_Payable.Text = string.Empty;

                txtTDS.Enabled = true;
                txtTDS.Text = "0.00";
                txtTDSAmt.Enabled = true;

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
                pnlgrdDetail.Enabled = false;
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                btntxtBillNo.Enabled = false;
                txtDoc_Date.Enabled = false;

                txtRef_Date.Enabled = false;
                //txtEwayBillno.Enabled = false;
                //txtAsnNo.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                btntxtMill_Code.Enabled = false;
                btntxtShipTo.Enabled = false;
                txtBillNo.Enabled = false;
                txtTaxable_Amount.Enabled = false;
                txtGST_Rate_Code.Enabled = false;
                btntxtGST_Rate_Code.Enabled = false;
                txtGST.Enabled = false;
                txtGross_Value.Enabled = false;

                txtCGST_Rate.Enabled = false;
                txtCGST_Amount.Enabled = false;
                txtSGST_Rate.Enabled = false;
                txtSGST_Amount.Enabled = false;
                txtIGST_Rate.Enabled = false;
                txtIGST_Amount.Enabled = false;

                txtMISC.Enabled = false;

                drpSub_Type.Enabled = true;
                // txtSub_Type.Enabled = false;
                txtAc_CodeDetails.Enabled = false;
                btntxtAc_CodeDetails.Enabled = false;
                txtvalue.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtnarration.Enabled = false;
                btnAdddetails.Text = "ADD";
                txtAc_CodeDetails.Text = string.Empty;
                lblAc_CodeDetails.Text = string.Empty;
                txtvalue.Text = string.Empty;

                //txtackno.Text = string.Empty;
                txtackno.Enabled = false;
                btntxtUnit.Enabled = false;
                txtTCSRate.Enabled = false;
                txtTCSAmt.Enabled = false;
                txtTCSNet_Payable.Enabled = false;

                txtTDS.Enabled = false;
                txtTDSAmt.Enabled = false;
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
                //txtDoc_No.Enabled = true;
                txtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                pnlgrdDetail.Enabled = true;
                lblMsg.Text = string.Empty;

                txtDoc_Date.Enabled = true;

                txtRef_Date.Enabled = true;

                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                btntxtMill_Code.Enabled = true;
                btntxtShipTo.Enabled = true;
                btntxtBillNo.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtGST_Rate_Code.Enabled = true;
                btntxtGST_Rate_Code.Enabled = true;
                txtGST.Enabled = true;
                txtGross_Value.Enabled = true;
                txtBillNo.Enabled = false;
                txtCGST_Rate.Enabled = true;
                txtCGST_Amount.Enabled = true;
                txtSGST_Rate.Enabled = true;
                txtSGST_Amount.Enabled = true;
                txtIGST_Rate.Enabled = true;
                txtIGST_Amount.Enabled = true;
                txtMISC.Enabled = true;

                txtnarration.Enabled = true;
                // txtTran_Type.Enabled = true;
                drpSub_Type.Enabled = false;
                //  txtSub_Type.Enabled = true;
                txtAc_CodeDetails.Enabled = true;
                btntxtAc_CodeDetails.Enabled = true;
                txtvalue.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtAsnNo.Enabled = true;
                txtEwayBillno.Enabled = true;
                txtackno.Enabled = true;
                txtackno.Enabled = true;
                //  lblNo.Enabled = false;
                btntxtUnit.Enabled = true;
                txtTCSRate.Enabled = true;
                txtTCSAmt.Enabled = true;
                txtTCSNet_Payable.Enabled = true;
                txtTDS.Enabled = true;
                txtTDSAmt.Enabled = true;
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




    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        //this.getMaxCode();
        hdnfTran_type.Value = drpSub_Type.SelectedValue;
        this.NextNumber();
        txtGST_Rate_Code.Text = "1";
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        lblGST_Rate_Code.Text = gstname;
        setFocusControl(txtDoc_Date);

    }
    #endregion
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["Year"].ToString() + "' and tran_type='" + hdnfTran_type.Value + "'"));
            if (counts == 0)
            {

                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'and Year_Code='" + Session["Year"].ToString() + "' and tran_type='" + hdnfTran_type.Value + "'")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(dcid) as bsid from " + tblHead + ""));
            if (counts == 0)
            {
                lblDoc_No.Text = "1";
                Entry_no = 1;
            }
            else
            {
                Entry_no = Convert.ToInt32(clsCommon.getString("SELECT max(dcid) as bsid from " + tblHead)) + 1;
                lblDoc_No.Text = Entry_no.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        hdnf.Value = lblDoc_No.Text;
        clsButtonNavigation.enableDisable("E");

        //pnlgrdDetail.Enabled = true;

        //string qry = getDisplayQuery();
        //fetchRecord(qry);
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        // setFocusControl(txtDoc_No);

        btnGenEinvoice.Enabled = false;

    }
    #endregion
    // private string getDisplayQuery()
    //{
    //    try
    //    {
    //        string qryDisplay = " select * from " + qryCommon + " where Doc_No=" + hdnf.Value + " and Company_Code='"
    //            + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  and Sub_Type='" + sub_type + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
    //        return qryDisplay;
    //    }
    //    catch
    //    {
    //        return "";
    //    }
    //}
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
        hdnf.Value = Request.QueryString["dcid"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            string max = clsCommon.getString("select max(dcid) as id from debitnotehead where Company_Code=" + Session["Company_Code"].ToString() + " " +
               " and Year_Code=" + Session["year"].ToString() + " and tran_type='" + drpSub_Type.SelectedValue + "'");
            hdnf.Value = max;
        }
        hdnfTran_type.Value = drpSub_Type.SelectedValue;
        Response.Redirect("pgeDebitCreditNote.aspx?dcid=" + hdnf.Value + "&Action=" + 1 + "&tran_type=" + hdnfTran_type.Value);
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
                        //txtEditDoc_No.Text = dt.Rows[0]["EditChange_No"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["doc_no"].ToString();
                        hdnfdebitdoc.Value = txtDoc_No.Text;
                        lblDoc_No.Text = hdnf.Value;
                        hdnfdebitid.Value = lblDoc_No.Text;
                        txtRef_Date.Text = dt.Rows[0]["bill_dateConverted"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtBillNo.Text = dt.Rows[0]["bill_no"].ToString();

                        txtDoc_Date.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        lblBillid.Text = dt.Rows[0]["bill_id"].ToString();
                        txtAc_Code.Text = dt.Rows[0]["ac_code"].ToString();
                        lblAc_code.Text = dt.Rows[0]["Ac_Name_E"].ToString();

                        txtTaxable_Amount.Text = dt.Rows[0]["texable_amount"].ToString();
                        txtGST_Rate_Code.Text = dt.Rows[0]["gst_code"].ToString();
                        lblGST_Rate_Code.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtnarration.Text = dt.Rows[0]["Narration"].ToString();
                        string value = dt.Rows[0]["ASNNO"].ToString();
                        if (value != "0")
                        {
                            txtAsnNo.Text = dt.Rows[0]["ASNNO"].ToString();
                        }
                        else
                        {
                            txtAsnNo.Text = "";
                        }
                        string value2 = dt.Rows[0]["Ewaybillno"].ToString();
                        if (value2 != "0")
                        {
                            txtEwayBillno.Text = dt.Rows[0]["Ewaybillno"].ToString();
                        }
                        else
                        {
                            txtEwayBillno.Text = "";
                        }

                        //lblHSN_Code.Text = dt.Rows[0]["HSN_Code"].ToString();
                        txtCGST_Rate.Text = dt.Rows[0]["cgst_rate"].ToString();
                        txtCGST_Amount.Text = dt.Rows[0]["cgst_amount"].ToString();
                        txtSGST_Rate.Text = dt.Rows[0]["sgst_rate"].ToString();
                        txtSGST_Amount.Text = dt.Rows[0]["sgst_amount"].ToString();
                        txtIGST_Rate.Text = dt.Rows[0]["igst_rate"].ToString();
                        txtIGST_Amount.Text = dt.Rows[0]["igst_amount"].ToString();
                        txtMISC.Text = dt.Rows[0]["misc_amount"].ToString();
                        txtfinalAmount.Text = dt.Rows[0]["bill_amount"].ToString();
                        // drpSub_Type.Text = dt.Rows[0]["tran_type"].ToString();
                        drpSub_Type.SelectedValue = dt.Rows[0]["tran_type"].ToString();
                        hdnfAccode.Value = dt.Rows[0]["ac"].ToString();

                        txtShipTo.Text = dt.Rows[0]["Shit_To"].ToString();
                        lblShipTo.Text = dt.Rows[0]["ShopTo_Name"].ToString();
                        txtMill_Code.Text = dt.Rows[0]["Mill_Code"].ToString();
                        lblMill_Code.Text = dt.Rows[0]["Mill_Name"].ToString();
                        hdnfShipTo.Value = dt.Rows[0]["st"].ToString();
                        hdnfMill.Value = dt.Rows[0]["mc"].ToString();

                        txtUnit.Text = dt.Rows[0]["Unit_Code"].ToString();
                        lblUnit.Text = dt.Rows[0]["Unit_Name"].ToString();
                        hdnfUnit.Value = dt.Rows[0]["uc"].ToString();

                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();

                        txtTDS.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        string IsDeleted = dt.Rows[0]["IsDeleted"].ToString();

                        string ackno = dt.Rows[0]["ackno"].ToString();
                        if (ackno != "0")
                        {
                            txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        }
                        else
                        {
                            txtackno.Text = "";
                        }
                        hdnfbillid.Value = dt.Rows[0]["bill_no"].ToString();
                        hdnfbillno.Value = dt.Rows[0]["bill_id"].ToString();
                        hdnfBilltype.Value = dt.Rows[0]["bill_type"].ToString();

                        if (IsDeleted == "0")
                        {
                            lblMsg.Text = "delete";
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            btnCancel.Enabled = false;
                            btnGenEinvoice.Enabled = false;
                           
                        }

                        if (drpSub_Type.SelectedValue == "DN" || drpSub_Type.SelectedValue == "CN" || drpSub_Type.SelectedValue == "D1" || drpSub_Type.SelectedValue == "C2")
                        {
                            //  string cust = "Customer";
                            //  lnkAcCode.Text = "Customer";
                            // lblAcCodeCpation.Text = cust.ToString();

                        }
                        else
                        {
                            //  string cust1 = "Supplier";
                            //lnkAcCode.Text = "Supplier";
                            // lblAcCodeCpation.Text = cust1.ToString();
                        }

                        //         lblRefYear.Text = "Year: " + dt.Rows[0]["RefYear_Code"].ToString();
                        if (drpSub_Type.SelectedValue == "C1")
                        {
                            //lblbilltrandocno.Text = dt.Rows[0]["Biil_TranType_DocNO"].ToString();
                        }


                        if (txtEwayBillno.Text == string.Empty && txtackno.Text == string.Empty || txtEwayBillno.Text == "0" && txtackno.Text == "0")
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
                            if (txtEwayBillno.Text != string.Empty && txtackno.Text != string.Empty)
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
                        string qty = string.Empty;
                        if (drpSub_Type.SelectedValue == "CN" || drpSub_Type.SelectedValue == "DN")
                        {
                            qty = clsDAL.GetString(" select Qty from qrySaleBillForDebitnote where Tran_Type='" + hdnfBilltype.Value
                               + "' and Billid=" + hdnfbillid.Value + "  and IsDeleted!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        }
                        else
                        {
                            qty = clsDAL.GetString(" select Qty from qrypurchasebillforDebitnote where Tran_Type='" + hdnfBilltype.Value
                                + "' and purchaseid=" + hdnfbillid.Value + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        }
                        lblQty.Text = "Qty:" + qty;
                        recordExist = true;
                        //lblMsg.Text = "";
                        #region Details
                        qry = "";
                        //ds = clsDAL.SimpleQuery("select Detail_Id,HeadSub_Type,HeadAc_Code,HeadAc_Name_E,Amount from qryDebitCreditNote where Company_Code='"
                        //    + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString())
                        //    + "' and Sub_Type='" + sub_type + "'and Doc_No =" + txtDoc_No.Text);
                        ds = clsDAL.SimpleQuery("select  detail_Id,expac_code as ExAc_Code,expacaccountname as HeadAc_Name_E,value as Amount,Item_Code as Item_Code,Item_Name as Item_Name,HSN,Quantal as Qty,dcdetailid as AutoIDetailId,'' as rowAction,'' as SrNo,expac as ExAcodeId,ic from qrydebitnoteheaddetail where Detail_ID is not null and Company_Code='"
                           + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString())
                           + "' and tran_type='" + hdnfTran_type.Value + "'and dcid=" + hdnf.Value + "");
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
                        csCalculations();
                        pnlgrdDetail.Enabled = false;
                        // hdnf.Value = lblDoc_No.Text;
                    }
                    else
                    {
                        grdDetail.DataSource = ViewState["currentTable"];
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
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
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where dcid=" + hdnf.Value + "  and tran_type='" + hdnfTran_type.Value + "' ";
            return qryDisplay;

            // return qryDisplay;
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
        double.TryParse(txtvalue.Text, out amount);

        if (amount == 0 && amount != 0.0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct amount! ');", true);

            setFocusControl(txtvalue);
            return;
        }
        int AcCode;
        Int32.TryParse(txtAc_CodeDetails.Text, out AcCode);
        if (AcCode == 0)
        {
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct amount!);", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct Ac Code! ');", true);

            setFocusControl(txtAc_CodeDetails);
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
                if (dt.Rows[0]["Detail_Id"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_Id"].ToString());
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
                        dr["Detail_Id"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row 

                        int n = Convert.ToInt32(lblID.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_Id"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]

                        string id = clsCommon.getString("select detail_Id from " + tblDetails + " where doc_no='" + txtDoc_No.Text + "' and detail_Id='" + rowIndex + "' " +
                            " and Company_Code=" + Session["Company_Code"].ToString() + " and  Year_Code=" + Session["year"].ToString() + " and tran_type='" + drpSub_Type.SelectedValue + "'");
                        //  string id = clsCommon.getString("select Detail_Id from " + tblDetails + " where Detail_Id='" + lblID.Text + "' and Detail_Id=" + lblID.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
                    dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                    #region [Write here columns]
                    // dt.Columns.Add((new DataColumn("HeadSub_Type", typeof(string))));
                    dt.Columns.Add((new DataColumn("ExAc_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("HeadAc_Name_E", typeof(string))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("HSN", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                    dt.Columns.Add((new DataColumn("AutoIDetailId", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("ExAcodeId", typeof(int))));
                    dt.Columns.Add((new DataColumn("ic", typeof(int))));


                    dr = dt.NewRow();
                    dr["Detail_Id"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                #region [Write here columns]
                //  dt.Columns.Add((new DataColumn("HeadSub_Type", typeof(string))));
                dt.Columns.Add((new DataColumn("ExAc_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("HeadAc_Name_E", typeof(string))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("HSN", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                dt.Columns.Add((new DataColumn("AutoIDetailId", typeof(int))));

                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add((new DataColumn("ExAcodeId", typeof(int))));
                dt.Columns.Add((new DataColumn("ic", typeof(int))));


                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]

            if (txtAc_CodeDetails.Text != string.Empty)
            {
                dr["ExAc_Code"] = txtAc_CodeDetails.Text;
            }
            else
            {
                setFocusControl(txtAc_Code);
                return;
            }
            dr["HeadAc_Name_E"] = lblAc_CodeDetails.Text;
            if (txtvalue.Text != string.Empty)
            {
                dr["Amount"] = txtvalue.Text;
            }
            else
            {
                dr["Amount"] = 0.00;


            }
            dr["Item_Code"] = txtItem_Code.Text;
            dr["Item_Name"] = lblItem_Code.Text;
            dr["HSN"] = txtHSN.Text;
            if (txtQty.Text != string.Empty)
            {
                dr["Qty"] = txtQty.Text;
            }
            else
            {
                dr["Qty"] = 0.00;

            }
            dr["ExAcodeId"] = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where Ac_Code=" + txtAc_CodeDetails.Text + " and company_code=" + Session["company_code"].ToString() + "");
            // dr["AutoIDetailId"] = lblExAcId.Text;
            dr["ic"] = clsCommon.getString("select systemid from qrymstitem where system_code=" + txtItem_Code.Text + " and company_code=" + Session["company_code"].ToString() + "");
            if (btnAdddetails.Text == "ADD")
            {
                dr["AutoIDetailId"] = 0;
                dt.Rows.Add(dr);
            }
            else
            {

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
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtAc_CodeDetails);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            txtAc_CodeDetails.Text = string.Empty;
            lblAc_CodeDetails.Text = string.Empty;
            //txtAc_Code.Text = string.Empty;
            txtvalue.Text = string.Empty;
            txtItem_Code.Text = string.Empty;
            lblItem_Code.Text = string.Empty;
            txtHSN.Text = string.Empty;
            txtQty.Text = string.Empty;
            btnAdddetails.Text = "ADD";
            csCalculations();
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

        txtAc_CodeDetails.Text = string.Empty;
        txtvalue.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtAc_CodeDetails);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[12].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;

        txtAc_CodeDetails.Text = Server.HtmlDecode(gvrow.Cells[Ac_Code].Text);
        lblAc_CodeDetails.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtvalue.Text = Server.HtmlDecode(gvrow.Cells[5].Text);
        txtItem_Code.Text = Server.HtmlDecode(gvrow.Cells[6].Text);
        lblItem_Code.Text = Server.HtmlDecode(gvrow.Cells[7].Text);
        txtHSN.Text = Server.HtmlDecode(gvrow.Cells[8].Text);
        txtQty.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        lblExAcId.Text = Server.HtmlDecode(gvrow.Cells[13].Text);
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_Id"].ToString());
                string IDExisting = clsCommon.getString("select dcdetailid from " + tblDetails + " where Doc_No='"
                  + txtDoc_No.Text + "' and tran_type='" + sub_type + "'");
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "N";// Add rowaction id
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
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
            // csCalculations();
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

            e.Row.Cells[0].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("300px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("150px");
            //e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("20px");
            //e.row.cells[6].visible = false;
            e.Row.Cells[7].Visible = true;
            // e.row.cells[8].visible = fal

            e.Row.Cells[8].Visible = true;

            e.Row.Cells[AC_codeName].HorizontalAlign = HorizontalAlign.Left;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            //    e.Row.Cells[0].Visible =true;
            // }
            e.Row.Cells[Ac_Code].Visible = true;

            e.Row.Cells[9].Visible = true;
            //e.Row.Cells[Rowaction].Visible = false;
            //e.Row.Cells[Srno].Visible = false;

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
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtDoc_No")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                //e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
                //e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                //e.Row.Cells[3].ControlStyle.Width = new Unit("100px");

            }
            if (v == "txtBranch_Code" || v == "txtAc_Code" || v == "txtAc_CodeDetails"
                || v == "txtGST_Rate_code")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
            }
            if (v == "txtBusiness_Code" || v == "txtHSN")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("150px");


            }
            if (v == "txtBillNo")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[3].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[4].ControlStyle.Width = new Unit("350px");
                e.Row.Cells[5].ControlStyle.Width = new Unit("150px");
                e.Row.Cells[6].ControlStyle.Width = new Unit("150px");


            }
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
                            setFocusControl(txtAc_CodeDetails);
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
                        csCalculations();
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Only Numbers Allow!')", true);
            }
            else
            {

                int Sale_id = Convert.ToInt32(clsCommon.getString("select dcid from debitnotehead where doc_no=" + txtEditDoc_No.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " and " +
                    " Year_Code=" + Session["year"].ToString() + " and tran_type='" + drpSub_Type.SelectedValue + "'"));
                hdnf.Value = Sale_id.ToString();
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
                        "  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";

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

                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtDoc_No.Enabled = false;


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
        //try
        //{
        //    txtEditDoc_No.Text = string.Empty;
        //    pnlPopup.Style["display"] = "block";
        //    hdnfClosePopup.Value = "txtDoc_No";
        //    btnSearch_Click(sender, e);
        //}
        //catch
        //{
        //}
        try
        {
            if (btntxtDoc_No.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtDoc_No.Text = string.Empty;
                txtDoc_No.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtDoc_No);
            }

            if (btntxtDoc_No.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtDoc_No";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }
    #endregion





    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        //csCalculations();
    }
    #endregion

    protected void txtBillNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBillNo.Text;
        strTextBox = "txtBillNo";
        //  csCalculations();
        setFocusControl(txtRef_Date);
    }

    protected void txtRef_Date_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtGST_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtMISC_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMISC.Text;
        strTextBox = "txtMISC";
        csCalculations();
    }
    //protected void txtAsnNo_TextChanged(object sender, EventArgs e)
    //{

    //}


    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtBillNo")
            {
                string acname = "";
                if (txtBillNo.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBillNo.Text);
                    if (a == false)
                    {
                        btntxtBillNo_Click(this, new EventArgs());
                    }
                    else
                    {
                        //acname = clsCommon.getString("select doc_no from qrySaleBillForDebitnote where Tran_Type='" + hdnfBilltype.Value + "' and saleid=" + hdnfbillid.Value
                        //        + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //string docdate = clsCommon.getString("select docdate from qrySaleBillForDebitnote where Tran_Type='" + hdnfBilltype.Value + "' and saleid=" + hdnfbillid.Value
                        //       + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //if (acname != string.Empty && acname != "0")
                        //{

                        //    lblBillid.Text = acname;
                        //    txtRef_Date.Text = docdate;
                        //    setFocusControl(txtRef_Date);
                        //}
                        //else
                        //{
                        //    txtBillNo.Text = string.Empty;
                        //    lblBillid.Text = acname;
                        //    setFocusControl(txtAc_Code);
                        //}
                        qry = "select doc_no,docdate,ShipTo,ShipToName,MillCode,MillName,Qty from qrySaleBillForDebitnote where Tran_Type='" + hdnfBilltype.Value + "' and saleid=" + hdnfbillid.Value
                                + "  and IsDeleted!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblBillid.Text = ds.Tables[0].Rows[0]["doc_no"].ToString();
                                txtRef_Date.Text = ds.Tables[0].Rows[0]["docdate"].ToString();
                                txtShipTo.Text = ds.Tables[0].Rows[0]["Bill_To"].ToString();
                                lblShipTo.Text = ds.Tables[0].Rows[0]["BillToName"].ToString();
                                txtMill_Code.Text = ds.Tables[0].Rows[0]["MillCode"].ToString();
                                lblMill_Code.Text = ds.Tables[0].Rows[0]["MillName"].ToString();
                                txtUnit.Text = ds.Tables[0].Rows[0]["ShipTo"].ToString();
                                lblUnit.Text = ds.Tables[0].Rows[0]["ShipToName"].ToString();
                                lblQty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();

                                hdnfShipTo.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtShipTo.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                                hdnfMill.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtMill_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                                hdnfUnit.Value = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtShipTo.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");


                                setFocusControl(txtRef_Date);
                            }
                        }
                        else
                        {
                            txtBillNo.Text = string.Empty;
                            lblBillid.Text = string.Empty;
                            setFocusControl(txtAc_Code);
                        }


                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }
            }


            if (strTextBox == "txtAc_Code")
            {
                string acname = "";
                if (txtAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                    if (a == false)
                    {
                        btntxtAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfAccode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



                        if (acname != string.Empty && acname != "0")
                        {

                            lblAc_code.Text = acname;
                            setFocusControl(btntxtBillNo);

                        }
                        else
                        {
                            txtAc_Code.Text = string.Empty;
                            lblAc_code.Text = acname;
                            setFocusControl(txtAc_Code);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }


            }

            if (strTextBox == "txtGST_Rate_Code")
            {
                string GST_Rate = "";
                if (txtGST_Rate_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGST_Rate_Code.Text);
                    if (a == false)
                    {
                        btntxtGST_Rate_Code_Click(this, new EventArgs());
                    }
                    else
                    {

                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string qry1 = "select isnull(Rate,0) as Rate,isnull(CGST,0) as CGST, " +
                        " isnull(SGST,0) as SGST,isnull(IGST,0) as IGST from nt_1_gstratemaster  where Doc_no='" + txtGST_Rate_Code.Text + "' ";
                        ds = clsDAL.SimpleQuery(qry1);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    // txtCGST_Rate.Text = dt.Rows[0]["CGST"].ToString();
                                    //   txtSGST_Rate.Text = dt.Rows[0]["SGST"].ToString();
                                    //  txtIGST_Rate.Text = dt.Rows[0]["IGST"].ToString();
                                    lblGST_Rate_Code.Text = dt.Rows[0]["Rate"].ToString();
                                    //setFocusControl(txtGST);
                                    setFocusControl(txtAsnNo);
                                }
                                else
                                {
                                    txtGST_Rate_Code.Text = string.Empty;
                                    lblGST_Rate_Code.Text = GST_Rate;
                                    setFocusControl(txtGST_Rate_Code);
                                }
                            }
                            else
                            {
                                txtGST_Rate_Code.Text = string.Empty;
                                lblGST_Rate_Code.Text = GST_Rate;
                                setFocusControl(txtGST_Rate_Code);
                            }

                        }

                        else
                        {
                            txtGST_Rate_Code.Text = string.Empty;
                            lblGST_Rate_Code.Text = GST_Rate;
                            setFocusControl(txtGST_Rate_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtGST_Rate_Code);
                }


            }


            if (strTextBox == "txtCGST_Rate")
            {
                setFocusControl(txtCGST_Amount);
            }
            if (strTextBox == "txtSGST_Rate")
            {
                setFocusControl(txtSGST_Amount);
            }
            if (strTextBox == "txtTaxable_Amount")
            {
                setFocusControl(btnAdd);
            }
            if (strTextBox == "txtAc_CodeDetails")
            {
                string acname = "";
                if (txtAc_CodeDetails.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAc_CodeDetails.Text);
                    if (a == false)
                    {
                        btntxtAc_CodeDetails_Click(this, new EventArgs());
                    }
                    else
                    {

                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtAc_CodeDetails.Text
                                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (acname != string.Empty && acname != "0")
                        {
                            lblAc_CodeDetails.Text = acname;
                            setFocusControl(txtvalue);

                        }
                        else
                        {
                            txtAc_CodeDetails.Text = string.Empty;
                            lblAc_CodeDetails.Text = acname;
                            setFocusControl(txtAc_CodeDetails);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtAc_CodeDetails);
                }
            }

            if (strTextBox == "txtShipTo")
            {
                string acname = "";
                if (txtShipTo.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtShipTo.Text);
                    if (a == false)
                    {
                        btntxtShipTo_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfShipTo.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtShipTo.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtShipTo.Text
                                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



                        if (acname != string.Empty && acname != "0")
                        {

                            lblShipTo.Text = acname;
                            setFocusControl(txtMill_Code);

                        }
                        else
                        {
                            txtShipTo.Text = string.Empty;
                            lblShipTo.Text = acname;
                            setFocusControl(txtShipTo);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtShipTo);
                }


            }

            if (strTextBox == "txtMill_Code")
            {
                string acname = "";
                if (txtMill_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMill_Code.Text);
                    if (a == false)
                    {
                        btntxtMill_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfMill.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtMill_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtMill_Code.Text
                                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



                        if (acname != string.Empty && acname != "0")
                        {

                            lblMill_Code.Text = acname;
                            setFocusControl(txtUnit);

                        }
                        else
                        {
                            txtMill_Code.Text = string.Empty;
                            lblMill_Code.Text = acname;
                            setFocusControl(txtMill_Code);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtMill_Code);
                }


            }
            if (strTextBox == "txtUnit")
            {
                string acname = "";
                if (txtUnit.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtUnit.Text);
                    if (a == false)
                    {
                        btntxtUnit_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfUnit.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtUnit.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtUnit.Text
                                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



                        if (acname != string.Empty && acname != "0")
                        {

                            lblUnit.Text = acname;
                            setFocusControl(txtAc_CodeDetails);

                        }
                        else
                        {
                            txtUnit.Text = string.Empty;
                            lblUnit.Text = acname;
                            setFocusControl(txtAc_CodeDetails);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtUnit);
                }


            }
            if (strTextBox == "txtItem_Code")
            {
                string itemname = "";
                string HSN = "";
                if (txtItem_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtItem_Code.Text);
                    if (a == false)
                    {
                        btntxtItem_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        HSN = clsCommon.getString("select HSN from " + SystemMasterTable + " where System_Code=" + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (itemname != string.Empty && itemname != "0")
                        {
                            lblItem_Code.Text = itemname;
                            txtHSN.Text = HSN;
                            setFocusControl(txtQty);
                        }
                        else
                        {
                            txtItem_Code.Text = string.Empty;
                            lblItem_Code.Text = itemname;
                            txtHSN.Text = HSN;
                            setFocusControl(txtItem_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtItem_Code);
                }
            }
            if (txtBillNo.Text == string.Empty)
            {
                txtBillNo.Text = hdnfbillid.Value;
                lblBillid.Text = hdnfbillno.Value;
                lblQty.Text = hdnfqty.Value;
            }
            if (strTextBox == "txtTCSRate")
            {
                setFocusControl(txtTCSNet_Payable);
            }
            if (strTextBox == "txtTCSNet_Payable")
            {
                setFocusControl(btnSave);
            }
            #region calculate subtotal



            double cgstrate = 0.00;
            double sgstrate = 0.00;
            double igstrate = 0.00;


            double CGSTRateForPS = 0.00;
            double SGSTRateForPS = 0.00;
            double IGSTRateForPS = 0.00;

            double CGSTAmountForPS = 0.0;
            double SGSTAmountForPS = 0.0;
            double IGSTAmountForPS = 0.0;

            double subtotal = 0.00;
            double amount = 0.00;
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[7].Text != "D")
                    {
                        double item_Amt = Convert.ToDouble(Server.HtmlDecode(grdDetail.Rows[i].Cells[5].Text.Trim()));
                        subtotal = subtotal + item_Amt;
                    }
                }
                txtTaxable_Amount.Text = subtotal.ToString();
                amount = Convert.ToDouble(txtTaxable_Amount.Text);
            }
            #endregion




            if (Session["CompanyGSTStateCode"] == null || Session["CompanyGSTStateCode"] == string.Empty)
            {
                Session["CompanyGSTStateCode"] = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                    "  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            }
            int companyGstStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());

            string AcStateCode = "";
            if (txtAc_Code.Text.Trim() != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                if (a == true)
                {
                    //AcStateCode = clsCommon.getString("select isnull(GSTStateCode,0) from  qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAc_Code.Text + "");
                    AcStateCode = clsCommon.getString("select isnull(GSTStateCode,0) from  qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtShipTo.Text + "");

                }
            }
            int partygstStateCode = 0;
            if (AcStateCode.Trim().ToString() != "" && AcStateCode != "0")
            {
                partygstStateCode = Convert.ToInt32(AcStateCode);
            }
            Int32 GST_Rate_Code = txtGST_Rate_Code.Text != string.Empty ? Convert.ToInt32(txtGST_Rate_Code.Text) : 0;

            if (companyGstStateCode == partygstStateCode)
            {
                cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from nt_1_gstratemaster where Doc_no='" + GST_Rate_Code + "' "));
                CGSTRateForPS = cgstrate;


                double cgsttaxAmountOnMR = Math.Round((amount * cgstrate / 100), 2);
                CGSTAmountForPS = Math.Round(cgsttaxAmountOnMR, 2);

                sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from nt_1_gstratemaster where Doc_no='" + GST_Rate_Code + "' "));
                SGSTRateForPS = sgstrate;

                double sgsttaxAmountOnMR = Math.Round((amount * sgstrate / 100), 2);
                SGSTAmountForPS = Math.Round(sgsttaxAmountOnMR, 2);

            }
            else
            {

                igstrate = Convert.ToDouble(clsCommon.getString("select IGST from nt_1_gstratemaster where Doc_no='" + GST_Rate_Code + "' "));

                IGSTRateForPS = igstrate;
                double igsttaxAmountOnMR = ((amount) * igstrate / 100);

                IGSTAmountForPS = Math.Round(igsttaxAmountOnMR, 2);
            }
            txtCGST_Amount.Text = CGSTAmountForPS.ToString();
            txtSGST_Amount.Text = SGSTAmountForPS.ToString();
            txtIGST_Amount.Text = IGSTAmountForPS.ToString();
            txtCGST_Rate.Text = cgstrate.ToString();
            txtSGST_Rate.Text = sgstrate.ToString();
            txtIGST_Rate.Text = igstrate.ToString();
            double MISCAmt = txtMISC.Text != string.Empty ? Convert.ToDouble(txtMISC.Text) : 0.00;
            double finalA = amount + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS + MISCAmt;
            txtfinalAmount.Text = finalA.ToString();

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
            if (txtTCSRate.Text == string.Empty || txtTCSRate.Text == "0")
            {
                TCS_Rate = 0;
            }
            else
            {
                TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
            }
            Bill_Amt = Convert.ToDouble(txtfinalAmount.Text);
            if (TCS_Amt == 0)
            {
                TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
            }
            Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt), 2);


            txtTCSAmt.Text = TCS_Amt.ToString();
            txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();

            #region TDS Calculation
            double TDS_Rate = 0.000;
            double TDS_Amt = 0.00;
            TDS_Rate = Convert.ToDouble(txtTDS.Text);
            TDS_Amt = Math.Round(((amount * TDS_Rate) / 100), 2);
            txtTDSAmt.Text = TDS_Amt.ToString();
            #endregion
            #endregion
        }




        catch
        {

        }
    }

    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
    }
    #endregion
    protected void txtAsnNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAsnNo.Text;
        strTextBox = "txtAsnNo";
        setFocusControl(txtAsnNo);
        // csCalculations();
    }
    protected void txtfinalAmount_textchanged(object sender, EventArgs e)
    {
    }
    protected void txtnarration_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnarration.Text;
        strTextBox = "txtnarration";
        setFocusControl(txtTaxable_Amount);
        // csCalculations();
    }

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



    #region [txtRemark_TextChanged]
    protected void txtRemark_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtRemark.Text;
        //strTextBox = "txtRemark";
        //csCalculations();
        setFocusControl(btnSave);
    }
    #endregion



    #region [btntxtBusiness_Vertical_Code_Click]
    protected void btntxtBusiness_Vertical_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBusiness_Vertical_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtTaxable_Amount_TextChanged]
    protected void txtTaxable_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTaxable_Amount.Text;
        strTextBox = "txtTaxable_Amount";
        csCalculations();
    }
    #endregion

    #region [txtGST_Rate_Code_TextChanged]
    protected void txtGST_Rate_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_Rate_Code.Text;
        strTextBox = "txtGST_Rate_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtGST_Rate_Code_Click]
    protected void btntxtGST_Rate_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGST_Rate_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion



    #region [txtGross_Value_TextChanged]
    protected void txtGross_Value_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGross_Value.Text;
        strTextBox = "txtGross_Value";

    }
    #endregion



    #region [btntxtHSN_Click]
    protected void btntxtHSN_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtHSN";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtCGST_Rate_TextChanged]
    protected void txtCGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST_Rate.Text;
        strTextBox = "txtCGST_Rate";
        csCalculations();
    }
    #endregion

    #region [txtCGST_Amount_TextChanged]
    protected void txtCGST_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST_Amount.Text;
        strTextBox = "txtCGST_Amount";
        csCalculations();
    }
    #endregion

    #region [txtSGST_Rate_TextChanged]
    protected void txtSGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST_Rate.Text;
        strTextBox = "txtSGST_Rate";
        csCalculations();
    }
    #endregion

    #region [txtSGST_Amount_TextChanged]
    protected void txtSGST_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST_Amount.Text;
        strTextBox = "txtSGST_Amount";
        csCalculations();
    }
    #endregion

    #region [txtIGST_Rate_TextChanged]
    protected void txtIGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST_Rate.Text;
        strTextBox = "txtIGST_Rate";
        csCalculations();
    }
    #endregion

    #region [txtIGST_Amount _TextChanged]
    protected void txtIGST_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST_Amount.Text;
        strTextBox = "txtIGST_Amount ";
        csCalculations();
    }
    #endregion





    #region [drpSub_Type_TextChanged]
    protected void drpSub_Type_SelectedIndexChanged(object sender, EventArgs e)
    {

        string max = clsCommon.getString("select max(dcid) as id from debitnotehead where Company_Code=" + Session["Company_Code"].ToString() + " " +
           " and Year_Code=" + Session["year"].ToString() + " and tran_type='" + drpSub_Type.SelectedValue + "'");
        hdnf.Value = max;
        hdnfTran_type.Value = drpSub_Type.SelectedValue;
        clsButtonNavigation.enableDisable("N");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("N");
        ViewState["mode"] = "I";
        showLastRecord();
        setFocusControl(drpSub_Type);

    }
    #endregion



    #region [txtAc_Code_TextChanged]
    protected void txtAc_CodeDetails_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_CodeDetails.Text;
        strTextBox = "txtAc_CodeDetails";
        csCalculations();
    }
    #endregion

    #region [btntxtAc_Code_Click]
    protected void btntxtAc_CodeDetails_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAc_CodeDetails";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtAmount_TextChanged]
    protected void txtvalue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtvalue.Text;
        strTextBox = "txtvalue";
        //  csCalculations();
    }
    #endregion

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
                        // name += "Doc_No Like '%" + aa + "%'or";

                        name += "( Doc_No like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Net_Amount like '%" + aa + "%'  or Remark_For_Print like '%" + aa + "%') and";

                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select Group--";
                    // string qry = " select Doc_No,Sub_Type,Ref_No as Bill_No,Unique_Id,Ac_Code from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Doc_No";
                    string qry = " select Doc_No,Doc_Date,Ac_Name_E as  Party_name,Net_Amount as Amount,Remark_For_Print as Narration from qryDebitCreditNote where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Doc_No";
                    this.showPopup(qry);
                }
            }


            if (hdnfClosePopup.Value == "txtBranch_Code")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();

                    // name += " Branch_Name_E like '%" + aa + "%'  or Branch_Code like '%" + aa + "%'  or Branch_Name_R like '%" + aa + "%'  or";
                    name += "( Branch_Name_E like '%" + aa + "%' or Branch_Code like '%" + aa + "%' or Branch_Name_R like '%" + aa + "%' ) and";


                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Branch Code--";
                string qry = "select Branch_Code,Branch_Name_E,Branch_Name_R from  Branch_Master   where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + "  and ( " + name + ") order by Branch_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtRef_No")
            {


                lblPopupHead.Text = "--Select Ref no--";
                string qry = "";
                if (drpSub_Type.SelectedValue == "CS" || drpSub_Type.SelectedValue == "DS")
                {
                    foreach (var s in split)
                    {
                        string aa = s.ToString();

                        // name += " Branch_Name_E like '%" + aa + "%'  or Branch_Code like '%" + aa + "%'  or Branch_Name_R like '%" + aa + "%'  or";
                        name += "( Doc_No like '%" + aa + "%' or Supplier_name like '%" + aa + "%' or Unique_Id like '%" + aa + "%' ) ";


                    }
                    qry = " select distinct Unique_Id,Doc_No,Bill_No,Doc_Date,Supplier_name,Bill_Amount from qryPurchase" +
                         " where Tran_type='PB' and Supplier_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and " + name + " order by Doc_Date desc,Doc_No desc";
                }
                else
                {


                    foreach (var s in split)
                    {
                        string aa = s.ToString();

                        // name += " Branch_Name_E like '%" + aa + "%'  or Branch_Code like '%" + aa + "%'  or Branch_Name_R like '%" + aa + "%'  or";
                        name += "( Doc_No like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Unique_Id like '%" + aa + "%' ) and";


                    }
                    name = name.Remove(name.Length - 3);
                    qry = "select Unique_Id,Doc_No,convert(varchar(10),Doc_Date,103) as Doc_Date,Year_Code,Ac_Name_E as Partyname ,Doc_Date as DT from  qryPendingSaleBill   where Company_Code="
                        + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAc_Code.Text
                   + "  and ( " + name + ") order by DT desc";
                }

                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtBillNo")
            {
                lblPopupHead.Text = "--Debit Note--";
                if (drpSub_Type.SelectedValue == "CN" || drpSub_Type.SelectedValue == "DN")
                {
                    foreach (var s in split)
                    {
                        string aa = s.ToString();

                        // name += " Branch_Name_E like '%" + aa + "%'  or Branch_Code like '%" + aa + "%'  or Branch_Name_R like '%" + aa + "%'  or";
                        name += "( doc_no like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Billid like '%" + aa + "%' ) ";


                    }
                    qry = " select distinct Billid,doc_no,docdate,Ac_Code,Ac_Name_E as Party_Name,Amount,Tran_Type,ShipTo,ShipToName,MillCode,MillName,Bill_To,BillToName,uc,mc,bt,Qty,Year_Code as BillYC from qrySaleBillForDebitnote" +
                         " where  Ac_Code=" + txtAc_Code.Text + "  and IsDeleted!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and " + name + " order by docdate desc";
                }
                else
                {


                    foreach (var s in split)
                    {
                        string aa = s.ToString();

                        // name += " Branch_Name_E like '%" + aa + "%'  or Branch_Code like '%" + aa + "%'  or Branch_Name_R like '%" + aa + "%'  or";
                        name += "( Bill_No like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or purchaseid like '%" + aa + "%' ) and";


                    }
                    name = name.Remove(name.Length - 3);
                    //qry = " select distinct purchaseid,doc_no,docdate,Ac_Code,Ac_Name_E as Party_Name,TCS_Net_Payable as Amount,Tran_Type,Bill_No from qrypurchasebillforDebitnote" +
                    //      " where  Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    //     + " and " + name + " order by docdate desc,Bill_No desc";

                    qry = " select distinct purchaseid,doc_no,docdate,Ac_Code,Ac_Name_E as Party_Name,TCS_Net_Payable as Amount,Tran_Type,ShipTo,ShipToName,mill_code,MillName,0 as Bill_To,'' as BillToName,uc,mc,0 as bt,Qty,Year_Code as BillYC from qrypurchasebillforDebitnote" +
                         " where  Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and " + name + " order by docdate desc";
                }

                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtRef_Unique_Id")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                split = txtAc_Code.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    //name += " Ac_Name_E like '%" + aa + "%'  or Ac_Code like '%" + aa + "%'  or Ac_Name_R like '%" + aa + "%'  or";
                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or city_name_e like '%" + aa + "%' ) and";


                }

                name = name.Remove(name.Length - 3);
                string qry = string.Empty;

                lblPopupHead.Text = "--Select Branch Code--";
               // qry = "select Ac_Code,Ac_Name_E,cityname from  qrymstaccountmaster   where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               //+ "  and ( " + name + ") order by Ac_Name_E";


               // this.showPopup(qry);

                 qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
             + " and ( " + name + ") order by Ac_Name_E asc";

                this.showPopupAccountMaster(qry);
            }

            if (hdnfClosePopup.Value == "txtGST_Rate_Code")
            {
                split = txtGST_Rate_Code.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    //name += " GST_Code like '%" + aa + "%'  or GST_Name_E like '%" + aa + "%'  or Rate like '%" + aa + "%'  or";
                    name += "( Doc_no like '%" + aa + "%' or GST_Name like '%" + aa + "%' or Rate like '%" + aa + "%' ) and";


                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select--";
                string qry = "select Doc_no,GST_Name,Rate from  nt_1_gstratemaster   where ( " + name + ") order by GST_Name";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtHSN")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    //name += " HSN_Code like '%" + aa + "%'  or Doc_no like '%" + aa + "%'  or Reference_Name like '%" + aa + "%'  or";
                    name += "( HSN_Code like '%" + aa + "%' or Doc_no like '%" + aa + "%' or Reference_Name like '%" + aa + "%' ) and";


                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select HSN Code--";
                string qry = "select Doc_no,HSN_Code from  HSN_Master   where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + "  and ( " + name + ") order by HSN_Code";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtAc_CodeDetails")
            {
                split = txtAc_CodeDetails.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    //name += " Ac_Name_E like '%" + aa + "%'  or Ac_Code like '%" + aa + "%'  or Ac_Name_R like '%" + aa + "%'  or";
                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or cityname like '%" + aa + "%' ) and";


                }

                name = name.Remove(name.Length - 3);

                lblPopupHead.Text = "--Select Ac Code--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from  qrymstaccountmaster   where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               + "  and ( " + name + ") order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtShipTo")
            {
                split = txtShipTo.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or city_name_e like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Ac Code--";
               // string qry = "select Ac_Code,Ac_Name_E,cityname from  qrymstaccountmaster   where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               //+ "  and ( " + name + ") order by Ac_Name_E";
               // this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
         + " and  ( " + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtMill_Code")
            {
                split = txtMill_Code.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or city_name_e like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Ac Code--";
               // string qry = "select Ac_Code,Ac_Name_E,cityname from  qrymstaccountmaster   where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               //+ "  and ( " + name + ") order by Ac_Name_E";
               // this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
   + " and  ( " + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtUnit")
            {
                split = txtUnit.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or city_name_e like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Ac Code--";
               // string qry = "select Ac_Code,Ac_Name_E,cityname from  qrymstaccountmaster   where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               //+ "  and ( " + name + ") order by Ac_Name_E";
               // this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
     + " and  ( " + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtItem_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name,HSN from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "debitCreditNote", "javascript:pagevalidation();", true);

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
    protected void btntxtBillNo_Click(object sender, EventArgs e)
    {
        try
        {

            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBillNo";
            btnSearch_Click(sender, e);

        }
        catch
        {
        }
    }
    #endregion

    #region [txtShipTo_TextChanged]
    protected void txtShipTo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShipTo.Text;
        strTextBox = "txtShipTo";
        csCalculations();
    }
    #endregion
    #region [btntxtAc_Code_Click]
    protected void btntxtShipTo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtShipTo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtMill_Code_TextChanged]
    protected void txtMill_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMill_Code.Text;
        strTextBox = "txtMill_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtMill_Code_Click]
    protected void btntxtMill_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMill_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    protected void btnCancleEInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                bool isValidated = true;
                if (txtEwayBillno.Text != string.Empty || txtEwayBillno.Text != "NA")
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Can't Cancle this Einvoice!!!!!');", true);
                    return;
                }
                string Irn = txtEwayBillno.Text;
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
                string SBNo = txtDoc_No.Text;
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
                            qry1 = "update debitnotehead set ackno='NA',Ewaybillno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + txtDoc_No.Text + " and tran_type='" + hdnfTran_type.Value + "'";
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
   
    #region [txtUnit_TextChanged]
    protected void txtUnit_TextChanged(object sender, EventArgs e)
    {
        searchString = txtUnit.Text;
        strTextBox = "txtUnit";
        csCalculations();
    }
    #endregion
    #region [btnttxtUnit_Click]
    protected void btntxtUnit_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtUnit";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

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
    protected void txtTCSNet_Payable_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSNet_Payable.Text;
        strTextBox = "txtTCSNet_Payable";
        csCalculations();
    }

    #region [txtItem_Code_TextChanged]
    protected void txtItem_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Code.Text;
        strTextBox = "txtItem_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtItem_Code_Click]
    protected void btntxtItem_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtItem_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

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
}




