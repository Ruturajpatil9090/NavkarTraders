using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Report_pgeInterestCalculation : System.Web.UI.Page
{
    string searchStr = string.Empty;
    string tblPrefix = string.Empty;
    string AccountMasterTable = string.Empty;
    string strTextbox = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string searchString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
            txtFromDt.Text = clsGV.Start_Date;
            txtToDt.Text = clsGV.To_date;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            bool isvalidated = true;
            string accode = txtAc_Code.Text;
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            double intRate = 0.00;
            Int32 intDays = 0;
            pnlPopup.Style["display"] = "none";
            #region validation
            try
            {
                if (txtAc_Code.Text != string.Empty)
                {
                    string acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (acname != String.Empty)
                    {
                        isvalidated = true;
                    }
                    else
                    {
                        isvalidated = false;
                        lblAC_Code.Text = string.Empty;
                        txtAc_Code.Text = string.Empty;
                        setFocusControl(txtAc_Code);
                        return;
                    }
                }
                else
                {
                    isvalidated = false;
                    lblAC_Code.Text = string.Empty;
                    txtAc_Code.Text = string.Empty;
                    setFocusControl(txtAc_Code);
                    return;
                }

                if (txtFromDt.Text != string.Empty)
                {
                    try
                    {
                        fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    }
                    catch
                    {
                        isvalidated = false;
                        txtFromDt.Text = string.Empty;
                        setFocusControl(txtFromDt);
                        return;
                    }

                }
                else
                {
                    isvalidated = false;
                    setFocusControl(txtFromDt);
                    return;
                }
                if (txtToDt.Text != string.Empty)
                {
                    try
                    {
                        todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    }
                    catch
                    {
                        isvalidated = false;
                        txtToDt.Text = string.Empty;
                        setFocusControl(txtToDt);
                        return;
                    }

                }
                else
                {
                    isvalidated = false;
                    setFocusControl(txtToDt);
                    return;
                }

                if (txtInterestRate.Text != string.Empty)
                {
                    intRate = Convert.ToDouble(txtInterestRate.Text);
                    isvalidated = true;
                }
                else
                {
                    isvalidated = false;

                    setFocusControl(txtInterestRate);
                    return;
                }

                if (txtDays.Text != string.Empty)
                {
                    intDays = Convert.ToInt32(txtDays.Text);
                    isvalidated = true;
                }
                else
                {
                    isvalidated = false;

                    setFocusControl(txtDays);
                    return;
                }
            }
            catch
            {
                isvalidated = false;
                setFocusControl(txtFromDt);
                return;
            }
            #endregion

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','" + intRate + "','" + intDays + "','DrCr')", true);
        }
        catch (Exception exx)
        {

        }
    }

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            //if (txtSearchText.Text == string.Empty)
            //{
            //    txtSearchText.Text = searchStr;
            //}
            //else
            //{
            //    txtSearchText.Text = txtSearchText.Text;
            //}
            //pnlPopup.Style["display"] = "block";
            //lblPopupHead.Text = "--Select Account--";
            //string qry = "select Ac_Code as [Account Code], Ac_Name_E as [Account Name],Short_Name as [Short Name],CityName from " + tblPrefix + "qryAccountsList where (CityName like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
            //this.showPopup(qry);

            string searchtxt = searchString;
            if (searchStr != string.Empty)
            {
                txtSearchText.Text = searchStr;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;

            if (searchString != string.Empty)
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
            if (hdnfclosepopup.Value == "txtAc_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%'   or cityname like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
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

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        //objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAc_Code.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                if (a == false)
                {
                    searchStr = txtAc_Code.Text;
                    btnACCode_Click(this, new EventArgs());
                }
                else
                {
                    searchStr = txtAc_Code.Text;
                    strTextbox = "txtAc_Code";
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAc_Code.Text);
                    if (str != string.Empty)
                    {
                        hdnfpopup.Value = null;
                        lblAC_Code.Text = str;
                        txtEmailID.Text = clsCommon.getString("select Email_Id from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAc_Code.Text);
                        setFocusControl(txtFromDt);
                        pnlPopup.Style["display"] = "none";
                    }
                    else
                    {
                        txtAc_Code.Text = string.Empty;
                        lblAC_Code.Text = string.Empty;
                    }
                }
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

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    protected void btnACCode_Click(object sender, EventArgs e)
    {
        if (hdnfpopup.Value == "0")
        {
            pnlPopup.Style["display"] = "block";
            hdnfclosepopup.Value = "txtAc_Code";
            btnSearch_Click(sender, e);
        }
        //if (searchStr != string.Empty && hdnfclosepopup.Value == "txtAc_Code")
        //{
        //    txtSearchText.Text = searchStr;
        //}
        //string qry = "Select Ac_Code,Ac_Name_E from " + tblPrefix + "AccountMaster where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
        //this.showPopup(qry);
    }
    protected void btnOnlyDR_Click(object sender, EventArgs e)
    {
        try
        {
            bool isvalidated = true;
            string accode = txtAc_Code.Text;
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            double intRate = 0.00;
            Int32 intDays = 0;
            pnlPopup.Style["display"] = "none";
            #region validation
            try
            {
                if (txtAc_Code.Text != string.Empty)
                {
                    string acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (acname != String.Empty)
                    {
                        isvalidated = true;
                    }
                    else
                    {
                        isvalidated = false;
                        lblAC_Code.Text = string.Empty;
                        txtAc_Code.Text = string.Empty;
                        setFocusControl(txtAc_Code);
                        return;
                    }
                }
                else
                {
                    isvalidated = false;
                    lblAC_Code.Text = string.Empty;
                    txtAc_Code.Text = string.Empty;
                    setFocusControl(txtAc_Code);
                    return;
                }

                if (txtFromDt.Text != string.Empty)
                {
                    try
                    {
                        fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    }
                    catch
                    {
                        isvalidated = false;
                        txtFromDt.Text = string.Empty;
                        setFocusControl(txtFromDt);
                        return;
                    }

                }
                else
                {
                    isvalidated = false;
                    setFocusControl(txtFromDt);
                    return;
                }
                if (txtToDt.Text != string.Empty)
                {
                    try
                    {
                        todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    }
                    catch
                    {
                        isvalidated = false;
                        txtToDt.Text = string.Empty;
                        setFocusControl(txtToDt);
                        return;
                    }

                }
                else
                {
                    isvalidated = false;
                    setFocusControl(txtToDt);
                    return;
                }

                if (txtInterestRate.Text != string.Empty)
                {
                    intRate = Convert.ToDouble(txtInterestRate.Text);
                    isvalidated = true;
                }
                else
                {
                    isvalidated = false;

                    setFocusControl(txtInterestRate);
                    return;
                }

                if (txtDays.Text != string.Empty)
                {
                    intDays = Convert.ToInt32(txtDays.Text);
                    isvalidated = true;
                }
                else
                {
                    isvalidated = false;

                    setFocusControl(txtDays);
                    return;
                }
            }
            catch
            {
                isvalidated = false;
                setFocusControl(txtFromDt);
                return;
            }
            #endregion

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','" + intRate + "','" + intDays + "','Dr')", true);
        }
        catch (Exception exx)
        {

        }
    }
}