using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Report_pgeUTR_Report : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string AccountMasterTable = string.Empty;
    string searchStr = "";
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;

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
            pnlPopup.Style["display"] = "block";
            if (drpType.SelectedValue == "M")
            {
                hdnfClosePopup.Value = "txtAcCode";
            }
            else
            {
                hdnfClosePopup.Value = "txtBankCode";
            }
            btnSearch_Click(sender, e);
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
    protected void txtUtrNo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnPeridocTrailblance_Click(object sender, EventArgs e)
    {

        string accode = txtAcCode.Text;

        if (accode == string.Empty)
        {
            accode = "0";

        }

        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:btnperidoctribalnce('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnDoCashDiff_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;

        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:DoCashDiff('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnGetData_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnDetailRptNew_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:ut('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnUTRNODOWise_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:UTRNo('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnDOnoUTR_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "noutr", "javascript:noUTR('" + FromDt + "','" + ToDt + "')", true);

    }

    protected void btnMillPaySummary_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:MillSM('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnMillPayDetail_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:MillDE('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);
    }

    protected void btnTenderBal_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderBal('" + FromDt + "','" + ToDt + "')", true);


        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderBal('" + accode + "'," + FromDt + "','" + ToDt + "')", true);

    }

    protected void btnutrreport_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:UTRdetail('" + FromDt + "','" + ToDt + "')", true);


        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderBal('" + accode + "'," + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnUtrSummary_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:UTRSummary('" + accode + "','" + FromDt + "','" + ToDt + "')", true);



    }

    protected void btnurtdetailbalance_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:UTRdetailbalance('" + accode + "','" + FromDt + "','" + ToDt + "')", true);



    }

    protected void btntenderidwisesauda_Click(object sender, EventArgs e)
    {
        string bss = string.Empty;
        if (hdconfirm.Value == "Yes")
        {

            bss = "N";
            //  bss = hdnfpaymentcomnfirm.Value;
        }
        else
        {
            bss = "Y";
            //  bss = hdnfpaymentcomnfirm.Value;

        }

        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        if (hdconfirm.Value == "Yes")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderidwiseSauda('" + accode + "','" + FromDt + "','" + ToDt + "','" + bss + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderidwiseSauda('" + accode + "','" + FromDt + "','" + ToDt + "','" + bss + "')", true);
        }


    }

    protected void btnmillwisesauda_Click(object sender, EventArgs e)
    {
        string bss = string.Empty;
        if (hdconfirm.Value == "Yes")
        {

            bss = "N";
            //  bss = hdnfpaymentcomnfirm.Value;
        }
        else
        {
            bss = "Y";
            //  bss = hdnfpaymentcomnfirm.Value;

        }

        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        if (hdconfirm.Value == "Yes")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:milldwiseSauda('" + accode + "','" + FromDt + "','" + ToDt + "','" + bss + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:milldwiseSauda('" + accode + "','" + FromDt + "','" + ToDt + "','" + bss + "')", true);
        }


    }

    protected void btnnormalsauda_Click(object sender, EventArgs e)
    {
         string accode = txtAcCode.Text;
         string bss = "R";
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:UTRdetailbalance('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderidwiseSauda('" + accode + "','" + FromDt + "','" + ToDt + "','" + bss + "')", true);
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
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchStr != string.Empty)
            {
                txtSearchText.Text = searchStr;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code , Ac_Name_E ,Short_Name ,CityName from qrymstaccountmaster where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBankCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code as [Account Code], Ac_Name_E as [Account Name],Short_Name as [Short Name],CityName from " + tblPrefix + "qryAccountsList where Ac_Type='B' and (CityName like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
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
    protected void txtBankCode_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (txtBankCode.Text != string.Empty)
        //    {
        //        searchStr = txtBankCode.Text;
        //        strTextbox = "txtBankCode";
        //        string str = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Type='B' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtBankCode.Text);
        //        if (str != string.Empty)
        //        {
        //            lblBankName.Text = str;
        //            txtAcCode.Enabled = false;
        //            btnAcCode.Enabled = false;
        //            setFocusControl(txtFromDate);
        //        }
        //        else
        //        {
        //            txtBankCode.Text = string.Empty;
        //            lblBankName.Text = string.Empty;
        //        }
        //    }
        //    else
        //    {
        //        lblBankName.Text = string.Empty;
        //    }

        //}
        //catch
        //{

        //}
    }
    protected void btnBankCode_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    pnlPopup.Style["display"] = "block";
        //    hdnfClosePopup.Value = "txtBankCode";
        //    btnSearch_Click(sender, e);
        //}
        //catch
        //{
        //}
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(txtAcCode);
        txtAcCode.Text = string.Empty;
        lblAcCodeName.Text = string.Empty;
        pnlPopup.Style["display"] = "none";
    }
    protected void btnOnlyBalance_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:ob('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnSaudaBalSummary_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:SaudaSM('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnsaudaBalDetail_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:SaudaDE('" + accode + "','" + utr_no + "','" + AcType + "','" + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnAgainstBillSummary_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:Againtsb('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnAgaistSaleBillDetail_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        if (utr_no == string.Empty)
        {
            utr_no = "0";
        }
        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:Againtsbdetail('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

    }

    protected void btnpaymentbalance_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;

        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:paymentbalance('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

    }

    protected void btnpaymentdetail_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;

        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:paymentdetail('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

    }

    protected void btnmillcashdiff_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;

        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:MillCashDiff('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnpartybrokercashdiff_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;

        pnlPopup.Style["display"] = "none";
        string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:Partybrokercashdiff('" + accode + "','" + FromDt + "','" + ToDt + "')", true);

    }
    protected void btnadjustBills_Click(object sender, EventArgs e)
    {
        string qry = " update nt_1_transactdetail set Tender_No = h.doc_no,narration=narration+',SB:'+convert(varchar(10),h.doc_no) from nt_1_transactdetail s ,nt_1_sugarsale h where s.tenderdetailid =h.saleid and drpFilterValue='B' and Tender_No=0 ";
        string returndata = clsCommon.getString(qry);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('sucessfully Updated!!!')", true);

    }

    protected void btntenderdistpatchreport_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        // string utr_no = txtUtrNo.Text;
        pnlPopup.Style["display"] = "none";
        // string AcType = drpType.SelectedValue.ToString();
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tbal", "javascript:TenderdispatchReg('" + accode + "','" + FromDt + "','" + ToDt + "')", true);



    }
    protected void drpTranTYpe_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(txtAcCode);
        txtAcCode.Text = string.Empty;
        lblAcCodeName.Text = string.Empty;
        pnlPopup.Style["display"] = "none";
    }
    protected void btnalltrantype_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;
        string utr_no = txtUtrNo.Text;
        string type = drpTranTYpe.SelectedValue.ToString();
        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:trantype('" + type + "','" + FromDt + "','" + ToDt + "','" + accode + "')", true);

    }
    protected void btnDoCancle_Click(object sender, EventArgs e)
    {


        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:DoCancle('" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnRetailBillSummary_Click(object sender, EventArgs e)
    {


        pnlPopup.Style["display"] = "none";
        string accode = txtAcCode.Text;
        if (accode == string.Empty)
        {
            accode = "0";
        }
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:retailBillsummary('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnRetailBillDetail_Click(object sender, EventArgs e)
    {


        pnlPopup.Style["display"] = "none";
        string accode = txtAcCode.Text;
        if (accode == string.Empty)
        {
            accode = "0";
        }
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:retailbilldetail('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }

    protected void btnowndo_Click(object sender, EventArgs e)
    {

        pnlPopup.Style["display"] = "none";
        string accode = txtAcCode.Text;
        if (accode == string.Empty)
        {
            accode = "0";
        }
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:owndo('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }

    protected void btnPendingPurchase_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "none";
        string accode = txtAcCode.Text;
        if (accode == string.Empty)
        {
            accode = "0";
        }
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:PendingPurchase('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }

    protected void btnPendingSale_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "none";
        string accode = txtAcCode.Text;
        if (accode == string.Empty)
        {
            accode = "0";
        }
        string FromDt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:PendingSale('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
}