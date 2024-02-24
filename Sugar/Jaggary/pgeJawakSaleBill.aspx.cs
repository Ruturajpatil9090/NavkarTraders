using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Sugar_pgeJawakSaleBill : System.Web.UI.Page
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

    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "JSaleHead";
            //tblDetails = tblPrefix + "AwakDetail";
            AccountMasterTable = tblPrefix + "AccountMaster";
            qryCommon = tblPrefix + "qryJsaleHead";
            qryAccountList = "qrymstaccountmaster";
            cityMasterTable = tblPrefix + "CityMaster";
            systemMasterTable = "NT_1_JaggarySystemMaster";
            voucherTable = tblPrefix + "Voucher";
            qryVoucherList = tblPrefix + "qryVoucherList";
            trntype = "JS";
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
                    if (Session["JS_No"] != null)
                    {
                        hdnf.Value = Session["JS_No"].ToString();
                        // trntype = Session["RP_TYPE"].ToString();
                        //   drpTrnType.SelectedValue = Session["RP_TYPE"].ToString();
                        qry = getDisplayQuery();
                        this.fetchRecord(qry);
                        this.enableDisableNavigateButtons();

                        Session["JS_No"] = null;
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
                obj.tableName = tblHead + " where Tran_Type='" + trntype + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                                    txtdoc_no.Enabled = true;
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
                drpCsCr.Enabled = false;
                calenderExtenderDate.Enabled = false;

                btntxtCustCode.Enabled = false;
                btntxtItemCode.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtEditDoc_No.Enabled = true;
                btnGST_RateCode.Enabled = false;
                //btnSave.Enabled = true;
                txtpartyMobno.Text = string.Empty;

                ViewState["currentTable"] = null;
                // grdDetail.DataSource = null;
                //  grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                //btnFirst.Enabled = true;
                //btnPrevious.Enabled = true;
                lblCustomer.Text = string.Empty;
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


                //ViewState["currentTable"] = null;
                #region set Business logic for save
                ViewState["currentTable"] = null;
                //grdDetail.DataSource = null;
                //grdDetail.DataBind();
                setFocusControl(drpCsCr);
                drpCsCr.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtpartyMobno.Text = string.Empty;
                btntxtCustCode.Enabled = true;
                btntxtItemCode.Enabled = true;

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtEditDoc_No.Enabled = false;
                btnGST_RateCode.Enabled = true;
                lblCustomerGSTStateCode.Text = string.Empty;
                txtGST_RateCode.Text = string.Empty;
                lblGST_RateCode.Text = string.Empty;
                //btnprint.Enabled = false;

                // btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                grdtotal.DataSource = null;
                grdtotal.DataBind();
                pnlgrdDetail.Enabled = true;


                lblCustomer.Text = string.Empty;
                lblItem.Text = string.Empty;

                txtbillamnt.Text = string.Empty;


                txtdoc_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

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

                btntxtCustCode.Enabled = false;
                btntxtItemCode.Enabled = false;
                btnGST_RateCode.Enabled = false;
                txtEditDoc_No.Enabled = true;
                //lblCashBank.Text = string.Empty;
                lblItem.Text = string.Empty;
                //  btnprint.Enabled = true;
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

                btntxtItemCode.Enabled = true;
                txtItemCode.Enabled = true;
                btntxtCustCode.Enabled = true;
                btnAdddetails.Enabled = true;
                btnGST_RateCode.Enabled = true;
                btnClosedetails.Enabled = true;
                txtEditDoc_No.Enabled = false;
                btnPrintSaleBill.Enabled = false;
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
            qry = "select max(doc_no) as doc_no from " + tblHead + " where Tran_Type='" + trntype + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
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
        query = "   select count(*) from " + tblHead + " where TRAN_TYPE='JS' and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                    "  and TRAN_TYPE='JS' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY doc_no asc  ";
                string strDoc_No = clsCommon.getString(query);
                if (strDoc_No != string.Empty)
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
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY doc_no asc  ";
                strDoc_No = clsCommon.getString(query);
                if (strDoc_No != string.Empty)
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where  Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") " +
                "   and TRAN_TYPE='JS' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                    " and TRAN_TYPE='JS' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
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
                    " and TRAN_TYPE='JS' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MAX(doc_no) from " + tblHead + " where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") " +
                "  and TRAN_TYPE='JS' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
        drpCsCr.SelectedIndex = 1;
        //txtTCSRate.Text = Session["Jaggary_TCSRate"].ToString();
        string dtt = "01/07/2021";
        string dt11 = txtdoc_date.Text;
        //DateTime TDSDate = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //DateTime docdate = Convert.ToDateTime(dt11);
        DateTime docdate = DateTime.Parse(dt11, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime TDSDate = DateTime.Parse(dtt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        if (docdate >= TDSDate)
        {
            txtTDSRate.Text = Session["Jaggary_SaleTDSRate"].ToString();
        }
        else
        {
            txtTCSRate.Text = Session["Jaggary_TCSRate"].ToString();
        }
        txtGST_RateCode.Text = Session["Jaggary_GSTCode"].ToString();
        string GSTRatename = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGST_RateCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
        lblGST_RateCode.Text = GSTRatename;
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


    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region [Validation Part]
        bool isValidated = true;
        string qry = "";
        int Tenkg = 0;
        int FiveKg = 0;
        int ThirtyKg = 0;
        int box = 0;
        for (int i = 0; i < grdtotal.Rows.Count; i++)
        {


            int itemcode = Convert.ToInt32(grdtotal.Rows[i].Cells[0].Text);
            string category = "";
            category = clsCommon.getString("Select category from NT_1_JaggarySystemMaster where System_Type = 'I' and System_Code= " + itemcode + "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (category == "X")
            {
                int value10 = Convert.ToInt32(grdtotal.Rows[i].Cells[2].Text);
                box += value10;
            }
            if (category == "5")
            {
                int value5 = Convert.ToInt32(grdtotal.Rows[i].Cells[2].Text);
                FiveKg += value5;
            }
            if (category == "1")
            {
                int value1 = Convert.ToInt32(grdtotal.Rows[i].Cells[2].Text);
                Tenkg += value1;
            }

            if (category == "3")
            {
                int value3 = Convert.ToInt32(grdtotal.Rows[i].Cells[2].Text);
                ThirtyKg += value3;
            }

        }


        if (txtdoc_no.Text != string.Empty)
        {
            if (ViewState["mode"] != null)
            {
                if (ViewState["mode"].ToString() == "I")
                {
                    string str = clsCommon.getString("select doc_no from " + tblHead + " where doc_no=" + txtdoc_no.Text + " and  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblMsg.Text = "Code " + txtdoc_no.Text + " already exist";

                        //this.getMaxCode();
                        hdnf.Value = txtdoc_no.Text;
                        isValidated = false;
                        return;
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

        if (txtdoc_no.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
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



        if (txtCustCode.Text != string.Empty)
        {
            string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (str != string.Empty && str != "0")
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                //setFocusControl(txtCustCode);
                return;
            }
        }
        else
        {
            isValidated = false;
            //setFocusControl(txtCustCode);
            return;
        }

        if (txtGST_RateCode.Text != string.Empty)
        {
            string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtGST_RateCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (str != string.Empty && str != "0")
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                //setFocusControl(txtCustCode);
                return;
            }
        }
        else
        {
            isValidated = false;
            //setFocusControl(txtCustCode);
            return;
        }

        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {
            string dateawak = grdDetail.Rows[i].Cells[12].Text;

            if (dateawak == txtdoc_date.Text)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert(' Detail Date and Sale Bill Date are not same!')", true);
                setFocusControl(txtdoc_date);
                return;
            }

        }


        #endregion

        #region -Head part declearation
        Int32 DOC_NO = txtdoc_no.Text != string.Empty ? Convert.ToInt32(txtdoc_no.Text) : 0;
        string CashCredit = drpCsCr.SelectedValue.ToString();
        //string deliver_type = drpDelivered.SelectedValue.ToString();
        string DOC_DATE = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        Int32 Customer_Code = txtCustCode.Text != string.Empty ? Convert.ToInt32(txtCustCode.Text) : 0;
        Int32 GST_RateCode = txtGST_RateCode.Text != string.Empty ? Convert.ToInt32(txtGST_RateCode.Text) : 0;
        double Shub_amount = txtShubAmount.Text != string.Empty ? Convert.ToDouble(txtShubAmount.Text) : 0;
        double kharjat = txtKhajrat.Text != string.Empty ? Convert.ToDouble(txtKhajrat.Text) : 0;
        double total = txtTotal.Text != string.Empty ? Convert.ToDouble(txtTotal.Text) : 0;
        double billamt = txtbillamnt.Text != string.Empty ? Convert.ToDouble(txtbillamnt.Text) : 0;
        double P_Pol_Amount = txtPpolamount.Text != string.Empty ? Convert.ToDouble(txtPpolamount.Text) : 0;
        double PostPhone = txtPostPhone.Text != string.Empty ? Convert.ToDouble(txtPostPhone.Text) : 0;
        string retValue = string.Empty;
        string strRev = string.Empty;
        int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
        //int Year_Code = Convert.ToInt32(Session["year"].ToString());
        int year_Code = Convert.ToInt32(Session["year"].ToString());

        int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
        string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
        double roundoff = txtRoundoff.Text != string.Empty ? Convert.ToDouble(txtRoundoff.Text) : 0;
        double TaxableAmount = txtTaxableAmount.Text != string.Empty ? Convert.ToDouble(txtTaxableAmount.Text) : 0;

        double TCS_Rate = txtTCSRate.Text != string.Empty ? Convert.ToDouble(txtTCSRate.Text) : 0.000;
        double TCS_Amt = txtTCSAmt.Text != string.Empty ? Convert.ToDouble(txtTCSAmt.Text) : 0.00;
        double TCS_Net_Payable = txtTCSNet_Payable.Text != string.Empty ? Convert.ToDouble(txtTCSNet_Payable.Text) : 0.00;

        double TDS_Rate = txtTDSRate.Text != string.Empty ? Convert.ToDouble(txtTDSRate.Text) : 0.000;
        double TDS_Amt = txtTDSAmt.Text != string.Empty ? Convert.ToDouble(txtTDSAmt.Text) : 0.00;
        double IGST_Rate = txtIGSTRate.Text != string.Empty ? Convert.ToDouble(txtIGSTRate.Text) : 0.00;
        double IGST_Amount = txtIGSTAmount.Text != string.Empty ? Convert.ToDouble(txtIGSTAmount.Text) : 0.00;
        double CGST_Rate = txtCGSTRate.Text != string.Empty ? Convert.ToDouble(txtCGSTRate.Text) : 0.00;
        double CGST_Amount = txtCGSTAmount.Text != string.Empty ? Convert.ToDouble(txtCGSTAmount.Text) : 0.00;
        double SGST_Rate = txtSGSTRate.Text != string.Empty ? Convert.ToDouble(txtSGSTRate.Text) : 0.00;
        double SGST_Amount = txtSGSTAmount.Text != string.Empty ? Convert.ToDouble(txtSGSTAmount.Text) : 0.00;
        string VEHICLE_NO = txtVehicleNo.Text.ToUpper();
        string EWAYBILL = txtEwaybill.Text;
        string einvoiceno = txteinvoiceno.Text;
        string ackno = txtackno.Text;
        #endregion-End of Head part declearation
        clsGledgerupdations gleder = new clsGledgerupdations();
        btnSave.Enabled = false;

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
                    obj.columnNm = "doc_no,Tran_Type,Cash_Credit,doc_date,Cust_Code,Total,Khajarat,Shub_Amount,P_Pol_Amount,BillAmt,Company_Code,Year_Code,Created_By,roundoff,PostPhone,TenKg,Fivekg,ThirtyKg,Box,TCS_Rate,TCS_Amt,TCS_Net_Payable,TDS_Rate,TDS_Amt,IGST_Rate,IGST_Amount,CGST_Rate,CGST_Amount,SGST_Rate,SGST_Amount,GST_RateCode,TaxableAmount,VEHICLE_NO,Eway_Bill_No,einvoiceno,ackno";

                    obj.values = "'" + DOC_NO + "','" + trntype + "','" + CashCredit + "','" + DOC_DATE + "','"
                    + Customer_Code + "','" + total + "','" + kharjat + "','" + Shub_amount + "','" + P_Pol_Amount + "','" + billamt + "','"
                        + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + year_Code + "','" + user + "','" + roundoff + "','" + PostPhone
                        + "','" + Tenkg + "','" + FiveKg + "','" + ThirtyKg + "','" + box + "','" + TCS_Rate + "','" + TCS_Amt + "','" + TCS_Net_Payable
                        + "','" + TDS_Rate + "','" + TDS_Amt + "','" + IGST_Rate + "','" + IGST_Amount + "','" + CGST_Rate + "','" + CGST_Amount
                        + "','" + SGST_Rate + "','" + SGST_Amount + "','" + GST_RateCode + "','" + TaxableAmount + "','" + VEHICLE_NO + "','" + EWAYBILL + "','" + einvoiceno + "','" + ackno + "'";

                    ds = obj.insertAccountMaster(ref strRev);
                    retValue = strRev;
                }
                else
                {
                    //Update Mode
                    obj.flag = 2;
                    obj.tableName = tblHead;
                    //obj.columnNm = "Cash_Credit='" + CashCredit + "',doc_date='" + DOC_DATE + "',Cust_Code='" + Customer_Code + "',Shub_Amount='" + Shub_amount + "',BillAmt='"
                    //    + billamt + "',Total='" + total + "',Khajarat='" + kharjat + "',P_Pol_Amount='" + P_Pol_Amount + "',roundoff='" + roundoff + "',PostPhone='" + PostPhone +
                    //    "',TenKg='" + Tenkg + "',Fivekg='" + FiveKg + "',ThirtyKg='" + ThirtyKg + "',Box='" + box
                    //    + "',TCS_Rate='" + TCS_Rate + "',TCS_Amt='" + TCS_Amt + "',TCS_Net_Payable='" + TCS_Net_Payable + "',TDS_Rate='" + TDS_Rate + "',TDS_Amt='" + TDS_Amt + "',IGST_Rate='" + IGST_Rate + "',IGST_Amount='" + IGST_Amount + "',CGST_Rate='" + CGST_Rate + "',CGST_Amount='" + CGST_Amount + "',SGST_Rate='" + SGST_Rate + "',SGST_Amount='" + SGST_Amount + "'  where Year_Code='" + year_Code + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                    //    + "' and  doc_no='" + DOC_NO + "' and Tran_Type='" + trntype + "'";

                    obj.columnNm = "Cash_Credit='" + CashCredit + "',doc_date='" + DOC_DATE + "',Cust_Code='" + Customer_Code + "',Shub_Amount='" + Shub_amount + "',BillAmt='"
                       + billamt + "',Total='" + total + "',Khajarat='" + kharjat + "',P_Pol_Amount='" + P_Pol_Amount + "',roundoff='" + roundoff + "',PostPhone='" + PostPhone +
                       "',TenKg='" + Tenkg + "',Fivekg='" + FiveKg + "',ThirtyKg='" + ThirtyKg + "',Box='" + box
                       + "',TCS_Rate='" + TCS_Rate + "',TCS_Amt='" + TCS_Amt + "',TCS_Net_Payable='" + TCS_Net_Payable + "',TDS_Rate='" + TDS_Rate + "',TDS_Amt='" + TDS_Amt + "',IGST_Rate='" + IGST_Rate + "',IGST_Amount='" + IGST_Amount + "',CGST_Rate='" + CGST_Rate + "',CGST_Amount='" + CGST_Amount + "',SGST_Rate='" + SGST_Rate + "',SGST_Amount='" + SGST_Amount + "',GST_RateCode='" + GST_RateCode + "',TaxableAmount='" + TaxableAmount + "',VEHICLE_NO='" + VEHICLE_NO + "',Eway_Bill_No='" + EWAYBILL + "',einvoiceno='" + einvoiceno + "',ackno='" + ackno + "' where Year_Code='" + year_Code + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + "' and  doc_no='" + DOC_NO + "' and Tran_Type='" + trntype + "'";

                    obj.values = "none";
                    ds = new DataSet();
                    ds = obj.insertAccountMaster(ref strRev);
                    retValue = strRev;
                }

                #region gledger effect

                DataSet dsDelete = new DataSet();
                dsDelete = clsDAL.SimpleQuery("delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + trntype + "' AND CASHCREDIT='" + CashCredit + "' and YEAR_CODE=" + year_Code + " AND DOC_NO=" + DOC_NO + " AND COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                int orderCode = 1;
                string Jagagry_Sale_Ac = clsCommon.getString("select Jagagry_Sale_Ac from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Company_Code);
                Int32 Jagagry_Sale_Acid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + Jagagry_Sale_Ac +
       "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                //double Jagagry_Sale_ac_Amt = total - (kharjat + Shub_amount);
                double Jagagry_Sale_ac_Amt = total;

                string KharajatAc = clsCommon.getString("select Kharajat_Ac from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Company_Code);
                Int32 KharajatAcid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + KharajatAc +
       "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                string shubamtAc = clsCommon.getString("select Shub_Ac from NT_1_JaggryCompanyParameters where   Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Company_Code);
                Int32 shubamtAcid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + shubamtAc +
       "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                string p_polAc = clsCommon.getString("select P_Pol_AC from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Company_Code);
                Int32 p_polAcid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + p_polAc +
        "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                string salebill = "Jaggry Sale Bill No:" + DOC_NO + " Qty:" + txtTotalqty.Text + " TCS Amt:" + TCS_Amt;

                string POSTAGE_AC = clsCommon.getString("select POSTAGE_AC from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Company_Code);
                Int32 POSTAGE_ACid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + POSTAGE_AC +
          "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                string RoundOff_AC = clsCommon.getString("select RoundOff from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Company_Code);
                Int32 RoundOff_ACid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + RoundOff_AC +
       "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));

                int Jaggary_Sale_TCS_Account = Convert.ToInt32(clsCommon.getString("select Jaggary_SaleTCSAc from NT_1_JaggryCompanyParameters where  Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                       + " and Company_Code=" + Company_Code));
                Int32 Jaggary_Sale_TCS_Accountid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + Jaggary_Sale_TCS_Account +
      "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                string TCSNarration = "Jaggary TCS " + lblCustomer.Text + " " + DOC_NO;


                int Jaggary_Sale_TDS_Account = Convert.ToInt32(Session["Jaggary_SaleTDSAc"].ToString());
                Int32 Jaggary_Sale_TDS_Accountid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + Jaggary_Sale_TDS_Account +
     "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                string TDSNarration = "Jaggary TDS " + lblCustomer.Text + " " + DOC_NO;

                int SaleCGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["SaleCGSTAc"].ToString());
                Int32 SaleCGSTAcid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + SaleCGSTAc +
    "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                int SaleSGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["SaleSGSTAc"].ToString());
                Int32 SaleSGSTAcid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + SaleSGSTAc +
    "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                int SaleIGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["SaleIGSTAc"].ToString());
                Int32 SaleIGSTAcid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + SaleIGSTAc +
    "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                Int32 cashid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=1" +
   "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                Int32 Customer_Codeid = Convert.ToInt32(clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + Customer_Code +
  "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                if (billamt != 0)
                {
                    if (drpCsCr.SelectedValue == "CS")
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','1','" + salebill + "','" + Math.Abs(billamt) + "','"
                            + Company_Code + "','" + year_Code + "','" + orderCode + "','D','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "','" + cashid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }
                    else
                    {
                        if (Customer_Code != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Customer_Code + "','"
                                + salebill + "','" + Math.Abs(billamt + TCS_Amt) + "','" + Company_Code + "','" + year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                                + "','D','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "','" + Customer_Codeid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;
                        }
                        if (Jagagry_Sale_ac_Amt != 0)
                        {

                            string narration = "Jaggary sale Bill Credit Party Name: " + lblCustomer.Text;
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            //obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                            //obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Jagagry_Sale_Ac + "','Credit','" + Math.Abs(Jagagry_Sale_ac_Amt) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','D','" + Jagagry_Sale_Ac + "'";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Jagagry_Sale_Ac + "','" + narration + "','"
                                + Math.Abs(Jagagry_Sale_ac_Amt) + "','" + Company_Code + "','" + year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','"
                                + orderCode + "','C','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "','" + Jagagry_Sale_Acid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;

                        }
                        if (kharjat != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE," +
                                "DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + KharajatAc + "','Jaggary sale Bill Credit','"
                                + Math.Abs(kharjat) + "','" + Company_Code + "','" + year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + "','C','"
                                + Customer_Code + "','" + trntype + "','" + DOC_NO + "','" + KharajatAcid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;
                        }
                        if (Shub_amount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + shubamtAc + "','Jaggary Sale Bill Credit','"
                                + Math.Abs(Shub_amount) + "','" + Company_Code + "','" + year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString())
                                + "','" + orderCode + "','C','" + shubamtAc + "','" + trntype + "','" + DOC_NO + "','" + shubamtAcid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;
                        }
                        if (P_Pol_Amount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + p_polAc + "','Jaggary Sale Bill Credit','"
                                + Math.Abs(P_Pol_Amount) + "','" + Company_Code + "','" + year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                                + "','C','" + p_polAc + "','" + trntype + "','" + DOC_NO + "','" + p_polAcid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;
                        }
                        if (PostPhone != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + POSTAGE_AC
                                + "','Jaggary Sale Bill Credit','" + Math.Abs(PostPhone) + "','" + Company_Code + "','" + year_Code
                                + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                                + "','C','" + POSTAGE_AC + "','" + trntype + "','" + DOC_NO + "','" + POSTAGE_ACid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;
                        }

                        if (roundoff != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + RoundOff_AC
                                + "','Jaggary Sale Bill Credit','" + Math.Abs(roundoff) + "','" + Company_Code + "','" + year_Code
                                + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                                + "','C','" + RoundOff_AC + "','" + trntype + "','" + DOC_NO + "','" + RoundOff_ACid + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                            orderCode = orderCode + 1;
                        }

                    }
                    //if (TCS_Amt != 0)
                    //{
                    //    obj.flag = 1;
                    //    obj.tableName = tblPrefix + "GLEDGER";
                    //    obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                    //    obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Customer_Code
                    //        + "','" + TCSNarration + "','" + Math.Abs(TCS_Amt) + "','" + Company_Code + "','" + year_Code
                    //        + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                    //        + "','D','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "'";
                    //    ds = obj.insertAccountMaster(ref strRev);
                    //    retValue = strRev;
                    //    orderCode = orderCode + 1;
                    //}
                    if (TCS_Amt != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Jaggary_Sale_TCS_Account
                            + "','" + TCSNarration + "','" + Math.Abs(TCS_Amt) + "','" + Company_Code + "','" + year_Code
                            + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                            + "','C','" + Jaggary_Sale_TCS_Account + "','" + trntype + "','" + DOC_NO + "','" + Jaggary_Sale_TCS_Accountid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }

                    if (TDS_Amt != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Customer_Code
                            + "','" + TDSNarration + "','" + Math.Abs(TDS_Amt) + "','" + Company_Code + "','" + year_Code
                            + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                            + "','C','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "','" + Customer_Codeid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }
                    if (TDS_Amt != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Jaggary_Sale_TDS_Account
                            + "','" + TDSNarration + "','" + Math.Abs(TDS_Amt) + "','" + Company_Code + "','" + year_Code
                            + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                            + "','D','" + Jaggary_Sale_TCS_Account + "','" + trntype + "','" + DOC_NO + "','" + Jaggary_Sale_TDS_Accountid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }
                    if (CGST_Amount != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + SaleCGSTAc
                            + "','" + salebill + "','" + Math.Abs(CGST_Amount) + "','" + Company_Code + "','" + year_Code
                            + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                            + "','C','" + SaleCGSTAc + "','" + trntype + "','" + DOC_NO + "','" + SaleCGSTAcid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }
                    if (SGST_Amount != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + SaleSGSTAc
                            + "','" + salebill + "','" + Math.Abs(SGST_Amount) + "','" + Company_Code + "','" + year_Code
                            + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                            + "','C','" + SaleSGSTAc + "','" + trntype + "','" + DOC_NO + "','" + SaleSGSTAcid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }
                    if (IGST_Amount != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO,ac";
                        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + SaleIGSTAc
                            + "','" + salebill + "','" + Math.Abs(IGST_Amount) + "','" + Company_Code + "','" + year_Code
                            + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode
                            + "','C','" + SaleIGSTAc + "','" + trntype + "','" + DOC_NO + "','" + SaleIGSTAcid + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                        orderCode = orderCode + 1;
                    }

                }

                #endregion gledger Effect

                //#region gledger effect

                //DataSet dsDelete = new DataSet();
                //dsDelete = clsDAL.SimpleQuery("delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + trntype + "' AND CASHCREDIT='" + CashCredit + "' and YEAR_CODE=" + year_Code + " AND DOC_NO=" + DOC_NO + " AND COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                //int orderCode = 1;
                //string Jagagry_Sale_Ac = clsCommon.getString("select Jagagry_Sale_Ac from NT_1_JaggryCompanyParameters where  Company_Code=" + Company_Code);
                //double Jagagry_Sale_ac_Amt = total - (kharjat + Shub_amount);
                //string KharajatAc = clsCommon.getString("select Kharajat_Ac from NT_1_JaggryCompanyParameters where  Company_Code=" + Company_Code);
                //string shubamtAc = clsCommon.getString("select Shub_Ac from NT_1_JaggryCompanyParameters where  Company_Code=" + Company_Code);
                //string p_polAc = clsCommon.getString("select P_Pol_AC from NT_1_JaggryCompanyParameters where  Company_Code=" + Company_Code);

                //if (billamt != 0)
                //{
                //    if (drpCsCr.SelectedValue == "CS")
                //    {
                //        obj.flag = 1;
                //        obj.tableName = tblPrefix + "GLEDGER";
                //        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                //        obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','1','Jawak Sale','" + Math.Abs(billamt) + "','" + Company_Code + "','" + year_Code + "','" + orderCode + "','D','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "'";
                //        ds = obj.insertAccountMaster(ref strRev);
                //        retValue = strRev;
                //    }
                //    else
                //    {
                //        if (Customer_Code != 0)
                //        {
                //            obj.flag = 1;
                //            obj.tableName = tblPrefix + "GLEDGER";
                //            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                //            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Customer_Code + "','Jawak Sale Debit','" + Math.Abs(total) + "','" + Company_Code + "','" + year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + "','D','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "'";
                //            ds = obj.insertAccountMaster(ref strRev);
                //            retValue = strRev;
                //        }
                //        if (Jagagry_Sale_ac_Amt != 0)
                //        {

                //            obj.flag = 1;
                //            obj.tableName = tblPrefix + "GLEDGER";
                //            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                //            //obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                //            //obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Jagagry_Sale_Ac + "','Credit','" + Math.Abs(Jagagry_Sale_ac_Amt) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','D','" + Jagagry_Sale_Ac + "'";
                //            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + Jagagry_Sale_Ac + "','Jawak Credit','" + Math.Abs(Jagagry_Sale_ac_Amt) + "','" + Company_Code + "','" + year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + "','C','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "'";
                //            ds = obj.insertAccountMaster(ref strRev);
                //            retValue = strRev;

                //        }
                //        if (kharjat != 0)
                //        {
                //            obj.flag = 1;
                //            obj.tableName = tblPrefix + "GLEDGER";
                //            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                //            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + KharajatAc + "','Jawak Credit','" + Math.Abs(kharjat) + "','" + Company_Code + "','" + year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + "','C','" + Customer_Code + "','" + trntype + "','" + DOC_NO + "'";
                //            ds = obj.insertAccountMaster(ref strRev);
                //            retValue = strRev;
                //        }
                //        if (Shub_amount != 0)
                //        {
                //            obj.flag = 1;
                //            obj.tableName = tblPrefix + "GLEDGER";
                //            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                //            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + shubamtAc + "','Jawak Credit','" + Math.Abs(Shub_amount) + "','" + Company_Code + "','" + year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + "','C','" + shubamtAc + "','" + trntype + "','" + DOC_NO + "'";
                //            ds = obj.insertAccountMaster(ref strRev);
                //            retValue = strRev;
                //        }
                //        if (P_Pol_Amount != 0)
                //        {
                //            obj.flag = 1;
                //            obj.tableName = tblPrefix + "GLEDGER";
                //            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                //            obj.values = "'JS','" + CashCredit + "','" + DOC_NO + "','" + DOC_DATE + "','" + p_polAc + "','Jawak Credit','" + Math.Abs(P_Pol_Amount) + "','" + Company_Code + "','" + year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + orderCode + "','C','" + p_polAc + "','" + trntype + "','" + DOC_NO + "'";
                //            ds = obj.insertAccountMaster(ref strRev);
                //            retValue = strRev;
                //        }


                //    }

                //}

                //#endregion gledger Effect

                //gleder.JaggrySaleGledgerEffect(trntype, DOC_NO, Convert.ToInt32(Session["Company_Code"].ToString()), Convert.ToInt32(Session["year"].ToString()));


                txtEditDoc_No.Text = string.Empty;

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
                //csCalculations();
                //calcultion();

            }
        }
        #endregion

        #region For disable bill no in sale


        #endregion

        txtEditDoc_No.Text = string.Empty;
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string user = Session["user"].ToString();
                string usercheck = "DJ";
                string usercheck2 = "demo";
                string usercheck3 = "pankaj";
                if (user == usercheck || user == usercheck2 || user == usercheck3)
                {

                    string date = txtdoc_date.Text;
                    string invoiceno = txtdoc_no.Text;
                    //string ewaybillno = txtEway_Bill_No.Text == string.Empty ? Convert.ToString(txtEway_Bill_No.Text) : " EWay Bill: " + txtEway_Bill_No.Text.Trim();
                    string amount = txtbillamnt.Text;
                    string companycode = Session["Company_Code"].ToString();

                    string msg = string.Empty;
                    string MsgforMail = string.Empty;

                    msg = "Record is Deleted from Jaggary SaleBill... Invoice No : " + invoiceno + " ,Invoice Date :" + date + " ,Bill Amount :" + amount + " ,Comp Code :" + companycode;
                    Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                    string code = "9923309666";//9923309666
                    string subject = "9923309666";
                    foreach (string singleSubject in subject.Split(','))
                    {
                        if (!dict.ContainsKey(singleSubject))
                        {
                            string partymob = string.Empty;
                            partymob = singleSubject;

                            if (!string.IsNullOrWhiteSpace(partymob))
                            {
                                string msgAPI = clsGV.msgAPI;
                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=RBCKOP&accusage=1";
                                clsCommon.apicall(URL);

                            }

                            //txtPartyMobno.Text = singleSubject;

                            // dict.Add(subject, new List<string> { code });
                        }
                        else
                        {
                            dict[subject].Add(code);
                        }
                    }

                    string currentDoc_No = txtdoc_no.Text;
                    string qry = "";
                    DataSet ds = new DataSet();
                    qry = "delete from " + GLedgerTable + " where TRAN_TYPE='" + trntype + "' and DOC_NO=" + currentDoc_No +
                        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and YEAR_CODE=" + Convert.ToInt32(Session["year"].ToString());
                    ds = clsDAL.SimpleQuery(qry);

                    qry = "delete from " + tblHead + " where doc_no=" + currentDoc_No + " and Tran_Type='" + trntype +
                        "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                    ds = clsDAL.SimpleQuery(qry);

                    string query = "SELECT top 1 [doc_no] from " + tblHead + "  where doc_no>" + Convert.ToInt32(currentDoc_No) +
                            "  and Tran_Type='" + trntype + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY doc_no asc";

                    hdnf.Value = clsCommon.getString(query);

                    if (hdnf.Value == string.Empty)
                    {
                        query = "SELECT top 1 [doc_no] from " + tblHead + "  where doc_no<" + Convert.ToInt32(currentDoc_No) +
                             " and Tran_Type='" + trntype + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY doc_no desc  ";
                        hdnf.Value = clsCommon.getString(query);
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
                        //new code
                        clsButtonNavigation.enableDisable("N");         //No record exist  Last record deleted.
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    // string PartyName = lblmi.Text;


                    this.enableDisableNavigateButtons();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('You are not Authorized Person to Delete Record!!!!');", true);
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
        string str = clsCommon.getString("select count(doc_no) from " + tblHead + " where Year_Code="
                + Convert.ToInt32(Session["year"].ToString()) + " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
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
    }
    #endregion

    #region [btnprint_Click]
    protected void btnprint_Click(object sender, EventArgs e)
    {

    }
    #endregion


    //protected void btndownloadsellbill_Click(object sender, EventArgs e)
    //{
    //    //string fromDT = "";
    //    //string toDT = "";
    //    int companycode = Convert.ToInt32(Session["Company_Code"].ToString());
    //    int yearcode = Convert.ToInt32(Session["year"].ToString());
    //    string salebillno = txtDOC_NO.Text;
    //    // fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
    //    //toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
    //    //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:sb('" + fromDT + "','" + toDT + "')", true);
    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:DSB('" + salebillno + "','" + companycode + "','" + yearcode + "')", true);

    //}

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
                        drpCsCr.SelectedValue = dt.Rows[0]["Cash_Credit"].ToString().Trim();
                        txtdoc_no.Text = dt.Rows[0]["doc_no"].ToString();
                        txtdoc_date.Text = dt.Rows[0]["DOC_DATE"].ToString();
                        // txtdoc_date.Text = DateTime.Parse(dt.Rows[0]["DOC_DATE"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");


                        txtCustCode.Text = dt.Rows[0]["Cust_Code"].ToString();
                        string str = clsCommon.getString("select Ac_Name_E,GSTStateCode from NT_1_AccountMaster where Ac_Code=" + txtCustCode.Text
                                       + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //Double s1 = Convert.ToDouble(sh
                        lblCustomer.Text = str;
                        string GSTStateCode = clsCommon.getString("select GSTStateCode from NT_1_AccountMaster where Ac_Code=" + txtCustCode.Text
                                      + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //Double s1 = Convert.ToDouble(sh
                        lblCustomerGSTStateCode.Text = GSTStateCode;
                        txtTotal.Text = dt.Rows[0]["Total"].ToString();
                        txtKhajrat.Text = dt.Rows[0]["Khajarat"].ToString();
                        txtShubAmount.Text = dt.Rows[0]["Shub_Amount"].ToString();
                        txtPpolamount.Text = dt.Rows[0]["P_Pol_Amount"].ToString();
                        txtbillamnt.Text = dt.Rows[0]["BillAmt"].ToString();
                        txtRoundoff.Text = dt.Rows[0]["roundoff"].ToString();
                        txtPostPhone.Text = dt.Rows[0]["PostPhone"].ToString();
                        txtTaxableAmount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtpartyMobno.Text = clsCommon.getString("select Mobile_No from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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

                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        txtTCSNet_Payable.Text = dt.Rows[0]["TCS_Net_Payable"].ToString();

                        txtTDSRate.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["IGST_Rate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["IGST_Amount"].ToString();
                        txtCGSTRate.Text = dt.Rows[0]["CGST_Rate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["CGST_Amount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["SGST_Rate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["SGST_Amount"].ToString();
                        txtGST_RateCode.Text = dt.Rows[0]["GST_RateCode"].ToString();
                        string str1 = clsCommon.getString("select GST_Name from NT_1_GSTRateMaster where Doc_no=" + txtGST_RateCode.Text
                                       + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        lblGST_RateCode.Text = str1;
                        txtVehicleNo.Text = dt.Rows[0]["VEHICLE_NO"].ToString();
                        txtEwaybill.Text = dt.Rows[0]["Eway_Bill_No"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        if (txteinvoiceno.Text != string.Empty && txtackno.Text != string.Empty)
                        {
                            btnGenEinvoice.Enabled = false;
                        }
                        else
                        {
                            btnGenEinvoice.Enabled = true;
                        }
                        if (txtEwaybill.Text != string.Empty)
                        {
                            btnGentare_EWayBill.Enabled = false;
                        }
                        else
                        {
                            btnGentare_EWayBill.Enabled = true;
                        }
                        recordExist = true;
                        if (txtdoc_no.Text != string.Empty)
                        {
                            #region  Details
                            //DataSet ds = new DataSet();
                            // DataTable dt = new DataTable();
                            qry = "select TRAN_TYPE as Type,doc_no,Item_Code,ItemName,Qty,Net_Wt,Gross_Rate as Rate,Gross_Amnt as Sale_value,Bill_No," +
                                "Brand_Name,convert(varchar(10),DOC_DATE,103) as DOC_DATE from NT_1_awakSale where  Bill_No=" + txtdoc_no.Text + " and Year_Code="
                                + Convert.ToInt32(Session["year"].ToString()) + " and Cust_Code!=2 and Company_Code="
                                + Convert.ToInt32(Session["Company_Code"].ToString()) + " UNION all select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty," +
                                "Net_Wt as netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value,Bill_No,Brand_Name,convert(varchar(10),DOC_DATE,103) as DOC_DATE from qryJawakBalance where Bill_No="
                                + txtdoc_no.Text + " and Cust_Code!=2 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                            // qry = "select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty,netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value from qryAwakBalance UNION select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty,netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value from qryJawakBalance where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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

                            Int32 totalqty = 0;
                            totalqty = Convert.ToInt32(dt.Compute("SUM(Qty)", string.Empty));
                            txtTotalqty.Text = totalqty.ToString();
                            double shubamount1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(Shub_Amnt),0) from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                                       + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                            //Double s1 = Convert.ToDouble(shubamount1);
                            double shubamount2 = Convert.ToDouble(clsCommon.getString("select isnull(sum(Shub_Amnt),0) from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                                       + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                            double shubamount = shubamount1 + shubamount2;

                            txtShubAmount.Text = "0.00"; // shubamount.ToString();
                            double totalamt;
                            totalamt = Convert.ToDouble(dt.Compute("SUM(Sale_value)", string.Empty));

                            txtTotal.Text = totalamt.ToString();

                            double kharjat1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(kharajat),0) from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                                        + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                            double kharjat2 = Convert.ToDouble(clsCommon.getString("Select isnull(sum(kharajat),0) from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                                       + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                            double kharjat = Convert.ToDouble(kharjat1 + kharjat2);
                            txtKhajrat.Text = kharjat.ToString();

                            double Panjar_Amnt1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(Panjar_Amnt),0) from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                          + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));

                            double Panjar_Amnt2 = Convert.ToDouble(clsCommon.getString("Select isnull(sum(Panjar_Amnt),0) from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                                       + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                            double Panjar_Amnt = Convert.ToDouble(Panjar_Amnt1 + Panjar_Amnt2);
                            txtPpolamount.Text = Panjar_Amnt.ToString();

                            //  txtbillamnt.Text = txtKhajrat.Text + txtShubAmount.Text + txtTotal.Text;
                            btnDelete.Enabled = true;
                        }
                        //lblMsg.Text = "";

                        // #region  Details
                        // //qry = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and TRAN_TYPE='AW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        // //qry = "select detail_id as ID,Cust_Code,Customer_Name,System_Code as Item_Code,ItemName as Item_Name," +
                        // //"Brand_Code,Brand_Name,Hamali_Code,Hamali_name,Hamali_Amnt,Packing_Code,Packing_Name,Packing_Amnt,BNo as B_No,QTY," +
                        // //"NETKG,purcrate as P_Rate,purcvalue as P_Amt,salerate as S_Rate,commrate,CommAmnt,Sale_Amt as S_Amt," +
                        // //"netslrate as Net_Rate,awaksrno as Sr,Other_Per,Other_amnt"
                        // //          + ",Other_PM,netslamnt " +
                        // // " from " + qryCommon + " where doc_no="
                        // // + hdnf.Value + " and TRAN_TYPE='AW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                        // qry = "select detail_id as ID,Cust_Code,Customer_Name as Cust_Name,[Item_Code],[Item_Name]," +
                        //"[Brand_Code],[Brand_Name],[Bill_No],[Qty],[Net_Wt],[Purc_Rate],[Purc_Amnt],[Sale_Rate],[Sale_Amnt],[Comm_Rate]," +
                        //"[Comm_Amnt],[Hamali_Code],[Hamali_Name],[Hamali_Amnt],[Packing_Code],[Packing_Name],[Packing_Amnt]," +
                        //"[Other_Rate],[Other_Amnt],[Other_PM],[Gross_Rate]"
                        //          + ",[Gross_Amnt],[Sr],[Shub_Rate],[Shub_Amnt],[Panjar_Rate],[Panjar_Amnt] " +
                        // " from " + qryCommon + " where doc_no="
                        // + hdnf.Value + " and TRAN_TYPE='JW' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                        // ds = clsDAL.SimpleQuery(qry);
                        // if (ds != null)
                        // {
                        //     if (ds.Tables.Count > 0)
                        //     {
                        //         dt = ds.Tables[0];
                        //         if (dt.Rows.Count > 0)
                        //         {

                        //             dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                        //             dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                        //             for (int i = 0; i < dt.Rows.Count; i++)
                        //             {
                        //                 dt.Rows[i]["rowAction"] = "N";
                        //                 dt.Rows[i]["SrNo"] = i + 1;
                        //             }
                        //             grdDetail.DataSource = dt;
                        //             grdDetail.DataBind();
                        //             ViewState["currentTable"] = dt;
                        //         }
                        //         else
                        //         {
                        //             grdDetail.DataSource = null;
                        //             grdDetail.DataBind();
                        //             ViewState["currentTable"] = null;
                        //         }
                        //     }
                        //     else
                        //     {
                        //         grdDetail.DataSource = null;
                        //         grdDetail.DataBind();
                        //         ViewState["currentTable"] = null;
                        //     }



                        // }
                        // else
                        // {
                        //     grdDetail.DataSource = null;
                        //     grdDetail.DataBind();
                        //     ViewState["currentTable"] = null;
                        // }
                        // #endregion
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            //columnTotal();
            // csCalculations();
            //columnTotal();
            grdtotal1();
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
            string qryDisplay = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and Tran_Type='JS' and Year_Code="
                + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
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
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["SrNo"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["SrNo"].ToString());
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
                        dr["SrNo"] = rowIndex;
                        //dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Doc_No from " + tblDetails + " where Doc_No=" + txtdoc_no.Text + "  And Tran_Type=" + trntype + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code="
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
                    dt.Columns.Add((new DataColumn("SrNo", typeof(Int32))));
                    #region [Write here columns]


                    dt.Columns.Add((new DataColumn("Type", typeof(double))));
                    dt.Columns.Add((new DataColumn("Awak_No", typeof(double))));



                    dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));

                    dt.Columns.Add((new DataColumn("Qty", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));

                    dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Sale_Value", typeof(double))));



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
                dt.Columns.Add((new DataColumn("SrNo", typeof(Int32))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Type", typeof(double))));
                dt.Columns.Add((new DataColumn("Awak_No", typeof(double))));


                dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));

                dt.Columns.Add((new DataColumn("Qty", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));

                dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Sale_Value", typeof(double))));


                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]

            dr["Type"] = txtType.Text;
            dr["Awak_No"] = txtAwakno.Text;





            dr["Item_Code"] = txtItemCode.Text;
            dr["Item_Name"] = lblItem.Text;

            dr["Qty"] = txtItemCode.Text;
            dr["Net_Wt"] = lblItem.Text;
            dr["Rate"] = txtRate.Text;
            dr["Sale_Value"] = txtSaleValue.Text;

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
            txtType.Text = string.Empty;
            txtAwakno.Text = string.Empty;
            txtCustCode.Text = string.Empty;
            lblCustomer.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            lblItem.Text = string.Empty;

            txtQty.Text = string.Empty;
            txtNetWt.Text = string.Empty;
            txtSaleValue.Text = string.Empty;
            txtRate.Text = string.Empty;
            csCalculations();
            //columnTotal();
            setFocusControl(txtCustCode);
            //columnTotal();
            btnSave.Enabled = true;
            //pnlPopupDetails.Style["display"] = "none";

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
        txtCustCode.Text = string.Empty;
        lblCustomer.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        lblItem.Text = string.Empty;

        txtQty.Text = string.Empty;
        txtNetWt.Text = string.Empty;

        btnAdddetails.Text = "ADD";
        setFocusControl(txtCustCode);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gr)
    {
        try
        {


            txtType.Text = Server.HtmlDecode(gr.Cells[2].Text);
            txtAwakno.Text = Server.HtmlDecode(gr.Cells[3].Text);
            txtItemCode.Text = Server.HtmlDecode(gr.Cells[4].Text);
            lblItem.Text = Server.HtmlDecode(gr.Cells[5].Text);
            txtQty.Text = Server.HtmlDecode(gr.Cells[6].Text);
            txtNetWt.Text = Server.HtmlDecode(gr.Cells[7].Text);
            txtRate.Text = Server.HtmlDecode(gr.Cells[8].Text);
            txtSaleValue.Text = Server.HtmlDecode(gr.Cells[9].Text);
            txtAwakno.Text = Server.HtmlDecode(gr.Cells[10].Text);

            lblNo.Text = Server.HtmlDecode(gr.Cells[11].Text);
            lblID.Text = Server.HtmlDecode(gr.Cells[12].Text);

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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["SrNo"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where detail_id=" + ID + " and doc_no=" + txtdoc_no.Text
                    + " and TRAN_TYPE='JW' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[11].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[11].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[34].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[34].Text = "A";
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

            e.Row.Cells[0].ControlStyle.Width = new Unit("0px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("200px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("30px");



            //e.Row.Cells[0].Style["overflow"] = "hidden";
            //e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[2].Style["overflow"] = "hidden";
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[11].Style["overflow"] = "hidden";
            e.Row.Cells[6].Style["overflow"] = "hidden";
            e.Row.Cells[7].Style["overflow"] = "hidden";
            e.Row.Cells[8].Style["overflow"] = "hidden";
            e.Row.Cells[9].Style["overflow"] = "hidden";
            e.Row.Cells[10].Style["overflow"] = "hidden";
            e.Row.Cells[11].Style["overflow"] = "hidden";
            e.Row.Cells[12].Style["overflow"] = "hidden";
            e.Row.Cells[13].Style["overflow"] = "hidden";
            e.Row.Cells[124].Style["overflow"] = "hidden";
            int i = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
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

                if (e.Row.Cells[5].Text.Length > 27)
                {
                    e.Row.Cells[5].Style["overflow"] = "hidden";
                    string s = e.Row.Cells[5].Text.ToString();
                    //e.Row.Cells[11].Text = e.Row.Cells[11].Text.Substring(0, 27) + "..";
                    e.Row.Cells[5].ToolTip = s;
                }
                if (e.Row.Cells[11].Text.Length > 27)
                {
                    e.Row.Cells[11].Style["overflow"] = "hidden";
                    string s = e.Row.Cells[11].Text.ToString();
                    //e.Row.Cells[11].Text = e.Row.Cells[11].Text.Substring(0, 27) + "..";
                    e.Row.Cells[11].ToolTip = s;
                }
            }
            //e.Row.Cells[2].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[12].Visible = true;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;

        }
        catch
        {
        }
    }
    #endregion


    #region [grdtotal_RowDataBound]
    protected void grdtotal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            e.Row.Cells[0].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("100px");





            e.Row.Cells[0].Style["overflow"] = "hidden";
            e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[2].Style["overflow"] = "hidden";

            int i = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;


                if (e.Row.Cells[1].Text.Length > 27)
                {
                    e.Row.Cells[1].Style["overflow"] = "hidden";
                    string s = e.Row.Cells[5].Text.ToString();
                    //e.Row.Cells[11].Text = e.Row.Cells[11].Text.Substring(0, 27) + "..";
                    e.Row.Cells[1].ToolTip = s;
                }

            }
            //e.Row.Cells[2].Visible = false;

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
                        if (grdDetail.Rows[rowindex].Cells[11].Text != "D" && grdDetail.Rows[rowindex].Cells[11].Text != "R")
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

    #region [btntxtCustomer_Click]
    protected void btntxtCustCode_Click(object sender, EventArgs e)
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

    #region [btnGST_RateCode_Click]
    protected void btnGST_RateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGST_RateCode";
            btnSearch_Click(sender, e);
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


            #region[doc date]
            if (strTextBox == "txtdoc_date")
            {
                if (txtdoc_date.Text != string.Empty)
                {
                    string dt = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDate(dt) == true)
                    {
                        setFocusControl(txtCustCode);
                    }
                    else
                    {
                        txtdoc_date.Text = string.Empty;
                        setFocusControl(txtCustCode);
                    }
                }
                else
                {
                    setFocusControl(txtCustCode);
                }
            }
            #endregion


            if (strTextBox == "txtdoc_no")
            {
                #region  Details
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                // string str1 = clsCommon.getString("select distinct Cust_Code from NT_1_qryAwakNO where Bill_No=" + txtdoc_no.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                // string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + str1
                // + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //txtCustCode.Text = str1;
                //lblCustomer.Text = str;
                qry = "select TRAN_TYPE as Type,doc_no,Item_Code,ItemName,Qty,Net_Wt,Gross_Rate as Rate,Gross_Amnt as Sale_value,Bill_No,Brand_Name,convert(varchar(10),DOC_DATE,103) as DOC_DATE " +
                    "from NT_1_awakSale where  Bill_No=" + txtdoc_no.Text + " and Cust_Code!=2  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " UNION all select TRAN_TYPE as Type,doc_no,Item_Code,ItemName,Qty,Net_Wt,Gross_Rate as Rate,Gross_Amnt as Sale_value," +
                    "Bill_No,Brand_Name,convert(varchar(10),DOC_DATE,103) as DOC_DATE from qryJawakBalance where Bill_No=" + txtdoc_no.Text + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Cust_Code!=2 and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString());
                // qry = "select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty,netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value from qryAwakBalance UNION select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty,netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value from qryJawakBalance where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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

                Int32 totalqty = 0;
                totalqty = Convert.ToInt32(dt.Compute("SUM(Qty)", string.Empty));
                txtTotalqty.Text = totalqty.ToString();

                double shubamount1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(Shub_Amnt),0) from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                //Double s1 = Convert.ToDouble(shubamount1);
                double shubamount2 = Convert.ToDouble(clsCommon.getString("select isnull(sum(Shub_Amnt),0) from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                double shubamount = shubamount1 + shubamount2;
                txtShubAmount.Text = shubamount.ToString();
                txtShubAmount.Text = "0.00";
                

                double totalamt;
                totalamt = Convert.ToDouble(dt.Compute("SUM(Sale_value)", string.Empty));

                txtTotal.Text = totalamt.ToString();

                double kharjat1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(kharajat),0) from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                double kharjat2 = Convert.ToDouble(clsCommon.getString("Select isnull(sum(kharajat),0) from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                double kharjat = Convert.ToDouble(kharjat1 + kharjat2);
                txtKhajrat.Text = kharjat.ToString();


                double Panjar_Amnt1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(Panjar_Amnt),0) from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                          + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));

                double Panjar_Amnt2 = Convert.ToDouble(clsCommon.getString("Select isnull(sum(Panjar_Amnt),0) from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                double Panjar_Amnt = Convert.ToDouble(Panjar_Amnt1 + Panjar_Amnt2);
                txtPpolamount.Text = Panjar_Amnt.ToString();

                string dateawak = clsCommon.getString("select convert(varchar(10),DOC_DATE,103) as DOC_DATE  from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string datejawak = clsCommon.getString("select convert(varchar(10),DOC_DATE,103) as DOC_DATE  from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string awakparty = clsCommon.getString("select Cust_Code from NT_1_awakSale where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string jawakparty = clsCommon.getString("select Cust_Code from qryJawakBalance where Bill_No=" + txtdoc_no.Text
                           + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string awakpartyname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + awakparty
                + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string jawakpartyname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + jawakparty
              + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                if (awakparty != "0")
                {
                    txtCustCode.Text = awakparty;
                    lblCustomer.Text = awakpartyname;
                    string GSTStateCode = clsCommon.getString("select GSTStateCode from NT_1_AccountMaster where Ac_Code=" + txtCustCode.Text
                                     + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    lblCustomerGSTStateCode.Text = GSTStateCode;

                }
                else
                {
                    txtCustCode.Text = jawakparty;
                    lblCustomer.Text = jawakpartyname;
                    string GSTStateCode = clsCommon.getString("select GSTStateCode from NT_1_AccountMaster where Ac_Code=" + txtCustCode.Text
                                    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    lblCustomerGSTStateCode.Text = GSTStateCode;
                }

                if (dateawak != "0")
                {
                    txtdoc_date.Text = dateawak;
                }
                else
                {
                    txtdoc_date.Text = datejawak;
                }




                //Double s1 = Convert.ToDouble(shubamount1);
                //  txtbillamnt.Text = txtKhajrat.Text + txtShubAmount.Text + txtTotal.Text;
                btnDelete.Enabled = true;
                setFocusControl(txtdoc_date);
            }


            if (strTextBox == "txtKhajrat")
            {
                setFocusControl(txtShubAmount);
            }

            if (strTextBox == "txtShubAmount")
            {
                setFocusControl(txtPostPhone);
            }

            if (strTextBox == "txtPostPhone")
            {
                setFocusControl(txtTaxableAmount);
            }

            if (strTextBox == "txtRoundoff")
            {
                setFocusControl(txtbillamnt);
            }
            if (strTextBox == "txtTCSRate")
            {
                setFocusControl(txtTCSNet_Payable);
            }
            if (strTextBox == "txtTCSNet_Payable")
            {
                setFocusControl(btnSave);
            }


            #region[cust cocde]
            if (strTextBox == "txtCustCode")
            {
                if (txtCustCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtCustCode.Text);
                    if (a == false)
                    {
                        btntxtCustCode_Click(this, new EventArgs());
                    }
                    else
                    {

                        string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string comm_rate = clsCommon.getString("select [Commission] from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string GSTStateCode = clsCommon.getString("select GSTStateCode from NT_1_AccountMaster where Ac_Code=" + txtCustCode.Text
                                    + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtpartyMobno.Text = clsCommon.getString("select Mobile_No from " + AccountMasterTable + " where Ac_Code=" + txtCustCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (str != string.Empty && str != "0")
                        {

                            lblCustomer.Text = str;
                            lblCustomerGSTStateCode.Text = GSTStateCode;
                            // txtPer.Text = comm_rate;
                            setFocusControl(txtItemCode);
                        }
                        else
                        {
                            lblCustomer.Text = string.Empty;
                            lblCustomerGSTStateCode.Text = string.Empty;
                            //txtPer.Text = string.Empty;
                            txtCustCode.Text = string.Empty;
                            setFocusControl(txtCustCode);
                        }
                    }
                }
                else
                {
                    lblCustomer.Text = string.Empty;
                    lblGST_RateCode.Text = string.Empty;
                    txtCustCode.Text = string.Empty;
                    setFocusControl(txtCustCode);

                }


            }
            #endregion


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

                        string str = clsCommon.getString("select System_Name_E from " + systemMasterTable + " where System_Type='I' and  System_Code="
                         + txtItemCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (str != string.Empty && str != "0")
                        {
                            lblItem.Text = str;

                            //setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            lblItem.Text = string.Empty;
                            txtItemCode.Text = string.Empty;
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

            #region[GST RateCode]
            if (strTextBox == "txtGST_RateCode")
            {
                if (txtGST_RateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGST_RateCode.Text);
                    if (a == false)
                    {
                        btnGST_RateCode_Click(this, new EventArgs());
                    }
                    else
                    {

                        string str = clsCommon.getString("select GST_Name from NT_1_GSTRateMaster where  Doc_no="
                         + txtGST_RateCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (str != string.Empty && str != "0")
                        {
                            lblGST_RateCode.Text = str;

                            //setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            lblGST_RateCode.Text = string.Empty;
                            txtGST_RateCode.Text = string.Empty;
                            setFocusControl(txtGST_RateCode);
                        }
                    }
                }
                else
                {
                    lblGST_RateCode.Text = string.Empty;
                    txtGST_RateCode.Text = string.Empty;
                    setFocusControl(txtGST_RateCode);

                }


            }
            #endregion

            calcultion();
        }
        catch
        {
        }
    }
    #endregion
    //private void columnTotal()
    //{
    //    #region[total cal]
    //    Int32 jaggery30kg = 0;
    //    Int32 jaggery10kg = 0;
    //    Int32 jaggery5kg = 0;
    //    Int32 jaggerybox = 0;
    //    Int32 jaggeryboxg5kg = 0;

    //    Int32 netqty = 0;
    //    Int32 qty = 0;
    //    if (grdDetail.Rows.Count > 0)
    //    {

    //        for (int i = 0; i < grdDetail.Rows.Count; i++)
    //        {

    //            Int32 itemcode = Convert.ToInt32(grdDetail.Rows[i].Cells[4].Text);
    //            if (itemcode == 1)
    //            {
    //                Int32 qty30kg = Convert.ToInt32(grdDetail.Rows[i].Cells[6].Text);
    //                jaggery30kg += qty30kg;
    //            }
    //            if (itemcode == 2)
    //            {
    //                Int32 qty10kg = Convert.ToInt32(grdDetail.Rows[i].Cells[6].Text);
    //                jaggery10kg += qty10kg;
    //            }
    //            if (itemcode == 3)
    //            {
    //                Int32 qty5kg = Convert.ToInt32(grdDetail.Rows[i].Cells[6].Text);
    //                jaggery5kg += qty5kg;
    //            }
    //            if (itemcode == 4)
    //            {
    //                Int32 qtybox = Convert.ToInt32(grdDetail.Rows[i].Cells[6].Text);
    //                jaggerybox += qtybox;
    //            }
    //            if (itemcode == 7)
    //            {
    //                Int32 qtybox5kg = Convert.ToInt32(grdDetail.Rows[i].Cells[6].Text);
    //                jaggeryboxg5kg += qtybox5kg;
    //            }

    //            //double Stock_amnt1 = Convert.ToDouble(grdDetail.Rows[i].Cells[13].Text);
    //            //qty = Convert.ToInt32(grdDetail.Rows[i].Cells[10].Text);
    //            //double Sale_Amnt_Total1 = Convert.ToDouble(grdDetail.Rows[i].Cells[15].Text);

    //            //netqty += qty;
    //            //Purchase_Amnt_Total += Stock_amnt1;
    //            //Sale_Amnt_Total += Sale_Amnt_Total1;
    //        }
    //    }
    //    //lblqty.Text = netqty.ToString();
    //    lbl5kgs.Text = Convert.ToString(jaggery5kg);
    //    lbl30kgs.Text = Convert.ToString(jaggery30kg);
    //    lbl10kgs.Text = Convert.ToString(jaggery10kg);
    //    lblbox5kg.Text = Convert.ToString(jaggeryboxg5kg);
    //    lblbox.Text = Convert.ToString(jaggerybox);
    //    //lblStockValue.Text = Convert.ToString(Math.Round(Stock_amnt, 2)).ToString();
    //    //lblPurchaseValue.Text = Convert.ToString(Math.Round(Purchase_Amnt_Total, 2)).ToString();
    //    //lblSaleValue.Text = Convert.ToString(Math.Round(Sale_Amnt_Total, 2)).ToString();
    //    #endregion
    //}

    private void calcultion()
    {
        double shubamt = Convert.ToDouble("0" + txtShubAmount.Text);
        double kharjat = Convert.ToDouble("0" + txtKhajrat.Text);
        double Total = Convert.ToDouble("0" + txtTotal.Text);
        double panjarpol = Convert.ToDouble("0" + txtPpolamount.Text);
        //double roundoff = Convert.ToDouble("0" + txtRoundoff.Text);
        double roundoff = txtRoundoff.Text != string.Empty ? Convert.ToDouble(txtRoundoff.Text) : 0.00;
        double postphone = Convert.ToDouble("0" + txtPostPhone.Text);
        double TaxableAmount = Convert.ToDouble("0" + txtTaxableAmount.Text);
        double IGST_Rate = Convert.ToDouble("0" + txtIGSTRate.Text);
        double IGST_Amount = Convert.ToDouble("0" + txtIGSTAmount.Text);
        double CGST_Rate = Convert.ToDouble("0" + txtCGSTRate.Text);
        double CGST_Amount = Convert.ToDouble("0" + txtCGSTAmount.Text);
        double SGST_Rate = Convert.ToDouble("0" + txtSGSTRate.Text);
        double SGST_Amount = Convert.ToDouble("0" + txtSGSTAmount.Text);
        double billamt;

        #region GSTCalculation
        string aaa = "";
        if (txtCustCode.Text.Trim() != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtCustCode.Text);
            if (a == true)
            {
                aaa = clsCommon.getString("select ISNULL(GSTStateCode,0) from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtCustCode.Text + "");
            }
        }
        int partygstStateCode = 0;
        if (aaa.Trim().ToString() != "")
        {
            partygstStateCode = Convert.ToInt32(aaa);
        }

        int companyGstStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
        string GSTRateCode = txtGST_RateCode.Text;
        double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
        double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
        double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
        double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));

        double CGSTAmount = 0.0;
        double SGSTAmount = 0.0;
        double IGSTAmount = 0.0;

        double CGSTRate = 0.00;
        double SGSTRate = 0.00;
        double IGSTRate = 0.00;
        double tamnt = Math.Round((Total + kharjat + shubamt + panjarpol + postphone), 2);
        txtTaxableAmount.Text = tamnt.ToString();
        double taxableamnt = Convert.ToDouble("0" + txtTaxableAmount.Text);

        if (companyGstStateCode == partygstStateCode)
        {
            double millamount = taxableamnt;
            CGSTRate = cgstrate;
            double cgsttaxAmountOnMR = Math.Round((millamount * cgstrate / 100), 2);
            CGSTAmount = Math.Round(cgsttaxAmountOnMR, 2);

            SGSTRate = sgstrate;
            double sgsttaxAmountOnMR = Math.Round((millamount * sgstrate / 100), 2);
            SGSTAmount = Math.Round(sgsttaxAmountOnMR, 2);
        }
        else
        {
            IGSTRate = igstrate;
            double igsttaxAmountOnMR = ((taxableamnt) * igstrate / 100);
            IGSTAmount = Math.Round(igsttaxAmountOnMR, 2);
        }
        txtCGSTRate.Text = CGSTRate.ToString();
        txtCGSTAmount.Text = CGSTAmount.ToString();
        txtSGSTRate.Text = SGSTRate.ToString();
        txtSGSTAmount.Text = SGSTAmount.ToString();
        txtIGSTRate.Text = IGSTRate.ToString();
        txtIGSTAmount.Text = IGSTAmount.ToString();

        #endregion

        // billamt = shubamt + kharjat + Total + panjarpol + roundoff + postphone;
        billamt = taxableamnt + IGSTAmount + CGSTAmount + SGSTAmount + roundoff;
        txtbillamnt.Text = billamt.ToString();

        #region TCS Calculation
        //double TCS_Rate = 0.000;
        //double TCS_Amt = 0.00;
        //double Bill_Amt = 0.00;
        //double Net_Payable_Amt = 0.00;
        //TCS_Rate = Convert.ToDouble(txtTCSRate.Text);
        //Bill_Amt = Convert.ToDouble(txtbillamnt.Text);
        //TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
        //Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt), 2);


        //txtTCSAmt.Text = TCS_Amt.ToString();
        //txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
        #endregion

        #region TCS Calculation
        double TCS_Rate = 0.000;
        double TDS_Rate = 0.000;
        double TCS_Amt = 0.00;
        double TDS_Amt = 0.00;
        double Bill_Amt = 0.00;
        double Net_Payable_Amt = 0.00;

        string dtt = "01/07/2021";
        string dt11 = txtdoc_date.Text;
        //DateTime TDSDate = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //DateTime docdate = Convert.ToDateTime(dt11);
        DateTime docdate = DateTime.Parse(dt11, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime TDSDate = DateTime.Parse(dtt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        if (docdate >= TDSDate)
        {
            if (hdnfSaleTDSRate.Value != string.Empty)
            {
                txtTDSRate.Text = hdnfSaleTDSRate.Value;
            }
            else
            {
                txtTDSRate.Text = Session["Jaggary_SaleTDSRate"].ToString();
            }
            if (hdnfSaleTCSRate.Value != string.Empty)
            {
                txtTCSRate.Text = hdnfSaleTCSRate.Value;
            }

            // txtTCSRate.Text = "0.000";
        }
        else
        {
            txtTDSRate.Text = "0.000";
            if (hdnfSaleTCSRate.Value != string.Empty)
            {
                txtTCSRate.Text = hdnfSaleTCSRate.Value;
            }
            else
            {
                txtTCSRate.Text = Session["Jaggary_TCSRate"].ToString();
            }
        }

        TCS_Rate = Convert.ToDouble("0" + txtTCSRate.Text);

        TDS_Rate = Convert.ToDouble("0" + txtTDSRate.Text);

        Bill_Amt = Convert.ToDouble(txtbillamnt.Text);
        TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
        TDS_Amt = Math.Round(((Bill_Amt * TDS_Rate) / 100), 2);
        Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt), 2);

        txtTCSAmt.Text = TCS_Amt.ToString();
        txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
        txtTDSAmt.Text = TDS_Amt.ToString();
        #endregion

    }


    private void calcultionForaccode()
    {



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



    #region [txtCustCode_TextChanged]
    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCustCode.Text;
        strTextBox = "txtCustCode";
        csCalculations();
    }
    #endregion


    #region [txtGST_RateCode_TextChanged]
    protected void txtGST_RateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_RateCode.Text;
        strTextBox = "txtGST_RateCode";
        csCalculations();
    }
    #endregion

    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();

        grdtotal1();

        //columnTotal();
    }
    #endregion

    protected void grdtotal1()
    {
        #region  Details
        //DataSet ds = new DataSet();
        // DataTable dt = new DataTable();
        qry = "select  [Item_Code] as Item, [ItemName] as ItemName, [NetQty] as Total" +
            " from NT_1_BillWiseQty where  Bill_No=" + txtdoc_no.Text + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString());
        // qry = "select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty,netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value from qryAwakBalance UNION select TRAN_TYPE,doc_no,Item_Code,ItemName,Qty,netqty,Gross_Rate as Rate,Gross_Amnt as Sale_value from qryJawakBalance where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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

                    //dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Total", typeof(int)));
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    dt.Rows[i]["ItemName"] = dt.Rows[0]["ItemName"].ToString();
                    //    dt.Rows[i]["Total"] = dt.Rows[0]["Total"].ToString();
                    //}
                    grdtotal.DataSource = dt;
                    grdtotal.DataBind();
                    ViewState["currentTable"] = dt;
                }
                else
                {
                    grdtotal.DataSource = null;
                    grdtotal.DataBind();
                    ViewState["currentTable"] = null;
                }
            }
            else
            {
                grdtotal.DataSource = null;
                grdtotal.DataBind();
                ViewState["currentTable"] = null;
            }



        }
        else
        {
            grdtotal.DataSource = null;
            grdtotal.DataBind();
            ViewState["currentTable"] = null;
        }
        #endregion
    }

    #region [txtRate_TextChanged]
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        csCalculations();
    }
    #endregion

    #region [txtbillamnt_TextChanged]
    protected void txtbillamnt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtbillamnt.Text;
        strTextBox = "txtbillamnt";
        csCalculations();
    }
    #endregion

    #region [txtRoundoff_TextChanged]
    protected void txtRoundoff_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRoundoff.Text;
        strTextBox = "txtRoundoff";
        csCalculations();
    }
    #endregion

    #region [txtKhajrat_TextChanged]
    protected void txtKhajrat_TextChanged(object sender, EventArgs e)
    {
        searchString = txtKhajrat.Text;
        strTextBox = "txtKhajrat";
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


    #region [txtPpolamount_TextChanged]
    protected void txtPpolamount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPpolamount.Text;
        strTextBox = "txtPpolamount";
        csCalculations();
    }
    #endregion

    #region [txtShubAmount_TextChanged]
    protected void txtShubAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShubAmount.Text;
        strTextBox = "txtShubAmount";
        csCalculations();
    }
    #endregion

    #region [txtSaleValue_TextChanged]
    protected void txtSaleValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSaleValue.Text;
        strTextBox = "txtSaleValue";
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

    #region [txtQty]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
    }
    #endregion

    #region [txtPostPhone_TextChanged]
    protected void txtPostPhone_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPostPhone.Text;
        strTextBox = "txtPostPhone";
        csCalculations();
    }
    #endregion

    #region [txtTaxableAmount_TextChanged]
    protected void txtTaxableAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTaxableAmount.Text;
        strTextBox = "txtTaxableAmount";
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

    #region [txtIGSTRate_TextChanged]
    protected void txtIGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGSTRate.Text;
        strTextBox = "txtIGSTRate";
        csCalculations();
    }
    #endregion

    #region [txtIGSTAmount_TextChanged]
    protected void txtIGSTAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGSTAmount.Text;
        strTextBox = "txtIGSTAmount";
        csCalculations();
    }
    #endregion

    #region [txtCGSTRate_TextChanged]
    protected void txtCGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGSTRate.Text;
        strTextBox = "txtCGSTRate";
        csCalculations();
    }
    #endregion

    #region [txtCGSTAmount_TextChanged]
    protected void txtCGSTAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGSTAmount.Text;
        strTextBox = "txtCGSTAmount";
        csCalculations();
    }
    #endregion

    #region [txtSGSTRate_TextChanged]
    protected void txtSGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGSTRate.Text;
        strTextBox = "txtSGSTRate";
        csCalculations();
    }
    #endregion

    #region [txtSGSTAmount_TextChanged]
    protected void txtSGSTAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGSTAmount.Text;
        strTextBox = "txtSGSTAmount";
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
                if (btntxtdoc_no.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,Ac_Name_E as Customer_Name from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }


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
                string qry = "select Item_Code,ItemName,Qty,detail_id from qryAwakBalance where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString());
                //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtGST_RateCode")
            {
                lblPopupHead.Text = "--Select GST RateCode--";
                string qry = "select Doc_no,GST_Name,Rate from NT_1_GSTRateMaster where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString());
                //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
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
        //    string v = hdnfClosePopup.Value;
        //    int i = 0;
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (v == "txtnarration")
        //        {
        //            e.Row.Cells[0].Width = new Unit("60px");
        //            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //        }

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
            if (hdnfClosePopup.Value == "txtGST_RateCode")
            {
                setFocusControl(txtGST_RateCode);
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
                //string qry = "select *,CONVERT(varchar(10),doc_date,103) as DOC_DATE from " + tblPrefix + "JSaleHead where doc_no='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and tran_type='" + trntype + "'"; ;
                //string qry = "select *,CONVERT(varchar(10),doc_date,103) as DOC_DATE from " + tblPrefix + "JSaleHead where doc_no='" 
                //    + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and tran_type='" + trntype + "'"; ;

                string qry = "select * from " + qryCommon + " where doc_no=" + hdnf.Value + " and Tran_Type='JS' and Year_Code="
                    + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";

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


    protected void btnpartysendsms_Click(object sender, CommandEventArgs e)
    {
        try
        {
            if (txtpartyMobno.Text == "0")
            {
                //string NUMBERS = txtPartyMobno.Text + "," + txtTransportMobno.Text + "," + txtDriverMobno.Text;

                setFocusControl(txtpartyMobno);
                return;
            }

            string PartyName = lblCustomer.Text;
            string date = txtdoc_date.Text;
            string invoiceno = txtdoc_no.Text;
            //string ewaybillno = txtEway_Bill_No.Text == string.Empty ? Convert.ToString(txtEway_Bill_No.Text) : " EWay Bill: " + txtEway_Bill_No.Text.Trim();
            string amount = txtbillamnt.Text;
            string qty = txtTotalqty.Text;


            string msg = string.Empty;
            string MsgforMail = string.Empty;

            msg = "Party Name: " + PartyName + " ,Invoice Date :" + date + " ,Invoice No : " + invoiceno + " ,Quantity :" + qty + " ,Bill Amount :" + amount;
            //  MsgforMail = "Banbr/>k Details Of Party <br/>" + PartyName + " <br/>" + BankName + " <br/>" + BankAc_number + " <br/>" + BankIFSCode;

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            if (txtpartyMobno.Text != "")
            {
                string code = txtpartyMobno.Text;
                string subject = txtpartyMobno.Text;
                foreach (string singleSubject in subject.Split(','))
                {
                    if (!dict.ContainsKey(singleSubject))
                    {
                        string partymob = string.Empty;
                        partymob = singleSubject;
                        if (e.CommandName == "sms")
                        {
                            if (!string.IsNullOrWhiteSpace(partymob))
                            {
                                string msgAPI = clsGV.msgAPI;
                                string URL = msgAPI + "mobile=" + partymob + "&message=" + msg + "&senderid=RBCKOP&accusage=1";
                                clsCommon.apicall(URL);

                            }
                        }
                        //txtPartyMobno.Text = singleSubject;

                        // dict.Add(subject, new List<string> { code });
                    }
                    else
                    {
                        dict[subject].Add(code);
                    }
                }
            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert('SMS  Sent Successfully!')", true);
        }
        catch (Exception)
        {
            throw;
        }

    }


    #region [txtTCSRate_TextChanged]
    protected void txtTCSRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSRate.Text;
        strTextBox = "txtTCSRate";
        hdnfSaleTCSRate.Value = txtTCSRate.Text;
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

    #region [txtTDSRate_TextChanged]
    protected void txtTDSRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSRate.Text;
        strTextBox = "txtTDSRate";
        hdnfSaleTDSRate.Value = txtTDSRate.Text;
        csCalculations();
    }
    #endregion
    #region [txtTDSAmt_TextChanged]
    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAmt.Text;
        strTextBox = "txtTDSAmt";
        csCalculations();
    }
    #endregion
}