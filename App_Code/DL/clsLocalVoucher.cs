using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// Summary description for clsLocalVoucher
/// </summary>
public class clsLocalVoucher
{
    #region Declare
    private static string Msg = string.Empty;
    private static StringBuilder Head_Fields = null;
    private static StringBuilder Head_Values = null;
    private static string Head_Insert = string.Empty;
    private static StringBuilder Head_Update = null;
    private static string Head_Delete = string.Empty;
    private static string GLEDGER_Insert = string.Empty;
    private static string GLEDGER_Delete = string.Empty;
    private static string Detail_Insert = string.Empty;
    private static string Detail_Update = string.Empty;
    private static string Detail_Delete = string.Empty;
    private static string msg = string.Empty;
    #endregion

    public static string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
    public static SqlConnection _connection = new SqlConnection();
    private static SqlDataReader _reader = null;

    private static SqlDataAdapter _adapter = null;
    private static SqlCommand _sqlCmd = null;
    private static SqlTransaction _transaction = null;

    private static DataTable returndt = null;
    private static DataRow dr = null;
    public clsLocalVoucher()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable LV_Posting(int flag, LocalVoucher LV, string Type)
    {
        returndt = new DataTable();
        dr = null;
        returndt.Columns.Add("Querys", typeof(string));
        dr = returndt.NewRow();
        Head_Fields =new StringBuilder();
        Head_Insert = string.Empty;
        Head_Update =new StringBuilder();
        Head_Delete = string.Empty;
        Head_Values = new StringBuilder();

        try
        {
            GLEDGER_Delete = "delete from nt_1_gledger where TRAN_TYPE='LV' and Doc_No=" + LV.LV_Doc_No + " and COMPANY_CODE=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "";
            dr = null;
            dr = returndt.NewRow();
            dr["Querys"] = GLEDGER_Delete;
            returndt.Rows.Add(dr);
            if (flag == 1)
            {
                #region Head Column
                Head_Fields.Append("doc_no,");
                Head_Values.Append("" + LV.LV_Doc_No + ",");
                Head_Fields.Append("doc_date,");
                Head_Values.Append("'" + LV.LV_Doc_Date + "',");
                Head_Fields.Append("link_no,");
                Head_Values.Append("'" + LV.LV_Link_No + "',");
                Head_Fields.Append("link_type,");
                Head_Values.Append("'" + LV.LV_link_Type + "',");
                Head_Fields.Append("link_id,");
                Head_Values.Append("'" + LV.LV_Link_id + "',");
                Head_Fields.Append("ac_code,");
                Head_Values.Append("'" + LV.LV_Ac_Code + "',");
                Head_Fields.Append("unit_code,");
                Head_Values.Append("'" + LV.LV_Unit_Code + "',");
                Head_Fields.Append("broker_code,");
                Head_Values.Append("'" + LV.LV_Broker_CODE + "',");
                Head_Fields.Append("qntl,");
                Head_Values.Append("'" + LV.LV_Quantal + "',");
                Head_Fields.Append("packing,");
                Head_Values.Append("'" + LV.LV_PACKING + "',");
                Head_Fields.Append("bags,");
                Head_Values.Append("'" + LV.LV_BAGS + "',");
                Head_Fields.Append("grade,");
                Head_Values.Append("'" + LV.LV_Grade + "',");
                Head_Fields.Append("transport_code,");
                Head_Values.Append("'" + LV.LV_Transport_Code + "',");
                Head_Fields.Append("mill_rate,");
                Head_Values.Append("'" + LV.LV_Mill_Rate + "',");
                Head_Fields.Append("sale_rate,");
                Head_Values.Append("'" + LV.LV_Sale_Rate + "',");
                Head_Fields.Append("purc_rate,");
                Head_Values.Append("'" + LV.LV_Purchase_Rate + "',");
                Head_Fields.Append("commission_amount,");
                Head_Values.Append("" + LV.LV_Diff_Amount + ",");
                Head_Fields.Append("resale_rate,");
                Head_Values.Append("'" + LV.LV_Commission_Rate + "',");
                Head_Fields.Append("resale_commission,");
                Head_Values.Append("'" + LV.LV_Resale_Commisson + "',");
                Head_Fields.Append("misc_amount,");
                Head_Values.Append("'" + LV.LV_OTHER_Expenses + "',");
                Head_Fields.Append("texable_amount,");
                Head_Values.Append("'" + LV.LV_TaxableAmount + "',");
                Head_Fields.Append("gst_code,");
                Head_Values.Append("'" + LV.LV_GstRateCode + "',");
                Head_Fields.Append("cgst_rate,");
                Head_Values.Append("" + LV.LV_CGSTRate + ",");
                Head_Fields.Append("cgst_amount,");
                Head_Values.Append("'" + LV.LV_CGSTAmount + "',");
                Head_Fields.Append("sgst_rate,");
                Head_Values.Append("'" + LV.LV_SGSTRate + "',");
                Head_Fields.Append("sgst_amount,");
                Head_Values.Append("'" + LV.LV_SGSTAmount + "',");
                Head_Fields.Append("igst_rate,");
                Head_Values.Append("'" + LV.LV_IGSTRate + "',");
                Head_Fields.Append("igst_amount,");
                Head_Values.Append("'" + LV.LV_IGSTAmount + "',");

                Head_Fields.Append("company_code,");
                Head_Values.Append("" + LV.LV_Company_Code + ",");
                Head_Fields.Append("Year_Code,");
                Head_Values.Append("'" + LV.LV_Year_Code + "',");
                Head_Fields.Append("Branch_Code,");
                Head_Values.Append("'" + LV.LV_Branch_Code + "',");
                Head_Fields.Append("Created_By,");
                Head_Values.Append("'" + LV.LV_Created_By + "',");
                //Head_Fields .Append( "Modified_By,");
                //Head_Values.Append("'" + LV.LV_Modified_By + "',");
                Head_Fields.Append("commissionid,");
                Head_Values.Append("'" + LV.LV_commissionid + "',");
                Head_Fields.Append("mill_code,");
                Head_Values.Append("'" + LV.LV_Mill_Code + "',");
                Head_Fields.Append("ac,");
                Head_Values.Append("'" + LV.LV_ac + "',");
                Head_Fields.Append("uc,");
                Head_Values.Append("'" + LV.LV_uc + "',");
                Head_Fields.Append("bc,");
                Head_Values.Append("'" + LV.LV_bc + "',");
                Head_Fields.Append("tc,");
                Head_Values.Append("'" + LV.LV_tc + "',");
                Head_Fields.Append("mc,");
                Head_Values.Append("'" + LV.LV_mc + "',");

                Head_Fields.Append("narration1,");
                Head_Values.Append("'" + LV.LV_Narration1 + "',");
                Head_Fields.Append("narration2,");
                Head_Values.Append("'" + LV.LV_Narration2 + "',");
                Head_Fields.Append("narration3,");
                Head_Values.Append("'" + LV.LV_Narration3 + "',");
                Head_Fields.Append("narration4,");
                Head_Values.Append("'" + LV.LV_Narration4 + "',");
                Head_Fields.Append("bill_amount");
                Head_Values.Append("'" + LV.LV_Voucher_Amount + "'");

                #endregion
                Head_Insert = "insert into commission_bill (" + Head_Fields + ") values(" + Head_Values + ")";
                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head_Insert;
                returndt.Rows.Add(dr);

                Head_Fields = null;
                Head_Insert = null;
                Head_Update = null;
            }
            if (flag == 2)
            {
                #region Head Update
                Head_Update.Append("doc_date=");
                Head_Update.Append("'" + LV.LV_Doc_Date + "',");
                Head_Update.Append("ac_code=");
                Head_Update.Append("'" + LV.LV_Ac_Code + "',");
                Head_Update.Append("unit_code=");
                Head_Update.Append("'" + LV.LV_Unit_Code + "',");
                Head_Update.Append("broker_code=");
                Head_Update.Append("'" + LV.LV_Broker_CODE + "',");
                Head_Update.Append("qntl=");
                Head_Update.Append("'" + LV.LV_Quantal + "',");
                Head_Update.Append("packing=");
                Head_Update.Append("'" + LV.LV_PACKING + "',");
                Head_Update.Append("bags=");
                Head_Update.Append("'" + LV.LV_BAGS + "',");
                Head_Update.Append("grade=");
                Head_Update.Append("'" + LV.LV_Grade + "',");
                Head_Update.Append("transport_code=");
                Head_Update.Append("'" + LV.LV_Transport_Code + "',");
                Head_Update.Append("mill_rate=");
                Head_Update.Append("'" + LV.LV_Mill_Rate + "',");
                Head_Update.Append("sale_rate=");
                Head_Update.Append("'" + LV.LV_Sale_Rate + "',");
                Head_Update.Append("purc_rate=");
                Head_Update.Append("'" + LV.LV_Purchase_Rate + "',");
                Head_Update.Append("commission_amount=");
                Head_Update.Append("'" + LV.LV_Diff_Amount + "',");
                Head_Update.Append("resale_rate=");
                Head_Update.Append("'" + LV.LV_Commission_Rate + "',");
                Head_Update.Append("resale_commission=");
                Head_Update.Append("'" + LV.LV_Resale_Commisson + "',");
                Head_Update.Append("misc_amount=");
                Head_Update.Append("'" + LV.LV_OTHER_Expenses + "',");
                Head_Update.Append("texable_amount=");
                Head_Update.Append("'" + LV.LV_TaxableAmount + "',");
                Head_Update.Append("gst_code=");
                Head_Update.Append("'" + LV.LV_GstRateCode + "',");
                Head_Update.Append("cgst_rate=");
                Head_Update.Append("'" + LV.LV_CGSTRate + "',");
                Head_Update.Append("cgst_amount=");
                Head_Update.Append("'" + LV.LV_CGSTAmount + "',");
                Head_Update.Append("sgst_rate=");
                Head_Update.Append("'" + LV.LV_SGSTRate + "',");
                Head_Update.Append("sgst_amount=");
                Head_Update.Append("'" + LV.LV_SGSTAmount + "',");
                Head_Update.Append("igst_rate=");
                Head_Update.Append("'" + LV.LV_IGSTRate + "',");
                Head_Update.Append("igst_amount=");
                Head_Update.Append("'" + LV.LV_IGSTAmount + "',");
                Head_Update.Append("bill_amount=");
                Head_Update.Append("'" + LV.LV_Voucher_Amount + "',");
                Head_Update.Append("Modified_By=");
                Head_Update.Append("'" + LV.LV_Modified_By + "',");
                Head_Update.Append("ac=");
                Head_Update.Append("'" + LV.LV_ac + "',");
                Head_Update.Append("uc=");
                Head_Update.Append("'" + LV.LV_uc + "',");
                Head_Update.Append("bc=");
                Head_Update.Append("'" + LV.LV_bc + "',");
                Head_Update.Append("tc=");
                Head_Update.Append("'" + LV.LV_tc + "',");
                Head_Update.Append("mc=");
                Head_Update.Append("'" + LV.LV_mc + "',");
                Head_Update.Append("mill_code=");
                Head_Update.Append("'" + LV.LV_Mill_Code + "',");

                Head_Update.Append("narration1=");
                Head_Update.Append("'" + LV.LV_uc + "',");
                Head_Update.Append("narration2=");
                Head_Update.Append("'" + LV.LV_bc + "',");
                Head_Update.Append("narration3=");
                Head_Update.Append("'" + LV.LV_tc + "',");
                Head_Update.Append("narration4=");
                Head_Update.Append("'" + LV.LV_mc + "'");

                #endregion
                string Head_Updateqry = "update commission_bill set " + Head_Update + " where commissionid='" + LV.LV_commissionid + "'";
                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head_Updateqry;
                returndt.Rows.Add(dr);
                // Msg = LV_Crud_Operation(flag);
                Head_Fields = null;
                Head_Insert = null;
                Head_Update = null;
            }
            else if (flag == 3)
            {
                Head_Delete = "delete from commission_bill where commissionid=" + LV.LV_commissionid + "";
                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head_Delete;
                returndt.Rows.Add(dr);
                //GLEDGER_Delete = "delete from nt_1_gledger where TRAN_TYPE='LV' and Doc_No=" + LV.LV_Doc_No + " and COMPANY_CODE=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "";
                // msg = clsDAL.DataStore(Head_Insert, Head_Update, Head_Delete, Detail_Insert, Detail_Update, Detail_Delete, GLEDGER_Insert, GLEDGER_Delete, flag);
                GLEDGER_Delete = string.Empty;
                Head_Delete = string.Empty;

            }
            if (flag == 1 || flag == 2)
            {
                #region Gledger Effect
                FormTypes types = new FormTypes();
                StringBuilder Gledger_values = new StringBuilder();
                string drcr = string.Empty;

                int Order_Code = 1;
                StringBuilder Gledger_Column = new StringBuilder();
                Gledger_Column.Append("TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,UNIT_code,NARRATION,AMOUNT,TENDER_ID,TENDER_ID_DETAIL,VOUCHER_ID,COMPANY_CODE," +
                             " YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD,ADJUSTED_AMOUNT,Branch_Code,SORT_TYPE,SORT_NO,ac,vc,progid,tranid");


                string Quality_Diff_Ac = clsCommon.getString("select COMMISSION_AC from NT_1_CompanyParameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "");
                string CGSTAc = clsCommon.getString("select CGSTAc from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "");
                string SGSTAc = clsCommon.getString("select SGSTAc from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "");
                string IGSTAc = clsCommon.getString("select IGSTAc from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Year_Code + "");

                string PurchaseCGST = clsCommon.getString("select PurchaseCGSTAc from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Company_Code + "");
                string PurchaseSGST = clsCommon.getString("select PurchaseSGSTAc from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Company_Code + "");
                string PurchaseIGST = clsCommon.getString("select PurchaseIGSTAc from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Company_Code + "");
                string POSTAGE_AC = clsCommon.getString("select POSTAGE_AC from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Company_Code + "");
                string Resale_comm_Ac = clsCommon.getString("select COMMISSION_AC from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Company_Code + "");
                string OTHER_AMOUNT_AC = clsCommon.getString("select OTHER_AMOUNT_AC from nt_1_companyparameters where Company_Code=" + LV.LV_Company_Code + " and Year_Code=" + LV.LV_Company_Code + "");

                string QualityAC_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + Quality_Diff_Ac + " and Company_Code=" + LV.LV_Company_Code + " ");
                string CGSTAC_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + CGSTAc + " and Company_Code=" + LV.LV_Company_Code + " ");
                string SGSTAC_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + SGSTAc + " and Company_Code=" + LV.LV_Company_Code + " ");
                string IGSTAC_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + IGSTAc + " and Company_Code=" + LV.LV_Company_Code + " ");


                string PurchaseCGST_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + PurchaseCGST + " and Company_Code=" + LV.LV_Company_Code + " ");
                string PurchaseSGST_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + PurchaseSGST + " and Company_Code=" + LV.LV_Company_Code + " ");
                string PurchaseIGST_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + PurchaseIGST + " and Company_Code=" + LV.LV_Company_Code + " ");
                string POSTAGEAc_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + POSTAGE_AC + " and Company_Code=" + LV.LV_Company_Code + " ");
                string ResaleAc_id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + Resale_comm_Ac + " and Company_Code=" + LV.LV_Company_Code + " ");
                string OTHERAmount_Id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + OTHER_AMOUNT_AC + " and Company_Code=" + LV.LV_Company_Code + " ");
                string Ac_id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + LV.LV_Ac_Code + " and Company_Code=" + LV.LV_Company_Code + " ");
                string Transport_id = clsCommon.getString("select isnull(accoid,0) as accoid from qrymstaccountmaster where Ac_Code=" + LV.LV_Transport_Code + " and Company_Code=" + LV.LV_Company_Code + " ");


                // Acc Code Effect
                if (LV.LV_Voucher_Amount > 0)
                {
                    drcr = "D";
                }
                else
                {
                    drcr = "C";
                }
                Gledger_values.Append("('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + LV.LV_Ac_Code + "','0','','" + LV.LV_Voucher_Amount + "', " +
                                                       " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','" + drcr + "','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                       "case when 0=" + Ac_id + " then null else '" + Ac_id + "' end,'0','" + types.TT_CB + "','0')");
                drcr = string.Empty;
                // Quality_Diff_Ac Effect Credit
                if (LV.LV_Voucher_Amount > 0)
                {
                    #region Amt>0
                    Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + Quality_Diff_Ac + "','0','','" + (LV.LV_TaxableAmount - LV.LV_Resale_Commisson) + "', " +
                                                      " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','C','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                      " case when 0=" + QualityAC_Id + " then null else '" + QualityAC_Id + "' end,'0','" + types.TT_CB + "','0')");


                    // CGST Acc Effect
                    if (LV.LV_CGSTAmount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + CGSTAc + "','0','','" + LV.LV_CGSTAmount + "', " +
                                                         " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','C','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                         " case when 0=" + CGSTAC_Id + " then null else '" + CGSTAC_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }

                    //SGST Acc
                    if (LV.LV_SGSTAmount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + SGSTAc + "','0','','" + LV.LV_SGSTAmount + "', " +
                                                         " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','C','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                         " case when 0=" + SGSTAC_Id + " then null else '" + SGSTAC_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }

                    //IGST Acc
                    if (LV.LV_IGSTAmount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + IGSTAc + "','0','','" + LV.LV_IGSTAmount + "', " +
                                                         " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','C','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                         " case when 0=" + IGSTAC_Id + " then null else '" + IGSTAC_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }
                    #endregion

                }
                else
                {
                    #region Amt<0
                    //Quality Diff Acc Debit
                    if (LV.LV_Voucher_Amount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + Quality_Diff_Ac + "','0','','" + (LV.LV_TaxableAmount - LV.LV_Resale_Commisson) + "', " +
                                                          " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','D','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                          " case when 0=" + QualityAC_Id + " then null else '" + QualityAC_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }

                    // CGST Acc Effect
                    if (LV.LV_CGSTAmount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + PurchaseCGST + "','0','','" + LV.LV_CGSTAmount + "', " +
                                                         " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','D','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                         " case when 0=" + PurchaseCGST_Id + " then null else '" + PurchaseCGST_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }

                    //SGST Acc
                    if (LV.LV_SGSTAmount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + PurchaseSGST + "','0','','" + LV.LV_SGSTAmount + "', " +
                                                         " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','D','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                         " case when 0=" + PurchaseSGST_Id + " then null else '" + PurchaseSGST_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }

                    //IGST Acc
                    if (LV.LV_IGSTAmount > 0)
                    {
                        Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + PurchaseIGST + "','0','','" + LV.LV_IGSTAmount + "', " +
                                                         " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','D','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                         " case when 0=" + PurchaseIGST_Id + " then null else '" + PurchaseIGST_Id + "' end,'0','" + types.TT_CB + "','0')");
                    }
                    #endregion
                }

                // Resale Acc Effect
                if (LV.LV_Resale_Commisson != 0)
                {
                    if (LV.LV_Resale_Commisson > 0)
                    {
                        drcr = "C";
                    }
                    else
                    {
                        drcr = "D";
                    }

                    Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + Resale_comm_Ac + "','0','','" + LV.LV_Resale_Commisson + "', " +
                                                          " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','" + drcr + "','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                          " case when 0=" + ResaleAc_id + " then null else '" + ResaleAc_id + "' end,'0','" + types.TT_CB + "','0')");
                    drcr = string.Empty;
                }
                // Transport Acc Effect
                if (LV.LV_FREIGHT != 0)
                {
                    if (LV.LV_FREIGHT > 0)
                    {
                        drcr = "C";
                    }
                    else
                    {
                        drcr = "D";
                    }

                    Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + LV.LV_Transport_Code + "','0','','" + LV.LV_FREIGHT + "', " +
                                                          " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','" + drcr + "','0',0,'" + LV.LV_Branch_Code + "','LV','0'," +
                                                          " case when 0=" + Transport_id + " then null else '" + Transport_id + "' end,'0','" + types.TT_CB + "','0')");
                    drcr = string.Empty;
                }
                // Ac Code Effect

                if (LV.LV_FREIGHT != 0)
                {
                    if (LV.LV_FREIGHT > 0)
                    {
                        drcr = "D";
                    }
                    else
                    {
                        drcr = "C";
                    }

                    Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + LV.LV_Ac_Code + "','0','','" + LV.LV_FREIGHT + "', " +
                                                          " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','" + drcr + "','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                          "case when 0=" + Ac_id + " then null else '" + Ac_id + "' end,'0','" + types.TT_CB + "','0')");
                    drcr = string.Empty;
                }

                // Other Expences
                if (LV.LV_OTHER_Expenses != 0)
                {
                    if (LV.LV_OTHER_Expenses > 0)
                    {
                        drcr = "C";
                    }
                    else
                    {
                        drcr = "D";
                    }

                    Gledger_values.Append(",('LV','','" + LV.LV_Doc_No + "','" + LV.LV_Doc_Date + "','" + OTHER_AMOUNT_AC + "','0','','" + LV.LV_OTHER_Expenses + "', " +
                                                          " null,null,null,'" + LV.LV_Company_Code + "','" + LV.LV_Year_Code + "','" + Order_Code + "','" + drcr + "','0',0,'" + LV.LV_Branch_Code + "','LV','" + LV.LV_Doc_No + "'," +
                                                          " case when 0=" + OTHERAmount_Id + " then null else '" + OTHERAmount_Id + "' end,'0','" + types.TT_CB + "','0')");
                    drcr = string.Empty;


                }
                GLEDGER_Insert = "insert into nt_1_gledger (" + Gledger_Column + ") values " + Gledger_values + " ";
                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = GLEDGER_Insert;
                returndt.Rows.Add(dr);
                #endregion
            }

            // msg = clsDAL.DataStore(Head_Insert, Head_Update, Head_Delete, Detail_Insert, Detail_Update, Detail_Delete, GLEDGER_Insert, GLEDGER_Delete, flag);
            Head_Insert = string.Empty;
            Head_Update = null;
            Head_Delete = string.Empty;
            Detail_Insert = string.Empty;
            Detail_Update = string.Empty;
            Detail_Delete = string.Empty;
            GLEDGER_Insert = string.Empty;
            GLEDGER_Delete = string.Empty;

            return returndt;

        }
        catch
        {
            return null;
        }
    }



    public static bool OpenConnection()
    {
        try
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.ConnectionString = strConnectionString;
                _connection.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            String strException = ex.Message;
            return false;
        }
    }
}