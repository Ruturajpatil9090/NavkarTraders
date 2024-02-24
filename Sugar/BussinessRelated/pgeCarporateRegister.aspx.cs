using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeCarporateRegister : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string Branch_Code = string.Empty;
    string uptodate = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    static WebControl objAsp = null;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string strTextbox = string.Empty;
    string accountmasterlist = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearchText.Enabled = true;
        try
        {
            uptodate = DateTime.Now.ToString("yyyy/MM/dd");
            tblPrefix = Session["tblPrefix"].ToString();
            user = Session["user"].ToString();
            AccountMasterTable = tblPrefix + "AccountMaster";
            qryCommon = tblPrefix + "qryCarporateSaleList";
            accountmasterlist = tblPrefix + "qryAccountsList";
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                pnlPopup.Style["display"] = "none";
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    txtFromDate.Text = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    fillBranches();
                    pnlPopup.Style["display"] = "none";
                  //  pnlPopup.Enabled = true;
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void fillBranches()
    {
        try
        {
            ListItem li = new ListItem("All", "0");
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select * from BranchMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            ds = clsDAL.SimpleQuery(qry);
            drpBranch.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpBranch.DataSource = dt;
                        drpBranch.DataTextField = "Branch";
                        drpBranch.DataValueField = "Branch_Id";
                        drpBranch.DataBind();
                    }
                }
            }
            drpBranch.Items.Insert(0, li);
        }
        catch
        {

        }
    }
    private string BranchCode()
    {
        try
        {
            string branchname = drpBranch.SelectedItem.ToString();
            qry = "select Branch_Id from BranchMaster where Branch='" + branchname + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            Branch_Code = clsCommon.getString(qry);

        }
        catch (Exception)
        {
            throw;
        }
        return Branch_Code;
    }
    protected void btnBalanceReport_Click(object sender, EventArgs e)
    {
        BranchCode();
        string fromDt;
        string toDt;
        DateFormat(out fromDt, out toDt);
        string acCode = txtac_code.Text.Trim();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kt", "javascript:br('" + fromDt + "','" + toDt + "','" + Branch_Code + "','" + acCode + "')", true);
    }

    private void DateFormat(out string fromDt, out string toDt)
    {
        fromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        toDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BranchCode();
        string fromDt;
        string toDt;
        DateFormat(out fromDt, out toDt);
        string acCode = txtac_code.Text.Trim();
        string pds = drpSellingType.SelectedValue.ToString();
        string lotno = txtLot_No.Text.Trim();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kd", "javascript:csd('" + fromDt + "','" + toDt + "','" + pds + "','" + Branch_Code + "','" + acCode + "','" + lotno + "')", true);
    }

    #region [txtac_code_TextChanged]
    protected void txtac_code_TextChanged(object sender, EventArgs e)
        {
        searchString = txtac_code.Text;
        strTextBox = "txtac_code";

        if (txtac_code.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtac_code.Text);
            if (a == false)
            {
                btntxtac_code_Click(this, new EventArgs());
            }
            else
            {
                string party = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtac_code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (party != string.Empty)
                {
                    lblParty_name.Text = party;
                    setFocusControl(txtac_code);
                    F1Hdnf.Value = null;
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    txtac_code.Text = string.Empty;
                    lblParty_name.Text = string.Empty;
                    setFocusControl(txtac_code);

                }
            }
        }


    }
    #endregion

    #region [btntxtac_code_Click]
    protected void btntxtac_code_Click(object sender, EventArgs e)
    {
        try
        {
            if (F1Hdnf.Value == "0")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtac_code";
                btnSearch_Click(sender, e);

                setFocusControl(txtSearchText);
            }
        }
        catch
        {
        }

    }
    #endregion

    #region [Account Master Popup Button Code]
    protected void showPopupAccountMaster(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.myaccountmaster(qry);

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
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
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
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {

            }

            if (hdnfClosePopup.Value == "txtac_code")
            {

                //lblPopupHead.Text = "--Select Party--";
                //string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and Carporate_Party='Y' and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);
                string qry = " Locked=0  and Carporate_Party='Y' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' )";

                this.showPopupAccountMaster(qry);
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
                        setFocusControl(txtSearchText);
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
                strTextbox = hdnfClosePopup.Value;

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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtdoc_no")
            {
                e.Row.Cells[0].Width = new Unit("80px");
                e.Row.Cells[1].Width = new Unit("150px");
                e.Row.Cells[2].Width = new Unit("300px");
                e.Row.Cells[3].Width = new Unit("300px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                e.Row.Cells[0].Width = new Unit("80px");
                e.Row.Cells[1].Width = new Unit("400px");
                e.Row.Cells[2].Width = new Unit("250px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
}