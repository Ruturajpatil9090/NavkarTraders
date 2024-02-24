using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.Services;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

public partial class Sugar_BussinessRelated_pgeDoInformation : System.Web.UI.Page
{
    #region data section
    string temp = "0";

    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string SystemMastertable = string.Empty;
    string qryCommon = string.Empty;
    string qryHead = string.Empty;
    string qryDetail = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string qrycarporateSalebalance = string.Empty;
    string qryUTRBalance = string.Empty;
    string qrypurc_No = string.Empty;
    string qryAccountList = string.Empty;
    string millShortName = string.Empty;
    int defaultAccountCode = 0;
    string trnType = "DO";
    string AUTO_VOUCHER = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    public static int an = 0;
    double LvAmnt = 0.00;
    string Action = string.Empty;
    string cs = string.Empty;
    int DOC_NO = 0;
    int doid = 0;
    double OldGSTAmt = 0.00;
    double OldSaleAmt = 0.00;
    double OldCommission = 0.00;
    double OldBillAmt;
    double OldQty = 0.00;

    #endregion
    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    DataTable dt1 = null;
    DataTable dt2 = null;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;
    DataSet DS = null;

    StringBuilder Head_Update = null;
    string Head_Insert = string.Empty;
    string Head_Delete = string.Empty;
    StringBuilder Head_Values = null;
    StringBuilder Head_Fields = null;
    string Purchase_Delete = string.Empty;
    string Sale_Delete = string.Empty;
    string Detail_Insert = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;
    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    int flag = 0;
    string msg = string.Empty;
    string SelfBal = string.Empty;

    static string donumber = string.Empty;
    #region -Head part declearation
    //Int32 DOC_NO = 0;
    Int32 DO_CODE = 0;
    string DOC_DATE = string.Empty;
    string MILLNAME = string.Empty;
    string BILLTO = string.Empty;
    string SHIPTO = string.Empty;
    double QUANTAL = 0.00;
    string GRADE = string.Empty;
    string TRUCK_NO = string.Empty;
    string MAILSEND = string.Empty;
    string EINVOICE = string.Empty;
    string PAYMENT = string.Empty;
    string NARRATION1 = string.Empty;

    string userinfo = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    int Company_Code = 0;
    int Year_Code = 0;
    int Branch_Code = 0;

    int? docd = 0;

    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "deliveryorder";
            //tblDetails = tblPrefix + "DODetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMastertable = tblPrefix + "SystemMaster";
            qryCommon = tblPrefix + "qryDeliveryOrderList";
            qryHead = "qrydohead";
            //qryDetail = "qrydodetail";
            pnlPopup.Style["display"] = "none";
            GLedgerTable = tblPrefix + "GLEDGER";
            qrycarporateSalebalance = tblPrefix + "qryCarporatesellbalance";
            qryUTRBalance = tblPrefix + "qryUTRBalance";
            qryAccountList = "qrymstaccountmaster";
            qrypurc_No = "qrysugarBalancestock";
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            Maindt = new DataTable();
            dr = null;

            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            DS = new DataSet();
            if (!Page.IsPostBack)
            {
                //txtSearchText.Attributes.Add("onkeypress", "abc(event);");
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    //hdnvouchernumber.Value = "0";
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["DO"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");

                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        setFocusControl(btnUpdate);
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        btntxtDOC_NO.Enabled = false;
                        setFocusControl(txtDOC_DATE);
                    }
                    //txtFromDate.Text = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //txtToDate.Text = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
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

    //protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

    //        if (a == false)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Only Numbers Allow!')", true);
    //        }
    //        else
    //        {
    //            string qry = "select * from " + qryCommon + " where doc_no='" + txtEditDoc_No.Text + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Session["year"].ToString() + "";
    //            this.fetchRecord(qry);
    //            setFocusControl(txtEditDoc_No);

    //        }
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;

            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

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
                                    txtdoc_no.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtdoc_no.Enabled = false;
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

                //btnSave.Text = "Save";
                btnUpdate.Text = "Update";
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                lblMsg.Text = string.Empty;

                #region set Business logic
                txtDOC_DATE.Text = string.Empty;
                txtmillname.Text = string.Empty;
                //txtEditDoc_No.Enabled = true;
                txtbillto.Enabled = false;
                txtshipto.Enabled = false;
                txtqntl.Enabled = false;
                txtvehicle.Enabled = true;
                drpmailsend.Enabled = true;
                drpeinvoice.Enabled = true;
                drppayment.Enabled = true;
                txtnarration.Enabled = true;
                calenderExtenderDate.Enabled = false;

                #endregion
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
                //btnSave.Text = "Save";
                btntxtDOC_NO.Text = "Change No";
                btntxtDOC_NO.Enabled = true;
                txtdoc_no.Enabled = false;
                lblMsg.Text = "";
                //txtEditDoc_No.Enabled = false;
                btntxtDOC_NO.Enabled = false;
                #region set Business logic for save
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                //txtDOC_DATE.Text = string.Empty;
                txtmillname.Text = string.Empty;
                //txtEditDoc_No.Enabled = true;
                txtbillto.Enabled = false;
                txtshipto.Enabled = false;
                txtqntl.Enabled = false;
                txtvehicle.Enabled = true;
                drpmailsend.Enabled = true;
                drpeinvoice.Enabled = true;
                drppayment.Enabled = true;
                txtnarration.Enabled = true;
                calenderExtenderDate.Enabled = true;

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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                //txtEditDoc_No.Enabled = true;
                #region set Business logic for save
                txtDOC_DATE.Text = string.Empty;
                txtmillname.Text = string.Empty;
                //txtEditDoc_No.Enabled = true;
                txtbillto.Enabled = false;
                txtshipto.Enabled = false;
                txtqntl.Enabled = false;
                txtvehicle.Enabled = true;
                drpmailsend.Enabled = true;
                drpeinvoice.Enabled = true;
                drppayment.Enabled = true;
                txtnarration.Enabled = true;
                calenderExtenderDate.Enabled = false;
                #endregion
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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = true;
                lblMsg.Text = string.Empty;
                #region logic
                txtDOC_DATE.Text = string.Empty;
                txtmillname.Text = string.Empty;
                //txtEditDoc_No.Enabled = true;
                txtbillto.Enabled = false;
                txtshipto.Enabled = false;
                txtqntl.Enabled = false;
                txtvehicle.Enabled = true;
                drpmailsend.Enabled = true;
                drpeinvoice.Enabled = true;
                drppayment.Enabled = true;
                txtnarration.Enabled = true;
                calenderExtenderDate.Enabled = true;
                #endregion
                //txtEditDoc_No.Enabled = false;
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
                btnUpdate.Focus();
            }
            else                            //new code
            {
                //btnEdit.Enabled = false;
                //btnDelete.Enabled = false;
            }


        }

        catch
        {
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
                        hdnf.Value = dt.Rows[0]["doid"].ToString();
                        hdnfDoid.Value = dt.Rows[0]["doid"].ToString();
                        txtdoc_no.Text = dt.Rows[0]["doc_no"].ToString();
                        hdnfDodoc.Value = dt.Rows[0]["DOC_NO"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtmillname.Text = dt.Rows[0]["millshortname"].ToString();
                        txtbillto.Text = dt.Rows[0]["billtoshortname"].ToString();
                        txtshipto.Text = dt.Rows[0]["VoucherByname"].ToString();
                        txtqntl.Text = dt.Rows[0]["quantal"].ToString();
                        txtgrade.Text = dt.Rows[0]["GRADE"].ToString();
                        txtvehicle.Text = dt.Rows[0]["truck_no"].ToString();

                        drpmailsend.Text = dt.Rows[0]["MailSend"].ToString();
                        drpeinvoice.Text = dt.Rows[0]["ISEInvoice"].ToString();
                        drppayment.Text = dt.Rows[0]["IsPayment"].ToString();
                        txtnarration.Text = dt.Rows[0]["narration1"].ToString();

                        //string ischeckedTDSCut = dt.Rows[0]["TDSCut"].ToString();
                        //if (ischeckedTDSCut == "Y")
                        //{
                        //    chkTDSCutByUs.Checked = true;


                        //}
                        //else
                        //{
                        //    chkTDSCutByUs.Checked = false;


                        //}

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
            string qryDisplay = "select * from " + qryHead + "  where doid='" + hdnf.Value + "' ";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();
    }
    #endregion

    #region [btntxtDOC_NO_Click]
    protected void btntxtDOC_NO_Click(object sender, EventArgs e)
    {
        try
        {
            if (btntxtDOC_NO.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtdoc_no.Text = string.Empty;
                txtdoc_no.Enabled = true;
                //btnSave.Enabled = false;
                setFocusControl(txtdoc_no);
            }
            if (btntxtDOC_NO.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtdoc_no";
                btnSearch_Click(sender, e);

            }
        }
        catch
        {
        }
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

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {

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

    #region csCalculations
    private void csCalculations()
    {

    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }

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
        }
        catch
        {

        }
    }
    #endregion

    #region Generate Next Number
    private void NextNumber()
    {
        //try
        //{
        //    int counts = 0;
        //    counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
        //        " and Year_Code='" + Session["year"].ToString() + "'"));
        //    if (counts == 0)
        //    {
        //        txtdoc_no.Text = "1";
        //        Doc_No = 1;
        //    }
        //    else
        //    {
        //        Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
        //            " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
        //        txtdoc_no.Text = Doc_No.ToString();
        //    }

        //}
        //catch
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);

        //}
    }
    #endregion

    #region [btnUpdate_Click]
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            //DateTime dEnd = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:pagevalidation()", true);
        }

        catch (Exception exxx)
        {
            DOPurcSaleCRUD.LogError(exxx);
            btnUpdate.Enabled = true;
            setFocusControl(btnUpdate);
            con.Close();

        }
        finally
        {
            con.Close();
        }

    }
    #endregion

    protected void drpmailsend_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(txtDOC_DATE);
        }
        catch (Exception)
        {
        }
    }
    protected void drpeinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(txtDOC_DATE);
        }
        catch (Exception)
        {
        }
    }
    protected void drppayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(txtDOC_DATE);
        }
        catch (Exception)
        {
        }
    }

}