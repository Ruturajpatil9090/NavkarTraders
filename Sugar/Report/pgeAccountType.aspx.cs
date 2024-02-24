using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_pgeAccountType : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string searchString = string.Empty;
    string isAuthenticate = string.Empty;
    string searchStr = "";
    string strTextbox = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        //   AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                //txtFromDate.Text = clsGV.Start_Date;
                //txtToDate.Text = clsGV.End_Date;
                setFocusControl(drpAcType);
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }

    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(drpAcType);

    }
    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            //    hdnfClosePopup.Value = "Close";
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
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
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
                e.Row.Attributes["onselectstart"] = "javascript:return true;";

                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("250px");
            if (e.Row.Cells.Count > 2)
            {
                e.Row.Cells[2].Width = new Unit("100px");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

    protected void btnAccountList_Click(object sender, EventArgs e)
    {
        string AcType = drpAcType.SelectedValue.ToString();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:Ac('" + AcType + "')", true);

    }
    protected void btnGroup_type_Click(object sender, EventArgs e)
    {
        bool isValidated = true;
        try
        {
           
            string details = hdnfpodetailid.Value;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:Group_type('" + details + "')", true);

        }
        catch (Exception eex)
        {

        }

    }
    protected void txtAcCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAcCode.Text != string.Empty)
            {
                searchStr = txtAcCode.Text;
                strTextbox = "txtAcCode";

                bool a = clsCommon.isStringIsNumeric(txtAcCode.Text);
                if (a == false)
                {
                    btnAcCode_Click(this, new EventArgs());
                }
                else
                {
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                    if (str != string.Empty)
                    {
                        lblAcCodeName.Text = str;
                        setFocusControl(btnGroup_type);
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
            searchStr = txtAcCode.Text;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAcCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
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
            //if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
            //{
            //    txtSearchText.Text = searchStr;
            //}
            //else
            //{
            //    txtSearchText.Text = txtSearchText.Text;
            //}
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( group_Name_E Like '%" + aa + "%'or group_code like '%" + aa + "%')  and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Ac name--";
                string qry = "select group_code ,group_Name_E from nt_1_bsgroupmaster where " + name + " order by group_Name_E";
                this.showPopup(qry);

            }


        }
        catch
        {

        }
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
                        pnlPopup.Style["Display"] = "Block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();

                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["Display"] = "Block";
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void btnStateWise_Click(object sender, EventArgs e)
    {
        string AcType = drpAcType.SelectedValue.ToString();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:stateAc()", true);

    }
}