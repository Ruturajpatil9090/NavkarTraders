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

public partial class Sugar_BussinessRelated_pgeGoodsTransport : System.Web.UI.Page
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
            tblDetails = "nt_1_goodstransport";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMastertable = tblPrefix + "SystemMaster";
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
                        setFocusControl(txtdate);
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        setFocusControl(txtdo_date);
                    }
                    //txtFromDate.Text = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //txtToDate.Text = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
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
                                    txtdo_no.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtdo_no.Enabled = false;
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
              
                #region set Business logic
                txtdo_date.Text = string.Empty;
                txtdo_no.Text = string.Empty;
                txtlifting_date.Enabled = false;
                txtdriver_no.Enabled = false;
                txtvehicle_no.Enabled = false;
                txtmill_name.Enabled = false;
                txtgrade.Enabled = false;
                txtquantal.Enabled = false;
                txtfreight.Enabled = false;
                txtadvance.Enabled = false;
                txttransport_name.Enabled = false;
                txtDo_name.Enabled = false;
                txtsalebill_No.Enabled = false;
                txtgtnarration.Enabled = true;
                drpreached.Enabled = true;
                txtreached_date.Enabled = true;
                txtdate.Enabled = true;
                txtStation.Enabled = true;
                txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtreached_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
               
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
                #region set Business logic
                //btnSave.Text = "Save";
                txtdo_date.Text = string.Empty;
                txtdo_no.Text = string.Empty;
                txtlifting_date.Enabled = false;
                txtdriver_no.Enabled = false;
                txtvehicle_no.Enabled = false;
                txtmill_name.Enabled = false;
                txtgrade.Enabled = false;
                txtquantal.Enabled = false;
                txtfreight.Enabled = false;
                txtadvance.Enabled = false;
                txttransport_name.Enabled = false;
                txtDo_name.Enabled = false;
                txtsalebill_No.Enabled = false;
                txtgtnarration.Enabled = true;
                drpreached.Enabled = true;
                txtreached_date.Enabled = true;
                txtdate.Enabled = true;
                txtStation.Enabled = true;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
               
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
                #region set Business logic
                txtdo_date.Text = string.Empty;
                txtdo_no.Text = string.Empty;
                txtlifting_date.Enabled = false;
                txtdriver_no.Enabled = false;
                txtvehicle_no.Enabled = false;
                txtmill_name.Enabled = false;
                txtgrade.Enabled = false;
                txtquantal.Enabled = false;
                txtfreight.Enabled = false;
                txtadvance.Enabled = false;
                txttransport_name.Enabled = false;
                txtDo_name.Enabled = false;
                txtsalebill_No.Enabled = false;
                txtgtnarration.Enabled = true;
                drpreached.Enabled = true;
                txtreached_date.Enabled = false;
              
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
                #region logic
               txtdo_date.Text = string.Empty;
                txtdo_no.Text = string.Empty;
                txtlifting_date.Enabled = false;
                txtdriver_no.Enabled = false;
                txtvehicle_no.Enabled = false;
                txtmill_name.Enabled = false;
                txtgrade.Enabled = false;
                txtquantal.Enabled = false;
                txtfreight.Enabled = false;
                txtadvance.Enabled = false;
                txttransport_name.Enabled = false;
                txtDo_name.Enabled = false;
                txtsalebill_No.Enabled = false;
                txtgtnarration.Enabled = true;
                drpreached.Enabled = true;
                txtreached_date.Enabled = true;
                txtdate.Enabled = true;
                txtStation.Enabled = true;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
               
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
                        txtdo_no.Text = dt.Rows[0]["doc_no"].ToString();
                        hdnfDodoc.Value = dt.Rows[0]["Doc_No"].ToString();
                        
                        txtdo_date.Text = ds.Tables[0].Rows[0]["Do_Date_Conv"].ToString();
                        txtlifting_date.Text = ds.Tables[0].Rows[0]["doc_dateConverted"].ToString();
                        txtdriver_no.Text = ds.Tables[0].Rows[0]["driver_no"].ToString();
                        txtvehicle_no.Text = ds.Tables[0].Rows[0]["truck_no"].ToString();
                        txtmill_name.Text = ds.Tables[0].Rows[0]["millshortname"].ToString();
                        txtgrade.Text = ds.Tables[0].Rows[0]["grade"].ToString();
                        txtquantal.Text = ds.Tables[0].Rows[0]["quantal"].ToString();
                        txtbill_to_name.Text = ds.Tables[0].Rows[0]["billtoshortname"].ToString();
                        txtfreight.Text = ds.Tables[0].Rows[0]["FreightPerQtl"].ToString();
                        txtadvance.Text = ds.Tables[0].Rows[0]["Memo_Advance"].ToString();
                        txttransport_name.Text = ds.Tables[0].Rows[0]["transportshortname"].ToString();
                        drpreached.Text = ds.Tables[0].Rows[0]["vehicle_reached"].ToString();
                        txtreached_date.Text = ds.Tables[0].Rows[0]["reached_dateConv"].ToString();
                        txtDo_name.Text = ds.Tables[0].Rows[0]["DOName"].ToString();
                        txtsalebill_No.Text = ds.Tables[0].Rows[0]["SB_No"].ToString();
                        txtgtnarration.Text = ds.Tables[0].Rows[0]["GT_Remark"].ToString();
                        

                        #region  Details

                        qry = "select DetailID as ID,convert(varchar(10),Run_Date,103) as date,Station,Do_ID,GoodsID,'' as rowAction,'' as SrNo from " + tblDetails + " where Do_ID=" + hdnf.Value;
                        
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

                        var date = "";
                        if (grdDetail.Rows[0].Cells[3].Text == "")
                        {
                            date = "0";
                        }
                        else
                        {
                            date = grdDetail.Rows[0].Cells[3].Text;
                        }
                        //pnlgrdDetail.Enabled = false;
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
            string qryDisplay = "select * from " + qryHead + "  where doid='" + hdnf.Value + "' ";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [txtdo_no_TextChanged]
    protected void txtdo_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdo_no.Text;
        strTextBox = "txtdo_no";
        csCalculations();
    }
    #endregion

 
    #region [txtdate_TextChanged]
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdate.Text;
        strTextBox = "txtdate";
        csCalculations();
    }
    #endregion

    #region[txtreached_date_TextChanged]
    protected void txtreached_date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtreached_date.Text;
        strTextBox = "txtreached_date";
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
                        //hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                      //  hdHelpPageCount.Value = "0";
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
           // hdnfClosePopup.Value = "Close";
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
        if (strTextBox == "txtdate")
        {
            try
            {
                string dt = DateTime.Parse(txtdate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                //DateTime startDate = DateTime.Parse(clsGV.Start_Date);
                //TimeSpan oneDay = new TimeSpan(864000000000);
                //DateTime dayBeforeStartDate = startDate.Subtract(oneDay);
                if (clsCommon.isValidDate(dt) == true || dt == "31/03/2017")
                {
                    setFocusControl(txtStation);
                }
                else
                {
                    txtdate.Text = "";
                    setFocusControl(txtdate);
                }
            }
            catch
            {
                txtdate.Text = "";
                setFocusControl(txtdate);
            }
        }
        if (strTextBox == "txtreached_date")
        {
            try
            {
                string dt = DateTime.Parse(txtreached_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                //DateTime startDate = DateTime.Parse(clsGV.Start_Date);
                //TimeSpan oneDay = new TimeSpan(864000000000);
                //DateTime dayBeforeStartDate = startDate.Subtract(oneDay);
                if (clsCommon.isValidDate(dt) == true || dt == "31/03/2017")
                {
                    setFocusControl(btnUpdate);
                }
                else
                {
                    txtreached_date.Text = "";
                    setFocusControl(txtreached_date);
                }
            }
            catch
            {
                txtreached_date.Text = "";
                setFocusControl(txtreached_date);
            }
        }
          
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
        //        txtdo_no.Text = "1";
        //        Doc_No = 1;
        //    }
        //    else
        //    {
        //        Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
        //            " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
        //        txtdo_no.Text = Doc_No.ToString();
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

    protected void lnkDo_Click(object sender, EventArgs e)
    {
        Int32 sbno = txtdo_no.Text != string.Empty ? Convert.ToInt32(txtdo_no.Text) : 0;

        if (sbno != 0)
        {
            string DO = clsCommon.getString("select doid from nt_1_deliveryorder where doc_no=" + sbno + " and Company_Code=" + Session["Company_Code"].ToString() + " and " +
                "  Year_Code=" + Session["year"].ToString() + "");
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:DO();", true);
            string Action = "1";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DO('" + Action + "','" + DO + "')", true);
        }
    }

    protected void drpreached_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(txtreached_date);
        }
        catch (Exception)
        {
        }
    }
  
  
    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtdo_no.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    //btnEdit.Enabled = true;
                    //btnEdit.Focus();
                }
                // this.enableDisableNavigateButtons();
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
        // pnlPopupDetails.Style["display"] = "block";
        txtdate.Text = "";
        txtStation.Text = "";
        setFocusControl(txtdate);
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtdate.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtdate);
                return;
            }

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
                        //     rowIndex = dt.Rows.Count + 1;
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
                        string id = clsCommon.getString("select DetailID from " + tblDetails + " where Doc_No=" + txtdo_no.Text + " and Detail_ID=" + lblID.Text + " And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
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
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));

                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("date", typeof(string))));
                    dt.Columns.Add((new DataColumn("Station", typeof(string))));
                    dt.Columns.Add((new DataColumn("GoodsID", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(int))));

                #region [Write here columns]
                dt.Columns.Add((new DataColumn("date", typeof(string))));
                dt.Columns.Add((new DataColumn("Station", typeof(string))));
                dt.Columns.Add((new DataColumn("GoodsID", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
              
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["date"] = txtdate.Text;
            dr["Station"] = txtStation.Text;
           
            if (txtdate.Text != string.Empty)
            {
                dr["date"] = txtdate.Text;
            }
            else
            {
                setFocusControl(txtdate);
            }
            if (txtStation.Text != string.Empty)
            {
                dr["Station"] = txtStation.Text;
            }
            else
            {
                setFocusControl(txtStation);
            }
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["GoodsID"] = 0;
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
                // pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtdate);
            }
            else
            {
                // pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtdate);
                // btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            var date = "";
            if (grdDetail.Rows[0].Cells[3].Text == "")
            {
                date = "0";
            }
            else
            {
                date = grdDetail.Rows[0].Cells[3].Text;
            }
            txtdate.Text = "";
            txtStation.Text = "";
            setFocusControl(txtdate);
            btnAdddetails.Text = "ADD";
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        txtdate.Text = string.Empty;
        txtStation.Text = string.Empty;
        btnAdddetails.Text = string.Empty;
        btnClosedetails.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        btnClosedetails.Text = "Close";
        setFocusControl(txtdate);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[8].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);
        txtdate.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
       
        txtStation.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["GoodsID"].ToString());
                string IDExisting = clsCommon.getString("select DetailID from " + tblDetails + " where GoodsID=" + ID + " ");
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[6].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[6].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[6].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[6].Text = "A";
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


    #region [RowgrdPopup_RowCreatedCreated]
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
                        if (grdDetail.Rows[rowindex].Cells[6].Text != "D" && grdDetail.Rows[rowindex].Cells[6].Text != "R")
                        {
                            // pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
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
        csCalculations();
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("100px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("100px");
            //e.Row.Cells[10].Visible = false;

            e.Row.Cells[12].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("60px");
            int i = 1;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                i++;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string s = cell.Text.ToString();
                    if (cell.Text.Length > 20)
                    {
                        cell.Text = cell.Text.Substring(0, 31) + "..";
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

 
}