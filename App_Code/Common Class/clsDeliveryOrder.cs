using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for clsDeliveryOrder
/// </summary>
public static class clsDeliveryOrder
{	
    public static DataTable tenderDetails(string millCode)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "sp_helptext 'dbo.qryAccountmaster'";
            ds = clsDAL.SimpleQuery(qry);

            string acMasterView = string.Empty;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                acMasterView = acMasterView + " " + ds.Tables[0].Rows[i][0].ToString();
            }
            acMasterView = acMasterView.Replace("qryAccountmaster", "qryAccountmaster_104");
            
            ds = clsDAL.SimpleQuery(acMasterView);

            qry = "sp_helptext 'dbo.qryTender'";
            ds = clsDAL.SimpleQuery(qry);
            qry = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                qry = qry + " " + ds.Tables[0].Rows[i][0].ToString();
            }

            string tblTender = "Tender.";
            string tblTenderDetails = "TenderDetails.";
            string vw_qryAccountmaster ="qryAccountmaster_104";
            string vw_tender = "qryTender_104";

            qry = qry.Replace("qryTender", vw_tender);
            qry=qry.Replace("Tender.",tblTender);
            qry = qry.Replace("TenderDetails.", tblTenderDetails);
            qry = qry.Replace("qryAccountmaster", vw_qryAccountmaster);
            ds = clsDAL.SimpleQuery(qry);

            qry = string.Empty;
            qry = "select * from " + vw_tender + " where Mill_Code='" + millCode + "'";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }


            DataTable dt1 = new DataTable();
            dt1 = dt;
            qry = string.Empty;
            qry = "drop view " + vw_tender+" ";
            ds = clsDAL.SimpleQuery(qry);
            qry = string.Empty;
            qry = "drop view " + vw_qryAccountmaster+" ";
            ds = clsDAL.SimpleQuery(qry);
            return dt1;
           
        }
        catch
        {
            return null;
        }
    }
}



//             qry = "SELECT     dbo.Tender.Tender_No, Convert(varchar(10),dbo.Tender.Tender_Date,103)as Tender_Date, Convert(varchar(10),dbo.Tender.Lifting_Date,103) as Lifting_Date, dbo.Tender.Mill_Code, rsmill.Ac_Name_E AS millname, rsmill.city_name_e AS millcity,  "
//+ " dbo.Tender.Grade, dbo.Tender.Quantal, dbo.Tender.Packing, dbo.Tender.Bags, dbo.Tender.Payment_To, dbo.Tender.Tender_From, dbo.Tender.Tender_DO, "
//+" dbo.Tender.Voucher_By, dbo.Tender.Broker, dbo.Tender.Excise_Rate, dbo.Tender.Narration, rsbuyer.Ac_Name_E AS buyername, rsbuyer.city_name_e AS buyercity,  "
//+" rsbuyerparty.Ac_Name_E AS buyerpartyname, rsbuyerparty.city_name_e AS buyerpartyCity, dbo.TenderDetails.Buyer, dbo.TenderDetails.Buyer_Quantal, "
//+" dbo.TenderDetails.Sale_Rate, dbo.TenderDetails.Commission_Rate, dbo.TenderDetails.Narration AS buyernarration, dbo.TenderDetails.ID AS buyerid "
//+" FROM  dbo.Tender INNER JOIN "
//+" dbo.TenderDetails ON dbo.Tender.Tender_No = dbo.TenderDetails.Tender_No AND dbo.Tender.Company_Code = dbo.TenderDetails.Company_Code INNER JOIN "
//+" dbo.qryAccountmaster AS rsmill ON dbo.Tender.Company_Code = rsmill.Company_Code AND dbo.Tender.Mill_Code = rsmill.Ac_Code INNER JOIN "
//+" dbo.qryAccountmaster AS rsbuyer ON dbo.TenderDetails.Buyer = rsmill.Ac_Code AND dbo.TenderDetails.Company_Code = rsmill.Company_Code INNER JOIN "
//+" dbo.qryAccountmaster AS rsbuyerparty ON dbo.TenderDetails.Buyer_Party = rsmill.Ac_Code AND dbo.TenderDetails.Company_Code = rsmill.Company_Code "
//+" where dbo.Tender.Mill_Code='" +millCode+ "' ";
