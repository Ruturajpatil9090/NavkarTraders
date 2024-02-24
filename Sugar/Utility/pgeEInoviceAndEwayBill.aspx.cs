using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

public partial class Sugar_Utility_pgeEInoviceAndEwayBill : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string qry1 = string.Empty;
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
    string Trans_Date;
    string CGST_Rate;
    string SGST_Rate;
    string IGST_Rate;
    string Dono;
    string ID;
    string State_Code_BillTo;
    string State_Code_Mill;
    int Rowaction = 2;
    int Srno = 3;

    string type = "";
    string Sale_Bill = "";
    string trntype = "";
    string carporateSale = string.Empty;
    string DoIDNo = "";
    string selectedyear = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Dono = Request.QueryString["dono"];
        ID = Request.QueryString["ID"];
        type = Request.QueryString["Type"];
        Sale_Bill = Request.QueryString["Sale_Bill"];
        trntype = Request.QueryString["trntype"];
        carporateSale = Request.QueryString["carporateSale"];
        DoIDNo = Request.QueryString["DoIDNo"];
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "EwayBill";
            tblDetails = "";
            qryCommon = "EwayBill";
            user = Session["user"].ToString(); 
            selectedyear = Session["selectedyear"].ToString();

            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {

                    ViewState["currentTable"] = null;
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";



                    if (type == "SB")
                    {
                        BindEwayBill();
                    }
                    else if (type == "RR")
                    {

                        BindRetailEwayBill();
                    }
                    else if (type == "RS")
                    {

                        BindReturnSaleEwayBill();
                    }
                    else if (type == "LV")
                    {

                        BindCommissionEwayBill();
                    }
                    else if (type == "RB")
                    {

                        BindServiceEwayBill();
                    }
                    else if (type == "PR")
                    {

                        BindReturnPrurchaseEwayBill();
                    }
                    else if (type == "DN" || type == "CN" || type == "DS" || type == "CS")
                    {

                        BindDebitCreditEwayBill();
                    }
                    else
                    {
                    }
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
                if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
                {

                }
                else
                {

                }
            }
        }
        catch
        {
        }
    }



    private void BindEwayBill()
    {
        try
        {
            string qryelement = string.Empty;
            if (ID == string.Empty)
            {
                if (carporateSale == string.Empty)
                {
                    qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No,UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address," +
                                      " Upper(Buyer_City) as Buyer_City,(case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                                      " upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id,Upper(DispatchGst_No) as DispatchGst_No," +
                                      " upper(Dispatch_Name) as Dispatch_Name,upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
                                      " Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
                                      " upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
                                      " upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode,NETQNTL,rate," +
                                      " CGSTAmount,SGSTAmount,IGSTAmount,TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate,isnull(IGSTRate,0) as IGSTRate,Distance,LORRYNO,System_Name_E,HSN,GSTRate,LESS_FRT_RATE from NT_1qryEInvoice  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                     + " and DO_No=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                }
                else
                {
                    qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No," +
                                 "UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City," +
                                 "(case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                                 "upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id," +
                                 "Upper(DispatchGst_No) as DispatchGst_No, upper(Dispatch_Name) as Dispatch_Name," +
                                 "upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
                                 "Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
                                 "upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
                                 "upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode," +
                                 "NETQNTL,rate, CGSTAmount,SGSTAmount,IGSTAmount,TaxableAmount,isnull(CGSTRate,0) as CGSTRate," +
                                 "isnull(SGSTRate,0) as SGSTRate,isnull(IGSTRate,0) as IGSTRate,0 as Distance,LORRYNO,System_Name_E,HSN," +
                                 "GSTRate,LESS_FRT_RATE from NT_1qryEInvoiceCarporateSale  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                 + " and DO_No=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Carporate_Sale_No!=0";
                }

            }
            else
            {
                qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No,UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address," +
                                 " Upper(Buyer_City) as Buyer_City,(case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                                 " upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id,Upper(DispatchGst_No) as DispatchGst_No," +
                                 " upper(Dispatch_Name) as Dispatch_Name,upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
                                 " Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
                                 " upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
                                 " upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode,NETQNTL,rate," +
                                 " CGSTAmount,SGSTAmount,IGSTAmount,TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate,isnull(IGSTRate,0) as IGSTRate,Distance,LORRYNO,System_Name_E,HSN,GSTRate,LESS_FRT_RATE from NT_1qryEInvoice  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            }
            #region[from query]
            //string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string comnm = qrynm.ToUpper();
            // string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string state = clsCommon.getString("select LOWER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");

            //string mobno = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            // string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string companyemail = Session["EmailId"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }
                     
                        string doc_no = type + selectedyear +'-'+ dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        drpDoc_Type.SelectedValue = "INV";
                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtDispatch_GSTNo.Text = dt.Rows[0]["DispatchGst_No"].ToString();
                        txtDispatch_Name.Text = dt.Rows[0]["Dispatch_Name"].ToString();
                        txtDispatch_Address.Text = dt.Rows[0]["Dispatch_Address"].ToString();
                        txtDispatch_Location.Text = dt.Rows[0]["DispatchCity_City"].ToString();
                        txtDispatch_StateCode.Text = dt.Rows[0]["Dispatch_GSTStateCode"].ToString();
                        txtDispatch_PincodeCode.Text = dt.Rows[0]["Dispatch_Pincode"].ToString();

                        txtShip_To_GSTNo.Text = dt.Rows[0]["ShipToGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["ShipTo_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["ShipTo_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["ShipTo_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["ShipTo_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        drpIs_Service.SelectedValue = "N";
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";

                        double rate = Convert.ToDouble(dt.Rows[0]["rate"].ToString());
                        double LESS_FRT_RATE = Convert.ToDouble(dt.Rows[0]["LESS_FRT_RATE"].ToString());
                        txtUnit_Price.Text = (rate + LESS_FRT_RATE).ToString();

                        //txtUnit_Price.Text = dt.Rows[0]["rate"].ToString();LESS_FRT_RATE



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = dt.Rows[0]["GSTRate"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();



                        txtMode_of_Payment.Text = Mode_of_Payment;
                        txtAccount_Details.Text = Account_Details;
                        txtBranch.Text = Branch;
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }
    private void BindRetailEwayBill()
    {
        try
        {



            //string qryelement = "select  Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(Gst_No) as BuyerGst_No," +
            //                    " UPPER(Ac_Name_E) as Buyer_Name,UPPER(Address_E) as Buyer_Address, Upper(city_name_e) as Buyer_City," +
            //                     " (case Pincode when 0 then 999999 else Pincode end) as Buyer_Pincode, " +
            //                    " upper(State_Name) as Buyer_State_name,GSTStateCode as Buyer_State_Code,OffPhone as Buyer_Phno," +
            //                    " Email_Id as Buyer_Email_Id,upper(Gst_No) as ShipToGst_No,upper(Ac_Name_E) as ShipTo_Name,upper(Address_E) as ShipTo_Address, " +
            //                     " upper(city_name_e) as ShipTo_City,GSTStateCode as ShipTo_GSTStateCode,(case Pincode when 0 then 999999 else Pincode end) as ShipTo_Pincode," +
            //                     " Quantity as NETQNTL,rate, CGSTAmount,SGSTAmount,IGSTAmount,Total as TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate," +
            //                     "isnull(IGSTRate,0) as IGSTRate,Vehical_No as LORRYNO,0 as Distance,KgPerKatta,HSN from NT_1qryEInvoiceRetailSale  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());


            string qryelement = "select  Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No, UPPER(Buyer_Name) as Buyer_Name," +
                                " UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City, (case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                                " upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code as Buyer_State_Code,Buyer_Phno as Buyer_Phno," +
                                " Buyer_Email_Id as Buyer_Email_Id,upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name," +
                                " upper(ShipTo_Address) as ShipTo_Address,  upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode as ShipTo_GSTStateCode," +
                                " (case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode, NETQNTL as NETQNTL,rate, CGST_Amount as CGSTAmount,SGST_Amount as SGSTAmount," +
                                " IGST_Amount as IGSTAmount,Taxable_Amount as TaxableAmount,isnull(DetailCGST_Rate,0) as CGSTRate,isnull(DetailSGST_Rate,0) as SGSTRate," +
                                " isnull(DetailIGST_Rate,0) as IGSTRate,Vahical_No as LORRYNO,0 as Distance,KgPerKatta,HSN,System_Name_E from NT_1qryEInvoiceRetailSale " +
                                " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            #region[from query]
            //string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            //string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string state = clsCommon.getString("select LOWER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


            //string mobno = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();
            //string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;

            string companyemail = Session["EmailId"].ToString();

            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }
                        string doc_no = "RR" + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        drpDoc_Type.SelectedValue = "INV";
                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtDispatch_GSTNo.Text = gstno;
                        txtDispatch_Name.Text = comnm;
                        txtDispatch_Address.Text = address;
                        txtDispatch_Location.Text = city;
                        txtDispatch_StateCode.Text = companystatecode;
                        txtDispatch_PincodeCode.Text = pin;

                        txtShip_To_GSTNo.Text = dt.Rows[0]["ShipToGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["ShipTo_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["ShipTo_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["ShipTo_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["ShipTo_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        drpIs_Service.SelectedValue = "N";
                        double kgrate = Convert.ToDouble(dt.Rows[0]["KgPerKatta"].ToString());
                        double qtl = Convert.ToDouble(dt.Rows[0]["NETQNTL"].ToString());
                        // double Fqtl = Math.Round((qtl * kgrate) / 100, 2);
                        double Fqtl = Math.Round(qtl / 100, 2);
                        txtQty.Text = Fqtl.ToString();
                        txtUnit.Text = "QTL";
                        double unitprise = Convert.ToDouble(dt.Rows[0]["rate"].ToString());
                        double funitprise = Math.Round((unitprise / 105) * 100, 2);
                        txtUnit_Price.Text = funitprise.ToString();



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = "5";
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();

                        txtMode_of_Payment.Text = "Cash";
                        txtAccount_Details.Text = "61025337545";
                        txtBranch.Text = "SBIN0011782";
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }

    private void BindReturnSaleEwayBill()
    {
        try
        {
            string qryelement = string.Empty;



            qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No," +
                       " UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City," +
                       " (case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                       " upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id," +
                       " Upper(DispatchGst_No) as DispatchGst_No, upper(Dispatch_Name) as Dispatch_Name," +
                       " upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
                       " Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
                       " upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
                       " upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode," +
                       " NETQNTL,rate, CGSTAmount,SGSTAmount,IGSTAmount,subTotal as TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate,isnull(IGSTRate,0) as IGSTRate,0 as Distance,LORRYNO,System_Name_E,HSN,GSTRate from NT_1qryEInvoiceReturnSale  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            #region[from query]
            //string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string comnm = qrynm.ToUpper();
            // string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string state = clsCommon.getString("select LOWER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");

            //string mobno = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            //string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string companyemail = Session["EmailId"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }

                        string doc_no = type + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        drpDoc_Type.SelectedValue = "INV";
                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtDispatch_GSTNo.Text = dt.Rows[0]["DispatchGst_No"].ToString();
                        txtDispatch_Name.Text = dt.Rows[0]["Dispatch_Name"].ToString();
                        txtDispatch_Address.Text = dt.Rows[0]["Dispatch_Address"].ToString();
                        txtDispatch_Location.Text = dt.Rows[0]["DispatchCity_City"].ToString();
                        txtDispatch_StateCode.Text = dt.Rows[0]["Dispatch_GSTStateCode"].ToString();
                        txtDispatch_PincodeCode.Text = dt.Rows[0]["Dispatch_Pincode"].ToString();

                        txtShip_To_GSTNo.Text = dt.Rows[0]["ShipToGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["ShipTo_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["ShipTo_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["ShipTo_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["ShipTo_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        drpIs_Service.SelectedValue = "N";
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";
                        txtUnit_Price.Text = dt.Rows[0]["rate"].ToString();



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = dt.Rows[0]["GSTRate"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();



                        txtMode_of_Payment.Text = Mode_of_Payment;
                        txtAccount_Details.Text = Account_Details;
                        txtBranch.Text = Branch;
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }

    private void BindCommissionEwayBill()
    {
        try
        {
            string qryelement = string.Empty;




            qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No," +
                       " UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City," +
                       " (case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode, upper(Buyer_State_name) as Buyer_State_name," +
                       " Buyer_State_Code,Buyer_Phno,Buyer_Email_Id,NETQNTL,0 as rate, " +
                       " CGSTAmount,SGSTAmount,IGSTAmount,TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate," +
                       " isnull(IGSTRate,0) as IGSTRate,0 as Distance,'' as LORRYNO,System_Name_E,HSN,GSTRate,TCS_Net_Payable,sale_rate,purc_rate,mill_rate,resale_rate,isnull(isservice,'N') as IsService from NT_1qryEInvoiceCommisionBill  where " +
                       " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            #region[from query]




            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            //string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string companyemail = Session["EmailId"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }
                        string doc_no = type + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        double amt = Convert.ToDouble(dt.Rows[0]["TCS_Net_Payable"].ToString());

                        double millrate = Convert.ToDouble(dt.Rows[0]["mill_rate"].ToString());
                        double salerate = Convert.ToDouble(dt.Rows[0]["sale_rate"].ToString());
                        double purcrate = Convert.ToDouble(dt.Rows[0]["purc_rate"].ToString());
                        double resalerate = Convert.ToDouble(dt.Rows[0]["resale_rate"].ToString());
                        //if (salerate != 0.00)
                        //{
                        //    if (salerate > millrate)
                        //    {
                        //        drpDoc_Type.SelectedValue = "DBN";
                        //    }
                        //    else
                        //    {
                        //        drpDoc_Type.SelectedValue = "CRN";
                        //    }
                        //}
                        //if (purcrate != 0.00)
                        //{
                        //    if (purcrate > millrate)
                        //    {
                        //        drpDoc_Type.SelectedValue = "CRN";
                        //    }
                        //    else
                        //    {
                        //        drpDoc_Type.SelectedValue = "DBN";
                        //    }
                        //}
                        drpDoc_Type.SelectedValue = "INV";

                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtDispatch_GSTNo.Text = gstno;
                        txtDispatch_Name.Text = comnm;
                        txtDispatch_Address.Text = address;
                        txtDispatch_Location.Text = city;
                        txtDispatch_StateCode.Text = companystatecode;
                        txtDispatch_PincodeCode.Text = pin;

                        txtShip_To_GSTNo.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["Buyer_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        string IsService = dt.Rows[0]["IsService"].ToString();
                        if (IsService == "Y")
                        {
                            drpIs_Service.SelectedValue = "Y";
                        }
                        else
                        {
                            drpIs_Service.SelectedValue = "N";
                        }
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";



                        double rate = 0.00;
                        if (salerate != 0.00)
                        {
                            if (salerate > millrate)
                            {
                                rate = (salerate - millrate) + resalerate;
                            }
                            else
                            {
                                rate = (millrate - salerate) + resalerate;

                            }
                        }

                        if (purcrate != 0.00)
                        {
                            if (purcrate > millrate)
                            {
                                rate = (millrate - purcrate) + resalerate;
                            }
                            else
                            {
                                rate = (purcrate - millrate) + resalerate;

                            }
                        }

                        txtUnit_Price.Text = rate.ToString();



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = dt.Rows[0]["GSTRate"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();



                        txtMode_of_Payment.Text = Mode_of_Payment;
                        txtAccount_Details.Text = Account_Details;
                        txtBranch.Text = Branch;
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }

    private void BindServiceEwayBill()
    {
        try
        {
            string qryelement = string.Empty;

            qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No," +
                       " UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City," +
                       " (case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                       " upper(Buyer_State_name) as Buyer_State_name, Buyer_State_Code,Buyer_Phno,Buyer_Email_Id,0 as NETQNTL,0 as rate," +
                       " CGSTAmount,SGSTAmount,IGSTAmount,TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate," +
                       " isnull(IGSTRate,0) as IGSTRate,0 as Distance,'' as LORRYNO,System_Name_E,HSN,GSTRate,IsService" +
                       " from NT_1qryEInvoiceServiceBill  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and IsDeleted!=0 and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            #region[from query]




            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            //string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string companyemail = Session["EmailId"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }
                        //string doc_no = type + dt.Rows[0]["Doc_No"].ToString();
                        //txtDoc_No.Text = doc_no;
                        string docno = dt.Rows[0]["Doc_No"].ToString();
                        docno = docno.PadLeft(6, '0');
                        txtDoc_No.Text = type + selectedyear + '-' + docno;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        drpDoc_Type.SelectedValue = "INV";


                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtDispatch_GSTNo.Text = gstno;
                        txtDispatch_Name.Text = comnm;
                        txtDispatch_Address.Text = address;
                        txtDispatch_Location.Text = city;
                        txtDispatch_StateCode.Text = companystatecode;
                        txtDispatch_PincodeCode.Text = pin;

                        txtShip_To_GSTNo.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["Buyer_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        string IsService = dt.Rows[0]["IsService"].ToString();
                        //if (IsService != string.Empty)
                        //{
                        //    drpIs_Service.SelectedValue = dt.Rows[0]["IsService"].ToString();
                        //}
                        //else
                        //{
                        //    drpIs_Service.SelectedValue = "N";
                        //}
                        drpIs_Service.SelectedValue = "Y";
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";
                        txtUnit_Price.Text = dt.Rows[0]["rate"].ToString();



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = dt.Rows[0]["GSTRate"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();



                        txtMode_of_Payment.Text = Mode_of_Payment;
                        txtAccount_Details.Text = Account_Details;
                        txtBranch.Text = Branch;
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }

    private void BindReturnPrurchaseEwayBill()
    {
        try
        {
            string qryelement = string.Empty;



            qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No," +
                       " UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City," +
                       " (case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                       " upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id," +
                       " Upper(DispatchGst_No) as DispatchGst_No, upper(Dispatch_Name) as Dispatch_Name," +
                       " upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
                       " Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
                       " upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
                       " upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode," +
                       " NETQNTL,rate, CGSTAmount,SGSTAmount,IGSTAmount,subTotal as TaxableAmount,isnull(CGSTRate,0) as CGSTRate,isnull(SGSTRate,0) as SGSTRate,isnull(IGSTRate,0) as IGSTRate,0 as Distance,LORRYNO,System_Name_E,HSN,GSTRate from NT_1qryEInvoiceReturnPurchase  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            #region[from query]
            //string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string comnm = qrynm.ToUpper();
            // string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            // string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string state = clsCommon.getString("select LOWER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");

            //string mobno = clsCommon.getString("select Mobile_No from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));



            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            //string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string companyemail = Session["EmailId"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }
                        string doc_no = type + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        drpDoc_Type.SelectedValue = "CRN";
                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtDispatch_GSTNo.Text = dt.Rows[0]["DispatchGst_No"].ToString();
                        txtDispatch_Name.Text = dt.Rows[0]["Dispatch_Name"].ToString();
                        txtDispatch_Address.Text = dt.Rows[0]["Dispatch_Address"].ToString();
                        txtDispatch_Location.Text = dt.Rows[0]["DispatchCity_City"].ToString();
                        txtDispatch_StateCode.Text = dt.Rows[0]["Dispatch_GSTStateCode"].ToString();
                        txtDispatch_PincodeCode.Text = dt.Rows[0]["Dispatch_Pincode"].ToString();

                        txtShip_To_GSTNo.Text = dt.Rows[0]["ShipToGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["ShipTo_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["ShipTo_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["ShipTo_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["ShipTo_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        drpIs_Service.SelectedValue = "N";
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";
                        txtUnit_Price.Text = dt.Rows[0]["rate"].ToString();



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = dt.Rows[0]["GSTRate"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();



                        txtMode_of_Payment.Text = Mode_of_Payment;
                        txtAccount_Details.Text = Account_Details;
                        txtBranch.Text = Branch;
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }

    private void BindDebitCreditEwayBill()
    {
        try
        {
            string qryelement = string.Empty;

            //qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No,UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address," +
            //                 " Upper(Buyer_City) as Buyer_City,(case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
            //                 " upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id,Upper(DispatchGst_No) as DispatchGst_No," +
            //                 " upper(Dispatch_Name) as Dispatch_Name,upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
            //                 " Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
            //                 " upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
            //                 " upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode,0.00 as NETQNTL,0.00 as rate," +
            //                 " 0.00 as CGSTAmount,0.00 as SGSTAmount,0.00 as IGSTAmount,0.00 as TaxableAmount,0 as CGSTRate,0.00 as SGSTRate,0 IGSTRate,0 as Distance,'' as LORRYNO,'Sugar' as System_Name_E,1701 as HSN,5 as GSTRate from NT_1qryEInvoiceDebitCredit  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //                + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

            qryelement = "select doc_no as Doc_No,CONVERT(varchar,doc_date,103) as doc_date,UPPER(BuyerGst_No) as BuyerGst_No," +
                         "UPPER(Buyer_Name) as Buyer_Name,UPPER(Buyer_Address) as Buyer_Address, Upper(Buyer_City) as Buyer_City," +
                         "(case Buyer_Pincode when 0 then 999999 else Buyer_Pincode end) as Buyer_Pincode," +
                         "upper(Buyer_State_name) as Buyer_State_name,Buyer_State_Code,Buyer_Phno,Buyer_Email_Id," +
                         "Upper(DispatchGst_No) as DispatchGst_No, upper(Dispatch_Name) as Dispatch_Name," +
                         "upper(Dispatch_Address) as Dispatch_Address,Upper(DispatchCity_City) as DispatchCity_City," +
                         "Dispatch_GSTStateCode,(case Dispatch_Pincode when 0 then 999999 else Dispatch_Pincode end) as Dispatch_Pincode," +
                         "upper(ShipToGst_No) as ShipToGst_No,upper(ShipTo_Name) as ShipTo_Name,upper(ShipTo_Address) as ShipTo_Address," +
                         "upper(ShipTo_City) as ShipTo_City,ShipTo_GSTStateCode,(case ShipTo_Pincode when 0 then 999999 else ShipTo_Pincode end) as ShipTo_Pincode,0.00 as NETQNTL" +
                         ", rate, cgst_amount as CGSTAmount,sgst_amount as SGSTAmount,igst_amount as IGSTAmount,texable_amount as TaxableAmount,cgst_rate as CGSTRate," +
                         "sgst_rate as SGSTRate,igst_rate IGSTRate,0 as Distance,'' as LORRYNO,'Sugar' as System_Name_E,1701 as HSN,ShipToEmailID,ShipToOffPhone,ShipToState_Name" +
                         " from NT_1qryEInvoiceDebitCredit  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + type + "' ";

            #region[from query]



            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            // string companyemail = clsCommon.getString("select EmailId from tbluser where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string companystatecode = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

            string Mode_of_Payment = clsCommon.getString("select Mode_of_Payment from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Account_Details = clsCommon.getString("select Account_Details from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string Branch = clsCommon.getString("select Branch from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
            string companyemail = Session["EmailId"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double OtherCharges = 0.00;
            double Assessable_Value = 0.00;
            double Assessable_Amount = 0.00;
            double StCesVal = 0.00;
            double Cess_Value = 0.00;
            string Bill_To;
            string Ship_To;
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qryelement);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        drpSupply_Type.SelectedValue = "B2B";
                        hdnfTaxSch.Value = "GST";
                        drpRegRev_Type.SelectedValue = "N";
                        hdnfType.Value = "REG";
                        string IGSTOnIntra = dt.Rows[0]["IGSTRate"].ToString();

                        string buyer = dt.Rows[0]["Buyer_Name"].ToString();
                        string seller = comnm;
                        if (seller != buyer)
                        {
                            drpIGSTOnIntra.SelectedValue = "N";
                        }
                        else
                        {
                            drpIGSTOnIntra.SelectedValue = "Y";
                        }
                        string doc_no = type + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        if (type == "DN" || type == "D2")
                        {
                            drpDoc_Type.SelectedValue = "DBN";
                        }
                        else
                        {
                            drpDoc_Type.SelectedValue = "CRN";
                        }
                        txtSellerGST_No.Text = gstno;
                        txtSeller_Name.Text = comnm;
                        txtSeller_Address.Text = address;
                        txtSeller_Location.Text = city;
                        txtSeller_PIN.Text = pin;
                        txtSeller_StateName.Text = state;
                        txtSeller_StateCode.Text = companystatecode;
                        txtSeller_Phno.Text = mobno;
                        txtSeller_Email.Text = companyemail;

                        //txtBuyerGST_No.Text = dt.Rows[0]["BuyerGst_No"].ToString();
                        //txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString();
                        //txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        //txtBuyer_Location.Text = dt.Rows[0]["Buyer_City"].ToString();
                        //txtBuyer_PIN.Text = dt.Rows[0]["Buyer_Pincode"].ToString();
                        //txtBuyer_StateName.Text = dt.Rows[0]["Buyer_State_name"].ToString();
                        //txtBuyer_StateCode.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        //txtBuyer_Pos.Text = dt.Rows[0]["Buyer_State_Code"].ToString();
                        //txtByuer_Phno.Text = dt.Rows[0]["Buyer_Phno"].ToString();
                        //txtBuyer_Email.Text = dt.Rows[0]["Buyer_Email_Id"].ToString();

                        txtBuyerGST_No.Text = dt.Rows[0]["ShipToGst_No"].ToString();
                        txtBuyerName.Text = dt.Rows[0]["ShipTo_Name"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["ShipTo_Address"].ToString();
                        txtBuyer_Location.Text = dt.Rows[0]["ShipTo_City"].ToString();
                        txtBuyer_PIN.Text = dt.Rows[0]["ShipTo_Pincode"].ToString();
                        txtBuyer_StateName.Text = dt.Rows[0]["ShipToState_Name"].ToString();
                        txtBuyer_StateCode.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtBuyer_Pos.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtByuer_Phno.Text = dt.Rows[0]["ShipToOffPhone"].ToString();
                        txtBuyer_Email.Text = dt.Rows[0]["ShipToEmailID"].ToString();

                        txtDispatch_GSTNo.Text = dt.Rows[0]["DispatchGst_No"].ToString();
                        txtDispatch_Name.Text = dt.Rows[0]["Dispatch_Name"].ToString();
                        txtDispatch_Address.Text = dt.Rows[0]["Dispatch_Address"].ToString();
                        txtDispatch_Location.Text = dt.Rows[0]["DispatchCity_City"].ToString();
                        txtDispatch_StateCode.Text = dt.Rows[0]["Dispatch_GSTStateCode"].ToString();
                        txtDispatch_PincodeCode.Text = dt.Rows[0]["Dispatch_Pincode"].ToString();

                        txtShip_To_GSTNo.Text = dt.Rows[0]["ShipToGst_No"].ToString();
                        txtShip_Name.Text = dt.Rows[0]["ShipTo_Name"].ToString();
                        txtShipTo_Address.Text = dt.Rows[0]["ShipTo_Address"].ToString();
                        txtShip_To_Location.Text = dt.Rows[0]["ShipTo_City"].ToString();
                        txtShip_TostateCode.Text = dt.Rows[0]["ShipTo_GSTStateCode"].ToString();
                        txtShip_To_PinCode.Text = dt.Rows[0]["ShipTo_Pincode"].ToString();


                        hdnfSlNo.Value = "1";
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        drpIs_Service.SelectedValue = "N";
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";
                        txtUnit_Price.Text = dt.Rows[0]["rate"].ToString();



                        txtAssessable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Value.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Value.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Value.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCess_Value.Text = "0.00";
                        txtStCesVal.Text = "0.00";
                        txtDiscount.Text = "0.00";
                        txtOther_Charges.Text = "0.00";
                        txtGST_Rate.Text = dt.Rows[0]["rate"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0.00";
                        txtAssessable_Amount.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtTotal_Bill_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        Assessable_Value = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        Assessable_Amount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        StCesVal = Convert.ToDouble(txtStCesVal.Text);
                        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
                        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

                        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
                        txtTotal_Item_Amt.Text = TotInv_Value.ToString();
                        txtTotInv_Value.Text = TotInv_Value.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();



                        txtMode_of_Payment.Text = Mode_of_Payment;
                        txtAccount_Details.Text = Account_Details;
                        txtBranch.Text = Branch;
                        txtPayeeName.Text = "0";
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {

                        ViewState["currentTable"] = null;
                    }
                }
                else
                {

                    ViewState["currentTable"] = null;
                }
            }
            else
            {

                ViewState["currentTable"] = null;
            }
        }
        catch
        {
        }
    }


    #region[update pincode]
    protected void btnUpdatePincode_Click(object sender, EventArgs e)
    {
        //DataSet ds = new DataSet();
        //qry = "update NT_1_AccountMaster set Pincode=" + txtBuyer_StateCode.Text + " where Ac_Code='" + hdnfmillCode.Value + "' and  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        //ds = clsDAL.SimpleQuery(qry);
        //qry1 = "update NT_1_AccountMaster set Pincode=" + txtShip_To_PinCode.Text + " where Ac_Code='" + hdnfUnitCode.Value + "' and  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        //ds = clsDAL.SimpleQuery(qry1);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('successfully updated...!!');", true);
        string Ewaybullno = "success :true,message:IRN generated successfully,result:{AckNo:122010103159647,AckDt:2020-11-03 17:44:00,Irn:dece27d2444390a35c7aed0125a0df6923e9ec23008a8696ac32e2f55871e4b4,SignedInvoice";
        string str = Ewaybullno;
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
        string AckNo = string.Empty;
        string Irn = string.Empty;
        string ewaybildate;
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
                    string AckNoNew = getBetween(str, "AckNo", "AckDt");
                    AckNo = AckNoNew;
                    string IrnNew = getBetween(str, "Irn", "SignedInvoice");
                    Irn = IrnNew;
                    string sbno = txtDoc_No.Text;
                    string sbnoNew = txtDoc_No.Text;
                    sbno = sbno.Remove(sbno.Length - 2);
                    DataSet ds = new DataSet();
                    qry = "";
                    qry1 = "";
                    qry = "update NT_1_deliveryorder set ackno='" + AckNo + "',einvoiceno='" + Irn + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_no=" + Dono;
                    ds = clsDAL.SimpleQuery(qry);
                    string dosbno = clsCommon.getString("select SB_No from NT_1_deliveryorder where company_code="
                        + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_no=" + Dono);

                    qry1 = "update NT_1_SugarSale set ackno=" + AckNo + ",einvoiceno='" + Irn + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                       + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and newsbno=" + dosbno;
                    ds = clsDAL.SimpleQuery(qry1);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice Generated Successfully !');", true);
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

                //btnAdddetails.Enabled = false;
                //btnClosedetails.Enabled = false;
                ViewState["currentTable"] = null;

                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                drpIGSTOnIntra.Enabled = false;
                drpIs_Service.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                drpDoc_Type.Enabled = false;
                drpSupply_Type.Enabled = false;
                drpRegRev_Type.Enabled = false;
                txtSeller_Name.Enabled = false;
                txtBuyerName.Enabled = false;
                txtSeller_Address.Enabled = false;
                txtSeller_Address2.Enabled = false;
                txtBuyer_Address.Enabled = false;
                txtBuyer_Address2.Enabled = false;
                txtSellerGST_No.Enabled = false;
                txtBuyerGST_No.Enabled = false;
                txtBuyer_Location.Enabled = false;
                txtSeller_Location.Enabled = false;
                txtSeller_PIN.Enabled = false;
                txtBuyer_PIN.Enabled = false;
                txtSeller_StateName.Enabled = false;
                txtBuyer_StateName.Enabled = false;
                txtSeller_StateCode.Enabled = false;
                txtByuer_Phno.Enabled = false;
                txtBuyer_StateCode.Enabled = false;
                txtDispatch_Name.Enabled = false;
                txtShip_Name.Enabled = false;
                txtDispatch_Address.Enabled = false;
                txtDispatch_Address2.Enabled = false;
                txtShipTo_Address.Enabled = false;
                txtShipTo_Address2.Enabled = false;
                txtDispatch_GSTNo.Enabled = false;
                txtShip_To_GSTNo.Enabled = false;
                txtShip_To_Location.Enabled = false;
                txtDispatch_Location.Enabled = false;
                txtDispatch_StateCode.Enabled = false;
                txtShip_TostateCode.Enabled = false;
                txtShip_To_PinCode.Enabled = false;
                txtDispatch_PincodeCode.Enabled = false;
                txtSeller_Email.Enabled = false;
                txtBuyer_Email.Enabled = false;
                txtBuyer_Pos.Enabled = false;
                txtItem_Name.Enabled = false;
                txtItem_Description.Enabled = false;
                txtHSN.Enabled = false;
                txtQty.Enabled = false;
                txtUnit.Enabled = false;
                txtUnit_Price.Enabled = false;
                txtTotInv_Value.Enabled = false;
                txtAssessable_Value.Enabled = false;
                txtCGST_Value.Enabled = false;
                txtSGST_Value.Enabled = false;
                txtIGST_Value.Enabled = false;
                txtCess_Value.Enabled = false;
                txtStCesVal.Enabled = false;
                txtDiscount.Enabled = false;
                txtGST_Rate.Enabled = false;
                txtOther_Charges.Enabled = false;
                txtTaxable_Amt.Enabled = false;
                txtCGST_Amt.Enabled = false;
                txtSGST_Amt.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtCESS_Advol_Amt.Enabled = false;
                txtAssessable_Amount.Enabled = false;
                txtBranch.Enabled = false;
                txtPayeeName.Enabled = false;
                txtTotal_Item_Amt.Enabled = false;
                txtTotal_Bill_Amt.Enabled = false;
                txtMode_of_Payment.Enabled = false;
                txtAccount_Details.Enabled = false;
                txtApproximate_Distance.Enabled = false;
                txtTransporter_Name.Enabled = false;
                txtTransporter_ID.Enabled = false;
                drpVehicle_Type.Enabled = false;
                drpTrance_Mode.Enabled = false;
                txtVehicle_No.Enabled = false;

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

                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                //btnAdddetails.Enabled = true;
                //btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                drpIGSTOnIntra.Enabled = true;
                drpIs_Service.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                drpDoc_Type.Enabled = true;
                drpSupply_Type.Enabled = true;
                drpRegRev_Type.Enabled = true;
                txtSeller_Name.Enabled = true;
                txtBuyerName.Enabled = true;
                txtSeller_Address.Enabled = true;
                txtSeller_Address2.Enabled = true;
                txtBuyer_Address.Enabled = true;
                txtBuyer_Address2.Enabled = true;
                txtSellerGST_No.Enabled = true;
                txtBuyerGST_No.Enabled = true;
                txtBuyer_Location.Enabled = true;
                txtSeller_PIN.Enabled = true;
                txtBuyer_PIN.Enabled = true;
                txtSeller_StateName.Enabled = true;
                txtBuyer_StateName.Enabled = true;
                txtSeller_Location.Enabled = true;
                txtSeller_StateCode.Enabled = true;
                txtByuer_Phno.Enabled = true;
                txtBuyer_StateCode.Enabled = true;
                txtSeller_Email.Enabled = true;
                txtBuyer_Email.Enabled = true;
                txtBuyer_Pos.Enabled = true;
                txtDispatch_Name.Enabled = true;
                txtShip_Name.Enabled = true;
                txtDispatch_Address.Enabled = false;
                txtDispatch_Address2.Enabled = false;
                txtShipTo_Address.Enabled = false;
                txtShipTo_Address2.Enabled = false;
                txtDispatch_GSTNo.Enabled = true;
                txtShip_To_GSTNo.Enabled = true;
                txtShip_To_Location.Enabled = true;
                txtDispatch_Location.Enabled = true;
                txtDispatch_StateCode.Enabled = true;
                txtShip_TostateCode.Enabled = true;
                txtShip_To_PinCode.Enabled = true;
                txtDispatch_PincodeCode.Enabled = true;
                txtItem_Name.Enabled = true;
                txtItem_Description.Enabled = true;
                txtHSN.Enabled = true;
                txtQty.Enabled = true;
                txtUnit.Enabled = true;
                txtUnit_Price.Enabled = true;
                txtTotInv_Value.Enabled = true;
                txtAssessable_Value.Enabled = true;
                txtCGST_Value.Enabled = true;
                txtSGST_Value.Enabled = true;
                txtIGST_Value.Enabled = true;
                txtCess_Value.Enabled = true;
                txtStCesVal.Enabled = true;
                txtDiscount.Enabled = true;
                txtGST_Rate.Enabled = true;
                txtOther_Charges.Enabled = true;
                txtTaxable_Amt.Enabled = true;
                txtCGST_Amt.Enabled = true;
                txtSGST_Amt.Enabled = true;
                txtIGST_Amt.Enabled = true;
                txtCESS_Advol_Amt.Enabled = true;
                txtAssessable_Amount.Enabled = true;
                txtBranch.Enabled = true;
                txtPayeeName.Enabled = true;
                txtTotal_Item_Amt.Enabled = true;
                txtTotal_Bill_Amt.Enabled = true;
                txtMode_of_Payment.Enabled = true;
                txtAccount_Details.Enabled = true;
                txtApproximate_Distance.Enabled = true;
                txtTransporter_Name.Enabled = true;
                txtTransporter_ID.Enabled = true;
                drpVehicle_Type.Enabled = true;
                drpTrance_Mode.Enabled = true;
                txtVehicle_No.Enabled = true;

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
                drpIGSTOnIntra.Enabled = false;
                drpIs_Service.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                drpDoc_Type.Enabled = false;
                drpSupply_Type.Enabled = false;
                drpRegRev_Type.Enabled = false;
                txtSeller_Name.Enabled = false;
                txtBuyerName.Enabled = false;
                txtSeller_Address.Enabled = false;
                txtSeller_Address2.Enabled = false;
                txtBuyer_Address.Enabled = false;
                txtBuyer_Address2.Enabled = false;
                txtSellerGST_No.Enabled = false;
                txtBuyerGST_No.Enabled = false;
                txtBuyer_Location.Enabled = false;
                txtSeller_PIN.Enabled = false;
                txtBuyer_PIN.Enabled = false;
                txtSeller_StateName.Enabled = false;
                txtBuyer_StateName.Enabled = false;
                txtSeller_Location.Enabled = false;
                txtSeller_StateCode.Enabled = false;
                txtByuer_Phno.Enabled = false;
                txtBuyer_StateCode.Enabled = false;
                txtSeller_Email.Enabled = false;
                txtBuyer_Email.Enabled = false;
                txtBuyer_Pos.Enabled = false;
                txtDispatch_Name.Enabled = false;
                txtShip_Name.Enabled = false;
                txtShip_Name.Enabled = false;
                txtDispatch_GSTNo.Enabled = false;
                txtShip_To_GSTNo.Enabled = false;
                txtShip_To_Location.Enabled = false;
                txtDispatch_Location.Enabled = false;
                txtDispatch_StateCode.Enabled = false;
                txtShip_TostateCode.Enabled = false;
                txtShip_To_PinCode.Enabled = false;
                txtDispatch_PincodeCode.Enabled = false;
                txtItem_Name.Enabled = false;
                txtItem_Description.Enabled = false;
                txtHSN.Enabled = false;
                txtQty.Enabled = false;
                txtUnit.Enabled = false;
                txtUnit_Price.Enabled = false;
                txtTotInv_Value.Enabled = false;
                txtStCesVal.Enabled = false;
                txtDiscount.Enabled = false;
                txtGST_Rate.Enabled = false;
                txtOther_Charges.Enabled = false;
                txtAssessable_Value.Enabled = false;
                txtBranch.Enabled = false;
                txtPayeeName.Enabled = false;
                txtCGST_Value.Enabled = false;
                txtSGST_Value.Enabled = false;
                txtIGST_Value.Enabled = false;
                txtCess_Value.Enabled = false;
                txtTaxable_Amt.Enabled = false;
                txtCGST_Amt.Enabled = false;
                txtSGST_Amt.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtCESS_Advol_Amt.Enabled = false;
                txtAssessable_Amount.Enabled = false;
                txtTotal_Item_Amt.Enabled = false;
                txtTotal_Bill_Amt.Enabled = false;
                txtMode_of_Payment.Enabled = false;
                txtAccount_Details.Enabled = false;
                txtApproximate_Distance.Enabled = false;
                txtTransporter_Name.Enabled = false;
                txtTransporter_ID.Enabled = false;
                drpVehicle_Type.Enabled = false;
                drpTrance_Mode.Enabled = false;
                txtVehicle_No.Enabled = false;
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
                drpIGSTOnIntra.Enabled = true;
                drpIs_Service.Enabled = true;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                drpDoc_Type.Enabled = true;
                drpSupply_Type.Enabled = true;
                drpRegRev_Type.Enabled = true;
                txtSeller_Name.Enabled = true;
                txtBuyerName.Enabled = true;
                txtSeller_Address.Enabled = true;
                txtSeller_Address2.Enabled = true;
                txtBuyer_Address.Enabled = true;
                txtBuyer_Address2.Enabled = true;
                txtSellerGST_No.Enabled = true;
                txtBuyerGST_No.Enabled = true;
                txtBuyer_Location.Enabled = true;
                txtSeller_PIN.Enabled = true;
                txtBuyer_PIN.Enabled = true;
                txtSeller_StateName.Enabled = true;
                txtBuyer_StateName.Enabled = true;
                txtSeller_Location.Enabled = true;
                txtSeller_StateCode.Enabled = true;
                txtByuer_Phno.Enabled = true;
                txtBuyer_StateCode.Enabled = true;
                txtSeller_Email.Enabled = true;
                txtBuyer_Email.Enabled = true;
                txtBuyer_Pos.Enabled = true;
                txtDispatch_Name.Enabled = true;
                txtShip_Name.Enabled = true;
                txtBranch.Enabled = true;
                txtPayeeName.Enabled = true;
                txtDispatch_GSTNo.Enabled = true;
                txtShip_To_GSTNo.Enabled = true;
                txtShip_To_Location.Enabled = true;
                txtDispatch_Location.Enabled = true;
                txtDispatch_StateCode.Enabled = true;
                txtShip_TostateCode.Enabled = true;
                txtShip_To_PinCode.Enabled = true;
                txtDispatch_PincodeCode.Enabled = true;
                txtItem_Name.Enabled = true;
                txtItem_Description.Enabled = true;
                txtHSN.Enabled = true;
                txtQty.Enabled = true;
                txtUnit.Enabled = true;
                txtUnit_Price.Enabled = true;
                txtTotInv_Value.Enabled = true;
                txtAssessable_Value.Enabled = true;
                txtCGST_Value.Enabled = true;
                txtSGST_Value.Enabled = true;
                txtIGST_Value.Enabled = true;
                txtCess_Value.Enabled = true;
                txtStCesVal.Enabled = true;
                txtDiscount.Enabled = true;
                txtGST_Rate.Enabled = true;
                txtOther_Charges.Enabled = true;
                txtTaxable_Amt.Enabled = true;
                txtCGST_Amt.Enabled = true;
                txtSGST_Amt.Enabled = true;
                txtIGST_Amt.Enabled = true;
                txtCESS_Advol_Amt.Enabled = true;
                txtAssessable_Amount.Enabled = true;
                txtTotal_Item_Amt.Enabled = true;
                txtTotal_Bill_Amt.Enabled = true;
                txtMode_of_Payment.Enabled = true;
                txtAccount_Details.Enabled = true;
                txtApproximate_Distance.Enabled = true;
                txtTransporter_Name.Enabled = true;
                txtTransporter_ID.Enabled = true;
                drpVehicle_Type.Enabled = true;
                drpTrance_Mode.Enabled = true;
                txtVehicle_No.Enabled = true;

            }
            #region Always check this
            #endregion
        }
        catch
        {
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



    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (hdnf.Value != string.Empty)
        {
            //string query = getDisplayQuery();
            //bool recordExist = this.fetchRecord(query);
        }
        else
        {

        }
        string qry = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        if (qry != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");

        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");

            btnEdit.Enabled = false;
            // btnDelete.Enabled = false;
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
        searchString = txtEditDoc_No.Text;
        strTextBox = "txtEditDoc_No";

    }
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";

    }
    #endregion
    #region [drpIGSTOnIntra_TextChanged]
    protected void drpIGSTOnIntra_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region drpIs_Service_TextChanged]
    protected void drpIs_Service_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";

    }
    #endregion
    #region [drpDoc_Type_TextChanged]
    protected void drpDoc_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpSupply_Type_TextChanged]
    protected void drpSupply_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpRegRev_Type_TextChanged]
    protected void drpRegRev_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtSeller_Name_TextChanged]
    protected void txtSeller_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSeller_Name.Text;
        strTextBox = "txtSeller_Name";

    }
    #endregion
    #region [txtSeller_Address_TextChanged]
    protected void txtSeller_Address_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSeller_Address.Text;
        strTextBox = "txtSeller_Address";

    }
    #endregion
    #region [txtSellerGST_No_TextChanged]
    protected void txtSellerGST_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSellerGST_No.Text;
        strTextBox = "txtSellerGST_No";

    }
    #endregion
    #region [txtBuyer_Location_TextChanged]
    protected void txtBuyer_Location_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBuyer_Location.Text;
        strTextBox = "txtBuyer_Location";

    }
    #endregion
    #region [txtSeller_StateCode_TextChanged]
    protected void txtSeller_StateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSeller_StateCode.Text;
        strTextBox = "txtSeller_StateCode";

    }
    #endregion
    #region [txtBuyer_StateCode_TextChanged]
    protected void txtBuyer_StateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBuyer_StateCode.Text;
        strTextBox = "txtBuyer_StateCode";

    }
    #endregion
    #region [txtDispatch_Name_TextChanged]
    protected void txtDispatch_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDispatch_Name.Text;
        strTextBox = "txtDispatch_Name";

    }
    #endregion
    #region [txtShip_Name_TextChanged]
    protected void txtShip_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShip_Name.Text;
        strTextBox = "txtShip_Name";

    }
    #endregion
    #region [txtDispatch_GSTNo_TextChanged]
    protected void txtDispatch_GSTNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDispatch_GSTNo.Text;
        strTextBox = "txtDispatch_GSTNo";

    }
    #endregion
    #region [txtShip_To_Location_TextChanged]
    protected void txtShip_To_Location_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShip_To_Location.Text;
        strTextBox = "txtShip_To_Location";

    }
    #endregion
    #region [txtDispatch_StateCode_TextChanged]
    protected void txtDispatch_StateCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDispatch_StateCode.Text;
        strTextBox = "txtDispatch_StateCode";

    }
    #endregion
    #region [txtShip_To_PinCode_TextChanged]
    protected void txtShip_To_PinCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShip_To_PinCode.Text;
        strTextBox = "txtShip_To_PinCode";

    }
    #endregion
    #region [txtItem_Name_TextChanged]
    protected void txtItem_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Name.Text;
        strTextBox = "txtItem_Name";

    }
    #endregion
    #region [txtItem_Description_TextChanged]
    protected void txtItem_Description_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Description.Text;
        strTextBox = "txtItem_Description";

    }
    #endregion
    #region [txtHSN_TextChanged]
    protected void txtHSN_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHSN.Text;
        strTextBox = "txtHSN";

    }
    #endregion
    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        Calculations();

    }
    #endregion
    #region [txtGST_Rate_TextChanged]
    protected void txtGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_Rate.Text;
        strTextBox = "txtGST_Rate";
        Calculations();

    }
    #endregion
    #region [txtUnit_TextChanged]
    protected void txtUnit_TextChanged(object sender, EventArgs e)
    {
        searchString = txtUnit.Text;
        strTextBox = "txtUnit";


    }
    #endregion
    #region [txtUnit_Price_TextChanged]
    protected void txtUnit_Price_TextChanged(object sender, EventArgs e)
    {
        searchString = txtUnit_Price.Text;
        strTextBox = "txtUnit_Price";
        Calculations(); ;


    }
    #endregion
    #region [txtTotInv_Value_TextChanged]
    protected void txtTotInv_Value_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotInv_Value.Text;
        strTextBox = "txtTotInv_Value";

    }
    #endregion



    #region [txtTaxable_Amt_TextChanged]
    protected void txtTaxable_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTaxable_Amt.Text;
        strTextBox = "txtTaxable_Amt";

    }
    #endregion
    #region [txtCGST_Amt_TextChanged]
    protected void txtCGST_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST_Amt.Text;
        strTextBox = "txtCGST_Amt";

    }
    #endregion
    #region [txtSGST_Amt_TextChanged]
    protected void txtSGST_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST_Amt.Text;
        strTextBox = "txtSGST_Amt";

    }
    #endregion
    #region [txtIGST_Amt_TextChanged]
    protected void txtIGST_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST_Amt.Text;
        strTextBox = "txtIGST_Amt";

    }
    #endregion
    #region [txtCESS_Advol_Amt_TextChanged]
    protected void txtCESS_Advol_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCESS_Advol_Amt.Text;
        strTextBox = "txtCESS_Advol_Amt";

    }
    #endregion

    #region txtTotal_Item_Amt_TextChanged]
    protected void txtTotal_Item_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotal_Item_Amt.Text;
        strTextBox = "txtTotal_Item_Amt";

    }
    #endregion
    #region [txtTotal_Bill_Amt_TextChanged]
    protected void txtTotal_Bill_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotal_Bill_Amt.Text;
        strTextBox = "txtTotal_Bill_Amt";

    }
    #endregion
    #region [txtMode_of_Payment_TextChanged]
    protected void txtMode_of_Payment_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMode_of_Payment.Text;
        strTextBox = "txtMode_of_Payment";
    }
    #endregion
    #region [txtAccount_Details_TextChanged]
    protected void txtAccount_Details_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAccount_Details.Text;
        strTextBox = "txtAccount_Details";

    }
    #endregion
    #region [txtApproximate_Distance_TextChanged]
    protected void txtApproximate_Distance_TextChanged(object sender, EventArgs e)
    {
        searchString = txtApproximate_Distance.Text;
        strTextBox = "txtApproximate_Distance";

    }
    #endregion




    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    #endregion
    public static string GetPageHtml(string link, System.Net.WebProxy proxy = null)
    {
        System.Net.WebClient client = new System.Net.WebClient() { Encoding = Encoding.UTF8 };
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        if (proxy != null)
        {
            client.Proxy = proxy;
        }

        using (client)
        {
            try
            {
                return client.DownloadString(link);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public static string EncryptAsymmetric(string data, string key)
    {
        byte[] keyBytes = Convert.FromBase64String(key);
        AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
        RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
        RSAParameters rsaParameters = new RSAParameters();
        rsaParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned();
        rsaParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned();
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(rsaParameters);
        byte[] plaintext = Encoding.UTF8.GetBytes(data);
        byte[] ciphertext = rsa.Encrypt(plaintext, false);
        string cipherresult = Convert.ToBase64String(ciphertext);
        return cipherresult;
    }

    //public string GetAuthToken(string urlAddress, string username, string password)
    //{
    //    string jsonData = string.Empty;
    //    string authtoken = string.Empty;
    //    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress);

    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    request.Headers.Add("gspappid", username);//Client Id
    //    request.Headers.Add("gspappsecret", password);//Client Secret
    //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
    //    {
    //        string json = "{\"gspappid\":\"" + username + "\"," +
    //          "\"gspappsecret\":\"" + password + "\"}";
    //        streamWriter.Write(json);
    //        streamWriter.Flush();
    //        streamWriter.Close();
    //    }

    //    try
    //    {
    //        var httpResponse = (HttpWebResponse)request.GetResponse();
    //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //        {
    //            jsonData = streamReader.ReadToEnd();
    //        }
    //        var jsonObject = JObject.Parse(jsonData);
    //        jsonData = (string)jsonObject.SelectToken("access_token");
    //        authtoken = "Bearer " + jsonData;
    //    }
    //    catch (Exception ex)
    //    {
    //        return jsonData = "Issue occured, " + ex.Message;
    //    }

    //    return authtoken;
    //}

    //public string GenrateEWaybill(string urlAddress1, string username, string password, string gstin, string requestid, string token, string json)
    //{
    //    string jsonData = string.Empty;
    //    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress1);
    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    request.Headers.Add("user_name", username);
    //    request.Headers.Add("password", password);
    //    request.Headers.Add("gstin", gstin);
    //    request.Headers.Add("requestid", requestid);
    //    request.Headers.Add("Authorization", token);


    //    try
    //    {
    //        //Place the serialized content of the object to be posted into the request stream
    //        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
    //        {
    //            //  string json = "{\"gspappid\":\"" + username + "\"," +
    //            //"\"gspappsecret\":\"" + password + "\"}";
    //            //string acb = "{\"supplyType\":\"O\",\"subSupplyType\":\"1\",\"docType\":\"INV\",\"docNo\":\"123021544-8\",\"docDate\":\"15/12/2017\",\"fromGstin\":\"05AAACG2115R1ZN\",\"fromTrdName\":\"welton\",\"fromAddr1\":\"2ND CROSS NO 59  19  A\",\r\n\"fromAddr2\":\"GROUND FLOOR OSBORNE ROAD\",\r\n\"fromPlace\":\"FRAZER TOWN\",\r\n\"fromPincode\":560042,\r\n\"actFromStateCode\":29,\"fromStateCode\":29,\r\n\"toGstin\":\"05AAACG2140A1ZL\",\r\n\"toTrdName\":\"sthuthya\",\r\n\"toAddr1\":\"Shree Nilaya\",\r\n\"toAddr2\":\"Dasarahosahalli\",\r\n\"toPlace\":\"Beml Nagar\",\r\n\"toPincode\":689788,\r\n\"actToStateCode\":29,\r\n\"toStateCode\":28,\r\n\"totalValue\":5609889,\r\n\"cgstValue\":0,\r\n\"sgstValue\":0,\r\n\"igstValue\":168296.67,\r\n\"cessValue\":224395.56,\r\n\"totInvValue\":435678,\r\n\"transporterId\":\"\",\r\n\"transporterName\":\"\",\r\n\"transDocNo\":\"\",\r\n\"transMode\":\"1\",\r\n\"transDistance\":\"25\",\r\n\"transDocDate\":\"\",\r\n\"vehicleNo\":\"PVC1234\",\r\n\"vehicleType\":\"R\",\r\n\"itemList\":\r\n[{\r\n\"productName\":\"Wheat\",\r\n\"productDesc\":\"Wheat\",\r\n\"hsnCode\":1001,\r\n\"quantity\":4,\r\n\"qtyUnit\":\"BOX\",\r\n\"cgstRate\":0,\r\n\"sgstRate\":0,\r\n\"igstRate\":3,\r\n\"cessRate\":4,\r\n\"cessAdvol\":0,\r\n\"taxableAmount\":5609889\r\n}\r\n]}";
    //            // string acb = "{\r\n\"supplyType\":\"O\",\r\n\"subSupplyType\":\"1\",\r\n\r\n\"docType\":\"INV\",\r\n\"docNo\":\"8985123-8\",\r\n\"docDate\":\"15/12/2017\",\r\n\"fromGstin\":\"05AAACG2115R1ZN\",\r\n\"fromTrdName\":\"welton\",\r\n\"fromAddr1\":\"2ND CROSS NO 59  19  A\",\r\n\"fromAddr2\":\"GROUND FLOOR OSBORNE ROAD\",\r\n\"fromPlace\":\"FRAZER TOWN\",\r\n\"fromPincode\":560042,\r\n\"actFromStateCode\":29,\"fromStateCode\":29,\r\n\"toGstin\":\"05AAACG2140A1ZL\",\r\n\"toTrdName\":\"sthuthya\",\r\n\"toAddr1\":\"Shree Nilaya\",\r\n\"toAddr2\":\"Dasarahosahalli\",\r\n\"toPlace\":\"Beml Nagar\",\r\n\"toPincode\":689788,\r\n\"actToStateCode\":29,\r\n\"toStateCode\":28,\r\n\"totalValue\":5609889,\r\n\"cgstValue\":0,\r\n\"sgstValue\":0,\r\n\"igstValue\":168296.67,\r\n\"cessValue\":224395.56,\r\n\"totInvValue\":435678,\r\n\"transporterId\":\"\",\r\n\"transporterName\":\"\",\r\n\"transDocNo\":\"\",\r\n\"transMode\":\"1\",\r\n\"transDistance\":\"25\",\r\n\"transDocDate\":\"\",\r\n\"vehicleNo\":\"PVC1234\",\r\n\"vehicleType\":\"R\",\r\n\"itemList\":\r\n[{\r\n\"productName\":\"Wheat\",\r\n\"productDesc\":\"Wheat\",\r\n\"hsnCode\":1001,\r\n\"quantity\":4,\r\n\"qtyUnit\":\"BOX\",\r\n\"cgstRate\":0,\r\n\"sgstRate\":0,\r\n\"igstRate\":3,\r\n\"cessRate\":4,\r\n\"cessAdvol\":0,\r\n\"taxableAmount\":5609889\r\n}\r\n]}";
    //            // string jsonNew = "{\"supplyType\":\"O\",\"subSupplyType\":\"1\",\"docType\":\"INV\",\"docNo\":\"1120shu1bha1110897854540\",\"docDate\":\"15/12/2017\",\"fromGstin\":\"05AAACG2115R1ZN\",\"fromTrdName\":\"welton\",\"fromAddr1\":\"2ND CROSS NO 59  19  A\",\r\n\"fromAddr2\":\"GROUND FLOOR OSBORNE ROAD\",\r\n\"fromPlace\":\"FRAZER TOWN\",\r\n\"fromPincode\":560042,\r\n\"actFromStateCode\":29,\"fromStateCode\":29,\r\n\"toGstin\":\"05AAACG2140A1ZL\",\r\n\"toTrdName\":\"sthuthya\",\r\n\"toAddr1\":\"Shree Nilaya\",\r\n\"toAddr2\":\"Dasarahosahalli\",\r\n\"toPlace\":\"Beml Nagar\",\r\n\"toPincode\":500003,\r\n\"actToStateCode\":36,\r\n\"toStateCode\":36,\r\n\"totalValue\":5609889,\r\n\"cgstValue\":0,\r\n\"sgstValue\":0,\r\n\"igstValue\":168296.67,\r\n\"cessValue\":224395.56,\r\n\"totInvValue\":6002581.23,\r\n\"transporterId\":\"\",\r\n\"transporterName\":\"\",\r\n\"transDocNo\":\"\",\r\n\"transMode\":\"1\",\r\n\"transDistance\":\"25\",\r\n\"transDocDate\":\"\",\r\n\"vehicleNo\":\"PVC1234\",\r\n\"vehicleType\":\"R\",\r\n\"TransactionType\":\"1\",\r\n\"itemList\":\r\n[{\r\n\"productName\":\"Wheat\"\r\n,\"productDesc\":\"Wheat\",\r\n\"hsnCode\":1001,\r\n\"quantity\":4,\r\n\"qtyUnit\":\"BOX\",\r\n\"cgstRate\":0,\r\n\"sgstRate\":0,\r\n\"igstRate\":3,\r\n\"cessRate\":1,\r\n\"cessAdvol\":0,\r\n\"taxableAmount\":5609889\r\n},{\r\n\"productName\":\"Wheat\",\r\n\"productDesc\":\"Wheat\",\r\n\"hsnCode\":1001,\"quantity\":4,\r\n\"qtyUnit\":\"BOX\",\r\n\"cgstRate\":0,\r\n\"sgstRate\":0,\r\n\"igstRate\":3,\r\n\"cessRate\":1,\r\n\"cessAdvol\":0,\r\n\"taxableAmount\":5609889\r\n}]\r\n}\r\n}";

    //            //// string asb = "{\"supplyType\":\"O\",\"subSupplyType\":\"1\",\"docType\":\"INV\",\"docNo\":\"3g545jn10\",\"docDate\":\"15/12/2017\",\"fromGstin\":\"05AAACG2115R1ZN\",\"fromTrdName\":\"welton\",\"fromAddr1\":\"2ND CROSS NO 59  19  A\",\"fromAddr2\":\"GROUND FLOOR OSBORNE ROAD\",\"fromPlace\":\"FRAZER TOWN\",\"fromPincode\":560042,\"actFromStateCode\":29,\"fromStateCode\":29,\"toGstin\":\"05AAACG2140A1ZL\",\"toTrdName\":\"sthuthya\",\"toAddr1\":\"Shree Nilaya\",\"toAddr2\":\"Dasarahosahalli\",\"toPlace\":\"Beml Nagar\",\"toPincode\":500003,\"actToStateCode\":36,\"toStateCode\":36,\"totalValue\":5609889,\"cgstValue\":0,\"sgstValue\":0,\"igstValue\":168296.67,\"cessValue\":224395.56,\"totInvValue\":6002581.23,\"transporterId\":\"\",\"transporterName\":\"\",\"transDocNo\":\"\",\"transMode\":\"1\",\"transDistance\":\"25\",\"transDocDate\":\"\",\"vehicleNo\":\"PVC1234\",\"vehicleType\":\"R\",\"TransactionType\":\"1\",\"itemList\":[{\"productName\":\"Wheat\",\"productDesc\":\"Wheat\",\"hsnCode\":1001,\"quantity\":4,\"qtyUnit\":\"BOX\",\"cgstRate\":0,\"sgstRate\":0,\"igstRate\":3,\"cessRate\":1,\"cessAdvol\":0,\"taxableAmount\":5609889},{\"productName\":\"Wheat\",\"productDesc\":\"Wheat\",\"hsnCode\":1001,\"quantity\":4,\"qtyUnit\":\"BOX\",\"cgstRate\":0,\"sgstRate\":0,\"igstRate\":3,\"cessRate\":1,\"cessAdvol\":0,\"taxableAmount\":5609889}]}";

    //            // string asb = "{"supplyType":"O","subSupplyType":"1","docType":"INV","docNo":"3ghj10","docDate":"15/12/2017","fromGstin":"05AAACG2115R1ZN","fromTrdName":"welton","fromAddr1":"2ND CROSS NO 59  19  A","fromAddr2":"GROUND FLOOR OSBORNE ROAD","fromPlace":"FRAZER TOWN","fromPincode":560042,"actFromStateCode":29,"fromStateCode":29,"toGstin":"05AAACG2140A1ZL","toTrdName":"sthuthya","toAddr1":"Shree Nilaya","toAddr2":"Dasarahosahalli","toPlace":"Beml Nagar","toPincode":500003,"actToStateCode":36,"toStateCode":36,"totalValue":5609889,"cgstValue":0,"sgstValue":0,"igstValue":168296.67,"cessValue":224395.56,"totInvValue":6002581.23,"transporterId":"","transporterName":"","transDocNo":"","transMode":"1","transDistance":"25","transDocDate":"","vehicleNo":"PVC1234","vehicleType":"R","TransactionType":"1","itemList":[{"productName":"Wheat","productDesc":"Wheat","hsnCode":1001,"quantity":4,"qtyUnit":"BOX","cgstRate":0,"sgstRate":0,"igstRate":3,"cessRate":1,"cessAdvol":0,"taxableAmount":5609889},{"productName":"Wheat","productDesc":"Wheat","hsnCode":1001,"quantity":4,"qtyUnit":"BOX","cgstRate":0,"sgstRate":0,"igstRate":3,"cessRate":1,"cessAdvol":0,"taxableAmount":5609889}]}";

    //            //  var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(asb);


    //            string asb = json;
    //            streamWriter.Write(asb);
    //            streamWriter.Flush();
    //            streamWriter.Close();
    //        }



    //        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

    //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //        {
    //            Stream receiveStream = response.GetResponseStream();
    //            StreamReader readStream = null;
    //            readStream = new StreamReader(receiveStream);
    //            jsonData = readStream.ReadToEnd();
    //            response.Close();
    //            readStream.Close();
    //            var jsonObject = JObject.Parse(jsonData);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return jsonData = "Issue occured, " + ex.Message;
    //    }
    //    return jsonData;
    //}
    #region [btnGenEwayBill_Click]
    protected void btnGenEwayBill_Click(object sender, EventArgs e)
    {
        string acname = "";
        string vno;
        string from_address;
        string from_address1;
        string from_partnm;
        string Bill_From_Add;
        string from_city;
        string to_ac_name_e;
        string to_address1;
        string to_address2;
        string to_place;

        bool isValidated = true;
        if (txtSeller_PIN.Text != string.Empty && txtSeller_PIN.Text != "999999")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtSeller_PIN);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Invalied Mill Pincode!!!!!');", true);
            return;
        }
        if (txtBuyer_PIN.Text != string.Empty && txtBuyer_PIN.Text != "999999")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtBuyer_PIN);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Invalied Mill Pincode!!!!!');", true);
            return;
        }
        if (txtDispatch_PincodeCode.Text != string.Empty && txtDispatch_PincodeCode.Text != "999999")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDispatch_PincodeCode);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Invalied Bill To Pincode!!!!!');", true);
            return;
        }
        if (txtShip_To_PinCode.Text != string.Empty && txtShip_To_PinCode.Text != "999999")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtShip_To_PinCode);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Invalied Bill To Pincode!!!!!');", true);
            return;
        }
        string hsn = txtHSN.Text;
        if (hsn.Length <= 8)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtHSN);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please Enter Proper HSN Code!!!!!');", true);
            return;
        }


        #region Assing Value
        string SupTyp = drpSupply_Type.SelectedValue;
        string TaxSch = hdnfTaxSch.Value;
        string RegRev = drpRegRev_Type.SelectedValue;
        string Typ = hdnfType.Value;
        string IgstOnIntra = drpIGSTOnIntra.SelectedValue;
        string Type = drpDoc_Type.SelectedValue;
        string Doc_No = txtDoc_No.Text;
        string Doc_Date = txtDoc_Date.Text;
        #region Seller
        string SellerLglNm = txtSeller_Name.Text;
        SellerLglNm = SellerLglNm.Replace("-", "");
        SellerLglNm = SellerLglNm.Replace("/", "");
        SellerLglNm = SellerLglNm.Replace("&", "AND");
        SellerLglNm = SellerLglNm.Replace(".", "");
        SellerLglNm = SellerLglNm.Replace("#", "");
        SellerLglNm = SellerLglNm.Replace("(", "");
        SellerLglNm = SellerLglNm.Replace(")", "");
        SellerLglNm = SellerLglNm.Replace(":", "");
        SellerLglNm = SellerLglNm.Replace("_", "");
        SellerLglNm = SellerLglNm.Replace("@", "");
        SellerLglNm = SellerLglNm.Replace(";", "");
        SellerLglNm = SellerLglNm.Replace("=", "");
        SellerLglNm = SellerLglNm.Replace("\n", "");
        SellerLglNm = SellerLglNm.Replace("*", "");
        SellerLglNm = SellerLglNm.Replace(",", "");
        SellerLglNm = SellerLglNm.Replace("'", "");
        SellerLglNm = SellerLglNm.Replace("\"", "");
        SellerLglNm = SellerLglNm.Replace(@"\", "");

        string SellerLglName = SellerLglNm;

        string SellerAdd = txtSeller_Address.Text;
        SellerAdd = SellerAdd.Replace("-", "");
        SellerAdd = SellerAdd.Replace("/", "");
        SellerAdd = SellerAdd.Replace("&", "AND");
        SellerAdd = SellerAdd.Replace(".", "");
        SellerAdd = SellerAdd.Replace("#", "");
        SellerAdd = SellerAdd.Replace("(", "");
        SellerAdd = SellerAdd.Replace(")", "");
        SellerAdd = SellerAdd.Replace(":", "");
        SellerAdd = SellerAdd.Replace("_", "");
        SellerAdd = SellerAdd.Replace("@", "");
        SellerAdd = SellerAdd.Replace(";", "");
        SellerAdd = SellerAdd.Replace("=", "");
        SellerAdd = SellerAdd.Replace("\n", "");
        SellerAdd = SellerAdd.Replace("*", "");
        SellerAdd = SellerAdd.Replace(",", "");
        SellerAdd = SellerAdd.Replace("'", "");
        SellerAdd = SellerAdd.Replace("\"", "");
        SellerAdd = SellerAdd.Replace(@"\", "");

        string SellerAddress = SellerAdd;

        string SellerLoc = txtSeller_Location.Text;
        SellerLoc = SellerLoc.Replace("-", "");
        SellerLoc = SellerLoc.Replace("/", "");
        SellerLoc = SellerLoc.Replace("&", "AND");
        SellerLoc = SellerLoc.Replace(".", "");
        SellerLoc = SellerLoc.Replace("#", "");
        SellerLoc = SellerLoc.Replace("(", "");
        SellerLoc = SellerLoc.Replace(")", "");
        SellerLoc = SellerLoc.Replace(":", "");
        SellerLoc = SellerLoc.Replace("_", "");
        SellerLoc = SellerLoc.Replace("@", "");
        SellerLoc = SellerLoc.Replace(";", "");
        SellerLoc = SellerLoc.Replace("=", "");
        SellerLoc = SellerLoc.Replace("\n", "");
        SellerLoc = SellerLoc.Replace("*", "");
        SellerLoc = SellerLoc.Replace(",", "");
        SellerLoc = SellerLoc.Replace("'", "");
        SellerLoc = SellerLoc.Replace("\"", "");
        SellerLoc = SellerLoc.Replace(@"\", "");


        string SellerLocation = SellerLoc;

        string SellerStateNm = txtSeller_StateName.Text;
        SellerStateNm = SellerStateNm.Replace("-", "");
        SellerStateNm = SellerStateNm.Replace("/", "");
        SellerStateNm = SellerStateNm.Replace("&", "AND");
        SellerStateNm = SellerStateNm.Replace(".", "");
        SellerStateNm = SellerStateNm.Replace("#", "");
        SellerStateNm = SellerStateNm.Replace("(", "");
        SellerStateNm = SellerStateNm.Replace(")", "");
        SellerStateNm = SellerStateNm.Replace(":", "");
        SellerStateNm = SellerStateNm.Replace("_", "");
        SellerStateNm = SellerStateNm.Replace("@", "");
        SellerStateNm = SellerStateNm.Replace(";", "");
        SellerStateNm = SellerStateNm.Replace("=", "");
        SellerStateNm = SellerStateNm.Replace("\n", "");
        SellerStateNm = SellerStateNm.Replace("*", "");
        SellerStateNm = SellerStateNm.Replace(",", "");
        SellerStateNm = SellerStateNm.Replace("'", "");
        SellerStateNm = SellerStateNm.Replace("\"", "");
        SellerStateNm = SellerStateNm.Replace(@"\", "");

        string SellerStateName = SellerStateNm;

        string Seller_Email = txtSeller_Email.Text;
        Seller_Email = Seller_Email.Replace(@"\", "");
        Seller_Email = Seller_Email.Replace("\"", "");
        Seller_Email = Seller_Email.Replace("'", "");
        string Seller_EmailID = Seller_Email;
        string SellerPhone = txtSeller_Phno.Text;

        string SellerGstin = txtSellerGST_No.Text;
        string SellerPin = txtSeller_PIN.Text;
        string SellerStcd = txtSeller_StateCode.Text;
        #endregion

        # region Buyer
        string BuyerLglNm = txtBuyerName.Text;
        BuyerLglNm = BuyerLglNm.Replace("-", "");
        BuyerLglNm = BuyerLglNm.Replace("/", "");
        BuyerLglNm = BuyerLglNm.Replace("&", "AND");
        BuyerLglNm = BuyerLglNm.Replace(".", "");
        BuyerLglNm = BuyerLglNm.Replace("#", "");
        BuyerLglNm = BuyerLglNm.Replace("(", "");
        BuyerLglNm = BuyerLglNm.Replace(")", "");
        BuyerLglNm = BuyerLglNm.Replace(":", "");
        BuyerLglNm = BuyerLglNm.Replace("_", "");
        BuyerLglNm = BuyerLglNm.Replace("@", "");
        BuyerLglNm = BuyerLglNm.Replace(";", "");
        BuyerLglNm = BuyerLglNm.Replace("=", "");
        BuyerLglNm = BuyerLglNm.Replace("\n", "");
        BuyerLglNm = BuyerLglNm.Replace("*", "");
        BuyerLglNm = BuyerLglNm.Replace(",", "");
        BuyerLglNm = BuyerLglNm.Replace("'", "");
        BuyerLglNm = BuyerLglNm.Replace("\"", "");
        BuyerLglNm = BuyerLglNm.Replace(@"\", "");


        string BuyerLglName = BuyerLglNm;

        string BuyerAdd = txtBuyer_Address.Text;
        BuyerAdd = BuyerAdd.Replace("-", "");
        BuyerAdd = BuyerAdd.Replace("/", "");
        BuyerAdd = BuyerAdd.Replace("&", "AND");
        BuyerAdd = BuyerAdd.Replace(".", "");
        BuyerAdd = BuyerAdd.Replace("#", "");
        BuyerAdd = BuyerAdd.Replace("(", "");
        BuyerAdd = BuyerAdd.Replace(")", "");
        BuyerAdd = BuyerAdd.Replace(":", "");
        BuyerAdd = BuyerAdd.Replace("_", "");
        BuyerAdd = BuyerAdd.Replace("@", "");
        BuyerAdd = BuyerAdd.Replace(";", "");
        BuyerAdd = BuyerAdd.Replace("=", "");
        BuyerAdd = BuyerAdd.Replace("\n", "");
        BuyerAdd = BuyerAdd.Replace("*", "");
        BuyerAdd = BuyerAdd.Replace(",", "");
        BuyerAdd = BuyerAdd.Replace("'", "");
        BuyerAdd = BuyerAdd.Replace("\"", "");
        BuyerAdd = BuyerAdd.Replace(@"\", "");


        string BuyerAddress = BuyerAdd;

        string BuyerLoc = txtBuyer_Location.Text;
        BuyerLoc = BuyerLoc.Replace("-", "");
        BuyerLoc = BuyerLoc.Replace("/", "");
        BuyerLoc = BuyerLoc.Replace("&", "AND");
        BuyerLoc = BuyerLoc.Replace(".", "");
        BuyerLoc = BuyerLoc.Replace("#", "");
        BuyerLoc = BuyerLoc.Replace("(", "");
        BuyerLoc = BuyerLoc.Replace(")", "");
        BuyerLoc = BuyerLoc.Replace(":", "");
        BuyerLoc = BuyerLoc.Replace("_", "");
        BuyerLoc = BuyerLoc.Replace("@", "");
        BuyerLoc = BuyerLoc.Replace(";", "");
        BuyerLoc = BuyerLoc.Replace("=", "");
        BuyerLoc = BuyerLoc.Replace("\n", "");
        BuyerLoc = BuyerLoc.Replace("*", "");
        BuyerLoc = BuyerLoc.Replace(",", "");
        BuyerLoc = BuyerLoc.Replace("'", "");
        BuyerLoc = BuyerLoc.Replace("\"", "");
        BuyerLoc = BuyerLoc.Replace(@"\", "");

        string BuyerLocation = BuyerLoc;

        string BuyerStateNm = txtBuyer_StateName.Text;
        BuyerStateNm = BuyerStateNm.Replace("-", "");
        BuyerStateNm = BuyerStateNm.Replace("/", "");
        BuyerStateNm = BuyerStateNm.Replace("&", "AND");
        BuyerStateNm = BuyerStateNm.Replace(".", "");
        BuyerStateNm = BuyerStateNm.Replace("#", "");
        BuyerStateNm = BuyerStateNm.Replace("(", "");
        BuyerStateNm = BuyerStateNm.Replace(")", "");
        BuyerStateNm = BuyerStateNm.Replace(":", "");
        BuyerStateNm = BuyerStateNm.Replace("_", "");
        BuyerStateNm = BuyerStateNm.Replace("@", "");
        BuyerStateNm = BuyerStateNm.Replace(";", "");
        BuyerStateNm = BuyerStateNm.Replace("=", "");
        BuyerStateNm = BuyerStateNm.Replace("\n", "");
        BuyerStateNm = BuyerStateNm.Replace("*", "");
        BuyerStateNm = BuyerStateNm.Replace(",", "");
        BuyerStateNm = BuyerStateNm.Replace("'", "");
        BuyerStateNm = BuyerStateNm.Replace("\"", "");
        BuyerStateNm = BuyerStateNm.Replace(@"\", "");


        string BuyerStateName = BuyerStateNm;

        string Buyer_Email = txtBuyer_Email.Text;
        Buyer_Email = Buyer_Email.Replace(@"\", "");
        Buyer_Email = Buyer_Email.Replace("\"", "");
        Buyer_Email = Buyer_Email.Replace("'", "");
        string Buyer_EmailID = Buyer_Email;

        string BuyerStcd = txtBuyer_StateCode.Text;
        String BuyerGstin = txtBuyerGST_No.Text;
        string Buyer_PIN = txtBuyer_PIN.Text;
        #endregion

        #region Dispatch
        string DispatchLglNm = txtDispatch_Name.Text;
        DispatchLglNm = DispatchLglNm.Replace("-", "");
        DispatchLglNm = DispatchLglNm.Replace("/", "");
        DispatchLglNm = DispatchLglNm.Replace("&", "AND");
        DispatchLglNm = DispatchLglNm.Replace(".", "");
        DispatchLglNm = DispatchLglNm.Replace("#", "");
        DispatchLglNm = DispatchLglNm.Replace("(", "");
        DispatchLglNm = DispatchLglNm.Replace(")", "");
        DispatchLglNm = DispatchLglNm.Replace(":", "");
        DispatchLglNm = DispatchLglNm.Replace("_", "");
        DispatchLglNm = DispatchLglNm.Replace("@", "");
        DispatchLglNm = DispatchLglNm.Replace(";", "");
        DispatchLglNm = DispatchLglNm.Replace("=", "");
        DispatchLglNm = DispatchLglNm.Replace("\n", "");
        DispatchLglNm = DispatchLglNm.Replace("*", "");
        DispatchLglNm = DispatchLglNm.Replace(",", "");
        DispatchLglNm = DispatchLglNm.Replace("'", "");
        DispatchLglNm = DispatchLglNm.Replace("\"", "");
        DispatchLglNm = DispatchLglNm.Replace(@"\", "");



        string DispatchLglName = DispatchLglNm;

        string DispatchAdd = txtDispatch_Address.Text;
        DispatchAdd = DispatchAdd.Replace("-", "");
        DispatchAdd = DispatchAdd.Replace("/", "");
        DispatchAdd = DispatchAdd.Replace("&", "AND");
        DispatchAdd = DispatchAdd.Replace(".", "");
        DispatchAdd = DispatchAdd.Replace("#", "");
        DispatchAdd = DispatchAdd.Replace("(", "");
        DispatchAdd = DispatchAdd.Replace(")", "");
        DispatchAdd = DispatchAdd.Replace(":", "");
        DispatchAdd = DispatchAdd.Replace("_", "");
        DispatchAdd = DispatchAdd.Replace("@", "");
        DispatchAdd = DispatchAdd.Replace(";", "");
        DispatchAdd = DispatchAdd.Replace("=", "");
        DispatchAdd = DispatchAdd.Replace("\n", "");
        DispatchAdd = DispatchAdd.Replace("*", "");
        DispatchAdd = DispatchAdd.Replace(",", "");
        DispatchAdd = DispatchAdd.Replace("'", "");
        DispatchAdd = DispatchAdd.Replace("\"", "");
        DispatchAdd = DispatchAdd.Replace(@"\", "");


        string DispatchAddress = DispatchAdd;

        string DispatchLoc = txtDispatch_Location.Text;
        DispatchLoc = DispatchLoc.Replace("-", "");
        DispatchLoc = DispatchLoc.Replace("/", "");
        DispatchLoc = DispatchLoc.Replace("&", "AND");
        DispatchLoc = DispatchLoc.Replace(".", "");
        DispatchLoc = DispatchLoc.Replace("#", "");
        DispatchLoc = DispatchLoc.Replace("(", "");
        DispatchLoc = DispatchLoc.Replace(")", "");
        DispatchLoc = DispatchLoc.Replace(":", "");
        DispatchLoc = DispatchLoc.Replace("_", "");
        DispatchLoc = DispatchLoc.Replace("@", "");
        DispatchLoc = DispatchLoc.Replace(";", "");
        DispatchLoc = DispatchLoc.Replace("=", "");
        DispatchLoc = DispatchLoc.Replace("\n", "");
        DispatchLoc = DispatchLoc.Replace("*", "");
        DispatchLoc = DispatchLoc.Replace(",", "");
        DispatchLoc = DispatchLoc.Replace("'", "");
        DispatchLoc = DispatchLoc.Replace("\"", "");
        DispatchLoc = DispatchLoc.Replace(@"\", "");



        string DispatchLocation = DispatchLoc;

        string DispatchGstin = txtDispatch_GSTNo.Text;
        string DisparchPin = txtDispatch_PincodeCode.Text;
        string DispatchStcd = txtDispatch_StateCode.Text;
        #endregion

        #region ShipTo
        string ShipToLglNm = txtShip_Name.Text;
        ShipToLglNm = ShipToLglNm.Replace("-", "");
        ShipToLglNm = ShipToLglNm.Replace("/", "");
        ShipToLglNm = ShipToLglNm.Replace("&", "AND");
        ShipToLglNm = ShipToLglNm.Replace(".", "");
        ShipToLglNm = ShipToLglNm.Replace("#", "");
        ShipToLglNm = ShipToLglNm.Replace("(", "");
        ShipToLglNm = ShipToLglNm.Replace(")", "");
        ShipToLglNm = ShipToLglNm.Replace(":", "");
        ShipToLglNm = ShipToLglNm.Replace("_", "");
        ShipToLglNm = ShipToLglNm.Replace("@", "");
        ShipToLglNm = ShipToLglNm.Replace(";", "");
        ShipToLglNm = ShipToLglNm.Replace("=", "");
        ShipToLglNm = ShipToLglNm.Replace("\n", "");
        ShipToLglNm = ShipToLglNm.Replace("*", "");
        ShipToLglNm = ShipToLglNm.Replace(",", "");
        ShipToLglNm = ShipToLglNm.Replace("'", "");
        ShipToLglNm = ShipToLglNm.Replace("\"", "");
        ShipToLglNm = ShipToLglNm.Replace(@"\", "");



        string ShipToLglName = ShipToLglNm;

        string ShipToAdd = txtShipTo_Address.Text;
        ShipToAdd = ShipToAdd.Replace("-", "");
        ShipToAdd = ShipToAdd.Replace("/", "");
        ShipToAdd = ShipToAdd.Replace("&", "AND");
        ShipToAdd = ShipToAdd.Replace(".", "");
        ShipToAdd = ShipToAdd.Replace("#", "");
        ShipToAdd = ShipToAdd.Replace("(", "");
        ShipToAdd = ShipToAdd.Replace(")", "");
        ShipToAdd = ShipToAdd.Replace(":", "");
        ShipToAdd = ShipToAdd.Replace("_", "");
        ShipToAdd = ShipToAdd.Replace("@", "");
        ShipToAdd = ShipToAdd.Replace(";", "");
        ShipToAdd = ShipToAdd.Replace("=", "");
        ShipToAdd = ShipToAdd.Replace("\n", "");
        ShipToAdd = ShipToAdd.Replace("*", "");
        ShipToAdd = ShipToAdd.Replace(",", "");
        ShipToAdd = ShipToAdd.Replace("'", "");
        ShipToAdd = ShipToAdd.Replace("\"", "");
        ShipToAdd = ShipToAdd.Replace(@"\", "");


        string ShipToAddress = ShipToAdd;

        string ShipToLoc = txtShip_To_Location.Text;
        ShipToLoc = ShipToLoc.Replace("-", "");
        ShipToLoc = ShipToLoc.Replace("/", "");
        ShipToLoc = ShipToLoc.Replace("&", "AND");
        ShipToLoc = ShipToLoc.Replace(".", "");
        ShipToLoc = ShipToLoc.Replace("#", "");
        ShipToLoc = ShipToLoc.Replace("(", "");
        ShipToLoc = ShipToLoc.Replace(")", "");
        ShipToLoc = ShipToLoc.Replace(":", "");
        ShipToLoc = ShipToLoc.Replace("_", "");
        ShipToLoc = ShipToLoc.Replace("@", "");
        ShipToLoc = ShipToLoc.Replace(";", "");
        ShipToLoc = ShipToLoc.Replace("=", "");
        ShipToLoc = ShipToLoc.Replace("\n", "");
        ShipToLoc = ShipToLoc.Replace("*", "");
        ShipToLoc = ShipToLoc.Replace(",", "");
        ShipToLoc = ShipToLoc.Replace("'", "");
        ShipToLoc = ShipToLoc.Replace("\"", "");
        ShipToLoc = ShipToLoc.Replace(@"\", "");



        string ShipToLocation = ShipToLoc;

        string ShipToGstin = txtShip_To_GSTNo.Text;
        string ShipToPin = txtShip_To_PinCode.Text;
        string ShipToStcd = txtShip_TostateCode.Text;
        #endregion

        string AssVal = txtAssessable_Value.Text;
        string CgstVal = txtCGST_Value.Text;
        string SgstVal = txtSGST_Value.Text;
        string IgstVal = txtIGST_Value.Text;
        string CesVal = txtCess_Value.Text;

        double CGST = 0.00;
        double SGST = 0.00;
        double IGST = 0.00;
        double CessAmt = 0.00;
        double CessNontAdvol = 0.00;
        double OtherCharges = 0.00;
        double Assessable_Value = 0.00;
        double Assessable_Amount = 0.00;
        double StCesVal = 0.00;
        double Cess_Value = 0.00;
        CGST = Convert.ToDouble(txtCGST_Amt.Text);
        SGST = Convert.ToDouble(txtSGST_Amt.Text);
        IGST = Convert.ToDouble(txtIGST_Amt.Text);
        Assessable_Value = Convert.ToDouble(txtAssessable_Value.Text);
        Assessable_Amount = Convert.ToDouble(txtAssessable_Amount.Text);
        StCesVal = Convert.ToDouble(txtStCesVal.Text);
        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);

        double TotInv_Value = Assessable_Amount + CGST + SGST + IGST + Cess_Value + StCesVal + OtherCharges;
        string TotInvVal = TotInv_Value.ToString();
        string StCesVal1 = txtStCesVal.Text;
        string Disc = txtDiscount.Text;
        string OthChrg = txtOther_Charges.Text;
        string Mode = txtMode_of_Payment.Text;
        string AcctDet = txtAccount_Details.Text;
        string Branch = txtBranch.Text;
        string Nm = txtPayeeName.Text;
        string SlNo = hdnfSlNo.Value;
        string PrdNm = txtItem_Name.Text;
        string PrdDesc = txtItem_Description.Text;
        string HsnCd = txtHSN.Text;
        string IsServc = drpIs_Service.SelectedValue;
        string Qty = txtQty.Text;
        string Unit = txtUnit.Text;
        string UnitPrice = txtUnit_Price.Text;
        string GstRt = txtGST_Rate.Text;
        string IgstAmt = txtIGST_Amt.Text;
        string CgstAmt = txtCGST_Amt.Text;
        string SgstAmt = txtSGST_Amt.Text;
        string CesAmt = txtCESS_Advol_Amt.Text;
        string AssAmt = txtAssessable_Amount.Text;
        string TotItemVal = txtTotal_Item_Amt.Text;
        string TotAmt = txtTotal_Bill_Amt.Text;



        string Trans_DocNo = null;
        string Transport_Name = null;
        string Transport_Id = null;
        double to_Distance;
        to_Distance = Convert.ToDouble(txtApproximate_Distance.Text);
        double Distance = to_Distance;
        string Tans_Mode = drpTrance_Mode.SelectedValue;
        string Vehical_Type = drpVehicle_Type.SelectedValue;

        #region Relace Vehicle_No
        vno = txtVehicle_No.Text;
        vno = vno.Replace("-", "");
        vno = vno.Replace("/", "");
        vno = vno.Replace(" ", "");
        vno = vno.Replace("&", "");
        vno = vno.Replace(".", "");
        vno = vno.Replace("#", "");
        vno = vno.Replace("(", "");
        vno = vno.Replace(")", "");
        vno = vno.Replace(":", "");
        vno = vno.Replace("_", "");
        vno = vno.Replace("@", "");
        vno = vno.Replace(";", "");
        vno = vno.Replace("=", "");
        vno = vno.Replace(",", "");
        string Vehical_No = vno;
        #endregion
        string Trans_Datenew = txtDoc_Date.Text;



        string Version = "1.1";

        #endregion

        #region Json file
        string json = string.Empty;
        if (SellerGstin == DispatchGstin)
        {
            #region
            json = "{\"Version\": \"" + Version + "\"," +
                      "\"TranDtls\": {" + "\"SupTyp\":\"" + SupTyp + "\"," +
                      "\"TaxSch\": \"" + TaxSch + "\"," +
                      "\"RegRev\": \"" + RegRev + "\"," +
                      "\"Typ\": \"" + Typ + "\"," +
                      "\"IgstOnIntra\": \"" + IgstOnIntra + "\"}," +
                      "\"DocDtls\": {" + "\"Typ\": \"" + Type + "\"," +
                      "\"No\": \"" + Doc_No + "\"," +
                      "\"Dt\": \"" + Doc_Date + "\"}," +
                      "\"ExpDtls\": {}," +
                      "\"SellerDtls\": {" + "\"LglNm\":\"" + SellerLglName + "\"," +
                      "\"Addr1\":\"" + SellerAddress + "\"," +
                      "\"Loc\":\"" + SellerLocation + "\"," +
                      "\"State\":\"" + SellerStateName + "\"," +
                      "\"Gstin\": \"" + SellerGstin + "\"," +
                      "\"Pin\": " + SellerPin + "," +
                      "\"Stcd\":\"" + SellerStcd + "\"}," +
                      "\"BuyerDtls\": {" + "\"LglNm\":\"" + BuyerLglName + "\"," +
                      "\"TrdNm\": \"" + BuyerLglName + "\"," +
                      "\"Addr1\": \"" + BuyerAddress + "\"," +
                      "\"Loc\":\"" + BuyerLocation + "\"," +
                      "\"Pin\":" + Buyer_PIN + "," +
                      "\"State\":\"" + BuyerStateName + "\"," +
                      "\"Pos\": \"" + BuyerStcd + "\"," +
                      "\"Gstin\":\"" + BuyerGstin + "\"," +
                      "\"Stcd\":\"" + BuyerStcd + "\"}," +
                      "\"ShipDtls\": {" + "\"Gstin\": \"" + ShipToGstin + "\"," +
                      "\"LglNm\":\"" + ShipToLglName + "\"," +
                      "\"TrdNm\":\"" + ShipToLglName + "\"," +
                      "\"Addr1\":\"" + ShipToAdd + "\"," +
                      "\"Loc\":\"" + ShipToLocation + "\"," +
                      "\"Pin\": " + ShipToPin + "," +
                      "\"Stcd\":\"" + ShipToStcd + "\"}," +
                      "\"ValDtls\": {" + "\"AssVal\":" + AssVal + "," +
                      "\"CgstVal\": " + CgstVal + "," +
                      "\"SgstVal\": " + SgstVal + "," +
                      "\"IgstVal\": " + IgstVal + "," +
                      "\"CesVal\": " + CesVal + "," +
                      "\"TotInvVal\":" + TotInvVal + "," +
                      "\"StCesVal\": " + StCesVal1 + "," +
                      "\"Disc\": " + Disc + "," +
                      "\"OthChrg\": " + OthChrg + "}," +
                      "\"PayDtls\": {" + "\"Mode\":\"" + Mode + "\"," +
                      "\"AcctDet\": \"" + AcctDet + "\"," +
                      "\"Branch\": \"" + Branch + "\"," +
                      "\"Nm\": \"" + Nm + "\"}," +
                      "\"ItemList\": [{" + "\"SlNo\":\"" + SlNo + "\"," +
                      "\"PrdNm\": \"" + PrdNm + "\"," +
                      "\"PrdDesc\":\"" + PrdDesc + "\"," +
                      "\"HsnCd\":\"" + HsnCd + "\"," +
                      "\"IsServc\": \"" + IsServc + "\"," +
                      "\"Qty\": " + Qty + "," +
                      "\"Unit\": \"" + Unit + "\"," +
                      "\"UnitPrice\":" + UnitPrice + "," +
                      "\"GstRt\": " + GstRt + "," +
                      "\"IgstAmt\":  " + IgstAmt + "," +
                      "\"CgstAmt\": " + CgstAmt + "," +
                      "\"SgstAmt\":" + SgstAmt + "," +
                      "\"CesAmt\": " + CesAmt + "," +
                      "\"AssAmt\":" + AssAmt + "," +
                      "\"TotItemVal\": " + TotItemVal + "," +
                       "\"TotAmt\":" + TotAmt + "}]," +
                      "\"EwbDtls\": {" + "\"Transid\":null," +
                      "\"Transname\": null," +
                      "\"Distance\": " + Distance + "," +
                      "\"Transdocno\":null," +
                      "\"TransdocDt\": \"" + Trans_Datenew + "\"," +
                      "\"Vehno\": \"" + Vehical_No + "\"," +
                      "\"Vehtype\": \"" + Vehical_Type + "\"," +
                      "\"TransMode\": \"" + Tans_Mode + "\"}}";
            #endregion
        }
        else if (BuyerGstin == ShipToGstin)
        {
            #region
            json = "{\"Version\": \"" + Version + "\"," +
                      "\"TranDtls\": {" + "\"SupTyp\":\"" + SupTyp + "\"," +
                      "\"TaxSch\": \"" + TaxSch + "\"," +
                      "\"RegRev\": \"" + RegRev + "\"," +
                      "\"Typ\": \"" + Typ + "\"," +
                      "\"IgstOnIntra\": \"" + IgstOnIntra + "\"}," +
                      "\"DocDtls\": {" + "\"Typ\": \"" + Type + "\"," +
                      "\"No\": \"" + Doc_No + "\"," +
                      "\"Dt\": \"" + Doc_Date + "\"}," +
                      "\"ExpDtls\": {}," +
                      "\"SellerDtls\": {" + "\"LglNm\":\"" + SellerLglName + "\"," +
                      "\"Addr1\":\"" + SellerAddress + "\"," +
                      "\"Loc\":\"" + SellerLocation + "\"," +
                      "\"State\":\"" + SellerStateName + "\"," +
                      "\"Gstin\": \"" + SellerGstin + "\"," +
                      "\"Pin\": " + SellerPin + "," +
                      "\"Stcd\":\"" + SellerStcd + "\"}," +
                      "\"BuyerDtls\": {" + "\"LglNm\":\"" + BuyerLglName + "\"," +
                      "\"TrdNm\": \"" + BuyerLglName + "\"," +
                      "\"Addr1\": \"" + BuyerAddress + "\"," +
                      "\"Loc\":\"" + BuyerLocation + "\"," +
                      "\"Pin\":" + Buyer_PIN + "," +
                      "\"State\":\"" + BuyerStateName + "\"," +
                      "\"Pos\": \"" + BuyerStcd + "\"," +
                      "\"Gstin\":\"" + BuyerGstin + "\"," +
                      "\"Stcd\":\"" + BuyerStcd + "\"}," +
                      "\"DispDtls\": {" + "\"Nm\": \"" + DispatchLglName + "\"," +
                      "\"Addr1\":\"" + DispatchAddress + "\"," +
                      "\"Loc\":\"" + DispatchLocation + "\"," +
                      "\"Gstin\":\"" + DispatchGstin + "\"," +
                      "\"Pin\": " + DisparchPin + "," +
                      "\"Stcd\": \"" + DispatchStcd + "\"}," +
                      "\"ValDtls\": {" + "\"AssVal\":" + AssVal + "," +
                      "\"CgstVal\": " + CgstVal + "," +
                      "\"SgstVal\": " + SgstVal + "," +
                      "\"IgstVal\": " + IgstVal + "," +
                      "\"CesVal\": " + CesVal + "," +
                      "\"TotInvVal\":" + TotInvVal + "," +
                      "\"StCesVal\": " + StCesVal1 + "," +
                      "\"Disc\": " + Disc + "," +
                      "\"OthChrg\": " + OthChrg + "}," +
                      "\"PayDtls\": {" + "\"Mode\":\"" + Mode + "\"," +
                      "\"AcctDet\": \"" + AcctDet + "\"," +
                      "\"Branch\": \"" + Branch + "\"," +
                      "\"Nm\": \"" + Nm + "\"}," +
                      "\"ItemList\": [{" + "\"SlNo\":\"" + SlNo + "\"," +
                      "\"PrdNm\": \"" + PrdNm + "\"," +
                      "\"PrdDesc\":\"" + PrdDesc + "\"," +
                      "\"HsnCd\":\"" + HsnCd + "\"," +
                      "\"IsServc\": \"" + IsServc + "\"," +
                      "\"Qty\": " + Qty + "," +
                      "\"Unit\": \"" + Unit + "\"," +
                      "\"UnitPrice\":" + UnitPrice + "," +
                      "\"GstRt\": " + GstRt + "," +
                      "\"IgstAmt\":  " + IgstAmt + "," +
                      "\"CgstAmt\": " + CgstAmt + "," +
                      "\"SgstAmt\":" + SgstAmt + "," +
                      "\"CesAmt\": " + CesAmt + "," +
                      "\"AssAmt\":" + AssAmt + "," +
                      "\"TotItemVal\": " + TotItemVal + "," +
                       "\"TotAmt\":" + TotAmt + "}]," +
                      "\"EwbDtls\": {" + "\"Transid\":null," +
                      "\"Transname\": null," +
                      "\"Distance\": " + Distance + "," +
                      "\"Transdocno\":null," +
                      "\"TransdocDt\": \"" + Trans_Datenew + "\"," +
                      "\"Vehno\": \"" + Vehical_No + "\"," +
                      "\"Vehtype\": \"" + Vehical_Type + "\"," +
                      "\"TransMode\": \"" + Tans_Mode + "\"}}";

            #endregion
        }
        else if (SellerGstin == DispatchGstin && BuyerGstin == ShipToGstin)
        {
            #region
            json = "{\"Version\": \"" + Version + "\"," +
                      "\"TranDtls\": {" + "\"SupTyp\":\"" + SupTyp + "\"," +
                      "\"TaxSch\": \"" + TaxSch + "\"," +
                      "\"RegRev\": \"" + RegRev + "\"," +
                      "\"Typ\": \"" + Typ + "\"," +
                      "\"IgstOnIntra\": \"" + IgstOnIntra + "\"}," +
                      "\"DocDtls\": {" + "\"Typ\": \"" + Type + "\"," +
                      "\"No\": \"" + Doc_No + "\"," +
                      "\"Dt\": \"" + Doc_Date + "\"}," +
                      "\"ExpDtls\": {}," +
                      "\"SellerDtls\": {" + "\"LglNm\":\"" + SellerLglName + "\"," +
                      "\"Addr1\":\"" + SellerAddress + "\"," +
                      "\"Loc\":\"" + SellerLocation + "\"," +
                      "\"State\":\"" + SellerStateName + "\"," +
                      "\"Gstin\": \"" + SellerGstin + "\"," +
                      "\"Pin\": " + SellerPin + "," +
                      "\"Stcd\":\"" + SellerStcd + "\"}," +
                      "\"BuyerDtls\": {" + "\"LglNm\":\"" + BuyerLglName + "\"," +
                      "\"TrdNm\": \"" + BuyerLglName + "\"," +
                      "\"Addr1\": \"" + BuyerAddress + "\"," +
                      "\"Loc\":\"" + BuyerLocation + "\"," +
                      "\"Pin\":" + Buyer_PIN + "," +
                      "\"State\":\"" + BuyerStateName + "\"," +
                      "\"Pos\": \"" + BuyerStcd + "\"," +
                      "\"Gstin\":\"" + BuyerGstin + "\"," +
                      "\"Stcd\":\"" + BuyerStcd + "\"}," +
                      "\"ValDtls\": {" + "\"AssVal\":" + AssVal + "," +
                      "\"CgstVal\": " + CgstVal + "," +
                      "\"SgstVal\": " + SgstVal + "," +
                      "\"IgstVal\": " + IgstVal + "," +
                      "\"CesVal\": " + CesVal + "," +
                      "\"TotInvVal\":" + TotInvVal + "," +
                      "\"StCesVal\": " + StCesVal1 + "," +
                      "\"Disc\": " + Disc + "," +
                      "\"OthChrg\": " + OthChrg + "}," +
                      "\"PayDtls\": {" + "\"Mode\":\"" + Mode + "\"," +
                      "\"AcctDet\": \"" + AcctDet + "\"," +
                      "\"Branch\": \"" + Branch + "\"," +
                      "\"Nm\": \"" + Nm + "\"}," +
                      "\"ItemList\": [{" + "\"SlNo\":\"" + SlNo + "\"," +
                      "\"PrdNm\": \"" + PrdNm + "\"," +
                      "\"PrdDesc\":\"" + PrdDesc + "\"," +
                      "\"HsnCd\":\"" + HsnCd + "\"," +
                      "\"IsServc\": \"" + IsServc + "\"," +
                      "\"Qty\": " + Qty + "," +
                      "\"Unit\": \"" + Unit + "\"," +
                      "\"UnitPrice\":" + UnitPrice + "," +
                      "\"GstRt\": " + GstRt + "," +
                      "\"IgstAmt\":  " + IgstAmt + "," +
                      "\"CgstAmt\": " + CgstAmt + "," +
                      "\"SgstAmt\":" + SgstAmt + "," +
                      "\"CesAmt\": " + CesAmt + "," +
                      "\"AssAmt\":" + AssAmt + "," +
                      "\"TotItemVal\": " + TotItemVal + "," +
                       "\"TotAmt\":" + TotAmt + "}]," +
                      "\"EwbDtls\": {" + "\"Transid\":null," +
                      "\"Transname\": null," +
                      "\"Distance\": " + Distance + "," +
                      "\"Transdocno\":null," +
                      "\"TransdocDt\": \"" + Trans_Datenew + "\"," +
                      "\"Vehno\": \"" + Vehical_No + "\"," +
                      "\"Vehtype\": \"" + Vehical_Type + "\"," +
                      "\"TransMode\": \"" + Tans_Mode + "\"}}";
            #endregion
        }
        else
        {
            #region
            json = "{\"Version\": \"" + Version + "\"," +
                      "\"TranDtls\": {" + "\"SupTyp\":\"" + SupTyp + "\"," +
                      "\"TaxSch\": \"" + TaxSch + "\"," +
                      "\"RegRev\": \"" + RegRev + "\"," +
                      "\"Typ\": \"" + Typ + "\"," +
                      "\"IgstOnIntra\": \"" + IgstOnIntra + "\"}," +
                      "\"DocDtls\": {" + "\"Typ\": \"" + Type + "\"," +
                      "\"No\": \"" + Doc_No + "\"," +
                      "\"Dt\": \"" + Doc_Date + "\"}," +
                      "\"ExpDtls\": {}," +
                      "\"SellerDtls\": {" + "\"LglNm\":\"" + SellerLglName + "\"," +
                      "\"Addr1\":\"" + SellerAddress + "\"," +
                      "\"Loc\":\"" + SellerLocation + "\"," +
                      "\"State\":\"" + SellerStateName + "\"," +
                      "\"Gstin\": \"" + SellerGstin + "\"," +
                      "\"Pin\": " + SellerPin + "," +
                      "\"Stcd\":\"" + SellerStcd + "\"}," +
                      "\"BuyerDtls\": {" + "\"LglNm\":\"" + BuyerLglName + "\"," +
                      "\"TrdNm\": \"" + BuyerLglName + "\"," +
                      "\"Addr1\": \"" + BuyerAddress + "\"," +
                      "\"Loc\":\"" + BuyerLocation + "\"," +
                      "\"Pin\":" + Buyer_PIN + "," +
                      "\"State\":\"" + BuyerStateName + "\"," +
                      "\"Pos\": \"" + BuyerStcd + "\"," +
                      "\"Gstin\":\"" + BuyerGstin + "\"," +
                      "\"Stcd\":\"" + BuyerStcd + "\"}," +
                      "\"DispDtls\": {" + "\"Nm\": \"" + DispatchLglName + "\"," +
                      "\"Addr1\":\"" + DispatchAddress + "\"," +
                      "\"Loc\":\"" + DispatchLocation + "\"," +
                      "\"Gstin\":\"" + DispatchGstin + "\"," +
                      "\"Pin\": " + DisparchPin + "," +
                      "\"Stcd\": \"" + DispatchStcd + "\"}," +
                      "\"ShipDtls\": {" + "\"Gstin\": \"" + ShipToGstin + "\"," +
                      "\"LglNm\":\"" + ShipToLglName + "\"," +
                      "\"TrdNm\":\"" + ShipToLglName + "\"," +
                      "\"Addr1\":\"" + ShipToAdd + "\"," +
                      "\"Loc\":\"" + ShipToLocation + "\"," +
                      "\"Pin\": " + ShipToPin + "," +
                      "\"Stcd\":\"" + ShipToStcd + "\"}," +
                      "\"ValDtls\": {" + "\"AssVal\":" + AssVal + "," +
                      "\"CgstVal\": " + CgstVal + "," +
                      "\"SgstVal\": " + SgstVal + "," +
                      "\"IgstVal\": " + IgstVal + "," +
                      "\"CesVal\": " + CesVal + "," +
                      "\"TotInvVal\":" + TotInvVal + "," +
                      "\"StCesVal\": " + StCesVal1 + "," +
                      "\"Disc\": " + Disc + "," +
                      "\"OthChrg\": " + OthChrg + "}," +
                      "\"PayDtls\": {" + "\"Mode\":\"" + Mode + "\"," +
                      "\"AcctDet\": \"" + AcctDet + "\"," +
                      "\"Branch\": \"" + Branch + "\"," +
                      "\"Nm\": \"" + Nm + "\"}," +
                      "\"ItemList\": [{" + "\"SlNo\":\"" + SlNo + "\"," +
                      "\"PrdNm\": \"" + PrdNm + "\"," +
                      "\"PrdDesc\":\"" + PrdDesc + "\"," +
                      "\"HsnCd\":\"" + HsnCd + "\"," +
                      "\"IsServc\": \"" + IsServc + "\"," +
                      "\"Qty\": " + Qty + "," +
                      "\"Unit\": \"" + Unit + "\"," +
                      "\"UnitPrice\":" + UnitPrice + "," +
                      "\"GstRt\": " + GstRt + "," +
                      "\"IgstAmt\":  " + IgstAmt + "," +
                      "\"CgstAmt\": " + CgstAmt + "," +
                      "\"SgstAmt\":" + SgstAmt + "," +
                      "\"CesAmt\": " + CesAmt + "," +
                      "\"AssAmt\":" + AssAmt + "," +
                      "\"TotItemVal\": " + TotItemVal + "," +
                       "\"TotAmt\":" + TotAmt + "}]," +
                      "\"EwbDtls\": {" + "\"Transid\":null," +
                      "\"Transname\": null," +
                      "\"Distance\": " + Distance + "," +
                      "\"Transdocno\":null," +
                      "\"TransdocDt\": \"" + Trans_Datenew + "\"," +
                      "\"Vehno\": \"" + Vehical_No + "\"," +
                      "\"Vehtype\": \"" + Vehical_Type + "\"," +
                      "\"TransMode\": \"" + Tans_Mode + "\"}}";
            #endregion
        }
        #endregion

        //string username = "C1C2ED38DD58491597584CE1199B9ECF";
        //string password = "5C14DBC6GAD4CG4FC4G9FE3GC2E4B88A8FCA";

        ////string username = "FE39998210EF410AA9279254EF664D07";
        ////string password = "1724285EG420DG476BGAE4FGB9B2B78EABDD";



        ////string urlAddress = "https://gsp.adaequare.com/gsp/authenticate?grant_type=token";
        ////string urlAddress1 = " https://gsp.adaequare.com/test/enriched/ewb/ewayapi?action=GENEWAYBILL";
        // string urlAddress1 = " https://gsp.adaequare.com/enriched/ewb/ewayapi?action=GENEWAYBILL";
        //string urlAddress1 = "https://gsp.adaequare.com/enriched/ei/api/invoice";



        ////string USERNAME = "navkar789_API_123";
        ////string PASSWORD = "navkartraders12";
        ////string gstin = "27AABHJ9303C1ZM";




        string username = clsCommon.getString("select E_UserName from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string password = clsCommon.getString("select E_UserPassword from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string urlAddress = clsCommon.getString("select E_UrlAddress_Token from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string urlAddress1 = clsCommon.getString("select E_Envoice from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string USERNAME = clsCommon.getString("select E_UserName_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string PASSWORD = clsCommon.getString("select E_UserPassword_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        string gstin = clsCommon.getString("select E_Company_GSTno from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        string DDate = DateTime.Now.ToString();
        string SBNo = txtDoc_No.Text;
        string requestid = DDate + SBNo + "SS";
        // string token = GetAuthToken(urlAddress, username, password);
        string token = xmlExecuteDMLQry.GetAuthToken(urlAddress, username, password);

        string Ewaybullno = xmlExecuteDMLQry.GenrateEInvoice(urlAddress1, USERNAME, PASSWORD, gstin, requestid, token, json);

       // string Ewaybullno = "";


        string str = Ewaybullno;
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
        string AckNo = string.Empty;
        string Irn = string.Empty;
        Int64 EwbNo = 0;
        string ewaybildate;

        string QRstr = Ewaybullno;
        QRstr = QRstr.Replace("\"", "");
        QRstr = QRstr.Replace(",", "");

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
                    string AckNoNew = getBetween(str, "AckNo", "AckDt");
                    AckNo = AckNoNew;
                    string IrnNew = getBetween(str, "Irn", "SignedInvoice");
                    Irn = IrnNew;

                    string EwbNoNew = getBetween(str, "EwbNo", "EwbDt");
                    EwbNo = Convert.ToInt64(EwbNoNew);
                    string EwaybillDate = getBetween(str, "EwbValidTill", "Remarks");
                    EwaybillDate = EwaybillDate.Remove(EwaybillDate.Length - 7);
                    ewaybildate = DateTime.Parse(EwaybillDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                    string sbno = txtDoc_No.Text;
                    string sbnoNew = txtDoc_No.Text;
                    sbnoNew = sbnoNew.Remove(0, 2);
                    sbno = sbno.Remove(sbno.Length - 2);
                    DataSet ds = new DataSet();
                    qry = "";
                    qry1 = "";
                    Dono = Request.QueryString["dono"];
                    string dosbno = clsCommon.getString("select SB_No from NT_1_deliveryorder where company_code="
                        + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_no=" + Dono);
                    string QRCode = getBetween(QRstr, "SignedQRCode:", "Status:");
                    if (type == "SB")
                    {
                        if (ID == string.Empty)
                        {
                            qry = "update NT_1_deliveryorder set ackno='" + AckNo + "',einvoiceno='" + Irn + "',EWay_Bill_No=" + EwbNo + ",EwayBillValidDate='" + ewaybildate + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_no=" + Dono;
                            ds = clsDAL.SimpleQuery(qry);
                            qry1 = "update NT_1_SugarSale set ackno=" + AckNo + ",einvoiceno='" + Irn + "',Eway_Bill_No=" + EwbNo + ",EwayBillValidDate='" + ewaybildate + "',QRCode='" + QRCode + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                               + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + dosbno;
                            ds = clsDAL.SimpleQuery(qry1);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice & EwayBill Generated Successfully !');", true);

                            Dono = Request.QueryString["dono"];
                            int Action = 1;
                            string DO = clsCommon.getString("select doid from nt_1_deliveryorder where doc_no='" + Dono
                                + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:DoOpen('" + DO + "','" + Action + "')", true);


                        }
                        else
                        {
                            qry1 = "update NT_1_SugarSale set ackno=" + AckNo + ",einvoiceno='" + Irn + "',Eway_Bill_No=" + EwbNo + ",EwayBillValidDate='" + ewaybildate + "',QRCode='" + QRCode + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                              + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono;
                            ds = clsDAL.SimpleQuery(qry1);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice & EwayBill Generated Successfully !');", true);

                            int Action = 1;
                            string DO = clsCommon.getString("select saleid from NT_1_SugarSale where doc_no='" + Dono
                              + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nnn", "javascript:SBOpen('" + DO + "','" + Action + "')", true);
                        }

                    }
                    else if (type == "RR")
                    {
                        qry1 = "update Retail_Head set ACK='" + AckNo + "',Envoiceno='" + Irn + "',EwayBillNo='" + EwbNo + "', QRCode='" + QRCode + "' where  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                         + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono;
                        ds = clsDAL.SimpleQuery(qry1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice & EwayBill Generated Successfully !');", true);

                    }
                    else if (type == "RS")
                    {
                        qry1 = "update nt_1_sugarsalereturn set ackno='" + AckNo + "',einvoiceno='" + Irn + "',Eway_Bill_No='" + EwbNo + "', QRCode='" + QRCode + "' where  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                         + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono;
                        ds = clsDAL.SimpleQuery(qry1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice & EwayBill Generated Successfully !');", true);

                        int Action = 1;
                        string DO = clsCommon.getString("select srid from nt_1_sugarsalereturn where doc_no='" + Dono
                          + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ssn", "javascript:RSOpen('" + DO + "','" + Action + "')", true);

                    }
                    else
                    {
                    }
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
    #endregion

    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        int Start, End;
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }
        else
        {
            return "";
        }
    }

    #region [txtTransporter_Name_TextChanged]
    protected void txtTransporter_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransporter_Name.Text;
        strTextBox = "txtTransporter_Name";

    }
    #endregion
    #region [txtTransporter_ID_TextChanged]
    protected void txtTransporter_ID_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransporter_ID.Text;
        strTextBox = "txtTransporter_ID";

    }
    #endregion
    #region [drpVehicle_Type_TextChanged]
    protected void drpVehicle_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpTrance_Mode_TextChanged]
    protected void drpTrance_Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtVehicle_No_TextChanged]
    protected void txtVehicle_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVehicle_No.Text;
        strTextBox = "txtVehicle_No";

    }
    #endregion

    private void Calculations()
    {
        double qty = 0.00;
        double unitprice = 0.00;
        double gstrate = 0.00;
        double value = 0.00;
        double netvalue = 0.00;

        Int32 dispStateCode = 0;
        Int32 shiptostateCode = 0;

        double cgstamt = 0.00;
        double sgstamt = 0.00;
        double igstamt = 0.00;
        double cgstrate = 0.00;
        double sgstrate = 0.00;
        double igstrate = 0.00;

        double Cess_Value = 0.00;
        double StCesVal = 0.00;
        double OtherCharges = 0.00;

        if (txtQty.Text != "0.00" || txtQty.Text != string.Empty)
        {
            qty = Convert.ToDouble(txtQty.Text);
        }
        if (txtUnit_Price.Text != "0.00" || txtUnit_Price.Text != string.Empty)
        {
            unitprice = Convert.ToDouble(txtUnit_Price.Text);
        }

        value = qty * unitprice;

        if (txtGST_Rate.Text != "0.00" || txtGST_Rate.Text != string.Empty)
        {
            gstrate = Convert.ToDouble(txtGST_Rate.Text);
        }

        if (txtDispatch_StateCode.Text != "0.00" || txtDispatch_StateCode.Text != string.Empty)
        {
            dispStateCode = Convert.ToInt32(txtDispatch_StateCode.Text);
        }
        if (txtShip_TostateCode.Text != "0.00" || txtShip_TostateCode.Text != string.Empty)
        {
            shiptostateCode = Convert.ToInt32(txtShip_TostateCode.Text);
        }
        if (dispStateCode == shiptostateCode)
        {
            cgstrate = gstrate / 2;
            sgstrate = gstrate / 2;
            cgstamt = value * cgstrate / 100;
            sgstamt = value * sgstrate / 100;
            igstamt = 0.00;
        }
        else
        {
            igstrate = gstrate;
            cgstamt = 0.00;
            sgstamt = 0.00;
            igstamt = value * igstrate / 100;
        }

        Cess_Value = Convert.ToDouble(txtCess_Value.Text);
        StCesVal = Convert.ToDouble(txtStCesVal.Text);
        OtherCharges = Convert.ToDouble(txtOther_Charges.Text);
        netvalue = value + cgstamt + sgstamt + igstamt + Cess_Value + StCesVal + OtherCharges;

        txtAssessable_Value.Text = value.ToString();
        txtAssessable_Amount.Text = value.ToString();
        txtTotal_Bill_Amt.Text = value.ToString();

        txtCGST_Value.Text = cgstamt.ToString();
        txtSGST_Value.Text = sgstamt.ToString();
        txtIGST_Value.Text = igstamt.ToString();

        txtCGST_Amt.Text = cgstamt.ToString();
        txtSGST_Amt.Text = sgstamt.ToString();
        txtIGST_Amt.Text = igstamt.ToString();

        txtTotal_Item_Amt.Text = netvalue.ToString();
        txtTotInv_Value.Text = netvalue.ToString();

    }
}