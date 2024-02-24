using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
public partial class PgeEquityPurchaseSale : System.Web.UI.Page
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
    string Tran_Type = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    int Rowaction = 2;
    int Srno = 3;
    string Action = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "EquityPurchaseSale";
            tblDetails = "";
            qryCommon = "qryEquityPurchaseSale";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["Doc_No"];

                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        //this.NextNumber();
                        setFocusControl(drpTran_Type);
                    }

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
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
                //btnAdddetails.Enabled = false;
                //btnClosedetails.Enabled = false;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                drpTran_Type.Enabled = false;
                txtDate.Enabled = false;
                CalendarExtenderDatetxtDate.Enabled = false;
                drpLedgerEntry.Enabled = false;
                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = false;
                txtPurc_No.Enabled = false;
                btntxtPurc_No.Enabled = false;
                lblPurc_Year_Code.Enabled = false;
                lblPurc_Company_code.Enabled = false;
                txtQty.Enabled = false;
                txtBonus.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtProfit_Loss.Enabled = false;
                txtSale_To.Enabled = false;
                btnSale_To.Enabled = false;
                txtBrokage.Enabled = false;
                txtCess_Tax.Enabled = false;
                txtSTT.Enabled = false;
                txtService_Tax.Enabled = false;
                txtStamp_Charge.Enabled = false;
                txtTurnOver_Tax.Enabled = false;
                txtNet_Value.Enabled = false;
                txtNet_Rate.Enabled = false;
                txtNarration.Enabled = false;
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
                //btnAdddetails.Enabled = true;
                //btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                drpTran_Type.Enabled = true;
                txtDate.Enabled = true;
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDate.Enabled = true;
                drpLedgerEntry.Enabled = true;
                txtScript_Code.Enabled = true;
                lblScriptCode.Text = string.Empty;
                btntxtScript_Code.Enabled = true;
                drpTran_Type.SelectedValue = "P";
                if (drpTran_Type.SelectedValue == "P")
                {
                    txtPurc_No.Enabled = false;
                }
                else
                {
                    txtPurc_No.Enabled = true;
                }
                btntxtPurc_No.Enabled = true;
                lblPurc_Year_Code.Text = string.Empty;
                lblPurc_Company_code.Text = string.Empty;
                txtQty.Enabled = true;
                txtBonus.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtProfit_Loss.Enabled = true;
                txtSale_To.Enabled = true;
                lblSaleTo.Text = string.Empty;
                btnSale_To.Enabled = true;
                txtBrokage.Enabled = true;
                txtCess_Tax.Enabled = true;
                txtSTT.Enabled = true;
                txtService_Tax.Enabled = true;
                txtStamp_Charge.Enabled = true;
                txtTurnOver_Tax.Enabled = true;
                txtNet_Value.Enabled = true;
                txtNet_Rate.Enabled = true;
                txtNarration.Enabled = true;
                #region set Business logic for save
                #endregion
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
                drpTran_Type.Enabled = false;
                txtDate.Enabled = false;
                CalendarExtenderDatetxtDate.Enabled = false;
                drpLedgerEntry.Enabled = false;
                txtScript_Code.Enabled = false;
                btntxtScript_Code.Enabled = false;
                txtPurc_No.Enabled = false;
                btntxtPurc_No.Enabled = false;
                lblPurc_Year_Code.Enabled = false;
                lblPurc_Company_code.Enabled = false;
                txtQty.Enabled = false;
                txtBonus.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtProfit_Loss.Enabled = false;
                txtSale_To.Enabled = false;
                btnSale_To.Enabled = false;
                txtBrokage.Enabled = false;
                txtCess_Tax.Enabled = false;
                txtSTT.Enabled = false;
                txtService_Tax.Enabled = false;
                txtStamp_Charge.Enabled = false;
                txtTurnOver_Tax.Enabled = false;
                txtNet_Value.Enabled = false;
                txtNet_Rate.Enabled = false;
                txtNarration.Enabled = false;
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
                drpTran_Type.Enabled = true;
                txtDate.Enabled = true;
                CalendarExtenderDatetxtDate.Enabled = true;
                drpLedgerEntry.Enabled = true;
                txtScript_Code.Enabled = true;
                btntxtScript_Code.Enabled = true;
                txtPurc_No.Enabled = true;
                btntxtPurc_No.Enabled = true;
                lblPurc_Year_Code.Enabled = true;
                lblPurc_Company_code.Enabled = true;
                txtQty.Enabled = true;
                txtBonus.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtProfit_Loss.Enabled = true;
                txtSale_To.Enabled = true;
                btnSale_To.Enabled = true;
                txtBrokage.Enabled = true;
                txtCess_Tax.Enabled = true;
                txtSTT.Enabled = true;
                txtService_Tax.Enabled = true;
                txtStamp_Charge.Enabled = true;
                txtTurnOver_Tax.Enabled = true;
                txtNet_Value.Enabled = true;
                txtNet_Rate.Enabled = true;
                txtNarration.Enabled = true;
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
    //#region [enableDisableNavigateButtons]
    //private void enableDisableNavigateButtons()
    //{
    //    #region enable disable previous next buttons
    //    int RecordCount = 0;
    //    string query = "";
    //    query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code="Convert.ToInt32(Session["year"]).ToString());
    // string cnt = clsCommon.getString(query); 
    //   if (cnt != string.Empty) 
    //        {
    // RecordCount = Convert.ToInt32(cnt);
    //        }
    //    if (RecordCount != 0 && RecordCount == 1)
    //    {
    //        btnFirst.Enabled = true;
    //        btnPrevious.Enabled = false;
    //        btnNext.Enabled = false;
    //        btnLast.Enabled = false;
    //    }
    //    else if (RecordCount != 0 && RecordCount > 1)
    //    {
    //        btnFirst.Enabled = true;
    //        btnPrevious.Enabled = false;
    //        btnNext.Enabled = false;
    //        btnLast.Enabled = true;
    //    }
    //    if (txtDoc_No.Text != string.Empty)
    //    {
    //        #region check for next or previous record exist or not
    //        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())+ "' and Year_Code="+Convert.ToInt32(Session["year"]).ToString())+" ORDER BY Doc_No asc  ";
    //        string strDoc_No = clsCommon.getString(query);
    //         if (strDoc_No != string.Empty)
    //         {
    //          btnNext.Enabled = true;
    //          btnLast.Enabled = true;
    //         }
    //        else
    //         {
    //          btnNext.Enabled = false;
    //          btnLast.Enabled = false;
    //         }
    //        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())+ "' and Year_Code="+Convert.ToInt32(Session["year"]).ToString())+" ORDER BY Doc_No desc  ";
    //         if (strDoc_No != string.Empty)
    //         {
    //          btnPrevious.Enabled = true;
    //          btnFirst.Enabled = true;
    //         }
    //        else
    //         {
    //          btnPrevious.Enabled = false;
    //          btnFirst.Enabled = false;
    //         }
    //    }
    //        #endregion
    //    #endregion
    //}
    //#endregion

    //#region [First]
    //protected void btnFirst_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string query = "";
    //        query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
    //        hdnf.Value = clsCommon.getString(query);
    //        navigateRecord();
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion
    //#region [Previous]
    //protected void btnPrevious_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtDoc_No.Text != string.Empty)
    //        {
    //            string query = "";
    //            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No desc  ";
    //            hdnf.Value = clsCommon.getString(query);
    //            navigateRecord();
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion
    //#region [Next]
    //protected void btnNext_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtDoc_No.Text != string.Empty)
    //        {
    //            string query = "";
    //            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc  ";
    //            hdnf.Value = clsCommon.getString(query);
    //            navigateRecord();
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion
    //#region [Last]
    //protected void btnLast_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string query = "";
    //        query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
    //        hdnf.Value = clsCommon.getString(query);
    //        navigateRecord();
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string check = txtEditDoc_No.Text;
        string TranType = drpTran_Type.SelectedValue;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
            setFocusControl(drpLedgerEntry);

            string qry = "select max(Doc_No) as Doc_No from " + tblHead + " where Year_Code=" + Convert.ToString(Session["Year"]).ToString()
                + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString();

            Int32 Doc_No = Convert.ToInt32(clsCommon.getString(qry));
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
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_No);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                bool isValidated = true;
                string purchaseno = clsCommon.getString("select Doc_No from qryEquityPurchase where Purc_No=" + txtDoc_No.Text + " and  Purc_Company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Purc_Year_Code='" + Convert.ToInt32(Session["year"].ToString()));
                if (purchaseno != string.Empty)
                {
                    isValidated = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Do not Delete Record it is used');", true);
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
                        child1.SetAttributeValue("Year_Code", Year_Code);
                        //child1.SetAttributeValue("Tran_Type", "");
                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_EquityPurchaseSale";
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

                    //    DataTable dt_dalete = xml_ds.Tables[0];
                    //    string HDNF_VALUE = "";
                    //    if (dt_dalete.Rows.Count > 0)
                    //    {
                    //        HDNF_VALUE = dt_dalete.Rows[0]["Doc_No"].ToString();
                    //    }
                    //    hdnf.Value = HDNF_VALUE;

                    //    if (hdnf.Value == string.Empty)
                    //    {
                    //        DataTable dt_dalete_two = xml_ds.Tables[1];
                    //        if (dt_dalete_two.Rows.Count > 0)
                    //        {
                    //            hdnf.Value = dt_dalete_two.Rows[0]["Doc_No"].ToString();
                    //        }
                    //    }
                    //    if (hdnf.Value != string.Empty)
                    //    {
                    //        query = getDisplayQuery();
                    //        bool recordExist = this.fetchRecord(query);
                    //        this.makeEmptyForm("S");
                    //        clsButtonNavigation.enableDisable("S");
                    //    }
                    //    else
                    //    {
                    //        this.makeEmptyForm("N");
                    //        clsButtonNavigation.enableDisable("N");
                    //        btnEdit.Enabled = false;
                    //        btnDelete.Enabled = false;
                    //    }
                    } //this.enableDisableNavigateButtons();
                    Response.Redirect("../Master/PgeEquityPurchaseSaleUtility.aspx");
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
        hdnf.Value = clsCommon.getString("select max(Doc_No) from " + qryCommon + " where Company_Code=" + Session["Company_Code"].ToString() + " and " +
               " Year_Code=" + Session["year"].ToString() + "");


        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry); 

        //if (hdnf.Value != string.Empty)
        //{
        //    string query = getDisplayQuery();
        //    bool recordExist = this.fetchRecord(query);
        //}
        //else
        //{
        //    this.showLastRecord();
        //}
        //string qry = clsCommon.getString("select count(Doc_No) from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        //if (qry != "0")
        //{
        //    clsButtonNavigation.enableDisable("S");
        //    this.makeEmptyForm("S");
        //    this.enableDisableNavigateButtons();
        //}
        //else
        //{
        //    clsButtonNavigation.enableDisable("N");
        //    this.makeEmptyForm("N");
        //    this.enableDisableNavigateButtons();
        //    btnEdit.Enabled = false;
        //    btnDelete.Enabled = false;
        //}
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
                        //txtEditDoc_No.Text = dt.Rows[0]["EditDoc_No"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        //lbldoc_no.Text = dt.Rows[0][""].ToString();
                        drpTran_Type.SelectedValue = dt.Rows[0]["Tran_Type"].ToString();
                        txtDate.Text = dt.Rows[0]["Date"].ToString();
                        drpLedgerEntry.SelectedValue = dt.Rows[0]["LedgerEntry"].ToString();
                        txtScript_Code.Text = dt.Rows[0]["Script_Code"].ToString();
                        lblScriptCode.Text = dt.Rows[0]["Script_Name"].ToString();
                        txtPurc_No.Text = dt.Rows[0]["Purc_No"].ToString();
                        lblPurc_Year_Code.Text = dt.Rows[0]["Purc_Year_Code"].ToString();
                        lblPurc_Company_code.Text = dt.Rows[0]["Purc_Company_code"].ToString();
                        txtQty.Text = dt.Rows[0]["Qty"].ToString();
                        txtBonus.Text = dt.Rows[0]["Bonus"].ToString();
                        txtRate.Text = dt.Rows[0]["Rate"].ToString();
                        txtValue.Text = dt.Rows[0]["Value"].ToString();
                        txtProfit_Loss.Text = dt.Rows[0]["Profit_Loss"].ToString();
                        txtSale_To.Text = dt.Rows[0]["Sale_To"].ToString();
                        lblSaleTo.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        txtBrokage.Text = dt.Rows[0]["Brokage"].ToString();
                        txtCess_Tax.Text = dt.Rows[0]["Cess_Tax"].ToString();
                        txtSTT.Text = dt.Rows[0]["STT"].ToString();
                        txtService_Tax.Text = dt.Rows[0]["Service_Tax"].ToString();
                        txtStamp_Charge.Text = dt.Rows[0]["Stamp_Charge"].ToString();
                        txtTurnOver_Tax.Text = dt.Rows[0]["TurnOver_Tax"].ToString();
                        txtNet_Value.Text = dt.Rows[0]["Net_Value"].ToString();
                        txtNet_Rate.Text = dt.Rows[0]["Net_Rate"].ToString();
                        txtNarration.Text = dt.Rows[0]["Narration"].ToString();

                        string Date = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                        string PSBal = clsCommon.getString("select isnull(sum(Net_Value),0) as PSBal from EquityPurchaseSale where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Sale_To=" + txtSale_To.Text + " and Date='" + Date + "' and Tran_Type='P'");

                        string SBBal = clsCommon.getString("select isnull(sum(Net_Value),0) as SBBal from EquityPurchaseSale where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
                           + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Sale_To=" + txtSale_To.Text + " and Date='" + Date + "' and Tran_Type='S'");

                        double PSBalance = 0.00;
                        double SBBalance = 0.00;
                        double NetBal = 0.00;
                        PSBalance = Convert.ToDouble(PSBal);
                        SBBalance = Convert.ToDouble(SBBal);
                        NetBal = PSBalance - SBBalance;
                        lblbalannce.Text = NetBal.ToString();

                        //R_YearCode.Value = dt.Rows[0]["Year_Code"].ToString();
                        //R_CompanyCode.Value = dt.Rows[0]["Company_Code"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            hdnf.Value = txtDoc_No.Text;
            
            //this.enableDisableNavigateButtons();
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
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(drpTran_Type);
            }
            if (strTextBox == "drpTran_Type")
            {
                setFocusControl(txtDate);
            }
            if (strTextBox == "txtDate")
            {
                setFocusControl(drpLedgerEntry);
            }
            if (strTextBox == "drpLedgerEntry")
            {
                setFocusControl(txtScript_Code);
            }
            if (strTextBox == "txtScript_Code")
            {
                string scriptcodetext = txtScript_Code.Text.Trim();
                if (scriptcodetext != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(scriptcodetext);
                    if (a == false)
                    {
                        btntxtScript_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        string scriptName = clsCommon.getString("select Script_Name from Script_Master where FNO_Script_No=" + scriptcodetext + " and FNO_Type = 'E'");
                        if (scriptName != string.Empty && scriptName != "0")
                        {
                            lblScriptCode.Text = scriptName;
                            if (drpTran_Type.SelectedValue != "P")
                            {
                                setFocusControl(txtPurc_No);
                            }
                            else
                            {
                                setFocusControl(txtQty);
                            }
                            

                        }
                        else
                        {
                            txtScript_Code.Text = string.Empty;
                            lblScriptCode.Text = string.Empty;
                            setFocusControl(txtQty);

                        }
                    }
                }
                else
                {
                    lblScriptCode.Text = "";
                    setFocusControl(txtScript_Code);
                    
                }
                return;
            }
            if (strTextBox == "txtPurc_No")
            {
                if (txtPurc_No.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtPurc_No.Text);
                    if (a == false)
                    {
                        btntxtPurc_No_Click(this, new EventArgs());
                    }
                    else
                    {
                        string str = clsCommon.getString("select Purc_No from qryEquityBalance where Doc_No=" + txtPurc_No.Text + "");

                        if (str != string.Empty)
                        {
                            string purcyr = clsCommon.getString("select Year_Code from qryEquityBalance where Doc_No=" + txtPurc_No.Text + "");
                            string purccm = clsCommon.getString("select  Company_Code from qryEquityBalance where Doc_No=" + txtPurc_No.Text + "");
                           
                            //txtPurc_No.Text =  str;
                            lblPurc_Year_Code.Text = purcyr;
                            lblPurc_Company_code.Text = purccm;                         
                            ViewState["currentTable"] = null;                          
                            setFocusControl(txtQty);
                        }
                        else
                        {

                            txtPurc_No.Text = string.Empty;
                            lblPurc_Year_Code.Text = "";
                            lblPurc_Company_code.Text = "";
                            setFocusControl(txtPurc_No);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtPurc_No);
                }
                return;
            }
            if (strTextBox == "txtQty")
            {
                setFocusControl(txtBonus);
            }
            if (strTextBox == "txtBonus")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(txtValue);
            }
            if (strTextBox == "txtValue")
            {
                setFocusControl(txtProfit_Loss);
            }
            if (strTextBox == "txtProfit_Loss")
            {
                setFocusControl(txtSale_To);
            }
            if (strTextBox == "txtSale_To")
            {
                string SaleTocodetext = txtSale_To.Text.Trim();
                if (SaleTocodetext != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(SaleTocodetext);
                    if (a == false)
                    {
                        btnSale_To_Click(this, new EventArgs());
                    }
                    else
                    {
                        string SaleToName = clsCommon.getString("select Ac_Name_E from nt_1_accountmaster where Ac_Code=" + SaleTocodetext + " and Company_Code= " + Session["Company_Code"].ToString() + "");
                        if (SaleToName != string.Empty && SaleToName != "0")
                        {
                            hdnfSaleId.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + SaleTocodetext + " and Company_Code= " + Session["Company_Code"].ToString() + ""); 
                            lblSaleTo.Text = SaleToName;
                            setFocusControl(txtBrokage);

                        }
                        else
                        {
                            txtSale_To.Text = string.Empty;
                            lblSaleTo.Text = string.Empty;


                        }
                    }
                }
                else
                {
                    lblSaleTo.Text = "";
                    setFocusControl(txtSale_To);
                    
                }
                return;
            }
            if (strTextBox == "txtBrokage")
            {
                setFocusControl(txtCess_Tax);
            }
            if (strTextBox == "txtCess_Tax")
            {
                setFocusControl(txtSTT);
            }
            if (strTextBox == "txtSTT")
            {
                setFocusControl(txtService_Tax);
            }
            if (strTextBox == "txtService_Tax")
            {
                setFocusControl(txtStamp_Charge);
            }
            if (strTextBox == "txtStamp_Charge")
            {
                setFocusControl(txtTurnOver_Tax);
            }
            if (strTextBox == "txtTurnOver_Tax")
            {
                setFocusControl(txtNet_Value);
            }
            if (strTextBox == "txtNet_Value")
            {
                setFocusControl(txtNet_Rate);
            }
            if (strTextBox == "txtNet_Rate")
            {
                setFocusControl(txtNarration);
            }
            if (strTextBox == "txtNarration")
            {
                setFocusControl(btnSave);
            }
            #region Value Calculation 
            double value = 0.00;
            Int32 qty = Convert.ToInt32(txtQty.Text != string.Empty ? txtQty.Text : "0") + 0;
            Int32 bonus = Convert.ToInt32(txtBonus.Text != string.Empty ? txtBonus.Text : "0") + 0;
            double rate = Convert.ToDouble(txtRate.Text != string.Empty ? txtRate.Text : "0.00");

            if( drpTran_Type.SelectedValue == "P")
            {
                value = Math.Round((qty * rate), 2);
                
            }
            else
            {
                value = Math.Round((qty + bonus) * rate,2); 
            }
            
            txtValue.Text = value.ToString();
            #endregion

            #region Net Value Calculation
            double NetValue = 0.00;
            double val = Convert.ToDouble(txtValue.Text != string.Empty ? txtValue.Text : "0.00");
            double brokage = Convert.ToDouble(txtBrokage.Text != string.Empty ? txtBrokage.Text : "0.00");
            double cess = Convert.ToDouble(txtCess_Tax.Text != string.Empty ? txtCess_Tax.Text : "0.00");
            double stt = Convert.ToDouble(txtSTT.Text != string.Empty ? txtSTT.Text : "0.00");
            double service = Convert.ToDouble(txtService_Tax.Text != string.Empty ? txtService_Tax.Text : "0.00");
            double stamp = Convert.ToDouble(txtStamp_Charge.Text != string.Empty ? txtStamp_Charge.Text : "0.00");
            double turnover = Convert.ToDouble(txtTurnOver_Tax.Text != string.Empty ? txtTurnOver_Tax.Text : "0.00");

            if (drpTran_Type.SelectedValue == "P")
            {
                NetValue = val + brokage + cess + stt + service + stamp + turnover;
            }
            else
            {
                NetValue = val - (brokage + cess + stt + service + stamp + turnover);
            }
            txtNet_Value.Text = NetValue.ToString();
            #endregion

            #region Net Rate Calculation 
            double NetRate = 0.00;
            if ((qty + bonus) != 0)
            {
                NetRate = NetValue / (qty+bonus);
            }      
            txtNet_Rate.Text = NetRate.ToString();
            #endregion

        }
        catch
        {
        }
    }
    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Doc_No=" + hdnf.Value + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            return qryDisplay;

            //string qryDisplay = "select * from qryEquityPurchaseSale  where Doc_No =" + hdnf.Value;
            //return qryDisplay;
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
                //this.enableDisableNavigateButtons();
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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        //string v = hdnfClosePopup.Value;
        //if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------

            //--------------------------------------------------
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[0].Style["overflow"] = "hidden";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].Style["overflow"] = "hidden";
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[3].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            //--------------------------------------------------
            e.Row.Cells[4].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[5].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[5].Style["overflow"] = "hidden";
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[6].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[6].Style["overflow"] = "hidden";
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[7].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[7].Style["overflow"] = "hidden";
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[8].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[8].Style["overflow"] = "hidden";
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[9].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[9].Style["overflow"] = "hidden";
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Rowaction].Visible = true;
            e.Row.Cells[Srno].Visible = false;
        }
        catch
        {
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
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                string Doc_No = clsCommon.getString("select Doc_No from " + tblHead + " where Doc_No =" + txtEditDoc_No.Text + " and Company_Code="
            + Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                hdnf.Value = Doc_No;
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
    #region [drpTran_Type_TextChanged]
    protected void drpTran_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpTran_Type.SelectedValue == "S")
        {
            txtPurc_No.Enabled = true;
            
        }
        else
        {
            txtPurc_No.Enabled = false;
           
        }
    }
    #endregion
    #region [txtDate_TextChanged]
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDate.Text;
        strTextBox = "txtDate";
        csCalculations();
    }
    #endregion
    #region [drpLedgerEntry_TextChanged]
    protected void drpLedgerEntry_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
    }
    #endregion
    #region [txtBonus_TextChanged]
    protected void txtBonus_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBonus.Text;
        strTextBox = "txtBonus";
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
    #region [txtProfit_Loss_TextChanged]
    protected void txtProfit_Loss_TextChanged(object sender, EventArgs e)
    {
        searchString = txtProfit_Loss.Text;
        strTextBox = "txtProfit_Loss";
        csCalculations();
    }
    #endregion
    #region [txtSale_To_TextChanged]
    protected void txtSale_To_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSale_To.Text;
        strTextBox = "txtSale_To";
        csCalculations();
    }
    #endregion
    #region [btnSale_To_Click]
    protected void btnSale_To_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSale_To";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtBrokage_TextChanged]
    protected void txtBrokage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBrokage.Text;
        strTextBox = "txtBrokage";
        csCalculations();
    }
    #endregion
    #region [txtCess_Tax_TextChanged]
    protected void txtCess_Tax_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCess_Tax.Text;
        strTextBox = "txtCess_Tax";
        csCalculations();
    }
    #endregion
    #region [txtSTT_TextChanged]
    protected void txtSTT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSTT.Text;
        strTextBox = "txtSTT";
        csCalculations();
    }
    #endregion
    #region [txtService_Tax_TextChanged]
    protected void txtService_Tax_TextChanged(object sender, EventArgs e)
    {
        searchString = txtService_Tax.Text;
        strTextBox = "txtService_Tax";
        csCalculations();
    }
    #endregion
    #region [txtStamp_Charge_TextChanged]
    protected void txtStamp_Charge_TextChanged(object sender, EventArgs e)
    {
        searchString = txtStamp_Charge.Text;
        strTextBox = "txtStamp_Charge";
        csCalculations();
    }
    #endregion
    #region [txtTurnOver_Tax_TextChanged]
    protected void txtTurnOver_Tax_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTurnOver_Tax.Text;
        strTextBox = "txtTurnOver_Tax";
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
    #region [txtNet_Rate_TextChanged]
    protected void txtNet_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Rate.Text;
        strTextBox = "txtNet_Rate";
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
                    foreach (var s in split)
                    {
                        string aa = s.ToString();
                        name += "Doc_No Like '%" + aa + "%'or";
                    }
                    name = name.Remove(name.Length - 2);
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Doc_No";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtDoc_No")
            {
                lblPopupHead.Text = "--Select--";
                string qry = "";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtScript_Code")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( FNO_Script_No like '%" + aa + "%' or Script_Name like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Script Code--";
                string qry = "select FNO_Script_No,Script_Name from Script_Master where FNO_Type = 'E' and (Script_Name like '%" + txtSearchText.Text + "%' or FNO_Script_No like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtPurc_No")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Doc_No like '%" + aa + "%' or Date like '%" + aa + "%' or Qty like '%" + aa + "%' or Sale like '%" + aa + "%' or Balance like '%" + aa + "%'or Bonus like '%" + aa + "%' or SaleBonus like '%" + aa + "%' or BalanceBonus like '%" + aa + "%' or Year_Code like '%" + aa + "%' or Company_Code like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Purc No--";
                string qry = "select Doc_No,Date,Qty,Sale,Balance,Bonus,SaleBonus,BalanceBonus,Year_Code,Company_Code from qryEquityBalance " +
                "where Script_Code = "+txtScript_Code.Text+" and (Balance+BalanceBonus) <>0 and (Company_Code like '%" + txtSearchText.Text + "%' or Year_Code like '%" + txtSearchText.Text + "%' or BalanceBonus like '%" + txtSearchText.Text + "%' or SaleBonus like '%" + txtSearchText.Text + "%' or Bonus like '%" + txtSearchText.Text + "%' or Balance like '%" 
                + txtSearchText.Text + "%' or Sale like '%" + txtSearchText.Text + "%' or Qty like '%" + txtSearchText.Text + "%' or Date like '%" + txtSearchText.Text + "%' or Doc_No like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtSale_To")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Script Code--";
                string qry = "select Ac_Code,Ac_Name_E from nt_1_accountmaster where (Ac_Name_E like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%')";
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
        //child1.SetAttributeValue("EditDoc_No", txtEditDoc_No.Text != string.Empty ? txtEditDoc_No.Text : "0");
        //child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");
        child1.SetAttributeValue("Tran_Type", drpTran_Type.SelectedValue);
        string Date = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        child1.SetAttributeValue("Date", Date);
        child1.SetAttributeValue("LedgerEntry", drpLedgerEntry.SelectedValue);
        child1.SetAttributeValue("Script_Code", txtScript_Code.Text != string.Empty ? txtScript_Code.Text : "0");
        child1.SetAttributeValue("Purc_No", txtPurc_No.Text != string.Empty ? txtPurc_No.Text : "0");
        child1.SetAttributeValue("Purc_Year_Code", lblPurc_Year_Code.Text != string.Empty ? lblPurc_Year_Code.Text : "0");
        child1.SetAttributeValue("Purc_Company_code", lblPurc_Company_code.Text != string.Empty ? lblPurc_Company_code.Text : "0");
        child1.SetAttributeValue("Qty", txtQty.Text != string.Empty ? txtQty.Text : "0.00");
        child1.SetAttributeValue("Bonus", txtBonus.Text != string.Empty ? txtBonus.Text : "0.00");
        child1.SetAttributeValue("Rate", txtRate.Text != string.Empty ? txtRate.Text : "0.00");
        child1.SetAttributeValue("Value", txtValue.Text != string.Empty ? txtValue.Text : "0.00");
        child1.SetAttributeValue("Profit_Loss", txtProfit_Loss.Text != string.Empty ? txtProfit_Loss.Text : "0.00");
        child1.SetAttributeValue("Sale_To", txtSale_To.Text != string.Empty ? txtSale_To.Text : "0");
        child1.SetAttributeValue("Brokage", txtBrokage.Text != string.Empty ? txtBrokage.Text : "0.00");
        child1.SetAttributeValue("Cess_Tax", txtCess_Tax.Text != string.Empty ? txtCess_Tax.Text : "0.00");
        child1.SetAttributeValue("STT", txtSTT.Text != string.Empty ? txtSTT.Text : "0.00");
        child1.SetAttributeValue("Service_Tax", txtService_Tax.Text != string.Empty ? txtService_Tax.Text : "0.00");
        child1.SetAttributeValue("Stamp_Charge", txtStamp_Charge.Text != string.Empty ? txtStamp_Charge.Text : "0.00");
        child1.SetAttributeValue("TurnOver_Tax", txtTurnOver_Tax.Text != string.Empty ? txtTurnOver_Tax.Text : "0.00");
        child1.SetAttributeValue("Net_Value", txtNet_Value.Text != string.Empty ? txtNet_Value.Text : "0.00");
        child1.SetAttributeValue("Net_Rate", txtNet_Rate.Text != string.Empty ? txtNet_Rate.Text : "0.00");
        child1.SetAttributeValue("Narration", txtNarration.Text);
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_Code", Year_Code);
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
        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_EquityPurchaseSale";
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
            flag = 2;
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
        // this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
        #endregion
     
       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:validation();", true);
      
    }
    #endregion
}
