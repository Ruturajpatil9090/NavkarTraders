using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LocalVoucher
/// </summary>
public class LocalVoucher
{
    public LocalVoucher()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public String LV_Tran_Type { get; set; }
    public int LV_Doc_No { get; set; }

    public string LV_Suffix { get; set; }
    public int LV_DO_No { get; set; }
    public string LV_Doc_Date { get; set; }
    public int LV_Ac_Code { get; set; }
    public int LV_Unit_Code { get; set; }
    public int LV_Broker_CODE { get; set; }
    public double LV_Quantal { get; set; }
    public int LV_PACKING { get; set; }
    public double LV_BAGS { get; set; }
    public string LV_Grade { get; set; }
    public int LV_Mill_Code { get; set; }
    public double LV_Mill_Rate { get; set; }
    public double LV_Sale_Rate { get; set; }
    public double LV_Purchase_Rate { get; set; }
    public double LV_Commission_Rate { get; set; }
    public double LV_Resale_Commisson { get; set; }
    public double LV_RDiffTender { get; set; }
    public double LV_POSTAGE { get; set; }
    public double LV_BANK_COMMISSION { get; set; }
    public double LV_FREIGHT { get; set; }
    public string LV_Narration1 { get; set; }
    public string LV_Narration2 { get; set; }
    public string LV_Narration3 { get; set; }
    public string LV_Narration4 { get; set; }
    public double LV_OTHER_Expenses { get; set; }
    public double LV_Voucher_Amount { get; set; }
    public double LV_Diff_Amount { get; set; }
    public int LV_Due_Days { get; set; }
    public int LV_Company_Code { get; set; }
    public int LV_Year_Code { get; set; }
    public int LV_Branch_Code { get; set; }
    public string LV_Created_By { get; set; }
    public string LV_Modified_By { get; set; }
    //int LV_Commission_Rate { get; set; }
    public int LV_Transport_Code { get; set; }
    public double LV_CGSTRate { get; set; }
    public double LV_CGSTAmount { get; set; }
    public double LV_SGSTRate { get; set; }
    public double LV_SGSTAmount { get; set; }
    public double LV_IGSTRate { get; set; }
    public double LV_IGSTAmount { get; set; }
    public double LV_TaxableAmount { get; set; }
    public int LV_GstRateCode { get; set; }
    public int LV_Link_No { get; set; }
    public string LV_link_Type { get; set; }
    public int LV_Link_id { get; set; }
    public int LV_commissionid { get; set; }

    public int LV_ac { get; set; }
    public int LV_uc { get; set; }
    public int LV_bc { get; set; }
    public int LV_tc { get; set; }
    public int LV_mc { get; set; }
}