using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeAccountBill : System.Web.UI.Page
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
            // if (isAuthenticate == "1" || User_Type == "A")
            //{
            //    txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}
            //else
            //{
            //    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            //}
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
                        //setFocusControl(txtFromDt);
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
        setFocusControl(txtFromBill_No);
    }

    protected void btnFromBill_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtFromBill_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }


    protected void btnToBill_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtToBill_No";
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



    protected void btnMultiple_Bill_Click(object sender, EventArgs e)
    {
        Int32 Accode = 0;

        if (txtAcCode.Text != string.Empty || txtAcCode.Text != "")
        {
            Accode = Convert.ToInt32(txtAcCode.Text);

        }
        Int32 FromBillNo = Convert.ToInt32(txtFromBill_No.Text);
        Int32 ToBillNo = Convert.ToInt32(txtToBill_No.Text);
        if (txtAcCode.Text != string.Empty || txtAcCode.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:SB('" + Accode + "','" + FromBillNo + "','" + ToBillNo + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ugyy", "javascript:SB1('" + FromBillNo + "','" + ToBillNo + "')", true);

        }
    }

    protected void btndetailsalebillreport_Click(object sender, EventArgs e)
    {
        Int32 Accode = 0;

        if (txtAcCode.Text != string.Empty || txtAcCode.Text != "")
        {
            Accode = Convert.ToInt32(txtAcCode.Text);

        }
        Int32 FromBillNo = Convert.ToInt32(txtFromBill_No.Text);
        Int32 ToBillNo = txtToBill_No.Text != string.Empty ? Convert.ToInt32(txtToBill_No.Text) : 0;
        // Int32 ToBillNo = Convert.ToInt32(txtToBill_No.Text);
        if (txtAcCode.Text != string.Empty || txtAcCode.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:SBD('" + Accode + "','" + FromBillNo + "','" + ToBillNo + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ugyy", "javascript:SBD1('" + FromBillNo + "','" + ToBillNo + "')", true);

        }
    }


    protected void btnstock_Book_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids", "javascript:Stockbook('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnLoss_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids", "javascript:Loss('" + fromDT + "','" + toDT + "')", true);
    }


    protected void btnProfit_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids", "javascript:Profit('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnsaleregisterMSOMS_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids1", "javascript:MSOMS('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnjaggeryBalNetWt_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids", "javascript:Bnetwt('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnjawaksalebill_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kids1", "javascript:jawaksb('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnpurchasestock_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        string tdsrate = txttds_rate.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nm", "javascript:PS('" + fromDT + "','" + toDT + "','" + tdsrate + "')", true);
    }

    protected void btnpurchasestockGst_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        string tdsrate = txttds_rate.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nm", "javascript:PSGST('" + fromDT + "','" + toDT + "','" + tdsrate + "')", true);
    }

    protected void btntds_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        string tdsrate = txttds_rate.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tds", "javascript:tdsreport('" + fromDT + "','" + toDT + "','" + tdsrate + "')", true);
    }

    protected void btnmarketses_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ses", "javascript:marketses('" + fromDT + "','" + toDT + "')", true);
    }

    protected void btnPendingBill_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        //fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ss", "javascript:PB('" + fromDT + "','" + toDT + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ss", "javascript:PB()", true);

    }

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
                string qry = "select Ac_Code as [Account Code], Ac_Name_E as [Account Name],Short_Name as [Short Name] from " + AccountMasterTable + " where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";

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
        string From_BillNo = txtFromBill_No.Text;
        string To_BillNo = txtToBill_No.Text;
        //if (txtFromDt.Text != string.Empty)
        //{
        //    fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    fromDT = clsGV.Start_Date;
        //}
        //if (txtToDt.Text != string.Empty)
        //{
        //    toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    toDT = clsGV.End_Date;
        //}
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:pr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnSaleReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:sr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnBillWiseCustomer_Click(object sender, EventArgs e)
    {
        Int32 FromBillNo = Convert.ToInt32(txtFromBill_No.Text);
        Int32 ToBillNo = Convert.ToInt32(txtToBill_No.Text);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "shalaka", "javascript:BillCheck('" + FromBillNo + "','" + ToBillNo + "')", true);
    }
    protected void btnReturnPurchaseReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        //if (txtFromDt.Text != string.Empty)
        //{
        //    fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    fromDT = clsGV.Start_Date;
        //}
        //if (txtToDt.Text != string.Empty)
        //{
        //    toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    toDT = clsGV.End_Date;
        //}
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:rpr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }

    protected void btnAllBill_Click(object sender, EventArgs e)
    {

        Int32 FromBillNo = Convert.ToInt32(txtFromBill_No.Text);
        Int32 ToBillNo = Convert.ToInt32(txtToBill_No.Text);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SM('" + FromBillNo + "','" + ToBillNo + "')", true);
    }
    protected void btnReturnSaleReport_Click(object sender, EventArgs e)
    {
        string Ac_Code = txtAcCode.Text;
        string fromDT = "";
        string toDT = "";
        //if (txtFromDt.Text != string.Empty)
        //{
        //    fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    fromDT = clsGV.Start_Date;
        //}
        //if (txtToDt.Text != string.Empty)
        //{
        //    toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    toDT = clsGV.End_Date;
        //}
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:rsr('" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        pnlPopup.Style["display"] = "none";
    }
    protected void btnJaggrySaleRegisterWithQty_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saleqty", "javascript:jawaksbqty('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btnpurchaseRegisterTDS_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        string tdsrate = txttds_rate.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nm", "javascript:PRTDS('" + fromDT + "','" + toDT + "','" + tdsrate + "')", true);
    }

    protected void btnpurchaseRegisterWithoutTDS_Click(object sender, EventArgs e)
    {
        string fromDT = "";
        string toDT = "";
        fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        string Ac_Code = txtAcCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nm", "javascript:PRWTDS('" + fromDT + "','" + toDT + "','" + Ac_Code + "')", true);
    }
}