using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net;
using System.IO;
using System.Text;


/// <summary>
/// Summary description for clsCommon
/// </summary>
public static class clsCommon
{
    public static DataSet ds = null;
    public static DataTable dt = null;

    public static string getString(string qry)
    {
        try 
        {
           // using (clsDataProvider objDataProvider = new clsDataProvider())
            using (clsDAL objDataProvider = new clsDAL())

            {
                string retString = string.Empty;
                //ds = new DataSet();
                //dt = new DataTable();
                retString = objDataProvider.Get_Single_FieldValue_fromDR(qry);
                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        dt = ds.Tables[0];
                //        if (dt.Rows.Count > 0)
                //        {
                //            retString = dt.Rows[0][0].ToString();
                //        }
                //    }
                //}

                if (retString == string.Empty)
                {
                    retString = "0";
                }
                return retString;
            }
        }
        catch(Exception ex)
        {
            //DOPurcSaleCRUD.LogError(ex);
            return string.Empty;
        }
        finally
        {
            //ds.Dispose();
            //dt.Dispose();
        }
    }

    public static bool isStringIsNumeric(string strValue)
    {
        int iValue = 0;
        bool canConvert = int.TryParse(strValue, out iValue);
        if (int.TryParse(strValue, out iValue))
            return true;
        else
            return false;
    }



    #region old view code
    //public static void generateView(string companyCode)
    //{
    //    try
    //    {

    //        #region ---view qryTenderPurchaseView ------------------
    //        string tblPrefix=HttpContext.Current.Session["tblPrefix"].ToString();

    //        DataSet ds = new DataSet();
    //        DataTable dt = new DataTable();
    //        string qry = "sp_helptext 'dbo.qryTenderPurchaseFetchRecord'";
    //        ds = clsDAL.SimpleQuery(qry);

    //        string strtenderPurchaseView = string.Empty;
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            strtenderPurchaseView = strtenderPurchaseView + " " + ds.Tables[0].Rows[i][0].ToString();
    //        }
    //        strtenderPurchaseView = strtenderPurchaseView.Replace("qryTenderPurchaseFetchRecord", tblPrefix+"qryTenderPurchaseFetchRecord");

    //        /// check whether view is already exist in database or not
    //        string viewName = clsCommon.getString("select name FROM sys.views where name = '"+ tblPrefix+"qryTenderPurchaseFetchRecord"+"' ");
    //        if (viewName == string.Empty)
    //        {
    //            //view does not exist
    //            //create new
    //            strtenderPurchaseView = strtenderPurchaseView.Replace("Tender",tblPrefix+"Tender");
    //            strtenderPurchaseView = strtenderPurchaseView.Replace(tblPrefix+"Tender_", "Tender_");
    //            strtenderPurchaseView = strtenderPurchaseView.Replace("qry"+tblPrefix, "qry");
    //            strtenderPurchaseView = strtenderPurchaseView.Replace(tblPrefix + "TenderFromName", "TenderFromName");
    //            strtenderPurchaseView = strtenderPurchaseView.Replace(tblPrefix + "TenderDOName", "TenderDOName");
    //            strtenderPurchaseView = strtenderPurchaseView.Replace("AccountMaster", tblPrefix+ "AccountMaster");


    //            qry = strtenderPurchaseView;
    //            ds = clsDAL.SimpleQuery(qry);
    //        }
    //        else
    //        {
    //            //view already exist
    //        }
    //        #endregion

    //        #region----- View qryTenderPurchase Fetch Record [dbo.qryTenderDetailsFetchRecord]-------------------

    //        qry = "sp_helptext 'dbo.qryTenderDetailsFetchRecord'";
    //        ds = clsDAL.SimpleQuery(qry);

    //        string strtenderDetailsView = string.Empty;
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            strtenderDetailsView = strtenderDetailsView + " " + ds.Tables[0].Rows[i][0].ToString();
    //        }
    //        strtenderDetailsView = strtenderDetailsView.Replace("qryTenderDetailsFetchRecord", tblPrefix+ "qryTenderDetailsFetchRecord");

    //        viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryTenderDetailsFetchRecord" + "' ");
    //        if (viewName == string.Empty)
    //        {
    //            strtenderDetailsView = strtenderDetailsView.Replace("TenderDetails", tblPrefix + "TenderDetails");
    //            strtenderDetailsView = strtenderDetailsView.Replace("qry" + tblPrefix, "qry");
    //            strtenderDetailsView = strtenderDetailsView.Replace("dbo."+tblPrefix + "TenderDetails.","");
    //            strtenderDetailsView = strtenderDetailsView.Replace("AccountMaster", tblPrefix + "AccountMaster");
    //            strtenderDetailsView = strtenderDetailsView.Replace("Company_Code", tblPrefix + "TenderDetails.Company_Code");
    //            qry = strtenderDetailsView;
    //            ds = clsDAL.SimpleQuery(qry);
    //        }
    //        else
    //        {
    //            //view exist
    //        }
    //        #endregion

    //        #region----- View qryTenderList Fetch Record [dbo.qryTenderDetailsFetchRecord]-------------------

    //        //qry = "sp_helptext 'dbo.qryTenderList'";
    //        //ds = clsDAL.SimpleQuery(qry);

    //        //string strTenderListView = string.Empty;
    //        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        //{
    //        //    strTenderListView = strTenderListView + " " + ds.Tables[0].Rows[i][0].ToString();
    //        //}
    //        //strTenderListView = strTenderListView.Replace("qryTenderList", tblPrefix + "qryTenderList");

    //        //viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryTenderList" + "' ");
    //        //if (viewName == string.Empty)
    //        //{
    //        //    strTenderListView = strTenderListView.Replace("Tender", tblPrefix + "Tender");
    //        //    strTenderListView = strTenderListView.Replace("qry" + tblPrefix, "qry");
    //        //    strTenderListView = strTenderListView.Replace("dbo." + tblPrefix + "TenderDetails.", "");
    //        //    strTenderListView = strTenderListView.Replace("AccountMaster", tblPrefix + "AccountMaster");
    //        //    strTenderListView = strTenderListView.Replace("Company_Code", tblPrefix + "TenderDetails.Company_Code");
    //        //    qry = strTenderListView;
    //        //    ds = clsDAL.SimpleQuery(qry);
    //        //}
    //        //else
    //        //{
    //        //    //view exist
    //        //}
    //        #endregion

    //        #region -----------------View dbo.qryTenderView ------------------------

    //        qry = "sp_helptext 'dbo.qryTenderView'";
    //        ds = clsDAL.SimpleQuery(qry);

    //        string strtendereView = string.Empty;
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            strtendereView = strtendereView + " " + ds.Tables[0].Rows[i][0].ToString();
    //        }
    //        strtendereView = strtendereView.Replace("qryTenderView", tblPrefix + "qryTenderView");

    //        /// check whether view is already exist in database or not
    //        viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryTenderView" + "' ");
    //        if (viewName == string.Empty)
    //        {
    //            //view does not exist
    //            //create new
    //            strtendereView = strtendereView.Replace("Tender", tblPrefix + "Tender");
    //            strtendereView = strtendereView.Replace(tblPrefix + "Tender_", "Tender_");
    //            strtendereView = strtendereView.Replace("qry" + tblPrefix, "qry");
    //            strtendereView = strtendereView.Replace(tblPrefix + "TenderFromName", "TenderFromName");
    //            strtendereView = strtendereView.Replace(tblPrefix + "TenderDOName", "TenderDOName");
    //            strtendereView = strtendereView.Replace("AccountMaster", tblPrefix + "AccountMaster");


    //            qry = strtendereView;
    //            ds = clsDAL.SimpleQuery(qry);
    //        }
    //        else
    //        {
    //            //view already exist
    //        }


    //        #endregion

    //    }
    //    catch
    //    {

    //    }
    //}
    #endregion

    public static void generateView(string companyCode)
    {
        try
        {
            string tblPrefix = HttpContext.Current.Session["tblPrefix"].ToString();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            #region [qryTenderList]

            string qry = "sp_helptext 'dbo.qryTenderList'";
            ds = clsDAL.SimpleQuery(qry);

            string strTenderView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strTenderView = strTenderView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strTenderView = strTenderView.Replace("qryTenderList", tblPrefix + "qryTenderList");
            //  check whether view is already exist in database or not
            string viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryTenderList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist
                //create new  MNC_15_qryMNC_15_TenderList
                strTenderView = strTenderView.Replace("Tender", tblPrefix + "Tender");
                strTenderView = strTenderView.Replace("TenderDetails", tblPrefix + "TenderDetails");
                strTenderView = strTenderView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strTenderView = strTenderView.Replace(tblPrefix + "Tender_", "Tender_");
                strTenderView = strTenderView.Replace("qry" + tblPrefix + "TenderList", "qryTenderList");
                strTenderView = strTenderView.Replace(tblPrefix + tblPrefix, tblPrefix);
                qry = strTenderView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region [dbo.qryTendermillbalancepayment]
            qry = "sp_helptext 'dbo.qryTendermillbalancepayment'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            string strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryTendermillbalancepayment", tblPrefix + "qryTendermillbalancepayment");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryTendermillbalancepayment" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist
                //create new  MNC_15_qryMNC_15_TenderList
                strView = strView.Replace("Tender ", tblPrefix + "Tender ");
                strView = strView.Replace("Tender.", tblPrefix + "Tender.");
                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("GLEDGER", tblPrefix + "GLEDGER");
                strView = strView.Replace("CityMaster", tblPrefix + "CityMaster");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }

            #endregion

            #region qryVoucherList
            qry = "sp_helptext 'dbo.qryVoucherList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryVoucherList", tblPrefix + "qryVoucherList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryVoucherList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("VoucherDetails ", tblPrefix + "VoucherDetails ");
                strView = strView.Replace("VoucherDetails.", tblPrefix + "VoucherDetails.");
                strView = strView.Replace("Voucher ", tblPrefix + "Voucher ");
                strView = strView.Replace("Voucher.", tblPrefix + "Voucher.");
                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qryAccountsList
            qry = "sp_helptext 'dbo.qryAccountsList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryAccountsList", tblPrefix + "qryAccountsList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryAccountsList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("qryAccountsList ", tblPrefix + "qryAccountsList ");
                strView = strView.Replace("AcContacts", tblPrefix + "AcContacts");
                strView = strView.Replace("CityMaster", tblPrefix + "CityMaster");
                strView = strView.Replace("BSGroupMaster", tblPrefix + "BSGroupMaster");
                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qryDeliveryOrderList
            qry = "sp_helptext 'dbo.qryDeliveryOrderList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryDeliveryOrderList", tblPrefix + "qryDeliveryOrderList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryDeliveryOrderList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("deliveryOrder", tblPrefix + "deliveryOrder");
                strView = strView.Replace("DODetails", tblPrefix + "DODetails");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qrySugarSaleList
            qry = "sp_helptext 'dbo.qrySugarSaleList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qrySugarSaleList", tblPrefix + "qrySugarSaleList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qrySugarSaleList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("CityMaster", tblPrefix + "CityMaster");
                strView = strView.Replace("sugarsaleDetails", tblPrefix + "sugarsaleDetails");
                strView = strView.Replace("SugarSale", tblPrefix + "SugarSale");
                strView = strView.Replace(tblPrefix + tblPrefix, tblPrefix);
                strView = strView.Replace("qry" + tblPrefix, "qry");
                strView = strView.Replace("SystemMaster", tblPrefix + "SystemMaster");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion


            #region qryPartyUnitlist
            qry = "sp_helptext 'dbo.qryPartyUnitlist'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryPartyUnitlist", tblPrefix + "qryPartyUnitlist");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryPartyUnitlist" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("qryAccountsList", tblPrefix + "qryAccountsList");
                strView = strView.Replace("PartyUnit", tblPrefix + "PartyUnit");
                strView = strView.Replace("CityMaster", tblPrefix + "CityMaster");
                strView = strView.Replace("qry" + tblPrefix, "qry");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qryCarporateSaleList
            qry = "sp_helptext 'dbo.qryCarporateSaleList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryCarporateSaleList", tblPrefix + "qryCarporateSaleList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryCarporateSaleList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("qryPartyUnitlist", tblPrefix + "qryPartyUnitlist");
                strView = strView.Replace("CarporateSale", tblPrefix + "CarporateSale");
                strView = strView.Replace("qry" + tblPrefix, "qry");
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qrySugarPurcList
            qry = "sp_helptext 'dbo.qrySugarPurcList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qrySugarPurcList", tblPrefix + "qrySugarPurcList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qrySugarPurcList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("SugarPurchase", tblPrefix + "SugarPurchase");
                strView = strView.Replace("SugarPurchaseDetails", tblPrefix + "SugarPurchaseDetails");

                strView = strView.Replace("qryAccountsList", tblPrefix + "qryAccountsList");
                strView = strView.Replace("SystemMaster", tblPrefix + "SystemMaster");
                strView = strView.Replace(tblPrefix + tblPrefix, tblPrefix);
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qryTransactList
            qry = "sp_helptext 'dbo.qryTransactList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryTransactList", tblPrefix + "qryTransactList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryTransactList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("Transact", tblPrefix + "Transact");
                strView = strView.Replace("qry" + tblPrefix, "qry");
                strView = strView.Replace(tblPrefix + tblPrefix, tblPrefix);
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

            #region qryUTRList
            qry = "sp_helptext 'dbo.qryUTRList'";
            ds = new DataSet();
            dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);

            strView = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strView = strView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            strView = strView.Replace("qryUTRList", tblPrefix + "qryUTRList");

            viewName = clsCommon.getString("select name FROM sys.views where name = '" + tblPrefix + "qryUTRList" + "' ");
            if (viewName == string.Empty)
            {
                //view does not exist

                strView = strView.Replace("AccountMaster", tblPrefix + "AccountMaster");
                strView = strView.Replace("UTR", tblPrefix + "UTR");
                strView = strView.Replace("qry" + tblPrefix, "qry");
                strView = strView.Replace(tblPrefix + tblPrefix, tblPrefix);
                qry = strView;
                ds = clsDAL.SimpleQuery(qry);
            }
            else
            {
                //view exist
            }
            #endregion

        }
        catch
        {

        }
    }

    public static bool isValidDate(string dt1)
    {
        bool r = true;

        try
        {

            // DateTime Start_Date = DateTime.Parse(HttpContext.Current.Session["Start_Date"].ToString());
            // DateTime End_Date = DateTime.Parse(HttpContext.Current.Session["End_Date"].ToString());

            //  DateTime d1=DateTime.Parse(dt1);
            DateTime Start_Date = DateTime.Parse(HttpContext.Current.Session["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime End_Date = DateTime.Parse(HttpContext.Current.Session["End_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime d1 = DateTime.Parse(dt1, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            if (d1 >= Start_Date && d1 <= End_Date)
            {
                r = true;
            }
            else
            {
                r = false;
            }
            return r;
        }
        catch
        {
            r = false;
            return r;
        }
    }


    public static bool isValidDateforOp(string dt1)
    {
        bool r = true;

        try
        {

            // DateTime Start_Date = DateTime.Parse(HttpContext.Current.Session["Start_Date"].ToString());
            // DateTime End_Date = DateTime.Parse(HttpContext.Current.Session["End_Date"].ToString());

            //  DateTime d1=DateTime.Parse(dt1);
            DateTime Start_Date = DateTime.Parse(HttpContext.Current.Session["start_date_op"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime End_Date = DateTime.Parse(HttpContext.Current.Session["End_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime d1 = DateTime.Parse(dt1, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            if (d1 >= Start_Date && d1 <= End_Date)
            {
                r = true;
            }
            else
            {
                r = false;
            }
            return r;
        }
        catch
        {
            r = false;
            return r;
        }
    }

    public static string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }

    public static string DataTableToCSV(this DataTable datatable, char seperator)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < datatable.Columns.Count; i++)
        {
            sb.Append(datatable.Columns[i]);
            if (i < datatable.Columns.Count - 1)
                sb.Append(seperator);
        }
        sb.AppendLine();
        foreach (DataRow dr in datatable.Rows)
        {
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sb.Append(dr[i].ToString());

                if (i < datatable.Columns.Count - 1)
                    sb.Append(seperator);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    
}