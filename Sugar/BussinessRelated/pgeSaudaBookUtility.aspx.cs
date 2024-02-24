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

public partial class Sugar_BussinessRelated_pgeSaudaBookUtility : System.Web.UI.Page
{
    public DataSet ds = null;
    public DataTable dt = null;

    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
    string cs = string.Empty;
    string Action = string.Empty;
    string retValue;
    string fornotsaverecord;
    string str = string.Empty;
    string AccountMasterTable = string.Empty;
    #endregion
    #region updatepart

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    DataTable dt1 = null;
    DataTable dt2 = null;

    DataTable SalePurcdt = null;


    DataTable Maindt = null;
    DataRow dr = null;
    string msg = string.Empty;
    int Branch_Code = 0;
    Int32 mill_Code;
    Int32 ShipTo;
    Int32 Tender_No = 0;
    Int32 party = 0;
    string DeliveryType = string.Empty;
    Int32 broker = 0;
    int count = 0;
    Int32 subbroker = 0;
    Int32 Doc_No = 0;
    double quntal = 0.00;
    string saudadate = string.Empty;
    double sale_rate = 0.00;
    double commision_rate = 0.00;
    string payment_Date = string.Empty;
    int tenderID = 0;
    int tenderdetailid = 0;
    int shiptoid = 0;
    double TenderQuntl = 0.00;
    int partyid = 0;
    int brokerid = 0;
    int subbrokerid = 0;
    string dtype = string.Empty;
    string Delivery_Type = "";
    string Narration = "";
    double tcs_rate = 0.00;
    double gst_rate = 0.00;
    double tcs_amt = 0.00;
    double gst_amt = 0.00;
    double cashdiff = 0.00;
    #endregion


    string Head_Insert = string.Empty;
    StringBuilder Head_Update = null;
    string Head_Delete = string.Empty;
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    string Update_Detail = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        tblHead = "nt_1_tenderdetails";
        AccountMasterTable = "qrymstaccountmaster";
        Head_Fields = new StringBuilder();
        Head_Values = new StringBuilder();
        Head_Update = new StringBuilder();

        Maindt = new DataTable();
        dr = null;
        Maindt.Columns.Add("Querys", typeof(string));
        dr = Maindt.NewRow();
        if (!Page.IsPostBack)
        {
            pnlPopup.Style["display"] = "none";
            //txtDetailLiftingDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDetailSaudaDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
            {
                pnlPopup.Style["display"] = "none";
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                objAsp = btnSearch;
            }
            setFocusControl(txtmillcode);
        }
    }
    #region[txtmillcode_TextChanged]
    protected void txtmillcode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtmillcode.Text;
        strTextBox = "txtmillcode";
        csCalculations();

    }
    #endregion

    #region [btntxtmillcode_Click]
    protected void btntxtmillcode_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfpopup.Value == "")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtmillcode";
                btnSearch_Click(sender, e);
            }
            else
            {
                pnlPopup.Style["display"] = "none";
                hdnfpopup.Value = null;

            }

        }
        catch
        {
        }
    }
    #endregion


    #region[txtBP_Account_TextChanged]
    protected void txtBP_Account_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtBP_Account.Text;
            //searchString = txtBANK_CODE.Text;
            strTextBox = "txtBP_Account";
            string BP_Account = string.Empty;
            if (txtBP_Account.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtBP_Account.Text);
                if (a == false)
                {
                    btnBP_Account_Click(this, new EventArgs());
                }
                else
                {
                    BP_Account = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='" + txtBP_Account.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (BP_Account != string.Empty && BP_Account != "0")
                    {
                        //hdnfbp.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtBP_Account.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        //if (BP_Account.Length > 15)
                        //{
                        //    BP_Account.Substring(0, 15);
                        //}
                        //else if (BP_Account.Length > 10)
                        //{
                        //    BP_Account.Substring(0, 10);
                        //}
                        lblBP_Account.Text = BP_Account;
                        //AmtCalculation();
                        setFocusControl(txtcashdifference);
                    }
                    else
                    {
                        txtBP_Account.Text = string.Empty;
                        lblBP_Account.Text = string.Empty;
                        setFocusControl(txtBP_Account);
                        // AmtCalculation();
                    }
                }
            }
            else
            {
                txtBP_Account.Text = string.Empty;
                lblBP_Account.Text = BP_Account;
                setFocusControl(txtcashdifference);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region[btnBP_Account_Click]
    protected void btnBP_Account_Click(object sender, EventArgs e)
    {
        try
        {

            hdnfClosePopup.Value = "txtBP_Account";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }

    }
    #endregion

    #region [btnUpdate_Click]
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtcashdifference.Text != "" && txtcashdifference.Text != "0" && txtcashdifference.Text != "0.00")
        {

            if (txtBP_Account.Text == "" || txtBP_Account.Text == "2")
            {
                setFocusControl(txtBP_Account);
                return;

            }

        }
        //btnUpdate.Enabled = false;
        #region Assign Values
        fornotsaverecord = retValue;
        retValue = string.Empty;

        //string qry = "";
        str = string.Empty;

        bool isvalidate = true;

        #region validation
        if (txtmillcode.Text != string.Empty && txtmillcode.Text != "0")
        {
            isvalidate = true;
        }
        else
        {
            txtmillcode.Text = string.Empty;
            setFocusControl(txtmillcode);
            return;
        }
        if (txttender.Text != string.Empty && txttender.Text != "0")
        {
            isvalidate = true;
        }
        else
        {
            txttender.Text = string.Empty;
            setFocusControl(txttender);
            return;
        }
        if (txtBuyerQuantal.Text != string.Empty && txtBuyerQuantal.Text != "0.00")
        {
            isvalidate = true;
        }
        else
        {
            txtBuyerQuantal.Text = string.Empty;
            setFocusControl(txtBuyerQuantal);
            return;
        }
        if (txtBuyerSaleRate.Text != string.Empty && txtBuyerSaleRate.Text != "0.00")
        {
            isvalidate = true;
        }
        else
        {
            txtBuyerSaleRate.Text = string.Empty;
            setFocusControl(txtBuyerSaleRate);
            return;
        }
        #endregion

        int Year_Code = Convert.ToInt32(Session["year"].ToString());
        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
        int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
        //txtTCSrate1.Text = Session["TCS_Rate"].ToString();
        // int CompanyStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());

        mill_Code = txtmillcode.Text != string.Empty ? Convert.ToInt32(txtmillcode.Text) : 0;
        ShipTo = txtShipTo.Text != string.Empty ? Convert.ToInt32(txtShipTo.Text) : 0;
        Int32 Tender_No = txttender.Text != string.Empty ? Convert.ToInt32(txttender.Text) : 0;
        Int32 party = txtBuyer.Text != string.Empty ? Convert.ToInt32(txtBuyer.Text) : 0;
        quntal = txtBuyerQuantal.Text != string.Empty ? Convert.ToDouble(txtBuyerQuantal.Text) : 0.00;
        sale_rate = txtBuyerSaleRate.Text != string.Empty ? Convert.ToDouble(txtBuyerSaleRate.Text) : 0.00;
        commision_rate = txtBuyerCommission.Text != string.Empty ? Convert.ToDouble(txtBuyerCommission.Text) : 0.00;
        //saudadate = txtDetailSaudaDate.Text;
        saudadate = DateTime.Parse(txtDetailSaudaDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        //payment_Date = txtDetailLiftingDate.Text;
        payment_Date = DateTime.Parse(txtDetailLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        Narration = txtBuyerNarration.Text;
        Int32 broker = txtBuyerParty.Text != string.Empty ? Convert.ToInt32(txtBuyerParty.Text) : 0;
        Int32 subbroker = txtsubBroker.Text != string.Empty ? Convert.ToInt32(txtsubBroker.Text) : 0;
        Delivery_Type = drpDeliveryType.Text;
        tcs_rate = txtTCSrate1.Text != string.Empty ? Convert.ToDouble(txtTCSrate1.Text) : 0.00;
        gst_rate = txtGSTrate.Text != string.Empty ? Convert.ToDouble(txtGSTrate.Text) : 0.00;
        tcs_amt = txtTCSamount1.Text != string.Empty ? Convert.ToDouble(txtTCSamount1.Text) : 0.00;
        gst_amt = txtgstamt.Text != string.Empty ? Convert.ToDouble(txtgstamt.Text) : 0.00;
        cashdiff = txtcashdifference.Text != string.Empty ? Convert.ToDouble(txtcashdifference.Text) : 0.00;


        string loadingby = "";
        if (chkLoding_Chk.Checked == true)
        {
            loadingby = "Y";
        }
        else
        {
            loadingby = "N";
        }


        Int32 bpac = txtBP_Account.Text != string.Empty ? Convert.ToInt32(txtBP_Account.Text) : 0;
        Int32 bpid = 0;

        try
        {
            bpid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + bpac + " and company_code='" + Session["Company_Code"].ToString() + "'"));
        }
        catch
        {
            bpid = 0;
        }
        try
        {
            tenderID = Convert.ToInt32(clsCommon.getString("SELECT tenderdetailid from nt_1_tenderdetails where Tender_No=" + Tender_No +
                " and Company_Code=" + Session["Company_Code"].ToString()));
            //"' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString()));
        }
        catch
        {

        }
        try
        {
            partyid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + party + " and company_code='" + Session["Company_Code"].ToString() + "'"));
        }
        catch
        {

        }

        try
        {
            brokerid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + broker + " and company_code='" + Session["Company_Code"].ToString() + "'"));
        }
        catch
        {
            brokerid = 0;
        }

        try
        {
            shiptoid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + ShipTo + " and company_code='" + Session["Company_Code"].ToString() + "'"));
        }
        catch
        {

        }

        try
        {
            subbrokerid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + subbroker + " and company_code='" + Session["Company_Code"].ToString() + "'"));
        }
        catch
        {

        }

        string Created_By = Session["user"].ToString();
        int AutoID = 0;
        int flag = 0;
        string op = string.Empty;
        string returnmaxno = string.Empty;
        #endregion

        btnUpdate.Enabled = false;

        #region[head master]

        string isactive = "true";
        if (btnUpdate.Text == "Update")
        {
            this.NextNumber();

            TenderQuntl = Convert.ToDouble(clsCommon.getString("SELECT Buyer_Quantal from nt_1_tenderdetails where tenderdetailid="
                + tenderdetailid + " and Company_Code=" + Session["Company_Code"].ToString()));
            //    "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString()));
            TenderQuntl = TenderQuntl - quntal;

            #region Head Add Fields And Values
            Head_Fields.Append("Tender_No,");
            Head_Values.Append("'" + Tender_No + "',");
            Head_Fields.Append("Company_Code,");
            Head_Values.Append("'" + Company_Code + "',");
            Head_Fields.Append("Buyer,");
            Head_Values.Append("'" + party + "',");
            Head_Fields.Append("Buyer_Quantal,");
            Head_Values.Append("'" + quntal + "',");
            Head_Fields.Append("Sale_Rate,");
            Head_Values.Append("'" + sale_rate + "',");
            Head_Fields.Append("Commission_Rate,");
            Head_Values.Append("'" + commision_rate + "',");
            Head_Fields.Append("Sauda_Date,");
            Head_Values.Append("'" + saudadate + "',");
            Head_Fields.Append("Lifting_Date,");
            Head_Values.Append("'" + payment_Date + "',");
            Head_Fields.Append("Narration,");
            Head_Values.Append("'" + Narration + "',");
            Head_Fields.Append("ID,");
            Head_Values.Append("'" + count + "',");
            Head_Fields.Append("Buyer_Party,");
            Head_Values.Append("'" + broker + "',");
            Head_Fields.Append("sub_broker,");
            Head_Values.Append("'" + subbroker + "',");
            Head_Fields.Append("year_code,");
            Head_Values.Append("'" + Year_Code + "',");
            Head_Fields.Append("Delivery_Type,");
            Head_Values.Append("'" + Delivery_Type + "',");
            Head_Fields.Append("tenderid,");
            Head_Values.Append("'" + tenderID + "',");
            //Head_Fields.Append("tenderdetailid,");
            //Head_Values.Append("'" + Doc_No + "',");
            Head_Fields.Append("buyerid,");
            Head_Values.Append("'" + partyid + "',");
            Head_Fields.Append("buyerpartyid,");
            Head_Values.Append("'" + brokerid + "',");
            Head_Fields.Append("tcs_rate,");
            Head_Values.Append("'" + tcs_rate + "',");
            Head_Fields.Append("gst_rate,");
            Head_Values.Append("'" + gst_rate + "',");
            Head_Fields.Append("tcs_amt,");
            Head_Values.Append("'" + tcs_amt + "',");
            Head_Fields.Append("gst_amt,");
            Head_Values.Append("'" + gst_amt + "',");
            Head_Fields.Append("branch_id,");
            Head_Values.Append("'" + Branch_Code + "',");
            Head_Fields.Append("ShipTo,");
            Head_Values.Append("'" + ShipTo + "',");
            Head_Fields.Append("shiptoid,");
            Head_Values.Append("'" + shiptoid + "',");
            Head_Fields.Append("CashDiff,");
            Head_Values.Append("'" + cashdiff + "',");
            Head_Fields.Append("sbr,");
            Head_Values.Append("'" + subbrokerid + "',");
            Head_Fields.Append("loding_by_us,");
            Head_Values.Append("'" + loadingby + "',");

            Head_Fields.Append("bpid,");
            Head_Values.Append("'" + bpid + "',");

            Head_Fields.Append("BP_Detail");
            Head_Values.Append("'"+bpac+"'");


            #endregion

            Head_Insert = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";
            dr = null;
            dr = Maindt.NewRow();
            dr["Querys"] = Head_Insert;
            Maindt.Rows.Add(dr);

            Update_Detail = "Update " + tblHead + " set Buyer_Quantal='" + TenderQuntl + "' where tenderdetailid ='" + tenderdetailid + "'";
            dr = null;
            dr = Maindt.NewRow();
            dr["Querys"] = Update_Detail;
            Maindt.Rows.Add(dr);
            flag = 1;

        }
        else
        {


        }

        #endregion
        msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);

        string tenderdetailidnew = clsCommon.getString(" select IDENT_CURRENT('nt_1_tenderdetails')");

        string massage = "Mill_Name:" + lblmillname.Text.Trim() + ",Grade:" + lblgrade.Text.Trim() + ",Mill_Rate:" + lblmillrate.Text.Trim() +
        ",Purc_rate:" + lblpurcrate.Text.Trim() + ",Bill_To:" + lblBuyerName.Text.Trim() + ",Qty:" + txtBuyerQuantal.Text.Trim() +
        ",Sale_rate:" + txtBuyerSaleRate.Text.Trim() + " This Record Inserted Successfully!!!! TenderDetialID=" + tenderdetailidnew;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('" + (massage + "')"), true);

        if (msg == "Insert")
        {
            txtmillcode.Text = string.Empty;
            txttender.Text = string.Empty;
            txtBuyer.Text = string.Empty;
            txtShipTo.Text = string.Empty;
            txtBuyerParty.Text = string.Empty;
            txtsubBroker.Text = string.Empty;
            txtBuyerQuantal.Text = string.Empty;
            txtBuyerSaleRate.Text = string.Empty;
            txtcashdifference.Text = string.Empty;
            txtBuyerCommission.Text = string.Empty;
            txtDetailSaudaDate.Text = string.Empty;
            txtDetailLiftingDate.Text = string.Empty;
            txtBuyerNarration.Text = string.Empty;
            txtTCSrate1.Text = string.Empty;
            txtGSTrate.Text = string.Empty;
            txtTCSamount1.Text = string.Empty;
            txtgstamt.Text = string.Empty;
            lblmillname.Text = string.Empty;
            lblselfqty.Text = string.Empty;
            lblmillrate.Text = string.Empty;
            lblgrade.Text = string.Empty;
            lbllifting.Text = string.Empty;
            lblBuyerName.Text = string.Empty;
            lblbuyercity.Text = string.Empty;
            lblShiptoname.Text = string.Empty;
            lblshiptocity.Text = string.Empty;
            lblBuyerPartyName.Text = string.Empty;
            lblsubBroker.Text = string.Empty;
            lblcashdifferencevalue.Text = string.Empty;
            lblNetAmt.Text = string.Empty;
            lblseason.Text = string.Empty;
            lblpurcrate.Text = string.Empty;
            txtBP_Account.Text = string.Empty;
            lblBP_Account.Text = string.Empty;
            chkLoding_Chk.Checked = false;
            setFocusControl(txtmillcode);
            btnUpdate.Enabled = true;

            drpDeliveryType.SelectedValue = "C";
        }

    }


    #endregion

    #region Generate Next Number
    private void
        NextNumber()
    {
        try
        {
            int counts = 0;

            Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(tenderdetailid) as Tender_No from nt_1_tenderdetails")) + 1;
            //counts = Convert.ToInt32(clsCommon.getString("SELECT max(ID) as ID from nt_1_tenderdetails where Company_Code='" 
            //    + Session["Company_Code"].ToString() + "' " +
            //    " and year_code='" + Session["year"].ToString() + "' and Tender_No=" + txttender.Text + " ")) + 1;

            counts = Convert.ToInt32(clsCommon.getString("SELECT max(ID) as ID from nt_1_tenderdetails where Company_Code='"
               + Session["Company_Code"].ToString() + "' and Tender_No=" + txttender.Text + " ")) + 1;


            count = counts;
            //tenderID = Convert.ToInt32(clsCommon.getString("SELECT tenderid as tenderid from nt_1_tender where Company_Code='" 
            //    + Session["Company_Code"].ToString() + "' " +
            //    " and year_code='" + Session["year"].ToString() + "' and Tender_No=" + txttender.Text + " "));

            tenderID = Convert.ToInt32(clsCommon.getString("SELECT tenderid as tenderid from nt_1_tender where Company_Code='"
               + Session["Company_Code"].ToString() + "' and Tender_No=" + txttender.Text + " "));

            //tenderdetailid = Convert.ToInt32(clsCommon.getString("SELECT tenderdetailid from nt_1_tenderdetails where Tender_No=" + txttender.Text + " and Company_Code='" + Session["Company_Code"].ToString() + "' " +
            //    " and year_code='" + Session["year"].ToString() + "' and Buyer='2' "));

            tenderdetailid = Convert.ToInt32(clsCommon.getString("SELECT tenderdetailid from nt_1_tenderdetails where Tender_No=" + txttender.Text +
                " and Company_Code='" + Session["Company_Code"].ToString() + "' and Buyer='2' "));
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "txtmillcode")
            {
                setFocusControl(txtmillcode);
            }
            if (hdnfClosePopup.Value == "BP")
            {
                setFocusControl(txtBuyerParty);
            }
            if (hdnfClosePopup.Value == "BU")
            {
                setFocusControl(txtBuyer);
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
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType != DataControlRowType.Pager)
            {

                e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[3].ControlStyle.Width = new Unit("200px");
                e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
                e.Row.Cells[5].ControlStyle.Width = new Unit("50px");
                e.Row.Cells[6].ControlStyle.Width = new Unit("50px");
                e.Row.Cells[7].ControlStyle.Width = new Unit("40px");
                e.Row.Cells[8].ControlStyle.Width = new Unit("50px");
                e.Row.Cells[9].ControlStyle.Width = new Unit("40px");
            }

            if (v == "BU" || v == "BP" || v == "txtShipTo")
            {
                //if (e.Row.RowType != DataControlRowType.Pager)
                //{
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(50);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(20);
                // }
            }
            if (v == "txtBP_Account")
            {
                //if (e.Row.RowType != DataControlRowType.Pager)
                //{
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(50);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(20);
               // e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(20);
                // }
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

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //searchString = "";
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
            if (hdnfClosePopup.Value == "txtmillcode")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Tender_No like '%" + aa + "%' or millname like '%" + aa + "%' or millshortname like '%" + aa + "%' or Lifting_Date like '%" + aa + "%' or tenderdoname like '%" + aa + "%' or Grade like '%" + aa + "%'or Mill_Rate like '%"
                         + aa + "%' or balance like '%" + aa + "%' or Quantal like '%" + aa + "%' or tenderid like '%" + aa + "%' or Mill_Code like '%" + aa + "%' or mc like '%" + aa + "%' or season like '%" + aa + "%' or season like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Mill code--";
                string qry = "select Tender_No,millname,millshortname,convert(varchar(10),Lifting_Date,103) as Lifting_Date,tenderdoname,Grade,Mill_Rate,balance,Quantal,tenderid,Mill_Code,mc,season,Purc_Rate " +
                    " ,ID from qrytenderdobalanceview where Buyer=2 and balance !=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name + " ";
                // + " and Year_code=" + Session["year"].ToString() + " " +

                this.showPopup(qry);

            }

            if (hdnfClosePopup.Value == "BU")
            {
                //  txtSearchText.Text = txtBuyer.Text;
                lblPopupHead.Text = "--Select Buyer--";
                string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                            + " FROM  AccountMaster "
                            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" +
                            txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%"
                            + txtSearchText.Text + "%') order by Ac_Name_E asc";
                qry = qry.Replace("AccountMaster", AccountMasterTable);
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtShipTo")
            {
                //  txtSearchText.Text = txtBuyer.Text;
                lblPopupHead.Text = "--Select ShipTo--";
                string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                            + " FROM  AccountMaster "
                            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" +
                            txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%"
+ txtSearchText.Text + "%') order by Ac_Name_E asc";
                qry = qry.Replace("AccountMaster", AccountMasterTable);
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "BP")
            {
                ////txtSearchText.Text = txtBuyerParty.Text;
                //lblPopupHead.Text = "--Select Buyer Party--";
                //string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                //            + " FROM  AccountMaster "
                //            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" + txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //qry = qry.Replace("AccountMaster", AccountMasterTable);
                //this.showPopup(qry);

                lblPopupHead.Text = "--Select Buyer--";
                string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                            + " FROM  AccountMaster "
                            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" +
                            txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%"
+ txtSearchText.Text + "%') order by Ac_Name_E asc";
                qry = qry.Replace("AccountMaster", AccountMasterTable);
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "SubBrker")
            {

                //txtSearchText.Text = txtsubBroker.Text;
                lblPopupHead.Text = "--Select Buyer--";
                string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                            + " FROM  AccountMaster "
                            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" + txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                qry = qry.Replace("AccountMaster", AccountMasterTable);
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtBP_Account")
            {
                //txtSearchText.Text = txtBroker.Text;
                lblPopupHead.Text = "--Select BP Account--";
                string qry = "select Ac_Code , Ac_Name_E ,cityname  from " + AccountMasterTable + " where Locked=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
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
                        //pnlPopup.Style["display"] = "block";

                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        // pnlPopup.Style["display"] = "block";
                    }
                }
            }
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

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            //hdnfpopup.Value = null;

            if (strTextBox == "txtmillcode")
            {
                if (txtmillcode.Text != string.Empty)
                {
                    qry = "select Mill_Code,millname,millshortname,Tender_No,BALANCE,Quantal,Grade,Mill_Rate,Lifting_DateConverted,season,Purc_Rate from "
                        + tblPrefix + "qrytenderdobalanceview  where Mill_Code=" + txtmillcode.Text + " and Tender_No=" + hdnftenderno.Value +
                        "  and ID=" + hdnfID.Value + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            txtmillcode.Text = ds.Tables[0].Rows[0]["Mill_Code"].ToString();
                            lblmillname.Text = ds.Tables[0].Rows[0]["millshortname"].ToString();
                            txttender.Text = ds.Tables[0].Rows[0]["Tender_No"].ToString();
                            lblselfqty.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                            lblgrade.Text = ds.Tables[0].Rows[0]["Grade"].ToString();
                            lblmillrate.Text = ds.Tables[0].Rows[0]["Mill_Rate"].ToString();
                            lbllifting.Text = ds.Tables[0].Rows[0]["Lifting_DateConverted"].ToString();
                            lblseason.Text = ds.Tables[0].Rows[0]["Purc_Rate"].ToString();
                            lblpurcrate.Text = ds.Tables[0].Rows[0]["season"].ToString();
                            txtBuyerParty.Text = "2";
                            txtsubBroker.Text = "2";
                            txtGSTrate.Text = "5.00";
                            txtTCSrate1.Text = "0.10";
                            setFocusControl(txtBuyer);
                            txtDetailSaudaDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                            string dd = "";
                            dd = DateTime.Parse(txtDetailLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                            DateTime Headliftingdate = Convert.ToDateTime(dd);
                            string Docdate = Headliftingdate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            string Dlift = System.DateTime.Now.ToString("dd/MM/yyyy");
                            string d = DateTime.Parse(Dlift, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                            DateTime DetailLift = Convert.ToDateTime(d);


                            if (Headliftingdate > DetailLift)
                            {
                                txtDetailLiftingDate.Text = txtDetailLiftingDate.Text;
                            }
                            else
                            {
                                txtDetailLiftingDate.Text = Dlift;
                            }
                            if (drpDeliveryType.SelectedValue == "N" || drpDeliveryType.SelectedValue == "A")
                            {
                                setFocusControl(drpDeliveryType);
                                txtBuyerCommission.Enabled = false;
                            }
                            else
                            {
                                txtBuyerCommission.Enabled = true;
                            }

                        }
                    }
                }
            }
            if (strTextBox == "txttender")
            {
                setFocusControl(txtBuyer);
            }

            if (strTextBox == "txtBuyer")
            {
                setFocusControl(txtShipTo);
            }
            if (strTextBox == "txtShipTo")
            {
                setFocusControl(drpDeliveryType);
            }
            if (strTextBox == "txtBuyerQuantal")
            {
                setFocusControl(txtBuyerParty);
            }
            if (strTextBox == "txtBuyerParty")
            {
                setFocusControl(txtsubBroker);
            }
            if (strTextBox == "txtsubbroker")
            {
                setFocusControl(txtBuyerQuantal);
            }
            if (strTextBox == "txtBuyerQuantal")
            {
                setFocusControl(txtBuyerSaleRate);
            }
            if (strTextBox == "txtBuyerSaleRate")
            {
                setFocusControl(chkLoding_Chk);
            }
            if (strTextBox == "chkLoding_Chk")
            {
                setFocusControl(btnUpdate);
            }
            if (strTextBox == "txtcashdifference")
            {
                setFocusControl(txtBuyerCommission);
            }
            if (strTextBox == "txtBuyerCommission")
            {
                setFocusControl(txtDetailSaudaDate);
            }
            if (strTextBox == "txtDetailSaudaDate")
            {
                setFocusControl(txtDetailLiftingDate);
            }

            if (strTextBox == "txtDetailLiftingDate")
            {
                setFocusControl(txtBuyer);
            }
            if (strTextBox == "txtBuyerNarration")
            {
                setFocusControl(txtGSTrate);
            }
            if (strTextBox == "txtGSTrate")
            {
                setFocusControl(txtgstamt);
            }
            if (strTextBox == "txtgstamt")
            {
                setFocusControl(txtTCSrate1);
            }
            if (strTextBox == "txtTCSrate1")
            {
                setFocusControl(txtTCSamount1);
            }
            if (strTextBox == "txtTCSamount1")
            {
                setFocusControl(btnUpdate);
            }

        }
        catch
        {

        }

    }
    #endregion

    #region[txtBuyer_TextChanged]
    protected void txtBuyer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = "";
            strTextBox = "BU";
            searchString = txtBuyer.Text;
            string buyerName = string.Empty;
            string buyerCity = string.Empty;
            if (txtBuyer.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtBuyer.Text);
                if (a == false)
                {
                    btnBuyer_Click(this, new EventArgs());
                }
                else
                {
                    qry = "select Ac_Name_E,accoid,cityname from qrymstaccountmaster where Ac_Code=" + txtBuyer.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                buyerName = dt.Rows[0]["Ac_Name_E"].ToString();
                                lblBuyerName.Text = buyerName;
                                buyerCity = dt.Rows[0]["cityname"].ToString();
                                lblbuyercity.Text = buyerCity;
                                lblbuyer_id.Text = dt.Rows[0]["accoid"].ToString();

                                txtShipTo.Text = txtBuyer.Text;
                                lblShiptoname.Text = buyerName;
                                lblshiptocity.Text = buyerCity;
                                lblshiptoid.Text = dt.Rows[0]["accoid"].ToString();

                                // string dd = "";

                                //dd = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                                //  DateTime Headliftingdate = Convert.ToDateTime(dd);
                                //  string Docdate = Headliftingdate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                //  string Dlift = System.DateTime.Now.ToString("dd/MM/yyyy");
                                //string d = DateTime.Parse(Dlift, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                                //DateTime DetailLift = Convert.ToDateTime(d);


                                //if (Headliftingdate > DetailLift)
                                //{
                                //    txtDetailLiftingDate.Text = txtLiftingDate.Text;
                                //}
                                //else
                                //{
                                //    txtDetailLiftingDate.Text = Dlift;
                                //}

                                if (drpDeliveryType.SelectedValue == "C")
                                {
                                    AddCommission();
                                }
                                else
                                {
                                    txtBuyerCommission.Text = "0";
                                }
                                if (txtBuyer.Text != txtShipTo.Text)
                                {
                                    setFocusControl(txtShipTo);
                                }
                                else
                                {
                                    if (txtShipTo.Text != string.Empty)
                                    {
                                        setFocusControl(txtShipTo);
                                    }
                                    else
                                    {
                                        setFocusControl(txtBuyer);
                                    }

                                }


                            }
                        }
                    }

                    else
                    {
                        txtBuyer.Text = string.Empty;
                        lblBuyerName.Text = string.Empty;
                        lblbuyercity.Text = string.Empty;
                        txtBuyer.Focus();
                        setFocusControl(txtBuyer);
                    }
                }
            }
            else
            {
                txtBuyer.Text = string.Empty;
                lblBuyerName.Text = buyerName;
                lblbuyercity.Text = buyerCity;
                setFocusControl(txtBuyer);
            }

        }
        catch
        {
        }

    }

    private void AddCommission()
    {
        txtBuyerCommission.Text = clsCommon.getString("select isnull(Commission,0) from " + AccountMasterTable
            + " where Ac_Code=" + txtBuyer.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
    }
    #endregion

    #region[btnBuyer_Click]
    protected void btnBuyer_Click(object sender, EventArgs e)
    {
        //  pnlPopupTenderDetails.Style["display"] = "block";
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "BU";
        btnSearch_Click(sender, e);
    }
    #endregion

    protected void txtShipTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtShipTo.Text;
            string buyerPartyName = string.Empty;
            if (txtShipTo.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtShipTo.Text);
                if (a == false)
                {
                    btntxtShipTo_Click(this, new EventArgs());
                }
                else
                {
                    qry = "select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtShipTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                    buyerPartyName = clsCommon.getString(qry);
                    if (buyerPartyName != string.Empty && buyerPartyName != "0")
                    {
                        lblShiptoname.Text = buyerPartyName;
                        setFocusControl(drpDeliveryType);
                    }
                    else
                    {
                        txtShipTo.Text = "";
                        lblShiptoname.Text = "";
                        setFocusControl(drpDeliveryType);
                    }
                }
            }
        }
        catch
        {
        }
        strTextBox = "txtShipTo";
        searchString = txtShipTo.Text;
        csCalculations();
    }
    protected void btntxtShipTo_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtShipTo";
        btnSearch_Click(sender, e);
    }
    protected void drpDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(drpDeliveryType);


        if (drpDeliveryType.SelectedValue == "C")
        {
            AddCommission();
        }
        else
        {
            txtBuyerCommission.Text = "0";
        }
        if (drpDeliveryType.SelectedValue == "N" || drpDeliveryType.SelectedValue == "A")
        {
            txtBuyerCommission.Enabled = false;
        }
        else
        {
            txtBuyerCommission.Enabled = true;
        }
    }
    #region[txtBuyerParty_TextChanged]
    protected void txtBuyerParty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtBuyerParty.Text;
            string buyerPartyName = string.Empty;
            if (txtBuyerParty.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtBuyerParty.Text);
                if (a == false)
                {
                    btnBuyerParty_Click(this, new EventArgs());
                }
                else
                {
                    qry = "select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtBuyerParty.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                    buyerPartyName = clsCommon.getString(qry);
                    if (buyerPartyName != string.Empty && buyerPartyName != "0")
                    {
                        lblBuyerPartyName.Text = buyerPartyName;

                        setFocusControl(txtsubBroker);
                    }
                    else
                    {
                        txtBuyerParty.Text = "2";
                        lblBuyerPartyName.Text = "self";
                        setFocusControl(txtsubBroker);
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region[btnBuyerParty_Click]
    protected void btnBuyerParty_Click(object sender, EventArgs e)
    {
        // pnlPopupTenderDetails.Style["display"] = "None";
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "BP";
        btnSearch_Click(sender, e);
    }
    #endregion

    protected void txtsubBroker_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtsubBroker.Text;
            strTextBox = "SubBrker";
            string brokerName = string.Empty;
            if (txtsubBroker.Text == "0")
            {
                txtsubBroker.Text = "2";
            }
            if (txtsubBroker.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtsubBroker.Text);
                if (a == false)
                {
                    btnsubBrker_Click(this, new EventArgs());
                }
                else
                {
                    qry = "select Ac_Name_E,accoid from qrymstaccountmaster where Ac_Code='" + txtsubBroker.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                brokerName = dt.Rows[0]["Ac_Name_E"].ToString();
                                if (brokerName.Length > 15)
                                {
                                    brokerName.Substring(0, 15);
                                }
                                else if (brokerName.Length > 10)
                                {
                                    brokerName.Substring(0, 10);
                                }
                                lblsubBroker.Text = brokerName;
                                lblsubId.Text = dt.Rows[0]["accoid"].ToString();
                                //if (grdDetail.Rows.Count > 0)
                                //{
                                //    grdDetail.Rows[0].Cells[5].Text = Convert.ToString(txtBuyerParty.Text);
                                //    grdDetail.Rows[0].Cells[6].Text = Convert.ToString(lblBroker.Text);
                                //}
                                setFocusControl(txtBuyerQuantal);
                            }
                            else
                            {
                                txtBuyerParty.Text = "2";

                                setFocusControl(txtBuyerQuantal);
                            }
                        }
                    }

                    else
                    {
                        txtBuyerParty.Text = string.Empty;
                        lblBuyerPartyName.Text = string.Empty;
                        setFocusControl(txtBuyerParty);
                    }
                }
            }
            else
            {
                txtBuyerParty.Text = string.Empty;
                lblBuyerPartyName.Text = brokerName;
                setFocusControl(txtBuyerParty);
            }
        }
        catch
        {
        }
    }
    protected void btnsubBrker_Click(object sender, EventArgs e)
    {
        try
        {

            hdnfClosePopup.Value = "SubBrker";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }
    }
    #region [txtBuyerQuantal_TextChanged]
    protected void txtBuyerQuantal_TextChanged(object sender, EventArgs e)
    {

        TCSAmountCalculate();

        setFocusControl(txtBuyerSaleRate);
    }

    private void TCSAmountCalculate()
    {
        try
        {
            double cashdiffvalue = 0.00;
            double qntl = txtBuyerQuantal.Text != string.Empty ? Convert.ToDouble(txtBuyerQuantal.Text) : 0.00;
            double srate = Convert.ToDouble((txtBuyerSaleRate.Text != string.Empty ? Convert.ToDouble(txtBuyerSaleRate.Text) : 0) + (txtBuyerCommission.Text != string.Empty ? Convert.ToDouble(txtBuyerCommission.Text) : 0));
            double totalrate = (srate * Convert.ToDouble(txtBuyerQuantal.Text != string.Empty ? Convert.ToDouble(txtBuyerQuantal.Text) : 0));
            //txtGSTrate.Text = txtGSTrate.Text.Trim();
            string val = txtGSTrate.Text != string.Empty ? txtGSTrate.Text : "0";
            val = val.Trim();
            double gstr = Convert.ToDouble(val);
            double gstAmt = Convert.ToDouble((totalrate * gstr) / 100);
            txtgstamt.Text = gstAmt.ToString();
            double totalgstAmt = Convert.ToDouble(totalrate + gstAmt);
            //txtTCSrate1.Text = txtTCSrate1.Text.Trim();
            double tcsr = Convert.ToDouble(txtTCSrate1.Text.Trim() != string.Empty ? Convert.ToDouble(txtTCSrate1.Text) : 0);
            double value = 0.00;
            // value = Convert.ToDouble(lblValue.Text);
            totalgstAmt = Math.Round((totalgstAmt * tcsr) / 100, 2);

            txtTCSamount1.Text = totalgstAmt.ToString();
            double netamount = 0.00;
            netamount = totalgstAmt + value;
            netamount = qntl * srate + gstAmt + totalgstAmt / qntl;
            lblNetAmt.Text = netamount.ToString();
            double cashdiffvalue1 = 0.00;
            cashdiffvalue1 = Convert.ToDouble(txtcashdifference.Text.Trim() != string.Empty ? Convert.ToDouble(txtcashdifference.Text) : 0);
            cashdiffvalue = (srate - cashdiffvalue1) * qntl;
            lblcashdifferencevalue.Text = cashdiffvalue.ToString();
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBuyerSaleRate_TextChanged]
    protected void txtBuyerSaleRate_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(chkLoding_Chk);
        TCSAmountCalculate();
    }
    #endregion
    protected void txtcashdifference_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtBuyerCommission);
        TCSAmountCalculate();
    }
    #region [txtBuyerCommission_TextChanged]
    protected void txtBuyerCommission_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtDetailSaudaDate);
        TCSAmountCalculate();
    }
    #endregion
    #region [txtDetailSaudaDate_TextChanged]
    protected void txtDetailSaudaDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDetailSaudaDate.Text != string.Empty)
            {
                string d = DateTime.Parse(txtDetailSaudaDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                //txtDetailLiftingDate.Text = clsCommon.getString("SELECT date_format(DATE_ADD('" + d + "', INTERVAL 10 DAY),'%d/%m/%Y') as d");
                setFocusControl(txtDetailLiftingDate);
            }
        }
        catch
        {
            txtDetailSaudaDate.Text = string.Empty;
            setFocusControl(txtDetailSaudaDate);
        }
    }
    #endregion
    #region [txtDetailLiftingDate_TextChanged]
    protected void txtDetailLiftingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDetailLiftingDate.Text != string.Empty)
            {
                string d = DateTime.Parse(txtDetailLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                setFocusControl(txtBuyer);
            }
        }
        catch
        {
            txtDetailLiftingDate.Text = string.Empty;
            setFocusControl(txtDetailLiftingDate);
        }
    }
    #endregion
    #region [txtBuyerNarration_TextChanged]
    protected void txtBuyerNarration_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtGSTrate);
    }
    #endregion

    protected void txtGSTrate_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtgstamt);
        TCSAmountCalculate();
    }

    protected void txtTCSrate1_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtTCSamount1);
        TCSAmountCalculate();
    }
}