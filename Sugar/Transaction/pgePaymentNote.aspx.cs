using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Services;

public partial class Sugar_Transaction_pgePaymentNote : System.Web.UI.Page
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
            tblHead = "PaymentNote";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            qryAccountList = "qrymstaccountmaster";
            qryCommon = "qryPaymentNote";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            //hdnfcommisiontype.Value = Request.QueryString["Tran_Type"];
            //drpVouchertype.SelectedValue = hdnfcommisiontype.Value;
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
                        hdnf.Value = Request.QueryString["pid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        //setFocusControl(btnEdit);
                        setFocusControl(btnAdd);
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtBank);
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
                string qry = "select * from " + qryCommon + " where doc_no='" + txtEditDoc_No.Text + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Session["year"].ToString() + "";
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
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
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
                lblBank.Text = string.Empty;
                lblPayment_ToName.Text = string.Empty;
                txtEditDoc_No.Enabled = true;
                btntxtPayment_To.Enabled = false;
                btntxtBank.Enabled = false;
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
                txtEditDoc_No.Enabled = false;
                btntxtDOC_NO.Enabled = false;
                #region set Business logic for save
                lblBank.Text = string.Empty;
                lblPayment_ToName.Text = string.Empty;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btntxtBank.Enabled = true;
                btntxtPayment_To.Enabled = true;
                calenderExtenderDate.Enabled = true;

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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                txtEditDoc_No.Enabled = true;

                #region set Business logic for save

                btntxtBank.Enabled = false;
                btntxtPayment_To.Enabled = false;
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

                btntxtBank.Enabled = true;
                btntxtPayment_To.Enabled = true;
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
            string qry = string.Empty;
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                //btnEdit.Focus();
                btnAdd.Focus();
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

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.NextNumber();
        setFocusControl(txtBank);
        //txtGSTRateCode.Text = "2";
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        pnlPopupDetails.Style["display"] = "none";

    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
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
        hdnf.Value = Request.QueryString["pid"];

        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(pid),0) as pid from PaymentNote "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgePaymentNote.aspx?pid=" + hdnf.Value + "&Action=" + 1);
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
                        txtdoc_no.Text = dt.Rows[0]["doc_no"].ToString();
                        hdnflvdoc.Value = txtdoc_no.Text;                     
                        hdnflvid.Value = dt.Rows[0]["pid"].ToString();
                        hdnf.Value = dt.Rows[0]["pid"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtBank.Text = dt.Rows[0]["bank_ac"].ToString();
                        lblBank.Text = dt.Rows[0]["Bank_Name"].ToString();
                        txtPayment_To.Text = dt.Rows[0]["payment_to"].ToString();
                        lblPayment_ToName.Text = dt.Rows[0]["PaymentToName"].ToString();
                        txtamount.Text = dt.Rows[0]["amount"].ToString();
                        txtnarration.Text = dt.Rows[0]["narration"].ToString();

                        hdnfbankcode.Value = dt.Rows[0]["ba"].ToString();
                        hdnfpayto.Value = dt.Rows[0]["pt"].ToString();

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtBank" || v == "txtPayment_To")
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
            string qryDisplay = "select * from " + qryCommon + " where  pid=" + hdnf.Value + "";
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

    #region [txtDOC_DATE_TextChanged]
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_DATE.Text;
        strTextBox = "txtDOC_DATE";
        csCalculations();
    }
    #endregion

    #region [txtBank_TextChanged]
    protected void txtBank_TextChanged(object sender, EventArgs e)
    {
        strTextBox = "txtBank";
        searchString = txtBank.Text;
        csCalculations();
    }
    #endregion

    #region [btntxtBank_Click]
    protected void btntxtBank_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBank";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    protected void txtPayment_To_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPayment_To.Text;
        strTextBox = "txtPayment_To";
        csCalculations();
    }

    #region [btntxtPayment_To_Click]
    protected void btntxtPayment_To_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPayment_To";
            btnSearch_Click(sender, e);
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
            if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
                searchString = txtSearchText.Text;
            }

            if (hdnfClosePopup.Value == "txtBank")
            {

                lblPopupHead.Text = "--Select Party--";
                txtSearchText.Text = txtBank.Text;
               // string qry = "select Ac_Code,Ac_Name_E from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0 and " +
               //" (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";
               // this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }

            if (hdnfClosePopup.Value == "txtPayment_To")
            {

                lblPopupHead.Text = "--Select Broker--";
                txtSearchText.Text = txtPayment_To.Text;

               // string qry = "select Ac_Code,Ac_Name_E from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0 and " +
               //" (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
               // this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
           + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";

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
   
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtBank")
            {
                string partyName = string.Empty;
                if (txtBank.Text != string.Empty)
                {
                    searchString = txtBank.Text;
                    if (!clsCommon.isStringIsNumeric(searchString))
                    {
                        btntxtBank_Click(this, new EventArgs());
                    }
                    else
                    {
                        string qry = "";
                        hdnfbankcode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtBank.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        qry = "select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtBank.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        partyName = clsCommon.getString(qry);

                        if (partyName != string.Empty)
                        {
                            lblBank.Text = partyName;
                            setFocusControl(txtPayment_To);
                        }
                        else
                        {
                            lblBank.Text = string.Empty;
                            txtBank.Text = string.Empty;
                            setFocusControl(txtBank);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBank);
                }
            }
            if (strTextBox == "txtPayment_To")
            {
                string acname = "";
                if (txtPayment_To.Text != string.Empty)
                {
                    if (!clsCommon.isStringIsNumeric(txtPayment_To.Text))
                    {
                        btntxtPayment_To_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfpayto.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtPayment_To.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        string iscarporate = clsCommon.getString("select carporate_party from " + qryAccountList + " where Ac_Code=" + txtBank.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (iscarporate == "Y")
                        {
                            lblMsg.Text = "";
                            lblPopupHead.Text = "--Select Unit--";

                            acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtPayment_To.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (acname != string.Empty)
                            {
                                lblPayment_ToName.Text = acname;
                                setFocusControl(txtamount);
                                //txtTo_Place.Text = clsCommon.getString("select CityName from " + tblPrefix + "qryAccountsList where Ac_Code=" + txtAC_CODE.Text);
                            }
                            else
                            {
                                txtPayment_To.Text = string.Empty;
                                lblPayment_ToName.Text = acname;
                                setFocusControl(txtPayment_To);
                            }
                        }
                        else
                        {
                            acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtPayment_To.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (acname != string.Empty)
                            {

                                lblPayment_ToName.Text = acname;
                                setFocusControl(txtamount);
                                // txtTO_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            }
                            else
                            {
                                txtPayment_To.Text = string.Empty;
                                lblPayment_ToName.Text = acname;
                                setFocusControl(txtPayment_To);
                            }
                        }
                    }
                }
                else
                {
                    setFocusControl(txtPayment_To);
                }
            }

            if (strTextBox == "txtamount")
            {
                setFocusControl(txtnarration);
            }

            if (strTextBox == "txtnarration")
            {

                setFocusControl(btnSave);
            }

            #region [Calculation Part]

            double amt = Convert.ToDouble(txtamount.Text != string.Empty ? txtamount.Text : "0.00");
        }
        catch
        {
        }
    }
            #endregion
    #endregion
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string doc_no = txtdoc_no.Text;
        string paymentto = txtPayment_To.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kys", "javascript:PaymentToReport('" + doc_no + "','" + paymentto + "')", true);
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

            //counts = Convert.ToInt32(clsCommon.getString("SELECT count(pid) as pid from " + tblHead + " "));
            //if (counts == 0)
            //{
            //    lblLV_Id.Text = "1";
            //    LV_Id = 1;
            //}
            //else
            //{
            //    LV_Id = Convert.ToInt32(clsCommon.getString("SELECT max(pid) as pid from " + tblHead)) + 1;
            //    lblLV_Id.Text = LV_Id.ToString();
            //}
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);

        }
    }
    #endregion
}