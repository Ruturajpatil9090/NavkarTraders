using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurcSaleDo_CoomonFields
/// </summary>
public class PurchaseFields
{
    public PurchaseFields()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int PS_doc_no { get; set; }

    public string PS_Tran_Type { get; set; }
    public int PS_PURCNO { get; set; }
    public string PS_doc_date { get; set; }
    public int PS_Ac_Code { get; set; }
    public int PS_Unit_Code { get; set; }
    public int PS_mill_code { get; set; }
    public string PS_FROM_STATION { get; set; }
    public string PS_TO_STATION { get; set; }
    public string PS_Grade { get; set; }
    public string PS_LORRYNO { get; set; }
    public int PS_BROKER { get; set; }
    public string PS_wearhouse { get; set; }
    public double PS_subTotal { get; set; }
    public double PS_LESS_FRT_RATE { get; set; }
    public double PS_freight { get; set; }
    public double PS_cash_advance { get; set; }
    public double PS_bank_commission { get; set; }
    public double PS_OTHER_AMT { get; set; }
    public double PS_Bill_Amount { get; set; }
    public int PS_Due_Days { get; set; }
    public string PS_mill_inv_date { get; set; }
    public double PS_NETQNTL { get; set; }
    public double PS_CGSTRate { get; set; }
    public double PS_CGSTAmount { get; set; }
    public double PS_SGSTRate { get; set; }
    public double PS_SGSTAmount { get; set; }
    public double PS_IGSTRate { get; set; }
    public double PS_IGSTAmount { get; set; }
    public int PS_GstRateCode { get; set; }
    public int PS_Company_Code { get; set; }
    public int PS_Year_Code { get; set; }
    public int PS_Branch_Code { get; set; }
    public string PS_Created_By { get; set; }
    public string PS_Modified_By { get; set; }
    public string PS_Bill_No { get; set; }
    public string PS_EWay_Bill_No { get; set; }
    public int PS_purchase_Id { get; set; }
    public int PS_ac { get; set; }
    public int PS_uc { get; set; }
    public int PS_mc { get; set; }
    public int PS_bk { get; set; }
    public int PS_Purcid { get; set; }
    public string PS_SelfBal { get; set; }
}