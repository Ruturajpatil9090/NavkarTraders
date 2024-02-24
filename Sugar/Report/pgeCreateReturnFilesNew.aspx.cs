using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net.Mime;

public partial class Sugar_pgeCreateReturnFilesNew : System.Web.UI.Page
{
    string corportno = string.Empty;
    string doqry;
    string DO_No = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindContrydropdown();
        }
    }

    protected void btnCreateb2b_Click(object sender, EventArgs e)
    {
        string fromdt = txtFromDt.Text;
        string todt = txtToDt.Text;
        fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        try
        {
            #region comment

            //string qry = "select Ac_Code,PartyName,LTRIM(RTRIM(PartyGST)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(PartyStateCode,''),0) as PartyStateCode,CONVERT(NVARCHAR,newsbno) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
            //             "Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,PartyStateCode),2) +'-'+ LTRIM(RTRIM(PartyState))) as [Place Of Supply],'N' as [Reverse Charge],'Regular' as [Invoice Type]," +
            //             "'' as [E-Commerce GSTIN],5 as Rate,TaxableAmount as [Taxable Value],'' as [Cess Amount] from NT_1_qrySugarSaleForGSTReturn where doc_date>='2017-07-01' and doc_date between '"
            //             + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //             " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and newsbno!=0";

            //string qry1 = "select v.Ac_Code,a.Ac_Name_E as PartyName,LTRIM(RTRIM(a.Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(a.GSTStateCode,''),0) as PartyStateCode,'D'+CONVERT(NVARCHAR,v.Doc_No) as [Invoice Number],REPLACE(CONVERT(CHAR(11),v.Doc_Date, 106),' ','-') as [Invoice date]," +
            //                     " v.Voucher_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,a.GSTStateCode),2) +'-'+ LTRIM(RTRIM(a.GSTStateName))) as [Place Of Supply],'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],(CGSTRate+SGSTRate+IGSTRate) as Rate,v.TaxableAmount as [Taxable Value],'' as [Cess Amount]  from NT_1_Voucher v " +
            //                     "left outer join NT_1_qryAccountsList a on v.Company_Code=a.Company_Code and v.Ac_Code=a.Ac_Code where a.UnregisterGST=0 and v.Tran_Type='LV' and v.Voucher_Amount>0 and v.Doc_Date between '"
            //                     + fromdt + "' and '" + todt + "'  and v.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //                      " and v.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            //string qryRetailsSell = "select h.Party_Code as Ac_Code,p.Ac_Name_E as PartyName,LTRIM(RTRIM(p.Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(p.GSTStateCode,''),0) as PartyStateCode,'RS'+CONVERT(NVARCHAR,doc_no) as [Invoice Number]," +
            //                    " REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date],h.Grand_Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,p.GSTStateCode),2) +'-'+ LTRIM(RTRIM(p.GSTStateName))) as [Place Of Supply],'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN]," +
            //                    " 5 as Rate,h.Total as [Taxable Value],'' as [Cess Amount] from NT_1_RetailSaleHead h left outer join NT_1_qryAccountsList p on h.Party_Code=p.Ac_Code and h.Company_Code=p.Company_Code where h.doc_date>='2017-07-01' and h.doc_date between '" + fromdt + "'" +
            //                    " and '" + todt + "' and h.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and h.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and h.Tran_Type='CR' and p.UnregisterGST=0";

            //string qryRentbill = "select h.Customer_Code as Ac_Code,p.Ac_Name_E as PartyName,LTRIM(RTRIM(p.Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(p.GSTStateCode,''),0) as PartyStateCode,'Rent'+CONVERT(NVARCHAR,h.doc_no) as [Invoice Number]," +
            //                    " REPLACE(CONVERT(CHAR(11),Date, 106),' ','-') as [Invoice date],h.Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,p.GSTStateCode),2) +'-'+ LTRIM(RTRIM(p.GSTStateName))) as [Place Of Supply],'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN]," +
            //                    " g.Rate as Rate,h.Subtotal as [Taxable Value],'' as [Cess Amount] from NT_1_RentBillHead  h left outer join  dbo.NT_1_GSTRateMaster AS g ON h.GstRateCode = g.Doc_no AND h.Company_Code = g.Company_Code  LEFT OUTER JOIN" +
            //                    " NT_1_qryAccountsList p on h.Customer_Code=p.Ac_Code and h.Company_Code=p.Company_Code where h.Date>='2017-07-01' and h.Date between '" + fromdt + "'" +
            //                    " and '" + todt + "' and h.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and h.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and p.UnregisterGST=0  AND h.GstRateCode <> 3";
            #endregion

            //string qrySaleBill = "select Ac_Code,billtoname as PartyName,LTRIM(RTRIM(billtogstno)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(GSTStateCode,''),0) as PartyStateCode," +
            //                   " 'SB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
            //                   " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
            //                   " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],gstrate as Rate,TaxableAmount as [Taxable Value]," +
            //                   " '' as [Cess Amount] from qrysalehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'" +
            //                   " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and doc_no!=0";

            string qrySaleBill = "select (case when  isnull(Carporate_Sale_No,0)=0 then Ac_Code else carporate_ac end) as Ac_Code ," +
                                 " (case when  isnull(Carporate_Sale_No,0)=0 then PartyName else Ac_Name_E end) as PartyName," +
                                 " (case when  isnull(Carporate_Sale_No,0)=0 then [GSTIN/UIN of Recipient] else  LTRIM(RTRIM(Gst_No))end) as [GSTIN/UIN of Recipient]," +
                                 " (case when  isnull(Carporate_Sale_No,0)=0 then PartyStateCode else ISNULL(NULLIF (CA_GSTStateCode, ''),0) end) as PartyStateCode," +
                                 " 'SB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date], " +
                                 " [Invoice Value],(case when isnull(Carporate_Sale_No,0)=0 then [Place Of Supply] else RIGHT('0' + CONVERT(NVARCHAR,  CA_GSTStateCode), 2) +'-' + LTRIM(RTRIM(state_name)) end )as [Place Of Supply]," +
                                 " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],Rate,[Taxable Value], " +
                                 " '' as [Cess Amount] from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'" +
                                 " and IsDeleted!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and doc_no!=0";


            string qryCommissionBill = "select Ac_Code,Ac_Name_E as PartyName,LTRIM(RTRIM(Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(GSTStateCode,''),0) as PartyStateCode," +
                                     " 'LV'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
                                     " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                                     " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],Rate as Rate,texable_amount as [Taxable Value]," +
                                     " '' as [Cess Amount] from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' " +
                                     " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and doc_no!=0 and Bill_Amount!=0";

            string qryReatilSaleBill = "select Party_Code as Ac_Code,partyname as PartyName,LTRIM(RTRIM(PartyGst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(GSTStateCode,''),0) as PartyStateCode," +
                                 " 'RR'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
                                 " NetValue as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                                 " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],Gstrate as Rate,Taxable_Amount as [Taxable Value]," +
                                 " '' as [Cess Amount] from qryRetailSale where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  " +
                                 " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                                 + " and UnregisterGST=0 and doc_no!=0 and IsDelete!=0 GROUP BY Doc_No,Doc_Date, GSTStateCode, billtostatename,Gstrate,partyname,Party_Code,PartyGst_No,NetValue,Taxable_Amount";

            string qryReturnSaleBill = "select  FromAc as Ac_Code,FromAcName as PartyName,LTRIM(RTRIM(BillToGst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(BillToGSTStateCode,''),0) as PartyStateCode," +
                                     " 'RS'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
                                     " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,BillToGSTStateCode),2) +'-'+ LTRIM(RTRIM(BillToState))) as [Place Of Supply]," +
                                     " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],gstrate as Rate,subTotal as [Taxable Value]," +
                                     " '' as [Cess Amount] from qrysugarsalereturnhead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'" +
                                     " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and doc_no!=0 and IsDeleted!=0";

            string qryServiceBill = "select  Customer_Code as Ac_Code,Ac_Name_E as PartyName,LTRIM(RTRIM(Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(GSTStateCode,''),0) as PartyStateCode," +
                                  " 'RB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),Date, 106),' ','-') as [Invoice date]," +
                                  " Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                                  " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],gstrate as Rate,subTotal as [Taxable Value]," +
                                  " '' as [Cess Amount] from qryrentbillhead where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt + "'" +
                                  " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and IsDeleted!=0 and UnregisterGST=0 and doc_no!=0";

            string qryClodStorage = "select  Customer_Code as Ac_Code,Ac_Name_E as PartyName,LTRIM(RTRIM(Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(CustomerStateCode,''),0) as PartyStateCode," +
                                  " 'CB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),Date, 106),' ','-') as [Invoice date]," +
                                  " Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,CustomerStateCode),2) +'-'+ LTRIM(RTRIM(CustomerStateName))) as [Place Of Supply]," +
                                  " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],rate as Rate,subTotal as [Taxable Value]," +
                                  " '' as [Cess Amount] from qryColdStorageHead where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt + "'" +
                                  " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and doc_no!=0";

            string qryCreditnote = "select [Bill To ACC NO] as Ac_Code,BilltoName as PartyName,LTRIM(RTRIM([BILL TO GSTIN])) as [GSTIN/UIN of Recipient]," +
                  " ISNULL(NULLIF(BillToStateCode,''),0) as PartyStateCode, Inovice_No as [Invoice Number]," +
                  " REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date], bill_amount as [Invoice Value]," +
                  " (RIGHT('0'+CONVERT(NVARCHAR,BillToStateCode),2) +'-'+ LTRIM(RTRIM(shiptostatename))) as [Place Of Supply], " +
                  " 'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN],Rate as Rate,TaxableAmt as [Taxable Value], " +
                  " '' as [Cess Amount] from qryDebitcreditnote_GST where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'" +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=0 and IsDeleted !=0 and tran_type='DN'";


            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qrySaleBill);

            DataSet dsDNote = new DataSet();
            dsDNote = clsDAL.SimpleQuery(qryCommissionBill);

            DataSet dsRR = new DataSet();
            dsRR = clsDAL.SimpleQuery(qryReatilSaleBill);

            DataSet dsRS = new DataSet();
            dsRS = clsDAL.SimpleQuery(qryReturnSaleBill);

            DataSet dsRB = new DataSet();
            dsRB = clsDAL.SimpleQuery(qryServiceBill);

            DataSet dsCB = new DataSet();
            dsCB = clsDAL.SimpleQuery(qryClodStorage);
            DataSet dsCreditenote = new DataSet();
            dsCreditenote = clsDAL.SimpleQuery(qryCreditnote);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dsDNote.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsDNote.Tables[0], true, MissingSchemaAction.Ignore);
                    }

                    if (dsRR.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsRR.Tables[0], true, MissingSchemaAction.Ignore);
                    }

                    if (dsRS.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsRS.Tables[0], true, MissingSchemaAction.Ignore);
                    }

                    if (dsRB.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsRB.Tables[0], true, MissingSchemaAction.Ignore);
                    }

                    if (dsCB.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsCB.Tables[0], true, MissingSchemaAction.Ignore);
                    }
                    if (dsCreditenote.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsCreditenote.Tables[0], true, MissingSchemaAction.Ignore);
                    }
                    string fileName = "";
                    string strForCSV = "";

                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = "LEN([GSTIN/UIN of Recipient]) <> 15 ";

                    DataView dvWrongState = new DataView(dt);
                    dvWrongState.RowFilter = "PartyStateCode = 0";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongGSTNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 3;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 4)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "Ac_Code", "PartyName");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else if (dvWrongState.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongState.csv";
                        DataTable dtnew = dvWrongState.ToTable();

                        int colindex = 3;
                        for (int i = 0; i < dvWrongState.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 4)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "Ac_Code", "PartyName");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "b2b.csv";
                        dt.Columns.Remove("Ac_Code");
                        dt.Columns.Remove("PartyName");
                        dt.Columns.Remove("PartyStateCode");
                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCreateb2cl_Click(object sender, EventArgs e)
    {
        string fromdt = txtFromDt.Text;
        string todt = txtToDt.Text;
        fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        try
        {
            #region comment
            //string qry = "select CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
            //             " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,PartyStateCode),2) +'-'+ LTRIM(RTRIM(PartyState))) as [Place Of Supply]," +
            //             " 5 as Rate,TaxableAmount as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from NT_1_qryReportForB2CFile where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //             " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1 and Bill_Amount>250000";

            //string qry1 = "select 'D'+CONVERT(NVARCHAR,v.Doc_No) as [Invoice Number],REPLACE(CONVERT(CHAR(11),v.Doc_Date, 106),' ','-') as [Invoice date]," +
            //                     " v.Voucher_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,a.GSTStateCode),2) +'-'+ LTRIM(RTRIM(a.GSTStateName))) as [Place Of Supply],18 as Rate,v.TaxableAmount as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN]  from NT_1_Voucher v " +
            //                     " left outer join NT_1_qryAccountsList a on v.Company_Code=a.Company_Code and v.Ac_Code=a.Ac_Code where a.UnregisterGST=1 and v.Tran_Type='LV' and v.Voucher_Amount>250000 and v.Doc_Date between '" + fromdt + "' and '" + todt + "'  and v.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //                      " and v.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";

            //string qryRetailsSell = "select h.Party_Code as Ac_Code,p.Ac_Name_E as PartyName,LTRIM(RTRIM(p.Gst_No)) as [GSTIN/UIN of Recipient],ISNULL(NULLIF(p.GSTStateCode,''),0) as PartyStateCode,'RS'+CONVERT(NVARCHAR,doc_no) as [Invoice Number]," +
            //                    " REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date],h.Grand_Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,p.GSTStateCode),2) +'-'+ LTRIM(RTRIM(p.GSTStateName))) as [Place Of Supply],'N' as [Reverse Charge],'Regular' as [Invoice Type],'' as [E-Commerce GSTIN]," +
            //                    " 5 as Rate,h.Total as [Taxable Value],'' as [Cess Amount] from NT_1_RetailSaleHead h left outer join NT_1_qryAccountsList p on h.Party_Code=p.Ac_Code and h.Company_Code=p.Company_Code where h.doc_date>='2017-07-01' and h.doc_date between '" + fromdt + "'" +
            //                    " and '" + todt + "' and h.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and h.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and h.Tran_Type='CR' and p.UnregisterGST=1";

            #endregion

            //string qry = "select 'SB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," + 
            //            " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply],"+ 
            //            " gstrate as Rate,TaxableAmount as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrysalehead where "+
            //            " doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1" + 
            //            " and Bill_Amount>250000";

            string qry = "select 'SB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
                         " [Invoice Value],(case when isnull(Carporate_Sale_No,0)=0 then [Place Of Supply] else RIGHT('0' + CONVERT(NVARCHAR,  CA_GSTStateCode), 2) +'-' + LTRIM(RTRIM(state_name)) end )as [Place Of Supply]," +
                         " Rate,[Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrysaleheadfor_GSTReturn where " +
                         " doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and IsDeleted!=0  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) +
                         " and UnregisterGST=1 and [Invoice Value]>250000";


            string qry1 = "select 'LV'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date], " +
                        " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply], " +
                        " Rate as Rate,texable_amount as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrycommissionbill where " +
                        " doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1 " +
                        " and Bill_Amount>250000";

            string qry2 = "select 'RR'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date], " +
                         " sum(NetValue) as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply], " +
                         " Gstrate as Rate,sum(Taxable_Amount) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryRetailSale where " +
                         " doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1" +
                         " and NetValue>250000 and IsDelete!=0 group by doc_no,doc_date,GSTStateCode,billtostatename,Gstrate";

            string qry3 = "select 'RS'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date], " +
                        " Bill_Amount as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,BillToGSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply], " +
                        " gstrate as Rate,subTotal as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrysugarsalereturnhead where " +
                        " doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1" +
                        " and Bill_Amount>250000";

            string qry4 = "select 'RB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),Date, 106),' ','-') as [Invoice date], " +
                         " Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply], " +
                         " gstrate as Rate,subTotal as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryrentbillhead where " +
                         " Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and IsDeleted!=0 and UnregisterGST=1 " +
                         " and Total>250000";

            string qry5 = "select 'CB'+CONVERT(NVARCHAR,doc_no) as [Invoice Number],REPLACE(CONVERT(CHAR(11),Date, 106),' ','-') as [Invoice date], " +
                          " Total as [Invoice Value],(RIGHT('0'+CONVERT(NVARCHAR,CustomerStateCode),2) +'-'+ LTRIM(RTRIM(CustomerStateName))) as [Place Of Supply], " +
                          " rate as Rate,subTotal as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryColdStorageHead where " +
                          " Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1" +
                          " and Total>250000";
            string qry6 = " select Inovice_No as [Invoice Number],REPLACE(CONVERT(CHAR(11),doc_date, 106),' ','-') as [Invoice date]," +
                           " bill_amount as [Invoice Value]," +
                          " (RIGHT('0'+CONVERT(NVARCHAR,BillToStateCode),2) +'-'+ LTRIM(RTRIM([Bill To ACC NO]))) as [Place Of Supply], " +
                          "  rate as Rate,TaxableAmt as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryDebitcreditnote_GST where " +
                        " doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1" +
                        " and bill_amount>250000 and tran_type='DN'";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            DataSet dsDNote = new DataSet();
            dsDNote = clsDAL.SimpleQuery(qry1);

            DataSet dsRR = new DataSet();
            dsRR = clsDAL.SimpleQuery(qry2);

            DataSet dsRS = new DataSet();
            dsRS = clsDAL.SimpleQuery(qry3);

            DataSet dsRB = new DataSet();
            dsRB = clsDAL.SimpleQuery(qry4);

            DataSet dsCB = new DataSet();
            dsCB = clsDAL.SimpleQuery(qry5);


            DataSet dsCredit = new DataSet();
            dsCredit = clsDAL.SimpleQuery(qry6);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dsDNote.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsDNote.Tables[0]);
                    }
                    if (dsRR.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsRR.Tables[0]);
                    }
                    if (dsRS.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsRS.Tables[0]);
                    }
                    if (dsRB.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsRB.Tables[0]);
                    }
                    if (dsCB.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsCB.Tables[0]);
                    }
                    if (dsCredit.Tables[0].Rows.Count > 0)
                    {
                        dt.Merge(dsCredit.Tables[0]);
                    }



                    string fileName = "";
                    string strForCSV = "";

                    //DataView dvWrongGst = new DataView(dt);
                    //dvWrongGst.RowFilter = "LEN([GSTIN/UIN of Recipient]) <> 15 ";

                    //DataView dvWrongState = new DataView(dt);
                    //dvWrongState.RowFilter = "PartyStateCode = 0";

                    //if (dvWrongGst.ToTable().Rows.Count > 0)
                    //{
                    //    fileName = "WrongGSTNumbers.csv";
                    //    DataTable dtnew = dvWrongGst.ToTable();

                    //    int colindex = 3;
                    //    for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                    //    {
                    //        dtnew.Columns.RemoveAt(colindex);
                    //        if (dtnew.Columns.Count < 4)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //    dtnew = dtnew.DefaultView.ToTable(true, "Ac_Code", "PartyName");
                    //    strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    //}
                    //if (dvWrongState.ToTable().Rows.Count > 0)
                    //{
                    //    fileName = "WrongState.csv";
                    //    DataTable dtnew = dvWrongState.ToTable();

                    //    int colindex = 3;
                    //    for (int i = 0; i < dvWrongState.ToTable().Columns.Count; i++)
                    //    {
                    //        dtnew.Columns.RemoveAt(colindex);
                    //        if (dtnew.Columns.Count < 4)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //    dtnew = dtnew.DefaultView.ToTable(true, "Ac_Code", "PartyName");
                    //    strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    //}
                    //else
                    //{
                    fileName = "b2cl.csv";
                    //dt.Columns.Remove("Ac_Code");
                    //dt.Columns.Remove("PartyName");
                    //dt.Columns.Remove("PartyStateCode");
                    strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    //}
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCreateb2cs_Click(object sender, EventArgs e)
    {
        string fromdt = txtFromDt.Text;
        string todt = txtToDt.Text;
        fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        try
        {
            #region comment
            //string qry = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,PartyStateCode),2) +'-'+ LTRIM(RTRIM(PartyState))) as [Place Of Supply]," +
            //             " 5 as Rate,TaxableAmount as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from NT_1_qryReportForB2CFile where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //             " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1 and Bill_Amount<=250000";

            //string qry = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,PartyStateCode),2) +'-'+ LTRIM(RTRIM(PartyState))) as [Place Of Supply]," +
            //           " 5 as Rate,sum(TaxableAmount) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from NT_1_qryReportForB2CFile where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //           " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1 and Bill_Amount<=250000 group by PartyStateCode,PartyState";


            //string qry1 = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,a.GSTStateCode),2) +'-'+ LTRIM(RTRIM(a.GSTStateName))) as [Place Of Supply],18 as Rate,v.TaxableAmount as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN]  from NT_1_Voucher v " +
            //            " left outer join NT_1_qryAccountsList a on v.Company_Code=a.Company_Code and v.Ac_Code=a.Ac_Code where a.UnregisterGST=1 and v.Tran_Type='LV' and v.Voucher_Amount<=250000 and v.Doc_Date between '" + fromdt + "' and '" + todt + "'  and v.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
            //            " and v.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
            #endregion

            //string qry = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply],"+
            //             " gstrate as Rate,sum(TaxableAmount) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrysalehead "+
            //             " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
            //             " UnregisterGST=1 and Bill_Amount<=250000 group by GSTStateCode,billtostatename,gstrate";
            string qry = "select 'OE' as Type,(case when Carporate_Sale_No=0 then [Place Of Supply] else RIGHT('0' + CONVERT(NVARCHAR,  CA_GSTStateCode), 2) +'-' + LTRIM(RTRIM(state_name)) end )as [Place Of Supply]," +
                         " Rate,sum([Taxable Value]) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrysaleheadfor_GSTReturn" +
                         " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and IsDeleted!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and UnregisterGST=1" +
                         " and [Invoice Value]<=250000 group by rate,Carporate_Sale_No,[Place Of Supply],CA_GSTStateCode,state_name";

            string qry1 = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                          " Rate as Rate,sum(texable_amount) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrycommissionbill " +
                          " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                           " UnregisterGST=1 and Bill_Amount<=250000 group by GSTStateCode,billtostatename,Rate";


            string qry2 = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                          " 5.00 as Rate,sum(Taxable_Amount) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryRetailheadnew " +
                          " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and" +
                          " UnregisterGST=1 and NetValue<=250000 and IsDelete!=0 group by GSTStateCode,billtostatename";

            string qry3 = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,BillToGSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                          " gstrate as Rate,sum(subTotal) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qrysugarsalereturnhead " +
                          " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                          " UnregisterGST=1 and Bill_Amount<=250000 group by BillToGSTStateCode,billtostatename,gstrate";

            string qry4 = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,GSTStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as [Place Of Supply]," +
                          " gstrate as Rate,sum(subTotal) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryrentbillhead " +
                          " where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                          " UnregisterGST=1 and IsDeleted!=0 and Total<=250000 group by GSTStateCode,billtostatename,gstrate";

            string qry5 = "select 'OE' as Type,(RIGHT('0'+CONVERT(NVARCHAR,CustomerStateCode),2) +'-'+ LTRIM(RTRIM(CustomerStateName))) as [Place Of Supply]," +
                          " rate as Rate,sum(subTotal) as [Taxable Value],'' as [Cess Amount],'' as [E-Commerce GSTIN] from qryColdStorageHead " +
                          " where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and " +
                          " UnregisterGST=1 and Total<=250000 group by CustomerStateCode,CustomerStateName,rate";

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            DataSet dsDNote = new DataSet();
            dsDNote = clsDAL.SimpleQuery(qry1);

            DataSet dsRR = new DataSet();
            dsRR = clsDAL.SimpleQuery(qry2);

            DataSet dsRS = new DataSet();
            dsRS = clsDAL.SimpleQuery(qry3);

            DataSet dsRB = new DataSet();
            dsRB = clsDAL.SimpleQuery(qry4);

            DataSet dsCB = new DataSet();
            dsCB = clsDAL.SimpleQuery(qry5);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            if (dsDNote.Tables[0].Rows.Count > 0)
            {
                dt.Merge(dsDNote.Tables[0]);
            }

            if (dsRR.Tables[0].Rows.Count > 0)
            {
                dt.Merge(dsRR.Tables[0]);
            }
            if (dsRS.Tables[0].Rows.Count > 0)
            {
                dt.Merge(dsRS.Tables[0]);
            }
            if (dsRB.Tables[0].Rows.Count > 0)
            {
                dt.Merge(dsRB.Tables[0]);
            }
            if (dsCB.Tables[0].Rows.Count > 0)
            {
                dt.Merge(dsCB.Tables[0]);
            }
            string fileName = "";
            string strForCSV = "";

            //DataView dvWrongState = new DataView(dt);
            //dvWrongState.RowFilter = "PartyStateCode = 0";
            //if (dvWrongState.ToTable().Rows.Count > 0)
            //{
            //    fileName = "WrongState.csv";
            //    DataTable dtnew = dvWrongState.ToTable();

            //    int colindex = 3;
            //    for (int i = 0; i < dvWrongState.ToTable().Columns.Count; i++)
            //    {
            //        dtnew.Columns.RemoveAt(colindex);
            //        if (dtnew.Columns.Count < 4)
            //        {
            //            break;
            //        }
            //    }
            //    dtnew = dtnew.DefaultView.ToTable(true, "Ac_Code", "PartyName");
            //    strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
            //}
            //else
            //{
            fileName = "b2cs.csv";
            strForCSV = clsCommon.DataTableToCSV(dt, ',');
            //}
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(strForCSV.ToString());
            Response.Flush();
            Response.End();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnCreatePurchaseBillSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            //string qry = "select distinct doc_no as OurNo,Bill_No as MillInvoiceNo,PartyName,FromGSTNo,FromStateCode,Convert(varchar(10),doc_date,103) as Date, " +
            //             " LORRYNO as Vehicle_No,isnull(Quantal,0) as Quintal,subTotal as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,PURCNO as DO_No from NT_1_qrySugarPurchListForReport " +
            //             " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by doc_no";


            //string qry = "select doc_no as OurNo,Bill_No as MillInvoiceNo,PartyName,FromGSTNo,FromStateCode,Convert(varchar(10),doc_date,103) as Date, " +
            //             " LORRYNO as Vehicle_No,sum(isnull(Quantal,0)) as Quintal,subTotal as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,PURCNO as DO_No from NT_1_qrySugarPurchListForReport " +
            //             " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
            //             + " group by doc_no ,Bill_No ,PartyName,FromGSTNo,FromStateCode,doc_date,LORRYNO,subTotal,CGSTAmount,SGSTAmount,IGSTAmount,Bill_Amount,PURCNO order by doc_no";
            //string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+CONVERT(NVARCHAR,doc_no) as OurNo,Bill_No as MillInvoiceNo,EWay_Bill_No as MillEwayBill_NO,suppliergstno as FromGSTNo,Ac_Code as Party_Code,suppliername as Party_Name,millshortname as Mill_Name,GSTStateCode as FromStateCode,Convert(varchar(10),doc_date,103) as Date, " +
            //            " LORRYNO as Vehicle_No,sum(isnull(Quantal,0)) as Quintal,rate as Rate,subTotal as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,PURCNO as DO from qrypurchaseheaddetail " +
            //            " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
            //                         + " group by doc_no ,Bill_No,Ac_Code ,suppliername,suppliergstno,GSTStateCode,doc_date,LORRYNO,rate,subTotal,CGSTAmount,SGSTAmount,IGSTAmount,Bill_Amount,PURCNO,EWay_Bill_No,millshortname order by doc_date,doc_no";

            // main string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+CONVERT(NVARCHAR,doc_no) as OurNo,Bill_No as MillInvoiceNo,EWay_Bill_No as MillEwayBill_NO,suppliergstno as FromGSTNo,Ac_Code as Party_Code,suppliername as Party_Name,millshortname as Mill_Name,GSTStateCode as FromStateCode,Convert(varchar(10),doc_date,103) as Date, " +
            //          " LORRYNO as Vehicle_No,sum(isnull(Quantal,0)) as Quintal,subTotal/sum(isnull(Quantal,0)) as Rate,subTotal as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,PURCNO as DO from qrypurchaseheaddetail " +
            //          " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
            //                       + " group by doc_no ,Bill_No,Ac_Code ,suppliername,suppliergstno,GSTStateCode,doc_date,LORRYNO,subTotal,CGSTAmount,SGSTAmount,IGSTAmount,Bill_Amount,PURCNO,EWay_Bill_No,millshortname order by doc_date,doc_no";

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+CONVERT(NVARCHAR,doc_no) as OurNo,Bill_No as MillInvoiceNo,EWay_Bill_No as MillEwayBill_NO,suppliergstno as FromGSTNo,Ac_Code as Party_Code,suppliername as Party_Name,millshortname as Mill_Name,GSTStateCode as FromStateCode,Convert(varchar(10),doc_date,103) as Date, " +
                       " LORRYNO as Vehicle_No,sum(isnull(Quantal,0)) as Quintal,rate as Rate,subTotal as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,PURCNO as DO from qrypurchaseheaddetail " +
                       " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                                    + " group by doc_no ,Bill_No,Ac_Code ,suppliername,suppliergstno,GSTStateCode,doc_date,LORRYNO,rate,subTotal,CGSTAmount,SGSTAmount,IGSTAmount,Bill_Amount,PURCNO,EWay_Bill_No,millshortname order by doc_date,doc_no";
   


            //string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+CONVERT(NVARCHAR,doc_no) as OurNo,Bill_No as MillInvoiceNo,EWay_Bill_No as MillEwayBill_NO,suppliergstno as FromGSTNo,Ac_Code as Party_Code,suppliername as Party_Name,millshortname as Mill_Name,GSTStateCode as FromStateCode,Convert(varchar(10),doc_date,103) as Date, " +
            //          " LORRYNO as Vehicle_No,sum(isnull(Quantal,0)) as Quintal,rate as Rate,item_Amount as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,PURCNO as DO from qrypurchaseheaddetail " +
            //          " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
            //                       + " group by doc_no ,Bill_No,Ac_Code ,suppliername,suppliergstno,GSTStateCode,doc_date,LORRYNO,rate,subTotal,CGSTAmount,SGSTAmount,IGSTAmount,Bill_Amount,PURCNO,EWay_Bill_No,millshortname order by doc_date,doc_no";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Purchase Summary";
                    //DataTable dtnew = new DataTable();
                    //string[] parArr = new string[] { "SR_No", "OurNo", "MillInvoiceNo", "FromGSTNo", "FromStateCode", "Date", "Vehicle_No", "TaxableAmount", "CGST", "SGST", "IGST", "Payable_Amount" };
                    //dtnew = dt.DefaultView.ToTable(true);

                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));

                    grdAll.FooterRow.Cells[11].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[14].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[15].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[16].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[17].Text = totalPayable_Amount.ToString();

                    //grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnCreateSaleBillSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+CONVERT(NVARCHAR,doc_no) as Invoice_No,billtogstno as PartyGSTNo,Ac_Code as PartyCode,billtoname as PartyName,millshortname as Mill_Name,partygststatecode as billtogststatecode,CONVERT(varchar(10),doc_date,103) as Invoice_Date,LORRYNO as Vehicle_No,sum(Quantal) as Quintal,sum(item_Amount)/sum(Quantal) as Rate,TaxableAmount, " +
                " CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,DO_No,ackno as ACKNo from qrysalebillresgistersummary  " +
                " where doc_date>='2017-07-01' and doc_no!=0 and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and IsDeleted!=0 group by doc_no,billtogstno,Ac_Code,billtoname,millshortname,partygststatecode,doc_date,LORRYNO,Bill_Amount,DO_No,ackno,TaxableAmount,CGSTAmount,SGSTAmount,IGSTAmount,Bill_Amount order by doc_date";

            //string qry = " select doc_no as Invoice_No,billtogstno as PartyGSTNo,Ac_Code as PartyCode, billtoname as PartyName,billtogststatecode as PartyStateCode,doc_dateConverted as Invoice_Date" +
            //    ",LORRYNO as Vehicle_No,Quantal as Quintal,salerate as Rate ,TaxableAmount as TaxableAmount,CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Payable_Amount,DO_No as DO " +
            //    " from qrysalebillresgistersummary where doc_date between'" + fromdt + "' and '" + todt + "' and Company_Code="
            //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
            //    " order by doc_date";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DO_No = dt.Rows[i]["DO_No"].ToString();
                            corportno = clsCommon.getString("select Carporate_Sale_No from nt_1_deliveryorder where doc_no=" + DO_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

                            if (corportno != string.Empty && corportno != "0")
                            {
                                DataSet dsbillto = clsDAL.SimpleQuery("select carporate_ac,carporateacname,isnull(carporateacgstno,0) as carporateacgstno,isnull(carporateacstatecode,0) as carporateacstatecode from qrydohead where doc_no=" + DO_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                                if (dsbillto != null)
                                {
                                    DataTable dtbillto = dsbillto.Tables[0];
                                    if (dtbillto.Rows.Count > 0)
                                    {
                                        dt.Rows[i][3] = dtbillto.Rows[0]["carporate_ac"].ToString();
                                        dt.Rows[i][4] = dtbillto.Rows[0]["carporateacname"].ToString();
                                        dt.Rows[i][2] = dtbillto.Rows[0]["carporateacgstno"].ToString();
                                        dt.Rows[i][6] = dtbillto.Rows[0]["carporateacstatecode"].ToString();
                                    }
                                }
                            }
                            double rate = Convert.ToDouble(dt.Rows[i]["Rate"].ToString());
                            dt.Rows[i]["Rate"] = Math.Round(rate,2);
                        }
                    }
                    lblSummary.Text = "Sale Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));

                    grdAll.FooterRow.Cells[9].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[14].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[15].Text = totalPayable_Amount.ToString();

                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnOtherPur_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,s.Doc_No as Invoice_No,s.Gst_No as PartyGSTNo,Supplier_Code as PartyCode," +
             "s.suppilername as PartyName,s.GSTStateCode as PartyStateCode,CONVERT(varchar(10),s.doc_date,103) as Invoice_Date,s.Taxable_Amount as TaxableAmount,s.CGST_Amount as CGST,s.SGST_Amount as SGST,s.IGST_Amount as IGST,s.Bill_Amount from qryotherpurchase s " +
                  " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by s.doc_date";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Other Purchase Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    //double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Bill_Amount)", string.Empty));

                    //  grdAll.FooterRow.Cells[8].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[7].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalPayable_Amount.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnServiceBill_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            //            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'Rent'+CONVERT(NVARCHAR,s.Doc_No) as Invoice_No,s.Gst_No as PartyGSTNo,"
            //+ "Customer_Code as PartyCode,s.CustomerName as PartyName,s.CustomerStateCode as PartyStateCode,CONVERT(varchar(10),"
            //+ "s.Date,103) as Invoice_Date,s.Amount as TaxableAmount,s.CGSTAmount as CGST,s.SGSTAmount as "
            //+ "SGST,s.IGSTAmount as IGST,s.Final_Amount from NT_1_qryRentBill s  where s.Doc_Date>='01-07-2017' and s.Doc_Date between '" + fromdt + "' and '" + todt +
            //"' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by s.Date";

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RB'+CONVERT(NVARCHAR,s.Doc_No) as Invoice_No,s.Gst_No as PartyGSTNo," +
                        " Customer_Code as PartyCode,s.Ac_Name_E as PartyName,s.GSTStateCode as PartyStateCode,CONVERT(varchar(10),s.Date,103) as Invoice_Date," +
                        "s.Subtotal as TaxableAmount,s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Final_Amount,s.ackno as ACKNo from qryrentbillhead s " +
                        "where s.Date>='01-07-2017' and s.Date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and s.IsDeleted!=0 and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by s.Date";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Service Bill Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    //double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Final_Amount)", string.Empty));

                    //  grdAll.FooterRow.Cells[8].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[7].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalPayable_Amount.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btlCreateRetailSaleSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            //string qry = "select h.Tran_Type,'RS'+CONVERT(NVARCHAR,h.doc_no)  as Invoice_No,p.Gst_No as PartyGSTNo,h.Party_Code,p.Ac_Name_E as PartyName,p.GSTStateCode as PartyStateCode,CONVERT(VARCHAR(10),h.doc_date,103) as Invoice_Date,h.Vehical_No" +
            //          " ,h.Total as TaxableAmount, h.CGSTAmount as CGST,h.SGSTAmount as SGST,h.IGSTAmount as IGST,h.Grand_Total as Final_Amount from NT_1_RetailSaleHead h " +
            //          " left outer join NT_1_AccountMaster p on h.Party_Code=p.Ac_Code and h.Company_Code=p.Company_Code where h.doc_date>='2017-07-01' and h.doc_date between '" + fromdt + "' and '" + todt + "' and h.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and h.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by h.doc_date";

            string qry = "select h.Tran_Type,'RR'+CONVERT(NVARCHAR,h.doc_no)  as Invoice_No,p.Gst_No as PartyGSTNo,h.Party_Code,p.Ac_Name_E as PartyName,p.GSTStateCode as PartyStateCode,CONVERT(VARCHAR(10),h.doc_date,103)" +
                         " as Invoice_Date,h.Vahical_No ,h.Taxable_Amount as TaxableAmount, h.CGST_Amount as CGST,h.SGST_Amount as SGST,h.IGST_Amount as IGST,h.NetValue as Final_Amount from Retail_Head h  left outer join" +
                         " NT_1_AccountMaster p on h.Party_Code=p.Ac_Code and h.Company_Code=p.Company_Code where h.doc_date>='2017-07-01' and h.doc_date between '" + fromdt + "' and '" + todt + "' " +
                         " and h.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and h.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and h.IsDelete!=0 order by h.doc_date";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Retail Sale Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    //double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Final_Amount)", string.Empty));

                    //grdAll.FooterRow.Cells[8].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalPayable_Amount.ToString();

                    //grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnFrieghtSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            //string qry = "select '' as Challan_No,d.doc_no as DO_No,CONVERT(varchar(10),d.doc_date,103) as Date,mill_code as Mill_Code,m.Ac_Name_E as MillName,d.MillGSTStateCode as MillStateCode," +
            //            " d.SaleBillTo as Billed_To,p.Ac_Name_E as BillToName,case ISNULL(d.SaleBillTo,0) when 0 then 27 else d.SalebilltoGstStateCode end as BillToStateCode, d.truck_no as Vehicle_No,d.quantal as Quintal,d.MM_Rate as Rate,d.Memo_Advance as Amount " +
            //            " from NT_1_deliveryorder d left outer join NT_1_AccountMaster m on d.mill_code=m.Ac_Code and d.company_code=m.Company_Code " +
            //            " left outer join NT_1_AccountMaster p on d.SaleBillTo=p.Ac_Code and d.company_code=p.Company_Code where d.doc_date>='2017-07-01' and d.doc_date between '" + fromdt + "' and '" + todt + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.MM_Rate!=0  AND (d.desp_type ='DI') order by d.doc_date";


            string qry = "select '' as Challan_No,d.doc_no as DO_No,CONVERT(varchar(10),d.doc_date,103) as Date,mill_code as Mill_Code,m.Ac_Name_E as MillName," +
                       " d.MillGSTStateCode as MillStateCode, d.SaleBillTo as Billed_To,p.Ac_Name_E as BillToName,case ISNULL(d.SaleBillTo,0) when 0 then 27 else d.SalebilltoGstStateCode end as BillToStateCode," +
                       " T.Ac_Name_E as TransportName,case ISNULL(d.SaleBillTo,0) when 0 then 27 else d.TransportGSTStateCode end as TransportStateCode, " +
                       " d.truck_no as Vehicle_No,d.quantal as Quintal,d.MM_Rate as Rate,d.Memo_Advance as Amount  from NT_1_deliveryorder d " +
                       " LEFT OUTER JOIN dbo.nt_1_accountmaster AS T ON d.transport = T.Ac_Code and  d.company_code = T.company_code left outer join NT_1_AccountMaster m on d.mill_code=m.Ac_Code and d.company_code=m.Company_Code  left outer join " +
                       " NT_1_AccountMaster p on d.SaleBillTo=p.Ac_Code and d.company_code=p.Company_Code where d.doc_date>='2017-07-01' and " +
                       " d.doc_date between '" + fromdt + "' and '" + todt + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.MM_Rate!=0  AND " +
                       " (d.desp_type ='DI') and purc_no!=0 order by d.doc_date";
            DataSet ds1 = new DataSet();
            ds1 = clsDAL.SimpleQuery(qry);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds1.Tables[0];

                    dt1.Columns.Add(new DataColumn("CGST", typeof(double)));
                    dt1.Columns.Add(new DataColumn("SGST", typeof(double)));
                    dt1.Columns.Add(new DataColumn("IGST", typeof(double)));
                    dt1.Columns.Add(new DataColumn("FinalAmount", typeof(double)));

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        double cgstrate = 2.5;
                        double sgstrate = 2.5;
                        double igstrate = 5;

                        double CGSTAmount = 0.0;
                        double SGSTAmount = 0.0;
                        double IGSTAmount = 0.0;

                        int millStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"]);
                        int partyStateCode = Convert.ToInt32(dt1.Rows[i]["TransportStateCode"].ToString());
                        double Amount = Convert.ToDouble(dt1.Rows[i]["Amount"].ToString());
                        if (millStateCode == partyStateCode)
                        {
                            CGSTAmount = Math.Round((Amount * cgstrate / 100), 2);
                            SGSTAmount = Math.Round((Amount * sgstrate / 100), 2);
                        }
                        else
                        {
                            IGSTAmount = Math.Round((Amount * igstrate / 100), 2);
                        }

                        dt1.Rows[i]["CGST"] = CGSTAmount;
                        dt1.Rows[i]["SGST"] = SGSTAmount;
                        dt1.Rows[i]["IGST"] = IGSTAmount;
                        dt1.Rows[i]["FinalAmount"] = Math.Round((Amount + CGSTAmount + SGSTAmount + IGSTAmount), 2);
                    }
                    lblSummary.Text = "Frieght Summary";
                    grdAll.DataSource = dt1;
                    grdAll.DataBind();

                    double totalQuintal = Convert.ToDouble(dt1.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt1.Compute("SUM(Amount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt1.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt1.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt1.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt1.Compute("SUM(FinalAmount)", string.Empty));

                    grdAll.FooterRow.Cells[12].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[14].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[15].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[16].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[17].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[18].Text = totalPayable_Amount.ToString();

                    grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnDebitNoteSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'LV'+convert(varchar(50),v.doc_no) as DebitNote_No,a.Gst_No as PartyGSTNo,v.ac_code as PartyCode,a.Ac_Name_E as PartyName,a.GSTStateCode as PartyStateCode," +
                        " CONVERT(varchar(10),v.Doc_Date,103) as Date,v.qntl as Quintal,ABS(v.purc_rate) as Rate,ABS(v.texable_amount) as TaxableAmount,ABS(v.cgst_amount) as CGST,ABS(v.sgst_amount) as SGST," +
                        " ABS(v.igst_amount) as IGST,ABS(v.bill_amount) as Final_Amount,v.ackno as ACKNO from commission_bill v " +
                        " left outer join NT_1_AccountMaster a on v.Ac_Code=a.Ac_Code and v.Company_Code=a.Company_Code" +
                        " where v.texable_amount!=0 and v.doc_date>='2017-07-01' and v.doc_date between '" + fromdt + "' and '" + todt + "' and v.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and v.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and v.bill_amount>0 order by v.Doc_Date ";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Debit Note Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Final_Amount)", string.Empty));

                    grdAll.FooterRow.Cells[7].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalPayable_Amount.ToString();

                    //grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnCreditNoteSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            //string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,v.Doc_No as DebitNote_No,a.Gst_No as PartyGSTNo,v.Ac_Code as PartyCode,a.Ac_Name_E as PartyName,a.GSTStateCode as PartyStateCode," +
            //            " CONVERT(varchar(10),v.Doc_Date,103) as Date,v.Quantal as Quintal,ABS(v.Diff_Amount) as Rate,ABS(v.TaxableAmount) as TaxableAmount,ABS(v.CGSTAmount) as CGST,ABS(v.SGSTAmount) as SGST," +
            //            " ABS(v.IGSTAmount) as IGST,ABS(v.Voucher_Amount) as Final_Amount from NT_1_Voucher v " +
            //            " left outer join NT_1_AccountMaster a on v.Ac_Code=a.Ac_Code and v.Company_Code=a.Company_Code" +
            //            " where v.TaxableAmount!=0 and v.doc_date>='2017-07-01' and v.doc_date between '" + fromdt + "' and '" + todt + "' and v.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and v.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and v.Voucher_Amount<0 order by v.Doc_Date ";

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,v.doc_no as DebitNote_No,a.Gst_No as PartyGSTNo,v.ac_code as PartyCode,a.Ac_Name_E as PartyName,a.GSTStateCode as PartyStateCode," +
                      " CONVERT(varchar(10),v.Doc_Date,103) as Date,v.qntl as Quintal,ABS(v.purc_rate) as Rate,ABS(v.texable_amount) as TaxableAmount,ABS(v.cgst_amount) as CGST,ABS(v.sgst_amount) as SGST," +
                      " ABS(v.igst_amount) as IGST,ABS(v.bill_amount) as Final_Amount from commission_bill v " +
                      " left outer join NT_1_AccountMaster a on v.Ac_Code=a.Ac_Code and v.Company_Code=a.Company_Code" +
                      " where v.texable_amount!=0 and v.doc_date>='2017-07-01' and v.doc_date between '" + fromdt + "' and '" + todt + "' and v.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and v.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and v.bill_amount<0 order by v.Doc_Date ";


            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Credit Note Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Final_Amount)", string.Empty));

                    grdAll.FooterRow.Cells[7].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalPayable_Amount.ToString();

                    //grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnExportToexcel_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (ViewState["currentTable"] != null)
        {
            dt = (DataTable)ViewState["currentTable"];
            if (dt.Rows.Count > 0)
            {
                grdAll.DataSource = dt;
                grdAll.DataBind();

                double totaltax = Convert.ToDouble(dt.Compute("SUM(Taxable_Amt)", string.Empty));
                double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                double totalTCS = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));
                double totalTDS = Convert.ToDouble(dt.Compute("SUM(TDS)", string.Empty));
                double totalNet = Convert.ToDouble(dt.Compute("SUM(Bill_Amt)", string.Empty));

                grdAll.FooterRow.Cells[7].Text = totaltax.ToString();
                grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                grdAll.FooterRow.Cells[11].Text = totalTCS.ToString();
                grdAll.FooterRow.Cells[12].Text = totalTDS.ToString();
                grdAll.FooterRow.Cells[13].Text = totalNet.ToString();
            }
        }
        Export(grdAll, lblSummary.Text);

    }

    private void Export(GridView grd, string Name)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        grd.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btnEwayBill_Click(object sender, EventArgs e)
    {


        try
        {

            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select doc_no,CONVERT(varchar,doc_date,103) as doc_date,"
                      + " UPPER(billtoname) as BillToName,UPPER(billtogstno) as BillToGst ,UPPER(shiptoname) as ShippTo,UPPER(Address_E) as Address_E," +
                      "UPPER(city_name_e) as city_name_e,(case Pincode when 0 then 999999  else pincode end) as pincode,upper(BillToStateCode) AS BillToStateCode," +
                      "UPPER(State_Name) as State_Name,NETQNTL,TaxableAmount,"
                      + "(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,CGSTAmount,"
                      + "SGSTAmount,IGSTAmount,Distance,LORRYNO,UPPER(millname) as millname,upper(milladdress) as milladdress,(case millpincode when 0 then 999999  else millpincode end) as millpincode,millcityname,DO_No,upper(millstatename) as millstatename,convert(varchar,TransDate,103)as TransDate"
                      + " from qrysalebillresgistersummary where (Carporate_Sale_No = 0 OR  Carporate_Sale_No IS NULL) and doc_date between '" + fromdt + "' and '" + todt
                      + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            #region[from query]
            string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string comnm = qrynm.ToUpper();
            string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string state = clsCommon.getString("select UPPER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string DODate = clsCommon.getString("select doc_date from NT_1_deliveryorder ");
            #endregion

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    #region[colummn add]
                    dt.Columns.Add(new DataColumn("Supply Type", typeof(string)));
                    dt.Columns.Add(new DataColumn("Sub Type", typeof(string)));
                    dt.Columns.Add(new DataColumn("Doc Type", typeof(string)));
                    dt.Columns.Add(new DataColumn("Transaction Type", typeof(string)));

                    dt.Columns.Add(new DataColumn("Product", typeof(string)));
                    dt.Columns.Add(new DataColumn("Description", typeof(string)));
                    dt.Columns.Add(new DataColumn("HSN", typeof(string)));
                    dt.Columns.Add(new DataColumn("Unit", typeof(string)));
                    dt.Columns.Add(new DataColumn("CESS Amount", typeof(string)));
                    dt.Columns.Add(new DataColumn("CESS Non Advol Amount", typeof(string)));
                    dt.Columns.Add(new DataColumn("Other", typeof(string)));
                    dt.Columns.Add(new DataColumn("Total Invoice Value", typeof(string)));

                    dt.Columns.Add(new DataColumn("Trans Mode", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Distance level (Km)", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans ID", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans DocNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans Date", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Doc No",typeof(Int32)));
                    dt.Columns.Add(new DataColumn("From_Address2", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_Address1", typeof(string)));
                    // dt.Columns.Add(new DataColumn("To_Address2", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_OtherPartyName", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_GSTIN", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_Place", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_Pin Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_State", typeof(string)));
                    dt.Columns.Add(new DataColumn("Dispatch_State", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));

                    dt.Columns.Add(new DataColumn("Vehical_Type", typeof(string)));
                    #endregion
                    string vno;
                    string to_ac_name_e;
                    string to_address1;
                    string to_address2;
                    string to_place;
                    string from_partnm;
                    string from_address;
                    string from_city;
                    string do_no = "";

                    string Bill_To;
                    string Ship_To;
                    double taxamount;
                    string taxvalue;
                    double CGST;
                    double SGST;
                    double IGST;
                    double CessAmt = 0.00;
                    double CessNontAdvol = 0.00;
                    double Other = 0.00;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region[default value]
                        dt.Rows[i]["Supply Type"] = "Outward";
                        dt.Rows[i]["Sub Type"] = "Supply";
                        dt.Rows[i]["Doc Type"] = "Tax Invoice";

                        dt.Rows[i]["Product"] = "SUGAR";
                        dt.Rows[i]["Description"] = "SUGAR";
                        dt.Rows[i]["HSN"] = "1701";
                        dt.Rows[i]["Unit"] = "QUINTAL";
                        dt.Rows[i]["CESS Amount"] = "0";
                        dt.Rows[i]["CESS Non Advol Amount"] = "0.00";
                        dt.Rows[i]["Other"] = "0.00";

                        dt.Rows[i]["Trans Mode"] = "Road";
                        //dt.Rows[i]["Distance level (Km)"] = "0";
                        dt.Rows[i]["Trans Name"] = " ";
                        dt.Rows[i]["Trans ID"] = " ";
                        dt.Rows[i]["Trans DocNo"] = " ";
                        dt.Rows[i]["Trans Date"] = dt.Rows[i]["TransDate"].ToString();
                        // dt.Rows[i]["State_Name"] = dt.Rows[i]["BillToStateCode"];

                        #region[replace from adreess]
                        //from_address = address;

                        from_address = dt.Rows[i]["milladdress"].ToString();

                        //to_address1 = to_address1.Replace("-", "");
                        //to_address1 = to_address1.Replace("/", "");
                        //to_address1 = to_address1.Replace("&", "");
                        from_address = from_address.Replace(".", "");
                        //to_address1 = to_address1.Replace("#", "");
                        //to_address1 = to_address1.Replace(",", "");
                        from_address = from_address.Replace("(", "");
                        from_address = from_address.Replace(")", "");
                        from_address = from_address.Replace(":", "");
                        from_address = from_address.Replace("_", "");
                        from_address = from_address.Replace("@", "");
                        from_address = from_address.Replace(";", "");
                        from_address = from_address.Replace("=", "");
                        dt.Rows[i]["From_Address2"] = from_address;
                        dt.Rows[i]["From_Address1"] = dt.Rows[i]["millname"].ToString();
                        #endregion

                        #region[replace from other party name]
                        from_partnm = comnm;
                        from_partnm = from_partnm.Replace("-", "");
                        from_partnm = from_partnm.Replace("/", "");
                        from_partnm = from_partnm.Replace("&", "");
                        from_partnm = from_partnm.Replace(".", "");
                        from_partnm = from_partnm.Replace("#", "");
                        from_partnm = from_partnm.Replace("(", "");
                        from_partnm = from_partnm.Replace(")", "");
                        from_partnm = from_partnm.Replace(":", "");
                        from_partnm = from_partnm.Replace("_", "");
                        from_partnm = from_partnm.Replace("@", "");
                        from_partnm = from_partnm.Replace(";", "");
                        from_partnm = from_partnm.Replace("=", "");
                        dt.Rows[i]["From_OtherPartyName"] = from_partnm;
                        #endregion

                        dt.Rows[i]["From_GSTIN"] = gstno;

                        #region[replace from place]
                        //from_city = city;
                        from_city = dt.Rows[i]["millcityname"].ToString();
                        //to_address1 = to_address1.Replace("-", "");
                        //to_address1 = to_address1.Replace("/", "");
                        //to_address1 = to_address1.Replace("&", "");
                        from_city = from_city.Replace(".", "");
                        //to_address1 = to_address1.Replace("#", "");
                        //to_address1 = to_address1.Replace(",", "");
                        from_city = from_city.Replace("(", "");
                        from_city = from_city.Replace(")", "");
                        from_city = from_city.Replace(":", "");
                        from_city = from_city.Replace("_", "");
                        from_city = from_city.Replace("@", "");
                        from_city = from_city.Replace(";", "");
                        from_city = from_city.Replace("=", "");
                        dt.Rows[i]["From_Place"] = from_city;
                        #endregion

                        //dt.Rows[i]["From_Pin Code"] = pin;
                        dt.Rows[i]["From_Pin Code"] = dt.Rows[i]["millpincode"].ToString();
                        dt.Rows[i]["From_State"] = state;
                        dt.Rows[i]["Dispatch_State"] = dt.Rows[i]["millstatename"].ToString();


                        #endregion

                        Bill_To = dt.Rows[i]["BillToName"].ToString();
                        Ship_To = dt.Rows[i]["ShippTo"].ToString();
                        if (Bill_To != Ship_To)
                        {
                            dt.Rows[i]["Transaction Type"] = "Combination of 2 and 3";

                        }
                        else
                        {
                            dt.Rows[i]["Transaction Type"] = "Bill From-Dispatch From";

                        }

                        taxamount = Convert.ToDouble(dt.Rows[i]["TaxableAmount"].ToString());
                        CGST = Convert.ToDouble(dt.Rows[i]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[i]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[i]["IGSTAmount"].ToString());
                        // taxvalue = (dt.Rows[i]["Taxrate"].ToString());
                        //string TotaInvoice = (taxamount + '+' + taxvalue + '0').ToString();

                        //dt.Rows[i]["Total Invoice Value"] = TotaInvoice;
                        double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;

                        dt.Rows[i]["Total Invoice Value"] = TotalInvoice;



                        #region[replace vehicle no]
                        vno = Convert.ToString(dt.Rows[i]["LORRYNO"]);
                        vno = vno.Replace("-", "");
                        vno = vno.Replace("/", "");
                        vno = vno.Replace(" ", "");
                        vno = vno.Replace("&", "");
                        vno = vno.Replace(".", "");
                        vno = vno.Replace("#", "");
                        vno = vno.Replace("(", "");
                        vno = vno.Replace(")", "");
                        vno = vno.Replace(":", "");
                        vno = vno.Replace("_", "");
                        vno = vno.Replace("@", "");
                        vno = vno.Replace(";", "");
                        vno = vno.Replace("=", "");
                        dt.Rows[i]["LORRYNO"] = vno;
                        #endregion

                        #region[replae to party name]
                        to_ac_name_e = Convert.ToString(dt.Rows[i]["BillToName"]);
                        to_ac_name_e = to_ac_name_e.Replace("-", "");
                        to_ac_name_e = to_ac_name_e.Replace("/", "");
                        to_ac_name_e = to_ac_name_e.Replace("&", "");
                        to_ac_name_e = to_ac_name_e.Replace(".", "");
                        to_ac_name_e = to_ac_name_e.Replace("#", "");
                        to_ac_name_e = to_ac_name_e.Replace("(", "");
                        to_ac_name_e = to_ac_name_e.Replace(")", "");
                        to_ac_name_e = to_ac_name_e.Replace(":", "");
                        to_ac_name_e = to_ac_name_e.Replace("_", "");
                        to_ac_name_e = to_ac_name_e.Replace("@", "");
                        to_ac_name_e = to_ac_name_e.Replace(";", "");
                        to_ac_name_e = to_ac_name_e.Replace("=", "");
                        dt.Rows[i]["BillToName"] = to_ac_name_e;
                        #endregion

                        #region[replace to address1]
                        to_address1 = Convert.ToString(dt.Rows[i]["ShippTo"]);
                        to_address1 = to_address1.Replace("-", "");
                        to_address1 = to_address1.Replace("/", "");
                        to_address1 = to_address1.Replace("&", "");
                        to_address1 = to_address1.Replace(".", "");
                        to_address1 = to_address1.Replace("#", "");
                        to_address1 = to_address1.Replace(",", "");
                        to_address1 = to_address1.Replace("(", "");
                        to_address1 = to_address1.Replace(")", "");
                        to_address1 = to_address1.Replace(":", "");
                        to_address1 = to_address1.Replace("_", "");
                        to_address1 = to_address1.Replace("@", "");
                        to_address1 = to_address1.Replace(";", "");
                        to_address1 = to_address1.Replace("=", "");
                        dt.Rows[i]["ShippTo"] = to_address1;
                        //dt.Rows[i]["To_Address2"] = to_address1;
                        #endregion

                        #region[replace to address2]
                        to_address2 = Convert.ToString(dt.Rows[i]["Address_E"]);
                        to_address2 = to_address2.Replace("-", "");
                        to_address1 = to_address1.Replace("/", "");
                        to_address1 = to_address1.Replace("&", "");
                        to_address2 = to_address2.Replace(".", "");
                        to_address1 = to_address1.Replace("#", "");
                        to_address2 = to_address2.Replace(",", "");
                        to_address2 = to_address2.Replace("(", "");
                        to_address2 = to_address2.Replace(")", "");
                        to_address2 = to_address2.Replace(":", "");
                        to_address2 = to_address2.Replace("_", "");
                        to_address2 = to_address2.Replace("@", "");
                        to_address2 = to_address2.Replace(";", "");
                        to_address2 = to_address2.Replace("=", "");
                        to_address2 = to_address2.Replace("*", "");
                        to_address2 = to_address2.Replace(":", "");
                        to_address2 = to_address2.Replace(";", "");
                        dt.Rows[i]["Address_E"] = to_address2;
                        //dt.Rows[i]["To_Address2"] = to_address1;
                        #endregion

                        #region[replace to place]
                        to_place = Convert.ToString(dt.Rows[i]["city_name_e"]);
                        //to_address1 = to_address1.Replace("-", "");
                        //to_address1 = to_address1.Replace("/", "");
                        //to_address1 = to_address1.Replace("&", "");
                        to_place = to_place.Replace(".", "");
                        //to_address1 = to_address1.Replace("#", "");
                        //to_address1 = to_address1.Replace(",", "");
                        to_place = to_place.Replace("(", "");
                        to_place = to_place.Replace(")", "");
                        to_place = to_place.Replace(":", "");
                        to_place = to_place.Replace("_", "");
                        to_place = to_place.Replace("@", "");
                        to_place = to_place.Replace(";", "");
                        to_place = to_place.Replace("=", "");
                        dt.Rows[i]["city_name_e"] = to_place;
                        //  dt.Rows[i]["Ship_To_State"] = dt.Rows[i]["State_Name"];
                        #endregion

                        double to_Distance;

                        #region[replace to Distance]
                        to_Distance = Convert.ToDouble(dt.Rows[i]["Distance"]);


                        dt.Rows[i]["Distance"] = to_Distance;

                        dt.Rows[i]["Vehical_Type"] = "REGULAR";
                        #endregion

                    }

                    lblSummary.Text = "E Way Bill";

                    grdAll.DataSource = dt;

                    #region[sequence]
                    dt.Columns["Supply Type"].SetOrdinal(0);
                    dt.Columns["Sub Type"].SetOrdinal(1);
                    dt.Columns["Doc Type"].SetOrdinal(2);
                    dt.Columns["Transaction Type"].SetOrdinal(5);

                    dt.Columns["From_OtherPartyName"].SetOrdinal(6);
                    dt.Columns["From_GSTIN"].SetOrdinal(7);
                    dt.Columns["From_Address1"].SetOrdinal(8);
                    dt.Columns["From_Address2"].SetOrdinal(9);
                    dt.Columns["From_Place"].SetOrdinal(10);
                    dt.Columns["From_Pin Code"].SetOrdinal(11);
                    dt.Columns["From_State"].SetOrdinal(12);
                    dt.Columns["Dispatch_State"].SetOrdinal(13);
                    dt.Columns["State_Name"].SetOrdinal(21);
                    dt.Columns["Product"].SetOrdinal(22);
                    dt.Columns["Description"].SetOrdinal(23);
                    dt.Columns["HSN"].SetOrdinal(24);
                    dt.Columns["Unit"].SetOrdinal(25);
                    dt.Columns["NETQNTL"].SetOrdinal(26);
                    dt.Columns["CESS Amount"].SetOrdinal(32);
                    dt.Columns["CESS Non Advol Amount"].SetOrdinal(33);
                    dt.Columns["Other"].SetOrdinal(34);
                    dt.Columns["Total Invoice Value"].SetOrdinal(35);
                    dt.Columns["Trans Mode"].SetOrdinal(36);
                    // dt.Columns["Distance level (Km)"].SetOrdinal(31);
                    dt.Columns["Trans Name"].SetOrdinal(38);
                    dt.Columns["Trans ID"].SetOrdinal(39);
                    dt.Columns["Trans DocNo"].SetOrdinal(40);
                    dt.Columns["Trans Date"].SetOrdinal(41);
                    dt.Columns["Vehical_Type"].SetOrdinal(43);
                    dt.Columns["DO_No"].SetOrdinal(44);
                    dt.Columns["millname"].SetOrdinal(45);

                    #endregion

                    grdAll.DataBind();

                    #region[header name]
                    grdAll.HeaderRow.Cells[3].Text = "Doc No";
                    grdAll.HeaderRow.Cells[4].Text = "Doc Date";
                    // grdAll.HeaderRow.Cells[5].Text = "From_OtherPartyName";
                    //  grdAll.HeaderRow.Cells[6].Text = "From_GSTIN";
                    //grdAll.HeaderRow.Cells[7].Text = "From_Address1";
                    //grdAll.HeaderRow.Cells[9].Text = "From_Place";
                    //grdAll.HeaderRow.Cells[10].Text = "From_Pin Code";
                    grdAll.HeaderRow.Cells[13].Text = "Dispatch State";
                    grdAll.HeaderRow.Cells[14].Text = "To_OtherPartyName";
                    grdAll.HeaderRow.Cells[15].Text = "To_GSTIN";
                    grdAll.HeaderRow.Cells[16].Text = "To_Address1";
                    grdAll.HeaderRow.Cells[17].Text = "To_Address2";
                    grdAll.HeaderRow.Cells[18].Text = "To_Place";
                    grdAll.HeaderRow.Cells[19].Text = "To_Pin Code";
                    grdAll.HeaderRow.Cells[20].Text = "To_State";
                    grdAll.HeaderRow.Cells[21].Text = "Ship To State";
                    grdAll.HeaderRow.Cells[26].Text = "Qty";
                    grdAll.HeaderRow.Cells[27].Text = "Assessable Value";
                    grdAll.HeaderRow.Cells[28].Text = "Tax Rate(C+S+I+Cess+CESS Non Advol Amount)";
                    grdAll.HeaderRow.Cells[29].Text = "CGST Amount";
                    grdAll.HeaderRow.Cells[30].Text = "SGST Amount";
                    grdAll.HeaderRow.Cells[31].Text = "IGST Amount";
                    grdAll.HeaderRow.Cells[37].Text = "Distance level (Km)";
                    grdAll.HeaderRow.Cells[42].Text = "Vehicel No";


                    #endregion


                    foreach (GridViewRow gvr in grdAll.Rows)
                    {
                        grdAll.HeaderRow.Cells[45].Visible = true;
                        grdAll.HeaderRow.Cells[46].Visible = false;
                        grdAll.HeaderRow.Cells[47].Visible = false;
                        grdAll.HeaderRow.Cells[48].Visible = false;
                        grdAll.HeaderRow.Cells[49].Visible = false;
                        grdAll.HeaderRow.Cells[50].Visible = false;
                        //grdAll.HeaderRow.Cells[45].Visible = false;
                        //grdAll.HeaderRow.Cells[46].Visible = false;
                        //grdAll.HeaderRow.Cells[47].Visible = false;

                        gvr.Cells[45].Visible = true;
                        gvr.Cells[46].Visible = false;
                        gvr.Cells[47].Visible = false;
                        gvr.Cells[48].Visible = false;
                        gvr.Cells[49].Visible = false;
                        gvr.Cells[50].Visible = false;

                    }

                    //  e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
                    //  grdAll.HeaderRow.Cells[4].Width = new Unit("10px");
                    // grdAll.HeaderRow.Cells[4].ControlStyle.Width = new Unit("10px");

                }
            }

        }
        catch
        {
        }
        //try
        //{
        //    //  grdAll.DataBind() = null;
        //    //grdAll.DataSource = null;
        //    //grdAll.DataBind();
        //    string fromdt = txtFromDt.Text;
        //    string todt = txtToDt.Text;
        //    fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //    todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //    #region[comment]

        //    #endregion

        //    string qry = "select doc_no,CONVERT(varchar,doc_date,103) as doc_date,"
        //              + " BillToName,BillToGst,Ac_Name_E as ShippTo,Address_E,city_name_e,(case Pincode when 0 then 999999  else pincode end) as pincode," +
        //              "upper(BillToStateCode) AS BillToStateCode,UPPER(State_Name) as State_Name,NETQNTL,TaxableAmount,"
        //              + "(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0') as Taxrate,CGSTAmount,"
        //              + "SGSTAmount,IGSTAmount,Distance,LORRYNO,millname,millpincode,milladdress,millcityname,upper(millstatename) as millstatename"
        //             + " from NT_1_qryNameEwayBill where doc_date between '" + fromdt + "' and '" + todt
        //              + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
        //    DataSet ds = new DataSet();
        //    ds = clsDAL.SimpleQuery(qry);
        //    #region[from query]



        //    string qrynm = clsCommon.getString("select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    string comnm = qrynm.ToUpper();
        //    string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //string address = clsCommon.getString("select Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //string city = clsCommon.getString("select City_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    string state = clsCommon.getString("select State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    #endregion

        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataTable dt = new DataTable();

        //            dt = ds.Tables[0];

        //            //string address = clsCommon.getString("select Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //            //string city = clsCommon.getString("select City_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //            //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //            //string state = clsCommon.getString("select State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        //            #region[colummn add]
        //            dt.Columns.Add(new DataColumn("Supply Type", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Sub Type", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Doc Type", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Product", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Description", typeof(string)));
        //            dt.Columns.Add(new DataColumn("HSN", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
        //            dt.Columns.Add(new DataColumn("CESS Amount", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans Mode", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Distance level (Km)", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans Name", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans ID", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans DocNo", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans Date", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Doc No",typeof(Int32)));
        //            dt.Columns.Add(new DataColumn("From_Address2", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_Address1", typeof(string)));
        //            // dt.Columns.Add(new DataColumn("To_Address2", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_OtherPartyName", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_GSTIN", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_Place", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_Pin Code", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_State", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Dispatch_State", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Total_Invoice_Value", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Vehical_Type", typeof(string)));

        //            #endregion
        //            string vno;
        //            string to_ac_name_e;
        //            string to_address1;
        //            string to_address2;
        //            string to_place;
        //            string from_partnm;
        //            string from_address;
        //            string from_address2;
        //            string from_city;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {

        //                #region[default value]
        //                dt.Rows[i]["Supply Type"] = "Outward";
        //                dt.Rows[i]["Sub Type"] = "Supply";
        //                dt.Rows[i]["Doc Type"] = "Tax Invoice";
        //                dt.Rows[i]["Product"] = "SUGAR";
        //                dt.Rows[i]["Description"] = "SUGAR";
        //                dt.Rows[i]["HSN"] = "1701";
        //                dt.Rows[i]["Unit"] = "QUINTAL";
        //                dt.Rows[i]["CESS Amount"] = "0";
        //                dt.Rows[i]["Trans Mode"] = "Road";
        //                //dt.Rows[i]["Distance level (Km)"] = "0";
        //                dt.Rows[i]["Trans Name"] = " ";
        //                dt.Rows[i]["Trans ID"] = " ";
        //                dt.Rows[i]["Trans DocNo"] = " ";
        //                dt.Rows[i]["Trans Date"] = " ";
        //                // dt.Rows[i]["State_Name"] = dt.Rows[i]["BillToStateCode"];

        //                #region[replace from adreess]
        //                from_address = dt.Rows[i]["millname"].ToString();

        //                //to_address1 = to_address1.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                from_address = from_address.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                from_address = from_address.Replace("(", "");
        //                from_address = from_address.Replace(")", "");
        //                from_address = from_address.Replace(":", "");
        //                from_address = from_address.Replace("_", "");
        //                from_address = from_address.Replace("@", "");
        //                from_address = from_address.Replace(";", "");
        //                from_address = from_address.Replace("=", "");

        //                dt.Rows[i]["From_Address1"] = from_address;


        //                //dt.Rows[i]["From_Address2"] = dt.Rows[i]["milladdress"].ToString();
        //                #endregion

        //                #region[from address2]
        //                from_address2 = dt.Rows[i]["milladdress"].ToString();
        //                from_address2 = from_address2.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                from_address2 = from_address2.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                from_address2 = from_address2.Replace("/", "");
        //                from_address2 = from_address2.Replace(",", "");
        //                from_address2 = from_address2.Replace("(", "");
        //                from_address2 = from_address2.Replace(")", "");
        //                from_address2 = from_address2.Replace(":", "");
        //                from_address2 = from_address2.Replace("_", "");
        //                from_address2 = from_address2.Replace("@", "");
        //                from_address2 = from_address2.Replace(";", "");
        //                from_address2 = from_address2.Replace("=", "");
        //                from_address2 = from_address2.Replace("-", "");
        //                from_address2 = from_address2.Replace(",", "");
        //                from_address2 = from_address2.Replace(".", "");
        //                dt.Rows[i]["From_Address2"] = from_address2;

        //                #endregion


        //                #region[replace from other party name]
        //                from_partnm = comnm;
        //                from_partnm = from_partnm.Replace("-", "");
        //                from_partnm = from_partnm.Replace("/", "");
        //                from_partnm = from_partnm.Replace("&", "");
        //                from_partnm = from_partnm.Replace(".", "");
        //                from_partnm = from_partnm.Replace("#", "");
        //                from_partnm = from_partnm.Replace("(", "");
        //                from_partnm = from_partnm.Replace(")", "");
        //                from_partnm = from_partnm.Replace(":", "");
        //                from_partnm = from_partnm.Replace("_", "");
        //                from_partnm = from_partnm.Replace("@", "");
        //                from_partnm = from_partnm.Replace(";", "");
        //                from_partnm = from_partnm.Replace("=", "");
        //                dt.Rows[i]["From_OtherPartyName"] = from_partnm;
        //                #endregion

        //                dt.Rows[i]["From_GSTIN"] = gstno;

        //                #region[replace from place]
        //                from_city = dt.Rows[i]["millcityname"].ToString();

        //                //to_address1 = to_address1.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                from_city = from_city.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                from_city = from_city.Replace("(", "");
        //                from_city = from_city.Replace(")", "");
        //                from_city = from_city.Replace(":", "");
        //                from_city = from_city.Replace("_", "");
        //                from_city = from_city.Replace("@", "");
        //                from_city = from_city.Replace(";", "");
        //                from_city = from_city.Replace("=", "");
        //                dt.Rows[i]["From_Place"] = from_city;
        //                #endregion

        //                dt.Rows[i]["From_Pin Code"] = dt.Rows[i]["millpincode"].ToString();
        //                dt.Rows[i]["From_State"] = state;
        //                dt.Rows[i]["Dispatch_State"] = dt.Rows[i]["millstatename"].ToString();

        //                double Cess_Amount =Convert.ToDouble (dt.Rows[i]["CESS Amount"].ToString());
        //                double CGST_amount = Convert.ToDouble(dt.Rows[i]["CGSTAmount"].ToString());
        //                double SGST_amount = Convert.ToDouble(dt.Rows[i]["SGSTAmount"].ToString());
        //                double IGST_amount = Convert.ToDouble(dt.Rows[i]["IGSTAmount"].ToString());
        //                double Acessible_Value = Convert.ToDouble(dt.Rows[i]["TaxableAmount"].ToString());

        //                double TotalInvoiveAmount = Cess_Amount + CGST_amount + SGST_amount + IGST_amount + Acessible_Value;

        //                dt.Rows[i]["Total_Invoice_Value"] = TotalInvoiveAmount.ToString();


        //                #endregion

        //                #region[replace vehicle no]
        //                vno = Convert.ToString(dt.Rows[i]["LORRYNO"]);
        //                vno = vno.Replace("-", "");
        //                vno = vno.Replace("/", "");
        //                vno = vno.Replace(" ", "");
        //                vno = vno.Replace("&", "");
        //                vno = vno.Replace(".", "");
        //                vno = vno.Replace("#", "");
        //                vno = vno.Replace("(", "");
        //                vno = vno.Replace(")", "");
        //                vno = vno.Replace(":", "");
        //                vno = vno.Replace("_", "");
        //                vno = vno.Replace("@", "");
        //                vno = vno.Replace(";", "");
        //                vno = vno.Replace("=", "");
        //                dt.Rows[i]["LORRYNO"] = vno;
        //                #endregion

        //                #region[replae to party name]
        //                to_ac_name_e = Convert.ToString(dt.Rows[i]["BillToName"]);
        //                to_ac_name_e = to_ac_name_e.Replace("-", "");
        //                to_ac_name_e = to_ac_name_e.Replace("/", "");
        //                to_ac_name_e = to_ac_name_e.Replace("&", "");
        //                to_ac_name_e = to_ac_name_e.Replace(".", "");
        //                to_ac_name_e = to_ac_name_e.Replace("#", "");
        //                to_ac_name_e = to_ac_name_e.Replace("(", "");
        //                to_ac_name_e = to_ac_name_e.Replace(")", "");
        //                to_ac_name_e = to_ac_name_e.Replace(":", "");
        //                to_ac_name_e = to_ac_name_e.Replace("_", "");
        //                to_ac_name_e = to_ac_name_e.Replace("@", "");
        //                to_ac_name_e = to_ac_name_e.Replace(";", "");
        //                to_ac_name_e = to_ac_name_e.Replace("=", "");
        //                dt.Rows[i]["BillToName"] = to_ac_name_e;
        //                #endregion

        //                #region[replace to address1]
        //                to_address1 = Convert.ToString(dt.Rows[i]["ShippTo"]);
        //                to_address1 = to_address1.Replace("-", "");
        //                to_address1 = to_address1.Replace("/", "");
        //                to_address1 = to_address1.Replace("&", "");
        //                to_address1 = to_address1.Replace(".", "");
        //                to_address1 = to_address1.Replace("#", "");
        //                to_address1 = to_address1.Replace(",", "");
        //                to_address1 = to_address1.Replace("(", "");
        //                to_address1 = to_address1.Replace(")", "");
        //                to_address1 = to_address1.Replace(":", "");
        //                to_address1 = to_address1.Replace("_", "");
        //                to_address1 = to_address1.Replace("@", "");
        //                to_address1 = to_address1.Replace(";", "");
        //                to_address1 = to_address1.Replace("=", "");
        //                dt.Rows[i]["ShippTo"] = to_address1;
        //                //dt.Rows[i]["To_Address2"] = to_address1;
        //                #endregion

        //                #region[replace to address2]
        //                to_address2 = Convert.ToString(dt.Rows[i]["Address_E"]);
        //                to_address2 = to_address2.Replace("-", "");
        //                to_address1 = to_address1.Replace("/", "");
        //                to_address1 = to_address1.Replace("&", "");
        //                to_address2 = to_address2.Replace(".", "");
        //                to_address1 = to_address1.Replace("#", "");
        //                to_address2 = to_address2.Replace(",", "");
        //                to_address2 = to_address2.Replace("(", "");
        //                to_address2 = to_address2.Replace(")", "");
        //                to_address2 = to_address2.Replace(":", "");
        //                to_address2 = to_address2.Replace("_", "");
        //                to_address2 = to_address2.Replace("@", "");
        //                to_address2 = to_address2.Replace(";", "");
        //                to_address2 = to_address2.Replace("=", "");
        //                to_address2 = to_address2.Replace("*", "");
        //                to_address2 = to_address2.Replace(":", "");
        //                to_address2 = to_address2.Replace(";", "");
        //                dt.Rows[i]["Address_E"] = to_address2;
        //                //dt.Rows[i]["To_Address2"] = to_address1;
        //                #endregion

        //                #region[replace to place]
        //                to_place = Convert.ToString(dt.Rows[i]["city_name_e"]);
        //                //to_address1 = to_address1.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                to_place = to_place.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                to_place = to_place.Replace("(", "");
        //                to_place = to_place.Replace(")", "");
        //                to_place = to_place.Replace(":", "");
        //                to_place = to_place.Replace("_", "");
        //                to_place = to_place.Replace("@", "");
        //                to_place = to_place.Replace(";", "");
        //                to_place = to_place.Replace("=", "");
        //                dt.Rows[i]["city_name_e"] = to_place;
        //                //  dt.Rows[i]["Ship_To_State"] = dt.Rows[i]["State_Name"];
        //                #endregion

        //                double to_Distance;
        //                #region[replace to Distance]
        //                to_Distance = Convert.ToDouble(dt.Rows[i]["Distance"]);


        //                dt.Rows[i]["Distance"] = to_Distance;

        //                dt.Rows[i]["Vehical_Type"] = "Regular";
        //                #endregion


        //            }

        //            lblSummary.Text = "E Way Bill";

        //            grdAll.DataSource = dt;

        //            #region[sequence]
        //            dt.Columns["Supply Type"].SetOrdinal(0);
        //            dt.Columns["Sub Type"].SetOrdinal(1);
        //            dt.Columns["Doc Type"].SetOrdinal(2);
        //            dt.Columns["From_OtherPartyName"].SetOrdinal(5);
        //            dt.Columns["From_GSTIN"].SetOrdinal(6);
        //            dt.Columns["From_Address1"].SetOrdinal(7);
        //            dt.Columns["From_Address2"].SetOrdinal(8);
        //            dt.Columns["From_Place"].SetOrdinal(9);
        //            dt.Columns["From_Pin Code"].SetOrdinal(10);
        //            dt.Columns["From_State"].SetOrdinal(11);
        //            dt.Columns["Dispatch_State"].SetOrdinal(12);
        //            dt.Columns["State_Name"].SetOrdinal(20);
        //            dt.Columns["Product"].SetOrdinal(21);
        //            dt.Columns["Description"].SetOrdinal(22);
        //            dt.Columns["HSN"].SetOrdinal(23);
        //            dt.Columns["Unit"].SetOrdinal(24);
        //            dt.Columns["NETQNTL"].SetOrdinal(25);
        //            dt.Columns["CESS Amount"].SetOrdinal(31);
        //            dt.Columns["Total_Invoice_Value"].SetOrdinal(32);
        //            dt.Columns["Trans Mode"].SetOrdinal(33);
        //            // dt.Columns["Distance level (Km)"].SetOrdinal(31);
        //            dt.Columns["Trans Name"].SetOrdinal(35);
        //            dt.Columns["Trans ID"].SetOrdinal(36);
        //            dt.Columns["Trans DocNo"].SetOrdinal(37);
        //            dt.Columns["Trans Date"].SetOrdinal(38);
        //            dt.Columns["Vehical_Type"].SetOrdinal(40);
        //            //dt.Columns["millname"].SetOrdinal(40);
        //            //dt.Columns[""].SetOrdinal(35);
        //            #endregion

        //            grdAll.DataBind();

        //            #region[header name]
        //            grdAll.HeaderRow.Cells[3].Text = "Doc No";
        //            grdAll.HeaderRow.Cells[4].Text = "Doc Date";
        //            // grdAll.HeaderRow.Cells[5].Text = "From_OtherPartyName";
        //            //  grdAll.HeaderRow.Cells[6].Text = "From_GSTIN";
        //            //grdAll.HeaderRow.Cells[7].Text = "From_Address1";
        //            //grdAll.HeaderRow.Cells[9].Text = "From_Place";
        //            //grdAll.HeaderRow.Cells[10].Text = "From_Pin Code";
        //            grdAll.HeaderRow.Cells[12].Text = "Dispatch State";
        //            grdAll.HeaderRow.Cells[13].Text = "To_OtherPartyName";
        //            grdAll.HeaderRow.Cells[14].Text = "To_GSTIN";
        //            grdAll.HeaderRow.Cells[15].Text = "To_Address1";
        //            grdAll.HeaderRow.Cells[16].Text = "To_Address2";
        //            grdAll.HeaderRow.Cells[17].Text = "To_Place";
        //            grdAll.HeaderRow.Cells[18].Text = "To_Pin Code";
        //            grdAll.HeaderRow.Cells[19].Text = "To_State";
        //            grdAll.HeaderRow.Cells[20].Text = "Ship To State";
        //            grdAll.HeaderRow.Cells[25].Text = "Qty";
        //            grdAll.HeaderRow.Cells[26].Text = "Assessable Value";
        //            grdAll.HeaderRow.Cells[27].Text = "Tax Rate(C+S+I+Cess)";
        //            grdAll.HeaderRow.Cells[28].Text = "CGST Amount";
        //            grdAll.HeaderRow.Cells[29].Text = "SGST Amount";
        //            grdAll.HeaderRow.Cells[30].Text = "IGST Amount";
        //            grdAll.HeaderRow.Cells[34].Text = "Distance level (Km)";
        //            grdAll.HeaderRow.Cells[39].Text = "Vehicel No";

        //            #endregion

        //            foreach (GridViewRow gvr in grdAll.Rows)
        //            {
        //                grdAll.HeaderRow.Cells[41].Visible = false;
        //                grdAll.HeaderRow.Cells[42].Visible = false;
        //                grdAll.HeaderRow.Cells[43].Visible = false;
        //                grdAll.HeaderRow.Cells[44].Visible = false;
        //                grdAll.HeaderRow.Cells[45].Visible = false;
        //                gvr.Cells[41].Visible = false;
        //                gvr.Cells[42].Visible = false;
        //                gvr.Cells[43].Visible = false;
        //                gvr.Cells[44].Visible = false;
        //                gvr.Cells[45].Visible = false;
        //            }


        //            //if (grdAll.Columns.Count > 0)
        //            //{
        //            //    grdAll.Columns[40].Visible = false;
        //            //}
        //            //else
        //            //{
        //            //    grdAll.HeaderRow.Cells[40].Visible = false;
        //            //    foreach (GridViewRow gvr in grdAll.Rows)
        //            //    {
        //            //        gvr.Cells[40].Visible = false;
        //            //    }
        //            //}



        //            //  e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
        //            //  grdAll.HeaderRow.Cells[4].Width = new Unit("10px");
        //            // grdAll.HeaderRow.Cells[4].ControlStyle.Width = new Unit("10px");

        //        }
        //    }

        //}
        //catch
        //{
        //}
    }



    protected void btnEmpty_E_Way_Bil_Click(object sender, EventArgs e)
    {
        try
        {
            grdAll.DataSource = null;
            grdAll.DataBind();

            //grdAll.DataBind();
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            #region[comment]

            #endregion


            string qry = "select doc_no,CONVERT(varchar,doc_date,103) as doc_date,"
                      + " UPPER(BillToName) as BillToName,UPPER(BillToGst) as BillToGst ,UPPER(Ac_Name_E) as ShippTo,UPPER(Address_E) as Address_E,UPPER(city_name_e) as city_name_e,(case Pincode when 0 then 999999  else pincode end) as pincode,upper(BillToStateCode) AS BillToStateCode,UPPER(State_Name) as State_Name,NETQNTL,TaxableAmount,"
                      + "(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0'+'+'+'0') as Taxrate,CGSTAmount,"
                      + "SGSTAmount,IGSTAmount,Distance,LORRYNO,UPPER(millname) as millname,upper(milladdress) as milladdress,(case millpincode when 0 then 999999  else millpincode end) as millpincode,millcityname,DO_No,upper(millstatename) as millstatename,convert(varchar,TransDate,103)as TransDate"
                      + " from NT_1_qryNameEwayBill where doc_date between '" + fromdt + "' and '" + todt
                      + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and (Eway_Bill_No is null or Eway_Bill_No=0 )";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            #region[from query]
            string qrynm = clsCommon.getString("select UPPER(Company_Name_E) as Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string comnm = qrynm.ToUpper();
            string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string address = clsCommon.getString("select UPPER(Address_E) as Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string city = clsCommon.getString("select UPPER(City_E) as City_E  from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string state = clsCommon.getString("select UPPER(State_E) as State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            #endregion

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    #region[colummn add]
                    dt.Columns.Add(new DataColumn("Supply Type", typeof(string)));
                    dt.Columns.Add(new DataColumn("Sub Type", typeof(string)));
                    dt.Columns.Add(new DataColumn("Doc Type", typeof(string)));
                    dt.Columns.Add(new DataColumn("Transaction Type", typeof(string)));

                    dt.Columns.Add(new DataColumn("Product", typeof(string)));
                    dt.Columns.Add(new DataColumn("Description", typeof(string)));
                    dt.Columns.Add(new DataColumn("HSN", typeof(string)));
                    dt.Columns.Add(new DataColumn("Unit", typeof(string)));
                    dt.Columns.Add(new DataColumn("CESS Amount", typeof(string)));
                    dt.Columns.Add(new DataColumn("CESS Non Advol Amount", typeof(string)));
                    dt.Columns.Add(new DataColumn("Other", typeof(string)));
                    dt.Columns.Add(new DataColumn("Total Invoice Value", typeof(string)));

                    dt.Columns.Add(new DataColumn("Trans Mode", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Distance level (Km)", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans ID", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans DocNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("Trans Date", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Doc No",typeof(Int32)));
                    dt.Columns.Add(new DataColumn("From_Address2", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_Address1", typeof(string)));
                    // dt.Columns.Add(new DataColumn("To_Address2", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_OtherPartyName", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_GSTIN", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_Place", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_Pin Code", typeof(string)));
                    dt.Columns.Add(new DataColumn("From_State", typeof(string)));
                    dt.Columns.Add(new DataColumn("Dispatch_State", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));
                    //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));

                    dt.Columns.Add(new DataColumn("Vehical_Type", typeof(string)));
                    #endregion
                    string vno;
                    string to_ac_name_e;
                    string to_address1;
                    string to_address2;
                    string to_place;
                    string from_partnm;
                    string from_address;
                    string from_city;
                    string do_no = "";

                    string Bill_To;
                    string Ship_To;
                    double taxamount;
                    string taxvalue;
                    double CGST;
                    double SGST;
                    double IGST;
                    double CessAmt = 0.00;
                    double CessNontAdvol = 0.00;
                    double Other = 0.00;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region[default value]
                        dt.Rows[i]["Supply Type"] = "Outward";
                        dt.Rows[i]["Sub Type"] = "Supply";
                        dt.Rows[i]["Doc Type"] = "Tax Invoice";
                        dt.Rows[i]["Product"] = "SUGAR";
                        dt.Rows[i]["Description"] = "SUGAR";
                        dt.Rows[i]["HSN"] = "1701";
                        dt.Rows[i]["Unit"] = "QUINTAL";
                        dt.Rows[i]["CESS Amount"] = "0";

                        dt.Rows[i]["CESS Non Advol Amount"] = "0.00";
                        dt.Rows[i]["Other"] = "0.00";
                        dt.Rows[i]["Trans Mode"] = "ROAD";


                        //dt.Rows[i]["Distance level (Km)"] = "0";
                        dt.Rows[i]["Trans Name"] = " ";
                        dt.Rows[i]["Trans ID"] = " ";
                        dt.Rows[i]["Trans DocNo"] = " ";
                        dt.Rows[i]["Trans Date"] = dt.Rows[i]["TransDate"].ToString();
                        // dt.Rows[i]["State_Name"] = dt.Rows[i]["BillToStateCode"];

                        #region[replace from adreess]
                        //from_address = address;
                        from_address = dt.Rows[i]["milladdress"].ToString();

                        //to_address1 = to_address1.Replace("-", "");
                        //to_address1 = to_address1.Replace("/", "");
                        //to_address1 = to_address1.Replace("&", "");
                        from_address = from_address.Replace(".", "");
                        //to_address1 = to_address1.Replace("#", "");
                        //to_address1 = to_address1.Replace(",", "");
                        from_address = from_address.Replace("(", "");
                        from_address = from_address.Replace(")", "");
                        from_address = from_address.Replace(":", "");
                        from_address = from_address.Replace("_", "");
                        from_address = from_address.Replace("@", "");
                        from_address = from_address.Replace(";", "");
                        from_address = from_address.Replace("=", "");
                        dt.Rows[i]["From_Address2"] = from_address;
                        dt.Rows[i]["From_Address1"] = dt.Rows[i]["millname"].ToString();
                        #endregion

                        #region[replace from other party name]
                        from_partnm = comnm;
                        from_partnm = from_partnm.Replace("-", "");
                        from_partnm = from_partnm.Replace("/", "");
                        from_partnm = from_partnm.Replace("&", "");
                        from_partnm = from_partnm.Replace(".", "");
                        from_partnm = from_partnm.Replace("#", "");
                        from_partnm = from_partnm.Replace("(", "");
                        from_partnm = from_partnm.Replace(")", "");
                        from_partnm = from_partnm.Replace(":", "");
                        from_partnm = from_partnm.Replace("_", "");
                        from_partnm = from_partnm.Replace("@", "");
                        from_partnm = from_partnm.Replace(";", "");
                        from_partnm = from_partnm.Replace("=", "");
                        dt.Rows[i]["From_OtherPartyName"] = from_partnm;
                        #endregion

                        dt.Rows[i]["From_GSTIN"] = gstno;

                        #region[replace from place]
                        //from_city = city;
                        from_city = dt.Rows[i]["millcityname"].ToString();

                        //to_address1 = to_address1.Replace("-", "");
                        //to_address1 = to_address1.Replace("/", "");
                        //to_address1 = to_address1.Replace("&", "");
                        from_city = from_city.Replace(".", "");
                        //to_address1 = to_address1.Replace("#", "");
                        //to_address1 = to_address1.Replace(",", "");
                        from_city = from_city.Replace("(", "");
                        from_city = from_city.Replace(")", "");
                        from_city = from_city.Replace(":", "");
                        from_city = from_city.Replace("_", "");
                        from_city = from_city.Replace("@", "");
                        from_city = from_city.Replace(";", "");
                        from_city = from_city.Replace("=", "");
                        dt.Rows[i]["From_Place"] = from_city;
                        #endregion

                        //dt.Rows[i]["From_Pin Code"] = pin;
                        dt.Rows[i]["From_Pin Code"] = dt.Rows[i]["millpincode"].ToString();
                        dt.Rows[i]["From_State"] = state;
                        dt.Rows[i]["Dispatch_State"] = dt.Rows[i]["millstatename"].ToString();


                        #endregion


                        Bill_To = dt.Rows[i]["BillToName"].ToString();
                        Ship_To = dt.Rows[i]["ShippTo"].ToString();
                        if (Bill_To != Ship_To)
                        {
                            dt.Rows[i]["Transaction Type"] = "Combination of 2 and 3";

                        }
                        else
                        {
                            dt.Rows[i]["Transaction Type"] = "Bill From-Dispatch From";

                        }

                        taxamount = Convert.ToDouble(dt.Rows[i]["TaxableAmount"].ToString());
                        CGST = Convert.ToDouble(dt.Rows[i]["CGSTAmount"].ToString());
                        SGST = Convert.ToDouble(dt.Rows[i]["SGSTAmount"].ToString());
                        IGST = Convert.ToDouble(dt.Rows[i]["IGSTAmount"].ToString());
                        // taxvalue = (dt.Rows[i]["Taxrate"].ToString());
                        //string TotaInvoice = (taxamount + '+' + taxvalue + '0').ToString();

                        //dt.Rows[i]["Total Invoice Value"] = TotaInvoice;
                        double TotalInvoice = taxamount + CGST + SGST + IGST + CessAmt + CessNontAdvol + Other;

                        dt.Rows[i]["Total Invoice Value"] = TotalInvoice;

                        #region[replace vehicle no]
                        vno = Convert.ToString(dt.Rows[i]["LORRYNO"]);
                        vno = vno.Replace("-", "");
                        vno = vno.Replace("/", "");
                        vno = vno.Replace(" ", "");
                        vno = vno.Replace("&", "");
                        vno = vno.Replace(".", "");
                        vno = vno.Replace("#", "");
                        vno = vno.Replace("(", "");
                        vno = vno.Replace(")", "");
                        vno = vno.Replace(":", "");
                        vno = vno.Replace("_", "");
                        vno = vno.Replace("@", "");
                        vno = vno.Replace(";", "");
                        vno = vno.Replace("=", "");
                        dt.Rows[i]["LORRYNO"] = vno;
                        #endregion

                        #region[replae to party name]
                        to_ac_name_e = Convert.ToString(dt.Rows[i]["BillToName"]);
                        to_ac_name_e = to_ac_name_e.Replace("-", "");
                        to_ac_name_e = to_ac_name_e.Replace("/", "");
                        to_ac_name_e = to_ac_name_e.Replace("&", "");
                        to_ac_name_e = to_ac_name_e.Replace(".", "");
                        to_ac_name_e = to_ac_name_e.Replace("#", "");
                        to_ac_name_e = to_ac_name_e.Replace("(", "");
                        to_ac_name_e = to_ac_name_e.Replace(")", "");
                        to_ac_name_e = to_ac_name_e.Replace(":", "");
                        to_ac_name_e = to_ac_name_e.Replace("_", "");
                        to_ac_name_e = to_ac_name_e.Replace("@", "");
                        to_ac_name_e = to_ac_name_e.Replace(";", "");
                        to_ac_name_e = to_ac_name_e.Replace("=", "");
                        dt.Rows[i]["BillToName"] = to_ac_name_e;
                        #endregion

                        #region[replace to address1]
                        to_address1 = Convert.ToString(dt.Rows[i]["ShippTo"]);
                        to_address1 = to_address1.Replace("-", "");
                        to_address1 = to_address1.Replace("/", "");
                        to_address1 = to_address1.Replace("&", "");
                        to_address1 = to_address1.Replace(".", "");
                        to_address1 = to_address1.Replace("#", "");
                        to_address1 = to_address1.Replace(",", "");
                        to_address1 = to_address1.Replace("(", "");
                        to_address1 = to_address1.Replace(")", "");
                        to_address1 = to_address1.Replace(":", "");
                        to_address1 = to_address1.Replace("_", "");
                        to_address1 = to_address1.Replace("@", "");
                        to_address1 = to_address1.Replace(";", "");
                        to_address1 = to_address1.Replace("=", "");
                        dt.Rows[i]["ShippTo"] = to_address1;
                        //dt.Rows[i]["To_Address2"] = to_address1;
                        #endregion

                        #region[replace to address2]
                        to_address2 = Convert.ToString(dt.Rows[i]["Address_E"]);
                        to_address2 = to_address2.Replace("-", "");
                        to_address1 = to_address1.Replace("/", "");
                        to_address1 = to_address1.Replace("&", "");
                        to_address2 = to_address2.Replace(".", "");
                        to_address1 = to_address1.Replace("#", "");
                        to_address2 = to_address2.Replace(",", "");
                        to_address2 = to_address2.Replace("(", "");
                        to_address2 = to_address2.Replace(")", "");
                        to_address2 = to_address2.Replace(":", "");
                        to_address2 = to_address2.Replace("_", "");
                        to_address2 = to_address2.Replace("@", "");
                        to_address2 = to_address2.Replace(";", "");
                        to_address2 = to_address2.Replace("=", "");
                        to_address2 = to_address2.Replace("*", "");
                        to_address2 = to_address2.Replace(":", "");
                        to_address2 = to_address2.Replace(";", "");
                        dt.Rows[i]["Address_E"] = to_address2;
                        //dt.Rows[i]["To_Address2"] = to_address1;
                        #endregion

                        #region[replace to place]
                        to_place = Convert.ToString(dt.Rows[i]["city_name_e"]);
                        //to_address1 = to_address1.Replace("-", "");
                        //to_address1 = to_address1.Replace("/", "");
                        //to_address1 = to_address1.Replace("&", "");
                        to_place = to_place.Replace(".", "");
                        //to_address1 = to_address1.Replace("#", "");
                        //to_address1 = to_address1.Replace(",", "");
                        to_place = to_place.Replace("(", "");
                        to_place = to_place.Replace(")", "");
                        to_place = to_place.Replace(":", "");
                        to_place = to_place.Replace("_", "");
                        to_place = to_place.Replace("@", "");
                        to_place = to_place.Replace(";", "");
                        to_place = to_place.Replace("=", "");
                        dt.Rows[i]["city_name_e"] = to_place;
                        //  dt.Rows[i]["Ship_To_State"] = dt.Rows[i]["State_Name"];
                        #endregion

                        double to_Distance;
                        #region[replace to Distance]
                        to_Distance = Convert.ToDouble(dt.Rows[i]["Distance"]);


                        dt.Rows[i]["Distance"] = to_Distance;

                        dt.Rows[i]["Vehical_Type"] = "REGULAR";
                        #endregion



                    }

                    lblSummary.Text = "Empty E Way Bill";

                    grdAll.DataSource = dt;

                    #region[sequence]
                    dt.Columns["Supply Type"].SetOrdinal(0);
                    dt.Columns["Sub Type"].SetOrdinal(1);
                    dt.Columns["Doc Type"].SetOrdinal(2);
                    dt.Columns["Transaction Type"].SetOrdinal(5);

                    dt.Columns["From_OtherPartyName"].SetOrdinal(6);
                    dt.Columns["From_GSTIN"].SetOrdinal(7);
                    dt.Columns["From_Address1"].SetOrdinal(8);
                    dt.Columns["From_Address2"].SetOrdinal(9);
                    dt.Columns["From_Place"].SetOrdinal(10);
                    dt.Columns["From_Pin Code"].SetOrdinal(11);
                    dt.Columns["From_State"].SetOrdinal(12);
                    dt.Columns["Dispatch_State"].SetOrdinal(13);
                    dt.Columns["State_Name"].SetOrdinal(21);
                    dt.Columns["Product"].SetOrdinal(22);
                    dt.Columns["Description"].SetOrdinal(23);
                    dt.Columns["HSN"].SetOrdinal(24);
                    dt.Columns["Unit"].SetOrdinal(25);
                    dt.Columns["NETQNTL"].SetOrdinal(26);
                    dt.Columns["CESS Amount"].SetOrdinal(32);
                    dt.Columns["CESS Non Advol Amount"].SetOrdinal(33);
                    dt.Columns["Other"].SetOrdinal(34);
                    dt.Columns["Total Invoice Value"].SetOrdinal(35);
                    dt.Columns["Trans Mode"].SetOrdinal(36);
                    // dt.Columns["Distance level (Km)"].SetOrdinal(31);
                    dt.Columns["Trans Name"].SetOrdinal(38);
                    dt.Columns["Trans ID"].SetOrdinal(39);
                    dt.Columns["Trans DocNo"].SetOrdinal(40);
                    dt.Columns["Trans Date"].SetOrdinal(41);
                    dt.Columns["Vehical_Type"].SetOrdinal(43);
                    dt.Columns["DO_No"].SetOrdinal(44);
                    dt.Columns["millname"].SetOrdinal(45);

                    #endregion

                    grdAll.DataBind();

                    #region[header name]
                    grdAll.HeaderRow.Cells[3].Text = "Doc No";
                    grdAll.HeaderRow.Cells[4].Text = "Doc Date";
                    // grdAll.HeaderRow.Cells[5].Text = "From_OtherPartyName";
                    //  grdAll.HeaderRow.Cells[6].Text = "From_GSTIN";
                    //grdAll.HeaderRow.Cells[7].Text = "From_Address1";
                    //grdAll.HeaderRow.Cells[9].Text = "From_Place";
                    //grdAll.HeaderRow.Cells[10].Text = "From_Pin Code";
                    grdAll.HeaderRow.Cells[13].Text = "Dispatch State";
                    grdAll.HeaderRow.Cells[14].Text = "To_OtherPartyName";
                    grdAll.HeaderRow.Cells[15].Text = "To_GSTIN";
                    grdAll.HeaderRow.Cells[16].Text = "To_Address1";
                    grdAll.HeaderRow.Cells[17].Text = "To_Address2";
                    grdAll.HeaderRow.Cells[18].Text = "To_Place";
                    grdAll.HeaderRow.Cells[19].Text = "To_Pin Code";
                    grdAll.HeaderRow.Cells[20].Text = "To_State";
                    grdAll.HeaderRow.Cells[21].Text = "Ship To State";
                    grdAll.HeaderRow.Cells[26].Text = "Qty";
                    grdAll.HeaderRow.Cells[27].Text = "Assessable Value";
                    grdAll.HeaderRow.Cells[28].Text = "Tax Rate(C+S+I+Cess+CESS Non Advol Amount)";
                    grdAll.HeaderRow.Cells[29].Text = "CGST Amount";
                    grdAll.HeaderRow.Cells[30].Text = "SGST Amount";
                    grdAll.HeaderRow.Cells[31].Text = "IGST Amount";
                    grdAll.HeaderRow.Cells[37].Text = "Distance level (Km)";
                    grdAll.HeaderRow.Cells[42].Text = "Vehicel No";


                    #endregion

                    foreach (GridViewRow gvr in grdAll.Rows)
                    {
                        grdAll.HeaderRow.Cells[45].Visible = true;
                        grdAll.HeaderRow.Cells[46].Visible = false;
                        grdAll.HeaderRow.Cells[47].Visible = false;
                        grdAll.HeaderRow.Cells[48].Visible = false;
                        grdAll.HeaderRow.Cells[49].Visible = false;
                        grdAll.HeaderRow.Cells[50].Visible = false;



                        //grdAll.HeaderRow.Cells[44].Visible = false;
                        //grdAll.HeaderRow.Cells[45].Visible = false;
                        //grdAll.HeaderRow.Cells[46].Visible = false;
                        //grdAll.HeaderRow.Cells[47].Visible = false;

                        gvr.Cells[45].Visible = true;
                        gvr.Cells[46].Visible = false;
                        gvr.Cells[47].Visible = false;
                        gvr.Cells[48].Visible = false;
                        gvr.Cells[49].Visible = false;
                        gvr.Cells[50].Visible = false;

                        //gvr.Cells[44].Visible = false;
                        //gvr.Cells[45].Visible = false;
                        //gvr.Cells[46].Visible = false;
                        //gvr.Cells[47].Visible = false;

                    }

                    //  e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
                    //  grdAll.HeaderRow.Cells[4].Width = new Unit("10px");
                    // grdAll.HeaderRow.Cells[4].ControlStyle.Width = new Unit("10px");

                }
            }

        }
        catch
        {
        }
        //try
        //{
        //    //  grdAll.DataBind() = null;
        //    //grdAll.DataSource = null;
        //    //grdAll.DataBind();
        //    string fromdt = txtFromDt.Text;
        //    string todt = txtToDt.Text;
        //    fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //    todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        //    #region[comment]

        //    #endregion


        //    string qry = "select doc_no,CONVERT(varchar,doc_date,103) as doc_date,"
        //              + " BillToName,BillToGst,Ac_Name_E as ShippTo,Address_E,city_name_e,(case Pincode when 0 then 999999  else pincode end) as pincode," +
        //              "upper(BillToStateCode) AS BillToStateCode,UPPER(State_Name) as State_Name,NETQNTL,TaxableAmount,"
        //              + "(convert(varchar,CGSTRate,0) + '+' + convert(varchar,SGSTRate,0)+ '+' + convert(varchar,IGSTRate,0)+'+'+'0') as Taxrate,CGSTAmount,"
        //              + "SGSTAmount,IGSTAmount,Distance,LORRYNO,millname,millpincode,milladdress,millcityname,upper(millstatename) as millstatename"
        //              + " from NT_1_qryNameEwayBill where EWay_Bill_No=0 and doc_date between '" + fromdt + "' and '" + todt
        //              + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
        //    DataSet ds = new DataSet();
        //    ds = clsDAL.SimpleQuery(qry);
        //    #region[from query]



        //    string qrynm = clsCommon.getString("select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    string comnm = qrynm.ToUpper();
        //    string gstno = clsCommon.getString("select GST from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //string address = clsCommon.getString("select Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //string city = clsCommon.getString("select City_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    string state = clsCommon.getString("select State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //    #endregion

        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataTable dt = new DataTable();

        //            dt = ds.Tables[0];

        //            //string address = clsCommon.getString("select Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //            //string city = clsCommon.getString("select City_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //            //string pin = clsCommon.getString("select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //            //string state = clsCommon.getString("select State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        //            #region[colummn add]
        //            dt.Columns.Add(new DataColumn("Supply Type", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Sub Type", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Doc Type", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Product", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Description", typeof(string)));
        //            dt.Columns.Add(new DataColumn("HSN", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
        //            dt.Columns.Add(new DataColumn("CESS Amount", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans Mode", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Distance level (Km)", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans Name", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans ID", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans DocNo", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Trans Date", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Doc No",typeof(Int32)));
        //            dt.Columns.Add(new DataColumn("From_Address2", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_Address1", typeof(string)));
        //            // dt.Columns.Add(new DataColumn("To_Address2", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_OtherPartyName", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_GSTIN", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_Place", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_Pin Code", typeof(string)));
        //            dt.Columns.Add(new DataColumn("From_State", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Dispatch_State", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));
        //            //dt.Columns.Add(new DataColumn("Ship_To_State", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Total_Invoice_Value", typeof(string)));
        //            dt.Columns.Add(new DataColumn("Vehical_Type", typeof(string)));

        //            #endregion
        //            string vno;
        //            string to_ac_name_e;
        //            string to_address1;
        //            string to_address2;
        //            string to_place;
        //            string from_partnm;
        //            string from_address;
        //            string from_address2;
        //            string from_city;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {

        //                #region[default value]
        //                dt.Rows[i]["Supply Type"] = "Outward";
        //                dt.Rows[i]["Sub Type"] = "Supply";
        //                dt.Rows[i]["Doc Type"] = "Tax Invoice";
        //                dt.Rows[i]["Product"] = "SUGAR";
        //                dt.Rows[i]["Description"] = "SUGAR";
        //                dt.Rows[i]["HSN"] = "1701";
        //                dt.Rows[i]["Unit"] = "QUINTAL";
        //                dt.Rows[i]["CESS Amount"] = "0";
        //                dt.Rows[i]["Trans Mode"] = "Road";
        //                //dt.Rows[i]["Distance level (Km)"] = "0";
        //                dt.Rows[i]["Trans Name"] = " ";
        //                dt.Rows[i]["Trans ID"] = " ";
        //                dt.Rows[i]["Trans DocNo"] = " ";
        //                dt.Rows[i]["Trans Date"] = " ";
        //                // dt.Rows[i]["State_Name"] = dt.Rows[i]["BillToStateCode"];

        //                #region[replace from adreess]
        //                from_address = dt.Rows[i]["millname"].ToString();

        //                //to_address1 = to_address1.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                from_address = from_address.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                from_address = from_address.Replace("(", "");
        //                from_address = from_address.Replace(")", "");
        //                from_address = from_address.Replace(":", "");
        //                from_address = from_address.Replace("_", "");
        //                from_address = from_address.Replace("@", "");
        //                from_address = from_address.Replace(";", "");
        //                from_address = from_address.Replace("=", "");

        //                dt.Rows[i]["From_Address1"] = from_address;


        //                //dt.Rows[i]["From_Address2"] = dt.Rows[i]["milladdress"].ToString();
        //                #endregion

        //                #region[from address2]
        //                from_address2 = dt.Rows[i]["milladdress"].ToString();
        //                from_address2 = from_address2.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                from_address2 = from_address2.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                from_address2 = from_address2.Replace("/", "");
        //                from_address2 = from_address2.Replace(",", "");
        //                from_address2 = from_address2.Replace("(", "");
        //                from_address2 = from_address2.Replace(")", "");
        //                from_address2 = from_address2.Replace(":", "");
        //                from_address2 = from_address2.Replace("_", "");
        //                from_address2 = from_address2.Replace("@", "");
        //                from_address2 = from_address2.Replace(";", "");
        //                from_address2 = from_address2.Replace("=", "");
        //                from_address2 = from_address2.Replace("-", "");
        //                from_address2 = from_address2.Replace(",", "");
        //                from_address2 = from_address2.Replace(".", "");
        //                dt.Rows[i]["From_Address2"] = from_address2;

        //                #endregion


        //                #region[replace from other party name]
        //                from_partnm = comnm;
        //                from_partnm = from_partnm.Replace("-", "");
        //                from_partnm = from_partnm.Replace("/", "");
        //                from_partnm = from_partnm.Replace("&", "");
        //                from_partnm = from_partnm.Replace(".", "");
        //                from_partnm = from_partnm.Replace("#", "");
        //                from_partnm = from_partnm.Replace("(", "");
        //                from_partnm = from_partnm.Replace(")", "");
        //                from_partnm = from_partnm.Replace(":", "");
        //                from_partnm = from_partnm.Replace("_", "");
        //                from_partnm = from_partnm.Replace("@", "");
        //                from_partnm = from_partnm.Replace(";", "");
        //                from_partnm = from_partnm.Replace("=", "");
        //                dt.Rows[i]["From_OtherPartyName"] = from_partnm;
        //                #endregion

        //                dt.Rows[i]["From_GSTIN"] = gstno;

        //                #region[replace from place]
        //                from_city = dt.Rows[i]["millcityname"].ToString();

        //                //to_address1 = to_address1.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                from_city = from_city.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                from_city = from_city.Replace("(", "");
        //                from_city = from_city.Replace(")", "");
        //                from_city = from_city.Replace(":", "");
        //                from_city = from_city.Replace("_", "");
        //                from_city = from_city.Replace("@", "");
        //                from_city = from_city.Replace(";", "");
        //                from_city = from_city.Replace("=", "");
        //                dt.Rows[i]["From_Place"] = from_city;
        //                #endregion

        //                dt.Rows[i]["From_Pin Code"] = dt.Rows[i]["millpincode"].ToString();
        //                dt.Rows[i]["From_State"] = state;
        //                dt.Rows[i]["Dispatch_State"] = dt.Rows[i]["millstatename"].ToString();

        //                double Cess_Amount = Convert.ToDouble(dt.Rows[i]["CESS Amount"].ToString());
        //                double CGST_amount = Convert.ToDouble(dt.Rows[i]["CGSTAmount"].ToString());
        //                double SGST_amount = Convert.ToDouble(dt.Rows[i]["SGSTAmount"].ToString());
        //                double IGST_amount = Convert.ToDouble(dt.Rows[i]["IGSTAmount"].ToString());
        //                double Acessible_Value = Convert.ToDouble(dt.Rows[i]["TaxableAmount"].ToString());

        //                double TotalInvoiveAmount = Cess_Amount + CGST_amount + SGST_amount + IGST_amount + Acessible_Value;

        //                dt.Rows[i]["Total_Invoice_Value"] = TotalInvoiveAmount.ToString();


        //                #endregion

        //                #region[replace vehicle no]
        //                vno = Convert.ToString(dt.Rows[i]["LORRYNO"]);
        //                vno = vno.Replace("-", "");
        //                vno = vno.Replace("/", "");
        //                vno = vno.Replace(" ", "");
        //                vno = vno.Replace("&", "");
        //                vno = vno.Replace(".", "");
        //                vno = vno.Replace("#", "");
        //                vno = vno.Replace("(", "");
        //                vno = vno.Replace(")", "");
        //                vno = vno.Replace(":", "");
        //                vno = vno.Replace("_", "");
        //                vno = vno.Replace("@", "");
        //                vno = vno.Replace(";", "");
        //                vno = vno.Replace("=", "");
        //                dt.Rows[i]["LORRYNO"] = vno;
        //                #endregion

        //                #region[replae to party name]
        //                to_ac_name_e = Convert.ToString(dt.Rows[i]["BillToName"]);
        //                to_ac_name_e = to_ac_name_e.Replace("-", "");
        //                to_ac_name_e = to_ac_name_e.Replace("/", "");
        //                to_ac_name_e = to_ac_name_e.Replace("&", "");
        //                to_ac_name_e = to_ac_name_e.Replace(".", "");
        //                to_ac_name_e = to_ac_name_e.Replace("#", "");
        //                to_ac_name_e = to_ac_name_e.Replace("(", "");
        //                to_ac_name_e = to_ac_name_e.Replace(")", "");
        //                to_ac_name_e = to_ac_name_e.Replace(":", "");
        //                to_ac_name_e = to_ac_name_e.Replace("_", "");
        //                to_ac_name_e = to_ac_name_e.Replace("@", "");
        //                to_ac_name_e = to_ac_name_e.Replace(";", "");
        //                to_ac_name_e = to_ac_name_e.Replace("=", "");
        //                dt.Rows[i]["BillToName"] = to_ac_name_e;
        //                #endregion

        //                #region[replace to address1]
        //                to_address1 = Convert.ToString(dt.Rows[i]["ShippTo"]);
        //                to_address1 = to_address1.Replace("-", "");
        //                to_address1 = to_address1.Replace("/", "");
        //                to_address1 = to_address1.Replace("&", "");
        //                to_address1 = to_address1.Replace(".", "");
        //                to_address1 = to_address1.Replace("#", "");
        //                to_address1 = to_address1.Replace(",", "");
        //                to_address1 = to_address1.Replace("(", "");
        //                to_address1 = to_address1.Replace(")", "");
        //                to_address1 = to_address1.Replace(":", "");
        //                to_address1 = to_address1.Replace("_", "");
        //                to_address1 = to_address1.Replace("@", "");
        //                to_address1 = to_address1.Replace(";", "");
        //                to_address1 = to_address1.Replace("=", "");
        //                dt.Rows[i]["ShippTo"] = to_address1;
        //                //dt.Rows[i]["To_Address2"] = to_address1;
        //                #endregion

        //                #region[replace to address2]
        //                to_address2 = Convert.ToString(dt.Rows[i]["Address_E"]);
        //                to_address2 = to_address2.Replace("-", "");
        //                to_address1 = to_address1.Replace("/", "");
        //                to_address1 = to_address1.Replace("&", "");
        //                to_address2 = to_address2.Replace(".", "");
        //                to_address1 = to_address1.Replace("#", "");
        //                to_address2 = to_address2.Replace(",", "");
        //                to_address2 = to_address2.Replace("(", "");
        //                to_address2 = to_address2.Replace(")", "");
        //                to_address2 = to_address2.Replace(":", "");
        //                to_address2 = to_address2.Replace("_", "");
        //                to_address2 = to_address2.Replace("@", "");
        //                to_address2 = to_address2.Replace(";", "");
        //                to_address2 = to_address2.Replace("=", "");
        //                to_address2 = to_address2.Replace("*", "");
        //                to_address2 = to_address2.Replace(":", "");
        //                to_address2 = to_address2.Replace(";", "");
        //                dt.Rows[i]["Address_E"] = to_address2;
        //                //dt.Rows[i]["To_Address2"] = to_address1;
        //                #endregion

        //                #region[replace to place]
        //                to_place = Convert.ToString(dt.Rows[i]["city_name_e"]);
        //                //to_address1 = to_address1.Replace("-", "");
        //                //to_address1 = to_address1.Replace("/", "");
        //                //to_address1 = to_address1.Replace("&", "");
        //                to_place = to_place.Replace(".", "");
        //                //to_address1 = to_address1.Replace("#", "");
        //                //to_address1 = to_address1.Replace(",", "");
        //                to_place = to_place.Replace("(", "");
        //                to_place = to_place.Replace(")", "");
        //                to_place = to_place.Replace(":", "");
        //                to_place = to_place.Replace("_", "");
        //                to_place = to_place.Replace("@", "");
        //                to_place = to_place.Replace(";", "");
        //                to_place = to_place.Replace("=", "");
        //                dt.Rows[i]["city_name_e"] = to_place;
        //                //  dt.Rows[i]["Ship_To_State"] = dt.Rows[i]["State_Name"];
        //                #endregion

        //                double to_Distance;
        //                #region[replace to Distance]
        //                to_Distance = Convert.ToDouble(dt.Rows[i]["Distance"]);


        //                dt.Rows[i]["Distance"] = to_Distance;

        //                dt.Rows[i]["Vehical_Type"] = "Regular";
        //                #endregion


        //            }

        //            lblSummary.Text = "Empty E Way Bill";

        //            grdAll.DataSource = dt;

        //            #region[sequence]
        //            dt.Columns["Supply Type"].SetOrdinal(0);
        //            dt.Columns["Sub Type"].SetOrdinal(1);
        //            dt.Columns["Doc Type"].SetOrdinal(2);
        //            dt.Columns["From_OtherPartyName"].SetOrdinal(5);
        //            dt.Columns["From_GSTIN"].SetOrdinal(6);
        //            dt.Columns["From_Address1"].SetOrdinal(7);
        //            dt.Columns["From_Address2"].SetOrdinal(8);
        //            dt.Columns["From_Place"].SetOrdinal(9);
        //            dt.Columns["From_Pin Code"].SetOrdinal(10);
        //            dt.Columns["From_State"].SetOrdinal(11);
        //            dt.Columns["Dispatch_State"].SetOrdinal(12);
        //            dt.Columns["State_Name"].SetOrdinal(20);
        //            dt.Columns["Product"].SetOrdinal(21);
        //            dt.Columns["Description"].SetOrdinal(22);
        //            dt.Columns["HSN"].SetOrdinal(23);
        //            dt.Columns["Unit"].SetOrdinal(24);
        //            dt.Columns["NETQNTL"].SetOrdinal(25);
        //            dt.Columns["CESS Amount"].SetOrdinal(31);
        //            dt.Columns["Total_Invoice_Value"].SetOrdinal(32);
        //            dt.Columns["Trans Mode"].SetOrdinal(33);
        //            // dt.Columns["Distance level (Km)"].SetOrdinal(31);
        //            dt.Columns["Trans Name"].SetOrdinal(35);
        //            dt.Columns["Trans ID"].SetOrdinal(36);
        //            dt.Columns["Trans DocNo"].SetOrdinal(37);
        //            dt.Columns["Trans Date"].SetOrdinal(38);
        //            dt.Columns["Vehical_Type"].SetOrdinal(40);
        //            //dt.Columns["millname"].SetOrdinal(40);
        //            //dt.Columns[""].SetOrdinal(35);
        //            #endregion


        //            grdAll.DataBind();

        //            #region[header name]
        //            grdAll.HeaderRow.Cells[3].Text = "Doc No";
        //            grdAll.HeaderRow.Cells[4].Text = "Doc Date";
        //            // grdAll.HeaderRow.Cells[5].Text = "From_OtherPartyName";
        //            //  grdAll.HeaderRow.Cells[6].Text = "From_GSTIN";
        //            //grdAll.HeaderRow.Cells[7].Text = "From_Address1";
        //            //grdAll.HeaderRow.Cells[9].Text = "From_Place";
        //            //grdAll.HeaderRow.Cells[10].Text = "From_Pin Code";
        //            grdAll.HeaderRow.Cells[12].Text = "Dispatch State";
        //            grdAll.HeaderRow.Cells[13].Text = "To_OtherPartyName";
        //            grdAll.HeaderRow.Cells[14].Text = "To_GSTIN";
        //            grdAll.HeaderRow.Cells[15].Text = "To_Address1";
        //            grdAll.HeaderRow.Cells[16].Text = "To_Address2";
        //            grdAll.HeaderRow.Cells[17].Text = "To_Place";
        //            grdAll.HeaderRow.Cells[18].Text = "To_Pin Code";
        //            grdAll.HeaderRow.Cells[19].Text = "To_State";
        //            grdAll.HeaderRow.Cells[20].Text = "Ship To State";
        //            grdAll.HeaderRow.Cells[25].Text = "Qty";
        //            grdAll.HeaderRow.Cells[26].Text = "Assessable Value";
        //            grdAll.HeaderRow.Cells[27].Text = "Tax Rate(C+S+I+Cess)";
        //            grdAll.HeaderRow.Cells[28].Text = "CGST Amount";
        //            grdAll.HeaderRow.Cells[29].Text = "SGST Amount";
        //            grdAll.HeaderRow.Cells[30].Text = "IGST Amount";
        //            grdAll.HeaderRow.Cells[34].Text = "Distance level (Km)";
        //            grdAll.HeaderRow.Cells[39].Text = "Vehicel No";

        //            #endregion

        //            foreach (GridViewRow gvr in grdAll.Rows)
        //            {
        //                grdAll.HeaderRow.Cells[41].Visible = false;
        //                grdAll.HeaderRow.Cells[42].Visible = false;
        //                grdAll.HeaderRow.Cells[43].Visible = false;
        //                grdAll.HeaderRow.Cells[44].Visible = false;
        //                grdAll.HeaderRow.Cells[45].Visible = false;
        //                gvr.Cells[41].Visible = false;
        //                gvr.Cells[42].Visible = false;
        //                gvr.Cells[43].Visible = false;
        //                gvr.Cells[44].Visible = false;
        //                gvr.Cells[45].Visible = false;
        //            }


        //            //if (grdAll.Columns.Count > 0)
        //            //{
        //            //    grdAll.Columns[40].Visible = false;
        //            //}
        //            //else
        //            //{
        //            //    grdAll.HeaderRow.Cells[40].Visible = false;
        //            //    foreach (GridViewRow gvr in grdAll.Rows)
        //            //    {
        //            //        gvr.Cells[40].Visible = false;
        //            //    }
        //            //}



        //            //  e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
        //            //  grdAll.HeaderRow.Cells[4].Width = new Unit("10px");
        //            // grdAll.HeaderRow.Cells[4].ControlStyle.Width = new Unit("10px");

        //        }

        //    }

        //}
        //catch
        //{
        //}
        btnExportToexcel_Click(this, new EventArgs());

    }

    protected void btnSale_Bill_Checking_Click(object sender, EventArgs e)
    {

        try
        {
            //  grdAll.DataBind() = null;
            //grdAll.DataSource = null;
            //grdAll.DataBind();
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string qry = "select [doc_no],CONVERT(varchar,doc_date,103) as doc_date,"
                      + " TaxableAmount,[CGSTAmount] ,[SGSTAmount],[IGSTAmount]," +
                      "(CGSTAmount+SGSTAmount+IGSTAmount) as TotalGST,(CGSTAmount+SGSTAmount+IGSTAmount)-(CGSTAmount+SGSTAmount+IGSTAmount) as TOTAL"
                      + " from qrysalehead where IsDeleted!=0 and doc_date between '" + fromdt + "' and '" + todt
                      + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Sale Bill Checking";

                    grdAll.DataSource = dt;



                    grdAll.DataBind();


                }
            }

        }
        catch
        {
        }

        //btnExportToexcel_Click(this, new EventArgs());
    }


    //protected void grdAll_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //    }
    //}

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            DateTime fromdtTime = Convert.ToDateTime(fromdt);

            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            DateTime todtTime = Convert.ToDateTime(todt);

            DataTable MainTable = new DataTable();
            MainTable.Columns.Add((new DataColumn("Date", typeof(string))));

            int count = Convert.ToInt32(clsCommon.getString("select DATEDIFF(month,'" + fromdt + "','" + todt + "') as DateCunt"));
            if (count == 0)
            {
                count = 1;
            }
            for (int j = 0; j < 13; j++)
            {
                string Newfromdt = fromdtTime.ToString("yyyy-MM-dd");
                string Newtodt = DateTime.DaysInMonth(fromdtTime.Year, fromdtTime.Month).ToString();
                Newtodt = fromdtTime.Year + "-" + fromdtTime.Month + "-" + Newtodt;



                string qry = "select '' as Challan_No,d.doc_no as DO_No,CONVERT(varchar(10),d.doc_date,103) as Date,mill_code as Mill_Code,m.Ac_Name_E as MillName,d.MillGSTStateCode as MillStateCode," +
                            " d.SaleBillTo as Billed_To,p.Ac_Name_E as BillToName,case ISNULL(d.SaleBillTo,0) when 0 then 27 else d.SalebilltoGstStateCode end as BillToStateCode, d.truck_no as Vehicle_No,d.quantal as Quintal,d.MM_Rate as Rate,d.Memo_Advance as Amount " +
                            " from NT_1_deliveryorder d left outer join NT_1_AccountMaster m on d.mill_code=m.Ac_Code and d.company_code=m.Company_Code " +
                            " left outer join NT_1_AccountMaster p on d.SaleBillTo=p.Ac_Code and d.company_code=p.Company_Code where d.doc_date>='2017-07-01' and d.doc_date between '" + Newfromdt + "' and '" + Newtodt + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and MM_Rate!=0 and memo_no!=0 order by d.doc_date";

                DataSet ds1 = new DataSet();
                ds1 = clsDAL.SimpleQuery(qry);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = ds1.Tables[0];

                        dt1.Columns.Add(new DataColumn("CGST", typeof(double)));
                        dt1.Columns.Add(new DataColumn("SGST", typeof(double)));
                        dt1.Columns.Add(new DataColumn("IGST", typeof(double)));
                        dt1.Columns.Add(new DataColumn("FinalAmount", typeof(double)));

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {

                            double cgstrate = 2.5;
                            double sgstrate = 2.5;
                            double igstrate = 5;

                            double CGSTAmount = 0.0;
                            double SGSTAmount = 0.0;
                            double IGSTAmount = 0.0;

                            int millStateCode = Convert.ToInt32(dt1.Rows[i]["MillStateCode"].ToString());
                            int partyStateCode = Convert.ToInt32(dt1.Rows[i]["BillToStateCode"].ToString());
                            double Amount = Convert.ToDouble(dt1.Rows[i]["Amount"].ToString());
                            if (millStateCode == partyStateCode)
                            {
                                CGSTAmount = Math.Round((Amount * cgstrate / 100), 2);
                                SGSTAmount = Math.Round((Amount * sgstrate / 100), 2);
                            }
                            else
                            {
                                IGSTAmount = Math.Round((Amount * igstrate / 100), 2);
                            }

                            dt1.Rows[i]["CGST"] = CGSTAmount;
                            dt1.Rows[i]["SGST"] = SGSTAmount;
                            dt1.Rows[i]["IGST"] = IGSTAmount;
                            dt1.Rows[i]["FinalAmount"] = Math.Round((Amount + CGSTAmount + SGSTAmount + IGSTAmount), 2);
                        }
                        lblSummary.Text = "All Summary";
                        //grdAll.DataSource = dt1;
                        //grdAll.DataBind();

                        double totalQuintal = Convert.ToDouble(dt1.Compute("SUM(Quintal)", string.Empty));
                        double totalBagBillAmount = Convert.ToDouble(dt1.Compute("SUM(Amount)", string.Empty));
                        double totalCGST = Convert.ToDouble(dt1.Compute("SUM(CGST)", string.Empty));
                        double totalSGST = Convert.ToDouble(dt1.Compute("SUM(SGST)", string.Empty));
                        double totalIGST = Convert.ToDouble(dt1.Compute("SUM(IGST)", string.Empty));
                        double totalPayable_Amount = Convert.ToDouble(dt1.Compute("SUM(FinalAmount)", string.Empty));

                        DataSet summary = new DataSet();
                        //string qrysummry = "select  Tran_Type, sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount,SUM(IGSTAmount) as IGSTAmount,SUM(subTotal) as Amount,sum(Bill_Amount) as Total_Amount " +
                        //   " from NT_1_SugarSale where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromdt + "' and '" + todt + "' group by Tran_Type " +
                        //        "union select Tran_Type,  sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount " +
                        //            ",SUM(IGSTAmount) as IGSTAmount, SUM(subTotal) as Amount,sum(Bill_Amount) as Total_Amount from NT_1_SugarPurchase where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        //        " and doc_date between '" + fromdt + "' and '" + todt + "' group by Tran_Type " +
                        //        " union select Tran_Type, sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount" +
                        //            ", SUM(IGSTAmount) as IGSTAmount,SUM(Total) as Amount,sum(Grand_Total) as Total_Amount from NT_1_RetailSaleHead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) +
                        //        "and doc_date between '" + fromdt + "' and '" + todt + "' group by Tran_Type " +
                        //        " union select Tran_Type, sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount" +
                        //        ",SUM(IGSTAmount) as IGSTAmount, SUM(TaxableAmount) as Amount,sum(Voucher_Amount`) as Total_Amount from NT_1_Voucher where TaxableAmount!=0 and Voucher_Amount<0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        //        " and doc_date between '" + fromdt + "' and '" + todt + "' group by Tran_Type" +
                        //        " union select  Tran_Type, sum(abs(CGSTAmount))" +
                        //      " as CGSTAmount,SUM(abs(SGSTAmount)) as SGSTAmount,SUM(abs(IGSTAmount)) as IGSTAmount, SUM(TaxableAmount) as Amount,sum(abs(Voucher_Amount)) as Total_Amount " +
                        //      " from NT_1_Voucher where TaxableAmount!=0 and Voucher_Amount>0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        //      " and doc_date between '" + fromdt + "' and '" + todt + "' group by Tran_Type";
                        string qrysummry = "select  Tran_Type, sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount,SUM(IGSTAmount) as IGSTAmount,SUM(TaxableAmount) as Amount,sum(Bill_Amount) as Total_Amount " +
                     " from NT_1_SugarSale where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + Newfromdt + "' and '" + Newtodt + "' group by Tran_Type " +
                          "union select Tran_Type,  sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount " +
                              ",SUM(IGSTAmount) as IGSTAmount, SUM(subTotal) as Amount,sum(Bill_Amount) as Total_Amount from NT_1_SugarPurchase where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                          " and doc_date between '" + Newfromdt + "' and '" + Newtodt + "' group by Tran_Type " +
                          " union select Tran_Type, sum(CGSTAmount) as CGSTAmount,SUM(SGSTAmount) as SGSTAmount" +
                              ", SUM(IGSTAmount) as IGSTAmount,SUM(Total) as Amount,sum(Grand_Total) as Total_Amount from NT_1_RetailSaleHead where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code= " + Convert.ToInt32(Session["year"].ToString()) +
                          "and doc_date between '" + Newfromdt + "' and '" + Newtodt + "' group by Tran_Type " +
                              " union select Tran_Type, sum(cgst_amount) as CGSTAmount,SUM(sgst_amount) as SGSTAmount" +
                              ",SUM(igst_amount) as IGSTAmount, SUM(texable_amount) as Amount,sum(bill_amount) as Total_Amount from commission_bill where texable_amount!=0 and bill_amount<0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                          " and doc_date between '" + Newfromdt + "' and '" + Newtodt + "' group by Tran_Type" +
                              " union select  Tran_Type, sum(abs(cgst_amount))" +
                            " as CGSTAmount,SUM(abs(sgst_amount)) as SGSTAmount,SUM(abs(igst_amount)) as IGSTAmount, SUM(texable_amount) as Amount,sum(abs(bill_amount)) as Total_Amount " +
                            " from commission_bill where texable_amount!=0 and bill_amount>0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and doc_date between '" + Newfromdt + "' and '" + Newtodt + "' group by Tran_Type" +
                        " union select 'XP' as Tran_Type, sum(abs(CGST_Amount)) as CGSTAmount,SUM(abs(SGST_Amount)) as SGSTAmount,SUM(abs(IGST_Amount)) as" +
            " IGSTAmount, SUM(Taxable_Amount) as Amount,sum(abs(Bill_Amount)) as Total_Amount  from NT_1_Other_Purchase where Taxable_Amount!=0 and GST_RateCode<>3 and Bill_Amount>0 " +
            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + Newfromdt + "' and '" + Newtodt +
            "' union select 'RB' as Tran_Type, sum(abs(CGSTAmount)) as CGSTAmount,SUM(abs(SGSTAmount)) " +
          " as SGSTAmount,SUM(abs(IGSTAmount)) as IGSTAmount, SUM(Subtotal) as Amount,sum(abs(Final_Amount)) as Total_Amount  from NT_1_RentBillHead where Subtotal!=0 and GstRateCode<>3 and Final_Amount>0 " +
          "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Date between '" + Newfromdt + "' and '" + Newtodt + "'";
                        summary = clsDAL.SimpleQuery(qrysummry);
                        DataTable dtsummery = new DataTable();
                        dtsummery = summary.Tables[0];
                        DataRow dr = dtsummery.NewRow();
                        DataRow dr1 = MainTable.NewRow();
                        dr["Tran_Type"] = "FR";
                        dr["CGSTAmount"] = totalCGST;
                        dr["SGSTAmount"] = totalSGST;
                        dr["IGSTAmount"] = totalIGST;
                        dr["Amount"] = totalBagBillAmount;
                        dr["Total_Amount"] = totalPayable_Amount;
                        dtsummery.Rows.Add(dr);

                        dr1["Date"] = Newfromdt;
                        MainTable.Rows.Add(dr1);
                        MainTable.Merge(dtsummery);
                        todtTime = Convert.ToDateTime(Newtodt);

                        fromdtTime = todtTime.AddDays(1);
                        todtTime = fromdtTime.AddMonths(1).AddDays(-1);

                        if (Convert.ToDateTime(todt) == Convert.ToDateTime(Newtodt) || Convert.ToDateTime(todt) <= Convert.ToDateTime(Newtodt))
                        {
                            break;
                        }

                    }
                    else
                    {
                        todtTime = Convert.ToDateTime(Newtodt);

                        fromdtTime = todtTime.AddDays(1);
                        todtTime = fromdtTime.AddMonths(1).AddDays(-1);

                        if (Convert.ToDateTime(todt) == Convert.ToDateTime(Newtodt) || Convert.ToDateTime(todt) <= Convert.ToDateTime(Newtodt))
                        {
                            break;
                        }
                    }


                }

            }
            grdAll.DataSource = MainTable;
            grdAll.DataBind();


            double amount = Convert.ToDouble(MainTable.Compute("SUM(Amount)", string.Empty));
            double CGST = Convert.ToDouble(MainTable.Compute("SUM(CGSTAmount)", string.Empty));
            double SGST = Convert.ToDouble(MainTable.Compute("SUM(SGSTAmount)", string.Empty));
            double IGST = Convert.ToDouble(MainTable.Compute("SUM(IGSTAmount)", string.Empty));
            double Total_Amount = Convert.ToDouble(MainTable.Compute("SUM(Total_Amount)", string.Empty));

            // grdAll.FooterRow.Cells[0].Text = totalQuintal.ToString();
            grdAll.FooterRow.Cells[1].Text = amount.ToString();
            grdAll.FooterRow.Cells[2].Text = CGST.ToString();
            grdAll.FooterRow.Cells[3].Text = SGST.ToString();
            grdAll.FooterRow.Cells[4].Text = IGST.ToString();
            grdAll.FooterRow.Cells[5].Text = Total_Amount.ToString();


            grdAll.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            grdAll.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            grdAll.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            grdAll.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdAll.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
        catch (Exception es)
        {

        }
    }


    protected void btnSaleTCS_Click(object sender, EventArgs e)
    {
        try
        {

            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "";
            //if (drpSaleTCS.SelectedValue == "SB")
            //{
            //    qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,billtoname as [Name Of Party]," +
            //                " billtopanno as Pan,billtoaddress as Address,Bill_Amount as Net, CGSTAmount as CGST," +
            //                 " SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qrysalehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
            //                 + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";
            //}
            if (drpSaleTCS.SelectedValue == "SB")
            {
                //qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(PartyName,',',' '),'.','') as [Name Of Party]," +
                //               " REPLACE(billtopanno,',',' ') as Pan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,[Taxable Value] as Taxable_Amt, CGSTAmount as CGST," +
                //                " SGSTAmount as SGST,IGSTAmount as IGST,[Invoice Value] as Bill_Amt,TCS_Amt as TCS from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and Carporate_Sale_No=0 and doc_date between '" + fromdt + "' and '" + todt
                //                + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";

                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(billtoname,',',' '),'.','') as [Name Of Party]," +
                      " REPLACE(billtopanno,',',' ') as Pan, REPLACE(BillToTanNo,',',' ') as Tan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,[TaxableAmount] as Taxable_Amt, CGSTAmount as CGST," +
                      " SGSTAmount as SGST,IGSTAmount as IGST,Bill_Amount as Bill_Amt,TCS_Amt as TCS from qrysalehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                      + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";

            }
            if (drpSaleTCS.SelectedValue == "SC")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                              " REPLACE(AC_Pan,',',' ') as Pan, REPLACE(Tan_no,',',' ') as Tan, REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,[Taxable Value] as Taxable_Amt, CGSTAmount as CGST," +
                               " SGSTAmount as SGST,IGSTAmount as IGST,[Invoice Value] as Bill_Amt,TCS_Amt as TCS from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and Carporate_Sale_No!=0 and doc_date between '" + fromdt + "' and '" + todt
                               + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "NC")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                              " REPLACE(AC_Pan,',',' ') as Pan, REPLACE(Tan_no,',',' ') as Tan, REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,[Taxable Value] as Taxable_Amt, CGSTAmount as CGST," +
                               " SGSTAmount as SGST,IGSTAmount as IGST,[Invoice Value] as Bill_Amt,TCS_Amt as TCS from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and Carporate_Sale_No!=0 and doc_date between '" + fromdt + "' and '" + todt
                               + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "RR")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RR'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(PartyName,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(BillToTan,',',' ') as Tan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,Taxable_Amount as Taxable_Amt, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,NetPayble as Bill_Amt,TCS_Amount as TCS from qryRetailSale where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amount!=0 and IsDelete!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "LV")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'LV'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,bill_amount as Taxable_Amt, cgst_amount as CGST," +
                             " sgst_amount as SGST,igst_amount as IGST,TCS_Net_Payable as Bill_Amt, (  TCS_Amt * -1)  as TCS from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and Tran_Type='LV' order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "CB")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','')  as Address,Subtotal as Taxable_Amt, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,TCS_Net_Payable as Bill_Amt,TCS_Amt as TCS from qryColdStorageHead where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by Date";
            }
            if (drpSaleTCS.SelectedValue == "RB")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan, REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,Subtotal AS Taxable_Amt, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,Final_Amount as Bill_Amt,TCS_Amt as TCS from qryrentbillhead where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and IsDeleted!=0 and TCS_Amt!=0 order by Date";
            }
            if (drpSaleTCS.SelectedValue == "RS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RS'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date, REPLACE(REPLACE(FromAcName,',',' '),'.','')  as [Name Of Party]," +
                    " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,SUBTOTAL as Taxable_Amt, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Net_Payable as Bill_Amt," +
                    " TCS_Amt as TCS from qrysugarsalereturnhead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";
            }

            if (drpSaleTCS.SelectedValue == "DN")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'DN'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date, REPLACE(REPLACE(Ac_Name_E,',',' '),'.','')  as [Name Of Party]," +
                    " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,texable_amount as Taxable_Amt, cgst_amount as CGST, sgst_amount as SGST,igst_amount as IGST,TCS_Net_Payable as Bill_Amt," +
                    " TCS_Amt as TCS from qrydebitnotehead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and tran_type='DN' and isDeleted!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "DS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'DS'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date, REPLACE(REPLACE(Ac_Name_E,',',' '),'.','')  as [Name Of Party]," +
                   " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,texable_amount as Taxable_Amt, cgst_amount as CGST, sgst_amount as SGST,igst_amount as IGST,TCS_Net_Payable as Bill_Amt," +
                   " TCS_Amt as TCS from qrydebitnotehead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                   + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and tran_type='DS' and isDeleted!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "All")
            {
                #region
                //qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,CarporateBillTo_Name as [Name Of Party]," +
                //      " Carporate_Pan as Pan,Carporate_Address as Address,Bill_Amount as Net, CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qrysalehead union "



                //      + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RR'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,partyname as [Name Of Party]," +
                //             " CompanyPan as Pan,billtoaddress as Address,NetPayble as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amount as TCS from qryRetailSale union "

                //      + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'LV'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                //            " CompanyPan as Pan,Address_E as Address,TCS_Net_Payable as Net, cgst_amount as CGST,sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrycommissionbill union "

                //      + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,Ac_Name_E as [Name Of Party]," +
                //            " CompanyPan as Pan,Address_E as Address,TCS_Net_Payable as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qryColdStorageHead union "

                //     + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,Ac_Name_E as [Name Of Party]," +
                //            " CompanyPan as Pan,Address_E as Address,Final_Amount as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qryrentbillhead union "

                //    + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RS'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,FromAcName as [Name Of Party]," +
                //    " CompanyPan as Pan,billtoaddress as Address,TCS_Net_Payable as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST," +
                //    " TCS_Amt as TCS from qrysugarsalereturnhead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                //    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 ";
                #endregion

                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No, InvoiceNo,CONVERT(varchar(10),date,103) as date, [Name Of Party],  REPLACE(Pan,',',' ') as Pan,REPLACE(Tan,',',' ') as Tan,  REPLACE(REPLACE(Address,',',' '),'.','') as Address," +
                       " Taxable_Amt, CGST, SGST, IGST,Net as Bill_Amt, TCS from qryTCSAllUnion where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 " +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS!=0 ";
            }
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    dt = ds.Tables[0];

                    if (drpSaleTCS.SelectedValue == "All")
                    {
                        lblSummary.Text = "Sale TCS Summary";
                    }

                    if (drpSaleTCS.SelectedValue == "SB" || drpSaleTCS.SelectedValue == "SC" || drpSaleTCS.SelectedValue == "NC")
                    {
                        lblSummary.Text = "Sale TCS Summary";
                    }

                    if (drpSaleTCS.SelectedValue == "RS")
                    {
                        lblSummary.Text = "Sale Return Sale TCS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "RR")
                    {
                        lblSummary.Text = "Retail Sale TCS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "LV")
                    {
                        lblSummary.Text = "Commission Bill TCS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "CB")
                    {
                        lblSummary.Text = "Could Storage TCS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "RB")
                    {
                        lblSummary.Text = "Rent Bill TCS Summary";
                    }
                    grdAll.DataSource = dt;
                    grdAll.DataBind();


                    double totalNet = Convert.ToDouble(dt.Compute("SUM(Taxable_Amt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalTCS = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));
                    double totalBillAmt = Convert.ToDouble(dt.Compute("SUM(Bill_Amt)", string.Empty));


                    grdAll.FooterRow.Cells[7].Text = totalNet.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalBillAmt.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalTCS.ToString();


                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                }

                if (hdconfirm.Value == "Yes")
                {
                    #region[csv]
                    string fileName = "";
                    string strForCSV = "";
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = "LEN(Pan) < 1";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongPanNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 5;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 6)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "InvoiceNo", "Pan");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "SaleTCS.csv";

                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                    #endregion
                }
            }
            else
            {
                grdAll.DataSource = null;
                grdAll.DataBind();
                lblSummary.Text = "Records Not Found";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnPurchaseTCS_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "";
            if (drppurchasrtcs.SelectedValue == "PS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppliername as [Name Of Party]," +
                            " CompanyPan as Pan,Tan_no as Tan,supplieraddress as Address,Bill_Amount as Net, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qrypurchasehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 order by doc_date";

            }

            if (drppurchasrtcs.SelectedValue == "RP")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,partyname as [Name Of Party]," +
                            " CompanyPan as Pan,Tan_no as Tan,billtoaddress as Address,NetPayble as Net, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amount as TCS from qryRetailPurchase where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amount!=0 order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "CV")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CV'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and Tran_Type='CV' order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "CN")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CN'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and IsDeleted <> 0 AND Tran_Type='CN' order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "CS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                       "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                       "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                              + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and IsDeleted <> 0 AND Tran_Type='CS' order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "PR")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PR'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                       "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgstamount as CGST," +
                       "sgstamount as SGST,igstamount as IGST,TCS_Amt as TCS from qrysugarpurchasereturnhead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                              + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0  order by doc_date";

            }
            //if (drppurchasrtcs.SelectedValue == "XP")
            //{
            //    qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'XP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
            //           "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
            //           "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
            //                  + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and IsDeleted <> 0  order by doc_date";

            //}
            if (drppurchasrtcs.SelectedValue == "All")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppliername as [Name Of Party]," +
                          " CompanyPan as Pan,Tan_no as Tan,supplieraddress as Address,Bill_Amount as Net, CGSTAmount as CGST," +
                           " SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qrypurchasehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                           + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0   Union "



               + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,partyname as [Name Of Party]," +
                           " CompanyPan as Pan,Tan_no as Tan,billtoaddress as Address,NetPayble as Net, CGSTAmount as CGST," +
                            " SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amount as TCS from qryRetailPurchase where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                            + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amount!=0  union "

                + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CV'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and Tran_Type='CV'  union "

                + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CN'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and IsDeleted <> 0 AND Tran_Type='CN'  union "
              
                             +  "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                       "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                       "sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                              + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 and IsDeleted <> 0 AND Tran_Type='CS'  union "

                              + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PR'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                       "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgstamount as CGST," +
                       "sgstamount as SGST,igstamount as IGST,TCS_Amt as TCS from qrysugarpurchasereturnhead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                              + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0  ";


            }
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];

                    DataView dv = dt1.DefaultView;
                    dv.Sort = "date asc";
                    dt = dv.ToTable();

                    // dt.Select(dt.)

                    if (drppurchasrtcs.SelectedValue == "All")
                    {
                        lblSummary.Text = "Purchase TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "PS")
                    {
                        lblSummary.Text = "Purchase TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "RP")
                    {
                        lblSummary.Text = "Retail Purchase TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CV")
                    {
                        lblSummary.Text = "Commission TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CN")
                    {
                        lblSummary.Text = "Credit Note to Customer TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CS")
                    {
                        lblSummary.Text = "Credit Note to Supplier TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "PR")
                    {
                        lblSummary.Text = "Sugar Sale Return Purchase   TCS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CS")
                    {
                        lblSummary.Text = "Other Purchase TCS Summary";
                    }
                    grdAll.DataSource = dt;
                    grdAll.DataBind();
                    double totalNet = Convert.ToDouble(dt.Compute("SUM(Net)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalTCS = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));

                    grdAll.FooterRow.Cells[7].Text = totalNet.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalTCS.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;


                }
                if (hdconfirm.Value == "Yes")
                {
                    #region[csv]
                    string fileName = "";
                    string strForCSV = "";
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = "LEN(Pan) < 1 ";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongPanNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 5;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 6)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "PSNo", "Pan");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "PurchaseTCS.csv";

                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                    #endregion
                }
            }
            else
            {
                grdAll.DataSource = null;
                grdAll.DataBind();

                lblSummary.Text = "Records Not Found";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnGSTR2_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,doc_no as OurNo,PartyName as [Supplier Name],FromGSTNo as [Supplier GSTIN],FROM_STATION as [Place Of Supply], Bill_No as [Invoice No]," +
                        " Convert(varchar(10),Mill_Invc_Date,103) as [Invoice Date],Bill_Amount as TotInv_Value,subTotal as Taxable_Value,5 as GSTRate,IGSTAmount as IGST_Amount,CGSTAmount as CGST_Amount, SGSTAmount as SGST_Amount,0 as CessAmount from NT_1_qrySugarPurchListForReport " +
                         " where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                         + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "GSTR2 Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();


                    double totalInv_Value = Convert.ToDouble(dt.Compute("SUM(TotInv_Value)", string.Empty));
                    double totalTaxable_Value = Convert.ToDouble(dt.Compute("SUM(Taxable_Value)", string.Empty));
                    double totalIGST_Amount = Convert.ToDouble(dt.Compute("SUM(IGST_Amount)", string.Empty));
                    double totalCGST_Amount = Convert.ToDouble(dt.Compute("SUM(CGST_Amount)", string.Empty));
                    double totalSGST_Amount = Convert.ToDouble(dt.Compute("SUM(SGST_Amount)", string.Empty));


                    grdAll.FooterRow.Cells[7].Text = totalInv_Value.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalTaxable_Value.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST_Amount.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalCGST_Amount.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalSGST_Amount.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnGstRate_Click(object sender, EventArgs e)
    {
        //BindContrydropdown();
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            //txtFromDate.Text = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //txtToDate.Text = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            string gstrate = drpGstRate.SelectedValue;

            //string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,convert(varchar(50),InvoiceNo) as InvoiceNo,CONVERT(varchar(10),date,103) as date,Name_Of_Party," +
            //                " '" + Session["ComapnyPan"] + "' as Pan,Address,Net,CGST,SGST,IGST,TCS from qryGSTRatewiseSaleSummary where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt
            //                 + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS!=0  order by date";

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,convert(varchar(50),InvoiceNo) as InvoiceNo,CONVERT(varchar(10),date,103) as date,Name_Of_Party," +
                            "HSN_NO,TaxableAmt,CGST,SGST,IGST,BillAmt,TCS,Qntl from qryGSTRatewiseSaleSummary where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt
                             + "' and Rate= " + gstrate + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by date";

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            DataTable dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];

                    lblSummary.Text = "GST Rate wise Sale";

                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    double totalTaxableAmt = Convert.ToDouble(dt.Compute("SUM(TaxableAmt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalBillAmt = Convert.ToDouble(dt.Compute("SUM(BillAmt)", string.Empty));
                    double totalTCS = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));
                    double totalQntl = Convert.ToDouble(dt.Compute("SUM(Qntl)", string.Empty));

                    grdAll.FooterRow.Cells[5].Text = totalTaxableAmt.ToString();
                    grdAll.FooterRow.Cells[6].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[7].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalBillAmt.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalTCS.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalQntl.ToString();

                    grdAll.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;

                }

                if (hdconfirm.Value == "Yes")
                {
                    #region[csv]
                    string fileName = "";
                    string strForCSV = "";
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = " NOT(ISNULL(Address, '') = '') ";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongGSTNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 3;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 4)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "InvoiceNo", "Name Of Party");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "SaleTCS.csv";

                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                    #endregion
                }
            }
            else
            {
                grdAll.DataSource = null;
                grdAll.DataBind();
                lblSummary.Text = "Records Not Found";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void BindContrydropdown()
    {
        try
        {
            string qry = "Select Rate from nt_1_gstratemaster order by Rate";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    //drpGstRate.DataValueField = "select";
                    //DataRow row = dt.NewRow();
                    //row["select"] = "All";
                    ////dt.Rows.Add(row);
                    //dt.Rows.InsertAt(row, 0);
                    //drpsalebill.AppendDataBoundItems = false;
                    if (dt.Rows.Count > 0)
                    {
                        //drpGstRate.DataSource = dt;
                        ////dt.Columns["TRAN_TYPE"].SetOrdinal(0);

                        //drpGstRate.DataBind();

                        drpGstRate.DataSource = dt;
                        drpGstRate.DataValueField = dt.Columns[0].ToString();

                        drpGstRate.DataBind();


                    }
                    else
                    {
                        drpGstRate.DataSource = null;
                        drpGstRate.DataBind();


                    }
                }
            }

        }
        catch
        {
        }
    }

    protected void drpSaleTCS_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFocus(btnSaleTCS);
    }
    protected void drppurchasrtcs_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFocus(btnPurchaseTCS);
    }
    protected void btnhsnsale_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select HSN,Tran_Type,convert(varchar(10),doc_date,103) as doc_date,BillAmt,CgstAmt,SgstAmt,IgstAmt,TCSAmt,NetPayable from qryHSNSale where doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                             + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    dt = ds.Tables[0];

                    lblSummary.Text = "HSN Summary";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();


                    double totalInv_Value = Convert.ToDouble(dt.Compute("SUM(NetPayable)", string.Empty));
                    double totalTaxable_Value = Convert.ToDouble(dt.Compute("SUM(BillAmt)", string.Empty));
                    double totalIGST_Amount = Convert.ToDouble(dt.Compute("SUM(IgstAmt)", string.Empty));
                    double totalCGST_Amount = Convert.ToDouble(dt.Compute("SUM(CgstAmt)", string.Empty));
                    double totalSGST_Amount = Convert.ToDouble(dt.Compute("SUM(SgstAmt)", string.Empty));
                    double totaltcsamt = Convert.ToDouble(dt.Compute("SUM(TCSAmt)", string.Empty));

                    grdAll.FooterRow.Cells[8].Text = totalInv_Value.ToString();
                    grdAll.FooterRow.Cells[7].Text = totaltcsamt.ToString();

                    grdAll.FooterRow.Cells[3].Text = totalTaxable_Value.ToString();
                    grdAll.FooterRow.Cells[6].Text = totalIGST_Amount.ToString();
                    grdAll.FooterRow.Cells[4].Text = totalCGST_Amount.ToString();
                    grdAll.FooterRow.Cells[5].Text = totalSGST_Amount.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                }
                if (hdconfirm.Value == "Yes")
                {
                    #region[csv]
                    string fileName = "";
                    string strForCSV = "";
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = " NOT(ISNULL(Address, '') = '') ";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongGSTNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 3;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 4)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "HSN", "Tran_Type");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "HSNSummary.csv";

                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                    #endregion
                }
            }
            else
            {
                lblSummary.Text = "Records Not Found";
                grdAll.DataSource = null;
                grdAll.DataBind();
            }


        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void drpGstRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFocus(btnGstRate);
    }
    //protected void btnReturnSale_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string fromdt = txtFromDt.Text;
    //        string todt = txtToDt.Text;
    //        fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
    //        todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

    //        //string qry = "select * from (select s.doc_no as Invoice_No,s.PartyGST as PartyGSTNo,s.Ac_Code as PartyCode,s.PartyName,s.PartyStateCode,CONVERT(varchar,s.doc_date,103) as Invoice_Date,s.LORRYNO as Vehicle_No,s.Quantal as Quintal,s.rate as Rate,s.TaxableAmount, " +
    //        //    " s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.DO_No as DO,s.CS_No from NT_1_qrySugarSaleForGSTReturn s  " +
    //        //    " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";



    //        ////string qry = "select * from (select s.doc_no as Invoice_No,s.PartyGST as PartyGSTNo,s.Ac_Code as PartyCode,s.PartyName,s.PartyStateCode,CONVERT(varchar,s.doc_date,103) as Invoice_Date,s.LORRYNO as Vehicle_No,s.Quantal as Quintal,s.rate as Rate, " +
    //        ////   " s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.DO_No as DO,s.PURCNO as OldInvNo from NT_1_qrySugarRetaileSaleSummary s  " +
    //        ////   " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";


    //        string qry = "select * from (select s.doc_no as Invoice_No,s.PartyGST as PartyGSTNo,s.Ac_Code as PartyCode,s.PartyName,s.PartyStateCode,CONVERT(varchar,s.doc_date,103) as Invoice_Date,s.LORRYNO as Vehicle_No,s.Quantal as Quintal,s.rate as Rate,s.subTotal as TaxableAmount , " +
    //          " s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.DO_No as DO,s.PURCNO as OldInvNo from NT_1_qrySugarRetaileSaleSummary s  " +
    //          " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";


    //        DataSet ds = new DataSet();
    //        ds = clsDAL.SimpleQuery(qry);

    //        if (ds != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                DataTable dt = new DataTable();
    //                dt = ds.Tables[0];
    //                //dt.Columns.Remove("RowNumber");
    //                lblSummary.Text = "Return Sale Summary";

    //                //DataView dvCsNo = new DataView(dt);
    //                //dvCsNo.RowFilter = "CS_No<>0";

    //                //DataTable dtFiltered = new DataTable();
    //                //dtFiltered = dvCsNo.ToTable();
    //                //foreach (DataRow drRow in dtFiltered.Rows)
    //                //{
    //                //    string csno = drRow["CS_No"].ToString();
    //                //    string invno = drRow["Invoice_No"].ToString();
    //                //    string BillToCode = clsCommon.getString("Select ISNULL(Bill_To,0) from NT_1_CarporateSale where Doc_No=" + csno + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
    //                //    if (BillToCode != "0")
    //                //    {
    //                //        string billtoname = clsCommon.getString("Select Ac_Name_E from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
    //                //        string Gst_No = clsCommon.getString("Select Gst_No from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
    //                //        string placeofsupply = clsCommon.getString("Select (CONVERT(varchar,ISNULL([GSTStateCode],0))+'-'+GSTStateName) from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
    //                //        string statecode = clsCommon.getString("Select GSTStateCode from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

    //                //        DataRow drForUpdate = dt.Select("CS_No=" + csno + " and Invoice_No='" + invno + "'").FirstOrDefault();
    //                //        drForUpdate["PartyCode"] = BillToCode;
    //                //        drForUpdate["PartyName"] = billtoname;
    //                //        drForUpdate["PartyGSTNo"] = Gst_No;
    //                //        drForUpdate["PartyStateCode"] = statecode;
    //                //    }
    //                //}

    //                grdAll.DataSource = dt;
    //                grdAll.DataBind();

    //                double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
    //                double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
    //                double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
    //                double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
    //                double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
    //                double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));

    //                grdAll.FooterRow.Cells[7].Text = totalQuintal.ToString();
    //                grdAll.FooterRow.Cells[9].Text = totalBagBillAmount.ToString();
    //                grdAll.FooterRow.Cells[10].Text = totalCGST.ToString();
    //                grdAll.FooterRow.Cells[11].Text = totalSGST.ToString();
    //                grdAll.FooterRow.Cells[12].Text = totalIGST.ToString();
    //                grdAll.FooterRow.Cells[13].Text = totalPayable_Amount.ToString();

    //                //grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
    //                //grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
    //                //grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
    //                //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
    //                //grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
    //                //grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
    //                //grdAll.Columns.RemoveAt(15);
    //            }
    //        }

    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }

    //}

    protected void btnReturnSale_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");



            ////string qry = "select * from (select s.doc_no as Invoice_No,s.PartyGST as PartyGSTNo,s.Ac_Code as PartyCode,s.PartyName,s.PartyStateCode,CONVERT(varchar,s.doc_date,103) as Invoice_Date,s.LORRYNO as Vehicle_No,s.Quantal as Quintal,s.rate as Rate, " +
            ////   " s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.DO_No as DO,s.PURCNO as OldInvNo from NT_1_qrySugarRetaileSaleSummary s  " +
            ////   " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";


            //string qry = "select * from (select 'RS'+CONVERT(NVARCHAR,s.doc_no)  as Invoice_No,s.BillToGst_No as PartyGSTNo,s.FromAc as PartyCode,s.FromAcName as PartyName,s.BillToGSTStateCode as PartyStateCode,CONVERT(varchar,s.doc_date,103) as Invoice_Date,s.LORRYNO as Vehicle_No,s.Quantal as Quintal,s.rate as Rate,s.subTotal as TaxableAmount , " +
            //  " s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.PURCNO as OldInvNo,ackno as ACKNo from qrysugarsalereturnheaddetail s  " +
            //  " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";

            string qry = "select * from (select 'RS'+CONVERT(NVARCHAR,s.doc_no)  as Invoice_No,s.BillToGst_No as PartyGSTNo,s.FromAc as PartyCode,s.FromAcName as PartyName,s.BillToGSTStateCode as PartyStateCode,(RIGHT('0'+CONVERT(NVARCHAR,BillToGSTStateCode),2) +'-'+ LTRIM(RTRIM(BillToState))) as PlaceOfSupply,CONVERT(varchar,s.doc_date,103) as Invoice_Date," +
                          " s.LORRYNO as Vehicle_No,s.Quantal as Quintal,s.rate as Rate,s.subTotal as TaxableAmount ,  s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.PURCNO as OldInvNo,s.ackno as ACKNo ,convert(varchar(10),dbo.nt_1_sugarpurchasereturn.doc_date,103) as OldInvDate" +
                          " from qrysugarsalereturnheaddetail s   LEFT OUTER JOIN dbo.nt_1_sugarpurchasereturn ON s.PURCNO = dbo.nt_1_sugarpurchasereturn.doc_no and s.Company_Code = dbo.nt_1_sugarpurchasereturn.Company_Code and s.Year_Code = dbo.nt_1_sugarpurchasereturn.Year_Code " +
                          " where s.doc_date>='2017-07-01' and s.IsDeleted!=0 and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";




            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    //dt.Columns.Remove("RowNumber");
                    lblSummary.Text = "Return Sale Summary";

                    //DataView dvCsNo = new DataView(dt);
                    //dvCsNo.RowFilter = "CS_No<>0";

                    //DataTable dtFiltered = new DataTable();
                    //dtFiltered = dvCsNo.ToTable();
                    //foreach (DataRow drRow in dtFiltered.Rows)
                    //{
                    //    string csno = drRow["CS_No"].ToString();
                    //    string invno = drRow["Invoice_No"].ToString();
                    //    string BillToCode = clsCommon.getString("Select ISNULL(Bill_To,0) from NT_1_CarporateSale where Doc_No=" + csno + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    //    if (BillToCode != "0")
                    //    {
                    //        string billtoname = clsCommon.getString("Select Ac_Name_E from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    //        string Gst_No = clsCommon.getString("Select Gst_No from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    //        string placeofsupply = clsCommon.getString("Select (CONVERT(varchar,ISNULL([GSTStateCode],0))+'-'+GSTStateName) from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    //        string statecode = clsCommon.getString("Select GSTStateCode from NT_1_qryAccountsList where Ac_Code=" + BillToCode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");

                    //        DataRow drForUpdate = dt.Select("CS_No=" + csno + " and Invoice_No='" + invno + "'").FirstOrDefault();
                    //        drForUpdate["PartyCode"] = BillToCode;
                    //        drForUpdate["PartyName"] = billtoname;
                    //        drForUpdate["PartyGSTNo"] = Gst_No;
                    //        drForUpdate["PartyStateCode"] = statecode;
                    //    }
                    //}

                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));

                    grdAll.FooterRow.Cells[8].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[14].Text = totalPayable_Amount.ToString();

                    //grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.Columns.RemoveAt(15);
                }
            }

        }
        catch (Exception)
        {
            throw;
        }

    }
    protected void btnpurchaseReturn_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");



            string qry = "select * from (select 'PR'+CONVERT(NVARCHAR,s.doc_no) as Invoice_No,s.billtogstno as BillToGST ,s.Bill_To as BillTo,s. BilltoName,s.BillToStateCode,(RIGHT('0'+CONVERT(NVARCHAR,BillToStateCode),2) +'-'+ LTRIM(RTRIM(billtostatename))) as PlaceOfSupply,CONVERT(varchar,s.doc_date,103) as Invoice_Date,s.LORRYNO as Vehicle_No,s.Quantal as Quintal,purc_rate as Rate, " +
               "s.subTotal as TaxableAmt, s.CGSTAmount as CGST,s.SGSTAmount as SGST,s.IGSTAmount as IGST,s.Bill_Amount as Payable_Amount,s.PurcTranType+convert(nvarchar,s.PURCNO) as OldInvNo,'' as OldInvDate   from qrysugarpurcreturnheaddetail s  " +
               " where s.doc_date>='2017-07-01' and s.doc_date between '" + fromdt + "' and '" + todt + "' and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ) as a order by a.Invoice_Date";



            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    //dt.Columns.Remove("RowNumber");
                    lblSummary.Text = "Return Purchase Summary";


                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string invno = dt.Rows[i]["OldInvNo"].ToString();
                            invno = invno.Replace("RS", "");
                            invno = invno.Replace("SB", "");
                            string invdate;
                            if (invno != "0")
                            {
                                invdate = clsCommon.getString("select convert(varchar(10),doc_date,103) as doc_date from NT_1_SugarSale where doc_no='" + invno + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                                dt.Rows[i]["OldInvDate"] = invdate;
                            }
                            grdAll.DataSource = dt;
                            grdAll.DataBind();
                        }


                    }


                    double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));


                    double totalTaxAmt = Convert.ToDouble(dt.Compute("SUM(TaxableAmt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));


                    grdAll.FooterRow.Cells[8].Text = totalQuintal.ToString();


                    grdAll.FooterRow.Cells[10].Text = totalTaxAmt.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[14].Text = totalPayable_Amount.ToString();



                }
            }

        }
        catch (Exception)
        {
            throw;
        }

    }
    protected void btnShowentryno_Click(object sender, EventArgs e)
    {

        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            string fileName = "";
            string strForCSV = "";

            Int32 Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            Int32 Year_Code = Convert.ToInt32(Session["year"].ToString());

            child1.SetAttributeValue("Company_Code", Company_Code);
            child1.SetAttributeValue("Year_Code", Year_Code);
            child1.SetAttributeValue("FromDate", fromdt);
            child1.SetAttributeValue("ToDate", todt);


            root.Add(child1);

            string XMLReport = root.ToString();
            XDocument XDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = XDoc.ToString(SaveOptions.DisableFormatting);
            DataSet xml_ds = new DataSet();
            string spname = "SP_ShowEntry_No";
            string xmlfile = XMLReport;
            string op = "";
            string returnmaxno = "";
            int flag;

            #region[Insert]

            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);

            #endregion



            DataTable dt = new DataTable();
            dt = xml_ds.Tables[0];
            dt.Columns["Nature"].ColumnName = "Nature of Document";
            dt.Columns["Fromno"].ColumnName = "Sr. No. From";
            dt.Columns["Tono"].ColumnName = "Sr. No. To";
            dt.Columns["Total"].ColumnName = "Total Number";
            dt.Columns["Cancel"].ColumnName = "Cancelled";



            fileName = "ShowEntyNo.csv";

            strForCSV = clsCommon.DataTableToCSV(dt, ',');

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(strForCSV.ToString());
            Response.Flush();
            Response.End();

            grdAll.DataSource = dt;
            grdAll.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnDebitCreditNote_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "";
            if (drpdebitcreditnote.SelectedValue == "All")
            {
                qry = "select 0+ ROW_NUMBER() OVER(ORDER BY Company_Code) as SRNO,Inovice_No,Invoice_Date,[Bill To ACC NO],BilltoName,[BILL TO GSTIN]," +
                   " [BillToStateCode] ,(RIGHT('0'+CONVERT(NVARCHAR,BillToStateCode),2) +'-'+ LTRIM(RTRIM(shiptostatename))) as PlaceOfSupply,TaxableAmt,CGST,SGST,IGST,TCS,Payable_Amount,OldInvNo,OldInvDate,ackno as ACKNo from qryDebitcreditnote_GST where  " +
               " doc_date between '" + fromdt + "' and '" + todt +
                   "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                   " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and IsDeleted!=0  order by Invoice_Date";
            }
            else
            {

                qry = "select 0+ ROW_NUMBER() OVER(ORDER BY Company_Code) as SRNO,Inovice_No,Invoice_Date,[Bill To ACC NO],BilltoName,[BILL TO GSTIN]," +
                " [BillToStateCode] ,(RIGHT('0'+CONVERT(NVARCHAR,BillToStateCode),2) +'-'+ LTRIM(RTRIM(shiptostatename))) as PlaceOfSupply,TaxableAmt,CGST,SGST,IGST,TCS,Payable_Amount,OldInvNo,OldInvDate,ackno as ACKNo from qryDebitcreditnote_GST where  " +
            " doc_date between '" + fromdt + "' and '" + todt +
                "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + drpdebitcreditnote.SelectedValue + "' and IsDeleted!=0  order by Invoice_Date";
            }

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    //dt.Columns.Remove("RowNumber");
                    lblSummary.Text = drpdebitcreditnote.SelectedItem.ToString();


                    if (dt.Rows.Count > 0)
                    {


                        grdAll.DataSource = dt;
                        grdAll.DataBind();



                    }


                    //  double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));


                    double totalTaxAmt = Convert.ToDouble(dt.Compute("SUM(TaxableAmt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));
                    double tcs = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));

                    //grdAll.FooterRow.Cells[7].Text = totalQuintal.ToString();


                    grdAll.FooterRow.Cells[8].Text = totalTaxAmt.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = tcs.ToString();

                    grdAll.FooterRow.Cells[13].Text = totalPayable_Amount.ToString();



                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btngeneratememoadvance_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string frmdt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string Todt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string qry = "select convert(varchar(10),doc_date,103) as Doc_Date,'MM' as Tran_Type,'1' as Detail_ID, doc_no as Doc_No,'' as NewNo from qrydohead where " +
                "  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                " and doc_date between '" + frmdt + "' and '" + Todt +
                "' and Memo_Advance!=0 order by doc_date asc ,doc_no asc";
        ds = clsDAL.SimpleQuery(qry);
        dt = ds.Tables[0];
        dt.DefaultView.Sort = "Doc_Date asc";

        #region calculate rowindex
        if (dt.Rows.Count > 0)
        {
            DataRow dr = null;
            int rowIndex = 1;
            int maxIndex = 0;
            int[] index = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                index[i] = dt.Rows[i]["NewNo"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["NewNo"].ToString()) : 0;
            }
            if (index.Length > 0)
            {
                for (int i = 0; i < index.Length; i++)
                {
                    if (index[i] > maxIndex)
                    {
                        maxIndex = index[i];

                        rowIndex = maxIndex + 1;
                        dt.Rows[i]["NewNo"] = rowIndex;
                    }
                    maxIndex += 1;
                    rowIndex = maxIndex;
                    dt.Rows[i]["NewNo"] = rowIndex;
                }
                grdAll.DataSource = dt;
                grdAll.DataBind();
                ViewState["currentTable"] = dt;
                lblSummary.Text = "Memo Advance";
                btngenerateRCMnum.Visible = true;
            }
            else
            {
                rowIndex = maxIndex;          //1
            }
        }
        else
        {
            grdAll.DataSource = null;
            grdAll.DataBind();
            ViewState["currentTable"] = null;
        }
        #endregion

    }
    protected void btngenerateRCMnum_Click(object sender, EventArgs e)
    {
        if (lblSummary.Text == "Memo Advance")
        {
            #region -Head part declearation

            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));

            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
            string retValue = string.Empty;
            string strRev = string.Empty;
            #endregion-End of Head part declearation


            #region --------------------  Details --------------------
            for (int i = 0; i < grdAll.Rows.Count; i++)
            {
                string CheckingFlag = string.Empty;
                XElement child2 = new XElement("Head");

                child2.SetAttributeValue("Company_Code", Company_Code);
                child2.SetAttributeValue("Year_Code", Year_Code);


                child2.SetAttributeValue("Doc_Date", DateTime.Parse(grdAll.Rows[i].Cells[0].Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));

                child2.SetAttributeValue("Tran_Type", grdAll.Rows[i].Cells[1].Text);
                child2.SetAttributeValue("Detail_ID", grdAll.Rows[i].Cells[2].Text);
                child2.SetAttributeValue("Doc_No", grdAll.Rows[i].Cells[3].Text);


                child2.SetAttributeValue("NewNo", Server.HtmlDecode(grdAll.Rows[i].Cells[4].Text));


                root.Add(child2);
            }
            #endregion


            string XMLReport = root.ToString();

            Int32 lenght = XMLReport.Length;

            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            DataSet xml_ds = new DataSet();
            string spname = "SP_RCM_Generate_Entry";
            string xmlfile = XMLReport;
            string op = "";
            string returnmaxno = "";
            int flag;


            #region[Insert]
            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion

            if (op == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated!');", true);
            }

        }

    }

    protected void btnClodstorageInword_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            //string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,doc_no as Invoice_No,PartyGSTNo as PartyGSTNo,"+
            //             " Customer_Code as PartyCode,Ac_Name_E as PartyName,CustomerStateCode as Partygststatecode,CONVERT(varchar(10),Date,103) as Invoice_Date,"+
            //             " HSN as HSNCode,Subtotal as TaxableAmount,  CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Final_Amount as Payable_Amount from qryColdStorageHeadDetailBill"+   
            //             " where Date>='2017-07-01' and doc_no!=0 and Date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //             + "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by Date";

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,doc_no as Invoice_No,Gst_No as PartyGSTNo," +
                         " Customer_Code as PartyCode,Ac_Name_E as PartyName,CustomerStateCode as Partygststatecode," +
                         " CONVERT(varchar(10),Date,103) as Invoice_Date,(select top (1)HSN from qryColdStorageDetail where Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") as hsn," +
                         " Subtotal as TaxableAmount,  CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,Final_Amount as Payable_Amount" +
                         " from qryColdStorageHead where Date>='2017-07-01' and doc_no!=0 and Date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                         " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by Date";

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "ColdStorage OutWord Summary";


                    grdAll.DataSource = dt;
                    grdAll.DataBind();


                    double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Payable_Amount)", string.Empty));


                    grdAll.FooterRow.Cells[8].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalPayable_Amount.ToString();


                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnSaleTDS_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "";

            if (drpSaleTCS.SelectedValue == "SB")
            {
                //qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(PartyName,',',' '),'.','') as [Name Of Party]," +
                //               " REPLACE(billtopanno,',',' ') as Pan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,[Invoice Value] as Net, CGSTAmount as CGST," +
                //                " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and Carporate_Sale_No=0 and doc_date between '" + fromdt + "' and '" + todt
                //                + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and doc_No!=0 order by doc_date";

                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(billtoname,',',' '),'.','') as [Name Of Party]," +
                      " REPLACE(billtopanno,',',' ') as Pan,REPLACE(BillToTanNo,',',' ') as Tan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,[Bill_Amount] as Net, CGSTAmount as CGST, " +
                      " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrysalehead where doc_date>='2017-07-01'  and doc_date between '" + fromdt + "' and '" + todt
                      + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and doc_No!=0 order by doc_date";

            }
            if (drpSaleTCS.SelectedValue == "SC")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                              " REPLACE(AC_Pan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan, REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,[Invoice Value] as Net, CGSTAmount as CGST," +
                               " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and Carporate_Sale_No!=0 and doc_date between '" + fromdt + "' and '" + todt
                               + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and doc_No!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "NC")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                              " REPLACE(AC_Pan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan, REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,[Invoice Value] as Net, CGSTAmount as CGST," +
                               " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrysaleheadfor_GSTReturn where doc_date>='2017-07-01' and Carporate_Sale_No!=0 and doc_date between '" + fromdt + "' and '" + todt
                               + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and doc_No!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "RR")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RR'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(PartyName,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(BillToTan,',',' ') as Tan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,NetPayble as Net, sum(CGSTAmount) as CGST," +
                             " sum(SGSTAmount) as SGST,sum(IGSTAmount) as IGST,TDS_Amt as TDS from qryRetailSale where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and IsDelete!=0 " +
                            " group by doc_no,Doc_Date,PartyName,CompanyPan,BillToTan,billtoaddress,NetPayble ,TDS_Amt,Doc_No";
            }
            if (drpSaleTCS.SelectedValue == "LV")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'LV'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,TCS_Net_Payable as Net, cgst_amount as CGST," +
                             " sgst_amount as SGST,igst_amount as IGST,TDSAmount as TDS from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDSAmount!=0  order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "CB")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','')  as Address,TCS_Net_Payable as Net, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qryColdStorageHead where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 order by Date";
            }
            if (drpSaleTCS.SelectedValue == "RB")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,REPLACE(REPLACE(Ac_Name_E,',',' '),'.','') as [Name Of Party]," +
                            " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan, REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,Final_Amount as Net, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST, TDS as TDS from qryrentbillhead where Date>='2017-07-01' and Date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and IsDeleted!=0 and TDS!=0 order by Date";
            }
            if (drpSaleTCS.SelectedValue == "RS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RS'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date, REPLACE(REPLACE(FromAcName,',',' '),'.','')  as [Name Of Party]," +
                    " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(billtoaddress,',',' '),'.','') as Address,TCS_Net_Payable as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST," +
                    " TDS_Amt as TDS from qrysugarsalereturnhead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "DN")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'DN' +convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date, REPLACE(REPLACE(Ac_Name_E,',',' '),'.','')  as [Name Of Party]," +
                    " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,TCS_Net_Payable as Net, cgst_amount as CGST, sgst_amount as SGST,igst_amount as IGST," +
                    " TDS_Amt as TDS from qrydebitnotehead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and tran_type='DN' and isDeleted!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "DS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'DS' +convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date, REPLACE(REPLACE(Ac_Name_E,',',' '),'.','')  as [Name Of Party]," +
                   " REPLACE(CompanyPan,',',' ') as Pan,REPLACE(Tan_no,',',' ') as Tan,REPLACE(REPLACE(Address_E,',',' '),'.','') as Address,TCS_Net_Payable as Net, cgst_amount as CGST, sgst_amount as SGST,igst_amount as IGST," +
                   " TDS_Amt as TDS from qrydebitnotehead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                   + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and tran_type='DS' and isDeleted!=0 order by doc_date";
            }
            if (drpSaleTCS.SelectedValue == "All")
            {
                #region
                //qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'SB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,CarporateBillTo_Name as [Name Of Party]," +
                //      " Carporate_Pan as Pan,Carporate_Address as Address,Bill_Amount as Net, CGSTAmount as CGST,SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qrysalehead union "



                //      + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RR'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,partyname as [Name Of Party]," +
                //             " CompanyPan as Pan,billtoaddress as Address,NetPayble as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amount as TCS from qryRetailSale union "

                //      + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'LV'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                //            " CompanyPan as Pan,Address_E as Address,TCS_Net_Payable as Net, cgst_amount as CGST,sgst_amount as SGST,igst_amount as IGST,TCS_Amt as TCS from qrycommissionbill union "

                //      + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,Ac_Name_E as [Name Of Party]," +
                //            " CompanyPan as Pan,Address_E as Address,TCS_Net_Payable as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qryColdStorageHead union "

                //     + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RB'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),Date,103) as date,Ac_Name_E as [Name Of Party]," +
                //            " CompanyPan as Pan,Address_E as Address,Final_Amount as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST,TCS_Amt as TCS from qryrentbillhead union "

                //    + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RS'+convert(varchar(50),doc_no) as InvoiceNo,CONVERT(varchar(10),doc_date,103) as date,FromAcName as [Name Of Party]," +
                //    " CompanyPan as Pan,billtoaddress as Address,TCS_Net_Payable as Net, CGSTAmount as CGST, SGSTAmount as SGST,IGSTAmount as IGST," +
                //    " TCS_Amt as TCS from qrysugarsalereturnhead  where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                //    + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TCS_Amt!=0 ";
                #endregion

                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No, InvoiceNo,CONVERT(varchar(10),date,103) as date, [Name Of Party],  REPLACE(Pan,',',' ') as Pan,REPLACE(Tan,',',' ') as Tan,  REPLACE(REPLACE(Address,',',' '),'.','') as Address," +
                       " Net, CGST, SGST, IGST,TDS_Amt as  TDS from qryTCSAllUnion where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 " +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 ";
            }
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    dt = ds.Tables[0];

                    if (drpSaleTCS.SelectedValue == "All")
                    {
                        lblSummary.Text = "Sale TDS Summary";
                    }

                    if (drpSaleTCS.SelectedValue == "SB" || drpSaleTCS.SelectedValue == "SC" || drpSaleTCS.SelectedValue == "NC")
                    {
                        lblSummary.Text = "Sale TDS Summary";
                    }

                    if (drpSaleTCS.SelectedValue == "RS")
                    {
                        lblSummary.Text = "Sale Return Sale TDS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "RR")
                    {
                        lblSummary.Text = "Retail Sale TDS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "LV")
                    {
                        lblSummary.Text = "Commission Bill TDS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "CB")
                    {
                        lblSummary.Text = "Could Storage TDS Summary";
                    }
                    if (drpSaleTCS.SelectedValue == "RB")
                    {
                        lblSummary.Text = "Rent Bill TDS Summary";
                    }
                    grdAll.DataSource = dt;
                    grdAll.DataBind();


                    double totalNet = Convert.ToDouble(dt.Compute("SUM(Net)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalTCS = Convert.ToDouble(dt.Compute("SUM(TDS)", string.Empty));


                    grdAll.FooterRow.Cells[7].Text = totalNet.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalTCS.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                }

                if (hdconfirm.Value == "Yes")
                {
                    #region[csv]
                    string fileName = "";
                    string strForCSV = "";
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = "LEN(Pan) < 1";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongPanNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 5;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 6)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "InvoiceNo", "Pan");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "SaleTDS.csv";

                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                    #endregion
                }
            }
            else
            {
                grdAll.DataSource = null;
                grdAll.DataBind();
                lblSummary.Text = "Records Not Found";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnPurchaseTDS_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "";
            if (drppurchasrtcs.SelectedValue == "PS")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppliername as [Name Of Party]," +
                            " CompanyPan as Pan,Tan_no as Tan,supplieraddress as Address,Bill_Amount as Net, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrypurchasehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 order by doc_date";

            }

            if (drppurchasrtcs.SelectedValue == "RP")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,partyname as [Name Of Party]," +
                            " CompanyPan as Pan,Tan_no as Tan,billtoaddress as Address,NetPayble as Net, CGSTAmount as CGST," +
                             " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qryRetailPurchase where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "CV")
            {
                qry = " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CV'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,abs(TDSAmount) as TDS from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDSAmount!=0 and Tran_Type='CV' order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "CN")
            {
                qry = " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CN'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,TDS_Amt as TDS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                             + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and IsDeleted <> 0 AND Tran_Type='CN' order by doc_date";


            }
            if (drppurchasrtcs.SelectedValue == "CS")
            {
                qry = " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                 "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                 "sgst_amount as SGST,igst_amount as IGST,TDS_Amt as TDS from qrydebitnotehead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                        + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 and IsDeleted <> 0 AND Tran_Type='CS' order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "PR")
            {
                qry = " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PR'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party],"+
                 "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgstamount as CGST,"+
                 "sgstamount as SGST,igstamount as IGST,TDS_Amt as TDS from qrysugarpurchasereturnhead where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                        + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0  order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "XP")
            {
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'XP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppilername as [Name Of Party]," +
                     "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, CGST_Amount as CGST," +
                 "sgst_amount as SGST,igst_amount as IGST,TDS as TDS from qryotherpurchase where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                        + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS!=0  order by doc_date";

            }
            if (drppurchasrtcs.SelectedValue == "All")
            {
                #region[qry All]
                // qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppliername as [Name Of Party]," +
               //           " CompanyPan as Pan,Tan_no as Tan,supplieraddress as Address,Bill_Amount as Net, CGSTAmount as CGST," +
               //            " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrypurchasehead where TDS_Amt!=0   Union "



               //+ "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'RP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,partyname as [Name Of Party]," +
               //            " CompanyPan as Pan,Tan_no as Tan,billtoaddress as Address,NetPayble as Net, CGSTAmount as CGST," +
               //             " SGSTAmount as SGST,IGSTAmount as IGST,0 as TDS from qryRetailPurchase    Union "

               //             + " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CV'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
               //       "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
               //       "sgst_amount as SGST,igst_amount as IGST,abs(TDSAmount) as TDS from qrycommissionbill where  TDSAmount!=0 and Tran_Type='CV'  union "

               //              + " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CN'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
               //       "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
               //       "sgst_amount as SGST,igst_amount as IGST,TDS_Amt as TDS from qrydebitnotehead where  TDS_Amt!=0 and IsDeleted != 0 AND Tran_Type='CN'  Union "

               //              + " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
               //  "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
               //  "sgst_amount as SGST,igst_amount as IGST,TDS_Amt as TDS from qrydebitnotehead where  TDS_Amt!=0 and IsDeleted != 0 AND Tran_Type='CS'  Union "

               //         + " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PR'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
               //  "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgstamount as CGST," +
               //  "sgstamount as SGST,igstamount as IGST,TDS_Amt as TDS from qrysugarpurchasereturnhead where TDS_Amt!=0   union "

               //         + "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'XP'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppilername as [Name Of Party]," +
               //      "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, CGST_Amount as CGST," +
               //  "sgst_amount as SGST,igst_amount as IGST,TDS as TDS from qryotherpurchase where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                //         + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS!=0  "; 
                #endregion

                //qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,PSNo,CONVERT(varchar(10),date,103) as date,[Name Of Party] as Name_Of_Party,PartyGst_No , " 
                //      + " REPLACE(Pan,',',' ') as Pan,REPLACE(Tan,',',' ') as Tan, "
                //      + " Bill_Amount as Net, CGST as CGST, SGST as SGST, IGST as IGST, TDS_Amt as TDS " 
                //      + " from qryTCSPurchaseUnion where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt + "' " +
                //      "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "   and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS_Amt!=0 " + 
                //       "group by [Name Of Party],PartyGst_No,Pan,Tan,date,PSNo,CGST,SGST,IGST,TDS_Amt,Bill_Amount";
	
                qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'PS'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,suppliername as [Name Of Party]," +
                          " CompanyPan as Pan,Tan_no as Tan,supplieraddress as Address,Bill_Amount as Net, CGSTAmount as CGST," +
                           " SGSTAmount as SGST,IGSTAmount as IGST,TDS_Amt as TDS from qrypurchasehead where TDS_Amt!=0 and doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                         + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "     Union "

                         + " select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'CV'+convert(varchar(50),doc_no) as PSNo,CONVERT(varchar(10),doc_date,103) as date,Ac_Name_E as [Name Of Party]," +
                     "CompanyPan as Pan,Tan_no as Tan,Address_E as Address,Bill_Amount as Net, cgst_amount as CGST," +
                      "sgst_amount as SGST,igst_amount as IGST,abs(TDSAmount) as TDS from qrycommissionbill where doc_date>='2017-07-01' and doc_date between '" + fromdt + "' and '" + todt
                         + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and TDS!=0 and Tran_Type='CV' "; 


            }
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];

                    DataView dv = dt1.DefaultView;
                    dv.Sort = "date asc";
                    dt = dv.ToTable();

                    // dt.Select(dt.)

                    if (drppurchasrtcs.SelectedValue == "All")
                    {
                        lblSummary.Text = "Purchase TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "PS")
                    {
                        lblSummary.Text = "Purchase TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "RP")
                    {
                        lblSummary.Text = "Retail Purchase TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CV")
                    {
                        lblSummary.Text = "Commission TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CN")
                    {
                        lblSummary.Text = "Credit Note to Customer TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CS")
                    {
                        lblSummary.Text = "Credit Note to Supplier TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "PR")
                    {
                        lblSummary.Text = "Sugar Sale Return Purchase TDS Summary";
                    }
                    if (drppurchasrtcs.SelectedValue == "CS")
                    {
                        lblSummary.Text = "Other Purchase TDS Summary";
                    }
                    grdAll.DataSource = dt;
                    grdAll.DataBind();
                    double totalNet = Convert.ToDouble(dt.Compute("SUM(Net)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalTDS = Convert.ToDouble(dt.Compute("SUM(TDS)", string.Empty));

                    grdAll.FooterRow.Cells[7].Text = totalNet.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalTDS.ToString();

                    grdAll.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;


                }
                if (hdconfirm.Value == "Yes")
                {
                    #region[csv]
                    string fileName = "";
                    string strForCSV = "";
                    DataView dvWrongGst = new DataView(dt);
                    dvWrongGst.RowFilter = "LEN(Pan) < 1 ";

                    if (dvWrongGst.ToTable().Rows.Count > 0)
                    {
                        fileName = "WrongPanNumbers.csv";
                        DataTable dtnew = dvWrongGst.ToTable();

                        int colindex = 5;
                        for (int i = 0; i < dvWrongGst.ToTable().Columns.Count; i++)
                        {
                            dtnew.Columns.RemoveAt(colindex);
                            if (dtnew.Columns.Count < 6)
                            {
                                break;
                            }
                        }
                        dtnew = dtnew.DefaultView.ToTable(true, "PSNo", "Pan");
                        strForCSV = clsCommon.DataTableToCSV(dtnew, ',');
                    }
                    else
                    {
                        fileName = "PurchaseTDS.csv";

                        strForCSV = clsCommon.DataTableToCSV(dt, ',');
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(strForCSV.ToString());
                    Response.Flush();
                    Response.End();
                    #endregion
                }
            }
            else
            {
                grdAll.DataSource = null;
                grdAll.DataBind();

                lblSummary.Text = "Records Not Found";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnTransportTDS_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,d.doc_no as DO_No,CONVERT(varchar(10),d.doc_date,103) as Date,T.Ac_Name_E as TransportName, " +
                         "d.Pan_No,T.Tan_no,T.Address_E,d.MillGSTStateCode as MillStateCode,d.Memo_Advance as Amount,case ISNULL(d.SaleBillTo,0) when 0 then 27 else d.TransportGSTStateCode end as TransportStateCode, d.TDSAmt " +
                         "from NT_1_deliveryorder d  LEFT OUTER JOIN dbo.nt_1_accountmaster AS T ON d.transport = T.Ac_Code and " +
                         "d.company_code = T.company_code  where d.doc_date>='2017-07-01' and " +
                         " d.doc_date between '" + fromdt + "' and '" + todt + "' and d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0 and TDSAmt!=0 order by d.doc_date";

            DataSet ds1 = new DataSet();
            ds1 = clsDAL.SimpleQuery(qry);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds1.Tables[0];

                    dt1.Columns.Add(new DataColumn("CGST", typeof(double)));
                    dt1.Columns.Add(new DataColumn("SGST", typeof(double)));
                    dt1.Columns.Add(new DataColumn("IGST", typeof(double)));
                    //dt1.Columns.Add(new DataColumn("TDSAmt", typeof(double)));

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        double cgstrate = 2.5;
                        double sgstrate = 2.5;
                        double igstrate = 5;

                        double CGSTAmount = 0.0;
                        double SGSTAmount = 0.0;
                        double IGSTAmount = 0.0;

                        int millStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"]);
                        int partyStateCode = Convert.ToInt32(dt1.Rows[i]["TransportStateCode"].ToString());
                        double Amount = Convert.ToDouble(dt1.Rows[i]["Amount"].ToString());
                        double TDSAmount = Convert.ToDouble(dt1.Rows[i]["TDSAmt"].ToString());
                        if (millStateCode == partyStateCode)
                        {
                            CGSTAmount = Math.Round((Amount * cgstrate / 100), 2);
                            SGSTAmount = Math.Round((Amount * sgstrate / 100), 2);
                        }
                        else
                        {
                            IGSTAmount = Math.Round((Amount * igstrate / 100), 2);
                        }

                        dt1.Rows[i]["CGST"] = CGSTAmount;
                        dt1.Rows[i]["SGST"] = SGSTAmount;
                        dt1.Rows[i]["IGST"] = IGSTAmount;
                        //dt1.Rows[i]["TDSAmt"] = TDSAmount;
                        // dt1.Rows[i]["FinalAmount"] = Math.Round((Amount + CGSTAmount + SGSTAmount + IGSTAmount), 2);
                    }
                    lblSummary.Text = "Transport TDS";
                    grdAll.DataSource = dt1;
                    grdAll.DataBind();

                    //double totalQuintal = Convert.ToDouble(dt1.Compute("SUM(Quintal)", string.Empty));
                    double totalBagBillAmount = Convert.ToDouble(dt1.Compute("SUM(Amount)", string.Empty));
                    double totalTDSAmt = Convert.ToDouble(dt1.Compute("SUM(TDSAmt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt1.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt1.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt1.Compute("SUM(IGST)", string.Empty));
                    //double totalPayable_Amount = Convert.ToDouble(dt1.Compute("SUM(FinalAmount)", string.Empty));

                    //grdAll.FooterRow.Cells[12].Text = totalQuintal.ToString();
                    grdAll.FooterRow.Cells[8].Text = totalBagBillAmount.ToString();
                    grdAll.FooterRow.Cells[10].Text = totalTDSAmt.ToString();
                    grdAll.FooterRow.Cells[11].Text = totalCGST.ToString();
                    grdAll.FooterRow.Cells[12].Text = totalSGST.ToString();
                    grdAll.FooterRow.Cells[13].Text = totalIGST.ToString();
                    //grdAll.FooterRow.Cells[18].Text = totalPayable_Amount.ToString();

                    //grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    grdAll.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    //grdAll.FooterRow.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnAllTCSTDS_Click(object sender, EventArgs e)
    {
        try
        {
            grdAll.DataSource = null;
            grdAll.DataBind();
            grdDetail_Report.DataSource = null;
            grdDetail_Report.DataBind();
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");


            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'' as fromDate,'' as ToDate,[Name Of Party] as Name_Of_Party,PartyGst_No ,REPLACE(Pan,',',' ') as Pan," +
                       "REPLACE(Tan,',',' ') as Tan,sum(Taxable_Amt) as Taxable_Amt, sum(CGST) as CGST, sum(SGST) as SGST, sum(IGST) as IGST, sum(TCS) as TCS," +
                       "sum(TDS_Amt) as TDS,sum(Net) as Bill_Amt,Party_Email,'' as GSTPermission,'' as TDSPermission,'' as TCSPermission,'' as Permission from qryTCSAllUnion where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt + "' " +
                       "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and IsDeleted!=0 and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " group by [Name Of Party],PartyGst_No,Pan,Tan,Party_Email";

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    DataTable dtgrid = new DataTable();
                    DataRow dr = null;
                    dtgrid.Columns.Add(new DataColumn("SR_No", typeof(int)));
                    dtgrid.Columns.Add(new DataColumn("fromDate", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("ToDate", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Name_Of_Party", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("PartyGst_No", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Pan", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Tan", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Taxable_Amt", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("CGST", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("SGST", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("IGST", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("TCS", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("TDS", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("Bill_Amt", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("Party_Email", typeof(string)));

                    lblSummary.Text = "Sale TCS TDS Summary";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["fromDate"] = txtFromDt.Text;
                        dt.Rows[i]["ToDate"] = txtToDt.Text;

                        dr = dtgrid.NewRow();
                        dr["SR_No"] = dt.Rows[i]["SR_No"];
                        dr["fromDate"] = dt.Rows[i]["fromDate"];
                        dr["ToDate"] = dt.Rows[i]["ToDate"];
                        dr["Name_Of_Party"] = dt.Rows[i]["Name_Of_Party"];
                        dr["PartyGst_No"] = dt.Rows[i]["PartyGst_No"];
                        dr["Pan"] = dt.Rows[i]["Pan"];
                        dr["Tan"] = dt.Rows[i]["Tan"];
                        dr["Taxable_Amt"] = dt.Rows[i]["Taxable_Amt"];
                        dr["CGST"] = dt.Rows[i]["CGST"];
                        dr["SGST"] = dt.Rows[i]["SGST"];
                        dr["IGST"] = dt.Rows[i]["IGST"];
                        dr["TCS"] = dt.Rows[i]["TCS"];
                        dr["TDS"] = dt.Rows[i]["TDS"];
                        dr["Bill_Amt"] = dt.Rows[i]["Bill_Amt"];
                        dr["Party_Email"] = dt.Rows[i]["Party_Email"];
                        dtgrid.Rows.Add(dr);
                    }

                    grdDetail_Report.DataSource = dt;
                    grdDetail_Report.DataBind();
                    ViewState["currentTable"] = dtgrid;


                    double totaltax = Convert.ToDouble(dt.Compute("SUM(Taxable_Amt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalTCS = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));
                    double totalTDS = Convert.ToDouble(dt.Compute("SUM(TDS)", string.Empty));
                    double totalNet = Convert.ToDouble(dt.Compute("SUM(Bill_Amt)", string.Empty));

                    grdDetail_Report.FooterRow.Cells[7].Text = totaltax.ToString();
                    grdDetail_Report.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdDetail_Report.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdDetail_Report.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdDetail_Report.FooterRow.Cells[11].Text = totalTCS.ToString();
                    grdDetail_Report.FooterRow.Cells[12].Text = totalTDS.ToString();
                    grdDetail_Report.FooterRow.Cells[13].Text = totalNet.ToString();


                    grdDetail_Report.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;


                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        string mail = string.Empty;
        string pdfname = string.Empty;
        try
        {
            if (grdDetail_Report.Rows.Count > 0)
            {
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                for (int i = 0; i < grdDetail_Report.Rows.Count; i++)
                {
                    CheckBox chkpermissionGST = (CheckBox)grdDetail_Report.Rows[i].Cells[15].FindControl("chkpermissionGST");
                    CheckBox checkTDS = (CheckBox)grdDetail_Report.Rows[i].Cells[16].FindControl("chkpermissionTDS");
                    CheckBox chkpermissionTCS = (CheckBox)grdDetail_Report.Rows[i].Cells[17].FindControl("chkpermissionTCS");
                    CheckBox chkpermission = (CheckBox)grdDetail_Report.Rows[i].Cells[18].FindControl("chkpermission");
                    TextBox txtPartyEmail = (TextBox)grdDetail_Report.Rows[i].Cells[18].FindControl("txtemail");
                    int count = 0;

                    //DataTable dt = new DataTable();
                    //DataRow dr = null;
                    //dt.Columns.Add((new DataColumn("GST_NO", typeof(string))));
                    //dt.Columns.Add((new DataColumn("Pan_No", typeof(string))));
                    //dt.Columns.Add((new DataColumn("TAN_NO", typeof(string))));
                    //dt.Columns.Add((new DataColumn("Name_of_Party", typeof(string))));
                    //dt.Columns.Add((new DataColumn("Taxable_Amt", typeof(double))));
                    //dt.Columns.Add((new DataColumn("CGST_Amt", typeof(double))));
                    //dt.Columns.Add((new DataColumn("SGST_Amt", typeof(double))));
                    //dt.Columns.Add((new DataColumn("IGST_Amt", typeof(double))));
                    //dt.Columns.Add((new DataColumn("Bill_Amount", typeof(double))));
                    //dr = dt.NewRow();

                    string GSTbody = string.Empty;
                    string GSTsub = string.Empty;

                    string TDSbody = string.Empty;
                    string TDSsub = string.Empty;

                    string TCSbody = string.Empty;
                    string TCSsub = string.Empty;

                    string Allbody = string.Empty;
                    string Allsub = string.Empty;
                    if (lblSummary.Text == "PS TCS TDS Summary")
                    {
                        if (chkpermissionGST.Checked == true)
                        {
                            //dr["GST_NO"] = grdDetail_Report.Rows[i].Cells[4].Text.ToString();
                            //dr["Pan_No"] = grdDetail_Report.Rows[i].Cells[5].Text.ToString();
                            //dr["TAN_NO"] = grdDetail_Report.Rows[i].Cells[6].Text.ToString();
                            //dr["Name_of_Party"] = grdDetail_Report.Rows[i].Cells[3].Text.ToString();
                            //dr["Taxable_Amt"] = grdDetail_Report.Rows[i].Cells[7].Text.ToString();
                            //dr["CGST_Amt"] = grdDetail_Report.Rows[i].Cells[8].Text.ToString();
                            //dr["SGST_Amt"] = grdDetail_Report.Rows[i].Cells[9].Text.ToString();
                            //dr["IGST_Amt"] = grdDetail_Report.Rows[i].Cells[10].Text.ToString();
                            //dr["Bill_Amount"] = grdDetail_Report.Rows[i].Cells[13].Text.ToString();

                            //dt.Rows.Add(dr);
                            GSTbody = "To our Esteemed  business associates ,<br /> As per the new GST guidelines we may get INPUT TAX CREDIT (ITC)  " +
                                           "on only those purchases which will now reflect in GSTR2b.<br /> So henceforth the filing of your GSTR 1 should be well " +
                                           "in stipulated time. As well the generation of E-invoice is <br /> also very important for the reflection of GST credit in " +
                                           "GST R2B.<br /><br /> Following is our quantum purchased from you please revert if any difference at your end.<br /><br />" +
                                           "<table><tr><td align=center>GST NO</td><td></td><td align=center>Pan No</td><td></td><td align=center>TAN NO</td><td></td><td align=center>Name of Party</td><td>Taxable Amt</td><td>CGST Amt</td><td>SGST Amt</td><td>IGST Amt</td><td>Bill Amount</td></tr>" +
                                           "<tr><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[5].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[6].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdDetail_Report.Rows[i].Cells[3].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[7].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[8].Text.ToString() + "</td>" +
                                           "<td align=right>" + grdDetail_Report.Rows[i].Cells[9].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[10].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[13].Text.ToString() + "</td></tr></table>" +
                                           "<h4 style=color:Red;>Also we request you to file your GSTR1 within the 10th of this month. Any penalties or interest if levied on us for late filing will be your responsibility and Liability.If already filed please ignore this mail </h4>";
                            GSTsub = "RECONCILATION AND REMINDER TO FILE YOUR GSTR1";
                            count = count + 1;

                        }
                        if (checkTDS.Checked == true)
                        {
                            TDSbody = "Respected business associates<br /> We have deducted TDS  against our purchases  to you as below.Please reconcile and let us know if any differences arise. <br /><br />" +
                                      "<table><tr><td align=center>Party Name</td><td></td><td align=center>GST NO</td><td></td><td align=center>PAN NO</td><td></td><td align=center>TAN NO</td><td>TAXABLE VALUE</td><td>TDS DEDUCTED</td><td>TOTAL SALE VALUE</td>" +
                                           "</tr><tr><td>" + grdDetail_Report.Rows[i].Cells[3].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[5].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdDetail_Report.Rows[i].Cells[6].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[7].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[12].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[13].Text.ToString() + "</td></tr></table>";
                            TDSsub = "RECONCILIATION OF PURCHASE TDS " + txtFromDt.Text + " " + txtToDt.Text;
                            count = count + 1;

                        }
                        if (chkpermissionTCS.Checked == true)
                        {

                            TCSbody = "Respected business associates<br />  We have claimed TCS  against our purchases  to you as below.Please reconcile and let us know if any differences arise.  <br /><br />" +
                                      "<table><tr><td align=center>Party Name</td><td></td><td align=center>GST NO</td><td></td><td align=center>PAN NO</td><td></td><td align=center>TAN NO</td><td>TAXABLE VALUE</td><td>TCS DEDUCTED</td><td>TOTAL SALE VALUE</td>" +
                                           "</tr><tr><td>" + grdDetail_Report.Rows[i].Cells[3].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[5].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdDetail_Report.Rows[i].Cells[6].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[7].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[11].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[13].Text.ToString() + "</td></tr></table>" +
                                           "NOTE - Please pay the TCS  and also file the quarterly returns   timely before the due date. Also please share the 26AS as well as TCS certificate<br/>" +
                                           "once generated in every quarter. Ignore if TCS paid and Return filed.";
                            TCSsub = "RECONCILIATION OF PURCHASE TCS " + txtFromDt.Text + " " + txtToDt.Text;
                            count = count + 1;

                        }
                        if (chkpermission.Checked == true)
                        {

                            Allbody = "To our Esteemed  business associates ,<br /> As per the new GST guidelines we may get INPUT TAX CREDIT (ITC)  " +
                                           "on only those purchases which will now reflect in GSTR2b.<br /> So henceforth the filing of your GSTR 1 should be well " +
                                           "in stipulated time. As well the generation of E-invoice is <br /> also very important for the reflection of GST credit in " +
                                           "GST R2B.<br /><br /> Following is our quantum purchased from you please revert if any difference at your end.<br /><br />" +
                                           "<table><tr><td align=center>GST NO</td><td align=center>Pan No</td><td>TAN NO</td><td>Name of Party</td><td>Taxable Amt</td><td>CGST Amt</td><td>SGST Amt</td><td>IGST Amt</td><td>Bill Amount</td>" +
                                           "</tr><tr><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td></tr></table>";
                            Allsub = "RECONCILATION AND REMINDER TO FILE YOUR GSTR1,TDS.TCS";
                            count = count + 1;

                        }
                    }
                    else
                    {
                        if (chkpermissionGST.Checked == true)
                        {
                            //dr["GST_NO"] = grdDetail_Report.Rows[i].Cells[4].Text.ToString();
                            //dr["Pan_No"] = grdDetail_Report.Rows[i].Cells[5].Text.ToString();
                            //dr["TAN_NO"] = grdDetail_Report.Rows[i].Cells[6].Text.ToString();
                            //dr["Name_of_Party"] = grdDetail_Report.Rows[i].Cells[3].Text.ToString();
                            //dr["Taxable_Amt"] = grdDetail_Report.Rows[i].Cells[7].Text.ToString();
                            //dr["CGST_Amt"] = grdDetail_Report.Rows[i].Cells[8].Text.ToString();
                            //dr["SGST_Amt"] = grdDetail_Report.Rows[i].Cells[9].Text.ToString();
                            //dr["IGST_Amt"] = grdDetail_Report.Rows[i].Cells[10].Text.ToString();
                            //dr["Bill_Amount"] = grdDetail_Report.Rows[i].Cells[13].Text.ToString();

                            //dt.Rows.Add(dr);
                            GSTbody = "Respected business associates,<br /> We have generated Tax invoices on you for the above mentioned period as per the following data . Please reconcile and let us know if any differences arise<br /><br />" +
                                           "<table><tr><td align=center>Party Name</td><td></td><td align=center>GST NO</td><td></td><td align=center>PAN NO</td><td></td><td align=center>TAN No</td><td>TAXABLE VALUE</td><td>CGST Amt</td><td>SGST Amt</td><td>IGST Amt</td><td>TOTAL SALE VALUE </td></tr>" +
                                           "<tr><td>" + grdDetail_Report.Rows[i].Cells[3].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[5].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdDetail_Report.Rows[i].Cells[6].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[7].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[8].Text.ToString() + "</td>" +
                                           "<td align=right>" + grdDetail_Report.Rows[i].Cells[9].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[10].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[13].Text.ToString() + "</td></tr></table>";
                            GSTsub = "RECONCILIATION OF MONTHLY GSTR1 ON SALES " + txtFromDt.Text + "" + txtToDt.Text;
                            count = count + 1;

                        }
                        if (checkTDS.Checked == true)
                        {
                            TDSbody = "Respected business associates<br /> We have claimed the monthly TDS against our sales to you as below.Please reconcile and let us know if any differences arise.<br /><br />" +
                                      "<table><tr><td align=center>Party Name</td><td></td><td align=center>GST NO</td><td></td><td align=center>PAN NO</td><td></td><td align=center>TAN No</td><td>TAXABLE VALUE</td><td>TDS TO BE CLAIMED</td><td>TOTAL SALE VALUE</td>" +
                                           "</tr><tr><td>" + grdDetail_Report.Rows[i].Cells[3].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[5].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdDetail_Report.Rows[i].Cells[6].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[7].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[12].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[13].Text.ToString() + "</td></tr></table>";
                            TDSsub = " RECONCILIATION OF TDS ON SALES " + txtFromDt.Text + " " + txtToDt.Text;
                            count = count + 1;

                        }
                        if (chkpermissionTCS.Checked == true)
                        {

                            TCSbody = "Respected business associates<br />  We have collected  the monthly TCS against our sales to you as below.Please reconcile and let us know if any differences arise.<br /><br />" +
                                      "<table><tr><td align=center>Party Name</td><td></td><td align=center>GST NO</td><td></td><td align=center>PAN NO</td><td></td><td align=center>TAN NO</td><td>TAXABLE VALUE</td><td>TCS COLLECTED</td><td>TOTAL SALE VALUE</td>" +
                                           "</tr><tr><td>" + grdDetail_Report.Rows[i].Cells[3].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td><td></td><td>" + grdDetail_Report.Rows[i].Cells[5].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdDetail_Report.Rows[i].Cells[6].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[7].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[11].Text.ToString() + "</td><td align=right>" + grdDetail_Report.Rows[i].Cells[13].Text.ToString() + "</td></tr></table>";
                            TCSsub = "RECONCILIATION OF TCS ON SALES " + txtFromDt.Text + " " + txtToDt.Text;
                            count = count + 1;

                        }
                        if (chkpermission.Checked == true)
                        {

                            Allbody = "To our Esteemed  business associates ,<br /> As per the new GST guidelines we may get INPUT TAX CREDIT (ITC)  " +
                                           "on only those purchases which will now reflect in GSTR2b.<br /> So henceforth the filing of your GSTR 1 should be well " +
                                           "in stipulated time. As well the generation of E-invoice is <br /> also very important for the reflection of GST credit in " +
                                           "GST R2B.<br /><br /> Following is our quantum purchased from you please revert if any difference at your end.<br /><br />" +
                                           "<table><tr><td>GST NO</td><td>Pan No</td><td>TAN NO</td><td>Name of Party</td><td>Taxable Amt</td><td>CGST Amt</td><td>SGST Amt</td><td>IGST Amt</td><td>Bill Amount</td>" +
                                           "</tr><tr><td>" + grdDetail_Report.Rows[i].Cells[4].Text.ToString() + "</td></tr></table>";
                            Allsub = "RECONCILATION AND REMINDER TO FILE YOUR GSTR1,TDS.TCS";
                            count = count + 1;

                        }
                    }

                    //mail = grdDetail_Report.Rows[i].Cells[14].Text.ToString();
                    mail = txtPartyEmail.Text;
                    if (mail != string.Empty)
                    {

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MailMessage msg = new MailMessage();
                            if (chkpermissionGST.Checked == true)
                            {
                                SmtpServer.Host = clsGV.Email_Address;
                                msg.From = new MailAddress(mailFrom);
                                msg.To.Add(mail);
                                msg.IsBodyHtml = true;
                                msg.Body = GSTbody;
                                msg.Subject = GSTsub;

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
                                //attachment.Dispose();
                                if (File.Exists(pdfname))
                                {
                                    File.Delete(pdfname);
                                }
                            }

                            if (checkTDS.Checked == true)
                            {
                                SmtpServer.Host = clsGV.Email_Address;
                                msg.From = new MailAddress(mailFrom);
                                msg.To.Add(mail);
                                msg.IsBodyHtml = true;
                                msg.Body = TDSbody;
                                msg.Subject = TDSsub;

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
                                //attachment.Dispose();
                                if (File.Exists(pdfname))
                                {
                                    File.Delete(pdfname);
                                }
                            }

                            if (chkpermissionTCS.Checked == true)
                            {
                                SmtpServer.Host = clsGV.Email_Address;
                                msg.From = new MailAddress(mailFrom);
                                msg.To.Add(mail);
                                msg.IsBodyHtml = true;
                                msg.Body = TCSbody;
                                msg.Subject = TCSsub;

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
                                //attachment.Dispose();
                                if (File.Exists(pdfname))
                                {
                                    File.Delete(pdfname);
                                }
                            }

                            if (chkpermission.Checked == true)
                            {
                                SmtpServer.Host = clsGV.Email_Address;
                                msg.From = new MailAddress(mailFrom);
                                msg.To.Add(mail);
                                msg.IsBodyHtml = true;
                                msg.Body = Allbody;
                                msg.Subject = Allsub;

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
                                //attachment.Dispose();
                                if (File.Exists(pdfname))
                                {
                                    File.Delete(pdfname);
                                }

                            }


                            //msg.IsBodyHtml = true;


                        }

                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:alert('Message Sent Successfully!')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #region [RowCommandReport]
    protected void grdDetail_Report_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //int rowindex = row.RowIndex;
            //if (e.CommandArgument == "lnk")
            //{
            //    switch (e.CommandName)
            //    {
            //        case "EditRecord":
            //            if (grdDetail_Report.Rows[rowindex].Cells[RowactionResult].Text != "D" && grdDetail_Report.Rows[rowindex].Cells[RowactionResult].Text != "R")//add row action id
            //            {
            //                pnlPopupDetails.Style["display"] = "none";
            //                this.showDetailsRowResult(grdDetail_Report.Rows[rowindex]);

            //            }
            //            break;
            //        case "DeleteRecord":
            //            string action = "";
            //            LinkButton lnkDelete = (LinkButton)e.CommandSource;
            //            if (lnkDelete.Text == "Delete")
            //            {
            //                action = "Delete";
            //                lnkDelete.Text = "Open";
            //            }
            //            else
            //            {
            //                action = "Open";
            //                lnkDelete.Text = "Delete";
            //            }
            //            this.DeleteDetailsRowResult(grdDetail_Report.Rows[rowindex], action);
            //            break;
            //    }
            //}
        }
        catch
        {
        }
    }
    #endregion
    #region [grdDetail_Report_RowDataBound]
    protected void grdDetail_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(40);
            e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(30);
            e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(30);
            e.Row.Cells[7].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[9].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[10].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[11].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[12].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[13].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[14].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[15].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[16].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[17].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[18].ControlStyle.Width = Unit.Percentage(10);

            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[14].Style["overflow"] = "hidden";

            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;




        }
        catch
        {
        }
    }
    #endregion

    protected void btnPSTCSTDS_Click(object sender, EventArgs e)
    {
        try
        {
            grdAll.DataSource = null;
            grdAll.DataBind();
            grdDetail_Report.DataSource = null;
            grdDetail_Report.DataBind();
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");


            string qry = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'' as fromDate,'' as ToDate,[Name Of Party] as Name_Of_Party,PartyGst_No ,REPLACE(Pan,',',' ') as Pan," +
                       "REPLACE(Tan,',',' ') as Tan,sum(Taxable_Amt) as Taxable_Amt, sum(CGST) as CGST, sum(SGST) as SGST, sum(IGST) as IGST, sum(TCS) as TCS," +
                       "sum(TDS_Amt) as TDS,sum(Bill_Amount) as Bill_Amt,Party_Email,'' as GSTPermission,'' as TDSPermission,'' as TCSPermission,'' as Permission from qryTCSPurchaseUnion where date>='2017-07-01' and date between '" + fromdt + "' and '" + todt + "' " +
                       "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "   and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " group by [Name Of Party],PartyGst_No,Pan,Tan,Party_Email";

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    DataTable dtgrid = new DataTable();
                    DataRow dr = null;
                    dtgrid.Columns.Add(new DataColumn("SR_No", typeof(int)));
                    dtgrid.Columns.Add(new DataColumn("fromDate", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("ToDate", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Name_Of_Party", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("PartyGst_No", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Pan", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Tan", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("Taxable_Amt", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("CGST", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("SGST", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("IGST", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("TCS", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("TDS", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("Bill_Amt", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("Party_Email", typeof(string)));

                    lblSummary.Text = "PS TCS TDS Summary";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["fromDate"] = txtFromDt.Text;
                        dt.Rows[i]["ToDate"] = txtToDt.Text;

                        dr = dtgrid.NewRow();
                        dr["SR_No"] = dt.Rows[i]["SR_No"];
                        dr["fromDate"] = dt.Rows[i]["fromDate"];
                        dr["ToDate"] = dt.Rows[i]["ToDate"];
                        dr["Name_Of_Party"] = dt.Rows[i]["Name_Of_Party"];
                        dr["PartyGst_No"] = dt.Rows[i]["PartyGst_No"];
                        dr["Pan"] = dt.Rows[i]["Pan"];
                        dr["Tan"] = dt.Rows[i]["Tan"];
                        dr["Taxable_Amt"] = dt.Rows[i]["Taxable_Amt"];
                        dr["CGST"] = dt.Rows[i]["CGST"];
                        dr["SGST"] = dt.Rows[i]["SGST"];
                        dr["IGST"] = dt.Rows[i]["IGST"];
                        dr["TCS"] = dt.Rows[i]["TCS"];
                        dr["TDS"] = dt.Rows[i]["TDS"];
                        dr["Bill_Amt"] = dt.Rows[i]["Bill_Amt"];
                        dr["Party_Email"] = dt.Rows[i]["Party_Email"];
                        dtgrid.Rows.Add(dr);
                    }

                    grdDetail_Report.DataSource = dt;
                    grdDetail_Report.DataBind();
                    ViewState["currentTable"] = dtgrid;

                    double totaltax = Convert.ToDouble(dt.Compute("SUM(Taxable_Amt)", string.Empty));
                    double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    double totalTCS = Convert.ToDouble(dt.Compute("SUM(TCS)", string.Empty));
                    double totalTDS = Convert.ToDouble(dt.Compute("SUM(TDS)", string.Empty));
                    double totalNet = Convert.ToDouble(dt.Compute("SUM(Bill_Amt)", string.Empty));

                    grdDetail_Report.FooterRow.Cells[7].Text = totaltax.ToString();
                    grdDetail_Report.FooterRow.Cells[8].Text = totalCGST.ToString();
                    grdDetail_Report.FooterRow.Cells[9].Text = totalSGST.ToString();
                    grdDetail_Report.FooterRow.Cells[10].Text = totalIGST.ToString();
                    grdDetail_Report.FooterRow.Cells[11].Text = totalTCS.ToString();
                    grdDetail_Report.FooterRow.Cells[12].Text = totalTDS.ToString();
                    grdDetail_Report.FooterRow.Cells[13].Text = totalNet.ToString();


                    grdDetail_Report.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    grdDetail_Report.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;


                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnOurDO_Click(object sender, EventArgs e)
    {
        try
        {
            grdAll.DataSource = null;
            grdAll.DataBind();
            grdDetail_Report.DataSource = null;
            grdDetail_Report.DataBind();
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SR_No,'' as fromDate,'' as ToDate,dbo.qrydohead.doc_no, " +
                         "dbo.qrydohead.doc_dateConverted AS Do_Date, dbo.qrydohead.shiptoname, dbo.qrydohead.shiptogstno, " +
                         "dbo.qrydohead.shiptoemail, dbo.qrydohead.quantal, dbo.qrydohead.mill_rate, dbo.qrydohead.sale_rate, " +
                         "dbo.qrydohead.final_amout, dbo.qrytenderhead.tenderdoname, '' AS SendEmail FROM " +
                         "dbo.qrydohead LEFT OUTER JOIN dbo.qrytenderhead ON dbo.qrydohead.tenderid = dbo.qrytenderhead.tenderid " +
                         "where dbo.qrydohead.doc_date>='2017-07-01' and dbo.qrydohead.doc_date between '" + fromdt + "' and '" + todt + "' " +
                         "and dbo.qrydohead.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "   and dbo.qrydohead.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                         "GROUP BY dbo.qrydohead.doc_no, dbo.qrydohead.doc_dateConverted, dbo.qrydohead.shiptoname, dbo.qrydohead.shiptogstno, " +
                         "dbo.qrydohead.shiptoemail, dbo.qrydohead.quantal, dbo.qrydohead.mill_rate, dbo.qrydohead.sale_rate, " +
                         "dbo.qrydohead.final_amout, dbo.qrytenderhead.tenderdoname ";

            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    DataTable dtgrid = new DataTable();
                    DataRow dr = null;
                    dtgrid.Columns.Add(new DataColumn("SR_No", typeof(int)));
                    dtgrid.Columns.Add(new DataColumn("fromDate", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("ToDate", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("doc_no", typeof(int)));
                    dtgrid.Columns.Add(new DataColumn("Do_Date", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("shiptoname", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("shiptogstno", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("shiptoemail", typeof(string)));
                    dtgrid.Columns.Add(new DataColumn("quantal", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("mill_rate", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("sale_rate", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("final_amout", typeof(double)));
                    dtgrid.Columns.Add(new DataColumn("tenderdoname", typeof(string)));

                    lblSummary.Text = "Our DO";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["fromDate"] = txtFromDt.Text;
                        dt.Rows[i]["ToDate"] = txtToDt.Text;

                        dr = dtgrid.NewRow();
                        dr["SR_No"] = dt.Rows[i]["SR_No"];
                        dr["fromDate"] = dt.Rows[i]["fromDate"];
                        dr["ToDate"] = dt.Rows[i]["ToDate"];
                        dr["doc_no"] = dt.Rows[i]["doc_no"];
                        dr["Do_Date"] = dt.Rows[i]["Do_Date"];
                        dr["shiptoname"] = dt.Rows[i]["shiptoname"];
                        dr["shiptogstno"] = dt.Rows[i]["shiptogstno"];
                        dr["shiptoemail"] = dt.Rows[i]["shiptoemail"];
                        dr["quantal"] = dt.Rows[i]["quantal"];
                        dr["mill_rate"] = dt.Rows[i]["mill_rate"];
                        dr["sale_rate"] = dt.Rows[i]["sale_rate"];
                        dr["final_amout"] = dt.Rows[i]["final_amout"];
                        dr["tenderdoname"] = dt.Rows[i]["tenderdoname"];
                        dtgrid.Rows.Add(dr);
                    }

                    grdOurDO.DataSource = dt;
                    grdOurDO.DataBind();
                    ViewState["currentTable"] = dtgrid;

                    double totaltax = Convert.ToDouble(dt.Compute("SUM(final_amout)", string.Empty));

                    grdOurDO.FooterRow.Cells[11].Text = totaltax.ToString();

                    grdOurDO.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;

                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnDOSendEmail_Click(object sender, EventArgs e)
    {
        string mail = string.Empty;
        string pdfname = string.Empty;
        try
        {
            if (grdOurDO.Rows.Count > 0)
            {
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                for (int i = 0; i < grdOurDO.Rows.Count; i++)
                {
                    CheckBox chkpermissionsendemail = (CheckBox)grdOurDO.Rows[i].Cells[13].FindControl("chkpermissionsendemail");

                    int count = 0;

                    string DObody = string.Empty;
                    string DOsub = string.Empty;

                    if (lblSummary.Text == "Our DO")
                    {
                        if (chkpermissionsendemail.Checked == true)
                        {
                            DObody = "<table><tr><td align=center>Buyer Name</td><td></td><td align=center>Quantal</td><td></td><td align=center>Mill Rate</td><td></td><td align=center>Mill Amount</td><td>Tender Do Name</td></tr>" +
                                           "<tr><td>" + grdOurDO.Rows[i].Cells[5].Text.ToString() + "</td><td></td><td>" + grdOurDO.Rows[i].Cells[8].Text.ToString() + "</td><td></td><td>" + grdOurDO.Rows[i].Cells[9].Text.ToString() + "</td><td></td>" +
                                           "<td>" + grdOurDO.Rows[i].Cells[11].Text.ToString() + "</td><td align=right>" + grdOurDO.Rows[i].Cells[12].Text.ToString() + "</td></tr></table>";

                            DOsub = "Delivery Order";
                            count = count + 1;

                            mail = grdOurDO.Rows[i].Cells[7].Text.ToString();
                            if (mail != string.Empty)
                            {

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    MailMessage msg = new MailMessage();
                                    if (chkpermissionsendemail.Checked == true)
                                    {
                                        SmtpServer.Host = clsGV.Email_Address;
                                        msg.From = new MailAddress(mailFrom);
                                        msg.To.Add(mail);
                                        msg.IsBodyHtml = true;
                                        msg.Body = DObody;
                                        msg.Subject = DOsub;

                                        if (smtpPort != string.Empty)
                                        {
                                            SmtpServer.Port = Convert.ToInt32(smtpPort);
                                        }
                                        SmtpServer.EnableSsl = true;
                                        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        SmtpServer.UseDefaultCredentials = true;
                                        SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                                            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                        {
                                            return true;
                                        };
                                        SmtpServer.Send(msg);
                                        // attachment.Dispose();
                                        if (File.Exists(pdfname))
                                        {
                                            File.Delete(pdfname);
                                        }
                                    }

                                }

                            }
                        }
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:alert('Message Sent Successfully!')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region [RowCommandReport]
    protected void grdOurDO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    #endregion
    #region [grdOurDO_RowDataBound]
    protected void grdOurDO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[3].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[4].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[5].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[6].ControlStyle.Width = Unit.Percentage(25);
            e.Row.Cells[7].ControlStyle.Width = Unit.Percentage(30);
            e.Row.Cells[8].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[9].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[10].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[11].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[12].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[13].ControlStyle.Width = Unit.Percentage(10);
            //e.Row.Cells[14].ControlStyle.Width = Unit.Percentage(25);
            //e.Row.Cells[15].ControlStyle.Width = Unit.Percentage(10);

            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[7].Style["overflow"] = "hidden";
            //e.Row.Cells[14].Style["overflow"] = "hidden";

            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;




        }
        catch
        {
        }
    }
    #endregion
    protected void btncreditenote_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdt = txtFromDt.Text;
            string todt = txtToDt.Text;
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            string qry = "SELECT 0+ ROW_NUMBER() OVER(ORDER BY Company_Code) as SRNO,BILLTOGSTIN as [Receiver GSTIN/UIN ],(RIGHT('0'+CONVERT(NVARCHAR,BillToStateCode),2) +'-'+ LTRIM(RTRIM(BillToStatename))) as [Place of Supply],note_type as [Note Type]," +
                         "'Regular' as [Note Supply Type] ,'No' as [Reverse Charge],Inovice_No as [Voucher Number],Invoice_Date as[Voucher Date],bill_amount as [Voucher Value]," +
                         "TaxableAmt as [Taxable Value],Rate as [Tax Rate],IGST as [IGST Amount],CGST as [CGST Amount],SGST as [SGST Amount],0 as [Cess Amount] FROM dbo.qrydebitcreditandpurchasreturnunoin " +
                         " where doc_date between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    lblSummary.Text = "Credit Note GSTR1";
                    grdAll.DataSource = dt;
                    grdAll.DataBind();

                    //double totalQuintal = Convert.ToDouble(dt.Compute("SUM(Quintal)", string.Empty));
                    //double totalBagBillAmount = Convert.ToDouble(dt.Compute("SUM(TaxableAmount)", string.Empty));
                    //double totalCGST = Convert.ToDouble(dt.Compute("SUM(CGST)", string.Empty));
                    //double totalSGST = Convert.ToDouble(dt.Compute("SUM(SGST)", string.Empty));
                    //double totalIGST = Convert.ToDouble(dt.Compute("SUM(IGST)", string.Empty));
                    //double totalPayable_Amount = Convert.ToDouble(dt.Compute("SUM(Final_Amount)", string.Empty));

                    //grdAll.FooterRow.Cells[7].Text = totalQuintal.ToString();
                    //grdAll.FooterRow.Cells[9].Text = totalBagBillAmount.ToString();
                    //grdAll.FooterRow.Cells[10].Text = totalCGST.ToString();
                    //grdAll.FooterRow.Cells[11].Text = totalSGST.ToString();
                    //grdAll.FooterRow.Cells[12].Text = totalIGST.ToString();
                    //grdAll.FooterRow.Cells[13].Text = totalPayable_Amount.ToString();


                }

            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}