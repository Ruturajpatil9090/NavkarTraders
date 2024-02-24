using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


public partial class pgeGLedgerReport : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string AccountMasterTable = string.Empty;
    string searchStr = "";
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {
            BindContrydropdown();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                txtFromDt.Text = Session["Start_Date"].ToString();
                txtToDt.Text = Session["End_Date"].ToString();
                drpFilter.SelectedValue = "A";
                pnlAcNameWise.Visible = true;
                pnlBSGroupWise.Visible = false;
                setFocusControl(txtAcCode);
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    protected void btndepri_Click(object sender, EventArgs e)
    {
        string frmdt = Session["Start_Date"].ToString();
        string todt = clsGV.End_Date;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asdas1", "javascript:depr('" + frmdt + "','" + todt + "')", true);
    }


    protected void btnAcCode_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfnew.Value != "N")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtAcCode";
                btnSearch_Click(sender, e);
            }
            else
            {
                hdnfnew.Value = "";
            }
        }
        catch
        {
        }
    }
    protected void BindContrydropdown()
    {
        try
        {
            string qry = "Select TRAN_TYPE from qrytrantype order by TRAN_TYPE";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    drptrantype.DataValueField = "TRAN_TYPE";
                    DataRow row = dt.NewRow();
                    row["TRAN_TYPE"] = "All";
                    //dt.Rows.Add(row);
                    dt.Rows.InsertAt(row, 0);
                    //drpsalebill.AppendDataBoundItems = false;
                    if (dt.Rows.Count > 0)
                    {
                        drptrantype.DataSource = dt;
                        //dt.Columns["TRAN_TYPE"].SetOrdinal(0);

                        drptrantype.DataBind();


                    }
                    else
                    {
                        drptrantype.DataSource = null;
                        drptrantype.DataBind();


                    }
                }
            }

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
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                //string qry = "select a.Ac_Code as [Account Code], a.Ac_Name_E as [Account Name],a.Short_Name as [Short Name],CityName,ISNULL(SUM(case g.DRCR when 'D' then g.AMOUNT when 'C' then -g.AMOUNT end),0) as Balance from " + tblPrefix + "qryAccountsList a LEFT OUTER JOIN NT_1_GLEDGER g ON a.Ac_Code=g.AC_CODE and a.Company_Code=g.COMPANY_CODE where (a.Ac_Code like '%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%' or a.Short_Name like '%" + txtSearchText.Text + "%' or a.CityName like '%" + txtSearchText.Text + "%') and a.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' group by a.Ac_Name_E,a.Ac_Code,a.Short_Name,a.CityName order by a.Ac_Name_E";


                //string qry = "select Ac_Code,Ac_Name_E,cityname,isnull(SUM(case DRCR when 'D' then AMOUNT when 'C' then -AMOUNT end),0) as Balance from qrygledger "
                //+ " where (Ac_Code like'%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') and Company_Code='"
                //+ Convert.ToInt32(Session["Company_Code"].ToString()) + "' and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' group by Ac_Name_E,Ac_Code,cityname order by Ac_Name_E";

                //string qry = "SELECT a.Ac_Code, a.Ac_Name_E,sum(case when l.drcr='D' then l.AMOUNT else -l.amount end) as Balance FROM nt_1_bsgroupmaster g INNER JOIN  " +
                //         " nt_1_accountmaster a ON g.bsid = a.bsid INNER JOIN nt_1_gledger l ON a.accoid = l.ac where (a.Ac_Code like'%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%')  and l.Company_Code='"
                //          + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  group by  a.Ac_Code, a.Ac_Name_E";

                //string qry = "SELECT a.Ac_Code, a.Ac_Name_E,dbo.nt_1_citymaster.city_name_e as City_Name FROM nt_1_bsgroupmaster g INNER JOIN  " +
                //         " nt_1_accountmaster a ON g.bsid = a.bsid INNER JOIN nt_1_gledger l ON a.accoid = l.ac LEFT OUTER JOIN dbo.nt_1_citymaster ON a.cityid = dbo.nt_1_citymaster.cityid where (a.Ac_Code like'%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%' or dbo.nt_1_citymaster.city_name_e like '%" + txtSearchText.Text + "%')  and l.Company_Code='"
                //          + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and a.Locked=0  group by  a.Ac_Code, a.Ac_Name_E,dbo.nt_1_citymaster.city_name_e";


                string qry = "SELECT a.Ac_Code, a.Ac_Name_E,dbo.nt_1_citymaster.city_name_e as City_Name FROM nt_1_bsgroupmaster g RIGHT OUTER JOIN  " +
                    " nt_1_accountmaster a ON g.bsid = a.bsid LEFT  OUTER JOIN nt_1_gledger l ON a.accoid = l.ac LEFT OUTER JOIN dbo.nt_1_citymaster ON a.cityid = dbo.nt_1_citymaster.cityid where (a.Ac_Code like'%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%' or dbo.nt_1_citymaster.city_name_e like '%" + txtSearchText.Text + "%')  and l.Company_Code='"
                     + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and a.Locked=0  group by  a.Ac_Code, a.Ac_Name_E,dbo.nt_1_citymaster.city_name_e";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtUnitCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account Unit--";
                string qry = "select a.Ac_Code as [Account Code], a.Ac_Name_E as [Account Name],a.Short_Name as [Short Name],CityName,ISNULL(SUM(case g.DRCR when 'D' then g.AMOUNT when 'C' then -g.AMOUNT end),0) as Balance from qrymstaccountmaster a LEFT OUTER JOIN NT_1_GLEDGER g ON a.Ac_Code=g.AC_CODE and a.Company_Code=g.COMPANY_CODE where (a.Ac_Code like '%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%' or a.Short_Name like '%" + txtSearchText.Text + "%' or a.CityName like '%" + txtSearchText.Text + "%') and a.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and a.Locked=0 group by a.Ac_Name_E,a.Ac_Code,a.Short_Name,a.CityName order by a.Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBSGroupCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Group--";
                string qry = "select [group_Code], [group_Name_E] as [Group Name] from " + tblPrefix + "BSGroupMaster  where (group_Code like '%" + txtSearchText.Text + "%' or group_Name_E like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                this.showPopup(qry);
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

    #region [btn ledger tran Type]
    protected void btnLedgerTranType_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            string tran_type = drptrantype.SelectedValue;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (txtAcCode.Text == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;

                pnlPopup.Style["display"] = "none";

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:lgrtrantype('" + accode + "','" + fromdt + "','" + todt + "','DrCr')", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kyasd", "javascript:lgrtrantype('" + accode + "','" + fromdt + "','" + todt + "','DrCr','" + tran_type + "')", true);


            }

            else
            {
            }
        }
        catch (Exception eex)
        {

        }
    }
    #endregion

    protected void drptrantype_SelectedIndexChanged(object sender, EventArgs e)
    {

        //string trantype= drptrantype.sel
        //Sale_Bill = drpsalebill.SelectedValue.ToString();
        //showLastRecord();
    }

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

                searchStr = txtSearchText.Text;
                strTextbox = hdnfClosePopup.Value;

                setFocusControl(btnSearch);
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
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    protected void btndaybookhtmll_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;



            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            //string fromdt = txtFromDt.Text;
            //string todt = txtToDt.Text;



            //fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            pnlPopup.Style["display"] = "none";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "das", "javascript:daybookHtml('" + fromdt + "','" + todt + "')", true);
        }
        catch (Exception eex)
        {

        }
    }

    protected void btnDayBook_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;



            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            //string fromdt = txtFromDt.Text;
            //string todt = txtToDt.Text;



            //fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            pnlPopup.Style["display"] = "none";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "das", "javascript:daybook('" + fromdt + "','" + todt + "')", true);
        }
        catch (Exception eex)
        {

        }
    }

    #region [btnGetData_Click]
    protected void btnGetData_Click(object sender, EventArgs e)
    {
        try
        {
            //string fromdt = txtFromDt.Text;
            //string todt = txtToDt.Text;
            //fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            //string fromdt = Session["Start_Date"].ToString();
            //string todt = Session["End_Date"].ToString();

            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;



            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (txtAcCode.Text == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;

                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','DrCr')", true);
            }
            if (drpFilter.SelectedValue == "G")
            {
                string groupcode = txtBSGroupCode.Text;
                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp_multipleLedger('" + fromdt + "','" + todt + "','" + groupcode + "')", true);
            }
            else
            {
            }
        }
        catch (Exception eex)
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
                searchStr = txtAcCode.Text;
                hdnfAc.Value = "0";
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
                        hdnfAc.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and accoid=" + txtAcCode.Text);
                        string mob = clsCommon.getString("select Mobile_No from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                        txtMobileNo.Text = mob;
                        string balance = clsCommon.getString("select SUM(case DRCR when 'D' then AMOUNT when 'C' then -AMOUNT end) as Balance from " + tblPrefix + "GLEDGER" +
                                    " where AC_CODE=" + txtAcCode.Text + " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and YEAR_CODE<=" + Convert.ToInt32(Session["year"].ToString()) + " group by AC_CODE");
                        //string balance = clsCommon.getString("select SUM(case DRCR when 'D' then AMOUNT when 'C' then -AMOUNT end) as Balance from " + tblPrefix + "GLEDGER" +
                        //                " where AC_CODE=" + txtAcCode.Text + " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE");
                        txtBalance.Text = balance;
                        setFocusControl(txtUnitCode);
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

    protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpFilter.SelectedValue == "A")
            {
                pnlAcNameWise.Visible = true;
                pnlBSGroupWise.Visible = false;
                setFocusControl(txtAcCode);
            }
            else
            {
                pnlAcNameWise.Visible = false;
                pnlBSGroupWise.Visible = true;
                setFocusControl(txtBSGroupCode);
            }
        }
        catch
        {

        }
    }

    protected void txtBSGroupCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBSGroupCode.Text != string.Empty)
            {
                searchStr = txtBSGroupCode.Text;
                strTextbox = "txtBSGroupCode";
                string str = clsCommon.getString("select [group_Name_E] from " + tblPrefix + "BSGroupMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [group_Code]=" + txtBSGroupCode.Text);
                if (str != string.Empty)
                {
                    lblGroupName.Text = str;
                    setFocusControl(txtFromDt);
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    txtBSGroupCode.Text = string.Empty;
                    lblGroupName.Text = string.Empty;
                    setFocusControl(txtBSGroupCode);
                }
            }
        }
        catch
        {

        }
    }

    private void csCalculations()
    {
        try
        {

        }
        catch
        {

        }
    }

    protected void btnGroupCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBSGroupCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }

    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].Width = new Unit("60px");
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //    e.Row.Cells[1].Width = new Unit("300px");
        //    e.Row.Cells[2].Width = new Unit("100px");
        //    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        //}
        try
        {
            int i = 0;
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                if (v == "txtUnitCode")
                {
                    e.Row.Cells[0].Width = new Unit("150px");
                    e.Row.Cells[1].Width = new Unit("550px");
                    e.Row.Cells[2].Width = new Unit("250px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                }
                if (v == "txtAcCode")
                {
                    e.Row.Cells[0].Width = new Unit("150px");
                    e.Row.Cells[1].Width = new Unit("550px");
                    e.Row.Cells[2].Width = new Unit("250px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                }
                if (v == "txtBSGroupCode")
                {
                    e.Row.Cells[0].Width = new Unit("150px");
                    e.Row.Cells[1].Width = new Unit("550px");

                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                }
            } 
        }
        catch
        {

        }
    }

    protected void btnGroupWise_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (txtAcCode.Text == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            if (txtUnitCode.Text != string.Empty)
            {
                if (txtAcCode.Text == string.Empty)
                {
                    setFocusControl(txtAcCode);
                    return;
                }
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;
                string unit_Code = txtUnitCode.Text;
                pnlPopup.Style["display"] = "none";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:gw('" + accode + "','" + unit_Code + "','" + fromdt + "','" + todt + "','DrCr')", true);
            }
            if (drpFilter.SelectedValue == "G")
            {
                string groupcode = txtBSGroupCode.Text;
                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp_multipleLedger('" + fromdt + "','" + todt + "','" + groupcode + "')", true);
            }
            else
            {
            }
        }
        catch (Exception eex)
        {

        }
    }


    protected void btnUnitWise_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (txtAcCode.Text == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            if (txtUnitCode.Text != string.Empty)
            {
                if (txtAcCode.Text == string.Empty)
                {
                    setFocusControl(txtAcCode);
                    return;
                }
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;
                string unit_Code = txtUnitCode.Text;
                pnlPopup.Style["display"] = "none";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:uw('" + accode + "','" + unit_Code + "','" + fromdt + "','" + todt + "')", true);
            }
            if (drpFilter.SelectedValue == "G")
            {
                string groupcode = txtBSGroupCode.Text;
                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp_multipleLedger('" + fromdt + "','" + todt + "','" + groupcode + "')", true);
            }
            else
            {
            }
        }
        catch (Exception eex)
        {

        }
    }
    protected void txtUnitCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtUnitCode.Text != string.Empty)
            {
                searchStr = txtUnitCode.Text;
                strTextbox = "txtUnitCode";
                bool a = clsCommon.isStringIsNumeric(txtUnitCode.Text);
                if (a == false)
                {
                    btnUnitCode_Click(this, new EventArgs());
                }
                else
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtUnitCode.Text);
                    if (str != string.Empty)
                    {
                        lblUnitName.Text = str;
                        setFocusControl(txtFromDt);
                        pnlPopup.Style["display"] = "none";
                    }
                    else
                    {
                        txtUnitCode.Text = string.Empty;
                        lblUnitName.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }


    protected void btnonacreport_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            string Transport = txtAcCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgysdaf", "javascript:onAc('" + fromdt + "','" + todt + "','" + Transport + "','')", true);
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnSBNo_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            string msgReturn = "";
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //if (hdnfAc.Value == "0" || hdnfAc.Value==string.Empty)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('AcCode Is Wroung!!!!!');", true);
            //    return;
            //}
            //else
            //{
            string Transport = txtAcCode.Text;
            if (Transport == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgysdaf", "javascript:SBNO('" + fromdt + "','" + todt + "','" + Transport + "','')", true);

            }
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnmultipleourdono_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            string msgReturn = "";
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string Transport = txtAcCode.Text;
            if (Transport == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgysdaf", "javascript:ourDoNO('" + fromdt + "','" + todt + "','" + Transport + "','')", true);
            }
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnUnitCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtUnitCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void btnOnlyCr_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (txtAcCode.Text == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;

                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kyasd", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','Cr')", true);
            }
            if (drpFilter.SelectedValue == "G")
            {
                string groupcode = txtBSGroupCode.Text;
                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kyasa", "javascript:sp_multipleLedger('" + fromdt + "','" + todt + "','" + groupcode + "')", true);
            }
            else
            {
            }
        }
        catch (Exception eex)
        {

        }
    }
    protected void btnOnlyDr_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (txtAcCode.Text == string.Empty)
            {
                setFocusControl(txtAcCode);
                return;
            }
            if (drpFilter.SelectedValue == "A")
            {
                string accode = txtAcCode.Text;

                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kasy", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','Dr')", true);
            }
            if (drpFilter.SelectedValue == "G")
            {
                string groupcode = txtBSGroupCode.Text;
                pnlPopup.Style["display"] = "none";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp_multipleLedger('" + fromdt + "','" + todt + "','" + groupcode + "')", true);
            }
            else
            {
            }
        }
        catch (Exception eex)
        {

        }
    }
    private void datefunction()
    {
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        }
        else
        {
            fromDT = DateTime.Parse(Session["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        }
        else
        {
            toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        }
    }
    protected void btnDispMillWise_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            string Mill_Code = txtAcCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "posiasd", "javascript:dispmillwise('" + fromDT + "','" + toDT + "','" + Mill_Code + "')", true);
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnShortPayment_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            string Broker_Code = txtAcCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ktasd", "javascript:bsp('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ksa", "javascript:bwspd('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ksasaa", "javascript:bwspdzero('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnTransportAc_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            string Transport = txtAcCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgysdaf", "javascript:TBR('" + fromDT + "','" + toDT + "','" + Transport + "','')", true);
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnDaywiseSummary_Click(object sender, EventArgs e)
    {
        datefunction();
        string ac_code = txtAcCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "poi", "javascript:DispSummary('" + fromDT + "','" + toDT + "','','" + ac_code + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnAccountWise_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            string ac_code = txtAcCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgyaf", "javascript:PWDO('" + fromDT + "','" + toDT + "','','" + ac_code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hassdaf", "javascript:PWDOM('" + fromDT + "','" + toDT + "','','" + ac_code + "')", true);
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnEnvelopPrint_Click(object sender, EventArgs e)
    {
        if (txtAcCode.Text.Trim().ToString() == string.Empty)
        {
            setFocusControl(txtAcCode);
            return;
        }

        string ac_code = txtAcCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asdasda", "javascript:envelop('" + ac_code + "')", true);
    }

    protected void btnFrtRegister_Click(object sender, EventArgs e)
    {
        //if (txtAcCode.Text.Trim().ToString() == string.Empty)
        //{
        //    setFocusControl(txtAcCode);
        //    return;
        //}
        //if (txtFromDt.Text != string.Empty)
        //{
        //    fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //}
        //else
        //{
        //    fromDT = DateTime.Parse(Session["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //}
        //if (txtToDt.Text != string.Empty)
        //{
        //    toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //}
        //else
        //{
        //    toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //}

        //string ac_code = txtAcCode.Text;
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asdas", "javascript:frtregi('" + ac_code + "','" + fromDT + "','" + toDT + "')", true);
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            fromDT = Session["Start_Date"].ToString();
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        }
        else
        {
            toDT = clsGV.End_Date;
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:frtregi('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnSaudaRegister_Click(object sender, EventArgs e)
    {
        if (txtAcCode.Text.Trim().ToString() == string.Empty)
        {
            setFocusControl(txtAcCode);
            return;
        }
        if (txtFromDt.Text != string.Empty)
        {
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        }
        else
        {
            fromDT = DateTime.Parse(Session["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        }
        if (txtToDt.Text != string.Empty)
        {
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        }
        else
        {
            toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        }

        string ac_code = txtAcCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asdasdsds", "javascript:srd('" + ac_code + "','" + fromDT + "','" + toDT + "')", true);
    }
    protected void btndobleSaleBill_Click(object sender, EventArgs e)
    {

    }

    protected void btnMultipleLedger_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            string type = drpFilter.SelectedValue;
            string groupcode = "0";

            if (type == "G")
            {
                if (txtBSGroupCode.Text.Trim() != string.Empty)
                {
                    groupcode = txtBSGroupCode.Text;
                }
            }


            pnlPopup.Style["display"] = "none";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kyasd", "javascript:party('" + type + "','" + fromdt + "','" + todt + "','DrCr','" + groupcode + "')", true);
        }
        catch (Exception eex)
        {

        }

    }
    protected void btncashdiffledger_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            string Transport = txtAcCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgysdaf", "javascript:cashdiffledger('" + fromdt + "','" + todt + "','" + Transport + "','DrCr')", true);
            pnlPopup.Style["display"] = "none";
        }
        catch (Exception)
        {
            throw;
        }
    }

    //protected void btnPendingSauda_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    string fromdt = txtFromDt.Text;
    //    //    string todt = txtToDt.Text;
    //    //    fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
    //    //    todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

    //    //   // string type = drpFilter.SelectedValue;
    //    //    //string groupcode = "0";

    //    //    //if (type == "G")
    //    //    //{
    //    //    //    if (txtBSGroupCode.Text.Trim() != string.Empty)
    //    //    //    {
    //    //    //        groupcode = txtBSGroupCode.Text;
    //    //    //    }
    //    //    //}


    //    //    pnlPopup.Style["display"] = "none";
    //    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kyasd", "javascript:pendingsauda('" + fromdt + "','" + todt + "')", true);
    //    //}
    //    //catch (Exception eex)
    //    //{

    //    //}

    //}
    protected void btnDespatchdetail_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            string fromdt1 = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string todt1 = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            string accode = txtAcCode.Text;

            pnlPopup.Style["display"] = "none";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:despatchdetail('" + accode + "','" + fromdt1 + "','" + todt1 + "','DrCr')", true);


        }
        catch (Exception eex)
        {

        }
    }
    #region [btnGetData_Click]
    protected void btnLedgerForCrsyatal_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            string fromdt1 = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string todt1 = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            string accode = txtAcCode.Text;

            pnlPopup.Style["display"] = "none";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:LedgerCrystal('" + accode + "','" + fromdt1 + "','" + todt1 + "','DrCr')", true);


        }
        catch (Exception eex)
        {

        }
    }
    #endregion
}