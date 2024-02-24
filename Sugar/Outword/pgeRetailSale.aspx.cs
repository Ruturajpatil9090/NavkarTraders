using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
public partial class pgeRetailSale : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string trntype = string.Empty;
    static WebControl objAsp = null;
    int Detail_Id = 2;
    int Item_Code = 3;
    int Itemaname = 4;
    int Brand_Code = 5;
    int brandname = 6;
    int Qty = 7;
    int Wtper = 8;
    int Netkg = 9;
    int Rate = 10;
    int Salerate = 11;
    int Value = 12;
    int Market_Cess = 13;
    int Super_Cost = 14;
    int Packing = 15;
    int Hamali = 16;
    int Freight = 17;
    int Other = 18;
    int Taxable_Amount = 19;
    int GST_Code = 20;
    int gstrate = 21;
    int cgstrate = 22;
    int cgstamt = 23;
    int sgstrate = 24;
    int sgstamt = 25;
    int igstrate = 26;
    int igstamt = 27;

    int GST = 28;
    int Net_Value = 29;
    int Rowaction = 30;
    int Srno = 31;

    string IsDelete = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Retail_Head";
            tblDetails = "Retail_Detail";
            qryCommon = "qryRetailSale";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    string Action = Request.QueryString["Action"];
                    hdnfdrpVal.Value = Request.QueryString["tran_type"];
                    trntype = hdnfdrpVal.Value;
                    drpCashCredit.SelectedValue = hdnfdrpVal.Value;

                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["retailid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        pnlgrdDetail.Enabled = true;
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";

                        this.showLastRecord();
                        this.enableDisableNavigateButtons();
                        setFocusControl(btnEdit);
                    }
                    else
                    {
                        btnAdd_Click(sender, e);
                    }
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
                if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
                {
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    pnlPopup.Style["display"] = "block";
                    objAsp = btnSearch;
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where ";
                obj.code = "Doc_no";
                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    txtDoc_No.Text = ds.Tables[0].Rows[0][0].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion
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
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                //drpTran_Type.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtChallan_No.Enabled = false;
                txtChallan_Date.Enabled = false;
                CalendarExtenderDatetxtChallan_Date.Enabled = false;
                txtVahical_No.Enabled = false;
                txtParty_Code.Enabled = false;
                btntxtParty_Code.Enabled = false;
                btntxtShipto.Enabled = false;
                txtDue_Days.Enabled = false;
                txtTDS.Enabled = false;
                txtTDSAmt.Enabled = false;
                txtDue_date.Enabled = false;
                txtTotal.Enabled = false;
                drpCashCredit.Enabled = true;
                //txtVat.Enabled = false;
                txtRoundoff.Enabled = false;
                btnmillcode.Enabled = false;
                drpDelivered.Enabled = false;

                txtTaxable_Amount.Enabled = false;
                txtParty_Name.Enabled = false;
                txtBroker_Code.Enabled = false;
                btntxtBroker_Code.Enabled = false;
                drpCashRecieve.Enabled = false;
                txtParty_name_new.Enabled = false;
                txtNarration.Enabled = false;
                txtTCS_Rate.Enabled = false;
                txtTCS_Amount.Enabled = false;
                txtTCS_Net_Payble.Enabled = false;
                txtNewSBNo.Enabled = false;
                txtNewSBDate.Enabled = false;
                CalendarExtenderDatetxtNewSBDate.Enabled = false;
                txtEnvoiceno.Enabled = false;
                txtACK.Enabled = false;
                txtNetGST.Enabled = false;
                txtNetExp.Enabled = false;
                txtNetWeight.Enabled = false;
                txtNetValue.Enabled = false;
                txtNetHamali.Enabled = false;
                txtNetpacking.Enabled = false;
                txtNetOther.Enabled = false;
                txtNetMarketsess.Enabled = false;
                txtNetSuperCost.Enabled = false;
                txtNetFrieght.Enabled = false;
                txtNetPayble.Enabled = false;
                txtEwayBillNo.Enabled = false;
                txtItemvalue.Enabled = false;
                //txtTran_Type.Enabled = false;
                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWtper.Enabled = false;
                txtNetkg.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtMarket_Cess.Enabled = false;
                txtSuper_Cost.Enabled = false;
                txtPacking.Enabled = false;
                txtHamali.Enabled = false;
                txtFreight.Enabled = false;
                txtOther.Enabled = false;
                txtTaxable_Amount.Enabled = false;
                txtGST_Code.Enabled = false;
                btntxtGST_Code.Enabled = false;
                btntxtParty_name_new.Enabled = false;
                txtGST.Enabled = false;
                txtNet_Value.Enabled = false;
                txtTotalCGST.Enabled = false;
                txtTotalSGST.Enabled = false;
                txtTotalIGST.Enabled = false;
                txtNetPayble.Text = "";
                txtCGST_Amount.Text = "";
                txtSGST_Amount.Text = "";
                txtIGST_Amount.Text = "";
                txtNetExp.Text = "";
                txtNetGST.Text = "";
                txtNetWeight.Text = "";
                txtNetValue.Text = "";
                txtNetExp.Text = "";
                txtTCS_Amount.Text = "";
                txtTCS_Rate.Text = "";
                txtTCS_Net_Payble.Text = "";
                txtNewSBDate.Text = "";
                txtNewSBNo.Text = "";
                txtEnvoiceno.Text = "";
                txtACK.Text = "";
                txtNetMarketsess.Text = "";
                txtNetSuperCost.Text = "";
                txtNetpacking.Text = "";
                txtOther.Text = "";
                txtHamali.Text = "";
                txtNetFrieght.Text = "";
                btntxtPurc_No.Enabled = false;
                btnBack.Enabled = true;
                txtTransportCode.Enabled = false;
                btnTransport.Enabled = false;

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
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                drpCashCredit.Enabled = false;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                //  txtTran_Type.Enabled = true;
                btnmillcode.Enabled = true;
                txtItem_Code.Enabled = true;
                lblItemname.Text = string.Empty;
                btntxtItem_Code.Enabled = true;
                btntxtShipto.Enabled = true;
                txtBrand_Code.Enabled = true;
                lblBrandname.Text = string.Empty;
                lblshipto.Text = string.Empty;
                lblpartyStatecode.Text = string.Empty;
                lblshiptostatecode.Text = string.Empty;
                btntxtBrand_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWtper.Enabled = true;
                txtNetkg.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtMarket_Cess.Enabled = true;
                txtSuper_Cost.Enabled = true;
                txtPacking.Enabled = true;
                txtHamali.Enabled = true;
                txtFreight.Enabled = true;
                txtOther.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtGST_Code.Enabled = true;
                lblgstname.Text = string.Empty;
                btntxtGST_Code.Enabled = true;
                btntxtParty_name_new.Enabled = true;
                txtGST.Enabled = true;
                txtNet_Value.Enabled = true;
                txtNetPayble.Enabled = true;
                txtEwayBillNo.Enabled = true;
                txtItemvalue.Enabled = true;
                //   drpTran_Type.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtChallan_No.Enabled = true;
                txtChallan_Date.Enabled = true;
                txtChallan_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtChallan_Date.Enabled = true;
                txtVahical_No.Enabled = true;
                txtParty_Code.Enabled = true;
                lblParty_Code.Text = string.Empty;
                btntxtParty_Code.Enabled = true;
                txtDue_Days.Enabled = true;
                txtDue_date.Enabled = true;
                txtTotal.Enabled = true;
                txtEwayBillNo.Text = string.Empty;
                txtACK.Text = string.Empty;
                txtEnvoiceno.Text = string.Empty;
                // txtVat.Enabled = true;
                txtRoundoff.Enabled = true;

                drpDelivered.Enabled = true;
                txtTDS.Enabled = true;
                txtTDSAmt.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtParty_Name.Enabled = true;
                txtBroker_Code.Enabled = true;
                lblbrokername.Text = string.Empty;
                btntxtBroker_Code.Enabled = true;
                drpCashRecieve.Enabled = true;
                txtParty_name_new.Enabled = true;
                txtNarration.Enabled = true;
                txtTCS_Rate.Enabled = true;
                txtTCS_Amount.Enabled = true;
                txtTCS_Net_Payble.Enabled = true;
                txtNewSBNo.Enabled = true;
                txtNewSBDate.Enabled = true;
                txtNewSBDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtNewSBDate.Enabled = true;
                txtEnvoiceno.Enabled = true;
                txtACK.Enabled = true;
                txtNetGST.Enabled = true;
                txtNetExp.Enabled = true;
                txtNetWeight.Enabled = true;
                txtNetValue.Enabled = true;
                txtNetPayble.Enabled = true;
                txtNetHamali.Enabled = true;
                txtNetpacking.Enabled = true;
                txtNetOther.Enabled = true;
                txtNetMarketsess.Enabled = true;
                txtNetSuperCost.Enabled = true;
                txtNetFrieght.Enabled = true;

                txtTotalCGST.Enabled = true;
                txtTotalSGST.Enabled = true;
                txtTotalIGST.Enabled = true;
                txtTCS_Rate.Text = Session["TCSRate"].ToString();

                if (drpCashCredit.SelectedValue != "CS")
                {

                    txtTDS.Text = Session["SaleTDSRate"].ToString();
                }
                else
                {
                    txtTDS.Text = "0.00";
                }
                txtTDSAmt.Text = "0.00";
                txtNetPayble.Text = "";
                txtCGST_Amount.Text = "";
                txtSGST_Amount.Text = "";
                txtIGST_Amount.Text = "";
                txtNetExp.Text = "";
                txtNetGST.Text = "";
                txtNetWeight.Text = "";
                txtNetValue.Text = "";
                txtNetExp.Text = "";
                txtTCS_Amount.Text = "";
                txtTCS_Rate.Text = "";
                txtTCS_Net_Payble.Text = "";
                txtNewSBDate.Text = "";
                txtNewSBNo.Text = "";
                txtEnvoiceno.Text = "";
                txtACK.Text = "";
                txtTotal.Text = "";
                txtTotalCGST.Text = "";
                txtTotalSGST.Text = "";
                txtTotalIGST.Text = "";
                txtTaxable_Amount.Text = "";
                txtNetMarketsess.Text = "";
                txtNetSuperCost.Text = "";
                txtNetpacking.Text = "";
                txtOther.Text = "";
                txtHamali.Text = "";
                txtNetFrieght.Text = "";
                #region set Business logic for save
                #endregion
                txtPurc_No.Enabled = false;
                btnBack.Enabled = false;
                btntxtPurc_No.Enabled = true;
                txtItemvalue.Text = string.Empty;
                txtRoundoff.Text = string.Empty;
                txtNetHamali.Text = string.Empty;
                txtNetOther.Text = string.Empty;

                txtTransportCode.Enabled = true;
                btnTransport.Enabled = true;
                lblTransportName.Text = string.Empty;
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                drpCashCredit.Enabled = true;
                lblMsg.Text = string.Empty;
                //   drpTran_Type.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtChallan_No.Enabled = false;
                txtChallan_Date.Enabled = false;
                CalendarExtenderDatetxtChallan_Date.Enabled = false;
                txtVahical_No.Enabled = false;
                txtParty_Code.Enabled = false;
                btntxtParty_Code.Enabled = false;
                btntxtShipto.Enabled = false;
                txtDue_Days.Enabled = false;
                txtDue_date.Enabled = false;
                txtTotal.Enabled = false;
                txtNetPayble.Enabled = false;
                txtEwayBillNo.Enabled = false;
                txtItemvalue.Enabled = false;
                // txtVat.Enabled = false;
                txtRoundoff.Enabled = false;

                drpDelivered.Enabled = false;

                txtTaxable_Amount.Enabled = false;
                txtParty_Name.Enabled = false;
                txtBroker_Code.Enabled = false;
                btntxtBroker_Code.Enabled = false;
                drpCashRecieve.Enabled = false;
                txtParty_name_new.Enabled = false;
                txtNarration.Enabled = false;
                txtTCS_Rate.Enabled = false;
                txtTCS_Amount.Enabled = false;
                txtTCS_Net_Payble.Enabled = false;
                txtNewSBNo.Enabled = false;
                txtNewSBDate.Enabled = false;
                CalendarExtenderDatetxtNewSBDate.Enabled = false;
                txtEnvoiceno.Enabled = false;
                txtACK.Enabled = false;
                txtNetGST.Enabled = false;
                txtNetExp.Enabled = false;
                txtNetWeight.Enabled = false;
                txtNetValue.Enabled = false;
                txtNetPayble.Enabled = false;
                // txtTran_Type.Enabled = false;
                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWtper.Enabled = false;
                txtNetkg.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtMarket_Cess.Enabled = false;
                txtSuper_Cost.Enabled = false;
                txtPacking.Enabled = false;
                txtHamali.Enabled = false;
                txtFreight.Enabled = false;
                txtOther.Enabled = false;
                txtTaxable_Amount.Enabled = false;
                txtGST_Code.Enabled = false;
                btntxtGST_Code.Enabled = false;
                btntxtParty_name_new.Enabled = false;
                txtGST.Enabled = false;
                txtNet_Value.Enabled = false;
                txtNetHamali.Enabled = false;
                txtNetpacking.Enabled = false;
                txtNetOther.Enabled = false;
                txtNetMarketsess.Enabled = false;
                txtNetSuperCost.Enabled = false;
                txtNetFrieght.Enabled = false;
                btnmillcode.Enabled = false;
                txtTDS.Enabled = false;
                txtTDSAmt.Enabled = false;
                // txtTran_Type.Text = string.Empty;
                txtItem_Code.Text = string.Empty;
                btntxtItem_Code.Enabled = false;
                txtBrand_Code.Text = string.Empty;
                btntxtBrand_Code.Enabled = false;
                txtQty.Text = string.Empty;
                txtWtper.Text = string.Empty;
                txtNetkg.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtValue.Text = string.Empty;
                txtMarket_Cess.Text = string.Empty;
                txtSuper_Cost.Text = string.Empty;
                txtPacking.Text = string.Empty;
                txtHamali.Text = string.Empty;
                txtFreight.Text = string.Empty;
                txtOther.Text = string.Empty;
                lblmill.Text = string.Empty;
                txtTaxable_Amount.Text = string.Empty;
                txtGST_Code.Text = string.Empty;
                btntxtGST_Code.Enabled = false;
                txtGST.Text = string.Empty;
                txtNet_Value.Text = string.Empty;
                txtTotalCGST.Enabled = false;
                txtTotalSGST.Enabled = false;
                txtTotalIGST.Enabled = false;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnBack.Enabled = true;
                btntxtPurc_No.Enabled = false;
                txtTransportCode.Enabled = false;
                btnTransport.Enabled = false;
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                // drpTran_Type.Enabled = true;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtChallan_No.Enabled = true;
                txtChallan_Date.Enabled = true;
                CalendarExtenderDatetxtChallan_Date.Enabled = true;
                txtVahical_No.Enabled = true;
                txtParty_Code.Enabled = true;
                btntxtShipto.Enabled = true;
                btntxtParty_Code.Enabled = true;
                txtDue_Days.Enabled = true;
                txtDue_date.Enabled = true;
                txtTotal.Enabled = true;
                btnmillcode.Enabled = true;
                //    txtSubtotal.Enabled = true;
                //txtVat.Enabled = true;
                txtRoundoff.Enabled = true;
                //  txtGrand_total.Enabled = true;
                drpDelivered.Enabled = true;
                txtNetPayble.Enabled = true;
                txtEwayBillNo.Enabled = true;
                txtItemvalue.Enabled = true;
                btnmillcode.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtParty_Name.Enabled = true;
                txtBroker_Code.Enabled = true;
                btntxtBroker_Code.Enabled = true;
                drpCashRecieve.Enabled = true;
                txtParty_name_new.Enabled = true;
                txtNarration.Enabled = true;
                txtTCS_Rate.Enabled = true;
                txtTCS_Amount.Enabled = true;
                txtTCS_Net_Payble.Enabled = true;
                txtNewSBNo.Enabled = true;
                txtNewSBDate.Enabled = true;
                CalendarExtenderDatetxtNewSBDate.Enabled = true;
                txtEnvoiceno.Enabled = true;
                txtACK.Enabled = true;
                txtNetGST.Enabled = true;
                txtNetExp.Enabled = true;
                txtNetWeight.Enabled = true;
                txtNetValue.Enabled = true;
                drpCashCredit.Enabled = false;
                txtNetPayble.Enabled = true;
                txtNetHamali.Enabled = false;
                txtNetpacking.Enabled = false;
                txtNetOther.Enabled = false;
                txtNetMarketsess.Enabled = false;
                txtNetSuperCost.Enabled = false;
                txtNetFrieght.Enabled = false;
                txtTDS.Enabled = true;
                txtTDSAmt.Enabled = true;
                //txtTran_Type.Enabled = true;
                txtItem_Code.Enabled = true;
                btntxtItem_Code.Enabled = true;
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWtper.Enabled = true;
                txtNetkg.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtMarket_Cess.Enabled = true;
                txtSuper_Cost.Enabled = true;
                txtPacking.Enabled = true;
                txtHamali.Enabled = true;
                txtFreight.Enabled = true;
                txtOther.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtGST_Code.Enabled = true;
                btntxtGST_Code.Enabled = true;
                btntxtParty_name_new.Enabled = true;
                txtGST.Enabled = true;
                txtTotalCGST.Enabled = true;
                txtTotalSGST.Enabled = true;
                txtTotalIGST.Enabled = true;
                txtNet_Value.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btnBack.Enabled = false;
                btntxtPurc_No.Enabled = true;

                txtTransportCode.Enabled = true;
                btnTransport.Enabled = true;
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion
    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                this.enableDisableNavigateButtons();
                btnAdd.Focus();
            }
            else                     //new code
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [enableDisableNavigateButtons]

    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.Text + "' ";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(query);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RecordCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
        }
        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
        }
        if (txtDoc_No.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [Retailid] from " + tblHead + " where retailid>" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.Text + "' ORDER BY Retailid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                        }
                    }
                }
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [Retailid] from " + tblHead + " where retailid<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.Text + "'  ORDER BY Retailid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnPrevious.Enabled = true;
                            btnFirst.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                            btnFirst.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Retailid from " + tblHead + " where Retailid=(select MIN(Retailid) from " +
               tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.SelectedValue + "') " +
               " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.SelectedValue + "'";

            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion
    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Retailid] from " + tblHead + " where Retailid< " + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.SelectedValue + "' ORDER BY Retailid desc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Next]
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Retailid] from " + tblHead + " where Retailid > " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " " +
                    " and CashCredit='" + drpCashCredit.SelectedValue + "' ORDER BY Retailid asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Last]
    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Retailid from " + tblHead + " where Retailid=(select MAX(Retailid) from " + tblHead +
     " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
     " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.SelectedValue + "') " +
     " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
     " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and CashCredit='" + drpCashCredit.SelectedValue + "'";
            hdnf.Value = clsCommon.getString(query);
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("A");
        txtDue_Days.Text = "0";
        DateTime d = new DateTime();
        // d = DateTime.Now;
        string date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        //  d = DateTime.Parse(date);
        // d = d.AddDays(15);
        if (txtDue_Days.Text != string.Empty)
        {

            txtDue_date.Text = clsCommon.getString("select Convert(varchar(10),DATEADD(day," + txtDue_Days.Text + ",'" + date + "'),103) as d");
        }
        else
        {
            txtDue_Days.Text = "0";
            txtDue_date.Text = txtDoc_Date.Text;
        }
        setFocusControl(txtDue_date);
        setFocusControl(txtParty_Code);
        pnlPopupDetails.Style["display"] = "none";
        drpDelivered.SelectedValue = "0";
        this.NextNumber();
    }
    #endregion
    private void NextNumber()
    {
        try
        {
            int Doc_No = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and  Year_Code='" + Session["Year"].ToString() + "'  and CashCredit='" + drpCashCredit.Text + "'")) + 1;
            txtDoc_No.Text = Doc_No.ToString();

        }
        catch
        {
        }
    }

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_Date);
        btnGenEinvoice.Enabled = false;
        btnGentare_EWayBill.Enabled = false;
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string max = clsCommon.getString("select max(Retailid) as id from Retail_Head where Company_Code=" + Session["Company_Code"].ToString() + " " +
                  " and Year_Code=" + Session["year"].ToString() + " and CashCredit='" + drpCashCredit.SelectedValue + "'");
        hdnf.Value = max;

        trntype = drpCashCredit.SelectedValue;

        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
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
                        string yearcode = Session["year"].ToString();
                        if (hdnfyearcode.Value != yearcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same !')", true);
                            return false;
                        }
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
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
                        //txtEditDoc_No.Text = dt.Rows[0]["EditDoc_No"].ToString();
                        hdnf.Value = dt.Rows[0]["Retailid"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnfdoc.Value = txtDoc_No.Text;
                        hdnfid.Value = hdnf.Value;
                        //lblDoc_No.Text = dt.Rows[0][""].ToString();
                        //drpTran_Type.SelectedValue=dt.Rows[0]["Tran_Type"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["DocDate_Converted"].ToString();
                        txtChallan_No.Text = dt.Rows[0]["Challan_No"].ToString();
                        txtChallan_Date.Text = dt.Rows[0]["ChallanDate_Converted"].ToString();
                        txtVahical_No.Text = dt.Rows[0]["Vahical_No"].ToString();
                        txtParty_Code.Text = dt.Rows[0]["Party_Code"].ToString();
                        lblpartyStatecode.Text = dt.Rows[0]["GSTStateCode"].ToString();
                        hdnfpartystatecode.Value = dt.Rows[0]["GSTStateCode"].ToString();
                        lblParty_Code.Text = dt.Rows[0]["partyname"].ToString();
                        txtDue_Days.Text = dt.Rows[0]["Due_Days"].ToString();
                        txtDue_date.Text = dt.Rows[0]["Due_date_Converted"].ToString();
                        txtTotal.Text = dt.Rows[0]["Total"].ToString();
                        //txtSubtotal.Text = dt.Rows[0]["Subtotal"].ToString();
                        //txtVat.Text = dt.Rows[0]["Vat"].ToString();
                        txtRoundoff.Text = dt.Rows[0]["Roundoff"].ToString();
                        //  txtGrand_total.Text = dt.Rows[0]["Grand_total"].ToString();
                        drpDelivered.SelectedValue = dt.Rows[0]["Delivered"].ToString();
                        txtTotalCGST.Text = dt.Rows[0]["CGST_Amount"].ToString();
                        txtTotalSGST.Text = dt.Rows[0]["SGST_Amount"].ToString();
                        txtTotalIGST.Text = dt.Rows[0]["IGST_Amount"].ToString();

                        txtTaxable_Amount.Text = dt.Rows[0]["Taxable_Amount"].ToString();
                        txtParty_Name.Text = dt.Rows[0]["Party_Name"].ToString();
                        txtBroker_Code.Text = dt.Rows[0]["Broker_Code"].ToString();
                        lblbrokername.Text = dt.Rows[0]["brokername"].ToString();
                        txtParty_name_new.Text = dt.Rows[0]["Party_name_new"].ToString();
                        lblpartynamenew.Text = dt.Rows[0]["partynewname"].ToString();

                        txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                        txtTCS_Rate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCS_Amount.Text = dt.Rows[0]["TCS_Amount"].ToString();
                        txtTCS_Net_Payble.Text = dt.Rows[0]["TCS_Net_Payble"].ToString();
                        txtNewSBNo.Text = dt.Rows[0]["NewSBNo"].ToString();
                        txtNewSBDate.Text = dt.Rows[0]["NewSBDate"].ToString();
                        txtEnvoiceno.Text = dt.Rows[0]["Envoiceno"].ToString();
                        txtACK.Text = dt.Rows[0]["ACK"].ToString();
                        txtNetGST.Text = dt.Rows[0]["NetGST"].ToString();
                        txtNetExp.Text = dt.Rows[0]["NetExp"].ToString();
                        txtNetWeight.Text = dt.Rows[0]["NetWeight"].ToString();
                        txtNetValue.Text = dt.Rows[0]["NetValue"].ToString();
                        txtNetHamali.Text = dt.Rows[0]["NetHamali"].ToString();
                        txtNetFrieght.Text = dt.Rows[0]["NetFrieght"].ToString();
                        txtNetSuperCost.Text = dt.Rows[0]["NetSuperCost"].ToString();
                        txtNetOther.Text = dt.Rows[0]["NetOther"].ToString();
                        txtNetpacking.Text = dt.Rows[0]["NetPacking"].ToString();
                        txtNetMarketsess.Text = dt.Rows[0]["NetMarketSess"].ToString();
                        txtNetPayble.Text = dt.Rows[0]["NetPayble"].ToString();
                        hdnfnetPayble.Value = dt.Rows[0]["NetPayble"].ToString();
                        txtItemvalue.Text = dt.Rows[0]["Item_value"].ToString();
                        txtEwayBillNo.Text = dt.Rows[0]["EwayBillNo"].ToString();
                        txtShipto.Text = dt.Rows[0]["Shipto"].ToString();
                        lblshipto.Text = dt.Rows[0]["shiptoacname"].ToString();
                        lblshiptostatecode.Text = dt.Rows[0]["shiptostatecode"].ToString();
                        hdnfpc.Value = dt.Rows[0]["pc"].ToString();
                        hdnfst.Value = dt.Rows[0]["st"].ToString();
                        hdnfbc.Value = dt.Rows[0]["bc"].ToString();
                        hdnfpcn.Value = dt.Rows[0]["pcn"].ToString();

                        txtTransportCode.Text = dt.Rows[0]["Transport_Code"].ToString();
                        lblTransportName.Text = dt.Rows[0]["Transport_Name"].ToString();
                        hdnftc.Value = dt.Rows[0]["tc"].ToString();
                        txtTDS.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();

                        if (txtEwayBillNo.Text == string.Empty || txtEwayBillNo.Text == "0")
                        {
                            btnGentare_EWayBill.Enabled = true;
                        }
                        else
                        {
                            btnGentare_EWayBill.Enabled = false;
                        }
                        if (txtEnvoiceno.Text == string.Empty && txtACK.Text == string.Empty || txtEnvoiceno.Text == "0" && txtACK.Text == "0")
                        {
                            btnGenEinvoice.Enabled = true;
                        }
                        else
                        {
                            btnGenEinvoice.Enabled = false;
                        }
                        string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                        if (User_Type == "A")
                        {
                            if (txtEnvoiceno.Text != string.Empty && txtACK.Text != string.Empty)
                            {
                                btnCancleEinvoice.Enabled = true;
                            }
                            else
                            {
                                btnCancleEinvoice.Enabled = false;
                            }
                        }
                        else
                        {
                            btnCancleEinvoice.Enabled = false;
                        }
                        txtTime.Text = dt.Rows[0]["time"].ToString();
                        string checkvalue = dt.Rows[0]["CashRecieve"].ToString();
                        drpCashRecieve.Checked = Convert.ToBoolean(checkvalue);
                        IsDelete = dt.Rows[0]["IsDelete"].ToString();

                        recordExist = true;
                        //lblMsg.Text = "";
                        #region Details
                        qry = "SELECT Detail_Id ,Item_Code,Item_Code_Name ,Brand_Code ,Brand_Code_Name,Qty ,Wtper ,Netkg ,Rate,isnull(Salerate ,0) as Salerate,Value ,Market_Cess ,Super_Cost ,Packing" +
                            ",Hamali ,Freight ,Other ,Taxable_Amount1 as Taxable_Amount ,GST_Code,Gstrate as GST_Code_Name,CGSTRate, CGSTAmount,SGSTRate,SGSTAmount, IGSTRate, IGSTAmount,GST ,Net_Value, " +
                            "'' as rowAction,'' as SrNo,ic,retaildid,saleac,sid,PurcNo,MillCode,millname as MillName,mc,PD_ID from " + qryCommon +
                            " where Retailid=" + hdnf.Value + " and Company_Code=" + Session["Company_Code"].ToString() +
                            " and Year_Code=" + Session["year"].ToString() + " and CashCredit='" + drpCashCredit.SelectedValue + "'";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    // dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    //dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    grdDetail.DataSource = dt;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = dt;
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            //   hdnf.Value = txtDoc_No.Text;
            //  this.enableDisableNavigateButtons();
            if (IsDelete == "0")
            {
                lblMsg.Text = "Delete";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
                btnGenEinvoice.Enabled = false;
                btnGentare_EWayBill.Enabled = false;
            }
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
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }

            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    txtChallan_Date.Text = txtDoc_Date.Text;
                    txtDue_date.Text = txtDoc_Date.Text;

                    //DateTime d = new DateTime();
                    // d = DateTime.Now;
                    string date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    //  d = DateTime.Parse(date);
                    // d = d.AddDays(15);
                    if (txtDue_Days.Text != string.Empty)
                    {

                        txtDue_date.Text = clsCommon.getString("select Convert(varchar(10),DATEADD(day," + txtDue_Days.Text + ",'" + date + "'),103) as d");
                    }
                    else
                    {
                        txtDue_Days.Text = "0";
                        txtDue_date.Text = txtDoc_Date.Text;
                    }
                    //string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    ////if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtDoc_Date);
                    //}
                    //else
                    //{
                    //    txtDoc_Date.Text = "";
                    //    setFocusControl(txtDoc_Date);
                    //}
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtChallan_No")
            {
                setFocusControl(txtChallan_No);
            }
            if (strTextBox == "txtChallan_Date")
            {
                try
                {
                    //string dt = DateTime.Parse(txtChallan_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtChallan_Date);
                    //}
                    //else
                    //{
                    //    txtChallan_Date.Text = "";
                    //    setFocusControl(txtChallan_Date);
                    //}
                }
                catch
                {
                    txtChallan_Date.Text = "";
                    setFocusControl(txtChallan_Date);
                }
            }
            if (strTextBox == "txtVahical_No")
            {
                setFocusControl(txtVahical_No);
            }
            if (strTextBox == "txtParty_Code")
            {
                string acname = "";
                if (txtParty_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtParty_Code.Text);
                    if (a == false)
                    {
                        btntxtParty_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblParty_Code.Text = acname;
                            string acnamestatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfpartystatecode.Value = acnamestatecode;
                            if (acnamestatecode == "0")
                            {
                                lblpartyStatecode.Text = "";
                                lblshiptostatecode.Text = "";
                            }
                            else
                            {

                                lblpartyStatecode.Text = acnamestatecode;
                                lblshiptostatecode.Text = acnamestatecode;
                            }

                            string partcode = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfpc.Value = partcode;
                            setFocusControl(drpDelivered);
                            txtShipto.Text = txtParty_Code.Text;
                            lblshipto.Text = acname;
                            hdnfst.Value = partcode;

                            if (txtParty_Code.Text == "1")
                            {
                                drpCashRecieve.Checked = true;
                            }
                            else
                            {
                                drpCashRecieve.Checked = false;
                            }



                        }
                        else
                        {
                            txtParty_Code.Text = string.Empty;
                            lblParty_Code.Text = acname;
                            setFocusControl(txtParty_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtParty_Code);
                }
            }
            if (strTextBox == "txtmillcode")
            {
                string acname = "";
                if (txtmillcode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtmillcode.Text);
                    if (a == false)
                    {
                        btnmillcode_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtmillcode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblmill.Text = acname;
                            // string acnamestatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            hdnfmillid.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtmillcode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            setFocusControl(txtNarration);



                        }
                        else
                        {
                            txtmillcode.Text = string.Empty;
                            lblmill.Text = acname;
                            setFocusControl(txtmillcode);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtmillcode);
                }
            }
            if (strTextBox == "txtShipto")
            {
                string acname = "";
                if (txtShipto.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtShipto.Text);
                    if (a == false)
                    {
                        btntxtShipto_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtShipto.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblshipto.Text = acname;
                            string acnamestatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtShipto.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            if (acnamestatecode == "0")
                            {
                                lblshiptostatecode.Text = "";
                            }
                            else
                            {
                                lblshiptostatecode.Text = acnamestatecode;
                            }
                            hdnfst.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtShipto.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            //  lblpartyStatecode.Text = acnamestatecode;

                            setFocusControl(txtDue_Days);

                        }
                        else
                        {
                            txtShipto.Text = string.Empty;
                            lblshipto.Text = acname;
                            setFocusControl(txtShipto);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtShipto);
                }
            }
            if (strTextBox == "txtParty_name_new")
            {
                string acname = "";
                if (txtParty_name_new.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtParty_name_new.Text);
                    if (a == false)
                    {
                        btntxtParty_name_new_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_type='CR' and Ac_Code=" + txtParty_name_new.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblpartynamenew.Text = acname;
                            hdnfpcn.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtParty_name_new.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            setFocusControl(txtNarration);

                        }
                        else
                        {
                            txtParty_name_new.Text = string.Empty;
                            lblpartynamenew.Text = acname;
                            setFocusControl(txtParty_name_new);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtParty_name_new);
                }
            }
            if (strTextBox == "txtTransportCode")
            {
                string acname = "";
                if (txtTransportCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtTransportCode.Text);
                    if (a == false)
                    {
                        btnTransport_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            hdnftc.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtTransportCode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                            lblTransportName.Text = acname;
                            setFocusControl(txtParty_Name);


                        }
                        else
                        {
                            txtTransportCode.Text = string.Empty;
                            lblTransportName.Text = acname;
                            setFocusControl(txtParty_Name);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtTransportCode);
                }
            }
            if (strTextBox == "txtDue_Days")
            {
                DateTime d = new DateTime();
                // d = DateTime.Now;
                string date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                //  d = DateTime.Parse(date);
                // d = d.AddDays(15);
                if (txtDue_Days.Text != string.Empty)
                {

                    txtDue_date.Text = clsCommon.getString("select Convert(varchar(10),DATEADD(day," + txtDue_Days.Text + ",'" + date + "'),103) as d");
                }
                else
                {
                    txtDue_Days.Text = "0";
                    txtDue_date.Text = txtDoc_Date.Text;
                }
                setFocusControl(txtDue_date);
            }

            if (strTextBox == "txtTotal")
            {
                setFocusControl(txtTotal);
            }


            if (strTextBox == "txtRoundoff")
            {
                setFocusControl(txtRoundoff);
            }

            if (strTextBox == "drpDelivered")
            {
                setFocusControl(drpDelivered);
            }

            if (strTextBox == "txtTaxable_Amount")
            {
                setFocusControl(txtTaxable_Amount);
            }
            if (strTextBox == "txtParty_Name")
            {
                setFocusControl(txtParty_Name);
            }
            if (strTextBox == "txtBroker_Code")
            {
                string acname = "";
                if (txtBroker_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBroker_Code.Text);
                    if (a == false)
                    {
                        btntxtBroker_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtBroker_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblbrokername.Text = acname;
                            string address = clsCommon.getString("select Address_E from qrymstaccountmaster where Ac_Code=" + txtBroker_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string city = clsCommon.getString("select CityName from qrymstaccountmaster where Ac_Code=" + txtBroker_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblbrokername.Text = address + " <br/>" + city;
                            hdnfbc.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtBroker_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            setFocusControl(txtParty_Name);

                        }
                        else
                        {
                            txtBroker_Code.Text = "0";
                            lblbrokername.Text = acname;
                            setFocusControl(txtParty_Name);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtParty_Name);
                }
            }
            //if (strTextBox == "txtParty_name_new")
            //{
            //    setFocusControl(txtParty_name_new);
            //}
            if (strTextBox == "txtNarration")
            {
                setFocusControl(btntxtPurc_No);
            }
            if (strTextBox == "txtTCS_Rate")
            {
                setFocusControl(txtTCS_Rate);
            }
            if (strTextBox == "txtTCS_Amount")
            {
                setFocusControl(txtTCS_Amount);
            }
            if (strTextBox == "txtTCS_Net_Payble")
            {
                setFocusControl(txtTCS_Net_Payble);
            }
            if (strTextBox == "txtNewSBNo")
            {
                setFocusControl(txtNewSBNo);
            }
            if (strTextBox == "txtNewSBDate")
            {

            }
            if (strTextBox == "txtEnvoiceno")
            {
                setFocusControl(txtEnvoiceno);
            }
            if (strTextBox == "txtACK")
            {
                setFocusControl(txtACK);
            }
            if (strTextBox == "txtNetGST")
            {
                setFocusControl(txtNetGST);
            }
            if (strTextBox == "txtNetExp")
            {
                setFocusControl(txtNetExp);
            }
            if (strTextBox == "txtNetWeight")
            {
                setFocusControl(txtNetWeight);
            }
            if (strTextBox == "txtNetValue")
            {
                setFocusControl(txtNetValue);
            }
            if (strTextBox == "txtNetPayble")
            {
                setFocusControl(txtNetPayble);
            }

            if (strTextBox == "txtItem_Code")
            {
                string acname = "";
                if (txtItem_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtItem_Code.Text);
                    if (a == false)
                    {
                        btntxtItem_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select System_Name_E from NT_1_SystemMaster where System_Type='I' and System_Code=" + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblItemname.Text = acname;

                            string marketsess = clsCommon.getString("select MarkaSet from NT_1_SystemMaster where System_Type='I' and System_Code="
                                + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (marketsess == "Y")
                            {
                                string marketrate = clsCommon.getString("select MarkaPerc from NT_1_SystemMaster where System_Type='I' and System_Code="
                                + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                hdnfmarketsessrate.Value = marketrate;
                            }
                            else
                            {
                                hdnfmarketsessrate.Value = "0";
                            }
                            string supercost = clsCommon.getString("select Supercost from NT_1_SystemMaster where System_Type='I' and System_Code="
                              + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (supercost == "Y")
                            {
                                string marketrate = clsCommon.getString("select SuperPerc from NT_1_SystemMaster where System_Type='I' and System_Code="
                                + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                hdnfsupercostrate.Value = marketrate;
                            }
                            else
                            {
                                hdnfsupercostrate.Value = "0";
                            }

                            string rateper = clsCommon.getString("select RatePer from NT_1_SystemMaster where System_Type='I' and System_Code="
                          + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            hdnfrateper.Value = rateper;

                            setFocusControl(txtBrand_Code);

                            string gstcode = clsCommon.getString("select Gst_Code from NT_1_SystemMaster where System_Type='I' and System_Code="
                            + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            txtGST_Code.Text = gstcode.ToString();

                            string gstrate = clsCommon.getString("select Rate from NT_1_GSTRateMaster where Doc_no="
                           + gstcode + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            lblgstname.Text = gstrate.ToString();
                            string includinggst = clsCommon.getString("select LodingGst from NT_1_SystemMaster where System_Type='I' and System_Code="
                            + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfincludinggst.Value = includinggst.ToString();
                            txtPurc_No.Text = hdnfpurcno.Value;
                        }
                        else
                        {
                            txtItem_Code.Text = string.Empty;
                            lblItemname.Text = acname;
                            setFocusControl(txtItem_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtItem_Code);
                }
            }
            if (strTextBox == "txtBrand_Code")
            {
                string acname = "";
                if (txtBrand_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBrand_Code.Text);
                    if (a == false)
                    {
                        btntxtBrand_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = clsDAL.SimpleQuery("select Marka,Wt_Per from Brand_Master where Code=" + txtBrand_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                lblBrandname.Text = dt.Rows[0]["Marka"].ToString();
                                setFocusControl(txtQty);
                                txtWtper.Text = dt.Rows[0]["Wt_Per"].ToString();
                            }
                            else
                            {
                                lblBrandname.Text = "";
                                setFocusControl(txtBrand_Code);

                            }
                        }
                        else
                        {
                            lblBrandname.Text = "";
                            setFocusControl(txtBrand_Code);
                        }


                    }
                }
                else
                {
                    setFocusControl(txtBrand_Code);
                }
            }
            if (strTextBox == "txtQty")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtWtper")
            {
                setFocusControl(txtNetkg);
            }
            if (strTextBox == "txtNetkg")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(btnAdddetails);
            }
            if (strTextBox == "txtValue")
            {
                setFocusControl(txtMarket_Cess);
            }
            if (strTextBox == "txtMarket_Cess")
            {
                setFocusControl(txtSuper_Cost);
            }
            if (strTextBox == "txtSuper_Cost")
            {
                setFocusControl(txtPacking);
            }
            if (strTextBox == "txtPacking")
            {
                setFocusControl(txtHamali);
            }
            if (strTextBox == "txtHamali")
            {
                setFocusControl(txtFreight);
            }
            if (strTextBox == "txtFreight")
            {
                setFocusControl(txtOther);
            }
            if (strTextBox == "txtOther")
            {
                setFocusControl(txtTaxable_Amount1);
            }
            if (strTextBox == "txtTaxable_Amount")
            {
                setFocusControl(txtGST_Code);
            }
            if (strTextBox == "txtGST_Code")
            {
                string acname = "";
                if (txtGST_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGST_Code.Text);
                    if (a == false)
                    {
                        btntxtGST_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Rate from NT_1_GSTRateMaster where Doc_no=" + txtGST_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblgstname.Text = acname;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:Calculations('9')", true);

                            if (txtQty.Text == string.Empty)
                            {
                                setFocusControl(txtQty);
                            }
                            else
                            {
                                setFocusControl(txtCGST_Rate);
                            }

                        }
                        else
                        {
                            txtGST_Code.Text = string.Empty;
                            lblgstname.Text = acname;
                            setFocusControl(txtGST_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtGST_Code);
                }
            }
            if (strTextBox == "txtGST")
            {
                setFocusControl(txtGST);
            }
            if (strTextBox == "txtNet_Value")
            {
                setFocusControl(txtNet_Value);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region calculation
    private void calculation()
    {
        double Qty = txtQty.Text != string.Empty ? Convert.ToDouble(txtQty.Text) : 0.00;
        double Wtper = txtWtper.Text != string.Empty ? Convert.ToDouble(txtWtper.Text) : 0.00;
        double Netkg = txtNetkg.Text != string.Empty ? Convert.ToDouble(txtNetkg.Text) : 0.00;
        double mainrate = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0.00;
        double Rate = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0.00;
        double Value = txtValue.Text != string.Empty ? Convert.ToDouble(txtValue.Text) : 0.00;
        double Market_Cess = txtMarket_Cess.Text != string.Empty ? Convert.ToDouble(txtMarket_Cess.Text) : 0.00;
        double Super_Cost = txtSuper_Cost.Text != string.Empty ? Convert.ToDouble(txtSuper_Cost.Text) : 0.00;
        double Packing = txtPacking.Text != string.Empty ? Convert.ToDouble(txtPacking.Text) : 0.00;
        double Hamali = txtHamali.Text != string.Empty ? Convert.ToDouble(txtHamali.Text) : 0.00;
        double Freight = txtFreight.Text != string.Empty ? Convert.ToDouble(txtFreight.Text) : 0.00;
        double Other = txtOther.Text != string.Empty ? Convert.ToDouble(txtOther.Text) : 0.00;
        double Taxable_Amount = txtTaxable_Amount1.Text != string.Empty ? Convert.ToDouble(txtTaxable_Amount1.Text) : 0.00;
        double GST = txtGST.Text != string.Empty ? Convert.ToDouble(txtGST.Text) : 0.00;
        double Net_Value = txtNet_Value.Text != string.Empty ? Convert.ToDouble(txtNet_Value.Text) : 0.00;
        double GSTRATE = lblgstname.Text != string.Empty ? Convert.ToDouble(lblgstname.Text) : 0.00;
        double cgstrate = txtCGST_Rate.Text.Trim() != string.Empty ? Convert.ToDouble(txtCGST_Rate.Text) : 0.00;
        double cgstamt = txtCGST_Amount.Text.Trim() != string.Empty ? Convert.ToDouble(txtCGST_Amount.Text) : 0.00;
        double sgstrate = txtSGST_Rate.Text.Trim() != string.Empty ? Convert.ToDouble(txtSGST_Rate.Text) : 0.00;
        double sgstamt = txtSGST_Amount.Text.Trim() != string.Empty ? Convert.ToDouble(txtSGST_Amount.Text) : 0.00;
        double igstrate = txtIGST_Rate.Text.Trim() != string.Empty ? Convert.ToDouble(txtIGST_Rate.Text) : 0.00;
        double igstamt = txtIGST_Amount.Text.Trim() != string.Empty ? Convert.ToDouble(txtIGST_Amount.Text) : 0.00;
        double tcsrate = txtTCS_Rate.Text != string.Empty ? Convert.ToDouble(txtTCS_Rate.Text) : 0.00;
        string rateper = clsCommon.getString("select RatePer from NT_1_SystemMaster where System_Type='I' and System_Code="
        + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string includinggst = clsCommon.getString("select LodingGst from NT_1_SystemMaster where System_Type='I' and System_Code="
                           + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        hdnfincludinggst.Value = includinggst.ToString();

        hdnfrateper.Value = rateper;
        int partystatecode = Convert.ToInt32(hdnfpartystatecode.Value);
        int companystatecode = Convert.ToInt32(Session["CompanyGSTStateCode"]);
        double netvaluewithgst = 0.00;
        double salerate = 0.00;
        double rate1 = 0.00;
        tcsrate = Convert.ToDouble(Session["TCSRate"]);
        Netkg = Qty * Wtper;
        rate1 = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0.00;
        salerate = rate1;
        double itemvalue = 0.00;

        if (includinggst == "Y")
        {
            double a = Rate * (GSTRATE / 100);
            double b = a + Rate;
            double c = Rate / b;
            Rate = Math.Round(c * Rate, 2);
            salerate = Rate;

        }

        if (hdnfrateper.Value == "Q")
        {
            Value = Math.Round(Qty * Rate, 2);
            itemvalue = Math.Round((Netkg / 100) * rate1, 2);

        }
        else
        {
            Value = Math.Round((Netkg / 100) * Rate, 2);
            itemvalue = Math.Round((Netkg / 100) * rate1, 2);
        }
        //netvaluewithgst = Math.Round(Qty * mainrate, 2);
        double market = txtMarket_Cess.Text != string.Empty ? Convert.ToDouble(txtMarket_Cess.Text.Trim()) : 0.00;
        double super = txtSuper_Cost.Text != string.Empty ? Convert.ToDouble(txtSuper_Cost.Text.Trim()) : 0.00;

        if (market > 0)
        {
            Market_Cess = Math.Round(Value * Convert.ToDouble(hdnfmarketsessrate.Value) / 100, 2);
        }
        else
        {
            Market_Cess = 0;
        }
        if (super > 0)
        {
            Super_Cost = Math.Round(Value * Convert.ToDouble(hdnfsupercostrate.Value) / 100, 2);
        }
        else
        {
            Super_Cost = 0;
        }
        Taxable_Amount = Math.Round(Value + Market_Cess + Super_Cost + Packing + Hamali + Freight + Other, 2);

        if (companystatecode == partystatecode)
        {
            cgstrate = GSTRATE / 2;
            sgstrate = GSTRATE / 2;
            cgstamt = Math.Round(Taxable_Amount * cgstrate / 100, 2);
            sgstamt = Math.Round(Taxable_Amount * sgstrate / 100, 2);

        }
        else
        {
            igstrate = GSTRATE;
            igstamt = Math.Round(Taxable_Amount * igstrate / 100, 2);

        }

        GST = cgstamt + sgstamt + igstamt;
        //Taxable_Amount =  Math.Round(GST + Market_Cess + Super_Cost + Packing + Hamali + Freight + Other, 2);
        txtTaxable_Amount.Text = Taxable_Amount.ToString();

        txtNetkg.Text = Netkg.ToString();
        txtValue.Text = (itemvalue - (cgstamt + sgstamt + igstamt)).ToString();
        Taxable_Amount = Math.Round(itemvalue - (cgstamt + sgstamt + igstamt) + Market_Cess + Super_Cost + Packing + Hamali + Freight + Other, 2);
        Net_Value = Math.Round(Taxable_Amount + GST, 2);

        // txtValue.Text = Math.Round(Value, 2).ToString();
        txtRate.Text = rate1.ToString();

        txtsalerate.Text = salerate.ToString();
        // txtValue.Text = Taxable_Amount.ToString();
        txtMarket_Cess.Text = Market_Cess.ToString();
        txtSuper_Cost.Text = Super_Cost.ToString();
        txtTaxable_Amount1.Text = Taxable_Amount.ToString();
        txtGST.Text = GST.ToString();
        txtNet_Value.Text = Net_Value.ToString();
        txtCGST_Rate.Text = cgstrate.ToString();
        txtCGST_Amount.Text = cgstamt.ToString();
        txtSGST_Rate.Text = sgstrate.ToString();
        txtSGST_Amount.Text = sgstamt.ToString();
        txtIGST_Rate.Text = igstrate.ToString();
        txtIGST_Amount.Text = igstamt.ToString();

    }
    #endregion

    #region
    private void gridcalculation()
    {
        double netgst = 0.00;
        double totalvalue = 0.00;
        double netvalue = 0.00;
        double Totaltaxable = 0.00;
        double netwt = 0.00;
        double cgstamt1 = 0.00;
        double sgstamt1 = 0.00;
        double Igstamt1 = 0.00;
        double tcsnetpayable = 0.00;
        double tcsrate = txtTCS_Rate.Text != string.Empty ? Convert.ToDouble(txtTCS_Rate.Text) : 0.00;
        double tdsrate = txtTDS.Text != string.Empty ? Convert.ToDouble(txtTDS.Text) : 0.00;

        double nethamali = 0.00;
        double netmarketsess = 0.00;
        double netother = 0.00;
        double netFrieght = 0.00;
        double netpacking = 0.00;
        double netsupercost = 0.00;
        double netExp = 0.00;
        if (grdDetail.Rows.Count > 0)
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[Rowaction].Text != "D")
                {
                    double gst = Convert.ToDouble(grdDetail.Rows[i].Cells[GST].Text.Trim());
                    netgst = netgst + gst;
                    double value = Convert.ToDouble(grdDetail.Rows[i].Cells[Value].Text.Trim());
                    totalvalue = totalvalue + value;
                    double valuenet = Convert.ToDouble(grdDetail.Rows[i].Cells[Net_Value].Text.Trim());
                    netvalue = netvalue + valuenet;
                    double taxable = Convert.ToDouble(grdDetail.Rows[i].Cells[Taxable_Amount].Text.Trim());
                    Totaltaxable = Totaltaxable + taxable;
                    double wt = Convert.ToDouble(grdDetail.Rows[i].Cells[Wtper].Text.Trim());
                    netwt = netwt + wt;
                    double cgst = Convert.ToDouble(grdDetail.Rows[i].Cells[cgstamt].Text.Trim());
                    cgstamt1 = cgstamt1 + cgst;
                    double sgst = Convert.ToDouble(grdDetail.Rows[i].Cells[sgstamt].Text.Trim());
                    sgstamt1 = sgstamt1 + sgst;
                    double igst = Convert.ToDouble(grdDetail.Rows[i].Cells[igstamt].Text.Trim());
                    Igstamt1 = Igstamt1 + igst;
                    double packing = Convert.ToDouble(grdDetail.Rows[i].Cells[Packing].Text.Trim());
                    netpacking = netpacking + packing;

                    double other = Convert.ToDouble(grdDetail.Rows[i].Cells[Other].Text.Trim());
                    netother = netother + other;

                    double marketsess = Convert.ToDouble(grdDetail.Rows[i].Cells[Market_Cess].Text.Trim());
                    netmarketsess = netmarketsess + marketsess;

                    double supercost = Convert.ToDouble(grdDetail.Rows[i].Cells[Super_Cost].Text.Trim());
                    netsupercost = netsupercost + supercost;
                    double FRT = Convert.ToDouble(grdDetail.Rows[i].Cells[Freight].Text.Trim());
                    netFrieght = netFrieght + FRT;
                    double hamali = Convert.ToDouble(grdDetail.Rows[i].Cells[Hamali].Text.Trim());
                    nethamali = nethamali + hamali;
                }
            }


            double tcsamt = 0.00;
            if (tcsrate != 0)
            {
                tcsamt = Math.Round(((Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1) / 100) * tcsrate, 0);
            }
            else
            {
                tcsamt = 0.00;
            }

            double tdsamt = 0.00;
            if (tdsrate != 0)
            {
                tdsamt = Math.Round(((Totaltaxable) / 100) * tdsrate, 0);
            }
            else
            {
                tdsamt = 0.00;
            }
            tcsnetpayable = Totaltaxable + tcsamt;
            txtNetGST.Text = netgst.ToString();

            txtTotal.Text = totalvalue.ToString();
            txtItemvalue.Text = totalvalue.ToString();
            txtTaxable_Amount.Text = Totaltaxable.ToString();
            txtNetWeight.Text = netwt.ToString();
            txtTCS_Amount.Text = tcsamt.ToString();
            txtTDSAmt.Text = tdsamt.ToString();

            txtTotalIGST.Text = Igstamt1.ToString();
            txtTotalSGST.Text = sgstamt1.ToString();
            txtTotalCGST.Text = cgstamt1.ToString();
            txtTCS_Net_Payble.Text = tcsnetpayable.ToString();
            txtNetMarketsess.Text = netmarketsess.ToString();
            txtNetHamali.Text = nethamali.ToString();
            txtNetFrieght.Text = netFrieght.ToString();
            txtNetSuperCost.Text = netsupercost.ToString();
            txtNetpacking.Text = netpacking.ToString();
            double roundoff = 0.00;
            // int D=Convert.ToInt32(Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1);
            //   int e = (int)Math.Round(D, 0);

            double taxableround = Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1;
            // taxableround = double.Parse(taxableround.ToString().Split(',')[0]);
            //int d = (int)Math.Round(taxableround, 0);
            int result = (int)(Math.Floor(Convert.ToDouble(taxableround)));
            //  double taxableint = double.Truncate(taxableint);
            double b = Math.Round(Convert.ToDouble((Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1) - result), 2);
            double c = 0.00;
            string s = string.Empty;
            if (b < 0)
            {
                c = 1 + b;


            }
            else
            {
                if (b > 0)
                {
                    c = 1 - b;
                }
                else
                {
                    c = 0;
                }

            }
            roundoff = c;
            //if (roundoff > 0)
            //{
            //    roundoff = roundoff;
            //}
            //else
            //{
            //    roundoff = 0;
            //}
            s = Convert.ToString(c);
            s = s.Substring(0, 1);
            if (s == "0")
            {
                roundoff = roundoff;
            }
            else
            {
                roundoff = 0;
            }

            txtNetValue.Text = (netvalue + roundoff).ToString();
            txtRoundoff.Text = Math.Round(roundoff, 2).ToString();
            txtNetOther.Text = netother.ToString();

            txtNetPayble.Text = Convert.ToString(cgstamt1 + sgstamt1 + Igstamt1 + Totaltaxable + tcsamt + roundoff);
            //    txtNetGST.Text = netgst.ToString();
            txtNetExp.Text = (netFrieght + nethamali + netmarketsess + netsupercost + netother + netpacking).ToString();
            txtTaxable_Amount.Text = Totaltaxable.ToString();
        }
    }

    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Retailid=" + hdnf.Value + " and CashCredit='" + drpCashCredit.SelectedValue + "'";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion
    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                // txtDoc_No.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        pnlPopupDetails.Style["display"] = "block";
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["Detail_Id"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_Id"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["Detail_Id"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_Id"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from " + tblDetails +
                            " where Detail_Id='" + lblID.Text + "' and Doc_No=" + txtDoc_No.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                    #region [Write here columns]

                    dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Item_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Brand_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                    dt.Columns.Add((new DataColumn("Wtper", typeof(double))));
                    dt.Columns.Add((new DataColumn("Netkg", typeof(double))));
                    dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Salerate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Value", typeof(double))));
                    dt.Columns.Add((new DataColumn("Market_Cess", typeof(double))));
                    dt.Columns.Add((new DataColumn("Super_Cost", typeof(double))));
                    dt.Columns.Add((new DataColumn("Packing", typeof(double))));
                    dt.Columns.Add((new DataColumn("Hamali", typeof(double))));
                    dt.Columns.Add((new DataColumn("Freight", typeof(double))));
                    dt.Columns.Add((new DataColumn("Other", typeof(double))));
                    dt.Columns.Add((new DataColumn("Taxable_Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("GST_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("GST_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("CGSTRate", typeof(double))));
                    dt.Columns.Add((new DataColumn("CGSTAmount", typeof(double))));
                    dt.Columns.Add((new DataColumn("SGSTRate", typeof(double))));
                    dt.Columns.Add((new DataColumn("SGSTAmount", typeof(double))));
                    dt.Columns.Add((new DataColumn("IGSTRate", typeof(double))));
                    dt.Columns.Add((new DataColumn("IGSTAmount", typeof(double))));

                    dt.Columns.Add((new DataColumn("GST", typeof(double))));
                    dt.Columns.Add((new DataColumn("Net_Value", typeof(double))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("ic", typeof(int))));
                    dt.Columns.Add((new DataColumn("retaildid", typeof(int))));
                    dt.Columns.Add((new DataColumn("saleac", typeof(int))));
                    dt.Columns.Add((new DataColumn("sid", typeof(int))));

                    dt.Columns.Add((new DataColumn("PurcNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("MillCode", typeof(int))));
                    dt.Columns.Add((new DataColumn("MillName", typeof(string))));
                    dt.Columns.Add((new DataColumn("mc", typeof(int))));
                    dt.Columns.Add((new DataColumn("PD_ID", typeof(int))));

                    dr = dt.NewRow();
                    dr["Detail_Id"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Item_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Brand_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                dt.Columns.Add((new DataColumn("Wtper", typeof(double))));
                dt.Columns.Add((new DataColumn("Netkg", typeof(double))));
                dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Salerate", typeof(double))));
                dt.Columns.Add((new DataColumn("Value", typeof(double))));
                dt.Columns.Add((new DataColumn("Market_Cess", typeof(double))));
                dt.Columns.Add((new DataColumn("Super_Cost", typeof(double))));
                dt.Columns.Add((new DataColumn("Packing", typeof(double))));
                dt.Columns.Add((new DataColumn("Hamali", typeof(double))));
                dt.Columns.Add((new DataColumn("Freight", typeof(double))));
                dt.Columns.Add((new DataColumn("Other", typeof(double))));
                dt.Columns.Add((new DataColumn("Taxable_Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("GST_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("GST_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("CGSTRate", typeof(double))));
                dt.Columns.Add((new DataColumn("CGSTAmount", typeof(double))));
                dt.Columns.Add((new DataColumn("SGSTRate", typeof(double))));
                dt.Columns.Add((new DataColumn("SGSTAmount", typeof(double))));
                dt.Columns.Add((new DataColumn("IGSTRate", typeof(double))));
                dt.Columns.Add((new DataColumn("IGSTAmount", typeof(double))));
                dt.Columns.Add((new DataColumn("GST", typeof(double))));
                dt.Columns.Add((new DataColumn("Net_Value", typeof(double))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add((new DataColumn("ic", typeof(int))));
                dt.Columns.Add((new DataColumn("retaildid", typeof(int))));
                dt.Columns.Add((new DataColumn("saleac", typeof(int))));
                dt.Columns.Add((new DataColumn("sid", typeof(int))));

                dt.Columns.Add((new DataColumn("PurcNo", typeof(int))));
                dt.Columns.Add((new DataColumn("MillCode", typeof(int))));
                dt.Columns.Add((new DataColumn("MillName", typeof(string))));
                dt.Columns.Add((new DataColumn("mc", typeof(int))));
                dt.Columns.Add((new DataColumn("PD_ID", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            //dr["Tran_Type"] = txtTran_Type.Text;
            dr["Item_Code"] = txtItem_Code.Text;
            dr["Item_Code_Name"] = lblItemname.Text;
            dr["Brand_Code"] = txtBrand_Code.Text;
            dr["Brand_Code_Name"] = lblBrandname.Text;
            dr["Qty"] = txtQty.Text;
            dr["Wtper"] = txtWtper.Text;
            dr["Netkg"] = txtNetkg.Text;
            dr["Rate"] = txtRate.Text;
            dr["Salerate"] = txtsalerate.Text;

            dr["Value"] = txtValue.Text;
            dr["Market_Cess"] = txtMarket_Cess.Text;
            dr["Super_Cost"] = txtSuper_Cost.Text;
            dr["Packing"] = txtPacking.Text != string.Empty ? txtPacking.Text : "0";
            dr["Hamali"] = txtHamali.Text != string.Empty ? txtHamali.Text : "0";
            dr["Freight"] = txtFreight.Text != string.Empty ? txtFreight.Text : "0.00";
            dr["Other"] = txtOther.Text != string.Empty ? txtOther.Text : "0";
            dr["Taxable_Amount"] = txtTaxable_Amount1.Text;
            dr["GST_Code"] = txtGST_Code.Text;
            dr["GST_Code_Name"] = lblgstname.Text;
            dr["CGSTRate"] = txtCGST_Rate.Text != string.Empty ? txtCGST_Rate.Text : "0.00";
            dr["CGSTAmount"] = txtCGST_Amount.Text != string.Empty ? txtCGST_Amount.Text : "0.00";
            dr["SGSTRate"] = txtSGST_Rate.Text != string.Empty ? txtSGST_Rate.Text : "0.00";
            dr["SGSTAmount"] = txtSGST_Amount.Text != string.Empty ? txtSGST_Amount.Text : "0.00";
            dr["IGSTRate"] = txtIGST_Rate.Text != string.Empty ? txtIGST_Rate.Text : "0.00";
            dr["IGSTAmount"] = txtIGST_Amount.Text != string.Empty ? txtIGST_Amount.Text : "0.00";

            dr["GST"] = txtGST.Text;
            dr["Net_Value"] = txtNet_Value.Text;
            ds = clsDAL.SimpleQuery("select * from qrymstitem where System_Code=" + txtItem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            if (ds != null)
            {
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    dr["ic"] = Convert.ToInt32(dt1.Rows[0]["systemid"].ToString());
                    dr["saleac"] = Convert.ToInt32(dt1.Rows[0]["Sale_AC"].ToString());
                    dr["sid"] = Convert.ToInt32(dt1.Rows[0]["SaleAcid"].ToString());
                }

            }
            dr["PurcNo"] = txtPurc_No.Text;
            dr["MillCode"] = txtmillcode.Text;
            dr["MillName"] = lblmill.Text;
            Int32 mc = Convert.ToInt32(clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + txtmillcode.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
            dr["mc"] = mc;
            //Int32 PD_ID = Convert.ToInt32(clsCommon.getString("select isnull(purchasedetailid,0) as purchasedetailid from nt_1_sugarpurchasedetails where doc_no=" 
            //    + txtPurc_No.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + 
            //    " and Year_Code=" + Session["year"].ToString() + ""));
            Int32 PD_ID = 0;
            //Int32 PD_ID = Convert.ToInt32(clsCommon.getString("select isnull(purchasedetailid,0) as purchasedetailid from nt_1_sugarpurchasedetails where doc_no="
            // + txtPurc_No.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            // " and Year_Code=" + Session["year"].ToString() + ""));

            PD_ID = Convert.ToInt32(hdnfpurcid.Value);
            dr["PD_ID"] = PD_ID;
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["retaildid"] = 0;
                dt.Rows.Add(dr);
            }
            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
            ViewState["currentTable"] = dt;
            if (btnAdddetails.Text == "ADD")
            {
                pnlPopupDetails.Style["display"] = "none";
                //setFocusControl(txtTran_Type);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            //   txtTran_Type.Text = string.Empty;
            txtItem_Code.Text = string.Empty;
            lblItemname.Text = string.Empty;

            txtBrand_Code.Text = string.Empty;
            lblBrandname.Text = string.Empty;

            txtQty.Text = string.Empty;
            txtWtper.Text = string.Empty;
            txtNetkg.Text = string.Empty;
            txtsalerate.Text = string.Empty;

            txtValue.Text = string.Empty;
            txtMarket_Cess.Text = string.Empty;
            txtSuper_Cost.Text = string.Empty;
            txtPacking.Text = string.Empty;
            txtHamali.Text = string.Empty;
            txtFreight.Text = string.Empty;
            txtOther.Text = string.Empty;
            txtTaxable_Amount1.Text = string.Empty;
            txtGST_Code.Text = string.Empty;
            lblBrandname.Text = string.Empty;

            txtGST.Text = string.Empty;
            txtNet_Value.Text = string.Empty;

            txtCGST_Amount.Text = string.Empty;
            txtSGST_Amount.Text = string.Empty;
            txtIGST_Amount.Text = string.Empty;
            txtCGST_Rate.Text = string.Empty;
            txtSGST_Rate.Text = string.Empty;
            txtIGST_Rate.Text = string.Empty;

            txtPurc_No.Text = string.Empty;
            txtmillcode.Text = string.Empty;
            lblmill.Text = string.Empty;
            setFocusControl(btntxtPurc_No);
            btnAdddetails.Text = "ADD";
            gridcalculation();
            AmtCalculation();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:GridCalculations()", true);
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;
        //txtTran_Type.Text = string.Empty;
        txtItem_Code.Text = string.Empty;
        lblItemname.Text = string.Empty;

        txtBrand_Code.Text = string.Empty;
        lblBrandname.Text = string.Empty;

        txtQty.Text = string.Empty;
        txtWtper.Text = string.Empty;
        txtNetkg.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtValue.Text = string.Empty;
        txtMarket_Cess.Text = string.Empty;
        txtSuper_Cost.Text = string.Empty;
        txtPacking.Text = string.Empty;
        txtHamali.Text = string.Empty;
        txtFreight.Text = string.Empty;
        txtOther.Text = string.Empty;
        txtTaxable_Amount1.Text = string.Empty;
        txtGST_Code.Text = string.Empty;
        lblgstname.Text = string.Empty;

        txtGST.Text = string.Empty;
        txtNet_Value.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtItem_Code);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        //txtTran_Type.Text = Server.HtmlDecode(gvrow.Cells[Tran_Type].Text);
        txtItem_Code.Text = Server.HtmlDecode(gvrow.Cells[Item_Code].Text);
        string includinggst = clsCommon.getString("select LodingGst from NT_1_SystemMaster where System_Type='I' and System_Code="
                           + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        hdnfincludinggst.Value = includinggst.ToString();
        string marketsess = clsCommon.getString("select MarkaSet from NT_1_SystemMaster where System_Type='I' and System_Code="
                                + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        if (marketsess == "Y")
        {
            string marketrate = clsCommon.getString("select MarkaPerc from NT_1_SystemMaster where System_Type='I' and System_Code="
            + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            hdnfmarketsessrate.Value = marketrate;
        }
        else
        {
            hdnfmarketsessrate.Value = "0";
        }
        string supercost = clsCommon.getString("select Supercost from NT_1_SystemMaster where System_Type='I' and System_Code="
          + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        if (supercost == "Y")
        {
            string marketrate = clsCommon.getString("select SuperPerc from NT_1_SystemMaster where System_Type='I' and System_Code="
            + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            hdnfsupercostrate.Value = marketrate;
        }
        else
        {
            hdnfsupercostrate.Value = "0";
        }

        string rateper = clsCommon.getString("select RatePer from NT_1_SystemMaster where System_Type='I' and System_Code="
         + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        hdnfrateper.Value = rateper;
        lblItemname.Text = Server.HtmlDecode(gvrow.Cells[Itemaname].Text);

        txtBrand_Code.Text = Server.HtmlDecode(gvrow.Cells[Brand_Code].Text);
        lblBrandname.Text = Server.HtmlDecode(gvrow.Cells[brandname].Text.ToString());

        txtQty.Text = Server.HtmlDecode(gvrow.Cells[Qty].Text);
        txtWtper.Text = Server.HtmlDecode(gvrow.Cells[Wtper].Text);
        txtNetkg.Text = Server.HtmlDecode(gvrow.Cells[Netkg].Text);
        txtRate.Text = Server.HtmlDecode(gvrow.Cells[Rate].Text);
        txtsalerate.Text = Server.HtmlDecode(gvrow.Cells[Salerate].Text);
        txtValue.Text = Server.HtmlDecode(gvrow.Cells[Value].Text);
        txtMarket_Cess.Text = Server.HtmlDecode(gvrow.Cells[Market_Cess].Text);
        txtSuper_Cost.Text = Server.HtmlDecode(gvrow.Cells[Super_Cost].Text);
        txtPacking.Text = Server.HtmlDecode(gvrow.Cells[Packing].Text);
        txtHamali.Text = Server.HtmlDecode(gvrow.Cells[Hamali].Text);
        txtFreight.Text = Server.HtmlDecode(gvrow.Cells[Freight].Text);
        txtOther.Text = Server.HtmlDecode(gvrow.Cells[Other].Text);
        txtCGST_Rate.Text = Server.HtmlDecode(gvrow.Cells[cgstrate].Text.Trim());
        txtCGST_Amount.Text = Server.HtmlDecode(gvrow.Cells[cgstamt].Text.Trim());
        txtSGST_Rate.Text = Server.HtmlDecode(gvrow.Cells[sgstrate].Text.Trim());
        txtSGST_Amount.Text = Server.HtmlDecode(gvrow.Cells[sgstamt].Text.Trim());
        txtIGST_Rate.Text = Server.HtmlDecode(gvrow.Cells[igstrate].Text.Trim());
        txtIGST_Amount.Text = Server.HtmlDecode(gvrow.Cells[igstamt].Text.Trim());
        txtTaxable_Amount1.Text = Server.HtmlDecode(gvrow.Cells[Taxable_Amount].Text);

        txtGST_Code.Text = Server.HtmlDecode(gvrow.Cells[GST_Code].Text);
        lblgstname.Text = Server.HtmlDecode(gvrow.Cells[gstrate].Text);
        txtGST.Text = Server.HtmlDecode(gvrow.Cells[GST].Text);
        txtNet_Value.Text = Server.HtmlDecode(gvrow.Cells[Net_Value].Text);

        txtPurc_No.Text = Server.HtmlDecode(gvrow.Cells[36].Text);
        txtmillcode.Text = Server.HtmlDecode(gvrow.Cells[37].Text);
        lblmill.Text = Server.HtmlDecode(gvrow.Cells[38].Text);
        hdnfpurcid.Value = Server.HtmlDecode(gvrow.Cells[40].Text);
        txtPurc_No.Enabled = false;
    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_Id"].ToString());
                string IDExisting = clsCommon.getString("select Doc_No from " + tblDetails + " where Detail_Id='" + ID + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "N";// Add rowaction id
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
                gridcalculation();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            // e.Row.Cells[2].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------

            //--------------------------------------------------
            e.Row.Cells[Item_Code].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[Item_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Item_Code].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Itemaname].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Itemaname].Style["overflow"] = "hidden";
            e.Row.Cells[Itemaname].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Brand_Code].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[Brand_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Brand_Code].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[brandname].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[brandname].Style["overflow"] = "hidden";
            e.Row.Cells[brandname].HorizontalAlign = HorizontalAlign.Left;

            //--------------------------------------------------
            e.Row.Cells[Qty].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Qty].Style["overflow"] = "hidden";
            e.Row.Cells[Qty].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Wtper].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Wtper].Style["overflow"] = "hidden";
            e.Row.Cells[Wtper].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Netkg].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Netkg].Style["overflow"] = "hidden";
            e.Row.Cells[Netkg].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Rate].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Rate].Style["overflow"] = "hidden";
            e.Row.Cells[Rate].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Salerate].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Salerate].Style["overflow"] = "hidden";
            e.Row.Cells[Salerate].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Value].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Value].Style["overflow"] = "hidden";
            e.Row.Cells[Value].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Market_Cess].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Market_Cess].Style["overflow"] = "hidden";
            e.Row.Cells[Market_Cess].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Super_Cost].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Super_Cost].Style["overflow"] = "hidden";
            e.Row.Cells[Super_Cost].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Packing].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Packing].Style["overflow"] = "hidden";
            e.Row.Cells[Packing].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Hamali].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Hamali].Style["overflow"] = "hidden";
            e.Row.Cells[Hamali].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Freight].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Freight].Style["overflow"] = "hidden";
            e.Row.Cells[Freight].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Other].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Other].Style["overflow"] = "hidden";
            e.Row.Cells[Other].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Taxable_Amount].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[Taxable_Amount].Style["overflow"] = "hidden";
            e.Row.Cells[Taxable_Amount].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[GST_Code].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[GST_Code].Style["overflow"] = "hidden";
            e.Row.Cells[GST_Code].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[gstrate].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[gstrate].Style["overflow"] = "hidden";
            e.Row.Cells[gstrate].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[cgstrate].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[cgstrate].Style["overflow"] = "hidden";
            e.Row.Cells[cgstrate].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[cgstamt].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[cgstamt].Style["overflow"] = "hidden";
            e.Row.Cells[cgstamt].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[sgstrate].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[sgstrate].Style["overflow"] = "hidden";
            e.Row.Cells[sgstrate].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[sgstamt].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[sgstamt].Style["overflow"] = "hidden";
            e.Row.Cells[sgstamt].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[igstrate].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[igstrate].Style["overflow"] = "hidden";
            e.Row.Cells[igstrate].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[igstamt].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[igstamt].Style["overflow"] = "hidden";
            e.Row.Cells[igstamt].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[GST].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[GST].Style["overflow"] = "hidden";
            e.Row.Cells[GST].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Net_Value].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Net_Value].Style["overflow"] = "hidden";
            e.Row.Cells[Net_Value].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[32].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[32].Style["overflow"] = "hidden";
            e.Row.Cells[32].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[33].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[33].Style["overflow"] = "hidden";
            e.Row.Cells[33].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[34].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[34].Style["overflow"] = "hidden";
            e.Row.Cells[34].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[35].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[35].Style["overflow"] = "hidden";
            e.Row.Cells[35].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[36].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[36].Style["overflow"] = "hidden";
            e.Row.Cells[36].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[37].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[37].Style["overflow"] = "hidden";
            e.Row.Cells[37].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[38].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[38].Style["overflow"] = "hidden";
            e.Row.Cells[38].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[39].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[39].Style["overflow"] = "hidden";
            e.Row.Cells[39].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[40].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[40].Style["overflow"] = "hidden";
            e.Row.Cells[40].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            e.Row.Cells[Rowaction].Visible = true;
            e.Row.Cells[Srno].Visible = true;

            //}
        }
        catch
        {
        }
    }
    #endregion
    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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

                if (v == "txtBroker_Code")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                }
                if (v == "txtBrand_Code")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                }
                if (v == "txtBroker_Code")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                }
                if (v == "txtParty_Code")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                }
                if (v == "txtShipto")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                }
                if (v == "txtParty_name_new")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                }
                if (v == "txtmillcode")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("90px");
                }
                if (v == "txtItem_Code")
                {
                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("160px");

                }
                if (v == "txtGST_Code")
                {
                    e.Row.Cells[0].Width = new Unit("90px");
                    e.Row.Cells[1].Width = new Unit("90px");
                    e.Row.Cells[0].Width = new Unit("90px");

                }

                if (v == "txtPurc_No")
                {

                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("60px");
                    e.Row.Cells[2].Width = new Unit("160px");
                    e.Row.Cells[3].Width = new Unit("180px");
                    e.Row.Cells[4].Width = new Unit("100px");
                    e.Row.Cells[5].Width = new Unit("110px");
                    e.Row.Cells[6].Width = new Unit("10px");
                    e.Row.Cells[7].Width = new Unit("180px");
                    e.Row.Cells[8].Width = new Unit("50px");
                    e.Row.Cells[9].Width = new Unit("50px");
                    e.Row.Cells[10].Width = new Unit("60px");
                    e.Row.Cells[11].Width = new Unit("40px");
                    e.Row.Cells[12].Width = new Unit("60px");
                    e.Row.Cells[13].Width = new Unit("70px");
                    e.Row.Cells[14].Width = new Unit("30px");
                    e.Row.Cells[15].Width = new Unit("30px");
                    e.Row.Cells[16].Width = new Unit("30px");
                    e.Row.Cells[17].Width = new Unit("30px");
                    e.Row.Cells[18].Width = new Unit("30px");
                    e.Row.Cells[19].Width = new Unit("30px");

                }
            }
        }
        catch
        {

        }

    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        searchString = txtSearchText.Text;
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
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
    #region [RowCommand]
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[Rowaction].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "UPDATE";
                            setFocusControl(txtItem_Code);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);
                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Only Numbers Allow!')", true);
            }
            else
            {
                //string qry = "select * from " + qryHead + " where doc_no='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code="+Session["year"].ToString()+"";
                //this.fetchRecord(qry);
                //setFocusControl(txtEditDoc_No);

                string qry = "select * from " + qryCommon + " where doc_no='" + txtEditDoc_No.Text + "' and " +
                    "company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Session["year"].ToString() + " and CashCredit='" + drpCashCredit.SelectedValue + "'";
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);

            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();
    }
    #endregion
    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [drpTran_Type_TextChanged]
    protected void drpTran_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    #region [txtChallan_No_TextChanged]
    protected void txtChallan_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtChallan_No.Text;
        strTextBox = "txtChallan_No";
        csCalculations();
    }
    #endregion
    #region [txtChallan_Date_TextChanged]
    protected void txtChallan_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtChallan_Date.Text;
        strTextBox = "txtChallan_Date";
        csCalculations();
    }
    #endregion
    #region [txtVahical_No_TextChanged]
    protected void txtVahical_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVahical_No.Text;
        strTextBox = "txtVahical_No";
        csCalculations();
    }
    #endregion
    #region [txtParty_Code_TextChanged]
    protected void txtParty_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtParty_Code.Text;
        strTextBox = "txtParty_Code";
        csCalculations();
        AmtCalculation();
    }
    #endregion
    #region [btntxtParty_Code_Click]
    protected void btntxtParty_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtParty_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtDue_Days_TextChanged]
    protected void txtDue_Days_TextChanged(object sender, EventArgs e)
    {
        txtDue_Days.Text = "0" + txtDue_Days.Text.Trim();
        searchString = txtDue_Days.Text;
        strTextBox = "txtDue_Days";
        csCalculations();
    }
    #endregion
    #region [txtDue_date_TextChanged]
    protected void txtDue_date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDue_date.Text;
        strTextBox = "txtDue_date";
        setFocusControl(drpDelivered);
        //csCalculations();
    }
    #endregion
    #region [txtTotal_TextChanged]
    protected void txtTotal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotal.Text;
        strTextBox = "txtTotal";
        csCalculations();
    }
    #endregion


    #region [txtRoundoff_TextChanged]
    protected void txtRoundoff_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region [drpDelivered_TextChanged]
    protected void drpDelivered_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [txtTaxable_Amount_TextChanged]
    protected void txtTaxable_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTaxable_Amount.Text;
        strTextBox = "txtTaxable_Amount";
        csCalculations();
    }
    #endregion
    #region [txtParty_Name_TextChanged]
    protected void txtParty_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtParty_Name.Text;
        strTextBox = "txtParty_Name";
        setFocusControl(btntxtPurc_No);
        // csCalculations();
    }
    #endregion
    #region [txtBroker_Code_TextChanged]
    protected void txtBroker_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtBroker_Code.Text;
        strTextBox = "txtBroker_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtBroker_Code_Click]
    protected void btntxtBroker_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btnmillcode_Click]
    protected void btnmillcode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtmillcode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtParty_name_new_TextChanged]
    protected void txtParty_name_new_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtParty_name_new.Text;
        strTextBox = "txtParty_name_new";
        csCalculations();
    }
    #endregion
    #region [txtNarration_TextChanged]
    protected void txtNarration_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNarration.Text;
        strTextBox = "txtNarration";
        csCalculations();
    }
    #endregion
    #region [txtTCS_Rate_TextChanged]
    protected void txtTCS_Rate_TextChanged(object sender, EventArgs e)
    {
        hdnfTCSRate.Value = txtTCS_Rate.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:GridCalculations()", true);
        // gridcalculation();
    }
    #endregion
    #region [txtTCS_Amount_TextChanged]
    protected void txtTCS_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCS_Amount.Text;
        strTextBox = "txtTCS_Amount";
        csCalculations();
    }
    #endregion
    #region [txtTCS_Net_Payble_TextChanged]
    protected void txtTCS_Net_Payble_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCS_Net_Payble.Text;
        strTextBox = "txtTCS_Net_Payble";
        csCalculations();
    }
    #endregion
    #region [txtNewSBNo_TextChanged]
    protected void txtNewSBNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNewSBNo.Text;
        strTextBox = "txtNewSBNo";
        csCalculations();
    }
    #endregion
    #region [txtNewSBDate_TextChanged]
    protected void txtNewSBDate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNewSBDate.Text;
        strTextBox = "txtNewSBDate";
        csCalculations();
    }
    #endregion
    #region [txtEnvoiceno_TextChanged]
    protected void txtEnvoiceno_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEnvoiceno.Text;
        strTextBox = "txtEnvoiceno";
        csCalculations();
    }
    #endregion
    #region [txtACK_TextChanged]
    protected void txtACK_TextChanged(object sender, EventArgs e)
    {
        searchString = txtACK.Text;
        strTextBox = "txtACK";
        csCalculations();
    }
    #endregion
    #region [txtNetGST_TextChanged]
    protected void txtNetGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetGST.Text;
        strTextBox = "txtNetGST";
        csCalculations();
    }
    #endregion
    #region [txtNetExp_TextChanged]
    protected void txtNetExp_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetExp.Text;
        strTextBox = "txtNetExp";
        csCalculations();
    }
    #endregion
    #region [txtNetWeight_TextChanged]
    protected void txtNetWeight_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetWeight.Text;
        strTextBox = "txtNetWeight";
        csCalculations();
    }
    #endregion
    #region [txtNetValue_TextChanged]
    protected void txtNetValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetValue.Text;
        strTextBox = "txtNetValue";
        csCalculations();
    }
    #endregion
    #region [txtNetPayble_TextChanged]
    protected void txtNetPayble_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetPayble.Text;
        strTextBox = "txtNetPayble";
        csCalculations();
    }
    #endregion




    #region [txtItem_Code_TextChanged]
    protected void txtItem_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtItem_Code.Text;
        strTextBox = "txtItem_Code";
        csCalculations();
        setFocusControl(txtQty);
    }
    #endregion

    #region [btntxtItem_Code_Click]
    protected void btntxtItem_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtItem_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBrand_Code_TextChanged]
    protected void txtBrand_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtBrand_Code.Text;
        strTextBox = "txtBrand_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtBrand_Code_Click]
    protected void btntxtBrand_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBrand_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        if (txtQty.Text == "")
        {
            setFocusControl(txtQty);
        }
        else
        {
            setFocusControl(txtRate);
        }
        //csCalculations();
    }
    #endregion


    #region [txtWtper_TextChanged]
    protected void txtWtper_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWtper.Text;
        strTextBox = "txtWtper";
        csCalculations();
    }
    #endregion


    #region [txtNetkg_TextChanged]
    protected void txtNetkg_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetkg.Text;
        strTextBox = "txtNetkg";
        csCalculations();
    }
    #endregion


    #region [txtRate_TextChanged]
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        csCalculations();

    }
    #endregion


    #region [txtValue_TextChanged]
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtValue.Text;
        strTextBox = "txtValue";
        csCalculations();
    }
    #endregion


    #region [txtMarket_Cess_TextChanged]
    protected void txtMarket_Cess_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMarket_Cess.Text;
        strTextBox = "txtMarket_Cess";
        csCalculations();
    }
    #endregion

    #region [txtmillcode_TextChanged]
    protected void txtmillcode_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtmillcode.Text;
        strTextBox = "txtmillcode";
        csCalculations();
    }
    #endregion
    #region [txtSuper_Cost_TextChanged]
    protected void txtSuper_Cost_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSuper_Cost.Text;
        strTextBox = "txtSuper_Cost";
        csCalculations();
    }
    #endregion


    #region [txtPacking_TextChanged]
    protected void txtPacking_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPacking.Text;
        strTextBox = "txtPacking";
        csCalculations();
    }
    #endregion


    #region [txtHamali_TextChanged]
    protected void txtHamali_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali.Text;
        strTextBox = "txtHamali";
        csCalculations();
    }
    #endregion


    #region [txtFreight_TextChanged]
    protected void txtFreight_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFreight.Text;
        strTextBox = "txtFreight";
        csCalculations();
    }
    #endregion


    #region [txtOther_TextChanged]
    protected void txtOther_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOther.Text;
        strTextBox = "txtOther";
        csCalculations();
    }
    #endregion


    #region [txtTaxable_Amount_TextChanged]
    protected void txtTaxable_Amount1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTaxable_Amount.Text;
        strTextBox = "txtTaxable_Amount";
        csCalculations();
    }
    #endregion


    #region [txtGST_Code_TextChanged]
    protected void txtGST_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtGST_Code.Text;
        strTextBox = "txtGST_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtGST_Code_Click]
    protected void btntxtGST_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGST_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGST_TextChanged]
    protected void txtGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST.Text;
        strTextBox = "txtGST";
        csCalculations();
    }
    #endregion


    #region [txtNet_Value_TextChanged]
    protected void txtNet_Value_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Value.Text;
        strTextBox = "txtNet_Value";
        csCalculations();
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = searchString;
            }
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtDoc_No.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDoc_No.Text = string.Empty;
                    txtDoc_No.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtDoc_No);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtDoc_No.Text == "Choose No")
                {
                    foreach (var s in split)
                    {
                        string aa = s.ToString();
                        name += "doc_no Like '%" + aa + "%'or";
                    }
                    name = name.Remove(name.Length - 2);

                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,PartyName,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and " + name + " order by PartyName asc";
                    this.showPopup(qry);
                }
            }


            if (hdnfClosePopup.Value == "txtParty_Code")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                //}
                //name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0 and (Ac_Code Like '%" + txtSearchText.Text + "%'or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtShipto")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                //}
                // name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and Locked=0 and (Ac_Code Like '%" + txtSearchText.Text + "%'or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtParty_name_new")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                //}
                //name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Ac_type='CR' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0 and (Ac_Code Like '%" + txtSearchText.Text + "%'or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtmillcode")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                //}
                //name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0 and (Ac_Code Like '%" + txtSearchText.Text + "%'or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBroker_Code")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                //}
                //name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" +
                    Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0 and Ac_type='BR' and (Ac_Code Like '%" + txtSearchText.Text + "%'or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtItem_Code")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "System_Code Like '%" + aa + "%'or System_Name_E like '%" + aa + "%' or";
                //}
                //name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from  NT_1_SystemMaster "
                    + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " AND ( System_Code Like '%" + txtSearchText.Text + "%'or System_Name_E like '%" + txtSearchText.Text + "%' )";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "Code Like '%" + aa + "%'or Marka like '%" + aa + "%' or Wt_Per like '%" + aa + "%' or";
                //}
                // name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = " select Code,Marka,Wt_Per from Brand_Master where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + "and Mal_Code=" + txtItem_Code.Text + "and ( Code Like '%" + txtSearchText.Text + "%'or Marka like '%" + txtSearchText.Text + "%' or Wt_Per like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGST_Code")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "GST_Name Like '%" + aa + "%'or";
                //}
                //name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and (GST_Name Like '%" + txtSearchText.Text + "%') order by GST_Name"; ;
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPurc_No")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();
                //    name += "doc_no Like '%" + aa + "%'or millshortname like '%" + aa + "%' or Brand_Name like '%" + aa + "%' or";
                //}
                //  name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Item--";
                //string qry = "select doc_no,convert(varchar(10),doc_date,103) as doc_date,mill_code,millshortname,item_code,itemname,Brand_Code,Brand_Name,NETQNTL,desp,balance,Company_Code,Year_Code," +
                //             "purchaseid,purchasedetailid,Gst_Code,gstratre,packing,LodingGst,dono from qrypurchaseselfbalance where " +
                //             "Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                //             + " and Year_Code=" + Session["year"].ToString() + " and SelfBal='Y' and balance!=0 and (" + "doc_no Like '%" + txtSearchText.Text + "%'or millshortname like '%" + txtSearchText.Text + "%' or Brand_Name like '%" + txtSearchText.Text + "%' ) order by doc_date"; ;

                string qry = "select doc_no,convert(varchar(10),doc_date,103) as doc_date,mill_code,millshortname,item_code,itemname,Brand_Code,Brand_Name,NETQNTL,desp,balance,Company_Code,Year_Code," +
                           "purchaseid,purchasedetailid,Gst_Code,gstratre,packing,LodingGst,dono from qrypurchaseselfbalance where " +
                           "Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                          + " and SelfBal='Y' and balance!=0 and (" + "doc_no Like '%" + txtSearchText.Text + "%'or millshortname like '%" + txtSearchText.Text + "%' or Brand_Name like '%" + txtSearchText.Text + "%' ) order by doc_date"; ;



                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtTransportCode")
            {
                lblPopupHead.Text = "--Select Transport Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
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
            setFocusControl(txtSearchText);
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
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
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
                strTextBox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfyearcode.Value != Session["year"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
                return;
                // Response.Redirect("~/Sugar/pgeHome.aspx", false);
            }
            if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
                return;

            }
            if (txtParty_Code.Text != "1")
            {
                DateTime dEnd = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

                string locktds = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text
                 + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                if (locktds == "L" && dEnd >= ss)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Party lock!')", true);
                    txtParty_Code_TextChanged(this, new EventArgs());
                    txtParty_Code.Text = string.Empty;
                    setFocusControl(txtParty_Code);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "retailSale", "javascript:pagevalidation();", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "retailSale", "javascript:pagevalidation();", true);
            }
        }
        catch
        {
        }
    }
    #endregion
    protected void txtCGST_Rate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtCGST_Amount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtSGST_Rate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtSGST_Amount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtIGST_Rate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtIGST_Amount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void drpCashCredit_SelectedIndexChanged(object sender, EventArgs e)
    {
        showLastRecord();

    }
    protected void btntxtParty_name_new_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtParty_name_new";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void btntxtShipto_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtShipto";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtShipto_TextChanged(object sender, EventArgs e)
    {
        searchString = "";
        searchString = txtShipto.Text;
        strTextBox = "txtShipto";
        csCalculations();
    }
    protected void txtNetHamali_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtNetMarketsess_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtNetpacking_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtNetSuperCost_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtNetFrieght_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtNetOther_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancleEInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                bool isValidated = true;
                if (txtEnvoiceno.Text != string.Empty || txtEnvoiceno.Text != "NA")
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Can't Cancle this Einvoice!!!!!');", true);
                    return;
                }
                string Irn = txtEnvoiceno.Text;
                string Cnlrsn = "1";
                string Cnlrem = "Wrong entry";

                string json = "{\"Irn\":\"" + Irn + "\"," +
                               "\"Cnlrsn\":\"" + Cnlrsn + "\"," +
                               "\"Cnlrem\":\"" + Cnlrem + "\"}";

                string username = clsCommon.getString("select E_UserName from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string password = clsCommon.getString("select E_UserPassword from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string USERNAME = clsCommon.getString("select E_UserName_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string PASSWORD = clsCommon.getString("select E_UserPassword_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string gstin = clsCommon.getString("select E_Company_GSTno from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                ////string urlAddress = "https://gsp.adaequare.com/gsp/authenticate?grant_type=token";
                ////string urlAddress1 = "https://gsp.adaequare.com/enriched/ei/api/invoice/cancel";

                string urlAddress = clsCommon.getString("select E_UrlAddress_Token from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string urlAddress1 = clsCommon.getString("select EInvoiceCancle from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string DDate = DateTime.Now.ToString();
                string SBNo = txtDoc_No.Text;
                string requestid = DDate + SBNo + "SS";
                string token = xmlExecuteDMLQry.GetAuthToken(urlAddress, username, password);
                string Ewaybullno = xmlExecuteDMLQry.GenrateEInvoice(urlAddress1, USERNAME, PASSWORD, gstin, requestid, token, json);
                string str = Ewaybullno;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);


                str = str.Replace("{", "");
                str = str.Replace("}", "");
                str = str.Replace(":", "");
                str = str.Replace(",", "");
                str = str.Replace("\"", "");
                string sub2 = "true";
                bool b = str.Contains(sub2);

                string sub4 = "false";
                bool s = str.Contains(sub4);
                string dist = "distance";

                string sub3 = "WARNING";
                bool n = str.Contains(sub3);

                string qry = "";
                string qry1 = "";
                DataSet ds = new DataSet();
                if (b)
                {
                    if (n)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);
                    }
                    else
                    {
                        int index = str.IndexOf(sub2);
                        if (index > 0)
                        {
                            qry1 = "update Retail_Head set ACK='NA',Envoiceno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + txtDoc_No.Text;
                            ds = clsDAL.SimpleQuery(qry1);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice Canclled Successfully !');", true);
                        }

                    }
                }
                else if (s)
                {
                    int index = str.IndexOf(dist);
                    if (index > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Alert1", "alert('" + str + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + str + "');", true);
                }
            }
            else
            {
                lblMsg.Text = "Cannot Cancle this EInvoice !";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        catch
        {
        }
    }

    #region [txtPurc_No_TextChanged]
    protected void txtPurc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPurc_No.Text;
        strTextBox = "txtPurc_No";
        csCalculations();
    }
    #endregion

    #region [btntxtPurc_No_Click]
    protected void btntxtPurc_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPurc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }

    }
    #endregion



    protected void drpCashRecieve_CheckedChanged(object sender, EventArgs e)
    {
        setFocusControl(txtParty_Name);
    }
    protected void txtTransportCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransportCode.Text;
        strTextBox = "txtTransportCode";
        csCalculations();
    }
    protected void btnTransport_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTransportCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    private void AmtCalculation()
    {
        #region oldCode
        //string docdate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        //double Amount = 0.00;
        //double Amountf = 0.00;

        //string Amt = string.Empty;
        //string saleAmt = string.Empty;
        //double SBBalAmt = 0.00;
        //double taxableamt = 0.00;
        //double cgstamt = 0.00;
        //double sgstamt = 0.00;
        //double igstamt = 0.00;
        //double roundoffamt = 0.00;

        //double ramt = 0.00;
        //double samt = 0.00;

        //string SaleTDS = string.Empty;

        //if (txtTaxable_Amount.Text != string.Empty)
        //{
        //    taxableamt = Convert.ToDouble(txtTaxable_Amount.Text);
        //}
        //if (txtTaxable_Amount.Text != string.Empty)
        //{
        //    cgstamt = Convert.ToDouble(txtTotalCGST.Text);
        //}
        //if (txtTaxable_Amount.Text != string.Empty)
        //{
        //    sgstamt = Convert.ToDouble(txtTotalSGST.Text);
        //}
        //if (txtTaxable_Amount.Text != string.Empty)
        //{
        //    igstamt = Convert.ToDouble(txtTotalIGST.Text);
        //}
        //if (txtTaxable_Amount.Text != string.Empty)
        //{
        //    //roundoffamt = Convert.ToString(Convert.ToDouble(txtRoundoff.Text)) != string.Empty ? Convert.ToDouble(txtRoundoff.Text) : 0.00;
        //    roundoffamt = Convert.ToDouble(txtRoundoff.Text);
        //}

        //SBBalAmt = taxableamt + cgstamt + sgstamt + igstamt + roundoffamt;
        //if (txtParty_Code.Text != "1")
        //{
        //    Amt = clsCommon.getString("select (case when sum(NetValue)>0 then sum(NetValue) else 0 end ) as amt FROM Retail_Head where Doc_Date<='" + docdate + "' and Party_Code= '" + txtParty_Code.Text
        //        + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
        //    saleAmt = clsCommon.getString("select (case when sum(Bill_Amount)>0 then sum(Bill_Amount) else 0 end ) as amt FROM NT_1_SugarSale where doc_date<='" + docdate + "' and ac_code= '" + txtParty_Code.Text
        //    + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");

        //    SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text
        //      + " and Company_Code=" + Session["Company_Code"].ToString() + "");


        //}
        //if (Amt == string.Empty || Amt == "" || saleAmt == string.Empty || saleAmt == "")
        //{
        //    Amt = "0.00";
        //    saleAmt = "0.00";
        //}
        //ramt = Convert.ToDouble(Amt);
        //samt = Convert.ToDouble(saleAmt);
        //Amountf = Convert.ToDouble(ramt + samt);

        //double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
        //DateTime dEnd = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //if (btnSave.Text == "Save")
        //{
        //    Amount = Amountf + SBBalAmt;
        //}
        //else
        //{
        //    Amount = Amountf;
        //}

        //if (SaleTDS == "Y" && dEnd >= ss)
        //{
        //    if (Amount > balancelimit)
        //    {
        //        txtTCS_Rate.Text = "0.00";
        //        txtTCS_Amount.Text = "0.00";
        //        txtTDS.Text = Session["SaleTDSRate"].ToString();

        //    }
        //    else
        //    {
        //        txtTCS_Rate.Text = Session["TCSRate"].ToString();
        //        txtTDS.Text = "0.00";
        //        txtTDSAmt.Text = "0.00";
        //    }



        //}
        //else
        //{
        //    if (Amount > balancelimit)
        //    {
        //        txtTCS_Rate.Text = Session["TCSRate"].ToString();
        //        txtTDS.Text = "0.00";
        //        txtTDSAmt.Text = "0.00";

        //    }
        //    else
        //    {
        //        txtTCS_Rate.Text = "0.00";
        //        txtTCS_Amount.Text = "0.00";
        //        txtTDS.Text = "0.00";
        //        txtTDSAmt.Text = "0.00";
        //    }


        //}




        #endregion

        string docdate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        double Amount = 0.00;
        double Amountf = 0.00;

        string Amt = string.Empty;
        string saleAmt = string.Empty;
        double SBBalAmt = 0.00;
        double taxableamt = 0.00;
        double cgstamt = 0.00;
        double sgstamt = 0.00;
        double igstamt = 0.00;
        double roundoffamt = 0.00;

        double ramt = 0.00;
        double samt = 0.00;

        string SaleTDS = string.Empty;

        if (txtTaxable_Amount.Text != string.Empty)
        {
            taxableamt = Convert.ToDouble(txtTaxable_Amount.Text);
        }
        if (txtTaxable_Amount.Text != string.Empty)
        {
            cgstamt = Convert.ToDouble(txtTotalCGST.Text);
        }
        if (txtTaxable_Amount.Text != string.Empty)
        {
            sgstamt = Convert.ToDouble(txtTotalSGST.Text);
        }
        if (txtTaxable_Amount.Text != string.Empty)
        {
            igstamt = Convert.ToDouble(txtTotalIGST.Text);
        }
        if (txtTaxable_Amount.Text != string.Empty)
        {
            //roundoffamt = Convert.ToString(Convert.ToDouble(txtRoundoff.Text)) != string.Empty ? Convert.ToDouble(txtRoundoff.Text) : 0.00;
            roundoffamt = Convert.ToDouble(txtRoundoff.Text);
        }

        SBBalAmt = taxableamt + cgstamt + sgstamt + igstamt + roundoffamt;
        if (txtParty_Code.Text != "1")
        {
            Amt = clsCommon.getString("select (case when sum(NetValue)>0 then sum(NetValue) else 0 end ) as amt FROM Retail_Head where Doc_Date<='" + docdate + "' and Party_Code= '" + txtParty_Code.Text
                + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
            //saleAmt = clsCommon.getString("select (case when sum(Bill_Amount)>0 then sum(Bill_Amount) else 0 end ) as amt FROM NT_1_SugarSale where doc_date<='" + docdate + "' and ac_code= '" + txtParty_Code.Text
            //+ "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");

            //saleAmt = clsCommon.getString("select sum(isnull(AMOUNT,0)) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + txtParty_Code.Text
            //         + " and DRCR='D' and TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
            //         "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text
              + " and Company_Code=" + Session["Company_Code"].ToString() + "");

            DataTable dtpan = new DataTable();
            DataSet dspan = new DataSet();
            #region[balancelimit]
            string panno = clsCommon.getString("select CompanyPan from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text
               + " and Company_Code=" + Session["Company_Code"].ToString() + "");

            dspan = clsDAL.SimpleQuery("select Ac_Code from qrymstaccountmaster where CompanyPan='" + panno
               + "' and Company_Code=" + Session["Company_Code"].ToString() + "");
            dtpan = dspan.Tables[0];

            //PurchaseTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtAC_CODE
            //   + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            //double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
            double Amt2 = 0.00;
            double sbamt2 = 0.00;
            if (panno != string.Empty)
            {
                if (dtpan.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpan.Rows.Count; i++)
                    {
                        double Amt1 = 0.00;
                        //int accode = 0;
                        // accode=dtpan.Rows
                        //psamt1 = Convert.ToDouble(clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where  AC_CODE=" + dtpan.Rows[0]["Ac_Code"].ToString()
                        // + " and DRCR='C' and  TRAN_TYPE in('PS','PR','RP') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
                        // "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                        Amt1 = Convert.ToDouble(clsCommon.getString("select sum(isnull(AMOUNT,0)) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpan.Rows[i]["Ac_Code"].ToString()
                        + " and DRCR='D' and TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
                         "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                        Amt2 += Amt1;
                    }

                    saleAmt = Amt2.ToString();
                }
            #endregion

            }
            if (Amt == string.Empty || Amt == "" || saleAmt == string.Empty || saleAmt == "")
            {
                Amt = "0.00";
                saleAmt = "0.00";
            }
            ramt = Convert.ToDouble(Amt);
            samt = Convert.ToDouble(saleAmt);
            Amountf = Convert.ToDouble(ramt + samt);

            double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
            DateTime dEnd = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            if (btnSave.Text == "Save")
            {
                Amount = Amountf + SBBalAmt;
            }
            else
            {
                Amount = Amountf;
            }

            if (Amount >= balancelimit)
            {
                if (SaleTDS == "Y")
                {
                    if (drpCashCredit.SelectedValue != "CS")
                    {
                        txtTDS.Text = Session["SaleTDSRate"].ToString();
                    }
                    else
                    {
                        txtTDS.Text = "0.00";
                    }
                        txtTCS_Rate.Text = "0.00";
                    
                }
                else
                {
                    txtTDS.Text = "0.00";
                    txtTCS_Rate.Text = Session["TCSRate"].ToString();
                }

            }
            else
            {
                txtTDS.Text = "0.00";
                txtTCS_Rate.Text = "0.00";
            }
        }
    }
    protected void txtTDS_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtTDS.Text;
        //strTextBox = "txtTDS";
        //csCalculations();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:GridCalculations()", true);

    }
    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAmt.Text;
        strTextBox = "txtTDSAmt";
        csCalculations();
    }

}