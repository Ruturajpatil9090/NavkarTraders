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


public partial class Sugar_BussinessRelated_pgeGoodsTransportUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qrydohead";
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (!IsPostBack)
        {
            BindDummyRow();
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        } 
    }
    private void BindDummyRow()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("doc_no");
       dummy.Columns.Add("Do_DATE");
        dummy.Columns.Add("doc_date");
        dummy.Columns.Add("driver_no");
        dummy.Columns.Add("truck_no");
        dummy.Columns.Add("millshortname");
        dummy.Columns.Add("grade");
        dummy.Columns.Add("quantal");
        dummy.Columns.Add("billtoshortname");
        dummy.Columns.Add("Freight_Amount");
        dummy.Columns.Add("memo_advance");
        dummy.Columns.Add("transportshortname");
        dummy.Columns.Add("reached_date");
        dummy.Columns.Add("EWay_Bill_No");
        dummy.Columns.Add("doid");
        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, string Trantype,int PageSize, int Company_Code, int year)
    {
        string searchtxt = "";
        string delimStr = "";
        char[] delimiter = delimStr.ToCharArray();
        string words = "";
        string[] split = null;
        string name = string.Empty;
        //DateTime endD = DateTime.Parse(EndDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //DateTime d = new DateTime();
        //if (FromDate != "" && ToDate == "")
        //{
        //    d = DateTime.Parse(FromDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        //    d = d.AddDays(+(Convert.ToInt32(30)));
        //    ToDate = d.ToString("yyyy/MM/dd");

        //}
        //if (ToDate == "")
        //{

        //    d = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //    if (endD > d)
        //    {
        //        ToDate = DateTime.Now.ToString("yyyy/MM/dd");
        //    }
        //    else
        //    {
        //        ToDate = endD.ToString("yyyy/MM/dd");
        //        d = endD;
        //    }

        //}
        //if (FromDate == "")
        //{
        //    d = d.AddDays(-(Convert.ToInt32(30)));
        //    FromDate = d.ToString("yyyy/MM/dd");

        //}
        searchtxt = searchTerm;
        words = searchTerm;
        split = words.Split(delimiter);
        foreach (var s in split)
        {
            string aa = s.ToString();
            // name += "Doc_No Like '%" + aa + "%'or";
            //name += "( millshortname like '%" + aa + "%' or billtoshortname like  '%" + aa + "%'or Do_Date_Conv like  '%" + aa + "%' or doc_dateConverted like '%" + aa +
            //    "%' or driver_no like '%" + aa + "%' or truck_no like '%" + aa + "%' or reached_dateConv like '%" + aa + "%') and";

            name += "( convert(varchar(10),dbo.nt_1_deliveryorder.Do_DATE,120) like '%" + aa + "%' or convert(varchar(10),dbo.nt_1_deliveryorder.doc_date,120) like '%" + aa +
                      "%' or convert(varchar(10),dbo.nt_1_deliveryorder.reached_date,120) like '%" + aa + "%' or Mill.Short_Name like '%" + aa + "%' or BillTo.Short_Name  like  '%" + aa + "%'  or Transport.Mobile_No like '%" + aa +
                "%' or dbo.nt_1_deliveryorder.truck_no like '%" + aa + "%' ) and";


        }
        name = name.Remove(name.Length - 3);

        //string query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,Do_Date_Conv as Do_DATE,doc_dateConverted as doc_date,"
        // + " driver_no,truck_no,millshortname,grade,quantal,billtoshortname,Freight_Amount,memo_advance,transportshortname,reached_dateConv as reached_date,EWay_Bill_No,doid FROM qrydohead   where " + name + "   and Company_Code=" + Company_Code + " " +
        // " and Year_Code=" + year + " order by doc_no desc";

        string query = "";

        if (Trantype == "Y")
        {
            query = "SELECT ROW_NUMBER() OVER ( order by dbo.nt_1_deliveryorder.doc_no ASC) AS RowNumber,dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.Do_DATE, 103) AS Do_DATE, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, " +
                      "Transport.Mobile_No AS driver_no, dbo.nt_1_deliveryorder.truck_no, Mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.grade, dbo.nt_1_deliveryorder.quantal," +
                      "BillTo.Short_Name AS billtoshortname, dbo.nt_1_deliveryorder.Freight_Amount, dbo.nt_1_deliveryorder.Memo_Advance, Transport.Short_Name AS transportshortname, CONVERT(varchar(10)," +
                      "dbo.nt_1_deliveryorder.reached_date, 103) AS reached_date, dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.doid" +
                      " FROM dbo.nt_1_deliveryorder LEFT OUTER JOIN dbo.nt_1_accountmaster AS BillTo ON dbo.nt_1_deliveryorder.sb = BillTo.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS Mill ON dbo.nt_1_deliveryorder.mc = Mill.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS Transport ON dbo.nt_1_deliveryorder.tc = Transport.accoid " +
                      " where " + name + "   and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " and dbo.nt_1_deliveryorder.Year_Code=" + year + " and vehicle_reached='Y' order by doc_no desc ";
        }
        else if (Trantype == "N")
        {
            query = "SELECT ROW_NUMBER() OVER ( order by dbo.nt_1_deliveryorder.doc_no ASC) AS RowNumber,dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.Do_DATE, 103) AS Do_DATE, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, " +
                      "Transport.Mobile_No AS driver_no, dbo.nt_1_deliveryorder.truck_no, Mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.grade, dbo.nt_1_deliveryorder.quantal," +
                      "BillTo.Short_Name AS billtoshortname, dbo.nt_1_deliveryorder.Freight_Amount, dbo.nt_1_deliveryorder.Memo_Advance, Transport.Short_Name AS transportshortname, CONVERT(varchar(10)," +
                      "dbo.nt_1_deliveryorder.reached_date, 103) AS reached_date, dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.doid" +
                      " FROM dbo.nt_1_deliveryorder LEFT OUTER JOIN dbo.nt_1_accountmaster AS BillTo ON dbo.nt_1_deliveryorder.sb = BillTo.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS Mill ON dbo.nt_1_deliveryorder.mc = Mill.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS Transport ON dbo.nt_1_deliveryorder.tc = Transport.accoid " +
                      " where " + name + "   and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " and dbo.nt_1_deliveryorder.Year_Code=" + year + " and vehicle_reached='N' order by doc_no desc ";
        }
        else if (Trantype == "US")
        {
            query = "SELECT ROW_NUMBER() OVER ( order by dbo.nt_1_deliveryorder.doc_no ASC) AS RowNumber,dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.Do_DATE, 103) AS Do_DATE, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, " +
                      "Transport.Mobile_No AS driver_no, dbo.nt_1_deliveryorder.truck_no, Mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.grade, dbo.nt_1_deliveryorder.quantal," +
                      "BillTo.Short_Name AS billtoshortname, dbo.nt_1_deliveryorder.Freight_Amount, dbo.nt_1_deliveryorder.Memo_Advance, Transport.Short_Name AS transportshortname, CONVERT(varchar(10)," +
                      "dbo.nt_1_deliveryorder.reached_date, 103) AS reached_date, dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.doid" +
                      " FROM dbo.nt_1_deliveryorder LEFT OUTER JOIN dbo.nt_1_accountmaster AS BillTo ON dbo.nt_1_deliveryorder.sb = BillTo.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS Mill ON dbo.nt_1_deliveryorder.mc = Mill.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS Transport ON dbo.nt_1_deliveryorder.tc = Transport.accoid " +
                      " where " + name + "   and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " and dbo.nt_1_deliveryorder.Year_Code=" + year + " and vehicle_reached='N' and SB_No='0' order by doc_no desc ";
        }
        else
        {
            query = "SELECT ROW_NUMBER() OVER ( order by dbo.nt_1_deliveryorder.doc_no ASC) AS RowNumber,dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.Do_DATE, 103) AS Do_DATE, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, " +
                          "Transport.Mobile_No AS driver_no, dbo.nt_1_deliveryorder.truck_no, Mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.grade, dbo.nt_1_deliveryorder.quantal," +
                          "BillTo.Short_Name AS billtoshortname, dbo.nt_1_deliveryorder.Freight_Amount, dbo.nt_1_deliveryorder.Memo_Advance, Transport.Short_Name AS transportshortname, CONVERT(varchar(10)," +
                          "dbo.nt_1_deliveryorder.reached_date, 103) AS reached_date, dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.doid" +
                          " FROM dbo.nt_1_deliveryorder LEFT OUTER JOIN dbo.nt_1_accountmaster AS BillTo ON dbo.nt_1_deliveryorder.sb = BillTo.accoid LEFT OUTER JOIN " +
                          " dbo.nt_1_accountmaster AS Mill ON dbo.nt_1_deliveryorder.mc = Mill.accoid LEFT OUTER JOIN " +
                          " dbo.nt_1_accountmaster AS Transport ON dbo.nt_1_deliveryorder.tc = Transport.accoid " +
                          " where " + name + "   and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " and dbo.nt_1_deliveryorder.Year_Code=" + year + " order by doc_no desc ";
        }
        
      
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

                    DataRow[] results = ds.Tables[0].Select(f1 + f2, "doc_no desc");
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