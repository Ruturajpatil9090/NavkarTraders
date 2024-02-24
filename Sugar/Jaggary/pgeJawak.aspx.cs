using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;

public partial class Sugar_pgeJawak : System.Web.UI.Page
{

    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string systemMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string voucherTable = string.Empty;
    string qryVoucherList = string.Empty;
    int defaultAccountCode = 0;
    string trntype = string.Empty;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    static WebControl objAsp = null;
    int Doc_No = 0;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "Awakhead";
            tblDetails = tblPrefix + "AwakDetail";
            AccountMasterTable = tblPrefix + "AccountMaster";
            qryCommon = tblPrefix + "qryAwak";
            qryAccountList = "qrymstaccountmaster";
            cityMasterTable = tblPrefix + "CityMaster";
            systemMasterTable = "NT_1_JaggarySystemMaster";
            voucherTable = tblPrefix + "Voucher";
            qryVoucherList = tblPrefix + "qryVoucherList";
            trntype = "JW";
            // trntype = drpTrnType.SelectedValue;
            user = Session["user"].ToString();
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";

            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    if (Session["RP_NO"] != null)
                    {
                        hdnf.Value = Session["RP_NO"].ToString();
                        trntype = Session["RP_TYPE"].ToString();
                        //   drpTrnType.SelectedValue = Session["RP_TYPE"].ToString();
                        qry = getDisplayQuery();
                        this.fetchRecord(qry);
                        this.enableDisableNavigateButtons();

                        Session["RP_NO"] = null;
                        Session["RP_TYPE"] = null;
                    }
                    else
                    {
                        this.showLastRecord();
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

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from " + tblHead + "  where TRAN_TYPE='JW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
            if (counts == 0)
            {
                txtdoc_no.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(doc_no),0) as Doc_No from " + tblHead + " where TRAN_TYPE='JW' and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "")) + 1;
                txtdoc_no.Text = Doc_No.ToString();
            }


        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
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
                drpCsCr.Enabled = false;
                calenderExtenderDate.Enabled = false;
                //btntxtSupplier.Enabled = false;
                btntxtCustCode.Enabled = false;
                btntxtItemCode.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtEditDoc_No.Enabled = true;
                //btnSave.Enabled = true;
                btnawakno.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                btntxtPacking_Code.Enabled = false;
                btntxtHamali_Code.Enabled = false;

                ViewState["currentTable"] = null;
                // grdDetail.DataSource = null;
                //  grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                //btnFirst.Enabled = true;
                //btnPrevious.Enabled = true;
                lblCustomer.Text = string.Empty;
                lblAwakYearCode.Text = string.Empty;
                lblAwakQuantity.Text = string.Empty;
                //txtTotal.Text = string.Empty;
                // btnDelete.Enabled = true;


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
                #region set Business logic for save
                ViewState["currentTable"] = null;
                //grdDetail.DataSource = null;
                //grdDetail.DataBind();
                setFocusControl(drpCsCr);
                drpCsCr.Enabled = true;
                calenderExtenderDate.Enabled = true;
                //btntxtSupplier.Enabled = true;
                btntxtCustCode.Enabled = true;
                btntxtItemCode.Enabled = true;
                btnawakno.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                btntxtPacking_Code.Enabled = true;
                btntxtHamali_Code.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtEditDoc_No.Enabled = false;
                //btnprint.Enabled = false;

                // btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;

                //lblSupplier.Text = string.Empty;
                lblCustomer.Text = string.Empty;
                lblItem.Text = string.Empty;
                lblPurchaseValue.Text = string.Empty;
                lblSaleValue.Text = string.Empty;
                lblStockValue.Text = string.Empty;

                lblAwakYearCode.Text = string.Empty;
                lblAwakQuantity.Text = string.Empty;
                txtdoc_date.Text = clsCommon.getString("select Convert(varchar(10),DATEADD(day,0,getdate()),103) as d");

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
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                calenderExtenderDate.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                //btntxtSupplier.Enabled = false;
                btntxtCustCode.Enabled = false;
                btntxtItemCode.Enabled = false;
                btnawakno.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                btntxtPacking_Code.Enabled = false;
                btntxtHamali_Code.Enabled = false;
                txtEditDoc_No.Enabled = true;
                //lblCashBank.Text = string.Empty;
                lblItem.Text = string.Empty;
                //lblAwakYearCode.Text = string.Empty;
                // lblAwakQuantity.Text = string.Empty;
                //btnprint.Enabled = true;
                //btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;

                drpCsCr.Enabled = false;
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
                setFocusControl(txtdoc_date);
                calenderExtenderDate.Enabled = true;
                //btntxtSupplier.Enabled = true;
                btntxtItemCode.Enabled = true;
                txtItemCode.Enabled = true;
                btntxtCustCode.Enabled = true;
                btnAdddetails.Enabled = true;
                btnawakno.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                btntxtPacking_Code.Enabled = true;
                btntxtHamali_Code.Enabled = true;
                btnClosedetails.Enabled = true;
                txtEditDoc_No.Enabled = false;
                //btnprint.Enabled = false;
                //btnOpenDetailsPopup.Enabled = true;
                pnlgrdDetail.Enabled = true;
                txtCustCode.Enabled = true;
                btntxtCustCode.Enabled = true;
                drpCsCr.Enabled = true;


            }
            #region Always check this
            //if (dAction == "A" || dAction == "E")
            //{
            //    if (txtCashBank.Text != String.Empty)
            //    {
            //        txtCashBank.Text = "";
            //        lblCashBank.Text = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=1 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            //    }
            //    //if (drpTrnType.SelectedValue == "CP" || drpTrnType.SelectedValue == "CR")
            //    //{
            //    //    txtCashBank.Text = "1";
            //    //    lblCashBank.Text = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=1 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //    //    txtCashBank.Enabled = true;
            //    //    btntxtCashBank.Enabled = true;
            //    //}
            //    //else
            //    //{
            //    //    txtCashBank.Enabled = true;
            //    //    btntxtCashBank.Enabled = true;
            //    //}
            //}


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
            qry = "select max(doc_no) as doc_no from " + tblHead + " where TRAN_TYPE='JW' and Year_Code="
                + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
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
                        hdnf.Value = dt.Rows[0]["Doc_No"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            btnEdit.Enabled = true;
                            btnDelete.Enabled = true;
                            btnAdd.Focus();
                        }
                        else                     //new code
                        {
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                }
            }
            this.enableDisableNavigateButtons();

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
        query = "   select count(*) from " + tblHead + " where TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
            + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
        if (txtdoc_no.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not

                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                    "  and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY doc_no asc  ";
                string strDoc_No = clsCommon.getString(query);
                if (strDoc_No != "0")
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


                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and TRAN_TYPE='JW' ORDER BY doc_no asc  ";
                strDoc_No = clsCommon.getString(query);
                if (strDoc_No != "0")
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") " +
                "   and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                    " and TRAN_TYPE='JW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY doc_no DESC  ";
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
                    " and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MAX(doc_no) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + 
                " and TRAN_TYPE='JW' and Year_Code="+ Convert.ToInt32(Session["year"].ToString())+
                " ) and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

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
        //this.TranTypeFilter();
        // this.getMaxCode();
        this.NextNumber();
        drpCsCr.SelectedIndex = 1;
        // pnlPopupDetails.Style["display"] = "none";
    }

    private void TranTypeFilter()
    {
        try
        {
            //trntype = drpTrnType.SelectedValue;
            //if (trntype == "BP" || trntype == "CP")
            //{
            //    drpFilter.Visible = true;
            //    drpFilter.Items.Clear();
            //    drpFilter.Items.Add(new ListItem("--Select--", "A"));
            //    drpFilter.Items.Add(new ListItem("Against Transport Advance", "T"));
            //    txtVoucherNo.Enabled = false;
            //    txtvoucherType.Enabled = false;
            //    btntxtVoucherNo.Enabled = false;

            //}
            //else
            //{
            //    if (trntype == "BR")
            //    {
            //        drpFilter.Visible = true;
            //        drpFilter.Items.Clear();
            //        drpFilter.Items.Add(new ListItem("Against Sauda", "S"));
            //        drpFilter.Items.Add(new ListItem("Against Loading Voucher", "V"));
            //        drpFilter.Items.Add(new ListItem("Against Debit Note", "D"));
            //        txtVoucherNo.Enabled = true;
            //        txtvoucherType.Enabled = true;
            //        btntxtVoucherNo.Enabled = true;
            //    }
            //    else
            //    {
            //        drpFilter.Visible = false;
            //    }
            //}
        }
        catch (Exception)
        {
            throw;
        }
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
        txtdoc_no.Enabled = false;
        setFocusControl(txtCustCode);
        fordisablerowuseinsalebill();

    }
    #endregion

    #region [txtLevidetails_TextChanged]
    protected void txtLevidetails_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLevidetails.Text;
        strTextBox = "txtLevidetails";
        csCalculations();
    }
    #endregion

    #region [txtInsurance_TextChanged]
    protected void txtInsurance_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInsurance.Text;
        strTextBox = "txtInsurance";
        csCalculations();
    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region [Validation Part]
        bool isValidated = true;
        string qry = "";
        if (txtdoc_no.Text != string.Empty)
        {
            if (ViewState["mode"] != null)
            {
                if (ViewState["mode"].ToString() == "I")
                {
                    string str = clsCommon.getString("select doc_no from " + tblHead + " where doc_no=" + txtdoc_no.Text + " and TRAN_TYPE='JW' and Year_Code="
                        + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblMsg.Text = "Code " + txtdoc_no.Text + " already exist";

                        this.getMaxCode();
                        hdnf.Value = txtdoc_no.Text;
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }
            }
        }
        else
        {
            isValidated = false;
            setFocusControl(txtdoc_no);
            return;
        }

        if (txtdoc_date.Text != string.Empty)
        {

            if (clsCommon.isValidDate(txtdoc_date.Text) == true)
            {
                isValidated = true;
            }
            else
            {
                txtdoc_date.Text = "";
                isValidated = false;
                return;
            }
        }
        else
        {
            isValidated = false;
            setFocusControl(txtdoc_date);
            return;
        }

        //if (txtSupplier.Text != string.Empty)
        //{
        //    string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtSupplier.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    if (str != string.Empty && str != "0")
        //    {
        //        isValidated = true;
        //    }
        //    else
        //    {
        //        isValidated = false;
        //        setFocusControl(txtSupplier);
        //        return;
        //    }
        //}
        //else
        //{
        //    isValidated = false;
        //    setFocusControl(txtSupplier);
        //    return;
        //}

        int count = 0;
        if (grdDetail.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please Enter Purchase Details!');", true);
            isValidated = false;
            //setFocusControl(btnOpenDetailsPopup);
            return;
        }
        if (grdDetail.Rows.Count >= 1)
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[34].Text == "D" || grdDetail.Rows[i].Cells[34].Text == "R")
                {
                    count++;
                }
            }
            if (grdDetail.Rows.Count == count)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Minimum One Purchase Details is compulsory!');", true);
                isValidated = false;
                // setFocusControl(btnOpenDetailsPopup);
                return;
            }
        }

        //if (drpCsCr.SelectedIndex == 0)
        //{
        //    isValidated = false;
        //    setFocusControl(drpCsCr);
        //    return;
        //}


        double CashAdvacne = 0;// txtCASH_ADVANCE.Text != string.Empty ? Convert.ToDouble(txtCASH_ADVANCE.Text) : 0.00;
        string Transport = "";// txtTransportCode.Text != string.Empty ? txtTransportCode.Text : "0";
        //if (CashAdvacne != 0)
        //{
        //    if (Transport == "0" || string.IsNullOrEmpty(Transport))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Transport Is Compulsory!');", true);
        //        isValidated = false;
        //        setFocusControl(txtTransportCode);
        //        return;
        //    }
        //}
        #endregion

        #region -Head part declearation
        Int32 DOC_NO = txtdoc_no.Text != string.Empty ? Convert.ToInt32(txtdoc_no.Text) : 0;
        string CashCredit = drpCsCr.SelectedValue.ToString();
        //string deliver_type = drpDelivered.SelectedValue.ToString();
        string DOC_DATE = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        Int32 BILL_NO = txtBillNo.Text != string.Empty ? Convert.ToInt32(txtBillNo.Text) : 0;
        string VEHICLE_NO = txtVehicleNo.Text.ToUpper();
        //Int32 SUP_CODE = txtSupplier.Text != string.Empty ? Convert.ToInt32(txtSupplier.Text) : 0;
        Int32 Awak_No = txtAwakNo.Text != string.Empty ? Convert.ToInt32(txtAwakNo.Text) : 0;

        double PVALUE = lblPurchaseValue.Text != string.Empty ? Convert.ToDouble(lblPurchaseValue.Text) : 0;
        double SVALUE = lblSaleValue.Text != string.Empty ? Convert.ToDouble(lblSaleValue.Text) : 0;
        double STVALUE = lblStockValue.Text != string.Empty ? Convert.ToDouble(lblStockValue.Text) : 0;


        //Int32 Brand_Code = txtBrand_Code.Text != string.Empty ? Convert.ToInt32(txtBrand_Code.Text) : 0;
        //Int32 Hamli_Code = txtHamali_Code.Text != string.Empty ? Convert.ToInt32(txtHamali_Code.Text) : 0;
        //Int32 Packing_Code = txtPacking_Code.Text != string.Empty ? Convert.ToInt32(txtPacking_Code.Text) : 0;
        //double CommAmnt = txtcomm_amnt.Text != string.Empty ? Convert.ToDouble(txtcomm_amnt.Text) : 0.00;
        //double HamaliAmnt = txtHamali_amnt.Text != string.Empty ? Convert.ToDouble(txtHamali_amnt.Text) : 0.00;
        //double PackingAmnt = txtPacking_amnt.Text != string.Empty ? Convert.ToDouble(txtPacking_amnt.Text) : 0.00;
        //double Other_Per = txtOther_Per.Text != string.Empty ? Convert.ToDouble(txtOther_Per.Text) : 0.00;
        //double Other_Per_Amnt = txtOther_Per_Amnt.Text != string.Empty ? Convert.ToDouble(txtOther_Per_Amnt.Text) : 0.00;
        //double Other_PM = txtOther_PM.Text != string.Empty ? Convert.ToDouble(txtOther_PM.Text) : 0.00;
        //double gross_sale_amnt = txtgross_sale_amnt.Text != string.Empty ? Convert.ToDouble(txtgross_sale_amnt.Text) : 0.00;




        string cashcredit = string.Empty;

        string retValue = string.Empty;
        string strRev = string.Empty;
        int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
        //int Year_Code = Convert.ToInt32(Session["year"].ToString());
        int year_Code = Convert.ToInt32(Session["year"].ToString());

        int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
        string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");

        Int32 awakoerderYearcode = lblAwakYearCode.Text != string.Empty ? Convert.ToInt32(lblAwakYearCode.Text) : 0;

        #endregion-End of Head part declearation
        btnSave.Enabled = false;
        clsGledgerupdations gleder = new clsGledgerupdations();


        #region save Head Master
        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
        {

            if (ViewState["mode"] != null)
            {
                DataSet ds = new DataSet();
                if (ViewState["mode"].ToString() == "I")
                {
                    obj.flag = 1;
                    obj.tableName = tblHead;
                    obj.columnNm = "doc_no,TRAN_TYPE,CASHCREDIT,DOC_DATE,vehno,purchasevalue,salevalue,stockvalue," +
                        "awakno,AwakYearCode,Company_Code,Year_Code,Created_By";

                    obj.values = "'" + DOC_NO + "','" + trntype + "','" + CashCredit + "','" + DOC_DATE + "','" + VEHICLE_NO + "','" +
                       +PVALUE + "','" + SVALUE + "','" + STVALUE + "','" + Awak_No +
                       "','" + awakoerderYearcode + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + year_Code + "','" + user + "'";

                    ds = obj.insertAccountMaster(ref strRev);
                    retValue = strRev;
                }
                else
                {
                    //Update Mode
                    obj.flag = 2;
                    obj.tableName = tblHead;
                    obj.columnNm = "CASHCREDIT='" + CashCredit + "',doc_date='" + DOC_DATE + "',vehno='" + VEHICLE_NO +
                         "',purchasevalue='" + PVALUE + "',salevalue='" + SVALUE + "',stockvalue='" + STVALUE
                       + "',awakno='" + Awak_No + "',AwakYearCode='" + awakoerderYearcode + "'  where Year_Code='" + year_Code +
                       "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + "' and  doc_no='" + DOC_NO + "' and Tran_Type='" + trntype + "'";
                    obj.values = "none";
                    ds = new DataSet();
                    ds = obj.insertAccountMaster(ref strRev);
                    retValue = strRev;
                }



                #region --------------------  Details --------------------
                Int32 Cust_Code = 0;
                Int32 Item_code = 0;
                Int32 Brand_Code = 0;
                Int32 bill_no = 0;

                double Qty = 0.00;
                double Net_Wt = 0.00;

                double Purc_Rate = 0.00;
                double Purc_Amnt = 0.00;
                double Sale_Rate = 0.00;
                double Sale_Amnt = 0.00;
                double Comm_Rate = 0.0;
                double Comm_Amnt = 0.00;

                Int32 Hamali_Code = 0;
                double Hamali_Amnt = 0.00;
                Int32 Packing_Code = 0;
                double Packing_Amnt = 0.00;

                double Other_Rate = 0.00;
                double Other_Amnt = 0.00;
                double Other_PM = 0.00;

                double Gross_Rate = 0.00;
                double Gross_Amnt = 0.00;
                Int32 sr = 0;

                double Shub_Rate = 0.00;
                double Shub_Amnt = 0.00;
                double Panjar_Rate = 0.00;
                Double Panjar_Amnt = 0.00;
                string i_d = "";
                double levirate = 0.00;
                double insurance = 0.00;
                double leviamt = 0.00;
                double insuranceamt = 0.00;
                //Int32 vat_ac = 0;
                //double percent = 0.0;
                Int32 awakoerdercode = 0;
                double Poch_Rate = 0.00;
                double Frieght_rate = 0.00;
                //string Sr = "";
                if (strRev == "-1" || strRev == "-2")
                {
                    if (grdDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdDetail.Rows.Count; i++)
                        {
                            i_d = grdDetail.Rows[i].Cells[2].Text;
                            Cust_Code = Convert.ToInt32(grdDetail.Rows[i].Cells[3].Text);
                            Item_code = Convert.ToInt32(grdDetail.Rows[i].Cells[5].Text);
                            Brand_Code = Convert.ToInt32(grdDetail.Rows[i].Cells[7].Text);


                            bill_no = Convert.ToInt32(grdDetail.Rows[i].Cells[9].Text);
                            Qty = Convert.ToDouble(grdDetail.Rows[i].Cells[10].Text);
                            Net_Wt = Convert.ToDouble(grdDetail.Rows[i].Cells[11].Text);




                            Purc_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[12].Text);
                            Purc_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[13].Text);
                            Sale_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[14].Text);
                            Sale_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[15].Text);
                            Comm_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[16].Text);
                            Comm_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[17].Text);

                            Hamali_Code = Convert.ToInt32(grdDetail.Rows[i].Cells[18].Text);
                            Hamali_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[20].Text);
                            Packing_Code = Convert.ToInt32(grdDetail.Rows[i].Cells[21].Text);
                            Packing_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[23].Text);

                            Other_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[24].Text);
                            Other_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[25].Text);
                            Other_PM = Convert.ToDouble(grdDetail.Rows[i].Cells[26].Text);

                            Gross_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[27].Text);
                            Gross_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[28].Text);

                            sr = Convert.ToInt32(grdDetail.Rows[i].Cells[29].Text);
                            Shub_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[30].Text);
                            Shub_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[31].Text);
                            Panjar_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[32].Text);
                            Panjar_Amnt = Convert.ToDouble(grdDetail.Rows[i].Cells[33].Text);
                            levirate = Convert.ToDouble(grdDetail.Rows[i].Cells[34].Text);
                            leviamt = Convert.ToDouble(grdDetail.Rows[i].Cells[35].Text);
                            insurance = Convert.ToDouble(grdDetail.Rows[i].Cells[36].Text);
                            insuranceamt = Convert.ToDouble(grdDetail.Rows[i].Cells[37].Text);
                            awakoerdercode = Convert.ToInt32(grdDetail.Rows[i].Cells[38].Text);
                            Poch_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[41].Text);
                            Frieght_rate = Convert.ToDouble(grdDetail.Rows[i].Cells[42].Text);
                            if (grdDetail.Rows[i].Cells[39].Text != "N" && grdDetail.Rows[i].Cells[39].Text != "R")
                            {
                                if (grdDetail.Rows[i].Cells[39].Text == "A")
                                {
                                    obj.flag = 1;
                                    obj.tableName = tblDetails;


                                    obj.columnNm = "Tran_Type,Doc_No,Cust_Code,Item_Code,Brand_Code,Bill_No,Qty,Net_Wt,Purc_Rate,Purc_Amnt,Sale_Rate,Sale_Amnt," +
                                   "Comm_Rate,Comm_Amnt,Hamali_Code,Hamali_Amnt,Packing_Code,Packing_Amnt,Other_Rate,Other_Amnt,Other_PM,Gross_Rate,Gross_Amnt," +
                                   "Sr,Shub_Rate,Shub_Amnt,Panjar_Rate,Panjar_Amnt,Company_Code,detail_id,Created_By,Levi_Rate,Insurance_Rate,Levi_Amt," +
                                   " Insurance_Amt,awakordercode,Year_Code,Poch_Rate,Frieght_Rate";
                                    obj.values = "'" + trntype + "','" + DOC_NO + "','" + Cust_Code + "','" + Item_code + "','" + Brand_Code + "','"
                                        + bill_no + "','" + Qty + "','" + Net_Wt + "','" + Purc_Rate + "','" + Purc_Amnt + "','" + Sale_Rate + "','" + Sale_Amnt + "','"
                                        + Comm_Rate + "','" + Comm_Amnt + "','" + Hamali_Code + "','" + Hamali_Amnt + "','" + Packing_Code + "','"
                                        + Packing_Amnt + "','" + Other_Rate + "','" + Other_Amnt
                                        + "','" + Other_PM + "','" + Gross_Rate + "','" + Gross_Amnt + "','" + sr
                                        + "','" + Shub_Rate + "','" + Shub_Amnt + "','" + Panjar_Rate + "','" + Panjar_Amnt
                                        + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + i_d + "','" + user + "','" + levirate + "','" + insurance +
                                        "','" + leviamt + "','" + insuranceamt + "','" + awakoerdercode + "','" + year_Code + "','" + Poch_Rate + "','" + Frieght_rate + "'";

                                    ds = new DataSet();
                                    ds = obj.insertAccountMaster(ref strRev);
                                    retValue = strRev;
                                }
                                if (grdDetail.Rows[i].Cells[39].Text == "U")
                                {

                                    obj.flag = 2;
                                    obj.tableName = tblDetails;

                                    obj.columnNm = "Cust_Code='" + Cust_Code + "',Item_Code='" + Item_code + "',Brand_Code='" + Brand_Code + "',Bill_No='" + bill_no
                                        + "',Qty='"
                                       + Qty + "',Net_Wt='" + Net_Wt + "',Purc_Rate='" + Purc_Rate + "',Purc_Amnt='" + Purc_Amnt + "',Sale_Rate='" + Sale_Rate + "',Sale_Amnt='"
                                       + Sale_Amnt + "',Comm_Rate='" + Comm_Rate + "',Comm_Amnt='" + Comm_Amnt + "',Hamali_Code='" + Hamali_Code + "',Hamali_Amnt='" + Hamali_Amnt +
                                       "',Packing_Code='" + Packing_Code + "',Packing_Amnt='" + Packing_Amnt +
                                       "',Other_Rate='" + Other_Rate + "',Other_Amnt='" + Other_Amnt + "',Other_PM='" + Other_PM
                                       + "',Gross_Rate='" + Gross_Rate + "',Gross_Amnt='" + Gross_Amnt + "',Sr='" + sr
                                       + "',Shub_Rate='" + Shub_Rate + "',Shub_Amnt='" + Shub_Amnt + "',Panjar_Rate='" + Panjar_Rate + "',Panjar_Amnt='" + Panjar_Amnt +
                                       "',Levi_Rate='" + levirate + "',Insurance_Rate='" + insurance + "',Levi_Amt='" + leviamt + "',Insurance_Amt='" + insuranceamt +
                                       "',awakordercode='" + awakoerdercode + "' ,Poch_Rate='" + Poch_Rate + "',Frieght_Rate='" + Frieght_rate + "'"
                                       + " where Year_Code='" + year_Code + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                                       + "' and doc_no='" + DOC_NO + "' and Tran_Type='" + trntype + "' and detail_id='" + i_d + "'";

                                    //obj.columnNm = "PURC_CODE='" + Cust_Code + "',ITEMCODE='" + item_code + "',Brand_Code='" + Brand_Code + "',QTY='" + Quantity + "',billno='"
                                    //    + billing_no + "',purcrate='" + prate + "',purcvalue='" + pamt + "',salerate='" + srate + "',NETKG='" + NetWt + "',salevalue='"
                                    //    + samt + "',Packing_Amnt='" + pamt + "',commrate='" + percent + "',netslrate='" + ntrate + "',awaksrno='" + Sr +
                                    //    "',Packing_Code='" + Packing_Code + "',Other_Per='" + Other_Per +
                                    //    "',Hamali_Code='" + Hamali_Code + "',CommAmnt='" + Commamnt + "',Other_amnt='" + Other_amnt + "',Other_PM='" + Other_PM + "',netslamnt='" + Grosssaleamnt +
                                    //    "' where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                                    //    + "' and doc_no='" + DOC_NO + "' and Tran_Type='" + trntype + "' and detail_id='" + i_d + "'";
                                    obj.values = "none";
                                    ds = new DataSet();
                                    ds = obj.insertAccountMaster(ref strRev);
                                    retValue = strRev;
                                }
                                if (grdDetail.Rows[i].Cells[39].Text == "D")
                                {
                                    obj.flag = 3;
                                    obj.tableName = tblDetails;
                                    obj.columnNm = " Year_Code='" + year_Code + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                                        "' and doc_no='" + txtdoc_no.Text + "' and Tran_Type='" + trntype + "' and  detail_id='" + i_d + "'";
                                    obj.values = "none";
                                    ds = new DataSet();
                                    ds = obj.insertAccountMaster(ref strRev);
                                    retValue = strRev;
                                }
                            }

                            //if (vat_amount != 0)
                            //{
                            //    string vat_account = clsCommon.getString("select Vat_AC from " + tblPrefix + "SystemMaster where System_Code=" + item_code + " and System_Type='I' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
                            //    if (vat_account != "0")
                            //    {
                            //        obj.flag = 1;
                            //        obj.tableName = tblPrefix + "GLEDGER";
                            //        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            //        obj.values = "'RR','','" + DOC_NO + "','" + DOC_DATE + "','" + vat_account + "','Retail sell','" + Math.Abs(vat_amount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + 1 + "','C','" + AC_CODE + "','" + TRAN_TYPE + "','" + DOC_NO + "'";
                            //        ds = obj.insertAccountMaster(ref strRev);
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                    }
                }
                //gleder.SugarSaleGledgerEffect(trntype, DOC_NO, Convert.ToInt32(Session["Company_Code"].ToString()), Convert.ToInt32(Session["year"].ToString()));
                #endregion


                btnAdddetails.Text = "ADD";
                hdnf.Value = txtdoc_no.Text;
                if (retValue == "-1")
                {
                    clsButtonNavigation.enableDisable("S");
                    this.enableDisableNavigateButtons();
                    this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Added!');", true);
                    //ScriptManager.RegisterStartupScript(this ,this.GetType(), "Javascript", "javascript:Confirm_print();", true);

                }
                if (retValue == "-2" || retValue == "-3")
                {
                    clsButtonNavigation.enableDisable("S");
                    this.enableDisableNavigateButtons();
                    this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " xdf", "alert('Successfully Updated !');", true);
                }
                // setFocusControl(btnAdd);

                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>SB();</script>", false);
                csCalculations();
                //calcultion();

            }
        }
        #endregion
        //txtEditDoc_No.Text = string.Empty;
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {

                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    bool isValidated = true;
                    Int32 bill = Convert.ToInt32(grdDetail.Rows[i].Cells[9].Text);
                    string str = clsCommon.getString("select doc_no from NT_1_JSaleHead where doc_no=" + bill + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                        + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str == bill.ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Don't delete record it is use in sale bill');", true);
                        isValidated = false;
                        //setFocusControl(btnOpenDetailsPopup);
                        return;
                    }
                }

                #region
                //string currentDoc_No = txtdoc_no.Text;
                //string qry = "";
                //DataSet ds = new DataSet();


                //qry = "delete from " + tblHead + " where doc_no=" + currentDoc_No + " and Tran_Type='" + trntype + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                //ds = clsDAL.SimpleQuery(qry);

                //qry = "delete from " + tblDetails + " where doc_no=" + currentDoc_No + " and Tran_Type='" + trntype + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                //ds = clsDAL.SimpleQuery(qry);
                ////string DONumber = Convert.ToString(lblDONo.Text);
                ////if (!string.IsNullOrWhiteSpace(DONumber))
                ////{
                ////    if (DONumber == "0")
                ////    { }
                ////    else
                ////    {
                ////        ds = new DataSet();
                ////        qry = "update " + tblPrefix + "deliveryorder SET SB_No=0 where doc_no='" + DONumber + "' and Tran_Type='DO' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                ////        ds = clsDAL.SimpleQuery(qry);
                ////    }
                ////}

                //string query = "SELECT top 1 [doc_no] from " + tblHead + "  where doc_no>" + Convert.ToInt32(currentDoc_No) +
                //        "  and Tran_Type='" + trntype + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY doc_no asc";

                //hdnf.Value = clsCommon.getString(query);

                //if (hdnf.Value == string.Empty)
                //{
                //    query = "SELECT top 1 [doc_no] from " + tblHead + "  where doc_no<" + Convert.ToInt32(currentDoc_No) +
                //         " and Tran_Type='" + trntype + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY doc_no desc  ";
                //    hdnf.Value = clsCommon.getString(query);
                //}
                //if (hdnf.Value != string.Empty)
                //{
                //    query = getDisplayQuery();
                //    bool recordExist = this.fetchRecord(query);
                //    this.makeEmptyForm("S");
                //    clsButtonNavigation.enableDisable("S");
                //}
                //else
                //{
                //    this.makeEmptyForm("N");
                //    //new code
                //    clsButtonNavigation.enableDisable("N");         //No record exist  Last record deleted.
                //    btnEdit.Enabled = false;
                //    btnDelete.Enabled = false;
                //}
                //this.enableDisableNavigateButtons();
                #endregion

                string str1 = string.Empty;
                DataSet xml_ds = new DataSet();
                if (str1 == string.Empty)
                {
                    string currentDoc_No = txtdoc_no.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                        string strRev = string.Empty;
                        child1.SetAttributeValue("doc_no", txtdoc_no.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);
                        child1.SetAttributeValue("Year_Code", Year_Code);
                        child1.SetAttributeValue("Tran_Type", trntype);
                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_Jawak_Head";
                        string xmlfile = XMLReport;
                        string op = "";
                        string returnmaxno = "";
                        int flag = 10;
                        xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
                        strrev = op;





                    }
                    string query = "";
                    if (strrev == "-3")
                    {

                        DataTable dt_dalete = xml_ds.Tables[0];
                        string HDNF_VALUE = "";
                        if (dt_dalete.Rows.Count > 0)
                        {
                            HDNF_VALUE = dt_dalete.Rows[0]["doc_no"].ToString();
                        }
                        hdnf.Value = HDNF_VALUE;

                        if (hdnf.Value == string.Empty)
                        {
                            DataTable dt_dalete_two = xml_ds.Tables[1];
                            if (dt_dalete_two.Rows.Count > 0)
                            {
                                hdnf.Value = dt_dalete_two.Rows[0]["doc_no"].ToString();
                            }
                        }
                        if (hdnf.Value != string.Empty)
                        {
                            query = getDisplayQuery();
                            bool recordExist = this.fetchRecord(query);
                            this.makeEmptyForm("S");
                            clsButtonNavigation.enableDisable("S");
                        }
                        else
                        {
                            this.makeEmptyForm("N");
                            clsButtonNavigation.enableDisable("N");
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                    this.enableDisableNavigateButtons();
                }
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
        if (hdnf.Value != string.Empty)
        {
            string query = getDisplayQuery(); ;
            bool recordExist = this.fetchRecord(query);
        }
        else
        {
            this.showLastRecord();
        }
        string str = clsCommon.getString("select count(doc_no) from " + tblHead + " where TRAN_TYPE='JW' Year_Code="
                        + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
        if (str != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.enableDisableNavigateButtons();
            this.makeEmptyForm("S");
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.enableDisableNavigateButtons();
            this.makeEmptyForm("N");

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;
        txtCustCode.Text = string.Empty;
        lblCustomer.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        lblItem.Text = string.Empty;
        txtBrand_Code.Text = string.Empty;
        lblBrandName.Text = string.Empty;
        txtBNo.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtNetWt.Text = string.Empty;
        txtPRate.Text = string.Empty;
        txtPAmt.Text = string.Empty;
        txtSRate.Text = string.Empty;
        txtSAmt.Text = string.Empty;
        txtPer.Text = string.Empty;
        txtNetRate.Text = string.Empty;
        txtSr.Text = string.Empty;
        txtcomm_amnt.Text = string.Empty;
        txtHamali_Code.Text = string.Empty;
        lblHamali_Name.Text = string.Empty;
        txtHamali_amnt.Text = string.Empty;
        txtPacking_Code.Text = string.Empty;
        lblPacking_name.Text = string.Empty;
        txtPacking_amnt.Text = string.Empty;
        txtOther_Per.Text = string.Empty;
        txtOther_Per_Amnt.Text = string.Empty;
        txtOther_PM.Text = string.Empty;
        txtNetRate.Text = string.Empty;
        txtgross_sale_amnt.Text = string.Empty;
        txtSr.Text = string.Empty;
        txtShub_Rate.Text = string.Empty;
        txtShub_Amnt.Text = string.Empty;
        TxtPanjar_Rate.Text = string.Empty;
        txtPanjar_Amnt.Text = string.Empty;
        btnAdddetails.Text = "ADD";

    }
    #endregion

    #region [btnprint_Click]
    protected void btnprint_Click(object sender, EventArgs e)
    {

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
                        drpCsCr.SelectedValue = dt.Rows[0]["CASHCREDIT"].ToString().Trim();
                        txtdoc_no.Text = dt.Rows[0]["doc_no"].ToString();
                        txtdoc_date.Text = dt.Rows[0]["DOC_DATE"].ToString();
                        // txtdoc_date.Text = DateTime.Parse(dt.Rows[0]["DOC_DATE"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        txtBillNo.Text = dt.Rows[0]["billno"].ToString();
                        txtVehicleNo.Text = dt.Rows[0]["vehno"].ToString();
                        txtAwakNo.Text = dt.Rows[0]["awakno"].ToString();
                        //  hdnfTranType.Value = dt.Rows[0]["awakordercode"].ToString();
                        // string orderno = clsCommon.getString("select awakordercode from " + qryCommon + " where doc_no=" + txtdoc_no.Text + " and Tran_Type='JW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        lblAwakYearCode.Text = dt.Rows[0]["AwakYearCode"].ToString();
                        // txtTotal.Text = dt.Rows[0]["Total"].ToString();
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

                        recordExist = true;
                        //lblMsg.Text = "";

                        #region  Details
                        //qry = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and TRAN_TYPE='AW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        //qry = "select detail_id as ID,Cust_Code,Customer_Name,System_Code as Item_Code,ItemName as Item_Name," +
                        //"Brand_Code,Brand_Name,Hamali_Code,Hamali_name,Hamali_Amnt,Packing_Code,Packing_Name,Packing_Amnt,BNo as B_No,QTY," +
                        //"NETKG,purcrate as P_Rate,purcvalue as P_Amt,salerate as S_Rate,commrate,CommAmnt,Sale_Amt as S_Amt," +
                        //"netslrate as Net_Rate,awaksrno as Sr,Other_Per,Other_amnt"
                        //          + ",Other_PM,netslamnt " +
                        // " from " + qryCommon + " where doc_no="
                        // + hdnf.Value + " and TRAN_TYPE='AW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                        qry = "select detail_id as ID,Cust_Code,Customer_Name as Cust_Name,[Item_Code],[Item_Name]," +
                       "[Brand_Code],[Brand_Name],[Bill_No],[Qty],[Net_Wt],[Purc_Rate],[Purc_Amnt],[Sale_Rate],[Sale_Amnt],[Comm_Rate]," +
                       "[Comm_Amnt],[Hamali_Code],[Hamali_Name],[Hamali_Amnt],[Packing_Code],[Packing_Name],[Packing_Amnt]," +
                       "[Other_Rate],[Other_Amnt],[Other_PM],[Gross_Rate]"
                                 + ",[Gross_Amnt],[Sr],[Shub_Rate],[Shub_Amnt],[Panjar_Rate],[Panjar_Amnt],Levi_Rate,Levi_Amt,Insurance_Rate,Insurance_Amt,[awakordercode] as OrderCode,'' as rowAction,'' as SrNo,isnull(Poch_Rate,0) as Poch_Rate,isnull(Frieght_Rate,0) as Frieght_Rate" +
                        " from " + qryCommon + " where doc_no="
                        + hdnf.Value + " and TRAN_TYPE='JW'  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            columnTotal();
            // csCalculations();

            // lblPurchaseValue.Text = Math.Round(purc_value, 2).ToString();
            //this.columnTotal();
            //csCalculations();
            //this.enableDisableNavigateButtons();
            hdnf.Value = txtdoc_no.Text;
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
            string qryDisplay = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and TRAN_TYPE='JW' and Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (btnAdddetails.Text == "ADD")
            {
                string salebillno = txtBNo.Text;
                string str = clsCommon.getString("select doc_no from NT_1_JSaleHead where doc_no=" + salebillno + " and  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty && str != "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Sale Bill No " + salebillno + " already exist');", true);

                    //lblMsg.Text = "Sale Bill No " + salebillno + " already exist";
                    isValidated = false;
                    return;
                }
                else
                {
                    isValidated = true;
                }


            }

            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            bool isValidate = true;
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
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where Doc_No=" + txtdoc_no.Text
                            + "  And  TRAN_TYPE='JW' and detail_id=" + lblID.Text + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                            + "  and Company_Code="
                            + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (id != string.Empty)
                        {
                            dr["rowAction"] = "U";    //actual row
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



                    dt.Columns.Add((new DataColumn("Cust_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Cust_Name", typeof(string))));

                    dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));

                    dt.Columns.Add((new DataColumn("Brand_Code", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));

                    dt.Columns.Add((new DataColumn("Bill_No", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));

                    dt.Columns.Add((new DataColumn("Purc_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Purc_Amnt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Sale_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Sale_Amnt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Comm_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Comm_Amnt", typeof(string))));

                    dt.Columns.Add((new DataColumn("Hamali_Code", typeof(string))));
                    dt.Columns.Add((new DataColumn("Hamali_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Hamali_Amnt", typeof(string))));
                    dt.Columns.Add((new DataColumn("Packing_Code", typeof(string))));
                    dt.Columns.Add((new DataColumn("Packing_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Packing_Amnt", typeof(string))));


                    dt.Columns.Add((new DataColumn("Other_Rate", typeof(string))));
                    dt.Columns.Add((new DataColumn("Other_Amnt", typeof(string))));
                    dt.Columns.Add((new DataColumn("Other_PM", typeof(string))));


                    dt.Columns.Add((new DataColumn("Gross_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Gross_Amnt", typeof(string))));
                    dt.Columns.Add((new DataColumn("Sr", typeof(string))));

                    dt.Columns.Add((new DataColumn("Shub_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Shub_Amnt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Panjar_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Panjar_Amnt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Levi_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Levi_Amt", typeof(double))));

                    dt.Columns.Add((new DataColumn("Insurance_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Insurance_Amt", typeof(double))));
                    dt.Columns.Add((new DataColumn("OrderCode", typeof(double))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("Poch_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Frieght_Rate", typeof(double))));

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


                dt.Columns.Add((new DataColumn("Cust_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Cust_Name", typeof(string))));

                dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));

                dt.Columns.Add((new DataColumn("Brand_Code", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));

                dt.Columns.Add((new DataColumn("Bill_No", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Qty", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));

                dt.Columns.Add((new DataColumn("Purc_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Purc_Amnt", typeof(double))));
                dt.Columns.Add((new DataColumn("Sale_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Sale_Amnt", typeof(double))));
                dt.Columns.Add((new DataColumn("Comm_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Comm_Amnt", typeof(string))));

                dt.Columns.Add((new DataColumn("Hamali_Code", typeof(string))));
                dt.Columns.Add((new DataColumn("Hamali_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Hamali_Amnt", typeof(string))));
                dt.Columns.Add((new DataColumn("Packing_Code", typeof(string))));
                dt.Columns.Add((new DataColumn("Packing_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Packing_Amnt", typeof(string))));


                dt.Columns.Add((new DataColumn("Other_Rate", typeof(string))));
                dt.Columns.Add((new DataColumn("Other_Amnt", typeof(string))));
                dt.Columns.Add((new DataColumn("Other_PM", typeof(string))));


                dt.Columns.Add((new DataColumn("Gross_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Gross_Amnt", typeof(string))));
                dt.Columns.Add((new DataColumn("Sr", typeof(string))));

                dt.Columns.Add((new DataColumn("Shub_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Shub_Amnt", typeof(double))));
                dt.Columns.Add((new DataColumn("Panjar_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Panjar_Amnt", typeof(double))));
                dt.Columns.Add((new DataColumn("Levi_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Levi_Amt", typeof(double))));

                dt.Columns.Add((new DataColumn("Insurance_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Insurance_Amt", typeof(double))));
                dt.Columns.Add((new DataColumn("OrderCode", typeof(double))));

                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add((new DataColumn("Poch_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Frieght_Rate", typeof(double))));

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
          
            #region [ Set values to dr]

            #region[Customer code]
            //string Cust_Name = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text
            //    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (txtCustCode.Text != string.Empty)
            {
                //isValidate = true;
                dr["Cust_Code"] = txtCustCode.Text;
            }
            else
            {
                // isValidate = false;
                setFocusControl(txtCustCode);
                return;
            }

            dr["Cust_Name"] = lblCustomer.Text;
            #endregion

            #region[Item code]
            //string Item_Name = clsCommon.getString("select System_Name_E from " + systemMasterTable + " where System_Type = 'I' and System_Code="
            //    + txtItemCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (txtItemCode.Text != string.Empty)
            {
                //isValidate = true;
                dr["Item_Code"] = txtItemCode.Text;
            }
            else
            {
                // isValidate = false;
                setFocusControl(txtItemCode);
                return;
            }

            dr["Item_Name"] = lblItem.Text;
            #endregion

            #region[Brand code]
            //string brandname = clsCommon.getString("select [Brand_Name] from [NT_1_BrandMaster]  where [Doc_No]="
            //               + txtBrand_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (txtBrand_Code.Text != string.Empty)
            {
                //isValidate = true;
                dr["Brand_Code"] = txtBrand_Code.Text;
            }
            else
            {
                // isValidate = false;
                setFocusControl(txtBrand_Code);
                return;
            }

            dr["Brand_Name"] = lblBrandName.Text;
            #endregion

            #region[BNo]

            //int bno = Convert.ToInt32(txtBNo);
            if (txtBNo.Text == string.Empty || Convert.ToInt32(txtBNo.Text) <= 0)
            {
                if (txtCustCode.Text == "2")
                {
                    dr["Bill_No"] = 0;
                }
                else
                {
                    //isValidate = true;
                    isValidate = false;
                    setFocusControl(txtBNo);
                    return;
                }

            }
            else
            {


                dr["Bill_No"] = txtBNo.Text;

                //dr["Bill_No"] = 0;


            }
            #endregion

            #region[Qty]
            if (txtQty.Text != string.Empty)
            {
                double qty = Convert.ToDouble(txtQty.Text);


                //bool isValidated = true;
                Int32 jawakqnty = Convert.ToInt32(txtQty.Text);

                Int32 str = Convert.ToInt32(clsCommon.getString("select balnceqty from NT_1_qryJABalance where doc_no=" + txtAwakNo.Text + " and detail_id=" + lblAwakQuantity.Text
                    + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                // Int32 str1=Convert.ToInt32(str).ToString();
                if (jawakqnty > str && btnAdddetails.Text == "ADD")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Enter Correct quantity');", true);
                    isValidated = false;
                    setFocusControl(txtQty);
                    return;
                }
                else
                {
                    dr["Qty"] = txtQty.Text != string.Empty ? Convert.ToDouble(txtQty.Text) : 0.00;
                    // dr["Quantity"] = "0";
                    //setFocusControl(txtQty);

                }

            }
            else
            {
                setFocusControl(txtQty);
                return;
            }
            #endregion

            #region[Net Wt]
            if (txtNetWt.Text != string.Empty)
            {
                double amt = Convert.ToDouble(txtNetWt.Text);
                if (amt <= 0)
                {
                    setFocusControl(txtPRate);
                    return;


                }
                else
                {
                    dr["Net_Wt"] = txtPAmt.Text != string.Empty ? Convert.ToDouble(txtNetWt.Text) : 0.00;
                    //setFocusControl(txtNetWt);

                }

            }
            else
            {
                setFocusControl(txtPRate);
                return;
            }
            #endregion

            #region[P Rate]
            if (txtPRate.Text != string.Empty)
            {
                double amt = Convert.ToDouble(txtPRate.Text);
                if (amt <= 0)
                {
                    setFocusControl(txtPAmt);
                    return;


                }
                else
                {
                    dr["Purc_Rate"] = txtPRate.Text != string.Empty ? Convert.ToDouble(txtPRate.Text) : 0.00;
                    // dr["Quantity"] = "0";
                    //setFocusControl(txtPRate);

                }

            }
            else
            {
                setFocusControl(txtPRate);
                return;
            }
            #endregion

            #region[P.Amount]
            if (txtPAmt.Text != string.Empty)
            {
                double amt = Convert.ToDouble(txtPAmt.Text);
                if (amt <= 0)
                {
                    setFocusControl(txtPAmt);
                    return;


                }
                else
                {
                    dr["Purc_Amnt"] = txtPAmt.Text != string.Empty ? Convert.ToDouble(txtPAmt.Text) : 0.00;
                    // dr["Quantity"] = "0";
                    //setFocusControl(txtPAmt);

                }

            }
            else
            {
                setFocusControl(txtPAmt);
                return;
            }
            #endregion

            #region[S Rate]
            if (txtSRate.Text != string.Empty)
            {
                //isValidate = true;
                dr["Sale_Rate"] = txtSRate.Text;
            }
            else
            {
                if (txtCustCode.Text == "2")
                {
                    dr["Sale_Rate"] = 0;
                }
                else
                {
                    setFocusControl(txtSRate);
                    return;
                }

                // isValidate = false;
                //setFocusControl(txtSRate);
                //  return;
            }
            #endregion

            #region[S.Amount]
            if (txtSAmt.Text != string.Empty)
            {
                double samt = Convert.ToDouble(txtSAmt.Text);
                if (samt <= 0)
                {
                    dr["Sale_Amnt"] = 0;
                    //setFocusControl(txtSAmt);
                    // return;


                }
                else
                {
                    dr["Sale_Amnt"] = txtSAmt.Text != string.Empty ? Convert.ToDouble(txtSAmt.Text) : 0.00;
                    // dr["Quantity"] = "0";
                    //setFocusControl(txtSAmt);

                }

            }
            else
            {
                dr["Sale_Amnt"] = 0;
                //setFocusControl(txtSAmt);
                //return;
            }
            #endregion

            #region[Per]
            if (txtPer.Text != string.Empty)
            {
                //isValidate = true;
                dr["Comm_Rate"] = txtPer.Text;
            }
            else
            {
                dr["Comm_Rate"] = 0;
                // isValidate = false;
                //setFocusControl(txtPer);
                //return;
            }
            #endregion

            if (txtcomm_amnt.Text != string.Empty)
            {
                dr["Comm_Amnt"] = txtcomm_amnt.Text;
            }
            else
            {
                dr["Comm_Amnt"] = 0;
            }

            #region[Hamali code]

            if (txtHamali_Code.Text != string.Empty)
            {
                //isValidate = true;
                dr["Hamali_Code"] = txtHamali_Code.Text;
            }
            else
            {
                dr["Hamali_Code"] = 1;
                //// isValidate = false;
                //setFocusControl(txtHamali_Code);
                //return;
            }

            dr["Hamali_Name"] = lblHamali_Name.Text;
            if (txtHamali_amnt.Text != string.Empty)
            {
                dr["Hamali_Amnt"] = txtHamali_amnt.Text;
            }
            else
            {
                dr["Hamali_Amnt"] = 0;
            }
            #endregion

            #region[packing code]

            if (txtPacking_Code.Text != string.Empty)
            {
                //isValidate = true;
                dr["Packing_Code"] = txtPacking_Code.Text;
            }
            else
            {
                dr["Packing_Code"] = 1;
                //// isValidate = false;
                //setFocusControl(txtPacking_Code);
                //return;
            }

            dr["Packing_Name"] = lblPacking_name.Text;
            if (txtPacking_amnt.Text != string.Empty)
            {
                dr["Packing_Amnt"] = txtPacking_amnt.Text;
            }
            else
            {
                dr["Packing_Amnt"] = 0;
            }
            #endregion

            if (txtOther_Per.Text != string.Empty)
            {
                dr["Other_Rate"] = txtOther_Per.Text;
            }
            else
            {
                dr["Other_Rate"] = 0;
            }
            if (txtOther_Per_Amnt.Text != string.Empty)
            {
                dr["Other_Amnt"] = txtOther_Per_Amnt.Text;
            }
            else
            {
                dr["Other_Amnt"] = 0;
            }
            if (txtOther_PM.Text != string.Empty)
            {
                dr["Other_PM"] = txtOther_PM.Text;
            }
            else
            {
                dr["Other_PM"] = 0;
            }

            if (txtLevidetails.Text != string.Empty)
            {
                dr["Levi_Rate"] = txtLevidetails.Text;
            }
            else
            {

                setFocusControl(txtLevidetails);
                return;
                // dr["txtLevidetails"] = 0;
            }

            if (txtLevidetailsAmt.Text != string.Empty)
            {
                if (txtCustCode.Text == "2")
                {
                    dr["Levi_Amt"] = 0;
                }
                else
                {
                    dr["Levi_Amt"] = txtLevidetailsAmt.Text;
                }
            }
            else
            {
                //setFocusControl(txtLevidetails);
                //return;
                dr["Levi_Amt"] = 0;
            }
            if (txtInsurance.Text != string.Empty)
            {
                dr["Insurance_Rate"] = txtInsurance.Text;
            }
            else
            {

                setFocusControl(txtInsurance);
                return;
                // dr["txtLevidetails"] = 0;
            }
            if (txtInsuranceAmt.Text != string.Empty)
            {
                if (txtCustCode.Text == "2")
                {
                    dr["Insurance_Amt"] = 0;
                }
                else
                {
                    dr["Insurance_Amt"] = txtInsuranceAmt.Text;
                }
            }
            else
            {
                //setFocusControl(txtInsurance);
                //return;
                dr["Insurance_Amt"] = 0;
            }


            #region[Net Rate]
            if (txtNetRate.Text != string.Empty)
            {
                double amt = Convert.ToDouble(txtNetRate.Text);
                if (amt <= 0)
                {
                    setFocusControl(txtNetRate);
                    // return;


                }
                else
                {
                    dr["Gross_Rate"] = txtNetRate.Text != string.Empty ? Convert.ToDouble(txtNetRate.Text) : 0.00;
                    // dr["Quantity"] = "0";
                    //setFocusControl(txtNetRate);

                }

            }
            else
            {
                dr["Gross_Rate"] = 0;
                //setFocusControl(txtNetRate);
                //return;
            }
            #endregion

            if (txtgross_sale_amnt.Text != string.Empty)
            {
                dr["Gross_Amnt"] = txtgross_sale_amnt.Text;
            }
            else
            {
                dr["Gross_Amnt"] = 0;
            }
            #region[SR]
            if (txtSr.Text != string.Empty)
            {
                //isValidate = true;
                dr["Sr"] = txtSr.Text;
            }
            else
            {
                dr["Sr"] = 0;
                // isValidate = false;
                //setFocusControl(txtSr);
                //return;
            }
            #endregion
            if (txtShub_Rate.Text != string.Empty)
            {
                dr["Shub_Rate"] = txtShub_Rate.Text;
            }
            else
            {
                dr["Shub_Rate"] = 0;
            }
            if (txtShub_Amnt.Text != string.Empty)
            {
                dr["Shub_Amnt"] = txtShub_Amnt.Text;
            }
            else
            {
                dr["Shub_Amnt"] = 0;
            }
            if (TxtPanjar_Rate.Text != string.Empty)
            {
                dr["Panjar_Rate"] = TxtPanjar_Rate.Text;
            }
            else
            {
                dr["Panjar_Rate"] = 0;
            }
            if (txtPanjar_Amnt.Text != string.Empty)
            {
                dr["Panjar_Amnt"] = txtPanjar_Amnt.Text;
            }
            else
            {
                dr["Panjar_Amnt"] = 0;
            }
            if (lblAwakQuantity.Text != string.Empty)
            {
                dr["OrderCode"] = lblAwakQuantity.Text;
            }
            else
            {
                setFocusControl(txtItemCode);
                return;
                //dr["OrderCode"] = 0;
            }


            if (txtpochrate.Text != string.Empty)
            {
                dr["Poch_Rate"] = txtpochrate.Text;
            }
            else
            {
                dr["Poch_Rate"] = 0;
                setFocusControl(txtpochrate);
                return;
                // dr["txtLevidetails"] = 0;
            }

            if (txtfrieghtRate.Text != string.Empty)
            {
                dr["Frieght_Rate"] = txtfrieghtRate.Text;
            }
            else
            {
                dr["Frieght_Rate"] = 0;
                setFocusControl(txtfrieghtRate);
                return;
                // dr["txtLevidetails"] = 0;
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
                // pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtCustCode);
            }
            else
            {
                // pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnAdddetails.Text = "ADD";
                btnAdddetails.Focus();
            }
            // Empty Code->
            txtCustCode.Text = string.Empty;
            lblCustomer.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            lblItem.Text = string.Empty;
            txtBNo.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtNetWt.Text = string.Empty;
            txtPRate.Text = string.Empty;
            txtPAmt.Text = string.Empty;
            txtSRate.Text = string.Empty;
            txtSAmt.Text = string.Empty;
            txtPer.Text = string.Empty;
            txtNetRate.Text = string.Empty;
            txtSr.Text = string.Empty;
            txtcomm_amnt.Text = string.Empty;
            txtHamali_Code.Text = string.Empty;
            lblHamali_Name.Text = string.Empty;
            txtHamali_amnt.Text = string.Empty;
            txtPacking_Code.Text = string.Empty;
            lblPacking_name.Text = string.Empty;
            txtPacking_amnt.Text = string.Empty;
            txtOther_Per.Text = string.Empty;
            txtOther_Per_Amnt.Text = string.Empty;
            txtOther_PM.Text = string.Empty;
            txtNetRate.Text = string.Empty;
            txtgross_sale_amnt.Text = string.Empty;
            txtSr.Text = string.Empty;
            txtShub_Rate.Text = string.Empty;
            txtShub_Amnt.Text = string.Empty;
            TxtPanjar_Rate.Text = string.Empty;
            txtPanjar_Amnt.Text = string.Empty;
            lblHamali_Name.Text = string.Empty;
            lblPacking_name.Text = string.Empty;
            txtBrand_Code.Text = string.Empty;
            lblBrandName.Text = string.Empty;
            txtInsurance.Text = string.Empty;
            txtLevidetails.Text = string.Empty;
            txtLevidetailsAmt.Text = string.Empty;
            txtInsuranceAmt.Text = string.Empty;
            lblAwakQuantity.Text = string.Empty;
            txtpochrate.Text = string.Empty;
            txtfrieghtRate.Text = string.Empty;
            csCalculations();
            setFocusControl(txtCustCode);
            columnTotal();
            btnSave.Enabled = true;
            if (btnAdddetails.Text == "ADD")
            {
                txtCustCode.Enabled = true;
                txtItemCode.Enabled = true;
                txtBrand_Code.Enabled = true;
                txtBNo.Enabled = true;
                txtQty.Enabled = true;
                txtNetWt.Enabled = true;
                txtPRate.Enabled = true;
                txtPAmt.Enabled = true;
                txtSRate.Enabled = true;
                txtSAmt.Enabled = true;
                txtPer.Enabled = true;
                txtNetRate.Enabled = true;
                txtSr.Enabled = true;
                txtcomm_amnt.Enabled = true;
                txtHamali_Code.Enabled = true;
                lblHamali_Name.Enabled = true;
                txtHamali_amnt.Enabled = true;
                txtPacking_Code.Enabled = true;
                lblPacking_name.Enabled = true;
                txtPacking_amnt.Enabled = true;
                txtOther_Per.Enabled = true;
                txtOther_Per_Amnt.Enabled = true;
                txtOther_PM.Enabled = true;
                txtNetRate.Enabled = true;
                txtgross_sale_amnt.Enabled = true;
                txtSr.Enabled = true;
                txtShub_Rate.Enabled = true;
                txtShub_Amnt.Enabled = true;
                TxtPanjar_Rate.Enabled = true;
                txtPanjar_Amnt.Enabled = true;
                txtLevidetails.Enabled = true;
                txtInsurance.Enabled = true;
                txtInsuranceAmt.Enabled = true;
                txtLevidetailsAmt.Enabled = true;


                //pnlPopupDetails.Style["display"] = "none";
            }

            else
            {
                txtCustCode.Enabled = false;
                lblCustomer.Enabled = false;
                txtItemCode.Enabled = false;
                txtBrand_Code.Enabled = false;
                lblItem.Enabled = false;
                txtBNo.Enabled = false;
                txtQty.Enabled = false;
                txtNetWt.Enabled = false;
                txtPRate.Enabled = true;
                txtPAmt.Enabled = false;
                txtSRate.Enabled = false;
                txtSAmt.Enabled = false;
                txtPer.Enabled = false;
                txtNetRate.Enabled = false;
                txtSr.Enabled = false;
                txtcomm_amnt.Enabled = false;
                txtHamali_Code.Enabled = false;
                lblHamali_Name.Enabled = false;
                txtHamali_amnt.Enabled = false;
                txtPacking_Code.Enabled = false;
                lblPacking_name.Enabled = false;
                txtPacking_amnt.Enabled = false;
                txtOther_Per.Enabled = false;
                txtOther_Per_Amnt.Enabled = false;
                txtOther_PM.Enabled = false;
                txtNetRate.Enabled = false;
                txtgross_sale_amnt.Enabled = false;
                txtSr.Enabled = false;
                txtShub_Rate.Enabled = false;
                txtShub_Amnt.Enabled = false;
                TxtPanjar_Rate.Enabled = false;
                txtPanjar_Amnt.Enabled = false;
                txtLevidetails.Enabled = false;
                txtInsurance.Enabled = true;
                txtInsuranceAmt.Enabled = false;
                txtLevidetailsAmt.Enabled = false;
                calcultionForaccode();
                columnTotal();
                setFocusControl(txtPRate);
            }


            btnAdddetails.Text = "ADD";
            fordisablerowuseinsalebill();
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
        txtCustCode.Text = string.Empty;
        lblCustomer.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        lblItem.Text = string.Empty;
        txtBrand_Code.Text = string.Empty;
        lblBrandName.Text = string.Empty;
        txtBNo.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtNetWt.Text = string.Empty;
        txtPRate.Text = string.Empty;
        txtPAmt.Text = string.Empty;
        txtSRate.Text = string.Empty;
        txtSAmt.Text = string.Empty;
        txtPer.Text = string.Empty;
        txtNetRate.Text = string.Empty;
        txtSr.Text = string.Empty;
        txtcomm_amnt.Text = string.Empty;
        txtHamali_Code.Text = string.Empty;
        lblHamali_Name.Text = string.Empty;
        txtHamali_amnt.Text = string.Empty;
        txtPacking_Code.Text = string.Empty;
        lblPacking_name.Text = string.Empty;
        txtPacking_amnt.Text = string.Empty;
        txtOther_Per.Text = string.Empty;
        txtOther_Per_Amnt.Text = string.Empty;
        txtOther_PM.Text = string.Empty;
        txtNetRate.Text = string.Empty;
        txtgross_sale_amnt.Text = string.Empty;
        txtSr.Text = string.Empty;
        txtShub_Rate.Text = string.Empty;
        txtShub_Amnt.Text = string.Empty;
        TxtPanjar_Rate.Text = string.Empty;
        txtPanjar_Amnt.Text = string.Empty;
        txtLevidetails.Text = string.Empty;
        txtLevidetailsAmt.Text = string.Empty;
        txtInsurance.Text = string.Empty;
        txtInsuranceAmt.Text = string.Empty;


        txtCustCode.Enabled = true;
        txtItemCode.Enabled = true;
        txtBrand_Code.Enabled = true;
        txtBNo.Enabled = true;
        txtQty.Enabled = true;
        txtNetWt.Enabled = true;
        txtPRate.Enabled = true;
        txtPAmt.Enabled = true;
        txtSRate.Enabled = true;
        txtSAmt.Enabled = true;
        txtPer.Enabled = true;
        txtNetRate.Enabled = true;
        txtSr.Enabled = true;
        txtcomm_amnt.Enabled = true;
        txtHamali_Code.Enabled = true;
        lblHamali_Name.Enabled = true;
        txtHamali_amnt.Enabled = true;
        txtPacking_Code.Enabled = true;
        lblPacking_name.Enabled = true;
        txtPacking_amnt.Enabled = true;
        txtOther_Per.Enabled = true;
        txtOther_Per_Amnt.Enabled = true;
        txtOther_PM.Enabled = true;
        txtNetRate.Enabled = true;
        txtgross_sale_amnt.Enabled = true;
        txtSr.Enabled = true;
        txtShub_Rate.Enabled = true;
        txtShub_Amnt.Enabled = true;
        TxtPanjar_Rate.Enabled = true;
        txtPanjar_Amnt.Enabled = true;
        txtLevidetails.Enabled = true;
        txtLevidetailsAmt.Enabled = true;
        txtInsurance.Enabled = true;
        txtInsuranceAmt.Enabled = true;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtCustCode);

    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gr)
    {
        try
        {
            txtCustCode.Text = Server.HtmlDecode(gr.Cells[3].Text);
            lblCustomer.Text = Server.HtmlDecode(gr.Cells[4].Text);
            txtItemCode.Text = Server.HtmlDecode(gr.Cells[5].Text);
            lblItem.Text = Server.HtmlDecode(gr.Cells[6].Text);
            txtBrand_Code.Text = Server.HtmlDecode(gr.Cells[7].Text);
            lblBrandName.Text = Server.HtmlDecode(gr.Cells[8].Text);

            txtBNo.Text = Server.HtmlDecode(gr.Cells[9].Text);
            txtQty.Text = Server.HtmlDecode(gr.Cells[10].Text);
            txtNetWt.Text = Server.HtmlDecode(gr.Cells[11].Text);
            txtPRate.Text = Server.HtmlDecode(gr.Cells[12].Text);
            txtPAmt.Text = Server.HtmlDecode(gr.Cells[13].Text);
            txtSRate.Text = Server.HtmlDecode(gr.Cells[14].Text);
            txtSAmt.Text = Server.HtmlDecode(gr.Cells[15].Text);

            txtPer.Text = Server.HtmlDecode(gr.Cells[16].Text);
            txtcomm_amnt.Text = Server.HtmlDecode(gr.Cells[17].Text);


            txtHamali_Code.Text = Server.HtmlDecode(gr.Cells[18].Text);
            lblHamali_Name.Text = Server.HtmlDecode(gr.Cells[19].Text);
            txtHamali_amnt.Text = Server.HtmlDecode(gr.Cells[20].Text);
            txtPacking_Code.Text = Server.HtmlDecode(gr.Cells[21].Text);
            lblPacking_name.Text = Server.HtmlDecode(gr.Cells[22].Text);
            txtPacking_amnt.Text = Server.HtmlDecode(gr.Cells[23].Text);



            txtOther_Per.Text = Server.HtmlDecode(gr.Cells[24].Text);
            txtOther_Per_Amnt.Text = Server.HtmlDecode(gr.Cells[25].Text);
            txtOther_PM.Text = Server.HtmlDecode(gr.Cells[26].Text);

            txtNetRate.Text = Server.HtmlDecode(gr.Cells[27].Text);
            txtgross_sale_amnt.Text = Server.HtmlDecode(gr.Cells[28].Text);
            //txtCess.Text = Server.HtmlDecode(gr.Cells[28].Text);
            txtSr.Text = Server.HtmlDecode(gr.Cells[29].Text);

            txtShub_Rate.Text = Server.HtmlDecode(gr.Cells[30].Text);
            txtShub_Amnt.Text = Server.HtmlDecode(gr.Cells[31].Text);
            TxtPanjar_Rate.Text = Server.HtmlDecode(gr.Cells[32].Text);

            txtPanjar_Amnt.Text = Server.HtmlDecode(gr.Cells[33].Text);

            txtLevidetails.Text = Server.HtmlDecode(gr.Cells[34].Text);
            txtLevidetailsAmt.Text = Server.HtmlDecode(gr.Cells[35].Text);
            txtInsurance.Text = Server.HtmlDecode(gr.Cells[36].Text);
            txtInsuranceAmt.Text = Server.HtmlDecode(gr.Cells[37].Text);
            lblAwakQuantity.Text = Server.HtmlDecode(gr.Cells[38].Text);
            txtpochrate.Text = Server.HtmlDecode(gr.Cells[41].Text);
            txtfrieghtRate.Text = Server.HtmlDecode(gr.Cells[42].Text);

            lblNo.Text = Server.HtmlDecode(gr.Cells[40].Text);
            lblID.Text = Server.HtmlDecode(gr.Cells[2].Text);
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                string bill = txtBNo.Text;
                string str = clsCommon.getString("select doc_no from NT_1_JSaleHead where doc_no=" + bill + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str == bill.ToString())
                {
                    txtCustCode.Enabled = false;
                    lblCustomer.Enabled = false;
                    txtItemCode.Enabled = false;
                    txtBrand_Code.Enabled = false;
                    lblItem.Enabled = false;
                    txtBNo.Enabled = false;
                    txtQty.Enabled = false;
                    txtNetWt.Enabled = false;
                    txtPRate.Enabled = true;
                    txtPAmt.Enabled = false;
                    txtSRate.Enabled = false;
                    txtSAmt.Enabled = false;
                    txtPer.Enabled = false;
                    txtNetRate.Enabled = false;
                    txtSr.Enabled = false;
                    txtcomm_amnt.Enabled = false;
                    txtHamali_Code.Enabled = false;
                    lblHamali_Name.Enabled = false;
                    txtHamali_amnt.Enabled = false;
                    txtPacking_Code.Enabled = false;
                    lblPacking_name.Enabled = false;
                    txtPacking_amnt.Enabled = false;
                    txtOther_Per.Enabled = false;
                    txtOther_Per_Amnt.Enabled = false;
                    txtOther_PM.Enabled = false;
                    txtNetRate.Enabled = false;
                    txtgross_sale_amnt.Enabled = false;
                    txtSr.Enabled = false;
                    txtShub_Rate.Enabled = false;
                    txtShub_Amnt.Enabled = false;
                    TxtPanjar_Rate.Enabled = false;
                    txtPanjar_Amnt.Enabled = false;
                    txtLevidetails.Enabled = false;
                    txtInsurance.Enabled = true;
                    txtLevidetailsAmt.Enabled = false;
                    txtInsuranceAmt.Enabled = false;
                    calcultionForaccode();
                    columnTotal();
                    setFocusControl(txtPRate);
                }
                else
                {
                    txtCustCode.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand_Code.Enabled = true;
                    txtBNo.Enabled = true;
                    txtQty.Enabled = true;
                    txtNetWt.Enabled = true;
                    txtPRate.Enabled = true;
                    txtPAmt.Enabled = true;
                    txtSRate.Enabled = true;
                    txtSAmt.Enabled = true;
                    txtPer.Enabled = true;
                    txtNetRate.Enabled = true;
                    txtSr.Enabled = true;
                    txtcomm_amnt.Enabled = true;
                    txtHamali_Code.Enabled = true;
                    lblHamali_Name.Enabled = true;
                    txtHamali_amnt.Enabled = true;
                    txtPacking_Code.Enabled = true;
                    lblPacking_name.Enabled = true;
                    txtPacking_amnt.Enabled = true;
                    txtOther_Per.Enabled = true;
                    txtOther_Per_Amnt.Enabled = true;
                    txtOther_PM.Enabled = true;
                    txtNetRate.Enabled = true;
                    txtgross_sale_amnt.Enabled = true;
                    txtSr.Enabled = true;
                    txtShub_Rate.Enabled = true;
                    txtShub_Amnt.Enabled = true;
                    TxtPanjar_Rate.Enabled = true;
                    txtPanjar_Amnt.Enabled = true;
                    txtLevidetails.Enabled = true;
                    txtInsurance.Enabled = true;
                    txtLevidetailsAmt.Enabled = true;
                    txtInsuranceAmt.Enabled = true;
                    setFocusControl(txtCustCode);
                }

            }

            if (txtCustCode.Text == "2")
            {
                txtCustCode.Enabled = true;
                lblCustomer.Enabled = true;
                txtItemCode.Enabled = true;
                lblItem.Enabled = true;
                txtBNo.Enabled = true;
                txtQty.Enabled = true;
                txtNetWt.Enabled = true;
                txtPRate.Enabled = true;
                txtPAmt.Enabled = false;
                txtSRate.Enabled = false;
                txtSAmt.Enabled = false;
                txtPer.Enabled = false;
                txtNetRate.Enabled = false;
                txtSr.Enabled = false;
                txtcomm_amnt.Enabled = false;
                txtHamali_Code.Enabled = false;
                lblHamali_Name.Enabled = false;
                txtHamali_amnt.Enabled = false;
                txtPacking_Code.Enabled = false;
                lblPacking_name.Enabled = false;
                txtPacking_amnt.Enabled = false;
                txtOther_Per.Enabled = false;
                txtOther_Per_Amnt.Enabled = false;
                txtOther_PM.Enabled = false;
                txtNetRate.Enabled = false;
                txtgross_sale_amnt.Enabled = false;
                txtSr.Enabled = false;
                txtShub_Rate.Enabled = false;
                txtShub_Amnt.Enabled = false;
                TxtPanjar_Rate.Enabled = false;
                txtPanjar_Amnt.Enabled = false;
                txtLevidetails.Enabled = false;
                txtInsurance.Enabled = true;
                txtLevidetailsAmt.Enabled = false;
                txtInsuranceAmt.Enabled = false;
                calcultionForaccode();
                columnTotal();
            }
            else
            {
                if (txtBNo.Enabled == false)
                {
                    txtCustCode.Enabled = false;
                    lblCustomer.Enabled = false;
                    txtItemCode.Enabled = false;
                    txtBrand_Code.Enabled = false;
                    lblItem.Enabled = false;
                    txtBNo.Enabled = false;
                    txtQty.Enabled = false;
                    txtNetWt.Enabled = false;
                    txtPRate.Enabled = true;
                    txtPAmt.Enabled = false;
                    txtSRate.Enabled = false;
                    txtSAmt.Enabled = false;
                    txtPer.Enabled = false;
                    txtNetRate.Enabled = false;
                    txtSr.Enabled = false;
                    txtcomm_amnt.Enabled = false;
                    txtHamali_Code.Enabled = false;
                    lblHamali_Name.Enabled = false;
                    txtHamali_amnt.Enabled = false;
                    txtPacking_Code.Enabled = false;
                    lblPacking_name.Enabled = false;
                    txtPacking_amnt.Enabled = false;
                    txtOther_Per.Enabled = false;
                    txtOther_Per_Amnt.Enabled = false;
                    txtOther_PM.Enabled = false;
                    txtNetRate.Enabled = false;
                    txtgross_sale_amnt.Enabled = false;
                    txtSr.Enabled = false;
                    txtShub_Rate.Enabled = false;
                    txtShub_Amnt.Enabled = false;
                    TxtPanjar_Rate.Enabled = false;
                    txtPanjar_Amnt.Enabled = false;
                    txtLevidetails.Enabled = false;
                    txtInsurance.Enabled = true;
                    txtLevidetailsAmt.Enabled = false;
                    txtInsuranceAmt.Enabled = false;

                    calcultionForaccode();
                    columnTotal();
                    setFocusControl(txtPRate);
                }
                else
                {
                    txtCustCode.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtBrand_Code.Enabled = true;
                    txtBNo.Enabled = true;
                    txtQty.Enabled = true;
                    txtNetWt.Enabled = true;
                    txtPRate.Enabled = true;
                    txtPAmt.Enabled = true;
                    txtSRate.Enabled = true;
                    txtSAmt.Enabled = true;
                    txtPer.Enabled = true;
                    txtNetRate.Enabled = true;
                    txtSr.Enabled = true;
                    txtcomm_amnt.Enabled = true;
                    txtHamali_Code.Enabled = true;
                    lblHamali_Name.Enabled = true;
                    txtHamali_amnt.Enabled = true;
                    txtPacking_Code.Enabled = true;
                    lblPacking_name.Enabled = true;
                    txtPacking_amnt.Enabled = true;
                    txtOther_Per.Enabled = true;
                    txtOther_Per_Amnt.Enabled = true;
                    txtOther_PM.Enabled = true;
                    txtNetRate.Enabled = true;
                    txtgross_sale_amnt.Enabled = true;
                    txtSr.Enabled = true;
                    txtShub_Rate.Enabled = true;
                    txtShub_Amnt.Enabled = true;
                    TxtPanjar_Rate.Enabled = true;
                    txtPanjar_Amnt.Enabled = true;
                    txtLevidetails.Enabled = true;
                    txtInsurance.Enabled = true;
                    txtLevidetailsAmt.Enabled = true;
                    txtInsuranceAmt.Enabled = true;
                    calcultion();
                    columnTotal();
                    setFocusControl(txtCustCode);

                }

            }
        }
        catch (Exception)
        {
            throw;
        }
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
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where detail_id=" + ID + " and doc_no=" + txtdoc_no.Text
                    + " and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[39].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[39].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[39].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[39].Text = "A";
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
        try
        {
            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("300px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[17].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[18].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[19].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[20].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[21].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[22].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[23].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[24].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[25].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[26].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[27].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[28].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[29].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[30].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[31].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[32].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[33].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[34].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[35].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[36].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[37].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[38].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[39].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[40].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[41].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[42].ControlStyle.Width = new Unit("100px");

            e.Row.Cells[0].Style["overflow"] = "hidden";
            e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[2].Style["overflow"] = "hidden";
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[11].Style["overflow"] = "hidden";
            e.Row.Cells[6].Style["overflow"] = "hidden";
            e.Row.Cells[7].Style["overflow"] = "hidden";
            e.Row.Cells[8].Style["overflow"] = "hidden";
            e.Row.Cells[9].Style["overflow"] = "hidden";
            e.Row.Cells[10].Style["overflow"] = "hidden";
            e.Row.Cells[12].Style["overflow"] = "hidden";

            int i = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[27].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[28].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[30].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[31].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[32].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[33].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[34].HorizontalAlign = HorizontalAlign.Center;

                if (e.Row.Cells[4].Text.Length > 27)
                {
                    e.Row.Cells[4].Style["overflow"] = "hidden";
                    string s = e.Row.Cells[16].Text.ToString();
                    //e.Row.Cells[11].Text = e.Row.Cells[11].Text.Substring(0, 27) + "..";
                    e.Row.Cells[4].ToolTip = s;
                }
                if (e.Row.Cells[17].Text.Length > 27)
                {
                    e.Row.Cells[17].Style["overflow"] = "hidden";
                    string s = e.Row.Cells[11].Text.ToString();
                    //e.Row.Cells[11].Text = e.Row.Cells[11].Text.Substring(0, 27) + "..";
                    e.Row.Cells[11].ToolTip = s;
                }
            }
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;

        }
        catch
        {
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
                        if (grdDetail.Rows[rowindex].Cells[38].Text != "D" && grdDetail.Rows[rowindex].Cells[38].Text != "R")
                        {
                            //pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
                            // btnAdddetails_Click(this, new EventArgs());

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
            columnTotal();
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

    #region [btntxtSupplier_Click]
    protected void btntxtSupplier_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSupplier";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtCustomer_Click]
    protected void btntxtCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCustCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtawakno_Click]
    protected void btntxtawakno_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAwakNo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtItem_Click]
    protected void btntxtItem_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtItemCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
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


    #region [btntxtHamali_Code_Click]
    protected void btntxtHamali_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtHamali_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtPacking_Code_Click]
    protected void btntxtPacking_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPacking_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
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

    private void fordisablerowuseinsalebill()
    {
        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {
            Int32 bill = Convert.ToInt32(grdDetail.Rows[i].Cells[9].Text);
            string str = clsCommon.getString("select doc_no from NT_1_JSaleHead where doc_no=" + bill + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (bill != 0)
            {
                if (str == bill.ToString())
                {
                    grdDetail.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Red;
                    // grdDetail.Rows[i].Cells[0].ControlStyle.ForeColor=ConsoleColor.Red;
                    grdDetail.Rows[i].Cells[1].Enabled = false;
                }
            }
        }




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

                            string qry = "select * from " + tblHead + " where   Doc_No='" + txtValue + "' " +
                                "  and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";

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
                                                txtdoc_no.Enabled = false;

                                                btnSave.Enabled = true;   //IMP
                                                setFocusControl(txtdoc_date);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    //pnlgrdDetail.Enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtdoc_date);
                                            txtdoc_no.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            //this.makeEmptyForm("E");
                                            //lblMsg.Text = "** Record Not Found";
                                            //lblMsg.ForeColor = System.Drawing.Color.Red;
                                            //txtdoc_no.Text = string.Empty;
                                            //setFocusControl(txtdoc_no);
                                            ////pnlgrdDetail.Enabled = true;
                                            qry = getDisplayQuery();
                                            bool recordExist = this.fetchRecord(qry);
                                            if (recordExist == true)
                                            {
                                                pnlgrdDetail.Enabled = true;
                                                txtdoc_no.Enabled = false;
                                                setFocusControl(txtdoc_date);
                                                hdnf.Value = txtdoc_no.Text;
                                            }
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



            #region[doc date]
            if (strTextBox == "txtdoc_date")
            {
                if (txtdoc_date.Text != string.Empty)
                {
                    string dt = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDate(dt) == true)
                    {
                        //   setFocusControl(btnOpenDetailsPopup);
                    }
                    else
                    {
                        txtdoc_date.Text = string.Empty;
                        setFocusControl(txtdoc_date);
                    }
                }
                else
                {
                    setFocusControl(txtdoc_date);
                }
            }
            #endregion




            #region[AwakNo]
            if (strTextBox == "txtAwakNo")
            {
                if (txtAwakNo.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAwakNo.Text);
                    if (a == false)
                    {
                        btntxtawakno_Click(this, new EventArgs());
                    }
                    else
                    {
                        //string str = clsCommon.getString("select detail_id from qryAwakBalance where detail_id=" + hdnfTranType.Value.TrimStart() + " and doc_no=" + txtAwakNo.Text + " and TRAN_TYPE=" + "'AW'"
                        //    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //string awakyearcode = clsCommon.getString("select Year_Code from qryAwakBalance  where doc_no=" + txtAwakNo.Text + " and TRAN_TYPE=" + "'AW'"
                        //    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                        string awakyearcode = clsCommon.getString("select Year_Code from qryAwakBalance  where doc_no=" + txtAwakNo.Text + " and TRAN_TYPE=" + "'AW'"
                           + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + +Convert.ToInt32(Session["year"].ToString()));
                        if (awakyearcode != string.Empty || awakyearcode != "0")
                        {
                            lblAwakYearCode.Text = awakyearcode;
                            setFocusControl(txtAwakNo);
                        }
                        else
                        {
                            //lblSupplier.Text = string.Empty;
                            txtAwakNo.Text = string.Empty;
                            setFocusControl(txtAwakNo);
                        }
                    }

                }
                else
                {
                    // lblSupplier.Text = string.Empty;
                    txtAwakNo.Text = string.Empty;
                    setFocusControl(txtAwakNo);

                }
            }

            #endregion


            #region[hamali]
            if (strTextBox == "txtHamali_Code")
            {
                if (txtHamali_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtHamali_Code.Text);
                    if (a == false)
                    {
                        btntxtHamali_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        string str = clsCommon.getString("select [System_Name_E] from " + systemMasterTable + " where [System_Code]=" + txtHamali_Code.Text
                            + " and [System_Type]='H' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        ////string hamlirate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + " where [System_Code]=" + txtHamali_Code.Text
                        ////    + " and [System_Type]='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //string hamali_rate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + "  where [System_Code]="
                        // + txtHamali_Code.Text + " and System_Type='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (str != string.Empty && str != "0")
                        {
                            lblHamali_Name.Text = str;

                            //double h_rate = Convert.ToDouble(hamali_rate);

                            //double qty = Convert.ToDouble(txtQty.Text);
                            //double H_amount = Math.Round((h_rate * qty), 2);

                            //txtHamali_amnt.Text = Convert.ToString(H_amount);

                            setFocusControl(txtHamali_Code);
                        }
                        else
                        {
                            lblHamali_Name.Text = string.Empty;
                            txtHamali_Code.Text = string.Empty;
                            setFocusControl(txtHamali_Code);
                        }
                    }
                }
                else
                {
                    lblHamali_Name.Text = string.Empty;
                    txtHamali_Code.Text = string.Empty;
                    setFocusControl(txtHamali_Code);

                }

            }
            #endregion

            #region[packing]

            if (strTextBox == "txtPacking_Code")
            {
                if (txtPacking_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtPacking_Code.Text);
                    if (a == false)
                    {
                        btntxtPacking_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        string str = clsCommon.getString("select [System_Name_E] from " + systemMasterTable + " where [System_Code]=" + txtPacking_Code.Text
                            + " and [System_Type]='P' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        //string packing_rate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + "  where [System_Code]="
                        //+ txtPacking_Code.Text + " and System_Type='P' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (str != string.Empty && str != "0")
                        {
                            lblPacking_name.Text = str;
                            setFocusControl(txtPacking_Code);

                            //double p_rate = Convert.ToDouble(packing_rate);
                            //double qty = Convert.ToDouble(txtQty.Text);

                            //double P_amount = Math.Round((p_rate * qty), 2);

                            //txtPacking_amnt.Text = Convert.ToString(P_amount);
                        }
                        else
                        {
                            lblPacking_name.Text = string.Empty;
                            txtPacking_Code.Text = string.Empty;
                            setFocusControl(txtPacking_Code);
                        }
                    }
                }
                else
                {
                    lblPacking_name.Text = string.Empty;
                    txtPacking_Code.Text = string.Empty;
                    setFocusControl(txtPacking_Code);

                }

            }
            #endregion

            #region[cust cocdee]
            if (strTextBox == "txtCustCode")
            {
                if (txtCustCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtCustCode.Text);
                    if (a == false)
                    {
                        btntxtCustomer_Click(this, new EventArgs());
                    }
                    else
                    {

                        string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code!=2 and Ac_Code=" + txtCustCode.Text
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string comm_rate = clsCommon.getString("select [Commission] from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                        if (str != string.Empty && str != "0")
                        {
                            lblCustomer.Text = str;
                            txtPer.Text = comm_rate;
                            setFocusControl(txtItemCode);

                        }
                        else
                        {
                            lblCustomer.Text = string.Empty;
                            txtPer.Text = string.Empty;
                            txtCustCode.Text = string.Empty;
                            setFocusControl(txtCustCode);
                        }
                    }
                }
                else
                {
                    lblCustomer.Text = string.Empty;
                    txtCustCode.Text = string.Empty;
                    setFocusControl(txtCustCode);

                }


            }
            #endregion

            #region[brandcode]
            if (strTextBox == "txtBrand_Code")
            {
                if (txtBrand_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBrand_Code.Text);
                    if (a == false)
                    {
                        btntxtBrand_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        string str = clsCommon.getString("select [Brand_Name] from [NT_1_BrandMaster]  where [Doc_No]="
                            + txtBrand_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string str_hamali = clsCommon.getString("select [Hamali_Code] from [NT_1_BrandMaster]  where [Doc_No]="
                           + txtBrand_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string str_packing = clsCommon.getString("select [Packing_Code] from [NT_1_BrandMaster]  where [Doc_No]="
                           + txtBrand_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string hamalinm = clsCommon.getString("select [System_Name_E] from " + systemMasterTable + "  where [System_Code]="
                           + str_hamali + " and System_Type='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string packingnm = clsCommon.getString("select [System_Name_E] from " + systemMasterTable + "  where [System_Code]="
                          + str_packing + " and System_Type='P' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        //string hamali_rate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + "  where [System_Code]="
                        //  + str_hamali + " and System_Type='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //string packing_rate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + "  where [System_Code]="
                        //  + str_packing + " and System_Type='P' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (str != string.Empty && str != "0")
                        {
                            lblBrandName.Text = str;
                            txtHamali_Code.Text = str_hamali;
                            lblHamali_Name.Text = hamalinm;
                            txtPacking_Code.Text = str_packing;
                            lblPacking_name.Text = packingnm;
                            //txtHamali_amnt.Text = hamali_amnt;
                            //txtPacking_amnt.Text = packing_anmt;

                            //double sale_amount = Convert.ToDouble("0" + txtSAmt.Text);
                            //double commishion_rate = Convert.ToDouble("0" + txtPer.Text);
                            //double commishion_Amnt = Math.Round((sale_amount * commishion_rate / 100), 2);
                            //txtcomm_amnt.Text = commishion_Amnt.ToString();

                            //double h_rate = Convert.ToDouble(hamali_rate);
                            //double p_rate = Convert.ToDouble(packing_rate);
                            //double qty = Convert.ToDouble(txtQty.Text);
                            //double H_amount = Math.Round((h_rate * qty), 2);
                            //double P_amount = Math.Round((p_rate * qty), 2);
                            //txtHamali_amnt.Text = Convert.ToString(H_amount);
                            //txtPacking_amnt.Text = Convert.ToString(P_amount);


                            setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            lblBrandName.Text = string.Empty;
                            txtBrand_Code.Text = string.Empty;
                            setFocusControl(txtBrand_Code);
                        }
                    }
                }
                else
                {
                    lblBrandName.Text = string.Empty;
                    txtBrand_Code.Text = string.Empty;
                    setFocusControl(txtBrand_Code);

                }


            }

            #endregion

            if (strTextBox == "txtPRate")
            {

                setFocusControl(txtPAmt);
                txtSRate.Text = txtPRate.Text;

            }

            if (strTextBox == "txtSRate")
            {
                setFocusControl(txtPer);
            }

            if (strTextBox == "txtPer")
            {
                setFocusControl(txtHamali_Code);
            }

            if (strTextBox == "txtQty")
            {

                bool isValidated = true;
                Int32 jawakqnty = Convert.ToInt32(txtQty.Text);

                //string str = clsCommon.getString("select balnceqty from NT_1_qryJABalance where doc_no=" + txtAwakNo.Text + " and detail_id="
                //    + lblAwakQuantity.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string str = clsCommon.getString("select balnceqty from NT_1_qryJABalance where doc_no=" + txtAwakNo.Text + " and detail_id="
                  + lblAwakQuantity.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + lblAwakYearCode.Text);
                // Int32 str1=Convert.ToInt32(str).ToString();
                if (jawakqnty > Convert.ToInt32(str))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Enter Correct quantity');", true);
                    isValidated = false;
                    setFocusControl(txtQty);
                    return;
                }
                else
                {

                    setFocusControl(txtBrand_Code);//--
                }

            }

            if (strTextBox == "txtOther_Per")
            {
                setFocusControl(txtOther_PM);
            }

            if (strTextBox == "txtgross_sale_amnt")
            {
                setFocusControl(txtSr);
            }

            if (strTextBox == "txtNetRate")
            {
                setFocusControl(txtSr);
            }


            if (strTextBox == "txtShub_Rate")
            {
                setFocusControl(TxtPanjar_Rate);
            }

            if (strTextBox == "TxtPanjar_Rate")
            {
                setFocusControl(btnAdddetails);
            }



            if (strTextBox == "txtOther_PM")
            {
                setFocusControl(txtSr);

            }

            if (strTextBox == "txtpochrate")
            {
                setFocusControl(txtfrieghtRate);

            }
            if (strTextBox == "txtfrieghtRate")
            {
                setFocusControl(txtBNo);

            }



            #region[itemcode]
            if (strTextBox == "txtItemCode")
            {
                if (txtItemCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtItemCode.Text);
                    if (a == false)
                    {
                        btntxtItem_Click(this, new EventArgs());
                    }
                    else
                    {
                        //string itemcode = clsCommon.getString("select Item_Code from qryAwakBalance where detail_id=" + hdnfTranType.Value.Trim() + " and doc_no=" + txtAwakNo.Text + " and TRAN_TYPE=" + "'AW'"
                        //    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string itemcode = clsCommon.getString("select Item_Code from qryAwakBalance where detail_id=" + hdnfTranType.Value.Trim() + " and doc_no=" + txtAwakNo.Text + " and TRAN_TYPE=" + "'AW'"
                           + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + lblAwakYearCode.Text);
                        if (itemcode == txtItemCode.Text)
                        {
                            string str = clsCommon.getString("select System_Name_E from " + systemMasterTable + " where System_Type='I' and  System_Code="
                                + txtItemCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string LEVIcode = clsCommon.getString("select [Levi] from " + systemMasterTable + " where  [System_Code]=" + txtItemCode.Text
                                      + " and [System_Type]='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string levirate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + " where  [System_Code]=" + LEVIcode
                                      + " and [System_Type]='L' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            txtLevidetails.Text = levirate;
                            string Insurancecode = clsCommon.getString("select [Insurance] from " + systemMasterTable + " where  [System_Code]=" + txtItemCode.Text
                                        + " and [System_Type]='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string Insurancerate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + " where  [System_Code]=" + Insurancecode
                                      + " and [System_Type]='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            txtInsurance.Text = Insurancerate;
                            if (str != string.Empty && str != "0")
                            {
                                //string qry = "select * from qryAwakBalance where detail_id=" + hdnfTranType.Value.Trim() + " and doc_no=" + txtAwakNo.Text
                                //    + " and TRAN_TYPE='AW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());// +" and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                                string qry = "select * from qryAwakBalance where detail_id=" + hdnfTranType.Value.Trim() + " and doc_no=" + txtAwakNo.Text
                                   + " and TRAN_TYPE='AW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "and Year_Code=" + lblAwakYearCode.Text;
                                DataSet ds = new DataSet();
                                ds = clsDAL.SimpleQuery(qry);
                                DataTable dt = new DataTable();
                                dt = ds.Tables[0];
                                //txtSupplier.Text = dt.Rows[0]["AC_CODE"].ToString();
                                //lblSupplier.Text = dt.Rows[0]["Customername"].ToString();

                                txtBrand_Code.Text = dt.Rows[0]["Brand_Code"].ToString();
                                lblBrandName.Text = dt.Rows[0]["Brand_Name"].ToString();
                                //txtQty.Text = dt.Rows[0]["Qty"].ToString();
                                //txtNetWt.Text = dt.Rows[0]["Net_Wt"].ToString();
                                //txtItemCode.Text = dt.Rows[0]["Item_Code"].ToString();

                                txtPRate.Text = dt.Rows[0]["Purc_Rate"].ToString();
                                txtPAmt.Text = dt.Rows[0]["Purc_Amnt"].ToString();
                                txtSRate.Text = txtPRate.Text;
                                txtSAmt.Text = dt.Rows[0]["Sale_Amnt"].ToString();
                                txtPacking_Code.Text = dt.Rows[0]["Packing_Code"].ToString();
                                txtHamali_Code.Text = dt.Rows[0]["Hamali_Code"].ToString();
                                lblItem.Text = str;
                                lblAwakQuantity.Text = hdnfTranType.Value;
                                setFocusControl(txtBrand_Code);
                            }
                            else
                            {
                                lblItem.Text = string.Empty;
                                txtItemCode.Text = string.Empty;
                                setFocusControl(txtItemCode);
                            }
                        }
                        else
                        {
                            setFocusControl(txtItemCode);
                        }
                    }

                }
                else
                {
                    lblItem.Text = string.Empty;
                    txtItemCode.Text = string.Empty;
                    setFocusControl(txtItemCode);

                }


            }
            #endregion

            #region[comment]
            //string ac_city = clsCommon.getString("select CityName from " + tblPrefix + "qryAccountsList where Ac_Code=" + txtACCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //if (strTextBox == "txtVoucherNo")
            //{
            //    setFocusControl(txtamount);
            //    DataSet ds;
            //    if (drpFilter.SelectedValue == "V")
            //    {
            //        if (txtVoucherNo.Text != string.Empty)
            //        {
            //            qry = "";// "select * from " + tblPrefix + "Voucher where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Doc_No=" + txtVoucherNo.Text + " and Tran_Type='" + txtvoucherType.Text + "'";
            //            qry = "select Doc_No as doc_no,Tran_Type,Suffix,Convert(varchar(10),Doc_Date,103) as Doc_Date,PartyName,Unit_Name,NETQNTL,BrokerName,Sale_Rate,Bill_Amount,mill_code, " +
            //                  " (Select ISNULL(SUM(amount),0) as UA from " + tblPrefix + "Transact where Voucher_No=" + tblPrefix + "qryVoucherSaleUnion.Doc_No and Voucher_Type=" + tblPrefix + "qryVoucherSaleUnion.Tran_Type and Company_Code=" + tblPrefix + "qryVoucherSaleUnion.Company_Code and Year_Code=" + tblPrefix + "qryVoucherSaleUnion.Year_Code" +
            //                  " ) as Paid_Amount,(Bill_Amount - (Select ISNULL(SUM(amount),0) as UA from " + tblPrefix + "Transact where " +
            //                  " Voucher_No=" + tblPrefix + "qryVoucherSaleUnion.Doc_No and Voucher_Type=" + tblPrefix + "qryVoucherSaleUnion.Tran_Type and Year_Code=" + tblPrefix + "qryVoucherSaleUnion.Year_Code and Company_Code=" + tblPrefix + "qryVoucherSaleUnion.Company_Code )) as Balance " +
            //                  " from " + tblPrefix + "qryVoucherSaleUnion where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "" +
            //                  "  and doc_no=" + txtVoucherNo.Text + " and Tran_Type='" + txtvoucherType.Text + "'";

            //            ds = new DataSet();
            //            ds = clsDAL.SimpleQuery(qry);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                string VocNo = "Vouch No:" + txtVoucherNo.Text + "(" + txtvoucherType.Text + ")";
            //                string millcode = ds.Tables[0].Rows[0]["mill_code"].ToString();
            //                string millshortname = clsCommon.getString("Select Short_Name from " + tblPrefix + "AccountMaster where Ac_Code=" + millcode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //                double Qntl = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["NETQNTL"].ToString())), 2);
            //                double SR = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["Sale_Rate"].ToString())), 2);
            //                //double frt = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["FreightPerQtl"].ToString())), 2);
            //                //string naration = VocNo + " " + millshortname + "-" + Qntl + "-(SR:" + SR + "-frt:" + frt + ")";
            //                string naration = VocNo + " " + millshortname + "-" + Qntl + "-(SR:" + SR + ")";
            //                txtnarration.Text = naration;
            //                hdnfTransportBalance.Value = ds.Tables[0].Rows[0]["Balance"].ToString();
            //            }
            //            else
            //            {
            //                txtnarration.Text = "";
            //            }
            //        }
            //    }
            //    if (drpFilter.SelectedValue == "D")
            //    {
            //        if (txtVoucherNo.Text != string.Empty)
            //        {
            //            qry = "select Doc_No,MillName,Date,PartyName,Sale_Rate,Purchase_Rate,Quantal,Balance from NT_1_qryDebitNotesForBankReciept where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "" +
            //                  "  and Doc_No=" + txtVoucherNo.Text + " and Tran_Type='LV'";

            //            ds = new DataSet();
            //            ds = clsDAL.SimpleQuery(qry);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                string VocNo = "Vouch No:" + txtVoucherNo.Text + "(LV)";
            //                string millshortname = ds.Tables[0].Rows[0]["MillName"].ToString();
            //                double Qntl = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["Quantal"].ToString())), 2);
            //                double SR = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["Sale_Rate"].ToString())), 2);
            //                //double frt = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["FreightPerQtl"].ToString())), 2);
            //                //string naration = VocNo + " " + millshortname + "-" + Qntl + "-(SR:" + SR + "-frt:" + frt + ")";
            //                string naration = VocNo + " " + millshortname + "-" + Qntl + "-(SR:" + SR + ")";
            //                txtnarration.Text = naration;
            //                hdnfTransportBalance.Value = ds.Tables[0].Rows[0]["Balance"].ToString();
            //            }
            //            else
            //            {
            //                txtnarration.Text = "";
            //            }
            //        }
            //    }
            //    if (drpFilter.SelectedValue == "S")
            //    {
            //        string qry = "";  // "Select * from " + tblPrefix + "qryTenderList where Tender_No=" + txtVoucherNo.Text + " and ID=" + txtvoucherType.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            //        qry = "select [Tender_No],[ID],Convert(VarChar(10),[Tender_Date],103) as Tender_Date,[millname],[salerate],[salepartyfullname],[Buyer_Quantal],[salevalue],[received],[balance],[Commission_Rate] from " + tblPrefix + "qrySaudaBalance" +
            //           " where [Company_Code]=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_No=" + txtVoucherNo.Text + " and ID=" + txtvoucherType.Text + "";
            //        ds = new DataSet();
            //        ds = clsDAL.SimpleQuery(qry);
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            string millshortname = ds.Tables[0].Rows[0]["millname"].ToString();
            //            //string millshortname = clsCommon.getString("Select Short_Name from " + tblPrefix + "AccountMaster where Ac_Code=" + millcode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //            double Qntl = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["Buyer_Quantal"].ToString())), 2);
            //            double SR = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["salerate"].ToString())), 2);
            //            double Commission = Math.Round((Convert.ToDouble(ds.Tables[0].Rows[0]["Commission_Rate"].ToString())), 2);
            //            double Sale_Rate = SR - Commission;
            //            string SaudaDate = ds.Tables[0].Rows[0]["Tender_Date"].ToString();
            //            txtnarration.Text = millshortname + " " + Qntl + "(SR:" + Sale_Rate + "+Comm:" + Commission + ")dt-" + SaudaDate;
            //            hdnfTransportBalance.Value = ds.Tables[0].Rows[0]["balance"].ToString();
            //        }
            //    }
            //    if (drpFilter.SelectedValue == "T")
            //    {
            //        string qry = ""; // "Select * from " + tblPrefix + "qryDeliveryOrderListReport where doc_no=" + txtVoucherNo.Text.Trim() + " and tran_type='" + txtvoucherType.Text.Trim() + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            //        qry = "select do.doc_no,do.voucher_no,do.quantal,do.tran_type,Convert(varchar(10),do.doc_date,103) as date,do.millName as Mill,do.truck_no,do.TransportName,do.Memo_Advance," +
            //             " (Select (do.vasuli_amount+ISNULL(SUM(amount),0)) as UA from " + tblPrefix + "Transact where Voucher_No=do.doc_no and Voucher_Type=do.tran_type and credit_ac=do.transport and Company_Code=do.company_code and Year_Code=do.Year_Code ) as Paid,((do.Memo_Advance)-" +
            //             "(Select (do.vasuli_amount+ISNULL(SUM(amount),0)) as UA from " + tblPrefix + "Transact where Voucher_No=do.doc_no and Voucher_Type=do.tran_type and credit_ac=do.transport and Company_Code=do.company_code and Year_Code=do.Year_Code)) as Balance" +
            //             " from " + tblPrefix + "qryDeliveryOrderListReport do where tran_type='DO' and (do.Memo_Advance-do.vasuli_amount)!=0 and do.doc_no=" + txtVoucherNo.Text.Trim() + " and do.tran_type='" + txtvoucherType.Text.Trim() + "' and do.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and do.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
            //        ds = new DataSet();
            //        ds = clsDAL.SimpleQuery(qry);
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            string vn = "";
            //            string VoucherNo = ds.Tables[0].Rows[0]["voucher_no"].ToString();
            //            string lorry = ds.Tables[0].Rows[0]["truck_no"].ToString();
            //            string qntl = ds.Tables[0].Rows[0]["quantal"].ToString();
            //            string Memo_Advance = ds.Tables[0].Rows[0]["Memo_Advance"].ToString();
            //            if (VoucherNo != "0")
            //            {
            //                vn = "Vouc.No.:" + VoucherNo;
            //            }
            //            string ac_city = clsCommon.getString("select CityName from " + tblPrefix + "qryAccountsList where Ac_Code=" + txtACCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //            string narration = vn + " Lorry:" + lorry + " Qntl:" + qntl + "-" + Memo_Advance + "(" + ac_city + ")";
            //            txtnarration.Text = narration;
            //            hdnfTransportBalance.Value = ds.Tables[0].Rows[0]["Balance"].ToString();
            //        }
            //    }
            //}
            //if (strTextBox == "txtamount")
            //{
            //    if (txtVoucherNo.Text != string.Empty)
            //    {
            //        string hdnfVal = hdnfTransportBalance.Value.ToString().TrimStart();
            //        if (!string.IsNullOrEmpty(hdnfVal))
            //        {
            //            double TransportAdvanceBalance = hdnfVal != string.Empty ? Convert.ToDouble(hdnfVal) : 0.00;
            //            double amount1 = Convert.ToDouble(txtamount.Text);
            //            if (amount1 > TransportAdvanceBalance)
            //            {
            //                lblErrorAdvance.Text = "Amount Is Greater Than Transport Advance Balance!";
            //                setFocusControl(txtamount);
            //            }
            //            else
            //            {
            //                lblErrorAdvance.Text = "";
            //            }
            //        }
            //    }
            //    setFocusControl(txtadAmount);
            //}
            //if (strTextBox == "txtAmt")
            //{
            //    setFocusControl(txtChqNo);
            //}
            //if (strTextBox == "txtChqNo")
            //{
            //    setFocusControl(txtBerror);
            //}
            //if (strTextBox == "txtBerror")
            //{
            //    setFocusControl(txtBankDt);
            //}
            //if (strTextBox == "txtBankDt")
            //{
            //    setFocusControl(txtnarration);
            //}
            //if (strTextBox == "txtnarration")
            //{
            //    setFocusControl(txtMobile);
            //}

            //  this.columnTotal();
            //Purchase_Amnt_Total = Convert.ToDouble(dt.Compute("SUM(Purc_Amnt)", string.Empty));
            //Sale_Amnt_Total = Convert.ToDouble(dt.Compute("SUM(Gross_Amnt)", string.Empty));
            #endregion

            // int accode=
            // int CustCode1 =Convert.ToInt32(txtCustCode.Text).ToString();
            //if(Convert.ToInt32(txtCustCode.Text).ToString()==string.Empty)
            //{
            string accode = ((txtCustCode.Text).ToString());
            //for (int i = 0; i < grdDetail.Rows.Count; i++)
            //{

            //    CustCode1 = Convert.ToInt32(grdDetail.Rows[i].Cells[3].Text);
            //}
            if (accode == string.Empty || accode == "")
            {
                calcultionForaccode();
                calcultion();
            }
            else
            {
                if (accode == "2")
                {
                    txtCustCode.Enabled = true;
                    lblCustomer.Enabled = true;
                    txtItemCode.Enabled = true;
                    lblItem.Enabled = true;
                    txtBNo.Enabled = true;
                    txtQty.Enabled = true;
                    txtNetWt.Enabled = true;
                    txtPRate.Enabled = true;
                    txtPAmt.Enabled = false;
                    txtSRate.Enabled = false;
                    txtSAmt.Enabled = false;
                    txtPer.Enabled = false;
                    txtNetRate.Enabled = false;
                    txtSr.Enabled = false;
                    txtcomm_amnt.Enabled = false;
                    txtHamali_Code.Enabled = false;
                    lblHamali_Name.Enabled = false;
                    txtHamali_amnt.Enabled = false;
                    txtPacking_Code.Enabled = false;
                    lblPacking_name.Enabled = false;
                    txtPacking_amnt.Enabled = false;
                    txtOther_Per.Enabled = false;
                    txtOther_Per_Amnt.Enabled = false;
                    txtOther_PM.Enabled = false;
                    txtNetRate.Enabled = false;
                    txtgross_sale_amnt.Enabled = false;
                    txtSr.Enabled = false;
                    txtShub_Rate.Enabled = false;
                    txtShub_Amnt.Enabled = false;
                    TxtPanjar_Rate.Enabled = false;
                    txtPanjar_Amnt.Enabled = false;
                    txtLevidetails.Enabled = false;
                    txtInsurance.Enabled = true;
                    calcultionForaccode();
                    columnTotal();
                    //setFocusControl(txtPRate);
                }
                else
                {
                    if (txtBNo.Enabled == false)
                    {
                        txtCustCode.Enabled = false;
                        lblCustomer.Enabled = false;
                        txtItemCode.Enabled = false;
                        txtBrand_Code.Enabled = false;
                        lblItem.Enabled = false;
                        txtBNo.Enabled = false;
                        txtQty.Enabled = false;
                        txtNetWt.Enabled = false;
                        txtPRate.Enabled = true;
                        txtPAmt.Enabled = false;
                        txtSRate.Enabled = false;
                        txtSAmt.Enabled = false;
                        txtPer.Enabled = false;
                        txtNetRate.Enabled = false;
                        txtSr.Enabled = false;
                        txtcomm_amnt.Enabled = false;
                        txtHamali_Code.Enabled = false;
                        lblHamali_Name.Enabled = false;
                        txtHamali_amnt.Enabled = false;
                        txtPacking_Code.Enabled = false;
                        lblPacking_name.Enabled = false;
                        txtPacking_amnt.Enabled = false;
                        txtOther_Per.Enabled = false;
                        txtOther_Per_Amnt.Enabled = false;
                        txtOther_PM.Enabled = false;
                        txtNetRate.Enabled = false;
                        txtgross_sale_amnt.Enabled = false;
                        txtSr.Enabled = false;
                        txtShub_Rate.Enabled = false;
                        txtShub_Amnt.Enabled = false;
                        TxtPanjar_Rate.Enabled = false;
                        txtPanjar_Amnt.Enabled = false;
                        txtLevidetails.Enabled = false;
                        txtInsurance.Enabled = true;
                        calcultionForaccode();
                        columnTotal();

                        setFocusControl(btnAdddetails);
                    }
                    else
                    {
                        txtCustCode.Enabled = true;
                        txtItemCode.Enabled = true;
                        txtBrand_Code.Enabled = true;
                        txtBNo.Enabled = true;
                        txtQty.Enabled = true;
                        txtNetWt.Enabled = true;
                        txtPRate.Enabled = true;
                        txtPAmt.Enabled = true;
                        txtSRate.Enabled = true;
                        txtSAmt.Enabled = true;
                        txtPer.Enabled = true;
                        txtNetRate.Enabled = true;
                        txtSr.Enabled = true;
                        txtcomm_amnt.Enabled = true;
                        txtHamali_Code.Enabled = true;
                        lblHamali_Name.Enabled = true;
                        txtHamali_amnt.Enabled = true;
                        txtPacking_Code.Enabled = true;
                        lblPacking_name.Enabled = true;
                        txtPacking_amnt.Enabled = true;
                        txtOther_Per.Enabled = true;
                        txtOther_Per_Amnt.Enabled = true;
                        txtOther_PM.Enabled = true;
                        txtNetRate.Enabled = true;
                        txtgross_sale_amnt.Enabled = true;
                        txtSr.Enabled = true;
                        txtShub_Rate.Enabled = true;
                        txtShub_Amnt.Enabled = true;
                        TxtPanjar_Rate.Enabled = true;
                        txtPanjar_Amnt.Enabled = true;
                        txtLevidetails.Enabled = true;
                        txtInsurance.Enabled = true;
                        calcultion();
                        columnTotal();
                        // setFocusControl(txtPRate);

                    }

                }


            }
        }
        catch
        {
        }
    }

    private void calcultion()
    {



        string hamali_rate = "0.00";
        string packing_rate = "0.00";


        if (txtpochrate.Text == "0" || txtpochrate.Text == string.Empty || txtpochrate.Text == "0.00")
        {


            hamali_rate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + "  where [System_Code]="
                          + txtHamali_Code.Text + " and System_Type='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            packing_rate = clsCommon.getString("select [System_Rate] from " + systemMasterTable + "  where [System_Code]="
             + txtPacking_Code.Text + " and System_Type='P' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        }
        else
        {
            txtHamali_Code.Text = "1";
            string str = clsCommon.getString("select [System_Name_E] from " + systemMasterTable + " where [System_Code]=" + txtHamali_Code.Text
                        + " and [System_Type]='H' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblHamali_Name.Text = str;
            txtPacking_Code.Text = "1";
            string str1 = clsCommon.getString("select [System_Name_E] from " + systemMasterTable + " where [System_Code]=" + txtPacking_Code.Text
                          + " and [System_Type]='P' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblPacking_name.Text = str1;
            txtPer.Text = "0";
            txtPacking_amnt.Text = "0";
            txtHamali_amnt.Text = "0";
            txtOther_Per.Text = "0";
            txtOther_Per_Amnt.Text = "0";
            txtOther_PM.Text = "0";
            txtLevidetails.Text = "0";
            txtLevidetailsAmt.Text = "0";
            txtInsurance.Text = "0";
            txtInsuranceAmt.Text = "0";

        }
        #region[Net Wt]
        double jawakqty = Convert.ToDouble("0" + txtQty.Text);
        double awaknetwt = Convert.ToDouble(clsCommon.getString("select Net_Wt from NT_1_qryJABalance where doc_no=" + txtAwakNo.Text + " and detail_id=" + lblAwakQuantity.Text
            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
        double awakquantity = Convert.ToDouble(clsCommon.getString("select awakqty from NT_1_qryJABalance where doc_no=" + txtAwakNo.Text + " and detail_id="
            + lblAwakQuantity.Text + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
        double jawatnetqauntity = (awaknetwt / awakquantity) * jawakqty;




        if (txtNetWt.Text != string.Empty)
        {
            // txtNetWt.Text = Math.Round(jawatnetqauntity, 2).ToString();
        }
        else
        {
            txtNetWt.Text = Math.Round(jawatnetqauntity, 2).ToString();

        }
        #endregion


        #region[hamali amnt & packing amnt]

        double h_rate = Convert.ToDouble("0" + hamali_rate);
        double p_rate = Convert.ToDouble("0" + packing_rate);
        double qty = Convert.ToDouble("0" + txtQty.Text);
        //double commishion_Amnt = Math.Round((sale_amount * commishion_rate / 100), 2);
        double H_amount = Math.Round(((h_rate * qty)), 2);
        double P_amount = Math.Round(((p_rate * qty)), 2);
        txtHamali_amnt.Text = Convert.ToString(H_amount);
        txtPacking_amnt.Text = P_amount.ToString();
        #endregion

        #region[purchase amnt]
        double purchase_rate = Convert.ToDouble("0" + txtPRate.Text);
        double netwt = Convert.ToDouble("0" + txtNetWt.Text);
        double purchase_amnt = Math.Round(((purchase_rate * netwt) / 100), 2);
        txtPAmt.Text = purchase_amnt.ToString();
        #endregion

        #region[sal amnt]
        //txtSRate.Text = txtPRate.Text;
        double sale_rate = Convert.ToDouble("0" + txtSRate.Text);
        double sale_amnt = Math.Round(((sale_rate * netwt) / 100), 2);
        txtSAmt.Text = sale_amnt.ToString();
        #endregion

        #region[commision amnt]
        double sale_amount = Convert.ToDouble("0" + txtSAmt.Text);
        double commishion_rate = Convert.ToDouble("0" + txtPer.Text);
        double commishion_Amnt = Math.Round((sale_rate * commishion_rate / 100), 2);

        commishion_Amnt = Math.Round(commishion_Amnt * netwt / 100, 2);
        txtcomm_amnt.Text = commishion_Amnt.ToString();


        //CHK

        double amntchk = Math.Round(((sale_rate * commishion_rate) / 100) + sale_rate, 2);
        double sale = Math.Round((amntchk * netwt) / 100, 2);
        #endregion

        #region[other amnt]
        double other_rate = Convert.ToDouble("0" + txtOther_Per.Text);
        double other_amnt = Math.Round(((other_rate * netwt) / 100), 2);
        txtOther_Per_Amnt.Text = other_amnt.ToString();
        #endregion

        #region[gross sale amnt]
        //double otherpm = Convert.ToDouble("0" + txtOther_PM.Text);
        double otherpm = txtOther_PM.Text != string.Empty ? Convert.ToDouble(txtOther_PM.Text) : 0.00;
        double levirate = Convert.ToDouble("0" + txtLevidetails.Text);
        double insurancerate = Convert.ToDouble("0" + txtInsurance.Text);
        double insuranceamt = Math.Round((insurancerate * netwt), 2);
        txtInsuranceAmt.Text = insuranceamt.ToString();
        double levicalculation = levirate * qty;
        txtLevidetailsAmt.Text = levicalculation.ToString();
        // double grossamnt = Math.Round((Convert.ToDouble(sale_amnt + commishion_Amnt + H_amount + P_amount + other_amnt + otherpm)), 2);
        double grossamnt = Math.Round((Convert.ToDouble(sale_amnt + commishion_Amnt + H_amount + P_amount + other_amnt + otherpm + levicalculation + insuranceamt)), 3);
        txtgross_sale_amnt.Text = grossamnt.ToString();

        #endregion

        #region[gross rate]
        double txtgross_amnt = Convert.ToDouble("0" + txtgross_sale_amnt.Text);
        double netwt1 = Convert.ToDouble("0" + txtNetWt.Text);
        double gross_rate = Math.Round((Convert.ToDouble(txtgross_amnt / netwt1) * 100), 2);
        //txtNetRate.Text = "0.0";
        txtNetRate.Text = gross_rate.ToString();
        #endregion


        Int32 companycode = Convert.ToInt32(Session["Company_Code"].ToString());
        double ppolrate = Convert.ToDouble(clsCommon.getString("select P_Pol_Rate from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + companycode));
        double shubratre = Convert.ToDouble(clsCommon.getString("select Shub_Rate from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + companycode));

        #region[panjar amnt]
        //double panar_rate = 0.02;
        //if (companycode == 3)
        //{
        //    TxtPanjar_Rate.Text = "0";
        //}
        //else
        //{
        //    TxtPanjar_Rate.Text = "0.02";
        //}
        TxtPanjar_Rate.Text = ppolrate.ToString();
        double panjaramnt = Math.Round((Convert.ToDouble((TxtPanjar_Rate.Text).ToString()) * qty), 2);
        txtPanjar_Amnt.Text = panjaramnt.ToString();
        #endregion

        #region[shubaMt]
        txtShub_Rate.Text = shubratre.ToString();
        //double shub_rate = 0.06;
        //if (companycode == 3)
        //{
        //    txtShub_Rate.Text = "0.04";
        //}
        //else
        //{
        //    txtShub_Rate.Text = "0.06";
        //}
        //double shunamnt = Math.Round(Convert.ToDouble((txtShub_Rate.Text).ToString()) * (sale_amnt + panjaramnt) / 100, 2);
        double shunamnt = Math.Round(Convert.ToDouble((txtShub_Rate.Text).ToString()) * (sale_amnt + commishion_Amnt) / 100, 2);
        txtShub_Amnt.Text = shunamnt.ToString();

        txtgross_sale_amnt.Text = Convert.ToString(grossamnt + shunamnt);
        grossamnt = grossamnt + shunamnt;
        txtNetRate.Text = Convert.ToString((grossamnt / netwt) * 100);
        #endregion

        double pochrate = Convert.ToDouble("0" + txtpochrate.Text);
        if (pochrate != 0)
        {
            #region[poch amnt]

            //double pochrate = Convert.ToDouble("0" + txtpochrate.Text);
            double pochamt = Math.Round(((pochrate * netwt) / 100), 2);
            double Freightrate = Convert.ToDouble("0" + txtfrieghtRate.Text);
            double Lessfrieght = Math.Round(((pochamt - Freightrate)), 2);
            double gstrate = 0.00;
            // gstrate = Math.Round(((Lessfrieght * 5) / 100), 2);
            double gstamt = 0.00;
            // gstamt = Math.Round((Lessfrieght + gstrate), 2);
            double amtcal = 0.00;
            double kgperkatta = 0.00;
            double frieghtamt = 0.00;
            double itemvalue = 0.00;
            double itemgstratecode = 0.00;
            double itemgstrate = 0.00;
            kgperkatta = Convert.ToDouble(clsCommon.getString("select weight from NT_1_JaggarySystemMaster where System_Type='I' and Company_Code=" + companycode + " and System_Code=" + txtItemCode.Text));
            itemgstratecode = Convert.ToDouble(clsCommon.getString("select gstratecode from NT_1_JaggarySystemMaster where System_Type='I' and Company_Code=" + companycode + " and System_Code=" + txtItemCode.Text));
            itemgstrate = Convert.ToDouble(clsCommon.getString("select Rate from nt_1_gstratemaster where Company_Code=" + companycode + " and Doc_no=" + itemgstratecode));
            
            frieghtamt = Math.Round((kgperkatta * qty) / 30, 0);
            frieghtamt = frieghtamt * Freightrate;

            itemvalue = Math.Round(((pochrate * netwt) / 100), 2);
            itemvalue = itemvalue - frieghtamt;
            if (itemgstrate != 0)
            {
                gstamt = Math.Round((itemvalue * itemgstrate / 100), 2);
            }
            
            gstamt = gstamt + itemvalue;
            gstamt = Math.Round((itemvalue / gstamt) * itemvalue, 2);

            //amtcal = (Lessfrieght / gstamt);
            double taxable = 0.00;
            //  taxable = Math.Round((amtcal * Lessfrieght), 2);
            txtSAmt.Text = gstamt.ToString();
            txtSRate.Text = Math.Round((gstamt / netwt) * 100, 2).ToString();
            txtNetRate.Text = txtSRate.Text;
            txtgross_sale_amnt.Text = gstamt.ToString();
            //txtgross_sale_amnt.Text = taxable.ToString();



            double grosssaleamt = 0.00;
            grosssaleamt = Math.Round(((taxable / netwt) * 100), 2);
            // txtNetRate.Text = grosssaleamt.ToString();

            #endregion
        }



        columnTotal();




    }


    private void calcultionForaccode()
    {


        #region[purchase amnt]
        double purchase_rate = Convert.ToDouble("0" + txtPRate.Text);
        double netwt = Convert.ToDouble("0" + txtNetWt.Text);
        double purchase_amnt = Math.Round(((purchase_rate * netwt) / 100), 2);
        txtPAmt.Text = purchase_amnt.ToString();
        #endregion

        columnTotal();





    }

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

    private void columnTotal()
    {
        #region[total cal]
        double Stock_amnt = 0.00;
        double Purchase_Amnt_Total = 0.00;
        double Sale_Amnt_Total = 0.00;
        Int32 netqty = 0;
        Int32 qty = 0;
        if (grdDetail.Rows.Count > 0)
        {

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[39].Text != "D")
                {
                    Int32 CustCode = Convert.ToInt32(grdDetail.Rows[i].Cells[3].Text);
                    double Stock_amnt1 = Convert.ToDouble(grdDetail.Rows[i].Cells[13].Text);
                    qty = Convert.ToInt32(grdDetail.Rows[i].Cells[10].Text);
                    double Sale_Amnt_Total1 = Convert.ToDouble(grdDetail.Rows[i].Cells[15].Text);
                    if (CustCode == 2)
                    {
                        Stock_amnt += Stock_amnt1;
                    }
                    netqty += qty;
                    Purchase_Amnt_Total += Stock_amnt1;
                    Sale_Amnt_Total += Sale_Amnt_Total1;
                }
            }
        }
        lblqty.Text = netqty.ToString();
        lblStockValue.Text = Convert.ToString(Math.Round(Stock_amnt, 2)).ToString();
        lblPurchaseValue.Text = Convert.ToString(Math.Round(Purchase_Amnt_Total, 2)).ToString();
        lblSaleValue.Text = Convert.ToString(Math.Round(Sale_Amnt_Total, 2)).ToString();
        #endregion
    }
    #endregion

    #region [lblPurchaseValue_TextChanged]
    protected void lblPurchaseValue_TextChanged(object sender, EventArgs e)
    {
        searchString = lblPurchaseValue.Text;
        strTextBox = "lblPurchaseValue";
        csCalculations();
    }
    #endregion



    #region [txtCustCode_TextChanged]
    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCustCode.Text;
        strTextBox = "txtCustCode";
        csCalculations();
    }
    #endregion

    #region [txtItemCode_TextChanged]
    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItemCode.Text;
        strTextBox = "txtItemCode";
        csCalculations();
    }
    #endregion

    #region [txtPRate_TextChanged]
    protected void txtPRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPRate.Text;
        strTextBox = "txtPRate";
        csCalculations();
    }
    #endregion

    #region [txtOther_PM_TextChanged]
    protected void txtOther_PM_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOther_PM.Text;
        strTextBox = "txtOther_PM";
        csCalculations();
    }
    #endregion

    #region [txtNetWt_TextChanged]
    protected void txtNetWt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetWt.Text;
        strTextBox = "txtNetWt";
        csCalculations();
    }
    #endregion

    #region [TxtPanjar_Rate_TextChanged]
    protected void TxtPanjar_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = TxtPanjar_Rate.Text;
        strTextBox = "TxtPanjar_Rate";
        csCalculations();
    }
    #endregion

    #region [txtShub_Rate_TextChanged]
    protected void txtShub_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShub_Rate.Text;
        strTextBox = "txtShub_Rate";
        csCalculations();
    }
    #endregion


    #region [txtNetRate]
    protected void txtNetRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetRate.Text;
        strTextBox = "txtNetRate";
        csCalculations();
    }
    #endregion

    #region [txtgross_sale_amnt_TextChanged]
    protected void txtgross_sale_amnt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtgross_sale_amnt.Text;
        strTextBox = "txtgross_sale_amnt";
        csCalculations();
    }
    #endregion

    #region [txtQty]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
    }
    #endregion

    #region [txtOther_Per]
    protected void txtOther_Per_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOther_Per.Text;
        strTextBox = "txtOther_Per";
        csCalculations();
    }
    #endregion


    #region [txtSRate_TextChanged]
    protected void txtSRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSRate.Text;
        strTextBox = "txtSRate";
        csCalculations();
    }
    #endregion

    #region [txtPer_TextChanged]
    protected void txtPer_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPer.Text;
        strTextBox = "txtPer";
        csCalculations();
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

    #region [txtHamali_Code]
    protected void txtHamali_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali_Code.Text;
        strTextBox = "txtHamali_Code";
        csCalculations();
    }
    #endregion


    #region [txtHamali_amnt_TextChanged]
    protected void txtHamali_amnt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali_amnt.Text;
        strTextBox = "txtHamali_amnt";
        csCalculations();
    }
    #endregion


    #region [txtPacking_Code]
    protected void txtPacking_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali_amnt.Text;
        strTextBox = "txtPacking_Code";
        csCalculations();
    }
    #endregion

    #region [txtPacking_amnt]
    protected void txtPacking_amnt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPacking_amnt.Text;
        strTextBox = "txtPacking_amnt";
        csCalculations();
    }
    #endregion


    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty)// && strTextBox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
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

                //if (btntxtdoc_no.Text == "Choose No" || hdnfClosePopup.Value == "txtdoc_no")
                //{
                //    lblPopupHead.Text = "--Select DOC No--";
                //    string qry = "select doc_no,CONVERT(VARCHAR(10),doc_date,103) as doc_date,Supplier_Name,Item_Name,Qty as Awak_qty,Net_Wt as Net_Kg,Purc_Rate from NT_1_qryAwakNO where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                //        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or Supplier_Name like '%" + txtSearchText.Text + "%' or Customer_Name like '%" + txtSearchText.Text + "%') order by doc_date desc";
                //    this.showPopup(qry);
                //}
                //if (btntxtdoc_no.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,Customer_Name,Item_Name from " + qryCommon + " where TRAN_TYPE='JW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or Supplier_Name like '%" + txtSearchText.Text + "%' or Customer_Name like '%" + txtSearchText.Text + "%') order by doc_date desc";
                    this.showPopup(qry);
                }
            }
            //if (hdnfClosePopup.Value == "txtdoc_no")
            //{
            //    if (btntxtdoc_no.Text == "Change No")
            //    {
            //        pnlPopup.Style["display"] = "none";
            //        txtdoc_no.Text = string.Empty;
            //        txtdoc_no.Enabled = true;
            //        btnSave.Enabled = false;
            //        setFocusControl(txtdoc_no);
            //        hdnfClosePopup.Value = "Close";
            //    }
            //    else
            //    {
            //        lblPopupHead.Text = "--Select DOC No--";
            //        string qry = "select doc_no,doc_date,debitAcName from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Tran_Type='" + trntype + "'" +
            //        " and  (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%') group by doc_no,doc_date,debitAcName order by doc_no";
            //        this.showPopup(qry);
            //    }
            //}

            //if (hdnfClosePopup.Value == "txtSupplier")
            //{
            //    lblPopupHead.Text = "--Select Supplier Code--";
            //    string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
            //        " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' ) ORDER BY Ac_Name_E";
            //    this.showPopup(qry);
            //}


            if (hdnfClosePopup.Value == "txtCustCode")
            {
                lblPopupHead.Text = "--Select Customer Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
                    " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' ) ORDER BY Ac_Name_E";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtItemCode")
            {
                lblPopupHead.Text = "--Select Item--";
                //string qry = "select Item_Code,ItemName,[Net_Wt],balnceqty,detail_id from NT_1_qryJawakItemBalanceList where balnceqty<>0 and doc_no=" + txtAwakNo.Text + " and Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                //    " (ItemName like '%" + txtSearchText.Text + "%' or Item_Code like '%" + txtSearchText.Text + "%') ORDER BY ItemName";

                ////string qry1 = "select Item_Code,itemname,awakqty,detail_id,balnceqty,supplierName,Year_Code from NT_1_qryJABalance where doc_no='" + txtAwakNo.Text 
                ////    + "' and balnceqty!=0 and Company_Code="
                ////    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                ////        " and ( supplierName like '%" + txtSearchText.Text + "%'  or doc_no like'%" + txtSearchText.Text + "%') ORDER BY doc_no";

                ////string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                //this.showPopup(qry);

                string qry1 = "select Item_Code,ItemName,[Net_Wt],balnceqty,detail_id from NT_1_qryJABalance where balnceqty!=0 and doc_no=" + txtAwakNo.Text
                    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and ( ItemName like '%" + txtSearchText.Text + "%'  or Item_Code like'%" + txtSearchText.Text + "%') ORDER BY ItemName";

                this.showPopup(qry1);
            }

            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                lblPopupHead.Text = "--Select Brand--";
                string qry = "select Doc_No,Brand_Name from NT_1_BrandMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }

            //if (hdnfClosePopup.Value == "txtHamali_Code")
            //{
            //    lblPopupHead.Text = "--Select Hamali Code--";
            //    string qry = "select [System_Code],[System_Name_E],[System_Rate] from [NT_1_SystemMaster] where System_Type='H' and Company_Code="
            //        + Convert.ToInt32(Session["Company_Code"].ToString());
            //    //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            //    this.showPopup(qry);
            //}

            if (hdnfClosePopup.Value == "txtHamali_Code")
            {
                lblPopupHead.Text = "--Select Hamali Code--";
                string qry = "select [System_Code],[System_Name_E],[System_Rate] from " + systemMasterTable + " where  (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' or System_Rate like '%" + txtSearchText.Text + "%') and System_Type='H' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString());
                //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtPacking_Code")
            {
                lblPopupHead.Text = "--Select Packing Code--";
                string qry = "select [System_Code],[System_Name_E],[System_Rate] from " + systemMasterTable + " where (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' or System_Rate like '%" + txtSearchText.Text + "%') and System_Type='P' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString());
                //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }

            //if (hdnfClosePopup.Value == "txtPacking_Code")
            //{
            //    lblPopupHead.Text = "--Select Packing Code--";
            //    string qry = "select [System_Code],[System_Name_E],[System_Rate] from [NT_1_SystemMaster] where System_Type='P' and Company_Code="
            //        + Convert.ToInt32(Session["Company_Code"].ToString());
            //    //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            //    this.showPopup(qry);
            //}
            if (hdnfClosePopup.Value == "txtAwakNo")
            {
                lblPopupHead.Text = "--Select Packing Code--";
                if (btnSave.Text == "Update")
                {
                    string qry = "select doc_no,Item_Code,itemname,awakqty,balnceqty,detail_id,supplierName,Year_Code from NT_1_qryJABalance where Company_Code="
                        + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and ( supplierName like '%" + txtSearchText.Text + "%'  or doc_no like'%" + txtSearchText.Text + "%') ORDER BY doc_no";

                    this.showPopup(qry);
                }
                else
                {
                    string qry1 = "select doc_no,Item_Code,itemname,awakqty,balnceqty,detail_id,supplierName,Year_Code from NT_1_qryJABalance where balnceqty!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and ( supplierName like '%" + txtSearchText.Text + "%'  or doc_no like'%" + txtSearchText.Text + "%') ORDER BY doc_no";

                    this.showPopup(qry1);
                }

            }


        }
        catch
        {
        }
    }
    #endregion


    #region [lblSaleValue_TextChanged]
    protected void lblSaleValue_TextChanged(object sender, EventArgs e)
    {
        searchString = lblSaleValue.Text;
        strTextBox = "lblSaleValue";
        csCalculations();
    }
    #endregion

    #region [lblStockValue_TextChanged]
    protected void lblStockValue_TextChanged(object sender, EventArgs e)
    {
        searchString = lblStockValue.Text;
        strTextBox = "lblStockValue";
        csCalculations();
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        int i = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtItemCode")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Width = new Unit("50px");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Width = new Unit("50px");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;

            }
        }

        //        if (v != "txtnarration")
        //        {
        //            e.Row.Cells[0].Width = new Unit("60px");
        //            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //            e.Row.Cells[2].Width = new Unit("80px");
        //            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        //        }
        //        i++;
        //        foreach (TableCell cell in e.Row.Cells)
        //        {
        //            string s = cell.Text.ToString();
        //            if (cell.Text.Length > 35)
        //            {
        //                cell.Text = cell.Text.Substring(0, 35) + "..";
        //                cell.ToolTip = s;
        //            }
        //        }
        //    }
        //    if (e.Row.RowType != DataControlRowType.Pager)
        //    {
        //    if (v == "txtVoucherNo")
        //    {
        //        if (drpFilter.SelectedValue == "V")
        //        {
        //            e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(5);
        //            e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(20);
        //            e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[7].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[9].ControlStyle.Width = Unit.Percentage(10);

        //            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
        //            i++;
        //            foreach (TableCell cell in e.Row.Cells)
        //            {
        //                string s = cell.Text.ToString();
        //                if (cell.Text.Length > 30)
        //                {
        //                    cell.Text = cell.Text.Substring(0, 30) + "..";
        //                    cell.ToolTip = s;
        //                }
        //            }
        //        }
        //        if (drpFilter.SelectedValue == "T")
        //        {
        //            e.Row.Cells[0].ControlStyle.Width = new Unit("15px");
        //            e.Row.Cells[1].ControlStyle.Width = new Unit("10px");
        //            e.Row.Cells[2].ControlStyle.Width = new Unit("10px");
        //            e.Row.Cells[3].ControlStyle.Width = new Unit("300px");
        //            e.Row.Cells[4].ControlStyle.Width = new Unit("100px");
        //            e.Row.Cells[5].ControlStyle.Width = new Unit("300px");
        //            e.Row.Cells[6].ControlStyle.Width = new Unit("80px");
        //            e.Row.Cells[7].ControlStyle.Width = new Unit("80px");
        //            e.Row.Cells[8].ControlStyle.Width = new Unit("80px");
        //        }
        //        if (drpFilter.SelectedValue == "S")
        //        {
        //            e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(5);
        //            e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(20);
        //            e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[7].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(10);
        //            e.Row.Cells[9].ControlStyle.Width = Unit.Percentage(10);

        //            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
        //            i++;
        //            foreach (TableCell cell in e.Row.Cells)
        //            {
        //                string s = cell.Text.ToString();
        //                if (cell.Text.Length > 30)
        //                {
        //                    cell.Text = cell.Text.Substring(0, 30) + "..";
        //                    cell.ToolTip = s;
        //                }
        //            }
        //        }
        //    }
        //}
    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }


    protected void grdPopup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdPopup.SelectedIndex = -1;
            grdPopup.DataBind();
        }
        catch { }
    }

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "txtCustCode")
            {
                setFocusControl(txtCustCode);
            }

            if (hdnfClosePopup.Value == "txtItemCode")
            {
                setFocusControl(txtItemCode);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                setFocusControl(txtBrand_Code);
            }
            if (hdnfClosePopup.Value == "txtAwakNo")
            {
                setFocusControl(txtAwakNo);
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

    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                hdnf.Value = txtEditDoc_No.Text;
                pnlPopup.Style["display"] = "block";
                txtSearchText.Text = txtEditDoc_No.Text;
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry = "select *,CONVERT(varchar(10),doc_date,103) as DOC_DATE from " + qryCommon + " where doc_no='" + txtEditDoc_No.Text + "'and Year_Code='" + Convert.ToInt32(Session["year"].ToString())
                    + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and tran_type='" + trntype + "'"; ;
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);
                //pnlgrdDetail.Enabled = true;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    protected void txtAwakNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAwakNo.Text;
        strTextBox = "txtAwakNo";
        csCalculations();
    }

    #region [txtLevidetailsAmt_TextChanged]
    protected void txtLevidetailsAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLevidetailsAmt.Text;
        strTextBox = "txtLevidetailsAmt";
        csCalculations();
    }
    #endregion

    #region [txtInsuranceAmt_TextChanged]
    protected void txtInsuranceAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInsuranceAmt.Text;
        strTextBox = "txtInsuranceAmt";
        csCalculations();
    }
    #endregion

    protected void txtpochrate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtpochrate.Text;
        strTextBox = "txtpochrate";

        csCalculations();
    }
    protected void txtfrieghtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtfrieghtRate.Text;
        strTextBox = "txtfrieghtRate";

        csCalculations();
    }
}