using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Sugar_Report_rptFNO : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string searchStr = string.Empty;
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string AccountMasterTable = string.Empty;
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
                txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
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
        #region
        //try
        //{
        //    pnlPopup.Style["display"] = "block";
        //    string searchtxt = searchStr;
        //    string delimStr = "";
        //    char[] delimiter = delimStr.ToCharArray();
        //    string words = searchStr;
        //    string[] split = null;
        //    string name = string.Empty;
        //    if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
        //    {
        //        txtSearchText.Text = searchStr;
        //        searchtxt = txtSearchText.Text;
        //        words = txtSearchText.Text;
        //        split = words.Split(delimiter);
        //    }
        //    else
        //    {
        //        txtSearchText.Text = txtSearchText.Text;
        //        searchtxt = txtSearchText.Text;
        //        words = txtSearchText.Text;
        //        split = words.Split(delimiter);
        //    }
        //    if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
        //    {
        //        txtSearchText.Text = searchStr;
        //    }
        //    else
        //    {
        //        txtSearchText.Text = txtSearchText.Text;
        //    }
        //    if (hdnfClosePopup.Value == "txtAcCode")
        //    {
        //        foreach (var s in split)
        //        {
        //            string aa = s.ToString();
        //            //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
        //            name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
        //        }
        //        name = name.Remove(name.Length - 3);
        //        lblPopupHead.Text = "--Select Account--";
        //        string qry = "select Ac_Code,Ac_Name_E from " + tblHead + " where (" + name + ") order by Ac_Name_E asc";
        //        this.showPopup(qry);
        //    }
        //}
        //catch
        //{

        //}
#endregion
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

                string qry = "SELECT a.Ac_Code, a.Ac_Name_E,sum(case when l.drcr='D' then l.AMOUNT else -l.amount end) as Balance FROM nt_1_bsgroupmaster g INNER JOIN  " +
                         " nt_1_accountmaster a ON g.bsid = a.bsid INNER JOIN nt_1_gledger l ON a.accoid = l.ac where (a.Ac_Code like'%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%')  and l.Company_Code='"
                          + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  group by  a.Ac_Code, a.Ac_Name_E";

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
        string ac_code = txtAcCode.Text;
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:pr('" + fromDT + "','" + toDT + "','" + ac_code + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnSaleReport_Click(object sender, EventArgs e)
    {
        string ac_code = txtAcCode.Text;
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + fromDT + "','" + toDT + "','" + ac_code + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnStockBookDetailReport_Click(object sender, EventArgs e)
    {
        string ac_code = txtAcCode.Text;
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sbdr('" + fromDT + "','" + toDT + "','" + ac_code + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    
}