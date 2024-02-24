    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeServiceBillTwo : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string SystemMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string trntype = "CB";
    string user = string.Empty;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;

    Int32 DOC_NO = 0;
    int doc_id = 0;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "ColdStorageBillHead";
            tblDetails = tblPrefix + "ColdStorageBillDetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            //cityMasterTable = tblPrefix + "CityMaster";
            //qryCommon = tblPrefix + "qryStorageBillForReport";
            qryCommon = "qryColdStorageHead";
            qryAccountList = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();

            if (!Page.IsPostBack)
            {

                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["csid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");

                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";

                        this.showLastRecord();

                        setFocusControl(btnEdit);
                    }
                    else
                    {
                        //btnAdd_Click(this, new EventArgs());
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtDOC_DATE);
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
    protected void btnCancleEInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                bool isValidated = true;
                if (txteinvoiceno.Text != string.Empty || txteinvoiceno.Text != "NA")
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Can't Cancle this Einvoice!!!!!');", true);
                    return;
                }
                string Irn = txteinvoiceno.Text;
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
                string SBNo = txtDOC_NO.Text;
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
                            qry1 = "update commission_bill set ACK='NA',Envoiceno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                               " and doc_no=" + txtDOC_NO.Text + " and Tran_type='CS'";
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

    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    hdnf.Value = txtEditDoc_No.Text;
        //    string qry = "select *,CONVERT(varchar(10),doc_date,103) as date from " + qryCommon + " where doc_no='" + txtEditDoc_No.Text + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' and Tran_Type='" + trntype + "'";
        //    this.fetchRecord(qry);
        //    setFocusControl(txtEditDoc_No);
        //    //pnlgrdDetail.Enabled = true;
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                txtSearchText.Text = txtEditDoc_No.Text.ToString();
                strTextBox = "txtDOC_NO";
                //  btntxtAC_CODE_Click(this, new EventArgs());
                btntxtDOC_NO_Click(this, new EventArgs());
                setFocusControl(txtSearchText);
            }
            else
            {
              //  hdnf.Value = txtEditDoc_No.Text; 
              //  string qry = "select * from " + qryCommon + " where doc_no='" + hdnf.Value
              //+ "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by csid";
              //  this.fetchRecord(qry);
              //  setFocusControl(txtEditDoc_No); 
                int id = Convert.ToInt32(clsCommon.getString("select csid from " + tblHead + " where doc_no=" + txtEditDoc_No.Text +
                  " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                  " " + Session["year"].ToString() + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:CSPen('" + id + "')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                                    txtDOC_NO.Text = ds.Tables[0].Rows[0][0].ToString();
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
                btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                #region logic
                LblPartyname.Text = "";
                //LBLMILLNAME.Text = "";
                //LBLBROKERNAME.Text = "";
                btntxtAC_CODE.Enabled = false;
                //btntxtMILL_CODE.Enabled = false;
                //btntxtITEM_CODE.Enabled = false;
                btntxtdoc_no.Enabled = false;
                drpCashCredit.Enabled = false;
                calenderExtenderDate.Enabled = false;
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;

                btnPrintSaleBill.Enabled = true;
                //lblUnitName.Text = "";
                txtEditDoc_No.Enabled = true;
                btntxtGSTRateCode.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                #endregion

                btntxtItem_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;

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

                txtEditDoc_No.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = false;
                txtDOC_NO.Enabled = false;
                #region set Business logic for save
                LblPartyname.Text = "";
                //LBLMILLNAME.Text = "";
                //LBLBROKERNAME.Text = "";
                btntxtAC_CODE.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                drpCashCredit.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtarrival_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtdelivered_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtender1.Enabled = true;
                CalendarExtender2.Enabled = true;

                btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                //btntxtUnitcode.Enabled = true;
                //lblUnitName.Text = "";
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                lblstatename.Text = "";
                lblGSTRateName.Text = "";
                #endregion
                btntxtItem_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
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

                txtEditDoc_No.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                btnPrintSaleBill.Enabled = true;
                drpCashCredit.Enabled = false;
                //txtDescription.Text = string.Empty;

                //btntxtUnitcode.Enabled = false;
                #region logic
                btntxtAC_CODE.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                txtITEM_CODE.Text = "";
                txtarrival_Date.Text = "";
                txtdelivered_date.Text = "";
                txtdays.Text = "";
                txtBags.Text = "";
                txtKg.Text = "";
                txtRate.Text = "";
                txtAmount.Text = "";
                txtlot_No.Text = "";
                txtAmount.Text = string.Empty;
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                //btntxtITEM_CODE.Enabled = false;
                calenderExtenderDate.Enabled = false;

                #endregion
                btntxtItem_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
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

                txtEditDoc_No.Enabled = false;
                txtDOC_NO.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                btntxtGSTRateCode.Enabled = true;
                lblMsg.Text = string.Empty;
                btnOpenDetailsPopup.Enabled = true;
                pnlgrdDetail.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                drpCashCredit.Enabled = true;
                //btntxtUnitcode.Enabled = true;
                CalendarExtender1.Enabled = true;
                CalendarExtender2.Enabled = true;
                #region logic
                btntxtAC_CODE.Enabled = true;
                txtarrival_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtdelivered_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                //btntxtITEM_CODE.Enabled = true;
                calenderExtenderDate.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;

                #endregion
                btntxtItem_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
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
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnAdd.Focus();
            }
            else                     //new code
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
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
        query = "select count(*) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
        if (txtDOC_NO.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    " ORDER BY doc_no asc  ";
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    " ORDER BY doc_no asc  ";
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")" +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
            if (txtDOC_NO.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY doc_no DESC  ";
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
            if (txtDOC_NO.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY doc_no asc  ";
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
            query = "select DOC_NO from " + tblHead + " where DOC_NO=(select MAX(DOC_NO) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
        this.makeEmptyForm("A");
       // this.getMaxCode();
        this.NextNumber();
        txtDOC_NO.Enabled = false;
        pnlPopupDetails.Style["display"] = "none";
        txtGSTRateCode.Text = "1";
        lblGSTRateName.Text = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGSTRateCode.Text
            + "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        setFocusControl(txtAC_CODE);




        //lblDONo.Text = "0";
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
        txtDOC_NO.Enabled = false;
        //txtITEM_CODE.Text = "1";
        //if (txtITEM_CODE.Text == "1")
        //{
        //    searchString = txtITEM_CODE.Text;
        //    strTextBox = "txtITEM_CODE";
        //    csCalculations();
        //}
        setFocusControl(txtAC_CODE);
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
        hdnf.Value = clsCommon.getString("select max(csid) from NT_1_ColdStorageBillHead where Company_Code=" + Session["Company_Code"].ToString() + " and " +
                 " Year_Code=" + Session["year"].ToString() + "");


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
                        txtDOC_NO.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnfdocno.Value = dt.Rows[0]["Doc_No"].ToString();
                        hdnfcoldid.Value = dt.Rows[0]["csid"].ToString();
                        hdnf.Value = dt.Rows[0]["csid"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["ConvertDate"].ToString(); //System.Globalization.CultureInfo.CreateSpecificCulture("en-BG")).ToString("dd/MM/yyyy");
                        drpCashCredit.SelectedValue = dt.Rows[0]["Cash_Credit"].ToString().Trim();
                        txtAC_CODE.Text = dt.Rows[0]["Customer_Code"].ToString();
                        hdnfcustomerid.Value = dt.Rows[0]["ccid"].ToString();
                        LblPartyname.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        txtstate_code.Text = dt.Rows[0]["CustomerStateCode"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();
                        if (txtstate_code.Text == string.Empty)
                        {
                            txtstate_code.Text = "0";
                        }
                        lblstatename.Text = dt.Rows[0]["CustomerStateName"].ToString();
                        txtGSTRateCode.Text = dt.Rows[0]["GSTRateCode"].ToString();
                        lblGSTRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtSubtotal.Text = dt.Rows[0]["Subtotal"].ToString();
                        txtCGSTRate.Text = dt.Rows[0]["CGSTRate"].ToString();
                        txtCGSTAmount.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGSTRate.Text = dt.Rows[0]["SGSTRate"].ToString();
                        txtSGSTAmount.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGSTRate.Text = dt.Rows[0]["IGSTRate"].ToString();
                        txtIGSTAmount.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtTotal.Text = dt.Rows[0]["Total"].ToString();
                        txtRoundOff.Text = dt.Rows[0]["Round_Off"].ToString();
                        txtBILL_AMOUNT.Text = dt.Rows[0]["Final_Amount"].ToString();
                        txtbillNo.Text = dt.Rows[0]["billno"].ToString();
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


                        if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txteinvoiceno.Text == "0" && txtackno.Text == "0")
                        {
                            btnGenEinvoice.Enabled = true;
                        }
                        else
                        {
                            btnGenEinvoice.Enabled = false;
                        }

                        #region  Details
                        qry = "select Detail_Id as ID,Item,ItemName,Brand,BrandName,Lot_No,Arrival_Date,Delivered_Date,Period,Bags,Weight,Rate,Wt_Per,Net_Wt," +
                            "Amount,'' as rowAction,'' as SrNo,csdetailid,itid,ltid,SaleAc,Saleid from qryColdStorageDetail where csid=" + hdnf.Value
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
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

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + qryCommon + " where csid=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                txtDOC_NO.Text = hdnf.Value;
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

        pnlPopupDetails.Style["display"] = "block";
        //txtPACKING.Text = "50";
        txtITEM_CODE.Text = "";
        //txtDescription.Text = "";
        //txtBAGS.Text = "";
        //txtAmount.Text = "";

        //txtITEM_NARRATION.Text = "";
        // LBLITEMNAME.Text = "";
        setFocusControl(txtAC_CODE);
        txtITEM_CODE.Text = "1";
        string itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
        ItemSelection(itemname, "1");
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
                if (dt.Rows[0]["ID"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
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
                        //rowIndex = dt.Rows.Count + 1;
                        dr["ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        // update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + txtDOC_NO.Text + "  And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
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
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Item", typeof(string))));
                    dt.Columns.Add((new DataColumn("ItemName", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand", typeof(int))));
                    dt.Columns.Add((new DataColumn("BrandName", typeof(string))));
                    dt.Columns.Add((new DataColumn("Lot_No", typeof(string))));
                    dt.Columns.Add((new DataColumn("Arrival_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Delivered_Date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Period", typeof(int))));
                    dt.Columns.Add((new DataColumn("Bags", typeof(int))));
                    dt.Columns.Add((new DataColumn("Weight", typeof(double))));
                    dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Wt_Per", typeof(double))));
                    dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("csdetailid", typeof(int))));
                    dt.Columns.Add((new DataColumn("itid", typeof(int))));
                    dt.Columns.Add((new DataColumn("ltid", typeof(int))));
                    dt.Columns.Add((new DataColumn("SaleAc", typeof(int))));
                    dt.Columns.Add((new DataColumn("Saleid", typeof(int))));
                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(Int32))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Item", typeof(string))));
                dt.Columns.Add((new DataColumn("ItemName", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand", typeof(int))));
                dt.Columns.Add((new DataColumn("BrandName", typeof(string))));
                dt.Columns.Add((new DataColumn("Lot_No", typeof(string))));
                dt.Columns.Add((new DataColumn("Arrival_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("Delivered_Date", typeof(string))));
                dt.Columns.Add((new DataColumn("Period", typeof(int))));
                dt.Columns.Add((new DataColumn("Bags", typeof(int))));
                dt.Columns.Add((new DataColumn("Weight", typeof(double))));
                dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Wt_Per", typeof(double))));
                dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add((new DataColumn("csdetailid", typeof(int))));
                dt.Columns.Add((new DataColumn("itid", typeof(int))));
                dt.Columns.Add((new DataColumn("ltid", typeof(int))));
                dt.Columns.Add((new DataColumn("SaleAc", typeof(int))));
                dt.Columns.Add((new DataColumn("Saleid", typeof(int))));

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]


            if (txtITEM_CODE.Text != string.Empty)
            {
                dr["Item"] = txtITEM_CODE.Text;

                qry = "select * from qrymstitem where Company_Code=" + Session["Company_Code"].ToString() + " and System_Code=" + txtITEM_CODE.Text + "";

                ds = clsDAL.SimpleQuery(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr["ItemName"] = ds.Tables[0].Rows[0]["System_Name_E"].ToString();
                    dr["itid"] = ds.Tables[0].Rows[0]["systemid"].ToString();
                    dr["SaleAc"] = ds.Tables[0].Rows[0]["Sale_AC"].ToString();
                    dr["Saleid"] = ds.Tables[0].Rows[0]["SaleAcid"].ToString();
                }
            }
            else
            {
                setFocusControl(txtITEM_CODE);
            }
            if (txtBrand_Code.Text != string.Empty)
            {
                dr["Brand"] = txtBrand_Code.Text;
                dr["BrandName"] = lblband_Name.Text;
            }
            else
            {
                dr["Brand"] = 0;
                dr["BrandName"] = "";
                //setFocusControl(txtBrand_Code);
            }
            //double IGSTAmount = txtIGSTAmount.Text != string.Empty ? Convert.ToDouble(txtIGSTAmount.Text) : 0.00;
            dr["Lot_No"] = txtlot_No.Text != string.Empty ? Convert.ToDouble(txtlot_No.Text) : 0.00;

            dr["ltid"] = lblLotId.Text != string.Empty ? Convert.ToDouble(lblLotId.Text) : 0.00;

            dr["Arrival_Date"] = txtarrival_Date.Text;
            dr["Delivered_Date"] = txtdelivered_date.Text;

            dr["Period"] = txtdays.Text != string.Empty ? Convert.ToDouble(txtdays.Text) : 0;
            dr["Bags"] = txtBags.Text != string.Empty ? Convert.ToDouble(txtBags.Text) : 0;
            if (txtarrival_Date.Text == string.Empty)
            {

                dr["Arrival_Date"] = System.DateTime.Now.ToString("dd/MM/yyyy");
            }

            dr["Rate"] = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0;

            dr["Weight"] = txtWeight.Text != string.Empty ? Convert.ToDouble(txtWeight.Text) : 0;

            dr["Wt_Per"] = txtwt_per.Text != string.Empty ? Convert.ToDouble(txtwt_per.Text) : 0;

            dr["Net_Wt"] = txtNet_wt.Text != string.Empty ? Convert.ToDouble(txtNet_wt.Text) : 0;

            if (txtAmount.Text != string.Empty)
            {
                dr["Amount"] = txtAmount.Text;
            }
            else
            {
                setFocusControl(txtAmount);
                return;
            }

            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["csdetailid"] = 0;
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
                pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtITEM_CODE);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->

            txtITEM_CODE.Text = string.Empty;
            lblitem_Name.Text = string.Empty;
            txtBrand_Code.Text = string.Empty;
            lblband_Name.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtwt_per.Text = string.Empty;
            txtNet_wt.Text = string.Empty;
            txtlot_No.Text = string.Empty;

            txtdays.Text = string.Empty;
            txtBags.Text = string.Empty;
            txtKg.Text = string.Empty;
            txtRate.Text = string.Empty;

            txtAmount.Text = string.Empty;
            //LBLITEMNAME.Text = string.Empty;
            //txtAmount.Text = string.Empty;
            //txtDescription.Text = string.Empty;
            pnlPopupDetails.Style["display"] = "none";
            Calculation();
            csCalculations();
            btnAdddetails.Text = "ADD";
            setFocusControl(btntxtItem_Code);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:Calculations()", true);
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

        txtITEM_CODE.Text = string.Empty;
        txtlot_No.Text = string.Empty;
        txtarrival_Date.Text = string.Empty;
        txtdelivered_date.Text = string.Empty;
        txtdays.Text = string.Empty;
        txtBags.Text = string.Empty;
        txtKg.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtAmount.Text = string.Empty;

        btnAdddetails.Text = "ADD";
        setFocusControl(txtITEM_CODE);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[18].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);

        txtITEM_CODE.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        lblitem_Name.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtBrand_Code.Text = Server.HtmlDecode(gvrow.Cells[5].Text);
        lblband_Name.Text = Server.HtmlDecode(gvrow.Cells[6].Text);
        txtlot_No.Text = Server.HtmlDecode(gvrow.Cells[7].Text);
        lblLotId.Text = Server.HtmlDecode(gvrow.Cells[21].Text);
        hdnfLotId.Value = Server.HtmlDecode(gvrow.Cells[21].Text);
        txtarrival_Date.Text = Server.HtmlDecode(gvrow.Cells[8].Text);
        txtdelivered_date.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        txtdays.Text = Server.HtmlDecode(gvrow.Cells[10].Text);
        txtBags.Text = Server.HtmlDecode(gvrow.Cells[11].Text);
        txtWeight.Text = Server.HtmlDecode(gvrow.Cells[12].Text);
        txtRate.Text = Server.HtmlDecode(gvrow.Cells[13].Text);
        txtwt_per.Text = Server.HtmlDecode(gvrow.Cells[14].Text);
        txtNet_wt.Text = Server.HtmlDecode(gvrow.Cells[15].Text);
        txtAmount.Text = Server.HtmlDecode(gvrow.Cells[16].Text);

        //LBLITEMNAME.Text = clsCommon.getString("Select System_Name_E from " + tblPrefix + "SystemMaster where System_Code=" + Server.HtmlDecode(gvrow.Cells[3].Text.ToString()) + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //txtITEM_NARRATION.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        // txtDescription.Text = Server.HtmlDecode(gvrow.Cells[5].Text);

        // txtAmount.Text = Server.HtmlDecode(gvrow.Cells[6].Text);

        //vat ac name

        pnlPopupDetails.Style["display"] = "none";
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["ID"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[17].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[17].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[17].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[17].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
                Calculation();
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v == "txtPURCNO")
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[1].Width = new Unit("100px");
                e.Row.Cells[2].Width = new Unit("300px");
                e.Row.Cells[3].Width = new Unit("300px");
                e.Row.Cells[4].Width = new Unit("120px");
                e.Row.Cells[5].Width = new Unit("120px");

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            }
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
                        if (grdDetail.Rows[rowindex].Cells[17].Text != "D" && grdDetail.Rows[rowindex].Cells[17].Text != "R")
                        {
                            pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
                            setFocusControl(txtITEM_CODE);
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

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            // {

            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;


            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[17].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[18].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[19].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[20].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[21].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[22].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[23].ControlStyle.Width = new Unit("50px");



            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;

            int i = 1;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                i++;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string s = cell.Text.ToString();
                    if (cell.Text.Length > 20)
                    {
                        cell.Text = cell.Text.Substring(0, 26) + "..";
                        cell.ToolTip = s;
                    }
                }

            }
            //}
        }
        catch
        {
        }
    }
    #endregion

    #region [txtDOC_NO_TextChanged]
    protected void txtDOC_NO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_NO.Text;
        strTextBox = "txtDOC_NO";
        csCalculations();
    }
    #endregion

    #region [btntxtDOC_NO_Click]
    protected void btntxtDOC_NO_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtEditDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion



    //#region [btntxtPURCNO_Click]
    //protected void btntxtPURCNO_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        pnlPopup.Style["display"] = "block";
    //        hdnfClosePopup.Value = "txtPURCNO";
    //        btnSearch_Click(sender, e);
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion
    #region[bill no text changed]
    protected void txtbillNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtbillNo.Text;
        strTextBox = "txtbillNo";
        csCalculations();
    }
    #endregion

    #region [txtDOC_DATE_TextChanged]
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_DATE.Text;
        strTextBox = "txtDOC_DATE";
        csCalculations();
    }
    #endregion

    #region [txtArrival_DATE_TextChanged]
    protected void txtArrival_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtarrival_Date.Text;
        strTextBox = "txtarrival_Date";
        DateTime d = DateTime.Parse(txtarrival_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime d2 = DateTime.Parse(txtdelivered_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        TimeSpan t = (d2 - d);
        double NrOfDays = t.TotalDays;
        NrOfDays = NrOfDays + 1;
        txtdays.Text = NrOfDays.ToString();
        csCalculations();
    }
    #endregion

    #region [txtDelivered_DATE_TextChanged]
    protected void txtDelivered_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdelivered_date.Text;
        strTextBox = "txtdelivered_date";

        csCalculations();
    }
    #endregion



    #region [txtAC_CODE_TextChanged]
    protected void txtAC_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAC_CODE.Text;
        strTextBox = "txtAC_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtAC_CODE_Click]
    protected void btntxtAC_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAC_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    protected void txtITEMCODE_OnTextChanged(object sender, EventArgs e)
    {
        try
        {

            searchString = txtITEM_CODE.Text;
            strTextBox = "txtITEM_CODE";
            csCalculations();
        }
        catch
        {
        }
    }




    #region [txtTotal_TextChanged]
    protected void txtTotal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotal.Text;
        strTextBox = "txtTotal";
        csCalculations();
    }
    #endregion




    #region [txtRoundOff_TextChanged]
    protected void txtRoundOff_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRoundOff.Text;
        strTextBox = "txtRoundOff";
        csCalculations();
    }
    #endregion



    #region [txtSubtotal_TextChanged]
    protected void txtSubtotal_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSubtotal.Text;
        strTextBox = "txtSubtotal";
        csCalculations();
    }
    #endregion

    #region [txtBILL_AMOUNT_TextChanged]
    protected void txtBILL_AMOUNT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBILL_AMOUNT.Text;
        strTextBox = "txtBILL_AMOUNT";
        csCalculations();
    }
    #endregion



    //#region [txtITEM_CODE_TextChanged]
    //protected void txtITEM_CODE_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = txtITEM_CODE.Text;
    //    strTextBox = "txtITEM_CODE";
    //    csCalculations();
    //}
    //#endregion

    //#region [btntxtITEM_CODE_Click]
    //protected void btntxtITEM_CODE_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        pnlPopup.Style["display"] = "block";
    //        hdnfClosePopup.Value = "txtITEM_CODE";
    //        btnSearch_Click(sender, e);
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion


    #region [btntxtMillCode_Click]
    protected void btntxtMillCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMillCode";
            btnSearch_Click(sender, e);
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
            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                setFocusControl(txtAC_CODE);
            }
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:validation();", true);
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            if (hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtdoc_no.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDOC_NO.Text = string.Empty;
                    txtDOC_NO.Enabled = true;

                    btnSave.Enabled = false;

                    hdnfClosePopup.Value = "Close";
                    setFocusControl(txtDOC_NO);
                    return;
                }

                if (btntxtdoc_no.Text == "Choose No")
                {
                    if (searchString != string.Empty)
                    {
                        txtSearchText.Text = txtSearchText.Text;
                    }
                    else
                    {
                        txtSearchText.Text = txtSearchText.Text;
                    }

                    lblPopupHead.Text = "--Select Account--";
                    string qry = "select distinct Doc_No,Customer_Code,CustomerName  from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and (Customer_Code like '%" + txtSearchText.Text + "%' or CustomerName like '%" + txtSearchText.Text + "%' ) order by CustomerName asc";
                    this.showPopup(qry);
                    setFocusControl(txtSearchText);
                    return;
                }
            }

            if (hdnfClosePopup.Value == "txtAC_CODE")
            {
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Code,Marka from Brand_Master where Mal_Code=" + txtITEM_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Code like '%" + txtSearchText.Text + "%' or Marka like '%" + txtSearchText.Text + "%') order by Code";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtITEM_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "";
                if (btnSave.Text == "Save")
                {
                    qry = "select Item_Code,System_Name_E,Brand_Code,brandname,No_Of_Bags,Wt_Per,Dispatched,Balance,Inword_No as ChallanNo,convert(varchar(10),Inword_Date,103) as Challan_Date ,csdetailid from qryColdStorageBalance where Balance <> 0 and Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (System_Name_E like '%" + txtSearchText.Text + "%') and Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " order by System_Name_E asc";
                }
                else
                {
                    //qry = "select Item_Code,System_Name_E,Brand_Code,brandname,No_Of_Bags,Wt_Per,Dispatched,Balance,Inword_No as ChallanNo,convert(varchar(10),Inword_Date,103) as Challan_Date ,csdetailid from qryColdStorageBalance where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (System_Name_E like '%" + txtSearchText.Text + "%') and Year_Code<=" + Convert.ToInt32(Session["year"].ToString() + " order by System_Name_E asc");
                    qry = "select Item_Code,System_Name_E,Brand_Code,brandname,No_Of_Bags,Wt_Per,Dispatched,Balance,Inword_No as ChallanNo,convert(varchar(10),Inword_Date,103) as Challan_Date ,csdetailid from qryColdStorageBalance where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (System_Name_E like '%" + txtSearchText.Text + "%')  and Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " order by System_Name_E asc";
                }
                this.showPopup(qry);


            }
            if (hdnfClosePopup.Value == "txtGSTRateCode")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name"; ;
                this.showPopup(qry);
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
            if (strTextBox == "txtDOC_NO")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtDOC_NO.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtDOC_NO.Text != string.Empty)
                        {
                            txtValue = txtDOC_NO.Text;
                            string qry = "select * from " + tblHead + " where  doc_no='" + txtValue + "' " +
                                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Record Found
                                        hdnf.Value = dt.Rows[0]["doc_no"].ToString();

                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                //txtDoc_no.Enabled = false;
                                                hdnf.Value = txtDOC_NO.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                //setFocusControl(txtPURCNO);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtDOC_NO.Enabled = false;
                                                    //setFocusControl(txtPURCNO);
                                                    pnlgrdDetail.Enabled = true;
                                                    hdnf.Value = txtDOC_NO.Text;

                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            //setFocusControl(txtPURCNO);
                                            txtDOC_NO.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtDOC_NO.Text = string.Empty;
                                            setFocusControl(txtDOC_NO);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtDOC_NO);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtDOC_NO.Text = string.Empty;
                        setFocusControl(txtDOC_NO);
                    }
                }
                catch
                {

                }
                #endregion
            }
            //if (strTextBox == "txtPURCNO")
            //{
            //    setFocusControl(txtDOC_DATE);
            //}
            if (strTextBox == "txtDOC_DATE")
            {
                try
                {
                    string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDate(dt) == true)
                    {
                        txtdelivered_date.Text = txtDOC_DATE.Text;
                        setFocusControl(txtAC_CODE);

                    }
                    else
                    {

                        txtDOC_DATE.Text = "";
                        setFocusControl(txtDOC_DATE);
                    }
                }
                catch
                {
                    txtDOC_DATE.Text = "";
                    setFocusControl(txtDOC_DATE);
                }

            }
            if (strTextBox == "txtarrival_Date")
            {
                if (txtarrival_Date.Text == "")
                {
                    setFocusControl(txtarrival_Date);
                    return;
                }
                else
                {
                    setFocusControl(txtdelivered_date);

                }
                //try
                //{
                //    string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                //    if (clsCommon.isValidDate(dt) == true)
                //    {
                //        setFocusControl(txtdelivered_date);
                //    }
                //    else
                //    {
                //        txtarrival_Date.Text = "";
                //        setFocusControl(txtarrival_Date);
                //    }
                //}
                //catch
                //{
                //    txtarrival_Date.Text = "";
                //    setFocusControl(txtarrival_Date);
                //}

            }
            if (strTextBox == "txtdelivered_date")
            {
                if (txtdelivered_date.Text == "")
                {
                    setFocusControl(txtdelivered_date);


                    return;
                }
                else
                {

                    //d = d.AddDays(-(DateTime);

                    setFocusControl(txtdays);
                }
                //try
                //{
                //    string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                //    if (clsCommon.isValidDate(dt) == true)
                //    {
                //        setFocusControl(txtdays);
                //    }
                //    else
                //    {
                //        txtdelivered_date.Text = "";
                //        setFocusControl(txtdelivered_date);
                //    }
                //}
                //catch
                //{
                //    txtdelivered_date.Text = "";
                //    setFocusControl(txtdelivered_date);
                //}

            }


            if (strTextBox == "txtAC_CODE")
            {
                string acname = "";
                string statecode = "";
                string statename = "";
                if (txtAC_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAC_CODE.Text);
                    if (a == false)
                    {
                        btntxtAC_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        statecode = clsCommon.getString("select GSTStateCode from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        statename = clsCommon.getString("select State_Name from GSTStateMaster where State_Code='" + statecode + "'");
                        if (acname != string.Empty)
                        {
                            LblPartyname.Text = acname;
                            txtstate_code.Text = statecode != string.Empty ? statecode : "0";
                            lblstatename.Text = statename;
                            hdnfcustomerid.Value = clsCommon.getString("select accoid from " + AccountMasterTable + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            setFocusControl(txtGSTRateCode);
                        }
                        else
                        {
                            txtAC_CODE.Text = string.Empty;
                            LblPartyname.Text = acname;
                            txtstate_code.Text = statecode;
                            lblstatename.Text = statename;
                            setFocusControl(txtAC_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAC_CODE);
                }
            }

            if (strTextBox == "txtITEM_CODE")
            {
                string itemname = "";
                if (txtITEM_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtITEM_CODE.Text);
                    if (a == false)
                    {
                        btntxtItem_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        itemname = clsCommon.getString("select System_Name_E from qryColdStorageBalance where Item_Code=" + txtITEM_CODE.Text + " and Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code<=" + Convert.ToInt32(Session["year"].ToString()) + " ");
                        if (itemname != string.Empty)
                        {
                            lblitem_Name.Text = itemname;
                            lblLotId.Text = hdnfLotId.Value;
                            setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            txtITEM_CODE.Text = string.Empty;
                            lblitem_Name.Text = string.Empty;
                            setFocusControl(txtITEM_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtITEM_CODE);
                }
            }
            if (strTextBox == "txtBrand_Code")
            {
                string itemname = "";
                if (txtBrand_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBrand_Code.Text);
                    if (a == false)
                    {
                        btntxtBrand_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        itemname = clsCommon.getString("select Marka from Brand_Master where Mal_Code=" + txtITEM_CODE.Text + " and Code=" + txtBrand_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ");
                        if (itemname != string.Empty && itemname != "0")
                        {
                            lblband_Name.Text = itemname;
                            setFocusControl(txtarrival_Date);
                        }
                        else
                        {
                            txtBrand_Code.Text = string.Empty;
                            lblband_Name.Text = string.Empty;
                            setFocusControl(txtBrand_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBrand_Code);
                }
            }
            if (strTextBox == "txtGSTRateCode")
            {
                string gstname = "";
                if (txtGSTRateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGSTRateCode.Text);
                    if (a == false)
                    {
                        btntxtGSTRateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGSTRateCode.Text + "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (gstname != string.Empty)
                        {
                            lblGSTRateName.Text = gstname;
                            // string trntype = "C";
                            //GSTCalc();
                            setFocusControl(txtbillNo);

                        }
                        else
                        {
                            txtGSTRateCode.Text = string.Empty;
                            lblGSTRateName.Text = gstname;
                            setFocusControl(txtGSTRateCode);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtGSTRateCode);
                }
            }


            if (strTextBox == "txtLCST")
            {
                //  this.CSTCalculation();
                setFocusControl(txtSGSTRate);

            }
            //if (strTextBox == "txtRate")
            //{
            //    double rate = Convert.ToDouble(txtRate.Text);
            //    if (rate <= 0)
            //    {
            //        setFocusControl(txtRate);
            //        return;
            //    }
            //    else
            //    {
            //        setFocusControl(txtAmount);
            //    }

            //}

            if (strTextBox == "txtIGSTRate")
            {
                //this.IGSTRateCalculation();
                setFocusControl(txtIGSTRate);
            }

            if (strTextBox == "txtSGSTRate")
            {
                //this.SGSTRateCalculation();
                setFocusControl(txtIGSTRate);
            }
            if (strTextBox == "txtRoundOff")
            {
                setFocusControl(btnSave);

            }
            //if (strTextBox == "txtKg")
            //{
            //    double kg = Convert.ToDouble(txtKg.Text);
            //    if (kg <= 0)
            //    {
            //        setFocusControl(txtKg);
            //        return;
            //    }
            //    else
            //    {
            //        setFocusControl(txtRate);
            //    }


            //}
            if (strTextBox == "txtbillNo")
            {
                //setFocusControl(lblitem_Name);
            }
            if (strTextBox == "txtITEMCODE")
            {
                //setFocusControl(txtlot_No);
            }
            if (strTextBox == "txtlot_No")
            {
                //setFocusControl(txtarrival_Date);
            }
            if (strTextBox == "txtdays")
            {
                //setFocusControl(txtBags);
            }
            if (strTextBox == "txtBags")
            {
                //setFocusControl(txtAmount);
            }
            if (strTextBox == "txtAmount")
            {
                setFocusControl(btnAdddetails);
            }
            Calculation();
        }
        catch
        {
        }
    }
    #region

    #endregion
    private void Calculation()
    {
        #region calculation part
        double subtotal = 0.00;

        double CGSTAmount = Convert.ToDouble("0" + txtCGSTAmount.Text);
        double SGSTAmount = Convert.ToDouble("0" + txtSGSTAmount.Text);
        double IGSTAmount = Convert.ToDouble("0" + txtIGSTAmount.Text);

        double roundoff = txtRoundOff.Text != string.Empty ? Convert.ToDouble(txtRoundOff.Text) : 0.00;
        double billamount = Convert.ToDouble("0" + txtBILL_AMOUNT.Text);


        //string itemName = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
        //if (itemName != string.Empty)
        //{
        //    string kgPerKatta = clsCommon.getString("select ISNULL(KgPerKatta,100) as KgPerKatta from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
        //    kgperk = Convert.ToDouble(kgPerKatta);
        //}

        //   double kg = qtl * kgperk;
        // double quintal = qtl;

        //item_Amount = Math.Round((quintal * (rate)), 2);



        //  txtVatTotal.Text = vatTotal.ToString();

        //        txtITEMAMOUNT.Text = item_Amount.ToString();

        //      txtGross.Text = (item_Amount + vatTotal).ToString();
        #region[amount calculation]
        //double amountcal = 0.00;
        //double kg = txtKg.Text != string.Empty ? Convert.ToDouble(txtKg.Text) : 0.0;
        //double rate = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0.0;
        //amountcal = kg * rate;
        //txtAmount.Text = Convert.ToString(amountcal);
        //if (amountcal == 0.00)
        //{
        //    txtAmount.Text = "";
        //}

        #endregion

        #region calculate subtotal
        if (grdDetail.Rows.Count > 0)
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[17].Text != "R" && grdDetail.Rows[i].Cells[17].Text != "D")
                {
                    double item_Amt = Convert.ToDouble(grdDetail.Rows[i].Cells[16].Text.Trim());
                    subtotal = subtotal + item_Amt;
                }
                //double gross = Convert.ToDouble(grdDetail.Rows[i].Cells[16].Text.Trim());
                //grossAmount = grossAmount + gross;
            }

            txtSubtotal.Text = subtotal.ToString();
            //txtVatAmount.Text = vatAmount.ToString();
            //txtBILL_AMOUNT.Text = grossAmount.ToString();
        }
        #endregion

        double cgstRate = txtCGSTRate.Text != string.Empty ? Convert.ToDouble(txtCGSTRate.Text) : 0.0;
        double sgstRate = txtSGSTRate.Text != string.Empty ? Convert.ToDouble(txtSGSTRate.Text) : 0.0;
        double igstRate = txtIGSTRate.Text != string.Empty ? Convert.ToDouble(txtIGSTRate.Text) : 0.0;
        string GSTRateCode = txtGSTRateCode.Text;

        double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
        double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
        double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
        double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));

        int partystatecode = txtstate_code.Text != string.Empty ? Convert.ToInt32(txtstate_code.Text) : 0;
        double cgstAmountOnBillAmount = 0;
        double sgstAmountOnBillAmount = 0;
        double igstAmountOnBillAmount = 0;
        //if (drpCashCredit.SelectedValue == "CR")
        //{
        #region gstcalculation
        if (txtAC_CODE.Text != string.Empty)
        {
            bool asd = clsCommon.isStringIsNumeric(txtAC_CODE.Text);
            if (asd)
            {
                if (txtAC_CODE.Text != "1")
                {
                    int CompanyGSTStateCode = Convert.ToInt32(clsCommon.getString("select GSTStateCode from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));

                    //int partystatecode = Convert.ToInt32(clsCommon.getString("select ISNULL(GSTStateCode,0) from " + tblPrefix + "AccountMaster where Ac_Code=" + txtAC_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
                    if (partystatecode != 0)
                    {
                        if (CompanyGSTStateCode == partystatecode)
                        {
                            txtCGSTRate.Text = cgstrate.ToString();
                            txtSGSTRate.Text = sgstrate.ToString();
                            txtIGSTRate.Text = "0";
                            cgstAmountOnBillAmount = Math.Round(((subtotal * cgstrate) / 100), 2);
                            sgstAmountOnBillAmount = Math.Round(((subtotal * sgstrate) / 100), 2);
                        }
                        else
                        {
                            igstAmountOnBillAmount = Math.Round(((subtotal * igstrate) / 100), 2);
                            txtCGSTRate.Text = "0";
                            txtSGSTRate.Text = "0";
                            txtIGSTRate.Text = igstrate.ToString();
                        }
                    }
                    else
                    {
                        txtCGSTRate.Text = "0";
                        txtSGSTRate.Text = "0";
                        txtIGSTRate.Text = igstrate.ToString();
                        igstAmountOnBillAmount = Math.Round(((subtotal * igstrate) / 100), 2);
                    }
                }
                else
                {
                    txtCGSTRate.Text = cgstrate.ToString();
                    txtSGSTRate.Text = sgstrate.ToString();
                    txtIGSTRate.Text = "0";
                    cgstAmountOnBillAmount = Math.Round(((subtotal * cgstrate) / 100), 2);
                    sgstAmountOnBillAmount = Math.Round(((subtotal * sgstrate) / 100), 2);
                }
            }
        }
        #endregion
        //}
        txtCGSTAmount.Text = cgstAmountOnBillAmount.ToString();
        txtSGSTAmount.Text = sgstAmountOnBillAmount.ToString();
        txtIGSTAmount.Text = igstAmountOnBillAmount.ToString();
        txtTotal.Text = Math.Round(subtotal + cgstAmountOnBillAmount + sgstAmountOnBillAmount + igstAmountOnBillAmount, 2).ToString();
        double total = Convert.ToDouble(txtTotal.Text);
        double subtotal1 = Convert.ToDouble(txtSubtotal.Text != string.Empty ? txtSubtotal.Text : "0.00");
         
            double totalgstamt = Math.Round(subtotal + cgstAmountOnBillAmount + sgstAmountOnBillAmount + igstAmountOnBillAmount);
            roundoff = Math.Round(totalgstamt - (subtotal + cgstAmountOnBillAmount + sgstAmountOnBillAmount + igstAmountOnBillAmount), 2);
            txtRoundOff.Text = roundoff.ToString();
            txtBILL_AMOUNT.Text = Math.Round(total + roundoff, 2).ToString();
        
        //subtotal = Math.Round(grossAmount + cgstAmountOnBillAmount + sgstAmountOnBillAmount + igstAmountOnBillAmount, 2);
        //txtSubtotal.Text = subtotal.ToString();
        //freight = Math.Round((lessfreight * netQntl), 2);
        //txtBILL_AMOUNT.Text = subtotal.ToString();

        #endregion
    }

    private void ItemSelection(string itemname, string itemCode)
    {
        //LBLITEMNAME.Text = itemname;
        //string qryGetVatAc = "select Vat_AC from " + tblPrefix + "SystemMaster where System_Code=" + itemCode + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
        //string vatAc_Code = clsCommon.getString(qryGetVatAc);
        //if (vatAc_Code != "0")
        //{
        //    txtVatAc.Text = vatAc_Code;
        //    string qryVatAcDetail = "select Ac_Name_E from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + vatAc_Code + "";
        //    string qryVatRate = "select Ac_rate from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + vatAc_Code + "";
        //    txtVatPercent.Text = clsCommon.getString(qryVatRate);
        //    lblVatAcName.Text = clsCommon.getString(qryVatAcDetail);
        //}
    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }

    protected void txtCGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGSTRate.Text;
        strTextBox = "txtLCST";
        csCalculations();

    }

    protected void txtKg_TextChanged(object sender, EventArgs e)
    {
        searchString = txtKg.Text;
        strTextBox = "txtKg";
        csCalculations();

    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        csCalculations();

    }

    protected void txtSGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGSTRate.Text;
        strTextBox = "txtSGSTRate";
        csCalculations();

    }
    protected void txtIGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGSTRate.Text;
        strTextBox = "txtIGSTRate";
        csCalculations();

    }
    protected void txtGSTRateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGSTRateCode.Text;
        strTextBox = "txtGSTRateCode";
        csCalculations();

    }
    protected void txtlotNo_OnTextChanged(object sender, EventArgs e)
    {
        searchString = txtlot_No.Text;
        strTextBox = "txtlot_No";
        // csCalculations();

    }

    protected void txtdays_OnTextChanged(object sender, EventArgs e)
    {
        searchString = txtdays.Text;
        strTextBox = "txtdays";
        csCalculations();
    }
    protected void txtBags_OnTextChanged(object sender, EventArgs e)
    {
        searchString = txtBags.Text;
        strTextBox = "txtBags";
        csCalculations();
    }

    protected void txtAmount_OnTextChanged(object sender, EventArgs e)
    {
        searchString = txtAmount.Text;
        strTextBox = "txtAmount";
        csCalculations();
    }

    protected void btntxtGSTRateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGSTRateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtWeight_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtrate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtwt_per_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtNet_wt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btntxtItem_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtITEM_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtBrand_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBrand_Code.Text;
        strTextBox = "txtBrand_Code";
        csCalculations();
    }
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
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select isnull(max(Doc_No),0) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["Year"].ToString() + "'")) + 1;
            //if (counts == 0)
            //{
            txtDOC_NO.Text = counts.ToString();
            DOC_NO = counts;
            //}


            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(csid),0) as rbid from " + tblHead + " ")) + 1;
            //if (counts == 0)
            //{
            lbldocid.Text = counts.ToString();
            doc_id = counts;
            //}



        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion

}