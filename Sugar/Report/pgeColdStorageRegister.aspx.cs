using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_pgeColdStorageRegister : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string AccountMasterTable = string.Empty;
    string searchStr = "";
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string searchString = string.Empty;

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
                txtFromDate.Text = clsGV.Start_Date;
                txtToDate.Text = clsGV.End_Date;
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    protected void btnAcCode_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfpopup.Value == "0")
            {
                hdnfClosePopup.Value = "txtAcCode";
                pnlPopup.Style["display"] = "block";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {

        }
    }

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            //hdnfClosePopup.Value = "Close";
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
                    btnAcCode_Click(this, new EventArgs());
                }
                else
                {
                    searchStr = txtAcCode.Text;
                    strTextbox = "txtAcCode";
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                    if (str != string.Empty)
                    {
                        hdnfpopup.Value = null;
                        lblAcCodeName.Text = str;
                        setFocusControl(txtFromDate);
                        pnlPopup.Style["display"] = "none";

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

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
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

                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("250px");
            e.Row.Cells[2].Width = new Unit("100px");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("100px");
            e.Row.Cells[2].Width = new Unit("300px");
            e.Row.Cells[1].Width = new Unit("100px");
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string searchtxt = searchStr;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = "";
            string[] split = null;
            string name = string.Empty;
            if (searchStr != string.Empty)
            {
                txtSearchText.Text = searchStr;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }
            else
            {
                txtSearchText.Text = searchStr;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code , Ac_Name_E ,Short_Name ,CityName from qrymstaccountmaster where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtpurches")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select doc_no ,mill_code,millname ,doc_dateConverted from qryretailseflbalncedetail where  (millname like '%" + txtSearchText.Text + "%' or mill_code like '%" + txtSearchText.Text + "%' or doc_no like '%" + txtSearchText.Text + "%' or doc_dateConverted like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by millname asc";
                this.showPopup(qry);
            }
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
                searchStr = txtSearchText.Text;
                strTextbox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }

    protected void btnColdStorageDetail_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;


        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:ColdStorageDetail('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnColdStorageBalance_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;


        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        //  string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:ColdStorageBalance('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }


}