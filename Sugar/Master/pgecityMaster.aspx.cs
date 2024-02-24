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

public partial class Sugar_pgecityMaster : System.Web.UI.Page
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
    string GLedgerTable = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
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
    Int32 City_Code = 0;
    string CityName = string.Empty;
    string Regional_name = string.Empty;
    Int32 PINCODE = 0;
    string StateDrP = string.Empty;
    string Subarea = string.Empty;
    string StateCode = string.Empty;
    double distance = 0.00;

    Int32 Cityid = 0;
    Int32 Doc_No = 0;
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
            tblHead = tblPrefix + "CityMaster";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "qryCityMaster";
            GLedgerTable = tblPrefix + "GLEDGER";
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

                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["cityid"];

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
                        setFocusControl(txtcityNameE);
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
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                obj.code = "city_code";
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
                btntxtGstStateCode.Enabled = false;
                btnSave.Text = "Save";
                btntxtDOC_NO.Text = "Choose No";
                txtEditDoc_No.Enabled = true;
                btntxtDOC_NO.Enabled = false;
                lblMsg.Text = string.Empty;

                drpState.Enabled = false;
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
                btntxtGstStateCode.Enabled = true;
                drpState.Enabled = true;
                btnSave.Text = "Save";
                btntxtDOC_NO.Text = "Change No";
                btntxtDOC_NO.Enabled = true;
                txtdoc_no.Enabled = false;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = "";
                lblGstStateName.Text = "";
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
                btntxtGstStateCode.Enabled = false;
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                drpState.Enabled = false;
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
                btntxtGstStateCode.Enabled = true;
                drpState.Enabled = true;
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = true;
                lblMsg.Text = string.Empty;
                txtEditDoc_No.Enabled = false;
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
                //  this.enableDisableNavigateButtons();
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
        this.NextNumber();
        txtdistance.Text = "0";
        setFocusControl(txtcityNameE);

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
        setFocusControl(txtcityNameE);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where city_code=" + txtdoc_no.Text + "");
                if (str == "0")   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtdoc_no.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where city_code='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                    dr = null;
                    dr = Maindt.NewRow();
                    dr["Querys"] = qry;
                    Maindt.Rows.Add(dr);
                    string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
                    if (msg == "Delete")
                    {
                        Response.Redirect("../Master/PgeCityMasterUtility.aspx");
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
        hdnf.Value = Request.QueryString["cityid"];
        // hdnfTranType.Value = Request.QueryString["System_Type"];

        hdnf.Value = clsCommon.getString("select max(cityid) from " + tblHead + " where Company_Code=" + Session["Company_Code"].ToString() + " ");

        Response.Redirect("pgecityMaster.aspx?cityid=" + hdnf.Value + "&Action=" + 1);

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
                        txtdoc_no.Text = dt.Rows[0]["city_code"].ToString();
                        txtcityNameE.Text = dt.Rows[0]["city_name_e"].ToString();
                        txtPincode.Text = dt.Rows[0]["pincode"].ToString();
                        txtSubArea.Text = dt.Rows[0]["Sub_Area"].ToString();
                        txtcityNameR.Text = dt.Rows[0]["city_name_r"].ToString();
                        txtdistance.Text = dt.Rows[0]["Distance"].ToString();
                        txtGstStateCode.Text = dt.Rows[0]["GstStateCode"].ToString();
                        lblGstStateName.Text = dt.Rows[0]["gststatename"].ToString();
                        lblbsid.Text = dt.Rows[0]["city_code"].ToString();
                        drpState.Text = dt.Rows[0]["state"].ToString();
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
                string docno = clsCommon.getString("select cityid from " + tblHead + " where city_code =" + txtEditDoc_No.Text + " and Company_Code="
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

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = "select * from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and city_code=" + hdnf.Value;
            //return qryDisplay;

            string qryDisplay = "select * from qrycitymaster  where cityid =" + hdnf.Value;
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
            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                lblPopupHead.Text = "--Select City--";
                string qry = "select city_code,city_name_e from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (city_name_e like '%" + txtSearchText.Text + "%' or city_code like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGstStateCode")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( State_Code like '%" + aa + "%' or State_Name like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "select State_Code,State_Name from GSTStateMaster where (State_Name like '%" + txtSearchText.Text + "%' or State_Code like '%" + txtSearchText.Text + "%')";
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
        try
        {
            int gstno;
            if (btnSave.Text == "Save")
            {
                if (txtPincode.Text != string.Empty)
                {
                    gstno = Convert.ToInt32(clsCommon.getString("select COUNT(*) from " + tblHead + " where company_code=" + Session["Company_Code"].ToString() +
                      " and pincode='" + txtPincode.Text + "' and city_code!=" + txtdoc_no.Text));
                    if (gstno == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Pincode No is already Exists!')", true);
                        return;
                    }
                    else if (gstno > 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Pincode No is already Exists!')", true);
                        return;
                    }
                }
            }
            string qry = "";
            #region validation
            bool isValidated = true;

            if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
                return;

            }
            if (txtdoc_no.Text != string.Empty)
            {
                if (ViewState["mode"].ToString() == "I")
                {

                    string str = clsCommon.getString("select city_code from " + tblHead + " where  city_code='" + txtdoc_no.Text + "'" +
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
            if (txtcityNameE.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtcityNameE);
                return;
            }

            //if (txtPincode.Text != string.Empty)
            //{
            //    isValidated = true;
            //}
            //else
            //{
            //    isValidated = false;
            //    setFocusControl(txtPincode);
            //    return;
            //}
            //if (txtdistance.Text != string.Empty)
            //{
            //    isValidated = true;
            //}
            //else
            //{
            //    isValidated = false;
            //    setFocusControl(txtdistance);
            //    return;
            //}
            if (txtGstStateCode.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtGstStateCode);
                return;
            }
            #endregion

            #region -Head part declearation

            City_Code = txtdoc_no.Text != string.Empty ? Convert.ToInt32(txtdoc_no.Text) : 0;
            Cityid = lblbsid.Text != string.Empty ? Convert.ToInt32(lblbsid.Text) : 0;
            CityName = txtcityNameE.Text.ToUpper();
            PINCODE = txtPincode.Text != string.Empty ? Convert.ToInt32(txtPincode.Text) : 0;
            Subarea = txtSubArea.Text.ToUpper();
            Regional_name = txtcityNameR.Text;
            StateDrP = drpState.Text;

            distance = txtdistance.Text != string.Empty ? Convert.ToDouble(txtdistance.Text) : 0.00;
            StateCode = txtGstStateCode.Text;

            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            int year_Code = Convert.ToInt32(Session["year"].ToString());
            //int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();

            int flag = 0;
            int no = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion-Save Part
            if (btnSave.Text == "Save")
            {
                this.NextNumber();
                Head_Fields.Append("city_code,");
                Head_Values.Append("'" + City_Code + "',");
                Head_Fields.Append("city_name_e,");
                Head_Values.Append("'" + CityName + "',");
                Head_Fields.Append("pincode,");
                Head_Values.Append("'" + PINCODE + "',");
                Head_Fields.Append("Sub_Area,");
                Head_Values.Append("'" + Subarea + "',");
                Head_Fields.Append("city_name_r,");
                Head_Values.Append("'" + Regional_name + "',");
                Head_Fields.Append("state,");
                Head_Values.Append("'" + StateDrP + "',");
                Head_Fields.Append("Distance,");
                Head_Values.Append("'" + distance + "',");
                Head_Fields.Append("GstStateCode,");
                Head_Values.Append("'" + StateCode + "',");
                //Head_Fields.Append("cityid,");
                //Head_Values.Append("'" + Cityid + "',");
                Head_Fields.Append("Company_Code,");
                Head_Values.Append("'" + Company_Code + "' ,");
                Head_Fields.Append("Created_By");
                Head_Values.Append("'" + Created_By + "'");

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
                Head_Update.Append("city_name_e=");
                Head_Update.Append("'" + CityName + "',");
                Head_Update.Append("pincode=");
                Head_Update.Append("'" + PINCODE + "',");
                Head_Update.Append("Sub_Area=");
                Head_Update.Append("'" + Subarea + "',");
                Head_Update.Append("city_name_r=");
                Head_Update.Append("'" + Regional_name + "',");
                Head_Update.Append("state=");
                Head_Update.Append("'" + StateDrP + "',");
                Head_Update.Append("Distance=");
                Head_Update.Append("'" + distance + "',");
                Head_Update.Append("GstStateCode=");
                Head_Update.Append("'" + StateCode + "',");
                Head_Update.Append("Modified_By=");
                Head_Update.Append("'" + Modified_By + "'");
                #endregion

                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and city_code='" + txtdoc_no.Text + "'";
                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = qry;
                Maindt.Rows.Add(dr);


            }
            string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
            if (msg == "Insert")
            {
                hdnf.Value = counts.ToString();
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

                            string qry = "select * from " + tblHead + " where   city_code='" + txtValue + "' " +
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
                                        hdnf.Value = dt.Rows[0]["city_code"].ToString();

                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                setFocusControl(txtcityNameE);
                                                //txtDoc_no.Enabled = false;

                                                btnSave.Enabled = true;   //IMP

                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = "select * from " + tblHead + " where city_code=" + hdnf.Value +
                                                   " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());


                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    setFocusControl(txtcityNameE);

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
                                            setFocusControl(txtcityNameE);
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
            if (strTextBox == "txtGstStateCode")
            {
                string gststatecodetext = txtGstStateCode.Text.Trim();
                if (gststatecodetext != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(gststatecodetext);
                    if (a == false)
                    {
                        btntxtGstStateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        string gstStateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecodetext + "");
                        if (gstStateName != string.Empty && gstStateName != "0")
                        {
                            lblGstStateName.Text = gstStateName;
                            setFocusControl(btnSave);

                        }
                        else
                        {
                            txtGstStateCode.Text = string.Empty;
                            lblGstStateName.Text = string.Empty;


                        }
                    }
                }
                else
                {
                    lblGstStateName.Text = "";
                    setFocusControl(txtGstStateCode);
                    setFocusControl(btnSave);
                }
                return;
            }
            if (strTextBox == "txtcityNameE")
            {
                setFocusControl(txtcityNameR);
            }
            if (strTextBox == "txtcityNameR")
            {
                setFocusControl(drpState);
            }
            if (strTextBox == "txtState")
            {
                setFocusControl(txtGstStateCode);
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
    protected void txtcityNameE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcityNameE.Text;
        strTextBox = "txtcityNameE";
        csCalculations();
    }


    protected void txtcityNameR_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcityNameR.Text;
        strTextBox = "txtcityNameR";
        csCalculations();
    }

    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblGstStateName.Text = drpState.SelectedValue;
        string gstcode =  clsCommon.getString ("select State_Code from GSTStateMaster where (State_Name like '%" + lblGstStateName.Text + "%')");         
        txtGstStateCode.Text = gstcode;
        txtdistance.Text = "0";
        txtGstStateCode.Enabled = false;
        btntxtGstStateCode.Enabled = false;
        SetFocus(txtdistance);
    }

    #region [txtGstStateCode_TextChanged]
    protected void txtGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGstStateCode.Text;
        strTextBox = "txtGstStateCode";

        csCalculations();
    }
    #endregion

    #region [btntxtGstStateCode_Click]
    protected void btntxtGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    protected void txtdistance_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtGstStateCode);
    }

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(city_code) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            if (counts == 0)
            {
                txtdoc_no.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(city_code) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
                txtdoc_no.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(cityid) as bsid from " + tblHead + " "));
            if (counts == 0)
            {
                lblbsid.Text = "1";
                Cityid = 1;
            }
            else
            {
                Cityid = Convert.ToInt32(clsCommon.getString("SELECT max(cityid) as bsid from " + tblHead)) + 1;
                lblbsid.Text = Cityid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
}