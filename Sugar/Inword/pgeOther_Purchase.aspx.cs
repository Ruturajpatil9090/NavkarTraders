using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

public partial class pgeOther_Purchase : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string GLedgerTable = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
    string cs = string.Empty;
    string Action = string.Empty;
    #endregion

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;

    #region text and label data Declaration
    Int32 Doc_No = 0;
    string billno = "0";
    string Doc_Date = string.Empty;
    Int32 Supplier_Code = 0;
    Int32 Exp_Ac = 0;
    string Narration = string.Empty;
    string ASN_No = string.Empty;
    string einvoiceno = string.Empty;
    double Taxable_Amount = 0.00;
    Int32 GST_RateCode = 0;
    double CGST_Rate = 0.00;
    double CGST_Amount = 0.00;
    double SGST_Rate = 0.00;
    double SGST_Amount = 0.00;
    double IGST_Rate = 0.00;
    double IGST_Amount = 0.00;
    double Other_Amount = 0.00;
    double Bill_Amount = 0.00;
    double TDS_Amt = 0.00;
    double TDS_Per = 0.00;
    double TDS = 0.00;
    Int32 TDS_Cutt_AcCode = 0;
    Int32 TDS_AcCode = 0;

    Int32 opid = 0;
    Int32 suppliercode = 0;
    Int32 expcode = 0;
    Int32 gstratecode = 0;
    Int32 tdscuttingcode = 0;
    Int32 tdsaccountcode = 0;
    int counts = 0;
    int count = 0;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "other_purchase";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = "qryotherpurchase";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {

                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["opid"];

                        //ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        pnlPopup.Style["display"] = "none";
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        setFocusControl(btnEdit);
                        hdnfpopup.Value = "0";
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtSupplier_Code);
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
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                obj.code = "Doc_No";
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
                                    txtDoc_No.Enabled = false;
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

                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtSupplier_Code.Enabled = false;
                lblSupplier_Code.Text = string.Empty;
                btntxtSupplier_Code.Enabled = false;
                txtExp_Ac.Enabled = false;
                lblExp_Ac.Text = string.Empty;
                btntxtExp_Ac.Enabled = false;
                txtNarration.Enabled = false;
                txtTaxable_Amount.Enabled = false;
                txtGST_RateCode.Enabled = false;
                lblGST_RateCode.Text = string.Empty;
                btntxtGST_RateCode.Enabled = false;
                txtCGST_Rate.Enabled = false;
                txtCGST_Amount.Enabled = false;
                txtSGST_Rate.Enabled = false;
                txtSGST_Amount.Enabled = false;
                txtIGST_Rate.Enabled = false;
                txtIGST_Amount.Enabled = false;
                txtOther_Amount.Enabled = false;
                txtBill_Amount.Enabled = false;
                txtGST_RateCode.Text = string.Empty;
                txtTDS_Amt.Enabled = false;
                txtTDS_Per.Enabled = false;
                txtTDS.Enabled = false;
                txtTDSCuttAc_Code.Enabled = false;
                btntxtCuttTDSAc_Code.Enabled = false;
                lblTDSCuttAc_Code.Text = string.Empty;
                txtTDSAc_Code.Enabled = false;
                btntxtTDSAc_Code.Enabled = false;
                lblTDSAc_Code.Text = string.Empty;
                txtasnno.Enabled = false;
                txteinvoice.Enabled = false;
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

                ViewState["currentTable"] = null;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtSupplier_Code.Enabled = true;
                lblSupplier_Code.Text = string.Empty;
                btntxtSupplier_Code.Enabled = true;
                txtExp_Ac.Enabled = true;
                lblExp_Ac.Text = string.Empty;
                btntxtExp_Ac.Enabled = true;
                txtNarration.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtGST_RateCode.Enabled = true;
                lblGST_RateCode.Text = string.Empty;
                btntxtGST_RateCode.Enabled = true;
                txtCGST_Rate.Enabled = true;
                txtCGST_Amount.Enabled = true;
                txtSGST_Rate.Enabled = true;
                txtSGST_Amount.Enabled = true;
                txtIGST_Rate.Enabled = true;
                txtIGST_Amount.Enabled = true;
                txtOther_Amount.Enabled = true;
                txtBill_Amount.Enabled = false;
                txtTDS_Amt.Enabled = true;
                txtTDS_Per.Enabled = true;
                txtTDS.Enabled = false;
                txtTDSCuttAc_Code.Enabled = true;
                btntxtCuttTDSAc_Code.Enabled = true;
                lblTDSCuttAc_Code.Text = string.Empty;
                txtTDSAc_Code.Enabled = true;
                btntxtTDSAc_Code.Enabled = true;
                lblTDSAc_Code.Text = string.Empty;
                txtasnno.Enabled = true;
                txteinvoice.Enabled = true;
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
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtSupplier_Code.Enabled = false;
                lblSupplier_Code.Text = string.Empty;
                btntxtSupplier_Code.Enabled = false;
                txtExp_Ac.Enabled = false;
                lblExp_Ac.Text = string.Empty;
                btntxtExp_Ac.Enabled = false;
                txtNarration.Enabled = false;
                txtTaxable_Amount.Enabled = false;
                txtGST_RateCode.Enabled = false;
                lblGST_RateCode.Text = string.Empty;
                btntxtGST_RateCode.Enabled = false;
                txtCGST_Rate.Enabled = false;
                txtCGST_Amount.Enabled = false;
                txtSGST_Rate.Enabled = false;
                txtSGST_Amount.Enabled = false;
                txtIGST_Rate.Enabled = false;
                txtIGST_Amount.Enabled = false;
                txtOther_Amount.Enabled = false;
                txtBill_Amount.Enabled = false;
                txtTDS_Amt.Enabled = false;
                txtTDS_Per.Enabled = false;
                txtTDS.Enabled = false;
                txtTDSCuttAc_Code.Enabled = false;
                btntxtCuttTDSAc_Code.Enabled = false;
                lblTDSCuttAc_Code.Text = string.Empty;
                txtTDSAc_Code.Enabled = false;
                btntxtTDSAc_Code.Enabled = false;
                lblTDSAc_Code.Text = string.Empty;
                txtasnno.Enabled = false;
                txteinvoice.Enabled = false;
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
                txtSupplier_Code.Enabled = true;
                btntxtSupplier_Code.Enabled = true;
                txtExp_Ac.Enabled = true;
                btntxtExp_Ac.Enabled = true;
                txtNarration.Enabled = true;
                txtTaxable_Amount.Enabled = true;
                txtGST_RateCode.Enabled = true;
                btntxtGST_RateCode.Enabled = true;
                txtCGST_Rate.Enabled = true;
                txtCGST_Amount.Enabled = true;
                txtSGST_Rate.Enabled = true;
                txtSGST_Amount.Enabled = true;
                txtIGST_Rate.Enabled = true;
                txtIGST_Amount.Enabled = true;
                txtOther_Amount.Enabled = true;
                txtBill_Amount.Enabled = true;
                txtTDS_Amt.Enabled = true;
                txtTDS_Per.Enabled = true;
                txtTDS.Enabled = true;
                txtTDSCuttAc_Code.Enabled = true;
                btntxtCuttTDSAc_Code.Enabled = true;
                txtTDSAc_Code.Enabled = true;
                btntxtTDSAc_Code.Enabled = true;
                txtasnno.Enabled = true;
                txteinvoice.Enabled = true;
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

    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtDoc_No.Text = hdnf.Value;
                hdnfSuffix.Value = "";
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                //  this.enableDisableNavigateButtons();
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

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.getMaxCode();
        this.NextNumber();
        setFocusControl(txtSupplier_Code);

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
        setFocusControl(txtSupplier_Code);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where Doc_No=" + txtDoc_No.Text + "");
                if (str == string.Empty)   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtDoc_No.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where Doc_No='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' ";
                    Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                    thred.Start(); //Thread Operation Start
                    thred.Join();


                    string qry1 = "delete from nt_1_gledger where Doc_No='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                       "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and TRAN_TYPE='XP'";
                    Thread thred1 = new Thread(() => { count = DataStore(qry1, flag); }); //Calling DataStore Method Using Thread
                    thred1.Start(); //Thread Operation Start
                    thred1.Join();

                    if (count == 3)
                    {
                        Response.Redirect("../Inword/pgeOther_Purchase.aspx");
                    }


                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                hdnf.Value = txtDoc_No.Text;
                showLastRecord();
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

        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(opid),0) as opid from nt_1_other_purchase "));

        hdnf.Value = Convert.ToString(maxno);
        clsButtonNavigation.enableDisable("S");
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
                        txtbillno.Text = dt.Rows[0]["billno"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        lblDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_Date.Text = DateTime.Parse(dt.Rows[0]["Doc_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        txtSupplier_Code.Text = dt.Rows[0]["Supplier_Code"].ToString();
                        lblSupplier_Code.Text = dt.Rows[0]["suppilername"].ToString();
                        txtExp_Ac.Text = dt.Rows[0]["Exp_Ac"].ToString();
                        lblExp_Ac.Text = dt.Rows[0]["expname"].ToString();
                        txtGST_RateCode.Text = dt.Rows[0]["GST_RateCode"].ToString();
                        lblGST_RateCode.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtTaxable_Amount.Text = dt.Rows[0]["Taxable_Amount"].ToString();
                        txtCGST_Rate.Text = dt.Rows[0]["CGST_Rate"].ToString();
                        txtCGST_Amount.Text = dt.Rows[0]["CGST_Amount"].ToString();
                        txtSGST_Rate.Text = dt.Rows[0]["SGST_Rate"].ToString();
                        txtSGST_Amount.Text = dt.Rows[0]["SGST_Amount"].ToString();
                        txtIGST_Rate.Text = dt.Rows[0]["IGST_Rate"].ToString();
                        txtIGST_Amount.Text = dt.Rows[0]["IGST_Amount"].ToString();
                        txtOther_Amount.Text = dt.Rows[0]["Other_Amount"].ToString();
                        txtBill_Amount.Text = dt.Rows[0]["Bill_Amount"].ToString();
                        txtTDS_Amt.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        txtTDS_Per.Text = dt.Rows[0]["TDS_Per"].ToString();
                        txtTDS.Text = dt.Rows[0]["TDS"].ToString();
                        txtTDSCuttAc_Code.Text = dt.Rows[0]["TDS_Cutt_AcCode"].ToString();
                        lblTDSCuttAc_Code.Text = dt.Rows[0]["tdscutacname"].ToString();
                        txtTDSAc_Code.Text = dt.Rows[0]["TDS_AcCode"].ToString();
                        lblTDSAc_Code.Text = dt.Rows[0]["tdsacname"].ToString();
                        txtasnno.Text = dt.Rows[0]["ASN_No"].ToString();
                        txteinvoice.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                        hdnf.Value = txtDoc_No.Text;
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
                        recordExist = true;
                        lblMsg.Text = "";

                    }
                }
            }
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType != DataControlRowType.Pager)
            {

                e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("200px");
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

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Doc_No=" + hdnf.Value;
            string qryDisplay = "select * from " + qryCommon + "  where opid =" + hdnf.Value;
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {

        try
        {
            if (btntxtDoc_No.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtDoc_No.Text = string.Empty;
                txtDoc_No.Enabled = true;


                btnSave.Enabled = false;
                setFocusControl(txtDoc_No);
            }

            if (btntxtDoc_No.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtDoc_No";
                btnSearch_Click(sender, e);
                //txtDoc_No.Enabled = true;

            }
        }
        catch
        {
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

            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Doc_No like '%" + aa + "%' or Doc_Date like '%" + aa + "%' or suppilername like '%" + aa + "%' ) and";

                }
                name = name.Remove(name.Length - 3);

                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_Name_R from NT_1_AccountMaster where ( " + name + ") order by Ac_Name_E";
                string qry = " select Doc_No,Doc_DateConverted as Doc_Date,suppilername FROM qryotherpurchase where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + " and " + name + " order by Doc_No";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtSupplier_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Ac_Name_R like '%" + aa + "%' ) and";

                }
                name = name.Remove(name.Length - 3);

                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_Name_R from NT_1_AccountMaster where ( " + name + ") order by Ac_Name_E";
                //string qry = " select Ac_Code,Ac_Name_E,Ac_Name_R from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Ac_Code";

                //this.showPopup(qry);

                string qry = "   dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                 + " and (" + name + ") order by Ac_Code ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtExp_Ac")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Ac_Name_R like '%" + aa + "%' ) and";

                }
                name = name.Remove(name.Length - 3);

                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_Name_R from NT_1_AccountMaster where ( " + name + ") order by Ac_Name_R";
                //string qry = " select Ac_Code,Ac_Name_E,Ac_Name_R from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and  (" + name + ") order by Ac_Code";
                //this.showPopup(qry);

                string qry = "   dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (" + name + ") order by Ac_Code ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtGST_RateCode")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += " Doc_no like '%" + aa + "%'  or GST_Name like '%" + aa + "%'  or Rate like '%" + aa + "%'  or";

                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where  ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name"; ;
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtTDSCuttAc_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Ac_Name_R like '%" + aa + "%' ) and";

                }
                name = name.Remove(name.Length - 3);

                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_Name_R from NT_1_AccountMaster where ( " + name + ") order by Ac_Name_E";
                //string qry = " select Ac_Code,Ac_Name_E,Ac_Name_R from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Ac_Code";
                string qry = "   dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (" + name + ") order by Ac_Code ";

                this.showPopupAccountMaster(qry);
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtTDSAc_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Ac_Name_R like '%" + aa + "%' ) and";

                }
                name = name.Remove(name.Length - 3);

                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_Name_R from NT_1_AccountMaster where ( " + name + ") order by Ac_Name_E";
                //string qry = " select Ac_Code,Ac_Name_E,Ac_Name_R from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Ac_Code";

                //this.showPopup(qry);

                string qry = "   dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
               + " and (" + name + ") order by Ac_Code ";
                this.showPopupAccountMaster(qry);
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

    #region [Account Master Popup Button Code]
    protected void showPopupAccountMaster(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.myaccountmaster(qry);

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
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
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

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            hdnfpopup.Value = null;

            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtSupplier_Code")
            {
                if (txtSupplier_Code.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtSupplier_Code.Text + "");
                    if (str != string.Empty && str != "0")
                    {
                        //hdnfpopup.Value = null;
                        lblSupplier_Code.Text = str;
                        txtTDSCuttAc_Code.Text = txtSupplier_Code.Text;
                        lblTDSCuttAc_Code.Text = lblSupplier_Code.Text;


                        setFocusControl(txtExp_Ac);
                    }
                    else
                    {
                        lblSupplier_Code.Text = string.Empty;
                        txtSupplier_Code.Text = string.Empty;
                        setFocusControl(txtExp_Ac);
                    }
                }
                else
                {
                    lblSupplier_Code.Text = string.Empty;
                    txtSupplier_Code.Text = string.Empty;
                    setFocusControl(txtExp_Ac);
                }
            }

            if (strTextBox == "txtExp_Ac")
            {
                if (txtExp_Ac.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtExp_Ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblExp_Ac.Text = str;
                        setFocusControl(txtGST_RateCode);
                    }
                    else
                    {
                        lblExp_Ac.Text = string.Empty;
                        txtExp_Ac.Text = string.Empty;
                        setFocusControl(txtGST_RateCode);
                    }
                }
                else
                {
                    lblExp_Ac.Text = string.Empty;
                    txtExp_Ac.Text = string.Empty;
                    setFocusControl(txtGST_RateCode);
                }
            }
            if (strTextBox == "txtGST_RateCode")
            {
                if (txtGST_RateCode.Text != string.Empty)
                {
                    string str = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGST_RateCode.Text + "  ");
                    if (str != string.Empty && str != "0")
                    {
                        lblGST_RateCode.Text = str;
                        setFocusControl(txtTaxable_Amount);
                    }
                    else
                    {
                        lblGST_RateCode.Text = string.Empty;
                        txtGST_RateCode.Text = string.Empty;
                        setFocusControl(txtTaxable_Amount);
                    }
                }
                else
                {
                    lblGST_RateCode.Text = string.Empty;
                    txtGST_RateCode.Text = string.Empty;
                    setFocusControl(txtTaxable_Amount);
                }
            }

            if (strTextBox == "txtTaxable_Amount")
            {
                double txableamt = Convert.ToDouble(txtTaxable_Amount.Text);
                txtTDS_Amt.Text = txableamt.ToString();
                setFocusControl(txtCGST_Rate);
            }

            if (strTextBox == "txtCGST_Rate")
            {
                //this.CSTCalculation();
                setFocusControl(txtSGST_Rate);
            }

            if (strTextBox == "txtSGST_Rate")
            {
                //this.LSGSTCalculation();
                setFocusControl(txtIGST_Rate);
            }

            if (strTextBox == "txtIGST_Rate")
            {
                //this.LIGSTCalculation();
                setFocusControl(txtOther_Amount);
            }

            if (strTextBox == "txtOther_Amount")
            {
                double txtBILL_AMOUNT_Convert = txtBill_Amount.Text != string.Empty ? Convert.ToDouble(txtBill_Amount.Text) : 0.00;
                double txtOTHER_AMT_Convert = txtOther_Amount.Text != string.Empty ? Convert.ToDouble(txtOther_Amount.Text) : 0.00;

                txtBILL_AMOUNT_Convert = (txtBILL_AMOUNT_Convert) + (txtOTHER_AMT_Convert);
                txtBill_Amount.Text = Convert.ToString(txtBILL_AMOUNT_Convert);
                setFocusControl(txtTDS_Amt);
            }

            if (strTextBox == "txtTDS_Amt")
            {
                double txtTDS_Amt_Convert = txtTDS_Amt.Text != string.Empty ? Convert.ToDouble(txtTDS_Amt.Text) : 0.00;
                double txtTDS_Per_Convert = txtTDS_Per.Text != string.Empty ? Convert.ToDouble(txtTDS_Per.Text) : 0.00;

                txtTDS_Amt_Convert = (txtTDS_Amt_Convert * txtTDS_Per_Convert) / 100;
                txtTDS_Amt.Text = Convert.ToString(txtTDS_Amt_Convert);

                //double TDS_Final_Amount = 0.00;
                //double TDS = 0.00;
                //double tds_amnt = txtTDS_Amt.Text != string.Empty ? Convert.ToDouble(txtTDS_Amt.Text) : 0;
                //double TDS_perc = txtTDS_Per.Text != string.Empty ? Convert.ToDouble(txtTDS_Per.Text) : 0;
                //double tds = txtTDS.Text != string.Empty ? Convert.ToDouble(txtTDS.Text) : 0;

                //TDS_Final_Amount = (tds_amnt * TDS_perc / 100);
                //TDS = (TDS_Final_Amount * TDS_perc / 100);

                setFocusControl(txtTDS_Per);
            }
            if (strTextBox == "txtTDS_Per")
            {
                setFocusControl(txtTDSCuttAc_Code);
            }
            //if (strTextBox == "txtTDS")
            //{                             
            //    setFocusControl(txtTDSCuttAc_Code);
            //}
            if (strTextBox == "txtTDSCuttAc_Code")
            {
                if (txtTDSCuttAc_Code.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtTDSCuttAc_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        lblTDSCuttAc_Code.Text = str;
                        setFocusControl(txtTDSAc_Code);
                    }
                    else
                    {
                        lblTDSCuttAc_Code.Text = string.Empty;
                        txtTDSCuttAc_Code.Text = string.Empty;
                        setFocusControl(txtTDSAc_Code);
                    }
                }
                else
                {
                    lblTDSCuttAc_Code.Text = string.Empty;
                    txtTDSCuttAc_Code.Text = string.Empty;
                    setFocusControl(txtTDSAc_Code);
                }
            }
            if (strTextBox == "txtTDSAc_Code")
            {
                if (txtTDSAc_Code.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtTDSAc_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        lblTDSAc_Code.Text = str;
                        if (txtExp_Ac.Text != string.Empty)
                        {
                            setFocusControl(txtbillno);
                        }
                        else
                        {
                            setFocusControl(txtExp_Ac);
                        }
                    }
                    else
                    {
                        lblTDSAc_Code.Text = string.Empty;
                        txtTDSAc_Code.Text = string.Empty;
                        setFocusControl(txtbillno);
                    }
                }
                else
                {
                    lblTDSAc_Code.Text = string.Empty;
                    txtTDSAc_Code.Text = string.Empty;
                    setFocusControl(txtbillno);
                }
            }
            if (strTextBox == "txtasnno")
            {
                setFocusControl(txteinvoice);
            }
            if (strTextBox == "txteinvoice")
            {
                setFocusControl(txtNarration);
            }


            #region calculation part

            double other = txtOther_Amount.Text != string.Empty ? Convert.ToDouble(txtOther_Amount.Text) : 0.0;
            double billAmt = 0.00;

            double CGST = Convert.ToDouble("0" + txtCGST_Amount.Text);
            double IGST = Convert.ToDouble("0" + txtIGST_Amount.Text);
            double SGST = Convert.ToDouble("0" + txtSGST_Amount.Text);

            string aaa = "";
            if (txtDoc_No.Text.Trim() != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtDoc_No.Text);
                if (a == true)
                {
                    aaa = clsCommon.getString("select isnull(GSTStateCode,0) from " + AccountMasterTable + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtSupplier_Code.Text + "");
                }
            }
            int suppliergstStateCode = 0;
            if (aaa.Trim().ToString() != "")
            {
                suppliergstStateCode = Convert.ToInt32(aaa);
            }

            string qa = Session["CompanyGSTStateCode"].ToString();
            if (Session["CompanyGSTStateCode"] == null || Session["CompanyGSTStateCode"] == string.Empty)
            {
                Session["CompanyGSTStateCode"] = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                    "  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            }
            int companyGstStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
            string GSTRateCode = "0";
            if (txtGST_RateCode.Text != string.Empty)
            {
                GSTRateCode = txtGST_RateCode.Text;
            }
            double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));

            double CGSTAmountForSA = 0.0;
            double SGSTAmountForSA = 0.0;
            double IGSTAmountForSA = 0.0;

            double CGSTRateForSA = 0.00;
            double SGSTRateForSA = 0.00;
            double IGSTRateForSA = 0.00;
            double taxamt = Convert.ToDouble(txtTaxable_Amount.Text != string.Empty ? Convert.ToDouble(txtTaxable_Amount.Text) : 0.00);
            if (companyGstStateCode == suppliergstStateCode)
            {
                CGSTRateForSA = cgstrate;

                double cgsttaxAmountOnMR = Math.Round((taxamt * cgstrate / 100), 2);
                //double cgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + cgsttaxAmountOnMR) * mill_rate)), 2);
                //double cgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - cgstExMillRate), 2);
                CGSTAmountForSA = Math.Round(cgsttaxAmountOnMR, 2);

                SGSTRateForSA = sgstrate;
                double sgsttaxAmountOnMR = Math.Round((taxamt * sgstrate / 100), 2);
                //double sgstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + sgsttaxAmountOnMR) * mill_rate)), 2);
                //double sgstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - sgstExMillRate), 2);
                SGSTAmountForSA = Math.Round(sgsttaxAmountOnMR, 2);
            }
            else
            {
                IGSTRateForSA = igstrate;
                double igsttaxAmountOnMR = ((taxamt) * igstrate / 100);
                //double igstExMillRate = Math.Round(Math.Abs((mill_rate / (mill_rate + igsttaxAmountOnMR) * mill_rate)), 2);
                //double igstRateAmountOnMR = Math.Round(Math.Abs(mill_rate - igstExMillRate), 2);
                IGSTAmountForSA = Math.Round(igsttaxAmountOnMR, 2);
            }
            txtCGST_Rate.Text = CGSTRateForSA.ToString();
            txtCGST_Amount.Text = CGSTAmountForSA.ToString();
            txtSGST_Rate.Text = SGSTRateForSA.ToString();
            txtSGST_Amount.Text = SGSTAmountForSA.ToString();
            txtIGST_Rate.Text = IGSTRateForSA.ToString();
            txtIGST_Amount.Text = IGSTAmountForSA.ToString();
            #endregion

            #region
            double taxableAmount = txtTaxable_Amount.Text != string.Empty ? Convert.ToDouble(txtTaxable_Amount.Text) : 0.0;
            double billAmountForCalculation = taxableAmount + CGSTAmountForSA + SGSTAmountForSA + IGSTAmountForSA;
            billAmountForCalculation = billAmountForCalculation + other;
            billAmt = Math.Round(billAmountForCalculation, 2);
            txtBill_Amount.Text = billAmt.ToString();
            #endregion

            #region
            // double TDSAmt = 0.00;
            //// double Other_Amount = txtOther_Amount.Text != string.Empty ? Convert.ToDouble(txtTDS_Per.Text) : 0.0;
            // double TDS_perc = txtTDS_Per.Text != string.Empty ? Convert.ToDouble(txtTDS_Per.Text) : 0.0;
            // double tds_amnt = txtTDS_Amt.Text != string.Empty ? Convert.ToDouble(txtTDS_Amt.Text) : 0;
            // double tdsAmountCalculation = (tds_amnt * TDS_perc / 100);
            // //tdsAmountCalculation = tdsAmountCalculation * TDS_perc / 100;
            // TDSAmt = Math.Round(tdsAmountCalculation, 2);
            // //txtTDS_Amt.Text = TDSAmt.ToString();




            double TDS_Amt_Convert = txtTDS_Amt.Text != string.Empty ? Convert.ToDouble(txtTDS_Amt.Text) : 0.00;
            double TDS_Per_Convert = txtTDS_Per.Text != string.Empty ? Convert.ToDouble(txtTDS_Per.Text) : 0.00;

            TDS_Amt_Convert = (TDS_Amt_Convert * TDS_Per_Convert) / 100;
            txtTDS.Text = Convert.ToString(TDS_Amt_Convert);
            #endregion

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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Enter Only Numbers!')", true);
            }
            else
            {
                int do_id = Convert.ToInt32(clsCommon.getString("select opid from " + tblHead + " where doc_no=" + txtEditDoc_No.Text +
                    " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                    " " + Session["year"].ToString() + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:OPPen('" + do_id + "')", true);

            }
        }
        catch (Exception)
        {
            throw;
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
    #region [txtSupplier_Code_TextChanged]
    protected void txtSupplier_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSupplier_Code.Text;
        strTextBox = "txtSupplier_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtSupplier_Code_Click]
    protected void btntxtSupplier_Code_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hdnfpopup.Value == "0")
            //{

            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSupplier_Code";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
    }
    #endregion
    #region [txtExp_Ac_TextChanged]
    protected void txtExp_Ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtExp_Ac.Text;
        strTextBox = "txtExp_Ac";
        csCalculations();
    }
    #endregion
    #region [btntxtExp_Ac_Click]
    protected void btntxtExp_Ac_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hdnfpopup.Value == "0")
            //{

            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtExp_Ac";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
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

    #region [txtbillno_TextChanged]
    protected void txtbillno_TextChanged(object sender, EventArgs e)
    {
        searchString = txtbillno.Text;
        strTextBox = "txtbillno";
        csCalculations();
    }
    #endregion
    #region [txtGST_RateCode_TextChanged]
    protected void txtGST_RateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_RateCode.Text;
        strTextBox = "txtGST_RateCode";
        csCalculations();
    }
    #endregion
    #region [btntxtGST_RateCode_Click]
    protected void btntxtGST_RateCode_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hdnfpopup.Value == "0")
            //{
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGST_RateCode";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
    }
    #endregion
    #region [txtCGST_Rate_TextChanged]
    protected void txtCGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST_Rate.Text;
        strTextBox = "txtCGST_Rate";
        csCalculations();
    }
    #endregion
    #region [txtCGST_Amount_TextChanged]
    protected void txtCGST_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST_Amount.Text;
        strTextBox = "txtCGST_Amount";
        csCalculations();
    }
    #endregion
    #region [txtSGST_Rate_TextChanged]
    protected void txtSGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST_Rate.Text;
        strTextBox = "txtSGST_Rate";
        csCalculations();
    }
    #endregion
    #region [txtSGST_Amount_TextChanged]
    protected void txtSGST_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST_Amount.Text;
        strTextBox = "txtSGST_Amount";
        csCalculations();
    }
    #endregion
    #region [txtIGST_Rate_TextChanged]
    protected void txtIGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST_Rate.Text;
        strTextBox = "txtIGST_Rate";
        csCalculations();
    }
    #endregion
    #region [txtIGST_Amount_TextChanged]
    protected void txtIGST_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST_Amount.Text;
        strTextBox = "txtIGST_Amount";
        csCalculations();
    }
    #endregion
    #region [txtOther_Amount_TextChanged]
    protected void txtOther_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOther_Amount.Text;
        strTextBox = "txtOther_Amount";
        csCalculations();
    }
    #endregion
    #region [txtBill_Amount_TextChanged]
    protected void txtBill_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_Amount.Text;
        strTextBox = "txtBill_Amount";
        csCalculations();
    }
    #endregion


    #region [txtTDS_Amt_TextChanged]
    protected void txtTDS_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS_Amt.Text;
        strTextBox = "txtTDS_Amt";
        csCalculations();
    }
    #endregion
    #region [txtTDS_Per_TextChanged]
    protected void txtTDS_Per_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS_Per.Text;
        strTextBox = "txtTDS_Per";
        csCalculations();
    }
    #endregion
    #region [txtTDS_TextChanged]
    protected void txtTDS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS.Text;
        strTextBox = "txtTDS";
        csCalculations();
    }
    #endregion

    #region [txtTDSCuttAc_Code_TextChanged]
    protected void txtTDSCuttAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSCuttAc_Code.Text;
        strTextBox = "txtTDSCuttAc_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtTDSAc_Code]
    protected void btntxtCuttTDSAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hdnfpopup.Value == "0")
            //{
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTDSCuttAc_Code";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
    }
    #endregion


    #region [txtTDSAc_Code_TextChanged]
    protected void txtTDSAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAc_Code.Text;
        strTextBox = "txtTDSAc_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtTDSAc_Code]
    protected void btntxtTDSAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hdnfpopup.Value == "0")
            //{
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTDSAc_Code";
            btnSearch_Click(sender, e);
            //}
            if (txtTDSAc_Code.Text != string.Empty)
            {
                setFocusControl(txtExp_Ac);

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
            string qry = "";
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
            //bool isvalidate = true;

            //#region validation
            //if (txtTDSAc_Code.Text != string.Empty)
            //{
            //    isvalidate = true;
            //}
            //else
            //{
            //    txtTDSAc_Code.Text = string.Empty;
            //    setFocusControl(txtExp_Ac);
            //    return;
            //}
            //#endregion

            #region -Head part declearation

            Doc_No = txtDoc_No.Text != string.Empty ? Convert.ToInt32(txtDoc_No.Text) : 0;
            billno = txtbillno.Text != string.Empty ? Convert.ToString(txtbillno.Text) : "0";
            DateTime Doc_Date1 = Convert.ToDateTime(txtDoc_Date.Text);
            //Doc_Date = Doc_Date1.ToString("yyyy/MM/dd");

           // Doc_Date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string DOC_DATE = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            Supplier_Code = txtSupplier_Code.Text != string.Empty ? Convert.ToInt32(txtSupplier_Code.Text) : 0;
            try
            {
                suppliercode = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Supplier_Code + " and company_code='" + Session["Company_Code"].ToString() + "'"));
            }
            catch
            {

            }
            Exp_Ac = txtExp_Ac.Text != string.Empty ? Convert.ToInt32(txtExp_Ac.Text) : 0;
            try
            {
                expcode = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Exp_Ac + " and company_code='" + Session["Company_Code"].ToString() + "'"));
            }
            catch
            {

            }
            GST_RateCode = txtGST_RateCode.Text != string.Empty ? Convert.ToInt32(txtGST_RateCode.Text) : 0;
            try
            {
                gstratecode = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + GST_RateCode + " and company_code='" + Session["Company_Code"].ToString() + "'"));
            }
            catch
            {

            }
            Taxable_Amount = txtTaxable_Amount.Text != string.Empty ? Convert.ToDouble(txtTaxable_Amount.Text) : 0.00;
            CGST_Rate = txtCGST_Rate.Text != string.Empty ? Convert.ToDouble(txtCGST_Rate.Text) : 0.00;
            CGST_Amount = txtCGST_Amount.Text != string.Empty ? Convert.ToDouble(txtCGST_Amount.Text) : 0.00;
            SGST_Rate = txtSGST_Rate.Text != string.Empty ? Convert.ToDouble(txtSGST_Rate.Text) : 0.00;
            SGST_Amount = txtSGST_Amount.Text != string.Empty ? Convert.ToDouble(txtSGST_Amount.Text) : 0.00;
            IGST_Rate = txtIGST_Rate.Text != string.Empty ? Convert.ToDouble(txtIGST_Rate.Text) : 0.00;
            IGST_Amount = txtIGST_Amount.Text != string.Empty ? Convert.ToDouble(txtIGST_Amount.Text) : 0.00;
            Other_Amount = txtOther_Amount.Text != string.Empty ? Convert.ToDouble(txtOther_Amount.Text) : 0.00;
            Bill_Amount = txtBill_Amount.Text != string.Empty ? Convert.ToDouble(txtBill_Amount.Text) : 0.00;
            TDS_Amt = txtTDS_Amt.Text != string.Empty ? Convert.ToDouble(txtTDS_Amt.Text) : 0.00;
            TDS_Per = txtTDS_Per.Text != string.Empty ? Convert.ToDouble(txtTDS_Per.Text) : 0.00;
            TDS = txtTDS.Text != string.Empty ? Convert.ToDouble(txtTDS.Text) : 0.00;
            TDS_Cutt_AcCode = txtTDSCuttAc_Code.Text != string.Empty ? Convert.ToInt32(txtTDSCuttAc_Code.Text) : 0;
            try
            {
                tdscuttingcode = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + TDS_Cutt_AcCode + " and company_code='" + Session["Company_Code"].ToString() + "'"));
            }
            catch
            {

            }
            TDS_AcCode = txtTDSAc_Code.Text != string.Empty ? Convert.ToInt32(txtTDSAc_Code.Text) : 0;
            try
            {
                tdsaccountcode = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + TDS_AcCode + " and company_code='" + Session["Company_Code"].ToString() + "'"));
            }
            catch
            {

            }
            Narration = txtNarration.Text;
            ASN_No = txtasnno.Text;
            einvoiceno = txteinvoice.Text;

            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            int year_Code = Convert.ToInt32(Session["year"].ToString());
            int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            string Head_Update = string.Empty;
            int flag = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion-Save Part

            if (btnSave.Text == "Save")
            {
                this.NextNumber();

                Head_Fields = "Doc_No,";
                Head_Values = "'" + Doc_No + "',";
                Head_Fields = Head_Fields + "Doc_Date,";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                Head_Fields = Head_Fields + "Supplier_Code,";
                Head_Values = Head_Values + "'" + Supplier_Code + "',";
                Head_Fields = Head_Fields + "Exp_Ac,";
                Head_Values = Head_Values + "'" + Exp_Ac + "',";
                Head_Fields = Head_Fields + "Narration,";
                Head_Values = Head_Values + "'" + Narration + "',";
                Head_Fields = Head_Fields + "Taxable_Amount,";
                Head_Values = Head_Values + "'" + Taxable_Amount + "',";
                Head_Fields = Head_Fields + "GST_RateCode,";
                Head_Values = Head_Values + "'" + GST_RateCode + "',";
                Head_Fields = Head_Fields + "CGST_Rate,";
                Head_Values = Head_Values + "'" + CGST_Rate + "',";
                Head_Fields = Head_Fields + "CGST_Amount,";
                Head_Values = Head_Values + "'" + CGST_Amount + "',";
                Head_Fields = Head_Fields + "SGST_Rate,";
                Head_Values = Head_Values + "'" + SGST_Rate + "',";
                Head_Fields = Head_Fields + "SGST_Amount,";
                Head_Values = Head_Values + "'" + SGST_Amount + "',";
                Head_Fields = Head_Fields + "IGST_Rate,";
                Head_Values = Head_Values + "'" + IGST_Rate + "',";
                Head_Fields = Head_Fields + "IGST_Amount,";
                Head_Values = Head_Values + "'" + IGST_Amount + "',";
                Head_Fields = Head_Fields + "Other_Amount,";
                Head_Values = Head_Values + "'" + Other_Amount + "',";
                Head_Fields = Head_Fields + "Bill_Amount,";
                Head_Values = Head_Values + "'" + Bill_Amount + "',";
                Head_Fields = Head_Fields + "Company_Code,";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                Head_Fields = Head_Fields + "Year_Code,";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                Head_Fields = Head_Fields + "TDS_Amt,";
                Head_Values = Head_Values + "'" + TDS_Amt + "',";
                Head_Fields = Head_Fields + "TDS_Per,";
                Head_Values = Head_Values + "'" + TDS_Per + "',";
                Head_Fields = Head_Fields + "TDS,";
                Head_Values = Head_Values + "'" + TDS + "',";
                Head_Fields = Head_Fields + "TDS_Cutt_AcCode,";
                Head_Values = Head_Values + "'" + TDS_Cutt_AcCode + "',";
                Head_Fields = Head_Fields + "TDS_AcCode,";
                Head_Values = Head_Values + "'" + TDS_AcCode + "',";
                Head_Fields = Head_Fields + "opid,";
                Head_Values = Head_Values + "'" + opid + "',";
                Head_Fields = Head_Fields + "sc,";
                Head_Values = Head_Values + "'" + suppliercode + "',";
                Head_Fields = Head_Fields + "ea,";
                Head_Values = Head_Values + "'" + expcode + "',";
                Head_Fields = Head_Fields + "tca,";
                Head_Values = Head_Values + "'" + tdscuttingcode + "',";
                Head_Fields = Head_Fields + "tac,";
                Head_Values = Head_Values + "'" + tdsaccountcode + "',";
                Head_Fields = Head_Fields + "billno,";
                Head_Values = Head_Values + "'" + billno + "',";
                Head_Fields = Head_Fields + "Created_By,";
                Head_Values = Head_Values + "'" + Created_By + "',";
                Head_Fields = Head_Fields + "ASN_No,";
                Head_Values = Head_Values + "'" + ASN_No + "',";
                Head_Fields = Head_Fields + "einvoiceno";
                Head_Values = Head_Values + "'" + einvoiceno + "'";

                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();


            }
            else
            {
                #region Create Update Query
                Head_Update = Head_Update + "Doc_Date =";
                Head_Update = Head_Update + "'" + DOC_DATE + "',";
                Head_Update = Head_Update + "Supplier_Code =";
                Head_Update = Head_Update + "'" + Supplier_Code + "',";
                Head_Update = Head_Update + "Exp_Ac =";
                Head_Update = Head_Update + "'" + Exp_Ac + "',";
                Head_Update = Head_Update + "GST_RateCode=";
                Head_Update = Head_Update + "'" + GST_RateCode + "',";
                Head_Update = Head_Update + "Taxable_Amount=";
                Head_Update = Head_Update + "'" + Taxable_Amount + "',";
                Head_Update = Head_Update + "CGST_Rate=";
                Head_Update = Head_Update + "'" + CGST_Rate + "',";
                Head_Update = Head_Update + "CGST_Amount=";
                Head_Update = Head_Update + "'" + CGST_Amount + "',";
                Head_Update = Head_Update + "SGST_Rate=";
                Head_Update = Head_Update + "'" + SGST_Rate + "',";
                Head_Update = Head_Update + "SGST_Amount=";
                Head_Update = Head_Update + "'" + SGST_Amount + "',";
                Head_Update = Head_Update + "IGST_Rate=";
                Head_Update = Head_Update + "'" + IGST_Rate + "',";
                Head_Update = Head_Update + "IGST_Amount=";
                Head_Update = Head_Update + "'" + IGST_Amount + "',";
                Head_Update = Head_Update + "Other_Amount=";
                Head_Update = Head_Update + "'" + Other_Amount + "',";
                Head_Update = Head_Update + "Bill_Amount=";
                Head_Update = Head_Update + "'" + Bill_Amount + "',";
                Head_Update = Head_Update + "Company_Code=";
                Head_Update = Head_Update + "'" + Company_Code + "',";
                //Head_Update = Head_Update + "Year_Code=";
                //Head_Update = Head_Update + "'" + Year_Code + "',";
                Head_Update = Head_Update + "TDS_Amt=";
                Head_Update = Head_Update + "'" + TDS_Amt + "',";
                Head_Update = Head_Update + "TDS_Per=";
                Head_Update = Head_Update + "'" + TDS_Per + "',";
                Head_Update = Head_Update + "TDS=";
                Head_Update = Head_Update + "'" + TDS + "',";
                Head_Update = Head_Update + "TDS_Cutt_AcCode=";
                Head_Update = Head_Update + "'" + TDS_Cutt_AcCode + "',";
                Head_Update = Head_Update + "TDS_AcCode=";
                Head_Update = Head_Update + "'" + TDS_AcCode + "',";
                Head_Update = Head_Update + "Narration=";
                Head_Update = Head_Update + "'" + Narration + "',";
                Head_Update = Head_Update + "sc=";
                Head_Update = Head_Update + "'" + suppliercode + "',";
                Head_Update = Head_Update + "ea=";
                Head_Update = Head_Update + "'" + expcode + "',";
                Head_Update = Head_Update + "tca=";
                Head_Update = Head_Update + "'" + tdscuttingcode + "',";
                Head_Update = Head_Update + "tac=";
                Head_Update = Head_Update + "'" + tdsaccountcode + "',";
                Head_Update = Head_Update + "billno=";
                Head_Update = Head_Update + "'" + billno + "',";
                Head_Update = Head_Update + "Modified_By=";
                Head_Update = Head_Update + "'" + Modified_By + "',";
                Head_Update = Head_Update + "ASN_No=";
                Head_Update = Head_Update + "'" + ASN_No + "',";
                Head_Update = Head_Update + "einvoiceno=";
                Head_Update = Head_Update + "'" + einvoiceno + "'";
                //Head_Update = Head_Update + "case when 0='" + tdsaccountcode + "' then null else '" + tdsaccountcode + "' end";
                #endregion

                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and YEAR_CODE='" + Year_Code + "' and Doc_No='" + txtDoc_No.Text + "'";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();

            }
            string deleteledger = Convert.ToString(clsCommon.getString("delete from nt_1_gledger where TRAN_TYPE='XP' and  DOC_NO=" + Doc_No +
               " and company_code='" + Session["Company_Code"].ToString() + "' and YEAR_CODE='" + Year_Code + "' "));
            Head_Values = "";
            //ledger effect
            string ledgerfleid = "";
            string values = "";
            int ordercode = 1;
            qry = "";
            int cgstacid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseCGSTAc"] +
               " and company_code='" + Session["Company_Code"].ToString() + "'"));
            int sgstacid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseSGSTAc"] +
                " and company_code='" + Session["Company_Code"].ToString() + "'"));
            int igstacid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseIGSTAc"] +
                " and company_code='" + Session["Company_Code"].ToString() + "'"));
            string gledger = "nt_1_gledger";
            string narration = txtNarration.Text != string.Empty ? txtNarration.Text : "";

            #region[supplier]
            if (Bill_Amount > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Supplier_Code + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";


               // Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + Bill_Amount + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'C',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + suppliercode + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion
            ledgerfleid = "";
            Head_Values = "";
            #region[taxable]
            if (Taxable_Amount > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Exp_Ac + "',";
                ledgerfleid = ledgerfleid + "NARRATION,"  + " ";
                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + Supplier_Code + "',";
               
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + Taxable_Amount + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + expcode + "'";
                
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion


            ledgerfleid = "";
            Head_Values = "";
            #region[cgst]
            if (CGST_Amount > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Session["PurchaseCGSTAc"] + "',";
                ledgerfleid = ledgerfleid + "NARRATION," +  " ";
                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + Supplier_Code + "',";
               // Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + CGST_Amount + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + cgstacid + "'";
               
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            ledgerfleid = "";
            Head_Values = "";

            #region[sgst]
            if (SGST_Amount > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Session["PurchaseSGSTAc"] + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";

                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + Supplier_Code + "',";
              //  Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + SGST_Amount + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + sgstacid + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            ledgerfleid = "";
            Head_Values = "";

            #region[igst]
            if (IGST_Amount > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Session["PurchaseIGSTAc"] + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + Supplier_Code + "',";
              //  Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + IGST_Amount + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + igstacid + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            ledgerfleid = "";
            Head_Values = "";

            if (Other_Amount != 0)
            {
                #region[otheramount]
                if (Other_Amount > 0)
                {
                    ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                    Head_Values = Head_Values + "'XP',";
                    ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                    Head_Values = Head_Values + "'" + Doc_No + "',";
                    ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                    Head_Values = Head_Values + "'" + DOC_DATE + "',";
                    ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                    Head_Values = Head_Values + "'" + Exp_Ac + "',";
                    ledgerfleid = ledgerfleid + "NARRATION," + " ";
                    Head_Values = Head_Values + "'" + narration + "',";
                   
                    //Head_Values = Head_Values + "'Other purchase',";
                    ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                    Head_Values = Head_Values + "'" + Other_Amount + "',";
                    ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                    Head_Values = Head_Values + "'" + Year_Code + "',";
                    ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                    Head_Values = Head_Values + "'" + Company_Code + "',";
                    ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                    Head_Values = Head_Values + "'" + ordercode + "',";
                    ledgerfleid = ledgerfleid + "DRCR," + " ";
                    Head_Values = Head_Values + "'D',";
                    ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                    Head_Values = Head_Values + "'XP',";
                    ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                    Head_Values = Head_Values + "'" + Doc_No + "',";
                    ledgerfleid = ledgerfleid + "ac" + " ";
                    Head_Values = Head_Values + "'" + expcode + "'";
                    ordercode = ordercode + 1;
                    flag = 1;
                    qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
                }
                else
                {
                    ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                    Head_Values = Head_Values + "'XP',";
                    ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                    Head_Values = Head_Values + "'" + Doc_No + "',";
                    ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                    Head_Values = Head_Values + "'" + DOC_DATE + "',";
                    ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                    Head_Values = Head_Values + "'" + Exp_Ac + "',";
                    ledgerfleid = ledgerfleid + "NARRATION," + " ";
                    Head_Values = Head_Values + "'Other purchase',";
                    ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                    Head_Values = Head_Values + "'" + Other_Amount + "',";
                    ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                    Head_Values = Head_Values + "'" + Year_Code + "',";
                    ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                    Head_Values = Head_Values + "'" + Company_Code + "',";
                    ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                    Head_Values = Head_Values + "'" + ordercode + "',";
                    ledgerfleid = ledgerfleid + "DRCR," + " ";
                    Head_Values = Head_Values + "'C',";
                    ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                    Head_Values = Head_Values + "'XP',";
                    ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                    Head_Values = Head_Values + "'" + Doc_No + "',";
                    ledgerfleid = ledgerfleid + "ac" + " ";
                    Head_Values = Head_Values + "'" + expcode + "'";
                    ordercode = ordercode + 1;
                    flag = 1;
                    qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
                }
                #endregion
            }
            ledgerfleid = "";
            Head_Values = "";

            #region[tds]
            if (TDS > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + TDS_Cutt_AcCode + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";
               
                // Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + TDS + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + tdscuttingcode + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            ledgerfleid = "";
            Head_Values = "";

            #region[tds]
            if (TDS > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + DOC_DATE + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + TDS_AcCode + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";

               // Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + TDS + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + Year_Code + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'C',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'XP',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + Doc_No + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + tdsaccountcode + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            if (btnSave.Text == "Save")
            {
                flag = 1;
            }
            else
            {
                flag = 2;
            }
            Thread thred1 = new Thread(() => { count = DataStore(qry, flag); });
            thred1.Start();
            thred1.Join();

            if (count == 2)
            {
                int do_id = Convert.ToInt32(clsCommon.getString("select opid from " + tblHead + " where doc_no=" + lblDoc_No.Text +
                  " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                  " " + Session["year"].ToString() + ""));
                hdnf.Value = Convert.ToString(do_id);
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
            }

            if (count == 1)
            {
                hdnf.Value = counts.ToString();
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
            }
        }
        catch
        {

        }
    }
    #endregion

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_No) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "' "));
            if (counts == 0)
            {
                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "' ")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(opid) as opid from " + tblHead + " "));
            if (counts == 0)
            {
                lblDoc_No.Text = "1";
                opid = 1;
            }
            else
            {
                opid = Convert.ToInt32(clsCommon.getString("SELECT max(opid) as opid from " + tblHead)) + 1;
                lblDoc_No.Text = opid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }

    }
    #endregion

    #region [data store]
    private int DataStore(string Query, int flag)
    {
        int count = 0;
        try
        {
            //Connection open
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            ///Execution
            myTran = con.BeginTransaction();
            //cmd.CommandText = qry;
            //cmd.Connection = con;
            //cmd.Transaction = myTran;
            cmd = new SqlCommand(Query, con, myTran);

            cmd.ExecuteNonQuery();
            myTran.Commit();
            Thread.Sleep(100);
            if (flag == 1)
            {
                count = 1;
            }
            else if (flag == 2)
            {
                count = 2;
            }
            else
            {
                count = 3;
            }

            return count;
        }
        catch
        {
            if (myTran != null)
            {
                myTran.Rollback();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);

            }
            return count;

        }
        finally
        {
            con.Close();
        }

    }
    #endregion
}

