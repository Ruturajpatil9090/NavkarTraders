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
public partial class pgeEwayBill : System.Web.UI.Page
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
    string State_Code_BillTo;
    string State_Code_Mill;
    int Rowaction = 2;
    int Srno = 3;

    string carporateSale = string.Empty;
    string type = "";
    string AutoID = "";
    string selectedyear = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        Dono = Request.QueryString["dono"];
        type = Request.QueryString["Type"];
        AutoID = Request.QueryString["ID"];
        carporateSale = Request.QueryString["carporateSale"];
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
                    //BindEwayBill();
                    if (type == "SB")
                    {
                        BindEwayBill();
                    }
                    else if (type == "RR")
                    {

                        BindRetailEwayBill();
                    }
                    else if (type == "JS")
                    {
                        BindJawakEwayBill();
                    }
                    else if (type == "RS")
                    {

                        BindReturnSaleEwayBill();
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
                    //pnlPopup.Style["display"] = "none";
                }
                else
                {
                    // pnlPopup.Style["display"] = "block";
                    // objAsp = btnSearch;
                }
            }
        }
        catch
        {
        }
    }
    #endregion



    #region old
    //private void BindEwayBill()
    //{
    //    try
    //    {
    //        string qryelement = string.Empty;
    //        if (AutoID == string.Empty)
    //        {
    //            qryelement = " select doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(billtoname) as BillToName,"
    //                          + "UPPER(billtogstno) as BillToGst ,UPPER(shiptoname) as ShippTo,"
    //                          + "UPPER(shiptoaddress) as Address_E,UPPER(shiptocityname) as city_name_e,"
    //                          + "(case billtopincode when 0 then 999999  else billtopincode end) as pincode,"
    //                          + "upper(billtostatecode) AS BillToStateCode, billtostatename as State_Name,"
    //                          + "NETQNTL,TaxableAmount,(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,CGSTAmount,SGSTAmount,IGSTAmount,"
    //                          + "'' as Distance,LORRYNO,UPPER(billfromname) as millname,upper(billfromaddress) as milladdress,"
    //                          + "(case billfrompincode when 0 then 999999  else billfrompincode end) as millpincode,"
    //                          + "billfromcityname,DO_NO as DONO,billfromstatename as millstatename,CONVERT(varchar,doc_date,103) as TransDate,"
    //                          + "CGSTRate,SGSTRate,IGSTRate,billtostatecode,billfromstatecode," +
    //                          "billtostatecode as GSTStateCode,mill_code,Unit_Code,System_Name_E,HSN from qryewaybill " +
    //                           " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
    //                           " and DO_NO=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
    //        }
    //        else
    //        {
    //            qryelement = " select doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(billtoname) as BillToName,"
    //                         + "UPPER(billtogstno) as BillToGst ,UPPER(shiptoname) as ShippTo,"
    //                         + "UPPER(shiptoaddress) as Address_E,UPPER(shiptocityname) as city_name_e,"
    //                         + "(case billtopincode when 0 then 999999  else billtopincode end) as pincode,"
    //                         + "upper(billtostatecode) AS BillToStateCode, billtostatename as State_Name,"
    //                         + "NETQNTL,TaxableAmount,(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,CGSTAmount,SGSTAmount,IGSTAmount,"
    //                         + "'' as Distance,LORRYNO,UPPER(billfromname) as millname,upper(billfromaddress) as milladdress,"
    //                         + "(case billfrompincode when 0 then 999999  else billfrompincode end) as millpincode,"
    //                         + "billfromcityname,DO_NO as DONO,billfromstatename as millstatename,CONVERT(varchar,doc_date,103) as TransDate,"
    //                         + "CGSTRate,SGSTRate,IGSTRate,billtostatecode,billfromstatecode," +
    //                         "billtostatecode as GSTStateCode,mill_code,Unit_Code,System_Name_E,HSN from qryewaybill " +
    //                          " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
    //                          " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
    //        }
    //        #region[from query]

    //        string distance = clsCommon.getString("select distance from NT_1_deliveryorder where Company_Code=" +
    //            Convert.ToInt32(Session["Company_Code"].ToString())
    //            + " AND Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono);

    //        string qrynm = Session["Company_Name"].ToString();
    //        string comnm = qrynm.ToUpper();
    //        string gstno = Session["Company_GST"].ToString();
    //        string address = Session["address"].ToString().ToUpper();
    //        string city = Session["Company_City"].ToString().ToUpper();
    //        string pin = Session["Company_Pincode"].ToString();
    //        string state = Session["mystatename"].ToString().ToLower();
    //        string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
    //        string mobno = Session["Company_Phone"].ToString();

    //        #endregion

    //        double taxamount;
    //        string taxvalue;
    //        double CGST;
    //        double SGST;
    //        double IGST;
    //        double CessAmt = 0.00;
    //        double CessNontAdvol = 0.00;
    //        double Other = 0.00;
    //        string Bill_To;
    //        string Ship_To;
    //        DataSet ds = new DataSet();
    //        ds = clsDAL.SimpleQuery(qryelement);
    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                DataTable dt = ds.Tables[0];
    //                if (dt.Rows.Count > 0)
    //                {
    //                    DataRow dr = null;
    //                    drpSupply_Type.SelectedValue = "0";
    //                    drpSub_Type.SelectedValue = "1";
    //                    drpTran_Type.SelectedValue = "INV";

    //                    string doc_no = type + dt.Rows[0]["Doc_No"].ToString();
    //                    txtDoc_No.Text = doc_no;
    //                    txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
    //                    Bill_To = dt.Rows[0]["BillToName"].ToString();
    //                    Ship_To = dt.Rows[0]["ShippTo"].ToString();
    //                    if (Bill_To != Ship_To)
    //                    {
    //                        drpTran_Type.SelectedValue = "4";

    //                    }
    //                    else
    //                    {
    //                        drpTran_Type.SelectedValue = "3";

    //                    }
    //                    txtBill_From_Name.Text = comnm;
    //                    txtBill_From_Address.Text = dt.Rows[0]["millname"].ToString();
    //                    //txtBill_From_Address.Text = address;
    //                    txtBill_From_Address2.Text = dt.Rows[0]["milladdress"].ToString();
    //                    txtBill_FromGST_No.Text = gstno;
    //                    txtBill_From_Place.Text = dt.Rows[0]["billfromcityname"].ToString();
    //                    txtBill_From_State.Text = state;
    //                    txtBill_From_PinCode.Text = dt.Rows[0]["millpincode"].ToString();
    //                    txtBill_From_State2.Text = dt.Rows[0]["millstatename"].ToString();
    //                    txtBill_To_Name.Text = dt.Rows[0]["BillToName"].ToString();
    //                    txtBill_To_Add.Text = dt.Rows[0]["ShippTo"].ToString();
    //                    txtBill_To_Add2.Text = dt.Rows[0]["Address_E"].ToString();
    //                    txtBill_TO_GSTNo.Text = dt.Rows[0]["BillToGst"].ToString();
    //                    txtBill_To_Place.Text = dt.Rows[0]["city_name_e"].ToString();
    //                    txtBill_To_State.Text = dt.Rows[0]["State_Name"].ToString();
    //                    txtBill_To_PinCode.Text = dt.Rows[0]["pincode"].ToString();
    //                    txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
    //                    txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
    //                    txtHSN.Text = dt.Rows[0]["HSN"].ToString();
    //                    txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
    //                    txtUnit.Text = "QTL";
    //                    txtTaxable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();


    //                    //if(CGST!='')
    //                    //drpCGST_SGST_Rate.SelectedValue = dt.Rows[0]["CGST_SGST_Rate"].ToString();
    //                    //drpIGST_Rate.SelectedValue = dt.Rows[0]["IGST_Rate"].ToString();
    //                    //drpCESS_Advol_Rate.SelectedValue = dt.Rows[0]["CESS_Advol_Rate"].ToString();
    //                    //drpCESS_NonAdvol_Rate.SelectedValue = dt.Rows[0]["CESS_NonAdvol_Rate"].ToString();
    //                    txtTaxable_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();
    //                    txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
    //                    txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
    //                    txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
    //                    txtCESS_Advol_Amt.Text = "0";
    //                    txtCESS_non_Advol_Amt.Text = "0.00";
    //                    txtOther_Amt.Text = "0.00";

    //                    CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
    //                    SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
    //                    IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
    //                    taxamount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
    //                    double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;
    //                    txtTotal_Bill_Amt.Text = TotalInvoice.ToString();

    //                    txtTransporter_Name.Text = "";
    //                    txtTransporter_ID.Text = "";
    //                    txtApproximate_Distance.Text = distance;
    //                    drpTrance_Mode.SelectedValue = "1";
    //                    drpVehicle_Type.SelectedValue = "R";
    //                    txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
    //                    //Trans_Date = dt.Rows[0]["TransDate"].ToString(); doc_date
    //                    Trans_Date = dt.Rows[0]["doc_date"].ToString();

    //                    hdnfCGST_Rate.Value = dt.Rows[0]["CGSTRate"].ToString();
    //                    hdnfSGST_Rate.Value = dt.Rows[0]["SGSTRate"].ToString();
    //                    hdnfIGST_Rate.Value = dt.Rows[0]["IGSTRate"].ToString();
    //                    hdnfState_Code_BillTo.Value = dt.Rows[0]["billtostatecode"].ToString();
    //                    hdnfMillState_Code.Value = dt.Rows[0]["billfromstatecode"].ToString();
    //                    hdnfState_Code.Value = dt.Rows[0]["GSTStateCode"].ToString();
    //                    hdnfmillCode.Value = dt.Rows[0]["mill_code"].ToString();
    //                    hdnfUnitCode.Value = dt.Rows[0]["Unit_Code"].ToString();
    //                    ViewState["currentTable"] = dt;



    //                }
    //                else
    //                {

    //                    ViewState["currentTable"] = null;
    //                }
    //            }
    //            else
    //            {

    //                ViewState["currentTable"] = null;
    //            }
    //        }
    //        else
    //        {

    //            ViewState["currentTable"] = null;
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    #endregion
    private void BindEwayBill()
    {
        try
        {
            string qryelement = string.Empty;
            if (AutoID == string.Empty)
            {
                if (carporateSale == string.Empty)
                {
                    qryelement = "select  doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(BillToName) as BillToName,UPPER(BillToGst) as BillToGst"
                              + ",UPPER(ShippTo) as ShippTo,UPPER(Address_E) as Address_E,UPPER(city_name_e) as city_name_e,"
                              + "(case Pincode when 0 then 999999  else pincode end) as pincode, State_Name as State_Name,NETQNTL,"
                              + "TaxableAmount,(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,"
                              + "CGSTAmount,SGSTAmount,IGSTAmount,0 as Distance,LORRYNO,UPPER(millname) as millname,upper(milladdress) as milladdress,"
                              + "(case millpincode when 0 then 999999  else millpincode end) as millpincode, millcityname,DO_No,millstatename as millstatename,"
                              + "convert(varchar,doc_date,103)as TransDate,CGSTRate,SGSTRate,IGSTRate,state_code_billto,millstatecode,GSTStateCode,mill_code,Unit_Code,System_Name_E,HSN"
                              + " from NT_1qryEwaybill where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                              + " and DO_No=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                }
                else
                {
                    qryelement = "select doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(billtoname) as BillToName,UPPER(billtogstno) as BillToGst ,"
                                 + "UPPER(shiptoname) as ShippTo,UPPER(shiptoaddress) as Address_E,UPPER(shiptocityname) as city_name_e,"
                                 + "(case shiptopincode when 0 then 999999  else shiptopincode end) as pincode,upper(billtostatecode) AS BillToStateCode,"
                                 + "billtostatename as State_Name,NETQNTL,TaxableAmount,(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,"
                                 + "CGSTAmount,SGSTAmount,IGSTAmount,'' as Distance,LORRYNO,UPPER(billfromname) as millname,upper(billfromaddress) as milladdress,"
                                 + " (case billfrompincode when 0 then 999999  else billfrompincode end) as millpincode,billfromcityname as millcityname ,DO_NO ,"
                                 + "billfromstatename as millstatename,CONVERT(varchar,doc_date,103) as TransDate,CGSTRate,SGSTRate,IGSTRate,billtostatecode as state_code_billto,"
                                 + " billfromstatecode as millstatecode,shiptostatecode as GSTStateCode,mill_code,0 as Unit_Code,System_Name_E,HSN from NT_1_qryEwayBillCarporateSale  where "
                                 + "Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DO_NO=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Carporate_Sale_No!=0";
                }
            }
            else
            {

                qryelement = "select  doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(BillToName) as BillToName,UPPER(BillToGst) as BillToGst"
                                 + ",UPPER(ShippTo) as ShippTo,UPPER(Address_E) as Address_E,UPPER(city_name_e) as city_name_e,"
                                 + "(case Pincode when 0 then 999999  else pincode end) as pincode, State_Name as State_Name,NETQNTL,"
                                 + "TaxableAmount,(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,"
                                 + "CGSTAmount,SGSTAmount,IGSTAmount,0 as Distance,LORRYNO,UPPER(millname) as millname,upper(milladdress) as milladdress,"
                                 + "(case millpincode when 0 then 999999  else millpincode end) as millpincode, millcityname,DO_No,millstatename as millstatename,"
                                 + "convert(varchar,doc_date,103)as TransDate,CGSTRate,SGSTRate,IGSTRate,state_code_billto,millstatecode,GSTStateCode,mill_code,Unit_Code,System_Name_E,HSN "
                                 + "from NT_1qryEwaybill where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                 + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            }
            #region[from query]

            string distance = clsCommon.getString("select distance from NT_1_deliveryorder where Company_Code=" +
                Convert.ToInt32(Session["Company_Code"].ToString())
                + " AND Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono);

            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();

            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double Other = 0.00;
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
                        drpSupply_Type.SelectedValue = "0";
                        drpSub_Type.SelectedValue = "1";
                        drpTran_Type.SelectedValue = "INV";

                        string doc_no = type + selectedyear + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        Bill_To = dt.Rows[0]["BillToName"].ToString();
                        Ship_To = dt.Rows[0]["ShippTo"].ToString();
                        if (carporateSale == string.Empty)
                        {
                            if (Bill_To != Ship_To)
                            {
                                drpTran_Type.SelectedValue = "4";

                            }
                            else
                            {
                                drpTran_Type.SelectedValue = "3";

                            }
                        }
                        else
                        {
                            drpTran_Type.SelectedValue = "4";
                        }
                        txtBill_From_Name.Text = comnm;
                        txtBill_From_Address.Text = dt.Rows[0]["millname"].ToString();
                        //txtBill_From_Address.Text = address;
                        txtBill_From_Address2.Text = dt.Rows[0]["milladdress"].ToString();
                        txtBill_FromGST_No.Text = gstno;
                        txtBill_From_Place.Text = dt.Rows[0]["millcityname"].ToString();
                        txtBill_From_State.Text = state;
                        txtBill_From_PinCode.Text = dt.Rows[0]["millpincode"].ToString();
                        txtBill_From_State2.Text = dt.Rows[0]["millstatename"].ToString();
                        txtBill_To_Name.Text = dt.Rows[0]["BillToName"].ToString();
                        txtBill_To_Add.Text = dt.Rows[0]["ShippTo"].ToString();
                        txtBill_To_Add2.Text = dt.Rows[0]["Address_E"].ToString();
                        txtBill_TO_GSTNo.Text = dt.Rows[0]["BillToGst"].ToString();
                        txtBill_To_Place.Text = dt.Rows[0]["city_name_e"].ToString();
                        txtBill_To_State.Text = dt.Rows[0]["State_Name"].ToString();
                        txtBill_To_PinCode.Text = dt.Rows[0]["pincode"].ToString();
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";
                        txtTaxable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();


                        //if(CGST!='')
                        //drpCGST_SGST_Rate.SelectedValue = dt.Rows[0]["CGST_SGST_Rate"].ToString();
                        //drpIGST_Rate.SelectedValue = dt.Rows[0]["IGST_Rate"].ToString();
                        //drpCESS_Advol_Rate.SelectedValue = dt.Rows[0]["CESS_Advol_Rate"].ToString();
                        //drpCESS_NonAdvol_Rate.SelectedValue = dt.Rows[0]["CESS_NonAdvol_Rate"].ToString();
                        txtTaxable_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0";
                        txtCESS_non_Advol_Amt.Text = "0.00";
                        txtOther_Amt.Text = "0.00";

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        taxamount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;
                        txtTotal_Bill_Amt.Text = TotalInvoice.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = distance;
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        //Trans_Date = dt.Rows[0]["TransDate"].ToString(); doc_date
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();

                        hdnfCGST_Rate.Value = dt.Rows[0]["CGSTRate"].ToString();
                        hdnfSGST_Rate.Value = dt.Rows[0]["SGSTRate"].ToString();
                        hdnfIGST_Rate.Value = dt.Rows[0]["IGSTRate"].ToString();
                        hdnfState_Code_BillTo.Value = dt.Rows[0]["state_code_billto"].ToString();
                        hdnfMillState_Code.Value = dt.Rows[0]["millstatecode"].ToString();
                        hdnfState_Code.Value = dt.Rows[0]["GSTStateCode"].ToString();
                        hdnfmillCode.Value = dt.Rows[0]["mill_code"].ToString();
                        hdnfUnitCode.Value = dt.Rows[0]["Unit_Code"].ToString();
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
            string qryelement = " select doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(BillToName) as BillToName," +
                                " UPPER(BillToGst) as BillToGst ,UPPER(ShipTo_AcName) as ShippTo,UPPER(Shipto_Address) as Address_E," +
                                " UPPER(ShipTo_City) as city_name_e,(case BillTo_Pincode when 0 then 999999  else BillTo_Pincode end) as pincode," +
                                " upper(BillToStateCode) AS BillToStateCode, BillTo_StateName as State_Name,Qty as NETQNTL,Taxable_Amount as TaxableAmount," +
                                " (convert(varchar,CGST_Rate,0) + '+' + convert(varchar,SGST_Rate,0)+ '+' + convert(varchar,IGST_Rate,0)+'+'+'0'+'+'+'0') as Taxrate," +
                                " CGST_Amount as  CGSTAmount,SGST_Amount as SGSTAmount,IGST_Amount as IGSTAmount,'' as Distance,Vahical_No as LORRYNO,'' as millname,'' as milladdress," +
                                " '99999' as millpincode,''billfromcityname,0 as DONO," +
                                " '' as millstatename,CONVERT(varchar,doc_date,103) as TransDate, CGST_Rate as CGSTRate,SGST_Rate as SGSTRate,IGST_Rate as IGSTRate," +
                                " BillToStateCode as billtostatecode,'' as billfromstatecode,BillToStateCode as GSTStateCode,''mill_code,''Unit_Code,System_Name_E,HSN,TransportId from " +
                                " NT_1_qryNameEwayBillRetail  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());


            #region[from query]

            //string distance = clsCommon.getString("select distance from NT_1_deliveryorder where Company_Code=" +
            //    Convert.ToInt32(Session["Company_Code"].ToString())
            //    + " AND Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono);

            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();
            string statecode = Session["CompanyGSTStateCode"].ToString();
            string distance = "0";
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double Other = 0.00;
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
                        drpSupply_Type.SelectedValue = "0";
                        drpSub_Type.SelectedValue = "1";
                        drpTran_Type.SelectedValue = "INV";

                        string doc_no = type + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        Bill_To = dt.Rows[0]["BillToName"].ToString();
                        Ship_To = dt.Rows[0]["ShippTo"].ToString();

                        
                        if (Bill_To != Ship_To)
                        {
                            drpTran_Type.SelectedValue = "2";

                        }
                        else
                        {
                            drpTran_Type.SelectedValue = "1";

                        }
                        txtBill_From_Name.Text = comnm;
                        txtBill_From_Address.Text = comnm;
                        //txtBill_From_Address.Text = address;
                        txtBill_From_Address2.Text = address;
                        txtBill_FromGST_No.Text = gstno;
                        txtBill_From_Place.Text = city;
                        txtBill_From_State.Text = state;
                        txtBill_From_PinCode.Text = pin;
                        txtBill_From_State2.Text = state;
                        txtBill_To_Name.Text = dt.Rows[0]["BillToName"].ToString();
                        txtBill_To_Add.Text = dt.Rows[0]["ShippTo"].ToString();
                        txtBill_To_Add2.Text = dt.Rows[0]["Address_E"].ToString();
                        txtBill_TO_GSTNo.Text = dt.Rows[0]["BillToGst"].ToString();
                        txtBill_To_Place.Text = dt.Rows[0]["city_name_e"].ToString();
                        txtBill_To_State.Text = dt.Rows[0]["State_Name"].ToString();
                        txtBill_To_PinCode.Text = dt.Rows[0]["pincode"].ToString();
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();

                        double qtl = Convert.ToDouble(dt.Rows[0]["NETQNTL"].ToString());
                        double Fqtl = Math.Round(qtl / 100, 2);
                        txtQty.Text = Fqtl.ToString();
                        txtUnit.Text = "QTL";
                        txtTaxable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();


                        //if(CGST!='')
                        //drpCGST_SGST_Rate.SelectedValue = dt.Rows[0]["CGST_SGST_Rate"].ToString();
                        //drpIGST_Rate.SelectedValue = dt.Rows[0]["IGST_Rate"].ToString();
                        //drpCESS_Advol_Rate.SelectedValue = dt.Rows[0]["CESS_Advol_Rate"].ToString();
                        //drpCESS_NonAdvol_Rate.SelectedValue = dt.Rows[0]["CESS_NonAdvol_Rate"].ToString();
                        txtTaxable_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0";
                        txtCESS_non_Advol_Amt.Text = "0.00";
                        txtOther_Amt.Text = "0.00";

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        taxamount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;
                        txtTotal_Bill_Amt.Text = TotalInvoice.ToString();

                        if (dt.Rows[0]["LORRYNO"].ToString() == string.Empty)
                        {
                            txtTransporter_Name.Text = dt.Rows[0]["BillToName"].ToString();
                            txtTransporter_ID.Text = dt.Rows[0]["BillToGst"].ToString();
                        }
                        else
                        {
                            txtTransporter_Name.Text = "";
                            txtTransporter_ID.Text = dt.Rows[0]["TransportId"].ToString();
                        }
                        txtApproximate_Distance.Text = distance;
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        //Trans_Date = dt.Rows[0]["TransDate"].ToString(); doc_date
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();

                        hdnfCGST_Rate.Value = dt.Rows[0]["CGSTRate"].ToString();
                        hdnfSGST_Rate.Value = dt.Rows[0]["SGSTRate"].ToString();
                        hdnfIGST_Rate.Value = dt.Rows[0]["IGSTRate"].ToString();
                        hdnfState_Code_BillTo.Value = dt.Rows[0]["billtostatecode"].ToString();
                        hdnfMillState_Code.Value = statecode;
                        hdnfState_Code.Value = dt.Rows[0]["GSTStateCode"].ToString();
                        hdnfmillCode.Value = dt.Rows[0]["mill_code"].ToString();
                        hdnfUnitCode.Value = dt.Rows[0]["Unit_Code"].ToString();
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
             string qryelement = " select doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(billtoname) as BillToName,"+
                               " UPPER(billtogstno) as BillToGst ,UPPER(shiptoname) as ShippTo,UPPER(shiptoaddress) as Address_E,"+
                               " UPPER(shiptocityname) as city_name_e,(case billtopincode when 0 then 999999  else billtopincode end) as pincode,"+
                               " upper(billtostatecode) AS BillToStateCode, billtostatename as State_Name,NETQNTL,subtotal as TaxableAmount,"+
                               " (convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,"+
                               " CGSTAmount,SGSTAmount,IGSTAmount,'' as Distance,LORRYNO,UPPER(billfromname) as millname,"+
                               " upper(billfromaddress) as milladdress,(case billfrompincode when 0 then 999999  else billfrompincode end) as millpincode,"+
                               " billfromcityname,DO_NO as DONO,billfromstatename as millstatename,CONVERT(varchar,doc_date,103) as TransDate,CGSTRate,"+
                               " SGSTRate,IGSTRate,billtostatecode,billfromstatecode,billtostatecode as GSTStateCode,mill_code,Unit_Code,System_Name_E,"+
                               " HSN from NT_1_qryEwayBillReturnSale  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_no=" + Dono + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
           
            #region[from query]

          

            string qrynm = Session["Company_Name"].ToString();
            string comnm = qrynm.ToUpper();
            string gstno = Session["Company_GST"].ToString();
            string address = Session["address"].ToString().ToUpper();
            string city = Session["Company_City"].ToString().ToUpper();
            string pin = Session["Company_Pincode"].ToString();
            string state = Session["mystatename"].ToString().ToLower();
            //string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string mobno = Session["Company_Phone"].ToString();
            string distance = "0";
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double Other = 0.00;
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
                        drpSupply_Type.SelectedValue = "0";
                        drpSub_Type.SelectedValue = "1";
                        drpTran_Type.SelectedValue = "INV";

                        string doc_no = type + selectedyear + '-' + dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        Bill_To = dt.Rows[0]["BillToName"].ToString();
                        Ship_To = dt.Rows[0]["ShippTo"].ToString();
                        if (Bill_To != Ship_To)
                        {
                            drpTran_Type.SelectedValue = "4";

                        }
                        else
                        {
                            drpTran_Type.SelectedValue = "3";

                        }
                        txtBill_From_Name.Text = comnm;
                        txtBill_From_Address.Text = dt.Rows[0]["millname"].ToString();
                        //txtBill_From_Address.Text = address;
                        txtBill_From_Address2.Text = dt.Rows[0]["milladdress"].ToString();
                        txtBill_FromGST_No.Text = gstno;
                        txtBill_From_Place.Text = dt.Rows[0]["billfromcityname"].ToString();
                        txtBill_From_State.Text = state;
                        txtBill_From_PinCode.Text = dt.Rows[0]["millpincode"].ToString();
                        txtBill_From_State2.Text = dt.Rows[0]["millstatename"].ToString();
                        txtBill_To_Name.Text = dt.Rows[0]["BillToName"].ToString();
                        txtBill_To_Add.Text = dt.Rows[0]["ShippTo"].ToString();
                        txtBill_To_Add2.Text = dt.Rows[0]["Address_E"].ToString();
                        txtBill_TO_GSTNo.Text = dt.Rows[0]["BillToGst"].ToString();
                        txtBill_To_Place.Text = dt.Rows[0]["city_name_e"].ToString();
                        txtBill_To_State.Text = dt.Rows[0]["State_Name"].ToString();
                        txtBill_To_PinCode.Text = dt.Rows[0]["pincode"].ToString();
                        txtItem_Name.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtItem_Description.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "QTL";
                        txtTaxable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();


                        //if(CGST!='')
                        //drpCGST_SGST_Rate.SelectedValue = dt.Rows[0]["CGST_SGST_Rate"].ToString();
                        //drpIGST_Rate.SelectedValue = dt.Rows[0]["IGST_Rate"].ToString();
                        //drpCESS_Advol_Rate.SelectedValue = dt.Rows[0]["CESS_Advol_Rate"].ToString();
                        //drpCESS_NonAdvol_Rate.SelectedValue = dt.Rows[0]["CESS_NonAdvol_Rate"].ToString();
                        txtTaxable_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0";
                        txtCESS_non_Advol_Amt.Text = "0.00";
                        txtOther_Amt.Text = "0.00";

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        taxamount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;
                        txtTotal_Bill_Amt.Text = TotalInvoice.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = distance;
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        //Trans_Date = dt.Rows[0]["TransDate"].ToString(); doc_date
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();

                        hdnfCGST_Rate.Value = dt.Rows[0]["CGSTRate"].ToString();
                        hdnfSGST_Rate.Value = dt.Rows[0]["SGSTRate"].ToString();
                        hdnfIGST_Rate.Value = dt.Rows[0]["IGSTRate"].ToString();
                        hdnfState_Code_BillTo.Value = dt.Rows[0]["billtostatecode"].ToString();
                        hdnfMillState_Code.Value = dt.Rows[0]["billfromstatecode"].ToString();
                        hdnfState_Code.Value = dt.Rows[0]["GSTStateCode"].ToString();
                        hdnfmillCode.Value = dt.Rows[0]["mill_code"].ToString();
                        hdnfUnitCode.Value = dt.Rows[0]["Unit_Code"].ToString();
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
        DataSet ds = new DataSet();
        qry = "update NT_1_AccountMaster set Pincode=" + txtBill_From_PinCode.Text + " where Ac_Code='" + hdnfmillCode.Value + "' and  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        ds = clsDAL.SimpleQuery(qry);
        qry1 = "update NT_1_AccountMaster set Pincode=" + txtBill_To_PinCode.Text + " where Ac_Code='" + hdnfUnitCode.Value + "' and  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString());
        ds = clsDAL.SimpleQuery(qry1);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('successfully updated...!!');", true);
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
                // pnlPopup.Style["display"] = "none";
                //btnAdddetails.Enabled = false;
                //btnClosedetails.Enabled = false;
                ViewState["currentTable"] = null;
                //btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                drpDoc_Type.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                drpTran_Type.Enabled = false;
                drpSupply_Type.Enabled = false;
                drpSub_Type.Enabled = false;
                txtBill_From_Name.Enabled = false;
                txtBill_From_Address.Enabled = false;
                txtBill_From_Address2.Enabled = false;
                txtBill_FromGST_No.Enabled = false;
                txtBill_From_Place.Enabled = false;
                txtBill_From_State.Enabled = false;
                txtBill_From_State2.Enabled = false;
                txtBill_From_PinCode.Enabled = false;
                txtBill_To_Name.Enabled = false;
                txtBill_To_Add.Enabled = false;
                txtBill_To_Add2.Enabled = false;
                txtBill_TO_GSTNo.Enabled = false;
                txtBill_To_Place.Enabled = false;
                txtBill_To_State.Enabled = false;
                txtBill_To_PinCode.Enabled = false;
                txtItem_Name.Enabled = false;
                txtItem_Description.Enabled = false;
                txtHSN.Enabled = false;
                txtQty.Enabled = false;
                txtUnit.Enabled = false;
                txtTaxable_Value.Enabled = false;
                drpCGST_SGST_Rate.Enabled = false;
                drpIGST_Rate.Enabled = false;
                drpCESS_Advol_Rate.Enabled = false;
                drpCESS_NonAdvol_Rate.Enabled = false;
                txtTaxable_Amt.Enabled = false;
                txtCGST_Amt.Enabled = false;
                txtSGST_Amt.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtCESS_Advol_Amt.Enabled = false;
                txtCESS_non_Advol_Amt.Enabled = false;
                txtOther_Amt.Enabled = false;
                txtTotal_Bill_Amt.Enabled = false;
                txtTransporter_Name.Enabled = false;
                txtTransporter_ID.Enabled = false;
                txtApproximate_Distance.Enabled = false;
                drpTrance_Mode.Enabled = false;
                drpVehicle_Type.Enabled = false;
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
                //btnSave.Text = "Save";
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                //btnAdddetails.Enabled = true;
                //btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                drpDoc_Type.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                drpTran_Type.Enabled = true;
                drpSupply_Type.Enabled = true;
                drpSub_Type.Enabled = true;
                txtBill_From_Name.Enabled = true;
                txtBill_From_Address.Enabled = true;
                txtBill_From_Address2.Enabled = true;
                txtBill_FromGST_No.Enabled = true;
                txtBill_From_Place.Enabled = true;
                txtBill_From_State.Enabled = true;
                txtBill_From_State2.Enabled = true;
                txtBill_From_PinCode.Enabled = true;
                txtBill_To_Name.Enabled = true;
                txtBill_To_Add.Enabled = true;
                txtBill_To_Add2.Enabled = true;
                txtBill_TO_GSTNo.Enabled = true;
                txtBill_To_Place.Enabled = true;
                txtBill_To_State.Enabled = true;
                txtBill_To_PinCode.Enabled = true;
                txtItem_Name.Enabled = true;
                txtItem_Description.Enabled = true;
                txtHSN.Enabled = true;
                txtQty.Enabled = true;
                txtUnit.Enabled = true;
                txtTaxable_Value.Enabled = true;
                drpCGST_SGST_Rate.Enabled = true;
                drpIGST_Rate.Enabled = true;
                drpCESS_Advol_Rate.Enabled = true;
                drpCESS_NonAdvol_Rate.Enabled = true;
                txtTaxable_Amt.Enabled = true;
                txtCGST_Amt.Enabled = true;
                txtSGST_Amt.Enabled = true;
                txtIGST_Amt.Enabled = true;
                txtCESS_Advol_Amt.Enabled = true;
                txtCESS_non_Advol_Amt.Enabled = true;
                txtOther_Amt.Enabled = true;
                txtTotal_Bill_Amt.Enabled = true;
                txtTransporter_Name.Enabled = true;
                txtTransporter_ID.Enabled = true;
                txtApproximate_Distance.Enabled = true;
                drpTrance_Mode.Enabled = true;
                drpVehicle_Type.Enabled = true;
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
                drpDoc_Type.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                drpTran_Type.Enabled = false;
                drpSupply_Type.Enabled = false;
                drpSub_Type.Enabled = false;
                txtBill_From_Name.Enabled = false;
                txtBill_From_Address.Enabled = false;
                txtBill_From_Address2.Enabled = false;
                txtBill_FromGST_No.Enabled = false;
                txtBill_From_Place.Enabled = false;
                txtBill_From_State.Enabled = false;
                txtBill_From_State2.Enabled = false;
                txtBill_From_PinCode.Enabled = false;
                txtBill_To_Name.Enabled = false;
                txtBill_To_Add.Enabled = false;
                txtBill_To_Add2.Enabled = false;
                txtBill_TO_GSTNo.Enabled = false;
                txtBill_To_Place.Enabled = false;
                txtBill_To_State.Enabled = false;
                txtBill_To_PinCode.Enabled = false;
                txtItem_Name.Enabled = false;
                txtItem_Description.Enabled = false;
                txtHSN.Enabled = false;
                txtQty.Enabled = false;
                txtUnit.Enabled = false;
                txtTaxable_Value.Enabled = false;
                drpCGST_SGST_Rate.Enabled = false;
                drpIGST_Rate.Enabled = false;
                drpCESS_Advol_Rate.Enabled = false;
                drpCESS_NonAdvol_Rate.Enabled = false;
                txtTaxable_Amt.Enabled = false;
                txtCGST_Amt.Enabled = false;
                txtSGST_Amt.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtCESS_Advol_Amt.Enabled = false;
                txtCESS_non_Advol_Amt.Enabled = false;
                txtOther_Amt.Enabled = false;
                txtTotal_Bill_Amt.Enabled = false;
                txtTransporter_Name.Enabled = false;
                txtTransporter_ID.Enabled = false;
                txtApproximate_Distance.Enabled = false;
                drpTrance_Mode.Enabled = false;
                drpVehicle_Type.Enabled = false;
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
                drpDoc_Type.Enabled = true;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                drpTran_Type.Enabled = true;
                drpSupply_Type.Enabled = true;
                drpSub_Type.Enabled = true;
                txtBill_From_Name.Enabled = true;
                txtBill_From_Address.Enabled = true;
                txtBill_From_Address2.Enabled = true;
                txtBill_FromGST_No.Enabled = true;
                txtBill_From_Place.Enabled = true;
                txtBill_From_State.Enabled = true;
                txtBill_From_State2.Enabled = true;
                txtBill_From_PinCode.Enabled = true;
                txtBill_To_Name.Enabled = true;
                txtBill_To_Add.Enabled = true;
                txtBill_To_Add2.Enabled = true;
                txtBill_TO_GSTNo.Enabled = true;
                txtBill_To_Place.Enabled = true;
                txtBill_To_State.Enabled = true;
                txtBill_To_PinCode.Enabled = true;
                txtItem_Name.Enabled = true;
                txtItem_Description.Enabled = true;
                txtHSN.Enabled = true;
                txtQty.Enabled = true;
                txtUnit.Enabled = true;
                txtTaxable_Value.Enabled = true;
                drpCGST_SGST_Rate.Enabled = true;
                drpIGST_Rate.Enabled = true;
                drpCESS_Advol_Rate.Enabled = true;
                drpCESS_NonAdvol_Rate.Enabled = true;
                txtTaxable_Amt.Enabled = true;
                txtCGST_Amt.Enabled = true;
                txtSGST_Amt.Enabled = true;
                txtIGST_Amt.Enabled = true;
                txtCESS_Advol_Amt.Enabled = true;
                txtCESS_non_Advol_Amt.Enabled = true;
                txtOther_Amt.Enabled = true;
                txtTotal_Bill_Amt.Enabled = true;
                txtTransporter_Name.Enabled = true;
                txtTransporter_ID.Enabled = true;
                txtApproximate_Distance.Enabled = true;
                drpTrance_Mode.Enabled = true;
                drpVehicle_Type.Enabled = true;
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




    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        setFocusControl(txtDoc_No);
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
        setFocusControl(drpSupply_Type);
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
            // string query = getDisplayQuery();
            // bool recordExist = this.fetchRecord(query);
        }
        else
        {
            // this.showLastRecord();
        }
        string qry = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        if (qry != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            //this.enableDisableNavigateButtons();
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");
            // this.enableDisableNavigateButtons();
            btnEdit.Enabled = false;
            //btnDelete.Enabled = false;
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
    #region [drpDoc_Type_TextChanged]
    protected void drpDoc_Type_SelectedIndexChanged(object sender, EventArgs e)
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
    #region [drpTran_Type_TextChanged]
    protected void drpTran_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpSupply_Type_TextChanged]
    protected void drpSupply_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpSub_Type_TextChanged]
    protected void drpSub_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtBill_From_Name_TextChanged]
    protected void txtBill_From_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_From_Name.Text;
        strTextBox = "txtBill_From_Name";

    }
    #endregion
    #region [txtBill_From_Address_TextChanged]
    protected void txtBill_From_Address_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_From_Address.Text;
        strTextBox = "txtBill_From_Address";

    }
    #endregion
    #region [txtBill_FromGST_No_TextChanged]
    protected void txtBill_FromGST_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_FromGST_No.Text;
        strTextBox = "txtBill_FromGST_No";

    }
    #endregion
    #region [txtBill_From_Place_TextChanged]
    protected void txtBill_From_Place_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_From_Place.Text;
        strTextBox = "txtBill_From_Place";

    }
    #endregion
    #region [txtBill_From_State_TextChanged]
    protected void txtBill_From_State_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_From_State.Text;
        strTextBox = "txtBill_From_State";

    }
    #endregion
    #region [txtBill_From_PinCode_TextChanged]
    protected void txtBill_From_PinCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_From_PinCode.Text;
        strTextBox = "txtBill_From_PinCode";

    }
    #endregion
    #region [txtBill_To_Name_TextChanged]
    protected void txtBill_To_Name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To_Name.Text;
        strTextBox = "txtBill_To_Name";

    }
    #endregion
    #region [txtBill_To_Add_TextChanged]
    protected void txtBill_To_Add_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To_Add.Text;
        strTextBox = "txtBill_To_Add";

    }
    #endregion
    #region [txtBill_TO_GSTNo_TextChanged]
    protected void txtBill_TO_GSTNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_TO_GSTNo.Text;
        strTextBox = "txtBill_TO_GSTNo";

    }
    #endregion
    #region [txtBill_To_Place_TextChanged]
    protected void txtBill_To_Place_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To_Place.Text;
        strTextBox = "txtBill_To_Place";

    }
    #endregion
    #region [txtBill_To_State_TextChanged]
    protected void txtBill_To_State_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To_State.Text;
        strTextBox = "txtBill_To_State";

    }
    #endregion
    #region [txtBill_To_PinCode_TextChanged]
    protected void txtBill_To_PinCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBill_To_PinCode.Text;
        strTextBox = "txtBill_To_PinCode";

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

    }
    #endregion
    #region [txtUnit_TextChanged]
    protected void txtUnit_TextChanged(object sender, EventArgs e)
    {
        searchString = txtUnit.Text;
        strTextBox = "txtUnit";

    }
    #endregion
    #region [txtTaxable_Value_TextChanged]
    protected void txtTaxable_Value_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTaxable_Value.Text;
        strTextBox = "txtTaxable_Value";

    }
    #endregion
    #region [drpCGST_SGST_Rate_TextChanged]
    protected void drpCGST_SGST_Rate_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpIGST_Rate_TextChanged]
    protected void drpIGST_Rate_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpCESS_Advol_Rate_TextChanged]
    protected void drpCESS_Advol_Rate_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpCESS_NonAdvol_Rate_TextChanged]
    protected void drpCESS_NonAdvol_Rate_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    #region [txtCESS_non_Advol_Amt_TextChanged]
    protected void txtCESS_non_Advol_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCESS_non_Advol_Amt.Text;
        strTextBox = "txtCESS_non_Advol_Amt";

    }
    #endregion
    #region [txtOther_Amt_TextChanged]
    protected void txtOther_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOther_Amt.Text;
        strTextBox = "txtOther_Amt";

    }
    #endregion
    #region [txtTotal_Bill_Amt_TextChanged]
    protected void txtTotal_Bill_Amt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTotal_Bill_Amt.Text;
        strTextBox = "txtTotal_Bill_Amt";

    }
    #endregion
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
    #region [txtApproximate_Distance_TextChanged]
    protected void txtApproximate_Distance_TextChanged(object sender, EventArgs e)
    {
        searchString = txtApproximate_Distance.Text;
        strTextBox = "txtApproximate_Distance";

    }
    #endregion
    #region [drpTrance_Mode_TextChanged]
    protected void drpTrance_Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [drpVehicle_Type_TextChanged]
    protected void drpVehicle_Type_SelectedIndexChanged(object sender, EventArgs e)
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
            //pnlPopup.Style["display"] = "block";
            //hdnfClosePopup.Value = "txtDoc_No";
            //btnSearch_Click(sender, e);
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

    public string GetAuthToken(string urlAddress, string username, string password)
    {
        string jsonData = string.Empty;
        string authtoken = string.Empty;
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("gspappid", username);//Client Id
        request.Headers.Add("gspappsecret", password);//Client Secret
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = "{\"gspappid\":\"" + username + "\"," +
              "\"gspappsecret\":\"" + password + "\"}";
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        try
        {
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                jsonData = streamReader.ReadToEnd();
            }
            var jsonObject = JObject.Parse(jsonData);
            jsonData = (string)jsonObject.SelectToken("access_token");
            authtoken = "Bearer " + jsonData;
        }
        catch (Exception ex)
        {
            return jsonData = "Issue occured, " + ex.Message;
        }

        return authtoken;
    }

    public string GenrateEWaybill(string urlAddress1, string username, string password, string gstin, string requestid, string token, string json)
    {
        string jsonData = string.Empty;
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress1);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("username", username);
        request.Headers.Add("password", password);
        request.Headers.Add("gstin", gstin);
        request.Headers.Add("requestid", requestid);
        request.Headers.Add("Authorization", token);


        try
        {
            //Place the serialized content of the object to be posted into the request stream
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {



                string asb = json;
                streamWriter.Write(asb);
                streamWriter.Flush();
                streamWriter.Close();
            }



            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream);
                jsonData = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                var jsonObject = JObject.Parse(jsonData);
            }
        }
        catch (Exception ex)
        {
            return jsonData = "Issue occured, " + ex.Message;
        }
        return jsonData;
    }
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
        if (txtBill_From_PinCode.Text != string.Empty && txtBill_From_PinCode.Text != "999999")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtBill_From_PinCode);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Invalied Mill Pincode!!!!!');", true);
            return;
        }
        if (txtBill_To_PinCode.Text != string.Empty && txtBill_To_PinCode.Text != "999999")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtBill_To_PinCode);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Invalied Bill To Pincode!!!!!');", true);
            return;
        }
        if (txtApproximate_Distance.Text != string.Empty && txtBill_To_PinCode.Text != "0.00")
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtApproximate_Distance);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please Enter Proper Distance!!!!!');", true);
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
        string Supply_Type = drpSupply_Type.SelectedValue;
        string SubType = drpSub_Type.SelectedValue;
        string Doc_Type = drpDoc_Type.SelectedValue;
        string Doc_No = txtDoc_No.Text;
        string Doc_Date = txtDoc_Date.Text;
        string Tran_Type = drpTran_Type.SelectedValue;
        #region replace from_partnm
        from_partnm = txtBill_From_Name.Text;
        from_partnm = from_partnm.Replace("-", "");
        from_partnm = from_partnm.Replace("/", "");
        from_partnm = from_partnm.Replace("&", "AND");
        from_partnm = from_partnm.Replace(".", "");
        from_partnm = from_partnm.Replace("#", "");
        from_partnm = from_partnm.Replace("(", "");
        from_partnm = from_partnm.Replace(")", "");
        from_partnm = from_partnm.Replace(":", "");
        from_partnm = from_partnm.Replace("_", "");
        from_partnm = from_partnm.Replace("@", "");
        from_partnm = from_partnm.Replace(";", "");
        from_partnm = from_partnm.Replace("=", "");
        from_partnm = from_partnm.Replace("\n", "");
        from_partnm = from_partnm.Replace("*", "");
        from_partnm = from_partnm.Replace(",", "");
        string Bill_From_Name = from_partnm;
        #endregion
        #region replace Bill_From_Add

        from_address1 = txtBill_From_Address.Text;
        from_address1 = from_address1.Replace(".", "");
        from_address1 = from_address1.Replace("(", "");
        from_address1 = from_address1.Replace(")", "");
        from_address1 = from_address1.Replace(":", "");
        from_address1 = from_address1.Replace("_", "");
        from_address1 = from_address1.Replace("@", "");
        from_address1 = from_address1.Replace(";", "");
        from_address1 = from_address1.Replace("=", "");
        from_address1 = from_address1.Replace("#", "");
        from_address1 = from_address1.Replace("-", "");
        from_address1 = from_address1.Replace("/", "");
        from_address1 = from_address1.Replace("&", "AND");
        from_address1 = from_address1.Replace("\n", "");
        from_address1 = from_address1.Replace("*", "");
        from_address1 = from_address1.Replace(",", "");
        Bill_From_Add = from_address1;


        from_address = txtBill_From_Address2.Text;
        from_address = from_address.Replace(".", "");
        from_address = from_address.Replace("(", "");
        from_address = from_address.Replace(")", "");
        from_address = from_address.Replace(":", "");
        from_address = from_address.Replace("_", "");
        from_address = from_address.Replace("@", "");
        from_address = from_address.Replace(";", "");
        from_address = from_address.Replace("=", "");
        from_address = from_address.Replace("#", "");
        from_address = from_address.Replace("-", "");
        from_address = from_address.Replace("/", "");
        from_address = from_address.Replace("&", "AND");
        from_address = from_address.Replace("\n", "");
        from_address = from_address.Replace("*", "");
        from_address = from_address.Replace(",", "");
        #endregion

        string Bill_From_Address2 = from_address;


        string Bill_From_GSTIn = "";
        Bill_From_GSTIn = clsCommon.getString("select GST from company where Company_Code="
           + Convert.ToInt32(Session["Company_Code"].ToString()));

        ////string Bill_From_GSTIn = "05AAACG2115R1ZN";
        #region replace from_city
        from_city = txtBill_From_Place.Text;
        from_city = from_city.Replace(".", "");
        from_city = from_city.Replace("(", "");
        from_city = from_city.Replace(")", "");
        from_city = from_city.Replace(":", "");
        from_city = from_city.Replace("_", "");
        from_city = from_city.Replace("@", "");
        from_city = from_city.Replace(";", "");
        from_city = from_city.Replace("=", "");
        from_city = from_city.Replace("-", "");
        from_city = from_city.Replace("&", "AND");
        from_city = from_city.Replace("\n", "");
        from_city = from_city.Replace("/", "");
        from_city = from_city.Replace("*", "");
        from_city = from_city.Replace(",", "");
        string Bill_From_Place = from_city;
        #endregion

        string Bill_From_State = "";
        Bill_From_State = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code="
            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

        //if (txtBill_From_State.Text != string.Empty)
        //{
        //    acname = clsCommon.getString("select ISNULL(State_Code,0) from GSTStateMasterNew where  State_Name=" + txtBill_From_State.Text + "");
        //    Bill_From_State = "29";
        //}
        string Bill_From_State2 = hdnfMillState_Code.Value;
        //if (txtBill_From_State2.Text != string.Empty)
        //{
        //    acname = clsCommon.getString("select ISNULL(State_Code,0) from GSTStateMasterNew where  State_Name=" + txtBill_From_State2.Text + "");
        //    Bill_From_State2 = "29";
        //}
        string Bill_From_PinCode = txtBill_From_PinCode.Text;
        // string Bill_From_PinCode = "560042";
        #region[replae to party name]
        to_ac_name_e = txtBill_To_Name.Text;
        to_ac_name_e = to_ac_name_e.Replace("-", "");
        to_ac_name_e = to_ac_name_e.Replace("/", "");
        to_ac_name_e = to_ac_name_e.Replace("&", "AND");
        to_ac_name_e = to_ac_name_e.Replace(".", "");
        to_ac_name_e = to_ac_name_e.Replace("#", "");
        to_ac_name_e = to_ac_name_e.Replace("(", "");
        to_ac_name_e = to_ac_name_e.Replace(")", "");
        to_ac_name_e = to_ac_name_e.Replace(":", "");
        to_ac_name_e = to_ac_name_e.Replace("_", "");
        to_ac_name_e = to_ac_name_e.Replace("@", "");
        to_ac_name_e = to_ac_name_e.Replace(";", "");
        to_ac_name_e = to_ac_name_e.Replace("=", "");
        to_ac_name_e = to_ac_name_e.Replace("\n", "");
        to_ac_name_e = to_ac_name_e.Replace("*", "");
        to_ac_name_e = to_ac_name_e.Replace(",", "");
        string Bill_To_Name = to_ac_name_e;
        #endregion

        #region[replace to address1]
        to_address1 = txtBill_To_Add.Text;
        to_address1 = to_address1.Replace("-", "");
        to_address1 = to_address1.Replace("/", "");
        to_address1 = to_address1.Replace("&", "AND");
        to_address1 = to_address1.Replace(".", "");
        to_address1 = to_address1.Replace("#", "");
        to_address1 = to_address1.Replace(",", "");
        to_address1 = to_address1.Replace("(", "");
        to_address1 = to_address1.Replace(")", "");
        to_address1 = to_address1.Replace(":", "");
        to_address1 = to_address1.Replace("_", "");
        to_address1 = to_address1.Replace("@", "");
        to_address1 = to_address1.Replace(";", "");
        to_address1 = to_address1.Replace("=", "");
        to_address1 = to_address1.Replace("\n", "");
        to_address1 = to_address1.Replace("*", "");
        to_address1 = to_address1.Replace(",", "");
        string Bill_To_Add = to_address1;
        #endregion
        #region[replace to address2]
        to_address2 = txtBill_To_Add2.Text;
        to_address2 = to_address2.Replace("-", "");
        to_address2 = to_address2.Replace("/", "");
        to_address2 = to_address2.Replace("&", "AND");
        to_address2 = to_address2.Replace(".", "");
        to_address2 = to_address2.Replace("#", "");
        to_address2 = to_address2.Replace(",", "");
        to_address2 = to_address2.Replace("(", "");
        to_address2 = to_address2.Replace(")", "");
        to_address2 = to_address2.Replace(":", "");
        to_address2 = to_address2.Replace("_", "");
        to_address2 = to_address2.Replace("@", "");
        to_address2 = to_address2.Replace(";", "");
        to_address2 = to_address2.Replace("=", "");
        to_address2 = to_address2.Replace("*", "");
        to_address2 = to_address2.Replace("\n", "");
        to_address2 = to_address2.Replace(",", "");
        string Bill_To_Add2 = to_address2;
        #endregion


        string Bill_To_GSTIn = txtBill_TO_GSTNo.Text;
        //// string Bill_To_GSTIn = "05AAACG2140A1ZL";


        #region[replace to place]
        to_place = txtBill_To_Place.Text;

        to_place = to_place.Replace(".", "");

        to_place = to_place.Replace("(", "");
        to_place = to_place.Replace(")", "");
        to_place = to_place.Replace(":", "");
        to_place = to_place.Replace("_", "");
        to_place = to_place.Replace("@", "");
        to_place = to_place.Replace(";", "");
        to_place = to_place.Replace("=", "");
        to_place = to_place.Replace("&", "AND");
        to_place = to_place.Replace("/", "");
        to_place = to_place.Replace("\n", "");
        to_place = to_place.Replace(",", "");
        to_place = to_place.Replace("*", "");
        to_place = to_place.Replace(",", "");
        string Bill_To_Place = to_place;
        #endregion


        string Bill_To_State = hdnfState_Code_BillTo.Value;
        string Bill_To_State2 = hdnfState_Code.Value;
        //if (txtBill_To_State.Text != string.Empty)
        //{
        //    acname = clsCommon.getString("select ISNULL(State_Code,0) from GSTStateMasterNew where  State_Name=" + txtBill_To_State.Text + "");
        //    Bill_To_State = "36";
        //}

        string Bill_To_Pincode = txtBill_To_PinCode.Text;
        //string Bill_To_Pincode = "500003";
        string Product_Nmae = txtItem_Name.Text;
        string Product_Desc = txtItem_Description.Text;
        string HSN = txtHSN.Text;
        double to_Qty;
        to_Qty = Convert.ToDouble(txtQty.Text);
        double Qty = to_Qty;
        string Unit = txtUnit.Text;

        double to_Taxable_Value;
        to_Taxable_Value = Convert.ToDouble(txtTaxable_Value.Text);
        double Taxable_Value = to_Taxable_Value;

        //string CGT_SGT_Rate = "0";
        //string IGST_Rate = "0";
        //string CESS_Advol_Rate = "0";
        //string CESS_NonAdvol_Rate = "0";

        double to_Taxable_Amt;
        to_Taxable_Amt = Convert.ToDouble(txtTaxable_Amt.Text);
        double Taxable_Amt = to_Taxable_Amt;
        // string Taxable_Amt = txtTaxable_Amt.Text;

        double to_CGST_Amt;
        to_CGST_Amt = Convert.ToDouble(txtCGST_Amt.Text);
        double CGST_Amt = to_CGST_Amt;
        //// double CGST_Amt = 0;
        // string CGST_Amt = txtCGST_Amt.Text;

        double to_SCGT_Amt;
        to_SCGT_Amt = Convert.ToDouble(txtSGST_Amt.Text);
        double SCGT_Amt = to_SCGT_Amt;
        ////double SCGT_Amt = 0;
        //string SCGT_Amt = txtSGST_Amt.Text;
        double to_IGST_Amt;
        to_IGST_Amt = Convert.ToDouble(txtIGST_Amt.Text);
        double IGST_Amt = to_IGST_Amt;
        ////double IGST_Amt = 1560;
        //string IGST_Amt = IGST_Amt.Text;
        double to_CESS_Advol_Amt;
        to_CESS_Advol_Amt = Convert.ToDouble(txtCESS_Advol_Amt.Text);
        double CESS_Advol_Amt = to_CESS_Advol_Amt;
        //string CESS_Advol_Amt = txtCESS_Advol_Amt.Text;
        double to_CESS_NonAdvol_Amt;
        to_CESS_NonAdvol_Amt = Convert.ToDouble(txtCESS_non_Advol_Amt.Text);
        double CESS_NonAdvol_Amt = to_CESS_NonAdvol_Amt;
        //string CESS_NonAdvol_Amt = txtCESS_non_Advol_Amt.Text;
        string Other_Amt = txtOther_Amt.Text;
        string Tot_Bill_Amt = txtTotal_Bill_Amt.Text;
        string Trans_DocNo = "";
        string Transport_Name = txtTransporter_Name.Text;
        string Transport_Id = txtTransporter_ID.Text;
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
        string Trans_Datenew = Trans_Date;
        //string CGST_Ratenew = "0";
        //string SGST_Ratenew = "0";
        //string IGST_Ratenew = "5";

        string CGST_Ratenew = hdnfCGST_Rate.Value;
        string SGST_Ratenew = hdnfSGST_Rate.Value;
        string IGST_Ratenew = hdnfIGST_Rate.Value;
        string CessRate = "0";
        string CessAdvol = "0";
        #endregion

        #region Json file
        string json = "{\"supplyType\":\"" + Supply_Type + "\"," +
              "\"subSupplyType\":\"" + SubType + "\"," +
              "\"docType\":\"" + Doc_Type + "\"," +
              "\"docNo\":\"" + Doc_No + "\"," +
              "\"docDate\":\"" + Doc_Date + "\"," +
              "\"fromGstin\":\"" + Bill_From_GSTIn + "\"," +
              "\"fromTrdName\":\"" + Bill_From_Name + "\"," +
              "\"fromAddr1\":\"" + Bill_From_Add + "\"," +
              "\"fromAddr2\":\"" + Bill_From_Address2 + "\"," +
              "\"fromPlace\":\"" + Bill_From_Place + "\"," +
              "\"fromPincode\":" + Bill_From_PinCode + "," +
              "\"actFromStateCode\":" + Bill_From_State2 + "," +
              "\"fromStateCode\":" + Bill_From_State + "," +
              "\"toGstin\":\"" + Bill_To_GSTIn + "\"," +
              "\"toTrdName\":\"" + Bill_To_Name + "\"," +
              "\"toAddr1\":\"" + Bill_To_Add + "\"," +
              "\"toAddr2\":\"" + Bill_To_Add2 + "\"," +
              "\"toPlace\":\"" + Bill_To_Place + "\"," +
              "\"toPincode\":" + Bill_To_Pincode + "," +
               "\"actToStateCode\":" + Bill_To_State2 + "," +
              "\"toStateCode\":" + Bill_To_State + "," +
              "\"totalValue\":" + Taxable_Amt + "," +
              "\"cgstValue\":" + CGST_Amt + "," +
              "\"sgstValue\":" + SCGT_Amt + "," +
              "\"igstValue\":" + IGST_Amt + "," +
              "\"cessValue\":" + CESS_Advol_Amt + "," +
              "\"totInvValue\":" + Tot_Bill_Amt + "," +
              "\"transporterId\":\"" + Transport_Id + "\"," +
              "\"transporterName\":\"" + Transport_Name + "\"," +
              "\"transDocNo\":\"" + Trans_DocNo + "\"," +                     //as balnk value
              "\"transMode\":\"" + Tans_Mode + "\"," +
              "\"transDistance\":\"" + Distance + "\"," +
              "\"transDocDate\":\"" + Trans_Date + "\"," +
              "\"vehicleNo\":\"" + Vehical_No + "\"," +
              "\"vehicleType\":\"" + Vehical_Type + "\"," +
              "\"TransactionType\":\"" + Tran_Type + "\"," +
              "\"itemList\":[{" +
              "\"productName\":\"" + Product_Nmae + "\"," +
              "\"productDesc\":\"" + Product_Desc + "\"," +
              "\"hsnCode\":" + HSN + "," +
              "\"quantity\":" + Qty + "," +
              "\"qtyUnit\":\"" + Unit + "\"," +
              "\"cgstRate\":" + CGST_Ratenew + "," +
              "\"sgstRate\":" + SGST_Ratenew + "," +
              "\"igstRate\":" + IGST_Ratenew + "," +
              "\"cessRate\":" + CessRate + "," +
              "\"cessAdvol\":" + CessAdvol + "," +
              "\"taxableAmount\":" + Taxable_Value + "}]}";
        #endregion

        ////string username = "C1C2ED38DD58491597584CE1199B9ECF";
        ////string password = "5C14DBC6GAD4CG4FC4G9FE3GC2E4B88A8FCA";

        //// string username = "FE39998210EF410AA9279254EF664D07";
        ////string password = "1724285EG420DG476BGAE4FGB9B2B78EABDD";


        ///// string urlAddress = "https://gsp.adaequare.com/gsp/authenticate?grant_type=token";
        ////string urlAddress1 = " https://gsp.adaequare.com/test/enriched/ewb/ewayapi?action=GENEWAYBILL";
        ////string urlAddress1 = " https://gsp.adaequare.com/enriched/ewb/ewayapi?action=GENEWAYBILL";

        //string USERNAME = "navkar789_API_123";
        //string PASSWORD = "navkartraders12";
        //string gstin = "27AABHJ9303C1ZM";

        ////string USERNAME = "05AAACG2115R1ZN";
        ////string PASSWORD = "abc123@@";
        ////string gstin = "05AAACG2115R1ZN";

        // string DDate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyyMMdd");
        string DDate = DateTime.Now.ToString();
        string SBNo = txtDoc_No.Text;
        string requestid = DDate + SBNo + "SS";
        //string requestid = "shubhangi_unique_mkf587";//Auto generate no 





        string username = clsCommon.getString("select E_UserName from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
        string password = clsCommon.getString("select E_UserPassword from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
         string urlAddress = clsCommon.getString("select E_UrlAddress_Token from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
        string urlAddress1 = clsCommon.getString("select E_UrlAddress_Bill from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
        string USERNAME = clsCommon.getString("select E_UserName_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
        string PASSWORD = clsCommon.getString("select E_UserPassword_Gov from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;
        string gstin = clsCommon.getString("select E_Company_GSTno from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())); ;





        //string token = "";
        string token = xmlExecuteDMLQry.GetAuthToken(urlAddress, username, password);
        ////string Ewaybullno = "";
        string Ewaybullno = xmlExecuteDMLQry.GenrateEWaybill(urlAddress1, USERNAME, PASSWORD, gstin, requestid, token, json);

        //string Ewaybullno = "";
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
        Int64 ewaybillno = 0;
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
                    string EwaybillNo = getBetween(str, "ewayBillNo", "ewayBillDate");
                    ewaybillno = Convert.ToInt64(EwaybillNo);
                    string EwaybillDate = getBetween(str, "validUpto", "alert");
                    EwaybillDate = EwaybillDate.Remove(EwaybillDate.Length - 9);
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
                   
                    if (type == "SB")
                    {
                        if (AutoID == string.Empty)
                        {
                            qry = "update NT_1_deliveryorder set EWay_Bill_No=" + ewaybillno + ",EwayBillValidDate='" + ewaybildate + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                             + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_no=" + Dono;
                            ds = clsDAL.SimpleQuery(qry);
                            qry1 = "update NT_1_SugarSale set EWay_Bill_No=" + ewaybillno + ",EwayBillValidDate='" + ewaybildate + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + dosbno;
                            ds = clsDAL.SimpleQuery(qry1);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EwayBill Generated Successfully !');", true);

                            Dono = Request.QueryString["dono"];
                            int Action = 1;
                            string DO = clsCommon.getString("select doid from nt_1_deliveryorder where doc_no='" + Dono
                                + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sss", "javascript:DoOpen('" + DO + "','" + Action + "')", true);
                        }
                        else
                        {
                            qry1 = "update NT_1_SugarSale set Eway_Bill_No=" + ewaybillno + ",EwayBillValidDate='" + ewaybildate + "' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono;
                            ds = clsDAL.SimpleQuery(qry1);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EwayBill Generated Successfully !');", true);

                            int Action = 1;
                            string DO = clsCommon.getString("select saleid from NT_1_SugarSale where doc_no='" + Dono
                              + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nnn", "javascript:SBOpen('" + DO + "','" + Action + "')", true);
                        }
                    }
                    else if (type == "RS")
                    {
                        qry1 = "update nt_1_sugarsalereturn set Eway_Bill_No='" + ewaybillno + "' where  company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono;
                        ds = clsDAL.SimpleQuery(qry1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EInvoice Generated Successfully !');", true);

                        int Action = 1;
                        string DO = clsCommon.getString("select srid from nt_1_sugarsalereturn where doc_no='" + Dono
                          + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ssn", "javascript:RSOpen('" + DO + "','" + Action + "')", true);
                    }
                    else if (type == "RR")
                    {
                        qry1 = "update Retail_Head set EwayBillNo='" + ewaybillno + "' where   company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + Dono;
                        ds = clsDAL.SimpleQuery(qry1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EwayBill Generated Successfully !');", true);

                        Dono = Request.QueryString["dono"];
                        int Action = 1;
                        string DO = clsCommon.getString("select Retailid from Retail_Head where doc_no='" + Dono
                            + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                        string trantype = clsCommon.getString("select CashCredit from Retail_Head where doc_no='" + Dono
                            + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "aa", "javascript:RROpen('" + DO + "','" + Action + "','" + trantype + "')", true);
                    }
                    else if (type == "JS")
                    {
                        qry = "update NT_1_JSaleHead set EWay_Bill_No=" + ewaybillno + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and doc_no=" + Dono;
                        ds = clsDAL.SimpleQuery(qry);

                        ds = clsDAL.SimpleQuery(qry1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('EwayBill Generated Successfully !');", true);
                        Session["JS_No"] = Dono;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "aa", "javascript:JSOpen();", true);
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
    private void BindJawakEwayBill()
    {
        try
        {
            //string qryelement = "select newsbno as doc_no,CONVERT(varchar,doc_date,103) as doc_date,"
            //   + " UPPER(BillToName) as BillToName,UPPER(BillToGst) as BillToGst ,UPPER(Ac_Name_E) as ShippTo,UPPER(Address_E) as Address_E," +
            //   "UPPER(city_name_e) as city_name_e,(case Pincode when 0 then 999999  else pincode end) as pincode,upper(BillToStateCode) AS BillToStateCode," +
            //   " State_Name as State_Name,NETQNTL,TaxableAmount,"
            //   + "(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,CGSTAmount,"
            //   + "SGSTAmount,IGSTAmount,Distance,LORRYNO,UPPER(millname) as millname,upper(milladdress) as milladdress,(case millpincode when 0 then 999999  else millpincode end) as millpincode," +
            //   " millcityname,DO_No,millstatename as millstatename,convert(varchar,TransDate,103)as TransDate,CGSTRate,SGSTRate,IGSTRate,state_code_billto,millstatecode,GSTStateCode,mill_code,Unit_Code "
            //   + " ,DO,DOAc_Name,DOAddress,(case DOPincode when 0 then 999999  else DOPincode end) as DOPincode,DOCity_Name,DOGstState_Code,DOState_Name from NT_1_qryNameEwayBillDoshi where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //   + " and DO_No=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            string qryelement = "select doc_no as doc_no,CONVERT(varchar,doc_date,103) as doc_date, UPPER(Ac_Name_E) as BillToName,UPPER(Gst_No) as BillToGst ," +
                                "UPPER(Ac_Name_E) as ShippTo,UPPER(Address_E) as Address_E,UPPER(city_name_e) as city_name_e," +
                                "(case Pincode when 0 then 999999  else pincode end) as pincode,upper(GSTStateCode) AS BillToStateCode," +
                                "State_Name as State_Name,Net_Wt as NETQNTL,TaxableAmount," +
                                "(convert(varchar,CGST_Rate,0) + '+' + convert(varchar,SGST_Rate,0)+ '+' + convert(varchar,IGST_Rate,0)+'+'+'0'+'+'+'0') as Taxrate," +
                                "CGST_Amount as CGSTAmount,SGST_Amount as SGSTAmount,IGST_Amount as IGSTAmount,Distance,VEHICLE_NO as LORRYNO,UPPER(Ac_Name_E) as millname,upper(Address_E) as milladdress," +
                                "(case Pincode when 0 then 999999  else Pincode end) as millpincode, city_name_e as millcityname,'' as DO_No,'' as millstatename," +
                                "convert(varchar,doc_date,103)as TransDateisnull,isnull(CGST_Rate,0.00) as CGSTRate,isnull(SGST_Rate,0.00) as SGSTRate,isnull(IGST_Rate,0.00) as IGSTRate,GSTStateCode as state_code_billto,GSTStateCode as millstatecode,GSTStateCode,Cust_Code as mill_code " +
                                "from NT_1_qryNameEwayBillJawak where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and doc_no=" + Dono + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            #region[from query]
            string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string comnm = qrynm.ToUpper();
            string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string address1 = clsCommon.getString("select UPPER(Address_R) as Address_R from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string state = clsCommon.getString("select LOWER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            string statecode = Session["CompanyGSTStateCode"].ToString();
            #endregion

            double taxamount;
            string taxvalue;
            double CGST;
            double SGST;
            double IGST;
            double CessAmt = 0.00;
            double CessNontAdvol = 0.00;
            double Other = 0.00;
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
                        drpSupply_Type.SelectedValue = "0";
                        drpSub_Type.SelectedValue = "1";
                        drpTran_Type.SelectedValue = "INV";

                        string doc_no = dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_No.Text = "J" + selectedyear + '-' + doc_no;
                        txtDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
                        //Bill_To = dt.Rows[0]["BillToName"].ToString();
                        //Ship_To = dt.Rows[0]["ShippTo"].ToString();
                        //if (Bill_To != Ship_To)
                        //{
                        //    drpTran_Type.SelectedValue = "4";

                        //}
                        //else
                        //{
                        //    drpTran_Type.SelectedValue = "3";

                        //}
                        drpTran_Type.SelectedValue = "1";
                        txtBill_From_Name.Text = comnm;
                        txtBill_FromGST_No.Text = gstno;
                        txtBill_From_State.Text = state;
                        // string DO = dt.Rows[0]["DO"].ToString();


                        //txtBill_From_Address.Text = dt.Rows[0]["millname"].ToString();
                        //txtBill_From_Address2.Text = dt.Rows[0]["milladdress"].ToString();
                        //txtBill_From_Place.Text = dt.Rows[0]["millcityname"].ToString();
                        //txtBill_From_PinCode.Text = dt.Rows[0]["millpincode"].ToString();
                        //txtBill_From_State2.Text = dt.Rows[0]["millstatename"].ToString();
                        //hdnfMillState_Code.Value = dt.Rows[0]["millstatecode"].ToString();
                        //hdnfmillCode.Value = dt.Rows[0]["mill_code"].ToString();
                        hdnfMillState_Code.Value = statecode;
                        txtBill_From_Address.Text = address;
                        txtBill_From_Address2.Text = address1;
                        txtBill_From_Place.Text = city;
                        txtBill_From_PinCode.Text = pin;
                        txtBill_From_State2.Text = state;


                        txtBill_To_Name.Text = dt.Rows[0]["BillToName"].ToString();
                        txtBill_To_Add.Text = dt.Rows[0]["ShippTo"].ToString();
                        txtBill_To_Add2.Text = dt.Rows[0]["Address_E"].ToString();
                        txtBill_TO_GSTNo.Text = dt.Rows[0]["BillToGst"].ToString();
                        txtBill_To_Place.Text = dt.Rows[0]["city_name_e"].ToString();
                        txtBill_To_State.Text = dt.Rows[0]["State_Name"].ToString();
                        txtBill_To_PinCode.Text = dt.Rows[0]["pincode"].ToString();
                        txtItem_Name.Text = "JAGGERY";
                        txtItem_Description.Text = "JAGGERY";
                        txtHSN.Text = "17011410";
                        txtQty.Text = dt.Rows[0]["NETQNTL"].ToString();
                        txtUnit.Text = "KGS";
                        txtTaxable_Value.Text = dt.Rows[0]["TaxableAmount"].ToString();


                        //if(CGST!='')
                        //drpCGST_SGST_Rate.SelectedValue = dt.Rows[0]["CGST_SGST_Rate"].ToString();
                        //drpIGST_Rate.SelectedValue = dt.Rows[0]["IGST_Rate"].ToString();
                        //drpCESS_Advol_Rate.SelectedValue = dt.Rows[0]["CESS_Advol_Rate"].ToString();
                        //drpCESS_NonAdvol_Rate.SelectedValue = dt.Rows[0]["CESS_NonAdvol_Rate"].ToString();
                        txtTaxable_Amt.Text = dt.Rows[0]["TaxableAmount"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGSTAmount"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGSTAmount"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGSTAmount"].ToString();
                        txtCESS_Advol_Amt.Text = "0";
                        txtCESS_non_Advol_Amt.Text = "0.00";
                        txtOther_Amt.Text = "0.00";

                        CGST = Convert.ToDouble(dt.Rows[0]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[0]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[0]["IGSTAmount"].ToString());
                        taxamount = Convert.ToDouble(dt.Rows[0]["TaxableAmount"].ToString());
                        double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;
                        txtTotal_Bill_Amt.Text = TotalInvoice.ToString();

                        txtTransporter_Name.Text = "";
                        txtTransporter_ID.Text = "";
                        txtApproximate_Distance.Text = dt.Rows[0]["Distance"].ToString();
                        drpTrance_Mode.SelectedValue = "1";
                        drpVehicle_Type.SelectedValue = "R";
                        txtVehicle_No.Text = dt.Rows[0]["LORRYNO"].ToString();
                        //Trans_Date = dt.Rows[0]["TransDate"].ToString(); doc_date
                        Trans_Date = dt.Rows[0]["doc_date"].ToString();

                        hdnfCGST_Rate.Value = dt.Rows[0]["CGSTRate"].ToString();
                        hdnfSGST_Rate.Value = dt.Rows[0]["SGSTRate"].ToString();
                        hdnfIGST_Rate.Value = dt.Rows[0]["IGSTRate"].ToString();
                        hdnfState_Code_BillTo.Value = dt.Rows[0]["state_code_billto"].ToString();
                        hdnfState_Code.Value = dt.Rows[0]["GSTStateCode"].ToString();
                        //hdnfUnitCode.Value = dt.Rows[0]["Unit_Code"].ToString();
                       // txtTransporterDoc_Date.Text = dt.Rows[0]["doc_date"].ToString();
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
}

