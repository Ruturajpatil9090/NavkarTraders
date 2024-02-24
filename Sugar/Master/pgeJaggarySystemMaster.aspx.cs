using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Master_pgeJaggarySystemMaster : System.Web.UI.Page
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
    int defaultAccountCode = 0;
    string trntype = string.Empty;
    string qryAccountList = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    static WebControl objAsp = null;
    string Sale_Bill = string.Empty;
    int Doc_No;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        tblHead = tblPrefix + "JaggarySystemMaster";
        tblDetails = tblPrefix + "";
        AccountMasterTable = tblPrefix + "AccountMaster";
        qryCommon = tblPrefix + "";
        qryAccountList = tblPrefix + "qryAccountsList";
        cityMasterTable = tblPrefix + "CityMaster";
        systemMasterTable = tblPrefix + "SystemMaster";
        user = Session["user"].ToString();
        trntype = drpSystype.SelectedValue;
        drpSystype.SelectedValue = trntype;
        pnlPopup.Style["display"] = "none";
        Sale_Bill = drpsalebill.SelectedValue.ToString();

        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                BindContrydropdown();
                Sale_Bill = drpsalebill.SelectedValue.ToString();

                clsButtonNavigation.enableDisable("N");
                pnlPopup.Style["display"] = "none";
                this.makeEmptyForm("N");

                ViewState["mode"] = "I";
                this.showLastRecord();
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
    protected void drpsalebill_SelectedIndexChanged(object sender, EventArgs e)
    {

        Sale_Bill = drpsalebill.SelectedValue.ToString();

        //showLastRecord();
    }
    protected void BindContrydropdown()
    {
        try
        {

            string qry = "Select [System_Code],[System_Name_E] from [NT_1_SystemMaster] where [System_Type]='O' and [Company_Code]=" +
                Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString());
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {

                    dt = ds.Tables[0];
                    dt.Rows.Add("0", "Select_Value");

                    drpsalebill.DataValueField = "System_Code";
                    drpsalebill.DataTextField = "System_Name_E";

                    //drpsalebill.AppendDataBoundItems = false;
                    if (dt.Rows.Count > 0)
                    {
                        dt.DefaultView.Sort = "System_Code";
                        drpsalebill.DataSource = dt;

                        drpsalebill.DataBind();

                    }
                    else
                    {
                        drpsalebill.DataSource = null;
                        drpsalebill.DataBind();


                    }
                }
            }

        }
        catch
        {
        }
    }

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

    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(System_Code) as System_Code from " + tblHead +
                 " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + trntype + "'";

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
                        hdnf.Value = dt.Rows[0][0].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            btnEdit.Enabled = true;
                            btnDelete.Enabled = true;
                            btnAdd.Focus();
                            this.enableDisableNavigateButtons();
                        }
                        else                     //new code
                        {
                            this.makeEmptyForm("N");
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                }
            }
        }
        catch
        {
        }

    }

    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + tblHead +
                " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Code=" + hdnf.Value + " and System_Type='" + drpSystype.SelectedValue + "'";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }

    private void makeEmptyForm(string dAction)
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
            btntxtsystemcode.Text = "Choose No";
            btntxtsystemcode.Enabled = false;
            lblMsg.Text = string.Empty;
            #region logic
            txtPurchaseAc.Enabled = false;
            txtSaleAC.Enabled = false;
            txtvatAC.Enabled = false;
            txtOpeningBal.Enabled = false;
            btntxtPurchaseAc.Enabled = false;
            btntxtSaleAC.Enabled = false;
            btntxtvatAC.Enabled = false;
            drpCategory.Enabled = false;
            drpsalebill.Enabled = false;
            drpRelatedTo.Enabled = false;
            lblPurchaseACName.Text = string.Empty;
            lblSaleACName.Text = string.Empty;
            lblVatACName.Text = string.Empty;
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
            btntxtsystemcode.Text = "Change No";
            btntxtsystemcode.Enabled = true;
            setFocusControl(txtsystemName);
            #region logic
            if (drpSystype.SelectedValue == "I")
            {
                txtPurchaseAc.Enabled = true;
                txtSaleAC.Enabled = true;
                txtvatAC.Enabled = true;
                txtOpeningBal.Enabled = true;
                btntxtPurchaseAc.Enabled = true;
                btntxtSaleAC.Enabled = true;
                btntxtvatAC.Enabled = true;

                lblPurchaseACName.Text = string.Empty;
                lblSaleACName.Text = string.Empty;
                lblVatACName.Text = string.Empty;
                drpsalebill.Enabled = true;


            }
            if (drpSystype.SelectedValue == "S")
            {
                txtPurchaseAc.Enabled = false;
                txtSaleAC.Enabled = false;
                txtvatAC.Enabled = false;
                txtOpeningBal.Enabled = false;
                btntxtPurchaseAc.Enabled = false;
                btntxtSaleAC.Enabled = false;
                btntxtvatAC.Enabled = false;
                txtSysRate.Enabled = false;
                lblPurchaseACName.Text = string.Empty;
                lblSaleACName.Text = string.Empty;
                lblVatACName.Text = string.Empty;
            }
            drpCategory.Enabled = true;
            drpRelatedTo.Enabled = true;
            //else
            //{
            //    txtPurchaseAc.Enabled = false;
            //    txtSaleAC.Enabled = false;
            //    txtvatAC.Enabled = false;
            //    txtOpeningBal.Enabled = false;
            //    btntxtPurchaseAc.Enabled = false;
            //    btntxtSaleAC.Enabled = false;
            //    btntxtvatAC.Enabled = false;

            //    lblPurchaseACName.Text = string.Empty;
            //    lblSaleACName.Text = string.Empty;
            //    lblVatACName.Text = string.Empty;
            //}
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
            btntxtsystemcode.Text = "Choose No";
            btntxtsystemcode.Enabled = false;
            #region logic
            drpSystype.Enabled = true;
            btntxtPurchaseAc.Enabled = false;
            btntxtSaleAC.Enabled = false;
            btntxtvatAC.Enabled = false;
            drpsalebill.Enabled = false;
            drpCategory.Enabled = false;
            drpRelatedTo.Enabled = false;
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
            btntxtsystemcode.Text = "Choose No";
            btntxtsystemcode.Enabled = true;
            lblMsg.Text = string.Empty;
            setFocusControl(txtsystemName);
            #region logic
            if (drpSystype.SelectedValue == "I")
            {
                txtPurchaseAc.Enabled = true;
                txtSaleAC.Enabled = true;
                txtvatAC.Enabled = true;
                txtOpeningBal.Enabled = true;
                btntxtPurchaseAc.Enabled = true;
                btntxtSaleAC.Enabled = true;
                btntxtvatAC.Enabled = true;
                drpsalebill.Enabled = true;

            }
            else
            {
                txtPurchaseAc.Enabled = false;
                txtSaleAC.Enabled = false;
                txtvatAC.Enabled = false;
                txtOpeningBal.Enabled = false;
                btntxtPurchaseAc.Enabled = false;
                btntxtSaleAC.Enabled = false;
                btntxtvatAC.Enabled = false;

                lblPurchaseACName.Text = string.Empty;
                lblSaleACName.Text = string.Empty;
                lblVatACName.Text = string.Empty;

            }

            drpCategory.Enabled = true;
            drpRelatedTo.Enabled = true;

            #endregion
        }
    }

    protected void txtsystemcode_TextChanged(object sender, EventArgs e)
    {
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtsystemcode.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtsystemcode.Text != string.Empty)
                {
                    txtValue = txtsystemcode.Text;

                    string qry = "select * from " + tblHead + " where  System_Code='" + txtValue + "' " +
                        "  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and System_Type='" + trntype + "'";

                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //Record Found
                                hdnf.Value = dt.Rows[0]["System_Code"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        // this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtsystemName);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtsystemcode.Enabled = false;
                                            setFocusControl(txtsystemcode);

                                            hdnf.Value = txtsystemcode.Text;

                                        }
                                    }
                                }
                            }
                            else   //Record Not Found
                            {
                                if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                {
                                    lblMsg.Text = "";
                                    setFocusControl(txtsystemName);
                                    txtsystemcode.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtsystemcode.Text = string.Empty;
                                    setFocusControl(txtsystemcode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtsystemcode);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtsystemcode.Text = string.Empty;
                setFocusControl(txtsystemcode);
            }
        }
        catch
        {

        }
        #endregion
    }

    protected void btntxttxtsystemcode_click(object sender, EventArgs e)
    {
        try
        {
            if (btntxtsystemcode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtsystemcode.Text = string.Empty;
                txtsystemcode.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtsystemcode);
            }

            if (btntxtsystemcode.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtsystemcode";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (searchString != string.Empty || strTextBox == hdnfClosePopup.Value)
        {
            txtSearchText.Text = searchString;
        }
        else
        {
            txtSearchText.Text = txtSearchText.Text;
        }

        if (hdnfClosePopup.Value == "txtsystemcode")
        {
            if (btntxtsystemcode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtsystemcode.Text = string.Empty;
                txtsystemcode.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtsystemcode);
                hdnfClosePopup.Value = "Close";
            }

            if (btntxtsystemcode.Text == "Choose No")
            {
                lblPopupHead.Text = "--Select Group--";
                string qry = "SELECT  System_Code,System_Name_E from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'";
                this.showPopup(qry);
            }
        }
        if (hdnfClosePopup.Value == "txtPurchaseAc" || hdnfClosePopup.Value == "txtSaleAC" || hdnfClosePopup.Value == "txtvatAC")
        {
            lblPopupHead.Text = "--Select Purchase AC--";
            string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
                " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' )";
            this.showPopup(qry);
        }

        if (hdnfClosePopup.Value == "txtLevi")
        {
            lblPopupHead.Text = "--Select Levi AC--";
            //string qry = "select System_Code,System_Name_E,System_Rate from " + tblPrefix + "SystemMaster where System_Type='L' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
            //    " (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' )";
            //string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
            //   " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' )";

            string qry = "select System_Code,System_Name_E,System_Rate from " + tblPrefix + "JaggarySystemMaster where System_Type='L' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
                " (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' )";
            
            this.showPopup(qry);
        }

        if (hdnfClosePopup.Value == "txtInsurance")
        {
            lblPopupHead.Text = "--Select Insurance AC--";
            //string qry = "select System_Code,System_Name_E,System_Rate from " + tblPrefix + "SystemMaster where System_Type='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
            //    " (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' )";
            string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
               " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' )";
       
            this.showPopup(qry);
        }

        if (hdnfClosePopup.Value == "txtgstratecode")
        {
            lblPopupHead.Text = "--Select Gst Rate Code--";
            string qry = "select [Doc_no],[GST_Name],[Rate] from " + tblPrefix + "GSTRateMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and" +
                " (Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' )";
            this.showPopup(qry);
        }

    }

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


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        drpSystype.Enabled = false;
        this.NextNumber();
        //this.getMaxCode();
        txtsystemcode.Enabled = false;
        setFocusControl(txtsystemName);
    }

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(System_Code) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and System_Type='" + drpSystype.SelectedValue + "'"));
            if (counts == 0)
            {

                txtsystemcode.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(System_Code) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and System_Type='" + drpSystype.SelectedValue + "' ")) + 1;
                txtsystemcode.Text = Doc_No.ToString();
            }

           
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }

    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;

            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'";
                obj.code = "System_Code";
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
                                    txtsystemcode.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtsystemcode.Enabled = false;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region validation
            bool isValidated = true;
            if (txtsystemcode.Text != string.Empty)
            {
                if (ViewState["mode"] != null)
                {
                    if (ViewState["mode"].ToString() == "I")
                    {
                        string str = clsCommon.getString("select System_Name_E from " + tblHead + " where System_Code=" + txtsystemcode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'");
                        if (str != string.Empty)
                        {
                            lblMsg.Text = "Code " + txtsystemcode.Text + " already exist";
                            this.getMaxCode();
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
                setFocusControl(txtsystemcode);
            }

            if (txtsystemName.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtsystemName);
            }
            if (drpSystype.SelectedValue == "V")
            {
                if (txtSysRate.Text != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtSysRate);
                }
            }
            if (drpSystype.SelectedValue == "I")          //if type I=Item
            {
                if (txtPurchaseAc.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtPurchaseAc.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                        setFocusControl(txtPurchaseAc);
                        return;
                    }
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtPurchaseAc);
                    return;
                }

                if (txtgstratecode.Text != string.Empty)
                {
                    string str = clsCommon.getString("select [GST_Name] from  [NT_1_GSTRateMaster] where [Doc_no]=" + txtgstratecode.Text
                        + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                        setFocusControl(txtgstratecode);
                        return;
                    }
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtgstratecode);
                    return;
                }

                if (txtweight.Text != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtweight);
                    return;
                }




                if (txtSaleAC.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtSaleAC.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                        setFocusControl(txtSaleAC);
                        return;
                    }
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtSaleAC);
                    return;
                }
                if (txtvatAC.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtvatAC.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = false;
                        setFocusControl(txtvatAC);
                        return;
                    }
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtvatAC);
                    return;
                }
            }


            #endregion

            #region declaration
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            string System_Type = drpSystype.SelectedValue;
            Int32 System_Code = txtsystemcode.Text != string.Empty ? Convert.ToInt32(txtsystemcode.Text) : 0;
            string System_Name_E = txtsystemName.Text;
            double System_Rate = txtSysRate.Text != string.Empty ? Convert.ToDouble(txtSysRate.Text) : 0.00;
            Int32 Purchase_AC = txtPurchaseAc.Text != string.Empty ? Convert.ToInt32(txtPurchaseAc.Text) : 0;
            Int32 Sale_AC = txtSaleAC.Text != string.Empty ? Convert.ToInt32(txtSaleAC.Text) : 0;
            Int32 Vat_AC = txtvatAC.Text != string.Empty ? Convert.ToInt32(txtvatAC.Text) : 0;
            double Opening_Bal = txtOpeningBal.Text != string.Empty ? Convert.ToDouble(txtOpeningBal.Text) : 0.00;
            string HSNNumber = txtHsnNumber.Text;
            string retValue = string.Empty;
            string strRev = string.Empty;
            Int32 Levicode = txtLevi.Text != string.Empty ? Convert.ToInt32(txtLevi.Text) : 0;
            // double Levi_Rate = lblLeviRate.Text != string.Empty ? Convert.ToDouble(lblLeviRate.Text) : 0.00;
            Int32 Insurance = txtInsurance.Text != string.Empty ? Convert.ToInt32(txtInsurance.Text) : 0;
            double weight = txtweight.Text != string.Empty ? Convert.ToDouble(txtweight.Text) : 0.00;
            Int32 gstratecode = txtgstratecode.Text != string.Empty ? Convert.ToInt32(txtgstratecode.Text) : 0;
            string category = drpCategory.SelectedValue;
            string maingroup = drpsalebill.SelectedValue;
            string RelatedTo = drpRelatedTo.SelectedValue;
            #endregion

            #region save
            if (isValidated == true)
            {
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    if (ViewState["mode"] != null)
                    {
                        DataSet ds = new DataSet();

                        if (ViewState["mode"].ToString() == "I")
                        {
                            obj.flag = 1;
                            obj.tableName = tblHead;
                            obj.columnNm = "System_Type,System_Code,System_Name_E,System_Rate,Purchase_AC,Sale_AC,Vat_AC,Opening_Bal,Company_Code,Year_Code," +
                                "Branch_Code,Created_By,HSN,Insurance,Levi,weight,gstratecode,category,MainGroup,RelatedTo";
                            obj.values = "'" + System_Type + "','" + System_Code + "','" + System_Name_E + "','" + System_Rate + "','" + Purchase_AC + "','"
                                + Sale_AC + "','" + Vat_AC + "','" + Opening_Bal + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','"
                                + Convert.ToInt32(Session["year"].ToString()) + "','" + Convert.ToInt32(Session["Branch_Code"].ToString()) + "','" + user
                                + "','" + HSNNumber + "','" + Insurance + "','" + Levicode + "','" + weight + "','" + gstratecode + "','" + category
                                + "','" + maingroup + "','" + RelatedTo + "'";
                            ds = obj.insertAccountMaster(ref strRev);

                            retValue = strRev;
                        }
                        if (ViewState["mode"].ToString() == "U")
                        {
                            obj.flag = 2;
                            obj.tableName = tblHead;
                            obj.columnNm = " System_Name_E='" + System_Name_E + "',System_Rate='" + System_Rate + "',Purchase_AC='" + Purchase_AC
                                + "',Sale_AC='" + Sale_AC + "',Vat_AC='" + Vat_AC + "',Opening_Bal='" + Opening_Bal + "',  Modified_By='"
                                + user + "',HSN='" + HSNNumber + "',Levi='" + Levicode + "',Insurance='" + Insurance + "', weight='" + weight
                                + "', gstratecode='" + gstratecode + "', category='" + category + "', MainGroup='" + maingroup + "',RelatedTo='" + RelatedTo
                                + "' where System_Code="
                                + System_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + trntype + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRev);

                            retValue = strRev;
                        }
                        if (retValue == "-1")
                        {
                            hdnf.Value = txtsystemcode.Text;
                            clsButtonNavigation.enableDisable("S");
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                            string qry = getDisplayQuery(); ;
                            this.fetchRecord(qry);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Added!');", true);
                        }
                        if (retValue == "-2" || retValue == "-3")
                        {
                            clsButtonNavigation.enableDisable("S");
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                            hdnf.Value = txtsystemcode.Text;
                            string qry = getDisplayQuery(); ;
                            this.fetchRecord(qry);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Updated !');", true);

                        }
                    }
                }
            }
            #endregion
        }
        catch
        {

        }
    }

    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;
            if (hdnf.Value != string.Empty)
            {

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
                            txtsystemcode.Text = dt.Rows[0]["System_Code"].ToString();
                            drpSystype.SelectedValue = dt.Rows[0]["System_Type"].ToString();
                            txtsystemName.Text = dt.Rows[0]["System_Name_E"].ToString();
                            txtSysRate.Text = dt.Rows[0]["System_Rate"].ToString();
                            string purch_Ac = dt.Rows[0]["Purchase_AC"].ToString();
                            txtPurchaseAc.Text = purch_Ac;
                            lblPurchaseACName.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + purch_Ac + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string sale_Ac = dt.Rows[0]["Sale_AC"].ToString();
                            txtSaleAC.Text = sale_Ac;
                            lblSaleACName.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + sale_Ac + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string vat_Ac = dt.Rows[0]["Vat_AC"].ToString();
                            txtvatAC.Text = vat_Ac;
                            lblVatACName.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + vat_Ac + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            txtOpeningBal.Text = dt.Rows[0]["Opening_Bal"].ToString();
                            txtHsnNumber.Text = dt.Rows[0]["HSN"].ToString();

                            string levicode = dt.Rows[0]["Levi"].ToString();
                            txtLevi.Text = levicode;
                            //lblLeviRate.Text = clsCommon.getString("Select System_Rate from " + tblPrefix + "SystemMaster where System_Type='L' and  System_Code=" + levicode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            //lblLeviRate.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + levicode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblLeviRate.Text = clsCommon.getString("Select System_Rate from " + tblPrefix + "JaggarySystemMaster where System_Type='L' and  System_Code=" + levicode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                           
                            txtweight.Text = dt.Rows[0]["weight"].ToString();
                            txtgstratecode.Text = dt.Rows[0]["gstratecode"].ToString();
                            lblgstratecodename.Text = clsCommon.getString("Select GST_Name from " + tblPrefix
                                + "GSTRateMaster where [Doc_no]=" + txtgstratecode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            string category = dt.Rows[0]["category"].ToString();
                            string maingrp = dt.Rows[0]["MainGroup"].ToString();
                            string RelatedTo = dt.Rows[0]["RelatedTo"].ToString();
                            if (RelatedTo == "")
                            {
                                drpRelatedTo.SelectedValue = "0";
                            }
                            else
                            {
                                drpRelatedTo.SelectedValue = RelatedTo.ToString();
                            }
                            if (category == "")
                            {
                                drpCategory.SelectedValue = "D";
                            }
                            else
                            {

                                drpCategory.SelectedValue = category.ToString();

                            }
                            if (maingrp == "")
                            {
                                drpsalebill.SelectedValue = "0";

                            }
                            else
                            {
                                drpsalebill.SelectedValue = maingrp.ToString();
                            }
                            string insurancecoed = dt.Rows[0]["Insurance"].ToString();
                            txtInsurance.Text = insurancecoed;
                            //lblInsuranceRate.Text = clsCommon.getString("Select System_Rate from " + tblPrefix + "SystemMaster where System_Type='C' and System_Code=" + insurancecoed + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblInsuranceRate.Text = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + insurancecoed + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                          
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
                            lblMsg.Text = "";

                        }
                    }
                }
                // this.enableDisableNavigateButtons();
                hdnf.Value = txtsystemcode.Text;
                return recordExist;
            }
            else
            {
                //   this.enableDisableNavigateButtons();
                return recordExist;
            }

        }
        catch
        {
            return false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");

        this.makeEmptyForm("E");
        txtsystemcode.Enabled = false;
        drpSystype.Enabled = false;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string currentDoc_No = txtsystemcode.Text;

                DataSet ds = new DataSet();
                string strrev = "";
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {


                    obj.flag = 3;
                    obj.tableName = tblHead;
                    obj.columnNm = "   System_Code=" + currentDoc_No +
                    " and System_Type='" + drpSystype.SelectedValue + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                    obj.values = "none";
                    ds = obj.insertAccountMaster(ref strrev);

                }
                string query = "";

                if (strrev == "-3")
                {
                    query = "SELECT top 1 [System_Code] from " + tblHead + "  where System_Code>" + Convert.ToInt32(currentDoc_No) +
                           "  and System_Type='" + drpSystype.SelectedValue + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            " ORDER BY System_Code asc  ";


                    hdnf.Value = clsCommon.getString(query);

                    if (hdnf.Value == string.Empty)
                    {
                        query = "SELECT top 1 [System_Code] from " + tblHead + "  where System_Code<" + Convert.ToInt32(currentDoc_No) +
                           " and System_Type='" + drpSystype.SelectedValue + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            " ORDER BY System_Code desc  ";
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
                }
                this.enableDisableNavigateButtons();
            }
        }

        catch
        {
        }
    }

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

        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        drpSystype.Enabled = true;
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select System_Code from " + tblHead + " where System_Code=(select MIN(System_Code) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "') " +
                "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsystemcode.Text != string.Empty)
            {
                string query = "SELECT top 1 [System_Code] from " + tblHead + " where System_Code<" + Convert.ToInt32(hdnf.Value) +
                    "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'" +
                    " ORDER BY System_Code DESC  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsystemcode.Text != string.Empty)
            {
                string query = "SELECT top 1 [System_Code] from " + tblHead + " where System_Code>" + Convert.ToInt32(hdnf.Value) +
                    "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'" +
                    " ORDER BY System_Code asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select System_Code from " + tblHead + " where System_Code=(select MAX(System_Code) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "') " +
                "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }

    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtsystemcode.Text = hdnf.Value;

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

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'";


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
        if (RecordCount > 0)
        {
            if (txtsystemcode.Text != string.Empty)
            {
                #region check for next or previous record exist or not

                query = "SELECT top 1 [System_Code] from " + tblHead + " where System_Code>" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'" +
                    " ORDER BY System_Code asc  ";
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


                query = "SELECT top 1 [System_Code] from " + tblHead + " where System_Code<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='" + drpSystype.SelectedValue + "'" +
                    " ORDER BY System_Code desc  ";
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
        else
        {
            this.makeEmptyForm("N");
        }
        #endregion
    }
    #endregion


    protected void txtsystemName_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtSysRate);
    }

    protected void txtSysRate_TextChanged(object sender, EventArgs e)
    {
        if (txtPurchaseAc.Enabled == true)
        {
            setFocusControl(txtPurchaseAc);
        }
        else
        {
            setFocusControl(btnSave);
        }
    }

    protected void txtPurchaseAc_TextChanged(object sender, EventArgs e)
    {

        if (txtPurchaseAc.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtPurchaseAc.Text);
            if (a == false)
            {
                searchString = txtPurchaseAc.Text;
                btntxtPurchaseAc_Click(this, new EventArgs());
            }
            else
            {
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtPurchaseAc.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    lblPurchaseACName.Text = str;
                    setFocusControl(txtSaleAC);
                }
                else
                {
                    lblPurchaseACName.Text = string.Empty;
                    txtPurchaseAc.Text = string.Empty;
                    setFocusControl(txtPurchaseAc);
                }
            }
        }
        else
        {
            lblPurchaseACName.Text = string.Empty;
            txtPurchaseAc.Text = string.Empty;
            setFocusControl(txtPurchaseAc);
        }
    }


    protected void txtLevi_TextChanged(object sender, EventArgs e)
    {
        if (txtLevi.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtLevi.Text);
            if (a == false)
            {
                searchString = txtLevi.Text;
                btntxtLevi_Click(this, new EventArgs());
            }
            else
            {
                //string str = clsCommon.getString("select System_Rate from " + tblPrefix + "SystemMaster where System_Type='L' and System_Code=" + txtLevi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string str = clsCommon.getString("select System_Rate from " + tblPrefix + "JaggarySystemMaster where System_Type='L' and System_Code=" + txtLevi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                //string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtLevi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
              
                   if (str != string.Empty)
                {
                    lblLeviRate.Text = str;
                    setFocusControl(txtLevi);
                }
                else
                {
                    lblLeviRate.Text = string.Empty;
                    txtLevi.Text = string.Empty;
                    setFocusControl(txtLevi);
                }
            }
        }
        else
        {
            lblLeviRate.Text = string.Empty;
            txtLevi.Text = string.Empty;
            setFocusControl(txtLevi);
        }
    }

    protected void txtInsurance_TextChanged(object sender, EventArgs e)
    {
        if (txtInsurance.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtInsurance.Text);
            if (a == false)
            {
                searchString = txtInsurance.Text;
                btntxtInsurance_Click(this, new EventArgs());
            }
            else
            {
                //string str = clsCommon.getString("select System_Rate from " + tblPrefix + "SystemMaster where System_Type='C' and System_Code=" + txtInsurance.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtInsurance.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
              
                if (str != string.Empty)
                {
                    lblInsuranceRate.Text = str;
                    setFocusControl(txtInsurance);
                }
                else
                {
                    lblInsuranceRate.Text = string.Empty;
                    txtInsurance.Text = string.Empty;
                    setFocusControl(txtInsurance);
                }
            }
        }
        else
        {
            lblSaleACName.Text = string.Empty;
            txtInsurance.Text = string.Empty;
            setFocusControl(txtInsurance);
        }
    }


    protected void txtSaleAC_TextChanged(object sender, EventArgs e)
    {
        if (txtSaleAC.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtSaleAC.Text);
            if (a == false)
            {
                searchString = txtSaleAC.Text;
                btntxtSaleAC_Click(this, new EventArgs());
            }
            else
            {
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtSaleAC.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    lblSaleACName.Text = str;
                    setFocusControl(txtvatAC);
                }
                else
                {
                    lblSaleACName.Text = string.Empty;
                    txtSaleAC.Text = string.Empty;
                    setFocusControl(txtSaleAC);
                }
            }
        }
        else
        {
            lblSaleACName.Text = string.Empty;
            txtSaleAC.Text = string.Empty;
            setFocusControl(txtSaleAC);
        }
    }

    protected void txtvatAC_TextChanged(object sender, EventArgs e)
    {

        if (txtvatAC.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtSaleAC.Text);
            if (a == false)
            {
                searchString = txtvatAC.Text;
                btntxtvatAC_Click(this, new EventArgs());
            }
            else
            {
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtvatAC.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    lblVatACName.Text = str;
                    setFocusControl(txtOpeningBal);
                }
                else
                {
                    lblVatACName.Text = string.Empty;
                    txtvatAC.Text = string.Empty;
                    setFocusControl(txtvatAC);
                }
            }
        }
        else
        {
            lblVatACName.Text = string.Empty;
            txtvatAC.Text = string.Empty;
            setFocusControl(txtvatAC);
        }
    }

    protected void txtOpeningBal_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnSave);
    }

    protected void btntxtLevi_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtLevi";
        searchString = txtLevi.Text;
        btnSearch_Click(sender, e);
    }

    protected void btntxtInsurance_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtInsurance";
        searchString = txtInsurance.Text;
        btnSearch_Click(sender, e);
    }

    protected void btntxtSaleAC_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtSaleAC";
        btnSearch_Click(sender, e);

    }


    protected void btntxtPurchaseAc_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtPurchaseAc";
        searchString = txtPurchaseAc.Text;
        btnSearch_Click(sender, e);
    }

    protected void btntxtvatAC_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtvatAC";
        btnSearch_Click(sender, e);
    }

    protected void drpSystype_TextChanged(object sender, EventArgs e)
    {
        string s_item = drpSystype.SelectedValue;
        drpSystype.SelectedValue = s_item;
        trntype = s_item;
        showLastRecord();
    }

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

    }

    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Width = new Unit("80px");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        }
    }

    protected void txtgstratecode_TextChanged(object sender, EventArgs e)
    {
        if (txtgstratecode.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtgstratecode.Text);
            if (a == false)
            {
                searchString = txtgstratecode.Text;
                btntxtgstratecode_Click(this, new EventArgs());
            }
            else
            {
                string str = clsCommon.getString("select [GST_Name] from NT_1_GSTRateMaster  where [Doc_no]=" + txtgstratecode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    lblgstratecodename.Text = str;
                    setFocusControl(btnSave);
                }
                else
                {
                    lblgstratecodename.Text = string.Empty;
                    txtgstratecode.Text = string.Empty;
                    setFocusControl(txtgstratecode);
                }
            }
        }
        else
        {
            lblgstratecodename.Text = string.Empty;
            txtgstratecode.Text = string.Empty;
            setFocusControl(txtgstratecode);
        }

    }
    protected void btntxtgstratecode_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtgstratecode";
        searchString = txtgstratecode.Text;
        btnSearch_Click(sender, e);
    }
}