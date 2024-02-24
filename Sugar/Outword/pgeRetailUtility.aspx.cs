using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Threading;
using System.Data.SqlClient;

public partial class Sugar_Outword_pgeRetailUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "";
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (!IsPostBack)
        {
            BindDummyRow();

        }
        SetFocus(btnAdd);
    }
    private void BindDummyRow()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("Doc_No");
        dummy.Columns.Add("Doc_Date");
        dummy.Columns.Add("Tran_Type");
        dummy.Columns.Add("partyname");
        dummy.Columns.Add("Delivered");
        dummy.Columns.Add("brokername");
        dummy.Columns.Add("millshortname");
        dummy.Columns.Add("Brand_Code_Name");
        dummy.Columns.Add("Qty");
        dummy.Columns.Add("Rate");
        dummy.Columns.Add("Net_Value");
        dummy.Columns.Add("ack");
        dummy.Columns.Add("EwayBillNo");
        dummy.Columns.Add("IsDelete");
        dummy.Columns.Add("Retailid");

        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, string Trantype, int PageSize, int Company_Code, int year)
    {
        string searchtxt = "";
        string delimStr = "";
        char[] delimiter = delimStr.ToCharArray();
        string words = "";
        string[] split = null;
        string name = string.Empty;

        searchtxt = searchTerm;
        words = searchTerm;
        split = words.Split(delimiter);
        foreach (var s in split)
        {
            string aa = s.ToString();
            // name += "Doc_No Like '%" + aa + "%'or";
            name += "( dbo.Retail_Head.Party_Name like '%" + aa + "%' or dbo.Retail_Head.Doc_No like '%" + aa + "%' or Doc_Date like '%" + aa + "%' or Grand_total like '%" + aa + "%' or dbo.Retail_Head.Retailid like '%" + aa + "%'or mill.Ac_Name_E like '%" + aa + "%' ) and";


        }
        name = name.Remove(name.Length - 3);


      //  string query = "SELECT ROW_NUMBER() OVER ( order by Doc_No ASC) AS RowNumber,Doc_No,convert(varchar(10),Doc_Date,103) as Doc_Date,CashCredit as Tran_Type,"
      //+ "partyname as Party_Name ,case when Delivered='1' then 'No' else 'Yes' end as Delivered,brokername,millshortname,Brand_Code_Name,Qty,Rate,Net_Value,ack,EwayBillNo,IsDelete,Retailid FROM qryRetailSale   where " + name + " and CashCredit='" + Trantype + "' and Company_Code=" + Company_Code + " " +
      //" and Year_Code=" + year + " order by Doc_No desc, Doc_Date desc";

        //string query = "select row_number() over ( order by dbo.retail_head.doc_no asc) as rownumber,dbo.retail_head.doc_no, convert(varchar(10), dbo.retail_head.doc_date, 103) as doc_date, " +
        //               "dbo.retail_head.cashcredit as tran_type, dbo.retail_head.party_name, case when delivered = '1' then 'no' else 'yes' end as delivered, " +
        //               "broker.ac_name_e as brokername, mill.ac_name_e as millshortname, dbo.brand_master.marka as brand_code_name, dbo.retail_detail.qty, dbo.retail_detail.rate, " +
        //               "dbo.retail_detail.net_value, dbo.retail_head.ack, dbo.retail_head.ewaybillno, dbo.retail_head.isdelete, dbo.retail_head.retailid " +
        //               "from         dbo.retail_detail left outer join " +
        //               "dbo.brand_master on dbo.retail_detail.company_code = dbo.brand_master.company_code and " +
        //               "dbo.retail_detail.brand_code = dbo.brand_master.code left outer join " +
        //               "dbo.nt_1_accountmaster as mill on dbo.retail_detail.mc = mill.accoid right outer join " +
        //               "dbo.retail_head left outer join " +
        //               "dbo.nt_1_accountmaster as broker on dbo.retail_head.company_code = broker.company_code and dbo.retail_head.broker_code = broker.ac_code on " +
        //               "dbo.retail_detail.retailid = dbo.retail_head.retailid " +
        //               "where " + name + " and CashCredit='" + Trantype + "' and dbo.Retail_Head.company_code=" + Company_Code + " and dbo.Retail_Head.Year_Code=" + year + " order by dbo.Retail_Head.Doc_No desc, Doc_Date desc";



       
        string query = "SELECT ROW_NUMBER() OVER ( order by dbo.Retail_Head.Doc_No ASC) AS RowNumber,dbo.Retail_Head.Doc_No, CONVERT(varchar(10), dbo.Retail_Head.Doc_Date, 103) AS Doc_Date, " +
                         "dbo.Retail_Head.CashCredit AS Tran_Type, dbo.Retail_Head.Party_Name, CASE WHEN Delivered = '1' THEN 'No' ELSE 'Yes' END AS Delivered, " +
                         "broker.Ac_Name_E AS brokername,  mill.Ac_Name_E AS millshortname,dbo.Brand_Master.Marka AS Brand_Code_Name, dbo.Retail_Detail.Qty, dbo.Retail_Detail.Rate, " +
                         "dbo.Retail_Detail.Net_Value, dbo.Retail_Head.ACK, dbo.Retail_Head.EwayBillNo, dbo.Retail_Head.IsDelete, dbo.Retail_Head.Retailid " +
                         "FROM         dbo.Retail_Detail LEFT OUTER JOIN " +
                         "dbo.Brand_Master ON dbo.Retail_Detail.Company_Code = dbo.Brand_Master.Company_Code AND " +
                         "dbo.Retail_Detail.Brand_Code = dbo.Brand_Master.Code LEFT OUTER JOIN " +
                         "dbo.nt_1_accountmaster AS mill ON dbo.Retail_Detail.mc = mill.accoid RIGHT OUTER JOIN " +
                         "dbo.Retail_Head LEFT OUTER JOIN " +
                         "dbo.nt_1_accountmaster AS broker ON dbo.Retail_Head.Company_Code = broker.company_code AND dbo.Retail_Head.Broker_Code = broker.Ac_Code ON " +
                         "dbo.Retail_Detail.Retailid = dbo.Retail_Head.Retailid " +
                         "where " + name + " and CashCredit='" + Trantype + "' and dbo.Retail_Head.Company_Code=" + Company_Code + " and dbo.Retail_Head.Year_Code=" + year + " order by dbo.Retail_Head.Doc_No desc, Doc_Date desc";

        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.Text;
        return GetData(cmd, pageIndex, PageSize).GetXml();

    }

    private static DataSet GetData(SqlCommand cmd, int pageIndex, int PageSize)
    {

        string RecordCount = "";
        string cs1 = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs1))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {

                cmd.Connection = con;
                sda.SelectCommand = cmd;
                DataSet dsreturn = new DataSet();
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    int number = 1;
                    DataTable dtnew = new DataTable();
                    dtnew = ds.Tables[0];
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        dtnew.Rows[i][0] = number;
                        number = number + 1;

                    }
                    string f1 = " RowNumber >=(" + pageIndex + " -1) * (" + PageSize + "+1) and RowNumber<=";
                    string f2 = "(((" + pageIndex + " -1) * " + PageSize + " +1) +" + PageSize + ")-1";

                    DataRow[] results = ds.Tables[0].Select(f1 + f2, "Doc_Date desc");
                    if (results.Count() > 0)
                    {
                        DataTable dt1 = results.CopyToDataTable();
                        dt1.TableName = "Customers";
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        RecordCount = ds.Tables[0].Rows.Count.ToString();

                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = RecordCount;

                        dsreturn = new DataSet();
                        dsreturn.Tables.Add(dt1);
                        dsreturn.Tables.Add(dt);

                        return dsreturn;
                    }
                    else
                    {
                        return dsreturn;
                    }


                }
            }
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}