using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;




public partial class pgeGstr3B : System.Web.UI.Page
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
    int Rowaction = 2;
    int Srno = 3;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Gstr3B";
            tblDetails = "GSTR3B_Excel_Utility_V4.0";
            qryCommon = tblHead;

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
                // btnAdddetails.Enabled = false;
                //btnClosedetails.Enabled = false;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                // btntxtDisplay.Text = true;
                txtDisplay.Enabled = false;
                btntxtShow3B.Enabled = false;
                txtEdiDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtFrom_Date.Enabled = false;
                CalendarExtenderDatetxtFrom_Date.Enabled = false;
                txtTo_Date.Enabled = false;
                CalendarExtenderDatetxtTo_Date.Enabled = false;
                txtSale_Taxable.Enabled = false;
                txtSale_Igst.Enabled = false;
                txtSale_Cgst.Enabled = false;
                txtSale_Sgst.Enabled = false;
                txtExport_Amount.Enabled = false;
                txtRC_Taxable.Enabled = false;
                txtRC_IGST.Enabled = false;
                txtRC_CGCT.Enabled = false;
                txtRC_Sgst.Enabled = false;
                txtO_RC_Taxable.Enabled = false;
                txtO_RC_Igst.Enabled = false;
                txtO_RC_Cgst.Enabled = false;
                txtO_RC_Sgst.Enabled = false;
                txtIrc_Igst.Enabled = false;
                txtIrc_Cgst.Enabled = false;
                txtIrc_Sgst.Enabled = false;
                txtPurchase_Igst.Enabled = false;
                txtPurchase_Cgst.Enabled = false;
                txtPuchase_Sgst.Enabled = false;
                txtIn_RC_Igst.Enabled = false;
                txtIn_RC_Cgst.Enabled = false;
                txtIn_RC_Sgst.Enabled = false;
                txtIntra_State_Supply.Enabled = false;
                txtCNCTaxableAmount.Enabled = false;
                txtCNCIgst.Enabled = false;
                txtCNCCgst.Enabled = false;
                txtCNCSgst.Enabled = false;
                txtDNCTaxable.Enabled = false;
                txtDNCIgst.Enabled = false;
                txtDNCCgst.Enabled = false;
                txtDNCSgst.Enabled = false;

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
                txtEdiDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                // btnAdddetails.Enabled = true;
                // btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtFrom_Date.Enabled = true;
                txtFrom_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtFrom_Date.Enabled = true;
                txtTo_Date.Enabled = true;
                txtTo_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtTo_Date.Enabled = true;
                txtSale_Taxable.Enabled = true;
                txtSale_Igst.Enabled = true;
                txtSale_Cgst.Enabled = true;
                txtSale_Sgst.Enabled = true;
                txtExport_Amount.Enabled = true;
                txtRC_Taxable.Enabled = true;
                txtRC_IGST.Enabled = true;
                txtRC_CGCT.Enabled = true;
                txtRC_Sgst.Enabled = true;
                txtO_RC_Taxable.Enabled = true;
                txtO_RC_Igst.Enabled = true;
                txtO_RC_Cgst.Enabled = true;
                txtO_RC_Sgst.Enabled = true;
                txtIrc_Igst.Enabled = true;
                txtIrc_Cgst.Enabled = true;
                txtIrc_Sgst.Enabled = true;
                txtPurchase_Igst.Enabled = true;
                txtPurchase_Cgst.Enabled = true;
                txtPuchase_Sgst.Enabled = true;
                txtIn_RC_Igst.Enabled = true;
                txtIn_RC_Cgst.Enabled = true;
                txtIn_RC_Sgst.Enabled = true;
                txtDisplay.Enabled = true;
                //  Button1.Enabled = true;
                btntxtShow3B.Enabled = true;
                txtIntra_State_Supply.Enabled = true;
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
                //btntxtDoc_No.Text = "Choose No";
                // btntxtDoc_No.Enabled = false;
                txtEdiDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtFrom_Date.Enabled = false;
                CalendarExtenderDatetxtFrom_Date.Enabled = false;
                txtTo_Date.Enabled = false;
                CalendarExtenderDatetxtTo_Date.Enabled = false;
                txtSale_Taxable.Enabled = false;
                txtSale_Igst.Enabled = false;
                txtSale_Cgst.Enabled = false;
                txtSale_Sgst.Enabled = false;
                txtExport_Amount.Enabled = false;
                txtRC_Taxable.Enabled = false;
                txtRC_IGST.Enabled = false;
                txtRC_CGCT.Enabled = false;
                txtRC_Sgst.Enabled = false;
                txtO_RC_Taxable.Enabled = false;
                txtO_RC_Igst.Enabled = false;
                txtO_RC_Cgst.Enabled = false;
                txtO_RC_Sgst.Enabled = false;
                txtIrc_Igst.Enabled = false;
                txtIrc_Cgst.Enabled = false;
                txtIrc_Sgst.Enabled = false;
                txtPurchase_Igst.Enabled = false;
                txtPurchase_Cgst.Enabled = false;
                txtPuchase_Sgst.Enabled = false;
                txtIn_RC_Igst.Enabled = false;
                txtIn_RC_Cgst.Enabled = false;
                txtIn_RC_Sgst.Enabled = false;
                txtIntra_State_Supply.Enabled = false;
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
                // btntxtDoc_No.Text = "Choose No";
                // btntxtDoc_No.Enabled = true;
                txtEdiDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtFrom_Date.Enabled = true;
                CalendarExtenderDatetxtFrom_Date.Enabled = true;
                txtTo_Date.Enabled = true;
                CalendarExtenderDatetxtTo_Date.Enabled = true;
                txtSale_Taxable.Enabled = true;
                txtSale_Igst.Enabled = true;
                txtSale_Cgst.Enabled = true;
                txtSale_Sgst.Enabled = true;
                txtExport_Amount.Enabled = true;
                txtRC_Taxable.Enabled = true;
                txtRC_IGST.Enabled = true;
                txtRC_CGCT.Enabled = true;
                txtRC_Sgst.Enabled = true;
                txtO_RC_Taxable.Enabled = true;
                txtO_RC_Igst.Enabled = true;
                txtO_RC_Cgst.Enabled = true;
                txtO_RC_Sgst.Enabled = true;
                txtIrc_Igst.Enabled = true;
                txtIrc_Cgst.Enabled = true;
                txtIrc_Sgst.Enabled = true;
                txtPurchase_Igst.Enabled = true;
                txtPurchase_Cgst.Enabled = true;
                txtPuchase_Sgst.Enabled = true;
                txtIn_RC_Igst.Enabled = true;
                txtIn_RC_Cgst.Enabled = true;
                txtIn_RC_Sgst.Enabled = true;
                txtIntra_State_Supply.Enabled = true;
                btntxtShow3B.Enabled = true;
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
            qry = "select max(Doc_No) as Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
        // #region enable disable previous next buttons
        //   int RecordCount = 0;
        //   string query = "";
        //   query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'";
        //string cnt = clsCommon.getString(query); 
        //  if (cnt != string.Empty) 
        //       {
        //RecordCount = Convert.ToInt32(cnt);
        //       }
        //   if (RecordCount != 0 && RecordCount == 1)
        //   {
        //       btnFirst.Enabled = true;
        //       btnPrevious.Enabled = false;
        //       btnNext.Enabled = false;
        //       btnLast.Enabled = false;
        //   }
        //   else if (RecordCount != 0 && RecordCount > 1)
        //   {
        //       btnFirst.Enabled = true;
        //       btnPrevious.Enabled = false;
        //       btnNext.Enabled = false;
        //       btnLast.Enabled = true;
        //   }
        //   if (txtDoc_No.Text != string.Empty)
        //   {
        //       #region check for next or previous record exist or not
        //       query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())+ "' ORDER BY Doc_No asc  ";
        //       string strDoc_No = clsCommon.getString(query);
        //        if (strDoc_No != string.Empty)
        //        {
        //         btnNext.Enabled = true;
        //         btnLast.Enabled = true;
        //        }
        //       else
        //        {
        //         btnNext.Enabled = false;
        //         btnLast.Enabled = false;
        //        }
        //       query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())+ "' ORDER BY Doc_No desc  ";
        //        if (strDoc_No != string.Empty)
        //        {
        //         btnPrevious.Enabled = true;
        //         btnFirst.Enabled = true;
        //        }
        //       else
        //        {
        //         btnPrevious.Enabled = false;
        //         btnFirst.Enabled = false;
        //        }
        //   }
        //       #endregion
        // #endregion

        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());


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
                " ORDER BY Doc_No asc  ";
            string strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty)
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
                " ORDER BY Doc_No desc  ";
            strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty)
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
            // query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No desc  ";
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc  ";
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion


    protected void btntxtDisplay_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string fromdate = DateTime.Parse(txtFrom_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string todate = DateTime.Parse(txtTo_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        qry = "select sum(subTotal) as ta,sum(CGSTAmount) as cgst,sum(SGSTAmount) as sgst,sum(IGSTAmount) as IGST,sum(Bill_Amount) as ba " +
            " from nt_1_sugarsale where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            " and doc_date between '" + fromdate + "' and '" + todate + "'";

        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            if (txtSale_Taxable.Text == string.Empty || txtSale_Taxable.Text == "0")
            {

                txtSale_Taxable.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtSale_Taxable.Text);
                txtSale_Taxable.Text = Math.Round(var).ToString();
            }
            if (txtSale_Cgst.Text == string.Empty || txtSale_Cgst.Text == "0")
            {

                txtSale_Cgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtSale_Cgst.Text);
                txtSale_Cgst.Text = Math.Round(var).ToString();
            }
            if (txtSale_Sgst.Text == string.Empty || txtSale_Sgst.Text == "0")
            {

                txtSale_Sgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtSale_Sgst.Text);
                txtSale_Sgst.Text = Math.Round(var).ToString();
            }
            if (txtSale_Igst.Text == string.Empty || txtSale_Igst.Text == "0")
            {

                txtSale_Igst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtSale_Igst.Text);
                txtSale_Igst.Text = Math.Round(var).ToString();
            }
        }
        qry = "select sum(Taxable_Amount) as ta,sum(CGST_Amount) as cgst,sum(SGST_Amount) as sgst,sum(IGST_Amount) as IGST" +
           " from Retail_Head where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
           " and doc_date between '" + fromdate + "' and '" + todate + "'";

        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            if (txtretail_taxable.Text == string.Empty || txtretail_taxable.Text == "0")
            {

                txtretail_taxable.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtretail_taxable.Text);
                txtretail_taxable.Text = Math.Round(var).ToString();
            }
            if (txtretailsaleCgst.Text == string.Empty || txtretailsaleCgst.Text == "0")
            {

                txtretailsaleCgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtretailsaleCgst.Text);
                txtretailsaleCgst.Text = Math.Round(var).ToString();
            }
            if (txtretailsaleSgst.Text == string.Empty || txtretailsaleSgst.Text == "0")
            {

                txtretailsaleSgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtretailsaleSgst.Text);
                txtretailsaleSgst.Text = Math.Round(var).ToString();
            }
            if (txtretailsaleIgst.Text == string.Empty || txtretailsaleIgst.Text == "0")
            {

                txtretailsaleIgst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtretailsaleIgst.Text);
                txtretailsaleIgst.Text = Math.Round(var).ToString();
            }
        }

        //qry = "select sum(Taxable_Amount) as ta,sum(CGST_Amount) as cgst,sum(SGST_Amount) as sgst,sum(IGST_Amount) as IGST,sum(Bill_Amount) as ba " +
        //    " from GSTSale_Head where ac_code=2178 and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        //    " and Doc_Date between '" + fromdate + "' and '" + todate + "'";

        //ds = clsDAL.SimpleQuery(qry);
        //dt = ds.Tables[0];

        //if (dt.Rows.Count > 0)
        //{
        //    if (txtExport_Amount.Text == string.Empty || txtExport_Amount.Text == "0")
        //    {
        //        txtExport_Amount.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00";
        //        Double var = Convert.ToDouble(txtExport_Amount.Text);
        //        txtExport_Amount.Text = Math.Round(var).ToString();
        //    }
        //}

        txtExport_Amount.Text = "0";


        double oigst = 0.0;
        double ocgst = 0.0;
        double osgst = 0.0;

        //Less-Sales Ret Credit Note To Customer

        #region

        double taxableamount = 0.00;
        double Cgst = 0.00;
        double Igst = 0.00;
        double Sgst = 0.00;
        qry = "select sum(subTotal) as ta,sum(CGSTAmount) as cgst,sum(SGSTAmount) as sgst,sum(IGSTAmount) as IGST " +
       " from nt_1_sugarpurchasereturn where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
       " and doc_date between '" + fromdate + "' and '" + todate + "'";



        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            taxableamount = Convert.ToDouble(dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00");
            Cgst = Convert.ToDouble(dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00");
            Igst = Convert.ToDouble(dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00");
            Sgst = Convert.ToDouble(dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00");
        }
        string qry1 = "select sum(texable_amount) as ta,sum(cgst_amount) as cgst,sum(sgst_amount) as sgst,sum(igst_amount) as IGST " +
                  " from debitnotehead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                  " and doc_date between '" + fromdate + "' and '" + todate + "' and tran_type='CN'";
          ds = clsDAL.SimpleQuery(qry1);
         DataTable  dtt = ds.Tables[0];


         if (dtt.Rows.Count > 0)
        {
            if (txtCNCTaxableAmount.Text == string.Empty || txtCNCTaxableAmount.Text == "0")
            {
                txtCNCTaxableAmount.Text = dtt.Rows[0]["ta"].ToString() != string.Empty ? dtt.Rows[0]["ta"].ToString() : "0.00";
                Double abc1 = Convert.ToDouble(txtCNCTaxableAmount.Text) + taxableamount;
                txtCNCTaxableAmount.Text = Math.Round(abc1).ToString();

            }
            if (txtCNCCgst.Text == string.Empty || txtCNCCgst.Text == "0")
            {
                txtCNCCgst.Text = dtt.Rows[0]["cgst"].ToString() != string.Empty ? dtt.Rows[0]["cgst"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtCNCCgst.Text) + Cgst;
                txtCNCCgst.Text = Math.Round(abc).ToString();
            }
            if (txtCNCSgst.Text == string.Empty || txtCNCSgst.Text == "0")
            {

                txtCNCSgst.Text = dtt.Rows[0]["sgst"].ToString() != string.Empty ? dtt.Rows[0]["sgst"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtCNCSgst.Text) + Sgst;
                txtCNCSgst.Text = Math.Round(abc).ToString();
            }
            if (txtCNCIgst.Text == string.Empty || txtCNCIgst.Text == "0")
            {
                txtCNCIgst.Text = dtt.Rows[0]["IGST"].ToString() != string.Empty ? dtt.Rows[0]["IGST"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtCNCIgst.Text) + Igst;
                txtCNCIgst.Text = Math.Round(abc).ToString();
            }
            oigst = Convert.ToDouble(txtCNCIgst.Text);
            ocgst = Convert.ToDouble(txtCNCCgst.Text);
            osgst = Convert.ToDouble(txtCNCSgst.Text);

        }
        //txtCNCTaxableAmount.Text = "0";
        //txtCNCCgst.Text = "0";
        //txtCNCIgst.Text = "0";
        //txtCNCSgst.Text = "0";

        #endregion

        //alka
        qry = "select sum(texable_amount) as ta,sum(cgst_amount) as cgst,sum(sgst_amount) as sgst,sum(igst_amount) as IGST " +
          " from debitnotehead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
          " and doc_date between '" + fromdate + "' and '" + todate + "' and tran_type='DN'";
        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (txtDNCTaxable.Text == string.Empty || txtDNCTaxable.Text == "0")
            {


                txtDNCTaxable.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0";
                Double var = Convert.ToDouble(txtDNCTaxable.Text);
                txtDNCTaxable.Text = Math.Round(var).ToString();
            }
            if (txtDNCCgst.Text == string.Empty || txtDNCCgst.Text == "0")
            {
                txtDNCCgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                Double var1 = Convert.ToDouble(txtDNCCgst.Text);
                txtDNCCgst.Text = Math.Round(var1).ToString();
            }
            if (txtDNCSgst.Text == string.Empty || txtDNCSgst.Text == "0")
            {
                txtDNCSgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                Double var2 = Convert.ToDouble(txtDNCSgst.Text);
                txtDNCSgst.Text = Math.Round(var2).ToString();
            }
            if (txtDNCIgst.Text == string.Empty || txtDNCIgst.Text == "0")
            {
                txtDNCIgst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                Double var3 = Convert.ToDouble(txtDNCIgst.Text);
                txtDNCIgst.Text = Math.Round(var3).ToString();

            }
            oigst = Convert.ToDouble(txtDNCIgst.Text);
            ocgst = Convert.ToDouble(txtDNCCgst.Text);
            osgst = Convert.ToDouble(txtDNCSgst.Text);

        }

        //*Reverse charge Taxable*


        //qry = "select sum(Amount_Detail) as ta,sum(CGST_Amount_Detail) as cgst,sum(SGST_Amount_Detail) as sgst,sum(IGST_Amount_Detail) as IGST " +
        //   " from qryRCM where Sub_Type In('CT','CL') and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        //   " and Doc_Date1 between '" + fromdate + "' and '" + todate + "' and gst_code<>1";

        //ds = clsDAL.SimpleQuery(qry);
        //dt = ds.Tables[0];


        //if (dt.Rows.Count > 0)
        //{
        //    if (txtRC_Taxable.Text == string.Empty || txtRC_Taxable.Text == "0")
        //    {
        //        txtRC_Taxable.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00";
        //        Double var = Convert.ToDouble(txtRC_Taxable.Text);
        //        txtRC_Taxable.Text = Math.Round(var).ToString();
        //    }
        //    if (txtRC_CGCT.Text == string.Empty || txtRC_CGCT.Text == "0")
        //    {
        //        txtRC_CGCT.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
        //        Double var = Convert.ToDouble(txtRC_CGCT.Text);
        //        txtRC_CGCT.Text = Math.Round(var).ToString();
        //    }
        //    if (txtRC_Sgst.Text == string.Empty || txtRC_Sgst.Text == "0")
        //    {
        //        txtRC_Sgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
        //        Double var = Convert.ToDouble(txtRC_Sgst.Text);
        //        txtRC_Sgst.Text = Math.Round(var).ToString();
        //    }
        //    if (txtRC_IGST.Text == string.Empty || txtRC_IGST.Text == "0")
        //    {
        //        txtRC_IGST.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
        //        Double var = Convert.ToDouble(txtRC_IGST.Text);
        //        txtRC_IGST.Text = Math.Round(var).ToString();
        //    }

        //}
        if (txtRC_Taxable.Text == "")
        {
            txtRC_Taxable.Text = "0";
        }

        if (txtRC_IGST.Text == "")
        {
            txtRC_IGST.Text = "0";
        }

        if (txtRC_CGCT.Text == "")
        {
            txtRC_CGCT.Text = "0";

        }

        if (txtRC_Sgst.Text == "")
        {
            txtRC_Sgst.Text = "0";

        }

        //Other Reverse Charge

        if (txtO_RC_Igst.Text == "")
        {
            txtO_RC_Igst.Text = "0";
        }

        if (txtO_RC_Cgst.Text == "")
        {
            txtO_RC_Cgst.Text = "0";

        }

        if (txtO_RC_Sgst.Text == "")
        {
            txtO_RC_Sgst.Text = "0";

        }
        //Purchase Detail	.....


        qry = "select sum(subTotal) as ta,sum(CGSTAmount) as cgst,sum(SGSTAmount) as sgst,sum(IGSTAmount) as IGST " +
       " from nt_1_sugarpurchasereturn where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
       " and doc_date between '" + fromdate + "' and '" + todate + "'";

        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            if (txtPurchase_Cgst.Text == string.Empty || txtPurchase_Cgst.Text == "0")
            {
                txtPurchase_Cgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtPurchase_Cgst.Text);
                txtPurchase_Cgst.Text = Math.Round(var).ToString();
            }
            if (txtPuchase_Sgst.Text == string.Empty || txtPuchase_Sgst.Text == "0")
            {
                txtPuchase_Sgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtPuchase_Sgst.Text);
                txtPuchase_Sgst.Text = Math.Round(var).ToString();
            }
            if (txtPurchase_Igst.Text == string.Empty || txtPurchase_Igst.Text == "0")
            {
                txtPurchase_Igst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                Double var = Convert.ToDouble(txtPurchase_Igst.Text);
                txtPurchase_Igst.Text = Math.Round(var).ToString();
            }
            oigst = Convert.ToDouble(txtPurchase_Igst.Text);
            ocgst = Convert.ToDouble(txtPurchase_Cgst.Text);
            osgst = Convert.ToDouble(txtPuchase_Sgst.Text);

        }

        //qry = "select sum(Sub_Total) as ta,sum(CGST_Amount_Total) as cgst,sum(SGST_Amount_Total) as sgst,sum(IGST_Amount_Total) as IGST " +
        // " from qryGstCreditnotetoSupplier where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        // " and Doc_Date between '" + fromdate + "' and '" + todate + "'";

        //Add Credit Note to Supplier

        qry = "select sum(texable_amount) as ta,sum(cgst_amount) as cgst,sum(sgst_amount) as sgst,sum(igst_amount) as IGST " +
             " from debitnotehead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
             " and doc_date between '" + fromdate + "' and '" + todate + "' and tran_type='CS'";
        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (txtPCNSTaxable.Text == string.Empty || txtPCNSTaxable.Text == "0")
            {
                txtPCNSTaxable.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtPCNSTaxable.Text);
                txtPCNSTaxable.Text = Math.Round(abc).ToString();
            }
            if (txtPCNSCgst.Text == string.Empty || txtPCNSCgst.Text == "0")
            {
                txtPCNSCgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtPCNSCgst.Text);
                txtPCNSCgst.Text = Math.Round(abc).ToString();
            }
            if (txtPCNSSgst.Text == string.Empty || txtPCNSSgst.Text == "0")
            {
                txtPCNSSgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtPCNSSgst.Text);
                txtPCNSSgst.Text = Math.Round(abc).ToString();
            }
            if (txtPCNSIgst.Text == string.Empty || txtPCNSIgst.Text == "0")
            {
                txtPCNSIgst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                Double abc = Convert.ToDouble(txtPCNSIgst.Text);
                txtPCNSIgst.Text = Math.Round(abc).ToString();

            }
            oigst = Convert.ToDouble(txtPCNSIgst.Text);
            ocgst = Convert.ToDouble(txtPCNSCgst.Text);
            osgst = Convert.ToDouble(txtPCNSSgst.Text);

        }

        //****** Less Purchase Return Debit Note 18%******

        //qry = "select sum(Sub_Total) as ta,sum(CGST_Amount_Total) as cgst,sum(SGST_Amount_Total) as sgst,sum(IGST_Amount_Total) as IGST " +
        //" from qryGstdebitNoteSupplier where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        //" and Doc_Date between '" + fromdate + "' and '" + todate + "'";


        qry = "select sum(texable_amount) as ta,sum(cgst_amount) as cgst,sum(sgst_amount) as sgst,sum(igst_amount) as IGST " +
          " from debitnotehead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
          " and doc_date between '" + fromdate + "' and '" + todate + "' and tran_type='DS'";
        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (txtPCNSTaxableA.Text == string.Empty || txtPCNSTaxableA.Text == "0")
            {
                txtPCNSTaxableA.Text = dt.Rows[0]["ta"].ToString() != string.Empty ? dt.Rows[0]["ta"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtPCNSTaxableA.Text);
                txtPCNSTaxableA.Text = Math.Round(abc).ToString();
            }
            if (txtPRCNSCgst.Text == string.Empty || txtPRCNSCgst.Text == "0")
            {
                txtPRCNSCgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtPRCNSCgst.Text);
                txtPRCNSCgst.Text = Math.Round(abc).ToString();
            }
            if (txtPRCNSSgst.Text == string.Empty || txtPRCNSSgst.Text == "0")
            {
                txtPRCNSSgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtPRCNSSgst.Text);
                txtPRCNSSgst.Text = Math.Round(abc).ToString();
            }
            if (txtPRCNSIgst.Text == string.Empty || txtPRCNSIgst.Text == "0")
            {
                txtPRCNSIgst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtPRCNSIgst.Text);
                txtPRCNSIgst.Text = Math.Round(abc).ToString();
            }

            oigst = Convert.ToDouble(txtPRCNSIgst.Text);
            ocgst = Convert.ToDouble(txtPRCNSCgst.Text);
            osgst = Convert.ToDouble(txtPRCNSSgst.Text);

        }





        qry = "select sum(CGST_Amt) as cgst,sum(SGST_Amt) as sgst,sum(IGST_Amt) as IGST " +
       " from other_input_gst where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
       " and Doc_Date between '" + fromdate + "' and '" + todate + "'";
        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {

            if (txtOICgst.Text == string.Empty || txtOICgst.Text == "0")
            {
                txtOICgst.Text = dt.Rows[0]["cgst"].ToString() != string.Empty ? dt.Rows[0]["cgst"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtOICgst.Text);
                txtOICgst.Text = Math.Round(abc).ToString();
            }
            if (txtOIIgst.Text == string.Empty || txtOIIgst.Text == "0")
            {
                txtOIIgst.Text = dt.Rows[0]["IGST"].ToString() != string.Empty ? dt.Rows[0]["IGST"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtOIIgst.Text);
                txtOIIgst.Text = Math.Round(abc).ToString();
            }
            if (txtOISgst.Text == string.Empty || txtOISgst.Text == "0")
            {
                txtOISgst.Text = dt.Rows[0]["sgst"].ToString() != string.Empty ? dt.Rows[0]["sgst"].ToString() : "0.00";
                double abc = Convert.ToDouble(txtOISgst.Text);
                txtOISgst.Text = Math.Round(abc).ToString();
            }
            oigst = Convert.ToDouble(txtOIIgst.Text);
            ocgst = Convert.ToDouble(txtOICgst.Text);
            osgst = Convert.ToDouble(txtOISgst.Text);
        }

        txtO_RC_Taxable.Text = "0";
        txtotherInput.Text = "0";
        txtIn_RC_Cgst.Text="0";
        txtIn_RC_Sgst.Text = "0";
        txtIn_RC_Igst.Text = "0";
        txtIntra_State_Supply.Text = "0";


        Calculation();
    }

    protected void btntxtShow3B_Click(object sender, EventArgs e)
    {
        #region comment

  //      Double Saletaxable = 0.00;
  //      Saletaxable = Convert.ToDouble(txtSale_Taxable.Text) + Convert.ToDouble(txtretail_taxable.Text);
  //      Double SaleIgst = Convert.ToDouble(txtSale_Igst.Text) + Convert.ToDouble(txtPCNSIgst.Text) + Convert.ToDouble(txtDNCIgst.Text);
  //      Double SaleCgst = Convert.ToDouble(txtSale_Cgst.Text) + Convert.ToDouble(txtCNCCgst.Text) + Convert.ToDouble(txtDNCCgst.Text);
  //      Double SaleSgst = Convert.ToDouble(txtSale_Sgst.Text) + Convert.ToDouble(txtCNCSgst.Text) + Convert.ToDouble(txtDNCSgst.Text);
  //      Double SaleExport = Convert.ToDouble(txtExport_Amount.Text);
  //      Double RCPayble = Convert.ToDouble(txtRC_Taxable.Text);
  //      Double RcIgst = Convert.ToDouble(txtRC_IGST.Text);
  //      Double RcCgst = Convert.ToDouble(txtRC_CGCT.Text);
  //      Double RcSgst = Convert.ToDouble(txtRC_Sgst.Text);
  //      Double PurchesIgst = Convert.ToDouble(txtPurchase_Igst.Text) + Convert.ToDouble(txtPCNSIgst.Text) + Convert.ToDouble(txtPRCNSIgst.Text) + Convert.ToDouble(txtOIIgst.Text);
  //      Double PurchesCgst = Convert.ToDouble(txtPurchase_Cgst.Text) + Convert.ToDouble(txtPCNSCgst.Text) + Convert.ToDouble(txtPRCNSCgst.Text) + Convert.ToDouble(txtOICgst.Text);
  //      Double PurchesSgst = Convert.ToDouble(txtPuchase_Sgst.Text) + Convert.ToDouble(txtPCNSSgst.Text) + Convert.ToDouble(txtPRCNSSgst.Text) + Convert.ToDouble(txtOISgst.Text);



  //      Excel._Application myExcelApp;
  //      Excel.Workbooks myExcelWorkbooks;
  //      Excel.Workbook myExcelWorkbook;
  //      // Excel ._Worksheet myExccelWorksheetToChange;
  //      object misValue = System.Reflection.Missing.Value;

  //      myExcelApp = new Excel.ApplicationClass();
  //      myExcelApp.Visible = true;
  ////      myExcelApp.DisplayAlerts = false;
  //      myExcelWorkbooks = myExcelApp.Workbooks;
  //      // String fileName = "D:\\GSTR3B_Excel_Utility_V4.0.xlsm";
  //      //String fileName = "E:\\GSTR3B_Excel_Utility_V4.7.xlsm";

       

  //     // string fileName = @"C:\IIS\GSTR3B_Excel_Utility_V4.7..xlsm";

  //      //string filepath = @"D:\pdffiles";
  //     // string fileName = filepath + "\\GSTR3B_Excel_Utility_V4.xlsm";
  //      String fileName = "C:\\IIS\\GSTR3B_Excel_Utility_V4.7.xlsm";
  //     //string fileName =@"D:\PDFFiles\GSTR3B_Excel_Utility_V4.7.xlsm";

  //      myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue,
  //      misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

  //      Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

  //      String cellFormulaAsString = myExcelWorksheet.get_Range("A2", misValue).Formula.ToString();

  //      myExcelWorksheet.get_Range("A1", misValue).Formula = Console.ReadLine();
  //      //myExcelWorksheet.get_Range("A1", misValue).Formula = Console.WriteLine();
  //      myExcelWorksheet.get_Range("C11", misValue).Formula = Saletaxable;
  //      myExcelWorksheet.get_Range("D11", misValue).Formula = SaleIgst;
  //      myExcelWorksheet.get_Range("E11", misValue).Formula = SaleCgst;
  //      //  myExcelWorksheet.get_Range("F11", misValue).Formula = txtSale_Sgst.Text;
  //      myExcelWorksheet.get_Range("C12", misValue).Formula = SaleExport;
  //      myExcelWorksheet.get_Range("C14", misValue).Formula = RCPayble;
  //      myExcelWorksheet.get_Range("D14", misValue).Formula = RcIgst;
  //      myExcelWorksheet.get_Range("E14", misValue).Formula = RcCgst;
  //      //  myExcelWorksheet.get_Range("F14", misValue).Formula = txtIrc_Sgst.Text;
  //      myExcelWorksheet.get_Range("C24", misValue).Formula = RcIgst;
  //      myExcelWorksheet.get_Range("D24", misValue).Formula = RcCgst;
  //      //    myExcelWorksheet.get_Range("E24", misValue).Formula = txtIrc_Sgst.Text;
  //      myExcelWorksheet.get_Range("C26", misValue).Formula = PurchesIgst;
  //      myExcelWorksheet.get_Range("D26", misValue).Formula = PurchesCgst;
  //      //     myExcelWorksheet.get_Range("E26", misValue).Formula = txtPuchase_Sgst.Text;
  //      myExcelWorksheet.get_Range("E40", misValue).Formula = txtIntra_State_Supply.Text;
        #endregion



        Double Saletaxable;
        Saletaxable = Convert.ToDouble(txtSale_Taxable.Text) + Convert.ToDouble(txtretail_taxable.Text);
        Double SaleIgst = Convert.ToDouble(txtSale_Igst.Text) + Convert.ToDouble(txtretailsaleIgst.Text); ;
        Double SaleCgst = Convert.ToDouble(txtSale_Cgst.Text) + Convert.ToDouble(txtretailsaleSgst.Text);
        Double SaleSgst = Convert.ToDouble(txtSale_Sgst.Text) + Convert.ToDouble(txtretailsaleSgst.Text);
        Double SaleExport = Convert.ToDouble(txtExport_Amount.Text);
        Double RCPayble = Convert.ToDouble(txtRC_Taxable.Text);
        Double RcIgst = Convert.ToDouble(txtRC_IGST.Text);
        Double RcCgst = Convert.ToDouble(txtRC_CGCT.Text);
        Double RcSgst = Convert.ToDouble(txtRC_Sgst.Text);
        Double PurchesIgst = Convert.ToDouble(txtPurchase_Igst.Text) + Convert.ToDouble(txtPCNSIgst.Text) + Convert.ToDouble(txtPRCNSIgst.Text) + Convert.ToDouble(txtOIIgst.Text);
        Double PurchesCgst = Convert.ToDouble(txtPurchase_Cgst.Text) + Convert.ToDouble(txtPCNSCgst.Text) + Convert.ToDouble(txtPRCNSCgst.Text) + Convert.ToDouble(txtOICgst.Text);
        Double PurchesSgst = Convert.ToDouble(txtPuchase_Sgst.Text) + Convert.ToDouble(txtPCNSSgst.Text) + Convert.ToDouble(txtPRCNSSgst.Text) + Convert.ToDouble(txtOISgst.Text);
        Double Intra_State_Supply = Convert.ToDouble(txtIntra_State_Supply.Text);


        Excel._Application myExcelApp;
        Excel.Workbooks myExcelWorkbooks;
        Excel.Workbook myExcelWorkbook;
        // Excel ._Worksheet myExccelWorksheetToChange;
        object misValue = System.Reflection.Missing.Value;

        myExcelApp = new Excel.ApplicationClass();
        myExcelApp.Visible = true;
        myExcelWorkbooks = myExcelApp.Workbooks;
        //String fileName = "C:\\IIS\\GSTR3B_Excel_Utility_V4.7.xlsm";

        //String fileName = "D:\\pdffiles\\GSTR3B_Excel_Utility_V4.7.xlsm";

        String fileName = "D:\\GSTFile\\GSTR3B_Excel_Utility_V4.7.xlsm";




        //  String fileName = " D:\\IIS\\Foundman\\GstTools\\GSTR3B_Excel_Utility_V4.0.xlsm";
         myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue,
        misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

        Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;

        String cellFormulaAsString = myExcelWorksheet.get_Range("A2", misValue).Formula.ToString();

        myExcelWorksheet.get_Range("A1", misValue).Formula = Console.ReadLine();
        //myExcelWorksheet.get_Range("A1", misValue).Formula = Console.WriteLine();
        myExcelWorksheet.get_Range("C11", misValue).Formula = Saletaxable;
        myExcelWorksheet.get_Range("D11", misValue).Formula = SaleIgst;
        myExcelWorksheet.get_Range("E11", misValue).Formula = SaleCgst;
        //  myExcelWorksheet.get_Range("F11", misValue).Formula = txtSale_Sgst.Text;
        myExcelWorksheet.get_Range("C12", misValue).Formula = SaleExport;
        myExcelWorksheet.get_Range("C14", misValue).Formula = RCPayble;
        myExcelWorksheet.get_Range("D14", misValue).Formula = RcIgst;
        myExcelWorksheet.get_Range("E14", misValue).Formula = RcCgst;
        //  myExcelWorksheet.get_Range("F14", misValue).Formula = txtIrc_Sgst.Text;
        myExcelWorksheet.get_Range("C24", misValue).Formula = RcIgst;
        myExcelWorksheet.get_Range("D24", misValue).Formula = RcCgst;
        //    myExcelWorksheet.get_Range("E24", misValue).Formula = txtIrc_Sgst.Text;
        myExcelWorksheet.get_Range("C26", misValue).Formula = PurchesIgst;
        myExcelWorksheet.get_Range("D26", misValue).Formula = PurchesCgst;
        //     myExcelWorksheet.get_Range("E26", misValue).Formula = txtPuchase_Sgst.Text;
        myExcelWorksheet.get_Range("E40", misValue).Formula = Intra_State_Supply;
    }
    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        setFocusControl(txtDoc_Date);
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

    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_Date);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
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
                        child1.SetAttributeValue("Year_Code", Year_Code);
                        child1.SetAttributeValue("Tran_Type", "");
                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_Gstr3B";
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
                        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
                        hdnf.Value = clsCommon.getString(query);
                        if (hdnf.Value == string.Empty)
                        {
                            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
                            hdnf.Value = clsCommon.getString(query);
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
                    }
                    this.enableDisableNavigateButtons();
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
                        // txtEdiDoc_No.Text = dt.Rows[0]["EdiDoc_No"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_Date"].ToString();
                        txtFrom_Date.Text = dt.Rows[0]["From_Date"].ToString();
                        txtTo_Date.Text = dt.Rows[0]["To_Date"].ToString();
                        txtSale_Taxable.Text = dt.Rows[0]["Sale_Taxable"].ToString();
                        txtSale_Igst.Text = dt.Rows[0]["Sale_Igst"].ToString();
                        txtSale_Cgst.Text = dt.Rows[0]["Sale_Cgst"].ToString();
                        txtSale_Sgst.Text = dt.Rows[0]["Sale_Sgst"].ToString();
                        txtExport_Amount.Text = dt.Rows[0]["Export_Amount"].ToString();
                        txtRC_Taxable.Text = dt.Rows[0]["RC_Taxable"].ToString();
                        txtRC_IGST.Text = dt.Rows[0]["RC_IGST"].ToString();
                        txtRC_CGCT.Text = dt.Rows[0]["RC_CGCT"].ToString();
                        txtRC_Sgst.Text = dt.Rows[0]["RC_Sgst"].ToString();
                        txtO_RC_Taxable.Text = dt.Rows[0]["O_RC_Taxable"].ToString();
                        txtO_RC_Igst.Text = dt.Rows[0]["O_RC_Igst"].ToString();
                        txtO_RC_Cgst.Text = dt.Rows[0]["O_RC_Cgst"].ToString();
                        txtO_RC_Sgst.Text = dt.Rows[0]["O_RC_Sgst"].ToString();
                        txtIrc_Igst.Text = dt.Rows[0]["Irc_Igst"].ToString();
                        txtIrc_Cgst.Text = dt.Rows[0]["Irc_Cgst"].ToString();
                        txtIrc_Sgst.Text = dt.Rows[0]["Irc_Sgst"].ToString();
                        txtPurchase_Igst.Text = dt.Rows[0]["Purchase_Igst"].ToString();
                        txtPurchase_Cgst.Text = dt.Rows[0]["Purchase_Cgst"].ToString();
                        txtPuchase_Sgst.Text = dt.Rows[0]["Puchase_Sgst"].ToString();
                        txtIn_RC_Igst.Text = dt.Rows[0]["In_RC_Igst"].ToString();
                        txtIn_RC_Cgst.Text = dt.Rows[0]["In_RC_Cgst"].ToString();
                        txtIn_RC_Sgst.Text = dt.Rows[0]["In_RC_Sgst"].ToString();
                        txtIntra_State_Supply.Text = dt.Rows[0]["Intra_State_Supply"].ToString();
                        txtCNCTaxableAmount.Text = dt.Rows[0]["CNCtaxable"].ToString();
                        txtCNCIgst.Text = dt.Rows[0]["CNCIgst"].ToString();
                        txtCNCCgst.Text = dt.Rows[0]["CNCCgst"].ToString();
                        txtCNCSgst.Text = dt.Rows[0]["CNCSgst"].ToString();

                        txtDNCTaxable.Text = dt.Rows[0]["DNCTaxable"].ToString();
                        txtDNCIgst.Text = dt.Rows[0]["DNCIgst"].ToString();
                        txtDNCCgst.Text = dt.Rows[0]["DNCCgst"].ToString();
                        txtDNCSgst.Text = dt.Rows[0]["DNCSgst"].ToString();

                        txtPCNSTaxable.Text = dt.Rows[0]["PCNStaxable"].ToString();
                        txtPCNSIgst.Text = dt.Rows[0]["PCNSIgst"].ToString();
                        txtPCNSCgst.Text = dt.Rows[0]["PCNSCgst"].ToString();
                        txtPCNSSgst.Text = dt.Rows[0]["PCNSSgst"].ToString();

                        txtPCNSTaxableA.Text = dt.Rows[0]["PRCNStaxable"].ToString();
                        txtPRCNSIgst.Text = dt.Rows[0]["PRCNSIgst"].ToString();
                        txtPRCNSCgst.Text = dt.Rows[0]["PRCNSCgst"].ToString();
                        txtPRCNSSgst.Text = dt.Rows[0]["PRCNSSgst"].ToString();
                        txtotherInput.Text = dt.Rows[0]["OtherInput"].ToString();
                        txtOIIgst.Text = dt.Rows[0]["OIIgst"].ToString();
                        txtOICgst.Text = dt.Rows[0]["OICgst"].ToString();
                        txtOISgst.Text = dt.Rows[0]["OISgst"].ToString();

                        txtretail_taxable.Text = dt.Rows[0]["RSTaxable"].ToString();
                        txtretailsaleIgst.Text = dt.Rows[0]["RSIgst"].ToString();
                        txtretailsaleCgst.Text = dt.Rows[0]["RSCgst"].ToString();
                        txtretailsaleSgst.Text = dt.Rows[0]["RSSgst"].ToString();

                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            hdnf.Value = txtDoc_No.Text;
            this.enableDisableNavigateButtons();
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
            if (strTextBox == "txtEdiDoc_No")
            {
                setFocusControl(txtEdiDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
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
            if (strTextBox == "txtFrom_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtFrom_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtFrom_Date);
                    //}
                    //else
                    //{
                    //    txtFrom_Date.Text = "";
                    //    setFocusControl(txtFrom_Date);
                    //}
                }
                catch
                {
                    txtFrom_Date.Text = "";
                    setFocusControl(txtFrom_Date);
                }
            }
            if (strTextBox == "txtTo_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtTo_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtTo_Date);
                    //}
                    //else
                    //{
                    //    txtTo_Date.Text = "";
                    //    setFocusControl(txtTo_Date);
                    //}
                }
                catch
                {
                    txtTo_Date.Text = "";
                    setFocusControl(txtTo_Date);
                }
            }
            if (strTextBox == "txtSale_Taxable")
            {
                setFocusControl(txtSale_Taxable);
            }
            if (strTextBox == "txtSale_Igst")
            {
                setFocusControl(txtSale_Igst);
            }
            if (strTextBox == "txtSale_Cgst")
            {
                setFocusControl(txtSale_Cgst);
            }
            if (strTextBox == "txtSale_Sgst")
            {
                setFocusControl(txtSale_Sgst);
            }
            if (strTextBox == "txtExport_Amount")
            {
                setFocusControl(txtExport_Amount);
            }
            if (strTextBox == "txtRC_Taxable")
            {


                setFocusControl(txtRC_Taxable);
            }
            if (strTextBox == "txtRC_IGST")
            {
                setFocusControl(txtRC_IGST);
            }
            if (strTextBox == "txtRC_CGCT")
            {
                setFocusControl(txtRC_CGCT);
            }
            if (strTextBox == "txtRC_Sgst")
            {
                setFocusControl(txtRC_Sgst);
            }
            if (strTextBox == "txtO_RC_Taxable")
            {
                setFocusControl(txtO_RC_Taxable);
            }
            if (strTextBox == "txtO_RC_Igst")
            {
                setFocusControl(txtO_RC_Igst);
            }
            if (strTextBox == "txtO_RC_Cgst")
            {
                setFocusControl(txtO_RC_Cgst);
            }
            if (strTextBox == "txtO_RC_Sgst")
            {
                setFocusControl(txtO_RC_Sgst);
            }
            if (strTextBox == "txtIrc_Igst")
            {
                setFocusControl(txtIrc_Igst);
            }
            if (strTextBox == "txtIrc_Cgst")
            {
                setFocusControl(txtIrc_Cgst);
            }
            if (strTextBox == "txtIrc_Sgst")
            {
                setFocusControl(txtIrc_Sgst);
            }
            if (strTextBox == "txtPurchase_Igst")
            {
                setFocusControl(txtPurchase_Igst);
            }
            if (strTextBox == "txtPurchase_Cgst")
            {
                setFocusControl(txtPurchase_Cgst);
            }
            if (strTextBox == "txtPuchase_Sgst")
            {
                setFocusControl(txtPuchase_Sgst);
            }
            if (strTextBox == "txtIn_RC_Igst")
            {
                setFocusControl(txtIn_RC_Igst);
            }
            if (strTextBox == "txtIn_RC_Cgst")
            {
                setFocusControl(txtIn_RC_Cgst);
            }
            if (strTextBox == "txtIn_RC_Sgst")
            {
                setFocusControl(txtIn_RC_Sgst);
            }
            if (strTextBox == "txtIntra_State_Supply")
            {
                setFocusControl(txtIntra_State_Supply);
            }

        }
        catch
        {
        }
    }

    #region calculation
    public void Calculation()
    {

        //txtIrc_Igst.Text = Convert.ToString(Convert.ToDouble(txtRC_IGST.Text) + Convert.ToDouble(txtO_RC_Igst.Text));
        //txtIrc_Cgst.Text = Convert.ToString(Convert.ToDouble(txtRC_CGCT.Text) + Convert.ToDouble(txtO_RC_Cgst.Text));
        //txtIrc_Sgst.Text = Convert.ToString(Convert.ToDouble(txtRC_Sgst.Text) + Convert.ToDouble(txtO_RC_Sgst.Text));

        txtIrc_Igst.Text = "0";
        txtIrc_Cgst.Text = "0";
        txtIrc_Sgst.Text = "0";
    }
    #endregion
    #endregion
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Doc_No=" + hdnf.Value + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
    #region [txtEdiDoc_No_TextChanged]
    protected void txtEdiDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEdiDoc_No.Text;
        strTextBox = "txtEdiDoc_No";
        csCalculations();
    }
    #endregion
    #region [txtDoc_No_TextChanged]
    //protected void btntxtShow_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = btntxtShow.Text;
    //    strTextBox = "txtShow";
    //    csCalculations();
    //    string excelFilePath = "D:xsl";
    //    System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);

    //    if (file.Exists)
    //    {
    //        Response.Clear();
    //        Response.AddHeader("Content-Disposition", "attachment; filename='" + tblDetails + "'");
    //        Response.AddHeader("Content-Length", file.Length.ToString());
    //        Response.ContentType = "application/octet-stream";
    //        Response.WriteFile(file.FullName);
    //        Response.End();
    //    }
    //}
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();
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
    #region [txtFrom_Date_TextChanged]
    protected void txtFrom_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFrom_Date.Text;
        strTextBox = "txtFrom_Date";
        csCalculations();
    }
    #endregion
    #region [txtTo_Date_TextChanged]
    protected void txtTo_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTo_Date.Text;
        strTextBox = "txtTo_Date";
        csCalculations();
    }
    #endregion
    #region [txtSale_Taxable_TextChanged]
    protected void txtSale_Taxable_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSale_Taxable.Text;
        strTextBox = "txtSale_Taxable";
        csCalculations();


    }
    #endregion
    #region [txtSale_Igst_TextChanged]
    protected void txtSale_Igst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSale_Igst.Text;
        strTextBox = "txtSale_Igst";
        csCalculations();
    }
    #endregion
    #region [txtSale_Cgst_TextChanged]
    protected void txtSale_Cgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSale_Cgst.Text;
        strTextBox = "txtSale_Cgst";
        csCalculations();
    }
    #endregion
    #region [txtSale_Sgst_TextChanged]
    protected void txtSale_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSale_Sgst.Text;
        strTextBox = "txtSale_Sgst";
        csCalculations();
    }
    #endregion
    #region [txtExport_Amount_TextChanged]
    protected void txtExport_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtExport_Amount.Text;
        strTextBox = "txtExport_Amount";
        csCalculations();
    }
    #endregion
    #region [txtRC_Taxable_TextChanged]
    protected void txtRC_Taxable_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRC_Taxable.Text;
        strTextBox = "txtRC_Taxable";
        csCalculations();
    }
    #endregion
    #region [txtRC_IGST_TextChanged]
    protected void txtRC_IGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRC_IGST.Text;
        strTextBox = "txtRC_IGST";
        csCalculations();
    }
    #endregion
    #region [txtRC_CGCT_TextChanged]
    protected void txtRC_CGCT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRC_CGCT.Text;
        strTextBox = "txtRC_CGCT";
        csCalculations();
    }
    #endregion
    #region [txtRC_Sgst_TextChanged]
    protected void txtRC_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRC_Sgst.Text;
        strTextBox = "txtRC_Sgst";
        csCalculations();
    }
    #endregion
    #region [txtO_RC_Taxable_TextChanged]
    protected void txtO_RC_Taxable_TextChanged(object sender, EventArgs e)
    {
        searchString = txtO_RC_Taxable.Text;
        strTextBox = "txtO_RC_Taxable";
        csCalculations();
    }
    #endregion
    #region [txtO_RC_Igst_TextChanged]
    protected void txtO_RC_Igst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtO_RC_Igst.Text;
        strTextBox = "txtO_RC_Igst";
        csCalculations();
        Calculation();
    }
    #endregion
    #region [txtO_RC_Cgst_TextChanged]
    protected void txtO_RC_Cgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtO_RC_Cgst.Text;
        strTextBox = "txtO_RC_Cgst";
        csCalculations();
    }
    #endregion
    #region [txtO_RC_Sgst_TextChanged]
    protected void txtO_RC_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtO_RC_Sgst.Text;
        strTextBox = "txtO_RC_Sgst";
        csCalculations();
    }
    #endregion
    #region [txtIrc_Igst_TextChanged]
    protected void txtIrc_Igst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIrc_Igst.Text;
        strTextBox = "txtIrc_Igst";
        csCalculations();
        Calculation();
    }
    #endregion
    #region [txtIrc_Cgst_TextChanged]
    protected void txtIrc_Cgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIrc_Cgst.Text;
        strTextBox = "txtIrc_Cgst";
        csCalculations();
        Calculation();
    }
    #endregion
    #region [txtIrc_Sgst_TextChanged]
    protected void txtIrc_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIrc_Sgst.Text;
        strTextBox = "txtIrc_Sgst";
        csCalculations();
        Calculation();
    }
    #endregion
    #region [txtPurchase_Igst_TextChanged]
    protected void txtPurchase_Igst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPurchase_Igst.Text;
        strTextBox = "txtPurchase_Igst";
        csCalculations();
    }
    #endregion
    #region [txtPurchase_Cgst_TextChanged]
    protected void txtPurchase_Cgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPurchase_Cgst.Text;
        strTextBox = "txtPurchase_Cgst";
        csCalculations();
    }
    #endregion
    #region [txtPuchase_Sgst_TextChanged]
    protected void txtPuchase_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPuchase_Sgst.Text;
        strTextBox = "txtPuchase_Sgst";
        csCalculations();
    }
    #endregion
    #region [txtIn_RC_Igst_TextChanged]
    protected void txtIn_RC_Igst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIn_RC_Igst.Text;
        strTextBox = "txtIn_RC_Igst";
        csCalculations();
    }
    #endregion
    protected void txtCNCTaxableAmount_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtCNCIgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtCNCCgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtCNCSgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtDNCTaxable_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtDNCIgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtDNCCgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtDNCSgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtPCNSTaxable_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtPCNSIgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtPCNSCgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtPCNSSgst_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtPCNSTaxableA_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtPRCNSIgst_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtPRCNSCgst_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtPRCNSSgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtotherInput_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtOIIgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtOICgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtOISgst_TextChanged(object sender, EventArgs e)
    {
    }




    #region [txtIn_RC_Cgst_TextChanged]
    protected void txtIn_RC_Cgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIn_RC_Cgst.Text;
        strTextBox = "txtIn_RC_Cgst";
        csCalculations();
    }
    #endregion
    #region [txtIn_RC_Sgst_TextChanged]
    protected void txtIn_RC_Sgst_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIn_RC_Sgst.Text;
        strTextBox = "txtIn_RC_Sgst";
        csCalculations();
    }
    #endregion
    #region [txtIntra_State_Supply_TextChanged]
    protected void txtIntra_State_Supply_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIntra_State_Supply.Text;
        strTextBox = "txtIntra_State_Supply";
        csCalculations();
    }
    #endregion

    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {


    }
    protected void txtretail_taxable_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtretailsaleIgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtretailsaleCgst_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtretailsaleSgst_TextChanged(object sender, EventArgs e)
    {
    }

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
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEdiDoc_No")
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

                    searchString = txtEdiDoc_No.Text;
                    strTextBox = "txtEdiDoc_No";
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
                        // name += "Doc_No Like '%" + aa + "%'or";

                        name += "( Doc_No like '%" + aa + "%' or Doc_Date like '%" + aa + "%' or From_Date like '%" + aa + "%'or Sale_Taxable like '%" + aa + "%')  and";

                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select Group--";
                    string qry = " select Doc_No,Doc_Date,From_Date,Sale_Taxable from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (" + name + ") order by Doc_No";
                    this.showPopup(qry);
                }
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
        child1.SetAttributeValue("EdiDoc_No", txtEdiDoc_No.Text != string.Empty ? txtEdiDoc_No.Text : "0");
        child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");
        child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd"));
        child1.SetAttributeValue("From_Date", DateTime.Parse(txtFrom_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd"));
        child1.SetAttributeValue("To_Date", DateTime.Parse(txtTo_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd"));
        child1.SetAttributeValue("Sale_Taxable", txtSale_Taxable.Text != string.Empty ? txtSale_Taxable.Text : "0.00");
        child1.SetAttributeValue("Sale_Igst", txtSale_Igst.Text != string.Empty ? txtSale_Igst.Text : "0.00");
        child1.SetAttributeValue("Sale_Cgst", txtSale_Cgst.Text != string.Empty ? txtSale_Cgst.Text : "0.00");
        child1.SetAttributeValue("Sale_Sgst", txtSale_Sgst.Text != string.Empty ? txtSale_Sgst.Text : "0.00");
        child1.SetAttributeValue("Export_Amount", txtExport_Amount.Text != string.Empty ? txtExport_Amount.Text : "0.00");
        child1.SetAttributeValue("RC_Taxable", txtRC_Taxable.Text != string.Empty ? txtRC_Taxable.Text : "0.00");
        child1.SetAttributeValue("RC_IGST", txtRC_IGST.Text != string.Empty ? txtRC_IGST.Text : "0.00");
        child1.SetAttributeValue("RC_CGCT", txtRC_CGCT.Text != string.Empty ? txtRC_CGCT.Text : "0.00");
        child1.SetAttributeValue("RC_Sgst", txtRC_Sgst.Text != string.Empty ? txtRC_Sgst.Text : "0.00");
        child1.SetAttributeValue("O_RC_Taxable", txtO_RC_Taxable.Text != string.Empty ? txtO_RC_Taxable.Text : "0.00");
        child1.SetAttributeValue("O_RC_Igst", txtO_RC_Igst.Text != string.Empty ? txtO_RC_Igst.Text : "0.00");
        child1.SetAttributeValue("O_RC_Cgst", txtO_RC_Cgst.Text != string.Empty ? txtO_RC_Cgst.Text : "0.00");
        child1.SetAttributeValue("O_RC_Sgst", txtO_RC_Sgst.Text != string.Empty ? txtO_RC_Sgst.Text : "0.00");
        child1.SetAttributeValue("Irc_Igst", txtIrc_Igst.Text != string.Empty ? txtIrc_Igst.Text : "0.00");
        child1.SetAttributeValue("Irc_Cgst", txtIrc_Cgst.Text != string.Empty ? txtIrc_Cgst.Text : "0.00");
        child1.SetAttributeValue("Irc_Sgst", txtIrc_Sgst.Text != string.Empty ? txtIrc_Sgst.Text : "0.00");
        child1.SetAttributeValue("Purchase_Igst", txtPurchase_Igst.Text != string.Empty ? txtPurchase_Igst.Text : "0.00");
        child1.SetAttributeValue("Purchase_Cgst", txtPurchase_Cgst.Text != string.Empty ? txtPurchase_Cgst.Text : "0.00");
        child1.SetAttributeValue("Puchase_Sgst", txtPuchase_Sgst.Text != string.Empty ? txtPuchase_Sgst.Text : "0.00");
        child1.SetAttributeValue("In_RC_Igst", txtIn_RC_Igst.Text != string.Empty ? txtIn_RC_Igst.Text : "0.00");
        child1.SetAttributeValue("In_RC_Cgst", txtIn_RC_Cgst.Text != string.Empty ? txtIn_RC_Cgst.Text : "0.00");
        child1.SetAttributeValue("In_RC_Sgst", txtIn_RC_Sgst.Text != string.Empty ? txtIn_RC_Sgst.Text : "0.00");
        child1.SetAttributeValue("Narration", TextBox1.Text != string.Empty ? TextBox1.Text : "");
        child1.SetAttributeValue("Intra_State_Supply", txtIntra_State_Supply.Text != string.Empty ? txtIntra_State_Supply.Text : "0.00");
        child1.SetAttributeValue("CNCtaxable", txtCNCTaxableAmount.Text != string.Empty ? txtCNCTaxableAmount.Text : "0.00");
        child1.SetAttributeValue("CNCIgst", txtCNCIgst.Text != string.Empty ? txtCNCIgst.Text : "0.00");
        child1.SetAttributeValue("CNCCgst", txtCNCCgst.Text != string.Empty ? txtCNCCgst.Text : "0.00");
        child1.SetAttributeValue("CNCSgst", txtCNCSgst.Text != string.Empty ? txtCNCSgst.Text : "0.00");

        child1.SetAttributeValue("DNCTaxable", txtDNCTaxable.Text != string.Empty ? txtDNCTaxable.Text : "0.00");
        child1.SetAttributeValue("DNCIgst", txtDNCIgst.Text != string.Empty ? txtDNCIgst.Text : "0.00");
        child1.SetAttributeValue("DNCCgst", txtDNCCgst.Text != string.Empty ? txtDNCCgst.Text : "0.00");
        child1.SetAttributeValue("DNCSgst", txtDNCSgst.Text != string.Empty ? txtDNCSgst.Text : "0.00");

        child1.SetAttributeValue("PCNStaxable", txtPCNSTaxable.Text != string.Empty ? txtPCNSTaxable.Text : "0.00");
        child1.SetAttributeValue("PCNSIgst", txtPCNSIgst.Text != string.Empty ? txtPCNSIgst.Text : "0.00");
        child1.SetAttributeValue("PCNSCgst", txtPCNSCgst.Text != string.Empty ? txtPCNSCgst.Text : "0.00");
        child1.SetAttributeValue("PCNSSgst", txtPCNSSgst.Text != string.Empty ? txtPCNSSgst.Text : "0.00");

        child1.SetAttributeValue("PRCNStaxable", txtPCNSTaxableA.Text != string.Empty ? txtPCNSTaxableA.Text : "0.00");
        child1.SetAttributeValue("PRCNSIgst", txtPRCNSIgst.Text != string.Empty ? txtPRCNSIgst.Text : "0.00");
        child1.SetAttributeValue("PRCNSCgst", txtPRCNSCgst.Text != string.Empty ? txtPRCNSCgst.Text : "0.00");
        child1.SetAttributeValue("PRCNSSgst", txtPRCNSSgst.Text != string.Empty ? txtPRCNSSgst.Text : "0.00");
        child1.SetAttributeValue("OtherInput", txtotherInput.Text != string.Empty ? txtotherInput.Text : "0.00");
        child1.SetAttributeValue("OIIgst", txtOIIgst.Text != string.Empty ? txtOIIgst.Text : "0.00");
        child1.SetAttributeValue("OICgst", txtOICgst.Text != string.Empty ? txtOICgst.Text : "0.00");
        child1.SetAttributeValue("OISgst", txtOISgst.Text != string.Empty ? txtOISgst.Text : "0.00");

        child1.SetAttributeValue("RSTaxable", txtretail_taxable.Text != string.Empty ? txtretail_taxable.Text : "0.00");
        child1.SetAttributeValue("RSIgst", txtretailsaleIgst.Text != string.Empty ? txtretailsaleIgst.Text : "0.00");
        child1.SetAttributeValue("RSCgst", txtretailsaleCgst.Text != string.Empty ? txtretailsaleCgst.Text : "0.00");
        child1.SetAttributeValue("RSSgst", txtretailsaleSgst.Text != string.Empty ? txtretailsaleSgst.Text : "0.00");

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
        string spname = "SP_Gstr3B";
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
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
        #endregion
    }
    #endregion
}

