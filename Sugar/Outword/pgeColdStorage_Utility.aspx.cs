using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Inword_pgeColdStorage_Utility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    int PageSize;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qrymstaccountmaster";
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
        dummy.Columns.Add("Date");
        dummy.Columns.Add("Cash_Credit");
        dummy.Columns.Add("Ac_Name_E");
        dummy.Columns.Add("Final_Amount");
        dummy.Columns.Add("csid");
      
        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();


    }

    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, int PageSize, int Company_Code,int Year_Code)
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
            // name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
            name += "( Doc_No like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Customer_Code like '%" + aa + "%' or Cash_Credit like '%" + aa + "%') and";


        }
        name = name.Remove(name.Length - 3);
        //string query = "SELECT ROW_NUMBER() OVER ( order by DOC_NO ASC) AS RowNumber,DOC_NO as CustomerID,Ac_Name_E as ContactName,"
        //+ "AMOUNT ,TRAN_TYPE as City FROM qryGledgernew where " + name + "";
        string query = string.Empty;

        query = "SELECT ROW_NUMBER() OVER ( order by Doc_No ASC) AS RowNumber,Doc_No,convert(varchar(10),Date,103) as Date,Cash_Credit,Ac_Name_E,Final_Amount,"
         + "csid  FROM qryColdStorageHead where Company_Code='" +
         Company_Code + "' and Year_Code='" + Year_Code + "' and " + name + "  order by Doc_No desc ";
       

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

                    DataRow[] results = dtnew.Select(f1 + f2, "Doc_No desc");
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


}