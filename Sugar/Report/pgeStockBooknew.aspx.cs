using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Report_pgeStockBooknew : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    //string qry = string.Empty;
    string searchStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                txtFromDt.Text = /*DateTime.Now.ToString("dd/MM/yyyy");*/ DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                txtToDt.Text = /*DateTime.Now.ToString("dd/MM/yyyy");*/ DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    protected void btnStockBook_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:sb('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btnItemwisestockdetail_Click(object sender, EventArgs e)
    {
        try
        {
            string fromDT = "";
            string toDT = "";
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids", "javascript:Itemwisestock('" + fromDT + "','" + toDT + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnReturnStockBook_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:rsb('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btnstocksummarynew_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:stocksummary('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btnReturnStockSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromDT = "";
            string toDT = "";
            fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids", "javascript:rss('" + fromDT + "','" + toDT + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnRetailStock_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kika", "javascript:sbr('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnonlyretailstock_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nn", "javascript:ors('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnundiliveredreport_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "gg", "javascript:udr('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btndiliveredreport_Click(object sender, EventArgs e)
    { 
         string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dr", "javascript:dr('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btncashreceive_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cr", "javascript:cr('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btndetailsatockreport_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        string purno = txtpurno.Text;
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cr", "javascript:StockDetail('" + fromDT + "','" + toDT + "','" + purno + "')", true);
    }
    protected void btncashnotreceive_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cnr", "javascript:cnr('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnpurcCreditReport_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cnr", "javascript:PRC('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btnMillWiseStockDeatil_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";

        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cnr", "javascript:MStockDeatil('" + fromDT + "','" + toDT + "')", true);
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
                        setFocusControl(txtFromDt);
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

    protected void txtbrandcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtbrandcode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtbrandcode.Text);
                if (a == false)
                {
                    searchStr = txtbrandcode.Text;
                    btnBrandcode_Click(this, new EventArgs());
                }
                else
                {
                    searchStr = txtbrandcode.Text;
                    strTextbox = "txtbrandcode";
                    string str = clsCommon.getString("select Marka from qryBrand_Master where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Code=" + txtbrandcode.Text);
                    if (str != string.Empty)
                    {
                        Label2.Text = str;
                        setFocusControl(txtFromDt);
                        pnlPopup.Style["display"] = "none";

                    }
                    else
                    {
                        txtbrandcode.Text = string.Empty;
                        Label2.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void txtpurno_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtFromDt);
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

    protected void btnpurno_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtpurno";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void btnBrandcode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtbrandcode";
            btnSearch_Click(sender, e);
        }
        catch
        {
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
                txtSearchText.Text = searchStr;
            }
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code , Ac_Name_E ,Short_Name ,CityName from qrymstaccountmaster where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtbrandcode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Code as [ Code], Marka as [BrandName],English_Name as [ English_Name] from qryBrand_Master where  (Code like '%" + txtSearchText.Text + "%' or Marka like '%" + txtSearchText.Text + "%' or English_Name like '%" + txtSearchText.Text + "%' or Marka like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Marka asc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtpurno")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select PURCNO , doc_dateConverted ,Quantal ,rate from qrypurchaseheaddetail where (PURCNO like '%" + txtSearchText.Text + "%' or doc_dateConverted like '%" + txtSearchText.Text + "%' or Quantal like '%" + txtSearchText.Text + "%' or rate like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and PURCNO!=0  order by PURCNO asc";
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
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
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
               
            }
        }
        catch
        {
        }
    }
}