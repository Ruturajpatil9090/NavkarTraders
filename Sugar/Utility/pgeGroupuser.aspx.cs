using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

public partial class Sugar_Utility_pgeGroupuser_ : System.Web.UI.Page
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
    string Tran_Type = "GI";             //Local Voucher
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            tblPrefix = Session["tblPrefix"].ToString();
            user = Session["user"].ToString();
            tblHead = "groupuser";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            qryAccountList = "qrymstaccountmaster";
            qryCommon = "groupuser";
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
                        hdnf.Value = Request.QueryString["Doc_No"];

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
                        setFocusControl(txtGroup_Name);
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
                string qry = "select * from " + qryCommon + " where  and Doc_No='" + txtEditDoc_No.Text + "'";
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
                obj.tableName = tblHead + " where  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
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
                                    txtGroup_Code.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtGroup_Code.Enabled = false;
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
                btntxtGroup_Code.Text = "Choose No";
                btntxtGroup_Code.Enabled = false;
                lblMsg.Text = string.Empty;
                txtPassword.Attributes["value"] ="";
                #region set Business logic
               


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
                btntxtGroup_Code.Text = "Change No";
                btntxtGroup_Code.Enabled = true;
                txtGroup_Code.Enabled = false;
                lblMsg.Text = "";

                txtEditDoc_No.Enabled = false;
                btntxtGroup_Code.Enabled = false;
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
                btntxtGroup_Code.Text = "Choose No";
                btntxtGroup_Code.Enabled = false;
                txtEditDoc_No.Enabled = true;

                #region set Business logic for save
               
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
                btntxtGroup_Code.Text = "Choose No";
                btntxtGroup_Code.Enabled = true;
                lblMsg.Text = string.Empty;
                #region logic
              
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + ")";
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
            if (txtGroup_Code.Text != string.Empty)
            {
                string query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + " ORDER BY Doc_No DESC";
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
            if (txtGroup_Code.Text != string.Empty)
            {
                string query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " ORDER BY Doc_No asc  ";
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + ")";

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
                txtGroup_Code.Text = hdnf.Value;
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
        setFocusControl(txtGroup_Name);
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

        btnGenEinvoice.Enabled = false;
        txtGroup_Code.Enabled = false;
        setFocusControl(txtGroup_Name);

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

        hdnf.Value = Request.QueryString["Doc_No"];

        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(Doc_No),0) as saleid from OtherInvoice "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgeGroupuser.aspx?Doc_No=" + hdnf.Value + "&Action=" + 1);
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

                        //hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        //hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtGroup_Code.Text = dt.Rows[0]["Doc_No"].ToString();
                       // hdnflvdoc.Value = txtdoc_no.Text;
                        lblBill_Id.Text = dt.Rows[0]["Doc_No"].ToString();
                        //hdnfbillid.Value = lblBill_Id.Text;
                        hdnf.Value = dt.Rows[0]["Doc_No"].ToString();

                        txtGroup_Name.Text = dt.Rows[0]["Group_Name"].ToString();
                        txtLogin_Name.Text = dt.Rows[0]["Login_Name"].ToString();
                        txtPassword.Attributes["value"] = dt.Rows[0]["Password"].ToString();
                        string User_Type = dt.Rows[0]["UserType"].ToString();
                        if (User_Type == "U")
                        {
                            drpUser_Type.SelectedValue = "U";
                        }
                        else
                        {
                            drpUser_Type.SelectedValue = "A";
                        }

                        hdnf.Value = txtGroup_Code.Text;
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
            if (v == "txtAC_CODE" || v == "txtSale_Code")
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(60);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(30);
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[0].Style["overflow"] = "hidden";
                e.Row.Cells[1].Style["overflow"] = "hidden";
                e.Row.Cells[2].Style["overflow"] = "hidden";
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
            string qryDisplay = "select * from " + qryCommon + " where  Doc_No=" + hdnf.Value + " ";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [txtGroup_Code_TextChanged]
    protected void txtGroup_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGroup_Code.Text;
        strTextBox = "txtGroup_Code";
        csCalculations();
    }
    #endregion



    #region [btntxtGroup_Code_Click]
    protected void btntxtGroup_Code_Click(object sender, EventArgs e)
    {
        try
        {
            if (btntxtGroup_Code.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtGroup_Code.Text = string.Empty;
                txtGroup_Code.Enabled = true;
                btnSave.Enabled = false;
                setFocusControl(txtGroup_Code);
            }
            if (btntxtGroup_Code.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtGroup_Code";
                btnSearch_Click(sender, e);

            }
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGroup_Name_TextChanged]
    protected void txtGroup_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGroup_Name.Text;
        strTextBox = "txtGroup_Name";
        csCalculations();
    }
    #endregion

    #region [txtLogin_Name_TextChanged]
    protected void txtLogin_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLogin_Name.Text;
        strTextBox = "txtLogin_Name";
        csCalculations();
    }
    #endregion
    #region [txtPassword_TextChanged]
    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
        hdnfpass.Value = txtPassword.Text;
        searchString = txtPassword.Text;
        strTextBox = "txtPassword";
        csCalculations();
    }
    #endregion
    #region [drpUser_Type_TextChanged]
    protected void drpUser_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
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

            if (hdnfClosePopup.Value == "txtGroup_Code")
            {
                lblPopupHead.Text = "--Select Voucher--";
                string qry = "SELECT  " + tblHead + ".Doc_No, " + tblHead + ".Suffix, " + tblHead + ".DO_No, Convert(varchar(10)," + tblHead + ".Doc_Date,103) as Doc_Date, " +
                " Party.Ac_Name_E AS PartyName, " + tblHead + ".Quantal FROM  " + AccountMasterTable + " AS Party left outer JOIN " +
                " " + tblHead + " ON Party.Ac_Code = " + tblHead + ".Ac_Code and Party.Company_Code = " + tblHead + ".Company_Code where " + tblHead + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and " + tblHead + ".Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    " and " + tblHead + ".Tran_Type='" + Tran_Type + "' and (Party.Ac_Name_E like '%" + searchString + "%' or " + tblHead + ".Doc_No like '%" + searchString + "%')";
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

        #region [Validation Part]
        bool isValidated = true;
        if (txtGroup_Name.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtGroup_Name);
            return;
        }
        if (txtLogin_Name.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtLogin_Name);
            return;
        }
        //if (txtPassword.Text != string.Empty)
        //{
        //    isValidated = true;
        //}
        //else
        //{
        //    isValidated = false;
        //    setFocusControl(txtPassword);
        //    return;
        //}


        #endregion

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:pagevalidation();", true);

    }
    #endregion
    

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtGroup_Code")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtGroup_Code.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtGroup_Code.Text != string.Empty)
                        {
                            txtValue = txtGroup_Code.Text;

                            string qry = "select * from " + tblHead + " where   txtGroup_Code='" + txtValue + "'";
                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Record Found
                                        hdnf.Value = dt.Rows[0]["txtGroup_Code"].ToString();
                                        // hdnfSuffix.Value = dt.Rows[0]["Suffix"].ToString();
                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                //this.getMaxCode();
                                                txtGroup_Code.Enabled = false;
                                                btnSave.Enabled = true;   //IMP
                                                //txtSUFFIX.Text = string.Empty;
                                                //setFocusControl(txtSUFFIX);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = "select * from " + qryCommon + " where txtGroup_Code=" + hdnf.Value;

                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtGroup_Code.Enabled = false;
                                                    setFocusControl(txtGroup_Name);
                                                    hdnf.Value = txtGroup_Code.Text;
                                                    //hdnfSuffix.Value = txtSUFFIX.Text.Trim();
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtGroup_Name);
                                            txtGroup_Code.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtGroup_Code.Text = string.Empty;
                                            setFocusControl(txtGroup_Code);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtGroup_Code);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtGroup_Code.Text = string.Empty;
                        setFocusControl(txtGroup_Code);
                    }
                }
                catch
                {

                }
                #endregion
            }


            if (strTextBox == "txtGroup_Name")
            {
                setFocusControl(txtLogin_Name);
            }
            if (strTextBox == "txtLogin_Name")
            {

                setFocusControl(txtPassword);
            }
            if (strTextBox == "txtPassword")
            {
                setFocusControl(drpUser_Type);
            }
            if (strTextBox == "drpUser_Type")
            {
                setFocusControl(btnSave);
            }

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

        //string party = txtAC_CODE.Text;
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:p('" + txtdoc_no.Text + "')", true);
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
            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_No) as Doc_No from " + tblHead + ""));
            if (counts == 0)
            {
                txtGroup_Code.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead)) + 1;
                txtGroup_Code.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(Doc_No) as Doc_No from " + tblHead + " "));
            if (counts == 0)
            {
                lblBill_Id.Text = "1";
                LV_Id = 1;
            }
            else
            {
                LV_Id = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead)) + 1;
                lblBill_Id.Text = LV_Id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);

        }
    }
    #endregion

}