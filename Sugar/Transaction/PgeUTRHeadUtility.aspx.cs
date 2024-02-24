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

public partial class Sugar_Master_PgeUTRHeadUtility : System.Web.UI.Page
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
        dummy.Columns.Add("doc_no");
        dummy.Columns.Add("doc_date");
        dummy.Columns.Add("bankShortName");
        dummy.Columns.Add("millshortname");
        dummy.Columns.Add("amount");
        dummy.Columns.Add("utr_no");
        dummy.Columns.Add("narration_header");
        dummy.Columns.Add("narration_footer");
        dummy.Columns.Add("utrid");

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
            name += "( mill.Ac_Name_E like '%" + aa + "%' or doc_no like '%" + aa + "%' or doc_date like '%" + aa + "%' or utr_no like '%" + aa +
                    "%' or amount like '%" + aa + "%' or utrid like '%" + aa + "%' or bank.Short_Name like '%" + aa + "%' or narration_header like '%" + aa +
                    "%' or narration_footer like '%" + aa + "%'or mill.Short_Name like '%" + aa + "%') and";


        }
        name = name.Remove(name.Length - 3);


       // string query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,bankShortName,millshortname,amount,utr_no,narration_header,narration_footer,"
       //+ "utrid FROM qryutrhead   where " + name + " and Company_Code=" + Company_Code + " " +
       //" and Year_Code=" + year + " order by doc_no desc, doc_date desc";

      //  string query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,bankShortName,millshortname,amount,utr_no,narration_header,narration_footer,"
      //+ "utrid FROM qryutrhead   where " + name + " and Company_Code=" + Company_Code + " " +
      //" order by doc_no desc, doc_date desc";

         string query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,dbo.nt_1_utr.doc_no, CONVERT(varchar(10), dbo.nt_1_utr.doc_date, 103) AS doc_date, " +
                        "bank.Short_Name AS bankShortName, mill.Short_Name AS millshortname, dbo.nt_1_utr.amount, dbo.nt_1_utr.utr_no, " +
                        "dbo.nt_1_utr.narration_header, dbo.nt_1_utr.narration_footer, dbo.nt_1_utr.utrid, mill.Ac_Name_E AS millname " +
                        "FROM         dbo.nt_1_utr LEFT OUTER JOIN " +
                        "dbo.nt_1_accountmaster AS mill ON dbo.nt_1_utr.mc = mill.accoid LEFT OUTER JOIN " +
                        "dbo.nt_1_accountmaster AS bank ON dbo.nt_1_utr.ba = bank.accoid " +
                        "where " + name + " and dbo.nt_1_utr.Company_Code=" + Company_Code + " order by doc_no desc, doc_date desc";
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
    protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    string qry = string.Empty;
    //    qry = " select doc_no,doc_dateConverted as doc_date,"
    //   + "millname ,utr_no,amount,utrid  FROM " + qryCommon + " limit 15";
    //    ds = clsDAL.SimpleQuery(qry);
    //    if (ds != null)
    //    {
    //        dt = ds.Tables[0];
    //        if (dt.Rows.Count > 0)
    //        {
    //            gvCustomers.DataSource = dt;
    //            gvCustomers.DataBind();
    //            ViewState["currentTable"] = dt;
    //        }
    //    }
    //    else
    //    {
    //        gvCustomers.DataSource = null;
    //        gvCustomers.DataBind();
    //        ViewState["currentTable"] = null;
    //    }
    }
}