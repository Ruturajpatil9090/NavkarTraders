using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
public partial class pgeBrand_Master : System.Web.UI.Page
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
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Brand_Master";
            tblDetails = "";
            qryCommon = " qryBrand_Master";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            setFocusControl(btnAdd);
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                  string  Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["BrandCode"];

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
                        //string docno = string.Empty;
                        //clsButtonNavigation.enableDisable("A");
                        //ViewState["mode"] = null;
                        //ViewState["mode"] = "I";
                        //this.makeEmptyForm("A");
                        btnAdd_Click(sender, e);
                        setFocusControl(txtMarka);
                    }
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
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
                                    txtCode.Text = ds.Tables[0].Rows[0][0].ToString();
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

                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtCode.Text = "Choose No";
                btntxtCode.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtMarka.Enabled = false;
                txtEnglish_Name.Enabled = false;
                txtMal_Code.Enabled = false;
                btntxtMal_Code.Enabled = false;
                txtAarambhi_Nag.Enabled = false;
                txtNagache_Vajan.Enabled = false;
                drpType.Enabled = false;
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
                btntxtCode.Text = "Change No";
                btntxtCode.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtCode.Enabled = false;

                ViewState["currentTable"] = null;
                txtMarka.Enabled = true;
                txtEnglish_Name.Enabled = true;
                txtMal_Code.Enabled = true;
                lblmal_code.Text = string.Empty;
                btntxtMal_Code.Enabled = true;
                txtAarambhi_Nag.Enabled = true;
                txtNagache_Vajan.Enabled = true;
                drpType.Enabled = true;
                btnBack.Enabled = false;
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
                btntxtCode.Text = "Choose No";
                btntxtCode.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtMarka.Enabled = false;
                txtEnglish_Name.Enabled = false;
                txtMal_Code.Enabled = false;
                btntxtMal_Code.Enabled = false;
                txtAarambhi_Nag.Enabled = false;
                txtNagache_Vajan.Enabled = false;
                drpType.Enabled = false;
                btnBack.Enabled = true;
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
                btntxtCode.Text = "Choose No";
                btntxtCode.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtMarka.Enabled = true;
                txtEnglish_Name.Enabled = true;
                txtMal_Code.Enabled = true;
                btntxtMal_Code.Enabled = true;
                txtAarambhi_Nag.Enabled = true;
                txtNagache_Vajan.Enabled = true;
                drpType.Enabled = true;
                btnBack.Enabled = false;
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
    #endregion
    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        //int RecordCount = 0;
        //string query = "";
        //query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString();
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
        //if (txtCode.Text!= string.Empty)
        //{
        //    #region check for next or previous record exist or not
        //    query = "SELECT top 1 [Code] from " + tblHead + " where Code>" + Convert.ToInt32(hdnf.Value)
        //        + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Item_Code asc  ";
        //    string strDoc_No = clsCommon.getString(query);
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //    }
        //    else
        //    {
        //        btnNext.Enabled = false;
        //        btnLast.Enabled = false;
        //    }
        //    query = "SELECT top 1 [Item_Code] from " + tblHead + " where Item_Code<" + Convert.ToInt32(hdnf.Value)
        //        + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  ORDER BY Item_Code desc  ";
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnPrevious.Enabled = true;
        //        btnFirst.Enabled = true;
        //    }
        //    else
        //    {
        //        btnPrevious.Enabled = false;
        //        btnFirst.Enabled = false;
        //    }
        //}
        //    #endregion
        #endregion

        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        // query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) " and Sub_Type='" + drpSub_Type.SelectedValue + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString());

        query = "select count(*) from " + tblHead + " where  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            "' ";
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
        if (txtCode.Text != string.Empty)
        {
            #region check for next or previous record exist or not

            query = "SELECT top 1 [Code] from " + tblHead + " where Code>" + Convert.ToInt32(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                 "  ORDER BY Code asc  ";


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


            query = "SELECT top 1 [Code] from " + tblHead + " where Code<" + int.Parse(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                  "  ORDER BY Code desc  ";
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
        #endregion

    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Code from " + tblHead + " where Code=(select MIN(Code) from " + tblHead + " where Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
            if (txtCode.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Code] from " + tblHead + " where Code< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='"
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Code desc  ";
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
            if (txtCode.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Code] from " + tblHead + " where Code> " + Convert.ToInt32(hdnf.Value)
                    + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  ORDER BY Code asc  ";
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
            query = "select Code from " + tblHead + " where Code=(select MAX(Code) from " + tblHead + " where Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
            setFocusControl(txtCode);
            //Int32 Code = Convert.ToInt32(clsCommon.getString("select IDENT_CURRENT('" + tblHead + "') as Doc_No"));
            string qry = "select isnull(max(Code),0) as Doc_No from " + tblHead +
                    " where Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString();
            Int32 Code = Convert.ToInt32(clsCommon.getString(qry));
            if (Code != 0)
            {
                int doc_no = Code + 1;
                Code = doc_no;
            }
            else
            {
                Code = 1;
            }
            txtCode.Text = Convert.ToString(Code);
            setFocusControl(txtMarka);
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
        txtCode.Enabled = false;
        setFocusControl(txtMarka);
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
                    string currentDoc_No = txtCode.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));

                        string strRev = string.Empty;
                        child1.SetAttributeValue("Code", txtCode.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);


                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_Brand_Master";
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
                        Response.Redirect("../BussinessRelated/PgeDoHeadUtility.aspx");
                    }
                    this.enableDisableNavigateButtons();
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
        hdnf.Value = Request.QueryString["BrandCode"];
        if (hdnf.Value == "0")
        {
            hdnf.Value = clsCommon.getString("select max(Code) from qryBrand_Master where Company_Code=" + Session["Company_Code"].ToString() + "");
        }
        // Response.Redirect("pgeDeliveryOrderForGSTxmlNew.aspx");
        Response.Redirect("pgeBrand_Master.aspx?BrandCode=" + hdnf.Value + "&Action=" + 1);
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

                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                        
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtCode.Text = dt.Rows[0]["Code"].ToString();

                        txtMarka.Text = dt.Rows[0]["Marka"].ToString();
                        txtEnglish_Name.Text = dt.Rows[0]["English_Name"].ToString();
                        txtMal_Code.Text = dt.Rows[0]["Mal_Code"].ToString();
                        lblmal_code.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtAarambhi_Nag.Text = dt.Rows[0]["Aarambhi_Nag"].ToString();
                        txtNagache_Vajan.Text = dt.Rows[0]["Nagache_Vajan"].ToString();
                        string a = dt.Rows[0]["Type"].ToString();
                        drpType.SelectedValue = dt.Rows[0]["Type"].ToString().Trim();
                        txtwtper.Text = dt.Rows[0]["Wt_Per"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            hdnf.Value = txtCode.Text;
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
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtCode")
            {
                setFocusControl(txtCode);
            }
            if (strTextBox == "txtMarka")
            {
                setFocusControl(txtMarka);
            }
            if (strTextBox == "txtEnglish_Name")
            {
                setFocusControl(txtEnglish_Name);
            }
            if (strTextBox == "txtMal_Code")
            {
                string acname = "";
                if (txtMal_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMal_Code.Text);
                    if (a == false)
                    {
                        btntxtMal_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        if (txtMal_Code.Text != string.Empty)
                        {
                            acname = clsCommon.getString("select System_Name_E from  NT_1_SystemMaster where System_Code=" + txtMal_Code.Text
                                + "  and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (acname != string.Empty)
                            {
                                lblmal_code.Text = acname;
                                setFocusControl(txtAarambhi_Nag);

                            }
                            else
                            {
                                txtMal_Code.Text = string.Empty;
                                lblmal_code.Text = acname;
                                setFocusControl(txtMal_Code);
                            }
                        }
                        else
                        {
                            txtMal_Code.Text = string.Empty;
                            lblmal_code.Text = acname;
                            setFocusControl(txtMal_Code);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtMal_Code);
                }
            }
            if (strTextBox == "txtAarambhi_Nag")
            {
                setFocusControl(txtAarambhi_Nag);
            }
            if (strTextBox == "txtNagache_Vajan")
            {
                setFocusControl(txtNagache_Vajan);
            }
            if (strTextBox == "drpType")
            {
                setFocusControl(drpType);
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
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Code=" + hdnf.Value + "";
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
                txtCode.Text = hdnf.Value;
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtCode" || v == "txtEditDoc_No")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("300px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("300px");
                //e.Row.Cells[3].ControlStyle.Width = new Unit("100px");

            }
            if (v == "txtMal_Code")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
            }
            //if (v == "txtBusiness_Code" || v == "txtHSN")
            //{
            //    e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
            //    e.Row.Cells[1].ControlStyle.Width = new Unit("150px");


            //}
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
        //searchString = txtEditDoc_No.Text;
        //strTextBox = "txtEditDoc_No";
        //csCalculations();
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
    #endregion
    #region [txtCode_TextChanged]
    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtCode.Text;
        //strTextBox = "txtCode";
        //csCalculations();
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtCode.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtCode.Text != string.Empty)
                {
                    txtValue = txtCode.Text;

                    string qry = "select * from " + tblHead + " where  Code='" + txtValue + "' " +
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
                                hdnf.Value = dt.Rows[0]["Code"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Code (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtMarka);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtCode.Enabled = false;
                                            setFocusControl(txtMarka);

                                            hdnf.Value = txtCode.Text;
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
                                    setFocusControl(txtMarka);
                                    txtCode.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtCode.Text = string.Empty;
                                    setFocusControl(txtCode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtCode);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtCode.Text = string.Empty;
                setFocusControl(txtCode);
            }
        }
        catch
        {

        }
        #endregion
    }
    #endregion
    #region [btntxtCode_Click]
    protected void btntxtCode_Click(object sender, EventArgs e)
    {

        try
        {
            if (btntxtCode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtCode.Text = string.Empty;
                txtCode.Enabled = true;


                btnSave.Enabled = false;
                setFocusControl(txtCode);
            }

            if (btntxtCode.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtCode";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtMarka_TextChanged]
    protected void txtMarka_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMarka.Text;
        strTextBox = "txtMarka";
        csCalculations();
    }
    #endregion
    #region [txtEnglish_Name_TextChanged]
    protected void txtEnglish_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEnglish_Name.Text;
        strTextBox = "txtEnglish_Name";
        csCalculations();
    }
    #endregion
    #region [txtMal_Code_TextChanged]
    protected void txtMal_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMal_Code.Text;
        strTextBox = "txtMal_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtMal_Code_Click]
    protected void btntxtMal_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMal_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtAarambhi_Nag_TextChanged]
    protected void txtAarambhi_Nag_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAarambhi_Nag.Text;
        strTextBox = "txtAarambhi_Nag";
        csCalculations();
    }
    #endregion
    #region [txtNagache_Vajan_TextChanged]
    protected void txtNagache_Vajan_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNagache_Vajan.Text;
        strTextBox = "txtNagache_Vajan";
        csCalculations();
    }
    #endregion
    #region [drpType_TextChanged]
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            if (hdnfClosePopup.Value == "txtCode" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtCode.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtCode.Text = string.Empty;
                    txtCode.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtCode);
                    hdnfClosePopup.Value = "Close";


                }
                if (btntxtCode.Text == "Choose No")
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
                        //name += "Code Like '%" + aa + "%'or";
                        name += "( Code like '%" + aa + "%' or English_Name like '%" + aa + "%' )  and";

                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select Code as Marka_Code,English_Name as Marka,Item_Name_E from qryMarkaMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Code";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtCode")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMal_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += "( Item_Code'%" + aa + "%' or Item_Name_E like '%" + aa + "%' ) and";
                    name += "( System_Code like '%" + aa + "%' or System_Name_E like '%" + aa + "%' )  and";


                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Purchase name--";
                string qry = "select System_Code,System_Name_E from  NT_1_SystemMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                  + "  and  System_Type='I' and ( " + name + ") order by System_Name_E";
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
        btnSave.Enabled = true;
        #region [Validation Part]
        bool isValidated = true;
        //        if textbox is date then if condition will be like this if(clsCommon.isValidDate(txtDoc_Date.Text==true))
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        if (txtCode.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtCode);
            return;
        }
        if (txtEnglish_Name.Text == string.Empty)
        {
            txtEnglish_Name.Text = txtMarka.Text;
        }
        #endregion

        #region -Head part declearation

        XElement root = new XElement("ROOT");
        XElement child1 = new XElement("Head");
        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
        int Branch_Code = Convert.ToInt32(Convert.ToInt32(Session["Branch_Code"].ToString()));
        string Created_By = Session["user"].ToString();
        string Modified_By = Session["user"].ToString();
        string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string retValue = string.Empty;
        string strRev = string.Empty;
        #endregion-End of Head part declearation
        #region Save Head Part

        child1.SetAttributeValue("Marka", txtMarka.Text);
        child1.SetAttributeValue("English_Name", txtEnglish_Name.Text);
        child1.SetAttributeValue("Mal_Code", txtMal_Code.Text != string.Empty ? txtMal_Code.Text : "0");
        child1.SetAttributeValue("Aarambhi_Nag", txtAarambhi_Nag.Text != string.Empty ? txtAarambhi_Nag.Text : "0");
        child1.SetAttributeValue("Nagache_Vajan", txtNagache_Vajan.Text != string.Empty ? txtNagache_Vajan.Text : "0.00");
        child1.SetAttributeValue("Type", drpType.SelectedValue);
        child1.SetAttributeValue("Wt_Per", txtwtper.Text != string.Empty ? txtwtper.Text : "0");
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Branch_Code", Branch_Code);
        if (btnSave.Text != "Save")
        {
            child1.SetAttributeValue("Modified_By", Modified_By);
            child1.SetAttributeValue("Modified_Date", Modified_Date);
            child1.SetAttributeValue("Code", txtCode.Text != string.Empty ? txtCode.Text : "0");

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
        string spname = "SP_Brand_Master";
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
        txtCode.Text = returnmaxno;
        hdnf.Value = txtCode.Text;
        retValue = op;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:Redirect('"+retValue+"','"+returnmaxno+"')", true);

        //if (retValue == "-1")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Added! No=" + returnmaxno + "');", true);

        //    Response.Redirect("pgeBrand_Master.aspx?BrandCode=" + returnmaxno + "&Action=" + 1);
        //}
        //if (retValue == "-2" || retValue == "-3")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated! No=" + returnmaxno + "');", true);
        //    Response.Redirect("pgeBrand_Master.aspx?BrandCode=" + returnmaxno + "&Action=" + 1);
        //}
        //clsButtonNavigation.enableDisable("S");
        //this.enableDisableNavigateButtons();
        //this.makeEmptyForm("S");
        //qry = getDisplayQuery();
        //this.fetchRecord(qry);
        #endregion
    }
    #endregion
}

