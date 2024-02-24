using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Globalization;

public partial class Report_rptDispatchDetails : System.Web.UI.Page
{
    string Mill_Code = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string Lot_No = string.Empty;
    string Sr_No = string.Empty;
    string qry = string.Empty;
    string Tender_No = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet databind;
    DataTable SecoundQry;
    DataTable selectedvalue;
    DataSet databindsecond;
    DataTable thirdqry;
    DataTable selectedvalue1;
    string isAuthenticate = string.Empty;
    string user = string.Empty;

    string tblPrefix = string.Empty;
    string f = "../GSReports/DispatchDetails_.htm";
    string f_Main = "../Report/rptDispatchDetails";
    string Branch_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        Mill_Code = Request.QueryString["Mill_Code"].ToString();
        fromDT = Request.QueryString["fromDT"].ToString();
        toDT = Request.QueryString["toDT"].ToString();
        Lot_No = Request.QueryString["Lot_No"].ToString();
        Sr_No = Request.QueryString["Sr_No"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                lblCompanyName.Text = Session["Company_Name"].ToString();
                this.BindData();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    private void BindData()
    {
        try
        {
            string fromdate = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy-MM-dd");
            string todate = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy-MM-dd");
            string Dispatched = "";


            //qry = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
            //            " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  order by ID";

            //databind = clsDAL.SimpleQuery(qry);
            //SecoundQry = databind.Tables[0];

            qry = " select doc_no as detail_id,doc_dateConverted as DI_Date,salebillname as Getpass,shiptoshortname as shiptoname,truck_no, " +
                " quantal as DI_Qty,doname as DI_DO,purc_no, purc_order,tenderdetailid from qrydohead where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "   and tran_type='DO' order by doc_date";
            databindsecond = clsDAL.SimpleQuery(qry);

            thirdqry = databindsecond.Tables[0];

            //if (Mill_Code != string.Empty)
            //{
            //    if (Mill_Code != string.Empty && Lot_No != string.Empty)
            //    {

            //        qry = "select ID,Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
            //                       " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
            //                       " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
            //                    + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
            //                       " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code + "  and Tender_No=" + Lot_No + " Order By  Tender_Date";
            //    }
            //    else
            //    {

            //        qry = "select ID,Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
            //                       " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
            //                       " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
            //                    + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
            //                       " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'  and Mill_Code=" + Mill_Code + "  Order By  Tender_Date";
            //    }
            //}
            //if (Mill_Code == string.Empty)
            //{
            //    if (Mill_Code == string.Empty && Lot_No == string.Empty)
            //    {
            //        qry = "select ID,Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
            //                           " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
            //                           " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
            //        + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
            //                           " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' Order By  Tender_Date";
            //    }

            //    if (Mill_Code == string.Empty && Lot_No != string.Empty)
            //    {
            //        if (Lot_No != string.Empty && Sr_No == string.Empty)
            //        {
            //            qry = "select ID,Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
            //                           " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
            //                           " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
            //                        + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
            //                           " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'  and Tender_No=" + Lot_No + " Order By  Tender_Date";
            //        }
            //        else
            //        {

            //        }
            //    }

            //}
            if (string.IsNullOrEmpty(Branch_Code))
            {
                if (Mill_Code != string.Empty)
                {
                    if (Mill_Code != string.Empty && Lot_No != string.Empty)
                    {
                        //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                        //        " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                        //        " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                        //        " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code" +
                        //        " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Mill_Code=" + Mill_Code + " AND qt.Tender_No=" + Lot_No + " AND qt.Buyer=2 " +
                        //        " AND Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                      
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                        //             " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //             " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //          + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + 
                        //          " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
                        //             " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                        //             + Mill_Code + " and Tender_No=" + Lot_No + " Order By  Tender_Date";
                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                    " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                    " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                 + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                                 "  and Buyer=2 AND " +
                                    " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                                    + Mill_Code + " and Tender_No=" + Lot_No + " Order By  Tender_Date";


                      //  string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                      //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                      //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                      //               + Mill_Code + " and Tender_No=" + Lot_No;

                        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                     " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                     " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                                    + Mill_Code + " and Tender_No=" + Lot_No;

                        databind = clsDAL.SimpleQuery(qry1);
                        SecoundQry = databind.Tables[0];

                    }
                    else
                    {
                        //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                        //        " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                        //        " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                        //        " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                        //        " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Mill_Code=" + Mill_Code + " AND qt.Buyer=2 " +
                        //        " AND Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                        //           " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //           " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //        + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
                        //           " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                        //           + Mill_Code + " Order By  Tender_Date";

                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                             + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Buyer=2 AND " +
                                " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                                + Mill_Code + " Order By  Tender_Date";

                      //  string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                      //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                      //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                      //               + Mill_Code;
                        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                     " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code="
                                    + Mill_Code;

                        databind = clsDAL.SimpleQuery(qry1);
                        SecoundQry = databind.Tables[0];
                    }
                }
                if (Mill_Code == string.Empty)
                {
                    if (Mill_Code == string.Empty && Lot_No == string.Empty)
                    {
                        //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                        //                   " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                        //                   " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                        //                   " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                        //                   " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Buyer=2 AND " +
                        //                   " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                        //                " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //                " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //             + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
                        //                " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' Order By  Tender_Date";

                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                      " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                      " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                   + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Buyer=2 AND " +
                                      " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' Order By  Tender_Date";

                    //    string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                    //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";

                        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                    " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";

                        databind = clsDAL.SimpleQuery(qry1);
                        SecoundQry = databind.Tables[0];

                    }

                    if (Mill_Code == string.Empty && Lot_No != string.Empty)
                    {
                        if (Lot_No != string.Empty && Sr_No == string.Empty)
                        {
                            //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                            //    " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                            //    " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                            //    " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                            //    " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Tender_No=" + Lot_No + " AND qt.Buyer=2 " +
                            //    " AND Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                            //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                            //           " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                            //           " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                            //        + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and Buyer=2 AND " +
                            //           " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " Order By  Tender_Date";

                            qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                    " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                    " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                 + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Buyer=2 AND " +
                                    " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " Order By  Tender_Date";

                   //         string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                   //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                   //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No;
                            string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No from  qrytenderheaddetail " +
                   " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                   " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No;

                            databind = clsDAL.SimpleQuery(qry1);
                            SecoundQry = databind.Tables[0];

                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                if (Mill_Code != string.Empty)
                {
                    if (Mill_Code != string.Empty && Lot_No != string.Empty)
                    {
                        //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                        //        " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                        //        " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                        //        " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code" +
                        //        " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Branch_Code=" + Branch_Code + " and qt.Mill_Code=" + Mill_Code + " AND qt.Tender_No=" + Lot_No + " AND qt.Buyer=2 " +
                        //        " AND Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                        //              " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //              " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //           + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                        //              " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and Mill_Code=" + Mill_Code + " Order By  Tender_Date";
                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                     " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                     " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                  + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  AND " +
                                     " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and Mill_Code=" + Mill_Code + " Order By  Tender_Date";

                 //       string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
                 //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                 //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and Mill_Code=" + Mill_Code;

                        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
                " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and Mill_Code=" + Mill_Code;

                        databind = clsDAL.SimpleQuery(qry1);
                        SecoundQry = databind.Tables[0];
                    }
                    else
                    {
                        //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                        //        " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                        //        " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                        //        " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                        //        " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Branch_Code=" + Branch_Code + " and qt.Mill_Code=" + Mill_Code + " AND qt.Buyer=2 " +
                        //        " AND Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                        //              " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //              " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //           + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                        //              " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code + " Order By  Tender_Date";
                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                    " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                    " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                 + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  AND " +
                                    " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code + " Order By  Tender_Date";

                //        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
                //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code;

                        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
        " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
        " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Mill_Code=" + Mill_Code;

                        databind = clsDAL.SimpleQuery(qry1);
                        SecoundQry = databind.Tables[0];

                    }
                }
                if (Mill_Code == string.Empty)
                {
                    if (Mill_Code == string.Empty && Lot_No == string.Empty)
                    {
                        //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                        //                   " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                        //                   " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                        //                   " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                        //                   " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Branch_Code=" + Branch_Code + " and qt.Buyer=2 AND " +
                        //                   " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                        //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                        //             " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                        //             " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                        //          + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  AND " +
                        //             " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'  Order By  Tender_Date";

                        qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                    " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                    " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                 + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND " +
                                    " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'  Order By  Tender_Date";

               //         string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
               //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";

                        string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
              " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
              " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";

                        databind = clsDAL.SimpleQuery(qry1);
                        SecoundQry = databind.Tables[0];


                    }

                    if (Mill_Code == string.Empty && Lot_No != string.Empty)
                    {
                        if (Lot_No != string.Empty && Sr_No == string.Empty)
                        {
                            //qry = "select qt.ID,qt.Tender_No as Tender_No,CONVERT(varchar(10),qt.Tender_Date,103) as Tender_Date ,A.Ac_Name_E as Mill " +
                            //    " ,qt.Grade as Grade,qt.Quantal as Quantal,qt.Mill_Rate as Mill_Rate, " +
                            //    " CONVERT(varchar(10),qt.Lifting_Date,103) as Lifting_Date,B.Ac_Name_E as Tender_DO from " + tblPrefix + "qryTenderListReport qt " +
                            //    " left outer  join " + tblPrefix + "AccountMaster A on qt.Mill_Code=A.Ac_Code AND qt.Company_Code=A.Company_Code " +
                            //    " left outer  join " + tblPrefix + "AccountMaster B on qt.Tender_DO=B.Ac_Code AND qt.Company_Code=B.Company_Code where qt.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and qt.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and qt.Branch_Code=" + Branch_Code + " and qt.Tender_No=" + Lot_No + " AND qt.Buyer=2 " +
                            //    " AND Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "'";
                            //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                            //         " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                            //         " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                            //      + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND " +
                            //         " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + "  Order By  Tender_Date";
                            qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                    " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                    " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                 + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND " +
                                    " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + "  Order By  Tender_Date";


               //             string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
               //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No;

                            string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
            " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
             " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No;

                            databind = clsDAL.SimpleQuery(qry1);
                            SecoundQry = databind.Tables[0];

                        }
                        else
                        {
                            //qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                            //       " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                            //       " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                            //    + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " AND " +
                            //       " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and ID=" + Sr_No + "  Order By  Tender_Date";

                            qry = "select distinct Tender_No as Tender_No,Tender_DateConverted as Tender_Date ,millshortname as Mill " +
                                   " ,Grade as Grade,Quantal as Quantal,Mill_Rate as Mill_Rate, " +
                                   " Lifting_DateConverted as Lifting_Date,tenderdoname as Tender_DO from qrytenderheaddetail "
                                + "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND " +
                                   " Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and ID=" + Sr_No + "  Order By  Tender_Date";

              //              string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
              //" where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
              //" and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and ID=" + Sr_No;

                            string qry1 = "select  ID,Tender_DateConverted as Tender_Date,Buyer as BuyerCode,buyerpartyname as Buyer,Buyer_Quantal as Qty,Sale_Rate as Sale_Rate,tenderdoname as Tender_DO ,Tender_No,tenderdetailid from  qrytenderheaddetail " +
        " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
         " and Tender_Date BETWEEN '" + fromdate + "' AND '" + todate + "' and Tender_No=" + Lot_No + " and ID=" + Sr_No;

                            databind = clsDAL.SimpleQuery(qry1);
                            SecoundQry = databind.Tables[0];

                        }
                    }
                }
            }




            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);



            dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn disp = new DataColumn("Dispatched", typeof(double));
                    ds.Tables[0].Columns.Add(disp);

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string tenderid = ds.Tables[0].Rows[j]["Tender_No"].ToString();

                        Dispatched = clsCommon.getString("Select SUM(quantal) as TD_Dispatch from " + tblPrefix + "deliveryorder WHERE company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and purc_no=" + tenderid + " AND tran_type='DO'");

                        //string tdid = ds.Tables[0].Rows[j]["ID"].ToString();
                        double dispatc = Dispatched != string.Empty ? double.Parse(Dispatched) : 0;
                        ds.Tables[0].Rows[j]["Dispatched"] = dispatc;
                    }
                    dt = ds.Tables[0];
                    dtl.DataSource = dt;
                    dtl.DataBind();

                }
                else
                {
                    dtl.DataSource = null;
                    dtl.DataBind();
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            clsDAL.CloseConnection();
        }
    }
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataList dtlTenderDetails = (DataList)e.Item.FindControl("dtlTenderDetails");
        Label tenderno = (Label)e.Item.FindControl("lblTenderNo");
        Tender_No = tenderno.Text.ToString();
        DataSet ds = new DataSet();
        string tddetailsid = string.Empty;
        string td_dispatch = "";


        DataView view = new DataView(SecoundQry, "Tender_No='" + tenderno.Text + "'", "Tender_No", DataViewRowState.CurrentRows);
        selectedvalue = view.ToTable(true, "ID", "Tender_Date", "BuyerCode", "Buyer", "Qty", "Sale_Rate", "Tender_DO", "Tender_No");

        if (selectedvalue.Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ID", typeof(string));
            dt1.Columns.Add("BuyerCode", typeof(string));
            dt1.Columns.Add("Tender_Date", typeof(string));
            dt1.Columns.Add("Buyer", typeof(string));
            dt1.Columns.Add("Qty", typeof(string));
            dt1.Columns.Add("Sale_Rate", typeof(string));
            dt1.Columns.Add("Tender_DO", typeof(string));
            if (selectedvalue.Rows.Count > 0)
            {

                for (int i = 0; i < selectedvalue.Rows.Count; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["ID"] = selectedvalue.Rows[i]["ID"].ToString();
                    string BuyerCode = selectedvalue.Rows[i]["BuyerCode"].ToString();
                    string buyerCityName = clsCommon.getString("select cityname from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + BuyerCode + "");
                    dr["Buyer"] = selectedvalue.Rows[i]["Buyer"].ToString() + " , " + buyerCityName;
                    dr["Qty"] = selectedvalue.Rows[i]["Qty"].ToString();
                    dr["Sale_Rate"] = selectedvalue.Rows[i]["Sale_Rate"].ToString();
                    dr["Tender_DO"] = selectedvalue.Rows[i]["Tender_DO"].ToString();
                    dt1.Rows.Add(dr);
                }
                DataColumn dc = new DataColumn("TD_Dispatch", typeof(string));
                dt1.Columns.Add(dc);
                DataColumn td_bal = new DataColumn("TD_Bal", typeof(double));
                dt1.Columns.Add(td_bal);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    tddetailsid = dt1.Rows[j]["ID"].ToString();

                    td_dispatch = clsCommon.getString("Select isnull( SUM(quantal),0 ) as TD_Dispatch from " + tblPrefix + "deliveryorder WHERE company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no=" + Tender_No + " AND purc_order=" + tddetailsid + " AND tran_type='DO'");

                    dt1.Rows[j]["TD_Dispatch"] = td_dispatch;
                    string td_qty = dt1.Rows[j]["Qty"].ToString();
                    double dispqty = td_dispatch != string.Empty ? double.Parse(td_dispatch) : 0;
                    double tdqty = td_qty != string.Empty ? double.Parse(td_qty) : 0;
                    double bal = tdqty - dispqty;
                    dt1.Rows[j]["TD_Bal"] = bal;
                }
                Label lblTenderBalLeft = (Label)e.Item.FindControl("lblTenderBalLeft");
                lblTenderBalLeft.Text = Convert.ToString(dt1.Compute("SUM(TD_Bal)", string.Empty));

                dtlTenderDetails.DataSource = dt1;
                dtlTenderDetails.DataBind();
            }
        }
    }
    protected void dtlTenderDetails_ItemDatBound(object sender, DataListItemEventArgs e)
    {
        Label purc_order = (Label)e.Item.FindControl("lblTenderDetailNo");
        //Label lblTenderNo = (Label)dtl.FindControl("lblTenderNo");
        DataList dtlDispatch = (DataList)e.Item.FindControl("dtlDispatch");
        string Purc_Order = purc_order.Text.ToString();
        string Purc_No = Tender_No;


        DataView view = new DataView(thirdqry, "purc_no='" + Tender_No + "'", "purc_no", DataViewRowState.CurrentRows);
        view.RowFilter = "purc_no=" + Tender_No + " and purc_order=" + Purc_Order;
        selectedvalue1 = view.ToTable(true, "detail_id", "DI_Date", "Getpass", "truck_no", "DI_Qty", "DI_DO", "purc_no", "purc_order", "shiptoname");


        //ds = new DataSet();
        //ds = clsDAL.SimpleQuery(qry);

        if (selectedvalue1.Rows.Count > 0)
        {
            dt = new DataTable();
            //dt = ds.Tables[0];
            dtlDispatch.DataSource = selectedvalue1;
            dtlDispatch.DataBind();
        }
        else
        {
            dtlDispatch.DataSource = null;
            dtlDispatch.DataBind();
        }

    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string email = txtEmail.Text;
            if (txtEmail.Text != string.Empty)
            {
                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);
                pnlMain.RenderControl(tw);
                string s = sw.ToString();

                try
                {
                    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(s);
                        }
                    }
                }
                catch (Exception)
                {
                    f = f_Main + ".htm";
                    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(s);
                        }
                    }
                }

                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(email);
                msg.Body = "Dispatch Details";
                msg.Attachments.Add(new Attachment(Server.MapPath(f)));
                msg.IsBodyHtml = true;
                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                msg.Subject = "Dispatch Details Report" + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
                msg.IsBodyHtml = true;
                if (smtpPort != string.Empty)
                {
                    SmtpServer.Port = Convert.ToInt32(smtpPort);
                }
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                SmtpServer.Send(msg);
            }
        }
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");

    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        pnlMain.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        string strFile = "report.xls";
        string strcontentType = "application/excel";
        Response.ClearContent();
        Response.ClearHeaders();
        Response.BufferOutput = true;
        Response.ContentType = strcontentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        Response.Write(StrExport.ToString());
        Response.Flush();
        Response.Close();
        Response.End();
    }
}
