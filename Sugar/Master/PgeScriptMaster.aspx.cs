using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;

public partial class Sugar_Master_PgeScriptMaster : System.Web.UI.Page
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
    int Rowaction = 2;
    int Srno = 3;
    string Action = string.Empty;
    #endregion
    #region
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(btnAdd);
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Script_Master";
            tblDetails = "";
            qryCommon = "";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";

            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["FNO_Script_No"];

                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                    }
                    else
                    {
                        string FNO_Script_No = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        //this.NextNumber();
                        setFocusControl(txtScript_Name);
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
                                    txtScript_Code.Text = ds.Tables[0].Rows[0][0].ToString();
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
                //btnAdddetails.Enabled = false;
                // btnClosedetails.Enabled = false;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtScript_Code.Text = "Choose No";
                btntxtScript_Code.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtScript_Name.Enabled = false;
                drpScript_Type.Enabled = false;
                txtLot_Size.Enabled = false;
                txtFace_Value.Enabled = false;
                txtScript_Id.Enabled = false;
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
                btntxtScript_Code.Text = "Change No";
                btntxtScript_Code.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtScript_Code.Enabled = false;
                // btnAdddetails.Enabled = true;
                // btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                txtScript_Name.Enabled = true;
                drpScript_Type.Enabled = true;
                txtLot_Size.Enabled = true;
                txtFace_Value.Enabled = true;
                txtScript_Id.Enabled = true;
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
                btntxtScript_Code.Text = "Choose No";
                btntxtScript_Code.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtScript_Name.Enabled = false;
                drpScript_Type.Enabled = false;
                txtLot_Size.Enabled = false;
                txtFace_Value.Enabled = false;
                txtScript_Id.Enabled = false;
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
                btntxtScript_Code.Text = "Choose No";
                btntxtScript_Code.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtScript_Name.Enabled = true;
                drpScript_Type.Enabled = false;
                txtLot_Size.Enabled = true;
                txtFace_Value.Enabled = true;
                txtScript_Id.Enabled = true;
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

        //if (ViewState["mode"].ToString() == "U")
        //{

        int RecordCount = 0;

        string query = "";
        query = "select count(*) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
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

        if (txtScript_Code.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [FNO_Script_No] from " + tblHead + " where FNO_Script_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY FNO_Script_No asc  ";
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

                query = "SELECT top 1 [FNO_Script_No] from " + tblHead + " where FNO_Script_No<" + Convert.ToInt32(hdnf.Value) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY FNO_Script_No asc  ";
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

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select FNO_Script_No from " + tblHead + " where FNO_Script_No=(select MIN(FNO_Script_No) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
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
            if (txtScript_Code.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [FNO_Script_No] from " + tblHead + " where FNO_Script_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY FNO_Script_No desc  ";
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
            if (txtScript_Code.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [FNO_Script_No] from " + tblHead + " where FNO_Script_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY FNO_Script_No asc  ";
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
            query = "select FNO_Script_No from " + tblHead + " where FNO_Script_No=(select MAX(FNO_Script_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
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
        string check = txtEditDoc_No.Text;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
            setFocusControl(txtScript_Name);
            Int32 FNO_Script_No = Convert.ToInt32(clsCommon.getString("select IDENT_CURRENT('" + tblHead + "') as FNO_Script_No"));
            if (FNO_Script_No != 0)
            {
                int doc_no = FNO_Script_No + 1;
                FNO_Script_No = doc_no;
            }
            else
            {
                FNO_Script_No = 1;
            }
            txtScript_Code.Text = Convert.ToString(FNO_Script_No);
        }
        else
        {
            btnCancel_Click(this, new EventArgs());
        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
        txtScript_Code.Enabled = false;
        setFocusControl(txtScript_Name);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string str = string.Empty;
                DataSet xml_ds = new DataSet();
                if (str == string.Empty)
                {
                    string currentDoc_No = txtScript_Code.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                        string trantype = drpScript_Type.SelectedValue;
                        string strRev = string.Empty;
                        child1.SetAttributeValue("FNO_Script_No", txtScript_Code.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);
                        child1.SetAttributeValue("Year_Code", Year_Code);
                        child1.SetAttributeValue("Tran_Type", trantype);
                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_Script_Master";
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
                    //    query = "SELECT top 1 [FNO_Script_No] from " + tblHead + " where FNO_Script_No>" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY FNO_Script_No asc  ";
                    //    hdnf.Value = clsCommon.getString(query);
                    //    if (hdnf.Value == string.Empty)
                    //    {
                    //        query = "SELECT top 1 [FNO_Script_No] from " + tblHead + " where FNO_Script_No<" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY FNO_Script_No desc  ";
                    //        hdnf.Value = clsCommon.getString(query);
                    //    }
                    //    if (hdnf.Value != string.Empty)
                    //    {
                    //        query = getDisplayQuery();
                    //        bool recordExist = this.fetchRecord(query);
                    //        this.makeEmptyForm("S");
                    //        clsButtonNavigation.enableDisable("S");
                    //    }
                    //    else
                    //    {
                    //        this.makeEmptyForm("N");
                    //        clsButtonNavigation.enableDisable("N");
                    //        btnEdit.Enabled = false;
                    //        btnDelete.Enabled = false;
                    //    }
                    }
                    //this.enableDisableNavigateButtons();
                    Response.Redirect("../Master/PgeScriptMasterUtility.aspx");
                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
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
        hdnf.Value = clsCommon.getString("select max(FNO_Script_No) from " + tblHead + " where Company_Code=" + Session["Company_Code"].ToString() + " and " +
              " Year_Code=" + Session["year"].ToString() + "");


        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry); 
        //if (hdnf.Value != string.Empty)
        //{
        //    string query = getDisplayQuery();
        //    bool recordExist = this.fetchRecord(query);
        //}
        //else
        //{
        //    this.showLastRecord();
        //}
        //string qry = clsCommon.getString("select count(FNO_Script_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        //if (qry != "0")
        //{
        //    clsButtonNavigation.enableDisable("S");
        //    this.makeEmptyForm("S");
        //    this.enableDisableNavigateButtons();
        //}
        //else
        //{
        //    clsButtonNavigation.enableDisable("N");
        //    this.makeEmptyForm("N");
        //    this.enableDisableNavigateButtons();
        //    btnEdit.Enabled = false;
        //    btnDelete.Enabled = false;
        //}
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
                                lblCreatedDate.Text = "";
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
                        //  txtEditDoc_No.Text = dt.Rows[0]["EditDoc_No"].ToString();
                        txtScript_Code.Text = dt.Rows[0]["FNO_Script_No"].ToString();
                        // lblScript_Code.Text = dt.Rows[0][""].ToString();
                        txtScript_Name.Text = dt.Rows[0]["Script_Name"].ToString();
                        drpScript_Type.SelectedValue = dt.Rows[0]["FNO_Type"].ToString();
                        txtLot_Size.Text = dt.Rows[0]["Lot_Size"].ToString();
                        txtFace_Value.Text = dt.Rows[0]["Face_Value"].ToString();
                        txtScript_Id.Text = dt.Rows[0]["Script_Id"].ToString();

                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            hdnf.Value = txtScript_Code.Text;
            this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion
    #region csCalculations
    private void
        csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_No")
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
                        txtScript_Code.Enabled = false;
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
            if (strTextBox == "txtScript_Code")
            {
                setFocusControl(txtScript_Code);
            }
            if (strTextBox == "txtScript_Name")
            {
                setFocusControl(txtScript_Name);
            }
            if (strTextBox == "drpFNO_Type")
            {
                setFocusControl(drpScript_Type);
            }
            if (strTextBox == "txtLot_Size")
            {
                setFocusControl(txtLot_Size);
            }
            if (strTextBox == "txtFace_Value")
            {
                setFocusControl(txtFace_Value);
            }
            if (strTextBox == "txtScript_Id")
            {
                setFocusControl(txtScript_Id);
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
            string qryDisplay = " select * from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and FNO_Script_No=" + hdnf.Value + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                txtScript_Code.Text = hdnf.Value;
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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.Pager)
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
    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                txtSearchText.Text = txtEditDoc_No.Text.ToString();
                strTextBox = "txtScript_Code";
                btntxtScript_Code_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry = "select * from " + tblHead + " where FNO_Script_No='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region [txtScript_Code_TextChanged]
    protected void txtScript_Code_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtScript_Code.Text;
        //strTextBox = "txtScript_Code";
        //csCalculations();

        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtScript_Code.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtScript_Code.Text != string.Empty)
                {
                    txtValue = txtScript_Code.Text;

                    string qry = "select * from " + tblHead + " where  FNO_Script_No='" + txtScript_Code.Text + "' " +
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
                                hdnf.Value = dt.Rows[0]["FNO_Script_No"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtScript_Name);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtScript_Code.Enabled = false;
                                            setFocusControl(txtScript_Name);
                                            clsButtonNavigation.enableDisable("E");
                                            hdnf.Value = txtScript_Code.Text;
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
                                    setFocusControl(txtScript_Name);
                                    txtScript_Code.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtScript_Code.Text = string.Empty;
                                    setFocusControl(txtScript_Code);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtScript_Code);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtScript_Code.Text = string.Empty;
                setFocusControl(txtScript_Code);
            }
        }
        catch
        {

        }
        #endregion
    }
    #endregion
    #region [btntxtScript_Code_Click]
    protected void btntxtScript_Code_Click(object sender, EventArgs e)
    {


        try
        {

            if (btntxtScript_Code.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtScript_Name_TextChanged]
    protected void txtScript_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtScript_Name.Text;
        strTextBox = "txtScript_Name";
        csCalculations();
    }
    #endregion
    #region [txtFace_Value_TextChanged]
    protected void txtFace_Value_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFace_Value.Text;
        strTextBox = "txtFace_Value";
        csCalculations();
    }
    #endregion
    #region [txtScript_Id_TextChanged]
    protected void txtScript_Id_TextChanged(object sender, EventArgs e)
    {
        searchString = txtScript_Id.Text;
        strTextBox = "txtScript_Id";
        csCalculations();
    }
    #endregion
    #region [drpScript_Type_SelectedIndexChanged]
    protected void drpScript_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtLot_Size_TextChanged]
    protected void txtLot_Size_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLot_Size.Text;
        strTextBox = "txtLot_Size";
        csCalculations();
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
            if (hdnfClosePopup.Value == "txtScript_Code" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtScript_Code.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtScript_Code.Text = string.Empty;
                    txtScript_Code.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtScript_Code);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtScript_Code.Text == "Choose No")
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
                        name += "( Script_Code like '%" + aa + "%' or Script_Name like '%" + aa + "%' or Face_Value like '%" + aa + "%' or Script_Id like '%" + aa + "%'  ) and";
                        // name += "Script_Code Like '%" + aa + "%'or";
                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select FNO_Script_No,Script_Name,Face_Value,Script_Id from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by FNO_Script_No";
                    this.showPopup(qry);
                }
            }

            //if (hdnfClosePopup.Value == "txtScript_Code")
            //{
            //    lblPopupHead.Text = "--Select--";
            //    string qry = " select Script_Code,Script_Name,Face_Value,Script_Id from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Script_Code";
            //    this.showPopup(qry);
            //}
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
        #region [Validation Part]
        bool isValidated = true;
        //        if textbox is date then if condition will be like this if(clsCommon.isValidDate(txtDoc_Date.Text==true))

        if (txtScript_Name.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtScript_Name);
            return;
        }
        if (txtLot_Size.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtLot_Size);
            return;
        }
        if (txtFace_Value.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtFace_Value);
            return;
        }
        if (txtScript_Id.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtScript_Id);
            return;
        }

        #endregion

        #region -Head part declearation

        XElement root = new XElement("ROOT");
        XElement child1 = new XElement("Head");
        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
        int Branch_Code = Convert.ToInt32(Convert.ToInt32(Session["Branch_Code"].ToString()));
        string Created_By = Session["user"].ToString();
        string Modified_By = Session["user"].ToString();
        string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string retValue = string.Empty;
        string strRev = string.Empty;
        #endregion-End of Head part declearation
        #region Save Head Part
        // child1.SetAttributeValue("EditDoc_No", txtEditDoc_No.Text != string.Empty ? txtEditDoc_No.Text : "0");
        //child1.SetAttributeValue("Script_Code", txtScript_Code.Text != string.Empty ? txtScript_Code.Text : "0");
        child1.SetAttributeValue("Script_Name", txtScript_Name.Text);
        child1.SetAttributeValue("FNO_Type", drpScript_Type.SelectedValue);
        child1.SetAttributeValue("Lot_Size", txtLot_Size.Text != string.Empty ? txtLot_Size.Text : "0");
        child1.SetAttributeValue("Face_Value", txtFace_Value.Text != string.Empty ? txtFace_Value.Text : "0");
        child1.SetAttributeValue("Script_Id", txtScript_Id.Text != string.Empty ? txtScript_Id.Text : "0");
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_Code", Year_Code);
        child1.SetAttributeValue("Branch_Code", Branch_Code);
        if (btnSave.Text != "Save")
        {
            child1.SetAttributeValue("Modified_By", Modified_By);
            child1.SetAttributeValue("Modified_Date", Modified_Date);
            child1.SetAttributeValue("FNO_Script_No", txtScript_Code.Text != string.Empty ? txtScript_Code.Text : "0");

        }
        else
        {
            child1.SetAttributeValue("Created_By", Created_By);
            child1.SetAttributeValue("Created_Date", Created_Date);
        }
        root.Add(child1);
        #endregion-End of Head part Save

        #region save Head Master
        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_Script_Master";
        string xmlfile = XMLReport;
        string op = "";
        string returnmaxno = "";
        int flag;
        if (btnSave.Text == "Save")
        {
            #region[Insert]
            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        else
        {
            #region[Update]
            flag = 2;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        txtScript_Code.Text = returnmaxno;
        hdnf.Value = txtScript_Code.Text;
        retValue = op;

        if (retValue == "-1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Added! No=" + returnmaxno + "');", true);
        }
        if (retValue == "-2" || retValue == "-3")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated! No=" + returnmaxno + "');", true);
        }
        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
        #endregion
    }
    #endregion
}


