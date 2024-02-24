using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Globalization;
public partial class pgeFuturetranstion : System.Web.UI.Page
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
    static WebControl objAsp = null;
    int Doc_No = 2;
    int Script_Code = 3;
    int Expiry_date = 5;
    int Lot_Size = 6;
    int Future_Type = 7;
    int Buy_Sale = 8;
    int lot_qty = 9;
    int Rate = 10;
    int Value = 11;
    int Strike_Price = 12;
    int AutoCarry = 13;
    int Rowaction = 14;
    int Srno = 15;
    DataTable dt = new DataTable();

    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Future_Head";
            tblDetails = "Future_Detail";
            qryCommon = "QryFutureTranstion";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    this.showLastRecord();
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
                grddetailFuture.DataSource = null;
                grddetailFuture.DataBind();

                grddetailFuture.Enabled = false;
                ViewState["currentTable1"] = null;
                ViewState["currentTable"] = null;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtBroker_code.Enabled = false;
                btntxtBroker_code.Enabled = false;
                txtBill_No.Enabled = false;
                txtsattlement_No.Enabled = false;
                txtRemark.Enabled = false;
                txtNetbrokrage.Enabled = false;
                txtNet_Cgst.Enabled = false;
                txtNet_Sgst.Enabled = false;
                txtNet_Igst.Enabled = false;
                txtNet_Stt.Enabled = false;
                txtNet_StampDuty.Enabled = false;
                txtNet_pnl.Enabled = false;
                txtNetExpenses.Enabled = false;
                txtNetProfitLoss.Enabled = false;
                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = false;
                txtExpiry_date.Enabled = false;
                // CalendarExtenderDatetxtExpiry_date.Enabled = false;
                txtLot_Size.Enabled = false;
                drpFuture_Type.Enabled = false;
                drpBuy_Sale.Enabled = false;
                txtlot_qty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtStrike_Price.Enabled = false;
                lblAutoCarry.Enabled = false;
                btnexpriydate.Enabled = false;

                txtnetExchangecharges.Enabled = false;
                txtnetSTF.Enabled = false;
                txtcmcharge.Enabled = false;
                txtNet_Postage.Enabled = false;

                btnDaily_Positoin.Enabled = false;
                btnDeletebtnDaily_Positoin.Enabled = false;

                txtNetbrokrage.Text = string.Empty;
                txtnetExchangecharges.Text = string.Empty;
                txtnetSTF.Text = string.Empty;
                txtNet_Stt.Text = string.Empty;
                txtNet_StampDuty.Text = string.Empty;
                txtcmcharge.Text = string.Empty;
                txtNet_Cgst.Text = string.Empty;
                txtNet_Sgst.Text = string.Empty;
                txtNet_Igst.Text = string.Empty;
                txtNet_Postage.Text = string.Empty;
                txtNetProfitLoss.Text = string.Empty;
                txtNetExpenses.Text = string.Empty;
                txtNet_pnl.Text = string.Empty;
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
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;

                grddetailFuture.DataSource = null;
                grddetailFuture.DataBind();
                ViewState["currentTable1"] = null;

                txtScript_Code.Enabled = false;
                lblscrpit_Code.Text = string.Empty;
                btntxtScript_Code.Enabled = true;
                txtExpiry_date.Enabled = true;
                btnexpriydate.Enabled = true;
                //  txtExpiry_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                txtLot_Size.Enabled = true;
                drpFuture_Type.Enabled = true;
                drpBuy_Sale.Enabled = true;
                txtlot_qty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtStrike_Price.Enabled = true;
                lblAutoCarry.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtBroker_code.Enabled = true;
                btntxtBroker_code.Enabled = true;
                txtBill_No.Enabled = true;
                txtsattlement_No.Enabled = true;
                txtRemark.Enabled = true;
                txtNetbrokrage.Enabled = true;
                txtNet_Cgst.Enabled = true;
                txtNet_Sgst.Enabled = true;
                txtNet_Igst.Enabled = true;
                txtNet_Stt.Enabled = true;
                txtNet_StampDuty.Enabled = true;
                txtNet_pnl.Enabled = true;
                txtNetExpenses.Enabled = true;
                txtNetProfitLoss.Enabled = true;
                lblParty_Code.Text = string.Empty;


                txtnetExchangecharges.Enabled = true;
                txtnetSTF.Enabled = true;
                txtcmcharge.Enabled = true;
                txtNet_Postage.Enabled = true;
                #region set Business logic for save
                #endregion

                btnDaily_Positoin.Enabled = false;
                btnDeletebtnDaily_Positoin.Enabled = false;


                txtNetbrokrage.Text = string.Empty;
                txtnetExchangecharges.Text = string.Empty;
                txtnetSTF.Text = string.Empty;
                txtNet_Stt.Text = string.Empty;
                txtNet_StampDuty.Text = string.Empty;
                txtcmcharge.Text = string.Empty;
                txtNet_Cgst.Text = string.Empty;
                txtNet_Sgst.Text = string.Empty;
                txtNet_Igst.Text = string.Empty;
                txtNet_Postage.Text = string.Empty;
                txtNetProfitLoss.Text = string.Empty;
                txtNetExpenses.Text = string.Empty;
                txtNet_pnl.Text = string.Empty;

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
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtBroker_code.Enabled = false;
                btntxtBroker_code.Enabled = false;
                txtBill_No.Enabled = false;
                txtsattlement_No.Enabled = false;
                txtRemark.Enabled = false;
                txtNetbrokrage.Enabled = false;
                txtNet_Cgst.Enabled = false;
                txtNet_Sgst.Enabled = false;
                txtNet_Igst.Enabled = false;
                txtNet_Stt.Enabled = false;
                txtNet_StampDuty.Enabled = false;
                txtNet_pnl.Enabled = false;
                txtNetExpenses.Enabled = false;
                txtNetProfitLoss.Enabled = false;
                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = false;
                txtExpiry_date.Enabled = false;
                btnexpriydate.Enabled = false;
                //  CalendarExtenderDatetxtExpiry_date.Enabled = false;
                txtLot_Size.Enabled = false;
                drpFuture_Type.Enabled = false;
                drpBuy_Sale.Enabled = false;
                txtlot_qty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtStrike_Price.Enabled = false;
                lblAutoCarry.Enabled = false;
                txtScript_Code.Text = string.Empty;
                btntxtScript_Code.Enabled = false;
                txtExpiry_date.Text = string.Empty;
                //   CalendarExtenderDatetxtExpiry_date.Enabled = false;
                txtLot_Size.Text = string.Empty;
                ////drpFuture_Type.Text = string.Empty;
                ////drpBuy_Sale.Text = string.Empty;
                txtlot_qty.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtValue.Text = string.Empty;
                lblParty_Code.Text = string.Empty;
                txtStrike_Price.Text = string.Empty;
                lblAutoCarry.Text = string.Empty;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnexpriydate.Enabled = false;

                txtnetExchangecharges.Enabled = false;
                txtnetSTF.Enabled = false;
                txtcmcharge.Enabled = false;
                txtNet_Postage.Enabled = false;

                btnDaily_Positoin.Enabled = false;
                btnDeletebtnDaily_Positoin.Enabled = false;
                grddetailFuture.Enabled = false;
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
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtBroker_code.Enabled = true;
                btntxtBroker_code.Enabled = true;
                txtBill_No.Enabled = true;
                txtsattlement_No.Enabled = true;
                txtRemark.Enabled = true;
                txtNetbrokrage.Enabled = true;
                txtNet_Cgst.Enabled = true;
                txtNet_Sgst.Enabled = true;
                txtNet_Igst.Enabled = true;
                txtNet_Stt.Enabled = true;
                txtNet_StampDuty.Enabled = true;
                txtNet_pnl.Enabled = true;
                txtNetExpenses.Enabled = true;
                txtNetProfitLoss.Enabled = true;
                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = true;
                txtExpiry_date.Enabled = true;
                btnexpriydate.Enabled = true;
                //  CalendarExtenderDatetxtExpiry_date.Enabled = true;
                txtLot_Size.Enabled = true;
                drpFuture_Type.Enabled = true;
                drpBuy_Sale.Enabled = true;
                txtlot_qty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtStrike_Price.Enabled = true;
                lblAutoCarry.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;

                txtnetExchangecharges.Enabled = true;
                txtnetSTF.Enabled = true;
                txtcmcharge.Enabled = true;
                txtNet_Postage.Enabled = true;


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
            qry = "select max(Doc_No) as Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_code=" + Convert.ToInt32(Session["year"].ToString());
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
                        hdnf.Value = dt.Rows[0]["Doc_No"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            btnEdit.Focus();
                        }
                        else                     //new code
                        {
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
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
    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        //int RecordCount = 0;
        //string query = "";
        //query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_code=" + Convert.ToInt32(Session["year"]).ToString();
        //string cnt = clsCommon.getString(query);
        //if (cnt != string.Empty)
        //{
        //    RecordCount = Convert.ToInt32(cnt);
        //}
        //if (RecordCount != 0 && RecordCount == 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = false;
        //}
        //else if (RecordCount != 0 && RecordCount > 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = true;
        //}
        //if (txtDoc_No.Text != string.Empty)
        //{
        //    #region check for next or previous record exist or not
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_code=" + Convert.ToInt32(Session["year"]).ToString() + " ORDER BY Doc_No asc  ";
        //    string strDoc_No = clsCommon.getString(query);
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //    }
        //    else
        //    {
        //        btnNext.Enabled = false;
        //        btnLast.Enabled = false;
        //    }
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_code=" + Convert.ToInt32(Session["year"]).ToString() + " ORDER BY Doc_No desc  ";
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnPrevious.Enabled = true;
        //        btnFirst.Enabled = true;
        //    }
        //    else
        //    {
        //        btnPrevious.Enabled = false;
        //        btnFirst.Enabled = false;
        //    }
        //}
        //    #endregion
        #endregion

        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        // query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) " and Sub_Type='" + drpSub_Type.SelectedValue + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString());

        query = "select count(*) from " + tblHead + " where  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' ";
        string cnt = clsCommon.getString(query);
        if (cnt != string.Empty)
        {
            RecordCount = Convert.ToInt32(cnt);
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
            #region check for next or previous record exist or not

            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No asc  ";
            string strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty && strDoc_No != "0")
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


            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + int.Parse(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                 "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No desc  ";
            strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty && strDoc_No != "0")
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
            #endregion
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No desc  ";
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc  ";
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
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
        string check = txtEditDoc_No.Text;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");

            string qry = "select Ac_Code as Brokercode from " + tblHead +
        " where Doc_No =(select max(Doc_No) from " + tblHead +
        " where Year_Code=" + Convert.ToString(Session["year"]).ToString()
      + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString()
      + ") and Year_Code=" + Convert.ToString(Session["year"]).ToString()
      + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString();

            string Brokercode = clsCommon.getString(qry);
            if (Brokercode != string.Empty)
            {
                txtBroker_code.Text = Brokercode;
            }

            lblParty_Code.Text = clsCommon.getString("select Ac_Name_E from  nt_1_accountmaster where Ac_Code=" + txtBroker_code.Text
                                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            //Date

            string qry1 = "select convert(varchar(10),Doc_Date,103) as Doc_Date from " + tblHead +
        " where Doc_Date =(select max(Doc_Date) from " + tblHead +
        " where Year_Code=" + Convert.ToString(Session["year"]).ToString()
      + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString()
      + ") and Year_Code=" + Convert.ToString(Session["year"]).ToString()
      + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString();

            string docdate = clsCommon.getString(qry1);
         
            if (docdate != string.Empty)
            {
                txtDoc_Date.Text = docdate;
            }
     
            setFocusControl(txtDoc_Date);
            pnlPopupDetails.Style["display"] = "none";
            lblAutoCarry.Text = "N";

            Int32 Doc_No = Convert.ToInt32(clsCommon.getString("select IDENT_CURRENT('" + tblHead + "') as Doc_No"));
            if (Doc_No != 0)
            {
                int doc_no = Doc_No + 1;
                Doc_No = doc_no;
            }
            else
            {
                Doc_No = 1;
            }
            txtDoc_No.Text = Convert.ToString(Doc_No);
        }
        else
        {
            btnCancel_Click(this, new EventArgs());
        }
    }
    #endregion

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


        if (grddetailFuture.Rows.Count != 0)
        {
            btnDaily_Positoin.Enabled = false;
            pnlgrdDetail.Enabled = false;
            btnAdddetails.Enabled = false;
            btnDeletebtnDaily_Positoin.Enabled = true;
            grddetailFuture.Enabled = false;
        }
        else
        {
            btnDaily_Positoin.Enabled = true;
            pnlgrdDetail.Enabled = true;
            btnAdddetails.Enabled = true;
            grddetailFuture.Enabled = true;
        }
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {

                string DocDate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                string DocNo = clsDAL.GetString("select Doc_No from  Future_Head where Ac_Code=" + txtBroker_code.Text + " and Doc_Date>'" + DocDate
                    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                if (DocNo != "0" && DocNo != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You Can Edit only last Record ');", true);
                    return;
                }


                string str = string.Empty;
                DataSet xml_ds = new DataSet();
                if (str == string.Empty)
                {
                    string currentDoc_No = txtDoc_No.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                        string strRev = string.Empty;
                        child1.SetAttributeValue("Doc_No", txtDoc_No.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);
                        child1.SetAttributeValue("Year_code", Year_Code);
                        // child1.SetAttributeValue("Tran_Type", "");
                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_FutureHead";
                        string xmlfile = XMLReport;
                        string op = "";
                        string returnmaxno = "";
                        int flag = 10;
                        xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
                        strrev = op;





                    }
                    string query = "";
                    if (strrev == "-3")
                    {

                        DataTable dt_dalete = xml_ds.Tables[0];
                        string HDNF_VALUE = "";
                        if (dt_dalete.Rows.Count > 0)
                        {
                            HDNF_VALUE = dt_dalete.Rows[0]["Doc_No"].ToString();
                        }
                        hdnf.Value = HDNF_VALUE;

                        if (hdnf.Value == string.Empty)
                        {
                            DataTable dt_dalete_two = xml_ds.Tables[1];
                            if (dt_dalete_two.Rows.Count > 0)
                            {
                                hdnf.Value = dt_dalete_two.Rows[0]["Doc_No"].ToString();
                            }
                        }
                        if (hdnf.Value != string.Empty)
                        {
                            query = getDisplayQuery();
                            bool recordExist = this.fetchRecord(query);
                            this.makeEmptyForm("S");
                            clsButtonNavigation.enableDisable("S");
                        }
                        else
                        {
                            this.makeEmptyForm("N");
                            clsButtonNavigation.enableDisable("N");
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    } this.enableDisableNavigateButtons();
                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
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
        if (hdnf.Value != string.Empty)
        {
            string query = getDisplayQuery();
            bool recordExist = this.fetchRecord(query);
        }
        else
        {
            this.showLastRecord();
        }
        string qry = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        if (qry != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            this.enableDisableNavigateButtons();
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");
            this.enableDisableNavigateButtons();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
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
                        //      txtEditDoc_No.Text = dt.Rows[0]["EditDoc_No"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["docdate"].ToString();
                        txtBroker_code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        lblParty_Code.Text = dt.Rows[0]["brokername"].ToString();
                        txtBill_No.Text = dt.Rows[0]["Bill_No"].ToString();
                        txtsattlement_No.Text = dt.Rows[0]["sattlement_No"].ToString();
                        txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                        double Netbrokrage = Convert.ToDouble(dt.Rows[0]["Netbrokrage"]);
                        if (Netbrokrage == 0.00)
                        {
                            txtNetbrokrage.Text = string.Empty;
                        }
                        else
                        {
                            txtNetbrokrage.Text = dt.Rows[0]["Netbrokrage"].ToString();
                        }

                        double Net_Cgst = Convert.ToDouble(dt.Rows[0]["Net_Cgst"]);
                        if (Net_Cgst == 0.00)
                        {
                            txtNet_Cgst.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_Cgst.Text = dt.Rows[0]["Net_Cgst"].ToString();
                        }
                        double Net_Sgst = Convert.ToDouble(dt.Rows[0]["Net_Sgst"]);
                        if (Net_Sgst == 0.00)
                        {
                            txtNet_Sgst.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_Sgst.Text = dt.Rows[0]["Net_Sgst"].ToString();
                        }
                        double Net_Igst = Convert.ToDouble(dt.Rows[0]["Net_Igst"]);
                        if (Net_Igst == 0.00)
                        {
                            txtNet_Igst.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_Igst.Text = dt.Rows[0]["Net_Igst"].ToString();
                        }
                        double Net_Stt = Convert.ToDouble(dt.Rows[0]["Net_Stt"]);
                        if (Net_Stt == 0.00)
                        {
                            txtNet_Stt.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_Stt.Text = dt.Rows[0]["Net_Stt"].ToString();
                        }
                        double Net_StampDuty = Convert.ToDouble(dt.Rows[0]["Net_StampDuty"]);
                        if (Net_StampDuty == 0.00)
                        {
                            txtNet_StampDuty.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_StampDuty.Text = dt.Rows[0]["Net_StampDuty"].ToString();
                        }
                        double Net_pnl = Convert.ToDouble(dt.Rows[0]["Net_pnl"]);
                        if (Net_pnl == 0.00)
                        {
                            txtNet_pnl.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_pnl.Text = dt.Rows[0]["Net_pnl"].ToString();
                        }

                        double Net_Exchange_Charges = Convert.ToDouble(dt.Rows[0]["Net_Exchange_Charges"]);
                        if (Net_Exchange_Charges == 0.00)
                        {
                            txtnetExchangecharges.Text = string.Empty;
                        }
                        else
                        {
                            txtnetExchangecharges.Text = dt.Rows[0]["Net_Exchange_Charges"].ToString();
                        }
                        double Net_STF = Convert.ToDouble(dt.Rows[0]["Net_STF"]);
                        if (Net_STF == 0.00)
                        {
                            txtnetSTF.Text = string.Empty;
                        }
                        else
                        {
                            txtnetSTF.Text = dt.Rows[0]["Net_STF"].ToString();
                        }
                        double Net_CM_Charge = Convert.ToDouble(dt.Rows[0]["Net_CM_Charge"]);
                        if (Net_CM_Charge == 0.00)
                        {
                            txtcmcharge.Text = string.Empty;
                        }
                        else
                        {
                            txtcmcharge.Text = dt.Rows[0]["Net_CM_Charge"].ToString();
                        }
                        double Net_Postage = Convert.ToDouble(dt.Rows[0]["Net_Postage"]);
                        if (Net_Postage == 0.00)
                        {
                            txtNet_Postage.Text = string.Empty;
                        }
                        else
                        {
                            txtNet_Postage.Text = dt.Rows[0]["Net_Postage"].ToString();
                        }
                        double Net_Expenses = Convert.ToDouble(dt.Rows[0]["Net_Expenses"]);
                        if (Net_Expenses == 0.00)
                        {
                            txtNetExpenses.Text = string.Empty;
                        }
                        else
                        {
                            txtNetExpenses.Text = dt.Rows[0]["Net_Expenses"].ToString();
                        }
                        double Net_ProfitLoss = Convert.ToDouble(dt.Rows[0]["Net_ProfitLoss"]);
                        if (Net_ProfitLoss == 0.00)
                        {
                            txtNetProfitLoss.Text = string.Empty;
                        }
                        else
                        {
                            txtNetProfitLoss.Text = dt.Rows[0]["Net_ProfitLoss"].ToString();
                        }

                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select detail_id as Detail_Id,Script_Code as Script_Code,scriptname as Script_Code_Name,  expriydate as Expiry_Date ,Lot_Size,Future_Type as Future_Type,Buy_Sale ,Lot_Qty as lot_qty,Rate," +
                            "Value,Strike_Price,AutoCarry from " + qryCommon + " where Doc_No="
                            + hdnf.Value + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
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

                        string Doc_Date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                        #region Details grid

                        qry = "select detail_Id as Detail_Id,Script_Code as Script_Code,Script_Name as Script_Name,convert(varchar(10),Expiry_Date,103) as Expiry_Date," +
                            " Lot_Size,Future_Type as Type,Buy_Qty,Buy_Rate,Buy_Value,Sale_Qty,Sale_Rate,Sale_Value,Strike_Price as SP,Closing_Qty,Closing_Rate," +
                           " Profit_Loss as ProfitLoss from QryFutureTranstionDetails where Company_code=1 and year_Code=1 and doc_no='" + txtDoc_No.Text
                           + "'and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";






                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {

                                    //for (int i = 0; i < dt.Rows.Count; i++)
                                    //{
                                    //    dt.Rows[i]["Detail_Id"] = i + 1;
                                    //}


                                    grddetailFuture.DataSource = dt;
                                    grddetailFuture.DataBind();
                                    ViewState["currentTable1"] = dt;
                                }
                                else
                                {
                                    grddetailFuture.DataSource = null;
                                    grddetailFuture.DataBind();
                                    ViewState["currentTable1"] = null;
                                }
                            }
                            else
                            {
                                grddetailFuture.DataSource = null;
                                grddetailFuture.DataBind();
                                ViewState["currentTable1"] = null;
                            }
                        }
                        else
                        {
                            grddetailFuture.DataSource = null;
                            grddetailFuture.DataBind();
                            ViewState["currentTable1"] = null;
                        }
                        #endregion

                    }
                }
            }
            hdnf.Value = txtDoc_No.Text;
            this.enableDisableNavigateButtons();
            return recordExist;

            //string DocDate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //string DocNo = clsDAL.GetString("select Doc_No from  Future_Head where Ac_Code=" + txtBroker_code.Text + " and Doc_Date>'" + DocDate 
            //    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            //if (DocNo != "0" && DocNo != "")
            //{
            //    btnEdit.Enabled = false;
            //    btnDelete.Enabled = false;
            //}


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
                setFocusControl(txtDoc_Date);
            }
            if (strTextBox == "txtBroker_code")
            {
                string acname = "";
                if (txtBroker_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBroker_code.Text);
                    if (a == false)
                    {
                        btntxtBroker_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtBroker_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            lblParty_Code.Text = acname;
                            string acnamestatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtBroker_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            string DocDate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                            string doc_no = clsDAL.GetString("select max(Doc_No) as Doc_No from QryFutureTranstionDetails where Ac_Code=" + txtBroker_code.Text + " and Doc_Date<='" + DocDate
                                + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                            if (doc_no != "0" && doc_no != "")
                            {
                                #region Details

                                qry = "select '' as Detail_Id,Script_Code as Script_Code,Script_Name as Script_Code_Name,convert(varchar(10),Expiry_Date,103) as Expiry_Date," +
                                    " Lot_Size as Lot_Size,Future_Type as Future_Type,case when Closing_Qty>0 then 'B' else 'S' end as Buy_Sale, " +
                                    "abs(Closing_Qty) as lot_qty,Closing_Rate as Rate,0.00 as Value,0.00 as Strike_Price,'Y' as AutoCarry from QryFutureTranstionDetails " +
                                    " where Ac_Code=" + txtBroker_code.Text + " and Doc_No=" + doc_no + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                    + " and Year_code=" + Convert.ToInt32(Session["year"].ToString()) + " and Closing_Qty!=0 and Closing_Rate!=0 order by Script_Code_Name";

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
                                            dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                            dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                double Value = 0.00;
                                                dt.Rows[i]["rowAction"] = "N";
                                                dt.Rows[i]["SrNo"] = i + 1;
                                                dt.Rows[i]["Detail_Id"] = i + 1;

                                                double lot_qty = Convert.ToDouble(dt.Rows[i]["lot_qty"]);
                                                double Rate = Convert.ToDouble(dt.Rows[i]["Rate"]);
                                                double Lot_Size = Convert.ToDouble(dt.Rows[i]["Lot_Size"]);
                                                Value = lot_qty * Rate * Lot_Size;
                                                dt.Rows[i]["Value"] = Value.ToString();
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
                                grdDetail.Enabled = true;
                                pnlgrdDetail.Enabled = true;
                                #endregion
                            }
                            setFocusControl(txtBill_No);

                        }
                        else
                        {
                            txtBroker_code.Text = string.Empty;
                            lblParty_Code.Text = acname;
                            setFocusControl(txtBroker_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBroker_code);
                }
            }
            if (strTextBox == "txtBill_No")
            {
                setFocusControl(txtsattlement_No);
            }
            if (strTextBox == "txtsattlement_No")
            {
                setFocusControl(txtRemark);
            }
            if (strTextBox == "txtRemark")
            {
                setFocusControl(btntxtScript_Code);
            }
            if (strTextBox == "txtNetbrokrage")
            {
                setFocusControl(txtnetExchangecharges);
            }
            if (strTextBox == "txtnetExchangecharges")
            {
                setFocusControl(txtnetSTF);
            }
            if (strTextBox == "txtnetSTF")
            {
                setFocusControl(txtNet_Stt);
            }
            if (strTextBox == "txtNet_Stt")
            {
                setFocusControl(txtNet_StampDuty);
            }
            if (strTextBox == "txtNet_StampDuty")
            {
                setFocusControl(txtcmcharge);
            }
            if (strTextBox == "txtcmcharge")
            {
                setFocusControl(txtNet_Cgst);
            }


            if (strTextBox == "txtNet_Cgst")
            {
                setFocusControl(txtNet_Sgst);
            }
            if (strTextBox == "txtNet_Sgst")
            {
                setFocusControl(txtNet_Igst);
            }
            if (strTextBox == "txtNet_Igst")
            {
                setFocusControl(txtNet_Postage);
            }
            if (strTextBox == "txtNet_Postage")
            {
                setFocusControl(txtNetProfitLoss);
            }
            if (strTextBox == "txtNetProfitLoss")
            {
                setFocusControl(txtNetExpenses);
            }

            if (strTextBox == "txtNetExpenses")
            {
                setFocusControl(txtNet_pnl);
            }
            if (strTextBox == "txtNet_pnl")
            {
                setFocusControl(btnSave);
            }
            if (strTextBox == "drpBuy_Sale")
            {
                setFocusControl(txtlot_qty);
            }
            if (strTextBox == "drpFuture_Type")
            {
                setFocusControl(drpBuy_Sale);
            }
            if (strTextBox == "txtScript_Code")
            {
                string acname = "";
                if (txtScript_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtScript_Code.Text);
                    if (a == false)
                    {
                        btntxtScript_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Script_Name from qryFNOMaster where FNO_Script_No=" + txtScript_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname != "0")
                        {
                            lblscrpit_Code.Text = acname;
                            //  string acnamestatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where FNO_Script_No=" + txtScript_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                            setFocusControl(txtExpiry_date);

                        }
                        else
                        {
                            txtScript_Code.Text = string.Empty;
                            lblscrpit_Code.Text = acname;
                            setFocusControl(txtScript_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtScript_Code);
                }
            }
            if (strTextBox == "txtExpiry_date")
            {
                setFocusControl(txtLot_Size);
            }
            if (strTextBox == "txtLot_Size")
            {
                setFocusControl(drpFuture_Type);
            }
            if (strTextBox == "txtlot_qty")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(txtValue);
            }
            if (strTextBox == "txtValue")
            {

                setFocusControl(txtStrike_Price);
            }
            if (strTextBox == "txtStrike_Price")
            {
                setFocusControl(btnAdddetails);
            }

            double Net_Brokrage = 0.00;
            double Net_Exchange_Charges = 0.00;
            double Net_STF = 0.00;
            double Net_STT = 0.00;
            double Net_StampDuty = 0.00;
            double Net_CM_Charge = 0.00;
            double Net_Cgst = 0.00;
            double Net_Sgst = 0.00;
            double Net_Igst = 0.00;
            double Net_Postage = 0.00;
            double Net_Expenses = 0.00;
            double Net_ProfitLoss = 0.00;
            double Net_pnl = 0.00;

            Net_Brokrage = Convert.ToDouble(txtNetbrokrage.Text != string.Empty ? txtNetbrokrage.Text : "0.00");
            Net_Exchange_Charges = Convert.ToDouble(txtnetExchangecharges.Text != string.Empty ? txtnetExchangecharges.Text : "0.00");
            Net_STF = Convert.ToDouble(txtnetSTF.Text != string.Empty ? txtnetSTF.Text : "0.00");
            Net_STT = Convert.ToDouble(txtNet_Stt.Text != string.Empty ? txtNet_Stt.Text : "0.00");
            Net_StampDuty = Convert.ToDouble(txtNet_StampDuty.Text != string.Empty ? txtNet_StampDuty.Text : "0.00");
            Net_CM_Charge = Convert.ToDouble(txtcmcharge.Text != string.Empty ? txtcmcharge.Text : "0.00");
            Net_Cgst = Convert.ToDouble(txtNet_Cgst.Text != string.Empty ? txtNet_Cgst.Text : "0.00");
            Net_Sgst = Convert.ToDouble(txtNet_Sgst.Text != string.Empty ? txtNet_Sgst.Text : "0.00");
            Net_Igst = Convert.ToDouble(txtNet_Igst.Text != string.Empty ? txtNet_Igst.Text : "0.00");
            Net_Postage = Convert.ToDouble(txtNet_Postage.Text != string.Empty ? txtNet_Postage.Text : "0.00");

            Net_Expenses = Net_Brokrage + Net_Exchange_Charges + Net_STF + Net_STT + Net_StampDuty + Net_CM_Charge + Net_Cgst + Net_Sgst + Net_Igst + Net_Postage;


            Net_ProfitLoss = Convert.ToDouble(txtNetProfitLoss.Text != string.Empty ? txtNetProfitLoss.Text : "0.00");
            if (Net_ProfitLoss != 0)
            {
                Net_pnl = Net_ProfitLoss + Net_Expenses;
            }

            txtNetExpenses.Text = Net_Expenses.ToString();
            txtNet_pnl.Text = Net_pnl.ToString();

        }


        catch
        {

        }

    }
    #endregion
    private void calculation()
    {
        double qntl = Convert.ToDouble(txtlot_qty.Text);
        double rate = Convert.ToDouble(txtRate.Text);
        double lotsixe = Convert.ToDouble(txtLot_Size.Text);

        double finalA = qntl * rate * lotsixe;
        txtValue.Text = finalA.ToString();
        setFocusControl(txtValue);
    }
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Doc_No=" + hdnf.Value + "  and Year_code=" + Convert.ToInt32(Session["year"].ToString());
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
                txtDoc_No.Text = hdnf.Value;
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
                        string id = clsCommon.getString("select Doc_No from " + tblDetails + " where Doc_No='" + lblID.Text + "' and Doc_No=" + lblID.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (id != string.Empty)
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
                    dt.Columns.Add((new DataColumn("Script_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Script_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Expiry_date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Lot_Size", typeof(int))));
                    dt.Columns.Add((new DataColumn("Future_Type", typeof(string))));
                    dt.Columns.Add((new DataColumn("Buy_Sale", typeof(string))));
                    dt.Columns.Add((new DataColumn("lot_qty", typeof(int))));
                    dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Value", typeof(double))));
                    dt.Columns.Add((new DataColumn("Strike_Price", typeof(double))));
                    dt.Columns.Add((new DataColumn("AutoCarry", typeof(string))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["Doc_No"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Script_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Script_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Expiry_date", typeof(string))));
                dt.Columns.Add((new DataColumn("Lot_Size", typeof(int))));
                dt.Columns.Add((new DataColumn("Future_Type", typeof(string))));
                dt.Columns.Add((new DataColumn("Buy_Sale", typeof(string))));
                dt.Columns.Add((new DataColumn("lot_qty", typeof(int))));
                dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Value", typeof(double))));
                dt.Columns.Add((new DataColumn("Strike_Price", typeof(double))));
                dt.Columns.Add((new DataColumn("AutoCarry", typeof(string))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Script_Code"] = txtScript_Code.Text;
            dr["Script_Code_Name"] = lblscrpit_Code.Text;
            dr["Expiry_date"] = txtExpiry_date.Text;
            dr["Lot_Size"] = txtLot_Size.Text;
            dr["Future_Type"] = drpFuture_Type.Text;
            dr["Buy_Sale"] = drpBuy_Sale.Text;
            dr["lot_qty"] = txtlot_qty.Text;
            dr["Rate"] = txtRate.Text;
            dr["Value"] = txtValue.Text;
            if (txtStrike_Price.Text == string.Empty)
            {

                dr["Strike_Price"] = 0;
            }
            else
            {
                dr["Strike_Price"] = txtStrike_Price.Text;
            }
            dr["AutoCarry"] = Server.HtmlDecode(lblAutoCarry.Text);
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
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
                setFocusControl(txtScript_Code);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            txtScript_Code.Text = string.Empty;
            lblscrpit_Code.Text = string.Empty;
            // txtExpiry_date.Text = string.Empty;
            txtLot_Size.Text = string.Empty;
            // drpFuture_Type.Text = string.Empty;
            // drpBuy_Sale.Text = string.Empty;
            txtlot_qty.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtStrike_Price.Text = string.Empty;
            //lblAutoCarry.Text = string.Empty;
            btnAdddetails.Text = "ADD";
            setFocusControl(btntxtScript_Code);
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
        txtScript_Code.Text = string.Empty;
        txtExpiry_date.Text = string.Empty;
        txtLot_Size.Text = string.Empty;
        drpFuture_Type.Text = string.Empty;
        drpBuy_Sale.Text = string.Empty;
        txtlot_qty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtValue.Text = string.Empty;
        txtStrike_Price.Text = string.Empty;
        lblAutoCarry.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtScript_Code);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtScript_Code.Text = Server.HtmlDecode(gvrow.Cells[Script_Code].Text);
        lblscrpit_Code.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtExpiry_date.Text = Server.HtmlDecode(gvrow.Cells[Expiry_date].Text);
        txtLot_Size.Text = Server.HtmlDecode(gvrow.Cells[Lot_Size].Text);
        drpFuture_Type.Text = Server.HtmlDecode(gvrow.Cells[Future_Type].Text);
        drpBuy_Sale.Text = Server.HtmlDecode(gvrow.Cells[Buy_Sale].Text);
        txtlot_qty.Text = Server.HtmlDecode(gvrow.Cells[lot_qty].Text);
        txtRate.Text = Server.HtmlDecode(gvrow.Cells[Rate].Text);
        txtValue.Text = Server.HtmlDecode(gvrow.Cells[Value].Text);
        txtStrike_Price.Text = Server.HtmlDecode(gvrow.Cells[Strike_Price].Text);
        lblAutoCarry.Text = Server.HtmlDecode(gvrow.Cells[AutoCarry].Text);
    }
    #endregion
    protected void drpFuture_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        searchString = drpFuture_Type.Text;
        strTextBox = "drpFuture_Type";
        csCalculations();
    }
    protected void drpBuy_Sale_SelectedIndexChanged(object sender, EventArgs e)
    {
        searchString = drpBuy_Sale.Text;
        strTextBox = "drpBuy_Sale";
        csCalculations();
    }

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
                string IDExisting = clsCommon.getString("select Doc_No from " + tblDetails + " where Detail_Id='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
            e.Row.Cells[2].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[2].Style["overflow"] = "hidden";

            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].Visible = false;
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].Visible = false;
            //--------------------------------------------------
            e.Row.Cells[Script_Code].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[Script_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Script_Code].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[Script_Code].Font.Size = 10;
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Expiry_date].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[Expiry_date].Style["overflow"] = "hidden";
            e.Row.Cells[Expiry_date].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------


            //--------------------------------------------------
            e.Row.Cells[Lot_Size].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[Lot_Size].Style["overflow"] = "hidden";
            e.Row.Cells[Lot_Size].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Future_Type].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[Future_Type].Style["overflow"] = "hidden";
            e.Row.Cells[Future_Type].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Buy_Sale].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[Buy_Sale].Style["overflow"] = "hidden";
            e.Row.Cells[Buy_Sale].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[lot_qty].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[lot_qty].Style["overflow"] = "hidden";
            e.Row.Cells[lot_qty].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Rate].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Rate].Style["overflow"] = "hidden";
            e.Row.Cells[Rate].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Value].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Value].Style["overflow"] = "hidden";
            e.Row.Cells[Value].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Strike_Price].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Strike_Price].Style["overflow"] = "hidden";
            e.Row.Cells[Strike_Price].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[AutoCarry].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[AutoCarry].Style["overflow"] = "hidden";
            e.Row.Cells[AutoCarry].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            //    e.Row.Cells[0].Visible =true;
            //}

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                string ACarry = grdDetail.Rows[i].Cells[AutoCarry].Text;
                if (ACarry == "Y")
                {
                    grdDetail.Rows[i].Cells[0].Enabled = false;
                    grdDetail.Rows[i].Cells[1].Enabled = false;
                }
                else
                {
                    grdDetail.Rows[i].Cells[0].Enabled = true;
                    grdDetail.Rows[i].Cells[1].Enabled = true;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.Cells[0].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtScript_Code")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("180px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[3].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[4].ControlStyle.Width = new Unit("100px");

            }
            if (v == "txtExpiry_date")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("150");

            }
        }
    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
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
                            setFocusControl(txtScript_Code);
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
        //searchString = txtEditDoc_No.Text;
        //strTextBox = "txtEditDoc_No";
        //csCalculations();

        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                searchString = txtEditDoc_No.Text;
                strTextBox = "txtEditDoc_No";
                pnlPopup.Style["display"] = "block";

                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                txtDoc_No.Enabled = false;
                hdnf.Value = txtEditDoc_No.Text;
                string qry1 = getDisplayQuery();
                fetchRecord(qry1);
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
    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    #region [txtBroker_code_TextChanged]
    protected void txtBroker_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBroker_code.Text;
        strTextBox = "txtBroker_code";
        csCalculations();
    }
    #endregion
    #region [btntxtBroker_code_Click]
    protected void btntxtBroker_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    protected void btnexpriydate_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtExpiry_date";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #region [txtBill_No_TextChanged]
    protected void txtBill_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_No.Text;
        strTextBox = "txtBill_No";
        csCalculations();
    }
    #endregion


    #region [txtsattlement_No_TextChanged]
    protected void txtsattlement_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtsattlement_No.Text;
        strTextBox = "txtsattlement_No";
        csCalculations();
    }
    #endregion
    #region [txtRemark_TextChanged]
    protected void txtRemark_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRemark.Text;
        strTextBox = "txtRemark";
        csCalculations();
    }
    #endregion
    #region [txtNetbrokrage_TextChanged]
    protected void txtNetbrokrage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetbrokrage.Text;
        strTextBox = "txtNetbrokrage";
        csCalculations();
    }
    #endregion
    #region [txtNet_Cgst_TextChanged]
    protected void txtNet_Cgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Cgst.Text;
        strTextBox = "txtNet_Cgst";
        csCalculations();
    }
    #endregion
    #region [txtNet_Sgst_TextChanged]
    protected void txtNet_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Sgst.Text;
        strTextBox = "txtNet_Sgst";
        csCalculations();
    }
    #endregion
    #region [txtNet_Igst_TextChanged]
    protected void txtNet_Igst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Igst.Text;
        strTextBox = "txtNet_Igst";
        csCalculations();
    }
    #endregion
    #region [txtNet_Stt_TextChanged]
    protected void txtNet_Stt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Stt.Text;
        strTextBox = "txtNet_Stt";
        csCalculations();
    }
    #endregion
    #region [txtNet_StampDuty_TextChanged]
    protected void txtNet_StampDuty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_StampDuty.Text;
        strTextBox = "txtNet_StampDuty";
        csCalculations();
    }
    #endregion
    #region [txtNet_pnl_TextChanged]
    protected void txtNet_pnl_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_pnl.Text;
        strTextBox = "txtNet_pnl";
        csCalculations();
    }
    #endregion

    #region [txtScript_Code_TextChanged]
    protected void txtScript_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtScript_Code.Text;
        strTextBox = "txtScript_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtScript_Code_Click]
    protected void btntxtScript_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtScript_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtExpiry_date_TextChanged]
    protected void txtExpiry_date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtExpiry_date.Text;
        strTextBox = "txtExpiry_date";
        csCalculations();
    }
    #endregion


    #region [txtLot_Size_TextChanged]
    protected void txtLot_Size_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLot_Size.Text;
        strTextBox = "txtLot_Size";
        csCalculations();
    }
    #endregion


    #region [txtlot_qty_TextChanged]
    protected void txtlot_qty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtlot_qty.Text;
        strTextBox = "txtlot_qty";
        csCalculations();
    }
    #endregion


    #region [txtRate_TextChanged]
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        calculation();
        //csCalculations();
    }
    #endregion


    #region [txtValue_TextChanged]
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtValue.Text;
        strTextBox = "txtValue";
        //calculation();
        // csCalculations();
    }
    #endregion


    #region [txtStrike_Price_TextChanged]
    protected void txtStrike_Price_TextChanged(object sender, EventArgs e)
    {
        searchString = txtStrike_Price.Text;
        strTextBox = "txtStrike_Price";
        csCalculations();
    }
    #endregion

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
            if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
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
                    searchString = txtEditDoc_No.Text;
                    strTextBox = "txtEditDoc_No";
                    pnlPopup.Style["display"] = "block";

                    if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
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

                    foreach (var s in split)
                    {
                        string aa = s.ToString();
                        name += "( Doc_No Like '%" + aa + "%'or brokername like '%" + aa + "%') and";
                    }
                    //name = name.Remove(name.Length - 2);
                    //lblPopupHead.Text = "--Select Group--";
                    //string qry = " select Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Doc_No";
                    //this.showPopup(qry);

                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select distinct Doc_No,docdate as Doc_Date ,brokername as BrokerName from " + qryCommon + " where Year_Code=" + Session["year"] + " and Company_Code="
                        + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and " + name + " order by Doc_No desc";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtDoc_No")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBroker_code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and (" + name + ") order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtExpiry_date")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Expiry_Date Like '%" + aa + "%'or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Convert(varchar(10),Expiry_Date,103) as Expiry_Date  from Expiry_Master where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and (" + name + ") order by Expiry_Date";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtScript_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "FNO_Script_No Like '%" + aa + "%'or Script_Name like '%" + aa + "%' or FNO_Type like '%" + aa + "%' or Lot_Size like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select FNO_Script_No,Script_Name,Lot_Size,FNO_Type, Convert(varchar(10),Expiry_Date,103) as Expiry_Date from qryFNOMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and (" + name + ") order by Script_Name";

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
        #region [Validation Part]
        bool isValidated = true;
        //        if textbox is date then if condition will be like this if(clsCommon.isValidDate(txtDoc_Date.Text==true))
        /*  In Grid At Least One Record is required
int count = 0;
  if (grdDetail.Rows.Count == 0)
{
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"","alert('Please Enter Details!!!!             isValidated = false;
setFocusControl(btnOpenDetailsPopup); 
return;  
}
if (grdDetail.Rows.Count >= 1) 
for (int i = 0; i < grdDetail.Rows.Count; i++)
{
if (grdDetail.Rows[i].Cells[10].Text == "D")
{
count++; 
}
}
if (grdDetail.Rows.Count == count)
{
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"","alert('Please Enter Minumun One Details!!!!          isValidated = false; 
setFocusControl(btnOpenDetailsPopup); 
return;
} 
}
*/
        if (btnSave.Text != "Save")
        {
            string DocDate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string DocNo = clsDAL.GetString("select Doc_No from  Future_Head where Ac_Code=" + txtBroker_code.Text + " and Doc_Date>'" + DocDate
                + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            if (DocNo != "0" && DocNo != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You Can Edit only last Record ');", true);
                return;
            }
        }
        if (btnSave.Text == "Save")
        {
            //DateTime DocDate= DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

            string DocDate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            string doc_no = clsDAL.GetString("select Doc_No from Future_Head where Ac_Code=" + txtBroker_code.Text + " and Doc_Date>='" + DocDate
                + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_code=" + Convert.ToInt32(Session["year"].ToString() + ""));
            if (doc_no != "0" && doc_no != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You Can not Insert Record Back Dated entry not allowded');", true);
                return;
            }
        }

        if (grddetailFuture.Rows.Count > 0)
        {
            for (int i = 0; i < grddetailFuture.Rows.Count; i++)
            {
                double ClosingQty = Convert.ToDouble(grddetailFuture.Rows[i].Cells[13].Text);
                if (ClosingQty != 0)
                {

                    TextBox txtClosing_Rate = (TextBox)grddetailFuture.Rows[i].Cells[14].FindControl("txtClosing_Rate");
                    if (txtClosing_Rate.Text == "0.00" || txtClosing_Rate.Text == "" || txtClosing_Rate.Text == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Closing Rate can not be 0');", true);
                        return;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }
            }
        }

        #endregion

        #region -Head part declearation

        XElement root = new XElement("ROOT");
        XElement child1 = new XElement("Head");
        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
        int Branch_Code = Convert.ToInt32(Convert.ToInt32(Session["Branch_Code"].ToString()));
        string Created_By = Session["user"].ToString();
        string Modified_By = Session["user"].ToString();
        string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string retValue = string.Empty;
        string strRev = string.Empty;
        #endregion-End of Head part declearation
        #region Save Head Part
        //     child1.SetAttributeValue("EditDoc_No", txtEditDoc_No.Text != string.Empty ? txtEditDoc_No.Text : "0");
        //   child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");
        child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        child1.SetAttributeValue("Ac_Code", txtBroker_code.Text != string.Empty ? txtBroker_code.Text : "0");
        child1.SetAttributeValue("Bill_No", txtBill_No.Text != string.Empty ? txtBill_No.Text : "0");
        child1.SetAttributeValue("Sattlement_No", txtsattlement_No.Text != string.Empty ? txtsattlement_No.Text : "0");
        child1.SetAttributeValue("Remark", txtRemark.Text);
        child1.SetAttributeValue("NetBrokrage", txtNetbrokrage.Text != string.Empty ? txtNetbrokrage.Text : "0.00");
        child1.SetAttributeValue("Net_cgst", txtNet_Cgst.Text != string.Empty ? txtNet_Cgst.Text : "0.00");
        child1.SetAttributeValue("Net_sgst", txtNet_Sgst.Text != string.Empty ? txtNet_Sgst.Text : "0.00");
        child1.SetAttributeValue("Net_igst", txtNet_Igst.Text != string.Empty ? txtNet_Igst.Text : "0.00");
        child1.SetAttributeValue("Net_stt", txtNet_Stt.Text != string.Empty ? txtNet_Stt.Text : "0.00");
        child1.SetAttributeValue("Net_StampDuty", txtNet_StampDuty.Text != string.Empty ? txtNet_StampDuty.Text : "0.00");
        child1.SetAttributeValue("Net_pnl", txtNet_pnl.Text != string.Empty ? txtNet_pnl.Text : "0.00");

        child1.SetAttributeValue("Net_Exchange_Charges", txtnetExchangecharges.Text != string.Empty ? txtnetExchangecharges.Text : "0.00");
        child1.SetAttributeValue("Net_STF", txtnetSTF.Text != string.Empty ? txtnetSTF.Text : "0.00");
        child1.SetAttributeValue("Net_CM_Charge", txtcmcharge.Text != string.Empty ? txtcmcharge.Text : "0.00");
        child1.SetAttributeValue("Net_Postage", txtNet_Postage.Text != string.Empty ? txtNet_Postage.Text : "0.00");

        child1.SetAttributeValue("Net_Expenses", txtNetExpenses.Text != string.Empty ? txtNetExpenses.Text : "0.00");
        child1.SetAttributeValue("Net_ProfitLoss", txtNetProfitLoss.Text != string.Empty ? txtNetProfitLoss.Text : "0.00");

        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_code", Year_Code);
        child1.SetAttributeValue("Branch_Code", Branch_Code);
        if (btnSave.Text != "Save")
        {
            child1.SetAttributeValue("Modified_By", Modified_By);
            child1.SetAttributeValue("Modified_Date", Modified_Date);
            child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

        }
        else
        {
            child1.SetAttributeValue("Created_By", Created_By);
            child1.SetAttributeValue("Created_Date", Created_Date);
        }
        root.Add(child1);
        #endregion-End of Head part Save

        #region save Head Master
        #region --------------------  Details --------------------
        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {
            string CheckingFlag = string.Empty;
            XElement child2 = new XElement("Details");
            Int32 Detail_Id = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);
            if (btnSave.Text != "Save")
            {
                if (grdDetail.Rows[i].Cells[Rowaction].Text == "A")//RowAction Value
                {
                    CheckingFlag = "1";
                }
                else if (grdDetail.Rows[i].Cells[Rowaction].Text == "U" || grdDetail.Rows[i].Cells[Rowaction].Text == "N")
                {
                    CheckingFlag = "2";
                }
                else
                {
                    CheckingFlag = "3";
                }
            }
            child2.SetAttributeValue("Doc_No", txtDoc_No.Text);
            child2.SetAttributeValue("Company_Code", Company_Code);
            child2.SetAttributeValue("Year_code", Year_Code);
            child2.SetAttributeValue("Branch_Code", Branch_Code);


            child2.SetAttributeValue("Script_Code", Server.HtmlDecode(grdDetail.Rows[i].Cells[Script_Code].Text));
            string Expiry_date1 = Server.HtmlDecode(grdDetail.Rows[i].Cells[Expiry_date].Text);
            child2.SetAttributeValue("Expiry_Date", DateTime.Parse(Server.HtmlDecode(Expiry_date1), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child2.SetAttributeValue("Lot_Size", Server.HtmlDecode(grdDetail.Rows[i].Cells[Lot_Size].Text));
            child2.SetAttributeValue("Future_Type", Server.HtmlDecode(grdDetail.Rows[i].Cells[Future_Type].Text));
            child2.SetAttributeValue("Buy_Sale", Server.HtmlDecode(grdDetail.Rows[i].Cells[Buy_Sale].Text));
            child2.SetAttributeValue("Lot_Qty", Server.HtmlDecode(grdDetail.Rows[i].Cells[lot_qty].Text));
            child2.SetAttributeValue("Rate", Server.HtmlDecode(grdDetail.Rows[i].Cells[Rate].Text));
            child2.SetAttributeValue("Value", Server.HtmlDecode(grdDetail.Rows[i].Cells[Value].Text));
            child2.SetAttributeValue("Strike_Price", Server.HtmlDecode(grdDetail.Rows[i].Cells[Strike_Price].Text));
            child2.SetAttributeValue("detail_Id", Server.HtmlDecode(grdDetail.Rows[i].Cells[2].Text));
            child2.SetAttributeValue("AutoCarry", Server.HtmlDecode(grdDetail.Rows[i].Cells[AutoCarry].Text));
            if (btnSave.Text != "Save")
            {
                child2.SetAttributeValue("Flag", CheckingFlag);
            }
            child1.Add(child2);
        }
        #endregion

        #region grddetailFuture
        double Closing_Value = 0.00;
        double Closing_Qty = 0.00;
        double Closing_Rate = 0.00;
        double Lot_SizeDetail = 0.00;

        for (int i = 0; i < grddetailFuture.Rows.Count; i++)
        {

            string CheckingFlag = string.Empty;
            XElement child3 = new XElement("DetailGrid");


            child3.SetAttributeValue("Doc_No", txtDoc_No.Text);
            child3.SetAttributeValue("Company_Code", Company_Code);
            child3.SetAttributeValue("Year_Code", Year_Code);




            child3.SetAttributeValue("detail_Id", grddetailFuture.Rows[i].Cells[0].Text);
            child3.SetAttributeValue("Script_Code", grddetailFuture.Rows[i].Cells[1].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[1].Text) : 0);

            string Expiry_date1 = Server.HtmlDecode(grddetailFuture.Rows[i].Cells[3].Text);

            child3.SetAttributeValue("Expiry_Date", DateTime.Parse(Server.HtmlDecode(Expiry_date1), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
            child3.SetAttributeValue("Lot_Size", grddetailFuture.Rows[i].Cells[4].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[4].Text) : 0);
            child3.SetAttributeValue("Future_Type", grddetailFuture.Rows[i].Cells[5].Text);
            child3.SetAttributeValue("Buy_Qty", grddetailFuture.Rows[i].Cells[6].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[6].Text) : 0);
            child3.SetAttributeValue("Buy_Rate", grddetailFuture.Rows[i].Cells[7].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[7].Text) : 0);
            child3.SetAttributeValue("Buy_Value", grddetailFuture.Rows[i].Cells[8].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[8].Text) : 0);
            child3.SetAttributeValue("Sale_Qty", grddetailFuture.Rows[i].Cells[9].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[9].Text) : 0);
            child3.SetAttributeValue("Sale_Rate", grddetailFuture.Rows[i].Cells[10].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[10].Text) : 0);
            child3.SetAttributeValue("Sale_Value", grddetailFuture.Rows[i].Cells[11].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[11].Text) : 0);
            child3.SetAttributeValue("Strike_Price", grddetailFuture.Rows[i].Cells[12].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[12].Text) : 0);
            child3.SetAttributeValue("Closing_Qty", grddetailFuture.Rows[i].Cells[13].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[13].Text) : 0);

            TextBox txtClosing_Rate = (TextBox)grddetailFuture.Rows[i].Cells[14].FindControl("txtClosing_Rate");
            child3.SetAttributeValue("Closing_Rate", Server.HtmlDecode(txtClosing_Rate.Text) != string.Empty ? Convert.ToDouble(txtClosing_Rate.Text) : 0);

            TextBox txtProfitLoss = (TextBox)grddetailFuture.Rows[i].Cells[15].FindControl("txtProfitLoss");
            child3.SetAttributeValue("Profit_Loss", Server.HtmlDecode(txtProfitLoss.Text) != string.Empty ? Convert.ToDouble(txtProfitLoss.Text) : 0);



            //TextBox txtProfitLoss = (TextBox)grddetailFuture.Rows[i].Cells[15].FindControl("txtProfitLoss");
            //double profitloss = 0.00;
            // profitloss = Convert.ToDouble(Server.HtmlDecode(txtProfitLoss.Text));
            //child3.SetAttributeValue("Profit_Loss", profitloss);



            // child3.SetAttributeValue("Profit_Loss", grddetailFuture.Rows[i].Cells[15].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[15].Text) : 0);


            Closing_Qty = (grddetailFuture.Rows[i].Cells[13].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[13].Text) : 0);
            Closing_Rate = (Server.HtmlDecode(txtClosing_Rate.Text) != string.Empty ? Convert.ToDouble(txtClosing_Rate.Text) : 0);
            Lot_SizeDetail = (grddetailFuture.Rows[i].Cells[4].Text != string.Empty ? Convert.ToDouble(grddetailFuture.Rows[i].Cells[4].Text) : 0);
            Closing_Value = Closing_Qty * Closing_Rate * Lot_SizeDetail;
            child3.SetAttributeValue("Closing_Value", Closing_Value);

            root.Add(child3);
            //}
        }
        #endregion
        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_FutureHead";
        string xmlfile = XMLReport;
        string op = "";
        string returnmaxno = "";
        int flag;
        if (btnSave.Text == "Save")
        {
            #region[Insert]
            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        else
        {
            #region[Update]
            flag = 6;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        txtDoc_No.Text = returnmaxno;
        hdnf.Value = txtDoc_No.Text;
        retValue = op;

        if (retValue == "-1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Added! No=" + returnmaxno + "');", true);
        }
        if (retValue == "-2" || retValue == "-3")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated! No=" + returnmaxno + "');", true);
        }
        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);

        #endregion
    }
    #endregion

    #region grddetailFuture
    protected void grddetailFuture_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
        }
        catch
        {
        }

    }
    #endregion
    protected void grddetailFuture_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].ControlStyle.Width = new Unit("20px");
        e.Row.Cells[0].Style["overflow"] = "hidden";

        e.Row.Cells[1].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[1].Style["overflow"] = "hidden";

        e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
        e.Row.Cells[2].Style["overflow"] = "hidden";

        e.Row.Cells[3].ControlStyle.Width = new Unit("80px");
        e.Row.Cells[3].Style["overflow"] = "hidden";

        e.Row.Cells[4].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[4].Style["overflow"] = "hidden";

        e.Row.Cells[5].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[5].Style["overflow"] = "hidden";
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

        e.Row.Cells[6].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[6].Style["overflow"] = "hidden";


        e.Row.Cells[7].ControlStyle.Width = new Unit("120px");
        e.Row.Cells[7].Style["overflow"] = "hidden";
        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[8].ControlStyle.Width = new Unit("120px");
        e.Row.Cells[8].Style["overflow"] = "hidden";
        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[9].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[9].Style["overflow"] = "hidden";
        e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[10].ControlStyle.Width = new Unit("120px");
        e.Row.Cells[10].Style["overflow"] = "hidden";
        e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[11].ControlStyle.Width = new Unit("120px");
        e.Row.Cells[11].Style["overflow"] = "hidden";
        e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[12].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[12].Style["overflow"] = "hidden";
        e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[13].Style["overflow"] = "hidden";
        e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[14].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[14].Style["overflow"] = "hidden";
        e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

        e.Row.Cells[15].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[15].Style["overflow"] = "hidden";
        e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;


        for (int i = 0; i < grddetailFuture.Rows.Count; i++)
        {

            // string closingqty = e.Row.Cells[13].Text;
            string closingqty = grddetailFuture.Rows[i].Cells[13].Text;
            if (closingqty == "0")
            {
                //e.Row.Cells[14].Enabled = false;
                grddetailFuture.Rows[i].Cells[14].Enabled = false;
            }
            else
            {
                //e.Row.Cells[14].Enabled = true;
                grddetailFuture.Rows[i].Cells[14].Enabled = true;
            }
        }

        //e.Row.Cells[16].ControlStyle.Width = new Unit("60px");
        //e.Row.Cells[16].Style["overflow"] = "hidden";
        //e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;

        //e.Row.Cells[8].Visible = false;


    }


    #region [txtnetExchangecharges_TextChanged]
    protected void txtnetExchangecharges_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnetExchangecharges.Text;
        strTextBox = "txtnetExchangecharges";
        csCalculations();
    }
    #endregion
    #region [txtnetSTF_TextChanged]
    protected void txtnetSTF_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnetSTF.Text;
        strTextBox = "txtnetSTF";
        csCalculations();
    }
    #endregion
    #region [txtNet_Postage_TextChanged]
    protected void txtNet_Postage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Postage.Text;
        strTextBox = "txtNet_Postage";
        csCalculations();
    }
    #endregion

    #region [txtcmcharge_TextChanged]
    protected void txtcmcharge_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcmcharge.Text;
        strTextBox = "txtcmcharge";
        csCalculations();
    }
    #endregion

    #region [txtNetExpenses_TextChanged]
    protected void txtNetExpenses_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetExpenses.Text;
        strTextBox = "txtNetExpenses";
        csCalculations();
    }
    #endregion
    #region [txtNetProfitLoss_TextChanged]
    protected void txtNetProfitLoss_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetProfitLoss.Text;
        strTextBox = "txtNetProfitLoss";
        csCalculations();
    }
    #endregion
    #region [btnDaily_Positoin Click]
    protected void btnDaily_Positoin_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);

        string Doc_Date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        #region Details grid
        qry = "select '' as Detail_Id,Script_Code,Script_Name,convert(varchar(10),Expiry_Date,103) as Expiry_Date,Lot_Size,Future_Type as Type,buyqty as Buy_Qty,buyavg as Buy_Rate,buyvalue as Buy_Value," +
            " Sqleqty as Sale_Qty,saleavg as Sale_Rate,salevalue as Sale_Value,0 as SP,balanceqty as Closing_Qty, 0 as Closing_Rate,profit as ProfitLoss from qryfuturedausummarystage5 where Doc_Date='" + Doc_Date +
            "' And Doc_No='" + txtDoc_No.Text + "' order by Script_Name";

        // qry = "";
        double NetProfitLoss = 0.00;
        double TotNetProfitLoss = 0.00;
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    //dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["Detail_Id"] = i + 1;

                        NetProfitLoss = Convert.ToDouble(dt.Rows[i]["ProfitLoss"]);
                        TotNetProfitLoss = TotNetProfitLoss + NetProfitLoss;
                    }


                    grddetailFuture.DataSource = dt;
                    grddetailFuture.DataBind();
                    ViewState["currentTable1"] = dt;
                }
                else
                {
                    grddetailFuture.DataSource = null;
                    grddetailFuture.DataBind();
                    ViewState["currentTable1"] = null;
                }
            }
            else
            {
                grddetailFuture.DataSource = null;
                grddetailFuture.DataBind();
                ViewState["currentTable1"] = null;
            }
        }
        else
        {
            grddetailFuture.DataSource = null;
            grddetailFuture.DataBind();
            ViewState["currentTable1"] = null;
        }
        #endregion
        txtNetProfitLoss.Text = TotNetProfitLoss.ToString();
        txtNet_pnl.Text = TotNetProfitLoss.ToString();
        Panel1.Enabled = true;
        pnlgrdDetail.Enabled = false;
        btnAdddetails.Enabled = false;
        btnClosedetails.Enabled = false;

    }
    #endregion

    #region [DeletebtnDaily_Positoin Click]
    protected void DeletebtnDaily_Positoin_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string str = string.Empty;
                DataSet xml_ds = new DataSet();
                if (str == string.Empty)
                {
                    string currentDoc_No = txtDoc_No.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                        string strRev = string.Empty;
                        child1.SetAttributeValue("Doc_No", txtDoc_No.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);
                        child1.SetAttributeValue("Year_code", Year_Code);

                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_FutureHead";
                        string xmlfile = XMLReport;
                        string op = "";
                        string returnmaxno = "";
                        int flag = 11;
                        xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
                        strrev = op;


                        //clsButtonNavigation.enableDisable("S");
                        //this.enableDisableNavigateButtons();
                        //this.makeEmptyForm("S");
                        //qry = getDisplayQuery();
                        //this.fetchRecord(qry);

                        btnDeletebtnDaily_Positoin.Enabled = false;
                        btnDaily_Positoin.Enabled = true;
                        txtNetProfitLoss.Text = "0.00";
                    }

                }
            }
        }
        catch
        {
        }
    }
    #endregion
}

