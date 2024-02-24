using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// Summary description for clsSale_Posting
/// </summary>
public class clsSale_Posting
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
    private static int SaleDetailId = 0;
    private static string concatid = string.Empty;
    private static string GLEDGER_Insert = string.Empty;
    private static string GLEDGER_Delete = string.Empty;
    private static DataTable returndt = null;
    private static DataRow dr = null;
    #region Sale Detail Fields
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


    public static DataTable Sale_Posting(int flag, SaleFields sale, string type, DataTable dt)
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
        Detail_Values = null;
        concatid = string.Empty;
        Detail_Fields = null;
        Fields = null;
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
        Detail_Fields.Append("saledetailid,");
        Detail_Fields.Append("saleid,");
        Detail_Fields.Append("ic");
        #endregion
        try
        {

            GLEDGER_Delete = "delete from nt_1_gledger where TRAN_TYPE='SB' and Doc_No=" + sale.SB_doc_no + " and COMPANY_CODE=" + sale.SB_Company_Code + " and Year_Code=" + sale.SB_Year_Code + "";
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
                Fields.Append("Company_Code,");
                Fields.Append("Year_Code,");
                Fields.Append(" Branch_Code,");
                Fields.Append(" Created_By,");
                Fields.Append("DO_No,");
                Fields.Append("Transport_Code,");
                Fields.Append("RateDiff,");
                Fields.Append("ASN_No,");
                Fields.Append(" GstRateCode,");
                Fields.Append("CGSTRate,");
                Fields.Append("CGSTAmount,");
                Fields.Append("SGSTRate,");
                Fields.Append("SGSTAmount,");
                Fields.Append("IGSTRate,");
                Fields.Append("IGSTAmount,");
                Fields.Append("TaxableAmount,");
                Fields.Append("EWay_Bill_No,");
                Fields.Append("EWayBill_Chk,");
                Fields.Append("MillInvoiceNo,");
                Fields.Append("RoundOff,");
                Fields.Append(" saleid,");
                Fields.Append(" ac,");
                Fields.Append(" uc,");
                Fields.Append(" mc,");
                Fields.Append("bk,");
                Fields.Append(" tc,");
                Fields.Append(" DoNarrtion,");
                Fields.Append(" Purcid");
                #endregion


                #region Head Insert Qry
                Head_Values = new StringBuilder();

                Head_Values.Append(" " + sale.SB_doc_no + ",'" + sale.SB_Tran_Type + "'," + sale.SB_PURCNO + ",'" + sale.SB_doc_date + "'," + sale.SB_Ac_Code + "," +
                " " + sale.SB_Unit_Code + "," + sale.SB_mill_code + ",'" + sale.SB_FROM_STATION + "','" + sale.SB_TO_STATION + "','" + sale.SB_LORRYNO + "'," +
                " " + sale.SB_BROKER + ",'" + sale.SB_wearhouse + "'," + sale.SB_subTotal + "," + sale.SB_LESS_FRT_RATE + "," + sale.SB_freight + ",'" + sale.SB_cash_advance + "'," +
                " '" + sale.SB_bank_commission + "','" + sale.SB_OTHER_AMT + "'," + sale.SB_Bill_Amount + ",'" + sale.SB_Due_Days + "','" + sale.SB_NETQNTL + "'," +
                " '" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + sale.SB_Branch_Code + "','" + sale.SB_Created_By + "','" + sale.SB_DONO + "','" + sale.SB_TRANSPORT_CODE + "'," +
                "'" + sale.SB_RateDiff + "','" + sale.SB_ASN_No + "','" + sale.SB_GstRateCode + "'," +
                " '" + sale.SB_CGSTRate + "','" + sale.SB_CGSTAmount + "','" + sale.SB_SGSTRate + "','" + sale.SB_SGSTAmount + "','" + sale.SB_IGSTRate + "','" + sale.SB_IGSTAmount + "'," +
                "  '" + sale.SB_TAXABLEAMOUNT + "','" + sale.SB_EwayBill_No + "','" + sale.SB_EWay_BillChk + "','" + sale.SB_MillInvoiceno + "'," + sale.SB_Roundoff + "," + sale.SB_Sale_Id + "," +
                " case when 0=" + sale.SB_ac + " then null else " + sale.SB_ac + " end,case when 0=" + sale.SB_uc + " then null else " + sale.SB_uc + " end, " +
                " case when 0=" + sale.SB_mc + " then null else " + sale.SB_mc + " end, case when 0=" + sale.SB_bk + " then null else " + sale.SB_bk + " end, " +
                " case when 0=" + sale.SB_tc + " then null else " + sale.SB_tc + " end,'" + sale.SB_DoNarrtion + "','" + sale.SB_Purcid + "'");
                //Access Common Class Fields And Vallues
                Head_Insert = "insert into nt_1_sugarsale(" + Fields + ") values(" + Head_Values + ")";

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head_Insert;
                returndt.Rows.Add(dr);
                #endregion

                #region Detail Insert Qry

                Detail_Values = new StringBuilder();

                SaleDetailId = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(saledetailid),0) as saledetailid from nt_1_sugarsaledetails"));
                if (SaleDetailId == 0)
                {
                    SaleDetailId = 0;
                }
                else
                {
                    SaleDetailId = Convert.ToInt32(clsCommon.getString("select max(saledetailid) as saledetailid from nt_1_sugarsaledetails"));
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SaleDetailId = SaleDetailId + 1;
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
                        ic = Convert.ToInt32(clsCommon.getString("select isnull(systemid,0) as id from nt_1_systemmaster where Company_Code='" + sale.SB_Company_Code + "' " +
                            " and Year_Code='" + sale.SB_Year_Code + "'"));
                    }
                    catch
                    {
                    }
                    Detail_Values.Append("('" + sale.SB_doc_no + "','" + i_d + "','" + sale.SB_Tran_Type + "','" + item_code + "','" + narration + "','" + Quantal + "'," +
                        " '" + packing + "','" + bags + "','" + rates + "','" + item_Amount + "','" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "'," +
                        " '" + sale.SB_Branch_Code + "','" + sale.SB_Created_By + "','" + SaleDetailId + "','" + sale.SB_Sale_Id + "',case when 0='" + ic + "' then null else '" + ic + "' end),");
                }
                if (Detail_Values.Length > 0)
                {
                    Detail_Values.Remove(Detail_Values.Length - 1, 1);
                    Detail_Insert = "insert into nt_1_sugarsaledetails (" + Detail_Fields + ") values " + Detail_Values + "";

                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Insert;
                    returndt.Rows.Add(dr);
                }
                #endregion
                //Msg = SaleCrud_Operation(flag);
                //Head_Insert = string.Empty;
                //Detail_Insert = string.Empty;
                //Detail_Values = string.Empty;
                //Detail_Fields = string.Empty;
                //Fields = string.Empty;
            }
            else if (flag == 2)
            {
                #region Update Head

                Head_Update = new StringBuilder();
                //Head_Update = Head_Update + "doc_no=";
                //Head_Update = Head_Update + " '" + sale.SB_doc_no + "',";
                Head_Update.Append("Tran_Type='SB',");

                Head_Update.Append("PURCNO=");
                Head_Update.Append(" '" + sale.SB_PURCNO + "',");
                Head_Update.Append("Purcid=");
                Head_Update.Append(" '" + sale.SB_Purcid + "',");
                Head_Update.Append(" doc_date=");
                Head_Update.Append(" '" + sale.SB_doc_date + "',");
                Head_Update.Append(" Ac_Code =");
                Head_Update.Append(" '" + sale.SB_Ac_Code + "',");
                Head_Update.Append(" Unit_Code=");
                Head_Update.Append(" '" + sale.SB_Unit_Code + "',");
                Head_Update.Append(" mill_code=");
                Head_Update.Append(" '" + sale.SB_mill_code + "',");
                Head_Update.Append(" FROM_STATION=");
                Head_Update.Append(" '" + sale.SB_FROM_STATION + "',");
                Head_Update.Append(" TO_STATION=");
                Head_Update.Append(" '" + sale.SB_TO_STATION + "',");
                Head_Update.Append(" LORRYNO=");
                Head_Update.Append(" '" + sale.SB_LORRYNO + "',");
                Head_Update.Append("BROKER=");
                Head_Update.Append(" '" + sale.SB_BROKER + "',");
                Head_Update.Append(" wearhouse=");
                Head_Update.Append(" '" + sale.SB_wearhouse + "',");
                Head_Update.Append(" subTotal=");
                Head_Update.Append(" '" + sale.SB_subTotal + "',");
                Head_Update.Append("LESS_FRT_RATE=");
                Head_Update.Append(" '" + sale.SB_LESS_FRT_RATE + "',");
                Head_Update.Append("freight=");
                Head_Update.Append(" '" + sale.SB_freight + "',");
                Head_Update.Append("cash_advance=");
                Head_Update.Append(" '" + sale.SB_cash_advance + "',");
                Head_Update.Append("bank_commission=");
                Head_Update.Append(" '" + sale.SB_bank_commission + "',");
                Head_Update.Append(" OTHER_AMT=");
                Head_Update.Append(" '" + sale.SB_OTHER_AMT + "',");
                Head_Update.Append("Bill_Amount=");
                Head_Update.Append(" '" + sale.SB_Bill_Amount + "',");
                Head_Update.Append("Due_Days=");
                Head_Update.Append(" '" + sale.SB_Due_Days + "',");
                Head_Update.Append(" NETQNTL=");
                Head_Update.Append(" '" + sale.SB_NETQNTL + "',");
                Head_Update.Append(" Modified_By=");
                Head_Update.Append(" '" + sale.SB_Modified_By + "',");
                Head_Update.Append("DO_No=");
                Head_Update.Append(" '" + sale.SB_DONO + "',");
                Head_Update.Append("Transport_Code=");
                Head_Update.Append(" '" + sale.SB_TRANSPORT_CODE + "',");
                Head_Update.Append("RateDiff=");
                Head_Update.Append(" '" + sale.SB_RateDiff + "',");
                Head_Update.Append("ASN_No=");
                Head_Update.Append(" '" + sale.SB_ASN_No + "',");
                Head_Update.Append(" GstRateCode=");
                Head_Update.Append(" '" + sale.SB_GstRateCode + "',");
                Head_Update.Append("CGSTRate=");
                Head_Update.Append(" '" + sale.SB_CGSTRate + "',");
                Head_Update.Append("CGSTAmount=");
                Head_Update.Append(" '" + sale.SB_CGSTAmount + "',");
                Head_Update.Append("SGSTRate=");
                Head_Update.Append(" '" + sale.SB_SGSTRate + "',");
                Head_Update.Append("SGSTAmount=");
                Head_Update.Append(" '" + sale.SB_SGSTAmount + "',");
                Head_Update.Append("IGSTRate=");
                Head_Update.Append(" '" + sale.SB_IGSTRate + "',");
                Head_Update.Append("IGSTAmount=");
                Head_Update.Append(" '" + sale.SB_IGSTAmount + "',");
                Head_Update.Append("TaxableAmount=");
                Head_Update.Append(" '" + sale.SB_TAXABLEAMOUNT + "',");
                Head_Update.Append("EWay_Bill_No=");
                Head_Update.Append(" '" + sale.SB_EwayBill_No + "',");
                Head_Update.Append("EWayBill_Chk=");
                Head_Update.Append(" '" + sale.SB_EWay_BillChk + "',");
                Head_Update.Append("MillInvoiceNo=");
                Head_Update.Append(" '" + sale.SB_MillInvoiceno + "',");
                Head_Update.Append("RoundOff=");
                Head_Update.Append(" '" + sale.SB_Roundoff + "',");
                Head_Update.Append(" ac=");
                Head_Update.Append(" case when 0='" + sale.SB_ac + "' then null else '" + sale.SB_ac + "' end,");
                Head_Update.Append(" uc=");
                Head_Update.Append("  case when 0='" + sale.SB_uc + "' then null else '" + sale.SB_uc + "' end,");
                Head_Update.Append(" mc=");
                Head_Update.Append("  case when 0='" + sale.SB_mc + "' then null else '" + sale.SB_mc + "' end,");
                Head_Update.Append("bk=");
                Head_Update.Append(" case when 0='" + sale.SB_bk + "' then null else '" + sale.SB_bk + "' end,");
                Head_Update.Append("tc=");
                Head_Update.Append(" case when 0='" + sale.SB_tc + "' then null else '" + sale.SB_tc + "' end,");
                Head_Update.Append("DoNarrtion=");
                Head_Update.Append(" '" + sale.SB_DoNarrtion + "'");

                string Head = "update nt_1_sugarsale set " + Head_Update + " where saleid=" + sale.SB_Sale_Id + "";

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = Head;
                returndt.Rows.Add(dr);
                Head = string.Empty;
                #endregion

                SaleDetailId = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(saledetailid),0) as saledetailid from nt_1_sugarsaledetails"));
                if (SaleDetailId == 0)
                {
                    SaleDetailId = 0;
                }
                else
                {
                    SaleDetailId = Convert.ToInt32(clsCommon.getString("select max(saledetailid) as saledetailid from nt_1_sugarsaledetails"));
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
                    int saleID = Convert.ToInt32(dt.Rows[i]["saledetailid"].ToString());
                    int ic = 0;
                    try
                    {
                        ic = Convert.ToInt32(clsCommon.getString("select isnull(systemid,0) as id from nt_1_systemmaster where System_Code=" + item_code + "  and Company_Code=" + sale.SB_Company_Code + " and System_Type='I'"));
                    }
                    catch
                    {
                    }
                    #endregion
                    #region Detail Insert Qry
                    if (dt.Rows[i]["rowAction"].ToString() == "A")
                    {
                        SaleDetailId = SaleDetailId + 1;
                        Detail_Values.Append("('" + sale.SB_doc_no + "','" + i_d + "','" + sale.SB_Tran_Type + "','" + item_code + "','" + narration + "','" + Quantal + "'," +
                            " '" + packing + "','" + bags + "','" + rates + "','" + item_Amount + "','" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "'," +
                            " '" + sale.SB_Branch_Code + "','" + sale.SB_Created_By + "','" + SaleDetailId + "','" + sale.SB_Sale_Id + "',case when 0='" + ic + "' then null else '" + ic + "' end),");
                    }
                    #endregion
                    #region Detail Update Qry
                    else if (dt.Rows[i]["rowAction"].ToString() == "U")
                    {
                        Detail_Update.Append("item_code=case saledetailid when '" + saleID + "' then '" + item_code + "'  ELSE item_code END,");
                        Detail_Update.Append("narration=case saledetailid when '" + saleID + "' then '" + narration + "'  ELSE narration END,");
                        Detail_Update.Append("Quantal=case saledetailid when '" + saleID + "' then '" + Quantal + "'  ELSE Quantal END,");
                        Detail_Update.Append("packing=case saledetailid when '" + saleID + "' then '" + packing + "'  ELSE packing END,");

                        Detail_Update.Append("bags=case saledetailid when '" + saleID + "' then '" + bags + "'  ELSE bags END,");
                        Detail_Update.Append("rate=case saledetailid when '" + saleID + "' then '" + rates + "'  ELSE rate END,");
                        Detail_Update.Append("item_Amount=case saledetailid when '" + saleID + "' then '" + item_Amount + "'  ELSE item_Amount END,");
                        Detail_Update.Append("ic=case saledetailid when '" + saleID + "' then '" + ic + "'  ELSE ic END,");
                        Detail_Update.Append("Modified_By=case saledetailid when '" + saleID + "' then '" + sale.SB_Modified_By + "'  ELSE Modified_By END,");

                        concatid = concatid + "'" + saleID + "',";

                    }
                    #endregion
                    #region Detail Delete Qry
                    if (dt.Rows[i]["rowAction"].ToString() == "D")
                    {
                        Detail_Delete.Append("'" + dt.Rows[i]["saledetailid"].ToString() + "',");
                    }
                    #endregion
                }
                #endregion

                #region Create Qry
                if (Detail_Delete.Length > 0)
                {
                    Detail_Delete.Remove(Detail_Delete.Length - 1, 1);
                    string Detail_Deleteqry = "delete from nt_1_sugarsaledetails where saledetailid in(" + Detail_Delete + ")";
                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Deleteqry;
                    returndt.Rows.Add(dr);
                    Detail_Deleteqry = string.Empty;
                }
                if (Detail_Values.Length > 0)
                {
                    Detail_Values.Remove(Detail_Values.Length - 1, 1);
                    Detail_Insert = "insert into nt_1_sugarsaledetails (" + Detail_Fields + ") values " + Detail_Values + "";

                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Insert;
                    returndt.Rows.Add(dr);
                }

                if (Detail_Update.Length > 0)
                {
                    concatid = concatid.Remove(concatid.Length - 1);
                    Detail_Update.Remove(Detail_Update.Length - 1, 1);
                    string Detail_Updateqry = "update nt_1_sugarsaledetails set " + Detail_Update + " where saledetailid in(" + concatid + ")";
                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = Detail_Updateqry;
                    returndt.Rows.Add(dr);
                    Detail_Updateqry = string.Empty;
                }
                #endregion
                //Msg = SaleCrud_Operation(flag);
                //Head_Update = string.Empty;
                //Detail_Update = string.Empty;
                //Detail_Insert = string.Empty;
                //Detail_Delete = string.Empty;
                //Detail_Values = string.Empty;
                //Detail_Fields = string.Empty;
                //concatid = string.Empty;


            }
            else
            {
                Head_Delete = "delete from nt_1_sugarsale where saleid=" + sale.SB_Sale_Id + "";
                string Detail_Deleteqry = "delete from nt_1_sugarsaledetails where saleid=" + sale.SB_Sale_Id + "";



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

                if (sale.SB_DONO != 0)
                {
                    string qry = "update nt_1_deliveryorder set SB_No=null where doid=" + sale.SB_DONO + "";
                    dr = null;
                    dr = returndt.NewRow();
                    dr["Querys"] = qry;
                    returndt.Rows.Add(dr);
                }
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

                string CarporateSaleNo = clsCommon.getString("select Carporate_Sale_No from NT_1_deliveryorder where doc_no=" + sale.SB_DONO + " and tran_type='DO' and Company_Code=" + sale.SB_Company_Code + " " +
                    " and Year_Code=" + sale.SB_Year_Code + "");

                string ItemCode = dt.Rows[0]["item_code"].ToString();
                string Qnl = dt.Rows[0]["Quantal"].ToString();
                string rate = dt.Rows[0]["rate"].ToString();

                string PODetails = clsCommon.getString("select PODETAIL from NT_1_CarporateSale where Doc_No=" + CarporateSaleNo + " Company_Code=" + sale.SB_Company_Code + " " +
                    " and Year_Code=" + sale.SB_Year_Code + " ");
                string Partyshortname = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + sale.SB_Ac_Code + " and  Company_Code=" + sale.SB_Company_Code + "");
                string UnitCity = clsCommon.getString("select cityname from qrymstaccountmaster where Ac_Code=" + sale.SB_Unit_Code + " and  Company_Code=" + sale.SB_Company_Code + "");
                string TransportShort = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + sale.SB_TRANSPORT_CODE + " and Company_Code=" + sale.SB_Company_Code + "");
                string millShortname = clsCommon.getString("select Short_Name from qrymstaccountmaster where Ac_Code=" + sale.SB_mill_code + " and Company_Code=" + sale.SB_Company_Code + "");

                string SaleAcc = clsCommon.getString("select Sale_AC from nt_1_systemmaster where system_type='I' and system_code=" + ItemCode + " and company_code=" + sale.SB_Company_Code + "");
                string SaleCGST = clsCommon.getString("select CGSTAc from nt_1_companyparameters where Company_Code=" + sale.SB_Company_Code + " and Year_Code=" + sale.SB_Year_Code + "");
                string SaleSGST = clsCommon.getString("select SGSTAc from nt_1_companyparameters where Company_Code=" + sale.SB_Company_Code + " and Year_Code=" + sale.SB_Year_Code + "");
                string SaleIGST = clsCommon.getString("select IGSTAc from nt_1_companyparameters where Company_Code=" + sale.SB_Company_Code + " and Year_Code=" + sale.SB_Year_Code + "");

                string Acid = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + sale.SB_Ac_Code + " and Company_Code=" + sale.SB_Company_Code + "");
                string unitid = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + sale.SB_Unit_Code + " and Company_Code=" + sale.SB_Company_Code + "");
                string transportid = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + sale.SB_TRANSPORT_CODE + " and Company_Code=" + sale.SB_Company_Code + "");
                string millid = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + sale.SB_mill_code + " and Company_Code=" + sale.SB_Company_Code + "");
                string itemid = clsCommon.getString("select isnull(accoid,0) as sysid from qrymstaccountmaster where  Ac_Code=" + SaleAcc + " and Company_Code=" + sale.SB_Company_Code + "");
                string SaleCGST_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + SaleCGST + " and Company_Code=" + sale.SB_Company_Code + "");
                string SaleSGST_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + SaleSGST + " and Company_Code=" + sale.SB_Company_Code + "");
                string SaleIGST_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + SaleIGST + " and Company_Code=" + sale.SB_Company_Code + "");

                int Order_Code = 1;
                double saleamount_tender = (Convert.ToDouble(rate) * 5) / 100;
                double finalsaleamonut = Convert.ToDouble(rate) + saleamount_tender;
                double myAmt = sale.SB_Bill_Amount - sale.SB_cash_advance;
                double gstAmt = sale.SB_CGSTAmount + sale.SB_SGSTAmount + sale.SB_IGSTAmount;
                string CreditNarration = string.Empty;
                string DebitNarration = string.Empty;
                string roundoffac = clsCommon.getString("select RoundOff from nt_1_companyparameters where Company_Code=" + sale.SB_Company_Code +
                    " and Year_Code=" + sale.SB_Year_Code + "");
                string roundoff_id = clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where  Ac_Code=" + roundoffac + " and Company_Code=" + sale.SB_Company_Code + "");


                if (sale.SB_Ac_Code == sale.SB_Unit_Code)
                {
                    CreditNarration = "" + millShortname + "  " + sale.SB_NETQNTL + "  L:" + sale.SB_LORRYNO + "  PB:" + sale.SB_PURCNO + "  R:" + finalsaleamonut + "  F:" + sale.SB_LESS_FRT_RATE + "";
                    DebitNarration = "" + millShortname + "  " + sale.SB_NETQNTL + "  L:" + sale.SB_LORRYNO + "  PB:" + sale.SB_PURCNO + "  R:" + finalsaleamonut + "  F:" + sale.SB_LESS_FRT_RATE + "";
                }
                else
                {
                    CreditNarration = "" + millShortname + "  " + sale.SB_NETQNTL + "  L:" + sale.SB_LORRYNO + "  PB:" + sale.SB_PURCNO + "  R:" + finalsaleamonut + "  F:" + sale.SB_LESS_FRT_RATE + " ShipToName::" + UnitCity + "";
                    DebitNarration = "" + millShortname + "  " + sale.SB_NETQNTL + "  L:" + sale.SB_LORRYNO + "  PB:" + sale.SB_PURCNO + "  R:" + finalsaleamonut + "  F:" + sale.SB_LESS_FRT_RATE + "  ShipToName::" + UnitCity + "";
                }

                string vasualiamount = clsCommon.getString("select vasuli_rate1 from NT_1_deliveryorder where doc_no=" + sale.SB_DONO + " and tran_type='DO' and Company_Code=" + sale.SB_Company_Code + " " +
                    " and Year_Code=" + sale.SB_Year_Code + "");
                string memoamount = clsCommon.getString("select MM_Rate from NT_1_deliveryorder where doc_no=" + sale.SB_DONO + " and tran_type='DO' and Company_Code=" + sale.SB_Company_Code + " " +
                    " and Year_Code=" + sale.SB_Year_Code + "");
                double saleacsaleamt = Convert.ToDouble(rate) + Convert.ToDouble(vasualiamount);
                string saleaccountnarration = "" + millShortname + ", SB:" + Partyshortname + ", Qntl:" + sale.SB_NETQNTL +
                    ",L:" + sale.SB_LORRYNO + ", R:" + saleacsaleamt + ", MM:" + memoamount;
                string TransportNarration = "" + sale.SB_NETQNTL + "  " + sale.SB_cash_advance + "  " + millShortname + "  " + TransportShort + "  Lorry:" + sale.SB_LORRYNO + "  Party:" + Partyshortname + "";

                // Ac Code Effect
                if (sale.SB_Bill_Amount > 0)
                {

                    Gledger_values.Append("('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + sale.SB_Ac_Code + "','0','" + DebitNarration + "'," +
                        " '" + sale.SB_Bill_Amount + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','D',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                         " case when 0='" + Acid + "' then null else '" + Acid + "' end,'0','" + types.TT_SB + "','0')");
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + SaleAcc + "','0','" + saleaccountnarration + "'," +
                        " '" + sale.SB_TAXABLEAMOUNT + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                         "  case when 0='" + itemid + "' then null else '" + itemid + "' end,'0','" + types.TT_SB + "','0')");
                }
                //end Ac Code

                //CGSTAcc Effect
                if (sale.SB_CGSTAmount > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + SaleCGST + "','0','" + CreditNarration + "'," +
                      " '" + sale.SB_CGSTAmount + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                       " case when 0='" + SaleCGST_id + "' then null else '" + SaleCGST_id + "' end,'0','" + types.TT_SB + "','0')");
                }
                //end CGSTAcc Effect

                //SGSTAcc Effect
                if (sale.SB_SGSTAmount > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + SaleSGST + "','0','" + CreditNarration + "'," +
                      " '" + sale.SB_SGSTAmount + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                       " case when 0='" + SaleSGST_id + "' then null else '" + SaleSGST_id + "' end,'0','" + types.TT_SB + "','0')");
                }
                //end SGSTAcc Effect

                //IGSTAcc Effect
                if (sale.SB_IGSTAmount > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + SaleIGST + "','0','" + CreditNarration + "'," +
                      " '" + sale.SB_IGSTAmount + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                       " case when 0='" + SaleIGST_id + "' then null else '" + SaleIGST_id + "' end,'0','" + types.TT_SB + "','0')");
                }
                //end IGSTAcc Effect

                //Transport Advance
                if (sale.SB_cash_advance > 0)
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + sale.SB_TRANSPORT_CODE + "','0','" + TransportNarration + "'," +
                      " '" + sale.SB_cash_advance + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                       " case when 0='" + transportid + "' then null else '" + transportid + "' end,'0','" + types.TT_SB + "','0')");
                }
                double roundoff = sale.SB_Roundoff;
                if (roundoff < 0)
                {

                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + roundoffac + "','0','" + DebitNarration + "'," +
                      " '" + Math.Abs(roundoff) + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','D',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                       " case when 0='" + roundoff_id + "' then null else '" + roundoff_id + "' end,'0','" + types.TT_SB + "','0')");

                }
                else
                {
                    Order_Code = Order_Code + 1;
                    Gledger_values.Append(",('SB','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + roundoffac + "','0','" + CreditNarration + "'," +
                      " '" + Math.Abs(roundoff) + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + sale.SB_doc_no + "'," +
                                                       " case when 0='" + roundoff_id + "' then null else '" + roundoff_id + "' end,'0','" + types.TT_SB + "','0')");

                }

                // double mm_amount = sale.SB_freight;
                //if (mm_amount != 0)
                //{
                //    Order_Code = Order_Code + 1;
                //    Gledger_values.Append(",('DO','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + sale.SB_TRANSPORT_CODE + "','0','" + TransportNarration + "'," +
                //      " '" + mm_amount + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','DO','" + sale.SB_doc_no + "'," +
                //                                       " case when 0='" + transportid + "' then null else '" + transportid + "' end,'0','" + types.TT_SB + "','0')");

                //    Order_Code = Order_Code + 1;
                //    Gledger_values.Append(",('DO','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + freightac + "','0','" + DebitNarration + "'," +
                //      " '" + mm_amount + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','D',null,0,'1','DO','" + sale.SB_doc_no + "'," +
                //                                       " case when 0='" + freightac_id + "' then null else '" + freightac_id + "' end,'0','" + types.TT_SB + "','0')");


                //}

                // double FRIEGHTAMOUNT = sale.SB_FreightPaid_Amount;
                //if (FRIEGHTAMOUNT != 0)
                //{
                //    Order_Code = Order_Code + 1;
                //    Gledger_values.Append(",('DO','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','" + sale.SB_TRANSPORT_CODE + "','0','" + TransportNarration + "'," +
                //      " '" + FRIEGHTAMOUNT + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','C',null,0,'1','DO','" + sale.SB_doc_no + "'," +
                //                                       " case when 0='" + transportid + "' then null else '" + transportid + "' end,'0','" + types.TT_SB + "','0')");

                //    Order_Code = Order_Code + 1;
                //    Gledger_values.Append(",('DO','','" + sale.SB_doc_no + "','" + sale.SB_doc_date + "','1','0','" + DebitNarration + "'," +
                //      " '" + FRIEGHTAMOUNT + "', null,null,null,'" + sale.SB_Company_Code + "','" + sale.SB_Year_Code + "','" + Order_Code + "','D',null,0,'1','DO','" + sale.SB_doc_no + "'," +
                //                                       " case when 0='" + CASHID + "' then null else '" + freightac_id + "' end,'0','" + types.TT_SB + "','0')");

                //}


                GLEDGER_Insert = "insert into nt_1_gledger (" + Gledger_Column + ") values " + Gledger_values + " ";

                dr = null;
                dr = returndt.NewRow();
                dr["Querys"] = GLEDGER_Insert;
                returndt.Rows.Add(dr);
                #endregion
            }
            //Msg = clsDAL.DataStore(Head_Insert, Head_Update, Head_Delete, Detail_Insert, Detail_Update, Detail_Delete, GLEDGER_Insert, GLEDGER_Delete, flag);

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

    public static DataTable get()
    {
        throw new NotImplementedException();
    }
}