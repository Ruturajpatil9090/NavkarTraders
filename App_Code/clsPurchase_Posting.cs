using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// Summary description for clsPurchase_Posting
/// </summary>
public class clsPurchase_Posting
{
    #region Declare
    private static string Head_Insert = string.Empty;
    private static StringBuilder Head_Update = null;
    private static string Head_Delete = string.Empty;
    private static StringBuilder Fields = null;
    private static StringBuilder Head_Values = null;
    private static StringBuilder Detail_Update = null;
    private static StringBuilder Detail_Fields = null;
    private static StringBuilder Detail_Values = null;
    private static string Detail_Insert = string.Empty;
    private static StringBuilder Detail_Delete = null;
    private static int PurchaseDetailId = 0;
    private static string concatid = string.Empty;
    private static string GLEDGER_Insert = string.Empty;
    private static string GLEDGER_Delete = string.Empty;

    #region Purchase Detail Fields
    private static Int32 item_code = 0;
    private static string narration = "";
    private static double Quantal = 0.00;
    private static int packing = 0;
    private static int bags = 0;
    private static double rate = 0.00;
    private static double item_Amount = 0.00;
    private static string i_d = string.Empty;
    #endregion
    #endregion

    public static string strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
    public static SqlConnection _connection = new SqlConnection();
    private static SqlDataReader _reader = null;

    private static SqlDataAdapter _adapter = null;
    private static SqlCommand _sqlCmd = null;
    private static SqlTransaction _transaction = null;
    private static DataTable returndt = null;
    private static DataRow dr = null;

    public static DataTable Purchase_Posting(int flag, PurchaseFields purchase, string Type, DataTable dt)
    {
        returndt = new DataTable();
        dr = null;
        returndt.Columns.Add("Querys", typeof(string));
        dr = returndt.NewRow();
        string Msg = string.Empty;
        Head_Update = null;
        Detail_Update = null;
        Head_Delete = string.Empty;
        Detail_Insert = string.Empty;
        Detail_Delete = null;
        Head_Insert = string.Empty;
        Detail_Insert = string.Empty;
        Detail_Fields = null;
        Detail_Values = null;
        Head_Delete = string.Empty;

        concatid = string.Empty;
        Msg = string.Empty;

        #region Detail Table Column
        Detail_Fields = new StringBuilder();

        Detail_Fields.Append("doc_no,");
        Detail_Fields.Append("detail_id,");
        Detail_Fields.Append("Tran_Type,");
        Detail_Fields.Append("item_code,");
        Detail_Fields.Append("narration,");
        Detail_Fields.Append("Quantal,");
        Detail_Fields.Append("packing,");
        Detail_Fields.Append("bags,");
        Detail_Fields.Append("rate,");
        Detail_Fields.Append("item_Amount,");
        Detail_Fields.Append("Company_Code,");
        Detail_Fields.Append("Year_Code,");

        Detail_Fields.Append("Branch_Code,");
        Detail_Fields.Append("Created_By,");
        Detail_Fields.Append("purchasedetailid,");
        Detail_Fields.Append("purchaseid,");
        Detail_Fields.Append("ic");
        #endregion
        try
        {
            GLEDGER_Delete = "delete from nt_1_gledger where TRAN_TYPE='PS' and Doc_No=" + purchase.PS_doc_no + " and COMPANY_CODE=" + purchase.PS_Company_Code + " and Year_Code=" + purchase.PS_Year_Code + "";
            dr = null;
            dr = returndt.NewRow();
            dr["Querys"] = GLEDGER_Delete;
            returndt.Rows.Add(dr);

            if (flag == 1)
            {
                #region Head Table Column
                Fields = new StringBuilder();

                Fields.Append("doc_no,");
                Fields.Append("Tran_Type,");
                Fields.Append("PURCNO,");
                Fields.Append(" doc_date,");
                Fields.Append(" Ac_Code ,");
                Fields.Append(" Unit_Code,");
                Fields.Append(" mill_code,");
                Fields.Append(" FROM_STATION,");
                Fields.Append(" TO_STATION,");
                Fields.Append(" LORRYNO,");
                Fields.Append("BROKER,");
                Fields.Append(" wearhouse,");
                Fields.Append(" subTotal,");
                Fields.Append("LESS_FRT_RATE,");
                Fields.Append("freight,");
                Fields.Append("cash_advance,");
                Fields.Append("bank_commission,");
                Fields.Append(" OTHER_AMT,");
                Fields.Append("Bill_Amount,");
                Fields.Append("Due_Days,");

                Fields.Append(" NETQNTL,");
                Fields.Append("CGSTRate,");
                Fields.Append("CGSTAmount,");
                Fields.Append("SGSTRate,");
                Fields.Append("SGSTAmount,");
                Fields.Append("IGSTRate,");
                Fields.Append("IGSTAmount,");
                Fields.Append(" GstRateCode,");
                Fields.Append("Company_Code,");
                Fields.Append("Year_Code,");
                Fields.Append(" Branch_Code,");
                Fields.Append(" Created_By,");
                Fields.Append("Bill_No,");
                Fields.Append("EWay_Bill_No,");
                Fields.Append(" purchaseid,");
                Fields.Append(" ac,");
                Fields.Append(" uc,");
                Fields.Append(" mc,");
                Fields.Append("bk,");
                Fields.Append("mill_inv_date,");
                Fields.Append("grade,");
                Fields.Append("Purcid,");
                Fields.Append("SelfBal");
                #endregion


                #region Head Insert Qry
                //Access Common Class Fields And Vallues
                Head_Values = new StringBuilder();

                Head_Values.Append("" + purchase.PS_doc_no + ",'" + purchase.PS_Tran_Type + "'," + purchase.PS_PURCNO + ",'" + purchase.PS_doc_date + "'," + purchase.PS_Ac_Code + "," +
                " " + purchase.PS_Unit_Code + "," + purchase.PS_mill_code + ",'" + purchase.PS_FROM_STATION + "','" + purchase.PS_TO_STATION + "','" + purchase.PS_LORRYNO + "'," +
                " " + purchase.PS_BROKER + ",'" + purchase.PS_wearhouse + "'," + purchase.PS_subTotal + "," + purchase.PS_LESS_FRT_RATE + "," + purchase.PS_freight + ",'" + purchase.PS_cash_advance + "'," +
                " '" + purchase.PS_bank_commission + "','" + purchase.PS_OTHER_AMT + "'," + purchase.PS_Bill_Amount + ",'" + purchase.PS_Due_Days + "','" + purchase.PS_NETQNTL + "'," +
                " '" + purchase.PS_CGSTRate + "','" + purchase.PS_CGSTAmount + "','" + purchase.PS_SGSTRate + "','" + purchase.PS_SGSTAmount + "','" + purchase.PS_IGSTRate + "','" + purchase.PS_IGSTAmount + "'," +
                " '" + purchase.PS_GstRateCode + "','" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "'," +
                " '" + purchase.PS_Branch_Code + "','" + purchase.PS_Created_By + "','" + purchase.PS_Bill_No + "','" + purchase.PS_EWay_Bill_No + "'," + purchase.PS_purchase_Id + ",case when 0=" + purchase.PS_ac + " then null else " + purchase.PS_ac + " end, " +
                " case when 0=" + purchase.PS_uc + " then null else " + purchase.PS_uc + " end, case when 0=" + purchase.PS_mc + " then null else " + purchase.PS_mc + " end, case when 0=" + purchase.PS_bk + " then null else " + purchase.PS_bk + " end,'" + purchase.PS_mill_inv_date + "','" + purchase.PS_Grade + "','" + purchase.PS_Purcid + "','" + purchase.PS_SelfBal + "' ");

                Head_Insert = "insert into nt_1_sugarpurchase(" + Fields + ") values(" + Head_Values + ") ";

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head_Insert;
                returndt.Rows.Add(dr);
                #endregion

                #region Detail Insert Qry
                Detail_Values = new StringBuilder();

                PurchaseDetailId = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(purchasedetailid),0) as purchasedetailid from nt_1_sugarpurchasedetails"));
                if (PurchaseDetailId == 0)
                {
                    PurchaseDetailId = 0;
                }
                else
                {
                    PurchaseDetailId = Convert.ToInt32(clsCommon.getString("select max(purchasedetailid) as purchasedetailid from nt_1_sugarpurchasedetails"));
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PurchaseDetailId = PurchaseDetailId + 1;
                    item_code = Convert.ToInt32(dt.Rows[i]["item_code"].ToString());
                    narration = dt.Rows[i]["narration"].ToString();
                    Quantal = Convert.ToDouble(dt.Rows[i]["Quantal"].ToString());
                    packing = Convert.ToInt32(dt.Rows[i]["packing"].ToString());
                    bags = Convert.ToInt32(dt.Rows[i]["bags"].ToString());
                    double rates = Convert.ToDouble(dt.Rows[i]["rate"].ToString());
                    item_Amount = Convert.ToDouble(dt.Rows[i]["item_Amount"].ToString());
                    i_d = dt.Rows[i]["ID"].ToString();
                    int ic = 0;
                    try
                    {
                        ic = Convert.ToInt32(clsCommon.getString("select isnull(systemid,0) as id from nt_1_systemmaster where Company_Code='" + purchase.PS_Company_Code + "' " +
                            " and Year_Code='" + purchase.PS_Year_Code + "'"));
                    }
                    catch
                    {

                    }
                    Detail_Values.Append("('" + purchase.PS_doc_no + "','" + i_d + "','" + purchase.PS_Tran_Type + "','" + item_code + "','" + narration + "','" + Quantal + "'," +
                        " '" + packing + "','" + bags + "','" + rates + "','" + item_Amount + "','" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "'," +
                        " '" + purchase.PS_Branch_Code + "','" + purchase.PS_Created_By + "','" + PurchaseDetailId + "','" + purchase.PS_purchase_Id + "',case when 0='" + ic + "' then null else '" + ic + "' end),");
                }
                if (Detail_Values.Length > 0)
                {
                    Detail_Values.Remove(Detail_Values.Length - 1, 1);
                    Detail_Insert = "insert into nt_1_sugarpurchasedetails (" + Detail_Fields + ") values " + Detail_Values + "";

                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Insert;
                    returndt.Rows.Add(dr);
                }
                #endregion
                //Msg = PurchaseCrud_Operation(flag);
                //Head_Insert = string.Empty;
                //Detail_Insert = string.Empty;
                //Detail_Fields = string.Empty;
                //Detail_Values = string.Empty;
            }
            else if (flag == 2)
            {
                #region Update Head
                Head_Update = new StringBuilder();
                //Head_Update = Head_Update + "doc_no=";
                //Head_Update = Head_Update + " '" + purchase.PS_doc_no + "',";
                Head_Update.Append("Tran_Type='PS',");

                Head_Update.Append("PURCNO=");
                Head_Update.Append(" '" + purchase.PS_PURCNO + "',");
                Head_Update.Append(" doc_date=");
                Head_Update.Append(" '" + purchase.PS_doc_date + "',");
                Head_Update.Append(" Ac_Code =");
                Head_Update.Append(" '" + purchase.PS_Ac_Code + "',");
                Head_Update.Append(" Unit_Code=");
                Head_Update.Append(" '" + purchase.PS_Unit_Code + "',");
                Head_Update.Append(" mill_code=");
                Head_Update.Append(" '" + purchase.PS_mill_code + "',");
                Head_Update.Append(" FROM_STATION=");
                Head_Update.Append(" '" + purchase.PS_FROM_STATION + "',");
                Head_Update.Append(" TO_STATION=");
                Head_Update.Append(" '" + purchase.PS_TO_STATION + "',");
                Head_Update.Append(" LORRYNO=");
                Head_Update.Append(" '" + purchase.PS_LORRYNO + "',");
                Head_Update.Append("BROKER=");
                Head_Update.Append(" '" + purchase.PS_BROKER + "',");
                Head_Update.Append(" wearhouse=");
                Head_Update.Append(" '" + purchase.PS_wearhouse + "',");
                Head_Update.Append(" subTotal=");
                Head_Update.Append(" '" + purchase.PS_subTotal + "',");
                Head_Update.Append("LESS_FRT_RATE=");
                Head_Update.Append(" '" + purchase.PS_LESS_FRT_RATE + "',");
                Head_Update.Append("freight=");
                Head_Update.Append(" '" + purchase.PS_freight + "',");
                Head_Update.Append("cash_advance=");
                Head_Update.Append(" '" + purchase.PS_cash_advance + "',");
                Head_Update.Append("bank_commission=");
                Head_Update.Append(" '" + purchase.PS_bank_commission + "',");
                Head_Update.Append(" OTHER_AMT=");
                Head_Update.Append(" '" + purchase.PS_OTHER_AMT + "',");
                Head_Update.Append("Bill_Amount=");
                Head_Update.Append(" '" + purchase.PS_Bill_Amount + "',");
                Head_Update.Append("Due_Days=");
                Head_Update.Append(" '" + purchase.PS_Due_Days + "',");
                Head_Update.Append(" NETQNTL=");
                Head_Update.Append(" '" + purchase.PS_NETQNTL + "',");
                Head_Update.Append("CGSTRate=");
                Head_Update.Append(" '" + purchase.PS_CGSTRate + "',");
                Head_Update.Append("CGSTAmount=");
                Head_Update.Append(" '" + purchase.PS_CGSTAmount + "',");
                Head_Update.Append("SGSTRate=");
                Head_Update.Append(" '" + purchase.PS_SGSTRate + "',");
                Head_Update.Append("SGSTAmount=");
                Head_Update.Append(" '" + purchase.PS_SGSTAmount + "',");
                Head_Update.Append("IGSTRate=");
                Head_Update.Append(" '" + purchase.PS_IGSTRate + "',");
                Head_Update.Append("IGSTAmount=");
                Head_Update.Append(" '" + purchase.PS_IGSTAmount + "',");
                Head_Update.Append(" GstRateCode=");
                Head_Update.Append(" '" + purchase.PS_GstRateCode + "',");
                Head_Update.Append(" Modified_By=");
                Head_Update.Append(" '" + purchase.PS_Modified_By + "',");
                Head_Update.Append("Bill_No=");
                Head_Update.Append(" '" + purchase.PS_Bill_No + "',");
                Head_Update.Append("EWay_Bill_No=");
                Head_Update.Append(" '" + purchase.PS_EWay_Bill_No + "',");
                Head_Update.Append("mill_inv_date=");
                Head_Update.Append(" '" + purchase.PS_mill_inv_date + "',");
                Head_Update.Append("grade=");
                Head_Update.Append(" '" + purchase.PS_Grade + "',");
                Head_Update.Append("Purcid=");
                Head_Update.Append(" '" + purchase.PS_Purcid + "',");
                Head_Update.Append(" ac=");
                Head_Update.Append(" case when 0='" + purchase.PS_ac + "' then null else '" + purchase.PS_ac + "' end,");
                Head_Update.Append(" uc=");
                Head_Update.Append("case when 0='" + purchase.PS_uc + "' then null else '" + purchase.PS_uc + "' end,");
                Head_Update.Append(" mc=");
                Head_Update.Append(" case when 0='" + purchase.PS_mc + "' then null else '" + purchase.PS_mc + "' end,");
                Head_Update.Append("bk=");
                Head_Update.Append(" case when 0='" + purchase.PS_bk + "' then null else '" + purchase.PS_bk + "' end");

                string Head = "update nt_1_sugarpurchase set " + Head_Update + " where purchaseid=" + purchase.PS_purchase_Id + "";


                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head;
                returndt.Rows.Add(dr);
                Head = string.Empty;
                #endregion

                PurchaseDetailId = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(purchasedetailid),0) as purchasedetailid from nt_1_sugarpurchasedetails"));
                if (PurchaseDetailId == 0)
                {
                    PurchaseDetailId = 0;
                }
                else
                {
                    PurchaseDetailId = Convert.ToInt32(clsCommon.getString("select max(purchasedetailid) as purchasedetailid from nt_1_sugarpurchasedetails"));
                }
                #region Detail OPerations
                Detail_Values = new StringBuilder();
                Detail_Update = new StringBuilder();
                Detail_Delete = new StringBuilder();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    #region Datatable Values
                    item_code = Convert.ToInt32(dt.Rows[i]["item_code"].ToString());
                    narration = dt.Rows[i]["narration"].ToString();
                    Quantal = Convert.ToDouble(dt.Rows[i]["Quantal"].ToString());
                    packing = Convert.ToInt32(dt.Rows[i]["packing"].ToString());
                    bags = Convert.ToInt32(dt.Rows[i]["bags"].ToString());
                    double rates = Convert.ToDouble(dt.Rows[i]["rate"].ToString());
                    item_Amount = Convert.ToDouble(dt.Rows[i]["item_Amount"].ToString());
                    i_d = dt.Rows[i]["ID"].ToString();
                    int purcID = Convert.ToInt32(dt.Rows[i]["purchasedetailid"].ToString());
                    int ic = 0;
                    try
                    {
                        ic = Convert.ToInt32(clsCommon.getString("select isnull(systemid,0) as id from nt_1_systemmaster where System_Code=" + item_code + "  and Company_Code=" + purchase.PS_Company_Code + " and System_Type='I'"));
                    }
                    catch
                    {

                    }
                    #endregion
                    #region Detail Insert Qry
                    if (dt.Rows[i]["rowAction"].ToString() == "A")
                    {
                        PurchaseDetailId = PurchaseDetailId + 1;
                        Detail_Values.Append("('" + purchase.PS_doc_no + "','" + i_d + "','" + purchase.PS_Tran_Type + "','" + item_code + "','" + narration + "','" + Quantal + "'," +
                            " '" + packing + "','" + bags + "','" + rates + "','" + item_Amount + "','" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "'," +
                            " '" + purchase.PS_Branch_Code + "','" + purchase.PS_Created_By + "','" + PurchaseDetailId + "','" + purchase.PS_purchase_Id + "',case when 0='" + ic + "' then null else '" + ic + "' end),");
                    }
                    #endregion
                    #region Detail Update Qry
                    else if (dt.Rows[i]["rowAction"].ToString() == "U")
                    {
                        Detail_Update.Append("item_code=case purchasedetailid when '" + purcID + "' then '" + item_code + "'  ELSE item_code END,");
                        Detail_Update.Append("narration=case purchasedetailid when '" + purcID + "' then '" + narration + "'  ELSE narration END,");
                        Detail_Update.Append("Quantal=case purchasedetailid when '" + purcID + "' then '" + Quantal + "'  ELSE Quantal END,");
                        Detail_Update.Append("packing=case purchasedetailid when '" + purcID + "' then '" + packing + "'  ELSE packing END,");

                        Detail_Update.Append("bags=case purchasedetailid when '" + purcID + "' then '" + bags + "'  ELSE bags END,");
                        Detail_Update.Append("rate=case purchasedetailid when '" + purcID + "' then '" + rates + "'  ELSE rate END,");
                        Detail_Update.Append("item_Amount=case purchasedetailid when '" + purcID + "' then '" + item_Amount + "'  ELSE item_Amount END,");
                        Detail_Update.Append("ic=case purchasedetailid when '" + purcID + "' then '" + ic + "'  ELSE ic END,");
                        Detail_Update.Append("Modified_By=case purchasedetailid when '" + purcID + "' then '" + purchase.PS_Modified_By + "'  ELSE Modified_By END,");

                        concatid = concatid + "'" + purcID + "',";

                    }
                    #endregion
                    #region Detail Delete Qry
                    if (dt.Rows[i]["rowAction"].ToString() == "D")
                    {
                        Detail_Delete.Append("'" + dt.Rows[i]["purchasedetailid"].ToString() + "',");
                    }
                    #endregion
                }
                #endregion

                #region Create Qry
                if (Detail_Delete.Length > 0)
                {
                    Detail_Delete.Remove(Detail_Delete.Length - 1, 1);
                    string Detail_Deleteqry = "delete from nt_1_sugarpurchasedetails where purchasedetailid in(" + Detail_Delete + ")";

                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Deleteqry;
                    returndt.Rows.Add(dr);
                    Detail_Deleteqry = string.Empty;
                }
                if (Detail_Values.Length > 0)
                {
                    Detail_Values.Remove(Detail_Values.Length - 1, 1);
                    Detail_Insert = "insert into nt_1_sugarpurchasedetails (" + Detail_Fields + ") values " + Detail_Values + "";

                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Insert;
                    returndt.Rows.Add(dr);
                }

                if (Detail_Update.Length > 0)
                {
                    concatid = concatid.Remove(concatid.Length - 1);
                    Detail_Update.Remove(Detail_Update.Length - 1, 1);
                    string Detail_Updateqry = "update nt_1_sugarpurchasedetails set " + Detail_Update + " where purchasedetailid in(" + concatid + ")";

                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Updateqry;
                    returndt.Rows.Add(dr);
                    Detail_Updateqry = string.Empty;
                }
                #endregion
                //Msg = PurchaseCrud_Operation(flag);
                //Head_Update = string.Empty;
                //Detail_Update = string.Empty;
                //Detail_Insert = string.Empty;
                //Detail_Delete = string.Empty;
                //concatid = string.Empty;
                //Detail_Fields = string.Empty;
                //Detail_Values = string.Empty;

            }
            else
            {
                #region
                Head_Delete = "delete from nt_1_sugarpurchase where purchaseid=" + purchase.PS_purchase_Id + "";
                string Detail_Deleteqry = "delete from nt_1_sugarpurchasedetails where purchaseid=" + purchase.PS_purchase_Id + "";
                //GLEDGER_Delete = "delete from nt_1_gledger where TRAN_TYPE='PS' and Doc_No=" + purchase.PS_doc_no + " and COMPANY_CODE=" + purchase.PS_Company_Code + " and Year_Code=" + purchase.PS_Year_Code + "";

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Detail_Deleteqry;
                returndt.Rows.Add(dr);
                Detail_Deleteqry = string.Empty;
                //dr = null;
                //dr = returndt.NewRow();
                //dr["Querys"] = GLEDGER_Delete;
                //returndt.Rows.Add(dr);

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head_Delete;
                returndt.Rows.Add(dr);
                #endregion
            }
            if (flag == 1 || flag == 2)
            {
                #region Gledger Effect
                FormTypes types = new FormTypes();
                StringBuilder Gledger_values = null;
                StringBuilder Gledger_Column = null;

                Gledger_values = new StringBuilder();
                Gledger_Column = new StringBuilder();

                Gledger_Column.Append("TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,UNIT_code,NARRATION,AMOUNT,TENDER_ID,TENDER_ID_DETAIL,VOUCHER_ID,COMPANY_CODE," +
                           " YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD,ADJUSTED_AMOUNT,Branch_Code,SORT_TYPE,SORT_NO,ac,vc,progid,tranid");

                string ItemCode = dt.Rows[0]["item_code"].ToString();
                string Qnl = dt.Rows[0]["Quantal"].ToString();
                string rate = dt.Rows[0]["rate"].ToString();

                string BrokerShortName = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + purchase.PS_BROKER + " and company_code=" + purchase.PS_Company_Code + "");
                string MillShortName = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + purchase.PS_mill_code + " and company_code=" + purchase.PS_Company_Code + "");

                string PurchaseAcc = clsCommon.getString("select Purchase_AC from nt_1_systemmaster where system_type='I' and system_code=" + ItemCode + " and company_code=" + purchase.PS_Company_Code + "");
                string PurchaseCGST = clsCommon.getString("select PurchaseCGSTAc from nt_1_companyparameters where Company_Code=" + purchase.PS_Company_Code + " and Year_Code=" + purchase.PS_Year_Code + "");
                string PurchaseSGST = clsCommon.getString("select PurchaseSGSTAc from nt_1_companyparameters where Company_Code=" + purchase.PS_Company_Code + " and Year_Code=" + purchase.PS_Year_Code + "");
                string PurchaseIGST = clsCommon.getString("select PurchaseIGSTAc from nt_1_companyparameters where Company_Code=" + purchase.PS_Company_Code + " and Year_Code=" + purchase.PS_Year_Code + "");

                string acid = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + purchase.PS_Ac_Code + " and Company_Code=" + purchase.PS_Company_Code + "");
                string millid = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + purchase.PS_mill_code + " and Company_Code=" + purchase.PS_Company_Code + "");
                string itemid = clsCommon.getString("select isnull(accoid,0) as sysid from qrymstaccountmaster where  Ac_Code=" + PurchaseAcc + " and Company_Code=" + purchase.PS_Company_Code + "");
                string PurchaseCGST_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + PurchaseCGST + " and Company_Code=" + purchase.PS_Company_Code + "");
                string PurchaseSGST_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + PurchaseSGST + " and Company_Code=" + purchase.PS_Company_Code + "");
                string PurchaseIGST_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + PurchaseIGST + " and Company_Code=" + purchase.PS_Company_Code + "");

                string CreditNarration = "" + BrokerShortName + "  " + Qnl + "L:" + purchase.PS_LORRYNO + "  R:" + rate + "";
                string DebitNarration = "" + MillShortName + "  " + Qnl + "L:" + purchase.PS_LORRYNO + "  R:" + rate + "";

                int Order_Code = 1;
                // Ac Code And PurchaseAc Effect
                if (purchase.PS_Bill_Amount > 0)
                {
                    Gledger_values.Append("('PS','','" + purchase.PS_doc_no + "','" + purchase.PS_doc_date + "','" + purchase.PS_Ac_Code + "','0','" + CreditNarration + "'," +
                        " '" + purchase.PS_Bill_Amount + "', null,null,null,'" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "','" + Order_Code + "','C',null,0,'1','PS','" + purchase.PS_doc_no + "'," +
                                                         " case when 0='" + acid + "' then null else '" + acid + "' end ,'0','" + types.TT_PB + "','0')");
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('PS','','" + purchase.PS_doc_no + "','" + purchase.PS_doc_date + "','" + PurchaseAcc + "','0','" + DebitNarration + "'," +
                       " '" + purchase.PS_subTotal + "', null,null,null,'" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "','" + Order_Code + "','D',null,0,'1','PS','" + purchase.PS_doc_no + "'," +
                                                        "  case when 0='" + itemid + "' then null else '" + itemid + "' end,'0','" + types.TT_PB + "','0')");
                }
                //end Ac Code And PurchaseAc Effect

                //CGSTAcc Effect
                if (purchase.PS_CGSTAmount > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('PS','','" + purchase.PS_doc_no + "','" + purchase.PS_doc_date + "','" + PurchaseCGST + "','0','" + CreditNarration + "'," +
                      " '" + purchase.PS_CGSTAmount + "', null,null,null,'" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "','" + Order_Code + "','D',null,0,'1','PS','" + purchase.PS_doc_no + "'," +
                                                       " case when 0='" + PurchaseCGST_id + "' then null else '" + PurchaseCGST_id + "' end,'0','" + types.TT_PB + "','0')");
                }
                //end CGSTAcc Effect

                //SGSTAcc Effect
                if (purchase.PS_SGSTAmount > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('PS','','" + purchase.PS_doc_no + "','" + purchase.PS_doc_date + "','" + PurchaseSGST + "','0','" + CreditNarration + "'," +
                      " '" + purchase.PS_SGSTAmount + "', null,null,null,'" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "','" + Order_Code + "','D',null,0,'1','PS','" + purchase.PS_doc_no + "'," +
                                                       " case when 0='" + PurchaseSGST_id + "' then null else '" + PurchaseSGST_id + "' end,'0','" + types.TT_PB + "','0')");
                }
                //end SGSTAcc Effect

                //IGSTAcc Effect
                if (purchase.PS_IGSTAmount > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('PS','','" + purchase.PS_doc_no + "','" + purchase.PS_doc_date + "','" + PurchaseIGST + "','0','" + CreditNarration + "'," +
                      " '" + purchase.PS_IGSTAmount + "', null,null,null,'" + purchase.PS_Company_Code + "','" + purchase.PS_Year_Code + "','" + Order_Code + "','D',null,0,'1','PS','" + purchase.PS_doc_no + "'," +
                                                       " case when 0='" + PurchaseIGST_id + "' then null else '" + PurchaseIGST_id + "' end,'0','" + types.TT_PB + "','0')");
                }
                //end IGSTAcc Effect

                GLEDGER_Insert = "insert into nt_1_gledger (" + Gledger_Column + ") values " + Gledger_values + " ";

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = GLEDGER_Insert;
                returndt.Rows.Add(dr);
                #endregion
            }
            // Msg = clsDAL.DataStore(Head_Insert, Head_Update, Head_Delete, Detail_Insert, Detail_Update, Detail_Delete, GLEDGER_Insert, GLEDGER_Delete, flag);

            Head_Insert = string.Empty;
            Head_Update = null;
            Head_Delete = string.Empty;
            Detail_Insert = string.Empty;
            Detail_Update = null;
            Detail_Delete = null;
            concatid = string.Empty;
            Detail_Fields = null;
            Detail_Values = null;
            Fields = null;
            GLEDGER_Insert = string.Empty;
            GLEDGER_Delete = string.Empty;
            return returndt;

        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return null;
        }
        finally
        {
            //_ds.Dispose();
            // _sqlCmd.Dispose();
            // _adapter.Dispose();
            _connection.Close();
            //_connection.Dispose();
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