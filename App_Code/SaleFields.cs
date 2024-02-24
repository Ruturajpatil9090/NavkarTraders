using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SaleFields
/// </summary>
public class SaleFields
{
    public SaleFields()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int SB_doc_no { get; set; }

    public string SB_Tran_Type { get; set; }
    public int SB_PURCNO { get; set; }
    public int SB_DONO { get; set; }
    public string SB_doc_date { get; set; }
    public int SB_Ac_Code { get; set; }
    public int SB_Unit_Code { get; set; }
    public int SB_mill_code { get; set; }
    public int SB_TRANSPORT_CODE { get; set; }
    public string SB_FROM_STATION { get; set; }
    public string SB_TO_STATION { get; set; }
    public string SB_LORRYNO { get; set; }
    public int SB_BROKER { get; set; }
    public string SB_wearhouse { get; set; }
    public double SB_subTotal { get; set; }
    public double SB_TAXABLEAMOUNT { get; set; }
    public double SB_LESS_FRT_RATE { get; set; }
    public double SB_freight { get; set; }
    public double SB_cash_advance { get; set; }
    public double SB_bank_commission { get; set; }
    public double SB_OTHER_AMT { get; set; }
    public double SB_Bill_Amount { get; set; }
    public int SB_Due_Days { get; set; }
    public double SB_NETQNTL { get; set; }
    public double SB_RateDiff { get; set; }

    public int SB_Company_Code { get; set; }
    public int SB_Year_Code { get; set; }
    public int SB_Branch_Code { get; set; }
    public string SB_ASN_No { get; set; }
    public string SB_Created_By { get; set; }
    public string SB_Modified_By { get; set; }
    public double SB_CGSTRate { get; set; }
    public double SB_CGSTAmount { get; set; }
    public double SB_SGSTRate { get; set; }
    public double SB_SGSTAmount { get; set; }
    public double SB_IGSTRate { get; set; }
    public double SB_IGSTAmount { get; set; }
    public int SB_GstRateCode { get; set; }
    public string SB_EwayBill_No { get; set; }
    public string SB_MillInvoiceno { get; set; }
    public string SB_EWay_BillChk { get; set; }
    public double SB_Roundoff { get; set; }

    public int SB_Sale_Id { get; set; }
    public int SB_ac { get; set; }
    public int SB_uc { get; set; }
    public int SB_mc { get; set; }
    public int SB_bk { get; set; }
    public int SB_tc { get; set; }
    public int SB_Purcid { get; set; }
    public double SB_FreightPaid_Amount { get; set; }
    public string SB_DoNarrtion { get; set; }


}