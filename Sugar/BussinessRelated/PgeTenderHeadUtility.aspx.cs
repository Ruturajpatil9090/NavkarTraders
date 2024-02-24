
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


public partial class Sugar_Master_PgeTenderHeadUtility : System.Web.UI.Page
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
        dummy.Columns.Add("Tender_No");
        dummy.Columns.Add("Tender_Date");
        dummy.Columns.Add("millshortname");
        dummy.Columns.Add("Quantal");
        dummy.Columns.Add("Grade");
        dummy.Columns.Add("Mill_Rate");
        dummy.Columns.Add("paymenttoname");
        dummy.Columns.Add("tenderdoname");
        dummy.Columns.Add("season");
        dummy.Columns.Add("brokershortname");
        dummy.Columns.Add("Lifting_Date");
        dummy.Columns.Add("tenderid");
        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, int PageSize, int Company_Code, int year)
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
            name += "( millto.Short_Name like '%" + aa + "%' or tenderdo.Ac_Name_E like '%" + aa + "%' or Grade like '%" + aa +
                "%' or Tender_No like '%" + aa + "%' or dbo.nt_1_tender.Tender_Date like '%" + aa + "%' or Quantal like '%" + aa +
                "%' or Mill_Rate like '%" + aa + "%'or paymentto.Ac_Name_E like '%" + aa + "%'or tenderid like '%" + aa + "%' or season like '%" + aa +
                "%' or broker.Ac_Name_E like '%" + aa + "%' or dbo.nt_1_tender.Lifting_Date like '%" + aa + "%') and";

        }
        name = name.Remove(name.Length - 3);

       // string query = "SELECT ROW_NUMBER() OVER ( order by Tender_No desc) AS RowNumber,Tender_No,Tender_DateConverted as Tender_Date,"
       //+ "millshortname ,Quantal,Grade,Mill_Rate,paymenttoname,tenderdoname,season,brokershortname,Lifting_DateConverted as Lifting_Date,tenderid FROM qrytenderhead   where " + name + " and Company_Code=" + Company_Code + " " +
       //"  order by Tender_no desc";

        string query = "SELECT ROW_NUMBER() OVER ( order by Tender_No desc) AS RowNumber,dbo.nt_1_tender.Tender_No, CONVERT(varchar(10), " + 
                       "dbo.nt_1_tender.Tender_Date, 103) AS Tender_Date, millto.Short_Name AS millshortname, dbo.nt_1_tender.Quantal, " +
                       "dbo.nt_1_tender.Grade, dbo.nt_1_tender.Mill_Rate, paymentto.Ac_Name_E AS paymenttoname, tenderdo.Ac_Name_E AS tenderdoname, " +
                       "dbo.nt_1_tender.season, broker.Ac_Name_E AS brokershortname, CONVERT(varchar(10), " +
                       "dbo.nt_1_tender.Lifting_Date, 103) AS Lifting_Date, dbo.nt_1_tender.tenderid " +
                       "FROM         dbo.nt_1_tender LEFT OUTER JOIN " +
                       "dbo.nt_1_accountmaster AS tenderdo ON dbo.nt_1_tender.td = tenderdo.accoid LEFT OUTER JOIN " +
                       "dbo.nt_1_accountmaster AS broker ON dbo.nt_1_tender.bk = broker.accoid LEFT OUTER JOIN " +
                       "dbo.nt_1_accountmaster AS paymentto ON dbo.nt_1_tender.pt = paymentto.accoid LEFT OUTER JOIN " +
                       "dbo.nt_1_accountmaster AS millto ON dbo.nt_1_tender.mc = millto.accoid " +
                       "where " + name + " and dbo.nt_1_tender.Company_Code=" + Company_Code + "  order by Tender_no desc";

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

                    DataRow[] results = ds.Tables[0].Select(f1 + f2, "Tender_No desc");
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