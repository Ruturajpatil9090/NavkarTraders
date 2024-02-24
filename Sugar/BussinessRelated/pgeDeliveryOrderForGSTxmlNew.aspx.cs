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


public partial class Sugar_pgeDeliveryOrderForGSTxmlNew : System.Web.UI.Page
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
    string doauth = "";
    static string donumber = string.Empty;
    static string Edit_Sale_Rate = string.Empty;
    
    #region -Head part declearation
    string Limit = string.Empty;
    //Int32 DOC_NO = 0;
    string DOC_DATE = string.Empty;
    string PUR_DATE = string.Empty;
    string DESP_TYPE = string.Empty;
    string Delivery_Type = string.Empty;
    string MillInv_Date = string.Empty;
    string Inv_Chk = string.Empty;
    string MILL_CODE = string.Empty;
    string gst_code = string.Empty;
    string GETPASS_CODE = string.Empty;
    string VOUCHER_BY = string.Empty;
    double FRIEGHT_RATE = 0;
    double FRIEGHT_AMOUNT = 0.00;
    double VASULI_AMOUNT = 0.00;
    double VASULI_RATE = 0.00;
    double MEMO_ADVANCE = 0.00;
    string Ac_Code = string.Empty;
    string GRADE = string.Empty;
    double QUANTAL = 0.00;
    Int32 PACKING = 0;
    Int32 BAGS = 0;
    double mill_rate = 0.00;
    double EXCISE_RATE = 0.00;
    double Tender_Commission = 0.00;
    double SALE_RATE = 0.00;
    double MILL_AMOUNT = 0.00;

    double DIFF_RATE = 0.00;
    double DIFF_AMOUNT = 0.00;
    double VASULI_RATE_1 = 0.00;
    double VASULI_AMOUNT_1 = 0.00;
    string EWayBill_No = string.Empty;
    double Distance = 0.00;
    string SaleBillTo = string.Empty;

    string MM_CC = string.Empty;

    double MM_Rate = 0.00;
    string PAN_NO = string.Empty;
    Int32 DO_CODE = 0;
    Int32 BROKER_CODE = 0;
    string TRUCK_NO = string.Empty;
    Int32 TRANSPORT_CODE = 0;
    Int32 VASULI_AC = 0;


    double Tender_Commission_Amount = 0.00;
    Int32 OVTransportCode = 0;

    string NARRATION1 = string.Empty;
    string NARRATION2 = string.Empty;
    string NARRATION3 = string.Empty;
    string NARRATION4 = string.Empty;
    string NARRATION5 = string.Empty;
    string SBNARRATION = string.Empty;
    string INVOICE_NO = string.Empty;
    string CheckPost = string.Empty;
    int purc_no = 0;
    int purc_order = 0;

    #region other voucher amount
    double VoucherBrokrage = 0.00;
    double VoucherServiceCharge = 0.00;
    double VoucherRateDiffRate = 0.00;
    double VoucherRateDiffAmt = 0.00;
    double VoucherBankCommRate = 0.00;
    double VoucherBankCommAmt = 0.00;
    double VoucherInterest = 0.00;
    double VoucherTransport = 0.00;
    double VoucherOtherExpenses = 0.00;

    string EWay_BillChk = string.Empty;
    string MillInvoiceno = string.Empty;

    #endregion

    double FINAL_AMOUNT = 0.00;
    string userinfo = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    int Company_Code = 0;
    int Year_Code = 0;
    //int year_Code = 0;
    int Branch_Code = 0;
    float DIFF = 0;
    double LESS_DIFF = 0.00;
    double LESSDIFFOV = 0.00;
    string Driver_Mobile = string.Empty;

    Int32 Carporate_Sale_No = 0;
    string WhoseFrieght = string.Empty;
    int UTR_Year_Code = 0;
    int Carporate_Sale_Year_Code = 0;
    Int32 voucher_no = 0;
    string PDS = string.Empty;

    int memo_no = 0;

    string voucher_type = string.Empty;
    Int32 voucherlbl = 0;

    string myNarration = string.Empty;
    string myNarration2 = string.Empty;
    string myNarration3 = string.Empty;
    string myNarration4 = string.Empty;
    string vouchnarration = string.Empty;


    string utrno = string.Empty;
    string Utr_No = string.Empty;
    string nar = string.Empty;

    int VOUCHERAMOUNT = 0;
    //double MILL_AMOUNT =string.Empty;
    string city_code = string.Empty;
    string From_Place = string.Empty;
    string city_code2 = string.Empty;
    string To_Place = string.Empty;
    int SELFAC = 0;
    double BILL_AMOUNT = 0.00;
    double SUBTOTAL = 0.00;
    double TotalGstPurchaseAmount = 0.00;
    double TotalGstSaleAmount = 0.00;

    int GSTRateCode = 0;
    int GetpassGstStateCode = 0;
    int VoucherbyGstStateCode = 0;
    int SalebilltoGstStateCode = 0;
    int MillGstStateCode = 0;
    int TransportGstStateCode = 0;
    double GSTMillRateAmount = 0.00;
    double GSTSaleRateAmount = 0.00;
    double GSTExclSaleRateAmount = 0.00;
    double GSTExclMillRateAmount = 0.00;
    double GSTRate = 0.00;
    double cgstrate = 0.00;
    double sgstrate = 0.00;
    double igstrate = 0.00;
    double LessFriegthRateForSB = 0.00;
    double LessFriegthAmountForSB = 0.00;
    Int32 pdsparty = 0;
    Int32 pdsunit = 0;
    int paymentto = 0;
    int gstSateCodeForPurchaseBill = 0;
    int paymenttogststatecode = 0;
    // int CompanyGSTStateCode = 0;
    double millamount = 0.00;
    double cgsttaxAmountOnMR = 0.00;
    double sgsttaxAmountOnMR = 0.00;
    int pdspartystatecode = 0;
    double saleamount = 0.00;
    double sgsttaxAmountOnSR = 0.00;
    double igsttaxAmountOnSR = 0.00;
    double cgsttaxAmountOnSR = 0.00;
    int GSTRateCodeForRateDiff = 0;
    double GSTRateForLV = 0.00;
    double cgstrateForLV1 = 0.00;
    double sgstrateForLV1 = 0.00;
    double igstrateForLV1 = 0.00;

    double CGSTAmountForLV = 0.00;
    double SGSTAmountForLV = 0.00;
    double IGSTAmountForLV = 0.00;

    double CGSTRateForLV = 0.00;
    double SGSTRateForLV = 0.00;
    double IGSTRateForLV = 0.00;
    int CompanyGSTStateCode = 0;
    double voucherAmountForLV = 0.00;
    int SaleCGSTAc = 0;
    int SaleSGSTAc = 0;
    int SaleIGSTAc = 0;
    int PayableCGSTAc = 0;
    int PayableSGSTAc = 0;
    int PayableIGSTAc = 0;
    double CGSTAmountForPS = 0.0;
    double SGSTAmountForPS = 0.0;
    double IGSTAmountForPS = 0.0;
    double CGSTRateForPS = 0.00;
    double SGSTRateForPS = 0.00;
    double IGSTRateForPS = 0.00;

    double CGSTAmountForSB = 0.0;
    double SGSTAmountForSB = 0.0;
    double IGSTAmountForSB = 0.0;

    double CGSTRateForSB = 0.00;
    double SGSTRateForSB = 0.00;
    double IGSTRateForSB = 0.00;

    double SaleRateForSB = 0.00;
    double TaxableAmountForSB = 0.00;
    double SaleRateForNaka = 0.00;
    double gstOnSalerateAndAdvance = 0.00;

    double Diff_Rate = 0.00;
    double VOUCHER_AMOUNT = 0.00;
    string Rate_Type = string.Empty;
    Int32 SaleBillTransport = 0;
    Int32 DONumber = 0;
    string cureentbalance = string.Empty;
    string limitbalnce = string.Empty;
    double CurrBal = 0.00;
    double limitBal = 0.00;
    double GSTAmt = 0.00;
    double SaleAmt = 0.00;
    double Commission = 0.00;
    double BillAmt = 0.00;
    double Qty = 0.00;
    double NetLimit = 0.00;
    double OldBillAmtNew = 0.00;
    int Bill_To = 0;
    int bt = 0;
    int? mc = 0;
    int? gp = 0;
    int? st = 0;
    int? sb = 0;
    int? tc = 0;
    int? itemcode = 0;
    int? cscode = 0;
    int? ic = 0;
    int? tenderdetailid = 0;
    int? bk = 0;
    int? docd = 0;
    int? va = 0;

    string MillEwayBill = string.Empty;


    #endregion
    #region Detail Declare
    Int32 detail_Id = 0;
    string ddType = "";
    Int32 Bank_Code = 0;
    string Narration = "";
    double Amount = 0.0;
    int Utr_no = 0;
    int UTRDetail_ID = 0;
    int LT_no = 0;
    Int32 GID = 0;
    int bc = 0;
    int utrdetailid = 0;
    #endregion

    #region PURCHASE And SALE Posting VAriables
    PurchaseFields purchase = null;
    SaleFields salePosting = null;
    LocalVoucher LV = null;

    int millCityCode = 0;
    string fromPlace = string.Empty;
    int getPassCityCode = 0;
    string toPlace = string.Empty;
    int CompanyStateCode = 0;
    double MILLAMOUNT = 0.00;
    double CGSTtaxAmountOnMR = 0.00;
    double SGSTtaxAmountOnMR = 0.00;
    double IGSTtaxAmountOnMR = 0.00;

    double SGSTtaxAmountOnSR = 0.00;
    double CGSTtaxAmountOnSR = 0.00;
    double IGSTtaxAmountOnSR = 0.00;
    double CGST_AMOUNT = 0.00;
    double SGST_AMOUNT = 0.00;
    double IGST_AMOUNT = 0.00;
    double TOTALPurchase_Amount = 0.00;
    double ITEM_AMOUNT = 0.00;
    int VOUCHER_NO = 0;
    int maxcountpsno = 0;
    double CGSTRATE = 0.00;
    double SGSTRATE = 0.00;
    double IGSTRATE = 0.00;
    double GSTRATE = 0.00;
    int PaymentTo = 0;

    double TaxableAmountForSaleBill = 0.00;
    double LessFriegthRateForSaleBill = 0.00;
    double LessFriegthAmountForSaleBill = 0.00;
    int saleparty = 0;
    int pdsunitSaleBill = 0;
    double SaleBillSaleRate = 0.00;

    #endregion

    string oldSalebillto = "";
    string oldsalerate = "";
    string oldQntl = "";
    string oldFrieght = "";
    string oldMemoAdv = "";


    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            user = Session["user"].ToString();
            doauth = Session["DOPages"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "deliveryorder";
            tblDetails = tblPrefix + "DODetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            SystemMastertable = tblPrefix + "SystemMaster";
            qryCommon = tblPrefix + "qryDeliveryOrderList";
            qryHead = "qrydohead";
            qryDetail = "qrydodetail";
            pnlPopup.Style["display"] = "none";
            GLedgerTable = tblPrefix + "GLEDGER";
            qrycarporateSalebalance = tblPrefix + "qryCarporatesellbalance";
            qryUTRBalance = tblPrefix + "qryUTRBalance";
            qryAccountList = "qrymstaccountmaster";
            qrypurc_No = "qrysugarBalancestock";
            Edit_Sale_Rate = Session["Edit_Sale_Rate"].ToString();
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            Maindt = new DataTable();
            dr = null;

            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            DS = new DataSet();
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                //txtSearchText.Attributes.Add("onkeypress", "abc(event);");
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    //hdnvouchernumber.Value = "0";
                    if (doauth == "1")
                    {
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
                            this.enableDisableNavigateButtons();
                            setFocusControl(btnEdit);
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
                        txtFromDate.Text = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        txtToDate.Text = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
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
                obj.tableName = tblHead + " where  tran_type='" + trnType + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                obj.code = "doc_no";

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
                    if (c is System.Web.UI.WebControls.HiddenField)
                    {
                        ((System.Web.UI.WebControls.HiddenField)c).Value = "";
                    }
                }
                foreach (System.Web.UI.Control c in pnlVoucherEntries.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtGetpassGstStateCode.Enabled = false;
                btntxtVoucherbyGstStateCode.Enabled = false;
                btntxtSalebilltoGstStateCode.Enabled = false;
                btntxtMillGstStateCode.Enabled = false;
                btntxtTransportGstStateCode.Enabled = false;
                btntxtTDSAc.Enabled = false;
                btntxtCashDiffAc.Enabled = false;
                btntxtMemoGSTRate.Enabled = false;
                btntxtGstRate.Enabled = false;
                txtEditDoc_No.Enabled = true;
                txtcarporateSale.Enabled = false;
                txtSaleBillTo.Enabled = false;
                txtNARRATION4.Enabled = false;
                txtMillMobile.Enabled = true;
                txtMillMobile.Text.Trim();
                pnlVoucherEntries.Style["display"] = "none";
                pnlPopup.Style["display"] = "none";
                btnOpenDetailsPopup.Enabled = false;
                btnEditDetails.Enabled = false;

                btnSave.Text = "Save";
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                ddlFrieghtType.Enabled = false;
                lblMsg.Text = string.Empty;
                //txtPartyCommission.Enabled = false;
                drpCC.Enabled = false;
                btnVoucherOtherAmounts.Enabled = false;
                txtitem_Code.Enabled = false;
                btntxtitem_Code.Enabled = false;
                btngenratesalebill.Enabled = false;

                txtBill_To.Enabled = false;
                btntxtbill_To.Enabled = false;
                txtMillInv_Date.Enabled = false;
                chkInv_Chk.Enabled = false;

                #region Logic
                CalendarExtender2.Enabled = false;
                CalendarExtender4.Enabled = false;
                calenderExtenderDate.Enabled = false;
                CalendarExtender1.Enabled = false;
                drpDOType.Enabled = false;
                drpDeliveryType.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtGETPASS_CODE.Enabled = false;
                btntxtvoucher_by.Enabled = false;
                btntxtVasuliAc.Enabled = false;
                btntxtGRADE.Enabled = false;
                btntxtDO_CODE.Enabled = false;
                btntxtBroker_CODE.Enabled = false;
                btntxtTRANSPORT_CODE.Enabled = false;
                btntxtNARRATION1.Enabled = false;
                btntxtNARRATION2.Enabled = false;
                btntxtNARRATION3.Enabled = false;
                btntxtNARRATION4.Enabled = false;
                btntxtPurcNo.Enabled = false;
                drpDeliveryType.Enabled = false;
                lblUTRYearCode.Text = string.Empty;
                lblUTRCompnyCode.Text = string.Empty;
                lblCSYearCode.Text = string.Empty;
                btntxtUTRNo.Enabled = false;
                btntxtcarporateSale.Enabled = false;
                //btnTransLetter.Enabled = true;
                btnWayBill.Enabled = true;
                btnMail.Enabled = true;
                btnOurDO.Enabled = true;
                btnPartyBillDO.Enabled = true;
                btnPrintSaleBill.Enabled = true;
                //btnPrintCarpVoucher.Enabled = true;
                btnPrintMotorMemo.Enabled = true;
                //btnPrintITCVoc.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                btnSendSms.Enabled = true;
                lblVoucherLedgerByBalance.Text = string.Empty;
                lblSaleBillToLedgerByBalance.Text = string.Empty;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                drpInsured.Enabled = false;
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
                foreach (System.Web.UI.Control c in pnlVoucherEntries.Controls)
                {
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                        ((System.Web.UI.WebControls.Label)c).Enabled = true;
                    }
                    if (c is System.Web.UI.WebControls.HiddenField)
                    {
                        ((System.Web.UI.WebControls.HiddenField)c).Value = "";
                    }
                }


                hdnfmc.Value = "0";
                hdnfgp.Value = "0";
                hdnfst.Value = "0";
                hdnfsb.Value = "0";
                hdnftc.Value = "0";
                hdnfbk.Value = "0";
                hdnfva.Value = "0";
                hdnfbt.Value = "0";
                hdnfdocd.Value = "0";
                hdnfcscode.Value = "0";
                hdnfic.Value = "0";
                hdnfsaledetailid.Value = "0";
                hdnfpurcdetailid.Value = "0";
                hdnfTDSAcid.Value = "0";
                hdnfCashDiffAcid.Value = "0";
                hdnfDoid.Value = "0";
                hdnfpaymentid.Value = "0";
                hdnfpdsacID.Value = "0";
                hdnfpdsunitID.Value = "0";
                hdnfSaleTDS.Value = "0";
                hdnfPurchaseTDS.Value = "0";
                hdnfminRate.Value = "0";
                hdnfmaxRate.Value = "0";
                txtGETPASS_CODE.Enabled = false;
                btntxtGetpassGstStateCode.Enabled = false;
                txtGetpassGstStateCode.Enabled = false;
                txtVoucherbyGstStateCode.Enabled = false;
                txtSalebilltoGstStateCode.Enabled = false;
                txtTransportGstStateCode.Enabled = false;
                txtMillGstStateCode.Enabled = false;
                btntxtVoucherbyGstStateCode.Enabled = false;
                btntxtSalebilltoGstStateCode.Enabled = false;
                btntxtMillGstStateCode.Enabled = false;
                btntxtTransportGstStateCode.Enabled = false;
                lbltxtGetpassGstStateName.Text = "";
                lbltxtVoucherbyGstStateName.Text = "";
                lbltxtSalebilltoGstStateName.Text = "";
                lblBill_To.Text = "";
                lblCashDiffAcname.Text = "";
                lblMillAmtWOTCS.Text = "";
                lbltxtTransportGstStateCode.Text = "";
                lblMemoGSTRate.Text = "";
                btntxtGstRate.Enabled = true;
                btntxtMemoGSTRate.Enabled = true;
                lblVoucherLedgerByBalance.Text = string.Empty;
                lblSaleBillToLedgerByBalance.Text = string.Empty;
                txtEditDoc_No.Enabled = false;
                txtcarporateSale.Enabled = true;
                chkEWayBill.Checked = false;
                lblchkEWayBill.Text = string.Empty;
                //txtSaleBillTo.Enabled = false;
                btngenratesalebill.Enabled = false;


                txtNARRATION4.Enabled = false;
                txtMillMobile.Enabled = true;
                txtMillMobile.Text.Trim();
                drpCC.Enabled = true;
                btnSendSms.Enabled = false;
                btnSave.Text = "Save";
                btntxtDOC_NO.Text = "Change No";
                btntxtDOC_NO.Enabled = true;
                txtitem_Code.Enabled = true;
                btntxtitem_Code.Enabled = true;

                txtMillInv_Date.Enabled = true;
                chkInv_Chk.Enabled = true;
                //drpDeliveryType.Enabled = true;
                txtTCSRate.Text = Session["TCSRate"].ToString();
                hdnfTCSratepur.Value = txtTCSRate.Text;
                txtTCSRate_Sale.Text = Session["TCSRate"].ToString();
                txtSaleTDS.Text = Session["SaleTDSRate"].ToString();
                txtPurchaseTDS.Text = Session["PurchaseTDSRate"].ToString();
                hdnfSaleTDS.Value = txtSaleTDS.Text;
                hdnfPurchaseTDS.Value = txtPurchaseTDS.Text;
                drpInsured.Enabled = true;
                #region set Business logic for save
                CalendarExtender2.Enabled = true;
                CalendarExtender4.Enabled = true;

                calenderExtenderDate.Enabled = true;
                CalendarExtender1.Enabled = true;
                btnVoucherOtherAmounts.Enabled = true;
                drpDOType.Enabled = true;
                // btnTransLetter.Enabled = false;
                btnWayBill.Enabled = false;
                drpDeliveryType.Enabled = true;
                drpDeliveryType.Visible = true;
                btntxtMILL_CODE.Enabled = true;
                btntxtGETPASS_CODE.Enabled = true;
                btntxtvoucher_by.Enabled = true;
                btntxtVasuliAc.Enabled = true;
                btntxtGRADE.Enabled = true;
                btntxtTDSAc.Enabled = true;
                btntxtCashDiffAc.Enabled = true;
                btntxtDO_CODE.Enabled = true;
                btntxtBroker_CODE.Enabled = true;
                btntxtTRANSPORT_CODE.Enabled = true;
                btntxtNARRATION1.Enabled = true;
                btntxtNARRATION2.Enabled = true;
                btntxtNARRATION3.Enabled = true;
                btntxtNARRATION4.Enabled = true;
                btnOpenDetailsPopup.Enabled = true;
                btnEditDetails.Enabled = true;

                txtdoc_no.Enabled = false;
                btntxtPurcNo.Enabled = true;
                lblMillAmount.Text = string.Empty;
                lbltxtVasuliAc.Text = string.Empty;
                LBLMILL_NAME.Text = string.Empty;
                LBLGETPASS_NAME.Text = string.Empty;
                lblvoucherbyname.Text = string.Empty;
                LBLBROKER_NAME.Text = string.Empty;
                LBLDO_NAME.Text = string.Empty;
                LBLTRANSPORT_NAME.Text = string.Empty;
                lblDiffrate.Text = string.Empty;
                lblMemoNo.Text = "";
                lblVoucherNo.Text = "";
                lblVoucherType.Text = "";
                //lblFreight.Text = "";
                ddlFrieghtType.Enabled = true;
                ddlFrieghtType.SelectedIndex = 0;
                lblMsg.Text = "";
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                ViewState["currentTable"] = null;

                txtUTRNo.Text = string.Empty;
                txtUTRNo.Enabled = false;
                lblUTRYearCode.Text = string.Empty;
                lblUTRCompnyCode.Text = string.Empty;
                lblCSYearCode.Text = string.Empty;
                btntxtUTRNo.Enabled = true;
                btntxtcarporateSale.Enabled = true;
                btnMail.Enabled = false;
                btnPrintSaleBill.Enabled = false;
                btnOurDO.Enabled = false;
                btnPartyBillDO.Enabled = false;
                // btnPrintCarpVoucher.Enabled = false;
                btnPrintMotorMemo.Enabled = false;
                // btnPrintITCVoc.Enabled = false;
                txtDOC_DATE.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtPurchase_Date.Text = txtDOC_DATE.Text;
                txtBill_To.Enabled = false;
                btntxtbill_To.Enabled = false;
                txtMillInv_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEwayBill_ValidDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtdo_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                setFocusControl(txtDOC_DATE);

                grdGstAutoId.DataSource = null;
                grdGstAutoId.DataBind();
                ViewState["GstRateAutoId"] = null;

                txtnewsbno.Enabled = false;
                txtnewsbdate.Enabled = false;
                txteinvoiceno.Enabled = false;
                txtackno.Enabled = false;
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                lblPDSParty.Text = "";
                lblSB_No.Text = "0";
                txtGstRate.Text = "1";
                lblGstRateName.Text = clsCommon.getString("Select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1");
                hdnfOldBillAmt.Value = "0";
                txtitem_Code.Text = "1";
                lblitem_Name.Text = clsCommon.getString("select System_Name_E from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                hdnfic.Value = clsCommon.getString("select systemid from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                hdnfhsnnumber.Value = clsCommon.getString("select HSN from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                hdnfminRate.Value = clsCommon.getString("select minRate from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                hdnfmaxRate.Value = clsCommon.getString("select maxRate from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");

                //DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");

                DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + "");
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        Session["GSTRate"] = ds1.Tables[0].Rows[0]["Rate"].ToString();
                        Session["igstrate"] = ds1.Tables[0].Rows[0]["IGST"].ToString();
                        Session["sgstrate"] = ds1.Tables[0].Rows[0]["SGST"].ToString();
                        Session["cgstrate"] = ds1.Tables[0].Rows[0]["CGST"].ToString();
                    }
                }
                if (lblitem_Name.Text != string.Empty && lblitem_Name.Text != "0")
                {
                    setFocusControl(txtBrand_Code);
                }
                else
                {
                    txtitem_Code.Text = string.Empty;
                    setFocusControl(txtitem_Code);
                }
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
                foreach (System.Web.UI.Control c in pnlVoucherEntries.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtGetpassGstStateCode.Enabled = false;
                btntxtVoucherbyGstStateCode.Enabled = false;
                btntxtSalebilltoGstStateCode.Enabled = false;
                btntxtMillGstStateCode.Enabled = false;
                btntxtTransportGstStateCode.Enabled = false;
                btntxtGstRate.Enabled = false;
                btntxtMemoGSTRate.Enabled = false;
                btntxtVasuliAc.Enabled = false;
                txtEditDoc_No.Enabled = true;
                txtcarporateSale.Enabled = false;
                txtSaleBillTo.Enabled = false;
                txtNARRATION4.Enabled = false;
                txtMillMobile.Enabled = true;
                txtMillMobile.Text.Trim();
                drpCC.Enabled = false;
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;
                txtitem_Code.Enabled = false;
                btntxtitem_Code.Enabled = false;

                txtBill_To.Enabled = false;
                btntxtbill_To.Enabled = false;

                txtMillInv_Date.Enabled = false;
                chkInv_Chk.Enabled = false;
                #region Logic
                CalendarExtender2.Enabled = false;
                CalendarExtender4.Enabled = false;

                btnVoucherOtherAmounts.Enabled = false;
                calenderExtenderDate.Enabled = false;
                CalendarExtender1.Enabled = false;
                drpDOType.Enabled = false;
                drpDeliveryType.Enabled = false;
                btntxtMILL_CODE.Enabled = false;
                btntxtGETPASS_CODE.Enabled = false;
                btntxtTDSAc.Enabled = false;
                btntxtCashDiffAc.Enabled = false;
                btntxtvoucher_by.Enabled = false;
                btntxtGRADE.Enabled = false;
                btntxtDO_CODE.Enabled = false;
                btntxtBroker_CODE.Enabled = false;
                btntxtTRANSPORT_CODE.Enabled = false;
                btntxtNARRATION1.Enabled = false;
                btntxtNARRATION2.Enabled = false;
                btntxtNARRATION3.Enabled = false;
                btntxtNARRATION4.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                btnEditDetails.Enabled = false;

                btntxtPurcNo.Enabled = false;
                btnMail.Enabled = true;
                drpDeliveryType.Enabled = false;
                btntxtUTRNo.Enabled = false;
                btntxtcarporateSale.Enabled = false;
                // btnTransLetter.Enabled = true;
                btnWayBill.Enabled = true;
                ddlFrieghtType.Enabled = false;
                btnOurDO.Enabled = true;
                btnPartyBillDO.Enabled = true;
                btnPrintSaleBill.Enabled = true;
                // btnPrintCarpVoucher.Enabled = true;
                btnPrintMotorMemo.Enabled = true;
                // btnPrintITCVoc.Enabled = true;
                btnSendSms.Enabled = true;

                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                #endregion

                drpInsured.Enabled = false;
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;

                        if (((System.Web.UI.WebControls.TextBox)c).Text == "0.00")
                        {
                            ((System.Web.UI.WebControls.TextBox)c).Text = string.Empty;
                        }
                    }
                }
                foreach (System.Web.UI.Control c in pnlVoucherEntries.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;

                        if (((System.Web.UI.WebControls.TextBox)c).Text == "0.00")
                        {
                            ((System.Web.UI.WebControls.TextBox)c).Text = string.Empty;
                        }
                    }
                }
                txtGETPASS_CODE.Enabled = false;
                btntxtGetpassGstStateCode.Enabled = true;
                btntxtVoucherbyGstStateCode.Enabled = true;
                btntxtSalebilltoGstStateCode.Enabled = true;
                btntxtMillGstStateCode.Enabled = true;
                btntxtTransportGstStateCode.Enabled = true;
                btntxtGstRate.Enabled = true;
                btntxtVasuliAc.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtitem_Code.Enabled = true;
                btntxtitem_Code.Enabled = true;
                //txtSaleBillTo.Enabled = false;
                btntxtMemoGSTRate.Enabled = true;
                txtNARRATION4.Enabled = false;
                txtcarporateSale.Enabled = false;
                txtMillMobile.Enabled = true;
                txtMillMobile.Text.Trim();
                btnVoucherOtherAmounts.Enabled = true;
                drpCC.Enabled = true;
                btnSendSms.Enabled = true;
                hdnfpacking.Value = "1";
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = true;
                lblMsg.Text = string.Empty;
                drpDeliveryType.Enabled = false;


                txtBill_To.Enabled = false;
                btntxtbill_To.Enabled = false;
                #region set Business logic for edit
                CalendarExtender2.Enabled = true;
                CalendarExtender4.Enabled = true;
                calenderExtenderDate.Enabled = true;
                CalendarExtender1.Enabled = true;
                drpDOType.Enabled = false;
                btntxtMILL_CODE.Enabled = true;
                btntxtGETPASS_CODE.Enabled = true;
                btntxtvoucher_by.Enabled = true;
                btntxtGRADE.Enabled = true;
                btntxtDO_CODE.Enabled = true;
                btntxtBroker_CODE.Enabled = true;
                btntxtTRANSPORT_CODE.Enabled = true;
                btntxtNARRATION1.Enabled = true;
                btntxtNARRATION2.Enabled = true;
                btntxtNARRATION3.Enabled = true;
                btntxtNARRATION4.Enabled = true;
                btnOpenDetailsPopup.Enabled = true;
                btnEditDetails.Enabled = true;

                btntxtPurcNo.Enabled = true;
                txtUTRNo.Text = string.Empty;
                txtUTRNo.Enabled = false;
                lblUTRYearCode.Text = string.Empty;
                lblUTRCompnyCode.Text = string.Empty;
                btntxtUTRNo.Enabled = true;
                btnPrintSaleBill.Enabled = false;
                btntxtcarporateSale.Enabled = false;
                btnMail.Enabled = false;
                // btnTransLetter.Enabled = false;
                btnWayBill.Enabled = false;
                btnOurDO.Enabled = false;
                btnPartyBillDO.Enabled = false;
                ddlFrieghtType.Enabled = true;
                // btnPrintCarpVoucher.Enabled = false;
                btnPrintMotorMemo.Enabled = false;
                // btnPrintITCVoc.Enabled = false;
                string ov = lblVoucherNo.Text.ToString();
                string sb = lblSB_No.Text != string.Empty ? lblSB_No.Text : "0";
                if (ov == "0" || sb == "0")
                {
                    txtmillRate.ReadOnly = false;
                }
                else
                {
                    txtmillRate.ReadOnly = true;
                }
                #endregion
                btntxtTDSAc.Enabled = true;
                btntxtCashDiffAc.Enabled = true;
                txtMillGstStateCode.Enabled = false;
                txtGetpassGstStateCode.Enabled = false;
                txtVoucherbyGstStateCode.Enabled = false;
                txtSalebilltoGstStateCode.Enabled = false;
                txtTransportGstStateCode.Enabled = false;

                txtMillInv_Date.Enabled = true;
                chkInv_Chk.Enabled = true;
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                drpInsured.Enabled = true;
            }
            #region Always check this
            string s_item = "";
            s_item = drpDOType.SelectedValue;
            if (dAction == "E" || dAction == "A")
            {
                if (s_item == "DI")
                {
                    pnlgrdDetail.Enabled = true;
                    btnOpenDetailsPopup.Enabled = true;
                    btntxtUTRNo.Enabled = true;
                    //txtUTRNo.Enabled = true;
                    //grdDetail.DataSource = null;
                    //grdDetail.DataBind();
                }
                else
                {
                    pnlgrdDetail.Enabled = false;
                    btnOpenDetailsPopup.Enabled = false;
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();

                    txtUTRNo.Text = "";
                    lblUTRYearCode.Text = "";
                    lblUTRCompnyCode.Text = "";
                    //btntxtUTRNo.Enabled = false;
                    //txtUTRNo.Enabled = false;
                }
            }
            #endregion
            txtPurcNo.Enabled = false;
            txtPurcOrder.Enabled = false;
            if (txtcarporateSale.Text != string.Empty)
            {
                txtcarporateSale.Enabled = false;
            }

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

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int i = 0;
            // if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(6);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(7);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(6);
                e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(6);
                e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(12);
                e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(35);
                e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(30);
                e.Row.Cells[7].ControlStyle.Width = Unit.Percentage(16);
                e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[9].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[10].ControlStyle.Width = Unit.Percentage(15);
                e.Row.Cells[11].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[12].ControlStyle.Width = Unit.Percentage(20);
                e.Row.Cells[13].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[14].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[15].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[16].ControlStyle.Width = Unit.Percentage(10);
                //  e.Row.Cells[17].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[8].Visible = true;
                e.Row.Cells[9].Visible = true;
                e.Row.Cells[10].Visible = true;
                e.Row.Cells[11].Visible = true;


                e.Row.Cells[5].Style["overflow"] = "hidden";
                e.Row.Cells[6].Style["overflow"] = "hidden";


                i++;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string s = cell.Text.ToString();
                    if (cell.Text.Length > 33)
                    {
                        cell.Text = cell.Text.Substring(0, 33) + "...";
                        cell.ToolTip = s;
                    }
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
                if (v == "txtitem_Code")
                {
                    e.Row.Cells[0].Width = new Unit("50px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
                if (v == "txtGRADE")
                {
                    e.Row.Cells[0].Width = new Unit("400px");
                }
                if (v == "txtGETPASS_CODE" || v == "txtvoucher_by" || v == "txtBroker_CODE" || v == "txtDO_CODE" || v == "txtTRANSPORT_CODE" || v == "txtVasuliAc" || v == "txtTDSAc" || v == "txtCashDiffAc")
                {

                    e.Row.Cells[0].Width = new Unit("90px");
                    e.Row.Cells[1].Width = new Unit("400px");
                    e.Row.Cells[2].Width = new Unit("150px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                }

                if (v == "txtDO_CODE")
                {

                    e.Row.Cells[0].Width = new Unit("90px");
                    e.Row.Cells[1].Width = new Unit("400px");
                    e.Row.Cells[2].Width = new Unit("150px");
                    e.Row.Cells[3].Width = new Unit("150px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                }

                if (v == "txtMILL_CODE")
                {
                    e.Row.Cells[0].Width = new Unit("90px");
                    e.Row.Cells[1].Width = new Unit("400px");
                    e.Row.Cells[2].Width = new Unit("200px");
                    e.Row.Cells[3].Width = new Unit("150px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                }
                if (v == "txtdoc_no" || v == "txtEditDoc_No")
                {

                    e.Row.Cells[0].Width = new Unit("50px");
                    e.Row.Cells[1].Width = new Unit("150px");
                    e.Row.Cells[2].Width = new Unit("100px");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    //e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(60);
                    //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                }
                if (v == "txtdoc_no" || v == "txtEditDoc_No")
                {

                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(20);
                    e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(20);
                    e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(25);
                    e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(5);

                    i++;
                    //foreach (TableCell cell in e.Row.Cells)
                    //{
                    //    string s = cell.Text;
                    //    if (cell.Text.Length > 20)
                    //    {
                    //        cell.Text = cell.Text.Substring(0, 20) + "(..)";
                    //        cell.ToolTip = s;
                    //    }

                    //}
                }
                if (v == "txtMillGstStateCode" || v == "txtGetpassGstStateCode" || v == "txtVoucherbyGstStateCode" || v == "txtSalebilltoGstStateCode" || v == "txtTransportGstStateCode")
                {
                    e.Row.Cells[0].Width = new Unit("100px");
                    e.Row.Cells[1].Width = new Unit("400px");
                }
                if (v == "txtcarporateSale")
                {

                    e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(8);
                    e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(20);
                    e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(15);
                    e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(10);
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                    i++;
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        string s = cell.Text;
                        if (cell.Text.Length > 30)
                        {
                            cell.Text = cell.Text.Substring(0, 30) + "(..)";
                            cell.ToolTip = s;
                        }

                    }
                }

                if (v == "txtPurcNo")
                {


                    e.Row.Cells[0].ControlStyle.Width = new Unit("50px");
                    e.Row.Cells[1].ControlStyle.Width = new Unit("60px");
                    e.Row.Cells[2].ControlStyle.Width = new Unit("140px");
                    e.Row.Cells[3].ControlStyle.Width = new Unit("140px");
                    e.Row.Cells[4].ControlStyle.Width = new Unit("80px");
                    e.Row.Cells[5].ControlStyle.Width = new Unit("120px");

                    e.Row.Cells[6].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[7].ControlStyle.Width = new Unit("100px");
                    e.Row.Cells[8].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[9].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[10].ControlStyle.Width = new Unit("140px");
                    e.Row.Cells[11].ControlStyle.Width = new Unit("90px");

                    e.Row.Cells[12].ControlStyle.Width = new Unit("80px");
                    e.Row.Cells[13].ControlStyle.Width = new Unit("40px");
                    e.Row.Cells[14].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[15].ControlStyle.Width = new Unit("30px");
                    e.Row.Cells[16].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[16].ControlStyle.Width = new Unit("30px");

                    //grdPopup.Style["table-layout"] = "auto";
                    //grdPopup.CellSpacing = 10;

                    //i++;
                    //foreach (TableCell cell in e.Row.Cells)
                    //{
                    //    string s = cell.Text;
                    //    if (cell.Text.Length > 25)
                    //    {
                    //        cell.Text = cell.Text.Substring(0, 25) + "(..)";
                    //        cell.ToolTip = s;
                    //    }

                    //}
                }
                if (v == "txtGstRate" || v == "txtMemoGSTRate")
                {
                    e.Row.Cells[0].ControlStyle.Width = new Unit("50px");
                    e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
                    e.Row.Cells[2].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[3].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[4].ControlStyle.Width = new Unit("90px");
                    e.Row.Cells[5].ControlStyle.Width = new Unit("90px");
                }
                if (v == "txtUTRNo")
                {

                    e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
                    e.Row.Cells[1].ControlStyle.Width = new Unit("240px");
                    e.Row.Cells[2].ControlStyle.Width = new Unit("440px");
                    e.Row.Cells[3].ControlStyle.Width = new Unit("150px");
                    e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
                    e.Row.Cells[5].ControlStyle.Width = new Unit("150px");
                    e.Row.Cells[6].ControlStyle.Width = new Unit("40px");
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

                    i++;
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        string s = cell.Text;
                        if (cell.Text.Length > 50)
                        {
                            cell.Text = cell.Text.Substring(0, 50) + "(..)";
                            cell.ToolTip = s;
                        }

                    }
                }

            }
            //e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
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
            int pgCount = 0;
            pgCount = grdPopup.PageCount;
            if (e.Row.RowType == DataControlRowType.DataRow &&
               (e.Row.RowState == DataControlRowState.Normal ||
                e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex, pgCount);
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
                        if (grdDetail.Rows[rowindex].Cells[12].Text != "D" && grdDetail.Rows[rowindex].Cells[12].Text != "R")
                        {
                            pnlPopupDetails.Style["display"] = "block";
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
    }
    #endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and tran_type='" + trnType + "'";
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
        if (txtdoc_no.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [doc_no] from " + tblHead +
                    " where doid>" + Convert.ToInt32(hdnf.Value) +
                    " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "' ORDER BY doc_no asc  ";
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doid<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and tran_type='" + trnType + "' ORDER BY doc_no asc  ";
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
            query = "select doid from " + tblHead + "  where doid=(select MIN(doid) from " + tblHead + "  where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "') and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                  "  and tran_type='" + trnType + "'";
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
            if (txtdoc_no.Text != string.Empty)
            {
                string query = "SELECT top 1 [doid] from " + tblHead + "  where doid<" + Convert.ToInt32(hdnf.Value) +
                    "  and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and tran_type='" + trnType + "'" +
                    " ORDER BY doid DESC  ";
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
            if (txtdoc_no.Text != string.Empty)
            {
                string query = "SELECT top 1 [doid] from " + tblHead + "  where doid>" + Convert.ToInt32(hdnf.Value) +
                    "  and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and tran_type='" + trnType + "'" +
                    " ORDER BY doid asc  ";
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
            //query = "select doc_no from " + tblHead + "  where doc_no=(select MAX(doc_no) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "') and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and tran_type='" + trnType + "'";
            query = "select doid from " + tblHead + "  where doid=(select MAX(doid) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "') and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and tran_type='" + trnType + "'";


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
        drpDOType.SelectedValue = "DI";
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        txtGstRate.Text = "1";
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.getMaxCode();
        pnlPopupDetails.Style["display"] = "none";
        lblPDSParty.Text = "";
        lblSB_No.Text = "0";

        this.NextNumber();
        lblGstRateName.Text = clsCommon.getString("Select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1");


        txtMemoGSTRate.Text = txtGstRate.Text;
        lblMemoGSTRate.Text = lblGstRateName.Text;
        hdnfOldBillAmt.Value = "0";
        setFocusControl(txtDOC_DATE);
        btnperforminvoice.Enabled = false;
        drpInsured.SelectedValue = "N";
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {


        if (lblMsg.Text != "Delete")
        {
            ViewState["mode"] = null;
            ViewState["mode"] = "U";
            clsButtonNavigation.enableDisable("E");
            pnlgrdDetail.Enabled = true;
            this.makeEmptyForm("E");
            txtdoc_no.Enabled = false;
            hdnfpacking.Value = "2";
            // hdnfQty.Value = txtquantal.Text;


            PreQntl();
            //carporatesale();

            int vn = lblVoucherNo.Text.Trim() != string.Empty ? Convert.ToInt32(lblVoucherNo.Text) : 0;
            int sbn = lblSB_No.Text.Trim() != string.Empty ? Convert.ToInt32(lblSB_No.Text) : 0;
            if (vn != 0)
            {
                txtmillRate.ReadOnly = true;
            }
            else if (sbn != 0)
            {
                txtmillRate.ReadOnly = true;
            }
            else
            {
                txtmillRate.ReadOnly = false;
            }
            txtEditDoc_No.Text = string.Empty;



            OldQty = txtquantal.Text != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0;

            OldSaleAmt = txtSALE_RATE.Text != string.Empty ? Convert.ToDouble(txtSALE_RATE.Text) : 0;
            OldCommission = txtCommission.Text != string.Empty ? Convert.ToDouble(txtCommission.Text) : 0;
            OldGSTAmt = (OldSaleAmt * 5) / 100;
            OldBillAmt = (OldGSTAmt + OldSaleAmt + OldCommission) * OldQty;
            hdnfOldBillAmt.Value = OldBillAmt.ToString();
            setFocusControl(txtDOC_DATE);
            if (txtexcise_rate.Text == string.Empty || txtexcise_rate.Text == "0.00")
            {
                txtexcise_rate.Text = "0.00";
            }
            btnGenEinvoice.Enabled = false;
            btnGentare_EWayBill.Enabled = false;
            if (txteinvoiceno.Text != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Einvoice Already generated!!!!!');", true);

            }

            hdnfsalebillto.Value = txtSaleBillTo.Text;
            if (Edit_Sale_Rate == "Y")
            {
                if (drpDeliveryType.SelectedValue == "C")
                {
                    txtSALE_RATE.Enabled = true;
                }
            }
            else {
                if (drpDeliveryType.SelectedValue == "C")
                {
                    txtSALE_RATE.Enabled = false;
                } 
            }
            if (drpDOType.SelectedValue == "DO")
            {
                txtGETPASS_CODE.Enabled = true;
            }
            
            //AmtCalculation();
        }
    }

    private void PreQntl()
    {
        if (txtcarporateSale.Text != "0" || !string.IsNullOrWhiteSpace(txtcarporateSale.Text))
        {
            ViewState["PreQntl"] = txtquantal.Text.ToString();
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
                if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty)
                {
                    string refsbno = "0";
                    string refdoid = clsCommon.getString(" select top(1)   DOC_NO from multiple_receipt_detail where Bill_Tran_Type ='DO'and Bill_Auto_Id=" + hdnf.Value);
                    if (hdnfsaleid.Value != "0")
                    {
                        refsbno = clsCommon.getString(" select top(1)   DOC_NO from multiple_receipt_detail where Bill_Tran_Type ='SB'and Bill_Auto_Id=" + hdnfsaleid.Value);
                    }
                    string concat = string.Empty;

                    if (refdoid != string.Empty && refdoid != "0")
                    {
                        concat = "DO DocNo: " + refdoid + "";
                    }
                    if (refsbno != string.Empty && refsbno != "0")
                    {
                        concat = concat + "Receipt Payment DocNo: " + refsbno + "";
                    }
                    if (refdoid == "0" && refsbno == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Record Use In " + concat + "')", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Do No EInvoice is generated so it cannot be Deleted')", true);
                    return;
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

        //hdnf.Value = clsCommon.getString("select max(doid) from nt_1_deliveryorder where Company_Code=" + Session["Company_Code"].ToString() + " and " +
        //        " Year_Code=" + Session["year"].ToString() + "");


        //clsButtonNavigation.enableDisable("S");
        //this.makeEmptyForm("S");
        //qry = getDisplayQuery();
        //this.fetchRecord(qry);
        hdnf.Value = Request.QueryString["DO"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(doid),0) as doid from nt_1_deliveryorder "));
            clsButtonNavigation.enableDisable("S");
            hdnf.Value = Convert.ToString(maxno);
            this.enableDisableNavigateButtons();
        }
        Response.Redirect("pgeDeliveryOrderForGSTxmlNew.aspx?DO=" + hdnf.Value + "&Action=" + 1);
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
                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnf.Value = dt.Rows[0]["doid"].ToString();
                        hdnfDoid.Value = dt.Rows[0]["doid"].ToString();
                        txtdoc_no.Text = dt.Rows[0]["DOC_NO"].ToString();
                        hdnfDodoc.Value = dt.Rows[0]["DOC_NO"].ToString();
                        lbldoid.Text = dt.Rows[0]["doid"].ToString();
                        txtseasons.Text = dt.Rows[0]["season"].ToString();

                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtdo_date.Text = dt.Rows[0]["Do_Date_Conv"].ToString();
                        txtMillInv_Date.Text = dt.Rows[0]["mill_inv_dateConverted"].ToString();
                        Inv_Chk = dt.Rows[0]["mill_rcv"].ToString();
                        if (Inv_Chk == "Y")
                        {
                            chkInv_Chk.Checked = true;
                        }
                        else
                        {
                            chkInv_Chk.Checked = false;
                        }
                        txtMillEwayBill_No.Text = dt.Rows[0]["MillEwayBill"].ToString();
                        drpDOType.SelectedValue = dt.Rows[0]["DESP_TYPE"].ToString();
                        if (drpDOType.SelectedValue == "DI")
                        {
                            drpDeliveryType.Visible = true;
                            btngenratesalebill.Enabled = true;
                        }
                        else
                        {
                            drpDeliveryType.Visible = false;
                            btngenratesalebill.Enabled = false;
                        }
                        txtpurchaserate.Text = dt.Rows[0]["PurchaseRate"].ToString();

                        txtMILL_CODE.Text = dt.Rows[0]["MILL_CODE"].ToString();
                        LBLMILL_NAME.Text = dt.Rows[0]["millName"].ToString();
                        txtGstRate.Text = dt.Rows[0]["GstRateCode"].ToString();
                        lblGstRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtMemoGSTRate.Text = dt.Rows[0]["MemoGSTRate"].ToString();
                        lblMemoGSTRate.Text = clsCommon.getString("select Rate from nt_1_gstratemaster where Doc_no=" + txtMemoGSTRate.Text + "");
                        txtRCM_No.Text = dt.Rows[0]["RCMNumber"].ToString();
                        memorgstratecal();
                        txtMillEmailID.Text = dt.Rows[0]["millemailid"].ToString();
                        txtMillMobile.Text = dt.Rows[0]["MobileNo"].ToString();
                        txtBill_To.Text = dt.Rows[0]["carporate_ac"].ToString();
                        lblBill_To.Text = dt.Rows[0]["carporateacname"].ToString();
                        string getpasscode = dt.Rows[0]["GETPASSCODE"].ToString();
                        // hdnf.Value = dt.Rows[0]["tenderdetailid"].ToString();
                        txtGETPASS_CODE.Text = getpasscode;
                        string getpasscodecitycode = clsCommon.getString("Select City_Code from " + qryAccountList + "  where Ac_Code=" + getpasscode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string getpasscity = clsCommon.getString("Select city_name_e from " + tblPrefix + "CityMaster  where city_code=" + getpasscodecitycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        LBLGETPASS_NAME.Text = dt.Rows[0]["GetPassName"].ToString() + ", " + getpasscity;
                        txtGetpassGstStateCode.Text = dt.Rows[0]["GetpassGstStateCode"].ToString();
                        lbltxtGetpassGstStateName.Text = dt.Rows[0]["getpassstatename"].ToString();
                        txtVoucherbyGstStateCode.Text = dt.Rows[0]["VoucherbyGstStateCode"].ToString();
                        lbltxtVoucherbyGstStateName.Text = clsCommon.getString("select State_Name from gststatemaster where State_Code=" + txtVoucherbyGstStateCode.Text + "");
                        //lbltxtVoucherbyGstStateName.Text = dt.Rows[0]["gstmillstatename"].ToString();
                        txtSalebilltoGstStateCode.Text = dt.Rows[0]["SalebilltoGstStateCode"].ToString();
                        lbltxtSalebilltoGstStateName.Text = dt.Rows[0]["gststatesellbillname"].ToString();
                        txtGRADE.Text = dt.Rows[0]["GRADE"].ToString();


                        txtMillGstStateCode.Text = dt.Rows[0]["MillGSTStateCode"].ToString();
                        lbltxtMillGstStateCode.Text = dt.Rows[0]["gstmillstatename"].ToString();
                        txtTransportGstStateCode.Text = dt.Rows[0]["TransportGSTStateCode"].ToString();
                        lbltxtTransportGstStateCode.Text = dt.Rows[0]["gststatetransportname"].ToString();
                        string VoucherByCode = dt.Rows[0]["VOUCHER_BY"].ToString();
                        txtvoucher_by.Text = VoucherByCode;
                        lblVoucherLedgerByBalance.Text = AcBalance(VoucherByCode);
                        txtitem_Code.Text = dt.Rows[0]["itemcode"].ToString();
                        lblitem_Name.Text = dt.Rows[0]["itemname"].ToString();
                        hdnfhsnnumber.Value = clsCommon.getString("select HSN from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        hdnfminRate.Value = clsCommon.getString("select minRate from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        hdnfmaxRate.Value = clsCommon.getString("select maxRate from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");

                        //  lblvoucherbyname.Text = dt.Rows[0]["VoucherByname"].ToString();
                        string voucherbycodecitycode = clsCommon.getString("Select City_Code from " + tblPrefix + "AccountMaster  where Ac_Code=" + VoucherByCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string voucherbycity = clsCommon.getString("Select city_name_e from " + tblPrefix + "CityMaster  where city_code=" + voucherbycodecitycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        lblvoucherbyname.Text = dt.Rows[0]["VoucherByname"].ToString() + ", " + voucherbycity;
                        txtquantal.Text = dt.Rows[0]["QUANTAL"].ToString();
                        txtPACKING.Text = dt.Rows[0]["PACKING"].ToString();
                        txtBAGS.Text = dt.Rows[0]["BAGS"].ToString();
                        txtexcise_rate.Text = dt.Rows[0]["EXCISE_RATE"].ToString();
                        txtmillRate.Text = dt.Rows[0]["mill_rate"].ToString();
                        txtSALE_RATE.Text = dt.Rows[0]["SALE_RATE"].ToString();
                        txtCommission.Text = dt.Rows[0]["Tender_Commission"].ToString();
                        lblDiffrate.Text = dt.Rows[0]["DIFF_RATE"].ToString();
                        txtDIFF_AMOUNT.Text = dt.Rows[0]["DIFF_AMOUNT"].ToString();
                        txtDO_CODE.Text = dt.Rows[0]["DO"].ToString();
                        txtEWayBill_No.Text = dt.Rows[0]["EWay_Bill_No"].ToString();

                        txtEwayBill_ValidDate.Text = dt.Rows[0]["EwayBillValidDate"].ToString();

                        txtnewsbno.Text = dt.Rows[0]["newsbno"].ToString();
                        txtnewsbdate.Text = dt.Rows[0]["newsbdate"].ToString();
                        txteinvoiceno.Text = dt.Rows[0]["einvoiceno"].ToString();
                        txtackno.Text = dt.Rows[0]["ackno"].ToString();

                        LBLDO_NAME.Text = dt.Rows[0]["DOName"].ToString();
                        txtBroker_CODE.Text = dt.Rows[0]["BROKER"].ToString();
                        txtDistance.Text = dt.Rows[0]["Distance"].ToString();
                        LBLBROKER_NAME.Text = dt.Rows[0]["BrokerName"].ToString();
                        txtTruck_NO.Text = dt.Rows[0]["TRUCK_NO"].ToString();
                        txtTRANSPORT_CODE.Text = dt.Rows[0]["TRANSPORT"].ToString();
                        LBLTRANSPORT_NAME.Text = dt.Rows[0]["TransportName"].ToString();
                        txtNARRATION1.Text = dt.Rows[0]["NARRATION1"].ToString();
                        txtNARRATION2.Text = dt.Rows[0]["NARRATION2"].ToString();
                        txtNARRATION3.Text = dt.Rows[0]["NARRATION3"].ToString();
                        txtNARRATION4.Text = dt.Rows[0]["NARRATION4"].ToString();
                        txtNarration5.Text = dt.Rows[0]["NARRATION5"].ToString();
                        txtsbnarration.Text = dt.Rows[0]["SBNARRATION"].ToString();
                        lblLoadingSms.Text = dt.Rows[0]["LoadingSms"].ToString();
                        lblEmail.Text = dt.Rows[0]["MailSend"].ToString();
                        txtPurcNo.Text = dt.Rows[0]["purc_no"].ToString();
                        txtPurchase_Date.Text = dt.Rows[0]["Purchase_DateConverted"].ToString();
                        txtPurcOrder.Text = dt.Rows[0]["purc_order"].ToString();
                        txtDriverMobile.Text = dt.Rows[0]["driver_no"].ToString();
                        // txtINVOICE_NO.Text = dt.Rows[0]["Invoice_No"].ToString();
                        txtVasuliRate1.Text = dt.Rows[0]["vasuli_rate1"].ToString();
                        txtVasuliAmount1.Text = dt.Rows[0]["vasuli_amount1"].ToString();
                        txtVasuliAc.Text = dt.Rows[0]["Vasuli_Ac"].ToString();
                        //  txtSB_Other_Amount.Text = dt.Rows[0]["SB_Other_Amount"].ToString();
                        // string vByname = dt.Rows[0]["VasuliAcName"].ToString();
                        //string tooltip = vByname;
                        //if (vByname.Length > 25)
                        //{
                        //    vByname = vByname.Substring(0, vByname.Length - 25);
                        //}vasuliacname
                        lbltxtVasuliAc.Text = dt.Rows[0]["vasuliacname"].ToString();

                        // lbltxtVasuliAc.ToolTip = tooltip;
                        //txtPartyCommission.Text = dt.Rows[0]["Party_Commission_Rate"].ToString();
                        hdnfTenderDetailid.Value = dt.Rows[0]["tenderdetailid"].ToString();
                        txtMemoAdvanceRate.Text = dt.Rows[0]["MM_Rate"].ToString();
                        drpCC.SelectedValue = dt.Rows[0]["MM_CC"].ToString();
                        txtVoucherBrokrage.Text = dt.Rows[0]["Voucher_Brokrage"].ToString();
                        txtVoucherServiceCharge.Text = dt.Rows[0]["Voucher_Service_Charge"].ToString();
                        txtVoucherL_Rate_Diff.Text = dt.Rows[0]["Voucher_RateDiffRate"].ToString();
                        txtVoucherRATEDIFFAmt.Text = dt.Rows[0]["Voucher_RateDiffAmt"].ToString();
                        txtVoucherCommission_Rate.Text = dt.Rows[0]["Voucher_BankCommRate"].ToString();
                        txtVoucherBANK_COMMISSIONAmt.Text = dt.Rows[0]["Voucher_BankCommAmt"].ToString();
                        txtVoucherInterest.Text = dt.Rows[0]["Voucher_Interest"].ToString();
                        txtVoucherTransport_Amount.Text = dt.Rows[0]["Voucher_TransportAmt"].ToString();
                        txtVoucherOTHER_Expenses.Text = dt.Rows[0]["Voucher_OtherExpenses"].ToString();
                        //txtCheckPostName.Text = dt.Rows[0]["CheckPost"].ToString();
                        txtPanNo.Text = dt.Rows[0]["Pan_No"].ToString();
                        if (dt.Rows[0]["WhoseFrieght"].ToString() != "")
                        {
                            ddlFrieghtType.SelectedValue = dt.Rows[0]["WhoseFrieght"].ToString();
                        }
                        txtMillInvoiceno.Text = dt.Rows[0]["MillInvoiceNo"].ToString();

                        string ischecked = dt.Rows[0]["EWayBillChk"].ToString();
                        if (ischecked == "Y")
                        {
                            chkEWayBill.Checked = true;
                            lblchkEWayBill.Text = LBLMILL_NAME.Text;

                        }
                        else
                        {
                            chkEWayBill.Checked = false;

                            lblchkEWayBill.Text = "";
                        }
                        hdnfTenderID.Value = dt.Rows[0]["tenderid"].ToString();
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
                        txtFrieght.Text = dt.Rows[0]["FreightPerQtl"].ToString();
                        double frtAmount = Convert.ToDouble(dt.Rows[0]["Freight_Amount"].ToString());
                        txtFrieghtAmount.Text = frtAmount.ToString();

                        txtVasuliRate.Text = dt.Rows[0]["vasuli_rate"].ToString();
                        txtVasuliAmount.Text = dt.Rows[0]["vasuli_amount"].ToString();

                        double memoadvance = Convert.ToDouble(dt.Rows[0]["memo_advance"].ToString());
                        txtMemoAdvance.Text = memoadvance.ToString();

                        lblFrieghtToPay.Text = "Frieght To Pay: " + (frtAmount - memoadvance).ToString();

                        string CS_No = dt.Rows[0]["Carporate_Sale_No"].ToString();
                        txtcarporateSale.Text = CS_No;
                        CS_No = CS_No != string.Empty ? CS_No : "0";
                        PDS = clsCommon.getString("Select selling_type from carporatehead  where doc_no=" + CS_No + " and Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        hdnfPDS.Value = PDS;
                        lblCSYearCode.Text = PDS;
                        if (PDS == "P")
                        {
                            string prtyCode = clsCommon.getString("Select ac_code from carporatehead  where doc_no=" + CS_No + " and Company_Code="
                                + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            hdnfPDSPartyCode.Value = prtyCode;
                            string unitCode = clsCommon.getString("Select unit_code from carporatehead  where doc_no=" + CS_No +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            hdnfPDSUnitCode.Value = unitCode;
                            string nm = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster  where Ac_Code=" + prtyCode +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            lblPDSParty.Text = "<b Style=" + "color:black;" + ">Party:</b> " + nm;
                            btnPrintSaleBill.Visible = true;


                            //    hdnfbilltoStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + txtBill_To.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                            //txtBill_To.Text = clsCommon.getString("Select unit_code from carporatehead  where doc_no=" + CS_No +
                            //    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                            //string nm1 = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster  where Ac_Code=" + txtBill_To.Text +
                            //    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                            //lblBill_To.Text = nm1;

                        }
                        else
                        {
                            hdnfPDSPartyCode.Value = "0";
                            lblPDSParty.Text = "";
                            btnPrintSaleBill.Enabled = true;
                        }
                        //txtBill_To.Text = clsCommon.getString("Select unit_code from carporatehead  where doc_no=" + CS_No +
                        //        " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        //string nm1 = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster  where Ac_Code=" + txtBill_To.Text +
                        //    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                        //lblBill_To.Text = nm1;
                        //hdnfbt.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + Bill_To + " and Company_code='" + Session["Company_Code"].ToString() + "'");

                        hdnfpdsacID.Value = clsCommon.getString("select isnull(accoid,0) as id from nt_1_accountmaster where Ac_Code=" + hdnfPDSPartyCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        hdnfpdsunitID.Value = clsCommon.getString("select isnull(accoid,0) as id from nt_1_accountmaster where Ac_Code=" + hdnfPDSUnitCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        hdnfpdspartyStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + hdnfPDSPartyCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        hdnfpdsunitStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + hdnfPDSUnitCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        hdnfbilltoStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + txtBill_To.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        lblCSYearCode.Text = dt.Rows[0]["Carporate_Sale_Year_Code"].ToString();
                        lblMillAmount.Text = dt.Rows[0]["final_amout"].ToString();
                        lblMillAmtWOTCS.Text = dt.Rows[0]["Mill_AmtWO_TCS"].ToString();

                        if (txtcarporateSale.Text == "0")
                        {
                            txtcarporateSale.Text = "";
                        }
                        txtUTRNo.Text = dt.Rows[0]["Utr_No"].ToString();
                        lblUTRYearCode.Text = dt.Rows[0]["UTR_Year_Code"].ToString();
                        //lblUTRCompnyCode.Text = dt.Rows[0]["UTR_Year_Code"].ToString();
                        if (txtUTRNo.Text == "0")
                        {
                            txtUTRNo.Text = "";
                        }
                        hdnvouchernumber.Value = dt.Rows[0]["voucher_no"].ToString();
                        lblVoucherNo.Text = hdnvouchernumber.Value.TrimStart();
                        lblVoucherType.Text = dt.Rows[0]["voucher_type"].ToString();
                        hdnmemonumber.Value = dt.Rows[0]["memo_no"].ToString();
                        lblMemoNo.Text = hdnmemonumber.Value.TrimStart();
                        txtSaleBillTo.Text = dt.Rows[0]["SaleBillTo"].ToString();
                        lblSaleBillToLedgerByBalance.Text = AcBalance(txtSaleBillTo.Text);
                        string SB_No = dt.Rows[0]["SB_No"].ToString();

                        hdnfmc.Value = dt.Rows[0]["mc"].ToString();
                        hdnfgp.Value = dt.Rows[0]["gp"].ToString();
                        hdnfsb.Value = dt.Rows[0]["sb"].ToString();
                        hdnfst.Value = dt.Rows[0]["st"].ToString();
                        hdnftc.Value = dt.Rows[0]["tc"].ToString();
                        hdnfva.Value = dt.Rows[0]["va"].ToString();
                        hdnfbk.Value = dt.Rows[0]["bk"].ToString();
                        hdnfdocd.Value = dt.Rows[0]["docd"].ToString();
                        hdnfbt.Value = dt.Rows[0]["ca"].ToString();
                        hdnfic.Value = dt.Rows[0]["ic"].ToString();
                        hdnfcscode.Value = dt.Rows[0]["cs"].ToString();
                        txtBrand_Code.Text = dt.Rows[0]["brandcode"].ToString();
                        lblBrandname.Text = dt.Rows[0]["Marka"].ToString();

                        hdnfmillshortname.Value = dt.Rows[0]["millshortname"].ToString();
                        hdnfsalebilltoshortname.Value = dt.Rows[0]["billtoshortname"].ToString();
                        hdnfshiptoshortname.Value = dt.Rows[0]["shiptoshortname"].ToString();
                        hdnftransportshortname.Value = dt.Rows[0]["transportshortname"].ToString();
                        hdnfgetpassshortname.Value = dt.Rows[0]["getpassshortname"].ToString();
                        hdnfbrokershortName.Value = dt.Rows[0]["BrokerName"].ToString();
                        hdnfbilltoshortname.Value = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster  where Ac_Code=" + txtBill_To.Text +
                              " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");


                        string sellbillno = clsCommon.getString("select isnull(doc_no,0) as id from nt_1_sugarsale where DO_No=" + txtdoc_no.Text + " and " +
                           "Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                        if (sellbillno == "0")
                        {
                            btnperforminvoice.Enabled = true;
                        }
                        else
                        {
                            btnperforminvoice.Enabled = false;
                        }
                        if (!string.IsNullOrEmpty(SB_No))
                        {
                            if (SB_No != "0")
                            {
                                lblsbnol.Text = "Sale Bill No:";
                                lblSB_No.Text = SB_No;
                                hdnfSB_No.Value = SB_No;//"<b Style=" + "color:Black;" + ">Sale Bill No:</b> " +
                                btngenratesalebill.Enabled = false;
                                btnGenEinvoice.Enabled = true;
                                btnGentare_EWayBill.Enabled = true;
                            }
                            else
                            {
                                lblsbnol.Text = "";
                                lblSB_No.Text = "";
                                btnPrintSaleBill.Enabled = false;
                                btngenratesalebill.Enabled = true;
                                btnGenEinvoice.Enabled = false;
                                btnGentare_EWayBill.Enabled = false;
                            }

                        }
                        else
                        {
                            lblsbnol.Text = "";
                            lblSB_No.Text = "";
                            btnPrintSaleBill.Enabled = false;
                            if (txteinvoiceno.Text != string.Empty)
                            {
                                btngenratesalebill.Enabled = false;
                            }
                            else
                            {
                                btngenratesalebill.Enabled = true;
                            }
                        }
                        string DT = dt.Rows[0]["Delivery_Type"].ToString();
                        if (DT == "C")
                        {
                            drpDeliveryType.SelectedValue = "C";
                        }
                        else if (DT == "A")
                        {
                            drpDeliveryType.SelectedValue = "A";
                        }
                        else
                        {
                            drpDeliveryType.SelectedValue = "N";
                        }
                        hdnfsaleid.Value = clsCommon.getString("select isnull(saleid,0) as id from nt_1_sugarsale where DO_No=" + txtdoc_no.Text + " and " +
                           "Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                        if (lblVoucherType.Text == "PS")
                        {
                            hdnfpurcid.Value = clsCommon.getString("select isnull(purchaseid,0) as id from nt_1_sugarpurchase where doc_no=" + lblVoucherNo.Text + " and " +
                                "Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                            hdnfcommid.Value = "0";
                        }
                        else
                        {
                            hdnfcommid.Value = clsCommon.getString("select isnull(commissionid,0) as id from commission_bill where Tran_Type='" + lblVoucherType.Text + "'and  doc_no=" + lblVoucherNo.Text + " and " +
                                "Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                            hdnfpurcid.Value = "0";
                        }
                        hdnfsaledetailid.Value = clsCommon.getString("select isnull(saledetailid,0) as id from nt_1_sugarsaledetails where saleid=" + hdnfsaleid.Value + " and " +
                            "Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                        hdnfpurcdetailid.Value = clsCommon.getString("select isnull(purchasedetailid,0) as id from nt_1_sugarpurchasedetails where doc_no=" + lblVoucherNo.Text + " and " +
                               "Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");

                        hdnfsupplieshortname.Value = dt.Rows[0]["shiptoshortname"].ToString();
                        // hdnfsupplieshortname.Value = dt.Rows[0]["billtoshortname"].ToString();
                        txtTCSRate.Text = dt.Rows[0]["TCS_Rate"].ToString();
                        txtTCSRate_Sale.Text = dt.Rows[0]["Sale_TCS_Rate"].ToString();
                        txtCashDiff.Text = dt.Rows[0]["Cash_diff"].ToString();
                        double cashdiffvalue1 = 0.00;
                        double cashdiff = 0.00;

                        cashdiffvalue1 = Convert.ToDouble(txtCashDiff.Text.Trim() != string.Empty ? Convert.ToDouble(txtCashDiff.Text) : 0);
                        cashdiff = (Convert.ToDouble(txtSALE_RATE.Text) - cashdiffvalue1) * Convert.ToDouble(txtquantal.Text);
                        lblCashdiffvalue.Text = cashdiff.ToString();
                        txtBrand_Code.Text = dt.Rows[0]["brandcode"].ToString();
                        lblBrandname.Text = dt.Rows[0]["Marka"].ToString();
                        txtCashDiffAc.Text = dt.Rows[0]["CashDiffAc"].ToString();
                        lblCashDiffAcname.Text = dt.Rows[0]["CashdiffName"].ToString();
                        txtTDSAc.Text = dt.Rows[0]["TDSAc"].ToString();
                        lblTDSAcname.Text = dt.Rows[0]["TDSName"].ToString();

                        txttdsrate.Text = dt.Rows[0]["TDSRate"].ToString();
                        txttdsamount.Text = dt.Rows[0]["TDSAmt"].ToString();
                        hdnfTDSAcid.Value = dt.Rows[0]["TDSAcId"].ToString();
                        hdnfCashDiffAcid.Value = dt.Rows[0]["CashDiffAcId"].ToString();

                        txtSaleTDS.Text = dt.Rows[0]["SaleTDSRate"].ToString();
                        drpInsured.SelectedValue = dt.Rows[0]["Insured"].ToString();
                        txtInsurance.Text = dt.Rows[0]["Insurance"].ToString();


                        //hdnfSaleTDS.Value = dt.Rows[0]["SaleTDSRate"].ToString();
                        txtPurchaseTDS.Text = dt.Rows[0]["PurchaseTDSRate"].ToString();
                        //hdnfPurchaseTDS.Value = dt.Rows[0]["PurchaseTDSRate"].ToString();
                        string ischeckedTDSCut = dt.Rows[0]["TDSCut"].ToString();
                        if (ischeckedTDSCut == "Y")
                        {
                            chkTDSCutByUs.Checked = true;


                        }
                        else
                        {
                            chkTDSCutByUs.Checked = false;


                        }

                        DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
                        if (ds1 != null)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                Session["GSTRate"] = ds1.Tables[0].Rows[0]["Rate"].ToString();
                                Session["igstrate"] = ds1.Tables[0].Rows[0]["IGST"].ToString();
                                Session["sgstrate"] = ds1.Tables[0].Rows[0]["SGST"].ToString();
                                Session["cgstrate"] = ds1.Tables[0].Rows[0]["CGST"].ToString();
                            }
                        }
                        DataSet dsi = clsDAL.SimpleQuery("select Purchase_AC,Sale_AC,PurcAcid,SaleAcid from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        if (dsi != null)
                        {
                            if (dsi.Tables[0].Rows.Count > 0)
                            {
                                Session["PurchAc"] = dsi.Tables[0].Rows[0]["Purchase_AC"].ToString();
                                Session["SaleAc"] = dsi.Tables[0].Rows[0]["Sale_AC"].ToString();
                                Session["Purcid"] = dsi.Tables[0].Rows[0]["PurcAcid"].ToString();
                                Session["Saleid"] = dsi.Tables[0].Rows[0]["SaleAcid"].ToString();
                            }

                        }
                        if (!string.IsNullOrEmpty(SB_No))
                        {
                            if (SB_No != "0")
                            {
                                #region
                                //if (txtEWayBill_No.Text == string.Empty || txtEWayBill_No.Text == "0")
                                //{
                                //    btnGentare_EWayBill.Enabled = true;
                                //    btnGenEInvoiceEwaybill.Enabled = false;
                                //}
                                //else
                                //{
                                //    btnGentare_EWayBill.Enabled = false;
                                //    btnGenEInvoiceEwaybill.Enabled = false;
                                //}
                                //if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txteinvoiceno.Text == "0" && txtackno.Text == "0")
                                //{
                                //    btnGenEinvoice.Enabled = true;
                                //    btnGenEInvoiceEwaybill.Enabled = false;
                                //}
                                //else
                                //{
                                //    btnGenEinvoice.Enabled = false;
                                //    btnGenEInvoiceEwaybill.Enabled = false;
                                //}
                                #endregion
                                if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty
                                    && txtEWayBill_No.Text == string.Empty)
                                {
                                    btnGenEInvoiceEwaybill.Enabled = true;
                                    btnGentare_EWayBill.Enabled = false;
                                    btnGenEinvoice.Enabled = false;

                                }
                                else
                                {
                                    btnGenEInvoiceEwaybill.Enabled = false;
                                    if (txtEWayBill_No.Text == string.Empty || txtEWayBill_No.Text == "0")
                                    {
                                        btnGentare_EWayBill.Enabled = true;
                                        btnGenEInvoiceEwaybill.Enabled = false;
                                    }
                                    else
                                    {
                                        btnGentare_EWayBill.Enabled = false;
                                        btnGenEInvoiceEwaybill.Enabled = false;
                                    }
                                    if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txteinvoiceno.Text == "0" && txtackno.Text == "0")
                                    {
                                        btnGenEinvoice.Enabled = true;
                                        btnGenEInvoiceEwaybill.Enabled = false;
                                    }
                                    else
                                    {
                                        btnGenEinvoice.Enabled = false;
                                        btnGenEInvoiceEwaybill.Enabled = false;
                                    }
                                }

                            }
                            else
                            {
                                btnGentare_EWayBill.Enabled = false;
                                btnGenEinvoice.Enabled = false;
                                btnGenEInvoiceEwaybill.Enabled = false;
                            }
                        }
                        else
                        {
                            btnGentare_EWayBill.Enabled = false;
                            btnGenEinvoice.Enabled = false;
                            btnGenEInvoiceEwaybill.Enabled = false;
                        }
                        string AutopurchaseBill = dt.Rows[0]["AutoPurchaseBill"].ToString();

                        hdnfAutoPurchaseBill.Value = AutopurchaseBill;

                        Int32 autops = Convert.ToInt32(clsCommon.getString("select count(isnull(purc_no,0)) from nt_1_deliveryorder where purc_no=" + txtPurcNo.Text));

                        //if (autops > 0)
                        //{
                        //    hdnfAutoPurchaseBill.Value = "N";
                        //}


                        string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                        if (User_Type == "A")
                        {
                            if (txteinvoiceno.Text != string.Empty && txtackno.Text != string.Empty)
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

                        DataSet dsdncn = clsDAL.SimpleQuery("select tran_type,doc_no from debitnotehead where bill_no=" + hdnfsaleid.Value + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                        string dncn = string.Empty;
                        if (dsdncn != null)
                        {
                            if (dsdncn.Tables[0].Rows.Count > 0)
                            {
                                string tran_type = dsdncn.Tables[0].Rows[0]["tran_type"].ToString();
                                string doc_no = dsdncn.Tables[0].Rows[0]["doc_no"].ToString();
                                dncn = doc_no + tran_type;

                            }

                        }
                        DataSet dsdscs = clsDAL.SimpleQuery("select tran_type,doc_no from debitnotehead where bill_no=" + hdnfpurcid.Value + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_code=" + Session["year"].ToString() + "");
                        string dscs = string.Empty;
                        if (dsdscs != null)
                        {
                            if (dsdscs.Tables[0].Rows.Count > 0)
                            {
                                string tran_type = dsdscs.Tables[0].Rows[0]["tran_type"].ToString();
                                string doc_no = dsdscs.Tables[0].Rows[0]["doc_no"].ToString();
                                dscs = doc_no + tran_type;

                            }

                        }
                        lblDNCNNo.Text = dncn + dscs;

                        recordExist = true;
                        lblMsg.Text = "";
                        #region Deliverty order Details
                        qry = "select detail_Id as ID ,ddType as Type,Bank_Code,BankName, Narration,Amount,UTR_NO,LTNo,dodetailid,utrdetailid as UtrDetailId,'' as rowAction,'' as SrNo,bc,UtrYearCode,UtrCompanyCode from " + qryDetail + "  where doid=" + hdnf.Value + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    string idValue = dt.Rows[0]["ID"].ToString();
                                    if (idValue == "")  //blank Row
                                    {
                                        grdDetail.DataSource = null;
                                        grdDetail.DataBind();
                                        ViewState["currentTable"] = null;
                                    }
                                    else
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


                        #region
                        hdnfbilltoStateCode.Value = "0";
                        if (txtBill_To.Text != string.Empty)
                        {
                            hdnfbilltoStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + txtBill_To.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                        }

                        if (hdnfPDSUnitCode.Value != string.Empty && hdnfPDSUnitCode.Value != "0")
                        {
                            hdnfUnitCity.Value = clsCommon.getString("select cityname from qrymstaccountmaster where " +
                                "Ac_Code=" + hdnfPDSUnitCode.Value + " and  Company_Code=" + Session["Company_Code"].ToString() + "");

                        }
                        else
                        {
                            hdnfUnitCity.Value = clsCommon.getString("select cityname from qrymstaccountmaster where " +
                                "Ac_Code=" + txtvoucher_by.Text + " and  Company_Code=" + Session["Company_Code"].ToString() + "");
                        }
                        //DataSet dsgstids = clsDAL.SimpleQuery("select * from collectids where DoDoc_no=" + txtdoc_no.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");

                        string paymentto = clsCommon.getString("select Payment_To from  qrytenderheaddetail" +
                   "  where Tender_No=" + txtPurcNo.Text + " and ID=" + txtPurcOrder.Text +
                   " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //  + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        hdnfpaymentTo.Value = paymentto;
                        hdnfpaymentid.Value = clsCommon.getString("select isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + hdnfpaymentTo.Value + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                        if (grdDetail.Rows.Count > 0)
                        {
                            hdnfpaymenttosb.Value = grdDetail.Rows[0].Cells[4].Text;
                            hdnftenderno.Value = clsCommon.getString("select Tender_DO from nt_1_tender where Mill_Code=" + txtMILL_CODE.Text +
                                " and Tender_No='" + txtPurcNo.Text + "' and Company_code='" + Session["Company_Code"].ToString() + "'");
                            hdnfPaymentStateCode.Value = clsCommon.getString("select  isnull(GSTStateCode,0)  from nt_1_accountmaster where Ac_code=" + hdnfpaymentTo.Value +
                                " and Company_code='" + Session["Company_Code"].ToString() + "'");
                        }
                        hdnfistds.Value = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + paymentto
          + " and Company_Code=" + Session["Company_Code"].ToString() + "");


                        DataSet dsgstids = clsDAL.SimpleQuery("select * from collectids where doid=" + hdnf.Value + "");

                        if (dsgstids != null)
                        {
                            DataTable dtgstid = dsgstids.Tables[0];
                            dtgstid.Columns.Remove("DoDoc_no");
                            dtgstid.Columns.Remove("company_code");
                            dtgstid.Columns.Remove("Year_Code");
                            dtgstid.Columns.Remove("doid");
                            string accode = dtgstid.Rows[0]["paymentTo"].ToString();
                            accode = accode != string.Empty ? accode : "0";
                            hdnfpaymentShort.Value = clsCommon.getString("select Short_Name from qrymstaccountmaster where " +
                                   "Ac_Code=" + accode + " and  Company_Code=" + Session["Company_Code"].ToString() + "");

                            ViewState["GstRateAutoId"] = dtgstid;
                        }
                        #endregion
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            PreQntl();
            if (txtPurcNo.Text == "0")
            {
                lblMsg.Text = "Delete";
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
            }
            if (drpDOType.SelectedValue == "DO")
            {
                btngenratesalebill.Enabled = false;
                btnOurDO.Enabled = false;
                btnDO.Enabled = true;
            }
            else
            {
                btnOurDO.Enabled = true;
                btnDO.Enabled = false;
            }
            if (txteinvoiceno.Text != string.Empty)
            {
                //  lblMsg.Text = "Delete";
                //  btnEdit.Enabled = false;
                //btnDelete.Enabled = false;
                // btnCancel.Enabled = false;
            }
            string tenderPayment_To = string.Empty;
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    tenderPayment_To = grdDetail.Rows[i].Cells[4].Text;
                }
            }
            lblPaymenToLegBal.Text = AcBalance(tenderPayment_To);
            string tendertype = clsCommon.getString("select type from nt_1_tender where Mill_Code=" + txtMILL_CODE.Text +
                              " and Tender_No='" + txtPurcNo.Text + "' and Company_code='" + Session["Company_Code"].ToString() + "'");
            if (tendertype == "P")
            {
                btnOurDO.Enabled = false;
                btnPartyBillDO.Enabled = true;
            }
            else
            {
                btnOurDO.Enabled = true;
                btnPartyBillDO.Enabled = false;
            }

            string str = lblSaleBillToLedgerByBalance.Text;
            if (str.Contains("Debit"))
            {
                string bylimt = clsCommon.getString("select isnull(Limit_By,0) as acid from nt_1_accountmaster where Ac_Code='" + txtSaleBillTo.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                if (bylimt == "Y")
                {
                    btnOurDO.Enabled = false;
                }
                else
                {
                    btnOurDO.Enabled = true;
                }


            }
            else
            {
                btnOurDO.Enabled = true;
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



            //string qryDisplay = "select * from " + qryHead + "  where doid='" + hdnf.Value + "' ";
            string mystring = "";
            mystring = "   SELECT     dbo.nt_1_deliveryorder.tran_type, dbo.nt_1_deliveryorder.doc_no, dbo.nt_1_deliveryorder.desp_type, dbo.nt_1_deliveryorder.doc_date, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date,103) AS doc_dateConverted " +
                   ", dbo.nt_1_deliveryorder.mill_code, dbo.nt_1_deliveryorder.grade, dbo.nt_1_deliveryorder.quantal, dbo.nt_1_deliveryorder.packing, dbo.nt_1_deliveryorder.bags, " +
                    " dbo.nt_1_deliveryorder.mill_rate, dbo.nt_1_deliveryorder.sale_rate, dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.diff_rate, dbo.nt_1_deliveryorder.diff_amount, " +
                    " dbo.nt_1_deliveryorder.amount, dbo.nt_1_deliveryorder.DO, dbo.nt_1_deliveryorder.voucher_by, dbo.nt_1_deliveryorder.broker, dbo.nt_1_deliveryorder.company_code, " +
                    " dbo.nt_1_deliveryorder.Year_Code, dbo.nt_1_deliveryorder.Branch_Code, dbo.nt_1_deliveryorder.purc_no, dbo.nt_1_deliveryorder.purc, dbo.nt_1_deliveryorder.purc_order, " +
                    " dbo.nt_1_deliveryorder.purc_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.transport, dbo.nt_1_deliveryorder.less, dbo.nt_1_deliveryorder.less_amount, " +
                    " dbo.nt_1_deliveryorder.final_amout, dbo.nt_1_deliveryorder.vasuli, dbo.nt_1_deliveryorder.narration1, dbo.nt_1_deliveryorder.narration2, dbo.nt_1_deliveryorder.narration3, " +
                    " dbo.nt_1_deliveryorder.narration4, dbo.nt_1_deliveryorder.narration5, dbo.nt_1_deliveryorder.excise_rate, dbo.nt_1_deliveryorder.memo_no, dbo.nt_1_deliveryorder.freight, " +
                    " dbo.nt_1_deliveryorder.adv_freight1, dbo.nt_1_deliveryorder.driver_no, dbo.nt_1_deliveryorder.driver_Name, dbo.nt_1_deliveryorder.voucher_no, dbo.nt_1_deliveryorder.voucher_type, " +
                    " dbo.nt_1_deliveryorder.GETPASSCODE, dbo.nt_1_deliveryorder.tender_Remark, dbo.nt_1_deliveryorder.vasuli_rate, dbo.nt_1_deliveryorder.vasuli_amount, dbo.nt_1_deliveryorder.to_vasuli, " +
                    " dbo.nt_1_deliveryorder.naka_delivery, dbo.nt_1_deliveryorder.send_sms, dbo.nt_1_deliveryorder.Itag, dbo.nt_1_deliveryorder.Ac_Code, dbo.nt_1_deliveryorder.FreightPerQtl, " +
                    " dbo.nt_1_deliveryorder.Freight_Amount, dbo.nt_1_deliveryorder.Freight_RateMM, dbo.nt_1_deliveryorder.Freight_AmountMM, dbo.nt_1_deliveryorder.Memo_Advance, " +
                    " dbo.nt_1_deliveryorder.Paid_Rate1, dbo.nt_1_deliveryorder.Paid_Amount1, dbo.nt_1_deliveryorder.Paid_Narration1, dbo.nt_1_deliveryorder.Paid_Rate2, dbo.nt_1_deliveryorder.Paid_Amount2, " +
                    " dbo.nt_1_deliveryorder.Paid_Narration2, dbo.nt_1_deliveryorder.Paid_Rate3, dbo.nt_1_deliveryorder.Paid_Amount3, dbo.nt_1_deliveryorder.Paid_Narration3, dbo.nt_1_deliveryorder.MobileNo, " +
                    " dbo.nt_1_deliveryorder.Created_By, dbo.nt_1_deliveryorder.Modified_By, dbo.nt_1_deliveryorder.UTR_No, dbo.nt_1_deliveryorder.UTR_Year_Code, dbo.nt_1_deliveryorder.Carporate_Sale_No, " +
                    " dbo.nt_1_deliveryorder.Carporate_Sale_Year_Code, dbo.nt_1_deliveryorder.Delivery_Type, dbo.nt_1_deliveryorder.WhoseFrieght, dbo.nt_1_deliveryorder.SB_No, " +
                    " dbo.nt_1_deliveryorder.Invoice_No, dbo.nt_1_deliveryorder.vasuli_rate1, dbo.nt_1_deliveryorder.vasuli_amount1, dbo.nt_1_deliveryorder.Party_Commission_Rate, dbo.nt_1_deliveryorder.MM_CC, " +
                    " dbo.nt_1_deliveryorder.MM_Rate, dbo.nt_1_deliveryorder.Voucher_Brokrage, dbo.nt_1_deliveryorder.Voucher_Service_Charge, dbo.nt_1_deliveryorder.Voucher_RateDiffRate, " +
                    " dbo.nt_1_deliveryorder.Voucher_RateDiffAmt, dbo.nt_1_deliveryorder.Voucher_BankCommRate, dbo.nt_1_deliveryorder.Voucher_BankCommAmt, dbo.nt_1_deliveryorder.Voucher_Interest, " +
                    " dbo.nt_1_deliveryorder.Voucher_TransportAmt, dbo.nt_1_deliveryorder.Voucher_OtherExpenses, dbo.nt_1_deliveryorder.CheckPost, dbo.nt_1_deliveryorder.SaleBillTo, " +
                    " dbo.nt_1_deliveryorder.Pan_No, dbo.nt_1_deliveryorder.Vasuli_Ac, dbo.nt_1_deliveryorder.LoadingSms, dbo.nt_1_deliveryorder.GstRateCode, dbo.nt_1_deliveryorder.GetpassGstStateCode, " +
                    " dbo.nt_1_deliveryorder.VoucherbyGstStateCode, dbo.nt_1_deliveryorder.SalebilltoGstStateCode, dbo.nt_1_deliveryorder.GstAmtOnMR, dbo.nt_1_deliveryorder.GstAmtOnSR, " +
                    " dbo.nt_1_deliveryorder.GstExlSR, dbo.nt_1_deliveryorder.GstExlMR, dbo.nt_1_deliveryorder.MillGSTStateCode, dbo.nt_1_deliveryorder.TransportGSTStateCode, " +
                    " dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Distance, dbo.nt_1_deliveryorder.EWayBillChk, dbo.nt_1_deliveryorder.MillInvoiceNo, dbo.nt_1_deliveryorder.Purchase_Date, " +
                    " CONVERT(varchar(10), dbo.nt_1_deliveryorder.Purchase_Date, 103) AS Purchase_DateConverted, dbo.nt_1_deliveryorder.doid, dbo.nt_1_deliveryorder.mc, dbo.nt_1_deliveryorder.gp, " +
                    " dbo.nt_1_deliveryorder.st, dbo.nt_1_deliveryorder.sb, dbo.nt_1_deliveryorder.tc, dbo.nt_1_deliveryorder.itemcode, dbo.nt_1_deliveryorder.cs, dbo.nt_1_deliveryorder.ic, " +
                    " dbo.nt_1_deliveryorder.tenderdetailid, dbo.nt_1_deliveryorder.bk, dbo.nt_1_deliveryorder.docd, qrymstmillcode.Ac_Name_E AS millname, qrymstmillcode.Address_E AS milladress, " +
                    " qrymstmillcode.Gst_No AS millgstno, qrymstmillcode.Email_Id AS millemailid, qrymstmillcode.CompanyPan AS millpanno, qrymstmillcode.cityname AS millcityname, " +
                    " qrymstmillcode.citypincode AS millcitypincode, qrymstmillcode.citystate AS millcitystate, qrymstmillcode.citygststatecode AS millgststatecodemster, qrymstgetpass.Ac_Name_E AS getpassname, " +
                    " qrymstgetpass.Address_E AS getpassaddress, qrymstgetpass.Gst_No AS getpassgstno, qrymstgetpass.Email_Id AS getpassemailid, qrymstgetpass.CompanyPan AS getpasspanno, " +
                    " qrymstgetpass.cityname AS getpasscityname, qrymstgetpass.citypincode AS getpasscitypincode, qrymstgetpass.citystate AS getpasscitystate, " +
                    " qrymstgetpass.citygststatecode AS getpasscitygststatecode, qrymstshipto.Ac_Name_E AS shiptoname, qrymstshipto.Address_E AS shiptoaddress, qrymstshipto.Gst_No AS shiptogstno, " +
                    " qrymstshipto.Email_Id AS shiptoemail, qrymstshipto.CompanyPan AS shiptopanno, qrymstshipto.cityname AS shiptocityname, qrymstshipto.citypincode AS shiptocitypincode, " +
                    " qrymstshipto.citystate AS shiptocitystate, qrymstshipto.citygststatecode AS shiptogststatecode, qrymstsalebill.Ac_Name_E AS salebillname, qrymstsalebill.Address_E AS salebilladdress, " +
                    " qrymstsalebill.Gst_No AS salebillgstno, qrymstsalebill.Email_Id AS salebillemail, qrymstsalebill.CompanyPan AS salebillpanno, qrymstsalebill.cityname AS salebillcityname, " +
                    " qrymstsalebill.citypincode AS salebillcitypincode, qrymstsalebill.citystate AS salebillcitystate, qrymstsalebill.citygststatecode AS salebillcitygststatecode, " +
                    " qrymsttransportcode.Ac_Name_E AS transportname, qrymsttransportcode.Address_E AS transportaddress, qrymsttransportcode.CompanyPan AS transportpanno, " +
                    " qrymstbrokercode.Ac_Name_E AS brokername, qrymstdo.Ac_Name_E AS doname, qrymstbrokercode.Address_E AS doaddress, qrymsttransportcode.Gst_No AS transportgstno, " +
                    " qrymsttransportcode.Email_Id AS transportemail, qrymstdo.Gst_No AS dogstno, qrymstdo.Email_Id AS doemail, qrymstdo.CompanyPan AS dopanno, qrymstdo.cityname AS docityname, " +
                    " qrymstdo.citypincode AS docitypincode, qrymstdo.citystate AS docitystate, qrymstdo.citygststatecode AS docitygststatecode, dbo.qrymstitem.System_Name_E AS itemname, dbo.qrymstitem.HSN, " +
                    " qrymstmillcode.Short_Name AS millshortname, qrygetpassstatemaster.State_Name AS getpassstatename, qryshiptostatemaster.State_Name AS shiptostatename, " +
                    " gstmstmill.State_Name AS gstmillstatename, gstmstsellbill.State_Name AS gststatesellbillname, gstmsttransport.State_Name AS gststatetransportname, dbo.nt_1_gstratemaster.GST_Name, " +
                    " dbo.nt_1_deliveryorder.vb, dbo.nt_1_deliveryorder.va, qrymstvoucherby.Ac_Name_E AS voucherbyname, qrymstvasuliacc.Ac_Name_E AS vasuliacname, qrymstshipto.Mobile_No AS shiptomobno, " +
                    " qrymstshipto.FSSAI AS shiptofssai, qrymstshipto.ECC_No AS shiptoeccno, qrymsttransportcode.Mobile_No AS transportmobno, qrymstgetpass.Mobile_No AS getpassmobno, " +
                    " qrymstgetpass.Cst_no AS getpasscstno, qrymstgetpass.FSSAI AS getpassfssai, qrymstvoucherby.Address_E AS vouvherbyaddress, qrymstvoucherby.cityname AS voucherbycityname, " +
                    " qrymstvoucherby.citystate AS voucherbycitystate, qrymstvoucherby.Cst_no AS voucherbycstno, qrymstvoucherby.Gst_No AS voucherbygstno, qrymstvoucherby.CompanyPan AS voucherbypan, " +
                    " qrymstvoucherby.Mobile_No AS voucherbymobno, qrymstmillcode.Mobile_No AS millmobno, qrymstsalebill.Mobile_No AS salebillmobno, qrymstbrokercode.Mobile_No AS brokermobno, " +
                    " dbo.nt_1_deliveryorder.carporate_ac, dbo.nt_1_deliveryorder.ca, qrycarporateac.Ac_Name_E AS carporateacname, qrycarporateac.Gst_No AS carporateacgstno, " +
                    " qrycarporateac.citygststatecode AS carporateacstatecode, qrymstvoucherby.citygststatecode AS voucherbystatecode, qrymsttransportcode.citygststatecode AS transportstatecode, " +
                    " dbo.nt_1_deliveryorder.mill_inv_date, CONVERT(varchar(10), dbo.nt_1_deliveryorder.mill_inv_date, 103) AS mill_inv_dateConverted, dbo.nt_1_deliveryorder.mill_rcv, " +
                    " qrymstsalebill.Short_Name AS billtoshortname, qrymstshipto.Short_Name AS shiptoshortname, qrymsttransportcode.Short_Name AS transportshortname, qrymstdo.Short_Name AS doshortname, " +
                    " qrymstvoucherby.Short_Name AS voucherbyshortname, qrymstgetpass.Short_Name AS getpassshortname, dbo.nt_1_deliveryorder.MillEwayBill, dbo.nt_1_deliveryorder.TCS_Rate, " +
                    " dbo.nt_1_deliveryorder.Sale_TCS_Rate, dbo.nt_1_deliveryorder.Mill_AmtWO_TCS, dbo.nt_1_deliveryorder.newsbno, CONVERT(varchar(10), dbo.nt_1_deliveryorder.newsbdate, 103) AS newsbdate, " +
                    " dbo.nt_1_deliveryorder.einvoiceno, dbo.nt_1_deliveryorder.ackno, dbo.nt_1_deliveryorder.brandcode, dbo.Brand_Master.Marka, dbo.nt_1_deliveryorder.Cash_diff, " +
                    " dbo.nt_1_deliveryorder.CashDiffAc, dbo.nt_1_deliveryorder.TDSAc, dbo.nt_1_deliveryorder.CashDiffAcId, dbo.nt_1_deliveryorder.TDSAcId, dbo.nt_1_deliveryorder.TDSRate, " +
                    " dbo.nt_1_deliveryorder.TDSAmt, qryTDS.Ac_Name_E AS TDSName, qrycashdiif.Ac_Name_E AS CAshdiffName, dbo.nt_1_deliveryorder.TDSCut, dbo.nt_1_deliveryorder.tenderid, " +
                    " dbo.nt_1_tender.Payment_To, dbo.nt_1_deliveryorder.MemoGSTRate, qrymstshipto.Pincode, dbo.nt_1_deliveryorder.RCMCGSTAmt, dbo.nt_1_deliveryorder.RCMSGSTAmt, " +
                    " dbo.nt_1_deliveryorder.RCMIGSTAmt, dbo.nt_1_deliveryorder.saleid, qrymstgetpass.Pincode AS getpasspin, dbo.nt_1_tender.season, dbo.nt_1_accountmaster.Short_Name AS paymentshortname, " +
                    " dbo.nt_1_deliveryorder.RCMNumber, CONVERT(varchar(10), dbo.nt_1_deliveryorder.EwayBillValidDate, 103) AS EwayBillValidDate, dbo.nt_1_deliveryorder.SaleTDSRate, " +
                    " dbo.nt_1_deliveryorder.PurchaseTDSRate, dbo.nt_1_deliveryorder.PurchaseRate, dbo.nt_1_deliveryorder.SBNarration, ' ' AS WordinAmount, dbo.nt_1_tender.Tender_Date, ' ' AS utrnarration, " +
                    " qrymstdo.Address_E AS DoAdd, qrymstgetpass.Tan_no AS getpasstan_no, qrymstshipto.Tan_no AS shiptotan_no, qrymstdo.FSSAI AS dofssaino, qrycashdiif.cityname AS cashdiifcity, " +
                    " qrycashdiif.Mobile_No AS cashdiifmobno, dbo.nt_1_deliveryorder.MailSend, dbo.nt_1_deliveryorder.ISEInvoice, dbo.nt_1_deliveryorder.IsPayment, CONVERT(varchar(10), " +
                    " dbo.nt_1_deliveryorder.Do_DATE, 103) AS Do_Date_Conv, dbo.nt_1_deliveryorder.Insured, dbo.nt_1_deliveryorder.Do_DATE, dbo.nt_1_deliveryorder.vehicle_reached, CONVERT(varchar(10), " +
                    " dbo.nt_1_deliveryorder.reached_date, 103) AS reached_dateConv, dbo.nt_1_deliveryorder.Insurance, dbo.nt_1_tender.pt, dbo.nt_1_tender.AutoPurchaseBill, dbo.nt_1_deliveryorder.grade1, " +
                    " dbo.nt_1_deliveryorder.quantal1, dbo.nt_1_deliveryorder.packing1, dbo.nt_1_deliveryorder.bags1, dbo.nt_1_deliveryorder.mill_rate1, dbo.nt_1_deliveryorder.sale_rate1, " +
                    " dbo.nt_1_deliveryorder.purc_no1, dbo.nt_1_deliveryorder.purc_order1, dbo.nt_1_deliveryorder.ic1, dbo.nt_1_deliveryorder.itemcode1, dbo.nt_1_deliveryorder.PurchaseRate1, " +
                    " qrymstitem_1.System_Name_E AS itemname1, dbo.nt_1_deliveryorder.mill_amount1, dbo.nt_1_deliveryorder.mill_amountTCS1, dbo.nt_1_deliveryorder.tenderdetailid1, " +
                    " qrymstitem_1.HSN AS HSN1, dbo.nt_1_deliveryorder.SB_Other_Amount" +
                    " FROM         dbo.qrymstitem AS qrymstitem_1 RIGHT OUTER JOIN " +
                    " dbo.nt_1_deliveryorder ON qrymstitem_1.systemid = dbo.nt_1_deliveryorder.ic1 LEFT OUTER JOIN " +
                    " dbo.nt_1_accountmaster RIGHT OUTER JOIN " +
                    " dbo.nt_1_tender ON dbo.nt_1_accountmaster.accoid = dbo.nt_1_tender.pt ON dbo.nt_1_deliveryorder.purc_no = dbo.nt_1_tender.Tender_No AND " +
                    " dbo.nt_1_deliveryorder.company_code = dbo.nt_1_tender.Company_Code LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qryTDS ON dbo.nt_1_deliveryorder.TDSAcId = qryTDS.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrycashdiif ON dbo.nt_1_deliveryorder.CashDiffAcId = qrycashdiif.accoid LEFT OUTER JOIN " +
                    " dbo.Brand_Master ON dbo.nt_1_deliveryorder.company_code = dbo.Brand_Master.Company_Code AND dbo.nt_1_deliveryorder.brandcode = dbo.Brand_Master.Code LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymsttransportcode ON dbo.nt_1_deliveryorder.tc = qrymsttransportcode.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrycarporateac ON dbo.nt_1_deliveryorder.ca = qrycarporateac.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstvasuliacc ON dbo.nt_1_deliveryorder.va = qrymstvasuliacc.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstvoucherby ON dbo.nt_1_deliveryorder.vb = qrymstvoucherby.accoid LEFT OUTER JOIN " +
                    " dbo.nt_1_gstratemaster ON dbo.nt_1_deliveryorder.GstRateCode = dbo.nt_1_gstratemaster.Doc_no AND " +
                    " dbo.nt_1_deliveryorder.company_code = dbo.nt_1_gstratemaster.Company_Code LEFT OUTER JOIN " +
                    " dbo.qrymstitem ON dbo.nt_1_deliveryorder.ic = dbo.qrymstitem.systemid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstdo ON dbo.nt_1_deliveryorder.docd = qrymstdo.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstbrokercode ON qrymstbrokercode.accoid = dbo.nt_1_deliveryorder.bk LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstsalebill ON dbo.nt_1_deliveryorder.sb = qrymstsalebill.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstshipto LEFT OUTER JOIN " +
                    " dbo.gststatemaster AS qryshiptostatemaster ON qryshiptostatemaster.State_Code = qrymstshipto.GSTStateCode ON dbo.nt_1_deliveryorder.st = qrymstshipto.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstgetpass LEFT OUTER JOIN " +
                    " dbo.gststatemaster AS qrygetpassstatemaster ON qrygetpassstatemaster.State_Code = qrymstgetpass.GSTStateCode ON dbo.nt_1_deliveryorder.gp = qrymstgetpass.accoid LEFT OUTER JOIN " +
                    " dbo.qrymstaccountmaster AS qrymstmillcode LEFT OUTER JOIN " +
                    " dbo.gststatemaster AS gstmstmill ON qrymstmillcode.GSTStateCode = gstmstmill.State_Code ON qrymstmillcode.accoid = dbo.nt_1_deliveryorder.mc LEFT OUTER JOIN " +
                    " dbo.gststatemaster AS gstmstsellbill ON qrymstsalebill.GSTStateCode = gstmstsellbill.State_Code LEFT OUTER JOIN " +
                    " dbo.gststatemaster AS gstmsttransport ON qrymsttransportcode.GSTStateCode = gstmsttransport.State_Code " +
                    " where  dbo.nt_1_deliveryorder.doid='" + hdnf.Value + "' ";



            return mystring;
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
                //txtdoc_no.Text = hdnf.Value;
                hdnfSuffix.Value = "";
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }

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
        ////txtBANK_CODE.Text = txtMILL_CODE.Text;
        //txtBANK_CODE.Text = txtSaleBillTo.Text;

        // lblBank_name.Text = LBLMILL_NAME.Text;
        lblBank_name.Text = LBLMILL_NAME.Text;
        //  double qntl = double.Parse(txtquantal.Text);

        // double millrate = double.Parse(txtmillRate.Text);

        //  double Mill_Amount = qntl * millrate;

        double Mill_Amount = double.Parse(lblMillAmount.Text != string.Empty ? lblMillAmount.Text : "0.00");
        double Mill_AmountTCS = double.Parse(lblMillAmtWOTCS.Text != string.Empty ? lblMillAmtWOTCS.Text : "0.00");

        if (grdDetail.Rows.Count > 0)
        {
            double total = 0.00;
            string BANK_CODE = string.Empty;
            string Bank_name = string.Empty;
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[12].Text.ToString() != "D")
                {
                    double amount = Convert.ToDouble(grdDetail.Rows[i].Cells[7].Text);
                    total += amount;

                    BANK_CODE = grdDetail.Rows[i].Cells[4].Text;
                    Bank_name = grdDetail.Rows[i].Cells[5].Text;
                }
            }
            if (Mill_Amount != total)
            {
                //string BankAmt = Convert.ToString(Mill_Amount - total);
                ////string BankAmt = Convert.ToString(Mill_AmountTCS - total);

                string BankAmt = Convert.ToString(Math.Round((Mill_AmountTCS - total), 2));
                hdnfMainBankAmount.Value = BankAmt;
                txtBANK_AMOUNT.Text = BankAmt;
            }

            else
            {
                txtBANK_AMOUNT.Text = Convert.ToString(Mill_Amount);
            }

            txtBANK_CODE.Text = BANK_CODE;
            lblBank_name.Text = Bank_name;
        }


        setFocusControl(drpddType);
        lblUtrBalnceError.Text = "";
        double millamount = txtmillRate.Text != string.Empty ? Convert.ToDouble(txtmillRate.Text) : 0.00;
        double tax = 5.00;
        double result = 0.00;
        tax = tax / 100;
        result = millamount * tax;
        //txtwithGst_Amount.Text = result.ToString();
        ////txtwithGst_Amount.Text = (millamount + result).ToString();


        double qty = Convert.ToDouble(txtquantal.Text);
        txtwithGst_Amount.Text = (Mill_AmountTCS / qty).ToString();
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        hdnfpaymentTo.Value = txtBANK_CODE.Text;
        hdnfpaymentid.Value = clsCommon.getString("select isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + hdnfpaymentTo.Value + " and Company_code='" + Session["Company_Code"].ToString() + "'");
        hdnfpaymenttosb.Value = txtBANK_CODE.Text;
        hdnfPaymentStateCode.Value = clsCommon.getString("select  isnull(GSTStateCode,0)  from nt_1_accountmaster where Ac_code=" + hdnfpaymentTo.Value + " and Company_code='" + Session["Company_Code"].ToString() + "'");

        Int32 UTR_No = txtUTRNo.Text != string.Empty ? Convert.ToInt32(txtUTRNo.Text) : 0;
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
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;

                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select detail_Id from " + tblDetails + " where detail_Id=" + rowIndex + " and " +
                            " doc_no='" + txtdoc_no.Text + "' and Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                            " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        if (id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }

                        if (id == "1" && ViewState["mode"].ToString() == "I")
                        {
                            temp = "1";
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Type", typeof(string))));   //ddType
                    dt.Columns.Add((new DataColumn("Bank_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("BankName", typeof(string))));
                    dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("UTR_NO", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("LTNo", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("dodetailid", typeof(int))));

                    dt1.Columns.Add((new DataColumn("UtrDetailId", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add(new DataColumn("bc", typeof(string)));
                    dt.Columns.Add(new DataColumn("UtrYearCode", typeof(Int32)));
                    dt.Columns.Add(new DataColumn("UtrCompanyCode", typeof(Int32)));

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
                dt.Columns.Add((new DataColumn("Type", typeof(string))));   //ddType
                dt.Columns.Add((new DataColumn("Bank_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("BankName", typeof(string))));
                dt.Columns.Add((new DataColumn("Narration", typeof(string))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("UTR_NO", typeof(Int32))));
                dt.Columns.Add((new DataColumn("LTNo", typeof(Int32))));
                dt.Columns.Add((new DataColumn("dodetailid", typeof(int))));

                dt1.Columns.Add((new DataColumn("UtrDetailId", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add(new DataColumn("bc", typeof(string)));
                dt.Columns.Add(new DataColumn("UtrYearCode", typeof(Int32)));
                dt.Columns.Add(new DataColumn("UtrCompanyCode", typeof(Int32)));

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Type"] = drpddType.SelectedValue;
            dr["Bank_Code"] = txtBANK_CODE.Text;
            dr["Narration"] = txtNARRATION.Text;
            dr["BankName"] = lblBank_name.Text;
            dr["Amount"] = txtBANK_AMOUNT.Text;
            dr["UTR_NO"] = UTR_No;
            dr["LTNo"] = txtLT_No.Text != string.Empty ? txtLT_No.Text : "0";
            dr["bc"] = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + txtBANK_CODE.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");

            dr["UtrYearCode"] = lblUTRYearCode.Text != string.Empty ? lblUTRYearCode.Text : "0";
            dr["UtrCompanyCode"] = lblUTRCompnyCode.Text != string.Empty ? lblUTRCompnyCode.Text : "0";

            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["UtrDetailId"] = Convert.ToInt32(hdnfUtrdetail.Value);
                dr["dodetailid"] = "1";
                dt.Rows.Add(dr);
            }
            else
            {
                dr["UtrDetailId"] = Convert.ToInt32(hdnfUtrdetail.Value.Trim());
            }
            //else
            //{
            //    hdnfUtrdetail.Value = hdnfUtrdetail.Value.Trim();
            //    if (hdnfUtrdetail.Value == string.Empty)
            //    {
            //        dr["UtrDetailId"] = 0;
            //    }
            //    else
            //    {
            //        dr["UtrDetailId"] = Convert.ToInt32(hdnfUtrdetail.Value);
            //    }


            //}

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
                setFocusControl(txtBANK_CODE);
            }
            else
            {
                btnAdddetails.Text = "ADD";
                pnlPopupDetails.Style["display"] = "block";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            //txtBANK_CODE.Text = string.Empty;
            txtNARRATION.Text = string.Empty;
            //lblBank_name.Text = string.Empty;
            txtBANK_AMOUNT.Text = string.Empty;
            txtUTRNo.Text = string.Empty;
            txtLT_No.Text = string.Empty;
            lblUTRCompnyCode.Text = string.Empty;
            lblUTRYearCode.Text = string.Empty;
            //double qntl = double.Parse(txtquantal.Text);
            //double millrate = double.Parse(txtmillRate.Text);
            //double Mill_Amount = qntl * millrate;
            ////double Mill_Amount = double.Parse(lblMillAmount.Text);
            double Mill_Amount = double.Parse(lblMillAmtWOTCS.Text);

            if (grdDetail.Rows.Count > 0)
            {
                double total = 0.00;
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[12].Text.ToString() != "D")
                    {
                        double amount = Convert.ToDouble(grdDetail.Rows[i].Cells[7].Text);
                        total += amount;
                    }
                }
                if (Mill_Amount != total)
                {
                    string BankAmt = Convert.ToString(Mill_Amount - total);
                    hdnfMainBankAmount.Value = BankAmt;
                    txtBANK_AMOUNT.Text = BankAmt;
                }
                else
                {
                    txtBANK_AMOUNT.Text = Convert.ToString(Mill_Amount);
                    pnlPopupDetails.Style["display"] = "none";

                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        pnlPopupDetails.Style["display"] = "none";
        setFocusControl(btnSave);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gridViewRow)
    {

        drpddType.SelectedValue = Server.HtmlDecode(gridViewRow.Cells[3].Text);
        txtBANK_CODE.Text = Server.HtmlDecode(gridViewRow.Cells[4].Text);
        lblBank_name.Text = Server.HtmlDecode(gridViewRow.Cells[5].Text);
        txtNARRATION.Text = Server.HtmlDecode(gridViewRow.Cells[6].Text);
        string MainBankAmount = Server.HtmlDecode(gridViewRow.Cells[7].Text);
        txtBANK_AMOUNT.Text = MainBankAmount;
        hdnfMainBankAmount.Value = MainBankAmount;
        // hdnfUtrdetail.Value = Server.HtmlDecode(gridViewRow.Cells[11].Text.Trim());
        hdnfUtrdetail.Value = Server.HtmlDecode(gridViewRow.Cells[10].Text.Trim());
        hdnfUtrdetail.Value = hdnfUtrdetail.Value.Trim();
        hdnfUtrdetail.Value = hdnfUtrdetail.Value != string.Empty ? hdnfUtrdetail.Value : "0";
        txtUTRNo.Text = Server.HtmlDecode(gridViewRow.Cells[8].Text);
        txtLT_No.Text = Server.HtmlDecode(gridViewRow.Cells[9].Text);
        lblNo.Text = Server.HtmlDecode(gridViewRow.Cells[13].Text);
        lblID.Text = Server.HtmlDecode(gridViewRow.Cells[2].Text);

        lblUTRYearCode.Text = Server.HtmlDecode(gridViewRow.Cells[15].Text);
        lblUTRCompnyCode.Text = Server.HtmlDecode(gridViewRow.Cells[16].Text);

        setFocusControl(drpddType);
        lblUtrBalnceError.Text = "";

        double millamount = txtmillRate.Text != string.Empty ? Convert.ToDouble(txtmillRate.Text) : 0.00;
        double tax = 5.00;
        double result = 0.00;
        tax = tax / 100;
        result = millamount * tax;
        //txtwithGst_Amount.Text = result.ToString();
        //txtwithGst_Amount.Text = (millamount + result).ToString();
        double MillAmtWOTCS = Convert.ToDouble(lblMillAmtWOTCS.Text);
        double qty = Convert.ToDouble(txtquantal.Text);
        txtwithGst_Amount.Text = (MillAmtWOTCS / qty).ToString();
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["dodetailid"].ToString());
                string IDExisting = clsCommon.getString("select detail_Id from " + tblDetails + " where dodetailid=" + ID + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[12].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[12].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[12].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[12].Text = "A";
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

    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();
    }
    #endregion

    #region [btntxtdoc_no_Click]
    protected void btntxtdoc_no_Click(object sender, EventArgs e)
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

    #region [txtDOC_DATE_TextChanged]
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_DATE.Text;
        strTextBox = "txtDOC_DATE";
        csCalculations();
    }
    #endregion

    #region [drpDOType_SelectedIndexChanged]
    protected void drpDOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //searchString = drpDOType.SelectedValue;
        strTextBox = "drpDOType";
        if (drpDOType.SelectedValue == "DO")
        {
            txtGETPASS_CODE.Text = "";
            LBLGETPASS_NAME.Text = "";
            txtGETPASS_CODE.Enabled = true;

        }
        else
        {
            txtGETPASS_CODE.Enabled = false;
        }
        string comm = clsCommon.getString("Select Commission from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        txtCommission.Text = comm.ToString();
        csCalculations();
        if (drpDOType.SelectedValue == "DO")
        {
            grdDetail.DataSource = null;
            grdDetail.DataBind();

        }

    }
    #endregion

    #region [txtMILL_CODE_TextChanged]
    protected void txtMILL_CODE_TextChanged(object sender, EventArgs e)
    {

        searchString = txtMILL_CODE.Text;
        strTextBox = "txtMILL_CODE";
        csCalculations();
        if (txtPurcNo.Text != string.Empty && txtPurcOrder.Text != string.Empty)
        {
            if (ViewState["mode"].ToString() == "I")
            {
                calculation();
            }
        }
    }
    #endregion

    #region [btntxtMILL_CODE_Click]
    protected void btntxtMILL_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMILL_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGstRate_TextChanged]
    protected void txtGstRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGstRate.Text;
        strTextBox = "txtGstRate";
        csCalculations();
    }
    #endregion

    #region [btntxtGstRate_Click]
    protected void btntxtGstRate_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGstRate";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGETPASS_CODE_TextChanged]
    protected void txtGETPASS_CODE_TextChanged(object sender, EventArgs e)
    {

        searchString = txtGETPASS_CODE.Text;
        strTextBox = "txtGETPASS_CODE";
        string selfac = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
        if (txtGETPASS_CODE.Text == selfac)
        {
            txtNARRATION4.Enabled = false;
            txtSaleBillTo.Enabled = true;
        }
        else
        {
            //txtNARRATION4.Enabled = true;
            //txtSaleBillTo.Enabled = true;
            //txtSaleBillTo.Text = "";
            //txtNARRATION4.Text = "";
            //txtSalebilltoGstStateCode.Text = "";
            // lbltxtSalebilltoGstStateName.Text = "";
        }
        csCalculations();
        setFocusControl(txtitem_Code);
    }
    #endregion

    #region [btntxtGETPASS_CODE_Click]
    protected void btntxtGETPASS_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGETPASS_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtvoucher_by_TextChanged]
    protected void txtvoucher_by_TextChanged(object sender, EventArgs e)
    {

        searchString = txtvoucher_by.Text;
        strTextBox = "txtvoucher_by";
        csCalculations();
        calculation();
        GSTCalculations();
    }
    #endregion

    #region [btntxtvoucher_by_Click]
    protected void btntxtvoucher_by_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtvoucher_by";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtGRADE_TextChanged]
    protected void txtGRADE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGRADE.Text;
        if (txtGRADE.Text != string.Empty)
        {
            bool a = true;
            if (txtGRADE.Text.Length < 8)
            {
                a = clsCommon.isStringIsNumeric(txtGRADE.Text);
            }
            if (a == false)
            {
                btntxtGRADE_Click(this, new EventArgs());
            }
            else
            {
                pnlPopup.Style["display"] = "none";
                setFocusControl(txtquantal);
            }
        }
    }
    #endregion

    #region [btntxtGRADE_Click]
    protected void btntxtGRADE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGRADE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtquantal_TextChanged]
    protected void txtquantal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtquantal.Text != string.Empty && txtPACKING.Text != string.Empty && txtquantal.Text != "0" && txtPACKING.Text != "0")
            {
                int bags = Convert.ToInt32(Math.Round(float.Parse(txtquantal.Text) * (100 / float.Parse(txtPACKING.Text))));
                txtBAGS.Text = bags.ToString();
                if (hdnfpacking.Value != "1")
                {
                    //  setFocusControl(txtPACKING);
                    setFocusControl(txtSALE_RATE);
                }
                else
                {
                    setFocusControl(txtvoucher_by);
                    hdnfpacking.Value = "2";
                }
            }
            else if ((txtPACKING.Text == string.Empty || txtPACKING.Text == "0") && txtquantal.Text != string.Empty && txtquantal.Text != "0")
            {
                txtPACKING.Text = "50";
                int bags = Convert.ToInt32(Math.Round(float.Parse(txtquantal.Text) * (100 / float.Parse(txtPACKING.Text))));
                txtBAGS.Text = bags.ToString();
                setFocusControl(txtPACKING);
            }
            else
            {
                txtquantal.Text = string.Empty;
                setFocusControl(txtquantal);
                txtBAGS.Text = "0";
            }
            // searchString = txtquantal.Text;
            strTextBox = "txtquantal";
            calculation();
            MemoadvanceCalculation();
            //setFocusControl(txtmillRate);

            //DataTable dt1 = (DataTable)ViewState["GstRateAutoId"];
            //dt1.Rows[0]["TenderQty"] = Convert.ToDouble(Convert.ToDouble(hdnfTenderQty.Value != string.Empty ? hdnfTenderQty.Value : "0") - Convert.ToDouble(txtquantal.Text)).ToString();
            //grdGstAutoId.DataSource = dt1;
            //grdGstAutoId.DataBind();
            //ViewState["GstRateAutoId"] = dt1;

            hdnfTenderQty.Value = Convert.ToDouble(Convert.ToDouble(hdnfQTY.Value != string.Empty ? hdnfQTY.Value : "0") - Convert.ToDouble(txtquantal.Text)).ToString();
        }
        catch { }
    }
    #endregion

    #region [txtPACKING_TextChanged]
    protected void txtPACKING_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtPACKING.Text;
        strTextBox = "txtPACKING";
        csCalculations();
    }
    #endregion

    #region [txtBAGS_TextChanged]
    protected void txtBAGS_TextChanged(object sender, EventArgs e)
    {
        //  searchString = txtBAGS.Text;
        strTextBox = "txtBAGS";
        csCalculations();
    }
    #endregion

    #region [txtexcise_rate_TextChanged]
    protected void txtexcise_rate_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtexcise_rate.Text;
        strTextBox = "txtexcise_rate";
        csCalculations();
    }
    #endregion

    #region [txtSALE_RATE_TextChanged]
    protected void txtSALE_RATE_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtSALE_RATE.Text;
        strTextBox = "txtSALE_RATE";
        csCalculations();
        calculation();
        GSTCalculations();
    }
    #endregion

    #region [txtSB_Other_Amount_TextChanged]
    protected void txtSB_Other_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSALE_RATE.Text;
        strTextBox = "txtSB_Other_Amount";
    }
    #endregion

    #region [txtDIFF_AMOUNT_TextChanged]
    protected void txtDIFF_AMOUNT_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtDIFF_AMOUNT.Text;
        strTextBox = "txtDIFF_AMOUNT";
        csCalculations();
    }
    #endregion

    #region [txtDO_CODE_TextChanged]
    protected void txtDO_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDO_CODE.Text;
        strTextBox = "txtDO_CODE";
        csCalculations();
    }
    #endregion

    #region [txtPurchase_Date_TextChanged]
    protected void txtPurchase_Date_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtPurchase_Date.Text;
        strTextBox = "txtPurchase_Date";
        csCalculations();
    }
    #endregion

    #region [btntxtDO_CODE_Click]
    protected void btntxtDO_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDO_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBroker_CODE_TextChanged]
    protected void txtBroker_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBroker_CODE.Text;
        strTextBox = "txtBroker_CODE";
        csCalculations();
        hdnfbrokershortName.Value = clsCommon.getString("select Short_Name from " + qryAccountList + " where Ac_code=" + txtBroker_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");

    }
    #endregion

    #region [btntxtBroker_CODE_Click]
    protected void btntxtBroker_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    //#region [txtTruck_NO_TextChanged]
    //protected void txtTruck_NO_TextChanged(object sender, EventArgs e)
    //{
    //    //searchString = txtTruck_NO.Text;
    //    strTextBox = "txtTruck_NO";
    //    csCalculations();
    //}
    //#endregion

    #region [txtTRANSPORT_CODE_TextChanged]
    protected void txtTRANSPORT_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTRANSPORT_CODE.Text;
        strTextBox = "txtTRANSPORT_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtTRANSPORT_CODE_Click]
    protected void btntxtTRANSPORT_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTRANSPORT_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNARRATION1_TextChanged]
    protected void txtNARRATION1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNARRATION1.Text;
        strTextBox = "txtNARRATION1";
        csCalculations();
    }
    #endregion

    #region [btntxtNARRATION1_Click]
    protected void btntxtNARRATION1_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNARRATION1";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNARRATION2_TextChanged]
    protected void txtNARRATION2_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNARRATION2.Text;
        strTextBox = "txtNARRATION2";
        csCalculations();
    }
    #endregion

    #region [btntxtNARRATION2_Click]
    protected void btntxtNARRATION2_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNARRATION2";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNARRATION3_TextChanged]
    protected void txtNARRATION3_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNARRATION3.Text;
        strTextBox = "txtNARRATION3";
        csCalculations();
    }
    #endregion

    #region [btntxtNARRATION3_Click]
    protected void btntxtNARRATION3_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNARRATION3";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNARRATION4_TextChanged]
    protected void txtNARRATION4_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNARRATION4.Text;
        strTextBox = "txtNARRATION4";
        csCalculations();
    }
    #endregion

    #region [btntxtNARRATION4_Click]
    protected void btntxtNARRATION4_Click(object sender, EventArgs e)
    {
        try
        {

            searchString = txtSaleBillTo.Text;
            strTextBox = "txtSaleBillTo";
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNARRATION4";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion


    #region [txtBANK_CODE_TextChanged]
    protected void txtBANK_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBANK_CODE.Text;
        strTextBox = "txtBANK_CODE";
        csCalculations();
        lblPaymenToLegBal.Text = AcBalance(txtBANK_CODE.Text);
    }
    #endregion

    #region [btntxtBANK_CODE_Click]
    protected void btntxtBANK_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBANK_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNARRATION_TextChanged]
    protected void txtNARRATION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNARRATION.Text;
        strTextBox = "txtNARRATION";
        setFocusControl(txtBANK_AMOUNT);
        //csCalculations();
    }
    #endregion

    #region [btntxtNARRATION_Click]
    protected void btntxtNARRATION_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNARRATION";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBANK_AMOUNT_TextChanged]
    protected void txtBANK_AMOUNT_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtBANK_AMOUNT.Text;
        //strTextBox = "txtBANK_AMOUNT";
        setFocusControl(btnAdddetails);
        //csCalculations();
    }
    #endregion
    #region [txtLT_No_TextChanged]
    protected void txtLT_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtBANK_AMOUNT.Text;
        //strTextBox = "txtBANK_AMOUNT";
        setFocusControl(btnAdddetails);
        //csCalculations();
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

            if (drpDOType.SelectedValue == "DO")
            {
                if (txtGETPASS_CODE.Text == txtSaleBillTo.Text)
                {

                }
                else
                {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Please check SaleBillTo!')", true);

                return;

                }
            }

            DateTime dEnd = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

            string locktds = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
             + " and Company_Code=" + Session["Company_Code"].ToString() + "");

            //string selfbalance = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
            //+ " and Company_Code=" + Session["Company_Code"].ToString() + "");
            double selfbalance = Convert.ToDouble(clsCommon.getString("select Quantal from nt_1_tender where Mill_Code=" + txtMILL_CODE.Text +
                               " and Tender_No='" + txtPurcNo.Text + "' and Company_code='" + Session["Company_Code"].ToString() + "'"));

            double Dobalance = Convert.ToDouble(clsCommon.getString("select sum(quantal) from nt_1_deliveryorder where Mill_Code=" + txtMILL_CODE.Text +
                               " and purc_no='" + txtPurcNo.Text + "' and Company_code='" + Session["Company_Code"].ToString() + "' and Year_Code=" + Session["year"].ToString()));
            
            double doqnlt =Dobalance+ Convert.ToDouble(txtquantal.Text);

           // double 
          //  double chkqntl = Math.Abs(selfbalance - doqnlt);
            if (btnSave.Text == "Save")
            {
                if (doqnlt > selfbalance)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('please check tender balance!')", true);
                    return;
                    // Response.Redirect("~/Sugar/pgeHome.aspx", false);
                }
            }

            if (drpDOType.SelectedValue != "DO")
            {
                if (locktds == "L" && dEnd >= ss)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Party lock!')", true);
                    txtSaleBillTo_TextChanged(this, new EventArgs());
                    txtSaleBillTo.Text = string.Empty;
                    setFocusControl(txtSaleBillTo);
                    return;
                }
                else
                {
                    string SBToFSSAI = clsCommon.getString("select FSSAI from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
                    + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                    string ShipToFSSAI = clsCommon.getString("select FSSAI from qrymstaccountmaster where Ac_Code=" + txtvoucher_by.Text
                     + " and Company_Code=" + Session["Company_Code"].ToString() + "");

                    if (SBToFSSAI == string.Empty || SBToFSSAI == "0" || ShipToFSSAI == string.Empty || ShipToFSSAI == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Please Ckeck FSSAI No of SaleBillTo & ShipTo!')", true);

                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:pagevalidation()", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:pagevalidation()", true);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showsms", "javascript:showsmspopup();", true);
            }
        }
        catch (Exception exxx)
        {
            DOPurcSaleCRUD.LogError(exxx);
            btnSave.Enabled = true;
            setFocusControl(btnSave);
            con.Close();

        }
        finally
        {
            con.Close();
        }

    }

    #endregion

    public string AcBalance(string Ac_Code)
    {
        try
        {
            string todayDate = DateTime.Now.ToString("dd/MM/yyyy");
            string DOC_DTAE = DateTime.Parse(todayDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            qry = qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                            " from nt_1_gledger where AC_CODE='" + Ac_Code + "' and DOC_DATE<='" + DOC_DTAE + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  group by AC_CODE having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0";
            string Balance = "0";
            DataTable dtT = new DataTable();
            dtT.Columns.Add("accode", typeof(Int32));
            dtT.Columns.Add("acname", typeof(string));
            dtT.Columns.Add("city", typeof(string));
            dtT.Columns.Add("debitAmt", typeof(double));
            dtT.Columns.Add("creditAmt", typeof(double));
            dtT.Columns.Add("mobile", typeof(string));
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataView dv;
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dv = new DataView();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dtT.NewRow();
                            //dr["accode"] = dt.Rows[i]["AC_CODE"].ToString();
                            //dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                            //dr["city"] = dt.Rows[i]["CityName"].ToString();
                            //dr["mobile"] = dt.Rows[i]["Mobile_No"].ToString();
                            double bal = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString());
                            //if (DrCr == "Dr")
                            //{
                            //    if (bal > 0)
                            //    {
                            //        dr["debitAmt"] = bal.ToString();
                            //        dr["creditAmt"] = 0.00;
                            //        dtT.Rows.Add(dr);
                            //    }
                            //}
                            //else if (DrCr == "Cr")
                            //{
                            //    if (bal < 0)
                            //    {
                            //        dr["debitAmt"] = 0.00;
                            //        dr["creditAmt"] = Math.Abs(bal);
                            //        dtT.Rows.Add(dr);
                            //    }
                            //}
                            //else
                            //{
                            if (bal > 0)
                            {
                                // groupdebitamt += bal;
                                dr["debitAmt"] = bal.ToString();
                                dr["creditAmt"] = 0.00;
                                Balance = Math.Abs(bal).ToString() + " Debit";
                            }
                            else
                            {
                                //  groupcreditamt += -bal;
                                dr["debitAmt"] = 0.00;
                                dr["creditAmt"] = Math.Abs(bal);
                                Balance = Math.Abs(bal).ToString() + " Credit";
                            }
                            dtT.Rows.Add(dr);
                            //}
                        }
                    }
                }
            }
            return Balance;
        }
        catch (Exception)
        {
            return "0";
        }
    }

    private void MaxVoucher()
    {
        //string selfac = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
        if (AUTO_VOUCHER == "YES")
        {
            if (drpDOType.SelectedValue == "DO")
            {
                int voucherno = Convert.ToInt32(clsCommon.getString("Select COALESCE(isnull(MAX(Doc_No),0))+1 from commission_bill Where Tran_Type='LV' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                hdnvouchernumber.Value = voucherno.ToString();
                lblVoucherType.Text = "LV";
            }
            if (drpDOType.SelectedValue == "DI")
            {
                if (txtGETPASS_CODE.Text == Session["SELF_AC"].ToString())
                {
                    int voucherno = Convert.ToInt32(clsCommon.getString("Select COALESCE(MAX(doc_no),0)+1 from " + tblPrefix + "SugarPurchase where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                    hdnvouchernumber.Value = voucherno.ToString();
                    lblVoucherType.Text = "PS";
                }
                //else
                //{
                //    int voucherno = Convert.ToInt32(clsCommon.getString("Select COALESCE(MAX(Doc_No),0)+1 from " + tblPrefix + "Voucher Where Tran_Type='LV' and Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                //    hdnvouchernumber.Value = voucherno.ToString();
                //    lblVoucherType.Text = "LV";

                //    //int voucherno = Convert.ToInt32(clsCommon.getString("Select COALESCE(MAX(Doc_No),0)+1 from " + tblPrefix + "Voucher Where Tran_Type='OV' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                //    //hdnvouchernumber.Value = voucherno.ToString();
                //    //lblVoucherType.Text = "OV";
                //}
            }
        }
        else
        {
            hdnvouchernumber.Value = string.Empty;
        }
    }

    protected void txtmillRate_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtmillRate.Text;
        strTextBox = "txtmillRate";
        csCalculations();
        calculation();
        GSTCalculations();
        setFocusControl(txtSALE_RATE);
    }

    protected void txtPurcNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtPurcNo.Text;
            strTextBox = "txtPurcNo";
            txtPurcOrder.Enabled = false;
            txtPurcNo.Enabled = false;
            if (strTextBox == "txtPurcNo")
            {
                //txtPurcOrder.Text = "1";
                setFocusControl(txtGstRate);
                int i = 0;
                i++;
                if (txtcarporateSale.Text == string.Empty || txtcarporateSale.Text == "0")
                {
                    hdnfpacking.Value = Convert.ToString(i);
                }
                string a = txtPurcOrder.Text;
                if (txtPurcNo.Text != string.Empty && txtPurcOrder.Text != string.Empty)
                {
                    //string qry = "select Buyer,buyername,Buyer_Party,buyerpartyname,Voucher_By,voucherbyname,Grade,Quantal,Packing,Bags," +
                    //    " Excise_Rate,Mill_Rate,Sale_Rate,Tender_DO,tenderdoname,Broker,brokername,Commission_Rate as CR,Delivery_Type as DT,Payment_To,paymenttoname, " +
                    //   " gstratecode,gstratename,itemcode,itemname,tenderdetailid,ShipToname,shiptoid,ShipTo,season,Party_Bill_Rate,AutoPurchaseBill from  qrytenderheaddetail" +
                    //    "  where Tender_No=" + txtPurcNo.Text + " and ID=" + txtPurcOrder.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    //    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());



                    string qry = "select Buyer,buyername,Buyer_Party,buyerpartyname,Voucher_By,voucherbyname,Grade,Quantal,Packing,Bags," +
                       " Excise_Rate,Mill_Rate,Sale_Rate,Tender_DO,tenderdoname,Broker,brokername,Commission_Rate as CR,Delivery_Type as DT,Payment_To,paymenttoname, " +
                      " gstratecode,gstratename,itemcode,itemname,tenderdetailid,ShipToname,shiptoid,ShipTo,season,Party_Bill_Rate,AutoPurchaseBill from  qrytenderheaddetail" +
                       "  where Tender_No=" + txtPurcNo.Text + " and ID=" + txtPurcOrder.Text +
                       " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                    // + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());




                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds = clsDAL.SimpleQuery(qry);
                    string PP = "";
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string AutopurchaseBill = dt.Rows[0]["AutoPurchaseBill"].ToString();
                                hdnfAutoPurchaseBill.Value = AutopurchaseBill;

                                if (txtcarporateSale.Text == string.Empty || txtcarporateSale.Text == "0")
                                {
                                    PP = dt.Rows[0]["Payment_To"].ToString();
                                    string Buyer = dt.Rows[0]["Buyer"].ToString();
                                    string broker = dt.Rows[0]["Buyer_Party"].ToString();

                                    if (drpDOType.SelectedValue == "DO")
                                    {
                                        txtGETPASS_CODE.Text = dt.Rows[0]["Buyer"].ToString();
                                        LBLGETPASS_NAME.Text = dt.Rows[0]["buyername"].ToString();
                                    }

                                    txtGstRate.Text = dt.Rows[0]["gstratecode"].ToString();
                                    if (txtGstRate.Text == "0")
                                    {
                                        txtGstRate.Text = "1";
                                        lblGstRateName.Text = clsCommon.getString("Select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1");
                                    }
                                    else
                                    {
                                        lblGstRateName.Text = dt.Rows[0]["gstratename"].ToString();

                                    }
                                    txtitem_Code.Text = dt.Rows[0]["itemcode"].ToString();
                                    lblitem_Name.Text = dt.Rows[0]["itemname"].ToString();

                                    string shipto = dt.Rows[0]["ShipTo"].ToString();
                                    if (shipto != string.Empty && shipto != "0")
                                    {
                                        txtvoucher_by.Text = shipto;
                                        lblvoucherbyname.Text = dt.Rows[0]["ShipToname"].ToString();
                                    }
                                    else
                                    {
                                        txtvoucher_by.Text = dt.Rows[0]["Buyer"].ToString();
                                        lblvoucherbyname.Text = dt.Rows[0]["buyername"].ToString();
                                    }
                                    txtBroker_CODE.Text = broker;
                                    LBLBROKER_NAME.Text = dt.Rows[0]["buyerpartyname"].ToString();
                                    hdnfic.Value = clsCommon.getString("select systemid from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                                    //if (Buyer != broker)
                                    //{


                                    if (drpDOType.SelectedValue == "DI")
                                    {
                                        txtGETPASS_CODE.Text = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code='" + Session["year"].ToString() + "'");

                                        string selfac_name = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtGETPASS_CODE.Text
                                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                        string selftacc_city = clsCommon.getString("select CityName from " + qryAccountList + "  where Ac_Code=" + txtGETPASS_CODE.Text
                                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                        LBLGETPASS_NAME.Text = selfac_name + "," + selftacc_city;

                                        if (txtvoucher_by.Text == txtGETPASS_CODE.Text)
                                        {
                                            txtNARRATION4.Text = "";
                                            txtSaleBillTo.Text = "";
                                            txtSalebilltoGstStateCode.Text = "";
                                            lbltxtSalebilltoGstStateName.Text = "";
                                        }
                                        if (txtvoucher_by.Text != txtGETPASS_CODE.Text)
                                        {
                                            txtNARRATION4.Text = dt.Rows[0]["buyername"].ToString(); ;
                                            txtSaleBillTo.Text = dt.Rows[0]["Buyer"].ToString();

                                            string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            txtSalebilltoGstStateCode.Text = gststatecode;
                                            string salebilltostateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecode + "");
                                            lbltxtSalebilltoGstStateName.Text = salebilltostateName;


                                        }

                                    }
                                    else
                                    {
                                        //txtGETPASS_CODE.Text = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code='" + Session["year"].ToString() + "'");

                                        txtGETPASS_CODE.Text = dt.Rows[0]["Buyer"].ToString();
                                        string selfac_name = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtGETPASS_CODE.Text
                                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                        string selftacc_city = clsCommon.getString("select CityName from " + qryAccountList + "  where Ac_Code=" + txtGETPASS_CODE.Text
                                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                        LBLGETPASS_NAME.Text = selfac_name + "," + selftacc_city;
                                        txtSaleBillTo.Text = dt.Rows[0]["Buyer"].ToString();
                                        txtNARRATION4.Text = selfac_name + "," + selftacc_city;
                                        if (txtvoucher_by.Text != txtGETPASS_CODE.Text)
                                        {
                                            txtNARRATION4.Text = dt.Rows[0]["buyername"].ToString(); ;
                                            txtSaleBillTo.Text = dt.Rows[0]["Buyer"].ToString();

                                            string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            txtSalebilltoGstStateCode.Text = gststatecode;
                                            string salebilltostateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecode + "");
                                            lbltxtSalebilltoGstStateName.Text = salebilltostateName;


                                        }


                                    }

                                }
                                txtCashDiffAc.Text = txtSaleBillTo.Text;
                                lblCashDiffAcname.Text = txtNARRATION4.Text;
                                hdnfTenderDetailid.Value = dt.Rows[0]["tenderdetailid"].ToString();
                                txtGRADE.Text = dt.Rows[0]["Grade"].ToString();
                                txtseasons.Text = dt.Rows[0]["season"].ToString();
                                txtPACKING.Text = dt.Rows[0]["Packing"].ToString();
                                txtBAGS.Text = dt.Rows[0]["Bags"].ToString();
                                txtexcise_rate.Text = dt.Rows[0]["Excise_Rate"].ToString();
                                txtmillRate.Text = dt.Rows[0]["Mill_Rate"].ToString();
                                txtpurchaserate.Text = dt.Rows[0]["Party_Bill_Rate"].ToString();

                                double Comm_rate = Convert.ToDouble(dt.Rows[0]["CR"].ToString());
                                //txtPartyCommission.Text = Convert.ToString(Comm_rate);
                                double SR = Convert.ToDouble(dt.Rows[0]["Sale_Rate"].ToString());
                                hdnfSaleRate.Value = Convert.ToString(SR);
                                string DT = dt.Rows[0]["DT"].ToString();
                                if (txtcarporateSale.Text == string.Empty || txtcarporateSale.Text == "0")
                                {
                                    drpDeliveryType.SelectedValue = "N";
                                }
                                if (txtcarporateSale.Text == string.Empty || txtcarporateSale.Text == "0")
                                {

                                   
                                    txtCommission.Text = Comm_rate.ToString();

                                    string comm = clsCommon.getString("Select Commission from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    // txtSALE_RATE.Text = "0";
                                    //txtCommission.Text = comm.ToString();
                                    if (txteinvoiceno.Text == string.Empty && txtackno.Text == string.Empty || txtEWayBill_No.Text == string.Empty)
                                    {
                                        txtSALE_RATE.Text = (SR).ToString();
                                    }
                                }
                                PP = dt.Rows[0]["Payment_To"].ToString();
                                txtDO_CODE.Text = dt.Rows[0]["Tender_DO"].ToString();
                                LBLDO_NAME.Text = dt.Rows[0]["tenderdoname"].ToString();
                                txtPurcNo.Enabled = false;
                                string distance = clsCommon.getString("Select Distance from " + qryAccountList + " where Ac_Code=" + txtvoucher_by.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                txtDistance.Text = distance;

                                trnType = clsCommon.getString("select Delivery_Type from nt_1_tenderdetails where Tender_No=" + txtPurcNo.Text + " " +
                                    " and ID=" + txtPurcOrder.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                                if (trnType != "C")
                                {
                                    txtCommission.Enabled = false;
                                }

                                if (trnType != "0")
                                {
                                    if (txtPurcOrder.Text != "1")
                                    {
                                        drpDeliveryType.SelectedValue = trnType;
                                    }
                                    else
                                    {

                                        drpDeliveryType.SelectedValue = Session["Despatch_Type"].ToString();
                                    }
                                    if (trnType == "D")
                                    {
                                        drpDOType.SelectedValue = "DO";

                                    }
                                    else
                                    {
                                        drpDOType.SelectedValue = "DI";
                                    }
                                }

                                else
                                {

                                    drpDeliveryType.SelectedValue = Session["Despatch_Type"].ToString();
                                }
                            }

                            #region Assign
                            if (txtMILL_CODE.Text != string.Empty)
                            {
                                hdnfmc.Value = clsCommon.getString("select isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtMILL_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                                // tenderdetailid = Convert.ToInt32(clsCommon.getString("select tenderdetailid from qrytenderdobalanceview where Mill_Code=" + MILL_CODE + " and Company_code='" + Company_Code + "' and Year_Code='" + Year_Code + "'"));
                            }


                            if (txtGETPASS_CODE.Text != string.Empty)
                            {
                                hdnfgp.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtGETPASS_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }

                            if (txtvoucher_by.Text != string.Empty)
                            {
                                hdnfst.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtvoucher_by.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }

                            if (txtSaleBillTo.Text != string.Empty)
                            {
                                try
                                {
                                    hdnfsb.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtSaleBillTo.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                                }
                                catch { }
                            }

                            if (txtTRANSPORT_CODE.Text != string.Empty)
                            {
                                hdnftc.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtTRANSPORT_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }
                            if (txtTDSAc.Text != string.Empty)
                            {
                                hdnfTDSAcid.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtTDSAc.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }
                            if (txtCashDiffAc.Text != string.Empty)
                            {
                                hdnfCashDiffAcid.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtCashDiffAc.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }

                            if (txtBroker_CODE.Text != string.Empty)
                            {
                                hdnfbk.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtBroker_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                                hdnfbrokershortName.Value = clsCommon.getString("select Short_Name from " + qryAccountList + " where Ac_code=" + txtBroker_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");

                            }


                            Bill_To = Convert.ToInt32(txtBill_To.Text != string.Empty ? txtBill_To.Text : "0");
                            if (txtBill_To.Text != string.Empty && txtBill_To.Text != "0")
                            {
                                hdnfbt.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + Bill_To + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }
                            else
                            {
                                bt = 0;
                            }
                            if (txtDO_CODE.Text != string.Empty && txtDO_CODE.Text != "0")
                            {
                                hdnfdocd.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtDO_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            }
                            #region


                            #endregion

                            hdnfpaymentid.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + PP + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            hdnfpaymentTo.Value = PP;
                            hdnfpaymentShort.Value = clsCommon.getString("select Short_Name from " + qryAccountList + " where Ac_code=" + PP + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                            hdnfQTY.Value = txtquantal.Text != string.Empty ? txtquantal.Text : "0";
                            hdnfTenderQty.Value = txtquantal.Text != string.Empty ? txtquantal.Text : "0";
                            hdnfTenderID.Value = clsCommon.getString("select tenderid from nt_1_Tender where Tender_No=" + txtPurcNo.Text + " and " +
                                "Company_Code=" + Session["Company_Code"].ToString() +
                              "");
                            hdnfPaymentStateCode.Value = "0";
                            if (txtMILL_CODE.Text != PP)
                            {
                                hdnfPaymentStateCode.Value = clsCommon.getString("select  isnull(GSTStateCode,0)  from nt_1_accountmaster where Ac_code=" + PP + " and Company_code='" + Session["Company_Code"].ToString() + "'");

                            }

                            DataSet dsi = clsDAL.SimpleQuery("select Purchase_AC,Sale_AC,PurcAcid,SaleAcid from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            if (dsi != null)
                            {
                                Session["PurchAc"] = dsi.Tables[0].Rows[0]["Purchase_AC"].ToString();
                                Session["SaleAc"] = dsi.Tables[0].Rows[0]["Sale_AC"].ToString();
                                Session["Purcid"] = dsi.Tables[0].Rows[0]["PurcAcid"].ToString();
                                Session["Saleid"] = dsi.Tables[0].Rows[0]["SaleAcid"].ToString();

                            }
                            //grdGstAutoId.DataSource = dt1;
                            //grdGstAutoId.DataBind();
                            //ViewState["GstRateAutoId"] = dt1;

                            //this.GstRateAutoId();
                            #endregion
                            //if (txtPurcOrder.Text.Trim() == "1")
                            //{
                            //    drpDeliveryType.SelectedValue = "N";
                            //}
                        }
                    }
                }
            }
            //int doexist = Convert.ToInt32(clsCommon.getString("select count(*) from   nt_1_deliveryorder where  purc_no=" + txtPurcNo.Text +
            //    " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            //    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())));
            //if (doexist >= 1)
            //{

            //    hdnfAutoPurchaseBill.Value = "N";
            //}
            calculation();
            if (txtGETPASS_CODE.Text.Trim() != string.Empty)
            {
                string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + " where Ac_Code=" + txtGETPASS_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string stateName = "";
                if (gststatecode.Trim() != string.Empty)
                {
                    stateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecode + "");
                }
                txtGetpassGstStateCode.Text = gststatecode;
                lbltxtGetpassGstStateName.Text = stateName;
            }
            if (txtvoucher_by.Text.Trim() != string.Empty)
            {
                string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + " where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string stateName = "";
                if (gststatecode.Trim() != string.Empty)
                {
                    stateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecode + "");
                }
                txtVoucherbyGstStateCode.Text = gststatecode;
                lbltxtVoucherbyGstStateName.Text = stateName;
            }
            if (txtcarporateSale.Text != "0" && txtcarporateSale.Text != string.Empty)
            {
                string type = clsCommon.getString("select [DeliveryType] from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_Code=" + Session["Company_Code"].ToString());

                drpDeliveryType.SelectedValue = type;
                if (type == "C")
                {
                    txtCommission.Enabled = true;
                    // txtCommission.Text = "0";

                }
            }
            if (txtPurcOrder.Text != "1")
            {
                drpDeliveryType.Enabled = false;
            }
            else
            {
                drpDeliveryType.Enabled = true;
            }


            if (txtcarporateSale.Text != "0" && txtcarporateSale.Text != string.Empty)
            {
                string sellingType = clsCommon.getString("Select selling_type from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string delieveryType = clsCommon.getString("Select DeliveryType from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                if (sellingType == "P" && delieveryType == "C")
                {
                    string comm = clsCommon.getString("Select CommissionRate from carporatehead where doc_no=" + txtcarporateSale.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    txtCommission.Text = comm.ToString();
                }

            }
            else
            {
                if (txtPurcOrder.Text == "1" && drpDeliveryType.SelectedValue == "C")
                {

                    string comm = clsCommon.getString("Select Commission from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    txtCommission.Text = comm.ToString();
                }

                if (txtPurcOrder.Text != "1" && drpDeliveryType.SelectedValue == "C")
                {
                    string comm = clsCommon.getString("select Commission_Rate from  qrytenderheaddetail" +
                           "  where Tender_No=" + txtPurcNo.Text + " and ID=" + txtPurcOrder.Text +
                           " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    //  + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                    txtCommission.Text = comm.ToString();

                }

            }
            string tenderPayment_To = string.Empty;
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    tenderPayment_To = grdDetail.Rows[i].Cells[4].Text;
                }
            }
            lblPaymenToLegBal.Text = AcBalance(tenderPayment_To);

            //setFocusControl(txtGstRate);
            setFocusControl(txtvoucher_by);
            if (drpDOType.SelectedValue == "DI")
            {
                txtGETPASS_CODE.Enabled = false;
            }
            else
            {
              //  txtGETPASS_CODE.Enabled = true;
            }




            DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Session["GSTRate"] = ds1.Tables[0].Rows[0]["Rate"].ToString();
                    Session["igstrate"] = ds1.Tables[0].Rows[0]["IGST"].ToString();
                    Session["sgstrate"] = ds1.Tables[0].Rows[0]["SGST"].ToString();
                    Session["cgstrate"] = ds1.Tables[0].Rows[0]["CGST"].ToString();
                }
            }

            //Int32 autops = Convert.ToInt32(clsCommon.getString("select count(isnull(purc_no,0)) from nt_1_deliveryorder where purc_no=" + txtPurcNo.Text));

            //if (autops > 0)
            //{
            //    hdnfAutoPurchaseBill.Value = "N";
            //}
            Int32 Insuranceamt = Convert.ToInt32(clsCommon.getString("Select isnull(Insurance,0) from qrymstaccountmaster where Ac_Code='" + txtSaleBillTo.Text +
                  "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
            if (Insuranceamt > 0)
            {
                drpInsured.SelectedValue = "Y";
            }
            else
            {
                drpInsured.SelectedValue = "N";
            }

            if (drpInsured.SelectedValue == "Y")
            {
                //string Insurance = clsCommon.getString("Select isnull(Insurance,0) from qrymstaccountmaster where Ac_Code='" + txtSaleBillTo.Text +
                //    "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                txtInsurance.Text = Insuranceamt.ToString();
            }
            else
            {
                txtInsurance.Text = "0";

            }


        }
        catch
        {
        }
        //setFocusControl(txtvoucher_by);

    }

    private void GstRateAutoId()
    {
        try
        {
            hdnfPDSPartyCode.Value = hdnfPDSPartyCode.Value != string.Empty ? hdnfPDSPartyCode.Value : "0";
            hdnfPDSUnitCode.Value = hdnfPDSUnitCode.Value != string.Empty ? hdnfPDSUnitCode.Value : "0";
            txtBill_To.Text = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";
            txtitem_Code.Text = txtitem_Code.Text != string.Empty ? txtitem_Code.Text : "0";
            DataTable dt1 = new DataTable();
            gst_code = txtGstRate.Text != string.Empty ? txtGstRate.Text : "1";
            if (ViewState["GstRateAutoId"] != null)
            {
                dt1 = (DataTable)ViewState["GstRateAutoId"];
                string gt = dt1.Rows[0]["Doc_no"].ToString();
                if (gst_code != gt)
                {
                    DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + gst_code + " and company_code=" + Session["Company_Code"].ToString() + "");
                    DataTable dt11 = ds1.Tables[0];
                    dt1.Rows[0]["Doc_no"] = dt11.Rows[0]["Doc_no"].ToString();
                    dt1.Rows[0]["GST_Name"] = dt11.Rows[0]["GST_Name"].ToString();
                    dt1.Rows[0]["Rate"] = dt11.Rows[0]["Rate"].ToString();
                    dt1.Rows[0]["IGST"] = dt11.Rows[0]["IGST"].ToString();
                    dt1.Rows[0]["SGST"] = dt11.Rows[0]["SGST"].ToString();
                    dt1.Rows[0]["CGST"] = dt11.Rows[0]["CGST"].ToString();
                }

            }
            else
            {

                DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + gst_code + " and company_code=" + Session["Company_Code"].ToString() + "");
                dt1 = ds1.Tables[0];
                dt1.Columns.Add(new DataColumn("mc", typeof(string)));//
                dt1.Columns.Add(new DataColumn("gp", typeof(string)));//
                dt1.Columns.Add(new DataColumn("st", typeof(string)));//
                dt1.Columns.Add(new DataColumn("sb", typeof(string)));//
                dt1.Columns.Add(new DataColumn("tc", typeof(string)));//
                dt1.Columns.Add(new DataColumn("va", typeof(string)));//
                dt1.Columns.Add(new DataColumn("bt", typeof(string)));//
                dt1.Columns.Add(new DataColumn("ic", typeof(string)));//
                dt1.Columns.Add(new DataColumn("docd", typeof(string)));//
                dt1.Columns.Add(new DataColumn("bk", typeof(string)));//
                dt1.Columns.Add(new DataColumn("cscode", typeof(string)));//
                dt1.Columns.Add(new DataColumn("TenderQty", typeof(string)));
                dt1.Columns.Add(new DataColumn("TID", typeof(string)));
                dt1.Columns.Add(new DataColumn("ID", typeof(string)));
                dt1.Columns.Add(new DataColumn("TDetailID", typeof(string)));
                dt1.Columns.Add(new DataColumn("PDS", typeof(string)));
                dt1.Columns.Add(new DataColumn("pdsacID", typeof(string)));
                dt1.Columns.Add(new DataColumn("pdsunitID", typeof(string)));
                dt1.Columns.Add(new DataColumn("pdspartyStateCode", typeof(string)));
                dt1.Columns.Add(new DataColumn("pdsunitStateCode", typeof(string)));
                dt1.Columns.Add(new DataColumn("billtoStateCode", typeof(string)));
                dt1.Columns.Add(new DataColumn("PaymentStateCode", typeof(string)));
                dt1.Columns.Add(new DataColumn("paymentid", typeof(string)));
                dt1.Columns.Add(new DataColumn("paymentTo", typeof(string)));
                dt1.Columns.Add(new DataColumn("PurchAc", typeof(string)));
                dt1.Columns.Add(new DataColumn("Purcid", typeof(string)));
                dt1.Columns.Add(new DataColumn("SaleAc", typeof(string)));
                dt1.Columns.Add(new DataColumn("Saleid", typeof(string)));
                dt1.Columns.Add(new DataColumn("UnitCity", typeof(string)));
                dt1.Columns.Add(new DataColumn("paymentShort", typeof(string)));
                dt1.Columns.Add(new DataColumn("TDSAcid", typeof(string)));
                dt1.Columns.Add(new DataColumn("CashDiffAcid", typeof(string)));

            }
            dt1.Rows[0]["TDSAcid"] = hdnfTDSAcid.Value != string.Empty ? hdnfTDSAcid.Value : "0";
            dt1.Rows[0]["CashDiffAcid"] = hdnfCashDiffAcid.Value != string.Empty ? hdnfCashDiffAcid.Value : "0";
            dt1.Rows[0]["mc"] = hdnfmc.Value != string.Empty ? hdnfmc.Value : "0";
            dt1.Rows[0]["gp"] = hdnfgp.Value != string.Empty ? hdnfgp.Value : "0";
            dt1.Rows[0]["st"] = hdnfst.Value != string.Empty ? hdnfst.Value : "0";
            dt1.Rows[0]["sb"] = hdnfsb.Value != string.Empty ? hdnfsb.Value : "0";
            dt1.Rows[0]["tc"] = hdnftc.Value != string.Empty ? hdnftc.Value : "0";
            dt1.Rows[0]["va"] = hdnfva.Value != string.Empty ? hdnfva.Value : "0";
            dt1.Rows[0]["bt"] = hdnfbt.Value != string.Empty ? hdnfbt.Value : "0";
            dt1.Rows[0]["ic"] = hdnfic.Value != string.Empty ? hdnfic.Value : "0";
            dt1.Rows[0]["docd"] = hdnfdocd.Value != string.Empty ? hdnfdocd.Value : "0";
            dt1.Rows[0]["bk"] = hdnfbk.Value != string.Empty ? hdnfbk.Value : "0";
            dt1.Rows[0]["cscode"] = hdnfcscode.Value != string.Empty ? hdnfcscode.Value : "0";

            dt1.Rows[0]["PDS"] = lblCSYearCode.Text;
            dt1.Rows[0]["pdsacID"] = clsCommon.getString("select isnull(accoid,0) as id from nt_1_accountmaster where Ac_Code=" + hdnfPDSPartyCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            dt1.Rows[0]["pdsunitID"] = clsCommon.getString("select isnull(accoid,0) as id from nt_1_accountmaster where Ac_Code=" + hdnfPDSUnitCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            dt1.Rows[0]["pdspartyStateCode"] = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + hdnfPDSPartyCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            dt1.Rows[0]["pdsunitStateCode"] = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + hdnfPDSUnitCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            dt1.Rows[0]["billtoStateCode"] = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + txtBill_To.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            DataSet dsi = clsDAL.SimpleQuery("select Purchase_AC,Sale_AC,PurcAcid,SaleAcid from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            if (dsi != null)
            {
                if (dsi.Tables[0].Rows.Count > 0)
                {
                    dt1.Rows[0]["PurchAc"] = dsi.Tables[0].Rows[0]["Purchase_AC"].ToString();
                    dt1.Rows[0]["SaleAc"] = dsi.Tables[0].Rows[0]["Sale_AC"].ToString();
                    dt1.Rows[0]["Purcid"] = dsi.Tables[0].Rows[0]["PurcAcid"].ToString();
                    dt1.Rows[0]["Saleid"] = dsi.Tables[0].Rows[0]["SaleAcid"].ToString();
                }

            }
            if (hdnfPDSUnitCode.Value != "0")
            {
                dt1.Rows[0]["UnitCity"] = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + hdnfPDSUnitCode.Value + " and  Company_Code=" + Session["Company_Code"].ToString() + "");
            }
            else
            {
                dt1.Rows[0]["UnitCity"] = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + txtvoucher_by.Text + " and  Company_Code=" + Session["Company_Code"].ToString() + "");

            }
            grdGstAutoId.DataSource = dt1;
            grdGstAutoId.DataBind();

            ViewState["GstRateAutoId"] = dt1;
        }
        catch
        {
        }
    }

    protected void btntxtPurcNo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPurcNo";
            pnlPopup.ScrollBars = ScrollBars.Both;
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }

    protected void btntxtcarporateSale_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtcarporateSale";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void btntxtUTRNo_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtUTRNo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void txtcarporateSale_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcarporateSale.Text;
        strTextBox = "txtcarporateSale";
        hdnfpacking.Value = "2";
        csCalculations();
        setFocusControl(txtMILL_CODE);
    }

    protected void txtUTRNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            searchString = txtUTRNo.Text;
            strTextBox = "txtUTRNo";
            double Utr_Balance = hdnfUtrBalance.Value.TrimStart().ToString() != string.Empty ? Convert.ToDouble(hdnfUtrBalance.Value.TrimStart()) : 0.0;
            double Bank_Amount = hdnfMainBankAmount.Value.TrimStart().ToString() != string.Empty ? Convert.ToDouble(hdnfMainBankAmount.Value.TrimStart()) : 0.0;
            bool isValidated = true;
            if (txtUTRNo.Text != string.Empty && txtUTRNo.Text.Trim() != "0")
            {
                string qry = "";
                //qry = "select Year_Code,Company_Code from qryutrdobalanceforfinalview where Company_Code=" 
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + txtUTRNo.Text;


                qry = "select Year_Code,Company_Code from qryutrdobalanceforfinalview where Company_Code="
                 + Convert.ToInt32(Session["Company_Code"].ToString()) +
                 "  and doc_no=" + txtUTRNo.Text;
                // string s = clsCommon.getString(qry);

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
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                isValidated = true;
                                lblUTRYearCode.Text = dt.Rows[i]["Year_Code"].ToString();
                                lblUTRCompnyCode.Text = dt.Rows[i]["Company_Code"].ToString();
                            }

                        }
                    }
                }
                else
                {
                    isValidated = false;
                    txtUTRNo.Text = "";
                    lblUTRYearCode.Text = "";
                    lblUTRCompnyCode.Text = "";
                    setFocusControl(txtUTRNo);
                    return;
                }

                if (Bank_Amount > Utr_Balance)
                {
                    txtBANK_AMOUNT.Text = Utr_Balance.ToString();
                    lblUtrBalnceError.Text = "Mill Amount Is Greater Than Utr Balance.Remaining UTR Balance Reflect to Amount.Please Select Another UTR";
                    ViewState["ankush"] = "A";
                }
                else
                {
                    double millamount = Convert.ToDouble(txtBANK_AMOUNT.Text != string.Empty ? txtBANK_AMOUNT.Text : "0.00");
                    if (millamount < Bank_Amount)
                    {
                        txtBANK_AMOUNT.Text = millamount.ToString();
                    }
                    else
                    {
                        txtBANK_AMOUNT.Text = Bank_Amount.ToString();
                    }
                    lblUtrBalnceError.Text = "";
                }

            }
            if (strTextBox == "txtUTRNo")
            {
                setFocusControl(txtNARRATION);
            }
        }
        catch (Exception)
        {
            throw;
        }
        //csCalculations();
    }

    protected void txtMillEmailID_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMillEmailID.Text;
        strTextBox = "txtMillEmailID";
        csCalculations();
    }

    protected void txtSaleBillTo_TextChanged(object sender, EventArgs e)
    {
        if (txtcarporateSale.Text != "0" && txtcarporateSale.Text != string.Empty)
        {
            string sellingType = clsCommon.getString("Select selling_type from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string delieveryType = clsCommon.getString("Select DeliveryType from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            if (sellingType == "P" && delieveryType == "C")
            {
                string comm = clsCommon.getString("Select CommissionRate from carporatehead where doc_no=" + txtcarporateSale.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                txtCommission.Text = comm.ToString();
            }

        }
        else
        {
            if (txtPurcOrder.Text == "1" && drpDeliveryType.SelectedValue == "C")
            {

                string comm = clsCommon.getString("Select Commission from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                txtCommission.Text = comm.ToString();
            }

            if (txtPurcOrder.Text != "1" && drpDeliveryType.SelectedValue == "C")
            {
                string comm = clsCommon.getString("select Commission_Rate from  qrytenderheaddetail" +
                       "  where Tender_No=" + txtPurcNo.Text + " and ID=" + txtPurcOrder.Text +
                       " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                txtCommission.Text = comm.ToString();
                //string comm = clsCommon.getString("Select Commission from " + qryAccountList + " where Ac_Code=" + txtSaleBillTo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                //txtCommission.Text = comm.ToString();
            }

        }

        searchString = txtSaleBillTo.Text;
        strTextBox = "txtSaleBillTo";
        // hdnfTCSRate.Value = txtTCSRate_Sale.Text;
        if (btnSave.Text == "Save")
        {
            AmtCalculation();
        }
        if (txtSaleBillTo.Text != hdnfsalebillto.Value)
        {
            AmtCalculation();
        }
        csCalculations();
        //txtCashDiffAc.Text = txtSaleBillTo.Text;
        //lblCashDiffAcname.Text = txtNARRATION4.Text;
        //hdnfCashDiffAcid.Value = hdnfsb.Value;
    }

    protected void txtwithGst_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtwithGst_Amount.Text;
        strTextBox = "txtwithGst_Amount";
        csCalculations();
    }

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
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
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtDOC_NO.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtdoc_no.Text = string.Empty;
                    txtdoc_no.Enabled = true;
                    btnSave.Enabled = false;
                    setFocusControl(txtdoc_no);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtDOC_NO.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DO--";
                    tdDate.Visible = true;
                    string fromdt = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                    string todt = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                    //string qry = "select distinct(doc_no) as No,ISNULL(LEFT(millName,15),millShortName) as Mill,VoucherByname As Voucher_By," +
                    //    "GetPassName as Getpass,getpasscity,quantal as Qntl,mill_rate as [M.R],sale_rate as [S.R],convert(varchar(10),doc_date,103) as Date" +
                    //    " ,voucher_no,truck_no,FreightPerQtl as Frieght,vasuli_rate1 as Vasuli,TransportName,memo_no from "
                    //    + qryCommon + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    //    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "'" +
                    //    " and doc_date between '" + fromdt + "' and '" + todt + "' and (doc_no like '%" + txtSearchText.Text + "%' or truck_no like '%"
                    //    + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or millName like '%" + txtSearchText.Text
                    //    + "%' or truck_no like '%" + txtSearchText.Text + "%' or VoucherByname like '%" + txtSearchText.Text + "%') order by doc_no desc";

                    string qry = "select  doc_no as No,convert(varchar(10),doc_date,103) as Date,ISNULL(LEFT(millName,15),millShortName) as Mill,GetPassName as Getpass," +
                        "VoucherByname As Voucher_By,voucherby_cityname,ISNULL(LEFT(narration4,15),millShortName) as SalebillTo,getpasscity,quantal as Qntl,mill_rate as [M.R]," +
                        "sale_rate as [S.R] ,SB_No as SaleBill_no,voucher_no as Purchase_No,truck_no, " +
                        "FreightPerQtl as Frieght,vasuli_rate1 as Vasuli,ISNULL(LEFT(TransportName,15),millShortName)AS TransportShortName,memo_no from NT_1_qryDeliveryOrderList" +
                        " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                        + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_date between '" + fromdt + "' and '" + todt + "'" +
                        "and(doc_no like '%" + txtSearchText.Text + "%' or truck_no like '%"
                        + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or millName like '%" + txtSearchText.Text
                        + "%' or truck_no like '%" + txtSearchText.Text + "%' or VoucherByname like '%" + txtSearchText.Text + "%' or voucherby_cityname like '%" + txtSearchText.Text + "%') order by doc_no desc";

                    this.showPopup(qry);
                }
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {

                lblPopupHead.Text = "--Select Brand--";
                string qry = " select Code,Marka from Brand_Master where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + "and Mal_Code=" + txtitem_Code.Text + "and Code Like '%" + txtSearchText.Text + "%'or Marka like '%" + txtSearchText.Text + "%' ";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                //txtSearchText.Text = searchString;
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Mill--";
                //string qry = "select Ac_Code,Ac_Name_E as Name,Short_Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //   "  and Ac_type='M' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%'or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0  and  Ac_type='M' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E desc";
                this.showPopupAccountMaster(qry);

                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " + qryAccountList + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type='M' " +
                //    " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBill_To")
            {
                lblPopupHead.Text = "--Select Party--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' )";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtTDSAc")
            {
                lblPopupHead.Text = "--Select Party--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);

                string qry = " Locked=0 and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%')";
                this.showPopupAccountMaster(qry);

            }
            if (hdnfClosePopup.Value == "txtCashDiffAc")
            {
                lblPopupHead.Text = "--Select Party--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);

                string qry = " Locked=0 and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%')";
                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtGstRate")
            {
                //txtSearchText.Text = searchString;
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Mill--";
                string qry = "select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where Locked=0 and " +
                    "  (Doc_No like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtMemoGSTRate")
            {
                //txtSearchText.Text = searchString;
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Mill--";
                string qry = "select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where " +
                    "  (Doc_No like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGETPASS_CODE")
            {
                //txtSearchText.Text = txtGETPASS_CODE.Text;
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select GetpassCode--";
                //string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //   "  and Ac_type!='C'   and Ac_type!='B' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0  and Ac_type!='C' and Ac_type!='B' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                this.showPopupAccountMaster(qry);
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                //    qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type!='C' and " +
                //    qryAccountList + ".Ac_type!='B'" +
                //     " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtGetpassGstStateCode" || hdnfClosePopup.Value == "txtVoucherbyGstStateCode" || hdnfClosePopup.Value == "txtSalebilltoGstStateCode" || hdnfClosePopup.Value == "txtMillGstStateCode" || hdnfClosePopup.Value == "txtTransportGstStateCode")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Getpass State Code--";
                string qry = "select State_Code,State_Name from GSTStateMaster where (State_Code like '%" + txtSearchText.Text + "%' or State_Name LIKE '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtvoucher_by")
            {

                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Voucher--";
                //string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //    "  and Ac_type!='C'   and Ac_type!='B' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0  and Ac_type!='C' and Ac_type!='B' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtVasuliAc")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select VasuliAc--";
                //string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //   "  and Ac_type!='C'   and Ac_type!='B' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0  and Ac_type!='C' and Ac_type!='B' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                this.showPopupAccountMaster(qry);
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                //    qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type!='C' and " + qryAccountList + ".Ac_type!='B' " +
                //     " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGRADE")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Grade--";
                string qry = "select  System_Name_E from " + tblPrefix + "SystemMaster where System_Type='S' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and System_Name_E like '%" + txtSearchText.Text + "%' ";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtDO_CODE")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Do--";
                //string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City,Short_Name from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //   "  and Ac_type!='C'   and Ac_type!='B' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);

                string qry = " Locked=0  and Ac_type!='C' and Ac_type!='B' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                  + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                this.showPopupAccountMaster(qry);

                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                //    qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type!='C' and " + qryAccountList + ".Ac_type!='B' " +
                //     " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBroker_CODE")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Broker--";

                //string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //   "  and Ac_type!='C'   and Ac_type!='B' and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);

                string qry = " Locked=0  and Ac_type!='C' and Ac_type!='B' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                 + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                this.showPopupAccountMaster(qry);

                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                //    qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type!='C' and " + qryAccountList + ".Ac_type!='B' " +
                //     " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtTRANSPORT_CODE")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select transport Code--";
                //string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                //   "  and Ac_type='T'    and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0 and Ac_type='T' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                   + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                this.showPopupAccountMaster(qry);
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                //    qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type='T' " +
                //     " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNARRATION1")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Narration --";
                string qry = "select System_Name_E as Narration from " + SystemMastertable + " where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNARRATION2")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E as Narration from " + SystemMastertable + " where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNARRATION3")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E as Narration from " + SystemMastertable + " where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNARRATION4" || hdnfClosePopup.Value == "txtparty" || hdnfClosePopup.Value == "txtSaleBillTo")
            {
                tdDate.Visible = false;
                string self_ac = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (drpDeliveryType.SelectedValue == "DI")
                {

                    //  string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                    //"   and Ac_Code!=2 and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                    //  this.showPopup(qry);

                    string qry = " Locked=0  and Ac_Code!=2 and Ac_type!='C' and Ac_type!='B' and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') order by Ac_Name_E ";
                    this.showPopupAccountMaster(qry);

                    //if (self_ac == txtGETPASS_CODE.Text)
                    //{
                    //    hdnfClosePopup.Value = "txtparty";
                    //    lblPopupHead.Text = "--Select Party--";
                    //    string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                    //  " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                    //  qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type!='C' and " + qryAccountList + ".Ac_type!='B' " +
                    //   " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                    //    this.showPopup(qry);
                    //}
                    //else
                    //{
                    //    hdnfClosePopup.Value = "txtNARRATION4";
                    //    lblPopupHead.Text = "--Select Narration--";
                    //    string qry = "select System_Name_E as Narration from " + SystemMastertable + " where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                    //    this.showPopup(qry);
                    //}
                }
                else
                {
                    txtSearchText.Text = searchString;
                    hdnfClosePopup.Value = "txtparty";
                    lblPopupHead.Text = "--Select Party--";

                    string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                  "  and Ac_Code!=2   and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                    this.showPopup(qry);

                    //  string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                    //" left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                    //qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + qryAccountList + ".Ac_type!='C' and " + qryAccountList + ".Ac_type!='B' " +
                    // " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                    //  this.showPopup(qry);
                }
            }
            if (hdnfClosePopup.Value == "txtBANK_CODE")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Bank--";
                string qry = "select Ac_Code,Ac_Name_E as Name,cityname as City from qrymstaccountmaster where Locked=0 and Company_Code=" + Session["Company_Code"].ToString() + " " +
                   "     and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " +
                //    qryAccountList + ".Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //     " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtNARRATION")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select Narration--";
                string qry = "select System_Name_E as Narration from " + SystemMastertable + " where System_Type='N' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtitem_Code")
            {
                tdDate.Visible = false;
                lblPopupHead.Text = "--Select item--";
                //string qry = "select itemcode ,itemname as Name from qrytenderhead where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString() + " " +
                //    " and Year_Code=" + Session["year"].ToString() + " and (itemcode like '%" + txtSearchText.Text + "%' or  itemname like  '%" + txtSearchText.Text + "%'");

                qry = "select distinct System_Code,System_Name_E from nt_1_systemmaster where System_Type='I' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                    " and (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPurcNo")
            {
                tdDate.Visible = false;
                if (txtMILL_CODE.Text != string.Empty)
                {
                    lblPopupHead.Text = "--Select No--";
                    //string qry = "select Tender_No,Convert(varchar(10),Tender_Date,103) as Tender_Date,salepartyfullname as Party,buyerbrokerfullname as Party2,Mill_Rate,Grade,Sale_Rate,Buyer_Quantal,despatchqty,balance,doname,Convert(varchar(10),Lifting_Date,103) as Lifting_Date,ID,Delivery_Type as DT from "
                    // + qrypurc_No + " where  Mill_Code=" + txtMILL_CODE.Text + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    // " and (Tender_No like '%" + txtSearchText.Text + "%' or  Tender_Date like '%" + txtSearchText.Text + "%' or salepartyfullname like  '%" + txtSearchText.Text + "%' or buyerbrokerfullname like '%" + txtSearchText.Text + "%' or doname like '%" + txtSearchText.Text + "%') and balance!=0 and Mill_Code=" + txtMILL_CODE.Text + "  order by Tender_No desc";
                    string qry = "";
                    if (btnSave.Text == "Save")
                    {
                        qry = "select Tender_No,Tender_DateConverted as Tender_Date,buyername as Party2,buyerpartyname as Party,Mill_Rate,Grade,Sale_Rate,Buyer_Quantal,DESPATCH,BALANCE," +
                           "tenderdoname as doname,Lifting_DateConverted as Lifting_Date,ID,tenderdetailid,tenderid,Delivery_Type,shiptoname,tenderdoshortname,season,Party_Bill_Rate from " +
                        " qrytenderdobalanceview where BALANCE!=0 and Mill_Code=" + txtMILL_CODE.Text +
                        " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +

                        " and (Tender_No like '%" + txtSearchText.Text + "%' or  buyerpartyname like  '%" +
                        txtSearchText.Text + "%' or buyername like '%" + txtSearchText.Text + "%' or tenderdoname like '%" + txtSearchText.Text + "%') " +
                        "  and balance!=0   order by Tender_No desc";
                    }
                    else
                    {

                        qry = "select Tender_No,Tender_DateConverted as Tender_Date,buyername as Party2,buyerpartyname as Party,Mill_Rate,Grade,Sale_Rate,Buyer_Quantal,DESPATCH,BALANCE," +
                         "tenderdoname as doname,Lifting_DateConverted as Lifting_Date,ID,tenderdetailid,tenderid,Delivery_Type,shiptoname,tenderdoshortname,season,Party_Bill_Rate from " +
                      " qrytenderdobalanceview where   Mill_Code=" + txtMILL_CODE.Text +
                      " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +

                      " and (Tender_No like '%" + txtSearchText.Text + "%' or  buyerpartyname like  '%" + txtSearchText.Text +
                      "%' or buyername like '%" + txtSearchText.Text + "%' or tenderdoname like '%" + txtSearchText.Text + "%') " +
                      "     order by Tender_No desc";

                    }
                    this.showPopup(qry);
                }
                else
                {
                    setFocusControl(txtMILL_CODE);
                    pnlPopup.Style["display"] = "none";
                }
            }

            if (hdnfClosePopup.Value == "txtcarporateSale")//
            {
                tdDate.Visible = false;
                string qry = "";
                lblPopupHead.Text = "--Select Carporate Sale No--";
                if (ViewState["mode"].ToString() == "I")
                {
                    qry = "select distinct(Doc_No),doc_dateConverted as Doc_Date,carporatepartyaccountname as partyName,carporatepartyunitname as UnitName,sell_rate,pono as Po_Details,quantal,dispatched,balance,selling_type  from qrycarporatedobalance where balance!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and (carporatepartyaccountname like '%" + txtSearchText.Text + "%' or carporatepartyunitname like '%" + txtSearchText.Text + "%' or doc_dateConverted like '%" + txtSearchText.Text + "%')";
                }
                else
                {
                    qry = "select distinct(Doc_No),doc_dateConverted as Doc_Date,carporatepartyaccountname as partyName,carporatepartyunitname as UnitName,sell_rate,pono as Po_Details,quantal,dispatched,balance,selling_type  from qrycarporatedobalance where balance!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and (carporatepartyaccountname like '%" + txtSearchText.Text + "%' or carporatepartyunitname like '%" + txtSearchText.Text + "%' or doc_dateConverted like '%" + txtSearchText.Text + "%')";
                }

                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtUTRNo")
            {
                using (clsDataProvider obj = new clsDataProvider())
                {
                    tdDate.Visible = false;
                    if (txtBANK_CODE.Text != string.Empty)
                    {
                        lblPopupHead.Text = "--Select UTR No--";
                        //string qry = "select doc_no,utr_no,bankname,UTRAmount,UsedAmt,balance,narration_header,Year_Code,doc_date  from " + tblPrefix 
                        //    + "qryUTRBalanceForDO where balance!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) 
                        //    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and mill_code=" + txtBANK_CODE.Text;

                        //string qry = "select doc_no,utr_no,bankname,UTRAmount,UsedAmt,balance,narration_header,Year_Code,Convert(varchar(10),doc_date,103) as doc_date  from " + tblPrefix
                        //+ "qryUTRBalanceForDO where balance!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and mill_code=" + txtBANK_CODE.Text;

                        // string qry = "select doc_no,utr_no,bankname,'' as UTRAmount,lot_no,amount as amountDetail,paidamount as UsedAmt,balanceamount as balance,narration_header,Year_Code,utrdateConverted as doc_date,utrdetailid,Company_Code from " +
                        // " qryutrdobalanceforfinalview  where balanceamount!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        //+ " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + 
                        //" and mill_code=" + txtBANK_CODE.Text + " order by doc_no desc";

                        string qry = "select doc_no,utr_no,bankname,'' as UTRAmount,lot_no,amount as amountDetail,paidamount as UsedAmt,balanceamount as balance,narration_header,Year_Code,utrdateConverted as doc_date,utrdetailid,Company_Code from " +
                      " qryutrdobalanceforfinalview  where balanceamount!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and mill_code=" + txtBANK_CODE.Text + " order by doc_no desc";
                        DataSet ds = new DataSet();
                        ds = obj.GetDataSet(qry);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string utrno = "";
                                string dtUtrNo = dt.Rows[i]["doc_no"].ToString();
                                double AmtTotal = 0.00;
                                for (int j = 0; j < grdDetail.Rows.Count; j++)
                                {
                                    string grdUtrNo = grdDetail.Rows[j].Cells[8].Text.ToString();
                                    string rowAction = grdDetail.Rows[j].Cells[12].Text.ToString();
                                    if (dtUtrNo == grdUtrNo && rowAction == "A")
                                    {
                                        double Amt = Convert.ToDouble(grdDetail.Rows[j].Cells[7].Text.ToString());
                                        AmtTotal += Amt;
                                        utrno = dtUtrNo;
                                    }
                                }
                                if (dtUtrNo == utrno)
                                {
                                    double balance = Convert.ToDouble(dt.Rows[i]["balance"].ToString());
                                    double totalBal = balance - AmtTotal;
                                    dt.Rows[i]["balance"] = totalBal;
                                    dt.Rows[i]["UsedAmt"] = AmtTotal;

                                }
                            }
                            if (dt.Rows.Count > 0)
                            {
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    if (dt.Rows[k]["balance"].ToString() == "0")
                                    {
                                        dt.Rows[k].Delete();
                                    }
                                }
                                grdPopup.DataSource = dt;
                                grdPopup.DataBind();
                                hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                                setFocusControl(txtSearchText);
                            }
                            else
                            {
                                grdPopup.DataSource = null;
                                grdPopup.DataBind();
                                hdHelpPageCount.Value = "0";
                            }
                        }
                        else
                        {
                            grdPopup.DataSource = null;
                            grdPopup.DataBind();
                            hdHelpPageCount.Value = "0";
                        }
                    }
                    else
                    {
                        setFocusControl(txtBANK_CODE);
                        pnlPopup.Style["display"] = "none";
                    }
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
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "kj", "javascript:SelectRow(0, {0});", true);
                        //grdPopup.Rows[0].Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});",grdPopup.Rows[0].RowIndex);
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
            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                setFocusControl(txtMILL_CODE);
            }
            if (hdnfClosePopup.Value == "txtGETPASS_CODE")
            {
                setFocusControl(txtGETPASS_CODE);
            }
            if (hdnfClosePopup.Value == "txtvoucher_by")
            {
                setFocusControl(txtvoucher_by);
            }
            if (hdnfClosePopup.Value == "txtBroker_CODE")
            {
                setFocusControl(txtBroker_CODE);
            }
            if (hdnfClosePopup.Value == "txtTRANSPORT_CODE")
            {
                setFocusControl(txtTRANSPORT_CODE);
            }
            if (hdnfClosePopup.Value == "txtDO_CODE")
            {
                setFocusControl(txtDO_CODE);
            }
            if (hdnfClosePopup.Value == "txtBANK_CODE")
            {
                setFocusControl(txtBANK_CODE);
            }

            hdnfClosePopup.Value = "Close";
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
            setFocusControl(btnSave);
        }
        catch
        {
        }
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {

        bool isValidated = true;
        try
        {
            if (strTextBox == "txtdoc_no")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtdoc_no.Text, out n);
                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtdoc_no.Text != string.Empty)
                        {
                            txtValue = txtdoc_no.Text;
                            string qry = "select * from " + tblHead + "  where   Doc_No='" + txtValue + "' " +
                                "  and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "'";

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
                                                txtdoc_no.Enabled = true;
                                                //hdnf.Value = txtdoc_no.Text;
                                                btnSave.Enabled = true;   //IMP
                                                setFocusControl(txtDOC_DATE);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                pnlgrdDetail.Enabled = true;
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = true;
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtDOC_DATE);
                                            txtdoc_no.Enabled = true;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtdoc_no.Text = string.Empty;
                                            setFocusControl(txtdoc_no);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtdoc_no);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtdoc_no.Text = string.Empty;
                        setFocusControl(txtdoc_no);
                    }
                }
                catch
                {

                }
                #endregion

                // return;
            }
            if (strTextBox == "txtDOC_DATE")
            {
                if (txtDOC_DATE.Text != string.Empty)
                {
                    try
                    {
                        //string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        //DateTime Start_Date = DateTime.Parse(HttpContext.Current.Session["Start_Date"].ToString());
                        //DateTime End_Date = DateTime.Parse(HttpContext.Current.Session["End_Date"].ToString());
                        //txtNARRATION1.Text = Start_Date.ToString();
                        //txtNARRATION2.Text = DateTime.Parse(HttpContext.Current.Session["Start_Date"].ToString()).ToString();
                        //txtNARRATION3.Text = End_Date.ToString();
                        //txtNARRATION4.Text = DateTime.Parse(HttpContext.Current.Session["End_Date"].ToString()).ToString();
                        if (clsCommon.isValidDate(txtDOC_DATE.Text) == true)
                        {
                            txtPurchase_Date.Text = txtDOC_DATE.Text;
                            txtnewsbdate.Text = txtDOC_DATE.Text;
                            txtdo_date.Text = txtDOC_DATE.Text;
                            setFocusControl(btntxtcarporateSale);
                        }
                        else
                        {
                            txtDOC_DATE.Text = string.Empty;
                            setFocusControl(txtDOC_DATE);
                        }
                    }
                    catch (Exception exx)
                    {
                        txtNARRATION1.Text = exx.Message;
                        txtDOC_DATE.Text = string.Empty;
                        setFocusControl(txtDOC_DATE);
                    }
                }
                else
                {
                    setFocusControl(txtDOC_DATE);
                }

                // return;
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
                        acname = clsCommon.getString("select Marka from Brand_Master where Code=" + txtBrand_Code.Text + " and Mal_Code=" + txtitem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != "0")
                        {
                            lblBrandname.Text = acname;

                            setFocusControl(txtvoucher_by);

                        }
                        else
                        {
                            txtBrand_Code.Text = "0";
                            lblBrandname.Text = acname;
                            setFocusControl(txtvoucher_by);
                        }
                    }
                }
                else
                {
                    txtBrand_Code.Text = "0";
                    setFocusControl(txtvoucher_by);
                }
            }
            if (strTextBox == "txtBill_To")
            {
                if (txtBill_To.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBill_To.Text);
                    if (a == false)
                    {

                        // btntxtBill_To_Click(this, new EventArgs());
                        btntxtbill_To_Click(this, new EventArgs());
                    }
                    else
                    {
                        string billto = "";
                        string billshort = "";
                        if (txtBill_To.Text != string.Empty)
                        {

                            DataSet ds = clsDAL.SimpleQuery("select * from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtBill_To.Text + "");
                            if (ds != null)
                            {
                                DataTable dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    billto = dt.Rows[0]["Ac_Name_E"].ToString();
                                    billshort = dt.Rows[0]["Short_Name"].ToString();
                                    hdnfbt.Value = dt.Rows[0]["accoid"].ToString();
                                }
                            }

                            if (billto != string.Empty && billto != "0")
                            {
                                lblBill_To.Text = billto;
                                hdnfbilltoshortname.Value = billshort;
                                // setFocusControl(txtpodetail);
                            }
                        }
                        else
                        {
                            //setFocusControl(txtpodetail);
                        }
                    }
                }
                else
                {
                    // setFocusControl(txtpodetail);
                }
            }
            if (strTextBox == "txtPurchase_Date")
            {
                if (txtPurchase_Date.Text != string.Empty)
                {
                    try
                    {
                        if (clsCommon.isValidDate(txtPurchase_Date.Text) == true)
                        {
                            setFocusControl(txtGETPASS_CODE);
                        }
                        else
                        {
                            txtPurchase_Date.Text = string.Empty;
                            setFocusControl(txtPurchase_Date);
                        }
                    }
                    catch (Exception exx)
                    {
                        txtNARRATION1.Text = exx.Message;
                        txtPurchase_Date.Text = string.Empty;
                        setFocusControl(txtPurchase_Date);
                    }
                }
                else
                {
                    setFocusControl(txtPurchase_Date);
                }

                // return;
            }
            if (strTextBox == "drpDOType")
            {
                string s_item = "";
                s_item = drpDOType.SelectedValue;
                if (s_item == "DI")
                {
                    pnlgrdDetail.Enabled = true;
                    btnOpenDetailsPopup.Enabled = true;
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();
                    drpDeliveryType.Visible = true;

                    // txtUTRNo.Enabled = true;
                    //btntxtUTRNo.Enabled = true;

                }
                else
                {
                    drpDeliveryType.Visible = false;
                    pnlgrdDetail.Enabled = false;
                    btnOpenDetailsPopup.Enabled = false;
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();

                    //txtUTRNo.Text = "";
                    //txtUTRNo.Enabled = false;
                    //btntxtUTRNo.Enabled = false;
                }
                setFocusControl(txtMILL_CODE);
            }
            if (strTextBox == "txtSaleBillTo")
            {

                if (txtSaleBillTo.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtSaleBillTo.Text);
                    if (a == false)
                    {
                        btntxtNARRATION4_Click(this, new EventArgs());
                    }
                    else
                    {
                        string aa = txtSaleBillTo.Text;
                        char[] ch = aa.ToCharArray();
                        for (int i = 0; i < aa.Length; i++)
                        {
                            i = 0;
                            string sub = ch[i].ToString();
                            if (sub == "0")
                            {
                                aa = aa.Remove(i, 1);
                                ch = aa.ToCharArray();
                            }
                            else
                            {
                                break;
                            }
                        }
                        txtSaleBillTo.Text = aa;

                        if (txtSaleBillTo.Text == "2")
                        {
                            txtSaleBillTo.Text = string.Empty;
                            setFocusControl(txtSaleBillTo);
                            // return;
                        }
                        string salebillname = string.Empty;
                        string salebilltoshortname = "";
                        string gststatecode1 = string.Empty;
                        string stateName1 = "";
                        string salebilltocityname = clsCommon.getString("select cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtSaleBillTo.Text + "");

                        DataSet ds = clsDAL.SimpleQuery("select * from " + qryAccountList + "   where Ac_Code="
                            + txtSaleBillTo.Text + " and Ac_type!='C' and Ac_type!='B' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                salebillname = dt.Rows[0]["Ac_Name_E"].ToString();
                                salebilltoshortname = dt.Rows[0]["Short_Name"].ToString();
                                gststatecode1 = dt.Rows[0]["GSTStateCode"].ToString();
                                stateName1 = dt.Rows[0]["State_Name"].ToString();
                                hdnfsb.Value = dt.Rows[0]["accoid"].ToString();
                                salebilltocityname = dt.Rows[0]["cityname"].ToString();

                            }
                        }

                        if (salebillname != string.Empty && salebillname != "0")
                        {

                            // salebilltoshortname = clsCommon.getString("select Short_Name from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtSaleBillTo.Text + "");
                            hdnfbilltoshortname.Value = salebilltoshortname;
                            hdnfsalebilltoshortname.Value = salebilltoshortname;
                            txtNARRATION4.Text = salebillname + ", " + salebilltocityname;
                            lblSaleBillToLedgerByBalance.Text = AcBalance(txtSaleBillTo.Text);
                            // gststatecode1 = clsCommon.getString("select GSTStateCode from " + qryAccountList + "   where Ac_Code="
                            //    + txtSaleBillTo.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            //if (gststatecode1.Trim() != string.Empty)
                            //{
                            //    stateName1 = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + gststatecode1 + "");
                            //}

                            //                   string saletds = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
                            //                      + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            //                   if (saletds == "Y")
                            //                   {
                            //                       hdnfSaleTDS.Value = Session["SaleTDSRate"].ToString();
                            //                   }

                            //                   string paymenttotds = clsCommon.getString("select Payment_To from qrytenderheaddetail where Tender_No=" + txtPurcNo.Text
                            //+ " and Company_Code=" + Session["Company_Code"].ToString() + " and ID=" + txtPurcOrder.Text + " and Year_Code=" + Session["year"].ToString());

                            //                   string purtds = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + paymenttotds
                            //                        + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            //                   if (purtds == "Y")
                            //                   {
                            //                       hdnfPurchaseTDS.Value = Session["PurchaseTDSRate"].ToString();
                            //                   }
                            txtSalebilltoGstStateCode.Text = gststatecode1;
                            lbltxtSalebilltoGstStateName.Text = stateName1;
                            setFocusControl(txtGRADE);
                        }
                        else
                        {
                            txtSaleBillTo.Text = string.Empty;
                            txtNARRATION4.Text = salebillname + "," + salebilltocityname;
                            setFocusControl(txtSaleBillTo);
                        }


                    }
                }
                else
                {
                    txtNARRATION4.Text = "";
                    setFocusControl(txtSaleBillTo);
                }
                //return;
            }
            if (strTextBox == "txtitem_Code")
            {
                if (txtitem_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtitem_Code.Text);
                    if (a == false)
                    {
                        btntxtitem_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        lblitem_Name.Text = clsCommon.getString("select System_Name_E from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        hdnfic.Value = clsCommon.getString("select systemid from qrymstitem where System_Type='I' and  System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        hdnfhsnnumber.Value = clsCommon.getString("select HSN from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        hdnfminRate.Value = clsCommon.getString("select minRate from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                        hdnfmaxRate.Value = clsCommon.getString("select maxRate from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");

                        if (lblitem_Name.Text != string.Empty && lblitem_Name.Text != "0")
                        {
                            DataSet dsi = clsDAL.SimpleQuery("select Purchase_AC,Sale_AC,PurcAcid,SaleAcid,Gst_Code from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                            if (dsi != null)
                            {
                                if (dsi.Tables[0].Rows.Count > 0)
                                {
                                    Session["PurchAc"] = dsi.Tables[0].Rows[0]["Purchase_AC"].ToString();
                                    Session["SaleAc"] = dsi.Tables[0].Rows[0]["Sale_AC"].ToString();
                                    Session["Purcid"] = dsi.Tables[0].Rows[0]["PurcAcid"].ToString();
                                    Session["Saleid"] = dsi.Tables[0].Rows[0]["SaleAcid"].ToString();
                                    txtGstRate.Text = dsi.Tables[0].Rows[0]["Gst_Code"].ToString();

                                }

                            }


                            DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
                            if (ds1 != null)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    Session["GSTRate"] = ds1.Tables[0].Rows[0]["Rate"].ToString();
                                    Session["igstrate"] = ds1.Tables[0].Rows[0]["IGST"].ToString();
                                    Session["sgstrate"] = ds1.Tables[0].Rows[0]["SGST"].ToString();
                                    Session["cgstrate"] = ds1.Tables[0].Rows[0]["CGST"].ToString();
                                }
                            }


                            setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            txtitem_Code.Text = string.Empty;
                            setFocusControl(txtitem_Code);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtitem_Code);
                    //return;
                }
            }
            if (strTextBox == "txtMILL_CODE")
            {
                string millName = "";
                if (txtMILL_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMILL_CODE.Text);
                    if (a == false)
                    {
                        btntxtMILL_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        string millshortname1 = "";
                        string gststatecode1 = "";
                        string stateName1 = "";
                        //millName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "  where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type='M' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        DataSet ds = clsDAL.SimpleQuery("select * from " + qryAccountList + "  where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type='M' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                millName = dt.Rows[0]["Ac_Name_E"].ToString();
                                millshortname1 = dt.Rows[0]["Short_Name"].ToString();
                                txtMillEmailID.Text = dt.Rows[0]["Email_Id"].ToString();
                                txtMillMobile.Text = dt.Rows[0]["Mobile_No"].ToString();
                                gststatecode1 = dt.Rows[0]["GSTStateCode"].ToString();
                                stateName1 = dt.Rows[0]["State_Name"].ToString();
                                hdnfmc.Value = dt.Rows[0]["accoid"].ToString();
                            }
                        }

                        if (millName != string.Empty && millName != "0")
                        {

                            // millshortname1 = clsCommon.getString("select Short_Name from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtMILL_CODE.Text + "");

                            hdnfmillshortname.Value = millshortname1;
                            // txtMillEmailID.Text = clsCommon.getString("select Email_Id from " + qryAccountList + "  where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            // txtMillMobile.Text = clsCommon.getString("select Mobile_No from " + qryAccountList + "  where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            LBLMILL_NAME.Text = millName;

                            // gststatecode1 = clsCommon.getString("select GSTStateCode from " + qryAccountList + "   where Ac_Code=" + txtMILL_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            //if (gststatecode1.Trim() != string.Empty)
                            //{
                            //    stateName1 = clsCommon.getString("select State_Name from GSTStateMaster  where State_Code=" + gststatecode1 + "");
                            //}
                            txtMillGstStateCode.Text = gststatecode1;
                            lbltxtMillGstStateCode.Text = stateName1;
                            setFocusControl(btntxtPurcNo);
                        }
                        else
                        {
                            txtMILL_CODE.Text = string.Empty;
                            LBLMILL_NAME.Text = millName;
                            setFocusControl(txtMILL_CODE);
                        }
                    }
                }
                else
                {
                    LBLMILL_NAME.Text = "";
                    setFocusControl(txtMILL_CODE);
                }
                // return;
            }

            if (strTextBox == "txtGstRate")
            {
                string gstratename = "";
                if (txtGstRate.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGstRate.Text);
                    if (a == false)
                    {
                        btntxtGstRate_Click(this, new EventArgs());
                    }
                    else
                    {
                        gstratename = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster  where Doc_No=" + txtGstRate.Text + "");
                        if (gstratename != string.Empty && gstratename != "0")
                        {
                            DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
                            if (ds1 != null)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    Session["GSTRate"] = ds1.Tables[0].Rows[0]["Rate"].ToString();
                                    Session["igstrate"] = ds1.Tables[0].Rows[0]["IGST"].ToString();
                                    Session["sgstrate"] = ds1.Tables[0].Rows[0]["SGST"].ToString();
                                    Session["cgstrate"] = ds1.Tables[0].Rows[0]["CGST"].ToString();
                                }
                            }
                            lblGstRateName.Text = gstratename;

                            GSTCalculations();
                            setFocusControl(txtPurchase_Date);
                        }
                        else
                        {
                            txtGstRate.Text = string.Empty;
                            lblGstRateName.Text = gstratename;
                            setFocusControl(txtGstRate);
                        }
                    }
                }
                else
                {
                    lblGstRateName.Text = "";
                    setFocusControl(txtGstRate);
                }
                //  return;
            }
            if (strTextBox == "txtMemoGSTRate")
            {
                string gstratename = "";
                if (txtMemoGSTRate.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMemoGSTRate.Text);
                    if (a == false)
                    {
                        btntxtMemoGSTRate_Click(this, new EventArgs());
                    }
                    else
                    {
                        gstratename = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster  where Doc_No=" + txtMemoGSTRate.Text + "");
                        if (gstratename != string.Empty && gstratename != "0")
                        {
                            DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtMemoGSTRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
                            if (ds1 != null)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    hdnfmemogstrate.Value = ds1.Tables[0].Rows[0]["Rate"].ToString();
                                    hdnfmemoigst.Value = ds1.Tables[0].Rows[0]["IGST"].ToString();
                                    hdnfmemosgst.Value = ds1.Tables[0].Rows[0]["SGST"].ToString();
                                    hdnfmemocgst.Value = ds1.Tables[0].Rows[0]["CGST"].ToString();
                                    memorgstratecal();
                                }
                            }
                            lblMemoGSTRate.Text = gstratename;

                            // GSTCalculations();
                            setFocusControl(txtTDSAc);
                        }
                        else
                        {
                            txtMemoGSTRate.Text = string.Empty;
                            lblGstRateName.Text = gstratename;
                            setFocusControl(txtMemoGSTRate);
                        }
                    }
                }
                else
                {
                    lblMemoGSTRate.Text = "";
                    setFocusControl(txtMemoGSTRate);
                }
                //  return;
            }
            if (strTextBox == "txtMillEmailID")
            {
                setFocusControl(txtPurcNo);
            }
            if (strTextBox == "txtMillGstStateCode")
            {
                string stateName = "";
                if (txtMillGstStateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMillGstStateCode.Text);
                    if (a == false)
                    {
                        btntxtMillGstStateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        stateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + txtMillGstStateCode.Text + "");
                        if (stateName != string.Empty && stateName != "0")
                        {
                            lbltxtMillGstStateCode.Text = stateName;
                            setFocusControl(btntxtPurcNo);
                        }
                        else
                        {
                            txtMillGstStateCode.Text = string.Empty;
                            lbltxtMillGstStateCode.Text = stateName;
                            setFocusControl(txtMillGstStateCode);
                        }
                    }
                }
                else
                {
                    lbltxtMillGstStateCode.Text = stateName;
                    setFocusControl(txtMillGstStateCode);
                }
                //return;
            }

            if (strTextBox == "txtTransportGstStateCode")
            {
                string stateName = "";
                if (txtTransportGstStateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtTransportGstStateCode.Text);
                    if (a == false)
                    {
                        btntxtTransportGstStateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        stateName = clsCommon.getString("select State_Name from GSTStateMaster  where State_Code=" + txtTransportGstStateCode.Text + "");
                        if (stateName != string.Empty && stateName != "0")
                        {
                            lbltxtTransportGstStateCode.Text = stateName;
                            setFocusControl(txtFrieght);
                        }
                        else
                        {
                            txtTransportGstStateCode.Text = string.Empty;
                            lbltxtTransportGstStateCode.Text = stateName;
                            setFocusControl(txtTransportGstStateCode);
                        }
                    }
                }
                else
                {
                    lbltxtTransportGstStateCode.Text = stateName;
                    setFocusControl(txtTransportGstStateCode);
                }
                //  return;
            }



            if (strTextBox == "txtGetpassGstStateCode")
            {
                string stateName = "";
                if (txtGetpassGstStateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGetpassGstStateCode.Text);
                    if (a == false)
                    {
                        btntxtGetpassGstStateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        stateName = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + txtGetpassGstStateCode.Text + "");
                        if (stateName != string.Empty && stateName != "0")
                        {
                            lbltxtGetpassGstStateName.Text = stateName;
                            setFocusControl(txtitem_Code);
                        }
                        else
                        {
                            txtGetpassGstStateCode.Text = string.Empty;
                            lbltxtGetpassGstStateName.Text = stateName;
                            setFocusControl(txtGetpassGstStateCode);
                        }
                    }
                }
                else
                {
                    lbltxtGetpassGstStateName.Text = stateName;
                    setFocusControl(txtGetpassGstStateCode);
                }
                //  return;
            }

            if (strTextBox == "txtVoucherbyGstStateCode")
            {
                string stateName = "";
                if (txtVoucherbyGstStateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtVoucherbyGstStateCode.Text);
                    if (a == false)
                    {
                        btntxtVoucherbyGstStateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        stateName = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + txtVoucherbyGstStateCode.Text + "");
                        if (stateName != string.Empty && stateName != "0")
                        {
                            lbltxtVoucherbyGstStateName.Text = stateName;
                            if (txtNARRATION4.Enabled == true)
                            {
                                setFocusControl(txtNARRATION4);
                            }
                            else
                            {
                                setFocusControl(txtSaleBillTo);
                            }
                        }
                        else
                        {
                            txtVoucherbyGstStateCode.Text = string.Empty;
                            lbltxtVoucherbyGstStateName.Text = stateName;
                            setFocusControl(txtVoucherbyGstStateCode);
                        }
                    }
                }
                else
                {
                    lbltxtVoucherbyGstStateName.Text = stateName;
                    setFocusControl(txtVoucherbyGstStateCode);
                }
                //  return;
            }

            if (strTextBox == "txtSalebilltoGstStateCode")
            {
                string stateName = "";
                if (txtSalebilltoGstStateCode.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtSalebilltoGstStateCode.Text);
                    if (a == false)
                    {
                        btntxtSalebilltoGstStateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        stateName = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + txtSalebilltoGstStateCode.Text + "");
                        if (stateName != string.Empty && stateName != "0")
                        {
                            lbltxtSalebilltoGstStateName.Text = stateName;
                            setFocusControl(txtGRADE);
                        }
                        else
                        {
                            lbltxtSalebilltoGstStateName.Text = string.Empty;
                            lbltxtSalebilltoGstStateName.Text = stateName;
                            setFocusControl(txtSalebilltoGstStateCode);
                        }
                    }
                }
                else
                {
                    lbltxtSalebilltoGstStateName.Text = stateName;
                    setFocusControl(txtSalebilltoGstStateCode);
                }
                //return;
            }
            if (strTextBox == "txtGETPASS_CODE")
            {
                string getPassName = "";
                if (txtGETPASS_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGETPASS_CODE.Text);
                    if (a == false)
                    {
                        btntxtGETPASS_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        string aa = txtGETPASS_CODE.Text;
                        char[] ch = aa.ToCharArray();
                        for (int i = 0; i < aa.Length; i++)
                        {
                            i = 0;
                            string sub = ch[i].ToString();
                            if (sub == "0")
                            {
                                aa = aa.Remove(i, 1);
                                ch = aa.ToCharArray();
                            }
                            else
                            {
                                break;
                            }
                        }
                        txtGETPASS_CODE.Text = aa;
                        //if (txtGETPASS_CODE.Text != string.Empty)
                        //{
                        //    txtGETPASS_CODE.Text = txtGETPASS_CODE.Text.Substring(1);
                        //}
                        getPassName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "   where Ac_Code=" + txtGETPASS_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (getPassName != string.Empty && getPassName != "0")
                        {
                            string getpasscodecitycode = clsCommon.getString("Select City_Code from " + qryAccountList + "   where Ac_Code=" + txtGETPASS_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string getpasscity = clsCommon.getString("Select city_name_e from " + tblPrefix + "CityMaster   where city_code=" + getpasscodecitycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            LBLGETPASS_NAME.Text = getPassName + ", " + getpasscity;
                            string getpassshortname = "";
                            getpassshortname = clsCommon.getString("select Short_Name from " + qryAccountList + "   where Ac_Code=" + txtGETPASS_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfgetpassshortname.Value = getpassshortname;

                            hdnfgp.Value = clsCommon.getString("select isnull(accoid,0) as id from " + qryAccountList + "   where Ac_Code=" + txtGETPASS_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (txtvoucher_by.Text == "2")
                            {
                                //txtGETPASS_CODE.Text = txtGETPASS_CODE.Text.Substring(1);
                                txtvoucher_by.Text = txtGETPASS_CODE.Text;
                                lblvoucherbyname.Text = LBLGETPASS_NAME.Text + ", " + getpasscity; ;
                                txtCommission.Text = clsCommon.getString("select ISNULL(Commission,0) from " + qryAccountList + "  where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                //string selfac = Session["SELF_AC"].ToString();
                                if (txtGETPASS_CODE.Text == Session["SELF_AC"].ToString())
                                {
                                    txtSALE_RATE.Text = txtmillRate.Text;
                                    calculation();
                                }

                                string gststatecode1 = clsCommon.getString("select GSTStateCode from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                string stateName1 = "";
                                if (gststatecode1.Trim() != string.Empty)
                                {
                                    stateName1 = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecode1 + "");
                                }
                                txtVoucherbyGstStateCode.Text = gststatecode1;
                                lbltxtVoucherbyGstStateName.Text = stateName1;
                            }

                            string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + "   where Ac_Code=" + txtGETPASS_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string stateName = "";
                            if (gststatecode.Trim() != string.Empty && gststatecode != "0")
                            {
                                stateName = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + gststatecode + "");
                            }
                            txtGetpassGstStateCode.Text = gststatecode;
                            lbltxtGetpassGstStateName.Text = stateName;
                            setFocusControl(txtGetpassGstStateCode);
                        }
                        else
                        {
                            txtGETPASS_CODE.Text = string.Empty;
                            LBLGETPASS_NAME.Text = getPassName;
                            setFocusControl(txtGETPASS_CODE);
                        }
                    }
                }
                else
                {
                    LBLGETPASS_NAME.Text = "";
                    setFocusControl(txtGETPASS_CODE);
                }
            }
            if (strTextBox == "txtvoucher_by")
            {
                string vByName = "";
                string voucherbycitycode = "";
                string voucherbycity = "";
                string distance = "";
                if (txtvoucher_by.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtvoucher_by.Text);
                    if (a == false)
                    {
                        btntxtvoucher_by_Click(this, new EventArgs());
                    }
                    else
                    {
                        string aa = txtvoucher_by.Text;
                        char[] ch = aa.ToCharArray();
                        for (int i = 0; i < aa.Length; i++)
                        {
                            i = 0;
                            string sub = ch[i].ToString();
                            if (sub == "0")
                            {
                                aa = aa.Remove(i, 1);
                                ch = aa.ToCharArray();
                            }
                            else
                            {
                                break;
                            }
                        }
                        txtvoucher_by.Text = aa;
                        //vByName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        DataSet ds = clsDAL.SimpleQuery("select * from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                vByName = dt.Rows[0]["Ac_Name_E"].ToString();
                                voucherbycitycode = dt.Rows[0]["City_Code"].ToString();
                                voucherbycity = dt.Rows[0]["cityname"].ToString();
                                distance = dt.Rows[0]["Distance"].ToString();
                                hdnfst.Value = dt.Rows[0]["accoid"].ToString();
                                hdnfsupplieshortname.Value = dt.Rows[0]["Short_Name"].ToString();
                            }
                        }

                        if (vByName != string.Empty && vByName != "0")
                        {
                            lblVoucherLedgerByBalance.Text = AcBalance(txtvoucher_by.Text);
                            //string voucherbycitycode = clsCommon.getString("Select City_Code from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            // string voucherbycity = clsCommon.getString("Select city_name_e from " + tblPrefix + "CityMaster   where city_code=" + voucherbycitycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            //string distance = clsCommon.getString("Select Distance from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            lblvoucherbyname.Text = vByName + ", " + voucherbycity;
                            txtDistance.Text = distance;
                            //txtDistance.Enabled = false;


                            if (txtPurcOrder.Text != "1" && txtPurcOrder.Text.Trim() != string.Empty)
                            {
                                calculation();
                            }
                            else
                            {
                                int csale = txtcarporateSale.Text != string.Empty ? Convert.ToInt32(txtcarporateSale.Text) : 0;
                                if (csale == 0)
                                {
                                    txtCommission.Text = clsCommon.getString("select ISNULL(Commission,0) from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                }

                                calculation();
                            }

                            string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + "   where Ac_Code=" + txtvoucher_by.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string stateName = "";
                            if (gststatecode.Trim() != string.Empty && gststatecode != "0")
                            {
                                stateName = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + gststatecode + "");
                            }
                            txtVoucherbyGstStateCode.Text = gststatecode;
                            lbltxtVoucherbyGstStateName.Text = stateName;

                            setFocusControl(txtSaleBillTo);

                            // string selfac = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            if (txtGETPASS_CODE.Text == Session["SELF_AC"].ToString() && Session["SELF_AC"].ToString() != "0")
                            {
                                //txtSALE_RATE.Text = txtmillRate.Text;
                                calculation();
                            }

                            if (txtvoucher_by.Text == Session["SELF_AC"].ToString())
                            {
                                if (txtSaleBillTo.Text == string.Empty)
                                {
                                    txtNARRATION4.Text = lblvoucherbyname.Text;
                                    txtSaleBillTo.Text = txtvoucher_by.Text;
                                    txtSalebilltoGstStateCode.Text = txtVoucherbyGstStateCode.Text;
                                    lbltxtSalebilltoGstStateName.Text = lbltxtVoucherbyGstStateName.Text;
                                }
                                //txtNARRATION4.Text = "";
                                // txtSaleBillTo.Text = "";
                                // txtSalebilltoGstStateCode.Text = "";
                                // lbltxtSalebilltoGstStateName.Text = "";
                            }
                            if (txtvoucher_by.Text != Session["SELF_AC"].ToString() && txtcarporateSale.Text == string.Empty)
                            {
                                if (txtSaleBillTo.Text == string.Empty || txtSaleBillTo.Text == "2")
                                {
                                    txtNARRATION4.Text = lblvoucherbyname.Text;
                                    txtSaleBillTo.Text = txtvoucher_by.Text;
                                    txtSalebilltoGstStateCode.Text = txtVoucherbyGstStateCode.Text;
                                    lbltxtSalebilltoGstStateName.Text = lbltxtVoucherbyGstStateName.Text;
                                }
                                else
                                {
                                    //{
                                    //    txtNARRATION4.Text = lblvoucherbyname.Text;
                                    //    txtSaleBillTo.Text = txtvoucher_by.Text;
                                    //    txtSalebilltoGstStateCode.Text = txtVoucherbyGstStateCode.Text;
                                    //    lbltxtSalebilltoGstStateName.Text = lbltxtVoucherbyGstStateName.Text;
                                }

                            }


                            if (txtGETPASS_CODE.Text == Session["SELF_AC"].ToString() && txtvoucher_by.Text == Session["SELF_AC"].ToString())
                            {
                                txtSALE_RATE.Text = txtmillRate.Text;
                            }
                            if (drpDeliveryType.SelectedValue == "C")
                            {
                                if (drpDOType.SelectedValue != "DO" && txtSaleBillTo.Text == string.Empty && txtGETPASS_CODE.Text == Session["SELF_AC"].ToString())
                                {
                                    txtNARRATION4.Text = lblvoucherbyname.Text;
                                    txtSaleBillTo.Text = txtvoucher_by.Text;
                                    txtSalebilltoGstStateCode.Text = txtVoucherbyGstStateCode.Text;
                                    lbltxtSalebilltoGstStateName.Text = lbltxtVoucherbyGstStateName.Text;
                                }
                            }
                            string id = clsCommon.getString("select accoid from " + qryAccountList + "  where Ac_Code=" + txtSaleBillTo.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfsb.Value = id;
                            setFocusControl(txtSaleBillTo);



                        }
                        else
                        {
                            txtvoucher_by.Text = string.Empty;
                            lblvoucherbyname.Text = vByName;
                            setFocusControl(txtvoucher_by);
                        }
                    }
                }
                else
                {
                    lblvoucherbyname.Text = "";
                    setFocusControl(txtGETPASS_CODE);
                }
            }

            if (strTextBox == "txtVasuliAc")
            {
                string vByName = "";
                if (txtVasuliAc.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtVasuliAc.Text);
                    if (a == false)
                    {
                        btntxtVasuliAc_Click(this, new EventArgs());
                    }
                    else
                    {
                        vByName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "  where Ac_Code=" + txtVasuliAc.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfva.Value = clsCommon.getString("select isnull(accoid,0) as id from " + qryAccountList + "  where Ac_Code=" + txtVasuliAc.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (vByName != string.Empty && vByName != "0")
                        {
                            string tooltip = vByName;
                            if (vByName.Length > 25)
                            {
                                vByName = vByName.Substring(0, vByName.Length - 25);
                            }

                            lbltxtVasuliAc.Text = vByName;
                            lbltxtVasuliAc.ToolTip = tooltip;

                            setFocusControl(btnOpenDetailsPopup);
                        }
                        else
                        {
                            txtVasuliAc.Text = string.Empty;
                            lbltxtVasuliAc.Text = vByName;
                            setFocusControl(btnOpenDetailsPopup);
                        }
                    }
                }
                else
                {
                    lbltxtVasuliAc.Text = "";
                    //setFocusControl(txtVasuliAc);
                }
            }
            if (strTextBox == "txtGRADE")
            {
                setFocusControl(txtquantal);
            }

            if (strTextBox == "txtmillRate")
            {
                //txtPACKING.Text = "50";
                setFocusControl(txtSALE_RATE);
            }
            if (strTextBox == "txtSALE_RATE")
            {
                setFocusControl(txtCashDiff);
            }

            if (strTextBox == "txtDIFF_AMOUNT")
            {
                setFocusControl(txtFrieght);
            }
            if (strTextBox == "txtquantal")
            {
                setFocusControl(txtmillRate);
            }
            if (strTextBox == "txtwithGst_Amount")
            {

                double millamount = txtmillRate.Text != string.Empty ? Convert.ToDouble(txtmillRate.Text) : 0.00;
                double tax = 5.00;
                double result = 0.00;
                tax = tax / 100;
                result = millamount * tax;
                //txtwithGst_Amount.Text = result.ToString();
                txtwithGst_Amount.Text = (millamount + result).ToString();
            }
            if (strTextBox == "txtPACKING")
            {
                setFocusControl(txtBAGS);
            }
            if (strTextBox == "txtexcise_rate")
            {
                setFocusControl(txtFrieght);
            }
            if (strTextBox == "txtFrieght")
            {
                setFocusControl(txtMemoAdvanceRate);
            }
            if (strTextBox == "txtVasuliRate")
            {
                setFocusControl(txtVasuliRate1);
            }
            if (strTextBox == "txtVasuliRate1")
            {
                setFocusControl(txtVasuliAc);
            }
            if (strTextBox == "txtMemoAdvance")
            {
                setFocusControl(txtMemoGSTRate);
            }
            if (strTextBox == "txtexcise_rate")
            {
                setFocusControl(txtmillRate);
            }
            if (strTextBox == "txtDO_CODE")
            {
                string doname = "";
                if (txtDO_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtDO_CODE.Text);
                    if (a == false)
                    {
                        btntxtDO_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        doname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "   where Ac_Code=" + txtDO_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfdocd.Value = clsCommon.getString("select isnull(accoid,0) as id from " + qryAccountList + "  where Ac_Code=" + txtDO_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (doname != string.Empty && doname != "0")
                        {
                            LBLDO_NAME.Text = doname;
                            setFocusControl(txtMillEwayBill_No);
                        }
                        else
                        {
                            txtDO_CODE.Text = "0";
                            LBLDO_NAME.Text = doname;
                            setFocusControl(txtMillEwayBill_No);
                        }
                    }
                }
                else
                {
                    LBLDO_NAME.Text = "";
                    setFocusControl(txtMillEwayBill_No);
                }
            }
            if (strTextBox == "txtBroker_CODE")
            {
                string brokerName = "";
                if (txtBroker_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBroker_CODE.Text);
                    if (a == false)
                    {
                        btntxtBroker_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        brokerName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "   where Ac_Code=" + txtBroker_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfbk.Value = clsCommon.getString("select isnull(accoid,0) as id from " + qryAccountList + "  where Ac_Code=" + txtBroker_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (brokerName != string.Empty && brokerName != "0")
                        {

                            LBLBROKER_NAME.Text = brokerName;
                            setFocusControl(txtDistance);
                        }
                        else
                        {
                            txtBroker_CODE.Text = string.Empty;
                            LBLBROKER_NAME.Text = brokerName;
                            setFocusControl(txtDistance);
                        }
                    }
                }
                else
                {
                    LBLBROKER_NAME.Text = "";
                    setFocusControl(txtDistance);
                }
            }
            if (strTextBox == "txtTruck_NO")
            {
                setFocusControl(txtDriverMobile);
            }
            if (strTextBox == "txtDriverMobile")
            {
                setFocusControl(txtTRANSPORT_CODE);
            }
            if (strTextBox == "txtTRANSPORT_CODE")
            {
                string transportName = "";
                if (txtTRANSPORT_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtTRANSPORT_CODE.Text);
                    if (a == false)
                    {
                        btntxtTRANSPORT_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        transportName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "  where Ac_Code=" + txtTRANSPORT_CODE.Text + " and Ac_type='T' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string transportshortname = "";
                        transportshortname = clsCommon.getString("select Short_Name from " + qryAccountList + "  where Ac_Code=" + txtTRANSPORT_CODE.Text + " and Ac_type='T' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnftransportshortname.Value = transportshortname;

                        hdnftc.Value = clsCommon.getString("select isnull(accoid,0) as id from " + qryAccountList + "  where Ac_Code=" + txtTRANSPORT_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


                        if (transportName != string.Empty && transportName != "0")
                        {
                            LBLTRANSPORT_NAME.Text = transportName;
                            string gststatecode1 = clsCommon.getString("select GSTStateCode from " + qryAccountList + "  where Ac_Code=" + txtTRANSPORT_CODE.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string stateName1 = "";
                            if (gststatecode1.Trim() != string.Empty)
                            {
                                stateName1 = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + gststatecode1 + "");
                            }
                            txtTransportGstStateCode.Text = gststatecode1;
                            lbltxtTransportGstStateCode.Text = stateName1;
                            setFocusControl(txtFrieght);
                        }
                        else
                        {
                            txtTRANSPORT_CODE.Text = string.Empty;
                            LBLTRANSPORT_NAME.Text = transportName;
                            setFocusControl(txtFrieght);
                        }
                    }
                }
                else
                {
                    LBLTRANSPORT_NAME.Text = "";
                    setFocusControl(txtFrieght);
                }
            }
            if (strTextBox == "txtNARRATION1")
            {
                setFocusControl(txtNARRATION2);
            }
            if (strTextBox == "txtNARRATION2")
            {
                setFocusControl(txtNARRATION3);
            }
            if (strTextBox == "txtNARRATION3")
            {
                setFocusControl(txtNARRATION4);
            }
            if (strTextBox == "txtNARRATION4")
            {
                if (hdnfClosePopup.Value.Trim() == "Close")
                {
                    if (txtSaleBillTo.Text.Trim() != string.Empty)
                    {
                        string gststatecode = clsCommon.getString("select GSTStateCode from " + qryAccountList + "  where Ac_Code=" + txtSaleBillTo.Text + " and Ac_type!='B' and Ac_type!='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        string stateName = "";
                        if (gststatecode.Trim() != string.Empty)
                        {
                            stateName = clsCommon.getString("select State_Name from GSTStateMaster  where State_Code=" + gststatecode + "");
                        }
                        txtSalebilltoGstStateCode.Text = gststatecode;
                        lbltxtSalebilltoGstStateName.Text = stateName;
                    }
                }

                setFocusControl(btnOpenDetailsPopup);
            }
            if (strTextBox == "txtBANK_CODE")
            {
                string bankName = "";
                if (txtBANK_CODE.Text != string.Empty)
                {
                    bankName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "  where Ac_Code=" + txtBANK_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (bankName != string.Empty && bankName != "0")
                    {

                        lblBank_name.Text = bankName;
                        setFocusControl(btntxtUTRNo);
                    }
                    else
                    {
                        txtBANK_CODE.Text = string.Empty;
                        lblBank_name.Text = bankName;
                        setFocusControl(txtBANK_CODE);
                    }
                }
                else
                {
                    lblBank_name.Text = "";
                    setFocusControl(txtBANK_CODE);
                }


            }
            if (strTextBox == "txtTDSAc")
            {
                string bankName = "";
                if (txtTDSAc.Text != string.Empty)
                {
                    bankName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "  where Ac_Code=" + txtTDSAc.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (bankName != string.Empty && bankName != "0")
                    {
                        string id = clsCommon.getString("select accoid from " + qryAccountList + "  where Ac_Code=" + txtTDSAc.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        lblTDSAcname.Text = bankName;
                        hdnfTDSAcid.Value = id;
                        setFocusControl(txttdsrate);
                    }
                    else
                    {
                        txtTDSAc.Text = string.Empty;
                        lblTDSAcname.Text = bankName;
                        hdnfTDSAcid.Value = "0";
                        setFocusControl(txtVasuliRate);
                    }
                }
                else
                {
                    lblBank_name.Text = "";
                    setFocusControl(txtVasuliRate);
                }


            }
            if (strTextBox == "txtCashDiffAc")
            {
                string bankName = "";
                if (txtCashDiffAc.Text != string.Empty)
                {
                    bankName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + "  where Ac_Code=" + txtCashDiffAc.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (bankName != string.Empty && bankName != "0")
                    {
                        string id = clsCommon.getString("select accoid from " + qryAccountList + "  where Ac_Code=" + txtCashDiffAc.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        lblCashDiffAcname.Text = bankName;
                        hdnfCashDiffAcid.Value = id;
                        setFocusControl(txtTruck_NO);
                    }
                    else
                    {
                        txtCashDiffAc.Text = string.Empty;
                        lblCashDiffAcname.Text = bankName;
                        setFocusControl(txtCashDiffAc);
                    }
                }
                else
                {
                    lblBank_name.Text = "";
                    setFocusControl(txtCashDiffAc);
                }

            }
            if (strTextBox == "txtNARRATION")
            {
                setFocusControl(txtBANK_AMOUNT);
            }
            if (strTextBox == "txtBANK_AMOUNT")
            {
                setFocusControl(btnAdddetails);
            }

            if (strTextBox == "txtcarporateSale")
            {
                carporatesale();
            }

            if (strTextBox == "txtnewsbdate")
            {
                if (txtnewsbdate.Text != string.Empty)
                {
                    try
                    {
                        if (clsCommon.isValidDate(txtnewsbdate.Text) == true)
                        {
                            setFocusControl(txtMILL_CODE);
                        }
                        else
                        {
                            txtnewsbdate.Text = string.Empty;
                            setFocusControl(txtnewsbdate);
                        }
                    }
                    catch (Exception exx)
                    {
                        txtNARRATION1.Text = exx.Message;
                        txtnewsbdate.Text = string.Empty;
                        setFocusControl(txtnewsbdate);
                    }
                }
                else
                {
                    setFocusControl(txtnewsbdate);
                }

                return;
            }
            //calculation();
            //  hdnfTCSRate.Value = txtTCSRate_Sale.Text;
            if (btnSave.Text == "Save")
            {
                AmtCalculation();
            }
            // GstRateAutoId();



        }
        catch
        {
        }
    }

    private void memorgstratecal()
    {
        string gstrate = clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster   where Doc_No=" + txtMemoGSTRate.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //  lblMemoGSTRate.Text = gstrate;
        double rate = Convert.ToDouble(gstrate);
        hdnfmemogstrate.Value = rate.ToString();
        hdnfmemocgst.Value = (rate / 2).ToString();
        hdnfmemosgst.Value = (rate / 2).ToString();
        hdnfmemoigst.Value = (rate).ToString();

    }

    private void GSTCalculations()
    {
        try
        {
            if (txtGstRate.Text.Trim() != string.Empty)
            {
                string gstrate = clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster   where Doc_No=" + txtGstRate.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                double rate = Convert.ToDouble(gstrate);

                double salerate = txtSALE_RATE.Text != string.Empty ? Convert.ToDouble(txtSALE_RATE.Text) : 0.0;
                double millrate = txtmillRate.Text != string.Empty ? Convert.ToDouble(txtmillRate.Text) : 0.0;

                double taxAmountOnSR = (salerate * (rate / 100));
                double gstExSaleRate = Math.Round(Math.Abs((salerate / (salerate + taxAmountOnSR) * salerate)), 2);
                double gstRateAmountOnSR = Math.Round(Math.Abs(salerate - gstExSaleRate), 2);

                //txtGstSRAmount.Text = gstRateAmountOnSR.ToString();
                //txtGstExSaleRate.Text = gstExSaleRate.ToString();

                double taxAmountOnMR = (millrate * (rate / 100));
                double gstExMillRate = Math.Round(Math.Abs((millrate / (millrate + taxAmountOnMR) * millrate)), 2);
                double gstRateAmountOnMR = Math.Round(Math.Abs(millrate - gstExMillRate), 2);

                //txtGstMRAmount.Text = gstRateAmountOnMR.ToString();
                //txtGstExMillRate.Text = gstExMillRate.ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void carporatesale()
    {
        if (txtcarporateSale.Text != string.Empty)
        {
            bool a = clsCommon.isStringIsNumeric(txtcarporateSale.Text);
            if (a == false)
            {
                btntxtcarporateSale_Click(this, new EventArgs());
            }
            else
            {

                if (txtcarporateSale.Text == "0")
                {
                    txtcarporateSale.Text = string.Empty;
                    setFocusControl(drpDOType);
                    return;
                }
                qry = "select ac_code as Ac_Code,carporatepartyaccountname as partyName,carporatepartyunitname as Unit_name,Unit_Code,carporatepartyunitname as UnitName, " +
                    " broker as BrokerCode,carporatepartybrokername as BrokerName,sell_rate as Sale_Rate,pono as Po_Details,balance,selling_type as SellingType, " +
                    " bill_to,carporatebilltoname,CommissionRate from qrycarporatedobalance  where Doc_No=" + txtcarporateSale.Text + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                DataSet ds = new DataSet();
                ds = clsDAL.SimpleQuery(qry);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                string sellingType = dt.Rows[0]["SellingType"].ToString();
                lblCSYearCode.Text = sellingType;
                hdnfPDS.Value = lblCSYearCode.Text;
                //string selfac = clsCommon.getString("Select SELF_AC from " + tblPrefix + "CompanyParameters   where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Session["year"].ToString() + "");
                if (sellingType == "C")
                {
                    string getvocname = clsCommon.getString("Select Ac_Name_E from qrymstaccountmaster   where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + Session["SELF_AC"].ToString() + "");

                    txtGETPASS_CODE.Text = Session["SELF_AC"].ToString(); //dt.Rows[0]["Unit_name"].ToString();
                    LBLGETPASS_NAME.Text = getvocname; //dt.Rows[0]["UnitName"].ToString();
                    string partycode = dt.Rows[0]["Unit_Code"].ToString();
                    string partyname = dt.Rows[0]["Unit_name"].ToString();
                    string getpassstatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster   where Ac_Code=" + Session["SELF_AC"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    string getpasstatename = "";
                    if (getpassstatecode.Trim() != string.Empty && getpassstatecode != "0")
                    {
                        getpasstatename = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + getpassstatecode + "");
                        txtGetpassGstStateCode.Text = getpassstatecode;
                        lbltxtGetpassGstStateName.Text = getpasstatename;
                    }

                    txtvoucher_by.Text = partycode;
                    lblvoucherbyname.Text = partyname;
                    txtSaleBillTo.Text = dt.Rows[0]["Ac_Code"].ToString();
                    txtNARRATION4.Text = dt.Rows[0]["partyName"].ToString();
                    string partystatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster  where Ac_Code=" + partycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    string stateName1 = "";
                    if (partystatecode.Trim() != string.Empty && partystatecode != "0")
                    {
                        stateName1 = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + partystatecode + "");
                        txtVoucherbyGstStateCode.Text = partystatecode;
                        lbltxtVoucherbyGstStateName.Text = stateName1;

                        txtSalebilltoGstStateCode.Text = partystatecode;
                        lbltxtSalebilltoGstStateName.Text = stateName1;
                    }
                }
                else
                {
                    string getvocname = clsCommon.getString("Select Ac_Name_E from qrymstaccountmaster  where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + Session["SELF_AC"].ToString() + "");
                    txtGETPASS_CODE.Text = Session["SELF_AC"].ToString();
                    LBLGETPASS_NAME.Text = getvocname;
                    string getpassstatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where   Ac_Code=" + Session["SELF_AC"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    string getpasstatename = "";
                    if (getpassstatecode.Trim() != string.Empty && getpassstatecode != "0")
                    {
                        getpasstatename = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + getpassstatecode + "");
                        txtGetpassGstStateCode.Text = getpassstatecode;
                        lbltxtGetpassGstStateName.Text = getpasstatename;
                    }
                    string partycode = dt.Rows[0]["Ac_Code"].ToString();
                    string partyname = dt.Rows[0]["partyName"].ToString();
                    hdnfPDSPartyCode.Value = partycode;
                    hdnfPDSUnitCode.Value = dt.Rows[0]["Unit_Code"].ToString();
                    lblPDSParty.Text = "Party: " + dt.Rows[0]["partyName"].ToString();
                    txtvoucher_by.Text = partycode;
                    lblvoucherbyname.Text = partyname;

                    txtSaleBillTo.Text = partycode;
                    txtNARRATION4.Text = partyname;

                    txtVoucherbyGstStateCode.Text = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where   Ac_Code=" + partycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString() + ""));
                    lbltxtVoucherbyGstStateName.Text = clsCommon.getString("select State_Name from GSTStateMaster   where State_Code=" + txtSalebilltoGstStateCode.Text + "");

                    txtSalebilltoGstStateCode.Text = txtVoucherbyGstStateCode.Text;
                    lbltxtSalebilltoGstStateName.Text = lbltxtVoucherbyGstStateName.Text;
                }


                lblPoDetails.Text = "PO Details:- " + dt.Rows[0]["Po_Details"].ToString();
                txtBroker_CODE.Text = dt.Rows[0]["BrokerCode"].ToString();
                LBLBROKER_NAME.Text = dt.Rows[0]["BrokerName"].ToString();
                if (ViewState["mode"].ToString() == "I")
                {
                    txtquantal.Text = dt.Rows[0]["balance"].ToString();
                    txtSALE_RATE.Text = dt.Rows[0]["Sale_Rate"].ToString();
                }

                txtBill_To.Text = dt.Rows[0]["bill_to"].ToString();
                lblBill_To.Text = dt.Rows[0]["carporatebilltoname"].ToString();

                string type = clsCommon.getString("select [DeliveryType] from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_Code=" + Session["Company_Code"].ToString());

                drpDeliveryType.SelectedValue = type;

                if (type != "C")
                {
                    txtCommission.Enabled = false;
                }
                else
                {
                    txtCommission.Enabled = true;
                    txtCommission.Text = dt.Rows[0]["CommissionRate"].ToString();
                }
                drpDeliveryType.Enabled = false;
                ddlFrieghtType.SelectedValue = "O";
                ddlFrieghtType.Enabled = false;
                drpDOType.SelectedValue = "DI";
                drpDOType.Enabled = false;
                setFocusControl(txtMILL_CODE);


                if (txtGETPASS_CODE.Text != string.Empty)
                {
                    hdnfgp.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtGETPASS_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                }

                if (txtvoucher_by.Text != string.Empty)
                {
                    hdnfst.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtvoucher_by.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                }

                if (txtSaleBillTo.Text != string.Empty)
                {
                    try
                    {
                        hdnfsb.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtSaleBillTo.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                    }
                    catch { }
                }


                try
                {

                    hdnfcscode.Value = clsCommon.getString("select  isnull(carpid,0) as carpid from carporatehead where doc_no=" + txtcarporateSale.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                }
                catch
                {

                }

                if (txtBroker_CODE.Text != string.Empty)
                {
                    hdnfbk.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + txtBroker_CODE.Text + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                }


                Bill_To = Convert.ToInt32(txtBill_To.Text != string.Empty ? txtBill_To.Text : "0");
                if (txtBill_To.Text != string.Empty && txtBill_To.Text != "0")
                {
                    hdnfbt.Value = clsCommon.getString("select  isnull(accoid,0) as accoid from " + qryAccountList + " where Ac_code=" + Bill_To + " and Company_code='" + Session["Company_Code"].ToString() + "'");
                }
                else
                {
                    hdnfbt.Value = "0";
                }

                #region
                hdnfpdsacID.Value = clsCommon.getString("select isnull(accoid,0) as id from nt_1_accountmaster where Ac_Code=" + hdnfPDSPartyCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                hdnfpdsunitID.Value = clsCommon.getString("select isnull(accoid,0) as id from nt_1_accountmaster where Ac_Code=" + hdnfPDSUnitCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                hdnfpdspartyStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + hdnfPDSPartyCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                hdnfpdsunitStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + hdnfPDSUnitCode.Value + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                hdnfbilltoStateCode.Value = clsCommon.getString("select isnull(GSTStateCode,0) from nt_1_accountmaster  where Ac_Code=" + txtBill_To.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                DataSet dsi = clsDAL.SimpleQuery("select Purchase_AC,Sale_AC,PurcAcid,SaleAcid from qrymstitem where System_Type='I' and System_Code=" + txtitem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                if (dsi != null)
                {
                    if (dsi.Tables[0].Rows.Count > 0)
                    {
                        Session["PurchAc"] = dsi.Tables[0].Rows[0]["Purchase_AC"].ToString();
                        Session["SaleAc"] = dsi.Tables[0].Rows[0]["Sale_AC"].ToString();
                        Session["Purcid"] = dsi.Tables[0].Rows[0]["PurcAcid"].ToString();
                        Session["Saleid"] = dsi.Tables[0].Rows[0]["SaleAcid"].ToString();
                    }

                }
                if (hdnfPDSUnitCode.Value != "0")
                {
                    hdnfUnitCity.Value = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + hdnfPDSUnitCode.Value + " and  Company_Code=" + Session["Company_Code"].ToString() + "");
                }
                else
                {
                    hdnfUnitCity.Value = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + txtvoucher_by.Text + " and  Company_Code=" + Session["Company_Code"].ToString() + "");

                }
                #endregion

                //GstRateAutoId();
                //}
                //else
                //{
                //    txtcarporateSale.Text = "";
                //    lblCSYearCode.Text = "";
                //    setFocusControl(txtcarporateSale);
                //}
                setFocusControl(txtMILL_CODE);
            }

            setFocusControl(txtMILL_CODE);
        }
        else
        {
            txtcarporateSale.Text = "";
            lblCSYearCode.Text = "";
            setFocusControl(txtMILL_CODE);
        }
    }

    private void calculation()
    {
        try
        {
            //  hdnfTCSRate.Value = txtTCSRate_Sale.Text;
            if (btnSave.Text == "Save")
            {
                AmtCalculation();
            }
            double qt = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtquantal.Text != string.Empty ? txtquantal.Text : "0.00")));
            //double qt = Convert.ToString(Math.Abs(Convert.ToDouble(txtquantal.Text))) != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0.00;
            double tenderCommission = txtCommission.Text != string.Empty ? Convert.ToDouble(txtCommission.Text) : 0.00;
            double qtl = Math.Abs(qt);
            Int32 packing = Convert.ToInt32("0" + txtPACKING.Text != string.Empty ? txtPACKING.Text : "0");
            Int32 bags = 0;
            double saleRate = 0.00;
            double actualSaleRate = Convert.ToDouble("0" + txtSALE_RATE.Text != string.Empty ? txtSALE_RATE.Text : "0");
            double commision = Convert.ToDouble("0" + txtCommission.Text);
            saleRate = actualSaleRate + commision;
            double millRate = Convert.ToDouble("0" + txtmillRate.Text);
            double purchaseRate = Convert.ToDouble("0" + txtpurchaserate.Text);
            double gstRate = Convert.ToDouble("0" + txtexcise_rate.Text);

            double mill_amount = double.Parse("0" + lblMillAmount.Text);
            double diffAmt = 0.00;
            double diff = 0.00;
            double frieght = Convert.ToDouble("0" + txtFrieght.Text);
            if (drpDeliveryType.SelectedValue == "A")
            {
                saleRate = saleRate - frieght;
            }
            double vasuli_rate = Convert.ToDouble("0" + txtVasuliRate.Text);
            double frieght_amount = Convert.ToDouble("0" + txtFrieghtAmount.Text);
            double vasuli_amount = Convert.ToDouble("0" + txtVasuliAmount.Text);
            var vr1 = txtVasuliRate1.Text != string.Empty ? Convert.ToString(txtVasuliRate1.Text) : "0";
            var fmt = new NumberFormatInfo();
            fmt.NegativeSign = "-";
            var number = double.Parse(vr1, fmt);
            double vasuli_rate1 = number;// Convert.ToDouble("0" + txtVasuliRate1.Text);
            var va1 = txtVasuliAmount1.Text != string.Empty ? Convert.ToString(txtVasuliAmount1.Text) : "0";

            var va1amount = double.Parse(va1, fmt);
            double vasuli_amount1 = va1amount;//Convert.ToDouble("0" + txtVasuliAmount1.Text);
            double PurcTCSRate = txtTCSRate.Text != string.Empty ? Convert.ToDouble(txtTCSRate.Text) : 0.000;

            double PurcTDSRate = txtPurchaseTDS.Text != string.Empty ? Convert.ToDouble(txtPurchaseTDS.Text) : 0.000;
            double mill_amountTDS = 0.00;

            double mill_amountTCS = 0.00;
            double MillAmttTCS = 0.00;

            double TCS_Rate = 0.000;
            double TCS_Amt = 0.00;
            double Net_Payable_Amt = 0.00;
            string TCS = "0.000";
            //string TCS1 = clsCommon.getString("select TCS from " + tblPrefix + "CompanyParameters with (nolock)  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
            string TCS1 = Session["TCSRate"].ToString();
            if (TCS1 == txtTCSRate.Text)
            {
                TCS = TCS1;
            }
            else
            {
                if (txtTCSRate.Text == string.Empty)
                {
                    TCS = "0.000";
                }
                else
                {
                    TCS = txtTCSRate.Text;
                }
            }
            if (qtl != 0 && packing != 0)
            {
                bags = Convert.ToInt32((qtl / packing) * 100);
                txtBAGS.Text = bags.ToString();
            }
            else
            {
                txtBAGS.Text = bags.ToString();
            }

            if (saleRate != 0 && purchaseRate != 0)
            {
                hdnfSaleRate.Value = Convert.ToString(saleRate);
                diff = saleRate - purchaseRate;
                diffAmt = Math.Round(diff * qtl, 2);
                //mill_amount = qtl * Math.Round((purchaseRate + gstRate), 2);
                mill_amount = qtl * Math.Round((purchaseRate + gstRate), 2);

                mill_amountTCS = Math.Round((mill_amount * PurcTCSRate) / 100, 2);

                mill_amountTDS = Math.Round(((purchaseRate * qtl) * PurcTDSRate) / 100, 2);

                MillAmttTCS = (mill_amount + mill_amountTCS) - mill_amountTDS;

            }

            lblDiffrate.Text = diff.ToString();
            txtDIFF_AMOUNT.Text = diffAmt.ToString();
            //lblMillAmount.Text = mill_amount.ToString();
            lblMillAmount.Text = mill_amount.ToString();
            lblMillAmtWOTCS.Text = MillAmttTCS.ToString();
            if (mill_amount != 0 && TCS != "0")
            {
                TCS_Rate = Convert.ToDouble(TCS);
                TCS_Amt = Math.Round(((mill_amount * TCS_Rate) / 100), 2);
                Net_Payable_Amt = Math.Round((mill_amount + TCS_Amt), 2);
            }
            txtTCSRate.Text = TCS;
            // txtTCSAmt.Text = TCS_Amt.ToString();
            // txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();

            if (qtl != 0 && frieght != 0)
            {
                frieght_amount = qtl * frieght;
                txtFrieghtAmount.Text = frieght_amount.ToString();
                //txtMemoAdvance.Text = frieght_amount.ToString();
            }
            else
            {
                frieght_amount = 0.00;
                txtFrieghtAmount.Text = frieght_amount.ToString();
                //txtMemoAdvance.Text = frieght_amount.ToString();
            }

            if (qtl != 0 && vasuli_rate != 0)
            {
                vasuli_amount = qtl * vasuli_rate;
                txtVasuliAmount.Text = vasuli_amount.ToString();
            }
            else
            {
                vasuli_amount = 0.00;
                txtVasuliAmount.Text = vasuli_amount.ToString();
            }


            double cashdiff = 0.00;
            double cashdiffvalue1 = 0.00;
            cashdiffvalue1 = Convert.ToDouble(txtCashDiff.Text.Trim() != string.Empty ? Convert.ToDouble(txtCashDiff.Text) : 0);
            cashdiff = (saleRate - cashdiffvalue1) * qtl;
            lblCashdiffvalue.Text = cashdiff.ToString();


            TDSCAl();
            if (Edit_Sale_Rate == "Y")
            {
                if (drpDeliveryType.SelectedValue == "C" || txtcarporateSale.Text != "0")
                {
                    if (txtPurcOrder.Text != "1")
                    {
                        txtSALE_RATE.Enabled = true;
                    }
                    else
                    {
                        txtSALE_RATE.Enabled = true;
                    }
                }
                else
                {
                    txtSALE_RATE.Enabled = true;
                }
            }
            else {
                if (drpDeliveryType.SelectedValue == "C" || txtcarporateSale.Text != "0")
                {
                    if (txtPurcOrder.Text != "1")
                    {
                        txtSALE_RATE.Enabled = false;
                    }
                    else
                    {
                        txtSALE_RATE.Enabled = true;
                    }
                }
                else
                {
                    txtSALE_RATE.Enabled = true;
                }
            }
            #region ---Add default row in grid---
            if (ViewState["currentTable"] == null)
            {
                if (drpDOType.SelectedValue == "DI")
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add((new DataColumn("ID", typeof(Int32))));
                    #region [Write here columns]
                    dt1.Columns.Add((new DataColumn("Type", typeof(string))));
                    dt1.Columns.Add((new DataColumn("Bank_Code", typeof(Int32))));
                    dt1.Columns.Add((new DataColumn("BankName", typeof(string))));
                    dt1.Columns.Add((new DataColumn("Narration", typeof(string))));
                    dt1.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt1.Columns.Add((new DataColumn("UTR_NO", typeof(string))));
                    dt1.Columns.Add((new DataColumn("LTNo", typeof(Int32))));
                    dt1.Columns.Add((new DataColumn("dodetailid", typeof(int))));

                    dt1.Columns.Add((new DataColumn("UtrDetailId", typeof(int))));
                    #endregion
                    dt1.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt1.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt1.Columns.Add(new DataColumn("bc", typeof(string)));
                    dt1.Columns.Add(new DataColumn("UtrYearCode", typeof(Int32)));
                    dt1.Columns.Add(new DataColumn("UtrCompanyCode", typeof(Int32)));

                    DataRow dr = dt1.NewRow();
                    dr["ID"] = 1;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 1;
                    dr["Type"] = "T";
                    Int32 Payment_To = 0;
                    dr["dodetailid"] = 1;
                    if (txtMILL_CODE.Text != string.Empty)
                    {
                        Payment_To = Convert.ToInt32(clsCommon.getString("Select Payment_To from qrytenderheaddetail where Mill_Code=" + txtMILL_CODE.Text +
                            " and Tender_No=" + txtPurcNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                        // " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())));
                        dr["Bank_Code"] = Payment_To;
                    }
                    else
                    {
                        dr["Bank_Code"] = "0";
                    }
                    if (LBLMILL_NAME.Text != string.Empty)
                    {
                        string Payment_To_Name = clsCommon.getString("Select paymenttoname from qrytenderheaddetail where Mill_Code=" + txtMILL_CODE.Text +
                            " and Tender_No=" + txtPurcNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        dr["BankName"] = Payment_To_Name;
                    }
                    else
                    {
                        dr["BankName"] = "";
                    }
                    dr["Narration"] = "Transfer Letter";
                    if (txtSaleTDS.Text != string.Empty && txtSaleTDS.Text != "0")
                    {
                        dr["Amount"] = Math.Round(mill_amount, 2);
                    }
                    else
                    {
                        dr["Amount"] = Math.Round(MillAmttTCS, 2);

                    }
                    dr["UTR_NO"] = txtUTRNo.Text != string.Empty ? int.Parse(txtUTRNo.Text) : 0;


                    dr["UtrDetailId"] = 0;
                    dr["LTNo"] = txtLT_No.Text != string.Empty ? int.Parse(txtLT_No.Text) : 0;
                    dr["bc"] = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + Payment_To + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
                    dr["UtrYearCode"] = 0;
                    dr["UtrCompanyCode"] = 0;
                    dt1.Rows.Add(dr);
                    ViewState["currentTable"] = dt1;
                    grdDetail.DataSource = dt1;
                    grdDetail.DataBind();
                }
            }
            else
            {
                DataTable dt1 = (DataTable)ViewState["currentTable"];
                DataRow dr = dt1.Rows[0];

                if (temp == "0" && drpDOType.SelectedValue == "DI")
                {
                    dr["ID"] = dt1.Rows[0]["ID"].ToString();
                    //dr["rowAction"] = "U";
                    dr["SrNo"] = 1;

                    dr["Type"] = "T";
                    Int32 Payment_To = 0;
                    if (txtMILL_CODE.Text != string.Empty)
                    {
                        Payment_To = Convert.ToInt32(clsCommon.getString("Select Payment_To from qrytenderheaddetail where Mill_Code=" + txtMILL_CODE.Text +
                            " and Tender_No=" + txtPurcNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                        //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString())));
                        dr["Bank_Code"] = Payment_To;
                    }
                    else
                    {
                        dr["Bank_Code"] = "0";
                    }
                    if (LBLMILL_NAME.Text != string.Empty)
                    {
                        string Payment_To_Name = clsCommon.getString("Select paymenttoname from qrytenderheaddetail where Mill_Code=" + txtMILL_CODE.Text +
                            " and Tender_No=" + txtPurcNo.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        dr["BankName"] = Payment_To_Name;
                    }
                    else
                    {
                        dr["BankName"] = "";
                    }
                    dr["Narration"] = dt1.Rows[0]["Narration"].ToString();
                    dr["LTNo"] = dt1.Rows[0]["LTNo"].ToString();
                    dr["bc"] = clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + Payment_To + " and Company_Code=" + Session["Company_Code"].ToString() + " ");

                    double millAmount = Math.Round(MillAmttTCS, 2);

                    if (dt1.Rows.Count == 1)
                    {
                        if (ViewState["mode"].ToString() == "I" || dt1.Rows[0]["UTR_NO"].ToString() == "0")
                        {
                            dt1.Rows[0]["Amount"] = millAmount;
                            if (ViewState["mode"].ToString() == "I")
                            {
                                dt1.Rows[0]["rowAction"] = "A";
                            }
                            else
                            {
                                dt1.Rows[0]["rowAction"] = "U";
                            }

                        }
                    }
                    if (dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows[0]["UTR_NO"].ToString() != "0")
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (dt1.Rows[i]["rowAction"].ToString() != "D")
                                {
                                    if (millAmount > Convert.ToDouble(dt1.Rows[i]["Amount"].ToString()))
                                    {
                                        millAmount = millAmount - Convert.ToDouble(dt1.Rows[i]["Amount"].ToString());
                                    }
                                    else
                                    {
                                        if (i < dt1.Rows.Count)
                                        {
                                            dt1.Rows[i]["Amount"] = millAmount;
                                            dt1.Rows[i]["rowAction"] = "U";

                                            for (int k = i; k < dt1.Rows.Count - 1; k++)
                                            {
                                                dt1.Rows[k + 1]["rowAction"] = "D";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #region comment
                    //dr["Amount"] = millAmount;
                    //string UTR_NO = dt1.Rows[0]["UTR_NO"].ToString();
                    //hdnfMainBankAmount.Value = millAmount.ToString();
                    //dr["UTR_NO"] = UTR_NO;
                    //if (dt1.Rows.Count > 1)
                    //{
                    //    dt1.Rows[0]["rowAction"] = "U";

                    //    for (int i = 0; i < dt1.Rows.Count; i++)
                    //    {
                    //        //if (i == 0)
                    //        //{
                    //        //    dt1.Rows[i + 1]["rowAction"] = "D";
                    //        //    //dt1.Rows[i + 1].Delete();
                    //        //    //dt1.AcceptChanges();
                    //        //}
                    //    }
                    //}
                    //if (ViewState["mode"].ToString() == "U")
                    //{
                    //    double thisSum = Convert.ToDouble(clsCommon.getString("select ISNULL(SUM(UsedAmt),0) from " + tblPrefix + "qryUTRBalance where doc_no=" + UTR_NO + " and mill_code=" + txtMILL_CODE.Text + " and DO_No=" + txtdoc_no.Text.Trim() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                    //    double utrSum = Convert.ToDouble(clsCommon.getString("select ISNULL(SUM(balance),0) from " + tblPrefix + "qryUTRBalance where doc_no=" + UTR_NO + " and mill_code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                    //    double utrBal = thisSum + utrSum;
                    //    if (millAmount > utrBal)
                    //    {
                    //        dt1.Rows[0]["Amount"] = utrBal;
                    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Mill Amount Is Greater You Need To Add Another Utr!');", true);
                    //    }
                    //}
                    #endregion

                    ViewState["currentTable"] = dt1;
                    grdDetail.DataSource = dt1;
                    grdDetail.DataBind();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
            #endregion
    }
    #endregion


    private void AmtCalculation()
    {
        #region oldCode
        ////DataTable dt = clsTCSTDScal.AmtCalculation(txtSaleBillTo.Text, paymenttotds, docdate, Convert.ToDouble(txtTCSRate_Sale.Text),
        ////    Convert.ToDouble(txtSaleTDS.Text), Convert.ToDouble(txtTCSRate.Text), Convert.ToDouble(txtPurchaseTDS.Text), 1, 2, hdnfSaleTDS.Value,
        ////    hdnfTCSRate.Value, hdnfPurchaseTDS.Value, hdnfTCSratepur.Value);
        //string docdate = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        ////txtSaleTDS.Text = Session["SaleTDSRate"].ToString();
        ////txtPurchaseTDS.Text = Session["PurchaseTDSRate"].ToString();
        //double Amount = 0.00;
        //double Amountf = 0.00;
        //string SaleBillTo = txtSaleBillTo.Text;
        //string Amt = string.Empty;
        //double SBBalAmt = 0.00;

        //double gstRate = Convert.ToDouble("0" + txtexcise_rate.Text);
        //double saleRate = 0.00;
        //double actualSaleRate = Convert.ToDouble("0" + txtSALE_RATE.Text);
        //double commision = Convert.ToDouble("0" + txtCommission.Text);
        //double qt = Convert.ToString(Math.Abs(Convert.ToDouble(txtquantal.Text))) != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0.00;
        //string SaleTDS = "";
        //string PurchaseTDS = "";

        //string paymenttotds = clsCommon.getString("select Payment_To from qrytenderheaddetail where Tender_No=" + txtPurcNo.Text
        //    + " and Company_Code=" + Session["Company_Code"].ToString() + " and ID=" + txtPurcOrder.Text + " and Year_Code=" + Session["year"].ToString());
        //SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
        //    + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        //PurchaseTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + paymenttotds
        //   + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        //double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);

        //if (SaleTDS == "Y")
        //{
        //    string tsd = Convert.ToString(txtSaleTDS.Text);
        //    if (tsd == hdnfSaleTDS.Value)
        //    {
        //        hdnfSaleTDS.Value = Session["SaleTDSRate"].ToString();
        //    }
        //    else
        //    {
        //        hdnfSaleTDS.Value = txtSaleTDS.Text;
        //    }
        //    // hdnfSaleTDS.Value = Session["SaleTDSRate"].ToString();

        //}
        //else
        //{
        //    hdnfSaleTDS.Value = "0.00";
        //    string tcssale = Convert.ToString(txtTCSRate_Sale.Text);
        //    if (tcssale == hdnfTCSRate.Value)
        //    {
        //        hdnfTCSRate.Value = Session["TCSRate"].ToString();
        //    }
        //    else
        //    {
        //        hdnfTCSRate.Value = txtTCSRate_Sale.Text;
        //    }
        //    //hdnf.Value = Session["TCSRate"].ToString();
        //}


        //if (PurchaseTDS == "Y")
        //{
        //    string tsdpur = Convert.ToString(txtPurchaseTDS.Text);
        //    if (tsdpur == hdnfPurchaseTDS.Value)
        //    {
        //        hdnfPurchaseTDS.Value = Session["PurchaseTDSRate"].ToString();
        //    }
        //    else
        //    {
        //        hdnfPurchaseTDS.Value = txtPurchaseTDS.Text;
        //    }
        //    // hdnfPurchaseTDS.Value = Session["PurchaseTDSRate"].ToString();
        //}
        //else
        //{
        //    if (PurchaseTDS == "N")
        //    {
        //        hdnfPurchaseTDS.Value = "0.00";
        //        string tcspur = Convert.ToString(txtTCSRate.Text);

        //        if (tcspur == hdnfTCSratepur.Value)
        //        {
        //            hdnfTCSratepur.Value = Session["TCSRate"].ToString();
        //        }
        //        else
        //        {
        //            hdnfTCSratepur.Value = txtTCSRate.Text;
        //        }
        //    }
        //    else
        //    {
        //        string tsdpur = Convert.ToString(txtPurchaseTDS.Text);
        //        if (tsdpur == hdnfPurchaseTDS.Value)
        //        {
        //            hdnfPurchaseTDS.Value = Session["PurchaseTDSRate"].ToString();
        //        }
        //        else
        //        {
        //            hdnfPurchaseTDS.Value = txtPurchaseTDS.Text;
        //        }
        //    }

        //}
        //saleRate = actualSaleRate + commision;

        //SBBalAmt = (saleRate * gstRate) / 100 + saleRate * qt;
        ////Amt = clsCommon.getString("select (case when sum(Bill_Amount)>0 then sum(Bill_Amount) else 0 end ) as amt FROM NT_1_SugarSale where doc_date<='" + docdate + "' and ac_code= '" + txtSaleBillTo.Text
        ////    + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");


        //Amt = clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + txtSaleBillTo.Text
        //              + " and DRCR='D' and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        //if (Amt == string.Empty || Amt == "")
        //{
        //    Amt = "0.00";
        //}
        //Amountf = Convert.ToDouble(Amt);
        //if (btnSave.Text == "Save")
        //{
        //    Amount = Amountf + SBBalAmt;
        //}
        //else
        //{
        //    Amount = Amountf;
        //}
        //DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //if (PurchaseTDS == "Y" && dEnd >= ss)
        //{
        //    txtTCSRate.Text = "0.00";
        //    txtPurchaseTDS.Text = hdnfPurchaseTDS.Value;

        //}
        //else
        //{
        //    if (PurchaseTDS == "N")
        //    {
        //        txtPurchaseTDS.Text = "0.00";
        //        if (hdnfTCSratepur.Value != string.Empty)
        //        {
        //            txtTCSRate.Text = hdnfTCSratepur.Value;
        //        }
        //        else
        //        {
        //            txtTCSRate.Text = "0.000";
        //        }
        //    }
        //    else
        //    {
        //        txtTCSRate.Text = "0.00";
        //        txtPurchaseTDS.Text = hdnfPurchaseTDS.Value;
        //    }
        //}
        //if (SaleTDS == "Y" && dEnd >= ss)
        //{
        //    txtTCSRate_Sale.Text = "0.00";
        //    // txtTCSRate.Text = "0.00";
        //    hdnfTCSRate.Value = "0.00";
        //    txtSaleTDS.Text = hdnfSaleTDS.Value;
        //    #region[Balance]
        //    if (Amount > balancelimit)
        //    {
        //        //txtTCSRate.Text = Session["TCSRate"].ToString();
        //        txtSaleTDS.Text = Session["SaleTDSRate"].ToString();
        //    }
        //    else
        //    {
        //        // txtTCSRate.Text = "0.000";
        //        if (hdnfTCSRate.Value != string.Empty)
        //        {
        //            txtSaleTDS.Text = hdnfSaleTDS.Value;
        //        }
        //        else
        //        {
        //            txtSaleTDS.Text = "0.000";
        //        }

        //    }
        //    #endregion
        //}
        //else
        //{
        //    txtSaleTDS.Text = "0.00";
        //    #region[Balance]
        //    if (Amount > balancelimit)
        //    {
        //        //txtTCSRate.Text = Session["TCSRate"].ToString();
        //        txtTCSRate_Sale.Text = Session["TCSRate"].ToString();
        //    }
        //    else
        //    {
        //        // txtTCSRate.Text = "0.000";
        //        if (hdnfTCSRate.Value != string.Empty)
        //        {
        //            txtTCSRate_Sale.Text = hdnfTCSRate.Value;
        //        }
        //        else
        //        {
        //            txtTCSRate_Sale.Text = "0.000";
        //        }

        //    }
        //    #endregion
        //}

        #endregion

        //if (drpInsured.SelectedValue == "Y")
        //{
        //    string Insurance = clsCommon.getString("Select isnull(Insurance,0) from qrymstaccountmaster where Ac_Code='" + txtSaleBillTo.Text +
        //        "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    txtInsurance.Text = Insurance;
        //}
        //else
        //{
        //    txtInsurance.Text = "0";

        //}
        double Amount = 0.00;
        double Amountf = 0.00;
        string SaleBillTo = txtSaleBillTo.Text;
        string Amt = string.Empty;
        double SBBalAmt = 0.00;

        double gstRate = Convert.ToDouble("0" + txtexcise_rate.Text);
        double saleRate = 0.00;
        double actualSaleRate = Convert.ToDouble("0" + txtSALE_RATE.Text);
        double commision = Convert.ToDouble("0" + txtCommission.Text);
        double insurance = Convert.ToDouble("0" + txtInsurance.Text);


        double qt = Convert.ToString(Math.Abs(Convert.ToDouble(txtquantal.Text))) != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0.00;
        string SaleTDS = "";
        string PurchaseTDS = "";

        string PSAmt = string.Empty;
        double PSBalAmt = 0.00;
        double PSRate = Convert.ToDouble("0" + txtpurchaserate.Text);
        double PSAmountf = 0.00;
        double PSAmount = 0.00;
        string paymenttotds = clsCommon.getString("select Payment_To from qrytenderheaddetail where Tender_No=" + txtPurcNo.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() +
            " and ID=" + txtPurcOrder.Text);
        //" and Year_Code=" + Session["year"].ToString());
        SaleTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
            + " and Company_Code=" + Session["Company_Code"].ToString() + "");

        hdnfistds.Value = SaleTDS;

        DataTable dtpan = new DataTable();
        DataSet dspan = new DataSet();
        #region[balancelimit]
        string panno = clsCommon.getString("select CompanyPan from qrymstaccountmaster where Ac_Code=" + paymenttotds
           + " and Company_Code=" + Session["Company_Code"].ToString() + "");

        dspan = clsDAL.SimpleQuery("select Ac_Code from qrymstaccountmaster where CompanyPan='" + panno
           + "' and Company_Code=" + Session["Company_Code"].ToString() + "");
        dtpan = dspan.Tables[0];

        PurchaseTDS = clsCommon.getString("select TDSApplicable from qrymstaccountmaster where Ac_Code=" + paymenttotds
           + " and Company_Code=" + Session["Company_Code"].ToString() + "");
        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
        double psamt2 = 0.00;
        double sbamt2 = 0.00;
        if (btnSave.Text == "Save" || hdnfsalebillto.Value != txtSaleBillTo.Text)
        {
            //PSTDS
            if (panno != string.Empty)
            {
                if (dtpan.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpan.Rows.Count; i++)
                    {
                        double psamt1 = 0.00;
                        //int accode = 0;
                        // accode=dtpan.Rows
                        psamt1 = Convert.ToDouble(clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where  AC_CODE=" + dtpan.Rows[i]["Ac_Code"].ToString()
                         + " and DRCR='C' and  TRAN_TYPE in('PS','PR','RP') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) +
                         "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                        psamt2 += psamt1;
                    }
                    PSAmt = psamt2.ToString();
                }
            }
        #endregion

            PSBalAmt = PSRate * qt;
            if (PSAmt == string.Empty || PSAmt == "")
            {
                PSAmt = "0.00";
            }
            PSAmountf = Convert.ToDouble(PSAmt);
            PSAmount = PSAmountf + PSBalAmt;
            if (PSAmount >= balancelimit)
            {
                txtPurchaseTDS.Text = Session["PurchaseTDSRate"].ToString();
                txtTCSRate.Text = "0.00";
            }
            else
            {
                txtPurchaseTDS.Text = "0.00";
                txtTCSRate.Text = "0.00";
            }
            //SaleTDS
            saleRate = actualSaleRate + commision + insurance;
            SBBalAmt = (saleRate * gstRate) / 100 + saleRate * qt;
            DataTable dtpansb = new DataTable();
            DataSet dspansb = new DataSet();

            string pannosb = clsCommon.getString("select CompanyPan from qrymstaccountmaster where Ac_Code=" + txtSaleBillTo.Text
               + " and Company_Code=" + Session["Company_Code"].ToString() + "");

            Amt = clsCommon.getString("select isnull(sum(AMOUNT),0) as AMOUNT from qrygledger where companypan='" + pannosb + 
                "' and DRCR='D' and  TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" 
                + Convert.ToInt32(Session["year"].ToString())
                     + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            
            //dspansb = clsDAL.SimpleQuery("select Ac_Code from qrymstaccountmaster where CompanyPan='" + pannosb
            //   + "' and Company_Code=" + Session["Company_Code"].ToString() + "");
            //dtpansb = dspansb.Tables[0];
            //if (pannosb != string.Empty)
            //{
            //    if (dtpansb.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtpansb.Rows.Count; i++)
            //        {
            //            double sbamt1 = 0.00;

            //            sbamt1 = Convert.ToDouble(clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpansb.Rows[i]["Ac_Code"].ToString()
            //         + " and DRCR='D' and  TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString())
            //         + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
            //            sbamt2 += sbamt1;
            //        }
            //        Amt = sbamt2.ToString();
            //    }
            //}

            //   Amt = clsCommon.getString("select sum(AMOUNT) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpan.Rows[i].ToString()
            //+ " and DRCR='D' and  TRAN_TYPE in('SB','LV','CV','RR','RS','RB','CB''GI') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString())
            //+ "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            if (Amt == string.Empty || Amt == "")
            {
                Amt = "0.00";
            }
            Amountf = Convert.ToDouble(Amt);
            Amount = Amountf + SBBalAmt;

            if (Amount >= balancelimit)
            {
                if (SaleTDS == "Y")
                {
                    txtSaleTDS.Text = Session["SaleTDSRate"].ToString();
                    txtTCSRate_Sale.Text = "0.00";
                }
                else
                {
                    txtSaleTDS.Text = "0.00";
                    txtTCSRate_Sale.Text = Session["TCSRate"].ToString();
                }

            }
            else
            {
                txtSaleTDS.Text = "0.00";
                txtTCSRate_Sale.Text = "0.00";
            }

        }


    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            string tenderno = txtPurcNo.Text;
            string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where  Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //if (!string.IsNullOrWhiteSpace(ccMail))
            //{
            //    ccMail = "," + ccMail;
            //}
            //string millEmail = txtMillEmailID.Text + ccMail;
            string do_no = lbldoid.Text;
            if (do_no != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DOParty('" + do_no + "','" + txtMILL_CODE.Text + "','" + tenderno + "')", true);
                //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
            }
        }
        catch
        {

        }
    }

    protected void btnPartyBillDO_Click(object sender, EventArgs e)
    {
        try
        {
            string tenderno = txtPurcNo.Text;
            string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //if (!string.IsNullOrWhiteSpace(ccMail))
            //{
            //    ccMail = "," + ccMail;
            //}
            //string millEmail = txtMillEmailID.Text + ccMail;
            string do_no = lbldoid.Text;
            string a = "0";
            string bss = "N";

            bss = hdnfpaymentcomnfirm.Value;
            string paymenttonew = "0";
            if (drpDOType.SelectedValue == "DI")
            {
                paymenttonew = grdDetail.Rows[0].Cells[4].Text;
            }
            if (hdconfirm.Value == "Yes")
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:partybilldo('" + do_no + "','"
                        + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "','" + bss + "','" + paymenttonew + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
            else
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:partybilldo1('" + do_no + "','" + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
        }
        catch
        {

        }
    }

    protected void txtFrieght_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtFrieght.Text;
        strTextBox = "txtFrieght";
        csCalculations();
        calculation();
        if (ViewState["mode"].ToString() == "I")
        {
            txtMemoAdvance.Text = "0.00";
            txtMemoAdvanceRate.Text = "0.00";
        }
        setFocusControl(txtFrieght);
        //double frieght_amount = txtFrieghtAmount.Text != string.Empty ? Convert.ToDouble(txtFrieghtAmount.Text) : 0.00;
    }

    protected void txtVasuliRate_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtVasuliRate.Text;
        strTextBox = "txtVasuliRate";
        csCalculations();
        calculation();
    }

    protected void txtVasuliAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtquantal.Text != "")
        {
            calculation();
            setFocusControl(txtVasuliRate1);
        }
        else
        {
            setFocusControl(txtquantal);
        }
    }

    protected void txtVasuliAmount1_TextChanged(object sender, EventArgs e)
    {
        if (txtquantal.Text != "")
        {
            double qntl = Convert.ToDouble(txtquantal.Text);
            var va1 = txtVasuliAmount1.Text != string.Empty ? Convert.ToString(txtVasuliAmount1.Text) : "0";
            var fmt = new NumberFormatInfo();
            fmt.NegativeSign = "-";
            var va1amount = double.Parse(va1, fmt);
            double vasuli_amount1 = va1amount;
            txtVasuliRate1.Text = Convert.ToDouble(vasuli_amount1 / qntl).ToString();

            calculation();
            setFocusControl(txtVasuliAc);
        }
        else
        {
            setFocusControl(txtquantal);
        }
    }

    protected void txtMemoAdvance_TextChanged(object sender, EventArgs e)
    {
        // searchString = txtMemoAdvance.Text;
        strTextBox = "txtMemoAdvance";
        double qntl = Convert.ToDouble(txtquantal.Text);
        double memoadvane = Convert.ToDouble(txtMemoAdvance.Text);
        double rate = Math.Round((memoadvane / qntl), 2);
        txtMemoAdvanceRate.Text = rate.ToString();
        TDSCAl();
        setFocusControl(txtMemoGSTRate);
    }

    protected void drpDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(txtGETPASS_CODE);
        if (drpDeliveryType.SelectedValue == "C")
        {
            ddlFrieghtType.SelectedValue = "P";
        }
        else
        {
            ddlFrieghtType.SelectedValue = "O";
        }
    }

    //protected void btnAddNewAccount_Click(object sender, EventArgs e)
    //{

    //}

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void txtDriverMobile_TextChanged(object sender, EventArgs e)
    {
        //  searchString = txtDriverMobile.Text;
        strTextBox = "txtDriverMobile";
        csCalculations();
    }

    protected void btnOurDO_Click(object sender, EventArgs e)
    {
        try
        {
            string tenderno = txtPurcNo.Text;
            string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //if (!string.IsNullOrWhiteSpace(ccMail))
            //{
            //    ccMail = "," + ccMail;
            //}
            //string millEmail = txtMillEmailID.Text + ccMail;
            string do_no = lbldoid.Text;
            string a = "0";
            string bss = "N";

            bss = hdnfpaymentcomnfirm.Value;
            string paymenttonew = "0";
            if (drpDOType.SelectedValue == "DI")
            {
                paymenttonew = grdDetail.Rows[0].Cells[4].Text;
            }
            if (hdconfirm.Value == "Yes")
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:od('" + do_no + "','"
                        + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "','" + bss + "','" + paymenttonew + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
            else
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:od1('" + do_no + "','" + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
        }
        catch
        {

        }
    }

    protected void btnDeliveryChallan_Click(object sender, EventArgs e)
    {
        try
        {
            string ccMail = clsCommon.getString("Select Email_Id_cc from qrymstAccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (!string.IsNullOrWhiteSpace(ccMail))
            {
                ccMail = "," + ccMail;
            }
            string millEmail = txtMillEmailID.Text + ccMail;
            string do_no = lbldoid.Text;
            string a = "0";
            if (chkNoprintondo.Checked == true)
            {
                a = "1";
            }

            //if (txtcarporateSale.Text != string.Empty || txtcarporateSale.Text!="0")
            //{
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kys", "javascript:DC('" + do_no + "','" + millEmail + "','O','" + a + "')", true);
            //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
            //}
        }
        catch
        {

        }
    }


    protected void btnperforminvoice_Click(object sender, EventArgs e)
    {
        string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";

        string saleid = clsCommon.getString("select saleid from qrysalehead where DO_No='" + txtdoc_no.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
            " and Year_Code='" + Session["year"].ToString() + "'");
        string docnumber = txtdoc_no.Text;
        string corporatenumber = txtcarporateSale.Text;



        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:PI('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber + "')", true);

        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";
        //string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
        //    " and Year_Code='" + Session["year"].ToString() + "'");

        //if (hdconfirm.Value == "Yes")
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "')", true);
        //}
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);

    }

    protected void txtFrieghtAmount_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtINVOICE_NO_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnOpenDetailsPopup);
    }

    protected void txtVasuliRate1_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtVasuliRate1.Text;
        strTextBox = "txtVasuliRate1";

        VasuliAmountCalculation();

        csCalculations();
        calculation();
    }

    private void VasuliAmountCalculation()
    {
        double qntl = Convert.ToDouble(txtquantal.Text);
        var vr1 = txtVasuliRate1.Text != string.Empty ? Convert.ToString(txtVasuliRate1.Text) : "0";
        var fmt = new NumberFormatInfo();
        fmt.NegativeSign = "-";
        var number = double.Parse(vr1, fmt);
        double vasuli_rate1 = number;
        txtVasuliAmount1.Text = Convert.ToString(vasuli_rate1 * qntl);
    }

    protected void btnSendSms_Click(object sender, EventArgs e)
    {
        try
        {
            string driverMobile = txtDriverMobile.Text;
            string mobile = txtMillMobile.Text;
            string Cst_noC = clsCommon.getString("Select Cst_no from " + tblPrefix + "AccountMaster where Ac_Code=" + txtGETPASS_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string Tin_NoC = clsCommon.getString("Select Tin_No from " + tblPrefix + "AccountMaster where Ac_Code=" + txtGETPASS_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string ECC_NoC = clsCommon.getString("Select ECC_No from " + tblPrefix + "AccountMaster where Ac_Code=" + txtGETPASS_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string millshort = clsCommon.getString("Select Short_Name from " + tblPrefix + "AccountMaster where Ac_Code=" + txtMILL_CODE.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string msg = millshort + " DO.No:" + txtdoc_no.Text + " " + LBLGETPASS_NAME.Text + " TIN:" + Tin_NoC + " ECC No:" + ECC_NoC + " CST:" + Cst_noC + " Qntl:" + txtquantal.Text + " Mill Rate:" + txtmillRate.Text + " Lorry:" + txtTruck_NO.Text + " " + txtNARRATION1.Text;
            string API = clsGV.msgAPI;
            string Url = API + "mobile=" + mobile + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse resp = (HttpWebResponse)myReq.GetResponse();
            StreamReader reder = new StreamReader(resp.GetResponseStream());
            string respString = reder.ReadToEnd();
            reder.Close();
            resp.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Message Successfully Sent!');", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void txtPartyCommission_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //double SR = Convert.ToDouble(hdnfSaleRate.Value.TrimStart());
            //double MR = Convert.ToDouble(txtmillRate.Text);
            //double CR = Convert.ToDouble(txtPartyCommission.Text);
            //txtSALE_RATE.Text = Convert.ToString(MR + CR);
            //double qtl = Convert.ToDouble(txtquantal.Text);
            //double diff = 0.00;
            //double diffAmt = 0.00;
            //diff = SR - MR;
            //diffAmt = Math.Round(diff * qtl, 2);
            //calculation();
            setFocusControl(txtSALE_RATE);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void txtMemoAdvanceRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            MemoadvanceCalculation();
            setFocusControl(txtMemoGSTRate);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void MemoadvanceCalculation()
    {
        double rate = txtMemoAdvanceRate.Text != string.Empty ? Convert.ToDouble(txtMemoAdvanceRate.Text) : 0;
        double qntl = txtquantal.Text != string.Empty ? Convert.ToDouble(txtquantal.Text) : 0.0;
        txtMemoAdvance.Text = Convert.ToString(Math.Round((rate * qntl), 2));

        double frtRate = txtFrieght.Text != string.Empty ? Convert.ToDouble(txtFrieght.Text.ToString()) : 0.0;
        double remaingRate = frtRate - rate;
        lblFrieghtToPay.Text = "Frieght To Pay : " + (Math.Round((remaingRate * qntl), 2)).ToString();
    }

    protected void btnVoucherOtherAmounts_Click(object sender, EventArgs e)
    {
        pnlVoucherEntries.Style["display"] = "block";
        setFocusControl(txtVoucherBrokrage);
    }

    protected void txtVoucherBrokrage_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtVoucherServiceCharge);
    }

    protected void txtVoucherServiceCharge_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtVoucherL_Rate_Diff);
    }

    protected void txtVoucherL_Rate_Diff_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double voucratediff = Convert.ToDouble(txtVoucherL_Rate_Diff.Text);
            double quintal = Convert.ToDouble(txtquantal.Text);
            double ratediffamt = voucratediff * quintal;
            txtVoucherRATEDIFFAmt.Text = ratediffamt.ToString();
            setFocusControl(txtVoucherCommission_Rate);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void txtVoucherCommission_Rate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double bankcommrate = Convert.ToDouble(txtVoucherCommission_Rate.Text);
            double quintal = Convert.ToDouble(txtquantal.Text);
            double commamt = bankcommrate * quintal;
            txtVoucherBANK_COMMISSIONAmt.Text = commamt.ToString();
            setFocusControl(txtVoucherInterest);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void txtVoucherInterest_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtVoucherTransport_Amount);
    }

    protected void txtVoucherTransport_Amount_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtVoucherOTHER_Expenses);
    }

    protected void txtVoucherOTHER_Expenses_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnOk);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        pnlVoucherEntries.Style["display"] = "none";
        setFocusControl(txtDO_CODE);
    }

    #region [getMaxCodeofvouchers]
    private void getvoucherscode(string tblName, string objCode, string trantype, string tblColumnType)
    {
        try
        {
            DataSet ds = null;
            string docno = "0";
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                if (trantype == "NULL")
                {
                    obj.tableName = tblName + " where  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                }
                else
                {
                    obj.tableName = tblName + " where  " + tblColumnType + "='" + trantype + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                }
                obj.code = objCode;

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
                                    docno = ds.Tables[0].Rows[0][0].ToString();
                                    ViewState["maxval"] = docno;
                                }
                            }
                        }
                    }
                }
            }
            //return docno;
        }
        catch
        {
        }
    }
    #endregion

    #region [getMaxCodeofvouchers]
    private void getvoucherscode1(string tblName, string objCode, string trantype, string tblColumnType)
    {
        try
        {
            DataSet ds = null;
            string docno = "0";
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                if (trantype == "NULL")
                {
                    obj.tableName = tblName + " where  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                }
                else
                {
                    obj.tableName = tblName + " where  " + tblColumnType + "='" + trantype + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                }
                obj.code = objCode;

                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //if (ViewState["mode"] != null)
                            //{
                            if (ViewState["mode"].ToString() == "U")
                            {
                                docno = ds.Tables[0].Rows[0][0].ToString();
                                ViewState["maxval"] = docno;
                            }
                            // }
                        }
                    }
                }
            }
            //return docno;
        }
        catch
        {
        }
    }
    #endregion

    static string returnNumber(string docno)
    {
        return docno;
    }

    //protected void btnPrintITCVoc_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string vtype = clsCommon.getString("select voucher_type from " + qryCommon + " where doc_no=" + txtdoc_no.Text + " and tran_type='" + trnType + "' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
    //        if (vtype != "PS")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kysdsd", "javascript:ITCV('" + lblVoucherNo.Text + "');", true);
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    protected void lnkMemo_Click(object sender, EventArgs e)
    {
        Session["MEMO_NO"] = lblMemoNo.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjs", "javascript:memo();", true);
    }

    protected void lnkVoucOrPurchase_Click(object sender, EventArgs e)
    {
        string vocno = lblVoucherNo.Text;
        string vocType = lblVoucherType.Text;
        if (vocType == "PS")
        {
            vocno = clsCommon.getString("select purchaseid from qrypurchasehead where doc_no=" + vocno + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");
            Session["PURC_NO"] = vocno;
            int Action = 1;
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:sugarpurchase();", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asdas1", "javascript:sugarpurchase('" + Action + "','" + vocno + "')", true);
        }
        if (vocType == "LV")
        {
            vocno = clsCommon.getString("select commissionid from qrycommissionbill where Tran_Type='" + vocType + "' and  doc_no=" + vocno +
                " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");
            int Action = 1;
            //Session["LV_NO"] = vocno;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:LocalVoucher('" + Action + "','" + vocno + "','" + vocType + "');", true);
        }
        if (vocType == "CV")
        {
            vocno = clsCommon.getString("select commissionid from qrycommissionbill where Tran_Type='" + vocType + "' and  doc_no=" + vocno +
                " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");
            int Action = 1;
            //Session["LV_NO"] = vocno;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:LocalVoucher('" + Action + "','" + vocno + "','" + vocType + "');", true);
        }
        if (vocType == "OV")
        {
            Session["VOUC_NO"] = vocno;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsd", "javascript:loadingvoucher();", true);
        }
    }

    protected void lblsbnol_Click(object sender, EventArgs e)
    {
        Int32 sbno = lblSB_No.Text != string.Empty ? Convert.ToInt32(lblSB_No.Text) : 0;
        if (sbno != 0)
        {
            sbno = Convert.ToInt32(clsCommon.getString("select saleid from qrysalehead where doc_no=" + sbno + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));
            Session["SB_NO"] = sbno;
            int Action = 1;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjsdsd", "javascript:salebill('" + Action + "','" + sbno + "');", true);
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
                int do_id = Convert.ToInt32(clsCommon.getString("select doid from nt_1_deliveryorder where doc_no=" + txtEditDoc_No.Text +
                    " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                    " " + Session["year"].ToString() + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DoOPen('" + do_id + "')", true);

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnClosePopup_Click(object sender, EventArgs e)
    {
        pnlSendSMS.Style["display"] = "none";
        setFocusControl(btnAdd);
    }

    protected void txtCommission_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtTruck_NO);
        calculation();
    }

    #region [txtVasuliAc_TextChanged]
    protected void txtVasuliAc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVasuliAc.Text;
        strTextBox = "txtVasuliAc";
        csCalculations();
    }
    #endregion

    #region [btntxtVasuliAc_Click]
    protected void btntxtVasuliAc_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtVasuliAc";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    protected void txtVoucherbyGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVoucherbyGstStateCode.Text;
        strTextBox = "txtVoucherbyGstStateCode";
        csCalculations();
    }

    protected void btntxtVoucherbyGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtVoucherbyGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void txtGetpassGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGetpassGstStateCode.Text;
        strTextBox = "txtGetpassGstStateCode";
        csCalculations();
    }

    protected void btntxtGetpassGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGetpassGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void txtSalebilltoGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSalebilltoGstStateCode.Text;
        strTextBox = "txtSalebilltoGstStateCode";
        csCalculations();
    }

    protected void btntxtSalebilltoGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtSalebilltoGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void txtTransportGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransportGstStateCode.Text;
        strTextBox = "txtTransportGstStateCode";
        csCalculations();
    }

    protected void btntxtTransportGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTransportGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void txtMillGstStateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMillGstStateCode.Text;
        strTextBox = "txtMillGstStateCode";
        csCalculations();
    }

    protected void btntxtMillGstStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMillGstStateCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    protected void txtDistance_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnSave);
    }

    protected void chkEWayBill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEWayBill.Checked == true)
        {
            txtEWayBill_No.Text = txtMillEwayBill_No.Text;
            lblchkEWayBill.Text = LBLMILL_NAME.Text;

        }
        else
        {
            lblchkEWayBill.Text = "";
            txtEWayBill_No.Text = string.Empty;

        }

    }
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            //counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            //if (counts == 0)
            //{
            //    txtdoc_no.Text = "1";
            //    DOC_NO = 1;

            //}
            //else
            //{
            //DOC_NO = Convert.ToInt32(clsCommon.getString("SELECT max(carpid) as carpid from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
            DOC_NO = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["year"].ToString() + "'")) + 1;
            if (DOC_NO == 0)
            {
                txtdoc_no.Text = "1";
                DOC_NO = 1;
                txtdoc_no.Text = DOC_NO.ToString();
            }
            else
            {
                txtdoc_no.Text = DOC_NO.ToString();
            }
            // }

            doid = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(doid),0) as doid from " + tblHead)) + 1;
            if (doid == 0)
            {
                lbldoid.Text = "1";
            }
            else
            {
                lbldoid.Text = doid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
    #region DataStore
    //private int DataStore(int flag)
    //{
    //    int count = 0;
    //    try
    //    {
    //        //Connection open
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        con.Open();
    //        ///Execution
    //        myTran = con.BeginTransaction();
    //        //cmd.CommandText = qry;
    //        //cmd.Connection = con;
    //        //cmd.Transaction = myTran;
    //        if (flag == 1)
    //        {

    //            cmd = new SqlCommand(Head_Insert, con, myTran);
    //            cmd.ExecuteNonQuery();
    //            if (Detail_Insert != "")
    //            {
    //                cmd = new SqlCommand(Detail_Insert, con, myTran);
    //                cmd.ExecuteNonQuery();
    //            }
    //            if (Head_Update != "")
    //            {
    //                cmd = new SqlCommand(Head_Update, con, myTran);
    //                cmd.ExecuteNonQuery();
    //            }

    //            myTran.Commit();
    //            Thread.Sleep(100);

    //            count = 1;
    //        }
    //        else if (flag == 2)
    //        {
    //            if (Head_Update != "")
    //            {
    //                cmd = new SqlCommand(Head_Update, con, myTran);
    //                cmd.ExecuteNonQuery();
    //            }
    //            if (Detail_Update != "")
    //            {
    //                cmd = new SqlCommand(Detail_Update, con, myTran);
    //                cmd.ExecuteNonQuery();
    //            }
    //            if (Detail_Insert != "")
    //            {
    //                cmd = new SqlCommand(Detail_Insert, con, myTran);
    //                cmd.ExecuteNonQuery();
    //            }
    //            if (Detail_Delete != "")
    //            {
    //                cmd = new SqlCommand(Detail_Delete, con, myTran);
    //                cmd.ExecuteNonQuery();
    //            }
    //            myTran.Commit();
    //            Thread.Sleep(100);
    //            count = 2;
    //        }
    //        else
    //        {
    //            cmd = new SqlCommand(Detail_Delete, con, myTran);
    //            cmd.ExecuteNonQuery();
    //            cmd = new SqlCommand(Head_Delete, con, myTran);
    //            cmd.ExecuteNonQuery();
    //            myTran.Commit();
    //            Thread.Sleep(100);
    //            count = 3;
    //        }

    //        return count;
    //    }
    //    catch
    //    {
    //        if (myTran != null)
    //        {
    //            myTran.Rollback();
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);

    //        }
    //        return count;

    //    }
    //    finally
    //    {
    //        con.Close();
    //    }

    //}
    #endregion

    #region PurchasePosting
    private void PurchasePosting()
    {
        try
        {
            #region Purchase Posting
            purchase = new PurchaseFields();
            #region Find Max
            int counts = 0;
            if (btnSave.Text == "Save")
            {
                counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from nt_1_sugarpurchase where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'"));
                if (counts == 0)
                {
                    purchase.PS_doc_no = 1;
                }
                else
                {
                    purchase.PS_doc_no = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from nt_1_sugarpurchase where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                        " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                }
                counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(purchaseid),0) as purchaseid from nt_1_sugarpurchase "));
                if (counts == 0)
                {
                    purchase.PS_purchase_Id = 1;

                }
                else
                {
                    purchase.PS_purchase_Id = Convert.ToInt32(clsCommon.getString("SELECT max(purchaseid) as purchaseid from nt_1_sugarpurchase")) + 1;

                }
            }
            else
            {


                if (lblVoucherNo.Text == "0" || lblVoucherNo.Text == string.Empty)
                {
                    counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from nt_1_sugarpurchase where Company_Code='" + Session["Company_Code"].ToString() + "' " +
               " and Year_Code='" + Session["year"].ToString() + "'"));
                    if (counts == 0)
                    {
                        purchase.PS_doc_no = 1;
                    }
                    else
                    {
                        purchase.PS_doc_no = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from nt_1_sugarpurchase where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                            " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                    }
                    counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(purchaseid),0) as purchaseid from nt_1_sugarpurchasedetails "));
                    if (counts == 0)
                    {
                        purchase.PS_purchase_Id = 1;

                    }
                    else
                    {
                        purchase.PS_purchase_Id = Convert.ToInt32(clsCommon.getString("SELECT max(purchaseid) as purchaseid from nt_1_sugarpurchasedetails")) + 1;

                    }
                }
                else
                {
                    purchase.PS_doc_no = Convert.ToInt32(lblVoucherNo.Text);
                    purchase.PS_purchase_Id = Convert.ToInt32(clsCommon.getString("SELECT purchaseid from nt_1_sugarpurchase where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                             " and Year_Code='" + Session["year"].ToString() + "' and doc_no='" + lblVoucherNo.Text + "'"));
                }

            }
            #endregion

            gstSateCodeForPurchaseBill = 0;

            PaymentTo = Convert.ToInt32(clsCommon.getString("Select isnull(Payment_To,0) as paymentto from NT_1_Tender where Tender_No=" + purc_no + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));
            gstSateCodeForPurchaseBill = MillGstStateCode;
            //int selfact = Convert.ToInt32(Session["SELF_AC"].ToString());
            //CGSTRATE = Convert.ToDouble(clsCommon.getString("select CGST from NT_1_GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            //SGSTRATE = Convert.ToDouble(clsCommon.getString("select SGST from NT_1_GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            //IGSTRATE = Convert.ToDouble(clsCommon.getString("select IGST from NT_1_GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            //GSTRATE = Convert.ToDouble(clsCommon.getString("select Rate from NT_1_GSTRateMaster where Doc_no=" + GSTRateCode + " "));

            if (Convert.ToInt32(MILL_CODE) != PaymentTo)
            {
                gstSateCodeForPurchaseBill = Convert.ToInt32(clsCommon.getString("select isnull(GSTStateCode,0) as code from qrymstaccountmaster where Ac_Code=" + PaymentTo + " " +
                    " and Company_Code=" + Session["Company_Code"].ToString() + " "));
            }


            if (AUTO_VOUCHER == "YES")
            {
                if (drpDOType.SelectedValue == "DI")
                {
                    if (GETPASS_CODE == SELFAC.ToString() || PDS == "P")
                    {
                        #region GST Calculation
                        CompanyStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
                        if (CompanyStateCode == gstSateCodeForPurchaseBill)
                        {
                            MILLAMOUNT = (mill_rate * QUANTAL) + 0;
                            CGSTtaxAmountOnMR = Math.Round(MILLAMOUNT * cgstrate / 100, 2);
                            CGST_AMOUNT = CGSTtaxAmountOnMR;
                            SGSTtaxAmountOnMR = Math.Round(MILLAMOUNT * sgstrate / 100, 2);
                            SGST_AMOUNT = SGSTtaxAmountOnMR;
                            // IGSTRATE = 0;
                            IGST_AMOUNT = 0.00;
                        }
                        else
                        {
                            MILLAMOUNT = (mill_rate * QUANTAL) + 0;
                            IGSTtaxAmountOnMR = Math.Round(MILLAMOUNT * igstrate / 100, 2);
                            IGST_AMOUNT = IGSTtaxAmountOnMR;
                            CGST_AMOUNT = 0.00;
                            // CGSTRATE = 0;
                            SGST_AMOUNT = 0.00;
                            // SGSTRATE = 0;
                        }
                        #endregion
                        TOTALPurchase_Amount = Math.Round(QUANTAL * mill_rate + CGST_AMOUNT + SGST_AMOUNT + IGST_AMOUNT, 2);
                        ITEM_AMOUNT = QUANTAL * mill_rate;

                        VOUCHER_NO = Convert.ToInt32(lblVoucherNo.Text != string.Empty ? lblVoucherNo.Text : "0");
                        if (VOUCHER_NO > 0)
                        {
                            maxcountpsno = VOUCHER_NO;
                        }
                        else
                        {
                            maxcountpsno = 0;
                        }


                        // millCityCode = Convert.ToInt32(clsCommon.getString("select isnull(city_code,0) as citycode from NT_1_AccountMaster where Ac_Code=" + MILL_CODE + " "));
                        fromPlace = clsCommon.getString("select cityname from qrymstaccountmaster where  Ac_Code=" + MILL_CODE + " and Company_Code='" + Session["Company_Code"].ToString() + "'");
                        // getPassCityCode = Convert.ToInt32(clsCommon.getString("select isnull(city_code,0) as citycode from NT_1_AccountMaster where Ac_Code=" + GETPASS_CODE + " "));
                        toPlace = clsCommon.getString("select cityname from qrymstaccountmaster where  Ac_Code=" + GETPASS_CODE + " and Company_Code='" + Session["Company_Code"].ToString() + "'");

                        #region Assign Values To Purchase Common Class Fields
                        purchase.PS_Tran_Type = "PS";
                        purchase.PS_Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
                        purchase.PS_Year_Code = Convert.ToInt32(Session["year"].ToString());
                        purchase.PS_Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
                        purchase.PS_PURCNO = Convert.ToInt32(txtdoc_no.Text);
                        purchase.PS_Purcid = Convert.ToInt32(lbldoid.Text);
                        purchase.PS_doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                        purchase.PS_Ac_Code = Convert.ToInt32(PaymentTo);
                        purchase.PS_mill_code = Convert.ToInt32(MILL_CODE);
                        purchase.PS_GstRateCode = GSTRateCode;
                        purchase.PS_FROM_STATION = fromPlace;
                        purchase.PS_TO_STATION = toPlace;
                        purchase.PS_mill_inv_date = DateTime.Parse(txtMillInv_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                        purchase.PS_LORRYNO = TRUCK_NO;
                        purchase.PS_BROKER = BROKER_CODE;
                        purchase.PS_subTotal = (QUANTAL * mill_rate) + 0;
                        purchase.PS_LESS_FRT_RATE = 0.00;
                        purchase.PS_freight = 0.00;
                        purchase.PS_cash_advance = 0.00;
                        purchase.PS_bank_commission = 0.00;
                        purchase.PS_OTHER_AMT = 0.00;
                        purchase.PS_Grade = txtGRADE.Text;
                        purchase.PS_Bill_Amount = TOTALPurchase_Amount;
                        purchase.PS_Due_Days = 1;
                        purchase.PS_NETQNTL = QUANTAL + 0;
                        purchase.PS_Bill_No = txtMillInvoiceno.Text;
                        purchase.PS_CGSTRate = cgstrate + 0;
                        purchase.PS_CGSTAmount = CGST_AMOUNT + 0;
                        purchase.PS_IGSTRate = igstrate + 0;
                        purchase.PS_IGSTAmount = IGST_AMOUNT + 0;
                        purchase.PS_SGSTRate = sgstrate + 0;
                        purchase.PS_SGSTAmount = SGST_AMOUNT + 0;
                        purchase.PS_EWay_Bill_No = MillEwayBill;
                        if (GETPASS_CODE == SELFAC.ToString() && SaleBillTo == SELFAC.ToString())
                        {
                            purchase.PS_SelfBal = "Y";
                        }
                        else
                        {
                            purchase.PS_SelfBal = "N";
                        }
                        purchase.PS_Created_By = Session["user"].ToString();
                        purchase.PS_Modified_By = Session["user"].ToString();
                        purchase.PS_ac = Convert.ToInt32(clsCommon.getString("select isnull(accoid,0)as ccid from qrymstaccountmaster where Ac_Code=" + PaymentTo + " and Company_Code=" + Session["Company_Code"].ToString() + ""));
                        try
                        {
                            purchase.PS_bk = Convert.ToInt32(bk);
                        }
                        catch
                        {
                        }
                        try
                        {
                            purchase.PS_mc = Convert.ToInt32(mc);
                        }
                        catch
                        {
                        }
                        #endregion
                        dt1 = new DataTable();
                        DataRow dr = null;
                        dt1.Columns.Add((new DataColumn("ID", typeof(Int32))));

                        #region [Write here columns]
                        dt1.Columns.Add((new DataColumn("item_code", typeof(Int32))));
                        dt1.Columns.Add((new DataColumn("narration", typeof(string))));
                        dt1.Columns.Add((new DataColumn("Quantal", typeof(double))));
                        dt1.Columns.Add((new DataColumn("packing", typeof(Int32))));
                        dt1.Columns.Add((new DataColumn("bags", typeof(Int32))));
                        dt1.Columns.Add((new DataColumn("rate", typeof(double))));
                        dt1.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                        dt1.Columns.Add((new DataColumn("purchasedetailid", typeof(int))));
                        #endregion
                        dt1.Columns.Add(new DataColumn("rowAction", typeof(string)));
                        dt1.Columns.Add((new DataColumn("SrNo", typeof(int))));
                        dr = dt1.NewRow();
                        dr["ID"] = 1;

                        dr["SrNo"] = 0;

                        #region [ Set values to dr]
                        dr["item_code"] = itemcode;
                        dr["narration"] = string.Empty;
                        dr["Quantal"] = QUANTAL;
                        dr["packing"] = PACKING;
                        dr["bags"] = BAGS;
                        dr["rate"] = mill_rate;
                        dr["item_Amount"] = ITEM_AMOUNT;
                        if (btnSave.Text == "Save")
                        {
                            dr["rowAction"] = "A";
                            dr["purchasedetailid"] = 0;
                        }
                        else
                        {
                            if (lblVoucherNo.Text == string.Empty || lblVoucherNo.Text == "0")
                            {
                                dr["rowAction"] = "A";
                                dr["purchasedetailid"] = 0;
                            }
                            else
                            {
                                dr["rowAction"] = "U";
                                //string purcid = clsCommon.getString("select purchaseid from nt_1_sugarpurchase where  doc_no='" + lblVoucherNo.Text + "' and Company_Code=" + Session["Company_Code"].ToString() + " " +
                                //    " and Year_Code=" + Session["year"].ToString() + "");

                                dr["purchasedetailid"] = Convert.ToInt32(clsCommon.getString("select purchasedetailid from nt_1_sugarpurchasedetails where  doc_no='" + lblVoucherNo.Text + "' " +
                                    " and  Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));
                            }
                        }
                        dt1.Rows.Add(dr);

                        #endregion

                    }
                    else
                    {
                        #region LV
                        #endregion
                    }
                }

            }
            #endregion
        }
        catch
        {
        }
    }
    #endregion

    #region SalePosting
    private void SalePosting()
    {
        try
        {
            #region Sale Posting

            salePosting = new SaleFields();
            #region find max
            int counts = 0;
            salePosting.SB_FreightPaid_Amount = Convert.ToDouble(txtVasuliAmount.Text != string.Empty ? txtVasuliAmount.Text : "0");

            if (btnSave.Text == "Save")
            {
                counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from nt_1_sugarsale where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'"));
                if (counts == 0)
                {
                    salePosting.SB_doc_no = 1;

                }
                else
                {
                    salePosting.SB_doc_no = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from nt_1_sugarsale where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                        " and Year_Code='" + Session["year"].ToString() + "'")) + 1;

                }
                counts = Convert.ToInt32(clsCommon.getString("SELECT count(saleid) as saleid from nt_1_sugarsale "));
                if (counts == 0)
                {
                    salePosting.SB_Sale_Id = 1;

                }
                else
                {
                    salePosting.SB_Sale_Id = Convert.ToInt32(clsCommon.getString("SELECT max(saleid) as saleid from nt_1_sugarsale")) + 1;

                }
            }
            else
            {

                int sb = Convert.ToInt32(lblSB_No.Text != string.Empty ? lblSB_No.Text : "0");
                if (sb == 0)
                {
                    counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from nt_1_sugarsale where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "'"));
                    if (counts == 0)
                    {
                        salePosting.SB_doc_no = 1;

                    }
                    else
                    {
                        salePosting.SB_doc_no = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from nt_1_sugarsale where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                            " and Year_Code='" + Session["year"].ToString() + "'")) + 1;

                    }

                    counts = Convert.ToInt32(clsCommon.getString("SELECT count(saleid) as saleid from nt_1_sugarsale "));
                    if (counts == 0)
                    {
                        salePosting.SB_Sale_Id = 1;

                    }
                    else
                    {
                        salePosting.SB_Sale_Id = Convert.ToInt32(clsCommon.getString("SELECT max(saleid) as saleid from nt_1_sugarsale")) + 1;

                    }

                }
                else
                {

                    salePosting.SB_doc_no = Convert.ToInt32(lblSB_No.Text);
                    salePosting.SB_Sale_Id = Convert.ToInt32(clsCommon.getString("SELECT saleid  from nt_1_sugarsale where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                             " and Year_Code='" + Session["year"].ToString() + "' and doc_no='" + lblSB_No.Text + "'"));
                }


            }
            #endregion

            #region
            Int32 pdsparty = hdnfPDSPartyCode.Value != string.Empty ? Convert.ToInt32(hdnfPDSPartyCode.Value) : 0;
            Int32 pdsunit = hdnfPDSUnitCode.Value != string.Empty ? Convert.ToInt32(hdnfPDSUnitCode.Value) : 0;

            string PDS1 = string.Empty;
            int saleBillcityCode = 0;
            string saleBilltoCity = string.Empty;

            if (Carporate_Sale_No != null && Carporate_Sale_No != 0)
            {
                PDS1 = clsCommon.getString("select selling_type from carporatehead where Doc_No='" + Carporate_Sale_No + "' and Company_Code='"
                    + Session["Company_Code"].ToString() + "' ");

            }

            try
            {
                saleBilltoCity = clsCommon.getString("select cityname  from qrymstaccountmaster where Ac_Code='" + SaleBillTo + "' and Company_Code='" + Session["Company_Code"].ToString() + "'");
            }
            catch { }

            try
            {
                fromPlace = clsCommon.getString("select cityname  from qrymstaccountmaster where Ac_Code='" + MILL_CODE + "' and Company_Code='" + Session["Company_Code"].ToString() + "'");
            }
            catch { }


            CompanyStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());

            LessFriegthRateForSaleBill = MM_Rate + VASULI_RATE_1;
            LessFriegthAmountForSaleBill = MEMO_ADVANCE + VASULI_AMOUNT_1;

            string UnitCity = "";
            if (Carporate_Sale_No != null && Carporate_Sale_No != 0)
            {
                saleparty = Convert.ToInt32(clsCommon.getString("select isnull(ac_code,0) as ac_code from qrycarporateheaddetail where Doc_No='" + Carporate_Sale_No
                    + "' and Company_Code=" + Session["Company_Code"].ToString() + " "));


                pdsunitSaleBill = Convert.ToInt32(clsCommon.getString("select isnull(unit_code,0) as Unit from qrycarporateheaddetail where Doc_No='" + Carporate_Sale_No +
                    "' and Company_Code=" + Session["Company_Code"].ToString() + " "));

                UnitCity = clsCommon.getString("select CityName from qrymstaccountmaster where Ac_Code='" + pdsunitSaleBill + "' and Company_Code=" + Session["Company_Code"].ToString() + " ");
            }

            double saleRate = SALE_RATE + Tender_Commission + 0;
            double subTOTAL = saleRate + QUANTAL + 0;

            double salerateFor_naka = 0.00;
            double SalerateFor_Salebill = 0.00;
            double gstSalerateAndAdvance = 0.00;

            double taxableAmount = (saleRate * QUANTAL) + FRIEGHT_AMOUNT;
            int cSale = txtcarporateSale.Text.Trim() != string.Empty ? Convert.ToInt32(txtcarporateSale.Text) : 0;
            if (Delivery_Type == "C")
            {
                // TaxableAmountForSB = Math.Round((saleRate * QUANTAL) + MEMO_ADVANCE );
                TaxableAmountForSB = Math.Round((saleRate * QUANTAL) + MEMO_ADVANCE + VASULI_AMOUNT_1);
            }
            else
            {

                if (cSale == 0)
                {
                    salerateFor_naka = (saleRate - FRIEGHT_RATE + MM_Rate);
                    //double SaleRateForNaka = (SALE_RATE - FRIEGHT_RATE);
                    SaleRateForSB = Math.Round((((salerateFor_naka / (salerateFor_naka + (salerateFor_naka * GSTRate / 100))) * salerateFor_naka)), 2);
                    TaxableAmountForSB = Math.Round((SaleRateForSB * QUANTAL), 2);

                    //TaxableAmountForSB = Math.Round((SaleRateForSB * QUANTAL) + MEMO_ADVANCE);
                }
                else
                {
                    SaleRateForNaka = (saleRate - FRIEGHT_RATE + MM_Rate);
                    //double SaleRateForNaka = (SALE_RATE - FRIEGHT_RATE);
                    SaleRateForSB = (saleRate - FRIEGHT_RATE + MM_Rate); ;// Math.Round((((SaleRateForNaka / (SaleRateForNaka + (SaleRateForNaka * GSTRate / 100))) * SaleRateForNaka)), 2);
                    TaxableAmountForSB = Math.Round((SaleRateForSB * QUANTAL), 2);

                    //TaxableAmountForSB = Math.Round((SaleRateForSB * QUANTAL) + MEMO_ADVANCE);
                }



            }

            double SaleBillAmt = 0.00;
            if (SaleBillTo != "0" || saleparty != 0)
            {

                if (pdsparty != 0 && txtcarporateSale.Text != string.Empty)
                {
                    int pdspartystatecode = Convert.ToInt32(clsCommon.getString("select isnull(GSTStateCode,0) from qrymstaccountmaster  where Ac_Code=" + pdsparty + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
                    //SaleBillTo = pdsunit.ToString();
                    if (pdspartystatecode != 0)
                    {
                        if (CompanyStateCode == pdspartystatecode)
                        {
                            CGSTRateForSB = cgstrate;
                            double saleamount = saleRate * QUANTAL;
                            double cgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * cgstrate / 100), 2);
                            //double cgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + cgsttaxAmountOnSR) * SALE_RATE)), 2);
                            //double cgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - cgstExSaleRate), 2);

                            CGSTAmountForSB = Math.Round(cgsttaxAmountOnSR, 2);

                            SGSTRateForSB = sgstrate;
                            double sgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * sgstrate / 100), 2);
                            //double sgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + sgsttaxAmountOnSR) * SALE_RATE)), 2);
                            //double sgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - sgstExSaleRate), 2);
                            SGSTAmountForSB = Math.Round(sgsttaxAmountOnSR, 2);
                        }
                        else
                        {
                            IGSTRateForSB = igstrate;
                            double igsttaxAmountOnSR = (TaxableAmountForSB * igstrate / 100);
                            //double igstExSaleRate = Math.Round(Math.Abs((SaleRateForSB / (SaleRateForSB + igsttaxAmountOnSR) * SALE_RATE)), 2);
                            //double igstRateAmountOnSR = Math.Round(Math.Abs(SaleRateForSB - igstExSaleRate), 2);
                            IGSTAmountForSB = Math.Round(igsttaxAmountOnSR, 2);
                        }
                    }
                    else
                    {
                        //isValidated = false;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please Add State Code to Pds Party!');", true);
                        setFocusControl(txtMILL_CODE);
                        return;
                    }
                }
                else
                {
                    if (Carporate_Sale_No != 0)
                    {
                        if (PDS == "C")
                        {
                            string csbilltoname = clsCommon.getString("Select bill_to from carporatehead where Doc_No=" + Carporate_Sale_No
                                + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                            int csbilltoStateCode = Convert.ToInt32(clsCommon.getString("select isnull(GSTStateCode,0) from qrymstaccountmaster  where Ac_Code=" + csbilltoname +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));


                            if (CompanyStateCode == csbilltoStateCode)
                            {
                                CGSTRateForSB = cgstrate;
                                double saleamount = saleRate * QUANTAL;
                                double cgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * cgstrate / 100), 2);
                                //double cgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + cgsttaxAmountOnSR) * SALE_RATE)), 2);
                                //double cgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - cgstExSaleRate), 2);
                                CGSTAmountForSB = Math.Round(cgsttaxAmountOnSR, 2);

                                SGSTRateForSB = sgstrate;
                                double sgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * sgstrate / 100), 2);
                                //double sgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + sgsttaxAmountOnSR) * SALE_RATE)), 2);
                                //double sgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - sgstExSaleRate), 2);
                                SGSTAmountForSB = Math.Round(sgsttaxAmountOnSR, 2);
                            }
                            else
                            {
                                IGSTRateForSB = igstrate;
                                double igsttaxAmountOnSR = (TaxableAmountForSB * igstrate / 100);
                                //double igstExSaleRate = Math.Round(Math.Abs((SaleRateForSB / (SaleRateForSB + igsttaxAmountOnSR) * SALE_RATE)), 2);
                                //double igstRateAmountOnSR = Math.Round(Math.Abs(SaleRateForSB - igstExSaleRate), 2);
                                IGSTAmountForSB = Math.Round(igsttaxAmountOnSR, 2);
                            }
                        }
                        else
                        {
                            if (CompanyStateCode == SalebilltoGstStateCode)
                            {
                                CGSTRateForSB = cgstrate;
                                double saleamount = saleRate * QUANTAL;
                                double cgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * cgstrate / 100), 2);
                                //double cgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + cgsttaxAmountOnSR) * SALE_RATE)), 2);
                                //double cgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - cgstExSaleRate), 2);
                                CGSTAmountForSB = Math.Round(cgsttaxAmountOnSR, 2);

                                SGSTRateForSB = sgstrate;
                                double sgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * sgstrate / 100), 2);
                                //double sgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + sgsttaxAmountOnSR) * SALE_RATE)), 2);
                                //double sgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - sgstExSaleRate), 2);
                                SGSTAmountForSB = Math.Round(sgsttaxAmountOnSR, 2);
                            }
                            else
                            {
                                IGSTRateForSB = igstrate;
                                double igsttaxAmountOnSR = (TaxableAmountForSB * igstrate / 100);
                                //double igstExSaleRate = Math.Round(Math.Abs((SaleRateForSB / (SaleRateForSB + igsttaxAmountOnSR) * SALE_RATE)), 2);
                                //double igstRateAmountOnSR = Math.Round(Math.Abs(SaleRateForSB - igstExSaleRate), 2);
                                IGSTAmountForSB = Math.Round(igsttaxAmountOnSR, 2);
                            }
                        }
                    }
                    else
                    {
                        if (CompanyStateCode == SalebilltoGstStateCode)
                        {
                            CGSTRateForSB = cgstrate;
                            double saleamount = saleRate * QUANTAL;
                            double cgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * cgstrate / 100), 2);
                            //double cgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + cgsttaxAmountOnSR) * SALE_RATE)), 2);
                            //double cgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - cgstExSaleRate), 2);
                            CGSTAmountForSB = Math.Round(cgsttaxAmountOnSR, 2);

                            SGSTRateForSB = sgstrate;
                            double sgsttaxAmountOnSR = Math.Round((TaxableAmountForSB * sgstrate / 100), 2);
                            //double sgstExSaleRate = Math.Round(Math.Abs((SALE_RATE / (SALE_RATE + sgsttaxAmountOnSR) * SALE_RATE)), 2);
                            //double sgstRateAmountOnSR = Math.Round(Math.Abs(SALE_RATE - sgstExSaleRate), 2);
                            SGSTAmountForSB = Math.Round(sgsttaxAmountOnSR, 2);
                        }
                        else
                        {
                            IGSTRateForSB = igstrate;
                            double igsttaxAmountOnSR = (TaxableAmountForSB * igstrate / 100);
                            //double igstExSaleRate = Math.Round(Math.Abs((SaleRateForSB / (SaleRateForSB + igsttaxAmountOnSR) * SALE_RATE)), 2);
                            //double igstRateAmountOnSR = Math.Round(Math.Abs(SaleRateForSB - igstExSaleRate), 2);
                            IGSTAmountForSB = Math.Round(igsttaxAmountOnSR, 2);
                        }
                    }
                }
                #region
                double TotalGstSaleBillAmount = Math.Round(TaxableAmountForSB + CGSTAmountForSB + SGSTAmountForSB + IGSTAmountForSB);
                double Roundoff = 0.00;
                int maxcountSBno = 0;
                int SB_NO = Convert.ToInt32(lblSB_No.Text != string.Empty ? lblSB_No.Text : "0");
                if (SB_NO > 0)
                {
                    maxcountSBno = SB_NO;
                }
                else
                {
                    maxcountSBno = 0;
                }

                if (PDS1 == "P")
                {
                    if (drpDeliveryType.SelectedValue == "C")
                    {
                        Roundoff = Math.Round(TotalGstSaleBillAmount - (TaxableAmountForSB + CGSTAmountForSB + SGSTAmountForSB + IGSTAmountForSB), 2);
                    }
                    else
                    {
                        Roundoff = Math.Round(TotalGstSaleBillAmount - (TaxableAmountForSB + CGSTAmountForSB + SGSTAmountForSB + IGSTAmountForSB), 2);
                        //Math.Round(TotalGstSaleAmount - (TaxableAmountForSB + CGSTAmountForSB + SGSTAmountForSB
                        //                              + IGSTAmountForSB), 2)
                    }
                }
                else
                {
                    if (drpDeliveryType.SelectedValue == "C")
                    {
                        Roundoff = Math.Round(TotalGstSaleBillAmount - (TaxableAmountForSB + CGSTAmountForSB + SGSTAmountForSB + IGSTAmountForSB), 2);
                    }
                    else
                    {
                        Roundoff = Math.Round(TotalGstSaleBillAmount - (TaxableAmountForSB + CGSTAmountForSB + SGSTAmountForSB + IGSTAmountForSB), 2);
                    }
                }
                #endregion


                #region Assign Values To Sale Common Class
                salePosting.SB_PURCNO = Convert.ToInt32(purchase.PS_doc_no != null ? purchase.PS_doc_no : 0);
                salePosting.SB_doc_date = DOC_DATE;
                salePosting.SB_DoNarrtion = txtNARRATION3.Text;
                if (PDS1 == "P")
                {
                    salePosting.SB_Ac_Code = Convert.ToInt32(hdnfPDSPartyCode.Value);
                    salePosting.SB_Unit_Code = Convert.ToInt32(hdnfPDSUnitCode.Value);
                    salePosting.SB_FROM_STATION = fromPlace;
                    salePosting.SB_TO_STATION = UnitCity;
                    if (drpDeliveryType.SelectedValue == "C")
                    {
                        salePosting.SB_subTotal = QUANTAL * saleRate + 0;
                    }
                    else
                    {
                        salePosting.SB_subTotal = QUANTAL * SaleRateForSB + 0;

                    }
                }
                else
                {
                    salePosting.SB_Ac_Code = Convert.ToInt32(txtSaleBillTo.Text);
                    salePosting.SB_Unit_Code = Convert.ToInt32(VOUCHER_BY);

                    salePosting.SB_FROM_STATION = fromPlace;
                    salePosting.SB_TO_STATION = saleBilltoCity;
                    if (drpDeliveryType.SelectedValue == "C")
                    {
                        salePosting.SB_subTotal = QUANTAL * saleRate + 0;
                    }
                    else
                    {
                        salePosting.SB_subTotal = QUANTAL * SaleRateForSB + 0;

                    }
                }

                salePosting.SB_LORRYNO = TRUCK_NO;
                salePosting.SB_mill_code = Convert.ToInt32(MILL_CODE);
                salePosting.SB_BROKER = BROKER_CODE;

                if (drpDeliveryType.SelectedValue == "C")
                {
                    //salePosting.SB_LESS_FRT_RATE = Convert.ToDouble(txtMemoAdvanceRate.Text != string.Empty ? txtMemoAdvanceRate.Text : "0.00");
                    //salePosting.SB_freight = MEMO_ADVANCE;

                    salePosting.SB_LESS_FRT_RATE = Convert.ToDouble(txtMemoAdvanceRate.Text != string.Empty ? txtMemoAdvanceRate.Text : "0.00") + VASULI_RATE_1;

                    salePosting.SB_freight = MEMO_ADVANCE + VASULI_AMOUNT_1;
                }
                else
                {
                    salePosting.SB_LESS_FRT_RATE = Convert.ToDouble(txtMemoAdvanceRate.Text != string.Empty ? txtMemoAdvanceRate.Text : "0.00") + VASULI_RATE_1;

                    salePosting.SB_freight = MEMO_ADVANCE + VASULI_AMOUNT_1;
                }

                salePosting.SB_cash_advance = 0.00;
                salePosting.SB_bank_commission = 0.00;
                salePosting.SB_OTHER_AMT = 0.00;
                salePosting.SB_Bill_Amount = TotalGstSaleBillAmount;
                salePosting.SB_Due_Days = 0;
                salePosting.SB_NETQNTL = QUANTAL;
                salePosting.SB_Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
                salePosting.SB_Year_Code = Convert.ToInt32(Session["year"].ToString());
                salePosting.SB_Branch_Code = Branch_Code;
                salePosting.SB_Created_By = Session["user"].ToString();
                salePosting.SB_Modified_By = Session["user"].ToString();
                salePosting.SB_Tran_Type = "SB";
                salePosting.SB_DONO = DOC_NO;

                salePosting.SB_TRANSPORT_CODE = TRANSPORT_CODE;
                salePosting.SB_RateDiff = 0.00;
                salePosting.SB_GstRateCode = GSTRateCode;
                salePosting.SB_CGSTRate = cgstrate;
                salePosting.SB_CGSTAmount = CGSTAmountForSB;
                salePosting.SB_SGSTRate = sgstrate;
                salePosting.SB_SGSTAmount = SGSTAmountForSB;
                salePosting.SB_IGSTRate = igstrate;
                salePosting.SB_IGSTAmount = IGSTAmountForSB;
                salePosting.SB_TAXABLEAMOUNT = TaxableAmountForSB;
                salePosting.SB_EWay_BillChk = EWay_BillChk;
                salePosting.SB_EwayBill_No = EWayBill_No;
                salePosting.SB_MillInvoiceno = MillInvoiceno;
                salePosting.SB_Roundoff = Roundoff;
                salePosting.SB_Purcid = Convert.ToInt32(purchase.PS_purchase_Id != null ? purchase.PS_purchase_Id : 0);
                try
                {
                    salePosting.SB_ac = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + salePosting.SB_Ac_Code + " and Company_Code=" + Session["Company_Code"].ToString() + ""));
                }
                catch
                {

                }
                try
                {
                    salePosting.SB_uc = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + salePosting.SB_Unit_Code + " and Company_Code=" + Session["Company_Code"].ToString() + ""));
                }
                catch
                {

                }
                try
                {
                    salePosting.SB_bk = Convert.ToInt32(bk);
                }
                catch
                {
                }
                try
                {
                    salePosting.SB_tc = Convert.ToInt32(tc);
                }
                catch
                {
                }
                try
                {
                    salePosting.SB_mc = Convert.ToInt32(mc);
                }
                catch
                {
                }
                #endregion


                dt2 = new DataTable();
                DataRow dr = null;
                dt2.Columns.Add((new DataColumn("ID", typeof(Int32))));
                #region [Write here columns]
                dt2.Columns.Add((new DataColumn("item_code", typeof(Int32))));
                //dt.Columns.Add((new DataColumn("item_name", typeof(string))));
                dt2.Columns.Add((new DataColumn("narration", typeof(string))));
                dt2.Columns.Add((new DataColumn("Quantal", typeof(double))));
                dt2.Columns.Add((new DataColumn("packing", typeof(Int32))));
                dt2.Columns.Add((new DataColumn("bags", typeof(Int32))));
                dt2.Columns.Add((new DataColumn("rate", typeof(double))));
                dt2.Columns.Add((new DataColumn("item_Amount", typeof(double))));
                dt2.Columns.Add((new DataColumn("saledetailid", typeof(int))));
                #endregion
                dt2.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt2.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt2.NewRow();
                dr["ID"] = 1;

                dr["SrNo"] = 0;

                #region [ Set values to dr]
                dr["item_code"] = itemcode;
                //dr["item_name"] = LBLITEMNAME.Text;
                dr["narration"] = string.Empty;
                dr["Quantal"] = QUANTAL;
                dr["packing"] = PACKING;
                dr["bags"] = BAGS;




                if (drpDeliveryType.SelectedValue == "C")
                {
                    dr["rate"] = saleRate;
                    dr["item_Amount"] = QUANTAL * saleRate + 0;
                }
                else
                {
                    dr["rate"] = SaleRateForSB;
                    dr["item_Amount"] = QUANTAL * SaleRateForSB + 0;


                }



                if (btnSave.Text == "Save")
                {
                    dr["rowAction"] = "A";
                    dr["saledetailid"] = 1;
                }
                else
                {
                    if (lblSB_No.Text == string.Empty || lblSB_No.Text == "" || lblSB_No.Text == "0")
                    {
                        dr["rowAction"] = "A";
                        dr["saledetailid"] = 1;
                    }
                    else
                    {

                        dr["rowAction"] = "U";
                        //string saleid = clsCommon.getString("select saleid from nt_1_sugarsale where  doc_no='" + lblSB_No.Text + "' " +
                        //     " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");

                        dr["saledetailid"] = Convert.ToInt32(clsCommon.getString("select saledetailid from nt_1_sugarsaledetails where  doc_no='" + lblSB_No.Text + "' " +
                            " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + ""));
                    }
                }
                dt2.Rows.Add(dr);
                #endregion
            }

            #endregion

            #endregion
        }
        catch
        {
        }
    }
    #endregion

    #region LV_Posting
    private void LV_Posting()
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



        if (DIFF_AMOUNT != 0)
        {

            GSTRateForLV = Convert.ToDouble(GSTRate);
            cgstrateForLV1 = Convert.ToDouble(cgstrate);
            sgstrateForLV1 = Convert.ToDouble(sgstrate);
            igstrateForLV1 = Convert.ToDouble(igstrate);

            //double CGSTAmountForLV = 0.0;
            //double SGSTAmountForLV = 0.0;
            //double IGSTAmountForLV = 0.0;

            //double CGSTRateForLV = 0.00;
            //double SGSTRateForLV = 0.00;
            //double IGSTRateForLV = 0.00;

            if (DESP_TYPE == "DO")
            {
                LvAmnt = DIFF_AMOUNT;
                //if (Tender_Commission != 0)
                //{
                //    //LvAmnt += QUANTAL * Tender_Commission;

                //}

                if (LvAmnt != 0)
                {
                    int CompanyGSTStateCode = 0;
                    if (Session["CompanyGSTStateCode"].ToString() == null || Session["CompanyGSTStateCode"].ToString() == string.Empty)
                    {
                        CompanyGSTStateCode = Convert.ToInt32(clsCommon.getString("select GSTStateCode from NT_1_CompanyParameters where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'"));

                        Session["CompanyGSTStateCode"] = CompanyGSTStateCode.ToString();
                        // int CompanyGSTStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
                    }
                    else
                    {
                        CompanyGSTStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
                    }
                    if (VoucherbyGstStateCode == CompanyGSTStateCode)
                    {
                        CGSTRateForLV = cgstrateForLV1;
                        CGSTAmountForLV = Math.Round(LvAmnt * CGSTRateForLV / 100, 2);

                        SGSTRateForLV = sgstrateForLV1;
                        SGSTAmountForLV = Math.Round(LvAmnt * SGSTRateForLV / 100, 2);
                    }
                    else
                    {
                        IGSTRateForLV = igstrateForLV1;
                        IGSTAmountForLV = Math.Round(LvAmnt * IGSTRateForLV / 100, 2);
                    }
                }
            }


            voucherAmountForLV = Math.Round((LvAmnt + CGSTAmountForLV + SGSTAmountForLV + IGSTAmountForLV), 2);

            SaleCGSTAc = Convert.ToInt32(Session["SaleCGSTAc"].ToString());
            SaleSGSTAc = Convert.ToInt32(Session["SaleSGSTAc"].ToString());
            SaleIGSTAc = Convert.ToInt32(Session["SaleIGSTAc"].ToString());


            PayableCGSTAc = Convert.ToInt32(Session["PurchaseCGSTAc"].ToString());
            PayableSGSTAc = Convert.ToInt32(Session["PurchaseSGSTAc"].ToString());
            PayableIGSTAc = Convert.ToInt32(Session["PurchaseIGSTAc"].ToString());

            string LVNumber = Convert.ToString(voucher_no);
            string voucherByShortName = clsCommon.getString("Select Short_Name from qrymstaccountmaster Where Company_Code="
                                          + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + VOUCHER_BY + "");
            string brokerShortName = string.Empty;
            if (BROKER_CODE != 2)
            {
                brokerShortName = clsCommon.getString("Select Short_Name from  qrymstaccountmaster Where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + BROKER_CODE + "");
            }

            LV.LV_ac = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtvoucher_by.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
            LV.LV_uc = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtvoucher_by.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
            LV.LV_tc = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtTRANSPORT_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
            LV.LV_bc = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtBroker_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));
            LV.LV_mc = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where ac_code=" + txtMILL_CODE.Text + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ""));

            LV.LV_Doc_No = Convert.ToInt32(LVNumber);
            LV.LV_Doc_Date = DOC_DATE;
            LV.LV_Link_No = 0;
            LV.LV_link_Type = "";
            LV.LV_Link_id = 0;
            LV.LV_Ac_Code = Convert.ToInt32(Ac_Code);
            LV.LV_Unit_Code = Convert.ToInt32(GETPASS_CODE);
            LV.LV_Broker_CODE = BROKER_CODE;
            LV.LV_Quantal = QUANTAL;
            LV.LV_PACKING = PACKING;
            LV.LV_BAGS = BAGS;
            LV.LV_Grade = GRADE;
            LV.LV_Transport_Code = TRANSPORT_CODE;
            LV.LV_Mill_Rate = Convert.ToDouble(txtmillRate.Text);
            LV.LV_Sale_Rate = Convert.ToDouble(txtSALE_RATE.Text);
            LV.LV_Purchase_Rate = 0;
            LV.LV_FREIGHT = 0.00;
            LV.LV_Narration1 = "V.No " + vouchnarration + " " + voucherByShortName + " " + brokerShortName + "";
            LV.LV_Narration2 = "" + myNarration2 + " Lorry No:" + TRUCK_NO + "";
            LV.LV_Narration3 = myNarration3;
            LV.LV_Narration4 = "" + myNarration4 + " " + TRUCK_NO + "";
            LV.LV_Voucher_Amount = DIFF_AMOUNT;
            LV.LV_Diff_Amount = DIFF_RATE;
            LV.LV_Company_Code = Company_Code;
            LV.LV_Year_Code = Year_Code;
            LV.LV_Branch_Code = Branch_Code;
            LV.LV_Created_By = "" + Session["user"].ToString() + "";
            LV.LV_Commission_Rate = Tender_Commission;
            LV.LV_Resale_Commisson = Tender_Commission_Amount;
            LV.LV_GstRateCode = Convert.ToInt32(txtGstRate.Text);
            LV.LV_CGSTRate = CGSTRateForLV;
            LV.LV_CGSTAmount = CGSTAmountForLV;
            LV.LV_SGSTRate = SGSTRateForLV;
            LV.LV_SGSTAmount = SGSTAmountForLV;
            LV.LV_IGSTRate = IGSTRateForLV;
            LV.LV_IGSTAmount = IGSTAmountForLV;
            LV.LV_TaxableAmount = DIFF_AMOUNT;
        }
    }
    #endregion

    #region [txtBrand_Code_TextChanged]
    protected void txtBrand_Code_TextChanged(object sender, EventArgs e)
    {
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

    protected void txtitem_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtitem_Code.Text;
        strTextBox = "txtitem_Code";
        csCalculations();
    }
    protected void btntxtitem_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtitem_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtBill_To_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To.Text;
        strTextBox = "txtBill_To";
        csCalculations();
    }
    protected void btntxtbill_To_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtBroker.Text != string.Empty)
            //{
            lblMsg.Text = "";
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBill_To";
            btnSearch_Click(sender, e);
            //}
        }
        catch
        {
        }
    }
    protected void btnPrintMotorMemo_Click(object sender, EventArgs e)
    {
        string memono = lbldoid.Text;
        if (lblMemoNo.Text != string.Empty && lblMemoNo.Text != "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:MM('" + memono + "')", true);
        }
    }
    protected void btnPrintSaleBill_Click(object sender, EventArgs e)
    {
        string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";

        string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
            " and Year_Code='" + Session["year"].ToString() + "'");
        string docnumber = txtdoc_no.Text;
        string corporatenumber = txtcarporateSale.Text;
        string sellingtype = clsCommon.getString("select selling_type from carporatehead where doc_no='" + corporatenumber +
            "' and Company_Code='" + Session["Company_code"].ToString() + "'");
        string corporate = "0";

        if (corporatenumber != string.Empty)
        {
            if (sellingtype == "P")
            {
                string DeliveryType = clsCommon.getString("select DeliveryType from carporatehead where doc_no='" + corporatenumber +
               "' and Company_Code='" + Session["Company_code"].ToString() + "'");
                if (DeliveryType == "C")
                {
                    corporate = "0";
                }
                else
                {
                    corporate = "1";
                }

            }
            else
            {
                corporate = "1";
            }
        }
        else
        {
            corporate = "0";
        }

        if (hdconfirm.Value == "Yes")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber
                + "','" + corporate + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber +
                "','" + corporate + "')", true);
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";
        //string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
        //    " and Year_Code='" + Session["year"].ToString() + "'");

        //if (hdconfirm.Value == "Yes")
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "')", true);
        //}
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);

    }


    protected void btnPrintSaleBillfright_Click(object sender, EventArgs e)
    {
        string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";

        string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
            " and Year_Code='" + Session["year"].ToString() + "'");
        string docnumber = txtdoc_no.Text;
        string corporatenumber = txtcarporateSale.Text;
        string sellingtype = clsCommon.getString("select selling_type from carporatehead where doc_no='" + corporatenumber +
            "' and Company_Code='" + Session["Company_code"].ToString() + "'");
        string corporate = "1";

        //if (corporatenumber != string.Empty)
        //{
        //    if (sellingtype == "P")
        //    {
        //        string DeliveryType = clsCommon.getString("select DeliveryType from carporatehead where doc_no='" + corporatenumber +
        //       "' and Company_Code='" + Session["Company_code"].ToString() + "'");
        //        if (DeliveryType == "C")
        //        {
        //            corporate = "0";
        //        }
        //        else
        //        {
        //            corporate = "1";
        //        }

        //    }
        //    else
        //    {
        //        corporate = "1";
        //    }
        //}
        //else
        //{
        //    corporate = "0";
        //} 
         
        if (hdconfirm.Value == "Yes")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber
                + "','" + corporate + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber +
                "','" + corporate + "')", true);
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";
        //string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
        //    " and Year_Code='" + Session["year"].ToString() + "'");

        //if (hdconfirm.Value == "Yes")
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "')", true);
        //}
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);

    }
    protected void txtMillInv_Date_TextChanged(object sender, EventArgs e)
    {

    }


    protected void btngenratesalebill_Click(object sender, EventArgs e)
    {
       
        btnSave.Text = "Update";
        hdnfgeneratesalebill.Value = "Yes";
        qry = "select max(doc_no)+1 from nt_1_sugarsale where Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "";
        string sbno = clsCommon.getString(qry);

        qry = "select saleid from nt_1_sugarsale where DO_No="
          + txtdoc_no.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "";
        string sbnosale = clsCommon.getString(qry);

        string qry4 = "update nt_1_deliveryorder set SB_No=" + sbno + ",saleid=" + sbnosale + " where doid=" + hdnf.Value + "";

        // dr = null;
        dr = Maindt.NewRow();
        dr["Querys"] = qry4;
        Maindt.Rows.Add(dr);

        string qry1 = "update nt_1_sugarsale set Doc_no=" + sbno + " where saleid=" + sbnosale + "";

        // dr = null;
        dr = Maindt.NewRow();
        dr["Querys"] = qry1;
        Maindt.Rows.Add(dr);

        string qry2 = "update Nt_1_gledger set DOC_NO=" + sbno + " where saleid=" + sbnosale + " and TRAN_TYPE='SB'";

        //   dr = null;
        dr = Maindt.NewRow();
        dr["Querys"] = qry2;


        Maindt.Rows.Add(dr);

        string qry3 = "update  nt_1_sugarsaledetails set Doc_no=" + sbno + " where saleid=" + sbnosale + "";
        dr = Maindt.NewRow();
        dr["Querys"] = qry3;


        Maindt.Rows.Add(dr);
        flag = 2;
        msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);

        if (msg == "Update")
        {
            hdnf.Value = lbldoid.Text;
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            qry = getDisplayQuery();
            this.fetchRecord(qry);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
        }

        //}
    }
    protected void btnpendingsale_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:pendingSB()", true);
    }

    #region[Database's simple Query for store procedure]
    public static DataSet returndata(string GSTCode, string storeprocedurename, string millcode, string getpasscode, string shipto, string salebillto, string billto, string transportcode, string DO, string broker, string vasualiac, string itemcode, string companycode)
    {
        try
        {

            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection sql_conn = new SqlConnection(conn);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sql_conn;
            cmd.CommandText = storeprocedurename;
            cmd.CommandType = CommandType.StoredProcedure;
            string para_name = "GSTcode";


            //an out parameter


            //an in parameter

            cmd.Parameters.AddWithValue("GSTcode", GSTCode);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;


            cmd.Parameters.AddWithValue("millcode", millcode);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("getpasscode", getpasscode);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("shipto", shipto);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("salebillto", salebillto);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("billto", billto);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("transport", transportcode);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("Docode", DO);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("broker", broker);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("vasuliac", vasualiac);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("itemcode", itemcode);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("companycode", companycode);
            cmd.Parameters[para_name].Direction = ParameterDirection.Input;
            sql_conn.Open();

            //  SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            _adapter.Fill(ds);
            return ds;
            // ds.Load(rdr);
            //   string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            //   SqlConnection conn = new SqlConnection(connStr);
            //   SqlDataAdapter da = new SqlDataAdapter("gstreturn", conn); // Using a Store Procedure.
            //   da.SelectCommand.CommandType = CommandType.StoredProcedure; // Comment if using hard coded query.
            //   DataSet ds = new DataSet(); // Definition: Memory representation of the database.
            //   da.SelectCommand.Parameters.AddWithValue("@GSTcode", 1);
            //   //da.SelectCommand.Parameters("").Direction = ParameterDirection.Input// Repeat for each parameter present in the Store Procedure.
            //  // da.SelectCommand.Parameters.AddWithValue("@GSTcode",1).Direction = ParameterDirection.Input;

            ////   da.SelectCommand.Parameters.AddWithValue("@CGSTRate", "");
            //  // da.SelectCommand.Parameters.AddWithValue("@CGSTRate", 1).Direction = ParameterDirection.Output;

            //   da.Fill(ds);
            //   DataTable dt = ds.Tables[1];
            //if (dt.rows.count > 1)
            //{

            //}
            // string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            // SqlConnection conn = new SqlConnection(connStr);
            // conn.Close();
            // SqlCommand cmd = new SqlCommand(storeprocedurename, conn);
            // conn.Open();
            // cmd.Connection = conn;
            // // cmd.CommandText = "add_emp";
            // cmd.CommandType = CommandType.StoredProcedure;
            //// cmd.Parameters.Add(new SqlParameter("GSTcode", str));
            //// cmd.Connection.Open();
            // var result = cmd.ExecuteReader();
            // cmd.Connection.Close();
            //using (MySql.Data.MySqlClient.SqlConnection conn = new MySql.Data.MySqlClient.SqlConnection(connStr))
            //{
            //    MySql.Data.MySqlClient.SqlCommand cmd = new MySql.Data.MySqlClient.SqlCommand();
            //    cmd.Connection = conn;
            //    cmd.CommandText = "CALL " + storeprocedurename + "("+str +" , @gstrate, @CGSTrate, @SGSTrate, @IGSTrate)";

            //    // cmd.CommandText = "CALL MyProcedure(@MyOutputNum1, @MyOutputString2, ?MyParam1, ?MyParam2); SELECT CAST(@MyOutputNum1 AS SIGNED), @MyOutputString2;";
            //    // I am using the ?param style to make it easy to differentiate between user variables and parameters. @MyParam1 would also work.
            //  //  cmd.Parameters.AddWithValue("@GSTcode", str);
            //    //cmd.Parameters.AddWithValue("?MyParam2", "blah");
            //    conn.Open();
            //    MySql.Data.MySqlClient.SqlDataReader rdr = cmd.ExecuteReader();
            //    // If MyProcedure returns a result set that will come first so you will need: 
            //    //   while (rdr.Read()) {...} 
            //    //   and rdr.NextResult();
            //    // Now get the output parameters.
            //    long myOutputNum1 = -1;
            //    string myOutputString2 = null;
            //    if (rdr.Read())
            //    {
            //        int i = 0;
            //        if (rdr.FieldCount > i && !rdr.IsDBNull(i)) myOutputNum1 = rdr.GetInt64(i);
            //        i++;
            //        if (rdr.FieldCount > i && !rdr.IsDBNull(i)) myOutputString2 = rdr.GetString(i);
            //    }
            //}
            //if (OpenConnection())
            //{
            //    //_sqlCmd = new SqlCommand(str, _connection);
            //    //_sqlCmd.CommandTimeout = 100;
            //    //_adapter = new SqlDataAdapter(_sqlCmd);
            //    //_ds = new DataSet();
            //    //_adapter.Fill(_ds);
            //    //return _ds;


            //    // string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            //    _sqlCmd = new SqlCommand(storeprocedurename, _connection);
            //    _sqlCmd.CommandType = CommandType.StoredProcedure;
            //    _sqlCmd.Parameters.AddWithValue("GSTcode", str);
            //    using (SqlDataAdapter sda = new SqlDataAdapter(_sqlCmd))
            //    {
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);
            //        //GridView1.DataSource = dt;
            //        //GridView1.DataBind();
            //        return _ds;
            //    }

            //}
            //else
            //{
            //    return null;
            //}
            //  return ds;
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return null;
        }
        finally
        {
            //_ds.Dispose();
            //_sqlCmd.Dispose();
            //_adapter.Dispose();
            //_connection.Close();
            //_connection.Dispose();
        }
    }
    #endregion

    [WebMethod]
    public static string XMLInsert(string XML, string RecordStatus)
    {
        string msgReturn = "";
        try
        {
            msgReturn = XML.Length.ToString();
            //   XML.Replace("''", """");
            //msgReturn = GetWordAtPosition(XML, 4313);
            XDocument XDoc = XDocument.Parse(XML, LoadOptions.None);
            XML = XDoc.ToString(SaveOptions.DisableFormatting);
            SqlCommand cmd1 = new SqlCommand();

            cmd1.CommandText = "DeliveryOrder";
            cmd1.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string spname = "DeliveryOrder";
            string xmlfile = XML;
            string op = "";
            string returnmaxno = "";
            int flag;

            if (RecordStatus == "Update")
            {
                flag = 2;
            }
            else
            {
                flag = 1;
            }
            ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);

            return returnmaxno;
        }
        catch
        {
            return "";
        }
    }
    public static string GetWordAtPosition(string text, int position)
    {
        if (text.Length - 1 < position || text[position] == ' ') return null;

        int start = position;
        int end = position;
        while (text[start] != ' ' && start > 0) start--;
        while (text[end] != ' ' && end < text.Length - 1) end++;

        return text.Substring(start == 0 ? 0 : start + 1, end - start - 1);

    }

    [WebMethod]
    public static string NewInsert(string DoInsert, string DoDetail_Insert, string PurchaseInsert, string PurchaseInsertDetail, string SaleBillInsert, string SaleBillDetailInsert, string TenderInsert, string TenderUpdate, string DoDetailUpdate, string DoDetailDelete, string GLEDGERDO_Delete, string GLEDGER_Insert, string RecordStatus)
    {
        try
        {


            cmd.CommandText = "DOSAVE";
            cmd.CommandType = CommandType.StoredProcedure;
            //an out parameter


            //an in parameter
            cmd.Parameters.AddWithValue("TenderInsert", TenderInsert);
            cmd.Parameters["TenderInsert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("TenderUpdate", TenderUpdate);
            cmd.Parameters["TenderUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("DoInsert", DoInsert);
            cmd.Parameters["DoInsert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("DoDetail_Insert", DoDetail_Insert);
            cmd.Parameters["DoDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("PurchaseInsert", PurchaseInsert);
            cmd.Parameters["PurchaseInsert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("PurchaseInsertDetail", PurchaseInsertDetail);
            cmd.Parameters["PurchaseInsertDetail"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("SaleBillInsert", SaleBillInsert);
            cmd.Parameters["SaleBillInsert"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("SaleBillDetailInsert", SaleBillDetailInsert);
            cmd.Parameters["SaleBillDetailInsert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("DoDetailUpdate", DoDetailUpdate);
            cmd.Parameters["DoDetailUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("DoDetailDelete", DoDetailDelete);
            cmd.Parameters["DoDetailDelete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("GLEDGERDO_Delete", GLEDGERDO_Delete);
            cmd.Parameters["GLEDGERDO_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("GLEDGER_Insert", GLEDGER_Insert);
            cmd.Parameters["GLEDGER_Insert"].Direction = ParameterDirection.Input;


            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            _adapter.Fill(ds);
            string msgReturn = "";
            if (RecordStatus == "Save")
            {
                msgReturn = "Record Successfully Added";
            }
            else if (RecordStatus == "Update")
            {
                msgReturn = "Record Successfully Update";
            }
            return msgReturn;

        }
        catch (Exception exx)
        {
            DOPurcSaleCRUD.LogError(exx);
            con.Close();
            return "";
        }
        finally
        {
            con.Close();
        }
    }


    protected void txtTCSRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSRate.Text;
        strTextBox = "txtTCSRate";
        // hdnfTCSratepur.Value = txtTCSRate.Text;
        csCalculations();
        calculation();
    }
    protected void txtTCSRate_Sale_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCSRate_Sale.Text;
        strTextBox = "txtTCSRate_Sale";
        //   hdnfTCSRate.Value = txtTCSRate_Sale.Text;
        csCalculations();
        calculation();

        setFocusControl(txtVasuliRate);
    }

    #region [txtnewsbdate_TextChanged]
    protected void txtnewsbdate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnewsbdate.Text;
        strTextBox = "txtnewsbdate";
        csCalculations();
    }
    #endregion
    #region [txtnewsbno_TextChanged]
    protected void txtnewsbno_TextChanged(object sender, EventArgs e)
    {
        searchString = txtnewsbno.Text;
        strTextBox = "txtnewsbno";
        csCalculations();

    }
    #endregion
    protected void txtCashDiff_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtCashDiffAc);
        calculation();
    }
    protected void txtCashDiffAc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCashDiffAc.Text;
        strTextBox = "txtCashDiffAc";
        csCalculations();
    }
    protected void btntxtCashDiffAc_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCashDiffAc";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtTDSAc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAc.Text;
        strTextBox = "txtTDSAc";
        csCalculations();
        TDSCAl();
    }
    protected void btntxtTDSAc_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTDSAc";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txttdsrate_TextChanged(object sender, EventArgs e)
    {
        TDSCAl();
        setFocusControl(txtVasuliRate);
    }
    protected void txttdsamount_TextChanged(object sender, EventArgs e)
    {

    }

    private void TDSCAl()
    {
        MemoadvanceCalculation();
        double tdsrate = Convert.ToDouble(txttdsrate.Text != string.Empty ? txttdsrate.Text : "0.00");
        double tdsamount = Convert.ToDouble(txttdsamount.Text != string.Empty ? txttdsamount.Text : "0.00");
        double memoadvance = Convert.ToDouble(txtMemoAdvance.Text != string.Empty ? txtMemoAdvance.Text : "0.00");




        double tdsrate1 = 0.00;
        double tdsamt1 = 0.00;
        tdsamt1 = tdsrate * memoadvance / 100;
        txttdsamount.Text = tdsamt1.ToString();
    }
    protected void txtMemoGSTRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMemoGSTRate.Text;
        strTextBox = "txtMemoGSTRate";
        csCalculations();
    }
    protected void btntxtMemoGSTRate_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMemoGSTRate";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
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
                string SBNo = txtdoc_no.Text;
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
                            qry = "update nt_1_deliveryorder set ackno='NA',einvoiceno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_no=" + txtdoc_no.Text;
                            ds = clsDAL.SimpleQuery(qry);
                            qry1 = "update nt_1_sugarsale set ackno='NA',einvoiceno='NA' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + txtnewsbno.Text;
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
    protected void btnpreprintourdo_Click(object sender, EventArgs e)
    {
        try
        {
            string tenderno = txtPurcNo.Text;
            string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //if (!string.IsNullOrWhiteSpace(ccMail))
            //{
            //    ccMail = "," + ccMail;
            //}
            //string millEmail = txtMillEmailID.Text + ccMail;
            string do_no = lbldoid.Text;
            string a = "0";
            string bss = "N";

            bss = hdnfpaymentcomnfirm.Value;
            string paymenttonew = "0";
            if (drpDOType.SelectedValue == "DI")
            {
                paymenttonew = grdDetail.Rows[0].Cells[4].Text;
            }
            if (hdconfirm.Value == "Yes")
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:od1('" + do_no + "','" + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "')", true);

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:od('" + do_no + "','"
                    //    + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "','" + bss + "','" + paymenttonew + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
            else
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:od1('" + do_no + "','" + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
        }
        catch
        {

        }
    }
    protected void btnpreprintSB_Click(object sender, EventArgs e)
    {
        string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";
        string docnumber = txtdoc_no.Text;
        string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
            " and Year_Code='" + Session["year"].ToString() + "' and DO_No='" + docnumber + "'");

        string corporatenumber = txtcarporateSale.Text;
        string sellingtype = clsCommon.getString("select selling_type from carporatehead where doc_no='" + corporatenumber +
            "' and Company_Code='" + Session["Company_code"].ToString() + "'");
        string corporate = "0";

        if (corporatenumber != string.Empty)
        {
            if (sellingtype == "P")
            {
                string DeliveryType = clsCommon.getString("select DeliveryType from carporatehead where doc_no='" + corporatenumber +
               "' and Company_Code='" + Session["Company_code"].ToString() + "'");
                if (DeliveryType == "C")
                {
                    corporate = "0";
                }
                else
                {
                    corporate = "1";
                }

            }
            else
            {
                corporate = "1";
            }
        }
        else
        {
            corporate = "0";
        }

        if (hdconfirm.Value == "Yes")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber +
               "','" + corporate + "')", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber
            //    + "','" + corporate + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber +
                "','" + corporate + "')", true);
        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";
        //string saleid = clsCommon.getString("select saleid from qrysalehead where doc_no='" + lblSB_No.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
        //    " and Year_Code='" + Session["year"].ToString() + "'");

        //if (hdconfirm.Value == "Yes")
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB1('" + saleid + "','" + billto + "')", true);
        //}
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + saleid + "','" + billto + "')", true);

    }
    protected void txtPurchaseTDS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPurchaseTDS.Text;
        strTextBox = "txtPurchaseTDS";
        // hdnfPurchaseTDS.Value = txtPurchaseTDS.Text;
        csCalculations();
        calculation();

        setFocusControl(txtVasuliRate);
    }
    protected void txtSaleTDS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSaleTDS.Text;
        strTextBox = "txtSaleTDS";
        // hdnfSaleTDS.Value = txtSaleTDS.Text;
        csCalculations();
        calculation();

        setFocusControl(txtVasuliRate);
    }
    protected void txtpurchaserate_TextChanged(object sender, EventArgs e)
    {
        strTextBox = "txtpurchaserate";
        csCalculations();
        calculation();
        GSTCalculations();
        setFocusControl(txtCashDiff);
    }

    protected void btnDO_Click(object sender, EventArgs e)
    {
        try
        {
            string tenderno = txtPurcNo.Text;
            string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            string do_no = lbldoid.Text;
            string a = "0";
            string bss = "N";

            bss = hdnfpaymentcomnfirm.Value;
            string paymenttonew = "0";
            if (drpDOType.SelectedValue == "DI")
            {
                paymenttonew = grdDetail.Rows[0].Cells[4].Text;
            }
            if (do_no != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "knb", "javascript:donew('" + do_no + "','"
                    + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "','N','" + paymenttonew + "')", true);

            }
        }
        catch
        {

        }
    }
    protected void btnEditDetails_Click(object sender, EventArgs e)
    {
        pnlPopupDetails.Style["display"] = "block";
        this.showDetailsRow(grdDetail.Rows[0]);
        btnAdddetails.Text = "Update";
        //    pnlPopupDetails.Style["display"] = "block";
    }
    #region [txtdo_date_TextChanged]
    protected void txtdo_date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdo_date.Text;
        strTextBox = "txtdo_date";
        csCalculations();
    }
    #endregion
    protected void btnCustomizeDO_Click(object sender, EventArgs e)
    {
        #region
        //try
        //{
        //    string tenderno = txtPurcNo.Text;
        //    string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //if (!string.IsNullOrWhiteSpace(ccMail))
        //    //{
        //    //    ccMail = "," + ccMail;
        //    //}
        //    //string millEmail = txtMillEmailID.Text + ccMail;
        //    string do_no = lbldoid.Text;
        //    string a = "0";
        //    string bss = "N";

        //    bss = hdnfpaymentcomnfirm.Value;
        //    string paymenttonew = "0";
        //    if (drpDOType.SelectedValue == "DI")
        //    {
        //        paymenttonew = grdDetail.Rows[0].Cells[4].Text;
        //    }
        //    if (hdconfirm.Value == "Yes")
        //    {
        //        if (do_no != string.Empty)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:cd('" + do_no + "','"
        //                + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "','" + bss + "','" + paymenttonew + "')", true);
        //            //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
        //        }
        //    }
        //    else
        //    {
        //        if (do_no != string.Empty)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:cd1('" + do_no + "','" + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "')", true);
        //            //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
        //        }
        //    }
        //}
        //catch
        //{

        //}
        #endregion

        try
        {
            string tenderno = txtPurcNo.Text;
            string ccMail = clsCommon.getString("Select Email_Id from qrymstaccountmaster where Ac_Code='" + txtMILL_CODE.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //if (!string.IsNullOrWhiteSpace(ccMail))
            //{
            //    ccMail = "," + ccMail;
            //}
            //string millEmail = txtMillEmailID.Text + ccMail;
            string do_no = lbldoid.Text;
            string a = "0";
            string bss = "N";

            bss = hdnfpaymentcomnfirm.Value;
            string paymenttonew = "0";
            if (drpDOType.SelectedValue == "DI")
            {
                paymenttonew = grdDetail.Rows[0].Cells[4].Text;
            }
            if (hdconfirm.Value == "Yes")
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:Customize('" + do_no + "','"
                        + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "','" + bss + "','" + paymenttonew + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
            else
            {
                if (do_no != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:cd1('" + do_no + "','" + txtMILL_CODE.Text + "','O','" + a + "','" + tenderno + "')", true);
                    //Response.Redirect("../Report/rptDO.aspx?do_no=" + do_no + "&email=" + millEmail, true);
                }
            }
        }
        catch
        {

        }
    }

    protected void btnCustomizeSaleBill_Click(object sender, EventArgs e)
    {
        string billto = txtBill_To.Text != string.Empty ? txtBill_To.Text : "0";

        string saleid = clsCommon.getString("select saleid from qrysalehead where DO_No='" + txtdoc_no.Text + "' and Company_Code='" + Session["Company_code"].ToString() + "' " +
            " and Year_Code='" + Session["year"].ToString() + "'");
        string docnumber = txtdoc_no.Text;
        string corporatenumber = txtcarporateSale.Text;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:CI('" + saleid + "','" + billto + "','" + docnumber + "','" + corporatenumber + "')", true);
    }


    protected void txtInsurance_TextChanged(object sender, EventArgs e)
    {

    }
    protected void drpInsured_SelectedIndexChanged(object sender, EventArgs e)
    {
        //AmtCalculation();
    }
}