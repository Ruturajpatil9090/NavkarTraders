using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
public partial class pgeCheckPrintNew : System.Web.UI.Page
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
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    int Detail_Id = 2;
    int CheckDetail_Id = 3;
    int Ac_Code = 4;
    String Ac_Code_Name = string.Empty;
    int Chq_Caption = 6;
    int Amount = 7;
    int Narration = 8;
    int Bank_Date = 9;
    int Ac_Id = 10;
    int Rowaction = 11;
    int Srno = 12;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "CheckHeadNew";
            tblDetails = "CheckDetailNew";
            qryCommon = "qryCheckNew";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    hdnf.Value = Request.QueryString["Check_Id"];
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    this.showLastRecord();
                    this.enableDisableNavigateButtons();
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
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
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                txtCheck_Id.Enabled = false;
                txtCheckFrom.Enabled = false;
                txtCheckTo.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtPayment_No.Enabled = false;
                btntxtPayment_No.Enabled = false;
                txtCashBank.Enabled = false;
                btntxtCashBank.Enabled = false;
                txtQty.Enabled = false;
                txtCheckDetail_Id.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtChq_Caption.Enabled = false;
                txtAmount.Enabled = false;
                txtNarration.Enabled = false;
                txtBank_Date.Enabled = false;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtAc_Id.Enabled = false;
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
                txtEditDoc_no.Enabled = false;
                txtDoc_No.Enabled = false;
                ViewState["currentTable"] = null;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                txtCheckDetail_Id.Enabled = true;
                txtAc_Code.Enabled = true;
                lblAc_Name.Text = string.Empty;
                btntxtAc_Code.Enabled = true;
                txtChq_Caption.Enabled = true;
                txtAmount.Enabled = true;
                txtNarration.Enabled = true;
                txtBank_Date.Enabled = true;
                txtBank_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtBank_Date.Enabled = true;
                txtAc_Id.Enabled = true;
                txtCheck_Id.Enabled = true;
                txtCheckFrom.Enabled = true;
                txtCheckTo.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtPayment_No.Enabled = true;
                lblPayment_No.Text = string.Empty;
                btntxtPayment_No.Enabled = true;
                txtCashBank.Enabled = true;
                lblCashBank.Text = string.Empty;
                btntxtCashBank.Enabled = true;
                txtQty.Enabled = true;
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
                txtEditDoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                txtCheck_Id.Enabled = false;
                txtCheckFrom.Enabled = false;
                txtCheckTo.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtPayment_No.Enabled = false;
                btntxtPayment_No.Enabled = false;
                txtCashBank.Enabled = false;
                btntxtCashBank.Enabled = false;
                txtQty.Enabled = false;
                txtCheckDetail_Id.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtChq_Caption.Enabled = false;
                txtAmount.Enabled = false;
                txtNarration.Enabled = false;
                txtBank_Date.Enabled = false;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtAc_Id.Enabled = false;
                txtCheckDetail_Id.Text = string.Empty;
                txtAc_Code.Text = string.Empty;
                btntxtAc_Code.Enabled = false;
                txtChq_Caption.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtNarration.Text = string.Empty;
                txtBank_Date.Text = string.Empty;
                CalendarExtenderDatetxtBank_Date.Enabled = false;
                txtAc_Id.Text = string.Empty;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
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
                txtEditDoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                txtCheck_Id.Enabled = true;
                txtCheckFrom.Enabled = true;
                txtCheckTo.Enabled = true;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtPayment_No.Enabled = true;
                btntxtPayment_No.Enabled = true;
                txtCashBank.Enabled = true;
                btntxtCashBank.Enabled = true;
                txtQty.Enabled = true;
                txtCheckDetail_Id.Enabled = true;
                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                txtChq_Caption.Enabled = true;
                txtAmount.Enabled = true;
                txtNarration.Enabled = true;
                txtBank_Date.Enabled = true;
                CalendarExtenderDatetxtBank_Date.Enabled = true;
                txtAc_Id.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
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
        query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'";
        string cnt = clsCommon.getString(query);
        if (cnt != string.Empty)
        {
            RecordCount = Convert.ToInt32(cnt);
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
            #region check for next or previous record exist or not
            query = "SELECT top 1 [Doc_No] from " + tblHead +
                " where Check_Id>" + Convert.ToInt32(hdnf.Value) + 
                " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                "' ORDER BY Doc_No asc  ";
            string strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty)
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            query = "SELECT top 1 [Doc_No] from " + tblHead +
                " where Check_Id<" + Convert.ToInt32(hdnf.Value) +
                " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                "' ORDER BY Doc_No desc  ";
            if (strDoc_No != string.Empty)
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
        }
            #endregion
        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
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
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
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
            query = "select Check_Id from " + tblHead + " where Check_Id=(select MAX(Check_Id) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
        setFocusControl(txtDoc_No);
        pnlPopupDetails.Style["display"] = "none";
        Int32 Doc_No = Convert.ToInt32(clsCommon.getString("select count(doc_no) as Doc_No  From " + tblHead + " "));
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
        hdnf.Value = Request.QueryString["Check_Id"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select  isnull(max(Check_Id),0) as Check_Id  from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'"));
            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgeCheckPrintNew.aspx?Check_Id=" + hdnf.Value + "&Action=" + 1);
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
                                lblCreatedDate.Text = "Created Date" + DateTime.Parse(dt.Rows[0]["Created_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
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
                                lblModifiedDate.Text = "Modified Date" + DateTime.Parse(dt.Rows[0]["Modified_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                            }
                        }
                       //txtEditDoc_no.Text = dt.Rows[0]["EditDoc_no"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        //lbldoc_no.Text = dt.Rows[0][""].ToString();
                        txtCheck_Id.Text = dt.Rows[0]["Check_Id"].ToString();
                        txtCheckFrom.Text = dt.Rows[0]["CheckFrom"].ToString();
                        txtCheckTo.Text = dt.Rows[0]["CheckTo"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_Date"].ToString();
                        txtPayment_No.Text = dt.Rows[0]["Payment_No"].ToString();
                       // lblPayment_No.Text = dt.Rows[0][""].ToString();
                        txtCashBank.Text = dt.Rows[0]["CashBank"].ToString();
                       // lblCashBank.Text = dt.Rows[0][""].ToString();
                        txtQty.Text = dt.Rows[0]["Qty"].ToString();

                        hdnfpidid.Value = dt.Rows[0]["pid"].ToString();
                        hdnfcbidid.Value = dt.Rows[0]["cbid"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select Detail_Id as ID ,CheckDetail_Id,Ac_Code,Chq_Caption, 0 as Ac_Code_Name,Amount,Narration,Bank_Date,Ac_Id from " + qryCommon +
                            " where Check_Id=" + hdnf.Value + " and Company_Code=" + Session["Company_Code"].ToString() +
                            " and Year_Code=" + Session["year"].ToString() + "";
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
                        #endregion
                        pnlgrdDetail.Enabled = false;
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
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_no")
            {
                setFocusControl(txtEditDoc_no);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "txtCheck_Id")
            {
                setFocusControl(txtCheck_Id);
            }
            if (strTextBox == "txtCheckFrom")
            {
                setFocusControl(txtCheckFrom);
            }
            if (strTextBox == "txtCheckTo")
            {
                setFocusControl(txtCheckTo);
            }
            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    if (dt == "")
                    {
                        setFocusControl(txtDoc_Date);
                    }
                    else
                    {
                        txtDoc_Date.Text = "";
                        setFocusControl(txtDoc_Date);
                    }
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtPayment_No")
            {
                setFocusControl(txtPayment_No);
            }
            if (strTextBox == "txtCashBank")
            {
                setFocusControl(txtCashBank);
            }
            if (strTextBox == "txtQty")
            {
                setFocusControl(txtQty);
            }
            if (strTextBox == "txtCheckDetail_Id")
            {
                setFocusControl(txtCheckDetail_Id);
            }
            if (strTextBox == "txtAc_Code")
            {
                setFocusControl(txtAc_Code);
            }
            if (strTextBox == "txtChq_Caption")
            {
                setFocusControl(txtChq_Caption);
            }
            if (strTextBox == "txtAmount")
            {
                setFocusControl(txtAmount);
            }
            if (strTextBox == "txtNarration")
            {
                setFocusControl(txtNarration);
            }
            if (strTextBox == "txtBank_Date")
            {
                setFocusControl(txtBank_Date);
            }
            if (strTextBox == "txtAc_Id")
            {
                setFocusControl(txtAc_Id);
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
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Check_Id=" + hdnf.Value;
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
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_Id"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from " + tblDetails + " where Detail_Id='" + lblID.Text + "' and Detail_Id=" + lblID.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
                    dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("CheckDetail_Id", typeof(int))));
                    dt.Columns.Add((new DataColumn("Ac_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Ac_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Chq_Caption", typeof(string))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("Bank_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Ac_Id", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
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
                dt.Columns.Add((new DataColumn("CheckDetail_Id", typeof(int))));
                dt.Columns.Add((new DataColumn("Ac_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Ac_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Chq_Caption", typeof(string))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                dt.Columns.Add((new DataColumn("Bank_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("Ac_Id", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            if (txtCheckDetail_Id.Text != string.Empty)
            {
                dr["CheckDetail_Id"] = txtCheckDetail_Id.Text;
            }
            else
            {
                setFocusControl(txtCheckDetail_Id);
                return;
            }
            if (txtAc_Code.Text != string.Empty)
            {
                dr["Ac_Code"] = txtAc_Code.Text;
            }
            else
            {
                setFocusControl(txtAc_Code);
                return;
            }
            dr["Ac_Code_Name"] = lblAc_Name.Text;
            if (txtChq_Caption.Text != string.Empty)
            {
                dr["Chq_Caption"] = txtChq_Caption.Text;
            }
            else
            {
                setFocusControl(txtChq_Caption);
                return;
            }
            if (txtAmount.Text != string.Empty)
            {
                dr["Amount"] = txtAmount.Text;
            }
            else
            {
                setFocusControl(txtAmount);
                return;
            }
            if (txtNarration.Text != string.Empty)
            {
                dr["Narration"] = txtNarration.Text;
            }
            else
            {
                setFocusControl(txtNarration);
                return;
            }
            if (txtBank_Date.Text != string.Empty)
            {
                dr["Bank_Date"] = txtBank_Date.Text;
            }
            else
            {
                setFocusControl(txtBank_Date);
                return;
            }
            if (txtAc_Id.Text != string.Empty)
            {
                dr["Ac_Id"] = txtAc_Id.Text;
            }
            else
            {
                setFocusControl(txtAc_Id);
                return;
            }
            #endregion
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
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtCheckDetail_Id);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            txtCheckDetail_Id.Text = string.Empty;
            txtAc_Code.Text = string.Empty;
            txtChq_Caption.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtNarration.Text = string.Empty;
            txtBank_Date.Text = string.Empty;
            txtAc_Id.Text = string.Empty;
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
        txtCheckDetail_Id.Text = string.Empty;
        txtAc_Code.Text = string.Empty;
        txtChq_Caption.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtNarration.Text = string.Empty;
        txtBank_Date.Text = string.Empty;
        txtAc_Id.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtCheckDetail_Id);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtCheckDetail_Id.Text = Server.HtmlDecode(gvrow.Cells[CheckDetail_Id].Text);
        txtAc_Code.Text = Server.HtmlDecode(gvrow.Cells[Ac_Code].Text);
        lblAc_Name.Text = Server.HtmlDecode(gvrow.Cells[5].Text); 
        txtChq_Caption.Text = Server.HtmlDecode(gvrow.Cells[Chq_Caption].Text);
        txtAmount.Text = Server.HtmlDecode(gvrow.Cells[Amount].Text);
        txtNarration.Text = Server.HtmlDecode(gvrow.Cells[Narration].Text);
        txtBank_Date.Text = Server.HtmlDecode(gvrow.Cells[Bank_Date].Text);
        txtAc_Id.Text = Server.HtmlDecode(gvrow.Cells[Ac_Id].Text);
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
                string IDExisting = clsCommon.getString("select Detail_Id from " + tblDetails + " where Doc_No='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            // e.Row.Cells[2].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("10px");
            //--------------------------------------------------
            //e.Row.Cells[Detail_Id].ControlStyle.Width = new Unit("120px");
            //e.Row.Cells[Detail_Id].Style["overflow"] = "hidden";
            //e.Row.Cells[Detail_Id].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[3].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[4].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[5].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[5].Style["overflow"] = "hidden";
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[6].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[6].Style["overflow"] = "hidden";
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[7].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[7].Style["overflow"] = "hidden";
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[8].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[8].Style["overflow"] = "hidden";
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[9].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[9].Style["overflow"] = "hidden";
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[10].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[10].Style["overflow"] = "hidden";
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left; 
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            //    e.Row.Cells[0].Visible =true;
            //}
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
                            setFocusControl(txtCheckDetail_Id);
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
    #region [txtEditDoc_no_TextChanged]
    protected void txtEditDoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEditDoc_no.Text;
        strTextBox = "txtEditDoc_no";
        csCalculations();
    }
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();
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
    #region [txtCheck_Id_TextChanged]
    protected void txtCheck_Id_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheck_Id.Text;
        strTextBox = "txtCheck_Id";
        csCalculations();
    }
    #endregion
    #region [txtCheckFrom_TextChanged]
    protected void txtCheckFrom_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheckFrom.Text;
        strTextBox = "txtCheckFrom";
        csCalculations();
    }
    #endregion
    #region [txtCheckTo_TextChanged]
    protected void txtCheckTo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheckTo.Text;
        strTextBox = "txtCheckTo";
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
    #region [txtPayment_No_TextChanged]
    protected void txtPayment_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPayment_No.Text;
        strTextBox = "txtPayment_No";
        csCalculations();
    }
    #endregion
    #region [btntxtPayment_No_Click]
    protected void btntxtPayment_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPayment_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtCashBank_TextChanged]
    protected void txtCashBank_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCashBank.Text;
        strTextBox = "txtCashBank";
        csCalculations();
    }
    #endregion
    #region [btntxtCashBank_Click]
    protected void btntxtCashBank_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCashBank";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
    }
    #endregion

    #region [txtCheckDetail_Id_TextChanged]
    protected void txtCheckDetail_Id_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCheckDetail_Id.Text;
        strTextBox = "txtCheckDetail_Id";
        csCalculations();
    }
    #endregion


    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
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

    #region [txtChq_Caption_TextChanged]
    protected void txtChq_Caption_TextChanged(object sender, EventArgs e)
    {
        searchString = txtChq_Caption.Text;
        strTextBox = "txtChq_Caption";
        csCalculations();
    }
    #endregion


    #region [txtAmount_TextChanged]
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAmount.Text;
        strTextBox = "txtAmount";
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


    #region [txtAc_Id_TextChanged]
    protected void txtAc_Id_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Id.Text;
        strTextBox = "txtAc_Id";
        csCalculations();
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
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_no")
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
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and code='%like" + txtSearchText.Text + "' order by Doc_No";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtDoc_No")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPayment_No")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtCashBank")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ABC", "javascript:pagevalidation();", true);
    }
    #endregion
}

