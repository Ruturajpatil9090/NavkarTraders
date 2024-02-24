using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Master_pgeJaggaryBrand_Master : System.Web.UI.Page
{


    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string SystemMaster = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string GLedgerTable = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "BrandMaster";
            tblDetails = tblPrefix + "";
            SystemMaster = tblPrefix + "JaggarySystemMaster";
            // cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "qryJaggryBrandMaster";
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
                    this.showLastRecord();
                    this.enableDisableNavigateButtons();
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

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;

            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                txtEditDoc_No.Enabled = true;
                btntxtDOC_NO.Enabled = false;
                lblMsg.Text = string.Empty;

                lblHamaliCode_Name.Text = string.Empty;
                lblPackingCode_Name.Text = string.Empty;
                btntxtHamali_Code.Enabled = false;
                btntxtPacking_Code.Enabled = false;


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
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = "";

                #region set Business logic for save
                lblHamaliCode_Name.Text = string.Empty;
                lblPackingCode_Name.Text = string.Empty;
                btntxtHamali_Code.Enabled = true;
                btntxtPacking_Code.Enabled = true;

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
                btntxtHamali_Code.Enabled = false;
                btntxtPacking_Code.Enabled = false;
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
                txtEditDoc_No.Enabled = false;
                btntxtHamali_Code.Enabled = true;
                btntxtPacking_Code.Enabled = true;
                #region logic

                #endregion

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
            qry = "select max(Doc_No) as Doc_No from " + tblHead +
                 " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                            this.enableDisableNavigateButtons();
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
        }
        catch
        {
        }
    }
    #endregion

    //#region [enableDisableNavigateButtons]
    //private void enableDisableNavigateButtons()
    //{
    //    #region enable disable previous next buttons
    //    int RecordCount = 0;
    //    string query = "";
    //    query = "   select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

    //    string cnt = clsCommon.getString(query);
    //    if (cnt != string.Empty)
    //    {
    //        RecordCount = Convert.ToInt32(cnt);
    //    }

    //    if (RecordCount != 0 && RecordCount == 1)
    //    {
    //        btnFirst.Enabled = true;
    //        btnPrevious.Enabled = false;
    //        btnNext.Enabled = false;
    //        btnLast.Enabled = false;
    //    }
    //    else if (RecordCount != 0 && RecordCount > 1)
    //    {
    //        btnFirst.Enabled = true;
    //        btnPrevious.Enabled = false;
    //        btnNext.Enabled = false;
    //        btnLast.Enabled = true;
    //    }
    //    if (txtdoc_no.Text != string.Empty)
    //    {
    //        if (hdnf.Value != string.Empty)
    //        {
    //            #region check for next or previous record exist or not

    //            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) +
    //                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
    //                " ORDER BY Doc_No asc  ";
    //            string strDoc_No = clsCommon.getString(query);
    //            if (strDoc_No != string.Empty)
    //            {
    //                //next record exist
    //                btnNext.Enabled = true;
    //                btnLast.Enabled = true;
    //            }
    //            else
    //            {
    //                //next record does not exist
    //                btnNext.Enabled = false;
    //                btnLast.Enabled = false;
    //            }


    //            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) +
    //                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
    //                " ORDER BY Doc_No asc  ";
    //            strDoc_No = clsCommon.getString(query);
    //            if (strDoc_No != string.Empty)
    //            {
    //                //previous record exist
    //                btnPrevious.Enabled = true;
    //                btnFirst.Enabled = true;
    //            }
    //            else
    //            {
    //                btnPrevious.Enabled = false;
    //                btnFirst.Enabled = false;
    //            }
    //            #endregion
    //        }
    //    }
    //    #endregion
    //}
    //#endregion


    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons

        //if (ViewState["mode"].ToString() == "U")
        //{

        int RecordCount = 0;

        string query = "";
        query = "   select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()); ;
        //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
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
            //  btnLast.Focus();
        }

        if (txtdoc_no.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) +
                    "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY Doc_No asc  ";
                //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY tenderid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnLast.Enabled = true;
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnLast.Enabled = false;
                            btnNext.Enabled = false;
                        }
                    }
                }

                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY Doc_No asc  ";
                //    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY tenderid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnFirst.Enabled = true;
                            btnPrevious.Enabled = true;
                        }
                        else
                        {
                            btnFirst.Enabled = false;
                            btnPrevious.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        // }

        #endregion
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
        this.getMaxCode();
        setFocusControl(txtBrand_Name);

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
                string currentDoc_No = txtdoc_no.Text;

                DataSet ds = new DataSet();
                string query = "";
                string strrev = "";
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    obj.flag = 3;
                    obj.tableName = tblHead;
                    obj.columnNm = " Doc_No=" + currentDoc_No + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                    obj.values = "none";
                    ds = obj.insertAccountMaster(ref strrev);
                }

                if (strrev == "-3")
                {
                    query = "SELECT top 1 [Doc_No] from " + tblHead + "  where Doc_No>" + Convert.ToInt32(currentDoc_No) +
                           "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            " ORDER BY Doc_No asc  ";
                    hdnf.Value = clsCommon.getString(query);

                    if (hdnf.Value == string.Empty)
                    {
                        query = "SELECT top 1 [Doc_No] from " + tblHead + "  where Doc_No<" + Convert.ToInt32(currentDoc_No) +
                             "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            " ORDER BY Doc_No desc  ";
                        hdnf.Value = clsCommon.getString(query);
                    }

                    if (hdnf.Value != string.Empty)
                    {
                        query = getDisplayQuery(); ;
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
        string str = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

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
                        txtdoc_no.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtBrand_Name.Text = dt.Rows[0]["Brand_Name"].ToString();

                        txtHamali_Code.Text = dt.Rows[0]["Hamali_Code"].ToString();
                        txtPacking_Code.Text = dt.Rows[0]["Packing_Code"].ToString();
                        lblHamaliCode_Name.Text = dt.Rows[0]["HamaliCode_Name"].ToString();
                        lblPackingCode_Name.Text = dt.Rows[0]["PackingCode_Name"].ToString();



                        hdnf.Value = txtdoc_no.Text;
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
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = "";
            #region validation
            bool isValidated = true;

            if (txtdoc_no.Text != string.Empty)
            {
                if (ViewState["mode"].ToString() == "I")
                {

                    string str = clsCommon.getString("select Doc_No from " + tblHead + " where  Doc_No='" + txtdoc_no.Text + "'" +
                             "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblMsg.Text = "Doc No " + txtdoc_no.Text + " already exist";
                        getMaxCode();
                        isValidated = true;
                        return;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }
                else
                {
                    isValidated = true;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtdoc_no);
                hdnf.Value = txtdoc_no.Text;
                return;
            }

            if (txtBrand_Name.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtBrand_Name);
                return;
            }

            if (txtPacking_Code.Text != string.Empty)
            {
                string str = clsCommon.getString("select System_Name_E from " + SystemMaster + " where System_Code="
                    + txtPacking_Code.Text + " and System_Type='P' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtPacking_Code);
                    return;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtPacking_Code);
                return;
            }

            if (txtHamali_Code.Text != string.Empty)
            {
                string str = clsCommon.getString("select System_Name_E from " + SystemMaster + " where System_Code="
                    + txtHamali_Code.Text + " and System_Type='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtHamali_Code);
                    return;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtHamali_Code);
                return;
            }


            #endregion

            #region -Head part declearation

            Int32 DOC_NO = txtdoc_no.Text != string.Empty ? Convert.ToInt32(txtdoc_no.Text) : 0;
            string BrandName = txtBrand_Name.Text.ToUpper();
            Int32 Packing_Code = txtPacking_Code.Text != string.Empty ? Convert.ToInt32(txtPacking_Code.Text) : 0;
            Int32 Hamali_Code = txtHamali_Code.Text != string.Empty ? Convert.ToInt32(txtHamali_Code.Text) : 0;
            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            int year_Code = Convert.ToInt32(Session["year"].ToString());
            //int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();

            #endregion-End of Head part declearation

            #region ---------- save ----------
            if (isValidated == true)
            {
                // csCalculations();

                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    if (ViewState["mode"] != null)
                    {
                        DataSet ds = new DataSet();

                        if (ViewState["mode"].ToString() == "I")
                        {
                            obj.flag = 1;
                            obj.tableName = tblHead;
                            obj.columnNm = "Doc_No,Brand_Name,Packing_Code,Hamali_Code,Company_Code,Created_By";
                            obj.values = "'" + DOC_NO + "','" + BrandName + "','" + Packing_Code + "','" + Hamali_Code + "','"
                                + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + Created_By + "'";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                        }
                        if (ViewState["mode"].ToString() == "U")
                        {
                            obj.flag = 2;
                            obj.tableName = tblHead;
                            obj.columnNm = " Brand_Name='" + BrandName + "',Packing_Code='" + Packing_Code + "',Hamali_Code='" + Hamali_Code + "',Modified_By='"
                                + Modified_By + "' where Doc_No='" + txtdoc_no.Text + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRev);
                            retValue = strRev;
                        }
                        if (retValue == "-1")
                        {
                            hdnf.Value = txtdoc_no.Text;
                            clsButtonNavigation.enableDisable("S");
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                            qry = getDisplayQuery();
                            this.fetchRecord(qry);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Added!');", true);
                        }
                        if (retValue == "-2" || retValue == "-3")
                        {
                            hdnf.Value = txtdoc_no.Text;
                            clsButtonNavigation.enableDisable("S");
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                            qry = getDisplayQuery();
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
    #endregion

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
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                                                setFocusControl(txtBrand_Name);
                                                //txtDoc_no.Enabled = false;

                                                btnSave.Enabled = true;   //IMP

                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = "select * from " + tblHead + " where Doc_No=" + hdnf.Value +
                                                   " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());


                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    setFocusControl(txtBrand_Name);

                                                    //   hdnf.Value = txtdoc_no.Text;
                                                    //   hdnfSuffix.Value = txtSUFFIX.Text.Trim();
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtBrand_Name);
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

            #region[packing]

            if (strTextBox == "txtPacking_Code")
            {
                string packingname = "";
                if (txtPacking_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtPacking_Code.Text);
                    if (a == false)
                    {
                        btntxtPacking_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        packingname = clsCommon.getString("select System_Name_E from " + SystemMaster + " where System_Code="
                        + txtPacking_Code.Text + " and System_Type='P' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (packingname != string.Empty)
                        {

                            lblPackingCode_Name.Text = packingname;

                            setFocusControl(txtHamali_Code);


                        }
                        else
                        {
                            txtPacking_Code.Text = string.Empty;
                            lblPackingCode_Name.Text = packingname;
                            setFocusControl(txtPacking_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtPacking_Code);
                }
            }
            #endregion

            #region[Hamali]

            if (strTextBox == "txtHamali_Code")
            {
                string Hamaliname = "";
                if (txtHamali_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtHamali_Code.Text);
                    if (a == false)
                    {
                        btntxtHamali_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        Hamaliname = clsCommon.getString("select System_Name_E from " + SystemMaster + " where System_Code="
                        + txtHamali_Code.Text + " and System_Type='H' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (Hamaliname != string.Empty)
                        {

                            lblHamaliCode_Name.Text = Hamaliname;

                            setFocusControl(btnSave);


                        }
                        else
                        {
                            txtHamali_Code.Text = string.Empty;
                            lblHamaliCode_Name.Text = Hamaliname;
                            setFocusControl(txtHamali_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtHamali_Code);
                }
            }
            #endregion

        }
        catch
        {

        }
    }
    #endregion

    protected void txtPacking_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPacking_Code.Text;
        strTextBox = "txtPacking_Code";
        csCalculations();

    }

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

    protected void txtHamali_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali_Code.Text;
        strTextBox = "txtHamali_Code";
        csCalculations();

    }

    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        {
            try
            {
                bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
                if (a == false)
                {
                    searchString = txtEditDoc_No.Text;
                    strTextBox = "txtEditDoc_No";
                    pnlPopup.Style["display"] = "block";
                    hdnfClosePopup.Value = "txtEditDoc_No";
                    btnSearch_Click(this, new EventArgs());
                }
                else
                {
                    hdnf.Value = txtEditDoc_No.Text;
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
    }

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + ") " +
                "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY Doc_No DESC  ";
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
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " ORDER BY Doc_No asc  ";
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead
                + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") " +
                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
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
            string qryDisplay = "select * from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Doc_No=" + hdnf.Value;
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
                txtSearchText.Text = searchString;
            }
            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                lblPopupHead.Text = "--Select City--";
                string qry = "select [Doc_No],[Brand_Name],[PackingCode_Name],[HamaliCode_Name] from " + qryCommon + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and (Brand_Name like '%" + txtSearchText.Text + "%' or Doc_No like '%"
                    + txtSearchText.Text + "%' or PackingCode_Name like '%" + txtSearchText.Text + "%' or HamaliCode_Name like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }

            //if (hdnfClosePopup.Value == "txtPacking_Code")
            //{
            //    lblPopupHead.Text = "--Select Code--";
            //    string qry = "select System_Code,System_Name_E,System_Rate from " + SystemMaster + " where System_Type='P' and Company_Code="
            //        + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (System_Name_E like '%" + txtSearchText.Text + "%' or System_Code like '%"
            //        + txtSearchText.Text + "%')";
            //    this.showPopup(qry);
            //}

            //if (hdnfClosePopup.Value == "txtHamali_Code")
            //{
            //    lblPopupHead.Text = "--Select Code--";
            //    string qry = "select System_Code,System_Name_E,System_Rate from " + SystemMaster + " where System_Type='H' and Company_Code="
            //        + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (System_Name_E like '%" + txtSearchText.Text + "%' or System_Code like '%"
            //        + txtSearchText.Text + "%')";
            //    this.showPopup(qry);
            //}

            if (hdnfClosePopup.Value == "txtHamali_Code")
            {
                lblPopupHead.Text = "--Select Hamali Code--";
                string qry = "select [System_Code],[System_Name_E],[System_Rate] from " + SystemMaster + " where  (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' or System_Rate like '%" + txtSearchText.Text + "%') and System_Type='H' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString());
                //string qry = "select System_Code,System_Name_E as Item_Name from " + systemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtPacking_Code")
            {
                lblPopupHead.Text = "--Select Packing Code--";
                string qry = "select [System_Code],[System_Name_E],[System_Rate] from " + SystemMaster + " where (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%' or System_Rate like '%" + txtSearchText.Text + "%') and System_Type='P' and Company_Code="
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

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType != DataControlRowType.Pager)
            {

                e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
            }
            if (v == "txtDoc_No")
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(10);


            }


        }
        catch
        {

        }
    }
    #endregion
}