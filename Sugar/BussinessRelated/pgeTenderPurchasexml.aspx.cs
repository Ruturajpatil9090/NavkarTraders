using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Text;
using System.Web.Services;

public partial class Sugar_pgeTenderPurchasexml : System.Web.UI.Page
{

    public DataSet ds = null;
    public DataTable dt = null;
    Hashtable hash = null;
    string tblHeadVoucher = string.Empty;
    string GLedgerTable = string.Empty;
    public string tableName { get; set; }
    public string code { get; set; }

    string cs = string.Empty;
    string Action = string.Empty;
    //int Tender_No = 0;
    int id = 0;
    int flag = 0;
    int count = 0;
    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    LocalVoucher LV = null;
    int voucher_no = 0;
    string GLEDGER_Insert = string.Empty;
    string GLEDGER_Delete = string.Empty;
    string msg = string.Empty;


    DataTable dt1 = null;
    DataTable dt2 = null;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string qryHead = string.Empty;
    string qryDetail = string.Empty;
    string strTextbox = string.Empty;
    int defaultAccountCode = 0;
    string searchString = string.Empty;
    static WebControl objAsp = null;
    string Tran_Type = "LV";
    string millShortName = string.Empty;
    string DOShortname = string.Empty;
    string voucherbyshortname = string.Empty;
    string AUTO_VOUCHER = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string retValue;
    string fornotsaverecord;
    #endregion
    #region Head Declaration Field
    string Lifting_Date = string.Empty;
    string Lifting_Date_Head = String.Empty;
    string Tender_Date = String.Empty;
    Int32 Mill_Code = 0;
    string Sell_Note_No = String.Empty;
    double Mill_Rate = 0.00;
    string Grade = String.Empty;
    double Quantal = 0;
    int Packing = 0;
    int Bags = 0;
    double VOUCHERAMOUNT = 0;
    double DIFF = 0.00;
    bool isNumeric;
    int n;
    double PURCHASE_RATE = 0;
    int Payment_To, Tender_From, Tender_DO, Voucher_By, Broker = 0;
    Int32 VoucherNo = 0;
    float m = 0;
    float Excise_Rate = 0;
    Int32 GstRate_Code = 0;
    string Narration = string.Empty;
    string userName = string.Empty;
    float Purc_Rate = 0;
    string type = string.Empty;
    int Branch_Id = 0;
    string Created_By = clsGV.user;
    string Modified_By = clsGV.user;
    string myNarration = string.Empty;
    double Brokrage = 0.00;
    string str = string.Empty;
    double Diff_Amount = 0.00;
    int docno = 0;
    Int32 Tender_No = 0;
    string Year_Code = string.Empty;
    Int32 Company_Code = 0;
    Int32 mc = 0;
    Int32 itemcode = 0;
    string season = string.Empty;
    Int32 pt = 0;
    Int32 tf = 0;
    Int32 vb = 0;
    Int32 bk = 0;
    Int32 ic = 0;
    Int32 td = 0;
    Int32 Rc = 0;
    double CashDiff = 0.00;

    string Head_Insert = string.Empty;
    StringBuilder Head_Update = null;
    string Head_Delete = string.Empty;
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    #endregion

    #region Detail Declare Field
    string Detail_Insert = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;

    Int32 Buyer = 0;
    double Buyer_Quantal = 0.00;
    double Sale_Rate = 0.00;
    double Commission_Rate = 0.00;
    string Narration1 = "";
    // DateTime.Now.ToString("dd/MM/yyyy");
    string dtype = string.Empty;
    string Delivery_Type = "";
    string Sauda_Date = string.Empty;
    int subBroker = 0;
    string Lifting_Date1 = string.Empty;
    int ID = 0;
    double Buyer_Party = 0;

    #endregion
    #region Grid Fields
    int Detail_Id = 2;
    int Party = 3;
    int Name_of_Party = 4;
    int Broker_No = 5;
    int Name_Of_Broker = 6;
    int Shipto = 7;
    int shiptoname = 8;
    int quantal = 9;
    int saleRate = 10;
    int Cashdiff = 11;
    int commissions = 12;
    int saudaDate = 13;
    int liftingDate = 14;
    int Sauda_Narration = 15;
    int delivaryType = 16;

    int sub_broker = 17;
    int subBrokername = 18;
    int tenderdetailid = 19;

    int rowAction = 20;
    int SrNo = 21;
    int partyid = 22;
    int Dbc = 23;
    int Sbc = 24;
    int DGSTRate = 25;
    int DGSTAmt = 26;
    int DTCSRate = 27;
    int DTCSAmount = 28;
    int Shiptoid = 29;
    int BP_Detail = 30;
    int BPDetailname = 31;
    int bpid = 32;
    int loding_by_us = 33;

    #endregion


    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            #region [tn]
            ViewState["tn"] = Request.QueryString["tn"];
            ViewState["source"] = Request.QueryString["source"];
            hdnfmillpaymentdate.Value = Session["millpaymentdate"].ToString();
            #endregion

            #region set company name
            string Company_Name_E = clsCommon.getString("select Company_Name_E from Company where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
            Label lbl = (Label)Master.FindControl("lblCompanyName");
            lbl.Text = Company_Name_E;
            #endregion

            tblHeadVoucher = tblPrefix + "voucher";
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "Tender";
            tblDetails = tblPrefix + "TenderDetails";
            AccountMasterTable = "qrymstaccountmaster";
            qryCommon = "qrytenderheaddetail";
            GLedgerTable = tblPrefix + "GLEDGER";
            qryDetail = "qrytenderdetail";
            qryHead = "qrytenderhead";
            defaultAccountCode = Convert.ToInt32(clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Name_E='Self'"));
            pnlPopup.Style["display"] = "none";
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            #region purc rate enable/disables
            if (drpResale.SelectedValue == "M")
            {
                txtPurcRate.Enabled = false;
                // txtDate.Focus();
                rfvtxtPurcRate.Enabled = false;
                rfvtxtDO.Enabled = false;
                rfvtxtPaymentTo.Enabled = false;
                rfvtxtPaymentTo.Enabled = false;
            }
            else
            {
                txtPurcRate.Enabled = true;
                txtDate.Focus();
                rfvtxtPurcRate.Enabled = true;
                rfvtxtDO.Enabled = true;
                rfvtxtPaymentTo.Enabled = true;
                rfvtxtPaymentTo.Enabled = true;
            }
            #endregion

            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();

            Detail_Fields = new StringBuilder();
            Head_Fields = new StringBuilder();
            Head_Values = new StringBuilder();
            Detail_Values = new StringBuilder();
            Detail_Update = new StringBuilder();
            Detail_Delete = new StringBuilder();
            Head_Update = new StringBuilder();
            if (!Page.IsPostBack)
            {
                isAuthenticate = "1";

                //isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {

                        hdnf.Value = Request.QueryString["tenderid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        pnlgrdDetail.Enabled = true;
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        //this.DebitCreditDiff();
                        this.showLastRecord();
                        this.enableDisableNavigateButtons();
                        setFocusControl(btnAdd);


                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                        #region add self row into grid
                        if (grdDetail.Rows.Count == 0)
                        {
                            this.btnADDBuyerDetails_Click(sender, e);
                        }
                        #endregion


                        setFocusControl(drpResale);
                    }
                    GSTCode();
                    if (drpDeliveryType.SelectedValue == "N" || drpDeliveryType.SelectedValue == "A")
                    {
                        txtBuyerCommission.Enabled = false;
                    }
                    else
                    {
                        txtBuyerCommission.Enabled = true;
                    }

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            //if (objAsp != null)
            //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);

            if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
            {
                pnlPopup.Style["display"] = "none";
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                objAsp = btnSearch;
                //if (objAsp != null)
            }
        }
        catch
        {
            //  Response.Redirect("http://localhost:3994/HomePage/pgeloginForm.aspx");
        }
    }

    private void GSTCode()
    {

        txtGstrateCode.Text = txtGstrateCode.Text != string.Empty ? txtGstrateCode.Text : "0";
        try
        {
            DataSet ds = clsDAL.SimpleQuery("select * from NT_1_GSTRateMaster where Doc_no=" + txtGstrateCode.Text + "");
            DataTable dt = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["Rate"] = ds.Tables[0].Rows[0]["Rate"].ToString();
                Session["IGST"] = ds.Tables[0].Rows[0]["IGST"].ToString();
                Session["SGST"] = ds.Tables[0].Rows[0]["SGST"].ToString();
                Session["CGST"] = ds.Tables[0].Rows[0]["CGST"].ToString();
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


                    if (c is System.Web.UI.WebControls.HiddenField)
                    {
                        ((System.Web.UI.WebControls.HiddenField)c).Value = "";
                    }

                }
                txtEditDoc_No.Enabled = true;
                pnlPopup.Style["display"] = "none";
                //Button1.Enabled = false;
                btnSave.Text = "Save";
                btnSave.Enabled = false;
                btnChangeNo.Text = "Choose No";
                btnChangeNo.Enabled = false;
                txtBuyer.Enabled = false;
                btnBuyer.Enabled = false;
                txtBuyerParty.Enabled = false;
                btnBuyerParty.Enabled = false;
                lblMsg.Text = string.Empty;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                btntxtitem_code.Enabled = false;
                btnGstrateCode.Enabled = false;
                ViewState["currentTable"] = null;

                drpDeliveryType.Enabled = false;
                txtsubBroker.Enabled = false;
                txtBuyerQuantal.Enabled = false;
                txtBuyerSaleRate.Enabled = false;
                txtBuyerCommission.Enabled = false;
                txtDetailSaudaDate.Enabled = false;
                txtDetailLiftingDate.Enabled = false;
                txtBuyerNarration.Enabled = false;
                btnADDBuyerDetails.Enabled = false;
                btnClose.Enabled = false;
                btnsubBrker.Enabled = false;
                chkLoding_Chk.Enabled = false;

                txtBuyer.Enabled = false;
                txtBuyerParty.Enabled = false;
                txtCashDiff.Enabled = false;
                txtcashdifference.Enabled = false;
                txtShipTo.Enabled = false;
                btntxtShipTo.Enabled = false;
                txtGSTrate.Enabled = false;
                txtgstamt.Enabled = false;
                txtTCSrate1.Enabled = false;
                txtTCSamount1.Enabled = false;
                drpautopurchase.Enabled = false;
                txtBP_Account_Detail.Enabled = false;
                btnBP_Account_Detail.Enabled = false;
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
                    if (c is System.Web.UI.WebControls.HiddenField)
                    {
                        ((System.Web.UI.WebControls.HiddenField)c).Value = "";
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                txtEditDoc_No.Enabled = false;
                btnSave.Text = "Save";
                btnChangeNo.Text = "Change No";
                btnChangeNo.Enabled = false;

                #region set Business logic for save
                string start = DateTime.Now.ToShortDateString(); //clsCommon.getString("select Convert(varchar(10),GETDATE(),103) as d");
                DateTime startdate = DateTime.Parse(start);
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); //startdate.ToShortDateString();
                //  clsCommon.getString("select Convert(varchar(10),DATEADD(day,15,getdate()),103) as d");
                int millpaymentdate = Convert.ToInt32(hdnfmillpaymentdate.Value);
                txtLiftingDate.Text = DateTime.Today.AddDays(millpaymentdate).ToString("dd/MM/yyyy"); //liftingdate.ToString("dd/MM/yyyy");

                // DateTime Headliftingdate = DateTime.Parse(txtLiftingDate.Text);
                txtTenderNo.Enabled = false;
                pnlgrdDetail.Enabled = true;
                lblBroker_Id.Text = string.Empty;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                lblVoucherNo.Text = "";
                lblVoucherType.Text = "";
                txtDetailSaudaDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                //string Dlift = clsCommon.getString("SELECT date_format(CURDATE(),'%d/%m/%Y') as d");
                //DateTime DetailLift = DateTime.Parse(Dlift);


                string dd = "";

                dd = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                DateTime Headliftingdate = Convert.ToDateTime(dd);
                string Docdate = Headliftingdate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string Dlift = System.DateTime.Now.ToString("yyyy/MM/dd");
                DateTime DetailLift = Convert.ToDateTime(Dlift);


                if (Headliftingdate > DetailLift)
                {
                    txtDetailLiftingDate.Text = txtLiftingDate.Text;
                }
                else
                {
                    txtDetailLiftingDate.Text = Dlift;
                }
                ViewState["currentTable"] = null;
                btntxtitem_code.Enabled = true;
                btnGstrateCode.Enabled = true;
                #endregion

                drpDeliveryType.Enabled = true;
                txtsubBroker.Enabled = true;
                txtBuyerQuantal.Enabled = true;
                txtBuyerSaleRate.Enabled = true;
                txtBuyerCommission.Enabled = true;
                txtDetailSaudaDate.Enabled = true;
                txtDetailLiftingDate.Enabled = true;
                txtBuyerNarration.Enabled = true;
                chkLoding_Chk.Enabled = true;
                btnADDBuyerDetails.Enabled = true;
                btnClose.Enabled = true;
                btnsubBrker.Enabled = true;
                txtcashdifference.Enabled = true;
                txtShipTo.Enabled = true;
                txtcopyfrom.Enabled = true;
                btntxtShipTo.Enabled = true;
                txtBuyer.Enabled = true;
                txtBuyerParty.Enabled = true;
                btnBuyer.Enabled = true;
                btnBuyerParty.Enabled = true;
                txtCashDiff.Enabled = true;
                drpautopurchase.Enabled = true;
                txtBP_Account_Detail.Enabled = true;
                btnBP_Account_Detail.Enabled = true;

                txtitem_code.Text = "1";
                lblitemname.Text = clsCommon.getString("select System_Name_E from nt_1_systemmaster where System_Code=1 and " +
                    "Company_Code=" + Session["Company_Code"].ToString() + " and System_Type='I'");
                hdnfic.Value = clsCommon.getString("select systemid from qrymstitem where System_Code=1 and " +
                    "Company_Code=" + Session["Company_Code"].ToString() + "");

                hdnfhsnnumber.Value = clsCommon.getString("select HSN from qrymstitem where System_Code=1 and " +
                    "Company_Code=" + Session["Company_Code"].ToString() + "");

                txtTenderFrom.Text = "2";
                lblTenderFrom.Text = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='2' and " +
                    "Company_Code=" + Session["Company_Code"].ToString() + "");
                txtDO.Text = "2";
                lblDO.Text = lblTenderFrom.Text;
                txtVoucherBy.Text = "2";
                lblVoucherBy.Text = lblTenderFrom.Text;
                txtBroker.Text = "2";
                txtBP_Account.Text = "2";
                lblBroker.Text = lblDO.Text;
                txtGstrateCode.Text = "1";

                hdnftf.Value = Session["SELFID"].ToString();
                hdnftdo.Value = Session["SELFID"].ToString();
                hdnfvb.Value = Session["SELFID"].ToString();
                hdnfbc.Value = Session["SELFID"].ToString();
                //txtTCSRate.Text = Session["TCSRate"].ToString();
                txtTCSRate.Text = "0.00";
                txtTdsrate.Text = Session["SaleTDSRate"].ToString();
                txtGSTrate.Enabled = true;
                txtgstamt.Enabled = true;
                txtTCSrate1.Enabled = true;
                txtTCSamount1.Enabled = true;

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
                btnChangeNo.Text = "Choose No";
                btnChangeNo.Enabled = false;
                drpDeliveryType.Enabled = false;
                txtsubBroker.Enabled = false;
                txtBuyerQuantal.Enabled = false;
                txtBuyerSaleRate.Enabled = false;
                txtBuyerCommission.Enabled = false;
                txtDetailSaudaDate.Enabled = false;
                txtDetailLiftingDate.Enabled = false;
                txtBuyerNarration.Enabled = false;
                chkLoding_Chk.Enabled = false;
                btnADDBuyerDetails.Enabled = false;
                btnClose.Enabled = false;
                btnsubBrker.Enabled = false;

                txtBuyer.Enabled = false;
                txtBuyerParty.Enabled = false;
                btnBuyer.Enabled = false;
                btnBuyerParty.Enabled = false;
                btntxtitem_code.Enabled = false;
                btnGstrateCode.Enabled = false;
                txtCashDiff.Enabled = false;

                txtGSTrate.Enabled = false;
                txtgstamt.Enabled = false;
                txtTCSrate1.Enabled = false;
                txtTCSamount1.Enabled = false;
                txtcashdifference.Enabled = false;
                txtShipTo.Enabled = false;
                btntxtShipTo.Enabled = false;
                drpautopurchase.Enabled = false;
                txtBP_Account_Detail.Enabled = false;
                btnBP_Account_Detail.Enabled = false;
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


                GridViewRow gr = (GridViewRow)grdDetail.Rows[0];

                gr.Enabled = false;
                //txtTenderNo.Enabled = true;
                btnChangeNo.Text = "Choose No";
                txtDetailSaudaDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                btnChangeNo.Enabled = false;
                lblMsg.Text = string.Empty;
                setFocusControl(drpResale);
                txtEditDoc_No.Enabled = false;
                pnlgrdDetail.Enabled = true;
                txtBuyer.Enabled = true;
                txtBuyerParty.Enabled = true;
                btnBuyer.Enabled = true;
                btnBuyerParty.Enabled = true;
                btntxtitem_code.Enabled = true;
                btnGstrateCode.Enabled = true;

                drpDeliveryType.Enabled = true;
                txtsubBroker.Enabled = true;
                txtBuyerQuantal.Enabled = true;
                txtBuyerSaleRate.Enabled = true;
                txtBuyerCommission.Enabled = true;
                txtDetailSaudaDate.Enabled = true;
                txtDetailLiftingDate.Enabled = true;
                txtBuyerNarration.Enabled = true;
                chkLoding_Chk.Enabled = true;
                btnADDBuyerDetails.Enabled = true;
                btnClose.Enabled = true;
                btnsubBrker.Enabled = true;

                txtBuyer.Enabled = true;
                txtBuyerParty.Enabled = true;
                btnBuyer.Enabled = true;
                btnBuyerParty.Enabled = true;
                txtCashDiff.Enabled = true;

                txtGSTrate.Enabled = true;
                txtgstamt.Enabled = true;
                txtTCSrate1.Enabled = true;
                txtTCSamount1.Enabled = true;
                txtcashdifference.Enabled = true;
                txtShipTo.Enabled = true;
                btntxtShipTo.Enabled = true;
                drpautopurchase.Enabled = true;
                txtBP_Account_Detail.Enabled = true;
                btnBP_Account_Detail.Enabled = true;
            }

            #region Always check this
            if (drpResale.SelectedValue == "M")
            {
                txtPurcRate.Text = string.Empty;
                txtPurcRate.Enabled = false;

                rfvtxtPurcRate.Enabled = false;
                rfvtxtPaymentTo.Enabled = false;
                rfvtxtDO.Enabled = false;
            }
            else
            {
                //  txtPurcRate.Text = string.Empty;
                //   txtPurcRate.Enabled = true;

                rfvtxtPurcRate.Enabled = true;
                rfvtxtPaymentTo.Enabled = true;
                rfvtxtDO.Enabled = true;
            }
            #endregion

            #region common
            if (dAction == "S" || dAction == "N")
            {
                pnlgrdDetail.Enabled = false;

                btnMillCode.Enabled = false;
                btnGrade.Enabled = false;
                btnPaymentTo.Enabled = false;
                btnTenderFrom.Enabled = false;
                btnTenderDO.Enabled = false;
                btnVoucherBy.Enabled = false;
                btnBroker.Enabled = false;
                btnBP_Account.Enabled = false;
                // Button1.Enabled = false;
                calenderExtenderDate.Enabled = false;
                calenderExtenderLiftingdate.Enabled = false;
                drpResale.Enabled = false;

            }
            if (dAction == "A" || dAction == "N")
            {
                lblMillName.Text = string.Empty;
                lblPaymentTo.Text = string.Empty;
                lblTenderFrom.Text = string.Empty;
                lblDO.Text = string.Empty;
                lblVoucherBy.Text = string.Empty;
                lblBroker.Text = string.Empty;
                lblMsg.Text = string.Empty;

                lbldiff.Text = "0";
                lblAmount.Text = "0";
                drpResale.SelectedValue = "M";
                drpautopurchase.SelectedValue = "Y";

                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
            }

            if (dAction == "A" || dAction == "E")
            {
                btnMillCode.Enabled = true;
                btnGrade.Enabled = true;
                btnPaymentTo.Enabled = true;
                btnTenderFrom.Enabled = true;
                btnTenderDO.Enabled = true;
                btnVoucherBy.Enabled = true;
                btnBroker.Enabled = true;
                btnBP_Account.Enabled = true;

                // Button1.Enabled = true;
                calenderExtenderDate.Enabled = true;
                calenderExtenderLiftingdate.Enabled = true;
                drpResale.Enabled = true;
            }
            #endregion
            // AutoPostBackControl.Dispose();

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

    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = "select * from " + tblHead +
            //    " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and group_Code=" + hdnf.Value;

            string qryDisplay = "select * from " + qryHead +
                " where tenderid =" + hdnf.Value;
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }


    #region [btnClose_Click]
    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtBuyer.Text = string.Empty;
        lblBuyerName.Text = string.Empty;
        txtBuyerParty.Text = string.Empty;
        lblBuyerPartyName.Text = string.Empty;
        txtBuyerQuantal.Text = string.Empty;
        txtBuyerSaleRate.Text = string.Empty;
        txtBuyerCommission.Text = string.Empty;
        txtBuyerNarration.Text = string.Empty;
        lblNetAmt.Text = "";
        lblno.Text = string.Empty;
        txtsubBroker.Text = string.Empty;
        txtShipTo.Text = string.Empty;
        lblshiptoid.Text = string.Empty;
        lblShiptoname.Text = string.Empty;
        txtcashdifference.Text = string.Empty;
        lblcashdifferencevalue.Text = string.Empty;
        btnADDBuyerDetails.Text = "ADD";
        // pnlPopupTenderDetails.Style["display"] = "none";
        hdnfNextFocus.Value = "";
        btnSave.Focus();
    }
    #endregion

    #region [btnADDBuyerDetails_Click]
    protected void btnADDBuyerDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcashdifference.Text != "" && txtcashdifference.Text != "0" && txtcashdifference.Text != "0.00")
            {

                if (txtBP_Account_Detail.Text == "" || txtBP_Account_Detail.Text == "2")
                {
                    setFocusControl(txtBP_Account_Detail);
                    return;

                }

            }
            hdnfNextFocus.Value = "";
            int rowIndex = 0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];

                if (btnADDBuyerDetails.Text == "ADD")
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
                        rowIndex = maxIndex;          //0
                    }
                    #endregion

                    //rowIndex = dt.Rows.Count + 1;
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
                else
                {
                    //update row
                    int n = Convert.ToInt32(lblno.Text);
                    rowIndex = Convert.ToInt32(lblID.Text);
                    dr = (DataRow)dt.Rows[n - 1];
                    dr["ID"] = rowIndex;
                    dr["SrNo"] = 0;

                    #region decide whether actual row is updating or virtual [rowAction]
                    string id = clsCommon.getString("select ID from " + tblDetails + " where  tenderDetailid='" + lbltenderdetailid.Text + "'");
                    if (id != string.Empty && id != "0")
                    {
                        dr["rowAction"] = "U";   //actual row
                    }
                    else
                    {
                        id = clsCommon.getString("select ID from " + tblDetails + " where Tender_No='" + txtTenderNo.Text + "' and ID='" + lblID.Text
                            + "' and IsActive='False' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and year_code="
                            + Convert.ToInt32(Session["year"].ToString()) + "");
                        if (id != string.Empty && id != "0")  //added but deleted by another user
                        {
                            dr["rowAction"] = "N";
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                    }

                    #endregion
                }

            }
            else
            {
                rowIndex = 1;
                dr = null;

                dt.Columns.Add(new DataColumn("ID", typeof(string)));
                dt.Columns.Add(new DataColumn("Party", typeof(int)));
                dt.Columns.Add(new DataColumn("Name_of_Party", typeof(string)));

                dt.Columns.Add(new DataColumn("Broker", typeof(int)));
                dt.Columns.Add(new DataColumn("Name_Of_Broker", typeof(string)));
                dt.Columns.Add(new DataColumn("ShipTo", typeof(int)));
                dt.Columns.Add(new DataColumn("ShipToname", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantal", typeof(double)));
                dt.Columns.Add(new DataColumn("Sale_Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("CashDifference", typeof(float)));

                dt.Columns.Add(new DataColumn("Commission", typeof(float)));
                dt.Columns.Add(new DataColumn("Sauda_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Lifting_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Sauda_Narration", typeof(string)));
                dt.Columns.Add(new DataColumn("Delivery_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("sub_broker", typeof(int)));
                dt.Columns.Add(new DataColumn("subBrokername", typeof(string)));
                dt.Columns.Add(new DataColumn("tenderdetailid", typeof(int)));
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));

                dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                dt.Columns.Add(new DataColumn("partyid", typeof(int)));
                dt.Columns.Add(new DataColumn("Dbc", typeof(int)));
                dt.Columns.Add(new DataColumn("Sbc", typeof(int)));

                dt.Columns.Add(new DataColumn("GSTRate", typeof(double)));
                dt.Columns.Add(new DataColumn("GSTAmt", typeof(double)));
                dt.Columns.Add(new DataColumn("TCSRate", typeof(string)));
                dt.Columns.Add(new DataColumn("TCSAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("Shiptoid", typeof(int)));
                dt.Columns.Add(new DataColumn("BP_Detail", typeof(int)));
                dt.Columns.Add(new DataColumn("BPDetailname", typeof(string)));
                dt.Columns.Add(new DataColumn("bpid", typeof(string)));
                dt.Columns.Add(new DataColumn("loding_by_us", typeof(string)));

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }

            if (rowIndex != 1)
            {
                dr["Party"] = Convert.ToInt32(txtBuyer.Text);
                dr["Name_of_Party"] = lblBuyerName.Text;
                if (txtBuyerParty.Text != string.Empty)
                {
                    dr["Broker"] = Convert.ToInt32(txtBuyerParty.Text);
                }
                else
                {
                    dr["Broker"] = 2;
                }
                if (lblBuyerPartyName.Text == string.Empty)
                {
                    dr["Name_Of_Broker"] = "Self";
                }
                else
                {
                    dr["Name_Of_Broker"] = Server.HtmlDecode(lblBuyerPartyName.Text);
                }
                string ship = txtShipTo.Text;
                ship = ship.Trim();

                if (ship != "")
                {
                    dr["ShipTo"] = txtShipTo.Text.Trim();
                    dr["ShipToname"] = lblShiptoname.Text.Trim();
                }

                else
                {
                    txtShipTo.Text = "0";
                    dr["ShipTo"] = "0";
                    dr["ShipToname"] = "0";
                }

                string BPDetail = txtBP_Account_Detail.Text;
                BPDetail = BPDetail.Trim();

                if (BPDetail != "")
                {
                    dr["BP_Detail"] = txtBP_Account_Detail.Text.Trim();
                    dr["BPDetailname"] = lblBP_Account_Detail.Text.Trim();
                }

                else
                {
                    txtBP_Account_Detail.Text = "2";
                    dr["BP_Detail"] = "2";
                    dr["BPDetailname"] = "Self";
                }

                double Quntal = Convert.ToDouble(txtBuyerQuantal.Text);
                double grdqntal = double.Parse(grdDetail.Rows[0].Cells[9].Text);
                if (grdqntal < Quntal)
                {
                    dr["Quantal"] = txtBuyerQuantal.Text;
                    //setFocusControl(txtBuyerQuantal);
                    //return;
                }
                else
                {
                    dr["Quantal"] = txtBuyerQuantal.Text;
                }

                if (txtBuyerSaleRate.Text != string.Empty)
                {
                    dr["Sale_Rate"] = float.Parse(txtBuyerSaleRate.Text);
                }
                else
                {
                    dr["Sale_Rate"] = 0.00;
                }
                string BillNo = txtcashdifference.Text;
                BillNo = BillNo.Trim();

                if (BillNo != "")
                {
                    dr["CashDifference"] = float.Parse(txtcashdifference.Text);
                }

                else
                {
                    dr["CashDifference"] = "0";

                }

                if (txtBuyerCommission.Text != string.Empty)
                {
                    dr["Commission"] = float.Parse(txtBuyerCommission.Text);
                }
                else
                {
                    dr["Commission"] = 0.00;
                }
                if (txtDetailSaudaDate.Text != string.Empty)
                {
                    dr["Sauda_Date"] = txtDetailSaudaDate.Text;
                }
                else
                {
                    dr["Sauda_Date"] = txtDate.Text;
                }

                if (txtDetailLiftingDate.Text != string.Empty)
                {
                    //string a = System.DateTime.Now.ToString("dd-MM-yyyy");
                    dr["Lifting_Date"] = txtDetailLiftingDate.Text;
                }
                else
                {
                    dr["Lifting_Date"] = txtLiftingDate.Text;
                }

                //dr["Sauda_Narration"] = txtBuyerNarration.Text;
                dr["Sauda_Narration"] = Server.HtmlDecode(txtBuyerNarration.Text);

                if (chkLoding_Chk.Checked == true)
                {
                    dr["loding_by_us"] = "Y";
                }
                else
                {
                    dr["loding_by_us"] = "N";
                }
                if (drpDeliveryType.SelectedValue == "C")
                {
                    dr["Delivery_Type"] = "Commission";
                }
                if (drpDeliveryType.SelectedValue == "N")
                {
                    dr["Delivery_Type"] = "With GST Naka Delivery";
                }
                if (drpDeliveryType.SelectedValue == "A")
                {
                    dr["Delivery_Type"] = "Naka Delivery without GST Rate";
                }
                if (drpDeliveryType.SelectedValue == "D")
                {
                    dr["Delivery_Type"] = "DO";
                }
                if (txtsubBroker.Text != string.Empty && txtsubBroker.Text != "0")
                {
                    dr["sub_broker"] = Convert.ToInt32(txtsubBroker.Text);
                }
                else
                {
                    dr["sub_broker"] = 2;
                }
                if (lblsubBroker.Text == string.Empty)
                {
                    dr["subBrokername"] = "Self";
                }
                else
                {
                    dr["subBrokername"] = Server.HtmlDecode(lblsubBroker.Text);
                }

                dr["partyid"] = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code=" + dr["Party"].ToString() + " and " +
                    "company_Code=" + Session["Company_Code"].ToString() + "");
                dr["Dbc"] = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code=" + dr["Broker"].ToString() + " and " +
                    "company_Code=" + Session["Company_Code"].ToString() + "");
                dr["Sbc"] = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code=" + dr["sub_broker"].ToString() + " and " +
                    "company_Code=" + Session["Company_Code"].ToString() + "");

                dr["Shiptoid"] = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code=" + (txtShipTo.Text != string.Empty ? txtShipTo.Text : "0") + " and " +
                   "company_Code=" + Session["Company_Code"].ToString() + "");
                dr["bpid"] = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code=" + (txtBP_Account_Detail.Text != string.Empty ? txtBP_Account_Detail.Text : "0") + " and " +
                  "company_Code=" + Session["Company_Code"].ToString() + "");
                //dr["subBrokername"] = lblsubBroker.Text;
                txtGSTrate.Text = txtGSTrate.Text.Trim();
                dr["GSTRate"] = Server.HtmlDecode(txtGSTrate.Text != string.Empty ? txtGSTrate.Text : "0");
                dr["GSTAmt"] = Server.HtmlDecode(txtgstamt.Text != string.Empty ? txtgstamt.Text : "0");
                dr["TCSRate"] = Server.HtmlDecode(txtTCSrate1.Text != string.Empty ? txtTCSrate1.Text : "0");
                dr["TCSAmount"] = Server.HtmlDecode(txtTCSamount1.Text != string.Empty ? txtTCSamount1.Text : "0");
                // dr["Shiptoid"] = Server.HtmlDecode(lblshiptoid.Text != string.Empty ? lblshiptoid.Text : "0");

                if (btnADDBuyerDetails.Text == "ADD")
                {
                    dr["tenderdetailid"] = 0;
                    dt.Rows.Add(dr);


                }
                //else
                //{
                //    dr["tenderdetailid"] = lbltenderdetailid.Text;

                //}

                string id = clsCommon.getString("select ID from " + tblDetails + " where Tender_No='" + txtTenderNo.Text + "' and ID='1' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                if (id != string.Empty && id != "0")
                {
                    dt.Rows[0]["rowAction"] = "U";
                }
                else
                {
                    dt.Rows[0]["rowAction"] = "A";
                }
            }
            else
            {
                dr["rowAction"] = "A";
                dr["SrNo"] = 1;
                dr["Party"] = "2";
                dr["Name_of_Party"] = "Self";
                dr["Broker"] = "2";
                dr["Name_Of_Broker"] = "Self";
                dr["ShipTo"] = "2";
                dr["ShipToname"] = "Self";
                lblbuyer_id.Text = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=2");
                lblbuyerparty_id.Text = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=2");
                if (txtQuantal.Text != string.Empty)
                {
                    dr["Quantal"] = float.Parse(txtQuantal.Text);
                }
                else
                {
                    dr["Quantal"] = 0;
                }
                if (txtMillRate.Text != string.Empty)
                {
                    dr["Sale_Rate"] = float.Parse(txtMillRate.Text);
                }
                else
                {
                    dr["Sale_Rate"] = 0.00;
                }
                string BillNo = txtcashdifference.Text;
                BillNo = BillNo.Trim();

                if (BillNo != "")
                {
                    dr["CashDifference"] = float.Parse(txtcashdifference.Text);
                }

                else
                {
                    dr["CashDifference"] = "0";

                }
                dr["Commission"] = 0.00;
                dr["Sauda_Date"] = txtDate.Text;
                dr["Lifting_Date"] = txtLiftingDate.Text;
                dr["sub_broker"] = "2";
                dr["subBrokername"] = "self";
                dr["Sauda_Narration"] = string.Empty;
                dr["Delivery_Type"] = "";
                dr["loding_by_us"] = string.Empty;
                //dr["tenderdetailid"] = 1;
                dr["partyid"] = Session["SELFID"].ToString();
                dr["Dbc"] = Session["SELFID"].ToString();
                dr["Sbc"] = Session["SELFID"].ToString();
                dr["Shiptoid"] = Session["SELFID"].ToString();
                dr["bpid"] = Session["SELFID"].ToString();
                dr["GSTRate"] = Server.HtmlDecode(txtGSTrate.Text != string.Empty ? txtGSTrate.Text : "0");
                dr["GSTAmt"] = Server.HtmlDecode(txtgstamt.Text != string.Empty ? txtgstamt.Text : "0");
                dr["TCSRate"] = Server.HtmlDecode(txtTCSrate1.Text != string.Empty ? txtTCSrate1.Text : "0");
                dr["TCSAmount"] = Server.HtmlDecode(txtTCSamount1.Text != string.Empty ? txtTCSamount1.Text : "0");
                // dr["Shiptoid"] = Server.HtmlDecode(lblshiptoid.Text != string.Empty ? lblshiptoid.Text : "0");

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

            GridViewRow gr = (GridViewRow)grdDetail.Rows[0];
            gr.Enabled = false;
            #region set grid view ro colors
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[14].Text == "D" || grdDetail.Rows[i].Cells[14].Text == "R")
                {
                    grdDetail.Rows[i].Style["background-color"] = "#64BB7F";
                    grdDetail.Rows[i].ForeColor = System.Drawing.Color.White;
                    //  grdDetail.Rows[i].BackColor = System.Drawing.Color.Red;
                }
            }

            #endregion

            ViewState["currentTable"] = dt;
            //PopupTenderDetails.Show();

            txtBuyer.Text = string.Empty;
            drpDeliveryType.SelectedIndex = 1;
            lblBuyerName.Text = string.Empty;
            txtBuyerParty.Text = string.Empty;
            lblBuyerPartyName.Text = string.Empty;
            txtBuyerQuantal.Text = string.Empty;
            lblno.Text = string.Empty;
            txtsubBroker.Text = string.Empty;
            lblBP_Account_Detail.Text = string.Empty;
            txtBP_Account_Detail.Text = string.Empty;
            chkLoding_Chk.Checked = false;
            //txtDetailLiftingDate.Text = string.Empty;
            //txtDetailSaudaDate.Text = string.Empty;

            if (drpResale.SelectedValue == "R" || drpResale.SelectedValue == "W")
            {
                txtBuyerSaleRate.Text = txtPurcRate.Text != string.Empty ? Convert.ToString(txtPurcRate.Text) : "0";
            }
            else
            {
                txtBuyerSaleRate.Text = txtMillRate.Text;
            }
            txtBuyerCommission.Text = string.Empty;
            txtBuyerNarration.Text = string.Empty;
            lblID.Text = string.Empty;
            lblShiptoname.Text = string.Empty;
            txtShipTo.Text = string.Empty;
            txtcashdifference.Text = string.Empty;
            lblcashdifferencevalue.Text = string.Empty;

            //txtGSTrate.Text = string.Empty;
            //txtgstamt.Text = string.Empty;
            //txtTCSrate1.Text = string.Empty;
            //txtTCSamount1.Text = string.Empty;

            if (btnADDBuyerDetails.Text == "ADD")
            {
                // pnlPopupTenderDetails.Style["display"] = "block";
                txtBuyer.Focus();
            }
            else
            {
                // pnlPopupTenderDetails.Style["display"] = "none";
                // Button1.Focus();
            }

            btnADDBuyerDetails.Text = "ADD";
            lblNetAmt.Text = "";
            //calculate balance self
            this.calculateBalanceSelf();
            setFocusControl(txtBuyer);
            string dd = "";
            //DateTime Headliftingdate = DateTime.Parse(txtLiftingDate.Text);
            //string Dlift = clsCommon.getString("SELECT date_format(CURDATE(),'%d/%m/%Y') as d");
            //DateTime DetailLift = DateTime.Parse(Dlift);
            if (txtLiftingDate.Text != string.Empty)
            {
                // string d = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                //DateTime oDate = DateTime.ParseExact(txtLiftingDate.Text, "yyyy/MM/dd", null);
                string liftdate = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                DateTime oDate = DateTime.ParseExact(liftdate, "yyyy/MM/dd", null);
                string d = DateTime.Now.ToString("yyyy/MM/dd");
                DateTime currentDate = DateTime.ParseExact(d, "yyyy/MM/dd", null);

                if (currentDate >= oDate)
                {
                    txtDetailLiftingDate.Text = txtLiftingDate.Text;
                }
                else
                {
                    txtDetailLiftingDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); ;
                }
                drpDeliveryType.SelectedValue = Session["Despatch_Type"].ToString();
            }

        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            drpDeliveryType.SelectedValue = Session["Despatch_Type"].ToString();
            clsButtonNavigation.enableDisable("A");

            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
            this.NextNumber();
            #region Set Excise Rate
            txtExciseRate.Text = clsCommon.getString("select isnull(EXCISE_RATE,0) from " + tblPrefix + "CompanyParameters where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");

            #endregion

            #region add self row into grid
            if (grdDetail.Rows.Count == 0)
            {
                this.btnADDBuyerDetails_Click(sender, e);
            }
            #endregion


            txtGstrateCode.Text = "1";
            setFocusControl(drpResale);
        }
        catch
        {
            setFocusControl(drpResale);
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
                //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                obj.code = "Tender_No";
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
                                    txtTenderNo.Text = ds.Tables[0].Rows[0][0].ToString();
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
    private void AmtCalculation()
    {
        string SaleBillTo = txtPaymentTo.Text;
        string Amt = string.Empty;
        double SBBalAmt = 0.00;
        double gstRate = Convert.ToDouble("0" + txtGSTrate.Text);
        double saleRate = 0.00;
        string docdate = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string SaleTDS = "";
        string PurchaseTDS = "";



        SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtPaymentTo.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        //  hdnfistds.Value = SaleTDS;
        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);

        if (SaleTDS == "Y")
        {
            string tsd = Convert.ToString(txtTdsrate.Text);
            if (tsd == hdnfSaleTDS.Value)
            {
                hdnfSaleTDS.Value = Session["SaleTDSRate"].ToString();
            }
            else
            {
                hdnfSaleTDS.Value = txtTdsrate.Text;
            }


        }
        else
        {
            hdnfSaleTDS.Value = "0.00";
            string tcssale = Convert.ToString(txtTCSRate.Text);
            if (tcssale == hdnfTCSRate.Value)
            {
                hdnfTCSRate.Value = Session["TCSRate"].ToString();
            }
            else
            {
                hdnfTCSRate.Value = txtTCSRate.Text;
            }

        }
        DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        if (SaleTDS == "Y" && dEnd >= ss)
        {
            txtTCSRate.Text = "0.00";
            // txtTCSRate.Text = "0.00";
            hdnfTCSRate.Value = "0.00";
            txtTdsrate.Text = hdnfSaleTDS.Value;
        }
        else
        {
            txtTdsrate.Text = "0.00";

        }
    }

    private void AmtCalculationforvoucher()
    {

        string docdate = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        double Amount = 0.00;
        double Amountf = 0.00;
        string SaleBillTo = txtDO.Text;
        string Amt = string.Empty;
        double SBBalAmt = 0.00;

        // double gstRate = Convert.ToDouble("0" + txtIGSTRate.Text);
        double saleRate = 0.00;
        //double actualSaleRate = Convert.ToDouble("0" + txtSALE_RATE.Text);
        // double commision = Convert.ToDouble("0" + txtCommission.Text);
        //double qt = Convert.ToString(Math.Abs(Convert.ToDouble(txtquantal.Text))) != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0.00;
        string SaleTDS = "";
        string PurchaseTDS = "";


        SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtDO.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        hdnfistds.Value = SaleTDS;
        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);

        if (SaleTDS == "Y")
        {
            string tsd = Convert.ToString(txtvtdsrate.Text);
            if (tsd == hdnfVSaleTDS.Value)
            {
                hdnfVSaleTDS.Value = Session["SaleTDSRate"].ToString();
            }
            else
            {
                hdnfVSaleTDS.Value = txtvtdsrate.Text;
            }


        }
        else
        {
            hdnfVSaleTDS.Value = "0.00";
            string tcssale = Convert.ToString(txtvtcsrate.Text);
            if (tcssale == hdnfVTCSRate.Value)
            {
                hdnfVTCSRate.Value = Session["TCSRate"].ToString();
            }
            else
            {
                hdnfVTCSRate.Value = txtvtcsrate.Text;
            }

        }

        //saleRate = actualSaleRate + commision;

        //SBBalAmt = (saleRate * gstRate) / 100 + saleRate * qt;
        double qntl = Convert.ToDouble("0" + txtQuantal.Text);
        double MR = Convert.ToDouble("0" + txtMillRate.Text);
        double PR = Convert.ToDouble("0" + txtPurcRate.Text);
        double diff = MR - PR;
        double diffamt = diff * qntl;

        SBBalAmt = Convert.ToDouble("0" + diffamt);



        Amt = clsCommon.getString("select isnull sum((AMOUNT,'0')) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + txtDO.Text
                      + " and DRCR='D' and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        if (Amt == string.Empty || Amt == "")
        {
            Amt = "0.00";
        }
        Amountf = Convert.ToDouble(Amt);
        if (btnSave.Text == "Save")
        {
            Amount = Amountf + SBBalAmt;
        }
        else
        {
            Amount = Amountf;
        }
        DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        if (SaleTDS == "Y" && dEnd >= ss)
        {
            txtvtcsrate.Text = "0.00";

            hdnfVTCSRate.Value = "0.00";
            txtvtdsrate.Text = hdnfVSaleTDS.Value;
            #region[Balance]
            if (Amount > balancelimit)
            {

                txtvtdsrate.Text = Session["SaleTDSRate"].ToString();
            }
            else
            {

                if (hdnfVTCSRate.Value != string.Empty)
                {
                    txtvtdsrate.Text = hdnfVSaleTDS.Value;
                }
                else
                {
                    txtvtdsrate.Text = "0.000";
                }

            }
            #endregion
        }
        else
        {
            txtvtdsrate.Text = "0.00";
            #region[Balance]
            if (Amount > balancelimit)
            {

                txtvtcsrate.Text = Session["TCSRate"].ToString();
            }
            else
            {

                if (hdnfVTCSRate.Value != string.Empty)
                {
                    txtvtcsrate.Text = hdnfVTCSRate.Value;
                }
                else
                {
                    txtvtcsrate.Text = "0.000";
                }

            }
            #endregion
        }


    }

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = true;


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tender", "javascript:pagevalidation();", true);

        }
        catch
        {
        }
        //btnSave.Enabled = false;

    }


    #endregion

    #region [saveTenderDetails]
    private void saveTenderDetails(string tenderNo, string companyCode)
    {
        try
        {
            string str = "";
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    Int32 Buyer = Convert.ToInt32(grdDetail.Rows[i].Cells[3].Text);
                    double Buyer_Quantal = Convert.ToDouble(grdDetail.Rows[i].Cells[7].Text);
                    double Sale_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[8].Text);
                    double Commission_Rate = Convert.ToDouble(grdDetail.Rows[i].Cells[9].Text);
                    string Narration = "";
                    // DateTime.Now.ToString("dd/MM/yyyy");
                    string dtype = grdDetail.Rows[i].Cells[13].Text.ToString();
                    string Delivery_Type = "";
                    if (dtype == "Commission")
                    {
                        Delivery_Type = "C";
                    }
                    else
                    {
                        Delivery_Type = "N";
                    }
                    string Sauda_Date = grdDetail.Rows[i].Cells[10].Text.ToString();
                    string Lifting_Date = grdDetail.Rows[i].Cells[11].Text.ToString();

                    Sauda_Date = DateTime.Parse(Sauda_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    Lifting_Date = DateTime.Parse(Lifting_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                    Narration = Server.HtmlDecode(grdDetail.Rows[i].Cells[12].Text);
                    int ID = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);
                    double Buyer_Party = 0;
                    //if (grdDetail.Rows.Count == 1)
                    //{
                    //    Buyer_Party = Convert.ToInt32(txtBroker.Text);
                    //}
                    //else
                    //{

                    Buyer_Party = Convert.ToInt32(Server.HtmlDecode(grdDetail.Rows[i].Cells[5].Text));

                    //}
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        if (grdDetail.Rows[i].Cells[14].Text != "N")   //For N do nothing for that row
                        {
                            if (grdDetail.Rows[i].Cells[14].Text == "A")
                            {
                                #region check whether same id is inserted in table already or not (if then insert next no)
                                string id = clsCommon.getString("select AutoID from " + tblDetails + " where Tender_No='" + tenderNo + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and year_code=" + Convert.ToInt32(Session["year"].ToString()) + " and ID='" + ID + "'");
                                if (id != string.Empty && id != "0")
                                {
                                    //this id is already inserted Get max id
                                    string newId = clsCommon.getString("select max(ID) from " + tblDetails + " where Tender_No='" + tenderNo + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  and year_code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
                                    ID = Convert.ToInt32(newId) + 1;
                                }
                                #endregion

                                obj.flag = 1;
                                obj.columnNm = "Tender_No,Company_Code,Buyer,Buyer_Quantal,Sale_Rate,Commission_Rate,Sauda_Date,Lifting_Date,Narration,ID,Buyer_Party,IsActive,year_code,Branch_Id,Delivery_Type";
                                obj.values = "'" + tenderNo + "','" + companyCode + "','" + Buyer + "','" + Buyer_Quantal + "','" + Sale_Rate + "','" + Commission_Rate
                                    + "','" + Sauda_Date + "','" + Lifting_Date + "','" + Narration + "','" + ID + "','" + Buyer_Party + "','True','"
                                    + Convert.ToInt32(Session["year"].ToString()) + "','" + Convert.ToInt32(Session["Branch_Code"].ToString()) + "','" + Delivery_Type + "'";
                            }
                            if (grdDetail.Rows[i].Cells[14].Text == "U")
                            {
                                obj.flag = 2;
                                obj.columnNm = " Buyer='" + Buyer + "',Buyer_Quantal='" + Buyer_Quantal + "',Sale_Rate='" + Sale_Rate + "',Commission_Rate='"
                                    + Commission_Rate + "',Sauda_Date='" + Sauda_Date + "',Lifting_Date='" + Lifting_Date + "',Narration='" + Narration + "',Buyer_Party='"
                                    + Buyer_Party + "',Delivery_Type='" + Delivery_Type + "' where Tender_No='" + tenderNo + "' and Company_Code='"
                                    + Convert.ToInt32(Session["Company_Code"].ToString()) + "'  and year_code='" + Convert.ToInt32(Session["year"].ToString())
                                    + "'  and ID='" + ID + "'";
                                obj.values = "none";
                            }
                            if (grdDetail.Rows[i].Cells[14].Text == "D")
                            {
                                obj.flag = 3;
                                obj.columnNm = "Tender_No='" + tenderNo + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and ID='" + ID
                                    + "' and year_code='" + Convert.ToInt32(Session["year"].ToString()) + "'";
                                obj.values = "none";
                            }
                            obj.tableName = tblDetails;

                            DataSet ds = new DataSet();
                            ds = obj.insertAccountMaster(ref str);
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



    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int k = clsIsEdit.Tender_No;
        //if (!clsIsEdit.Tender.Any(a => a == Convert.ToInt32(txtTenderNo.Text)))
        //{
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        //Session["enableSave"] = 0;
        hdnf.Value = lblTender_Id.Text;
        // this.showLastRecord();
        //this.fetchRecord(txtTenderNo.Text);
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtTenderNo.Enabled = false;
        txtEditDoc_No.Text = "";
        //}
        //DateTime Headliftingdate = DateTime.Parse(txtLiftingDate.Text);
        //string Dlift = clsCommon.getString("SELECT date_format(CURDATE(),'%d/%m/%Y') as d");
        //DateTime DetailLift = DateTime.Parse(Dlift);
        string dd = "";
        drpDeliveryType.SelectedValue = Session["Despatch_Type"].ToString();
        dd = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        DateTime Headliftingdate = Convert.ToDateTime(dd);
        string Docdate = Headliftingdate.ToString("yyyy-MM-dd HH:mm:ss.fff");

        string Dlift = System.DateTime.Now.ToString("dd/MM/yyyy");
        string d = DateTime.Parse(Dlift, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        DateTime DetailLift = Convert.ToDateTime(d);


        if (Headliftingdate > DetailLift)
        {
            txtDetailLiftingDate.Text = txtLiftingDate.Text;
        }
        else
        {
            txtDetailLiftingDate.Text = Dlift;
        }

        if (drpDeliveryType.SelectedValue == "N" || drpDeliveryType.SelectedValue == "A")
        {
            txtBuyerCommission.Enabled = false;
        }
        else
        {
            txtBuyerCommission.Enabled = true;
        }
        if (lblVoucherNo.Text == string.Empty || lblVoucherNo.Text == "0")
        {
            drpResale.Enabled = true;
        }
        else
        {
            string envice = clsCommon.getString("select einvoiceno from commission_bill where doc_no=" + lblVoucherNo.Text +
                           "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                           " and Tran_Type='" + lblVoucherType.Text + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            if (envice == string.Empty || envice == "NA" || envice == "0")
            //  if (envice != "0")
            {


            }
            else
            {
                txtPurcRate.Enabled = false;
                txtMillRate.Enabled = false;
            }

            drpResale.Enabled = false;
        }
        int doexist = Convert.ToInt32(clsCommon.getString("select count(*) from   nt_1_deliveryorder where  purc_no=" + txtTenderNo.Text +
              " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
              " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())));
        //if (doexist >= 1)
        //{
        //   drpautopurchase.Enabled = false;
        //}
        int UTRexist = Convert.ToInt32(clsCommon.getString("select count(*) from   NT_1_UTRDetail where  lot_no=" + txtTenderNo.Text +
             " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
             " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())));
        if (doexist >= 1 || UTRexist >= 1)
        {
            drpautopurchase.Enabled = false;
        }
    }

    //private void SaveChanges()
    //{
    //    Session["enableSave"] = 1;
    //}
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string vouchereinvoiceno = "";
                vouchereinvoiceno = clsCommon.getString("select einvoiceno from commission_bill where  commissionid=" + hdnfcommisionid.Value);
                if (vouchereinvoiceno != string.Empty && vouchereinvoiceno != "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Voucher No EInvoice is generated so it cannot be Deleted')", true);
                    return;
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
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
        hdnf.Value = clsCommon.getString("select max(tenderid) from nt_1_tender where Company_Code=" + Session["Company_Code"].ToString() + " and " +
                " Year_Code=" + Session["year"].ToString() + "");


        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);

    }
    #endregion

    #region [fetchRecord]
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
                        //hdnf.Value = txtValue;
                        //txtTenderNo.Text = txtValue;
                        hdnf.Value = dt.Rows[0]["tenderid"].ToString();
                        txtDate.Text = dt.Rows[0]["Tender_DateConverted"].ToString();
                        txtLiftingDate.Text = dt.Rows[0]["Lifting_DateConverted"].ToString();
                        lblTender_Id.Text = hdnf.Value;
                        hdnftenderid.Value = hdnf.Value;
                        //txtDate.Text = dt.Rows[0]["Tender_Date"].ToString();
                        //txtLiftingDate.Text = dt.Rows[0]["Lifting_Date"].ToString();
                        //Response.Write("MillCode" + dt.Rows[0]["Mill_Code"].ToString());
                        //hdnf.Value = dt.Rows[0]["Tender_No"].ToString();
                        txtCashDiff.Text = dt.Rows[0]["CashDiff"].ToString();
                        txtTenderNo.Text = dt.Rows[0]["Tender_No"].ToString();
                        hdnftenderno.Value = txtTenderNo.Text;
                        txtMillCode.Text = dt.Rows[0]["Mill_Code"].ToString();
                        txtSeason.Text = dt.Rows[0]["season"].ToString();
                        txtitem_code.Text = dt.Rows[0]["itemcode"].ToString();
                        lblitemname.Text = dt.Rows[0]["itemname"].ToString();
                        hdnfhsnnumber.Value = dt.Rows[0]["HSN"].ToString();

                        txtGrade.Text = dt.Rows[0]["Grade"].ToString();
                        txtQuantal.Text = dt.Rows[0]["Quantal"].ToString();//Convert.ToString(Math.Abs(Convert.ToDouble(dt.Rows[0]["Quantal"].ToString())));
                        txtPacking.Text = dt.Rows[0]["Packing"].ToString();
                        txtBags.Text = dt.Rows[0]["Bags"].ToString();
                        txtMillRate.Text = dt.Rows[0]["Mill_Rate"].ToString();
                        txtpartybillrate.Text = dt.Rows[0]["Party_Bill_Rate"].ToString();

                        // txtPurcRate.Text = dt.Rows[0][""].ToString();
                        txtTenderFrom.Text = dt.Rows[0]["Tender_From"].ToString();
                        txtPaymentTo.Text = dt.Rows[0]["Payment_To"].ToString();
                        txtDO.Text = dt.Rows[0]["Tender_DO"].ToString();
                        txtVoucherBy.Text = dt.Rows[0]["Voucher_By"].ToString();
                        txtBroker.Text = dt.Rows[0]["Broker"].ToString();
                        txtExciseRate.Text = dt.Rows[0]["Excise_Rate"].ToString();
                        txtSellNoteNo.Text = dt.Rows[0]["Sell_Note_No"].ToString();
                        txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                        txtGstrateCode.Text = dt.Rows[0]["gstratecode"].ToString();
                        lblgstrateCode.Text = dt.Rows[0]["gstrate"].ToString();
                        hdnfmc.Value = dt.Rows[0]["mc"].ToString();
                        hdnfpt.Value = dt.Rows[0]["pt"].ToString();
                        hdnftf.Value = dt.Rows[0]["tf"].ToString();
                        hdnftdo.Value = dt.Rows[0]["td"].ToString();
                        hdnfvb.Value = dt.Rows[0]["vb"].ToString();
                        hdnfbc.Value = dt.Rows[0]["bk"].ToString();
                        hdnfic.Value = dt.Rows[0]["ic"].ToString();
                        hdnfbp.Value = dt.Rows[0]["bp"].ToString();
                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();
                        lblTender_Id.Text = dt.Rows[0]["tenderid"].ToString();
                        txtBP_Account.Text = dt.Rows[0]["Bp_Account"].ToString();

                        hdnfvoucherbygst.Value = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtVoucherBy.Text + " and " +
                           "Company_Code='" + Session["Company_Code"].ToString() + "'");

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

                        Int32 voucherno = dt.Rows[0]["Voucher_No"].ToString().Trim() != string.Empty ? Convert.ToInt32(dt.Rows[0]["Voucher_No"].ToString()) : 0;
                        vouchernumber.Value = voucherno.ToString();
                        lblMillName.Text = dt.Rows[0]["millname"].ToString();
                        lblPaymentTo.Text = dt.Rows[0]["paymenttoname"].ToString();
                        lblTenderFrom.Text = dt.Rows[0]["tenderfromname"].ToString();
                        lblVoucherBy.Text = dt.Rows[0]["voucherbyname"].ToString();
                        lblDO.Text = dt.Rows[0]["tenderdoname"].ToString();
                        lblBroker.Text = dt.Rows[0]["brokername"].ToString();
                        lblBP_Account.Text = dt.Rows[0]["BpAccountName"].ToString();

                        txtPurcRate.Text = dt.Rows[0]["Purc_Rate"].ToString();
                        drpResale.SelectedValue = dt.Rows[0]["type"].ToString();
                        lblVoucherNo.Text = vouchernumber.Value;
                        lblVoucherType.Text = dt.Rows[0]["Voucher_Type"].ToString();
                        hdnfcommisionid.Value = dt.Rows[0]["commissionid"].ToString();
                        txtBrokrage.Text = dt.Rows[0]["Brokrage"].ToString();
                        drptempertender.Text = dt.Rows[0]["Temptender"].ToString();
                        txtTdsrate.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTdsamount.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        txtTdsamount.Text = dt.Rows[0]["TDS_Amt"].ToString();
                        //  if(drpResale.SelectedValue
                        txtvtcsrate.Text = clsCommon.getString("select isnull(TCS_Rate,0) from commission_bill where doc_no=" + lblVoucherNo.Text +
                            "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            " and Tran_Type='" + lblVoucherType.Text + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

                        txtvtdsrate.Text = clsCommon.getString("select isnull(TDS_Per,0) from commission_bill where doc_no=" + lblVoucherNo.Text +
                          "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                          " and Tran_Type='" + lblVoucherType.Text + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        hdnfVTCSRate.Value = txtvtcsrate.Text;
                        hdnfVSaleTDS.Value = txtvtdsrate.Text;

                        hdnfistds.Value = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtDO.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        drpautopurchase.SelectedValue = dt.Rows[0]["AutoPurchaseBill"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";

                        #region Tender Details

                        // qry = "select  ID,Buyer as Party,buyerbrokerfullname  as [Name of Party],Buyer_Party as Broker,salepartyfullname as [Name of Broker],Buyer_Quantal as [Quantal],Sale_Rate as [Sale Rate],Commission_Rate as [Commission],Convert(varchar(10),DetailSaudaDate,103) as Sauda_Date,Convert(varchar(10),DetailLiftingDate,103) as Lifting_Date,saudanarration as [Sauda Narration],Delivery_Type from " + qryDetail + " where tenderid='" + hdnf.Value + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and IsActive='True' and TDYearCode='" + Convert.ToInt32(Session["year"].ToString()) + "' order by ID";
                        qry = "select  ID,Buyer as Party,buyername as Name_of_Party,Buyer_Party as Broker," +
" buyerpartyname as Name_of_Broker,isnull(ShipTo,0) as ShipTo ,isnull(ShipToname,0) as ShipToname, Buyer_Quantal as Quantal,Sale_Rate as Sale_Rate,isnull(CashDifference,0) as CashDifference," +
" Commission_Rate as Commission,Sauda_DateConverted as Sauda_Date,payment_dateConverted as Lifting_Date,detail_narration as Sauda_Narration," +
" Delivery_Type,sub_broker as sub_broker,subbrokername as subBrokername ,tenderdetailid,'' as rowAction,'' as SrNo,buyerid as partyid,buyerpartyid as Dbc,sbr as Sbc,gst_rate as GSTRate,gst_amt as GSTAmt,tcs_rate as TCSRate,tcs_amt as TCSAmount,Shiptoid,BP_Detail,BPDetailname,bpid,loding_by_us   from qrytenderheaddetail where tenderid='" + hdnf.Value + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by ID";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {

                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i]["Delivery_Type"].ToString() == "C")
                                        {
                                            dt.Rows[i]["Delivery_Type"] = "Commission";
                                        }
                                        if (dt.Rows[i]["Delivery_Type"].ToString() == "N")
                                        {
                                            dt.Rows[i]["Delivery_Type"] = "With GST Naka Delivery";
                                        }
                                        if (dt.Rows[i]["Delivery_Type"].ToString() == "D")
                                        {
                                            dt.Rows[i]["Delivery_Type"] = "DO";
                                        }
                                        if (dt.Rows[i]["Delivery_Type"].ToString() == "A")
                                        {
                                            dt.Rows[i]["Delivery_Type"] = "Naka Delivery without GST Rate";
                                        }

                                    }
                                    // dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    // dt.Columns.Add(new DataColumn("SrNo", typeof(int)));

                                    dt.Rows[0]["SrNo"] = 1;                           //self row
                                    dt.Rows[0]["rowAction"] = "U";                     //self row

                                    for (int i = 1; i < dt.Rows.Count; i++)
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

                        this.calculateDiff();
                        this.calculateAmount();
                        this.TCSCalculation();
                        this.TdsCalculation();
                        GSTCode();
                        GridViewRow gr = (GridViewRow)grdDetail.Rows[0];
                        gr.Enabled = false;
                        lblBalanceSelf.Text = Server.HtmlDecode(gr.Cells[7].Text);
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            this.makeEmptyForm("S");
            //hdnf.Value = txtTenderNo.Text;
            return recordExist;
        }
        catch
        {
            // throw;
            return false;
        }
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        #region [code]
        try
        {
            string query = "";
            query = "select tenderid from " + tblHead + " where tenderid=(select MIN(tenderid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            hdnf.Value = clsCommon.getString(query);
            navigateRecord();

        }
        catch
        {

        }
        #endregion
    }
    #endregion

    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTenderNo.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "SELECT top 1 [tenderid] from " + tblHead + " where tenderid<" + Convert.ToInt32(hdnf.Value) + " and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                    + " ORDER BY tenderid DESC  ";
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
            if (txtTenderNo.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "SELECT top 1 [tenderid] from " + tblHead + " where tenderid>" + Convert.ToInt32(hdnf.Value) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY tenderid asc  ";
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
            query = "select tenderid from " + tblHead + " where tenderid=(select MAX(tenderid) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();



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

        //if (ViewState["mode"].ToString() == "U")
        //{

        int RecordCount = 0;

        string query = "";
        query = "select count(*) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
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
            //  btnLast.Focus();
        }

        if (txtTenderNo.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [tenderid] from " + tblHead +
                    " where tenderid>" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY tenderid asc  ";
                //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY tenderid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnLast.Enabled = true;
                            btnNext.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnLast.Enabled = false;
                            btnNext.Enabled = false;
                        }
                    }
                }

                ds = new DataSet();
                dt = new DataTable();

                query = "SELECT top 1 [tenderid] from " + tblHead + " where tenderid<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY tenderid asc  ";
                //    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY tenderid asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnFirst.Enabled = true;
                            btnPrevious.Enabled = true;
                        }
                        else
                        {
                            btnFirst.Enabled = false;
                            btnPrevious.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        // }

        #endregion
    }
    #endregion

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
                        if (grdDetail.Rows[rowindex].Cells[rowAction].Text != "D" && grdDetail.Rows[rowindex].Cells[rowAction].Text != "R")
                        {
                            // pnlPopupTenderDetails.Style["display"] = "block";
                            this.showRecord(grdDetail.Rows[rowindex]);
                            btnADDBuyerDetails.Text = "Update";
                        }
                        break;

                    case "DeleteRecord":
                        string action = "";

                        DataTable dt1 = (DataTable)ViewState["currentTable"];
                        int refid = Convert.ToInt32(dt1.Rows[rowindex]["tenderdetailid"].ToString());

                        string reftenderid = clsCommon.getString(" select doc_no from nt_1_transactdetail where tenderdetailid=" + refid + " and drpFilterValue='S'  ");
                        string doid = clsCommon.getString(" select doc_no from qrydohead where tenderdetailid=" + refid + "");

                        //string refutr = clsCommon.getString(" select tenderid from qryutrheaddetail where lot_no=" + txtTenderNo.Text + " and " +
                        //    " Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");

                        //string utrno = clsCommon.getString(" select doc_no from qryutrheaddetail where lot_no=" + txtTenderNo.Text + " and " +
                        //    " Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");
                        string concat = string.Empty;

                        if (doid != string.Empty && doid != "0")
                        {
                            concat = "DO DocNo: " + doid + "";
                        }
                        if (reftenderid != string.Empty && reftenderid != "0")
                        {
                            concat = concat + "Receipt Payment DocNo: " + reftenderid + "";
                        }
                        if (doid == "0" && reftenderid == "0")
                        {

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
                            this.deleteTenderDetailsRecord(grdDetail.Rows[rowindex], action);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Record Use In " + concat + "')", true);
                        }
                        break;
                }
            }
        }
        catch
        {

        }

    }
    #endregion

    #region [deleteTenderDetailsRecord]
    private void deleteTenderDetailsRecord(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;

            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["tenderdetailid"].ToString());

                string IDExisting = clsCommon.getString("select ID from " + tblDetails + " where tenderdetailid=" + ID + " ");
                if (IDExisting != string.Empty && IDExisting != "0")
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "D";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "N";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                    //  dt.Rows[rowIndex]["rowAction"] = "D";  //isactive false
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;

                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "R";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "A";

                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }

                    // dt.Rows[rowIndex]["rowAction"] = "N";   //Do nothing
                }
                ViewState["currentTable"] = dt;
                this.calculateBalanceSelf();
                //ViewState["currentTable"] = dt;
            }
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows[i]["RowNumber"] = i + 1;
            //}
        }
        catch
        {

        }
    }
    #endregion

    #region [showRecord]
    private void showRecord(GridViewRow gridViewRow)
    {
        if (btnADDBuyerDetails.Text == "Update")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Please Add The Details')", true);

        }
        lblno.Text = Server.HtmlDecode(gridViewRow.Cells[SrNo].Text);
        lblID.Text = Server.HtmlDecode(gridViewRow.Cells[2].Text);
        lbltenderdetailid.Text = Server.HtmlDecode(gridViewRow.Cells[tenderdetailid].Text);
        txtBuyer.Text = Server.HtmlDecode(gridViewRow.Cells[Party].Text);
        lblBuyerName.Text = Server.HtmlDecode(gridViewRow.Cells[Name_of_Party].Text);
        string deliveryType = Server.HtmlDecode(gridViewRow.Cells[delivaryType].Text);
        string type = "";
        if (deliveryType == "Commission")
        {
            type = "C";
        }
        else if (deliveryType == "DO")
        {
            type = "D";
        }
        else if (deliveryType == "Naka Delivery without GST Rate")
        {
            type = "A";
        }
        else
        {
            type = "N";
        }
        txtDetailSaudaDate.Text = Server.HtmlDecode(gridViewRow.Cells[saudaDate].Text);
        txtDetailLiftingDate.Text = Server.HtmlDecode(gridViewRow.Cells[liftingDate].Text);
        drpDeliveryType.SelectedValue = type;
        txtBuyerParty.Text = Server.HtmlDecode(gridViewRow.Cells[Broker_No].Text);
        lblBuyerPartyName.Text = Server.HtmlDecode(gridViewRow.Cells[Name_Of_Broker].Text);
        txtBP_Account_Detail.Text = Server.HtmlDecode(gridViewRow.Cells[BP_Detail].Text);
        lblBP_Account_Detail.Text = Server.HtmlDecode(gridViewRow.Cells[BPDetailname].Text);
        if (gridViewRow.Cells[Shipto].Text.Trim() != "&nbsp;" || gridViewRow.Cells[Shipto].Text.Trim() != string.Empty)
        {
            txtShipTo.Text = Server.HtmlDecode(gridViewRow.Cells[Shipto].Text.Trim());
            lblShiptoname.Text = Server.HtmlDecode(gridViewRow.Cells[shiptoname].Text.Trim());
        }
        else
        {
            txtShipTo.Text = "0";
            lblShiptoname.Text = "0";
        }

        lblshiptoid.Text = Server.HtmlDecode(gridViewRow.Cells[Shiptoid].Text.Trim());

        txtcashdifference.Text = Server.HtmlDecode(gridViewRow.Cells[Cashdiff].Text);

        double buyerqntl = Convert.ToDouble(Server.HtmlDecode(gridViewRow.Cells[quantal].Text));
        txtBuyerQuantal.Text = Convert.ToString(Math.Abs(buyerqntl));
        double salerate = Convert.ToDouble(Server.HtmlDecode(gridViewRow.Cells[saleRate].Text));
        txtBuyerSaleRate.Text = Convert.ToString(Math.Abs(salerate));
        double buyercmmrate = Convert.ToDouble(Server.HtmlDecode(gridViewRow.Cells[commissions].Text));
        txtBuyerCommission.Text = Convert.ToString(Math.Abs(buyercmmrate));
        // txtBuyerNarration.Text = Server.HtmlDecode(gridViewRow.Cells[Sauda_Narration].Text);
        string BuyerNarration = gridViewRow.Cells[Sauda_Narration].Text;
        if (BuyerNarration == "&nbsp;")
        {
            txtBuyerNarration.Text = "";
        }
        else
        {
            txtBuyerNarration.Text = Server.HtmlDecode(gridViewRow.Cells[Sauda_Narration].Text);
        }
        txtsubBroker.Text = Server.HtmlDecode(gridViewRow.Cells[sub_broker].Text);
        lblsubBroker.Text = Server.HtmlDecode(gridViewRow.Cells[subBrokername].Text);
        txtGSTrate.Text = Server.HtmlDecode(gridViewRow.Cells[22].Text);
        txtgstamt.Text = Server.HtmlDecode(gridViewRow.Cells[23].Text);
        txtTCSrate1.Text = Server.HtmlDecode(gridViewRow.Cells[24].Text);
        txtTCSamount1.Text = Server.HtmlDecode(gridViewRow.Cells[25].Text);
        string loding_by_us = Server.HtmlDecode(gridViewRow.Cells[33].Text);
        if (loding_by_us == "Y")
        {
            chkLoding_Chk.Checked = true;
        }
        else
        {
            chkLoding_Chk.Checked = false;
        }
        TCSAmountCalculate();

        if (ViewState["currentTable"] != null)
        {

        }
        setFocusControl(txtBuyer);
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

    #region[btnMillCode_Click]
    protected void btnMillCode_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMillCode";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }
    }
    #endregion

    #region [txtMillCode_TextChanged]
    protected void txtMillCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "Close";
            string millName = string.Empty;
            searchString = txtMillCode.Text;
            if (txtMillCode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtMillCode.Text);
                if (a == false)
                {
                    btnMillCode_Click(this, new EventArgs());
                }
                else
                {
                    DataSet ds = clsDAL.SimpleQuery("select Ac_Name_E,accoid,Short_Name from qrymstaccountmaster where Ac_Code='" + txtMillCode.Text
                        + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Ac_type='M'");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            millName = ds.Tables[0].Rows[0]["Ac_Name_E"].ToString();
                            if (millName != string.Empty && millName != "0")
                            {
                                hdnfmc.Value = ds.Tables[0].Rows[0]["accoid"].ToString();
                                hdnfmillshort.Value = ds.Tables[0].Rows[0]["Short_Name"].ToString();

                                lblMillName.Text = millName;
                                setFocusControl(txtSeason);

                                if (btnSave.Text == "Save")
                                {
                                    txtPaymentTo.Text = txtMillCode.Text;
                                    lblPaymentTo.Text = millName;
                                    hdnfpt.Value = hdnfmc.Value;
                                }
                                // AmtCalculation();
                            }
                            else
                            {
                                txtMillCode.Text = string.Empty;
                                lblMillName.Text = string.Empty;
                                setFocusControl(txtMillCode);
                            }
                        }
                        else
                        {
                            txtMillCode.Text = string.Empty;
                            lblMillName.Text = string.Empty;
                            setFocusControl(txtMillCode);
                        }
                    }
                    else
                    {
                        txtMillCode.Text = string.Empty;
                        lblMillName.Text = string.Empty;
                        setFocusControl(txtMillCode);
                    }
                }
            }
            else
            {
                txtMillCode.Text = string.Empty;
                lblMillName.Text = millName;
                setFocusControl(txtMillCode);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnGrade_Click]
    protected void btnGrade_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "GR";
            btnSearch_Click(sender, e);
            setFocusControl(txtSearchText);

        }
        catch
        {

        }
    }
    #endregion

    #region [txtGrade_TextChanged]
    protected void txtGrade_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGrade.Text;
        if (txtGrade.Text != string.Empty)
        {
            bool a = true;
            if (txtGrade.Text.Length < 8)
            {
                a = clsCommon.isStringIsNumeric(txtGrade.Text);
            }
            if (a == false)
            {
                // btnGrade_Click(this, new EventArgs());
            }
            else
            {
                pnlPopup.Style["display"] = "none";
                setFocusControl(txtQuantal);
            }
        }
    }
    #endregion

    #region[btnPaymentTo_Click]
    protected void btnPaymentTo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPaymentTo";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }
    }
    #endregion

    #region [txtPaymentTo_TextChanged]
    protected void txtPaymentTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtPaymentTo.Text;
            string paymentToname = string.Empty;
            if (txtPaymentTo.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtPaymentTo.Text);
                if (a == false)
                {
                    btnPaymentTo_Click(this, new EventArgs());
                }
                else
                {
                    paymentToname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='" + txtPaymentTo.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (paymentToname != string.Empty && paymentToname != "0")
                    {
                        hdnfpt.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtPaymentTo.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (paymentToname.Length > 15)
                        {
                            paymentToname.Substring(0, 15);
                        }
                        else if (paymentToname.Length > 10)
                        {
                            paymentToname.Substring(0, 10);
                        }
                        lblPaymentTo.Text = paymentToname;
                        //AmtCalculation();
                        if (drpResale.SelectedValue == "M")
                        {
                            setFocusControl(txtBroker);
                        }
                        else
                        {
                            setFocusControl(txtTenderFrom);
                        }
                    }
                    else
                    {
                        txtPaymentTo.Text = string.Empty;
                        lblPaymentTo.Text = string.Empty;
                        setFocusControl(txtPaymentTo);
                        // AmtCalculation();
                    }
                }
            }
            else
            {
                txtPaymentTo.Text = string.Empty;
                lblPaymentTo.Text = paymentToname;
                setFocusControl(txtPaymentTo);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region[btnTenderFrom_Click]
    protected void btnTenderFrom_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "txtTenderFrom";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }
    }
    #endregion

    #region[txtTenderFrom_TextChanged]
    protected void txtTenderFrom_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtTenderFrom.Text;
            string tenderFromName = string.Empty;
            if (txtTenderFrom.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtTenderFrom.Text);
                if (a == false)
                {
                    btnTenderFrom_Click(this, new EventArgs());
                }
                else
                {
                    tenderFromName = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='" + txtTenderFrom.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (tenderFromName != string.Empty && tenderFromName != "0")
                    {
                        hdnftf.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtTenderFrom.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (tenderFromName.Length > 15)
                        {
                            tenderFromName.Substring(0, 15);
                        }
                        else if (tenderFromName.Length > 10)
                        {
                            tenderFromName.Substring(0, 10);
                        }
                        lblTenderFrom.Text = tenderFromName;
                        setFocusControl(txtDO);
                    }
                    else
                    {
                        txtTenderFrom.Text = string.Empty;
                        lblTenderFrom.Text = string.Empty;
                        setFocusControl(txtDO);
                    }
                }
            }
            else
            {
                txtTenderFrom.Text = string.Empty;
                lblTenderFrom.Text = tenderFromName;
                setFocusControl(txtDO);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region[btnTenderDO_Click]
    protected void btnTenderDO_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "DO";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }
    }
    #endregion

    #region[txtDO_TextChanged]
    protected void txtDO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtDO.Text;
            string doName = string.Empty;
            if (txtDO.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtDO.Text);
                if (a == false)
                {
                    btnTenderDO_Click(this, new EventArgs());
                }
                else
                {
                    doName = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='" + txtDO.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (doName != string.Empty && doName != "0")
                    {
                        hdnftdo.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtDO.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (doName.Length > 15)
                        {
                            doName.Substring(0, 15);
                        }
                        else if (doName.Length > 10)
                        {
                            doName.Substring(0, 10);
                        }
                        lblDO.Text = doName;

                        txtTenderFrom.Text = txtDO.Text;
                        lblTenderFrom.Text = doName;
                        txtVoucherBy.Text = txtDO.Text;
                        lblVoucherBy.Text = doName;
                        hdnfvoucherbygst.Value = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtVoucherBy.Text + " and " +
                          "Company_Code='" + Session["Company_Code"].ToString() + "'");
                        hdnftf.Value = hdnftdo.Value;
                        hdnfvb.Value = hdnftdo.Value;
                        setFocusControl(txtVoucherBy);
                    }
                    else
                    {
                        txtDO.Text = string.Empty;
                        lblDO.Text = string.Empty;
                        setFocusControl(txtDO);
                    }
                }
            }
            else
            {
                txtDO.Text = string.Empty;
                lblDO.Text = doName;
                setFocusControl(txtDO);
            }
            if (drpResale.SelectedValue == "R")
            {
                AmtCalculationforvoucher();
            }
            else
            {

                txtvtcsrate.Text = "0.00";
                txtvtdsrate.Text = "0.00";
            }
        }
        catch
        {
        }
    }
    #endregion

    #region[btnVoucherBy_Click]
    protected void btnVoucherBy_Click(object sender, EventArgs e)
    {
        try
        {

            hdnfClosePopup.Value = "txtVoucherBy";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);

        }
        catch
        {

        }
    }
    #endregion

    #region[txtVoucherBy_TextChanged]
    protected void txtVoucherBy_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtVoucherBy.Text;
            string voucherByName = string.Empty;
            if (txtVoucherBy.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtVoucherBy.Text);
                if (a == false)
                {
                    btnVoucherBy_Click(this, new EventArgs());
                }
                else
                {
                    voucherByName = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='" + txtVoucherBy.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (voucherByName != string.Empty && voucherByName != "0")
                    {
                        hdnfvoucherbygst.Value = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtVoucherBy.Text + " and " +
                            "Company_Code='" + Session["Company_Code"].ToString() + "'");
                        hdnfvb.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtVoucherBy.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (voucherByName.Length > 15)
                        {
                            voucherByName.Substring(0, 15);
                        }
                        else if (voucherByName.Length > 10)
                        {
                            voucherByName.Substring(0, 10);
                        }
                        lblVoucherBy.Text = voucherByName;
                        setFocusControl(txtBroker);
                    }
                    else
                    {
                        txtVoucherBy.Text = string.Empty;
                        lblVoucherBy.Text = string.Empty;
                        setFocusControl(txtVoucherBy);
                    }
                }
            }
            else
            {
                txtVoucherBy.Text = string.Empty;
                lblVoucherBy.Text = voucherByName;
                setFocusControl(txtVoucherBy);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region[btnBroker_Click]
    protected void btnBroker_Click(object sender, EventArgs e)
    {
        try
        {

            hdnfClosePopup.Value = "BR";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }
    }
    #endregion
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
    protected void txtBrokrage_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtExciseRate);

    }
    #region[txtBroker_TextChanged]
    protected void txtBroker_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtBroker.Text;
            string brokerName = string.Empty;
            if (txtBroker.Text != string.Empty)
            {

                bool a = clsCommon.isStringIsNumeric(txtBroker.Text);
                if (a == false)
                {
                    btnBroker_Click(this, new EventArgs());
                }
                else
                {
                    qry = "select Ac_Name_E,accoid from qrymstaccountmaster where Ac_Code='" + txtBroker.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";

                    brokerName = clsCommon.getString(qry);
                    if (brokerName != string.Empty && brokerName != "0")
                    {
                        hdnfbc.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtBroker.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (brokerName.Length > 15)
                        {
                            brokerName.Substring(0, 15);
                        }
                        else if (brokerName.Length > 10)
                        {
                            brokerName.Substring(0, 10);
                        }
                        lblBroker.Text = brokerName;
                        setFocusControl(txtBrokrage);
                    }
                    else
                    {
                        txtBroker.Text = "2";
                        setFocusControl(txtBrokrage);
                    }

                }
            }
            else
            {
                txtBroker.Text = string.Empty;
                lblBroker.Text = brokerName;
                setFocusControl(txtBroker);
            }
        }
        catch
        {
        }
    }
    #endregion

    protected void txtsubBroker_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtsubBroker.Text;
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
                                //    grdDetail.Rows[0].Cells[5].Text = Convert.ToString(txtBroker.Text);
                                //    grdDetail.Rows[0].Cells[6].Text = Convert.ToString(lblBroker.Text);
                                //}
                                setFocusControl(txtBuyerQuantal);
                            }
                            else
                            {
                                txtBroker.Text = "2";

                                setFocusControl(txtBuyerQuantal);
                            }
                        }
                    }

                    else
                    {
                        txtBroker.Text = string.Empty;
                        lblBroker.Text = string.Empty;
                        setFocusControl(txtBroker);
                    }
                }
            }
            else
            {
                txtBroker.Text = string.Empty;
                lblBroker.Text = brokerName;
                setFocusControl(txtBroker);
            }
        }
        catch
        {
        }
    }
    protected void txtGstrateCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtGstrateCode.Text;
            string GstrateCode = string.Empty;
            if (txtGstrateCode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtGstrateCode.Text);
                if (a == false)
                {
                    btnGstrateCode_Click(this, new EventArgs());

                }
                else
                {
                    GstrateCode = clsCommon.getString("select Rate from nt_1_gstratemaster where Doc_no='" + txtGstrateCode.Text + "' ");
                    if (GstrateCode != string.Empty && GstrateCode != "0")
                    {
                        if (GstrateCode.Length > 15)
                        {
                            GstrateCode.Substring(0, 15);
                        }
                        else if (GstrateCode.Length > 10)
                        {
                            GstrateCode.Substring(0, 10);
                        }
                        lblgstrateCode.Text = GstrateCode;
                        calculateAmount();
                        GSTCode();
                        setFocusControl(txtExciseRate);

                    }
                    else
                    {
                        txtGstrateCode.Text = string.Empty;
                        lblgstrateCode.Text = string.Empty;
                        setFocusControl(txtGstrateCode);
                    }
                }
            }
            else
            {
                txtGstrateCode.Text = string.Empty;
                lblPaymentTo.Text = GstrateCode;
                setFocusControl(txtGstrateCode);
            }

        }
        catch
        {
        }
    }
    protected void btnGstrateCode_Click(object sender, EventArgs e)
    {
        //  pnlPopupTenderDetails.Style["display"] = "block";
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "GC";
        btnSearch_Click(sender, e);
    }

    #region[btnBuyer_Click]
    protected void btnBuyer_Click(object sender, EventArgs e)
    {
        //  pnlPopupTenderDetails.Style["display"] = "block";
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "BU";
        btnSearch_Click(sender, e);
    }
    #endregion

    #region[txtBuyer_TextChanged]
    protected void txtBuyer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtBuyer.Text;
            string buyerName = string.Empty;
            if (txtBuyer.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtBuyer.Text);
                if (a == false)
                {
                    btnBuyer_Click(this, new EventArgs());
                }
                else
                {
                    qry = "select Ac_Name_E,accoid from qrymstaccountmaster where Ac_Code=" + txtBuyer.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                                lblbuyer_id.Text = dt.Rows[0]["accoid"].ToString();

                                txtShipTo.Text = txtBuyer.Text;
                                lblShiptoname.Text = buyerName;
                                lblshiptoid.Text = dt.Rows[0]["accoid"].ToString();


                                string dd = "";

                                dd = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                                DateTime Headliftingdate = Convert.ToDateTime(dd);
                                string Docdate = Headliftingdate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                string Dlift = System.DateTime.Now.ToString("dd/MM/yyyy");
                                string d = DateTime.Parse(Dlift, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                                DateTime DetailLift = Convert.ToDateTime(d);


                                if (Headliftingdate > DetailLift)
                                {
                                    txtDetailLiftingDate.Text = txtLiftingDate.Text;
                                }
                                else
                                {
                                    txtDetailLiftingDate.Text = Dlift;
                                }

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
                        txtBuyer.Focus();
                        setFocusControl(txtBuyer);
                    }
                }
            }
            else
            {
                txtBuyer.Text = string.Empty;
                lblBuyerName.Text = buyerName;
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

    #region [txtQuantal_TextChanged]
    protected void txtQuantal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "none";
            string pck = "0";
            pck = clsCommon.getString("select KgPerKatta from qrymstitem where System_Type='I' and System_Code=" + txtitem_code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            txtPacking.Text = pck;
            if (txtQuantal.Text != string.Empty && txtPacking.Text != string.Empty && txtQuantal.Text != "0" && txtPacking.Text != "0")
            {
                int bags = Convert.ToInt32(Math.Round(float.Parse(txtQuantal.Text) * (100 / float.Parse(txtPacking.Text))));
                txtBags.Text = bags.ToString();
            }
            else if ((txtPacking.Text == string.Empty || txtPacking.Text == "0") && txtQuantal.Text != string.Empty && txtQuantal.Text != "0")
            {
                txtPacking.Text = "50";
                int bags = Convert.ToInt32(Math.Round(float.Parse(txtQuantal.Text) * (100 / float.Parse(txtPacking.Text))));
                txtBags.Text = bags.ToString();
                setFocusControl(txtPacking);
            }
            else
            {
                txtQuantal.Text = string.Empty;
                setFocusControl(txtQuantal);
                txtBags.Text = "0";
            }

            TCSAmountCalculate();

            if (grdDetail.Rows.Count == 0)
            {
                this.btnADDBuyerDetails_Click(sender, e);
            }
            else
            {
                #region decide whether actual row is updating or virtual [rowAction]
                string id = clsCommon.getString("select ID from " + tblDetails + " where Tender_No='" + txtTenderNo.Text +
                    "' and ID='1' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //" and year_code=" + Convert.ToInt32(Session["year"].ToString()) + " ");
                DataRow dr = null;
                DataTable dt = (DataTable)ViewState["currentTable"];

                dr = dt.Rows[0];
                if (id != string.Empty && id != "0")
                {
                    dr["rowAction"] = "U";   //actual row
                }
                else
                {
                    dr["rowAction"] = "A";    //virtual row
                }


                #endregion
                ViewState["currentTable"] = dt;
                grdDetail.DataSource = dt;
                grdDetail.DataBind();
                GridViewRow gr = (GridViewRow)grdDetail.Rows[0];

                gr.Enabled = false;
            }

            this.calculateAmount();
            this.calculateBalanceSelf();
            this.TCSCalculation();
            this.TdsCalculation();
            setFocusControl(txtPacking);
        }
        catch
        {

        }
        SetFocus(txtPacking);
    }
    #endregion

    #region[txtPacking_TextChanged]
    protected void txtPacking_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtQuantal.Text != string.Empty && txtPacking.Text != string.Empty && txtQuantal.Text != "0" && txtPacking.Text != "0")
            {
                int bags = Convert.ToInt32(Math.Round(float.Parse(txtQuantal.Text) * (100 / float.Parse(txtPacking.Text))));
                txtBags.Text = bags.ToString();
                setFocusControl(txtMillRate);
            }
            else if ((txtPacking.Text == string.Empty || txtPacking.Text == "0") && txtQuantal.Text != string.Empty && txtQuantal.Text != "0")
            {
                txtPacking.Text = "50";
                int bags = Convert.ToInt32(Math.Round(float.Parse(txtQuantal.Text) * (100 / float.Parse(txtPacking.Text))));
                txtBags.Text = bags.ToString();
                setFocusControl(txtMillRate);
            }
            else
            {
                txtQuantal.Text = string.Empty;
                setFocusControl(txtQuantal);
                txtBags.Text = "0";
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [txtDate_TextChanged]
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != string.Empty)
            {
                try
                {
                    string scdate = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                    DateTime d = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                    int millpaymentdate = Convert.ToInt32(hdnfmillpaymentdate.Value);
                    d = d.AddDays(+(Convert.ToInt32(millpaymentdate)));
                    txtLiftingDate.Text = d.ToString("dd/MM/yyyy");
                    setFocusControl(txtLiftingDate);
                }
                catch
                {
                    txtDate.Text = string.Empty;
                }
            }
            else
            {
                setFocusControl(txtDate);
            }
            //if (txtDate.Text != string.Empty)
            //{
            //    DateTime d = new DateTime();
            //    // d = DateTime.Now;
            //    string date = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //    //  d = DateTime.Parse(date);
            //    // d = d.AddDays(15);
            //    int millpaymentdate = Convert.ToInt32(hdnfmillpaymentdate.Value);
            //    // txtLiftingDate.Text = clsCommon.getString("select adddate('" + date + "', interval 10 DAY) as d");
            //    txtLiftingDate.Text = clsCommon.getString("select DATEADD(day,"+millpaymentdate+",'" + date + "')");
            //    txtLiftingDate.Text = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //    setFocusControl(txtLiftingDate);
            //}
            //else
            //{
            //    setFocusControl(txtDate);
            //}
        }
        catch
        {
            txtDate.Text = string.Empty;
            setFocusControl(txtDate);
            calenderExtenderDate.Animated = true;
        }
    }
    #endregion

    #region [txtLiftingDate_TextChanged]
    protected void txtLiftingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtLiftingDate.Text != string.Empty)
            {
                // string d = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                //DateTime oDate = DateTime.ParseExact(txtLiftingDate.Text, "yyyy/MM/dd", null);
                string liftdate = DateTime.Parse(txtLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                DateTime oDate = DateTime.ParseExact(liftdate, "yyyy/MM/dd", null);
                string d = DateTime.Now.ToString("yyyy/MM/dd");
                DateTime currentDate = DateTime.ParseExact(d, "yyyy/MM/dd", null);

                if (currentDate >= oDate)
                {
                    txtDetailLiftingDate.Text = txtLiftingDate.Text;
                }
                else
                {
                    txtDetailLiftingDate.Text = DateTime.Now.ToString("dd/MM/yyyy"); ;
                }
                setFocusControl(txtMillCode);
            }
        }
        catch
        {
            txtLiftingDate.Text = string.Empty;
            setFocusControl(txtLiftingDate);
        }
    }
    #endregion

    #region [txtDetailSaudaDate_TextChanged]
    protected void txtDetailSaudaDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDetailSaudaDate.Text != string.Empty)
            {
                //string d = DateTime.Parse(txtDetailSaudaDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
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
                // string d = DateTime.Parse(txtDetailLiftingDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                setFocusControl(txtBuyerNarration);
            }
        }
        catch
        {
            txtDetailLiftingDate.Text = string.Empty;
            setFocusControl(txtDetailLiftingDate);
        }
    }
    #endregion

    #region[txtMillRate_TextChanged]
    protected void txtMillRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtMillRate.Text != string.Empty)
            {
                setFocusControl(txtCashDiff);
                this.calculateAmount();
                this.TCSCalculation();
                this.TdsCalculation();
                if (drpResale.SelectedValue == "R" || drpResale.SelectedValue == "W")
                {
                    this.calculateDiff();
                }
                if (drpResale.SelectedValue == "M")
                {

                    this.setFocusControl(txtCashDiff);
                }
                else
                {
                    this.setFocusControl(txtPurcRate);
                }
            }
            txtBuyerSaleRate.Text = txtMillRate.Text;
            txtpartybillrate.Text = txtMillRate.Text;

            pnlPopup.Style["display"] = "none";
        }
        catch
        {

        }
    }
    #endregion

    #region [calculateDiff]
    protected void calculateDiff()
    {
        try
        {
            float millrate = 0;
            float purcRate = 0;
            float diff = 0;
            if (txtMillRate.Text != string.Empty)
            {
                millrate = float.Parse(txtMillRate.Text);
            }
            if (txtPurcRate.Text != string.Empty)
            {
                purcRate = float.Parse(txtPurcRate.Text);
            }

            diff = millrate - purcRate;
            lbldiff.Text = diff.ToString();
        }
        catch
        {

        }
    }
    #endregion

    #region [calculateAmount]
    protected void calculateAmount()
    {
        try
        {
            float quantal = 0;
            float millrate = 0;
            double amount = 0;
            float purcrate = 0;
            float diff = 0;

            if (txtQuantal.Text != string.Empty)
            {
                quantal = float.Parse(txtQuantal.Text);
            }
            //if (txtMillRate.Text != string.Empty)
            //{
            //    millrate = float.Parse(txtMillRate.Text);
            //}
            if (txtpartybillrate.Text != string.Empty && txtpartybillrate.Text != "0")
            {
                millrate = float.Parse(txtpartybillrate.Text);

            }
            else
            {
                millrate = float.Parse(txtMillRate.Text);
            }
            if (txtPurcRate.Text != string.Empty)
            {
                purcrate = float.Parse(txtPurcRate.Text);
            }
            if (lbldiff.Text != string.Empty)
            {
                diff = float.Parse(lbldiff.Text);
            }

            if (drpResale.SelectedValue == "M")
            {
                amount = quantal * millrate;

            }
            else
            {
                amount = quantal * diff;
            }
            lblAmount.Text = Math.Round(amount, 2).ToString();

            double value = 0.00;
            double gstrate = Convert.ToDouble(lblgstrateCode.Text);

            gstrate = Math.Round((millrate * gstrate / 100), 2);

            txtExciseRate.Text = gstrate.ToString();
            lblMillRateGst.Text = (millrate + gstrate).ToString();
            value = quantal * (millrate + gstrate);
            lblValue.Text = value.ToString();
        }
        catch
        {

        }
    }
    #endregion

    #region[calculateBalanceSelf]
    /// <summary>
    /// /////////////
    /// </summary>
    protected void calculateBalanceSelf()
    {
        try
        {
            float quantal = 0;
            float balanceSelf = 0;
            float buyerQuantal = 0;
            float quantalTotal = 0;

            //calculate total of quantals in grid

            for (int i = 1; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[rowAction].Text != "D" && grdDetail.Rows[i].Cells[rowAction].Text != "R")
                {
                    //                   if (grdDetail.Rows[i].RowIndex != 0)
                    //                 {
                    quantalTotal = quantalTotal + float.Parse(grdDetail.Rows[i].Cells[9].Text);
                    //               }
                }
            }
            //  quantalTotal = quantalTotal + buyerQuantal;

            if (txtQuantal.Text != string.Empty)
            {
                quantal = float.Parse(txtQuantal.Text);
            }

            if (lblBalanceSelf.Text != string.Empty)
            {
                balanceSelf = float.Parse(lblBalanceSelf.Text);
            }
            if (txtBuyerQuantal.Text != string.Empty)
            {
                buyerQuantal = float.Parse(txtBuyerQuantal.Text);
            }
            balanceSelf = quantal - quantalTotal;
            lblBalanceSelf.Text = balanceSelf.ToString();

            //set to first row balance self
            grdDetail.Rows[0].Cells[9].Text = balanceSelf.ToString();
            //  grdDetail.Rows[0].Cells[12].Text = "U";
            GridViewRow gr = (GridViewRow)grdDetail.Rows[0];
            gr.Enabled = false;
        }
        catch
        {

        }
    }
    #endregion

    #region [txtPurcRate_TextChanged]
    protected void txtPurcRate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            this.calculateDiff();
            this.calculateAmount();
            this.TCSCalculation();
            this.TdsCalculation();
            setFocusControl(txtPaymentTo);
        }
        catch
        {

        }
    }
    #endregion

    #region [drpResale_SelectedIndexChanged]
    protected void drpResale_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpResale.SelectedValue == "M")
            {
                txtPurcRate.Enabled = false;
                rfvtxtPurcRate.Enabled = false;

                txtTenderFrom.Text = "2";
                lblTenderFrom.Text = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='2' and " +
                    "Company_Code=" + Session["Company_Code"].ToString() + "");
                txtDO.Text = "2";
                lblDO.Text = lblTenderFrom.Text;
                txtVoucherBy.Text = "2";
                lblVoucherBy.Text = lblTenderFrom.Text;

            }
            else
            {
                txtPurcRate.Enabled = true;
                //setFocusControl(txtDate);
                rfvtxtPurcRate.Enabled = true;

                txtTenderFrom.Text = string.Empty;
                lblTenderFrom.Text = string.Empty;
                txtDO.Text = string.Empty;
                lblDO.Text = string.Empty;
                txtVoucherBy.Text = string.Empty;
                lblVoucherBy.Text = string.Empty;
            }

            setFocusControl(drpResale);

        }
        catch
        {

        }
    }
    #endregion

    #region [txtTenderNo_TextChanged]
    protected void txtTenderNo_TextChanged(object sender, EventArgs e)
    {
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtTenderNo.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtTenderNo.Text != string.Empty)
                {
                    txtValue = txtTenderNo.Text;

                    string qry = "select * from " + tblHead + " where Tender_No='" + txtValue +
                        "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                    //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ";
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //Record Found
                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Tender No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        txtTenderNo.Enabled = false;
                                        //Session["enableSave"] = 1;
                                        btnSave.Enabled = true;   //IMP
                                        setFocusControl(txtVoucherBy);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        bool recordExist = this.fetchRecord(txtValue);
                                        if (recordExist == true)
                                        {
                                            //txtTenderNo.Enabled = true;
                                            pnlgrdDetail.Enabled = true;
                                            setFocusControl(drpResale);
                                        }
                                    }
                                }
                            }
                            else   //Record Not Found
                            {
                                if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                {
                                    lblMsg.Text = "";
                                    setFocusControl(drpResale);
                                    txtTenderNo.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                    calculateBalanceSelf();
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("A");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtTenderNo.Text = string.Empty;
                                    setFocusControl(txtTenderNo);
                                    calculateBalanceSelf();
                                    //txtTenderNo.Enabled = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    txtTenderNo.Focus();
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Tender No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtTenderNo.Text = string.Empty;
                txtTenderNo.Focus();
            }
        }
        catch
        {

        }
        #endregion
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


            //if (searchString != string.Empty)
            //{
            //    txtSearchText.Text = searchString;
            //}
            //else
            //{
            //    txtSearchText.Text = txtSearchText.Text;
            //}

            if (hdnfClosePopup.Value == "txtMillCode")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += " ( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' or city_name_e like '%" + aa + "%' or Short_Name like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                // lblPopupHead.Text = "--Select Group--";

                string qry = " Locked=0 and Ac_type='M' and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and (" + name + ") order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "GR")
            {
                if (txtGrade.Text != string.Empty)
                {
                    split = txtGrade.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( System_Name_E like '%" + aa + "%' or System_Type like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Group--";
                string qry = "select System_Name_E from " + tblPrefix + "SystemMaster where System_Type='S' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and System_Name_E like '%" + txtSearchText.Text + "%' ";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPaymentTo")
            {
                split = txtPaymentTo.Text.Split(delimiter);
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or city_name_e like '%" + aa + "%' ) and";

                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Group--";
                //string qry = "select Ac_Code , Ac_Name_E,cityname from " + AccountMasterTable + " where Locked=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and " + name + " order by Ac_Name_E asc";
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and (" + name + ") order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtTenderFrom")
            {
                // txtSearchText.Text = txtTenderFrom.Text;
                lblPopupHead.Text = "--Select Tender From--";
                //string qry = "select Ac_Code , Ac_Name_E ,cityname from " + AccountMasterTable +
                //    " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                         + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "DO")
            {
                //    txtSearchText.Text = txtDO.Text;
                lblPopupHead.Text = "--Select DO--";
                //string qry = "select Ac_Code , Ac_Name_E ,cityname from " + AccountMasterTable +
                //    " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                      + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtVoucherBy")
            {
                //txtSearchText.Text = txtVoucherBy.Text;
                lblPopupHead.Text = "--Select Voucher By--";
                //string qry = "select Ac_Code ,Ac_Name_E,cityname  from " + AccountMasterTable + " where Locked=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "BR")
            {
                //txtSearchText.Text = txtBroker.Text;
                lblPopupHead.Text = "--Select Broker--";
                //string qry = "select Ac_Code , Ac_Name_E ,cityname  from " + AccountMasterTable + " where Locked=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //this.showPopup(qry)
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "SubBrker")
            {

                //txtSearchText.Text = txtsubBroker.Text;
                lblPopupHead.Text = "--Select Buyer--";
                //string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                //            + " FROM  AccountMaster "
                //            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" + txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //qry = qry.Replace("AccountMaster", AccountMasterTable);
                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }

            if (hdnfClosePopup.Value == "BU")
            {
                //  txtSearchText.Text = txtBuyer.Text;
                lblPopupHead.Text = "--Select Buyer--";
                //string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                //            + " FROM  AccountMaster "
                //            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" + txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //qry = qry.Replace("AccountMaster", AccountMasterTable);
                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                   + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtShipTo")
            {
                //  txtSearchText.Text = txtBuyer.Text;
                lblPopupHead.Text = "--Select Buyer--";
                //string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                //            + " FROM  AccountMaster "
                //            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" + txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //qry = qry.Replace("AccountMaster", AccountMasterTable);
                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "BP")
            {
                //txtSearchText.Text = txtBuyerParty.Text;
                lblPopupHead.Text = "--Select Buyer Party--";
                //string qry = "SELECT     Ac_Code , Ac_Name_E , Short_Name, cityname  "
                //            + " FROM  AccountMaster "
                //            + " where AccountMaster.Locked=0 and AccountMaster.Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (AccountMaster.Ac_Code like'%" + txtSearchText.Text + "%' or AccountMaster.Ac_Name_E like'%" + txtSearchText.Text + "%' or AccountMaster.Short_Name like'%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //qry = qry.Replace("AccountMaster", AccountMasterTable);
                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "GC")
            {
                //txtSearchText.Text = txtGstrateCode.Text;
                lblPopupHead.Text = "--Select Buyer Party--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where  ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name"; ;

                this.showPopup(qry);
            }


            if (hdnfClosePopup.Value == "TN" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                //txtSearchText.Text = txtTenderNo.Text;
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Tender --";
                // string qry = "select distinct Tender_No,CONVERT(Date, Tender_Date,103) as Tender_Date,millname,Quantal,Packing,Mill_Rate,doname from " + qryCommon + " where ([millname] like '%" + txtSearchText.Text + "%' or Tender_No like '%" + txtSearchText.Text + "%' or doname like '%" + txtSearchText.Text + "%') and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by Tender_No desc";

                qry = "SELECT  Tender_No, Tender_Date,  millname, Quantal,Grade,buyerbrokershortname, Buyer_Quantal,Mill_Rate, Sale_Rate,doname" +
                " FROM  " + qryCommon + " where Buyer=2 and (Tender_No like '%" + txtSearchText.Text + "%' or Tender_Date like '%" + txtSearchText.Text + "%' or millname like '%" + txtSearchText.Text + "%' or millfullname like '%" + txtSearchText.Text + "%') and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by Tender_No desc";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtitem_code")
            {
                //txtSearchText.Text = txtitem_code.Text;
                lblPopupHead.Text = "--Select item--";
                //string qry = "select itemcode ,itemname as Name from qrytenderhead where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString() + " " +
                //    " and Year_Code=" + Session["year"].ToString() + " and (itemcode like '%" + txtSearchText.Text + "%' or  itemname like  '%" + txtSearchText.Text + "%'");

                qry = "select  System_Code,System_Name_E,Vat_AC as Gst,gstratre from qrymstitem where   (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "BPAccount")
            {
                //txtSearchText.Text = txtBroker.Text;
                lblPopupHead.Text = "--Select BPAccount--";
                //name += " ( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' or city_name_e like '%" + aa + "%' or Short_Name like '%" + aa + "%' ) and";

                //string qry = "select Ac_Code , Ac_Name_E ,cityname  from " + AccountMasterTable + " where Locked=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "BPDetail")
            {
                //txtSearchText.Text = txtBroker.Text;
                lblPopupHead.Text = "--Select BP Account--";
                //string qry = "select Ac_Code , Ac_Name_E ,cityname  from " + AccountMasterTable + " where Locked=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' " + "and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E asc";
                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
            }
            //  hdnfClosePopup.Value = "";
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
            if (hdnfClosePopup.Value == "MM")
            {
                setFocusControl(txtMillCode);
            }
            if (hdnfClosePopup.Value == "GR")
            {
                setFocusControl(txtGrade);
            }
            if (hdnfClosePopup.Value == "PT")
            {
                setFocusControl(txtPaymentTo);
            }
            if (hdnfClosePopup.Value == "TF")
            {
                setFocusControl(txtTenderFrom);
            }
            if (hdnfClosePopup.Value == "DO")
            {
                setFocusControl(txtDO);
            }
            if (hdnfClosePopup.Value == "VB")
            {
                setFocusControl(txtVoucherBy);
            }
            if (hdnfClosePopup.Value == "BR")
            {
                setFocusControl(txtBroker);
            }
            if (hdnfClosePopup.Value == "BU")
            {
                setFocusControl(txtBuyer);
            }
            if (hdnfClosePopup.Value == "BP")
            {
                setFocusControl(txtBuyerParty);
            }
            if (hdnfClosePopup.Value == "BU")
            {
                setFocusControl(txtBuyer);
            }
            if (hdnfClosePopup.Value == "BPAccount")
            {
                setFocusControl(txtBP_Account);
            }
            if (hdnfClosePopup.Value == "BPDetail")
            {
                setFocusControl(txtBP_Account_Detail);
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

    #region [btnChangeNo_Click]
    protected void changeNo_click(object sender, EventArgs e)
    {
        try
        {

            //if (hdnfClosePopup.Value =="TN")
            //{

            if (btnChangeNo.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtTenderNo.Text = string.Empty;
                txtTenderNo.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtTenderNo);

            }
            if (btnChangeNo.Text == "Choose No")
            {
                try
                {
                    pnlgrdDetail.Enabled = true;
                    //setFocusControl(txtSearchText);
                    hdnfClosePopup.Value = "TN";
                    pnlPopup.Style["display"] = "block";
                    btnSearch_Click(sender, e);

                }
                catch
                {

                }
                //}

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

            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[Detail_Id].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[Party].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[Name_of_Party].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Broker_No].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Name_Of_Broker].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[quantal].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[saleRate].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[commissions].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[saudaDate].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[liftingDate].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[Sauda_Narration].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[delivaryType].Style["overflow"] = "hidden";
            e.Row.Cells[delivaryType].ToolTip = e.Row.Cells[10].Text;
            e.Row.Cells[delivaryType].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[tenderdetailid].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[sub_broker].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[subBrokername].ControlStyle.Width = new Unit("180px");
            e.Row.Cells[rowAction].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[SrNo].ControlStyle.Width = new Unit("50px");

            e.Row.Cells[Shipto].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[shiptoname].ControlStyle.Width = new Unit("180px");
            e.Row.Cells[Cashdiff].ControlStyle.Width = new Unit("90px");

            //e.Row.Cells[19].ControlStyle.Width = new Unit("40px");
            //e.Row.Cells[20].ControlStyle.Width = new Unit("40px");
            //e.Row.Cells[21].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[partyid].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[Dbc].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[Sbc].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[DGSTRate].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[DGSTAmt].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[DTCSRate].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[DTCSAmount].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[Shiptoid].ControlStyle.Width = new Unit("90px");
            e.Row.Cells[BP_Detail].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[BPDetailname].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[bpid].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[loding_by_us].ControlStyle.Width = new Unit("120px");

            e.Row.Cells[delivaryType].Visible = true;
            e.Row.Cells[tenderdetailid].Visible = true;
            e.Row.Cells[rowAction].Visible = true;
            e.Row.Cells[SrNo].Visible = true;
            e.Row.Cells[0].Style["overflow"] = "hidden";
            e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[2].Style["overflow"] = "hidden";
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[5].Style["overflow"] = "hidden";
            e.Row.Cells[6].Style["overflow"] = "hidden";
            e.Row.Cells[7].Style["overflow"] = "hidden";
            e.Row.Cells[8].Style["overflow"] = "hidden";
            e.Row.Cells[9].Style["overflow"] = "hidden";
            e.Row.Cells[10].Style["overflow"] = "hidden";
            e.Row.Cells[11].Style["overflow"] = "hidden";
            e.Row.Cells[22].Style["overflow"] = "hidden";
            e.Row.Cells[23].Style["overflow"] = "hidden";
            e.Row.Cells[24].Style["overflow"] = "hidden";
            e.Row.Cells[25].Style["overflow"] = "hidden";
            e.Row.Cells[31].Style["overflow"] = "hidden";
            e.Row.Cells[subBrokername].Style["overflow"] = "hidden";
            e.Row.Cells[4].ToolTip = e.Row.Cells[4].Text;
            e.Row.Cells[8].ToolTip = e.Row.Cells[8].Text;

            int i = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;

                //i++;
                //foreach (TableCell cell in e.Row.Cells)
                //{
                //    string s = cell.Text.ToString();
                //    if (cell.Text.Length > 33)
                //    {
                //        cell.Text = cell.Text.Substring(0, 33) + "...";
                //        cell.ToolTip = s;
                //    }
                //}
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
        {
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                if (v == "TN")
                {

                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[2].Width = new Unit("100px");
                    e.Row.Cells[3].Width = new Unit("80px");
                    e.Row.Cells[4].Width = new Unit("100px");

                }
                if (v == "txtitem_code")
                {
                    e.Row.Cells[0].Width = new Unit("50px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("60px");
                    e.Row.Cells[3].Width = new Unit("100px");
                }
                if (v == "txtVoucherBy" || v == "GC")
                {
                    e.Row.Cells[0].Width = new Unit("100px");
                    e.Row.Cells[1].Width = new Unit("400px");
                    e.Row.Cells[2].Width = new Unit("100px");

                }
                if (v == "txtMillCode")
                {

                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("300px");
                    e.Row.Cells[2].Width = new Unit("150px");
                    e.Row.Cells[3].Width = new Unit("30px");
                }


                if (v == "txtPaymentTo" || v == "txtTenderFrom" || v == "DO" || v == "txtVoucherBy" || v == "BR")
                {
                    //if (e.Row.RowType == DataControlRowType.DataRow)
                    //{
                    e.Row.Cells[0].Width = new Unit("150px");
                    e.Row.Cells[1].Width = new Unit("500px");
                    e.Row.Cells[2].Width = new Unit("300px");
                    //}
                    //if (e.Row.RowType != DataControlRowType.Pager)
                    //{
                    //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    ////e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                    ////e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(60);
                    ////e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(30);
                    //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    //}
                }
                if (v == "GR")
                {
                    e.Row.Cells[0].Width = new Unit("400px");

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

            }
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
                strTextbox = hdnfClosePopup.Value;

                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBuyerQuantal_TextChanged]
    protected void txtBuyerQuantal_TextChanged(object sender, EventArgs e)
    {

        TCSAmountCalculate();
        if (btnADDBuyerDetails.Text == "ADD")
        {
            txtBuyerSaleRate.Text = "";
        }
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
            value = Convert.ToDouble(lblValue.Text);
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
        setFocusControl(txtcashdifference);
        TCSAmountCalculate();
    }
    #endregion

    #region [txtBuyerCommission_TextChanged]
    protected void txtBuyerCommission_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtDetailSaudaDate);
        TCSAmountCalculate();
    }
    #endregion

    #region [txtBuyerNarration_TextChanged]
    protected void txtBuyerNarration_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnADDBuyerDetails);
    }
    #endregion

    #region [txtExciseRate_TextChanged]
    protected void txtExciseRate_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtSellNoteNo);
    }
    #endregion

    #region [txtNarration_TextChanged]
    protected void txtNarration_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtGSTrate);
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    protected void btn_Click(object sender, EventArgs e)
    {
        //string url = "http://www.google.com";
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.open('");
        //sb.Append(url);
        //sb.Append(",'_blank'");
        //sb.Append("');");
        //sb.Append("</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
    }

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

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

    protected void drptempertender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(txtDate);
        }
        catch (Exception)
        {
        }
    }
    protected void txtSellNoteNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(txtNarration);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {

        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Enter Only Numbers!')", true);
            }
            else
            {
                int Tender_id = Convert.ToInt32(clsCommon.getString("select tenderid from nt_1_tender where Tender_No=" + txtEditDoc_No.Text +
                    " and Company_Code=" + Session["Company_Code"].ToString()));
                //   + " and " +  " Year_Code=" + Session["year"].ToString() + ""));
                hdnf.Value = Tender_id.ToString();
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select isnull(count(Tender_No),0) as Tender_No from " + tblHead +
                " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            //"' and Year_Code='" + Session["Year"].ToString() + "'"));
            if (counts == 0)
            {
                txtTenderNo.Text = "1";
                Tender_No = 1;
            }
            else
            {
                Tender_No = Convert.ToInt32(clsCommon.getString("SELECT max(Tender_No) as Tender_No from " + tblHead +
                    " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
                //"' and  Year_Code='" + Session["Year"].ToString() + "'")) + 1;
                txtTenderNo.Text = Tender_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(tenderid),0) as tenderid from " + tblHead + " "));
            if (counts == 0)
            {
                lblTender_Id.Text = "1";
                id = 1;
            }
            else
            {
                id = Convert.ToInt32(clsCommon.getString("SELECT max(tenderid) as tenderid from " + tblHead)) + 1;
                lblTender_Id.Text = id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion

    protected void txtcopyfrom_TextChanged(object sender, EventArgs e)
    {


        int Tender_id = Convert.ToInt32(clsCommon.getString("select tenderid from nt_1_tender where Tender_No=" + txtcopyfrom.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " and " +
            " Year_Code=" + Session["year"].ToString() + ""));
        hdnf.Value = Tender_id.ToString();

        string qry = "select * from " + qryHead +
                " where tenderid =" + hdnf.Value;

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
                    hdnf.Value = dt.Rows[0]["tenderid"].ToString();
                    txtDate.Text = dt.Rows[0]["Tender_DateConverted"].ToString();
                    txtLiftingDate.Text = dt.Rows[0]["Lifting_DateConverted"].ToString();
                    lblTender_Id.Text = hdnf.Value;
                    hdnftenderid.Value = hdnf.Value;

                    txtCashDiff.Text = dt.Rows[0]["CashDiff"].ToString();

                    txtMillCode.Text = dt.Rows[0]["Mill_Code"].ToString();
                    txtSeason.Text = dt.Rows[0]["season"].ToString();
                    txtitem_code.Text = dt.Rows[0]["itemcode"].ToString();
                    lblitemname.Text = dt.Rows[0]["itemname"].ToString();
                    txtGrade.Text = dt.Rows[0]["Grade"].ToString();
                    txtQuantal.Text = dt.Rows[0]["Quantal"].ToString();//Convert.ToString(Math.Abs(Convert.ToDouble(dt.Rows[0]["Quantal"].ToString())));
                    txtPacking.Text = dt.Rows[0]["Packing"].ToString();
                    txtBags.Text = dt.Rows[0]["Bags"].ToString();
                    txtMillRate.Text = dt.Rows[0]["Mill_Rate"].ToString();
                    txtpartybillrate.Text = dt.Rows[0]["Party_Bill_Rate"].ToString();


                    txtTenderFrom.Text = dt.Rows[0]["Tender_From"].ToString();
                    txtPaymentTo.Text = dt.Rows[0]["Payment_To"].ToString();
                    txtDO.Text = dt.Rows[0]["Tender_DO"].ToString();
                    txtVoucherBy.Text = dt.Rows[0]["Voucher_By"].ToString();
                    txtBroker.Text = dt.Rows[0]["Broker"].ToString();
                    txtExciseRate.Text = dt.Rows[0]["Excise_Rate"].ToString();
                    txtSellNoteNo.Text = dt.Rows[0]["Sell_Note_No"].ToString();
                    txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                    txtGstrateCode.Text = dt.Rows[0]["gstratecode"].ToString();
                    lblgstrateCode.Text = dt.Rows[0]["gstrate"].ToString();
                    hdnfmc.Value = dt.Rows[0]["mc"].ToString();
                    hdnfpt.Value = dt.Rows[0]["pt"].ToString();
                    hdnftf.Value = dt.Rows[0]["tf"].ToString();
                    hdnftdo.Value = dt.Rows[0]["td"].ToString();
                    hdnfvb.Value = dt.Rows[0]["vb"].ToString();
                    hdnfbc.Value = dt.Rows[0]["bk"].ToString();
                    hdnfic.Value = dt.Rows[0]["ic"].ToString();
                    txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                    txtTCSAmt.Text = dt.Rows[0]["TCS_Amt"].ToString();


                    hdnfvoucherbygst.Value = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtVoucherBy.Text + " and " +
                       "Company_Code='" + Session["Company_Code"].ToString() + "'");



                    Int32 voucherno = dt.Rows[0]["Voucher_No"].ToString().Trim() != string.Empty ? Convert.ToInt32(dt.Rows[0]["Voucher_No"].ToString()) : 0;
                    vouchernumber.Value = voucherno.ToString();
                    lblMillName.Text = dt.Rows[0]["millname"].ToString();
                    lblPaymentTo.Text = dt.Rows[0]["paymenttoname"].ToString();
                    lblTenderFrom.Text = dt.Rows[0]["tenderfromname"].ToString();
                    lblVoucherBy.Text = dt.Rows[0]["voucherbyname"].ToString();
                    lblDO.Text = dt.Rows[0]["tenderdoname"].ToString();
                    lblBroker.Text = dt.Rows[0]["brokername"].ToString();

                    txtPurcRate.Text = dt.Rows[0]["Purc_Rate"].ToString();
                    drpResale.SelectedValue = dt.Rows[0]["type"].ToString();
                    lblVoucherNo.Text = vouchernumber.Value;
                    lblVoucherType.Text = dt.Rows[0]["Voucher_Type"].ToString();
                    hdnfcommisionid.Value = dt.Rows[0]["commissionid"].ToString();
                    txtBrokrage.Text = dt.Rows[0]["Brokrage"].ToString();

                    lblMsg.Text = "";

                    #region Tender Details

                    // qry = "select  ID,Buyer as Party,buyerbrokerfullname  as [Name of Party],Buyer_Party as Broker,salepartyfullname as [Name of Broker],Buyer_Quantal as [Quantal],Sale_Rate as [Sale Rate],Commission_Rate as [Commission],Convert(varchar(10),DetailSaudaDate,103) as Sauda_Date,Convert(varchar(10),DetailLiftingDate,103) as Lifting_Date,saudanarration as [Sauda Narration],Delivery_Type from " + qryDetail + " where tenderid='" + hdnf.Value + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and IsActive='True' and TDYearCode='" + Convert.ToInt32(Session["year"].ToString()) + "' order by ID";
                    qry = "select  ID,Buyer as Party,buyername as Name_of_Party,Buyer_Party as Broker," +
" buyerpartyname as Name_of_Broker,isnull(ShipTo,0) as ShipTo ,isnull(ShipToname,0) as ShipToname, BALANCE as Quantal,Sale_Rate as Sale_Rate,isnull(CashDifference,0) as CashDifference," +
" Commission_Rate as Commission,Sauda_DateConverted as Sauda_Date,payment_dateConverted as Lifting_Date,detail_narration as Sauda_Narration," +
" Delivery_Type,sub_broker as sub_broker,subbrokername as subBrokername ,tenderdetailid,'' as rowAction,'' as SrNo,buyerid as partyid,buyerpartyid as Dbc,sbr as Sbc,gst_rate as GSTRate,gst_amt as GSTAmt,tcs_rate as TCSRate,tcs_amt as TCSAmount,Shiptoid,loding_by_us  from qrytenderbalance where tenderid='" + hdnf.Value + "' and BALANCE!=0 and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'   order by ID";
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["Delivery_Type"].ToString() == "C")
                                    {
                                        dt.Rows[i]["Delivery_Type"] = "Commission";
                                    }
                                    if (dt.Rows[i]["Delivery_Type"].ToString() == "N")
                                    {
                                        dt.Rows[i]["Delivery_Type"] = "With GST Naka Delivery";
                                    }
                                    if (dt.Rows[i]["Delivery_Type"].ToString() == "D")
                                    {
                                        dt.Rows[i]["Delivery_Type"] = "DO";
                                    }
                                    if (dt.Rows[i]["Delivery_Type"].ToString() == "A")
                                    {
                                        dt.Rows[i]["Delivery_Type"] = "Naka Delivery without GST Rate";
                                    }
                                    //  double qty = Convert.ToDouble(dt.Rows[i]["Quantal"].ToString());

                                    double qty = Convert.ToDouble(dt.Compute("SUM(Quantal)", string.Empty));
                                    double totaldes = 0.00;
                                    totaldes = totaldes + qty;
                                    dt.Rows[i]["SrNo"] = 1;                           //self row
                                    dt.Rows[i]["rowAction"] = "A";

                                    txtQuantal.Text = totaldes.ToString();
                                }

                                grdDetail.DataSource = dt;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = dt;

                            }
                        }
                    }
                    #endregion
                }
            }

        }


    }



    protected void txtitem_code_TextChanged(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "Close";
            string itemname = string.Empty;
            searchString = txtMillCode.Text;
            if (txtitem_code.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtitem_code.Text);
                if (a == false)
                {
                    btntxtitem_code_Click(this, new EventArgs());
                }
                else
                {
                    DataSet ds = clsDAL.SimpleQuery("select System_Name_E,Gst_Code,gstratre,systemid,HSN,KgPerKatta from qrymstitem where System_Type='I' and System_Code=" + txtitem_code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                    string pck = "0";
                    // pck = clsCommon.getString("select KgPerKatta from qrymstitem where System_Type='I' and System_Code=" + txtitem_code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            itemname = ds.Tables[0].Rows[0]["System_Name_E"].ToString();
                            string itemgst = ds.Tables[0].Rows[0]["Gst_Code"].ToString();

                            string rr = ds.Tables[0].Rows[0]["gstratre"].ToString();
                            if (itemname != string.Empty && itemname != "0")
                            {
                                hdnfic.Value = ds.Tables[0].Rows[0]["systemid"].ToString();
                                lblitemname.Text = itemname;
                                txtPacking.Text = ds.Tables[0].Rows[0]["KgPerKatta"].ToString();
                                hdnfhsnnumber.Value = ds.Tables[0].Rows[0]["HSN"].ToString();
                                if (txtQuantal.Text != string.Empty)
                                {
                                    int bags = Convert.ToInt32(Math.Round(float.Parse(txtQuantal.Text) * (100 / float.Parse(txtPacking.Text))));
                                    txtBags.Text = bags.ToString();
                                }
                                if (itemgst == string.Empty)
                                {
                                    txtGstrateCode.Text = "1";
                                    lblgstrateCode.Text = clsCommon.getString("Select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=1");

                                }
                                else
                                {
                                    txtGstrateCode.Text = itemgst;
                                    lblgstrateCode.Text = rr;
                                }

                                this.calculateAmount();
                                setFocusControl(txtGrade);
                            }
                            else
                            {
                                txtitem_code.Text = string.Empty;
                                lblitemname.Text = string.Empty;
                                setFocusControl(txtitem_code);
                            }
                        }
                        else
                        {
                            txtitem_code.Text = string.Empty;
                            lblitemname.Text = string.Empty;
                            setFocusControl(txtitem_code);
                        }
                    }
                    else
                    {
                        txtitem_code.Text = string.Empty;
                        lblitemname.Text = string.Empty;
                        setFocusControl(txtitem_code);
                    }
                }
            }
            else
            {
                txtitem_code.Text = string.Empty;
                lblitemname.Text = string.Empty;
                setFocusControl(txtitem_code);
            }
        }
        catch
        {
        }
    }
    protected void btntxtitem_code_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtitem_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region LV POsting
    private void LV_Posting()
    {
        try
        {
            LV = new LocalVoucher();
            #region max
            int counts = 0;
            if (btnSave.Text == "Save")
            {
                counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from commission_bill where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'"));
                if (counts == 0)
                {
                    LV.LV_Doc_No = 1;
                    voucher_no = 1;

                }
                else
                {
                    LV.LV_Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from commission_bill where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                           " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                    voucher_no = LV.LV_Doc_No;
                }
                counts = Convert.ToInt32(clsCommon.getString("SELECT count(commissionid) as commissionid from commission_bill "));
                if (counts == 0)
                {
                    LV.LV_commissionid = 1;

                }
                else
                {
                    LV.LV_commissionid = Convert.ToInt32(clsCommon.getString("SELECT max(commissionid) as commissionid from commission_bill")) + 1;

                }
            }
            else
            {
                LV.LV_Doc_No = Convert.ToInt32(lblVoucherNo.Text);
                voucher_no = LV.LV_Doc_No;
                LV.LV_commissionid = Convert.ToInt32(clsCommon.getString("SELECT commissionid  from qrycommissionbill where doc_no=" + lblVoucherNo.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " " +
                    " and Year_Code=" + Session["year"].ToString() + ""));
            }
            #endregion

            string PartyStateCode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + Voucher_By + " and Company_Code='" + Session["Company_Code"].ToString() + "'");
            string CompanyStateCode = clsCommon.getString("select GSTStateCode from NT_1_CompanyParameters where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["year"].ToString() + "'");

            double CGSTRATE = Convert.ToDouble(clsCommon.getString("select CGST from NT_1_GSTRateMaster where Doc_no=" + GstRate_Code + " and Company_Code=" + Session["Company_Code"].ToString() + " "));
            double SGSTRATE = Convert.ToDouble(clsCommon.getString("select SGST from NT_1_GSTRateMaster where Doc_no=" + GstRate_Code + " and Company_Code=" + Session["Company_Code"].ToString() + " "));
            double IGSTRATE = Convert.ToDouble(clsCommon.getString("select IGST from NT_1_GSTRateMaster where Doc_no=" + GstRate_Code + " and Company_Code=" + Session["Company_Code"].ToString() + " "));
            double GSTRATE = Convert.ToDouble(clsCommon.getString("select Rate from NT_1_GSTRateMaster where Doc_no=" + GstRate_Code + " and Company_Code=" + Session["Company_Code"].ToString() + " "));


            double taxmillamt = Quantal * Diff_Amount;

            double CGSTAmount = 0.00;
            double SGSTAmount = 0.00;
            double IGSTAmount = 0.00;

            if (CompanyStateCode == PartyStateCode)
            {
                CGSTAmount = Math.Round((taxmillamt * CGSTRATE / 100), 2);
                SGSTAmount = Math.Round((taxmillamt * SGSTRATE / 100), 2);
                IGSTAmount = 0.00;
                IGSTRATE = 0.00;
            }
            else
            {
                CGSTAmount = 0.000;
                SGSTAmount = 0.00;
                CGSTRATE = 0.00;
                SGSTRATE = 0.00;
                IGSTAmount = Math.Round((taxmillamt * IGSTRATE / 100), 2);

            }

            double Voucher_Amt = taxmillamt + CGSTAmount + SGSTAmount + IGSTAmount;

            LV.LV_ac = vb;
            LV.LV_bc = bk;
            LV.LV_mc = mc;
            LV.LV_Doc_No = voucher_no;
            LV.LV_Doc_Date = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            LV.LV_Link_No = 0;
            LV.LV_link_Type = "";
            LV.LV_Link_id = 0;
            LV.LV_Ac_Code = Convert.ToInt32(Voucher_By);
            LV.LV_Unit_Code = 0;
            LV.LV_Broker_CODE = Convert.ToInt32(txtBroker.Text != string.Empty ? txtBroker.Text : "0");
            LV.LV_Quantal = Quantal;
            LV.LV_PACKING = Packing;
            LV.LV_BAGS = Bags;
            LV.LV_Grade = Grade;
            LV.LV_Transport_Code = 0;
            LV.LV_Mill_Rate = Convert.ToDouble(Mill_Rate);
            LV.LV_Sale_Rate = 0.00;
            LV.LV_Purchase_Rate = Convert.ToDouble(txtPurcRate.Text != string.Empty ? txtPurcRate.Text : "0.00"); ;
            LV.LV_FREIGHT = 0.00;
            LV.LV_Narration1 = myNarration;
            LV.LV_Narration2 = string.Empty;
            LV.LV_Narration3 = string.Empty;
            LV.LV_Narration4 = string.Empty;
            LV.LV_Voucher_Amount = Diff_Amount;
            LV.LV_Diff_Amount = Diff_Amount;
            LV.LV_Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            LV.LV_Year_Code = Convert.ToInt32(Session["year"].ToString());
            LV.LV_Branch_Code = 0;
            LV.LV_Created_By = "" + Session["user"].ToString() + "";
            LV.LV_Commission_Rate = 0.00;
            LV.LV_Resale_Commisson = 0.00;
            LV.LV_GstRateCode = GstRate_Code;
            LV.LV_CGSTRate = CGSTRATE;
            LV.LV_CGSTAmount = CGSTAmount;
            LV.LV_SGSTRate = SGSTRATE;
            LV.LV_SGSTAmount = SGSTAmount;
            LV.LV_IGSTRate = IGSTRATE;
            LV.LV_IGSTAmount = IGSTAmount;
            LV.LV_TaxableAmount = Diff_Amount;

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
                        hdnfbp.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtBP_Account.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (BP_Account.Length > 15)
                        {
                            BP_Account.Substring(0, 15);
                        }
                        else if (BP_Account.Length > 10)
                        {
                            BP_Account.Substring(0, 10);
                        }
                        lblBP_Account.Text = BP_Account;
                        //AmtCalculation();
                        //if (drpResale.SelectedValue == "M")
                        //{
                        setFocusControl(txtTenderFrom);

                    }
                    else
                    {
                        txtBP_Account.Text = string.Empty;
                        lblBP_Account.Text = string.Empty;
                        setFocusControl(txtCashDiff);
                        // AmtCalculation();
                    }
                }
            }
            else
            {
                txtBP_Account.Text = string.Empty;
                lblBP_Account.Text = BP_Account;
                setFocusControl(txtPaymentTo);
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

            hdnfClosePopup.Value = "BPAccount";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }

    }
    #endregion

    #region[txtBP_Account_Detail_TextChanged]
    protected void txtBP_Account_Detail_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtBP_Account_Detail.Text;
            string BP_Account_Detail = string.Empty;
            if (txtBP_Account_Detail.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtBP_Account_Detail.Text);
                if (a == false)
                {
                    btnBP_Account_Click(this, new EventArgs());
                }
                else
                {
                    BP_Account_Detail = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code='" + txtBP_Account_Detail.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    if (BP_Account_Detail != string.Empty && BP_Account_Detail != "0")
                    {
                        hdnfbpdetail.Value = clsCommon.getString("select isnull(accoid,0) as acid from nt_1_accountmaster where Ac_Code='" + txtBP_Account_Detail.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        if (BP_Account_Detail.Length > 15)
                        {
                            BP_Account_Detail.Substring(0, 15);
                        }
                        else if (BP_Account_Detail.Length > 10)
                        {
                            BP_Account_Detail.Substring(0, 10);
                        }
                        lblBP_Account_Detail.Text = BP_Account_Detail;
                        //AmtCalculation();
                        if (drpResale.SelectedValue == "M")
                        {
                            setFocusControl(txtBuyerCommission);
                        }
                        else
                        {
                            setFocusControl(txtBuyerCommission);
                        }
                    }
                    else
                    {
                        txtBP_Account_Detail.Text = string.Empty;
                        lblBP_Account_Detail.Text = string.Empty;
                        setFocusControl(txtBuyerParty);
                        // AmtCalculation();
                    }
                }
            }
            else
            {
                txtBP_Account_Detail.Text = string.Empty;
                lblBP_Account_Detail.Text = BP_Account_Detail;
                setFocusControl(txtPaymentTo);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region[btnBP_Account_Detail_Click]
    protected void btnBP_Account_Detail_Click(object sender, EventArgs e)
    {
        try
        {

            hdnfClosePopup.Value = "BPDetail";
            pnlPopup.Style["display"] = "block";
            btnSearch_Click(sender, e);
        }
        catch
        {

        }

    }
    #endregion

    protected void txtCashDiff_TextChanged(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string XMLInsert(string XML, string status)
    {

        string myString = "'1' is an unexpected token. The expected token is  or. Line 1, position 170.";
        string lastWord = myString.Split(' ').Last();
        string msgReturn = "";
        try
        {
            msgReturn = XML.Length.ToString();
            //   XML.Replace("''", """");
            //msgReturn = GetWordAtPosition(XML, 4313);
            XDocument XDoc = XDocument.Parse(XML, LoadOptions.None);
            XML = XDoc.ToString(SaveOptions.DisableFormatting);
            return msgReturn;
        }
        catch
        {
            return "";
        }
    }

    [WebMethod]
    public static string NewInsert(string TenderInsertUpdate, string TenderDetail_Insert, string TenderDetail_Update, string TenderDetail_Delete, string status)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "HeadDetail_CrudOpration";
            cmd.CommandType = CommandType.StoredProcedure;
            //an out parameter


            //an in parameter
            cmd.Parameters.AddWithValue("QryInsertAndUpdate", TenderInsertUpdate);
            cmd.Parameters["QryInsertAndUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Insert", TenderDetail_Insert);
            cmd.Parameters["QryDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Update", TenderDetail_Update);
            cmd.Parameters["QryDetail_Update"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Delete", TenderDetail_Delete);
            cmd.Parameters["QryDetail_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryGledger_Delete", "");
            cmd.Parameters["QryGledger_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryGledger_Insert", "");
            cmd.Parameters["QryGledger_Insert"].Direction = ParameterDirection.Input;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            _adapter.Fill(ds);
            string msgReturn = "";
            if (status == "Save")
            {
                msgReturn = "Record Successfully Added";
            }
            else if (status == "Update")
            {
                msgReturn = "Record Successfully Update";
            }
            return msgReturn;

        }
        catch
        {
            con.Close();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    #region [txtTCSRate_TextChanged]
    protected void txtTCSRate_TextChanged(object sender, EventArgs e)
    {
        this.TCSCalculation();
        this.TdsCalculation();
        // setFocusControl(txtBuyer);

    }
    #endregion
    #region [txtTCSAmt_TextChanged]
    protected void txtTCSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSAmt.Text;

    }
    #endregion
    #region [txtTDSAmt_TextChanged]
    protected void txtTdsamount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTdsamount.Text;

    }
    #endregion
    #region [txtTDSRate_TextChanged]
    protected void txtTdsrate_TextChanged(object sender, EventArgs e)
    {
        this.TdsCalculation();
        setFocusControl(txtBuyer);

    }
    #endregion

    protected void TdsCalculation()
    {
        try
        {
            double TDSRate = 0.00;
            double Quantal = 0.00;
            double GSTRate = 0.00;
            double TdSAmt = 0.00;
            double psamt = 0.00;
            if (txtTdsrate.Text != string.Empty)
            {
                TDSRate = txtTdsrate.Text != string.Empty ? Convert.ToDouble(txtTdsrate.Text) : 0.00;
            }
            if (txtQuantal.Text != string.Empty)
            {
                Quantal = Convert.ToDouble(txtQuantal.Text);
            }
            if (txtQuantal.Text != string.Empty)
            {
                GSTRate = lblMillRateGst.Text != string.Empty ? Convert.ToDouble(lblMillRateGst.Text) : 0;
            }
            if (drpResale.SelectedValue == "P")
            {
                if (txtpartybillrate.Text != string.Empty)
                {
                    psamt = txtpartybillrate.Text != string.Empty ? Convert.ToDouble(txtpartybillrate.Text) : 0;

                }
            }
            else
            {
                if (txtMillRate.Text != string.Empty)
                {
                    psamt = txtMillRate.Text != string.Empty ? Convert.ToDouble(txtMillRate.Text) : 0;

                }
            }

            TdSAmt = Math.Round(((Quantal * psamt) * TDSRate) / 100, 2);
            txtTdsamount.Text = TdSAmt.ToString();
        }
        catch (Exception)
        {
            throw;
        }

    }
    protected void TCSCalculation()
    {
        try
        {
            double TCSRate = 0.00;
            double Quantal = 0.00;
            double GSTRate = 0.00;
            double TCSAmt = 0.00;
            if (txtTCSRate.Text != string.Empty)
            {
                TCSRate = txtTCSRate.Text != string.Empty ? Convert.ToDouble(txtTCSRate.Text) : 0.00;
            }
            if (txtQuantal.Text != string.Empty)
            {
                Quantal = Convert.ToDouble(txtQuantal.Text);
            }
            if (txtQuantal.Text != string.Empty)
            {
                GSTRate = lblMillRateGst.Text != string.Empty ? Convert.ToDouble(lblMillRateGst.Text) : 0;
            }


            TCSAmt = Math.Round(((Quantal * GSTRate) * TCSRate) / 100, 2);
            txtTCSAmt.Text = TCSAmt.ToString();

            txtTCSrate1.Text = TCSRate.ToString();
            txtTCSamount1.Text = TCSAmt.ToString();
            txtGSTrate.Text = lblgstrateCode.Text;
            txtgstamt.Text = txtExciseRate.Text;
            double value = 0.00;
            value = lblValue.Text != string.Empty ? Convert.ToDouble(lblValue.Text) : 0;
            double tcsamtwithvalue = 0.00;
            tcsamtwithvalue = TCSAmt + value;
            lblvaluewithtcsamt.Text = tcsamtwithvalue.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }
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
    }
    protected void btntxtShipTo_Click(object sender, EventArgs e)
    {
        pnlPopup.Style["display"] = "block";
        hdnfClosePopup.Value = "txtShipTo";
        btnSearch_Click(sender, e);
    }
    protected void txtcashdiff_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtcashdifference_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtBuyerCommission);
        TCSAmountCalculate();
    }
    protected void txtpartybillrate_TextChanged(object sender, EventArgs e)
    {
        //this.TdsCalculation();
        this.calculateAmount();
        this.TCSCalculation();
        this.TdsCalculation();
    }
    protected void txtvtcsrate_TextChanged(object sender, EventArgs e)
    {
        AmtCalculationforvoucher();
    }
    protected void txtvtdsrate_TextChanged(object sender, EventArgs e)
    {
        AmtCalculationforvoucher();
    }
    protected void drpautopurchase_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(txtDate);
    }
}