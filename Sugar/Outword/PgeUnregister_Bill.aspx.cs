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

public partial class Sugar_Outword_PgeUnregister_Bill : System.Web.UI.Page
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
            tblHead = "dbo.OtherInvoice";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            qryAccountList = "qrymstaccountmaster";
            qryCommon = "qryOtherInvoice";
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
                        hdnf.Value = Request.QueryString["bill_id"];
                     
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
                        setFocusControl(txtAC_CODE);
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
                string qry = "select * from " + qryCommon + " where  and doc_no='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Session["year"].ToString() + "";
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
                lblSale_name.Text = string.Empty;
                txtEditDoc_No.Enabled = true;
                btntxtAC_CODE.Enabled = false;
                btntxtSale_Code.Enabled = false;
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
                lblAc_name.Text = string.Empty;
                lblSale_name.Text = string.Empty;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btntxtAC_CODE.Enabled = true;
                btntxtSale_Code.Enabled = true;
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
                btntxtAC_CODE.Enabled = false;
                btntxtSale_Code.Enabled = false;
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
                btntxtAC_CODE.Enabled = true;
                btntxtSale_Code.Enabled = true;
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                    " and   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MAX(doc_no) from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")  " +
                " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

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
        setFocusControl(txtAC_CODE);
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
        txtdoc_no.Enabled = false;
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

        hdnf.Value = Request.QueryString["bill_id"];
       
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(bill_id),0) as saleid from OtherInvoice "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("PgeUnregister_Bill.aspx?bill_id=" + hdnf.Value + "&Action=" + 1);
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
                        lblBill_Id.Text = dt.Rows[0]["bill_id"].ToString();
                        hdnfbillid.Value = lblBill_Id.Text;
                        hdnf.Value = dt.Rows[0]["bill_id"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtAC_CODE.Text = dt.Rows[0]["AC_CODE"].ToString();
                        lblAc_name.Text = dt.Rows[0]["Ac_Name"].ToString();
                        txtSale_Code.Text = dt.Rows[0]["sale_code"].ToString();
                        lblSale_name.Text = dt.Rows[0]["Sale_Name"].ToString();
                        txtDescroption.Text = dt.Rows[0]["narration"].ToString();
                        txtAmount.Text = dt.Rows[0]["amount"].ToString();
                        txtOther_narration.Text = dt.Rows[0]["other_narration"].ToString();

                        hdnfAcode.Value = dt.Rows[0]["ac"].ToString();
                        hdnfsalecode.Value = dt.Rows[0]["sa"].ToString();

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
            string qryDisplay = "select * from " + qryCommon + " where  bill_id=" + hdnf.Value + " ";
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

    protected void txtSale_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSale_Code.Text;
        strTextBox = "txtSale_Code";
        csCalculations();
    }


    protected void btntxtSale_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSale_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }




    #region [txtDescroption_TextChanged]
    protected void txtDescroption_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDescroption.Text;
        strTextBox = "txtDescroption";
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
            
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {

                lblPopupHead.Text = "--Select Party--";
                txtSearchText.Text = txtAC_CODE.Text;
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0 and " +
               " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtSale_Code")
            {
                lblPopupHead.Text = "--Select Transport--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Session["Company_Code"].ToString() + " and Locked=0  and " +
               " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
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
                            setFocusControl(txtSale_Code);
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
            if (strTextBox == "txtSale_Code")
            {
                string acname = "";
                if (txtSale_Code.Text != string.Empty)
                {
                    if (!clsCommon.isStringIsNumeric(txtSale_Code.Text))
                    {
                        btntxtSale_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        hdnfsalecode.Value = clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtSale_Code.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtSale_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (acname != string.Empty)
                            {

                                lblSale_name.Text = acname;
                                setFocusControl(txtDescroption);
                                // txtTO_STATION.Text = clsCommon.getString("select CityName from " + qryAccountList + " where Ac_Code=" + txtUnit_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            }
                            else
                            {
                                txtSale_Code.Text = string.Empty;
                                lblSale_name.Text = acname;
                                setFocusControl(txtSale_Code);
                            }
                        }
                    
                }
                else
                {
                    setFocusControl(txtSale_Code);
                }
            }
           

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

                            string qry = "select * from " + tblHead + " where   Doc_No='" + txtValue + "' " +
                                "  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and " +
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
                                       // hdnfSuffix.Value = dt.Rows[0]["Suffix"].ToString();
                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                //this.getMaxCode();
                                                txtdoc_no.Enabled = false;
                                                btnSave.Enabled = true;   //IMP
                                                //txtSUFFIX.Text = string.Empty;
                                                //setFocusControl(txtSUFFIX);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " " +
                                                   "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                                                  " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    setFocusControl(txtAC_CODE);
                                                    hdnf.Value = txtdoc_no.Text;
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
                                            setFocusControl(txtAC_CODE);
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
            

            if (strTextBox == "txtDOC_DATE")
            {
                setFocusControl(txtAC_CODE);
            }
            if (strTextBox == "txtAmount")
            {
               
                setFocusControl(txtOther_narration);
            }
            if (strTextBox == "txtOther_narration")
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
       
        string party = txtAC_CODE.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:p('" + txtdoc_no.Text + "')", true);
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

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(bill_id) as bill_id from " + tblHead + " "));
            if (counts == 0)
            {
                lblBill_Id.Text = "1";
                LV_Id = 1;
            }
            else
            {
                LV_Id = Convert.ToInt32(clsCommon.getString("SELECT max(bill_id) as bill_id from " + tblHead)) + 1;
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