using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for clsGledgerupdations
/// </summary>
public class clsGledgerupdations
{
    string tblPrefix = string.Empty;
    DataSet MyDs;
    DataSet MyGL;
    DataSet ds;
    string MyTran_Type = "";
    string MyDoc_No = "";
    string MyDoc_Date = "";
    string MyAC_Code = "";
    string MyNarration = "";
    double MyAmount = 0.00;
    int MyOrderCode = 0;
    string MyDrCrHead = "";
    int totalcount = 0;
    string MyVoucherNo = "";
    string MyVoucherType = "";
    string strRef = "";
    int mygledgercount = 0;

    public clsGledgerupdations()
    {
    }

    #region DoUpdation
    public void DOUpdation(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            //using (clsDataProvider objDataProvider = new clsDataProvider())
            //{
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select *,CONVERT(varchar(10),doc_date,103) as doc_date1 From " + tblPrefix + "qryDeliveryOrderList where tran_type='" + Tran_Type + "' and company_code=" + Company_Code + " and Year_Code=" + Year_Code + " and doc_no=" + Doc_No + " order by detail_Id");
            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyTran_Type = MyDs.Tables[0].Rows[0]["tran_type"].ToString();
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date1"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyVoucherNo = MyDs.Tables[0].Rows[0]["voucher_no"].ToString();
                MyVoucherType = MyDs.Tables[0].Rows[0]["voucher_type"].ToString();
                MyDrCrHead = MyDs.Tables[0].Rows[0]["voucher_by"].ToString();
                string desp_type = MyDs.Tables[0].Rows[0]["desp_type"].ToString();
                string getpasscode = MyDs.Tables[0].Rows[0]["GETPASSCODE"].ToString();
                string getpassName = MyDs.Tables[0].Rows[0]["GetPassName"].ToString();
                string getPassCityCode = MyDs.Tables[0].Rows[0]["getpasscityCode"].ToString();
                string getpassCity = clsCommon.getString("Select city_name_e from " + tblPrefix + "CityMaster where city_code=" + getPassCityCode + " and company_code=" + Company_Code);
                string MillShortName = MyDs.Tables[0].Rows[0]["millShortName"].ToString();
                string getpassShortname = MyDs.Tables[0].Rows[0]["getpassShortName"].ToString();
                string Qntl = MyDs.Tables[0].Rows[0]["quantal"].ToString();
                string millRate = MyDs.Tables[0].Rows[0]["mill_rate"].ToString();
                string saleRate = MyDs.Tables[0].Rows[0]["sale_rate"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["truck_no"].ToString();
                string detailNaration = MyDs.Tables[0].Rows[0]["Narration"].ToString();
                string vasuli_rate = MyDs.Tables[0].Rows[0]["vasuli_rate"].ToString();
                string TransportCode = MyDs.Tables[0].Rows[0]["transport"].ToString();
                string TransportShort = clsCommon.getString("Select Short_Name from " + tblPrefix + "AccountMaster where Ac_Code=" + TransportCode + " and Company_Code=" + Company_Code);
                string voucherByNarration = MillShortName + " " + getpassShortname + " " + Qntl + "*" + millRate + " " + lorryNo + " " + getpassName + "," + getpassCity + " " + detailNaration;
                string BankCodeNarration = getpassName + "," + getpassCity + " " + Qntl + "*" + millRate + " " + lorryNo + " " + detailNaration;
                string TransportNarration = Qntl + "*" + vasuli_rate + " " + MillShortName + " " + TransportShort + " Lorry: " + lorryNo + " " + getpassShortname;
                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 0;
                #region DO GLEGDER Updation
                if (Convert.ToDouble(MyDs.Tables[0].Rows[0]["vasuli_amount"]) > 0)
                {
                    totalcount = 2;
                }
                if (MyDs.Tables[0].Rows.Count > 0)
                {
                    totalcount += ((MyDs.Tables[0].Rows.Count) * 2);
                }

                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + MyTran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion

                #region dorecordsequal

                MyNarration = "";
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    double vasuli_amount = MyDs.Tables[0].Rows[0]["vasuli_amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["vasuli_amount"].ToString()) : 0.00;
                    if (vasuli_amount > 0)
                    {
                        if (mygledgercount > MyOrderCode)
                        {
                            MyAC_Code = MyDs.Tables[0].Rows[0]["transport"].ToString();
                            MyAmount = vasuli_amount;
                            string rev = "";
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + TransportNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;

                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='1',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyAC_Code + "',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                        else
                        {
                            MyAC_Code = MyDs.Tables[0].Rows[0]["transport"].ToString();
                            MyAmount = vasuli_amount;
                            string rev = "";
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + TransportNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;

                            MyOrderCode = MyOrderCode + 1;
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','1','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyAC_Code + "','" + MyVoucherType + "','" + MyVoucherNo + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    for (int i = 0; i < MyDs.Tables[0].Rows.Count; i++)
                    {
                        double Amount = MyDs.Tables[0].Rows[i]["Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[i]["Amount"].ToString()) : 0.00;
                        if (desp_type == "DI")
                        {
                            if (mygledgercount > MyOrderCode)
                            {
                                MyAC_Code = MyDs.Tables[0].Rows[i]["Bank_Code"].ToString();
                                MyAmount = Amount;
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + BankCodeNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                MyOrderCode = MyOrderCode + 1;

                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead + "',UNIT_CODE='" + getpasscode + "',NARRATION='" + voucherByNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='" + MyAC_Code + "',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                MyOrderCode = MyOrderCode + 1;
                            }
                            else
                            {
                                MyAC_Code = MyDs.Tables[0].Rows[i]["Bank_Code"].ToString();
                                MyAmount = Amount;
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + BankCodeNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                MyOrderCode = MyOrderCode + 1;

                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + getpasscode + "','" + voucherByNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyAC_Code + "','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                    }
                #endregion
                }
                #endregion
            }
            //}
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region tblPrefix
    private static string Prefix()
    {
        string tblPrefix = clsCommon.getString("Select tblPrefix from tblPrefix");
        return tblPrefix;
    }
    #endregion

    #region Loading Voucher GLedger Effect
    public void LoadingVoucherGlederEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {

        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * From " + tblPrefix + "qryVoucherList where tran_type='" + Tran_Type + "' and company_code=" + Company_Code + " and Year_Code=" + Year_Code + " and doc_no=" + Doc_No);
            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyTran_Type = MyDs.Tables[0].Rows[0]["Tran_Type"].ToString();
                MyDoc_No = MyDs.Tables[0].Rows[0]["Doc_No"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["Doc_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyVoucherNo = MyDs.Tables[0].Rows[0]["Doc_No"].ToString();
                MyVoucherType = MyDs.Tables[0].Rows[0]["Tran_Type"].ToString();
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                string Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString();
                string frtRate = MyDs.Tables[0].Rows[0]["FreightPerQtl"].ToString();
                string Narration1 = MyDs.Tables[0].Rows[0]["Narration1"].ToString();
                string LorryNo = MyDs.Tables[0].Rows[0]["Lorry_No"].ToString();

                string TransportAmount = MyDs.Tables[0].Rows[0]["Cash_Ac_Amount"].ToString();
                string Qntl = MyDs.Tables[0].Rows[0]["Qntl1"].ToString();
                string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millShort"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShort"].ToString();

                string VoucherNarration = Narration1 + " " + LorryNo;
                string TransportNarration = Qntl + "*" + frtRate + " " + TransportAmount + " " + MillShortName + " " + TransportShort + " Lorry: " + LorryNo + " " + PartyShort;
                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 0;

                double Diff_Rate = MyDs.Tables[0].Rows[0]["Diff_Rate"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Diff_Rate"]) : 0.00;
                double Brokrage = MyDs.Tables[0].Rows[0]["Brokrage"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Brokrage"]) : 0.00;
                double Service_Charge = MyDs.Tables[0].Rows[0]["Service_Charge"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Service_Charge"]) : 0.00;
                double RATEDIFF = MyDs.Tables[0].Rows[0]["RATEDIFF"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["RATEDIFF"]) : 0.00;
                double Commission_Amount = MyDs.Tables[0].Rows[0]["Commission_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Commission_Amount"]) : 0.00;
                double Interest = MyDs.Tables[0].Rows[0]["Interest"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Interest"]) : 0.00;
                double Transport_Amount = MyDs.Tables[0].Rows[0]["Transport_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Transport_Amount"]) : 0.00;
                double OTHER_Expenses = MyDs.Tables[0].Rows[0]["OTHER_Expenses"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["OTHER_Expenses"]) : 0.00;
                double Cash_Ac_Amount = MyDs.Tables[0].Rows[0]["Cash_Ac_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Cash_Ac_Amount"]) : 0.00;
                double Mill_Amount = MyDs.Tables[0].Rows[0]["Mill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Mill_Amount"]) : 0.00;
                double Mill_Amount1 = MyDs.Tables[0].Rows[0]["Mill_Amount1"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Mill_Amount1"]) : 0.00;
                #region DO GLEGDER Updation
                if (Diff_Rate != 0)
                {
                    totalcount = 1;
                }
                if (Brokrage > 0)
                {
                    ++totalcount;
                }
                if (Service_Charge > 0)
                {
                    ++totalcount;
                }
                if (RATEDIFF > 0)
                {
                    ++totalcount;
                }
                if (Commission_Amount > 0)
                {
                    ++totalcount;
                }
                if (Interest > 0)
                {
                    ++totalcount;
                }
                if (Transport_Amount > 0)
                {
                    ++totalcount;
                }
                if (OTHER_Expenses > 0)
                {
                    ++totalcount;
                }
                if (Cash_Ac_Amount > 0)
                {
                    totalcount = totalcount + 2;
                }
                double transport_amount = Cash_Ac_Amount + Mill_Amount + Mill_Amount1;
                if ((Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"]) - transport_amount) != 0)
                {
                    totalcount = totalcount + 1;
                }

                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + MyTran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion

                #region dorecordsequal

                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string rev = "";
                    if (Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"]) > 0)
                    {
                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["QUALITY_DIFF_AC"].ToString();
                            MyAmount = Diff_Rate;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                if (MyAmount > 0)
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                else
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["QUALITY_DIFF_AC"].ToString();
                            MyAmount = Diff_Rate;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                if (MyAmount > 0)
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                else
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["BROKRAGE_AC"].ToString();
                            MyAmount = Brokrage;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["BROKRAGE_AC"].ToString();
                            MyAmount = Brokrage;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["SERVICE_CHARGE_AC"].ToString();
                            MyAmount = Service_Charge;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["SERVICE_CHARGE_AC"].ToString();
                            MyAmount = Service_Charge;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["QUALITY_DIFF_AC"].ToString();
                            MyAmount = RATEDIFF;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["QUALITY_DIFF_AC"].ToString();
                            MyAmount = RATEDIFF;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["COMMISSION_AC"].ToString();
                            MyAmount = Commission_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["COMMISSION_AC"].ToString();
                            MyAmount = Commission_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }

                        }
                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["INTEREST_AC"].ToString();
                            MyAmount = Interest;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["INTEREST_AC"].ToString();
                            MyAmount = Interest;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["TRANSPORT_AC"].ToString();
                            MyAmount = Transport_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["TRANSPORT_AC"].ToString();
                            MyAmount = Transport_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["OTHER_AMOUNT_AC"].ToString();
                            MyAmount = OTHER_Expenses;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["OTHER_AMOUNT_AC"].ToString();
                            MyAmount = OTHER_Expenses;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = MyDs.Tables[0].Rows[0]["Cash_Account"].ToString();
                            MyAmount = Cash_Ac_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + TransportNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = MyDs.Tables[0].Rows[0]["Cash_Account"].ToString();
                            MyAmount = Cash_Ac_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + TransportNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString(); ;
                            MyAmount = Cash_Ac_Amount;
                            if (Convert.ToDouble(MyAmount) != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + "Advance " + TransportShort + " " + VoucherNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                            MyAmount = Cash_Ac_Amount;
                            if (Convert.ToDouble(MyAmount) != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + Unit_Code + "','" + "Advance " + TransportShort + " " + VoucherNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            transport_amount = Cash_Ac_Amount + Mill_Amount + Mill_Amount1;
                            MyAC_Code = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                            MyAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"].ToString()) - transport_amount;
                            if (Convert.ToDouble(MyAmount) != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                if (Convert.ToDouble(MyAmount) > 0)
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + VoucherNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                else
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + VoucherNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            transport_amount = Cash_Ac_Amount + Mill_Amount + Mill_Amount1;
                            MyAC_Code = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                            MyAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"].ToString()) - transport_amount;
                            if (Convert.ToDouble(MyAmount) != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                if (Convert.ToDouble(MyAmount) > 0)
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + Unit_Code + "','" + VoucherNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                else
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + Unit_Code + "','" + VoucherNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                    }
                #endregion
                }
                #endregion
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Commision Voucher GLedger Effect
    public void CommisionVoucherGlederEffect(string Tran_Type, string Suffix, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            string qry1 = "Select * From " + tblPrefix + "qryVoucherList where Suffix='" + Suffix + "' and tran_type='" + Tran_Type + "' and company_code=" + Company_Code + " and Year_Code=" + Year_Code + " and doc_no=" + Doc_No;
            MyDs = clsDAL.SimpleQuery(qry1);
            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyTran_Type = MyDs.Tables[0].Rows[0]["Tran_Type"].ToString();
                MyDoc_No = MyDs.Tables[0].Rows[0]["Doc_No"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["Doc_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyVoucherNo = MyDs.Tables[0].Rows[0]["Doc_No"].ToString();
                MyVoucherType = MyDs.Tables[0].Rows[0]["Tran_Type"].ToString();
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                string Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString();
                string Narration1 = MyDs.Tables[0].Rows[0]["Narration1"].ToString();
                string LorryNo = MyDs.Tables[0].Rows[0]["Lorry_No"].ToString();
                string Qntl = MyDs.Tables[0].Rows[0]["Qntl1"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millShort"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShort"].ToString();

                string VoucherNarration = Narration1 + " " + LorryNo;

                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 0;
                double Voucher_Amount = MyDs.Tables[0].Rows[0]["Voucher_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"]) : 0.00;
                double Diff_Rate = MyDs.Tables[0].Rows[0]["Diff_Rate"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Diff_Rate"]) : 0.00;
                double Commission_Amount = MyDs.Tables[0].Rows[0]["Commission_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Commission_Amount"]) : 0.00;
                double loading_Charges = MyDs.Tables[0].Rows[0]["Loading_Charge"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Loading_Charge"]) : 0.00;
                double otherAmount = Commission_Amount + loading_Charges;

                #region DO GLEGDER Updation
                if (Commission_Amount > 0)
                {
                    ++totalcount;
                }

                if ((Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"])) != 0)
                {
                    totalcount = totalcount + 1;
                }

                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + MyTran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion

                #region dorecordsequal

                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string rev = "";
                    if (Convert.ToDouble(MyDs.Tables[0].Rows[0]["Voucher_Amount"]) > 0)
                    {
                        //debit or credit to Partys account
                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = MyDrCrHead;
                            MyAmount = Voucher_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                if (MyAmount > 0)
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                else
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = MyDrCrHead;
                            MyAmount = Voucher_Amount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                if (MyAmount > 0)
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                else
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }

                        if (mygledgercount >= MyOrderCode)
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["QUALITY_DIFF_AC"].ToString();
                            MyAmount = otherAmount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                if (MyAmount > 0)
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                else
                                {
                                    obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + MyNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='1',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                }
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                        else
                        {
                            MyAC_Code = System.Web.HttpContext.Current.Session["QUALITY_DIFF_AC"].ToString();
                            MyAmount = otherAmount;
                            if (MyAmount != 0)
                            {
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                if (MyAmount > 0)
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }
                                else
                                {
                                    obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + MyNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','1','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                }

                                ds = obj.insertAccountMaster(ref strRef);
                                rev = strRef;
                                MyOrderCode = MyOrderCode + 1;
                            }
                        }
                    }

                    for (int i = 0; i < MyDs.Tables[0].Rows.Count; i++)
                    {
                        string BankCode = MyDs.Tables[0].Rows[i]["Bank_Code"].ToString();
                        double detailAmount = MyDs.Tables[0].Rows[i]["Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[i]["Amount"].ToString()) : 0.0;
                        string detailsNarration = MyDs.Tables[0].Rows[i]["Narration"].ToString();
                        if (detailAmount != 0)
                        {
                            if (mygledgercount > MyOrderCode)
                            {
                                MyAC_Code = BankCode;
                                MyAmount = detailAmount;
                                obj.flag = 2;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyAC_Code + "',NARRATION='" + detailsNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + MyVoucherType + "',SORT_NO='" + MyVoucherNo + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + MyTran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                                obj.values = "none";
                                ds = obj.insertAccountMaster(ref strRef);
                                MyOrderCode = MyOrderCode + 1;
                            }
                            else
                            {
                                MyAC_Code = BankCode;
                                MyAmount = detailAmount;
                                obj.flag = 1;
                                obj.tableName = tblPrefix + "GLEDGER";
                                obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                                obj.values = "'" + MyTran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyAC_Code + "','" + detailsNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','" + MyVoucherType + "','" + MyVoucherNo + "'";
                                ds = obj.insertAccountMaster(ref strRef);
                                MyOrderCode = MyOrderCode + 1;

                            }
                        }
                    }
                #endregion
                }
                #endregion
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Sugar Purchase Gledger
    public void SugarPurchaseGledgerEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from " + tblPrefix + "qrySugarPurcList where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                int Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString() != string.Empty ? Convert.ToInt32(MyDs.Tables[0].Rows[0]["Unit_Code"].ToString()) : 0;
                string itemCode = "";
                itemCode = MyDs.Tables[0].Rows[0]["item_code"].ToString();
                string Purchase_Account = clsCommon.getString("select Purchase_AC from " + tblPrefix + "SystemMaster where System_Type='I' and System_Code=" + itemCode + " and Company_Code=" + Company_Code);
                double Bill_Amount = MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString()) : 0.00;
                string Qntl = MyDs.Tables[0].Rows[0]["NETQNTL"].ToString();
                string TransportAmount = MyDs.Tables[0].Rows[0]["cash_advance"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millshortname"].ToString();
                //string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["LORRYNO"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShortname"].ToString();
                string BrokerShort = MyDs.Tables[0].Rows[0]["BrokerShort"].ToString();
                string Subtotal = MyDs.Tables[0].Rows[0]["subTotal"].ToString();
                string DebitNarration = MillShortName + " " + Qntl + " Lorry: " + lorryNo + " " + BrokerShort;
                string CreditNarration = PartyShort + " " + Qntl + " " + " Lorry: " + lorryNo + " " + Subtotal + "/" + Qntl;
                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 0;
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {

                    //Credit Effect for Party
                    string rev = "";

                    if (mygledgercount >= MyOrderCode)
                    {
                        obj.flag = 2;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Bill_Amount + "',DRCR='C',DRCR_HEAD='" + Purchase_Account + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }
                    else
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + Unit_Code + "','" + CreditNarration + "','" + Bill_Amount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + Purchase_Account + "','" + Tran_Type + "','" + Doc_No + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }
                    if (mygledgercount >= MyOrderCode)
                    {
                        obj.flag = 2;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Purchase_Account + "',NARRATION='" + DebitNarration + "',AMOUNT='" + Bill_Amount + "',DRCR='D',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;

                    }
                    else
                    {
                        //Debit Effect for Purchase Account
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Purchase_Account + "','" + DebitNarration + "','" + Bill_Amount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }

                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Sugar Purchase Return Gledger
    public void SugarPurchaseReturnGledgerEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from " + tblPrefix + "qrySugarPurcListReturn where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                int Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString() != string.Empty ? Convert.ToInt32(MyDs.Tables[0].Rows[0]["Unit_Code"].ToString()) : 0;
                string itemCode = "";
                itemCode = MyDs.Tables[0].Rows[0]["item_code"].ToString();
                string Purchase_Account = clsCommon.getString("select SaleReturnAc from " + tblPrefix + "CompanyParameters where Company_Code=" + Company_Code);
                double Bill_Amount = MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString()) : 0.00;
                string Qntl = MyDs.Tables[0].Rows[0]["NETQNTL"].ToString();
                string TransportAmount = MyDs.Tables[0].Rows[0]["cash_advance"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millshortname"].ToString();
                //string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["LORRYNO"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShortname"].ToString();
                string BrokerShort = MyDs.Tables[0].Rows[0]["BrokerShort"].ToString();
                string Subtotal = MyDs.Tables[0].Rows[0]["subTotal"].ToString();
                string PurcNo = MyDs.Tables[0].Rows[0]["PURCNO"].ToString();
                string PurcTranType = MyDs.Tables[0].Rows[0]["PurcTranType"].ToString();
                string DebitNarration = MillShortName + " " + Qntl + " Lorry: " + lorryNo + " " + BrokerShort + " " + PartyShort + " " + PurcNo + ":" + PurcTranType;
                string CreditNarration = PartyShort + " " + Qntl + " " + " Lorry: " + Subtotal + "/" + Qntl + " " + PurcNo + ":" + PurcTranType;
                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 0;
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    //Credit Effect for Party
                    string rev = "";

                    if (mygledgercount >= MyOrderCode)
                    {
                        obj.flag = 2;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Bill_Amount + "',DRCR='C',DRCR_HEAD='" + Purchase_Account + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;

                    }
                    else
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + Unit_Code + "','" + CreditNarration + "','" + Bill_Amount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + Purchase_Account + "','" + Tran_Type + "','" + Doc_No + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }
                    if (mygledgercount >= MyOrderCode)
                    {
                        obj.flag = 2;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Purchase_Account + "',NARRATION='" + DebitNarration + "',AMOUNT='" + Bill_Amount + "',DRCR='D',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;

                    }
                    else
                    {
                        //Debit Effect for Purchase Account
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Purchase_Account + "','" + DebitNarration + "','" + Bill_Amount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }

                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    public void SugarSaleGledgerEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from " + tblPrefix + "qrySugarSaleList where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                int Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString() != string.Empty ? Convert.ToInt32(MyDs.Tables[0].Rows[0]["Unit_Code"].ToString()) : 0;
                //string DO_No = MyDs.Tables[0].Rows[0]["DO_No"].ToString();
                //string CashCredit = clsCommon.getString("Select MM_CC from " + tblPrefix + "deliveryorder where doc_no=" + DO_No + " and tran_type='DO' and company_code=" + Company_Code + " and Year_Code=" + Year_Code + "");
                string Transport_Ac = MyDs.Tables[0].Rows[0]["Transport_Code"].ToString();
                string itemCode = "";
                itemCode = MyDs.Tables[0].Rows[0]["item_code"].ToString();
                string Sale_Account = clsCommon.getString("select Sale_AC from " + tblPrefix + "SystemMaster where System_Type='I' and System_Code=" + itemCode + " and Company_Code=" + Company_Code);
                double Bill_Amount = MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString()) : 0.00;
                double Cash_Advance = MyDs.Tables[0].Rows[0]["cash_advance"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["cash_advance"].ToString()) : 0.00;
                string Qntl = MyDs.Tables[0].Rows[0]["NETQNTL"].ToString();
                string TransportAmount = MyDs.Tables[0].Rows[0]["cash_advance"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millshortname"].ToString();
                string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["LORRYNO"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShortname"].ToString();
                string PartyFullName = MyDs.Tables[0].Rows[0]["PartyName"].ToString();
                string BrokerShort = MyDs.Tables[0].Rows[0]["BrokerShort"].ToString();
                string Subtotal = MyDs.Tables[0].Rows[0]["subTotal"].ToString();
                string PURCNO = MyDs.Tables[0].Rows[0]["PURCNO"].ToString();
                string DO_No = MyDs.Tables[0].Rows[0]["DO_No"].ToString();

                string CarporateSaleNo = clsCommon.getString("Select Carporate_Sale_No from " + tblPrefix + "deliveryorder where doc_no=" + DO_No + " and tran_type='DO' and company_code=" + Company_Code + " and Year_Code=" + Year_Code);

                string PODetails = clsCommon.getString("Select PODETAIL from " + tblPrefix + "CarporateSale where Doc_No=" + CarporateSaleNo + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);
                if (!string.IsNullOrWhiteSpace(PODetails))
                {
                    PODetails = "PO Details:" + PODetails;
                }

                if (!string.IsNullOrWhiteSpace(BrokerShort))
                {
                    BrokerShort = "Brok:" + BrokerShort;
                }
                if (!string.IsNullOrWhiteSpace(PartyShort))
                {
                    PartyShort = "Party:" + PartyShort;
                }
                string TransportNarration = Qntl + " " + TransportAmount + " " + MillShortName + " " + TransportShort + " Lorry: " + lorryNo + " " + PartyShort;

                string UnitCity = clsCommon.getString("Select CityName from " + tblPrefix + "qryAccountsList where Ac_Code=" + Unit_Code + " and Company_Code=" + Company_Code + "");

                string DebitNarration = MillShortName + " " + Qntl + " Lorry: " + lorryNo + " " + BrokerShort + " " + UnitCity + " Purc.No:" + PURCNO + " " + PODetails;

                string CreditNarration = PartyFullName + " " + Qntl + " " + " Lorry: " + lorryNo + " Do.No:" + DO_No;

                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 2;
                if (Cash_Advance != 0)
                {
                    totalcount = totalcount + 1;
                }

                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string rev = "";
                    #region Transport Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Transport_Ac + "',NARRATION='" + TransportNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Transport_Ac + "','" + TransportNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                    #region Sale Account Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Bill_Amount - Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Sale_Account + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='DO',SORT_NO='" + DO_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Bill_Amount - Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Sale_Account + "','" + CreditNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','DO','" + DO_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                    #region Party Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Bill_Amount;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + DebitNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='0',SORT_TYPE='DO',SORT_NO='" + DO_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Bill_Amount;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + Unit_Code + "','" + DebitNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','0','DO','" + DO_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                }

            }
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            throw;
        }
    }

    public void SugarSaleReturnGledgerEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from " + tblPrefix + "qrySugarSaleListReturn where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                int Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString() != string.Empty ? Convert.ToInt32(MyDs.Tables[0].Rows[0]["Unit_Code"].ToString()) : 0;
                string Transport_Ac = MyDs.Tables[0].Rows[0]["Transport_Code"].ToString();
                string itemCode = "";
                itemCode = MyDs.Tables[0].Rows[0]["item_code"].ToString();
                string Sale_Account = clsCommon.getString("select ReturnSaleAc from " + tblPrefix + "CompanyParameters where Company_Code=" + Company_Code);
                double Bill_Amount = MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString()) : 0.00;
                double Cash_Advance = MyDs.Tables[0].Rows[0]["cash_advance"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["cash_advance"].ToString()) : 0.00;
                string Qntl = MyDs.Tables[0].Rows[0]["NETQNTL"].ToString();
                string TransportAmount = MyDs.Tables[0].Rows[0]["cash_advance"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millshortname"].ToString();
                string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["LORRYNO"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShortname"].ToString();
                string BrokerShort = MyDs.Tables[0].Rows[0]["BrokerShort"].ToString();
                string Subtotal = MyDs.Tables[0].Rows[0]["subTotal"].ToString();
                string PURCNO = MyDs.Tables[0].Rows[0]["PURCNO"].ToString();
                string TransportNarration = Qntl + " " + TransportAmount + " " + MillShortName + " " + TransportShort + " Lorry: " + lorryNo + " " + PartyShort;
                string DebitNarration = MillShortName + " " + Qntl + " Lorry: " + lorryNo + " Brok:" + BrokerShort + " Party:" + PartyShort + " Purc.No:" + PURCNO;
                string CreditNarration = PartyShort + " " + Qntl + " " + " Lorry: " + Subtotal + "/" + Qntl + " Purc.No:" + PURCNO;
                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 2;
                if (Cash_Advance != 0)
                {
                    totalcount = totalcount + 1;
                }

                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string rev = "";
                    #region Transport Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Transport_Ac + "',NARRATION='" + TransportNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }

                    }
                    else
                    {
                        MyAmount = Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Transport_Ac + "','" + TransportNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                    #region Sale Account Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Bill_Amount - Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Sale_Account + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Bill_Amount - Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Sale_Account + "','" + CreditNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                    #region Party Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Bill_Amount;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + DebitNarration + "',AMOUNT='" + Math.Abs(MyAmount) + "',DRCR='D',DRCR_HEAD='0',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Bill_Amount;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + Unit_Code + "','" + DebitNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','0','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Sugar Purchase Gledger For GST
    public void SugarPurchaseGledgerEffectForGST(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            //using (clsDataProvider objDataProvider = new clsDataProvider())
            //{
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from " + tblPrefix + "qrySugarPurcList where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                int Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString() != string.Empty ? Convert.ToInt32(MyDs.Tables[0].Rows[0]["Unit_Code"].ToString()) : 0;
                string itemCode = "";
                itemCode = MyDs.Tables[0].Rows[0]["item_code"].ToString();
                string Purchase_Account = clsCommon.getString("select Purchase_AC from " + tblPrefix + "SystemMaster where System_Type='I' and System_Code=" + itemCode + " and Company_Code=" + Company_Code);
                double Bill_Amount = MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString()) : 0.00;
                string Qntl = MyDs.Tables[0].Rows[0]["NETQNTL"].ToString();
                string TransportAmount = MyDs.Tables[0].Rows[0]["cash_advance"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millshortname"].ToString();
                //string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["LORRYNO"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShortname"].ToString();
                string BrokerShort = MyDs.Tables[0].Rows[0]["BrokerShort"].ToString();
                string Subtotal = MyDs.Tables[0].Rows[0]["subTotal"].ToString();
                string DebitNarration = MillShortName + " " + Qntl + " Lorry: " + lorryNo + " " + BrokerShort;
                string CreditNarration = PartyShort + " " + Qntl + " " + " Lorry: " + lorryNo + " " + Subtotal + "/" + Qntl;
                double CGSTAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["CGSTAmount"].ToString());
                double SGSTAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["SGSTAmount"].ToString());
                double IGSTAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["IGSTAmount"].ToString());

                double totalGSTAmount = (CGSTAmount + SGSTAmount + IGSTAmount);
                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 2;

                if (CGSTAmount != 0)
                {
                    totalcount = totalcount + 1;
                }
                if (SGSTAmount != 0)
                {
                    totalcount = totalcount + 1;
                }
                if (IGSTAmount != 0)
                {
                    totalcount = totalcount + 1;
                }

                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion

                int PurchaseCGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["PurchaseCGSTAc"].ToString());
                int PurchaseSGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["PurchaseSGSTAc"].ToString());
                int PurchaseIGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["PurchaseIGSTAc"].ToString());
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    //Credit Effect for Party
                    string rev = "";

                    if (mygledgercount >= MyOrderCode)
                    {
                        obj.flag = 2;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Bill_Amount + "',DRCR='C',DRCR_HEAD='" + Purchase_Account + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }
                    else
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + Unit_Code + "','" + CreditNarration + "','" + Bill_Amount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + Purchase_Account + "','" + Tran_Type + "','" + Doc_No + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;

                    }
                    if (mygledgercount >= MyOrderCode)
                    {
                        obj.flag = 2;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Purchase_Account + "',NARRATION='" + DebitNarration + "',AMOUNT='" + (Bill_Amount - totalGSTAmount) + "',DRCR='D',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;

                    }
                    else
                    {
                        //Debit Effect for Purchase Account
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Purchase_Account + "','" + DebitNarration + "','" + (Bill_Amount - totalGSTAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }

                    if (mygledgercount >= MyOrderCode)
                    {
                        if (CGSTAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + PurchaseCGSTAc + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + CreditNarration + "',AMOUNT='" + CGSTAmount + "',DRCR='D',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        if (CGSTAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + PurchaseCGSTAc + "','" + Unit_Code + "','" + CreditNarration + "','" + CGSTAmount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }

                    if (mygledgercount >= MyOrderCode)
                    {

                        if (SGSTAmount != 0)
                        {

                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + PurchaseSGSTAc + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + CreditNarration + "',AMOUNT='" + SGSTAmount + "',DRCR='D',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }

                    }
                    else
                    {
                        if (SGSTAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + PurchaseSGSTAc + "','" + Unit_Code + "','" + CreditNarration + "','" + SGSTAmount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }

                    if (mygledgercount >= MyOrderCode)
                    {
                        if (IGSTAmount != 0)
                        {

                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + PurchaseIGSTAc + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + CreditNarration + "',AMOUNT='" + IGSTAmount + "',DRCR='D',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        if (IGSTAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + PurchaseIGSTAc + "','" + Unit_Code + "','" + CreditNarration + "','" + IGSTAmount + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                }
            }
            //}
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion


    public void SugarSaleGledgerEffectForGST(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            //using (clsDataProvider objDataProvider = new clsDataProvider())
            //{
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from " + tblPrefix + "qrySugarSaleList where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Ac_Code"].ToString();
                int Unit_Code = MyDs.Tables[0].Rows[0]["Unit_Code"].ToString() != string.Empty ? Convert.ToInt32(MyDs.Tables[0].Rows[0]["Unit_Code"].ToString()) : 0;
                //string DO_No = MyDs.Tables[0].Rows[0]["DO_No"].ToString();
                //string CashCredit = clsCommon.getString("Select MM_CC from " + tblPrefix + "deliveryorder where doc_no=" + DO_No + " and tran_type='DO' and company_code=" + Company_Code + " and Year_Code=" + Year_Code + "");
                string Transport_Ac = MyDs.Tables[0].Rows[0]["Transport_Code"].ToString();
                string itemCode = "";
                itemCode = MyDs.Tables[0].Rows[0]["item_code"].ToString();
                string Sale_Account = clsCommon.getString("select Sale_AC from " + tblPrefix + "SystemMaster where System_Type='I' and System_Code=" + itemCode + " and Company_Code=" + Company_Code);
                double Bill_Amount = MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Bill_Amount"].ToString()) : 0.00;
                double Cash_Advance = MyDs.Tables[0].Rows[0]["cash_advance"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["cash_advance"].ToString()) : 0.00;
                string Qntl = MyDs.Tables[0].Rows[0]["NETQNTL"].ToString();
                string TransportAmount = MyDs.Tables[0].Rows[0]["cash_advance"].ToString();
                string MillShortName = MyDs.Tables[0].Rows[0]["millshortname"].ToString();
                string TransportShort = MyDs.Tables[0].Rows[0]["TransportShort"].ToString();
                string lorryNo = MyDs.Tables[0].Rows[0]["LORRYNO"].ToString();
                string PartyShort = MyDs.Tables[0].Rows[0]["PartyShortname"].ToString();
                string PartyFullName = MyDs.Tables[0].Rows[0]["PartyName"].ToString();
                string BrokerShort = MyDs.Tables[0].Rows[0]["BrokerShort"].ToString();
                string Subtotal = MyDs.Tables[0].Rows[0]["subTotal"].ToString();
                string PURCNO = MyDs.Tables[0].Rows[0]["PURCNO"].ToString();
                string DO_No = MyDs.Tables[0].Rows[0]["DO_No"].ToString();
                //string Roundoff = MyDs.Tables[0].Rows[0]["RoundOff"].ToString();

                double Roundoff = Math.Abs(MyDs.Tables[0].Rows[0]["RoundOff"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["RoundOff"].ToString()) : 0.00);
                double roundof1 = Convert.ToDouble(MyDs.Tables[0].Rows[0]["RoundOff"].ToString());
                string drcr_Roundoff = Convert.ToDouble(MyDs.Tables[0].Rows[0]["RoundOff"]) > 0 ? "C" : "D";
                string Roundoff_ac = clsCommon.getString("select RoundOff from NT_1_CompanyParameters where Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);



                string CarporateSaleNo = clsCommon.getString("Select Carporate_Sale_No from " + tblPrefix + "deliveryorder where doc_no=" + DO_No + " and tran_type='DO' and company_code=" + Company_Code + " and Year_Code=" + Year_Code);

                string PODetails = clsCommon.getString("Select PODETAIL from " + tblPrefix + "CarporateSale where Doc_No=" + CarporateSaleNo + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);
                if (!string.IsNullOrWhiteSpace(PODetails))
                {
                    PODetails = "PO Details:" + PODetails;
                }

                if (!string.IsNullOrWhiteSpace(BrokerShort))
                {
                    BrokerShort = "Brok:" + BrokerShort;
                }
                if (!string.IsNullOrWhiteSpace(PartyShort))
                {
                    PartyShort = "Party:" + PartyShort;
                }
                string TransportNarration = Qntl + " " + TransportAmount + " " + MillShortName + " " + TransportShort + " Lorry: " + lorryNo + " " + PartyShort;

                string UnitCity = clsCommon.getString("Select CityName from " + tblPrefix + "qryAccountsList where Ac_Code=" + Unit_Code + " and Company_Code=" + Company_Code + "");

                string DebitNarration = MillShortName + " " + Qntl + " Lorry: " + lorryNo + " " + BrokerShort + " " + UnitCity + " Purc.No:" + PURCNO + " " + PODetails;

                string CreditNarration = PartyFullName + " " + Qntl + " " + " Lorry: " + lorryNo + " Do.No:" + DO_No;
                double CGSTAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["CGSTAmount"].ToString());
                double SGSTAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["SGSTAmount"].ToString());
                double IGSTAmount = Convert.ToDouble(MyDs.Tables[0].Rows[0]["IGSTAmount"].ToString());

                double totalGSTAmount = (CGSTAmount + SGSTAmount + IGSTAmount);


                MyOrderCode = 1;


                totalcount = 2;
                if (Cash_Advance != 0)
                {
                    totalcount = totalcount + 1;
                }
                if (Roundoff != 0)
                {
                    totalcount = totalcount + 1;
                }

                if (CGSTAmount != 0)
                {
                    totalcount = totalcount + 1;
                }
                if (SGSTAmount != 0)
                {
                    totalcount = totalcount + 1;
                }
                if (IGSTAmount != 0)
                {
                    totalcount = totalcount + 1;
                }

                DataSet MyGL1 = clsDAL.SimpleQuery("delete  from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No
                       + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + " and [ORDER_CODE] > " + totalcount);

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;

                int SaleCGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["SaleCGSTAc"].ToString());
                int SaleSGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["SaleSGSTAc"].ToString());
                int SaleIGSTAc = Convert.ToInt32(System.Web.HttpContext.Current.Session["SaleIGSTAc"].ToString());


                #region dodeletemorerecords
                if (totalcount < MyGL.Tables[0].Rows.Count)
                {
                    ds = clsDAL.SimpleQuery("Delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='"
                        + Tran_Type + "' and DOC_NO='" + MyDoc_No + "' and COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='"
                        + Year_Code + "'  AND ORDER_CODE>'" + totalcount + "'");
                }
                #endregion
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string rev = "";
                    #region Transport Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date
                                + "',AC_CODE='" + Transport_Ac + "',NARRATION='" + TransportNarration + "',AMOUNT='" + Math.Abs(MyAmount)
                                + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='DO',SORT_NO='" + DO_No + "' where COMPANY_CODE='"
                                + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type
                                + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Transport_Ac + "','"
                                + TransportNarration + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" 
                                + MyDrCrHead + "','DO','" + DO_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion

                    #region Sale Account Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Bill_Amount - (Cash_Advance + roundof1);
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Sale_Account
                                + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Round((Math.Abs(MyAmount - totalGSTAmount)), 2) + "',DRCR='C',DRCR_HEAD='"
                                + MyDrCrHead + "',SORT_TYPE='DO',SORT_NO='" + DO_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code
                                + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Bill_Amount - Cash_Advance;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Sale_Account + "','" + CreditNarration + "','"
                                + Math.Round((Math.Abs(MyAmount - totalGSTAmount)), 2) + "','" + Company_Code + "','" + Year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','"
                                + MyDrCrHead + "','DO','" + DO_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion

                    if (mygledgercount >= MyOrderCode)
                    {
                        if (CGSTAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + SaleCGSTAc
                                + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Abs(CGSTAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead
                                + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code
                                + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        if (CGSTAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + SaleCGSTAc + "','"
                                + CreditNarration + "','" + Math.Abs(CGSTAmount) + "','" + Company_Code + "','" + Year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode
                                + "','C','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }






                    if (mygledgercount >= MyOrderCode)
                    {
                        if (SGSTAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + SaleSGSTAc
                                + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Abs(SGSTAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead
                                + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='"
                                + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        if (SGSTAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + SaleSGSTAc + "','" + CreditNarration + "','"
                                + Math.Abs(SGSTAmount) + "','" + Company_Code + "','" + Year_Code + "','" +
                                Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','" + MyDrCrHead
                                + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }

                    if (mygledgercount >= MyOrderCode)
                    {
                        if (IGSTAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + SaleIGSTAc
                                + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Abs(IGSTAmount) + "',DRCR='C',DRCR_HEAD='" + MyDrCrHead + "',SORT_TYPE='"
                                + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='"
                                + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        if (IGSTAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + SaleIGSTAc + "','" + CreditNarration + "','" + Math.Abs(IGSTAmount)
                                + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString())
                                + "','" + MyOrderCode + "','C','" + MyDrCrHead + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }

                    #region Party Effect
                    if (mygledgercount >= MyOrderCode)
                    {
                        MyAmount = Bill_Amount;
                        if (MyAmount != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + MyDrCrHead
                                + "',UNIT_CODE='" + Unit_Code + "',NARRATION='" + DebitNarration + "',AMOUNT='" + Math.Abs(MyAmount)
                                + "',DRCR='D',DRCR_HEAD='0',SORT_TYPE='DO',SORT_NO='" + DO_No + "' where COMPANY_CODE='" + Company_Code
                                + "' AND YEAR_CODE='" + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        MyAmount = Bill_Amount;
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,UNIT_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + MyDrCrHead + "','" + Unit_Code + "','" + DebitNarration
                                + "','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','0','DO','" + DO_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion


                    #region[round off]


                    if (mygledgercount >= MyOrderCode)
                    {
                        if (Roundoff != 0)
                        {
                            obj.flag = 2;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE='" + Tran_Type + "',DOC_NO='" + MyDoc_No + "',DOC_DATE='" + MyDoc_Date + "',AC_CODE='" + Roundoff_ac
                                + "',NARRATION='" + CreditNarration + "',AMOUNT='" + Math.Abs(Roundoff) + "',DRCR='" + drcr_Roundoff + "',DRCR_HEAD='" + Roundoff_ac
                                + "',SORT_TYPE='" + Tran_Type + "',SORT_NO='" + Doc_No + "' where COMPANY_CODE='" + Company_Code + "' AND YEAR_CODE='"
                                + Year_Code + "' AND ORDER_CODE='" + MyOrderCode + "' AND TRAN_TYPE='" + Tran_Type + "' AND DOC_NO='" + MyDoc_No + "'";
                            obj.values = "none";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    else
                    {
                        if (Roundoff != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Roundoff_ac + "','"
                                + CreditNarration + "','" + Math.Abs(Roundoff) + "','" + Company_Code + "','" + Year_Code + "','"
                                + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode
                                + "','" + drcr_Roundoff + "','" + Roundoff_ac + "','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                    }
                    #endregion
                }
            }
            //}
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            throw;
        }
    }

    public void JaggrySaleGledgerEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from NT_1_JSaleHead where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["Cust_Code"].ToString();
                double Total = MyDs.Tables[0].Rows[0]["Total"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Total"].ToString()) : 0;
                string Cashcredit = MyDs.Tables[0].Rows[0]["Cash_Credit"].ToString();
                double Bill_Amount = MyDs.Tables[0].Rows[0]["BillAmt"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["BillAmt"].ToString()) : 0.00;
                double Shubamt = MyDs.Tables[0].Rows[0]["Shub_Amount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Shub_Amount"].ToString()) : 0.00;

                double kharajat = MyDs.Tables[0].Rows[0]["Khajarat"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Khajarat"].ToString()) : 0.00;

                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 2;

                string KharajatAc = clsCommon.getString("select Kharajat_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);
                string shubamtAc = clsCommon.getString("select Shub_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);
                string Jagagry_Sale_Ac = clsCommon.getString("select Jagagry_Sale_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);

                double Jagagry_Sale_ac_Amt = Total - (kharajat + Shubamt);
                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string rev = "";
                    MyAmount = Bill_Amount;

                    //if (MyAmount != 0)
                    //{
                    //    obj.flag = 1;
                    //    obj.tableName = tblPrefix + "GLEDGER";
                    //    obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                    //    obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + Cashcredit + "','" + MyDoc_Date + "','" + MyDrCrHead + "','Jawak Sale Debited','" + Math.Abs(Total) + "','" + Company_Code + "','" + Year_Code +  "','" + MyOrderCode + "','D','0'";
                    //    ds = obj.insertAccountMaster(ref strRef);
                    //    rev = strRef;
                    //    MyOrderCode = MyOrderCode + 1;
                    //}

                    if (kharajat != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + Cashcredit + "','" + MyDoc_Date + "','" + KharajatAc + "','Kharajat Credit','" + Math.Abs(kharajat) + "','" + Company_Code + "','" + Year_Code + "','" + "','" + MyOrderCode + "','C','0','";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }


                    if (Jagagry_Sale_ac_Amt != 0)
                    {

                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                        obj.values = "'JS','" + Cashcredit + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Jagagry_Sale_Ac + "','Credit','" + Math.Abs(Jagagry_Sale_ac_Amt) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','D','" + Jagagry_Sale_Ac + "'";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;

                    }
                    if (Shubamt != 0)
                    {
                        obj.flag = 1;
                        obj.tableName = tblPrefix + "GLEDGER";
                        obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + Cashcredit + "','" + MyDoc_Date + "','" + Jagagry_Sale_Ac + "','Credit','" + Math.Abs(Shubamt) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','C','0','";
                        ds = obj.insertAccountMaster(ref strRef);
                        rev = strRef;
                        MyOrderCode = MyOrderCode + 1;
                    }

                }

            }
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            throw;
        }
    }


    public void JaggryAwakGledgerEffect(string Tran_Type, int Doc_No, int Company_Code, int Year_Code)
    {
        try
        {
            ds = new DataSet();
            string tblPrefix = Prefix();
            //DataSet dsDelete = new DataSet();
            //dsDelete = clsDAL.SimpleQuery("delete from " + tblPrefix + "GLEDGER where TRAN_TYPE='AW' AND DOC_NO=" + Doc_No + " AND COMPANY_CODE=" + Company_Code + "");

            MyDs = new DataSet();
            MyDs = clsDAL.SimpleQuery("Select * from qryAwakBalance where doc_no=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code);

            if (MyDs.Tables[0].Rows.Count > 0)
            {
                MyDoc_No = MyDs.Tables[0].Rows[0]["doc_no"].ToString();
                MyDoc_Date = DateTime.Parse(MyDs.Tables[0].Rows[0]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                MyDrCrHead = MyDs.Tables[0].Rows[0]["AC_CODE"].ToString();
                double Addless = MyDs.Tables[0].Rows[0]["addless"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["addless"].ToString()) : 0;
                string cashcredit = MyDs.Tables[0].Rows[0]["CASHCREDIT"].ToString();
                double Bill_Amount = MyDs.Tables[0].Rows[0]["AMOUNT"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["AMOUNT"].ToString()) : 0.00;
                double supercost = MyDs.Tables[0].Rows[0]["supercost"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["supercost"].ToString()) : 0.00;
                double shubamt = MyDs.Tables[0].Rows[0]["Shub_Amnt"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["kharajat"].ToString()) : 0.00;
                double kharajat = MyDs.Tables[0].Rows[0]["kharajat"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["kharajat"].ToString()) : 0.00;
                double levi = MyDs.Tables[0].Rows[0]["levi"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["levi"].ToString()) : 0.00;
                double adat = MyDs.Tables[0].Rows[0]["adat"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["adat"].ToString()) : 0.00;
                double purchasevalue = MyDs.Tables[0].Rows[0]["Purc_Amnt"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Purc_Amnt"].ToString()) : 0.00;
                double cess = MyDs.Tables[0].Rows[0]["Cess"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["Cess"].ToString()) : 0.00;
                double tdsamt = MyDs.Tables[0].Rows[0]["tdsamount"].ToString() != string.Empty ? Convert.ToDouble(MyDs.Tables[0].Rows[0]["tdsamount"].ToString()) : 0.00;
                double supplieramt = Addless + supercost + kharajat + levi + adat + cess;

                MyOrderCode = 1;

                MyGL = new DataSet();
                MyGL = clsDAL.SimpleQuery("Select * from " + tblPrefix + "GLEDGER where TRAN_TYPE='" + Tran_Type + "' and DOC_NO=" + Doc_No + " and COMPANY_CODE=" + Company_Code + " and YEAR_CODE=" + Year_Code + "");
                mygledgercount = MyGL.Tables[0].Rows.Count;
                totalcount = 2;

                string KharajatAc = clsCommon.getString("select Kharajat_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);
                string shubamtAc = clsCommon.getString("select Shub_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);
                string Jagagry_Sale_Ac = clsCommon.getString("select Jagagry_Sale_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);

                double Jagagry_Sale_ac_Amt = Bill_Amount - (kharajat + shubamt);
                string str = clsCommon.getString("select Tds_Ac from NT_1_CompanyParameters where  Company_Code=" + Company_Code);

                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    //For supllier credit
                    string rev = "";
                    MyAmount = Bill_Amount;
                    //if (mygledgercount >= MyOrderCode)
                    //{
                    //    if (MyAmount != 0)
                    //    {
                    //        obj.flag = 1;
                    //        obj.tableName = tblPrefix + "GLEDGER";
                    //        obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                    //        obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + cashcredit + "','" + MyDoc_Date + "','" + str + "','Supplier Credit','" + Math.Abs(MyAmount) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','0','" + Tran_Type + "','" + Doc_No + "'";
                    //        ds = obj.insertAccountMaster(ref strRef);
                    //        rev = strRef;
                    //        MyOrderCode = MyOrderCode + 1;
                    //    }

                    //}
                    //For supllier debit
                    if (mygledgercount >= MyOrderCode)
                    {
                        if (MyAmount != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + cashcredit + "','" + MyDoc_Date + "','" + MyDrCrHead + "','Supplier Debit without TDS','" + Math.Abs(supplieramt) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','0','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                        if (str != string.Empty)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + cashcredit + "','" + MyDoc_Date + "','" + str + "','TDS amount Credit For Ac ','" + Math.Abs(tdsamt) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','C','0','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                        if (str != string.Empty)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,DOC_NO,CASHCREDIT,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'" + Tran_Type + "','" + MyDoc_No + "','" + cashcredit + "','" + MyDoc_Date + "','" + MyDrCrHead + "','TDS amount Debit For Supplier','" + Math.Abs(tdsamt) + "','" + Company_Code + "','" + Year_Code + "','" + Convert.ToInt32(System.Web.HttpContext.Current.Session["Branch_Code"].ToString()) + "','" + MyOrderCode + "','D','0','" + Tran_Type + "','" + Doc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                        if (Jagagry_Sale_ac_Amt != 0)
                        {

                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            //obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD";
                            obj.values = "'JS','" + cashcredit + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + Jagagry_Sale_Ac + "','Credit','" + Math.Abs(Jagagry_Sale_ac_Amt) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','D','" + Jagagry_Sale_Ac + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;

                        }
                        if (kharajat != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'JS','" + cashcredit + "','" + MyOrderCode + "','" + MyDoc_Date + "','" + KharajatAc + "','Jawak Credit','" + Math.Abs(kharajat) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','D','" + KharajatAc + "','AW','" + MyDoc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }
                        if (shubamt != 0)
                        {
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "GLEDGER";
                            obj.columnNm = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,Branch_Code,ORDER_CODE,DRCR,DRCR_HEAD,SORT_TYPE,SORT_NO";
                            obj.values = "'JS','" + cashcredit + "','" + MyDoc_No + "','" + MyDoc_Date + "','" + shubamtAc + "','Jawak Credit','" + Math.Abs(shubamt) + "','" + Company_Code + "','" + Year_Code + "','" + MyOrderCode + "','D','" + shubamtAc + "','AW','" + MyDoc_No + "'";
                            ds = obj.insertAccountMaster(ref strRef);
                            rev = strRef;
                            MyOrderCode = MyOrderCode + 1;
                        }

                    }
                    //For supllier tds amt 


                }

            }
        }
        catch (Exception ex)
        {
            clsNoToWord n = new clsNoToWord();
            n.WriteToFile(ex.ToString());
            throw;
        }
    }


}