using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;

public partial class Report_PgeShareRegister : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string searchStr = string.Empty;
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string AccountMasterTable = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    int Rowaction = 2;
    int Srno = 3;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }

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
                //btnClosedetails.Enabled = false;
                ViewState["currentTable"] = null;

                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = false;

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

                //btnAdddetails.Enabled = true;
                //btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;

                txtScript_Code.Enabled = true;
                lblScriptCode.Text = string.Empty;
                btntxtScript_Code.Enabled = true;

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

                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = false;

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

                txtScript_Code.Enabled = true;
                btntxtScript_Code.Enabled = true;

            }
            #region Always check this
            #endregion
        }
        catch
        {
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
                        //txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        //lbldoc_no.Text = dt.Rows[0][""].ToString();                        
                        txtScript_Code.Text = dt.Rows[0]["Script_Code"].ToString();
                        lblScriptCode.Text = dt.Rows[0]["Script_Name"].ToString();
                        recordExist = true;
                        //lblMsg.Text = "";
                    }
                }
            }
            //this.enableDisableNavigateButtons();
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
            if (strTextBox == "txtAcCode")
            {
                setFocusControl(txtAcCode);
            }
            if (strTextBox == "txtScript_Code")
            {
                string scriptcodetext = txtScript_Code.Text.Trim();
                if (scriptcodetext != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(scriptcodetext);
                    if (a == false)
                    {
                        btntxtScript_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        string scriptName = clsCommon.getString("select Script_Name from Script_Master where FNO_Script_No=" + scriptcodetext + " and FNO_Type = 'E'");
                        if (scriptName != string.Empty && scriptName != "0")
                        {
                            lblScriptCode.Text = scriptName;
                            setFocusControl(txtFromDt);

                        }
                        else
                        {
                            txtScript_Code.Text = string.Empty;
                            lblScriptCode.Text = string.Empty;


                        }
                    }
                }
                else
                {
                    lblScriptCode.Text = "";
                    setFocusControl(txtScript_Code);
                    setFocusControl(txtFromDt);
                }
                if (strTextBox == "txtFromDt")
                {
                    setFocusControl(txtFromDt);
                }
                if (strTextBox == "txtToDt")
                {
                    setFocusControl(txtToDt);
                }
                return;
            }

        }
        catch
        {
        }
    }
    #endregion
    #region [txtScript_Code_TextChanged]
    protected void txtScript_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtScript_Code.Text;
        strTextBox = "txtScript_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtScript_Code_Click]
    protected void btntxtScript_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtScript_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    protected void txtAcCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAcCode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAcCode.Text);
                if (a == false)
                {
                    searchStr = txtAcCode.Text;
                    strTextbox = "txtAcCode";
                    btnAcCode_Click(this, new EventArgs());
                }
                else
                {
                    searchStr = txtAcCode.Text;
                    strTextbox = "txtAcCode";
                    string str = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                    if (str != string.Empty)
                    {
                        lblAcCodeName.Text = str;
                        setFocusControl(txtFromDt);
                    }
                    else
                    {
                        txtAcCode.Text = string.Empty;
                        lblAcCodeName.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }
    protected void btnAcCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAcCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    #endregion
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
                e.Row.Attributes["onselectstart"] = "javascript:return true;";
            }
        }
        catch
        {
            throw;
        }
    }
    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        //string v = hdnfClosePopup.Value;
        //if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------

            //--------------------------------------------------
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[0].Style["overflow"] = "hidden";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].Style["overflow"] = "hidden";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[3].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Rowaction].Visible = true;
            e.Row.Cells[Srno].Visible = false;
        }
        catch
        {
        }
    }
    #endregion
    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {

        }
    }
    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = searchStr;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchStr;
            string[] split = null;
            string name = string.Empty;
            if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchStr;
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
            if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchStr;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                //    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                //}
                //name = name.Remove(name.Length - 3);
                //lblPopupHead.Text = "--Select Account--";
                //string qry = "select Ac_Code,Ac_Name_E from " + tblHead + " where (" + name + ") order by Ac_Name_E asc";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtScript_Code")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( FNO_Script_No like '%" + aa + "%' or Script_Name like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Script Code--";
                string qry = "select FNO_Script_No,Script_Name from Script_Master where FNO_Type = 'E' and (Script_Name like '%" + txtSearchText.Text + "%' or FNO_Script_No like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
        }
        catch
        {

        }
    }
    private void showPopup(string qry)
    {
        try
        {
            this.setFocusControl(txtSearchText);
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

    protected void btnPurchaseReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:epr('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnSaleReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:esr('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnEquityBalance_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:eb('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnEquityGlobal_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:eg('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnStockBook_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        string FNO_Script_No = txtScript_Code.Text;

        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }

        //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sb('" + Script_Code + "','" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sbnew('" + fromDT + "','" + toDT + "','" + FNO_Script_No + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnTaxRegister_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:tr('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnShortLong_Click(object sender, EventArgs e) 
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:slt('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnReturnPurchaseReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:rpr('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnReturnSaleReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:rsr('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnServiceBillReport_Click(object sender, EventArgs e)
    {
    }
    protected void btnRetailSaleReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = clsGV.Start_Date;
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:rs('" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }
}