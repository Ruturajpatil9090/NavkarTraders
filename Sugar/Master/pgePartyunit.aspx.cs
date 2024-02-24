using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

public partial class pgePartyunit : System.Web.UI.Page
{
    #region data section
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
    string accountmasterlist = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;

    string cs = string.Empty;
    string Action = string.Empty;
    #endregion

    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    StringBuilder Head_Update = null;

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;

    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    #region text and label data Declaration
    Int32 Unit_Code = 0;
    Int32 Ac_Code = 0;
    Int32 Unit_name = 0;
    string Remark = string.Empty;

    Int32 Doc_No = 0;
    Int32 ucid = 0;
    Int32 ac = 0;
    Int32 uc = 0;
    int counts = 0;
    int count = 0;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "PartyUnit";
            tblDetails = tblPrefix + "";
            AccountMasterTable = "qrymstaccountmaster";
            qryCommon = "qrypartyunit";
            pnlPopup.Style["display"] = "none";
            accountmasterlist = "qrymstaccountmaster";
            cityMasterTable = tblPrefix + "CityMaster";
            pnlPopup.Style["display"] = "none";
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);

            Head_Fields = new StringBuilder();
            Head_Values = new StringBuilder();
            Head_Update = new StringBuilder();

            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    //pnlPopup.Style["display"] = "none";
                    //ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    //this.makeEmptyForm("N");
                    //ViewState["mode"] = "I";
                    //this.showLastRecord();


                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["unitid"];

                        //ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        pnlPopup.Style["display"] = "none";
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        setFocusControl(btnEdit);
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtac_code);
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
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                obj.code = "unit_code";
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
                // btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                txtEditDoc_No.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;

                #region logic
                lblParty_name.Text = string.Empty;
                btntxtac_code.Enabled = false;
                lblCityName.Text = string.Empty;
                btntxtcity_code.Enabled = false;
                lblUnitName.Text = string.Empty;
                btntxtunit_name.Enabled = false;
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
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = true;
                txtEditDoc_No.Enabled = false;
                #region set Business logic for save

                #region logic
                lblParty_name.Text = string.Empty;
                btntxtac_code.Enabled = true;
                lblCityName.Text = string.Empty;
                btntxtcity_code.Enabled = true;
                lblUnitName.Text = string.Empty;
                btntxtunit_name.Enabled = true;
                #endregion

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
                txtEditDoc_No.Enabled = true;
                #region logic

                btntxtac_code.Enabled = false;
                btntxtcity_code.Enabled = false;
                btntxtunit_name.Enabled = false;
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
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;

                #region logic

                btntxtac_code.Enabled = true;
                btntxtcity_code.Enabled = true;
                btntxtunit_name.Enabled = true;
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
            qry = "select max(unit_code) as doc_no from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                        hdnf.Value = dt.Rows[0]["doc_no"].ToString();
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
                }
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
        //pnlPopupDetails.Style["display"] = "none";
        txtdoc_no.Enabled = false;
        setFocusControl(txtac_code);
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        //pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtdoc_no.Enabled = false;
        setFocusControl(txtac_code);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {


            if (hdconfirm.Value == "Yes")
            {
                string str = clsCommon.getString("select Ac_Code from nt_1_carporatesale where Ac_Code=" + txtac_code.Text + "");
                if (str == "0")   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtdoc_no.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where unit_code='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                    dr = null;
                    dr = Maindt.NewRow();
                    dr["Querys"] = qry;
                    Maindt.Rows.Add(dr);

                    string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
                    if (msg == "Delete")
                    {
                        Response.Redirect("../Master/pgePartyunitUtility.aspx");
                    }


                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                   
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                hdnf.Value = txtdoc_no.Text;
                showLastRecord();
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

        clsButtonNavigation.enableDisable("S");

        this.makeEmptyForm("S");
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
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                      
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtdoc_no.Text = dt.Rows[0]["unit_code"].ToString();
                        txtac_code.Text = dt.Rows[0]["AC_CODE"].ToString();
                        lblParty_name.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        txtunit_name.Text = dt.Rows[0]["Unit_name"].ToString();
                        lblUnitName.Text = dt.Rows[0]["unitname"].ToString();
                        lblbsid.Text = dt.Rows[0]["ucid"].ToString();
                        txtremarks.Text = dt.Rows[0]["Remarks"].ToString();
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
                        //pnlgrdDetail.Enabled = false;
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

                            string qry = "select * from " + tblHead + " where  unit_code='" + txtValue + "' " +
                                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Record Found
                                        hdnf.Value = dt.Rows[0]["unit_code"].ToString();

                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** unit_code (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                //txtdoc_no.Enabled = false;
                                                //  hdnf.Value = txtdoc_no.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                setFocusControl(txtac_code);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    setFocusControl(txtac_code);
                                                    hdnf.Value = txtdoc_no.Text;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtac_code);
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
            if (strTextBox == "txtac_code")
            {
                string acname = "";
                if (txtac_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtac_code.Text);
                    if (a == false)
                    {
                        btntxtac_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where carporate_party='Y' and Ac_Code=" + txtac_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {

                            lblParty_name.Text = acname;
                            setFocusControl(txtunit_name);
                        }
                        else
                        {
                            txtac_code.Text = string.Empty;
                            lblParty_name.Text = acname;
                            setFocusControl(txtac_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtac_code);
                }
            }
            if (strTextBox == "txtPartycode")
            {

                string acname = "";
                if (txtPartycode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtPartycode.Text);
                    if (a == false)
                    {
                        btnGetparty_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where carporate_party='Y' and Ac_Code=" + txtac_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {

                            lblpartyname.Text = acname;
                        }
                        else
                        {
                            txtPartycode.Text = string.Empty;
                            lblpartyname.Text = acname;
                        }
                    }
                }
                else
                {
                    setFocusControl(txtPartycode);
                }
            }

            if (strTextBox == "txtunit_name")
            {
                string acname = "";
                if (txtunit_name.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtunit_name.Text);
                    if (a == false)
                    {
                        btntxtunit_name_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtunit_name.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {

                            lblUnitName.Text = acname;
                            setFocusControl(txtremarks);
                        }
                        else
                        {
                            txtunit_name.Text = string.Empty;
                            lblUnitName.Text = acname;
                            setFocusControl(txtunit_name);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtunit_name);
                }
            }
            if (strTextBox == "txtunit_address")
            {
                setFocusControl(txtcity_code);
            }
            if (strTextBox == "txtcity_code")
            {
                if (txtcity_code.Text != string.Empty)
                {
                    string cityName = clsCommon.getString("select city_name_e from " + cityMasterTable + " where city_code=" + txtcity_code.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (cityName != string.Empty && cityName != "0")
                    {
                        lblCityName.Text = cityName;
                        setFocusControl(txtvat_no);
                    }
                    else
                    {
                        txtcity_code.Text = string.Empty;
                        lblCityName.Text = string.Empty;
                        setFocusControl(txtcity_code);
                    }

                }
                else
                {

                }
            }
            if (strTextBox == "txtvat_no")
            {
                setFocusControl(txtecc_no);
            }
            if (strTextBox == "txtecc_no")
            {
                setFocusControl(txtperson1);
            }
            if (strTextBox == "txtperson1")
            {
                setFocusControl(txtperson1_mobile);
            }
            if (strTextBox == "txtperson1_mobile")
            {
                setFocusControl(txtperson2);
            }
            if (strTextBox == "txtperson2")
            {
                setFocusControl(txtperson2_mobile);
            }
            if (strTextBox == "txtperson2_mobile")
            {
                setFocusControl(txtremarks);
            }
            if (strTextBox == "txtremarks")
            {
                setFocusControl(btnSave);
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
            string qryDisplay = "select * from " + qryCommon + " where unit_code=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            if (v != "txtcity_code")
            {
                e.Row.Cells[2].Width = new Unit("80px");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
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

    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();
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

    #region [txtac_code_TextChanged]
    protected void txtac_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtac_code.Text;
        strTextBox = "txtac_code";
        csCalculations();
    }
    #endregion


    #region [btntxtac_code_Click]
    protected void btntxtac_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtac_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtcity_code_Click]
    protected void btntxtcity_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtcity_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtunit_name_Click]
    protected void btntxtunit_name_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtunit_name";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtunit_name_TextChanged]
    protected void txtunit_name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtunit_name.Text;
        strTextBox = "txtunit_name";
        csCalculations();
    }
    #endregion

    #region [txtunit_address_TextChanged]
    protected void txtunit_address_TextChanged(object sender, EventArgs e)
    {
        searchString = txtunit_address.Text;
        strTextBox = "txtunit_address";
        csCalculations();
    }
    #endregion

    #region [txtcity_code_TextChanged]
    protected void txtcity_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcity_code.Text;
        strTextBox = "txtcity_code";
        csCalculations();
    }
    #endregion

    #region [txtvat_no_TextChanged]
    protected void txtvat_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtvat_no.Text;
        strTextBox = "txtvat_no";
        csCalculations();
    }
    #endregion

    #region [txtecc_no_TextChanged]
    protected void txtecc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtecc_no.Text;
        strTextBox = "txtecc_no";
        csCalculations();
    }
    #endregion

    #region [txtperson1_TextChanged]
    protected void txtperson1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtperson1.Text;
        strTextBox = "txtperson1";
        csCalculations();
    }
    #endregion

    #region [txtperson1_mobile_TextChanged]
    protected void txtperson1_mobile_TextChanged(object sender, EventArgs e)
    {
        searchString = txtperson1_mobile.Text;
        strTextBox = "txtperson1_mobile";
        csCalculations();
    }
    #endregion

    #region [txtperson2_TextChanged]
    protected void txtperson2_TextChanged(object sender, EventArgs e)
    {
        searchString = txtperson2.Text;
        strTextBox = "txtperson2";
        csCalculations();
    }
    #endregion

    #region [txtperson2_mobile_TextChanged]
    protected void txtperson2_mobile_TextChanged(object sender, EventArgs e)
    {
        searchString = txtperson2_mobile.Text;
        strTextBox = "txtperson2_mobile";
        csCalculations();
    }
    #endregion

    #region [txtremarks_TextChanged]
    protected void txtremarks_TextChanged(object sender, EventArgs e)
    {
        searchString = txtremarks.Text;
        strTextBox = "txtremarks";
        csCalculations();
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
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

            //    if (btntxtdoc_no.Text == "Choose No")
            //    {
            //        lblPopupHead.Text = "--Select Unit--";
            //        string qry = "select unit_code as Doc_No,Ac_Code as Party_Code,partyName as Party,UnitName as Unit from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            //            " and (unit_code like '%" + txtSearchText.Text + "%' or Unit_name like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' )";
            //        this.showPopup(qry);
            //    }

            //}
            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                lblPopupHead.Text = "--Select City--";
                string qry = "select unit_code as Doc_No,Ac_Code as Party_Code,Ac_Name_E as Party,unitname as Unit from " + qryCommon + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (unit_code like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or unitname like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtac_code")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + accountmasterlist + " where carporate_party='Y' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%"
                    + txtSearchText.Text + "%'or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtPartycode")
            {
                lblPopupHead.Text = "--Select Party--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + accountmasterlist + " where carporate_party='Y' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtcity_code")
            {
                lblPopupHead.Text = "--Select City--";
                string qry = "select city_code,city_name_e as CityName from " + cityMasterTable + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and (city_code like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%'  )  order by city_name_e";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtunit_name")
            {

                //lblPopupHead.Text = "--Select Unit--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from  " + accountmasterlist + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);

                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + accountmasterlist + " where  Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%"
                    + txtSearchText.Text + "%'or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
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
            if (hdnfClosePopup.Value == "txtac_code")
            {
                setFocusControl(txtac_code);
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
        try
        {
            #region [Validation Part]
            bool isValidated = true;
            if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
                return;

            }
            if (txtdoc_no.Text != string.Empty)
            {
                if (ViewState["mode"] != null)
                {
                    if (ViewState["mode"].ToString() == "I")
                    {
                        string str = clsCommon.getString("select Unit_name from " + tblHead + " where unit_code=" + txtdoc_no.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (str != "0")
                        {
                            // lblMsg.Text = "Code " + txtdoc_no.Text + " already exist";
                            this.NextNumber();

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
            }

            if (txtac_code.Text != string.Empty)
            {
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where carporate_party='Y' and Ac_Code=" + txtac_code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty && str != "0")
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtac_code);
                    return;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtac_code);
                return;
            }

            if (txtunit_name.Text != string.Empty)
            {
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtunit_name.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty && str != "0")
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtunit_name);
                    return;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtunit_name);
                return;
            }

            #endregion
            #region -Head part declearation
            Int32 DOC_NO = txtdoc_no.Text != string.Empty ? Convert.ToInt32(txtdoc_no.Text) : 0;
            ucid = lblbsid.Text != string.Empty ? Convert.ToInt32(lblbsid.Text) : 0;
            Int32 AC_CODE = txtac_code.Text != string.Empty ? Convert.ToInt32(txtac_code.Text) : 0;

            if (txtac_code.Text != string.Empty && txtac_code.Text != "0")
            {
                ac = Convert.ToInt32(clsCommon.getString("select accoid from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + ""));
            }
            else
            {
                ac = 0;
            }
            Int32 UNIT_NAME = txtunit_name.Text != string.Empty ? Convert.ToInt32(txtunit_name.Text) : 0;
            if (txtunit_name.Text != string.Empty && txtunit_name.Text != "0")
            {
                uc = Convert.ToInt32(clsCommon.getString("select accoid from " + AccountMasterTable + " where Ac_Code=" + txtunit_name.Text + " and Company_Code=" + Session["Company_Code"].ToString() + ""));

            }
            else
            {
                uc = 0;
            }
            string REMARKS = txtremarks.Text;

            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            int year_Code = Convert.ToInt32(Session["year"].ToString());
            int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");


            int flag = 0;
            int no = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion-End of Head part declearation
            if (btnSave.Text == "Save")
            {
                this.NextNumber();
                Head_Fields.Append("unit_code,");
                Head_Values.Append("'" + DOC_NO + "',");
                Head_Fields.Append("Ac_Code,");
                Head_Values.Append("'" + AC_CODE + "',");
                Head_Fields.Append("Unit_name,");
                Head_Values.Append("'" + UNIT_NAME + "',");
                Head_Fields.Append("Remarks,");
                Head_Values.Append("'" + REMARKS + "',");
                Head_Fields.Append("Company_Code,");
                Head_Values.Append("'" + Company_Code + "',");
                Head_Fields.Append("Year_Code,");
                Head_Values.Append("'" + Year_Code + "',");
                Head_Fields.Append("Created_By,");
                Head_Values.Append("'" + Created_By + "',");
                Head_Fields.Append("ac,");
                Head_Values.Append("'" + ac + "',");
                Head_Fields.Append("uc");
                Head_Values.Append("'" + uc + "'");
            //    Head_Fields.Append("ucid");
              //  Head_Values.Append("'" + ucid + "' ");


                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";
                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = qry;
                Maindt.Rows.Add(dr);

            }
            else
            {
                #region Create Update Query
                Head_Update.Append("Ac_Code=");
                Head_Update.Append("'" + AC_CODE + "',");
                Head_Update.Append("Unit_name=");
                Head_Update.Append("'" + UNIT_NAME + "',");
                Head_Update.Append("Remarks=");
                Head_Update.Append("'" + REMARKS + "',");
                Head_Update.Append("ac=");
                Head_Update.Append("'" + ac + "',");
                Head_Update.Append("uc=");
                Head_Update.Append("'" + uc + "',");
                Head_Update.Append("Modified_By=");
                Head_Update.Append("'" + Modified_By + "'");
                #endregion

                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and unit_code='" + txtdoc_no.Text + "'";
                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = qry;
                Maindt.Rows.Add(dr);





            }
            string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);

            if (msg == "Insert")
            {
                hdnf.Value = ucid.ToString();
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
            }
            else if (msg == "Update")
            {

                hdnf.Value = lblbsid.Text;
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);

            }
        }
        catch
        {

        }





    }
    #endregion
    private int DataStore(string Query, int flag)
    {
        int count = 0;
        try
        {
            //Connection open
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            ///Execution
            myTran = con.BeginTransaction();
            //cmd.CommandText = qry;
            //cmd.Connection = con;
            //cmd.Transaction = myTran;
            cmd = new SqlCommand(Query, con, myTran);

            cmd.ExecuteNonQuery();
            myTran.Commit();
            Thread.Sleep(100);
            if (flag == 1)
            {
                count = 1;
            }
            else if (flag == 2)
            {
                count = 2;
            }
            else
            {
                count = 3;
            }

            return count;
        }
        catch
        {
            if (myTran != null)
            {
                myTran.Rollback();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);

            }
            return count;

        }
        finally
        {
            con.Close();
        }

    }
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string partycode = txtPartycode.Text;
            if (string.IsNullOrEmpty(txtPartycode.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "au", "javascript:au()", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "pd", "javascript:pd('" + partycode + "')", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void txtPartycode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPartycode.Text;
        strTextBox = "txtPartycode";
        csCalculations();
    }
    protected void btnGetparty_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPartycode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(unit_code) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            if (counts == 0)
            {
                txtdoc_no.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(unit_code) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' ")) + 1;
                txtdoc_no.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(ucid) as ucid from " + tblHead));
            if (counts == 0)
            {
                lblbsid.Text = "1";
                ucid = 1;
            }
            else
            {
                ucid = Convert.ToInt32(clsCommon.getString("SELECT max(ucid) as ucid from " + tblHead)) + 1;
                lblbsid.Text = ucid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
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
                searchString = txtEditDoc_No.Text;
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                string docno = clsCommon.getString("select ucid from " + tblHead + " where unit_code =" + txtEditDoc_No.Text + " and Company_Code="
        + Session["Company_Code"].ToString());
                hdnf.Value = docno;
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

